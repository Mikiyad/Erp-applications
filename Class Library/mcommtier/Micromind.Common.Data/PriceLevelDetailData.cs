using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PriceLevelDetailData : DataSet
	{
		public const string PRICELEVELID_FIELD = "LevelID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string AMOUNT_FIELD = "Amount";

		public const string PRICELEVELDETAIL_TABLE = "[Price Level Details]";

		public DataTable PriceLevelDetailTable => base.Tables["[Price Level Details]"];

		public PriceLevelDetailData()
		{
			BuildDataTables();
		}

		public PriceLevelDetailData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Price Level Details]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LevelID", typeof(short));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			dataColumn = columns.Add("ProductID", typeof(int));
			dataColumn.AllowDBNull = false;
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			base.Tables.Add(dataTable);
		}
	}
}
