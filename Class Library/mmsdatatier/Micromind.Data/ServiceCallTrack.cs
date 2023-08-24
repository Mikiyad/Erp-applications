using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ServiceCallTrack : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string JOBID_PARM = "@JobID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string CONTACTNAME_PARM = "@ContactName";

		private const string CONTACTNO_PARM = "@ContactNo";

		private const string LOCATION_PARM = "@Location";

		private const string REQRECEIVEDDATE_PARM = "@ReqReceivedDate";

		private const string REQRECEIVEDTIME_PARM = "@ReqReceivedTime";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string SERVICEEMPLOYEEID_PARM = "@ServiceEmployeeID";

		private const string SERVICEASSIGNDATE_PARM = "@ServiceAssignDate";

		private const string SERVICEASSIGNTIME_PARM = "@ServiceAssignTime";

		private const string SERVICEUNDER_PARM = "@ServiceUnder";

		private const string SERVICESTARTDATE_PARM = "@ServiceStartDate";

		private const string SERVICESTARTTIME_PARM = "@ServiceStartTime";

		private const string SERVICEFINISHEDDATE_PARM = "@ServiceFinishedDate";

		private const string SERVICEFINISHEDTIME_PARM = "@ServiceFinishedTime";

		private const string TRAVELHOURS_PARM = "@TravelHours";

		private const string TRAVELMINS_PARM = "@TravelMins";

		private const string LABOURHOURS_PARM = "@LabourHours";

		private const string LABOURMINS_PARM = "@LabourMins";

		private const string SERVICEHOURS_PARM = "@ServiceHours";

		private const string SERVICETOTAL_PARM = "@ServiceTotal";

		private const string PARTSTOTAL_PARM = "@PartsTotal";

		private const string LABOURTOTAL_PARM = "@LabourTotal";

		private const string TRAVELTOTAL_PARM = "@TravelTotal";

		private const string TOTALCHARGES_PARM = "@TotalCharges";

		private const string REPAIRDETAILS_PARM = "@RepairDetails";

		private const string STATUS_PARM = "@Status";

		private const string ISVOID_PARM = "@IsVoid";

		private const string SERVICECALLTRACK_TABLE = "Service_CallTrack";

		private const string SERVICECLIENTASSETDETAIL_TABLE = "Service_ClientAsset_Detail";

		private const string SERVICEPARTSREPLACEDDETAIL_TABLE = "Service_PartsReplaced_Detail";

		private const string CLIENTASSETID_PARM = "@ClientAssetID";

		private const string SERIALNO_PARM = "@SerialNo";

		private const string PROBLEMDESCRIPTION_PARM = "@ProblemDescription";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string DESCRIPTION_PARM = "@Description";

		private const string CHARGEABLESTATUS_PARM = "@ChargeableStatus";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ServiceCallTrack(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateServiceCallTrackText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Service_CallTrack", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("JobID", "@JobID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("ContactName", "@ContactName"), new FieldValue("ContactNo", "@ContactNo"), new FieldValue("Location", "@Location"), new FieldValue("ReqReceivedDate", "@ReqReceivedDate"), new FieldValue("ReqReceivedTime", "@ReqReceivedTime"), new FieldValue("ServiceEmployeeID", "@ServiceEmployeeID"), new FieldValue("ServiceAssignDate", "@ServiceAssignDate"), new FieldValue("ServiceAssignTime", "@ServiceAssignTime"), new FieldValue("ServiceUnder", "@ServiceUnder"), new FieldValue("ServiceStartDate", "@ServiceStartDate"), new FieldValue("ServiceStartTime", "@ServiceStartTime"), new FieldValue("ServiceFinishedDate", "@ServiceFinishedDate"), new FieldValue("ServiceFinishedTime", "@ServiceFinishedTime"), new FieldValue("TravelHours", "@TravelHours"), new FieldValue("TravelMins", "@TravelMins"), new FieldValue("LabourHours", "@LabourHours"), new FieldValue("LabourMins", "@LabourMins"), new FieldValue("ServiceHours", "@ServiceHours"), new FieldValue("ServiceTotal", "@ServiceTotal"), new FieldValue("PartsTotal", "@PartsTotal"), new FieldValue("LabourTotal", "@LabourTotal"), new FieldValue("TravelTotal", "@TravelTotal"), new FieldValue("TotalCharges", "@TotalCharges"), new FieldValue("RepairDetails", "@RepairDetails"), new FieldValue("Status", "@Status"), new FieldValue("IsVoid", "@IsVoid"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Service_CallTrack", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateServiceCallTrackCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateServiceCallTrackText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateServiceCallTrackText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ContactName", SqlDbType.NVarChar);
			parameters.Add("@ContactNo", SqlDbType.NVarChar);
			parameters.Add("@Location", SqlDbType.NVarChar);
			parameters.Add("@ReqReceivedDate", SqlDbType.DateTime);
			parameters.Add("@ReqReceivedTime", SqlDbType.Time);
			parameters.Add("@ServiceEmployeeID", SqlDbType.NVarChar);
			parameters.Add("@ServiceAssignDate", SqlDbType.DateTime);
			parameters.Add("@ServiceAssignTime", SqlDbType.Time);
			parameters.Add("@ServiceUnder", SqlDbType.Char);
			parameters.Add("@ServiceStartDate", SqlDbType.DateTime);
			parameters.Add("@ServiceStartTime", SqlDbType.Time);
			parameters.Add("@ServiceFinishedDate", SqlDbType.DateTime);
			parameters.Add("@ServiceFinishedTime", SqlDbType.Time);
			parameters.Add("@TravelHours", SqlDbType.Int);
			parameters.Add("@TravelMins", SqlDbType.Int);
			parameters.Add("@LabourHours", SqlDbType.Int);
			parameters.Add("@LabourMins", SqlDbType.Int);
			parameters.Add("@ServiceHours", SqlDbType.Int);
			parameters.Add("@ServiceTotal", SqlDbType.Decimal);
			parameters.Add("@PartsTotal", SqlDbType.Decimal);
			parameters.Add("@LabourTotal", SqlDbType.Decimal);
			parameters.Add("@TravelTotal", SqlDbType.Decimal);
			parameters.Add("@TotalCharges", SqlDbType.Decimal);
			parameters.Add("@RepairDetails", SqlDbType.NText);
			parameters.Add("@Status", SqlDbType.Int);
			parameters.Add("@IsVoid", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ContactName"].SourceColumn = "ContactName";
			parameters["@ContactNo"].SourceColumn = "ContactNo";
			parameters["@Location"].SourceColumn = "Location";
			parameters["@ReqReceivedDate"].SourceColumn = "ReqReceivedDate";
			parameters["@ReqReceivedTime"].SourceColumn = "ReqReceivedTime";
			parameters["@ServiceEmployeeID"].SourceColumn = "ServiceEmployeeID";
			parameters["@ServiceAssignDate"].SourceColumn = "ServiceAssignDate";
			parameters["@ServiceAssignTime"].SourceColumn = "ServiceAssignTime";
			parameters["@ServiceUnder"].SourceColumn = "ServiceUnder";
			parameters["@ServiceStartDate"].SourceColumn = "ServiceStartDate";
			parameters["@ServiceStartTime"].SourceColumn = "ServiceStartTime";
			parameters["@ServiceFinishedDate"].SourceColumn = "ServiceFinishedDate";
			parameters["@ServiceFinishedTime"].SourceColumn = "ServiceFinishedTime";
			parameters["@TravelHours"].SourceColumn = "TravelHours";
			parameters["@TravelMins"].SourceColumn = "TravelMins";
			parameters["@LabourHours"].SourceColumn = "LabourHours";
			parameters["@LabourMins"].SourceColumn = "LabourMins";
			parameters["@ServiceHours"].SourceColumn = "ServiceHours";
			parameters["@ServiceTotal"].SourceColumn = "ServiceTotal";
			parameters["@PartsTotal"].SourceColumn = "PartsTotal";
			parameters["@LabourTotal"].SourceColumn = "LabourTotal";
			parameters["@TravelTotal"].SourceColumn = "TravelTotal";
			parameters["@TotalCharges"].SourceColumn = "TotalCharges";
			parameters["@RepairDetails"].SourceColumn = "RepairDetails";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
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

		private string GetInsertUpdateServicePartsReplacedDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Service_PartsReplaced_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Description", "@Description"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("ChargeableStatus", "@ChargeableStatus"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateServicePartsReplacedDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateServicePartsReplacedDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateServicePartsReplacedDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Int);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ChargeableStatus", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@ChargeableStatus"].SourceColumn = "ChargeableStatus";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateServiceClientAssetDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Service_ClientAsset_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ClientAssetID", "@ClientAssetID"), new FieldValue("SerialNo", "@SerialNo"), new FieldValue("ProblemDescription", "@ProblemDescription"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateServiceClientAssetDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateServiceClientAssetDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateServiceClientAssetDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ClientAssetID", SqlDbType.NVarChar);
			parameters.Add("@SerialNo", SqlDbType.NVarChar);
			parameters.Add("@ProblemDescription", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ClientAssetID"].SourceColumn = "ClientAssetID";
			parameters["@SerialNo"].SourceColumn = "SerialNo";
			parameters["@ProblemDescription"].SourceColumn = "ProblemDescription";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ServiceCallTrackData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobMaterialRequisition(ServiceCallTrackData serviceCallTrackData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateServiceCallTrackCommand = GetInsertUpdateServiceCallTrackCommand(isUpdate);
			try
			{
				DataRow dataRow = serviceCallTrackData.ServiceCallTrackTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Service_CallTrack", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in serviceCallTrackData.ServiceClientAssetDetail.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobMaterialRequisitionDetailsRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateServiceCallTrackCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(serviceCallTrackData, "Service_CallTrack", insertUpdateServiceCallTrackCommand)) : (flag & Insert(serviceCallTrackData, "Service_CallTrack", insertUpdateServiceCallTrackCommand)));
				insertUpdateServiceCallTrackCommand = GetInsertUpdateServiceClientAssetDetailsCommand(isUpdate: false);
				insertUpdateServiceCallTrackCommand.Transaction = sqlTransaction;
				if (serviceCallTrackData.ServiceClientAssetDetail.Rows.Count > 0)
				{
					flag &= Insert(serviceCallTrackData, "Service_ClientAsset_Detail", insertUpdateServiceCallTrackCommand);
				}
				insertUpdateServiceCallTrackCommand = GetInsertUpdateServicePartsReplacedDetailsCommand(isUpdate: false);
				insertUpdateServiceCallTrackCommand.Transaction = sqlTransaction;
				if (serviceCallTrackData.ServicePartsReplacedDetail.Rows.Count > 0)
				{
					flag &= Insert(serviceCallTrackData, "Service_PartsReplaced_Detail", insertUpdateServiceCallTrackCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Service_CallTrack", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Service Call Track";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Service_CallTrack", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ServiceCallTrack, sysDocID, text, "Service_CallTrack", sqlTransaction);
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

		public ServiceCallTrackData GetJobMaterialRequisitionByID(string sysDocID, string voucherID)
		{
			try
			{
				ServiceCallTrackData serviceCallTrackData = new ServiceCallTrackData();
				string textCommand = "SELECT * FROM Service_CallTrack WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(serviceCallTrackData, "Service_CallTrack", textCommand);
				if (serviceCallTrackData == null || serviceCallTrackData.Tables.Count == 0 || serviceCallTrackData.Tables["Service_CallTrack"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT * FROM Service_ClientAsset_Detail WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(serviceCallTrackData, "Service_ClientAsset_Detail", textCommand);
				textCommand = "SELECT * FROM Service_PartsReplaced_Detail WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(serviceCallTrackData, "Service_PartsReplaced_Detail", textCommand);
				return serviceCallTrackData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaterialRequisitionAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date], Description, Reference, \r\n                                (SELECT LocationName FROM Location L WHERE L.LocationID =  JMR.LocationID) AS Location FROM Service_CallTrack JMR WHERE Status='1'";
				FillDataSet(dataSet, "Service_CallTrack", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobMaterialRequisitionDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Service_ClientAsset_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag = Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Service_PartsReplaced_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidJobMaterialRequisition(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteJobMaterialRequisition(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobMaterialRequisitionDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Service_CallTrack WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Service Call Track", voucherID, sysDocID, ActivityTypes.Delete, null);
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

		public DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT JM.*,'' AS StrRepairDetails,C.CustomerName,E.FirstName+' '+E.LastName AS [EmployeeName]  FROM Service_CallTrack JM LEFT JOIN Customer C ON JM.CustomerID=C.CustomerID\r\n                        LEFT JOIN Employee E ON JM.ServiceEmployeeID=E.EmployeeID\r\n                         WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Service_CallTrack", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Service_CallTrack"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT Service_ClientAsset_Detail.*,CA.ClientAssetName FROM Service_ClientAsset_Detail LEFT JOIN ClientAsset CA ON CA.ClientAssetID=Service_ClientAsset_Detail.ClientAssetID WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                                              ORDER BY RowIndex";
				FillDataSet(dataSet, "Service_ClientAsset_Detail", cmdText);
				cmdText = "SELECT Service_PartsReplaced_Detail.*,P.Description AS ProductName FROM Service_PartsReplaced_Detail LEFT JOIN Product P ON P.ProductID=Service_PartsReplaced_Detail.ProductID WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                                              ORDER BY RowIndex";
				FillDataSet(dataSet, "Service_PartsReplaced_Detail", cmdText);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowIssuedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float num = 0f;
			bool flag = true;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,Issued FROM Job_Material_Requisition_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					_ = dataSet.Tables[0].Rows[0];
				}
				num += quantity;
				textCommand = "UPDATE Job_Material_Requisition_Detail SET Issued=" + num.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
			}
			catch
			{
				return false;
			}
		}

		internal bool CloseIssuedDoc(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Job_Material_Requisition_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Job_Material_Requisition_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(Issued,0))  FROM Job_Material_Requisition_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Job_Material_Requisition SET Status= '2' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, int status)
		{
			try
			{
				string exp = "UPDATE Job_Material_Requisition SET Status= " + status + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaterialRequisitionList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID], JI.TransactionDate AS [Date]   \r\n                            FROM Service_CallTrack JI \r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Service_CallTrack", str);
			return dataSet;
		}

		public DataSet AllowDelete(string sysDocID, string voucherNumber)
		{
			string textCommand = "SELECT * FROM Purchase_Order_Detail  WHERE  SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "PODetails", textCommand);
			textCommand = "SELECT * FROM Job_Inventory_Issue_Detail  WHERE  SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "'";
			FillDataSet(dataSet, "IssueDetails", textCommand);
			return dataSet;
		}

		internal bool UpdateRowOrderedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float num = 0f;
			bool flag = true;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity FROM Job_Material_Requisition_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					_ = dataSet.Tables[0].Rows[0];
				}
				num += quantity;
				textCommand = "UPDATE Job_Material_Requisition_Detail SET =" + num.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
			}
			catch
			{
				return false;
			}
		}
	}
}
