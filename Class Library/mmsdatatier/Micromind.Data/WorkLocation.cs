using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class WorkLocation : StoreObject
	{
		private const string WORKLOCATIONID_PARM = "@WorkLocationID";

		private const string WORKLOCATIONNAME_PARM = "@WorkLocationName";

		private const string JOBID_PARM = "@JobID";

		private const string INACTIVE_PARM = "@Inactive";

		private const string NOTE_PARM = "@Note";

		private const string WORKLOCATION_TABLE = "Work_Location";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public WorkLocation(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Work_Location", new FieldValue("WorkLocationID", "@WorkLocationID", isUpdateConditionField: true), new FieldValue("WorkLocationName", "@WorkLocationName"), new FieldValue("JobID", "@JobID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Work_Location", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@WorkLocationID", SqlDbType.NVarChar);
			parameters.Add("@WorkLocationName", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@WorkLocationID"].SourceColumn = "WorkLocationID";
			parameters["@WorkLocationName"].SourceColumn = "WorkLocationName";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertWorkLocation(WorkLocationData accountWorkLocationData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountWorkLocationData, "Work_Location", insertUpdateCommand);
				string text = accountWorkLocationData.WorkLocationTable.Rows[0]["WorkLocationID"].ToString();
				AddActivityLog("WorkLocation", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Work_Location", "WorkLocationID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateWorkLocation(WorkLocationData accountWorkLocationData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountWorkLocationData, "Work_Location", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountWorkLocationData.WorkLocationTable.Rows[0]["WorkLocationID"];
				UpdateTableRowByID("Work_Location", "WorkLocationID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountWorkLocationData.WorkLocationTable.Rows[0]["WorkLocationName"].ToString();
				AddActivityLog("WorkLocation", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Work_Location", "WorkLocationID", obj, sqlTransaction, isInsert: false);
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

		public WorkLocationData GetWorkLocation()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Work_Location");
			WorkLocationData workLocationData = new WorkLocationData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(workLocationData, "Work_Location", sqlBuilder);
			return workLocationData;
		}

		public bool DeleteWorkLocation(string workLocationID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Work_Location WHERE WorkLocationID = '" + workLocationID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Work Location", workLocationID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public WorkLocationData GetWorkLocationByID(string id, bool isPOS)
		{
			WorkLocationData workLocationData = new WorkLocationData();
			string textCommand = "SELECT * FROM Work_Location WHERE WorkLocationID='" + id + "' ";
			FillDataSet(workLocationData, "Work_Location", textCommand);
			return workLocationData;
		}

		public DataSet GetWorkLocationByFields(params string[] columns)
		{
			return GetWorkLocationByFields(null, isInactive: true, columns);
		}

		public DataSet GetWorkLocationByFields(string[] workLocationID, params string[] columns)
		{
			return GetWorkLocationByFields(workLocationID, isInactive: true, columns);
		}

		public DataSet GetWorkLocationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Work_Location");
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
				commandHelper.FieldName = "WorkLocationID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Work_Location";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Work_Location", sqlBuilder);
			return dataSet;
		}

		public DataSet GetWorkLocationList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT WorkLocationID [Code],WorkLocationName [Name]\r\n                           FROM Work_Location ";
			FillDataSet(dataSet, "Work_Location", textCommand);
			return dataSet;
		}

		public DataSet GetWorkLocationComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT WorkLocationID [Code], WorkLocationName [Name]\r\n                           FROM Work_Location ORDER BY WorkLocationID, WorkLocationName";
			FillDataSet(dataSet, "Work_Location", textCommand);
			return dataSet;
		}

		public DataSet GetSalesByWorkLocationSummaryReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT ISNULL(SD.WorkLocationID, 'No WorkLocation') AS [WorkLocationID], SP.WorkLocationName,\r\n                            SUM(ISNULL(SI.Discount, 0))  AS Discount, SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN SD.Amount ELSE 0 END) \r\n                            AS [CashSale], SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN SD.Amount ELSE 0 END) AS [CreditSale]  \r\n\t\t\t\t            FROM Sales_Invoice SI INNER JOIN Sales_Invoice_Detail SD ON SI.SysDocID=SD.SysDocID AND SI.VoucherID = SD.VoucherID\r\n                            LEFT OUTER JOIN WorkLocation SP ON SD.WorkLocationID = SP.WorkLocationID\r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				str = str + " AND SD.WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			str += " GROUP BY SD.WorkLocationID,SP.WorkLocationName ORDER BY SD.WorkLocationID";
			FillDataSet(dataSet, "Sales", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["WorkLocationID"]
			};
			str = "SELECT ISNULL(SD.WorkLocationID,'No WorkLocation') AS [WorkLocationID], SP.WorkLocationName,\r\n                            -1 * SUM(ISNULL(SI.Discount, 0)) AS DiscountReturn, SUM(Quantity * UnitPrice) AS  [SalesReturn]\r\n\t\t\t\t\t\t\tFROM Sales_Return SI INNER JOIN Sales_Return_Detail SD ON SI.SysDocID=SD.SysDocID AND SI.VoucherID = SD.VoucherID\r\n                            LEFT OUTER JOIN WorkLocation SP ON SD.WorkLocationID=SP.WorkLocationID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				str = str + " AND SD.WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			str += " GROUP BY SD.WorkLocationID, SP.WorkLocationName Order BY SD.WorkLocationID";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Sales", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["WorkLocationID"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetSalesByWorkLocationDetailReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT DISTINCT ISNULL(SD.WorkLocationID,'No WorkLocation') AS [WorkLocationID],WorkLocationName\r\n                             FROM Sales_Invoice_Detail SD \r\n                            INNER JOIN Sales_Invoice SI ON SI.SysDocID=SD.SysDocID  AND SI.VoucherID = SD.VoucherID  \r\n                            LEFT OUTER JOIN WorkLocation ON SD.WorkLocationID=WorkLocation.WorkLocationID ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				text3 = text3 + " AND SD.WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			text3 += " UNION ";
			text3 += "SELECT DISTINCT ISNULL(SD.WorkLocationID,'No WorkLocation') AS [WorkLocationID],WorkLocationName\r\n                    FROM Sales_Return_Detail SD \r\n                    INNER JOIN Sales_Return SI ON SI.SysDocID=SD.SysDocID  AND SI.VoucherID = SD.VoucherID\r\n                    LEFT OUTER JOIN WorkLocation ON SD.WorkLocationID=WorkLocation.WorkLocationID ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				text3 = text3 + " AND SD.WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			text3 += " ORDER BY WorkLocationID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Work_Location", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "SELECT DISTINCT SI.SysDocID, SI.VoucherID, ISNULL(WorkLocationID,'No WorkLocation') AS [WorkLocationID],TransactionDate,Note,  \r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type], \r\n                    CurrencyID,CurrencyRate, SI.Discount, SI.DiscountFC,  SD.Amount AS Total, SD.AmountFC AS TotalFC \r\n                    FROM Sales_Invoice SI INNER JOIN Sales_Invoice_Detail SD ON SI.SysDocID=SD.SysDocID AND SI.VoucherID = SD.VoucherID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				text3 = text3 + " AND WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			text3 += " UNION ";
			text3 = text3 + "SELECT DISTINCT SI.SysDocID, SI.VoucherID, ISNULL(WorkLocationID,'No WorkLocation') AS [WorkLocationID],TransactionDate,Note,  \r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type], \r\n                    CurrencyID, CurrencyRate, -1 * Discount, -1 * DiscountFC,  -1 * (Quantity * UnitPrice) AS Total, -1 * (Quantity * UnitPriceFC) AS TotalFC \r\n                    FROM Sales_Return SI INNER JOIN Sales_Return_Detail SD ON SI.SysDocID=SD.SysDocID AND SI.VoucherID = SD.VoucherID \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				text3 = text3 + " AND WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			text3 += " ORDER BY TransactionDate";
			FillDataSet(dataSet2, "Sales", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Sales Detail", dataSet.Tables["Work_Location"].Columns["WorkLocationID"], dataSet.Tables["Sales"].Columns["WorkLocationID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchaseByWorkLocationSummaryReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "Select ISNULL(SD.WorkLocationID,'No WorkLocation')AS [WorkLocationID], SP.WorkLocationName,\r\n                            SUM(Discount) AS Discount, SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN Total ELSE 0 END) \r\n                            AS [CashSale], SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN Total ELSE 0 END) \r\n                            AS [CreditSale]  FROM Purchase_Invoice SI INNER JOIN System_Document SD ON SI.SysDocID=SD.SysDocID\r\n                            LEFT OUTER JOIN WorkLocation SP ON SD.WorkLocationID=SP.WorkLocationID\r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				str = str + " AND WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			str += " GROUP BY SD.WorkLocationID,SP.WorkLocationName ORDER BY SD.WorkLocationID";
			FillDataSet(dataSet, "Purchase", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["WorkLocationID"]
			};
			str = "Select ISNULL(SD.WorkLocationID,'No WorkLocation')AS [WorkLocationID], SP.WorkLocationName,\r\n                            -1*SUM(Discount) AS DiscountReturn, SUM(Total) AS  [PurchaseReturn]  FROM Purchase_Return SI INNER JOIN System_Document SD ON SI.SysDocID=SD.SysDocID\r\n                            LEFT OUTER JOIN WorkLocation SP ON SD.WorkLocationID=SP.WorkLocationID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				str = str + " AND WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			str += " GROUP BY SD.WorkLocationID,SP.WorkLocationName Order BY SD.WorkLocationID";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Purchase", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["WorkLocationID"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetPurchaseByWorkLocationDetailReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "Select DISTINCT ISNULL(SD.WorkLocationID,'No WorkLocation') AS [WorkLocationID],WorkLocationName\r\n                        FROM System_Document SD \r\n                        INNER JOIN Purchase_Invoice SI ON SI.SysDocID=SD.SysDocID  \r\n                        LEFT OUTER JOIN WorkLocation ON SD.WorkLocationID=WorkLocation.WorkLocationID ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				text3 = text3 + " AND SID.WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			text3 += " UNION ";
			text3 += "Select DISTINCT ISNULL(SD.WorkLocationID,'No WorkLocation') AS [WorkLocationID],WorkLocationName\r\n                    FROM System_Document SD \r\n                    INNER JOIN Purchase_Return SI ON SI.SysDocID=SD.SysDocID  \r\n                    LEFT OUTER JOIN WorkLocation ON SD.WorkLocationID=WorkLocation.WorkLocationID ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				text3 = text3 + " AND SID.WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			text3 += " ORDER BY WorkLocationID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Work_Location", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select SI.SysDocID,VoucherID,ISNULL(WorkLocationID,'No WorkLocation') AS [WorkLocationID],TransactionDate,Note,  \r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type], \r\n                    CurrencyID,CurrencyRate,Discount,DiscountFC,  Total,TotalFC \r\n                    FROM Purchase_Invoice SI INNER JOIN System_Document SD ON SI.SysDocID=SD.SysDocID  \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				text3 = text3 + " AND WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			text3 += " UNION ";
			text3 = text3 + "Select SI.SysDocID,VoucherID,ISNULL(WorkLocationID,'No WorkLocation') AS [WorkLocationID],TransactionDate,Note,  \r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type], \r\n                    CurrencyID,CurrencyRate,-1*Discount,-1*DiscountFC,  -1 * Total, -1 * TotalFC \r\n                    FROM Purchase_Return SI INNER JOIN System_Document SD ON SI.SysDocID=SD.SysDocID  \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromWorkLocation != "")
			{
				text3 = text3 + " AND WorkLocationID BETWEEN '" + fromWorkLocation + "' AND '" + toWorkLocation + "' ";
			}
			text3 += " ORDER BY TransactionDate";
			FillDataSet(dataSet2, "Purchase", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Purchase Detail", dataSet.Tables["Work_Location"].Columns["WorkLocationID"], dataSet.Tables["Purchase"].Columns["WorkLocationID"], createConstraints: false);
			return dataSet;
		}
	}
}
