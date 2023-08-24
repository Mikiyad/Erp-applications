using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Interfaces.WS;
using Micromind.Common.Libraries;
using Micromind.Data;
using Micromind.Facade.WS;
using Micromind.Securities;
using Micromind.Securities.Cryptography;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Threading;

namespace Micromind.Facade.Factories
{
	public class WebCompanySystem : MarshalByRefObject, IWebCompanySystem, IDisposable
	{
		private bool isMultiUser;

		private SortedList configCol;

		public bool IsMultiUser
		{
			get
			{
				return isMultiUser;
			}
			set
			{
				isMultiUser = value;
			}
		}

		public WebCompanySystem()
		{
			configCol = new SortedList();
			CultureInfo currentCulture = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name)
			{
				NumberFormat = 
				{
					CurrencyDecimalSeparator = ".",
					NumberDecimalSeparator = "."
				}
			};
			Thread.CurrentThread.CurrentCulture = currentCulture;
		}

		~WebCompanySystem()
		{
			Dispose();
		}

		public override object InitializeLifetimeService()
		{
			ILease lease = (ILease)base.InitializeLifetimeService();
			if (lease.CurrentState == LeaseState.Initial)
			{
				lease.InitialLeaseTime = TimeSpan.FromHours(5.0);
				lease.SponsorshipTimeout = TimeSpan.FromHours(5.0);
				lease.RenewOnCallTime = TimeSpan.FromHours(5.0);
			}
			return lease;
		}

		private void AddConfig(string key, ConfigData configData)
		{
			if (!configCol.Contains(key))
			{
				configCol.Add(key, configData);
			}
			else
			{
				configCol[key] = configData;
			}
		}

		public void RemoveWorkerTokens(RequestInfo requestInfo)
		{
			checked
			{
				for (int i = 0; i < configCol.Keys.Count; i++)
				{
					string text = configCol.GetKey(i).ToString();
					ConfigData configData = configCol[text] as ConfigData;
					if (configData != null && !(configData.UserID != requestInfo.UserID) && !(configData.ClientMachineName != requestInfo.MachineName) && text != requestInfo.LoginToken && configData.config.IsAsync)
					{
						requestInfo.LoginToken = text;
						RemoveConfig(requestInfo);
						i--;
					}
				}
			}
		}

		private void RemoveConfig(RequestInfo requestInfo)
		{
			try
			{
				GetConfig(requestInfo).Disconnect();
				configCol.Remove(requestInfo.LoginToken);
			}
			catch
			{
			}
		}

		private Config GetConfig(RequestInfo requestInfo)
		{
			string loginToken = requestInfo.LoginToken;
			if (loginToken == null || loginToken == string.Empty)
			{
				throw new ApplicationException("Configuration key is not set or not correct.");
			}
			ConfigData configData = new ConfigData(requestInfo.AppName, requestInfo.MachineName, requestInfo.ServerName, requestInfo.SystemID, requestInfo.ProductKey, requestInfo.DBName, requestInfo.UserID, requestInfo.Password, null);
			if (configData == null)
			{
				throw new CompanyException("Connection to server lost. Please login again.", 1041);
			}
			configData.ID = requestInfo.ID;
			if (configData.config == null || configData.config.UserID == null)
			{
				configData.config = new Config(configData.ID);
				configData.config.SetLoginInfo(configData.ApplicationName, configData.ClientMachineName, configData.ServerName, configData.SystemID, configData.ProductKey, configData.DbName, configData.UserID, configData.Password);
			}
			configData.lastLogDate = DateTime.Now;
			return configData.config;
		}

		public ActivityData GetActiveUsers()
		{
			ArrayList arrayList = new ArrayList();
			ActivityData activityData = new ActivityData();
			foreach (ConfigData value in configCol.Values)
			{
				if (value != null && !arrayList.Contains(value.UserID))
				{
					arrayList.Add(value.UserID);
					DataRow dataRow = activityData.ActivityTable.NewRow();
					dataRow[ActivityData.USERID_FIELD] = value.UserID;
					dataRow[ActivityData.MACHINEID_FIELD] = value.ClientMachineName;
					dataRow[ActivityData.LOGINDATE_FIELD] = value.loginDateTime;
					dataRow[ActivityData.LASTLOGDATE_FIELD] = value.lastLogDate;
					dataRow[ActivityData.DBNAME_FIELD] = value.DbName;
					dataRow[ActivityData.TOKENID_FIELD] = value.ID;
					dataRow[ActivityData.SYSTEMID_FIELD] = value.SystemID;
					dataRow[ActivityData.PRODUCTKEY_FIELD] = value.ProductKey;
					dataRow[ActivityData.APPNAME_FIELD] = value.ApplicationName;
					dataRow[ActivityData.SERVERID_FIELD] = value.ServerName;
					activityData.ActivityTable.Rows.Add(dataRow);
				}
			}
			return activityData;
		}

		public void Dispose()
		{
			if (configCol != null)
			{
				configCol.Clear();
				configCol = null;
			}
		}

		private ICryptor GetDefaultCryptor()
		{
			return new AESCryptor(new CryptoHelper());
		}

		public string CreateEmptyLogin(RequestInfo requestInfo)
		{
			lock (this)
			{
				requestInfo.LoginToken = Guid.NewGuid().ToString("N");
				try
				{
					new ConfigHelper(requestInfo.LoginToken);
					Config config = new Config(requestInfo.LoginToken, ConnectionTypes.LocalServer);
					ConfigData configData = new ConfigData(requestInfo.AppName, requestInfo.MachineName, "", requestInfo.ProductKey, requestInfo.ServerName, "", "", "", config);
					configData.ID = requestInfo.LoginToken;
					AddConfig(requestInfo.LoginToken, configData);
					return requestInfo.LoginToken;
				}
				catch
				{
					throw;
				}
			}
		}

		public string SetLoginInfo(RequestInfo requestInfo)
		{
			return SetLoginInfo(requestInfo, isAsync: false);
		}

		public string SetLoginInfo(RequestInfo requestInfo, bool isAsync)
		{
			if (requestInfo.DBName == "")
			{
				requestInfo.DBName = "Master";
			}
			lock (this)
			{
				requestInfo.LoginToken = Guid.NewGuid().ToString("N");
				try
				{
					ConfigHelper configHelper = new ConfigHelper(requestInfo.ID);
					Config config = new Config(requestInfo.ID, requestInfo.ConnectionType);
					config.IsAsync = isAsync;
					config.SetLoginInfo(requestInfo.AppName, requestInfo.MachineName, requestInfo.ServerName, requestInfo.SystemID, requestInfo.ProductKey, requestInfo.DBName, requestInfo.UserID, requestInfo.Password, requestInfo.adminUserName, requestInfo.adminUserPassword, configHelper.Cryptor, requestInfo.ID, requestInfo.ConnectionType);
					config.OpenConnection();
					ConfigData configData = new ConfigData(requestInfo.AppName, requestInfo.MachineName, requestInfo.ServerName, requestInfo.SystemID, requestInfo.ProductKey, requestInfo.DBName, requestInfo.UserID, requestInfo.Password, config);
					AddConfig(requestInfo.LoginToken, configData);
					return requestInfo.LoginToken;
				}
				catch
				{
					throw;
				}
			}
		}

