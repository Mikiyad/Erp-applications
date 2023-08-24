using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Text;

namespace Micromind.Data
{
	
	public sealed class PrintTemplates : StoreObject
	{
		private const string TEMPLATEID_PARM = "@TemplateID";

		private const string NAME_PARM = "@Name";

		private const string DATA_PARM = "@Data";

		private const string PRINTBILLTO_PARM = "@PrintBillTo";

		private const string PRINTSHIPTO_PARM = "@PrintShipTo";

		private const string PRINTNUMBER_PARM = "@PrintNumber";

		private const string PRINTCOMPANYHEADER_PARM = "@PrintCompanyHeader";

		private const string PRINTDATE_PARM = "@PrintDate";

		private const string PRINTDOCUMENTTITLE_PARM = "@PrintDocumentTitle";

		private const string BILLTOTITLE_PARM = "@BillToTitle";

		private const string SHIPTOTITLE_PARM = "@ShipToTitle";

		private const string NUMBERTITLE_PARM = "@NumberTitle";

		private const string DATETITLE_PARM = "@DateTitle";

		private const string DOCUMENTTITLE_PARM = "@DocumentTitle";

		private const string PRINTPONO_PARM = "@PrintPONo";

		private const string POTITLE_PARM = "@POTitle";

		private const string PRINTDUEDATE_PARM = "@PrintDueDate";

		private const string DUEDATETITLE_PARM = "@DueDateTitle";

		private const string PRINTSALESREP_PARM = "@PrintSalesRep";

		private const string SALESREPTITLE_PARM = "@SalesRepTitle";

		private const string COLITEMTITLE_PARM = "@ColItemTitle";

		private const string COLDESCRIPTIONTITLE_PARM = "@ColDescriptionTitle";

		private const string COLPRICETITLE_PARM = "@ColPriceTitle";

		private const string COLAMOUNTTITLE_PARM = "@ColAmountTitle";

		private const string PRINTCUSTOMERMESSAGE_PARM = "@PrintCustomerMessage";

		private const string CUSTOMERMESSAGETITLE_PARM = "@CustomerMessageTitle";

		private const string PRINTDISCOUNT_PARM = "@PrintDiscount";

		private const string DISCOUNTTITLE_PARM = "@DiscountTitle";

		private const string PRINTTOTAL_PARM = "@PrintTotal";

		private const string TOTALTITLE_PARM = "@TotalTitle";

		private const string PRINTSUBTOTAL_PARM = "@PrintSubtotal";

		private const string SUBTOTALTITLE_PARM = "@SubtotalTitle";

		private const string PRINTBALANCEDUE_PARM = "@PrintBalanceDue";

		private const string BALANCEDUETITLE_PARM = "@BalanceDueTitle";

		private const string PRINTFREIGHT_PARM = "@PrintFreight";

		private const string FREIGHTTITLE_PARM = "@FreightTitle";

		private const string PRINTMISCELLANEOUS_PARM = "@PrintMiscellaneous";

		private const string MISCELLANEOUSTITLE_PARM = "@MiscellaneousTitle";

		private const string DOCUMENTTYPE_PARM = "@DocumentType";

		private const string ISREADONLY_PARM = "@IsReadOnly";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string PRINTCOMPANYADDRESS_PARM = "@PrintCompanyAddress";

		private const string PRINTADDRESSEMAIL_PARM = "@PrintAddressEmail";

		private const string PRINTADDRESSTELEPHONE_PARM = "@PrintAddressTelephone";

		private const string PRINTADDRESSWEBSITE_PARM = "@PrintAddressWebsite";

		private const string PRINTADDRESSFAX_PARM = "@PrintAddressFax";

		private const string PRINTADDRESSMOBILE_PARM = "@PrintAddressMobile";

		private const string PRINTADDRESS1_PARM = "@PrintAddress1";

		private const string PRINTADDRESS2_PARM = "@PrintAddress2";

		private const string PRINTADDRESS3_PARM = "@PrintAddress3";

		private const string PRINTADDRESSCITY_PARM = "@PrintAddressCity";

		private const string PRINTADDRESSCOUNTRY_PARM = "@PrintAddressCountry";

		private const string PRINTTOTALAMOUNTINWORDS_PARM = "@PrintTotalAmountInWords";

		private const string PRINTSIGNATURE1_PARM = "@PrintSignature1";

		private const string PRINTSIGNATURE2_PARM = "@PrintSignature2";

		private const string PRINTLOGO_PARM = "@PrintLogo";

		private const string PRINTREFERENCE_PARM = "@PrintReference";

		private const string PRINTLOCATION_PARM = "@PrintLocation";

		private const string PRINTCOLNUMBER_PARM = "@PrintColNumber";

		private const string PRINTCOLDESCRIPTION_PARM = "@PrintColDescription";

		private const string PRINTCOLQTY_PARM = "@PrintColQty";

		private const string PRINTCOLPRICE_PARM = "@PrintColPrice";

		private const string PRINTCOLTOTAL_PARM = "@PrintColTotal";

		private const string SIGNATURE1TITLE_PARM = "@Signature1Title";

		private const string SIGNATURE2TITLE_PARM = "@Signature2Title";

		private const string REFERENCETITLE_PARM = "@ReferenceTitle";

		private const string LOCATIONTITLE_PARM = "@LocationTitle";

		private const string COLNUMBERTITLE_PARM = "@ColNumberTitle";

		private const string COLQTYTITLE_PARM = "@ColQtyTitle";

		private const string COLTOTALTITLE_PARM = "@ColTotalTitle";

		private const string COLDATETITLE_PARM = "@ColDateTitle";

		private const string COLDEBITTITLE_PARM = "@ColDebitTitle";

		private const string COLCREDITTITLE_PARM = "@ColCreditTitle";

		private const string COLBALANCETITLE_PARM = "@ColBalanceTitle";

		private const string STATEMENTPERIODTITLE_PARM = "@StatementPeriodTitle";

		private const string STATEMENTAGING1TITLE_PARM = "@StatementAging1Title";

		private const string STATEMENTAGING2TITLE_PARM = "@StatementAging2Title";

		private const string STATEMENTAGING3TITLE_PARM = "@StatementAging3Title";

		private const string STATEMENTAGING4TITLE_PARM = "@StatementAging4Title";

		private const string STATEMENTAGING5TITLE_PARM = "@StatementAging5Title";

		private const string STATEMENTAGING6TITLE_PARM = "@StatementAging6Title";

		private const string STATEMENTAGING7TITLE_PARM = "@StatementAging7Title";

		private const string STATEMENTAGING8TITLE_PARM = "@StatementAging8Title";

		private const string TOTALDEBITTITLE_PARM = "@TotalDebitTitle";

		private const string TOTALCREDITTITLE_PARM = "@TotalCreditTitle";

		private const string BALANCETITLE_PARM = "@BalanceTitle";

		private const string PRINTCOLDATE_PARM = "@PrintColDate";

		private const string PRINTCOLDEBIT_PARM = "@PrintColDebit";

		private const string PRINTCOLCREDIT_PARM = "@PrintColCredit";

		private const string PRINTCOLBALANCE_PARM = "@PrintColBalance";

		private const string PRINTSTATEMENTPERIOD_PARM = "@PrintStatmentPeriod";

		private const string PRINTTOTALDEBIT_PARM = "@PrintTotalDebitTitle";

		private const string PRINTTOTALCREDIT_PARM = "@PrintTotalCreditTotal";

		private const string PRINTBALANCE_PARM = "@PrintBalance";

		private const string PRINTMONTHLYAGING_PARM = "@PrintMonthlyAging";

		private const string PRINTSTATEMENTCHECKS_PARM = "@PrintStatementChecks";

		private const string DEFAULTLANGUAGE_PARM = "@DefaultLanguage";

		private const string PRINTSHIPPER_PARM = "@PrintShipper";

		private const string SHIPPERTITLE_PARM = "@ShipperTitle";

		private const string PHONETITLE_PARM = "@PhoneTitle";

		private const string EMAILTITLE_PARM = "@EmailTitle";

		private const string FAXTITLE_PARM = "@FaxTitle";

		private const string WEBSITETITLE_PARM = "@WebsiteTitle";

		private const string PRINTJOB_PARM = "@PrintJob";

		private const string JOBTITLE_PARM = "@JobTitle";

		private const string PRINTPAYEE_PARM = "@PrintPayee";

		private const string PAYEETITLE_PARM = "@PayeeTitle";

		private const string PRINTPAYEEID_PARM = "@PrintPayeeID";

		private const string PRINTPAYEECOMPANYNAME_PARM = "@PrintPayeeCompanyName";

		private const string PRINTPAYEELASTNAME_PARM = "@PrintPayeeLastName";

		private const string PRINTPAYEMENTMETHOD_PARM = "@PrintPaymentMethod";

		private const string COLBALANCEALIGN_PARM = "@ColBalanceAlign";

		private const string COLCREDITALIGN_PARM = "@ColCreditAlign";

		private const string COLDEBITALIGN_PARM = "@ColDebitAlign";

		private const string COLDATEALIGN_PARM = "@ColDateAlign";

		private const string COLTOTALALIGN_PARM = "@ColTotalAlign";

		private const string COLQTYALIGN_PARM = "@ColQtyAlign";

		private const string COLNUMBERALIGN_PARM = "@ColNumberAlign";

		private const string COLDESCRIPTIONALIGN_PARM = "@ColDescriptionAlign";

		private const string COLPRICEALIGN_PARM = "@ColPriceAlign";

		private const string COLAMOUNTALIGN_PARM = "@ColAmountAlign";

		private const string COLUNITALIGN_PARM = "@ColUnitAlign";

		private const string COLUNITTITLE_PARM = "@ColUnitTitle";

		private const string PRINTCOLUNIT_PARM = "@PrintColUnit";

		private const string EDITION_PARM = "@Edition";

		private const string PRINTCOLAMOUNT_PARM = "@PrintColAmount";

		private const string PAYEEIDTITLE_PARM = "@PayeeIDTitle";

		private const string PAYMENTMETHODTITLE_PARM = "@PaymentMethodTitle";

		private const string PRINTSIGNATURE3_PARM = "@PrintSignature3";

		private const string SIGNATURE3TITLE_PARM = "@Signature3Title";

		private const string TOTALAMOUNTINWORDSTITLE_PARM = "@TotalAmountInWordsTitle";

		private const string COLORIGINALAMOUNTTITLE_PARM = "@ColOriginalAmountTitle";

		private const string PRINTCOLORIGINALAMOUNT_PARM = "@PrintColOriginalAmount";

		private const string COLORIGINALAMOUNTALIGN_PARM = "@ColOriginalAmountAlign";

		private const string COLAMOUNTDUETITLE_PARM = "@ColAmountDueTitle";

		private const string COLAMOUNTDUEALIGN_PARM = "@ColAmountDueAlign";

		private const string PRINTCOLAMOUNTDUE_PARM = "@PrintColAmountDue";

		private const string COLPAYMENTTITLE_PARM = "@ColPaymentTitle";

		private const string COLPAYMENTALIGN_PARM = "@ColPaymentAlign";

		private const string PRINTCOLPAYMENT_PARM = "@PrintColPayment";

		private const string PRINTCOLDISCOUNT_PARM = "@PrintColDiscount";

		private const string COLDISCOUNTTITLE_PARM = "@ColDiscountTitle";

		private const string COLDISCOUNTALIGN_PARM = "@ColDiscountAlign";

		private const string CHECKDATETITLE_PARM = "@CheckDateTitle";

		private const string PRINTCHECKDATE_PARM = "@PrintCheckDate";

		private const string PRINTBANK_PARM = "@PrintBank";

		private const string BANKTITLE_PARM = "@BankTitle";

