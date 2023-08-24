using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Interfaces.WS;
using Micromind.Common.Libraries;
using Micromind.Facade.Factories;
using Micromind.Securities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters;


namespace Micromind.ClientLibraries
{
	
	public sealed class Factory
	{
		public static SortedList<string, string> BackgroundWorkerLogins = new SortedList<string, string>();

		private static string loginAppName = null;

		private static string loginDBName = null;

		private static string loginUserID = null;

		private static string loginPassword = null;

		private static string loginToken = null;

		private static string workerLoginToken = null;

		private static ICompanySystem companySystem = null;

		private static string channelName = "5324723";

		private static DateTime lastTimeConnected = DateTime.Now;

		private static object syncRoot = new object();

		public static string LoginToken => loginToken;

		private static string ID => "D38E3CAB-5C9B-4808-AEF7-8DCE167061B8";

		private static string WorkerLoginToken => workerLoginToken;

		private static ICompanySystem CompanySystem
		{
			get
			{
				try
				{
					if (Factory.OnConnecting != null)
					{
						Factory.OnConnecting(true, null);
					}
					if (companySystem == null)
					{
						lock (syncRoot)
						{
							if (companySystem == null)
							{
								if (!GlobalRules.IsMultiUser)
								{
									companySystem = new CompanySystem();
								}
								else
								{
									string text = "CompanySystem";
									string text2 = Global.CurrentServerName;
									if (text2.IndexOf("\\") >= 0)
									{
										text2 = text2.Substring(0, text2.IndexOf("\\"));
									}
									string url = "tcp://" + text2 + ":" + Global.CurrentPortNumber + "/" + text;
									IChannel[] registeredChannels = ChannelServices.RegisteredChannels;
									foreach (IChannel channel in registeredChannels)
									{
										if (channel.ChannelName == channelName)
										{
											ChannelServices.UnregisterChannel(channel);
										}
									}
									ChannelServices.RegisterChannel(new TcpChannel(new Hashtable
									{
										[(object)"name"] = channelName,
										[(object)"port"] = 0,
										[(object)"typeFilterLevel"] = TypeFilterLevel.Full
									}, new BinaryClientFormatterSinkProvider(), null)
									{
										IsSecured = false
									}, ensureSecurity: true);
									companySystem = (ICompanySystem)Activator.GetObject(typeof(ICompanySystem), url);
									ILease lease = (ILease)RemotingServices.GetLifetimeService((MarshalByRefObject)companySystem);
									ClientSponsor clientSponsor = new ClientSponsor();
									clientSponsor.RenewalTime = new TimeSpan(10, 10, 10, 0, 0);
									lease.Register(clientSponsor);
									clientSponsor.Renewal(lease);
								}
							}
						}
					}
				}
				catch (SocketException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
				if (companySystem == null)
				{
					throw new ApplicationException("No connection could be established. Please make sure the correct server name and port number are specified, and you have entered a correct user ID and password.");
				}
				return companySystem;
			}
		}

		public static bool IsConnected
		{
			get
			{
				try
				{
					if (loginToken == null || companySystem == null)
					{
						return false;
					}
					return companySystem.IsConnected(loginToken);
				}
				catch (SocketException)
				{
					return false;
				}
				catch (Exception)
				{
					return false;
				}
			}
		}

		public static bool IsDBConnected
		{
			get
			{
				if (Global.ConStatus == ConnectionStatus.Connected || Global.ConStatus == ConnectionStatus.SQLConnected)
				{
					return true;
				}
				return false;
			}
		}

		public static IDatabaseSystem DatabaseSystem
		{
			get
			{
				try
				{
					if (loginToken == null || loginToken == string.Empty)
					{
						SetTempLogin();
					}
					return CompanySystem.CreateDatabaseSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductSystem ProductSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IUnitSystem UnitSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateUnitSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductCategorySystem ProductCategorySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductCategorySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeSystem EmployeeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeProvisionSystem EmployeeProvisionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeProvisionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeEOSSettlementSystem EmployeeEOSSettlementSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeEOSSettlementSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICandidateSystem CandidateSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCandidateSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IShippingMethodSystem ShippingMethodSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateShippingMethodSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICompanyAccountSystem CompanyAccountSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCompanyAccountSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICompanyAccountSystem CompanyAccountSystemAsync
		{
			get
			{
				SetWorkerLogin();
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCompanyAccountSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICurrencySystem CurrencySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCurrencySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBankSystem BankSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBankSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITransactionSystem TransactionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTransactionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchaseOrderSystem PurchaseOrderSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchaseOrderSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchaseCostEntrySystem PurchaseCostEntrySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchaseCostEntrySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBillOfLadingSystem BillOfLadingSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBillOfLadingSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchaseOrderNISystem PurchaseOrderNISystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchaseOrderNISystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetPurchaseOrderSystem FixedAssetPurchaseOrderSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetPurchaseOrderSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProjectSubContractPOSystem ProjectSubContractSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProjectSubContractSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductBrandSystem ProductBrandSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductBrandSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductManufacturerSystem ProductManufacturerSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductManufacturerSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IReleaseTypeSystem ReleaseTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateReleaseTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IHorseTypeSystem HorseTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateHorseTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEquipmentCategorySystem EquipmentCategorySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEquipmentCategorySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEquipmentTypeSystem EquipmentTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEquipmentTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IRequisitionTypeSystem RequisitionTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateRequisitionTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IRequisitionSystem RequisitionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateRequisitionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IHorseSexSystem HorseSexSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateHorseSexSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILeadStatusSystem LeadStatusSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLeadStatusSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICreditLimitReviewSystem CreditLimitReviewSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCreditLimitReviewSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductStyleSystem ProductStyleSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductStyleSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductSpecificationSystem ProductSpecificationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductSpecificationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductSizeSystem ProductSizeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductSizeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductAttributeSystem ProductAttributeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductAttributeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITermSystem TermSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTermSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IAccountGroupsSystem AccountGroupsSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateAccountGroupsSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IAnalysisGroupsSystem AnalysisGroupsSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateAnalysisGroupsSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IAnalysisSystem AnalysisSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateAnalysisSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISecuritySystem SecuritySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSecuritySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICompanyInformationSystem CompanyInformationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCompanyInformationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static INoteSystem WorkerNoteSystem
		{
			get
			{
				if (workerLoginToken == null)
				{
					SetWorkerLogin();
				}
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateNoteSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductSystem ProductSystemAsync
		{
			get
			{
				SetWorkerLogin();
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJournalSystem JournalSystemAsync
		{
			get
			{
				SetWorkerLogin();
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJournalSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomerSystem CustomerSystemAsync
		{
			get
			{
				SetWorkerLogin();
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomerSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static INoteSystem NoteSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateNoteSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISettingSystem SettingSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSettingSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISettingSystem SettingSystemAsync
		{
			get
			{
				SetWorkerLogin();
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSettingSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPrintTemplateSystem PrintTemplateSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePrintTemplateSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDepartmentSystem DepartmentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDepartmentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPriceLevelSystem PriceLevelSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePriceLevelSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPaymentMethodSystem PaymentMethodSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePaymentMethodSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IActivityLogSystem ActivityLogSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateActivityLogSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILicenseSystem LicenseSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLicenseSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IScheduleSystem ScheduleSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateScheduleSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomerSystem CustomerSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomerSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICountrySystem CountrySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCountrySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomerClassSystem CustomerClassSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomerClassSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IAreaSystem AreaSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateAreaSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITransporterSystem TransporterSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTransporterSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IContainerSizeSystem ContainerSizeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateContainerSizeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IINCOSystem INCOSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateINCOSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPartySystem PartySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePartySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IAccountAnalysisDetailSystem AccountAnalysisDetailSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateAccountAnalysisDetailSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalespersonSystem SalespersonSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalespersonSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomerAddressSystem CustomerAddressSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomerAddressSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IContactSystem ContactSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateContactSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVendorClassSystem VendorClassSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateVendorClassSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVendorSystem VendorSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateVendorSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVendorAddressSystem VendorAddressSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateVendorAddressSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBuyerSystem BuyerSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBuyerSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductClassSystem ProductClassSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductClassSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IGradeSystem GradeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateGradeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISponsorSystem SponsorSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSponsorSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProvisionTypeSystem ProvisionTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProvisionTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static INationalitySystem NationalitySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateNationalitySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IReligionSystem ReligionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateReligionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDivisionSystem DivisionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDivisionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICompanyDivisionSystem CompanyDivisionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCompanyDivisionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPositionSystem PositionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePositionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeAppraisalSystem EmployeeAppraisalSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeAppraisalSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeePerformanceSystem EmployeePerformanceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeePerformanceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeDocTypeSystem EmployeeDocTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeDocTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPatientDocTypeSystem PatientDocTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePatientDocTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVehicleDocTypeSystem VehicleDocTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateVehicleDocTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDegreeSystem DegreeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDegreeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISkillSystem SkillSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSkillSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomerGroupSystem CustomerGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomerGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVendorGroupSystem VendorGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateVendorGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeGroupSystem EmployeeGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeAddressSystem EmployeeAddressSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeAddressSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILocationSystem LocationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLocationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IWorkLocationSystem WorkLocationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateWorkLocationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeDependentSystem EmployeeDependentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeDependentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeDocumentSystem EmployeeDocumentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeDocumentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPatientDocumentSystem PatientDocumentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePatientDocumentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVehicleDocumentSystem VehicleDocumentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateVehicleDocumentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeSkillSystem EmployeeSkillSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeSkillSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILeaveTypeSystem LeaveTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLeaveTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobTypeSystem JobTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobTaskGroupSystem JobTaskGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobTaskGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IServiceActivitySystem ServiceActivitySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateServiceActivitySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICitySystem CitySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCitySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVehicleSystem VehicleSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateVehicleSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICostCategorySystem CostCategorySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCostCategorySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEquipmentSystem EquipmentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEquipmentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPayrollItemSystem PayrollItemSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePayrollItemSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDeductionSystem DeductionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDeductionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBenefitSystem BenefitSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBenefitSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeBenefitDetailSystem EmployeeBenefitDetailSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeBenefitDetailSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeePayrollItemDetailSystem EmployeePayrollItemDetailSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeePayrollItemDetailSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeDeductionDetailSystem EmployeeDeductionDetailSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeDeductionDetailSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDestinationSystem DestinationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDestinationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeLeaveDetailSystem EmployeeLeaveDetailSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeLeaveDetailSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeLeaveProcessSystem EmployeeLeaveProcessSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeLeaveProcessSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICompanyDocTypeSystem CompanyDocTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCompanyDocTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICompanyDocumentSystem CompanyDocumentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCompanyDocumentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITenancyContractSystem TenancyContractSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTenancyContractSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITradeLicenseSystem TradeLicenseSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTradeLicenseSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVisaSystem VisaSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateVisaSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJournalSystem JournalSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJournalSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IStandingJournalSystem StandingJournalSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateStandingJournalSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISystemDocumentSystem SystemDocumentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSystemDocumentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICostCenterSystem CostCenterSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCostCenterSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IRegisterSystem RegisterSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateRegisterSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IChequebookSystem ChequebookSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateChequebookSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IIssuedChequeSystem IssuedChequeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateIssuedChequeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IIssuedChequeSystem IssuedChequeSystemAsync
		{
			get
			{
				SetWorkerLogin();
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateIssuedChequeSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IReceivedChequeSystem ReceivedChequeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateReceivedChequeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IReceivedChequeSystem ReceivedChequeSystemAsync
		{
			get
			{
				SetWorkerLogin();
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateReceivedChequeSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IReturnedChequeReasonSystem ReturnedChequeReasonSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateReturnedChequeReasonSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IAdjustmentTypeSystem AdjustmentTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateAdjustmentTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IInventoryAdjustmentSystem InventoryAdjustmentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateInventoryAdjustmentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IInventoryDamageSystem InventoryDamageSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateInventoryDamageSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IInventoryTransferSystem InventoryTransferSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateInventoryTransferSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDriverSystem DriverSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDriverSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesQuoteSystem SalesQuoteSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesQuoteSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPriceListSystem PriceListSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePriceListSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFreightChargeSystem FreightChargeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFreightChargeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesOrderSystem SalesOrderSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesOrderSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesEnquirySystem SalesEnquirySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesEnquirySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesForecastingSystem SalesForecastingSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesForecastingSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesProformaSystem SalesProformaSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesProformaSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDeliveryNoteSystem DeliveryNoteSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDeliveryNoteSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IItemTransactionSystem ItemTransactionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateItemTransactionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IExportPickListSystem ExportPickListSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateExportPickListSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesInvoiceSystem SalesInvoiceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesInvoiceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesInvoiceNISystem SalesInvoiceNISystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesInvoiceNISystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobInvoiceSystem JobInvoiceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobInvoiceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesReceiptSystem SalesReceiptSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesReceiptSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesReturnSystem SalesReturnSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesReturnSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILPOReceiptSystem LPOReceiptSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLPOReceiptSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDeliveryReturnSystem DeliveryReturnSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDeliveryReturnSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchaseQuoteSystem PurchaseQuoteSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchaseQuoteSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPOShipmentSystem POShipmentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePOShipmentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IContainerTrackingSystem ContainerStrackingSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateContainerTrackingSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPortSystem PortSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePortSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchaseInvoiceSystem PurchaseInvoiceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchaseInvoiceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchaseInvoiceNISystem PurchaseInvoiceNISystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchaseInvoiceNISystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchaseReceiptSystem PurchaseReceiptSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchaseReceiptSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchaseReturnSystem PurchaseReturnSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchaseReturnSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IShortcutSystem ShortcutSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateShortcutSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeActivitySystem EmployeeActivitySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeActivitySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDisciplineActionTypeSystem DisciplineActionTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDisciplineActionTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeActivityTypeSystem EmployeeActivityTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeActivityTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalarySheetSystem SalarySheetSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalarySheetSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeTypeSystem EmployeeTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPayrollTransactionSystem PayrollTransactionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePayrollTransactionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProjectExpenseAllocationSystem ProjectExpenseAllocationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProjectExpenseAllocationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeLoanSystem EmployeeLoanSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeLoanSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmployeeLoanTypeSystem EmployeeLoanTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmployeeLoanTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IARJournalSystem ARJournalSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateARJournalSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IAPJournalSystem APJournalSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateAPJournalSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IUserSystem UserSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateUserSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IUserGroupSystem UserGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateUserGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IReminderSystem ReminderSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateReminderSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICompanyAddressSystem CompanyAddressSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCompanyAddressSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISmartListSystem SmartListSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSmartListSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IExternalReportSystem ExternalReportSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateExternalReportSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesPOSSystem SalesPOSSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesPOSSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPOSBatchSystem POSBatchSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePOSBatchSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPOSShiftSystem POSShiftSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePOSShiftSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPOSCashRegisterSystem POSCashRegisterSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePOSCashRegisterSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPOSHoldSystem POSHoldSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePOSHoldSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IConsignOutSystem ConsignOutSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateConsignOutSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IGarmentRentalSystem GarmentRentalSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateGarmentRentalSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IGarmentRentalReturnSystem GarmentRentalReturnSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateGarmentRentalReturnSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IConsignOutSettlementSystem ConsignOutSettlementSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateConsignOutSettlementSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IExpenseCodeSystem ExpenseCodeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateExpenseCodeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomerCategorySystem CustomerCategorySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomerCategorySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEntityCategorySystem EntityCategorySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEntityCategorySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyCategorySystem PropertyCategorySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyCategorySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICompanyOptionSystem CompanyOptionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCompanyOptionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IConsignOutReturnSystem ConsignOutReturnSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateConsignOutReturnSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IConsignInReturnSystem ConsignInReturnSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateConsignInReturnSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IConsignInSystem ConsignInSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateConsignInSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IConsignInSettlementSystem ConsignInSettlementSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateConsignInSettlementSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetSystem FixedAssetSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetLocationSystem FixedAssetLocationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetLocationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetGroupSystem FixedAssetGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetClassSystem FixedAssetClassSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetClassSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetPurchaseSystem FixedAssetPurchaseSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetPurchaseSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetBulkPurchaseSystem FixedAssetBulkPurchaseSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetBulkPurchaseSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetTransferSystem FixedAssetTransferSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetTransferSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetSaleSystem FixedAssetSaleSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetSaleSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFixedAssetDepSystem FixedAssetDepSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFixedAssetDepSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDimensionSystem DimensionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDimensionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductParentSystem ProductParentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductParentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IMatrixTemplateSystem MatrixTemplateSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateMatrixTemplateSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFiscalYearSystem FiscalYearSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFiscalYearSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILeadSystem LeadSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLeadSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILeadAddressSystem LeadAddressSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLeadAddressSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IGenericListSystem GenericListSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateGenericListSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IUDFSystem UDFSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateUDFSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPivotGroupSystem PivotGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePivotGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPivotSystem PivotSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePivotSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IAssemblyBuildSystem AssemblyBuildSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateAssemblyBuildSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductionSystem ProductionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IInventoryRepackingSystem InventoryRepackingSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateInventoryRepackingSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IWorkOrderSystem WorkOrderSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateWorkOrderSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyRentSystem PropertyRentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyRentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyCancelSystem PropertyCancelSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyCancelSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyServiceSystem PropertyServiceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyServiceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBOMSystem BOMSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBOMSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobBOMSystem JobBOMSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobBOMSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEOSRuleSystem EOSRuleSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEOSRuleSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IOverTimeSystem OverTimeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateOverTimeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalarySystem SalarySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalarySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEntityDocSystem EntityDocSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEntityDocSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobSystem JobSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IOpportunitySystem OpportunitySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateOpportunitySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICompetitorSystem CompetitorSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCompetitorSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IActivitySystem ActivitySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateActivitySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IFollowupSystem FollowupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateFollowupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBankReconciliationSystem BankReconciliationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBankReconciliationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomReportSystem CustomReportSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomReportSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICampaignSystem CampaignSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCampaignSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEventSystem EventSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEventSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobInventoryIssueSystem JobInventoryIssueSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobInventoryIssueSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobEstimationSystem JobEstimationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobEstimationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobMaterialRequisitionSystem JobMaterialRequisitionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobMaterialRequisitionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IServiceCallTrackSystem ServiceCallTrackSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateServiceCallTrackSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobMaterialEstimateSystem JobMaterialEstimateSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobMaterialEstimateSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobManHrsBudgetingSystem JobManHrsBudgetingSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobManHrsBudgetingSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobMaintenanceScheduleSystem JobMaintenanceScheduleSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobMaintenanceScheduleSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobMaintenanceServiceSystem JobMaintenanceServiceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobMaintenanceServiceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobInventoryReturnSystem JobInventoryReturnSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobInventoryReturnSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobExpenseIssueSystem JobExpenseIssueSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobExpenseIssueSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobTimesheetSystem JobTimesheetSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobTimesheetSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IOverTimeEntrySystem OverTimeEntrySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateOverTimeEntrySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IHolidayCalendarSystem HolidayCalendarSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateHolidayCalendarSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBankFacilityGroupSystem BankFacilityGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBankFacilityGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBankFacilitySystem BankFacilitySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBankFacilitySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IOpeningBalanceBatchSystem OpeningBalanceBatchSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateOpeningBalanceBatchSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPhysicalStockEntrySystem PhysicalStockEntrySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePhysicalStockEntrySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IOpeningBalanceLeaveSystem OpeningBalanceLeaveSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateOpeningBalanceLeaveSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IOpeningEntryTransactionSystem OpeningEntryTransactionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateOpeningEntryTransactionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBankFacilityTransactionSystem BankFacilityTransactionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBankFacilityTransactionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBankFacilityPaymentSystem BankFacilityPaymentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBankFacilityPaymentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomGadgetSystem CustomGadgetSystemAsync
		{
			get
			{
				SetWorkerLogin();
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomGadgetSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomGadgetSystem CustomGadgetSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomGadgetSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISendChequeSystem SendChequeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSendChequeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDiscountChequeSystem DiscountChequeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDiscountChequeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDiscountBillSystem DiscountBillSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDiscountBillSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IInventoryTransferTypeSystem InventoryTransferTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateInventoryTransferTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IExportPackingListSystem ExportPackingListSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateExportPackingListSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDashboardSystem DashboardSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDashboardSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDashboardSystem DashboardSystemAsync
		{
			get
			{
				SetWorkerLogin();
				if (workerLoginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDashboardSystem(workerLoginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IWebDashboardSystem WebDashboardSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateWebDashboardSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICollateralSystem CollateralSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCollateralSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IJobTaskSystem JobTaskSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateJobTaskSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IGRNReturnSystem GRNReturnSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateGRNReturnSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IArrivalReportSystem ArrivalReportSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateArrivalReportSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IQualityClaimSystem QualityClaimSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateQualityClaimSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IArrivalReportTemplateSystem ArrivalReportTemplateSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateArrivalReportTemplateSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IQualityTaskSystem QualityTaskSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateQualityTaskSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISurveyorSystem SurveyorSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSurveyorSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICheckListSystem CheckListSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCheckListSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IApprovalSystem ApprovalSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateApprovalSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyAgentSystem PropertyAgentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyAgentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyClassSystem PropertyClassSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyClassSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertySystem PropertySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyUnitSystem PropertyUnitSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyUnitSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyVirtualUnitSystem PropertyVirtualUnitSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyVirtualUnitSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyIncomeCodeSystem PropertyIncomeCodeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyIncomeCodeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IRentalPostingSystem RentalPostingSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateRentalPostingSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPaymentRequestSystem PaymentRequestSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePaymentRequestSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchaseClaimSystem PurchaseClaimSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchaseClaimSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IW3PLGRNSystem W3PLGRNSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateW3PLGRNSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IW3PLDeliverySystem W3PLDeliverySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateW3PLDeliverySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IW3PLInvoiceSystem W3PLInvoiceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateW3PLInvoiceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEmailMessageSystem EmailMessageSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEmailMessageSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICLVoucherSystem CLVoucherSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCLVoucherSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICLTokenSystem CLTokenSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCLTokenSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IClientAssetSystem ClientAssetSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateClientAssetSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IRouteSystem RouteSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateRouteSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IRouteGroupSystem RouteGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateRouteGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBinSystem BinSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBinSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IRackSystem RackSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateRackSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IServiceTypeSystem ServiceTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateServiceTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IMaintenanceSchedulerSystem MaintenanceSchedulerSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateMaintenanceSchedulerSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVehicleMaintenanceEntrySystem MaintenanceEntrySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateMaintenanceEntrySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEntityCommentSystem EntityCommentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEntityCommentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IInsuranceProviderSystem InsuranceProviderSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateInsuranceProviderSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomerInsuranceSystem CustomerInsuranceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomerInsuranceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IRiderSummarySystem RiderSummarySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateRiderSummarySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IHorseSummarySystem HorseSummarySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateHorseSummarySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProjectSubContractPISystem ProjectSubContractPISystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProjectSubContractPISystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEntityFlagSystem EntityFlagSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEntityFlagSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEAEquipmentSystem EAEquipmentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEAEquipmentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IMobilizationSystem MobilizationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateMobilizationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEquipmentTransferSystem EquipmentTransferSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEquipmentTransferSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IEquipmentWorkOrderSystem EquipmentWorkOrderSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateEquipmentWorkOrderSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILawyerSystem LawyerSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLawyerSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICasePartySystem CasePartySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCasePartySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IWorkOrderInventoryIssueSystem WorkOrderInventoryIssueSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateWorkOrderInventoryIssueSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILegalActivitySystem LegalActivitySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLegalActivitySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILegalActionSystem LegalActionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLegalActionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IWorkOrderInventoryReturnSystem WorkOrderInventoryReturnSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateWorkOrderInventoryReturnSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITaxSystem TaxSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTaxSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITaxGroupSystem TaxGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTaxGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IInventoryDismantleSystem InventoryDismantleSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateInventoryDismantleSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductPriceBulkUpdateSystem ProductPriceBulkpdateSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductPriceBulkpdateSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IMaterialReservationSystem MaterialReservationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateMaterialReservationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICaseClientSystem CaseClientSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCaseClientSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductMakeSystem ProductMakeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductMakeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductTypeSystem ProductTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IProductModelSystem ProductModelSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateProductModelSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IUserFavoriteSystem UserFavoriteSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateUserFavoriteSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPurchasePrepaymentInvoiceSystem PurchasePrepaymentInvoiceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePurchasePrepaymentInvoiceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalespersonGroupSystem SalespersonGroupSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalespersonGroupSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITaskStepsSystem TaskStepsSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTaskStepsSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITaskTypeSystem TaskTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTaskTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITaskTransactionSystem TaskTransactionSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTaskTransactionSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITaskTransactionStatusSystem TaskTransactionStatusSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTaskTransactionStatusSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILegalActionStatusSystem LegalActionStatusSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLegalActionStatusSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ITRApplicationSystem TRApplicationSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateTRApplicationSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IBudgetingSystem BudgetingSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateBudgetingSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IVehicleMileageTrackSystem VehicleMileageTrackSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateVehicleMileageTrackSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ISalesManTargetSystem SalesManTargetSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateSalesManTargetSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomerInsuranceClaimSystem CustomerInsuranceClaimSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomerInsuranceClaimSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyDocTypeSystem PropertyDocTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyDocTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IRecurringInvoiceSystem RecurringInvoiceSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateRecurringInvoiceSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyDocumentSystem PropertyDocumentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyDocumentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyTenantDocumentSystem PropertyTenantDocumentSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyTenantDocumentSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPropertyTenantDocTypeSystem PropertyTenantDocTypeSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePropertyTenantDocTypeSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ILoanEntrySystem LoanEntrySystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateLoanEntrySystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPrintTemplateMapSystem PrintTemplateMapSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePrintTemplateMapSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IDataSyncSystem DataSyncSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateDataSyncSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IPatientSystem PatientSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreatePatientSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static IControlLayoutSystem ControlLayoutSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateControlLayoutSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static ICustomListSystem CustomListSystem
		{
			get
			{
				if (loginToken == null)
				{
					throw new DBNotConnectedException();
				}
				try
				{
					return CompanySystem.CreateCustomListSystem(loginToken);
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
			}
		}

		public static event EventHandler OnConnecting;

		private Factory()
		{
		}

		private static void Reset()
		{
			loginAppName = null;
			loginDBName = null;
			loginUserID = null;
			loginPassword = null;
			loginToken = null;
			workerLoginToken = null;
			companySystem = null;
		}

		public static void Dispose()
		{
		}

		private static void CheckConnection()
		{
			if (Math.Abs((DateTime.Now - lastTimeConnected).TotalMinutes) > 10.0 && companySystem != null && loginToken != null && loginToken != null && companySystem != null && !companySystem.IsConnected(loginToken))
			{
				SetLoginInfo(loginAppName, loginDBName, loginUserID, loginPassword);
			}
			lastTimeConnected = DateTime.Now;
		}

		public static void DisconnectDB()
		{
			try
			{
				if (IsDBConnected)
				{
					Global.ConStatus = ConnectionStatus.DisConnected;
					if (loginToken != null)
					{
						CompanySystem.RemoveWorkerTokens(loginToken, Global.CurrentUser, Global.ComputerName);
						CompanySystem.Disconnect(loginToken);
						loginToken = null;
					}
					if (workerLoginToken != null)
					{
						CompanySystem.Disconnect(workerLoginToken);
						workerLoginToken = null;
					}
					CompanySystem.RemoveWorkerTokens(loginToken, Global.CurrentUser, Global.ComputerName);
					Reset();
				}
				else
				{
					loginToken = null;
					workerLoginToken = null;
				}
			}
			catch
			{
				loginToken = null;
			}
		}

		public static void RemoveWorkerLogins()
		{
			try
			{
				if (IsDBConnected && loginToken != null)
				{
					CompanySystem.RemoveWorkerTokens(loginToken, Global.CurrentUser, Environment.MachineName);
				}
			}
			catch
			{
			}
		}

		public static ActivityData GetActiveUsers()
		{
			return CompanySystem.GetActiveUsers();
		}

		public static bool RemoveUser(string userID, string machineID, string dbName)
		{
			return CompanySystem.RemoveUser(LoginToken, userID, machineID, dbName);
		}

		private static bool LogClientSignIn()
		{
			return CompanySystem.LogClientSignIn(loginToken);
		}

		public static bool SetLoginInfo(string appName, string dbName, string userID, string password)
		{
			return SetLoginInfo(suppressMessage: false, appName, dbName, userID, password);
		}

		public static string Encrypt(string data)
		{
			return new ConfigHelper(ID).Cryptor.Encrypt(data);
		}

		public static string Decrypt(string data)
		{
			return new ConfigHelper(ID).Cryptor.Decrypt(data);
		}

		private static string GetExternalAdminPass()
		{
			return Encrypt(Global.ExternalDBUserPassword);
		}

		public static bool SetLoginInfo(bool suppressMessage, string appName, string dbName, string userID, string password)
		{
			DisconnectDB();
			if (dbName == null || dbName.Trim() == string.Empty || userID == null || userID.Trim() == string.Empty)
			{
				return false;
			}
			if (Global.IsMultiUser && !GlobalRules.IsCorrectServerName(Global.CurrentServerName))
			{
				throw new ApplicationException("Incorrect server name.");
			}
			if (!GlobalRules.IsCorrectServerName(Global.CurrentInstanceName))
			{
				throw new ApplicationException("Incorrect instance name.");
			}
			loginAppName = appName;
			loginDBName = dbName;
			loginUserID = userID;
			if (userID.ToLower().Trim() != "sa")
			{
				password = Encrypt(CommonLib.GetAxolonPassword(ID, password, isEncrypted: true));
			}
			loginPassword = password;
			lastTimeConnected = DateTime.MaxValue;
			bool flag = true;
			if (loginToken != null)
			{
				flag = false;
			}
			loginToken = null;
			try
			{
				ConnectionTypes connectionType = ConnectionTypes.LocalServer;
				loginToken = CompanySystem.SetLoginInfo(appName, Environment.MachineName, Global.CurrentInstanceName, Global.GetSystemID(), Global.GetProductKey(), dbName, userID, password, Global.ExternalDBUserName, GetExternalAdminPass(), ID, connectionType);
				if (loginToken != null)
				{
					if (dbName.ToLower() == "master")
					{
						Global.ConStatus = ConnectionStatus.SQLConnected;
					}
					else
					{
						Global.ConStatus = ConnectionStatus.Connected;
					}
					if (flag)
					{
						LogClientSignIn();
					}
				}
				lastTimeConnected = DateTime.Now;
			}
			catch (SocketException ex)
			{
				if (!suppressMessage)
				{
					throw;
				}
				if (Global.IsAxolonAgent)
				{
					throw ex;
				}
			}
			catch (CompanyException ex2)
			{
				_ = ex2.Number;
				_ = 1024;
				throw;
			}
			catch (SqlException ex3)
			{
				if (!suppressMessage)
				{
					throw;
				}
				if (Global.IsAxolonAgent)
				{
					throw ex3;
				}
			}
			catch (Exception ex4)
			{
				if (!suppressMessage)
				{
					throw;
				}
				if (Global.IsAxolonAgent)
				{
					throw ex4;
				}
			}
			return IsDBConnected;
		}

		private static void SetTempLogin()
		{
			if (loginToken == null || loginToken == string.Empty)
			{
				loginToken = CompanySystem.CreateEmptyLogin("", Environment.MachineName, "", Global.GetSystemID(), Global.GetProductKey(), ID);
			}
		}

		public static DataSet GetDatabases(string instanceName)
		{
			try
			{
				if (loginToken == null || loginToken == string.Empty)
				{
					SetTempLogin();
				}
				return CompanySystem.CreateDatabaseSystem(loginToken).GetDatabases(Global.CurrentInstanceName, Global.CurrentUser, Global.CurrentPassword);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static bool IsDBExist(string instanceName, string dbName, string userName, string password)
		{
			try
			{
				if (loginToken == null || loginToken == string.Empty)
				{
					SetTempLogin();
				}
				return CompanySystem.CreateDatabaseSystem(loginToken).IsDBExist(dbName);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		private static void SetWorkerLogin()
		{
			try
			{
				ConnectionTypes connectionType = ConnectionTypes.LocalServer;
				workerLoginToken = CompanySystem.SetLoginInfo(loginAppName + " Background", Environment.MachineName, Global.CurrentInstanceName, Global.GetSystemID(), Global.GetProductKey(), loginDBName, loginUserID, loginPassword, Global.ExternalDBUserName, GetExternalAdminPass(), ID, connectionType, async: true);
			}
			catch
			{
				throw;
			}
		}

		static Factory()
		{
			Factory.OnConnecting = null;
		}
	}
}
