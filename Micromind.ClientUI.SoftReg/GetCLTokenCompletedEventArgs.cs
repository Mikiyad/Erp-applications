using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Micromind.ClientUI.SoftReg
{
	[GeneratedCode("System.Web.Services", "4.6.1590.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetCLTokenCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public DataSet Result
		{
			get
			{
				RaiseExceptionIfNecessary();
				return (DataSet)results[0];
			}
		}

		internal GetCLTokenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
