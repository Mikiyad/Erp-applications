using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class GLData : DataSet
	{
		public const string JOURNAL_TABLE = "Journal";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public const string JOURNALID_FIELD = "JournalID";

		public const string JOURNALDETAILID_FIELD = "JournalDetailID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string JOURNALDATE_FIELD = "JournalDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string ALLOCATIONID_FIELD = "AllocationID";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string DESCRIPTION_FIELD = "Description";

		public const string NARRATION_FIELD = "Narration";

		public const string NOTE_FIELD = "Note";

		public const string STJOURNALID_FIELD = "STJournalID";

		public const string STJYEAR_FIELD = "STJYear";

		public const string STJMONTH_FIELD = "STJMonth";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DUEDATE_FIELD = "DueDate";

		public const string ISBASEONLY_FIELD = "IsBaseOnly";

		public const string JOURNALDETAILS_TABLE = "Journal_Details";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string DOCTYPE_FIELD = "DocType";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string JDDATE_FIELD = "JDDate";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string DEBIT_FIELD = "Debit";

		public const string CREDIT_FIELD = "Credit";

		public const string DEBITFC_FIELD = "DebitFC";

		public const string CREDITFC_FIELD = "CreditFC";

		public const string ANALYSISID_FIELD = "AnalysisID";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string COSTCENTERID_FIELD = "CostCenterID";

		public const string BANKID_FIELD = "BankID";

		public const string CHECKDATE_FIELD = "CheckDate";

		public const string CHECKBOOKID_FIELD = "CheckbookID";

		public const string CHECKID_FIELD = "CheckID";

		public const string CHECKNUMBER_FIELD = "CheckNumber";

		public const string PAYMENTMETHODTYPE_FIELD = "PaymentMethodType";

		public const string ISARAP_FIELD = "IsARAP";

		public const string RECONCILEDATE_FIELD = "ReconcileDate";

		public const string ISRECONCILED_FIELD = "IsReconciled";

		public const string RECONCILEDBY_FIELD = "ReconciledBy";

		public const string STOREID_FIELD = "StoreID";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string ATTRIBUTEID1_FIELD = "AttributeID1";

		public const string ATTRIBUTEID2_FIELD = "AttributeID2";

		public const string CONSIGNID_FIELD = "ConsignID";

		public const string CONSIGNEXPENSEID_FIELD = "ConsignExpenseID";

		public const string JVENTRYTYPE_FIELD = "JVEntryType";

		public const string AMOUNT_FIELD = "Amount";

		public const string GLTYPEID_FIELD = "GLTYPEID";

		public const string GLIDRELATION_REL = "GLIDRelation";

		public const string GLTYPE_REL = "GLTypeRelation";

		public const string GLACCOUNT_REL = "GLAccountRelation";

		public const string AVGDEBIT_FIELD = "AvgDebit";

		public const string AVGCREDIT_FIELD = "AvgCredit";

		public const string REFERENCETYPE_FIELD = "ReferenceType";

		public const string GLTYPES_TABLE = "[GL Types]";

		public const string GLTYPESHORTNAME_FIELD = "GLTypeName";

		public const string ACCOUNTSBALANCE_TABLE = "[Accounts Balance]";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable JournalTable => base.Tables["Journal"];

		public DataTable JournalDetailsTable => base.Tables["Journal_Details"];

		public DataTable GLTypesTable => base.Tables["[GL Types]"];

		public DataTable GLAccountTable => base.Tables["Account"];

		public DataTable GLAccountsBalanceTable => base.Tables["[Accounts Balance]"];

		public DataTable AccountTypeTable => base.Tables["[Account Types]"];

		public GLData()
		{
			BuildDataTables();
		}

		public GLData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Journal");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("JournalID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("SysDocID", typeof(string));
			columns.Add("SysDocType", typeof(string));
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("JournalDate", typeof(DateTime)).AllowDBNull = false;
			columns.Add("VoucherID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("Narration", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("STJournalID", typeof(string));
			columns.Add("STJYear", typeof(short));
			columns.Add("STJMonth", typeof(short));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Journal_Details");
			columns = dataTable.Columns;
			dataColumn = columns.Add("JournalID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("JDDate", typeof(DateTime));
			columns.Add("DocType", typeof(short));
			columns.Add("AccountID", typeof(string)).AllowDBNull = true;
			columns.Add("Debit", typeof(decimal));
			columns.Add("Credit", typeof(decimal));
			columns.Add("DebitFC", typeof(decimal));
			columns.Add("CreditFC", typeof(decimal));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Description", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("AttributeID1", typeof(string));
			columns.Add("AttributeID2", typeof(string));
			columns.Add("JVEntryType", typeof(byte));
			columns.Add("ConsignID", typeof(string));
			columns.Add("ConsignExpenseID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("AllocationID", typeof(int));
			columns.Add("PayeeID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("PaymentMethodType", typeof(byte));
			columns.Add("BankID", typeof(string));
			columns.Add("CheckID", typeof(int));
			columns.Add("CheckDate", typeof(DateTime));
			columns.Add("CheckNumber", typeof(string));
			columns.Add("CheckbookID", typeof(string));
			columns.Add("IsARAP", typeof(bool));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("IsBaseOnly", typeof(bool));
			columns.Add("JournalDetailID", typeof(long));
			columns.Add("ReconcileDate", typeof(DateTime));
			base.Tables.Add(dataTable);
		}

		public static SysDocTypes GetGLType(string glTypeName)
		{
			string a = glTypeName.ToLower();
			if (a == "none")
			{
				return SysDocTypes.None;
			}
			return SysDocTypes.GJournal;
		}

		public static string GetGLType(SysDocTypes glTypes)
		{
			return "";
		}
	}
}
