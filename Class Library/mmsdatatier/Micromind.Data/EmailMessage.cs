using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Micromind.Data
{
	public sealed class EmailMessage : StoreObject
	{
		private static bool IsProcessingEmails;

		private const string MESSAGEID_PARM = "@MessageID";

		private const string SUBJECT_PARM = "@Subject";

		private const string PERIODFROM_PARM = "@PeriodFrom";

		private const string PERIODTO_PARM = "@PeriodTo";

		private const string CONFIGTYPE_PARM = "@ConfigType";

		private const string MESSAGETYPE_PARM = "@MessageType";

		private const string SENDERADDRESS_PARM = "@SenderAddress";

		private const string SENDERNAME_PARM = "@SenderName";

		private const string RECIPIENTADDRESS_PARM = "@RecipientAddress";

		private const string EMAILBODY_PARM = "@EmailBody";

		private const string CCADDRESS_PARM = "@CCAddress";

		private const string STATUS_PARM = "@Status";

		private const string BCCADDRESS_PARM = "@BCCAddress";

		private const string PARTYTYPE_PARM = "@PartyType";

		private const string PARTYID_PARM = "@PartyID";

		private const string EMAILDATE_PARM = "@EmailDate";

		private const string USERID_PARM = "@UserID";

		private const string ATTACHMENT_PARM = "@Attachment";

		private const string ATTACHMENTNAME_PARM = "@AttachmentName";

		private const string AMOUNT_PARM = "@Amount";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmailMessage(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Email_Message", new FieldValue("Subject", "@Subject"), new FieldValue("PeriodFrom", "@PeriodFrom"), new FieldValue("PeriodTo", "@PeriodTo"), new FieldValue("EmailDate", "@EmailDate"), new FieldValue("Status", "@Status"), new FieldValue("ConfigType", "@ConfigType"), new FieldValue("MessageType", "@MessageType"), new FieldValue("SenderAddress", "@SenderAddress"), new FieldValue("SenderName", "@SenderName"), new FieldValue("RecipientAddress", "@RecipientAddress"), new FieldValue("UserID", "@UserID"), new FieldValue("EmailBody", "@EmailBody"), new FieldValue("CCAddress", "@CCAddress"), new FieldValue("BCCAddress", "@BCCAddress"), new FieldValue("PartyType", "@PartyType"), new FieldValue("PartyID", "@PartyID"), new FieldValue("Attachment", "@Attachment"), new FieldValue("AttachmentName", "@AttachmentName"), new FieldValue("Amount", "@Amount"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Email_Message", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@Subject", SqlDbType.NVarChar);
			parameters.Add("@PeriodFrom", SqlDbType.DateTime);
			parameters.Add("@PeriodTo", SqlDbType.DateTime);
			parameters.Add("@EmailDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@ConfigType", SqlDbType.TinyInt);
			parameters.Add("@MessageType", SqlDbType.TinyInt);
			parameters.Add("@SenderAddress", SqlDbType.NVarChar);
			parameters.Add("@SenderName", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@RecipientAddress", SqlDbType.NVarChar);
			parameters.Add("@EmailBody", SqlDbType.NVarChar);
			parameters.Add("@CCAddress", SqlDbType.NVarChar);
			parameters.Add("@PartyType", SqlDbType.NVarChar);
			parameters.Add("@PartyID", SqlDbType.NVarChar);
			parameters.Add("@BCCAddress", SqlDbType.NVarChar);
			parameters.Add("@Attachment", SqlDbType.VarBinary);
			parameters.Add("@AttachmentName", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters["@Subject"].SourceColumn = "Subject";
			parameters["@PeriodFrom"].SourceColumn = "PeriodFrom";
			parameters["@PeriodTo"].SourceColumn = "PeriodTo";
			parameters["@EmailDate"].SourceColumn = "EmailDate";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@ConfigType"].SourceColumn = "ConfigType";
			parameters["@MessageType"].SourceColumn = "MessageType";
			parameters["@SenderAddress"].SourceColumn = "SenderAddress";
			parameters["@SenderName"].SourceColumn = "SenderName";
			parameters["@RecipientAddress"].SourceColumn = "RecipientAddress";
			parameters["@EmailBody"].SourceColumn = "EmailBody";
			parameters["@CCAddress"].SourceColumn = "CCAddress";
			parameters["@BCCAddress"].SourceColumn = "BCCAddress";
			parameters["@PartyType"].SourceColumn = "PartyType";
			parameters["@PartyID"].SourceColumn = "PartyID";
			parameters["@Attachment"].SourceColumn = "Attachment";
			parameters["@AttachmentName"].SourceColumn = "AttachmentName";
			parameters["@Amount"].SourceColumn = "Amount";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertEmailMessage(EmailMessageData emailMessageData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(emailMessageData, "Email_Message", insertUpdateCommand);
				string text = emailMessageData.EmailMessageTable.Rows[0]["MessageID"].ToString();
				AddActivityLog("EmailMessage", text, "", ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Email_Message", "MessageID", text, sqlTransaction, isInsert: true);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool UpdateEmailMessage(EmailMessageData emailMessageData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(emailMessageData, "Email_Message", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = emailMessageData.EmailMessageTable.Rows[0]["MessageID"];
				UpdateTableRowByID("Email_Message", "MessageID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = emailMessageData.EmailMessageTable.Rows[0]["Subject"].ToString();
				AddActivityLog("Email", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Email_Message", "MessageID", obj, sqlTransaction, isInsert: false);
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

		public EmailMessageData GetEmailMessage()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Email_Message");
			EmailMessageData emailMessageData = new EmailMessageData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(emailMessageData, "Email_Message", sqlBuilder);
			return emailMessageData;
		}

		public bool DeleteEmailMessage(string degreeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Email_Message WHERE MessageID = '" + degreeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("EmailMessage", degreeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmailMessageData GetEmailMessageByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "MessageID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Email_Message";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmailMessageData emailMessageData = new EmailMessageData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(emailMessageData, "Email_Message", sqlBuilder);
			return emailMessageData;
		}

		public DataSet GetEmailMessageByFields(params string[] columns)
		{
			return GetEmailMessageByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmailMessageByFields(string[] degreeID, params string[] columns)
		{
			return GetEmailMessageByFields(degreeID, isInactive: true, columns);
		}

		public DataSet GetEmailMessageByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Email_Message");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "MessageID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Email_Message";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Email_Message", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmailMessageList(EmailMessageTypes type, bool includeSent)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT MessageID ,EmailDate [Date],Subject,RecipientAddress [Recipient],PeriodFrom [From],PeriodTo [To],UserID,CCAddress [CC],Amount,PartyType,PartyID,\r\n                             (CASE PartyType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName\r\n                        ELSE Account.AccountName END) AS PartyName,ISNULL(Msg.Status,1) Status\r\n                        FROM Email_Message MSG LEFt OUTER JOIN \r\n                        Account ON MSG.PartyID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON MSG.PartyID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON MSG.PartyID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON MSG.PartyID=Employee.EmployeeID  WHERE 1=1 ";
			if (type != 0)
			{
				text = text + " AND MessageType = " + (byte)type;
			}
			if (!includeSent)
			{
				text += " AND ISNULL(MSG.Status,1) <> 10";
			}
			FillDataSet(dataSet, "Email_Message", text);
			return dataSet;
		}

		public DataSet GetEmailMessageComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EmailMessageID [Code],EmailMessageName [Name]\r\n                           FROM EmailMessage ORDER BY EmailMessageID,EmailMessageName";
			FillDataSet(dataSet, "Email_Message", textCommand);
			return dataSet;
		}

		public bool ProcessEmails()
		{
			if (IsProcessingEmails)
			{
				return true;
			}
			int num = -1;
			try
			{
				IsProcessingEmails = true;
				string textCommand = "SELECT * FROM Email_Message WHERE ISNULL(Status,1) IN (1,2)";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Email_Message", textCommand);
				new CompanyInformationData();
				CompanyInformationData emailConfig = new CompanyInformations(base.DBConfig).GetEmailConfig(CompanyEmailConfigTypes.None);
				DataRow[] array = emailConfig.EmailConfigTable.Select("EmailID = 1");
				DataRow dataRow = null;
				if (array.Length != 0)
				{
					dataRow = array[0];
				}
				array = emailConfig.EmailConfigTable.Select("EmailID = 2");
				DataRow dataRow2 = null;
				if (array.Length != 0)
				{
					dataRow2 = array[0];
				}
				array = emailConfig.EmailConfigTable.Select("EmailID = 3");
				DataRow dataRow3 = null;
				if (array.Length != 0)
				{
					dataRow3 = array[0];
				}
				array = emailConfig.EmailConfigTable.Select("EmailID = 4");
				DataRow dataRow4 = null;
				if (array.Length != 0)
				{
					dataRow4 = array[0];
				}
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					num = int.Parse(row["MessageID"].ToString());
					DateTime dateTime = DateTime.Now;
					string text = "";
					if (!row["PeriodTo"].IsDBNullOrEmpty())
					{
						dateTime = DateTime.Parse(row["PeriodTo"].ToString());
					}
					string subject = row["Subject"].ToString();
					CompanyEmailConfigTypes companyEmailConfigTypes = (CompanyEmailConfigTypes)int.Parse(row["ConfigType"].ToString());
					EmailMessageTypes emailMessageTypes = (EmailMessageTypes)int.Parse(row["MessageType"].ToString());
					string text2 = "";
					string text3 = row["AttachmentName"].ToString();
					byte[] buffer = null;
					if (!row["AttachmentName"].IsDBNullOrEmpty())
					{
						buffer = (byte[])row["Attachment"];
					}
					string text4 = row["PartyID"].ToString();
					string text5 = row["EmailBody"].ToString();
					string text6 = row["RecipientAddress"].ToString();
					string text7 = "";
					string host = "";
					string userName = "";
					string password = "";
					string text8 = "";
					int result = 25;
					bool result2 = false;
					switch (companyEmailConfigTypes)
					{
					case CompanyEmailConfigTypes.Accounting:
					{
						if (dataRow == null)
						{
							throw new CompanyException("Email is not configured for accounting emails.");
						}
						host = dataRow["OutgoingServer"].ToString();
						text7 = dataRow["EmailAddress"].ToString();
						userName = dataRow["UserName"].ToString();
						password = dataRow["EmailPass"].ToString();
						text8 = dataRow["SenderName"].ToString();
						switch (emailMessageTypes)
						{
						case EmailMessageTypes.Statement:
							text5 = dataRow["Body2"].ToString();
							break;
						case EmailMessageTypes.PaySlip:
							text5 = dataRow["Body3"].ToString();
							break;
						default:
							text5 = dataRow["Body1"].ToString();
							break;
						}
						switch (emailMessageTypes)
						{
						case EmailMessageTypes.Statement:
							text2 = dataRow["CC2"].ToString();
							break;
						case EmailMessageTypes.PaySlip:
							text2 = dataRow["CC3"].ToString();
							break;
						default:
							text2 = dataRow["CC1"].ToString();
							break;
						}
						bool flag = false;
						if (!dataRow["CCSalesperson"].IsDBNullOrEmpty())
						{
							flag = bool.Parse(dataRow["CCSalesperson"].ToString());
						}
						if (flag)
						{
							textCommand = "SELECT Email FROM Salesperson SP INNER JOIN Customer CUS ON CUS.SalesPersonID = SP.SalespersonID\r\n                                        WHERE CUS.CustomerID = '1001' AND ISNULL(EmailStatement,'False')='True'";
							string text9 = ExecuteScalar(textCommand).ToString();
							if (text9 != "")
							{
								text2 = ((!(text2 == "")) ? (text2 + "," + text9) : text9);
							}
						}
						int.TryParse(dataRow["EmailSMTPPort"].ToString(), out result);
						bool.TryParse(dataRow["EmailUseSSL"].ToString(), out result2);
						break;
					}
					case CompanyEmailConfigTypes.Purchasing:
						if (dataRow2 == null)
						{
							throw new CompanyException("Email is not configured for purchasing emails.");
						}
						host = dataRow2["OutgoingServer"].ToString();
						text7 = dataRow2["EmailAddress"].ToString();
						userName = dataRow2["UserName"].ToString();
						password = dataRow2["EmailPass"].ToString();
						text8 = dataRow2["SenderName"].ToString();
						int.TryParse(dataRow2["EmailSMTPPort"].ToString(), out result);
						bool.TryParse(dataRow2["EmailUseSSL"].ToString(), out result2);
						break;
					case CompanyEmailConfigTypes.Sales:
						if (dataRow3 == null)
						{
							throw new CompanyException("Email is not configured for sales emails.");
						}
						host = dataRow3["OutgoingServer"].ToString();
						text7 = dataRow3["EmailAddress"].ToString();
						userName = dataRow3["UserName"].ToString();
						password = dataRow3["EmailPass"].ToString();
						text8 = dataRow3["SenderName"].ToString();
						int.TryParse(dataRow3["EmailSMTPPort"].ToString(), out result);
						bool.TryParse(dataRow3["EmailUseSSL"].ToString(), out result2);
						break;
					case CompanyEmailConfigTypes.Notifications:
						if (dataRow4 == null)
						{
							throw new CompanyException("Email is not configured for notification emails.");
						}
						host = dataRow4["OutgoingServer"].ToString();
						text7 = dataRow4["EmailAddress"].ToString();
						userName = dataRow4["UserName"].ToString();
						password = dataRow4["EmailPass"].ToString();
						text8 = dataRow4["SenderName"].ToString();
						int.TryParse(dataRow4["EmailSMTPPort"].ToString(), out result);
						bool.TryParse(dataRow4["EmailUseSSL"].ToString(), out result2);
						break;
					}
					string text10 = "";
					if (text6.IsNullOrEmpty())
					{
						text10 = "Recipient cannot be empty.";
					}
					else if (text7.IsNullOrEmpty())
					{
						text10 = "Sender address cannot be empty.";
					}
					if (text5.Contains("[NAME]"))
					{
						if (!row["PartyID"].IsDBNullOrEmpty())
						{
							text4 = row["PartyID"].ToString();
						}
						if (text4 != "")
						{
							object fieldValue = new Databases(base.DBConfig).GetFieldValue("Customer", "CustomerName", "CustomerID", text4, null);
							if (!fieldValue.IsDBNullOrEmpty())
							{
								text = fieldValue.ToString();
							}
						}
						if (text != "")
						{
							text5 = text5.Replace("[NAME]", text);
						}
					}
					text5 = text5.Replace("[DATE]", dateTime.ToString("dd, MMMM yyyy"));
					MailMessage mailMessage = new MailMessage();
					if (!text2.IsNullOrEmpty())
					{
						mailMessage.CC.Add(text2);
					}
					mailMessage.Subject = subject;
					mailMessage.IsBodyHtml = false;
					mailMessage.BodyEncoding = Encoding.Unicode;
					mailMessage.Body = text5;
					mailMessage.To.Add(text6);
					mailMessage.From = new MailAddress(text7, text8);
					if (text3 != "")
					{
						Attachment item = new Attachment(new MemoryStream(buffer), text3, "application/pdf");
						mailMessage.Attachments.Add(item);
					}
					SmtpClient smtpClient = new SmtpClient();
					smtpClient.Port = result;
					smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
					smtpClient.UseDefaultCredentials = false;
					smtpClient.Host = host;
					smtpClient.EnableSsl = result2;
					smtpClient.Credentials = new NetworkCredential(userName, password);
					smtpClient.Send(mailMessage);
					text10 = "Successful";
					textCommand = "UPDATE Email_Message SET SenderAddress='" + text7 + "',StatusMessage = '" + text10 + "', SenderName = '" + text8 + "',Status = 10 WHERE MessageID = " + num;
					ExecuteNonQuery(textCommand);
				}
				return true;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteError("Error in sending email. Email ID:" + num.ToString() + "\n" + ex.Message);
				throw;
			}
			finally
			{
				IsProcessingEmails = false;
			}
		}
	}
}
