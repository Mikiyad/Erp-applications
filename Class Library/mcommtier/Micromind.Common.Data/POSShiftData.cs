using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class POSShiftData : DataSet
	{
		public const string POSSHIFT_TABLE = "POS_Shift";

		public const string SHIFTID_FIELD = "ShiftID";

		public const string BATCHID_FIELD = "BatchID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string USERID_FIELD = "UserID";

		public const string OPENDATE_FIELD = "OpenDate";

		public const string CLOSEDATE_FIELD = "CloseDate";

		public const string CASHREGISTERID_FIELD = "CashRegisterID";

		public const string OPENINGCASH_FIELD = "OpeningCash";

		public const string CLOSINGCASH_FIELD = "ClosingCash";

		public const string STATUS_FIELD = "Status";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable POSShiftTable => base.Tables["POS_Shift"];

		public POSShiftData()
		{
			BuildDataTables();
		}

		public POSShiftData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("POS_Shift");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ShiftID", typeof(int));
			dataColumn.AllowDBNull = true;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LocationID", typeof(string)).AllowDBNull = true;
			columns.Add("BatchID", typeof(int));
			columns.Add("CashRegisterID", typeof(string));
			columns.Add("UserID", typeof(string));
			columns.Add("OpeningCash", typeof(decimal));
			columns.Add("ClosingCash", typeof(decimal));
			columns.Add("OpenDate", typeof(DateTime));
			columns.Add("CloseDate", typeof(DateTime));
			columns.Add("Status", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
