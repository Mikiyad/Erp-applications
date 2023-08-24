using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;

namespace Micromind.DataCaches
{
	public static class CombosData
	{
		private static DataSet allAccountsList;

		private static DataSet accountGroupsList;

		private static DataSet posExpenseAccountList;

		private static DataSet customerClasss;

		private static DataSet customers;

		private static DataSet vendorClasss;

		private static DataSet vendorGroups;

		private static DataSet customerGroups;

		private static DataSet vendors;

		private static DataSet countryList;

		private static DataSet agentList;

		private static DataSet areaList;

		private static DataSet priceLevelList;

		private static DataSet salespersonList;

		private static DataSet paymentTermList;

		private static DataSet shippingMethodsList;

		private static DataSet paymentMethodList;

		private static DataSet analysisList;

		private static DataSet analysisGroupList;

		private static DataSet employeeList;

		private static DataSet employeefilterList;

		private static DataSet contactList;

		private static DataSet activityList;

		private static DataSet buyerList;

		private static DataSet currencyList;

		private static DataSet bankList;

		private static DataSet bankFacilityGroupList;

		private static DataSet bankFacilityList;

		private static DataSet costCenter;

		private static DataSet sysDocList;

		private static DataSet transactionDocList;

		private static DataSet registerList;

		private static DataSet chequebookList;

		private static DataSet returnedChequeReasonList;

		private static DataSet receivedChequeList;

		private static DataSet customerAddressList;

		private static DataSet consignInList;

		private static DataSet transporterList;

		private static DataSet tenantClasss;

		private static DataSet serviceproviders;

		private static DataSet containerSizeList;

		private static DataSet analysisnonAccountList;

		private static DataSet INCOList;

		private static DataSet salespersonGroupList;

		private static DataSet propertyAgentList;

		private static DataSet leadAddressList;

		private static DataSet leadList;

		private static DataSet leadSourceList;

		private static DataSet customerleads;

		private static DataSet taskStepsList;

		private static DataSet taskTypeList;

		private static DataSet productCategoryList;

		private static DataSet productManufacturerList;

		private static DataSet productBrandList;

		private static DataSet productStyleList;

		private static DataSet productSpecificationList;

		private static DataSet unitList;

		private static DataSet productClassList;

		private static DataSet locationList;

		private static DataSet workLocationList;

		private static DataSet adjustmentTypeList;

		private static DataSet inventoryTransferTypeList;

		private static DataSet productList;

		private static DataSet productUnitDetailList;

		private static DataSet dimensionList;

		private static DataSet matrixTemplateList;

		private static DataSet posItemList;

		private static DataSet productParentList;

		private static DataSet bomList;

		private static DataSet holidayList;

		private static DataSet binList;

		private static DataSet routeList;

		private static DataSet routeGroupList;

		private static DataSet rackList;

		private static DataSet assetList;

		private static DataSet packageList;

		private static DataSet productUnitList;

		private static DataSet surveyorList;

		private static DataSet ReleaseTypeList;

		private static DataSet insuranceProviderList;

		private static DataSet MedicalinsuranceProviderList;

		private static DataSet ServiceItemList;

		private static DataSet leadStatusList;

		private static DataSet serviceactivityList;

		private static DataSet riderList;

		private static DataSet horseTypeList;

		private static DataSet horseSexList;

		private static DataSet taxList;

		private static DataSet taxGroupList;

		private static DataSet printTemplateMapList;

		private static DataSet posPaymentMethods;

		private static DataSet userGroupList;

		private static DataSet userList;

		private static DataSet expenseCodeList;

		private static DataSet fiscalYearList;

		private static DataSet pivotGroupList;

		private static DataSet arrivalReportTemplateList;

		private static DataSet departmentList;

		private static DataSet gradeList;

		private static DataSet sponsorList;

		private static DataSet driverList;

		private static DataSet nationalityList;

		private static DataSet religionList;

		private static DataSet divisionList;

		private static DataSet companyDivisionList;

		private static DataSet positionList;

		private static DataSet employeeDocTypeList;

		private static DataSet employeeProvisionTypeList;

		private static DataSet tenantDocTypeList;

		private static DataSet degreeList;

		private static DataSet skillList;

		private static DataSet employeeGroupList;

		private static DataSet employeeTypeList;

		private static DataSet leaveTypeList;

		private static DataSet payrollItemList;

		private static DataSet deductionList;

		private static DataSet benefitList;

		private static DataSet destinationList;

		private static DataSet companyDocTypeList;

		private static DataSet portList;

		private static DataSet posCashRegisterList;

		private static DataSet disciplineActionTypeList;

		private static DataSet employeeActivityTypeList;

		private static DataSet employeeLoanTypeList;

		private static DataSet employeeLoanList;

		private static DataSet eosRuleList;

		private static DataSet overTimeList;

		private static DataSet bankFacilityTransaction;

		private static DataSet smartListCategoryList;

		private static DataSet externalReportCategoryList;

		private static DataSet employeeLoanAllList;

		private static DataSet vehicleDocTypeList;

		private static DataSet containerList;

		private static DataSet patientDocTypeList;

		private static DataSet genericList;

		private static DataSet customList;

		private static DataSet entityCategoryList;

		private static DataSet fixedAssetClassList;

		private static DataSet fixedAssetGroupList;

		private static DataSet fixedAssetLocationList;

		private static DataSet fixedAssetList;

		private static DataSet jobList;

		private static DataSet jobTypeList;

		private static DataSet jobTaskGroupList;

		private static DataSet jobFeeList;

		private static DataSet opportunityList;

		private static DataSet competitorList;

		private static DataSet campaignList;

		private static DataSet jobTaskList;

		private static DataSet costCategoryList;

		private static DataSet equipmentList;

		private static DataSet cityList;

		private static DataSet vehicleList;

		private static DataSet horseList;

		private static DataSet jobbomList;

		private static DataSet ProductMakeList;

		private static DataSet ProductTypeList;

		private static DataSet propertyClassList;

		private static DataSet propertyList;

		private static DataSet propertyincomecodeList;

		private static DataSet propertyUnitList;

		private static DataSet propertyDocTypeList;

		private static DataSet propertyTenantDocTypeList;

		private static DataSet equipmentCategoryList;

		private static DataSet equipmentTypeList;

		private static DataSet requisitionTypeList;

		private static DataSet EAEquipmentList;

		private static DataSet lawyerList;

		private static DataSet casePartyList;

		private static DataSet caseclientList;

		private static DataSet actionStatusList;

