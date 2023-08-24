using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeActivity : StoreObject
	{
		private const string ACTIVITYID_PARM = "@ActivityID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SUBJECT_PARM = "@Subject";

		private const string ACTIVITYTYPE_PARM = "@ActivityType";

		private const string DOCID_PARM = "@DocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string REASON_PARM = "@Reason";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string EMPLOYEEACTIVITY_TABLE = "Employee_Activity";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string TRANSFERFROMLOCATION_PARM = "@TransferFromLocation";

		private const string TRANSFERTOLOCATION_PARM = "@TransferToLocation";

		private const string TRANSFERFROMDEP_PARM = "@TransferFromDep";

		private const string TRANSFERTODEP_PARM = "@TransferToDep";

		private const string TRANSFERFROMDIVISION_PARM = "@TransferFromDivision";

		private const string TRANSFERTODIVISION_PARM = "@TransferToDivision";

		private const string REQUESTEDBY_PARM = "@RequestedBy";

		private const string PERIOD_PARM = "@Period";

		private const string EMPLOYEETRANSFER_TABLE = "Employee_Transfer";

		private const string TERMINATIONTYPE_PARM = "@TerminationType";

		private const string EMPLOYEETERMINATION_TABLE = "Employee_Termination";

		private const string CANCELLATIONTYPE_PARM = "@CancellationType";

		private const string VCAPPRECEIVEDDATE_PARM = "@VCAppReceivedDate";

		private const string VCAPPTYPEDDATE_PARM = "@VCAppTypedDate";

		private const string VCAPPSUBMITTEDDATE_PARM = "@VCAppSubmittedDate";

		private const string MBREFNUMBERCANCEL_PARM = "@MBReferenceNoCancel";

		private const string RPCANCELDATEIMG_PARM = "@RPCancelDateIMG";

		private const string DEPARTUREDATE_PARM = "@DepartureDate";

		private const string LASTWORKINGDATE_PARM = "@LastWorkingDate";

		private const string EXITPORT_PARM = "@ExitPort";

		private const string SIGNEDCNDOCRECVDDATE_PARM = "@SignedCNDOCRecvdDate";

		private const string FROMGRADE_PARM = "@FromGrade";

		private const string TOGRADE_PARM = "@ToGrade";

		private const string FROMPOSITION_PARM = "@FromPosition";

		private const string TOPOSITION_PARM = "@ToPosition";

		private const string EMPLOYEEPROMOTION_TABLE = "Employee_Promotion";

		private const string DISCIPLINARYACTIONTYPEID_PARM = "@ActionTypeID";

		private const string EMPLOYEEDISCIPLINARYACTION_TABLE = "Employee_DisciplinaryAction";

		private const string EMPLOYEEREHIRE_TABLE = "Employee_Rehire";

		private const string GENERALACTIVITYTYPE_PARM = "@GeneralActivityTypeID";

		private const string EMPLOYEELEAVEREQUEST_TABLE = "Employee_Leave_Request";

		private const string LEAVETYPEID_PARM = "@LeaveTypeID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string ACTUALLEAVEDAYS_PARM = "@ActualLeaveDays";

		private const string TICKETENTITLE_PARM = "@TicketEntitle";

		private const string REPLACEMENTID_PARM = "@ReplacementID";

		private const string ISAPPROVED_PARM = "@IsApproved";

		private const string ISCLOSED_PARM = "@IsClosed";

		private const string ISVOID_PARM = "@IsVoid";

		private const string APPROVEDBY_PARM = "@ApprovedBy";

		private const string APPROVEDDATE_PARM = "@ApproveDate";

		private const string RESUMPTIONDATE_PARM = "@ResumptionDate";

		private const string TRAVELLINGDATE_PARM = "@TravellingDate";

		private const string APPROVALREMARKS_PARM = "@ApprovalRemarks";

		private const string EMPLOYEELEAVEENCASHMENT_TABLE = "Employee_Leave_Encashment";

		private const string ENCASHNO_PARM = "@EncashNo";

		private const string ASONDATE_PARM = "@AsOnDate";

		private const string LEAVEELIGIBLE_PARM = "@LeaveEligible";

		private const string LEAVEENCASH_PARM = "@LeaveEncash";

		private const string AMOUNTENCASH_PARM = "@AmountEncash";

		private const string LEAVEPAYIDFIELD_PARM = "@LeavePaymentID";

		private const string LEAVEAPPNOFIELD_PARM = "@LeaveApplicationNo";

		private const string AMOUNT_PARM = "@Amount";

		private const string TICKETAMOUNT_PARM = "@TicketAmount";

		private const string TOTAL_PARM = "@Total";

		private const string SALARYAMOUNT_PARM = "@SalaryAmount";

		private const string ELIGIBLEDAYS_PARM = "@EligibleDays";

		private const string DEDUCTIONID_PARM = "@DeductionID";

		private const string DEDUCTIONAMOUNT_PARM = "@DeductionAmount";

		private const string BASEDONLEAVETAKEN_PARM = "@BasedOnLeaveTaken";

		private const string EMPLOYEERESUMPTION_TABLE = "Employee_Resumption";

		private const string LEAVEID_PARM = "@LeaveID";

		private const string EMPLOYEEPASSPORTCONTROL_TABLE = "Employee_Passport_Control";

		private const string REASONID_PARM = "@ReasonID";

		private const string PPRELEASEDATE_PARM = "@PPReleaseDate";

		private const string PPRETURNDATE_PARM = "@PPReturnDate";

		private const string ISSUEDBY_PARM = "@IssuedBy";

		private const string ACCEPTEDBY_PARM = "@AcceptedBy";

		private const string RETURNNOTE_PARM = "@ReturnNote";

		private const string RATING_PARM = "@Rating";

		private const string EMPLOYEEABSCONDING_TABLE = "Employee_Absconding";

		private const string ADVICERECEIVEDON_PARM = "@AdviceReceivedOn";

		private const string REALABSCONDINGDDATE_PARM = "@RealAbscondingDate";

		private const string ABSCONDINGREGDATEMOL_PARM = "@AbscondingRegDateMOL";

		private const string MBREFNUMBER_PARM = "@MBReferenceNo";

		private const string ABSCONDINGREGDATEIMG_PARM = "@AbscondingRegDateIMG";

		private const string MBREFNUMBERABS_PARM = "@AbscondingReferenceNo";

		private const string PASSPORTHELDINIMG_PARM = "@PassportHeldInIMG";

		private const string MOLCANCELLATIONDATE_PARM = "@MOLCancellationDate";

		private const string IMGCANCELLATIONDATE_PARM = "@IMGCancellationDate";

		private const string TICKETAMOUNTPAID_PARM = "@TicketAmountPaid";

		public EmployeeActivity(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateEmployeeActivityText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Activity", new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Subject", "@Subject"), new FieldValue("ActivityType", "@ActivityType"), new FieldValue("Reason", "@Reason"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Activity", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Activity", new FieldValue("ActivityID", "@ActivityID", isUpdateConditionField: true));
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeActivityCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeActivityText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeActivityText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			if (isUpdate)
			{
				parameters.Add("@ActivityID", SqlDbType.Int);
			}
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Subject", SqlDbType.NVarChar);
			parameters.Add("@ActivityType", SqlDbType.TinyInt);
			parameters.Add("@Reason", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			if (isUpdate)
			{
				parameters["@ActivityID"].SourceColumn = "ActivityID";
			}
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Subject"].SourceColumn = "Subject";
			parameters["@ActivityType"].SourceColumn = "ActivityType";
			parameters["@Reason"].SourceColumn = "Reason";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
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

		private string GetInsertUpdateEmployeeTransferText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Transfer", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("TransferFromLocation", "@TransferFromLocation"), new FieldValue("TransferToLocation", "@TransferToLocation"), new FieldValue("TransferFromDep", "@TransferFromDep"), new FieldValue("TransferToDep", "@TransferToDep"), new FieldValue("TransferFromDivision", "@TransferFromDivision"), new FieldValue("TransferToDivision", "@TransferToDivision"), new FieldValue("FromPosition", "@FromPosition"), new FieldValue("ToPosition", "@ToPosition"), new FieldValue("RequestedBy", "@RequestedBy"), new FieldValue("Period", "@Period"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeTransferCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeTransferText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeTransferText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@TransferFromLocation", SqlDbType.NVarChar);
			parameters.Add("@TransferToLocation", SqlDbType.NVarChar);
			parameters.Add("@TransferFromDep", SqlDbType.NVarChar);
			parameters.Add("@TransferToDep", SqlDbType.NVarChar);
			parameters.Add("@TransferFromDivision", SqlDbType.NVarChar);
			parameters.Add("@TransferToDivision", SqlDbType.NVarChar);
			parameters.Add("@FromPosition", SqlDbType.NVarChar);
			parameters.Add("@ToPosition", SqlDbType.NVarChar);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters.Add("@Period", SqlDbType.Char);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@TransferFromLocation"].SourceColumn = "TransferFromLocation";
			parameters["@TransferToLocation"].SourceColumn = "TransferToLocation";
			parameters["@TransferFromDep"].SourceColumn = "TransferFromDep";
			parameters["@TransferToDep"].SourceColumn = "TransferToDep";
			parameters["@TransferFromDivision"].SourceColumn = "TransferFromDivision";
			parameters["@TransferToDivision"].SourceColumn = "TransferToDivision";
			parameters["@FromPosition"].SourceColumn = "FromPosition";
			parameters["@ToPosition"].SourceColumn = "ToPosition";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			parameters["@Period"].SourceColumn = "Period";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeTerminationText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Termination", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("TerminationType", "@TerminationType"), new FieldValue("RequestedBy", "@RequestedBy"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeTerminationCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeTerminationText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeTerminationText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters.Add("@TerminationType", SqlDbType.Char);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			parameters["@TerminationType"].SourceColumn = "TerminationType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeCancellationText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Cancellation", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("CancellationType", "@CancellationType"), new FieldValue("VCAppReceivedDate", "@VCAppReceivedDate"), new FieldValue("VCAppTypedDate", "@VCAppTypedDate"), new FieldValue("VCAppSubmittedDate", "@VCAppSubmittedDate"), new FieldValue("MBReferenceNoCancel", "@MBReferenceNoCancel"), new FieldValue("RPCancelDateIMG", "@RPCancelDateIMG"), new FieldValue("DepartureDate", "@DepartureDate"), new FieldValue("LastWorkingDate", "@LastWorkingDate"), new FieldValue("SignedCNDOCRecvdDate", "@SignedCNDOCRecvdDate"), new FieldValue("ExitPort", "@ExitPort"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Cancellation", new FieldValue("ActivityID", "@ActivityID", isUpdateConditionField: true));
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeCancellationCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeCancellationText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeCancellationText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@CancellationType", SqlDbType.TinyInt);
			parameters.Add("@VCAppReceivedDate", SqlDbType.DateTime);
			parameters.Add("@VCAppTypedDate", SqlDbType.DateTime);
			parameters.Add("@VCAppSubmittedDate", SqlDbType.DateTime);
			parameters.Add("@MBReferenceNoCancel", SqlDbType.NVarChar);
			parameters.Add("@RPCancelDateIMG", SqlDbType.DateTime);
			parameters.Add("@DepartureDate", SqlDbType.DateTime);
			parameters.Add("@LastWorkingDate", SqlDbType.DateTime);
			parameters.Add("@SignedCNDOCRecvdDate", SqlDbType.DateTime);
			parameters.Add("@ExitPort", SqlDbType.NVarChar);
			parameters["@CancellationType"].SourceColumn = "CancellationType";
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@VCAppReceivedDate"].SourceColumn = "VCAppReceivedDate";
			parameters["@VCAppTypedDate"].SourceColumn = "VCAppTypedDate";
			parameters["@VCAppSubmittedDate"].SourceColumn = "VCAppSubmittedDate";
			parameters["@MBReferenceNoCancel"].SourceColumn = "MBReferenceNoCancel";
			parameters["@RPCancelDateIMG"].SourceColumn = "RPCancelDateIMG";
			parameters["@DepartureDate"].SourceColumn = "DepartureDate";
			parameters["@LastWorkingDate"].SourceColumn = "LastWorkingDate";
			parameters["@SignedCNDOCRecvdDate"].SourceColumn = "SignedCNDOCRecvdDate";
			parameters["@ExitPort"].SourceColumn = "ExitPort";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeePromotionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Promotion", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("FromGrade", "@FromGrade"), new FieldValue("ToGrade", "@ToGrade"), new FieldValue("FromPosition", "@FromPosition"), new FieldValue("ToPosition", "@ToPosition"), new FieldValue("RequestedBy", "@RequestedBy"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeePromotionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeePromotionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeePromotionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters.Add("@FromGrade", SqlDbType.NVarChar);
			parameters.Add("@ToGrade", SqlDbType.NVarChar);
			parameters.Add("@FromPosition", SqlDbType.NVarChar);
			parameters.Add("@ToPosition", SqlDbType.NVarChar);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			parameters["@FromGrade"].SourceColumn = "FromGrade";
			parameters["@ToGrade"].SourceColumn = "ToGrade";
			parameters["@FromPosition"].SourceColumn = "FromPosition";
			parameters["@ToPosition"].SourceColumn = "ToPosition";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeDisciplinaryActionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_DisciplinaryAction", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("ActionTypeID", "@ActionTypeID"), new FieldValue("RequestedBy", "@RequestedBy"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeDisciplinaryActionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeDisciplinaryActionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeDisciplinaryActionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters.Add("@ActionTypeID", SqlDbType.NVarChar);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			parameters["@ActionTypeID"].SourceColumn = "ActionTypeID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeRehireText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Rehire", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("RequestedBy", "@RequestedBy"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeRehireCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeRehireText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeRehireText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeLeaveRequestText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Leave_Request", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("DocNumber", "@DocID"), new FieldValue("LeaveTypeID", "@LeaveTypeID"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("ActualLeaveDays", "@ActualLeaveDays"), new FieldValue("TicketEntitle", "@TicketEntitle"), new FieldValue("ReplacementID", "@ReplacementID"), new FieldValue("IsApproved", "@IsApproved"), new FieldValue("IsClosed", "@IsClosed"), new FieldValue("ApprovedBy", "@ApprovedBy"), new FieldValue("ResumptionDate", "@ResumptionDate"), new FieldValue("TravellingDate", "@TravellingDate"), new FieldValue("ApprovalRemarks", "@ApprovalRemarks"), new FieldValue("ApproveDate", "@ApproveDate"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Leave_Request", new FieldValue("ActivityID", "@ActivityID", isUpdateConditionField: true));
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeLeaveRequestCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeLeaveRequestText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeLeaveRequestText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@DocID", SqlDbType.NVarChar);
			parameters.Add("@LeaveTypeID", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@TicketEntitle", SqlDbType.Bit);
			parameters.Add("@ReplacementID", SqlDbType.NVarChar);
			parameters.Add("@IsApproved", SqlDbType.Bit);
			parameters.Add("@IsClosed", SqlDbType.Bit);
			parameters.Add("@ApprovedBy", SqlDbType.NVarChar);
			parameters.Add("@ApproveDate", SqlDbType.DateTime);
			parameters.Add("@ResumptionDate", SqlDbType.DateTime);
			parameters.Add("@TravellingDate", SqlDbType.DateTime);
			parameters.Add("@ActualLeaveDays", SqlDbType.Int);
			parameters.Add("@ApprovalRemarks", SqlDbType.NVarChar);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@DocID"].SourceColumn = "DocNumber";
			parameters["@LeaveTypeID"].SourceColumn = "LeaveTypeID";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@ActualLeaveDays"].SourceColumn = "ActualLeaveDays";
			parameters["@TicketEntitle"].SourceColumn = "TicketEntitle";
			parameters["@ReplacementID"].SourceColumn = "ReplacementID";
			parameters["@IsApproved"].SourceColumn = "IsApproved";
			parameters["@IsClosed"].SourceColumn = "IsClosed";
			parameters["@ApprovedBy"].SourceColumn = "ApprovedBy";
			parameters["@ApproveDate"].SourceColumn = "ApproveDate";
			parameters["@ResumptionDate"].SourceColumn = "ResumptionDate";
			parameters["@TravellingDate"].SourceColumn = "TravellingDate";
			parameters["@ApprovalRemarks"].SourceColumn = "ApprovalRemarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeLeaveEncashmentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Leave_Encashment", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("EncashNo", "@EncashNo"), new FieldValue("AsOnDate", "@AsOnDate"), new FieldValue("LeaveEligible", "@LeaveEligible"), new FieldValue("LeaveEncash", "@LeaveEncash"), new FieldValue("AmountEncash", "@AmountEncash"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeLeaveEncashmentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeLeaveEncashmentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeLeaveEncashmentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@EncashNo", SqlDbType.NVarChar);
			parameters.Add("@AsOnDate", SqlDbType.DateTime);
			parameters.Add("@LeaveEligible", SqlDbType.Int);
			parameters.Add("@LeaveEncash", SqlDbType.Int);
			parameters.Add("@AmountEncash", SqlDbType.Decimal);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@EncashNo"].SourceColumn = "EncashNo";
			parameters["@AsOnDate"].SourceColumn = "AsOnDate";
			parameters["@LeaveEligible"].SourceColumn = "LeaveEligible";
			parameters["@LeaveEncash"].SourceColumn = "LeaveEncash";
			parameters["@AmountEncash"].SourceColumn = "AmountEncash";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeLeavePaymentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Leave_Payment", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ActivityID", "@ActivityID"), new FieldValue("LeavePaymentID", "@LeavePaymentID"), new FieldValue("LeaveApplicationNo", "@LeaveApplicationNo"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("Amount", "@Amount"), new FieldValue("Total", "@Total"), new FieldValue("TicketAmount", "@TicketAmount"), new FieldValue("SalaryAmount", "@SalaryAmount"), new FieldValue("EligibleDays", "@EligibleDays"), new FieldValue("DeductionID", "@DeductionID"), new FieldValue("DeductionAmount", "@DeductionAmount"), new FieldValue("BasedOnLeaveTaken", "@BasedOnLeaveTaken"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeLeavePaymentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeLeavePaymentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeLeavePaymentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@LeavePaymentID", SqlDbType.NVarChar);
			parameters.Add("@LeaveApplicationNo", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@Total", SqlDbType.Money);
			parameters.Add("@TicketAmount", SqlDbType.Money);
			parameters.Add("@SalaryAmount", SqlDbType.Money);
			parameters.Add("@EligibleDays", SqlDbType.Int);
			parameters.Add("@DeductionAmount", SqlDbType.Money);
			parameters.Add("@DeductionID", SqlDbType.NVarChar);
			parameters.Add("@BasedOnLeaveTaken", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@LeavePaymentID"].SourceColumn = "LeavePaymentID";
			parameters["@LeaveApplicationNo"].SourceColumn = "LeaveApplicationNo";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@TicketAmount"].SourceColumn = "TicketAmount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@SalaryAmount"].SourceColumn = "SalaryAmount";
			parameters["@EligibleDays"].SourceColumn = "EligibleDays";
			parameters["@DeductionID"].SourceColumn = "DeductionID";
			parameters["@DeductionAmount"].SourceColumn = "DeductionAmount";
			parameters["@BasedOnLeaveTaken"].SourceColumn = "BasedOnLeaveTaken";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeGeneralActivityText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_GeneralActivity", new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("GeneralActivityTypeID", "@GeneralActivityTypeID"), new FieldValue("RequestedBy", "@RequestedBy"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Subject", "@Subject"), new FieldValue("ActivityType", "@ActivityType"), new FieldValue("Reason", "@Reason"), new FieldValue("Rating", "@Rating"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeGeneralActivityCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeGeneralActivityText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeGeneralActivityText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters.Add("@GeneralActivityTypeID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Subject", SqlDbType.NVarChar);
			parameters.Add("@ActivityType", SqlDbType.TinyInt);
			parameters.Add("@Reason", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Rating", SqlDbType.TinyInt);
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			parameters["@GeneralActivityTypeID"].SourceColumn = "GeneralActivityTypeID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Subject"].SourceColumn = "Subject";
			parameters["@ActivityType"].SourceColumn = "ActivityType";
			parameters["@Reason"].SourceColumn = "Reason";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Rating"].SourceColumn = "Rating";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeResumptionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Resumption", new FieldValue("ActivityID", "@ActivityID", isUpdateConditionField: true), new FieldValue("LeaveID", "@LeaveID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeResumptionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeResumptionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeResumptionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@LeaveID", SqlDbType.Int);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@LeaveID"].SourceColumn = "LeaveID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeePassPortControlText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Passport_Control", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("DocNumber", "@DocID"), new FieldValue("ReasonID", "@ReasonID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("PPReleaseDate", "@PPReleaseDate"), new FieldValue("PPReturnDate", "@PPReturnDate"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("ApprovedBy", "@ApprovedBy"), new FieldValue("IssuedBy", "@IssuedBy"), new FieldValue("AcceptedBy", "@AcceptedBy"), new FieldValue("ReturnNote", "@ReturnNote"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Passport_Control", new FieldValue("ActivityID", "@ActivityID", isUpdateConditionField: true));
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeePassPortControlCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeePassPortControlText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeePassPortControlText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@DocID", SqlDbType.NVarChar);
			parameters.Add("@ReasonID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@PPReleaseDate", SqlDbType.DateTime);
			parameters.Add("@PPReturnDate", SqlDbType.DateTime);
			parameters.Add("@IsVoid", SqlDbType.Bit);
			parameters.Add("@ApprovedBy", SqlDbType.NVarChar);
			parameters.Add("@IssuedBy", SqlDbType.NVarChar);
			parameters.Add("@AcceptedBy", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@ReturnNote", SqlDbType.NVarChar);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@DocID"].SourceColumn = "DocNumber";
			parameters["@ReasonID"].SourceColumn = "ReasonID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@PPReleaseDate"].SourceColumn = "PPReleaseDate";
			parameters["@PPReturnDate"].SourceColumn = "PPReturnDate";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters["@ApprovedBy"].SourceColumn = "ApprovedBy";
			parameters["@IssuedBy"].SourceColumn = "IssuedBy";
			parameters["@AcceptedBy"].SourceColumn = "AcceptedBy";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@ReturnNote"].SourceColumn = "ReturnNote";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeAbscondingText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Absconding", new FieldValue("ActivityID", "@ActivityID"), new FieldValue("AdviceReceivedOn", "@AdviceReceivedOn"), new FieldValue("RealAbscondingDate", "@RealAbscondingDate"), new FieldValue("AbscondingRegDateMOL", "@AbscondingRegDateMOL"), new FieldValue("MBReferenceNo", "@MBReferenceNo"), new FieldValue("AbscondingRegDateIMG", "@AbscondingRegDateIMG"), new FieldValue("AbscondingReferenceNo", "@AbscondingReferenceNo"), new FieldValue("MOLCancellationDate", "@MOLCancellationDate"), new FieldValue("IMGCancellationDate", "@IMGCancellationDate"), new FieldValue("LastWorkingDate", "@LastWorkingDate"), new FieldValue("PassportHeldInIMG", "@PassportHeldInIMG"), new FieldValue("TicketAmountPaid", "@TicketAmountPaid"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Absconding", new FieldValue("ActivityID", "@ActivityID", isUpdateConditionField: true));
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeAbscondingCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeAbscondingText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeAbscondingText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ActivityID", SqlDbType.Int);
			parameters.Add("@AdviceReceivedOn", SqlDbType.DateTime);
			parameters.Add("@RealAbscondingDate", SqlDbType.DateTime);
			parameters.Add("@AbscondingRegDateMOL", SqlDbType.DateTime);
			parameters.Add("@MBReferenceNo", SqlDbType.NVarChar);
			parameters.Add("@AbscondingRegDateIMG", SqlDbType.DateTime);
			parameters.Add("@AbscondingReferenceNo", SqlDbType.NVarChar);
			parameters.Add("@LastWorkingDate", SqlDbType.DateTime);
			parameters.Add("@MOLCancellationDate", SqlDbType.DateTime);
			parameters.Add("@IMGCancellationDate", SqlDbType.DateTime);
			parameters.Add("@PassportHeldInIMG", SqlDbType.NVarChar);
			parameters.Add("@TicketAmountPaid", SqlDbType.NVarChar);
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@AdviceReceivedOn"].SourceColumn = "AdviceReceivedOn";
			parameters["@RealAbscondingDate"].SourceColumn = "RealAbscondingDate";
			parameters["@AbscondingRegDateMOL"].SourceColumn = "AbscondingRegDateMOL";
			parameters["@MBReferenceNo"].SourceColumn = "MBReferenceNo";
			parameters["@AbscondingRegDateIMG"].SourceColumn = "AbscondingRegDateIMG";
			parameters["@AbscondingReferenceNo"].SourceColumn = "AbscondingReferenceNo";
			parameters["@LastWorkingDate"].SourceColumn = "LastWorkingDate";
			parameters["@MOLCancellationDate"].SourceColumn = "MOLCancellationDate";
			parameters["@IMGCancellationDate"].SourceColumn = "IMGCancellationDate";
			parameters["@PassportHeldInIMG"].SourceColumn = "PassportHeldInIMG";
			parameters["@TicketAmountPaid"].SourceColumn = "TicketAmountPaid";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(EmployeeActivityData journalData)
		{
			return true;
		}

		public bool InsertUpdateEmployeeActivity(EmployeeActivityData employeeActivityData, EmployeeActivityTypes activityType, bool isUpdate)
		{
			bool flag = true;
			string text = "";
			try
			{
				SqlCommand insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeActivityCommand(isUpdate);
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow = null;
				string text2 = "";
				if (activityType != EmployeeActivityTypes.General)
				{
					dataRow = employeeActivityData.EmployeeActivityTable.Rows[0];
					text2 = dataRow["EmployeeID"].ToString();
				}
				insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
				int num = -1;
				if (activityType != EmployeeActivityTypes.General)
				{
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Activity", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Activity", insertUpdateEmployeeActivityCommand)));
					num = ((!isUpdate) ? int.Parse(GetInsertedRowIdentity("Employee_Activity", insertUpdateEmployeeActivityCommand).ToString()) : int.Parse(dataRow["ActivityID"].ToString()));
					foreach (DataRow row in employeeActivityData.Tables[1].Rows)
					{
						row["ActivityID"] = num;
					}
				}
				switch (activityType)
				{
				case EmployeeActivityTypes.Transfer:
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeTransferCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Transfer", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Transfer", insertUpdateEmployeeActivityCommand)));
					text = "UPDATE Employee SET\r\n                            LocationID=(SELECT TransferToLocation FROM Employee_Transfer WHERE ActivityID=" + num.ToString() + "),\r\n                            DivisionID=(SELECT TransferToDivision FROM Employee_Transfer WHERE ActivityID=" + num.ToString() + "),\r\n                            DepartmentID=(SELECT TransferToDep FROM Employee_Transfer WHERE ActivityID=" + num.ToString() + "),\r\n                            PositionID=(SELECT ToPosition FROM Employee_Transfer WHERE ActivityID=" + num.ToString() + ")\r\n                            WHERE EmployeeID='" + text2 + "'";
					flag &= Update(text, sqlTransaction);
					break;
				case EmployeeActivityTypes.Termination:
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeTerminationCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					if (!isUpdate)
					{
						object fieldValue3 = new Databases(base.DBConfig).GetFieldValue("Employee", "IsTerminated", "EmployeeID", text2, sqlTransaction);
						if (fieldValue3 != null && fieldValue3.ToString() != "" && bool.Parse(fieldValue3.ToString()))
						{
							throw new CompanyException("Employee is already terminated", 1017);
						}
					}
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Termination", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Termination", insertUpdateEmployeeActivityCommand)));
					text = "UPDATE Employee SET\r\n                            IsTerminated= 'True',\r\n                            TerminationDate=(SELECT TransactionDate FROM Employee_Activity WHERE ActivityID=" + num.ToString() + "),\r\n                            TerminationID =" + num.ToString() + "\r\n                            WHERE EmployeeID = '" + text2 + "'";
					flag &= Update(text, sqlTransaction);
					break;
				case EmployeeActivityTypes.Cancellation:
				{
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeCancellationCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					if (!isUpdate)
					{
						object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee", "IsCancelled", "EmployeeID", text2, sqlTransaction);
						if (fieldValue != null && fieldValue.ToString() != "" && bool.Parse(fieldValue.ToString()))
						{
							throw new CompanyException("Employee is already cancelled", 1052);
						}
					}
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Cancellation", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Cancellation", insertUpdateEmployeeActivityCommand)));
					byte b = byte.Parse(employeeActivityData.EmployeeCancellationTable.Rows[0]["CancellationType"].ToString());
					DateTime result = DateTime.Now;
					if (DateTime.TryParse(employeeActivityData.EmployeeCancellationTable.Rows[0]["DepartureDate"].ToString(), out result))
					{
						text = "UPDATE Employee SET\r\n                            IsCancelled= 'True', Status = " + b + ",\r\n                            CancellationDate=(SELECT TransactionDate FROM Employee_Activity WHERE ActivityID=" + num.ToString() + ")\r\n                            WHERE EmployeeID = '" + text2 + "'";
						flag &= Update(text, sqlTransaction);
					}
					break;
				}
				case EmployeeActivityTypes.Absconding:
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeAbscondingCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Absconding", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Absconding", insertUpdateEmployeeActivityCommand)));
					break;
				case EmployeeActivityTypes.Promotion:
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeePromotionCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Promotion", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Promotion", insertUpdateEmployeeActivityCommand)));
					text = "UPDATE Employee SET\r\n                            GradeID=(SELECT ToGrade FROM Employee_Promotion WHERE ActivityID=" + num.ToString() + "),\r\n                            PositionID=(SELECT ToPosition FROM Employee_Promotion WHERE ActivityID=" + num.ToString() + ")\r\n                            WHERE EmployeeID = '" + text2 + "'";
					flag &= Update(text, sqlTransaction);
					break;
				case EmployeeActivityTypes.DisciplinaryAction:
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeDisciplinaryActionCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_DisciplinaryAction", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_DisciplinaryAction", insertUpdateEmployeeActivityCommand)));
					break;
				case EmployeeActivityTypes.Rehire:
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeRehireCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					if (!isUpdate)
					{
						object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Employee", "IsTerminated", "EmployeeID", text2, sqlTransaction);
						if (fieldValue2 != null && fieldValue2.ToString() != "" && !bool.Parse(fieldValue2.ToString()))
						{
							throw new CompanyException("Employee is active.", 1018);
						}
					}
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Rehire", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Rehire", insertUpdateEmployeeActivityCommand)));
					text = "UPDATE Employee SET\r\n                            IsTerminated= 'False',\r\n                            RehireDate=(SELECT TransactionDate FROM Employee_Activity WHERE ActivityID=" + num.ToString() + ")\r\n                            WHERE EmployeeID = '" + text2 + "'";
					flag &= Update(text, sqlTransaction);
					break;
				case EmployeeActivityTypes.General:
				{
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeGeneralActivityCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_GeneralActivity", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_GeneralActivity", insertUpdateEmployeeActivityCommand)));
					DataRow dataRow4 = employeeActivityData.EmployeeGeneralActivityTable.Rows[0];
					string text4 = dataRow4["VoucherID"].ToString();
					string sysDocID = dataRow4["SysDocID"].ToString();
					if (!isUpdate)
					{
						flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(sysDocID, text4, "Employee_GeneralActivity", "VoucherID", sqlTransaction);
					}
					if (flag)
					{
						new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.EmployeeGeneralActivity, sysDocID, text4, "Employee_GeneralActivity", sqlTransaction);
					}
					break;
				}
				case EmployeeActivityTypes.Leave:
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeLeaveRequestCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					if (isUpdate)
					{
						foreach (DataRow row2 in employeeActivityData.EmployeeLeaveRequestTable.Rows)
						{
							row2["ApproveDate"] = DBNull.Value;
							row2["ApprovedBy"] = DBNull.Value;
							row2["ResumptionDate"] = DBNull.Value;
						}
					}
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Leave_Request", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Leave_Request", insertUpdateEmployeeActivityCommand)));
					break;
				case EmployeeActivityTypes.LeaveEncashment:
				{
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeLeaveEncashmentCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Leave_Encashment", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Leave_Encashment", insertUpdateEmployeeActivityCommand)));
					GLData journalData2 = CreateLeaveEncashmentGLData(employeeActivityData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData2, isUpdate, sqlTransaction);
					break;
				}
				case EmployeeActivityTypes.LeavePayment:
				{
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeLeavePaymentCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Leave_Payment", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Leave_Payment", insertUpdateEmployeeActivityCommand)));
					GLData journalData = CreateLeavePaymentGLData(employeeActivityData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
					int num3 = 0;
					foreach (DataRow row3 in employeeActivityData.Tables[2].Rows)
					{
						num3 = int.Parse(row3["ActivityID"].ToString());
					}
					text = "UPDATE Employee_Leave_Request SET\r\n                            IsPaid= 'True'                        \r\n                            WHERE ActivityID = '" + num3.ToString() + "'";
					flag &= Update(text, sqlTransaction);
					if (!isUpdate)
					{
						DataRow dataRow3 = employeeActivityData.EmployeeLeavePaymentTable.Rows[0];
						flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow3["SysDocID"].ToString(), dataRow3["VoucherID"].ToString(), "Employee_Leave_Payment", "VoucherID", sqlTransaction);
					}
					break;
				}
				case EmployeeActivityTypes.Resumption:
				{
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeResumptionCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Resumption", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Resumption", insertUpdateEmployeeActivityCommand)));
					if (employeeActivityData.Tables.Contains("Employee_Leave_Request") && employeeActivityData.Tables["Employee_Leave_Request"].Rows.Count > 0)
					{
						foreach (DataRow row4 in employeeActivityData.EmployeeLeaveRequestTable.Rows)
						{
							row4["ActivityID"] = num;
						}
						insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeeLeaveRequestCommand(isUpdate);
						insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
						flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Leave_Request", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Leave_Request", insertUpdateEmployeeActivityCommand)));
					}
					int num2 = 0;
					foreach (DataRow row5 in employeeActivityData.Tables["Employee_Resumption"].Rows)
					{
						num2 = int.Parse(row5["LeaveID"].ToString());
					}
					text = "UPDATE Employee_Leave_Request SET\r\n                            ResumptionDate= (SELECT TransactionDate FROM Employee_Activity WHERE ActivityID=" + num.ToString() + ")                     \r\n                            WHERE ActivityID = '" + num2.ToString() + "'";
					flag &= Update(text, sqlTransaction);
					text = "SELECT ISNULL(IsAnnual,0) as IsAnnual FROM Leave_Type LT INNER JOIN Employee_Leave_Request ELR ON LT.LeaveTypeID=ELR.LeaveTypeID WHERE ELR.ActivityID=" + num2.ToString();
					if (bool.Parse(ExecuteScalar(text, sqlTransaction).ToString()))
					{
						text = "UPDATE Employee SET\r\n                            OnVacation= 'False',\r\n                            ResumedDate=(SELECT TransactionDate FROM Employee_Activity WHERE ActivityID=" + num.ToString() + ")\r\n                            WHERE EmployeeID = '" + text2 + "'";
						flag &= Update(text, sqlTransaction);
					}
					foreach (DataRow row6 in employeeActivityData.Tables["Employee_Resumption"].Rows)
					{
						if (row6["EndDate"].ToString() != "" && row6["EndDate"] != DBNull.Value)
						{
							string text3 = CommonLib.ToSqlDateTimeString(DateTime.Parse(row6["EndDate"].ToString()));
							text = "UPDATE Employee_Leave_Request SET\r\n                            EndDate= '" + text3 + "'                     \r\n                            WHERE ActivityID = '" + num2.ToString() + "'";
							flag &= Update(text, sqlTransaction);
						}
					}
					break;
				}
				case EmployeeActivityTypes.PassportControl:
					insertUpdateEmployeeActivityCommand = GetInsertUpdateEmployeePassPortControlCommand(isUpdate);
					insertUpdateEmployeeActivityCommand.Transaction = sqlTransaction;
					flag = (isUpdate ? (flag & Update(employeeActivityData, "Employee_Passport_Control", insertUpdateEmployeeActivityCommand)) : (flag & Insert(employeeActivityData, "Employee_Passport_Control", insertUpdateEmployeeActivityCommand)));
					break;
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Employee_Activity", "ActivityID", num, sqlTransaction, !isUpdate);
				string entityName = "Employee Activity - " + activityType.ToString();
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, num.ToString(), ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, num.ToString(), ActivityTypes.Add, sqlTransaction);
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

		private GLData CreateLeaveEncashmentGLData(EmployeeActivityData employeeActivityData, SqlTransaction sqlTransaction)
		{
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				GLData gLData = new GLData();
				DataRow dataRow = employeeActivityData.EmployeeActivityTable.Rows[0];
				DataRow dataRow2 = employeeActivityData.EmployeeLeaveEncashmentTable.Rows[0];
				string text = "";
				string value = dataRow["ActivityType"].ToString();
				string value2 = dataRow2["EncashNo"].ToString();
				string text2 = dataRow["EmployeeID"].ToString();
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.EmployeeLeaveEncashment;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = value;
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = value2;
				dataRow3["Note"] = dataRow["Note"].ToString();
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				foreach (DataRow row in employeeActivityData.EmployeeLeaveEncashmentTable.Rows)
				{
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee", "AccountID", "EmployeeID", text2, sqlTransaction);
					if (fieldValue == null || !(fieldValue.ToString() != ""))
					{
						throw new CompanyException("Account is not set for the employee '" + text2 + "'.", 1021);
					}
					text = fieldValue.ToString();
					decimal num = default(decimal);
					string text3 = PayCodeTypes.Allowance.ToString();
					string value3 = "";
					num = Math.Round(decimal.Parse(row["AmountEncash"].ToString()), currencyDecimalPoints);
					DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text;
					dataRow4["PayeeID"] = text2;
					dataRow4["PayeeType"] = "E";
					dataRow4["IsARAP"] = true;
					dataRow4["Credit"] = num;
					dataRow4["Debit"] = DBNull.Value;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
					byte b = 1;
					object obj2 = null;
					if (b == 1 || b == 2)
					{
						obj2 = new Databases(base.DBConfig).GetFieldValue("PayrollItem", "AccountID", "PayrollItemID", text3, sqlTransaction);
						if (obj2 == null || obj2.ToString() == "")
						{
							throw new CompanyException("Account is not set for payroll item '" + text3 + "'.", 1022);
						}
						value3 = obj2.ToString();
					}
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = value3;
					dataRow4["PayeeID"] = text2;
					dataRow4["Debit"] = num;
					dataRow4["Credit"] = DBNull.Value;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateLeavePaymentGLData(EmployeeActivityData employeeActivityData, SqlTransaction sqlTransaction)
		{
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				GLData gLData = new GLData();
				DataRow dataRow = employeeActivityData.EmployeeActivityTable.Rows[0];
				DataRow dataRow2 = employeeActivityData.EmployeeLeavePaymentTable.Rows[0];
				string text = "";
				string value = dataRow2["SysDocID"].ToString();
				string value2 = dataRow2["VoucherID"].ToString();
				string text2 = dataRow["EmployeeID"].ToString();
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.EmployeeLeavePayment;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = value;
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = value2;
				dataRow3["Note"] = dataRow["Note"].ToString();
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				foreach (DataRow row in employeeActivityData.EmployeeLeavePaymentTable.Rows)
				{
					string text3 = employeeActivityData.EmployeeLeavePaymentTable.Rows[0]["SysDocID"].ToString();
					string textCommand = "SELECT ISNULL(Emp.AccountID, ISNULL(CLS.AccountID,LOC.EmployeeAccountID )) AS EmployeeAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Employee EMP ON EmployeeID = '" + text2 + "'\r\n                                LEFT OUTER JOIN Employee_Type CLS ON EMP.ContractType = CLS.TypeID\r\n                                WHERE SysDocID = '" + text3 + "'";
					DataSet dataSet = new DataSet();
					FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("There is no location assigned to this system document or location record is missing.");
					}
					text = dataSet.Tables["Accounts"].Rows[0]["EmployeeAccountID"].ToString();
					decimal num = default(decimal);
					PayCodeTypes.Allowance.ToString();
					string value3 = "";
					num = Math.Round(decimal.Parse(row["Amount"].ToString()), currencyDecimalPoints) + Math.Round(decimal.Parse(row["TicketAmount"].ToString()), currencyDecimalPoints);
					num = decimal.Parse(row["Amount"].ToString()) + decimal.Parse(row["TicketAmount"].ToString()) - decimal.Parse(row["DeductionAmount"].ToString());
					DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = text;
					dataRow5["PayeeID"] = text2;
					dataRow5["PayeeType"] = "E";
					dataRow5["IsARAP"] = true;
					dataRow5["Credit"] = num;
					dataRow5["Debit"] = DBNull.Value;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
					byte b = 1;
					object obj = null;
					string text4 = "";
					decimal num2 = default(decimal);
					string text5 = "";
					string text6 = "";
					string text7 = "";
					decimal num3 = default(decimal);
					string text8 = "";
					decimal num4 = default(decimal);
					string text9 = "";
					if (b == 1 || b == 2)
					{
						DataRow dataRow6 = employeeActivityData.Tables[2].Rows[0];
						text4 = dataRow6["TAccountID"].ToString();
						text5 = dataRow6["PayAccountID"].ToString();
						num2 = decimal.Parse(dataRow6["TicketAmount"].ToString());
						num3 = decimal.Parse(dataRow6["Basic"].ToString());
						text6 = dataRow6["PayItem"].ToString();
						text7 = dataRow6["Type"].ToString();
						text8 = employeeActivityData.Tables[1].Rows[0]["DeductionID"].ToString();
						num4 = decimal.Parse(employeeActivityData.Tables[1].Rows[0]["DeductionAmount"].ToString());
						obj = new Databases(base.DBConfig).GetFieldValue("Leave_Type", "AccountID", "LeaveTypeID", text7, sqlTransaction);
						if (obj != null)
						{
							text9 = obj.ToString();
						}
						if (text5 == null || text5.ToString() == "")
						{
							throw new CompanyException("Account is not set for payroll item '" + text6 + "'.", 1022);
						}
						value3 = ((!(text9 != "") || text9 == null) ? text5.ToString() : text9);
					}
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value3;
					dataRow5["PayeeID"] = text2;
					dataRow5["PayeeType"] = "A";
					dataRow5["Debit"] = num3;
					dataRow5["Credit"] = DBNull.Value;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
					if (num2 > 0m)
					{
						text6 = "Ticket Amount";
						if (text4 == null || text4.ToString() == "")
						{
							throw new CompanyException("Account is not set for payroll item '" + text6 + "'.", 1022);
						}
						text4 = text4.ToString();
						dataRow5 = gLData.JournalDetailsTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["JournalID"] = 0;
						dataRow5["AccountID"] = text4;
						dataRow5["PayeeID"] = text2;
						dataRow5["PayeeType"] = "A";
						dataRow5["Debit"] = num2;
						dataRow5["Credit"] = DBNull.Value;
						dataRow5.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow5);
					}
					if (num4 > 0m)
					{
						text6 = "Deduction Amount";
						object fieldValue = new Databases(base.DBConfig).GetFieldValue("PayrollItem", "AccountID", "PayrollItemID", text8, sqlTransaction);
						if (fieldValue == null || !(fieldValue.ToString() != ""))
						{
							throw new CompanyException("Account is not set for the deduction '" + text8 + "'.", 1021);
						}
						string value4 = fieldValue.ToString();
						dataRow5 = gLData.JournalDetailsTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["JournalID"] = 0;
						dataRow5["AccountID"] = value4;
						dataRow5["PayeeID"] = text2;
						dataRow5["PayeeType"] = "A";
						dataRow5["Credit"] = num4;
						dataRow5["Debit"] = DBNull.Value;
						dataRow5.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow5);
					}
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeActivityData GetEmployeeActivityByID(int activityID)
		{
			try
			{
				EmployeeActivityTypes employeeActivityTypes = (EmployeeActivityTypes)byte.Parse((new Databases(base.DBConfig).GetFieldValue("Employee_Activity", "ActivityType", "ActivityID", activityID, null) ?? throw new CompanyException("Activity type is not defined for this activity.")).ToString());
				EmployeeActivityData employeeActivityData = new EmployeeActivityData();
				string textCommand = "SELECT * FROM Employee_Activity WHERE ActivityID = " + activityID;
				FillDataSet(employeeActivityData, "Employee_Activity", textCommand);
				if (employeeActivityData == null || employeeActivityData.Tables.Count == 0 || employeeActivityData.Tables["Employee_Activity"].Rows.Count == 0)
				{
					return null;
				}
				string srcTable = "";
				switch (employeeActivityTypes)
				{
				case EmployeeActivityTypes.Transfer:
					textCommand = "SELECT ET.*,LocationName,DivisionName,DepartmentName,PositionName FROM Employee_Transfer ET\r\n                                    LEFT OUTER JOIN Location ON Location.LocationID=ET.TransferFromLocation\r\n                                    LEFT OUTER JOIN Division ON Division.DivisionID=ET.TransferFromDivision\r\n                                    LEFT OUTER JOIN Department ON Department.DepartmentID=ET.TransferFromDep\r\n                                    LEFT OUTER JOIN Position ON Position.PositionID=ET.FromPosition\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Transfer";
					break;
				case EmployeeActivityTypes.Termination:
					textCommand = "SELECT ET.* FROM Employee_Termination ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Termination";
					break;
				case EmployeeActivityTypes.Promotion:
					textCommand = "SELECT EP.*,GradeName,PositionName FROM Employee_Promotion EP\r\n                                    LEFT OUTER JOIN Employee_Grade EG ON EG.GradeID=EP.FromGrade\r\n                                    LEFT OUTER JOIN Position ON Position.PositionID=EP.FromPosition\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Promotion";
					break;
				case EmployeeActivityTypes.DisciplinaryAction:
					textCommand = "SELECT EP.* FROM Employee_DisciplinaryAction EP\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_DisciplinaryAction";
					break;
				case EmployeeActivityTypes.Rehire:
					textCommand = "SELECT ET.* FROM Employee_Rehire ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Rehire";
					break;
				case EmployeeActivityTypes.General:
					textCommand = "SELECT ET.* FROM Employee_GeneralActivity ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_GeneralActivity";
					break;
				case EmployeeActivityTypes.Leave:
					textCommand = " SELECT ELR.*,ISNULL(ELR.ActualLeaveDays,0) AS LeaveDays,LT.DeductionProportion,ELR.ResumptionDate AS ResumeDate, CASE WHEN EA.TransactionDate  IS NULL AND ELR.ResumptionDate IS NULL THEN 'False' ELSE 'True' END AS IsResumed,EA.ActivityID as ResumeActivityID FROM Employee_Leave_Request ELR\r\n                                  LEFT OUTER JOIN Employee_Resumption ER ON ER.LeaveID = ELR.ActivityID\r\n                                  LEFT OUTER JOIN Employee_Activity EA ON EA.ActivityID = ER.ActivityID\r\n                                  INNER JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID\r\n                                    WHERE ELR.ActivityID=" + activityID.ToString();
					srcTable = "Employee_Leave_Request";
					break;
				case EmployeeActivityTypes.LeaveEncashment:
					textCommand = "SELECT ET.* FROM Employee_Leave_Encashment ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Leave_Encashment";
					break;
				case EmployeeActivityTypes.LeavePayment:
					textCommand = "SELECT ET.* FROM Employee_Leave_Payment ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Leave_Payment";
					break;
				case EmployeeActivityTypes.Resumption:
					textCommand = "SELECT ER.*,ELR.ResumptionDate as EndDate FROM Employee_Resumption ER LEFT JOIN Employee_Leave_Request ELR ON ER.LeaveID=ELR.ActivityID\r\n                                    WHERE ER.ActivityID=" + activityID.ToString();
					srcTable = "Employee_Resumption";
					break;
				case EmployeeActivityTypes.Cancellation:
					textCommand = "SELECT EC.* FROM Employee_Cancellation EC\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Cancellation";
					break;
				case EmployeeActivityTypes.Absconding:
					textCommand = "SELECT EA.* FROM Employee_Absconding EA\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Absconding";
					break;
				case EmployeeActivityTypes.PassportControl:
					textCommand = " SELECT ELR.*  FROM Employee_Passport_Control ELR\r\n                                    WHERE ELR.ActivityID=" + activityID.ToString();
					srcTable = "Employee_Passport_Control";
					break;
				}
				FillDataSet(employeeActivityData, srcTable, textCommand);
				return employeeActivityData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeGeneralActivityToPrint(string sysDocID, string voucherID)
		{
			EmployeeActivityData employeeActivityData = new EmployeeActivityData();
			string textCommand = "SELECT FirstName+' '+LastName AS [Name] ,EA.*,LocationName,DivisionName,DepartmentName,PositionName,E.Joiningdate,LabourID,SponsorName,\r\n                                (SELECT DocumentNumber FROM Employee_Document ED WHERE ED.EmployeeID=E.EmployeeID AND ED.DocumentTypeID='PASSPORT') AS Passport \r\n                                from Employee_GeneralActivity EA \r\n                                LEFT JOIN Employee E ON EA.EmployeeID=E.EmployeeID\r\n                                LEFT OUTER JOIN Location ON Location.LocationID=E.LocationID\r\n                                LEFT OUTER JOIN Division ON Division.DivisionID=E.DivisionID\r\n                                LEFT OUTER JOIN Department ON Department.DepartmentID=E.DepartmentID \r\n                                LEFT OUTER JOIN Position ON Position.PositionID=E.PositionID\r\n                                LEFT OUTER JOIN Sponsor S ON S.SponsorID=E.SponsorID\r\n                                WHERE EA.SysDocID = '" + sysDocID + "' AND EA.VoucherID='" + voucherID + "'";
			FillDataSet(employeeActivityData, "Employee_GeneralActivity", textCommand);
			return employeeActivityData;
		}

		public DataSet GetEmployeeActivityToPrint(int activityID)
		{
			try
			{
				EmployeeActivityTypes employeeActivityTypes = (EmployeeActivityTypes)byte.Parse((new Databases(base.DBConfig).GetFieldValue("Employee_Activity", "ActivityType", "ActivityID", activityID, null) ?? throw new CompanyException("Activity type is not defined for this activity.")).ToString());
				EmployeeActivityData employeeActivityData = new EmployeeActivityData();
				string textCommand = "SELECT FirstName+' '+LastName AS [Name] ,EA.*,LocationName,DivisionName,DepartmentName,PositionName,E.Joiningdate,LabourID,SponsorName,\r\n                                (SELECT DocumentNumber FROM Employee_Document ED WHERE ED.EmployeeID=E.EmployeeID AND ED.DocumentTypeID='PASSPORT') AS Passport ,TypeName as ContractType,\r\n                                Phone1,Phone2,Mobile,Email,PostalCode,Fax,Address1,Address2,Address3,AddressPrintFormat,City,State,ResumedDate [LastResumedDate],Photo,\r\n                                (SELECT Sum(Amount) FROM Employee_PayrollItem_Detail EPD  INNER JOIN PayrollItem PI ON PI.PayrollItemID=EPD.PayrollItemID  WHERE PI.PayCodeType = '1' \r\n\t\t\t\t\t\t\t\t  AND EPD.EmployeeID = E.EmployeeID Group By EPD.EmployeeID) [BasicSalary] \r\n                                from Employee_Activity EA \r\n                                LEFT JOIN Employee E ON EA.EmployeeID=E.EmployeeID\r\n                                LEFT OUTER JOIN Location ON Location.LocationID=E.LocationID\r\n                                LEFT OUTER JOIN Division ON Division.DivisionID=E.DivisionID\r\n                                LEFT OUTER JOIN Department ON Department.DepartmentID=E.DepartmentID \r\n                                LEFT OUTER JOIN Position ON Position.PositionID=E.PositionID\r\n                                LEFT OUTER JOIN Sponsor S ON S.SponsorID=E.SponsorID\r\n                                LEFT OUTER JOIN  Employee_Type  ET ON  E.ContractType=ET.TypeID\r\n                                LEFT OUTER JOIN Employee_Address EAD ON EAD.EmployeeID=E.EmployeeID AND AddressID='PRIMARY'\r\n                                WHERE ActivityID = " + activityID;
				FillDataSet(employeeActivityData, "Employee_Activity", textCommand);
				if (employeeActivityData == null || employeeActivityData.Tables.Count == 0 || employeeActivityData.Tables["Employee_Activity"].Rows.Count == 0)
				{
					return null;
				}
				string srcTable = "";
				switch (employeeActivityTypes)
				{
				case EmployeeActivityTypes.Transfer:
					textCommand = "SELECT ET.*,LocationName,DivisionName,DepartmentName FROM Employee_Transfer ET\r\n                                    LEFT OUTER JOIN Location ON Location.LocationID=ET.TransferFromLocation\r\n                                    LEFT OUTER JOIN Division ON Division.DivisionID=ET.TransferFromDivision\r\n                                    LEFT OUTER JOIN Department ON Department.DepartmentID=ET.TransferFromDep\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Transfer";
					break;
				case EmployeeActivityTypes.Termination:
					textCommand = "SELECT ET.* FROM Employee_Termination ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Termination";
					break;
				case EmployeeActivityTypes.Promotion:
					textCommand = "SELECT EP.*,GradeName,PositionName FROM Employee_Promotion EP\r\n                                    LEFT OUTER JOIN Employee_Grade EG ON EG.GradeID=EP.FromGrade\r\n                                    LEFT OUTER JOIN Position ON Position.PositionID=EP.FromPosition\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Promotion";
					break;
				case EmployeeActivityTypes.DisciplinaryAction:
					textCommand = "SELECT EP.* FROM Employee_DisciplinaryAction EP\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_DisciplinaryAction";
					break;
				case EmployeeActivityTypes.Rehire:
					textCommand = "SELECT ET.* FROM Employee_Rehire ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Rehire";
					break;
				case EmployeeActivityTypes.General:
					textCommand = "SELECT ET.* FROM Employee_GeneralActivity ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_GeneralActivity";
					break;
				case EmployeeActivityTypes.Leave:
					textCommand = "SELECT ET.*,LT.LeaveTypeName,((SELECT SUM(Days) FROM Employee_Leave_Process ELP WHERE ELP.EmployeeID=EA.EmployeeID \r\n                           AND YEAR(ELP.FromDate)=YEAR(ET.StartDate))-ISNULL((SELECT SUM(ActualLeaveDays) AS [Taken] From Employee_Leave_Request ELR INNER JOIN  Employee_Activity EA1\r\n                           ON ELR.ActivityID=EA1.ActivityID AND EA1.EmployeeID=EA.EmployeeID AND ELR.IsApproved<>0 AND YEAR(ELR.StartDate)=YEAR(GETDATE())),0)) AS [Days_Avail],\r\n                           (SELECT TOP 1 Days FROM Employee_Leave_Process ELP WHERE ELP.EmployeeID=EA.EmployeeID AND YEAR(ELP.FromDate)=YEAR(ET.StartDate) AND Days > 20) AS [Days_Elig]\r\n                           FROM Employee_Leave_Request ET LEFT JOIN Leave_Type LT ON ET.LeaveTypeID=LT.LeaveTypeID\r\n                           INNER JOIN Employee_Activity EA ON EA.ActivityID=ET.ActivityID\r\n                            WHERE ET.ActivityID=" + activityID.ToString();
					srcTable = "Employee_Leave_Request";
					break;
				case EmployeeActivityTypes.LeaveEncashment:
					textCommand = "SELECT ET.* FROM Employee_Leave_Encashment ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Leave_Encashment";
					break;
				case EmployeeActivityTypes.LeavePayment:
					textCommand = "SELECT ET.* FROM Employee_Leave_Payment ET\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Leave_Payment";
					break;
				case EmployeeActivityTypes.Resumption:
					textCommand = "SELECT ER.* FROM Employee_Resumption ER\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Resumption";
					break;
				case EmployeeActivityTypes.PassportControl:
					textCommand = "SELECT ER.*,RT.ReleaseTypeName FROM Employee_Passport_Control ER  LEFT JOIN Release_Type RT ON ER.ReasonID=RT.ReleaseTypeID\r\n\r\n                            WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Passport_Control";
					break;
				case EmployeeActivityTypes.Absconding:
					textCommand = "SELECT EA.* FROM Employee_Absconding EA\r\n                                    WHERE ActivityID=" + activityID.ToString();
					srcTable = "Employee_Absconding";
					break;
				}
				FillDataSet(employeeActivityData, srcTable, textCommand);
				DataSet dataSet = new DataSet();
				textCommand = "select PID.employeeid, PayrollItemID, Amount  from Employee_PayrollItem_Detail PID \r\n                         LEFT OUTER JOIN Employee_Activity EA ON EA. EmployeeID =PID.EmployeeID where PayType =1 AND ActivityID=" + activityID.ToString();
				FillDataSet(dataSet, "PayrollDetails", textCommand);
				employeeActivityData.Merge(dataSet);
				return employeeActivityData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteActivity(string activityID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Activity WHERE ActivityID = " + activityID;
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee Activity", activityID, activityType, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteActivity(string activityID, EmployeeActivityTypes actType)
		{
			bool flag = true;
			string text = "";
			SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
			try
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee_Resumption", "LeaveID", "ActivityID", activityID, sqlTransaction);
				object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Employee_Activity", "EmployeeID", "ActivityID", activityID, sqlTransaction);
				object obj = null;
				string commandText = "DELETE FROM Employee_Activity WHERE ActivityID = " + activityID;
				flag &= Delete(commandText, sqlTransaction);
				if (actType == EmployeeActivityTypes.LeaveEncashment)
				{
					commandText = "DELETE FROM Employee_Leave_Encashment\r\n                                    WHERE ActivityID = " + activityID.ToString();
					flag &= Delete(commandText, sqlTransaction);
				}
				if (actType == EmployeeActivityTypes.LeavePayment)
				{
					new Databases(base.DBConfig).GetFieldValue("Employee_Leave_Payment", "LeavePaymentID", "ActivityID", activityID, sqlTransaction);
					object fieldValue3 = new Databases(base.DBConfig).GetFieldValue("Employee_Leave_Payment", "LeaveApplicationNo", "ActivityID", activityID, sqlTransaction);
					object fieldValue4 = new Databases(base.DBConfig).GetFieldValue("Employee_Leave_Payment", "SysDocID", "ActivityID", activityID, sqlTransaction);
					object fieldValue5 = new Databases(base.DBConfig).GetFieldValue("Employee_Leave_Payment", "VoucherID", "ActivityID", activityID, sqlTransaction);
					commandText = "DELETE FROM Employee_Leave_Payment\r\n                                    WHERE ActivityID = " + activityID.ToString();
					flag &= Delete(commandText, sqlTransaction);
					commandText = "UPDATE Employee_Leave_Request SET\r\n                            IsPaid= 'false'                        \r\n                            WHERE DocNumber = '" + fieldValue3.ToString() + "'";
					flag &= Update(commandText, sqlTransaction);
					flag &= new Journal(base.DBConfig).DeleteJournal(fieldValue4.ToString(), fieldValue5.ToString(), sqlTransaction);
				}
				if (actType == EmployeeActivityTypes.Cancellation)
				{
					string str = fieldValue2 as string;
					commandText = "DELETE FROM Employee_Cancellation\r\n                                    WHERE ActivityID = " + activityID.ToString();
					flag &= Delete(commandText, sqlTransaction);
					text = "UPDATE Employee SET\r\n                            IsTerminated= NULL, IsCancelled = NULL,\r\n                            TerminationDate=NULL, TerminationID = NULL WHERE EmployeeID = '" + str + "'";
					flag &= Update(text, sqlTransaction);
				}
				if (actType == EmployeeActivityTypes.Absconding)
				{
					commandText = "DELETE FROM Employee_Absconding\r\n                                    WHERE ActivityID = " + activityID.ToString();
					flag &= Delete(commandText, sqlTransaction);
				}
				if (actType == EmployeeActivityTypes.Resumption)
				{
					text = "select MAX( elr.ResumptionDate) as LastResumedate,ea.EmployeeID,elr.LeaveTypeID from Employee_Leave_Request elr left join Employee_Activity ea on elr.ActivityID=ea.ActivityID AND elr.ActivityID<>'" + fieldValue.ToString() + "'\r\n                            inner join  Leave_Type LT on LT.LeaveTypeID = elr.LeaveTypeID and LT.IsAnnual = 1 WHERE ea.EmployeeID='" + fieldValue2.ToString() + "' group by ea.EmployeeID,elr.LeaveTypeID";
					obj = ExecuteScalar(text, sqlTransaction);
					commandText = "DELETE FROM Employee_Resumption\r\n                                    WHERE ActivityID = '" + activityID.ToString() + "'";
					flag &= Delete(commandText, sqlTransaction);
					text = "UPDATE Employee_Leave_Request SET                            \r\n                            ResumptionDate=NULL WHERE ActivityID = '" + fieldValue.ToString() + "'";
					flag &= Update(text, sqlTransaction);
					text = "SELECT ISNULL(IsAnnual,0) as IsAnnual FROM Leave_Type LT INNER JOIN Employee_Leave_Request ELR ON LT.LeaveTypeID=ELR.LeaveTypeID WHERE ELR.ActivityID=" + fieldValue.ToString();
					object obj2 = ExecuteScalar(text, sqlTransaction);
					DateTime dateTime = default(DateTime);
					string text2 = "";
					if (obj != null)
					{
						dateTime = DateTime.Parse(obj.ToString());
						text2 = CommonLib.ToSqlDateTimeString(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0));
					}
					if (bool.Parse(obj2.ToString()))
					{
						text = "UPDATE Employee SET                           \r\n                            ResumedDate='" + text2 + "' WHERE EmployeeID = '" + fieldValue2.ToString() + "'";
						flag &= Update(text, sqlTransaction);
					}
				}
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee Activity", activityID, activityType, sqlTransaction);
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

		public bool DeleteEmployeeGeneralActivity(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM Employee_GeneralActivity WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag = (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee General Activity", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetUnapprovedLeaveRequests()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT ELR.DocNumber,ELR.ActivityID,LeaveTypeID [Type],TransactionDate AS [App Date],EA.EmployeeID AS [Employee ID],\r\n                                FirstName + ' ' + LastName AS [Employee Name],StartDate [Start Date],EndDate [End Date],\r\n                                CASE WHEN ELR.ActualLeaveDays=0 THEN  DATEDIFF(day,ELR.StartDate,ELR.EndDate)+1 ELSE ISNULL(ELR.ActualLeaveDays,DATEDIFF(day,ELR.StartDate,ELR.EndDate)+1) END AS LeaveDays,IsApproved,\r\n                                 IsClosed,IsVoid FROM Employee_Leave_Request ELR\r\n                                INNER JOIN Employee_Activity EA ON EA.ActivityID=ELR.ActivityID\r\n                                INNER JOIN Employee On EA.EmployeeID=Employee.EmployeeID\r\n                                WHERE ApproveDate IS NULL AND ISVOID IS NULL ORDER BY TransactionDate";
				FillDataSet(dataSet, "Employee_Leave_Request", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeLeaveHistory(string employeeId)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT TOP 20 ELR.ActivityID,LeaveTypeID [Type],TransactionDate AS [App Date],EA.EmployeeID AS [Employee ID],\r\n                                FirstName + ' ' + LastName AS [Employee Name],StartDate [Start Date],EndDate [End Date],DATEDIFF (d ,StartDate ,EndDate )+1 as Leavedays,IsApproved,\r\n                                 IsClosed,IsVoid,ELR.ApprovalRemarks FROM Employee_Leave_Request ELR\r\n                                INNER JOIN Employee_Activity EA ON EA.ActivityID=ELR.ActivityID\r\n                                INNER JOIN Employee On EA.EmployeeID=Employee.EmployeeID\r\n                                WHERE ApproveDate IS NOT NULL\r\n                                AND EA.EmployeeID = '" + employeeId + "' ORDER BY TransactionDate";
				FillDataSet(dataSet, "EmployeeLeaveHistory", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeAppraisalHistory(string employeeId)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT  EA.SysDocID[Doc ID], EA.VoucherID AS [Doc Number],EA.EmployeeID[Employee ID], EA.Reason, EA.TransactionDate,EA.Rating, Employee.FirstName+ Employee.MiddleName+Employee.LastName AS [Employe Name],  EA.Note from  Employee_GeneralActivity EA\r\n                                INNER JOIN Employee On EA.EmployeeID=Employee.EmployeeID\r\n                                AND EA.EmployeeID = '" + employeeId + "' ORDER BY EA.TransactionDate";
				FillDataSet(dataSet, "EmployeeAppraisalHistory", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAllEmployeeApprovedLeaves(string employeeID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT Distinct ELR.ActivityID,CAST('' as bit) as C, DocNumber [Doc Number], LT.LeaveTypeID [Type], TransactionDate AS [App Date], EA.EmployeeID AS [EmployeeID],Employee.JoiningDate,\r\n                                FirstName + ' ' + LastName AS [Employee Name], StartDate [Start Date], EndDate [End Date], IsApproved,\r\n                                IsClosed, IsVoid,IsNull(PaidDays,0) as PaidDays , (SELECT sum(CASE WHEN EPD.PayType=1 Then iSNULL(Amount,0) When EPD.PayType=2 Then 0-ISNULL(Amount,0) ELSe 0 END) AS Amount FROM Employee_PayrollItem_Detail EPD  INNER JOIN PayrollItem PI ON PI.PayrollItemID=EPD.PayrollItemID  WHERE PI.InLeaveSalary = '1'  AND EPD.EmployeeID = Employee.EmployeeID Group By EPD.EmployeeID) [Basic], \r\n                                (SELECT Top 1 EPD.PayrollItemID FROM Employee_PayrollItem_Detail EPD  INNER JOIN PayrollItem PI ON PI.PayrollItemID=EPD.PayrollItemID  WHERE PI.InLeaveSalary = '1'  AND EPD.EmployeeID = Employee.EmployeeID) [PayItem] ,\r\n\t\t\t\t\t\t\t\t(SELECT Top 1 AccountID FROM Employee_PayrollItem_Detail EPD  INNER JOIN PayrollItem PI ON PI.PayrollItemID=EPD.PayrollItemID  WHERE PI.InLeaveSalary = '1'  AND EPD.EmployeeID = Employee.EmployeeID) [PayAccountID] ,TicketAmount ,D.AccountID AS [TAccountID]\r\n\t\t\t\t\t\t\t\tFROM Employee_Leave_Request ELR\r\n                                INNER JOIN Employee_Activity EA ON EA.ActivityID=ELR.ActivityID\r\n                                INNER JOIN Employee On EA.EmployeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t\tINNER JOIN Leave_Type LT ON  LT.LeaveTypeID=ELR.LeaveTypeID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Destination D on Employee.DestinationID=D.DestinationID\r\n                                LEFT JOIN Opening_Balance_Leave_Detail OPLD on EA.EmployeeID=OPLD.EmployeeID\r\n                                WHERE LT.IsAnnual=1 AND\r\n\t\t\t\t\t\t\t\t ISNULL(IsApproved, 0) = 1 AND ISNULL(IsPaid,0)=0 ";
				if (employeeID != "")
				{
					str = str + " AND EA.EmployeeID='" + employeeID + "'";
				}
				str += " ORDER BY TransactionDate";
				FillDataSet(dataSet, "EmployeeApprovedLeaves", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeesOnLeave()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT EmployeeID AS [Employee ID],FirstName + ' ' + LastName AS [Employee Name],AnnualLeaveDate AS [Leave Date],\r\n                                DateDiff(Day,AnnualLeaveDate,GetDate()) Days\r\n                                FROM Employee WHERE OnVacation='True'";
				FillDataSet(dataSet, "Employee", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeLeaveByID(string activityID)
		{
			try
			{
				if (activityID == "")
				{
					return null;
				}
				string textCommand = " SELECT ELR.ActivityID AS LeaveID, EMP.EmployeeID,EMP.FirstName + ' ' + EMP.LastName AS EmployeeName, DocNumber,LT.LeaveTypeName AS LeaveType,StartDate,EndDate,EA2.TransactionDate AS ResumptionDate,\r\n                                     DEP.DepartmentName,WL.WorkLocationName  AS LocationName,POS.PositionName,DIV.DivisionName,EMP.IsTerminated,ISNULL(ELR.ActualLeaveDays,0) as ActualLeaveDays\r\n                                     FROM Employee_Leave_Request ELR INNER JOIN Employee_Activity EA ON ELR.ActivityID = EA.ActivityID\r\n                                     INNER JOIN Leave_Type LT ON LT.LeaveTypeID = ELR.LeaveTypeID\r\n                                     INNER JOIN Employee EMP ON EMP.EmployeeID = EA.EmployeeID LEFT OUTER JOIN Employee_Resumption RES ON RES.LeaveID = ELR.ActivityID\r\n                                     LEFT OUTER JOIN Employee_Activity EA2 ON EA2.ActivityID = RES.ActivityID \r\n                                     LEFT OUTER JOIN Department DEP ON Dep.DepartmentID = EMP.DepartmentID\r\n                                     LEFT OUTER JOIN Position POS ON POS.PositionID = EMP.PositionID\r\n                                     LEFT OUTER JOIN Division DIV ON DIV.DivisionID = EMP.DivisionID\r\n                                     LEFT OUTER JOIN Work_Location WL ON WL.WorkLocationID = EMP.LocationID\r\n                                     WHERE ELR.ActivityID =  " + activityID;
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Leave", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeesLeavesToResume()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT ELR.ActivityID AS LeaveID,EMP.EmployeeID,EMP.FirstName + ' ' + EMP.LastName AS EmployeeName, DocNumber,LT.LeaveTypeName AS LeaveType,StartDate,EndDate,ISNULL(ELR.ResumptionDate,EA2.TransactionDate) AS ResumptionDate, EMP.LocationID+'-'+WorkLocationName AS Location,\r\n                                EMP.DepartmentID+'-'+DepartmentName AS Department, EMP.PositionID+'-'+p.PositionName AS Designation\r\n                                 FROM Employee_Leave_Request ELR INNER JOIN Employee_Activity EA ON ELR.ActivityID = EA.ActivityID\r\n                                 INNER JOIN Leave_Type LT ON LT.LeaveTypeID = ELR.LeaveTypeID\r\n                                 INNER JOIN Employee EMP ON EMP.EmployeeID = EA.EmployeeID LEFT OUTER JOIN Employee_Resumption RES ON RES.LeaveID = ELR.ActivityID\r\n                                 LEFT OUTER JOIN Employee_Activity EA2 ON EA2.ActivityID = RES.ActivityID \r\n\t\t\t\t\t\t\t\t LEFT JOIN Work_Location L ON EMP.LocationID=L.WorkLocationID\r\n\t\t\t\t\t\t\t\t LEFT JOIN Department D ON EMP.DepartmentID=D.DepartmentID\r\n\t\t\t\t\t\t\t\t LEFT JOIN Position P ON EMP.PositionID=p.PositionID\r\n                                 WHERE ISNULL(ELR.IsApproved,'FALSE')='TRUE' AND EA2.TransactionDate IS NULL AND ELR.ResumptionDate IS NULL";
				FillDataSet(dataSet, "Employee", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public string EncashmentAmount(string employeeID)
		{
			string exp = "SELECT SUM(Amount)/30 AS Salary FROM Employee_PayrollItem_Detail EPD LEFT JOIN PayrollItem PI ON EPD.PayrollItemID=PI.PayrollItemID WHERE PI.InLeaveSalary=1 AND EmployeeID='" + employeeID.ToString() + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return "";
		}

		public bool ApproveRejectLeaveRequest(int activityID, string employeeID, DateTime startDate, DateTime endDate, bool isApprove, string approvalRemarks)
		{
			bool flag = true;
			string text = CommonLib.ToSqlDateTimeString(new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0));
			string text2 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 11, 59, 59));
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text3 = CommonLib.ToSqlDateTimeString(startDate);
				string exp = "SELECT IsApproved FROM Employee_Leave_Request WHERE ActivityID=" + activityID.ToString();
				ExecuteScalar(exp, sqlTransaction);
				exp = "UPDATE Employee_Leave_Request SET   ApprovalRemarks ='" + approvalRemarks + "', IsApproved= '" + isApprove.ToString() + "', StartDate ='" + text + "', EndDate='" + text2 + "',\r\n                        ApprovedBy='" + base.DBConfig.UserID + "',ApproveDate='" + text3 + "'\r\n                        WHERE ActivityID=" + activityID.ToString();
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				exp = "SELECT ISNULL(IsAnnual,0) as IsAnnual FROM Leave_Type LT INNER JOIN Employee_Leave_Request ELR ON LT.LeaveTypeID=ELR.LeaveTypeID WHERE ELR.ActivityID=" + activityID.ToString();
				if (bool.Parse(ExecuteScalar(exp, sqlTransaction).ToString()))
				{
					exp = "UPDATE Employee SET OnVacation= 'True',\r\n                        AnnualLeaveDate ='" + text3 + "'\r\n                        WHERE EmployeeID='" + employeeID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				DateTime dateTime = startDate;
				DateTime dateTime2 = endDate;
				exp = "SELECT ISNULL(AutoResumptionDays,0) FROM Company ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				endDate = dateTime.AddDays(1.0);
				text2 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0));
				int num = int.Parse(dateTime2.Subtract(startDate).Days.ToString()) + 1;
				if (int.Parse(obj.ToString()) < num)
				{
					return flag;
				}
				if (int.Parse(obj.ToString()) == 0)
				{
					return flag;
				}
				exp = "UPDATE Employee_Leave_Request SET ResumptionDate ='" + text2 + "'\r\n                        WHERE ActivityID=" + activityID.ToString();
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
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

		public DataSet GetEmployeeLeaveList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "   SELECT  CONVERT(varchar(10), ELR.ActivityID) AS TransactionID ,ELR.DocNumber,E.EmployeeID,E.FirstName,ELR.LeaveTypeID,ELR.StartDate ,ELR.EndDate,CASE WHEN ELR.ActualLeaveDays=0 THEN  DATEDIFF(day,ELR.StartDate,ELR.EndDate)+1 ELSE ISNULL(ELR.ActualLeaveDays,DATEDIFF(day,ELR.StartDate,ELR.EndDate)+1) END AS LeaveDays  FROM Employee_Leave_Request ELR   \r\n    \t\t\t\t\t\t      LEFT  JOIN Employee_Resumption ER ON ER.LeaveID = ELR.ActivityID\r\n\t\t\t\t\t\t\t\t  LEFT JOIN  Employee_Activity EA ON EA.ActivityID = ELR.ActivityID\r\n                                  LEFT JOIN Employee E ON E.EmployeeID=EA.EmployeeID  \r\n                                  INNER JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID \r\n                                  WHERE  ISNULL(isvoid,0) =0  \r\n                                  order by  ELR.ActivityID";
			FillDataSet(dataSet, "Employee_Leave_Request", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeLeaveList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "   SELECT  CONVERT(varchar(10), ELR.ActivityID) AS TransactionID ,ELR.DocNumber AS [Doc Number],E.EmployeeID,E.FirstName,ELR.LeaveTypeID,ELR.StartDate ,ELR.EndDate,CASE WHEN ELR.ActualLeaveDays=0 THEN  DATEDIFF(day,ELR.StartDate,ELR.EndDate)+1 ELSE ISNULL(ELR.ActualLeaveDays,DATEDIFF(day,ELR.StartDate,ELR.EndDate)+1) END AS LeaveDays  FROM Employee_Leave_Request ELR   \r\n    \t\t\t\t\t\t      LEFT  JOIN Employee_Resumption ER ON ER.LeaveID = ELR.ActivityID\r\n\t\t\t\t\t\t\t\t  LEFT JOIN  Employee_Activity EA ON EA.ActivityID = ELR.ActivityID\r\n                                  LEFT JOIN Employee E ON E.EmployeeID=EA.EmployeeID  \r\n                                  INNER JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID \r\n                                  WHERE 1=1  \r\n                                 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ELR.StartDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " order by  ELR.ActivityID";
			FillDataSet(dataSet, "Employee_Leave_Request", text3);
			return dataSet;
		}

		public DataSet GetEmployeeLeaveList(string employeeID)
		{
			DataSet dataSet = new DataSet();
			string str = "   SELECT  CONVERT(varchar(10), ELR.ActivityID) AS TransactionID ,ELR.DocNumber AS [Doc Number],E.EmployeeID,E.FirstName,\r\n                                   LeaveTypeName,ELR.LeaveTypeID,ELR.StartDate ,ELR.EndDate,\r\n                                   CASE WHEN ELR.ActualLeaveDays=0 THEN  DATEDIFF(day,ELR.StartDate,ELR.EndDate)+1 ELSE ISNULL(ELR.ActualLeaveDays,DATEDIFF(day,ELR.StartDate,ELR.EndDate)+1) END AS LeaveDays  \r\n                                , ELR.ResumptionDate,DATEDIFF( day,ELR.EndDate, ELR.ResumptionDate) as ExcessDays, ELR.ActualLeaveDays\r\n                                FROM Employee_Leave_Request ELR   \r\n                                LEFT  JOIN Employee_Resumption ER ON ER.LeaveID = ELR.ActivityID\r\n                                LEFT JOIN  Employee_Activity EA ON EA.ActivityID = ELR.ActivityID\r\n                                LEFT JOIN Employee E ON E.EmployeeID=EA.EmployeeID  \r\n                                INNER JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID \r\n                                  WHERE 1=1  and EA.EmployeeID='" + employeeID + "'\r\n                                 ";
			str += " order by  ELR.ActivityID";
			FillDataSet(dataSet, "Employee_Leave_Request", str);
			return dataSet;
		}

		public DataSet GetEmployeeLeavePaymentList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = " Select SysDocID,VoucherID,EA.TransactionDate,E.FirstName + ' ' + E.LastName AS [Employee Name],Amount,ELP.TicketAmount [Ticket Amount],SalaryAmount [Salary Amount],DeductionAmount [Deduction Amount],Total,EA.Note   FROM Employee_Leave_Payment ELP   \r\n    \t\t\t\t\t\t     \r\n\t\t\t\t\t\t\t\t  LEFT JOIN  Employee_Activity EA ON EA.ActivityID = ELP.ActivityID\r\n                                  LEFT JOIN Employee E ON E.EmployeeID=EA.EmployeeID  \r\n                                 \r\n                                  WHERE 1=1  \r\n                                 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND EA.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " order by SysDocID,VoucherID";
			FillDataSet(dataSet, "Employee_Leave_Request", text3);
			return dataSet;
		}

		public DataSet GetEmployeeAbscondingEntryList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "  select CONVERT(varchar(10), AC.ActivityID) AS [Code], AC.EmployeeID, E.FirstName+ '.'+ E.LastName AS [Employee Name],EA.AbscondingReferenceNo, EA.MBReferenceNo , EA.AbscondingRegDateMOL,\r\n                                EA.LastWorkingDate, EA.RealAbscondingDate from Employee_Absconding EA LEFT OUTER JOIN Employee_Activity AC ON AC.ActivityID=EA.ActivityID LEFT OUTER JOIN Employee E ON AC.EmployeeID=E.EmployeeID\r\n                                 \r\n                                  WHERE 1=1  \r\n                                 ";
			FillDataSet(dataSet, "Employee_Absconding", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeePassportControlList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "   SELECT  CONVERT(varchar(10), ELR.ActivityID) AS TransactionID ,ELR.DocNumber,E.EmployeeID,E.FirstName,ELR.PPReleaseDate ,ELR.PPReturnDate  FROM Employee_Passport_Control ELR \r\n    \t\t\t\t\t\t     LEFT JOIN  Employee_Activity EA ON EA.ActivityID = ELR.ActivityID\r\n                                  LEFT JOIN Employee E ON E.EmployeeID=EA.EmployeeID                                  \r\n                                  WHERE  ISNULL(isvoid,0) =0  \r\n                                  order by  ELR.ActivityID";
			FillDataSet(dataSet, "Employee_Passport_Control", textCommand);
			return dataSet;
		}

		public DataSet GetActivityList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ACT.SysDocID [Doc ID], ACT.VoucherID [Number],\r\n                               ActivityName [Activity Name], \r\n                        (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN 'Lead' \tWHEN 2 THEN 'Customer' WHEN 3 THEN 'Opportunity'\r\n\t\t\t\t\t\tELSE '' END) AS [Account Type], (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN Lead.LeadName\r\n\t\t\t\t\t\tWHEN 2 THEN CUS.CustomerName WHEN 3 THEN OP.OpportunityName\r\n\t\t\t\t\t\tELSE '' END) AS [Account Name],Con.FirstName + ' ' + Con.LastName AS Contact,\r\n                                CASE ActivityType WHEN 0 THEN 'Call' \r\n                                WHEN 1 THEN 'Email' WHEN 2 THEN 'Task'  WHEN 3 THEN 'Fax' \r\n                                WHEN 4 THEN 'Letter' WHEN 5 THEN 'Appointment' WHEN 6 THEN 'Compaign' \r\n                                WHEN 7 THEN 'Service' WHEN 1000 THEN 'Custom' END AS [Activity Type],ACT.OwnerID [Performed By],  ActivityDateTime [Date], ACT.Note\r\n                           FROM Activity ACT LEFT OUTER JOIN Lead ON Lead.LeadID = Act.RelatedID\r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CustomerID = Act.RelatedID\r\n                            LEFT OUTER JOIN Opportunity OP ON OP.OpportunityID = Act.RelatedID\r\n                            LEFT OUTER JOIN Contact CON ON CON.ContactID = ACT.ContactID\r\n                                --Checking user access right for the doc id\r\n                                WHERE ACT.RelatedType='" + 2 + "' AND  (ACT.SysDocID NOT IN (SELECT SysDocID FROM System_Doc_Entity_Link)\r\n\r\n\r\n                                OR Act.SysDocID IN (SELECT SysDocID FROM System_Doc_Entity_Link SDL WHERE SDL.EntityType = 5 AND SDL.EntityID = '" + base.DBConfig.UserID + "'))";
			FillDataSet(dataSet, "Activity", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeLeaveResumptionDetailList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "  SELECT  CONVERT(varchar(10), ER.ActivityID) AS TransactionID ,E.EmployeeID,ELR.DocNumber,\r\n                                 E.FirstName,E.LocationID+'-'+WorkLocationName AS Location,E.DepartmentID+'-'+ DepartmentName AS Department, E.PositionID+'-'+PositionName AS Designation,   ELR.StartDate ,ELR.EndDate,ISNULL(ELR.ResumptionDate,EA2.TransactionDate) as ResumptionDate   FROM  Employee_Resumption ER   \r\n                                LEFT  JOIN Employee_Leave_Request ELR ON ELR.ActivityID=ER.LeaveID \r\n                                LEFT JOIN  Employee_Activity EA ON EA.ActivityID = ELR.ActivityID\r\n                                LEFT JOIN Employee E ON E.EmployeeID=EA.EmployeeID  \r\n                                LEFT OUTER JOIN Employee_Activity EA2 ON EA2.ActivityID = ER.ActivityID \r\n                                INNER JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID \r\n                                LEFT JOIN Work_Location On E.LocationID=Work_Location.WorkLocationID\r\n                                LEFT JOIN Department ON E.DepartmentID=Department.DepartmentID\r\n                                LEFT JOIN Position ON E.PositionID=Position.PositionID\r\n                                order by  ER.ActivityID";
			FillDataSet(dataSet, "Employee_Resumption", textCommand);
			return dataSet;
		}

		public bool IsPassportAllocated(string employeeId)
		{
			bool flag = true;
			try
			{
				string exp = "SELECT count(EmployeeID) FROM Employee_Activity EA \r\n\t                    INNER JOIN Employee_Passport_Control EP ON EA.ActivityID = EP.ActivityID                    \r\n                        WHERE EmployeeID = '" + employeeId + "' AND ISNULL(PPReturnDate,'') =''";
				object obj = ExecuteScalar(exp);
				if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
				{
					return false;
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeActivityData GetEmployeeGeneralActivityByID(string sysDocID, string voucherID)
		{
			try
			{
				EmployeeActivityData employeeActivityData = new EmployeeActivityData();
				string textCommand = "SELECT * FROM Employee_GeneralActivity WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(employeeActivityData, "Employee_GeneralActivity", textCommand);
				return employeeActivityData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGeneralActivityList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Doc Number], TransactionDate AS [Date],Subject,EG.EmployeeID, FirstName+' '+LastName AS [Name]\r\n                            FROM Employee_GeneralActivity EG\r\n                             LEFT JOIN Employee E ON EG.EmployeeID=E.EmployeeID\r\n                            WHERE  TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Employee_GeneralActivity", str);
			return dataSet;
		}

		public DataSet AllowDelete(string empID, DateTime StartDate)
		{
			string text = CommonLib.ToSqlDateTimeString(StartDate);
			string textCommand = "SELECT ELR.ActivityID AS LeaveID,EMP.EmployeeID,EMP.FirstName + ' ' + EMP.LastName AS EmployeeName, DocNumber,LT.LeaveTypeName AS LeaveType,StartDate,EndDate,ISNULL(ELR.ResumptionDate,EA2.TransactionDate) AS ResumptionDate\r\n                                 FROM Employee_Leave_Request ELR INNER JOIN Employee_Activity EA ON ELR.ActivityID = EA.ActivityID\r\n                                 INNER JOIN Leave_Type LT ON LT.LeaveTypeID = ELR.LeaveTypeID\r\n                                 INNER JOIN Employee EMP ON EMP.EmployeeID = EA.EmployeeID LEFT OUTER JOIN Employee_Resumption RES ON RES.LeaveID = ELR.ActivityID\r\n                                 LEFT OUTER JOIN Employee_Activity EA2 ON EA2.ActivityID = RES.ActivityID \r\n                                 WHERE ISNULL(ELR.IsApproved,'FALSE')='TRUE' AND EA2.TransactionDate IS NULL AND ELR.ResumptionDate IS NULL\r\n\t\t\t\t\t\t\t\t AND EMP.EmployeeID='" + empID + "'  AND ELR.StartDate<'" + text + "' ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "LeaveDetails", textCommand);
			return dataSet;
		}

		public EmployeeActivityData GetEmployeeLeavePaymentActivityByID(string sysDocID, string voucherID)
		{
			try
			{
				string str = "";
				EmployeeActivityData employeeActivityData = new EmployeeActivityData();
				string textCommand = "SELECT * FROM Employee_Leave_Payment WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(employeeActivityData, "Employee_Leave_Payment", textCommand);
				if (employeeActivityData.Tables["Employee_Leave_Payment"].Rows.Count > 0)
				{
					str = employeeActivityData.Tables["Employee_Leave_Payment"].Rows[0]["ActivityID"].ToString();
				}
				textCommand = "SELECT * FROM Employee_Activity WHERE ActivityID = " + str;
				FillDataSet(employeeActivityData, "Employee_Activity", textCommand);
				return employeeActivityData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeResumptionDataToPrint(int activityID)
		{
			try
			{
				byte.Parse((new Databases(base.DBConfig).GetFieldValue("Employee_Activity", "ActivityType", "ActivityID", activityID, null) ?? throw new CompanyException("Activity type is not defined for this activity.")).ToString());
				EmployeeActivityData employeeActivityData = new EmployeeActivityData();
				string textCommand = "SELECT FirstName+' '+LastName AS [Name] ,EA.*,LocationName,DivisionName,DepartmentName,PositionName,E.Joiningdate,LabourID,SponsorName,\r\n                                (SELECT DocumentNumber FROM Employee_Document ED WHERE ED.EmployeeID=E.EmployeeID AND ED.DocumentTypeID='PASSPORT') AS Passport ,TypeName as ContractType,\r\n                                Phone1,Phone2,Mobile,Email,PostalCode,Fax,Address1,Address2,Address3,AddressPrintFormat,City,State,ResumedDate [LastResumedDate],Photo\r\n                                from Employee_Activity EA \r\n                                LEFT JOIN Employee E ON EA.EmployeeID=E.EmployeeID\r\n                                LEFT OUTER JOIN Location ON Location.LocationID=E.LocationID\r\n                                LEFT OUTER JOIN Division ON Division.DivisionID=E.DivisionID\r\n                                LEFT OUTER JOIN Department ON Department.DepartmentID=E.DepartmentID \r\n                                LEFT OUTER JOIN Position ON Position.PositionID=E.PositionID\r\n                                LEFT OUTER JOIN Sponsor S ON S.SponsorID=E.SponsorID\r\n                                LEFT OUTER JOIN  Employee_Type  ET ON  E.ContractType=ET.TypeID\r\n                                LEFT OUTER JOIN Employee_Address EAD ON EAD.EmployeeID=E.EmployeeID AND AddressID='PRIMARY'\r\n                                WHERE ActivityID = " + activityID;
				FillDataSet(employeeActivityData, "Employee_Activity", textCommand);
				if (employeeActivityData == null || employeeActivityData.Tables.Count == 0 || employeeActivityData.Tables["Employee_Activity"].Rows.Count == 0)
				{
					return null;
				}
				string text = "";
				textCommand = "SELECT ER.*, ELR.*,LT.LeaveTypeName FROM Employee_Resumption ER \r\n                    LEFT OUTER JOIN Employee_Leave_request ELR ON ER.LeaveID=ELR.ActivityID\r\n                    LEft Outer JOIn Leave_Type LT on LT.LeaveTypeID=ELR.LeaveTypeID\r\n                                WHERE ER.ActivityID=" + activityID.ToString();
				text = "Employee_Resumption";
				FillDataSet(employeeActivityData, text, textCommand);
				return employeeActivityData;
			}
			catch
			{
				throw;
			}
		}
	}
}
