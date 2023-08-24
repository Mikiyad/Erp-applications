using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class MatrixTemplateData : DataSet
	{
		public const string MATRIXTEMPLATE_TABLE = "Matrix_Template";

		public const string MATRIXTEMPLATEID_FIELD = "TemplateID";

		public const string MATRIXTEMPLATENAME_FIELD = "TemplateName";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string MATRIXTEMPLATEDETAIL_TABLE = "Matrix_Template_Detail";

		public const string DIMENSION_FIELD = "Dimension";

		public const string DIMENSIONID_FIELD = "DimensionID";

		public const string ATTRIBUTEID_FIELD = "AttributeID";

		public const string ATTRIBUTENAME_FIELD = "AttributeName";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable MatrixTemplateTable => base.Tables["Matrix_Template"];

		public DataTable MatrixTemplateDetailTable => base.Tables["Matrix_Template_Detail"];

		public MatrixTemplateData()
		{
			BuildDataTables();
		}

		public MatrixTemplateData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Matrix_Template");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TemplateID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TemplateName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Matrix_Template_Detail");
			columns = dataTable.Columns;
			columns.Add("TemplateID", typeof(string)).AllowDBNull = false;
			columns.Add("Dimension", typeof(byte)).AllowDBNull = false;
			columns.Add("DimensionID", typeof(string)).AllowDBNull = false;
			columns.Add("AttributeID", typeof(string)).AllowDBNull = false;
			columns.Add("AttributeName", typeof(string));
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
		}
	}
}
