using Micromind.Common.Data;
using System.Collections.Generic;

namespace Micromind.ClientUI.Libraries
{
	internal class DataImportHelper
	{
		public static CustomerData CustomerImportFields
		{
			get
			{
				CustomerData customerData = new CustomerData();
				customerData.Tables.Remove("Customer_Address");
				customerData.Tables.Remove("Customer_Contact_Detail");
				List<string> list = new List<string>();
				list.Add("CustomerID");
				list.Add("CustomerName");
				list.Add("ForeignName");
				list.Add("ShortName");
				list.Add("ParentCustomerID");
				list.Add("BankName");
				list.Add("BankBranch");
				list.Add("CustomerClassID");
				list.Add("AreaID");
				list.Add("SubAreaID");
				list.Add("CountryID");
				list.Add("CurrencyID");
				list.Add("PaymentMethodID");
				list.Add("ShippingMethodID");
				list.Add("ARAccountID");
				list.Add("Rating");
				list.Add("DivisionID");
				list.Add("SalesPersonID");
				list.Add("PaymentTermID");
				list.Add("Note");
				list.Add("PriceLevelID");
				list.Add("BillToAddressID");
				list.Add("ShipToAddressID");
				list.Add("StatementAddressID");
				list.Add("IsCustomerSince");
				list.Add("DateEstablished");
				list.Add("CreditReviewDate");
				list.Add("CreditReviewBy");
				list.Add("CollectionUserID");
				list.Add("CreditLimitType");
				list.Add("CreditAmount");
				list.Add("InsApplicationDate");
				list.Add("InsApprovedAmount");
				list.Add("InsRemarks");
				list.Add("InsRequestedAmount");
				list.Add("InsStatus");
				list.Add("InsPolicyNumber");
				checked
				{
					for (int i = 0; i < customerData.CustomerTable.Columns.Count; i++)
					{
						if (!list.Contains(customerData.CustomerTable.Columns[i].ColumnName))
						{
							customerData.CustomerTable.Columns.RemoveAt(i);
							i--;
						}
					}
					return customerData;
				}
			}
		}

		public static VendorData VendorImportFields
		{
			get
			{
				VendorData vendorData = new VendorData();
				vendorData.Tables.Remove("Vendor_Address");
				vendorData.Tables.Remove("Vendor_Contact_Detail");
				List<string> list = new List<string>();
				list.Add("VendorID");
				list.Add("VendorName");
				list.Add("ForeignName");
				list.Add("VendorClassID");
				list.Add("AreaID");
				list.Add("CountryID");
				list.Add("CurrencyID");
				list.Add("PaymentMethodID");
				list.Add("ShippingMethodID");
				list.Add("BankName");
				list.Add("BankBranch");
				list.Add("BankAccountNumber");
				list.Add("APAccountID");
				list.Add("PriceLevelID");
				list.Add("BuyerID");
				list.Add("PaymentTermID");
				list.Add("ParentVendorID");
				list.Add("CreditLimitType");
				list.Add("CreditAmount");
				list.Add("Note");
				checked
				{
					for (int i = 0; i < vendorData.VendorTable.Columns.Count; i++)
					{
						if (!list.Contains(vendorData.VendorTable.Columns[i].ColumnName))
						{
							vendorData.VendorTable.Columns.RemoveAt(i);
							i--;
						}
					}
					return vendorData;
				}
			}
		}

		public static ProductData ProductImportFields
		{
			get
			{
				ProductData productData = new ProductData();
				productData.Tables.Remove("Product_Unit");
				List<string> list = new List<string>();
				list.Add("ProductID");
				list.Add("Description");
				list.Add("Description2");
				list.Add("UPC");
				list.Add("ItemType");
				list.Add("UnitPrice");
				list.Add("UnitPrice1");
				list.Add("UnitPrice2");
				list.Add("UnitPrice3");
				list.Add("MinPrice");
				list.Add("ClassID");
				list.Add("VendorRef");
				list.Add("LastCost");
				list.Add("CostMethod");
				list.Add("CategoryID");
				list.Add("QuantityPerUnit");
				list.Add("Weight");
				list.Add("UnitID");
				list.Add("ReorderLevel");
				list.Add("COGSAccount");
				list.Add("AssetAccount");
				list.Add("IncomeAccount");
				list.Add("PreferredVendor");
				list.Add("StyleID");
				list.Add("Attribute");
				list.Add("Size");
				list.Add("BrandID");
				list.Add("ManufacturerID");
				list.Add("Origin");
				list.Add("WarrantyPeriod");
				list.Add("Note");
				checked
				{
					for (int i = 0; i < productData.ProductTable.Columns.Count; i++)
					{
						if (!list.Contains(productData.ProductTable.Columns[i].ColumnName))
						{
							productData.ProductTable.Columns.RemoveAt(i);
							i--;
						}
					}
					return productData;
				}
			}
		}

		public static EmployeeData EmployeeImportFields
		{
			get
			{
				EmployeeData employeeData = new EmployeeData();
				employeeData.Tables.Remove("Employee_Address");
				List<string> list = new List<string>();
				list.Add("EmployeeID");
				list.Add("LastName");
				list.Add("FirstName");
				list.Add("MiddleName");
				list.Add("BirthDate");
				list.Add("NickName");
				list.Add("JoiningDate");
				list.Add("GroupID");
				list.Add("TerminationDate");
				list.Add("IsTerminated");
				list.Add("CancellationDate");
				list.Add("IsCancelled");
				list.Add("GradeID");
				list.Add("DayOff");
				list.Add("BirthPlace");
				list.Add("SponsorID");
				list.Add("NationalityID");
				list.Add("Probation");
				list.Add("ConfirmationDate");
				list.Add("ReligionID");
				list.Add("BloodGroup");
				list.Add("ContractType");
				list.Add("Notes");
				list.Add("LocationID");
				list.Add("DivisionID");
				list.Add("DepartmentID");
				list.Add("PositionID");
				list.Add("ReportToID");
				list.Add("PayPeriod");
				list.Add("Gender");
				list.Add("MaritalStatus");
				list.Add("SpouseName");
				list.Add("AccountID");
				list.Add("BankID");
				list.Add("NationalID");
				list.Add("Status");
				list.Add("LabourID");
				list.Add("IBAN");
				list.Add("LastRevisedSalaryDate");
				checked
				{
					for (int i = 0; i < employeeData.EmployeeTable.Columns.Count; i++)
					{
						if (!list.Contains(employeeData.EmployeeTable.Columns[i].ColumnName))
						{
							employeeData.EmployeeTable.Columns.RemoveAt(i);
							i--;
						}
					}
					return employeeData;
				}
			}
		}

		public static CompanyAccountData AccountImportFields
		{
			get
			{
				CompanyAccountData companyAccountData = new CompanyAccountData();
				List<string> list = new List<string>();
				list.Add("AccountID");
				list.Add("AccountName");
				list.Add("Alias");
				list.Add("CurrencyID");
				list.Add("BankAccountType");
				list.Add("BankAccountNumber");
				list.Add("SubType");
				list.Add("Note");
				list.Add("UserDefined1");
				list.Add("UserDefined2");
				list.Add("UserDefined3");
				list.Add("UserDefined4");
				list.Add("GroupID");
				checked
				{
					for (int i = 0; i < companyAccountData.CompanyAccountTable.Columns.Count; i++)
					{
						if (!list.Contains(companyAccountData.CompanyAccountTable.Columns[i].ColumnName))
						{
							companyAccountData.CompanyAccountTable.Columns.RemoveAt(i);
							i--;
						}
					}
					return companyAccountData;
				}
			}
		}
	}
}
