using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Buyer : StoreObject
	{
		private const string BUYERID_PARM = "@BuyerID";

		private const string BUYERNAME_PARM = "@FullName";

		public const string EMPLOYEEID_PARM = "@EmployeeID";

		public const string ADDRESS_PARM = "@Address";

		public const string CITY_PARM = "@City";

		public const string COUNTRY_PARM = "@Country";

		public const string STATE_PARM = "@State";

		public const string POSTALCODE_PARM = "@PostalCode";

		public const string ADDRESSPRINTFORMAT_PARM = "@AddressPrintFormat";

		public const string PHONE1_PARM = "@Phone1";

		public const string PHONE2_PARM = "@Phone2";

		public const string MOBILE_PARM = "@Mobile";

		public const string FAX_PARM = "@Fax";

		public const string AREAID_PARM = "@AreaID";

		public const string COUNTRYID_PARM = "@CountryID";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string EMAIL_PARM = "@Email";

		public const string WEBSITE_PARM = "@Website";

		public const string BANKNAME_PARM = "@BankName";

		public const string BANKBRANCH_PARM = "@BankBranch";

		public const string BANKACCOUNTNUMBER_PARM = "@BankAccountNumber";

		public const string NOTE_PARM = "@Note";

		public const string BUYER_TABLE = "Buyer";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Buyer(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Buyer", new FieldValue("BuyerID", "@BuyerID", isUpdateConditionField: true), new FieldValue("FullName", "@FullName"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("Address", "@Address"), new FieldValue("City", "@City"), new FieldValue("Country", "@Country"), new FieldValue("State", "@State"), new FieldValue("PostalCode", "@PostalCode"), new FieldValue("AddressPrintFormat", "@AddressPrintFormat"), new FieldValue("Phone1", "@Phone1"), new FieldValue("Phone2", "@Phone2"), new FieldValue("Mobile", "@Mobile"), new FieldValue("Fax", "@Fax"), new FieldValue("Email", "@Email"), new FieldValue("Website", "@Website"), new FieldValue("BankName", "@BankName"), new FieldValue("BankBranch", "@BankBranch"), new FieldValue("BankAccountNumber", "@BankAccountNumber"), new FieldValue("AreaID", "@AreaID"), new FieldValue("CountryID", "@CountryID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Buyer", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@FullName", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@Address", SqlDbType.NVarChar);
			parameters.Add("@City", SqlDbType.NVarChar);
			parameters.Add("@Country", SqlDbType.NVarChar);
			parameters.Add("@State", SqlDbType.NVarChar);
			parameters.Add("@PostalCode", SqlDbType.NVarChar);
			parameters.Add("@AddressPrintFormat", SqlDbType.NVarChar);
			parameters.Add("@Phone1", SqlDbType.NVarChar);
			parameters.Add("@Phone2", SqlDbType.NVarChar);
			parameters.Add("@Mobile", SqlDbType.NVarChar);
			parameters.Add("@Fax", SqlDbType.NVarChar);
			parameters.Add("@Email", SqlDbType.NVarChar);
			parameters.Add("@Website", SqlDbType.NVarChar);
			parameters.Add("@BankName", SqlDbType.NVarChar);
			parameters.Add("@BankBranch", SqlDbType.NVarChar);
			parameters.Add("@BankAccountNumber", SqlDbType.NVarChar);
			parameters.Add("@AreaID", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@FullName"].SourceColumn = "FullName";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@Address"].SourceColumn = "Address";
			parameters["@City"].SourceColumn = "City";
			parameters["@Country"].SourceColumn = "Country";
			parameters["@State"].SourceColumn = "State";
			parameters["@PostalCode"].SourceColumn = "PostalCode";
			parameters["@AddressPrintFormat"].SourceColumn = "AddressPrintFormat";
			parameters["@Phone1"].SourceColumn = "Phone1";
			parameters["@Phone2"].SourceColumn = "Phone2";
			parameters["@Mobile"].SourceColumn = "Mobile";
			parameters["@Fax"].SourceColumn = "Fax";
			parameters["@Email"].SourceColumn = "Email";
			parameters["@Website"].SourceColumn = "Website";
			parameters["@BankName"].SourceColumn = "BankName";
			parameters["@BankBranch"].SourceColumn = "BankBranch";
			parameters["@BankAccountNumber"].SourceColumn = "BankAccountNumber";
			parameters["@AreaID"].SourceColumn = "AreaID";
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertBuyer(BuyerData accountBuyerData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountBuyerData, "Buyer", insertUpdateCommand);
				string text = accountBuyerData.BuyerTable.Rows[0]["BuyerID"].ToString();
				AddActivityLog("Buyer", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Buyer", "BuyerID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateBuyer(BuyerData accountBuyerData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountBuyerData, "Buyer", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountBuyerData.BuyerTable.Rows[0]["BuyerID"];
				UpdateTableRowByID("Buyer", "BuyerID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountBuyerData.BuyerTable.Rows[0]["FullName"].ToString();
				AddActivityLog("Buyer", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Buyer", "BuyerID", obj, sqlTransaction, isInsert: false);
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

		public BuyerData GetBuyer()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Buyer");
			BuyerData buyerData = new BuyerData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(buyerData, "Buyer", sqlBuilder);
			return buyerData;
		}

		public bool DeleteBuyer(string buyerID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Buyer WHERE BuyerID = '" + buyerID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Buyer", buyerID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public BuyerData GetBuyerByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "BuyerID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Buyer";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			BuyerData buyerData = new BuyerData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(buyerData, "Buyer", sqlBuilder);
			return buyerData;
		}

		public DataSet GetBuyerByFields(params string[] columns)
		{
			return GetBuyerByFields(null, isInactive: true, columns);
		}

		public DataSet GetBuyerByFields(string[] buyerID, params string[] columns)
		{
			return GetBuyerByFields(buyerID, isInactive: true, columns);
		}

		public DataSet GetBuyerByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Buyer");
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
				commandHelper.FieldName = "BuyerID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Buyer";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Buyer", sqlBuilder);
			return dataSet;
		}

		public DataSet GetBuyerList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BuyerID [Buyer Code],FullName [Full Name],Note,IsInactive [Inactive]\r\n                           FROM Buyer ";
			FillDataSet(dataSet, "Buyer", textCommand);
			return dataSet;
		}

		public DataSet GetBuyerComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BuyerID [Code],FullName [Name]\r\n                           FROM Buyer WHERE IsInactive<> 1 ORDER BY BuyerID,FullName";
			FillDataSet(dataSet, "Buyer", textCommand);
			return dataSet;
		}

		public DataSet GetPurchaseByBuyerSummaryReport(DateTime from, DateTime to, string fromBuyer, string toBuyer)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "Select ISNULL(SI.BuyerID,'No Buyer')AS [BuyerID], SP.FullName,\r\n                            SUM(Discount) AS Discount,\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN Total ELSE 0 END) AS [CashSale],\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN Total ELSE 0 END) AS [CreditSale] \r\n                            FROM Purchase_Invoice SI LEFT OUTER JOIN Buyer SP ON SI.BuyerID=SP.BuyerID\r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromBuyer != "")
			{
				str = str + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			str += " GROUP BY SI.BuyerID,SP.FullName ORDER BY SI.BuyerID";
			FillDataSet(dataSet, "Purchase", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["BuyerID"]
			};
			str = "Select ISNULL(SI.BuyerID,'No Buyer')AS [BuyerID], SP.FullName,\r\n                    -1*SUM(Discount) AS DiscountReturn,\r\n                    SUM(Total) AS [PurchaseReturn]\r\n                    FROM Purchase_Return SI LEFT OUTER JOIN Buyer SP ON SI.BuyerID=SP.BuyerID \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromBuyer != "")
			{
				str = str + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			str += " GROUP BY SI.BuyerID,SP.FullName Order BY SI.BuyerID";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Purchase", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["BuyerID"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetPurchaseByBuyerDetailReport(DateTime from, DateTime to, string fromBuyer, string toBuyer)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "Select DISTINCT ISNULL(SI.BuyerID,'No Buyer') AS [BuyerID],FullName\r\n                            FROM Purchase_Invoice SI LEFT OUTER JOIN Buyer ON SI.BuyerID=Buyer.BuyerID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromBuyer != "")
			{
				text3 = text3 + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			text3 += " UNION ";
			text3 += "Select DISTINCT ISNULL(SI.BuyerID,'No Buyer') AS [BuyerID],FullName\r\n                            FROM Purchase_Return SI LEFT OUTER JOIN Buyer ON SI.BuyerID=Buyer.BuyerID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromBuyer != "")
			{
				text3 = text3 + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			text3 += " ORDER BY BuyerID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Buyer", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select SysDocID,VoucherID,ISNULL(BuyerID,'No Buyer') AS [BuyerID],TransactionDate,Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Purchase' ELSE 'Cash Purchase' END AS [Type],\r\n                    BuyerID,CurrencyID,CurrencyRate,Discount,DiscountFC,\r\n                    Total,TotalFC \r\n\r\n                    FROM Purchase_Invoice SI WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromBuyer != "")
			{
				text3 = text3 + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			text3 += " UNION ";
			text3 = text3 + "Select SysDocID,VoucherID,ISNULL(BuyerID,'No Buyer') AS [BuyerID],TransactionDate,Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type],\r\n                    BuyerID,CurrencyID,CurrencyRate,-1*Discount,-1*DiscountFC,\r\n                    -1*Total,-1*TotalFC \r\n\r\n                    FROM Purchase_Return SI WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromBuyer != "")
			{
				text3 = text3 + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
			}
			text3 += " ORDER BY TransactionDate";
			FillDataSet(dataSet2, "Purchase", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Purchase Detail", dataSet.Tables["Buyer"].Columns["BuyerID"], dataSet.Tables["Purchase"].Columns["BuyerID"], createConstraints: false);
			return dataSet;
		}
	}
}
