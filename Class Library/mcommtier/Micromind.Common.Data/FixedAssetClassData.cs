using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class FixedAssetClassData : DataSet
	{
		public const string ASSETCLASS_TABLE = "FixedAsset_Class";

		public const string ASSETCLASSID_FIELD = "AssetClassID";

		public const string ASSETCLASSNAME_FIELD = "AssetClassName";

		public const string ASSETACCOUNTID_FIELD = "AssetAccountID";

		public const string DEPACCOUNTID_FIELD = "DepAccountID";

		public const string ACCUMDEPACCOUNTID_FIELD = "AccumDepAccountID";

		public const string PROFITLOSSACCOUNTID_FIELD = "ProfitLossAccountID";

		public const string DEPFREQUENCY_FIELD = "DepFrequency";

		public const string DEPMETHOD_FIELD = "DepMethod";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable AssetClassTable => base.Tables["FixedAsset_Class"];

		public FixedAssetClassData()
		{
			BuildDataTables();
		}

		public FixedAssetClassData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("FixedAsset_Class");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AssetClassID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("AssetClassName", typeof(string)).AllowDBNull = false;
			columns.Add("AssetAccountID", typeof(string));
			columns.Add("DepAccountID", typeof(string));
			columns.Add("ProfitLossAccountID", typeof(string));
			columns.Add("AccumDepAccountID", typeof(string));
			columns.Add("DepFrequency", typeof(byte));
			columns.Add("DepMethod", typeof(byte));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
