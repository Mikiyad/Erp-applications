using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobEstimationData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string MATERIALOHP_FIELD = "MaterialOHP";

		public const string LABOUROHP_FIELD = "LabourOHP";

		public const string OTHEROHP_FIELD = "OtherOHP";

		public const string JOBID_FIELD = "JobID";

		public const string NOTE_FIELD = "Note";

		public const string REFERENCE_FIELD = "Reference";

		public const string LABELC1_FIELD = "LabelC1";

		public const string LABELC2_FIELD = "LabelC2";

		public const string LABELC3_FIELD = "LabelC3";

		public const string LABELC4_FIELD = "LabelC4";

		public const string LABELC5_FIELD = "LabelC5";

		public const string LABELC6_FIELD = "LabelC6";

		public const string JOBESTIMATION_TABLE = "Job_Estimation";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string PHASEID_FIELD = "PhaseID";

		public const string BOQID_FIELD = "BOQID";

		public const string BOQQUANTITY_FIELD = "BOQQuantity";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string COST_FIELD = "Cost";

		public const string ACTUALCOST_FIELD = "ActualCost";

		public const string LABOURCOST_FIELD = "LabourCost";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string ITEMTYPE_FIELD = "ItemType";

		public const string QUANTITY_FIELD = "Quantity";

		public const string MATERIALTOTAL_FIELD = "MaterialTotal";

		public const string LABOURTOTAL_FIELD = "LabourTotal";

		public const string MATERIALOH_FIELD = "MaterialOH";

		public const string LABOUROH_FIELD = "LabourOH";

		public const string OTHEROH_FIELD = "OtherOH";

		public const string NETTOTAL_FIELD = "NetTotal";

		public const string ATTRIBUTEC1_FIELD = "AttributeC1";

		public const string ATTRIBUTEC2_FIELD = "AttributeC2";

		public const string ATTRIBUTEC3_FIELD = "AttributeC3";

		public const string ATTRIBUTEC4_FIELD = "AttributeC4";

		public const string ATTRIBUTEC5_FIELD = "AttributeC5";

		public const string ATTRIBUTEC6_FIELD = "AttributeC6";

		public const string REMARKS_FIELD = "Remarks";

		public const string TOTAL_FIELD = "Total";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITCOST_FIELD = "UnitCost";

		public const string UNITLABOURCOST_FIELD = "UnitLabourCost";

		public const string ROWRELATION_FIELD = "RowRelation";

		public const string COSTMARKUP_FIELD = "CostMarkUp";

		public const string PREVIOUSREVISEDDATE_FIELD = "PreviousRevisedDate";

		public const string LASTREVISEDDATE_FIELD = "LastRevisedDate";

		public const string REVISIONNO_FIELD = "RevisionNo";

		public const string ISVOID_FIELD = "IsVoid";

		public const string JOBESTIMATIONDETAIL_TABLE = "Job_Estimation_Detail";

		public const string JOBESTIMATIONDETAILITEMS_TABLE = "Job_Estimation_Detail_Item";

		public DataTable JobEstimationTable => base.Tables["Job_Estimation"];

		public DataTable JobEstimationDetailTable => base.Tables["Job_Estimation_Detail"];

		public DataTable JobEstimationDetailItemsTable => base.Tables["Job_Estimation_Detail_Item"];

		public JobEstimationData()
		{
			BuildDataTables();
		}

		public JobEstimationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Estimation");
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
			columns.Add("MaterialOHP", typeof(string));
			columns.Add("LabourOHP", typeof(string));
			columns.Add("OtherOHP", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("LabelC1", typeof(string));
			columns.Add("LabelC2", typeof(string));
			columns.Add("LabelC3", typeof(string));
			columns.Add("LabelC4", typeof(string));
			columns.Add("LabelC5", typeof(string));
			columns.Add("LabelC6", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("LastRevisedDate", typeof(DateTime));
			columns.Add("PreviousRevisedDate", typeof(DateTime));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Estimation_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("PhaseID", typeof(string));
			columns.Add("BOQID", typeof(string));
			columns.Add("BOQQuantity", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("AttributeC1", typeof(decimal));
			columns.Add("AttributeC2", typeof(decimal));
			columns.Add("AttributeC3", typeof(decimal));
			columns.Add("AttributeC4", typeof(decimal));
			columns.Add("AttributeC5", typeof(decimal));
			columns.Add("AttributeC6", typeof(decimal));
			columns.Add("Remarks", typeof(string));
			columns.Add("Total", typeof(decimal));
			columns.Add("CostMarkUp", typeof(decimal));
			columns.Add("MaterialOHP", typeof(decimal));
			columns.Add("LabourOHP", typeof(decimal));
			columns.Add("OtherOHP", typeof(decimal));
			columns.Add("RowRelation", typeof(short));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Estimation_Detail_Item");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("BOQID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(decimal));
			columns.Add("Cost", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("LabourCost", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("ActualCost", typeof(decimal));
			columns.Add("MaterialTotal", typeof(decimal));
			columns.Add("LabourTotal", typeof(decimal));
			columns.Add("MaterialOH", typeof(decimal));
			columns.Add("LabourOH", typeof(decimal));
			columns.Add("OtherOH", typeof(decimal));
			columns.Add("NetTotal", typeof(decimal));
			columns.Add("AttributeC1", typeof(decimal));
			columns.Add("AttributeC2", typeof(decimal));
			columns.Add("AttributeC3", typeof(decimal));
			columns.Add("AttributeC4", typeof(decimal));
			columns.Add("AttributeC5", typeof(decimal));
			columns.Add("AttributeC6", typeof(decimal));
			columns.Add("UnitQuantity", typeof(decimal));
			columns.Add("UnitCost", typeof(decimal));
			columns.Add("UnitLabourCost", typeof(decimal));
			columns.Add("RowRelation", typeof(short));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
