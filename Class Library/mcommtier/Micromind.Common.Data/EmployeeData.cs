using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeData : DataSet
	{
		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string LASTNAME_FIELD = "LastName";

		public const string FIRSTNAME_FIELD = "FirstName";

		public const string MIDDLENAME_FIELD = "MiddleName";

		public const string BIRTHDATE_FIELD = "BirthDate";

		public const string NICKNAME_FIELD = "NickName";

		public const string JOININGDATE_FIELD = "JoiningDate";

		public const string GROUPID_FIELD = "GroupID";

		public const string TERMINATIONDATE_FIELD = "TerminationDate";

		public const string TERMINATIONID_FIELD = "TerminationID";

		public const string ISTERMINATED_FIELD = "IsTerminated";

		public const string CANCELLATIONDATE_FIELD = "CancellationDate";

		public const string ISCANCELLED_FIELD = "IsCancelled";

		public const string GRADEID_FIELD = "GradeID";

		public const string DAYOFF_FIELD = "DayOff";

		public const string ONVACATION_FIELD = "OnVacation";

		public const string BIRTHPLACE_FIELD = "BirthPlace";

		public const string SPONSORID_FIELD = "SponsorID";

		public const string NATIONALITYID_FIELD = "NationalityID";

		public const string PROBATION_FIELD = "Probation";

		public const string CONFIRMATIONDATE_FIELD = "ConfirmationDate";

		public const string RELIGIONID_FIELD = "ReligionID";

		public const string BLOODGROUP_FIELD = "BloodGroup";

		public const string QUALIFICATION_FIELD = "Qualification";

		public const string CONTRACTTYPE_FIELD = "ContractType";

		public const string PAYMENTMETHODID_FIELD = "PaymentMethodID";

		public const string BANKID_FIELD = "BankID";

		public const string ACCOUNTNUMBER_FIELD = "AccountNumber";

		public const string UID_FIELD = "UID";

		public const string VISANUMBER_FIELD = "VisaNumber";

		public const string ANNUALLEAVEDATE_FIELD = "AnnualLeaveDate";

		public const string RESUMEDDATE_FIELD = "ResumedDate";

		public const string NOTES_FIELD = "Notes";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYDIVISIONID_FIELD = "CompanyDivisionID";

		public const string DEPARTMENTID_FIELD = "DepartmentID";

		public const string POSITIONID_FIELD = "PositionID";

		public const string REPORTTOID_FIELD = "ReportToID";

		public const string PAYPERIOD_FIELD = "PayPeriod";

		public const string GENDER_FIELD = "Gender";

		public const string LASTPAYDATE_FIELD = "LastPayDate";

		public const string MARITALSTATUS_FIELD = "MaritalStatus";

		public const string MEDICALINSURANCEPROVIDERID_FIELD = "MedicalInsuranceProviderID";

		public const string MEDICALINSURANCECATEGORYID_FIELD = "MedicalInsuranceCategoryID";

		public const string MEDICALINSURANCENUMBER_FIELD = "MedicalInsuranceNumber";

		public const string MEDICALINSURANCEVALIDFROM_FIELD = "MedicalInsValidFrom";

		public const string MEDICALINSURANCEVALIDTO_FIELD = "MedicalInsValidTo";

		public const string FAMILYMEMBERSCOUNT_FIELD = "NumberOfDependants";

		public const string MEDICALINSURANCEAMOUNT_FIELD = "MedicalInsuranceAmount";

		public const string SPOUSENAME_FIELD = "SpouseName";

		public const string NATIONALID_FIELD = "NationalID";

		public const string STATUS_FIELD = "Status";

		public const string PRIMARYADDRESSID_FIELD = "PrimaryAddressID";

		public const string BASICSALARY_FIELD = "BasicSalary";

		public const string CURRENCY_FIELD = "CurrencyID";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string SALARYREMARKS_FIELD = "SalaryRemarks";

		public const string LASTREVISEDSALARYDATE_FIELD = "LastRevisedSalaryDate";

		public const string EOSRULEID_FIELD = "EOSRuleID";

		public const string OVERTIMEID_FIELD = "OverTimeID";

		public const string BALANCE_FIELD = "Balance";

		public const string DESTINATIONID_FIELD = "DestinationID";

		public const string NUMBEROFTICKETS_FIELD = "NumberOfTickets";

		public const string TICKETAMOUNT_FIELD = "TicketAmount";

		public const string TICKETPERIOD_FIELD = "TicketPeriod";

		public const string TICKETREMARKS_FIELD = "TicketRemarks";

		public const string APPRIASALPOINTS_FIELD = "AppriasalPoints";

		public const string CALENDARID_FIELD = "CalendarID";

		public const string USERDEFINED1_FIELD = "UserDefined1";

		public const string USERDEFINED2_FIELD = "UserDefined2";

		public const string USERDEFINED3_FIELD = "UserDefined3";

		public const string USERDEFINED4_FIELD = "UserDefined4";

		public const string EMPANALYSISID_FIELD = "EmpAnalysisID";

		public const string ANALYSISGROUPID_FIELD = "AnalysisGroupID";

		public const string LABOURID_FIELD = "LabourID";

		public const string IBANNUMBER_FIELD = "IBAN";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PREVIOUSREVISEDSALARYDATE_FIELD = "PreviousRevisedSalaryDate";

		public const string RECRUITMENTCHANNEL_FIELD = "RecruitmentChannelID";

		public const string VISADESIGNATION_FIELD = "VisaDesignationID";

		public const string LEGALPOSITION_FIELD = "LegalPositionID";

		public const string EMPLOYEE_TABLE = "Employee";

		public const string EMPLOYEEADDRESS_TABLE = "Employee_Address";

		public DataTable EmployeeTable => base.Tables["Employee"];

		public DataTable EmployeeAddressTable => base.Tables["Employee_Address"];

		public EmployeeData()
		{
			BuildDataTables();
		}

		public EmployeeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EmployeeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LastName", typeof(string));
			columns.Add("FirstName", typeof(string));
			columns.Add("MiddleName", typeof(string));
			columns.Add("BirthDate", typeof(DateTime));
			columns.Add("NickName", typeof(string));
			columns.Add("JoiningDate", typeof(DateTime));
			columns.Add("GroupID", typeof(string));
			columns.Add("TerminationDate", typeof(DateTime));
			columns.Add("TerminationID", typeof(int));
			columns.Add("IsTerminated", typeof(bool));
			columns.Add("CancellationDate", typeof(DateTime));
			columns.Add("IsCancelled", typeof(bool));
			columns.Add("GradeID", typeof(string));
			columns.Add("DayOff", typeof(short));
			columns.Add("OnVacation", typeof(bool));
			columns.Add("BirthPlace", typeof(string));
			columns.Add("SponsorID", typeof(string));
			columns.Add("NationalityID", typeof(string));
			columns.Add("Probation", typeof(short));
			columns.Add("ConfirmationDate", typeof(DateTime));
			columns.Add("ReligionID", typeof(string));
			columns.Add("BloodGroup", typeof(string));
			columns.Add("Qualification", typeof(string));
			columns.Add("ContractType", typeof(string));
			columns.Add("AnnualLeaveDate", typeof(DateTime));
			columns.Add("ResumedDate", typeof(DateTime));
			columns.Add("Notes", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("CompanyDivisionID", typeof(string));
			columns.Add("DepartmentID", typeof(string));
			columns.Add("PositionID", typeof(string));
			columns.Add("ReportToID", typeof(string));
			columns.Add("PayPeriod", typeof(short));
			columns.Add("Gender", typeof(char));
			columns.Add("MaritalStatus", typeof(short));
			columns.Add("SpouseName", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("BankID", typeof(string));
			columns.Add("NationalID", typeof(string));
			columns.Add("UID", typeof(string));
			columns.Add("VisaNumber", typeof(string));
			columns.Add("Status", typeof(short));
			columns.Add("LabourID", typeof(string));
			columns.Add("EmpAnalysisID", typeof(string));
			columns.Add("IBAN", typeof(string));
			columns.Add("LastRevisedSalaryDate", typeof(DateTime));
			columns.Add("PrimaryAddressID", typeof(string));
			columns.Add("CalendarID", typeof(string));
			columns.Add("UserDefined1", typeof(string));
			columns.Add("UserDefined2", typeof(string));
			columns.Add("UserDefined3", typeof(string));
			columns.Add("UserDefined4", typeof(string));
			columns.Add("PreviousRevisedSalaryDate", typeof(DateTime));
			columns.Add("MedicalInsuranceProviderID", typeof(string));
			columns.Add("MedicalInsuranceCategoryID", typeof(string));
			columns.Add("MedicalInsuranceNumber", typeof(string));
			columns.Add("MedicalInsValidFrom", typeof(DateTime));
			columns.Add("MedicalInsValidTo", typeof(DateTime));
			columns.Add("NumberOfDependants", typeof(short));
			columns.Add("RecruitmentChannelID", typeof(string));
			columns.Add("VisaDesignationID", typeof(string));
			columns.Add("MedicalInsuranceAmount", typeof(decimal));
			columns.Add("LegalPositionID", typeof(string));
			base.Tables.Add(dataTable);
			EmployeeAddressData employeeAddressData = new EmployeeAddressData();
			dataTable = new DataTable("Employee_Address");
			foreach (DataColumn column in employeeAddressData.EmployeeAddressTable.Columns)
			{
				dataTable.Columns.Add(column.ColumnName, column.DataType);
			}
			base.Tables.Add(dataTable);
		}

		public static DataSet CreateUpdatePayrollTransactionTable()
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Employee");
			dataTable.Columns.Add("EmployeeID", typeof(string));
			dataTable.Columns.Add("LastPayDate", typeof(DateTime));
			return dataSet;
		}

		public static DataSet CreateEmployeeSalaryDetailTable()
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Employee");
			dataTable.Columns.Add("EmployeeID", typeof(string));
			dataTable.Columns.Add("BasicSalary", typeof(decimal));
			dataTable.Columns.Add("CurrencyID", typeof(string));
			dataTable.Columns.Add("PaymentMethodID", typeof(byte));
			dataTable.Columns.Add("BankID", typeof(string));
			dataTable.Columns.Add("AccountNumber", typeof(string));
			dataTable.Columns.Add("SalaryRemarks", typeof(string));
			dataTable.Columns.Add("EOSRuleID", typeof(string));
			dataTable.Columns.Add("OverTimeID", typeof(string));
			dataTable.Columns.Add("DestinationID", typeof(string));
			dataTable.Columns.Add("NumberOfTickets", typeof(byte));
			dataTable.Columns.Add("TicketAmount", typeof(decimal));
			dataTable.Columns.Add("TicketPeriod", typeof(short));
			dataTable.Columns.Add("TicketRemarks", typeof(string));
			dataTable.Columns.Add("LastRevisedSalaryDate", typeof(DateTime));
			dataTable.Columns.Add("PreviousRevisedSalaryDate", typeof(DateTime));
			return dataSet;
		}
	}
}
