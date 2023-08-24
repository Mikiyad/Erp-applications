using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data.WS
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LicenseData : DataSet
	{
		public static string LICENSE_TABLE = "Licenses";

		public static string LICENSEID_FIELD = "LicenseID";

		public static string LICENSEKEY_FIELD = "LicenseKey";

		public static string LICENSECODE_FIELD = "LicenseCode";

		public static string NUMACTIVATED_FIELD = "NumActivated";

		public static string FIRSTDATEACTIVATED_FIELD = "FirstDateActivated";

		public static string LASTDATEACTIVATED_FIELD = "LastDateActivated";

		public static string COMPANY_FIELD = "Company";

		public static string EMAIL_FIELD = "Email";

		public static string PHONE_FIELD = "Phone";

		public static string CONTACT_FIELD = "Contact";

		public static string MACHINEID_FIELD = "MachineID";

		public DataTable LicenseTable => base.Tables[LICENSE_TABLE];

		public LicenseData()
		{
			BuildDataTables();
		}

		public LicenseData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable(LICENSE_TABLE);
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add(LICENSEID_FIELD, typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add(LICENSEKEY_FIELD, typeof(string));
			columns.Add(LICENSECODE_FIELD, typeof(string));
			columns.Add(NUMACTIVATED_FIELD, typeof(int)).DefaultValue = 0;
			columns.Add(FIRSTDATEACTIVATED_FIELD, typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add(LASTDATEACTIVATED_FIELD, typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add(COMPANY_FIELD, typeof(string));
			columns.Add(EMAIL_FIELD, typeof(string));
			columns.Add(PHONE_FIELD, typeof(string));
			columns.Add(CONTACT_FIELD, typeof(string));
			columns.Add(MACHINEID_FIELD, typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
