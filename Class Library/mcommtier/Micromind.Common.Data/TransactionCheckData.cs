using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TransactionCheckData : DataSet
	{
		public const string CHECKID_FIELD = "CheckID";

		public const string TRANSACTIONID_FIELD = "TransactionID";

		public const string CHECKDATE_FIELD = "CheckDate";

		public const string CHECKNUMBER_FIELD = "CheckNumber";

		public const string BANKID_FIELD = "BankID";

		public const string STATUS_FIELD = "Status";

		public const string DESCRIPTIONS_FIELD = "Descriptions";

		public const string TRANSACTIONCHECKS_TABLE = "[Transaction Checks]";

		public const string TRANSACTION_REL = "TransactionRelation";

		public const string BANK_REL = "BankRelation";

		public DataTable TransactionCheckTable => base.Tables["[Transaction Checks]"];

		public TransactionCheckData()
		{
			BuildDataTables();
		}

		public TransactionCheckData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Transaction Checks]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CheckID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("TransactionID", typeof(int));
			columns.Add("CheckNumber", typeof(string));
			columns.Add("CheckDate", typeof(DateTime));
			columns.Add("BankID", typeof(int));
			columns.Add("Status", typeof(byte));
			columns.Add("Descriptions", typeof(string));
			base.Tables.Add(dataTable);
		}

		public void AddRelations()
		{
			if (!base.Tables.Contains("GL_Transaction"))
			{
				throw new ApplicationException("Relation GL_Transaction must exist.");
			}
			if (!base.Tables.Contains("Bank"))
			{
				throw new ApplicationException("Relation Bank must exist.");
			}
			DataRelation relation = new DataRelation("TransactionRelation", base.Tables["GL_Transaction"].Columns["TransactionID"], TransactionCheckTable.Columns["TransactionID"]);
			DataRelation relation2 = new DataRelation("BankRelation", base.Tables["Bank"].Columns["BankID"], TransactionCheckTable.Columns["BankID"]);
			try
			{
				base.Relations.Add(relation);
				base.Relations.Add(relation2);
			}
			catch
			{
				throw;
			}
		}
	}
}
