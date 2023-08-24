using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Micromind.Data
{
	public sealed class Vehicle : StoreObject
	{
		private const string VEHICLE_TABLE = "Vehicle";

		private const string VEHICLEID_PARM = "@VehicleID";

		private const string VEHICLENAME_PARM = "@VehicleName";

		private const string COUNTRYID_PARM = "@CountryID";

		private const string REGISTRATIONNO_PARM = "@RegistrationNumber";

		private const string CITYID_PARM = "@RegistrationCityID";

		private const string WORKINGAREA_PARM = "@WorkingAreaID";

		private const string CHASISNO_PARM = "@ChasisNumber";

		private const string VEHICLETYPE_PARM = "@VehicleTypeID";

		private const string COLOR_PARM = "@Color";

		private const string YEAR_PARM = "@Year";

		private const string FUEL_PARM = "@Fuel";

		private const string MODEL_PARM = "@Model";

		private const string FIXEDASSET_PARM = "@FixedAssetID";

		private const string INSURANCEVENDOR_PARM = "@InsuranceVendorID";

		private const string INSPOLICYNO_PARM = "@InsurancePolicyNumber";

		private const string INSEXPIRYDATE_PARM = "@InsuranceExpiryDate";

		private const string REGEXPIRYDATE_PARM = "@RegistrationExpiryDate";

		private const string TRACKINGNO_PARM = "@TrackingNumber";

		private const string LOCATION_PARM = "@LocationID";

		private const string DIVISION_PARM = "@DivisionID";

		private const string DRIVERID_PARM = "@DriverID";

		private const string OWNEDBY_PARM = "@OwnedBy";

		private const string WEIGHTCAPACITY_PARM = "@WeightCapacity";

		private const string VEHICLEWEIGHT_PARM = "@VehicleWeight";

		private const string PLATENO_PARM = "@PlateNo";

		private const string ORIGINID_PARM = "@Origin";

		private const string TRAFFICFILENO_PARM = "@TrafficFileNo";

		private const string INACTIVE_PARM = "@IsInactive";

		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Vehicle(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vehicle", new FieldValue("VehicleID", "@VehicleID", isUpdateConditionField: true), new FieldValue("VehicleName", "@VehicleName"), new FieldValue("RegistrationNumber", "@RegistrationNumber"), new FieldValue("RegistrationCityID", "@RegistrationCityID"), new FieldValue("CountryID", "@CountryID"), new FieldValue("WorkingAreaID", "@WorkingAreaID"), new FieldValue("ChasisNumber", "@ChasisNumber"), new FieldValue("VehicleTypeID", "@VehicleTypeID"), new FieldValue("Color", "@Color"), new FieldValue("Year", "@Year"), new FieldValue("Fuel", "@Fuel"), new FieldValue("Model", "@Model"), new FieldValue("FixedAssetID", "@FixedAssetID"), new FieldValue("InsuranceVendorID", "@InsuranceVendorID"), new FieldValue("InsurancePolicyNumber", "@InsurancePolicyNumber"), new FieldValue("InsuranceExpiryDate", "@InsuranceExpiryDate"), new FieldValue("RegistrationExpiryDate", "@RegistrationExpiryDate"), new FieldValue("TrackingNumber", "@TrackingNumber"), new FieldValue("LocationID", "@LocationID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("DriverID", "@DriverID"), new FieldValue("OwnedBy", "@OwnedBy"), new FieldValue("TrafficFileNo", "@TrafficFileNo"), new FieldValue("PlateNo", "@PlateNo"), new FieldValue("Origin", "@Origin"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("WeightCapacity", "@WeightCapacity"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("Note", "@Note"), new FieldValue("VehicleWeight", "@VehicleWeight"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Vehicle", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters.Add("@VehicleName", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@RegistrationCityID", SqlDbType.NVarChar);
			parameters.Add("@RegistrationNumber", SqlDbType.NVarChar);
			parameters.Add("@WorkingAreaID", SqlDbType.NVarChar);
			parameters.Add("@ChasisNumber", SqlDbType.NVarChar);
			parameters.Add("@VehicleTypeID", SqlDbType.NVarChar);
			parameters.Add("@Color", SqlDbType.NVarChar);
			parameters.Add("@Year", SqlDbType.SmallInt);
			parameters.Add("@Fuel", SqlDbType.NVarChar);
			parameters.Add("@Model", SqlDbType.NVarChar);
			parameters.Add("@FixedAssetID", SqlDbType.NVarChar);
			parameters.Add("@InsuranceVendorID", SqlDbType.NVarChar);
			parameters.Add("@InsurancePolicyNumber", SqlDbType.NVarChar);
			parameters.Add("@InsuranceExpiryDate", SqlDbType.DateTime);
			parameters.Add("@RegistrationExpiryDate", SqlDbType.DateTime);
			parameters.Add("@TrackingNumber", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@OwnedBy", SqlDbType.NVarChar);
			parameters.Add("@TrafficFileNo", SqlDbType.NVarChar);
			parameters.Add("@PlateNo", SqlDbType.NVarChar);
			parameters.Add("@Origin", SqlDbType.NVarChar);
			parameters.Add("@WeightCapacity", SqlDbType.Decimal);
			parameters.Add("@VehicleWeight", SqlDbType.Decimal);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@VehicleName"].SourceColumn = "VehicleName";
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@RegistrationCityID"].SourceColumn = "RegistrationCityID";
			parameters["@RegistrationNumber"].SourceColumn = "RegistrationNumber";
			parameters["@WorkingAreaID"].SourceColumn = "WorkingAreaID";
			parameters["@ChasisNumber"].SourceColumn = "ChasisNumber";
			parameters["@VehicleTypeID"].SourceColumn = "VehicleTypeID";
			parameters["@Color"].SourceColumn = "Color";
			parameters["@Year"].SourceColumn = "Year";
			parameters["@Fuel"].SourceColumn = "Fuel";
			parameters["@Model"].SourceColumn = "Model";
			parameters["@FixedAssetID"].SourceColumn = "FixedAssetID";
			parameters["@InsuranceVendorID"].SourceColumn = "InsuranceVendorID";
			parameters["@InsurancePolicyNumber"].SourceColumn = "InsurancePolicyNumber";
			parameters["@InsuranceExpiryDate"].SourceColumn = "InsuranceExpiryDate";
			parameters["@RegistrationExpiryDate"].SourceColumn = "RegistrationExpiryDate";
			parameters["@TrackingNumber"].SourceColumn = "TrackingNumber";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@OwnedBy"].SourceColumn = "OwnedBy";
			parameters["@TrafficFileNo"].SourceColumn = "TrafficFileNo";
			parameters["@PlateNo"].SourceColumn = "PlateNo";
			parameters["@Origin"].SourceColumn = "Origin";
			parameters["@WeightCapacity"].SourceColumn = "WeightCapacity";
			parameters["@VehicleWeight"].SourceColumn = "VehicleWeight";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertVehicle(VehicleData accountVehicleData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			DataSet dataSet = new DataSet();
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountVehicleData, "Vehicle", insertUpdateCommand);
				if (accountVehicleData.Tables["Analysis"].Rows.Count > 0)
				{
					DataTable table = accountVehicleData.Tables["Analysis"].Copy();
					dataSet.Tables.Add(table);
					flag &= new Analysis(base.DBConfig).InsertAutoAnalysis(dataSet);
				}
				string text = accountVehicleData.VehicleTable.Rows[0]["VehicleID"].ToString();
				AddActivityLog("Vehicle", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Vehicle", "VehicleID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateVehicle(VehicleData accountVehicleData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountVehicleData, "Vehicle", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountVehicleData.VehicleTable.Rows[0]["VehicleID"];
				UpdateTableRowByID("Vehicle", "VehicleID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountVehicleData.VehicleTable.Rows[0]["VehicleName"].ToString();
				AddActivityLog("Vehicle", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Vehicle", "VehicleID", obj, sqlTransaction, isInsert: false);
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

		public VehicleData GetVehicle()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vehicle");
			VehicleData vehicleData = new VehicleData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vehicleData, "Vehicle", sqlBuilder);
			return vehicleData;
		}

		public bool DeleteVehicle(string vehicleID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Vehicle WHERE VehicleID = '" + vehicleID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Vehicle", vehicleID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public VehicleData GetVehicleByID(string id)
		{
			VehicleData vehicleData = new VehicleData();
			string textCommand = "SELECT *, \r\n\t\t\t\t\t\t\t(SELECT Count(P2.VehicleID)\r\n\t\t\t\t\t\t\tFROM Vehicle   P2 WHERE P2.Photo IS Not NULL AND P2.VehicleID=P1.VehicleID)  AS HasPhoto  from Vehicle P1 where VehicleID='" + id + "'";
			FillDataSet(vehicleData, "Vehicle", textCommand);
			return vehicleData;
		}

		public DataSet GetVehicleByFields(params string[] columns)
		{
			return GetVehicleByFields(null, isInactive: true, columns);
		}

		public DataSet GetVehicleByFields(string[] vehicleID, params string[] columns)
		{
			return GetVehicleByFields(vehicleID, isInactive: true, columns);
		}

		public DataSet GetVehicleByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vehicle");
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
				commandHelper.FieldName = "VehicleID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Vehicle";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Vehicle", sqlBuilder);
			return dataSet;
		}

		public DataSet GetVehicleList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VehicleID, VehicleName, RegistrationNumber, \r\n                                Year, Model, RegistrationExpiryDate, InsuranceExpiryDate, WeightCapacity, VehicleWeight, GL.GenericListName AS [Vehicle Type],Vehicle.[IsInactive] [Inactive]\r\n                           FROM Vehicle LEFT OUTER JOIN Generic_List GL ON GL.GenericListID=VehicleTypeID ";
			FillDataSet(dataSet, "Vehicle", textCommand);
			return dataSet;
		}

		public DataSet GetVehicleComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VehicleID [Code], VehicleName [Name]\r\n                           FROM Vehicle ORDER BY VehicleID,VehicleName";
			FillDataSet(dataSet, "Vehicle", textCommand);
			return dataSet;
		}

		public DataSet GetVehicleList(string fromVehicle, string toVehicle)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT VehicleID,VehicleName\r\n                           FROM Vehicle WHERE 1=1 ";
			if (fromVehicle != "")
			{
				text = text + " AND VehicleID BETWEEN '" + fromVehicle + "' AND '" + toVehicle + "'";
			}
			text += " ORDER BY VehicleID,VehicleName";
			FillDataSet(dataSet, "Vehicle", text);
			return dataSet;
		}

		public bool AddVehiclePhoto(string vehicleID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Vehicle SET Photo=@Photo WHERE VehicleID='" + vehicleID + "'");
				sqlCommand.Parameters.AddWithValue("@Photo", image);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public MemoryStream GetvehicleThumbnailImage(string vehicleID)
		{
			string exp = "SELECT Photo\r\n\t\t\t\t\t\t   FROM Vehicle WHERE VehicleID='" + vehicleID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				byte[] array = (byte[])obj;
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(array, 0, array.Length);
				return memoryStream;
			}
			return null;
		}

		public bool RemoveVehiclePhoto(string vehicleID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Vehicle SET Photo= Null WHERE VehicleID='" + vehicleID + "'");
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
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
	}
}
