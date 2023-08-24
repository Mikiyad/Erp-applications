using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Employees : StoreObject
	{
		public class Leavelist
		{
			private DateTime start;

			private DateTime end;

			public DateTime Start
			{
				get
				{
					return start;
				}
				set
				{
					start = value;
				}
			}

			public DateTime End
			{
				get
				{
					return end;
				}
				set
				{
					end = value;
				}
			}

			public Leavelist(DateTime Dtstart, DateTime Dtend)
			{
				start = Dtstart;
				end = Dtend;
			}
		}

		private const string EMPLOYEETABLE_PARM = "Employee";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string LASTNAME_PARM = "@LastName";

		private const string FIRSTNAME_PARM = "@FirstName";

		private const string MIDDLENAME_PARM = "@MiddleName";

		private const string BIRTHDATE_PARM = "@BirthDate";

		private const string NICKNAME_PARM = "@NickName";

		private const string JOININGDATE_PARM = "@JoiningDate";

		private const string TERMINATIONDATE_PARM = "@TerminationDate";

		private const string TERMINATIONREASON_PARM = "@TerminationReason";

		private const string TERMINATIONNOTE_PARM = "@TerminationNote";

		private const string CANCELLATIONDATE_PARM = "@CancellationDate";

		private const string GROUPID_PARM = "@GroupID";

		private const string GRADEID_PARM = "@GradeID";

		private const string DAYOFF_PARM = "@DayOff";

		private const string ONVACATION_PARM = "@OnVacation";

		private const string BIRTHPLACE_PARM = "@BirthPlace";

		private const string SPONSORID_PARM = "@SponsorID";

		private const string NATIONALITYID_PARM = "@NationalityID";

		private const string PROBATION_PARM = "@Probation";

		private const string CONFIRMATIONDATE_PARM = "@ConfirmationDate";

		private const string RELIGIONID_PARM = "@ReligionID";

		private const string BLOODGROUP_PARM = "@BloodGroup";

		private const string QUALIFICATION_PARM = "@Qualification";

		private const string CONTRACTTYPE_PARM = "@ContractType";

		private const string PAYMENTMETHODID_PARM = "@PaymentMethodID";

		private const string BANKID_PARM = "@BankID";

		private const string ACCOUNTNUMBER_PARM = "@AccountNumber";

		private const string UID_PARM = "@UID";

		private const string VISANUMBER_PARM = "@VisaNumber";

		private const string ANNUALLEAVEDATE_PARM = "@AnnualLeaveDate";

		private const string RESUMEDDATE_PARM = "@ResumedDate";

		private const string NOTES_PARM = "@Notes";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string COMPANYDIVISIONID_PARM = "@CompanyDivisionID";

		private const string DEPARTMENTID_PARM = "@DepartmentID";

		private const string POSITIONID_PARM = "@PositionID";

		private const string REPORTTOID_PARM = "@ReportToID";

		private const string PAYPERIOD_PARM = "@PayPeriod";

		private const string GENDER_PARM = "@Gender";

		private const string LASTPAYDATE_PARM = "@LastPayDate";

		private const string MARITALSTATUS_PARM = "@MaritalStatus";

		private const string SPOUSENAME_PARM = "@SpouseName";

		private const string NATIONALID_PARM = "@NationalID";

		private const string STATUS_PARM = "@Status";

		private const string LASTREVISEDSALARYDATE_PARM = "@LastRevisedSalaryDate";

		private const string PRIMARYADDRESSID_PARM = "@PrimaryAddressID";

		private const string BASICSALARY_PARM = "@BasicSalary";

		private const string CURRENCY_PARM = "@CurrencyID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string EMPANALYSISID_PARM = "@EmpAnalysisID";

		private const string LABOURID_PARM = "@LabourID";

		private const string CALENDARID_PARM = "@CalendarID";

		private const string IBANNUMBER_PARM = "@IBAN";

		private const string SALARYREMARKS_PARM = "@SalaryRemarks";

		private const string EOSRULEID_PARM = "@EOSRuleID";

		private const string OVERTIMEID_PARM = "@OverTimeID";

		private const string MEDICALINSURANCEPROVIDERID_PARM = "@MedicalInsuranceProviderID";

		private const string MEDICALINSURANCECATEGORYID_PARM = "@MedicalInsuranceCategoryID";

		private const string MEDICALINSURANCENUMBER_PARM = "@MedicalInsuranceNumber";

		private const string MEDICALINSURANCEVALIDFROM_PARM = "@MedicalInsValidFrom";

		private const string MEDICALINSURANCEVALIDTO_PARM = "@MedicalInsValidTo";

		private const string FAMILYMEMBERSCOUNT_PARM = "@NumberOfDependants";

		private const string MEDICALINSURANCEAMOUNT_PARM = "@MedicalInsuranceAmount";

		private const string DESTINATIONID_PARM = "@DestinationID";

		private const string NUMBEROFTICKETS_PARM = "@NumberOfTickets";

		private const string TICKETAMOUNT_PARM = "@TicketAmount";

		private const string TICKETPERIOD_PARM = "@TicketPeriod";

		private const string TICKETREMARKS_PARM = "@TicketRemarks";

		private const string PAYTYPE_FIELD = "@PayType";

		private const string PAYROLLITEMID_FIELD = "@PayrollItemID";

		private const string AMOUNT_FIELD = "@Amount";

		private const string ROWINDEX_FIELD = "@RowIndex";

		private const string USERDEFINED1_PARM = "@UserDefined1";

		private const string USERDEFINED2_PARM = "@UserDefined2";

		private const string USERDEFINED3_PARM = "@UserDefined3";

		private const string USERDEFINED4_PARM = "@UserDefined4";

		private const string LEGALPOSITION_PARM = "@LegalPositionID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string RECRUITMENTCHANNEL_PARM = "@RecruitmentChannelID";

		private const string VISADESIGNATION_PARM = "@VisaDesignationID";

		public bool CheckConcurrency = true;

		private decimal minusLeaves;

		public decimal MinusLeaves
		{
			get
			{
				return minusLeaves;
			}
			set
			{
				minusLeaves = value;
			}
		}

		public Employees(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("LastName", "@LastName"), new FieldValue("FirstName", "@FirstName"), new FieldValue("MiddleName", "@MiddleName"), new FieldValue("BirthDate", "@BirthDate"), new FieldValue("NickName", "@NickName"), new FieldValue("JoiningDate", "@JoiningDate"), new FieldValue("GroupID", "@GroupID"), new FieldValue("GradeID", "@GradeID"), new FieldValue("DayOff", "@DayOff"), new FieldValue("OnVacation", "@OnVacation"), new FieldValue("BirthPlace", "@BirthPlace"), new FieldValue("SponsorID", "@SponsorID"), new FieldValue("NationalityID", "@NationalityID"), new FieldValue("Probation", "@Probation"), new FieldValue("ConfirmationDate", "@ConfirmationDate"), new FieldValue("ReligionID", "@ReligionID"), new FieldValue("BloodGroup", "@BloodGroup"), new FieldValue("Qualification", "@Qualification"), new FieldValue("ContractType", "@ContractType"), new FieldValue("AnnualLeaveDate", "@AnnualLeaveDate"), new FieldValue("ResumedDate", "@ResumedDate"), new FieldValue("Notes", "@Notes"), new FieldValue("LocationID", "@LocationID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyDivisionID", "@CompanyDivisionID"), new FieldValue("DepartmentID", "@DepartmentID"), new FieldValue("PositionID", "@PositionID"), new FieldValue("ReportToID", "@ReportToID"), new FieldValue("PayPeriod", "@PayPeriod"), new FieldValue("Gender", "@Gender"), new FieldValue("MaritalStatus", "@MaritalStatus"), new FieldValue("SpouseName", "@SpouseName"), new FieldValue("NationalID", "@NationalID"), new FieldValue("Status", "@Status"), new FieldValue("AccountID", "@AccountID"), new FieldValue("EmpAnalysisID", "@EmpAnalysisID"), new FieldValue("BankID", "@BankID"), new FieldValue("LabourID", "@LabourID"), new FieldValue("IBAN", "@IBAN"), new FieldValue("UID", "@UID"), new FieldValue("VisaNumber", "@VisaNumber"), new FieldValue("CalendarID", "@CalendarID"), new FieldValue("MedicalInsuranceProviderID", "@MedicalInsuranceProviderID"), new FieldValue("MedicalInsuranceCategoryID", "@MedicalInsuranceCategoryID"), new FieldValue("MedicalInsuranceNumber", "@MedicalInsuranceNumber"), new FieldValue("MedicalInsValidFrom", "@MedicalInsValidFrom"), new FieldValue("MedicalInsValidTo", "@MedicalInsValidTo"), new FieldValue("NumberOfDependants", "@NumberOfDependants"), new FieldValue("PrimaryAddressID", "@PrimaryAddressID"), new FieldValue("UserDefined1", "@UserDefined1"), new FieldValue("UserDefined2", "@UserDefined2"), new FieldValue("UserDefined3", "@UserDefined3"), new FieldValue("UserDefined4", "@UserDefined4"), new FieldValue("RecruitmentChannelID", "@RecruitmentChannelID"), new FieldValue("MedicalInsuranceAmount", "@MedicalInsuranceAmount"), new FieldValue("LegalPositionID", "@LegalPositionID"), new FieldValue("VisaDesignationID", "@VisaDesignationID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		public SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@LastName", SqlDbType.NVarChar);
			parameters.Add("@FirstName", SqlDbType.NVarChar);
			parameters.Add("@MiddleName", SqlDbType.NVarChar);
			parameters.Add("@BirthDate", SqlDbType.SmallDateTime);
			parameters.Add("@NickName", SqlDbType.NVarChar);
			parameters.Add("@JoiningDate", SqlDbType.SmallDateTime);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@GradeID", SqlDbType.NVarChar);
			parameters.Add("@DayOff", SqlDbType.TinyInt);
			parameters.Add("@OnVacation", SqlDbType.Bit);
			parameters.Add("@BirthPlace", SqlDbType.NVarChar);
			parameters.Add("@SponsorID", SqlDbType.NVarChar);
			parameters.Add("@NationalityID", SqlDbType.NVarChar);
			parameters.Add("@Probation", SqlDbType.SmallInt);
			parameters.Add("@ConfirmationDate", SqlDbType.SmallDateTime);
			parameters.Add("@ReligionID", SqlDbType.NVarChar);
			parameters.Add("@BloodGroup", SqlDbType.NVarChar);
			parameters.Add("@Qualification", SqlDbType.NVarChar);
			parameters.Add("@ContractType", SqlDbType.NVarChar);
			parameters.Add("@AnnualLeaveDate", SqlDbType.SmallDateTime);
			parameters.Add("@ResumedDate", SqlDbType.SmallDateTime);
			parameters.Add("@Notes", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyDivisionID", SqlDbType.NVarChar);
			parameters.Add("@DepartmentID", SqlDbType.NVarChar);
			parameters.Add("@PositionID", SqlDbType.NVarChar);
			parameters.Add("@ReportToID", SqlDbType.NVarChar);
			parameters.Add("@PayPeriod", SqlDbType.TinyInt);
			parameters.Add("@Gender", SqlDbType.Char);
			parameters.Add("@MaritalStatus", SqlDbType.TinyInt);
			parameters.Add("@SpouseName", SqlDbType.NVarChar);
			parameters.Add("@NationalID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@PrimaryAddressID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@EmpAnalysisID", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@LabourID", SqlDbType.NVarChar);
			parameters.Add("@UID", SqlDbType.NVarChar);
			parameters.Add("@VisaNumber", SqlDbType.NVarChar);
			parameters.Add("@IBAN", SqlDbType.NVarChar);
			parameters.Add("@CalendarID", SqlDbType.NVarChar);
			parameters.Add("@MedicalInsuranceProviderID", SqlDbType.NVarChar);
			parameters.Add("@MedicalInsuranceCategoryID", SqlDbType.NVarChar);
			parameters.Add("@MedicalInsuranceNumber", SqlDbType.NVarChar);
			parameters.Add("@MedicalInsValidFrom", SqlDbType.DateTime);
			parameters.Add("@MedicalInsValidTo", SqlDbType.DateTime);
			parameters.Add("@NumberOfDependants", SqlDbType.Int);
			parameters.Add("@UserDefined1", SqlDbType.NVarChar);
			parameters.Add("@UserDefined2", SqlDbType.NVarChar);
			parameters.Add("@UserDefined3", SqlDbType.NVarChar);
			parameters.Add("@UserDefined4", SqlDbType.NVarChar);
			parameters.Add("@RecruitmentChannelID", SqlDbType.NVarChar);
			parameters.Add("@VisaDesignationID", SqlDbType.NVarChar);
			parameters.Add("@MedicalInsuranceAmount", SqlDbType.Money);
			parameters.Add("@LegalPositionID", SqlDbType.NVarChar);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@LastName"].SourceColumn = "LastName";
			parameters["@FirstName"].SourceColumn = "FirstName";
			parameters["@MiddleName"].SourceColumn = "MiddleName";
			parameters["@BirthDate"].SourceColumn = "BirthDate";
			parameters["@NickName"].SourceColumn = "NickName";
			parameters["@JoiningDate"].SourceColumn = "JoiningDate";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@GradeID"].SourceColumn = "GradeID";
			parameters["@DayOff"].SourceColumn = "DayOff";
			parameters["@OnVacation"].SourceColumn = "OnVacation";
			parameters["@BirthPlace"].SourceColumn = "BirthPlace";
			parameters["@SponsorID"].SourceColumn = "SponsorID";
			parameters["@NationalityID"].SourceColumn = "NationalityID";
			parameters["@Probation"].SourceColumn = "Probation";
			parameters["@ConfirmationDate"].SourceColumn = "ConfirmationDate";
			parameters["@ReligionID"].SourceColumn = "ReligionID";
			parameters["@BloodGroup"].SourceColumn = "BloodGroup";
			parameters["@Qualification"].SourceColumn = "Qualification";
			parameters["@ContractType"].SourceColumn = "ContractType";
			parameters["@AnnualLeaveDate"].SourceColumn = "AnnualLeaveDate";
			parameters["@ResumedDate"].SourceColumn = "ResumedDate";
			parameters["@Notes"].SourceColumn = "Notes";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyDivisionID"].SourceColumn = "CompanyDivisionID";
			parameters["@DepartmentID"].SourceColumn = "DepartmentID";
			parameters["@PositionID"].SourceColumn = "PositionID";
			parameters["@ReportToID"].SourceColumn = "ReportToID";
			parameters["@PayPeriod"].SourceColumn = "PayPeriod";
			parameters["@Gender"].SourceColumn = "Gender";
			parameters["@MaritalStatus"].SourceColumn = "MaritalStatus";
			parameters["@SpouseName"].SourceColumn = "SpouseName";
			parameters["@NationalID"].SourceColumn = "NationalID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@PrimaryAddressID"].SourceColumn = "PrimaryAddressID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@EmpAnalysisID"].SourceColumn = "EmpAnalysisID";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@LabourID"].SourceColumn = "LabourID";
			parameters["@UID"].SourceColumn = "UID";
			parameters["@VisaNumber"].SourceColumn = "VisaNumber";
			parameters["@IBAN"].SourceColumn = "IBAN";
			parameters["@CalendarID"].SourceColumn = "CalendarID";
			parameters["@MedicalInsuranceProviderID"].SourceColumn = "MedicalInsuranceProviderID";
			parameters["@MedicalInsuranceCategoryID"].SourceColumn = "MedicalInsuranceCategoryID";
			parameters["@MedicalInsuranceNumber"].SourceColumn = "MedicalInsuranceNumber";
			parameters["@MedicalInsValidFrom"].SourceColumn = "MedicalInsValidFrom";
			parameters["@MedicalInsValidTo"].SourceColumn = "MedicalInsValidTo";
			parameters["@NumberOfDependants"].SourceColumn = "NumberOfDependants";
			parameters["@UserDefined1"].SourceColumn = "UserDefined1";
			parameters["@UserDefined2"].SourceColumn = "UserDefined2";
			parameters["@UserDefined3"].SourceColumn = "UserDefined3";
			parameters["@UserDefined4"].SourceColumn = "UserDefined4";
			parameters["@RecruitmentChannelID"].SourceColumn = "RecruitmentChannelID";
			parameters["@VisaDesignationID"].SourceColumn = "VisaDesignationID";
			parameters["@MedicalInsuranceAmount"].SourceColumn = "MedicalInsuranceAmount";
			parameters["@LegalPositionID"].SourceColumn = "LegalPositionID";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateEmployee(EmployeeData accountEmployeeData, bool isUpdate)
		{
			bool flag = true;
			DataSet dataSet = new DataSet();
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				if (isUpdate)
				{
					flag = Update(accountEmployeeData, "Employee", insertUpdateCommand);
				}
				else
				{
					flag = Insert(accountEmployeeData, "Employee", insertUpdateCommand);
					if (accountEmployeeData.Tables["Analysis"].Rows.Count > 0)
					{
						DataTable table = accountEmployeeData.Tables["Analysis"].Copy();
						dataSet.Tables.Add(table);
						flag &= new Analysis(base.DBConfig).InsertAutoAnalysis(dataSet);
					}
				}
				string text = accountEmployeeData.EmployeeTable.Rows[0]["EmployeeID"].ToString();
				if (flag && accountEmployeeData.EmployeeAddressTable.Rows.Count > 0)
				{
					string a = accountEmployeeData.EmployeeAddressTable.Rows[0]["AddressID"].ToString();
					if (isUpdate && a != "")
					{
						insertUpdateCommand = new EmployeeAddresses(base.DBConfig).GetInsertUpdateCommand(isUpdate);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Update(accountEmployeeData, "Employee_Address", insertUpdateCommand);
					}
					else
					{
						if (a == "")
						{
							accountEmployeeData.EmployeeAddressTable.Rows[0]["AddressID"] = "PRIMARY";
						}
						insertUpdateCommand = new EmployeeAddresses(base.DBConfig).GetInsertUpdateCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						if (accountEmployeeData.EmployeeAddressTable.Rows.Count > 0)
						{
							flag &= Insert(accountEmployeeData, "Employee_Address", insertUpdateCommand);
						}
					}
				}
				if (flag)
				{
					if (isUpdate)
					{
						AddActivityLog("Employee", text, ActivityTypes.Update, sqlTransaction);
					}
					else
					{
						AddActivityLog("Employee", text, ActivityTypes.Add, sqlTransaction);
					}
					UpdateTableRowInsertUpdateInfo("Employee", "EmployeeID", text, sqlTransaction, !isUpdate);
				}
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Employee, text.ToString(), "Employee", "EmployeeID", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		private string GetSalaryDetailInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("BasicSalary", "@BasicSalary"), new FieldValue("BankID", "@BankID"), new FieldValue("AccountNumber", "@AccountNumber"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("PaymentMethodID", "@PaymentMethodID"), new FieldValue("SalaryRemarks", "@SalaryRemarks"), new FieldValue("DestinationID", "@DestinationID"), new FieldValue("NumberOfTickets", "@NumberOfTickets"), new FieldValue("TicketAmount", "@TicketAmount"), new FieldValue("TicketPeriod", "@TicketPeriod"), new FieldValue("EOSRuleID", "@EOSRuleID"), new FieldValue("OverTimeID", "@OverTimeID"), new FieldValue("TicketRemarks", "@TicketRemarks"), new FieldValue("LastRevisedSalaryDate", "@LastRevisedSalaryDate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetSalaryDetailInsertUpdateCommand(bool isUpdate)
		{
			SqlCommand sqlCommand;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				sqlCommand = new SqlCommand(GetSalaryDetailInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			else
			{
				sqlCommand = new SqlCommand(GetSalaryDetailInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@BasicSalary", SqlDbType.Money);
			parameters.Add("@AccountNumber", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodID", SqlDbType.TinyInt);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@SalaryRemarks", SqlDbType.NVarChar);
			parameters.Add("@DestinationID", SqlDbType.NVarChar);
			parameters.Add("@NumberOfTickets", SqlDbType.TinyInt);
			parameters.Add("@TicketAmount", SqlDbType.Money);
			parameters.Add("@TicketPeriod", SqlDbType.SmallInt);
			parameters.Add("@TicketRemarks", SqlDbType.NVarChar);
			parameters.Add("@EOSRuleID", SqlDbType.NVarChar);
			parameters.Add("@OverTimeID", SqlDbType.NVarChar);
			parameters.Add("@LastRevisedSalaryDate", SqlDbType.SmallDateTime);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@BasicSalary"].SourceColumn = "BasicSalary";
			parameters["@AccountNumber"].SourceColumn = "AccountNumber";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@PaymentMethodID"].SourceColumn = "PaymentMethodID";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@SalaryRemarks"].SourceColumn = "SalaryRemarks";
			parameters["@DestinationID"].SourceColumn = "DestinationID";
			parameters["@NumberOfTickets"].SourceColumn = "NumberOfTickets";
			parameters["@TicketAmount"].SourceColumn = "TicketAmount";
			parameters["@TicketPeriod"].SourceColumn = "TicketPeriod";
			parameters["@TicketRemarks"].SourceColumn = "TicketRemarks";
			parameters["@EOSRuleID"].SourceColumn = "EOSRuleID";
			parameters["@OverTimeID"].SourceColumn = "OverTimeID";
			parameters["@LastRevisedSalaryDate"].SourceColumn = "LastRevisedSalaryDate";
			return sqlCommand;
		}

		public bool UpdateEmployeeSalaryDetails(DataSet data, bool isRevised)
		{
			bool flag = true;
			SqlCommand salaryDetailInsertUpdateCommand = GetSalaryDetailInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (salaryDetailInsertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				sqlTransaction = (salaryDetailInsertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				string text = data.Tables["Employee"].Rows[0]["EmployeeID"].ToString();
				flag = Update(data, "Employee", salaryDetailInsertUpdateCommand);
				string empty = string.Empty;
				string text2 = data.Tables["Employee"].Rows[0]["EmployeeID"].ToString();
				if (!flag)
				{
					return flag;
				}
				if (data.Tables.Contains("Employee_PayrollItem_Detail"))
				{
					if (isRevised)
					{
						DateTime dateTime = DateTime.Parse(data.Tables["Employee"].Rows[0]["PreviousRevisedSalaryDate"].ToString());
						empty = "INSERT INTO Employee_PayrollItem_History SELECT EPD.EmployeeID,EPD.PayrollItemID,EPD.PayType,EPD.StartDate,EPD.EndDate,'" + dateTime + "',EPD.Amount,EPD.LastAmount,EPD.RowIndex,\r\n                                    EPD.DateCreated,EPD.DateUpdated,EPD.CreatedBy,EPD.UpdatedBy FROM Employee_PayrollItem_Detail EPD\r\n                                    INNER JOIN Employee E ON EPD.EmployeeID=E.EmployeeID WHERE EPD.EmployeeID IN ('" + text + "') ";
						flag &= (ExecuteNonQuery(empty, sqlTransaction) >= 0);
					}
					salaryDetailInsertUpdateCommand = new EmployeePayrollItemDetails(base.DBConfig).GetInsertUpdateCommand(isUpdate: false);
					salaryDetailInsertUpdateCommand.Transaction = sqlTransaction;
					flag &= new EmployeePayrollItemDetails(base.DBConfig).DeleteEmployeePayrollItems(sqlTransaction, text2, isLog: false);
					if (data.Tables["Employee_PayrollItem_Detail"].Rows.Count > 0)
					{
						flag &= Insert(data, "Employee_PayrollItem_Detail", salaryDetailInsertUpdateCommand);
						AddActivityLog("Employee Payroll Item", text2, ActivityTypes.Update, null);
					}
				}
				if (data.Tables.Contains("Employee_Deduction_Detail"))
				{
					if (isRevised)
					{
						empty = "INSERT INTO Employee_Deduction_History SELECT * FROM Employee_Deduction_Detail WHERE EmployeeID IN ('" + text + "') AND PayType IN (2)";
						flag &= (ExecuteNonQuery(empty, sqlTransaction) >= 0);
					}
					salaryDetailInsertUpdateCommand = new EmployeeDeductionDetails(base.DBConfig).GetInsertUpdateCommand(isUpdate: false);
					salaryDetailInsertUpdateCommand.Transaction = sqlTransaction;
					flag &= new EmployeeDeductionDetails(base.DBConfig).DeleteEmployeeDeductions(sqlTransaction, text2, isLog: false);
					if (data.Tables["Employee_Deduction_Detail"].Rows.Count > 0)
					{
						flag = Insert(data, "Employee_Deduction_Detail", salaryDetailInsertUpdateCommand);
						AddActivityLog("Employee Deduction Item", text2, ActivityTypes.Update, null);
					}
				}
				if (!data.Tables.Contains("Employee_Benefit_Detail"))
				{
					return flag;
				}
				if (isRevised)
				{
					empty = "INSERT INTO Employee_Benefit_History SELECT * FROM Employee_Benefit_Detail WHERE EmployeeID IN ('" + text + "') ";
					flag &= (ExecuteNonQuery(empty, sqlTransaction) >= 0);
				}
				salaryDetailInsertUpdateCommand = new EmployeeBenefitDetails(base.DBConfig).GetInsertUpdateCommand(isUpdate: false);
				salaryDetailInsertUpdateCommand.Transaction = sqlTransaction;
				flag &= new EmployeeBenefitDetails(base.DBConfig).DeleteEmployeeBenefits(sqlTransaction, text2);
				if (data.Tables["Employee_Benefit_Detail"].Rows.Count <= 0)
				{
					return flag;
				}
				flag &= Insert(data, "Employee_Benefit_Detail", salaryDetailInsertUpdateCommand);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public DataSet GetEmployeeSalaryDetailsByEmployeeID(string employeeID)
		{
			string textCommand = "SELECT     EmployeeID,FirstName AS [First Name],MiddleName AS [Middle Name],LastName AS [Last Name],\r\n                                FirstName + ' ' +MiddleName + ' ' + LastName AS [Full Name],NickName [Nick Name], BankID, IBAN, BasicSalary, PaymentMethodID, AccountID, CurrencyID, \r\n                                SalaryRemarks,DestinationID,TicketRemarks,TicketPeriod,NumberOfTickets,TicketAmount,EOSRuleID,OvertimeID, LastRevisedSalaryDate\r\n                            FROM Employee WHERE EmployeeID='" + employeeID + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee", textCommand);
			dataSet.Merge(new EmployeePayrollItemDetails(base.DBConfig).GetEmployeePayrollItemsByEmployeeID(employeeID));
			dataSet.Merge(new EmployeeBenefitDetails(base.DBConfig).GetEmployeeBenefitsByEmployeeID(employeeID));
			dataSet.Relations.Add("Employee_PayrollItemDetail", new DataColumn[1]
			{
				dataSet.Tables["Employee"].Columns["EmployeeID"]
			}, new DataColumn[1]
			{
				dataSet.Tables["Employee_PayrollItem_Detail"].Columns["EmployeeID"]
			}, createConstraints: false);
			dataSet.Relations.Add("Employee_BenefitDetail", new DataColumn[1]
			{
				dataSet.Tables["Employee"].Columns["EmployeeID"]
			}, new DataColumn[1]
			{
				dataSet.Tables["Employee_Benefit_Detail"].Columns["EmployeeID"]
			}, createConstraints: false);
			return dataSet;
		}

		public EmployeeData GetEmployee()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee");
			EmployeeData employeeData = new EmployeeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeData, "Employee", sqlBuilder);
			return employeeData;
		}

		public bool DeleteEmployee(string employeeID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM UDF_Employee  WHERE EntityID = '" + employeeID + "'";
				flag = Delete(commandText, trans);
				commandText = "DELETE FROM Employee WHERE EmployeeID = '" + employeeID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Employee_Address WHERE EmployeeID = '" + employeeID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public EmployeeData GetEmployeeByID(string id)
		{
			EmployeeData employeeData = new EmployeeData();
			string textCommand = "SELECT E.*,CASE WHEN Photo IS NULL THEN 'False' ELSE 'True' END AS HasPhoto,\r\n                    CAST((DATEDIFF(m, JoiningDate, ISNULL(E.CancellationDate,GETDATE()))/12) as varchar) + ' Year & ' + CAST((DATEDIFF(m, JoiningDate,ISNULL(E.CancellationDate,GETDATE()))%12) as varchar) + ' Month' AS ServicePeriodMonth  FROM Employee E\r\n                            WHERE EmployeeID='" + id + "'";
			FillDataSet(employeeData, "Employee", textCommand);
			if (employeeData == null || employeeData.Tables.Count == 0 || employeeData.Tables[0].Rows.Count == 0)
			{
				return employeeData;
			}
			string text = "";
			text = employeeData.Tables["Employee"].Rows[0]["PrimaryAddressID"].ToString();
			if (text == "")
			{
				text = "PRIMARY";
			}
			textCommand = "SELECT * FROM Employee_Address\r\n                            WHERE EmployeeID='" + id + "' AND AddressID='" + text + "'";
			FillDataSet(employeeData, "Employee_Address", textCommand);
			textCommand = "SELECT * FROM UDF_Employee WHERE EntityID = '" + id + "'";
			FillDataSet(employeeData, "UDF", textCommand);
			return employeeData;
		}

		public DataSet GetEmployeeByFields(params string[] columns)
		{
			return GetEmployeeByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeByFields(string[] employeeID, params string[] columns)
		{
			return GetEmployeeByFields(employeeID, isInactive: true, columns);
		}

		public DataSet GetEmployeeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "EmployeeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeList()
		{
			return GetEmployeeList(includePhoto: false);
		}

		public DataSet GetEmployeeList(bool includePhoto)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "  SELECT DISTINCT CASE WHEN EMP.Photo IS NULL THEN 0 ELSE 1 END AS P,EMP.EmployeeID AS [Employee Code],EMP.FirstName AS [First Name],EMP.MiddleName AS [Middle Name],EMP.LastName AS [Last Name],\r\n                                EMP.FirstName + ' ' + EMP.MiddleName + ' ' + EMP.LastName AS [Full Name],EMP.NickName [Nick Name],EMP.BirthDate AS [Birth Date], DATEDIFF(Year,EMP.BirthDate,GetDate()) AS Age,\r\n                                EMP.JoiningDate  AS [Joining Date],GradeName AS Grade,SP.SponsorName AS Sponsor,EMP.NationalityID AS Nationality,ET.TypeName [Type], WL.WorkLocationName AS Location,\r\n                                DIV.DivisionName AS Division, DEP.DepartmentName AS Department,POS.PositionName AS Position,EGP.GroupName AS [Group],ET.TypeName AS [Class],\r\n                                EMP2.FirstName + ' ' + EMP2.LastName AS [Report To], EMP.Gender, EMP.Status,\r\n                                ADR.Mobile,ADR.Email,ADR.Phone1 [Phone],ADR.Phone2,\r\n                                ISNULL(EMP.OnVacation,'False') AS Vacation , (SELECT SUM(Amount) FROM Employee_PayrollItem_Detail PD WHERE  PD.EmployeeID = EMP.EmployeeID) AS Salary,\r\n\t\t\t\t\t\t\t    CAST((DATEDIFF(m, EMP.JoiningDate, ISNULL(EMP.CancellationDate,GETDATE()))/12) as varchar) + ' Year & ' + CAST((DATEDIFF(m, EMP.JoiningDate,ISNULL(EMP.CancellationDate,GETDATE()))%12) as varchar) + ' Month' AS ServicePeriod\r\n                                FROM Employee EMP\r\n                                LEFT OUTER JOIN Employee_Grade EG ON EG.GradeID = EMP.GradeID\r\n                                LEFT OUTER JOIN Sponsor SP ON SP.SponsorID = EMP.SponsorID\r\n                                LEFT OUTER JOIN Employee_Type ET ON ET.TypeID = EMP.ContractType\r\n                                LEFT OUTER JOIN Work_Location WL ON WL.WorkLocationID = EMP.LocationID\r\n                                LEFT OUTER JOIN Division DIV ON DIV.DivisionID = EMP.DivisionID\r\n                                LEFT OUTER JOIN Department DEP ON DEP.DepartmentID = EMP.DepartmentID\r\n                                LEFT OUTER JOIN Position POS ON POS.PositionID = EMP.PositionID\r\n                                LEFT OUTER JOIN Employee_Group EGP ON EGP.GroupID = EMP.GroupID\r\n                                LEFT OUTER JOIN Employee EMP2 ON EMP2.EmployeeID = EMP.ReportToID\r\n                                LEFT OUTER JOIN Employee_Address ADR ON ADR.EmployeeID = EMP.EmployeeID AND ADR.AddressID = EMP.PrimaryAddressID\r\n                                ORDER BY [Employee Code]   ";
				FillDataSet(dataSet, "Employee", textCommand);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["Employee Code"]
				};
				if (includePhoto)
				{
					textCommand = "SELECT EmployeeID [Employee Code],Photo FROM Employee WHERE Photo IS NOT NULL ";
					DataSet dataSet2 = new DataSet();
					FillDataSet(dataSet2, "Photos", textCommand);
					dataSet.Tables[0].Merge(dataSet2.Tables[0]);
					dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
					{
						dataSet2.Tables[0].Columns["Employee Code"]
					};
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EmployeeID [Code],ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],\r\n                            ISNULL(IsTerminated,'False') AS IsTerminated, PositionID,SponsorId FROM Employee  ORDER BY EmployeeID,FirstName";
			FillDataSet(dataSet, "Employee", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeFilterComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EmployeeID [Code],ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],\r\n                            ISNULL(IsTerminated,'False') AS IsTerminated,ReportToID,(SELECT ReportToID FROM Employee E2 WHERE E2.EmployeeID=E.ReportToID) AS SubReportToID\r\n                           FROM Employee E  ORDER BY EmployeeID,FirstName";
			FillDataSet(dataSet, "Employee", textCommand);
			return dataSet;
		}

		public string GetEmployeeAccountID(string sysDocID, string employeeID)
		{
			string exp = "  SELECT ISNULL(EMP.AccountID,ISNULL(ET.AccountID, LOC.EmployeeAccountID)) AS EmpAccountID FROM  Employee EMP \r\n                             LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                          LEFT OUTER JOIN Location LOC ON Loc.LocationID  = (SELECT LocationID FROM System_Document WHERE SysDocID = '" + sysDocID + "')\r\n                          WHERE EmployeeID = '" + employeeID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return "";
		}

		public DataSet GetEmployeeBriefInfo(string employeeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Employee.LocationID,Employee.DivisionID,Employee.DepartmentID,Work_Location.WorkLocationName,DivisionName,DepartmentName,Work_Location.WorkLocationID,\r\n                                IsTerminated, Employee.IsCancelled, PositionName,Employee.PositionID,Employee.GradeID,GradeName,\r\n                                FirstName + ' ' + LastName AS [EmployeeName], Employee.Gender, JoiningDate, ConfirmationDate, LabourID, \r\n                                (SELECT TOP 1 ActivityID FROM Employee_Activity WHERE ActivityType = 15 AND EmployeeID = Employee.EmployeeID) AS ActivityID,c.ContractType, C.WPNumber, c.WPExpiryDate, c.Photo,C.PassportNo FROM Employee\r\n                                LEFT  JOIN Work_Location ON Work_Location.WorkLocationID=Employee.LocationID\r\n                                LEFT OUTER JOIN Division ON Division.DivisionID=Employee.DivisionID\r\n                                LEFT OUTER JOIN Department ON Department.DepartmentID=Employee.DepartmentID\r\n                                LEFT OUTER JOIN Position ON Position.PositionID=Employee.PositionID\r\n                                LEFT OUTER JOIN Employee_Grade EG ON EG.GradeID=Employee.GradeID\r\n                                LEFT OUTER JOIN Candidate C ON C.EmployeeNo=Employee.EmployeeID\r\n                               WHERE Employee.EmployeeID='" + employeeID + "'";
			FillDataSet(dataSet, "Employee", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeBriefInfoAbsconding(string employeeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Employee.LocationID,Employee.DivisionID,Employee.DepartmentID,Work_Location.WorkLocationName,DivisionName,DepartmentName,Work_Location.WorkLocationID,\r\n                                IsTerminated, Employee.IsCancelled, PositionName,Employee.PositionID,Employee.GradeID,GradeName,\r\n                                FirstName + ' ' + LastName AS [EmployeeName], Employee.Gender, JoiningDate, ConfirmationDate, LabourID, \r\n                                (SELECT TOP 1 ActivityID FROM Employee_Activity WHERE ActivityType = 17 AND EmployeeID = Employee.EmployeeID) AS ActivityID,c.ContractType, C.WPNumber, c.WPExpiryDate, Employee.Photo,C.PassportNo FROM Employee\r\n                                LEFT  JOIN Work_Location ON Work_Location.WorkLocationID=Employee.LocationID\r\n                                LEFT OUTER JOIN Division ON Division.DivisionID=Employee.DivisionID\r\n                                LEFT OUTER JOIN Department ON Department.DepartmentID=Employee.DepartmentID\r\n                                LEFT OUTER JOIN Position ON Position.PositionID=Employee.PositionID\r\n                                LEFT OUTER JOIN Employee_Grade EG ON EG.GradeID=Employee.GradeID\r\n                                LEFT OUTER JOIN Candidate C ON C.EmployeeNo=Employee.EmployeeID\r\n                               WHERE Employee.EmployeeID='" + employeeID + "'";
			FillDataSet(dataSet, "Employee", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeLeaveInfo(string employeeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ISNULL((DATEDIFF(day, E.JoiningDate, GETDATE()) / 365) * 30, 0) [Total Leaves], \r\n                            SUM(ISNULL(DATEDIFF(day, ELR.StartDate, ELR.EndDate) + 1, 0)) [Leaves Taken],\r\n\t\t                    ISNULL((DATEDIFF(day, E.JoiningDate, GETDATE()) / 365) * 30, 0) - \r\n\t\t                    SUM(ISNULL(DATEDIFF(day, ELR.StartDate, ELR.EndDate) + 1, 0)) [Leaves Eligible], \r\n\t                        ISNULL((SELECT SUM(LeaveEncash) FROM Employee_Leave_Encashment ELE INNER JOIN Employee_Activity EA ON ELE.ActivityID = EA.ActivityID WHERE EmployeeID='" + employeeID + "'), 0) [Leaves Encash]\r\n\t\t                    FROM Employee E INNER JOIN Employee_Activity EA ON E.EmployeeID = EA.EmployeeID\r\n                            INNER JOIN Employee_Leave_Request ELR ON EA.ActivityID = ELR.ActivityID\r\n                            WHERE ISNULL(ELR.IsApproved, 0) = 1\r\n                            AND LeaveTypeID = (SELECT TOP 1 LeaveTypeID FROM Leave_Type WHERE IsAnnual=1)  AND ISNULL(ELR.IsApproved, 0) = 1\r\n                            AND E.EmployeeID='" + employeeID + "' GROUP BY E.JoiningDate";
			FillDataSet(dataSet, "Employee", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeBalanceSummary(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showZeroBalance, DateTime to, string EmployeeIDs)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT EJ.EmployeeID,FirstName + ' ' + LastName AS [EmployeeName],\r\n                            SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) AS Balance FROM Employee_Journal EJ\r\n                            INNER JOIN Employee E ON EJ.EmployeeID=E.EmployeeID WHERE ISNULL(IsVoid,'False')='False'\r\n                            AND EJ.JournalDate<='" + text + "' ";
			if (EmployeeIDs != "")
			{
				str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				str = str + " AND EJ.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND EJ.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (!showZeroBalance)
			{
				str = str + " AND (SELECT SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) FROM Employee_Journal EJ2 WHERE EJ.EmployeeID=EJ2.EmployeeID AND EJ2.JournalDate<='" + text + "')<>0 ";
			}
			str += " GROUP BY EJ.EmployeeID, FirstName, LastName";
			str += " ORDER BY EJ.EmployeeID, EmployeeName";
			FillDataSet(dataSet, "Employee", str);
			return dataSet;
		}

		public DataSet GetEmployeeBalanceDetailReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showZeroBalance, string EmployeeIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT DISTINCT EJ.EmployeeID ,FirstName + ' ' + LastName AS [EmployeeName] ,\r\n                        ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM Employee_Journal ARJ2 \r\n                         WHERE EJ.EmployeeID=ARJ2.EmployeeID AND ARJ2.JournalDate<'" + text + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [OpeningBalance],\r\n                        ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM Employee_Journal ARJ2 \r\n                         WHERE EJ.EmployeeID=ARJ2.EmployeeID AND ARJ2.JournalDate<='" + text2 + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [EndingBalance]\r\n                        FROM Employee_Journal EJ INNER JOIN Employee E ON EJ.EmployeeID=E.EmployeeID WHERE 1=1";
			if (!showZeroBalance)
			{
				str = str + " AND JournalDate < '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False' ";
			}
			if (EmployeeIDs != "")
			{
				str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				str = str + " AND EJ.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND EJ.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			str += " ORDER BY EJ.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee", str);
			DataSet dataSet2 = new DataSet();
			str = "SELECT Employee_Journal.EmployeeID ,SysDocType,'' AS [DocType], Employee_Journal.SysDocID  ,VoucherID ,JournalDate,\r\n                            ChequeNumber ,ChequeDate ,Description,Reference,Employee_Journal.CurrencyID,CurrencyRate,DebitFC,CreditFC, Debit,Credit FROM Employee_Journal \r\n\r\n\t\t\t\t\t\t\tLEFT JOIn Employee E On Employee_Journal.EmployeeID=E.EmployeeID\r\n\r\n                            LEFT OUTER JOIN System_Document SD ON Employee_Journal.SysDocID=SD.SysDocID WHERE \r\n                            JournalDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromEmployee != "")
			{
				str = str + " AND Employee_Journal.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND Employee_Journal.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			str += " ORDER BY JournalDate";
			FillDataSet(dataSet2, "Employee_Journal", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Balance Detail", dataSet.Tables["Employee"].Columns["EmployeeID"], dataSet.Tables["Employee_Journal"].Columns["EmployeeID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetEmployeeGraduityEligibilityReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, DateTime asOfDate, string EmployeeIDs)
		{
			CommonLib.ToSqlDateTimeString(asOfDate);
			string str = "SELECT E.EmployeeID, E.FirstName, E.LastName, JoiningDate, ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2)  AS Tenure,  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 1, 2)  AS Tenure2,SUM(CASE WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= 0 AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) < 1 THEN 0  WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= 1 AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) <=5 THEN ((Amount / 30) * 21) *  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) > 5   THEN ((Amount / 30) * 21 * 5)+  (((ROUND((CAST(DATEDIFF(DAY, JoiningDate, '" + asOfDate + "') AS FLOAT) ), 2)+1)/365)-5)*30*((Amount / 30))ELSE 0.00 END ) AS Graduity FROM Employee E INNER JOIN Employee_PayrollItem_Detail EPD ON E.EmployeeID = EPD.EmployeeID INNER JOIN PayrollItem PI ON EPD.PayrollItemID = PI.PayrollItemID WHERE PI.InServiceBenefit = 1 AND ISNULL(E.IsTerminated, 0) = 0 ";
			if (EmployeeIDs != "")
			{
				str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			str += " GROUP BY E.EmployeeID, E.FirstName, E.LastName, JoiningDate ORDER BY E.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee", str);
			return dataSet;
		}

		public DataSet GetEmployeeHistoryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, DateTime asOfDate, string EmployeeIDs)
		{
			CommonLib.ToSqlDateTimeString(asOfDate);
			string str = "SELECT E.EmployeeID, E.FirstName, E.LastName, E.SponsorID, S.SponsorName,JoiningDate, (SELECT EC.LastWorkingDate FROM Employee_Cancellation EC INNER JOIN  Employee_Activity EA ON\r\n                            EC.ActivityID=EA.ActivityID AND EA.EmployeeID=E.EmployeeID) AS [LastWorkingDate],CAST(DATEDIFF(YEAR, JoiningDate, '" + asOfDate + "') AS VARCHAR(3)) + '.' + CAST(DATEDIFF(MONTH, JoiningDate,'" + asOfDate + "') % 12 AS VARCHAR(2)) AS Tenure FROM Employee E LEFT JOIN Sponsor S ON E.SponsorID=S.SponsorID WHERE ISNULL(E.IsTerminated, 0) = 0 ";
			if (EmployeeIDs != "")
			{
				str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			str += " ORDER BY E.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee", str);
			return dataSet;
		}

		public DataSet GetEmployeeListReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT E.*,S.SponsorName, EP.*, CASE Status \r\n                                WHEN 1 THEN 'A' ELSE 'T' END AS [Employee Status] \r\n                                FROM Employee E LEFT OUTER JOIN  Position EP ON E.PositionID=EP.PositionID\r\n                                LEFT JOIN Sponsor S ON E.SponsorID=S.SponsorID\r\n                                WHERE 1=1 ";
			if (EmployeeIDs != "")
			{
				text = text + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text = text + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text = text + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text = text + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text = text + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text = text + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text = text + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text = text + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text = text + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text = text + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text = text + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text = text + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text = text + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text = text + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				text = text + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				text = text + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text = text + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text = text + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text = text + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Status,1) = 1";
			}
			FillDataSet(dataSet, "Employees", text);
			return dataSet;
		}

		public DataSet GetEmployeeLeaveReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromLeave, string toLeave, LeaveApprovalType approvalType)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			string text3 = "SELECT E.EmployeeID, E.FirstName + ' ' + E.LastName AS Name, ELR.LeaveTypeID, ISNULL(EA2.Note,EA.Note) AS Note,EA.Reason, DATEDIFF (Day, ELR.StartDate, ELR.EndDate) + 1 as NumberOfDays,\r\n\t\t                    ELR.StartDate, ELR.EndDate, ELR.LastWorkingDate,LT.LeaveTypeName,ELR.ApproveDate,EA2.TransactionDate as ResumeDate,\r\n                            (CASE ELR.IsApproved WHEN 1 THEN 'Approved' ELSE 'Not Approved' END) AS Status FROM Employee E\r\n                            INNER JOIN Employee_Activity EA ON E.EmployeeID = EA.EmployeeID\r\n                            INNER JOIN Employee_Leave_Request ELR ON EA.ActivityID = ELR.ActivityID\r\n                            INNER JOIN Leave_Type LT ON ELR.LeaveTypeID=LT.LeaveTypeID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Employee_Resumption ER ON ER.LeaveID=ELR.ActivityID\r\n                            LEFT OUTER JOIN Employee_Activity EA2 ON EA2.ActivityID =ER.ActivityID\r\n                            WHERE (StartDate >= '" + text + "' AND EndDate <= '" + text2 + "')";
			if (fromEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID>='" + fromEmployee + "'";
			}
			if (toEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID<='" + toEmployee + "'";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID>='" + fromDepartment + "'";
			}
			if (toDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID<='" + toDepartment + "'";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND E.LocationID>='" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				text3 = text3 + " AND E.LocationID<='" + toLocation + "'";
			}
			if (fromLeave != "")
			{
				text3 = text3 + " AND ELR.LeaveTypeID >='" + fromLeave + "'";
			}
			if (toLocation != "")
			{
				text3 = text3 + " AND ELR.LeaveTypeID <='" + toLeave + "'";
			}
			if (approvalType != 0)
			{
				text3 = text3 + " AND ISNULL(ELR.IsApproved, 0) = " + ((approvalType == LeaveApprovalType.Approved) ? 1 : 0);
			}
			FillDataSet(dataSet, "Employees", text3);
			text3 = "SELECT EA.EmployeeID,EA.TransactionDate from Employee_Resumption ER inner join Employee_Activity EA on ER.ActivityID=EA.ActivityID\r\n                     inner join Employee E on E.EmployeeID=EA.EmployeeID inner join\r\n                 Employee_Leave_Request ELR ON ER.ActivityID = ELR.ActivityID\r\n                              WHERE (ELR.StartDate >= '" + text + "' AND ELR.EndDate <= '" + text2 + "')";
			if (fromEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID>='" + fromEmployee + "'";
			}
			if (toEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID<='" + toEmployee + "'";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID>='" + fromDepartment + "'";
			}
			if (toDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID<='" + toDepartment + "'";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND E.LocationID>='" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				text3 = text3 + " AND E.LocationID<='" + toLocation + "'";
			}
			FillDataSet(dataSet2, "EmployeesResumption", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Employee_Activity", dataSet.Tables["Employees"].Columns["EmployeeID"], dataSet.Tables["EmployeesResumption"].Columns["EmployeeID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetEmployeeLeaveReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string fromLeave, string toLeave, LeaveApprovalType approvalType, string EmployeeIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			string text3 = "SELECT E.EmployeeID, E.FirstName + ' ' + E.LastName AS Name,E.SponsorID,S.SponsorName, ELR.LeaveTypeID, ISNULL(EA2.Note,EA.Note) AS Note,EA.Reason, DATEDIFF (Day, ELR.StartDate, ELR.EndDate) + 1 as NumberOfDays,\r\n\t\t                    ELR.StartDate, ELR.EndDate, ELR.LastWorkingDate,LT.LeaveTypeName,ELR.ApproveDate,ELR.ApprovedBy,ELR.TravellingDate,EA2.TransactionDate as ResumeDate,ELR.ActualLeaveDays,\r\n                            (CASE ELR.IsApproved WHEN 1 THEN 'Approved' ELSE 'Not Approved' END) AS Status FROM Employee E\r\n                            INNER JOIN Employee_Activity EA ON E.EmployeeID = EA.EmployeeID\r\n                            INNER JOIN Employee_Leave_Request ELR ON EA.ActivityID = ELR.ActivityID\r\n                            INNER JOIN Leave_Type LT ON ELR.LeaveTypeID=LT.LeaveTypeID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Employee_Resumption ER ON ER.LeaveID=ELR.ActivityID\r\n                            LEFT OUTER JOIN Employee_Activity EA2 ON EA2.ActivityID =ER.ActivityID\r\n\t\t\t\t\t\t\tLEFT JOIN Sponsor S On E.SponsorID=S.SponsorID\r\n                            WHERE (StartDate >= '" + text + "' AND EndDate <= '" + text2 + "')";
			if (EmployeeIDs != "")
			{
				text3 = text3 + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text3 = text3 + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text3 = text3 + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text3 = text3 + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text3 = text3 + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text3 = text3 + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text3 = text3 + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text3 = text3 + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text3 = text3 + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text3 = text3 + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text3 = text3 + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text3 = text3 + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				text3 = text3 + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				text3 = text3 + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text3 = text3 + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text3 = text3 + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text3 = text3 + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (fromLeave != "")
			{
				text3 = text3 + " AND ELR.LeaveTypeID >='" + fromLeave + "'";
			}
			if (approvalType != 0)
			{
				text3 = text3 + " AND ISNULL(ELR.IsApproved, 0) = " + ((approvalType == LeaveApprovalType.Approved) ? 1 : 0);
			}
			text3 = text3 + "UNION   SELECT E.EmployeeID, E.FirstName + ' ' + E.LastName AS Name,E.SponsorID,S.SponsorName,EPB.LeaveTypeID,EPB.Description AS Note,'' AS Reason, DATEDIFF (Day, EPB.LeaveStartDate, EPB.LeaveEndDate) + 1 as NumberOfDays, EPB.LeaveStartDate,\r\n                           EPB.LeaveEndDate,EPB.LeaveStartDate-1 AS LastWorkingDate, LT.LeaveTypeName,'' AS ApproveDate,'' AS ApprovedBy,'' AS TravellingDate,'' AS Resumedate,0 AS ActualLeaveDays, '' AS [Status] \r\n\t\t\t\t\t\t   FROM Employee E INNER JOIN Opening_Balance_Leave_Detail EPB ON E.EmployeeID = EPB.EmployeeID INNER JOIN Leave_Type LT ON EPB.LeaveTypeID=LT.LeaveTypeID\r\n\t\t\t\t\t\t   LEFT JOIN Sponsor S ON E.SponsorID=S.SponsorID\r\n                           WHERE (EPB.LeaveStartDate >= '" + text + "' AND EPB.LeaveEndDate <= '" + text2 + "')";
			if (EmployeeIDs != "")
			{
				text3 = text3 + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text3 = text3 + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text3 = text3 + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text3 = text3 + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text3 = text3 + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text3 = text3 + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text3 = text3 + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text3 = text3 + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text3 = text3 + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text3 = text3 + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text3 = text3 + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text3 = text3 + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				text3 = text3 + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				text3 = text3 + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text3 = text3 + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text3 = text3 + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text3 = text3 + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (fromLeave != "")
			{
				text3 = text3 + " AND EPB.LeaveTypeID >='" + fromLeave + "'";
			}
			text3 += " ORDER BY  E.EmployeeID,ELR.StartDate ASC ";
			FillDataSet(dataSet, "Employees", text3);
			text3 = "SELECT EA.EmployeeID,EA.TransactionDate from Employee_Resumption ER inner join Employee_Activity EA on ER.ActivityID=EA.ActivityID\r\n                     inner join Employee E on E.EmployeeID=EA.EmployeeID inner join\r\n                 Employee_Leave_Request ELR ON ER.ActivityID = ELR.ActivityID\r\n                              WHERE (ELR.StartDate >= '" + text + "' AND ELR.EndDate <= '" + text2 + "')";
			if (EmployeeIDs != "")
			{
				text3 = text3 + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text3 = text3 + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text3 = text3 + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text3 = text3 + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text3 = text3 + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text3 = text3 + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text3 = text3 + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text3 = text3 + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text3 = text3 + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text3 = text3 + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text3 = text3 + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text3 = text3 + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text3 = text3 + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				text3 = text3 + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				text3 = text3 + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text3 = text3 + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text3 = text3 + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text3 = text3 + " AND E.AccountID <='" + toAccount + "' ";
			}
			FillDataSet(dataSet2, "EmployeesResumption", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Employee_Activity", dataSet.Tables["Employees"].Columns["EmployeeID"], dataSet.Tables["EmployeesResumption"].Columns["EmployeeID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetEmployeeProfileReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT E.EmployeeID ,FirstName,MiddleName,LastName, NickName, BirthDate,LocationID,PositionName,Photo, BloodGroup,\r\n                            CASE Status WHEN 1 THEN 'A' WHEN  2 THEN 'Terminated' WHEN  3 THEN 'Resigned'  WHEN  4 THEN 'Absconded' WHEN  5 THEN 'Hold' END  AS [Status],\r\n                            CAST((DATEDIFF(m, BirthDate, GETDATE())/12) as varchar) AS Age, JoiningDate,E.LabourID,E.UID,E.VisaNumber,E.NationalID,\r\n                             CASE WHEN E.PaymentMethodID = 1 THEN 'Cash' WHEN E.PaymentMethodID = 2 THEN 'Cheque' WHEN E.PaymentMethodID = 3 THEN 'Bank Transfer' ELSE 'NIL' END AS [Payment Mode] , \r\n                            CAST((DATEDIFF(m, JoiningDate, GETDATE())/12) as varchar) + ' Year & ' + CAST((DATEDIFF(m, JoiningDate, GETDATE())%12) as varchar) + ' Month' AS ServicePeriodMonth,\r\n                            TerminationDate,GradeName,ISNULL(OnVacation,'False') AS OnVacation,SponsorName,E.NationalityID,ReligionName,\r\n                            (SELECT ED.DocumentNumber FROM Employee_Document ED WHERE ED.DocumentTypeID='PP' AND ED.EmployeeID=E.EmployeeID ) AS [Passport],\r\n                            CAST((DATEDIFF(m, ISNULL(ISNULL(ResumedDate,AnnualLeaveDate),JoiningDate), GETDATE())/12) as varchar) + ' Year & ' + \r\n                            CAST((DATEDIFF(m, ISNULL(ISNULL(ResumedDate,AnnualLeaveDate),JoiningDate), GETDATE())%12) as varchar) + ' Month' AS CurrentServicePeriod,\r\n                            AnnualLeaveDate,ResumedDate,Notes,LocationID,Division.DivisionName,DepartmentName,PositionName,GroupName,Gender,(SELECT P.PositionName FROM Position P WHERE P.PositionID=E.PositionID) AS [VisaDesignation],\r\n                            CASE MaritalStatus WHEN 1 THEN 'NA' WHEN 2 THEN 'Single' WHEN 3 THEN 'Married' WHEN 4 THEN 'Divorced' WHEN 5 THEN 'Widow' END AS MaritalStatus,\r\n                            (SELECT TOP 1 EndDate from Employee_Leave_Request WHERE ActivityID IN (SELECT ActivityID FROM Employee_Activity WHERE EmployeeID>='" + fromEmployee + "' AND E.EmployeeID<='" + toEmployee + "')  \r\n                            AND LeaveTypeID IN (SELECT LeaveTypeID FROM Leave_Type WHERE IsAnnual=1) AND IsApproved=1 ) AS EndDate,\r\n                            (SELECT TOP 1 ResumptionDate from Employee_Leave_Request WHERE ActivityID IN (SELECT ActivityID FROM Employee_Activity WHERE EmployeeID>='" + fromEmployee + "' AND E.EmployeeID<='" + toEmployee + "')   \r\n                            AND LeaveTypeID IN (SELECT LeaveTypeID FROM Leave_Type WHERE IsAnnual=1) AND IsApproved=1 ) AS ResumptionDate,\r\n                            Phone1,Mobile,Email,PostalCode,Fax, EA.Address1, EA.Address2, EA.Address3, EA.AddressPrintFormat, EA.City, EA.Comment, EA.Country, EA.State,NA.NationalityName,CD.DivisionName AS [Company Division]\r\n                            FROM Employee E LEFT OUTER JOIN  Position EP ON E.PositionID=EP.PositionID\r\n                            LEFT OUTER JOIN Employee_Grade Grade ON Grade.GradeID=E.GradeID\r\n                            LEFT OUTER JOIN Sponsor ON Sponsor.SponsorID=E.SponsorID\r\n                            LEFT OUTER JOIN Religion ON Religion.ReligionID=E.ReligionID\r\n                            LEFT OUTER JOIN Division ON Division.DivisionID=E.DivisionID\r\n                            LEFT OUTER JOIN Department ON Department.DepartmentID=E.DepartmentID\r\n                            LEFT OUTER JOIN Employee_Group EG ON EG.GroupID=E.GroupID\r\n                            LEFT OUTER JOIN Nationality NA ON NA.NationalityID=E.NationalityID\r\n                            LEFT OUTER JOIN Company_Division CD ON CD.DivisionID=E.CompanyDivisionID\r\n                            LEFT OUTER JOIN Employee_Address EA ON EA.EmployeeID=E.EmployeeID AND AddressID='PRIMARY'\r\n                            WHERE 1=1 ";
			if (fromEmployee != "")
			{
				text = text + " AND E.EmployeeID>='" + fromEmployee + "'";
			}
			if (toEmployee != "")
			{
				text = text + " AND E.EmployeeID<='" + toEmployee + "'";
			}
			if (fromDepartment != "")
			{
				text = text + " AND E.DepartmentID>='" + fromDepartment + "'";
			}
			if (toDepartment != "")
			{
				text = text + " AND E.DepartmentID<='" + toDepartment + "'";
			}
			if (fromLocation != "")
			{
				text = text + " AND E.LocationID>='" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				text = text + " AND E.LocationID<='" + toLocation + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Status,1) = 1 ";
			}
			FillDataSet(dataSet, "Employees", text);
			text = "SELECT * FROM UDF_Employee WHERE 1=1 ";
			if (fromEmployee != "")
			{
				text = text + " AND EntityID >='" + fromEmployee + "'";
			}
			if (toEmployee != "")
			{
				text = text + " AND EntityID <='" + toEmployee + "'";
			}
			FillDataSet(dataSet, "UDF", text);
			text = "SELECT * FROM Employee_PayrollItem_Detail WHERE Amount>0  ";
			if (fromEmployee != "")
			{
				text = text + " AND EmployeeID >='" + fromEmployee + "'";
			}
			if (toEmployee != "")
			{
				text = text + " AND EmployeeID <='" + toEmployee + "'";
			}
			FillDataSet(dataSet, "SalaryData", text);
			return dataSet;
		}

		public DataSet GetEmployeeProfileReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT E.EmployeeID ,FirstName,MiddleName,LastName, NickName, BirthDate,LocationID,PositionName,Photo, BloodGroup,\r\n                            CASE Status WHEN 1 THEN 'A' WHEN  2 THEN 'Terminated' WHEN  3 THEN 'Resigned'  WHEN  4 THEN 'Absconded' WHEN  5 THEN 'Hold' END  AS [Status],CAST((DATEDIFF(m, BirthDate, GETDATE())/12) as varchar) AS Age, JoiningDate,\r\n                            CAST((DATEDIFF(m, JoiningDate, GETDATE())/12) as varchar) + ' Year & ' + CAST((DATEDIFF(m, JoiningDate, GETDATE())%12) as varchar) + ' Month' AS ServicePeriodMonth,\r\n                            TerminationDate,GradeName,ISNULL(OnVacation,'False') AS OnVacation,SponsorName,E.NationalityID,NA.NationalityName,ReligionName,\r\n                            CAST((DATEDIFF(m, ISNULL(ISNULL(ResumedDate,AnnualLeaveDate),JoiningDate), GETDATE())/12) as varchar) + ' Year & ' + CAST((DATEDIFF(m, ISNULL(ISNULL(ResumedDate,AnnualLeaveDate),JoiningDate), GETDATE())%12) as varchar) + ' Month' AS CurrentServicePeriod,\r\n                            AnnualLeaveDate,ResumedDate,Notes,LocationID,DivisionName,DepartmentName,PositionName,GroupName,Gender,\r\n                            CASE MaritalStatus WHEN 1 THEN 'NA' WHEN 2 THEN 'Single' WHEN 3 THEN 'Married' WHEN 4 THEN 'Divorced' WHEN 5 THEN 'Widow' END AS MaritalStatus,\r\n                            (SELECT TOP 1 EndDate from Employee_Leave_Request WHERE ActivityID IN (SELECT ActivityID FROM Employee_Activity WHERE EmployeeID>='" + fromEmployee + "' AND E.EmployeeID<='" + toEmployee + "')  \r\n                            AND LeaveTypeID IN (SELECT LeaveTypeID FROM Leave_Type WHERE IsAnnual=1) AND IsApproved=1 ) AS EndDate,\r\n                            (SELECT TOP 1 ResumptionDate from Employee_Leave_Request WHERE ActivityID IN (SELECT ActivityID FROM Employee_Activity WHERE EmployeeID>='" + fromEmployee + "' AND E.EmployeeID<='" + toEmployee + "')   \r\n                            AND LeaveTypeID IN (SELECT LeaveTypeID FROM Leave_Type WHERE IsAnnual=1) AND IsApproved=1 ) AS ResumptionDate,\r\n                            Phone1,Mobile,Email,PostalCode,Fax\r\n                            FROM Employee E LEFT OUTER JOIN  Position EP ON E.PositionID=EP.PositionID\r\n                            LEFT OUTER JOIN Employee_Grade Grade ON Grade.GradeID=E.GradeID\r\n                            LEFT OUTER JOIN Sponsor ON Sponsor.SponsorID=E.SponsorID\r\n                            LEFT OUTER JOIN Religion ON Religion.ReligionID=E.ReligionID\r\n                            LEFT OUTER JOIN Division ON Division.DivisionID=E.DivisionID\r\n                            LEFT OUTER JOIN Department ON Department.DepartmentID=E.DepartmentID\r\n                            LEFT OUTER JOIN Employee_Group EG ON EG.GroupID=E.GroupID\r\n                            LEFT OUTER JOIN Nationality NA ON E.NationalityID=NA.NationalityID\r\n                            LEFT OUTER JOIN Employee_Address EA ON EA.EmployeeID=E.EmployeeID AND AddressID='PRIMARY'\r\n                            WHERE 1=1 ";
			if (EmployeeIDs != "")
			{
				text = text + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text = text + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text = text + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text = text + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text = text + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text = text + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text = text + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text = text + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text = text + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text = text + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text = text + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text = text + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text = text + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text = text + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				text = text + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				text = text + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text = text + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text = text + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text = text + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Status,1) = 1 ";
			}
			FillDataSet(dataSet, "Employees", text);
			return dataSet;
		}

		public DataSet GetEmployeeActivityReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = "SELECT DISTINCT E.EmployeeID,FirstName + ' ' + LastName AS [EmployeeName]\r\n                            FROM Employee E INNER JOIN Employee_Activity EA\r\n                            ON E.EmployeeID=EA.EmployeeID\r\n                            WHERE 1=1 ";
			if (EmployeeIDs != "")
			{
				text = text + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text = text + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text = text + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text = text + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text = text + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text = text + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text = text + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text = text + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text = text + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text = text + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text = text + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text = text + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text = text + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text = text + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				text = text + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				text = text + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text = text + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text = text + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text = text + " AND E.AccountID <='" + toAccount + "' ";
			}
			FillDataSet(dataSet, "Employee", text);
			text = "SELECT ActivityID,EA.EmployeeID,FirstName + ' ' + LastName AS [Employee Name],TransactionDate,ActivityType,\r\n                            Reason,Reference,Note \r\n                            FROM Employee_Activity EA\r\n                            INNER JOIN Employee E ON E.EmployeeID=EA.EmployeeID\r\n                            WHERE 1=1 ";
			if (fromEmployee != "")
			{
				text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text = text + " AND E.EmployeeID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text = text + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text = text + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text = text + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text = text + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text = text + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text = text + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text = text + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text = text + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text = text + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text = text + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text = text + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text = text + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				text = text + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				text = text + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text = text + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text = text + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text = text + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Status,1) = 1 ";
			}
			FillDataSet(dataSet, "Activity", text);
			dataSet.Relations.Add("Employee_Activity", dataSet.Tables["Employee"].Columns["EmployeeID"], dataSet.Tables["Activity"].Columns["EmployeeID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetEmployeeSalaryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime startDate, DateTime endDate, int periodYear, int periodMonth)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0));
				string text2 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 11, 59, 59));
				string text3 = "";
				_ = string.Empty;
				text3 = "SELECT SSD.EmployeeID, FirstName + ' ' + LastName [Employee Name], SS.Month [MonthVal],\r\n\t                        CASE [Month] WHEN 1 THEN 'January' WHEN 2 THEN 'February' WHEN 3 THEN 'March' WHEN 4 THEN 'April' \r\n\t\t                        WHEN 5 THEN 'May' WHEN 6 THEN 'June' WHEN 7 THEN 'July' WHEN 8 THEN 'August' \r\n\t\t                        WHEN 9 THEN 'September' WHEN 10 THEN 'October' WHEN 11 THEN 'Nov' WHEN 12 THEN 'December' END AS [Month], \r\n\t\t                        [Year], Basic, Allowance, 0 AS [Other Benefits], Deduction, PaidAmount\r\n                        FROM SalarySheet SS\r\n                        INNER JOIN SalarySheet_Detail SSD ON SS.SysDocID = SSD.SysDocID AND SS.VoucherID = SSD.VoucherID\r\n                        INNER JOIN Employee E ON SSD.EmployeeID = E.EmployeeID\r\n                        WHERE 1=1\r\n                    \r\n                        AND (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True'))  \r\n                        AND ((StartDate BETWEEN '" + text + "' AND '" + text2 + "') OR ( EndDate BETWEEN '" + text + "' AND '" + text2 + "')) ";
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E.LocationID<='" + toLocation + "' ";
				}
				text3 += "  ORDER BY SSD.EmployeeID, [MonthVal] ";
				FillDataSet(dataSet, "SalarySheet_Detail", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeSalaryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime startDate, DateTime endDate, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int periodYear, int periodMonth, string EmployeeIDs)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0));
				string text2 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 11, 59, 59));
				string text3 = "";
				_ = string.Empty;
				text3 = "SELECT SSD.EmployeeID, FirstName + ' ' + LastName [Employee Name], SS.Month [MonthVal],\r\n\t                        CASE [Month] WHEN 1 THEN 'January' WHEN 2 THEN 'February' WHEN 3 THEN 'March' WHEN 4 THEN 'April' \r\n\t\t                        WHEN 5 THEN 'May' WHEN 6 THEN 'June' WHEN 7 THEN 'July' WHEN 8 THEN 'August' \r\n\t\t                        WHEN 9 THEN 'September' WHEN 10 THEN 'October' WHEN 11 THEN 'Nov' WHEN 12 THEN 'December' END AS [Month], \r\n\t\t                        [Year], Basic, Allowance, 0 AS [Other Benefits], Deduction, PaidAmount\r\n                        FROM SalarySheet SS\r\n                        INNER JOIN SalarySheet_Detail SSD ON SS.SysDocID = SSD.SysDocID AND SS.VoucherID = SSD.VoucherID\r\n                        INNER JOIN Employee E ON SSD.EmployeeID = E.EmployeeID\r\n                        WHERE 1=1\r\n                    \r\n                        AND (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True'))  \r\n                        AND ((StartDate BETWEEN '" + text + "' AND '" + text2 + "') OR ( EndDate BETWEEN '" + text + "' AND '" + text2 + "')) ";
				if (EmployeeIDs != "")
				{
					text3 = text3 + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text3 = text3 + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text3 = text3 + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text3 = text3 + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text3 = text3 + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text3 = text3 + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text3 = text3 + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text3 = text3 + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text3 = text3 + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPosition != "")
				{
					text3 = text3 + " AND E.PositionID <='" + toPosition + "' ";
				}
				if (fromBank != "")
				{
					text3 = text3 + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text3 = text3 + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text3 = text3 + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text3 = text3 + " AND E.AccountID <='" + toAccount + "' ";
				}
				text3 += "  ORDER BY SSD.EmployeeID, [MonthVal] ";
				FillDataSet(dataSet, "SalarySheet_Detail", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeSalarySlipReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, int month, int year)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SS.*, CASE Month WHEN 1 THEN 'JAN' WHEN 2 THEN 'FEB' WHEN 3 THEN 'MAR' WHEN 4 THEN 'APR' WHEN 5 THEN 'MAY' WHEN 6 THEN 'JUN' WHEN 7 THEN 'JUL' WHEN 8 THEN 'AUG' WHEN 9 THEN 'SEP'  WHEN 10 THEN 'OCT' WHEN 11 THEN 'NOV' WHEN '12' THEN 'DEC' END AS MonthName FROM SalarySheet SS WHERE Month=" + month + " AND Year = " + year;
				FillDataSet(dataSet, "SalarySheet", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["SalarySheet"].Rows.Count == 0)
				{
					return null;
				}
				string text = dataSet.Tables["SalarySheet"].Rows[0]["SysDocID"].ToString();
				string text2 = "";
				for (int i = 0; i < dataSet.Tables["SalarySheet"].Rows.Count; i++)
				{
					text2 = "'" + dataSet.Tables["SalarySheet"].Rows[i]["VoucherID"].ToString() + "'";
					if (i < dataSet.Tables["SalarySheet"].Rows.Count - 1)
					{
						text2 += ",";
					}
				}
				textCommand = "SELECT SSD.EmployeeID, FirstName + ' ' + LastName AS EmployeeName,SP.SponsorName,P.PositionName,E.JoiningDate,WL.WorkLocationName,WorkDays,Absent,WorkDays-Absent AS NetDays,\r\n                        Basic ,Allowance,Deduction,Basic + Allowance+Deduction  AS GrossSalary,OTHours AS OTHours,OTRate,OTFactor AS Factor,\r\n                        OTFixedAmount AS FixedAmount,OTIsFixed AS IsFixed,OTAmount,NetSalary, IBAN, BankID, LabourID,\r\n                        (SELECT RoutingCode FROM Bank WHERE Bank.BankID =  E.BankID) AS BankRouteCode               \r\n                        FROM SalarySheet_Detail SSD INNER JOIN Employee E ON SSD.EmployeeID= E.EmployeeID\r\n                        --LEFT JOIN Employee_PayrollItem_Detail EPD ON EPD.EmployeeID=E.EmployeeID\r\n                        LEFT JOIN Position P ON E.PositionID=P.PositionID\r\n                        LEFT JOIN Sponsor SP ON E.SponsorID=SP.SponsorID\r\n                        LEFT JOIN Work_Location WL ON WL.WorkLocationID=E.LocationID  \r\n                        WHERE SysDocID='" + text + "' AND VoucherID IN (" + text2 + ")";
				if (fromEmployee != "")
				{
					textCommand = textCommand + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					textCommand = textCommand + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					textCommand = textCommand + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					textCommand = textCommand + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					textCommand = textCommand + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					textCommand = textCommand + " AND E.LocationID<='" + toLocation + "' ";
				}
				textCommand += "  ORDER BY E.EmployeeID ";
				FillDataSet(dataSet, "SalarySheet_Detail", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["SalarySheet_Detail"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT SSDI.*,NULL AS StartDate, NULL AS EndDate,'' AS EmployeeName\r\n                        FROM SalarySheet_Detail_Item SSDI INNER JOIN Employee E ON SSDI.EmployeeID= E.EmployeeID\r\n                        WHERE SysDocID='" + text + "' AND VoucherID IN (" + text2 + ")";
				if (fromEmployee != "")
				{
					textCommand = textCommand + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					textCommand = textCommand + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					textCommand = textCommand + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					textCommand = textCommand + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					textCommand = textCommand + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					textCommand = textCommand + " AND E.LocationID<='" + toLocation + "' ";
				}
				textCommand += "  ORDER BY E.EmployeeID ";
				FillDataSet(dataSet, "SalarySheet_Detail_Item", textCommand);
				dataSet.Relations.Add("REL", dataSet.Tables["SalarySheet_Detail"].Columns["EmployeeID"], dataSet.Tables["SalarySheet_Detail_Item"].Columns["EmployeeID"]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeSalarySlipReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int month, int year, string EmployeeIDs)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT SS.*, CASE Month WHEN 1 THEN 'JAN' WHEN 2 THEN 'FEB' WHEN 3 THEN 'MAR' WHEN 4 THEN 'APR' WHEN 5 THEN 'MAY' WHEN 6 THEN 'JUN' WHEN 7 THEN 'JUL' WHEN 8 THEN 'AUG' WHEN 9 THEN 'SEP'  WHEN 10 THEN 'OCT' WHEN 11 THEN 'NOV' WHEN '12' THEN 'DEC' END AS MonthName FROM SalarySheet SS  INNER JOIN SalarySheet_Detail SSD ON SS.SysDocID=SSD.SysDocID AND SS.VoucherID=SSD.VoucherID INNER JOIN Employee E ON E.EmployeeID = SSD.EmployeeID WHERE Month=" + month + " AND Year = " + year;
				if (EmployeeIDs != "")
				{
					str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					str = str + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					str = str + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					str = str + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					str = str + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					str = str + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					str = str + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					str = str + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					str = str + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					str = str + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					str = str + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPosition != "")
				{
					str = str + " AND E.PositionID <='" + toPosition + "' ";
				}
				if (fromBank != "")
				{
					str = str + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					str = str + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					str = str + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					str = str + " AND E.AccountID <='" + toAccount + "' ";
				}
				str += "  GROUP BY SS.SysdocID,SS.VoucherID,SS.SheetName,SS.TransactionDate,SS.Month,SS.Year,SS.StartDate,SS.EndDate,SS.Note,SS.Reference,SS.IsPosted,SS.IsClosed,\r\n                                   SS.IsVoid,SS.ApprovalStatus,SS.VerificationStatus,SS.DateCreated,SS.DateUpdated,SS.CreatedBy,SS.UpdatedBy,SS.DivisionID,SS.CompanyID ";
				FillDataSet(dataSet, "SalarySheet", str);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["SalarySheet"].Rows.Count == 0)
				{
					return null;
				}
				string text = dataSet.Tables["SalarySheet"].Rows[0]["SysDocID"].ToString();
				string text2 = "";
				for (int i = 0; i < dataSet.Tables["SalarySheet"].Rows.Count; i++)
				{
					text2 = "'" + dataSet.Tables["SalarySheet"].Rows[i]["VoucherID"].ToString() + "'";
					if (i < dataSet.Tables["SalarySheet"].Rows.Count - 1)
					{
						text2 += ",";
					}
				}
				str = "SELECT SSD.EmployeeID, FirstName + ' ' + LastName AS EmployeeName,SP.SponsorName,P.PositionName,E.JoiningDate,WL.WorkLocationName,WorkDays,\r\n                        Absent,WorkDays-Absent-AnnualLeaves AS NetDays,AnnualLeaves,\r\n                        Basic ,Allowance,Deduction,Basic + Allowance+Deduction  AS GrossSalary,OTHours AS OTHours,OTRate,OTFactor AS Factor,\r\n                        OTFixedAmount AS FixedAmount,OTIsFixed AS IsFixed,OTAmount,NetSalary, IBAN, BankID, LabourID,\r\n                        (SELECT RoutingCode FROM Bank WHERE Bank.BankID =  E.BankID) AS BankRouteCode , E.BirthDate, E.BirthPlace, E.BloodGroup, E.DepartmentID +' '+ D.DepartmentName AS Department ,E.DivisionID +' '+DV.DivisionName AS DIVISION, E.LabourID, E.ContractType,E.GradeID              \r\n                        FROM SalarySheet_Detail SSD INNER JOIN Employee E ON SSD.EmployeeID= E.EmployeeID\r\n                        --LEFT JOIN Employee_PayrollItem_Detail EPD ON EPD.EmployeeID=E.EmployeeID\r\n                        LEFT OUTER JOIN Position P ON E.PositionID=P.PositionID\r\n                        LEFT OUTER JOIN Sponsor SP ON E.SponsorID=SP.SponsorID\r\n                        LEFT OUTER JOIN Department D ON E.DepartmentID=D.DepartmentID\r\n                        LEFT OUTER JOIN Work_Location WL ON WL.WorkLocationID=E.LocationID  \r\n                        LEFT OUTER JOIN Division DV ON E.DivisionID=DV.DivisionID\r\n                        WHERE SysDocID='" + text + "' AND VoucherID IN (" + text2 + ")";
				if (EmployeeIDs != "")
				{
					str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					str = str + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					str = str + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					str = str + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					str = str + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					str = str + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					str = str + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					str = str + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					str = str + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					str = str + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					str = str + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPosition != "")
				{
					str = str + " AND E.PositionID <='" + toPosition + "' ";
				}
				if (fromBank != "")
				{
					str = str + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					str = str + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					str = str + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					str = str + " AND E.AccountID <='" + toAccount + "' ";
				}
				str += "  ORDER BY E.EmployeeID ";
				FillDataSet(dataSet, "SalarySheet_Detail", str);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["SalarySheet_Detail"].Rows.Count == 0)
				{
					return null;
				}
				str = "SELECT SSDI.*,NULL AS StartDate, NULL AS EndDate,'' AS EmployeeName\r\n                        FROM SalarySheet_Detail_Item SSDI INNER JOIN Employee E ON SSDI.EmployeeID= E.EmployeeID\r\n                        WHERE SysDocID='" + text + "' AND VoucherID IN (" + text2 + ")";
				if (EmployeeIDs != "")
				{
					str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					str = str + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					str = str + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					str = str + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					str = str + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					str = str + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					str = str + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					str = str + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					str = str + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					str = str + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					str = str + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPosition != "")
				{
					str = str + " AND E.PositionID <='" + toPosition + "' ";
				}
				if (fromBank != "")
				{
					str = str + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					str = str + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					str = str + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					str = str + " AND E.AccountID <='" + toAccount + "' ";
				}
				str += "  ORDER BY E.EmployeeID ";
				FillDataSet(dataSet, "SalarySheet_Detail_Item", str);
				dataSet.Relations.Add("REL", dataSet.Tables["SalarySheet_Detail"].Columns["EmployeeID"], dataSet.Tables["SalarySheet_Detail_Item"].Columns["EmployeeID"]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeSalarySlipReportWeb(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int month, int year, string EmployeeIDs)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT SS.*, CASE Month WHEN 1 THEN 'JAN' WHEN 2 THEN 'FEB' WHEN 3 THEN 'MAR' WHEN 4 THEN 'APR' WHEN 5 THEN 'MAY' WHEN 6 THEN 'JUN' WHEN 7 THEN 'JUL' WHEN 8 THEN 'AUG' WHEN 9 THEN 'SEP'  WHEN 10 THEN 'OCT' WHEN 11 THEN 'NOV' WHEN '12' THEN 'DEC' END AS MonthName FROM SalarySheet SS  INNER JOIN SalarySheet_Detail SSD ON SS.SysDocID=SSD.SysDocID AND SS.VoucherID=SSD.VoucherID INNER JOIN Employee E ON E.EmployeeID = SSD.EmployeeID WHERE Month=" + month + " AND Year = " + year;
				if (EmployeeIDs != "")
				{
					str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					str = str + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					str = str + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					str = str + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					str = str + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					str = str + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					str = str + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					str = str + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					str = str + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					str = str + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					str = str + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPosition != "")
				{
					str = str + " AND E.PositionID <='" + toPosition + "' ";
				}
				if (fromBank != "")
				{
					str = str + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					str = str + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					str = str + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					str = str + " AND E.AccountID <='" + toAccount + "' ";
				}
				str += "  ORDER BY E.EmployeeID ";
				FillDataSet(dataSet, "SalarySheet", str);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["SalarySheet"].Rows.Count == 0)
				{
					return null;
				}
				string text = dataSet.Tables["SalarySheet"].Rows[0]["SysDocID"].ToString();
				string text2 = "";
				for (int i = 0; i < dataSet.Tables["SalarySheet"].Rows.Count; i++)
				{
					text2 = "'" + dataSet.Tables["SalarySheet"].Rows[i]["VoucherID"].ToString() + "'";
					if (i < dataSet.Tables["SalarySheet"].Rows.Count - 1)
					{
						text2 += ",";
					}
				}
				str = "SELECT SSD.EmployeeID, FirstName + ' ' + LastName AS EmployeeName,SP.SponsorName,P.PositionName,E.JoiningDate,WL.WorkLocationName,WorkDays,Absent,WorkDays-Absent AS NetDays,\r\n                        Basic ,Allowance,Deduction,Basic + Allowance+Deduction  AS GrossSalary,OTHours AS OTHours,OTRate,OTFactor AS Factor,\r\n                        OTFixedAmount AS FixedAmount,OTIsFixed AS IsFixed,OTAmount,NetSalary, IBAN, BankID, LabourID,\r\n                        (SELECT RoutingCode FROM Bank WHERE Bank.BankID =  E.BankID) AS BankRouteCode               \r\n                        FROM SalarySheet_Detail SSD INNER JOIN Employee E ON SSD.EmployeeID= E.EmployeeID\r\n                        --LEFT JOIN Employee_PayrollItem_Detail EPD ON EPD.EmployeeID=E.EmployeeID\r\n                        LEFT OUTER JOIN Position P ON E.PositionID=P.PositionID\r\n                        LEFT OUTER JOIN Sponsor SP ON E.SponsorID=SP.SponsorID\r\n                        LEFT OUTER JOIN Work_Location WL ON WL.WorkLocationID=E.LocationID  \r\n                        WHERE SysDocID='" + text + "' AND VoucherID IN (" + text2 + ")";
				if (EmployeeIDs != "")
				{
					str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					str = str + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					str = str + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					str = str + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					str = str + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					str = str + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					str = str + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					str = str + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					str = str + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					str = str + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					str = str + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPosition != "")
				{
					str = str + " AND E.PositionID <='" + toPosition + "' ";
				}
				if (fromBank != "")
				{
					str = str + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					str = str + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					str = str + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					str = str + " AND E.AccountID <='" + toAccount + "' ";
				}
				str += "  ORDER BY E.EmployeeID ";
				FillDataSet(dataSet, "SalarySheet_Detail", str);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["SalarySheet_Detail"].Rows.Count == 0)
				{
					return null;
				}
				str = "SELECT SSDI.*,NULL AS StartDate, NULL AS EndDate,'' AS EmployeeName\r\n                        FROM SalarySheet_Detail_Item SSDI INNER JOIN Employee E ON SSDI.EmployeeID= E.EmployeeID\r\n                        WHERE SysDocID='" + text + "' AND VoucherID IN (" + text2 + ")";
				if (EmployeeIDs != "")
				{
					str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					str = str + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					str = str + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					str = str + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					str = str + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					str = str + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					str = str + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					str = str + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					str = str + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					str = str + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					str = str + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPosition != "")
				{
					str = str + " AND E.PositionID <='" + toPosition + "' ";
				}
				if (fromBank != "")
				{
					str = str + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					str = str + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					str = str + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					str = str + " AND E.AccountID <='" + toAccount + "' ";
				}
				str += "  ORDER BY E.EmployeeID ";
				FillDataSet(dataSet, "SalarySheet_Detail_Item", str);
				dataSet.Relations.Add("REL", dataSet.Tables["SalarySheet_Detail"].Columns["EmployeeID"], dataSet.Tables["SalarySheet_Detail_Item"].Columns["EmployeeID"]);
				DataSet dataSet2 = new DataSet();
				string textCommand = "SELECT     EmployeeID,FirstName AS [First Name],MiddleName AS [Middle Name],LastName AS [Last Name],\r\n                                FirstName + ' ' +MiddleName + ' ' + LastName AS [Full Name],NickName [Nick Name], BankID, IBAN, BasicSalary, PaymentMethodID, AccountID, CurrencyID, \r\n                                SalaryRemarks,DestinationID,TicketRemarks,TicketPeriod,NumberOfTickets,TicketAmount,EOSRuleID,OvertimeID, LastRevisedSalaryDate\r\n                            FROM Employee WHERE EmployeeID='" + fromEmployee + "'";
				FillDataSet(dataSet2, "Employee", textCommand);
				dataSet.Merge(dataSet2);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeLeaveStatusReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime startDate, DateTime endDate, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string fromLeave, string toLeave, string EmployeeIDs)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0));
				string text2 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 11, 59, 59));
				string text3 = "";
				string text4 = "";
				_ = string.Empty;
				text4 = "On Account";
				text3 = "select e.EmployeeID, ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],eld.LeaveTypeID,LT.LeaveTypeName,SUM(obld.LeaveTaken) as [openingLeavesTaken],\r\n                             DATEDIFF(MONTH,e.JoiningDate,'" + text + "') as Sevicemonths,  \r\n                             CASE WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND MonthGreater1 > DATEDIFF(MONTH,e.JoiningDate,'" + text + "') THEN 'True' ELSE 'False' END AS 'AnnualEligible',                                             \r\n                             CASE\r\n                             -- WHEN lt.IsAnnual='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  AND MonthLesser1 < DATEDIFF(MONTH,e.JoiningDate,GETDATE()))  THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-(MonthLesser1+MonthGreater1))* AllowedDays1\r\n                              WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser1 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text + "'))* AllowedDays1\r\n                             WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser1 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text + "'))* AllowedDays1\r\n                              End AS '1SET',\r\n                             CASE \r\n                             --WHEN lt.IsAnnual='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  OR MonthLesser2 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())) THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-(MonthLesser2+MonthGreater2)) * AllowedDays2 \r\n                             WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser2 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text + "'))* AllowedDays2\r\n                             WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser2 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text + "'))* AllowedDays2\r\n\r\n                             End AS '2SET',\r\n                             CASE\r\n                             WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser3 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text + "'))* AllowedDays3\r\n                          -- WHEN lt.IsAnnual='1' AND MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())   THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-MonthGreater3) * AllowedDays3 \r\n                             WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser3 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text + "'))* AllowedDays3\r\n                             End AS '3SET',\r\n                             LT.IsAnnual,  LT.Days AS LeaveDayswithType,(\r\n                             SELECT  SUM(DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken\r\n                             FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n                             LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                             LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID \r\n                             LEFT JOIN   Opening_Balance_Leave_Detail OPBLD1 ON OPBLD1.EmployeeID=E1.EmployeeID\r\n        LEFT JOIN   Opening_Balance_Leave OPBL1 ON OPBL1.BatchID=OPBLD1.BatchID AND OPBL1.SysDocID=OPBLD1.SysDocID \r\n                             INNER JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID \r\n                             WHERE  ISNULL(isvoid,0) =0  --AND ELR1.StartDate>= ISNULL(OPBL1.BatchDate,ELR1.StartDate) \r\n                             AND IsApproved=1 and E1.EmployeeID=E.EmployeeID AND LT1.LeaveTypeID=LT.LeaveTypeID AND ELR1.EndDate<='" + text2 + "'\r\n                             GROUP BY LT1.LeaveTypeName, E1.EmployeeID) AS TotalTaken,0.0 AS TotalLeaves,0.0 AS LeavesRemaining,'" + text4 + "' AS Basedon\r\n                             FROM Employee e LEFT join Employee_Type_Detail eld ON e.ContractType=eld.TypeID\r\n                             LEFT JOIN  Employee_Type ET ON e.ContractType=ET.TypeID\r\n                             LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=eld.LeaveTypeID\r\n                             LEFT JOIN Opening_Balance_Leave_Detail  obld ON e.EmployeeID=obld.EmployeeID AND eld.LeaveTypeID=obld.LeaveTypeID AND lt.IsAnnual=1 WHERE ISNULL(E.Status,1) = 1 ";
				if (fromLeave != "" && toLocation != "")
				{
					text3 = text3 + " AND eld.LeaveTypeID >='" + fromLeave + "' AND eld.LeaveTypeID <='" + toLeave + "'";
				}
				else if (fromLeave != "")
				{
					text3 = text3 + " AND eld.LeaveTypeID ='" + fromLeave + "'";
				}
				if (EmployeeIDs != "")
				{
					text3 = text3 + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text3 = text3 + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text3 = text3 + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text3 = text3 + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text3 = text3 + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text3 = text3 + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text3 = text3 + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text3 = text3 + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text3 = text3 + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPosition != "")
				{
					text3 = text3 + " AND E.PositionID <='" + toPosition + "' ";
				}
				if (fromBank != "")
				{
					text3 = text3 + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text3 = text3 + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text3 = text3 + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text3 = text3 + " AND E.AccountID <='" + toAccount + "' ";
				}
				text3 += " GROUP BY e.EmployeeID,FirstName,MiddleName,LastName,eld.LeaveTypeID,LT.LeaveTypeName,e.JoiningDate,lt.IsAnnual,LT.IsCumulative,MonthGreater1,MonthLesser1,AllowedDays1,MonthGreater2,MonthLesser2,AllowedDays2,MonthGreater3,MonthLesser3,AllowedDays3,LT.Days,LT.LeaveTypeID";
				FillDataSet(dataSet, "EmployeeLeaveStatus", text3);
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					dataSet.Tables[0].Rows[0]["Basedon"] = text4;
					TosetLeave(dataSet, startDate, endDate);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeAnnualDueReport_Last(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime startDate, DateTime endDate)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0));
				string text2 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 11, 59, 59));
				string text3 = "";
				_ = string.Empty;
				text3 = "SELECT E.EmployeeID, FirstName + ' ' + LastName [Employee Name], (DATEDIFF(day, E.JoiningDate, GETDATE()) / 365) * 30 [Eligible Days], DATEDIFF(day, ELR.StartDate, ELR.EndDate) + 1 [Leaves Taken]\r\n                        FROM Employee E INNER JOIN Employee_Activity EA ON E.EmployeeID = EA.EmployeeID\r\n                        INNER JOIN Employee_Leave_Request ELR ON EA.ActivityID = ELR.ActivityID\r\n                        WHERE ISNULL(IsApproved, 0) = 1\r\n                        AND LeaveTypeID IN (SELECT LeaveTypeID FROM Leave_Type WHERE IsAnnual=1) AND ISNULL(IsApproved, 0) = 1";
				text3 = text3 + " AND ApproveDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E.LocationID<='" + toLocation + "' ";
				}
				FillDataSet(dataSet, "EmployeeLeaveDue", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeAnnualDueReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime startDate, DateTime endDate, object obj2, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string EmployeeIDs)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			string text2 = "";
			string text3 = CommonLib.ToSqlDateTimeString(new DateTime(startDate.Year, startDate.Month, startDate.Day, 11, 59, 59));
			string text4 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 11, 59, 59));
			if (obj2 != null)
			{
				if (obj2.ToString().Trim() == "OA" || obj2.ToString() == "")
				{
					text2 = "On Account";
					text = "select e.EmployeeID, ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],eld.LeaveTypeID,LT.LeaveTypeName,SUM(obld.LeaveTaken) as [openingLeavesTaken],\r\n                            DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "') as Sevicemonths,  \r\n                            CASE WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND MonthGreater1 > DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "') THEN 'True' ELSE 'False' END AS 'AnnualEligible',                                             \r\n                            CASE\r\n                            -- WHEN lt.IsAnnual='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  AND MonthLesser1 < DATEDIFF(MONTH,e.JoiningDate,GETDATE()))  THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-(MonthLesser1+MonthGreater1))* AllowedDays1\r\n                             WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser1 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text3 + "'))* AllowedDays1\r\n                            WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser1 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text3 + "'))* AllowedDays1\r\n                             End AS '1SET',\r\n                            CASE \r\n                            --WHEN lt.IsAnnual='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  OR MonthLesser2 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())) THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-(MonthLesser2+MonthGreater2)) * AllowedDays2 \r\n                            WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser2 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text3 + "'))* AllowedDays2\r\n                            WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser2 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text3 + "'))* AllowedDays2\r\n\r\n                            End AS '2SET',\r\n                            CASE\r\n                            WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser3 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text3 + "'))* AllowedDays3\r\n                         -- WHEN lt.IsAnnual='1' AND MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())   THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-MonthGreater3) * AllowedDays3 \r\n                            WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser3 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text3 + "'))* AllowedDays3\r\n                            End AS '3SET',\r\n                            LT.IsAnnual,  LT.Days AS LeaveDayswithType,(\r\n                            SELECT TOP 1 SUM(DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken\r\n                            FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n                            LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                            LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID \r\n                            LEFT JOIN   Opening_Balance_Leave_Detail OPBLD1 ON OPBLD1.EmployeeID=E1.EmployeeID AND OPBLD1.LeaveTypeID=ELR1.LeaveTypeID\r\n\t\t\t\t\t\t\tLEFT JOIN   Opening_Balance_Leave OPBL1 ON OPBL1.BatchID=OPBLD1.BatchID AND OPBL1.SysDocID=OPBLD1.SysDocID \r\n                            INNER JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID \r\n                            WHERE  ISNULL(isvoid,0) =0  --AND ELR1.StartDate>= ISNULL(OPBL1.BatchDate,ELR1.StartDate) \r\n                            AND IsApproved=1 and E1.EmployeeID=E.EmployeeID AND LT1.LeaveTypeID=LT.LeaveTypeID AND ELR1.EndDate<='" + text4 + "'\r\n                            GROUP BY LT1.LeaveTypeName, E1.EmployeeID, OPBLD1.LeaveEndDate) AS TotalTaken,0.0 AS TotalLeaves,0.0 AS LeavesRemaining,'" + text2 + "' AS Basedon\r\n                            FROM Employee e LEFT join Employee_Type_Detail eld ON e.ContractType=eld.TypeID\r\n                            LEFT JOIN  Employee_Type ET ON e.ContractType=ET.TypeID\r\n                            LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=eld.LeaveTypeID\r\n                            LEFT JOIN Opening_Balance_Leave_Detail  obld ON e.EmployeeID=obld.EmployeeID AND eld.LeaveTypeID=obld.LeaveTypeID AND lt.IsAnnual=1 WHERE eld.LeaveTypeID<>'' AND ISNULL(E.Status,1) = 1 AND ET.LeaveSelection=ISNULL('" + obj2.ToString().Trim() + "','')";
					if (EmployeeIDs != "")
					{
						text = text + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
					}
					if (fromEmployee != "")
					{
						text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
					}
					if (toEmployee != "")
					{
						text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
					}
					if (fromDepartment != "")
					{
						text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
					}
					if (toDepartment != "")
					{
						text = text + " AND E.EmployeeID<='" + toDepartment + "' ";
					}
					if (fromLocation != "")
					{
						text = text + " AND E.LocationID>='" + fromLocation + "' ";
					}
					if (toLocation != "")
					{
						text = text + " AND E.LocationID<='" + toLocation + "' ";
					}
					if (fromType != "")
					{
						text = text + " AND E.ContractType >='" + fromType + "' ";
					}
					if (toType != "")
					{
						text = text + " AND E.ContractType <='" + toType + "' ";
					}
					if (fromDivision != "")
					{
						text = text + " AND E.DivisionID >='" + fromDivision + "' ";
					}
					if (toDivision != "")
					{
						text = text + " AND E.DivisionID <='" + toDivision + "' ";
					}
					if (fromSponsor != "")
					{
						text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
					}
					if (toSponsor != "")
					{
						text = text + " AND E.SponsorID <='" + toSponsor + "' ";
					}
					if (fromGroup != "")
					{
						text = text + " AND E.GroupID >='" + fromGroup + "' ";
					}
					if (toGroup != "")
					{
						text = text + " AND E.GroupID <='" + toGroup + "' ";
					}
					if (fromGrade != "")
					{
						text = text + " AND E.GradeID >='" + fromGrade + "' ";
					}
					if (toGrade != "")
					{
						text = text + " AND E.GradeID <='" + toGrade + "' ";
					}
					if (fromPosition != "")
					{
						text = text + " AND E.PositionID >='" + fromPosition + "' ";
					}
					if (toPosition != "")
					{
						text = text + " AND E.PositionID <='" + toPosition + "' ";
					}
					if (fromBank != "")
					{
						text = text + " AND E.BankID >='" + fromBank + "' ";
					}
					if (toBank != "")
					{
						text = text + " AND E.BankID <='" + toBank + "' ";
					}
					if (fromAccount != "")
					{
						text = text + " AND E.AccountID >='" + fromAccount + "' ";
					}
					if (toAccount != "")
					{
						text = text + " AND E.AccountID <='" + toAccount + "' ";
					}
					text += " GROUP BY e.EmployeeID,FirstName,MiddleName,LastName,eld.LeaveTypeID,LT.LeaveTypeName,e.JoiningDate,lt.IsAnnual,LT.IsCumulative,MonthGreater1,MonthLesser1,AllowedDays1,MonthGreater2,MonthLesser2,AllowedDays2,MonthGreater3,MonthLesser3,AllowedDays3,LT.Days,LT.LeaveTypeID";
					FillDataSet(dataSet, "LeaveAvailability", text);
					DataView defaultView = dataSet.Tables["LeaveAvailability"].DefaultView;
					defaultView.RowFilter = "IsAnnual=1";
					DataSet dataSet2 = new DataSet();
					DataTable table = defaultView.ToTable();
					dataSet2.Tables.Add(table);
					dataSet.Clear();
					dataSet.Merge(dataSet2);
				}
				else if (obj2.ToString().Trim() == "CD")
				{
					text2 = "Calendar Year";
					text = "select E.EmployeeID, ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],eld.LeaveTypeID,LT.LeaveTypeName,SUM(obld.LeaveTaken) as [openingLeavesTaken]\r\n                            ,DATEDIFF(MONTH,e.JoiningDate,GETDATE()) as Sevicemonths, \r\n\t\t\t\t\t\t\tCASE WHEN lt.IsAnnual='1' THEN elp.Days End AS 'AnnualAllowedDays',\r\n\t\t\t\t\t\t\tCASE WHEN lt.IsAnnual='1' THEN elp.FromDate END AS 'FromDate',\r\n\t\t\t\t\t\t\tCASE WHEN lt.IsAnnual='1' THEN elp.ToDate End AS 'ToDate',\r\n                             LT.Days AS LeaveDayswithType,(\r\n\t\t\t\t    \t\tSELECT  SUM(DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken\r\n\t\t\t\t\t        FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n\t\t\t\t\t        LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                            LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID  \r\n                            LEFT JOIN  Employee_Type ET1 ON E1.ContractType=ET1.TypeID\r\n                            LEFT JOIN   Opening_Balance_Leave_Detail OPBLD1 ON OPBLD1.EmployeeID=E1.EmployeeID\r\n\t\t\t\t\t\t\tLEFT JOIN   Opening_Balance_Leave OPBL1 ON OPBL1.BatchID=OPBLD1.BatchID AND OPBL1.SysDocID=OPBLD1.SysDocID\r\n                            INNER JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID \r\n                            WHERE  ISNULL(isvoid,0) =0 --AND ELR1.StartDate>= ISNULL(OPBL1.BatchDate,ELR1.StartDate) \r\n                            AND ELR1.EndDate<='" + text4 + "' AND IsApproved=1 and E1.EmployeeID=E.EmployeeID AND LT1.LeaveTypeID=LT.LeaveTypeID\r\n\t\t\t\t\t        GROUP BY LT1.LeaveTypeName, E1.EmployeeID) AS TotalTaken,(select count(HD1.FromDate) from Holiday_Calendar_Detail HD1\r\n                             LEFT JOIN EMPLOYEE E2 ON HD1.CalendarID=e.CalendarID \r\n                             LEFT JOIN  Employee_Activity EA2 ON EA2.EmployeeID = E2.EmployeeID\r\n                             LEFT JOIN Employee_Leave_Request ELR2 ON EA2.ActivityID = ELR2.ActivityID\r\n                             INNER JOIN Leave_Type LT2 ON LT2.LeaveTypeID=ELR2.LeaveTypeID \r\n                             and (HD1.FromDate BETWEEN ELR2.StartDate AND ELR2.EndDate\r\n                             )WHERE  ISNULL(isvoid,0) =0 and ISNULL(IsApproved,0)=1  and E2.EmployeeID=E.EmployeeID AND LT2.LeaveTypeID=LT.LeaveTypeID GROUP BY LT2.LeaveTypeName, E2.EmployeeID)AS ToLessTaken,\r\n                            0 AS TotalLeaves,0 AS LeavesRemaining,'" + text2 + "' AS Basedon,CASE WHEN lt.IsAnnual='1' THEN 'True' ELSE 'False' END AS IsAnnual\r\n                             FROM Employee e LEFT join Employee_Type_Detail eld ON e.ContractType=eld.TypeID\r\n                            LEFT JOIN  Employee_Type ET ON E.ContractType=ET.TypeID          \r\n\t\t\t\t\t\t\t LEFT JOIN Employee_Leave_Process elp ON e.EmployeeID=elp.EmployeeID\r\n                             LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=eld.LeaveTypeID\r\n                             LEFT JOIN Opening_Balance_Leave_Detail  obld ON e.EmployeeID=obld.EmployeeID AND eld.LeaveTypeID=obld.LeaveTypeID WHERE eld.LeaveTypeID<>'' AND lt.IsAnnual=1 AND ISNULL(E.Status,1) = 1 AND ET.LeaveSelection='" + obj2.ToString().Trim() + "'";
					if (EmployeeIDs != "")
					{
						text = text + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
					}
					if (fromEmployee != "")
					{
						text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
					}
					if (toEmployee != "")
					{
						text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
					}
					if (fromDepartment != "")
					{
						text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
					}
					if (toDepartment != "")
					{
						text = text + " AND E.EmployeeID<='" + toDepartment + "' ";
					}
					if (fromLocation != "")
					{
						text = text + " AND E.LocationID>='" + fromLocation + "' ";
					}
					if (toLocation != "")
					{
						text = text + " AND E.LocationID<='" + toLocation + "' ";
					}
					if (fromType != "")
					{
						text = text + " AND E.ContractType >='" + fromType + "' ";
					}
					if (toType != "")
					{
						text = text + " AND E.ContractType <='" + toType + "' ";
					}
					if (fromDivision != "")
					{
						text = text + " AND E.DivisionID >='" + fromDivision + "' ";
					}
					if (toDivision != "")
					{
						text = text + " AND E.DivisionID <='" + toDivision + "' ";
					}
					if (fromSponsor != "")
					{
						text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
					}
					if (toSponsor != "")
					{
						text = text + " AND E.SponsorID <='" + toSponsor + "' ";
					}
					if (fromGroup != "")
					{
						text = text + " AND E.GroupID >='" + fromGroup + "' ";
					}
					if (toGroup != "")
					{
						text = text + " AND E.GroupID <='" + toGroup + "' ";
					}
					if (fromGrade != "")
					{
						text = text + " AND E.GradeID >='" + fromGrade + "' ";
					}
					if (toGrade != "")
					{
						text = text + " AND E.GradeID <='" + toGrade + "' ";
					}
					if (fromPosition != "")
					{
						text = text + " AND E.PositionID >='" + fromPosition + "' ";
					}
					if (toPosition != "")
					{
						text = text + " AND E.PositionID <='" + toPosition + "' ";
					}
					if (fromBank != "")
					{
						text = text + " AND E.BankID >='" + fromBank + "' ";
					}
					if (toBank != "")
					{
						text = text + " AND E.BankID <='" + toBank + "' ";
					}
					if (fromAccount != "")
					{
						text = text + " AND E.AccountID >='" + fromAccount + "' ";
					}
					if (toAccount != "")
					{
						text = text + " AND E.AccountID <='" + toAccount + "' ";
					}
					text += "GROUP BY e.EmployeeID,FirstName,MiddleName,LastName,eld.LeaveTypeID,LT.LeaveTypeName,e.JoiningDate,lt.IsAnnual,LT.IsCumulative,MonthGreater1,MonthLesser1,AllowedDays1,MonthGreater2,MonthLesser2,AllowedDays2\r\n                        ,MonthGreater3,MonthLesser3,AllowedDays3,LT.Days,LT.LeaveTypeID,elp.Days,elp.FromDate,elp.ToDate,E.CalendarID,elp.RowIndex,elp.VoucherID ORDER by E.EmployeeID,LT.LeaveTypeID,elp.ToDate asc";
					FillDataSet(dataSet, "LeaveAvailability", text);
					new DataSet();
					text = "SELECT E1.EmployeeID,ISNULL (ELR1.ActualLeaveDays,DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken,\r\n                                   0 AS ToLessTaken,ELP1.FromDate,ELP1.ToDate,CASE WHEN LT1.IsAnnual='1' THEN\r\n                                   ELP1.DAYS ELSE LT1.DAYS END  AS DaysAllowed,LT1.LeaveTypeID,CASE WHEN LT1.IsAnnual='1' THEN 'True' ELSE 'False' END AS IsAnnual\r\n                                   FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n                                   LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                                   LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID  \r\n                                   LEFT JOIN   Opening_Balance_Leave_Detail OPBLD1 ON OPBLD1.EmployeeID=E1.EmployeeID\r\n                                   LEFT JOIN   Opening_Balance_Leave OPBL1 ON OPBL1.BatchID=OPBLD1.BatchID AND OPBL1.SysDocID=OPBLD1.SysDocID\r\n                                   LEFT JOIN Employee_Leave_Process ELP1 ON E1.EmployeeID=ELP1.EmployeeID \t\t\t\t\t\t\t\r\n                                   LEFT JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID \r\n                                   LEFT JOIN Holiday_Calendar_Detail HC ON  HC.CalendarID=E1.CalendarID\r\n                                   WHERE  ISNULL(isvoid,0) =0 \r\n                                   AND CONVERT(VARCHAR(10),ELR1.StartDate,111)>= CONVERT(VARCHAR(10),ISNULL(OPBL1.BatchDate,ELR1.StartDate),111)\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t       AND CONVERT(VARCHAR(10),ELR1.StartDate,111) >= CONVERT(VARCHAR(10),ISNULL(ELP1.FromDate,ELR1.StartDate) ,111)\r\n                                   AND CONVERT(VARCHAR(10),ELR1.EndDate,111) <= CONVERT(VARCHAR(10),ISNULL(ELP1.ToDate,ELR1.EndDate),111)                                        \r\n                                  AND IsApproved=1 AND LT1.LeaveTypeID=ELR1.LeaveTypeID ";
					if (EmployeeIDs != "")
					{
						text = text + " AND E1.EmployeeID IN(" + EmployeeIDs + ")";
					}
					if (fromEmployee != "")
					{
						text = text + " AND E1.EmployeeID>='" + fromEmployee + "' ";
					}
					if (toEmployee != "")
					{
						text = text + " AND E1.EmployeeID<='" + toEmployee + "' ";
					}
					if (fromDepartment != "")
					{
						text = text + " AND E1.DepartmentID>='" + fromDepartment + "' ";
					}
					if (toDepartment != "")
					{
						text = text + " AND E1.EmployeeID<='" + toDepartment + "' ";
					}
					if (fromLocation != "")
					{
						text = text + " AND E1.LocationID>='" + fromLocation + "' ";
					}
					if (toLocation != "")
					{
						text = text + " AND E1.LocationID<='" + toLocation + "' ";
					}
					if (fromType != "")
					{
						text = text + " AND E1.ContractType >='" + fromType + "' ";
					}
					if (toType != "")
					{
						text = text + " AND E1.ContractType <='" + toType + "' ";
					}
					if (fromDivision != "")
					{
						text = text + " AND E1.DivisionID >='" + fromDivision + "' ";
					}
					if (toDivision != "")
					{
						text = text + " AND E1.DivisionID <='" + toDivision + "' ";
					}
					if (fromSponsor != "")
					{
						text = text + " AND E1.SponsorID >='" + fromSponsor + "' ";
					}
					if (toSponsor != "")
					{
						text = text + " AND E1.SponsorID <='" + toSponsor + "' ";
					}
					if (fromGroup != "")
					{
						text = text + " AND E1.GroupID >='" + fromGroup + "' ";
					}
					if (toGroup != "")
					{
						text = text + " AND E1.GroupID <='" + toGroup + "' ";
					}
					if (fromGrade != "")
					{
						text = text + " AND E1.GradeID >='" + fromGrade + "' ";
					}
					if (toGrade != "")
					{
						text = text + " AND E1.GradeID <='" + toGrade + "' ";
					}
					if (fromPosition != "")
					{
						text = text + " AND E1.PositionID >='" + fromPosition + "' ";
					}
					if (toPosition != "")
					{
						text = text + " AND E1.PositionID <='" + toPosition + "' ";
					}
					if (fromBank != "")
					{
						text = text + " AND E1.BankID >='" + fromBank + "' ";
					}
					if (toBank != "")
					{
						text = text + " AND E1.BankID <='" + toBank + "' ";
					}
					if (fromAccount != "")
					{
						text = text + " AND E1.AccountID >='" + fromAccount + "' ";
					}
					if (toAccount != "")
					{
						text = text + " AND E1.AccountID <='" + toAccount + "' ";
					}
					text += "GROUP BY E1.EmployeeID,ELR1.StartDate,ELR1.EndDate,ELP1.FromDate,ELP1.ToDate,ELP1.VoucherID,ELP1.Days,LT1.IsAnnual,LT1.LeaveTypeID,LT1.DAYS,E1.CalendarID,HC.CalendarID,ELR1.ActualLeaveDays,ELP1.RowIndex";
					FillDataSet(dataSet, "LeavesTaken", text);
				}
				if (obj2.ToString() != "" && obj2 != null && dataSet.Tables[0].Rows.Count > 0)
				{
					dataSet.Tables[0].Rows[0]["Basedon"] = text2;
					TosetLeave(dataSet, startDate, endDate);
				}
			}
			return dataSet;
		}

		public DataSet TosetLeave(DataSet currentData, DateTime startDate, DateTime endDate)
		{
			List<Leavelist> list = new List<Leavelist>();
			string a = "";
			if (currentData.Tables.Count > 0)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				int result4 = 0;
				int result5 = 0;
				int result6 = 0;
				int result7 = 0;
				bool result8 = false;
				bool result9 = false;
				decimal num = default(decimal);
				int num2 = 0;
				int result10 = 0;
				DateTime result11 = new DateTime(2014, 1, 1);
				DateTime result12 = new DateTime(2014, 1, 1);
				DateTime d = new DateTime(1, 1, 1);
				if (currentData.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "On Account")
				{
					for (int i = 0; i < currentData.Tables[0].Rows.Count; i++)
					{
						decimal.TryParse(currentData.Tables[0].Rows[i]["1SET"].ToString(), out result);
						decimal.TryParse(currentData.Tables[0].Rows[i]["2SET"].ToString(), out result2);
						decimal.TryParse(currentData.Tables[0].Rows[i]["3SET"].ToString(), out result3);
						int.TryParse(currentData.Tables[0].Rows[i]["openingLeavesTaken"].ToString(), out result4);
						int.TryParse(currentData.Tables[0].Rows[i]["TotalTaken"].ToString(), out result5);
						int.TryParse(currentData.Tables[0].Rows[i]["LeaveDayswithType"].ToString(), out result6);
						bool.TryParse(currentData.Tables[0].Rows[i]["AnnualEligible"].ToString(), out result8);
						bool.TryParse(currentData.Tables[0].Rows[i]["IsAnnual"].ToString(), out result9);
						if (result != 0m || result2 != 0m || result3 != 0m || (!result8 && result9))
						{
							result6 = 0;
						}
						decimal num3 = default(decimal);
						decimal num4 = default(decimal);
						num3 = result + result2 + result3 + (decimal)result6;
						num4 = result + result2 + result3 - (decimal)result4 + (decimal)result6 - (decimal)result5;
						result5 += result4;
						currentData.Tables[0].Rows[i]["TotalLeaves"] = num3;
						currentData.Tables[0].Rows[i]["LeavesRemaining"] = num4;
						currentData.Tables[0].Rows[i]["TotalTaken"] = result5;
					}
					currentData.Tables[0].Columns.Remove("1SET");
					currentData.Tables[0].Columns.Remove("2SET");
					currentData.Tables[0].Columns.Remove("3SET");
				}
				else if (currentData.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "Calendar Year")
				{
					List<string> list2 = new List<string>();
					List<string> list3 = new List<string>();
					for (int j = 0; j < currentData.Tables[0].Rows.Count; j++)
					{
						int.TryParse(currentData.Tables[0].Rows[j]["openingLeavesTaken"].ToString(), out result4);
						int.TryParse(currentData.Tables[0].Rows[j]["TotalTaken"].ToString(), out result5);
						int.TryParse(currentData.Tables[0].Rows[j]["LeaveDayswithType"].ToString(), out result6);
						DateTime.TryParse(currentData.Tables[0].Rows[j]["FromDate"].ToString(), out result11);
						DateTime.TryParse(currentData.Tables[0].Rows[j]["ToDate"].ToString(), out result12);
						string text = currentData.Tables[0].Rows[j]["LeaveTypeID"].ToString();
						string text2 = currentData.Tables[0].Rows[j]["EmployeeID"].ToString();
						if (a != text2)
						{
							list.Clear();
							num2 = 0;
						}
						a = text2;
						result9 = bool.Parse(currentData.Tables[0].Rows[j]["IsAnnual"].ToString());
						DataView defaultView = currentData.Tables[1].DefaultView;
						if (result9 && result11 != d && result12 != d)
						{
							defaultView.RowFilter = "EmployeeID='" + text2.ToString() + "' AND FromDate='" + result11.ToString() + "' AND ToDate='" + result12.ToString() + "' AND LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
						}
						else if (result9 && result11 == d && result12 == d)
						{
							defaultView.RowFilter = "EmployeeID='" + text2.ToString() + "' AND LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
						}
						else if (!result9)
						{
							defaultView.RowFilter = "EmployeeID='" + text2.ToString() + "' AND LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
						}
						DataSet dataSet = new DataSet();
						DataTable dataTable = defaultView.ToTable();
						dataSet.Tables.Add(dataTable);
						bool flag = false;
						for (int k = 0; k < list.Count; k++)
						{
							DateTime start = list[k].Start;
							DateTime end = list[k].End;
							if (result11 >= start && result12 <= end)
							{
								flag = true;
							}
						}
						object obj;
						object obj2;
						if (result11 != d && result12 != d && !flag)
						{
							DataTable dataTable2 = new DataTable();
							dataTable2 = dataTable.DefaultView.ToTable(true, "DaysTaken");
							new DataTable();
							DataTable dataTable3 = dataTable.DefaultView.ToTable(true, "ToLessTaken");
							obj = dataTable2.Compute("Sum(DaysTaken)", "DaysTaken <> 0");
							obj2 = dataTable3.Compute("Sum(ToLessTaken)", "ToLessTaken <> 0");
							list.Add(new Leavelist(result11, result12));
						}
						else
						{
							obj = DBNull.Value;
							obj2 = DBNull.Value;
						}
						if (obj != DBNull.Value && obj2 != DBNull.Value)
						{
							currentData.Tables[0].Rows[j]["TotalTaken"] = int.Parse(obj.ToString()) - int.Parse(obj2.ToString());
						}
						else if (obj != DBNull.Value)
						{
							currentData.Tables[0].Rows[j]["TotalTaken"] = obj;
						}
						else if (obj == DBNull.Value && result9)
						{
							currentData.Tables[0].Rows[j]["TotalTaken"] = 0;
						}
						int.TryParse(currentData.Tables[0].Rows[j]["TotalTaken"].ToString(), out result10);
						int.TryParse(currentData.Tables[0].Rows[j]["AnnualAllowedDays"].ToString(), out result7);
						if (result9)
						{
							result6 = 0;
						}
						currentData.Tables[0].Rows[j]["TotalLeaves"] = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7;
						if (result9 && num2 != result10 && !flag)
						{
							num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result10 + MinusLeaves;
							num2 = result10;
						}
						else if (!result9)
						{
							num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result5;
						}
						else if ((result9 && num2 != result10) & flag)
						{
							num = (decimal)result7 + MinusLeaves;
							currentData.Tables[0].Rows[j]["TotalTaken"] = DBNull.Value;
						}
						else if (result9 && num2 == result10 && !flag)
						{
							num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result10 + MinusLeaves;
						}
						else if (result9 && num2 == 0 && result10 == 0)
						{
							num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result10 + MinusLeaves;
						}
						if (num >= 0m && result9 && MinusLeaves != 0m)
						{
							currentData.Tables[0].Rows[j]["TotalTaken"] = -1m * MinusLeaves;
							currentData.Tables[0].Rows[j]["LeavesRemaining"] = num;
							MinusLeaves = 0m;
						}
						else if (num >= 0m && result9 && MinusLeaves == 0m)
						{
							currentData.Tables[0].Rows[j]["LeavesRemaining"] = num;
						}
						else if (num < 0m && result9)
						{
							currentData.Tables[0].Rows[j]["TotalTaken"] = result7;
							currentData.Tables[0].Rows[j]["LeavesRemaining"] = 0;
							MinusLeaves = num;
						}
						else if (!result9)
						{
							currentData.Tables[0].Rows[j]["LeavesRemaining"] = num;
						}
						if (!(result11.Date < startDate.Date) || result12.Date < startDate.Date)
						{
						}
						if (result7 == 0 && !list2.Contains(currentData.Tables[0].Rows[j]["LeaveTypeID"].ToString()) && !list3.Contains(currentData.Tables[0].Rows[j]["EmployeeID"].ToString()))
						{
							list2.Add(currentData.Tables[0].Rows[j]["LeaveTypeID"].ToString());
							list3.Add(currentData.Tables[0].Rows[j]["EmployeeID"].ToString());
						}
						int result13 = 0;
						int.TryParse(currentData.Tables[0].Rows[j]["LeavesRemaining"].ToString(), out result13);
					}
					currentData.Tables[1].Columns.Remove("FromDate");
					currentData.Tables[1].Columns.Remove("ToDate");
					currentData.AcceptChanges();
				}
				if (currentData.Tables.Count > 1)
				{
					currentData.Relations.Add("AnnualLeaveDueDetails", new DataColumn[2]
					{
						currentData.Tables[0].Columns["EmployeeID"],
						currentData.Tables[0].Columns["LeaveTypeID"]
					}, new DataColumn[2]
					{
						currentData.Tables[1].Columns["EmployeeID"],
						currentData.Tables[1].Columns["LeaveTypeID"]
					}, createConstraints: false);
				}
			}
			return currentData;
		}

		public byte[] GetEmployeeThumbnailImage(string employeeID)
		{
			string exp = "SELECT Photo\r\n\t\t\t\t\t\t   FROM Employee P WHERE EmployeeID='" + employeeID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return (byte[])obj;
			}
			return null;
		}

		private bool ThumbnailImageAbort()
		{
			return true;
		}

		public bool AddEmployeePhoto(string employeeID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Employee SET Photo=@Photo WHERE EmployeeID='" + employeeID + "'");
				sqlCommand.Parameters.AddWithValue("@Photo", image);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return result;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool RemoveEmployeePhoto(string employeeID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Employee SET Photo= Null WHERE EmployeeID='" + employeeID + "'");
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool IsEmployeeSettled(string employeeID)
		{
			bool result = false;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				result = bool.Parse(new Databases(base.DBConfig).GetFieldValue("Employee", "IsEOSSettled", "EmployeeID", employeeID, sqlTransaction).ToString());
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public DataSet GetEmployeeFinalSettlement(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, DateTime asOfDate, string EmployeeIDs)
		{
			CommonLib.ToSqlDateTimeString(asOfDate);
			string text = "SELECT E.EmployeeID ,FirstName,MiddleName,LastName, NickName, BirthDate,E.LocationID,PositionName,Photo,Balance,\r\n                            CASE Status WHEN 1 THEN 'A' ELSE 'Terminated' END AS [Status] , CAST((DATEDIFF(m, BirthDate, GETDATE())/12) as varchar) AS Age, JoiningDate,\r\n                            CAST((DATEDIFF(m, JoiningDate, GETDATE())/12) as varchar) + ' Year & ' + CAST((DATEDIFF(m, JoiningDate, GETDATE())%12) as varchar) + ' Month' AS ServicePeriodMonth,\r\n                            ISNULL(OnVacation,'False') AS OnVacation,SponsorName,NationalityID,ReligionName,\r\n                            CAST((DATEDIFF(m, ISNULL(ISNULL(ResumedDate,AnnualLeaveDate),JoiningDate), GETDATE())/12) as varchar) + ' Year & ' + CAST((DATEDIFF(m, ISNULL(ISNULL(ResumedDate,AnnualLeaveDate),JoiningDate), GETDATE())%12) as varchar) + ' Month' AS CurrentServicePeriod,\r\n                            CASE MaritalStatus WHEN 1 THEN 'NA' WHEN 2 THEN 'Single' WHEN 3 THEN 'Married' WHEN 4 THEN 'Divorced' WHEN 5 THEN 'Widow' END AS MaritalStatus,\r\n                            Phone1,Mobile,Email,Notes,DepartmentName,GroupName,Gender\r\n                            FROM Employee E LEFT OUTER JOIN  Position EP ON E.PositionID=EP.PositionID\r\n                            LEFT OUTER JOIN Employee_Grade Grade ON Grade.GradeID=E.GradeID\r\n                            LEFT OUTER JOIN Sponsor ON Sponsor.SponsorID=E.SponsorID\r\n                            LEFT OUTER JOIN Religion ON Religion.ReligionID=E.ReligionID\r\n                            LEFT OUTER JOIN Division ON Division.DivisionID=E.DivisionID\r\n                            LEFT OUTER JOIN Department ON Department.DepartmentID=E.DepartmentID\r\n                            LEFT OUTER JOIN Employee_Group EG ON EG.GroupID=E.GroupID\r\n                            LEFT OUTER JOIN Location  ON Location.LocationID=E.LocationID\r\n                            LEFT OUTER JOIN Employee_Address EA ON EA.EmployeeID=E.EmployeeID AND AddressID='PRIMARY' WHERE 1=1 ";
			if (EmployeeIDs != "")
			{
				text = text + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text = text + " AND E.EmployeeID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text = text + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text = text + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text = text + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text = text + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text = text + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text = text + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text = text + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text = text + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text = text + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text = text + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text = text + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text = text + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				text = text + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				text = text + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text = text + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text = text + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text = text + " AND E.AccountID <='" + toAccount + "' ";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee", text);
			text = "SELECT *  FROM Employee_EOSSettlement E WHERE 1=1 ";
			if (fromEmployee != "")
			{
				text = text + " AND E.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			text += " ORDER BY E.EmployeeID";
			FillDataSet(dataSet, "EOSDetails", text);
			dataSet.Relations.Add("RelEOSDetails", dataSet.Tables["Employee"].Columns["EmployeeID"], dataSet.Tables["EOSDetails"].Columns["EmployeeID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetEventEmployeeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EmployeeID [Doc ID],ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Number]                           \r\n                           FROM Employee ORDER BY EmployeeID,FirstName";
			FillDataSet(dataSet, "Employee", textCommand);
			return dataSet;
		}

		public DataSet GetActiveEmployeeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EmployeeID [Code],ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name]                           \r\n                           FROM Employee WHERE Status=1 ORDER BY EmployeeID,FirstName ";
			FillDataSet(dataSet, "Employee", textCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SSD.EmployeeID, FirstName + ' ' + LastName[Employee Name], SS.Month[MonthVal],\r\n\t                        CASE[Month] WHEN 1 THEN 'January' WHEN 2 THEN 'February' WHEN 3 THEN 'March' WHEN 4 THEN 'April'\r\n\r\n                                WHEN 5 THEN 'May' WHEN 6 THEN 'June' WHEN 7 THEN 'July' WHEN 8 THEN 'August'\r\n\r\n                                WHEN 9 THEN 'September' WHEN 10 THEN 'October' WHEN 11 THEN 'Nov' WHEN 12 THEN 'December' END AS[Month],\r\n\r\n                                [Year], Basic, Allowance, 0 AS[Other Benefits], Deduction, PaidAmount\r\n                       FROM SalarySheet SS\r\n                        INNER JOIN SalarySheet_Detail SSD ON SS.SysDocID = SSD.SysDocID AND SS.VoucherID = SSD.VoucherID\r\n                        INNER JOIN Employee E ON SSD.EmployeeID = E.EmployeeID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee", sqlCommand);
			return dataSet;
		}

		public DataSet GetHRLetterReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int month, int year, string EmployeeIDs, string strGroupBy)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT [EmployeeID] \r\n                            , [FirstName] + ' ' + [MiddleName] + ' ' + [LastName] FullName\r\n                            , [FirstName] , [MiddleName] , [LastName] ,FORMAT ([BirthDate] , 'dd-MMM-yyyy') [BirthDate] ,  FORMAT ([JoiningDate] , 'dd-MMM-yyyy') [JoiningDate], [Photo] , [IsTerminated] \r\n                            , FORMAT ([TerminationDate] , 'dd-MMM-yyyy')[TerminationDate] , [TerminationID] , [IsCancelled] , FORMAT ([CancellationDate] , 'dd-MMM-yyyy') [CancellationDate] , FORMAT ([RehireDate] , 'dd-MMM-yyyy') [RehireDate] , [IsEOSSettled] , [GradeID] , [OnVacation] \r\n                            , [SponsorID] , [NationalityID] , [UID] , [VisaNumber] , [Probation] ,FORMAT ([ConfirmationDate] , 'dd-MMM-yyyy') [ConfirmationDate] ,FORMAT ([AnnualLeaveDate] , 'dd-MMM-yyyy') [AnnualLeaveDate] ,FORMAT ([ResumedDate] , 'dd-MMM-yyyy') [ResumedDate] , [LocationID] \r\n                            , D.DepartmentName , P.PositionName , E.[GroupID] , EG.GroupName,  [ReportToID] , [Gender] \r\n                            , case gender when 'M' then 'His' else 'Her' end as GenderHisHer\r\n                            , case gender when 'M' then 'He' else 'She' end as GenderHeShe\r\n                            , case gender when 'M' then 'him' else 'her' end as Genderhimher\r\n                            , [NationalID] \r\n                            , [Status] \r\n                            , [LabourID] \r\n                            ,  (select   isnull(sum(amount),0) from Employee_PayrollItem_Detail EPD where paytype=1 and epd.EmployeeID = e.EmployeeID ) GrossSalary,'' as AmountInWords\r\n                            FROM [dbo].[Employee] E\r\n                            LEFT JOIN Department D ON E.DepartmentID = D.DepartmentID \r\n                            LEFT JOIN Position P ON E.PositionID = P.PositionID \r\n                            LEFT JOIN Employee_Group EG ON E.GroupID = EG.GroupID WHERE 1=1 ";
				if (EmployeeIDs != "")
				{
					str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					str = str + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					str = str + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					str = str + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					str = str + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					str = str + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					str = str + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					str = str + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					str = str + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					str = str + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					str = str + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPosition != "")
				{
					str = str + " AND E.PositionID <='" + toPosition + "' ";
				}
				if (fromBank != "")
				{
					str = str + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					str = str + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					str = str + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					str = str + " AND E.AccountID <='" + toAccount + "' ";
				}
				str += "  ORDER BY E.EmployeeID ";
				FillDataSet(dataSet, "Employee", str);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Employee"].Rows.Count == 0)
				{
					return null;
				}
				if (strGroupBy.Equals("1") || strGroupBy.Equals("2") || strGroupBy.Equals("3") || strGroupBy.Equals("4"))
				{
					str = "SELECT E.EmployeeID , PayrollItem.PayrollItemID , PayrollItem.PayrollItemName PayType ,isnull(E.Amount,0) as Amount\r\n                            FROM Employee_PayrollItem_Detail  E\r\n                            INNER JOIN Employee E2 ON E.EmployeeID=E2.EmployeeID\r\n                        INNER JOIN PayrollItem ON E.PayrollItemID = PayrollItem.PayrollItemID ";
					if (EmployeeIDs != "")
					{
						str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
					}
					if (fromEmployee != "")
					{
						str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
					}
					if (toEmployee != "")
					{
						str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
					}
					if (fromDepartment != "")
					{
						str = str + " AND E2.DepartmentID>='" + fromDepartment + "' ";
					}
					if (toDepartment != "")
					{
						str = str + " AND E2.EmployeeID<='" + toDepartment + "' ";
					}
					if (fromLocation != "")
					{
						str = str + " AND E2.LocationID>='" + fromLocation + "' ";
					}
					if (toLocation != "")
					{
						str = str + " AND E2.LocationID<='" + toLocation + "' ";
					}
					if (fromType != "")
					{
						str = str + " AND E2.ContractType >='" + fromType + "' ";
					}
					if (toType != "")
					{
						str = str + " AND E2.ContractType <='" + toType + "' ";
					}
					if (fromDivision != "")
					{
						str = str + " AND E2.DivisionID >='" + fromDivision + "' ";
					}
					if (toDivision != "")
					{
						str = str + " AND E2.DivisionID <='" + toDivision + "' ";
					}
					if (fromSponsor != "")
					{
						str = str + " AND E2.SponsorID >='" + fromSponsor + "' ";
					}
					if (toSponsor != "")
					{
						str = str + " AND E2.SponsorID <='" + toSponsor + "' ";
					}
					if (fromGroup != "")
					{
						str = str + " AND E2.GroupID >='" + fromGroup + "' ";
					}
					if (toGroup != "")
					{
						str = str + " AND E2.GroupID <='" + toGroup + "' ";
					}
					if (fromGrade != "")
					{
						str = str + " AND E2.GradeID >='" + fromGrade + "' ";
					}
					if (toGrade != "")
					{
						str = str + " AND E2.GradeID <='" + toGrade + "' ";
					}
					if (fromPosition != "")
					{
						str = str + " AND E2.PositionID >='" + fromPosition + "' ";
					}
					if (toPosition != "")
					{
						str = str + " AND E2.PositionID <='" + toPosition + "' ";
					}
					if (fromBank != "")
					{
						str = str + " AND E2.BankID >='" + fromBank + "' ";
					}
					if (toBank != "")
					{
						str = str + " AND E2.BankID <='" + toBank + "' ";
					}
					if (fromAccount != "")
					{
						str = str + " AND E2.AccountID >='" + fromAccount + "' ";
					}
					if (toAccount != "")
					{
						str = str + " AND E2.AccountID <='" + toAccount + "' ";
					}
					FillDataSet(dataSet, "Salary_Details", str);
					dataSet.Relations.Add("RelEOSDetails", dataSet.Tables["Employee"].Columns["EmployeeID"], dataSet.Tables["Salary_Details"].Columns["EmployeeID"], createConstraints: false);
					int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
					foreach (DataRow row in dataSet.Tables["Employee"].Rows)
					{
						row["AmountInWords"] = NumToWord.GetNumInWords(decimal.Parse((row["GrossSalary"].ToString() == "") ? "0" : row["GrossSalary"].ToString()), currencyDecimalPoints);
					}
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeList(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT EmployeeID,FirstName+' '+MiddleName+' '+LastName as EmployeeName FROM Employee E WHERE 1=1 ";
			if (EmployeeIDs != "")
			{
				text = text + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text = text + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text = text + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text = text + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text = text + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text = text + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text = text + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text = text + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text = text + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text = text + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text = text + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text = text + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text = text + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text = text + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				text = text + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				text = text + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text = text + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text = text + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text = text + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Status,1) = 1";
			}
			FillDataSet(dataSet, "Employee", text);
			return dataSet;
		}
	}
}
