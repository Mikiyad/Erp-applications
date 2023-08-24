using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SystemDocumentSystem : MarshalByRefObject, ISystemDocumentSystem, IDisposable
	{
		private Config config;

		public SystemDocumentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSystemDocument(SystemDocumentData data)
		{
			return new SystemDocuments(config).InsertSystemDocument(data);
		}

		public bool UpdateSystemDocument(SystemDocumentData data)
		{
			return UpdateSystemDocument(data, checkConcurrency: false);
		}

		public bool UpdateSystemDocument(SystemDocumentData data, bool checkConcurrency)
		{
			return new SystemDocuments(config).UpdateSystemDocument(data);
		}

		public SystemDocumentData GetSystemDocument()
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetSystemDocument();
			}
		}

		public DataSet GetControls()
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetControls();
			}
		}

		public DataSet GetItemTransactionSystemDoc()
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetItemTransactionSystemDoc();
			}
		}

		public DataSet GetItemBarCodeSystemDoc()
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetItemBarCodeSystemDoc();
			}
		}

		public bool DeleteSystemDocument(string groupID)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.DeleteSystemDocument(groupID);
			}
		}

		public SystemDocumentData GetSystemDocumentByID(string id)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetSystemDocumentByID(id);
			}
		}

		public DataSet GetSystemDocumentByFields(params string[] columns)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetSystemDocumentByFields(columns);
			}
		}

		public DataSet GetSystemDocumentByFields(string[] ids, params string[] columns)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetSystemDocumentByFields(ids, columns);
			}
		}

		public DataSet GetSystemDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetSystemDocumentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetSystemDocumentList()
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetSystemDocumentList();
			}
		}

		public DataSet GetSystemDocumentComboList(string DefaultLocation)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetSystemDocumentComboList(DefaultLocation);
			}
		}

		public DataSet GetTransactionComboList()
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetTransactionComboList();
			}
		}

		public string GetNextDocumentNumber(string sysDocID)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				if (string.IsNullOrWhiteSpace(sysDocID))
				{
					return "";
				}
				return systemDocuments.GetNextDocumentNumber(sysDocID);
			}
		}

		public string GetNextDocumentNumber(string tableName, string fieldName)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetNextDocumentNumber(tableName, fieldName);
			}
		}

		public string GetNextNumber(string tableName, string fieldName)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetNextNumber(tableName, fieldName);
			}
		}

		public string GetNextEmployeeNumber()
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetNextEmployeeNumber();
			}
		}

		public string GetNextSponserEmployeeNumber(string SponserID)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetNextSponserEmployeeNumber(SponserID);
			}
		}

		public bool ExistDocumentNumber(string tableName, string fieldName, string sysDocID, string documentNumber)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.ExistDocumentNumber(tableName, fieldName, sysDocID, documentNumber, null);
			}
		}

		public string GetDocumentNumberPrefix(string sysDocID)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetDocumentNumberPrefix(sysDocID);
			}
		}

		public bool InsertEntityLinks(SystemDocumentData systemDocumentData, string sysDocID, SysDocEntityTypes entityType)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.InsertEntityLinks(systemDocumentData, sysDocID, entityType);
			}
		}

		public bool InsertExternalReportLinks(SystemDocumentData systemDocumentData, string reportID, SysDocEntityTypes entityType)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.InsertExternalReportLinks(systemDocumentData, reportID, entityType);
			}
		}

		public DataSet GetEntityLinks(string sysDocID, SysDocEntityTypes entityType)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetEntityLinks(sysDocID, entityType);
			}
		}

		public DataSet GetServiceSupplierLinks(SysDocEntityTypes entityType)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetServiceSupplierLinks(entityType);
			}
		}

		public DataSet GetExternalReportUserLinks(string reportID, SysDocEntityTypes entityType)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetExternalReportUserLinks(reportID, entityType);
			}
		}

		public string GetNextCardNumber(string tableName, string idFieldName)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetNextCardNumber(tableName, idFieldName);
			}
		}

		public string GetNextCardNumber(string tableName, string idFieldName, string filterColumnName, string filterColumnValue)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetNextCardNumber(tableName, idFieldName, filterColumnName, filterColumnValue);
			}
		}

		public bool HasuserAccess(string SysDocID, string LocationID)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.HasuserAccess(SysDocID, LocationID, null);
			}
		}

		public string GetCardUserID(string tableName, string fieldName, string fieldValue)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetCardUserID(tableName, fieldName, fieldValue);
			}
		}

		public string GetTransUserID(string tableName, string fieldName, string sysDocID, string documentNumber)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetTransUserID(tableName, fieldName, sysDocID, documentNumber, null);
			}
		}

		public DataSet CheckStimeStampStatus(string tableName, string RefsysDocID, string RefdocumentNumber)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.CheckStimeStampStatus(tableName, RefsysDocID, RefdocumentNumber);
			}
		}

		public bool InsertDocAttachementDetail(DataSet systemDocumentData, string sysDocID, SysDocTypes entityType)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.InsertDocAttachementDetail(systemDocumentData, sysDocID, entityType);
			}
		}

		public DataSet GetDocAttachements(string sysDocID, SysDocTypes entityType)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetDocAttachements(sysDocID, entityType);
			}
		}

		public bool InsertOpenListQuery(SystemDocumentData data)
		{
			return new SystemDocuments(config).InsertOpenListQuery(data);
		}

		public DataSet GetOpenQueryList(string ID)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetOpenQueryList(ID);
			}
		}

		public DataSet ExecuteOpenListQuery(string query, DateTime from, DateTime to, bool showVoid)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.ExecuteOpenListQuery(query, from, to, showVoid);
			}
		}

		public DataSet ExecuteOpenListQuery(string query, bool showVoid, bool isBuiltin = false)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.ExecuteOpenListQuery(query, showVoid, isBuiltin);
			}
		}

		public string ReplaceSystemParametersOpenList(string query, DateTime from, DateTime to, string sysDocID, string ComboSysDocID)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.ReplaceSystemParametersOpenList(query, from, to, sysDocID, ComboSysDocID);
			}
		}

		public string ReplaceVoidParameterOpenList(string query, bool isShowVoid)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.ReplaceVoidParameterOpenList(query, isShowVoid);
			}
		}

		public string GetSysDocList(DataTable dt, string locationID)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetSysDocList(dt, locationID);
			}
		}

		public DataSet GetSysDocList(string tableName)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetSysDocList(tableName);
			}
		}

		public SysDocTypes GetBarCodeSystemDocumentType(string sysDocID)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetBarCodeSystemDocumentType(sysDocID);
			}
		}

		public string GetNextCatgryWiseDocNumber(string tableName, string idFieldName, string filterColumnName, string filterColumnValue)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.GetNextCatgryWiseDocNumber(tableName, idFieldName, filterColumnName, filterColumnValue);
			}
		}

		public bool DeleteEntityLinks(string sysDocID, SysDocEntityTypes entityType)
		{
			using (SystemDocuments systemDocuments = new SystemDocuments(config))
			{
				return systemDocuments.DeleteEntityLinks(sysDocID, entityType);
			}
		}
	}
}
