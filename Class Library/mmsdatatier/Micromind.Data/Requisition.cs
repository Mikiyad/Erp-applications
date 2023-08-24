using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Requisition : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REQUISITIONTYPEID_PARM = "@RequisitionTypeID";

		private const string JOBID_PARM = "@JobID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string STATUS_PARM = "@Status";

		private const string EQUIPMENTCATEGORYID_PARM = "@EquipmentCategoryID";

		private const string EQUIPMENTID_PARM = "@EquipmentID";

		private const string REMARKS_PARM = "@Remarks";

		private const string REQUIREDON_PARM = "@RequiredOn";

		private const string REQUIREDTILL_PARM = "@RequiredTill";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string REQUISITION_TABLE = "EA_Requisition";

		public Requisition(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateRequisitionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Requisition", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("JobID", "@JobID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("RequisitionTypeID", "@RequisitionTypeID"), new FieldValue("EquipmentCategoryID", "@EquipmentCategoryID"), new FieldValue("EquipmentID", "@EquipmentID"), new FieldValue("RequiredOn", "@RequiredOn"), new FieldValue("RequiredTill", "@RequiredTill"), new FieldValue("Status", "@Status"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EA_Requisition", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateRequisitionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateRequisitionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateRequisitionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@RequisitionTypeID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@EquipmentCategoryID", SqlDbType.NVarChar);
			parameters.Add("@EquipmentID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@RequiredOn", SqlDbType.DateTime);
			parameters.Add("@RequiredTill", SqlDbType.DateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@RequisitionTypeID"].SourceColumn = "RequisitionTypeID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@EquipmentCategoryID"].SourceColumn = "EquipmentCategoryID";
			parameters["@EquipmentID"].SourceColumn = "EquipmentID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@RequiredOn"].SourceColumn = "RequiredOn";
			parameters["@RequiredTill"].SourceColumn = "RequiredTill";
			parameters["@Status"].SourceColumn = "Status";
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

		private bool ValidateData(RequisitionData journalData)
		{
			return true;
		}

		public bool InsertUpdateRequisition(RequisitionData RequisitionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateRequisitionCommand = GetInsertUpdateRequisitionCommand(isUpdate);
			try
			{
				DataRow dataRow = RequisitionData.RequisitionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("EA_Requisition", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateRequisitionCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (RequisitionData.Tables["EA_Requisition"].Rows.Count > 0)
					{
						flag &= Insert(RequisitionData, "EA_Requisition", insertUpdateRequisitionCommand);
					}
				}
				else
				{
					flag &= Update(RequisitionData, "EA_Requisition", insertUpdateRequisitionCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("EA_Requisition", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Requisition";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "EA_Requisition", "VoucherID", sqlTransaction);
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

		public RequisitionData GetRequisitionByID(string sysDocID, string voucherID)
		{
			try
			{
				RequisitionData requisitionData = new RequisitionData();
				string textCommand = "SELECT * FROM EA_Requisition WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(requisitionData, "EA_Requisition", textCommand);
				if (requisitionData == null || requisitionData.Tables.Count == 0 || requisitionData.Tables["EA_Requisition"].Rows.Count == 0)
				{
					return null;
				}
				return requisitionData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetRequisition()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID [Doc ID],VoucherID [Number],TransactionDate AS [Date] FROM EA_Requisition where Status=1 ";
				FillDataSet(dataSet, "EA_Requisition", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		private bool VoidRequisition(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteRequisition(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM EA_Requisition WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Requisition", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetRequisitionToPrint(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("select EAR.*, EC.EquipmentCategoryName, E.Description,ERT.RequisitionTypeName , E.Capacity, E.CapacityType, E.Model, E.RegistrationNumber, E.Color, E.Fuel, E.Power, E.Year, E.OwnedBy \r\n                                from EA_Requisition EAR LEFT JOIN EA_Equipment_Category EC \r\n                                ON EAR.EquipmentCategoryID=EC.EquipmentCategoryID \r\n                                LEFT JOIN EA_Equipment E ON EAR.EquipmentID=E.EquipmentID\r\n                                LEFT JOIN EA_Requisition_Type ERT ON EAR.RequisitionTypeID=ERT.RequisitionTypeID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "EA_Requisition", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["EA_Requisition"].Rows.Count == 0)
				{
					return null;
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetRequisitionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = " SELECT IA.SysDocID, IA.VoucherID, IA.TransactionDate, IA.LocationID, \r\n                                IA.Reference, IA.AdjustmentTypeID, IAD.ProductID, IAD.Description, \r\n                                IAD.Quantity, IAD.UnitID, IAD.RowIndex\r\n                                FROM Inventory_Adjustment IA INNER JOIN Inventory_Adjustment_Detail IAD ON IA.SysDocID = IAD.SysDocID AND                             \r\n                                IA.VoucherID = IAD.VoucherID \r\n                                INNER JOIN PRODUCT P ON IAD.ProductID=P.ProductID\r\n                                WHERE 1=1 ";
				text3 = text3 + " AND P.ItemType NOT IN ('3') AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND IAD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND IAD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND IAD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromWarehouse != "" || toWarehouse != "")
				{
					text3 = text3 + " AND IA.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				text3 += " ORDER BY IAD.RowIndex";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetRequisitionList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date] FROM EA_Requisition   ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + "  WHERE  SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "EA_Requisition", str);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT 'False' AS V, SysDocID,VoucherID,TransactionDate,LocationID FROM EA_Requisition ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "EA_Requisition", sqlCommand);
			return dataSet;
		}

		public DataSet GetRequisitionByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string text3 = "select RE.SysDocID, RE.VoucherID, RE.EquipmentID, \r\n                            RE.EquipmentCategoryID, RE.RequiredOn, RE.RequiredTill, RE.RequisitionTypeID, RE.JobID, RE.LocationID, RE.Remarks,RE.TransactionDate,\r\n                             E.Description, E.Model, E.RegistrationNumber, E.ParentEquipmentID, E.Year, E.fuel, E.Capacity,CASE E.CapacityType WHEN 1 THEN 'SEAT' WHEN 2 THEN 'TON' END AS [CAPACITY TYPE], E.Power, E.OwnedBy,RequisitionTypeName\r\n                             from EA_Requisition RE LEFT JOIN EA_Equipment E ON RE.EquipmentID=E.EquipmentID\r\n                             LEFT JOIN EA_Requisition_Type RT On RE.RequisitionTypeID=RT.RequisitionTypeID\r\n\t\t\t\t\t\t     WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromEquipment != "")
				{
					text3 = text3 + " AND RE.EquipmentID BETWEEN '" + fromEquipment + "' AND '" + toEquipment + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND RE.RequisitionTypeID IN (SELECT RequisitionTypeID FROM EA_Requisition WHERE RequisitionTypeID BETWEEN '" + fromType + "' AND '" + toType + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND RE.EquipmentCategoryID IN (SELECT EquipmentCategoryID FROM EA_Requisition WHERE EquipmentCategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND RE.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND RE.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Requisition", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateRequisitionStatus(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "Select Count(*) FROM EA_Requisition REQ\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE EA_Requisition SET Status=0 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}
	}
}
