using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Micromind.Data
{
	public sealed class Candidates : StoreObject
	{
		private const string CANDIDATETABLE_PARM = "Candidate";

		private const string CANDIDATEID_PARM = "@CandidateID";

		private const string PASSPORTNO_PARM = "@PassportNo";

		private const string APPLTYPE_PARM = "@ApplType";

		private const string GIVENNAME_PARM = "@GivenName";

		private const string SURNAME_PARM = "@SurName";

		private const string FATHERNAME_PARM = "@FatherName";

		private const string MOTHERNAME_PARM = "@MotherName";

		private const string SPOUSENAME_PARM = "@SpouseName";

		private const string PASSPORTPLACEOFISSUE_PARM = "@PassportPlaceOfIssue";

		private const string PASSPORTISSUEDATE_PARM = "@PassportIssueDate";

		private const string PASSPORTEXPIRYDATE_PARM = "@PassportExpiryDate";

		private const string PASSPORTADDRESS_PARM = "@PassportAddress";

		private const string ECRSTATUS_PARM = "@ECRStatus";

		private const string SYSTEMDATE_PARM = "@SystemDate";

		private const string SELECTIONSTATUS_PARM = "@SelectionStatus";

		private const string SELECTEDON_PARM = "@SelectedOn";

		private const string SELECTEDAT_PARM = "@SelectedAt";

		private const string THROUGHAGENT_PARM = "@ThroughAgent";

		private const string APPLICATIONTYPINGDATEMOL_PARM = "@ApplicationTypingDateMOL";

		private const string MBNUMBERMOL_PARM = "@MBNumberMOL";

		private const string APPROVALDATEMOL_PARM = "@ApprovalDateMOL";

		private const string CANCELLATIONDATE_PARM = "@CancellationDate";

		private const string BGPAIDONMOL_PARM = "@BGPaidOnMOL";

		private const string VISAAPPLIEDTHROUGHIMG_PARM = "@VisaAppliedThroughIMG";

		private const string VISAPOSTEDONIMG_PARM = "@VisaPostedOnIMG";

		private const string VISAAPPROVEDONIMG_PARM = "@VisaApprovedOnIMG";

		private const string VISADELIVEREDONIMG_PARM = "@VisaDeliveredOnIMG";

		private const string ARRIVEDON_PARM = "@ArrivedOn";

		private const string EMPLOYEENO_PARM = "@EmployeeNo";

		private const string MEDICALTYPINGON_PARM = "@MedicalTypingOn";

		private const string MEDICALATTENDEDON_PARM = "@MedicalAttendedOn";

		private const string MEDICALCOLLECTEDON_PARM = "@MedicalCollectedOn";

		private const string APPLICATIONTYPEDONEID_PARM = "@ApplicationTypedOnEID";

		private const string ATTENDEDFOREID_PARM = "@AttendedForEID";

		private const string COLLECTEDONEID_PARM = "@CollectedOnEID";

		private const string DESIGNATIONID_PARM = "@DesignationID";

		private const string VISASTATUS_PARM = "@VisaStatus";

		private const string AGTTYPE_PARM = "@AGTType";

		private const string MBNUMBERAGT_PARM = "@MBNumberAGT";

		private const string AOTYPINGDATE_PARM = "@AOTypingDate";

		private const string AOREGNUMBER_PARM = "@AORegNumber";

		private const string APPROVALSTATUSMOL_PARM = "@ApprovalStatusMOL";

		private const string MOLREMARKS_PARM = "@MOLRemarks";

		private const string EXPECTEDARRIVALDATE_PARM = "@ExpectedArrivaldate";

		private const string IMGREMARKS_PARM = "@IMGRemarks";

		private const string APPROVALSTATUSIMG_PARM = "@ApprovalStatusIMG";

		private const string SIGNEDAORECEIVEDDATE_PARM = "@SignedAOrecvdDate";

		private const string SIGNEDAGTRECEIVEDDATE_PARM = "@SignedAGTrecvdDate";

		private const string APPLICATIONPOSTEDONRP_PARM = "@ApplicationPostedOnRP";

		private const string APPLICATIONAPPROVEDONRP_PARM = "@ApplicationApprovedOnRP";

		private const string SUBMITTEDTOZAJIL_PARM = "@SubmittedToZajil";

		private const string PASSPORTCOLLECTEDON_PARM = "@PassportCollectedOn";

		private const string VISACOPYTOAGENTON_PARM = "@VisaCopyToAgentOn";

		private const string APPROVALVALIDTILLMOL_PARM = "@ApprovalValidTillMOL";

		private const string TEMPWPNO_PARM = "@TempWPNumber";

		private const string APPROVALFEEPAIDONMOL_PARM = "@ApprovalFeePaidOnMOL";

		private const string BGTYPEMOL_PARM = "@BGTypeMOL";

		private const string VISAISSUEPLACEIMG_PARM = "@VisaIssuePlaceIMG";

		private const string VISANUMBER_PARM = "@VisaNumber";

		private const string VISAISSUEDATEIMG_PARM = "@VisaIssueDateIMG";

		private const string VISAEXPIRYDATEIMG_PARM = "@VisaExpiryDateIMG";

		private const string UIDNUMBERIMG_PARM = "@UIDNumberIMG";

		private const string ISCANCELLED_PARM = "@IsCancelled";

		private const string STAGE_PARM = "@CancellationStage";

		private const string VCAPPRECEIVEDDATE_PARM = "@VCAppReceivedDate";

		private const string APPCANCELLATIONDATE_PARM = "@AppCancellationDate";

		private const string IMGCANCELLATIONDATE_PARM = "@IMGCancellationDate";

		private const string MOLCANCELLATIONDATE_PARM = "@MOLCancellationDate";

		private const string MBNUMBERCANCEL_PARM = "@MBNumberCancel";

		private const string REASON_PARM = "@CancellationReason";

		private const string CANCELLATIONREMARKS_PARM = "@CancellationRemarks";

		private const string ARRIVALPORT_PARM = "@ArrivalPort";

		private const string CATEGORY_PARM = "@Category";

		private const string RPISSUEDATE_PARM = "@RPIssueDate";

		private const string RPEXPIRYDATE_PARM = "@RPExpiryDate";

		private const string RPISSUEPLACE_PARM = "@RPIssuePlace";

		private const string PROCESSTYPERP_PARM = "@RPProcessType";

		private const string AGTTYPEDON_PARM = "@AGTTypedOn";

		private const string AGTSUBMITTEDON_PARM = "@AGTSubmittedOn";

		private const string WPNO_PARM = "@WPNumber";

		private const string PERSONALIDNO_PARM = "@PersonalIDNo";

		private const string WPISSUEDATE_PARM = "@WPIssueDate";

		private const string WPEXPIRYDATE_PARM = "@WPExpiryDate";

		private const string WPISSUEPLACE_PARM = "@WPIssuePlace";

		private const string MEDICALRESULT_PARM = "@MedicalResult";

		private const string MEDICALNOTE_PARM = "@MedicalNote";

		private const string ABSREGDATEMOL_PARM = "@ABSRegDateMOL";

		private const string ABSAPPRDATEMOL_PARM = "@ABSApprDateMOL";

		private const string ABSAPPRDATEIMG_PARM = "@ABSApprDateIMG";

		private const string ABSREFNO_PARM = "@ABSRefNo";

		private const string APPRECEIVEDON_PARM = "@ApprReceivedOn";

		private const string SUBMITTEDTOMOL_PARM = "@SubmittedToMOL";

		private const string DEPARTUREDATE_PARM = "@DepartureDate";

		private const string APPTYPEONMOL_PARM = "@AppTypedOnMOL";

		private const string RPCANCELLEDONIMG_PARM = "@RPCancelledOnIMG";

		private const string EXITPORT_PARM = "@ExitPort";

		private const string NATIONALID_PARM = "@NationalID";

		private const string NATIONALIDVALIDITY_PARM = "@NationalIDValidity";

		private const string RELIGIONID_PARM = "@ReligionID";

		private const string BIRTHDATE_PARM = "@BirthDate";

		private const string BIRTHPLACE_PARM = "@BirthPlace";

		private const string NATIONALITYID_PARM = "@NationalityID";

		private const string BLOODGROUP_PARM = "@BloodGroup";

		private const string VISADESIGNATION_PARM = "@VisaDesignation";

		private const string ACTUALDESIGNATION_PARM = "@ActualDesignation";

		private const string NOTES_PARM = "@Notes";

		private const string REMARKS_PARM = "@Remarks";

		private const string GENDER_PARM = "@Gender";

		private const string MARITALSTATUS_PARM = "@MaritalStatus";

		private const string PAYTYPE_PARM = "@PayType";

		private const string PAYROLLITEMID_PARM = "@PayrollItemID";

		private const string AMOUNT_PARM = "@Amount";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string QUALIFICATION_PARM = "@QualificationID";

		private const string LANGUAGE_PARM = "@LanguageID";

		private const string EXPERIENCELOCAL_PARM = "@ExperienceLocal";

		private const string EXPERIENCEABROAD_PARM = "@ExperienceAbroad";

		private const string STATUS_PARM = "@Status";

		private const string PRIMARYADDRESSID_PARM = "@PrimaryAddressID";

		private const string BASICSALARY_PARM = "@BasicSalary";

		private const string CURRENCY_PARM = "@CurrencyID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string SALARYREMARKS_PARM = "@SalaryRemarks";

		private const string SPONSORID_PARM = "@SponsorID";

		private const string HEALTHCARDNO_PARM = "@HealthCardNo";

		private const string GROUPID_PARM = "@GroupID";

		private const string USERDEFINED1_PARM = "@UserDefined1";

		private const string USERDEFINED2_PARM = "@UserDefined2";

		private const string USERDEFINED3_PARM = "@UserDefined3";

		private const string USERDEFINED4_PARM = "@UserDefined4";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string EMPLOYEEBENEFITDETAIL_TABLE = "Candidate_Benefit_Detail";

		private const string CANDIDATE_PARM = "@CandidateID";

		private const string BENEFITID_PARM = "@BenefitID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string LASTAMOUNT_PARM = "@LastAmount";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string AGREEMENTSTATUS_PARM = "@AgreementStatus";

		private const string SPECIALCONDITION_PARM = "@SpecialCondition";

		public bool CheckConcurrency = true;

		public Candidates(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Candidate", new FieldValue("CandidateID", "@CandidateID", isUpdateConditionField: true), new FieldValue("PassportNo", "@PassportNo"), new FieldValue("ApplType", "@ApplType"), new FieldValue("GivenName", "@GivenName"), new FieldValue("SurName", "@SurName"), new FieldValue("NationalityID", "@NationalityID"), new FieldValue("Gender", "@Gender"), new FieldValue("GroupID", "@GroupID"), new FieldValue("BirthDate", "@BirthDate"), new FieldValue("BirthPlace", "@BirthPlace"), new FieldValue("PassportPlaceOfIssue", "@PassportPlaceOfIssue"), new FieldValue("PassportIssueDate", "@PassportIssueDate"), new FieldValue("PassportExpiryDate", "@PassportExpiryDate"), new FieldValue("FatherName", "@FatherName"), new FieldValue("MotherName", "@MotherName"), new FieldValue("SpouseName", "@SpouseName"), new FieldValue("PassportAddress", "@PassportAddress"), new FieldValue("DesignationID", "@DesignationID"), new FieldValue("VisaStatus", "@VisaStatus"), new FieldValue("Notes", "@Notes"), new FieldValue("ECRStatus", "@ECRStatus"), new FieldValue("SystemDate", "@SystemDate"), new FieldValue("SelectionStatus", "@SelectionStatus"), new FieldValue("SelectedOn", "@SelectedOn"), new FieldValue("SelectedAt", "@SelectedAt"), new FieldValue("ThroughAgent", "@ThroughAgent"), new FieldValue("VisaCopyToAgentOn", "@VisaCopyToAgentOn"), new FieldValue("VisaDesignation", "@VisaDesignation"), new FieldValue("ActualDesignation", "@ActualDesignation"), new FieldValue("Remarks", "@Remarks"), new FieldValue("QualificationID", "@QualificationID"), new FieldValue("LanguageID", "@LanguageID"), new FieldValue("ExperienceLocal", "@ExperienceLocal"), new FieldValue("ExperienceAbroad", "@ExperienceAbroad"), new FieldValue("AOTypingDate", "@AOTypingDate"), new FieldValue("AORegNumber", "@AORegNumber"), new FieldValue("ApplicationTypingDateMOL", "@ApplicationTypingDateMOL"), new FieldValue("ApprovalStatusMOL", "@ApprovalStatusMOL"), new FieldValue("MOLRemarks", "@MOLRemarks"), new FieldValue("MBNumberMOL", "@MBNumberMOL"), new FieldValue("SponsorID", "@SponsorID"), new FieldValue("ApprovalDateMOL", "@ApprovalDateMOL"), new FieldValue("ApprovalValidTillMOL", "@ApprovalValidTillMOL"), new FieldValue("TempWPNumber", "@TempWPNumber"), new FieldValue("ApprovalFeePaidOnMOL", "@ApprovalFeePaidOnMOL"), new FieldValue("BGPaidOnMOL", "@BGPaidOnMOL"), new FieldValue("BGTypeMOL", "@BGTypeMOL"), new FieldValue("VisaAppliedThroughIMG", "@VisaAppliedThroughIMG"), new FieldValue("VisaPostedOnIMG", "@VisaPostedOnIMG"), new FieldValue("VisaApprovedOnIMG", "@VisaApprovedOnIMG"), new FieldValue("VisaIssuePlaceIMG", "@VisaIssuePlaceIMG"), new FieldValue("VisaNumber", "@VisaNumber"), new FieldValue("ApprovalStatusIMG", "@ApprovalStatusIMG"), new FieldValue("IMGRemarks", "@IMGRemarks"), new FieldValue("ExpectedArrivaldate", "@ExpectedArrivaldate"), new FieldValue("VisaIssueDateIMG", "@VisaIssueDateIMG"), new FieldValue("VisaExpiryDateIMG", "@VisaExpiryDateIMG"), new FieldValue("UIDNumberIMG", "@UIDNumberIMG"), new FieldValue("ArrivedOn", "@ArrivedOn"), new FieldValue("ArrivalPort", "@ArrivalPort"), new FieldValue("Category", "@Category"), new FieldValue("EmployeeNo", "@EmployeeNo"), new FieldValue("HealthCardNo", "@HealthCardNo"), new FieldValue("MedicalTypingOn", "@MedicalTypingOn"), new FieldValue("MedicalAttendedOn", "@MedicalAttendedOn"), new FieldValue("MedicalCollectedOn", "@MedicalCollectedOn"), new FieldValue("MedicalResult", "@MedicalResult"), new FieldValue("MedicalNote", "@MedicalNote"), new FieldValue("ApplicationTypedOnEID", "@ApplicationTypedOnEID"), new FieldValue("AttendedForEID", "@AttendedForEID"), new FieldValue("CollectedOnEID", "@CollectedOnEID"), new FieldValue("NationalID", "@NationalID"), new FieldValue("NationalIDValidity", "@NationalIDValidity"), new FieldValue("AGTType", "@AGTType"), new FieldValue("MBNumberAGT", "@MBNumberAGT"), new FieldValue("AGTTypedOn", "@AGTTypedOn"), new FieldValue("AGTSubmittedOn", "@AGTSubmittedOn"), new FieldValue("WPNumber", "@WPNumber"), new FieldValue("PersonalIDNo", "@PersonalIDNo"), new FieldValue("WPIssuePlace", "@WPIssuePlace"), new FieldValue("WPIssueDate", "@WPIssueDate"), new FieldValue("WPExpiryDate", "@WPExpiryDate"), new FieldValue("RPProcessType", "@RPProcessType"), new FieldValue("ApplicationPostedOnRP", "@ApplicationPostedOnRP"), new FieldValue("ApplicationApprovedOnRP", "@ApplicationApprovedOnRP"), new FieldValue("SubmittedToZajil", "@SubmittedToZajil"), new FieldValue("PassportCollectedOn", "@PassportCollectedOn"), new FieldValue("RPIssuePlace", "@RPIssuePlace"), new FieldValue("RPIssueDate", "@RPIssueDate"), new FieldValue("RPExpiryDate", "@RPExpiryDate"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("AgreementStatus", "@AgreementStatus"), new FieldValue("SignedAOrecvdDate", "@SignedAOrecvdDate"), new FieldValue("SignedAGTrecvdDate", "@SignedAGTrecvdDate"), new FieldValue("SpecialCondition", "@SpecialCondition"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Candidate", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
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
			parameters.Add("@CandidateID", SqlDbType.NVarChar);
			parameters.Add("@PassportNo", SqlDbType.NVarChar);
			parameters.Add("@ApplType", SqlDbType.TinyInt);
			parameters.Add("@GivenName", SqlDbType.NVarChar);
			parameters.Add("@SurName", SqlDbType.NVarChar);
			parameters.Add("@NationalityID", SqlDbType.NVarChar);
			parameters.Add("@Gender", SqlDbType.NVarChar);
			parameters.Add("@BirthDate", SqlDbType.SmallDateTime);
			parameters.Add("@BirthPlace", SqlDbType.NVarChar);
			parameters.Add("@PassportPlaceOfIssue", SqlDbType.NVarChar);
			parameters.Add("@PassportIssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@PassportExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@FatherName", SqlDbType.NVarChar);
			parameters.Add("@MotherName", SqlDbType.NVarChar);
			parameters.Add("@SpouseName", SqlDbType.NVarChar);
			parameters.Add("@PassportAddress", SqlDbType.NVarChar);
			parameters.Add("@DesignationID", SqlDbType.NVarChar);
			parameters.Add("@VisaStatus", SqlDbType.NVarChar);
			parameters.Add("@Notes", SqlDbType.NVarChar);
			parameters.Add("@ECRStatus", SqlDbType.TinyInt);
			parameters.Add("@SystemDate", SqlDbType.DateTime);
			parameters.Add("@SelectionStatus", SqlDbType.TinyInt);
			parameters.Add("@SelectedOn", SqlDbType.SmallDateTime);
			parameters.Add("@SelectedAt", SqlDbType.NVarChar);
			parameters.Add("@ThroughAgent", SqlDbType.NVarChar);
			parameters.Add("@VisaCopyToAgentOn", SqlDbType.SmallDateTime);
			parameters.Add("@VisaDesignation", SqlDbType.NVarChar);
			parameters.Add("@ActualDesignation", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@QualificationID", SqlDbType.NVarChar);
			parameters.Add("@LanguageID", SqlDbType.NVarChar);
			parameters.Add("@ExperienceLocal", SqlDbType.Decimal);
			parameters.Add("@ExperienceAbroad", SqlDbType.Decimal);
			parameters.Add("@AOTypingDate", SqlDbType.DateTime);
			parameters.Add("@AORegNumber", SqlDbType.NVarChar);
			parameters.Add("@ApprovalStatusMOL", SqlDbType.TinyInt);
			parameters.Add("@MOLRemarks", SqlDbType.NVarChar);
			parameters.Add("@ApplicationTypingDateMOL", SqlDbType.SmallDateTime);
			parameters.Add("@MBNumberMOL", SqlDbType.NVarChar);
			parameters.Add("@SponsorID", SqlDbType.NVarChar);
			parameters.Add("@ApprovalDateMOL", SqlDbType.SmallDateTime);
			parameters.Add("@ApprovalValidTillMOL", SqlDbType.SmallDateTime);
			parameters.Add("@TempWPNumber", SqlDbType.NVarChar);
			parameters.Add("@ApprovalFeePaidOnMOL", SqlDbType.SmallDateTime);
			parameters.Add("@BGPaidOnMOL", SqlDbType.SmallDateTime);
			parameters.Add("@BGTypeMOL", SqlDbType.NVarChar);
			parameters.Add("@ExpectedArrivaldate", SqlDbType.DateTime);
			parameters.Add("@ApprovalStatusIMG", SqlDbType.TinyInt);
			parameters.Add("@IMGRemarks", SqlDbType.NVarChar);
			parameters.Add("@VisaAppliedThroughIMG", SqlDbType.NVarChar);
			parameters.Add("@VisaPostedOnIMG", SqlDbType.SmallDateTime);
			parameters.Add("@VisaApprovedOnIMG", SqlDbType.SmallDateTime);
			parameters.Add("@VisaIssuePlaceIMG", SqlDbType.NVarChar);
			parameters.Add("@VisaNumber", SqlDbType.NVarChar);
			parameters.Add("@VisaIssueDateIMG", SqlDbType.SmallDateTime);
			parameters.Add("@VisaExpiryDateIMG", SqlDbType.SmallDateTime);
			parameters.Add("@UIDNumberIMG", SqlDbType.NVarChar);
			parameters.Add("@ArrivedOn", SqlDbType.SmallDateTime);
			parameters.Add("@ArrivalPort", SqlDbType.NVarChar);
			parameters.Add("@Category", SqlDbType.NVarChar);
			parameters.Add("@EmployeeNo", SqlDbType.NVarChar);
			parameters.Add("@HealthCardNo", SqlDbType.NVarChar);
			parameters.Add("@MedicalTypingOn", SqlDbType.SmallDateTime);
			parameters.Add("@MedicalAttendedOn", SqlDbType.SmallDateTime);
			parameters.Add("@MedicalCollectedOn", SqlDbType.SmallDateTime);
			parameters.Add("@MedicalResult", SqlDbType.NVarChar);
			parameters.Add("@MedicalNote", SqlDbType.NVarChar);
			parameters.Add("@ApplicationTypedOnEID", SqlDbType.SmallDateTime);
			parameters.Add("@AttendedForEID", SqlDbType.SmallDateTime);
			parameters.Add("@CollectedOnEID", SqlDbType.SmallDateTime);
			parameters.Add("@NationalID", SqlDbType.NVarChar);
			parameters.Add("@NationalIDValidity", SqlDbType.SmallDateTime);
			parameters.Add("@AGTType", SqlDbType.NVarChar);
			parameters.Add("@MBNumberAGT", SqlDbType.NVarChar);
			parameters.Add("@AGTTypedOn", SqlDbType.SmallDateTime);
			parameters.Add("@AGTSubmittedOn", SqlDbType.SmallDateTime);
			parameters.Add("@WPNumber", SqlDbType.NVarChar);
			parameters.Add("@PersonalIDNo", SqlDbType.NVarChar);
			parameters.Add("@WPIssuePlace", SqlDbType.NVarChar);
			parameters.Add("@WPIssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@WPExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@RPProcessType", SqlDbType.NVarChar);
			parameters.Add("@ApplicationPostedOnRP", SqlDbType.SmallDateTime);
			parameters.Add("@ApplicationApprovedOnRP", SqlDbType.SmallDateTime);
			parameters.Add("@SubmittedToZajil", SqlDbType.SmallDateTime);
			parameters.Add("@PassportCollectedOn", SqlDbType.SmallDateTime);
			parameters.Add("@RPIssuePlace", SqlDbType.NVarChar);
			parameters.Add("@RPIssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@RPExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@SpecialCondition", SqlDbType.NVarChar);
			parameters.Add("@AgreementStatus", SqlDbType.NVarChar);
			parameters.Add("@SignedAOrecvdDate", SqlDbType.DateTime);
			parameters.Add("@SignedAGTrecvdDate", SqlDbType.DateTime);
			parameters["@CandidateID"].SourceColumn = "CandidateID";
			parameters["@PassportNo"].SourceColumn = "PassportNo";
			parameters["@ApplType"].SourceColumn = "ApplType";
			parameters["@GivenName"].SourceColumn = "GivenName";
			parameters["@SurName"].SourceColumn = "SurName";
			parameters["@NationalityID"].SourceColumn = "NationalityID";
			parameters["@Gender"].SourceColumn = "Gender";
			parameters["@BirthDate"].SourceColumn = "BirthDate";
			parameters["@BirthPlace"].SourceColumn = "BirthPlace";
			parameters["@PassportPlaceOfIssue"].SourceColumn = "PassportPlaceOfIssue";
			parameters["@PassportIssueDate"].SourceColumn = "PassportIssueDate";
			parameters["@PassportExpiryDate"].SourceColumn = "PassportExpiryDate";
			parameters["@DesignationID"].SourceColumn = "DesignationID";
			parameters["@VisaStatus"].SourceColumn = "VisaStatus";
			parameters["@FatherName"].SourceColumn = "FatherName";
			parameters["@MotherName"].SourceColumn = "MotherName";
			parameters["@SpouseName"].SourceColumn = "SpouseName";
			parameters["@PassportAddress"].SourceColumn = "PassportAddress";
			parameters["@Notes"].SourceColumn = "Notes";
			parameters["@ECRStatus"].SourceColumn = "ECRStatus";
			parameters["@SystemDate"].SourceColumn = "SystemDate";
			parameters["@SelectionStatus"].SourceColumn = "SelectionStatus";
			parameters["@SelectedOn"].SourceColumn = "SelectedOn";
			parameters["@SelectedAt"].SourceColumn = "SelectedAt";
			parameters["@ThroughAgent"].SourceColumn = "ThroughAgent";
			parameters["@VisaCopyToAgentOn"].SourceColumn = "VisaCopyToAgentOn";
			parameters["@VisaDesignation"].SourceColumn = "VisaDesignation";
			parameters["@ActualDesignation"].SourceColumn = "ActualDesignation";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@QualificationID"].SourceColumn = "QualificationID";
			parameters["@LanguageID"].SourceColumn = "LanguageID";
			parameters["@ExperienceLocal"].SourceColumn = "ExperienceLocal";
			parameters["@ExperienceAbroad"].SourceColumn = "ExperienceAbroad";
			parameters["@AOTypingDate"].SourceColumn = "AOTypingDate";
			parameters["@AORegNumber"].SourceColumn = "AORegNumber";
			parameters["@ApprovalStatusMOL"].SourceColumn = "ApprovalStatusMOL";
			parameters["@MOLRemarks"].SourceColumn = "MOLRemarks";
			parameters["@ApplicationTypingDateMOL"].SourceColumn = "ApplicationTypingDateMOL";
			parameters["@MBNumberMOL"].SourceColumn = "MBNumberMOL";
			parameters["@SponsorID"].SourceColumn = "SponsorID";
			parameters["@ApprovalDateMOL"].SourceColumn = "ApprovalDateMOL";
			parameters["@ApprovalValidTillMOL"].SourceColumn = "ApprovalValidTillMOL";
			parameters["@TempWPNumber"].SourceColumn = "TempWPNumber";
			parameters["@ApprovalFeePaidOnMOL"].SourceColumn = "ApprovalFeePaidOnMOL";
			parameters["@BGPaidOnMOL"].SourceColumn = "BGPaidOnMOL";
			parameters["@BGTypeMOL"].SourceColumn = "BGTypeMOL";
			parameters["@VisaAppliedThroughIMG"].SourceColumn = "VisaAppliedThroughIMG";
			parameters["@VisaPostedOnIMG"].SourceColumn = "VisaPostedOnIMG";
			parameters["@VisaApprovedOnIMG"].SourceColumn = "VisaApprovedOnIMG";
			parameters["@VisaIssuePlaceIMG"].SourceColumn = "VisaIssuePlaceIMG";
			parameters["@VisaNumber"].SourceColumn = "VisaNumber";
			parameters["@VisaIssueDateIMG"].SourceColumn = "VisaIssueDateIMG";
			parameters["@VisaExpiryDateIMG"].SourceColumn = "VisaExpiryDateIMG";
			parameters["@UIDNumberIMG"].SourceColumn = "UIDNumberIMG";
			parameters["@ApprovalStatusIMG"].SourceColumn = "ApprovalStatusIMG";
			parameters["@ExpectedArrivaldate"].SourceColumn = "ExpectedArrivaldate";
			parameters["@IMGRemarks"].SourceColumn = "IMGRemarks";
			parameters["@ArrivedOn"].SourceColumn = "ArrivedOn";
			parameters["@ArrivalPort"].SourceColumn = "ArrivalPort";
			parameters["@Category"].SourceColumn = "Category";
			parameters["@EmployeeNo"].SourceColumn = "EmployeeNo";
			parameters["@HealthCardNo"].SourceColumn = "HealthCardNo";
			parameters["@MedicalTypingOn"].SourceColumn = "MedicalTypingOn";
			parameters["@MedicalAttendedOn"].SourceColumn = "MedicalAttendedOn";
			parameters["@MedicalCollectedOn"].SourceColumn = "MedicalCollectedOn";
			parameters["@MedicalResult"].SourceColumn = "MedicalResult";
			parameters["@MedicalNote"].SourceColumn = "MedicalNote";
			parameters["@ApplicationTypedOnEID"].SourceColumn = "ApplicationTypedOnEID";
			parameters["@AttendedForEID"].SourceColumn = "AttendedForEID";
			parameters["@CollectedOnEID"].SourceColumn = "CollectedOnEID";
			parameters["@NationalID"].SourceColumn = "NationalID";
			parameters["@NationalIDValidity"].SourceColumn = "NationalIDValidity";
			parameters["@AGTType"].SourceColumn = "AGTType";
			parameters["@MBNumberAGT"].SourceColumn = "MBNumberAGT";
			parameters["@AGTTypedOn"].SourceColumn = "AGTTypedOn";
			parameters["@AGTSubmittedOn"].SourceColumn = "AGTSubmittedOn";
			parameters["@WPNumber"].SourceColumn = "WPNumber";
			parameters["@PersonalIDNo"].SourceColumn = "PersonalIDNo";
			parameters["@WPIssuePlace"].SourceColumn = "WPIssuePlace";
			parameters["@WPIssueDate"].SourceColumn = "WPIssueDate";
			parameters["@WPExpiryDate"].SourceColumn = "WPExpiryDate";
			parameters["@RPProcessType"].SourceColumn = "RPProcessType";
			parameters["@ApplicationPostedOnRP"].SourceColumn = "ApplicationPostedOnRP";
			parameters["@ApplicationApprovedOnRP"].SourceColumn = "ApplicationApprovedOnRP";
			parameters["@SubmittedToZajil"].SourceColumn = "SubmittedToZajil";
			parameters["@PassportCollectedOn"].SourceColumn = "PassportCollectedOn";
			parameters["@RPIssuePlace"].SourceColumn = "RPIssuePlace";
			parameters["@RPIssueDate"].SourceColumn = "RPIssueDate";
			parameters["@RPExpiryDate"].SourceColumn = "RPExpiryDate";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@AgreementStatus"].SourceColumn = "AgreementStatus";
			parameters["@SpecialCondition"].SourceColumn = "SpecialCondition";
			parameters["@SignedAOrecvdDate"].SourceColumn = "SignedAOrecvdDate";
			parameters["@SignedAGTrecvdDate"].SourceColumn = "SignedAGTrecvdDate";
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

		private string GetCancellationText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Candidate", new FieldValue("CandidateID", "@CandidateID", isUpdateConditionField: true), new FieldValue("PassportNo", "@PassportNo"), new FieldValue("GivenName", "@GivenName"), new FieldValue("SurName", "@SurName"), new FieldValue("NationalityID", "@NationalityID"), new FieldValue("Gender", "@Gender"), new FieldValue("IsCancelled", "@IsCancelled"), new FieldValue("CancellationStage", "@CancellationStage"), new FieldValue("VCAppReceivedDate", "@VCAppReceivedDate"), new FieldValue("AppCancellationDate", "@AppCancellationDate"), new FieldValue("IMGCancellationDate", "@IMGCancellationDate"), new FieldValue("MOLCancellationDate", "@MOLCancellationDate"), new FieldValue("MBNumberCancel", "@MBNumberCancel"), new FieldValue("CancellationReason", "@CancellationReason"), new FieldValue("CancellationRemarks", "@CancellationRemarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Candidate", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetCancellationCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetCancellationText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetCancellationText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CandidateID", SqlDbType.NVarChar);
			parameters.Add("@PassportNo", SqlDbType.NVarChar);
			parameters.Add("@GivenName", SqlDbType.NVarChar);
			parameters.Add("@SurName", SqlDbType.NVarChar);
			parameters.Add("@NationalityID", SqlDbType.NVarChar);
			parameters.Add("@Gender", SqlDbType.NVarChar);
			parameters.Add("@IsCancelled", SqlDbType.Bit);
			parameters.Add("@CancellationStage", SqlDbType.NVarChar);
			parameters.Add("@VCAppReceivedDate", SqlDbType.DateTime);
			parameters.Add("@AppCancellationDate", SqlDbType.DateTime);
			parameters.Add("@IMGCancellationDate", SqlDbType.DateTime);
			parameters.Add("@MOLCancellationDate", SqlDbType.DateTime);
			parameters.Add("@MBNumberCancel", SqlDbType.NVarChar);
			parameters.Add("@CancellationReason", SqlDbType.NVarChar);
			parameters.Add("@CancellationRemarks", SqlDbType.NVarChar);
			parameters["@CandidateID"].SourceColumn = "CandidateID";
			parameters["@PassportNo"].SourceColumn = "PassportNo";
			parameters["@GivenName"].SourceColumn = "GivenName";
			parameters["@SurName"].SourceColumn = "SurName";
			parameters["@NationalityID"].SourceColumn = "NationalityID";
			parameters["@Gender"].SourceColumn = "Gender";
			parameters["@IsCancelled"].SourceColumn = "IsCancelled";
			parameters["@CancellationStage"].SourceColumn = "CancellationStage";
			parameters["@VCAppReceivedDate"].SourceColumn = "VCAppReceivedDate";
			parameters["@AppCancellationDate"].SourceColumn = "AppCancellationDate";
			parameters["@IMGCancellationDate"].SourceColumn = "IMGCancellationDate";
			parameters["@MOLCancellationDate"].SourceColumn = "MOLCancellationDate";
			parameters["@MBNumberCancel"].SourceColumn = "MBNumberCancel";
			parameters["@CancellationReason"].SourceColumn = "CancellationReason";
			parameters["@CancellationRemarks"].SourceColumn = "CancellationRemarks";
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

		private string GetInsertUpdateCandidateSalaryText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Candidate_Salary", new FieldValue("CandidateID", "@CandidateID"), new FieldValue("PayrollItemID", "@PayrollItemID"), new FieldValue("PayType", "@PayType"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Amount", "@Amount"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_PayrollItem_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateCandidateSalaryBenefit(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Candidate_Benefit_Detail", new FieldValue("CandidateID", "@CandidateID"), new FieldValue("BenefitID", "@BenefitID"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Amount", "@Amount"), new FieldValue("LastAmount", "@LastAmount"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Candidate_Benefit_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		internal SqlCommand GetInsertUpdateCandidateSalaryCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateCandidateSalaryText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateCandidateSalaryText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CandidateID", SqlDbType.NVarChar);
			parameters.Add("@PayrollItemID", SqlDbType.NVarChar);
			parameters.Add("@PayType", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters["@CandidateID"].SourceColumn = "CandidateID";
			parameters["@PayrollItemID"].SourceColumn = "PayrollItemID";
			parameters["@PayType"].SourceColumn = "PayType";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		internal SqlCommand GetInsertUpdateCommandBenefit(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateCandidateSalaryBenefit(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateCandidateSalaryBenefit(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CandidateID", SqlDbType.NVarChar);
			parameters.Add("@BenefitID", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.SmallDateTime);
			parameters.Add("@EndDate", SqlDbType.SmallDateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@LastAmount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters["@CandidateID"].SourceColumn = "CandidateID";
			parameters["@BenefitID"].SourceColumn = "BenefitID";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@LastAmount"].SourceColumn = "LastAmount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		public bool InsertUpdateCandidate(CandidateData accountCandidateData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(accountCandidateData, "Candidate", insertUpdateCommand) : Update(accountCandidateData, "Candidate", insertUpdateCommand));
				string text = accountCandidateData.CandidateTable.Rows[0]["CandidateID"].ToString();
				if (accountCandidateData.EmployeeTable.Rows.Count > 0)
				{
					bool num = bool.Parse(accountCandidateData.CandidateTable.Rows[0]["IsExist"].ToString());
					string text2 = accountCandidateData.CandidateTable.Rows[0]["EmployeeNo"].ToString();
					DateTime now = DateTime.Now;
					string text3 = accountCandidateData.EmployeeTable.Rows[0]["CreatedBy"].ToString();
					insertUpdateCommand = new Employees(base.DBConfig).GetInsertUpdateCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountCandidateData, "Employee", insertUpdateCommand);
					SqlCommand sqlCommand = new SqlCommand();
					if (!num)
					{
						sqlCommand = new SqlCommand("Update EMPLOYEE SET DateCreated= '" + now + "', CreatedBy= '" + text3 + "' WHERE EmployeeID='" + text2 + "'");
						sqlCommand.Transaction = sqlTransaction;
						flag &= (ExecuteNonQuery(sqlCommand) > 0);
					}
					if (accountCandidateData.EmployeeAddressTable.Rows.Count > 0)
					{
						insertUpdateCommand = new EmployeeAddresses(base.DBConfig).GetInsertUpdateCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Insert(accountCandidateData, "Employee_Address", insertUpdateCommand);
					}
					insertUpdateCommand = new EmployeePayrollItemDetails(base.DBConfig).GetInsertUpdateCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= new EmployeePayrollItemDetails(base.DBConfig).DeleteEmployeePayrollItems(sqlTransaction, text2);
					if (accountCandidateData.EmployeePayrollItemDetail.Rows.Count > 0)
					{
						flag &= Insert(accountCandidateData, "Employee_PayrollItem_Detail", insertUpdateCommand);
					}
					insertUpdateCommand = new EmployeeBenefitDetails(base.DBConfig).GetInsertUpdateCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= new EmployeeBenefitDetails(base.DBConfig).DeleteEmployeeBenefits(sqlTransaction, text2);
					if (accountCandidateData.EmployeeBenefit.Rows.Count > 0)
					{
						flag &= Insert(accountCandidateData, "Employee_Benefit_Detail", insertUpdateCommand);
					}
				}
				if (accountCandidateData.Tables.Contains("Candidate_Salary"))
				{
					insertUpdateCommand = new Candidates(base.DBConfig).GetInsertUpdateCandidateSalaryCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= new Candidates(base.DBConfig).DeleteCandidatePayrollItems(sqlTransaction, text.ToString());
					if (accountCandidateData.CandidateSalaryTable.Rows.Count > 0)
					{
						flag &= Insert(accountCandidateData, "Candidate_Salary", insertUpdateCommand);
					}
				}
				if (accountCandidateData.Tables.Contains("Candidate_Benefit_Detail"))
				{
					insertUpdateCommand = new Candidates(base.DBConfig).GetInsertUpdateCommandBenefit(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= new Candidates(base.DBConfig).DeleteEmployeeBenefits(sqlTransaction, text.ToString());
					if (accountCandidateData.CandidateBenefitDetailTable.Rows.Count > 0)
					{
						flag &= Insert(accountCandidateData, "Candidate_Benefit_Detail", insertUpdateCommand);
					}
				}
				if (!flag)
				{
					return flag;
				}
				if (isUpdate)
				{
					AddActivityLog("Candidate", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("Candidate", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Candidate", "CandidateID", text, sqlTransaction, !isUpdate);
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

		internal bool DeleteCandidatePayrollItems(SqlTransaction sqlTransaction, string candidateID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Candidate_Salary WHERE CandidateID = '" + candidateID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Candidate Payroll Item", candidateID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteEmployeeBenefits(SqlTransaction sqlTransaction, string candidateID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Candidate_Benefit_Detail WHERE CandidateID = '" + candidateID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Candidate Benefit", candidateID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool CancelCandidate(CandidateData accountCandidateData)
		{
			bool flag = true;
			SqlCommand cancellationCommand = GetCancellationCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (cancellationCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCandidateData, "Candidate", cancellationCommand);
				string text = accountCandidateData.CandidateTable.Rows[0]["CandidateID"].ToString();
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Candidate Cancellation", text, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Candidate", "CandidateID", text, sqlTransaction, isInsert: false);
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

		public DataSet GetCandidateSalaryDetailsByCandidateID(string candidateID)
		{
			string textCommand = "SELECT   CandidateID, BankID, AccountNumber, BasicSalary, PaymentMethodID, AccountID, CurrencyID, \r\n                                SalaryRemarks,DestinationID,TicketRemarks,TicketPeriod,NumberOfTickets,TicketAmount,EOSRuleID,OvertimeID\r\n                            FROM Candidate WHERE CandidateID='" + candidateID + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Candidate", textCommand);
			return dataSet;
		}

		public CandidateData GetCandidate()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Candidate");
			CandidateData candidateData = new CandidateData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(candidateData, "Candidate", sqlBuilder);
			return candidateData;
		}

		public bool DeleteCandidate(string candidateID)
		{
			bool result = true;
			try
			{
				return result;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public CandidateData GetCandidateByID(string id)
		{
			CandidateData candidateData = new CandidateData();
			string empty = string.Empty;
			empty = "SELECT *,ISNULL(VisaStatus,0) as VisaSelectStatus , (SELECT COUNT(*) FROM Employee WHERE Employee.EmployeeID = Candidate.EmployeeNo) AS IsExist FROM Candidate \r\n                            WHERE CandidateID='" + id + "'";
			FillDataSet(candidateData, "Candidate", empty);
			empty = "SELECT * FROM UDF_Candidate WHERE EntityID = '" + id + "'";
			FillDataSet(candidateData, "UDF", empty);
			empty = "SELECT CandidateID,EAD.PayrollItemID,PayType,PayrollItemName,Amount  FROM Candidate_Salary EAD INNER JOIN PayrollItem ON PayrollItem.PayrollItemID = EAD.PayrollItemID WHERE CandidateID = '" + id + "'";
			FillDataSet(candidateData, "Candidate_Salary", empty);
			empty = "SELECT CandidateID,EAD.BenefitID,BenefitName,Amount,StartDate,EndDate,Remarks FROM Candidate_Benefit_Detail EAD INNER JOIN Benefit ON Benefit.BenefitID = EAD.BenefitID WHERE CandidateID = '" + id + "'";
			FillDataSet(candidateData, "Candidate_Benefit_Detail", empty);
			return candidateData;
		}

		public DataSet GetCandidateByFields(params string[] columns)
		{
			return GetCandidateByFields(null, isInactive: true, columns);
		}

		public DataSet GetCandidateByFields(string[] candidateID, params string[] columns)
		{
			return GetCandidateByFields(candidateID, isInactive: true, columns);
		}

		public DataSet GetCandidateByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Candidate");
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
				commandHelper.FieldName = "CandidateID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Candidate";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Candidate", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCandidateList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CandidateID as [VS Code], GivenName + ' ' + SurName as [Full Name], PassportNo as [Passport No], VisaNumber as [Visa No], ArrivedOn as [Arrival Date], EmployeeNo as [Emp No], \r\n                            CASE WHEN Photo IS NULL THEN 'No' ELSE 'Yes' END AS Photo, '' [Absconding Date], '' [Cancellation Date], '' [Exit Date]\r\n                            FROM Candidate";
			FillDataSet(dataSet, "Candidate", textCommand);
			return dataSet;
		}

		public DataSet GetAppointmentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CandidateID as [VS Code], GivenName + ' ' + SurName as [Full Name], PassportNo as [Passport No], VisaNumber as [Visa No], ArrivedOn as [Arrival Date], EmployeeNo as [Emp No], \r\n                            CASE WHEN Photo IS NULL THEN 'No' ELSE 'Yes' END AS Photo    FROM Candidate";
			FillDataSet(dataSet, "Candidate", textCommand);
			return dataSet;
		}

		public DataSet GetCandidateComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CandidateID [Code],ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],\r\n                            ISNULL(IsTerminated,'False') AS IsTerminated\r\n                           FROM Candidate ORDER BY CandidateID,FirstName";
			FillDataSet(dataSet, "Candidate", textCommand);
			return dataSet;
		}

		public DataSet GetAgentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GenericListID [Code], GenericListName [Name]\r\n                           FROM Generic_List WHERE GenericListType = " + 5.ToString() + " ORDER BY GenericListID, GenericListName";
			FillDataSet(dataSet, "Generic_List", textCommand);
			return dataSet;
		}

		public string GetCandidateAccountID(string candidateID)
		{
			string exp = "SELECT AccountID FROM Candidate WHERE CandidateID='" + candidateID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return "";
		}

		public DataSet GetCandidateBriefInfo(string candidateID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Candidate.LocationID,Candidate.DivisionID,Candidate.DepartmentID,LocationName,DivisionName,DepartmentName,\r\n                                IsTerminated,PositionName,Candidate.PositionID,Candidate.GradeID,GradeName,\r\n                                FirstName + ' ' + LastName AS [CandidateName] FROM Candidate\r\n                                LEFT OUTER JOIN Location ON Location.LocationID=Candidate.LocationID\r\n                                LEFT OUTER JOIN Division ON Division.DivisionID=Candidate.DivisionID\r\n                                LEFT OUTER JOIN Department ON Department.DepartmentID=Candidate.DepartmentID\r\n                                LEFT OUTER JOIN Position ON Position.PositionID=Candidate.PositionID\r\n                                LEFT OUTER JOIN Candidate_Grade EG ON EG.GradeID=Candidate.GradeID\r\n                               WHERE CandidateID='" + candidateID + "'";
			FillDataSet(dataSet, "Candidate", textCommand);
			return dataSet;
		}

		public DataSet GetCandidateBalanceSummary(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT EJ.CandidateID ,FirstName + ' ' + LastName AS [CandidateName],SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) AS Balance FROM Candidate_Journal EJ\r\n                        INNER JOIN Candidate ON EJ.CandidateID=Candidate.CandidateID WHERE ISNULL(IsVoid,'False')='False' ";
			if (fromCandidate != "")
			{
				text = text + " AND EJ.CandidateID BETWEEN '" + fromCandidate + "' AND '" + toCandidate + "' ";
			}
			if (fromDepartment != "")
			{
				text = text + " AND EJ.CandidateID IN (SELECT CandidateID FROM Candidate WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				text = text + " AND EJ.CandidateID IN (SELECT CandidateID FROM Candidate WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				text = text + " AND EJ.CandidateID IN (SELECT CandidateID FROM Candidate WHERE LocationID >= '" + fromLocation + "') ";
			}
			if (toLocation != "")
			{
				text = text + " AND EJ.CandidateID IN (SELECT CandidateID FROM Candidate WHERE LocationID <= '" + toLocation + "') ";
			}
			if (!showZeroBalance)
			{
				text += " AND (SELECT SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) FROM Candidate_Journal EJ2 WHERE EJ.CandidateID=EJ2.CandidateID)<>0 ";
			}
			text += " GROUP BY EJ.CandidateID, FirstName, LastName ";
			text += " ORDER BY EJ.CandidateID, CandidateName";
			FillDataSet(dataSet, "Candidate", text);
			return dataSet;
		}

		public DataSet GetCandidateBalanceDetailReport(DateTime from, DateTime to, string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT DISTINCT EJ.CandidateID ,FirstName + ' ' + LastName AS [CandidateName] ,\r\n                        ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM Candidate_Journal ARJ2 \r\n                         WHERE EJ.CandidateID=ARJ2.CandidateID AND ARJ2.JournalDate<'" + text + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [OpeningBalance],\r\n                        ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM Candidate_Journal ARJ2 \r\n                         WHERE EJ.CandidateID=ARJ2.CandidateID AND ARJ2.JournalDate<='" + text2 + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [EndingBalance]\r\n                        FROM Candidate_Journal EJ INNER JOIN Candidate ON EJ.CandidateID=Candidate.CandidateID WHERE 1=1";
			if (!showZeroBalance)
			{
				text3 = text3 + " AND JournalDate < '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			if (fromCandidate != "")
			{
				text3 = text3 + " AND EJ.CandidateID BETWEEN '" + fromCandidate + "' AND '" + toCandidate + "' ";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND DepartmentID BETWEEN '" + fromDepartment + "' AND '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "') ";
			}
			text3 += " ORDER BY EJ.CandidateID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Candidate", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "SELECT CandidateID ,SysDocType,'' AS [DocType], Candidate_Journal.SysDocID  ,VoucherID ,JournalDate,\r\n                            ChequeNumber ,ChequeDate ,Description,Reference,CurrencyID,CurrencyRate,DebitFC,CreditFC, Debit,Credit FROM Candidate_Journal INNER JOIN System_Document SD ON Candidate_Journal.SysDocID=SD.SysDocID WHERE \r\n                            JournalDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromCandidate != "")
			{
				text3 = text3 + " AND CandidateID BETWEEN '" + fromCandidate + "' AND '" + toCandidate + "' ";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND CandidateID IN (SELECT CandidateID FROM Candidate WHERE DepartmentID BETWEEN '" + fromDepartment + "' AND '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND CandidateID IN (SELECT CandidateID FROM Candidate WHERE LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "') ";
			}
			text3 += " ORDER BY JournalDate";
			dataSet.Merge(dataSet2);
			return dataSet;
		}

		public DataSet GetCandidateListReport(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT CandidateID,FirstName,LastName,LocationID,DepartmentID,PositionName,CASE Status \r\n                                WHEN 1 THEN 'A' ELSE 'T' END AS [Status] \r\n                                FROM Candidate LEFT OUTER JOIN  Position EP ON Candidate.PositionID=EP.PositionID\r\n                                WHERE 1=1 ";
			if (fromCandidate != "")
			{
				text = text + " AND CandidateID>='" + fromCandidate + "'";
			}
			if (toCandidate != "")
			{
				text = text + " AND CandidateID<='" + toCandidate + "'";
			}
			if (fromDepartment != "")
			{
				text = text + " AND DepartmentID>='" + fromDepartment + "'";
			}
			if (toDepartment != "")
			{
				text = text + " AND DepartmentID<='" + toDepartment + "'";
			}
			if (fromLocation != "")
			{
				text = text + " AND LocationID>='" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				text = text + " AND LocationID<='" + toLocation + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Status,1) = 1";
			}
			FillDataSet(dataSet, "Candidates", text);
			return dataSet;
		}

		public DataSet GetCandidateLeaveReport(DateTime from, DateTime to, string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromLeave, string toLeave, LeaveApprovalType approvalType)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string text3 = "SELECT E.CandidateID, E.FirstName + ' ' + E.LastName AS Name, ELR.LeaveTypeID, EA.Reason, DATEDIFF (Day, ELR.StartDate, ELR.EndDate) + 1 as NumberOfDays,\r\n\t\t                    ELR.StartDate, ELR.EndDate, ELR.LastWorkingDate, (CASE ELR.IsApproved WHEN 1 THEN 'Approved' ELSE 'Not Approved' END) AS Status FROM Candidate E\r\n                            INNER JOIN Candidate_Activity EA ON E.CandidateID = EA.CandidateID\r\n                            INNER JOIN Candidate_Leave_Request ELR ON EA.ActivityID = ELR.ActivityID\r\n                            WHERE (StartDate >= '" + text + "' AND EndDate <= '" + text2 + "')";
			if (fromCandidate != "")
			{
				text3 = text3 + " AND E.CandidateID>='" + fromCandidate + "'";
			}
			if (toCandidate != "")
			{
				text3 = text3 + " AND E.CandidateID<='" + toCandidate + "'";
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
			FillDataSet(dataSet, "Candidates", text3);
			return dataSet;
		}

		public DataSet GetCandidateProfileReport(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string str = " SELECT   DISTINCT  ROW_NUMBER() OVER (ORDER BY Candidate.CandidateID) AS [Row Number], CandidateID, PassportNo, GivenName, SurName, FatherName, MotherName, SpouseName, \r\n                            NationalityID, Gender, CONVERT(VARCHAR(11), BirthDate, 106) AS [Birth Date], BirthPlace, PassportPlaceOfIssue, CONVERT(VARCHAR(11), PassportIssueDate, 106) \r\n                            AS PassportIssueDate, CONVERT(VARCHAR(11), PassportExpiryDate, 106) AS PassportExpiryDate, PassportAddress, \r\n                            CASE WHEN SelectionStatus = 1 THEN 'In Progress' WHEN SelectionStatus = 2 THEN 'On Hold' WHEN SelectionStatus = 3 THEN 'Approved' WHEN SelectionStatus = 4 THEN 'Selected'\r\n                            WHEN SelectionStatus = 5 THEN 'Rejected' END AS SelectionStatus, CONVERT(VARCHAR(11), SelectedOn, 106) AS SelectedOn, SelectedAt, ThroughAgent,\r\n                            (SELECT     PositionName\r\n                                FROM          dbo.Position\r\n                                WHERE      (PositionID = dbo.Candidate.VisaDesignation)) AS [Visa Designation],\r\n                            (SELECT     PositionName\r\n                                FROM          dbo.Position AS Position_1\r\n                                WHERE      (PositionID = dbo.Candidate.ActualDesignation)) AS [Actual Designation], QualificationID, LanguageID, ExperienceLocal, ExperienceAbroad, \r\n                            CONVERT(VARCHAR(11), ApplicationTypingDateMOL, 106) AS ApplicationTypingDateMOL, MBNumberMOL, SponsorID, CONVERT(VARCHAR(11), VisaCopyToAgentOn, 106) \r\n                            AS VisaCopyToAgentOn, CONVERT(VARCHAR(11), ApprovalDateMOL, 106) AS ApprovalDateMOL, CONVERT(VARCHAR(11), ApprovalValidTillMOL, 106) AS ApprovalValidTillMOL, \r\n                            TempWPNumber, CONVERT(VARCHAR(11), ApprovalFeePaidOnMOL, 106) AS ApprovalFeePaidOnMOL, CONVERT(VARCHAR(11), BGPaidOnMOL, 106) AS BGPaidOnMOL, \r\n                            BGTypeMOL, VisaAppliedThroughIMG, CONVERT(VARCHAR(11), VisaPostedOnIMG, 106) AS VisaPostedOnIMG, CONVERT(VARCHAR(11), VisaApprovedOnIMG, 106) \r\n                            AS VisaApprovedOnIMG, VisaIssuePlaceIMG, VisaNumber, CONVERT(VARCHAR(11), VisaIssueDateIMG, 106) AS VisaIssueDateIMG, CONVERT(VARCHAR(11), \r\n                            VisaExpiryDateIMG, 106) AS VisaExpiryDateIMG, UIDNumberIMG, CONVERT(VARCHAR(11), ArrivedOn, 106) AS ArrivedOn, ArrivalPort, Category, EmployeeNo, HealthCardNo, \r\n                            CONVERT(VARCHAR(11), MedicalTypingOn, 106) AS MedicalTypingOn, CONVERT(VARCHAR(11), MedicalAttendedOn, 106) AS MedicalAttendedOn, CONVERT(VARCHAR(11), \r\n                            MedicalCollectedOn, 106) AS MedicalCollectedOn, MedicalResult, MedicalNote, CONVERT(VARCHAR(11), ApplicationTypedOnEID, 106) AS ApplicationTypedOnEID, \r\n                            CONVERT(VARCHAR(11), AttendedForEID, 106) AS AttendedForEID, CONVERT(VARCHAR(11), CollectedOnEID, 106) AS CollectedOnEID, NationalID, CONVERT(VARCHAR(11), \r\n                            NationalIDValidity, 106) AS NationalIDValidity, AGTType, MBNumberAGT, CONVERT(VARCHAR(11), AGTTypedOn, 106) AS AGTTypedOn, CONVERT(VARCHAR(11), \r\n                            AGTSubmittedOn, 106) AS AGTSubmittedOn, WPNumber, PersonalIDNo, WPIssuePlace, CONVERT(VARCHAR(11), WPIssueDate, 106) AS WPIssueDate, CONVERT(VARCHAR(11), \r\n                            WPExpiryDate, 106) AS WPExpiryDate, RPProcessType, CONVERT(VARCHAR(11), ApplicationPostedOnRP, 106) AS ApplicationPostedOnRP, CONVERT(VARCHAR(11), \r\n                            ApplicationApprovedOnRP, 106) AS ApplicationApprovedOnRP, CONVERT(VARCHAR(11), SubmittedToZajil, 106) AS SubmittedToZajil, CONVERT(VARCHAR(11), \r\n                            PassportCollectedOn, 106) AS PassportCollectedOn, RPIssuePlace, CONVERT(VARCHAR(11), RPIssueDate, 106) AS RPIssueDate, CONVERT(VARCHAR(11), RPExpiryDate, 106) \r\n                            AS RPExpiryDate, CASE WHEN Photo IS NULL THEN 'No' ELSE 'Yes' END AS Photo,''[Absconding Date],\r\n                            CONVERT (VARCHAR(11),Candidate.MOLCancellationDate,106) as [Cand Cn Dt],Candidate.CancellationStage,CONVERT (VARCHAR(11),Candidate.AppCancellationDate,106) as [Appl Cn Date],CONVERT (VARCHAR(11),Employee_Cancellation.VCAppSubmittedDate,106) as [Emp Cn Dt],\r\n                            CONVERT (VARCHAR(11),Employee_Cancellation.DepartureDate,106) as [Exit Date],Notes AS [General Notes] \r\n                            FROM Candidate LEFT JOIN Employee_Activity ON Candidate.EmployeeNo= Employee_Activity.EmployeeID AND ActivityType IN (15)\r\n                            LEFT JOIN Employee_Cancellation ON Employee_Cancellation.ActivityID= Employee_Activity.ActivityID  where 1=1 ";
			if (fromCandidate != "")
			{
				str = str + " AND Candidate.CandidateID>='" + fromCandidate + "'";
			}
			if (toCandidate != "")
			{
				str = str + " AND Candidate.CandidateID<='" + toCandidate + "'";
			}
			if (fromDepartment != "")
			{
				str = str + " AND Candidate.DepartmentID>='" + fromDepartment + "'";
			}
			if (toDepartment != "")
			{
				str = str + " AND Candidate.DepartmentID<='" + toDepartment + "'";
			}
			if (fromLocation != "")
			{
				str = str + " AND Candidate.LocationID>='" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				str = str + " AND Candidate.LocationID<='" + toLocation + "'";
			}
			if (!showInactive)
			{
				str += " AND ISNULL(Status,1) = 1 ";
			}
			str += "ORDER BY Candidate.CandidateID";
			FillDataSet(dataSet, "Candidates", str);
			str = "SELECT * FROM UDF_Candidate WHERE EntityID = '" + fromCandidate + "'";
			FillDataSet(dataSet, "UDF", str);
			return dataSet;
		}

		public DataSet GetCandidateActivityReport(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = "SELECT DISTINCT Candidate.CandidateID,FirstName + ' ' + LastName AS [CandidateName]\r\n                            FROM Candidate INNER JOIN Candidate_Activity EA\r\n                            ON Candidate.CandidateID=EA.CandidateID\r\n                            WHERE 1=1 ";
			if (fromCandidate != "")
			{
				text = text + " AND Candidate.CandidateID>='" + fromCandidate + "'";
			}
			if (toCandidate != "")
			{
				text = text + " AND Candidate.CandidateID<='" + toCandidate + "'";
			}
			if (fromDepartment != "")
			{
				text = text + " AND Candidate.DepartmentID>='" + fromDepartment + "'";
			}
			if (toDepartment != "")
			{
				text = text + " AND Candidate.DepartmentID<='" + toDepartment + "'";
			}
			if (fromLocation != "")
			{
				text = text + " AND Candidate.LocationID>='" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				text = text + " AND Candidate.LocationID<='" + toLocation + "'";
			}
			FillDataSet(dataSet, "Candidate", text);
			text = "SELECT ActivityID,EA.CandidateID,FirstName + ' ' + LastName AS [Candidate Name],TransactionDate,ActivityType,\r\n                            Reason,Reference,Note \r\n                            FROM Candidate_Activity EA\r\n                            INNER JOIN Candidate ON Candidate.CandidateID=EA.CandidateID\r\n                            WHERE 1=1 ";
			if (fromCandidate != "")
			{
				text = text + " AND Candidate.CandidateID>='" + fromCandidate + "'";
			}
			if (toCandidate != "")
			{
				text = text + " AND Candidate.CandidateID<='" + toCandidate + "'";
			}
			if (fromDepartment != "")
			{
				text = text + " AND Candidate.DepartmentID>='" + fromDepartment + "'";
			}
			if (toDepartment != "")
			{
				text = text + " AND Candidate.DepartmentID<='" + toDepartment + "'";
			}
			if (fromLocation != "")
			{
				text = text + " AND Candidate.LocationID>='" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				text = text + " AND Candidate.LocationID<='" + toLocation + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Status,1) = 1 ";
			}
			FillDataSet(dataSet, "Activity", text);
			dataSet.Relations.Add("Candidate_Activity", dataSet.Tables["Candidate"].Columns["CandidateID"], dataSet.Tables["Activity"].Columns["CandidateID"], createConstraints: false);
			return dataSet;
		}

		public bool AddCandidatePhoto(string candidateID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Candidate SET Photo=@Photo WHERE CandidateID='" + candidateID + "'");
				sqlCommand.Parameters.AddWithValue("@Photo", image);
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

		public bool RemoveCandidatePhoto(string candidateID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Candidate SET Photo= Null WHERE CandidateID='" + candidateID + "'");
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

		public MemoryStream GetCandidateThumbnailImage(string candidateID)
		{
			string exp = "SELECT Photo\r\n\t\t\t\t\t\t   FROM Candidate WHERE CandidateID='" + candidateID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				byte[] array = (byte[])obj;
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(array, 0, array.Length);
				return memoryStream;
			}
			return null;
		}

		public string GetNextSequenceNumber(string tableName, string fieldName)
		{
			DataSet dataSet = new DataSet();
			_ = string.Empty;
			int result = 0;
			string result2 = "1".PadLeft(4, '0');
			string empty = string.Empty;
			empty = (("SELECT MAX(CONVERT(INT, ISNULL(" + fieldName + ", 0))) AS ID FROM " + tableName) ?? "");
			FillDataSet(dataSet, "VOUCHER_TABLE", empty);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0 && int.TryParse(dataSet.Tables[0].Rows[0]["ID"].ToString(), out result))
			{
				result2 = (result + 1).ToString().PadLeft(4, '0');
			}
			return result2;
		}

		public bool IsEmployee(string employeeNo)
		{
			int result = 0;
			string exp = "SELECT COUNT(*) FROM Employee WHERE EmployeeID='" + employeeNo + "'";
			new DataSet();
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				int.TryParse(obj.ToString(), out result);
			}
			if (result > 0)
			{
				return true;
			}
			return false;
		}

		public DataSet GetCandidateAppointmentDetails(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT C.*,CASE VisaStatus WHEN 1 THEN 'NA' WHEN 2 THEN 'Bachelor' WHEN 3 THEN 'Family' WHEN 4 THEN 'Visit' WHEN 5 THEN 'Transit' WHEN 6 THEN 'Employment' WHEN 7 THEN 'Other' END AS [VisaStatusName],\r\n                        S.SponsorName,R.ReligionName,CT.CountryName,GL1.GenericListName as Qualification,GL2.GenericListName as Language,EP.PositionName\r\n                        FROM Candidate C LEFT OUTER JOIN  Position EP ON C.DesignationID=EP.PositionID                              \r\n                        LEFT OUTER JOIN Sponsor S ON S.SponsorID=C.SponsorID\r\n                        LEFT OUTER JOIN Religion R ON R.ReligionID=C.ReligionID\r\n                        LEFT OUTER JOIN Country CT ON CT.CountryID=C.NationalID\t\t\t\t\t\t\t\t\r\n                        LEFT OUTER JOIN Generic_List GL1 ON GL1.GenericListID=C.QualificationID\r\n                        LEFT OUTER JOIN Generic_List GL2 ON GL2.GenericListID=C.LanguageID\r\n                        WHERE 1=1 ";
			if (fromCandidate != "")
			{
				text = text + " AND C.CandidateID>='" + fromCandidate + "'";
			}
			if (toCandidate != "")
			{
				text = text + " AND C.CandidateID<='" + toCandidate + "'";
			}
			if (fromDepartment != "")
			{
				text = text + " AND C.DepartmentID>='" + fromDepartment + "'";
			}
			if (toDepartment != "")
			{
				text = text + " AND C.DepartmentID<='" + toDepartment + "'";
			}
			if (fromLocation != "")
			{
				text = text + " AND C.LocationID>='" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				text = text + " AND C.LocationID<='" + toLocation + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(C.Status,1) = 1 ";
			}
			FillDataSet(dataSet, "AppointmentDetails", text);
			DataSet dataSet2 = new DataSet();
			text = "SELECT CandidateID,EAD.PayrollItemID,PayType,PayrollItemName,Amount  FROM Candidate_Salary EAD INNER JOIN PayrollItem ON PayrollItem.PayrollItemID = EAD.PayrollItemID WHERE CandidateID = '" + fromCandidate + "'";
			FillDataSet(dataSet2, "Candidate_Salary", text);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("AppointmentSalaryDetails", new DataColumn[1]
			{
				dataSet.Tables["AppointmentDetails"].Columns["CandidateID"]
			}, new DataColumn[1]
			{
				dataSet.Tables["Candidate_Salary"].Columns["CandidateID"]
			}, createConstraints: false);
			return dataSet;
		}
	}
}
