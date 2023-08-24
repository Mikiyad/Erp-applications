using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeProvision : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string PROVISIONTYPEID_PARM = "@ProvisionTypetID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string SERVICEPERIOD_PARM = "@ServicePeriod";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string DUEAMOUNT_PARM = "@DueAmount";

		private const string POSTEDAMOUNT_PARM = "@Posted";

		private const string CURRENTAMOUNT_PARM = "@CurrentAmount";

		private const string EMPLOYEEPROVISION_TABLE = "Employee_Provision";

		private const string EMPLOYEEPROVISIONDETAIL_TABLE = "Employee_Provision_Detail";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeeProvision(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateEmployeeProvisionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Provision", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Note", "@Note"), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ProvisionTypeID", "@ProvisionTypetID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Provision", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeProvisionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeProvisionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeProvisionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProvisionTypetID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProvisionTypetID"].SourceColumn = "ProvisionTypeID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Reference"].SourceColumn = "Reference";
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

		private string GetInsertUpdateEmployeeProvisionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Provision_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("ServicePeriod", "@ServicePeriod"), new FieldValue("CurrentAmount", "@CurrentAmount"), new FieldValue("DueAmount", "@DueAmount"), new FieldValue("Posted", "@Posted"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSEmployeeProvisionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeProvisionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeProvisionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ServicePeriod", SqlDbType.Decimal);
			parameters.Add("@CurrentAmount", SqlDbType.Decimal);
			parameters.Add("@DueAmount", SqlDbType.Decimal);
			parameters.Add("@Posted", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@ServicePeriod"].SourceColumn = "ServicePeriod";
			parameters["@CurrentAmount"].SourceColumn = "CurrentAmount";
			parameters["@DueAmount"].SourceColumn = "DueAmount";
			parameters["@Posted"].SourceColumn = "Posted";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateEmployeeProvision(EmployeeProvisionData employeeProvisionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateEmployeeProvisionCommand = GetInsertUpdateEmployeeProvisionCommand(isUpdate);
			try
			{
				DataRow dataRow = employeeProvisionData.EmployeeProvisionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Employee_Provision", "SysDocID", text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in employeeProvisionData.EmployeeProvisionDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteEmployeeProvisionDetailsRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateEmployeeProvisionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(employeeProvisionData, "Employee_Provision", insertUpdateEmployeeProvisionCommand)) : (flag & Insert(employeeProvisionData, "Employee_Provision", insertUpdateEmployeeProvisionCommand)));
				if (employeeProvisionData.Tables["Employee_Provision_Detail"].Rows.Count > 0)
				{
					insertUpdateEmployeeProvisionCommand = GetInsertUpdateSEmployeeProvisionDetailsCommand(isUpdate: false);
					insertUpdateEmployeeProvisionCommand.Transaction = sqlTransaction;
					flag &= Insert(employeeProvisionData, "Employee_Provision_Detail", insertUpdateEmployeeProvisionCommand);
				}
				GLData journalData = CreateGLData(employeeProvisionData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Employee_Provision", "SysDocID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Employee Provision";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Employee_Provision", "VoucherID", sqlTransaction);
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

		internal bool DeleteEmployeeProvisionDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Provision_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateGLData(EmployeeProvisionData employeeProvisionData, SqlTransaction sqlTransaction)
		{
			try
			{
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				GLData gLData = new GLData();
				DataRow dataRow = employeeProvisionData.EmployeeProvisionTable.Rows[0];
				dataRow["SysDocID"].ToString();
				dataRow["VoucherID"].ToString();
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.EmployeeProvision;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (int)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = dataRow["Note"].ToString();
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				decimal num = default(decimal);
				foreach (DataRow row in employeeProvisionData.EmployeeProvisionDetailTable.Rows)
				{
					num += decimal.Parse(row["CurrentAmount"].ToString());
				}
				string str = dataRow["ProvisionTypeID"].ToString();
				string textCommand = "SELECT ExpenseAccountID,ProvisionAccountID FROM Employee_Provision_Type WHERE ProvisionTypeID = '" + str + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "EmployeeProvision", textCommand, sqlTransaction);
				string text = "";
				string text2 = "";
				if (dataSet.Tables[0].Rows.Count <= 0)
				{
					throw new CompanyException("Account is not set for the provision type.");
				}
				text = dataSet.Tables[0].Rows[0]["ProvisionAccountID"].ToString();
				text2 = dataSet.Tables[0].Rows[0]["ExpenseAccountID"].ToString();
				DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = text2;
				dataRow4["PayeeID"] = "";
				dataRow4["PayeeType"] = "A";
				dataRow4["IsARAP"] = true;
				dataRow4["Credit"] = num;
				dataRow4["Description"] = dataRow["Note"].ToString();
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = text;
				dataRow4["PayeeID"] = "";
				dataRow4["PayeeType"] = "A";
				dataRow4["IsARAP"] = true;
				dataRow4["Debit"] = num;
				dataRow4["Description"] = dataRow["Note"].ToString();
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeProvisionData GetEmployeeProvisionByID(string sysDocID, string voucherID)
		{
			try
			{
				EmployeeProvisionData employeeProvisionData = new EmployeeProvisionData();
				string textCommand = "SELECT * FROM Employee_Provision EP WHERE VoucherID='" + voucherID + "' AND SysDocID = '" + sysDocID + "'";
				FillDataSet(employeeProvisionData, "Employee_Provision", textCommand);
				if (employeeProvisionData == null || employeeProvisionData.Tables.Count == 0 || employeeProvisionData.Tables["Employee_Provision"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT EPD.*,E.FirstName+' '+E.MiddleName+' '+E.LastName as EmployeeName,CONVERT(VARCHAR(10),JoiningDate,3)as JoiningDate\r\n                        FROM Employee_Provision_Detail EPD  LEFT JOIN Employee E ON E.EmployeeID=EPD.EmployeeID WHERE VoucherID='" + voucherID + "' AND SysDocID = '" + sysDocID + "'";
				FillDataSet(employeeProvisionData, "Employee_Provision_Detail", textCommand);
				return employeeProvisionData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteEmployeeProvision(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteEmployeeProvisionDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Salary_Deduction WHERE VoucherID = '" + voucherID + "' AND SysDocID = '" + sysDocID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee Provision", voucherID, activityType, sqlTransaction);
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

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = " SELECT EP.SysDocID,VoucherID,TransactionDate,EPT.ProvisionTypeName [Provision Type],Reference,Note FROM Employee_Provision EP \r\n                            LEFT JOIN Employee_Provision_Type EPT ON EPT.ProvisionTypeID=EP.ProvisionTypeID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Quote", sqlCommand);
			return dataSet;
		}

		public DataSet GetEmployeeList(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs, DateTime asOfDate, string voucherID)
		{
			DataSet dataSet = new DataSet();
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string str = "SELECT E.EmployeeID,E.FirstName+' '+E.MiddleName+' '+E.LastName as EmployeeName,CONVERT(VARCHAR(10),JoiningDate,3)as JoiningDate,Convert(decimal(10," + currencyDecimalPoints + "), ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2))  AS [Service Period], Convert(decimal(10," + currencyDecimalPoints + "), ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 1, 2))  AS Tenure2,Convert(decimal(10," + currencyDecimalPoints + "),SUM(CASE WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= 0 AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) < 1 THEN 0  WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= 1 AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) <=5 THEN ((Amount / 30) * 21) *  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) > 5   THEN ((Amount / 30) * 21 * 5)+  (((ROUND((CAST(DATEDIFF(DAY, JoiningDate, '" + asOfDate + "') AS FLOAT) ), 2)+1)/365)-5)*30*((Amount / 30))ELSE 0.00 END )) AS Graduity ,Convert(decimal(10," + currencyDecimalPoints + "),(SELECT ISNULL(SUM(ISNULL(CurrentAmount,0)),0) From Employee_Provision_Detail Where EmployeeID=E.EmployeeID AND voucherID<'" + voucherID + "')) as TotalPostedAmount,(SELECT   Isnull(sum(amount),0) from Employee_PayrollItem_Detail EPD where Paytype=1 and EPD.EmployeeID = E.EmployeeID ) GrossSalary FROM Employee E INNER JOIN Employee_PayrollItem_Detail EPD ON E.EmployeeID = EPD.EmployeeID INNER JOIN PayrollItem PI ON EPD.PayrollItemID = PI.PayrollItemID WHERE PI.InServiceBenefit = 1 AND ISNULL(E.IsTerminated, 0) = 0 ";
			if (EmployeeIDs != "")
			{
				str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (!showInactive)
			{
				str += " AND ISNULL(Status,1) = 1";
			}
			str += " GROUP BY E.EmployeeID, E.FirstName,E.MiddleName, E.LastName, JoiningDate ORDER BY E.EmployeeID";
			FillDataSet(dataSet, "Employee", str);
			return dataSet;
		}

		public DataSet GetEmployeeTicketDetails(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs, DateTime asOfDate, string voucherID)
		{
			string str = "SELECT  EmployeeID,FirstName AS [First Name],MiddleName AS [Middle Name],LastName AS [Last Name],CONVERT(VARCHAR(10),JoiningDate,3)as JoiningDate ,\r\n                                FirstName + ' ' +MiddleName + ' ' + LastName AS [EmployeeName],TicketRemarks,TicketPeriod,NumberOfTickets,TicketAmount,EOSRuleID FROM Employee E Where 1=1 ";
			if (EmployeeIDs != "")
			{
				str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (!showInactive)
			{
				str += " AND ISNULL(Status,1) = 1";
			}
			str += " ORDER BY E.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee", str);
			return dataSet;
		}
	}
}
