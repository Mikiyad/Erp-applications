using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EquipmentTransfer : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string EQUIPMENTID_PARM = "@EquipmentID";

		private const string LOCATIONFROMID_PARM = "@LocationFromID";

		private const string LOCATIONTOID_PARM = "@LocationToID";

		private const string JOBFROMID_PARM = "@JobFromID";

		private const string JOBTO_PARM = "@JobToID";

		private const string EMPLOYEEFROMID_PARM = "@EmployeeFromID";

		private const string EMPLOYEETOID_PARM = "@EmployeeToID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string CURRENTMETERREADING_PARM = "@CurrentMeterReading";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string REQVOUCHERID_PARM = "@ReqVoucherID";

		private const string REQSYSDOCID_PARM = "@ReqSysDocID";

		private const string EQUIPMENTTRANSFER_TABLE = "EA_Equipment_Transfer";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EquipmentTransfer(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateEquipmentTransferText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Equipment_Transfer", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("EquipmentID", "@EquipmentID"), new FieldValue("CurrentMeterReading", "@CurrentMeterReading"), new FieldValue("Reference", "@Reference"), new FieldValue("LocationFromID", "@LocationFromID"), new FieldValue("LocationToID", "@LocationToID"), new FieldValue("JobFromID", "@JobFromID"), new FieldValue("JobToID", "@JobToID"), new FieldValue("EmployeeFromID", "@EmployeeFromID"), new FieldValue("EmployeeToID", "@EmployeeToID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("ReqSysDocID", "@ReqSysDocID"), new FieldValue("ReqVoucherID", "@ReqVoucherID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EA_Equipment_Transfer", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEquipmentTransferCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEquipmentTransferText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEquipmentTransferText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@EquipmentID", SqlDbType.NVarChar);
			parameters.Add("@CurrentMeterReading", SqlDbType.NVarChar);
			parameters.Add("@LocationFromID", SqlDbType.NVarChar);
			parameters.Add("@LocationToID", SqlDbType.NVarChar);
			parameters.Add("@JobFromID", SqlDbType.NVarChar);
			parameters.Add("@JobToID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeFromID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeToID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@ReqSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ReqVoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EquipmentID"].SourceColumn = "EquipmentID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@LocationFromID"].SourceColumn = "LocationFromID";
			parameters["@LocationToID"].SourceColumn = "LocationToID";
			parameters["@JobFromID"].SourceColumn = "JobFromID";
			parameters["@JobToID"].SourceColumn = "JobToID";
			parameters["@EmployeeFromID"].SourceColumn = "EmployeeFromID";
			parameters["@EmployeeToID"].SourceColumn = "EmployeeToID";
			parameters["@CurrentMeterReading"].SourceColumn = "CurrentMeterReading";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@ReqSysDocID"].SourceColumn = "ReqSysDocID";
			parameters["@ReqVoucherID"].SourceColumn = "ReqVoucherID";
			parameters["@Note"].SourceColumn = "Note";
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

		private bool ValidateData(EquipmentTransferData journalData)
		{
			return true;
		}

		public bool InsertUpdateEquipmentTransfer(EquipmentTransferData EquipmentTransferData, bool isUpdate)
		{
			return InsertUpdateEquipmentTransfer(EquipmentTransferData, isUpdate, null);
		}

		public bool InsertUpdateEquipmentTransfer(EquipmentTransferData EquipmentTransferData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateEquipmentTransferCommand = GetInsertUpdateEquipmentTransferCommand(isUpdate);
			string text = "";
			string text2 = "";
			bool flag2 = false;
			try
			{
				DataRow dataRow = EquipmentTransferData.EquipmentTransferTable.Rows[0];
				if (sqlTransaction == null)
				{
					flag2 = true;
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text3 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				text = dataRow["SourceSysDocID"].ToString();
				text2 = dataRow["SourceVoucherID"].ToString();
				if (text2 != "")
				{
					int.Parse(dataRow["SourceRowIndex"].ToString());
				}
				string text4 = dataRow["EquipmentID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("EA_Equipment_Transfer", "VoucherID", dataRow["SysDocID"].ToString(), text3, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text5 = dataRow["LocationToID"].ToString();
				string text6 = dataRow["JobToID"].ToString();
				string text7 = dataRow["EmployeeToID"].ToString();
				string exp = " UPDATE EA_Equipment SET JobID= '" + text6 + "', LocationID = '" + text5 + "' , OwnedBy = '" + text7 + "'\r\n                                WHERE EquipmentID= '" + text4 + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				insertUpdateEquipmentTransferCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(EquipmentTransferData, "EA_Equipment_Transfer", insertUpdateEquipmentTransferCommand)) : (flag & Insert(EquipmentTransferData, "EA_Equipment_Transfer", insertUpdateEquipmentTransferCommand)));
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				if (text2 != "")
				{
					flag &= new Mobilization(base.DBConfig).UpdateMobilizationStatus(text, text2, text4, sqlTransaction);
				}
				flag &= UpdateTableRowInsertUpdateInfo("EA_Equipment_Transfer", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Equipment Transfer";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text3, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text3, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "EA_Equipment_Transfer", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.EquipmentTransfer, sysDocID, text3, "EA_Equipment_Transfer", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				if (flag2)
				{
					base.DBConfig.EndTransaction(flag);
				}
			}
		}

		private GLData CreateEquipmentTransferGLData(EquipmentTransferData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.EquipmentTransferTable.Rows[0];
			string value = "";
			string value2 = "";
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.EquipmentTransfer;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Equipment Transfer";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = value2;
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = value;
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		public EquipmentTransferData GetEquipmentTransferByID(string sysDocID, string voucherID)
		{
			try
			{
				EquipmentTransferData equipmentTransferData = new EquipmentTransferData();
				string textCommand = "SELECT * FROM EA_Equipment_Transfer WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(equipmentTransferData, "EA_Equipment_Transfer", textCommand);
				if (equipmentTransferData == null || equipmentTransferData.Tables.Count == 0 || equipmentTransferData.Tables["EA_Equipment_Transfer"].Rows.Count == 0)
				{
					return null;
				}
				return equipmentTransferData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEquipmentTransferToPrint(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("select EAR.*, EC.EquipmentCategoryName, E.Description, E.Capacity, E.CapacityType, E.Model, E.RegistrationNumber, E.Color, E.Fuel, E.Power, E.Year, E.OwnedBy ,\r\n                                (select WorkLocationName from EA_Equipment_Transfer EAT LEFT JOIN Work_Location W ON EAT.LocationFromID=W.WorkLocationID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS WorkLocationFrom,(select WorkLocationName from EA_Equipment_Transfer EAT LEFT JOIN Work_Location W ON EAT.LocationToID=W.WorkLocationID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS WorkLocationTO,\r\n                                 (select JobName from EA_Equipment_Transfer EAT LEFT JOIN Job W ON EAT.JobFromID=W.JobID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS JobFrom, \r\n                                 (select JobName from EA_Equipment_Transfer EAT LEFT JOIN Job W ON EAT.JobToID=W.JobID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS JobTo,\r\n                                  (select W.FirstName+''+W.LastName from EA_Equipment_Transfer EAT LEFT JOIN Employee W ON EAT.EmployeeFromID=W.EmployeeID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID)  AS EmployeeFromName,\r\n                                  (select W.FirstName+''+w.LastName from EA_Equipment_Transfer EAT LEFT JOIN Employee W ON EAT.EmployeeToID=W.EmployeeID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID)  AS EmployeeTOName\r\n                                from EA_Equipment_Transfer EAR \r\n                               \r\n                                LEFT JOIN EA_Equipment E ON EAR.EquipmentID=E.EquipmentID\r\n                                LEFT JOIN EA_Equipment_Category EC \r\n                                ON E.EquipmentCategoryID=EC.EquipmentCategoryID \r\n\t\t\t\t\t\t\t\tLEFT JOIN Work_Location W ON EAR.LocationToID=W.WorkLocationID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Job ON EAR.JobToID=job.JobID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Employee ON EAR.EmployeeFromID=Employee.EmployeeID\r\n\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")");
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

		public DataSet GetEquipmentTransferReport(DateTime fromDate, DateTime toDate, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string text3 = "select EAR.*, EC.EquipmentCategoryName, E.Description, E.Capacity,CASE E.CapacityType WHEN 1 THEN 'SEAT' WHEN 2 THEN 'TON' END AS [CAPACITY TYPE], E.Model, E.RegistrationNumber, E.Color, E.Fuel, E.Power, E.Year, E.OwnedBy ,\r\n                                (select WorkLocationName from EA_Equipment_Transfer EAT LEFT JOIN Work_Location W ON EAT.LocationFromID=W.WorkLocationID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS WorkLocationFrom,(select WorkLocationName from EA_Equipment_Transfer EAT LEFT JOIN Work_Location W ON EAT.LocationToID=W.WorkLocationID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS WorkLocationTO,\r\n                                 (select JobName from EA_Equipment_Transfer EAT LEFT JOIN Job W ON EAT.JobFromID=W.JobID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS JobFrom, \r\n                                 (select JobName from EA_Equipment_Transfer EAT LEFT JOIN Job W ON EAT.JobToID=W.JobID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS JobTo,\r\n                                  (select W.FirstName+''+W.LastName from EA_Equipment_Transfer EAT LEFT JOIN Employee W ON EAT.EmployeeFromID=W.EmployeeID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID)  AS EmployeeFromName,\r\n                                  (select W.FirstName+''+w.LastName from EA_Equipment_Transfer EAT LEFT JOIN Employee W ON EAT.EmployeeToID=W.EmployeeID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID)  AS EmployeeTOName\r\n                                from EA_Equipment_Transfer EAR \r\n                                LEFT JOIN EA_Equipment E ON EAR.EquipmentID=E.EquipmentID\r\n                                LEFT JOIN EA_Equipment_Category EC \r\n                                ON E.EquipmentCategoryID=EC.EquipmentCategoryID \r\n\t\t\t\t\t\t\t\tLEFT JOIN Work_Location W ON EAR.LocationToID=W.WorkLocationID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Job ON EAR.JobToID=job.JobID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Employee ON EAR.EmployeeFromID=Employee.EmployeeID\r\n\t\t\t\t\t\t     WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromEquipment != "")
				{
					text3 = text3 + " AND EAR.EquipmentID BETWEEN '" + fromEquipment + "' AND '" + toEquipment + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND E.EquipmentTypeID>='" + fromType + "'";
				}
				if (toType != "")
				{
					text3 = text3 + " AND E.EquipmentTypeID<='" + toType + "'";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND E.EquipmentCategoryID>='" + fromCategory + "'";
				}
				if (toCategory != "")
				{
					text3 = text3 + " AND E.EquipmentCategoryID<='" + toCategory + "'";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Equipment Transfer", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidEquipmentTransfer(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				EquipmentTransferData equipmentTransferData = new EquipmentTransferData();
				text = "SELECT SOD.* FROM EA_Equipment_Transfer SOD\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(equipmentTransferData, "EA_Equipment_Transfer", text, sqlTransaction);
				if (equipmentTransferData.EquipmentTransferTable.Rows.Count == 0)
				{
					return true;
				}
				foreach (DataRow row in equipmentTransferData.EquipmentTransferTable.Rows)
				{
					string text2 = row["SourceSysDocID"].ToString();
					string text3 = row["SourceVoucherID"].ToString();
					int.Parse(row["SourceRowIndex"].ToString());
					string text4 = row["EquipmentID"].ToString();
					if (text3 != "")
					{
						text = "UPDATE EA_Mobilization_Equipment__Detail SET Status=1 WHERE SysDocID='" + text2 + "' AND VoucherID='" + text3 + "'AND EquipmentID='" + text4 + "'";
						flag = (ExecuteNonQuery(text, sqlTransaction) > 0);
					}
				}
				text = "UPDATE EA_Equipment_Transfer SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Equipment Transfer", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteEquipmentTransfer(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				EquipmentTransferData equipmentTransferData = new EquipmentTransferData();
				text = "SELECT SOD.* FROM EA_Equipment_Transfer SOD\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(equipmentTransferData, "EA_Equipment_Transfer", text, sqlTransaction);
				if (equipmentTransferData.EquipmentTransferTable.Rows.Count == 0)
				{
					return true;
				}
				foreach (DataRow row in equipmentTransferData.EquipmentTransferTable.Rows)
				{
					string text2 = row["SourceSysDocID"].ToString();
					string text3 = row["SourceVoucherID"].ToString();
					int.Parse(row["SourceRowIndex"].ToString());
					string text4 = row["EquipmentID"].ToString();
					if (text3 != "")
					{
						text = "UPDATE EA_Mobilization_Equipment__Detail SET Status=1 WHERE SysDocID='" + text2 + "' AND VoucherID='" + text3 + "'AND EquipmentID='" + text4 + "'";
						flag = (ExecuteNonQuery(text, sqlTransaction) > 0);
					}
				}
				text = "DELETE FROM EA_Equipment_Transfer WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Equipment Transfer", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetEquipmentTransferReport(DateTime from, DateTime to, string warehouseCode, bool isTransferOut)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT IT.* FROM EA_Equipment_Transfer IT \r\n                                WHERE ";
				text3 = text3 + "  TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (warehouseCode != "")
				{
					text3 = ((!isTransferOut) ? (text3 + " AND LocationToID = '" + warehouseCode + "' ") : (text3 + " AND LocationFromID = '" + warehouseCode + "' "));
				}
				text3 += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Transfer", text3);
				dataSet.Relations.Add("TransferRel", new DataColumn[2]
				{
					dataSet.Tables["Transfer"].Columns["SysDocID"],
					dataSet.Tables["Transfer"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["TransferDetail"].Columns["SysDocID"],
					dataSet.Tables["TransferDetail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT  SysDocID,VoucherID,FT.EquipmentID [Equipment ID], TransactionDate [Date], LocationfromID [From Location],JobFromID[From Job], JobToID[To Job],\r\n                                LocationToID [To Location],  Reference\r\n                                FROM EA_Equipment_Transfer FT\r\n                                 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "EA_Equipment_Transfer", sqlCommand);
			return dataSet;
		}
	}
}
