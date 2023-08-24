using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CompanyOptionData : DataSet
	{
		public const string COMPANYOPTION_TABLE = "Company_Option";

		public const string OPTIONID_FIELD = "OptionID";

		public const string GROUPID_FIELD = "GroupID";

		public const string OPTIONVALUE_FIELD = "OptionValue";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public DataTable CompanyOptionTable => base.Tables["Company_Option"];

		public CompanyOptionData()
		{
			BuildDataTables();
		}

		public CompanyOptionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Company_Option");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("OptionID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("OptionValue", typeof(string)).AllowDBNull = false;
			columns.Add("GroupID", typeof(short));
			columns.Add("SysDocType", typeof(int));
			columns.Add("SysDocID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
