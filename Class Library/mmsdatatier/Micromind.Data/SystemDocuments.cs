using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Data.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Micromind.Data
{
	public sealed class SystemDocuments : StoreObject
	{
		private const string SYSTEMDOCUMENT_TABLE = "System_Document";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string DOCNAME_PARM = "@DocName";

		private const string SYSDOCTYPE_PARM = "@SysDocType";

		private const string INACTIVE_PARM = "@Inactive";

		private const string NEXTNUMBER_PARM = "@NextNumber";

		private const string NUMBERPREFIX_PARM = "@NumberPrefix";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string PRINTTEMPLATENAME_PARM = "@PrintTemplateName";

		private const string CONSIGNOUTLOCATIONID_PARM = "@ConsignOutLocationID";

		private const string PRINTAFTERSAVE_PARM = "@PrintAfterSave";

		private const string PRICEINLCUDETAX_PARM = "@PriceIncludeTax";

		private const string DOPRINT_PARM = "@DoPrint";

		private const string ALLOWFOC_PARM = "@AllowFOC";

		private const string ISBOLMANDATORY_PARM = "@IsBOLMandatory";

		private const string ALLOWMULTITEMPLATE_PARM = "@AllowMultiTemplate";

		private const string ISSUPPLIERINVOICENUMBERMANDATORY_PARM = "@IsSupplierInvoiceNoMandatory";

		private const string ITEMFILTERBASEDONCUSTOMER_PARM = "@ItemFilterBasedonCustomer";

		private const string SALESACCOUNTID_PARM = "@SalesAccountID";

		private const string COGSACCOUNTID_PARM = "@COGSAccountID";

		private const string SALESTAXACCOUNTID_PARM = "@SalesTaxAccountID";

		private const string DISCOUNTGIVENACCOUNTID_PARM = "@DiscountGivenAccountID";

		private const string OPENLISTQUERY_PARM = "@OPenListQuery";

		private const string ENTITYID_PARM = "@ExternalReportID";

		private const string ENTITYTYPE_PARM = "@EntityType";

		private const string REPORTNAME_PARM = "@EntityID";

		private const string USERID_PARM = "@UserID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string PRINTTEMPLATE_PARM = "@PrintTemplate";

		private const string TEMPLATEDESCRIPTION_PARM = "@TemplateDescription";

		private const string TEMPLATEKEYWORD_PARM = "@TemplateKeyword";

		private const string SYSTEMDOCDETAIL_TABLE = "System_Doc_Detail";

		private const string CONSIGNINSALESACCOUNTID_PARM = "@ConsignInSalesAccountID";

		private const string CONSIGNINCOGSACCOUNTID_PARM = "@ConsignInCOGSAccountID";

		private const string CONSIGNINPAYABLEACCOUNTID_PARM = "@ConsignInPayableAccountID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public SystemDocuments(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("System_Document", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("DocName", "@DocName"), new FieldValue("SysDocType", "@SysDocType"), new FieldValue("Inactive", "@Inactive"), new FieldValue("NextNumber", "@NextNumber"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("SalesAccountID", "@SalesAccountID"), new FieldValue("COGSAccountID", "@COGSAccountID"), new FieldValue("SalesTaxAccountID", "@SalesTaxAccountID"), new FieldValue("DiscountGivenAccountID", "@DiscountGivenAccountID"), new FieldValue("PrintTemplateName", "@PrintTemplateName"), new FieldValue("ConsignOutLocationID", "@ConsignOutLocationID"), new FieldValue("PrintAfterSave", "@PrintAfterSave"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("DoPrint", "@DoPrint"), new FieldValue("AllowFOC", "@AllowFOC"), new FieldValue("AllowMultiTemplate", "@AllowMultiTemplate"), new FieldValue("IsBOLMandatory", "@IsBOLMandatory"), new FieldValue("ConsignInSalesAccountID", "@ConsignInSalesAccountID"), new FieldValue("ConsignInCOGSAccountID", "@ConsignInCOGSAccountID"), new FieldValue("ConsignInPayableAccountID", "@ConsignInPayableAccountID"), new FieldValue("OpenListQuery", "@OPenListQuery"), new FieldValue("NumberPrefix", "@NumberPrefix"), new FieldValue("IsSupplierInvoiceNoMandatory", "@IsSupplierInvoiceNoMandatory"), new FieldValue("ItemFilterBasedonCustomer", "@ItemFilterBasedonCustomer"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("System_Document", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@DocName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@PrintAfterSave", SqlDbType.Bit);
			parameters.Add("@DoPrint", SqlDbType.Bit);
			parameters.Add("@SysDocType", SqlDbType.Int);
			parameters.Add("@NextNumber", SqlDbType.NVarChar);
			parameters.Add("@NumberPrefix", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@SalesAccountID", SqlDbType.NVarChar);
			parameters.Add("@COGSAccountID", SqlDbType.NVarChar);
			parameters.Add("@SalesTaxAccountID", SqlDbType.NVarChar);
			parameters.Add("@DiscountGivenAccountID", SqlDbType.NVarChar);
			parameters.Add("@PrintTemplateName", SqlDbType.NVarChar);
			parameters.Add("@ConsignOutLocationID", SqlDbType.NVarChar);
			parameters.Add("@ConsignInSalesAccountID", SqlDbType.NVarChar);
			parameters.Add("@ConsignInCOGSAccountID", SqlDbType.NVarChar);
			parameters.Add("@ConsignInPayableAccountID", SqlDbType.NVarChar);
			parameters.Add("@OPenListQuery", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@AllowFOC", SqlDbType.Bit);
			parameters.Add("@AllowMultiTemplate", SqlDbType.Bit);
			parameters.Add("@IsBOLMandatory", SqlDbType.Bit);
			parameters.Add("@IsSupplierInvoiceNoMandatory", SqlDbType.Bit);
			parameters.Add("@ItemFilterBasedonCustomer", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@DocName"].SourceColumn = "DocName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@PrintAfterSave"].SourceColumn = "PrintAfterSave";
			parameters["@DoPrint"].SourceColumn = "DoPrint";
			parameters["@SysDocType"].SourceColumn = "SysDocType";
			parameters["@NextNumber"].SourceColumn = "NextNumber";
			parameters["@NumberPrefix"].SourceColumn = "NumberPrefix";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@SalesAccountID"].SourceColumn = "SalesAccountID";
			parameters["@COGSAccountID"].SourceColumn = "COGSAccountID";
			parameters["@SalesTaxAccountID"].SourceColumn = "SalesTaxAccountID";
			parameters["@DiscountGivenAccountID"].SourceColumn = "DiscountGivenAccountID";
			parameters["@PrintTemplateName"].SourceColumn = "PrintTemplateName";
			parameters["@ConsignOutLocationID"].SourceColumn = "ConsignOutLocationID";
			parameters["@ConsignInSalesAccountID"].SourceColumn = "ConsignInSalesAccountID";
			parameters["@ConsignInCOGSAccountID"].SourceColumn = "ConsignInCOGSAccountID";
			parameters["@ConsignInPayableAccountID"].SourceColumn = "ConsignInPayableAccountID";
			parameters["@OPenListQuery"].SourceColumn = "OpenListQuery";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@AllowFOC"].SourceColumn = "AllowFOC";
			parameters["@AllowMultiTemplate"].SourceColumn = "AllowMultiTemplate";
			parameters["@IsBOLMandatory"].SourceColumn = "IsBOLMandatory";
			parameters["@IsSupplierInvoiceNoMandatory"].SourceColumn = "IsSupplierInvoiceNoMandatory";
			parameters["@ItemFilterBasedonCustomer"].SourceColumn = "ItemFilterBasedonCustomer";
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

		private string GetInsertUpdateEntityLinkText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("System_Doc_Entity_Link", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("EntityID", "@ExternalReportID"), new FieldValue("EntityType", "@EntityType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateExternalReportLinkText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("External_Report_User_Link", new FieldValue("ExternalReportName", "@EntityID"), new FieldValue("UserID", "@UserID"), new FieldValue("EntityType", "@EntityType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateSysDocDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("System_Doc_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("SysDocType", "@SysDocType"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("PrintTemplate", "@PrintTemplate"), new FieldValue("TemplateKeyword", "@TemplateKeyword"), new FieldValue("TemplateDescription", "@TemplateDescription"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEntityLinkCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEntityLinkText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEntityLinkText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@ExternalReportID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@ExternalReportID"].SourceColumn = "EntityID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateExternalReportLinkCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateExternalReportLinkText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateExternalReportLinkText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@EntityID", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters["@EntityID"].SourceColumn = "ExternalReportName";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateSysDocDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSysDocDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSysDocDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@SysDocType", SqlDbType.Int);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@PrintTemplate", SqlDbType.NVarChar);
			parameters.Add("@TemplateDescription", SqlDbType.NVarChar);
			parameters.Add("@TemplateKeyword", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@SysDocType"].SourceColumn = "SysDocType";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@PrintTemplate"].SourceColumn = "PrintTemplate";
			parameters["@TemplateDescription"].SourceColumn = "TemplateDescription";
			parameters["@TemplateKeyword"].SourceColumn = "TemplateKeyword";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertEntityLinks(SystemDocumentData systemDocumentData, string sysDocID, SysDocEntityTypes entityType)
		{
			bool flag = true;
			SqlCommand insertUpdateEntityLinkCommand = GetInsertUpdateEntityLinkCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateEntityLinkCommand.Transaction = base.DBConfig.StartNewTransaction());
				string exp = "DELETE FROM System_Doc_Entity_Link WHERE SysDocID = '" + sysDocID + "' AND EntityTYpe = " + (byte)entityType;
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag = Insert(systemDocumentData, "System_Doc_Entity_Link", insertUpdateEntityLinkCommand);
				AddActivityLog("System Document", sysDocID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("System_Document", "SysDocID", sysDocID, sqlTransaction, isInsert: false);
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

		public bool InsertExternalReportLinks(SystemDocumentData systemDocumentData, string ReportID, SysDocEntityTypes entityType)
		{
			bool flag = true;
			SqlCommand insertUpdateExternalReportLinkCommand = GetInsertUpdateExternalReportLinkCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateExternalReportLinkCommand.Transaction = base.DBConfig.StartNewTransaction());
				string exp = "DELETE FROM External_Report_User_Link WHERE ExternalReportName = '" + ReportID + "' AND EntityType = " + (byte)entityType;
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag = Insert(systemDocumentData, "External_Report_User_Link", insertUpdateExternalReportLinkCommand);
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

		public bool InsertSystemDocument(SystemDocumentData systemDocumentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(systemDocumentData, "System_Document", insertUpdateCommand);
				string text = systemDocumentData.SystemDocumentTable.Rows[0]["SysDocID"].ToString();
				AddActivityLog("System Document", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("System_Document", "SysDocID", text, sqlTransaction, isInsert: true);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool UpdateSystemDocument(SystemDocumentData systemDocumentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(systemDocumentData, "System_Document", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = systemDocumentData.SystemDocumentTable.Rows[0]["SysDocID"];
				UpdateTableRowByID("System_Document", "SysDocID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = systemDocumentData.SystemDocumentTable.Rows[0]["DocName"].ToString();
				AddActivityLog("System Document", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("System_Document", "SysDocID", obj, sqlTransaction, isInsert: false);
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

		public SystemDocumentData GetSystemDocument()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("System_Document");
			SystemDocumentData systemDocumentData = new SystemDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(systemDocumentData, "System_Document", sqlBuilder);
			return systemDocumentData;
		}

		public DataSet GetControls()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * FROM CONTROL_TYPE";
			FillDataSet(dataSet, "System_Document", textCommand);
			return dataSet;
		}

		public DataSet GetItemTransactionSystemDoc()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID, DocName, SysDocType\r\n                           FROM System_Document where SysDocType IN(24,32,18,19,26)";
			FillDataSet(dataSet, "System_Document", textCommand);
			return dataSet;
		}

		public DataSet GetItemBarCodeSystemDoc()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID, DocName, SysDocType\r\n                           FROM System_Document where SysDocType IN(24,32,18,19,26,31,38,50,39,33)";
			FillDataSet(dataSet, "System_Document", textCommand);
			return dataSet;
		}

		public bool DeleteSystemDocument(string systemDocumentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM System_Document WHERE SysDocID = '" + systemDocumentID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("System Document", systemDocumentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public SystemDocumentData GetSystemDocumentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "SysDocID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "System_Document";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			SystemDocumentData systemDocumentData = new SystemDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(systemDocumentData, "System_Document", sqlBuilder);
			return systemDocumentData;
		}

		public DataSet GetSystemDocumentByFields(params string[] columns)
		{
			return GetSystemDocumentByFields(null, isInactive: true, columns);
		}

		public DataSet GetSystemDocumentByFields(string[] systemDocumentID, params string[] columns)
		{
			return GetSystemDocumentByFields(systemDocumentID, isInactive: true, columns);
		}

		public DataSet GetSystemDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("System_Document");
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
				commandHelper.FieldName = "SysDocID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "System_Document";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "System_Document", sqlBuilder);
			return dataSet;
		}

		public DataSet GetSystemDocumentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID,DocName [Name],SysDocType,LocationID\r\n                           FROM System_Document ";
			FillDataSet(dataSet, "System_Document", textCommand);
			return dataSet;
		}

		public DataSet GetSystemDocumentComboList(string DefaultLocation)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID [Code],DocName [Name],SysDocType,LocationID, ISNULL(PrintAfterSave,'False') AS PrintAfterSave, ISNULL(DoPrint,'False') AS DoPrint, PrintTemplateName,ISNULL(PriceIncludeTax,'False') AS PriceIncludeTax,DivisionID\r\n                           FROM System_Document ORDER BY SysDocID,DocName";
			FillDataSet(dataSet, "System_Document", textCommand);
			return dataSet;
		}

		public DataSet GetTransactionComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID [Code],DocName [Name],SysDocType,LocationID, ISNULL(PrintAfterSave,'False') AS PrintAfterSave, ISNULL(DoPrint,'False') AS DoPrint, PrintTemplateName,ISNULL(PriceIncludeTax,'False') AS PriceIncludeTax\r\n                           FROM System_Document where SysDocType IN (3,24,53,51,52,32,95,50,33,31,25,26) ORDER BY SysDocID,DocName";
			FillDataSet(dataSet, "System_Document", textCommand);
			return dataSet;
		}

		public DataSet GetEntityLinks(string sysDocID, SysDocEntityTypes entityType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * FROM System_Doc_Entity_Link WHERE SysDocID = '" + sysDocID + "' AND EntityType = " + (int)entityType;
			FillDataSet(dataSet, "System_Doc_Entity_Link", textCommand);
			return dataSet;
		}

		public DataSet GetServiceSupplierLinks(SysDocEntityTypes entityType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Distinct EntityID FROM System_Doc_Entity_Link WHERE EntityID='S-SU' AND EntityType = " + (int)entityType;
			FillDataSet(dataSet, "System_Doc_Entity_Link", textCommand);
			return dataSet;
		}

		public DataSet GetExternalReportUserLinks(string reportID, SysDocEntityTypes entityType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * FROM External_Report_User_Link WHERE ExternalReportName = '" + reportID + "' AND EntityType = " + (int)entityType;
			FillDataSet(dataSet, "External_Report_User_Link", textCommand);
			return dataSet;
		}

		public SysDocTypes GetSystemDocumentType(string docID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT SysDocType  \r\n                           FROM System_Document WHERE SysDocID='" + docID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null)
			{
				return (SysDocTypes)int.Parse(obj.ToString());
			}
			return SysDocTypes.None;
		}

		public SysDocTypes GetBarCodeSystemDocumentType(string docID)
		{
			string exp = "SELECT SysDocType  \r\n                           FROM System_Document WHERE SysDocID='" + docID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return (SysDocTypes)int.Parse(obj.ToString());
			}
			return SysDocTypes.None;
		}

		public string GetDocumentNumberPrefix(string sysDocID)
		{
			string exp = "SELECT NumberPrefix FROM System_Document WHERE SysDocID='" + sysDocID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return obj.ToString();
			}
			return "";
		}

		public string GetNextDocumentNumber(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			long result = 1L;
			string text2 = "";
			string textCommand = "SELECT NumberPrefix , NextNumber,LastNumber FROM System_Document WHERE SysDocID='" + sysDocID + "'";
			FillDataSet(dataSet, "System_Document", textCommand);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				text = dataSet.Tables[0].Rows[0]["NumberPrefix"].ToString();
				text2 = dataSet.Tables[0].Rows[0]["LastNumber"].ToString();
				if (dataSet.Tables[0].Rows[0]["NextNumber"] != DBNull.Value)
				{
					long.TryParse(dataSet.Tables[0].Rows[0]["NextNumber"].ToString(), out result);
				}
			}
			if (text2 != "")
			{
				int num = text2.Length - text.Length;
				string text3 = "";
				for (int i = 0; i < num; i++)
				{
					text3 += "0";
				}
				return text + result.ToString(text3);
			}
			return text + result.ToString("000000");
		}

		public string GetNextDocumentNumber(string tableName, string fieldName)
		{
			DataSet dataSet = new DataSet();
			string text = string.Empty;
			int num = 1;
			string empty = string.Empty;
			string text2 = "";
			int num2 = -1;
			empty = (("SELECT MAX(ISNULL(" + fieldName + ", 0)) AS ID FROM " + tableName) ?? "");
			FillDataSet(dataSet, "VOUCHER_TABLE", empty);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				text = dataSet.Tables[0].Rows[0]["ID"].ToString();
				text2 = CommonLib.GetNumberPrefix(text);
				if (text2.Length == text.Length)
				{
					return text + "000001";
				}
				num2 = int.Parse(text.Substring(text2.Length, text.Length - text2.Length));
			}
			if (num2 >= 0)
			{
				int num3 = text.Length - text2.Length;
				string text3 = "";
				for (int i = 0; i < num3; i++)
				{
					text3 += "0";
				}
				num = ++num2;
				return text2 + num.ToString(text3);
			}
			return text2 + num.ToString("000000");
		}

		public string GetNextNumber(string tableName, string fieldName)
		{
			return GetNextDocumentNumber(tableName, fieldName);
		}

		public string GetNextEmployeeNumber()
		{
			DataSet dataSet = new DataSet();
			_ = string.Empty;
			int result = 0;
			string result2 = "1".PadLeft(8, '0');
			string empty = string.Empty;
			empty = "SELECT  MAX(EmployeeID) AS ID FROM Employee";
			FillDataSet(dataSet, "Employee", empty);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0 && int.TryParse(dataSet.Tables[0].Rows[0]["ID"].ToString(), out result))
			{
				result2 = (result + 1).ToString().PadLeft(4, '0');
			}
			return result2;
		}

		public string GetNextSponserEmployeeNumber(string SponsorID)
		{
			DataSet dataSet = new DataSet();
			string empty = string.Empty;
			int result = 0;
			string result2 = "1".PadLeft(8, '0');
			string empty2 = string.Empty;
			empty2 = "SELECT  MAX(EmployeeID) AS ID FROM Employee where SponsorID='" + SponsorID + "'";
			FillDataSet(dataSet, "Employee", empty2);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				empty = dataSet.Tables[0].Rows[0]["ID"].ToString();
				string[] array = empty.Split('-');
				string text = "";
				int num = 0;
				if (array.Length > 1)
				{
					text = array[0];
					if (int.TryParse(int.Parse(array[1].ToString().PadLeft(4, '0')).ToString(), out result))
					{
						result2 = text + "-" + (result + 1).ToString("00000");
					}
				}
				else if (array.Length <= 1 && int.TryParse(empty, out result))
				{
					result2 = (result + 1).ToString().PadLeft(4, '0');
				}
			}
			return result2;
		}

		public bool UpdateNextToDeletedDocumentNumber(string sysDocID, string deletedVoucherID, string tableName, string fieldName, SqlTransaction sqlTransaction)
		{
			int result = -1;
			int result2 = -1;
			string empty = string.Empty;
			empty = CommonLib.GetNumberPrefix(deletedVoucherID);
			int.TryParse(deletedVoucherID.Substring(empty.Length, deletedVoucherID.Length - empty.Length), out result);
			string exp = "SELECT MAX (" + fieldName + ") FROM " + tableName + " WHERE SysDocID='" + sysDocID + "' AND " + fieldName + " LIKE '" + empty + "%' AND " + fieldName + " <='" + deletedVoucherID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				empty = CommonLib.GetNumberPrefix(obj.ToString());
				int.TryParse(obj.ToString().Substring(empty.Length, obj.ToString().Length - empty.Length), out result2);
			}
			string currentNumber = (obj as string) ?? string.Empty;
			if (result - 1 == result2)
			{
				return UpdateNextDocumentNumber(sysDocID, currentNumber, tableName, fieldName, sqlTransaction);
			}
			return true;
		}

		public bool UpdateNextDocumentNumber(string sysDocID, string currentNumber, string tableName, string fieldName, SqlTransaction sqlTransaction)
		{
			if (sysDocID.ToLower().StartsWith("sys_"))
			{
				return true;
			}
			int result = 10;
			string text = "";
			text = CommonLib.GetNumberPrefix(currentNumber);
			int.TryParse(currentNumber.Substring(text.Length, currentNumber.Length - text.Length), out result);
			int num = currentNumber.Length - text.Length;
			string text2 = "";
			for (int i = 0; i < num; i++)
			{
				text2 += "0";
			}
			string documentNumber;
			do
			{
				result++;
				documentNumber = text + result.ToString(text2);
			}
			while (ExistDocumentNumber(tableName, fieldName, sysDocID, documentNumber, sqlTransaction));
			string exp = "UPDATE System_Document SET NextNumber=" + result + ",LastNumber='" + currentNumber + "' WHERE SysDocID='" + sysDocID + "'";
			if (ExecuteNonQuery(exp, sqlTransaction) > 0)
			{
				return true;
			}
			return false;
		}

		public bool ExistDocumentNumber(string tableName, string fieldName, string sysDocID, string documentNumber, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT TOP 1 " + fieldName + " FROM " + tableName + " WHERE SysDocID='" + sysDocID + "' AND " + fieldName + " ='" + documentNumber + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "")
			{
				return true;
			}
			return false;
		}

		public bool ExistDocumentNumber(string tableName, string fieldName, string documentNumber, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT TOP 1 " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " ='" + documentNumber + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "")
			{
				return true;
			}
			return false;
		}

		public string GetNextCardNumber(string tableName, string idFieldName)
		{
			return GetNextCardNumber(tableName, idFieldName, "", "");
		}

		public string GetNextCardNumber(string tableName, string idFieldName, string filterColumnName, string filterColumnValue)
		{
			string text = "SELECT TOP 1 " + idFieldName + " AS Code FROM " + tableName;
			if (filterColumnName != "")
			{
				text = text + " WHERE " + filterColumnName + " = '" + filterColumnValue + "' ";
			}
			text += " ORDER BY DateCreated DESC";
			string text2 = "";
			object obj = ExecuteScalar(text);
			if (!obj.IsDBNullOrEmpty())
			{
				text2 = obj.ToString();
			}
			string numberPrefix = CommonLib.GetNumberPrefix(text2);
			if (numberPrefix.Length == text2.Length)
			{
				return text2 + "00001";
			}
			long result = 0L;
			long.TryParse(text2.Substring(numberPrefix.Length, text2.Length - numberPrefix.Length), out result);
			long num = 0L;
			if (result >= 0)
			{
				int num2 = text2.Length - numberPrefix.Length;
				string text3 = "";
				for (int i = 0; i < num2; i++)
				{
					text3 += "0";
				}
				num = ++result;
				return numberPrefix + num.ToString(text3);
			}
			return numberPrefix + num.ToString("000000");
		}

		public bool HasuserAccess(string sysDocID, string LocationID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT LocationID  FROM System_Document WHERE SysDocID='" + sysDocID + "' AND LocationID='" + LocationID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "")
			{
				return true;
			}
			return false;
		}

		public string GetTransUserID(string tableName, string fieldName, string sysDocID, string documentNumber, SqlTransaction sqlTransaction)
		{
			string result = "";
			string exp = "SELECT CreatedBy " + fieldName + " FROM " + tableName + " WHERE SysDocID='" + sysDocID + "' AND " + fieldName + " ='" + documentNumber + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "")
			{
				return obj.ToString();
			}
			return result;
		}

		public DataSet CheckStimeStampStatus(string tableName, string RefsysDocID, string RefdocumentNumber)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ISNULL(DateUpdated,DateCreated) as updatetime,GETDATE() AS TimeStamp FROM " + tableName + " WHERE  SysDocID='" + RefsysDocID + "' AND  VoucherID='" + RefdocumentNumber + "'";
			FillDataSet(dataSet, "timedetails", textCommand);
			return dataSet;
		}

		public string GetCardUserID(string tableName, string fieldName, object val)
		{
			string text = "";
			val = AddSingleQuote(val.ToString());
			string exp = "SELECT CreatedBy FROM " + tableName + " WHERE " + fieldName + "  = '" + val.ToString() + "'";
			try
			{
				return ExecuteScalar(exp).ToString();
			}
			catch
			{
				throw;
			}
		}

		public bool InsertDocAttachementDetail(DataSet systemDocumentData, string sysDocID, SysDocTypes entityType)
		{
			bool flag = true;
			SqlCommand insertUpdateSysDocDetailCommand = GetInsertUpdateSysDocDetailCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateSysDocDetailCommand.Transaction = base.DBConfig.StartNewTransaction());
				string exp = "DELETE FROM System_Doc_Detail WHERE SysDocID = '" + sysDocID + "' AND SysDocType = " + (int)entityType;
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag = Insert(systemDocumentData, "System_Doc_Detail", insertUpdateSysDocDetailCommand);
				AddActivityLog("System Document", sysDocID, ActivityTypes.Update, sqlTransaction);
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

		public DataSet GetDocAttachements(string sysDocID, SysDocTypes entityType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * FROM System_Doc_Detail WHERE SysDocID = '" + sysDocID + "' AND SysDocType = " + (int)entityType;
			FillDataSet(dataSet, "System_Doc_Detail", textCommand);
			return dataSet;
		}

		public bool InsertOpenListQuery(DataSet systemDocumentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Update(systemDocumentData, "System_Document", insertUpdateCommand);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public DataSet GetOpenQueryList(string ID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "";
			if (!string.IsNullOrEmpty(ID))
			{
				textCommand = "SELECT  * FROM System_Document where SysDocID='" + ID + "'  AND ISNULL(OpenListQuery, '') != '' ";
			}
			FillDataSet(dataSet, "System_Document", textCommand);
			return dataSet;
		}

		public string GetSysDocList(DataTable dt, string locationID)
		{
			new DataSet();
			string text = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (dt.Columns.Contains("Doc ID"))
				{
					text = text + "'" + dt.Rows[i]["Doc ID"] + "'";
					if (i < dt.Rows.Count - 1)
					{
						text += ",";
					}
					continue;
				}
				if (dt.Columns.Contains("SysDocID"))
				{
					text = text + "'" + dt.Rows[i]["SysDocID"] + "'";
					if (i < dt.Rows.Count - 1)
					{
						text += ",";
					}
					continue;
				}
				return null;
			}
			SqlTransaction sqlTransaction = null;
			string text2 = "";
			if (!string.IsNullOrEmpty(text))
			{
				if (!string.IsNullOrEmpty(locationID))
				{
					text2 = "select top 1 SysDocID from System_Document where LocationID='" + locationID + "' AND SysDocID IN (" + text + ") AND OpenListQuery!='NULL'";
					sqlTransaction = base.DBConfig.StartNewTransaction();
					object obj = ExecuteScalar(text2, sqlTransaction);
					base.DBConfig.EndTransaction(result: true);
					return obj?.ToString();
				}
				return null;
			}
			return null;
		}

		public DataSet GetSysDocList(string tablename)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = "select Distinct SysDocID from " + tablename + " ";
			FillDataSet(dataSet, "SysDocList", text);
			return dataSet;
		}

		public DataSet GetSysDocIDList(int docType)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = "select Distinct SysDocID from  System_Document where SysDocType =" + docType;
			FillDataSet(dataSet, "SysDocList", text);
			return dataSet;
		}

		public DataSet ExecuteOpenListQuery(string query, DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			CommonLib.ToSqlDateTimeString(from);
			CommonLib.ToSqlDateTimeString(to);
			if (!SQLHelper.ValidateQuerySecurity(query))
			{
				throw new CompanyException("report query does not allow one or more keywords used in this query.");
			}
			SqlCommand sqlCommand = new SqlCommand(query);
			FillDataSet(dataSet, "System_Document", sqlCommand);
			return dataSet;
		}

		public DataSet ExecuteOpenListQuery(string query, bool showVoid, bool isBuiltin = false)
		{
			DataSet dataSet = new DataSet();
			if (!SQLHelper.ValidateQuerySecurity(query))
			{
				throw new CompanyException("report query does not allow one or more keywords used in this query.");
			}
			if (isBuiltin)
			{
				query = ReplaceVoidParameterOpenList(query, showVoid);
			}
			else if (!showVoid)
			{
				query += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(query);
			FillDataSet(dataSet, "System_Document", sqlCommand);
			return dataSet;
		}

		public string ReplaceSystemParametersOpenList(string query, DateTime fromDate, DateTime toDate, string SysDocID, string ComboSysDocID)
		{
			string userID = base.DBConfig.UserID;
			string replacement = "";
			string replacement2 = "";
			string replacement3 = "";
			string replacement4 = CommonLib.ToSqlDateTimeString(fromDate);
			string replacement5 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ISNULL(DefaultSalespersonID,'') AS CurrentSalesPerson,ISNULL(DefaultInventoryLocationID,'') AS CurrentInventoryLocation,\r\n                                ISNULL(DefaultTransactionLocationID,'') AS CurrentTransactionLocation FROM Users  WHERE UserID = '" + userID + "'";
			FillDataSet(dataSet, "Users", textCommand);
			DataRow dataRow = dataSet.Tables["Users"].Rows[0];
			if (dataSet != null && dataSet.Tables["Users"].Rows.Count > 0)
			{
				replacement = dataRow["CurrentSalesPerson"].ToString();
				replacement2 = dataRow["CurrentInventoryLocation"].ToString();
				replacement3 = dataRow["CurrentTransactionLocation"].ToString();
			}
			query = Regex.Replace(query, "@CurrentSalesPerson", replacement, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@CurrentInventoryLocation", replacement2, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@CurrentTransactionLocation", replacement3, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@CurrentUser", userID, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@FromDate", replacement4, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@EndDate", replacement5, RegexOptions.IgnoreCase);
			if (query.Contains("@SysDocID"))
			{
				query = Regex.Replace(query, "@SysDocID", SysDocID, RegexOptions.IgnoreCase);
			}
			return query;
		}

		public string ReplaceVoidParameterOpenList(string query, bool isShowVoid)
		{
			if (isShowVoid)
			{
				int num = query.ToLower().LastIndexOf("isnull");
				int num2 = query.ToLower().LastIndexOf("'@showvoid'");
				if (num == -1 || num2 == -1)
				{
					return query;
				}
				string str = query.Substring(num, num2 - num);
				query = query.Insert(num, str + "'False' Or ");
				query = Regex.Replace(query, "@shoWVoid", isShowVoid.ToString(), RegexOptions.IgnoreCase);
			}
			else
			{
				query = Regex.Replace(query, "@shoWVoid", isShowVoid.ToString(), RegexOptions.IgnoreCase);
			}
			return query;
		}

		public string GetNextCatgryWiseDocNumber(string tableName, string idFieldName, string filterColumnName, string filterColumnValue)
		{
			DataSet dataSet = new DataSet();
			string empty = string.Empty;
			string empty2 = string.Empty;
			string text = "";
			string text2 = "";
			string text3 = "";
			empty2 = ((" SELECT MAX(ISNULL(" + idFieldName + ", 0)) AS Code FROM " + tableName) ?? "");
			if (filterColumnName != "")
			{
				empty2 = empty2 + " WHERE " + idFieldName + " Like'" + filterColumnValue + "%' AND " + filterColumnName + "='" + filterColumnValue + "' ";
			}
			empty2 += " Group By DateCreated ORDER BY DateCreated DESC";
			FillDataSet(dataSet, "VOUCHER_TABLE", empty2);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				empty = dataSet.Tables[0].Rows[0]["Code"].ToString();
				int indexofNumber = getIndexofNumber(empty);
				empty = empty.Replace(" ", "");
				if (empty.Contains("-"))
				{
					text2 = empty.Substring(empty.LastIndexOf('-') + 1);
					text3 = empty.Substring(0, empty.LastIndexOf('-') + 1);
				}
				else
				{
					text2 = empty.Substring(indexofNumber, empty.Length - indexofNumber);
					text3 = empty.Substring(0, indexofNumber);
				}
				int num = Convert.ToInt32(text2) + 1;
				int num2 = empty.Length - text3.Length - num.ToString().Length;
				string text4 = "";
				for (int i = 0; i < num2; i++)
				{
					text4 += "0";
				}
				return text3 + text4 + num.ToString();
			}
			return filterColumnValue + "0";
		}

		private int getIndexofNumber(string cell)
		{
			int num = -1;
			foreach (char c in cell)
			{
				num++;
				if (char.IsDigit(c))
				{
					return num;
				}
			}
			return num;
		}

		public bool UpdateToLastDocumentNumber(string sysDocID, string currentNumber, string tableName, string fieldName, SqlTransaction sqlTransaction)
		{
			if (sysDocID.ToLower().StartsWith("sys_"))
			{
				return true;
			}
			int result = 10;
			string text = "";
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT MAX(VoucherID) as currentNumber FROM " + tableName + " WHERE SysDocID='" + sysDocID + "'";
			FillDataSet(dataSet, "System_Document", textCommand);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				currentNumber = dataSet.Tables[0].Rows[0]["currentNumber"].ToString();
			}
			text = CommonLib.GetNumberPrefix(currentNumber);
			int.TryParse(currentNumber.Substring(text.Length, currentNumber.Length - text.Length), out result);
			int num = currentNumber.Length - text.Length;
			string text2 = "";
			for (int i = 0; i < num; i++)
			{
				text2 += "0";
			}
			string text3 = "";
			do
			{
				result++;
				text3 = text + result.ToString(text2);
			}
			while (ExistDocumentNumber(tableName, fieldName, sysDocID, text3, sqlTransaction));
			textCommand = "UPDATE System_Document SET NextNumber='" + result + "' WHERE SysDocID='" + sysDocID + "'";
			if (ExecuteNonQuery(textCommand, sqlTransaction) > 0)
			{
				return true;
			}
			return false;
		}

		public bool UpdateToLastDocumentNumberWithTemporary(string sysDocID, string currentNumber, string tableName, string fieldName, SqlTransaction sqlTransaction)
		{
			if (sysDocID.ToLower().StartsWith("sys_"))
			{
				return true;
			}
			int result = 10;
			string text = "";
			DataSet dataSet = new DataSet();
			string textCommand = " SELECT MAX(T.VoucherID) as currentNumber FROM(SELECT VoucherID  FROM " + tableName + " WHERE SysDocID='" + sysDocID + "' UNION SELECT VoucherID FROM Temporary_Transaction WHERE SysDocID = '" + sysDocID + "')T";
			FillDataSet(dataSet, "System_Document", textCommand);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				currentNumber = dataSet.Tables[0].Rows[0]["currentNumber"].ToString();
			}
			text = CommonLib.GetNumberPrefix(currentNumber);
			int.TryParse(currentNumber.Substring(text.Length, currentNumber.Length - text.Length), out result);
			int num = currentNumber.Length - text.Length;
			string text2 = "";
			for (int i = 0; i < num; i++)
			{
				text2 += "0";
			}
			string text3 = "";
			do
			{
				result++;
				text3 = text + result.ToString(text2);
			}
			while (ExistDocumentNumber(tableName, fieldName, sysDocID, text3, sqlTransaction));
			textCommand = "UPDATE System_Document SET NextNumber='" + result + "' WHERE SysDocID='" + sysDocID + "'";
			if (ExecuteNonQuery(textCommand, sqlTransaction) > 0)
			{
				return true;
			}
			return false;
		}

		public bool DeleteEntityLinks(string sysdocID, SysDocEntityTypes entityType)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM System_Doc_Entity_Link WHERE SysDocID = '" + sysdocID + "' AND EntityTYpe = " + (byte)entityType;
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("System Document", sysdocID, ActivityTypes.Update, null);
				UpdateTableRowInsertUpdateInfo("System_Document", "SysDocID", sysdocID, null, isInsert: false);
				return flag;
			}
			catch
			{
				throw;
			}
		}
	}
}
