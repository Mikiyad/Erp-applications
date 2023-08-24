using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyService : StoreObject
	{
		public const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string REPORTINGDATE_PARM = "@ReportingDate";

		private const string PROPERTYID_PARM = "@PropertyID";

		private const string UNITID_PARM = "@UnitID";

		public const string TENANTID_PARM = "@TenantID";

		public const string PRIORITYSTATUS_PARM = "@PriorityStatus";

		public const string REQUIREDDATETIME_PARM = "@RequiredDatetime";

		public const string CONVENIENTDATETIME_PARM = "@ConvenientDatetime";

		public const string REQUESTNOTES_PARM = "@RequestNotes";

		public const string PROPERTYSERVICEREQUEST_TABLE = "Property_Service_Request";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string PROPERTYSERVICEFACILITYDETAIL_TABLE = "Property_ServiceFacility_Detail";

		public const string SERVICEFACILITYSYSDOCID_PARM = "@SysDocID";

		public const string SERVICEFACILITYVOUCHERID_PARM = "@VoucherID";

		private const string SERVICEFACILITYROWINDEX_PARM = "@RowIndex";

		public const string FACILITYID_PARM = "@FacilityID";

		public const string PROPERTYSERVICETYPEDETAIL_TABLE = "Property_ServiceType_Detail";

		public const string SERVICETYPESYSDOCID_PARM = "@SysDocID";

		public const string SERVICETYPEVOUCHERID_PARM = "@VoucherID";

		private const string SERVICETYPEROWINDEX_PARM = "@RowIndex";

		private const string SERVICETYPEID_PARM = "@ServiceTypeID";

		public const string PROPERTYSERVICEASSIGN_TABLE = "Property_Service_Assign";

		public const string SERVICEASSIGNSYSDOCID_PARM = "@SysDocID";

		public const string SERVICEASSIGNVOUCHERID_PARM = "@VoucherID";

		public const string SERVICEASSIGNSOURCESYSDOCID_PARM = "@SourceSysDocID";

		public const string SERVICEASSIGNSOURCEVOUCHERID_PARM = "@SourceVoucherID";

		public const string SERVICEASSIGNSERVICEPROVIDERID_PARM = "@ServiceProviderID";

		public const string SERVICEASSIGNPLANNEDDATE_PARM = "@PlannedDate";

		public const string SERVICEASSIGNSTATUSDATE_PARM = "@StatusDate";

		public const string SERVICEASSIGNSTATUS_PARM = "@Status";

		public const string SERVICEASSIGNAMOUNT_PARM = "@Amount";

		public const string SERVICEASSIGNREMARKS_PARM = "@Remarks";

		private const string SERVICEASSIGNCREATEDBY_PARM = "@CreatedBy";

		private const string SERVICEASSIGNDATECREATED_PARM = "@DateCreated";

		private const string SERVICEASSIGNUPDATEDBY_PARM = "@UpdatedBy";

		private const string SERVICEASSIGNDATEUPDATED_PARM = "@DateUpdated";

		public PropertyService(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Service_Request", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ReportingDate", "@ReportingDate"), new FieldValue("PropertyID", "@PropertyID"), new FieldValue("UnitID", "@UnitID"), new FieldValue("TenantID", "@TenantID"), new FieldValue("PriorityStatus", "@PriorityStatus"), new FieldValue("RequiredDatetime", "@RequiredDatetime"), new FieldValue("ConvenientDatetime", "@ConvenientDatetime"), new FieldValue("RequestNotes", "@RequestNotes"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Service_Request", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ReportingDate", SqlDbType.DateTime);
			parameters.Add("@PropertyID", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@TenantID", SqlDbType.NVarChar);
			parameters.Add("@PriorityStatus", SqlDbType.NVarChar);
			parameters.Add("@RequiredDatetime", SqlDbType.DateTime);
			parameters.Add("@ConvenientDatetime", SqlDbType.DateTime);
			parameters.Add("@RequestNotes", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ReportingDate"].SourceColumn = "ReportingDate";
			parameters["@PropertyID"].SourceColumn = "PropertyID";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@TenantID"].SourceColumn = "TenantID";
			parameters["@PriorityStatus"].SourceColumn = "PriorityStatus";
			parameters["@RequiredDatetime"].SourceColumn = "RequiredDatetime";
			parameters["@ConvenientDatetime"].SourceColumn = "ConvenientDatetime";
			parameters["@RequestNotes"].SourceColumn = "RequestNotes";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateServiceTypeDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_ServiceType_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex", isUpdateConditionField: true), new FieldValue("ServiceTypeID", "@ServiceTypeID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_ServiceType_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateServiceTypeDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateServiceTypeDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateServiceTypeDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@ServiceTypeID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@ServiceTypeID"].SourceColumn = "ServiceTypeID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateFacilityTypeDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_ServiceFacility_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex", isUpdateConditionField: true), new FieldValue("FacilityID", "@FacilityID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_ServiceFacility_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFacilityTypeDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFacilityTypeDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFacilityTypeDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@FacilityID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@FacilityID"].SourceColumn = "FacilityID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateServiceAssignDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Service_Assign", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("ServiceProviderID", "@ServiceProviderID"), new FieldValue("PlannedDate", "@PlannedDate"), new FieldValue("StatusDate", "@StatusDate"), new FieldValue("Status", "@Status"), new FieldValue("Amount", "@Amount"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Service_Assign", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateServiceAssignDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateServiceAssignDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateServiceAssignDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ServiceProviderID", SqlDbType.NVarChar);
			parameters.Add("@PlannedDate", SqlDbType.DateTime);
			parameters.Add("@StatusDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@ServiceProviderID"].SourceColumn = "ServiceProviderID";
			parameters["@PlannedDate"].SourceColumn = "PlannedDate";
			parameters["@StatusDate"].SourceColumn = "StatusDate";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertPropertyService(PropertyServiceData accountPropertyServiceData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = accountPropertyServiceData.PropertyServiceDetailTable.Rows[0];
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (flag && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Property_Service_Request", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text2 = accountPropertyServiceData.PropertyServiceDetailTable.Rows[0]["VoucherID"].ToString();
				flag &= DeleteServiceTypeDetailsRows(text2, sqlTransaction);
				flag &= DeleteServiceFacilityDetailsRows(text2, sqlTransaction);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Insert(accountPropertyServiceData, "Property_Service_Request", insertUpdateCommand);
				if (accountPropertyServiceData.Tables["Property_ServiceType_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateServiceTypeDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPropertyServiceData, "Property_ServiceType_Detail", insertUpdateCommand);
				}
				if (accountPropertyServiceData.Tables["Property_ServiceFacility_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateFacilityTypeDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPropertyServiceData, "Property_ServiceFacility_Detail", insertUpdateCommand);
				}
				AddActivityLog("Property Service Request", text, sysDocID, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Service_Request", "VoucherID", text2, sqlTransaction, isInsert: true);
				if (!flag)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Property_Service_Request", "VoucherID", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool UpdatePropertyService(PropertyServiceData accountPropertyServiceData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				string text = accountPropertyServiceData.PropertyServiceDetailTable.Rows[0]["VoucherID"].ToString();
				string sysDocID = accountPropertyServiceData.PropertyServiceDetailTable.Rows[0]["SysDocID"].ToString();
				flag &= DeleteServiceTypeDetailsRows(text.ToString(), sqlTransaction);
				flag &= DeleteServiceFacilityDetailsRows(text.ToString(), sqlTransaction);
				flag = Update(accountPropertyServiceData, "Property_Service_Request", insertUpdateCommand);
				if (accountPropertyServiceData.Tables["Property_ServiceType_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateServiceTypeDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPropertyServiceData, "Property_ServiceType_Detail", insertUpdateCommand);
				}
				if (accountPropertyServiceData.Tables["Property_ServiceFacility_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateFacilityTypeDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPropertyServiceData, "Property_ServiceFacility_Detail", insertUpdateCommand);
				}
				if (!flag)
				{
					return flag;
				}
				UpdateTableRowByID("Property_Service_Request", "VoucherID", "DateUpdated", text, DateTime.Now, sqlTransaction);
				AddActivityLog("Property Service Request", text, sysDocID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Service_Request", "VoucherID", text, sqlTransaction, isInsert: false);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		internal bool DeleteServiceTypeDetailsRows(string VoucherID, SqlTransaction sqlTransaction)
		{
			bool result = true;
			try
			{
				string commandText = "DELETE FROM Property_ServiceType_Detail WHERE VoucherID = '" + VoucherID + "'";
				result = Delete(commandText, sqlTransaction);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		internal bool DeleteServiceFacilityDetailsRows(string VoucherID, SqlTransaction sqlTransaction)
		{
			bool result = true;
			try
			{
				string commandText = "DELETE FROM Property_ServiceFacility_Detail WHERE VoucherID = '" + VoucherID + "'";
				result = Delete(commandText, sqlTransaction);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public PropertyServiceData GetPropertyService()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Service_Request");
			PropertyServiceData propertyServiceData = new PropertyServiceData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyServiceData, "Property_Service_Request", sqlBuilder);
			return propertyServiceData;
		}

		public bool InsertPropertyServiceAssign(PropertyServiceData propertyServiceAssignData)
		{
			bool flag = true;
			SqlCommand insertUpdateServiceAssignDetailCommand = GetInsertUpdateServiceAssignDetailCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = propertyServiceAssignData.PropertyServiceAssignDetailTable.Rows[0];
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = propertyServiceAssignData.PropertyServiceAssignDetailTable.Rows[0]["VoucherID"].ToString();
				string sysDocID = propertyServiceAssignData.PropertyServiceAssignDetailTable.Rows[0]["SysDocID"].ToString();
				if (flag && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Property_Service_Assign", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateServiceAssignDetailCommand.Transaction = sqlTransaction;
				flag = Insert(propertyServiceAssignData, "Property_Service_Assign", insertUpdateServiceAssignDetailCommand);
				string fieldIDValue = propertyServiceAssignData.PropertyServiceAssignDetailTable.Rows[0]["VoucherID"].ToString();
				AddActivityLog("Property Service Assign", text, sysDocID, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Service_Assign", "VoucherID", fieldIDValue, sqlTransaction, isInsert: true);
				if (!flag)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Property_Service_Request", "VoucherID", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool UpdatePropertyServiceAssign(PropertyServiceData propertyServiceAssignData)
		{
			bool flag = true;
			SqlCommand insertUpdateServiceAssignDetailCommand = GetInsertUpdateServiceAssignDetailCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateServiceAssignDetailCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(propertyServiceAssignData, "Property_Service_Assign", insertUpdateServiceAssignDetailCommand);
				if (!flag)
				{
					return flag;
				}
				string text = propertyServiceAssignData.PropertyServiceAssignDetailTable.Rows[0]["VoucherID"].ToString();
				string sysDocID = propertyServiceAssignData.PropertyServiceAssignDetailTable.Rows[0]["SysDocID"].ToString();
				UpdateTableRowByID("Property_Service_Assign", "VoucherID", "DateUpdated", text, DateTime.Now, sqlTransaction);
				AddActivityLog("Property Service Assign", text, sysDocID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Service_Assign", "VoucherID", text, sqlTransaction, isInsert: false);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool DeletePropertyServiceRequest(string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag = DeleteServiceFacilityDetailsRows(voucherID, sqlTransaction);
				flag = DeleteServiceTypeDetailsRows(voucherID, sqlTransaction);
				string commandText = "DELETE FROM Property_Service_Request WHERE VoucherID = '" + voucherID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Property Service Detail", voucherID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeletePropertyServiceAssign(string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM Property_Service_Assign WHERE VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("PropertyServiceAssign", voucherID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public PropertyServiceData GetPropertyServiceByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "VoucherID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Property_Service_Request";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PropertyServiceData propertyServiceData = new PropertyServiceData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyServiceData, "Property_Service_Request", sqlBuilder);
			if (propertyServiceData == null || propertyServiceData.Tables.Count == 0 || propertyServiceData.Tables[0].Rows.Count == 0)
			{
				return propertyServiceData;
			}
			string textCommand = " SELECT PSF.VoucherID,PSF.FacilityID [Doc ID],GenericListName AS Number FROM Property_ServiceFacility_Detail PSF\r\n                            left JOIN Generic_List  ON  PSF.FacilityID=  Generic_List.GenericListID\r\n                            WHERE PSF.VoucherID='" + id + "'  AND Generic_List.GenericListType = 12 ";
			FillDataSet(propertyServiceData, "Property_ServiceFacility_Detail", textCommand);
			textCommand = "";
			textCommand = "\t\tSELECT PST.VoucherID,PST.ServiceTypeID [Doc ID],GenericListName AS Number FROM Property_ServiceType_Detail PST\r\n                            INNER JOIN Generic_List  ON Generic_List.GenericListID = PST.ServiceTypeID\r\n                            WHERE PST.VoucherID='" + id + "' AND Generic_List.GenericListType = 14 ";
			FillDataSet(propertyServiceData, "Property_ServiceType_Detail", textCommand);
			return propertyServiceData;
		}

		public PropertyServiceData GetPropertyServiceAssignByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "VoucherID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Property_Service_Assign";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PropertyServiceData propertyServiceData = new PropertyServiceData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyServiceData, "Property_Service_Assign", sqlBuilder);
			if (propertyServiceData != null && propertyServiceData.Tables.Count != 0)
			{
				_ = propertyServiceData.Tables[0].Rows.Count;
			}
			return propertyServiceData;
		}

		public DataSet GetPropertyServiceByFields(params string[] columns)
		{
			return GetPropertyServiceByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyServiceByFields(string[] taskID, params string[] columns)
		{
			return GetPropertyServiceByFields(taskID, isInactive: true, columns);
		}

		public DataSet GetPropertyServiceByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Service_Request");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "VoucherID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Property_Service_Request";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Property_Service_Request", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPropertyServiceList(bool includeClosedTasks)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT TaskID [Task Code],ContainerNumber [Container#],V.VendorID, V.VendorID + ' - ' + v.VendorName AS Vendor,GRNSysDocID [GRN DocID],GRNVoucherID [GRN Number],Description\r\n                           FROM Quality_Task QT INNER JOIN Vendor V ON V.VendorID = QT.VendorID ";
			if (!includeClosedTasks)
			{
				text += " WHERE ISNULL(Status,1) = 1 AND NOT EXISTS (SELECT * FROM Arrival_Report AR WHERE AR.TaskID = QT.TaskID) ";
			}
			FillDataSet(dataSet, "Property_Service_Request", text);
			return dataSet;
		}

		public DataSet GetPropertyServiceComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name]\r\n                           FROM Quality_Task ORDER BY CategoryID,CategoryName";
			FillDataSet(dataSet, "Property_Service_Request", textCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   SysDocID , VoucherID,ReportingDate,P.PropertyName [Property],PU.PropertyUnitName [Unit],C.CustomerName [Tenant],RequestNotes  FROM Property_Service_Request PSR \r\n                          Left Join Property P ON P.PropertyID=PSR.PropertyID\r\n                          Left Join Property_Unit PU ON PU.PropertyUnitID=PSR.UnitID\r\n                          Left Join Customer C ON C.CustomerID=PSR.TenantID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE ReportingDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND PSR.SysDocID = '" + sysDocID + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Property_Service_Request", sqlCommand);
			return dataSet;
		}

		public DataSet GetPropertyServiceAssignList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   SysDocID , VoucherID,V.VendorName [ServiceProvider],Amount,PlannedDate,StatusDate,Remarks  FROM Property_Service_Assign PSA \r\n                           Left Join Vendor V ON V.VendorId= PSA.ServiceProviderID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE PlannedDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND PSA.SysDocID = '" + sysDocID + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Property_Service_Assign", sqlCommand);
			return dataSet;
		}

		public DataSet GetServiceRequestList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT SysDocID[Doc ID], VoucherID[Number], ReportingDate[Date], PropertyID[Property]  FROM Property_Service_Request PSR\r\n                           where SysDocID + VoucherID NOT IN (select SourceSysDocID + SourceVoucherID from property_service_assign)");
			FillDataSet(dataSet, "Property_Service_Request", sqlCommand);
			return dataSet;
		}

		public DataSet GetTenantByUnit(string unitID, DateTime reportingdate)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(reportingdate);
			SqlCommand sqlCommand = new SqlCommand("SELECT   CustomerID FROM Property_Rent Where UnitID='" + unitID + "' AND '" + text + "' between ContractStartDate AND ContractEndDate  Order by ContractEndDate desc");
			FillDataSet(dataSet, "PropertyRent", sqlCommand);
			return dataSet;
		}

		public DataSet GetPropertyServiceRequestToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT   SysDocID , VoucherID,ReportingDate,RequiredDateTime,ConvenientDateTime,P.PropertyName [Property],PU.PropertyUnitName [Unit],C.CustomerName [Tenant],CA.AddressPrintFormat,CA.Email,CA.Phone1,CA.Mobile,RequestNotes,\r\n                          Case when PriorityStatus='0' Then 'Low' When PriorityStatus='1' Then 'High' When PriorityStatus='2' Then 'Medium' Else '' End AS PriorityStatus,PSR.DateCreated,PSR.DateUpdated,PSR.CreatedBy,PSR.UpdatedBy \r\n                          FROM Property_Service_Request PSR \r\n                          Left Join Property P ON P.PropertyID = PSR.PropertyID\r\n                          Left Join Property_Unit PU ON PU.PropertyUnitID = PSR.UnitID\r\n                          Left Join Customer C ON C.CustomerID = PSR.TenantID \r\n                          LEFT OUTER JOIN  Customer_Address CA ON C.CustomerID=CA.CustomerID WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Property_Service_Request", textCommand);
				textCommand = "SELECT  SysDocID,VoucherID,GenericListID as FacilityID,GenericListName as FacilityType,RowIndex FROM Property_ServiceFacility_Detail PFD Left Join Generic_List PF ON PF.GenericListID=PFD.FacilityID\r\n                         WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Property_ServiceFacility_Detail", textCommand);
				textCommand = "SELECT  SysDocID,VoucherID,GenericListID as ServiceID,GenericListName as ServiceType,RowIndex FROM Property_ServiceType_Detail PSD Left Join Generic_List PF ON PF.GenericListID=PSD.ServiceTypeID\r\n                         WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Property_ServiceType_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPropertyServiceAssignToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT   SysDocID , VoucherID,SourceSysDocID,SourceVoucherID,V.VendorName [ServiceProvider],Amount,PlannedDate,StatusDate, \r\n                                Case when Status='0' Then 'Open' When [Status]='1' Then 'Assigned' When [Status]='2' Then 'Inprogress' When [Status]='3' Then 'Hold'  When [Status]='4' Then 'Closed'  Else '' End AS [Status],\r\n                                Remarks,PSA.DateCreated,PSA.DateUpdated,PSA.CreatedBy,PSA.UpdatedBy FROM Property_Service_Assign PSA\r\n                                Left Join Vendor V ON V.VendorId = PSA.ServiceProviderID  WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Property_Service_Assign", textCommand);
				if (dataSet.Tables["Property_Service_Assign"].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables["Property_Service_Assign"].Rows[0];
					string text2 = dataRow["SourceSysDocID"].ToString();
					string text3 = dataRow["SourceVoucherID"].ToString();
					textCommand = "SELECT   SysDocID , VoucherID,ReportingDate,RequiredDateTime,ConvenientDateTime,P.PropertyName [Property],PU.PropertyUnitName [Unit],C.CustomerName [Tenant],RequestNotes,\r\n                          Case when PriorityStatus='0' Then 'Low' When PriorityStatus='1' Then 'High' When PriorityStatus='2' Then 'Medium' Else '' End AS PriorityStatus,PSR.DateCreated,PSR.DateUpdated,PSR.CreatedBy,PSR.UpdatedBy \r\n                          FROM Property_Service_Request PSR \r\n                          Left Join Property P ON P.PropertyID = PSR.PropertyID\r\n                          Left Join Property_Unit PU ON PU.PropertyUnitID = PSR.UnitID\r\n                          Left Join Customer C ON C.CustomerID = PSR.TenantID  WHERE VoucherID IN ('" + text3 + "') AND SysDocID='" + text2 + "'";
					FillDataSet(dataSet, "Property_Service_Request", textCommand);
					textCommand = "SELECT  SysDocID,VoucherID,GenericListID as FacilityID,GenericListName as FacilityType,RowIndex FROM Property_ServiceFacility_Detail PFD Left Join Generic_List PF ON PF.GenericListID=PFD.FacilityID\r\n                         WHERE VoucherID IN ('" + text3 + "') AND SysDocID='" + text2 + "'";
					FillDataSet(dataSet, "Property_ServiceFacility_Detail", textCommand);
					textCommand = "SELECT  SysDocID,VoucherID,GenericListID as ServiceID,GenericListName as ServiceType,RowIndex FROM Property_ServiceType_Detail PSD Left Join Generic_List PF ON PF.GenericListID=PSD.ServiceTypeID\r\n                         WHERE VoucherID IN ('" + text3 + "') AND SysDocID='" + text2 + "'";
					FillDataSet(dataSet, "Property_ServiceType_Detail", textCommand);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
