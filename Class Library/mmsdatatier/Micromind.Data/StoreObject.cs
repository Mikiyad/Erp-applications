using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Micromind.Data
{
	public class StoreObject : IDisposable
	{
		protected SqlDataAdapter dsCommand;

		protected SqlCommand loadCommand;

		protected SqlCommand insertCommand;

		protected SqlCommand updateCommand;

		protected SqlCommand deleteCommand;

		protected SqlCommand selectCommand;

		private Config config;

		protected Config DBConfig
		{
			get
			{
				return config;
			}
			set
			{
				config = value;
			}
		}

		protected string CryptorID => "D38E3CAB-5C9B-4808-AEF7-8DCE167061B8";

		internal StoreObject(Config config)
		{
			dsCommand = new SqlDataAdapter();
			dsCommand.SelectCommand = new SqlCommand();
			this.config = config;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (dsCommand != null)
			{
				if (dsCommand.SelectCommand != null)
				{
					if (dsCommand.SelectCommand.Connection != null)
					{
						dsCommand.SelectCommand.Connection.Dispose();
					}
					dsCommand.SelectCommand.Dispose();
				}
				dsCommand.Dispose();
				dsCommand = null;
			}
			if (loadCommand != null)
			{
				loadCommand.Dispose();
				loadCommand = null;
			}
			if (selectCommand != null)
			{
				selectCommand.Dispose();
				selectCommand = null;
			}
			if (deleteCommand != null)
			{
				deleteCommand.Dispose();
				deleteCommand = null;
			}
			if (updateCommand != null)
			{
				updateCommand.Dispose();
				updateCommand = null;
			}
			if (insertCommand != null)
			{
				insertCommand.Dispose();
				insertCommand = null;
			}
		}

		protected void OnRowUpdated(object sender, SqlRowUpdatedEventArgs e)
		{
		}

		protected bool Delete(DataSet dataSet, string srcTable, SqlCommand deleteCommand)
		{
			bool result = true;
			if (dsCommand == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				dsCommand.DeleteCommand = deleteCommand;
				dsCommand.Update(dataSet, srcTable);
				if (dataSet.HasErrors)
				{
					return false;
				}
				dataSet.Tables[srcTable].AcceptChanges();
				return result;
			}
			catch (Exception ex)
			{
				result = false;
				throw ex;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool Update(DataSet dataSet, string srcTable, SqlCommand updateCommand)
		{
			bool flag = true;
			if (dsCommand == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				updateCommand.Transaction = updateCommand.Transaction;
				dsCommand.UpdateCommand = updateCommand;
				flag &= (dsCommand.Update(dataSet, srcTable) >= 0);
				if (dataSet.HasErrors)
				{
					return false;
				}
				dataSet.Tables[srcTable].AcceptChanges();
				return flag;
			}
			catch (Exception ex)
			{
				flag = false;
				throw ex;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool Update(DataSet dataSet, string srcTable, SqlCommand updateCommand, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			if (dsCommand == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				updateCommand.Transaction = sqlTransaction;
				dsCommand.UpdateCommand = updateCommand;
				flag &= (dsCommand.Update(dataSet, srcTable) >= 0);
				if (dataSet.HasErrors)
				{
					return false;
				}
				dataSet.Tables[srcTable].AcceptChanges();
				return flag;
			}
			catch (Exception ex)
			{
				flag = false;
				throw ex;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected object GetInsertedRowIdentity(string tableName, SqlCommand command)
		{
			string text2 = command.CommandText = "SELECT @@Identity from " + tableName;
			config.OpenConnection();
			command.Connection = config.Connection;
			return command.ExecuteScalar()?.ToString();
		}

		protected bool Insert(DataSet dataSet, string srcTable, SqlCommand insertCommand, bool continueUpdateOnError)
		{
			bool flag = true;
			if (dsCommand == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				dsCommand.ContinueUpdateOnError = continueUpdateOnError;
				dsCommand.InsertCommand = insertCommand;
				flag &= (dsCommand.Update(dataSet, srcTable) > 0);
				if (dataSet.HasErrors)
				{
					return false;
				}
				dataSet.Tables[srcTable].AcceptChanges();
				return flag;
			}
			catch (Exception ex)
			{
				flag = false;
				throw ex;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool Insert(DataSet dataSet, string srcTable, SqlCommand insertCommand)
		{
			bool flag = true;
			if (dsCommand == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				dsCommand.InsertCommand = insertCommand;
				dsCommand.InsertCommand.Transaction = insertCommand.Transaction;
				int num = dsCommand.Update(dataSet, srcTable);
				flag = (flag && num > 0);
				dataSet.Tables[0].GetChanges();
				
				if (dataSet.HasErrors)
				{
					return false;
				}
				flag=!dataSet.HasErrors;


				dataSet.Tables[srcTable].AcceptChanges();
				return flag;
			}
			catch (Exception ex)
			{
				flag = false;
				throw ex;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool Insert(DataSet dataSet, string srcTable, SqlCommand insertCommand, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			if (dsCommand == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				dsCommand.InsertCommand = insertCommand;
				dsCommand.InsertCommand.Transaction = sqlTransaction;
				flag &= (dsCommand.Update(dataSet, srcTable) > 0);
				if (dataSet.HasErrors)
				{
					return false;
				}
				dataSet.Tables[srcTable].AcceptChanges();
				return flag;
			}
			catch (Exception ex)
			{
				flag = false;
				throw ex;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected SqlDataReader GetDataReader(string[] srcTables, SqlBuilder sqlBuilder)
		{
			if (dsCommand == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				string selectExpression = sqlBuilder.GetSelectExpression();
				dsCommand.SelectCommand.CommandText = selectExpression;
				SqlCommand sqlCommand = dsCommand.SelectCommand;
				sqlCommand.CommandType = CommandType.Text;
				config.OpenConnection();
				sqlCommand.Connection = config.Connection;
				SqlDataReader result = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				config.CloseConnection();
				return result;
			}
			catch
			{
				config.CloseConnection();
				throw;
			}
		}

		protected SqlDataReader GetDataReader(string exp)
		{
			if (dsCommand == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				dsCommand.SelectCommand.CommandText = exp;
				SqlCommand sqlCommand = dsCommand.SelectCommand;
				sqlCommand.CommandType = CommandType.Text;
				config.OpenConnection();
				sqlCommand.Connection = config.Connection;
				SqlDataReader result = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				config.CloseConnection();
				return result;
			}
			catch
			{
				config.CloseConnection();
				throw;
			}
		}

		protected bool DeleteTableRowByID(string tableName, string colIDName, int[] ids)
		{
			return DeleteTableRowByID(tableName, colIDName, ids, null);
		}

		protected bool DeleteTableRowByID(string tableName, string colIDName, int[] ids, SqlTransaction sqlTransaction)
		{
			bool flag = false;
			if (ids.Length == 0)
			{
				return true;
			}
			try
			{
				string str = "DELETE FROM " + tableName + " WHERE " + colIDName + " IN(";
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < ids.Length; i++)
				{
					stringBuilder.Append(ids[i].ToString()).Append(",");
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				stringBuilder.Append(") ");
				str += stringBuilder.ToString();
				return flag & Delete(str, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		protected bool DeleteTableRowByID(string tableName, string colIDName, string val)
		{
			bool flag = false;
			try
			{
				val = AddSingleQuote(val);
				string commandText = "DELETE FROM " + tableName + " WHERE " + colIDName + " = '" + val + "'";
				return Delete(commandText, null);
			}
			catch
			{
				throw;
			}
		}

		protected bool DeleteTableRowByID(string tableName, string colIDName, string val, SqlTransaction sqlTransaction)
		{
			bool flag = false;
			try
			{
				val = AddSingleQuote(val);
				string commandText = "DELETE FROM " + tableName + " WHERE " + colIDName + " = '" + val + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		protected bool UpdateTableRowByID(string tableName, string colIDName, string colName, string id, string val)
		{
			bool flag = false;
			try
			{
				id = AddSingleQuote(id.ToString());
				string commandText = ("UPDATE " + tableName + " SET " + colName + "='" + val.ToString() + "' WHERE " + colIDName + "=" + id.ToString()) ?? "";
				flag = Update(commandText, null);
				UpdateTableDateStamps(tableName, val, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		protected bool UpdateTableRowByID(string tableName, string colIDName, string colName, object id, string val)
		{
			bool flag = false;
			try
			{
				id = AddSingleQuote(id.ToString());
				string commandText = ("UPDATE " + tableName + " SET " + colName + "='" + val.ToString() + "' WHERE " + colIDName + "=" + id.ToString()) ?? "";
				flag = Update(commandText, null);
				UpdateTableDateStamps(tableName, val, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		protected bool UpdateTableRowByID(string tableName, string colIDName, string colName, int[] ids, object val)
		{
			bool flag = false;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("UPDATE " + tableName + " SET " + colName + "=" + val.ToString() + " WHERE " + colIDName + " IN (");
				foreach (int num in ids)
				{
					stringBuilder.Append(num.ToString()).Append(",");
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				stringBuilder.Append(")");
				flag &= Update(stringBuilder.ToString(), DBConfig.SqlTransaction);
				UpdateTableDateStamps(tableName, val, DBConfig.SqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		protected bool UpdateTableRowByID(string tableName, string colIDName, string colName, object id, object val)
		{
			bool flag = false;
			try
			{
				id = AddSingleQuote(id.ToString());
				string commandText = "UPDATE " + tableName + " SET " + colName + "=" + val.ToString() + " WHERE " + colIDName + "=" + id.ToString();
				flag = Update(commandText, null);
				UpdateTableDateStamps(tableName, val, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		protected bool UpdateTableRowByID(string tableName, string colIDName, string colName, object id, DateTime val)
		{
			bool flag = false;
			try
			{
				id = AddSingleQuote(id.ToString());
				string commandText = "UPDATE " + tableName + " SET " + colName + "='" + val.ToString(StoreConfiguration.CurrentCulture) + "' WHERE " + colIDName + "=" + id.ToString();
				flag = Update(commandText, null);
				UpdateTableDateStamps(tableName, val, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		protected bool UpdateTableRowByID(string tableName, string colIDName, string colName, object id, DateTime val, SqlTransaction sqlTransaction)
		{
			bool flag = false;
			try
			{
				id = AddSingleQuote(id.ToString());
				string commandText = "UPDATE " + tableName + " SET " + colName + "='" + val.ToString(StoreConfiguration.CurrentCulture) + "' WHERE " + colIDName + "='" + id.ToString() + "'";
				flag = Update(commandText, sqlTransaction);
				UpdateTableDateStamps(tableName, val, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		protected bool UpdateTableRowByID(string tableName, string colIDName, string colName, object id, object val, SqlTransaction sqlTransaction)
		{
			bool flag = false;
			try
			{
				id = AddSingleQuote(id.ToString());
				string commandText = "UPDATE " + tableName + " SET " + colName + "=" + val.ToString() + " WHERE " + colIDName + "=" + id.ToString();
				flag = Update(commandText, sqlTransaction);
				UpdateTableDateStamps(tableName, val, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateTableDateStamps(string tableName, object date, SqlTransaction sqlTransaction)
		{
			return true;
		}

		internal bool UpdateTableRowInsertUpdateInfo(string tableName, string fieldIDName, object fieldIDValue, SqlTransaction sqlTransaction, bool isInsert)
		{
			bool flag = true;
			try
			{
				if (!(DBConfig.UserID != ""))
				{
					return flag;
				}
				string text = DateTime.Now.ToString(StoreConfiguration.CurrentCulture);
				string text2 = "";
				if (!isInsert)
				{
					text2 = "UPDATE " + tableName + " SET UpdatedBy= '" + DBConfig.UserID.ToString() + "',DateUpdated='" + text + "' WHERE " + fieldIDName + "='" + fieldIDValue.ToString() + "'";
					flag &= Update(text2, sqlTransaction);
					return flag;
				}
				text2 = "UPDATE " + tableName + " SET DateCreated='" + text + "',CreatedBy= '" + DBConfig.UserID.ToString() + "' WHERE " + fieldIDName + "='" + fieldIDValue.ToString() + "'";
				flag &= Update(text2, sqlTransaction);
				return flag;
			}
			catch
			{
				return flag;
			}
		}

		internal bool UpdateTableRowInsertUpdateInfo(string tableName, string sysDocIDName, string sysDocIDValue, string voucherIDName, object voucherIDValue, SqlTransaction sqlTransaction, bool isInsert)
		{
			bool flag = true;
			try
			{
				if (DBConfig.UserID.ToLower() == "axolonfixer")
				{
					return true;
				}
				if (!(DBConfig.UserID != ""))
				{
					return flag;
				}
				string text = DateTime.Now.ToString(StoreConfiguration.CurrentCulture);
				string text2 = "";
				if (!isInsert)
				{
					text2 = "UPDATE " + tableName + " SET UpdatedBy= '" + DBConfig.UserID + "', DateUpdated='" + text + "'  WHERE " + sysDocIDName + "='" + sysDocIDValue.ToString() + "' AND " + voucherIDName + " = '" + voucherIDValue + "'";
					flag &= Update(text2, sqlTransaction);
					return flag;
				}
				text2 = "UPDATE " + tableName + " SET DateCreated='" + text + "',CreatedBy= '" + DBConfig.UserID + "' WHERE " + sysDocIDName + "='" + sysDocIDValue.ToString() + "' AND " + voucherIDName + " = '" + voucherIDValue + "'";
				flag &= Update(text2, sqlTransaction);
				return flag;
			}
			catch
			{
				return flag;
			}
		}

		protected bool Update(string commandText, SqlTransaction trans)
		{
			bool result = true;
			SqlCommand sqlCommand = new SqlCommand(commandText, config.Connection);
			if (trans != null)
			{
				sqlCommand.Transaction = trans;
			}
			sqlCommand.CommandType = CommandType.Text;
			config.OpenConnection();
			try
			{
				if (sqlCommand.ExecuteNonQuery() < 0)
				{
					return false;
				}
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool Delete(string commandText, SqlTransaction trans)
		{
			bool result = true;
			SqlCommand sqlCommand = new SqlCommand(commandText, config.Connection);
			if (trans != null)
			{
				sqlCommand.Transaction = trans;
			}
			sqlCommand.CommandType = CommandType.Text;
			config.OpenConnection();
			try
			{
				sqlCommand.ExecuteNonQuery();
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool FillDataSet(DataSet dataSet, int maxRecords, string srcTable, SqlBuilder sqlBuilder, SqlTransaction sqlTransaction)
		{
			bool result = true;
			if (dsCommand == null || dataSet == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				maxRecords = ((maxRecords >= 0) ? maxRecords : 0);
				string selectExpression = sqlBuilder.GetSelectExpression();
				SqlCommand sqlCommand = dsCommand.SelectCommand;
				sqlCommand.CommandText = selectExpression;
				sqlCommand.CommandType = CommandType.Text;
				if (sqlTransaction != null)
				{
					sqlCommand.Transaction = sqlTransaction;
				}
				config.OpenConnection();
				sqlCommand.Connection = config.Connection;
				dsCommand.Fill(dataSet, 0, maxRecords, srcTable);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool FillDataSet(DataSet dataSet, string srcTable, SqlBuilder sqlBuilder)
		{
			bool result = true;
			if (dsCommand == null || dataSet == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				string selectExpression = sqlBuilder.GetSelectExpression();
				SqlCommand sqlCommand = dsCommand.SelectCommand;
				sqlCommand.CommandText = selectExpression;
				sqlCommand.CommandType = CommandType.Text;
				if (config.SqlTransaction != null)
				{
					sqlCommand.Transaction = config.SqlTransaction;
				}
				config.OpenConnection();
				sqlCommand.Connection = config.Connection;
				dsCommand.Fill(dataSet, srcTable);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool FillDataSet(DataSet dataSet, string srcTable, string textCommand)
		{
			bool result = true;
			if (dsCommand == null || dataSet == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				SqlCommand sqlCommand = dsCommand.SelectCommand;
				sqlCommand.CommandText = textCommand;
				sqlCommand.CommandType = CommandType.Text;
				if (config.SqlTransaction != null)
				{
					sqlCommand.Transaction = config.SqlTransaction;
				}
				config.OpenConnection();
				sqlCommand.Connection = config.Connection;
				dsCommand.Fill(dataSet, srcTable);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool FillDataSet(DataSet dataSet, string srcTable, SqlBuilder sqlBuilder, SqlTransaction sqlTransaction)
		{
			bool result = true;
			if (dsCommand == null || dataSet == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				string selectExpression = sqlBuilder.GetSelectExpression();
				SqlCommand sqlCommand = dsCommand.SelectCommand;
				sqlCommand.CommandText = selectExpression;
				sqlCommand.CommandType = CommandType.Text;
				if (sqlTransaction != null)
				{
					sqlCommand.Transaction = sqlTransaction;
				}
				config.OpenConnection();
				sqlCommand.Connection = config.Connection;
				dsCommand.Fill(dataSet, srcTable);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool FillDataSet(DataSet dataSet, string srcTable, string textCommand, SqlTransaction sqlTransaction)
		{
			bool result = true;
			if (dsCommand == null || dataSet == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				SqlCommand sqlCommand = dsCommand.SelectCommand;
				sqlCommand.CommandText = textCommand;
				sqlCommand.CommandType = CommandType.Text;
				if (sqlTransaction != null)
				{
					sqlCommand.Transaction = sqlTransaction;
				}
				config.OpenConnection();
				sqlCommand.Connection = config.Connection;
				dsCommand.Fill(dataSet, srcTable);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool FillDataSet(DataSet dataSet, string srcTable, SqlCommand sqlCommand)
		{
			bool result = true;
			if (dsCommand == null || dataSet == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				config.OpenConnection();
				dsCommand.SelectCommand = sqlCommand;
				dsCommand.SelectCommand.Connection = config.Connection;
				dsCommand.Fill(dataSet, srcTable);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool FillDataSet(DataSet dataSet, SqlCommand sqlCommand)
		{
			bool result = true;
			try
			{
				dsCommand.SelectCommand = sqlCommand;
				if (sqlCommand.Connection.State != ConnectionState.Open)
				{
					sqlCommand.Connection.Open();
				}
				dsCommand.SelectCommand.Connection = sqlCommand.Connection;
				dsCommand.Fill(dataSet);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected bool FillDataSet(DataSet dataSet, string srcTable, SqlCommand sqlCommand, bool mustOpenConnection)
		{
			bool result = true;
			if (dsCommand == null || dataSet == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				if (mustOpenConnection)
				{
					config.OpenConnection();
					dsCommand.SelectCommand.Connection = config.Connection;
				}
				dsCommand.SelectCommand = sqlCommand;
				dsCommand.Fill(dataSet, srcTable);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (mustOpenConnection)
				{
					config.CloseConnection();
				}
			}
		}

		protected bool FillDataSet(DataSet dataSet, string srcTable, SqlCommand sqlCommand, SqlConnection sqlConnection)
		{
			bool result = true;
			if (dsCommand == null || dataSet == null)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			try
			{
				dsCommand.SelectCommand = sqlCommand;
				dsCommand.SelectCommand.Connection = sqlConnection;
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				dsCommand.Fill(dataSet, srcTable);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				config.CloseConnection();
			}
		}

		protected string GetLastNumericField(string tableName, string primaryKeyName, string fieldName)
		{
			string exp = "Select " + fieldName + " FROM " + tableName + " WHERE " + primaryKeyName + " IN ( SELECT MAX( " + primaryKeyName + ") FROM " + tableName + " WHERE " + fieldName + " LIKE '%[0-9]%')";
			try
			{
				return (string)ExecuteScalar(exp);
			}
			catch
			{
				return "1";
			}
		}

		protected string GetLastNumericField(string tableName, string primaryKeyName, string numericFieldName, string supportingFieldName, object supportingFieldValue)
		{
			string exp = "Select " + numericFieldName + " FROM " + tableName + " WHERE " + primaryKeyName + " IN ( SELECT MAX( " + primaryKeyName + ") FROM " + tableName + " WHERE " + numericFieldName + " LIKE '%[0-9]%' AND " + supportingFieldName + "='" + supportingFieldValue.ToString() + "')";
			try
			{
				return (string)ExecuteScalar(exp);
			}
			catch
			{
				return "1";
			}
		}

		protected string GetAutoNumber(string tableName, string primaryKeyName, string fieldName)
		{
			string lastNumericField = GetLastNumericField(tableName, primaryKeyName, fieldName);
			if (lastNumericField == null || lastNumericField.Length == 0)
			{
				return "1";
			}
			return IncrementNumber(lastNumericField);
		}

		protected string GetAutoNumber(string tableName, string primaryKeyName, string numericFieldName, string supportingFieldName, object supportingFieldValue)
		{
			string lastNumericField = GetLastNumericField(tableName, primaryKeyName, numericFieldName, supportingFieldName, supportingFieldValue);
			if (lastNumericField == null || lastNumericField.Length == 0)
			{
				return "1";
			}
			return IncrementNumber(lastNumericField);
		}

		protected int GetFieldCount(string tableName, string fieldName, object val, SqlTransaction sqlTransaction)
		{
			int num = 0;
			object obj = null;
			val = AddSingleQuote(val.ToString());
			string exp = "SELECT COUNT(" + fieldName + ") FROM " + tableName + " WHERE " + fieldName + "  = '" + val.ToString() + "'";
			try
			{
				obj = ExecuteScalar(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
			if (obj == null)
			{
				return 0;
			}
			return int.Parse(obj.ToString());
		}

		protected bool IsTableFieldValueExist(string tableName, string fieldName, object val)
		{
			object obj = null;
			val = AddSingleQuote(val.ToString());
			string exp = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + "  = '" + val.ToString() + "'";
			try
			{
				obj = ExecuteScalar(exp);
			}
			catch
			{
				throw;
			}
			return obj != null;
		}

		protected bool IsTableFieldValueExist(string tableName, string fieldName, object val, SqlConnection sqlConnection)
		{
			object obj = null;
			val = AddSingleQuote(val.ToString());
			string exp = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + "  = '" + val.ToString() + "'";
			try
			{
				obj = ExecuteScalar(exp, sqlConnection);
			}
			catch
			{
				throw;
			}
			return obj != null;
		}

		protected bool IsTableFieldValueExist(string tableName, string fieldName, object val, SqlTransaction sqlTransaction)
		{
			object obj = null;
			val = AddSingleQuote(val.ToString());
			string exp = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + "  = '" + val.ToString() + "'";
			try
			{
				obj = ExecuteScalar(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
			return obj != null;
		}

		protected bool IsTableFieldValueExist(string tableName, string fieldName, string fieldName2, object val, object val2)
		{
			object obj = null;
			val = AddSingleQuote(val.ToString());
			string exp = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + "  = '" + val.ToString() + "' AND " + fieldName2 + "  = '" + val2.ToString() + "'";
			try
			{
				obj = ExecuteScalar(exp);
			}
			catch
			{
				throw;
			}
			return obj != null;
		}

		protected bool IsTableFieldValueExist(string tableName1, string tableName2, string fieldName, string fieldName2, string fieldName3, object val, object val2, object val3)
		{
			object obj = null;
			val = AddSingleQuote(val.ToString());
			string exp = "SELECT " + fieldName + " FROM " + tableName1 + " T1 INNER JOIN " + tableName2 + " T2 ON T1.VoucherID =  T2.VoucherID WHERE " + fieldName + "  = '" + val.ToString() + "' AND " + fieldName2 + "  = " + val2.ToString() + " AND " + fieldName3 + "  = " + val3.ToString();
			try
			{
				obj = ExecuteScalar(exp);
			}
			catch
			{
				throw;
			}
			return obj != null;
		}

		protected bool IsTableFieldValueExist(string tableName, string fieldName, string fieldName2, object val, object val2, SqlTransaction sqlTransaction)
		{
			object obj = null;
			val = AddSingleQuote(val.ToString());
			string exp = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + "  = '" + val.ToString() + "' AND " + fieldName2 + "  = '" + val2.ToString() + "'";
			try
			{
				obj = ExecuteScalar(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
			return obj != null;
		}

		protected bool IsTableFieldValueExist(string tableName, string fieldName, string fieldName2, int val, int val2, SqlTransaction sqlTransaction)
		{
			object obj = null;
			string exp = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + "  = " + val.ToString() + " AND " + fieldName2 + "  = " + val2.ToString();
			try
			{
				obj = ExecuteScalar(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
			return obj != null;
		}

		public object ExecuteSelectScalar(string tableName, string fieldIDName, object fieldIDValue, string field)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ").Append(field).Append(" FROM ");
			stringBuilder.Append(tableName).Append(" ").Append("WHERE ")
				.Append(fieldIDName);
			stringBuilder.Append("=").Append(fieldIDValue.ToString()).Append("");
			return ExecuteScalar(stringBuilder.ToString());
		}

		public object ExecuteSelectScalar(string tableName, string fieldIDName, string fieldIDValue, string field)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ").Append(field).Append(" FROM ");
			stringBuilder.Append(tableName).Append(" ").Append("WHERE ")
				.Append(fieldIDName);
			stringBuilder.Append("='").Append(fieldIDValue.ToString()).Append("'");
			return ExecuteScalar(stringBuilder.ToString());
		}

		public object ExecuteSelectScalar(string tableName, string fieldIDName, object fieldIDValue, string field, SqlTransaction sqlTransaction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ").Append(field).Append(" FROM ");
			stringBuilder.Append(tableName).Append(" ").Append("WHERE ")
				.Append(fieldIDName);
			stringBuilder.Append("=").Append(fieldIDValue.ToString()).Append("");
			return ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
		}

		public object ExecuteSelectScalar(string tableName, string fieldIDName, string fieldIDValue, string field, SqlTransaction sqlTransaction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ").Append(field).Append(" FROM ");
			stringBuilder.Append(tableName).Append(" ").Append("WHERE ")
				.Append(fieldIDName);
			stringBuilder.Append("='").Append(fieldIDValue.ToString()).Append("'");
			return ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
		}

		public object ExecuteSelectScalar(string tableName, string fieldIDName, string fieldIDName2, string fieldIDValue, string fieldIDValue2, string field, SqlTransaction sqlTransaction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ").Append(field).Append(" FROM ");
			stringBuilder.Append(tableName).Append(" ").Append("WHERE ")
				.Append(fieldIDName);
			stringBuilder.Append("='").Append(fieldIDValue.ToString()).Append("' AND ");
			stringBuilder.Append(fieldIDName2).Append("='").Append(fieldIDValue2.ToString())
				.Append("'");
			return ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
		}

		public object ExecuteSelectScalar(string tableName, string fieldIDName, string fieldIDName2, string fieldIDValue, object fieldIDValue2, string field, SqlTransaction sqlTransaction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ").Append(field).Append(" FROM ");
			stringBuilder.Append(tableName).Append(" ").Append("WHERE ")
				.Append(fieldIDName);
			stringBuilder.Append("='").Append(fieldIDValue.ToString()).Append("' AND ");
			stringBuilder.Append(fieldIDName2).Append("=").Append(fieldIDValue2.ToString());
			return ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
		}

		public object ExecuteSelectScalar(string tableName, string fieldIDName, string fieldIDName2, object fieldIDValue, object fieldIDValue2, string field, SqlTransaction sqlTransaction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ").Append(field).Append(" FROM ");
			stringBuilder.Append(tableName).Append(" ").Append("WHERE ")
				.Append(fieldIDName);
			stringBuilder.Append("=").Append(fieldIDValue.ToString()).Append(" AND ");
			stringBuilder.Append(fieldIDName2).Append("=").Append(fieldIDValue2.ToString());
			return ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
		}

		protected object ExecuteScalar(string exp)
		{
			object obj = null;
			try
			{
				SqlCommand obj2 = new SqlCommand(exp, config.Connection)
				{
					CommandType = CommandType.Text
				};
				config.OpenConnection();
				obj2.Connection = config.Connection;
				obj2.Transaction = config.SqlTransaction;
				obj = obj2.ExecuteScalar();
				config.CloseConnection();
				return obj;
			}
			catch
			{
				config.CloseConnection();
				throw;
			}
		}

		protected object ExecuteScalar(string exp, SqlConnection connection)
		{
			object obj = null;
			try
			{
				SqlCommand obj2 = new SqlCommand(exp, connection)
				{
					CommandType = CommandType.Text
				};
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}
				obj2.Connection = connection;
				return obj2.ExecuteScalar();
			}
			catch
			{
				throw;
			}
		}

		protected object ExecuteScalar(string exp, SqlTransaction sqlTransaction)
		{
			object obj = null;
			try
			{
				SqlCommand sqlCommand = new SqlCommand(exp, config.Connection);
				sqlCommand.CommandType = CommandType.Text;
				config.OpenConnection();
				sqlCommand.Connection = config.Connection;
				if (sqlTransaction != null)
				{
					sqlCommand.Transaction = sqlTransaction;
				}
				obj = sqlCommand.ExecuteScalar();
				config.CloseConnection();
				return obj;
			}
			catch
			{
				config.CloseConnection();
				throw;
			}
		}

		protected int ExecuteNonQuery(string exp)
		{
			int num = 0;
			try
			{
				SqlCommand obj = new SqlCommand(exp, config.Connection)
				{
					CommandType = CommandType.Text
				};
				config.OpenConnection();
				obj.Transaction = config.SqlTransaction;
				obj.Connection = config.Connection;
				num = obj.ExecuteNonQuery();
				config.CloseConnection();
				return num;
			}
			catch(Exception ex)
			{
				config.CloseConnection();
				Console.WriteLine(ex.ToString());
				return 0;
				
			}
		}

		public int ExecuteNonQuery(string exp, SqlTransaction sqlTransaction)
		{
			try
			{
				return ExecuteNonQuery(exp, updateInBatch: false, sqlTransaction);
			}
			catch
			{ 
			
				throw;
			}
		}

		public int ExecuteNonQuery(string exp, bool updateInBatch, SqlTransaction sqlTransaction)
		{
			int num = 0;
			try
			{
				SqlCommand sqlCommand = new SqlCommand(exp, config.Connection);
				if (updateInBatch)
				{
					dsCommand.UpdateBatchSize = 1000;
					sqlCommand.UpdatedRowSource = UpdateRowSource.None;
					sqlCommand.CommandTimeout = 100;
				}
				sqlCommand.CommandType = CommandType.Text;
				config.OpenConnection();
				sqlCommand.Connection = config.Connection;
				if (sqlTransaction != null)
				{
					sqlCommand.Transaction = sqlTransaction;
				}
				num = sqlCommand.ExecuteNonQuery();
				dsCommand.UpdateBatchSize = 1;
				config.CloseConnection();
				return num;
			}
			catch
			{
				config.CloseConnection();
				throw;
			}
		}

		public int ExecuteNonQuery(SqlCommand command)
		{
			int num = 0;
			bool flag = false;
			try
			{
				if (command.Connection == null)
				{
					config.OpenConnection();
					command.Connection = config.Connection;
					flag = true;
				}
				else if (command.Connection.State != ConnectionState.Open)
				{
					command.Connection.Open();
				}
				return command.ExecuteNonQuery();
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag)
				{
					config.CloseConnection();
				}
			}
		}

		protected int ExecuteNonQuery(string exp, SqlCommand command)
		{
			int num = 0;
			bool flag = false;
			try
			{
				command.CommandText = exp;
				command.CommandType = CommandType.Text;
				if (command.Connection == null)
				{
					config.OpenConnection();
					command.Connection = config.Connection;
					flag = true;
				}
				else if (command.Connection.State != ConnectionState.Open)
				{
					command.Connection.Open();
				}
				return command.ExecuteNonQuery();
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag)
				{
					config.CloseConnection();
				}
			}
		}

		internal object ExecuteScalar(SqlCommand command)
		{
			bool flag = false;
			try
			{
				if (command.Connection == null)
				{
					config.OpenConnection();
					command.Connection = config.Connection;
					flag = true;
				}
				else if (command.Connection.State != ConnectionState.Open)
				{
					command.Connection.Open();
				}
				return command.ExecuteScalar();
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag)
				{
					config.CloseConnection();
				}
			}
		}

		protected string IncrementNumber(string number)
		{
			if (number == null)
			{
				return number;
			}
			char[] array = number.ToCharArray();
			string text = "";
			int num = 0;
			if (array.Length != 0 && number != "0" && (array[0] == '0' || char.IsLetter(array[0])))
			{
				do
				{
					text += array[num++].ToString();
				}
				while (num < array.Length && (array[num] == '0' || char.IsLetter(array[num])));
			}
			string text2 = "";
			int num2 = 0;
			for (num2 = num; num2 < array.Length && char.IsDigit(array[num2]); num2++)
			{
				text2 += array[num2].ToString();
			}
			int num3;
			try
			{
				num3 = int.Parse(text2);
			}
			catch
			{
				num3 = 0;
			}
			num3++;
			text2 = "";
			for (int i = num2; i < array.Length; i++)
			{
				text2 += array[i].ToString();
			}
			text2 = num3.ToString() + text2;
			if (text.Length > 0)
			{
				text2 = text + text2;
			}
			return text2;
		}

		protected string GetIncrementedNumber(string number)
		{
			if (number == null)
			{
				return number;
			}
			if (number.LastIndexOf("-") >= 0)
			{
				int num = number.LastIndexOf("-");
				string str = number.Substring(0, num + 1);
				number = number.Substring(num + 1);
				number = IncrementNumber(number);
				number = str + number;
			}
			else
			{
				number = IncrementNumber(number);
			}
			return number;
		}

		protected string AddSingleQuote(string str)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			int num;
			for (num = 0; num != str.Length; num += 2)
			{
				num = str.IndexOf('\'', num);
				if (num < 0)
				{
					break;
				}
				str = str.Insert(num, "'");
			}
			return str;
		}

		protected string RemoveChar(string str, char c)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			while (true)
			{
				int num = str.IndexOf(c);
				if (num < 0)
				{
					break;
				}
				str = str.Remove(num, 1);
			}
			return str;
		}

		protected bool IsParent(object parentID, object subID, string tableName, string parentFieldName, string subFieldName)
		{
			object obj = subID;
			while (true)
			{
				string exp = "SELECT " + parentFieldName + " FROM " + tableName + " WHERE " + subFieldName + "=" + obj.ToString();
				obj = ExecuteScalar(exp);
				if (obj == DBNull.Value || obj == null)
				{
					break;
				}
				if (obj.ToString() == parentID.ToString())
				{
					return true;
				}
			}
			return false;
		}

		protected int GetParentLevel(object parentID, string tableName, string parentFieldName, string subFieldName)
		{
			if (parentID == null || parentID == DBNull.Value)
			{
				return 0;
			}
			int num = 0;
			while (true)
			{
				string exp = "SELECT " + parentFieldName + " FROM " + tableName + " WHERE " + subFieldName + "=" + parentID.ToString();
				parentID = ExecuteScalar(exp);
				if (parentID == DBNull.Value || parentID == null)
				{
					break;
				}
				num++;
			}
			return num;
		}

		protected bool AddActivityLog(string entityName, string entityID, string sysDocID, ActivityTypes activityType, object transAmount, SysDocTypes glType, object reference, SqlTransaction sqlTransaction)
		{
			return AddActivityLog(entityName, entityID, sysDocID, activityType, null, transAmount, glType, reference, sqlTransaction);
		}

		protected bool AddActivityLog(string entityName, string entiyID, string sysDocID, ActivityTypes activityType, SqlTransaction sqlTransaction)
		{
			return AddActivityLog(entityName, entiyID, sysDocID, activityType, null, null, SysDocTypes.None, null, sqlTransaction);
		}

		protected bool AddActivityLog(string entityName, string entiyID, ActivityTypes activityType, SqlTransaction sqlTransaction)
		{
			return AddActivityLog(entityName, entiyID, "", activityType, null, null, SysDocTypes.None, null, sqlTransaction);
		}

		protected bool AddActivityLog(string entityName, string entiyID, string sysDocID, ActivityTypes activityType, string payeeName, object transAmount, SysDocTypes glType, object reference, SqlTransaction sqlTransaction)
		{
			if (!StoreConfiguration.IsActivityLogEnabled)
			{
				return false;
			}
			using (ActivityLogs activityLogs = new ActivityLogs(DBConfig))
			{
				return activityLogs.InsertActivityLog(entityName, entiyID, sysDocID, activityType, payeeName, transAmount, glType, DataComboType.None, reference, sqlTransaction);
			}
		}

		protected bool AddActivityLog(string entityName, string entiyID, string sysDocID, ActivityTypes activityType, string payeeName, object transAmount, SysDocTypes glType, DataComboType comboType, object reference, SqlTransaction sqlTransaction)
		{
			if (!StoreConfiguration.IsActivityLogEnabled)
			{
				return false;
			}
			using (ActivityLogs activityLogs = new ActivityLogs(DBConfig))
			{
				return activityLogs.InsertActivityLog(entityName, entiyID, sysDocID, activityType, payeeName, transAmount, glType, comboType, reference, sqlTransaction);
			}
		}

		protected void BuildSqlFields(SqlBuilder sqlBuilder, params string[] columns)
		{
			if (sqlBuilder == null)
			{
				throw new NullReferenceException("sqlBuilder is null.");
			}
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				if (text.IndexOf("!") > 0)
				{
					string columnName = text.Substring(text.IndexOf(".") + 1, text.IndexOf("!") - text.IndexOf(".") - 1);
					string alias = text.Substring(text.IndexOf("!") + 1);
					sqlBuilder.AddColumn(tableName, columnName, alias);
				}
				else
				{
					string columnName2 = text.Substring(text.IndexOf(".") + 1);
					sqlBuilder.AddColumn(tableName, columnName2);
				}
			}
		}

		protected string GetCommaSeperatedIDs(DataSet data, string tableName, string idField)
		{
			if (data == null || data.Tables[tableName] == null || data.Tables[tableName].Rows.Count == 0)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (data.Tables[tableName].Rows.Count >= 0)
			{
				stringBuilder.Append("'" + AddSingleQuote(data.Tables[tableName].Rows[0][idField].ToString()) + "'");
			}
			string text = "";
			for (int i = 1; i < data.Tables[tableName].Rows.Count; i++)
			{
				text = data.Tables[tableName].Rows[i][idField].ToString();
				if (!stringBuilder.ToString().Contains("'" + text + "'"))
				{
					stringBuilder.AppendLine(", '" + AddSingleQuote(data.Tables[tableName].Rows[i][idField].ToString()) + "' ");
				}
			}
			return stringBuilder.ToString();
		}
	}
}
