using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Contacts : StoreObject
	{
		private const string CONTACT_TABLE = "@Contact";

		private const string CONTACTID_PARM = "@ContactID";

		private const string FIRSTNAME_PARM = "@FirstName";

		private const string MIDDLENAME_PARM = "@MiddleName";

		private const string LASTNAME_PARM = "@LastName";

		private const string JOBTITLE_PARM = "@JobTitle";

		private const string NICKNAME_PARM = "@NickName";

		private const string ADDRESS_PARM = "@Address";

		private const string CITY_PARM = "@City";

		private const string STATE_PARM = "@State";

		private const string COUNTRY_PARM = "@Country";

		private const string POSTALCODE_PARM = "@PostalCode";

		private const string DEPARTMENT_PARM = "@Department";

		private const string PHONE1_PARM = "@Phone1";

		private const string PHONE2_PARM = "@Phone2";

		private const string FAX_PARM = "@Fax";

		private const string MOBILE_PARM = "@Mobile";

		private const string EMAIL1_PARM = "@Email1";

		private const string EMAIL2_PARM = "@Email2";

		private const string WEBSITE_PARM = "@Website";

		private const string ADDRESSPRINTFORMAT_PARM = "@AddressPrintFormat";

		private const string NOTE_PARM = "@Note";

		private const string INACTIVE_PARM = "@Inactive";

		private const string BANKNAME_PARM = "@BankName";

		private const string BANKACCOUNTNUMBER_PARM = "@BankAccountNumber";

		private const string TWITTER_PARM = "@Twitter";

		private const string FACEBOOK_PARM = "@Facebook";

		private const string SKYPE_PARM = "@Skype";

		private const string LINKEDIN_PARM = "@LinkedIn";

		private const string BIRTHDATE_PARM = "@BirthDate";

		private const string SPOUSENAME_PARM = "@SpouseName";

		private const string IMADDRESS_PARM = "@IMAddress";

		private const string ANNIVERSARY_PARM = "@Anniversary";

		private const string MANAGERNAME_PARM = "@ManagerName";

		private const string ASSISTANTNAME_PARM = "@AssistantName";

		private const string CHILDRENNAME_PARM = "@ChildrenName";

		private const string NATIONALITY_PARM = "@Nationality";

		private const string GENDER_PARM = "@Gender";

		private const string HOBBIES_PARM = "@Hobbies";

		private const string LANGUAGE_PARM = "@Language";

		private const string USERDEFINED1_PARM = "@UserDefined1";

		private const string USERDEFINED2_PARM = "@UserDefined2";

		private const string USERDEFINED3_PARM = "@UserDefined3";

		private const string USERDEFINED4_PARM = "@UserDefined4";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		public Contacts(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Contact", new FieldValue("ContactID", "@ContactID", isUpdateConditionField: true), new FieldValue("FirstName", "@FirstName"), new FieldValue("MiddleName", "@MiddleName"), new FieldValue("LastName", "@LastName"), new FieldValue("JobTitle", "@JobTitle"), new FieldValue("NickName", "@NickName"), new FieldValue("Address", "@Address"), new FieldValue("City", "@City"), new FieldValue("State", "@State"), new FieldValue("Country", "@Country"), new FieldValue("PostalCode", "@PostalCode"), new FieldValue("Department", "@Department"), new FieldValue("Phone1", "@Phone1"), new FieldValue("Phone2", "@Phone2"), new FieldValue("Fax", "@Fax"), new FieldValue("Mobile", "@Mobile"), new FieldValue("Email1", "@Email1"), new FieldValue("Email2", "@Email2"), new FieldValue("Website", "@Website"), new FieldValue("Facebook", "@Facebook"), new FieldValue("Twitter", "@Twitter"), new FieldValue("Skype", "@Skype"), new FieldValue("LinkedIn", "@LinkedIn"), new FieldValue("AddressPrintFormat", "@AddressPrintFormat"), new FieldValue("Inactive", "@Inactive"), new FieldValue("BirthDate", "@BirthDate"), new FieldValue("SpouseName", "@SpouseName"), new FieldValue("IMAddress", "@IMAddress"), new FieldValue("Anniversary", "@Anniversary"), new FieldValue("ManagerName", "@ManagerName"), new FieldValue("AssistantName", "@AssistantName"), new FieldValue("ChildrenName", "@ChildrenName"), new FieldValue("Nationality", "@Nationality"), new FieldValue("Gender", "@Gender"), new FieldValue("Hobbies", "@Hobbies"), new FieldValue("Language", "@Language"), new FieldValue("BankName", "@BankName"), new FieldValue("BankAccountNumber", "@BankAccountNumber"), new FieldValue("UserDefined1", "@UserDefined1"), new FieldValue("UserDefined2", "@UserDefined2"), new FieldValue("UserDefined3", "@UserDefined3"), new FieldValue("UserDefined4", "@UserDefined4"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Contact", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ContactID", SqlDbType.NVarChar);
			parameters.Add("@FirstName", SqlDbType.NVarChar);
			parameters.Add("@MiddleName", SqlDbType.NVarChar);
			parameters.Add("@LastName", SqlDbType.NVarChar);
			parameters.Add("@JobTitle", SqlDbType.NVarChar);
			parameters.Add("@NickName", SqlDbType.NVarChar);
			parameters.Add("@Address", SqlDbType.NVarChar);
			parameters.Add("@City", SqlDbType.NVarChar);
			parameters.Add("@State", SqlDbType.NVarChar);
			parameters.Add("@Country", SqlDbType.NVarChar);
			parameters.Add("@PostalCode", SqlDbType.NVarChar);
			parameters.Add("@Department", SqlDbType.NVarChar);
			parameters.Add("@Phone1", SqlDbType.NVarChar);
			parameters.Add("@Phone2", SqlDbType.NVarChar);
			parameters.Add("@Fax", SqlDbType.NVarChar);
			parameters.Add("@Mobile", SqlDbType.NVarChar);
			parameters.Add("@Email1", SqlDbType.NVarChar);
			parameters.Add("@Email2", SqlDbType.NVarChar);
			parameters.Add("@Website", SqlDbType.NVarChar);
			parameters.Add("@Facebook", SqlDbType.NVarChar);
			parameters.Add("@Twitter", SqlDbType.NVarChar);
			parameters.Add("@Skype", SqlDbType.NVarChar);
			parameters.Add("@LinkedIn", SqlDbType.NVarChar);
			parameters.Add("@AddressPrintFormat", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.NVarChar);
			parameters.Add("@BirthDate", SqlDbType.DateTime);
			parameters.Add("@SpouseName", SqlDbType.NVarChar);
			parameters.Add("@IMAddress", SqlDbType.NVarChar);
			parameters.Add("@Anniversary", SqlDbType.DateTime);
			parameters.Add("@ManagerName", SqlDbType.NVarChar);
			parameters.Add("@AssistantName", SqlDbType.NVarChar);
			parameters.Add("@ChildrenName", SqlDbType.NVarChar);
			parameters.Add("@Nationality", SqlDbType.NVarChar);
			parameters.Add("@Gender", SqlDbType.Char);
			parameters.Add("@Hobbies", SqlDbType.NVarChar);
			parameters.Add("@Language", SqlDbType.NVarChar);
			parameters.Add("@BankName", SqlDbType.NVarChar);
			parameters.Add("@BankAccountNumber", SqlDbType.NVarChar);
			parameters.Add("@UserDefined1", SqlDbType.NVarChar);
			parameters.Add("@UserDefined2", SqlDbType.NVarChar);
			parameters.Add("@UserDefined3", SqlDbType.NVarChar);
			parameters.Add("@UserDefined4", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ContactID"].SourceColumn = "ContactID";
			parameters["@FirstName"].SourceColumn = "FirstName";
			parameters["@MiddleName"].SourceColumn = "MiddleName";
			parameters["@LastName"].SourceColumn = "LastName";
			parameters["@JobTitle"].SourceColumn = "JobTitle";
			parameters["@NickName"].SourceColumn = "NickName";
			parameters["@Address"].SourceColumn = "Address";
			parameters["@City"].SourceColumn = "City";
			parameters["@State"].SourceColumn = "State";
			parameters["@Country"].SourceColumn = "Country";
			parameters["@PostalCode"].SourceColumn = "PostalCode";
			parameters["@Department"].SourceColumn = "Department";
			parameters["@Phone1"].SourceColumn = "Phone1";
			parameters["@Phone2"].SourceColumn = "Phone2";
			parameters["@Fax"].SourceColumn = "Fax";
			parameters["@Mobile"].SourceColumn = "Mobile";
			parameters["@Email1"].SourceColumn = "Email1";
			parameters["@Email2"].SourceColumn = "Email2";
			parameters["@Website"].SourceColumn = "Website";
			parameters["@Facebook"].SourceColumn = "Facebook";
			parameters["@Twitter"].SourceColumn = "Twitter";
			parameters["@Skype"].SourceColumn = "Skype";
			parameters["@LinkedIn"].SourceColumn = "LinkedIn";
			parameters["@AddressPrintFormat"].SourceColumn = "AddressPrintFormat";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@BirthDate"].SourceColumn = "BirthDate";
			parameters["@SpouseName"].SourceColumn = "SpouseName";
			parameters["@IMAddress"].SourceColumn = "IMAddress";
			parameters["@Anniversary"].SourceColumn = "Anniversary";
			parameters["@ManagerName"].SourceColumn = "ManagerName";
			parameters["@AssistantName"].SourceColumn = "AssistantName";
			parameters["@ChildrenName"].SourceColumn = "ChildrenName";
			parameters["@Nationality"].SourceColumn = "Nationality";
			parameters["@Gender"].SourceColumn = "Gender";
			parameters["@Hobbies"].SourceColumn = "Hobbies";
			parameters["@Language"].SourceColumn = "Language";
			parameters["@BankName"].SourceColumn = "BankName";
			parameters["@BankAccountNumber"].SourceColumn = "BankAccountNumber";
			parameters["@UserDefined1"].SourceColumn = "UserDefined1";
			parameters["@UserDefined2"].SourceColumn = "UserDefined2";
			parameters["@UserDefined3"].SourceColumn = "UserDefined3";
			parameters["@UserDefined4"].SourceColumn = "UserDefined4";
			parameters["@Note"].SourceColumn = "Note";
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

		public bool InsertUpdateContact(ContactData contactData, bool isUpdate)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = ((!isUpdate) ? Insert(contactData, "Contact", insertUpdateCommand) : Update(contactData, "Contact", insertUpdateCommand));
				string text = contactData.ContactTable.Rows[0]["ContactID"].ToString();
				if (isUpdate)
				{
					AddActivityLog("Contact", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("Contact", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Contact", "ContactID", text, sqlTransaction, !isUpdate);
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

		public ContactData GetContact()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Contact");
			ContactData contactData = new ContactData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(contactData, "Contact", sqlBuilder);
			return contactData;
		}

		public bool DeleteContact(string contactID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM UDF_Contact  WHERE EntityID = '" + contactID + "'";
				flag = Delete(commandText, trans);
				commandText = "DELETE FROM Contact WHERE ContactID = '" + contactID + "'";
				flag = Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Contact", contactID, ActivityTypes.Delete, null);
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

		public ContactData GetContactByID(string id)
		{
			ContactData contactData = new ContactData();
			id = AddSingleQuote(id);
			string textCommand = "SELECT *, ISNULL((SELECT 1\r\n                            FROM Contact   P2 WHERE Photo IS Not NULL AND P2.ContactID=P1.ContactID ),0) AS HasPhoto FROM Contact P1 WHERE ContactID = '" + id + "'";
			FillDataSet(contactData, "Contact", textCommand);
			textCommand = "SELECT * FROM UDF_Contact WHERE EntityID = '" + id + "'";
			FillDataSet(contactData, "UDF", textCommand);
			return contactData;
		}

		public DataSet GetContactByFields(params string[] columns)
		{
			return GetContactByFields(null, isInactive: true, columns);
		}

		public DataSet GetContactByFields(string[] contactID, params string[] columns)
		{
			return GetContactByFields(contactID, isInactive: true, columns);
		}

		public DataSet GetContactByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Contact");
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
				commandHelper.FieldName = "ContactID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Contact";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "Inactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Contact";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Contact", sqlBuilder);
			return dataSet;
		}

		public DataSet GetContactList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "select ContactID [Code],FirstName AS [First Name],LastName AS [Last Name],City,Country,Phone1 AS [Phone],Mobile,Fax,Email1 AS [Email],STUFF((SELECT ', ' + EC.CategoryName [text()]\r\n                            FROM Contact CN1 LEFT JOIN Entity_Category_Detail ECD on CN.ContactID=ECD.EntityID LEFT JOIN Entity_Category EC  on EC.CategoryID=ECD.CategoryID \r\n                            WHERE CN1.ContactID=CN.ContactID\r\n                            FOR XML PATH(''), TYPE)\r\n                            .value('.','NVARCHAR(MAX)'),1,2,' ') Category\r\n                            FROM Contact CN  ";
			FillDataSet(dataSet, "Contact", textCommand);
			return dataSet;
		}

		public DataSet GetContactComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ContactID [Code],ISNULL(FirstName,'') + ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') AS [Name] FROM Contact\r\n                            WHERE INACTIVE<>1 ORDER BY ContactID,Name";
			FillDataSet(dataSet, "Contact", textCommand);
			return dataSet;
		}

		public bool AddContactPhoto(string contactID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Contact SET Photo=@Photo WHERE ContactID='" + contactID + "'");
				sqlCommand.Parameters.AddWithValue("@Photo", image);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool RemoveContactPhoto(string contactID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Contact SET Photo= Null WHERE ContactID='" + contactID + "'");
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public byte[] GetContactThumbnailImage(string contactID)
		{
			string exp = "SELECT Photo \r\n                           FROM Contact WHERE ContactID='" + contactID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return (byte[])obj;
			}
			return null;
		}
	}
}
