using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CandidateData : DataSet
	{
		public const string CANDIDATEID_FIELD = "CandidateID";

		public const string PASSPORTNO_FIELD = "PassportNo";

		public const string APPLTYPE_FIELD = "ApplType";

		public const string GIVENNAME_FIELD = "GivenName";

		public const string SURNAME_FIELD = "SurName";

		public const string FATHERNAME_FIELD = "FatherName";

		public const string MOTHERNAME_FIELD = "MotherName";

		public const string SPOUSENAME_FIELD = "SpouseName";

		public const string PASSPORTPLACEOFISSUE_FIELD = "PassportPlaceOfIssue";

		public const string PASSPORTISSUEDATE_FIELD = "PassportIssueDate";

		public const string PASSPORTEXPIRYDATE_FIELD = "PassportExpiryDate";

		public const string PASSPORTADDRESS_FIELD = "PassportAddress";

		public const string ECRSTATUS_FIELD = "ECRStatus";

		public const string SYSTEMDATE_FIELD = "SystemDate";

		public const string SELECTIONSTATUS_FIELD = "SelectionStatus";

		public const string SELECTEDON_FIELD = "SelectedOn";

		public const string SELECTEDAT_FIELD = "SelectedAt";

		public const string THROUGHAGENT_FIELD = "ThroughAgent";

		public const string APPLICATIONTYPINGDATEMOL_FIELD = "ApplicationTypingDateMOL";

		public const string MBNUMBERMOL_FIELD = "MBNumberMOL";

		public const string APPROVALDATEMOL_FIELD = "ApprovalDateMOL";

		public const string BGPAIDONMOL_FIELD = "BGPaidOnMOL";

		public const string VISAAPPLIEDTHROUGHIMG_FIELD = "VisaAppliedThroughIMG";

		public const string VISAPOSTEDONIMG_FIELD = "VisaPostedOnIMG";

		public const string VISAAPPROVEDONIMG_FIELD = "VisaApprovedOnIMG";

		public const string VISADELIVEREDONIMG_FIELD = "VisaDeliveredOnIMG";

		public const string ARRIVEDON_FIELD = "ArrivedOn";

		public const string EMPLOYEENO_FIELD = "EmployeeNo";

		public const string MEDICALTYPINGON_FIELD = "MedicalTypingOn";

		public const string MEDICALATTENDEDON_FIELD = "MedicalAttendedOn";

		public const string MEDICALCOLLECTEDON_FIELD = "MedicalCollectedOn";

		public const string APPLICATIONTYPEDONEID_FIELD = "ApplicationTypedOnEID";

		public const string ATTENDEDFOREID_FIELD = "AttendedForEID";

		public const string COLLECTEDONEID_FIELD = "CollectedOnEID";

		public const string APPLICATIONPOSTEDONRP_FIELD = "ApplicationPostedOnRP";

		public const string APPLICATIONAPPROVEDONRP_FIELD = "ApplicationApprovedOnRP";

		public const string SUBMITTEDTOZAJIL_FIELD = "SubmittedToZajil";

		public const string PASSPORTCOLLECTEDON_FIELD = "PassportCollectedOn";

		public const string RPISSUEDATE_FIELD = "RPIssueDate";

		public const string RPEXPIRYDATE_FIELD = "RPExpiryDate";

		public const string RPISSUEPLACE_FIELD = "RPIssuePlace";

		public const string PROCESSTYPERP_FIELD = "RPProcessType";

		public const string AOTYPINGDATE_FIELD = "AOTypingDate";

		public const string AOREGNUMBER_FIELD = "AORegNumber";

		public const string AGTTYPEDON_FIELD = "AGTTypedOn";

		public const string AGTSUBMITTEDON_FIELD = "AGTSubmittedOn";

		public const string WPNO_FIELD = "WPNumber";

		public const string PERSONALIDNO_FIELD = "PersonalIDNo";

		public const string WPISSUEDATE_FIELD = "WPIssueDate";

		public const string WPEXPIRYDATE_FIELD = "WPExpiryDate";

		public const string WPISSUEPLACE_FIELD = "WPIssuePlace";

		public const string APPROVALSTATUSMOL_FIELD = "ApprovalStatusMOL";

		public const string MOLREMARKS_FIELD = "MOLRemarks";

		public const string EXPECTEDARRIVALDATE_FIELD = "ExpectedArrivaldate";

		public const string IMGREMARKS_FIELD = "IMGRemarks";

		public const string APPROVALSTATUSIMG_FIELD = "ApprovalStatusIMG";

		public const string VISACOPYTOAGENTON_FIELD = "VisaCopyToAgentOn";

		public const string APPROVALVALIDTILLMOL_FIELD = "ApprovalValidTillMOL";

		public const string TEMPWPNO_FIELD = "TempWPNumber";

		public const string APPROVALFEEPAIDONMOL_FIELD = "ApprovalFeePaidOnMOL";

		public const string BGTYPEMOL_FIELD = "BGTypeMOL";

		public const string VISAISSUEPLACEIMG_FIELD = "VisaIssuePlaceIMG";

		public const string VISANUMBER_FIELD = "VisaNumber";

		public const string VISAISSUEDATEIMG_FIELD = "VisaIssueDateIMG";

		public const string VISAEXPIRYDATEIMG_FIELD = "VisaExpiryDateIMG";

		public const string UIDNUMBERIMG_FIELD = "UIDNumberIMG";

		public const string ARRIVALPORT_FIELD = "ArrivalPort";

		public const string CATEGORY_FIELD = "Category";

		public const string MEDICALRESULT_FIELD = "MedicalResult";

		public const string MEDICALNOTE_FIELD = "MedicalNote";

		public const string SPONSORID_FIELD = "SponsorID";

		public const string AGTTYPE_FIELD = "AGTType";

		public const string MBNUMBERAGT_FIELD = "MBNumberAGT";

		public const string HEALTHCARDNO_FIELD = "HealthCardNo";

		public const string ABSREGDATEMOL_FIELD = "ABSRegDateMOL";

		public const string ABSAPPRDATEMOL_FIELD = "ABSApprDateMOL";

		public const string ABSAPPRDATEIMG_FIELD = "ABSApprDateIMG";

		public const string ABSREFNO_FIELD = "ABSRefNo";

		public const string APPRECEIVEDON_FIELD = "ApprReceivedOn";

		public const string SUBMITTEDTOMOL_FIELD = "SubmittedToMOL";

		public const string DEPARTUREDATE_FIELD = "DepartureDate";

		public const string APPTYPEONMOL_FIELD = "AppTypedOnMOL";

		public const string RPCANCELLEDONIMG_FIELD = "RPCancelledOnIMG";

		public const string EXITPORT_FIELD = "ExitPort";

		public const string UID_FIELD = "UID";

		public const string NATIONALID_FIELD = "NationalID";

		public const string NATIONALIDVALIDITY_FIELD = "NationalIDValidity";

		public const string RELIGIONID_FIELD = "ReligionID";

		public const string BIRTHDATE_FIELD = "BirthDate";

		public const string BIRTHPLACE_FIELD = "BirthPlace";

		public const string NATIONALITYID_FIELD = "NationalityID";

		public const string BLOODGROUP_FIELD = "BloodGroup";

		public const string CANCELLATIONDATE_FIELD = "CancellationDate";

		public const string VISADESIGNATION_FIELD = "VisaDesignation";

		public const string ACTUALDESIGNATION_FIELD = "ActualDesignation";

		public const string NOTES_FIELD = "Notes";

		public const string REMARKS_FIELD = "Remarks";

		public const string DESIGNATIONID_FIELD = "DesignationID";

		public const string VISASTATUS_FIELD = "VisaStatus";

		public const string GENDER_FIELD = "Gender";

		public const string ISCANCELLED_FIELD = "IsCancelled";

		public const string STAGE_FIELD = "CancellationStage";

		public const string VCAPPRECEIVEDDATE_FIELD = "VCAppReceivedDate";

		public const string APPCANCELLATIONDATE_FIELD = "AppCancellationDate";

		public const string IMGCANCELLATIONDATE_FIELD = "IMGCancellationDate";

		public const string MOLCANCELLATIONDATE_FIELD = "MOLCancellationDate";

		public const string MBNUMBERCANCEL_FIELD = "MBNumberCancel";

		public const string SIGNEDAORECEIVEDDATE_FIELD = "SignedAOrecvdDate";

		public const string SIGNEDAGTRECEIVEDDATE_FIELD = "SignedAGTrecvdDate";

		public const string REASON_FIELD = "CancellationReason";

		public const string CANCELLATIONREMARKS_FIELD = "CancellationRemarks";

		public const string MARITALSTATUS_FIELD = "MaritalStatus";

		public const string STATUS_FIELD = "Status";

		public const string PRIMARYADDRESSID_FIELD = "PrimaryAddressID";

		public const string BASICSALARY_FIELD = "BasicSalary";

		public const string CURRENCY_FIELD = "CurrencyID";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string SALARYREMARKS_FIELD = "SalaryRemarks";

		public const string EOSRULEID_FIELD = "EOSRuleID";

		public const string OVERTIMEID_FIELD = "OverTimeID";

		public const string BALANCE_FIELD = "Balance";

		public const string GROUPID_FIELD = "GroupID";

		public const string QUALIFICATION_FIELD = "QualificationID";

		public const string LANGUAGE_FIELD = "LanguageID";

		public const string EXPERIENCELOCAL_FIELD = "ExperienceLocal";

		public const string EXPERIENCEABROAD_FIELD = "ExperienceAbroad";

		public const string ISEXIST_FIELD = "IsExist";

		public const string USERDEFINED1_FIELD = "UserDefined1";

		public const string USERDEFINED2_FIELD = "UserDefined2";

		public const string USERDEFINED3_FIELD = "UserDefined3";

		public const string USERDEFINED4_FIELD = "UserDefined4";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CANDIDATE_TABLE = "Candidate";

		public const string CANDIDATEADDRESS_TABLE = "Candidate_Address";

		public const string AGENT_TABLE = "Agent";

		public const string AGENTID_FIELD = "AgentID";

		public const string AGENTNAME_FIELD = "AgentName";

		public const string CANDIDATE_PAYROLLITEMDETAIL_TABLE = "Candidate_PayrollItem_Detail";

		public const string CANDIDATESALARY_TABLE = "Candidate_Salary";

		public const string PAYTYPE_FIELD = "PayType";

		public const string PAYROLLITEMID_FIELD = "PayrollItemID";

		public const string AMOUNT_FIELD = "Amount";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CANDIDATEBENEFITDETAIL_TABLE = "Candidate_Benefit_Detail";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string BENEFITID_FIELD = "BenefitID";

		public const string LASTAMOUNT_FIELD = "LastAmount";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string AGREEMENT_FIELD = "AgreementStatus";

		public const string SPECIALCONDITION_FIELD = "SpecialCondition";

		public DataTable CandidateBenefitDetailTable => base.Tables["Candidate_Benefit_Detail"];

		public DataTable CandidateTable => base.Tables["Candidate"];

		public DataTable EmployeeTable => base.Tables["Employee"];

		public DataTable EmployeeAddressTable => base.Tables["Employee_Address"];

		public DataTable CandidateSalaryTable => base.Tables["Candidate_Salary"];

		public DataTable EmployeePayrollItemDetail => base.Tables["Employee_PayrollItem_Detail"];

		public DataTable EmployeeBenefit => base.Tables["Employee_Benefit_Detail"];

		public CandidateData()
		{
			BuildDataTables();
		}

		public CandidateData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Candidate");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CandidateID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PassportNo", typeof(string));
			columns.Add("GivenName", typeof(string));
			columns.Add("SurName", typeof(string));
			columns.Add("NationalityID", typeof(string));
			columns.Add("ApplType", typeof(string));
			columns.Add("DesignationID", typeof(string));
			columns.Add("VisaStatus", typeof(string));
			columns.Add("Gender", typeof(char));
			columns.Add("BirthDate", typeof(DateTime));
			columns.Add("BirthPlace", typeof(string));
			columns.Add("PassportPlaceOfIssue", typeof(string));
			columns.Add("PassportIssueDate", typeof(DateTime));
			columns.Add("PassportExpiryDate", typeof(DateTime));
			columns.Add("FatherName", typeof(string));
			columns.Add("MotherName", typeof(string));
			columns.Add("SpouseName", typeof(string));
			columns.Add("PassportAddress", typeof(string));
			columns.Add("Notes", typeof(string));
			columns.Add("IsExist", typeof(bool));
			columns.Add("ECRStatus", typeof(string));
			columns.Add("SystemDate", typeof(DateTime));
			columns.Add("GroupID", typeof(string));
			columns.Add("SelectionStatus", typeof(string));
			columns.Add("SelectedOn", typeof(DateTime));
			columns.Add("SelectedAt", typeof(string));
			columns.Add("ThroughAgent", typeof(string));
			columns.Add("VisaCopyToAgentOn", typeof(DateTime));
			columns.Add("VisaDesignation", typeof(string));
			columns.Add("ActualDesignation", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("QualificationID", typeof(string));
			columns.Add("LanguageID", typeof(string));
			columns.Add("ExperienceLocal", typeof(decimal));
			columns.Add("ExperienceAbroad", typeof(decimal));
			columns.Add("CancellationDate", typeof(DateTime));
			columns.Add("DivisionID", typeof(string));
			columns.Add("AgreementStatus", typeof(string));
			columns.Add("SpecialCondition", typeof(string));
			columns.Add("ApplicationTypingDateMOL", typeof(DateTime));
			columns.Add("MBNumberMOL", typeof(string));
			columns.Add("SponsorID", typeof(string));
			columns.Add("ApprovalDateMOL", typeof(DateTime));
			columns.Add("SignedAOrecvdDate", typeof(DateTime));
			columns.Add("ApprovalValidTillMOL", typeof(DateTime));
			columns.Add("TempWPNumber", typeof(string));
			columns.Add("ApprovalFeePaidOnMOL", typeof(DateTime));
			columns.Add("BGPaidOnMOL", typeof(DateTime));
			columns.Add("BGTypeMOL", typeof(string));
			columns.Add("AOTypingDate", typeof(DateTime));
			columns.Add("AORegNumber", typeof(string));
			columns.Add("ApprovalStatusMOL", typeof(string));
			columns.Add("MOLRemarks", typeof(string));
			columns.Add("VisaAppliedThroughIMG", typeof(string));
			columns.Add("VisaPostedOnIMG", typeof(DateTime));
			columns.Add("VisaApprovedOnIMG", typeof(DateTime));
			columns.Add("VisaIssuePlaceIMG", typeof(string));
			columns.Add("VisaNumber", typeof(string));
			columns.Add("VisaIssueDateIMG", typeof(DateTime));
			columns.Add("VisaExpiryDateIMG", typeof(DateTime));
			columns.Add("UIDNumberIMG", typeof(string));
			columns.Add("ApprovalStatusIMG", typeof(string));
			columns.Add("ExpectedArrivaldate", typeof(DateTime));
			columns.Add("IMGRemarks", typeof(string));
			columns.Add("ArrivedOn", typeof(DateTime));
			columns.Add("ArrivalPort", typeof(string));
			columns.Add("Category", typeof(string));
			columns.Add("EmployeeNo", typeof(string));
			columns.Add("HealthCardNo", typeof(string));
			columns.Add("MedicalTypingOn", typeof(DateTime));
			columns.Add("MedicalAttendedOn", typeof(DateTime));
			columns.Add("MedicalCollectedOn", typeof(DateTime));
			columns.Add("MedicalResult", typeof(string));
			columns.Add("MedicalNote", typeof(string));
			columns.Add("ApplicationTypedOnEID", typeof(DateTime));
			columns.Add("AttendedForEID", typeof(DateTime));
			columns.Add("CollectedOnEID", typeof(DateTime));
			columns.Add("NationalID", typeof(string));
			columns.Add("NationalIDValidity", typeof(DateTime));
			columns.Add("AGTType", typeof(string));
			columns.Add("MBNumberAGT", typeof(string));
			columns.Add("AGTTypedOn", typeof(DateTime));
			columns.Add("AGTSubmittedOn", typeof(DateTime));
			columns.Add("SignedAGTrecvdDate", typeof(DateTime));
			columns.Add("WPNumber", typeof(string));
			columns.Add("PersonalIDNo", typeof(string));
			columns.Add("WPIssuePlace", typeof(string));
			columns.Add("WPIssueDate", typeof(DateTime));
			columns.Add("WPExpiryDate", typeof(DateTime));
			columns.Add("RPProcessType", typeof(string));
			columns.Add("ApplicationPostedOnRP", typeof(DateTime));
			columns.Add("ApplicationApprovedOnRP", typeof(DateTime));
			columns.Add("SubmittedToZajil", typeof(DateTime));
			columns.Add("PassportCollectedOn", typeof(DateTime));
			columns.Add("RPIssuePlace", typeof(string));
			columns.Add("RPIssueDate", typeof(DateTime));
			columns.Add("RPExpiryDate", typeof(DateTime));
			columns.Add("IsCancelled", typeof(bool));
			columns.Add("CancellationStage", typeof(string));
			columns.Add("VCAppReceivedDate", typeof(DateTime));
			columns.Add("AppCancellationDate", typeof(DateTime));
			columns.Add("IMGCancellationDate", typeof(DateTime));
			columns.Add("MOLCancellationDate", typeof(DateTime));
			columns.Add("MBNumberCancel", typeof(string));
			columns.Add("CancellationReason", typeof(string));
			columns.Add("CancellationRemarks", typeof(string));
			columns.Add("PrimaryAddressID", typeof(string));
			columns.Add("UserDefined1", typeof(string));
			columns.Add("UserDefined2", typeof(string));
			columns.Add("UserDefined3", typeof(string));
			columns.Add("UserDefined4", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee");
			DataColumnCollection columns2 = dataTable.Columns;
			DataColumn dataColumn2 = columns2.Add("EmployeeID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn2
			};
			columns2.Add("LastName", typeof(string));
			columns2.Add("FirstName", typeof(string));
			columns2.Add("MiddleName", typeof(string));
			columns2.Add("BirthDate", typeof(DateTime));
			columns2.Add("NickName", typeof(string));
			columns2.Add("JoiningDate", typeof(DateTime));
			columns2.Add("GroupID", typeof(string));
			columns2.Add("TerminationDate", typeof(DateTime));
			columns2.Add("TerminationID", typeof(int));
			columns2.Add("IsTerminated", typeof(bool));
			columns2.Add("GradeID", typeof(string));
			columns2.Add("DayOff", typeof(short));
			columns2.Add("OnVacation", typeof(bool));
			columns2.Add("BirthPlace", typeof(string));
			columns2.Add("SponsorID", typeof(string));
			columns2.Add("NationalityID", typeof(string));
			columns2.Add("Probation", typeof(short));
			columns2.Add("ConfirmationDate", typeof(DateTime));
			columns2.Add("ReligionID", typeof(string));
			columns2.Add("BloodGroup", typeof(string));
			columns2.Add("Qualification", typeof(string));
			columns2.Add("ContractType", typeof(string));
			columns2.Add("AnnualLeaveDate", typeof(DateTime));
			columns2.Add("ResumedDate", typeof(DateTime));
			columns2.Add("Notes", typeof(string));
			columns2.Add("LocationID", typeof(string));
			columns2.Add("DivisionID", typeof(string));
			columns2.Add("DepartmentID", typeof(string));
			columns2.Add("PositionID", typeof(string));
			columns2.Add("ReportToID", typeof(string));
			columns2.Add("PayPeriod", typeof(short));
			columns2.Add("Gender", typeof(char));
			columns2.Add("MaritalStatus", typeof(short));
			columns2.Add("SpouseName", typeof(string));
			columns2.Add("AccountID", typeof(string));
			columns2.Add("BankID", typeof(string));
			columns2.Add("NationalID", typeof(string));
			columns2.Add("Status", typeof(short));
			columns2.Add("LabourID", typeof(string));
			columns2.Add("IBAN", typeof(string));
			columns2.Add("UID", typeof(string));
			columns2.Add("VisaNumber", typeof(string));
			columns2.Add("LastRevisedSalaryDate", typeof(DateTime));
			columns2.Add("PrimaryAddressID", typeof(string));
			columns2.Add("UserDefined1", typeof(string));
			columns2.Add("UserDefined2", typeof(string));
			columns2.Add("UserDefined3", typeof(string));
			columns2.Add("UserDefined4", typeof(string));
			columns2.Add("MedicalInsuranceProviderID", typeof(string));
			columns2.Add("MedicalInsuranceCategoryID", typeof(string));
			columns2.Add("MedicalInsuranceNumber", typeof(string));
			columns2.Add("MedicalInsValidFrom", typeof(DateTime));
			columns2.Add("MedicalInsValidTo", typeof(DateTime));
			columns2.Add("NumberOfDependants", typeof(short));
			columns2.Add("CalendarID", typeof(short));
			columns2.Add("VisaDesignationID", typeof(string));
			columns2.Add("RecruitmentChannelID", typeof(string));
			columns2.Add("MedicalInsuranceAmount", typeof(decimal));
			columns2.Add("EmpAnalysisID", typeof(string));
			columns2.Add("CompanyDivisionID", typeof(string));
			columns2.Add("LegalPositionID", typeof(string));
			columns2.Add("DateCreated", typeof(DateTime));
			columns2.Add("DateUpdated", typeof(DateTime));
			columns2.Add("CreatedBy", typeof(string));
			columns2.Add("UpdatedBy", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Candidate_Salary");
			DataColumnCollection columns3 = dataTable.Columns;
			columns3.Add("CandidateID", typeof(string));
			columns3.Add("Amount", typeof(decimal));
			columns3.Add("PayrollItemID", typeof(string));
			columns3.Add("PayType", typeof(byte));
			columns3.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_PayrollItem_Detail");
			DataColumnCollection columns4 = dataTable.Columns;
			columns4.Add("EmployeeID", typeof(string));
			columns4.Add("PayrollItemID", typeof(string));
			columns4.Add("Amount", typeof(decimal));
			columns4.Add("LastAmount", typeof(decimal));
			columns4.Add("PayType", typeof(byte));
			columns4.Add("StartDate", typeof(DateTime));
			columns4.Add("EndDate", typeof(DateTime));
			columns4.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			EmployeeAddressData employeeAddressData = new EmployeeAddressData();
			dataTable = new DataTable("Employee_Address");
			foreach (DataColumn column in employeeAddressData.EmployeeAddressTable.Columns)
			{
				dataTable.Columns.Add(column.ColumnName, column.DataType);
			}
			base.Tables.Add(dataTable);
			EmployeeBenefitDetailData employeeBenefitDetailData = new EmployeeBenefitDetailData();
			dataTable = new DataTable("Employee_Benefit_Detail");
			foreach (DataColumn column2 in employeeBenefitDetailData.EmployeeBenefitDetailTable.Columns)
			{
				dataTable.Columns.Add(column2.ColumnName, column2.DataType);
			}
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Candidate_Benefit_Detail");
			DataColumnCollection columns5 = dataTable.Columns;
			DataColumn dataColumn5 = columns5.Add("CandidateID", typeof(string));
			dataColumn5.AllowDBNull = false;
			dataColumn5.AutoIncrement = false;
			DataColumn dataColumn6 = columns5.Add("BenefitID", typeof(string));
			dataColumn6.AllowDBNull = false;
			dataColumn6.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn5,
				dataColumn6
			};
			columns5.Add("Amount", typeof(decimal));
			columns5.Add("LastAmount", typeof(decimal));
			columns5.Add("StartDate", typeof(DateTime));
			columns5.Add("EndDate", typeof(DateTime));
			columns5.Add("Remarks", typeof(string));
			columns5.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}

		public static DataSet CreateUpdatePayrollTransactionTable()
		{
			return null;
		}
	}
}
