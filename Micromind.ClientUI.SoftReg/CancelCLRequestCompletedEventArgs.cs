using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Micromind.ClientUI.SoftReg
{
	[GeneratedCode("System.Web.Services", "4.6.1590.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CancelCLRequestCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public bool Result
		{
			get
			{
				RaiseExceptionIfNecessary();
				return (bool)results[0];
			}
		}

		internal CancelCLRequestCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
