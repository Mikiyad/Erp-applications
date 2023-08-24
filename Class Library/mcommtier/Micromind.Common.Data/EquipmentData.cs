using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EquipmentData : DataSet
	{
		public const string EQUIPMENT_TABLE = "Equipment";

		public const string EQUIPMENTID_FIELD = "EquipmentID";

		public const string EQUIPMENTNAME_FIELD = "EquipmentName";

		public const string EQUIPMENTDESC_FIELD = "Description";

		public const string ASSETID_FIELD = "AssetID";

		public const string BILLINGRATE_FIELD = "BillingRate";

		public const string BILLINGUNIT_FIELD = "BillingUnit";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EquipmentTable => base.Tables["Equipment"];

		public EquipmentData()
		{
			BuildDataTables();
		}

		public EquipmentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Equipment");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EquipmentID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("EquipmentName", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("AssetID", typeof(string));
			columns.Add("BillingRate", typeof(decimal));
			columns.Add("BillingUnit", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
