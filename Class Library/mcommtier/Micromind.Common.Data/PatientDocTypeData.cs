using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PatientDocTypeData : DataSet
	{
		public const string PATIENTDOCUMENTTYPE_TABLE = "Patient_Doc_Type";

		public const string TYPEID_FIELD = "TypeID";

		public const string TYPENAME_FIELD = "TypeName";

		public const string NOTE_FIELD = "Note";

		public const string REMIND_FIELD = "Remind";

		public const string REMINDDAYS_FIELD = "RemindDays";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EmpDocumentTypeTable => base.Tables["Patient_Doc_Type"];

		public PatientDocTypeData()
		{
			BuildDataTables();
		}

		public PatientDocTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Patient_Doc_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TypeName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("Remind", typeof(bool));
			columns.Add("RemindDays", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
