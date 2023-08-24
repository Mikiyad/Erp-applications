using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SystemDocumentData : DataSet
	{
		public const string SYSTEMDOCUMENT_TABLE = "System_Document";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string DOCNAME_FIELD = "DocName";

		public const string NEXTNUMBER_FIELD = "NextNumber";

		public const string NUMBERPREFIX_FIELD = "NumberPrefix";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public const string INACTIVE_FIELD = "Inactive";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string PRINTTEMPLATENAME_FIELD = "PrintTemplateName";

		public const string CONSIGNOUTLOCATIONID_FIELD = "ConsignOutLocationID";

		public const string ALLOWFOC_FIELD = "AllowFOC";

		public const string ISBOLMANDATORY_FIELD = "IsBOLMandatory";

		public const string ALLOWMULTITEMPLATE_FIELD = "AllowMultiTemplate";

		public const string PRICEINLCUDETAX_FIELD = "PriceIncludeTax";

		public const string ISSUPPLIERINVOICENUMBERMANDATORY_FIELD = "IsSupplierInvoiceNoMandatory";

		public const string ITEMFILTERBASEDONCUSTOMER_FIELD = "ItemFilterBasedonCustomer";

		public const string SALESACCOUNTID_FIELD = "SalesAccountID";

		public const string COGSACCOUNTID_FIELD = "COGSAccountID";

		public const string SALESTAXACCOUNTID_FIELD = "SalesTaxAccountID";

		public const string DISCOUNTGIVENACCOUNTID_FIELD = "DiscountGivenAccountID";

		public const string ENTITYID_FIELD = "EntityID";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string SYSTEMDOCENTITYLINK_TABLE = "System_Doc_Entity_Link";

		public const string REPORTNAME_FIELD = "ExternalReportName";

		public const string USERID_FIELD = "UserID";

		public const string EXTERNAL_REPORT_USER_LINK_TABLE = "External_Report_User_Link";

		public const string OPENLISTQUERY_FIELD = "OpenListQuery";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string PRINTTEMPLATE_FIELD = "PrintTemplate";

		public const string TEMPLATEDESCRIPTION_FIELD = "TemplateDescription";

		public const string TEMPLATEKEYWORD_FIELD = "TemplateKeyword";

		public const string SYSTEMDOCDETAIL_TABLE = "System_Doc_Detail";

		public const string CONSIGNINSALESACCOUNTID_FIELD = "ConsignInSalesAccountID";

		public const string CONSIGNINCOGSACCOUNTID_FIELD = "ConsignInCOGSAccountID";

		public const string CONSIGNINPAYABLEACCOUNTID_FIELD = "ConsignInPayableAccountID";

		public const string PRINTAFTERSAVE_FIELD = "PrintAfterSave";

		public const string DOPRINT_FIELD = "DoPrint";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable SystemDocumentTable => base.Tables["System_Document"];

		public DataTable SystemDocDetailTable => base.Tables["System_Doc_Detail"];

		public SystemDocumentData()
		{
			BuildDataTables();
		}

		public SystemDocumentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("System_Document");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("DocName", typeof(string)).AllowDBNull = false;
			columns.Add("SysDocType", typeof(string)).AllowDBNull = false;
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			columns.Add("PrintAfterSave", typeof(bool)).DefaultValue = false;
			columns.Add("DoPrint", typeof(bool)).DefaultValue = false;
			columns.Add("NextNumber", typeof(long));
			columns.Add("PriceIncludeTax", typeof(bool));
			columns.Add("LocationID", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("SalesAccountID", typeof(string));
			columns.Add("COGSAccountID", typeof(string));
			columns.Add("SalesTaxAccountID", typeof(string));
			columns.Add("DiscountGivenAccountID", typeof(string));
			columns.Add("ConsignOutLocationID", typeof(string));
			columns.Add("NumberPrefix", typeof(string));
			columns.Add("ConsignInSalesAccountID", typeof(string));
			columns.Add("PrintTemplateName", typeof(string));
			columns.Add("ConsignInCOGSAccountID", typeof(string));
			columns.Add("ConsignInPayableAccountID", typeof(string));
			columns.Add("OpenListQuery", typeof(string));
			columns.Add("AllowFOC", typeof(bool));
			columns.Add("AllowMultiTemplate", typeof(bool)).DefaultValue = false;
			columns.Add("IsBOLMandatory", typeof(bool)).DefaultValue = false;
			columns.Add("IsSupplierInvoiceNoMandatory", typeof(bool)).DefaultValue = false;
			columns.Add("ItemFilterBasedonCustomer", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("System_Doc_Entity_Link");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string)).AllowDBNull = false;
			columns.Add("EntityID", typeof(string)).AllowDBNull = false;
			columns.Add("EntityType", typeof(byte)).AllowDBNull = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("External_Report_User_Link");
			columns = dataTable.Columns;
			columns.Add("ExternalReportName", typeof(string)).AllowDBNull = false;
			columns.Add("UserID", typeof(string)).AllowDBNull = false;
			columns.Add("EntityType", typeof(byte)).AllowDBNull = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("System_Doc_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string)).AllowDBNull = false;
			columns.Add("SysDocType", typeof(int)).AllowDBNull = false;
			columns.Add("RowIndex", typeof(long));
			columns.Add("PrintTemplate", typeof(string));
			columns.Add("TemplateDescription", typeof(string));
			columns.Add("TemplateKeyword", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
