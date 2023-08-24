using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EquipmentTypeData : DataSet
	{
		public const string TYPEID_FIELD = "EquipmentTypeID";

		public const string TYPENAME_FIELD = "EquipmentTypeName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string EQUIPMENTTYPE_TABLE = "EA_Equipment_Type";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EquipmentTypeTable => base.Tables["EA_Equipment_Type"];

		public EquipmentTypeData()
		{
			BuildDataTables();
		}

		public EquipmentTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("EA_Equipment_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EquipmentTypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("EquipmentTypeName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
