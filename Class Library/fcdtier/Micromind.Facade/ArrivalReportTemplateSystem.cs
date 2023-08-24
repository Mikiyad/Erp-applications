using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ArrivalReportTemplateSystem : MarshalByRefObject, IArrivalReportTemplateSystem, IDisposable
	{
		private Config config;

		public ArrivalReportTemplateSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateArrivalReportTemplate(ArrivalReportTemplateData data)
		{
			return new ArrivalReportTemplate(config).InsertArrivalReportTemplate(data);
		}

		public bool UpdateArrivalReportTemplate(ArrivalReportTemplateData data)
		{
			return UpdateArrivalReportTemplate(data, checkConcurrency: false);
		}

		public bool UpdateArrivalReportTemplate(ArrivalReportTemplateData data, bool checkConcurrency)
		{
			return new ArrivalReportTemplate(config).UpdateArrivalReportTemplate(data);
		}

		public ArrivalReportTemplateData GetArrivalReportTemplate()
		{
			using (ArrivalReportTemplate arrivalReportTemplate = new ArrivalReportTemplate(config))
			{
				return arrivalReportTemplate.GetArrivalReportTemplate();
			}
		}

		public bool DeleteArrivalReportTemplate(string groupID)
		{
			using (ArrivalReportTemplate arrivalReportTemplate = new ArrivalReportTemplate(config))
			{
				return arrivalReportTemplate.DeleteArrivalReportTemplate(groupID);
			}
		}

		public ArrivalReportTemplateData GetArrivalReportTemplateByID(string id)
		{
			using (ArrivalReportTemplate arrivalReportTemplate = new ArrivalReportTemplate(config))
			{
				return arrivalReportTemplate.GetArrivalReportTemplateByID(id);
			}
		}

		public DataSet GetArrivalReportTemplateByFields(params string[] columns)
		{
			using (ArrivalReportTemplate arrivalReportTemplate = new ArrivalReportTemplate(config))
			{
				return arrivalReportTemplate.GetArrivalReportTemplateByFields(columns);
			}
		}

		public DataSet GetArrivalReportTemplateByFields(string[] ids, params string[] columns)
		{
			using (ArrivalReportTemplate arrivalReportTemplate = new ArrivalReportTemplate(config))
			{
				return arrivalReportTemplate.GetArrivalReportTemplateByFields(ids, columns);
			}
		}

		public DataSet GetArrivalReportTemplateByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ArrivalReportTemplate arrivalReportTemplate = new ArrivalReportTemplate(config))
			{
				return arrivalReportTemplate.GetArrivalReportTemplateByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetArrivalReportTemplateList()
		{
			using (ArrivalReportTemplate arrivalReportTemplate = new ArrivalReportTemplate(config))
			{
				return arrivalReportTemplate.GetArrivalReportTemplateList();
			}
		}

		public DataSet GetArrivalReportTemplateComboList()
		{
			using (ArrivalReportTemplate arrivalReportTemplate = new ArrivalReportTemplate(config))
			{
				return arrivalReportTemplate.GetArrivalReportTemplateComboList();
			}
		}
	}
}
