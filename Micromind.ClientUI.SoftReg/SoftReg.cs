using Micromind.ClientUI.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace Micromind.ClientUI.SoftReg
{
	[GeneratedCode("System.Web.Services", "4.6.1590.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "SoftRegSoap", Namespace = "http://www.starasoft.com/softregws")]
	public class SoftReg : SoapHttpClientProtocol
	{
		private SendOrPostCallback HelloWorldOperationCompleted;

		private SendOrPostCallback GetActivationCodeOperationCompleted;

		private SendOrPostCallback CanConnectOperationCompleted;

		private SendOrPostCallback RegisterTrialSummary2OperationCompleted;

		private SendOrPostCallback RegisterTrialSummaryOperationCompleted;

		private SendOrPostCallback RegisterTrialDetailOperationCompleted;

		private SendOrPostCallback RegisterTrialDetail2OperationCompleted;

		private SendOrPostCallback SaveFeedbackOperationCompleted;

		private SendOrPostCallback CheckForUpdateOperationCompleted;

		private SendOrPostCallback GetRegisterationListOperationCompleted;

		private SendOrPostCallback GetFeedbackListOperationCompleted;

		private SendOrPostCallback SayHelloOperationCompleted;

		private SendOrPostCallback SendErrorOperationCompleted;

		private SendOrPostCallback RequestCLOperationCompleted;

		private SendOrPostCallback CancelCLRequestOperationCompleted;

		private SendOrPostCallback GetCLTokenOperationCompleted;

		private SendOrPostCallback CreateConnectionOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (IsLocalFileSystemWebService(base.Url) && !useDefaultCredentialsSetExplicitly && !IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		public event HelloWorldCompletedEventHandler HelloWorldCompleted;

		public event GetActivationCodeCompletedEventHandler GetActivationCodeCompleted;

		public event CanConnectCompletedEventHandler CanConnectCompleted;

		public event RegisterTrialSummary2CompletedEventHandler RegisterTrialSummary2Completed;

		public event RegisterTrialSummaryCompletedEventHandler RegisterTrialSummaryCompleted;

		public event RegisterTrialDetailCompletedEventHandler RegisterTrialDetailCompleted;

		public event RegisterTrialDetail2CompletedEventHandler RegisterTrialDetail2Completed;

		public event SaveFeedbackCompletedEventHandler SaveFeedbackCompleted;

		public event CheckForUpdateCompletedEventHandler CheckForUpdateCompleted;

		public event GetRegisterationListCompletedEventHandler GetRegisterationListCompleted;

		public event GetFeedbackListCompletedEventHandler GetFeedbackListCompleted;

		public event SayHelloCompletedEventHandler SayHelloCompleted;

		public event SendErrorCompletedEventHandler SendErrorCompleted;

		public event RequestCLCompletedEventHandler RequestCLCompleted;

		public event CancelCLRequestCompletedEventHandler CancelCLRequestCompleted;

		public event GetCLTokenCompletedEventHandler GetCLTokenCompleted;

		public event CreateConnectionCompletedEventHandler CreateConnectionCompleted;

		public SoftReg()
		{
			Url = Settings.Default.muiapi_LocalReg_SoftReg;
			if (IsLocalFileSystemWebService(Url))
			{
				UseDefaultCredentials = true;
				useDefaultCredentialsSetExplicitly = false;
			}
			else
			{
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/HelloWorld", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string HelloWorld()
		{
			return (string)Invoke("HelloWorld", new object[0])[0];
		}

		public IAsyncResult BeginHelloWorld(AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("HelloWorld", new object[0], callback, asyncState);
		}

		public string EndHelloWorld(IAsyncResult asyncResult)
		{
			return (string)EndInvoke(asyncResult)[0];
		}

		public void HelloWorldAsync()
		{
			HelloWorldAsync(null);
		}

		public void HelloWorldAsync(object userState)
		{
			if (HelloWorldOperationCompleted == null)
			{
				HelloWorldOperationCompleted = OnHelloWorldOperationCompleted;
			}
			InvokeAsync("HelloWorld", new object[0], HelloWorldOperationCompleted, userState);
		}

		private void OnHelloWorldOperationCompleted(object arg)
		{
			if (this.HelloWorldCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/GetActivationCode", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string GetActivationCode(string autCode, string productID, string productKey, string systemKey, string ipAddress, string computerName, string firstName, string lastName, string companyName, string email, string telephone, string city, string country)
		{
			return (string)Invoke("GetActivationCode", new object[13]
			{
				autCode,
				productID,
				productKey,
				systemKey,
				ipAddress,
				computerName,
				firstName,
				lastName,
				companyName,
				email,
				telephone,
				city,
				country
			})[0];
		}

		public IAsyncResult BeginGetActivationCode(string autCode, string productID, string productKey, string systemKey, string ipAddress, string computerName, string firstName, string lastName, string companyName, string email, string telephone, string city, string country, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("GetActivationCode", new object[13]
			{
				autCode,
				productID,
				productKey,
				systemKey,
				ipAddress,
				computerName,
				firstName,
				lastName,
				companyName,
				email,
				telephone,
				city,
				country
			}, callback, asyncState);
		}

		public string EndGetActivationCode(IAsyncResult asyncResult)
		{
			return (string)EndInvoke(asyncResult)[0];
		}

		public void GetActivationCodeAsync(string autCode, string productID, string productKey, string systemKey, string ipAddress, string computerName, string firstName, string lastName, string companyName, string email, string telephone, string city, string country)
		{
			GetActivationCodeAsync(autCode, productID, productKey, systemKey, ipAddress, computerName, firstName, lastName, companyName, email, telephone, city, country, null);
		}

		public void GetActivationCodeAsync(string autCode, string productID, string productKey, string systemKey, string ipAddress, string computerName, string firstName, string lastName, string companyName, string email, string telephone, string city, string country, object userState)
		{
			if (GetActivationCodeOperationCompleted == null)
			{
				GetActivationCodeOperationCompleted = OnGetActivationCodeOperationCompleted;
			}
			InvokeAsync("GetActivationCode", new object[13]
			{
				autCode,
				productID,
				productKey,
				systemKey,
				ipAddress,
				computerName,
				firstName,
				lastName,
				companyName,
				email,
				telephone,
				city,
				country
			}, GetActivationCodeOperationCompleted, userState);
		}

		private void OnGetActivationCodeOperationCompleted(object arg)
		{
			if (this.GetActivationCodeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetActivationCodeCompleted(this, new GetActivationCodeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/CanConnect", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string CanConnect(string path)
		{
			return (string)Invoke("CanConnect", new object[1]
			{
				path
			})[0];
		}

		public IAsyncResult BeginCanConnect(string path, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("CanConnect", new object[1]
			{
				path
			}, callback, asyncState);
		}

		public string EndCanConnect(IAsyncResult asyncResult)
		{
			return (string)EndInvoke(asyncResult)[0];
		}

		public void CanConnectAsync(string path)
		{
			CanConnectAsync(path, null);
		}

		public void CanConnectAsync(string path, object userState)
		{
			if (CanConnectOperationCompleted == null)
			{
				CanConnectOperationCompleted = OnCanConnectOperationCompleted;
			}
			InvokeAsync("CanConnect", new object[1]
			{
				path
			}, CanConnectOperationCompleted, userState);
		}

		private void OnCanConnectOperationCompleted(object arg)
		{
			if (this.CanConnectCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CanConnectCompleted(this, new CanConnectCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/RegisterTrialSummary2", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string RegisterTrialSummary2(string autCode, string productID, string productKey, string ipAddress, string computerName, string osName, string recordType, string note1, string note2)
		{
			return (string)Invoke("RegisterTrialSummary2", new object[9]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName,
				osName,
				recordType,
				note1,
				note2
			})[0];
		}

		public IAsyncResult BeginRegisterTrialSummary2(string autCode, string productID, string productKey, string ipAddress, string computerName, string osName, string recordType, string note1, string note2, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("RegisterTrialSummary2", new object[9]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName,
				osName,
				recordType,
				note1,
				note2
			}, callback, asyncState);
		}

		public string EndRegisterTrialSummary2(IAsyncResult asyncResult)
		{
			return (string)EndInvoke(asyncResult)[0];
		}

		public void RegisterTrialSummary2Async(string autCode, string productID, string productKey, string ipAddress, string computerName, string osName, string recordType, string note1, string note2)
		{
			RegisterTrialSummary2Async(autCode, productID, productKey, ipAddress, computerName, osName, recordType, note1, note2, null);
		}

		public void RegisterTrialSummary2Async(string autCode, string productID, string productKey, string ipAddress, string computerName, string osName, string recordType, string note1, string note2, object userState)
		{
			if (RegisterTrialSummary2OperationCompleted == null)
			{
				RegisterTrialSummary2OperationCompleted = OnRegisterTrialSummary2OperationCompleted;
			}
			InvokeAsync("RegisterTrialSummary2", new object[9]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName,
				osName,
				recordType,
				note1,
				note2
			}, RegisterTrialSummary2OperationCompleted, userState);
		}

		private void OnRegisterTrialSummary2OperationCompleted(object arg)
		{
			if (this.RegisterTrialSummary2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RegisterTrialSummary2Completed(this, new RegisterTrialSummary2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/RegisterTrialSummary", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string RegisterTrialSummary(string autCode, string productID, string productKey, string ipAddress, string computerName)
		{
			return (string)Invoke("RegisterTrialSummary", new object[5]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName
			})[0];
		}

		public IAsyncResult BeginRegisterTrialSummary(string autCode, string productID, string productKey, string ipAddress, string computerName, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("RegisterTrialSummary", new object[5]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName
			}, callback, asyncState);
		}

		public string EndRegisterTrialSummary(IAsyncResult asyncResult)
		{
			return (string)EndInvoke(asyncResult)[0];
		}

		public void RegisterTrialSummaryAsync(string autCode, string productID, string productKey, string ipAddress, string computerName)
		{
			RegisterTrialSummaryAsync(autCode, productID, productKey, ipAddress, computerName, null);
		}

		public void RegisterTrialSummaryAsync(string autCode, string productID, string productKey, string ipAddress, string computerName, object userState)
		{
			if (RegisterTrialSummaryOperationCompleted == null)
			{
				RegisterTrialSummaryOperationCompleted = OnRegisterTrialSummaryOperationCompleted;
			}
			InvokeAsync("RegisterTrialSummary", new object[5]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName
			}, RegisterTrialSummaryOperationCompleted, userState);
		}

		private void OnRegisterTrialSummaryOperationCompleted(object arg)
		{
			if (this.RegisterTrialSummaryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RegisterTrialSummaryCompleted(this, new RegisterTrialSummaryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/RegisterTrialDetail", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string RegisterTrialDetail(string autCode, string productID, string productKey, string ipAddress, string computerName, string firstName, string lastName, string companyName, string email, string telephone, string city, string country)
		{
			return (string)Invoke("RegisterTrialDetail", new object[12]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName,
				firstName,
				lastName,
				companyName,
				email,
				telephone,
				city,
				country
			})[0];
		}

		public IAsyncResult BeginRegisterTrialDetail(string autCode, string productID, string productKey, string ipAddress, string computerName, string firstName, string lastName, string companyName, string email, string telephone, string city, string country, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("RegisterTrialDetail", new object[12]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName,
				firstName,
				lastName,
				companyName,
				email,
				telephone,
				city,
				country
			}, callback, asyncState);
		}

		public string EndRegisterTrialDetail(IAsyncResult asyncResult)
		{
			return (string)EndInvoke(asyncResult)[0];
		}

		public void RegisterTrialDetailAsync(string autCode, string productID, string productKey, string ipAddress, string computerName, string firstName, string lastName, string companyName, string email, string telephone, string city, string country)
		{
			RegisterTrialDetailAsync(autCode, productID, productKey, ipAddress, computerName, firstName, lastName, companyName, email, telephone, city, country, null);
		}

		public void RegisterTrialDetailAsync(string autCode, string productID, string productKey, string ipAddress, string computerName, string firstName, string lastName, string companyName, string email, string telephone, string city, string country, object userState)
		{
			if (RegisterTrialDetailOperationCompleted == null)
			{
				RegisterTrialDetailOperationCompleted = OnRegisterTrialDetailOperationCompleted;
			}
			InvokeAsync("RegisterTrialDetail", new object[12]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName,
				firstName,
				lastName,
				companyName,
				email,
				telephone,
				city,
				country
			}, RegisterTrialDetailOperationCompleted, userState);
		}

		private void OnRegisterTrialDetailOperationCompleted(object arg)
		{
			if (this.RegisterTrialDetailCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RegisterTrialDetailCompleted(this, new RegisterTrialDetailCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/RegisterTrialDetail2", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string RegisterTrialDetail2(string autCode, string productID, string productKey, string ipAddress, string computerName, string osName, string recordType, string note1, string note2, string firstName, string lastName, string companyName, string email, string telephone, string city, string country)
		{
			return (string)Invoke("RegisterTrialDetail2", new object[16]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName,
				osName,
				recordType,
				note1,
				note2,
				firstName,
				lastName,
				companyName,
				email,
				telephone,
				city,
				country
			})[0];
		}

		public IAsyncResult BeginRegisterTrialDetail2(string autCode, string productID, string productKey, string ipAddress, string computerName, string osName, string recordType, string note1, string note2, string firstName, string lastName, string companyName, string email, string telephone, string city, string country, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("RegisterTrialDetail2", new object[16]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName,
				osName,
				recordType,
				note1,
				note2,
				firstName,
				lastName,
				companyName,
				email,
				telephone,
				city,
				country
			}, callback, asyncState);
		}

		public string EndRegisterTrialDetail2(IAsyncResult asyncResult)
		{
			return (string)EndInvoke(asyncResult)[0];
		}

		public void RegisterTrialDetail2Async(string autCode, string productID, string productKey, string ipAddress, string computerName, string osName, string recordType, string note1, string note2, string firstName, string lastName, string companyName, string email, string telephone, string city, string country)
		{
			RegisterTrialDetail2Async(autCode, productID, productKey, ipAddress, computerName, osName, recordType, note1, note2, firstName, lastName, companyName, email, telephone, city, country, null);
		}

		public void RegisterTrialDetail2Async(string autCode, string productID, string productKey, string ipAddress, string computerName, string osName, string recordType, string note1, string note2, string firstName, string lastName, string companyName, string email, string telephone, string city, string country, object userState)
		{
			if (RegisterTrialDetail2OperationCompleted == null)
			{
				RegisterTrialDetail2OperationCompleted = OnRegisterTrialDetail2OperationCompleted;
			}
			InvokeAsync("RegisterTrialDetail2", new object[16]
			{
				autCode,
				productID,
				productKey,
				ipAddress,
				computerName,
				osName,
				recordType,
				note1,
				note2,
				firstName,
				lastName,
				companyName,
				email,
				telephone,
				city,
				country
			}, RegisterTrialDetail2OperationCompleted, userState);
		}

		private void OnRegisterTrialDetail2OperationCompleted(object arg)
		{
			if (this.RegisterTrialDetail2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RegisterTrialDetail2Completed(this, new RegisterTrialDetail2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/SaveFeedback", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool SaveFeedback(string productID, string productKey, string name, string email, DateTime date, string memo)
		{
			return (bool)Invoke("SaveFeedback", new object[6]
			{
				productID,
				productKey,
				name,
				email,
				date,
				memo
			})[0];
		}

		public IAsyncResult BeginSaveFeedback(string productID, string productKey, string name, string email, DateTime date, string memo, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("SaveFeedback", new object[6]
			{
				productID,
				productKey,
				name,
				email,
				date,
				memo
			}, callback, asyncState);
		}

		public bool EndSaveFeedback(IAsyncResult asyncResult)
		{
			return (bool)EndInvoke(asyncResult)[0];
		}

		public void SaveFeedbackAsync(string productID, string productKey, string name, string email, DateTime date, string memo)
		{
			SaveFeedbackAsync(productID, productKey, name, email, date, memo, null);
		}

		public void SaveFeedbackAsync(string productID, string productKey, string name, string email, DateTime date, string memo, object userState)
		{
			if (SaveFeedbackOperationCompleted == null)
			{
				SaveFeedbackOperationCompleted = OnSaveFeedbackOperationCompleted;
			}
			InvokeAsync("SaveFeedback", new object[6]
			{
				productID,
				productKey,
				name,
				email,
				date,
				memo
			}, SaveFeedbackOperationCompleted, userState);
		}

		private void OnSaveFeedbackOperationCompleted(object arg)
		{
			if (this.SaveFeedbackCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SaveFeedbackCompleted(this, new SaveFeedbackCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/CheckForUpdate", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool CheckForUpdate(string productID, string currentVersion)
		{
			return (bool)Invoke("CheckForUpdate", new object[2]
			{
				productID,
				currentVersion
			})[0];
		}

		public IAsyncResult BeginCheckForUpdate(string productID, string currentVersion, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("CheckForUpdate", new object[2]
			{
				productID,
				currentVersion
			}, callback, asyncState);
		}

		public bool EndCheckForUpdate(IAsyncResult asyncResult)
		{
			return (bool)EndInvoke(asyncResult)[0];
		}

		public void CheckForUpdateAsync(string productID, string currentVersion)
		{
			CheckForUpdateAsync(productID, currentVersion, null);
		}

		public void CheckForUpdateAsync(string productID, string currentVersion, object userState)
		{
			if (CheckForUpdateOperationCompleted == null)
			{
				CheckForUpdateOperationCompleted = OnCheckForUpdateOperationCompleted;
			}
			InvokeAsync("CheckForUpdate", new object[2]
			{
				productID,
				currentVersion
			}, CheckForUpdateOperationCompleted, userState);
		}

		private void OnCheckForUpdateOperationCompleted(object arg)
		{
			if (this.CheckForUpdateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckForUpdateCompleted(this, new CheckForUpdateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/GetRegisterationList", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public DataSet GetRegisterationList(string key, DateTime from, DateTime to)
		{
			return (DataSet)Invoke("GetRegisterationList", new object[3]
			{
				key,
				from,
				to
			})[0];
		}

		public IAsyncResult BeginGetRegisterationList(string key, DateTime from, DateTime to, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("GetRegisterationList", new object[3]
			{
				key,
				from,
				to
			}, callback, asyncState);
		}

		public DataSet EndGetRegisterationList(IAsyncResult asyncResult)
		{
			return (DataSet)EndInvoke(asyncResult)[0];
		}

		public void GetRegisterationListAsync(string key, DateTime from, DateTime to)
		{
			GetRegisterationListAsync(key, from, to, null);
		}

		public void GetRegisterationListAsync(string key, DateTime from, DateTime to, object userState)
		{
			if (GetRegisterationListOperationCompleted == null)
			{
				GetRegisterationListOperationCompleted = OnGetRegisterationListOperationCompleted;
			}
			InvokeAsync("GetRegisterationList", new object[3]
			{
				key,
				from,
				to
			}, GetRegisterationListOperationCompleted, userState);
		}

		private void OnGetRegisterationListOperationCompleted(object arg)
		{
			if (this.GetRegisterationListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRegisterationListCompleted(this, new GetRegisterationListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/GetFeedbackList", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public DataSet GetFeedbackList(string key)
		{
			return (DataSet)Invoke("GetFeedbackList", new object[1]
			{
				key
			})[0];
		}

		public IAsyncResult BeginGetFeedbackList(string key, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("GetFeedbackList", new object[1]
			{
				key
			}, callback, asyncState);
		}

		public DataSet EndGetFeedbackList(IAsyncResult asyncResult)
		{
			return (DataSet)EndInvoke(asyncResult)[0];
		}

		public void GetFeedbackListAsync(string key)
		{
			GetFeedbackListAsync(key, null);
		}

		public void GetFeedbackListAsync(string key, object userState)
		{
			if (GetFeedbackListOperationCompleted == null)
			{
				GetFeedbackListOperationCompleted = OnGetFeedbackListOperationCompleted;
			}
			InvokeAsync("GetFeedbackList", new object[1]
			{
				key
			}, GetFeedbackListOperationCompleted, userState);
		}

		private void OnGetFeedbackListOperationCompleted(object arg)
		{
			if (this.GetFeedbackListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetFeedbackListCompleted(this, new GetFeedbackListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/SayHello", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string SayHello()
		{
			return (string)Invoke("SayHello", new object[0])[0];
		}

		public IAsyncResult BeginSayHello(AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("SayHello", new object[0], callback, asyncState);
		}

		public string EndSayHello(IAsyncResult asyncResult)
		{
			return (string)EndInvoke(asyncResult)[0];
		}

		public void SayHelloAsync()
		{
			SayHelloAsync(null);
		}

		public void SayHelloAsync(object userState)
		{
			if (SayHelloOperationCompleted == null)
			{
				SayHelloOperationCompleted = OnSayHelloOperationCompleted;
			}
			InvokeAsync("SayHello", new object[0], SayHelloOperationCompleted, userState);
		}

		private void OnSayHelloOperationCompleted(object arg)
		{
			if (this.SayHelloCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SayHelloCompleted(this, new SayHelloCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/SendError", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool SendError(string productID, string productKey, string computerName, string errorMessage)
		{
			return (bool)Invoke("SendError", new object[4]
			{
				productID,
				productKey,
				computerName,
				errorMessage
			})[0];
		}

		public IAsyncResult BeginSendError(string productID, string productKey, string computerName, string errorMessage, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("SendError", new object[4]
			{
				productID,
				productKey,
				computerName,
				errorMessage
			}, callback, asyncState);
		}

		public bool EndSendError(IAsyncResult asyncResult)
		{
			return (bool)EndInvoke(asyncResult)[0];
		}

		public void SendErrorAsync(string productID, string productKey, string computerName, string errorMessage)
		{
			SendErrorAsync(productID, productKey, computerName, errorMessage, null);
		}

		public void SendErrorAsync(string productID, string productKey, string computerName, string errorMessage, object userState)
		{
			if (SendErrorOperationCompleted == null)
			{
				SendErrorOperationCompleted = OnSendErrorOperationCompleted;
			}
			InvokeAsync("SendError", new object[4]
			{
				productID,
				productKey,
				computerName,
				errorMessage
			}, SendErrorOperationCompleted, userState);
		}

		private void OnSendErrorOperationCompleted(object arg)
		{
			if (this.SendErrorCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SendErrorCompleted(this, new SendErrorCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/RequestCL", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool RequestCL(int companyID, string systemKey, string customerID, string customerName, decimal amount, string userID, decimal currentLimit, decimal balance, string xString)
		{
			return (bool)Invoke("RequestCL", new object[9]
			{
				companyID,
				systemKey,
				customerID,
				customerName,
				amount,
				userID,
				currentLimit,
				balance,
				xString
			})[0];
		}

		public IAsyncResult BeginRequestCL(int companyID, string systemKey, string customerID, string customerName, decimal amount, string userID, decimal currentLimit, decimal balance, string xString, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("RequestCL", new object[9]
			{
				companyID,
				systemKey,
				customerID,
				customerName,
				amount,
				userID,
				currentLimit,
				balance,
				xString
			}, callback, asyncState);
		}

		public bool EndRequestCL(IAsyncResult asyncResult)
		{
			return (bool)EndInvoke(asyncResult)[0];
		}

		public void RequestCLAsync(int companyID, string systemKey, string customerID, string customerName, decimal amount, string userID, decimal currentLimit, decimal balance, string xString)
		{
			RequestCLAsync(companyID, systemKey, customerID, customerName, amount, userID, currentLimit, balance, xString, null);
		}

		public void RequestCLAsync(int companyID, string systemKey, string customerID, string customerName, decimal amount, string userID, decimal currentLimit, decimal balance, string xString, object userState)
		{
			if (RequestCLOperationCompleted == null)
			{
				RequestCLOperationCompleted = OnRequestCLOperationCompleted;
			}
			InvokeAsync("RequestCL", new object[9]
			{
				companyID,
				systemKey,
				customerID,
				customerName,
				amount,
				userID,
				currentLimit,
				balance,
				xString
			}, RequestCLOperationCompleted, userState);
		}

		private void OnRequestCLOperationCompleted(object arg)
		{
			if (this.RequestCLCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RequestCLCompleted(this, new RequestCLCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/CancelCLRequest", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool CancelCLRequest(int companyID, string systemKey)
		{
			return (bool)Invoke("CancelCLRequest", new object[2]
			{
				companyID,
				systemKey
			})[0];
		}

		public IAsyncResult BeginCancelCLRequest(int companyID, string systemKey, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("CancelCLRequest", new object[2]
			{
				companyID,
				systemKey
			}, callback, asyncState);
		}

		public bool EndCancelCLRequest(IAsyncResult asyncResult)
		{
			return (bool)EndInvoke(asyncResult)[0];
		}

		public void CancelCLRequestAsync(int companyID, string systemKey)
		{
			CancelCLRequestAsync(companyID, systemKey, null);
		}

		public void CancelCLRequestAsync(int companyID, string systemKey, object userState)
		{
			if (CancelCLRequestOperationCompleted == null)
			{
				CancelCLRequestOperationCompleted = OnCancelCLRequestOperationCompleted;
			}
			InvokeAsync("CancelCLRequest", new object[2]
			{
				companyID,
				systemKey
			}, CancelCLRequestOperationCompleted, userState);
		}

		private void OnCancelCLRequestOperationCompleted(object arg)
		{
			if (this.CancelCLRequestCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CancelCLRequestCompleted(this, new CancelCLRequestCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/GetCLToken", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public DataSet GetCLToken(int companyID, string systemKey)
		{
			return (DataSet)Invoke("GetCLToken", new object[2]
			{
				companyID,
				systemKey
			})[0];
		}

		public IAsyncResult BeginGetCLToken(int companyID, string systemKey, AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("GetCLToken", new object[2]
			{
				companyID,
				systemKey
			}, callback, asyncState);
		}

		public DataSet EndGetCLToken(IAsyncResult asyncResult)
		{
			return (DataSet)EndInvoke(asyncResult)[0];
		}

		public void GetCLTokenAsync(int companyID, string systemKey)
		{
			GetCLTokenAsync(companyID, systemKey, null);
		}

		public void GetCLTokenAsync(int companyID, string systemKey, object userState)
		{
			if (GetCLTokenOperationCompleted == null)
			{
				GetCLTokenOperationCompleted = OnGetCLTokenOperationCompleted;
			}
			InvokeAsync("GetCLToken", new object[2]
			{
				companyID,
				systemKey
			}, GetCLTokenOperationCompleted, userState);
		}

		private void OnGetCLTokenOperationCompleted(object arg)
		{
			if (this.GetCLTokenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCLTokenCompleted(this, new GetCLTokenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://www.starasoft.com/softregws/CreateConnection", RequestNamespace = "http://www.starasoft.com/softregws", ResponseNamespace = "http://www.starasoft.com/softregws", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool CreateConnection()
		{
			return (bool)Invoke("CreateConnection", new object[0])[0];
		}

		public IAsyncResult BeginCreateConnection(AsyncCallback callback, object asyncState)
		{
			return BeginInvoke("CreateConnection", new object[0], callback, asyncState);
		}

		public bool EndCreateConnection(IAsyncResult asyncResult)
		{
			return (bool)EndInvoke(asyncResult)[0];
		}

		public void CreateConnectionAsync()
		{
			CreateConnectionAsync(null);
		}

		public void CreateConnectionAsync(object userState)
		{
			if (CreateConnectionOperationCompleted == null)
			{
				CreateConnectionOperationCompleted = OnCreateConnectionOperationCompleted;
			}
			InvokeAsync("CreateConnection", new object[0], CreateConnectionOperationCompleted, userState);
		}

		private void OnCreateConnectionOperationCompleted(object arg)
		{
			if (this.CreateConnectionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateConnectionCompleted(this, new CreateConnectionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			if (url == null || url == string.Empty)
			{
				return false;
			}
			Uri uri = new Uri(url);
			if (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			return false;
		}
	}
}
