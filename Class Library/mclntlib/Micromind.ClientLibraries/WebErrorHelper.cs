using Micromind.Common;
using Micromind.Common.Libraries;
using Micromind.Securities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	public sealed class WebErrorHelper
	{
		public static bool isConnectionLost;

		public const int RecordInUse = 547;

		public static Form SupportForm;

		private static string AppServerName;

		public static event EventHandler ConnectionLost;

		public static event EventHandler SendErrorMessage;

		public static event EventHandler ViewErrorMessage;

		private WebErrorHelper()
		{
		}

		public static string GetErrorCols(string tableName, DataSet dataSet, int rowIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (dataSet.Tables[tableName].Rows.Count > 0)
			{
				DataRow dataRow = dataSet.Tables[tableName].Rows[rowIndex];
				DataColumn[] columnsInError = dataRow.GetColumnsInError();
				foreach (DataColumn dataColumn in columnsInError)
				{
					stringBuilder.AppendFormat("{0}: {1} \n", dataColumn.ColumnName, dataRow.GetColumnError(dataColumn.ColumnName));
				}
			}
			return stringBuilder.ToString();
		}

		public static string GetErrorCols(string tableName, DataSet dataSet)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (dataSet.Tables[tableName].Rows.Count > 0)
			{
				DataRow dataRow = dataSet.Tables[tableName].Rows[0];
				DataColumn[] columnsInError = dataRow.GetColumnsInError();
				foreach (DataColumn dataColumn in columnsInError)
				{
					stringBuilder.AppendFormat("{0}: {1}\n ", dataColumn.ColumnName, dataRow.GetColumnError(dataColumn.ColumnName));
				}
			}
			return stringBuilder.ToString();
		}

		public static string GetErrorCols(DataRow dataRow)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataColumn[] columnsInError = dataRow.GetColumnsInError();
			foreach (DataColumn dataColumn in columnsInError)
			{
				stringBuilder.AppendFormat("{0}: {1}\n ", dataColumn.ColumnName, dataRow.GetColumnError(dataColumn.ColumnName));
			}
			return stringBuilder.ToString();
		}

		public static void ProcessError(SqlException ex)
		{
			ProcessError(ex, "");
		}

		public static void ProcessError(SqlException ex, params string[] userMessage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in userMessage)
			{
				stringBuilder.Append(value).Append("\n");
			}
			stringBuilder.Remove(stringBuilder.Length, 1);
			ProcessError(ex, stringBuilder.ToString());
		}

		public static void ProcessError(Exception ex, params string[] userMessage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in userMessage)
			{
				stringBuilder.Append(value).Append("\n");
			}
			stringBuilder.Remove(stringBuilder.Length, 1);
			ProcessError(ex, stringBuilder.ToString());
		}

		public static void ProcessError(SqlException ex, string userMessage)
		{
			if (ex == null)
			{
				return;
			}
			bool isShowSendButton = true;
			if (ex.Message.IndexOf("Login failed") >= 0)
			{
				WarningMessage(ex.Message);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			switch (ex.Number)
			{
			case 11:
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("\n");
				stringBuilder.Append("Please make sure that SQL Server is running.");
				isShowSendButton = false;
				break;
			case 2:
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("\n");
				stringBuilder.Append("Try to login again.");
				isShowSendButton = false;
				break;
			case 17:
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("\n");
				if (GlobalRules.IsMultiUser)
				{
					stringBuilder.Append("Please verify that your user ID and password are correct and SQL Server is running.");
				}
				else
				{
					stringBuilder.Append("Please verify that your user ID and password are correct and SQL Server is running.");
				}
				isShowSendButton = false;
				break;
			case 18456:
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("\n");
				if (GlobalRules.IsMultiUser)
				{
					stringBuilder.Append("Please verify that your user ID and password are correct.");
				}
				else
				{
					stringBuilder.Append("Please verify that your user ID and password are correct.");
				}
				isShowSendButton = false;
				break;
			case 4060:
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("\n");
				stringBuilder.Append("Please verify that the user ID and password are correct.");
				isShowSendButton = false;
				break;
			case 547:
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			case 5149:
				stringBuilder.Append("There is not sufficient disk space.");
				isShowSendButton = false;
				break;
			case 5110:
				stringBuilder.Append("The file path is not correct.");
				isShowSendButton = false;
				break;
			case 5105:
				stringBuilder.Append("The file name you have provided is not a valid file name, or cannot access the disk.");
				isShowSendButton = false;
				break;
			case 2601:
				stringBuilder.Append("This name is in use. Please enter another name.");
				isShowSendButton = false;
				break;
			case 208:
				stringBuilder.Append("One or more fields or tables missing in the database. Please make sure the database version is correct.");
				break;
			case 1801:
			case 1832:
				stringBuilder.Append(ex.Message + "Database name already exists.");
				isShowSendButton = false;
				break;
			case 207:
				stringBuilder.Append(ex.Message + "Invalid database.");
				isShowSendButton = false;
				break;
			case 8114:
				stringBuilder.Append("Invalid date.");
				isShowSendButton = false;
				break;
			case 229:
			case 15007:
				stringBuilder.Append("Access denied. Please contact your administrator.");
				isShowSendButton = false;
				break;
			case 3703:
				stringBuilder.Append("You cannot detach this database because it is currently in use.");
				isShowSendButton = false;
				break;
			case 3201:
				stringBuilder.Append("Cannot backup your data in this location. Please verify that you have sufficient access.");
				isShowSendButton = false;
				break;
			case 5172:
				stringBuilder.Append(ex.Message).Append("\n").Append("Please try again.");
				break;
			case 18452:
				Global.ReconnectionNeeded();
				break;
			default:
				stringBuilder.Append("SQL Server Error:" + ex.Number + " - " + ex.Message);
				break;
			}
			if (stringBuilder.Length > 0 || (userMessage != null && userMessage.Length > 0))
			{
				ErrorHelper.OnViewErrorMessage(new ErrorEvent("Error", stringBuilder.ToString() + "\n" + userMessage, ex)
				{
					isShowSendButton = isShowSendButton
				});
			}
		}

		public static void ProcessCompanyExceptionError(CompanyException ex, string userMessage)
		{
			ProcessCompanyExceptionError(ex, userMessage, MessageBoxIcon.Hand);
		}

		public static void ProcessCompanyExceptionError(CompanyException ex, string userMessage, MessageBoxIcon icon)
		{
			if (ex != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int number = ex.Number;
				if (number == 1028)
				{
					stringBuilder.Append(ex.Message);
					stringBuilder.Append("\n");
					stringBuilder.Append("Please select an account for the payee.");
				}
				else
				{
					stringBuilder.Append("System Error:" + ex.Number + " - " + ex.Message);
				}
				if (stringBuilder.Length > 0 || (userMessage != null && userMessage.Length > 0))
				{
					ErrorHelper.ErrorMessage(stringBuilder.ToString());
				}
			}
		}

		public static void ProcessError(Exception e)
		{
			if (!(e.GetType() == typeof(NothingException)))
			{
				ProcessError(e, "");
			}
		}

		public static void ProcessError(Exception e, string userMessage)
		{
			if (e != null && e.GetType() == typeof(SqlException))
			{
				ProcessError((SqlException)e, userMessage);
				return;
			}
			if (e != null && e.GetType() == typeof(CompanyException))
			{
				CompanyException ex = (CompanyException)e;
				if (ex.Number == 1038)
				{
					ErrorMessage("Cannot record transaction at this date.\nThe transaction date is in a period which is closed. Please change the transaction date or re-open the period.");
				}
				else if (ex.Number == 1041)
				{
					if (!isConnectionLost)
					{
						isConnectionLost = true;
						new ConnectionLostDialog().ShowDialog();
						isConnectionLost = false;
					}
				}
				else
				{
					ProcessCompanyExceptionError(ex, userMessage);
				}
				return;
			}
			if (e.Message.IndexOf("Login failed") >= 0 || e.GetType() == typeof(Micromind.Common.Libraries.InvalidKeyException) || e.GetType() == typeof(Micromind.Securities.InvalidKeyException))
			{
				WarningMessage(e.Message);
				return;
			}
			if (e.GetType() == typeof(DBConcurrencyException))
			{
				ErrorMessage("You cannot edit this record because this record has already been changed by another user. Please reopen the record before updating.");
				return;
			}
			if (e.GetType() == typeof(DBNotConnectedException))
			{
				WarningMessage("The connection has been lost. Please make sure you are connected to the database server.");
				return;
			}
			bool isShowSendButton = true;
			StringBuilder stringBuilder = new StringBuilder();
			if (e.GetType() == typeof(RemotingException))
			{
				stringBuilder.Append("A problem occured while connecting to the database server. Please make sure that 'Axolon Server' service is started.\nThe error message is:\n" + e.Message);
				isShowSendButton = false;
			}
			else
			{
				if (e.GetType() == typeof(ClosingBookException))
				{
					ErrorHelper.WarningMessage("Please provide a correct password to edit this transaction on or before the closing date.");
					isShowSendButton = false;
					return;
				}
				if (e.GetType() == typeof(SocketException))
				{
					stringBuilder.Append("Cannot connect to database server.\nPlease check your database and connection settings and make sure that 'Axolon Server' is started and accessible.");
					isShowSendButton = false;
				}
				else if (e.GetType() == typeof(InvalidCastException))
				{
					stringBuilder.Append(e.Message ?? "");
				}
				else if (e.GetType() == typeof(InvalidPrinterException))
				{
					stringBuilder.Append(e.Message + "\nThere is a problem with your printer. Please make sure that there is a printer installed and working.");
					isShowSendButton = false;
				}
				else if (e.GetType() == typeof(DBConcurrencyException))
				{
					stringBuilder.Append("You cannot edit this record because this record has already been changed by another user. Please reopen the record before updating.");
				}
				else if (e.GetType() == typeof(WebException))
				{
					stringBuilder.Append("Cannot not connect to the internet.");
					isShowSendButton = false;
				}
				else
				{
					stringBuilder.Append(e.Message);
				}
			}
			if (stringBuilder.Length > 0 || (userMessage != null && userMessage.Length > 0))
			{
				ErrorHelper.OnViewErrorMessage(new ErrorEvent("Error", stringBuilder.ToString() + "\n" + userMessage, e)
				{
					isShowSendButton = isShowSendButton
				});
			}
		}

		public static void WarningMessage(params string[] msgs)
		{
			if (msgs.Length != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string arg in msgs)
				{
					stringBuilder.AppendFormat("{0}\n", arg);
				}
				MessageBox.Show(stringBuilder.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		public static void ErrorMessage(params string[] msgs)
		{
			if (msgs.Length != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string arg in msgs)
				{
					stringBuilder.AppendFormat("{0}\n", arg);
				}
				MessageBox.Show(stringBuilder.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public static void TitledWarningMessage(string title, params string[] msgs)
		{
			if ((title == null || title == string.Empty) && msgs.Length == 0)
			{
				return;
			}
			if (msgs.Length == 0)
			{
				MessageBox.Show(title, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string arg in msgs)
			{
				stringBuilder.AppendFormat("{0}\n", arg);
			}
			if (title != null && title.Trim().Length != 0)
			{
				MessageBox.Show(stringBuilder.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show(stringBuilder.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		public static void StopMessage(string title, params string[] msgs)
		{
			if ((title == null || title == string.Empty) && msgs.Length == 0)
			{
				return;
			}
			if (msgs.Length == 0)
			{
				MessageBox.Show(title, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string arg in msgs)
			{
				stringBuilder.AppendFormat("{0}\n", arg);
			}
			if (title != null && title.Trim().Length != 0)
			{
				MessageBox.Show(stringBuilder.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				MessageBox.Show(stringBuilder.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public static void InformationMessage(params string[] msgs)
		{
			InformationMessage(Application.ProductName, msgs);
		}

		public static void InformationMessage(string title, params string[] msgs)
		{
			if ((title == null || title == string.Empty) && msgs.Length == 0)
			{
				return;
			}
			if (msgs.Length == 0)
			{
				MessageBox.Show(title, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string arg in msgs)
			{
				stringBuilder.AppendFormat("{0}\n", arg);
			}
			if (title != null && title.Trim().Length != 0)
			{
				MessageBox.Show(stringBuilder.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show(stringBuilder.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		public static void WarningMessage(Exception ex)
		{
			WarningMessage(ex.Message);
		}

		public static void WarningMessage(StringBuilder builder)
		{
			MessageBox.Show(builder.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public static DialogResult WarningMessageOkCancel(params string[] msgs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string arg in msgs)
			{
				stringBuilder.AppendFormat("{0}\n", arg);
			}
			return MessageBox.Show(stringBuilder.ToString(), Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
		}

		public static DialogResult GetItemDeleteConfirmation()
		{
			return ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this item?");
		}

		public static DialogResult QuestionMessageYesNoCancel(StringBuilder builder)
		{
			return MessageBox.Show(builder.ToString(), Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
		}

		public static DialogResult QuestionMessageYesNoCancel(params string[] msgs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string arg in msgs)
			{
				stringBuilder.AppendFormat("{0}\n", arg);
			}
			return MessageBox.Show(stringBuilder.ToString(), Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
		}

		public static DialogResult QuestionMessageYesNo(StringBuilder builder)
		{
			return MessageBox.Show(builder.ToString(), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public static DialogResult QuestionMessageYesNo(params string[] msgs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string arg in msgs)
			{
				stringBuilder.AppendFormat("{0}\n", arg);
			}
			return MessageBox.Show(stringBuilder.ToString(), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public static DialogResult QuestionMessage(MessageBoxButtons messageBoxButton, params string[] msgs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string arg in msgs)
			{
				stringBuilder.AppendFormat("{0}\n", arg);
			}
			return MessageBox.Show(stringBuilder.ToString(), Application.ProductName, messageBoxButton, MessageBoxIcon.Question);
		}

		public static void OnSendErrorMessage(ErrorEvent errorEvent)
		{
			if (WebErrorHelper.SendErrorMessage != null)
			{
				WebErrorHelper.SendErrorMessage(null, errorEvent);
			}
		}

		public static void OnViewErrorMessage(ErrorEvent errorEvent)
		{
			if (WebErrorHelper.ViewErrorMessage != null)
			{
				WebErrorHelper.ViewErrorMessage(null, errorEvent);
			}
		}

		public static void OnConnectionLost(ErrorEvent errorEvent)
		{
			if (WebErrorHelper.ConnectionLost != null)
			{
				WebErrorHelper.ConnectionLost(null, errorEvent);
			}
		}

		static WebErrorHelper()
		{
			WebErrorHelper.ConnectionLost = null;
			isConnectionLost = false;
			WebErrorHelper.SendErrorMessage = null;
			WebErrorHelper.ViewErrorMessage = null;
			SupportForm = null;
			AppServerName = "Axolon Server";
		}
	}
}
