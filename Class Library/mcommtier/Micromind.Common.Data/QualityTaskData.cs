using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class QualityTaskData : DataSet
	{
		public const string TASKID_FIELD = "TaskID";

		public const string CONTAINERNUMBER_FIELD = "ContainerNumber";

		public const string VENDORID_FIELD = "VendorID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string RECEIVEDATE_FIELD = "ReceiveDate";

		public const string GRNSYSDOCID_FIELD = "GRNSysDocID";

		public const string GRNVOUCHERID_FIELD = "GRNVoucherID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ASSIGNEDTO_FIELD = "AssignedTo";

		public const string QUALITYTASK_TABLE = "Quality_Task";

		public const string COMMODITYID_FIELD = "CommodityID";

		public const string STATUS_FIELD = "Status";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable QualityTaskTable => base.Tables["Quality_Task"];

		public QualityTaskData()
		{
			BuildDataTables();
		}

		public QualityTaskData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Quality_Task");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TaskID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("VendorID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("ReceiveDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("GRNSysDocID", typeof(string));
			columns.Add("GRNVoucherID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("AssignedTo", typeof(string));
			columns.Add("CommodityID", typeof(string));
			columns.Add("Status", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
