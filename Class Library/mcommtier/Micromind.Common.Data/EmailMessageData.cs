using System;
using System.ComponentModel;
using System.Data;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmailMessageData : DataSet
	{
		public const string MESSAGEID_FIELD = "MessageID";

		public const string SUBJECT_FIELD = "Subject";

		public const string PERIODFROM_FIELD = "PeriodFrom";

		public const string PERIODTO_FIELD = "PeriodTo";

		public const string CONFIGTYPE_FIELD = "ConfigType";

		public const string MESSAGETYPE_FIELD = "MessageType";

		public const string SENDERADDRESS_FIELD = "SenderAddress";

		public const string SENDERNAME_FIELD = "SenderName";

		public const string USERID_FIELD = "UserID";

		public const string RECIPIENTADDRESS_FIELD = "RecipientAddress";

		public const string EMAILBODY_FIELD = "EmailBody";

		public const string CCADDRESS_FIELD = "CCAddress";

		public const string BCCADDRESS_FIELD = "BCCAddress";

		public const string ATTACHMENT_FIELD = "Attachment";

		public const string ATTACHMENTNAME_FIELD = "AttachmentName";

		public const string PARTYTYPE_FIELD = "PartyType";

		public const string PARTYID_FIELD = "PartyID";

		public const string EMAILDATE_FIELD = "EmailDate";

		public const string STATUS_FIELD = "Status";

		public const string AMOUNT_FIELD = "Amount";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EMAILMESSAGE_TABLE = "Email_Message";

		public DataTable EmailMessageTable => base.Tables["Email_Message"];

		public EmailMessageData()
		{
			BuildDataTables();
		}

		public void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Email_Message");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("MessageID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Subject", typeof(string));
			columns.Add("PeriodFrom", typeof(DateTime));
			columns.Add("PeriodTo", typeof(DateTime));
			columns.Add("EmailDate", typeof(DateTime));
			columns.Add("ConfigType", typeof(byte));
			columns.Add("MessageType", typeof(byte));
			columns.Add("Status", typeof(byte));
			columns.Add("SenderAddress", typeof(string));
			columns.Add("UserID", typeof(string));
			columns.Add("SenderName", typeof(string));
			columns.Add("RecipientAddress", typeof(string));
			columns.Add("EmailBody", typeof(string));
			columns.Add("CCAddress", typeof(string));
			columns.Add("PartyType", typeof(string));
			columns.Add("PartyID", typeof(string));
			columns.Add("BCCAddress", typeof(string));
			columns.Add("Attachment", typeof(byte[]));
			columns.Add("AttachmentName", typeof(string));
			columns.Add("Amount", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
