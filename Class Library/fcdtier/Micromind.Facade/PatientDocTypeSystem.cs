using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PatientDocTypeSystem : MarshalByRefObject, IPatientDocTypeSystem, IDisposable
	{
		private Config config;

		public PatientDocTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePatientDocType(PatientDocTypeData data)
		{
			return new PatientDocType(config).InsertPatientDocType(data);
		}

		public bool UpdatePatientDocType(PatientDocTypeData data)
		{
			return UpdatePatientDocType(data, checkConcurrency: false);
		}

		public bool UpdatePatientDocType(PatientDocTypeData data, bool checkConcurrency)
		{
			return new PatientDocType(config).UpdatePatientDocType(data);
		}

		public PatientDocTypeData GetPatientDocType()
		{
			using (PatientDocType patientDocType = new PatientDocType(config))
			{
				return patientDocType.GetPatientDocType();
			}
		}

		public bool DeletePatientDocType(string groupID)
		{
			using (PatientDocType patientDocType = new PatientDocType(config))
			{
				return patientDocType.DeletePatientDocType(groupID);
			}
		}

		public PatientDocTypeData GetPatientDocTypeByID(string id)
		{
			using (PatientDocType patientDocType = new PatientDocType(config))
			{
				return patientDocType.GetPatientDocTypeByID(id);
			}
		}

		public DataSet GetPatientDocTypeByFields(params string[] columns)
		{
			using (PatientDocType patientDocType = new PatientDocType(config))
			{
				return patientDocType.GetPatientDocTypeByFields(columns);
			}
		}

		public DataSet GetPatientDocTypeByFields(string[] ids, params string[] columns)
		{
			using (PatientDocType patientDocType = new PatientDocType(config))
			{
				return patientDocType.GetPatientDocTypeByFields(ids, columns);
			}
		}

		public DataSet GetPatientDocTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PatientDocType patientDocType = new PatientDocType(config))
			{
				return patientDocType.GetPatientDocTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPatientDocTypeList()
		{
			using (PatientDocType patientDocType = new PatientDocType(config))
			{
				return patientDocType.GetPatientDocTypeList();
			}
		}

		public DataSet GetPatientDocTypeComboList()
		{
			using (PatientDocType patientDocType = new PatientDocType(config))
			{
				return patientDocType.GetPatientDocTypeComboList();
			}
		}
	}
}
