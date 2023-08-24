using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EquipmentTransferData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string EQUIPMENTID_FIELD = "EquipmentID";

		public const string LOCATIONFROMID_FIELD = "LocationFromID";

		public const string LOCATIONTOID_FIELD = "LocationToID";

		public const string JOBFROMID_FIELD = "JobFromID";

		public const string JOBTO_FIELD = "JobToID";

		public const string EMPLOYEEFROMID_FIELD = "EmployeeFromID";

		public const string EMPLOYEETOID_FIELD = "EmployeeToID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string TRANSFERTYPE_FIELD = "TransferType";

		public const string CURRENTMETERREADING_FIELD = "CurrentMeterReading";

		public const string NOTE_FIELD = "Note";

		public const string ISVOID_FIELD = "IsVoid";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string REQVOUCHERID_FIELD = "ReqVoucherID";

		public const string REQSYSDOCID_FIELD = "ReqSysDocID";

		public const string EQUIPMENTTRANSFER_TABLE = "EA_Equipment_Transfer";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EquipmentTransferTable => base.Tables["EA_Equipment_Transfer"];

		public EquipmentTransferData()
		{
			BuildDataTables();
		}

		public EquipmentTransferData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("EA_Equipment_Transfer");
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
			columns.Add("EquipmentID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("TransferType", typeof(byte));
			columns.Add("Note", typeof(string));
			columns.Add("CurrentMeterReading", typeof(string));
			columns.Add("LocationFromID", typeof(string));
			columns.Add("LocationToID", typeof(string));
			columns.Add("JobFromID", typeof(string));
			columns.Add("JobToID", typeof(string));
			columns.Add("EmployeeFromID", typeof(string));
			columns.Add("EmployeeToID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("ReqSysDocID", typeof(string));
			columns.Add("ReqVoucherID", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
