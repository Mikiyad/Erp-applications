using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.DataControls.QueryBuilder
{
	public class SQL
	{
		private string ms_Server;

		private string ms_DataBase;

		private string ms_User;

		private string ms_Password;

		private string ms_Sql;

		private Form mi_Owner;

		private Thread mi_Thread;

		private SqlConnection mi_Connect;

		private SqlCommand mi_Command;

		private DataSet mi_DataSet;

		private string ms_ErrorMsg;

		private string ms_ExecTime;

		private bool mb_Aborting;

		private int ms32_FirstLine;

		public string ExecuteTime => ms_ExecTime;

		public SQL(Form i_Owner, string s_Server, string s_DataBase, string s_User, string s_Password)
		{
			mi_Owner = i_Owner;
			ms_Server = s_Server;
			ms_DataBase = s_DataBase;
			ms_User = s_User;
			ms_Password = s_Password;
		}

		public DataSet ExecuteSQL(string s_Sql, int s32_FirstLine)
		{
			if (mi_Thread != null)
			{
				throw new Exception("Application design error: Create a new instance of the SQL class for each query!");
			}
			ms_Sql = s_Sql;
			mi_DataSet = null;
			ms_ErrorMsg = null;
			mb_Aborting = false;
			ms32_FirstLine = s32_FirstLine;
			ms_ExecTime = "0.000";
			mi_Thread = new Thread(WorkThread);
			mi_Thread.Start();
			DataSet result = mi_DataSet;
			mi_Connect = null;
			mi_Command = null;
			mi_DataSet = null;
			return result;
		}

		private void WorkThread()
		{
			try
			{
				string str = $"Server={ms_Server}; Database={ms_DataBase}; ";
				str = ((ms_User != null || ms_Password != null) ? (str + $"User ID={ms_User}; Password={ms_Password}; Trusted_Connection=no;") : (str + "Trusted_Connection=yes;"));
				mi_Connect = new SqlConnection(str);
				mi_Connect.Open();
			}
			catch (Exception ex)
			{
				mi_Connect = null;
				ms_ErrorMsg = $"Error while connecting server {ms_Server}.\n{ex.Message}";
			}
			mi_Command = null;
			try
			{
				mi_Connect.Close();
			}
			catch
			{
			}
		}

		private void OnUserAbort()
		{
			if (!mb_Aborting)
			{
				mb_Aborting = true;
				if (mi_Command != null)
				{
					mi_Command.Cancel();
					Thread.Sleep(500);
				}
				if (mi_Connect != null)
				{
					try
					{
						mi_Connect.Close();
					}
					catch
					{
					}
				}
			}
		}

		public DataTable ReadTable(string s_SQL, int s32_FirstLine)
		{
			DataSet dataSet = ExecuteSQL(s_SQL, s32_FirstLine);
			if (dataSet == null)
			{
				return null;
			}
			if (dataSet.Tables.Count != 1)
			{
				return null;
			}
			return dataSet.Tables[0];
		}

		public object ReadScalar(string s_SQL, int s32_FirstLine)
		{
			DataTable dataTable = ReadTable(s_SQL, s32_FirstLine);
			if (dataTable == null)
			{
				return null;
			}
			dataTable.Clear();
			return null;
		}

		public string[] ListAllDataBases()
		{
			ms_DataBase = "master";
			DataTable dataTable = ReadTable("SELECT name FROM sysdatabases ORDER BY name", -1);
			if (dataTable == null)
			{
				return null;
			}
			string[] array = new string[dataTable.Rows.Count];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				array[i] = (string)dataTable.Rows[i]["name"];
			}
			dataTable.Clear();
			return array;
		}

		public object SysObjectExists(string s_SysObj)
		{
			string s_SQL = "SELECT COUNT(*) FROM sysobjects WHERE id= object_id('[" + s_SysObj + "]')";
			object obj = ReadScalar(s_SQL, -1);
			if (!(obj is int))
			{
				return null;
			}
			return (int)obj > 0;
		}

		public TableDef LoadTableDefFromServer(string s_TableName)
		{
			string s_SQL = BuildSqlGetTableColumns(s_TableName);
			DataTable dataTable = ReadTable(s_SQL, -1);
			if (dataTable == null)
			{
				return null;
			}
			return TableDef.FromDataTable(dataTable);
		}

		private string BuildSqlGetTableColumns(string s_TableName)
		{
			string text = "";
			if (s_TableName != null)
			{
				text += $" WHERE obj.name = '{s_TableName}'\n";
			}
			return string.Format(Functions.ReadStringResource("GetTableColumns.sql"), text) + "\n";
		}

		public Hashtable ReadAllSysObjects(bool b_Trig, bool b_Proc, bool b_View, bool b_Func, bool b_Tabl, bool b_Date)
		{
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			if (b_Trig)
			{
				hashtable2.Add("Triggers", string.Format("SELECT object_name(id) AS name, text FROM syscomments WHERE encrypted=0 AND ({0}) ORDER BY id, colid \n", "objectproperty(id, 'IsTrigger')=1"));
			}
			if (b_Proc)
			{
				hashtable2.Add("Procedures", string.Format("SELECT object_name(id) AS name, text FROM syscomments WHERE encrypted=0 AND ({0}) ORDER BY id, colid \n", "objectproperty(id, 'IsProcedure')=1 OR objectproperty(id, 'IsExtendedProc')=1"));
			}
			if (b_View)
			{
				hashtable2.Add("Views", string.Format("SELECT object_name(id) AS name, text FROM syscomments WHERE encrypted=0 AND ({0}) ORDER BY id, colid \n", "objectproperty(id, 'IsView')=1"));
			}
			if (b_Func)
			{
				hashtable2.Add("Functions", string.Format("SELECT object_name(id) AS name, text FROM syscomments WHERE encrypted=0 AND ({0}) ORDER BY id, colid \n", "objectproperty(id, 'IsTableFunction')=1 OR objectproperty(id, 'IsInlineFunction')=1 OR objectproperty(id, 'IsScalarFunction')=1"));
			}
			if (b_Tabl)
			{
				hashtable2.Add("Tables", BuildSqlGetTableColumns(null));
			}
			if (b_Date)
			{
				hashtable2.Add("Dates", "SELECT name, crdate FROM sysobjects\n");
			}
			if (hashtable2.Count == 0)
			{
				return null;
			}
			string text = "";
			foreach (string key in hashtable2.Keys)
			{
				text += (string)hashtable2[key];
			}
			DataSet dataSet = ExecuteSQL(text, -1);
			if (dataSet == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(100000);
			int num = 0;
			foreach (string key2 in hashtable2.Keys)
			{
				Application.DoEvents();
				DataTable dataTable = dataSet.Tables[num++];
				Hashtable hashtable3 = new Hashtable();
				if (!(key2 == "Dates"))
				{
					if (key2 == "Tables")
					{
						hashtable3 = TableDef.TableFactory(dataTable);
					}
					else
					{
						for (int i = 0; i < dataTable.Rows.Count; i++)
						{
							DataRow dataRow = dataTable.Rows[i];
							string text3 = Functions.ToStr(dataRow["name"]);
							stringBuilder.Append(dataRow["text"]);
							if (i + 1 == dataTable.Rows.Count || text3 != Functions.ToStr(dataTable.Rows[i + 1]["name"]))
							{
								hashtable3[text3] = stringBuilder.ToString();
								stringBuilder.Length = 0;
							}
						}
					}
				}
				else
				{
					foreach (DataRow row in dataTable.Rows)
					{
						hashtable3[row["name"]] = row["crdate"];
					}
				}
				hashtable[key2] = hashtable3;
			}
			dataSet.Clear();
			return hashtable;
		}

		public string[] ReadAllDataTypes()
		{
			string s_SQL = "SELECT name FROM systypes ORDER BY name";
			DataTable dataTable = ReadTable(s_SQL, -1);
			if (dataTable == null)
			{
				return null;
			}
			string[] array = new string[dataTable.Rows.Count];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				array[i] = (string)dataTable.Rows[i]["name"];
			}
			return array;
		}

		public static string ParseErrorMessage(Exception Ex, int s32_FirstSelectedLine)
		{
			int num = 0;
			if (Ex is SqlException)
			{
				num = ((SqlException)Ex).LineNumber;
			}
			string[] array = Ex.Message.Replace("\r", "").Split('\n');
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = 0;
				if (array[i].StartsWith("Line ") || array[i].StartsWith("Línea ") || array[i].StartsWith("Zeile "))
				{
					int num3 = array[i].IndexOf(":");
					if (num3 > 5 && num3 < 10)
					{
						num2 = int.Parse(array[i].Substring(5, num3 - 5));
						array[i] = array[i].Substring(num3 + 1).Trim();
					}
				}
				if (num2 == 0 && i == 0 && num > 1 && num < 50000)
				{
					num2 = num;
				}
				text = ((s32_FirstSelectedLine <= 0 || num2 <= 0) ? (text + array[i] + "\n") : (text + $"Line {num2 + s32_FirstSelectedLine - 1}: {array[i]}\n"));
			}
			if (text.Trim().Length == 0)
			{
				text = "The Sql server created an exception without a message!";
			}
			return text;
		}
	}
}
