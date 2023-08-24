using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class UDFSystem : MarshalByRefObject, IUDFSystem, IDisposable
	{
		private Config config;

		public UDFSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public UDFData GetUDF()
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.GetUDF();
			}
		}

		public UDFData GetUDFByID(string id)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.GetUDFByID(id);
			}
		}

		public DataSet GetUDFByFields(params string[] columns)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.GetUDFByFields(columns);
			}
		}

		public DataSet GetUDFByFields(string[] ids, params string[] columns)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.GetUDFByFields(ids, columns);
			}
		}

		public DataSet GetUDFByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.GetUDFByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetUDFList(string udfTable)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.GetUDFList(udfTable);
			}
		}

		public DataSet GetUDFComboList()
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.GetUDFComboList();
			}
		}

		public bool DeleteUDFColumn(string columnName, string udfTable)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.DeleteUDFColumn(columnName, udfTable);
			}
		}

		public bool InsertUpdateUDFColumn(UDFData udfData, bool isUpdate)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.InsertUpdateUDFColumn(udfData, isUpdate);
			}
		}

		public bool UpdateUDFColumnDataType(string columnName, string udfTable, SqlDbType newDataType, int length)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.UpdateUDFColumnDataType(columnName, udfTable, newDataType, length);
			}
		}

		public bool UpdateUDFDisplayName(string columnName, string udfTable, string displayName)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.UpdateUDFDisplayName(columnName, udfTable, displayName);
			}
		}

		public DataSet GetUDFEntryFields(string udfTable)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.GetUDFEntryFields(udfTable);
			}
		}

		public DataSet GetEntityUDFSchema(string tableName, string idColumn1, string idColumn2)
		{
			using (UDF uDF = new UDF(config))
			{
				return uDF.GetEntityUDFSchema(tableName, idColumn1, idColumn2);
			}
		}
	}
}
