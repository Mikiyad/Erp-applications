using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeEOSSettlementData : DataSet
	{
		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string LASTWORKINGDATE_FIELD = "LastWorkingDate";

		public const string EOSBENEFIT_FIELD = "EOSBenefit";

		public const string LEAVEDUE_FIELD = "LeaveDue";

		public const string DUEAMOUNT_FIELD = "DueAmount";

		public const string SALARYDUE_FIELD = "SalaryDue";

		public const string OTHERBENEFITS_FIELD = "OtherBenefits";

		public const string TOTALPAYABLE_FIELD = "TotalPayable";

		public const string LOAN_FIELD = "Loan";

		public const string OTHERDEDUCTIONID_FIELD = "OtherDeductionID";

		public const string DEDUCTIONAMOUNT_FIELD = "DeductionAmount";

		public const string TICKETAMOUNT_FIELD = "TicketAmount";

		public const string NETTOTAL_FIELD = "NetTotal";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ISVOID_FIELD = "IsVoid";

		public const string EMPLOYEEEOSSETTLEMENT_TABLE = "Employee_EOSSettlement";

		public const string EMPLOYEEEOS_TABLE = "Employee_EOS";

		public const string EMPLOYEEEOSEOSDEDUCTION_TABLE = "Employee_EOS_Deduction_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string EMPLOYEEBASIC_FIELD = "EmployeeBasic";

		public const string CALCULATEDLEAVEAMOUNT_FIELD = "CalculatedLeaveAmount";

		public const string CALCULATEDSALARYAMOUNT_FIELD = "CalculatedSalaryAmount";

		public const string CALCULATEDGRATUITYAMOUNT_FIELD = "CalculatedGratuityAmount";

		public const string PAIDLEAVEAMOUNT_FIELD = "PaidLeaveAmount";

		public const string PAIDSALARYAMOUNT_FIELD = "PaidSalaryAmount";

		public const string PAIDGRATUITYAMOUNT_FIELD = "PaidGratuityAmount";

		public const string PAIDDEDUCTIONAMOUNT_FIELD = "PaidDeductionAmount";

		public const string PAIDLOANAMOUNT_FIELD = "PaidLoanAmount";

		public const string LEAVEDESCRIPTION_FIELD = "LeaveDescription";

		public const string SALARYDESCRIPTION_FIELD = "SalaryDescription";

		public const string GRATUITYDESCRIPTION_FIELD = "GratuityDescription";

		public const string PAIDTICKETAMOUNT_FIELD = "PaidTicketAmount";

		public const string NOTE_FIELD = "Note";

		public const string ISRESIGNED_FIELD = "IsResigned";

		public const string DEDUCTIONID_FIELD = "DeductionID";

		public const string AMOUNT_FIELD = "Amount";

		public const string DESCRIPTION_FIELD = "Description";

		public DataTable EmployeeEOSSettlementTable => base.Tables["Employee_EOSSettlement"];

		public DataTable EmployeeEOSTable => base.Tables["Employee_EOS"];

		public DataTable EmployeeEOSDeductionDetailTable => base.Tables["Employee_EOS_Deduction_Detail"];

		public EmployeeEOSSettlementData()
		{
			BuildDataTables();
		}

		public EmployeeEOSSettlementData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_EOSSettlement");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EmployeeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LastWorkingDate", typeof(DateTime));
			columns.Add("EOSBenefit", typeof(decimal));
			columns.Add("LeaveDue", typeof(short));
			columns.Add("DueAmount", typeof(decimal));
			columns.Add("SalaryDue", typeof(decimal));
			columns.Add("OtherBenefits", typeof(decimal));
			columns.Add("TotalPayable", typeof(decimal));
			columns.Add("Loan", typeof(decimal));
			columns.Add("OtherDeductionID", typeof(string));
			columns.Add("DeductionAmount", typeof(decimal));
			columns.Add("NetTotal", typeof(decimal));
			columns.Add("TicketAmount", typeof(decimal));
			columns.Add("IsVoid", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_EOS");
			columns = dataTable.Columns;
			dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("EmployeeBasic", typeof(decimal));
			columns.Add("LastWorkingDate", typeof(DateTime));
			columns.Add("IsResigned", typeof(bool));
			columns.Add("CalculatedGratuityAmount", typeof(decimal));
			columns.Add("CalculatedLeaveAmount", typeof(decimal));
			columns.Add("CalculatedSalaryAmount", typeof(decimal));
			columns.Add("PaidGratuityAmount", typeof(decimal));
			columns.Add("PaidLeaveAmount", typeof(decimal));
			columns.Add("PaidSalaryAmount", typeof(decimal));
			columns.Add("PaidTicketAmount", typeof(decimal));
			columns.Add("PaidLoanAmount", typeof(decimal));
			columns.Add("PaidDeductionAmount", typeof(decimal));
			columns.Add("GratuityDescription", typeof(string));
			columns.Add("LeaveDescription", typeof(string));
			columns.Add("SalaryDescription", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("NetTotal", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_EOS_Deduction_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("DeductionID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
