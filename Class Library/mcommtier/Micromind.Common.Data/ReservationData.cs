using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ReservationData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string RESERVEID_FIELD = "ReserveID";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public const string LOTNUMBER_FIELD = "LotNumber";

		public const string JOBID_FIELD = "JobID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string ORDERROWINDEX_FIELD = "OrderRowIndex";

		public const string VALIDDATEUPTO_FIELD = "ValidDateUpTo";

		public const string SOURCERESERVEID_FIELD = "SourceReserveID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string RESERVATIONDETAIL_TABLE = "Reservation_Detail";

		public DataTable ReservationDetailsTable => base.Tables["Reservation_Detail"];

		public ReservationData()
		{
			BuildDataTables();
		}

		public ReservationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			BuildDataTables();
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Reservation_Detail");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("ReserveID", typeof(long));
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("SysDocType", typeof(int));
			columns.Add("ProductID", typeof(string));
			columns.Add("LotNumber", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CustomerID", typeof(string));
			columns.Add("OrderRowIndex", typeof(long));
			columns.Add("Quantity", typeof(decimal));
			columns.Add("ValidDateUpTo", typeof(DateTime));
			columns.Add("SourceReserveID", typeof(long));
			base.Tables.Add(dataTable);
		}
	}
}
