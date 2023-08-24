using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class POSShift : StoreObject
	{
		private const string SHIFTID_PARM = "@ShiftID";

		private const string BATCHID_PARM = "@BatchID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string USERID_PARM = "@UserID";

		private const string OPENDATE_PARM = "@OpenDate";

		private const string CLOSEDATE_PARM = "@CloseDate";

		private const string CASHREGISTERID_PARM = "@CashRegisterID";

		private const string OPENINGCASH_PARM = "@OpeningCash";

		private const string CLOSINGCASH_PARM = "@ClosingCash";

		private const string STATUS_PARM = "@Status";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public POSShift(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("POS_Shift", new FieldValue("ShiftID", "@ShiftID", isUpdateConditionField: true), new FieldValue("BatchID", "@BatchID", isUpdateConditionField: true), new FieldValue("LocationID", "@LocationID"), new FieldValue("UserID", "@UserID"), new FieldValue("OpenDate", "@OpenDate"), new FieldValue("CloseDate", "@CloseDate"), new FieldValue("OpeningCash", "@OpeningCash"), new FieldValue("ClosingCash", "@ClosingCash"), new FieldValue("CashRegisterID", "@CashRegisterID"), new FieldValue("Status", "@Status"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("POS_Shift", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ShiftID", SqlDbType.Int);
			parameters.Add("@BatchID", SqlDbType.Int);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@OpenDate", SqlDbType.DateTime);
			parameters.Add("@OpeningCash", SqlDbType.Decimal);
			parameters.Add("@ClosingCash", SqlDbType.Decimal);
			parameters.Add("@CloseDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.NVarChar);
			parameters.Add("@CashRegisterID", SqlDbType.NVarChar);
			parameters["@ShiftID"].SourceColumn = "ShiftID";
			parameters["@BatchID"].SourceColumn = "BatchID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@OpenDate"].SourceColumn = "OpenDate";
			parameters["@OpeningCash"].SourceColumn = "OpeningCash";
			parameters["@ClosingCash"].SourceColumn = "ClosingCash";
			parameters["@CloseDate"].SourceColumn = "CloseDate";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CashRegisterID"].SourceColumn = "CashRegisterID";
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

		public bool CreatePOSShift(POSShiftData posShiftData)
		{
			return InsertPOSShift(posShiftData);
		}

		public bool InsertPOSShift(POSShiftData accountPOSShiftData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = accountPOSShiftData.Tables[0].Rows[0];
				dataRow["Status"] = "O";
				dataRow["OpenDate"] = DateTime.Now;
				dataRow.EndEdit();
				if (GetCurrentOpenShiftID(dataRow["LocationID"].ToString()) != -1)
				{
					throw new CompanyException("There is already an open shift for this user.");
				}
				string locationID = dataRow["LocationID"].ToString();
				POSBatch pOSBatch = new POSBatch(base.DBConfig);
				int currentOpenBatchID = pOSBatch.GetCurrentOpenBatchID(locationID);
				if (currentOpenBatchID <= 0)
				{
					flag &= pOSBatch.CreatePOSBatch(locationID);
					if (flag)
					{
						currentOpenBatchID = pOSBatch.GetCurrentOpenBatchID(locationID);
					}
				}
				if (currentOpenBatchID <= 0)
				{
					throw new CompanyException("There is no open batch available and creating new batch failed. Please try again.", 2005);
				}
				dataRow["BatchID"] = currentOpenBatchID;
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT TOP 1 ShiftID + 1 FROM POS_Shift WHERE BatchID = " + currentOpenBatchID + " ORDER BY DateCreated DESC";
				object obj = ExecuteScalar(exp, sqlTransaction);
				int num = 1;
				if (obj != null && obj.ToString() != "")
				{
					num = int.Parse(obj.ToString());
				}
				dataRow["ShiftID"] = num;
				insertUpdateCommand.Transaction = sqlTransaction;
				flag &= Insert(accountPOSShiftData, "POS_Shift", insertUpdateCommand);
				string text = accountPOSShiftData.POSShiftTable.Rows[0]["ShiftID"].ToString();
				AddActivityLog("POS Shift", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("POS_Shift", "ShiftID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePOSShift(POSShiftData accountPOSShiftData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPOSShiftData, "POS_Shift", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPOSShiftData.POSShiftTable.Rows[0]["ShiftID"];
				UpdateTableRowByID("POS_Shift", "ShiftID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				AddActivityLog("POS Shift", obj.ToString(), ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("POS_Shift", "ShiftID", obj, sqlTransaction, isInsert: false);
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

		public POSShiftData GetPOSShift()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("POS_Shift");
			POSShiftData pOSShiftData = new POSShiftData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(pOSShiftData, "POS_Shift", sqlBuilder);
			return pOSShiftData;
		}

		public bool DeletePOSShift(string posShiftID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM POS_Shift WHERE ShiftID = '" + posShiftID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("POS Shift", posShiftID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public POSShiftData GetPOSShiftByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ShiftID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "POS_Shift";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			POSShiftData pOSShiftData = new POSShiftData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(pOSShiftData, "POS_Shift", sqlBuilder);
			return pOSShiftData;
		}

		public DataSet GetPOSShiftByFields(params string[] columns)
		{
			return GetPOSShiftByFields(null, isInactive: true, columns);
		}

		public DataSet GetPOSShiftByFields(string[] posShiftID, params string[] columns)
		{
			return GetPOSShiftByFields(posShiftID, isInactive: true, columns);
		}

		public DataSet GetPOSShiftByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("POS_Shift");
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
				commandHelper.FieldName = "ShiftID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "POS_Shift";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "POS_Shift", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPOSShiftList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT POSShiftID [POSShift Code],POSShiftName [POSShift Name]\r\n                           FROM POSShift ";
			FillDataSet(dataSet, "POS_Shift", textCommand);
			return dataSet;
		}

		public DataSet GetPOSShiftComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT POSShiftID [Code],POSShiftName [Name]\r\n                           FROM POSShift ORDER BY POSShiftID,POSShiftName";
			FillDataSet(dataSet, "POS_Shift", textCommand);
			return dataSet;
		}

		public int GetCurrentOpenShiftID(string registerID)
		{
			new DataSet();
			string exp = "SELECT ShiftID  \r\n                           FROM POS_Shift WHERE Status='O' AND CashRegisterID='" + registerID + "'";
			object obj = ExecuteScalar(exp);
			if (obj == null)
			{
				return -1;
			}
			return int.Parse(obj.ToString());
		}

		public bool ClosePOSShift(int batchID, int shiftID, string registerID, decimal closingCash)
		{
			bool result = true;
			GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(DateTime.Now);
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT Status FROM POS_Shift WHERE\r\n                                ShiftID = " + shiftID.ToString() + " AND BatchID = " + batchID.ToString() + " AND CashRegisterID = '" + registerID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && obj.ToString() != "" && obj.ToString() == "C")
				{
					throw new CompanyException("This shift is already closed.");
				}
				exp = "UPDATE POS_Shift SET CloseDate = '" + text + "', Status = 'C',ClosingCash = " + closingCash + " WHERE\r\n                                ShiftID = " + shiftID.ToString() + " AND BatchID = " + batchID.ToString() + " AND CashRegisterID = '" + registerID + "'";
				result = (ExecuteNonQuery(exp, sqlTransaction) > 0);
				AddActivityLog("POS Shift", shiftID.ToString(), ActivityTypes.Close, sqlTransaction);
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
