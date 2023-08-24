using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeBenefitDetailData : DataSet
	{
		public const string EMPLOYEEBENEFITDETAIL_TABLE = "Employee_Benefit_Detail";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string BENEFITID_FIELD = "BenefitID";

		public const string AMOUNT_FIELD = "Amount";

		public const string LASTAMOUNT_FIELD = "LastAmount";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string REMARKS_FIELD = "Remarks";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EmployeeBenefitDetailTable => base.Tables["Employee_Benefit_Detail"];

		public EmployeeBenefitDetailData()
		{
			BuildDataTables();
		}

		public EmployeeBenefitDetailData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Benefit_Detail");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EmployeeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("BenefitID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("Amount", typeof(decimal));
			columns.Add("LastAmount", typeof(decimal));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