		private bool IsKeyInUse(string yourToken, string systemID, string productKey)
		{
			IDictionaryEnumerator enumerator = configCol.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ConfigData configData = enumerator.Value as ConfigData;
				if (configData != null && configData.ProductKey == productKey && configData.SystemID != systemID)
				{
					throw new CompanyException("This product license is already in use by machine '" + configData.ClientMachineName + "'. Please logoff the user or add an additional license.", 1024);
				}
			}
			return false;
		}

		public DateTime GetLastUpdateDate()
		{
			string text = StoreConfiguration.OriginalDiectory + Path.DirectorySeparatorChar.ToString() + GetDownloadDirName() + Path.DirectorySeparatorChar.ToString() + "App.config";
			if (!File.Exists(text))
			{
				return DateTime.MinValue;
			}
			XMLReader.FilePath = text;
			string key = XMLReader.GetKey("LastUpdateDateTime");
			DateTime result = DateTime.MinValue;
			try
			{
				if (key == null)
				{
					return result;
				}
				result = DateTime.Parse(key);
				return result;
			}
			catch
			{
				return result;
			}
		}

		public string GetDownloadDirName()
		{
			string key = XMLReader.GetKey("DownloadDir");
			if (key != null && key.Trim() != string.Empty)
			{
				return key;
			}
			XMLReader.CreateKey("DownloadDir", "Downloads");
			return "Downloads";
		}

		public string GetPatchSetupFile()
		{
			string key = XMLReader.GetKey("PatchSetupFile");
			if (key != null && key.Trim() != string.Empty)
			{
				return key;
			}
			XMLReader.CreateKey("PatchSetupFile", "patch.exe");
			return "patch.exe";
		}

		public Stream GetUpdateSetupStream()
		{
			Stream stream = null;
			string path = StoreConfiguration.OriginalDiectory + Path.DirectorySeparatorChar.ToString() + GetDownloadDirName() + Path.DirectorySeparatorChar.ToString() + GetPatchSetupFile();
			checked
			{
				if (File.Exists(path))
				{
					stream = new MemoryStream();
					Stream stream2 = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
					byte[] array = new byte[32768];
					stream.Seek(0L, SeekOrigin.Begin);
					int num = 0;
					int num2;
					while ((num2 = stream2.Read(array, num, array.Length - num)) > 0)
					{
						num += num2;
						if (num == array.Length)
						{
							int num3 = stream2.ReadByte();
							if (num3 == -1)
							{
								break;
							}
							byte[] array2 = new byte[array.Length * 2];
							Array.Copy(array, array2, array.Length);
							array2[num] = (byte)num3;
							array = array2;
							num++;
						}
					}
					stream.Write(array, 0, num);
					stream.Flush();
					stream.Close();
				}
				return stream;
			}
		}

		public bool LogClientSignIn(RequestInfo requestInfo)
		{
			return GetConfig(requestInfo).LogClientSignIn();
		}

		private bool LogClientSignOut(RequestInfo requestInfo)
		{
			return GetConfig(requestInfo).LogClientSignOut();
		}

		public bool IsConnected(RequestInfo requestInfo)
		{
			ConfigData configData = configCol[requestInfo.LoginToken] as ConfigData;
			if (configData != null && configData.config != null)
			{
				return true;
			}
			return false;
		}

		public void Disconnect(RequestInfo requestInfo)
		{
			LogClientSignOut(requestInfo);
			RemoveConfig(requestInfo);
		}

		public IProductSystem CreateProductSystem(RequestInfo requestInfo)
		{
			return new ProductSystem(GetConfig(requestInfo));
		}

		public IUnitSystem CreateUnitSystem(RequestInfo requestInfo)
		{
			return new UnitSystem(GetConfig(requestInfo));
		}

		public IProductCategorySystem CreateProductCategorySystem(RequestInfo requestInfo)
		{
			return new ProductCategorySystem(GetConfig(requestInfo));
		}

		public IMaintenanceSchedulerSystem CreateMaintenanceSchedulerSystem(RequestInfo requestInfo)
		{
			return new MaintenanceSchedulerSystem(GetConfig(requestInfo));
		}

		public IVehicleMaintenanceEntrySystem CreateMaintenanceEntrySystem(RequestInfo requestInfo)
		{
			return new VehicleMaintenanceEntrySystem(GetConfig(requestInfo));
		}

		public IEmployeeSystem CreateEmployeeSystem(RequestInfo requestInfo)
		{
			return new EmployeeSystem(GetConfig(requestInfo));
		}

		public IEmployeeProvisionSystem CreateEmployeeProvisionSystem(RequestInfo requestInfo)
		{
			return new EmployeeProvisionSystem(GetConfig(requestInfo));
		}

		public ICandidateSystem CreateCandidateSystem(RequestInfo requestInfo)
		{
			return new CandidateSystem(GetConfig(requestInfo));
		}

		public IShippingMethodSystem CreateShippingMethodSystem(RequestInfo requestInfo)
		{
			return new ShippingMethodSystem(GetConfig(requestInfo));
		}

		public ICompanyAccountSystem CreateCompanyAccountSystem(RequestInfo requestInfo)
		{
			return new CompanyAccountSystem(GetConfig(requestInfo));
		}

		public ICurrencySystem CreateCurrencySystem(RequestInfo requestInfo)
		{
			return new CurrencySystem(GetConfig(requestInfo));
		}

		public IBankSystem CreateBankSystem(RequestInfo requestInfo)
		{
			return new BankSystem(GetConfig(requestInfo));
		}

		public IPhysicalStockEntrySystem CreatePhysicalStockEntrySystem(RequestInfo requestInfo)
		{
			return new PhysicalStockEntrySystem(GetConfig(requestInfo));
		}

		public IProductManufacturerSystem CreateProductManufacturerSystem(RequestInfo requestInfo)
		{
			return new ProductManufacturerSystem(GetConfig(requestInfo));
		}

		public IReleaseTypeSystem CreateReleaseTypeSystem(RequestInfo requestInfo)
		{
			return new ReleaseTypeSystem(GetConfig(requestInfo));
		}

		public ILeadStatusSystem CreateLeadStatusSystem(RequestInfo requestInfo)
		{
			return new LeadStatusSystem(GetConfig(requestInfo));
		}

		public IProductBrandSystem CreateProductBrandSystem(RequestInfo requestInfo)
		{
			return new ProductBrandSystem(GetConfig(requestInfo));
		}

		public IProductStyleSystem CreateProductStyleSystem(RequestInfo requestInfo)
		{
			return new ProductStyleSystem(GetConfig(requestInfo));
		}

		public IProductSpecificationSystem CreateProductSpecificationSystem(RequestInfo requestInfo)
		{
			return new ProductSpecificationSystem(GetConfig(requestInfo));
		}

		public IProductAttributeSystem CreateProductAttributeSystem(RequestInfo requestInfo)
		{
			return new ProductAttributeSystem(GetConfig(requestInfo));
		}

		public IProductSizeSystem CreateProductSizeSystem(RequestInfo requestInfo)
		{
			return new ProductSizeSystem(GetConfig(requestInfo));
		}

		public ITransactionSystem CreateTransactionSystem(RequestInfo requestInfo)
		{
			return new TransactionSystem(GetConfig(requestInfo));
		}

		public IContainerSizeSystem CreateContainerSizeSystem(RequestInfo requestInfo)
		{
			return new ContainerSizeSystem(GetConfig(requestInfo));
		}

		public IPurchaseOrderSystem CreatePurchaseOrderSystem(RequestInfo requestInfo)
		{
			return new PurchaseOrderSystem(GetConfig(requestInfo));
		}

		public IPurchaseCostEntrySystem CreatePurchaseCostEntrySystem(RequestInfo requestInfo)
		{
			return new PurchaseCostEntrySystem(GetConfig(requestInfo));
		}

		public IPurchaseOrderNISystem CreatePurchaseOrderNISystem(RequestInfo requestInfo)
		{
			return new PurchaseOrderNISystem(GetConfig(requestInfo));
		}

		public IHolidayCalendarSystem CreateHolidayCalendarSystem(RequestInfo requestInfo)
		{
			return new HolidayCalendarSystem(GetConfig(requestInfo));
		}

		public ITermSystem CreateTermSystem(RequestInfo requestInfo)
		{
			return new TermSystem(GetConfig(requestInfo));
		}

		public ISecuritySystem CreateSecuritySystem(RequestInfo requestInfo)
		{
			return new SecuritySystem(GetConfig(requestInfo));
		}

		public ICompanyInformationSystem CreateCompanyInformationSystem(RequestInfo requestInfo)
		{
			return new CompanyInformationSystem(GetConfig(requestInfo));
		}

		public IDatabaseSystem CreateDatabaseSystem(RequestInfo requestInfo)
		{
			return new DatabaseSystem(GetConfig(requestInfo));
		}

		public INoteSystem CreateNoteSystem(RequestInfo requestInfo)
		{
			return new NoteSystem(GetConfig(requestInfo));
		}

		public ISettingSystem CreateSettingSystem(RequestInfo requestInfo)
		{
			return new SettingSystem(GetConfig(requestInfo));
		}

		public IPrintTemplateSystem CreatePrintTemplateSystem(RequestInfo requestInfo)
		{
			return new PrintTemplateSystem(GetConfig(requestInfo));
		}

		public IDepartmentSystem CreateDepartmentSystem(RequestInfo requestInfo)
		{
			return new DepartmentSystem(GetConfig(requestInfo));
		}

		public IPriceLevelSystem CreatePriceLevelSystem(RequestInfo requestInfo)
		{
			return new PriceLevelSystem(GetConfig(requestInfo));
		}

		public IPaymentMethodSystem CreatePaymentMethodSystem(RequestInfo requestInfo)
		{
			return new PaymentMethodSystem(GetConfig(requestInfo));
		}

		public IActivityLogSystem CreateActivityLogSystem(RequestInfo requestInfo)
		{
			return new ActivityLogSystem(GetConfig(requestInfo));
		}

		public IFollowupSystem CreateFollowupSystem(RequestInfo requestInfo)
		{
			return new FollowupSystem(GetConfig(requestInfo));
		}

		public ILicenseSystem CreateLicenseSystem(RequestInfo requestInfo)
		{
			return new LicenseSystem(GetConfig(requestInfo));
		}

		public IScheduleSystem CreateScheduleSystem(RequestInfo requestInfo)
		{
			return new ScheduleSystem(GetConfig(requestInfo));
		}

		public IAccountGroupsSystem CreateAccountGroupsSystem(RequestInfo requestInfo)
		{
			return new AccountGroupsSystem(GetConfig(requestInfo));
		}

		public IAnalysisGroupsSystem CreateAnalysisGroupsSystem(RequestInfo requestInfo)
		{
			return new AnalysisGroupsSystem(GetConfig(requestInfo));
		}

		public IAnalysisSystem CreateAnalysisSystem(RequestInfo requestInfo)
		{
			return new AnalysisSystem(GetConfig(requestInfo));
		}

		public ICustomerSystem CreateCustomerSystem(RequestInfo requestInfo)
		{
			return new CustomerSystem(GetConfig(requestInfo));
		}

		public ICountrySystem CreateCountrySystem(RequestInfo requestInfo)
		{
			return new CountrySystem(GetConfig(requestInfo));
		}

		public ICustomerClassSystem CreateCustomerClassSystem(RequestInfo requestInfo)
		{
			return new CustomerClassSystem(GetConfig(requestInfo));
		}

		public IAreaSystem CreateAreaSystem(RequestInfo requestInfo)
		{
			return new AreaSystem(GetConfig(requestInfo));
		}

		public ITransporterSystem CreateTransporterSystem(RequestInfo requestInfo)
		{
			return new TransporterSystem(GetConfig(requestInfo));
		}

		public IINCOSystem CreateINCOSystem(RequestInfo requestInfo)
		{
			return new INCOSystem(GetConfig(requestInfo));
		}

		public IPartySystem CreatePartySystem(RequestInfo requestInfo)
		{
			return new PartySystem(GetConfig(requestInfo));
		}

		public IAccountAnalysisDetailSystem CreateAccountAnalysisDetailSystem(RequestInfo requestInfo)
		{
			return new AccountAnalysisSystem(GetConfig(requestInfo));
		}

		public ISalespersonSystem CreateSalespersonSystem(RequestInfo requestInfo)
		{
			return new SalespersonSystem(GetConfig(requestInfo));
		}

		public ICustomerAddressSystem CreateCustomerAddressSystem(RequestInfo requestInfo)
		{
			return new CustomerAddressSystem(GetConfig(requestInfo));
		}

		public IContactSystem CreateContactSystem(RequestInfo requestInfo)
		{
			return new ContactSystem(GetConfig(requestInfo));
		}

		public IVendorClassSystem CreateVendorClassSystem(RequestInfo requestInfo)
		{
			return new VendorClassSystem(GetConfig(requestInfo));
		}

		public IVendorSystem CreateVendorSystem(RequestInfo requestInfo)
		{
			return new VendorSystem(GetConfig(requestInfo));
		}

		public IVendorAddressSystem CreateVendorAddressSystem(RequestInfo requestInfo)
		{
			return new VendorAddressSystem(GetConfig(requestInfo));
		}

		public IBuyerSystem CreateBuyerSystem(RequestInfo requestInfo)
		{
			return new BuyerSystem(GetConfig(requestInfo));
		}

		public IProductClassSystem CreateProductClassSystem(RequestInfo requestInfo)
		{
			return new ProductClassSystem(GetConfig(requestInfo));
		}

		public IGradeSystem CreateGradeSystem(RequestInfo requestInfo)
		{
			return new GradeSystem(GetConfig(requestInfo));
		}

		public ISponsorSystem CreateSponsorSystem(RequestInfo requestInfo)
		{
			return new SponsorSystem(GetConfig(requestInfo));
		}

		public IProvisionTypeSystem CreateProvisionTypeSystem(RequestInfo requestInfo)
		{
			return new ProvisionTypeSystem(GetConfig(requestInfo));
		}

		public INationalitySystem CreateNationalitySystem(RequestInfo requestInfo)
		{
			return new NationalitySystem(GetConfig(requestInfo));
		}

		public IReligionSystem CreateReligionSystem(RequestInfo requestInfo)
		{
			return new ReligionSystem(GetConfig(requestInfo));
		}

		public IDivisionSystem CreateDivisionSystem(RequestInfo requestInfo)
		{
			return new DivisionSystem(GetConfig(requestInfo));
		}

		public ICompanyDivisionSystem CreateCompanyDivisionSystem(RequestInfo requestInfo)
		{
			return new CompanyDivisionSystem(GetConfig(requestInfo));
		}

		public IPositionSystem CreatePositionSystem(RequestInfo requestInfo)
		{
			return new PositionSystem(GetConfig(requestInfo));
		}

		public IEmployeeAppraisalSystem CreateEmployeeAppraisalSystem(RequestInfo requestInfo)
		{
			return new EmployeeAppraisalSystem(GetConfig(requestInfo));
		}

		public IEmployeePerformanceSystem CreateEmployeePerfrmanceSystem(RequestInfo requestInfo)
		{
			return new EmployeePerformanceSystem(GetConfig(requestInfo));
		}

		public IEmployeeDocTypeSystem CreateEmployeeDocTypeSystem(RequestInfo requestInfo)
		{
			return new EmployeeDocTypeSystem(GetConfig(requestInfo));
		}

		public IVehicleDocTypeSystem CreateVehicleDocTypeSystem(RequestInfo requestInfo)
		{
			return new VehicleDocTypeSystem(GetConfig(requestInfo));
		}

		public IDegreeSystem CreateDegreeSystem(RequestInfo requestInfo)
		{
			return new DegreeSystem(GetConfig(requestInfo));
		}

		public ISkillSystem CreateSkillSystem(RequestInfo requestInfo)
		{
			return new SkillSystem(GetConfig(requestInfo));
		}

		public ICustomerGroupSystem CreateCustomerGroupSystem(RequestInfo requestInfo)
		{
			return new CustomerGroupSystem(GetConfig(requestInfo));
		}

		public IVendorGroupSystem CreateVendorGroupSystem(RequestInfo requestInfo)
		{
			return new VendorGroupSystem(GetConfig(requestInfo));
		}

		public IEmployeeGroupSystem CreateEmployeeGroupSystem(RequestInfo requestInfo)
		{
			return new EmployeeGroupSystem(GetConfig(requestInfo));
		}

		public IEmployeeAddressSystem CreateEmployeeAddressSystem(RequestInfo requestInfo)
		{
			return new EmployeeAddressSystem(GetConfig(requestInfo));
		}

		public ILocationSystem CreateLocationSystem(RequestInfo requestInfo)
		{
			return new LocationSystem(GetConfig(requestInfo));
		}

		public IWorkLocationSystem CreateWorkLocationSystem(RequestInfo requestInfo)
		{
			return new WorkLocationSystem(GetConfig(requestInfo));
		}

		public IEmployeeDependentSystem CreateEmployeeDependentSystem(RequestInfo requestInfo)
		{
			return new EmployeeDependentSystem(GetConfig(requestInfo));
		}

		public IEmployeeDocumentSystem CreateEmployeeDocumentSystem(RequestInfo requestInfo)
		{
			return new EmployeeDocumentSystem(GetConfig(requestInfo));
		}

		public IVehicleDocumentSystem CreateVehicleDocumentSystem(RequestInfo requestInfo)
		{
			return new VehicleDocumentSystem(GetConfig(requestInfo));
		}

		public IEmployeeSkillSystem CreateEmployeeSkillSystem(RequestInfo requestInfo)
		{
			return new EmployeeSkillSystem(GetConfig(requestInfo));
		}

		public ILeaveTypeSystem CreateLeaveTypeSystem(RequestInfo requestInfo)
		{
			return new LeaveTypeSystem(GetConfig(requestInfo));
		}

		public IJobTypeSystem CreateJobTypeSystem(RequestInfo requestInfo)
		{
			return new JobTypeSystem(GetConfig(requestInfo));
		}

		public IServiceActivitySystem CreateServiceActivitySystem(RequestInfo requestInfo)
		{
			return new ServiceActivitySystem(GetConfig(requestInfo));
		}

		public ICostCategorySystem CreateCostCategorySystem(RequestInfo requestInfo)
		{
			return new CostCategorySystem(GetConfig(requestInfo));
		}

		public IEquipmentSystem CreateEquipmentSystem(RequestInfo requestInfo)
		{
			return new EquipmentSystem(GetConfig(requestInfo));
		}

		public IPayrollItemSystem CreatePayrollItemSystem(RequestInfo requestInfo)
		{
			return new PayrollItemSystem(GetConfig(requestInfo));
		}

		public IDeductionSystem CreateDeductionSystem(RequestInfo requestInfo)
		{
			return new DeductionSystem(GetConfig(requestInfo));
		}

		public IBenefitSystem CreateBenefitSystem(RequestInfo requestInfo)
		{
			return new BenefitSystem(GetConfig(requestInfo));
		}

		public IEmployeePayrollItemDetailSystem CreateEmployeePayrollItemDetailSystem(RequestInfo requestInfo)
		{
			return new EmployeePayrollItemDetailSystem(GetConfig(requestInfo));
		}

		public IEmployeeDeductionDetailSystem CreateEmployeeDeductionDetailSystem(RequestInfo requestInfo)
		{
			return new EmployeeDeductionDetailSystem(GetConfig(requestInfo));
		}

		public IEmployeeBenefitDetailSystem CreateEmployeeBenefitDetailSystem(RequestInfo requestInfo)
		{
			return new EmployeeBenefitDetailSystem(GetConfig(requestInfo));
		}

		public IDestinationSystem CreateDestinationSystem(RequestInfo requestInfo)
		{
			return new DestinationSystem(GetConfig(requestInfo));
		}

		public IEmployeeLeaveDetailSystem CreateEmployeeLeaveDetailSystem(RequestInfo requestInfo)
		{
			return new EmployeeLeaveDetailSystem(GetConfig(requestInfo));
		}

		public IEmployeeLeaveProcessSystem CreateEmployeeLeaveProcessSystem(RequestInfo requestInfo)
		{
			return new EmployeeLeaveProcessSystem(GetConfig(requestInfo));
		}

		public ICompanyDocTypeSystem CreateCompanyDocTypeSystem(RequestInfo requestInfo)
		{
			return new CompanyDocTypeSystem(GetConfig(requestInfo));
		}

		public ICompanyDocumentSystem CreateCompanyDocumentSystem(RequestInfo requestInfo)
		{
			return new CompanyDocumentSystem(GetConfig(requestInfo));
		}

		public ITenancyContractSystem CreateTenancyContractSystem(RequestInfo requestInfo)
		{
			return new TenancyContractSystem(GetConfig(requestInfo));
		}

		public ITradeLicenseSystem CreateTradeLicenseSystem(RequestInfo requestInfo)
		{
			return new TradeLicenseSystem(GetConfig(requestInfo));
		}

		public IVisaSystem CreateVisaSystem(RequestInfo requestInfo)
		{
			return new VisaSystem(GetConfig(requestInfo));
		}

		public IJournalSystem CreateJournalSystem(RequestInfo requestInfo)
		{
			return new JournalSystem(GetConfig(requestInfo));
		}

		public ISystemDocumentSystem CreateSystemDocumentSystem(RequestInfo requestInfo)
		{
			return new SystemDocumentSystem(GetConfig(requestInfo));
		}

		public ICostCenterSystem CreateCostCenterSystem(RequestInfo requestInfo)
		{
			return new CostCenterSystem(GetConfig(requestInfo));
		}

		public IRegisterSystem CreateRegisterSystem(RequestInfo requestInfo)
		{
			return new RegisterSystem(GetConfig(requestInfo));
		}

		public IChequebookSystem CreateChequebookSystem(RequestInfo requestInfo)
		{
			return new ChequebookSystem(GetConfig(requestInfo));
		}

		public IIssuedChequeSystem CreateIssuedChequeSystem(RequestInfo requestInfo)
		{
			return new IssuedChequeSystem(GetConfig(requestInfo));
		}

		public IReceivedChequeSystem CreateReceivedChequeSystem(RequestInfo requestInfo)
		{
			return new ReceivedChequeSystem(GetConfig(requestInfo));
		}

		public IReturnedChequeReasonSystem CreateReturnedChequeReasonSystem(RequestInfo requestInfo)
		{
			return new ReturnedChequeReasonSystem(GetConfig(requestInfo));
		}

		public IAdjustmentTypeSystem CreateAdjustmentTypeSystem(RequestInfo requestInfo)
		{
			return new AdjustmentTypeSystem(GetConfig(requestInfo));
		}

		public IInventoryAdjustmentSystem CreateInventoryAdjustmentSystem(RequestInfo requestInfo)
		{
			return new InventoryAdjustmentSystem(GetConfig(requestInfo));
		}

		public IInventoryDamageSystem CreateInventoryDamageSystem(RequestInfo requestInfo)
		{
			return new InventoryDamageSystem(GetConfig(requestInfo));
		}

		public IInventoryTransferSystem CreateInventoryTransferSystem(RequestInfo requestInfo)
		{
			return new InventoryTransferSystem(GetConfig(requestInfo));
		}

		public IDriverSystem CreateDriverSystem(RequestInfo requestInfo)
		{
			return new DriverSystem(GetConfig(requestInfo));
		}

		public ISalesQuoteSystem CreateSalesQuoteSystem(RequestInfo requestInfo)
		{
			return new SalesQuoteSystem(GetConfig(requestInfo));
		}

		public ISalesOrderSystem CreateSalesOrderSystem(RequestInfo requestInfo)
		{
			return new SalesOrderSystem(GetConfig(requestInfo));
		}

		public ISalesEnquirySystem CreateSalesEnquirySystem(RequestInfo requestInfo)
		{
			return new SalesEnquirySystem(GetConfig(requestInfo));
		}

		public ISalesProformaSystem CreateSalesProformaSystem(RequestInfo requestInfo)
		{
			return new SalesProformaSystem(GetConfig(requestInfo));
		}

		public IPriceListSystem CreatePriceListSystem(RequestInfo requestInfo)
		{
			return new PriceListSystem(GetConfig(requestInfo));
		}

		public IDeliveryNoteSystem CreateDeliveryNoteSystem(RequestInfo requestInfo)
		{
			return new DeliveryNoteSystem(GetConfig(requestInfo));
		}

		public IExportPickListSystem CreateExportPickListSystem(RequestInfo requestInfo)
		{
			return new ExportPickListSystem(GetConfig(requestInfo));
		}

		public ISalesInvoiceSystem CreateSalesInvoiceSystem(RequestInfo requestInfo)
		{
			return new SalesInvoiceSystem(GetConfig(requestInfo));
		}

		public ISalesInvoiceNISystem CreateSalesInvoiceNISystem(RequestInfo requestInfo)
		{
			return new SalesInvoiceNISystem(GetConfig(requestInfo));
		}

		public ISalesReceiptSystem CreateSalesReceiptSystem(RequestInfo requestInfo)
		{
			return new SalesReceiptSystem(GetConfig(requestInfo));
		}

		public ISalesReturnSystem CreateSalesReturnSystem(RequestInfo requestInfo)
		{
			return new SalesReturnSystem(GetConfig(requestInfo));
		}

		public ILPOReceiptSystem CreateLPOReceiptSystem(RequestInfo requestInfo)
		{
			return new LPOReceiptSystem(GetConfig(requestInfo));
		}

		public IJobInvoiceSystem CreateJobInvoiceSystem(RequestInfo requestInfo)
		{
			return new JobInvoiceSystem(GetConfig(requestInfo));
		}

		public IDeliveryReturnSystem CreateDeliveryReturnSystem(RequestInfo requestInfo)
		{
			return new DeliveryReturnSystem(GetConfig(requestInfo));
		}

		public IPurchaseQuoteSystem CreatePurchaseQuoteSystem(RequestInfo requestInfo)
		{
			return new PurchaseQuoteSystem(GetConfig(requestInfo));
		}

		public IPOShipmentSystem CreatePOShipmentSystem(RequestInfo requestInfo)
		{
			return new POShipmentSystem(GetConfig(requestInfo));
		}

		public IPortSystem CreatePortSystem(RequestInfo requestInfo)
		{
			return new PortSystem(GetConfig(requestInfo));
		}

		public IPurchaseInvoiceSystem CreatePurchaseInvoiceSystem(RequestInfo requestInfo)
		{
			return new PurchaseInvoiceSystem(GetConfig(requestInfo));
		}

		public IPurchaseInvoiceNISystem CreatePurchaseInvoiceNISystem(RequestInfo requestInfo)
		{
			return new PurchaseInvoiceNISystem(GetConfig(requestInfo));
		}

		public IPurchaseReceiptSystem CreatePurchaseReceiptSystem(RequestInfo requestInfo)
		{
			return new PurchaseReceiptSystem(GetConfig(requestInfo));
		}

		public IPurchaseReturnSystem CreatePurchaseReturnSystem(RequestInfo requestInfo)
		{
			return new PurchaseReturnSystem(GetConfig(requestInfo));
		}

		public IShortcutSystem CreateShortcutSystem(RequestInfo requestInfo)
		{
			return new ShortcutSystem(GetConfig(requestInfo));
		}

		public IEmployeeActivitySystem CreateEmployeeActivitySystem(RequestInfo requestInfo)
		{
			return new EmployeeActivitySystem(GetConfig(requestInfo));
		}

		public IDisciplineActionTypeSystem CreateDisciplineActionTypeSystem(RequestInfo requestInfo)
		{
			return new DisciplineActionTypeSystem(GetConfig(requestInfo));
		}

		public IEmployeeActivityTypeSystem CreateEmployeeActivityTypeSystem(RequestInfo requestInfo)
		{
			return new EmployeeActivityTypeSystem(GetConfig(requestInfo));
		}

		public ISalarySheetSystem CreateSalarySheetSystem(RequestInfo requestInfo)
		{
			return new SalarySheetSystem(GetConfig(requestInfo));
		}

		public IEmployeeTypeSystem CreateEmployeeTypeSystem(RequestInfo requestInfo)
		{
			return new EmployeeTypeSystem(GetConfig(requestInfo));
		}

		public IPayrollTransactionSystem CreatePayrollTransactionSystem(RequestInfo requestInfo)
		{
			return new PayrollTransactionSystem(GetConfig(requestInfo));
		}

		public IProjectExpenseAllocationSystem CreateProjectExpenseAllocationSystem(RequestInfo requestInfo)
		{
			return new ProjectExpenseAllocationSystem(GetConfig(requestInfo));
		}

		public IEmployeeLoanSystem CreateEmployeeLoanSystem(RequestInfo requestInfo)
		{
			return new EmployeeLoanSystem(GetConfig(requestInfo));
		}

		public IEmployeeLoanTypeSystem CreateEmployeeLoanTypeSystem(RequestInfo requestInfo)
		{
			return new EmployeeLoanTypeSystem(GetConfig(requestInfo));
		}

		public IARJournalSystem CreateARJournalSystem(RequestInfo requestInfo)
		{
			return new ARJournalSystem(GetConfig(requestInfo));
		}

		public IAPJournalSystem CreateAPJournalSystem(RequestInfo requestInfo)
		{
			return new APJournalSystem(GetConfig(requestInfo));
		}

		public IUserSystem CreateUserSystem(RequestInfo requestInfo)
		{
			return new UserSystem(GetConfig(requestInfo));
		}

		public IUserGroupSystem CreateUserGroupSystem(RequestInfo requestInfo)
		{
			return new UserGroupSystem(GetConfig(requestInfo));
		}

		public IReminderSystem CreateReminderSystem(RequestInfo requestInfo)
		{
			return new ReminderSystem(GetConfig(requestInfo));
		}

		public ICompanyAddressSystem CreateCompanyAddressSystem(RequestInfo requestInfo)
		{
			return new CompanyAddressSystem(GetConfig(requestInfo));
		}

		public ISmartListSystem CreateSmartListSystem(RequestInfo requestInfo)
		{
			return new SmartListSystem(GetConfig(requestInfo));
		}

		public IExternalReportSystem CreateExternalReportSystem(RequestInfo requestInfo)
		{
			return new ExternalReportSystem(GetConfig(requestInfo));
		}

		public IItemTransactionSystem CreateItemTransactionSystem(RequestInfo requestInfo)
		{
			return new ItemTransactionSystem(GetConfig(requestInfo));
		}

		public ISalesPOSSystem CreateSalesPOSSystem(RequestInfo requestInfo)
		{
			return new SalesPOSSystem(GetConfig(requestInfo));
		}

		public IPOSBatchSystem CreatePOSBatchSystem(RequestInfo requestInfo)
		{
			return new POSBatchSystem(GetConfig(requestInfo));
		}

		public IPOSShiftSystem CreatePOSShiftSystem(RequestInfo requestInfo)
		{
			return new POSShiftSystem(GetConfig(requestInfo));
		}

		public IPOSCashRegisterSystem CreatePOSCashRegisterSystem(RequestInfo requestInfo)
		{
			return new POSCashRegisterSystem(GetConfig(requestInfo));
		}

		public IPOSHoldSystem CreatePOSHoldSystem(RequestInfo requestInfo)
		{
			return new POSHoldSystem(GetConfig(requestInfo));
		}

		public IConsignOutSystem CreateConsignOutSystem(RequestInfo requestInfo)
		{
			return new ConsignOutSystem(GetConfig(requestInfo));
		}

		public IConsignOutSettlementSystem CreateConsignOutSettlementSystem(RequestInfo requestInfo)
		{
			return new ConsignOutSettlementSystem(GetConfig(requestInfo));
		}

		public IExpenseCodeSystem CreateExpenseCodeSystem(RequestInfo requestInfo)
		{
			return new ExpenseCodeSystem(GetConfig(requestInfo));
		}

		public ICustomerCategorySystem CreateCustomerCategorySystem(RequestInfo requestInfo)
		{
			return new CustomerCategorySystem(GetConfig(requestInfo));
		}

		public IEntityCategorySystem CreateEntityCategorySystem(RequestInfo requestInfo)
		{
			return new EntityCategorySystem(GetConfig(requestInfo));
		}

		public ICompanyOptionSystem CreateCompanyOptionSystem(RequestInfo requestInfo)
		{
			return new CompanyOptionSystem(GetConfig(requestInfo));
		}

		public IConsignOutReturnSystem CreateConsignOutReturnSystem(RequestInfo requestInfo)
		{
			return new ConsignOutReturnSystem(GetConfig(requestInfo));
		}

		public IConsignInReturnSystem CreateConsignInReturnSystem(RequestInfo requestInfo)
		{
			return new ConsignInReturnSystem(GetConfig(requestInfo));
		}

		public IConsignInSystem CreateConsignInSystem(RequestInfo requestInfo)
		{
			return new ConsignInSystem(GetConfig(requestInfo));
		}

		public IConsignInSettlementSystem CreateConsignInSettlementSystem(RequestInfo requestInfo)
		{
			return new ConsignInSettlementSystem(GetConfig(requestInfo));
		}

		public IFixedAssetSystem CreateFixedAssetSystem(RequestInfo requestInfo)
		{
			return new FixedAssetSystem(GetConfig(requestInfo));
		}

		public IFixedAssetGroupSystem CreateFixedAssetGroupSystem(RequestInfo requestInfo)
		{
			return new FixedAssetGroupSystem(GetConfig(requestInfo));
		}

		public IFixedAssetLocationSystem CreateFixedAssetLocationSystem(RequestInfo requestInfo)
		{
			return new FixedAssetLocationSystem(GetConfig(requestInfo));
		}

		public IFixedAssetClassSystem CreateFixedAssetClassSystem(RequestInfo requestInfo)
		{
			return new FixedAssetClassSystem(GetConfig(requestInfo));
		}

		public IFixedAssetPurchaseSystem CreateFixedAssetPurchaseSystem(RequestInfo requestInfo)
		{
			return new FixedAssetPurchaseSystem(GetConfig(requestInfo));
		}

		public IFixedAssetPurchaseOrderSystem CreateFixedAssetPurchaseOrderSystem(RequestInfo requestInfo)
		{
			return new FixedAssetPurchaseOrderSystem(GetConfig(requestInfo));
		}

		public IFixedAssetTransferSystem CreateFixedAssetTransferSystem(RequestInfo requestInfo)
		{
			return new FixedAssetTransferSystem(GetConfig(requestInfo));
		}

		public IFixedAssetSaleSystem CreateFixedAssetSaleSystem(RequestInfo requestInfo)
		{
			return new FixedAssetSaleSystem(GetConfig(requestInfo));
		}

		public IFixedAssetDepSystem CreateFixedAssetDepSystem(RequestInfo requestInfo)
		{
			return new FixedAssetDepSystem(GetConfig(requestInfo));
		}

		public IDimensionSystem CreateDimensionSystem(RequestInfo requestInfo)
		{
			return new DimensionSystem(GetConfig(requestInfo));
		}

		public IProductParentSystem CreateProductParentSystem(RequestInfo requestInfo)
		{
			return new ProductParentSystem(GetConfig(requestInfo));
		}

		public IMatrixTemplateSystem CreateMatrixTemplateSystem(RequestInfo requestInfo)
		{
			return new MatrixTemplateSystem(GetConfig(requestInfo));
		}

		public IFiscalYearSystem CreateFiscalYearSystem(RequestInfo requestInfo)
		{
			return new FiscalYearSystem(GetConfig(requestInfo));
		}

		public ILeadSystem CreateLeadSystem(RequestInfo requestInfo)
		{
			return new LeadSystem(GetConfig(requestInfo));
		}

		public ILeadAddressSystem CreateLeadAddressSystem(RequestInfo requestInfo)
		{
			return new LeadAddressSystem(GetConfig(requestInfo));
		}

		public IGenericListSystem CreateGenericListSystem(RequestInfo requestInfo)
		{
			return new GenericListSystem(GetConfig(requestInfo));
		}

		public IUDFSystem CreateUDFSystem(RequestInfo requestInfo)
		{
			return new UDFSystem(GetConfig(requestInfo));
		}

		public IPivotGroupSystem CreatePivotGroupSystem(RequestInfo requestInfo)
		{
			return new PivotGroupSystem(GetConfig(requestInfo));
		}

		public IPivotSystem CreatePivotSystem(RequestInfo requestInfo)
		{
			return new PivotSystem(GetConfig(requestInfo));
		}

		public IAssemblyBuildSystem CreateAssemblyBuildSystem(RequestInfo requestInfo)
		{
			return new AssemblyBuildSystem(GetConfig(requestInfo));
		}

		public IProductionSystem CreateProductionSystem(RequestInfo requestInfo)
		{
			return new ProductionSystem(GetConfig(requestInfo));
		}

		public IInventoryRepackingSystem CreateInventoryRepackingSystem(RequestInfo requestInfo)
		{
			return new InventoryRepackingSystem(GetConfig(requestInfo));
		}

		public IWorkOrderSystem CreateWorkOrderSystem(RequestInfo requestInfo)
		{
			return new WorkOrderSystem(GetConfig(requestInfo));
		}

		public IBOMSystem CreateBOMSystem(RequestInfo requestInfo)
		{
			return new BOMSystem(GetConfig(requestInfo));
		}

		public IEOSRuleSystem CreateEOSRuleSystem(RequestInfo requestInfo)
		{
			return new EOSRuleSystem(GetConfig(requestInfo));
		}

		public IOverTimeSystem CreateOverTimeSystem(RequestInfo requestInfo)
		{
			return new OverTimeSystem(GetConfig(requestInfo));
		}

		public ISalarySystem CreateSalarySystem(RequestInfo requestInfo)
		{
			return new SalarySystem(GetConfig(requestInfo));
		}

		public IEntityDocSystem CreateEntityDocSystem(RequestInfo requestInfo)
		{
			return new EntityDocSystem(GetConfig(requestInfo));
		}

		public IJobSystem CreateJobSystem(RequestInfo requestInfo)
		{
			return new JobSystem(GetConfig(requestInfo));
		}

		public IOpportunitySystem CreateOpportunitySystem(RequestInfo requestInfo)
		{
			return new OpportunitySystem(GetConfig(requestInfo));
		}

		public ICompetitorSystem CreateCompetitorSystem(RequestInfo requestInfo)
		{
			return new CompetitorSystem(GetConfig(requestInfo));
		}

		public IActivitySystem CreateActivitySystem(RequestInfo requestInfo)
		{
			return new ActivitySystem(GetConfig(requestInfo));
		}

		public IBankReconciliationSystem CreateBankReconciliationSystem(RequestInfo requestInfo)
		{
			return new BankReconciliationSystem(GetConfig(requestInfo));
		}

		public ICustomReportSystem CreateCustomReportSystem(RequestInfo requestInfo)
		{
			return new CustomReportSystem(GetConfig(requestInfo));
		}

		public ICampaignSystem CreateCampaignSystem(RequestInfo requestInfo)
		{
			return new CampaignSystem(GetConfig(requestInfo));
		}

		public IEventSystem CreateEventSystem(RequestInfo requestInfo)
		{
			return new EventSystem(GetConfig(requestInfo));
		}

		public IJobInventoryIssueSystem CreateJobInventoryIssueSystem(RequestInfo requestInfo)
		{
			return new JobInventoryIssueSystem(GetConfig(requestInfo));
		}

		public IJobInventoryReturnSystem CreateJobInventoryReturnSystem(RequestInfo requestInfo)
		{
			return new JobInventoryReturnSystem(GetConfig(requestInfo));
		}

		public IJobExpenseIssueSystem CreateJobExpenseIssueSystem(RequestInfo requestInfo)
		{
			return new JobExpenseIssueSystem(GetConfig(requestInfo));
		}

		public IJobTimesheetSystem CreateJobTimesheetSystem(RequestInfo requestInfo)
		{
			return new JobTimesheetSystem(GetConfig(requestInfo));
		}

		public IOverTimeEntrySystem CreateOverTimeEntrySystem(RequestInfo requestInfo)
		{
			return new OverTimeEntrySystem(GetConfig(requestInfo));
		}

		public IJobMaterialRequisitionSystem CreateJobMaterialRequisitionSystem(RequestInfo requestInfo)
		{
			return new JobMaterialRequisitionSystem(GetConfig(requestInfo));
		}

		public IJobMaterialEstimateSystem CreateJobMaterialEstimateSystem(RequestInfo requestInfo)
		{
			return new JobMaterialEstimateSystem(GetConfig(requestInfo));
		}

		public IJobManHrsBudgetingSystem CreateJobManHrsBudgetingSystem(RequestInfo requestInfo)
		{
			return new JobManHrsBudgetingSystem(GetConfig(requestInfo));
		}

		public IJobMaintenanceScheduleSystem CreateJobMaintenanceScheduleSystem(RequestInfo requestInfo)
		{
			return new JobMaintenanceScheduleSystem(GetConfig(requestInfo));
		}

		public IJobMaintenanceServiceSystem CreateJobMaintenanceServiceSystem(RequestInfo requestInfo)
		{
			return new JobMaintenanceServiceSystem(GetConfig(requestInfo));
		}

		public IServiceCallTrackSystem CreateServiceCallTrackSystem(RequestInfo requestInfo)
		{
			return new ServiceCallTrackSystem(GetConfig(requestInfo));
		}

		public IBankFacilityGroupSystem CreateBankFacilityGroupSystem(RequestInfo requestInfo)
		{
			return new BankFacilityGroupSystem(GetConfig(requestInfo));
		}

		public IBankFacilitySystem CreateBankFacilitySystem(RequestInfo requestInfo)
		{
			return new BankFacilitySystem(GetConfig(requestInfo));
		}

		public IOpeningBalanceBatchSystem CreateOpeningBalanceBatchSystem(RequestInfo requestInfo)
		{
			return new OpeningBalanceBatchSystem(GetConfig(requestInfo));
		}

		public IOpeningBalanceLeaveSystem CreateOpeningBalanceLeaveSystem(RequestInfo requestInfo)
		{
			return new OpeningBalanceLeaveSystem(GetConfig(requestInfo));
		}

		public ICitySystem CreateCitySystem(RequestInfo requestInfo)
		{
			return new CitySystem(GetConfig(requestInfo));
		}

		public IVehicleSystem CreateVehicleSystem(RequestInfo requestInfo)
		{
			return new VehicleSystem(GetConfig(requestInfo));
		}

		public IBankFacilityTransactionSystem CreateBankFacilityTransactionSystem(RequestInfo requestInfo)
		{
			return new BankFacilityTransactionSystem(GetConfig(requestInfo));
		}

		public IBankFacilityPaymentSystem CreateBankFacilityPaymentSystem(RequestInfo requestInfo)
		{
			return new BankFacilityPaymentSystem(GetConfig(requestInfo));
		}

		public ICustomGadgetSystem CreateCustomGadgetSystem(RequestInfo requestInfo)
		{
			return new CustomGadgetSystem(GetConfig(requestInfo));
		}

		public ISendChequeSystem CreateSendChequeSystem(RequestInfo requestInfo)
		{
			return new SendChequeSystem(GetConfig(requestInfo));
		}

		public IDiscountChequeSystem CreateDiscountChequeSystem(RequestInfo requestInfo)
		{
			return new DiscountChequeSystem(GetConfig(requestInfo));
		}

		public IInventoryTransferTypeSystem CreateInventoryTransferTypeSystem(RequestInfo requestInfo)
		{
			return new InventoryTransferTypeSystem(GetConfig(requestInfo));
		}

		public IExportPackingListSystem CreateExportPackingListSystem(RequestInfo requestInfo)
		{
			return new ExportPackingListSystem(GetConfig(requestInfo));
		}

		public IStandingJournalSystem CreateStandingJournalSystem(RequestInfo requestInfo)
		{
			return new StandingJournalSystem(GetConfig(requestInfo));
		}

		public IDashboardSystem CreateDashboardSystem(RequestInfo requestInfo)
		{
			return new DashboardSystem(GetConfig(requestInfo));
		}

		public IWebDashboardSystem CreateWebDashboardSystem(RequestInfo requestInfo)
		{
			return new WebDashboardSystem(GetConfig(requestInfo));
		}

		public ICollateralSystem CreateCollateralSystem(RequestInfo requestInfo)
		{
			return new CollateralSystem(GetConfig(requestInfo));
		}

		public IJobTaskSystem CreateJobTaskSystem(RequestInfo requestInfo)
		{
			return new JobTaskSystem(GetConfig(requestInfo));
		}

		public IClientAssetSystem CreateClientAssetSystem(RequestInfo requestInfo)
		{
			return new ClientAssetSystem(GetConfig(requestInfo));
		}

		public IBinSystem CreateBinSystem(RequestInfo requestInfo)
		{
			return new BinSystem(GetConfig(requestInfo));
		}

		public IRouteSystem CreateRouteSystem(RequestInfo requestInfo)
		{
			return new RouteSystem(GetConfig(requestInfo));
		}

		public IRouteGroupSystem CreateRouteGroupSystem(RequestInfo requestInfo)
		{
			return new RouteGroupSystem(GetConfig(requestInfo));
		}

		public IRackSystem CreateRackSystem(RequestInfo requestInfo)
		{
			return new RackSystem(GetConfig(requestInfo));
		}

		public IGRNReturnSystem CreateGRNReturnSystem(RequestInfo requestInfo)
		{
			return new GRNReturnSystem(GetConfig(requestInfo));
		}

		public IArrivalReportSystem CreateArrivalReportSystem(RequestInfo requestInfo)
		{
			return new ArrivalReportSystem(GetConfig(requestInfo));
		}

		public IQualityClaimSystem CreateQualityClaimSystem(RequestInfo requestInfo)
		{
			return new QualityClaimSystem(GetConfig(requestInfo));
		}

		public IQualityTaskSystem CreateQualityTaskSystem(RequestInfo requestInfo)
		{
			return new QualityTaskSystem(GetConfig(requestInfo));
		}

		public ISurveyorSystem CreateSurveyorSystem(RequestInfo requestInfo)
		{
			return new SurveyorSystem(GetConfig(requestInfo));
		}

		public IArrivalReportTemplateSystem CreateArrivalReportTemplateSystem(RequestInfo requestInfo)
		{
			return new ArrivalReportTemplateSystem(GetConfig(requestInfo));
		}

		public IApprovalSystem CreateApprovalSystem(RequestInfo requestInfo)
		{
			return new ApprovalSystem(GetConfig(requestInfo));
		}

		public IPropertyAgentSystem CreatePropertyAgentSystem(RequestInfo requestInfo)
		{
			return new PropertyAgentSystem(GetConfig(requestInfo));
		}

		public IPropertyClassSystem CreatePropertyClassSystem(RequestInfo requestInfo)
		{
			return new PropertyClassSystem(GetConfig(requestInfo));
		}

		public IPropertySystem CreatePropertySystem(RequestInfo requestInfo)
		{
			return new PropertySystem(GetConfig(requestInfo));
		}

		public IPropertyUnitSystem CreatePropertyUnitSystem(RequestInfo requestInfo)
		{
			return new PropertyUnitSystem(GetConfig(requestInfo));
		}

		public IPropertyVirtualUnitSystem CreatePropertyVirtualUnitSystem(RequestInfo requestInfo)
		{
			return new PropertyVirtualUnitSystem(GetConfig(requestInfo));
		}

		public IPropertyIncomeCodeSystem CreatePropertyIncomeCodeSystem(RequestInfo requestInfo)
		{
			return new PropertyIncomeCodeSystem(GetConfig(requestInfo));
		}

		public IPropertyCategorySystem CreatePropertyCategorySystem(RequestInfo requestInfo)
		{
			return new PropertyCategorySystem(GetConfig(requestInfo));
		}

		public ICheckListSystem CreateCheckListSystem(RequestInfo requestInfo)
		{
			return new CheckListSystem(GetConfig(requestInfo));
		}

		public IPaymentRequestSystem CreatePaymentRequestSystem(RequestInfo requestInfo)
		{
			return new PaymentRequestSystem(GetConfig(requestInfo));
		}

		public IPropertyRentSystem CreatePropertyRentSystem(RequestInfo requestInfo)
		{
			return new PropertyRentSystem(GetConfig(requestInfo));
		}

		public IPropertyCancelSystem CreatePropertyCancelSystem(RequestInfo requestInfo)
		{
			return new PropertyCancelSystem(GetConfig(requestInfo));
		}

		public IRentalPostingSystem CreateRentalPostingSystem(RequestInfo requestInfo)
		{
			return new RentalPostingSystem(GetConfig(requestInfo));
		}

		public IW3PLGRNSystem CreateW3PLGRNSystem(RequestInfo requestInfo)
		{
			return new W3PLGRNSystem(GetConfig(requestInfo));
		}

		public IW3PLDeliverySystem CreateW3PLDeliverySystem(RequestInfo requestInfo)
		{
			return new W3PLDeliverySystem(GetConfig(requestInfo));
		}

		public IW3PLInvoiceSystem CreateW3PLInvoiceSystem(RequestInfo requestInfo)
		{
			return new W3PLInvoiceSystem(GetConfig(requestInfo));
		}

		public IPurchaseClaimSystem CreatePurchaseClaimSystem(RequestInfo requestInfo)
		{
			return new PurchaseClaimSystem(GetConfig(requestInfo));
		}

		public IPropertyServiceSystem CreatePropertyServiceSystem(RequestInfo requestInfo)
		{
			return new PropertyServiceSystem(GetConfig(requestInfo));
		}

		public IJobBOMSystem CreateJobBOMSystem(RequestInfo requestInfo)
		{
			return new JobBOMSystem(GetConfig(requestInfo));
		}

		public IJobEstimationSystem CreateJobEstimationSystem(RequestInfo requestInfo)
		{
			return new JobEstimationSystem(GetConfig(requestInfo));
		}

		public IGarmentRentalSystem CreateGarmentRentalSystem(RequestInfo requestInfo)
		{
			return new GarmentRentalSystem(GetConfig(requestInfo));
		}

		public IGarmentRentalReturnSystem CreateGarmentRentalReturnSystem(RequestInfo requestInfo)
		{
			return new GarmentRentalReturnSystem(GetConfig(requestInfo));
		}

		public IEmailMessageSystem CreateEmailMessageSystem(RequestInfo requestInfo)
		{
			return new EmailMessageSystem(GetConfig(requestInfo));
		}

		public ICLVoucherSystem CreateCLVoucherSystem(RequestInfo requestInfo)
		{
			return new CLVoucherSystem(GetConfig(requestInfo));
		}

		public ICLTokenSystem CreateCLTokenSystem(RequestInfo requestInfo)
		{
			return new CLTokenSystem(GetConfig(requestInfo));
		}

		public IEmployeeEOSSettlementSystem CreateEmployeeEOSSettlementSystem(RequestInfo requestInfo)
		{
			return new EmployeeEOSSettlementSystem(GetConfig(requestInfo));
		}

		public IFixedAssetBulkPurchaseSystem CreateFixedAssetBulkPurchaseSystem(RequestInfo requestInfo)
		{
			return new FixedAssetBulkPurchaseSystem(GetConfig(requestInfo));
		}

		public IServiceTypeSystem CreateServiceTypeSystem(RequestInfo requestInfo)
		{
			return new ServiceTypeSystem(GetConfig(requestInfo));
		}

		public IEntityCommentSystem CreateEntityCommentSystem(RequestInfo requestInfo)
		{
			return new EntityCommentSystem(GetConfig(requestInfo));
		}

		public IContainerTrackingSystem CreateContainerTrackingSystem(RequestInfo requestInfo)
		{
			return new ContainerTrackingSystem(GetConfig(requestInfo));
		}

		public IInsuranceProviderSystem CreateInsuranceProviderSystem(RequestInfo requestInfo)
		{
			return new InsuranceProviderSystem(GetConfig(requestInfo));
		}

		public ICustomerInsuranceSystem CreateCustomerInsuranceSystem(RequestInfo requestInfo)
		{
			return new CustomerInsuranceSystem(GetConfig(requestInfo));
		}

		public IRiderSummarySystem CreateRiderSummarySystem(RequestInfo requestInfo)
		{
			return new RiderSummarySystem(GetConfig(requestInfo));
		}

		public IHorseSummarySystem CreateHorseSummarySystem(RequestInfo requestInfo)
		{
			return new HorseSummarySystem(GetConfig(requestInfo));
		}

		public IHorseTypeSystem CreateHorseTypeSystem(RequestInfo requestInfo)
		{
			return new HorseTypeSystem(GetConfig(requestInfo));
		}

		public IHorseSexSystem CreateHorseSexSystem(RequestInfo requestInfo)
		{
			return new HorseSexSystem(GetConfig(requestInfo));
		}

		public IProjectSubContractPOSystem CreateProjectSubContractSystem(RequestInfo requestInfo)
		{
			return new ProjectSubContractPOSystem(GetConfig(requestInfo));
		}

		public IProjectSubContractPISystem CreateProjectSubContractPISystem(RequestInfo requestInfo)
		{
			return new ProjectSubContractPISystem(GetConfig(requestInfo));
		}

		public ICreditLimitReviewSystem CreateCreditLimitReviewSystem(RequestInfo requestInfo)
		{
			return new CreditLimitReviewSystem(GetConfig(requestInfo));
		}

		public IEntityFlagSystem CreateEntityFlagSystem(RequestInfo requestInfo)
		{
			return new EntityFlagSystem(GetConfig(requestInfo));
		}

		public IEmployeePerformanceSystem CreateEmployeePerformanceSystem(RequestInfo requestInfo)
		{
			return new EmployeePerformanceSystem(GetConfig(requestInfo));
		}

		public IBillOfLadingSystem CreateBillOfLadingSystem(RequestInfo requestInfo)
		{
			return new BillOfLadingSystem(GetConfig(requestInfo));
		}

		public IEquipmentCategorySystem CreateEquipmentCategorySystem(RequestInfo requestInfo)
		{
			return new EquipmentCategorySystem(GetConfig(requestInfo));
		}

		public IEquipmentTypeSystem CreateEquipmentTypeSystem(RequestInfo requestInfo)
		{
			return new EquipmentTypeSystem(GetConfig(requestInfo));
		}

		public IEAEquipmentSystem CreateEAEquipmentSystem(RequestInfo requestInfo)
		{
			return new EAEquipmentSystem(GetConfig(requestInfo));
		}

		public IRequisitionTypeSystem CreateRequisitionTypeSystem(RequestInfo requestInfo)
		{
			return new RequisitionTypeSystem(GetConfig(requestInfo));
		}

		public IRequisitionSystem CreateRequisitionSystem(RequestInfo requestInfo)
		{
			return new RequisitionSystem(GetConfig(requestInfo));
		}

		public IMobilizationSystem CreateMobilizationSystem(RequestInfo requestInfo)
		{
			return new MobilizationSystem(GetConfig(requestInfo));
		}

		public IEquipmentTransferSystem CreateEquipmentTransferSystem(RequestInfo requestInfo)
		{
			return new EquipmentTransferSystem(GetConfig(requestInfo));
		}

		public IEquipmentWorkOrderSystem CreateEquipmentWorkOrderSystem(RequestInfo requestInfo)
		{
			return new EquipmentWorkOrderSystem(GetConfig(requestInfo));
		}

		public ILawyerSystem CreateLawyerSystem(RequestInfo requestInfo)
		{
			return new LawyerSystem(GetConfig(requestInfo));
		}

		public ICasePartySystem CreateCasePartySystem(RequestInfo requestInfo)
		{
			return new CasePartySystem(GetConfig(requestInfo));
		}

		public IWorkOrderInventoryIssueSystem CreateWorkOrderInventoryIssueSystem(RequestInfo requestInfo)
		{
			return new WorkOrderInventoryIssueSystem(GetConfig(requestInfo));
		}

		public ILegalActivitySystem CreateLegalActivitySystem(RequestInfo requestInfo)
		{
			return new LegalActivitySystem(GetConfig(requestInfo));
		}

		public IWorkOrderInventoryReturnSystem CreateWorkOrderInventoryReturnSystem(RequestInfo requestInfo)
		{
			return new WorkOrderInventoryReturnSystem(GetConfig(requestInfo));
		}

		public IFreightChargeSystem CreateFreightChargeSystem(RequestInfo requestInfo)
		{
			return new FreightChargeSystem(GetConfig(requestInfo));
		}

		public ITaxSystem CreateTaxSystem(RequestInfo requestInfo)
		{
			return new TaxSystem(GetConfig(requestInfo));
		}

		public IInventoryDismantleSystem CreateInventoryDismantleSystem(RequestInfo requestInfo)
		{
			return new InventoryDismantleSystem(GetConfig(requestInfo));
		}

		public IProductPriceBulkUpdateSystem CreateProductPriceBulkpdateSystem(RequestInfo requestInfo)
		{
			return new ProductPriceBulkUpdateSystem(GetConfig(requestInfo));
		}

		public IOpeningEntryTransactionSystem CreateOpeningEntryTransactionSystem(RequestInfo requestInfo)
		{
			return new OpeningEntryTransactionSystem(GetConfig(requestInfo));
		}

		public IMaterialReservationSystem CreateMaterialReservationSystem(RequestInfo requestInfo)
		{
			return new MaterialReservationSystem(GetConfig(requestInfo));
		}

		public ICaseClientSystem CreateCaseClientSystem(RequestInfo requestInfo)
		{
			return new CaseClientSystem(GetConfig(requestInfo));
		}

		public ILegalActionSystem CreateLegalActionSystem(RequestInfo requestInfo)
		{
			return new LegalActionSystem(GetConfig(requestInfo));
		}

		public ISalesForecastingSystem CreateSalesForecastingSystem(RequestInfo requestInfo)
		{
			return new SalesForecastingSystem(GetConfig(requestInfo));
		}

		public IProductMakeSystem CreateProductMakeSystem(RequestInfo requestInfo)
		{
			return new ProductMakeSystem(GetConfig(requestInfo));
		}

		public IProductTypeSystem CreateProductTypeSystem(RequestInfo requestInfo)
		{
			return new ProductTypeSystem(GetConfig(requestInfo));
		}

		public IProductModelSystem CreateProductModelSystem(RequestInfo requestInfo)
		{
			return new ProductModelSystem(GetConfig(requestInfo));
		}

		public ITaxGroupSystem CreateTaxGroupSystem(RequestInfo requestInfo)
		{
			return new TaxGroupSystem(GetConfig(requestInfo));
		}

		public IUserFavoriteSystem CreateUserFavoriteSystem(RequestInfo requestInfo)
		{
			return new UserFavoriteSystem(GetConfig(requestInfo));
		}

		public IPurchasePrepaymentInvoiceSystem CreatePurchasePrepaymentInvoiceSystem(RequestInfo requestInfo)
		{
			return new PurchasePrepaymentInvoiceSystem(GetConfig(requestInfo));
		}

		public ISalespersonGroupSystem CreateSalespersonGroupSystem(RequestInfo requestInfo)
		{
			return new SalespersonGroupSystem(GetConfig(requestInfo));
		}

		public ITaskStepsSystem CreateTaskStepsSystem(RequestInfo requestInfo)
		{
			return new TaskStepsSystem(GetConfig(requestInfo));
		}

		public ITaskTypeSystem CreateTaskTypeSystem(RequestInfo requestInfo)
		{
			return new TaskTypeSystem(GetConfig(requestInfo));
		}

		public ITaskTransactionSystem CreateTaskTransactionSystem(RequestInfo requestInfo)
		{
			return new TaskTransactionSystem(GetConfig(requestInfo));
		}

		public ITaskTransactionStatusSystem CreateTaskTransactionStatusSystem(RequestInfo requestInfo)
		{
			return new TaskTransactionStatusSystem(GetConfig(requestInfo));
		}

		public ILegalActionStatusSystem CreateLegalActionStatusSystem(RequestInfo requestInfo)
		{
			return new LegalActionStatusSystem(GetConfig(requestInfo));
		}

		public ITRApplicationSystem CreateTRApplicationSystem(RequestInfo requestInfo)
		{
			return new TRApplicationSystem(GetConfig(requestInfo));
		}

		public IBudgetingSystem CreateBudgetingSystem(RequestInfo requestInfo)
		{
			return new BudgetingSystem(GetConfig(requestInfo));
		}

		public IVehicleMileageTrackSystem CreateVehicleMileageTrackSystem(RequestInfo requestInfo)
		{
			return new VehicleMileageTrackSystem(GetConfig(requestInfo));
		}

		public ISalesManTargetSystem CreateSalesManTargetSystem(RequestInfo requestInfo)
		{
			return new SalesManTargetSystem(GetConfig(requestInfo));
		}

		public ICustomerInsuranceClaimSystem CreateCustomerInsuranceClaimSystem(RequestInfo requestInfo)
		{
			return new CustomerInsuranceClaimSystem(GetConfig(requestInfo));
		}

		public IPropertyDocumentSystem CreatePropertyDocumentSystem(RequestInfo requestInfo)
		{
			return new PropertyDocumentSystem(GetConfig(requestInfo));
		}

		public IPropertyTenantDocumentSystem CreatePropertyTenantDocumentSystem(RequestInfo requestInfo)
		{
			return new PropertyTenantDocumentSystem(GetConfig(requestInfo));
		}

		public IPropertyDocTypeSystem CreatePropertyDocTypeSystem(RequestInfo requestInfo)
		{
			return new PropertyDocTypeSystem(GetConfig(requestInfo));
		}

		public IRecurringInvoiceSystem CreateRecurringInvoiceSystem(RequestInfo requestInfo)
		{
			return new RecurringInvoiceSystem(GetConfig(requestInfo));
		}

		public IPropertyTenantDocTypeSystem CreatePropertyTenantDocTypeSystem(RequestInfo requestInfo)
		{
			return new PropertyTenantDocTypeSystem(GetConfig(requestInfo));
		}

		public ILoanEntrySystem CreateLoanEntrySystem(RequestInfo requestInfo)
		{
			return new LoanEntrySystem(GetConfig(requestInfo));
		}

		public IPrintTemplateMapSystem CreatePrintTemplateMapSystem(RequestInfo requestInfo)
		{
			return new PrintTemplateMapSystem(GetConfig(requestInfo));
		}
	}
}
