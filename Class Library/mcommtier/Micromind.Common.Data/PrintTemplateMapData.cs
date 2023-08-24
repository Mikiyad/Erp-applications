using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PrintTemplateMapData : DataSet
	{
		public const string PRINTTEMPLATEMAP_TABLE = "Print_Template_Map";

		public const string MAPID_FIELD = "MapID";

		public const string TEMPLATENAME_FIELD = "TemplateName";

		public const string SCREENTYPEID_FIELD = "ScreenType";

		public const string SCREENID_FIELD = "ScreenID";

		public const string SCREENAREA_FIELD = "ScreenArea";

		public const string FILENAME_FIELD = "FileName";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PrintTemplateMapTable => base.Tables["Print_Template_Map"];

		public PrintTemplateMapData()
		{
			BuildDataTables();
		}

		public PrintTemplateMapData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Print_Template_Map");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("MapID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TemplateName", typeof(string));
			columns.Add("ScreenType", typeof(int));
			columns.Add("ScreenID", typeof(string));
			columns.Add("ScreenArea", typeof(string));
			columns.Add("FileName", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