		private const string CHECKNUMBERTITLE_PARM = "@CheckNumberTitle";

		private const string PRINTCHECKNUMBER_PARM = "@PrintCheckNumber";

		private const string PRINTACCOUNT_PARM = "@PrintAccount";

		private const string ACCOUNTTITLE_PARM = "@AccountTitle";

		private const string PRINTEMPLOYEE_PARM = "@PrintEmployee";

		private const string EMPLOYEETITLE_PARM = "@EmployeeTitle";

		private const string PRINTBEGINDATE_PARM = "@PrintBeginDate";

		private const string BEGINDATETITLE_PARM = "@BeginDateTitle";

		private const string PRINTENDDATE_PARM = "@PrintEndDate";

		private const string ENDDATETITLE_PARM = "@EndDateTitle";

		private const string PRINTCOLTYPE_PARM = "@PrintColType";

		private const string COLTYPETITLE_PARM = "@ColTypeTitle";

		private const string COLTYPEALIGN_PARM = "@ColTypeAlign";

		private const string COLPAYEETITLE_PARM = "@ColPayeeTitle";

		private const string COLPAYEEALIGN_PARM = "@ColPayeeAlign";

		private const string PRINTCOLPAYEE_PARM = "@PrintColPayee";

		public PrintTemplates(Config config)
			: base(config)
		{
		}

