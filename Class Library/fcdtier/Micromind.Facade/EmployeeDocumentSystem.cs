using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeDocumentSystem : MarshalByRefObject, IEmployeeDocumentSystem, IDisposable
	{
		private Config config;

		public EmployeeDocumentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeDocument(EmployeeDocumentData data)
		{
			return new EmployeeDocuments(config).InsertEmployeeDocument(data);
		}

		public bool UpdateEmployeeDocument(EmployeeDocumentData data)
		{
			return UpdateEmployeeDocument(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeDocument(EmployeeDocumentData data, bool checkConcurrency)
		{
			return new EmployeeDocuments(config).UpdateEmployeeDocument(data);
		}

		public EmployeeDocumentData GetEmployeeDocument()
		{
			using (EmployeeDocuments employeeDocuments = new EmployeeDocuments(config))
			{
				return employeeDocuments.GetEmployeeDocument();
			}
		}

		public bool DeleteEmployeeDocument(string groupID)
		{
			using (EmployeeDocuments employeeDocuments = new EmployeeDocuments(config))
			{
				return employeeDocuments.DeleteEmployeeDocument(groupID);
			}
		}

		public EmployeeDocumentData GetEmployeeDocumentByID(string id)
		{
			using (EmployeeDocuments employeeDocuments = new EmployeeDocuments(config))
			{
				return employeeDocuments.GetEmployeeDocumentByID(id);
			}
		}

		public EmployeeDocumentData GetEmployeeDocumentsByEmployeeID(string employeeID)
		{
			using (EmployeeDocuments employeeDocuments = new EmployeeDocuments(config))
			{
				return employeeDocuments.GetEmployeeDocumentsByEmployeeID(employeeID);
			}
		}

		public DataSet GetEmployeeDocumentByFields(params string[] columns)
		{
			using (EmployeeDocuments employeeDocuments = new EmployeeDocuments(config))
			{
				return employeeDocuments.GetEmployeeDocumentByFields(columns);
			}
		}

		public DataSet GetEmployeeDocumentByFields(string[] ids, params string[] columns)
		{
			using (EmployeeDocuments employeeDocuments = new EmployeeDocuments(config))
			{
				return employeeDocuments.GetEmployeeDocumentByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeDocuments employeeDocuments = new EmployeeDocuments(config))
			{
				return employeeDocuments.GetEmployeeDocumentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeDocumentList()
		{
			using (EmployeeDocuments employeeDocuments = new EmployeeDocuments(config))
			{
				return employeeDocuments.GetEmployeeDocumentList();
			}
		}

		public DataSet GetEmployeeDocumentComboList()
		{
			using (EmployeeDocuments employeeDocuments = new EmployeeDocuments(config))
			{
				return employeeDocuments.GetEmployeeDocumentComboList();
			}
		}
	}
}
