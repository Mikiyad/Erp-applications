using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class VehicleMileageTrack : StoreObject
	{
		public const string SYSDOCID_PARM = "@SysDocID";

		public const string VOUCHERID_PARM = "@VoucherID";

		public const string TRANSACTIONDATE_PARM = "@TransactionDate";

		public const string ISDATEWISE_PARM = "@IsDateWise";

		public const string NOTE_PARM = "@Note";

		public const string CREATEDBY_PARM = "@CreatedBy";

		public const string DATECREATED_PARM = "@DateCreated";

		public const string UPDATEDBY_PARM = "@UpdatedBy";

		public const string DATEUPDATED_PARM = "@DateUpdated";

		public const string TRIPDATE_PARM = "@TripDate";

		public const string TRIPTIME_PARM = "@TripTime";

		public const string VEHICLEID_PARM = "@VehicleID";

		public const string NAME_PARM = "@Name";

		public const string PURPOSE_PARM = "@Purpose";

		public const string DRIVERID_PARM = "@DriverID";

		public const string PREVIOUSREADING_PARM = "@PreviousReading";

		public const string CURRENTREADING_PARM = "@CurrentReading";

		public const string MILEAGE_PARM = "@Mileage";

		public const string REMARKS_PARM = "@Remarks";

		public const string ROWINDEX_PARM = "@RowIndex";

		public const string VEHICLEMILEAGETRACK_TABLE = "Vehicle_Mileage_Track";

		public const string VEHICLEMILEAGETRACKDETAIL_TABLE = "Vehicle_Mileage_Track_Detail";

		public VehicleMileageTrack(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateVehicleMileageTrackText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vehicle_Mileage_Track", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("IsDateWise", "@IsDateWise"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Vehicle_Mileage_Track", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateVehicleMileageTrackCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateVehicleMileageTrackText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateVehicleMileageTrackText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@IsDateWise", SqlDbType.Bit);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@IsDateWise"].SourceColumn = "IsDateWise";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
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

		private string GetInsertUpdateVehicleMileageTrackDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vehicle_Mileage_Track_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TripDate", "@TripDate"), new FieldValue("TripTime", "@TripTime"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("Name", "@Name"), new FieldValue("Purpose", "@Purpose"), new FieldValue("DriverID", "@DriverID"), new FieldValue("PreviousReading", "@PreviousReading"), new FieldValue("CurrentReading", "@CurrentReading"), new FieldValue("Mileage", "@Mileage"), new FieldValue("Remarks", "@Remarks"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateVehicleMileageTrackDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateVehicleMileageTrackDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateVehicleMileageTrackDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TripDate", SqlDbType.DateTime);
			parameters.Add("@TripTime", SqlDbType.DateTime);
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters.Add("@Name", SqlDbType.NVarChar);
			parameters.Add("@Purpose", SqlDbType.NVarChar);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@PreviousReading", SqlDbType.Int);
			parameters.Add("@CurrentReading", SqlDbType.Int);
			parameters.Add("@Mileage", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TripDate"].SourceColumn = "TripDate";
			parameters["@TripTime"].SourceColumn = "TripTime";
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@Name"].SourceColumn = "Name";
			parameters["@Purpose"].SourceColumn = "Purpose";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@PreviousReading"].SourceColumn = "PreviousReading";
			parameters["@CurrentReading"].SourceColumn = "CurrentReading";
			parameters["@Mileage"].SourceColumn = "Mileage";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(VehicleMileageTrackData journalData)
		{
			return true;
		}

		public bool InsertUpdateVehicleMileageTrack(VehicleMileageTrackData vehiclemileagetrackData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateVehicleMileageTrackCommand = GetInsertUpdateVehicleMileageTrackCommand(isUpdate);
			try
			{
				DataRow dataRow = vehiclemileagetrackData.VehicleMileageTrackTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Vehicle_Mileage_Track", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in vehiclemileagetrackData.VehicleMileageTrackDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
					row["SysDocID"] = dataRow["SysDocID"];
				}
				if (isUpdate)
				{
					flag &= DeleteVehicleMileageTrackDetailsRows(text, sqlTransaction);
				}
				insertUpdateVehicleMileageTrackCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(vehiclemileagetrackData, "Vehicle_Mileage_Track", insertUpdateVehicleMileageTrackCommand)) : (flag & Insert(vehiclemileagetrackData, "Vehicle_Mileage_Track", insertUpdateVehicleMileageTrackCommand)));
				insertUpdateVehicleMileageTrackCommand = GetInsertUpdateVehicleMileageTrackDetailsCommand(isUpdate: false);
				insertUpdateVehicleMileageTrackCommand.Transaction = sqlTransaction;
				if (vehiclemileagetrackData.VehicleMileageTrackDetailTable.Rows.Count > 0)
				{
					flag &= Insert(vehiclemileagetrackData, "Vehicle_Mileage_Track_Detail", insertUpdateVehicleMileageTrackCommand);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Vehicle_Mileage_Track", "SysDocID", text2, "VoucherID", text, sqlTransaction, !isUpdate);
				string entityName = "VehicleMileageTrack";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Vehicle_Mileage_Track", "VoucherID", sqlTransaction);
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

		public VehicleMileageTrackData GetVehicleMileageTrackByID(string sysDocID, string voucherID)
		{
			try
			{
				VehicleMileageTrackData vehicleMileageTrackData = new VehicleMileageTrackData();
				string textCommand = "SELECT * FROM Vehicle_Mileage_Track WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(vehicleMileageTrackData, "Vehicle_Mileage_Track", textCommand);
				if (vehicleMileageTrackData == null || vehicleMileageTrackData.Tables.Count == 0 || vehicleMileageTrackData.Tables["Vehicle_Mileage_Track"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT *\r\n                        FROM Vehicle_Mileage_Track_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(vehicleMileageTrackData, "Vehicle_Mileage_Track_Detail", textCommand);
				return vehicleMileageTrackData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteVehicleMileageTrackDetailsRows(string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Vehicle_Mileage_Track_Detail WHERE VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteVehicleMileageTrack(string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteVehicleMileageTrackDetailsRows(voucherID, sqlTransaction);
				text = "DELETE FROM Vehicle_Mileage_Track WHERE VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("VehicleMileageTrack", voucherID, activityType, sqlTransaction);
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

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SysDocID,VoucherID,TransactionDate,IsDateWise FROM Vehicle_Mileage_Track ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Vehicle_Mileage_Track", sqlCommand);
			return dataSet;
		}

		public DataSet GetPreviousReadingValue(string VehicleID)
		{
			try
			{
				VehicleMileageTrackData vehicleMileageTrackData = new VehicleMileageTrackData();
				string textCommand = "SELECT TOP 1 * FROM Vehicle_Mileage_Track_Detail WHERE VehicleID='" + VehicleID + "' order By TripDate desc ";
				FillDataSet(vehicleMileageTrackData, "Vehicle_Mileage_Track_Detail", textCommand);
				if (vehicleMileageTrackData == null || vehicleMileageTrackData.Tables.Count == 0 || vehicleMileageTrackData.Tables["Vehicle_Mileage_Track_Detail"].Rows.Count == 0)
				{
					return null;
				}
				return vehicleMileageTrackData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetVehicleMileageTrackToPrint(string sysDocID, string[] voucherID)
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
				string textCommand = " SELECT  VMT.* FROM Vehicle_Mileage_Track VMT  WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Vehicle_Mileage_Track", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Vehicle_Mileage_Track"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = " SELECT VMT.*,V.VehicleName AS VehicleName,D.DriverName AS DriverName\t\t\t\t\r\n                           FROM Vehicle_Mileage_Track_Detail VMT \r\n                            LEFT JOIN Vehicle V ON V.VehicleID=VMT.VehicleID\r\n                            LEFT JOIN Driver D ON D.DriverID = VMT.DriverID\r\n                                     WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                                              ORDER BY RowIndex";
				FillDataSet(dataSet, "Vehicle_Mileage_Track_Detail", textCommand);
				dataSet.Relations.Add("Vehicle_Mil_REl", new DataColumn[2]
				{
					dataSet.Tables["Vehicle_Mileage_Track"].Columns["SysDocID"],
					dataSet.Tables["Vehicle_Mileage_Track"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Vehicle_Mileage_Track_Detail"].Columns["SysDocID"],
					dataSet.Tables["Vehicle_Mileage_Track_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
