using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Data.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Vendors : StoreObject
	{
		private const string VENDORID_PARM = "@VendorID";

		private const string NAME_PARM = "@Name";

		private const string FOREIGNNAME_PARM = "@ForeignName";

		private const string COMPANYNAME_PARM = "@CompanyName";

		private const string LEGALNAME_PARM = "@FormalName";

		private const string FIRSTNAME_PARM = "@FirstName";

		private const string LASTNAME_PARM = "@LastName";

		private const string MIDDLENAME_PARM = "@MiddleName";

		private const string NOTE_PARM = "@Note";

		private const string PARENTVENDORID_PARM = "@ParentVendorID";

		private const string VENDORCLASSID_PARM = "@VendorClassID";

		private const string VENDORGROUPID_PARM = "@VendorGroupID";

		private const string AREAID_PARM = "@AreaID";

		private const string COUNTRYID_PARM = "@CountryID";

		private const string ISHOLD_PARM = "@IsHold";

		private const string ISSERVICEPROVIDER_PARM = "@IsServiceProvider";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string PAYMENTMETHODID_PARM = "@PaymentMethodID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string ACCEPTCHECKPAYMENT_PARM = "@AcceptCheckPayment";

		private const string ACCEPTPDC_PARM = "@AcceptPDC";

		private const string CREDITLIMITTYPE_PARM = "@CreditLimitType";

		private const string ALLOWCONSIGNMENT_PARM = "@AllowConsignment";

		private const string CONSIGNCOMPERCENT_PARM = "@ConsignComPercent";

		private const string ALLOWOPENACCOUNTPAYMENT_PARM = "@AllowOAP";

		private const string LICENSEEXPDATE_PARM = "@LicenseExpDate";

		private const string CONTRACTEXPDATE_PARM = "@ContractExpDate";

		private const string BANKNAME_PARM = "@BankName";

		private const string BANKBRANCH_PARM = "@BankBranch";

		private const string BANKACCOUNTNUMBER_PARM = "@BankAccountNumber";

		private const string SWIFTCODE_PARM = "@SwiftCode";

		private const string VATREGISTRATIONNUMBER_PARM = "@VATRegistrationNumber";

		private const string APACCOUNTID_PARM = "@APAccountID";

		private const string PRIMARYADDRESSID_PARM = "@PrimaryAddressID";

		private const string ISHOLDFORPAYMENT_PARM = "@IsHoldForPayment";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXIDNUMBER_PARM = "@TaxIDNumber";

		private const string USERDEFINED1_PARM = "@UserDefined1";

		private const string USERDEFINED2_PARM = "@UserDefined2";

		private const string USERDEFINED3_PARM = "@UserDefined3";

		private const string USERDEFINED4_PARM = "@UserDefined4";

		private const string BUYERID_PARM = "@BuyerID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string CREDITAMOUNT_PARM = "@CreditAmount";

		private const string PAYMENTTERMID_PARM = "@PaymentTermID";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string CONTACTNAME_PARM = "@ContactName";

		private const string JOBTITLE_PARM = "@JobTitle";

		private const string CONTACTID_PARM = "@ContactID";

		private const string ROWINDEX_PARM = "@RowIndex";

		public Vendors(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vendor", new FieldValue("VendorID", "@VendorID", isUpdateConditionField: true), new FieldValue("VendorName", "@Name"), new FieldValue("ForeignName", "@ForeignName"), new FieldValue("CompanyName", "@CompanyName"), new FieldValue("LegalName", "@FormalName"), new FieldValue("ParentVendorID", "@ParentVendorID"), new FieldValue("VendorClassID", "@VendorClassID"), new FieldValue("VendorGroupID", "@VendorGroupID"), new FieldValue("AreaID", "@AreaID"), new FieldValue("IsHold", "@IsHold"), new FieldValue("CountryID", "@CountryID"), new FieldValue("PaymentMethodID", "@PaymentMethodID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("AcceptCheckPayment", "@AcceptCheckPayment"), new FieldValue("AcceptPDC", "@AcceptPDC"), new FieldValue("CreditLimitType", "@CreditLimitType"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("AllowConsignment", "@AllowConsignment"), new FieldValue("ConsignComPercent", "@ConsignComPercent"), new FieldValue("IsServiceProvider", "@IsServiceProvider"), new FieldValue("AllowOAP", "@AllowOAP"), new FieldValue("ContractExpDate", "@ContractExpDate"), new FieldValue("LicenseExpDate", "@LicenseExpDate"), new FieldValue("BankName", "@BankName"), new FieldValue("BankBranch", "@BankBranch"), new FieldValue("BankAccountNumber", "@BankAccountNumber"), new FieldValue("SwiftCode", "@SwiftCode"), new FieldValue("APAccountID", "@APAccountID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxIDNumber", "@TaxIDNumber"), new FieldValue("PrimaryAddressID", "@PrimaryAddressID"), new FieldValue("UserDefined1", "@UserDefined1"), new FieldValue("UserDefined2", "@UserDefined2"), new FieldValue("UserDefined3", "@UserDefined3"), new FieldValue("UserDefined4", "@UserDefined4"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("CreditAmount", "@CreditAmount"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("PaymentTermID", "@PaymentTermID"), new FieldValue("IsHoldForPayment", "@IsHoldForPayment"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Vendor", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@Name", SqlDbType.NVarChar);
			parameters.Add("@ForeignName", SqlDbType.NVarChar);
			parameters.Add("@CompanyName", SqlDbType.NVarChar);
			parameters.Add("@FormalName", SqlDbType.NVarChar);
			parameters.Add("@ParentVendorID", SqlDbType.NVarChar);
			parameters.Add("@IsServiceProvider", SqlDbType.Bit);
			parameters.Add("@VendorClassID", SqlDbType.NVarChar);
			parameters.Add("@VendorGroupID", SqlDbType.NVarChar);
			parameters.Add("@IsHold", SqlDbType.Bit);
			parameters.Add("@AreaID", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodID", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@AcceptCheckPayment", SqlDbType.Bit);
			parameters.Add("@AcceptPDC", SqlDbType.Bit);
			parameters.Add("@CreditLimitType", SqlDbType.TinyInt);
			parameters.Add("@AllowConsignment", SqlDbType.Bit);
			parameters.Add("@ConsignComPercent", SqlDbType.Decimal);
			parameters.Add("@BankName", SqlDbType.NVarChar);
			parameters.Add("@BankBranch", SqlDbType.NVarChar);
			parameters.Add("@BankAccountNumber", SqlDbType.NVarChar);
			parameters.Add("@SwiftCode", SqlDbType.NVarChar);
			parameters.Add("@AllowOAP", SqlDbType.Bit);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxIDNumber", SqlDbType.NVarChar);
			parameters.Add("@APAccountID", SqlDbType.NVarChar);
			parameters.Add("@PrimaryAddressID", SqlDbType.NVarChar);
			parameters.Add("@UserDefined1", SqlDbType.NVarChar);
			parameters.Add("@UserDefined2", SqlDbType.NVarChar);
			parameters.Add("@UserDefined3", SqlDbType.NVarChar);
			parameters.Add("@UserDefined4", SqlDbType.NVarChar);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@CreditAmount", SqlDbType.Money);
			parameters.Add("@PaymentTermID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@ContractExpDate", SqlDbType.DateTime);
			parameters.Add("@LicenseExpDate", SqlDbType.DateTime);
			parameters.Add("@IsHoldForPayment", SqlDbType.Bit);
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@Name"].SourceColumn = "VendorName";
			parameters["@ForeignName"].SourceColumn = "ForeignName";
			parameters["@CompanyName"].SourceColumn = "CompanyName";
			parameters["@FormalName"].SourceColumn = "LegalName";
			parameters["@ParentVendorID"].SourceColumn = "ParentVendorID";
			parameters["@VendorClassID"].SourceColumn = "VendorClassID";
			parameters["@VendorGroupID"].SourceColumn = "VendorGroupID";
			parameters["@IsHold"].SourceColumn = "IsHold";
			parameters["@AreaID"].SourceColumn = "AreaID";
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@PaymentMethodID"].SourceColumn = "PaymentMethodID";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@AcceptCheckPayment"].SourceColumn = "AcceptCheckPayment";
			parameters["@AcceptPDC"].SourceColumn = "AcceptPDC";
			parameters["@CreditLimitType"].SourceColumn = "CreditLimitType";
			parameters["@AllowConsignment"].SourceColumn = "AllowConsignment";
			parameters["@ConsignComPercent"].SourceColumn = "ConsignComPercent";
			parameters["@AllowOAP"].SourceColumn = "AllowOAP";
			parameters["@ContractExpDate"].SourceColumn = "ContractExpDate";
			parameters["@LicenseExpDate"].SourceColumn = "LicenseExpDate";
			parameters["@BankName"].SourceColumn = "BankName";
			parameters["@BankBranch"].SourceColumn = "BankBranch";
			parameters["@BankAccountNumber"].SourceColumn = "BankAccountNumber";
			parameters["@SwiftCode"].SourceColumn = "SwiftCode";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxIDNumber"].SourceColumn = "TaxIDNumber";
			parameters["@APAccountID"].SourceColumn = "APAccountID";
			parameters["@PrimaryAddressID"].SourceColumn = "PrimaryAddressID";
			parameters["@UserDefined1"].SourceColumn = "UserDefined1";
			parameters["@UserDefined2"].SourceColumn = "UserDefined2";
			parameters["@UserDefined3"].SourceColumn = "UserDefined3";
			parameters["@UserDefined4"].SourceColumn = "UserDefined4";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@CreditAmount"].SourceColumn = "CreditAmount";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@IsHoldForPayment"].SourceColumn = "IsHoldForPayment";
			parameters["@PaymentTermID"].SourceColumn = "PaymentTermID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@IsServiceProvider"].SourceColumn = "IsServiceProvider";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateVendorContactText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vendor_Contact_Detail", new FieldValue("VendorID", "@VendorID", isUpdateConditionField: true), new FieldValue("ContactID", "@ContactID", isUpdateConditionField: true), new FieldValue("JobTitle", "@JobTitle"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateVendorContactCommand(bool isUpdate)
		{
			SqlCommand sqlCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				sqlCommand = new SqlCommand(GetInsertUpdateVendorContactText(isUpdate: true), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			else
			{
				sqlCommand = new SqlCommand(GetInsertUpdateVendorContactText(isUpdate: false), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@ContactID", SqlDbType.NVarChar);
			parameters.Add("@JobTitle", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@ContactID"].SourceColumn = "ContactID";
			parameters["@JobTitle"].SourceColumn = "JobTitle";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Note"].SourceColumn = "Note";
			return sqlCommand;
		}

		public bool InsertUpdateVendor(VendorData accountVendorData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(accountVendorData, "Vendor", insertUpdateCommand) : Update(accountVendorData, "Vendor", insertUpdateCommand));
				if (flag)
				{
					insertUpdateCommand = new VendorAddresses(base.DBConfig).GetInsertUpdateCommand(isUpdate);
					insertUpdateCommand.Transaction = sqlTransaction;
					if (isUpdate)
					{
						flag &= Update(accountVendorData, "Vendor_Address", insertUpdateCommand);
					}
					else if (accountVendorData.Tables["Vendor_Address"].Rows.Count > 0)
					{
						flag &= Insert(accountVendorData, "Vendor_Address", insertUpdateCommand);
					}
				}
				string text = accountVendorData.VendorTable.Rows[0]["VendorID"].ToString();
				if (flag)
				{
					if (isUpdate)
					{
						DeleteVendorContacts(sqlTransaction, text.ToString());
					}
					if (accountVendorData.Tables["Vendor_Contact_Detail"].Rows.Count > 0)
					{
						insertUpdateCommand = GetInsertUpdateVendorContactCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Insert(accountVendorData, "Vendor_Contact_Detail", insertUpdateCommand);
					}
				}
				if (isUpdate)
				{
					AddActivityLog("Vendor", text, "", ActivityTypes.Update, "", null, SysDocTypes.None, DataComboType.Vendor, null, sqlTransaction);
				}
				else
				{
					AddActivityLog("Vendor", text, "", ActivityTypes.Add, "", null, SysDocTypes.None, DataComboType.Vendor, null, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Vendor", "VendorID", text, sqlTransaction, !isUpdate);
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Vendor, text, "Vendor", "VendorID", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		private bool DeleteVendorContacts(SqlTransaction sqlTransaction, string vendorID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Vendor_Contact_Detail WHERE VendorID = '" + vendorID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Vendor Contact", vendorID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetServiceProviderComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VendorID [Code],VendorName  [Name],REPLACE(VendorID,' ','') + REPLACE(VendorName,' ','')  AS SearchColumn ,ISNULL(CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID, \r\n                             ParentVendorID,AllowConsignment,ConsignComPercent, ShippingMethodID, PaymentTermID, PaymentMethodID, BuyerID, PrimaryAddressID,VendorClassID, CASE WHEN ISNULL(V.TaxOption,0) <> 0 THEN V.TaxOption ELSE ISNULL(VC.TaxOption,2) END AS TaxOption, \r\n                                CASE WHEN ISNULL(V.TaxOption,0) <> 0 THEN IsNull(v.TaxGroupID,VC.TaxGroupID) ELSE VC.TaxGroupID END AS TaxGroupID\r\n                            FROM Vendor V LEFT OUTER JOIN Vendor_Class VC ON V.VendorClassID=VC.ClassID\r\n                            WHERE V.ISINACTIVE<>1 and IsServiceProvider=1 ORDER BY VendorID,VendorName";
			FillDataSet(dataSet, "Vendor", textCommand);
			return dataSet;
		}

		public VendorData GetVendor()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vendor");
			VendorData vendorData = new VendorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vendorData, "Vendor", sqlBuilder);
			return vendorData;
		}

		public VendorData GetServiceProvider()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vendor");
			VendorData vendorData = new VendorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vendorData, "Vendor", sqlBuilder);
			return vendorData;
		}

		public bool DeleteVendor(string vendorID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM UDF_Vendor  WHERE EntityID = '" + vendorID + "'";
				flag = Delete(commandText, trans);
				commandText = "DELETE FROM Vendor_Address  WHERE VendorID = '" + vendorID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Vendor_Contact_Detail  WHERE VendorID = '" + vendorID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Vendor WHERE VendorID = '" + vendorID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Vendor", vendorID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool DeleteServiceProvider(string vendorID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM UDF_Vendor  WHERE EntityID = '" + vendorID + "'";
				flag = Delete(commandText, trans);
				commandText = "DELETE FROM Vendor_Address  WHERE VendorID = '" + vendorID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Vendor_Contact_Detail  WHERE VendorID = '" + vendorID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Vendor WHERE VendorID = '" + vendorID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Vendor", vendorID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public VendorData GetVendorByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "VendorID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Vendor";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			VendorData vendorData = new VendorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vendorData, "Vendor", sqlBuilder);
			if (vendorData == null || vendorData.Tables.Count == 0 || vendorData.Tables[0].Rows.Count == 0)
			{
				return vendorData;
			}
			string a = "";
			if (a == "")
			{
				a = "PRIMARY";
			}
			a = vendorData.Tables["Vendor"].Rows[0]["PrimaryAddressID"].ToString();
			string textCommand = "SELECT * FROM Vendor_Address\r\n                            WHERE VendorID='" + id + "' AND AddressID='" + a + "'";
			FillDataSet(vendorData, "Vendor_Address", textCommand);
			textCommand = "SELECT     CD.VendorID, CD.ContactID, CD.JobTitle,CD.Note, C.FirstName,C.LastName,C.Country,C.City,C.Phone1,C.Phone2,C.Email1\r\n                        FROM Vendor_Contact_Detail AS CD INNER JOIN Contact C ON C.ContactID = CD.ContactID \r\n                            WHERE VendorID='" + id + "'  ORDER BY RowIndex";
			FillDataSet(vendorData, "Vendor_Contact_Detail", textCommand);
			textCommand = "SELECT * FROM UDF_Vendor WHERE EntityID = '" + id + "'";
			FillDataSet(vendorData, "UDF", textCommand);
			return vendorData;
		}

		public VendorData GetServiceProviderByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "VendorID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Vendor";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			VendorData vendorData = new VendorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vendorData, "Vendor", sqlBuilder);
			if (vendorData == null || vendorData.Tables.Count == 0 || vendorData.Tables[0].Rows.Count == 0)
			{
				return vendorData;
			}
			string a = "";
			if (a == "")
			{
				a = "PRIMARY";
			}
			a = vendorData.Tables["Vendor"].Rows[0]["PrimaryAddressID"].ToString();
			string textCommand = "SELECT * FROM Vendor_Address\r\n                            WHERE VendorID='" + id + "' AND AddressID='" + a + "'";
			FillDataSet(vendorData, "Vendor_Address", textCommand);
			textCommand = "SELECT     CD.VendorID, CD.ContactID, CD.JobTitle,CD.Note, C.FirstName,C.LastName,C.Country,C.City,C.Phone1,C.Phone2,C.Email1\r\n                        FROM Vendor_Contact_Detail AS CD INNER JOIN Contact C ON C.ContactID = CD.ContactID\r\n                            WHERE  VendorID='" + id + "' ORDER BY RowIndex";
			FillDataSet(vendorData, "Vendor_Contact_Detail", textCommand);
			textCommand = "SELECT * FROM UDF_Vendor WHERE EntityID = '" + id + "'";
			FillDataSet(vendorData, "UDF", textCommand);
			return vendorData;
		}

		public DataSet GetVendorByFields(params string[] columns)
		{
			return GetVendorByFields(null, isInactive: true, columns);
		}

		public DataSet GetVendorByFields(string[] vendorID, params string[] columns)
		{
			return GetVendorByFields(vendorID, isInactive: true, columns);
		}

		public DataSet GetVendorByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vendor");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "VendorID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Vendor";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Vendor";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Vendor", sqlBuilder);
			return dataSet;
		}

		public DataSet GetVendorList(bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  Vendor.IsInactive AS [I],Vendor.VendorID AS [Vendor Code], Vendor.VendorName AS Name,Vendor.TaxIDNumber AS [Tax ID No], CA.City,Country.CountryName AS Country,Area.AreaName AS Area, CA.Phone1 AS Phone, CA.Mobile, \r\n                        CA.Fax, CA.Email\r\n                        FROM  Vendor INNER JOIN\r\n                        Vendor_Address AS CA ON Vendor.VendorID = CA.VendorID AND CA.AddressID = 'PRIMARY' \r\n                        LEFT OUTER JOIN Country ON Country.CountryID = Vendor.CountryID\r\n                        LEFT OUTER JOIN Area ON Area.AreaID = Vendor.AreaID\r\n                        \r\n                        ";
			if (!showInactive)
			{
				text += " WHERE ISNULL(Vendor.IsInactive,'False')='False'";
			}
			FillDataSet(dataSet, "Vendor", text);
			return dataSet;
		}

		public DataSet GetServiceProviderList(bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  Vendor.VendorID AS [Vendor Code], Vendor.VendorName AS Name, CA.City,Country.CountryName AS Country,Area.AreaName AS Area, CA.Phone1 AS Phone, CA.Mobile, \r\n                        CA.Fax, CA.Email\r\n                        FROM  Vendor INNER JOIN\r\n                        Vendor_Address AS CA ON Vendor.VendorID = CA.VendorID AND CA.AddressID = 'PRIMARY' \r\n                        LEFT OUTER JOIN Country ON Country.CountryID = Vendor.CountryID\r\n                        LEFT OUTER JOIN Area ON Area.AreaID = Vendor.AreaID\r\n                        \r\n                        ";
			if (!showInactive)
			{
				text += " WHERE ISNULL(Vendor.IsInactive,'False')='False' and IsServiceProvider=1";
			}
			FillDataSet(dataSet, "Vendor", text);
			return dataSet;
		}

		public DataSet GetServiceProviderList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Vendor.VendorID AS [Vendor Code], Vendor.VendorName AS Name, CA.City,Country.CountryName AS Country,Area.AreaName AS Area, CA.Phone1 AS Phone, CA.Mobile, \r\n                        CA.Fax, CA.Email, Vendor.IsInactive AS [I]\r\n                        FROM  Vendor INNER JOIN\r\n                        Vendor_Address AS CA ON Vendor.VendorID = CA.VendorID AND CA.AddressID = 'PRIMARY' \r\n                        LEFT OUTER JOIN Country ON Country.CountryID = Vendor.CountryID\r\n                        LEFT OUTER JOIN Area ON Area.AreaID = Vendor.AreaID\r\n                        WHERE ISNULL(Vendor.IsInactive,'False')='False' and IsServiceProvider=1";
			FillDataSet(dataSet, "Vendor", textCommand);
			return dataSet;
		}

		public DataSet GetVendorComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VendorID [Code],VendorName + (CASE WHEN ISNULL(LegalName,'')='' THEN '' ELSE ' [' + LegalName + ']' END) [Name],REPLACE(VendorID,' ','') + REPLACE(VendorName,' ','') + REPLACE(LegalName,' ','') AS SearchColumn ,ISNULL(CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID, \r\n                             ParentVendorID,AllowConsignment,AllowOAP,ConsignComPercent, ShippingMethodID, PaymentTermID, PaymentMethodID, BuyerID, PrimaryAddressID,VendorClassID,\r\n                                CASE WHEN ISNULL(V.TaxOption,0) <> 0 THEN V.TaxOption ELSE ISNULL(VC.TaxOption,2) END AS TaxOption, \r\n                                CASE WHEN ISNULL(V.TaxOption,0) <> 0 THEN IsNull(v.TaxGroupID,VC.TaxGroupID) ELSE VC.TaxGroupID END AS TaxGroupID\r\n                            FROM Vendor V LEFT OUTER JOIN Vendor_Class VC ON V.VendorClassID=VC.ClassID\r\n                            WHERE V.ISINACTIVE<>1 ORDER BY VendorID,VendorName";
			FillDataSet(dataSet, "Vendor", textCommand);
			return dataSet;
		}

		public DataSet GetVendorSelectionList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VendorID [Code],VendorName [Name] FROM Vendor\r\n                            WHERE ISINACTIVE<>1 ORDER BY VendorID,VendorName";
			FillDataSet(dataSet, "Vendor", textCommand);
			return dataSet;
		}

		public string GetVendorAPAccountID(string sysDocID, string vendorID)
		{
			string exp = "    SELECT ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID FROM  Vendor VEN \r\n                                LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                              LEFT OUTER JOIN Location LOC ON Loc.LocationID  = (SELECT LocationID FROM System_Document WHERE SysDocID = '" + sysDocID + "')\r\n                              WHERE VendorID = '" + vendorID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return "";
		}

		public DataSet GetVendorBalanceSummary(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, string vendorIDs)
		{
			return GetVendorBalanceSummary(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, showZeroBalance, vendorIDs, isFC: false);
		}

		public DataSet GetVendorBalanceSummary(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, string vendorIDs, bool isFC)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			if (isFC)
			{
				text = "FC";
			}
			string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
			baseCurrencyID = ((!isFC) ? ("'" + baseCurrencyID + "' AS CurrencyID,") : " Vendor.CurrencyID,");
			string str = "SELECT DISTINCT APJ.VendorID [Vendor Code] ,VendorName AS [Vendor Name] ,";
			str += baseCurrencyID;
			str = str + " ISNULL((SELECT SUM(ISNULL(Credit" + text + ",0)- ISNULL(Debit" + text + ",0)) ";
			str += " FROM APJournal APJ2 \r\n                             WHERE ISNULL(IsNonStatement,'False') = 'False' AND  APJ.VendorID=APJ2.VendorID \r\n                             AND ISNULL(IsVoid,'False')='False'),0)\r\n                             AS [Net Balance], PT.TermName\r\n                             FROM APJournal APJ INNER JOIN Vendor ON APJ.VendorID=Vendor.VendorID\r\n                               LEFT OUTER JOIN Payment_Term PT ON PT.PaymentTermID=Vendor.PaymentTermID\r\n                              WHERE ISNULL(IsNonStatement,'False') = 'False' AND  ISNULL(IsVoid,'False')='False'  ";
			if (vendorIDs != "")
			{
				str = str + " AND APJ.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND APJ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (!showZeroBalance)
			{
				str = str + "           and\r\n                             ISNULL((SELECT SUM(ISNULL(Credit" + text + ",0)- ISNULL(Debit" + text + ",0)) ";
			}
			if (!isFC)
			{
				str += " + ISNULL((SELECT SUM(ISNULL(RealizedGainLoss,0)) FROM AP_Payment_Allocation APP WHERE APP.VendorID=APJ.VendorID),0) ";
			}
			str += " FROM APJournal APJ2 \r\n                             WHERE ISNULL(IsNonStatement,'False') = 'False' AND APJ.VendorID=APJ2.VendorID \r\n                             AND ISNULL(IsVoid,'False')='False'),0) <> 0 ";
			str += "ORDER BY APJ.VendorID ";
			FillDataSet(dataSet, "Vendor", str);
			return dataSet;
		}

		public DataSet GetVendorDueBalanceSummary(string vendorID, string currencyID, DateTime asOfDate)
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			decimal num6 = default(decimal);
			decimal num7 = default(decimal);
			string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
			string text = CommonLib.ToSqlDateTimeString(asOfDate);
			bool flag = false;
			if (currencyID != "" && currencyID != baseCurrencyID)
			{
				flag = true;
			}
			string text2 = "";
			if (flag)
			{
				text2 = "FC";
			}
			string exp = " SELECT  SUM(ISNULL(Credit" + text2 + ",0)- ISNULL(Debit" + text2 + ",0))  FROM APJournal \r\n\t                              WHERE ISNULL(IsNonStatement,'False') = 'False' AND   ISNULL(isvoid,'False') = 'False'  AND VendorID  = '" + vendorID + "' ";
			if (flag)
			{
				exp = " SELECT  SUM(ISNULL(Credit" + text2 + ",0)+ISNULL(ConCredit" + text2 + ",0)- ISNULL(Debit" + text2 + ",0)- ISNULL(ConDebit" + text2 + ",0))  FROM APJournal \r\n\t                              WHERE ISNULL(IsNonStatement,'False') = 'False' AND   ISNULL(isvoid,'False') = 'False'  AND VendorID  = '" + vendorID + "' ";
			}
			object obj = ExecuteScalar(exp);
			if (!obj.IsNullOrEmpty())
			{
				num = decimal.Parse(obj.ToString());
			}
			exp = "  SELECT SUM(ISNULL(AP.Balance,0) - ISNULL(AP.Paid,0)) AS BalanceDue FROM\r\n                            (SELECT APJ.VendorID, apid,SUM(ISNULL(Credit" + text2 + ",0)- ISNULL(Debit" + text2 + ",0)) AS Balance,\r\n                            (SELECT SUM(app.PaymentAmount" + text2 + ") from AP_Payment_Allocation APP WHERE APP.APJournalID = APJ.APID) AS Paid FROM APJournal APJ\r\n\t                              WHERE ISNULL(IsNonStatement,'False') = 'False' AND  Credit IS NOT NULL AND ISNULL(isvoid,'False') = 'False'  AND VendorID  = '" + vendorID + "'\r\n\t\t\t\t\t\t\t\t  AND APDueDate < '" + text + "'\r\n\t\t\t\t\t\t\t\t  GROUP BY VendorID,apj.apid) AS AP";
			obj = ExecuteScalar(exp);
			if (!obj.IsNullOrEmpty())
			{
				num2 = decimal.Parse(obj.ToString());
			}
			exp = "  SELECT SUM(AP.Unallocated) FROM \r\n                        (SELECT SUM(ISNULL(Debit" + text2 + ",0)- ISNULL(Credit" + text2 + ",0)) - ISNULL((SELECT SUM(PaymentAmount" + text2 + ") FROM AP_Payment_Allocation APP WHERE APP.PaymentAPID = APJ.APID),0) AS Unallocated\r\n                         FROM  APJournal APJ WHERE Debit IS NOT NULL AND  ISNULL(isvoid,'False') = 'False' AND  ISNULL(IsNonStatement,'False') = 'False'   AND VendorID  = '" + vendorID + "'\r\n  \t                          GROUP BY VendorID,APJ.ApID\r\n                         Having SUM(ISNULL(Debit" + text2 + ",0)- ISNULL(Credit" + text2 + ",0)) - ISNULL((SELECT SUM(PaymentAmount" + text2 + ") FROM AP_Payment_Allocation APP WHERE APP.PaymentAPID = APJ.APID),0) > 0) AS AP";
			obj = ExecuteScalar(exp);
			if (!obj.IsNullOrEmpty())
			{
				num3 = decimal.Parse(obj.ToString());
			}
			exp = "SELECT SUM(PC.Claimamount) AS Amount FROM Purchase_claim PC\r\n                    LEFT JOIN Purchase_Receipt PR ON PR.SysDocID=PC.SourceSysdocid AND PR.VoucherID=PC.SourceVoucherID\r\n                    WHERE PC.ClaimStatus=1 AND PR.VendorID='" + vendorID + "'";
			obj = ExecuteScalar(exp);
			if (!obj.IsNullOrEmpty())
			{
				num4 = decimal.Parse(obj.ToString());
			}
			exp = "SELECT SUM(ClaimAmount)  AS Amount FROM Quality_Claim  WHERE  ClaimStatus=1 AND VendorID='" + vendorID + "'";
			obj = ExecuteScalar(exp);
			if (!obj.IsNullOrEmpty())
			{
				num5 = decimal.Parse(obj.ToString());
			}
			exp = "SELECT COUNT( DISTINCT PC.VOUCHERID) AS PCCount FROM Purchase_claim PC\r\n                    LEFT JOIN Purchase_Receipt PR ON PR.SysDocID=PC.SourceSysdocid AND PR.VoucherID=PC.SourceVoucherID\r\n                    WHERE PC.ClaimStatus=1 AND PR.VendorID='" + vendorID + "'";
			obj = ExecuteScalar(exp);
			if (!obj.IsNullOrEmpty())
			{
				num6 = decimal.Parse(obj.ToString());
			}
			exp = "SELECT COUNT( DISTINCT VOUCHERID)  AS QCCount FROM Quality_Claim  WHERE ClaimStatus=1 AND VendorID='" + vendorID + "'";
			obj = ExecuteScalar(exp);
			if (!obj.IsNullOrEmpty())
			{
				num7 = decimal.Parse(obj.ToString());
			}
			exp = "select  ISNULL(SUM(Amount),0) as PaymentRequested,ISNULL(SUM(AmountFC),0) as PaymentRequestedFC FROM Payment_Request WHERE PAYEEID\r\n                    IN (SELECT PAYEEID FROM Payment_Request WHERE  PAYEEID='" + vendorID + "') AND Status=1 AND ISNULL(isvoid,'False') = 'False'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "paymentrequestDtls", exp);
			DataSet dataSet2 = new DataSet();
			DataTable dataTable = dataSet2.Tables.Add("Vendor");
			dataTable.Columns.Add("VendorID");
			dataTable.Columns.Add("Balance", typeof(decimal));
			dataTable.Columns.Add("BalanceDue", typeof(decimal));
			dataTable.Columns.Add("Unallocated", typeof(decimal));
			dataTable.Columns.Add("Purchaseclaimamt", typeof(decimal));
			dataTable.Columns.Add("Qualityclaimamt", typeof(decimal));
			dataTable.Columns.Add("PCCount", typeof(decimal));
			dataTable.Columns.Add("QCCount", typeof(decimal));
			dataTable.Rows.Add(vendorID, num, num2, num3, num4, num5, num6, num7);
			dataSet2.Merge(dataSet);
			return dataSet2;
		}

		public DataSet GetVendorBalanceAmount(string vendorID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DISTINCT CUS.VendorID,VendorName,CreditAmount,IsInactive,IsHold,CreditLimitType,ParentVendorID,\r\n                              ISNULL((SELECT  SUM(ISNULL(Debit,0)- ISNULL(Credit,0))  FROM APJournal \r\n\t                              WHERE ISNULL(IsNonStatement,'False') = 'False' AND  VendorID = CUS.VendorID and ISNULL(isvoid,'False') = 'False' ),0) +\r\n\t\t\t\t\t\t\t\t   ISNULL((SELECT  SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) AS Balance FROM APJournal  WHERE ISNULL(IsNonStatement,'False') = 'False' AND  VendorID IN \r\n\t\t\t\t\t\t\t\t (Select VendorID FROM Vendor WHERE ParentVendorID = CUS.VendorID)  and ISNULL(isvoid,'False') = 'False'),0) AS Balance,\r\n                                 ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) AS Amount FROM Cheque_Issued ChqRec\r\n                                 WHERE Status IN (1) AND ISNULL(IsVoid,'False')='False' AND PayeeType='C' AND PayeeID = CUS.VendorID),0) + \r\n\t\t\t\t\t\t\t\t ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) AS Amount FROM Cheque_Issued ChqRec\r\n                                 WHERE Status IN (1) AND ISNULL(IsVoid,'False')='False' AND PayeeType='C' AND PayeeID IN (Select VendorID FROM Vendor WHERE ParentVendorID = CUS.VendorID)),0)  AS PDCAmount,\r\n                                 AcceptCheckPayment,AcceptPDC,CUS.CurrencyID \r\n                                 FROM  Vendor CUS  WHERE CUS.VendorID = '" + vendorID + "'  \r\n\t                             GROUP BY CUS.VendorID,CUS.ParentVendorID,VendorName,CreditAmount,IsInactive,IsHold,AcceptCheckPayment,AcceptPDC,CUS.CurrencyID ,CreditLimitType";
			FillDataSet(dataSet, "Vendor", textCommand);
			return dataSet;
		}

		public DataSet GetVendorAgingBalanceList(bool showZeroBalance, bool isFC)
		{
			DateTime asOfDate = DateTime.Now.EndOfDay();
			return GetVendorAgingBalanceList("", "", "", "", "", "", asOfDate, showZeroBalance, isFC, includeAgingTable: false, "");
		}

		public DataSet GetVendorAgingBalanceList(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, bool isFC, bool includeAgingTable, string vendorIDs)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
			_ = (asOfDate < DateTime.Today);
			string text = (!(asOfDate == DateTime.MaxValue)) ? CommonLib.ToSqlDateTimeString(asOfDate) : CommonLib.ToSqlDateTimeString(DateTime.Parse("1-1-2099"));
			string text2 = "";
			string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
			if (isFC)
			{
				text2 = "FC";
			}
			DataSet dataSet = new DataSet();
			string str = "SELECT * FROM (SELECT VEN.IsInactive I,Flag F,CASE WHEN ISNULL(IsHold,'False')='True' THEN 'Hold' ELSE 'Active' END AS Status, Ven.VendorID ,\r\n                            CASE WHEN  ShortName IS NULL OR ShortName = '' THEN  VendorName ELSE VendorName + ' [' + ShortName + ']' END AS VendorName, ";
			str = ((!isFC) ? (str + "'" + baseCurrencyID + "' AS CUR ") : (str + "ISNULL(Ven.CurrencyID,'" + baseCurrencyID + "') AS CUR "));
			str = str + " ,(SELECT TOP 1 APJ.APDate from APJournal APJ where  ISNULL(APJ.Debit,0)<>0 AND APJ.VendorID=Ven.VendorID order by APJ.APDate desc) as LastPaymentDate,\r\n                            (SELECT TOP 1 APJ.APDate from APJournal APJ where  ISNULL(APJ.Credit,0)<>0 AND APJ.VendorID=Ven.VendorID order by APJ.APDate desc) as LastPurchaseDate,\r\n                            0.00 AS CurrentBalance, 0.00 AS Month1, 0.00 AS Month2,0.00 AS Month3,0.00 AS Month4,0.00 AS Month5,0.00 AS Month6,0.00 AS [Over], \r\n                             CASE WHEN VEN.CurrencyID IS NULL OR VEN.CurrencyID = '" + baseCurrencyID + "' THEN\r\n                             ISNULL((SELECT SUM(ISNULL(Credit ,0)- ISNULL(Debit  ,0)) FROM APJournal ARJ WHERE ISNULL(IsNonStatement,'False') = 'False' AND  VEN.VendorID=ARJ.VendorID  AND ISNULL(ISPDCRow,'False') = 'False' AND ISNULL(IsVoid,'False')='False' AND ARJ.APDate <= '" + text + "'),0) ELSE\r\n\t\t\t\t\t\t\t ISNULL((SELECT SUM(ISNULL(Credit" + text2 + " ,0)- ISNULL(Debit" + text2 + " ,0)) FROM APJournal ARJ WHERE ";
			if (isFC)
			{
				str += " ARJ.CurrencyID = VEN.CurrencyID AND ";
			}
			str = str + " ISNULL(IsNonStatement,'False') = 'False' AND  VEN.VendorID=ARJ.VendorID  AND  ISNULL(ISPDCRow,'False') = 'False' AND ISNULL(IsVoid,'False')='False' AND ARJ.APDate<='" + text + "'),0)  ";
			if (isFC)
			{
				str = str + " + ISNULL((SELECT SUM(ISNULL(ConCreditFC ,0)- ISNULL(ConDebitFC ,0)) FROM APJournal ARJ WHERE ISNULL(IsNonStatement,'False') = 'False' AND  ISNULL(ARJ.CurrencyID,'" + baseCurrencyID + "') <> VEN.CurrencyID AND  VEN.VendorID=ARJ.VendorID AND ISNULL(ISPDCRow,'False') = 'False' AND ISNULL(IsVoid,'False')='False'  AND ARJ.APDate<='" + text + "'),0) ";
			}
			str = str + "END AS TotalBalance,\r\n                              0.0 AS Unallocated, 0.0 AS TotalDue,\r\n                            (SELECT  SUM( ISNULL(BFT.Amount" + text2 + " ,0)) FROM TR_Application BFT INNER JOIN Bank_Facility BF  ON BF.FacilityID=BFT.BankFacilityID WHERE ISNULL(IsVoid,'False') = 'False' AND VoucherID NOT IN ( SELECT  SourceVoucherID from Bank_Facility_Transaction\r\n                            TR INNER JOIN TR_Application TRA ON TR.SourceSysDocID=TRA.SysDocID AND  TR.SourceVoucherID=TRA.VoucherID  )  \r\n                            AND VEN.VendorID = BFT.PayeeID )AS UnProcessedTR,\r\n                            (select   sum( ISNULL(PPI.Amount" + text2 + " ,0))  from Purchase_Prepayment_Invoice  PPI  \r\n                             where ISNULL(PPI.IsVoid,'False')='False' AND ppi.Status  is null and PPI.VendorID=Ven.VendorID) AS Prepayment,\r\n\t                          (Select SUM(Amount) FROM Cheque_Issued CR WHERE Status IN (1,2,5) AND CR.PayeeType = 'V' AND VEN.VendorID = CR.PayeeID  AND ISNULL(IsVoid,'False') = 'False') AS PDC , 0.00 AS NetOffPDC,\r\n\t                          VEN.PaymentTermID,PT.TermName, VEN.CreditAmount ,VEN.AcceptPDC,   CON.CountryName Country,Area.AreaName Area,CLS.ClassName Class,\r\n\t                           CA.ContactName,CA.ContactTitle,CA.Email,CA.Mobile,CA.Phone1,CA.Phone2,CA.Fax,CA.PostalCode,\r\n                                (SELECT (cast(CC.CategoryName as nvarchar)+', ') as [text()] FROM Entity_Category_Detail ccd INNER JOIN Entity_Category CC ON CC.CategoryID=CCD.CategoryID\r\n                                  WHERE ccd.EntityID = Ven.VendorID AND CCD.EntityType = 1 order by ccd.CategoryID for XML PATH('')) as Categories \r\n\r\n                              FROM Vendor Ven\r\n                            LEFT OUTER JOIN Payment_Term PT ON PT.PaymentTermID=Ven.PaymentTermID\r\n                              LEFT OUTER JOIN Area ON Area.AreaID = VEN.AreaID\r\n                              LEFT OUTER JOIN Country CON ON CON.CountryID = VEN.CountryID\r\n                              LEFT OUTER JOIN Vendor_Class CLS ON CLS.ClassID = VEN.VendorClassID\r\n                              LEFT OUTER JOIN Vendor_Address CA ON CA.VendorID = VEN.VendorID AND CA.AddressID = 'PRIMARY'\r\n                         ) AS Vendor \r\n                              WHERE 1 = 1 ";
			if (vendorIDs != "")
			{
				str = str + " AND VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND VendorID >= '" + fromVendor + "' ";
			}
			if (toVendor != "")
			{
				str = str + " AND VendorID <= '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorClassID >= '" + fromClass + "' ";
			}
			if (toClass != "")
			{
				str = str + " AND VendorClassID <= '" + toClass + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorGroupID >= '" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND VendorGroupID <= '" + toGroup + "' ";
			}
			if (!showZeroBalance)
			{
				str += " AND (ISNULL(PDC,0)<>0 OR ISNULL(TotalBalance,0) <> 0 )";
			}
			str += " ORDER BY VendorID,VendorName";
			FillDataSet(dataSet, "Vendor", str);
			string text3 = "0";
			string text4 = "0";
			string text5 = "ISNULL(RealizedGainLoss, 0)";
			if (isFC)
			{
				text3 = "ISNULL(ConDebitFC,0) - CASE WHEN ConDebitFC IS NOT NULL THEN ISNULL(DebitFC,0) ELSE 0 END ";
				text4 = "ISNULL(ConCreditFC,0) - CASE WHEN ConCreditFC IS NOT NULL THEN ISNULL(CreditFC,0) ELSE 0 END";
				text5 = "(CASE WHEN ISNULL(V.CurrencyID,'" + baseCurrencyID + "') = '" + baseCurrencyID + "' THEN ISNULL(RealizedGainLoss, 0) ELSE 0 END)";
			}
			str = "  SELECT APID, ARJ.VendorID,APDate,ARJ.SysDocID,ARJ.VoucherID,ARJ.Reference,  APDueDate,\r\n\t\t\t\t\t\t\t\tCASE WHEN Ven.CurrencyID IS NULL OR Ven.CurrencyID = '" + baseCurrencyID + "' THEN\r\n                                  ISNULL(Credit,0) -  (SELECT ISNULL(SUM(ISNULL(PaymentAmount,0)- " + text5 + "),0) FROM AP_Payment_Allocation ARP INNER JOIN Vendor V ON V.VendorID =ARP.VendorID WHERE ARJ.APID = ARP.APJournalID  AND ARJ.APDate<='" + text + "'   AND ARP.AllocationDate<='" + text + "') ELSE\t\t\r\n\t\t\t\t\t\t\t\t   ISNULL(Credit" + text2 + ",0)  + " + text4 + " -  (SELECT ISNULL(SUM(ISNULL(PaymentAmount" + text2 + ",0)-" + text5 + "),0) FROM AP_Payment_Allocation ARP INNER JOIN Vendor V ON V.VendorID =ARP.VendorID WHERE ARJ.APID = ARP.APJournalID  AND ARJ.APDate<='" + text + "'   AND ARP.AllocationDate<='" + text + "')\tEND\t AS AmountDue\r\n                                  FROM APJournal ARJ   \r\n                                LEFT OUTER JOIN System_Document SD ON ARJ.SysDocID = SD.SysDocID\r\n\t\t\t\t\t\t\t\tINNER JOIN Vendor VEN ON Ven.VendorID = ARJ.VendorID\r\n                                WHERE    ISNULL(Credit,0)>0 AND ISNULL(IsVoid,'False')='False' AND  ISNULL(SD.SysDocType,1) NOT IN ( 14,16 ,12)  AND ARJ.APDate <= '" + text + "'\r\n                                AND (SELECT   CASE WHEN Ven.CurrencyID IS NULL OR Ven.CurrencyID = '" + baseCurrencyID + "' THEN ISNULL(SUM(ISNULL(PaymentAmount,0) - " + text5 + "),0) ELSE\r\n\t\t\t\t\t\t\t\t\t ISNULL(SUM(ISNULL(PaymentAmount" + text2 + ",0)),0) END  FROM AP_Payment_Allocation PA INNER JOIN Vendor V ON V.VendorID =PA.VendorID\r\n\t                                WHERE    ARJ.APID = PA.APJournalID AND ARJ.APDate<='" + text + "'   AND PA.AllocationDate<='" + text + "') < (CASE WHEN Ven.CurrencyID IS NULL OR Ven.CurrencyID = '" + baseCurrencyID + "' THEN ISNULL(Credit,0) ELSE  ISNULL(Credit" + text2 + ",0) + " + text4 + " END) ";
			if (isFC)
			{
				str = str + " AND ISNULL(ARJ.CurrencyID,'" + baseCurrencyID + "') =  ISNULL(Ven.CurrencyID,'" + baseCurrencyID + "') ";
			}
			str += " and  ARJ.SysDocID <> 'SYS_VAL' ";
			str = str + "                UNION\r\n\t     \r\n                            SELECT APID, ARJ.VendorID,APDate,ARJ.SysDocID,ARJ.VoucherID,ARJ.Reference, APDueDate, \r\n                                -1 * CASE WHEN Ven.CurrencyID IS NULL OR Ven.CurrencyID = '" + baseCurrencyID + "' THEN ISNULL(Debit,0) ELSE ISNULL(Debit" + text2 + ",0) + " + text3 + " END  \r\n                                + (SELECT CASE WHEN Ven.CurrencyID IS NULL OR Ven.CurrencyID = '" + baseCurrencyID + "' THEN ISNULL(SUM(ISNULL(PaymentAmount,0)),0) ELSE ISNULL(SUM(ISNULL(PaymentAmount" + text2 + ",0)),0) END FROM AP_Payment_Allocation ARP\r\n                                \r\n\t\t\t\t\t\t\t\tWHERE ARJ.SysDocID=ARP.PaymentSysDocID AND ARJ.VoucherID=ARP.PaymentVoucherID  AND ARJ.APID = ARP.PaymentAPID AND ARJ.VendorID = ARP.VendorID  AND ARJ.APDate<='" + text + "'   AND ARP.AllocationDate<='" + text + "')  AS Unallocated\r\n                                  FROM APJournal ARJ   \r\n                                LEFT OUTER JOIN System_Document SD ON ARJ.SysDocID = SD.SysDocID\r\n                                INNER JOIN Vendor Ven ON ARJ.VendorID=Ven.VendorID\r\n                                WHERE    CASE WHEN Ven.CurrencyID IS NULL OR Ven.CurrencyID = '" + baseCurrencyID + "' THEN  ISNULL(Debit,0) ELSE ISNULL(Debit" + text2 + ",0) + " + text3 + " END >0 AND ISNULL(IsVoid,'False')='False'  AND ISNULL(SD.SysDocType,1)  NOT IN ( 12,14,16 )  --excempted sysdoctypes Need to be changed ,\r\n                                AND (SELECT CASE WHEN Ven.CurrencyID IS NULL OR Ven.CurrencyID = '" + baseCurrencyID + "' THEN  ISNULL(SUM(ISNULL(PaymentAmount,0)),0) ELSE  ISNULL(SUM(ISNULL(PaymentAmount" + text2 + ",0)),0) END FROM AP_Payment_Allocation PA\r\n\t                                WHERE PA.PaymentSysDocID=ARJ.SysDocID AND PA.PaymentVoucherID=ARJ.VoucherID  AND PA.PaymentAPID= ARJ.APID AND PA.VendorID = ARJ.VendorID  AND ARJ.APDate<='" + text + "'   AND PA.AllocationDate<='" + text + "')<  CASE WHEN Ven.CurrencyID IS NULL OR Ven.CurrencyID = '" + baseCurrencyID + "' THEN  ISNULL(ISNULL(Debit,0),0) ELSE  ISNULL(ISNULL(Debit" + text2 + ",0)+  " + text3 + ",0) END ";
			if (vendorIDs != "")
			{
				str = str + " AND VEN.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND VEN.VendorID >= '" + fromVendor + "' ";
			}
			if (toVendor != "")
			{
				str = str + " AND VEN.VendorID <= '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorClassID >= '" + fromClass + "' ";
			}
			if (toClass != "")
			{
				str = str + " AND VendorClassID <= '" + toClass + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorGroupID >= '" + fromGroup + "' ";
			}
			str = str + " and  ARJ.SysDocID <> 'SYS_VAL'  AND ARJ.APDate<='" + text + "' ";
			if (isFC)
			{
				str = str + " AND ISNULL(ARJ.CurrencyID,'" + baseCurrencyID + "') =  ISNULL(Ven.CurrencyID,'" + baseCurrencyID + "') ";
			}
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Aging", str);
			DataHelper dataHelper = new DataHelper(base.DBConfig);
			DataSet dataSet3 = new DataSet();
			dataSet3 = new CompanyOption(base.DBConfig).GetCompanyOptionList(76, 107);
			bool companyOption = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingByDueDate, defaultValue: true);
			bool companyOption2 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging1, defaultValue: true);
			bool companyOption3 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging2, defaultValue: true);
			bool companyOption4 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging3, defaultValue: true);
			bool companyOption5 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging4, defaultValue: true);
			bool companyOption6 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging5, defaultValue: true);
			bool companyOption7 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging6, defaultValue: true);
			dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom0, 0);
			int companyOption8 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom1, 1);
			int companyOption9 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom2, 31);
			int companyOption10 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom3, 61);
			int companyOption11 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom4, 91);
			int companyOption12 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom5, 121);
			int companyOption13 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom6, 151);
			dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom7, 181);
			int companyOption14 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo0, 0);
			int companyOption15 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo1, 30);
			int companyOption16 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo2, 60);
			int companyOption17 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo3, 90);
			int companyOption18 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo4, 120);
			int companyOption19 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo5, 150);
			int companyOption20 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo6, 180);
			dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo7, 999);
			int num = 1;
			int num2 = 0;
			if (companyOption7)
			{
				num = 6;
				num2 = companyOption20;
			}
			else if (companyOption6)
			{
				num = 5;
				num2 = companyOption19;
			}
			else if (companyOption5)
			{
				num = 4;
				num2 = companyOption18;
			}
			else if (companyOption4)
			{
				num = 3;
				num2 = companyOption17;
			}
			else if (companyOption3)
			{
				num = 2;
				num2 = companyOption16;
			}
			else if (companyOption2)
			{
				num = 1;
				num2 = companyOption15;
			}
			else
			{
				num = 0;
				num2 = companyOption14;
			}
			for (int i = 0; i < dataSet.Tables["Vendor"].Rows.Count; i++)
			{
				DataRow dataRow = dataSet.Tables["Vendor"].Rows[i];
				string str2 = dataRow["VendorID"].ToString();
				_ = DateTime.Today;
				DataRow[] array = dataSet2.Tables["Aging"].Select("VendorID = '" + str2 + "'");
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				decimal num5 = default(decimal);
				decimal num6 = default(decimal);
				decimal num7 = default(decimal);
				decimal num8 = default(decimal);
				decimal num9 = default(decimal);
				decimal num10 = default(decimal);
				decimal num11 = default(decimal);
				decimal d = default(decimal);
				for (int j = 0; j < array.Length; j++)
				{
					DateTime d2 = DateTime.Parse(array[j]["APDate"].ToString());
					if (companyOption && array[j]["APDueDate"] != DBNull.Value)
					{
						d2 = DateTime.Parse(array[j]["APDueDate"].ToString());
					}
					if (!companyOption)
					{
						d2 = d2.AddDays(companyOption14);
					}
					if (array[j]["AmountDue"] != DBNull.Value && decimal.Parse(array[j]["AmountDue"].ToString()) > 0m)
					{
						decimal.Parse(array[j]["AmountDue"].ToString());
						int num12 = (asOfDate - d2).Days - companyOption14;
						if (num12 <= 0)
						{
							num3 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption8 && num12 <= companyOption15)
						{
							num5 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption9 && num12 <= companyOption16)
						{
							num6 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption10 && num12 <= companyOption17)
						{
							num7 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption11 && num12 <= companyOption18)
						{
							num8 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption12 && num12 <= companyOption19)
						{
							num9 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption13 && num12 <= companyOption20)
						{
							num10 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						if (num12 > num2)
						{
							num4 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						if (num12 >= companyOption8)
						{
							d += decimal.Parse(array[j]["AmountDue"].ToString());
						}
					}
					else if (array[j]["AmountDue"] != DBNull.Value)
					{
						num11 += Math.Abs(decimal.Parse(array[j]["AmountDue"].ToString()));
					}
				}
				decimal result = default(decimal);
				decimal.TryParse(dataRow["TotalBalance"].ToString(), out result);
				dataRow["CurrentBalance"] = num3;
				dataRow["Month1"] = num5;
				dataRow["Month2"] = num6;
				dataRow["Month3"] = num7;
				dataRow["Month4"] = num8;
				dataRow["Month5"] = num9;
				dataRow["Month6"] = num10;
				dataRow["Over"] = num4;
				dataRow["Unallocated"] = num11;
				if (d - num11 > 0m)
				{
					dataRow["TotalDue"] = d - num11;
				}
				else
				{
					dataRow["TotalDue"] = 0;
				}
				decimal result2 = default(decimal);
				decimal.TryParse(dataRow["PDC"].ToString(), out result2);
				if (flag)
				{
					dataRow["NetOffPDC"] = result;
				}
				else
				{
					dataRow["NetOffPDC"] = result + result2;
				}
				if (!showZeroBalance && result == 0m && result2 == 0m)
				{
					dataSet.Tables["Vendor"].Rows.RemoveAt(i);
					i--;
				}
			}
			for (int num13 = 6; num13 > num; num13--)
			{
				dataSet.Tables["Vendor"].Columns.Remove("Month" + num13.ToString());
			}
			if (includeAgingTable)
			{
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("AgingRel", new DataColumn[1]
				{
					dataSet.Tables["Vendor"].Columns["VendorID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Aging"].Columns["VendorID"]
				}, createConstraints: false);
			}
			return dataSet;
		}

		public bool SetFlag(string vendorID, byte flagID)
		{
			try
			{
				string exp = (flagID <= 0) ? ("UPDATE Vendor SET Flag = NULL WHERE VendorID = '" + vendorID + "'") : ("UPDATE Vendor SET Flag = " + flagID + " WHERE VendorID = '" + vendorID + "'");
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool HasBalance(string vendorID)
		{
			try
			{
				DataSet vendorBalanceAmount = GetVendorBalanceAmount(vendorID);
				decimal result = default(decimal);
				if (vendorBalanceAmount == null || vendorBalanceAmount.Tables[0].Rows.Count == 0)
				{
					return false;
				}
				decimal.TryParse(vendorBalanceAmount.Tables[0].Rows[0]["Balance"].ToString(), out result);
				if (result != 0m)
				{
					return true;
				}
				return false;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetVendorBalanceDetailFCReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string vendorIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT DISTINCT APJ.VendorID [Vendor Code] ,VendorName AS [Vendor Name] ,AddressPrintFormat,\r\n                        ISNULL((SELECT SUM(ISNULL(CreditFC,0)- ISNULL(DebitFC,0))\r\n                            FROM APJournal APJ2 \r\n                         WHERE ISNULL(IsNonStatement,'False') = 'False' AND  APJ.VendorID=APJ2.VendorID AND APJ2.APDate<'" + text + "'  AND APJ.CurrencyID = '" + currencyID + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Opening Balance],\r\n                        ISNULL((SELECT SUM(ISNULL(CreditFC,0)- ISNULL(DebitFC,0))\r\n\r\n                        FROM APJournal APJ2 \r\n                         WHERE ISNULL(IsNonStatement,'False') = 'False' AND  APJ.VendorID=APJ2.VendorID AND APJ2.APDate<='" + text2 + "'  AND APJ.CurrencyID = '" + currencyID + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Ending Balance]\r\n                        FROM APJournal APJ INNER JOIN Vendor ON APJ.VendorID=Vendor.VendorID \r\n                        LEFT OUTER JOIN Vendor_Address CA\r\n                          ON Vendor.VendorID=CA.VendorID WHERE ";
			str = str + " APDate < '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			str = str + " AND APJ.CurrencyID='" + currencyID + "' ";
			if (vendorIDs != "")
			{
				str = str + " AND APJ.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND APJ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromJob != "")
			{
				str = str + " AND APJ.JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			str += " ORDER BY APJ.VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", str);
			DataSet dataSet2 = new DataSet();
			str = "SELECT VendorID [Vendor Code],SysDocType,'' AS [Doc Type], APJ.SysDocID AS [Doc ID],VoucherID [Doc No],APDate AS [Date],\r\n                         ChequeNumber AS [Chq#],ChequeDate AS [Chq Date],Description,Reference,CurrencyID,CurrencyRate, DebitFC AS Debit, CreditFC AS Credit\r\n\r\n                            FROM APJournal APJ LEFT OUTER JOIN System_Document SD ON APJ.SysDocID=SD.SysDocID WHERE \r\n                            ISNULL(IsNonStatement,'False') = 'False' AND  APDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			str = str + " AND CurrencyID='" + currencyID + "' ";
			if (vendorIDs != "")
			{
				str = str + " AND VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " ORDER BY Date";
			FillDataSet(dataSet2, "APJournal", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Balance Detail", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["APJournal"].Columns["Vendor Code"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetVendorBalanceDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "";
			string text4 = "0";
			string text5 = "0";
			if (currencyID != "" && currencyID != new Currencies(base.DBConfig).GetBaseCurrencyID())
			{
				text3 = "FC";
				text4 = " (ISNULL(ConDebitFC,0) - CASE WHEN ConDebitFC IS NOT NULL THEN ISNULL(DebitFC,0) ELSE 0 END) ";
				text5 = " (ISNULL(ConCreditFC,0) - CASE WHEN ConCreditFC IS NOT NULL THEN ISNULL(CreditFC,0) ELSE 0 END)";
			}
			string str = "SELECT DISTINCT APJ.VendorID [Vendor Code] ,VendorName AS [Vendor Name] ,AllowConsignment,AddressPrintFormat,\r\n                        ISNULL((SELECT SUM(ISNULL(Credit" + text3 + ",0) + " + text5 + "- ISNULL(Debit" + text3 + ",0) - " + text4 + ") \r\n\r\n                            FROM APJournal APJ2 \r\n                         WHERE ISNULL(IsNonStatement,'False') = 'False' AND  APJ.VendorID=APJ2.VendorID AND APJ2.APDate<'" + text + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Opening Balance],\r\n                        ISNULL((SELECT SUM(ISNULL(Credit" + text3 + ",0) + " + text5 + " - (ISNULL(Debit" + text3 + ",0) + " + text4 + "))\r\n\r\n                        FROM APJournal APJ2 \r\n                         WHERE ISNULL(IsNonStatement,'False') = 'False' AND  APJ.VendorID=APJ2.VendorID AND APJ2.APDate<='" + text2 + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Ending Balance],\r\n                        (SELECT ISNULL(SUM(ISNULL(AmountFC,Amount)),0) AS PDC FROM Cheque_Issued ChqRec WHERE Status IN (1,3,4) AND ISNULL(IsVoid,'False')='False'  AND ChqRec.PayeeType = 'V' AND ChqRec.PayeeID = APJ.VendorID) AS PDC\r\n                        FROM APJournal APJ INNER JOIN Vendor ON APJ.VendorID=Vendor.VendorID \r\n                       LEFT OUTER JOIN Vendor_Address CA\r\n                            ON Vendor.VendorID=CA.VendorID\r\n                        WHERE ";
			str = str + " APDate < '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromVendor != "")
			{
				str = str + " AND APJ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (text3 != "")
			{
				str = str + " AND APJ.CurrencyID = '" + currencyID + "' ";
			}
			if (fromJob != "")
			{
				str = str + " AND APJ.JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			str += " ORDER BY APJ.VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", str);
			DataSet dataSet2 = new DataSet();
			str = "SELECT APJ.VendorID [Vendor Code],SysDocType,'' AS [Doc Type], APJ.SysDocID AS [Doc ID],APJ.VoucherID [Doc No],APDate AS [Date],J.JobID,J.JobName,\r\n                    PI.Note,(SELECT TOP 1 J.JobID+ ' - '+ J.JobName FROM Job J LEFT JOIN Purchase_Invoice_Detail PRD ON J.JobID=PRD.JobID WHERE PRD.SysDocID = \r\n                    PI.SysDocID AND PRD.VoucherID =PI.VoucherID) AS Project,\r\n                    (SELECT SUBSTRING((SELECT  ',' + S.SourceVoucherID + ''  FROM  Purchase_Receipt S  INNER JOIN Purchase_Invoice  D ON s.InvoiceVoucherID = D.VoucherID AND S.InvoiceSysDocID = D.SysDocID \r\n                     WHERE D.SysDocID = PI.SysDocID AND D.VoucherID = PI.VoucherID FOR XML PATH('')),2,20000)) AS  [LPO],\r\n                (SELECT SUBSTRING((SELECT  ',' + JD.CheckNumber + ''  FROM  Journal_Details JD WHERE JD.SysDocID = APJ.SysDocID AND JD.VoucherID = APJ.VoucherID AND  JD.IsARAP IS NULL FOR XML PATH('')),2,20000)) AS [CheckNumber],\r\n                    ChequeNumber AS [Chq#],ChequeDate AS [Chq Date],CASE WHEN Description = '' THEN PI.Note ELSE Description END AS Description,APJ.Reference,APJ.CurrencyID,APJ.CurrencyRate, DebitFC, CreditFC,\r\n                    ISNULL(Debit" + text3 + ",0) + " + text4 + " AS Debit, ISNULL(Credit" + text3 + ",0) + " + text5 + " AS Credit\r\n                    FROM APJournal APJ LEFT OUTER JOIN System_Document SD ON APJ.SysDocID=SD.SysDocID \r\n                    LEFT OUTER JOIN Purchase_Invoice PI ON PI.SysDocID=APJ.SysDocID AND PI.VoucherID=APJ.VoucherID\r\n                    LEFT JOIN Job J on APJ.JobID=J.JobID \r\n                    WHERE ISNULL(IsNonStatement,'False') = 'False' AND APDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL( APJ.IsVoid,'False')='False' ";
			if (fromVendor != "")
			{
				str = str + " AND  APJ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND  APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND  APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (text3 != "")
			{
				str += " AND APJ.SysDocID <> 'SYS_VAL' ";
			}
			if (fromJob != "")
			{
				str = str + " AND APJ.JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			str += " ORDER BY Date";
			FillDataSet(dataSet2, "APJournal", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Balance Detail", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["APJournal"].Columns["Vendor Code"], createConstraints: false);
			str = "SELECT SysDocID,VoucherID,CASE WHEN Status<>8 THEN ChequeNumber ELSE '(R)' + ChequeNumber END AS ChequeNumber,ChqRec.IssueDate,\r\n                        ChqRec.BankID,Bank.BankName,PayeeID,ChequeDate,ISNULL(AmountFC,Amount) AS Amount\r\n                        FROM Cheque_Issued ChqRec\r\n                        LEFT OUTER JOIN Bank ON Bank.BankID=ChqRec.BankID\r\n                        WHERE Status IN (1,4,5) AND ISNULL(IsVoid,'False')='False'\r\n                        AND PayeeType='V' ";
			if (fromVendor != "")
			{
				str = str + " AND PayeeID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND PayeeID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND PayeeID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			FillDataSet(dataSet, "Cheque_Issued", str);
			dataSet.Relations.Add("PDCRel", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["Cheque_Issued"].Columns["PayeeID"], createConstraints: false);
			str = "SELECT CON.SysDocID,CON.VoucherID AS Consign#,CON.VendorID,TransactionDate AS [Consign Date] ,SUM((COD.Quantity  - ISNULL(QuantitySettled,0) - ISNULL(QuantityReturned,0)) * P.AverageCost) AS Value                    \r\n                        FROM Consign_In Con\r\n                        INNER JOIN Consign_IN_Detail COD ON CON.SysDocID = COD.SysDocID AND CON.VoucherID = COD.VoucherID\r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID = COD.ProductID\r\n\t\t\t\t\t\tWHERE ISNULL(IsClosed,'False') = 'False' AND  ISNULL(IsVoid,'False') = 'False' ";
			if (fromVendor != "")
			{
				str = str + " AND CON.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND CON.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND CON.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY CON.SysDocID,CON.VoucherID,TransactionDate,CON.VendorID ";
			FillDataSet(dataSet, "Consignment", str);
			dataSet.Relations.Add("ConsignRel", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["Consignment"].Columns["VendorID"], createConstraints: false);
			str = " SELECT PR.SysDocID,PR.VoucherID,VendorID,TransactionDate, SUM(PRD.Quantity) AS Quantity,SUM(PRD.Quantity * P.AverageCost) AS  Amount\r\n                             FROM Purchase_Receipt PR INNER JOIN Purchase_Receipt_Detail PRD ON PR.SysDocID = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID \r\n                             INNER JOIN Product P ON P.ProductID = PRD.ProductID\r\n                             WHERE ISNULL(IsInvoiced,'False') = 'False' AND  PRD.Quantity-ISNULL(QuantityReturned,0) <>0 ";
			if (fromVendor != "")
			{
				str = str + " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY PR.SysDocID,PR.VoucherID,VendorID,TransactionDate,PR.Reference2 ";
			FillDataSet(dataSet, "GRN", str);
			dataSet.Relations.Add("GRNRel", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["GRN"].Columns["VendorID"], createConstraints: false);
			new DataSet();
			str = "SELECT PPI.SYSDOCID,PPI.VOUCHERID,V.VendorID,V.VendorName,PPI.SOURCEVOUCHERID as PO_NUMBER ,PPI.POAMOUNT AS PO_AMOUNT,ISNULL(PPI.AMOUNTFC, PPI.Amount) AS InvoiceAmount,\r\n                                 ( ISNULL(PPI.AMOUNTFC, PPI.Amount) - ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) + SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END   \r\n\t\t\t\t\t\t\t\tFROM AP_Payment_Allocation PA WHERE Pa.InvoiceSysDocID= PPI.VoucherID AND PA.InvoiceSysDocID = PPI.SysDocID AND PA.VendorID= PPI.VendorID   GROUP BY CurrencyID),0)) AS Paid,\r\n\r\n                                 ( ISNULL(PPI.AMOUNTFC, PPI.Amount) - ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) + SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END   \r\n\t\t\t\t\t\t\t\tFROM AP_Payment_Allocation PA WHERE Pa.PaymentVoucherID= PPI.VoucherID AND PA.VendorID= PPI.VendorID   GROUP BY CurrencyID),0)) AS Unallocated\r\n                                FROM PURCHASE_PREPAYMENT_INVOICE  PPI  INNER JOIN Vendor V ON V.VendorID = PPI.VendorID WHERE ISNULL(Status,0)=0";
			if (fromVendor != "")
			{
				str = str + " AND V.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND V.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND V.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			FillDataSet(dataSet, "PrePayments", str);
			dataSet.Relations.Add("PrepaymentRel", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["PrePayments"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetVendorBalanceDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string vendorIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "";
			string text4 = "0";
			string text5 = "0";
			if (currencyID != "" && currencyID != new Currencies(base.DBConfig).GetBaseCurrencyID())
			{
				text3 = "FC";
				text4 = " (ISNULL(ConDebitFC,0) - CASE WHEN ConDebitFC IS NOT NULL THEN ISNULL(DebitFC,0) ELSE 0 END) ";
				text5 = " (ISNULL(ConCreditFC,0) - CASE WHEN ConCreditFC IS NOT NULL THEN ISNULL(CreditFC,0) ELSE 0 END)";
			}
			string str = "SELECT DISTINCT APJ.VendorID [Vendor Code] ,VendorName AS [Vendor Name] ,AllowConsignment,AddressPrintFormat,Vendor.CreditAmount,Vendor.PDCAmount,CA.Address1,CA.Address2,\r\n                        ISNULL((SELECT SUM(ISNULL(Credit" + text3 + ",0) + " + text5 + "- ISNULL(Debit" + text3 + ",0) - " + text4 + ") \r\n\r\n                            FROM APJournal APJ2 \r\n                         WHERE ISNULL(IsNonStatement,'False') = 'False' AND APJ.VendorID=APJ2.VendorID AND APJ2.APDate<'" + text + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Opening Balance],\r\n                        ISNULL((SELECT SUM(ISNULL(Credit" + text3 + ",0) + " + text5 + " - (ISNULL(Debit" + text3 + ",0) + " + text4 + "))\r\n\r\n                        FROM APJournal APJ2 \r\n                         WHERE APJ.VendorID=APJ2.VendorID AND APJ2.APDate<='" + text2 + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Ending Balance],\r\n                        (SELECT ISNULL(SUM(ISNULL(AmountFC,Amount)),0) AS PDC FROM Cheque_Issued ChqRec WHERE Status=2 AND ISNULL(IsVoid,'False')='False'  AND ChqRec.PayeeType = 'V' AND ChqRec.PayeeID = APJ.VendorID) AS PDC,\r\n                        (SELECT ISNULL(SUM(ISNULL(Amount, 0) + 0), 0) AS PDC FROM Cheque_Issued ChqIss WHERE Status IN (2,4) AND ISNULL(IsVoid,'False')='False'\r\n                        AND ( ChqIss.ClearanceDate IS NULL OR Status IN (4) AND ClearanceDate > '" + text2 + "') AND IssueDate <= '" + text2 + "'  AND ChqIss.PayeeType = 'V' AND ChqIss.PayeeID = Vendor.VendorID) AS PDCD  \r\n                        FROM APJournal APJ INNER JOIN Vendor ON APJ.VendorID=Vendor.VendorID \r\n                        LEFT OUTER JOIN Vendor_Address CA\r\n                            ON Vendor.VendorID=CA.VendorID\r\n                        WHERE ";
			str = str + "ISNULL(IsNonStatement,'False') = 'False' AND  APDate < '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (vendorIDs != "")
			{
				str = str + " AND APJ.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND APJ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "' ";
			}
			if (text3 != "")
			{
				str = str + " AND APJ.CurrencyID = '" + currencyID + "' ";
			}
			if (fromJob != "")
			{
				str = str + " AND APJ.JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			str += " ORDER BY APJ.VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", str);
			DataSet dataSet2 = new DataSet();
			str = "SELECT APJ.VendorID [Vendor Code],SysDocType,'' AS [Doc Type], APJ.SysDocID AS [Doc ID],APJ.VoucherID [Doc No],APDate AS [Date],J.JobID,J.JobName,\r\n                    PI.Note,(SELECT TOP 1 J.JobID+ ' - '+ J.JobName FROM Job J LEFT JOIN Purchase_Invoice_Detail PRD ON J.JobID=PRD.JobID WHERE PRD.SysDocID = \r\n                    PI.SysDocID AND PRD.VoucherID =PI.VoucherID) AS Project,\r\n                    (SELECT SUBSTRING((SELECT  ',' + S.SourceVoucherID + ''  FROM  Purchase_Receipt S  INNER JOIN Purchase_Invoice  D ON s.InvoiceVoucherID = D.VoucherID AND S.InvoiceSysDocID = D.SysDocID \r\n                     WHERE D.SysDocID = PI.SysDocID AND D.VoucherID = PI.VoucherID FOR XML PATH('')),2,20000)) AS  [LPO],\r\n                (SELECT SUBSTRING((SELECT  ',' + JD.CheckNumber + ''  FROM  Journal_Details JD WHERE JD.SysDocID = APJ.SysDocID AND JD.VoucherID = APJ.VoucherID AND  JD.IsARAP IS NULL FOR XML PATH('')),2,20000)) AS [CheckNumber],\r\n                (SELECT SUBSTRING((SELECT  ', ' + convert(varchar,  ( JD.CheckDate) , 106) + ' '   FROM  Journal_Details JD WHERE JD.SysDocID = APJ.SysDocID AND JD.VoucherID = APJ.VoucherID AND  JD.IsARAP IS NULL FOR XML PATH('')),2,20000)) AS [CheckDate],\r\n                    ChequeNumber AS [Chq#],ChequeDate AS [Chq Date],CASE WHEN Description = '' THEN PI.Note ELSE Description END AS Description,APJ.Reference,APJ.CurrencyID,APJ.CurrencyRate, DebitFC, CreditFC,\r\n                    ISNULL(Debit" + text3 + ",0) + " + text4 + " AS Debit, ISNULL(Credit" + text3 + ",0) + " + text5 + " AS Credit,(SELECT SUBSTRING((SELECT  DISTINCT   ',' + TD.Description + ''  FROM  Transaction_Details TD \r\nWHERE TD.SysDocID = APJ.SysDocID AND TD.VoucherID = APJ.VoucherID FOR XML PATH('')),2,20000)) AS [SubDescription],(SELECT TOP 1 Status FROM Cheque_Issued CR WHERE CR.SysDocID=APJ.SysDocID AND\r\nCR.VoucherID=APJ.VoucherID AND CR.PayeeID=APJ.VendorID AND CR.Status=2 AND ISNULL(IsVoid,'False')='False') AS [ChkStatus]\r\n\r\n                    FROM APJournal APJ LEFT OUTER JOIN System_Document SD ON APJ.SysDocID=SD.SysDocID \r\n                    LEFT OUTER JOIN Purchase_Invoice PI ON PI.SysDocID=APJ.SysDocID AND PI.VoucherID=APJ.VoucherID\r\n                    LEFT JOIN Job J on APJ.JobID=J.JobID \r\n                    WHERE ISNULL(IsNonStatement,'False') = 'False'   AND  ISNULL(ISPDCRow,'False') = 'False' AND APDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL( APJ.IsVoid,'False')='False' ";
			if (vendorIDs != "")
			{
				str = str + " AND APJ.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND  APJ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND  APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND  APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (text3 != "")
			{
				str += " AND APJ.SysDocID <> 'SYS_VAL' ";
			}
			if (fromJob != "")
			{
				str = str + " AND APJ.JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			str += " ORDER BY Date";
			FillDataSet(dataSet2, "APJournal", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Balance Detail", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["APJournal"].Columns["Vendor Code"], createConstraints: false);
			str = "SELECT SysDocID,VoucherID,CASE WHEN Status<>8 THEN ChequeNumber ELSE '(R)' + ChequeNumber END AS ChequeNumber,ChqRec.IssueDate,\r\n                        ChqRec.BankID,Bank.BankName,PayeeID,ChequeDate,ISNULL(AmountFC,Amount) AS Amount,ChqRec.note\r\n                        FROM Cheque_Issued ChqRec\r\n                        LEFT OUTER JOIN Bank ON Bank.BankID=ChqRec.BankID where\r\n                       Status IN (2,4) AND ISNULL(IsVoid,'False')='False' AND ( ChqRec.ClearanceDate IS NULL OR Status IN (4) AND ClearanceDate > '" + text2 + "') AND IssueDate <='" + text2 + "'\r\n                        AND PayeeType='V' ";
			if (vendorIDs != "")
			{
				str = str + " AND PayeeID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND PayeeID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND PayeeID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND PayeeID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			FillDataSet(dataSet, "Cheque_Issued", str);
			dataSet.Relations.Add("PDCRel", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["Cheque_Issued"].Columns["PayeeID"], createConstraints: false);
			str = "SELECT CON.SysDocID,CON.VoucherID AS Consign#,CON.VendorID,TransactionDate AS [Consign Date] ,SUM((COD.Quantity  - ISNULL(QuantitySettled,0) - ISNULL(QuantityReturned,0)) * P.AverageCost) AS Value                    \r\n                        FROM Consign_In Con\r\n                        INNER JOIN Consign_IN_Detail COD ON CON.SysDocID = COD.SysDocID AND CON.VoucherID = COD.VoucherID\r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID = COD.ProductID\r\n\t\t\t\t\t\tWHERE ISNULL(IsClosed,'False') = 'False' AND  ISNULL(IsVoid,'False') = 'False' ";
			if (vendorIDs != "")
			{
				str = str + " AND CON.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND CON.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND CON.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND CON.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY CON.SysDocID,CON.VoucherID,TransactionDate,CON.VendorID ";
			FillDataSet(dataSet, "Consignment", str);
			dataSet.Relations.Add("ConsignRel", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["Consignment"].Columns["VendorID"], createConstraints: false);
			str = " SELECT PR.SysDocID,PR.VoucherID,VendorID,TransactionDate, SUM(PRD.Quantity) AS Quantity,SUM(PRD.Quantity * P.AverageCost) AS  Amount\r\n                             FROM Purchase_Receipt PR INNER JOIN Purchase_Receipt_Detail PRD ON PR.SysDocID = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID \r\n                             INNER JOIN Product P ON P.ProductID = PRD.ProductID\r\n                             WHERE ISNULL(IsInvoiced,'False') = 'False' ";
			if (vendorIDs != "")
			{
				str = str + " AND VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY PR.SysDocID,PR.VoucherID,VendorID,TransactionDate,PR.Reference2 ";
			FillDataSet(dataSet, "GRN", str);
			dataSet.Relations.Add("GRNRel", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["GRN"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchaseByVendorDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "Select DISTINCT SI.VendorID,VendorName\r\n                            FROM Purchase_Invoice SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND SI.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " UNION ";
			text3 += "Select DISTINCT SI.VendorID,VendorName\r\n                            FROM Purchase_Return SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND SI.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " ORDER BY VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select SysDocID,VoucherID,VendorID,TransactionDate,Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Purchase' ELSE 'Cash Purchase' END AS [Type],\r\n                    BuyerID,CurrencyID,CurrencyRate,Discount,DiscountFC,\r\n                    Total,TotalFC \r\n\r\n                    FROM Purchase_Invoice SI WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " UNION ";
			text3 = text3 + "Select SysDocID,VoucherID,VendorID,TransactionDate,Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type],\r\n                    BuyerID,CurrencyID,CurrencyRate,-1*Discount,-1*DiscountFC,\r\n                    -1*Total,-1*TotalFC \r\n\r\n                    FROM Purchase_Return SI WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " ORDER BY TransactionDate";
			FillDataSet(dataSet2, "Purchase", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Purchase Detail", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["Purchase"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchaseByVendorSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "Select SI.VendorID, CU.VendorName,\r\n                            SUM(Discount) AS Discount,\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN Total ELSE 0 END) AS [CashPurchase],\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN Total ELSE 0 END) AS [CreditPurchase] \r\n                            FROM Purchase_Invoice SI INNER JOIN Vendor CU ON SI.VendorID=CU.VendorID\r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (vendorIDs != "")
			{
				str = str + " AND SI.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY SI.VendorID,CU.VendorName";
			FillDataSet(dataSet, "Purchase", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["VendorID"]
			};
			str = "Select SI.VendorID, CU.VendorName,\r\n                    -1*SUM(Discount) AS DiscountReturn,\r\n                    SUM(Total) AS [PurchaseReturn]\r\n                    FROM Purchase_Return SI INNER JOIN Vendor CU ON SI.VendorID=CU.VendorID \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (vendorIDs != "")
			{
				str = str + " AND SI.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY SI.VendorID,CU.VendorName";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Purchase", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["VendorID"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetPurchaseCostEntryByVendorDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "Select DISTINCT SID.ExpenseID, EX.ExpenseName\r\n\t\t\t\t\t\t\tFROM Purchase_Cost_Entry_Detail SID INNER JOIN Purchase_Cost_Entry SI ON SID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID\r\n\t\t\t\t\t\t\tINNER JOIN Expense_Code EX ON SID.ExpenseID=EX.ExpenseID\r\n\r\n\t\t\t\t\t\t\tLEFT JOIN Vendor V ON V.VendorID= SID.SupplierID";
				text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(SI.IsVoid,'False')='False'  ";
				if (vendorIDs != "")
				{
					text3 = text3 + " AND SID.SupplierID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					text3 = text3 + " AND SID.SupplierID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.SupplierID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND SID.SupplierID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				text3 += " GROUP BY SID.SupplierID,V.VendorName, SID.ExpenseID, EX.ExpenseName";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Expense_Code", text3);
				DataSet dataSet2 = new DataSet();
				text3 = "Select SID.*,SID.SupplierID + '-' + VendorName AS 'Vendor',SID.Description, SI.Total, SI.TotalFC, SI.TaxAmount, SI.TaxAmountFC, SI.Discount, SI.DiscountFC,\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t ISNULL(SI.DiscountFC,SI.Discount) AS Discount,ISNULL(SI.TotalFC,SI.Total) - ISNULL(ISNULL(SI.DiscountFC,SI.Discount),0) AS GrandTotal,\r\n                                 ISNULL(ISNULL(SI.TaxAmountFC,SI.TaxAmount) ,0) AS Tax,ISNULL(SI.TotalFC,SI.Total) AS Total\r\n\t\t\t\t\t\tFROM Purchase_Cost_Entry_Detail SID INNER JOIN Purchase_Cost_Entry SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Vendor PR ON SID.SupplierID=PR.VendorID INNER JOIN \r\n\t\t\t\t\t\tExpense_Code EX ON EX.ExpenseID=SID.ExpenseID\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (vendorIDs != "")
				{
					text3 = text3 + " AND SID.SupplierID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					text3 = text3 + " AND SID.SupplierID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.SupplierID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND SID.SupplierID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				text3 += " GROUP BY SID.SupplierID,PR.VendorName, SID.SysDocID, SID.VoucherID, SID.BOLNumber, SID.CurrencyID, SID.ExpenseID, SID.Description, \r\n                          SID.RowIndex, SID.Amount, SID.Cost, SID.SourceVoucherID, SID.SourceSysDocID, SID.AmountFC, SID.CurrencyRate,SID.RateType, SID.AllocatedCost, \r\n                            SI.Total, SI.TotalFC, SI.TaxAmount, SI.TaxAmountFC, SI.Discount, SI.DiscountFC, SID.Quantity, SID.DueDate, SID.Remarks, SID.TaxOption, SID.TaxGroupID, SID.TaxAmount";
				FillDataSet(dataSet2, "PurchaseCostEntryDetail", text3);
				dataSet.Merge(dataSet2.Tables[0]);
				dataSet.Relations.Add("PurchaseCostEntryDetail", dataSet.Tables["Expense_Code"].Columns["ExpenseID"], dataSet.Tables["PurchaseCostEntryDetail"].Columns["ExpenseID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseCostEntryByVendorSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "Select SI.*, CU.VendorName, ShippingMethodName \r\n                            \r\n                            \r\n                            FROM Purchase_Cost_Entry SI \r\n\t\t\t\t\t\t\tINNER JOIN Vendor CU ON SI.VendorID=CU.VendorID\r\n\t\t\t\t\t\t\tLEFT JOIN Shipping_Method SM On Si.ShippingMethodID=Sm.ShippingMethodID\r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (vendorIDs != "")
			{
				str = str + " AND SI.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += "GROUP BY SI.VendorID,CU.VendorName, SI.SysDocID, Si.VoucherID, Si.BOLNumber, SI.ContainerNumber, SI.TransactionDate, Si.PurchaseFlow, SI.Port, SI.LoadingPort, \r\n                    SI.ETA, SI.ATD, SI.Status, SI.ShippingMethodID, SI.IsVoid, SI.Reference, SI.SourceVoucherID, SI.SourceSysDocID, SI.PONumber,\r\n                    SI. Shipper, SI.ClearingAgent, SI.Weight, SI.IsReceived, SI.Note, SI.Value, SI.ShipStatus, SI.TransporterID, SI. ContainerSizeID, \r\n                    SI.ApprovalStatus, SI.CurrencyID, SI.BuyerID, SI.Discount, SI.DiscountFC, SI.TaxAmount, SI.TaxAmountFC, SI.Total, SI.TotalFC,\r\n                    SI. DateCreated, SI.DateUpdated, SI.CreatedBy, SI.DateCreated, SI.UpdatedBy, SM.ShippingMethodName,SI.TaxGroupID";
			FillDataSet(dataSet, "Purchase", str);
			return dataSet;
		}

		public DataSet GetPurchaseByVendorGroupDetailReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "Select DISTINCT ISNULL(CG.GroupID,'No Group') AS GroupID,CG.GroupName\r\n                            FROM Purchase_Invoice SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                            LEFT OUTER JOIN Vendor_Group CG ON CG.GroupID=Vendor.VendorGroupID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				text3 = text3 + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " UNION ";
			text3 += "Select DISTINCT ISNULL(CG.GroupID,'No Group') AS GroupID,CG.GroupName\r\n                            FROM Purchase_Return SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                            LEFT OUTER JOIN Vendor_Group CG ON CG.GroupID=Vendor.VendorGroupID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				text3 = text3 + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " ORDER BY GroupID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select ISNULL(CG.GroupID,'No Group') AS GroupID,SysDocID,VoucherID,SI.VendorID + '-' + Vendor.VendorName AS Vendor,TransactionDate,SI.Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Purchase' ELSE 'Cash Purchase' END AS [Type],\r\n                    SI.BuyerID, SI.CurrencyID,CurrencyRate,Discount,DiscountFC,\r\n                    Total,TotalFC \r\n\r\n                    FROM Purchase_Invoice SI INNER JOIN Vendor ON Vendor.VendorID=SI.VendorID\r\n                    LEFT OUTER JOIN Vendor_Group CG ON CG.GroupID=Vendor.VendorGroupID \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				text3 = text3 + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " UNION ";
			text3 = text3 + "Select ISNULL(CG.GroupID,'No Group') AS GroupID,SysDocID,VoucherID,SI.VendorID + '-' + Vendor.VendorName AS Vendor,TransactionDate,SI.Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type],\r\n                    SI.BuyerID, SI.CurrencyID,CurrencyRate,-1*Discount,-1*DiscountFC,\r\n                    -1*Total,-1*TotalFC \r\n\r\n                    FROM Purchase_Return SI INNER JOIN Vendor ON Vendor.VendorID=SI.VendorID\r\n                    LEFT OUTER JOIN Vendor_Group CG ON CG.GroupID=Vendor.VendorGroupID  \r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				text3 = text3 + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " ORDER BY TransactionDate";
			FillDataSet(dataSet2, "Purchase", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Purchase Detail", dataSet.Tables["Vendor"].Columns["GroupID"], dataSet.Tables["Purchase"].Columns["GroupID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchaseByVendorGroupSummaryReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "Select ISNULL(CG.GroupID,'No Group') AS GroupID, CG.GroupName,\r\n                            SUM(Discount) AS Discount,\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN Total ELSE 0 END) AS [CashPurchase],\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN Total ELSE 0 END) AS [CreditPurchase] \r\n                            FROM Purchase_Invoice SI INNER JOIN Vendor CU ON SI.VendorID=CU.VendorID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Vendor_Group CG ON CU.VendorGroupID=CG.GroupID\r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				str = str + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY CG.GroupID, CG.GroupName";
			FillDataSet(dataSet, "Purchase", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["VendorID"]
			};
			str = "Select ISNULL(CG.GroupID,'No Group') AS GroupID, CG.GroupName,\r\n                            -1*SUM(Discount) AS DiscountReturn,\r\n                            SUM(Total) AS [PurchaseReturn]\r\n                            FROM Purchase_Return SI INNER JOIN Vendor CU ON SI.VendorID=CU.VendorID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Vendor_Group CG ON CU.VendorGroupID=CG.GroupID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				str = str + " AND SI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY CG.GroupID, CG.GroupName";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Purchase", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["GroupID"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetVendorListReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string vendorIDs)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  VendorID,VendorName,VendorClassID, AreaID,\r\n                                ContactName,CountryID,VendorGroupID \r\n                                FROM Vendor \r\n                                WHERE 1=1 ";
			if (vendorIDs != "")
			{
				text = text + " AND VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text = text + " AND VendorID>='" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				text = text + " AND VendorID<='" + toVendor + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND VendorClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND VendorClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text = text + " AND VendorGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text = text + " AND VendorGroupID<='" + toGroup + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Vendors", text);
			return dataSet;
		}

		public DataSet GetVendorPrimaryContactListReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string vendorIDs)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  Vendor.VendorID,VendorName,\r\n                            CA.ContactName,AddressPrintFormat,City,Country,Phone1,Mobile,Fax,Email\r\n                            FROM Vendor LEFT OUTER JOIN Vendor_Address CA\r\n                            ON Vendor.VendorID=CA.VendorID\r\n                                WHERE CA.AddressID=Vendor.PrimaryAddressID ";
			if (vendorIDs != "")
			{
				text = text + " AND Vendor.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text = text + " AND Vendor.VendorID>='" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				text = text + " AND Vendor.VendorID<='" + toVendor + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND VendorClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND VendorClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text = text + " AND VendorGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text = text + " AND VendorGroupID<='" + toGroup + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Vendors", text);
			return dataSet;
		}

		public DataSet GetVendorProfileReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  Vendor.VendorID, VendorName,ForeignName,LegalName,ClassName,Vendor.ContactName,Vendor.TermID,\r\n                            AreaName,Vendor.AcceptCheckPayment,CA.Phone1,CA.Mobile,CA.Fax,CA.Email,CA.Website,\r\n                            Vendor.AcceptPDC,Vendor.CreditLimitType,Vendor.CreditAmount,GroupName,\r\n                            Vendor.CountryID,Vendor.ShippingMethodID,Vendor.IsInactive,IsHold,Vendor.BankName,Vendor.BankBranch,Vendor.BankAccountNumber,\r\n                            Vendor.PaymentTermID,\r\n                            Vendor.Note,Vendor.PaymentMethodID,Vendor.BuyerID + '-' + Buyer.FullName AS Buyer\r\n                            FROM Vendor LEFT OUTER JOIN Vendor_Class CusClass ON Vendor.VendorClassID=CusClass.ClassID\r\n                            LEFT OUTER JOIN Vendor_Group Cus_Group ON Vendor.VendorGroupID=Cus_Group.GroupID\r\n                            LEFT OUTER JOIN Area Area ON Vendor.AreaID=Area.AreaID\r\n                            LEFT OUTER JOIN Buyer Buyer ON Vendor.BuyerID=Buyer.BuyerID\r\n                            LEFT OUTER JOIN Vendor_Address CA ON Vendor.VendorID=CA.VendorID AND Vendor.PrimaryAddressID=CA.AddressID \r\n                            WHERE 1=1 ";
			if (fromVendor != "")
			{
				text = text + " AND Vendor.VendorID>='" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				text = text + " AND Vendor.VendorID<='" + toVendor + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND Vendor.VendorClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND Vendor.VendorClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text = text + " AND Vendor.VendorGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text = text + " AND Vendor.VendorGroupID<='" + toGroup + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Vendor.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Vendors", text);
			return dataSet;
		}

		public DataSet GetVendorProfileReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string vendorIDs)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  Vendor.VendorID, VendorName,ForeignName,LegalName,ClassName,Vendor.ContactName,Vendor.TermID,\r\n                            AreaName,Vendor.AcceptCheckPayment,CA.Phone1,CA.Mobile,CA.Fax,CA.Email,CA.Website,\r\n                            Vendor.AcceptPDC,Vendor.CreditLimitType,Vendor.CreditAmount,GroupName,\r\n                            Vendor.CountryID,Vendor.ShippingMethodID,Vendor.IsInactive,IsHold,Vendor.BankName,Vendor.BankBranch,Vendor.BankAccountNumber,\r\n                            Vendor.PaymentTermID,\r\n                            Vendor.Note,Vendor.PaymentMethodID,Vendor.BuyerID + '-' + Buyer.FullName AS Buyer\r\n                            FROM Vendor LEFT OUTER JOIN Vendor_Class CusClass ON Vendor.VendorClassID=CusClass.ClassID\r\n                            LEFT OUTER JOIN Vendor_Group Cus_Group ON Vendor.VendorGroupID=Cus_Group.GroupID\r\n                            LEFT OUTER JOIN Area Area ON Vendor.AreaID=Area.AreaID\r\n                            LEFT OUTER JOIN Buyer Buyer ON Vendor.BuyerID=Buyer.BuyerID\r\n                            LEFT OUTER JOIN Vendor_Address CA ON Vendor.VendorID=CA.VendorID AND Vendor.PrimaryAddressID=CA.AddressID \r\n                            WHERE 1=1 ";
			if (vendorIDs != "")
			{
				text = text + " AND Vendor.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text = text + " AND Vendor.VendorID>='" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				text = text + " AND Vendor.VendorID<='" + toVendor + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND Vendor.VendorClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND Vendor.VendorClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text = text + " AND Vendor.VendorGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text = text + " AND Vendor.VendorGroupID<='" + toGroup + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Vendor.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Vendors", text);
			return dataSet;
		}

		public DataSet GetVendorActivityReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT VendorID,VendorName FROM Vendor WHERE ISNULL(IsInactive,'False')='False' ";
			if (vendorIDs != "")
			{
				str = str + " AND VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str = str + " AND VendorID >= '" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				str = str + " AND VendorID <= '" + toVendor + "'";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID <= '" + toGroup + "') ";
			}
			str += " ORDER BY VendorID ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", str);
			DataSet dataSet2 = new DataSet();
			string text3 = "";
			if (vendorIDs != "")
			{
				str = str + " AND VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID <= '" + toGroup + "') ";
			}
			str = "SELECT Purchase_Invoice.SysDocID,VoucherID,VendorID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Purchase_Invoice INNER JOIN System_Document SD ON Purchase_Invoice.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' " + text3 + "\r\n\r\n                        UNION \r\n\r\n                        SELECT Purchase_Return.SysDocID,VoucherID,VendorID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Purchase_Return INNER JOIN System_Document SD ON Purchase_Return.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n                        UNION \r\n\r\n                        SELECT Purchase_Quote.SysDocID,VoucherID,VendorID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Purchase_Quote INNER JOIN System_Document SD ON Purchase_Quote.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n                        UNION \r\n\r\n                        SELECT Purchase_Order.SysDocID,VoucherID,VendorID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Purchase_Order INNER JOIN System_Document SD ON Purchase_Order.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n                        UNION \r\n\r\n                        SELECT Purchase_Order.SysDocID,VoucherID,VendorID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Purchase_Order INNER JOIN System_Document SD ON Purchase_Order.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n\r\n                        UNION \r\n\r\n                        SELECT Purchase_Receipt.SysDocID,VoucherID,VendorID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        NULL AS Amount \r\n                        FROM Purchase_Receipt INNER JOIN System_Document SD ON Purchase_Receipt.SysDocID=SD.SysDocID\r\n                        WHERE  ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n                        UNION \r\n\r\n\r\n                        SELECT GL_Transaction.SysDocID,VoucherID,PayeeID AS VendorID,TransactionDate,\r\n                        SysDocType,Description AS [Note],\r\n                        NULL AS Amount \r\n                        FROM GL_Transaction INNER JOIN System_Document SD ON GL_Transaction.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND PayeeType='C' AND\r\n                        TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n\r\n                        ORDER BY TransactionDate";
			FillDataSet(dataSet2, "VendorActivity", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Vendor_Activity", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["VendorActivity"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetVendorDocumentAddress(string vendorID, string addressField)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AddressPrintFormat\r\n                           FROM Vendor_Address CA  \r\n                           WHERE CA.AddressID='PRIMARY'";
			FillDataSet(dataSet, "Vendor", textCommand);
			return dataSet;
		}

		public DataSet GetVendorStatement(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, bool showZeroBalance, string vendorIDs)
		{
			try
			{
				to = new DateTime(to.Year, to.Month, to.Day, 23, 59, 59);
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string str = "SELECT DISTINCT APJ.VendorID [Vendor Code] ,VendorName AS [Vendor Name] , VA.AddressPrintFormat,";
				str = (isFC ? (str + " ISNULL((SELECT SUM(ISNULL(CreditFC,0) + ISNULL(ConCreditFC,0) - CASE WHEN ConCreditFC IS NOT NULL THEN ISNULL(CreditFC,0) ELSE 0 END - (ISNULL(DebitFC,0) + ISNULL(ConDebitFC,0) - CASE WHEN ConDebitFC IS NOT NULL THEN ISNULL(DebitFC,0) ELSE 0 END)) ") : (str + " ISNULL((SELECT SUM(ISNULL(Credit,0)- ISNULL(Debit,0)) "));
				str = str + "FROM APJournal APJ2 \r\n                         WHERE APJ.VendorID=APJ2.VendorID AND APJ2.APDate<'" + text + "' AND ISNULL(IsVoid,'False')='False'  AND ISNULL(IsNonStatement,'False')='False'),0)\r\n                        AS [Opening Balance], ";
				str = (isFC ? (str + " ISNULL((SELECT SUM(ISNULL(CreditFC,0) + ISNULL(ConCreditFC,0) - CASE WHEN ConCreditFC IS NOT NULL THEN ISNULL(CreditFC,0) ELSE 0 END  - (ISNULL(DebitFC,0) + ISNULL(ConDebitFC,0) - CASE WHEN ConDebitFC IS NOT NULL THEN ISNULL(DebitFC,0) ELSE 0 END)) ") : (str + " ISNULL((SELECT SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) "));
				str = str + "FROM APJournal APJ2 \r\n                         WHERE APJ.VendorID=APJ2.VendorID AND APJ2.APDate<='" + text2 + "' AND ISNULL(IsVoid,'False')='False' AND ISNULL(IsNonStatement,'False')='False'),0)\r\n                        AS [Ending Balance]\r\n                        FROM APJournal APJ INNER JOIN Vendor ON APJ.VendorID=Vendor.VendorID \r\n                        LEFT OUTER JOIN Vendor_Address VA ON Vendor.VendorID=VA.VendorID\r\n                        AND VA.AddressID='Primary' WHERE ";
				str = str + " APDate < '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False' ";
				if (vendorIDs != "")
				{
					str = str + " AND APJ.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					str = str + " AND APJ.VendorID >= '" + fromVendor + "' ";
				}
				if (toVendor != "")
				{
					str = str + " AND APJ.VendorID <= '" + toVendor + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND VendorClassID >= '" + fromClass + "' ";
				}
				if (toClass != "")
				{
					str = str + " AND VendorClassID <= '" + toClass + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND VendorGroupID >= '" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					str = str + " AND VendorGroupID <= '" + toGroup + "' ";
				}
				str += " ORDER BY APJ.VendorID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Vendor", str);
				DataSet dataSet2 = new DataSet();
				str = "SELECT APJ.VendorID [Vendor Code],SysDocType,'' AS [Doc Type], APJ.SysDocID AS [Doc ID],APJ.VoucherID [Doc No],APDate AS [Date],\r\n                            ChequeNumber AS [Chq#],ChequeDate AS [Chq Date],Description,APJ.Reference,PUI.Reference2,APJ.CurrencyID,APJ.CurrencyRate,DebitFC,CreditFC, ";
				str = ((!isFC) ? (str + " Debit, Credit ") : (str + " ISNULL(DebitFC,0) + ISNULL(ConDebitFC,0)- CASE WHEN ConDebitFC IS NOT NULL THEN ISNULL(DebitFC,0) ELSE 0 END AS Debit, ISNULL(CreditFC,0) +  ISNULL(ConCreditFC,0) - CASE WHEN ConCreditFC IS NOT NULL THEN ISNULL(CreditFC,0) ELSE 0 END AS Credit "));
				str = str + "  FROM APJournal APJ LEFT OUTER JOIN System_Document SD ON APJ.SysDocID=SD.SysDocID\r\n                    LEFT OUTER JOIN Purchase_Invoice PUI ON PUI.SysDocID = APJ.SysDocID AND PUI.VoucherID = APJ.VoucherID WHERE   APDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(APJ.IsVoid,'False')='False' AND ISNULL(IsNonStatement,'False')='False'";
				if (vendorIDs != "")
				{
					str = str + " AND APJ.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					str = str + " AND APJ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					str = str + " AND APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (isFC)
				{
					str += " AND (DebitFC IS NOT NULL OR CreditFC IS NOT NULL OR ConDebitFC IS NOT NULL OR ConCreditFC IS NOT NULL)";
				}
				str += " ORDER BY Date";
				FillDataSet(dataSet2, "APJournal", str);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Balance Detail", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["APJournal"].Columns["Vendor Code"], createConstraints: false);
				str = "SELECT SysDocID,VoucherID,ChequeNumber,ChqRec.ChequebookID,Bank.BankName,PayeeID,ChequeDate,ISNULL(AmountFC,Amount) AS Amount\r\n                        FROM Cheque_Issued ChqRec\r\n                        LEFT OUTER JOIN Bank ON Bank.BankID=ChqRec.ChequebookID\r\n                        WHERE \r\n                        Status=2 AND \r\n                        ISNULL(IsVoid,'False')='False'\r\n                        AND PayeeType='V'";
				FillDataSet(dataSet, "Cheque_Issued", str);
				dataSet.Relations.Add("PDC", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["Cheque_Issued"].Columns["PayeeID"], createConstraints: false);
				str = "SELECT  SysDocID,VoucherID,BankFacilityID,PayeeID,Reference,BF.CurrentAccountID,BF.PayableAccountID,CurrencyID, ISNULL(AmountFC,Amount) AS Amount, Amount AS AmountLC\r\n                                    FROM TR_Application BFT INNER JOIN Bank_Facility BF  ON BF.FacilityID=BFT.BankFacilityID WHERE VoucherID NOT IN ( SELECT  SourceVoucherID from Bank_Facility_Transaction\r\n                                    TR INNER JOIN TR_Application TRA ON TR.SourceSysDocID=TRA.SysDocID AND  TR.SourceVoucherID=TRA.VoucherID  )  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				FillDataSet(dataSet, "TR_Application", str);
				if (dataSet.Tables["Vendor"].Rows.Count > 0 && dataSet.Tables["TR_Application"].Rows.Count > 0)
				{
					dataSet.Relations.Add("TRApplication", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["TR_Application"].Columns["PayeeID"], createConstraints: false);
				}
				str = "SELECT SysDocID,VoucherID,VendorID,SourceSysDocID,SOurceVoucherID,TransactionDate,ISNULL(CurrencyID,(SELECT CurrencyID \r\n                                FROM Currency WHERE ISNULL(IsBase,'False')='True')) AS CurrencyID,\r\n                                ISNULL(CurrencyRate,1) AS CurrencyRate, ISNULL(AmountFC,Amount) AS Amount, \r\n\t\t\t\t\t\t\t\t(SELECT  SUM(ISNULL(AmountFC,Amount)) FROM AP_Payment_Allocation APP  \r\n                                WHERE APP.InvoiceSysDocID = PPI.SysDocID AND APP.InvoiceVoucherID = PPI.VoucherID GROUP BY APP.InvoiceSysDocID, APP.InvoiceVoucherID ) AS Paid FROM Purchase_Prepayment_Invoice PPI \r\n                                WHERE  InvoiceVoucherID IS NULL AND ISNULL(IsVoid,'False')='False' AND TransactionDate <= '" + text2 + "' AND ISNULL(PPI.Status,0)=0 ";
				if (vendorIDs != "")
				{
					str = str + " AND VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					str = str + " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				str += " GROUP BY SysDocID, VoucherID, VendorID, SourceSysDocID, SOurceVoucherID, TransactionDate, CurrencyID, CurrencyRate, AmountFC, Amount ";
				FillDataSet(dataSet, "Purchase_PrePayment_Invoice", str);
				dataSet.Relations.Add("Prepayments", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["Purchase_PrePayment_Invoice"].Columns["VendorID"], createConstraints: false);
				DataSet vendorAgingSummary = GetVendorAgingSummary(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, to, showZeroBalance: true, isFC, vendorIDs);
				vendorAgingSummary.Tables[0].TableName = "Aging";
				dataSet.Merge(vendorAgingSummary.Tables["Aging"]);
				dataSet.Relations.Add("Aging", dataSet.Tables["Vendor"].Columns["Vendor Code"], dataSet.Tables["Aging"].Columns["VendorID"], createConstraints: false);
				dataSet.Tables.Add("StatementInfo");
				dataSet.Tables["StatementInfo"].Columns.Add("PeriodFrom", typeof(DateTime));
				dataSet.Tables["StatementInfo"].Columns.Add("PeriodTo", typeof(DateTime));
				dataSet.Tables["StatementInfo"].Rows.Add(from, to);
				foreach (DataRow row in dataSet.Tables["APJournal"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(row["Debit"].ToString(), out result);
					decimal.TryParse(row["DebitFC"].ToString(), out result3);
					decimal.TryParse(row["Credit"].ToString(), out result2);
					decimal.TryParse(row["CreditFC"].ToString(), out result4);
					if (result == 0m)
					{
						row["Debit"] = DBNull.Value;
					}
					if (result2 == 0m)
					{
						row["Credit"] = DBNull.Value;
					}
					if (result3 == 0m)
					{
						row["DebitFC"] = DBNull.Value;
					}
					if (result4 == 0m)
					{
						row["CreditFC"] = DBNull.Value;
					}
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetVendorAgingSummary(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, bool isFC, string vendorIDs)
		{
			return GetVendorAgingSummary(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, asOfDate, showZeroBalance, includeAgingTable: false, isFC, vendorIDs);
		}

		public DataSet GetVendorAgingSummary(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, bool includeAgingTable, bool isFC, string vendorIDs)
		{
			return GetVendorAgingBalanceList(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, asOfDate, showZeroBalance, isFC, includeAgingTable, vendorIDs);
		}

		public DataSet GetVendorAgingDetail(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, string vendorIDs)
		{
			try
			{
				DateTime dateTime = DateTime.Today;
				bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
				if (asOfDate < DateTime.Today)
				{
					dateTime = asOfDate;
				}
				if (asOfDate == DateTime.MaxValue)
				{
					CommonLib.ToSqlDateTimeString(DateTime.Parse("1-1-2099"));
				}
				else
				{
					CommonLib.ToSqlDateTimeString(asOfDate);
				}
				bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.AgingByDueDate, true).ToString());
				DataSet dataSet = new DataSet();
				dataSet = new CompanyOption(base.DBConfig).GetCompanyOptionList(76, 107);
				DataSet vendorAgingSummary = GetVendorAgingSummary(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, asOfDate, showZeroBalance, includeAgingTable: true, isFC: false, vendorIDs);
				vendorAgingSummary.Tables["Aging"].Columns.Add("Current", typeof(decimal));
				vendorAgingSummary.Tables["Aging"].Columns.Add("Month1", typeof(decimal));
				vendorAgingSummary.Tables["Aging"].Columns.Add("Month2", typeof(decimal));
				vendorAgingSummary.Tables["Aging"].Columns.Add("Month3", typeof(decimal));
				vendorAgingSummary.Tables["Aging"].Columns.Add("Month4", typeof(decimal));
				vendorAgingSummary.Tables["Aging"].Columns.Add("Month5", typeof(decimal));
				vendorAgingSummary.Tables["Aging"].Columns.Add("Month6", typeof(decimal));
				vendorAgingSummary.Tables["Aging"].Columns.Add("Over", typeof(decimal));
				new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
				DataHelper dataHelper = new DataHelper(base.DBConfig);
				bool companyOption = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowAging1, defaultValue: true);
				bool companyOption2 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowAging2, defaultValue: true);
				bool companyOption3 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowAging3, defaultValue: true);
				bool companyOption4 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowAging4, defaultValue: true);
				bool companyOption5 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowAging5, defaultValue: true);
				bool companyOption6 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowAging6, defaultValue: true);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingFrom0, 0);
				int companyOption7 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingFrom1, 1);
				int companyOption8 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingFrom2, 31);
				int companyOption9 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingFrom3, 61);
				int companyOption10 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingFrom4, 91);
				int companyOption11 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingFrom5, 121);
				int companyOption12 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingFrom6, 151);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingFrom7, 181);
				int companyOption13 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingTo0, 0);
				int companyOption14 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingTo1, 30);
				int companyOption15 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingTo2, 60);
				int companyOption16 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingTo3, 90);
				int companyOption17 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingTo4, 120);
				int companyOption18 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingTo5, 150);
				int companyOption19 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingTo6, 180);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingTo7, 999);
				int num = 0;
				num = (companyOption6 ? companyOption19 : (companyOption5 ? companyOption18 : (companyOption4 ? companyOption17 : (companyOption3 ? companyOption16 : (companyOption2 ? companyOption15 : ((!companyOption) ? companyOption13 : companyOption14))))));
				decimal num2 = default(decimal);
				foreach (DataRow row in vendorAgingSummary.Tables["Aging"].Rows)
				{
					DateTime d = DateTime.Parse(row["APDate"].ToString());
					if (flag && row["APDueDate"] != DBNull.Value)
					{
						d = DateTime.Parse(row["APDueDate"].ToString());
					}
					if (!flag)
					{
						d = d.AddDays(companyOption13);
					}
					if (row["AmountDue"] != DBNull.Value && decimal.Parse(row["AmountDue"].ToString()) > 0m)
					{
						decimal.Parse(row["AmountDue"].ToString());
						int num3 = (asOfDate - d).Days - companyOption13;
						if (num3 <= 0)
						{
							row["Current"] = row["AmountDue"];
						}
						else if (num3 >= companyOption7 && num3 <= companyOption14)
						{
							row["Month1"] = row["AmountDue"];
						}
						else if (num3 >= companyOption8 && num3 <= companyOption15)
						{
							row["Month2"] = row["AmountDue"];
						}
						else if (num3 >= companyOption9 && num3 <= companyOption16)
						{
							row["Month3"] = row["AmountDue"];
						}
						else if (num3 >= companyOption10 && num3 <= companyOption17)
						{
							row["Month4"] = row["AmountDue"];
						}
						else if (num3 >= companyOption11 && num3 <= companyOption18)
						{
							row["Month5"] = row["AmountDue"];
						}
						else if (num3 >= companyOption12 && num3 <= companyOption19)
						{
							row["Month6"] = row["AmountDue"];
						}
						if (num3 > num)
						{
							row["Over"] = row["AmountDue"];
						}
						if (num3 >= companyOption7)
						{
							num2 += decimal.Parse(row["AmountDue"].ToString());
						}
					}
				}
				DataRow[] array = vendorAgingSummary.Tables["Aging"].Select(" AmountDue IS NOT NULL AND ISNULL(AmountDue,0) > 0");
				if (array.Length != 0)
				{
					DataSet dataSet2 = new DataSet();
					dataSet2.Merge(array);
					vendorAgingSummary.Tables["Aging"].Rows.Clear();
					vendorAgingSummary.Tables["Aging"].Merge(dataSet2.Tables[0]);
				}
				return vendorAgingSummary;
			}
			catch
			{
				throw;
			}
		}

		internal DateTime CalculateDueDate(DateTime invoiceDate, string vendorID, SqlTransaction sqlTransaction)
		{
			DateTime result = invoiceDate;
			DataSet dataSet = new DataSet();
			if (vendorID != "")
			{
				string textCommand = "SELECT * FROM Payment_Term\r\n                                WHERE PaymentTermID IN (Select PaymentTermID FROM Vendor WHERE VendorID = '" + vendorID + "')";
				FillDataSet(dataSet, "Term", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Term"].Rows.Count == 0)
				{
					return result;
				}
				DataRow dataRow = dataSet.Tables["Term"].Rows[0];
				int result2 = 0;
				int.TryParse(dataRow["TermType"].ToString(), out result2);
				PaymentTermTypes paymentTermTypes = (PaymentTermTypes)result2;
				int num = int.Parse(dataRow["NetDays"].ToString());
				switch (paymentTermTypes)
				{
				case PaymentTermTypes.FromInvoiceDate:
					result = invoiceDate.AddDays(num);
					break;
				case PaymentTermTypes.FromEOM:
					result = new DateTime(invoiceDate.Year, invoiceDate.Month, 1).AddMonths(1).AddDays(-1.0).AddDays(num);
					break;
				default:
					result = invoiceDate.AddDays(num);
					break;
				}
			}
			return result;
		}

		public DataSet GetTopVendorBalanceList(int count)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT  TOP " + count.ToString() + " VendorName, SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) AS Amount \r\n                                        INTO Temp_V\r\n                                        FROM APJournal ARJ INNER JOIN Vendor V \r\n                                        ON ARJ.VendorID=V.VendorID\r\n                                        WHERE ISNULL(IsVoid,'False')='False'\r\n                                        GROUP By VendorName\r\n\r\n                                        SELECT VendorName Name,Amount FROM Temp_V WHERE Amount>0\r\n\r\n                                        DROP Table Temp_V";
			FillDataSet(dataSet, "Vendor", textCommand);
			return dataSet;
		}

		public DataSet GetVendorOutstandingInvoicesReport(DateTime reportDate, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool isFC, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(reportDate);
				string str = "WITH CTE AS(\tSELECT DISTINCT APJ.VendorID, VendorName, Vendor.CurrencyID,Vendor.PaymentTermID,PT.TermName,\r\n                     \r\n                            ( SELECT SUM(ISNULL(Credit,0) - ISNULL(Debit,0))\r\n\t                            FROM APJournal APJ1\r\n\t\t\t\t\t\t\t\tWHERE ISNULL(APJ1.IsNonStatement,'False') = 'False' AND  APJ.VendorID = APJ1.VendorID \r\n\t\t\t\t\t\t\t\t-- AND ISNULL(ISPDCRow,'False') = 'False'  \r\n\t\t\t\t\t\t\t\tAND ISNULL(IsVoid,'False')='False'  \r\n\t\t\t\t\t\t\t\tAND APDate <= '" + text + "') AS EndingBalance,\r\n\t\t\t\t\t\t\t\t(SELECT SUM(ISNULL(Debit,0)) FROM APJournal APJ2 WHERE ISNULL(APJ2.IsNonStatement,'False') = 'False' AND ISNULL(IsVoid,'False')='False' AND APJ2.VendorID = APJ.VendorID AND Debit IS NOT NULL) - ( SELECT ISNULL(SUM(ISNULL(PaymentAmount, 0)), 0) FROM AP_Payment_Allocation APP\r\n                                WHERE  APJ.VendorID = APP.VendorID )  AS Unallocated\r\n                                FROM APJournal APJ LEFT OUTER JOIN  System_Document SD ON APJ.SysDocID = SD.SysDocID \r\n                                INNER JOIN Vendor ON APJ.VendorID = Vendor.VendorID \r\n                                LEFT OUTER JOIN Payment_Term PT ON Vendor.PaymentTermID=PT.PaymentTermID \r\n                                WHERE ISNULL(SD.SysDocType,0) <> 7  AND  ISNULL(IsVoid,'False') = 'False'  ";
				str = str + " AND APDate <='" + text + "' ";
				if (vendorIDs != "")
				{
					str = str + " AND APJ.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					str = str + " AND APJ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "' ";
				}
				str += " GROUP BY APJ.VendorID, VendorName, Vendor.CurrencyID, Vendor.PaymentTermID,PT.TermName,APJ.SysDocID,APJ.VoucherID,IsNonStatement ) ";
				str += " SELECT * FROM CTE WHERE EndingBalance > 0 or Unallocated > 0  ORDER BY VendorID ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Vendor", str);
				str = "SELECT APID JournalID,SysDocID,VoucherID,VendorID,Description,APDate,APJ.APDueDate,ISNULL(ISNULL(creditFC,credit),0) AS OriginalAmount,\r\n                        (SELECT TOP 1 JobID FROM Purchase_Invoice_Detail PID WHERE PID.SysDocID=APJ.SysDocID AND PID.VoucherID=APJ.VoucherID) AS JOB,\r\n                         (SELECT S.DueDate FROM Purchase_Invoice S WHERE S.SysDocID=APJ.SysDocID AND S.VoucherID=APJ.VoucherID) AS [DueDate],\r\n                        ISNULL(APJ.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID, \r\n                        ISNULL(APJ.CurrencyRate,1) AS CurrencyRate,  \r\n                        ISNULL(ISNULL(ISNULL(creditFC,credit),0) -  \r\n                        (SELECT CASE WHEN APJ.CurrencyID IS NULL  OR APJ.CurrencyID=(SELECT CurrencyID FROM Currency WHERE IsBase='True') THEN SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0)) \r\n                        ELSE SUM(ISNULL(PaymentAmountFC,0)+ISNULL(DiscountAmountFC,0)) END FROM AP_Payment_Allocation APP\r\n                        WHERE APJ.APID=APP.APJournalID),ISNULL(ISNULL(creditFC,credit),0))  AS AmountDue,\r\n                                                       \r\n                        DATEDIFF(Day,ISNULL(APDueDate, APDate), '" + text + "') AS OverdueDays\r\n                        FROM APJournal APJ   \r\n                        --RIGHT JOIN Purchase_Invoice_Detail PID ON PID.SysDocID=SysDocID AND PID.VoucherID=VoucherID\r\n                        WHERE ISNULL(credit,0)>0 AND ISNULL(IsVoid,'False')='False' AND \r\n \r\n                        (SELECT CASE WHEN (APJ.CurrencyID IS NULL OR APJ.CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')) THEN \r\n                        ISNULL(SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0)),0) ELSE  \r\n                        ISNULL(SUM(ISNULL(PaymentAmountFC,0)+ISNULL(DiscountAmountFC,0)),0) END FROM AP_Payment_Allocation PA\r\n                        WHERE APJ.APID=PA.APJournalID)<\r\n                        CASE WHEN (APJ.CurrencyID IS NULL OR APJ.CurrencyID = (SELECT CurrencyID FROM Currency WHERE IsBase='True') )\r\n                        THEN ISNULL(ISNULL(credit,0),0) ELSE ISNULL(ISNULL(creditFC,0),0) END   ";
				str = str + " AND APDate <= '" + text + "' ";
				if (vendorIDs != "")
				{
					str = str + " AND APJ.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					str = str + " AND APJ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					str = str + " AND APJ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromJob != "")
				{
					str = str + "AND (SELECT TOP 1 JobID FROM Purchase_Invoice_Detail PID WHERE PID.SysDocID=APJ.SysDocID AND PID.VoucherID=APJ.VoucherID)= '" + fromJob + "'";
				}
				str += "  GROUP BY APJ.VendorID,APJ.APID,APJ.SysDocID,apj.VoucherID,APJ.Description,APJ.APDate,apj.CreditFC,apj.Credit,apj.CurrencyID,apj.CurrencyRate,apj.APDueDate";
				FillDataSet(dataSet, "Invoices", str);
				str = "SELECT * FROM \r\n                                    (\r\n                                    SELECT SysDocID,VoucherID,APJ.VendorID,VendorName,APDate,ISNULL(APJ.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID, APJ.APID,\r\n                                    ISNULL(ISNULL(DebitFC,Debit),0) AS OriginalAmount,\r\n                                   ISNULL(ISNULL(DebitFC,Debit),0) -   \r\n                                ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n\t\t\t\t\t\t\t\tTHEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) +SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END FROM AP_Payment_Allocation APP\r\n                                WHERE APJ.SysDocID=APP.PaymentSysDocID AND APJ.VoucherID=APP.PaymentVoucherID AND APJ.VendorID = APP.VendorID AND APJ.APID = APP.PaymentAPID GROUP BY CurrencyID),0)  AS Unallocated\r\n                                  FROM APJournal APJ   INNER JOIN Vendor ON APJ.VendorID=Vendor.VendorID\r\n                                WHERE ISNULL(Debit,0)>0 AND APJ.SysDocID NOT IN('SYS_VDS','SYS_VUA') AND ISNULL(IsVoid,'False')='False' \r\n                                AND (SELECT ISNULL(SUM(ISNULL(PaymentAmountFC,PaymentAmount)),0) FROM AP_Payment_Allocation PA\r\n\t                                WHERE PA.PaymentSysDocID=APJ.SysDocID AND PA.PaymentVoucherID=APJ.VoucherID  AND APJ.VendorID = PA.VendorID AND APJ.APID = PA.PaymentAPID)<ISNULL(ISNULL(DebitFC,Debit),0)) ALC\r\n\t\t\t\t\t\t\t\t\tWHERE Unallocated<>0 AND SysDocID <> 'SYS_VAL' ";
				str = str + " AND APDate <= '" + text + "' ";
				if (vendorIDs != "")
				{
					str = str + " AND ALC.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					str = str + " AND ALC.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND ALC.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					str = str + " AND ALC.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromJob != "")
				{
					str = str + "AND (SELECT TOP 1 JobID FROM Purchase_Invoice_Detail PID WHERE PID.SysDocID=ALC.SysDocID AND PID.VoucherID=ALC.VoucherID)= '" + fromJob + "'";
				}
				str += " ORDER BY  APDate ";
				FillDataSet(dataSet, "UnallocatedInvoices", str);
				dataSet.Relations.Add("OutstandingInvoices", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["Invoices"].Columns["VendorID"], createConstraints: false);
				dataSet.Relations.Add("UnallocatedTab", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["UnallocatedInvoices"].Columns["VendorID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryPurchaseDetail(DateTime from, DateTime to, string productID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string textCommand = " SELECT * INTO #TMP  FROM Axo_Purchase_Detail ASD   WHERE Date BETWEEN '" + text + "' AND '" + text2 + "' AND (ASD.ProductID LIKE '" + productID + "'  OR ASD.Description LIKE '" + productID + "')\r\n                                    SELECT ASD.* ,P.Description AS ProductDescription,SP.FullName AS [Buyer Name],CUS.VendorName,   P.CategoryID,PC.CategoryName AS Category \r\n                                            FROM #TMP AS ASD\r\n                                         INNER JOIN Product P ON P.ProductID = ASD.ProductID\r\n                                          INNER JOIN Vendor CUS ON CUS.VendorID = ASD.VendorID\r\n\t\t\t\t\t\t\t\t\t\t  LEFT OUTER JOIN Product_Category PC ON PC.CategoryID = P.CategoryID\r\n\t\t\t\t\t\t\t\t\t\t  LEFT OUTER JOIN Buyer SP ON SP.BuyerID = ASD.BuyerID\r\n                                            WHERE Date BETWEEN '" + text + "' AND '" + text2 + "' AND (ASD.ProductID LIKE '" + productID + "'  OR ASD.Description LIKE '" + productID + "') DROP Table #TMP";
				FillDataSet(dataSet, "Purchase", textCommand);
				textCommand = "  SELECT X.AvgPrice,X.AvgCost,X.Volume, Convert(Date,TransactionDate) AS  PriceDate\r\n                          FROM (SELECT   Convert(date,IT.Date) AS TransactionDate,ABS(SUM(Quantity)) AS Volume, CASE WHEN SUM(Quantity) = 0 THEN 0 ELSE  ROUND(sum(Quantity * UnitPrice)/SUM(Quantity),2) END as AvgPrice,\r\n\t\t                          CASE WHEN SUM(Quantity) = 0 THEN 0 ELSE   ROUND(SUM(Quantity * IT.AverageCost)/SUM(Quantity),2) END as AvgCost\r\n                                  FROM Axo_Purchase_Detail IT WHERE   IT.Date BETWEEN '" + text + "' AND '" + text2 + "' and (productid LIKE '" + productID + "' OR Description LIKE '" + productID + "')\r\n                                  GROUP BY Convert(Date,IT.Date)) X  ORDER BY PriceDate  ";
				FillDataSet(dataSet, "Price", textCommand);
				textCommand = "select PO.TransactionDate as Date,PO.VendorID as SupplierID,V.VendorName as SupplierName ,POD.ProductID,POD.JoBID,POD.Quantity,POD.UnitID,POD.UnitPrice,(POD.Quantity *POD.UnitPrice)as Amount  From purchase_order PO\r\n                      INNER JOIN Purchase_Order_Detail POD ON PO.SysDocID=POD.SysDocID and PO.VoucherID=POD.VoucherID \r\n\t\t\t\t\t  INNER JOIN Vendor V ON V.VendorID=PO.VendorID\r\n\t\t\t\t\t  LEFT JOIN Job J ON J.JobId=POD.JoBID  WHERE PO.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND (POD.ProductID LIKE '" + productID + "'  OR POD.Description LIKE '" + productID + "')";
				FillDataSet(dataSet, "PODetail", textCommand);
				textCommand = "select PR.TransactionDate as Date,PR.VendorID as SupplierID,V.VendorName as SupplierName ,PRD.ProductID,PRD.JoBID,PRD.Quantity,PRD.UnitID ,PRD.UnitPrice  From Purchase_Receipt PR\r\n                      INNER JOIN Purchase_Receipt_Detail PRD ON PR.SysDocID=PRD.SysDocID and PR.VoucherID=PRD.VoucherID \r\n\t\t\t\t\t  INNER JOIN Vendor V ON V.VendorID=PR.VendorID\r\n\t\t\t\t\t  LEFT JOIN Job J ON J.JobId=PRD.JoBID\r\n\t\t\t\t\t    WHERE PR.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND (PRD.ProductID LIKE '" + productID + "'  OR PRD.Description LIKE '" + productID + "')";
				FillDataSet(dataSet, "GRNDetail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result: false);
			}
		}

		public DataSet GetInventoryPurchaseDetailByVendor(DateTime from, DateTime to, string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string textCommand = " SELECT * INTO #TMP  FROM Axo_Purchase_Detail ASD   WHERE Date BETWEEN '" + text + "' AND '" + text2 + "' AND (ASD.VendorID LIKE '" + vendorID + "'  OR ASD.Description LIKE '" + vendorID + "')\r\n                                    SELECT ASD.* ,P.Description AS ProductDescription,SP.FullName AS [Buyer Name],CUS.VendorName,   P.CategoryID,PC.CategoryName AS Category \r\n                                            FROM #TMP AS ASD\r\n                                         INNER JOIN Product P ON P.ProductID = ASD.ProductID\r\n                                          INNER JOIN Vendor CUS ON CUS.VendorID = ASD.VendorID\r\n\t\t\t\t\t\t\t\t\t\t  LEFT OUTER JOIN Product_Category PC ON PC.CategoryID = P.CategoryID\r\n\t\t\t\t\t\t\t\t\t\t  LEFT OUTER JOIN Buyer SP ON SP.BuyerID = ASD.BuyerID\r\n                                            WHERE Date BETWEEN '" + text + "' AND '" + text2 + "' AND (ASD.VendorID LIKE '" + vendorID + "'  OR ASD.Description LIKE '" + vendorID + "') DROP Table #TMP";
				FillDataSet(dataSet, "Purchase", textCommand);
				textCommand = "  SELECT X.AvgPrice,X.AvgCost,X.Volume, Convert(Date,TransactionDate) AS  PriceDate\r\n                          FROM (SELECT   Convert(date,IT.Date) AS TransactionDate,ABS(SUM(Quantity)) AS Volume, CASE WHEN SUM(Quantity) = 0 THEN 0 ELSE  ROUND(sum(Quantity * UnitPrice)/SUM(Quantity),2) END as AvgPrice,\r\n\t\t                          CASE WHEN SUM(Quantity) = 0 THEN 0 ELSE   ROUND(SUM(Quantity * IT.AverageCost)/SUM(Quantity),2) END as AvgCost\r\n                                  FROM Axo_Purchase_Detail IT WHERE   IT.Date BETWEEN '" + text + "' AND '" + text2 + "' and (VendorID LIKE '" + vendorID + "' OR Description LIKE '" + vendorID + "')\r\n                                  GROUP BY Convert(Date,IT.Date)) X  ORDER BY PriceDate  ";
				FillDataSet(dataSet, "Price", textCommand);
				textCommand = "select PO.VoucherID,PO.TransactionDate as Date,PO.VendorID as SupplierID,V.VendorName as SupplierName ,POD.ProductID,POD.JoBID,POD.Quantity,POD.UnitID,POD.UnitPrice,(POD.Quantity *POD.UnitPrice)as Amount  From purchase_order PO\r\n                      INNER JOIN Purchase_Order_Detail POD ON PO.SysDocID=POD.SysDocID and PO.VoucherID=POD.VoucherID \r\n\t\t\t\t\t  INNER JOIN Vendor V ON V.VendorID=PO.VendorID\r\n\t\t\t\t\t  LEFT JOIN Job J ON J.JobId=POD.JoBID  WHERE PO.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND (PO.VendorID LIKE '" + vendorID + "'  OR POD.Description LIKE '" + vendorID + "')";
				FillDataSet(dataSet, "PODetail", textCommand);
				textCommand = "select PR.VoucherID,PR.TransactionDate as Date,PR.VendorID as SupplierID,V.VendorName as SupplierName ,PRD.ProductID,PRD.JoBID,PRD.Quantity,PRD.UnitID ,PRD.UnitPrice  From Purchase_Receipt PR\r\n                      INNER JOIN Purchase_Receipt_Detail PRD ON PR.SysDocID=PRD.SysDocID and PR.VoucherID=PRD.VoucherID \r\n\t\t\t\t\t  INNER JOIN Vendor V ON V.VendorID=PR.VendorID\r\n\t\t\t\t\t  LEFT JOIN Job J ON J.JobId=PRD.JoBID\r\n\t\t\t\t\t    WHERE PR.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND (PR.VendorID LIKE '" + vendorID + "'  OR PRD.Description LIKE '" + vendorID + "')";
				FillDataSet(dataSet, "GRNDetail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result: false);
			}
		}

		public DataSet GetPurchaseLogReport(string sysDocID, string ContainerNo, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			CommonLib.ToSqlDateTimeString(DateTime.Now);
			CommonLib.ToSqlDateTimeString(DateTime.Now);
			string text = "SELECT PN.VendorID,V.VendorName,PN.SysDocID,PN.VoucherID,PN.ContainerNumber,PN.TransactionDate,PN.BuyerID,B.FullName\r\n                            FROM Purchase_Invoice PN LEFT JOIN Vendor V ON PN.VendorID=V.VendorID\r\n                            LEFT JOIN Buyer B ON B.BuyerID=PN.BuyerID WHERE PN.IsImport=1 ";
			if (vendorIDs != "")
			{
				text = text + " AND PN.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text = text + " AND PN.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (ContainerNo != "")
			{
				text = text + " AND PN.ContainerNumber IN ('" + ContainerNo + "') ";
			}
			if (fromClass != "")
			{
				text = text + " AND PN.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text = text + " AND PN.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text += " ORDER BY PN.VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", text);
			DataSet dataSet2 = new DataSet();
			text = "SELECT PN.VendorID,V.VendorName,PN.SysDocID,PN.VoucherID,PN.ContainerNumber,PID.ProductID,PID.Description,PID.Quantity,PN.TransactionDate,\r\n                    ISNULL(PN.UpdatedBy,PN.CreatedBy) AS [Created By],PID.QuantityReceived,PID.UnitPrice,ISNULL(PID.UnitPriceFC,PID.UnitPrice) AS [Price],PID.Amount, ISNULL(PID.AmountFC,PID.Amount) AS [Total]\r\n                    FROM Purchase_Invoice PN LEFT JOIN Vendor V ON PN.VendorID=V.VendorID\r\n                    INNER JOIN Purchase_Invoice_Detail PID ON PN.SysDocID=PID.SysDocID AND PN.VoucherID=PID.VoucherID\r\n                    WHERE PN.IsImport=1 ";
			if (vendorIDs != "")
			{
				text = text + " AND PN.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text = text + " AND PN.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (ContainerNo != "")
			{
				text = text + " AND PN.ContainerNumber IN ('" + ContainerNo + "') ";
			}
			FillDataSet(dataSet2, "Invoice", text);
			dataSet.Merge(dataSet2);
			dataSet2.Clear();
			text = "SELECT PL.VendorID,V.VendorName,PL.SysDocID,PL.VoucherID,PL.ContainerNumber,PLD.ProductID,PLD.Description,PLD.Quantity,PL.TransactionDate,PL.ETA,\r\n                    ISNULL(PL.UpdatedBy,PL.CreatedBy) AS [Created By],PLD.UnitPrice\r\n                    FROM PO_Shipment PL LEFT JOIN Vendor V ON PL.VendorID=V.VendorID\r\n                    INNER JOIN PO_Shipment_Detail PLD ON PL.SysDocID=PLD.SysDocID AND PL.VoucherID=PLD.VoucherID ";
			if (vendorIDs != "")
			{
				text = text + " AND PL.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text = text + " AND PL.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (ContainerNo != "")
			{
				text = text + " AND PL.ContainerNumber IN ('" + ContainerNo + "') ";
			}
			FillDataSet(dataSet2, "PackingList", text);
			dataSet.Merge(dataSet2);
			dataSet2.Clear();
			text = "SELECT PR.VendorID,V.VendorName,PR.SysDocID,PR.VoucherID,PR.ContainerNumber,PRD.ProductID,PRD.Description,PRD.Quantity,PR.TransactionDate,\r\n                    ISNULL(PR.UpdatedBy,PR.CreatedBy) AS [Created By]\r\n                    FROM Purchase_Receipt PR LEFT JOIN Vendor V ON PR.VendorID=V.VendorID\r\n                    INNER JOIN Purchase_Receipt_Detail PRD ON PR.SysDocID=PRD.SysDocID AND PR.VoucherID=PRD.VoucherID\r\n                    WHERE PR.IsImport=1 ";
			if (vendorIDs != "")
			{
				text = text + " AND PR.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text = text + " AND PR.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (ContainerNo != "")
			{
				text = text + " AND PR.ContainerNumber IN ('" + ContainerNo + "') ";
			}
			FillDataSet(dataSet2, "GRNDetls", text);
			dataSet.Merge(dataSet2);
			dataSet2.Clear();
			text = "SELECT DISTINCT PO.VendorID,V.VendorName,PO.SysDocID,PO.VoucherID,POD.ProductID,POD.Description,POD.Quantity,PS.ContainerNumber,PO.ETA,POD.UnitPrice,\r\n                    PO.TransactionDate,ISNULL(PO.UpdatedBy,PO.CreatedBy) AS [Created By]\r\n                    FROM Purchase_Order PO LEFT JOIN Vendor V ON PO.VendorID=V.VendorID\r\n                    INNER JOIN Purchase_Order_Detail POD ON PO.SysDocID=POD.SysDocID AND PO.VoucherID=POD.VoucherID\r\n                    INNER JOIN PO_Shipment_Detail PSD ON PSD.SourceSysDocID=PO.SysDocID AND PSD.SourceVoucherID=PO.VoucherID\r\n                    INNER JOIN PO_Shipment PS ON PSD.SysDocID=PS.SysDocID AND PSD.VoucherID=PS.VoucherID\r\n                    WHERE PO.IsImport=1\r\n                     ";
			if (vendorIDs != "")
			{
				text = text + " AND PO.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text = text + " AND PO.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (ContainerNo != "")
			{
				text = text + " AND PS.ContainerNumber IN ('" + ContainerNo + "') ";
			}
			FillDataSet(dataSet2, "PO", text);
			dataSet.Merge(dataSet2);
			dataSet2.Clear();
			dataSet.Relations.Add("InvoiceREL", new DataColumn[2]
			{
				dataSet.Tables["Vendor"].Columns["VendorID"],
				dataSet.Tables["Vendor"].Columns["ContainerNumber"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Invoice"].Columns["VendorID"],
				dataSet.Tables["Invoice"].Columns["ContainerNumber"]
			}, createConstraints: false);
			dataSet.Relations.Add("PackingListREL", new DataColumn[2]
			{
				dataSet.Tables["Vendor"].Columns["VendorID"],
				dataSet.Tables["Vendor"].Columns["ContainerNumber"]
			}, new DataColumn[2]
			{
				dataSet.Tables["PackingList"].Columns["VendorID"],
				dataSet.Tables["PackingList"].Columns["ContainerNumber"]
			}, createConstraints: false);
			dataSet.Relations.Add("GRNDetlsREL", new DataColumn[2]
			{
				dataSet.Tables["Vendor"].Columns["VendorID"],
				dataSet.Tables["Vendor"].Columns["ContainerNumber"]
			}, new DataColumn[2]
			{
				dataSet.Tables["GRNDetls"].Columns["VendorID"],
				dataSet.Tables["GRNDetls"].Columns["ContainerNumber"]
			}, createConstraints: false);
			dataSet.Relations.Add("POREL", new DataColumn[2]
			{
				dataSet.Tables["Vendor"].Columns["VendorID"],
				dataSet.Tables["Vendor"].Columns["ContainerNumber"]
			}, new DataColumn[2]
			{
				dataSet.Tables["PO"].Columns["VendorID"],
				dataSet.Tables["PO"].Columns["ContainerNumber"]
			}, createConstraints: false);
			return dataSet;
		}

		public DataSet GetInventoryPurchaseItemDetail(string vendorID, string productID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "select TOP 3  CONVERT(VARCHAR(10), PI.TransactionDate, 110) as Date,PID.SysDocID,PID.VoucherID ,PID.JoBID,VendorName,PID.Quantity,PID.UnitID ,PID.UnitPrice  From Purchase_Invoice PI\r\n                      INNER JOIN Purchase_Invoice_Detail PID ON PI.SysDocID=PID.SysDocID and PI.VoucherID=PID.VoucherID \r\n\t\t\t\t\t  INNER JOIN Vendor V ON V.VendorID=PI.VendorID\r\n\t\t\t\t\t  LEFT JOIN Job J ON J.JobId=PID.JoBID WHERE PID.ProductID LIKE '" + productID + "'";
				if (vendorID != "")
				{
					str = str + " AND PI.VendorID='" + vendorID + "' ";
				}
				str += " ORDER BY PI.TransactionDate DESC";
				FillDataSet(dataSet, "PurchaseItem", str);
				if (vendorID == "")
				{
					dataSet.Clear();
					str = (str = "select TOP 5  CONVERT(VARCHAR(10), PI.TransactionDate, 110) as Date,PID.SysDocID,PID.VoucherID,PID.JoBID,VendorName,PID.Quantity,PID.UnitID ,PID.UnitPrice  From Purchase_Invoice PI\r\n                      INNER JOIN Purchase_Invoice_Detail PID ON PI.SysDocID=PID.SysDocID and PI.VoucherID=PID.VoucherID \r\n\t\t\t\t\t  INNER JOIN Vendor V ON V.VendorID=PI.VendorID\r\n\t\t\t\t\t  LEFT JOIN Job J ON J.JobId=PID.JoBID WHERE PID.ProductID LIKE '" + productID + "'");
					str += " ORDER BY PI.TransactionDate DESC";
					FillDataSet(dataSet, "PurchaseItem", str);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result: false);
			}
		}

		public DataSet GetInventoryPurchaseItemDetail(string vendorID, string productID, bool loadAll)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				text = (loadAll ? ("select   CONVERT(VARCHAR(10), PI.TransactionDate, 110) as Date,PID.SysDocID,PID.VoucherID ,PID.JoBID,VendorName,PID.Quantity,PID.UnitID ,PID.UnitPrice  From Purchase_Invoice PI\r\n                      INNER JOIN Purchase_Invoice_Detail PID ON PI.SysDocID=PID.SysDocID and PI.VoucherID=PID.VoucherID \r\n\t\t\t\t\t  INNER JOIN Vendor V ON V.VendorID=PI.VendorID\r\n\t\t\t\t\t  LEFT JOIN Job J ON J.JobId=PID.JoBID WHERE PID.ProductID LIKE '" + productID + "'") : ("select TOP 3  CONVERT(VARCHAR(10), PI.TransactionDate, 110) as Date,PID.SysDocID,PID.VoucherID ,PID.JoBID,VendorName,PID.Quantity,PID.UnitID ,PID.UnitPrice  From Purchase_Invoice PI\r\n                      INNER JOIN Purchase_Invoice_Detail PID ON PI.SysDocID=PID.SysDocID and PI.VoucherID=PID.VoucherID \r\n\t\t\t\t\t  INNER JOIN Vendor V ON V.VendorID=PI.VendorID\r\n\t\t\t\t\t  LEFT JOIN Job J ON J.JobId=PID.JoBID WHERE PID.ProductID LIKE '" + productID + "'"));
				if (vendorID != "")
				{
					text = text + " AND PI.VendorID='" + vendorID + "' ";
				}
				text += " ORDER BY PI.TransactionDate DESC";
				FillDataSet(dataSet, "PurchaseItem", text);
				if (vendorID == "")
				{
					dataSet.Clear();
					text = (loadAll ? ("select  CONVERT(VARCHAR(10), PI.TransactionDate, 110) as Date,PID.SysDocID,PID.VoucherID,PID.JoBID,VendorName,PID.Quantity,PID.UnitID ,PID.UnitPrice  From Purchase_Invoice PI\r\n                          INNER JOIN Purchase_Invoice_Detail PID ON PI.SysDocID=PID.SysDocID and PI.VoucherID=PID.VoucherID \r\n\t\t\t\t\t      INNER JOIN Vendor V ON V.VendorID=PI.VendorID\r\n\t\t\t\t\t      LEFT JOIN Job J ON J.JobId=PID.JoBID WHERE PID.ProductID LIKE '" + productID + "'") : ("select TOP 5  CONVERT(VARCHAR(10), PI.TransactionDate, 110) as Date,PID.SysDocID,PID.VoucherID,PID.JoBID,VendorName,PID.Quantity,PID.UnitID ,PID.UnitPrice  From Purchase_Invoice PI\r\n                      INNER JOIN Purchase_Invoice_Detail PID ON PI.SysDocID=PID.SysDocID and PI.VoucherID=PID.VoucherID \r\n\t\t\t\t\t  INNER JOIN Vendor V ON V.VendorID=PI.VendorID\r\n\t\t\t\t\t  LEFT JOIN Job J ON J.JobId=PID.JoBID WHERE PID.ProductID LIKE '" + productID + "'"));
					text += " ORDER BY PI.TransactionDate DESC";
					FillDataSet(dataSet, "PurchaseItem", text);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result: false);
			}
		}

		public DataSet GetVendorOutstandingInvoicesDetailReport(DateTime reportDate, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool isFC, string vendorIDs)
		{
			string str = CommonLib.ToSqlDateTimeString(reportDate);
			string str2 = "\tSELECT T.*,J.JobName,V.VendorName FROM (\r\n                            SELECT APID JournalID,VendorID,SysDocID,VoucherID,Description,APDate,ISNULL(ISNULL(creditFC,credit),0) AS OriginalAmount,\r\n                            ISNULL((SELECT TOP 1 JobID FROM TRANSACTION_DETAILS PID WHERE PID.SysDocID=APJ.SysDocID AND PID.VoucherID=APJ.VoucherID),\r\n                            (SELECT TOP 1 JobID FROM Purchase_Invoice_Detail PID WHERE PID.SysDocID=APJ.SysDocID AND PID.VoucherID=APJ.VoucherID)) AS JOB, \r\n                            ((SELECT CASE WHEN APJ.CurrencyID IS NULL  OR APJ.CurrencyID=(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                            THEN SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0))\r\n                            ELSE SUM(ISNULL(PaymentAmountFC,0)+ISNULL(DiscountAmountFC,0)) END FROM AP_Payment_Allocation APP\r\n                            WHERE APJ.APID=APP.APJournalID))  AS PaidAmount,\r\n                            (SELECT SUM(PaymentAmount) FROM AP_Payment_Allocation APP1 WHERE APP1.InvoiceSysDocID=APJ.SysDocID\r\n                            AND APP1.InvoiceVoucherID=APJ.VoucherID AND APP1.VendorID=APJ.VendorID\r\n                            AND ISNULL((SELECT SUM(ISNULL(ISNULL(creditFC,credit),0)) FROM Journal_Details WHERE SysDocID=APP1.PaymentSysDocID\r\n                            AND VoucherID=APP1.PaymentVoucherID AND CheckbookID IS NOT NULL),0) > 0) AS PDC\r\n                            FROM APJournal APJ WHERE ISNULL(credit,0)>0 AND ISNULL(IsVoid,'False')='False' \r\n                            ) AS T\r\n                            LEFT JOIN Job J ON T.JOB=J.JobID\r\n                            INNER JOIN Vendor V ON V.VendorID=T.VendorID\r\n                            WHERE 1=1  ";
			str2 = str2 + " AND APDate <='" + str + "' ";
			if (vendorIDs != "")
			{
				str2 = str2 + " AND V.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str2 = str2 + " AND V.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str2 = str2 + " AND V.VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "' ";
			}
			if (fromGroup != "")
			{
				str2 = str2 + " AND V.VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "' ";
			}
			if (fromJob != "")
			{
				str2 = str2 + " AND T.JOB BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", str2);
			str2 = "SELECT AP.*,SD.DocName FROM AP_Payment_Allocation AP INNER JOIN System_Document SD ON AP.PaymentSysDocID=SD.SysDocID WHERE 1=1";
			if (vendorIDs != "")
			{
				str2 = str2 + " AND AP.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				str2 = str2 + " AND AP.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				str2 = str2 + " AND AP.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str2 = str2 + " AND AP.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			FillDataSet(dataSet, "InvoicesDetail", str2);
			dataSet.Relations.Add("Vendor_Rel", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["InvoicesDetail"].Columns["VendorID"], createConstraints: false);
			dataSet.Relations.Add("InvoicesDetail_Rel", new DataColumn[2]
			{
				dataSet.Tables["Vendor"].Columns["SysDocID"],
				dataSet.Tables["Vendor"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["InvoicesDetail"].Columns["InvoiceSysDocID"],
				dataSet.Tables["InvoicesDetail"].Columns["InvoiceVoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public DataSet GetTransactionDetails(string VendorID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * from APJournal where vendorid='" + VendorID + "'";
			FillDataSet(dataSet, "Customer", textCommand);
			return dataSet;
		}

		public string GetServiceProviderAccountID(string ProviderID)
		{
			new DataSet();
			string exp = "SELECT APAccountID\r\n                           FROM Vendor where VendorID='" + ProviderID + "'";
			return ExecuteScalar(exp).ToString();
		}

		public DataSet GetPurchaseQuoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			string text3 = "SELECT DISTINCT PQ.VendorID,V.VendorName FROM Purchase_Quote PQ\r\n                                INNER JOIN Vendor V ON V.VendorID = PQ.VendorID \r\n\t\t\t\t\t\t\t\t";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromVendor != "")
			{
				text3 = text3 + " AND PQ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PQ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PQ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " GROUP BY PQ.VendorID,V.VendorName ORDER BY VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select PQD.ProductID,\r\n\t\t\t\t\t\tTransactionDate,PQD.VoucherID,PQ.VendorAddress + '-' + VendorName AS 'Vendor',PQ.VendorID,PQD.Description, PQ.CurrencyID, PQ.Discount,  PQ.Note, PQ.PONumber,\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tPQD.Quantity,UnitPrice, (PQD.Quantity * UnitPrice) AS Amount\r\n\r\n                        FROM Purchase_Quote_Detail PQD INNER JOIN Purchase_Quote PQ ON\r\n\r\n                        PQD.SysDocID = PQ.SysDocID AND PQD.VoucherID = PQ.VoucherID\r\n\r\n                        INNER JOIN Product PR ON PQD.ProductID = PR.ProductID INNER JOIN\r\n\r\n                        Vendor ON Vendor.VendorID = PQ.VendorID \r\n                                   ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromItem != "")
			{
				text3 = text3 + " AND PQD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromItemClass != "")
			{
				text3 = text3 + " AND PQD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
			}
			if (fromItemCategory != "")
			{
				text3 = text3 + " AND PQD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
			}
			if (fromManufacturer != "")
			{
				text3 = text3 + " AND PQD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text3 = text3 + " AND PQD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text3 = text3 + " AND PQD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromBrand != "")
			{
				text3 = text3 + " AND PQD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PQ.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PQ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PQ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromBuyer != "")
			{
				text3 = text3 + " AND PQ.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			text3 += " GROUP BY PQ.TransactionDate,PQ.VendorID,Vendor.VendorName,PQD.ProductID,TransactionDate,PQD.VoucherID,PQD.Quantity,UnitPrice, PQD.Description, PQ.CurrencyID, PQ.Discount, PQ.Note, PQ.PONumber, PQ.VendorAddress ORDER BY PQ.VendorID";
			FillDataSet(dataSet2, "PurchaseQuote", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("PurchaseQuoteREL", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["PurchaseQuote"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchaseQuoteSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT PQ.VendorID,PQ.*, TermName, B.FullName AS [Buyer], VendorName FROM Purchase_Quote PQ\r\n                                INNER JOIN Vendor V \r\n\t\t\t\t\t\t\t\tON V.VendorID = PQ.VendorID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Payment_Term PT On PQ.TermID=PT.PaymentTermID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Buyer B ON PQ.BuyerID=B.BuyerID\r\n                                 WHERE [TransactionDate] BETWEEN'" + text + "' AND '" + text2 + "' ";
				if (fromCustomer != "")
				{
					text3 = text3 + " AND PQ.VendorID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND PQ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND PQ.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromBuyer != "")
				{
					text3 = text3 + " AND PQ.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				text3 += "Group By PQ.VendorID,PQ.SysDocID, PQ.VoucherID,VendorName, PQ.TransactionDate, PQ.RequiredDate, \r\n                        PQ.ShippingAddressID,PQ.ShippingMethodID, PQ.Status, PQ.CurrencyID, PQ.TermID,PQ.TaxOption,PQ.PayeeTaxGroupID,   PQ.IsVoid, PQ.Reference, \r\n                        PQ.Discount,PQ.TaxAmount,PQ.TaxAmountFC, PQ.Total, PQ.PONumber,PQ.Note, PQ.ApprovalStatus,PQ.CurrencyID,PQ.DateCreated, PQ.DateUpdated, PQ.CreatedBy, \r\n                        PQ.UpdatedBy, TermName, PQ.IsImport, PQ.DueDate, PQ.PurchaseFlow, PQ.BuyerID, PQ.VendorAddress, PQ.Reference2,PQ.JobID,PQ.CostCategoryID, B.FullName, PQ.PriceIncludeTax,PQ.VendorReferenceNo, PQ.DivisionID,PQ.CompanyID";
				FillDataSet(dataSet, "PurchaseQuote", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			string text3 = "SELECT DISTINCT PI.VendorID,V.VendorName FROM Purchase_Invoice PI\r\n                                INNER JOIN Vendor V ON V.VendorID = PI.VendorID \r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PI.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " GROUP BY PI.VendorID,V.VendorName ORDER BY VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select PID.ProductID,TransactionDate,PID.VoucherID,PI.VendorID + '-' + VendorName AS 'Vendor',PI.VendorID,PID.Description, PI.CurrencyID, PI.Discount,  PI.Note, PI.PONumber,\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tPID.Quantity,UnitPrice, (PID.Quantity * UnitPrice) AS Amount\r\n\r\n                        FROM Purchase_Invoice_Detail PID INNER JOIN Purchase_Invoice PI ON\r\n\r\n                        PID.SysDocID = PI.SysDocID AND PID.VoucherID = PI.VoucherID\r\n\r\n                        INNER JOIN Product PR ON PID.ProductID = PR.ProductID INNER JOIN\r\n\r\n                        Vendor ON Vendor.VendorID = PI.VendorID\r\n                                     ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromItem != "")
			{
				text3 = text3 + " AND PID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromItemClass != "")
			{
				text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
			}
			if (fromItemCategory != "")
			{
				text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
			}
			if (fromManufacturer != "")
			{
				text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromBrand != "")
			{
				text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
			}
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PI.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PI.VendorID IN (SELECT VendorID FROM Vendor WHERE WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromBuyer != "")
			{
				text3 = text3 + " AND PI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			text3 += " GROUP BY PI.TransactionDate,PI.VendorID,Vendor.VendorName,PID.ProductID,TransactionDate,PID.VoucherID,PID.Quantity,UnitPrice, PID.Description, PI.CurrencyID, PI.Discount, PI.Note, PI.PONumber  ORDER BY PI.VendorID";
			FillDataSet(dataSet2, "PurchaseInvoiceDetail", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("PurchaseInvoiceREL", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["PurchaseInvoiceDetail"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchaseInvoiceSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT PI.VendorID,PI.*, TermName, B.FullName AS [Buyer], VendorName FROM Purchase_Invoice PI\r\n                                INNER JOIN Vendor V \r\n\t\t\t\t\t\t\t\tON V.VendorID = PI.VendorID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Payment_Term PT On PI.TermID=PT.PaymentTermID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Buyer B ON PI.BuyerID=B.BuyerID\r\n                                WHERE [TransactionDate] BETWEEN'" + text + "' AND '" + text2 + "' ";
				if (vendorIDs != "")
				{
					text3 = text3 + " AND PI.VendorID IN(" + vendorIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND PI.VendorID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND PI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND PI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromBuyer != "")
				{
					text3 = text3 + " AND PI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				text3 += " Group By PI.VendorID,PI.SysDocID, PI.VoucherID,VendorName, PI.TransactionDate,PI.ShippingMethodID, PI.Status, PI.CurrencyID, PI.TermID,PI.TaxOption,PI.PayeeTaxGroupID,PI.IsTaxIncluded, \r\n                            PI.IsVoid,PI.ApprovalStatus, PI.VerificationStatus, PI.Reference, PI.Discount,PI.TaxAmount, PI.Total, PI.PONumber,PI.Note,PI.CurrencyID,PI.DateCreated, PI.DateUpdated, PI.CreatedBy, PI.UpdatedBy,\r\n                            TermName, PI.IsImport, PI.DueDate, PI.PurchaseFlow, PI.BuyerID,PI.Reference2, B.FullName, PI.IsCash,PI.RegisterID, PI.CurrencyRate, PI.SourceDocType, PI.ContainerNumber, PI.Port, PI.BOLNumber, PI.Shipper,\r\n                            PI.ClearingAgent, PI.DiscountFC, PI.TaxAmountFC,PI.TotalFC, PI.PayeeTaxGroupID, PI.PriceIncludeTax,PI.VendorReferenceNo, PI.CompanyID,PI.DivisionID,PI.IsHoldForPayment";
				FillDataSet(dataSet, "PurchaseInvoice", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseOrderDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromJob, string toJob, string fromLocation, string toLocation, bool IsImport, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			string text3 = "SELECT DISTINCT PO.VendorID,V.VendorName FROM Purchase_Order PO\r\n                                INNER JOIN Vendor V ON V.VendorID = PO.VendorID \r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PO.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PO.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PO.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PO.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " GROUP BY PO.VendorID,V.VendorName ORDER BY VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select POD.ProductID,TransactionDate,POD.VoucherID,J.JobName,PO.SysDocID,\r\n                        PO.VendorID + '-' + VendorName AS 'Vendor',PO.VendorID,POD.Description,\r\n                        PO.CurrencyID, PO.Discount,  PO.Note, PO.PONumber,\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tPOD.Quantity,UnitPrice, (POD.Quantity * UnitPrice) AS Amount, PO.LocationID,L.LocationName\r\n\r\n                        FROM Purchase_Order_Detail POD INNER JOIN Purchase_Order PO ON\r\n\r\n                        POD.SysDocID = PO.SysDocID AND POD.VoucherID = PO.VoucherID\r\n                        LEFT JOIN Location L ON PO.LocationID=L.LocationID\r\n                        INNER JOIN Product PR ON POD.ProductID = PR.ProductID \r\n\t\t\t\t\t\tINNER JOIN Vendor ON Vendor.VendorID = PO.VendorID\r\n\t\t\t\t\t\tLEFT JOIN Job J On POD.JobID=J.JobID  \r\n                                    ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromItem != "")
			{
				text3 = text3 + " AND POD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromItemClass != "")
			{
				text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
			}
			if (fromItemCategory != "")
			{
				text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
			}
			if (fromManufacturer != "")
			{
				text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromBrand != "")
			{
				text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
			}
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PO.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PO.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PO.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PO.VendorID IN (SELECT VendorID FROM Vendor WHERE WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromBuyer != "")
			{
				text3 = text3 + " AND PO.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			if (fromJob != "")
			{
				text3 = text3 + " AND JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND PO.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 = text3 + " AND ISNULL(IsImport,'False')= '" + IsImport.ToString() + "' ";
			text3 += " GROUP BY PO.TransactionDate,PO.VendorID,PO.SysDocID,Vendor.VendorName,POD.ProductID,TransactionDate,POD.VoucherID,POD.Quantity,UnitPrice, \r\n                      POD.Description, PO.CurrencyID, PO.Discount, PO.Note, PO.PONumber, J.JobName,PO.LocationID,L.LocationName ORDER BY PO.VendorID";
			FillDataSet(dataSet2, "PurchaseOrderDetail", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("PurchaseOrderREL", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["PurchaseOrderDetail"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchaseOrderSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromJob, string toJob, bool IsImport, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT PO.VendorID,PO.*, TermName, B.FullName AS [Buyer], VendorName FROM Purchase_Order PO\r\n                                INNER JOIN Vendor V \r\n\t\t\t\t\t\t\t\tON V.VendorID = PO.VendorID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Payment_Term PT On PO.TermID=PT.PaymentTermID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Buyer B ON PO.BuyerID=B.BuyerID\r\n                                WHERE [TransactionDate] BETWEEN'" + text + "' AND '" + text2 + "' ";
				if (vendorIDs != "")
				{
					text3 = text3 + " AND PO.VendorID IN(" + vendorIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND PO.VendorID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND PO.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND PO.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromBuyer != "")
				{
					text3 = text3 + " AND PO.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				text3 = text3 + " AND ISNULL(IsImport,'False')= '" + IsImport.ToString() + "' ";
				text3 += "Group By PO.VendorID,PO.SysDocID, PO.VoucherID,VendorName, PO.TransactionDate,PO.ShippingMethodID, PO.Status, PO.CurrencyID, PO.TermID,  PO.IsVoid, PO.Reference, PO.Discount,PO.TaxAmount, PO.Total, \r\n                        PO.PONumber,PO.Note,PO.CurrencyID,PO.DateCreated, PO.DateUpdated, PO.CreatedBy, PO.UpdatedBy, TermName, PO.IsImport, PO.DueDate, PO.PurchaseFlow, PO.BuyerID,PO.Reference2, B.FullName,PO.SourceDocType,PO.ContainerSizeID,\r\n                    PO.RequiredDate,  PO.VendorAddress, Po.PortLoading,PO.PortDestination, PO.ETA, PO.ETD, PO.ActualReqDate, PO.INCOID, PO.IsShipped, PO.ApprovalStatus, PO.VerificationStatus, PO.JobID, PO.CostCategoryID, PO.DeliveryAddressID, \r\n                    PO.BillingAddressID, PO.DeliveryAddress, PO.Remarks1, PO.Remarks2,PO.PriceIncludeTax,PO.PayeeTaxGroupID, PO.LocationID, PO.TaxOption, PO.TaxGroupID, PO.TaxPercentage,PO.VendorReferenceNo,PO.DivisionID,PO.CompanyID, PO.CurrencyRate";
				FillDataSet(dataSet, "PurchaseOrder", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchasePLDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			string text3 = "SELECT DISTINCT PS.VendorID,V.VendorName FROM PO_Shipment PS\r\n                                INNER JOIN Vendor V ON V.VendorID = PS.VendorID \r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PS.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PS.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " GROUP BY PS.VendorID,V.VendorName ORDER BY VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select PSD.ProductID,TransactionDate,PSD.VoucherID,PS.VendorID + '-' + VendorName AS 'Vendor',PS.VendorID,PSD.Description, PS.CurrencyID, PS.Note, PS.PONumber,\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tPSD.Quantity,UnitPrice, (PSD.Quantity * UnitPrice) AS Amount\r\n\r\n                        FROM PO_Shipment_Detail PSD INNER JOIN PO_Shipment PS ON\r\n\r\n                        PSD.SysDocID = PS.SysDocID AND PSD.VoucherID = PS.VoucherID\r\n\r\n                        INNER JOIN Product PR ON PSD.ProductID = PR.ProductID INNER JOIN\r\n\r\n                        Vendor ON Vendor.VendorID = PS.VendorID \r\n                                  ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromItem != "")
			{
				text3 = text3 + " AND PSD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromItemClass != "")
			{
				text3 = text3 + " AND PSD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
			}
			if (fromItemCategory != "")
			{
				text3 = text3 + " AND PSD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
			}
			if (fromManufacturer != "")
			{
				text3 = text3 + " AND PSD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text3 = text3 + " AND PSD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text3 = text3 + " AND PSD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PS.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PS.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromBuyer != "")
			{
				text3 = text3 + " AND PS.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			text3 += " GROUP BY PS.TransactionDate,PS.VendorID,Vendor.VendorName,PSD.ProductID,TransactionDate,PSD.VoucherID,PSD.Quantity,UnitPrice, PSD.Description, PS.CurrencyID,  PS.Note, PS.PONumber ORDER BY PS.VendorID";
			FillDataSet(dataSet2, "PurchasePLDetail", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("PurchasePLREL", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["PurchasePLDetail"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchasePLSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT PS.VendorID,PS.*,  B.FullName AS [Buyer], VendorName FROM PO_Shipment PS\r\n                                INNER JOIN Vendor V \r\n\t\t\t\t\t\t\t\tON V.VendorID = PS.VendorID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Buyer B ON PS.BuyerID=B.BuyerID\r\n                                WHERE [TransactionDate] BETWEEN'" + text + "' AND '" + text2 + "' ";
				if (vendorIDs != "")
				{
					text3 = text3 + " AND PS.VendorID IN(" + vendorIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND PS.VendorID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromBuyer != "")
				{
					text3 = text3 + " AND PS.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				text3 += "Group By PS.VendorID,PS.SysDocID, PS.VoucherID,VendorName, PS.TransactionDate,PS.Status, PS.CurrencyID,PS.IsVoid, PS.Reference,  PS.PONumber,PS.Note,PS.CurrencyID,PS.DateCreated, PS.DateUpdated, PS.CreatedBy, PS.UpdatedBy, PS.PurchaseFlow,\r\n                         PS.BuyerID,B.FullName,PS.ContainerNumber, PS.Port, PS.BOLNumber, PS.Shipper, PS.ClearingAgent, PS.LoadingPort, PS.ETA, PS.ATD, PS.ShippingMethodID, PS.Weight, PS.IsReceived, PS.Value, PS.ShipStatus, PS.TransporterID, Ps.ContainerSizeID,\r\n                        PS.ApprovalStatus, PS.VerificationStatus,PS.VendorReferenceNo,PS.CompanyID,PS.DivisionID";
				FillDataSet(dataSet, "PurchasePLSummary", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetcashPurchaseDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			string text3 = "SELECT DISTINCT PS.VendorID,V.VendorName FROM PO_Shipment PS\r\n                                INNER JOIN Vendor V ON V.VendorID = PS.VendorID \r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PS.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PS.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " GROUP BY PS.VendorID,V.VendorName ORDER BY VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select PSD.ProductID,TransactionDate,PSD.VoucherID,PS.VendorID + '-' + VendorName AS 'Vendor',PS.VendorID,PSD.Description, PS.CurrencyID, PS.Note, PS.PONumber,\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tPSD.Quantity,UnitPrice, (PSD.Quantity * UnitPrice) AS Amount\r\n\r\n                        FROM PO_Shipment_Detail PSD INNER JOIN PO_Shipment PS ON\r\n\r\n                        PSD.SysDocID = PS.SysDocID AND PSD.VoucherID = PS.VoucherID\r\n\r\n                        INNER JOIN Product PR ON PSD.ProductID = PR.ProductID INNER JOIN\r\n\r\n                        Vendor ON Vendor.VendorID = PS.VendorID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromItem != "")
			{
				text3 = text3 + " AND PSD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromItemClass != "")
			{
				text3 = text3 + " AND PSD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
			}
			if (fromItemCategory != "")
			{
				text3 = text3 + " AND PSD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
			}
			if (fromBrand != "")
			{
				text3 = text3 + " AND PSD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
			}
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PSD.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PS.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromBuyer != "")
			{
				text3 = text3 + " AND PS.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			text3 += " GROUP BY PS.TransactionDate,PS.VendorID,Vendor.VendorName,PSD.ProductID,TransactionDate,PSD.VoucherID,PSD.Quantity,UnitPrice, PSD.Description, PS.CurrencyID,  PS.Note, PS.PONumber ORDER BY PS.VendorID";
			FillDataSet(dataSet2, "PurchasePLDetail", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("PurchasePLREL", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["PurchasePLDetail"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetCashPurchaseSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT PS.VendorID,PS.*,  B.FullName AS [Buyer], VendorName FROM PO_Shipment PS\r\n                                INNER JOIN Vendor V \r\n\t\t\t\t\t\t\t\tON V.VendorID = PS.VendorID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Buyer B ON PS.BuyerID=B.BuyerID\r\n                                WHERE [TransactionDate] BETWEEN'" + text + "' AND '" + text2 + "' ";
				if (vendorIDs != "")
				{
					text3 = text3 + " AND PS.VendorID IN(" + vendorIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND PS.VendorID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND PS.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromBuyer != "")
				{
					text3 = text3 + " AND PS.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				text3 += "Group By PS.VendorID,PS.SysDocID, PS.VoucherID,VendorName, PS.TransactionDate,PS.Status, PS.CurrencyID,PS.IsVoid, PS.Reference,  PS.PONumber,PS.Note,PS.CurrencyID,PS.DateCreated, PS.DateUpdated, PS.CreatedBy, PS.UpdatedBy, PS.PurchaseFlow, PS.BuyerID,B.FullName,PS.ContainerNumber, PS.Port, PS.BOLNumber, PS.Shipper, PS.ClearingAgent, PS.LoadingPort, PS.ETA, PS.ATD, PS.ShippingMethodID, PS.Weight, PS.IsReceived, PS.Value, PS.ShipStatus, PS.TransporterID, Ps.ContainerSizeID";
				FillDataSet(dataSet, "PurchasePLSummary", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseGRNDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			string text3 = "SELECT DISTINCT PR.VendorID,V.VendorName FROM Purchase_Receipt PR\r\n                                INNER JOIN Vendor V ON V.VendorID = PR.VendorID  \r\n\r\n            ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PR.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PR.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " GROUP BY PR.VendorID,V.VendorName ORDER BY VendorID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vendor", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select PRD.*,P.Description2,TransactionDate,PR.VendorID + '-' + VendorName AS 'Vendor',PR.VendorID, PR.CurrencyID, PR.Discount,  PR.Note, PR.PONumber,PR.SysDocID,\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\t (PRD.Quantity * UnitPrice) AS Amount, B.FullName AS Buyer, B.BuyerID\r\n\r\n                        FROM Purchase_Receipt_Detail PRD INNER JOIN Purchase_Receipt PR ON\r\n\r\n                        PRD.SysDocID = PR.SysDocID AND PRD.VoucherID = PR.VoucherID\r\n\r\n                        INNER JOIN Product P ON PRD.ProductID = P.ProductID INNER JOIN\r\n\r\n                        Vendor ON Vendor.VendorID = PR.VendorID LEFT JOIN \r\n\t\t\t\t\t\tBuyer B ON PR.BuyerID =B.BuyerID \r\n                                    ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromItem != "")
			{
				text3 = text3 + " AND PRD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromItemClass != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
			}
			if (fromItemCategory != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
			}
			if (fromManufacturer != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromBrand != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
			}
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PR.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PR.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromBuyer != "")
			{
				text3 = text3 + " AND PR.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND PRD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += "GROUP By PR.TransactionDate,PR.VendorID,Vendor.VendorName,PRD.ProductID,TransactionDate,PRD.VoucherID,PRD.Quantity,UnitPrice,PRD.Description,PR.CurrencyID, \r\n                    PR.Discount, PR.Note, PR.PONumber , B.FullName, B.BuyerID,PR.SysDocID, PRD.SysDocID,PRD.QuantityReturned,PRD.Remarks,prd.UnitID,PRD.UnitQuantity,PRD.UnitFactor,\r\n                    PRD.FactorType,PRD.RowIndex,prd.JobID,PRD.TaxOption,PRD.TaxGroupID,PRD.LocationID,PRD.SpecificationID,PRD.StyleID ,PRD.OrderVoucherID ,PRD.OrderSysDocID,PRD.OrderRowIndex ,\r\n                    PRD.IsPORRow,PRD.RowSource,PRD.PKSysDocID ,PRD.PKRowIndex,PRD.PKVoucherID,PRD.ListVoucherID,PRD.ListSysDocID,PRD.ListRowIndex,P.Description2,PRD.ITRowID ";
			FillDataSet(dataSet2, "PurchaseGRNDetail", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("PurchaseGRNREL", dataSet.Tables["Vendor"].Columns["VendorID"], dataSet.Tables["PurchaseGRNDetail"].Columns["VendorID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchaseGRNSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT PR.VendorID,PR.*, TermName, B.FullName AS [Buyer], VendorName, CS.ContainerSizeName,(SELECT SUM(Quantity*UnitPrice) FROM Purchase_Receipt_Detail  \r\n                                PRD WHERE PRD.SysDocID=PR.SysDocID AND PRD.VoucherID=PR.VoucherID) AS [TotalAmount] FROM Purchase_Receipt PR\r\n                                INNER JOIN Vendor V \r\n\t\t\t\t\t\t\t\tON V.VendorID = PR.VendorID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Payment_Term PT On PR.TermID=PT.PaymentTermID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Buyer B ON PR.BuyerID=B.BuyerID\r\n\t\t\t\t\t\t\t\tLEFT JOIN ContainerSize CS ON PR.ContainerSizeID=CS.ContainerSizeID\r\n                                WHERE [TransactionDate] BETWEEN'" + text + "' AND '" + text2 + "' ";
				if (vendorIDs != "")
				{
					text3 = text3 + " AND PR.VendorID IN(" + vendorIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND PR.VendorID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromBuyer != "")
				{
					text3 = text3 + " AND PR.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				text3 += "  Group By PR.VendorID,PR.SysDocID, PR.VoucherID,VendorName, PR.TransactionDate, PR.Status, PR.CurrencyID, PR.TermID,PR.TaxOption,PR.PayeeTaxGroupID,PR.IsVoid, PR.Reference,PR.Discount,PR.TaxAmount, \r\n                    PR.Total, PR.PONumber,PR.Note,PR.CurrencyID,PR.DateCreated, PR.DateUpdated, PR.CreatedBy, PR.UpdatedBy, TermName, PR.IsImport, PR.PurchaseFlow, PR.BuyerID,PR.Reference2, B.FullName,  PR.CurrencyRate, PR.SourceDocType,\r\n                    PR.ContainerNumber,PR.DiscountFC, PR.TaxAmountFC,PR.TotalFC, PR.ShippingMethodID, PR.IsInvoiced, PR.SourceSysDocID, PR.SourceVoucherID,PR.InvoiceSysDocID, PR.InvoiceVoucherID, PR.POSysDocID, PR.POVoucherID, PR.TransporterID,\r\n                    PR.VehicleID, PR.ContainerSizeID, CS.ContainerSizeName, PR.ApprovalStatus, PR.VerificationStatus,PR.VendorReferenceNo, PR.CompanyID, PR.DivisionID, PR.DriverID,PR.ClaimStatus,PR.GroupName,PR.ClaimAmount,PR.ClaimAmountFC,PR.ClaimCurrencyID,PR.ClaimCurrencyRate,PR.ClaimRef1,PR.ClaimRef2,PR.ClaimRemarks";
				FillDataSet(dataSet, "PurchaseGRN", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetVendorOutstandingSummaryReport(DateTime reportDate, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(reportDate);
				DataSet dataSet = new DataSet();
				string text2 = "SELECT T.*,(T.[Due Amount] + T.PDC)[Due Amount],T.[Due Amount] AS[Bal.Less PDC]\r\n                                FROM(\r\n                                SELECT DISTINCT Vendor.VendorID, Vendor.VendorName, Vendor.CreditAmount,\r\n                                ISNULL((SELECT SUM(ISNULL(Credit, 0) + 0 - 0 - ISNULL(Debit, 0)) FROM APJournal APJ2\r\n                                WHERE Vendor.VendorID = APJ2.VendorID AND APJ2.APDate <= '" + text + "'\r\n                                AND ISNULL(ISPDCRow, 'False') = 'False'  AND ISNULL(IsVoid, 'False') = 'False'), 0) AS[Due Amount],\r\n                                (SELECT ISNULL(SUM(ISNULL(Amount, 0) + 0), 0) AS PDC FROM Cheque_Issued ChqIss WHERE Status IN (2,4) AND ISNULL(IsVoid,'False')='False'\r\n                                AND ( ChqIss.ClearanceDate IS NULL OR Status IN (4) AND ClearanceDate > '" + text + "') AND IssueDate <= '" + text + "'  AND ChqIss.PayeeType = 'V' AND ChqIss.PayeeID = Vendor.VendorID) AS PDC\r\n                                FROM Vendor LEFT OUTER JOIN APJournal APJ ON APJ.VendorID = Vendor.VendorID WHERE\r\n                                ISNULL(IsVoid, 'False') = 'False'  AND APJ.APDate <= '" + text + "') T WHERE ((T.[Due Amount])<>0 OR (T.PDC)<>0)\r\n                                   ";
				if (vendorIDs != "")
				{
					text2 = text2 + " AND T.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					text2 = text2 + " AND T.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromClass != "")
				{
					text2 = text2 + " AND T.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					text2 = text2 + " AND T.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				text2 += " ORDER BY T.VendorID ";
				FillDataSet(dataSet, "Invoices", text2);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public string GetVendorAddressPrintFormat(string vendorID, string addressID)
		{
			new DataSet();
			string exp = "SELECT AddressPrintFormat\r\n                           FROM  Vendor_Address CA  \r\n                           WHERE CA.VendorID='" + vendorID + "' AND CA.AddressID = '" + addressID + "'";
			object obj = ExecuteScalar(exp);
			if (obj.IsDBNullOrEmpty())
			{
				return "";
			}
			return obj.ToString();
		}

		public DataSet GetVendorOutstandingInvoicesList(string fromVendor, bool isFC)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT APID JournalID,SysDocID,VoucherID,VendorID,Description,APDate,APJ.APDueDate,ISNULL(ISNULL(creditFC,credit),0) AS OriginalAmount,\r\n                        (SELECT TOP 1 JobID FROM Purchase_Invoice_Detail PID WHERE PID.SysDocID=APJ.SysDocID AND PID.VoucherID=APJ.VoucherID) AS JOB,\r\n                        ISNULL(APJ.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID, \r\n                        ISNULL(APJ.CurrencyRate,1) AS CurrencyRate,  \r\n                        ISNULL(ISNULL(ISNULL(creditFC,credit),0) -  \r\n                        (SELECT CASE WHEN APJ.CurrencyID IS NULL  OR APJ.CurrencyID=(SELECT CurrencyID FROM Currency WHERE IsBase='True') THEN SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0)) \r\n                        ELSE SUM(ISNULL(PaymentAmountFC,0)+ISNULL(DiscountAmountFC,0)) END FROM AP_Payment_Allocation APP\r\n                        WHERE APJ.APID=APP.APJournalID),ISNULL(ISNULL(creditFC,credit),0))  AS AmountDue,\r\n                                                       \r\n                        DATEDIFF(Day,ISNULL(APDueDate, APDate), GetDate()) AS OverdueDays\r\n                        FROM APJournal APJ   \r\n                        --RIGHT JOIN Purchase_Invoice_Detail PID ON PID.SysDocID=SysDocID AND PID.VoucherID=VoucherID\r\n                        WHERE ISNULL(credit,0)>0 AND ISNULL(IsVoid,'False')='False' AND \r\n \r\n                        (SELECT CASE WHEN (APJ.CurrencyID IS NULL OR APJ.CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')) THEN \r\n                        ISNULL(SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0)),0) ELSE  \r\n                        ISNULL(SUM(ISNULL(PaymentAmountFC,0)+ISNULL(DiscountAmountFC,0)),0) END FROM AP_Payment_Allocation PA\r\n                        WHERE APJ.APID=PA.APJournalID)<\r\n                        CASE WHEN (APJ.CurrencyID IS NULL OR APJ.CurrencyID = (SELECT CurrencyID FROM Currency WHERE IsBase='True') )\r\n                        THEN ISNULL(ISNULL(credit,0),0) ELSE ISNULL(ISNULL(creditFC,0),0) END  AND  SysDocID <> 'SYS_VAL' AND APJ.VendorID= '" + fromVendor + "' ORDER BY  APDate ";
				FillDataSet(dataSet, "OutstandingInvoices", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
