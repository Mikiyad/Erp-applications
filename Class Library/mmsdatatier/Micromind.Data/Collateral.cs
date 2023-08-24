using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Collateral : StoreObject
	{
		private const string COLLATERALID_PARM = "@CollateralID";

		private const string COLLATERALNAME_PARM = "@CollateralName";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string TYPEID_PARM = "@TypeID";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string RECEIVEDATE_PARM = "@ReceiveDate";

		private const string AMOUNT_PARM = "@Amount";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string DOCNO_PARM = "@DocNo";

		private const string BANKID_PARM = "@BankID";

		private const string ISRETURNED_PARM = "@IsReturned";

		private const string RETURNDATE_PARM = "@ReturnDate";

		private const string RETURNNOTE_PARM = "@ReturnNote";

		private const string CUSTODIANID_PARM = "@CustodianID";

		private const string RECEIVERNAME_PARM = "@ReceiverName";

		private const string STATUS_PARM = "@Status";

		private const string NOTE_PARM = "@Note";

		public const string COLLATERAL_TABLE = "Collateral";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Collateral(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Collateral", new FieldValue("CollateralID", "@CollateralID", isUpdateConditionField: true), new FieldValue("CollateralName", "@CollateralName"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("TypeID", "@TypeID"), new FieldValue("BankID", "@BankID"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("ReceiveDate", "@ReceiveDate"), new FieldValue("Amount", "@Amount"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("DocNo", "@DocNo"), new FieldValue("IsReturned", "@IsReturned"), new FieldValue("ReturnDate", "@ReturnDate"), new FieldValue("ReturnNote", "@ReturnNote"), new FieldValue("CustodianID", "@CustodianID"), new FieldValue("ReceiverName", "@ReceiverName"), new FieldValue("Status", "@Status"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Collateral", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CollateralID", SqlDbType.NVarChar);
			parameters.Add("@CollateralName", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.Char);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@TypeID", SqlDbType.NVarChar);
			parameters.Add("@ExpiryDate", SqlDbType.DateTime);
			parameters.Add("@ReceiveDate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@DocNo", SqlDbType.NVarChar);
			parameters.Add("@IsReturned", SqlDbType.Bit);
			parameters.Add("@ReturnDate", SqlDbType.DateTime);
			parameters.Add("@ReturnNote", SqlDbType.NVarChar);
			parameters.Add("@CustodianID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@ReceiverName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@CollateralID"].SourceColumn = "CollateralID";
			parameters["@CollateralName"].SourceColumn = "CollateralName";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@ReceiveDate"].SourceColumn = "ReceiveDate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@DocNo"].SourceColumn = "DocNo";
			parameters["@IsReturned"].SourceColumn = "IsReturned";
			parameters["@ReturnDate"].SourceColumn = "ReturnDate";
			parameters["@ReturnNote"].SourceColumn = "ReturnNote";
			parameters["@CustodianID"].SourceColumn = "CustodianID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@ReceiverName"].SourceColumn = "ReceiverName";
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

		public bool InsertCollateral(CollateralData accountCollateralData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountCollateralData, "Collateral", insertUpdateCommand);
				string text = accountCollateralData.CollateralTable.Rows[0]["CollateralID"].ToString();
				AddActivityLog("Collateral", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Collateral", "CollateralID", text, sqlTransaction, isInsert: true);
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Collateral, text, "Collateral", "CollateralID", sqlTransaction);
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

		public bool UpdateCollateral(CollateralData accountCollateralData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCollateralData, "Collateral", insertUpdateCommand);
				object obj = accountCollateralData.CollateralTable.Rows[0]["CollateralID"];
				if (flag)
				{
					UpdateTableRowByID("Collateral", "CollateralID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
					string entiyID = accountCollateralData.CollateralTable.Rows[0]["CollateralName"].ToString();
					AddActivityLog("Collateral", entiyID, ActivityTypes.Update, sqlTransaction);
					UpdateTableRowInsertUpdateInfo("Collateral", "CollateralID", obj, sqlTransaction, isInsert: false);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Collateral, obj.ToString(), "Collateral", "CollateralID", sqlTransaction);
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

		public CollateralData GetCollateral()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Collateral");
			CollateralData collateralData = new CollateralData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(collateralData, "Collateral", sqlBuilder);
			return collateralData;
		}

		public bool DeleteCollateral(string collateralID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Collateral WHERE CollateralID = '" + collateralID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Collateral", collateralID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CollateralData GetCollateralByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CollateralID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Collateral";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CollateralData collateralData = new CollateralData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(collateralData, "Collateral", sqlBuilder);
			return collateralData;
		}

		public DataSet GetCollateralByFields(params string[] columns)
		{
			return GetCollateralByFields(null, isInactive: true, columns);
		}

		public DataSet GetCollateralByFields(string[] collateralID, params string[] columns)
		{
			return GetCollateralByFields(collateralID, isInactive: true, columns);
		}

		public DataSet GetCollateralByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Collateral");
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
				commandHelper.FieldName = "CollateralID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Collateral";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Collateral", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCollateralList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Distinct  CollateralID,CollateralName,PayeeType [Payee Type], PayeeID [Payee Code],\r\n\r\n                         (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS [Payee Name],\r\n\t\t\t\t\t\tTypeID [Type],ReceiveDate,Amount, IsReturned,STUFF((\r\n            SELECT ',' + isnull( ed.EntityDocName,'NA')\r\n            FROM EntityDocs  ed\r\n            WHERE C.CollateralID=ED.EntityID\r\n            FOR XML PATH('')\r\n            ), 1, 1, '') AS Attachment\r\n                        FROM Collateral C LEFt OUTER JOIN \r\n                        Account ON C.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON C.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON C.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON C.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t LEFT JOIN EntityDocs ed ON C.CollateralID=ED.EntityID order by CollateralID\r\n\r\n\r\n\t\t\t\t\t\t ";
			FillDataSet(dataSet, "Collateral", textCommand);
			return dataSet;
		}

		public DataSet GetCollateralComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CollateralID [Code],CollateralName [Name]\r\n                           FROM Collateral ORDER BY CollateralID,CollateralName";
			FillDataSet(dataSet, "Collateral", textCommand);
			return dataSet;
		}

		public DataSet GetCollteralToPrint(string fromColID, string toColID, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT  CollateralID [Doc Number],CollateralName, ReceiveDate [Receive Date], Amount, CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee]\r\n                            ,DocNo [Cheque No],Collateral.Note, TypeID, GL.GenericListName, ExpiryDate, Amount, ReceiveDate,ReceiverName,Collateral.CurrencyID,Collateral.BankID,Bank.BankName\r\n                            FROM Collateral LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            LEFT JOIN Generic_List GL ON GL.GenericListID=Collateral.TypeID\r\n                            LEFT OUTER JOIN Bank ON Collateral.BankID=Bank.BankID\r\n                            WHERE 1=1  AND CollateralID='" + fromColID + "' ORDER BY ABS(DocNo)";
			FillDataSet(dataSet, "Collateral ", textCommand);
			return dataSet;
		}
	}
}
