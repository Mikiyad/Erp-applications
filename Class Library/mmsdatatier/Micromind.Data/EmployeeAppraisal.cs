using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeAppraisal : StoreObject
	{
		private const string EMPLOYEEAPPRAISAL_TABLE = "Employee_Appraisal";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string NOTE_PARM = "@Note";

		private const string POSITIONID_PARM = "@PositionID";

		private const string KPIPARAMETER_PARM = "@KPIParameter";

		private const string WEIGHTAGE_PARM = "@Weightage";

		private const string POINTS_PARM = "@Points";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string REMARKS_PARM = "@Remarks";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ACTUAL_PARM = "@Actual";

		private const string ACH_PARM = "@ACH";

		private const string RATING_PARM = "@Rating";

		private const string SCALE_PARM = "@Scale";

		private const string TARGET_PARM = "@Target";

		private const string EMPLOYEEAPPRAISALDETAIL_TABLE = "Employee_Appraisal_Detail";

		public EmployeeAppraisal(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Appraisal", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("PositionID", "@PositionID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Appraisal", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@PositionID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@PositionID"].SourceColumn = "PositionID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
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

		internal bool DeletePositionDetailsRows(string voucherID, string SysDocID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Appraisal_Detail WHERE VoucherID = '" + voucherID + "' AND SysDocID = '" + SysDocID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertPosition(EmployeeAppraisalData accountPositionData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow = accountPositionData.EmployeeAppraisalTable.Rows[0];
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				foreach (DataRow row in accountPositionData.EmployeeAppraisalDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (new SystemDocuments(base.DBConfig).ExistDocumentNumber("Employee_Appraisal", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Insert(accountPositionData, "Employee_Appraisal", insertUpdateCommand);
				if (accountPositionData.Tables["Employee_Appraisal_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdatePositionDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPositionData, "Employee_Appraisal_Detail", insertUpdateCommand);
				}
				decimal num = default(decimal);
				foreach (DataRow row2 in accountPositionData.EmployeeAppraisalDetailTable.Rows)
				{
					num += decimal.Parse(row2["Points"].ToString());
				}
				string exp = "UPDATE Employee SET AppriasalPoints = " + num + " WHERE EmployeeID='" + dataRow["EmployeeID"].ToString() + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				string text2 = accountPositionData.EmployeeAppraisalTable.Rows[0]["VoucherID"].ToString();
				AddActivityLog("EmployeeAppraisal", text2, sysDocID, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Appraisal", "VoucherID", text2, sqlTransaction, isInsert: true);
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Employee_Appraisal", "VoucherID", sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.EmployeeAppraisal, sysDocID, text, "Employee_Appraisal", sqlTransaction);
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

		public bool UpdatePosition(EmployeeAppraisalData accountPositionData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow = accountPositionData.EmployeeAppraisalTable.Rows[0];
				string voucherID = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				foreach (DataRow row in accountPositionData.EmployeeAppraisalDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
					row["SysDocID"] = dataRow["SysDocID"];
				}
				insertUpdateCommand.Transaction = sqlTransaction;
				flag &= DeletePositionDetailsRows(voucherID, sysDocID, sqlTransaction);
				flag = Update(accountPositionData, "Employee_Appraisal", insertUpdateCommand);
				if (accountPositionData.Tables["Employee_Appraisal_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdatePositionDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPositionData, "Employee_Appraisal_Detail", insertUpdateCommand);
				}
				decimal num = default(decimal);
				foreach (DataRow row2 in accountPositionData.EmployeeAppraisalDetailTable.Rows)
				{
					num += decimal.Parse(row2["Points"].ToString());
				}
				string exp = "UPDATE Employee SET AppriasalPoints = " + num + " WHERE EmployeeID='" + dataRow["EmployeeID"].ToString() + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (flag)
				{
					object obj2 = accountPositionData.EmployeeAppraisalTable.Rows[0]["VoucherID"];
					UpdateTableRowByID("Employee_Appraisal", "VoucherID", "DateUpdated", obj2, DateTime.Now, sqlTransaction);
					string entiyID = accountPositionData.EmployeeAppraisalTable.Rows[0]["VoucherID"].ToString();
					AddActivityLog("EmployeeAppraisal", entiyID, sysDocID, ActivityTypes.Update, sqlTransaction);
					UpdateTableRowInsertUpdateInfo("Employee_Appraisal", "VoucherID", obj2, sqlTransaction, isInsert: false);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.OverTimeEntry, sysDocID, voucherID, "Employee_Appraisal", sqlTransaction);
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

		public EmployeeAppraisalData GetPosition()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Appraisal");
			EmployeeAppraisalData employeeAppraisalData = new EmployeeAppraisalData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeAppraisalData, "Employee_Appraisal", sqlBuilder);
			return employeeAppraisalData;
		}

		public bool DeletePosition(string voucherID, string SysDocID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeletePositionDetailsRows(voucherID, SysDocID, sqlTransaction);
				string exp = "DELETE FROM Employee_Appraisal WHERE VoucherID = '" + voucherID + "' AND SysDocID = '" + SysDocID + "'";
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

		public EmployeeAppraisalData GetPositionByID(string sysDocID, string id)
		{
			EmployeeAppraisalData employeeAppraisalData = new EmployeeAppraisalData();
			string textCommand = "SELECT * FROM Employee_Appraisal WHERE VoucherID='" + id + "' AND SysDocID='" + sysDocID + "'";
			FillDataSet(employeeAppraisalData, "Employee_Appraisal", textCommand);
			if (employeeAppraisalData == null || employeeAppraisalData.Tables.Count == 0 || employeeAppraisalData.Tables["Employee_Appraisal"].Rows.Count == 0)
			{
				return null;
			}
			textCommand = "SELECT EAD.*,P.Scale,P.Target  FROM Employee_Appraisal_Detail EAD INNER JOIN Employee_Appraisal EA ON EA.VoucherID=EAD.VoucherID\r\n                    INNER JOIN Employee E ON E.EmployeeID=EA.EmployeeID  INNER JOIN Position_Details P ON EA.PositionID =P.PositionID AND EAD.RowIndex=P.RowIndex  WHERE EAD.VoucherID='" + id + "' AND EAD.SysDocID='" + sysDocID + "'";
			FillDataSet(employeeAppraisalData, "Employee_Appraisal_Detail", textCommand);
			return employeeAppraisalData;
		}

		public DataSet GetPositionByFields(params string[] columns)
		{
			return GetPositionByFields(null, isInactive: true, columns);
		}

		public DataSet GetPositionByFields(string[] positionID, params string[] columns)
		{
			return GetPositionByFields(positionID, isInactive: true, columns);
		}

		public DataSet GetPositionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Appraisal");
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
				commandHelper.TableName = "Employee_Appraisal";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Appraisal", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPositionList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PositionID [EmployeeAppraisal Code],PositionName [EmployeeAppraisal Name],Note,Inactive\r\n                           FROM Employee_Appraisal ";
			FillDataSet(dataSet, "Employee_Appraisal", textCommand);
			return dataSet;
		}

		public DataSet GetPositionComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PositionID [Code],PositionName [Name]\r\n                           FROM EmployeeAppraisal ORDER BY PositionID,PositionName";
			FillDataSet(dataSet, "Employee_Appraisal", textCommand);
			return dataSet;
		}

		private SqlCommand GetInsertUpdatePositionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePositionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePositionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@KPIParameter", SqlDbType.NText);
			parameters.Add("@Weightage", SqlDbType.Decimal);
			parameters.Add("@Points", SqlDbType.Decimal);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Actual", SqlDbType.Decimal);
			parameters.Add("@ACH", SqlDbType.Decimal);
			parameters.Add("@Rating", SqlDbType.Decimal);
			parameters.Add("@Scale", SqlDbType.NVarChar);
			parameters.Add("@Target", SqlDbType.Decimal);
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@KPIParameter"].SourceColumn = "KPIParameter";
			parameters["@Weightage"].SourceColumn = "Weightage";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Points"].SourceColumn = "Points";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Actual"].SourceColumn = "Actual";
			parameters["@ACH"].SourceColumn = "ACH";
			parameters["@Rating"].SourceColumn = "Rating";
			parameters["@Scale"].SourceColumn = "Scale";
			parameters["@Target"].SourceColumn = "Target";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePositionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Appraisal_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("Weightage", "@Weightage"), new FieldValue("KPIParameter", "@KPIParameter"), new FieldValue("Points", "@Points"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Actual", "@Actual"), new FieldValue("ACH", "@ACH"), new FieldValue("Rating", "@Rating"), new FieldValue("Scale", "@Scale"), new FieldValue("Target", "@Target"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		public DataSet GetEmployeeKPIList(string EmployeeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT pd.* FROM Position p INNER JOIN Position_Details pd ON p.PositionID=pd.PositionID  LEFT JOIN  Employee e ON p.PositionID=e.PositionID WHERE e.EmployeeID='" + EmployeeID + "'";
			FillDataSet(dataSet, "Position_Details", textCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT VoucherID [Doc Number],TransactionDate,EmployeeID,PositionID,Note from Employee_Appraisal ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee_Appraisal", sqlCommand);
			return dataSet;
		}

		public DataSet GetEmployeeAppraisalToPrint(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT * FROM Employee_Appraisal WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Employee_Appraisal", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Employee_Appraisal"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT EAD.*,P.Scale,P.Target  FROM Employee_Appraisal_Detail EAD INNER JOIN Employee_Appraisal EA ON EA.VoucherID=EAD.VoucherID\r\n                    INNER JOIN Employee E ON E.EmployeeID=EA.EmployeeID  INNER JOIN Position_Details P ON EA.PositionID =P.PositionID AND EAD.RowIndex=P.RowIndex  WHERE EAD.VoucherID='" + voucherID + "' AND EAD.SysDocID='" + sysDocID + "' ORDER BY EAD.RowIndex";
				FillDataSet(dataSet, "Employee_Appraisal_Detail", cmdText);
				dataSet.Relations.Add("EmployeeAppraisal", new DataColumn[2]
				{
					dataSet.Tables["Employee_Appraisal"].Columns["SysDocID"],
					dataSet.Tables["Employee_Appraisal"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Employee_Appraisal_Detail"].Columns["SysDocID"],
					dataSet.Tables["Employee_Appraisal_Detail"].Columns["VoucherID"]
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
