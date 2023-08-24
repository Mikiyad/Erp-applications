using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Micromind.Data
{
	public sealed class ShipmentAddresses : StoreObject
	{
		private const string SHIPMENTID_PARM = "@ShipmentID";

		private const string PARTNERID_PARM = "@PartnerID";

		private const string SHIPMENTNAME_PARM = "@ShipmentName";

		private const string LINE1_PARM = "@Line1";

		private const string LINE2_PARM = "@Line2";

		private const string LINE3_PARM = "@Line3";

		private const string LINE4_PARM = "@Line4";

		private const string LINE5_PARM = "@Line5";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string DESCRIPTION_PARM = "@Description";

		private const string NOTE_PARM = "@Note";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		public bool CheckConcurrency = true;

		public ShipmentAddresses(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Shipment Addresses]", new FieldValue("PartnerID", "@PartnerID"), new FieldValue("ShipmentName", "@ShipmentName"), new FieldValue("Line1", "@Line1"), new FieldValue("Line2", "@Line2"), new FieldValue("Line3", "@Line3"), new FieldValue("Line4", "@Line4"), new FieldValue("Line5", "@Line5"), new FieldValue("Description", "@Description"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Shipment Addresses]", new FieldValue("ShipmentID", "@ShipmentID", isUpdateConditionField: true), new FieldValue("PartnerID", "@PartnerID"), new FieldValue("ShipmentName", "@ShipmentName"), new FieldValue("Line1", "@Line1"), new FieldValue("Line2", "@Line2"), new FieldValue("Line3", "@Line3"), new FieldValue("Line4", "@Line4"), new FieldValue("Line5", "@Line5"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Description", "@Description"), new FieldValue("Note", "@Note"));
			if (CheckConcurrency)
			{
				sqlBuilder.AddInsertUpdateParameters("[Shipment Addresses]", new FieldValue("DateTimeStamp", "@DateTimeStamp", isUpdateConditionField: true, checkForNullValue: true));
			}
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@PartnerID", SqlDbType.Int);
				parameters.Add("@ShipmentName", SqlDbType.NVarChar);
				parameters.Add("@Line1", SqlDbType.NVarChar);
				parameters.Add("@Line2", SqlDbType.NVarChar);
				parameters.Add("@Line3", SqlDbType.NVarChar);
				parameters.Add("@Line4", SqlDbType.NVarChar);
				parameters.Add("@Line5", SqlDbType.NVarChar);
				parameters.Add("@Description", SqlDbType.NVarChar);
				parameters["@PartnerID"].SourceColumn = "PartnerID";
				parameters["@ShipmentName"].SourceColumn = "ShipmentName";
				parameters["@Line1"].SourceColumn = "Line1";
				parameters["@Line2"].SourceColumn = "Line2";
				parameters["@Line3"].SourceColumn = "Line3";
				parameters["@Line4"].SourceColumn = "Line4";
				parameters["@Line5"].SourceColumn = "Line5";
				parameters["@Description"].SourceColumn = "Description";
			}
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand(GetUpdateText(), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add("@ShipmentID", SqlDbType.Int);
				parameters.Add("@PartnerID", SqlDbType.Int);
				parameters.Add("@ShipmentName", SqlDbType.NVarChar);
				parameters.Add("@Line1", SqlDbType.NVarChar);
				parameters.Add("@Line2", SqlDbType.NVarChar);
				parameters.Add("@Line3", SqlDbType.NVarChar);
				parameters.Add("@Line4", SqlDbType.NVarChar);
				parameters.Add("@Line5", SqlDbType.NVarChar);
				parameters.Add("@IsInactive", SqlDbType.Bit);
				parameters.Add("@Description", SqlDbType.NVarChar);
				parameters.Add("@Note", SqlDbType.NText);
				parameters.Add("@DateTimeStamp", SqlDbType.DateTime);
				parameters["@ShipmentID"].SourceColumn = "ShipmentID";
				parameters["@PartnerID"].SourceColumn = "PartnerID";
				parameters["@ShipmentName"].SourceColumn = "ShipmentName";
				parameters["@Line1"].SourceColumn = "Line1";
				parameters["@Line2"].SourceColumn = "Line2";
				parameters["@Line3"].SourceColumn = "Line3";
				parameters["@Line4"].SourceColumn = "Line4";
				parameters["@Line5"].SourceColumn = "Line5";
				parameters["@IsInactive"].SourceColumn = "IsInactive";
				parameters["@Description"].SourceColumn = "Description";
				parameters["@DateTimeStamp"].SourceColumn = "DateTimeStamp";
				parameters["@Note"].SourceColumn = "Note";
			}
			return updateCommand;
		}

		public bool InsertShipmentAddress(ShipmentAddressData shipmentAddressData)
		{
			SqlCommand insertCommand = GetInsertCommand();
			base.DBConfig.ConnectionMustClose = false;
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(shipmentAddressData, "[Shipment Addresses]", insertCommand);
				if (!flag)
				{
					return flag;
				}
				object insertedRowIdentity = GetInsertedRowIdentity("[Shipment Addresses]", insertCommand);
				shipmentAddressData.ShipmentAddressTable.Rows[0]["ShipmentID"] = insertedRowIdentity;
				UpdateTableRowByID("[Shipment Addresses]", "ShipmentID", "DateTimeStamp", insertedRowIdentity, DateTime.Now, sqlTransaction);
				string entiyID = shipmentAddressData.ShipmentAddressTable.Rows[0]["ShipmentName"].ToString();
				AddActivityLog("Shipment Address", entiyID, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("[Shipment Addresses]", "ShipmentID", insertedRowIdentity, sqlTransaction, isInsert: true);
				return flag;
			}
			catch (Exception ex)
			{
				flag = false;
				throw ex;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool UpdateShipmentAddress(ShipmentAddressData shipmentAddressData)
		{
			bool flag = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(shipmentAddressData, "[Shipment Addresses]", updateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = shipmentAddressData.ShipmentAddressTable.Rows[0]["ShipmentID"];
				UpdateTableRowByID("[Shipment Addresses]", "ShipmentID", "DateTimeStamp", obj, DateTime.Now, sqlTransaction);
				string entiyID = shipmentAddressData.ShipmentAddressTable.Rows[0]["ShipmentName"].ToString();
				AddActivityLog("Shipment Address", entiyID, ActivityTypes.Update, null);
				UpdateTableRowInsertUpdateInfo("[Shipment Addresses]", "ShipmentID", obj, sqlTransaction, isInsert: false);
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

		public ShipmentAddressData GetShipmentAddresses()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Shipment Addresses]";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddOrderByColumn("[Shipment Addresses]", "ShipmentName");
			sqlBuilder.IsComparing = false;
			sqlBuilder.UseDistinct = false;
			ShipmentAddressData shipmentAddressData = new ShipmentAddressData();
			FillDataSet(shipmentAddressData, "[Shipment Addresses]", sqlBuilder);
			return shipmentAddressData;
		}

		public DataSet GetShipmentAddressesSummary()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddColumn("[Shipment Addresses]", "ShipmentName");
			sqlBuilder.AddColumn("[Shipment Addresses]", "Description");
			sqlBuilder.AddColumn("[Shipment Addresses]", "ShipmentID");
			sqlBuilder.AddColumn("[Shipment Addresses]", "IsInactive");
			sqlBuilder.AddColumn("Partners", "Name");
			sqlBuilder.AddJointer("[Shipment Addresses]", "PartnerID", "Partners", "PartnerID");
			sqlBuilder.AddOrderByColumn("[Shipment Addresses]", "ShipmentName");
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = true;
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "[Shipment Addresses]", sqlBuilder);
			return dataSet;
		}

		public ShipmentAddressData GetShipmentAddressesByShipmentID(int shipmentID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ShipmentID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = shipmentID;
			commandHelper.TableName = "[Shipment Addresses]";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.UseDistinct = false;
			ShipmentAddressData shipmentAddressData = new ShipmentAddressData();
			FillDataSet(shipmentAddressData, "[Shipment Addresses]", sqlBuilder);
			return shipmentAddressData;
		}

		public bool DeleteShipmentAddress(int shipmetAddressID)
		{
			bool flag = true;
			try
			{
				string shipmentAddressNameByID = GetShipmentAddressNameByID(shipmetAddressID);
				flag = DeleteTableRowByID("[Shipment Addresses]", "ShipmentID", shipmetAddressID.ToString());
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Shipment Address", shipmentAddressNameByID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetShipmentAddressNameByID(object id)
		{
			try
			{
				object obj = ExecuteSelectScalar("[Shipment Addresses]", "ShipmentID", id, "ShipmentName");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "";
		}

		public ShipmentAddressData GetShipmentAddressesByPartnerID(int partnerID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PartnerID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = partnerID;
			commandHelper.TableName = "[Shipment Addresses]";
			sqlBuilder.AddCommandHelper(commandHelper);
			ShipmentAddressData shipmentAddressData = new ShipmentAddressData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(shipmentAddressData, "[Shipment Addresses]", sqlBuilder);
			return shipmentAddressData;
		}

		public bool ActivateShipemtAddress(object id, bool activate)
		{
			string shipmentAddressNameByID = GetShipmentAddressNameByID(id);
			activate = !activate;
			bool num = UpdateTableRowByID("[Shipment Addresses]", "ShipmentID", "IsInactive", id, Convert.ToByte(activate));
			if (num)
			{
				if (!activate)
				{
					AddActivityLog("Shipment Address", shipmentAddressNameByID, ActivityTypes.Activate, null);
					return num;
				}
				AddActivityLog("Shipment Address", shipmentAddressNameByID, ActivityTypes.Inactivate, null);
			}
			return num;
		}

		public string GetShipmentAddressIDByName(string shipmentName, string partnerID)
		{
			try
			{
				string exp = "SELECT ShipmentID FROM [Shipment Addresses] WHERE ShipmentName='" + shipmentName + "' AND PartnerID=" + partnerID;
				object obj = ExecuteScalar(exp);
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "-1";
		}

		public bool ExistShipmentName(int partnerID, string shipmentName)
		{
			try
			{
				return IsTableFieldValueExist("[Shipment Addresses]", "PartnerID", "ShipmentName", partnerID, shipmentName);
			}
			catch
			{
				throw;
			}
		}

		public bool ExistShipmentName(string shipmentName)
		{
			try
			{
				return IsTableFieldValueExist("[Shipment Addresses]", "ShipmentName", shipmentName);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetShipmentAddressesByFields(params string[] columns)
		{
			return GetShipmentAddressesByFields(isInactive: true, new int[0], columns);
		}

		public DataSet GetShipmentAddressesByFields(int[] addressesID, params string[] columns)
		{
			return GetShipmentAddressesByFields(isInactive: true, addressesID, columns);
		}

		public DataSet GetShipmentAddressesByFields(bool isInactive, int[] addressesID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			BuildSqlFields(sqlBuilder, columns);
			if (addressesID != null && addressesID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "ShipmentID";
				commandHelper.FieldValue = addressesID;
				commandHelper.TableName = "[Shipment Addresses]";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.TableName = "[Shipment Addresses]";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			sqlBuilder.AddJointer("[Shipment Addresses]", "PartnerID", "Partners", "PartnerID");
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(dataSet, "[Shipment Addresses]", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDefaultBillingAddress(int[] partnersID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT sa.*,p.Name from [Shipment Addresses] sa ");
			stringBuilder.Append("LEFT OUTER JOIN Partners p ");
			stringBuilder.Append("ON p.PartnerID = sa.PartnerID ");
			stringBuilder.Append("WHERE sa.ShipmentID IN ");
			stringBuilder.Append("(SELECT DefaultBillingID FROM [Partner Addresses] pa ");
			stringBuilder.Append("LEFT OUTER JOIN Partners p ");
			stringBuilder.Append("ON p.PartnerID = pa.PartnerID ");
			if (partnersID != null && partnersID.Length != 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("WHERE P.PartnerID IN(");
				for (int i = 0; i < partnersID.Length; i++)
				{
					stringBuilder2.Append(partnersID[i].ToString()).Append(",");
				}
				stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
				stringBuilder2.Append(") ");
				stringBuilder.Append(stringBuilder2.ToString()).Append(" ");
			}
			stringBuilder.Append(")");
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			try
			{
				FillDataSet(dataSet, "[Shipment Addresses]", stringBuilder.ToString());
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDefaultShippingAddress(int[] partnersID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT sa.*,p.Name from [Shipment Addresses] sa ");
			stringBuilder.Append("LEFT OUTER JOIN Partners p ");
			stringBuilder.Append("ON p.PartnerID = sa.PartnerID ");
			stringBuilder.Append("WHERE sa.ShipmentID IN ");
			stringBuilder.Append("(SELECT DefaultShippingID FROM [Partner Addresses] pa ");
			stringBuilder.Append("LEFT OUTER JOIN Partners p ");
			stringBuilder.Append("ON p.PartnerID = pa.PartnerID ");
			if (partnersID != null && partnersID.Length != 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("WHERE P.PartnerID IN(");
				for (int i = 0; i < partnersID.Length; i++)
				{
					stringBuilder2.Append(partnersID[i].ToString()).Append(",");
				}
				stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
				stringBuilder2.Append(") ");
				stringBuilder.Append(stringBuilder2.ToString()).Append(" ");
			}
			stringBuilder.Append(")");
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			try
			{
				FillDataSet(dataSet, "[Shipment Addresses]", stringBuilder.ToString());
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDefaultBillingAddressLines(int[] partnersID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT sa.PartnerID,p.Name,sa.Line1,sa.Line2,sa.Line3,sa.Line4,sa.Line5 from [Shipment Addresses] sa ");
			stringBuilder.Append("LEFT OUTER JOIN Partners p ");
			stringBuilder.Append("ON p.PartnerID = sa.PartnerID ");
			stringBuilder.Append("WHERE sa.ShipmentID IN ");
			stringBuilder.Append("(SELECT DefaultBillingID FROM [Partner Addresses] pa ");
			stringBuilder.Append("LEFT OUTER JOIN Partners p ");
			stringBuilder.Append("ON p.PartnerID = pa.PartnerID ");
			if (partnersID != null && partnersID.Length != 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("WHERE P.PartnerID IN(");
				for (int i = 0; i < partnersID.Length; i++)
				{
					stringBuilder2.Append(partnersID[i].ToString()).Append(",");
				}
				stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
				stringBuilder2.Append(") ");
				stringBuilder.Append(stringBuilder2.ToString()).Append(" ");
			}
			stringBuilder.Append(")");
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			try
			{
				FillDataSet(dataSet, "[Shipment Addresses]", stringBuilder.ToString());
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDefaultShippingAddressLines(int[] partnersID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT sa.PartnerID,p.Name,sa.Line1,sa.Line2,sa.Line3,sa.Line4,sa.Line5 from [Shipment Addresses] sa ");
			stringBuilder.Append("LEFT OUTER JOIN Partners p ");
			stringBuilder.Append("ON p.PartnerID = sa.PartnerID ");
			stringBuilder.Append("WHERE sa.ShipmentID IN ");
			stringBuilder.Append("(SELECT DefaultShippingID FROM [Partner Addresses] pa ");
			stringBuilder.Append("LEFT OUTER JOIN Partners p ");
			stringBuilder.Append("ON p.PartnerID = pa.PartnerID ");
			if (partnersID != null && partnersID.Length != 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("WHERE P.PartnerID IN(");
				for (int i = 0; i < partnersID.Length; i++)
				{
					stringBuilder2.Append(partnersID[i].ToString()).Append(",");
				}
				stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
				stringBuilder2.Append(") ");
				stringBuilder.Append(stringBuilder2.ToString()).Append(" ");
			}
			stringBuilder.Append(")");
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			try
			{
				FillDataSet(dataSet, "[Shipment Addresses]", stringBuilder.ToString());
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
