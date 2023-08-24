using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SurveyorSystem : MarshalByRefObject, ISurveyorSystem, IDisposable
	{
		private Config config;

		public SurveyorSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSurveyor(SurveyorData data)
		{
			return new Surveyor(config).InsertSurveyor(data);
		}

		public bool UpdateSurveyor(SurveyorData data)
		{
			return UpdateSurveyor(data, checkConcurrency: false);
		}

		public bool UpdateSurveyor(SurveyorData data, bool checkConcurrency)
		{
			return new Surveyor(config).UpdateSurveyor(data);
		}

		public SurveyorData GetSurveyor()
		{
			using (Surveyor surveyor = new Surveyor(config))
			{
				return surveyor.GetSurveyor();
			}
		}

		public bool DeleteSurveyor(string taskID)
		{
			using (Surveyor surveyor = new Surveyor(config))
			{
				return surveyor.DeleteSurveyor(taskID);
			}
		}

		public SurveyorData GetSurveyorByID(string id)
		{
			using (Surveyor surveyor = new Surveyor(config))
			{
				return surveyor.GetSurveyorByID(id);
			}
		}

		public DataSet GetSurveyorByFields(params string[] columns)
		{
			using (Surveyor surveyor = new Surveyor(config))
			{
				return surveyor.GetSurveyorByFields(columns);
			}
		}

		public DataSet GetSurveyorByFields(string[] ids, params string[] columns)
		{
			using (Surveyor surveyor = new Surveyor(config))
			{
				return surveyor.GetSurveyorByFields(ids, columns);
			}
		}

		public DataSet GetSurveyorByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Surveyor surveyor = new Surveyor(config))
			{
				return surveyor.GetSurveyorByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetSurveyorList()
		{
			using (Surveyor surveyor = new Surveyor(config))
			{
				return surveyor.GetSurveyorList();
			}
		}

		public DataSet GetSurveyorComboList()
		{
			using (Surveyor surveyor = new Surveyor(config))
			{
				return surveyor.GetSurveyorComboList();
			}
		}
	}
}
