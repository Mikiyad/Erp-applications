using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CompanyDocumentSystem : MarshalByRefObject, ICompanyDocumentSystem, IDisposable
	{
		private Config config;

		public CompanyDocumentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCompanyDocument(CompanyDocumentData data)
		{
			return new CompanyDocuments(config).InsertCompanyDocument(data);
		}

		public bool UpdateCompanyDocument(CompanyDocumentData data)
		{
			return UpdateCompanyDocument(data, checkConcurrency: false);
		}

		public bool UpdateCompanyDocument(CompanyDocumentData data, bool checkConcurrency)
		{
			return new CompanyDocuments(config).UpdateCompanyDocument(data);
		}

		public CompanyDocumentData GetCompanyDocument()
		{
			using (CompanyDocuments companyDocuments = new CompanyDocuments(config))
			{
				return companyDocuments.GetCompanyDocument();
			}
		}

		public bool DeleteCompanyDocument(string groupID)
		{
			using (CompanyDocuments companyDocuments = new CompanyDocuments(config))
			{
				return companyDocuments.DeleteCompanyDocument(groupID);
			}
		}

		public CompanyDocumentData GetCompanyDocumentByID(string id)
		{
			using (CompanyDocuments companyDocuments = new CompanyDocuments(config))
			{
				return companyDocuments.GetCompanyDocumentByID(id);
			}
		}

		public CompanyDocumentData GetCompanyDocumentsByCompanyID(string companyID)
		{
			using (CompanyDocuments companyDocuments = new CompanyDocuments(config))
			{
				return companyDocuments.GetCompanyDocumentsByCompanyID(companyID);
			}
		}

		public DataSet GetCompanyDocumentByFields(params string[] columns)
		{
			using (CompanyDocuments companyDocuments = new CompanyDocuments(config))
			{
				return companyDocuments.GetCompanyDocumentByFields(columns);
			}
		}

		public DataSet GetCompanyDocumentByFields(string[] ids, params string[] columns)
		{
			using (CompanyDocuments companyDocuments = new CompanyDocuments(config))
			{
				return companyDocuments.GetCompanyDocumentByFields(ids, columns);
			}
		}

		public DataSet GetCompanyDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CompanyDocuments companyDocuments = new CompanyDocuments(config))
			{
				return companyDocuments.GetCompanyDocumentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCompanyDocumentList()
		{
			using (CompanyDocuments companyDocuments = new CompanyDocuments(config))
			{
				return companyDocuments.GetCompanyDocumentList();
			}
		}

		public DataSet GetCompanyDocumentComboList()
		{
			using (CompanyDocuments companyDocuments = new CompanyDocuments(config))
			{
				return companyDocuments.GetCompanyDocumentComboList();
			}
		}
	}
}
