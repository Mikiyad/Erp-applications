using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmailMessageSystem : MarshalByRefObject, IEmailMessageSystem, IDisposable
	{
		private Config config;

		public EmailMessageSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmailMessage(EmailMessageData data)
		{
			return new EmailMessage(config).InsertEmailMessage(data);
		}

		public bool UpdateEmailMessage(EmailMessageData data)
		{
			return UpdateEmailMessage(data, checkConcurrency: false);
		}

		public bool UpdateEmailMessage(EmailMessageData data, bool checkConcurrency)
		{
			return new EmailMessage(config).UpdateEmailMessage(data);
		}

		public EmailMessageData GetEmailMessage()
		{
			using (EmailMessage emailMessage = new EmailMessage(config))
			{
				return emailMessage.GetEmailMessage();
			}
		}

		public bool DeleteEmailMessage(string groupID)
		{
			using (EmailMessage emailMessage = new EmailMessage(config))
			{
				return emailMessage.DeleteEmailMessage(groupID);
			}
		}

		public EmailMessageData GetEmailMessageByID(string id)
		{
			using (EmailMessage emailMessage = new EmailMessage(config))
			{
				return emailMessage.GetEmailMessageByID(id);
			}
		}

		public DataSet GetEmailMessageByFields(params string[] columns)
		{
			using (EmailMessage emailMessage = new EmailMessage(config))
			{
				return emailMessage.GetEmailMessageByFields(columns);
			}
		}

		public DataSet GetEmailMessageByFields(string[] ids, params string[] columns)
		{
			using (EmailMessage emailMessage = new EmailMessage(config))
			{
				return emailMessage.GetEmailMessageByFields(ids, columns);
			}
		}

		public DataSet GetEmailMessageByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmailMessage emailMessage = new EmailMessage(config))
			{
				return emailMessage.GetEmailMessageByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmailMessageList(EmailMessageTypes type, bool includeSent)
		{
			using (EmailMessage emailMessage = new EmailMessage(config))
			{
				return emailMessage.GetEmailMessageList(type, includeSent);
			}
		}

		public DataSet GetEmailMessageComboList()
		{
			using (EmailMessage emailMessage = new EmailMessage(config))
			{
				return emailMessage.GetEmailMessageComboList();
			}
		}

		public bool ProcessEmails()
		{
			using (EmailMessage emailMessage = new EmailMessage(config))
			{
				return emailMessage.ProcessEmails();
			}
		}
	}
}
