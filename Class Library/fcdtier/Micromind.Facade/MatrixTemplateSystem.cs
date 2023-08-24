using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class MatrixTemplateSystem : MarshalByRefObject, IMatrixTemplateSystem, IDisposable
	{
		private Config config;

		public MatrixTemplateSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateMatrixTemplate(MatrixTemplateData data)
		{
			return new MatrixTemplate(config).InsertUpdateMatrixTemplate(data, isUpdate: false);
		}

		public bool UpdateMatrixTemplate(MatrixTemplateData data)
		{
			return UpdateMatrixTemplate(data, checkConcurrency: false);
		}

		public bool UpdateMatrixTemplate(MatrixTemplateData data, bool checkConcurrency)
		{
			return new MatrixTemplate(config).InsertUpdateMatrixTemplate(data, isUpdate: true);
		}

		public MatrixTemplateData GetMatrixTemplate()
		{
			using (MatrixTemplate matrixTemplate = new MatrixTemplate(config))
			{
				return matrixTemplate.GetMatrixTemplate();
			}
		}

		public bool DeleteMatrixTemplate(string groupID)
		{
			using (MatrixTemplate matrixTemplate = new MatrixTemplate(config))
			{
				return matrixTemplate.DeleteMatrixTemplate(groupID);
			}
		}

		public MatrixTemplateData GetMatrixTemplateByID(string id)
		{
			using (MatrixTemplate matrixTemplate = new MatrixTemplate(config))
			{
				return matrixTemplate.GetMatrixTemplateByID(id);
			}
		}

		public DataSet GetMatrixTemplateByFields(params string[] columns)
		{
			using (MatrixTemplate matrixTemplate = new MatrixTemplate(config))
			{
				return matrixTemplate.GetMatrixTemplateByFields(columns);
			}
		}

		public DataSet GetMatrixTemplateByFields(string[] ids, params string[] columns)
		{
			using (MatrixTemplate matrixTemplate = new MatrixTemplate(config))
			{
				return matrixTemplate.GetMatrixTemplateByFields(ids, columns);
			}
		}

		public DataSet GetMatrixTemplateByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (MatrixTemplate matrixTemplate = new MatrixTemplate(config))
			{
				return matrixTemplate.GetMatrixTemplateByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetMatrixTemplateList()
		{
			using (MatrixTemplate matrixTemplate = new MatrixTemplate(config))
			{
				return matrixTemplate.GetMatrixTemplateList();
			}
		}

		public DataSet GetMatrixTemplateComboList()
		{
			using (MatrixTemplate matrixTemplate = new MatrixTemplate(config))
			{
				return matrixTemplate.GetMatrixTemplateComboList();
			}
		}
	}
}
