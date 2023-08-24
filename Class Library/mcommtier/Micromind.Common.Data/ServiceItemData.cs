using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ServiceItemData : DataSet
	{
		public const string SERVICEITEM_TABLE = "Service_Item";

		public const string SERVICEITEMID_FIELD = "ServiceItemID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string SERVICETYPE_FIELD = "ServiceType";

		public const string REPEATCOUNTDAYS_FIELD = "RepeatCountDays";

		public const string REPEATCOUNTKM_FIELD = "RepeatCountKM";

		public const string APACCOUNTID_FIELD = "APAccountID";

		public const string REMINDERDAYS_FIELD = "ReminderDays";

		public const string REMINDERKMS_FIELD = "ReminderKM";

		public const string VEHICLETYPE_FIELD = "VehicleType";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ServiceItemTable => base.Tables["Service_Item"];

		public ServiceItemData()
		{
			BuildDataTables();
		}

		public ServiceItemData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Service_Item");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ServiceItemID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ServiceType", typeof(string));
			columns.Add("RepeatCountDays", typeof(decimal)).DefaultValue = 0;
			columns.Add("RepeatCountKM", typeof(decimal)).DefaultValue = 0;
			columns.Add("ReminderDays", typeof(decimal)).DefaultValue = 0;
			columns.Add("ReminderKM", typeof(decimal)).DefaultValue = 0;
			columns.Add("APAccountID", typeof(string));
			columns.Add("VehicleType", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("Description", typeof(string));
			columns.Add("TaxOption", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
