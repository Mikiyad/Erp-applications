using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BouncedCheckData : DataSet
	{
		public const string BOUNCEDCHECKID_FIELD = "BouncedCheckID";

		public const string ORIGINALCHECKID_FIELD = "OriginalCheckID";

		public const string BOUNCEDDATE_FIELD = "BouncedDate";

		public const string ISREASONBANKCREDIT_FIELD = "IsReasonBankCredit";

		public const string DESCRIPTION_FIELD = "Description";

		public const string BOUNCEDCHECKS_TABLE = "[Bounced Checks]";

		public DataTable BouncedCheckTable => base.Tables["[Bounced Checks]"];

		public BouncedCheckData()
		{
			BuildDataTables();
		}

		public BouncedCheckData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Bounced Checks]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BouncedCheckID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("OriginalCheckID", typeof(int)).AllowDBNull = false;
			columns.Add("BouncedDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("IsReasonBankCredit", typeof(bool));
			columns.Add("Description", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
