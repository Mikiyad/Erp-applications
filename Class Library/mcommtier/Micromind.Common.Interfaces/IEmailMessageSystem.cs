using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmailMessageSystem
	{
		bool CreateEmailMessage(EmailMessageData degreeData);

		bool UpdateEmailMessage(EmailMessageData degreeData);

		EmailMessageData GetEmailMessage();

		bool DeleteEmailMessage(string ID);

		EmailMessageData GetEmailMessageByID(string id);

		DataSet GetEmailMessageByFields(params string[] columns);

		DataSet GetEmailMessageByFields(string[] ids, params string[] columns);

		DataSet GetEmailMessageByFields(string[] ids, bool isInactive, params string[] columns);

		bool ProcessEmails();

		DataSet GetEmailMessageList(EmailMessageTypes type, bool includeSent);

		DataSet GetEmailMessageComboList();
	}
}
