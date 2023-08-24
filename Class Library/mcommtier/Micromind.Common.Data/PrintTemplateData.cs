using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PrintTemplateData : DataSet
	{
		public const string PRINTTEMPLATES_TABLE = "[Print Templates]";

		public const string TEMPLATEID_FIELD = "TemplateID";

		public const string NAME_FIELD = "Name";

		public const string DATA_FIELD = "Data";

		public const string PRINTBILLTO_FIELD = "PrintBillTo";

		public const string PRINTSHIPTO_FIELD = "PrintShipTo";

		public const string PRINTNUMBER_FIELD = "PrintNumber";

		public const string PRINTCOMPANYHEADER_FIELD = "PrintCompanyHeader";

		public const string PRINTDATE_FIELD = "PrintDate";

		public const string PRINTDOCUMENTTITLE_FIELD = "PrintDocumentTitle";

		public const string BILLTOTITLE_FIELD = "BillToTitle";

		public const string SHIPTOTITLE_FIELD = "ShipToTitle";

		public const string NUMBERTITLE_FIELD = "NumberTitle";

		public const string DATETITLE_FIELD = "DateTitle";

		public const string DOCUMENTTITLE_FIELD = "DocumentTitle";

		public const string PRINTPONO_FIELD = "PrintPONo";

		public const string POTITLE_FIELD = "POTitle";

		public const string PRINTDUEDATE_FIELD = "PrintDueDate";

		public const string DUEDATETITLE_FIELD = "DueDateTitle";

		public const string PRINTSALESREP_FIELD = "PrintSalesRep";

		public const string SALESREPTITLE_FIELD = "SalesRepTitle";

		public const string COLDESCRIPTIONTITLE_FIELD = "ColDescriptionTitle";

		public const string COLPRICETITLE_FIELD = "ColPriceTitle";

		public const string COLAMOUNTTITLE_FIELD = "ColAmountTitle";

		public const string PRINTCUSTOMERMESSAGE_FIELD = "PrintCustomerMessage";

		public const string CUSTOMERMESSAGETITLE_FIELD = "CustomerMessageTitle";

		public const string PRINTDISCOUNT_FIELD = "PrintDiscount";

		public const string DISCOUNTTITLE_FIELD = "DiscountTitle";

		public const string PRINTTOTAL_FIELD = "PrintTotal";

		public const string TOTALTITLE_FIELD = "TotalTitle";

		public const string PRINTSUBTOTAL_FIELD = "PrintSubtotal";

		public const string SUBTOTALTITLE_FIELD = "SubtotalTitle";

		public const string PRINTBALANCEDUE_FIELD = "PrintBalanceDue";

		public const string BALANCEDUETITLE_FIELD = "BalanceDueTitle";

		public const string PRINTFREIGHT_FIELD = "PrintFreight";

		public const string FREIGHTTITLE_FIELD = "FreightTitle";

		public const string PRINTMISCELLANEOUS_FIELD = "PrintMiscellaneous";

		public const string MISCELLANEOUSTITLE_FIELD = "MiscellaneousTitle";

		public const string DOCUMENTTYPE_FIELD = "DocumentType";

		public const string ISREADONLY_FIELD = "IsReadOnly";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string PRINTCOMPANYADDRESS_FIELD = "PrintCompanyAddress";

		public const string PRINTADDRESSEMAIL_FIELD = "PrintAddressEmail";

		public const string PRINTADDRESSTELEPHONE_FIELD = "PrintAddressTelephone";

		public const string PRINTADDRESSWEBSITE_FIELD = "PrintAddressWebsite";

		public const string PRINTADDRESSFAX_FIELD = "PrintAddressFax";

		public const string PRINTADDRESSMOBILE_FIELD = "PrintAddressMobile";

		public const string PRINTADDRESS1_FIELD = "PrintAddress1";

		public const string PRINTADDRESS2_FIELD = "PrintAddress2";

		public const string PRINTADDRESS3_FIELD = "PrintAddress3";

		public const string PRINTADDRESSCITY_FIELD = "PrintAddressCity";

		public const string PRINTADDRESSCOUNTRY_FIELD = "PrintAddressCountry";

		public const string PRINTTOTALAMOUNTINWORDS_FIELD = "PrintTotalAmountInWords";

		public const string PRINTSIGNATURE1_FIELD = "PrintSignature1";

		public const string PRINTSIGNATURE2_FIELD = "PrintSignature2";

		public const string PRINTLOGO_FIELD = "PrintLogo";

		public const string PRINTREFERENCE_FIELD = "PrintReference";

		public const string PRINTLOCATION_FIELD = "PrintLocation";

		public const string PRINTCOLNUMBER_FIELD = "PrintColNumber";

		public const string PRINTCOLDESCRIPTION_FIELD = "PrintColDescription";

		public const string PRINTCOLQTY_FIELD = "PrintColQty";

		public const string PRINTCOLPRICE_FIELD = "PrintColPrice";

		public const string PRINTCOLTOTAL_FIELD = "PrintColTotal";

		public const string SIGNATURE1TITLE_FIELD = "Signature1Title";

		public const string SIGNATURE2TITLE_FIELD = "Signature2Title";

		public const string REFERENCETITLE_FIELD = "ReferenceTitle";

		public const string LOCATIONTITLE_FIELD = "LocationTitle";

		public const string COLNUMBERTITLE_FIELD = "ColNumberTitle";

		public const string COLQTYTITLE_FIELD = "ColQtyTitle";

		public const string COLTOTALTITLE_FIELD = "ColTotalTitle";

		public const string COLDATETITLE_FIELD = "ColDateTitle";

		public const string COLDEBITTITLE_FIELD = "ColDebitTitle";

		public const string COLCREDITTITLE_FIELD = "ColCreditTitle";

		public const string COLBALANCETITLE_FIELD = "ColBalanceTitle";

		public const string STATEMENTPERIODTITLE_FIELD = "StatementPeriodTitle";

		public const string STATEMENTAGING1TITLE_FIELD = "StatementAging1Title";

		public const string STATEMENTAGING2TITLE_FIELD = "StatementAging2Title";

		public const string STATEMENTAGING3TITLE_FIELD = "StatementAging3Title";

		public const string STATEMENTAGING4TITLE_FIELD = "StatementAging4Title";

		public const string STATEMENTAGING5TITLE_FIELD = "StatementAging5Title";

		public const string STATEMENTAGING6TITLE_FIELD = "StatementAging6Title";

		public const string STATEMENTAGING7TITLE_FIELD = "StatementAging7Title";

		public const string STATEMENTAGING8TITLE_FIELD = "StatementAging8Title";

		public const string TOTALDEBITTITLE_FIELD = "TotalDebitTitle";

		public const string TOTALCREDITTITLE_FIELD = "TotalCreditTitle";

		public const string BALANCETITLE_FIELD = "BalanceTitle";

		public const string PRINTCOLDATE_FIELD = "PrintColDate";

		public const string PRINTCOLDEBIT_FIELD = "PrintColDEbit";

		public const string PRINTCOLCREDIT_FIELD = "PrintColCredit";

		public const string PRINTCOLBALANCE_FIELD = "PrintColBalance";

		public const string PRINTSTATEMENTPERIOD_FIELD = "PrintStatementPeriod";

		public const string PRINTTOTALDEBIT_FIELD = "PrintTotalDebit";

		public const string PRINTTOTALCREDIT_FIELD = "PrintTotalCredit";

		public const string PRINTBALANCE_FIELD = "PrintBalance";

		public const string PRINTMONTHLYAGING_FIELD = "PrintMonthlyAging";

		public const string PRINTSTATEMENTCHECKS_FIELD = "PrintStatementChecks";

		public const string PRINTJOB_FIELD = "PrintJob";

		public const string JOBTITLE_FIELD = "JobTitle";

		public const string PHONETITLE_FIELD = "PhoneTitle";

		public const string FAXTITLE_FIELD = "FaxTitle";

		public const string EMAILTITLE_FIELD = "EmailTitle";

		public const string WEBSITETITLE_FIELD = "WebsiteTitle";

		public const string DEFAULTLANGUAGE_FIELD = "DefaultLanguage";

		public const string PRINTSHIPPER_FIELD = "PrintShipper";

		public const string SHIPPERTITLE_FIELD = "ShipperTitle";

		public const string PRINTPAYEE_FIELD = "PrintPayee";

		public const string PAYEETITLE_FIELD = "PayeeTitle";

		public const string PRINTPAYEEID_FIELD = "PrintPayeeID";

		public const string PRINTPAYEECOMPANYNAME_FIELD = "PrintPayeeCompanyName";

		public const string PRINTPAYEELASTNAME_FIELD = "PrintPayeeLastName";

		public const string PRINTPAYEMENTMETHOD_FIELD = "PrintPaymentMethod";

		public const string COLBALANCEALIGN_FIELD = "ColBalanceAlign";

		public const string COLCREDITALIGN_FIELD = "ColCreditAlign";

		public const string COLDEBITALIGN_FIELD = "ColDebitAlign";

		public const string COLDATEALIGN_FIELD = "ColDateAlign";

		public const string COLTOTALALIGN_FIELD = "ColTotalAlign";

		public const string COLQTYALIGN_FIELD = "ColQtyAlign";

		public const string COLNUMBERALIGN_FIELD = "ColNumberAlign";

		public const string COLDESCRIPTIONALIGN_FIELD = "ColDescriptionAlign";

		public const string COLPRICEALIGN_FIELD = "ColPriceAlign";

		public const string COLAMOUNTALIGN_FIELD = "ColAmountAlign";

		public const string COLUNITALIGN_FIELD = "ColUnitAlign";

		public const string COLUNITTITLE_FIELD = "ColUnitTitle";

		public const string PRINTCOLUNIT_FIELD = "PrintColUnit";

		public const string EDITION_FIELD = "Edition";

		public const string PRINTCOLAMOUNT_FIELD = "PrintColAmount";

		public const string PAYEEIDTITLE_FIELD = "PayeeIDTitle";

		public const string PAYMENTMETHODTITLE_FIELD = "PaymentMethodTitle";

		public const string PRINTSIGNATURE3_FIELD = "PrintSignature3";

		public const string SIGNATURE3TITLE_FIELD = "Signature3Title";

		public const string TOTALAMOUNTINWORDSTITLE_FIELD = "TotalAmountInWordsTitle";

		public const string COLORIGINALAMOUNTTITLE_FIELD = "ColOriginalAmountTitle";

		public const string PRINTCOLORIGINALAMOUNT_FIELD = "PrintColOriginalAmount";

		public const string COLORIGINALAMOUNTALIGN_FIELD = "ColOriginalAmountAlign";

		public const string COLAMOUNTDUETITLE_FIELD = "ColAmountDueTitle";

		public const string COLAMOUNTDUEALIGN_FIELD = "ColAmountDueAlign";

		public const string PRINTCOLAMOUNTDUE_FIELD = "PrintColAmountDue";

		public const string COLPAYMENTTITLE_FIELD = "ColPaymentTitle";

		public const string COLPAYMENTALIGN_FIELD = "ColPaymentAlign";

		public const string PRINTCOLPAYMENT_FIELD = "PrintColPayment";

		public const string PRINTCOLDISCOUNT_FIELD = "PrintColDiscount";

		public const string COLDISCOUNTTITLE_FIELD = "ColDiscountTitle";

		public const string COLDISCOUNTALIGN_FIELD = "ColDiscountAlign";

		public const string CHECKDATETITLE_FIELD = "CheckDateTitle";

		public const string PRINTCHECKDATE_FIELD = "PrintCheckDate";

		public const string PRINTBANK_FIELD = "PrintBank";

		public const string BANKTITLE_FIELD = "BankTitle";

		public const string CHECKNUMBERTITLE_FIELD = "CheckNumberTitle";

		public const string PRINTCHECKNUMBER_FIELD = "PrintCheckNumber";

		public const string PRINTACCOUNT_FIELD = "PrintAccount";

		public const string ACCOUNTTITLE_FIELD = "AccountTitle";

		public const string PRINTEMPLOYEE_FIELD = "PrintEmployee";

		public const string EMPLOYEETITLE_FIELD = "EmployeeTitle";

		public const string PRINTBEGINDATE_FIELD = "PrintBeginDate";

		public const string BEGINDATETITLE_FIELD = "BeginDateTitle";

		public const string PRINTENDDATE_FIELD = "PrintEndDate";

		public const string ENDDATETITLE_FIELD = "EndDateTitle";

		public const string PRINTCOLTYPE_FIELD = "PrintColType";

		public const string COLTYPETITLE_FIELD = "ColTypeTitle";

		public const string COLTYPEALIGN_FIELD = "ColTypeAlign";

		public const string COLPAYEETITLE_FIELD = "ColPayeeTitle";

		public const string COLPAYEEALIGN_FIELD = "ColPayeeAlign";

		public const string PRINTCOLPAYEE_FIELD = "PrintColPayee";

		public DataTable PrintTemplatesTable => base.Tables["[Print Templates]"];

		public PrintTemplateData()
		{
			BuildDataTables();
		}

		public PrintTemplateData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Print Templates]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TemplateID", typeof(short));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Name", typeof(string)).AllowDBNull = false;
			columns.Add("Data", typeof(string));
			columns.Add("PrintBillTo", typeof(bool)).DefaultValue = true;
			columns.Add("PrintShipTo", typeof(bool)).DefaultValue = true;
			columns.Add("PrintNumber", typeof(bool)).DefaultValue = true;
			columns.Add("PrintCompanyHeader", typeof(bool)).DefaultValue = true;
			columns.Add("PrintDate", typeof(bool)).DefaultValue = true;
			columns.Add("PrintDocumentTitle", typeof(bool)).DefaultValue = true;
			columns.Add("BillToTitle", typeof(string)).DefaultValue = "Bill To";
			columns.Add("ShipToTitle", typeof(string)).DefaultValue = "Ship To";
			columns.Add("NumberTitle", typeof(string)).DefaultValue = "Number";
			columns.Add("DateTitle", typeof(string)).DefaultValue = "Date";
			columns.Add("DocumentTitle", typeof(string));
			columns.Add("PrintPONo", typeof(bool)).DefaultValue = true;
			columns.Add("POTitle", typeof(string)).DefaultValue = "PO Number";
			columns.Add("PrintDueDate", typeof(bool)).DefaultValue = true;
			columns.Add("DueDateTitle", typeof(string)).DefaultValue = "Due Date";
			columns.Add("PrintSalesRep", typeof(bool)).DefaultValue = true;
			columns.Add("SalesRepTitle", typeof(string)).DefaultValue = "Sales Rep";
			columns.Add("ColDescriptionTitle", typeof(string)).DefaultValue = "Description";
			columns.Add("ColPriceTitle", typeof(string)).DefaultValue = "Price";
			columns.Add("ColAmountTitle", typeof(string)).DefaultValue = "Amount";
			columns.Add("PrintCustomerMessage", typeof(bool)).DefaultValue = true;
			columns.Add("CustomerMessageTitle", typeof(string)).DefaultValue = "Customer Message";
			columns.Add("PrintDiscount", typeof(bool)).DefaultValue = true;
			columns.Add("DiscountTitle", typeof(string)).DefaultValue = "Discount";
			columns.Add("PrintTotal", typeof(bool)).DefaultValue = true;
			columns.Add("TotalTitle", typeof(string)).DefaultValue = "Total";
			columns.Add("PrintSubtotal", typeof(bool)).DefaultValue = true;
			columns.Add("SubtotalTitle", typeof(string)).DefaultValue = "Subtotal";
			columns.Add("PrintBalanceDue", typeof(bool)).DefaultValue = true;
			columns.Add("BalanceDueTitle", typeof(string)).DefaultValue = "Balance Due";
			columns.Add("PrintFreight", typeof(bool)).DefaultValue = true;
			columns.Add("FreightTitle", typeof(string)).DefaultValue = "Freight";
			columns.Add("PrintMiscellaneous", typeof(bool)).DefaultValue = true;
			columns.Add("MiscellaneousTitle", typeof(string)).DefaultValue = "Miscellaneous";
			columns.Add("DocumentType", typeof(byte));
			columns.Add("IsReadOnly", typeof(bool)).DefaultValue = false;
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			columns.Add("PrintCompanyAddress", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddressEmail", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddressTelephone", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddressWebsite", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddressFax", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddressMobile", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddress1", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddress2", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddress3", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddressCity", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAddressCountry", typeof(bool)).DefaultValue = true;
			columns.Add("PrintTotalAmountInWords", typeof(bool)).DefaultValue = true;
			columns.Add("PrintSignature1", typeof(bool)).DefaultValue = true;
			columns.Add("PrintSignature2", typeof(bool)).DefaultValue = true;
			columns.Add("PrintLogo", typeof(bool)).DefaultValue = true;
			columns.Add("Signature1Title", typeof(string)).DefaultValue = "Signature";
			columns.Add("Signature2Title", typeof(string)).DefaultValue = "";
			columns.Add("ReferenceTitle", typeof(string)).DefaultValue = "Reference";
			columns.Add("LocationTitle", typeof(string)).DefaultValue = "Location";
			columns.Add("PrintReference", typeof(bool)).DefaultValue = true;
			columns.Add("PrintLocation", typeof(bool)).DefaultValue = true;
			columns.Add("PrintColNumber", typeof(bool)).DefaultValue = true;
			columns.Add("PrintColDescription", typeof(bool)).DefaultValue = true;
			columns.Add("PrintColQty", typeof(bool)).DefaultValue = true;
			columns.Add("PrintColPrice", typeof(bool)).DefaultValue = true;
			columns.Add("PrintColTotal", typeof(bool)).DefaultValue = true;
			columns.Add("ColNumberTitle", typeof(string)).DefaultValue = "Number";
			columns.Add("ColQtyTitle", typeof(string)).DefaultValue = "Qty";
			columns.Add("ColTotalTitle", typeof(string)).DefaultValue = "Total";
			columns.Add("ColDateTitle", typeof(string)).DefaultValue = "Date";
			columns.Add("ColDebitTitle", typeof(string)).DefaultValue = "Debit";
			columns.Add("ColCreditTitle", typeof(string)).DefaultValue = "Credit";
			columns.Add("ColBalanceTitle", typeof(string)).DefaultValue = "Balance";
			columns.Add("StatementPeriodTitle", typeof(string)).DefaultValue = "Period";
			columns.Add("StatementAging1Title", typeof(string)).DefaultValue = "";
			columns.Add("StatementAging2Title", typeof(string)).DefaultValue = "";
			columns.Add("StatementAging3Title", typeof(string)).DefaultValue = "";
			columns.Add("StatementAging4Title", typeof(string)).DefaultValue = "";
			columns.Add("StatementAging5Title", typeof(string)).DefaultValue = "";
			columns.Add("StatementAging6Title", typeof(string)).DefaultValue = "";
			columns.Add("StatementAging7Title", typeof(string)).DefaultValue = "";
			columns.Add("StatementAging8Title", typeof(string)).DefaultValue = "";
			columns.Add("TotalDebitTitle", typeof(string)).DefaultValue = "Total Debit";
			columns.Add("TotalCreditTitle", typeof(string)).DefaultValue = "Total Credit";
			columns.Add("BalanceTitle", typeof(string)).DefaultValue = "Balance";
			columns.Add("PrintColDate", typeof(bool)).DefaultValue = true;
			columns.Add("PrintColDEbit", typeof(bool)).DefaultValue = true;
			columns.Add("PrintColCredit", typeof(bool)).DefaultValue = true;
			columns.Add("PrintColBalance", typeof(bool)).DefaultValue = true;
			columns.Add("PrintStatementPeriod", typeof(bool)).DefaultValue = true;
			columns.Add("PrintTotalDebit", typeof(bool)).DefaultValue = true;
			columns.Add("PrintTotalCredit", typeof(bool)).DefaultValue = true;
			columns.Add("PrintBalance", typeof(bool)).DefaultValue = true;
			columns.Add("PrintMonthlyAging", typeof(bool)).DefaultValue = true;
			columns.Add("PrintStatementChecks", typeof(bool)).DefaultValue = true;
			columns.Add("PrintJob", typeof(bool)).DefaultValue = true;
			columns.Add("JobTitle", typeof(string)).DefaultValue = "Job";
			columns.Add("DefaultLanguage", typeof(byte)).DefaultValue = 1;
			columns.Add("PrintShipper", typeof(bool)).DefaultValue = false;
			columns.Add("ShipperTitle", typeof(string)).DefaultValue = "Shipper";
			columns.Add("PhoneTitle", typeof(string)).DefaultValue = "Tel";
			columns.Add("EmailTitle", typeof(string)).DefaultValue = "Email";
			columns.Add("FaxTitle", typeof(string)).DefaultValue = "Fax";
			columns.Add("WebsiteTitle", typeof(string)).DefaultValue = "Website";
			columns.Add("PrintPayee", typeof(bool)).DefaultValue = false;
			columns.Add("PayeeTitle", typeof(string)).DefaultValue = "Received From:";
			columns.Add("PrintPayeeID", typeof(bool)).DefaultValue = true;
			columns.Add("PrintPayeeCompanyName", typeof(bool)).DefaultValue = false;
			columns.Add("PrintPayeeLastName", typeof(bool)).DefaultValue = false;
			columns.Add("PrintPaymentMethod", typeof(bool)).DefaultValue = false;
			columns.Add("ColBalanceAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("ColCreditAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("ColDebitAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("ColDateAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("ColTotalAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("ColQtyAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("ColNumberAlign", typeof(byte)).DefaultValue = 1;
			columns.Add("ColDescriptionAlign", typeof(byte)).DefaultValue = 1;
			columns.Add("ColPriceAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("ColAmountAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("ColUnitAlign", typeof(byte)).DefaultValue = 1;
			columns.Add("ColUnitTitle", typeof(string)).DefaultValue = "Unit";
			columns.Add("PrintColUnit", typeof(bool)).DefaultValue = true;
			columns.Add("Edition", typeof(byte)).DefaultValue = 1;
			columns.Add("PrintColAmount", typeof(bool)).DefaultValue = true;
			columns.Add("PayeeIDTitle", typeof(string)).DefaultValue = "Payee ID";
			columns.Add("PaymentMethodTitle", typeof(string)).DefaultValue = "Payment Method";
			columns.Add("PrintSignature3", typeof(bool)).DefaultValue = true;
			columns.Add("Signature3Title", typeof(string)).DefaultValue = "";
			columns.Add("TotalAmountInWordsTitle", typeof(string)).DefaultValue = "The Sum of";
			columns.Add("ColOriginalAmountTitle", typeof(string)).DefaultValue = "Original Amt";
			columns.Add("PrintColOriginalAmount", typeof(bool)).DefaultValue = true;
			columns.Add("ColOriginalAmountAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("ColAmountDueTitle", typeof(string)).DefaultValue = "Amt Due";
			columns.Add("ColAmountDueAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("PrintColAmountDue", typeof(bool)).DefaultValue = true;
			columns.Add("ColPaymentTitle", typeof(string)).DefaultValue = "Payment";
			columns.Add("ColPaymentAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("PrintColPayment", typeof(bool)).DefaultValue = true;
			columns.Add("PrintColDiscount", typeof(bool)).DefaultValue = true;
			columns.Add("ColDiscountTitle", typeof(string)).DefaultValue = "Discount";
			columns.Add("ColDiscountAlign", typeof(byte)).DefaultValue = 3;
			columns.Add("CheckDateTitle", typeof(string)).DefaultValue = "Chk Date";
			columns.Add("PrintCheckDate", typeof(bool)).DefaultValue = true;
			columns.Add("PrintBank", typeof(bool)).DefaultValue = true;
			columns.Add("BankTitle", typeof(string)).DefaultValue = "Bank";
			columns.Add("CheckNumberTitle", typeof(string)).DefaultValue = "Chk#";
			columns.Add("PrintCheckNumber", typeof(bool)).DefaultValue = true;
			columns.Add("PrintAccount", typeof(bool)).DefaultValue = true;
			columns.Add("AccountTitle", typeof(string)).DefaultValue = "Account";
			columns.Add("PrintEmployee", typeof(bool)).DefaultValue = true;
			columns.Add("EmployeeTitle", typeof(string)).DefaultValue = "Employee";
			columns.Add("PrintBeginDate", typeof(bool)).DefaultValue = true;
			columns.Add("BeginDateTitle", typeof(string)).DefaultValue = "Begin Date";
			columns.Add("PrintEndDate", typeof(bool)).DefaultValue = true;
			columns.Add("EndDateTitle", typeof(string)).DefaultValue = "End Date";
			columns.Add("PrintColType", typeof(bool)).DefaultValue = true;
			columns.Add("ColTypeTitle", typeof(string)).DefaultValue = "Type";
			columns.Add("ColTypeAlign", typeof(byte)).DefaultValue = 1;
			columns.Add("ColPayeeTitle", typeof(string)).DefaultValue = "Payee";
			columns.Add("ColPayeeAlign", typeof(byte)).DefaultValue = 1;
			columns.Add("PrintColPayee", typeof(bool)).DefaultValue = true;
			base.Tables.Add(dataTable);
		}
	}
}
