using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	public sealed class ErrorEvent : EventArgs
	{
		public string ErrorSubject;

		public string Memo;

		public Exception exception;

		public SqlException sqlException;

		public bool IsRightToLeft;

		public ErrorTypes ErrorType;

		public MessageBoxButtons MessageBoxButton;

		public Form ErrorOwner;

		public bool isShowSendButton = true;

		public ErrorEvent()
		{
		}

		public ErrorEvent(string errorSubject, string memo)
		{
			ErrorSubject = errorSubject;
			Memo = memo;
		}

		public ErrorEvent(string errorSubject, string memo, Exception exception)
		{
			ErrorSubject = errorSubject;
			Memo = memo;
			this.exception = exception;
		}

		public ErrorEvent(string errorSubject, string memo, SqlException sqlException)
		{
			ErrorSubject = errorSubject;
			Memo = memo;
			this.sqlException = sqlException;
		}
	}
}
