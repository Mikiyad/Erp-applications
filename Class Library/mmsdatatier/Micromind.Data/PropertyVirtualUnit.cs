using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyVirtualUnit : StoreObject
	{
		private const string PROPERTYVIRTUALUNITID_PARM = "@PropertyVirtualUnitID";

		private const string PROPERTYVIRTUALUNITNAME_PARM = "@PropertyVirtualUnitName";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string NOTE_PARM = "@Note";

		private const string PROPERTYVIRTUALUNIT_TABLE = "Property_VirtualUnit";

		private const string PROPERTYVIRTUALUNITDETAIL_TABLE = "Property_VirtualUnit_Detail";

		private const string PROPERTYUNITID_PARM = "@PropertyUnitID";

		private static string DESCRIPTION_PARM = "@Description";

		private static string SHARINGPERCENT_PARM = "@SharingPercent";

		private static string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "CreatedBy";

		private const string DATECREATED_PARM = "DateCreated";

		private const string UPDATEDBY_PARM = "UpdatedBy";

		private const string DATEUPDATED_PARM = "DateUpdated";

		public PropertyVirtualUnit(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_VirtualUnit", new FieldValue("PropertyVirtualUnitID", "@PropertyVirtualUnitID", isUpdateConditionField: true), new FieldValue("PropertyVirtualUnitName", "@PropertyVirtualUnitName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_VirtualUnit", new FieldValue("DateUpdated", "DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@PropertyVirtualUnitID", SqlDbType.NVarChar);
			parameters.Add("@PropertyVirtualUnitName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@PropertyVirtualUnitID"].SourceColumn = "PropertyVirtualUnitID";
			parameters["@PropertyVirtualUnitName"].SourceColumn = "PropertyVirtualUnitName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				parameters.Add("DateUpdated", SqlDbType.DateTime);
				parameters["DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_VirtualUnit_Detail", new FieldValue("PropertyVirtualUnitID", "@PropertyVirtualUnitID"), new FieldValue("PropertyUnitID", "@PropertyUnitID"), new FieldValue(PropertyVirtualUnitData.SHARINGPERCENT_FIELD, SHARINGPERCENT_PARM), new FieldValue("RowIndex", ROWINDEX_PARM), new FieldValue(PropertyVirtualUnitData.DESCRIPTION_FIELD, DESCRIPTION_PARM));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_VirtualUnit_Detail", new FieldValue("DateUpdated", "DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@PropertyVirtualUnitID", SqlDbType.NVarChar);
			parameters.Add("@PropertyUnitID", SqlDbType.NVarChar);
			parameters.Add(SHARINGPERCENT_PARM, SqlDbType.Decimal);
			parameters.Add(DESCRIPTION_PARM, SqlDbType.NVarChar);
			parameters.Add(ROWINDEX_PARM, SqlDbType.Int);
			parameters["@PropertyVirtualUnitID"].SourceColumn = "PropertyVirtualUnitID";
			parameters["@PropertyUnitID"].SourceColumn = "PropertyUnitID";
			parameters[DESCRIPTION_PARM].SourceColumn = PropertyVirtualUnitData.DESCRIPTION_FIELD;
			parameters[SHARINGPERCENT_PARM].SourceColumn = PropertyVirtualUnitData.SHARINGPERCENT_FIELD;
			parameters[ROWINDEX_PARM].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				parameters.Add("DateUpdated", SqlDbType.DateTime);
				parameters["DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertPropertyVirtualUnit(PropertyVirtualUnitData accountPropertyVirtualUnitData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountPropertyVirtualUnitData, "Property_VirtualUnit", insertUpdateCommand);
				insertUpdateCommand = GetInsertUpdateDetailsCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag &= Insert(accountPropertyVirtualUnitData, "Property_VirtualUnit_Detail", insertUpdateCommand);
				string text = accountPropertyVirtualUnitData.PropertyVirtualUnitTable.Rows[0]["PropertyVirtualUnitID"].ToString();
				string text2 = accountPropertyVirtualUnitData.PropertyVirtualUnitTable.Rows[0]["PropertyVirtualUnitName"].ToString();
				AddActivityLog("PropertyVirtualUnit", text, ActivityTypes.Add, sqlTransaction);
				if (accountPropertyVirtualUnitData.PropertyVirtualUnitTable.Rows.Count > 0)
				{
					string exp = "INSERT INTO Property_Unit (PropertyUnitID,PropertyUnitName,IsVirtual,Status) VALUES('" + text + "','" + text2 + "',null,0) ";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
					foreach (DataRow row in accountPropertyVirtualUnitData.PropertyVirtualUnitDetailTable.Rows)
					{
						text = row["PropertyUnitID"].ToString();
						string exp2 = "UPDATE Property_Unit  SET IsVirtual=1 where PropertyUnitID='" + text + "'";
						flag &= (ExecuteNonQuery(exp2, sqlTransaction) >= 0);
					}
				}
				UpdateTableRowInsertUpdateInfo("Property_VirtualUnit", "PropertyVirtualUnitID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePropertyVirtualUnit(PropertyVirtualUnitData accountPropertyVirtualUnitData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPropertyVirtualUnitData, "Property_VirtualUnit", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPropertyVirtualUnitData.PropertyVirtualUnitTable.Rows[0]["PropertyVirtualUnitID"];
				insertUpdateCommand = GetInsertUpdateDetailsCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				string commandText = "DELETE FROM Property_VirtualUnit_Detail WHERE PropertyVirtualUnitID = '" + obj + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= Insert(accountPropertyVirtualUnitData, "Property_VirtualUnit_Detail", insertUpdateCommand);
				UpdateTableRowByID("Property_VirtualUnit", "PropertyVirtualUnitID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string text = accountPropertyVirtualUnitData.PropertyVirtualUnitTable.Rows[0]["PropertyVirtualUnitName"].ToString();
				AddActivityLog("PropertyVirtualUnit", text, ActivityTypes.Update, sqlTransaction);
				if (accountPropertyVirtualUnitData.PropertyVirtualUnitTable.Rows.Count > 0)
				{
					commandText = "UPDATE Property_Unit  SET PropertyUnitName='" + text + "',IsVirtual=null where PropertyUnitID='" + obj + "'";
					flag &= (ExecuteNonQuery(commandText, sqlTransaction) >= 0);
					foreach (DataRow row in accountPropertyVirtualUnitData.PropertyVirtualUnitDetailTable.Rows)
					{
						obj = row["PropertyUnitID"].ToString();
						string exp = "UPDATE Property_Unit  SET IsVirtual=1 where PropertyUnitID='" + obj + "'";
						flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
					}
				}
				UpdateTableRowInsertUpdateInfo("Property_VirtualUnit", "PropertyVirtualUnitID", obj, sqlTransaction, isInsert: false);
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

		public PropertyVirtualUnitData GetPropertyVirtualUnit()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_VirtualUnit");
			PropertyVirtualUnitData propertyVirtualUnitData = new PropertyVirtualUnitData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyVirtualUnitData, "Property_VirtualUnit", sqlBuilder);
			return propertyVirtualUnitData;
		}

		public bool DeletePropertyVirtualUnit(string PropertyVirtualUnitID)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			SqlCommand sqlCommand = new SqlCommand();
			try
			{
				sqlTransaction = (sqlCommand.Transaction = base.DBConfig.StartNewTransaction());
				PropertyVirtualUnitData propertyVirtualUnitByID = GetPropertyVirtualUnitByID(PropertyVirtualUnitID);
				string exp;
				if (propertyVirtualUnitByID.PropertyVirtualUnitTable.Rows.Count > 0)
				{
					string str = propertyVirtualUnitByID.PropertyVirtualUnitTable.Rows[0]["PropertyVirtualUnitID"].ToString();
					exp = "DELETE FROM Property_Unit   where PropertyUnitID='" + str + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
					foreach (DataRow row in propertyVirtualUnitByID.PropertyVirtualUnitDetailTable.Rows)
					{
						str = row["PropertyUnitID"].ToString();
						string exp2 = "UPDATE Property_Unit  SET IsVirtual=null where PropertyUnitID='" + str + "'";
						flag &= (ExecuteNonQuery(exp2, sqlTransaction) >= 0);
					}
					if (flag)
					{
						base.DBConfig.EndTransaction(flag);
					}
				}
				exp = "DELETE FROM Property_VirtualUnit WHERE PropertyVirtualUnitID = '" + PropertyVirtualUnitID + "'";
				flag = Delete(exp, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("PropertyVirtualUnit", PropertyVirtualUnitID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PropertyVirtualUnitData GetPropertyVirtualUnitByID(string id)
		{
			PropertyVirtualUnitData propertyVirtualUnitData = new PropertyVirtualUnitData();
			string textCommand = "SELECT * from Property_VirtualUnit  WHERE PropertyVirtualUnitID = '" + id + "'";
			FillDataSet(propertyVirtualUnitData, "Property_VirtualUnit", textCommand);
			textCommand = "SELECT PDL.*,PU.PropertyUnitName from Property_VirtualUnit_Detail PDL  LEFT JOIN Property_Unit PU ON PDL.PropertyUnitID=PU.PropertyUnitID WHERE PropertyVirtualUnitID = '" + id + "'";
			FillDataSet(propertyVirtualUnitData, "Property_VirtualUnit_Detail", textCommand);
			return propertyVirtualUnitData;
		}

		public DataSet GetPropertyVirtualUnitByFields(params string[] columns)
		{
			return GetPropertyVirtualUnitByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyVirtualUnitByFields(string[] PropertyVirtualUnitID, params string[] columns)
		{
			return GetPropertyVirtualUnitByFields(PropertyVirtualUnitID, isInactive: true, columns);
		}

		public DataSet GetPropertyVirtualUnitByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_VirtualUnit");
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
				commandHelper.FieldName = "PropertyVirtualUnitID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Property_VirtualUnit";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Property_VirtualUnit", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPropertyVirtualUnitList(bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyVirtualUnitID [Code],PropertyVirtualUnitName [Name],Note,IsInactive [Inactive]\r\n                           FROM Property_VirtualUnit ";
			FillDataSet(dataSet, "Property_VirtualUnit", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyVirtualUnitComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyVirtualUnitID [Code],PropertyVirtualUnitName [Name]\r\n                           FROM Property_VirtualUnit ORDER BY PropertyVirtualUnitID,Name";
			FillDataSet(dataSet, "Property_VirtualUnit", textCommand);
			return dataSet;
		}
	}
}
