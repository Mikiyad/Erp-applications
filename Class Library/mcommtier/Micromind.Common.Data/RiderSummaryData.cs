using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class RiderSummaryData : DataSet
	{
		public const string RIDERSUMMARY_TABLE = "Rider_Summary";

		public const string RIDERSUMMARYID_FIELD = "RiderID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string RIDERSUMMARYNAME_FIELD = "RiderName";

		public const string TYPE_FIELD = "Type";

		public const string LICENSENUMBER_FIELD = "LicenseNumber";

		public const string FEIREGNO_FIELD = "FEIRegisterNumber";

		public const string FATHERSNAME_FIELD = "FathersName";

		public const string FAMILYNAME_FIELD = "FamilyName";

		public const string NATIONALITY_FIELD = "Nationality";

		public const string DATEOFBIRTH_FIELD = "DateofBirth";

		public const string GENDER_FIELD = "Gender";

		public const string CONTACTNUMBER_FIELD = "ContactNumber";

		public const string EMAIL_FIELD = "EMail";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable RiderSummaryTable => base.Tables["Rider_Summary"];

		public RiderSummaryData()
		{
			BuildDataTables();
		}

		public RiderSummaryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Rider_Summary");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("RiderID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("RiderName", typeof(string));
			columns.Add("Type", typeof(string));
			columns.Add("IsInactive", typeof(bool));
			columns.Add("LicenseNumber", typeof(string));
			columns.Add("FEIRegisterNumber", typeof(string));
			columns.Add("FathersName", typeof(string));
			columns.Add("FamilyName", typeof(string));
			columns.Add("Nationality", typeof(string));
			columns.Add("DateofBirth", typeof(DateTime));
			columns.Add("Gender", typeof(string));
			columns.Add("ContactNumber", typeof(string));
			columns.Add("EMail", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
