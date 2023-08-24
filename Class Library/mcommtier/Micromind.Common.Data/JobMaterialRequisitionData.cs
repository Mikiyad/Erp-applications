using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobMaterialRequisitionData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE_FIELD2 = "Reference2";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REQUESTEDBY_FIELD = "RequestedBy";

		public const string REQONDATE_FIELD = "ReqonDate";

		public const string JOBMATERIALREQUISITION_TABLE = "Job_Material_Requisition";

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

		public const string ONHAND_FIELD = "OnHand";

		public const string APPROVALSTATUS_FIELD = "ApprovalStatus";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string FACTOR_FIELD = "Factor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string ISSUED_FIELD = "Issued";

		public const string STATUS_FIELD = "Status";

		public const string REMARKS_FIELD = "Remarks";

		public const string SPECIFICATIONID_FIELD = "SpecificationID";

		public const string STYLEID_FIELD = "StyleID";

		public const string JOBMATERIALREQUISITIONDETAIL_TABLE = "Job_Material_Requisition_Detail";

		public DataTable JobMaterialRequisitionTable => base.Tables["Job_Material_Requisition"];

		public DataTable JobMaterialRequisitionDetailTable => base.Tables["Job_Material_Requisition_Detail"];

		public JobMaterialRequisitionData()
		{
			BuildDataTables();
		}

		public JobMaterialRequisitionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Material_Requisition");
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
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("AccountID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RequestedBy", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("ReqonDate", typeof(DateTime));
			columns.Add("ApprovalStatus", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Material_Requisition_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Cost", typeof(decimal));
			columns.Add("Quantity", typeof(float));
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("Factor", typeof(float));
			columns.Add("FactorType", typeof(string));
			columns.Add("OnHand", typeof(float)).DefaultValue = DBNull.Value;
			columns.Add("Issued", typeof(decimal));
			columns.Add("Remarks", typeof(string));
			columns.Add("SpecificationID", typeof(string));
			columns.Add("StyleID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
