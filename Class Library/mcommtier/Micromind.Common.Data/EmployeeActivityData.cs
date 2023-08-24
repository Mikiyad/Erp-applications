using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeActivityData : DataSet
	{
		public EmployeeActivityTypes activityType = EmployeeActivityTypes.General;

		public const string ACTIVITYID_FIELD = "ActivityID";

		public const string DOCID_FIELD = "DocNumber";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string SUBJECT_FIELD = "Subject";

		public const string ACTIVITYTYPE_FIELD = "ActivityType";

		public const string REASON_FIELD = "Reason";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string EMPLOYEEACTIVITY_TABLE = "Employee_Activity";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string TRANSFERFROMLOCATION_FIELD = "TransferFromLocation";

		public const string TRANSFERTOLOCATION_FIELD = "TransferToLocation";

		public const string TRANSFERFROMDEP_FIELD = "TransferFromDep";

		public const string TRANSFERTODEP_FIELD = "TransferToDep";

		public const string TRANSFERFROMDIVISION_FIELD = "TransferFromDivision";

		public const string TRANSFERTODIVISION_FIELD = "TransferToDivision";

		public const string REQUESTEDBY_FIELD = "RequestedBy";

		public const string PERIOD_FIELD = "Period";

		public const string EMPLOYEETRANSFER_TABLE = "Employee_Transfer";

		public const string TERMINATIONTYPE_FIELD = "TerminationType";

		public const string EMPLOYEETERMINATION_TABLE = "Employee_Termination";

		public const string EMPLOYEECANCELLATION_TABLE = "Employee_Cancellation";

		public const string LASTWORKINGDATE_FIELD = "LastWorkingDate";

		public const string CANCELLATIONTYPE_FIELD = "CancellationType";

		public const string VCAPPRECEIVEDDATE_FIELD = "VCAppReceivedDate";

		public const string VCAPPTYPEDDATE_FIELD = "VCAppTypedDate";

		public const string VCAPPSUBMITTEDDATE_FIELD = "VCAppSubmittedDate";

		public const string MBREFNUMBERCANCEL_FIELD = "MBReferenceNoCancel";

		public const string RPCANCELDATEIMG_FIELD = "RPCancelDateIMG";

		public const string DEPARTUREDATE_FIELD = "DepartureDate";

		public const string EXITPORT_FIELD = "ExitPort";

		public const string SIGNEDCNDOCRECVDDATE_FIELD = "SignedCNDOCRecvdDate";

		public const string FROMGRADE_FIELD = "FromGrade";

		public const string TOGRADE_FIELD = "ToGrade";

		public const string FROMPOSITION_FIELD = "FromPosition";

		public const string TOPOSITION_FIELD = "ToPosition";

		public const string EMPLOYEEPROMOTION_TABLE = "Employee_Promotion";

		public const string DISCIPLINARYACTIONTYPEID_FIELD = "ActionTypeID";

		public const string EMPLOYEEDISCIPLINARYACTION_TABLE = "Employee_DisciplinaryAction";

		public const string EMPLOYEEREHIRE_TABLE = "Employee_Rehire";

		public const string EMPLOYEEGENERALACTIVITY_TABLE = "Employee_GeneralActivity";

		public const string GENERALACTIVITYTYPE_FIELD = "GeneralActivityTypeID";

		public const string EMPLOYEELEAVEREQUEST_TABLE = "Employee_Leave_Request";

		public const string LEAVETYPEID_FIELD = "LeaveTypeID";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string ACTUALLEAVEDAYS_FIELD = "ActualLeaveDays";

		public const string TICKETENTITLE_FIELD = "TicketEntitle";

		public const string REPLACEMENTID_FIELD = "ReplacementID";

		public const string ISAPPROVED_FIELD = "IsApproved";

		public const string ISCLOSED_FIELD = "IsClosed";

		public const string ISVOID_FIELD = "IsVoid";

		public const string APPROVEDBY_FIELD = "ApprovedBy";

		public const string APPROVEDDATE_FIELD = "ApproveDate";

		public const string RESUMPTIONDATE_FIELD = "ResumptionDate";

		public const string TRAVELLINGDATE_FIELD = "TravellingDate";

		public const string APPROVALREMARKS_FIELD = "ApprovalRemarks";

		public const string EMPLOYEELEAVEENCASHMENT_TABLE = "Employee_Leave_Encashment";

		public const string ENCASHNO_FIELD = "EncashNo";

		public const string ASONDATE_FIELD = "AsOnDate";

		public const string LEAVEELIGIBLE_FIELD = "LeaveEligible";

		public const string LEAVEENCASH_FIELD = "LeaveEncash";

		public const string AMOUNTENCASH_FIELD = "AmountEncash";

		public const string EMPLOYEELEAVESALARYPAYMENT_TABLE = "Employee_Leave_Payment";

		public const string LEAVEPAYIDFIELD_FIELD = "LeavePaymentID";

		public const string LEAVEAPPNOFIELD_FIELD = "LeaveApplicationNo";

		public const string AMOUNT_FIELD = "Amount";

		public const string TICKETAMOUNT_FIELD = "TicketAmount";

		public const string TOTAL_FIELD = "Total";

		public const string SALARYAMOUNT_FIELD = "SalaryAmount";

		public const string ELIGIBLEDAYS_FIELD = "EligibleDays";

		public const string DEDUCTIONID_FIELD = "DeductionID";

		public const string DEDUCTIONAMOUNT_FIELD = "DeductionAmount";

		public const string BASEDONLEAVETAKEN_FIELD = "BasedOnLeaveTaken";

		public const string EMPLOYEERESUMPTION_TABLE = "Employee_Resumption";

		public const string LEAVEID_FIELD = "LeaveID";

		public const string EMPLOYEEPASSPORTCONTROL_TABLE = "Employee_Passport_Control";

		public const string REASONID_FIELD = "ReasonID";

		public const string PPRELEASEDATE_FIELD = "PPReleaseDate";

		public const string PPRETURNDATE_FIELD = "PPReturnDate";

		public const string ISSUEDBY_FIELD = "IssuedBy";

		public const string ACCEPTEDBY_FIELD = "AcceptedBy";

		public const string RETURNNOTE_FIELD = "ReturnNote";

		public const string RATING_FIELD = "Rating";

		public const string EMPLOYEEABSCONDING_TABLE = "Employee_Absconding";

		public const string ADVICERECEIVEDON_FIELD = "AdviceReceivedOn";

		public const string REALABSCONDINGDDATE_FIELD = "RealAbscondingDate";

		public const string ABSCONDINGREGDATEMOL_FIELD = "AbscondingRegDateMOL";

		public const string MBREFNUMBER_FIELD = "MBReferenceNo";

		public const string ABSCONDINGREGDATEIMG_FIELD = "AbscondingRegDateIMG";

		public const string MBREFNUMBERABS_FIELD = "AbscondingReferenceNo";

		public const string PASSPORTHELDINIMG_FIELD = "PassportHeldInIMG";

		public const string TICKETAMOUNTPAID_FIELD = "TicketAmountPaid";

		public const string MOLCANCELLATIONDATE_FIELD = "MOLCancellationDate";

		public const string IMGCANCELLATIONDATE_FIELD = "IMGCancellationDate";

		public DataTable EmployeeActivityTable => base.Tables["Employee_Activity"];

		public DataTable EmployeeTransferTable => base.Tables["Employee_Transfer"];

		public DataTable EmployeeTerminationTable => base.Tables["Employee_Termination"];

		public DataTable EmployeeCancellationTable => base.Tables["Employee_Cancellation"];

		public DataTable EmployeePromotionTable => base.Tables["Employee_Promotion"];

		public DataTable EmployeeDisciplinaryActionTable => base.Tables["Employee_DisciplinaryAction"];

		public DataTable EmployeeRehireTable => base.Tables["Employee_Rehire"];

		public DataTable EmployeeGeneralActivityTable => base.Tables["Employee_GeneralActivity"];

		public DataTable EmployeeLeaveRequestTable => base.Tables["Employee_Leave_Request"];

		public DataTable EmployeeResumptionTable => base.Tables["Employee_Resumption"];

		public DataTable EmployeeLeaveEncashmentTable => base.Tables["Employee_Leave_Encashment"];

		public DataTable EmployeeLeavePaymentTable => base.Tables["Employee_Leave_Payment"];

		public DataTable EmployeePassportControlTable => base.Tables["Employee_Passport_Control"];

		public DataTable EmployeeAbscondingTable => base.Tables["Employee_Absconding"];

		public EmployeeActivityData()
		{
			BuildDataTables();
		}

		public EmployeeActivityData(EmployeeActivityTypes activityType)
		{
			this.activityType = activityType;
			BuildDataTables();
		}

		public EmployeeActivityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Activity");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ActivityID", typeof(int));
			dataColumn.AutoIncrement = true;
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Subject", typeof(string));
			columns.Add("ActivityType", typeof(byte));
			columns.Add("Reason", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			if (activityType == EmployeeActivityTypes.Transfer)
			{
				dataTable = new DataTable("Employee_Transfer");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("TransferFromLocation", typeof(string));
				columns.Add("TransferToLocation", typeof(string));
				columns.Add("TransferFromDep", typeof(string));
				columns.Add("TransferToDep", typeof(string));
				columns.Add("TransferFromDivision", typeof(string));
				columns.Add("TransferToDivision", typeof(string));
				columns.Add("FromPosition", typeof(string));
				columns.Add("ToPosition", typeof(string));
				columns.Add("RequestedBy", typeof(string));
				columns.Add("Period", typeof(string));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.Termination)
			{
				dataTable = new DataTable("Employee_Termination");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("RequestedBy", typeof(string));
				columns.Add("TerminationType", typeof(char));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.Cancellation)
			{
				dataTable = new DataTable("Employee_Cancellation");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("CancellationType", typeof(byte));
				columns.Add("VCAppReceivedDate", typeof(DateTime));
				columns.Add("VCAppTypedDate", typeof(DateTime));
				columns.Add("VCAppSubmittedDate", typeof(DateTime));
				columns.Add("RPCancelDateIMG", typeof(DateTime));
				columns.Add("MBReferenceNoCancel", typeof(string));
				columns.Add("DepartureDate", typeof(DateTime));
				columns.Add("ExitPort", typeof(string));
				columns.Add("LastWorkingDate", typeof(DateTime));
				columns.Add("SignedCNDOCRecvdDate", typeof(DateTime));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.Promotion)
			{
				dataTable = new DataTable("Employee_Promotion");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("RequestedBy", typeof(string));
				columns.Add("FromGrade", typeof(string));
				columns.Add("ToGrade", typeof(string));
				columns.Add("FromPosition", typeof(string));
				columns.Add("ToPosition", typeof(string));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.DisciplinaryAction)
			{
				dataTable = new DataTable("Employee_DisciplinaryAction");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("RequestedBy", typeof(string));
				columns.Add("ActionTypeID", typeof(string));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.Rehire)
			{
				dataTable = new DataTable("Employee_Rehire");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("RequestedBy", typeof(string));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.General)
			{
				dataTable = new DataTable("Employee_GeneralActivity");
				columns = dataTable.Columns;
				columns.Add("VoucherID", typeof(string));
				columns.Add("SysDocID", typeof(string));
				columns.Add("RequestedBy", typeof(string));
				columns.Add("GeneralActivityTypeID", typeof(string));
				columns.Add("TransactionDate", typeof(DateTime));
				columns.Add("EmployeeID", typeof(string));
				columns.Add("Reference", typeof(string));
				columns.Add("Subject", typeof(string));
				columns.Add("ActivityType", typeof(byte));
				columns.Add("Reason", typeof(string));
				columns.Add("Note", typeof(string));
				columns.Add("Rating", typeof(int)).DefaultValue = 0;
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.Leave)
			{
				dataTable = new DataTable("Employee_Leave_Request");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("DocNumber", typeof(string));
				columns.Add("LeaveTypeID", typeof(string));
				columns.Add("StartDate", typeof(DateTime));
				columns.Add("EndDate", typeof(DateTime));
				columns.Add("ActualLeaveDays", typeof(short));
				columns.Add("TicketEntitle", typeof(bool));
				columns.Add("ReplacementID", typeof(string));
				columns.Add("IsApproved", typeof(bool));
				columns.Add("IsClosed", typeof(bool));
				columns.Add("IsVoid", typeof(bool));
				columns.Add("ApprovedBy", typeof(string));
				columns.Add("ApproveDate", typeof(DateTime));
				columns.Add("ResumptionDate", typeof(DateTime));
				columns.Add("TravellingDate", typeof(DateTime));
				columns.Add("ApprovalRemarks", typeof(string));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.LeaveEncashment)
			{
				dataTable = new DataTable("Employee_Leave_Encashment");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("EncashNo", typeof(string));
				columns.Add("EmployeeID", typeof(string));
				columns.Add("AsOnDate", typeof(DateTime));
				columns.Add("LeaveEligible", typeof(byte));
				columns.Add("LeaveEncash", typeof(byte));
				columns.Add("AmountEncash", typeof(decimal));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.LeavePayment)
			{
				dataTable = new DataTable("Employee_Leave_Payment");
				columns = dataTable.Columns;
				columns.Add("SysDocID", typeof(string));
				columns.Add("VoucherID", typeof(string));
				columns.Add("ActivityID", typeof(int));
				columns.Add("LeavePaymentID", typeof(string));
				columns.Add("LeaveApplicationNo", typeof(string));
				columns.Add("StartDate", typeof(DateTime));
				columns.Add("EndDate", typeof(DateTime));
				columns.Add("Amount", typeof(decimal));
				columns.Add("SalaryAmount", typeof(decimal));
				columns.Add("EligibleDays", typeof(long));
				columns.Add("BasedOnLeaveTaken", typeof(bool));
				columns.Add("TicketAmount", typeof(decimal));
				columns.Add("Total", typeof(decimal));
				columns.Add("DeductionAmount", typeof(decimal));
				columns.Add("DeductionID", typeof(string));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.Resumption)
			{
				dataTable = new DataTable("Employee_Resumption");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("LeaveID", typeof(int));
				columns.Add("EndDate", typeof(DateTime));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.LeaveEncashment)
			{
				dataTable = new DataTable("Employee_Leave_Encashment");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.PassportControl)
			{
				dataTable = new DataTable("Employee_Passport_Control");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("DocNumber", typeof(string));
				columns.Add("ReasonID", typeof(string));
				columns.Add("TransactionDate", typeof(DateTime));
				columns.Add("PPReleaseDate", typeof(DateTime));
				columns.Add("PPReturnDate", typeof(DateTime));
				columns.Add("IsVoid", typeof(bool));
				columns.Add("ApprovedBy", typeof(string));
				columns.Add("IssuedBy", typeof(string));
				columns.Add("AcceptedBy", typeof(string));
				columns.Add("Note", typeof(string));
				columns.Add("ReturnNote", typeof(string));
				base.Tables.Add(dataTable);
			}
			else if (activityType == EmployeeActivityTypes.Absconding)
			{
				dataTable = new DataTable("Employee_Absconding");
				columns = dataTable.Columns;
				columns.Add("ActivityID", typeof(int));
				columns.Add("AdviceReceivedOn", typeof(DateTime));
				columns.Add("RealAbscondingDate", typeof(DateTime));
				columns.Add("AbscondingRegDateMOL", typeof(DateTime));
				columns.Add("MBReferenceNo", typeof(string));
				columns.Add("AbscondingRegDateIMG", typeof(DateTime));
				columns.Add("AbscondingReferenceNo", typeof(string));
				columns.Add("PassportHeldInIMG", typeof(string));
				columns.Add("TicketAmountPaid", typeof(string));
				columns.Add("LastWorkingDate", typeof(DateTime));
				columns.Add("MOLCancellationDate", typeof(DateTime));
				columns.Add("IMGCancellationDate", typeof(DateTime));
				base.Tables.Add(dataTable);
			}
		}
	}
}
