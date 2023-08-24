using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Salesperson : StoreObject
	{
		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string SALESPERSONNAME_PARM = "@FullName";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string REPORTTO_PARM = "@ReportTo";

		private const string ADDRESS_PARM = "@Address";

		private const string CITY_PARM = "@City";

		private const string COUNTRY_PARM = "@Country";

		private const string STATE_PARM = "@State";

		private const string POSTALCODE_PARM = "@PostalCode";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string GROUPID_PARM = "@GroupID";

		private const string ADDRESSPRINTFORMAT_PARM = "@AddressPrintFormat";

		private const string PHONE1_PARM = "@Phone1";

		private const string PHONE2_PARM = "@Phone2";

		private const string MOBILE_PARM = "@Mobile";

		private const string FAX_PARM = "@Fax";

		private const string COMISSIONPERCENT_PARM = "@CommissionPercent";

		private const string COMMISSIONTYPE_PARM = "@CommissionType";

		private const string AREAID_PARM = "@AreaID";

		private const string COUNTRYID_PARM = "@CountryID";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string EMAIL_PARM = "@Email";

		private const string WEBSITE_PARM = "@Website";

		private const string EMAILSTATEMENT_PARM = "@EmailStatement";

		private const string ALIASNAME_PARM = "@Alias";

		private const string BANKNAME_PARM = "@BankName";

		private const string BANKBRANCH_PARM = "@BankBranch";

		private const string BANKACCOUNTNUMBER_PARM = "@BankAccountNumber";

		private const string NOTE_PARM = "@Note";

		private const string SALESPERSON_TABLE = "Salesperson";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Salesperson(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Salesperson", new FieldValue("SalespersonID", "@SalespersonID", isUpdateConditionField: true), new FieldValue("FullName", "@FullName"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("ReportTo", "@ReportTo"), new FieldValue("Address", "@Address"), new FieldValue("City", "@City"), new FieldValue("Country", "@Country"), new FieldValue("State", "@State"), new FieldValue("PostalCode", "@PostalCode"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("GroupID", "@GroupID"), new FieldValue("AddressPrintFormat", "@AddressPrintFormat"), new FieldValue("Phone1", "@Phone1"), new FieldValue("Phone2", "@Phone2"), new FieldValue("Mobile", "@Mobile"), new FieldValue("Fax", "@Fax"), new FieldValue("Email", "@Email"), new FieldValue("Website", "@Website"), new FieldValue("EmailStatement", "@EmailStatement"), new FieldValue("Alias", "@Alias"), new FieldValue("BankName", "@BankName"), new FieldValue("BankBranch", "@BankBranch"), new FieldValue("BankAccountNumber", "@BankAccountNumber"), new FieldValue("CommissionPercent", "@CommissionPercent"), new FieldValue("CommissionType", "@CommissionType"), new FieldValue("AreaID", "@AreaID"), new FieldValue("CountryID", "@CountryID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Salesperson", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@FullName", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@ReportTo", SqlDbType.NVarChar);
			parameters.Add("@Address", SqlDbType.NVarChar);
			parameters.Add("@City", SqlDbType.NVarChar);
			parameters.Add("@Country", SqlDbType.NVarChar);
			parameters.Add("@State", SqlDbType.NVarChar);
			parameters.Add("@PostalCode", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@AddressPrintFormat", SqlDbType.NVarChar);
			parameters.Add("@Phone1", SqlDbType.NVarChar);
			parameters.Add("@Phone2", SqlDbType.NVarChar);
			parameters.Add("@Mobile", SqlDbType.NVarChar);
			parameters.Add("@Fax", SqlDbType.NVarChar);
			parameters.Add("@Email", SqlDbType.NVarChar);
			parameters.Add("@Website", SqlDbType.NVarChar);
			parameters.Add("@EmailStatement", SqlDbType.Bit);
			parameters.Add("@Alias", SqlDbType.NVarChar);
			parameters.Add("@BankName", SqlDbType.NVarChar);
			parameters.Add("@BankBranch", SqlDbType.NVarChar);
			parameters.Add("@BankAccountNumber", SqlDbType.NVarChar);
			parameters.Add("@CommissionPercent", SqlDbType.Decimal);
			parameters.Add("@CommissionType", SqlDbType.TinyInt);
			parameters.Add("@AreaID", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@FullName"].SourceColumn = "FullName";
			parameters["@ReportTo"].SourceColumn = "ReportTo";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@Address"].SourceColumn = "Address";
			parameters["@City"].SourceColumn = "City";
			parameters["@Country"].SourceColumn = "Country";
			parameters["@State"].SourceColumn = "State";
			parameters["@PostalCode"].SourceColumn = "PostalCode";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@AddressPrintFormat"].SourceColumn = "AddressPrintFormat";
			parameters["@Phone1"].SourceColumn = "Phone1";
			parameters["@Phone2"].SourceColumn = "Phone2";
			parameters["@Mobile"].SourceColumn = "Mobile";
			parameters["@Fax"].SourceColumn = "Fax";
			parameters["@Email"].SourceColumn = "Email";
			parameters["@Website"].SourceColumn = "Website";
			parameters["@EmailStatement"].SourceColumn = "EmailStatement";
			parameters["@Alias"].SourceColumn = "Alias";
			parameters["@BankName"].SourceColumn = "BankName";
			parameters["@BankBranch"].SourceColumn = "BankBranch";
			parameters["@BankAccountNumber"].SourceColumn = "BankAccountNumber";
			parameters["@CommissionPercent"].SourceColumn = "CommissionPercent";
			parameters["@CommissionType"].SourceColumn = "CommissionType";
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

		public bool InsertSalesperson(SalespersonData accountSalespersonData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountSalespersonData, "Salesperson", insertUpdateCommand);
				string text = accountSalespersonData.SalespersonTable.Rows[0]["SalespersonID"].ToString();
				AddActivityLog("Salesperson", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Salesperson", "SalespersonID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateSalesperson(SalespersonData accountSalespersonData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountSalespersonData, "Salesperson", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountSalespersonData.SalespersonTable.Rows[0]["SalespersonID"];
				UpdateTableRowByID("Salesperson", "SalespersonID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				accountSalespersonData.SalespersonTable.Rows[0]["FullName"].ToString();
				AddActivityLog("Salesperson", obj.ToString(), ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Salesperson", "SalespersonID", obj, sqlTransaction, isInsert: false);
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

		public SalespersonData GetSalesperson()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Salesperson");
			SalespersonData salespersonData = new SalespersonData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(salespersonData, "Salesperson", sqlBuilder);
			return salespersonData;
		}

		public bool DeleteSalesperson(string salespersonID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Salesperson WHERE SalespersonID = '" + salespersonID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Salesperson", salespersonID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public SalespersonData GetSalespersonByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "SalespersonID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Salesperson";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			SalespersonData salespersonData = new SalespersonData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(salespersonData, "Salesperson", sqlBuilder);
			return salespersonData;
		}

		public DataSet GetSalespersonByFields(params string[] columns)
		{
			return GetSalespersonByFields(null, isInactive: true, columns);
		}

		public DataSet GetSalespersonByFields(string[] salespersonID, params string[] columns)
		{
			return GetSalespersonByFields(salespersonID, isInactive: true, columns);
		}

		public DataSet GetSalespersonByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Salesperson");
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
				commandHelper.FieldName = "SalespersonID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Salesperson";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Salesperson", sqlBuilder);
			return dataSet;
		}

		public DataSet GetSalespersonList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SalespersonID [Code],FullName [Full Name],Phone1 AS [Phone 1],Phone2 AS [Phone 2],Mobile\r\n                           FROM Salesperson ";
			FillDataSet(dataSet, "Salesperson", textCommand);
			return dataSet;
		}

		public DataSet GetSalespersonComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SalespersonID [Code],FullName [Name]\r\n                           FROM Salesperson  WHERE ISNULL(IsInactive,'False') = 'False' ORDER BY SalespersonID,FullName ";
			FillDataSet(dataSet, "Salesperson", textCommand);
			return dataSet;
		}

		public DataSet GetSalesBySalespersonByReportToReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			base.DBConfig.StartNewTransaction();
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				DataSet dataSet = new DataSet();
				text3 = "SELECT  ISNULL(SI.ReportTo, 'No SalesMan') AS [SalesPersonID], SP.FullName AS [SalesPerson],\r\n                           Salesperson_Group.GroupName,Division.DivisionName,Area.AreaName,Country.CountryName,\r\n                            SUM( SI.Total-SI.Discount+SI.RoundOff) AS [Sale],SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) \r\n                                AS [CashSale], SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) AS [CreditSale] ,\r\n                                (SELECT SUM(A.Amount) FROM Sales_Man_Target_Detail A WHERE A.SalesManID=SI.ReportTo) AS [Target] \r\n                            FROM Sales_Invoice SI \r\n                            LEFT JOIN Salesperson SP ON SP.SalespersonID=SI.ReportTo\r\n                               LEFT OUTER JOIN Salesperson_Group ON Salesperson_Group.GroupID=SP.GroupID\r\n                               LEFT OUTER JOIN Division ON Division.DivisionID=SP.DivisionID\r\n                               LEFT OUTER JOIN Area ON Area.AreaID=SP.AreaID\r\n                               LEFT OUTER JOIN Country ON Country.CountryID=SP.CountryID             \r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				text3 += "GROUP BY SI.ReportTo,SP.FullName,GroupName,DivisionName,Areaname,CountryName\r\n                    ORDER BY SI.ReportTo";
				FillDataSet(dataSet, "Sales", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["SalesPersonID"]
				};
				text3 = "SELECT  ISNULL(SI.ReportTo, 'No SalesMan') AS [SalesPersonID], SP.FullName AS [SalesPerson],\r\n                           Salesperson_Group.GroupName,Division.DivisionName,Area.AreaName,Country.CountryName,\r\n                            SUM( SI.Total-SI.Discount+SI.RoundOff ) AS [SaleReturn],\r\n                                SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) \r\n                                AS [CashSaleReturn], SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) AS [CreditSaleReturn]\r\n                            FROM Sales_Return SI \r\n                            LEFT JOIN Salesperson SP ON SP.SalespersonID=SI.ReportTo \r\n                               LEFT OUTER JOIN Salesperson_Group ON Salesperson_Group.GroupID=SP.GroupID\r\n                               LEFT OUTER JOIN Division ON Division.DivisionID=SP.DivisionID\r\n                               LEFT OUTER JOIN Area ON Area.AreaID=SP.AreaID\r\n                               LEFT OUTER JOIN Country ON Country.CountryID=SP.CountryID              \r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				text3 += "GROUP BY SI.ReportTo,SP.FullName,GroupName,DivisionName,Areaname,CountryName\r\n                                ORDER BY SI.ReportTo";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Sales", text3);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["SalesPersonID"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.RollbackTransaction();
			}
		}

		public DataSet GetSalesBySalespersonSummaryReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT ISNULL(ASD.SalespersonID,'None') AS SalespersonID,SP.FullName AS FullName,\r\n                           Salesperson_Group.GroupName,Division.DivisionName,Area.AreaName,Country.CountryName\r\n                                ,SUM(ASD.Amount) AS SalesAmount,\r\n                                SUM( ASD.COGS) AS COGS,\r\n                                SUM(ASD.Amount - ASD.COGS) AS Profit\r\n                                FROM Axo_Sales_Detail ASD \r\n                               LEFT JOIN Salesperson SP ON SP.SalespersonID=ASD.SalespersonID\r\n                               LEFT OUTER JOIN Salesperson_Group ON Salesperson_Group.GroupID=SP.GroupID\r\n                               LEFT OUTER JOIN Division ON Division.DivisionID=SP.DivisionID\r\n                               LEFT OUTER JOIN Area ON Area.AreaID=SP.AreaID\r\n                               LEFT OUTER JOIN Country ON Country.CountryID=SP.CountryID\r\n                                WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				text3 += "  GROUP BY ASD.SalespersonID,SP.FullName ,GroupName,DivisionName,Areaname,CountryName ";
				FillDataSet(dataSet, "Sales", text3, sqlTransaction);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["SalespersonID"]
				};
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.RollbackTransaction();
			}
		}

		public DataSet GetSalesBySalespersonDetailReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "Select DISTINCT ISNULL(SI.SalespersonID,'No Salesperson') AS [SalespersonID],FullName,\r\n                           Salesperson_Group.GroupName,Division.DivisionName,Area.AreaName,Country.CountryName\r\n                           FROM Sales_Invoice SI\r\n                         LEFT OUTER JOIN Salesperson ON SI.SalespersonID=Salesperson.SalespersonID\r\n                         LEFT OUTER JOIN Salesperson_Group ON Salesperson_Group.GroupID=Salesperson.GroupID\r\n                         LEFT OUTER JOIN Division ON Division.DivisionID=Salesperson.DivisionID\r\n                         LEFT OUTER JOIN Area ON Area.AreaID=Salesperson.AreaID\r\n                         LEFT OUTER JOIN Country ON Country.CountryID=Salesperson.CountryID\r\n\r\n                          ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromSalesperson != "")
			{
				text3 = text3 + " AND SI.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
			}
			if (fromSalespersonGroup != "")
			{
				text3 = text3 + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
			}
			if (fromSalespersonDivision != "")
			{
				text3 = text3 + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
			}
			if (fromSalespersonArea != "")
			{
				text3 = text3 + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
			}
			if (fromSalespersonCountry != "")
			{
				text3 = text3 + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
			}
			text3 += " UNION ";
			text3 += "Select DISTINCT ISNULL(SI.SalespersonID,'No Salesperson') AS [SalespersonID],FullName,\r\n                        Salesperson_Group.GroupName,Division.DivisionName,Area.AreaName,Country.CountryName\r\n                            FROM Sales_Return SI \r\n                         LEFT OUTER JOIN Salesperson ON SI.SalespersonID=Salesperson.SalespersonID\r\n                         LEFT OUTER JOIN Salesperson_Group ON Salesperson_Group.GroupID=Salesperson.GroupID\r\n                         LEFT OUTER JOIN Division ON Division.DivisionID=Salesperson.DivisionID\r\n                         LEFT OUTER JOIN Area ON Area.AreaID=Salesperson.AreaID\r\n                         LEFT OUTER JOIN Country ON Country.CountryID=Salesperson.CountryID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromSalesperson != "")
			{
				text3 = text3 + " AND SI.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
			}
			if (fromSalespersonGroup != "")
			{
				text3 = text3 + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
			}
			if (fromSalespersonDivision != "")
			{
				text3 = text3 + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
			}
			if (fromSalespersonArea != "")
			{
				text3 = text3 + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
			}
			if (fromSalespersonCountry != "")
			{
				text3 = text3 + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
			}
			text3 += " ORDER BY SalespersonID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Salesperson", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "SELECT ASD.*,ISNULL(ASD.SalespersonID,'None') AS SalespersonID,SP.FullName AS [Salesperson Name],(ASD.Amount) AS SalesAmount,\r\n                    ( ASD.COGS) AS COGS,\r\n                    (ASD.Amount - ASD.COGS) AS Profit,ASD.AmountFC AS TotalFC\r\n                    FROM Axo_Sales_Detail ASD LEFT JOIN Salesperson SP ON SP.SalespersonID=ASD.SalespersonID\r\n                    WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromSalesperson != "")
			{
				text3 = text3 + " AND ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
			}
			if (fromSalespersonGroup != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
			}
			if (fromSalespersonDivision != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
			}
			if (fromSalespersonArea != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
			}
			if (fromSalespersonCountry != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
			}
			text3 += " ORDER BY Date";
			FillDataSet(dataSet2, "Sales", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Sales Detail", dataSet.Tables["Salesperson"].Columns["SalespersonID"], dataSet.Tables["Sales"].Columns["SalespersonID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetSalesPersonCommissionDetailReport(DateTime from, DateTime to, string fromBrand, string toBrand, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, decimal commissionPercent)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT T.CategoryID,T.CategoryName,T.MainCategory,T.SalespersonID,T.BrandID,T.BrandName,\r\n                        (SELECT PC1.CategoryName FROM Product_Category PC1 WHERE PC1.CategoryID=T.MainCategory) AS[Main],\r\n                        (SELECT PC1.CommissionPercent FROM Product_Category PC1 WHERE PC1.CategoryID=T.MainCategory) AS[CatgryCommissionPercent],\r\n                        T.[Salesperson Name],T.ReportTo,T.Location,\r\n                        SUM(T.COGS) AS [COGS],SUM(T.SalesAmount) AS [SalesAmount],SUM(T.Quantity) AS [SalesQty]\r\n                        FROM (SELECT P.BrandID,PB.BrandName,P.CategoryID,PC.CategoryName,\r\n                        (SELECT PC1.ParentCategoryID FROM Product_Category PC1 WHERE PC1.CategoryID=PC.CategoryID) AS [MainCategory],ASD.Quantity,\r\n                        ISNULL(ASD.SalespersonID,'None') AS SalespersonID,ASD.ReportTo AS ReportTo ,SP.FullName AS [Salesperson Name],\r\n                        (ASD.Amount) AS SalesAmount,\r\n                        ( ASD.COGS) AS COGS,SD.LocationID AS [Location],\r\n                        (ASD.Amount - ASD.COGS) AS Profit,ASD.AmountFC AS TotalFC\r\n                        FROM Axo_Sales_Detail ASD LEFT JOIN Salesperson SP ON SP.SalespersonID=ASD.SalespersonID\r\n                        INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                        INNER JOIN Product_Category PC ON PC.CategoryID=P.CategoryID \r\n                        LEFT JOIN Product_Brand PB ON PB.BrandID=P.BrandID  \r\n                        LEFT JOIN Customer C  ON C.CustomerID=ASD.CustomerID  \r\n                        LEFT JOIN System_Document SD  ON SD.SysDocID=ASD.[Doc ID]    \r\n                             ";
			text3 = text3 + " WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromBrand != "")
			{
				text3 = text3 + " AND P.BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "' ";
			}
			if (fromCategory != "")
			{
				text3 = text3 + " AND ( P.CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "'  OR (SELECT PC1.ParentCategoryID FROM Product_Category PC1 WHERE PC1.CategoryID=PC.CategoryID) BETWEEN '" + fromCategory + "' AND '" + toCategory + "')   ";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			if (fromSalesperson != "")
			{
				text3 = text3 + " AND (ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR  ASD.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' )";
			}
			if (fromSalespersonGroup != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
			}
			if (fromSalespersonDivision != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
			}
			if (fromSalespersonArea != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
			}
			if (fromSalespersonCountry != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
			}
			text3 += " )T  GROUP BY T.CategoryID,T.CategoryName,T.MainCategory,T.SalespersonID,T.[Salesperson Name],T.ReportTo,T.Location,T.BrandID,T.BrandName ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "SalespersonCommission", text3);
			return dataSet;
		}

		public DataSet GetSalesPersonCommissionItemIncludedReport(DateTime from, DateTime to, string fromBrand, string toBrand, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, decimal commissionPercent)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT ASD.*,C.CustomerName,P.BrandID,PB.BrandName,P.CategoryID,PC.CategoryName,(SELECT PC1.ParentCategoryID FROM Product_Category PC1 WHERE PC1.CategoryID=PC.CategoryID) AS [MainCategory],\r\n                            ISNULL(ASD.SalespersonID,'None') AS SalespersonID,ASD.ReportTo,SP.FullName AS [Salesperson Name],(ASD.Amount) AS SalesAmount,(SELECT PC1.CommissionPercent FROM Product_Category PC1 WHERE PC1.CategoryID=PC.CategoryID) AS[CatgryCommissionPercent],\r\n                            ( ASD.COGS) AS COGS,\r\n                              --(SELECT LocationID FROM Users WHERE DefaultSalespersonID=ASD.SalespersonID) AS [Location],\r\n                            (ASD.Amount - ASD.COGS) AS Profit,ASD.AmountFC AS TotalFC,SD.LocationID AS [Location]\r\n                            FROM Axo_Sales_Detail ASD LEFT JOIN Salesperson SP ON SP.SalespersonID=ASD.SalespersonID\r\n                            INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                            LEFT JOIN Product_Category PC ON PC.CategoryID=P.CategoryID \r\n                            LEFT JOIN Product_Brand PB ON PB.BrandID=P.BrandID  \r\n                            LEFT JOIN Customer C  ON C.CustomerID=ASD.CustomerID  \r\n                            LEFT JOIN System_Document SD  ON SD.SysDocID=ASD.[Doc ID] \r\n                             ";
			text3 = text3 + " WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromBrand != "")
			{
				text3 = text3 + " AND P.BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "' ";
			}
			if (fromCategory != "")
			{
				text3 = text3 + " AND ( P.CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "'  OR (SELECT PC1.ParentCategoryID FROM Product_Category PC1 WHERE PC1.CategoryID=PC.CategoryID) BETWEEN '" + fromCategory + "' AND '" + toCategory + "')   ";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			if (fromSalesperson != "")
			{
				text3 = text3 + " AND (ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR  ASD.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' )";
			}
			if (fromSalespersonGroup != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
			}
			if (fromSalespersonDivision != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
			}
			if (fromSalespersonArea != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
			}
			if (fromSalespersonCountry != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
			}
			text3 += " ORDER BY ASD.Date ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "SalespersonCommission", text3);
			return dataSet;
		}

		public DataSet GetSalesPersonCommissionSummaryReport(DateTime from, DateTime to, string fromBrand, string toBrand, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, decimal commissionPercent)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT T.MainCategory,\r\n                        (SELECT PC1.CategoryName FROM Product_Category PC1 WHERE PC1.CategoryID=T.MainCategory) AS[Main],(SELECT PC1.CommissionPercent FROM Product_Category PC1 WHERE PC1.CategoryID=T.MainCategory) AS[CatgryCommissionPercent],T.ReportTo,\r\n                        SUM(T.COGS) AS [COGS],SUM(T.SalesAmount) AS [SalesAmount],SUM(T.Quantity) AS [SalesQty]\r\n                        FROM (SELECT P.BrandID,PB.BrandName,P.CategoryID,PC.CategoryName,\r\n                        (SELECT PC1.ParentCategoryID FROM Product_Category PC1 WHERE PC1.CategoryID=PC.CategoryID) AS [MainCategory],ASD.Quantity,\r\n                        ISNULL(ASD.SalespersonID,'None') AS SalespersonID,ASD.ReportTo AS ReportTo ,SP.FullName AS [Salesperson Name],\r\n                        (ASD.Amount) AS SalesAmount,\r\n                        ( ASD.COGS) AS COGS,SD.LocationID AS [Location],\r\n                        (ASD.Amount - ASD.COGS) AS Profit,ASD.AmountFC AS TotalFC\r\n                        FROM Axo_Sales_Detail ASD LEFT JOIN Salesperson SP ON SP.SalespersonID=ASD.SalespersonID\r\n                        INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                        INNER JOIN Product_Category PC ON PC.CategoryID=P.CategoryID \r\n                        LEFT JOIN Product_Brand PB ON PB.BrandID=P.BrandID  \r\n                        LEFT JOIN Customer C  ON C.CustomerID=ASD.CustomerID  \r\n                        LEFT JOIN System_Document SD  ON SD.SysDocID=ASD.[Doc ID]  \r\n                    \r\n                    ";
			text3 = text3 + " WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "'  ";
			if (fromBrand != "")
			{
				text3 = text3 + " AND P.BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "' ";
			}
			if (fromCategory != "")
			{
				text3 = text3 + " AND ( P.CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "'  OR (SELECT PC1.ParentCategoryID FROM Product_Category PC1 WHERE PC1.CategoryID=PC.CategoryID) BETWEEN '" + fromCategory + "' AND '" + toCategory + "')   ";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			if (fromSalesperson != "")
			{
				text3 = text3 + " AND (ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR  ASD.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' )";
			}
			if (fromSalespersonGroup != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
			}
			if (fromSalespersonDivision != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
			}
			if (fromSalespersonArea != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
			}
			if (fromSalespersonCountry != "")
			{
				text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
			}
			text3 += ") T GROUP BY T.MainCategory,T.ReportTo";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "SalespersonCommission", text3);
			return dataSet;
		}

		public DataSet GetSalesByMainCategory(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT T.MainCategory,T.SalespersonID,\r\n                        (SELECT PC1.CategoryName FROM Product_Category PC1 WHERE PC1.CategoryID=T.MainCategory) AS[Main],T.CustomerName,\r\n                        SUM(T.COGS) AS [COGS],SUM(T.SalesAmount) AS [SalesAmount],SUM(T.Quantity) AS [SalesQty]\r\n                        FROM (SELECT P.CategoryID,PC.CategoryName,C.CustomerName,\r\n                        (SELECT PC1.ParentCategoryID FROM Product_Category PC1 WHERE PC1.CategoryID=PC.CategoryID) AS [MainCategory],ASD.Quantity,\r\n                        (ASD.Amount) AS SalesAmount,(ASD.COGS) AS COGS,(ASD.Amount - ASD.COGS) AS Profit,ASD.AmountFC AS TotalFC,ASD.ReportTo AS SalespersonID\r\n                        FROM Axo_Sales_Detail ASD LEFT JOIN Salesperson SP ON SP.SalespersonID=ASD.ReportTo\r\n                        INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                        INNER JOIN Product_Category PC ON PC.CategoryID=P.CategoryID \r\n                        LEFT JOIN Customer C  ON C.CustomerID=ASD.CustomerID                            \r\n                                     \r\n                    \r\n                    ";
			text3 = text3 + " WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "'  ";
			if (customerIDs != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND (ASD.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "'  OR C.ParentCustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' )";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromArea != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			if (fromSalesperson != "")
			{
				text3 = text3 + " AND  ( ASD.ReportTo IN (SELECT SalespersonID FROM Salesperson WHERE SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "') OR ASD.SalespersonID IN (SELECT SalespersonID FROM Salesperson WHERE SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "')) ";
			}
			if (fromSalespersonGroup != "")
			{
				text3 = text3 + "AND  ( ASD.ReportTo IN (SELECT SalespersonID FROM Salesperson WHERE  GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "')  OR  ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "')) ";
			}
			if (fromSalespersonDivision != "")
			{
				text3 = text3 + " AND( ASD.ReportTo IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') OR ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') )";
			}
			if (fromSalespersonArea != "")
			{
				text3 = text3 + "AND (ASD.ReportTo IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "')  OR ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') )";
			}
			if (fromSalespersonCountry != "")
			{
				text3 = text3 + " AND( ASD.ReportTo IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') OR ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "')) ";
			}
			text3 += ") T GROUP BY T.SalespersonID,T.MainCategory,T.CustomerName";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "SalesByMainCategory", text3);
			return dataSet;
		}

		public DataSet GetSalesByMainCategorySummary(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT T.MainCategory,T.SalespersonID,\r\n                            (SELECT PC1.CategoryName FROM Product_Category PC1 WHERE PC1.CategoryID=T.MainCategory) AS[Main],\r\n                            SUM(T.COGS) AS [COGS],SUM(T.SalesAmount) AS [SalesAmount],SUM(T.Quantity) AS [SalesQty]\r\n                            FROM (SELECT P.CategoryID,PC.CategoryName,C.CustomerName,\r\n                            (SELECT PC1.ParentCategoryID FROM Product_Category PC1 WHERE PC1.CategoryID=PC.CategoryID) AS [MainCategory],ASD.Quantity,\r\n                            (ASD.Amount) AS SalesAmount,(ASD.COGS) AS COGS,(ASD.Amount - ASD.COGS) AS Profit,ASD.AmountFC AS TotalFC,ASD.ReportTo AS SalespersonID\r\n                            FROM Axo_Sales_Detail ASD LEFT JOIN Salesperson SP ON SP.SalespersonID=ASD.ReportTo\r\n                            INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                            INNER JOIN Product_Category PC ON PC.CategoryID=P.CategoryID \r\n                            LEFT JOIN Customer C  ON C.CustomerID=ASD.CustomerID                                           \r\n                    \r\n                    ";
			text3 = text3 + " WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "'  ";
			if (customerIDs != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND (ASD.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' OR C.ParentCustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ) ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromArea != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			if (fromSalesperson != "")
			{
				text3 = text3 + " AND  ( ASD.ReportTo IN (SELECT SalespersonID FROM Salesperson WHERE SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "') OR ASD.SalespersonID IN (SELECT SalespersonID FROM Salesperson WHERE SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "')) ";
			}
			if (fromSalespersonGroup != "")
			{
				text3 = text3 + "AND  ( ASD.ReportTo IN (SELECT SalespersonID FROM Salesperson WHERE  GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "')  OR  ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "')) ";
			}
			if (fromSalespersonDivision != "")
			{
				text3 = text3 + " AND( ASD.ReportTo IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') OR ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') )";
			}
			if (fromSalespersonArea != "")
			{
				text3 = text3 + "AND (ASD.ReportTo IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "')  OR ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') )";
			}
			if (fromSalespersonCountry != "")
			{
				text3 = text3 + " AND( ASD.ReportTo IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') OR ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') )";
			}
			text3 += ") T GROUP BY T.SalespersonID,T.MainCategory";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "SalesByMainCategory", text3);
			return dataSet;
		}

		public DataSet GetSalesComparisonDetailReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				DataSet dataSet = new DataSet();
				text3 = "SELECT Month(TransactionDate) AS #,DATENAME(month, TransactionDate) AS Month, ISNULL(SI.ReportTo, 'No SalesMan') AS [SalesPersonID], SP.FullName AS [SalesPerson],\r\n                        Salesperson_Group.GroupName,Division.DivisionName,Area.AreaName,Country.CountryName,\r\n                        SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) AS [CashSale], \r\n                        SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) AS [CreditSale]  \r\n                        FROM Sales_Invoice SI \r\n                        LEFT JOIN Salesperson SP ON SP.SalespersonID=SI.ReportTo\r\n                               LEFT OUTER JOIN Salesperson_Group ON Salesperson_Group.GroupID=SP.GroupID\r\n                               LEFT OUTER JOIN Division ON Division.DivisionID=SP.DivisionID\r\n                               LEFT OUTER JOIN Area ON Area.AreaID=SP.AreaID\r\n                               LEFT OUTER JOIN Country ON Country.CountryID=SP.CountryID\r\n        \r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				text3 += "GROUP BY SI.ReportTo,SP.FullName,GroupName,DivisionName,Areaname,CountryName,Month(TransactionDate),DATENAME(month, TransactionDate) ORDER BY SI.ReportTo";
				FillDataSet(dataSet, "Sales", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["TransactionDate"]
				};
				text3 = "SELECT Month(TransactionDate) AS #,DATENAME(month, TransactionDate) AS Month, ISNULL(SI.ReportTo, 'No SalesMan') AS [SalesPersonID], SP.FullName AS [SalesPerson],\r\n                        Salesperson_Group.GroupName,Division.DivisionName,Area.AreaName,Country.CountryName,\r\n                        SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) AS [CashSaleReturn], \r\n                        SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) AS [CreditSaleReturn]  \r\n                        FROM Sales_Return SI \r\n                        LEFT JOIN Salesperson SP ON SP.SalespersonID=SI.ReportTo\r\n                               LEFT OUTER JOIN Salesperson_Group ON Salesperson_Group.GroupID=SP.GroupID\r\n                               LEFT OUTER JOIN Division ON Division.DivisionID=SP.DivisionID\r\n                               LEFT OUTER JOIN Area ON Area.AreaID=SP.AreaID\r\n                               LEFT OUTER JOIN Country ON Country.CountryID=SP.CountryID        \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				text3 += "GROUP BY SI.ReportTo,SP.FullName,GroupName,DivisionName,Areaname,CountryName,Month(TransactionDate),DATENAME(month, TransactionDate) ORDER BY SI.ReportTo";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Sales", text3);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["TransactionDate"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesComparisonSummaryReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				DataSet dataSet = new DataSet();
				text3 = "SELECT Month(TransactionDate) AS #,DATENAME(month, TransactionDate) AS Month, ISNULL(SI.ReportTo, 'No SalesMan') AS [SalesPersonID], SP.FullName AS [SalesPerson],\r\n       Salesperson_Group.GroupName,Division.DivisionName,Area.AreaName,Country.CountryName,\r\n                        SUM( SI.Total-SI.Discount+SI.RoundOff) AS [Sale]\r\n                        FROM Sales_Invoice SI \r\n                        LEFT JOIN Salesperson SP ON SP.SalespersonID=SI.ReportTo\r\n                               LEFT OUTER JOIN Salesperson_Group ON Salesperson_Group.GroupID=SP.GroupID\r\n                               LEFT OUTER JOIN Division ON Division.DivisionID=SP.DivisionID\r\n                               LEFT OUTER JOIN Area ON Area.AreaID=SP.AreaID\r\n                               LEFT OUTER JOIN Country ON Country.CountryID=SP.CountryID      \r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				text3 += "GROUP BY SI.ReportTo,SP.FullName,GroupName,DivisionName,Areaname,CountryName,Month(TransactionDate),DATENAME(month, TransactionDate) ORDER BY SI.ReportTo";
				FillDataSet(dataSet, "Sales", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["TransactionDate"]
				};
				text3 = "SELECT Month(TransactionDate) AS #,DATENAME(month, TransactionDate) AS Month, ISNULL(SI.ReportTo, 'No SalesMan') AS [SalesPersonID], SP.FullName AS [SalesPerson],\r\n                         Salesperson_Group.GroupName,Division.DivisionName,Area.AreaName,Country.CountryName,\r\n                        SUM( SI.Total-SI.Discount+SI.RoundOff ) AS [SaleReturn]\r\n                        FROM Sales_Return SI \r\n                        LEFT JOIN Salesperson SP ON SP.SalespersonID=SI.ReportTo  \r\n                               LEFT OUTER JOIN Salesperson_Group ON Salesperson_Group.GroupID=SP.GroupID\r\n                               LEFT OUTER JOIN Division ON Division.DivisionID=SP.DivisionID\r\n                               LEFT OUTER JOIN Area ON Area.AreaID=SP.AreaID\r\n                               LEFT OUTER JOIN Country ON Country.CountryID=SP.CountryID\r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND SI.ReportTo IN (SELECT SalesPersonID as ReportTo FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				text3 += "GROUP BY SI.ReportTo,SP.FullName,GroupName,DivisionName,Areaname,CountryName\r\n,Month(TransactionDate),DATENAME(month, TransactionDate) ORDER BY SI.ReportTo";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Sales", text3);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["TransactionDate"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
