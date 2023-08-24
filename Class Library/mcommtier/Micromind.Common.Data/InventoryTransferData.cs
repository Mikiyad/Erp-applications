using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class InventoryTransferData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ACCEPTDATE_FIELD = "AcceptDate";

		public const string REJECTREFERENCE_FIELD = "RejectReference";

		public const string REJECTNOTE_FIELD = "RejectNote";

		public const string REJECTDATE_FIELD = "RejectDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string ACCEPTSYSDOCID_FIELD = "AcceptSysDocID";

		public const string ACCEPTVOUCHERID_FIELD = "AcceptVoucherID";

		public const string REJECTACCEPTSYSDOCID_FIELD = "RejectAcceptSysDocID";

		public const string REJECTACCEPTVOUCHERID_FIELD = "RejectAcceptVoucherID";

		public const string REJECTACCEPTNOTE_FIELD = "RejectAcceptNote";

		public const string REJECTACCEPTDATE_FIELD = "RejectAcceptDate";

		public const string REJECTACCEPTREFERENCE_FIELD = "RejectAcceptReference";

		public const string REJECTSYSDOCID_FIELD = "RejectSysDocID";

		public const string ACCEPTREFERENCE_FIELD = "AcceptReference";

		public const string LOCATIONFROMID_FIELD = "LocationFromID";

		public const string LOCATIONTOID_FIELD = "LocationToID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string DRIVERID_FIELD = "DriverID";

		public const string VEHICLENUMBER_FIELD = "VehicleNumber";

		public const string TRANSFERTYPEID_FIELD = "TransferTypeID";

		public const string ISVOID_FIELD = "IsVoid";

		public const string ISACCEPTED_FIELD = "IsAccepted";

		public const string ISREJECTED_FIELD = "IsRejected";

		public const string ISREJECTACCEPTED_FIELD = "IsRejectAccepted";

		public const string INVENTORYTRANSFER_TABLE = "Inventory_Transfer";

		public const string ACCEPTEDBY_FIELD = "AcceptedBy";

		public const string REJECTEDBY_FIELD = "RejectedBy";

		public const string REJECTACCEPTEDBY_FIELD = "RejectAcceptedBy";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string UNITID_FIELD = "UnitID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string REMARKS_FIELD = "Remarks";

		public const string FACTOR_FIELD = "Factor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string ACCEPTEDQUANTITY_FIELD = "AcceptedQuantity";

		public const string ACCEPTEDUNITQUANTITY_FIELD = "AcceptedUnitQuantity";

		public const string REJECTEDQUANTITY_FIELD = "RejectedQuantity";

		public const string REJECTEDUNITQUANTITY_FIELD = "RejectedUnitQuantity";

		public const string LISTVOUCHERID_FIELD = "ListVoucherID";

		public const string LISTSYSDOCID_FIELD = "ListSysDocID";

		public const string LISTROWINDEX_FIELD = "ListRowIndex";

		public const string INVENTORYTRANSFERDETAIL_TABLE = "Inventory_Transfer_Detail";

		public DataTable InventoryTransferTable => base.Tables["Inventory_Transfer"];

		public DataTable InventoryTransferDetailsTable => base.Tables["Inventory_Transfer_Detail"];

		public InventoryTransferData()
		{
			BuildDataTables();
		}

		public InventoryTransferData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Inventory_Transfer");
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
			columns.Add("AcceptDate", typeof(DateTime));
			columns.Add("RejectDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("AcceptReference", typeof(string));
			columns.Add("AcceptVoucherID", typeof(string));
			columns.Add("RejectAcceptSysDocID", typeof(string));
			columns.Add("RejectAcceptVoucherID", typeof(string));
			columns.Add("RejectAcceptNote", typeof(string));
			columns.Add("RejectAcceptDate", typeof(DateTime));
			columns.Add("RejectedBy", typeof(string));
			columns.Add("AcceptedBy", typeof(string));
			columns.Add("RejectAcceptedBy", typeof(string));
			columns.Add("RejectAcceptReference", typeof(string));
			columns.Add("TransferTypeID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("LocationFromID", typeof(string));
			columns.Add("LocationToID", typeof(string));
			columns.Add("AcceptSysDocID", typeof(string));
			columns.Add("RejectSysDocID", typeof(string));
			columns.Add("RejectReference", typeof(string));
			columns.Add("RejectNote", typeof(string));
			columns.Add("DriverID", typeof(string));
			columns.Add("VehicleNumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("IsAccepted", typeof(bool));
			columns.Add("IsRejected", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Inventory_Transfer_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("UnitID", typeof(string));
			columns.Add("Quantity", typeof(float));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("Factor", typeof(float));
			columns.Add("FactorType", typeof(string));
			columns.Add("IsTrackLot", typeof(bool));
			columns.Add("AcceptedQuantity", typeof(float));
			columns.Add("AcceptedUnitQuantity", typeof(float));
			columns.Add("RejectedQuantity", typeof(float));
			columns.Add("RejectedUnitQuantity", typeof(float));
			columns.Add("ListVoucherID", typeof(string));
			columns.Add("ListSysDocID", typeof(string));
			columns.Add("ListRowIndex", typeof(short));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotIssueDetailTable(this);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
