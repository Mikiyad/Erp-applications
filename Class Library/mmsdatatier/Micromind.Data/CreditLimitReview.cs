using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CreditLimitReview : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string DOCUMENTNUMBER_PARM = "@VoucherID";

		private const string CREDITLIMITREVIEW_TABLE = "Credit_Limit_Review";

		private const string RATINGDATE_PARM = "@RatingDate";

		private const string RATINGBY_PARM = "@RatingBy";

		private const string RATINGREMARKS_PARM = "@RatingRemarks";

		private const string RATING_PARM = "@Rating";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string ACCEPTCHECKPAYMENT_PARM = "@AcceptCheckPayment";

		private const string ACCEPTPDC_PARM = "@AcceptPDC";

		private const string CREDITLIMITTYPE_PARM = "@CreditLimitType";

		private const string CREDITAMOUNT_PARM = "@CreditAmount";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private string IDvalue;

		public CreditLimitReview(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Credit_Limit_Review", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("Rating", "@Rating"), new FieldValue("RatingBy", "@RatingBy"), new FieldValue("RatingDate", "@RatingDate"), new FieldValue("RatingRemarks", "@RatingRemarks"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("CreditAmount", "@CreditAmount"), new FieldValue("TransactionDate", "@TransactionDate"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Credit_Limit_Review", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SysDocID", SqlDbType.VarChar);
			parameters.Add("@VoucherID", SqlDbType.VarChar);
			parameters.Add("@CustomerID", SqlDbType.VarChar);
			parameters.Add("@Rating", SqlDbType.TinyInt);
			parameters.Add("@RatingDate", SqlDbType.DateTime);
			parameters.Add("@RatingBy", SqlDbType.NVarChar);
			parameters.Add("@RatingRemarks", SqlDbType.NVarChar);
			parameters.Add("@CreditAmount", SqlDbType.Money);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Rating"].SourceColumn = "Rating";
			parameters["@RatingBy"].SourceColumn = "RatingBy";
			parameters["@RatingDate"].SourceColumn = "RatingDate";
			parameters["@RatingRemarks"].SourceColumn = "RatingRemarks";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@CreditAmount"].SourceColumn = "CreditAmount";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
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

		public bool InsertUpdateCreditLimitReview(CreditLimitReviewData CreditLimitReviewData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = CreditLimitReviewData.CreditLimitReviewTable.Rows[0];
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(CreditLimitReviewData, "Credit_Limit_Review", insertUpdateCommand) : Update(CreditLimitReviewData, "Credit_Limit_Review", insertUpdateCommand));
				string entiyID = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (flag)
				{
					flag &= new Customers(base.DBConfig).UpdateCreditReviewDetails(CreditLimitReviewData, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				string text = CreditLimitReviewData.CreditLimitReviewTable.Rows[0]["VoucherID"].ToString();
				flag &= AddActivityLog("Credit Limit Review", entiyID, sysDocID, ActivityTypes.Add, sqlTransaction);
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Credit_Limit_Review", "VoucherID", sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Credit_Limit_Review", "VoucherID", text, sqlTransaction, isInsert: true);
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.CreditLimitReview, dataRow["SysDocID"].ToString(), text, "Credit_Limit_Review", sqlTransaction);
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

		public bool UpdateCreditLimitReview(CreditLimitReviewData CreditLimitReviewData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(CreditLimitReviewData, "Credit_Limit_Review", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = CreditLimitReviewData.CreditLimitReviewTable.Rows[0]["VoucherID"];
				UpdateTableRowByID("Credit_Limit_Review", "VoucherID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = CreditLimitReviewData.CreditLimitReviewTable.Rows[0]["VoucherID"].ToString();
				AddActivityLog("Credit Limit review", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Credit_Limit_Review", "SysDocID", obj, sqlTransaction, isInsert: false);
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

		public CreditLimitReviewData GetCreditLimitReview()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Credit_Limit_Review");
			CreditLimitReviewData creditLimitReviewData = new CreditLimitReviewData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(creditLimitReviewData, "Credit_Limit_Review", sqlBuilder);
			return creditLimitReviewData;
		}

		public bool DeleteCreditLimitReview(string documentNumber)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Credit_Limit_Review WHERE VoucherID='" + documentNumber + "'";
				flag = Delete(commandText, null);
				if (flag)
				{
					flag &= new Customers(base.DBConfig).UpdateInsuranceDetailsAfterDeletion(documentNumber);
				}
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Credit Limit Review", documentNumber, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CreditLimitReviewData GetCreditLimitReviewByID(string sysDocID, string id)
		{
			CreditLimitReviewData creditLimitReviewData = new CreditLimitReviewData();
			string textCommand = $"SELECT TOP 1 * FROM Credit_Limit_Review CI where sysdocId= '{sysDocID}' and  CI.VoucherID='" + id + "' ORDER By CI.DateCreated desc";
			FillDataSet(creditLimitReviewData, "Credit_Limit_Review", textCommand);
			return creditLimitReviewData;
		}

		public CreditLimitReviewData GetCreditLimitReviewIndividuals(string id)
		{
			CreditLimitReviewData creditLimitReviewData = new CreditLimitReviewData();
			SqlCommand sqlCommand = new SqlCommand("SELECT ci.CustomerID[CUSTOMER CODE], C.CustomerName [CUSTOMER NAME], ci.InsProvider [INSURANCE PROVIDER],ci.InsPolicyNumber [POLICY NUMBER],ci.InsRequestedAmount [REQUESTED AMOUNT], ci.InsApprovedAmount[APPROVED AMOUNT] from Customer_Insurance ci LEFT JOIN CUSTOMER C ON CI.CustomerID=c.CustomerID  WHERE CI.CustomerID='" + id + "' ");
			FillDataSet(creditLimitReviewData, "Credit_Limit_Review", sqlCommand);
			return creditLimitReviewData;
		}

		public CreditLimitReviewData GetList(DateTime from, DateTime to, bool showVoid)
		{
			CreditLimitReviewData creditLimitReviewData = new CreditLimitReviewData();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT CI.CustomerID[CUSTOMER CODE], C.CustomerName [CUSTOMER NAME],CI.AcceptCheckPayment,CI.AcceptPDC,CI.CreditLimitType,CI.CreditAmount, CI.DateCreated [DATE]from Credit_Limit_Review CI LEFT JOIN CUSTOMER C ON CI.CustomerID=c.CustomerID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " where  CI.DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(creditLimitReviewData, "Credit_Limit_Review", sqlCommand);
			return creditLimitReviewData;
		}

		public DataSet GetList(string id)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT CI.VoucherID, CI.SysDocID [Document ID], CI.CustomerID[CUSTOMER CODE], C.CustomerName [CUSTOMER NAME],CI.AcceptCheckPayment,\r\n                    CI.AcceptPDC,CI.CreditLimitType,CI.CreditAmount, CI.DateCreated [DATE] ,\r\n                    CASE WHEN (SELECT Count(EntityDocName) FROM EntityDocs   P2 WHERE EntityDocPath IS Not NULL AND EntitySysDocID=CI.SysDocID and EntityID=CI.VoucherID )>0 then 'True' else 'False' END AS [Has Attachment]\r\n                    from Credit_Limit_Review CI LEFT JOIN CUSTOMER C ON CI.CustomerID=c.CustomerID ";
			if (id != "")
			{
				text = text + " where CI.CustomerID='" + id + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text);
			FillDataSet(dataSet, "Credit_Limit_Review", sqlCommand);
			return dataSet;
		}

		public DataSet GetCreditLimitreviewByFields(params string[] columns)
		{
			return GetCreditLimitreviewByFields(null, isInactive: true, columns);
		}

		public DataSet GetCreditLimitreviewByFields(string[] CreditLimitreviewID, params string[] columns)
		{
			return GetCreditLimitreviewByFields(CreditLimitreviewID, isInactive: true, columns);
		}

		public DataSet GetCreditLimitreviewByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Credit_Limit_Review");
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
				commandHelper.FieldName = "VoucherID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Credit_Limit_Review";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Credit_Limit_Review", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCreditLimitreviewList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CreditLimitreviewID [CreditLimitreview Code],CreditLimitreviewName [CreditLimitreview Name],Note,IsInactive [Inactive]\r\n                           FROM Release_Type ";
			FillDataSet(dataSet, "Credit_Limit_Review", textCommand);
			return dataSet;
		}

		public DataSet GetCreditLimitreviewComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CreditLimitreviewID [Code],CreditLimitreviewName [Name]\r\n                           FROM Release_Type ORDER BY CreditLimitreviewID,CreditLimitreviewName";
			FillDataSet(dataSet, "Credit_Limit_Review", textCommand);
			return dataSet;
		}

		public string GetNextDocumentNumber(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			int num = 1;
			string text2 = "";
			string textCommand = "SELECT MAX(VoucherID) AS LastNumber FROM Credit_Limit_Review WHERE SysDocID='" + sysDocID + "'";
			FillDataSet(dataSet, "System_Document", textCommand);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				text2 = dataSet.Tables[0].Rows[0]["LastNumber"].ToString();
			}
			for (int i = 0; i < text2.Length && !char.IsNumber(text2[i]); i++)
			{
				text += text2[i].ToString();
			}
			if (text2 != "")
			{
				num = int.Parse(text2.Substring(text.Length)) + 1;
				int num2 = text2.Length - text.Length;
				string text3 = "";
				for (int j = 0; j < num2; j++)
				{
					text3 += "0";
				}
				return text + num.ToString(text3);
			}
			return text + num.ToString("00000000");
		}

		public CreditLimitReviewData GetCustomerIndividualsByID(string id)
		{
			new SqlBuilder();
			string textCommand = "SELECT Top 1 * from Credit_Limit_Review CLR WHERE CustomerID = '" + id + "' Order By  TransactionDate DESC";
			CreditLimitReviewData creditLimitReviewData = new CreditLimitReviewData();
			FillDataSet(creditLimitReviewData, "Credit_Limit_Review", textCommand);
			if (creditLimitReviewData != null && creditLimitReviewData.Tables.Count > 0)
			{
				_ = creditLimitReviewData.Tables[0].Rows.Count;
				_ = 0;
				return creditLimitReviewData;
			}
			return creditLimitReviewData;
		}
	}
}
