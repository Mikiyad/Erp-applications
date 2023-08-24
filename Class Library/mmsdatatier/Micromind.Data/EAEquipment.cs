using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EAEquipment : StoreObject
	{
		private const string EA_EQUIPMENT_TABLE = "EA_Equipment";

		private const string EQUIPMENTID_PARM = "@EquipmentID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REGISTRATIONNO_PARM = "@RegistrationNumber";

		private const string JOBID_PARM = "@JobID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string EQUIPMENTCATEGORYID_PARM = "@EquipmentCategoryID";

		private const string EQUIPMENTTYPEID_PARM = "@EquipmentTypeID";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string OWNERSHIP_PARM = "@OwnerShip";

		private const string VENDORID_PARM = "@VendorID";

		private const string PARENTEQUIPMENTID_PARM = "@ParentEquipmentID";

		private const string MODEL_PARM = "@Model";

		private const string COLOR_PARM = "@Color";

		private const string YEAR_PARM = "@Year";

		private const string FUEL_PARM = "@Fuel";

		private const string CAPACITY_PARM = "@Capacity";

		private const string CAPACITYTYPE_PARM = "@CapacityType";

		private const string POWER_PARM = "@Power";

		private const string SERIALNO_PARM = "@SerialNo";

		private const string PLATENO_PARM = "@PlateNo";

		private const string TRACKINGID_PARM = "@TrackingID";

		private const string ENGINENUMBER_PARM = "@EngineNumber";

		private const string OWNEDBY_PARM = "@OwnedBy";

		private const string MAINTENANCEINCHARGE_PARM = "@MaintenanceInCharge";

		private const string NOTIFICATIONEMAIL_PARM = "@NotificationEmail";

		private const string FIXEDASSETGROUPID_PARM = "@FixedAssetGroupID";

		private const string FIXEDASSETID_PARM = "@FixedAssetID";

		private const string ISMETER_PARM = "@IsMeter";

		private const string ISMILEAGE_PARM = "@IsMileage";

		private const string ISHOURS_PARM = "@IsHours";

		private const string INACTIVE_PARM = "@IsInactive";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EAEquipment(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Equipment", new FieldValue("EquipmentID", "@EquipmentID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("RegistrationNumber", "@RegistrationNumber"), new FieldValue("JobID", "@JobID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("EquipmentCategoryID", "@EquipmentCategoryID"), new FieldValue("EquipmentTypeID", "@EquipmentTypeID"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("OwnerShip", "@OwnerShip"), new FieldValue("VendorID", "@VendorID"), new FieldValue("ParentEquipmentID", "@ParentEquipmentID"), new FieldValue("Model", "@Model"), new FieldValue("Color", "@Color"), new FieldValue("Year", "@Year"), new FieldValue("Fuel", "@Fuel"), new FieldValue("Capacity", "@Capacity"), new FieldValue("CapacityType", "@CapacityType"), new FieldValue("Power", "@Power"), new FieldValue("SerialNo", "@SerialNo"), new FieldValue("PlateNo", "@PlateNo"), new FieldValue("IsMeter", "@IsMeter"), new FieldValue("IsMileage", "@IsMileage"), new FieldValue("IsHours", "@IsHours"), new FieldValue("TrackingID", "@TrackingID"), new FieldValue("EngineNumber", "@EngineNumber"), new FieldValue("OwnedBy", "@OwnedBy"), new FieldValue("MaintenanceInCharge", "@MaintenanceInCharge"), new FieldValue("NotificationEmail", "@NotificationEmail"), new FieldValue("FixedAssetGroupID", "@FixedAssetGroupID"), new FieldValue("FixedAssetID", "@FixedAssetID"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EA_Equipment", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EquipmentID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@RegistrationNumber", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@EquipmentCategoryID", SqlDbType.NVarChar);
			parameters.Add("@EquipmentTypeID", SqlDbType.NVarChar);
			parameters.Add("@ExpiryDate", SqlDbType.DateTime);
			parameters.Add("@OwnerShip", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@ParentEquipmentID", SqlDbType.NVarChar);
			parameters.Add("@Model", SqlDbType.NVarChar);
			parameters.Add("@Color", SqlDbType.NVarChar);
			parameters.Add("@Year", SqlDbType.SmallInt);
			parameters.Add("@Fuel", SqlDbType.NVarChar);
			parameters.Add("@Capacity", SqlDbType.NVarChar);
			parameters.Add("@CapacityType", SqlDbType.NVarChar);
			parameters.Add("@Power", SqlDbType.NVarChar);
			parameters.Add("@SerialNo", SqlDbType.NVarChar);
			parameters.Add("@PlateNo", SqlDbType.NVarChar);
			parameters.Add("@TrackingID", SqlDbType.NVarChar);
			parameters.Add("@EngineNumber", SqlDbType.NVarChar);
			parameters.Add("@OwnedBy", SqlDbType.NVarChar);
			parameters.Add("@MaintenanceInCharge", SqlDbType.NVarChar);
			parameters.Add("@IsMeter", SqlDbType.Bit);
			parameters.Add("@IsMileage", SqlDbType.Bit);
			parameters.Add("@IsHours", SqlDbType.Bit);
			parameters.Add("@NotificationEmail", SqlDbType.NVarChar);
			parameters.Add("@FixedAssetGroupID", SqlDbType.NVarChar);
			parameters.Add("@FixedAssetID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@EquipmentID"].SourceColumn = "EquipmentID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@RegistrationNumber"].SourceColumn = "RegistrationNumber";
			parameters["@EquipmentCategoryID"].SourceColumn = "EquipmentCategoryID";
			parameters["@EquipmentTypeID"].SourceColumn = "EquipmentTypeID";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@OwnerShip"].SourceColumn = "OwnerShip";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@ParentEquipmentID"].SourceColumn = "ParentEquipmentID";
			parameters["@Model"].SourceColumn = "Model";
			parameters["@Color"].SourceColumn = "Color";
			parameters["@Year"].SourceColumn = "Year";
			parameters["@Fuel"].SourceColumn = "Fuel";
			parameters["@Capacity"].SourceColumn = "Capacity";
			parameters["@CapacityType"].SourceColumn = "CapacityType";
			parameters["@Power"].SourceColumn = "Power";
			parameters["@SerialNo"].SourceColumn = "SerialNo";
			parameters["@PlateNo"].SourceColumn = "PlateNo";
			parameters["@TrackingID"].SourceColumn = "TrackingID";
			parameters["@EngineNumber"].SourceColumn = "EngineNumber";
			parameters["@OwnedBy"].SourceColumn = "OwnedBy";
			parameters["@MaintenanceInCharge"].SourceColumn = "MaintenanceInCharge";
			parameters["@IsMeter"].SourceColumn = "IsMeter";
			parameters["@IsHours"].SourceColumn = "IsHours";
			parameters["@IsMileage"].SourceColumn = "IsMileage";
			parameters["@NotificationEmail"].SourceColumn = "NotificationEmail";
			parameters["@FixedAssetGroupID"].SourceColumn = "FixedAssetGroupID";
			parameters["@FixedAssetID"].SourceColumn = "FixedAssetID";
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

		public bool InsertEquipment(EAEquipmentData accountEA_EquipmentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountEA_EquipmentData, "EA_Equipment", insertUpdateCommand);
				string text = accountEA_EquipmentData.EA_EquipmentTable.Rows[0]["EquipmentID"].ToString();
				AddActivityLog("Enterprise Asset Equipment", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EA_Equipment", "EquipmentID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEquipment(EAEquipmentData accountEA_EquipmentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEA_EquipmentData, "EA_Equipment", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEA_EquipmentData.EA_EquipmentTable.Rows[0]["EquipmentID"];
				UpdateTableRowByID("EA_Equipment", "EquipmentID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				accountEA_EquipmentData.EA_EquipmentTable.Rows[0]["Description"].ToString();
				AddActivityLog("Enterprise Asset Equipment", obj.ToString(), ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EA_Equipment", "EquipmentID", obj, sqlTransaction, isInsert: false);
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

		public EAEquipmentData GetEquipment()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EA_Equipment");
			EAEquipmentData eAEquipmentData = new EAEquipmentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(eAEquipmentData, "EA_Equipment", sqlBuilder);
			return eAEquipmentData;
		}

		public bool DeleteEquipment(string equipmentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM EA_Equipment WHERE EquipmentID = '" + equipmentID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Enterprise Asset Equipment", equipmentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EAEquipmentData GetEquipmentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EquipmentID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "EA_Equipment";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EAEquipmentData eAEquipmentData = new EAEquipmentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(eAEquipmentData, "EA_Equipment", sqlBuilder);
			return eAEquipmentData;
		}

		public DataSet GetEquipmentByFields(params string[] columns)
		{
			return GetEquipmentByFields(null, isInactive: true, columns);
		}

		public DataSet GetEquipmentByFields(string[] EquipmentID, params string[] columns)
		{
			return GetEquipmentByFields(EquipmentID, isInactive: true, columns);
		}

		public DataSet GetEquipmentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EA_Equipment");
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
				commandHelper.FieldName = "EquipmentID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "EA_Equipment";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "EA_Equipment", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEquipmentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EquipmentID, Description, RegistrationNumber, \r\n                                Year, Model, ExpiryDate,Capacity\r\n                           FROM EA_Equipment ";
			FillDataSet(dataSet, "EA_Equipment", textCommand);
			return dataSet;
		}

		public DataSet GetEquipmentByCategoryID(string ID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EquipmentID, Description, RegistrationNumber, \r\n                                Year, Model, ExpiryDate,Capacity\r\n                           FROM EA_Equipment where EquipmentCategoryID='" + ID + "'";
			FillDataSet(dataSet, "EA_Equipment", textCommand);
			return dataSet;
		}

		public DataSet GetEquipmentListReport(string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "Select EA.*,CASE EA.CapacityType WHEN 1 THEN 'SEAT' WHEN 2 THEN 'TON' END AS [CAPACITY TYPE], WorkLocationName, EquipmentCategoryName, VendorName, EquipmentTypeName, JobName, FG.AssetGroupName, E.FirstName+''+E.LastName AS Employee from EA_Equipment EA\r\n                            LEFT JOIN Work_Location W ON EA.LocationID=W.WorkLocationID\r\n                            LEFT JOIN EA_Equipment_Category EC ON EA.EquipmentCategoryID=EC.EquipmentCategoryID\r\n                            LEFT JOIN Vendor V ON EA.VendorID=V.VendorID \r\n\t\t\t\t\t\t\tLEFT JOIN EA_Equipment_Type ET ON EA.EquipmentTypeID=ET.EquipmentTypeID \r\n\t\t\t\t\t\t\tLEFT JOIN Job J ON EA.JobID=J.JobID\r\n\t\t\t\t\t\t\tLEFT JOIN FixedAsset_Group FG ON EA.FixedAssetGroupID=FG.AssetGroupID\r\n\t\t\t\t\t\t\tLEFT JOIN Employee E ON EA.OwnedBy=E.EmployeeID\r\n                            WHERE 1=1 ";
			if (fromEquipment != "")
			{
				text = text + " AND EquipmentID>='" + fromEquipment + "'";
			}
			if (toEquipment != "")
			{
				text = text + " AND EquipmentID<='" + toEquipment + "'";
			}
			if (fromType != "")
			{
				text = text + " AND EA.EquipmentTypeID>='" + fromType + "'";
			}
			if (toType != "")
			{
				text = text + " AND EA.EquipmentTypeID<='" + toType + "'";
			}
			if (fromCategory != "")
			{
				text = text + " AND EA.EquipmentCategoryID>='" + fromCategory + "'";
			}
			if (toCategory != "")
			{
				text = text + " AND EA.EquipmentCategoryID<='" + toCategory + "'";
			}
			if (fromLocation != "")
			{
				text = text + " AND EA.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			if (fromJob != "")
			{
				text = text + " AND EA.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(EA.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Equipments", text);
			return dataSet;
		}

		public DataSet GetEquipmentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EquipmentID [Code], Description [Name], EquipmentCategoryID \r\n                           FROM EA_Equipment WHERE ISINACTIVE<>1 ORDER BY EquipmentID,Description";
			FillDataSet(dataSet, "EA_Equipment", textCommand);
			return dataSet;
		}

		public DataSet GetEquipmentReport(string fromEquipment, string toEquipment, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "Select EA.*, WorkLocationName, EquipmentCategoryName, VendorName, EquipmentTypeName, JobName, FG.AssetGroupName, E.FirstName+''+E.LastName AS Employee from EA_Equipment EA\r\n                            LEFT JOIN Work_Location W ON EA.LocationID=W.WorkLocationID\r\n                            LEFT JOIN EA_Equipment_Category EC ON EA.EquipmentCategoryID=EC.EquipmentCategoryID\r\n                            LEFT JOIN Vendor V ON EA.VendorID=V.VendorID \r\n\t\t\t\t\t\t\tLEFT JOIN EA_Equipment_Type ET ON EA.EquipmentTypeID=ET.EquipmentTypeID \r\n\t\t\t\t\t\t\tLEFT JOIN Job J ON EA.JobID=J.JobID\r\n\t\t\t\t\t\t\tLEFT JOIN FixedAsset_Group FG ON EA.FixedAssetGroupID=FG.AssetGroupID\r\n\t\t\t\t\t\t\tLEFT JOIN Employee E ON EA.OwnedBy=E.EmployeeID WHERE 1=1 ";
			if (fromEquipment != "")
			{
				text = text + " AND EA.EquipmentID>='" + fromEquipment + "'";
			}
			if (toEquipment != "")
			{
				text = text + " AND EA.EquipmentID<='" + toEquipment + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(EA.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Equipment", text);
			return dataSet;
		}

		public DataSet GetEquipmentFlowReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, string sysDocID, string voucherID)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT * FROM  EA_Requisition JR WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				if (fromDate != DateTime.MinValue)
				{
					text3 = text3 + " AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				}
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
				FillDataSet(dataSet, "Requisition", text3);
				DataSet dataSet2 = new DataSet();
				text3 = " select MED.EquipmentID, MED.LocationID, MED.JobID,'' AS Employee,''  AS ProductID, eq.Description, EQ.Capacity, EQ.Color, EQ.RegistrationNumber,'' AS Employee,'' AS Product, JobName,0 AS Quantity, 0 AS Amount, 0 AS Unitprice\r\n                         from EA_Mobilization_Equipment__Detail MED  LEFT JOIN EA_Equipment EQ ON MED.EquipmentID = EQ.EquipmentID LEFT JOIN Job J ON MED.JobID = J.JobID where SourceVoucherID = '" + voucherID + "' AND SourceSysDocID = '" + sysDocID + "'\r\n                        UNION\r\n\r\n                        select '', '', '', MMD.EmployeeID,'', '', '', '','', EmployeeName,'','',0,0,0 from EA_Mobilization_Manpower__Detail MMD LEFT JOIN EA_Mobilization_Equipment__Detail MED ON MMD.SysDocID = MED.SysDocID and MMD.VoucherID = MED.VoucherID LEFT JOIN Employee E ON MMD.EmployeeID = E.EmployeeID\r\n                        UNION\r\n                        select '', MRD.LocationID, MRD.JobID,'',  MRD.ProductID, '', '', '','','',  P.Description, JobName, MRD.Quantity, MRD.Amount, MRD.UnitPrice from EA_Mobilization_Resources__Detail MRD LEFT JOIN EA_Mobilization_Equipment__Detail MED ON MRD.SysDocID = MED.SysDocID and MRD.VoucherID = MED.VoucherID\r\n                        LEFT JOIN Product P ON MRD.ProductID = p.ProductID\r\n                        LEFT JOIN Job J ON MRD.JobID = J.JobID";
				FillDataSet(dataSet2, "Mobilization", text3);
				dataSet.Merge(dataSet2);
				DataSet dataSet3 = new DataSet();
				text3 = "select EAR.*, EC.EquipmentCategoryName, E.Description, E.Capacity,CASE E.CapacityType WHEN 1 THEN 'SEAT' WHEN 2 THEN 'TON' END AS [CAPACITY TYPE], E.Model, E.RegistrationNumber, E.Color, E.Fuel, E.Power, E.Year, E.OwnedBy ,\r\n                                (select WorkLocationName from EA_Equipment_Transfer EAT LEFT JOIN Work_Location W ON EAT.LocationFromID=W.WorkLocationID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS WorkLocationFrom,(select WorkLocationName from EA_Equipment_Transfer EAT LEFT JOIN Work_Location W ON EAT.LocationToID=W.WorkLocationID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS WorkLocationTO,\r\n                                 (select JobName from EA_Equipment_Transfer EAT LEFT JOIN Job W ON EAT.JobFromID=W.JobID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS JobFrom, \r\n                                 (select JobName from EA_Equipment_Transfer EAT LEFT JOIN Job W ON EAT.JobToID=W.JobID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID) AS JobTo,\r\n                                  (select W.FirstName+''+W.LastName from EA_Equipment_Transfer EAT LEFT JOIN Employee W ON EAT.EmployeeFromID=W.EmployeeID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID)  AS EmployeeFromName,\r\n                                  (select W.FirstName+''+w.LastName from EA_Equipment_Transfer EAT LEFT JOIN Employee W ON EAT.EmployeeToID=W.EmployeeID where EAT.VoucherID=EAR.VoucherID and EAT.SysDocID=EAR.SysDocID)  AS EmployeeTOName\r\n                                from EA_Equipment_Transfer EAR \r\n                                LEFT JOIN EA_Equipment E ON EAR.EquipmentID=E.EquipmentID\r\n                                LEFT JOIN EA_Equipment_Category EC \r\n                                ON E.EquipmentCategoryID=EC.EquipmentCategoryID \r\n\t\t\t\t\t\t\t\tLEFT JOIN Work_Location W ON EAR.LocationToID=W.WorkLocationID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Job ON EAR.JobToID=job.JobID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Employee ON EAR.EmployeeFromID=Employee.EmployeeID\r\n\t\t\t\t\t\t         WHERE ReqVoucherID='" + voucherID + "' AND ReqSysDocID='" + sysDocID + "'";
				FillDataSet(dataSet3, "Transfer", text3);
				dataSet.Merge(dataSet3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
