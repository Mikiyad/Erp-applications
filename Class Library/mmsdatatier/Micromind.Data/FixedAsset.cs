using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FixedAsset : StoreObject
	{
		private const string ASSETID_PARM = "@AssetID";

		private const string ASSETNAME_PARM = "@AssetName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ASSET_TABLE = "FixedAsset";

		private const string AQUESITIONDATE_PARM = "@AquesitionDate";

		private const string AQUESITIONCOST_PARM = "@AquesitionCost";

		private const string ASSETCLASSID_PARM = "@AssetClassID";

		private const string ASSETGROUPID_PARM = "@AssetGroupID";

		private const string LIFE_PARM = "@Life";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string COMPANYDIVISIONID_PARM = "@CompanyDivisionID";

		private const string DEPARTMENTID_PARM = "@DepartmentID";

		private const string ASSETLOCATIONID_PARM = "@AssetLocationID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string ORIGINALVALUE_PARM = "@OriginalValue";

		private const string SALVAGEVALUE_PARM = "@SalvageValue";

		private const string INVOICENUMBER_PARM = "@InvoiceNumber";

		private const string PURCHASEDATE_PARM = "@PurchaseDate";

		private const string BOOKVALUE_PARM = "@BookValue";

		private const string SUPPLIERNAME_PARM = "@SupplierName";

		private const string PURCHASEREMARKS_PARM = "@PurchaseRemarks";

		private const string DEPMETHOD_PARM = "@DepMethod";

		private const string OPENINGDEPAMOUNT_PARM = "@OpeningDepAmount";

		private const string DEPPERCENT_PARM = "@DepPercent";

		private const string DEPFREQUENCY_PARM = "@DepFrequency";

		private const string DEPSTARTDATE_PARM = "@DepStartDate";

		private const string ACCUMDEP_PARM = "@AccumDep";

		private const string LASTDEPAMOUNT_PARM = "@LastDepAmount";

		private const string LASTDEPDATE_PARM = "@LastDepDate";

		private const string SERIALNUMBER_PARM = "@SerialNumber";

		private const string BARCODENUMBER_PARM = "@BarcodeNumber";

		private const string MODELNUMBER_PARM = "@ModelNumber";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string FIXEDASSETDEPSCHEDULE_TABLE = "FixedAsset_Dep_Schedule";

		private const string SCHEDULEDATE_PARM = "@ScheduleDate";

		private const string ISRECORDED_PARM = "@IsRecorded";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERNUMBER_PARM = "@VoucherNumber";

		private const string MONTH_PARM = "@Month";

		private const string YEAR_PARM = "@Year";

		private const string DEPAMOUNT_PARM = "@DepAmount";

		public FixedAsset(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset", new FieldValue("AssetID", "@AssetID", isUpdateConditionField: true), new FieldValue("AssetName", "@AssetName"), new FieldValue("AquesitionDate", "@AquesitionDate"), new FieldValue("AquesitionCost", "@AquesitionCost"), new FieldValue("AssetClassID", "@AssetClassID"), new FieldValue("AssetGroupID", "@AssetGroupID"), new FieldValue("Life", "@Life"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyDivisionID", "@CompanyDivisionID"), new FieldValue("DepartmentID", "@DepartmentID"), new FieldValue("AssetLocationID", "@AssetLocationID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("OriginalValue", "@OriginalValue"), new FieldValue("BookValue", "@BookValue"), new FieldValue("SalvageValue", "@SalvageValue"), new FieldValue("InvoiceNumber", "@InvoiceNumber"), new FieldValue("PurchaseDate", "@PurchaseDate"), new FieldValue("SupplierName", "@SupplierName"), new FieldValue("PurchaseRemarks", "@PurchaseRemarks"), new FieldValue("DepMethod", "@DepMethod"), new FieldValue("OpeningDepAmount", "@OpeningDepAmount"), new FieldValue("DepPercent", "@DepPercent"), new FieldValue("DepFrequency", "@DepFrequency"), new FieldValue("DepStartDate", "@DepStartDate"), new FieldValue("AccumDep", "@AccumDep"), new FieldValue("LastDepAmount", "@LastDepAmount"), new FieldValue("LastDepDate", "@LastDepDate"), new FieldValue("SerialNumber", "@SerialNumber"), new FieldValue("BarcodeNumber", "@BarcodeNumber"), new FieldValue("ModelNumber", "@ModelNumber"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@AssetID", SqlDbType.NVarChar);
			parameters.Add("@AssetName", SqlDbType.NVarChar);
			parameters.Add("@AquesitionDate", SqlDbType.DateTime);
			parameters.Add("@AquesitionCost", SqlDbType.Money);
			parameters.Add("@AssetClassID", SqlDbType.NVarChar);
			parameters.Add("@AssetGroupID", SqlDbType.NVarChar);
			parameters.Add("@Life", SqlDbType.Int);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyDivisionID", SqlDbType.NVarChar);
			parameters.Add("@DepartmentID", SqlDbType.NVarChar);
			parameters.Add("@AssetLocationID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@OriginalValue", SqlDbType.Money);
			parameters.Add("@SalvageValue", SqlDbType.Money);
			parameters.Add("@BookValue", SqlDbType.Money);
			parameters.Add("@InvoiceNumber", SqlDbType.NVarChar);
			parameters.Add("@PurchaseDate", SqlDbType.DateTime);
			parameters.Add("@SupplierName", SqlDbType.NVarChar);
			parameters.Add("@PurchaseRemarks", SqlDbType.NVarChar);
			parameters.Add("@DepMethod", SqlDbType.TinyInt);
			parameters.Add("@OpeningDepAmount", SqlDbType.Money);
			parameters.Add("@DepPercent", SqlDbType.Decimal);
			parameters.Add("@DepFrequency", SqlDbType.TinyInt);
			parameters.Add("@DepStartDate", SqlDbType.DateTime);
			parameters.Add("@AccumDep", SqlDbType.Money);
			parameters.Add("@LastDepAmount", SqlDbType.Money);
			parameters.Add("@LastDepDate", SqlDbType.DateTime);
			parameters.Add("@SerialNumber", SqlDbType.NVarChar);
			parameters.Add("@BarcodeNumber", SqlDbType.NVarChar);
			parameters.Add("@ModelNumber", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@AssetID"].SourceColumn = "AssetID";
			parameters["@AssetName"].SourceColumn = "AssetName";
			parameters["@AquesitionCost"].SourceColumn = "AquesitionCost";
			parameters["@AquesitionDate"].SourceColumn = "AquesitionDate";
			parameters["@AssetClassID"].SourceColumn = "AssetClassID";
			parameters["@AssetGroupID"].SourceColumn = "AssetGroupID";
			parameters["@Life"].SourceColumn = "Life";
			parameters["@BookValue"].SourceColumn = "BookValue";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyDivisionID"].SourceColumn = "CompanyDivisionID";
			parameters["@DepartmentID"].SourceColumn = "DepartmentID";
			parameters["@AssetLocationID"].SourceColumn = "AssetLocationID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@OriginalValue"].SourceColumn = "OriginalValue";
			parameters["@SalvageValue"].SourceColumn = "SalvageValue";
			parameters["@InvoiceNumber"].SourceColumn = "InvoiceNumber";
			parameters["@PurchaseDate"].SourceColumn = "PurchaseDate";
			parameters["@SupplierName"].SourceColumn = "SupplierName";
			parameters["@PurchaseRemarks"].SourceColumn = "PurchaseRemarks";
			parameters["@DepMethod"].SourceColumn = "DepMethod";
			parameters["@OpeningDepAmount"].SourceColumn = "OpeningDepAmount";
			parameters["@DepPercent"].SourceColumn = "DepPercent";
			parameters["@DepFrequency"].SourceColumn = "DepFrequency";
			parameters["@DepStartDate"].SourceColumn = "DepStartDate";
			parameters["@AccumDep"].SourceColumn = "AccumDep";
			parameters["@LastDepAmount"].SourceColumn = "LastDepAmount";
			parameters["@LastDepDate"].SourceColumn = "LastDepDate";
			parameters["@SerialNumber"].SourceColumn = "SerialNumber";
			parameters["@BarcodeNumber"].SourceColumn = "BarcodeNumber";
			parameters["@ModelNumber"].SourceColumn = "ModelNumber";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		private string GetInsertUpdateScheduleText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Dep_Schedule", new FieldValue("AssetID", "@AssetID"), new FieldValue("ScheduleDate", "@ScheduleDate"), new FieldValue("Month", "@Month"), new FieldValue("Year", "@Year"), new FieldValue("DepAmount", "@DepAmount"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateScheduleCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateScheduleText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateScheduleText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@AssetID", SqlDbType.NVarChar);
			parameters.Add("@ScheduleDate", SqlDbType.DateTime);
			parameters.Add("@Month", SqlDbType.TinyInt);
			parameters.Add("@Year", SqlDbType.Int);
			parameters.Add("@DepAmount", SqlDbType.Decimal);
			parameters["@AssetID"].SourceColumn = "AssetID";
			parameters["@ScheduleDate"].SourceColumn = "ScheduleDate";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@Year"].SourceColumn = "Year";
			parameters["@DepAmount"].SourceColumn = "DepAmount";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertAsset(FixedAssetData accountAssetData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountAssetData, "FixedAsset", insertUpdateCommand);
				string text = accountAssetData.AssetTable.Rows[0]["AssetID"].ToString();
				AddActivityLog("Fixed Asset", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FixedAsset", "AssetID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateAsset(FixedAssetData accountAssetData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountAssetData, "FixedAsset", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountAssetData.AssetTable.Rows[0]["AssetID"];
				UpdateTableRowByID("FixedAsset", "AssetID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountAssetData.AssetTable.Rows[0]["AssetName"].ToString();
				AddActivityLog("Fixed Asset", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FixedAsset", "AssetID", obj, sqlTransaction, isInsert: false);
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

		public FixedAssetData GetAsset()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FixedAsset");
			FixedAssetData fixedAssetData = new FixedAssetData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(fixedAssetData, "FixedAsset", sqlBuilder);
			return fixedAssetData;
		}

		public bool DeleteAsset(string assetID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM FixedAsset WHERE AssetID = '" + assetID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Fixed Asset", assetID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public FixedAssetData GetAssetByID(string id)
		{
			FixedAssetData fixedAssetData = new FixedAssetData();
			try
			{
				string textCommand = "SELECT * , ISNULL((SELECT SUM(FD.DepAmount) FROM FixedAsset_Dep_Detail FD WHERE FD.FixedAssetID = FA.AssetID),0) AS AccumulatedDep,\r\n                                ISNULL(FA.AquesitionCost,0) -  ISNULL(FA.OpeningDepAmount,0) - ISNULL((SELECT SUM(FD.DepAmount) FROM FixedAsset_Dep_Detail FD WHERE FD.FixedAssetID = FA.AssetID),0) AS CurrentValue\r\n                                FROM FixedAsset FA WHERE AssetID = '" + id + "'";
				FillDataSet(fixedAssetData, "FixedAsset", textCommand);
				return fixedAssetData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAssetByFields(params string[] columns)
		{
			return GetAssetByFields(null, isInactive: true, columns);
		}

		public DataSet GetAssetByFields(string[] assetID, params string[] columns)
		{
			return GetAssetByFields(assetID, isInactive: true, columns);
		}

		public DataSet GetAssetByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FixedAsset");
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
				commandHelper.FieldName = "AssetID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "FixedAsset";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "FixedAsset", sqlBuilder);
			return dataSet;
		}

		public string GetAssetAccountID(string assetID, string accountFieldName, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT " + accountFieldName + "\r\n                            FROM FixedAsset FA INNER JOIN FixedAsset_Class FAC ON FA.AssetClassID=FAC.AssetClassID\r\n                            WHERE AssetID = '" + assetID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null)
			{
				return obj.ToString();
			}
			return "";
		}

		public DataSet GetAssetList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AssetID [Asset Code],AssetName [Asset Name],FC.AssetClassName [Asset ClassName],FG.AssetGroupName [Asset GroupName],FL.AssetLocationName[Asset LocationName],D.DivisionName,DT.DepartmentName\r\n                           FROM FixedAsset F LEFT JOIN  FixedAsset_Class FC ON FC.AssetClassID = F.AssetClassID\r\n\t\t\t\t\t\t   LEFT JOIN  FixedAsset_Group FG ON FG.AssetGroupID = F.AssetGroupID \r\n\t\t\t\t\t\t   LEFT JOIN  FixedAsset_Location FL ON FL.AssetLocationID = F.AssetLocationID \r\n\t\t\t\t\t\t   LEFT JOIN  Division D  ON D.DivisionID = F.DivisionID \r\n\t\t\t\t\t\t\tLEFT JOIN  Department DT  ON Dt.DepartmentID = F.DepartmentID ";
			FillDataSet(dataSet, "FixedAsset", textCommand);
			return dataSet;
		}

		public DataSet GetAssetList(string fromAsset, string toAsset, string fromGroup, string toGroup, string fromClass, string toClass, string fromArea, string toArea)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT AssetID [Asset Code],AssetName [Asset Name],FC.AssetClassName [Asset ClassName],FG.AssetGroupName [Asset GroupName],FL.AssetLocationName[Asset LocationName],D.DivisionName,DT.DepartmentName\r\n                           FROM FixedAsset FA LEFT JOIN  FixedAsset_Class FC ON FC.AssetClassID = FA.AssetClassID\r\n\t\t\t\t\t\t   LEFT JOIN  FixedAsset_Group FG ON FG.AssetGroupID = FA.AssetGroupID \r\n\t\t\t\t\t\t   LEFT JOIN  FixedAsset_Location FL ON FL.AssetLocationID = FA.AssetLocationID \r\n\t\t\t\t\t\t   LEFT JOIN  Division D  ON D.DivisionID = FA.DivisionID \r\n\t\t\t\t\t\t\tLEFT JOIN  Department DT  ON Dt.DepartmentID = FA.DepartmentID where 1=1 ";
			if (fromAsset != "")
			{
				text = text + " AND AssetID>='" + fromAsset + "'";
			}
			if (toAsset != "")
			{
				text = text + " AND AssetID<='" + toAsset + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND FA.AssetClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND FA.AssetClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text = text + " AND FA.AssetGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text = text + " AND FA.AssetGroupID<='" + toGroup + "'";
			}
			if (fromArea != "")
			{
				text = text + " AND FA.AssetLocationID>='" + fromArea + "'";
			}
			if (toArea != "")
			{
				text = text + " AND FA.AssetLocationID<='" + toArea + "'";
			}
			FillDataSet(dataSet, "FixedAsset", text);
			return dataSet;
		}

		public DataSet GetAssetDepSchedule(string assetID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AssetID,ScheduleDate,DepAmount,ISNULL(IsRecorded,'False') AS Booked,TransactionDate,SysDocID,VoucherNumber\r\n                                FROM FixedAsset_Dep_Schedule ";
			FillDataSet(dataSet, "FixedAsset_Dep_Schedule", textCommand);
			return dataSet;
		}

		public DataSet GetAssetComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AssetID [Code],AssetName [Name]\r\n                           FROM FixedAsset ORDER BY AssetID,AssetName";
			FillDataSet(dataSet, "FixedAsset", textCommand);
			return dataSet;
		}

		public DataSet CalculateDepreciation(int year, int month, string assetID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DateTime dateTime = new DateTime(year, month, 1, 0, 0, 0).AddMonths(1).AddSeconds(-1.0);
				string text = StoreConfiguration.ToSqlDateTimeString(new DateTime(year, month, 1, 0, 0, 0).AddMonths(1).AddSeconds(-1.0));
				string text2 = StoreConfiguration.ToSqlDateTimeString(new DateTime(dateTime.Year, dateTime.Month, 1, 23, 59, 59, 0));
				string textCommand = " --Straight Line Depreciation items\r\n                                SELECT * FROM (\r\n                                SELECT *,ISNULL(AquesitionCost,0) - ISNULL(OpeningDepAmount,0) - ISNULL((SELECT SUM(ISNULL(Dep.DepAmount,0)) FROM FixedAsset_Dep_Detail DEP WHERE Dep.FixedAssetID = AssetID),0) AS CurrentValue,\r\n                                ISNULL(AquesitionCost,0) - ISNULL(AccumDep,0) - ISNULL((SELECT SUM(ISNULL(Dep.DepAmount,0)) FROM FixedAsset_Dep_Detail DEP WHERE Dep.FixedAssetID = AssetID AND Dep.Year < 2015),0) AS YearValue\r\n                                FROM FixedAsset WHERE  DepStartDate <= '" + text + "' AND ISNULL(DepMethod,1) = 2 AND DATEADD(Month,Life,DepStartDate) >= '" + text2 + "' AND ISNULL(Status,1) = 1\r\n                                AND NOT EXISTS (SELECT * FROM FixedAsset_Dep_Detail FAD WHERE FAD.FixedAssetID = AssetID AND FAD.Month = " + month + " AND FAD.Year = " + year + ") AND ISNULL(FixedAsset.Inactive, 'True') = 'False'\r\n                                UNION\r\n\r\n                                -- Reducing Balance Items\r\n                                SELECT *,ISNULL(AquesitionCost,0) - ISNULL(OPeningDepAmount,0) - ISNULL((SELECT SUM(ISNULL(Dep.DepAmount,0)) FROM FixedAsset_Dep_Detail DEP WHERE Dep.FixedAssetID = AssetID),0) AS CurrentValue,\r\n                                ISNULL(AquesitionCost,0)- ISNULL(OPeningDepAmount,0) - ISNULL(AccumDep,0) - ISNULL((SELECT SUM(ISNULL(Dep.DepAmount,0)) FROM FixedAsset_Dep_Detail DEP WHERE Dep.FixedAssetID = AssetID AND Dep.Year < 2015),0) AS YearValue\r\n                                FROM FixedAsset WHERE   DepStartDate <= '" + text + "' AND ISNULL(DepMethod,1) = 3\r\n                                 AND ISNULL(AquesitionCost,0) - ISNULL(AccumDep,0) - ISNULL((SELECT SUM(ISNULL(Dep.DepAmount,0)) FROM FixedAsset_Dep_Detail DEP WHERE Dep.FixedAssetID = AssetID),0) > ISNULL(SalvageValue,0) AND ISNULL(Status,1) = 1 AND ISNULL(FixedAsset.Inactive, 'True') = 'False'\r\n                                    AND NOT EXISTS (SELECT * FROM FixedAsset_Dep_Detail FAD WHERE FAD.FixedAssetID = AssetID AND FAD.Month = " + month + " AND FAD.Year = " + year + ") \r\n                                 ) AS Assets  where AssetID='" + assetID + "' ORDER BY AssetID";
				FillDataSet(dataSet, "FixedAsset", textCommand);
				FixedAssetDepData fixedAssetDepData = new FixedAssetDepData();
				foreach (DataRow row in dataSet.Tables["FixedAsset"].Rows)
				{
					FixedAssetDepMethods fixedAssetDepMethods = (FixedAssetDepMethods)int.Parse(row["DepMethod"].ToString());
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					int result6 = 0;
					decimal.TryParse(row["OpeningDepAmount"].ToString(), out result3);
					decimal.TryParse(row["SalvageValue"].ToString(), out result);
					decimal.TryParse(row["AquesitionCost"].ToString(), out result2);
					decimal.TryParse(row["CurrentValue"].ToString(), out result4);
					decimal.TryParse(row["YearValue"].ToString(), out result5);
					int.TryParse(row["Life"].ToString(), out result6);
					if (!(result4 == 0m) && !(result4 <= result))
					{
						DataRow dataRow2 = fixedAssetDepData.FixedAssetDepDetailsTable.NewRow();
						dataRow2["FixedAssetID"] = row["AssetID"].ToString();
						dataRow2["Month"] = month;
						dataRow2["Year"] = year;
						dataRow2["Description"] = row["AssetName"].ToString();
						dataRow2["YearValue"] = result5;
						dataRow2["CurrentValue"] = result4;
						decimal num = default(decimal);
						switch (fixedAssetDepMethods)
						{
						case FixedAssetDepMethods.StraightLine:
							num = ((result6 <= 0) ? (result2 - result) : Math.Round((result2 - result) / (decimal)result6, currencyDecimalPoints));
							break;
						case FixedAssetDepMethods.ReducingBalance:
						{
							decimal result7 = default(decimal);
							decimal.TryParse(row["DepPercent"].ToString(), out result7);
							num = Math.Round(result7 * result5 / 1200m, currencyDecimalPoints);
							break;
						}
						}
						dataRow2["DepAmount"] = num;
						fixedAssetDepData.FixedAssetDepDetailsTable.Rows.Add(dataRow2);
					}
				}
				return fixedAssetDepData;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertDepreciationSchedule(string assetID, SqlTransaction sqlTransaction)
		{
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				FixedAssetData assetByID = GetAssetByID(assetID);
				DataRow dataRow = assetByID.AssetTable.Rows[0];
				int num = int.Parse(dataRow["DepMethod"].ToString());
				int num2 = int.Parse(dataRow["DepFrequency"].ToString());
				DateTime dateTime = DateTime.Today;
				if (dataRow["DepStartDate"] != DBNull.Value)
				{
					dateTime = DateTime.Parse(dataRow["DepStartDate"].ToString());
				}
				if (num == 1)
				{
					return true;
				}
				int num3 = int.Parse(dataRow["Life"].ToString());
				decimal d = default(decimal);
				if (dataRow["AquesitionCost"] != DBNull.Value)
				{
					d = decimal.Parse(dataRow["AquesitionCost"].ToString());
				}
				decimal d2 = default(decimal);
				if (dataRow["SalvageValue"] != DBNull.Value)
				{
					d2 = decimal.Parse(dataRow["SalvageValue"].ToString());
				}
				if (d == 0m)
				{
					return true;
				}
				int num4 = 0;
				decimal num5 = default(decimal);
				switch (num2)
				{
				case 1:
				{
					num4 = num3;
					num5 = Math.Round((d - d2) / (decimal)num4, currencyDecimalPoints);
					dateTime = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddSeconds(-1.0);
					for (int j = 0; j < num3; j++)
					{
						DataRow dataRow3 = assetByID.FixedAssetDepSchedule.NewRow();
						dataRow3["DepAmount"] = num5;
						dataRow3["ScheduleDate"] = dateTime;
						dataRow3["AssetID"] = assetID;
						dataRow3["Month"] = dateTime.Month;
						dataRow3["Year"] = dateTime.Year;
						dataRow3["IsRecorded"] = true;
						dataRow3["TransactionDate"] = DateTime.Now;
						assetByID.FixedAssetDepSchedule.Rows.Add(dataRow3);
						dateTime = dateTime.AddMonths(1).AddSeconds(-1.0);
					}
					break;
				}
				case 2:
				{
					num4 = num3 / 12;
					num5 = Math.Round((d - d2) / (decimal)num4, currencyDecimalPoints);
					int month = int.Parse(new Databases(base.DBConfig).GetFieldValue("Company", "FiscalFirstMonth", "CompanyID", 1, sqlTransaction).ToString());
					dateTime = new DateTime(dateTime.Year, month, 1).AddMonths(1).AddSeconds(-1.0);
					for (int i = 0; i < num3; i++)
					{
						DataRow dataRow2 = assetByID.FixedAssetDepSchedule.NewRow();
						dataRow2["DepAmount"] = num5;
						dataRow2["ScheduleDate"] = dateTime;
						dataRow2["AssetID"] = assetID;
						dataRow2["IsRecorded"] = true;
						dataRow2["Month"] = dateTime.Month;
						dataRow2["Year"] = dateTime.Year;
						dataRow2["TransactionDate"] = DateTime.Now;
						assetByID.FixedAssetDepSchedule.Rows.Add(dataRow2);
						dateTime = dateTime.AddYears(1).AddSeconds(-1.0);
					}
					break;
				}
				}
				SqlCommand insertUpdateScheduleCommand = GetInsertUpdateScheduleCommand(isUpdate: false);
				insertUpdateScheduleCommand.Transaction = sqlTransaction;
				return Insert(assetByID, "FixedAsset_Dep_Schedule", insertUpdateScheduleCommand);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public DataSet GetAssetListReport(string fromAsset, string toAsset, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string FromDivision, string ToDivision, string FromDepartment, string ToDepartment, string FromCountry, string ToCountry, string FromCompany, string ToCompany, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT FA.AssetID,AssetName,PurchaseDate,FAC.AssetClassName,FAG.AssetGroupName,FAL.AssetLocationName,D.DepartmentName,DV.DivisionName, Life , \r\n                             Life - ( SELECT count ( VoucherID ) FROM FixedAsset_Dep_Detail DEP WHERE DEP.FixedAssetID = FA.AssetID ) AS [BalanceDep],FA.AquesitionCost,FA.OpeningDepAmount,\r\n                             ISNULL((SELECT SUM(FD.DepAmount) FROM FixedAsset_Dep_Detail FD WHERE FD.FixedAssetID = FA.AssetID),0) AS AccumulatedDep,\r\n                             ISNULL(FA.AquesitionCost,0) -  ISNULL(FA.OpeningDepAmount,0) - ISNULL((SELECT SUM(FD.DepAmount) FROM FixedAsset_Dep_Detail FD WHERE FD.FixedAssetID = FA.AssetID),0) AS BookValue\r\n                             FROM FixedAsset FA \r\n                             LEFT JOIN FixedAsset_Class FAC ON FA.AssetClassID=FAC.AssetClassID\r\n                             LEFT JOIN FixedAsset_Group FAG ON FA.AssetGroupID=FAG.AssetGroupID\r\n                             LEFT JOIN FixedAsset_Location FAL ON FA.AssetLocationID=FAL.AssetLocationID\r\n                             LEFT JOIN Department D ON FA.DepartmentID=D.DepartmentID\r\n                             LEFT JOIN Division DV ON FA.DivisionID=DV.DivisionID\r\n                             WHERE 1=1\r\n                             ";
			if (fromAsset != "")
			{
				text = text + " AND AssetID>='" + fromAsset + "'";
			}
			if (toAsset != "")
			{
				text = text + " AND AssetID<='" + toAsset + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND FA.AssetClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND FA.AssetClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text = text + " AND FA.AssetGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text = text + " AND FA.AssetGroupID<='" + toGroup + "'";
			}
			if (fromArea != "")
			{
				text = text + " AND FA.AssetLocationID>='" + fromArea + "'";
			}
			if (toArea != "")
			{
				text = text + " AND FA.AssetLocationID<='" + toArea + "'";
			}
			if (FromDivision != "")
			{
				text = text + " AND FA.DivisionID>='" + FromDivision + "'";
			}
			if (ToDivision != "")
			{
				text = text + " AND FA.DivisionID<='" + ToDivision + "'";
			}
			if (FromDepartment != "")
			{
				text = text + " AND FA.DepartmentID>='" + FromDepartment + "'";
			}
			if (ToDepartment != "")
			{
				text = text + " AND FA.DepartmentID<='" + ToDepartment + "'";
			}
			if (FromCountry != "")
			{
				text = text + " AND FAL.CountryID>='" + FromCompany + "'";
			}
			if (ToCountry != "")
			{
				text = text + " AND FAL.CountryID<='" + ToCountry + "'";
			}
			if (FromCompany != "")
			{
				text = text + " AND FAL.CompanyID>='" + FromCompany + "'";
			}
			if (ToCompany != "")
			{
				text = text + " AND FAL.CompanyID<='" + ToCompany + "'";
			}
			FillDataSet(dataSet, "Assets", text);
			return dataSet;
		}

		public DataSet GetAssetPurchaseDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT * FROM FixedAsset_Purchase FP \r\n                                LEFT JOIN FixedAsset_Purchase_Detail FPP ON FP.SysDocID=FPP.SysDocID AND FP.VoucherID=FPP.VoucherID\r\n                                  ";
				text3 = text3 + "WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND FPP.AssetID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND FPP.AssetID IN (SELECT AssetID FROM Product WHERE AssetClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND FPP.AssetID IN (SELECT AssetID FROM Product WHERE AssetGroupID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				text3 += "UNION SELECT FP.SysDocID, FP.VoucherID,FP.TransactionDate,FP.PayeeID,FP.PayeeType,FP.VendorID,FP.CurrencyID,FP.CurrencyRate,FP.Reference,FP.Quantity,\r\n\t\t\t\t\t\t\t\t  FP.LocationID,FP.AssetClassID,FP.ItemAmount,FP.Amount,FP.AmountFC,'' TaxOption,'' TaxGroupID,0 TaxAmount,FP.IsVoid,FP.Note,FP.BuyerID,FP.DateCreated,FP.DateUpdated,FP.CreatedBy,FP.UpdatedBy,\r\n                                  FPP.SysDocID,FPP.VoucherID,FPP.AssetID,FPP.AssetName,FPP.SerialNumber,FPP.BarcodeNumber,FPP.Description,FPP.RowIndex,FPP.Amount,FPP.AmountFC,'' TaxOption,'' TaxGroupID,0 TaxAmount,0 TaxAmountFC FROM FixedAsset_BulkPurchase FP \r\n                                LEFT JOIN FixedAsset_BulkPurchase_Detail FPP ON FP.SysDocID=FPP.SysDocID AND FP.VoucherID=FPP.VoucherID\r\n                                  ";
				text3 = text3 + "WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND FPP.AssetID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND FPP.AssetID IN (SELECT AssetID FROM Product WHERE AssetClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND FPP.AssetID IN (SELECT AssetID FROM Product WHERE AssetGroupID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "FixedAsset_Purchase", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAssetSaleDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT * FROM FixedAsset_Sale FS\r\n                               LEFT JOIN FixedAsset_Sale_Detail FSD ON FS.SysDocID=FSD.SysDocID AND FS.VoucherID=FSD.VoucherID\r\n\r\n                                  ";
				text3 = text3 + "WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND FSD.AssetID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND FSD.AssetID IN (SELECT AssetID FROM Product WHERE AssetClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND FSD.AssetID IN (SELECT AssetID FROM Product WHERE AssetGroupID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "FixedAsset_Sale", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAssetTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				showitemwithTansactions = true;
				string text3 = "SELECT FA.*,FAC.AssetClassName,FAG.AssetGroupName,FAL.AssetLocationName,D.DepartmentName,DV.DivisionName\r\n                        FROM FixedAsset FA LEFT JOIN FixedAsset_Class FAC ON FA.AssetClassID=FAC.AssetClassID\r\n                        LEFT JOIN FixedAsset_Group FAG ON FA.AssetGroupID=FAG.AssetGroupID\r\n                        LEFT JOIN FixedAsset_Location FAL ON FA.AssetLocationID=FAL.AssetLocationID\r\n                        LEFT JOIN Department D ON FA.DepartmentID=D.DepartmentID\r\n                        LEFT JOIN Division DV ON FA.DivisionID=DV.DivisionID \r\n                        WHERE 1=1";
				if (fromItem != "")
				{
					text3 = text3 + " AND FA.AssetID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND FA.AssetClassID  BETWEEN '" + fromClass + "' AND '" + toClass + "' ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND FA.AssetGroupID  BETWEEN '" + fromCategory + "' AND '" + toCategory + "' ";
				}
				if (fromWarehouse != "" || toWarehouse != "")
				{
					text3 = text3 + " AND FA.AssetLocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (showitemwithTansactions)
				{
					text3 = text3 + "AND FA.AssetID IN(SELECT TEMP.AssetID FROM(SELECT distinct FDD.FixedAssetID AS AssetID, FD.TransactionDate    FROM FixedAsset_Dep FD LEFT JOIN FixedAsset_Dep_Detail FDD ON FD.SysDocID=FDD.SysDocID AND \r\n                            FD.VoucherID=FDD.VoucherID   UNION  SELECT distinct FPP.AssetID AS AssetID, FP.TransactionDate FROM FixedAsset_Purchase FP    LEFT JOIN FixedAsset_Purchase_Detail FPP ON FP.SysDocID=FPP.SysDocID AND FP.VoucherID=FPP.VoucherID \r\n                            UNION SELECT distinct FSD.AssetID AS AssetID, FS.TransactionDate  FROM FixedAsset_Sale FS\r\n                           LEFT JOIN FixedAsset_Sale_Detail FSD ON FS.SysDocID=FSD.SysDocID AND FS.VoucherID=FSD.VoucherID)AS TEMP WHERE TEMP.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "')";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "FixedAsset", text3);
				text3 = "SELECT * FROM (SELECT FD.SysDocID,FD.VoucherID,FD.TransactionDate,FDD.FixedAssetID,FDD.Description,SD.DocName,FDD.DepAmount\r\n                        FROM FixedAsset_Dep FD \r\n                        LEFT JOIN FixedAsset_Dep_Detail FDD ON FD.SysDocID=FDD.SysDocID AND FD.VoucherID=FDD.VoucherID\r\n                        LEFT JOIN System_Document SD ON SD.SysDocID=FD.SysDocID\r\n                        UNION\r\n                        SELECT FP.SysDocID,FP.VoucherID,FP.TransactionDate,FPP.AssetID,FPP.Description,SD.DocName,FPP.Amount\r\n                        FROM FixedAsset_Purchase FP\r\n                        LEFT JOIN FixedAsset_Purchase_Detail FPP ON FP.SysDocID=FPP.SysDocID AND FP.VoucherID=FPP.VoucherID\r\n                        LEFT JOIN System_Document SD ON SD.SysDocID=FP.SysDocID\r\n                        UNION\r\n                        SELECT FS.SysDocID,FS.VoucherID,FS.TransactionDate,FSD.AssetID,FSD.Description,SD.DocName,FSD.Amount\r\n                        FROM FixedAsset_Sale FS\r\n                        LEFT JOIN FixedAsset_Sale_Detail FSD ON FS.SysDocID=FSD.SysDocID AND FS.VoucherID=FSD.VoucherID\r\n                        LEFT JOIN System_Document SD ON SD.SysDocID=FS.SysDocID";
				text3 = text3 + " ) AS FA  WHERE FA.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				FillDataSet(dataSet, "FixedAsset_Tran", text3);
				dataSet.Relations.Add("FixedAsset_Rel", new DataColumn[1]
				{
					dataSet.Tables["FixedAsset"].Columns["AssetID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["FixedAsset_Tran"].Columns["FixedAssetID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAssetDepreciationReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems)
		{
			try
			{
				CommonLib.ToSqlDateTimeString(from);
				string str = CommonLib.ToSqlDateTimeString(to);
				if (fromWarehouse != "" || toWarehouse != "")
				{
					_ = " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				string str2 = "SELECT * FROM FixedAsset_Dep FD \r\n                                LEFT JOIN FixedAsset_Dep_Detail FDD ON FD.SysDocID=FDD.SysDocID AND FD.VoucherID=FDD.VoucherID \r\n                                LEFT JOIN FixedAsset FA ON FA.AssetID=FDD.FixedAssetID";
				str2 = str2 + "   WHERE FD.TransactionDate < '" + str + "' ";
				if (fromItem != "")
				{
					str2 = str2 + " AND FA.AssetID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str2 = str2 + " AND FA.AssetClassID  BETWEEN '" + fromClass + "' AND '" + toClass + "' ";
				}
				if (fromCategory != "")
				{
					str2 = str2 + " AND FA.AssetGroupID  BETWEEN '" + fromCategory + "' AND '" + toCategory + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "FixedAsset_Dep", str2);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAssetTransferReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				showitemwithTansactions = true;
				DataSet dataSet = new DataSet();
				string text3 = "SELECT *,FA.AssetName,\r\n                                (SELECT DivisionName FROM Division WHERE DivisionID=FAT.DivisionFromID) AS FromDiv,\r\n                                (SELECT DivisionName FROM Division WHERE DivisionID=FAT.DivisionToID) AS ToDiv,\r\n                                (SELECT AssetLocationName FROM FixedAsset_Location WHERE AssetLocationID=FAT.LocationFromID) AS FromLoc,\r\n                                (SELECT AssetLocationName FROM FixedAsset_Location WHERE AssetLocationID=FAT.LocationToID) AS ToLoc,\r\n                                (SELECT DepartmentName FROM Department WHERE DepartmentID=FAT.DepartmentFromID) AS FromDep,\r\n                                (SELECT DepartmentName FROM Department WHERE DepartmentID=FAT.DepartmentToID) AS ToLoc\r\n                                FROM FixedAsset_Transfer FAT LEFT JOIN FixedAsset FA ON FAT.AssetID=FA.AssetID  \r\n                               WHERE FAT.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND FA.AssetID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND FA.AssetClassID  BETWEEN '" + fromClass + "' AND '" + toClass + "' ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND FA.AssetGroupID  BETWEEN '" + fromCategory + "' AND '" + toCategory + "' ";
				}
				if (fromWarehouse != "" || toWarehouse != "")
				{
					text3 = text3 + " AND FA.AssetLocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				FillDataSet(dataSet, "FixedAsset_Trans", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public FixedAssetData GetAssetByClassID(string id)
		{
			FixedAssetData fixedAssetData = new FixedAssetData();
			try
			{
				string textCommand = "SELECT TOP 1 * , ISNULL((SELECT SUM(FD.DepAmount) FROM FixedAsset_Dep_Detail FD WHERE FD.FixedAssetID = FA.AssetID),0) AS AccumulatedDep,\r\n                                ISNULL(FA.AquesitionCost,0) -  ISNULL(FA.OpeningDepAmount,0) - ISNULL((SELECT SUM(FD.DepAmount) FROM FixedAsset_Dep_Detail FD WHERE FD.FixedAssetID = FA.AssetID),0) AS CurrentValue\r\n                                FROM FixedAsset FA WHERE AssetClassID = '" + id + "' order by DateCreated desc";
				FillDataSet(fixedAssetData, "FixedAsset", textCommand);
				return fixedAssetData;
			}
			catch
			{
				throw;
			}
		}
	}
}
