using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LetterData : DataSet
	{
		public const string LETTERS_TABLE = "Letters";

		public const string LETTERID_FIELD = "LetterID";

		public const string LETTERNAME_FIELD = "LetterName";

		public const string BODY_FIELD = "Body";

		public const string SUBJECT_FIELD = "Subject";

		public const string SALUTATION_FIELD = "Salutation";

		public const string COMPLIMENTARYCLOSING_FIELD = "ComplimentaryClosing";

		public const string ISRIGHTTOLEFT_FIELD = "IsRightToLeft";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public DataTable LetterTable => base.Tables["Letters"];

		public LetterData()
		{
			BuildDataTables();
		}

		public LetterData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Letters");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LetterID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LetterName", typeof(string)).AllowDBNull = false;
			columns.Add("Body", typeof(string));
			columns.Add("Subject", typeof(string));
			columns.Add("Salutation", typeof(string));
			columns.Add("ComplimentaryClosing", typeof(string));
			columns.Add("IsRightToLeft", typeof(bool)).DefaultValue = false;
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
