using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PaymentTermData : DataSet
	{
		public const string TERMS_TABLE = "Payment_Term";

		public const string TERMINSTALLMENTS_TABLE = "Payment_Term_Installment";

		public const string PAYMENTTERMID_FIELD = "PaymentTermID";

		public const string TERMNAME_FIELD = "TermName";

		public const string NETDAYS_FIELD = "NetDays";

		public const string DISCOUNTDAYS_FIELD = "DiscountDays";

		public const string DISCOUNTAMOUNT_FIELD = "DiscountAmount";

		public const string DISCOUNTTYPE_FIELD = "DiscountType";

		public const string TERMTYPE_FIELD = "TermType";

		public const string ISINSTALLMENTS_FIELD = "IsInstallments";

		public const string INACTIVE_FIELD = "Inactive";

		public const string NOTE_FIELD = "Note";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string PERCENTAGE_FIELD = "Percentage";

		public const string DAYS_FIELD = "Days";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TermTable => base.Tables["Payment_Term"];

		public DataTable InstallmentsTable => base.Tables["Payment_Term_Installment"];

		public PaymentTermData()
		{
			BuildDataTables();
		}

		public PaymentTermData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Payment_Term");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PaymentTermID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TermName", typeof(string));
			columns.Add("NetDays", typeof(short)).DefaultValue = 0;
			columns.Add("DiscountDays", typeof(short)).DefaultValue = 0;
			columns.Add("DiscountAmount", typeof(float)).DefaultValue = 0;
			columns.Add("DiscountType", typeof(short)).DefaultValue = 1;
			columns.Add("TermType", typeof(short)).DefaultValue = 1;
			columns.Add("IsInstallments", typeof(bool));
			columns.Add("Inactive", typeof(bool)).DefaultValue = 0;
			columns.Add("Note", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Payment_Term_Installment");
			columns = dataTable.Columns;
			dataColumn = columns.Add("PaymentTermID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("RowIndex", typeof(short));
			columns.Add("Days", typeof(short)).DefaultValue = 0;
			columns.Add("Percentage", typeof(short)).DefaultValue = 0;
			columns.Add("TermType", typeof(short));
			base.Tables.Add(dataTable);
		}

		public static decimal GetDiscount(decimal invoiceAmount, byte netDays, byte cashDiscountDays, float cashDiscountPercent, DateTime invoiceDate, DateTime paymentDate)
		{
			TimeSpan timeSpan = paymentDate - invoiceDate;
			if (cashDiscountPercent == 0f)
			{
				return 0m;
			}
			if (timeSpan.Days <= cashDiscountDays)
			{
				return invoiceAmount * (decimal)cashDiscountPercent;
			}
			return 0m;
		}
	}
}
