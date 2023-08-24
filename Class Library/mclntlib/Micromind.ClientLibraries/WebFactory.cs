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
	
	public sealed class WebFactory
	{
		public static SortedList<string, string> BackgroundWorkerLogins = new SortedList<string, string>();

		private static string loginAppName = null;

		private static string loginDBName = null;

		private static string loginUserID = null;

		private static string loginPassword = null;

		private static string loginToken = null;

		private static string workerLoginToken = null;

		private static IWebCompanySystem companySystem = null;

		private static string channelName = "5324723";

		private static DateTime lastTimeConnected = DateTime.Now;

		private static object syncRoot = new object();

		public static string ID => "D38E3CAB-5C9B-4808-AEF7-8DCE167061B8";

		public static string LoginToken => loginToken;

		private static string WorkerLoginToken => workerLoginToken;

		private static IWebCompanySystem WebCompanySystem
		{
			get
			{
				try
				{
					if (WebFactory.OnConnecting != null)
					{
						WebFactory.OnConnecting(true, null);
					}
					if (companySystem == null)
					{
						lock (syncRoot)
						{
							if (companySystem == null)
							{
								if (!GlobalRules.IsMultiUser)
								{
									companySystem = new WebCompanySystem();
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
									companySystem = (IWebCompanySystem)Activator.GetObject(typeof(IWebCompanySystem), url);
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

		public static event EventHandler OnConnecting;

		private WebFactory()
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

		private static void CheckConnection(RequestInfo requestInfo)
		{
			if (Math.Abs((DateTime.Now - lastTimeConnected).TotalMinutes) > 10.0 && companySystem != null && requestInfo != null && requestInfo != null && companySystem != null && !companySystem.IsConnected(requestInfo))
			{
				SetLoginInfo(requestInfo);
			}
			lastTimeConnected = DateTime.Now;
		}

		public static bool IsConnected(RequestInfo requestInfo)
		{
			try
			{
				if (requestInfo == null || companySystem == null)
				{
					return false;
				}
				return companySystem.IsConnected(requestInfo);
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

		public static void DisconnectDB(RequestInfo requestInfo)
		{
			try
			{
				if (IsDBConnected)
				{
					Global.ConStatus = ConnectionStatus.DisConnected;
					if (requestInfo != null)
					{
						WebCompanySystem.RemoveWorkerTokens(requestInfo);
						WebCompanySystem.Disconnect(requestInfo);
						loginToken = null;
					}
					if (workerLoginToken != null)
					{
						WebCompanySystem.Disconnect(requestInfo);
						workerLoginToken = null;
					}
					WebCompanySystem.RemoveWorkerTokens(requestInfo);
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

		public static void RemoveWorkerLogins(RequestInfo requestInfo)
		{
			try
			{
				if (IsDBConnected && requestInfo != null)
				{
					WebCompanySystem.RemoveWorkerTokens(requestInfo);
				}
			}
			catch
			{
			}
		}

		public static ActivityData GetActiveUsers()
		{
			return WebCompanySystem.GetActiveUsers();
		}

		private static bool LogClientSignIn(RequestInfo requestInfo)
		{
			return WebCompanySystem.LogClientSignIn(requestInfo);
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

		public static bool SetLoginInfo(RequestInfo requestInfo)
		{
			DisconnectDB(requestInfo);
			if (requestInfo.DBName == null || requestInfo.DBName.Trim() == string.Empty || requestInfo.UserID == null || requestInfo.UserID.Trim() == string.Empty)
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
			bool flag = true;
			if (requestInfo != null)
			{
				flag = false;
			}
			requestInfo.LoginToken = null;
			try
			{
				requestInfo.adminUserName = Global.ExternalDBUserName;
				requestInfo.adminUserPassword = GetExternalAdminPass();
				loginToken = WebCompanySystem.SetLoginInfo(requestInfo);
				if (requestInfo != null)
				{
					if (requestInfo.UserID.ToLower() == "master")
					{
						Global.ConStatus = ConnectionStatus.SQLConnected;
					}
					else
					{
						Global.ConStatus = ConnectionStatus.Connected;
					}
					if (flag)
					{
						LogClientSignIn(requestInfo);
					}
				}
				lastTimeConnected = DateTime.Now;
			}
			catch (SocketException ex)
			{
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
				if (Global.IsAxolonAgent)
				{
					throw ex3;
				}
			}
			catch (Exception ex4)
			{
				if (Global.IsAxolonAgent)
				{
					throw ex4;
				}
			}
			return IsDBConnected;
		}

		private static void SetTempLogin(RequestInfo requestInfo)
		{
			if (requestInfo == null || requestInfo.UserID == string.Empty)
			{
				WebCompanySystem.CreateEmptyLogin(requestInfo);
			}
		}

		public static DataSet GetDatabases(RequestInfo requestInfo)
		{
			_ = requestInfo.InstanceName;
			try
			{
				if (requestInfo == null || requestInfo.UserID == string.Empty)
				{
					SetTempLogin(requestInfo);
				}
				return WebCompanySystem.CreateDatabaseSystem(requestInfo).GetDatabases(Global.CurrentInstanceName, requestInfo.UserID, Decrypt(requestInfo.Password));
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static bool IsDBExist(RequestInfo requestInfo)
		{
			try
			{
				if (requestInfo == null || requestInfo.UserID == string.Empty)
				{
					SetTempLogin(requestInfo);
				}
				return WebCompanySystem.CreateDatabaseSystem(requestInfo).IsDBExist(requestInfo.DBName);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDatabaseSystem GetDatabaseSystem(RequestInfo requestInfo)
		{
			try
			{
				if (requestInfo == null || requestInfo.UserID == string.Empty)
				{
					SetTempLogin(requestInfo);
				}
				return WebCompanySystem.CreateDatabaseSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductSystem GetProductSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IUnitSystem GetUnitSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateUnitSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductCategorySystem GetProductCategorySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductCategorySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeSystem GetEmployeeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeProvisionSystem GetEmployeeProvisionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeProvisionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeEOSSettlementSystem GetEmployeeEOSSettlementSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeEOSSettlementSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICandidateSystem GetCandidateSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCandidateSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IShippingMethodSystem GetShippingMethodSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateShippingMethodSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICompanyAccountSystem GetCompanyAccountSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCompanyAccountSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICompanyAccountSystem GetCompanyAccountSystemAsync(RequestInfo requestInfo)
		{
			SetWorkerLogin(requestInfo);
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCompanyAccountSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICurrencySystem GetCurrencySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCurrencySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBankSystem GetBankSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBankSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITransactionSystem GetTransactionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTransactionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchaseOrderSystem GetPurchaseOrderSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchaseOrderSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchaseCostEntrySystem GetGetPurchaseCostEntrySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchaseCostEntrySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBillOfLadingSystem GetGetBillOfLadingSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBillOfLadingSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchaseOrderNISystem GetGetPurchaseOrderNISystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchaseOrderNISystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetPurchaseOrderSystem GetFixedAssetPurchaseOrderSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetPurchaseOrderSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProjectSubContractPOSystem GetProjectSubContractSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProjectSubContractSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductBrandSystem GetProductBrandSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductBrandSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductManufacturerSystem GetProductManufacturerSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductManufacturerSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IReleaseTypeSystem GetReleaseTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateReleaseTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IHorseTypeSystem GetHorseTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateHorseTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEquipmentCategorySystem GetEquipmentCategorySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEquipmentCategorySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEquipmentTypeSystem GetEquipmentTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEquipmentTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IRequisitionTypeSystem GetRequisitionTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateRequisitionTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IRequisitionSystem GetRequisitionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateRequisitionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IHorseSexSystem GetHorseSexSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateHorseSexSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILeadStatusSystem GetLeadStatusSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLeadStatusSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICreditLimitReviewSystem GetCreditLimitReviewSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCreditLimitReviewSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductStyleSystem GetProductStyleSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductStyleSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductSpecificationSystem GetProductSpecificationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductSpecificationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductSizeSystem GetProductSizeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductSizeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductAttributeSystem GetProductAttributeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductAttributeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITermSystem GetTermSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTermSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IAccountGroupsSystem GetAccountGroupsSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateAccountGroupsSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IAnalysisGroupsSystem GetAnalysisGroupsSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateAnalysisGroupsSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IAnalysisSystem GetAnalysisSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateAnalysisSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISecuritySystem GetSecuritySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSecuritySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICompanyInformationSystem GetCompanyInformationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCompanyInformationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		private static void SetWorkerLogin(RequestInfo requestInfo)
		{
			try
			{
				requestInfo.ConnectionType = ConnectionTypes.LocalServer;
				workerLoginToken = WebCompanySystem.SetLoginInfo(requestInfo, async: true);
			}
			catch
			{
				throw;
			}
		}

		public static INoteSystem GetWorkerNoteSystem(RequestInfo requestInfo)
		{
			if (workerLoginToken == null)
			{
				SetWorkerLogin(requestInfo);
			}
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateNoteSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductSystem GetProductSystemAsync(RequestInfo requestInfo)
		{
			SetWorkerLogin(requestInfo);
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJournalSystem GetJournalSystemAsync(RequestInfo requestInfo)
		{
			SetWorkerLogin(requestInfo);
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJournalSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomerSystem GetCustomerSystemAsync(RequestInfo requestInfo)
		{
			SetWorkerLogin(requestInfo);
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomerSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static INoteSystem GetNoteSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateNoteSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISettingSystem GetSettingSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSettingSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISettingSystem GetSettingSystemAsync(RequestInfo requestInfo)
		{
			SetWorkerLogin(requestInfo);
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSettingSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPrintTemplateSystem GetPrintTemplateSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePrintTemplateSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDepartmentSystem GetDepartmentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDepartmentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPriceLevelSystem GetPriceLevelSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePriceLevelSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPaymentMethodSystem GetPaymentMethodSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePaymentMethodSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IActivityLogSystem GetActivityLogSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateActivityLogSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILicenseSystem GetLicenseSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLicenseSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IScheduleSystem GetScheduleSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateScheduleSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomerSystem GetCustomerSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomerSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICountrySystem GetCountrySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCountrySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomerClassSystem GetCustomerClassSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomerClassSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IAreaSystem GetAreaSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateAreaSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITransporterSystem GetTransporterSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTransporterSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IContainerSizeSystem GetContainerSizeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateContainerSizeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IINCOSystem GetINCOSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateINCOSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPartySystem GetPartySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePartySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IAccountAnalysisDetailSystem GetAccountAnalysisDetailSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateAccountAnalysisDetailSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalespersonSystem GetSalespersonSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalespersonSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomerAddressSystem GetCustomerAddressSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomerAddressSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IContactSystem GetContactSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateContactSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVendorClassSystem GetVendorClassSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateVendorClassSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVendorSystem GetVendorSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateVendorSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVendorAddressSystem GetVendorAddressSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateVendorAddressSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBuyerSystem GetBuyerSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBuyerSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductClassSystem GetProductClassSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductClassSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IGradeSystem GetGradeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateGradeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISponsorSystem GetSponsorSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSponsorSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProvisionTypeSystem GetProvisionTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProvisionTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static INationalitySystem GetNationalitySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateNationalitySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IReligionSystem GetReligionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateReligionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDivisionSystem GetDivisionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDivisionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICompanyDivisionSystem GetCompanyDivisionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCompanyDivisionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPositionSystem GetPositionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePositionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeAppraisalSystem GetEmployeeAppraisalSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeAppraisalSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeePerformanceSystem GetEmployeePerformanceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeePerformanceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeDocTypeSystem GetEmployeeDocTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeDocTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVehicleDocTypeSystem GetVehicleDocTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateVehicleDocTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDegreeSystem GetDegreeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDegreeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISkillSystem GetSkillSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSkillSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomerGroupSystem GetCustomerGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomerGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVendorGroupSystem GetVendorGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateVendorGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeGroupSystem GetEmployeeGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeAddressSystem GetEmployeeAddressSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeAddressSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILocationSystem GetLocationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLocationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IWorkLocationSystem GetWorkLocationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateWorkLocationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeDependentSystem GetEmployeeDependentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeDependentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeDocumentSystem GetEmployeeDocumentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeDocumentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVehicleDocumentSystem GetVehicleDocumentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateVehicleDocumentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeSkillSystem GetEmployeeSkillSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeSkillSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILeaveTypeSystem GetLeaveTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLeaveTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobTypeSystem GetJobTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IServiceActivitySystem GetServiceActivitySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateServiceActivitySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICitySystem GetCitySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCitySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVehicleSystem GetVehicleSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateVehicleSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICostCategorySystem GetCostCategorySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCostCategorySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEquipmentSystem GetEquipmentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEquipmentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPayrollItemSystem GetPayrollItemSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePayrollItemSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDeductionSystem GetDeductionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDeductionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBenefitSystem GetBenefitSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBenefitSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeBenefitDetailSystem GetEmployeeBenefitDetailSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeBenefitDetailSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeePayrollItemDetailSystem GetEmployeePayrollItemDetailSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeePayrollItemDetailSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeDeductionDetailSystem GetEmployeeDeductionDetailSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeDeductionDetailSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDestinationSystem GetDestinationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDestinationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeLeaveDetailSystem GetEmployeeLeaveDetailSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeLeaveDetailSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeLeaveProcessSystem GetEmployeeLeaveProcessSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeLeaveProcessSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICompanyDocTypeSystem GetCompanyDocTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCompanyDocTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICompanyDocumentSystem GetCompanyDocumentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCompanyDocumentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITenancyContractSystem GetTenancyContractSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTenancyContractSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITradeLicenseSystem GetTradeLicenseSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTradeLicenseSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVisaSystem GetVisaSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateVisaSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJournalSystem GetJournalSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJournalSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IStandingJournalSystem GetStandingJournalSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateStandingJournalSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISystemDocumentSystem GetSystemDocumentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSystemDocumentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICostCenterSystem GetCostCenterSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCostCenterSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IRegisterSystem GetRegisterSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateRegisterSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IChequebookSystem GetChequebookSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateChequebookSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IIssuedChequeSystem GetIssuedChequeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateIssuedChequeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IIssuedChequeSystem GetIssuedChequeSystemAsync(RequestInfo requestInfo)
		{
			SetWorkerLogin(requestInfo);
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateIssuedChequeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IReceivedChequeSystem GetReceivedChequeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateReceivedChequeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IReceivedChequeSystem GetReceivedChequeSystemAsync(RequestInfo requestInfo)
		{
			SetWorkerLogin(requestInfo);
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateReceivedChequeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IReturnedChequeReasonSystem GetReturnedChequeReasonSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateReturnedChequeReasonSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IAdjustmentTypeSystem GetAdjustmentTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateAdjustmentTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IInventoryAdjustmentSystem GetInventoryAdjustmentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateInventoryAdjustmentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IInventoryDamageSystem GetInventoryDamageSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateInventoryDamageSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IInventoryTransferSystem GetInventoryTransferSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateInventoryTransferSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDriverSystem GetDriverSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDriverSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesQuoteSystem GetSalesQuoteSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesQuoteSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPriceListSystem GetPriceListSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePriceListSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFreightChargeSystem GetFreightChargeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFreightChargeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesOrderSystem GetSalesOrderSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesOrderSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesEnquirySystem GetSalesEnquirySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesEnquirySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesForecastingSystem GetSalesForecastingSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesForecastingSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesProformaSystem GetSalesProformaSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesProformaSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDeliveryNoteSystem GetDeliveryNoteSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDeliveryNoteSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IItemTransactionSystem GetItemTransactionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateItemTransactionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IExportPickListSystem GetExportPickListSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateExportPickListSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesInvoiceSystem GetSalesInvoiceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesInvoiceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesInvoiceNISystem GetSalesInvoiceNISystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesInvoiceNISystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobInvoiceSystem GetJobInvoiceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobInvoiceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesReceiptSystem GetSalesReceiptSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesReceiptSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesReturnSystem GetSalesReturnSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesReturnSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILPOReceiptSystem GetLPOReceiptSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLPOReceiptSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDeliveryReturnSystem GetDeliveryReturnSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDeliveryReturnSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchaseQuoteSystem GetPurchaseQuoteSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchaseQuoteSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPOShipmentSystem GetPOShipmentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePOShipmentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IContainerTrackingSystem GetContainerStrackingSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateContainerTrackingSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPortSystem GetPortSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePortSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchaseInvoiceSystem GetPurchaseInvoiceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchaseInvoiceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchaseInvoiceNISystem GetPurchaseInvoiceNISystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchaseInvoiceNISystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchaseReceiptSystem GetPurchaseReceiptSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchaseReceiptSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchaseReturnSystem GetPurchaseReturnSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchaseReturnSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IShortcutSystem GetShortcutSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateShortcutSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeActivitySystem GetEmployeeActivitySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeActivitySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDisciplineActionTypeSystem GetDisciplineActionTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDisciplineActionTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeActivityTypeSystem GetEmployeeActivityTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeActivityTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalarySheetSystem GetSalarySheetSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalarySheetSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeTypeSystem GetEmployeeTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPayrollTransactionSystem GetPayrollTransactionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePayrollTransactionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProjectExpenseAllocationSystem GetProjectExpenseAllocationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProjectExpenseAllocationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeLoanSystem GetEmployeeLoanSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeLoanSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmployeeLoanTypeSystem GetEmployeeLoanTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmployeeLoanTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IARJournalSystem GetARJournalSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateARJournalSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IAPJournalSystem GetAPJournalSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateAPJournalSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IUserSystem GetUserSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateUserSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IUserGroupSystem GetUserGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateUserGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IReminderSystem GetReminderSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateReminderSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICompanyAddressSystem GetCompanyAddressSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCompanyAddressSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISmartListSystem GetSmartListSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSmartListSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IExternalReportSystem GetExternalReportSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateExternalReportSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesPOSSystem GetSalesPOSSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesPOSSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPOSBatchSystem GetPOSBatchSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePOSBatchSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPOSShiftSystem GetPOSShiftSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePOSShiftSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPOSCashRegisterSystem GetPOSCashRegisterSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePOSCashRegisterSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPOSHoldSystem GetPOSHoldSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePOSHoldSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IConsignOutSystem GetConsignOutSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateConsignOutSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IGarmentRentalSystem GetGarmentRentalSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateGarmentRentalSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IGarmentRentalReturnSystem GetGarmentRentalReturnSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateGarmentRentalReturnSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IConsignOutSettlementSystem GetConsignOutSettlementSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateConsignOutSettlementSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IExpenseCodeSystem GetExpenseCodeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateExpenseCodeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomerCategorySystem GetCustomerCategorySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomerCategorySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEntityCategorySystem GetEntityCategorySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEntityCategorySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyCategorySystem GetPropertyCategorySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyCategorySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICompanyOptionSystem GetCompanyOptionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCompanyOptionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IConsignOutReturnSystem GetConsignOutReturnSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateConsignOutReturnSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IConsignInReturnSystem GetConsignInReturnSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateConsignInReturnSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IConsignInSystem GetConsignInSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateConsignInSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IConsignInSettlementSystem GetConsignInSettlementSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateConsignInSettlementSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetSystem GetFixedAssetSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetLocationSystem GetFixedAssetLocationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetLocationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetGroupSystem GetFixedAssetGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetClassSystem GetFixedAssetClassSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetClassSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetPurchaseSystem GetFixedAssetPurchaseSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetPurchaseSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetBulkPurchaseSystem GetFixedAssetBulkPurchaseSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetBulkPurchaseSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetTransferSystem GetFixedAssetTransferSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetTransferSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetSaleSystem GetFixedAssetSaleSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetSaleSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFixedAssetDepSystem GetFixedAssetDepSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFixedAssetDepSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDimensionSystem GetDimensionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDimensionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductParentSystem GetProductParentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductParentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IMatrixTemplateSystem GetMatrixTemplateSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateMatrixTemplateSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFiscalYearSystem GetFiscalYearSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFiscalYearSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILeadSystem GetLeadSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLeadSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILeadAddressSystem GetLeadAddressSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLeadAddressSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IGenericListSystem GetGenericListSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateGenericListSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IUDFSystem GetUDFSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateUDFSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPivotGroupSystem GetPivotGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePivotGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPivotSystem GetPivotSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePivotSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IAssemblyBuildSystem GetAssemblyBuildSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateAssemblyBuildSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductionSystem GetProductionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IInventoryRepackingSystem GetInventoryRepackingSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateInventoryRepackingSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IWorkOrderSystem GetWorkOrderSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateWorkOrderSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyRentSystem GetPropertyRentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyRentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyCancelSystem GetPropertyCancelSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyCancelSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyServiceSystem GetPropertyServiceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyServiceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBOMSystem GetBOMSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBOMSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobBOMSystem GetJobBOMSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobBOMSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEOSRuleSystem GetEOSRuleSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEOSRuleSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IOverTimeSystem GetOverTimeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateOverTimeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalarySystem GetSalarySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalarySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEntityDocSystem GetEntityDocSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEntityDocSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobSystem GetJobSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IOpportunitySystem GetOpportunitySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateOpportunitySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICompetitorSystem GetCompetitorSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCompetitorSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IActivitySystem GetActivitySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateActivitySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IFollowupSystem GetFollowupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateFollowupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBankReconciliationSystem GetBankReconciliationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBankReconciliationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomReportSystem GetCustomReportSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomReportSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICampaignSystem GetCampaignSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCampaignSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEventSystem GetEventSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEventSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobInventoryIssueSystem GetJobInventoryIssueSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobInventoryIssueSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobEstimationSystem GetJobEstimationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobEstimationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobMaterialRequisitionSystem GetJobMaterialRequisitionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobMaterialRequisitionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IServiceCallTrackSystem GetServiceCallTrackSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateServiceCallTrackSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobMaterialEstimateSystem GetJobMaterialEstimateSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobMaterialEstimateSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobManHrsBudgetingSystem GetJobManHrsBudgetingSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobManHrsBudgetingSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobMaintenanceScheduleSystem GetJobMaintenanceScheduleSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobMaintenanceScheduleSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobMaintenanceServiceSystem GetJobMaintenanceServiceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobMaintenanceServiceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobInventoryReturnSystem GetJobInventoryReturnSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobInventoryReturnSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobExpenseIssueSystem GetJobExpenseIssueSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobExpenseIssueSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobTimesheetSystem GetJobTimesheetSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobTimesheetSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IOverTimeEntrySystem GetOverTimeEntrySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateOverTimeEntrySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IHolidayCalendarSystem GetHolidayCalendarSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateHolidayCalendarSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBankFacilityGroupSystem GetBankFacilityGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBankFacilityGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBankFacilitySystem GetBankFacilitySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBankFacilitySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IOpeningBalanceBatchSystem GetOpeningBalanceBatchSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateOpeningBalanceBatchSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPhysicalStockEntrySystem GetPhysicalStockEntrySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePhysicalStockEntrySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IOpeningBalanceLeaveSystem GetOpeningBalanceLeaveSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateOpeningBalanceLeaveSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IOpeningEntryTransactionSystem GetOpeningEntryTransactionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateOpeningEntryTransactionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBankFacilityTransactionSystem GetBankFacilityTransactionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBankFacilityTransactionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBankFacilityPaymentSystem GetBankFacilityPaymentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBankFacilityPaymentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomGadgetSystem GetCustomGadgetSystemAsync(RequestInfo requestInfo)
		{
			SetWorkerLogin(requestInfo);
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomGadgetSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomGadgetSystem GetCustomGadgetSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomGadgetSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISendChequeSystem GetSendChequeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSendChequeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDiscountChequeSystem GetDiscountChequeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDiscountChequeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IInventoryTransferTypeSystem GetInventoryTransferTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateInventoryTransferTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IExportPackingListSystem GetExportPackingListSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateExportPackingListSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDashboardSystem GetDashboardSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDashboardSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IDashboardSystem GetDashboardSystemAsync(RequestInfo requestInfo)
		{
			SetWorkerLogin(requestInfo);
			if (workerLoginToken == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateDashboardSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IWebDashboardSystem GetWebDashboardSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateWebDashboardSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICollateralSystem GetCollateralSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCollateralSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IJobTaskSystem GetJobTaskSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateJobTaskSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IGRNReturnSystem GetGRNReturnSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateGRNReturnSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IArrivalReportSystem GetArrivalReportSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateArrivalReportSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IQualityClaimSystem GetQualityClaimSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateQualityClaimSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IArrivalReportTemplateSystem GetArrivalReportTemplateSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateArrivalReportTemplateSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IQualityTaskSystem GetQualityTaskSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateQualityTaskSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISurveyorSystem GetSurveyorSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSurveyorSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICheckListSystem GetCheckListSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCheckListSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IApprovalSystem GetApprovalSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateApprovalSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyAgentSystem GetPropertyAgentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyAgentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyClassSystem GetPropertyClassSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyClassSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertySystem GetPropertySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyUnitSystem GetPropertyUnitSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyUnitSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyVirtualUnitSystem GetPropertyVirtualUnitSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyVirtualUnitSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyIncomeCodeSystem GetPropertyIncomeCodeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyIncomeCodeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IRentalPostingSystem GetRentalPostingSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateRentalPostingSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPaymentRequestSystem GetPaymentRequestSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePaymentRequestSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchaseClaimSystem GetPurchaseClaimSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchaseClaimSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IW3PLGRNSystem GetW3PLGRNSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateW3PLGRNSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IW3PLDeliverySystem GetW3PLDeliverySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateW3PLDeliverySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IW3PLInvoiceSystem GetW3PLInvoiceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateW3PLInvoiceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEmailMessageSystem GetEmailMessageSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEmailMessageSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICLVoucherSystem GetCLVoucherSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCLVoucherSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICLTokenSystem GetCLTokenSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCLTokenSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IClientAssetSystem GetClientAssetSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateClientAssetSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IRouteSystem GetRouteSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateRouteSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IRouteGroupSystem GetRouteGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateRouteGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBinSystem GetBinSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBinSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IRackSystem GetRackSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateRackSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IServiceTypeSystem GetServiceTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateServiceTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IMaintenanceSchedulerSystem GetMaintenanceSchedulerSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateMaintenanceSchedulerSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVehicleMaintenanceEntrySystem GetMaintenanceEntrySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateMaintenanceEntrySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEntityCommentSystem GetEntityCommentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEntityCommentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IInsuranceProviderSystem GetInsuranceProviderSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateInsuranceProviderSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomerInsuranceSystem GetCustomerInsuranceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomerInsuranceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IRiderSummarySystem GetRiderSummarySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateRiderSummarySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IHorseSummarySystem GetHorseSummarySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateHorseSummarySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProjectSubContractPISystem GetProjectSubContractPISystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProjectSubContractPISystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEntityFlagSystem GetEntityFlagSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEntityFlagSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEAEquipmentSystem GetEAEquipmentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEAEquipmentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IMobilizationSystem GetMobilizationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateMobilizationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEquipmentTransferSystem GetEquipmentTransferSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEquipmentTransferSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IEquipmentWorkOrderSystem GetEquipmentWorkOrderSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateEquipmentWorkOrderSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILawyerSystem GetLawyerSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLawyerSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICasePartySystem GetCasePartySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCasePartySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IWorkOrderInventoryIssueSystem GetWorkOrderInventoryIssueSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateWorkOrderInventoryIssueSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILegalActivitySystem GetLegalActivitySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLegalActivitySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILegalActionSystem GetLegalActionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLegalActionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IWorkOrderInventoryReturnSystem GetWorkOrderInventoryReturnSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateWorkOrderInventoryReturnSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITaxSystem GetTaxSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTaxSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITaxGroupSystem GetTaxGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTaxGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IInventoryDismantleSystem GetInventoryDismantleSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateInventoryDismantleSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductPriceBulkUpdateSystem GetProductPriceBulkpdateSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductPriceBulkpdateSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IMaterialReservationSystem GetMaterialReservationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateMaterialReservationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICaseClientSystem GetCaseClientSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCaseClientSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductMakeSystem GetProductMakeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductMakeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductTypeSystem GetProductTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IProductModelSystem GetProductModelSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateProductModelSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IUserFavoriteSystem GetUserFavoriteSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateUserFavoriteSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPurchasePrepaymentInvoiceSystem GetPurchasePrepaymentInvoiceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePurchasePrepaymentInvoiceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalespersonGroupSystem GetSalespersonGroupSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalespersonGroupSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITaskStepsSystem GetTaskStepsSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTaskStepsSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITaskTypeSystem GetTaskTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTaskTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITaskTransactionSystem GetTaskTransactionSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTaskTransactionSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITaskTransactionStatusSystem GetTaskTransactionStatusSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTaskTransactionStatusSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILegalActionStatusSystem GetLegalActionStatusSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLegalActionStatusSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ITRApplicationSystem GetTRApplicationSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateTRApplicationSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IBudgetingSystem GetBudgetingSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateBudgetingSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IVehicleMileageTrackSystem GetVehicleMileageTrackSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateVehicleMileageTrackSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ISalesManTargetSystem GetSalesManTargetSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateSalesManTargetSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ICustomerInsuranceClaimSystem GetCustomerInsuranceClaimSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateCustomerInsuranceClaimSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyDocTypeSystem GetPropertyDocTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyDocTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IRecurringInvoiceSystem GetRecurringInvoiceSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateRecurringInvoiceSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyDocumentSystem GetPropertyDocumentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyDocumentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyTenantDocumentSystem GetPropertyTenantDocumentSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyTenantDocumentSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPropertyTenantDocTypeSystem GetPropertyTenantDocTypeSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePropertyTenantDocTypeSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static ILoanEntrySystem GetLoanEntrySystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreateLoanEntrySystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static IPrintTemplateMapSystem GetGetPrintTemplateMapSystem(RequestInfo requestInfo)
		{
			if (requestInfo == null)
			{
				throw new DBNotConnectedException();
			}
			try
			{
				return WebCompanySystem.CreatePrintTemplateMapSystem(requestInfo);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		static WebFactory()
		{
			WebFactory.OnConnecting = null;
		}
	}
}
