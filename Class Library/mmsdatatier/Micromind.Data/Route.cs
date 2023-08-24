using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Route : StoreObject
	{
		private const string ROUTE_TABLE = "Route";

		private const string ROUTEID_PARM = "@RouteID";

		private const string ISINACTIVE_PARM = "@Inactive";

		private const string ROUTENAME_PARM = "@RouteName";

		private const string REMARKS_PARM = "@Remarks";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string BOMID_PARM = "@BomID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Route(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Route", new FieldValue("RouteID", "@RouteID", isUpdateConditionField: true), new FieldValue("RouteName", "@RouteName"), new FieldValue("LocationID", "@LocationID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Route", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@RouteID", SqlDbType.NVarChar);
			parameters.Add("@RouteName", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@RouteID"].SourceColumn = "RouteID";
			parameters["@RouteName"].SourceColumn = "RouteName";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertRoute(RouteData routeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(routeData, "Route", insertUpdateCommand);
				string text = routeData.RouteTable.Rows[0]["RouteID"].ToString();
				AddActivityLog("Route", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Route", "RouteID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateRoute(RouteData routeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(routeData, "Route", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = routeData.RouteTable.Rows[0]["RouteID"];
				UpdateTableRowByID("Route", "RouteID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = routeData.RouteTable.Rows[0]["RouteName"].ToString();
				AddActivityLog("Route", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Route", "RouteID", obj, sqlTransaction, isInsert: false);
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

		public RouteData GetRoute()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Route");
			RouteData routeData = new RouteData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(routeData, "Route", sqlBuilder);
			return routeData;
		}

		public bool DeleteRoute(string routeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Route WHERE RouteID = '" + routeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Route", routeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public RouteData GetRouteByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "RouteID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Route";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			RouteData routeData = new RouteData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(routeData, "Route", sqlBuilder);
			return routeData;
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			return GetJobTaskByFields(null, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] routeID, params string[] columns)
		{
			return GetJobTaskByFields(routeID, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Route");
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
				commandHelper.FieldName = "RouteID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Route";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Route", sqlBuilder);
			return dataSet;
		}

		public DataSet GetRouteList(bool isInactive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select RouteID [Route ID],RouteName  FROM Route WHERE ISNULL(Inactive,'False')='False' ";
			FillDataSet(dataSet, "Route", textCommand);
			return dataSet;
		}

		public DataSet GetRouteComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT RouteID [Code],RouteName [Name]\r\n                           FROM Route ORDER BY RouteID,RouteName";
			FillDataSet(dataSet, "Route", textCommand);
			return dataSet;
		}
	}
}
