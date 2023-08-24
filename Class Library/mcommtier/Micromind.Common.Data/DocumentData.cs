using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DocumentData : DataSet
	{
		public static string DOCUMENTID_FIELD = "DocumentID";

		public static string CREATEDBY_FIELD = "CreatedBy";

		public static string DATECREATED_FIELD = "DateCreated";

		public static string LASTVERIFIED_FIELD = "LastVerified";

		public static string SUBJECT_FIELD = "Subject";

		public static string TITLE_FIELD = "Title";

		public static string DESCRIPTION_FIELD = "Description";

		public static string PUBLISHER_FIELD = "Publisher";

		public static string CONTRIBUTOR_FIELD = "Contributor";

		public static string PUBLISHEDDATE_FIELD = "PublishedDate";

		public static string REVISEDDATE_FIELD = "RevisedDate";

		public static string TYPE_FIELD = "Type";

		public static string DOCLANGUAGE_FIELD = "DocLanguage";

		public static string SOURCEURL_FIELD = "SourceURL";

		public static string FILEPATH_FIELD = "FilePath";

		public static string SOURCERELIABILITY_FIELD = "SourceReliability";

		public static string SUMMARY_FIELD = "Summary";

		public static string KEYWORDS_FIELD = "KeyWords";

		public static string NOTE_FIELD = "Note";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public static string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public static string DOCUMENTS_TABLE = "Documents";

		public DataTable DocumentTable => base.Tables[DOCUMENTS_TABLE];

		public DocumentData()
		{
			BuildDataTables();
		}

		public DocumentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable(DOCUMENTS_TABLE);
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add(DOCUMENTID_FIELD, typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add(CREATEDBY_FIELD, typeof(short));
			columns.Add(DATECREATED_FIELD, typeof(DateTime));
			columns.Add(LASTVERIFIED_FIELD, typeof(DateTime));
			columns.Add(SUBJECT_FIELD, typeof(string)).AllowDBNull = false;
			columns.Add(TITLE_FIELD, typeof(string));
			columns.Add(DESCRIPTION_FIELD, typeof(string));
			columns.Add(PUBLISHER_FIELD, typeof(int));
			columns.Add(CONTRIBUTOR_FIELD, typeof(int));
			columns.Add(PUBLISHEDDATE_FIELD, typeof(DateTime));
			columns.Add(REVISEDDATE_FIELD, typeof(DateTime));
			columns.Add(TYPE_FIELD, typeof(short));
			columns.Add(DOCLANGUAGE_FIELD, typeof(short));
			columns.Add(SOURCEURL_FIELD, typeof(string));
			columns.Add(FILEPATH_FIELD, typeof(string));
			columns.Add(SOURCERELIABILITY_FIELD, typeof(byte));
			columns.Add(SUMMARY_FIELD, typeof(string));
			columns.Add(KEYWORDS_FIELD, typeof(string));
			columns.Add(NOTE_FIELD, typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add(DATETIMESTAMP_FIELD, typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
