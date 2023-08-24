using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IUDFSystem
	{
		UDFData GetUDF();

		UDFData GetUDFByID(string id);

		DataSet GetUDFByFields(params string[] columns);

		DataSet GetUDFByFields(string[] ids, params string[] columns);

		DataSet GetUDFByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetUDFList(string udfTableName);

		DataSet GetUDFComboList();

		bool DeleteUDFColumn(string columnName, string udfTable);

		bool InsertUpdateUDFColumn(UDFData udfData, bool isUpdate);

		bool UpdateUDFColumnDataType(string columnName, string udfTable, SqlDbType newDataType, int length);

		bool UpdateUDFDisplayName(string columnName, string udfTable, string displayName);

		DataSet GetUDFEntryFields(string tableName);

		DataSet GetEntityUDFSchema(string tableName, string idColumn1, string idColumn2);
	}
}
