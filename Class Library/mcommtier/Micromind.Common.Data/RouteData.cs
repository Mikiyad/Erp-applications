using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class RouteData : DataSet
	{
		public const string ROUTE_TABLE = "Route";

		public const string ROUTEID_FIELD = "RouteID";

		public const string ISINACTIVE_FIELD = "Inactive";

		public const string ROUTENAME_FIELD = "RouteName";

		public const string REMARKS_FIELD = "Remarks";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string BOMID_FIELD = "BomID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable RouteTable => base.Tables["Route"];

		public RouteData()
		{
			BuildDataTables();
		}

		public RouteData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Route");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("RouteID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("RouteName", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("Remarks", typeof(string));
			columns.Add("LocationID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
