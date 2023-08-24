using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CollateralData : DataSet
	{
		public const string COLLATERALID_FIELD = "CollateralID";

		public const string COLLATERALNAME_FIELD = "CollateralName";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string TYPEID_FIELD = "TypeID";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string RECEIVEDATE_FIELD = "ReceiveDate";

		public const string AMOUNT_FIELD = "Amount";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string BANKID_FIELD = "BankID";

		public const string DOCNO_FIELD = "DocNo";

		public const string ISRETURNED_FIELD = "IsReturned";

		public const string RETURNDATE_FIELD = "ReturnDate";

		public const string RETURNNOTE_FIELD = "ReturnNote";

		public const string RECEIVERNAME_FIELD = "ReceiverName";

		public const string CUSTODIANID_FIELD = "CustodianID";

		public const string STATUS_FIELD = "Status";

		public const string NOTE_FIELD = "Note";

		public const string COLLATERAL_TABLE = "Collateral";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CollateralTable => base.Tables["Collateral"];

		public CollateralData()
		{
			BuildDataTables();
		}

		public CollateralData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Collateral");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CollateralID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CollateralName", typeof(string)).AllowDBNull = false;
			columns.Add("PayeeType", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("TypeID", typeof(string));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("ReceiveDate", typeof(DateTime));
			columns.Add("Amount", typeof(decimal));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("BankID", typeof(string));
			columns.Add("DocNo", typeof(string));
			columns.Add("IsReturned", typeof(bool));
			columns.Add("ReturnDate", typeof(DateTime));
			columns.Add("ReturnNote", typeof(string));
			columns.Add("ReceiverName", typeof(string));
			columns.Add("CustodianID", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
