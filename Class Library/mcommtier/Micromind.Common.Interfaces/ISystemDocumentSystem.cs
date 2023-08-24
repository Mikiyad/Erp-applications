using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISystemDocumentSystem
	{
		bool CreateSystemDocument(SystemDocumentData systemDocumentData);

		bool UpdateSystemDocument(SystemDocumentData systemDocumentData);

		SystemDocumentData GetSystemDocument();

		bool DeleteSystemDocument(string ID);

		SystemDocumentData GetSystemDocumentByID(string id);

		DataSet GetSystemDocumentByFields(params string[] columns);

		DataSet GetSystemDocumentByFields(string[] ids, params string[] columns);

		DataSet GetSystemDocumentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetSystemDocumentList();

		DataSet GetSystemDocumentComboList(string defaultLocation);

		DataSet GetTransactionComboList();

		string GetNextDocumentNumber(string sysDocID);

		string GetNextDocumentNumber(string tableName, string fieldName);

		string GetNextNumber(string tableName, string fieldName);

		string GetNextEmployeeNumber();

		string GetNextSponserEmployeeNumber(string SponsorID);

		bool ExistDocumentNumber(string tableName, string fieldName, string sysDocID, string documentNumber);

		string GetDocumentNumberPrefix(string sysDocID);

		bool InsertEntityLinks(SystemDocumentData systemDocumentData, string sysDocID, SysDocEntityTypes entityType);

		bool InsertExternalReportLinks(SystemDocumentData systemDocumentData, string ReportID, SysDocEntityTypes entityType);

		DataSet GetEntityLinks(string sysDocID, SysDocEntityTypes entityType);

		DataSet GetServiceSupplierLinks(SysDocEntityTypes entityType);

		DataSet GetExternalReportUserLinks(string reportID, SysDocEntityTypes entityType);

		string GetNextCardNumber(string tableName, string idFieldName);

		string GetNextCardNumber(string tableName, string idFieldName, string filterColumnName, string filterColumnValue);

		bool HasuserAccess(string sysDocID, string LocationID);

		string GetCardUserID(string tableName, string fieldName, string fieldValue);

		string GetTransUserID(string tableName, string fieldName, string sysDocID, string documentNumber);

		DataSet CheckStimeStampStatus(string tableName, string RefsysDocID, string RefdocumentNumber);

		bool InsertDocAttachementDetail(DataSet systemDocumentData, string sysDocID, SysDocTypes entityType);

		DataSet GetDocAttachements(string sysDocID, SysDocTypes entityType);

		DataSet GetItemTransactionSystemDoc();

		DataSet GetItemBarCodeSystemDoc();

		bool InsertOpenListQuery(SystemDocumentData systemDocumentData);

		DataSet GetOpenQueryList(string ID);

		DataSet ExecuteOpenListQuery(string query, DateTime from, DateTime to, bool showVoid);

		DataSet ExecuteOpenListQuery(string query, bool showVoid, bool isBuiltin = false);

		string ReplaceSystemParametersOpenList(string query, DateTime from, DateTime to, string sysDocID, string ComboSysDocID);

		string ReplaceVoidParameterOpenList(string query, bool isShowVoid);

		string GetNextCatgryWiseDocNumber(string tableName, string idFieldName, string filterColumnName, string filterColumnValue);

		string GetSysDocList(DataTable dt, string locationID);

		DataSet GetSysDocList(string tableName);

		SysDocTypes GetBarCodeSystemDocumentType(string sysDocID);

		DataSet GetControls();

		bool DeleteEntityLinks(string sysDocID, SysDocEntityTypes entityType);
	}
}
