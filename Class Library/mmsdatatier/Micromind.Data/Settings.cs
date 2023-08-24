using Micromind.Common;
using Micromind.Common.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;

namespace Micromind.Data
{
	public sealed class Settings : StoreObject
	{
		private string convertedVoucherID = "";

		public Settings(Config config)
			: base(config)
		{
		}

		public bool SaveSetting(string id, string appName, string key, string val, string data)
		{
			if (id == null)
			{
				id = string.Empty;
			}
			if (appName == null)
			{
				appName = string.Empty;
			}
			if (key == null)
			{
				key = string.Empty;
			}
			if (val == null)
			{
				val = string.Empty;
			}
			if (data == null)
			{
				data = string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ID FROM Settings WHERE ID = '").Append(id).Append("' AND ");
			stringBuilder.Append("SName='").Append(appName).Append("' AND ");
			stringBuilder.Append("SKey='").Append(key).Append("' AND ")
				.Append("SValue='")
				.Append(val)
				.Append("'");
			if (ExecuteScalar(stringBuilder.ToString()) == null)
			{
				stringBuilder = null;
				stringBuilder = new StringBuilder();
				stringBuilder.Append("INSERT INTO Settings (ID,SName,SKey,SValue,SData) ");
				stringBuilder.Append("VALUES ('").Append(id).Append("','")
					.Append(appName)
					.Append("','");
				stringBuilder.Append(key).Append("','").Append(val)
					.Append("','")
					.Append(data.ToString())
					.Append("')");
			}
			else
			{
				stringBuilder = null;
				stringBuilder = new StringBuilder();
				stringBuilder.Append("UPDATE Settings SET SData ='").Append(data).Append("' WHERE ");
				stringBuilder.Append("ID='").Append(id).Append("' AND ");
				stringBuilder.Append("SName='").Append(appName).Append("' AND ");
				stringBuilder.Append("SKey='").Append(key).Append("' AND ");
				stringBuilder.Append("SValue='").Append(val).Append("' ");
			}
			return ExecuteNonQuery(stringBuilder.ToString()) > 0;
		}

		public bool SaveSettingStream(string userID, string key, string groupName, byte[] data)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Settings WHERE [SKey]='" + key + "' AND ID = '" + userID + "'";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO Settings  (ID,SKey,SName,SBinData) VALUES(@ID, @SKey,@SName,@BinData)");
				sqlCommand.Parameters.AddWithValue("@BinData", data);
				sqlCommand.Parameters.AddWithValue("@SKey", key);
				sqlCommand.Parameters.AddWithValue("@SName", groupName);
				sqlCommand.Parameters.AddWithValue("@ID", userID);
				sqlCommand.Transaction = sqlTransaction;
				flag &= (ExecuteNonQuery(sqlCommand) > 0);
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

