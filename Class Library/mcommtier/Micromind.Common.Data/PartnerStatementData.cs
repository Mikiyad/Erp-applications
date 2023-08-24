using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PartnerStatementData : DataSet
	{
		public const string PARTNERSTATEMENTS_TABLE = "[Partner Statements]";

		public const string STATEMENTID_FIELD = "StatementID";

		public const string DATE_FIELD = "Date";

		public const string PARTNERID_FIELD = "PartnerID";

		public const string PARTNERNAME_FIELD = "PartnerName";

		public const string FROMDATE_FIELD = "FromDate";

		public const string TODATE_FIELD = "ToDate";

		public const string PARTNERSTATEMENTDETAILS_TABLE = "[Partner Statement Details]";

		public const string DESCRIPTION_FIELD = "Description";

		public const string DEBIT_FIELD = "Debit";

		public const string CREDIT_FIELD = "Credit";

		public const string BALANCE_FIELD = "Balance";

		public const string AGINGBYDAYS_TABLE = "[Aging By Day]";

		public const string CURRENT_FIELD = "Current";

		public const string DAY1TO30_FIELD = "1to30Days";

		public const string DAY31TO60_FIELD = "31to60Days";

		public const string DAY61TO90_FIELD = "61to90Days";

		public const string DAY91TO120_FIELD = "91to120Days";

		public const string OVER120DAYS_FIELD = "Over120Days";

		public const string BALANCEDUE_FIELD = "BalanceDue";

		public const string AGINGBYMONTH_TABLE = "[Aging By Month]";

		public const string MONTH1_FIELD = "Month1";

		public const string MONTH2_FIELD = "Month2";

		public const string MONTH3_FIELD = "Month3";

		public const string MONTH4_FIELD = "Month4";

		public const string MONTH5_FIELD = "Month5";

		public const string MONTH6_FIELD = "Month6";

		public const string MONTH1BALANCE_FIELD = "Month1Balance";

		public const string MONTH2BALANCE_FIELD = "Month2Balance";

		public const string MONTH3BALANCE_FIELD = "Month3Balance";

		public const string MONTH4BALANCE_FIELD = "Month4Balance";

		public const string MONTH5BALANCE_FIELD = "Month5Balance";

		public const string MONTH6BALANCE_FIELD = "Month6Balance";

		public const string UNPOSTEDCHECKS_TABLE = "[Unposted Checks]";

		public const string TRANSACTIONDATE_FIELD = "TraactionDate";

		public const string TRANSACTIONNUMBER_FIELD = "TraactionNumber";

		public const string BANKNAME_FIELD = "BankName";

		public const string CHECKDATE_FIELD = "CheckDate";

		public const string CHECKNUMBER_FIELD = "CheckNumber";

		public const string AMOUNT_FIELD = "Amount";

		public const string TOTALDEBIT_FIELD = "TotalDebit";

		public const string TOTALCREDIT_FIELD = "TotalCredit";

		public DataTable PartnerStatementTable => base.Tables["[Partner Statements]"];

		public DataTable PartnerStatementDetailsTable => base.Tables["[Partner Statement Details]"];

		public DataTable AgingByDaysTable => base.Tables["[Aging By Day]"];

		public DataTable AgingByMonthTable => base.Tables["[Aging By Month]"];

		public DataTable UnpostedChecksTable => base.Tables["[Unposted Checks]"];

		public DataTable ShimmentAddressTable
		{
			get
			{
				if (base.Tables.Contains("[Shipment Addresses]"))
				{
					return base.Tables["[Shipment Addresses]"];
				}
				return new ShipmentAddressData().ShipmentAddressTable;
			}
		}

		public PartnerStatementData()
		{
			BuildDataTables();
		}

		public PartnerStatementData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Partner Statements]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("StatementID", typeof(int));
			dataColumn.AllowDBNull = true;
			dataColumn.AutoIncrement = false;
			columns.Add("Date", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("FromDate", typeof(DateTime));
			columns.Add("ToDate", typeof(DateTime));
			columns.Add("PartnerID", typeof(int));
			columns.Add("PartnerName", typeof(string));
			columns.Add("TotalDebit", typeof(decimal)).DefaultValue = 0;
			columns.Add("TotalCredit", typeof(decimal)).DefaultValue = 0;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Partner Statement Details]");
			DataColumnCollection columns2 = dataTable.Columns;
			DataColumn dataColumn2 = columns2.Add("StatementID", typeof(int));
			dataColumn2.AllowDBNull = true;
			dataColumn2.AutoIncrement = false;
			columns2.Add("Date", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns2.Add("Description", typeof(string));
			columns2.Add("Debit", typeof(decimal)).DefaultValue = 0;
			columns2.Add("Credit", typeof(decimal)).DefaultValue = 0;
			columns2.Add("Balance", typeof(decimal)).DefaultValue = 0;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Aging By Day]");
			DataColumnCollection columns3 = dataTable.Columns;
			DataColumn dataColumn3 = columns3.Add("StatementID", typeof(int));
			dataColumn3.AllowDBNull = true;
			dataColumn3.AutoIncrement = false;
			columns3.Add("Current", typeof(decimal)).DefaultValue = 0;
			columns3.Add("1to30Days", typeof(decimal)).DefaultValue = 0;
			columns3.Add("31to60Days", typeof(decimal)).DefaultValue = 0;
			columns3.Add("61to90Days", typeof(decimal)).DefaultValue = 0;
			columns3.Add("91to120Days", typeof(decimal)).DefaultValue = 0;
			columns3.Add("Over120Days", typeof(decimal)).DefaultValue = 0;
			columns3.Add("BalanceDue", typeof(decimal)).DefaultValue = 0;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Aging By Month]");
			DataColumnCollection columns4 = dataTable.Columns;
			DataColumn dataColumn4 = columns4.Add("StatementID", typeof(int));
			dataColumn4.AllowDBNull = true;
			dataColumn4.AutoIncrement = false;
			columns4.Add("Month1", typeof(string));
			columns4.Add("Month2", typeof(string));
			columns4.Add("Month3", typeof(string));
			columns4.Add("Month4", typeof(string));
			columns4.Add("Month5", typeof(string));
			columns4.Add("Month6", typeof(string));
			columns4.Add("Month1Balance", typeof(decimal)).DefaultValue = 0;
			columns4.Add("Month2Balance", typeof(decimal)).DefaultValue = 0;
			columns4.Add("Month3Balance", typeof(decimal)).DefaultValue = 0;
			columns4.Add("Month4Balance", typeof(decimal)).DefaultValue = 0;
			columns4.Add("Month5Balance", typeof(decimal)).DefaultValue = 0;
			columns4.Add("Month6Balance", typeof(decimal)).DefaultValue = 0;
			columns4.Add("BalanceDue", typeof(decimal)).DefaultValue = 0;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Unposted Checks]");
			DataColumnCollection columns5 = dataTable.Columns;
			DataColumn dataColumn5 = columns5.Add("StatementID", typeof(int));
			dataColumn5.AllowDBNull = true;
			dataColumn5.AutoIncrement = false;
			columns5.Add("TraactionDate", typeof(DateTime));
			columns5.Add("TraactionNumber", typeof(string));
			columns5.Add("BankName", typeof(string));
			columns5.Add("CheckDate", typeof(DateTime));
			columns5.Add("CheckNumber", typeof(string));
			columns5.Add("Amount", typeof(decimal)).DefaultValue = 0;
			base.Tables.Add(dataTable);
		}
	}
}
