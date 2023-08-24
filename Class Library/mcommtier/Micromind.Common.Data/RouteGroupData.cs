using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class RouteGroupData : DataSet
	{
		public const string ROUTEGROUP_TABLE = "Route_Group";

		public const string ROUTEGROUPID_FIELD = "RouteGroupID";

		public const string ROUTEGROUPNAME_FIELD = "RouteGroupName";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string ROUTEGROUPDETAIL_TABLE = "Route_Group_Detail";

		public const string STEP_FIELD = "Step";

		public const string ROUTEID_FIELD = "RouteID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string PREVIOUSSTEP_FIELD = "PreviousStep";

		public const string REMARKS_FIELD = "Remarks";

		public DataTable RouteGroupTable => base.Tables["Route_Group"];

		public DataTable RouteGroupDetailTable => base.Tables["Route_Group_Detail"];

		public RouteGroupData()
		{
			BuildDataTables();
		}

		public RouteGroupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Route_Group");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("RouteGroupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("RouteGroupName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Route_Group_Detail");
			columns = dataTable.Columns;
			columns.Add("RouteGroupID", typeof(string));
			columns.Add("Step", typeof(string)).AllowDBNull = false;
			columns.Add("RouteID", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("PreviousStep", typeof(string));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
