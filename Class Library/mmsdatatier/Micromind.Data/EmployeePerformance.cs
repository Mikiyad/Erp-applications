using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeePerformance : StoreObject
	{
		private const string EMPLOYEEPERFORMANCE_TABLE = "Employee_Performance";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string FROMMONTH_PARM = "@FromMonth";

		private const string TOMONTH_PARM = "@ToMonth";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string NOTE_PARM = "@Note";

		private const string POSITIONID_PARM = "@PerformanceID";

		private const string PERFORMANCEPARAMETER_PARM = "@PerformanceParameter";

		private const string SCORE_PARM = "@Score";

		private const string REMARKS_PARM = "@Remarks";

		private const string PLUSSCORE_PARM = "@PlusScore";

		private const string MINUSSCORE_PARM = "@MinusScore";

		public const string CREATEDBY_PARM = "CreatedBy";

		public const string DATECREATED_PARM = "DateCreated";

		public const string UPDATEDBY_PARM = "UpdatedBy";

		public const string DATEUPDATED_PARM = "DateUpdated";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string EMPLOYEEPERFORMANCEDETAIL_TABLE = "Employee_Performance_Detail";

		public EmployeePerformance(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Performance", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("FromMonth", "@FromMonth"), new FieldValue("ToMonth", "@ToMonth"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("PositionID", "@PerformanceID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Performance", new FieldValue("DateUpdated", "DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@FromMonth", SqlDbType.DateTime);
			parameters.Add("@ToMonth", SqlDbType.DateTime);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@PerformanceID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@PerformanceID"].SourceColumn = "PositionID";
			parameters["@FromMonth"].SourceColumn = "FromMonth";
			parameters["@ToMonth"].SourceColumn = "ToMonth";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
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

		internal bool DeletePerformanceDetailsRows(string voucherID, string SysDocID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Performance_Detail WHERE VoucherID = '" + voucherID + "' AND SysDocID = '" + SysDocID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertPerformance(EmployeePerformanceData accountPerformanceData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow = accountPerformanceData.EmployeePerformanceDetailTable.Rows[0];
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				foreach (DataRow row in accountPerformanceData.EmployeePerformanceDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (new SystemDocuments(base.DBConfig).ExistDocumentNumber("Employee_Performance", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Insert(accountPerformanceData, "Employee_Performance", insertUpdateCommand);
				if (accountPerformanceData.Tables["Employee_Performance_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdatePerformanceDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPerformanceData, "Employee_Performance_Detail", insertUpdateCommand);
				}
				string text2 = accountPerformanceData.EmployeePerformanceTable.Rows[0]["VoucherID"].ToString();
				AddActivityLog("EmployeeAppraisal", text2, sysDocID, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Performance", "VoucherID", text2, sqlTransaction, isInsert: true);
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Employee_Performance", "VoucherID", sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.EmployeeAppraisal, sysDocID, text, "Employee_Performance", sqlTransaction);
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

		public bool UpdatePerformance(EmployeePerformanceData accountPerformanceData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow = accountPerformanceData.EmployeePerformanceTable.Rows[0];
				string voucherID = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				foreach (DataRow row in accountPerformanceData.EmployeePerformanceDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
					row["SysDocID"] = dataRow["SysDocID"];
				}
				insertUpdateCommand.Transaction = sqlTransaction;
				flag &= DeletePerformanceDetailsRows(voucherID, sysDocID, sqlTransaction);
				flag = Update(accountPerformanceData, "Employee_Performance", insertUpdateCommand);
				if (accountPerformanceData.Tables["Employee_Performance_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdatePerformanceDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPerformanceData, "Employee_Performance_Detail", insertUpdateCommand);
				}
				if (flag)
				{
					object obj2 = accountPerformanceData.EmployeePerformanceTable.Rows[0]["VoucherID"];
					UpdateTableRowByID("Employee_Performance", "VoucherID", "DateUpdated", obj2, DateTime.Now, sqlTransaction);
					string entiyID = accountPerformanceData.EmployeePerformanceTable.Rows[0]["VoucherID"].ToString();
					AddActivityLog("EmployeePerformance", entiyID, sysDocID, ActivityTypes.Update, sqlTransaction);
					UpdateTableRowInsertUpdateInfo("Employee_Performance", "VoucherID", obj2, sqlTransaction, isInsert: false);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.OverTimeEntry, sysDocID, voucherID, "Employee_Performance", sqlTransaction);
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

		public EmployeePerformanceData GetPerformance()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Performance");
			EmployeePerformanceData employeePerformanceData = new EmployeePerformanceData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeePerformanceData, "Employee_Performance", sqlBuilder);
			return employeePerformanceData;
		}

		public bool DeletePerformance(string voucherID, string SysDocID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeletePerformanceDetailsRows(voucherID, SysDocID, sqlTransaction);
				string exp = "DELETE FROM Employee_Performance WHERE VoucherID = '" + voucherID + "' AND SysDocID = '" + SysDocID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("EmployeeAppraisal", voucherID, ActivityTypes.Delete, sqlTransaction);
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

		public EmployeePerformanceData GetPerformanceByID(string sysDocID, string id)
		{
			EmployeePerformanceData employeePerformanceData = new EmployeePerformanceData();
			string textCommand = "SELECT * FROM Employee_Performance WHERE VoucherID='" + id + "' AND SysDocID='" + sysDocID + "'";
			FillDataSet(employeePerformanceData, "Employee_Performance", textCommand);
			if (employeePerformanceData == null || employeePerformanceData.Tables.Count == 0 || employeePerformanceData.Tables["Employee_Performance"].Rows.Count == 0)
			{
				return null;
			}
			textCommand = "SELECT EPD.*,P.Score FROM  Employee_Performance_Detail EPD INNER JOIN Employee_Performance EA ON EA.VoucherID=EPD.VoucherID\r\n                    INNER JOIN Employee E ON E.EmployeeID=EA.EmployeeID  INNER JOIN Performance_Details P ON EA.PositionID =P.PositionID AND EPD.RowIndex=P.RowIndex  WHERE EPD.VoucherID='" + id + "' AND EPD.SysDocID='" + sysDocID + "'";
			FillDataSet(employeePerformanceData, "Employee_Performance_Detail", textCommand);
			return employeePerformanceData;
		}

		public DataSet GetPerformanceByFields(params string[] columns)
		{
			return GetPerformanceByFields(null, isInactive: true, columns);
		}

		public DataSet GetPerformanceByFields(string[] PerformanceID, params string[] columns)
		{
			return GetPerformanceByFields(PerformanceID, isInactive: true, columns);
		}

		public DataSet GetPerformanceByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Performance");
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
				commandHelper.FieldName = "PositionID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Performance";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Performance", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPerformanceList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PerformanceID [EmployeeAppraisal Code],PerformanceName [EmployeeAppraisal Name],Note,Inactive\r\n                           FROM Employee_Appraisal ";
			FillDataSet(dataSet, "Employee_Performance", textCommand);
			return dataSet;
		}

		public DataSet GetPerformanceComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PerformanceID [Code],PerformanceName [Name]\r\n                           FROM EmployeeAppraisal ORDER BY PerformanceID,PerformanceName";
			FillDataSet(dataSet, "Employee_Performance", textCommand);
			return dataSet;
		}

		private SqlCommand GetInsertUpdatePerformanceDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePerformanceDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePerformanceDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@PerformanceParameter", SqlDbType.NText);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Score", SqlDbType.Decimal);
			parameters.Add("@PlusScore", SqlDbType.Decimal);
			parameters.Add("@MinusScore", SqlDbType.Decimal);
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@PerformanceParameter"].SourceColumn = "PerformanceParameter";
			parameters["@PlusScore"].SourceColumn = "PlusScore";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@MinusScore"].SourceColumn = "MinusScore";
			parameters["@Score"].SourceColumn = "Score";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePerformanceDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Performance_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("Score", "@Score"), new FieldValue("PerformanceParameter", "@PerformanceParameter"), new FieldValue("PlusScore", "@PlusScore"), new FieldValue("Remarks", "@Remarks"), new FieldValue("MinusScore", "@MinusScore"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		public DataSet GetEmployeePerfromanceList(string EmployeeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT pd.* FROM Position p INNER JOIN Performance_Details pd ON p.PositionID=pd.PositionID  LEFT JOIN  Employee e ON p.PositionID=e.PositionID WHERE e.EmployeeID='" + EmployeeID + "'";
			FillDataSet(dataSet, "Performance_Details", textCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT VoucherID [Doc Number], SysDocID, FromMonth, ToMonth , EmployeeID,PositionID,Note from Employee_Performance ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee_Performance", sqlCommand);
			return dataSet;
		}

		public DataSet GetEmployeePerformanceToPrint(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT * FROM Employee_Performance WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Employee_Performance", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Employee_Performance"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT EPD.*,P.Score FROM  Employee_Performance_Detail EPD INNER JOIN Employee_Performance EA ON EA.VoucherID=EPD.VoucherID\r\n                    INNER JOIN Employee E ON E.EmployeeID=EA.EmployeeID  INNER JOIN Performance_Details P ON EA.PositionID =P.PositionID AND EPD.RowIndex=P.RowIndex  WHERE EPD.VoucherID='" + voucherID + "' AND EPD.SysDocID='" + sysDocID + "' ORDER BY EPD.RowIndex";
				FillDataSet(dataSet, "Employee_Performance_Detail", cmdText);
				dataSet.Relations.Add("EmployeePerformance", new DataColumn[2]
				{
					dataSet.Tables["Employee_Performance"].Columns["SysDocID"],
					dataSet.Tables["Employee_Performance"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Employee_Performance_Detail"].Columns["SysDocID"],
					dataSet.Tables["Employee_Performance_Detail"].Columns["VoucherID"]
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
