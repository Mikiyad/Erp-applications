using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EquipmentCategoryData : DataSet
	{
		public const string CATEGORYID_FIELD = "EquipmentCategoryID";

		public const string CATEGORYNAME_FIELD = "EquipmentCategoryName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string EQUIPMENTCATEGORY_TABLE = "EA_Equipment_Category";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EquipmentCategoryTable => base.Tables["EA_Equipment_Category"];

		public EquipmentCategoryData()
		{
			BuildDataTables();
		}

		public EquipmentCategoryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("EA_Equipment_Category");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EquipmentCategoryID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("EquipmentCategoryName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
