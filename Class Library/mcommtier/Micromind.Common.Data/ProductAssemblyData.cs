using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductAssemblyData : DataSet
	{
		public const string ASSEMBLIES_TABLE = "Assemblies";

		public const string ASSEMBLYDETAILS_TABLE = "[Assembly Details]";

		public const string ASSEMBLYID_FIELD = "AssemblyID";

		public const string ASSEMBLYDETAILID_FIELD = "AssemblyDetailID";

		public const string BOMID_FIELD = "BOMID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string ITEMID_FIELD = "ItemID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string BUILDDATE_FIELD = "BuildDate";

		public const string STOREID_FIELD = "StoreID";

		public DataTable AssembliesTable => base.Tables["Assemblies"];

		public DataTable AssemblyDetailsTable => base.Tables["[Assembly Details]"];

		public ProductAssemblyData()
		{
			BuildDataTables();
		}

		public ProductAssemblyData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Assemblies");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AssemblyID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("BOMID", typeof(int));
			columns.Add("ItemID", typeof(int));
			columns.Add("StoreID", typeof(int));
			columns.Add("Quantity", typeof(float));
			columns.Add("BuildDate", typeof(DateTime));
			columns.Add("Description", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Assembly Details]");
			columns = dataTable.Columns;
			dataColumn = columns.Add("AssemblyDetailID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("AssemblyID", typeof(int));
			columns.Add("Quantity", typeof(float));
			columns.Add("ItemID", typeof(int));
			base.Tables.Add(dataTable);
		}
	}
}
