using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PatientDocumentSystem : MarshalByRefObject, IPatientDocumentSystem, IDisposable
	{
		private Config config;

		public PatientDocumentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePatientDocument(PatientDocumentData data)
		{
			return new PatientDocuments(config).InsertPatientDocument(data);
		}

		public bool UpdatePatientDocument(PatientDocumentData data)
		{
			return UpdatePatientDocument(data, checkConcurrency: false);
		}

		public bool UpdatePatientDocument(PatientDocumentData data, bool checkConcurrency)
		{
			return new PatientDocuments(config).UpdatePatientDocument(data);
		}

		public PatientDocumentData GetPatientDocument()
		{
			using (PatientDocuments patientDocuments = new PatientDocuments(config))
			{
				return patientDocuments.GetPatientDocument();
			}
		}

		public bool DeletePatientDocument(string groupID)
		{
			using (PatientDocuments patientDocuments = new PatientDocuments(config))
			{
				return patientDocuments.DeletePatientDocument(groupID);
			}
		}

		public PatientDocumentData GetPatientDocumentByID(string id)
		{
			using (PatientDocuments patientDocuments = new PatientDocuments(config))
			{
				return patientDocuments.GetPatientDocumentByID(id);
			}
		}

		public PatientDocumentData GetPatientDocumentsByPatientID(string PatientID)
		{
			using (PatientDocuments patientDocuments = new PatientDocuments(config))
			{
				return patientDocuments.GetPatientDocumentsByPatientID(PatientID);
			}
		}

		public DataSet GetPatientDocumentByFields(params string[] columns)
		{
			using (PatientDocuments patientDocuments = new PatientDocuments(config))
			{
				return patientDocuments.GetPatientDocumentByFields(columns);
			}
		}

		public DataSet GetPatientDocumentByFields(string[] ids, params string[] columns)
		{
			using (PatientDocuments patientDocuments = new PatientDocuments(config))
			{
				return patientDocuments.GetPatientDocumentByFields(ids, columns);
			}
		}

		public DataSet GetPatientDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PatientDocuments patientDocuments = new PatientDocuments(config))
			{
				return patientDocuments.GetPatientDocumentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPatientDocumentList()
		{
			using (PatientDocuments patientDocuments = new PatientDocuments(config))
			{
				return patientDocuments.GetPatientDocumentList();
			}
		}

		public DataSet GetPatientDocumentComboList()
		{
			using (PatientDocuments patientDocuments = new PatientDocuments(config))
			{
				return patientDocuments.GetPatientDocumentComboList();
			}
		}
	}
}
