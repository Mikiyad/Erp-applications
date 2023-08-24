using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PaymentMethodData : DataSet
	{
		public const string PAYMENTMETHODS_TABLE = "Payment_Method";

		public const string PAYMENTMETHODID_FIELD = "PaymentMethodID";

		public const string METHODNAME_FIELD = "MethodName";

		public const string METHODTYPE_FIELD = "MethodType";

		public const string DEFAULTACCOUNTID_FIELD = "DefaultAccountID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PaymentMethodTable => base.Tables["Payment_Method"];

		public PaymentMethodData()
		{
			BuildDataTables();
		}

		public PaymentMethodData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Payment_Method");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PaymentMethodID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("MethodName", typeof(string)).AllowDBNull = false;
			columns.Add("MethodType", typeof(byte));
			columns.Add("DefaultAccountID", typeof(int));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
