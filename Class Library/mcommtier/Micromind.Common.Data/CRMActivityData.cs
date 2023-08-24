using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CRMActivityData : DataSet
	{
		public const string CRMACTIVITY_TABLE = "Activity";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string CRMACTIVITYNAME_FIELD = "ActivityName";

		public const string CRMACTIVITYTYPE_FIELD = "ActivityType";

		public const string REASONID_FIELD = "ReasonID";

		public const string RELATEDID_FIELD = "RelatedID";

		public const string RELATEDTYPE_FIELD = "RelatedType";

		public const string CONTACTID_FIELD = "ContactID";

		public const string OWNERID_FIELD = "OwnerID";

		public const string DATETIME_FIELD = "ActivityDateTime";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CRMActivityTable => base.Tables["Activity"];

		public CRMActivityData()
		{
			BuildDataTables();
		}

		public CRMActivityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Activity");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("ActivityName", typeof(string)).AllowDBNull = false;
			columns.Add("ActivityType", typeof(byte));
			columns.Add("ReasonID", typeof(string));
			columns.Add("RelatedID", typeof(string));
			columns.Add("RelatedType", typeof(byte));
			columns.Add("ContactID", typeof(string));
			columns.Add("OwnerID", typeof(string));
			columns.Add("ActivityDateTime", typeof(DateTime));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
