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
	public class CompanySystem : MarshalByRefObject, ICompanySystem, IDisposable
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

		public CompanySystem()
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

		~CompanySystem()
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

		public void RemoveWorkerTokens(string currentToken, string userID, string computerName)
		{
			checked
			{
				for (int i = 0; i < configCol.Keys.Count; i++)
				{
					string text = configCol.GetKey(i).ToString();
					ConfigData configData = configCol[text] as ConfigData;
					if (configData != null && !(configData.UserID != userID) && !(configData.ClientMachineName != computerName) && text != currentToken && configData.config.IsAsync)
					{
						RemoveConfig(text);
						i--;
					}
				}
			}
		}

		private void RemoveConfig(string key)
		{
			try
			{
				GetConfig(key).Disconnect();
				configCol.Remove(key);
			}
			catch
			{
			}
		}

		private Config GetConfig(string key)
		{
			if (key == null || key == string.Empty)
			{
				throw new ApplicationException("Configuration key is not set or not correct.");
			}
			ConfigData configData = configCol[key] as ConfigData;
			if (configData == null)
			{
				throw new CompanyException("Connection to server lost. Please login again.", 1041);
			}
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

		public bool RemoveUser(string yourToken, string userID, string machineID, string dbName)
		{
			GetConfig(yourToken);
			bool result = false;
			IDictionaryEnumerator enumerator = configCol.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ConfigData configData = enumerator.Value as ConfigData;
				if (configData != null && configData.UserID == userID && configData.ClientMachineName == machineID && configData.DbName == dbName)
				{
					if (yourToken == enumerator.Key.ToString())
					{
						throw new Exception("You cannot remove yourself.");
					}
					configCol.Remove(enumerator.Key);
					result = true;
					break;
				}
			}
			return result;
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

		public string CreateEmptyLogin(string applicationName, string clientMachineName, string serverName, string systemID, string productKey, string id)
		{
			lock (this)
			{
				string text = Guid.NewGuid().ToString("N");
				try
				{
					new ConfigHelper(id);
					Config config = new Config(id, ConnectionTypes.LocalServer);
					ConfigData configData = new ConfigData(applicationName, clientMachineName, systemID, productKey, serverName, "", "", "", config);
					configData.ID = id;
					AddConfig(text, configData);
					return text;
				}
				catch
				{
					throw;
				}
			}
		}

		public string SetLoginInfo(string applicationName, string clientMachineName, string serverName, string systemID, string productKey, string dbName, string userID, string password, string dbAdminUser, string dbAdminPassword, string id, ConnectionTypes connectionType)
		{
			return SetLoginInfo(applicationName, clientMachineName, serverName, systemID, productKey, dbName, userID, password, dbAdminUser, dbAdminPassword, id, connectionType, isAsync: false);
		}

		public string SetLoginInfo(string applicationName, string clientMachineName, string serverName, string systemID, string productKey, string dbName, string userID, string password, string dbAdminUser, string dbAdminPassword, string id, ConnectionTypes connectionType, bool isAsync)
		{
			if (dbName == "")
			{
				dbName = "Master";
			}
			if (dbName == null || dbName.Trim() == string.Empty || userID == null || userID.Trim() == string.Empty)
			{
				return null;
			}
			lock (this)
			{
				string text = Guid.NewGuid().ToString("N");
				try
				{
					ConfigHelper configHelper = new ConfigHelper(id);
					Config config = new Config(id, connectionType);
					config.IsAsync = isAsync;
					config.SetLoginInfo(applicationName, clientMachineName, serverName, systemID, productKey, dbName, userID, password, dbAdminUser, dbAdminPassword, configHelper.Cryptor, id, connectionType);
					config.OpenConnection();
					ConfigData configData = new ConfigData(applicationName, clientMachineName, serverName, systemID, productKey, dbName, userID, password, config);
					AddConfig(text, configData);
					return text;
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

		public bool LogClientSignIn(string token)
		{
			return GetConfig(token).LogClientSignIn();
		}

		private bool LogClientSignOut(string token)
		{
			return GetConfig(token).LogClientSignOut();
		}

		public bool IsConnected(string token)
		{
			ConfigData configData = configCol[token] as ConfigData;
			if (configData != null && configData.config != null)
			{
				return true;
			}
			return false;
		}

		public void Disconnect(string token)
		{
			LogClientSignOut(token);
			RemoveConfig(token);
		}

		public IProductSystem CreateProductSystem(string token)
		{
			return new ProductSystem(GetConfig(token));
		}

		public IUnitSystem CreateUnitSystem(string token)
		{
			return new UnitSystem(GetConfig(token));
		}

		public IProductCategorySystem CreateProductCategorySystem(string token)
		{
			return new ProductCategorySystem(GetConfig(token));
		}

		public IMaintenanceSchedulerSystem CreateMaintenanceSchedulerSystem(string token)
		{
			return new MaintenanceSchedulerSystem(GetConfig(token));
		}

		public IVehicleMaintenanceEntrySystem CreateMaintenanceEntrySystem(string token)
		{
			return new VehicleMaintenanceEntrySystem(GetConfig(token));
		}

		public IEmployeeSystem CreateEmployeeSystem(string token)
		{
			return new EmployeeSystem(GetConfig(token));
		}

		public IEmployeeProvisionSystem CreateEmployeeProvisionSystem(string token)
		{
			return new EmployeeProvisionSystem(GetConfig(token));
		}

		public ICandidateSystem CreateCandidateSystem(string token)
		{
			return new CandidateSystem(GetConfig(token));
		}

		public IShippingMethodSystem CreateShippingMethodSystem(string token)
		{
			return new ShippingMethodSystem(GetConfig(token));
		}

		public ICompanyAccountSystem CreateCompanyAccountSystem(string token)
		{
			return new CompanyAccountSystem(GetConfig(token));
		}

		public ICurrencySystem CreateCurrencySystem(string token)
		{
			return new CurrencySystem(GetConfig(token));
		}

		public IBankSystem CreateBankSystem(string token)
		{
			return new BankSystem(GetConfig(token));
		}

		public IPhysicalStockEntrySystem CreatePhysicalStockEntrySystem(string token)
		{
			return new PhysicalStockEntrySystem(GetConfig(token));
		}

		public IProductManufacturerSystem CreateProductManufacturerSystem(string token)
		{
			return new ProductManufacturerSystem(GetConfig(token));
		}

		public IReleaseTypeSystem CreateReleaseTypeSystem(string token)
		{
			return new ReleaseTypeSystem(GetConfig(token));
		}

		public ILeadStatusSystem CreateLeadStatusSystem(string token)
		{
			return new LeadStatusSystem(GetConfig(token));
		}

		public IProductBrandSystem CreateProductBrandSystem(string token)
		{
			return new ProductBrandSystem(GetConfig(token));
		}

		public IProductStyleSystem CreateProductStyleSystem(string token)
		{
			return new ProductStyleSystem(GetConfig(token));
		}

		public IProductSpecificationSystem CreateProductSpecificationSystem(string token)
		{
			return new ProductSpecificationSystem(GetConfig(token));
		}

		public IProductAttributeSystem CreateProductAttributeSystem(string token)
		{
			return new ProductAttributeSystem(GetConfig(token));
		}

		public IProductSizeSystem CreateProductSizeSystem(string token)
		{
			return new ProductSizeSystem(GetConfig(token));
		}

		public ITransactionSystem CreateTransactionSystem(string token)
		{
			return new TransactionSystem(GetConfig(token));
		}

		public IContainerSizeSystem CreateContainerSizeSystem(string token)
		{
			return new ContainerSizeSystem(GetConfig(token));
		}

		public IPurchaseOrderSystem CreatePurchaseOrderSystem(string token)
		{
			return new PurchaseOrderSystem(GetConfig(token));
		}

		public IPurchaseCostEntrySystem CreatePurchaseCostEntrySystem(string token)
		{
			return new PurchaseCostEntrySystem(GetConfig(token));
		}

		public IPurchaseOrderNISystem CreatePurchaseOrderNISystem(string token)
		{
			return new PurchaseOrderNISystem(GetConfig(token));
		}

		public IHolidayCalendarSystem CreateHolidayCalendarSystem(string token)
		{
			return new HolidayCalendarSystem(GetConfig(token));
		}

		public ITermSystem CreateTermSystem(string token)
		{
			return new TermSystem(GetConfig(token));
		}

		public ISecuritySystem CreateSecuritySystem(string token)
		{
			return new SecuritySystem(GetConfig(token));
		}

		public ICompanyInformationSystem CreateCompanyInformationSystem(string token)
		{
			return new CompanyInformationSystem(GetConfig(token));
		}

		public IDatabaseSystem CreateDatabaseSystem(string token)
		{
			return new DatabaseSystem(GetConfig(token));
		}

		public INoteSystem CreateNoteSystem(string token)
		{
			return new NoteSystem(GetConfig(token));
		}

		public ISettingSystem CreateSettingSystem(string token)
		{
			return new SettingSystem(GetConfig(token));
		}

		public IPrintTemplateSystem CreatePrintTemplateSystem(string token)
		{
			return new PrintTemplateSystem(GetConfig(token));
		}

		public IDepartmentSystem CreateDepartmentSystem(string token)
		{
			return new DepartmentSystem(GetConfig(token));
		}

		public IPriceLevelSystem CreatePriceLevelSystem(string token)
		{
			return new PriceLevelSystem(GetConfig(token));
		}

		public IPaymentMethodSystem CreatePaymentMethodSystem(string token)
		{
			return new PaymentMethodSystem(GetConfig(token));
		}

		public IActivityLogSystem CreateActivityLogSystem(string token)
		{
			return new ActivityLogSystem(GetConfig(token));
		}

		public IFollowupSystem CreateFollowupSystem(string token)
		{
			return new FollowupSystem(GetConfig(token));
		}

		public ILicenseSystem CreateLicenseSystem(string token)
		{
			return new LicenseSystem(GetConfig(token));
		}

		public IScheduleSystem CreateScheduleSystem(string token)
		{
			return new ScheduleSystem(GetConfig(token));
		}

		public IAccountGroupsSystem CreateAccountGroupsSystem(string token)
		{
			return new AccountGroupsSystem(GetConfig(token));
		}

		public IAnalysisGroupsSystem CreateAnalysisGroupsSystem(string token)
		{
			return new AnalysisGroupsSystem(GetConfig(token));
		}

		public IAnalysisSystem CreateAnalysisSystem(string token)
		{
			return new AnalysisSystem(GetConfig(token));
		}

		public ICustomerSystem CreateCustomerSystem(string token)
		{
			return new CustomerSystem(GetConfig(token));
		}

		public ICountrySystem CreateCountrySystem(string token)
		{
			return new CountrySystem(GetConfig(token));
		}

		public ICustomerClassSystem CreateCustomerClassSystem(string token)
		{
			return new CustomerClassSystem(GetConfig(token));
		}

		public IAreaSystem CreateAreaSystem(string token)
		{
			return new AreaSystem(GetConfig(token));
		}

		public ITransporterSystem CreateTransporterSystem(string token)
		{
			return new TransporterSystem(GetConfig(token));
		}

		public IINCOSystem CreateINCOSystem(string token)
		{
			return new INCOSystem(GetConfig(token));
		}

		public IPartySystem CreatePartySystem(string token)
		{
			return new PartySystem(GetConfig(token));
		}

		public IAccountAnalysisDetailSystem CreateAccountAnalysisDetailSystem(string token)
		{
			return new AccountAnalysisSystem(GetConfig(token));
		}

		public ISalespersonSystem CreateSalespersonSystem(string token)
		{
			return new SalespersonSystem(GetConfig(token));
		}

		public ICustomerAddressSystem CreateCustomerAddressSystem(string token)
		{
			return new CustomerAddressSystem(GetConfig(token));
		}

		public IContactSystem CreateContactSystem(string token)
		{
			return new ContactSystem(GetConfig(token));
		}

		public IVendorClassSystem CreateVendorClassSystem(string token)
		{
			return new VendorClassSystem(GetConfig(token));
		}

		public IVendorSystem CreateVendorSystem(string token)
		{
			return new VendorSystem(GetConfig(token));
		}

		public IVendorAddressSystem CreateVendorAddressSystem(string token)
		{
			return new VendorAddressSystem(GetConfig(token));
		}

		public IBuyerSystem CreateBuyerSystem(string token)
		{
			return new BuyerSystem(GetConfig(token));
		}

		public IProductClassSystem CreateProductClassSystem(string token)
		{
			return new ProductClassSystem(GetConfig(token));
		}

		public IGradeSystem CreateGradeSystem(string token)
		{
			return new GradeSystem(GetConfig(token));
		}

		public ISponsorSystem CreateSponsorSystem(string token)
		{
			return new SponsorSystem(GetConfig(token));
		}

		public IProvisionTypeSystem CreateProvisionTypeSystem(string token)
		{
			return new ProvisionTypeSystem(GetConfig(token));
		}

		public INationalitySystem CreateNationalitySystem(string token)
		{
			return new NationalitySystem(GetConfig(token));
		}

		public IReligionSystem CreateReligionSystem(string token)
		{
			return new ReligionSystem(GetConfig(token));
		}

		public IDivisionSystem CreateDivisionSystem(string token)
		{
			return new DivisionSystem(GetConfig(token));
		}

		public ICompanyDivisionSystem CreateCompanyDivisionSystem(string token)
		{
			return new CompanyDivisionSystem(GetConfig(token));
		}

		public IPositionSystem CreatePositionSystem(string token)
		{
			return new PositionSystem(GetConfig(token));
		}

		public IEmployeeAppraisalSystem CreateEmployeeAppraisalSystem(string token)
		{
			return new EmployeeAppraisalSystem(GetConfig(token));
		}

		public IEmployeePerformanceSystem CreateEmployeePerfrmanceSystem(string token)
		{
			return new EmployeePerformanceSystem(GetConfig(token));
		}

		public IEmployeeDocTypeSystem CreateEmployeeDocTypeSystem(string token)
		{
			return new EmployeeDocTypeSystem(GetConfig(token));
		}

		public IPatientDocTypeSystem CreatePatientDocTypeSystem(string token)
		{
			return new PatientDocTypeSystem(GetConfig(token));
		}

		public IVehicleDocTypeSystem CreateVehicleDocTypeSystem(string token)
		{
			return new VehicleDocTypeSystem(GetConfig(token));
		}

		public IDegreeSystem CreateDegreeSystem(string token)
		{
			return new DegreeSystem(GetConfig(token));
		}

		public ISkillSystem CreateSkillSystem(string token)
		{
			return new SkillSystem(GetConfig(token));
		}

		public ICustomerGroupSystem CreateCustomerGroupSystem(string token)
		{
			return new CustomerGroupSystem(GetConfig(token));
		}

		public IVendorGroupSystem CreateVendorGroupSystem(string token)
		{
			return new VendorGroupSystem(GetConfig(token));
		}

		public IEmployeeGroupSystem CreateEmployeeGroupSystem(string token)
		{
			return new EmployeeGroupSystem(GetConfig(token));
		}

		public IEmployeeAddressSystem CreateEmployeeAddressSystem(string token)
		{
			return new EmployeeAddressSystem(GetConfig(token));
		}

		public ILocationSystem CreateLocationSystem(string token)
		{
			return new LocationSystem(GetConfig(token));
		}

		public IWorkLocationSystem CreateWorkLocationSystem(string token)
		{
			return new WorkLocationSystem(GetConfig(token));
		}

		public IEmployeeDependentSystem CreateEmployeeDependentSystem(string token)
		{
			return new EmployeeDependentSystem(GetConfig(token));
		}

		public IEmployeeDocumentSystem CreateEmployeeDocumentSystem(string token)
		{
			return new EmployeeDocumentSystem(GetConfig(token));
		}

		public IPatientDocumentSystem CreatePatientDocumentSystem(string token)
		{
			return new PatientDocumentSystem(GetConfig(token));
		}

		public IVehicleDocumentSystem CreateVehicleDocumentSystem(string token)
		{
			return new VehicleDocumentSystem(GetConfig(token));
		}

		public IEmployeeSkillSystem CreateEmployeeSkillSystem(string token)
		{
			return new EmployeeSkillSystem(GetConfig(token));
		}

		public ILeaveTypeSystem CreateLeaveTypeSystem(string token)
		{
			return new LeaveTypeSystem(GetConfig(token));
		}

		public IJobTypeSystem CreateJobTypeSystem(string token)
		{
			return new JobTypeSystem(GetConfig(token));
		}

		public IJobTaskGroupSystem CreateJobTaskGroupSystem(string token)
		{
			return new JobTaskGroupSystem(GetConfig(token));
		}

		public IServiceActivitySystem CreateServiceActivitySystem(string token)
		{
			return new ServiceActivitySystem(GetConfig(token));
		}

		public ICostCategorySystem CreateCostCategorySystem(string token)
		{
			return new CostCategorySystem(GetConfig(token));
		}

		public IEquipmentSystem CreateEquipmentSystem(string token)
		{
			return new EquipmentSystem(GetConfig(token));
		}

		public IPayrollItemSystem CreatePayrollItemSystem(string token)
		{
			return new PayrollItemSystem(GetConfig(token));
		}

		public IDeductionSystem CreateDeductionSystem(string token)
		{
			return new DeductionSystem(GetConfig(token));
		}

		public IBenefitSystem CreateBenefitSystem(string token)
		{
			return new BenefitSystem(GetConfig(token));
		}

		public IEmployeePayrollItemDetailSystem CreateEmployeePayrollItemDetailSystem(string token)
		{
			return new EmployeePayrollItemDetailSystem(GetConfig(token));
		}

		public IEmployeeDeductionDetailSystem CreateEmployeeDeductionDetailSystem(string token)
		{
			return new EmployeeDeductionDetailSystem(GetConfig(token));
		}

		public IEmployeeBenefitDetailSystem CreateEmployeeBenefitDetailSystem(string token)
		{
			return new EmployeeBenefitDetailSystem(GetConfig(token));
		}

		public IDestinationSystem CreateDestinationSystem(string token)
		{
			return new DestinationSystem(GetConfig(token));
		}

		public IEmployeeLeaveDetailSystem CreateEmployeeLeaveDetailSystem(string token)
		{
			return new EmployeeLeaveDetailSystem(GetConfig(token));
		}

		public IEmployeeLeaveProcessSystem CreateEmployeeLeaveProcessSystem(string token)
		{
			return new EmployeeLeaveProcessSystem(GetConfig(token));
		}

		public ICompanyDocTypeSystem CreateCompanyDocTypeSystem(string token)
		{
			return new CompanyDocTypeSystem(GetConfig(token));
		}

		public ICompanyDocumentSystem CreateCompanyDocumentSystem(string token)
		{
			return new CompanyDocumentSystem(GetConfig(token));
		}

		public ITenancyContractSystem CreateTenancyContractSystem(string token)
		{
			return new TenancyContractSystem(GetConfig(token));
		}

		public ITradeLicenseSystem CreateTradeLicenseSystem(string token)
		{
			return new TradeLicenseSystem(GetConfig(token));
		}

		public IVisaSystem CreateVisaSystem(string token)
		{
			return new VisaSystem(GetConfig(token));
		}

		public IJournalSystem CreateJournalSystem(string token)
		{
			return new JournalSystem(GetConfig(token));
		}

		public ISystemDocumentSystem CreateSystemDocumentSystem(string token)
		{
			return new SystemDocumentSystem(GetConfig(token));
		}

		public ICostCenterSystem CreateCostCenterSystem(string token)
		{
			return new CostCenterSystem(GetConfig(token));
		}

		public IRegisterSystem CreateRegisterSystem(string token)
		{
			return new RegisterSystem(GetConfig(token));
		}

		public IChequebookSystem CreateChequebookSystem(string token)
		{
			return new ChequebookSystem(GetConfig(token));
		}

		public IIssuedChequeSystem CreateIssuedChequeSystem(string token)
		{
			return new IssuedChequeSystem(GetConfig(token));
		}

		public IReceivedChequeSystem CreateReceivedChequeSystem(string token)
		{
			return new ReceivedChequeSystem(GetConfig(token));
		}

		public IReturnedChequeReasonSystem CreateReturnedChequeReasonSystem(string token)
		{
			return new ReturnedChequeReasonSystem(GetConfig(token));
		}

		public IAdjustmentTypeSystem CreateAdjustmentTypeSystem(string token)
		{
			return new AdjustmentTypeSystem(GetConfig(token));
		}

		public IInventoryAdjustmentSystem CreateInventoryAdjustmentSystem(string token)
		{
			return new InventoryAdjustmentSystem(GetConfig(token));
		}

		public IInventoryDamageSystem CreateInventoryDamageSystem(string token)
		{
			return new InventoryDamageSystem(GetConfig(token));
		}

		public IInventoryTransferSystem CreateInventoryTransferSystem(string token)
		{
			return new InventoryTransferSystem(GetConfig(token));
		}

		public IDriverSystem CreateDriverSystem(string token)
		{
			return new DriverSystem(GetConfig(token));
		}

		public ISalesQuoteSystem CreateSalesQuoteSystem(string token)
		{
			return new SalesQuoteSystem(GetConfig(token));
		}

		public ISalesOrderSystem CreateSalesOrderSystem(string token)
		{
			return new SalesOrderSystem(GetConfig(token));
		}

		public ISalesEnquirySystem CreateSalesEnquirySystem(string token)
		{
			return new SalesEnquirySystem(GetConfig(token));
		}

		public ISalesProformaSystem CreateSalesProformaSystem(string token)
		{
			return new SalesProformaSystem(GetConfig(token));
		}

		public IPriceListSystem CreatePriceListSystem(string token)
		{
			return new PriceListSystem(GetConfig(token));
		}

		public IDeliveryNoteSystem CreateDeliveryNoteSystem(string token)
		{
			return new DeliveryNoteSystem(GetConfig(token));
		}

		public IExportPickListSystem CreateExportPickListSystem(string token)
		{
			return new ExportPickListSystem(GetConfig(token));
		}

		public ISalesInvoiceSystem CreateSalesInvoiceSystem(string token)
		{
			return new SalesInvoiceSystem(GetConfig(token));
		}

		public ISalesInvoiceNISystem CreateSalesInvoiceNISystem(string token)
		{
			return new SalesInvoiceNISystem(GetConfig(token));
		}

		public ISalesReceiptSystem CreateSalesReceiptSystem(string token)
		{
			return new SalesReceiptSystem(GetConfig(token));
		}

		public ISalesReturnSystem CreateSalesReturnSystem(string token)
		{
			return new SalesReturnSystem(GetConfig(token));
		}

		public ILPOReceiptSystem CreateLPOReceiptSystem(string token)
		{
			return new LPOReceiptSystem(GetConfig(token));
		}

		public IJobInvoiceSystem CreateJobInvoiceSystem(string token)
		{
			return new JobInvoiceSystem(GetConfig(token));
		}

		public IDeliveryReturnSystem CreateDeliveryReturnSystem(string token)
		{
			return new DeliveryReturnSystem(GetConfig(token));
		}

		public IPurchaseQuoteSystem CreatePurchaseQuoteSystem(string token)
		{
			return new PurchaseQuoteSystem(GetConfig(token));
		}

		public IPOShipmentSystem CreatePOShipmentSystem(string token)
		{
			return new POShipmentSystem(GetConfig(token));
		}

		public IPortSystem CreatePortSystem(string token)
		{
			return new PortSystem(GetConfig(token));
		}

		public IPurchaseInvoiceSystem CreatePurchaseInvoiceSystem(string token)
		{
			return new PurchaseInvoiceSystem(GetConfig(token));
		}

		public IPurchaseInvoiceNISystem CreatePurchaseInvoiceNISystem(string token)
		{
			return new PurchaseInvoiceNISystem(GetConfig(token));
		}

		public IPurchaseReceiptSystem CreatePurchaseReceiptSystem(string token)
		{
			return new PurchaseReceiptSystem(GetConfig(token));
		}

		public IPurchaseReturnSystem CreatePurchaseReturnSystem(string token)
		{
			return new PurchaseReturnSystem(GetConfig(token));
		}

		public IShortcutSystem CreateShortcutSystem(string token)
		{
			return new ShortcutSystem(GetConfig(token));
		}

		public IEmployeeActivitySystem CreateEmployeeActivitySystem(string token)
		{
			return new EmployeeActivitySystem(GetConfig(token));
		}

		public IDisciplineActionTypeSystem CreateDisciplineActionTypeSystem(string token)
		{
			return new DisciplineActionTypeSystem(GetConfig(token));
		}

		public IEmployeeActivityTypeSystem CreateEmployeeActivityTypeSystem(string token)
		{
			return new EmployeeActivityTypeSystem(GetConfig(token));
		}

		public ISalarySheetSystem CreateSalarySheetSystem(string token)
		{
			return new SalarySheetSystem(GetConfig(token));
		}

		public IEmployeeTypeSystem CreateEmployeeTypeSystem(string token)
		{
			return new EmployeeTypeSystem(GetConfig(token));
		}

		public IPayrollTransactionSystem CreatePayrollTransactionSystem(string token)
		{
			return new PayrollTransactionSystem(GetConfig(token));
		}

		public IProjectExpenseAllocationSystem CreateProjectExpenseAllocationSystem(string token)
		{
			return new ProjectExpenseAllocationSystem(GetConfig(token));
		}

		public IEmployeeLoanSystem CreateEmployeeLoanSystem(string token)
		{
			return new EmployeeLoanSystem(GetConfig(token));
		}

		public IEmployeeLoanTypeSystem CreateEmployeeLoanTypeSystem(string token)
		{
			return new EmployeeLoanTypeSystem(GetConfig(token));
		}

		public IARJournalSystem CreateARJournalSystem(string token)
		{
			return new ARJournalSystem(GetConfig(token));
		}

		public IAPJournalSystem CreateAPJournalSystem(string token)
		{
			return new APJournalSystem(GetConfig(token));
		}

		public IUserSystem CreateUserSystem(string token)
		{
			return new UserSystem(GetConfig(token));
		}

		public IUserGroupSystem CreateUserGroupSystem(string token)
		{
			return new UserGroupSystem(GetConfig(token));
		}

		public IReminderSystem CreateReminderSystem(string token)
		{
			return new ReminderSystem(GetConfig(token));
		}

		public ICompanyAddressSystem CreateCompanyAddressSystem(string token)
		{
			return new CompanyAddressSystem(GetConfig(token));
		}

		public ISmartListSystem CreateSmartListSystem(string token)
		{
			return new SmartListSystem(GetConfig(token));
		}

		public IExternalReportSystem CreateExternalReportSystem(string token)
		{
			return new ExternalReportSystem(GetConfig(token));
		}

		public IItemTransactionSystem CreateItemTransactionSystem(string token)
		{
			return new ItemTransactionSystem(GetConfig(token));
		}

		public ISalesPOSSystem CreateSalesPOSSystem(string token)
		{
			return new SalesPOSSystem(GetConfig(token));
		}

		public IPOSBatchSystem CreatePOSBatchSystem(string token)
		{
			return new POSBatchSystem(GetConfig(token));
		}

		public IPOSShiftSystem CreatePOSShiftSystem(string token)
		{
			return new POSShiftSystem(GetConfig(token));
		}

		public IPOSCashRegisterSystem CreatePOSCashRegisterSystem(string token)
		{
			return new POSCashRegisterSystem(GetConfig(token));
		}

		public IPOSHoldSystem CreatePOSHoldSystem(string token)
		{
			return new POSHoldSystem(GetConfig(token));
		}

		public IConsignOutSystem CreateConsignOutSystem(string token)
		{
			return new ConsignOutSystem(GetConfig(token));
		}

		public IConsignOutSettlementSystem CreateConsignOutSettlementSystem(string token)
		{
			return new ConsignOutSettlementSystem(GetConfig(token));
		}

		public IExpenseCodeSystem CreateExpenseCodeSystem(string token)
		{
			return new ExpenseCodeSystem(GetConfig(token));
		}

		public ICustomerCategorySystem CreateCustomerCategorySystem(string token)
		{
			return new CustomerCategorySystem(GetConfig(token));
		}

		public IEntityCategorySystem CreateEntityCategorySystem(string token)
		{
			return new EntityCategorySystem(GetConfig(token));
		}

		public ICompanyOptionSystem CreateCompanyOptionSystem(string token)
		{
			return new CompanyOptionSystem(GetConfig(token));
		}

		public IConsignOutReturnSystem CreateConsignOutReturnSystem(string token)
		{
			return new ConsignOutReturnSystem(GetConfig(token));
		}

		public IConsignInReturnSystem CreateConsignInReturnSystem(string token)
		{
			return new ConsignInReturnSystem(GetConfig(token));
		}

		public IConsignInSystem CreateConsignInSystem(string token)
		{
			return new ConsignInSystem(GetConfig(token));
		}

		public IConsignInSettlementSystem CreateConsignInSettlementSystem(string token)
		{
			return new ConsignInSettlementSystem(GetConfig(token));
		}

		public IFixedAssetSystem CreateFixedAssetSystem(string token)
		{
			return new FixedAssetSystem(GetConfig(token));
		}

		public IFixedAssetGroupSystem CreateFixedAssetGroupSystem(string token)
		{
			return new FixedAssetGroupSystem(GetConfig(token));
		}

		public IFixedAssetLocationSystem CreateFixedAssetLocationSystem(string token)
		{
			return new FixedAssetLocationSystem(GetConfig(token));
		}

		public IFixedAssetClassSystem CreateFixedAssetClassSystem(string token)
		{
			return new FixedAssetClassSystem(GetConfig(token));
		}

		public IFixedAssetPurchaseSystem CreateFixedAssetPurchaseSystem(string token)
		{
			return new FixedAssetPurchaseSystem(GetConfig(token));
		}

		public IFixedAssetPurchaseOrderSystem CreateFixedAssetPurchaseOrderSystem(string token)
		{
			return new FixedAssetPurchaseOrderSystem(GetConfig(token));
		}

		public IFixedAssetTransferSystem CreateFixedAssetTransferSystem(string token)
		{
			return new FixedAssetTransferSystem(GetConfig(token));
		}

		public IFixedAssetSaleSystem CreateFixedAssetSaleSystem(string token)
		{
			return new FixedAssetSaleSystem(GetConfig(token));
		}

		public IFixedAssetDepSystem CreateFixedAssetDepSystem(string token)
		{
			return new FixedAssetDepSystem(GetConfig(token));
		}

		public IDimensionSystem CreateDimensionSystem(string token)
		{
			return new DimensionSystem(GetConfig(token));
		}

		public IProductParentSystem CreateProductParentSystem(string token)
		{
			return new ProductParentSystem(GetConfig(token));
		}

		public IMatrixTemplateSystem CreateMatrixTemplateSystem(string token)
		{
			return new MatrixTemplateSystem(GetConfig(token));
		}

		public IFiscalYearSystem CreateFiscalYearSystem(string token)
		{
			return new FiscalYearSystem(GetConfig(token));
		}

		public ILeadSystem CreateLeadSystem(string token)
		{
			return new LeadSystem(GetConfig(token));
		}

		public ILeadAddressSystem CreateLeadAddressSystem(string token)
		{
			return new LeadAddressSystem(GetConfig(token));
		}

		public IGenericListSystem CreateGenericListSystem(string token)
		{
			return new GenericListSystem(GetConfig(token));
		}

		public IUDFSystem CreateUDFSystem(string token)
		{
			return new UDFSystem(GetConfig(token));
		}

		public IPivotGroupSystem CreatePivotGroupSystem(string token)
		{
			return new PivotGroupSystem(GetConfig(token));
		}

		public IPivotSystem CreatePivotSystem(string token)
		{
			return new PivotSystem(GetConfig(token));
		}

		public IAssemblyBuildSystem CreateAssemblyBuildSystem(string token)
		{
			return new AssemblyBuildSystem(GetConfig(token));
		}

		public IProductionSystem CreateProductionSystem(string token)
		{
			return new ProductionSystem(GetConfig(token));
		}

		public IInventoryRepackingSystem CreateInventoryRepackingSystem(string token)
		{
			return new InventoryRepackingSystem(GetConfig(token));
		}

		public IWorkOrderSystem CreateWorkOrderSystem(string token)
		{
			return new WorkOrderSystem(GetConfig(token));
		}

		public IBOMSystem CreateBOMSystem(string token)
		{
			return new BOMSystem(GetConfig(token));
		}

		public IEOSRuleSystem CreateEOSRuleSystem(string token)
		{
			return new EOSRuleSystem(GetConfig(token));
		}

		public IOverTimeSystem CreateOverTimeSystem(string token)
		{
			return new OverTimeSystem(GetConfig(token));
		}

		public ISalarySystem CreateSalarySystem(string token)
		{
			return new SalarySystem(GetConfig(token));
		}

		public IEntityDocSystem CreateEntityDocSystem(string token)
		{
			return new EntityDocSystem(GetConfig(token));
		}

		public IJobSystem CreateJobSystem(string token)
		{
			return new JobSystem(GetConfig(token));
		}

		public IOpportunitySystem CreateOpportunitySystem(string token)
		{
			return new OpportunitySystem(GetConfig(token));
		}

		public ICompetitorSystem CreateCompetitorSystem(string token)
		{
			return new CompetitorSystem(GetConfig(token));
		}

		public IActivitySystem CreateActivitySystem(string token)
		{
			return new ActivitySystem(GetConfig(token));
		}

		public IBankReconciliationSystem CreateBankReconciliationSystem(string token)
		{
			return new BankReconciliationSystem(GetConfig(token));
		}

		public ICustomReportSystem CreateCustomReportSystem(string token)
		{
			return new CustomReportSystem(GetConfig(token));
		}

		public ICampaignSystem CreateCampaignSystem(string token)
		{
			return new CampaignSystem(GetConfig(token));
		}

		public IEventSystem CreateEventSystem(string token)
		{
			return new EventSystem(GetConfig(token));
		}

		public IJobInventoryIssueSystem CreateJobInventoryIssueSystem(string token)
		{
			return new JobInventoryIssueSystem(GetConfig(token));
		}

		public IJobInventoryReturnSystem CreateJobInventoryReturnSystem(string token)
		{
			return new JobInventoryReturnSystem(GetConfig(token));
		}

		public IJobExpenseIssueSystem CreateJobExpenseIssueSystem(string token)
		{
			return new JobExpenseIssueSystem(GetConfig(token));
		}

		public IJobTimesheetSystem CreateJobTimesheetSystem(string token)
		{
			return new JobTimesheetSystem(GetConfig(token));
		}

		public IOverTimeEntrySystem CreateOverTimeEntrySystem(string token)
		{
			return new OverTimeEntrySystem(GetConfig(token));
		}

		public IJobMaterialRequisitionSystem CreateJobMaterialRequisitionSystem(string token)
		{
			return new JobMaterialRequisitionSystem(GetConfig(token));
		}

		public IJobMaterialEstimateSystem CreateJobMaterialEstimateSystem(string token)
		{
			return new JobMaterialEstimateSystem(GetConfig(token));
		}

		public IJobManHrsBudgetingSystem CreateJobManHrsBudgetingSystem(string token)
		{
			return new JobManHrsBudgetingSystem(GetConfig(token));
		}

		public IJobMaintenanceScheduleSystem CreateJobMaintenanceScheduleSystem(string token)
		{
			return new JobMaintenanceScheduleSystem(GetConfig(token));
		}

		public IJobMaintenanceServiceSystem CreateJobMaintenanceServiceSystem(string token)
		{
			return new JobMaintenanceServiceSystem(GetConfig(token));
		}

		public IServiceCallTrackSystem CreateServiceCallTrackSystem(string token)
		{
			return new ServiceCallTrackSystem(GetConfig(token));
		}

		public IBankFacilityGroupSystem CreateBankFacilityGroupSystem(string token)
		{
			return new BankFacilityGroupSystem(GetConfig(token));
		}

		public IBankFacilitySystem CreateBankFacilitySystem(string token)
		{
			return new BankFacilitySystem(GetConfig(token));
		}

		public IOpeningBalanceBatchSystem CreateOpeningBalanceBatchSystem(string token)
		{
			return new OpeningBalanceBatchSystem(GetConfig(token));
		}

		public IOpeningBalanceLeaveSystem CreateOpeningBalanceLeaveSystem(string token)
		{
			return new OpeningBalanceLeaveSystem(GetConfig(token));
		}

		public ICitySystem CreateCitySystem(string token)
		{
			return new CitySystem(GetConfig(token));
		}

		public IVehicleSystem CreateVehicleSystem(string token)
		{
			return new VehicleSystem(GetConfig(token));
		}

		public IBankFacilityTransactionSystem CreateBankFacilityTransactionSystem(string token)
		{
			return new BankFacilityTransactionSystem(GetConfig(token));
		}

		public IBankFacilityPaymentSystem CreateBankFacilityPaymentSystem(string token)
		{
			return new BankFacilityPaymentSystem(GetConfig(token));
		}

		public ICustomGadgetSystem CreateCustomGadgetSystem(string token)
		{
			return new CustomGadgetSystem(GetConfig(token));
		}

		public ISendChequeSystem CreateSendChequeSystem(string token)
		{
			return new SendChequeSystem(GetConfig(token));
		}

		public IDiscountChequeSystem CreateDiscountChequeSystem(string token)
		{
			return new DiscountChequeSystem(GetConfig(token));
		}

		public IDiscountBillSystem CreateDiscountBillSystem(string token)
		{
			return new DiscountBillSystem(GetConfig(token));
		}

		public IInventoryTransferTypeSystem CreateInventoryTransferTypeSystem(string token)
		{
			return new InventoryTransferTypeSystem(GetConfig(token));
		}

		public IExportPackingListSystem CreateExportPackingListSystem(string token)
		{
			return new ExportPackingListSystem(GetConfig(token));
		}

		public IStandingJournalSystem CreateStandingJournalSystem(string token)
		{
			return new StandingJournalSystem(GetConfig(token));
		}

		public IDashboardSystem CreateDashboardSystem(string token)
		{
			return new DashboardSystem(GetConfig(token));
		}

		public IWebDashboardSystem CreateWebDashboardSystem(string token)
		{
			return new WebDashboardSystem(GetConfig(token));
		}

		public ICollateralSystem CreateCollateralSystem(string token)
		{
			return new CollateralSystem(GetConfig(token));
		}

		public IJobTaskSystem CreateJobTaskSystem(string token)
		{
			return new JobTaskSystem(GetConfig(token));
		}

		public IClientAssetSystem CreateClientAssetSystem(string token)
		{
			return new ClientAssetSystem(GetConfig(token));
		}

		public IBinSystem CreateBinSystem(string token)
		{
			return new BinSystem(GetConfig(token));
		}

		public IRouteSystem CreateRouteSystem(string token)
		{
			return new RouteSystem(GetConfig(token));
		}

		public IRouteGroupSystem CreateRouteGroupSystem(string token)
		{
			return new RouteGroupSystem(GetConfig(token));
		}

		public IRackSystem CreateRackSystem(string token)
		{
			return new RackSystem(GetConfig(token));
		}

		public IGRNReturnSystem CreateGRNReturnSystem(string token)
		{
			return new GRNReturnSystem(GetConfig(token));
		}

		public IArrivalReportSystem CreateArrivalReportSystem(string token)
		{
			return new ArrivalReportSystem(GetConfig(token));
		}

		public IQualityClaimSystem CreateQualityClaimSystem(string token)
		{
			return new QualityClaimSystem(GetConfig(token));
		}

		public IQualityTaskSystem CreateQualityTaskSystem(string token)
		{
			return new QualityTaskSystem(GetConfig(token));
		}

		public ISurveyorSystem CreateSurveyorSystem(string token)
		{
			return new SurveyorSystem(GetConfig(token));
		}

		public IArrivalReportTemplateSystem CreateArrivalReportTemplateSystem(string token)
		{
			return new ArrivalReportTemplateSystem(GetConfig(token));
		}

		public IApprovalSystem CreateApprovalSystem(string token)
		{
			return new ApprovalSystem(GetConfig(token));
		}

		public IPropertyAgentSystem CreatePropertyAgentSystem(string token)
		{
			return new PropertyAgentSystem(GetConfig(token));
		}

		public IPropertyClassSystem CreatePropertyClassSystem(string token)
		{
			return new PropertyClassSystem(GetConfig(token));
		}

		public IPropertySystem CreatePropertySystem(string token)
		{
			return new PropertySystem(GetConfig(token));
		}

		public IPropertyUnitSystem CreatePropertyUnitSystem(string token)
		{
			return new PropertyUnitSystem(GetConfig(token));
		}

		public IPropertyVirtualUnitSystem CreatePropertyVirtualUnitSystem(string token)
		{
			return new PropertyVirtualUnitSystem(GetConfig(token));
		}

		public IPropertyIncomeCodeSystem CreatePropertyIncomeCodeSystem(string token)
		{
			return new PropertyIncomeCodeSystem(GetConfig(token));
		}

		public IPropertyCategorySystem CreatePropertyCategorySystem(string token)
		{
			return new PropertyCategorySystem(GetConfig(token));
		}

		public ICheckListSystem CreateCheckListSystem(string token)
		{
			return new CheckListSystem(GetConfig(token));
		}

		public IPaymentRequestSystem CreatePaymentRequestSystem(string token)
		{
			return new PaymentRequestSystem(GetConfig(token));
		}

		public IPropertyRentSystem CreatePropertyRentSystem(string token)
		{
			return new PropertyRentSystem(GetConfig(token));
		}

		public IPropertyCancelSystem CreatePropertyCancelSystem(string token)
		{
			return new PropertyCancelSystem(GetConfig(token));
		}

		public IRentalPostingSystem CreateRentalPostingSystem(string token)
		{
			return new RentalPostingSystem(GetConfig(token));
		}

		public IW3PLGRNSystem CreateW3PLGRNSystem(string token)
		{
			return new W3PLGRNSystem(GetConfig(token));
		}

		public IW3PLDeliverySystem CreateW3PLDeliverySystem(string token)
		{
			return new W3PLDeliverySystem(GetConfig(token));
		}

		public IW3PLInvoiceSystem CreateW3PLInvoiceSystem(string token)
		{
			return new W3PLInvoiceSystem(GetConfig(token));
		}

		public IPurchaseClaimSystem CreatePurchaseClaimSystem(string token)
		{
			return new PurchaseClaimSystem(GetConfig(token));
		}

		public IPropertyServiceSystem CreatePropertyServiceSystem(string token)
		{
			return new PropertyServiceSystem(GetConfig(token));
		}

		public IJobBOMSystem CreateJobBOMSystem(string token)
		{
			return new JobBOMSystem(GetConfig(token));
		}

		public IJobEstimationSystem CreateJobEstimationSystem(string token)
		{
			return new JobEstimationSystem(GetConfig(token));
		}

		public IGarmentRentalSystem CreateGarmentRentalSystem(string token)
		{
			return new GarmentRentalSystem(GetConfig(token));
		}

		public IGarmentRentalReturnSystem CreateGarmentRentalReturnSystem(string token)
		{
			return new GarmentRentalReturnSystem(GetConfig(token));
		}

		public IEmailMessageSystem CreateEmailMessageSystem(string token)
		{
			return new EmailMessageSystem(GetConfig(token));
		}

		public ICLVoucherSystem CreateCLVoucherSystem(string token)
		{
			return new CLVoucherSystem(GetConfig(token));
		}

		public ICLTokenSystem CreateCLTokenSystem(string token)
		{
			return new CLTokenSystem(GetConfig(token));
		}

		public IEmployeeEOSSettlementSystem CreateEmployeeEOSSettlementSystem(string token)
		{
			return new EmployeeEOSSettlementSystem(GetConfig(token));
		}

		public IFixedAssetBulkPurchaseSystem CreateFixedAssetBulkPurchaseSystem(string token)
		{
			return new FixedAssetBulkPurchaseSystem(GetConfig(token));
		}

		public IServiceTypeSystem CreateServiceTypeSystem(string token)
		{
			return new ServiceTypeSystem(GetConfig(token));
		}

		public IEntityCommentSystem CreateEntityCommentSystem(string token)
		{
			return new EntityCommentSystem(GetConfig(token));
		}

		public IContainerTrackingSystem CreateContainerTrackingSystem(string token)
		{
			return new ContainerTrackingSystem(GetConfig(token));
		}

		public IInsuranceProviderSystem CreateInsuranceProviderSystem(string token)
		{
			return new InsuranceProviderSystem(GetConfig(token));
		}

		public ICustomerInsuranceSystem CreateCustomerInsuranceSystem(string token)
		{
			return new CustomerInsuranceSystem(GetConfig(token));
		}

		public IRiderSummarySystem CreateRiderSummarySystem(string token)
		{
			return new RiderSummarySystem(GetConfig(token));
		}

		public IHorseSummarySystem CreateHorseSummarySystem(string token)
		{
			return new HorseSummarySystem(GetConfig(token));
		}

		public IHorseTypeSystem CreateHorseTypeSystem(string token)
		{
			return new HorseTypeSystem(GetConfig(token));
		}

		public IHorseSexSystem CreateHorseSexSystem(string token)
		{
			return new HorseSexSystem(GetConfig(token));
		}

		public IProjectSubContractPOSystem CreateProjectSubContractSystem(string token)
		{
			return new ProjectSubContractPOSystem(GetConfig(token));
		}

		public IProjectSubContractPISystem CreateProjectSubContractPISystem(string token)
		{
			return new ProjectSubContractPISystem(GetConfig(token));
		}

		public ICreditLimitReviewSystem CreateCreditLimitReviewSystem(string token)
		{
			return new CreditLimitReviewSystem(GetConfig(token));
		}

		public IEntityFlagSystem CreateEntityFlagSystem(string token)
		{
			return new EntityFlagSystem(GetConfig(token));
		}

		public IEmployeePerformanceSystem CreateEmployeePerformanceSystem(string token)
		{
			return new EmployeePerformanceSystem(GetConfig(token));
		}

		public IBillOfLadingSystem CreateBillOfLadingSystem(string token)
		{
			return new BillOfLadingSystem(GetConfig(token));
		}

		public IEquipmentCategorySystem CreateEquipmentCategorySystem(string token)
		{
			return new EquipmentCategorySystem(GetConfig(token));
		}

		public IEquipmentTypeSystem CreateEquipmentTypeSystem(string token)
		{
			return new EquipmentTypeSystem(GetConfig(token));
		}

		public IEAEquipmentSystem CreateEAEquipmentSystem(string token)
		{
			return new EAEquipmentSystem(GetConfig(token));
		}

		public IRequisitionTypeSystem CreateRequisitionTypeSystem(string token)
		{
			return new RequisitionTypeSystem(GetConfig(token));
		}

		public IRequisitionSystem CreateRequisitionSystem(string token)
		{
			return new RequisitionSystem(GetConfig(token));
		}

		public IMobilizationSystem CreateMobilizationSystem(string token)
		{
			return new MobilizationSystem(GetConfig(token));
		}

		public IEquipmentTransferSystem CreateEquipmentTransferSystem(string token)
		{
			return new EquipmentTransferSystem(GetConfig(token));
		}

		public IEquipmentWorkOrderSystem CreateEquipmentWorkOrderSystem(string token)
		{
			return new EquipmentWorkOrderSystem(GetConfig(token));
		}

		public ILawyerSystem CreateLawyerSystem(string token)
		{
			return new LawyerSystem(GetConfig(token));
		}

		public ICasePartySystem CreateCasePartySystem(string token)
		{
			return new CasePartySystem(GetConfig(token));
		}

		public IWorkOrderInventoryIssueSystem CreateWorkOrderInventoryIssueSystem(string token)
		{
			return new WorkOrderInventoryIssueSystem(GetConfig(token));
		}

		public ILegalActivitySystem CreateLegalActivitySystem(string token)
		{
			return new LegalActivitySystem(GetConfig(token));
		}

		public IWorkOrderInventoryReturnSystem CreateWorkOrderInventoryReturnSystem(string token)
		{
			return new WorkOrderInventoryReturnSystem(GetConfig(token));
		}

		public IFreightChargeSystem CreateFreightChargeSystem(string token)
		{
			return new FreightChargeSystem(GetConfig(token));
		}

		public ITaxSystem CreateTaxSystem(string token)
		{
			return new TaxSystem(GetConfig(token));
		}

		public IInventoryDismantleSystem CreateInventoryDismantleSystem(string token)
		{
			return new InventoryDismantleSystem(GetConfig(token));
		}

		public IProductPriceBulkUpdateSystem CreateProductPriceBulkpdateSystem(string token)
		{
			return new ProductPriceBulkUpdateSystem(GetConfig(token));
		}

		public IOpeningEntryTransactionSystem CreateOpeningEntryTransactionSystem(string token)
		{
			return new OpeningEntryTransactionSystem(GetConfig(token));
		}

		public IMaterialReservationSystem CreateMaterialReservationSystem(string token)
		{
			return new MaterialReservationSystem(GetConfig(token));
		}

		public ICaseClientSystem CreateCaseClientSystem(string token)
		{
			return new CaseClientSystem(GetConfig(token));
		}

		public ILegalActionSystem CreateLegalActionSystem(string token)
		{
			return new LegalActionSystem(GetConfig(token));
		}

		public ISalesForecastingSystem CreateSalesForecastingSystem(string token)
		{
			return new SalesForecastingSystem(GetConfig(token));
		}

		public IProductMakeSystem CreateProductMakeSystem(string token)
		{
			return new ProductMakeSystem(GetConfig(token));
		}

		public IProductTypeSystem CreateProductTypeSystem(string token)
		{
			return new ProductTypeSystem(GetConfig(token));
		}

		public IProductModelSystem CreateProductModelSystem(string token)
		{
			return new ProductModelSystem(GetConfig(token));
		}

		public ITaxGroupSystem CreateTaxGroupSystem(string token)
		{
			return new TaxGroupSystem(GetConfig(token));
		}

		public IUserFavoriteSystem CreateUserFavoriteSystem(string token)
		{
			return new UserFavoriteSystem(GetConfig(token));
		}

		public IPurchasePrepaymentInvoiceSystem CreatePurchasePrepaymentInvoiceSystem(string token)
		{
			return new PurchasePrepaymentInvoiceSystem(GetConfig(token));
		}

		public ISalespersonGroupSystem CreateSalespersonGroupSystem(string token)
		{
			return new SalespersonGroupSystem(GetConfig(token));
		}

		public ITaskStepsSystem CreateTaskStepsSystem(string token)
		{
			return new TaskStepsSystem(GetConfig(token));
		}

		public ITaskTypeSystem CreateTaskTypeSystem(string token)
		{
			return new TaskTypeSystem(GetConfig(token));
		}

		public ITaskTransactionSystem CreateTaskTransactionSystem(string token)
		{
			return new TaskTransactionSystem(GetConfig(token));
		}

		public ITaskTransactionStatusSystem CreateTaskTransactionStatusSystem(string token)
		{
			return new TaskTransactionStatusSystem(GetConfig(token));
		}

		public ILegalActionStatusSystem CreateLegalActionStatusSystem(string token)
		{
			return new LegalActionStatusSystem(GetConfig(token));
		}

		public ITRApplicationSystem CreateTRApplicationSystem(string token)
		{
			return new TRApplicationSystem(GetConfig(token));
		}

		public IBudgetingSystem CreateBudgetingSystem(string token)
		{
			return new BudgetingSystem(GetConfig(token));
		}

		public IVehicleMileageTrackSystem CreateVehicleMileageTrackSystem(string token)
		{
			return new VehicleMileageTrackSystem(GetConfig(token));
		}

		public ISalesManTargetSystem CreateSalesManTargetSystem(string token)
		{
			return new SalesManTargetSystem(GetConfig(token));
		}

		public ICustomerInsuranceClaimSystem CreateCustomerInsuranceClaimSystem(string token)
		{
			return new CustomerInsuranceClaimSystem(GetConfig(token));
		}

		public IPropertyDocumentSystem CreatePropertyDocumentSystem(string token)
		{
			return new PropertyDocumentSystem(GetConfig(token));
		}

		public IPropertyTenantDocumentSystem CreatePropertyTenantDocumentSystem(string token)
		{
			return new PropertyTenantDocumentSystem(GetConfig(token));
		}

		public IPropertyDocTypeSystem CreatePropertyDocTypeSystem(string token)
		{
			return new PropertyDocTypeSystem(GetConfig(token));
		}

		public IRecurringInvoiceSystem CreateRecurringInvoiceSystem(string token)
		{
			return new RecurringInvoiceSystem(GetConfig(token));
		}

		public IPropertyTenantDocTypeSystem CreatePropertyTenantDocTypeSystem(string token)
		{
			return new PropertyTenantDocTypeSystem(GetConfig(token));
		}

		public ILoanEntrySystem CreateLoanEntrySystem(string token)
		{
			return new LoanEntrySystem(GetConfig(token));
		}

		public IPrintTemplateMapSystem CreatePrintTemplateMapSystem(string token)
		{
			return new PrintTemplateMapSystem(GetConfig(token));
		}

		public IDataSyncSystem CreateDataSyncSystem(string token)
		{
			return new DataSyncSystem(GetConfig(token));
		}

		public IPatientSystem CreatePatientSystem(string token)
		{
			return new PatientSystem(GetConfig(token));
		}

		public IControlLayoutSystem CreateControlLayoutSystem(string token)
		{
			return new ControlLayoutSystem(GetConfig(token));
		}

		public ICustomListSystem CreateCustomListSystem(string token)
		{
			return new CustomListSystem(GetConfig(token));
		}
	}
}
