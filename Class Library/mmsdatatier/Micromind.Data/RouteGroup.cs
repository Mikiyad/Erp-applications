using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class RouteGroup : StoreObject
	{
		public const string ROUTEGROUP_TABLE = "Route_Group";

		public const string ROUTEGROUPID_PARM = "@RouteGroupID";

		public const string ROUTEGROUPNAME_PARM = "@RouteGroupName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string DATECREATED_PARM = "@DateCreated";

		public const string DATEUPDATED_PARM = "@DateUpdated";

		public const string CREATEDBY_PARM = "@CreatedBy";

		public const string UPDATEDBY_PARM = "@UpdatedBy";

		public const string ROUTEGROUPDETAIL_TABLE = "Route_Group_Detail";

		public const string STEP_PARM = "@Step";

		public const string ROUTEID_PARM = "@RouteID";

		public const string DESCRIPTION_PARM = "@Description";

		public const string PREVIOUSSTEP_PARM = "@PreviousStep";

		public const string REMARKS_PARM = "@Remarks";

		public RouteGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Route_Group", new FieldValue("RouteGroupID", "@RouteGroupID", isUpdateConditionField: true), new FieldValue("RouteGroupName", "@RouteGroupName"), new FieldValue("Note", "@Note"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Route_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@RouteGroupID", SqlDbType.NVarChar);
			parameters.Add("@RouteGroupName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@RouteGroupID"].SourceColumn = "RouteGroupID";
			parameters["@RouteGroupName"].SourceColumn = "RouteGroupName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Route_Group_Detail", new FieldValue("RouteGroupID", "@RouteGroupID"), new FieldValue("Step", "@Step"), new FieldValue("RouteID", "@RouteID"), new FieldValue("Description", "@Description"), new FieldValue("PreviousStep", "@PreviousStep"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Route_Group_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@RouteGroupID", SqlDbType.NVarChar);
			parameters.Add("@Step", SqlDbType.Int);
			parameters.Add("@RouteID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@PreviousStep", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@RouteGroupID"].SourceColumn = "RouteGroupID";
			parameters["@Step"].SourceColumn = "Step";
			parameters["@RouteID"].SourceColumn = "RouteID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@PreviousStep"].SourceColumn = "PreviousStep";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertRouteGroup(RouteGroupData routeGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(routeGroupData, "Route_Group", insertUpdateCommand);
				insertUpdateCommand = GetInsertUpdateDetailsCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag &= Insert(routeGroupData, "Route_Group_Detail", insertUpdateCommand);
				string text = routeGroupData.RouteGroupTable.Rows[0]["RouteGroupID"].ToString();
				AddActivityLog("Route Group", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Route_Group", "RouteGroupID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateRouteGroup(RouteGroupData routeGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(routeGroupData, "Route_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = routeGroupData.RouteGroupTable.Rows[0]["RouteGroupID"];
				insertUpdateCommand = GetInsertUpdateDetailsCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				string commandText = "DELETE FROM Route_Group_Detail WHERE RouteGroupID = '" + obj + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= Insert(routeGroupData, "Route_Group_Detail", insertUpdateCommand);
				UpdateTableRowByID("Route_Group", "RouteGroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = routeGroupData.RouteGroupTable.Rows[0]["RouteGroupName"].ToString();
				AddActivityLog("Route Group", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Task_Type", "TaskTypeID", obj, sqlTransaction, isInsert: false);
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

		public bool DeleteRouteGroup(string routeGroupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Route_Group WHERE RouteGroupID = '" + routeGroupID + "'";
				flag = Delete(commandText, null);
				if (flag)
				{
					commandText = "DELETE FROM Route_Group_Detail WHERE RouteGroupID = '" + routeGroupID + "'";
					flag = Delete(commandText, null);
				}
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Route Group", routeGroupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public RouteGroupData GetRouteGroupByID(string id)
		{
			RouteGroupData routeGroupData = new RouteGroupData();
			string textCommand = "SELECT * from Route_Group  WHERE RouteGroupID = '" + id + "'";
			FillDataSet(routeGroupData, "Route_Group", textCommand);
			textCommand = "SELECT * from Route_Group_Detail  WHERE RouteGroupID = '" + id + "'";
			FillDataSet(routeGroupData, "Route_Group_Detail", textCommand);
			return routeGroupData;
		}

		public DataSet GetRouteGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT RouteGroupID [Code],RouteGroupName [Name]\r\n                           FROM Route_Group ORDER BY RouteGroupID,RouteGroupName ";
			FillDataSet(dataSet, "Route_Group", textCommand);
			return dataSet;
		}

		public DataSet GetRouteGroupList(bool isInactive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select RouteGroupID [Code],RouteGroupName Name,Note  FROM Route_Group WHERE ISNULL(Inactive,'False')='False' ";
			FillDataSet(dataSet, "Route_Group", textCommand);
			return dataSet;
		}
	}
}
