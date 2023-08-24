using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobInventoryIssueData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE_FIELD2 = "Reference2";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REQUESTEDBY_FIELD = "RequestedBy";

		public const string SOURCESYSDOCTYPE_FIELD = "SourceSysDocType";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string JOBINVENTORYISSUE_TABLE = "Job_Inventory_Issue";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string COST_FIELD = "Cost";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITID_FIELD = "UnitID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string FACTOR_FIELD = "Factor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string JOBINVENTORYISSUEDETAIL_TABLE = "Job_Inventory_Issue_Detail";

		public DataTable JobInventoryIssueTable => base.Tables["Job_Inventory_Issue"];

		public DataTable JobInventoryIssueDetailTable => base.Tables["Job_Inventory_Issue_Detail"];

		public JobInventoryIssueData()
		{
			BuildDataTables();
		}

		public JobInventoryIssueData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Inventory_Issue");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("AccountID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RequestedBy", typeof(string));
			columns.Add("SourceSysDocType", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Inventory_Issue_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Cost", typeof(decimal));
			columns.Add("Quantity", typeof(float));
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("Factor", typeof(float));
			columns.Add("FactorType", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			base.Tables.Add(dataTable);
		}
	}
}
