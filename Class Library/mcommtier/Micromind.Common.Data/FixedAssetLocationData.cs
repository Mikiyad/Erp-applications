using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class FixedAssetLocationData : DataSet
	{
		public const string ASSETLOCATION_TABLE = "FixedAsset_Location";

		public const string ASSETLOCATIONID_FIELD = "AssetLocationID";

		public const string ASSETLOCATIONNAME_FIELD = "AssetLocationName";

		public const string DEPACCOUNTID_FIELD = "DepAccountID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable AssetLocationTable => base.Tables["FixedAsset_Location"];

		public FixedAssetLocationData()
		{
			BuildDataTables();
		}

		public FixedAssetLocationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("FixedAsset_Location");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AssetLocationID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("AssetLocationName", typeof(string)).AllowDBNull = false;
			columns.Add("DepAccountID", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("CountryID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