		public static DataSet GetGenericListList(bool refresh, GenericListTypes listType)
		{
			if (genericList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.GenericList))
			{
				return genericList;
			}
			genericList = Factory.GenericListSystem.GetGenericListComboList(GenericListTypes.All);
			genericList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.GenericList, needRefresh: false);
			return genericList;
		}

		public static DataSet GetCustomListList(bool refresh, string customListCode)
		{
			if (customList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CustomList))
			{
				return customList;
			}
			customList = Factory.CustomListSystem.GetCustomListComboList("");
			customList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CustomList, needRefresh: false);
			return customList;
		}

		public static DataSet GetEntityCategoryList(bool refresh)
		{
			if (entityCategoryList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EntityCategory))
			{
				return entityCategoryList;
			}
			entityCategoryList = Factory.EntityCategorySystem.GetEntityCategoryCombosDataList();
			entityCategoryList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EntityCategory, needRefresh: false);
			return entityCategoryList;
		}

		public static DataSet GetAllAccountsList(bool refresh)
		{
			if (allAccountsList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Accounts))
			{
				return allAccountsList;
			}
			allAccountsList = Factory.CompanyAccountSystem.GetAccountsComboList();
			allAccountsList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Accounts, needRefresh: false);
			return allAccountsList;
		}

		public static DataSet GetAccountGroupList(bool refresh)
		{
			if (accountGroupsList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.AccountGroup))
			{
				return accountGroupsList;
			}
			accountGroupsList = Factory.AccountGroupsSystem.GetAccountGroupsComboList();
			accountGroupsList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.AccountGroup, needRefresh: false);
			return accountGroupsList;
		}

		public static DataSet GetPOSExpenseAccountList(bool refresh, string cashRegisterID)
		{
			if (posExpenseAccountList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.POSCashRegister))
			{
				return posExpenseAccountList;
			}
			posExpenseAccountList = Factory.POSCashRegisterSystem.GetExpenseAccountList(cashRegisterID);
			posExpenseAccountList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.POSCashRegister, needRefresh: false);
			return posExpenseAccountList;
		}

		public static DataSet GetAnalysisList(bool refresh)
		{
			if (analysisList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Analysis))
			{
				return analysisList;
			}
			analysisList = Factory.AnalysisSystem.GetAnalysisComboList();
			analysisList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Analysis, needRefresh: false);
			return analysisList;
		}

		public static DataSet GetAnalysisNonAccountList(bool refresh)
		{
			if (analysisnonAccountList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.AnalysisNonAccount))
			{
				return analysisnonAccountList;
			}
			analysisnonAccountList = Factory.AnalysisSystem.GetAnalysisNonAccountComboList();
			analysisnonAccountList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.AnalysisNonAccount, needRefresh: false);
			return analysisnonAccountList;
		}

		public static DataSet GetAnalysisGroupList(bool refresh)
		{
			if (analysisGroupList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.AnalysisGroup))
			{
				return analysisGroupList;
			}
			analysisGroupList = Factory.AnalysisGroupsSystem.GetAnalysisGroupsComboList();
			analysisGroupList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.AnalysisGroup, needRefresh: false);
			return analysisGroupList;
		}

		public static DataSet GetCurrencyList(bool refresh)
		{
			if (currencyList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Currency))
			{
				return currencyList;
			}
			currencyList = Factory.CurrencySystem.GetCurrencyComboList();
			currencyList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Currency, needRefresh: false);
			return currencyList;
		}

		public static DataSet GetBankList(bool refresh)
		{
			if (bankList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Bank))
			{
				return bankList;
			}
			bankList = Factory.BankSystem.GetBankComboList();
			bankList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Bank, needRefresh: false);
			return bankList;
		}

		public static DataSet GetBankFacilityGroupList(bool refresh)
		{
			if (bankFacilityGroupList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.BankFacilityGroup))
			{
				return bankFacilityGroupList;
			}
			bankFacilityGroupList = Factory.BankFacilityGroupSystem.GetBankFacilityGroupComboList();
			bankFacilityGroupList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.BankFacilityGroup, needRefresh: false);
			return bankFacilityGroupList;
		}

		public static DataSet GetBankFacilityList(bool refresh)
		{
			if (bankFacilityList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.BankFacility))
			{
				return bankFacilityList;
			}
			bankFacilityList = Factory.BankFacilitySystem.GetBankFacilityComboList();
			bankFacilityList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.BankFacility, needRefresh: false);
			return bankFacilityList;
		}

		public static DataSet GetCostCenterList(bool refresh)
		{
			if (costCenter != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CostCenter))
			{
				return costCenter;
			}
			costCenter = Factory.CostCenterSystem.GetCostCenterComboList();
			costCenter.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CostCenter, needRefresh: false);
			return costCenter;
		}

		public static DataSet GetSysDocList(bool refresh)
		{
			if (sysDocList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.SysDoc))
			{
				return sysDocList;
			}
			string defaultLocationID = Global.DefaultLocationID;
			sysDocList = Factory.SystemDocumentSystem.GetSystemDocumentComboList(defaultLocationID);
			sysDocList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.SysDoc, needRefresh: false);
			return sysDocList;
		}

		public static DataSet GetTransactionComboList(bool refresh)
		{
			if (transactionDocList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.SysDoc))
			{
				return transactionDocList;
			}
			_ = Global.DefaultLocationID;
			transactionDocList = Factory.SystemDocumentSystem.GetTransactionComboList();
			transactionDocList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.SysDoc, needRefresh: false);
			return transactionDocList;
		}

		public static DataSet GetRegisterList(bool refresh)
		{
			if (registerList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Register))
			{
				return registerList;
			}
			registerList = Factory.RegisterSystem.GetRegisterComboList();
			registerList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Register, needRefresh: false);
			return registerList;
		}

		public static DataSet GetChequebookList(bool refresh)
		{
			if (chequebookList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Chequebook))
			{
				return chequebookList;
			}
			chequebookList = Factory.ChequebookSystem.GetChequebookComboList();
			chequebookList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Chequebook, needRefresh: false);
			return chequebookList;
		}

		public static DataSet GetReturnedChequeReasonList(bool refresh)
		{
			if (returnedChequeReasonList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ReturnedChequeReason))
			{
				return returnedChequeReasonList;
			}
			returnedChequeReasonList = Factory.ReturnedChequeReasonSystem.GetReturnedChequeReasonComboList();
			returnedChequeReasonList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ReturnedChequeReason, needRefresh: false);
			return returnedChequeReasonList;
		}

		public static DataSet GetReceivedChequeList(bool refresh)
		{
			if (receivedChequeList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ReceivedCheque))
			{
				return receivedChequeList;
			}
			receivedChequeList = Factory.ReceivedChequeSystem.GetReceivedChequeComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.ReceivedCheque, needRefresh: false);
			return receivedChequeList;
		}

		public static DataSet GetCustomerClasssList(bool refresh)
		{
			if (customerClasss != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CustomerClass))
			{
				return customerClasss;
			}
			customerClasss = Factory.CustomerClassSystem.GetCustomerClassComboList();
			customerClasss.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CustomerClass, needRefresh: false);
			return customerClasss;
		}

		public static DataSet GetTenantClasssList(bool refresh)
		{
			if (tenantClasss != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.TenantClass))
			{
				return tenantClasss;
			}
			tenantClasss = Factory.CustomerClassSystem.GetTenantClassComboList();
			tenantClasss.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.TenantClass, needRefresh: false);
			return tenantClasss;
		}

		public static DataSet GetCustomerAddressList(bool refresh)
		{
			if (customerAddressList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CustomerAddress))
			{
				return customerAddressList;
			}
			customerAddressList = Factory.CustomerAddressSystem.GetCustomerAddressComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.CustomerAddress, needRefresh: false);
			return customerAddressList;
		}

		public static DataSet GetConsignInList(bool refresh)
		{
			if (consignInList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ConsignIn))
			{
				return consignInList;
			}
			consignInList = Factory.ConsignInSystem.GetConsignInComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.ConsignIn, needRefresh: false);
			return consignInList;
		}

		public static DataSet GetCustomerGroupsList(bool refresh)
		{
			if (customerGroups != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CustomerGroup))
			{
				return customerGroups;
			}
			customerGroups = Factory.CustomerGroupSystem.GetCustomerGroupComboList();
			customerGroups.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CustomerGroup, needRefresh: false);
			return customerGroups;
		}

		public static DataSet GetCustomersList(bool refresh)
		{
			if (customers != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Customer))
			{
				return customers;
			}
			customers = Factory.CustomerSystem.GetCustomerComboList();
			customers.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Customer, needRefresh: false);
			return customers;
		}

		public static DataSet getCaseClientList(bool refresh)
		{
			if (caseclientList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CaseClient))
			{
				return caseclientList;
			}
			caseclientList = Factory.CaseClientSystem.GetCustomerComboList();
			caseclientList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CaseClient, needRefresh: false);
			return caseclientList;
		}

		public static DataSet GetLeadAddressList(bool refresh)
		{
			if (leadAddressList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.LeadAddress))
			{
				return leadAddressList;
			}
			leadAddressList = Factory.LeadAddressSystem.GetLeadAddressComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.LeadAddress, needRefresh: false);
			return leadAddressList;
		}

		public static DataSet GetLeadsList(bool refresh)
		{
			if (leadList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Lead))
			{
				return leadList;
			}
			leadList = Factory.LeadSystem.GetLeadComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.Lead, needRefresh: false);
			return leadList;
		}

		public static DataSet GetLeadSourceList(bool refresh)
		{
			if (leadSourceList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Lead))
			{
				return leadSourceList;
			}
			leadSourceList = Factory.LeadSystem.GetLeadSourceComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.Lead, needRefresh: false);
			return leadList;
		}

		public static DataSet GetTaskStepsList(bool refresh)
		{
			if (taskStepsList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.TaskSteps))
			{
				return taskStepsList;
			}
			taskStepsList = Factory.TaskStepsSystem.GetTaskStepsComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.TaskSteps, needRefresh: false);
			return taskStepsList;
		}

		public static DataSet GetTaskTypeList(bool refresh)
		{
			if (taskTypeList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.TaskType))
			{
				return taskTypeList;
			}
			taskTypeList = Factory.TaskTypeSystem.GetTaskTypeComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.TaskType, needRefresh: false);
			return taskTypeList;
		}

		public static DataSet GetPatientDocTypeList(bool refresh)
		{
			if (customerleads != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PatientDocType))
			{
				return patientDocTypeList;
			}
			patientDocTypeList = Factory.PatientDocTypeSystem.GetPatientDocTypeComboList();
			patientDocTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PatientDocType, needRefresh: false);
			return patientDocTypeList;
		}

		public static DataSet GetCustomerLeadsList(bool refresh)
		{
			if (customerleads != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CustomerandLead))
			{
				return customerleads;
			}
			customerleads = Factory.CustomerSystem.GetCustomerLeadsComboList();
			customerleads.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CustomerandLead, needRefresh: false);
			return customerleads;
		}

		public static DataSet GetVendorClasssList(bool refresh)
		{
			if (vendorClasss != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.VendorClass))
			{
				return vendorClasss;
			}
			vendorClasss = Factory.VendorClassSystem.GetVendorClassComboList();
			vendorClasss.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.VendorClass, needRefresh: false);
			return vendorClasss;
		}

		public static DataSet GetVendorGroupsList(bool refresh)
		{
			if (vendorGroups != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.VendorGroup))
			{
				return vendorGroups;
			}
			vendorGroups = Factory.VendorGroupSystem.GetVendorGroupComboList();
			vendorGroups.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.VendorGroup, needRefresh: false);
			return vendorGroups;
		}

		public static DataSet GetVendorsList(bool refresh)
		{
			if (vendors != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Vendor))
			{
				return vendors;
			}
			vendors = Factory.VendorSystem.GetVendorComboList();
			vendors.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Vendor, needRefresh: false);
			return vendors;
		}

		public static DataSet GetBuyerList(bool refresh)
		{
			if (buyerList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Buyer))
			{
				return buyerList;
			}
			buyerList = Factory.BuyerSystem.GetBuyerComboList();
			buyerList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Buyer, needRefresh: false);
			return buyerList;
		}

		public static DataSet GetPropertyAgentList(bool refresh)
		{
			if (propertyAgentList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PropertyAgent))
			{
				return propertyAgentList;
			}
			propertyAgentList = Factory.PropertyAgentSystem.GetPropertyAgentComboList();
			propertyAgentList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PropertyAgent, needRefresh: false);
			return propertyAgentList;
		}

		public static DataSet GetServiceProviderList(bool refresh)
		{
			if (serviceproviders != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ServiceProvider))
			{
				return serviceproviders;
			}
			serviceproviders = Factory.VendorSystem.GetServiceProviderComboList();
			serviceproviders.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ServiceProvider, needRefresh: false);
			return serviceproviders;
		}

		public static DataSet GetEmployeeList(bool refresh)
		{
			if (employeeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Employee))
			{
				return employeeList;
			}
			employeeList = Factory.EmployeeSystem.GetEmployeeComboList();
			employeeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Employee, needRefresh: false);
			return employeeList;
		}

		public static DataSet GetEmployeeFilterList(bool refresh)
		{
			if (employeefilterList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EmployeeFilter))
			{
				return employeefilterList;
			}
			employeefilterList = Factory.EmployeeSystem.GetEmployeeFilterComboList();
			employeefilterList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeFilter, needRefresh: false);
			return employeefilterList;
		}

		public static DataSet GetEmployeeGroupList(bool refresh)
		{
			if (employeeGroupList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EmployeeGroup))
			{
				return employeeGroupList;
			}
			employeeGroupList = Factory.EmployeeGroupSystem.GetEmployeeGroupComboList();
			employeeGroupList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeGroup, needRefresh: false);
			return employeeGroupList;
		}

		public static DataSet GetEmployeeTypeList(bool refresh)
		{
			if (employeeTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EmployeeType))
			{
				return employeeTypeList;
			}
			employeeTypeList = Factory.EmployeeTypeSystem.GetEmployeeTypeComboList();
			employeeTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeType, needRefresh: false);
			return employeeTypeList;
		}

		public static DataSet GetGradeList(bool refresh)
		{
			if (gradeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Grade))
			{
				return gradeList;
			}
			gradeList = Factory.GradeSystem.GetGradeComboList();
			gradeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Grade, needRefresh: false);
			return gradeList;
		}

		public static DataSet GetSponsorList(bool refresh)
		{
			if (sponsorList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Sponsor))
			{
				return sponsorList;
			}
			sponsorList = Factory.SponsorSystem.GetSponsorComboList();
			sponsorList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Sponsor, needRefresh: false);
			return sponsorList;
		}

		public static DataSet GetDriverList(bool refresh)
		{
			if (driverList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Driver))
			{
				return driverList;
			}
			driverList = Factory.DriverSystem.GetDriverComboList();
			driverList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Driver, needRefresh: false);
			return driverList;
		}

		public static DataSet GetNationalityList(bool refresh)
		{
			if (nationalityList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Nationality))
			{
				return nationalityList;
			}
			nationalityList = Factory.NationalitySystem.GetNationalityComboList();
			nationalityList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Nationality, needRefresh: false);
			return nationalityList;
		}

		public static DataSet GetReligionList(bool refresh)
		{
			if (religionList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Religion))
			{
				return religionList;
			}
			religionList = Factory.ReligionSystem.GetReligionComboList();
			religionList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Religion, needRefresh: false);
			return religionList;
		}

		public static DataSet GetDivisionList(bool refresh)
		{
			if (divisionList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Division))
			{
				return divisionList;
			}
			divisionList = Factory.DivisionSystem.GetDivisionComboList();
			divisionList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Division, needRefresh: false);
			return divisionList;
		}

		public static DataSet GetCompanyDivisionList(bool refresh)
		{
			if (companyDivisionList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CompanyDivision))
			{
				return companyDivisionList;
			}
			companyDivisionList = Factory.CompanyDivisionSystem.GetDivisionComboList();
			companyDivisionList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CompanyDivision, needRefresh: false);
			return companyDivisionList;
		}

		public static DataSet GetPositionList(bool refresh)
		{
			if (positionList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Position))
			{
				return positionList;
			}
			positionList = Factory.PositionSystem.GetPositionComboList();
			positionList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Position, needRefresh: false);
			return positionList;
		}

		public static DataSet GetDepartmentList(bool refresh)
		{
			if (departmentList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Department))
			{
				return departmentList;
			}
			departmentList = Factory.DepartmentSystem.GetDepartmentComboList();
			departmentList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Department, needRefresh: false);
			return departmentList;
		}

		public static DataSet GetEmployeeDocTypeList(bool refresh)
		{
			if (employeeDocTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EmployeeDocType))
			{
				return employeeDocTypeList;
			}
			employeeDocTypeList = Factory.EmployeeDocTypeSystem.GetEmployeeDocTypeComboList();
			employeeDocTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeDocType, needRefresh: false);
			return employeeDocTypeList;
		}

		public static DataSet GetEmployeeProvisionTypeList(bool refresh)
		{
			if (employeeProvisionTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EmployeeProvisionType))
			{
				return employeeProvisionTypeList;
			}
			employeeProvisionTypeList = Factory.ProvisionTypeSystem.GetProvisionTypeComboList();
			employeeProvisionTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeProvisionType, needRefresh: false);
			return employeeProvisionTypeList;
		}

		public static DataSet GetTenantDocTypeList(bool refresh)
		{
			if (tenantDocTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PropertyTenantDocType))
			{
				return tenantDocTypeList;
			}
			tenantDocTypeList = Factory.PropertyTenantDocTypeSystem.GetPropertyTenantDocTypeComboList();
			tenantDocTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PropertyTenantDocType, needRefresh: false);
			return tenantDocTypeList;
		}

		public static DataSet GetVehicleDocTypeList(bool refresh)
		{
			if (vehicleDocTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.VehicleDocType))
			{
				return vehicleDocTypeList;
			}
			vehicleDocTypeList = Factory.VehicleDocTypeSystem.GetVehicleDocTypeComboList();
			vehicleDocTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.VehicleDocType, needRefresh: false);
			return vehicleDocTypeList;
		}

		public static DataSet GetDegreeList(bool refresh)
		{
			if (degreeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Degree))
			{
				return degreeList;
			}
			degreeList = Factory.DegreeSystem.GetDegreeComboList();
			degreeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Degree, needRefresh: false);
			return degreeList;
		}

		public static DataSet GetSkillList(bool refresh)
		{
			if (skillList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Skill))
			{
				return skillList;
			}
			skillList = Factory.SkillSystem.GetSkillComboList();
			skillList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Skill, needRefresh: false);
			return skillList;
		}

		public static DataSet GetLeaveTypeList(bool refresh)
		{
			if (leaveTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.LeaveType))
			{
				return leaveTypeList;
			}
			leaveTypeList = Factory.LeaveTypeSystem.GetLeaveTypeComboList();
			leaveTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.LeaveType, needRefresh: false);
			return leaveTypeList;
		}

		public static DataSet GetJobTypeList(bool refresh)
		{
			if (jobTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.JobType))
			{
				return jobTypeList;
			}
			jobTypeList = Factory.JobTypeSystem.GetJobTypeComboList();
			jobTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.JobType, needRefresh: false);
			return jobTypeList;
		}

		public static DataSet GetJobTaskGroupList(bool refresh)
		{
			if (jobTaskGroupList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.JobTaskGroup))
			{
				return jobTaskGroupList;
			}
			jobTaskGroupList = Factory.JobTaskGroupSystem.GetJobTaskGroupComboList();
			jobTaskGroupList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.JobTaskGroup, needRefresh: false);
			return jobTaskGroupList;
		}

		public static DataSet GetPayrollItemList(bool refresh)
		{
			if (payrollItemList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PayrollItem))
			{
				return payrollItemList;
			}
			payrollItemList = Factory.PayrollItemSystem.GetPayrollItemComboList();
			payrollItemList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PayrollItem, needRefresh: false);
			return payrollItemList;
		}

		public static DataSet GetDeductionList(bool refresh)
		{
			if (deductionList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Deduction))
			{
				return deductionList;
			}
			deductionList = Factory.DeductionSystem.GetDeductionComboList();
			deductionList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Deduction, needRefresh: false);
			return deductionList;
		}

		public static DataSet GetBenefitList(bool refresh)
		{
			if (benefitList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Benefit))
			{
				return benefitList;
			}
			benefitList = Factory.BenefitSystem.GetBenefitComboList();
			benefitList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Benefit, needRefresh: false);
			return benefitList;
		}

		public static DataSet GetDestinationList(bool refresh)
		{
			if (destinationList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Destination))
			{
				return destinationList;
			}
			destinationList = Factory.DestinationSystem.GetDestinationComboList();
			destinationList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Destination, needRefresh: false);
			return destinationList;
		}

		public static DataSet GetCompanyDocTypeList(bool refresh)
		{
			if (companyDocTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CompanyDocType))
			{
				return companyDocTypeList;
			}
			companyDocTypeList = Factory.CompanyDocTypeSystem.GetCompanyDocTypeComboList();
			companyDocTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CompanyDocType, needRefresh: false);
			return companyDocTypeList;
		}

		public static DataSet GetDisciplineActionTypeList(bool refresh)
		{
			if (disciplineActionTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.DisciplineActionType))
			{
				return disciplineActionTypeList;
			}
			disciplineActionTypeList = Factory.DisciplineActionTypeSystem.GetActionTypeComboList();
			disciplineActionTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.DisciplineActionType, needRefresh: false);
			return disciplineActionTypeList;
		}

		public static DataSet GetEmployeeActivityTypeList(bool refresh)
		{
			if (employeeActivityTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EmployeeActivityType))
			{
				return employeeActivityTypeList;
			}
			employeeActivityTypeList = Factory.EmployeeActivityTypeSystem.GetActivityTypeComboList();
			employeeActivityTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeActivityType, needRefresh: false);
			return employeeActivityTypeList;
		}

		public static DataSet GetEmployeeLoanTypeList(bool refresh)
		{
			if (employeeLoanTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EmployeeLoanType))
			{
				return employeeLoanTypeList;
			}
			employeeLoanTypeList = Factory.EmployeeLoanTypeSystem.GetEmployeeLoanTypeComboList();
			employeeLoanTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeLoanType, needRefresh: false);
			return employeeLoanTypeList;
		}

		public static DataSet GetEmployeeLoanList(bool refresh)
		{
			if (employeeLoanList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EmployeeLoan))
			{
				return employeeLoanList;
			}
			employeeLoanList = Factory.EmployeeLoanSystem.GetEmployeeLoanComboList();
			employeeLoanList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeLoan, needRefresh: false);
			return employeeLoanList;
		}

		public static DataSet GetEmployeeLoanAllList(bool refresh)
		{
			if (employeeLoanAllList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EmployeeLoanAll))
			{
				return employeeLoanAllList;
			}
			employeeLoanAllList = Factory.EmployeeLoanSystem.GetEmployeeLoanAllComboList();
			employeeLoanAllList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeLoanAll, needRefresh: false);
			return employeeLoanAllList;
		}

		public static DataSet GetEOSRuleList(bool refresh)
		{
			if (eosRuleList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EOSRule))
			{
				return eosRuleList;
			}
			eosRuleList = Factory.EOSRuleSystem.GetEOSRuleComboList();
			eosRuleList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EOSRule, needRefresh: false);
			return eosRuleList;
		}

		public static DataSet GetOverTimeList(bool refresh)
		{
			if (overTimeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.OverTime))
			{
				return overTimeList;
			}
			overTimeList = Factory.OverTimeSystem.GetOverTimeComboList();
			overTimeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.OverTime, needRefresh: false);
			return overTimeList;
		}

		public static DataSet GetFixedAssetClassList(bool refresh)
		{
			if (fixedAssetClassList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.FixedAssetClass))
			{
				return fixedAssetClassList;
			}
			fixedAssetClassList = Factory.FixedAssetClassSystem.GetAssetClassComboList();
			fixedAssetClassList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.FixedAssetClass, needRefresh: false);
			return fixedAssetClassList;
		}

		public static DataSet GetFixedAssetGroupList(bool refresh)
		{
			if (fixedAssetGroupList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.FixedAssetGroup))
			{
				return fixedAssetGroupList;
			}
			fixedAssetGroupList = Factory.FixedAssetGroupSystem.GetAssetGroupComboList();
			fixedAssetGroupList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.FixedAssetGroup, needRefresh: false);
			return fixedAssetGroupList;
		}

		public static DataSet GetFixedAssetLocationList(bool refresh)
		{
			if (fixedAssetLocationList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.FixedAssetLocation))
			{
				return fixedAssetLocationList;
			}
			fixedAssetLocationList = Factory.FixedAssetLocationSystem.GetAssetLocationComboList();
			fixedAssetLocationList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.FixedAssetLocation, needRefresh: false);
			return fixedAssetLocationList;
		}

		public static DataSet GetFixedAssetList(bool refresh)
		{
			if (fixedAssetList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.FixedAsset))
			{
				return fixedAssetList;
			}
			fixedAssetList = Factory.FixedAssetSystem.GetAssetComboList();
			fixedAssetList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.FixedAsset, needRefresh: false);
			return fixedAssetList;
		}

		public static DataSet GetProductList(bool refresh)
		{
			if (productList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Product))
			{
				return productList;
			}
			DataSet dataSet = productList = CommonLib.DecompressDataSet(Factory.ProductSystem.GetProductComboList());
			dataSet.Tables[0].Columns.Add("SearchColumn");
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				row["SearchColumn"] = row["Code"].ToString().Replace(" ", "") + row["Name"].ToString().Replace(" ", "");
			}
			productList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Product, needRefresh: false);
			return productList;
		}

		public static DataSet GetProductUnitList(bool refresh)
		{
			if (productUnitDetailList != null && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductUnitDetail))
			{
				return productUnitDetailList;
			}
			productUnitDetailList = Factory.UnitSystem.GetProductUnitDetailComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductUnitDetail, needRefresh: false);
			return productUnitDetailList;
		}

		public static DataSet GetProductParentList(bool refresh)
		{
			if (productParentList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductParent))
			{
				return productParentList;
			}
			productParentList = Factory.ProductParentSystem.GetProductParentComboList();
			productParentList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductParent, needRefresh: false);
			return productParentList;
		}

		public static DataSet GetProductUnitDetailList(bool refresh)
		{
			if (productUnitDetailList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductUnitDetail))
			{
				return productUnitDetailList;
			}
			productUnitDetailList = Factory.UnitSystem.GetProductUnitDetailComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductUnitDetail, needRefresh: false);
			return productUnitDetailList;
		}

		public static DataSet GetDimensionList(bool refresh)
		{
			if (dimensionList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Dimension))
			{
				return dimensionList;
			}
			dimensionList = Factory.DimensionSystem.GetDimensionComboList();
			dimensionList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Dimension, needRefresh: false);
			return dimensionList;
		}

		public static DataSet GetMatrixTemplateList(bool refresh)
		{
			if (matrixTemplateList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.MatrixTemplate))
			{
				return matrixTemplateList;
			}
			matrixTemplateList = Factory.MatrixTemplateSystem.GetMatrixTemplateComboList();
			matrixTemplateList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.MatrixTemplate, needRefresh: false);
			return matrixTemplateList;
		}

		public static DataSet GetProductCategoryList(bool refresh)
		{
			if (productCategoryList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductCategory))
			{
				return productCategoryList;
			}
			productCategoryList = Factory.ProductCategorySystem.GetProductCategoryComboList();
			productCategoryList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductCategory, needRefresh: false);
			return productCategoryList;
		}

		public static DataSet GetProductManufacturerList(bool refresh)
		{
			if (productManufacturerList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductManufacturer))
			{
				return productManufacturerList;
			}
			productManufacturerList = Factory.ProductManufacturerSystem.GetProductManufacturerComboList();
			productManufacturerList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductManufacturer, needRefresh: false);
			return productManufacturerList;
		}

		public static DataSet GetProductBrandList(bool refresh)
		{
			if (productBrandList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductBrand))
			{
				return productBrandList;
			}
			productBrandList = Factory.ProductBrandSystem.GetProductBrandComboList();
			productBrandList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductBrand, needRefresh: false);
			return productBrandList;
		}

		public static DataSet GetProductStyleList(bool refresh)
		{
			if (productStyleList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductStyle))
			{
				return productStyleList;
			}
			productStyleList = Factory.ProductStyleSystem.GetProductStyleComboList();
			productStyleList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductStyle, needRefresh: false);
			return productStyleList;
		}

		public static DataSet GetProductSpecification(bool refresh)
		{
			if (productSpecificationList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductSpecification))
			{
				return productSpecificationList;
			}
			productSpecificationList = Factory.ProductSpecificationSystem.GetProductSpecificationComboList();
			productSpecificationList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductSpecification, needRefresh: false);
			return productSpecificationList;
		}

		public static DataSet GetUnitList(bool refresh)
		{
			if (unitList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Unit))
			{
				return unitList;
			}
			unitList = Factory.UnitSystem.GetUnitComboList();
			unitList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Unit, needRefresh: false);
			return unitList;
		}

		public static DataSet GetProductClassList(bool refresh)
		{
			if (productClassList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductClass))
			{
				return productClassList;
			}
			productClassList = Factory.ProductClassSystem.GetProductClassComboList();
			productClassList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductClass, needRefresh: false);
			return productClassList;
		}

		public static DataSet GetAdjustmentTypeList(bool refresh)
		{
			if (adjustmentTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.AdjustmentType))
			{
				return adjustmentTypeList;
			}
			adjustmentTypeList = Factory.AdjustmentTypeSystem.GetAdjustmentTypeComboList();
			adjustmentTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.AdjustmentType, needRefresh: false);
			return adjustmentTypeList;
		}

		public static DataSet GetInventoryTransferTypeList(bool refresh)
		{
			if (inventoryTransferTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.AdjustmentType))
			{
				return inventoryTransferTypeList;
			}
			inventoryTransferTypeList = Factory.InventoryTransferTypeSystem.GetInventoryTransferTypeComboList();
			inventoryTransferTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.InventoryTransferType, needRefresh: false);
			return inventoryTransferTypeList;
		}

		public static DataSet GetPOSItemList(bool refresh)
		{
			if (posItemList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ItemPOS))
			{
				return posItemList;
			}
			posItemList = Factory.ProductSystem.GetProductListPOS();
			ComboDataHelper.SetRefreshStatus(DataComboType.ItemPOS, needRefresh: false);
			return posItemList;
		}

		public static DataSet GetPOSPaymentMethods(bool refresh)
		{
			if (posPaymentMethods != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.POSPaymentMethod))
			{
				return posPaymentMethods;
			}
			posPaymentMethods = Factory.POSCashRegisterSystem.GetPaymentMethodsList("", showInactive: false);
			ComboDataHelper.SetRefreshStatus(DataComboType.POSPaymentMethod, needRefresh: false);
			return posPaymentMethods;
		}

		public static DataSet GetBOMList(bool refresh)
		{
			if (bomList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.BOM))
			{
				return bomList;
			}
			bomList = Factory.BOMSystem.GetBOMComboList();
			bomList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.BOM, needRefresh: false);
			return bomList;
		}

		public static DataSet GetHolidayList(bool refresh)
		{
			if (holidayList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.HolidayCalendar))
			{
				return holidayList;
			}
			holidayList = Factory.HolidayCalendarSystem.GetHolidayComboList();
			holidayList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.HolidayCalendar, needRefresh: false);
			return holidayList;
		}

		public static DataSet GetServiceActivityList(bool refresh)
		{
			if (serviceactivityList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ServiceActivity))
			{
				return serviceactivityList;
			}
			serviceactivityList = Factory.ServiceActivitySystem.GetServiceActivityComboList();
			serviceactivityList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ServiceActivity, needRefresh: false);
			return serviceactivityList;
		}

		public static DataSet GetBinList(bool refresh)
		{
			if (binList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Bin))
			{
				return binList;
			}
			binList = Factory.BinSystem.GetBinComboList();
			binList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Bin, needRefresh: false);
			return binList;
		}

		public static DataSet GetRouteList(bool refresh)
		{
			if (routeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Route))
			{
				return routeList;
			}
			routeList = Factory.RouteSystem.GetRouteComboList();
			routeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Route, needRefresh: false);
			return routeList;
		}

		public static DataSet GetRouteGroupList(bool refresh)
		{
			if (routeGroupList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.RouteGroup))
			{
				return routeGroupList;
			}
			routeGroupList = Factory.RouteGroupSystem.GetRouteGroupComboList();
			routeGroupList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.RouteGroup, needRefresh: false);
			return routeGroupList;
		}

		public static DataSet GetRackList(bool refresh)
		{
			if (rackList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Rack))
			{
				return rackList;
			}
			rackList = Factory.RackSystem.GetRackComboList();
			rackList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Rack, needRefresh: false);
			return rackList;
		}

		public static DataSet GetCientAssetList(bool refresh)
		{
			if (assetList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ClientAsset))
			{
				return assetList;
			}
			assetList = Factory.ClientAssetSystem.GetClientAssetComboList();
			assetList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ClientAsset, needRefresh: false);
			return assetList;
		}

		public static DataSet GetJobBOMList(bool refresh)
		{
			if (jobbomList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.JobBOM))
			{
				return jobbomList;
			}
			jobbomList = Factory.JobBOMSystem.GetJobBOMComboList();
			jobbomList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.JobBOM, needRefresh: false);
			return jobbomList;
		}

		public static DataSet GetPackageList(bool refresh)
		{
			if (packageList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Package))
			{
				return packageList;
			}
			packageList = Factory.BOMSystem.GetPackageComboList();
			packageList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Package, needRefresh: false);
			return packageList;
		}

		public static DataSet GetCountryList(bool refresh)
		{
			if (countryList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Country))
			{
				return countryList;
			}
			countryList = Factory.CountrySystem.GetCountryComboList();
			countryList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Country, needRefresh: false);
			return countryList;
		}

		public static DataSet GetLocationList(bool refresh)
		{
			if (locationList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Location))
			{
				return locationList;
			}
			locationList = Factory.LocationSystem.GetLocationComboList();
			locationList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Location, needRefresh: false);
			return locationList;
		}

		public static DataSet GetWorkLocationList(bool refresh)
		{
			if (workLocationList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.WorkLocation))
			{
				return workLocationList;
			}
			workLocationList = Factory.WorkLocationSystem.GetWorkLocationComboList();
			workLocationList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.WorkLocation, needRefresh: false);
			return workLocationList;
		}

		public static DataSet GetUserList(bool refresh)
		{
			if (userList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.User))
			{
				return userList;
			}
			userList = Factory.UserSystem.GetUserComboList();
			userList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.User, needRefresh: false);
			return userList;
		}

		public static DataSet GetUserGroupList(bool refresh)
		{
			if (userGroupList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.UserGroup))
			{
				return userGroupList;
			}
			userGroupList = Factory.UserGroupSystem.GetUserGroupComboList();
			userGroupList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.UserGroup, needRefresh: false);
			return userGroupList;
		}

		public static DataSet GetPaymentTermList(bool refresh)
		{
			if (paymentTermList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PaymentTerm))
			{
				return paymentTermList;
			}
			paymentTermList = Factory.TermSystem.GetPaymentTermsComboList();
			paymentTermList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PaymentTerm, needRefresh: false);
			return paymentTermList;
		}

		public static DataSet GetPaymentMethodList(bool refresh)
		{
			if (paymentMethodList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PaymentMethod))
			{
				return paymentMethodList;
			}
			paymentMethodList = Factory.PaymentMethodSystem.GetPaymentMethodsComboList();
			paymentMethodList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PaymentMethod, needRefresh: false);
			return paymentMethodList;
		}

		public static DataSet GetShippingMethodsList(bool refresh)
		{
			if (shippingMethodsList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ShippingMethod))
			{
				return shippingMethodsList;
			}
			shippingMethodsList = Factory.ShippingMethodSystem.GetShippingMethodsComboList();
			shippingMethodsList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ShippingMethod, needRefresh: false);
			return shippingMethodsList;
		}

		public static DataSet GetAreaList(bool refresh)
		{
			if (areaList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Area))
			{
				return areaList;
			}
			areaList = Factory.AreaSystem.GetAreaComboList();
			areaList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Area, needRefresh: false);
			return areaList;
		}

		public static DataSet GetTransporterList(bool refresh)
		{
			if (transporterList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Transporter))
			{
				return transporterList;
			}
			transporterList = Factory.TransporterSystem.GetTransporterComboList();
			transporterList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Transporter, needRefresh: false);
			return transporterList;
		}

		public static DataSet GetLawyerList(bool refresh)
		{
			if (lawyerList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Lawyer))
			{
				return lawyerList;
			}
			lawyerList = Factory.LawyerSystem.GetLawyerList();
			lawyerList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Lawyer, needRefresh: false);
			return lawyerList;
		}

		public static DataSet GetCasePartyList(bool refresh)
		{
			if (casePartyList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CaseParty))
			{
				return casePartyList;
			}
			casePartyList = Factory.CasePartySystem.GetCasePartyList();
			casePartyList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CaseParty, needRefresh: false);
			return casePartyList;
		}

		public static DataSet GetLegalActionStatusList(bool refresh)
		{
			if (actionStatusList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.LegalActionStatus))
			{
				return actionStatusList;
			}
			actionStatusList = Factory.LegalActionStatusSystem.GetLegalActionStatusComboList();
			actionStatusList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.LegalActionStatus, needRefresh: false);
			return actionStatusList;
		}

		public static DataSet GetEquipmentCategoryList(bool refresh)
		{
			if (equipmentCategoryList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EquipmentCategory))
			{
				return equipmentCategoryList;
			}
			equipmentCategoryList = Factory.EquipmentCategorySystem.GetEquipmentCategoryComboList();
			equipmentCategoryList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EquipmentCategory, needRefresh: false);
			return equipmentCategoryList;
		}

		public static DataSet GetEquipmentTypeList(bool refresh)
		{
			if (equipmentTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EquipmentType))
			{
				return equipmentTypeList;
			}
			equipmentTypeList = Factory.EquipmentTypeSystem.GetEquipmentTypeComboList();
			equipmentTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EquipmentType, needRefresh: false);
			return equipmentTypeList;
		}

		public static DataSet GetEAEquipmentList(bool refresh)
		{
			if (EAEquipmentList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.EAEquipment))
			{
				return EAEquipmentList;
			}
			EAEquipmentList = Factory.EAEquipmentSystem.GetEquipmentComboList();
			EAEquipmentList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.EAEquipment, needRefresh: false);
			return EAEquipmentList;
		}

		public static DataSet GetRequisitionTypeList(bool refresh)
		{
			if (requisitionTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.RequisitionType))
			{
				return requisitionTypeList;
			}
			requisitionTypeList = Factory.RequisitionTypeSystem.GetRequisitionTypeComboList();
			requisitionTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.RequisitionType, needRefresh: false);
			return requisitionTypeList;
		}

		public static DataSet GetContainerSizeList(bool refresh)
		{
			if (containerSizeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ContainerSize))
			{
				return containerSizeList;
			}
			containerSizeList = Factory.ContainerSizeSystem.GetContainerSizeComboList();
			containerSizeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ContainerSize, needRefresh: false);
			return containerSizeList;
		}

		public static DataSet GetINCOList(bool refresh)
		{
			if (INCOList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.INCO))
			{
				return INCOList;
			}
			INCOList = Factory.INCOSystem.GetINCOComboList();
			INCOList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.INCO, needRefresh: false);
			return INCOList;
		}

		public static DataSet GetJobList(bool refresh)
		{
			if (jobList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Job))
			{
				return jobList;
			}
			jobList = Factory.JobSystem.GetJobComboList();
			jobList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Job, needRefresh: false);
			return jobList;
		}

		public static DataSet GetJobFeeList(bool refresh)
		{
			if (jobFeeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.JobFee))
			{
				return jobFeeList;
			}
			jobFeeList = Factory.JobSystem.GetJobFeeComboList();
			jobFeeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.JobFee, needRefresh: false);
			return jobFeeList;
		}

		public static DataSet GetCostCategoryList(bool refresh)
		{
			if (costCategoryList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.CostCategory))
			{
				return costCategoryList;
			}
			costCategoryList = Factory.CostCategorySystem.GetCostCategoryComboList();
			costCategoryList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.CostCategory, needRefresh: false);
			return costCategoryList;
		}

		public static DataSet GetEquipmentList(bool refresh)
		{
			if (equipmentList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Equipment))
			{
				return equipmentList;
			}
			equipmentList = Factory.EquipmentSystem.GetEquipmentComboList();
			equipmentList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Equipment, needRefresh: false);
			return equipmentList;
		}

		public static DataSet GetCityList(bool refresh)
		{
			if (cityList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.City))
			{
				return cityList;
			}
			cityList = Factory.CitySystem.GetCityComboList();
			cityList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.City, needRefresh: false);
			return cityList;
		}

		public static DataSet GetVehicleList(bool refresh)
		{
			if (vehicleList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Vehicle))
			{
				return vehicleList;
			}
			vehicleList = Factory.VehicleSystem.GetVehicleComboList();
			vehicleList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Vehicle, needRefresh: false);
			return vehicleList;
		}

		public static DataSet GethorseList(bool refresh)
		{
			if (horseList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.HorseSummary))
			{
				return horseList;
			}
			horseList = Factory.HorseSummarySystem.GetHorseSummaryComboList();
			horseList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.HorseSummary, needRefresh: false);
			return horseList;
		}

		public static DataSet GetProductMakeList(bool refresh)
		{
			if (ProductMakeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductMake))
			{
				return ProductMakeList;
			}
			ProductMakeList = Factory.ProductMakeSystem.GetProductMakeComboList();
			ProductMakeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductMake, needRefresh: false);
			return ProductMakeList;
		}

		public static DataSet GetProductTypeList(bool refresh)
		{
			if (ProductTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ProductType))
			{
				return ProductTypeList;
			}
			ProductTypeList = Factory.ProductTypeSystem.GetProductTypeComboList();
			ProductTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ProductType, needRefresh: false);
			return ProductTypeList;
		}

		public static DataSet GetOpportunityList(bool refresh)
		{
			if (opportunityList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Opportunity))
			{
				return opportunityList;
			}
			opportunityList = Factory.OpportunitySystem.GetOpportunityComboList();
			opportunityList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Opportunity, needRefresh: false);
			return opportunityList;
		}

		public static DataSet GetCompetitorList(bool refresh)
		{
			if (competitorList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Competitor))
			{
				return competitorList;
			}
			competitorList = Factory.CompetitorSystem.GetCompetitorComboList();
			competitorList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Competitor, needRefresh: false);
			return competitorList;
		}

		public static DataSet GetCampaignList(bool refresh)
		{
			if (campaignList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Campaign))
			{
				return campaignList;
			}
			campaignList = Factory.CampaignSystem.GetCampaignComboList();
			campaignList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Campaign, needRefresh: false);
			return campaignList;
		}

		public static DataSet GetJobTaskList(bool refresh)
		{
			if (jobTaskList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.JobTask))
			{
				return jobTaskList;
			}
			jobTaskList = Factory.JobTaskSystem.GetJobTaskComboList();
			jobTaskList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Campaign, needRefresh: false);
			return jobTaskList;
		}

		public static DataSet GetSalespersonList(bool refresh)
		{
			if (salespersonList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Salesperson))
			{
				return salespersonList;
			}
			salespersonList = Factory.SalespersonSystem.GetSalespersonComboList();
			salespersonList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Salesperson, needRefresh: false);
			return salespersonList;
		}

		public static DataSet GetSalespersonGroupList(bool refresh)
		{
			if (salespersonGroupList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.TransactionDoc))
			{
				return salespersonGroupList;
			}
			salespersonGroupList = Factory.SalespersonGroupSystem.GetSalespersonGroupComboList();
			salespersonGroupList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.TransactionDoc, needRefresh: false);
			return salespersonGroupList;
		}

		public static DataSet GetContactsList(bool refresh)
		{
			if (contactList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Contact))
			{
				return contactList;
			}
			contactList = Factory.ContactSystem.GetContactComboList();
			contactList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Contact, needRefresh: false);
			return contactList;
		}

		public static DataSet GetActivityList(bool refresh)
		{
			if (activityList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Activity))
			{
				return activityList;
			}
			activityList = Factory.ActivitySystem.GetActivityComboList();
			activityList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Activity, needRefresh: false);
			return activityList;
		}

		public static DataSet GetPropertyClassList(bool refresh)
		{
			if (propertyClassList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PropertyClass))
			{
				return propertyClassList;
			}
			propertyClassList = Factory.PropertyClassSystem.GetPropertyClassComboList();
			propertyClassList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PropertyClass, needRefresh: false);
			return propertyClassList;
		}

		public static DataSet GetPropertyList(bool refresh)
		{
			if (propertyList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Property))
			{
				return propertyList;
			}
			propertyList = Factory.PropertySystem.GetPropertyComboList();
			propertyList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Property, needRefresh: false);
			return propertyList;
		}

		public static DataSet GetPropertyUnitList(bool refresh)
		{
			if (propertyUnitList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PropertyUnit))
			{
				return propertyUnitList;
			}
			propertyUnitList = Factory.PropertyUnitSystem.GetPropertyUnitComboList();
			propertyUnitList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Property, needRefresh: false);
			return propertyUnitList;
		}

		public static DataSet GetPropertyDocTypeList(bool refresh)
		{
			if (propertyDocTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PropertyDocType))
			{
				return propertyDocTypeList;
			}
			propertyDocTypeList = Factory.PropertyDocTypeSystem.GetPropertyDocTypeComboList();
			propertyDocTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PropertyDocType, needRefresh: false);
			return propertyDocTypeList;
		}

		public static DataSet GetPropertyTenantDocTypeList(bool refresh)
		{
			if (propertyTenantDocTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PropertyTenantDocType))
			{
				return propertyTenantDocTypeList;
			}
			propertyTenantDocTypeList = Factory.PropertyTenantDocTypeSystem.GetPropertyTenantDocTypeComboList();
			propertyTenantDocTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PropertyTenantDocType, needRefresh: false);
			return propertyTenantDocTypeList;
		}

		public static DataSet GetRiderList(bool refresh)
		{
			if (riderList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.RiderSummary))
			{
				return riderList;
			}
			riderList = Factory.RiderSummarySystem.GetRiderSummaryComboList();
			riderList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.RiderSummary, needRefresh: false);
			return riderList;
		}

		public static DataSet GetHorseTypeList(bool refresh)
		{
			if (horseTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.HorseType))
			{
				return horseTypeList;
			}
			horseTypeList = Factory.HorseTypeSystem.GetHorseTypeComboList();
			horseTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.HorseType, needRefresh: false);
			return horseTypeList;
		}

		public static DataSet GetHorseSexList(bool refresh)
		{
			if (horseTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.HorseSex))
			{
				return horseSexList;
			}
			horseSexList = Factory.HorseSexSystem.GetHorseSexComboList();
			horseSexList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.HorseSex, needRefresh: false);
			return horseSexList;
		}

		public static DataSet GetSurveyorList(bool refresh)
		{
			if (surveyorList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Surveyor))
			{
				return surveyorList;
			}
			surveyorList = Factory.SurveyorSystem.GetSurveyorComboList();
			surveyorList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Surveyor, needRefresh: false);
			return surveyorList;
		}

		public static DataSet GetPriceLevelList(bool refresh)
		{
			if (priceLevelList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PriceLevel))
			{
				return priceLevelList;
			}
			DataRow dataRow = Factory.ProductSystem.GetPriceLevelComboList().Tables[0].Rows[0];
			priceLevelList = new DataSet();
			priceLevelList.Tables.Add("PriceLevel");
			priceLevelList.Tables[0].Columns.Add("Code");
			priceLevelList.Tables[0].Columns.Add("Name");
			priceLevelList.Tables[0].Rows.Add("", "");
			string text = "";
			if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice1))
			{
				text = dataRow[0].ToString();
				if (text == "")
				{
					text = "Standard Price";
				}
				priceLevelList.Tables[0].Rows.Add(0, text);
			}
			if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice2))
			{
				text = dataRow[1].ToString();
				if (text == "")
				{
					text = "Wholesale Price";
				}
				priceLevelList.Tables[0].Rows.Add(1, text);
			}
			if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice3))
			{
				text = dataRow[2].ToString();
				if (text == "")
				{
					text = "Special Price";
				}
				priceLevelList.Tables[0].Rows.Add(2, text);
			}
			if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
			{
				priceLevelList.Tables[0].Rows.Add(3, "Min Price");
			}
			ComboDataHelper.SetRefreshStatus(DataComboType.PriceLevel, needRefresh: false);
			return priceLevelList;
		}

		public static DataSet GetExpenseCodeList(bool refresh)
		{
			if (expenseCodeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ExpenseCode))
			{
				return expenseCodeList;
			}
			expenseCodeList = Factory.ExpenseCodeSystem.GetExpenseCodeComboList();
			expenseCodeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ExpenseCode, needRefresh: false);
			return expenseCodeList;
		}

		public static DataSet GetPropertyIncomeCodeList(bool refresh)
		{
			if (propertyincomecodeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ExpenseCode))
			{
				return propertyincomecodeList;
			}
			propertyincomecodeList = Factory.PropertyIncomeCodeSystem.GetPropertyIncomeCodeComboList();
			propertyincomecodeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PropertyIncomeCode, needRefresh: false);
			return propertyincomecodeList;
		}

		public static DataSet GetFiscalYearList(bool refresh)
		{
			if (fiscalYearList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.FiscalYear))
			{
				return fiscalYearList;
			}
			fiscalYearList = Factory.FiscalYearSystem.GetFiscalYearComboList();
			fiscalYearList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.FiscalYear, needRefresh: false);
			return fiscalYearList;
		}

		public static DataSet GetPivotGroupList(bool refresh)
		{
			if (pivotGroupList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PivotGroup))
			{
				return pivotGroupList;
			}
			pivotGroupList = Factory.PivotGroupSystem.GetPivotGroupComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.PivotGroup, needRefresh: false);
			return pivotGroupList;
		}

		public static DataSet GetArrivalReportTemplateList(bool refresh)
		{
			if (arrivalReportTemplateList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ArrivalReportTemplate))
			{
				return arrivalReportTemplateList;
			}
			arrivalReportTemplateList = Factory.ArrivalReportTemplateSystem.GetArrivalReportTemplateComboList();
			ComboDataHelper.SetRefreshStatus(DataComboType.ArrivalReportTemplate, needRefresh: false);
			return arrivalReportTemplateList;
		}

		public static DataSet GetPortList(bool refresh)
		{
			if (portList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Port))
			{
				return portList;
			}
			portList = Factory.PortSystem.GetPortComboList();
			portList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Port, needRefresh: false);
			return portList;
		}

		public static DataSet GetSmartListCategoryList(bool refresh)
		{
			if (smartListCategoryList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.SmartListCategory))
			{
				return smartListCategoryList;
			}
			smartListCategoryList = Factory.SmartListSystem.GetSmartListCategoryComboList();
			smartListCategoryList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.SmartListCategory, needRefresh: false);
			return smartListCategoryList;
		}

		public static DataSet GetExternalReportCategoryList(bool refresh)
		{
			if (externalReportCategoryList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ExternalReportCategory))
			{
				return externalReportCategoryList;
			}
			externalReportCategoryList = Factory.ExternalReportSystem.GetExternalReportCategoryComboList();
			externalReportCategoryList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ExternalReportCategory, needRefresh: false);
			return externalReportCategoryList;
		}

		public static DataSet GetPOSCashRegisterList(bool refresh)
		{
			if (posCashRegisterList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.POSCashRegister))
			{
				return posCashRegisterList;
			}
			posCashRegisterList = Factory.POSCashRegisterSystem.GetPOSCashRegisterComboList();
			posCashRegisterList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.POSCashRegister, needRefresh: false);
			return posCashRegisterList;
		}

		public static DataSet GetAgentList(bool refresh)
		{
			if (agentList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Agent))
			{
				return agentList;
			}
			agentList = Factory.CandidateSystem.GetAgentComboList();
			agentList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Agent, needRefresh: false);
			return agentList;
		}

		public static DataSet GetReleaseTypeList(bool refresh)
		{
			if (ReleaseTypeList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ReleaseType))
			{
				return ReleaseTypeList;
			}
			ReleaseTypeList = Factory.ReleaseTypeSystem.GetReleaseTypeComboList();
			ReleaseTypeList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ReleaseType, needRefresh: false);
			return ReleaseTypeList;
		}

		public static DataSet GetInsuranceProviderList(bool refresh)
		{
			if (insuranceProviderList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.InsuranceProvider))
			{
				return insuranceProviderList;
			}
			insuranceProviderList = Factory.InsuranceProviderSystem.GetInsuranceProviderComboList();
			insuranceProviderList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.InsuranceProvider, needRefresh: false);
			return insuranceProviderList;
		}

		public static DataSet GetMedicalInsuranceProviderList(bool refresh)
		{
			if (MedicalinsuranceProviderList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.MedicalInsuranceProvider))
			{
				return MedicalinsuranceProviderList;
			}
			MedicalinsuranceProviderList = Factory.InsuranceProviderSystem.GetMedicalInsuranceProviderComboList();
			MedicalinsuranceProviderList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.MedicalInsuranceProvider, needRefresh: false);
			return MedicalinsuranceProviderList;
		}

		public static DataSet GetServiceItemList(bool refresh)
		{
			if (ServiceItemList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.ServiceItem))
			{
				return ServiceItemList;
			}
			ServiceItemList = Factory.ServiceTypeSystem.GetServiceItemComboList();
			ServiceItemList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.ServiceItem, needRefresh: false);
			return ServiceItemList;
		}

		public static DataSet GetLeadStatusList(bool refresh)
		{
			if (leadStatusList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.LeadStatus))
			{
				return leadStatusList;
			}
			leadStatusList = Factory.LeadStatusSystem.GetLeadStatusComboList();
			leadStatusList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.LeadStatus, needRefresh: false);
			return leadStatusList;
		}

		public static DataSet GetTaxList(bool refresh)
		{
			if (taxList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.Tax))
			{
				return taxList;
			}
			taxList = Factory.TaxSystem.GetTaxComboList();
			taxList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.Tax, needRefresh: false);
			return taxList;
		}

		public static DataSet GetTaxGroupList(bool refresh)
		{
			if (taxGroupList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.TaxGroup))
			{
				return taxGroupList;
			}
			taxGroupList = Factory.TaxGroupSystem.GetTaxGroupComboList();
			taxGroupList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.TaxGroup, needRefresh: false);
			return taxGroupList;
		}

		public static DataSet GetPrintTemplateMapList(bool refresh)
		{
			if (printTemplateMapList != null && !refresh && !refresh && !ComboDataHelper.NeedRefresh(DataComboType.PrintTemplateMap))
			{
				return printTemplateMapList;
			}
			printTemplateMapList = Factory.PrintTemplateMapSystem.GetPrintTemplateMapComboList();
			printTemplateMapList.Tables[0].Rows.Add("", "");
			ComboDataHelper.SetRefreshStatus(DataComboType.PrintTemplateMap, needRefresh: false);
			return printTemplateMapList;
		}

		public static void ResetAllCaches()
		{
			allAccountsList = null;
			accountGroupsList = null;
			customerClasss = null;
			customers = null;
			vendorClasss = null;
			vendorGroups = null;
			customerGroups = null;
			vendors = null;
			countryList = null;
			areaList = null;
			priceLevelList = null;
			salespersonList = null;
			paymentTermList = null;
			shippingMethodsList = null;
			paymentMethodList = null;
			analysisList = null;
			analysisGroupList = null;
			employeeList = null;
			contactList = null;
			buyerList = null;
			currencyList = null;
			bankList = null;
			costCenter = null;
			sysDocList = null;
			registerList = null;
			chequebookList = null;
			returnedChequeReasonList = null;
			receivedChequeList = null;
			customerAddressList = null;
			taxList = null;
			taxGroupList = null;
			productCategoryList = null;
			productManufacturerList = null;
			productBrandList = null;
			productStyleList = null;
			unitList = null;
			productClassList = null;
			locationList = null;
			adjustmentTypeList = null;
			productList = null;
			productUnitList = null;
			dimensionList = null;
			matrixTemplateList = null;
			posItemList = null;
			posPaymentMethods = null;
			userGroupList = null;
			userList = null;
			departmentList = null;
			gradeList = null;
			sponsorList = null;
			driverList = null;
			nationalityList = null;
			religionList = null;
			divisionList = null;
			positionList = null;
			employeeDocTypeList = null;
			degreeList = null;
			skillList = null;
			employeeGroupList = null;
			employeeTypeList = null;
			leaveTypeList = null;
			payrollItemList = null;
			deductionList = null;
			benefitList = null;
			destinationList = null;
			companyDocTypeList = null;
			portList = null;
			disciplineActionTypeList = null;
			employeeActivityTypeList = null;
			employeeLoanTypeList = null;
			employeeLoanList = null;
			fixedAssetClassList = null;
			fixedAssetGroupList = null;
			fixedAssetLocationList = null;
			fixedAssetList = null;
			jobList = null;
			jobTypeList = null;
			opportunityList = null;
		}
	}
}
