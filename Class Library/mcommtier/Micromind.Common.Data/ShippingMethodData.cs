using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ShippingMethodData : DataSet
	{
		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string SHIPPINGMETHODNAME_FIELD = "ShippingMethodName";

		public const string PHONE_FIELD = "Phone";

		public const string CONTACTNAME_FIELD = "ContactName";

		public const string NOTE_FIELD = "Note";

		public const string SHIPPINGMETHOD_TABLE = "Shipping_Method";

		public const string INACTIVE_FIELD = "Inactive";

		public const string TRACKSHIPMENT_FIELD = "TrackShipment";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ShippingMethodTable => base.Tables["Shipping_Method"];

		public ShippingMethodData()
		{
			BuildDataTables();
		}

		public ShippingMethodData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Shipping_Method");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ShippingMethodID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ShippingMethodName", typeof(string)).AllowDBNull = false;
			columns.Add("Phone", typeof(string));
			columns.Add("ContactName", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			columns.Add("TrackShipment", typeof(bool)).DefaultValue = false;
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
