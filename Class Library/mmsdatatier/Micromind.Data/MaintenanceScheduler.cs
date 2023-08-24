using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class MaintenanceScheduler : StoreObject
	{
		public const string SYSDOCID_PARM = "@SysDocID";

		public const string DOCID_PARM = "@VoucherID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string MAINTENANCESCHEDULER_TABLE = "Vehicle_Maintenance_Scheduler";

		public const string VEHICLENUMBER_PARM = "@VehicleNumber";

		public const string MAINTENANCEDATE_PARM = "@MaintenanceDate";

		public const string ODOMETER_PARM = "@Odometer";

		public const string SERVICETYPE_PARM = "@ServiceType";

		public const string SERVICEPROVIDER_PARM = "@ServiceProvider";

		public const string AMOUNT_PARM = "@Amount";

		public const string ISVOID_PARM = "@IsVoid";

		public const string REQUIREDTIME_PARM = "@TimeRequired";

		public const string TRACKMAINTENANCE_PARM = "@TrackMaintenance";

		public const string STATUS_PARM = "@Status";

		public const string NOTE_PARM = "@Note";

		public MaintenanceScheduler(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateMaintenanceSchedulerText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vehicle_Maintenance_Scheduler", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VehicleNumber", "@VehicleNumber"), new FieldValue("TimeRequired", "@TimeRequired"), new FieldValue("CreatedBy", "@CreatedBy"), new FieldValue("DateCreated", "@DateCreated"), new FieldValue("ServiceProvider", "@ServiceProvider"), new FieldValue("ServiceType", "@ServiceType"), new FieldValue("Status", "@Status"), new FieldValue("TrackMaintenance", "@TrackMaintenance"), new FieldValue("Odometer", "@Odometer"), new FieldValue("MaintenanceDate", "@MaintenanceDate"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("Amount", "@Amount"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Vehicle_Maintenance_Scheduler", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateMaintenanceSchedulerCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateMaintenanceSchedulerText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateMaintenanceSchedulerText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VehicleNumber", SqlDbType.NVarChar);
			parameters.Add("@MaintenanceDate", SqlDbType.DateTime);
			parameters.Add("@CreatedBy", SqlDbType.NVarChar);
			parameters.Add("@DateCreated", SqlDbType.DateTime);
			parameters.Add("@Odometer", SqlDbType.NVarChar);
			parameters.Add("@ServiceType", SqlDbType.NVarChar);
			parameters.Add("@ServiceProvider", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.NVarChar);
			parameters.Add("@TimeRequired", SqlDbType.NVarChar);
			parameters.Add("@TrackMaintenance", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@IsVoid", SqlDbType.Bit);
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
			}
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@VehicleNumber"].SourceColumn = "VehicleNumber";
			parameters["@MaintenanceDate"].SourceColumn = "MaintenanceDate";
			parameters["@CreatedBy"].SourceColumn = "CreatedBy";
			parameters["@DateCreated"].SourceColumn = "DateCreated";
			parameters["@Odometer"].SourceColumn = "Odometer";
			parameters["@ServiceType"].SourceColumn = "ServiceType";
			parameters["@ServiceProvider"].SourceColumn = "ServiceProvider";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@TimeRequired"].SourceColumn = "TimeRequired";
			parameters["@TrackMaintenance"].SourceColumn = "MaintenanceDate";
			parameters["@TimeRequired"].SourceColumn = "TimeRequired";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			if (isUpdate)
			{
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(MaintenanceSchedulerData journalData)
		{
			return true;
		}

		public bool InsertUpdateMaintenanceScheduler(MaintenanceSchedulerData MaintenanceSchedulerData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateMaintenanceSchedulerCommand = GetInsertUpdateMaintenanceSchedulerCommand(isUpdate);
			try
			{
				DataRow dataRow = MaintenanceSchedulerData.MaintenanceSchedulerTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Vehicle_Maintenance_Scheduler", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in MaintenanceSchedulerData.MaintenanceSchedulerTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
					row["SysDocID"] = dataRow["SysDocID"];
				}
				insertUpdateMaintenanceSchedulerCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(MaintenanceSchedulerData, "Vehicle_Maintenance_Scheduler", insertUpdateMaintenanceSchedulerCommand)) : (flag & Insert(MaintenanceSchedulerData, "Vehicle_Maintenance_Scheduler", insertUpdateMaintenanceSchedulerCommand)));
				insertUpdateMaintenanceSchedulerCommand = GetInsertUpdateMaintenanceSchedulerCommand(isUpdate: false);
				insertUpdateMaintenanceSchedulerCommand.Transaction = sqlTransaction;
				if (flag)
				{
					flag &= UpdateTableRowInsertUpdateInfo("Vehicle_Maintenance_Scheduler", "SysDocID", text2, "VoucherID", text, sqlTransaction, isInsert: false);
					string entityName = "Vehicle Maintenance Scheduler";
					flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
					if (!isUpdate)
					{
						flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Vehicle_Maintenance_Scheduler", "VoucherID", sqlTransaction);
					}
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.MaintenanceScheduler, text2, text, "Vehicle_Maintenance_Scheduler", sqlTransaction);
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

		public MaintenanceSchedulerData GetMaintenanceSchedulerByID(string sysDocID, string voucherID)
		{
			try
			{
				MaintenanceSchedulerData maintenanceSchedulerData = new MaintenanceSchedulerData();
				string textCommand = "SELECT * FROM Vehicle_Maintenance_Scheduler WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(maintenanceSchedulerData, "Vehicle_Maintenance_Scheduler", textCommand);
				if (maintenanceSchedulerData == null || maintenanceSchedulerData.Tables.Count == 0 || maintenanceSchedulerData.Tables["Vehicle_Maintenance_Scheduler"].Rows.Count == 0)
				{
					return null;
				}
				return maintenanceSchedulerData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteMaintenanceScheduler(string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM Vehicle_Maintenance_Scheduler WHERE VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("VEHICLE MAINTENANCE SCHEDULER", voucherID, activityType, sqlTransaction);
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

		public DataSet GetMaintenanceSchedulerToPrint(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("SELECT MA.SysDocID, MA.VoucherID,MA.Amount,MA.Odometer,MA.ServiceProvider,MA.Status,MA.TrackMaintenance,MA.MaintenanceDate, ma.VehicleNumber,MA.Note,ma.CreatedBy,ma.DateUpdated\r\n                               FROM    Vehicle_Maintenance_Scheduler MA LEFT JOIN\r\n                         Vehicle V ON MA.VehicleNumber = V.VehicleID INNER JOIN VENDOR VR ON VR.VendorID=MA.ServiceProvider  \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "Vehicle_Maintenance_Scheduler", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetMaintenanceSchedulerReport(DateTime from, DateTime to, string fromVehicle, string toVehicle)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT  Maintenance_Scheduler.SysDocID, Maintenance_Scheduler.VoucherID, Maintenance_Scheduler.VehicleNumber, Maintenance_Scheduler.Odometer, \r\n                         Maintenance_Scheduler.Amount, Maintenance_Scheduler.TimeRequired,Service_Item.Description, \r\n                         CASE Service_Item.ServiceType WHEN 0 THEN 'NONE' WHEN 1 THEN 'REPAIR' WHEN 2 THEN 'MAINTENANCE' WHEN 3 THEN 'INSPECTION' WHEN 4 THEN 'RENEWALS' END AS 'SERVICE TYPE', Service_Item.RepeatCountDays, Service_Item.RepeatCountKM, Service_Item.ReminderDays, Service_Item.ReminderKM, \r\n                         Service_Item.VehicleType, Vendor.VendorName, Maintenance_Scheduler.MaintenanceDate,Maintenance_Scheduler.Note\r\n                          FROM  Vehicle_Maintenance_Scheduler Maintenance_Scheduler INNER JOIN\r\n                         Service_Item ON Maintenance_Scheduler.ServiceType = Service_Item.ServiceItemID INNER JOIN\r\n                         Vendor ON Maintenance_Scheduler.ServiceProvider = Vendor.VendorID";
			text3 = text3 + " where Maintenance_Scheduler.MaintenanceDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromVehicle != "")
			{
				text3 = text3 + " AND Maintenance_Scheduler.VehicleNumber BETWEEN '" + fromVehicle + "' AND '" + toVehicle + "' ";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vehicle_Maintenance_Scheduler", text3);
			return dataSet;
		}

		public DataSet GetMaintenanceSchedulerSummary(string vehicle)
		{
			string text = "SELECT MA.SysDocID [Doc ID], MA.VoucherID [Number],MA.Amount,MA.Odometer,MA.ServiceProvider,\r\n                        MA.Status,MA.TrackMaintenance,MA.MaintenanceDate, ma.VehicleNumber,ma.CreatedBy,ma.DateUpdated\r\n                               FROM    Vehicle_Maintenance_Scheduler MA LEFT JOIN\r\n                         Vehicle V ON MA.VehicleNumber = V.VehicleID INNER JOIN VENDOR VR ON VR.VendorID=MA.ServiceProvider where ISNULL(IsVoid,'False')='False' and Status=0 ";
			if (vehicle != "")
			{
				text = text + " AND MA.VehicleNumber='" + vehicle + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vehicle_Maintenance_Scheduler", text);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			StoreConfiguration.ToSqlDateTimeString(from);
			StoreConfiguration.ToSqlDateTimeString(to);
			SqlCommand sqlCommand = new SqlCommand("SELECT SysDocID,VoucherID,MaintenanceDate,Note FROM Vehicle_Maintenance_Scheduler ");
			FillDataSet(dataSet, "Vehicle_Maintenance_Scheduler", sqlCommand);
			return dataSet;
		}

		public bool VoidMaintenance(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				MaintenanceSchedulerData dataSet = new MaintenanceSchedulerData();
				string textCommand = "SELECT * FROM Vehicle_Maintenance_Scheduler\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Vehicle_Maintenance_Scheduler", textCommand);
				textCommand = "UPDATE Vehicle_Maintenance_Scheduler SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Vehicle Maintenance  Scheduler", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool UpdateMaintenanceStatus(string sysDocID, string voucherID, int status)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				MaintenanceSchedulerData maintenanceSchedulerData = new MaintenanceSchedulerData();
				if (sysDocID != "" && voucherID != "")
				{
					string textCommand = "SELECT * FROM Vehicle_Maintenance_Scheduler\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					FillDataSet(maintenanceSchedulerData, "Vehicle_Maintenance_Scheduler", textCommand);
					textCommand = "UPDATE Vehicle_Maintenance_Scheduler SET Status = '" + status + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					return flag;
				}
				if (!(voucherID != ""))
				{
					return flag;
				}
				string textCommand2 = "SELECT * FROM Vehicle_Maintenance_Scheduler\r\n                              WHERE VoucherID = '" + voucherID + "'";
				FillDataSet(maintenanceSchedulerData, "Vehicle_Maintenance_Scheduler", textCommand2);
				if (maintenanceSchedulerData == null)
				{
					return flag;
				}
				if (maintenanceSchedulerData.Tables.Count <= 0)
				{
					return flag;
				}
				if (maintenanceSchedulerData.Tables["Vehicle_Maintenance_Scheduler"].Rows.Count <= 0)
				{
					return flag;
				}
				textCommand2 = "UPDATE Vehicle_Maintenance_Scheduler SET Status = '" + status + "' WHERE  VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand2, sqlTransaction) > 0);
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
	}
}
