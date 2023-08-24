using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class FixedAssetTransferData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string LOCATIONFROMID_FIELD = "LocationFromID";

		public const string LOCATIONTOID_FIELD = "LocationToID";

		public const string DIVISIONFROMID_FIELD = "DivisionFromID";

		public const string DIVISIONTOID_FIELD = "DivisionToID";

		public const string DEPARTMENTFROMOID_FIELD = "DepartmentFromID";

		public const string DEPARTMENTTOID_FIELD = "DepartmentToID";

		public const string EMPLOYEEFROMID_FIELD = "EmployeeFromID";

		public const string EMPLOYEETOID_FIELD = "EmployeeToID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string TRANSFERTYPE_FIELD = "TransferType";

		public const string NOTE_FIELD = "Note";

		public const string ISVOID_FIELD = "IsVoid";

		public const string FIXEDASSETTRANSFER_TABLE = "FixedAsset_Transfer";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ASSETID_FIELD = "AssetID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string FIXEDASSETTRANSFERDETAIL_TABLE = "FixedAsset_Transfer_Detail";

		public DataTable FixedAssetTransferTable => base.Tables["FixedAsset_Transfer"];

		public FixedAssetTransferData()
		{
			BuildDataTables();
		}

		public FixedAssetTransferData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("FixedAsset_Transfer");
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
			columns.Add("Reference", typeof(string));
			columns.Add("TransferType", typeof(byte));
			columns.Add("Note", typeof(string));
			columns.Add("AssetID", typeof(string));
			columns.Add("LocationFromID", typeof(string));
			columns.Add("LocationToID", typeof(string));
			columns.Add("DivisionFromID", typeof(string));
			columns.Add("DivisionToID", typeof(string));
			columns.Add("DepartmentFromID", typeof(string));
			columns.Add("DepartmentToID", typeof(string));
			columns.Add("EmployeeFromID", typeof(string));
			columns.Add("EmployeeToID", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