		private SqlBuilder GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Print Templates]", new FieldValue("Name", "@Name"), new FieldValue("Data", "@Data"), new FieldValue("PrintBillTo", "@PrintBillTo"), new FieldValue("PrintShipTo", "@PrintShipTo"), new FieldValue("PrintNumber", "@PrintNumber"), new FieldValue("PrintCompanyHeader", "@PrintCompanyHeader"), new FieldValue("PrintDate", "@PrintDate"), new FieldValue("PrintDocumentTitle", "@PrintDocumentTitle"), new FieldValue("BillToTitle", "@BillToTitle"), new FieldValue("ShipToTitle", "@ShipToTitle"), new FieldValue("NumberTitle", "@NumberTitle"), new FieldValue("DateTitle", "@DateTitle"), new FieldValue("DocumentTitle", "@DocumentTitle"), new FieldValue("PrintPONo", "@PrintPONo"), new FieldValue("POTitle", "@POTitle"), new FieldValue("PrintDueDate", "@PrintDueDate"), new FieldValue("DueDateTitle", "@DueDateTitle"), new FieldValue("PrintSalesRep", "@PrintSalesRep"), new FieldValue("SalesRepTitle", "@SalesRepTitle"), new FieldValue("ColDescriptionTitle", "@ColDescriptionTitle"), new FieldValue("ColPriceTitle", "@ColPriceTitle"), new FieldValue("ColAmountTitle", "@ColAmountTitle"), new FieldValue("PrintCustomerMessage", "@PrintCustomerMessage"), new FieldValue("CustomerMessageTitle", "@CustomerMessageTitle"), new FieldValue("PrintDiscount", "@PrintDiscount"), new FieldValue("DiscountTitle", "@DiscountTitle"), new FieldValue("PrintTotal", "@PrintTotal"), new FieldValue("TotalTitle", "@TotalTitle"), new FieldValue("PrintSubtotal", "@PrintSubtotal"), new FieldValue("SubtotalTitle", "@SubtotalTitle"), new FieldValue("PrintBalanceDue", "@PrintBalanceDue"), new FieldValue("BalanceDueTitle", "@BalanceDueTitle"), new FieldValue("PrintFreight", "@PrintFreight"), new FieldValue("FreightTitle", "@FreightTitle"), new FieldValue("PrintMiscellaneous", "@PrintMiscellaneous"), new FieldValue("MiscellaneousTitle", "@MiscellaneousTitle"), new FieldValue("DocumentType", "@DocumentType"), new FieldValue("IsReadOnly", "@IsReadOnly"), new FieldValue("PrintCompanyAddress", "@PrintCompanyAddress"), new FieldValue("PrintAddressEmail", "@PrintAddressEmail"), new FieldValue("PrintAddressTelephone", "@PrintAddressTelephone"), new FieldValue("PrintAddressWebsite", "@PrintAddressWebsite"), new FieldValue("PrintAddressFax", "@PrintAddressFax"), new FieldValue("PrintAddressMobile", "@PrintAddressMobile"), new FieldValue("PrintAddress1", "@PrintAddress1"), new FieldValue("PrintAddress2", "@PrintAddress2"), new FieldValue("PrintAddress3", "@PrintAddress3"), new FieldValue("PrintAddressCity", "@PrintAddressCity"), new FieldValue("PrintAddressCountry", "@PrintAddressCountry"), new FieldValue("PrintTotalAmountInWords", "@PrintTotalAmountInWords"), new FieldValue("PrintSignature1", "@PrintSignature1"), new FieldValue("PrintSignature2", "@PrintSignature2"), new FieldValue("PrintLogo", "@PrintLogo"), new FieldValue("PrintReference", "@PrintReference"), new FieldValue("PrintLocation", "@PrintLocation"), new FieldValue("PrintColNumber", "@PrintColNumber"), new FieldValue("PrintColDescription", "@PrintColDescription"), new FieldValue("PrintColQty", "@PrintColQty"), new FieldValue("PrintColPrice", "@PrintColPrice"), new FieldValue("PrintColTotal", "@PrintColTotal"), new FieldValue("Signature1Title", "@Signature1Title"), new FieldValue("Signature2Title", "@Signature2Title"), new FieldValue("ReferenceTitle", "@ReferenceTitle"), new FieldValue("LocationTitle", "@LocationTitle"), new FieldValue("ColNumberTitle", "@ColNumberTitle"), new FieldValue("ColQtyTitle", "@ColQtyTitle"), new FieldValue("ColTotalTitle", "@ColTotalTitle"), new FieldValue("ColDateTitle", "@ColDateTitle"), new FieldValue("ColDebitTitle", "@ColDebitTitle"), new FieldValue("ColCreditTitle", "@ColCreditTitle"), new FieldValue("ColBalanceTitle", "@ColBalanceTitle"), new FieldValue("StatementPeriodTitle", "@StatementPeriodTitle"), new FieldValue("StatementAging1Title", "@StatementAging1Title"), new FieldValue("StatementAging2Title", "@StatementAging2Title"), new FieldValue("StatementAging3Title", "@StatementAging3Title"), new FieldValue("StatementAging4Title", "@StatementAging4Title"), new FieldValue("StatementAging5Title", "@StatementAging5Title"), new FieldValue("StatementAging6Title", "@StatementAging6Title"), new FieldValue("StatementAging7Title", "@StatementAging7Title"), new FieldValue("StatementAging8Title", "@StatementAging8Title"), new FieldValue("TotalDebitTitle", "@TotalDebitTitle"), new FieldValue("TotalCreditTitle", "@TotalCreditTitle"), new FieldValue("BalanceTitle", "@BalanceTitle"), new FieldValue("PrintColDate", "@PrintColDate"), new FieldValue("PrintColDEbit", "@PrintColDebit"), new FieldValue("PrintColCredit", "@PrintColCredit"), new FieldValue("PrintColBalance", "@PrintColBalance"), new FieldValue("PrintStatementPeriod", "@PrintStatmentPeriod"), new FieldValue("PrintTotalDebit", "@PrintTotalDebitTitle"), new FieldValue("PrintTotalCredit", "@PrintTotalCreditTotal"), new FieldValue("PrintBalance", "@PrintBalance"), new FieldValue("PrintMonthlyAging", "@PrintMonthlyAging"), new FieldValue("PrintStatementChecks", "@PrintStatementChecks"), new FieldValue("DefaultLanguage", "@DefaultLanguage"), new FieldValue("PrintShipper", "@PrintShipper"), new FieldValue("ShipperTitle", "@ShipperTitle"), new FieldValue("PhoneTitle", "@PhoneTitle"), new FieldValue("EmailTitle", "@EmailTitle"), new FieldValue("FaxTitle", "@FaxTitle"), new FieldValue("WebsiteTitle", "@WebsiteTitle"), new FieldValue("PrintJob", "@PrintJob"), new FieldValue("JobTitle", "@JobTitle"), new FieldValue("PrintPayee", "@PrintPayee"), new FieldValue("PayeeTitle", "@PayeeTitle"), new FieldValue("PrintPayeeID", "@PrintPayeeID"), new FieldValue("PrintPayeeCompanyName", "@PrintPayeeCompanyName"), new FieldValue("PrintPayeeLastName", "@PrintPayeeLastName"), new FieldValue("PrintPaymentMethod", "@PrintPaymentMethod"), new FieldValue("ColBalanceAlign", "@ColBalanceAlign"), new FieldValue("ColCreditAlign", "@ColCreditAlign"), new FieldValue("ColDateAlign", "@ColDateAlign"), new FieldValue("ColDebitAlign", "@ColDebitAlign"), new FieldValue("ColDescriptionAlign", "@ColDescriptionAlign"), new FieldValue("ColNumberAlign", "@ColNumberAlign"), new FieldValue("ColPriceAlign", "@ColPriceAlign"), new FieldValue("ColQtyAlign", "@ColQtyAlign"), new FieldValue("ColTotalAlign", "@ColTotalAlign"), new FieldValue("ColUnitAlign", "@ColUnitAlign"), new FieldValue("ColAmountAlign", "@ColAmountAlign"), new FieldValue("ColUnitTitle", "@ColUnitTitle"), new FieldValue("PrintColUnit", "@PrintColUnit"), new FieldValue("Edition", "@Edition"), new FieldValue("PrintColAmount", "@PrintColAmount"), new FieldValue("PayeeIDTitle", "@PayeeIDTitle"), new FieldValue("PaymentMethodTitle", "@PaymentMethodTitle"), new FieldValue("PrintSignature3", "@PrintSignature3"), new FieldValue("Signature3Title", "@Signature3Title"), new FieldValue("TotalAmountInWordsTitle", "@TotalAmountInWordsTitle"), new FieldValue("ColOriginalAmountTitle", "@ColOriginalAmountTitle"), new FieldValue("PrintColOriginalAmount", "@PrintColOriginalAmount"), new FieldValue("ColOriginalAmountAlign", "@ColOriginalAmountAlign"), new FieldValue("ColAmountDueTitle", "@ColAmountDueTitle"), new FieldValue("ColAmountDueAlign", "@ColAmountDueAlign"), new FieldValue("PrintColAmountDue", "@PrintColAmountDue"), new FieldValue("ColPaymentTitle", "@ColPaymentTitle"), new FieldValue("ColPaymentAlign", "@ColPaymentAlign"), new FieldValue("PrintColPayment", "@PrintColPayment"), new FieldValue("PrintColDiscount", "@PrintColDiscount"), new FieldValue("ColDiscountTitle", "@ColDiscountTitle"), new FieldValue("ColDiscountAlign", "@ColDiscountAlign"), new FieldValue("CheckDateTitle", "@CheckDateTitle"), new FieldValue("PrintCheckDate", "@PrintCheckDate"), new FieldValue("PrintBank", "@PrintBank"), new FieldValue("BankTitle", "@BankTitle"), new FieldValue("CheckNumberTitle", "@CheckNumberTitle"), new FieldValue("PrintCheckNumber", "@PrintCheckNumber"), new FieldValue("PrintAccount", "@PrintAccount"), new FieldValue("AccountTitle", "@AccountTitle"), new FieldValue("PrintEmployee", "@PrintEmployee"), new FieldValue("EmployeeTitle", "@EmployeeTitle"), new FieldValue("PrintBeginDate", "@PrintBeginDate"), new FieldValue("BeginDateTitle", "@BeginDateTitle"), new FieldValue("PrintEndDate", "@PrintEndDate"), new FieldValue("EndDateTitle", "@EndDateTitle"), new FieldValue("PrintColType", "@PrintColType"), new FieldValue("ColTypeTitle", "@ColTypeTitle"), new FieldValue("ColTypeAlign", "@ColTypeAlign"), new FieldValue("ColPayeeTitle", "@ColPayeeTitle"), new FieldValue("ColPayeeAlign", "@ColPayeeAlign"), new FieldValue("PrintColPayee", "@PrintColPayee"));
			return sqlBuilder;
		}

		private SqlBuilder GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Print Templates]", new FieldValue("TemplateID", "@TemplateID", isUpdateConditionField: true), new FieldValue("Name", "@Name"), new FieldValue("Data", "@Data"), new FieldValue("PrintBillTo", "@PrintBillTo"), new FieldValue("PrintShipTo", "@PrintShipTo"), new FieldValue("PrintNumber", "@PrintNumber"), new FieldValue("PrintCompanyHeader", "@PrintCompanyHeader"), new FieldValue("PrintDate", "@PrintDate"), new FieldValue("PrintDocumentTitle", "@PrintDocumentTitle"), new FieldValue("BillToTitle", "@BillToTitle"), new FieldValue("ShipToTitle", "@ShipToTitle"), new FieldValue("NumberTitle", "@NumberTitle"), new FieldValue("DateTitle", "@DateTitle"), new FieldValue("DocumentTitle", "@DocumentTitle"), new FieldValue("PrintPONo", "@PrintPONo"), new FieldValue("POTitle", "@POTitle"), new FieldValue("PrintDueDate", "@PrintDueDate"), new FieldValue("DueDateTitle", "@DueDateTitle"), new FieldValue("PrintSalesRep", "@PrintSalesRep"), new FieldValue("SalesRepTitle", "@SalesRepTitle"), new FieldValue("ColDescriptionTitle", "@ColDescriptionTitle"), new FieldValue("ColPriceTitle", "@ColPriceTitle"), new FieldValue("ColAmountTitle", "@ColAmountTitle"), new FieldValue("PrintCustomerMessage", "@PrintCustomerMessage"), new FieldValue("CustomerMessageTitle", "@CustomerMessageTitle"), new FieldValue("PrintDiscount", "@PrintDiscount"), new FieldValue("DiscountTitle", "@DiscountTitle"), new FieldValue("PrintTotal", "@PrintTotal"), new FieldValue("TotalTitle", "@TotalTitle"), new FieldValue("PrintSubtotal", "@PrintSubtotal"), new FieldValue("SubtotalTitle", "@SubtotalTitle"), new FieldValue("PrintBalanceDue", "@PrintBalanceDue"), new FieldValue("BalanceDueTitle", "@BalanceDueTitle"), new FieldValue("PrintFreight", "@PrintFreight"), new FieldValue("FreightTitle", "@FreightTitle"), new FieldValue("PrintMiscellaneous", "@PrintMiscellaneous"), new FieldValue("MiscellaneousTitle", "@MiscellaneousTitle"), new FieldValue("DocumentType", "@DocumentType"), new FieldValue("IsReadOnly", "@IsReadOnly"), new FieldValue("PrintCompanyAddress", "@PrintCompanyAddress"), new FieldValue("PrintAddressEmail", "@PrintAddressEmail"), new FieldValue("PrintAddressTelephone", "@PrintAddressTelephone"), new FieldValue("PrintAddressWebsite", "@PrintAddressWebsite"), new FieldValue("PrintAddressFax", "@PrintAddressFax"), new FieldValue("PrintAddressMobile", "@PrintAddressMobile"), new FieldValue("PrintAddress1", "@PrintAddress1"), new FieldValue("PrintAddress2", "@PrintAddress2"), new FieldValue("PrintAddress3", "@PrintAddress3"), new FieldValue("PrintAddressCity", "@PrintAddressCity"), new FieldValue("PrintAddressCountry", "@PrintAddressCountry"), new FieldValue("PrintTotalAmountInWords", "@PrintTotalAmountInWords"), new FieldValue("PrintSignature1", "@PrintSignature1"), new FieldValue("PrintSignature2", "@PrintSignature2"), new FieldValue("PrintLogo", "@PrintLogo"), new FieldValue("PrintReference", "@PrintReference"), new FieldValue("PrintLocation", "@PrintLocation"), new FieldValue("PrintColNumber", "@PrintColNumber"), new FieldValue("PrintColDescription", "@PrintColDescription"), new FieldValue("PrintColQty", "@PrintColQty"), new FieldValue("PrintColPrice", "@PrintColPrice"), new FieldValue("PrintColTotal", "@PrintColTotal"), new FieldValue("Signature1Title", "@Signature1Title"), new FieldValue("Signature2Title", "@Signature2Title"), new FieldValue("ReferenceTitle", "@ReferenceTitle"), new FieldValue("LocationTitle", "@LocationTitle"), new FieldValue("ColNumberTitle", "@ColNumberTitle"), new FieldValue("ColQtyTitle", "@ColQtyTitle"), new FieldValue("ColTotalTitle", "@ColTotalTitle"), new FieldValue("ColDateTitle", "@ColDateTitle"), new FieldValue("ColDebitTitle", "@ColDebitTitle"), new FieldValue("ColCreditTitle", "@ColCreditTitle"), new FieldValue("ColBalanceTitle", "@ColBalanceTitle"), new FieldValue("StatementPeriodTitle", "@StatementPeriodTitle"), new FieldValue("StatementAging1Title", "@StatementAging1Title"), new FieldValue("StatementAging2Title", "@StatementAging2Title"), new FieldValue("StatementAging3Title", "@StatementAging3Title"), new FieldValue("StatementAging4Title", "@StatementAging4Title"), new FieldValue("StatementAging5Title", "@StatementAging5Title"), new FieldValue("StatementAging6Title", "@StatementAging6Title"), new FieldValue("StatementAging7Title", "@StatementAging7Title"), new FieldValue("StatementAging8Title", "@StatementAging8Title"), new FieldValue("TotalDebitTitle", "@TotalDebitTitle"), new FieldValue("TotalCreditTitle", "@TotalCreditTitle"), new FieldValue("BalanceTitle", "@BalanceTitle"), new FieldValue("PrintColDate", "@PrintColDate"), new FieldValue("PrintColDEbit", "@PrintColDebit"), new FieldValue("PrintColCredit", "@PrintColCredit"), new FieldValue("PrintColBalance", "@PrintColBalance"), new FieldValue("PrintStatementPeriod", "@PrintStatmentPeriod"), new FieldValue("PrintTotalDebit", "@PrintTotalDebitTitle"), new FieldValue("PrintTotalCredit", "@PrintTotalCreditTotal"), new FieldValue("PrintBalance", "@PrintBalance"), new FieldValue("PrintMonthlyAging", "@PrintMonthlyAging"), new FieldValue("PrintStatementChecks", "@PrintStatementChecks"), new FieldValue("DefaultLanguage", "@DefaultLanguage"), new FieldValue("PrintShipper", "@PrintShipper"), new FieldValue("ShipperTitle", "@ShipperTitle"), new FieldValue("PhoneTitle", "@PhoneTitle"), new FieldValue("EmailTitle", "@EmailTitle"), new FieldValue("FaxTitle", "@FaxTitle"), new FieldValue("WebsiteTitle", "@WebsiteTitle"), new FieldValue("PrintJob", "@PrintJob"), new FieldValue("JobTitle", "@JobTitle"), new FieldValue("PrintPayee", "@PrintPayee"), new FieldValue("PayeeTitle", "@PayeeTitle"), new FieldValue("PrintPayeeID", "@PrintPayeeID"), new FieldValue("PrintPayeeCompanyName", "@PrintPayeeCompanyName"), new FieldValue("PrintPayeeLastName", "@PrintPayeeLastName"), new FieldValue("PrintPaymentMethod", "@PrintPaymentMethod"), new FieldValue("ColBalanceAlign", "@ColBalanceAlign"), new FieldValue("ColCreditAlign", "@ColCreditAlign"), new FieldValue("ColDateAlign", "@ColDateAlign"), new FieldValue("ColDebitAlign", "@ColDebitAlign"), new FieldValue("ColDescriptionAlign", "@ColDescriptionAlign"), new FieldValue("ColNumberAlign", "@ColNumberAlign"), new FieldValue("ColPriceAlign", "@ColPriceAlign"), new FieldValue("ColQtyAlign", "@ColQtyAlign"), new FieldValue("ColTotalAlign", "@ColTotalAlign"), new FieldValue("ColUnitAlign", "@ColUnitAlign"), new FieldValue("ColAmountAlign", "@ColAmountAlign"), new FieldValue("ColUnitTitle", "@ColUnitTitle"), new FieldValue("PrintColUnit", "@PrintColUnit"), new FieldValue("Edition", "@Edition"), new FieldValue("PrintColAmount", "@PrintColAmount"), new FieldValue("PayeeIDTitle", "@PayeeIDTitle"), new FieldValue("PaymentMethodTitle", "@PaymentMethodTitle"), new FieldValue("PrintSignature3", "@PrintSignature3"), new FieldValue("Signature3Title", "@Signature3Title"), new FieldValue("TotalAmountInWordsTitle", "@TotalAmountInWordsTitle"), new FieldValue("ColOriginalAmountTitle", "@ColOriginalAmountTitle"), new FieldValue("PrintColOriginalAmount", "@PrintColOriginalAmount"), new FieldValue("ColOriginalAmountAlign", "@ColOriginalAmountAlign"), new FieldValue("ColAmountDueTitle", "@ColAmountDueTitle"), new FieldValue("ColAmountDueAlign", "@ColAmountDueAlign"), new FieldValue("PrintColAmountDue", "@PrintColAmountDue"), new FieldValue("ColPaymentTitle", "@ColPaymentTitle"), new FieldValue("ColPaymentAlign", "@ColPaymentAlign"), new FieldValue("PrintColPayment", "@PrintColPayment"), new FieldValue("PrintColDiscount", "@PrintColDiscount"), new FieldValue("ColDiscountTitle", "@ColDiscountTitle"), new FieldValue("ColDiscountAlign", "@ColDiscountAlign"), new FieldValue("CheckDateTitle", "@CheckDateTitle"), new FieldValue("PrintCheckDate", "@PrintCheckDate"), new FieldValue("PrintBank", "@PrintBank"), new FieldValue("BankTitle", "@BankTitle"), new FieldValue("CheckNumberTitle", "@CheckNumberTitle"), new FieldValue("PrintCheckNumber", "@PrintCheckNumber"), new FieldValue("PrintAccount", "@PrintAccount"), new FieldValue("AccountTitle", "@AccountTitle"), new FieldValue("PrintEmployee", "@PrintEmployee"), new FieldValue("EmployeeTitle", "@EmployeeTitle"), new FieldValue("PrintBeginDate", "@PrintBeginDate"), new FieldValue("BeginDateTitle", "@BeginDateTitle"), new FieldValue("PrintEndDate", "@PrintEndDate"), new FieldValue("EndDateTitle", "@EndDateTitle"), new FieldValue("PrintColType", "@PrintColType"), new FieldValue("ColTypeTitle", "@ColTypeTitle"), new FieldValue("ColTypeAlign", "@ColTypeAlign"), new FieldValue("ColPayeeTitle", "@ColPayeeTitle"), new FieldValue("ColPayeeAlign", "@ColPayeeAlign"), new FieldValue("PrintColPayee", "@PrintColPayee"), new FieldValue("DateTimeStamp", "@DateTimeStamp", isUpdateConditionField: true, checkForNullValue: true));
			return sqlBuilder;
		}

		private SqlCommand GetInsertCommand()
		{
			insertCommand = null;
			SqlBuilder insertText = GetInsertText();
			insertCommand = new SqlCommand(insertText.GetInsertExpression(), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@Name", SqlDbType.NVarChar);
			parameters.Add("@Data", SqlDbType.NText);
			parameters.Add("@PrintBillTo", SqlDbType.Bit);
			parameters.Add("@PrintShipTo", SqlDbType.Bit);
			parameters.Add("@PrintNumber", SqlDbType.Bit);
			parameters.Add("@PrintCompanyHeader", SqlDbType.Bit);
			parameters.Add("@PrintDate", SqlDbType.Bit);
			parameters.Add("@PrintDocumentTitle", SqlDbType.Bit);
			parameters.Add("@BillToTitle", SqlDbType.NVarChar);
			parameters.Add("@ShipToTitle", SqlDbType.NVarChar);
			parameters.Add("@NumberTitle", SqlDbType.NVarChar);
			parameters.Add("@DateTitle", SqlDbType.NVarChar);
			parameters.Add("@DocumentTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintDueDate", SqlDbType.Bit);
			parameters.Add("@PrintPONo", SqlDbType.Bit);
			parameters.Add("@POTitle", SqlDbType.NVarChar);
			parameters.Add("@DueDateTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintSalesRep", SqlDbType.Bit);
			parameters.Add("@SalesRepTitle", SqlDbType.NVarChar);
			parameters.Add("@ColDescriptionTitle", SqlDbType.NVarChar);
			parameters.Add("@ColPriceTitle", SqlDbType.NVarChar);
			parameters.Add("@ColAmountTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintCustomerMessage", SqlDbType.Bit);
			parameters.Add("@CustomerMessageTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintDiscount", SqlDbType.Bit);
			parameters.Add("@DiscountTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintTotal", SqlDbType.Bit);
			parameters.Add("@TotalTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintSubtotal", SqlDbType.Bit);
			parameters.Add("@SubtotalTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintBalanceDue", SqlDbType.Bit);
			parameters.Add("@BalanceDueTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintFreight", SqlDbType.Bit);
			parameters.Add("@FreightTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintMiscellaneous", SqlDbType.Bit);
			parameters.Add("@MiscellaneousTitle", SqlDbType.NVarChar);
			parameters.Add("@DocumentType", SqlDbType.TinyInt);
			parameters.Add("@IsReadOnly", SqlDbType.Bit);
			parameters.Add("@PrintCompanyAddress", SqlDbType.Bit);
			parameters.Add("@PrintAddressEmail", SqlDbType.Bit);
			parameters.Add("@PrintAddressTelephone", SqlDbType.Bit);
			parameters.Add("@PrintAddressWebsite", SqlDbType.Bit);
			parameters.Add("@PrintAddressFax", SqlDbType.Bit);
			parameters.Add("@PrintAddressMobile", SqlDbType.Bit);
			parameters.Add("@PrintAddress1", SqlDbType.Bit);
			parameters.Add("@PrintAddress2", SqlDbType.Bit);
			parameters.Add("@PrintAddress3", SqlDbType.Bit);
			parameters.Add("@PrintAddressCity", SqlDbType.Bit);
			parameters.Add("@PrintAddressCountry", SqlDbType.Bit);
			parameters.Add("@PrintTotalAmountInWords", SqlDbType.Bit);
			parameters.Add("@PrintSignature1", SqlDbType.Bit);
			parameters.Add("@PrintSignature2", SqlDbType.Bit);
			parameters.Add("@PrintLogo", SqlDbType.Bit);
			parameters.Add("@PrintReference", SqlDbType.Bit);
			parameters.Add("@PrintLocation", SqlDbType.Bit);
			parameters.Add("@PrintColNumber", SqlDbType.Bit);
			parameters.Add("@PrintColDescription", SqlDbType.Bit);
			parameters.Add("@PrintColQty", SqlDbType.Bit);
			parameters.Add("@PrintColPrice", SqlDbType.Bit);
			parameters.Add("@PrintColTotal", SqlDbType.Bit);
			parameters.Add("@Signature1Title", SqlDbType.NVarChar);
			parameters.Add("@Signature2Title", SqlDbType.NVarChar);
			parameters.Add("@ReferenceTitle", SqlDbType.NVarChar);
			parameters.Add("@LocationTitle", SqlDbType.NVarChar);
			parameters.Add("@ColNumberTitle", SqlDbType.NVarChar);
			parameters.Add("@ColQtyTitle", SqlDbType.NVarChar);
			parameters.Add("@ColTotalTitle", SqlDbType.NVarChar);
			parameters.Add("@ColDateTitle", SqlDbType.NVarChar);
			parameters.Add("@ColDebitTitle", SqlDbType.NVarChar);
			parameters.Add("@ColCreditTitle", SqlDbType.NVarChar);
			parameters.Add("@ColBalanceTitle", SqlDbType.NVarChar);
			parameters.Add("@StatementPeriodTitle", SqlDbType.NVarChar);
			parameters.Add("@StatementAging1Title", SqlDbType.NVarChar);
			parameters.Add("@StatementAging2Title", SqlDbType.NVarChar);
			parameters.Add("@StatementAging3Title", SqlDbType.NVarChar);
			parameters.Add("@StatementAging4Title", SqlDbType.NVarChar);
			parameters.Add("@StatementAging5Title", SqlDbType.NVarChar);
			parameters.Add("@StatementAging6Title", SqlDbType.NVarChar);
			parameters.Add("@StatementAging7Title", SqlDbType.NVarChar);
			parameters.Add("@StatementAging8Title", SqlDbType.NVarChar);
			parameters.Add("@TotalDebitTitle", SqlDbType.NVarChar);
			parameters.Add("@TotalCreditTitle", SqlDbType.NVarChar);
			parameters.Add("@BalanceTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintColDate", SqlDbType.Bit);
			parameters.Add("@PrintColDebit", SqlDbType.Bit);
			parameters.Add("@PrintColCredit", SqlDbType.Bit);
			parameters.Add("@PrintColBalance", SqlDbType.Bit);
			parameters.Add("@PrintStatmentPeriod", SqlDbType.Bit);
			parameters.Add("@PrintTotalDebitTitle", SqlDbType.Bit);
			parameters.Add("@PrintTotalCreditTotal", SqlDbType.Bit);
			parameters.Add("@PrintBalance", SqlDbType.Bit);
			parameters.Add("@PrintMonthlyAging", SqlDbType.Bit);
			parameters.Add("@PrintStatementChecks", SqlDbType.Bit);
			parameters.Add("@DefaultLanguage", SqlDbType.TinyInt);
			parameters.Add("@PrintShipper", SqlDbType.Bit);
			parameters.Add("@ShipperTitle", SqlDbType.NVarChar);
			parameters.Add("@PhoneTitle", SqlDbType.NVarChar);
			parameters.Add("@EmailTitle", SqlDbType.NVarChar);
			parameters.Add("@FaxTitle", SqlDbType.NVarChar);
			parameters.Add("@WebsiteTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintJob", SqlDbType.Bit);
			parameters.Add("@JobTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintPayee", SqlDbType.Bit);
			parameters.Add("@PayeeTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintPayeeID", SqlDbType.Bit);
			parameters.Add("@PrintPayeeCompanyName", SqlDbType.Bit);
			parameters.Add("@PrintPayeeLastName", SqlDbType.Bit);
			parameters.Add("@PrintPaymentMethod", SqlDbType.Bit);
			parameters.Add("@ColBalanceAlign", SqlDbType.TinyInt);
			parameters.Add("@ColCreditAlign", SqlDbType.TinyInt);
			parameters.Add("@ColDateAlign", SqlDbType.TinyInt);
			parameters.Add("@ColDebitAlign", SqlDbType.TinyInt);
			parameters.Add("@ColDescriptionAlign", SqlDbType.TinyInt);
			parameters.Add("@ColNumberAlign", SqlDbType.TinyInt);
			parameters.Add("@ColPriceAlign", SqlDbType.TinyInt);
			parameters.Add("@ColQtyAlign", SqlDbType.TinyInt);
			parameters.Add("@ColTotalAlign", SqlDbType.TinyInt);
			parameters.Add("@ColUnitAlign", SqlDbType.TinyInt);
			parameters.Add("@ColAmountAlign", SqlDbType.TinyInt);
			parameters.Add("@ColUnitTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintColUnit", SqlDbType.Bit);
			parameters.Add("@Edition", SqlDbType.TinyInt);
			parameters.Add("@PrintColAmount", SqlDbType.Bit);
			parameters.Add("@PayeeIDTitle", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintSignature3", SqlDbType.Bit);
			parameters.Add("@Signature3Title", SqlDbType.NVarChar);
			parameters.Add("@TotalAmountInWordsTitle", SqlDbType.NVarChar);
			parameters.Add("@ColOriginalAmountTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintColOriginalAmount", SqlDbType.Bit);
			parameters.Add("@ColOriginalAmountAlign", SqlDbType.TinyInt);
			parameters.Add("@ColAmountDueTitle", SqlDbType.NVarChar);
			parameters.Add("@ColAmountDueAlign", SqlDbType.TinyInt);
			parameters.Add("@PrintColAmountDue", SqlDbType.Bit);
			parameters.Add("@ColPaymentTitle", SqlDbType.NVarChar);
			parameters.Add("@ColPaymentAlign", SqlDbType.TinyInt);
			parameters.Add("@PrintColPayment", SqlDbType.Bit);
			parameters.Add("@PrintColDiscount", SqlDbType.Bit);
			parameters.Add("@ColDiscountTitle", SqlDbType.NVarChar);
			parameters.Add("@ColDiscountAlign", SqlDbType.TinyInt);
			parameters.Add("@CheckDateTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintCheckDate", SqlDbType.Bit);
			parameters.Add("@PrintBank", SqlDbType.Bit);
			parameters.Add("@BankTitle", SqlDbType.NVarChar);
			parameters.Add("@CheckNumberTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintCheckNumber", SqlDbType.Bit);
			parameters.Add("@PrintAccount", SqlDbType.Bit);
			parameters.Add("@AccountTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintEmployee", SqlDbType.Bit);
			parameters.Add("@EmployeeTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintBeginDate", SqlDbType.Bit);
			parameters.Add("@BeginDateTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintEndDate", SqlDbType.Bit);
			parameters.Add("@EndDateTitle", SqlDbType.NVarChar);
			parameters.Add("@PrintColType", SqlDbType.Bit);
			parameters.Add("@ColTypeTitle", SqlDbType.NVarChar);
			parameters.Add("@ColTypeAlign", SqlDbType.TinyInt);
			parameters.Add("@ColPayeeTitle", SqlDbType.NVarChar);
			parameters.Add("@ColPayeeAlign", SqlDbType.TinyInt);
			parameters.Add("@PrintColPayee", SqlDbType.Bit);
			parameters["@Name"].SourceColumn = "Name";
			parameters["@Data"].SourceColumn = "Data";
			parameters["@PrintBillTo"].SourceColumn = "PrintBillTo";
			parameters["@PrintShipTo"].SourceColumn = "PrintShipTo";
			parameters["@PrintNumber"].SourceColumn = "PrintNumber";
			parameters["@PrintCompanyHeader"].SourceColumn = "PrintCompanyHeader";
			parameters["@PrintDate"].SourceColumn = "PrintDate";
			parameters["@PrintDocumentTitle"].SourceColumn = "PrintDocumentTitle";
			parameters["@BillToTitle"].SourceColumn = "BillToTitle";
			parameters["@ShipToTitle"].SourceColumn = "ShipToTitle";
			parameters["@NumberTitle"].SourceColumn = "NumberTitle";
			parameters["@DateTitle"].SourceColumn = "DateTitle";
			parameters["@DocumentTitle"].SourceColumn = "DocumentTitle";
			parameters["@PrintPONo"].SourceColumn = "PrintPONo";
			parameters["@POTitle"].SourceColumn = "POTitle";
			parameters["@PrintDueDate"].SourceColumn = "PrintDueDate";
			parameters["@DueDateTitle"].SourceColumn = "DueDateTitle";
			parameters["@PrintSalesRep"].SourceColumn = "PrintSalesRep";
			parameters["@SalesRepTitle"].SourceColumn = "SalesRepTitle";
			parameters["@ColDescriptionTitle"].SourceColumn = "ColDescriptionTitle";
			parameters["@ColPriceTitle"].SourceColumn = "ColPriceTitle";
			parameters["@ColAmountTitle"].SourceColumn = "ColAmountTitle";
			parameters["@PrintCustomerMessage"].SourceColumn = "PrintCustomerMessage";
			parameters["@CustomerMessageTitle"].SourceColumn = "CustomerMessageTitle";
			parameters["@PrintDiscount"].SourceColumn = "PrintDiscount";
			parameters["@DiscountTitle"].SourceColumn = "DiscountTitle";
			parameters["@PrintTotal"].SourceColumn = "PrintTotal";
			parameters["@TotalTitle"].SourceColumn = "TotalTitle";
			parameters["@PrintSubtotal"].SourceColumn = "PrintSubtotal";
			parameters["@SubtotalTitle"].SourceColumn = "SubtotalTitle";
			parameters["@PrintBalanceDue"].SourceColumn = "PrintBalanceDue";
			parameters["@BalanceDueTitle"].SourceColumn = "BalanceDueTitle";
			parameters["@PrintFreight"].SourceColumn = "PrintFreight";
			parameters["@FreightTitle"].SourceColumn = "FreightTitle";
			parameters["@PrintMiscellaneous"].SourceColumn = "PrintMiscellaneous";
			parameters["@MiscellaneousTitle"].SourceColumn = "MiscellaneousTitle";
			parameters["@DocumentType"].SourceColumn = "DocumentType";
			parameters["@IsReadOnly"].SourceColumn = "IsReadOnly";
			parameters["@PrintCompanyAddress"].SourceColumn = "PrintCompanyAddress";
			parameters["@PrintAddressEmail"].SourceColumn = "PrintAddressEmail";
			parameters["@PrintAddressTelephone"].SourceColumn = "PrintAddressTelephone";
			parameters["@PrintAddressWebsite"].SourceColumn = "PrintAddressWebsite";
			parameters["@PrintAddressFax"].SourceColumn = "PrintAddressFax";
			parameters["@PrintAddressMobile"].SourceColumn = "PrintAddressMobile";
			parameters["@PrintAddress1"].SourceColumn = "PrintAddress1";
			parameters["@PrintAddress2"].SourceColumn = "PrintAddress2";
			parameters["@PrintAddress3"].SourceColumn = "PrintAddress3";
			parameters["@PrintAddressCity"].SourceColumn = "PrintAddressCity";
			parameters["@PrintAddressCountry"].SourceColumn = "PrintAddressCountry";
			parameters["@PrintTotalAmountInWords"].SourceColumn = "PrintTotalAmountInWords";
			parameters["@PrintSignature1"].SourceColumn = "PrintSignature1";
			parameters["@PrintSignature2"].SourceColumn = "PrintSignature2";
			parameters["@PrintLogo"].SourceColumn = "PrintLogo";
			parameters["@PrintReference"].SourceColumn = "PrintReference";
			parameters["@PrintLocation"].SourceColumn = "PrintLocation";
			parameters["@PrintColNumber"].SourceColumn = "PrintColNumber";
			parameters["@PrintColDescription"].SourceColumn = "PrintColDescription";
			parameters["@PrintColQty"].SourceColumn = "PrintColQty";
			parameters["@PrintColPrice"].SourceColumn = "PrintColPrice";
			parameters["@PrintColTotal"].SourceColumn = "PrintColTotal";
			parameters["@Signature1Title"].SourceColumn = "Signature1Title";
			parameters["@Signature2Title"].SourceColumn = "Signature2Title";
			parameters["@ReferenceTitle"].SourceColumn = "ReferenceTitle";
			parameters["@LocationTitle"].SourceColumn = "LocationTitle";
			parameters["@ColNumberTitle"].SourceColumn = "ColNumberTitle";
			parameters["@ColQtyTitle"].SourceColumn = "ColQtyTitle";
			parameters["@ColTotalTitle"].SourceColumn = "ColTotalTitle";
			parameters["@ColDateTitle"].SourceColumn = "ColDateTitle";
			parameters["@ColDebitTitle"].SourceColumn = "ColDebitTitle";
			parameters["@ColCreditTitle"].SourceColumn = "ColCreditTitle";
			parameters["@ColBalanceTitle"].SourceColumn = "ColBalanceTitle";
			parameters["@StatementPeriodTitle"].SourceColumn = "StatementPeriodTitle";
			parameters["@StatementAging1Title"].SourceColumn = "StatementAging1Title";
			parameters["@StatementAging2Title"].SourceColumn = "StatementAging2Title";
			parameters["@StatementAging3Title"].SourceColumn = "StatementAging3Title";
			parameters["@StatementAging4Title"].SourceColumn = "StatementAging4Title";
			parameters["@StatementAging5Title"].SourceColumn = "StatementAging5Title";
			parameters["@StatementAging6Title"].SourceColumn = "StatementAging6Title";
			parameters["@StatementAging7Title"].SourceColumn = "StatementAging7Title";
			parameters["@StatementAging8Title"].SourceColumn = "StatementAging8Title";
			parameters["@TotalDebitTitle"].SourceColumn = "TotalDebitTitle";
			parameters["@TotalCreditTitle"].SourceColumn = "TotalCreditTitle";
			parameters["@BalanceTitle"].SourceColumn = "BalanceTitle";
			parameters["@PrintColDate"].SourceColumn = "PrintColDate";
			parameters["@PrintColDebit"].SourceColumn = "PrintColDEbit";
			parameters["@PrintColCredit"].SourceColumn = "PrintColCredit";
			parameters["@PrintColDebit"].SourceColumn = "PrintColDEbit";
			parameters["@PrintColBalance"].SourceColumn = "PrintColBalance";
			parameters["@PrintStatmentPeriod"].SourceColumn = "PrintStatementPeriod";
			parameters["@PrintTotalDebitTitle"].SourceColumn = "PrintTotalDebit";
			parameters["@PrintTotalCreditTotal"].SourceColumn = "PrintTotalCredit";
			parameters["@PrintBalance"].SourceColumn = "PrintBalance";
			parameters["@PrintMonthlyAging"].SourceColumn = "PrintMonthlyAging";
			parameters["@PrintStatementChecks"].SourceColumn = "PrintStatementChecks";
			parameters["@DefaultLanguage"].SourceColumn = "DefaultLanguage";
			parameters["@PrintShipper"].SourceColumn = "PrintShipper";
			parameters["@ShipperTitle"].SourceColumn = "ShipperTitle";
			parameters["@PhoneTitle"].SourceColumn = "PhoneTitle";
			parameters["@EmailTitle"].SourceColumn = "EmailTitle";
			parameters["@FaxTitle"].SourceColumn = "FaxTitle";
			parameters["@WebsiteTitle"].SourceColumn = "WebsiteTitle";
			parameters["@PrintJob"].SourceColumn = "PrintJob";
			parameters["@JobTitle"].SourceColumn = "JobTitle";
			parameters["@PrintPayee"].SourceColumn = "PrintPayee";
			parameters["@PayeeTitle"].SourceColumn = "PayeeTitle";
			parameters["@PrintPayeeID"].SourceColumn = "PrintPayeeID";
			parameters["@PrintPayeeCompanyName"].SourceColumn = "PrintPayeeCompanyName";
			parameters["@PrintPayeeLastName"].SourceColumn = "PrintPayeeLastName";
			parameters["@PrintPaymentMethod"].SourceColumn = "PrintPaymentMethod";
			parameters["@ColBalanceAlign"].SourceColumn = "ColBalanceAlign";
			parameters["@ColCreditAlign"].SourceColumn = "ColCreditAlign";
			parameters["@ColDateAlign"].SourceColumn = "ColDateAlign";
			parameters["@ColDebitAlign"].SourceColumn = "ColDebitAlign";
			parameters["@ColDescriptionAlign"].SourceColumn = "ColDescriptionAlign";
			parameters["@ColNumberAlign"].SourceColumn = "ColNumberAlign";
			parameters["@ColPriceAlign"].SourceColumn = "ColPriceAlign";
			parameters["@ColQtyAlign"].SourceColumn = "ColQtyAlign";
			parameters["@ColTotalAlign"].SourceColumn = "ColTotalAlign";
			parameters["@ColUnitAlign"].SourceColumn = "ColUnitAlign";
			parameters["@ColAmountAlign"].SourceColumn = "ColAmountAlign";
			parameters["@ColUnitTitle"].SourceColumn = "ColUnitTitle";
			parameters["@PrintColUnit"].SourceColumn = "PrintColUnit";
			parameters["@Edition"].SourceColumn = "Edition";
			parameters["@PrintColAmount"].SourceColumn = "PrintColAmount";
			parameters["@PayeeIDTitle"].SourceColumn = "PayeeIDTitle";
			parameters["@PaymentMethodTitle"].SourceColumn = "PaymentMethodTitle";
			parameters["@PrintSignature3"].SourceColumn = "PrintSignature3";
			parameters["@Signature3Title"].SourceColumn = "Signature3Title";
			parameters["@TotalAmountInWordsTitle"].SourceColumn = "TotalAmountInWordsTitle";
			parameters["@ColOriginalAmountTitle"].SourceColumn = "ColOriginalAmountTitle";
			parameters["@PrintColOriginalAmount"].SourceColumn = "PrintColOriginalAmount";
			parameters["@ColOriginalAmountAlign"].SourceColumn = "ColOriginalAmountAlign";
			parameters["@ColAmountDueTitle"].SourceColumn = "ColAmountDueTitle";
			parameters["@ColAmountDueAlign"].SourceColumn = "ColAmountDueAlign";
			parameters["@PrintColAmountDue"].SourceColumn = "PrintColAmountDue";
			parameters["@ColPaymentTitle"].SourceColumn = "ColPaymentTitle";
			parameters["@ColPaymentAlign"].SourceColumn = "ColPaymentAlign";
			parameters["@PrintColPayment"].SourceColumn = "PrintColPayment";
			parameters["@PrintColDiscount"].SourceColumn = "PrintColDiscount";
			parameters["@ColDiscountTitle"].SourceColumn = "ColDiscountTitle";
			parameters["@ColDiscountAlign"].SourceColumn = "ColDiscountAlign";
			parameters["@CheckDateTitle"].SourceColumn = "CheckDateTitle";
			parameters["@PrintCheckDate"].SourceColumn = "PrintCheckDate";
			parameters["@PrintBank"].SourceColumn = "PrintBank";
			parameters["@BankTitle"].SourceColumn = "BankTitle";
			parameters["@CheckNumberTitle"].SourceColumn = "CheckNumberTitle";
			parameters["@PrintCheckNumber"].SourceColumn = "PrintCheckNumber";
			parameters["@PrintAccount"].SourceColumn = "PrintAccount";
			parameters["@AccountTitle"].SourceColumn = "AccountTitle";
			parameters["@PrintEmployee"].SourceColumn = "PrintEmployee";
			parameters["@EmployeeTitle"].SourceColumn = "EmployeeTitle";
			parameters["@PrintBeginDate"].SourceColumn = "PrintBeginDate";
			parameters["@BeginDateTitle"].SourceColumn = "BeginDateTitle";
			parameters["@PrintEndDate"].SourceColumn = "PrintEndDate";
			parameters["@EndDateTitle"].SourceColumn = "EndDateTitle";
			parameters["@PrintColType"].SourceColumn = "PrintColType";
			parameters["@ColTypeTitle"].SourceColumn = "ColTypeTitle";
			parameters["@ColTypeAlign"].SourceColumn = "ColTypeAlign";
			parameters["@ColPayeeTitle"].SourceColumn = "ColPayeeTitle";
			parameters["@ColPayeeAlign"].SourceColumn = "ColPayeeAlign";
			parameters["@PrintColPayee"].SourceColumn = "PrintColPayee";
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				SqlBuilder updateText = GetUpdateText();
				updateCommand = new SqlCommand(updateText.GetUpdateExpression(), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add("@TemplateID", SqlDbType.SmallInt);
				parameters.Add("@Name", SqlDbType.NVarChar);
				parameters.Add("@Data", SqlDbType.NText);
				parameters.Add("@PrintBillTo", SqlDbType.Bit);
				parameters.Add("@PrintShipTo", SqlDbType.Bit);
				parameters.Add("@PrintNumber", SqlDbType.Bit);
				parameters.Add("@PrintCompanyHeader", SqlDbType.Bit);
				parameters.Add("@PrintDate", SqlDbType.Bit);
				parameters.Add("@PrintDocumentTitle", SqlDbType.Bit);
				parameters.Add("@BillToTitle", SqlDbType.NVarChar);
				parameters.Add("@ShipToTitle", SqlDbType.NVarChar);
				parameters.Add("@NumberTitle", SqlDbType.NVarChar);
				parameters.Add("@DateTitle", SqlDbType.NVarChar);
				parameters.Add("@DocumentTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintDueDate", SqlDbType.Bit);
				parameters.Add("@PrintPONo", SqlDbType.Bit);
				parameters.Add("@POTitle", SqlDbType.NVarChar);
				parameters.Add("@DueDateTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintSalesRep", SqlDbType.Bit);
				parameters.Add("@SalesRepTitle", SqlDbType.NVarChar);
				parameters.Add("@ColDescriptionTitle", SqlDbType.NVarChar);
				parameters.Add("@ColPriceTitle", SqlDbType.NVarChar);
				parameters.Add("@ColAmountTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintCustomerMessage", SqlDbType.Bit);
				parameters.Add("@CustomerMessageTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintDiscount", SqlDbType.Bit);
				parameters.Add("@DiscountTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintTotal", SqlDbType.Bit);
				parameters.Add("@TotalTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintSubtotal", SqlDbType.Bit);
				parameters.Add("@SubtotalTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintBalanceDue", SqlDbType.Bit);
				parameters.Add("@BalanceDueTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintFreight", SqlDbType.Bit);
				parameters.Add("@FreightTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintMiscellaneous", SqlDbType.Bit);
				parameters.Add("@MiscellaneousTitle", SqlDbType.NVarChar);
				parameters.Add("@DocumentType", SqlDbType.TinyInt);
				parameters.Add("@IsReadOnly", SqlDbType.Bit);
				parameters.Add("@DateTimeStamp", SqlDbType.DateTime);
				parameters.Add("@PrintCompanyAddress", SqlDbType.Bit);
				parameters.Add("@PrintAddressEmail", SqlDbType.Bit);
				parameters.Add("@PrintAddressTelephone", SqlDbType.Bit);
				parameters.Add("@PrintAddressWebsite", SqlDbType.Bit);
				parameters.Add("@PrintAddressFax", SqlDbType.Bit);
				parameters.Add("@PrintAddressMobile", SqlDbType.Bit);
				parameters.Add("@PrintAddress1", SqlDbType.Bit);
				parameters.Add("@PrintAddress2", SqlDbType.Bit);
				parameters.Add("@PrintAddress3", SqlDbType.Bit);
				parameters.Add("@PrintAddressCity", SqlDbType.Bit);
				parameters.Add("@PrintAddressCountry", SqlDbType.Bit);
				parameters.Add("@PrintTotalAmountInWords", SqlDbType.Bit);
				parameters.Add("@PrintSignature1", SqlDbType.Bit);
				parameters.Add("@PrintSignature2", SqlDbType.Bit);
				parameters.Add("@PrintLogo", SqlDbType.Bit);
				parameters.Add("@PrintReference", SqlDbType.Bit);
				parameters.Add("@PrintLocation", SqlDbType.Bit);
				parameters.Add("@PrintColNumber", SqlDbType.Bit);
				parameters.Add("@PrintColDescription", SqlDbType.Bit);
				parameters.Add("@PrintColQty", SqlDbType.Bit);
				parameters.Add("@PrintColPrice", SqlDbType.Bit);
				parameters.Add("@PrintColTotal", SqlDbType.Bit);
				parameters.Add("@Signature1Title", SqlDbType.NVarChar);
				parameters.Add("@Signature2Title", SqlDbType.NVarChar);
				parameters.Add("@ReferenceTitle", SqlDbType.NVarChar);
				parameters.Add("@LocationTitle", SqlDbType.NVarChar);
				parameters.Add("@ColNumberTitle", SqlDbType.NVarChar);
				parameters.Add("@ColQtyTitle", SqlDbType.NVarChar);
				parameters.Add("@ColTotalTitle", SqlDbType.NVarChar);
				parameters.Add("@ColDateTitle", SqlDbType.NVarChar);
				parameters.Add("@ColDebitTitle", SqlDbType.NVarChar);
				parameters.Add("@ColCreditTitle", SqlDbType.NVarChar);
				parameters.Add("@ColBalanceTitle", SqlDbType.NVarChar);
				parameters.Add("@StatementPeriodTitle", SqlDbType.NVarChar);
				parameters.Add("@StatementAging1Title", SqlDbType.NVarChar);
				parameters.Add("@StatementAging2Title", SqlDbType.NVarChar);
				parameters.Add("@StatementAging3Title", SqlDbType.NVarChar);
				parameters.Add("@StatementAging4Title", SqlDbType.NVarChar);
				parameters.Add("@StatementAging5Title", SqlDbType.NVarChar);
				parameters.Add("@StatementAging6Title", SqlDbType.NVarChar);
				parameters.Add("@StatementAging7Title", SqlDbType.NVarChar);
				parameters.Add("@StatementAging8Title", SqlDbType.NVarChar);
				parameters.Add("@TotalDebitTitle", SqlDbType.NVarChar);
				parameters.Add("@TotalCreditTitle", SqlDbType.NVarChar);
				parameters.Add("@BalanceTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintColDate", SqlDbType.Bit);
				parameters.Add("@PrintColDebit", SqlDbType.Bit);
				parameters.Add("@PrintColCredit", SqlDbType.Bit);
				parameters.Add("@PrintColBalance", SqlDbType.Bit);
				parameters.Add("@PrintStatmentPeriod", SqlDbType.Bit);
				parameters.Add("@PrintTotalDebitTitle", SqlDbType.Bit);
				parameters.Add("@PrintTotalCreditTotal", SqlDbType.Bit);
				parameters.Add("@PrintBalance", SqlDbType.Bit);
				parameters.Add("@PrintMonthlyAging", SqlDbType.Bit);
				parameters.Add("@PrintStatementChecks", SqlDbType.Bit);
				parameters.Add("@DefaultLanguage", SqlDbType.TinyInt);
				parameters.Add("@PrintShipper", SqlDbType.Bit);
				parameters.Add("@ShipperTitle", SqlDbType.NVarChar);
				parameters.Add("@PhoneTitle", SqlDbType.NVarChar);
				parameters.Add("@EmailTitle", SqlDbType.NVarChar);
				parameters.Add("@FaxTitle", SqlDbType.NVarChar);
				parameters.Add("@WebsiteTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintJob", SqlDbType.Bit);
				parameters.Add("@JobTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintPayee", SqlDbType.Bit);
				parameters.Add("@PayeeTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintPayeeID", SqlDbType.Bit);
				parameters.Add("@PrintPayeeCompanyName", SqlDbType.Bit);
				parameters.Add("@PrintPayeeLastName", SqlDbType.Bit);
				parameters.Add("@PrintPaymentMethod", SqlDbType.Bit);
				parameters.Add("@ColBalanceAlign", SqlDbType.TinyInt);
				parameters.Add("@ColCreditAlign", SqlDbType.TinyInt);
				parameters.Add("@ColDateAlign", SqlDbType.TinyInt);
				parameters.Add("@ColDebitAlign", SqlDbType.TinyInt);
				parameters.Add("@ColDescriptionAlign", SqlDbType.TinyInt);
				parameters.Add("@ColNumberAlign", SqlDbType.TinyInt);
				parameters.Add("@ColPriceAlign", SqlDbType.TinyInt);
				parameters.Add("@ColQtyAlign", SqlDbType.TinyInt);
				parameters.Add("@ColTotalAlign", SqlDbType.TinyInt);
				parameters.Add("@ColUnitAlign", SqlDbType.TinyInt);
				parameters.Add("@ColAmountAlign", SqlDbType.TinyInt);
				parameters.Add("@ColUnitTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintColUnit", SqlDbType.Bit);
				parameters.Add("@Edition", SqlDbType.TinyInt);
				parameters.Add("@PrintColAmount", SqlDbType.Bit);
				parameters.Add("@PayeeIDTitle", SqlDbType.NVarChar);
				parameters.Add("@PaymentMethodTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintSignature3", SqlDbType.Bit);
				parameters.Add("@Signature3Title", SqlDbType.NVarChar);
				parameters.Add("@TotalAmountInWordsTitle", SqlDbType.NVarChar);
				parameters.Add("@ColOriginalAmountTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintColOriginalAmount", SqlDbType.Bit);
				parameters.Add("@ColOriginalAmountAlign", SqlDbType.TinyInt);
				parameters.Add("@ColAmountDueTitle", SqlDbType.NVarChar);
				parameters.Add("@ColAmountDueAlign", SqlDbType.TinyInt);
				parameters.Add("@PrintColAmountDue", SqlDbType.Bit);
				parameters.Add("@ColPaymentTitle", SqlDbType.NVarChar);
				parameters.Add("@ColPaymentAlign", SqlDbType.TinyInt);
				parameters.Add("@PrintColPayment", SqlDbType.Bit);
				parameters.Add("@PrintColDiscount", SqlDbType.Bit);
				parameters.Add("@ColDiscountTitle", SqlDbType.NVarChar);
				parameters.Add("@ColDiscountAlign", SqlDbType.TinyInt);
				parameters.Add("@CheckDateTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintCheckDate", SqlDbType.Bit);
				parameters.Add("@PrintBank", SqlDbType.Bit);
				parameters.Add("@BankTitle", SqlDbType.NVarChar);
				parameters.Add("@CheckNumberTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintCheckNumber", SqlDbType.Bit);
				parameters.Add("@PrintAccount", SqlDbType.Bit);
				parameters.Add("@AccountTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintEmployee", SqlDbType.Bit);
				parameters.Add("@EmployeeTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintBeginDate", SqlDbType.Bit);
				parameters.Add("@BeginDateTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintEndDate", SqlDbType.Bit);
				parameters.Add("@EndDateTitle", SqlDbType.NVarChar);
				parameters.Add("@PrintColType", SqlDbType.Bit);
				parameters.Add("@ColTypeTitle", SqlDbType.NVarChar);
				parameters.Add("@ColTypeAlign", SqlDbType.TinyInt);
				parameters.Add("@ColPayeeTitle", SqlDbType.NVarChar);
				parameters.Add("@ColPayeeAlign", SqlDbType.TinyInt);
				parameters.Add("@PrintColPayee", SqlDbType.Bit);
				parameters["@TemplateID"].SourceColumn = "TemplateID";
				parameters["@Name"].SourceColumn = "Name";
				parameters["@Data"].SourceColumn = "Data";
				parameters["@PrintBillTo"].SourceColumn = "PrintBillTo";
				parameters["@PrintShipTo"].SourceColumn = "PrintShipTo";
				parameters["@PrintNumber"].SourceColumn = "PrintNumber";
				parameters["@PrintCompanyHeader"].SourceColumn = "PrintCompanyHeader";
				parameters["@PrintDate"].SourceColumn = "PrintDate";
				parameters["@PrintDocumentTitle"].SourceColumn = "PrintDocumentTitle";
				parameters["@BillToTitle"].SourceColumn = "BillToTitle";
				parameters["@ShipToTitle"].SourceColumn = "ShipToTitle";
				parameters["@NumberTitle"].SourceColumn = "NumberTitle";
				parameters["@DateTitle"].SourceColumn = "DateTitle";
				parameters["@DocumentTitle"].SourceColumn = "DocumentTitle";
				parameters["@PrintPONo"].SourceColumn = "PrintPONo";
				parameters["@POTitle"].SourceColumn = "POTitle";
				parameters["@PrintDueDate"].SourceColumn = "PrintDueDate";
				parameters["@DueDateTitle"].SourceColumn = "DueDateTitle";
				parameters["@PrintSalesRep"].SourceColumn = "PrintSalesRep";
				parameters["@SalesRepTitle"].SourceColumn = "SalesRepTitle";
				parameters["@ColDescriptionTitle"].SourceColumn = "ColDescriptionTitle";
				parameters["@ColPriceTitle"].SourceColumn = "ColPriceTitle";
				parameters["@ColAmountTitle"].SourceColumn = "ColAmountTitle";
				parameters["@PrintCustomerMessage"].SourceColumn = "PrintCustomerMessage";
				parameters["@CustomerMessageTitle"].SourceColumn = "CustomerMessageTitle";
				parameters["@PrintDiscount"].SourceColumn = "PrintDiscount";
				parameters["@DiscountTitle"].SourceColumn = "DiscountTitle";
				parameters["@PrintTotal"].SourceColumn = "PrintTotal";
				parameters["@TotalTitle"].SourceColumn = "TotalTitle";
				parameters["@PrintSubtotal"].SourceColumn = "PrintSubtotal";
				parameters["@SubtotalTitle"].SourceColumn = "SubtotalTitle";
				parameters["@PrintBalanceDue"].SourceColumn = "PrintBalanceDue";
				parameters["@BalanceDueTitle"].SourceColumn = "BalanceDueTitle";
				parameters["@PrintFreight"].SourceColumn = "PrintFreight";
				parameters["@FreightTitle"].SourceColumn = "FreightTitle";
				parameters["@PrintMiscellaneous"].SourceColumn = "PrintMiscellaneous";
				parameters["@MiscellaneousTitle"].SourceColumn = "MiscellaneousTitle";
				parameters["@DocumentType"].SourceColumn = "DocumentType";
				parameters["@IsReadOnly"].SourceColumn = "IsReadOnly";
				parameters["@DateTimeStamp"].SourceColumn = "DateTimeStamp";
				parameters["@PrintCompanyAddress"].SourceColumn = "PrintCompanyAddress";
				parameters["@PrintAddressEmail"].SourceColumn = "PrintAddressEmail";
				parameters["@PrintAddressTelephone"].SourceColumn = "PrintAddressTelephone";
				parameters["@PrintAddressWebsite"].SourceColumn = "PrintAddressWebsite";
				parameters["@PrintAddressFax"].SourceColumn = "PrintAddressFax";
				parameters["@PrintAddressMobile"].SourceColumn = "PrintAddressMobile";
				parameters["@PrintAddress1"].SourceColumn = "PrintAddress1";
				parameters["@PrintAddress2"].SourceColumn = "PrintAddress2";
				parameters["@PrintAddress3"].SourceColumn = "PrintAddress3";
				parameters["@PrintAddressCity"].SourceColumn = "PrintAddressCity";
				parameters["@PrintAddressCountry"].SourceColumn = "PrintAddressCountry";
				parameters["@PrintTotalAmountInWords"].SourceColumn = "PrintTotalAmountInWords";
				parameters["@PrintSignature1"].SourceColumn = "PrintSignature1";
				parameters["@PrintSignature2"].SourceColumn = "PrintSignature2";
				parameters["@PrintLogo"].SourceColumn = "PrintLogo";
				parameters["@PrintReference"].SourceColumn = "PrintReference";
				parameters["@PrintLocation"].SourceColumn = "PrintLocation";
				parameters["@PrintColNumber"].SourceColumn = "PrintColNumber";
				parameters["@PrintColDescription"].SourceColumn = "PrintColDescription";
				parameters["@PrintColQty"].SourceColumn = "PrintColQty";
				parameters["@PrintColPrice"].SourceColumn = "PrintColPrice";
				parameters["@PrintColTotal"].SourceColumn = "PrintColTotal";
				parameters["@Signature1Title"].SourceColumn = "Signature1Title";
				parameters["@Signature2Title"].SourceColumn = "Signature2Title";
				parameters["@ReferenceTitle"].SourceColumn = "ReferenceTitle";
				parameters["@LocationTitle"].SourceColumn = "LocationTitle";
				parameters["@ColNumberTitle"].SourceColumn = "ColNumberTitle";
				parameters["@ColQtyTitle"].SourceColumn = "ColQtyTitle";
				parameters["@ColTotalTitle"].SourceColumn = "ColTotalTitle";
				parameters["@ColDateTitle"].SourceColumn = "ColDateTitle";
				parameters["@ColDebitTitle"].SourceColumn = "ColDebitTitle";
				parameters["@ColCreditTitle"].SourceColumn = "ColCreditTitle";
				parameters["@ColBalanceTitle"].SourceColumn = "ColBalanceTitle";
				parameters["@StatementPeriodTitle"].SourceColumn = "StatementPeriodTitle";
				parameters["@StatementAging1Title"].SourceColumn = "StatementAging1Title";
				parameters["@StatementAging2Title"].SourceColumn = "StatementAging2Title";
				parameters["@StatementAging3Title"].SourceColumn = "StatementAging3Title";
				parameters["@StatementAging4Title"].SourceColumn = "StatementAging4Title";
				parameters["@StatementAging5Title"].SourceColumn = "StatementAging5Title";
				parameters["@StatementAging6Title"].SourceColumn = "StatementAging6Title";
				parameters["@StatementAging7Title"].SourceColumn = "StatementAging7Title";
				parameters["@StatementAging8Title"].SourceColumn = "StatementAging8Title";
				parameters["@TotalDebitTitle"].SourceColumn = "TotalDebitTitle";
				parameters["@TotalCreditTitle"].SourceColumn = "TotalCreditTitle";
				parameters["@BalanceTitle"].SourceColumn = "BalanceTitle";
				parameters["@PrintColDate"].SourceColumn = "PrintColDate";
				parameters["@PrintColDebit"].SourceColumn = "PrintColDEbit";
				parameters["@PrintColCredit"].SourceColumn = "PrintColCredit";
				parameters["@PrintColDebit"].SourceColumn = "PrintColDEbit";
				parameters["@PrintColBalance"].SourceColumn = "PrintColBalance";
				parameters["@PrintStatmentPeriod"].SourceColumn = "PrintStatementPeriod";
				parameters["@PrintTotalDebitTitle"].SourceColumn = "PrintTotalDebit";
				parameters["@PrintTotalCreditTotal"].SourceColumn = "PrintTotalCredit";
				parameters["@PrintBalance"].SourceColumn = "PrintBalance";
				parameters["@PrintMonthlyAging"].SourceColumn = "PrintMonthlyAging";
				parameters["@PrintStatementChecks"].SourceColumn = "PrintStatementChecks";
				parameters["@DefaultLanguage"].SourceColumn = "DefaultLanguage";
				parameters["@PrintShipper"].SourceColumn = "PrintShipper";
				parameters["@ShipperTitle"].SourceColumn = "ShipperTitle";
				parameters["@PhoneTitle"].SourceColumn = "PhoneTitle";
				parameters["@EmailTitle"].SourceColumn = "EmailTitle";
				parameters["@FaxTitle"].SourceColumn = "FaxTitle";
				parameters["@WebsiteTitle"].SourceColumn = "WebsiteTitle";
				parameters["@PrintJob"].SourceColumn = "PrintJob";
				parameters["@JobTitle"].SourceColumn = "JobTitle";
				parameters["@PrintPayee"].SourceColumn = "PrintPayee";
				parameters["@PayeeTitle"].SourceColumn = "PayeeTitle";
				parameters["@PrintPayeeID"].SourceColumn = "PrintPayeeID";
				parameters["@PrintPayeeCompanyName"].SourceColumn = "PrintPayeeCompanyName";
				parameters["@PrintPayeeLastName"].SourceColumn = "PrintPayeeLastName";
				parameters["@PrintPaymentMethod"].SourceColumn = "PrintPaymentMethod";
				parameters["@ColBalanceAlign"].SourceColumn = "ColBalanceAlign";
				parameters["@ColCreditAlign"].SourceColumn = "ColCreditAlign";
				parameters["@ColDateAlign"].SourceColumn = "ColDateAlign";
				parameters["@ColDebitAlign"].SourceColumn = "ColDebitAlign";
				parameters["@ColDescriptionAlign"].SourceColumn = "ColDescriptionAlign";
				parameters["@ColNumberAlign"].SourceColumn = "ColNumberAlign";
				parameters["@ColPriceAlign"].SourceColumn = "ColPriceAlign";
				parameters["@ColQtyAlign"].SourceColumn = "ColQtyAlign";
				parameters["@ColTotalAlign"].SourceColumn = "ColTotalAlign";
				parameters["@ColUnitAlign"].SourceColumn = "ColUnitAlign";
				parameters["@ColAmountAlign"].SourceColumn = "ColAmountAlign";
				parameters["@ColUnitTitle"].SourceColumn = "ColUnitTitle";
				parameters["@PrintColUnit"].SourceColumn = "PrintColUnit";
				parameters["@Edition"].SourceColumn = "Edition";
				parameters["@PrintColAmount"].SourceColumn = "PrintColAmount";
				parameters["@PayeeIDTitle"].SourceColumn = "PayeeIDTitle";
				parameters["@PaymentMethodTitle"].SourceColumn = "PaymentMethodTitle";
				parameters["@PrintSignature3"].SourceColumn = "PrintSignature3";
				parameters["@Signature3Title"].SourceColumn = "Signature3Title";
				parameters["@TotalAmountInWordsTitle"].SourceColumn = "TotalAmountInWordsTitle";
				parameters["@ColOriginalAmountTitle"].SourceColumn = "ColOriginalAmountTitle";
				parameters["@PrintColOriginalAmount"].SourceColumn = "PrintColOriginalAmount";
				parameters["@ColOriginalAmountAlign"].SourceColumn = "ColOriginalAmountAlign";
				parameters["@ColAmountDueTitle"].SourceColumn = "ColAmountDueTitle";
				parameters["@ColAmountDueAlign"].SourceColumn = "ColAmountDueAlign";
				parameters["@PrintColAmountDue"].SourceColumn = "PrintColAmountDue";
				parameters["@ColPaymentTitle"].SourceColumn = "ColPaymentTitle";
				parameters["@ColPaymentAlign"].SourceColumn = "ColPaymentAlign";
				parameters["@PrintColPayment"].SourceColumn = "PrintColPayment";
				parameters["@PrintColDiscount"].SourceColumn = "PrintColDiscount";
				parameters["@ColDiscountTitle"].SourceColumn = "ColDiscountTitle";
				parameters["@ColDiscountAlign"].SourceColumn = "ColDiscountAlign";
				parameters["@CheckDateTitle"].SourceColumn = "CheckDateTitle";
				parameters["@PrintCheckDate"].SourceColumn = "PrintCheckDate";
				parameters["@PrintBank"].SourceColumn = "PrintBank";
				parameters["@BankTitle"].SourceColumn = "BankTitle";
				parameters["@CheckNumberTitle"].SourceColumn = "CheckNumberTitle";
				parameters["@PrintCheckNumber"].SourceColumn = "PrintCheckNumber";
				parameters["@PrintAccount"].SourceColumn = "PrintAccount";
				parameters["@AccountTitle"].SourceColumn = "AccountTitle";
				parameters["@PrintEmployee"].SourceColumn = "PrintEmployee";
				parameters["@EmployeeTitle"].SourceColumn = "EmployeeTitle";
				parameters["@PrintBeginDate"].SourceColumn = "PrintBeginDate";
				parameters["@BeginDateTitle"].SourceColumn = "BeginDateTitle";
				parameters["@PrintEndDate"].SourceColumn = "PrintEndDate";
				parameters["@EndDateTitle"].SourceColumn = "EndDateTitle";
				parameters["@PrintColType"].SourceColumn = "PrintColType";
				parameters["@ColTypeTitle"].SourceColumn = "ColTypeTitle";
				parameters["@ColTypeAlign"].SourceColumn = "ColTypeAlign";
				parameters["@ColPayeeTitle"].SourceColumn = "ColPayeeTitle";
				parameters["@ColPayeeAlign"].SourceColumn = "ColPayeeAlign";
				parameters["@PrintColPayee"].SourceColumn = "PrintColPayee";
			}
			return updateCommand;
		}

		public bool InsertPrint(PrintTemplateData printData)
		{
			if (printData.PrintTemplatesTable.Rows.Count == 0)
			{
				return true;
			}
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				SqlCommand insertCommand = GetInsertCommand();
				sqlTransaction = (insertCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(printData, "[Print Templates]", insertCommand);
				object insertedRowIdentity = GetInsertedRowIdentity("[Print Templates]", insertCommand);
				if (insertedRowIdentity == null)
				{
					return flag;
				}
				if (insertedRowIdentity == DBNull.Value)
				{
					return flag;
				}
				if (!(insertedRowIdentity.ToString() != string.Empty))
				{
					return flag;
				}
				printData.PrintTemplatesTable.Rows[0]["TemplateID"] = insertedRowIdentity;
				UpdateTableRowByID("[Print Templates]", "TemplateID", "DateTimeStamp", insertedRowIdentity, DateTime.Now, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				string entiyID = printData.PrintTemplatesTable.Rows[0]["Name"].ToString();
				AddActivityLog("Print Template", entiyID, ActivityTypes.Add, sqlTransaction);
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

		public bool UpdatePrint(PrintTemplateData printData)
		{
			bool flag = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(printData, "[Print Templates]", updateCommand);
				object id = printData.PrintTemplatesTable.Rows[0]["TemplateID"];
				UpdateTableRowByID("[Print Templates]", "TemplateID", "DateTimeStamp", id, DateTime.Now, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				string entiyID = printData.PrintTemplatesTable.Rows[0]["Name"].ToString();
				AddActivityLog("Print Template", entiyID, ActivityTypes.Update, sqlTransaction);
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

		public bool UpdatePrintTemplate(int templateID, string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE ").Append("[Print Templates]");
			stringBuilder.Append(" SET ").Append("Data").Append("='");
			stringBuilder.Append(data).Append("' ");
			stringBuilder.Append("WHERE ").Append("TemplateID");
			stringBuilder.Append("=").Append(templateID.ToString());
			return ExecuteNonQuery(stringBuilder.ToString()) > 0;
		}

		public PrintTemplateData GetPrintByID(int templateID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TemplateID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = templateID;
			commandHelper.TableName = "[Print Templates]";
			sqlBuilder.AddCommandHelper(commandHelper);
			PrintTemplateData printTemplateData = new PrintTemplateData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(printTemplateData, "[Print Templates]", sqlBuilder);
				return printTemplateData;
			}
			catch
			{
				throw;
			}
			finally
			{
				commandHelper = null;
				sqlBuilder = null;
			}
		}

		public PrintTemplateData GetPrintTemplates(bool isReadOnly)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			if (!isReadOnly)
			{
				commandHelper = new CommandHelper();
				commandHelper.FieldName = "IsReadOnly";
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.FieldValue = 0;
				commandHelper.TableName = "[Print Templates]";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.AddOrderByColumn("[Print Templates]", "Name");
			PrintTemplateData printTemplateData = new PrintTemplateData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(printTemplateData, "[Print Templates]", sqlBuilder);
				return printTemplateData;
			}
			catch
			{
				throw;
			}
			finally
			{
				sqlBuilder = null;
			}
		}

		public bool DeletePrintTemplate(int templateID)
		{
			bool flag = true;
			try
			{
				string templateNameByID = GetTemplateNameByID(templateID);
				flag = DeleteTableRowByID("[Print Templates]", "TemplateID", templateID.ToString());
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Print Template", templateNameByID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetTemplateIDByName(string name)
		{
			try
			{
				object obj = ExecuteSelectScalar("[Print Templates]", "Name", name, "TemplateID");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "-1";
		}

		public string GetTemplateNameByID(object id)
		{
			try
			{
				object obj = ExecuteSelectScalar("[Print Templates]", "TemplateID", id, "Name");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "";
		}

		public bool ExistPrintTemplate(string name)
		{
			try
			{
				return IsTableFieldValueExist("[Print Templates]", "Name", name);
			}
			catch
			{
				throw;
			}
		}

		public PrintTemplateData GetPrintTemplates(PrintTemplateTypes[] types, bool readOnly)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT * FROM ").Append("[Print Templates]");
			bool flag = false;
			if (types != null && types.Length != 0)
			{
				flag = true;
				stringBuilder.Append(" WHERE ");
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("DocumentType").Append(" IN(");
				for (int i = 0; i < types.Length; i++)
				{
					stringBuilder2.Append(((byte)types[i]).ToString()).Append(",");
				}
				stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
				stringBuilder2.Append(") ");
				stringBuilder.Append(stringBuilder2.ToString()).Append(" ");
			}
			if (!readOnly)
			{
				if (flag)
				{
					stringBuilder.Append("AND");
				}
				else
				{
					stringBuilder.Append("WHERE");
				}
				stringBuilder.Append("IsReadOnly").Append("='0'");
			}
			PrintTemplateData printTemplateData = new PrintTemplateData();
			try
			{
				FillDataSet(printTemplateData, "[Print Templates]", stringBuilder.ToString());
				return printTemplateData;
			}
			catch
			{
				throw;
			}
			finally
			{
				stringBuilder = null;
			}
		}

		public string GetTemplateLayout(int id)
		{
			object obj = ExecuteSelectScalar("[Print Templates]", "TemplateID", id, "Data");
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return string.Empty;
		}

		public bool LoadPrintTemplateFile(string fileName, string templateName)
		{
			if (!File.Exists(fileName))
			{
				throw new ApplicationException("File does not exist.");
			}
			StringCollection stringCollection = new StringCollection();
			stringCollection.Add(fileName);
			return LoadTemplates(stringCollection, templateName);
		}

		public bool LoadPrintTemplateStream(byte[] stream, string templateName)
		{
			return LoadPrintTemplateStream(stream, templateName, overwrite: false);
		}

		public bool LoadPrintTemplateStream(byte[] template, string templateName, bool overwrite)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(template, 0, template.Length);
			if (memoryStream == null)
			{
				throw new NullReferenceException("Stream cannot be null.");
			}
			string tempFileName = Path.GetTempFileName();
			Stream stream = new FileStream(tempFileName, FileMode.Open);
			byte[] array = new byte[32768];
			memoryStream.Seek(0L, SeekOrigin.Begin);
			int num = 0;
			int num2;
			while ((num2 = memoryStream.Read(array, num, array.Length - num)) > 0)
			{
				num += num2;
				if (num == array.Length)
				{
					int num3 = memoryStream.ReadByte();
					if (num3 == -1)
					{
						break;
					}
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, array2, array.Length);
					array2[num] = (byte)num3;
					array = array2;
					num++;
				}
			}
			stream.Write(array, 0, num);
			stream.Flush();
			StringCollection stringCollection = new StringCollection();
			stringCollection.Add(tempFileName);
			stream.Close();
			stream = null;
			return LoadTemplates(stringCollection, templateName);
		}

		internal bool LoadTemplates(StringCollection strCol, string singleTemplateName)
		{
			return LoadTemplates(strCol, singleTemplateName, overwrite: false);
		}

		internal bool LoadTemplates(StringCollection strCol, string singleTemplateName, bool overwrite)
		{
			if (strCol.Count > 1)
			{
				singleTemplateName = null;
			}
			bool result = true;
			StringEnumerator enumerator = strCol.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					PrintTemplateData printTemplateData = new PrintTemplateData();
					DataRow dataRow;
					try
					{
						StreamReader streamReader = new StreamReader(current, Encoding.ASCII);
						StringBuilder stringBuilder = new StringBuilder();
						string text = streamReader.ReadLine();
						bool flag = false;
						while (text != null)
						{
							if (text.Trim() != string.Empty && text.Trim().Substring(0, 4).Equals("!@#$"))
							{
								flag = true;
								break;
							}
							stringBuilder.Append(text);
							text = streamReader.ReadLine();
						}
						if (flag)
						{
							dataRow = printTemplateData.PrintTemplatesTable.NewRow();
							dataRow["Data"] = stringBuilder.ToString();
							for (string text2 = streamReader.ReadLine(); text2 != null; text2 = streamReader.ReadLine())
							{
								string[] array = text2.Split('=');
								if (array.Length == 2 && printTemplateData.PrintTemplatesTable.Columns.Contains(array[0].Trim()))
								{
									object obj = array[1].Trim();
									if (obj == null || obj.ToString() == string.Empty)
									{
										obj = DBNull.Value;
									}
									else if (obj.ToString().ToLower() == "true")
									{
										obj = 1;
									}
									else if (obj.ToString().ToLower() == "false")
									{
										obj = 0;
									}
									try
									{
										dataRow[array[0].Trim()] = obj;
									}
									catch
									{
									}
								}
							}
							if (singleTemplateName != null && singleTemplateName.Trim() != string.Empty)
							{
								dataRow["Name"] = singleTemplateName;
							}
							printTemplateData.PrintTemplatesTable.Rows.Add(dataRow);
							streamReader?.Close();
							goto IL_01dc;
						}
					}
					catch (Exception ex)
					{
						throw ex;
					}
					continue;
					IL_01dc:
					if (printTemplateData.PrintTemplatesTable.Rows.Count > 0)
					{
						try
						{
							if (!ExistPrintTemplate(dataRow["Name"].ToString().Trim()))
							{
								result = InsertPrint(printTemplateData);
							}
							else if (overwrite)
							{
								string templateIDByName = GetTemplateIDByName(dataRow["Name"].ToString().Trim());
								if (templateIDByName != "-1")
								{
									DeletePrintTemplate(int.Parse(templateIDByName));
									result = InsertPrint(printTemplateData);
								}
							}
						}
						catch
						{
						}
					}
					if (printTemplateData != null)
					{
						printTemplateData.Dispose();
						printTemplateData = null;
					}
				}
				return result;
			}
			finally
			{
				(enumerator as IDisposable)?.Dispose();
			}
		}
	}
}
