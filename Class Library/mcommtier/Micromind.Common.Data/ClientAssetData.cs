using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ClientAssetData : DataSet
	{
		public const string CLIENTASSET_TABLE = "ClientAsset";

		public const string CLIENTASSETID_FIELD = "ClientAssetID";

		public const string ISINACTIVE_FIELD = "Inactive";

		public const string CLIENTASSETNAME_FIELD = "ClientAssetName";

		public const string NOTE_FIELD = "Note";

		public const string BRANDID_FIELD = "BrandID";

		public const string MANUFACTURERID_FIELD = "ManufacturerID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string STARTDATE_FIELD = "StartDate";

		public const string SERIALNO_FIELD = "SerialNo";

		public const string SERVICEBYID_FIELD = "ServiceByID";

		public const string JOBID_FIELD = "JobID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ClientAssetTable => base.Tables["ClientAsset"];

		public ClientAssetData()
		{
			BuildDataTables();
		}

		public ClientAssetData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("ClientAsset");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ClientAssetID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ClientAssetName", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("Note", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("BrandID", typeof(string));
			columns.Add("ManufacturerID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("SerialNo", typeof(string));
			columns.Add("ServiceByID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