		public bool SaveSettingStreamForDashBoard(string userID, string key, string groupName, byte[] data, string SysDocID, string VoucherID, int AutoKeyID, DataSet ds, bool IsNewRecord)
		{
			bool flag = true;
			try
			{
				Regex.Split(key, "-")[0].ToString();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool num = new SystemDocuments(base.DBConfig).ExistDocumentNumber("Temporary_Transaction", "VoucherID", SysDocID, VoucherID, sqlTransaction);
				string text = "";
				text = (num ? "U" : "A");
				string text2 = "";
				DateTime result = new DateTime(1998, 4, 30);
				new DataTable();
				SysDocTypes transaction = (SysDocTypes)Enum.Parse(value: int.Parse(groupName.ToString()).ToString(CultureInfo.InvariantCulture), enumType: typeof(SysDocTypes));
				IsNewRecord = new SystemDocuments(base.DBConfig).ExistDocumentNumber("Temporary_Transaction", "VoucherID", SysDocID, VoucherID, sqlTransaction);
				DateTime.TryParse(ds.Tables[0].Rows[0]["TransactionDate"].ToString(), out result);
				text2 = ds.Tables[0].Rows[0]["CustomerID"].ToString();
				SqlCommand sqlCommand = new SqlCommand();
				if (text == "A")
				{
					sqlCommand = new SqlCommand("INSERT INTO Temporary_Transaction  (ID,SKey,SName,SBinData,SysDocID,VoucherID,CustomerID,TransactionDate) VALUES(@ID, @SKey,@SName,@BinData,@SysDocID,@VoucherID,@CustomerID,@TransactionDate)");
				}
				else if (text == "U")
				{
					sqlCommand = new SqlCommand("UPDATE Temporary_Transaction  SET ID=@ID,SKey=@SKey,SName=@SName,SBinData=@BinData,SysDocID=@SysDocID,VoucherID=@VoucherID,CustomerID=@CustomerID,TransactionDate=@TransactionDate WHERE AutoKeyID=@AutoKeyID");
				}
				sqlCommand.Parameters.AddWithValue("@BinData", data);
				sqlCommand.Parameters.AddWithValue("@SKey", key);
				sqlCommand.Parameters.AddWithValue("@SName", groupName);
				sqlCommand.Parameters.AddWithValue("@ID", userID);
				sqlCommand.Parameters.AddWithValue("@SysDocID", SysDocID);
				sqlCommand.Parameters.AddWithValue("@VoucherID", VoucherID);
				sqlCommand.Parameters.AddWithValue("@customerID", text2);
				sqlCommand.Parameters.AddWithValue("@TransactionDate", result);
				sqlCommand.Parameters.AddWithValue("@AutoKeyID", AutoKeyID);
				sqlCommand.Transaction = sqlTransaction;
				flag &= (ExecuteNonQuery(sqlCommand) > 0);
				if (text == "A")
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(SysDocID, VoucherID, ds.Tables[0].ToString(), "VoucherID", sqlTransaction);
				}
				flag &= insertTempActvity(text, VoucherID, SysDocID, transaction, flag, sqlTransaction);
				if (!flag)
				{
					throw new CompanyException("Trasaction - " + SysDocID + " - " + VoucherID + " already updated on Temporary trasanction,Please click the document link", 50001);
				}
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

		public bool insertTempDeleteActvity(string key, bool updateresult, SqlTransaction sqltrans, string CnvVoucherID, int autoKeyID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT * FROM Temporary_Transaction WHERE 1=1 ";
			if (key != "")
			{
				text = text + " AND SKey ='" + key + "'";
			}
			FillDataSet(dataSet, "Temp_Tran", text);
			text = (("DELETE FROM Temporary_Transaction WHERE [AutoKeyID] = " + autoKeyID) ?? "");
			bool num = updateresult & (ExecuteNonQuery(text, sqltrans) >= 0);
			convertedVoucherID = CnvVoucherID;
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			string text2 = dataRow["SysDocID"].ToString();
			string entityID = dataRow["VoucherID"].ToString();
			SysDocTypes transaction = (SysDocTypes)Enum.Parse(value: int.Parse(new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", text2, sqltrans).ToString()).ToString(CultureInfo.InvariantCulture), enumType: typeof(SysDocTypes));
			return num & insertTempActvity("D", entityID, text2, transaction, updateresult, sqltrans);
		}

		public bool insertTempActvity(string chkField, string entityID, string sysDocID, SysDocTypes transaction, bool updateresult, SqlTransaction sqlTransaction)
		{
			bool flag = updateresult;
			string text = "";
			ActivityTypes activityTypes = ActivityTypes.Other;
			if (chkField == "A")
			{
				activityTypes = ActivityTypes.Add;
			}
			else if (chkField == "U")
			{
				activityTypes = ActivityTypes.Update;
			}
			else if (chkField == "D")
			{
				activityTypes = ActivityTypes.TempConverted;
			}
			if (transaction == SysDocTypes.SalesReceipt)
			{
				transaction = SysDocTypes.SalesInvoice;
			}
			text = activityTypes + "  Temp-" + transaction.ToString() + " " + entityID.ToString();
			if (chkField == "D")
			{
				text = activityTypes + " " + transaction.ToString() + " " + entityID.ToString() + " to live " + transaction.ToString() + " " + convertedVoucherID.ToString();
			}
			SqlCommand sqlCommand = new SqlCommand("INSERT INTO [Activity Logs](ActivityType,EntityID,SysDocID,LogDate,UserID,MachineID,Description) VALUES(@ActivityType,@EntityID,@SysDocID,@LogDate,@UserID,@MachineID,@Description)");
			sqlCommand.Parameters.AddWithValue("@ActivityType", activityTypes);
			sqlCommand.Parameters.AddWithValue("@EntityID", entityID);
			sqlCommand.Parameters.AddWithValue("@SysDocID", sysDocID);
			sqlCommand.Parameters.AddWithValue("@LogDate", DateTime.Now);
			sqlCommand.Parameters.AddWithValue("@UserID", base.DBConfig.AccessUser);
			sqlCommand.Parameters.AddWithValue("@MachineID", base.DBConfig.ClientMachineName);
			sqlCommand.Parameters.AddWithValue("@Description", text);
			sqlCommand.Transaction = sqlTransaction;
			return flag & (ExecuteNonQuery(sqlCommand) > 0);
		}

		public bool SaveSetting(string userID, string key, object value)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Settings WHERE SKey='" + key + "' AND ID = '" + userID + "'";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO Settings  (ID,SKey,SValue) VALUES(@ID, @SKey,@SValue)");
				sqlCommand.Parameters.AddWithValue("@SValue", value.ToString());
				sqlCommand.Parameters.AddWithValue("@SKey", key);
				sqlCommand.Parameters.AddWithValue("@ID", userID);
				sqlCommand.Transaction = sqlTransaction;
				flag &= (ExecuteNonQuery(sqlCommand) > 0);
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

		public object GetData(string id, string appName, string key, string val)
		{
			if (id == null)
			{
				id = string.Empty;
			}
			if (appName == null)
			{
				appName = string.Empty;
			}
			if (key == null)
			{
				key = string.Empty;
			}
			if (val == null)
			{
				val = string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT SData FROM Settings WHERE ID = '").Append(id).Append("' AND ");
			stringBuilder.Append("SName='").Append(appName).Append("' AND ");
			stringBuilder.Append("SKey='").Append(key).Append("' AND ")
				.Append("SValue='")
				.Append(val)
				.Append("'");
			return ExecuteScalar(stringBuilder.ToString());
		}

		public object GetUserSetting(string userID, string key)
		{
			string exp = "SELECT SValue \r\n                           FROM Settings WHERE ID='" + userID + "' AND SKey='" + key + "'";
			return ExecuteScalar(exp);
		}

		public byte[] GetBinaryData(string userID, string groupName, string key)
		{
			new DataSet();
			string text = "SELECT SBinData\r\n                           FROM Settings WHERE SKey='" + key + "' ";
			if (userID != "")
			{
				text = text + " AND ID='" + userID + "' ";
			}
			if (groupName != "")
			{
				text = text + " AND sName ='" + groupName + "'";
			}
			object obj = ExecuteScalar(text);
			new MemoryStream();
			try
			{
				return (byte[])obj;
			}
			catch
			{
				throw;
			}
		}

		public byte[] GetBinaryTemporaryData(string userID, string groupName, string key)
		{
			new DataSet();
			string text = "SELECT SBinData\r\n                           FROM Temporary_Transaction WHERE SKey='" + key + "' ";
			if (userID != "")
			{
				text = text + " AND ID='" + userID + "' ";
			}
			if (groupName != "")
			{
				text = text + " AND sName ='" + groupName + "'";
			}
			object obj = ExecuteScalar(text);
			new MemoryStream();
			try
			{
				return (byte[])obj;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSettingsList(string userID, string groupName)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT SKey AS Name,SName \r\n                           FROM Settings WHERE 1=1 ";
			if (userID != "")
			{
				text = text + " AND ID ='" + userID + "'";
			}
			if (groupName != "")
			{
				text = text + " AND SName = '" + groupName + "'";
			}
			FillDataSet(dataSet, "Settings", text);
			return dataSet;
		}

		public byte[] Serializer(object _object)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				((IFormatter)new BinaryFormatter()).Serialize((Stream)memoryStream, _object);
				return memoryStream.ToArray();
			}
		}

		public byte[] StrToByteArray(string str)
		{
			return new UTF8Encoding().GetBytes(str);
		}

		public DataSet GetSettingsListData(string userID, string groupName)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT  SBinData,T.SysDocID,VoucherID,CustomerID,TransactionDate,AutoKeyID\r\n                            FROM Temporary_Transaction  T LEFT JOIN System_Document SD ON T.SysDocID=SD.SysDocID WHERE 1=1 \r\n                            ";
			if (userID != "")
			{
				str = str + "  AND SD.LocationID=(Select DefaultTransactionLocationID from Users where UserID='" + userID + "')";
			}
			if (groupName != "")
			{
				str = str + " AND SName = '" + groupName + "'";
			}
			str += " Order By T.SysDocID,VoucherID,CustomerID,TransactionDate";
			FillDataSet(dataSet, "Settings", str);
			return dataSet;
		}

		public string GetTempTransactionByKey(string skeylike)
		{
			new DataSet();
			string text = "SELECT  SKey\r\n                           FROM Temporary_Transaction WHERE 1=1 ";
			if (skeylike != "")
			{
				text = text + " AND SKey Like '" + skeylike + "%'";
			}
			return ExecuteScalar(text).ToString();
		}

		public bool DeleteSetting(string key, string userID, string groupName)
		{
			bool flag = true;
			try
			{
				if (userID + key + groupName == "")
				{
					throw new CompanyException("At least one of setting parameters must be provided to delete.");
				}
				string text = "DELETE FROM Settings WHERE 1=1 ";
				if (key != "")
				{
					text = text + " AND SKey = '" + key + "'";
				}
				if (userID != "")
				{
					text = text + " AND ID = '" + userID + "'";
				}
				if (groupName != "")
				{
					text = text + " AND sName = '" + groupName + "'";
				}
				return flag & Delete(text, null);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteSettingTemporary(string key, string userID, string groupName)
		{
			bool flag = true;
			try
			{
				if (userID + key + groupName == "")
				{
					throw new CompanyException("At least one of setting parameters must be provided to delete.");
				}
				string text = "DELETE FROM Temporary_Transaction WHERE 1=1 ";
				if (key != "")
				{
					text = text + " AND SKey = '" + key + "'";
				}
				if (userID != "")
				{
					text = text + " AND ID = '" + userID + "'";
				}
				if (groupName != "")
				{
					text = text + " AND sName = '" + groupName + "'";
				}
				return flag & Delete(text, null);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteSetting(string autokey)
		{
			bool flag = true;
			try
			{
				if (autokey == "")
				{
					throw new CompanyException("At least one of setting parameters must be provided to delete.");
				}
				string text = "DELETE FROM Temporary_Transaction WHERE 1=1 ";
				if (autokey != "")
				{
					text = text + " AND AutoKeyID = '" + autokey + "'";
				}
				return flag & Delete(text, null);
			}
			catch
			{
				throw;
			}
		}

		public int GetTemporaryAutoKeyID(string sKey)
		{
			new DataSet();
			string text = "SELECT AutoKeyID FROM Temporary_Transaction WHERE 1=1";
			if (sKey != "")
			{
				text = text + " AND SKey like '" + sKey + "%'";
			}
			return int.Parse(ExecuteScalar(text).ToString());
		}
	}
}
