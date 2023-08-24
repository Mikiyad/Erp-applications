using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ShipmentAddressData : DataSet
	{
		public const string SHIPMENTID_FIELD = "ShipmentID";

		public const string PARTNERID_FIELD = "PartnerID";

		public const string SHIPMENTNAME_FIELD = "ShipmentName";

		public const string LINE1_FIELD = "Line1";

		public const string LINE2_FIELD = "Line2";

		public const string LINE3_FIELD = "Line3";

		public const string LINE4_FIELD = "Line4";

		public const string LINE5_FIELD = "Line5";

		public const string DESCRIPTION_FIELD = "Description";

		public const string NOTE_FIELD = "Note";

		public const string SHIPMENTADDRESS_TABLE = "[Shipment Addresses]";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable ShipmentAddressTable => base.Tables["[Shipment Addresses]"];

		public ShipmentAddressData()
		{
			BuildDataTables();
		}

		public ShipmentAddressData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Shipment Addresses]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ShipmentID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PartnerID", typeof(int)).AllowDBNull = false;
			columns.Add("ShipmentName", typeof(string)).AllowDBNull = false;
			columns.Add("Line1", typeof(string));
			columns.Add("Line2", typeof(string));
			columns.Add("Line3", typeof(string));
			columns.Add("Line4", typeof(string));
			columns.Add("Line5", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
