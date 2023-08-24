using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class AccountAnalysisData : DataSet
	{
		public const string ACCOUNTID_FIELD = "AccountID";

		public const string ANALYSISGROUPID_FIELD = "AnalysisGroupID";

		public const string TYPE_FIELD = "TYPE";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ACCCOUNTANALYSISDETAIL_TABLE = "Account_Analysis_Detail";

		public DataTable AccountAnalysisDetailTable => base.Tables["Account_Analysis_Detail"];

		public AccountAnalysisData()
		{
			BuildDataTables();
		}

		public AccountAnalysisData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Account_Analysis_Detail");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AccountID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("AnalysisGroupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("TYPE", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
