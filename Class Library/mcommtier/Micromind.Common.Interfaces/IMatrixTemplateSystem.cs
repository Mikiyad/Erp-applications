using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IMatrixTemplateSystem
	{
		bool CreateMatrixTemplate(MatrixTemplateData matrixTemplateData);

		bool UpdateMatrixTemplate(MatrixTemplateData matrixTemplateData);

		MatrixTemplateData GetMatrixTemplate();

		bool DeleteMatrixTemplate(string ID);

		MatrixTemplateData GetMatrixTemplateByID(string id);

		DataSet GetMatrixTemplateByFields(params string[] columns);

		DataSet GetMatrixTemplateByFields(string[] ids, params string[] columns);

		DataSet GetMatrixTemplateByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetMatrixTemplateList();

		DataSet GetMatrixTemplateComboList();
	}
}
