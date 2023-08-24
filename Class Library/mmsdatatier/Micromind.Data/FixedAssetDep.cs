using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FixedAssetDep : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string MONTH_PARM = "@Month";

		private const string YEAR_PARM = "@Year";

		private const string YEARVALUE_PARM = "@YearValue";

		private const string CURRENTVALUE_PARM = "@CurrentValue";

		private const string FIXEDASSETDEP_TABLE = "FixedAsset_Dep";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string FIXEDASSETID_PARM = "@FixedAssetID";

		private const string DEPAMOUNT_PARM = "@DepAmount";

		private const string ASSETACCOUNTID_PARM = "@AssetAccountID";

		private const string DEPACCOUNTID_PARM = "@DepAccountID";

		private const string FIXEDASSETDEPDETAIL_TABLE = "FixedAsset_Dep_Detail";

		public FixedAssetDep(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateFixedAssetDepText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Dep", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("Year", "@Year"), new FieldValue("Month", "@Month"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset_Dep", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFixedAssetDepCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFixedAssetDepText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFixedAssetDepText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Year", SqlDbType.Int);
			parameters.Add("@Month", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Year"].SourceColumn = "Year";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
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

		private string GetInsertUpdateFixedAssetDepDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Dep_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("FixedAssetID", "@FixedAssetID"), new FieldValue("DepAmount", "@DepAmount"), new FieldValue("CurrentValue", "@CurrentValue"), new FieldValue("YearValue", "@YearValue"), new FieldValue("Year", "@Year"), new FieldValue("Month", "@Month"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("AssetAccountID", "@AssetAccountID"), new FieldValue("DepAccountID", "@DepAccountID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFixedAssetDepDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFixedAssetDepDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFixedAssetDepDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@FixedAssetID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Year", SqlDbType.Int);
			parameters.Add("@Month", SqlDbType.TinyInt);
			parameters.Add("@CurrentValue", SqlDbType.Money);
			parameters.Add("@YearValue", SqlDbType.Money);
			parameters.Add("@DepAmount", SqlDbType.Money);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@AssetAccountID", SqlDbType.NVarChar);
			parameters.Add("@DepAccountID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@FixedAssetID"].SourceColumn = "FixedAssetID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@CurrentValue"].SourceColumn = "CurrentValue";
			parameters["@YearValue"].SourceColumn = "YearValue";
			parameters["@Year"].SourceColumn = "Year";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@DepAmount"].SourceColumn = "DepAmount";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@AssetAccountID"].SourceColumn = "AssetAccountID";
			parameters["@DepAccountID"].SourceColumn = "DepAccountID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(FixedAssetDepData journalData)
		{
			return true;
		}

		public bool InsertUpdateFixedAssetDep(FixedAssetDepData fixedAssetDepData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateFixedAssetDepCommand = GetInsertUpdateFixedAssetDepCommand(isUpdate);
			try
			{
				DataRow dataRow = fixedAssetDepData.FixedAssetDepTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("FixedAsset_Dep", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in fixedAssetDepData.FixedAssetDepDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdateFixedAssetDepCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (fixedAssetDepData.Tables["FixedAsset_Dep"].Rows.Count > 0)
					{
						flag &= Insert(fixedAssetDepData, "FixedAsset_Dep", insertUpdateFixedAssetDepCommand);
					}
				}
				else
				{
					flag &= Update(fixedAssetDepData, "FixedAsset_Dep", insertUpdateFixedAssetDepCommand);
				}
				insertUpdateFixedAssetDepCommand = GetInsertUpdateFixedAssetDepDetailsCommand(isUpdate: false);
				insertUpdateFixedAssetDepCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteFixedAssetDepDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (fixedAssetDepData.Tables["FixedAsset_Dep_Detail"].Rows.Count > 0)
				{
					flag &= Insert(fixedAssetDepData, "FixedAsset_Dep_Detail", insertUpdateFixedAssetDepCommand);
				}
				GLData journalData = CreateFixedAssetDepGLData(fixedAssetDepData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("FixedAsset_Dep", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Fixed Asset Depreciation";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "FixedAsset_Dep", "VoucherID", sqlTransaction);
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

		private GLData CreateFixedAssetDepGLData(FixedAssetDepData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.FixedAssetDepTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				decimal num = default(decimal);
				string text = "";
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.FixedAssetDep;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (byte)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = "Fixed Asset Depreciation";
				dataRow2["Note"] = dataRow["Description"];
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				string text2 = dataRow["SysDocID"].ToString();
				string text3 = dataRow["VoucherID"].ToString();
				int month = int.Parse(dataRow["Month"].ToString());
				int year = int.Parse(dataRow["Year"].ToString());
				DataSet dataSet = new DataSet();
				DateTime dateTime = new DateTime(year, month, 1).AddMonths(1).AddSeconds(-1.0);
				string textCommand = "SELECT AssetID, FC.AccumDepAccountID,FC.DepAccountID ClassDepAccountID, FL.DepAccountID AS LocationDepAccountID\r\n                                FROM FixedAsset FA INNER JOIN FixedAsset_Class FC ON FA.AssetClassID = FC.AssetClassID\r\n                                INNER JOIN FixedAsset_Location FL ON FL.AssetLocationID = FA.AssetLocationID \r\n                                WHERE AssetID IN (SELECT AssetID FROM FixedAsset_Dep_Detail WHERE VoucherID = '" + text3 + "' AnD SysDocID = '" + text2 + "')";
				FillDataSet(dataSet, "Asset", textCommand, sqlTransaction);
				textCommand = "SELECT AssetID,TransactionDate,LocationFromID,LocationToID, FL.DepAccountID \r\n                    FROM FixedAsset_Transfer FT INNER JOIN FixedAsset_Location FL ON FT.LocationFromID = FL.AssetLocationID\r\n                    WHERE FT.AssetID IN (SELECT AssetID FROM FixedAsset_Dep_Detail WHERE VoucherID = '" + text3 + "' AnD SysDocID = '" + text2 + "')";
				FillDataSet(dataSet, "Transfer", textCommand, sqlTransaction);
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				Hashtable hashtable2 = new Hashtable();
				ArrayList arrayList2 = new ArrayList();
				foreach (DataRow row in transactionData.FixedAssetDepDetailsTable.Rows)
				{
					decimal num2 = decimal.Parse(row["DepAmount"].ToString());
					string str = row["FixedAssetID"].ToString();
					DataRow[] array = dataSet.Tables["Asset"].Select("AssetID = '" + str + "'");
					DataRow dataRow3 = null;
					if (array.Length != 0)
					{
						dataRow3 = array[0];
					}
					if (dataRow3 == null)
					{
						throw new CompanyException("Asset class information not found for asset:" + str);
					}
					string text4 = dataRow3["AccumDepAccountID"].ToString();
					string text5 = dataRow3["LocationDepAccountID"].ToString();
					string text6 = dataRow3["ClassDepAccountID"].ToString();
					if (text4 == "")
					{
						throw new CompanyException("Accumulated depreciation account not found for asset:" + str);
					}
					if (text5 == "" && text6 == "")
					{
						throw new CompanyException("Depreciation account not found for asset:" + str);
					}
					text = text4;
					if (hashtable2.ContainsKey(text))
					{
						num = decimal.Parse(hashtable2[text].ToString());
						num += Math.Round(num2, currencyDecimalPoints);
						hashtable2[text] = num;
					}
					else
					{
						hashtable2.Add(text, Math.Round(num2, currencyDecimalPoints));
						arrayList2.Add(text);
					}
					if (text6 != "")
					{
						text = text6;
						if (hashtable.ContainsKey(text))
						{
							num = decimal.Parse(hashtable[text].ToString());
							num += Math.Round(num2, currencyDecimalPoints);
							hashtable[text] = num;
						}
						else
						{
							hashtable.Add(text, Math.Round(num2, currencyDecimalPoints));
							arrayList.Add(text);
						}
					}
					else
					{
						DataRow[] array2 = dataSet.Tables["Transfer"].Select("AssetID= '" + str + "'", "TransactionDate");
						int num3 = 1;
						int day = dateTime.Day;
						decimal d = decimal.Round(num2 / (decimal)day, currencyDecimalPoints);
						decimal d2 = default(decimal);
						foreach (DataRow obj2 in array2)
						{
							DateTime dateTime2 = DateTime.Parse(obj2["TransactionDate"].ToString());
							string str2 = obj2["LocationFromID"].ToString();
							string text7 = obj2["DepAccountID"].ToString();
							if (text7 == "")
							{
								throw new CompanyException("Depreciation account is not selected for location:" + str2);
							}
							int day2 = dateTime2.Day;
							int value = day2 - num3;
							decimal num4 = Math.Round(d * (decimal)value, currencyDecimalPoints);
							text = text7;
							if (hashtable.ContainsKey(text))
							{
								num = decimal.Parse(hashtable[text].ToString());
								num += Math.Round(num4, currencyDecimalPoints);
								hashtable[text] = num;
							}
							else
							{
								hashtable.Add(text, Math.Round(num4, currencyDecimalPoints));
								arrayList.Add(text);
							}
							d2 += num4;
							num3 = day2;
						}
						text = text5;
						if (hashtable.ContainsKey(text))
						{
							num = decimal.Parse(hashtable[text].ToString());
							num += Math.Round(num2 - d2, currencyDecimalPoints);
							hashtable[text] = num;
						}
						else
						{
							hashtable.Add(text, Math.Round(num2 - d2, currencyDecimalPoints));
							arrayList.Add(text);
						}
					}
				}
				DataRow dataRow4 = null;
				for (int j = 0; j < hashtable2.Count; j++)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					text = arrayList2[j].ToString();
					num = decimal.Parse(hashtable2[text].ToString());
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text;
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = num;
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
				for (int k = 0; k < hashtable.Count; k++)
				{
					text = arrayList[k].ToString();
					num = decimal.Parse(hashtable[text].ToString());
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text;
					dataRow4["Debit"] = num;
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public FixedAssetDepData GetFixedAssetDepByID(string sysDocID, string voucherID)
		{
			try
			{
				FixedAssetDepData fixedAssetDepData = new FixedAssetDepData();
				string textCommand = "SELECT * FROM FixedAsset_Dep WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetDepData, "FixedAsset_Dep", textCommand);
				if (fixedAssetDepData == null || fixedAssetDepData.Tables.Count == 0 || fixedAssetDepData.Tables["FixedAsset_Dep"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM FixedAsset_Dep_Detail TD\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetDepData, "FixedAsset_Dep_Detail", textCommand);
				return fixedAssetDepData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteFixedAssetDepDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM FixedAsset_Dep_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidFixedAssetDep(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (flag)
				{
					ActivityTypes activityType = ActivityTypes.Void;
					if (!isVoid)
					{
						activityType = ActivityTypes.Unvoid;
					}
					AddActivityLog("Fixed Asset Depreciation", voucherID, sysDocID, activityType, sqlTransaction);
				}
			}
			catch
			{
				throw;
			}
			return false;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "select   SysDocID [Doc ID],VoucherID [Doc Number],\r\n\t\t\t\t\t\t\tReference,TransactionDate [Invoice Date] , Description\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t from FixedAsset_Dep INV where 1=1\r\n\r\n\t\t\t\t\t\t\t  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "FixedAsset_Dep", sqlCommand);
			return dataSet;
		}

		public bool DeleteFixedAssetDep(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteFixedAssetDepDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM FixedAsset_Dep WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(61, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Fixed Asset Depreciation", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetFixedAssetDepToPrint(string sysDocID, string voucherID)
		{
			return GetFixedAssetDepToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetFixedAssetDepToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				string text2 = "SELECT FP.* from FixedAsset_Dep FP\r\n                                 WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "'";
				new SqlCommand(text2);
				FillDataSet(dataSet, "FixedAsset_Dep", text2);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["FixedAsset_Dep"].Rows.Count == 0)
				{
					return null;
				}
				text2 = "SELECT FPD.* ,F.*, FC.AssetClassName [Asset ClassName],FG.AssetGroupName [Asset GroupName],FL.AssetLocationName[Asset LocationName],\r\n                        D.DivisionName,DT.DepartmentName, FC.AssetAccountID \r\n                        , (select AccountName from Account A where A.AccountID = fc.assetaccountid) as AssetAccount\r\n                        , FC.DepAccountID \r\n                        , (select AccountName from Account A where A.AccountID = fc.DepAccountID ) as DepreciationAccount\r\n                        , FC.ProfitLossAccountID \r\n                        , (select AccountName from Account A where A.AccountID = fc.ProfitLossAccountID ) as ProfitLossAccount\r\n                        , FC.AccumDepAccountID \r\n                        , (select AccountName from Account A where A.AccountID = fc.AccumDepAccountID  ) as AccumDepAccount\r\n                        FROM   FixedAsset_Dep_Detail FPD \r\n                        LEFT OUTER JOIN FixedAsset F ON FPD.FixedAssetID=F.AssetID\r\n                        LEFT JOIN  FixedAsset_Class FC ON FC.AssetClassID = F.AssetClassID\r\n\t\t\t\t\t\tLEFT JOIN  FixedAsset_Group FG ON FG.AssetGroupID = F.AssetGroupID \r\n\t\t\t\t\t\tLEFT JOIN  FixedAsset_Location FL ON FL.AssetLocationID = F.AssetLocationID \r\n\t\t\t\t\t\tLEFT JOIN  Division D  ON D.DivisionID = F.DivisionID \r\n\t\t\t\t\t\tLEFT JOIN  Department DT  ON Dt.DepartmentID = F.DepartmentID\r\n                        WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(dataSet, "FixedAsset_Dep_Detail", text2);
				dataSet.Relations.Add("FixedAssetDep", new DataColumn[2]
				{
					dataSet.Tables["FixedAsset_Dep"].Columns["SysDocID"],
					dataSet.Tables["FixedAsset_Dep"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["FixedAsset_Dep_Detail"].Columns["SysDocID"],
					dataSet.Tables["FixedAsset_Dep_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
