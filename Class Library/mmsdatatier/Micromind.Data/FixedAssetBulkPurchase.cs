using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FixedAssetBulkPurchase : StoreObject
	{
		private const string FIXEDASSETBULKPURCHASE_TABLE = "FixedAsset_BulkPurchase";

		private const string FIXEDASSETBULKPURCHASEDETAIL_TABLE = "FixedAsset_BulkPurchase_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string VENDORID_PARM = "@VendorID";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string BUYERID_PARM = "@BuyerID";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string ISVOID_PARM = "@IsVoid";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string ASSETCLASSID_PARM = "@AssetClassID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string ITEMAMOUNT_PARM = "@ItemAmount";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ASSETID_PARM = "@AssetID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string ASSETNAME_PARM = "@AssetName";

		private const string SERIALNUMBER_PARM = "@SerialNumber";

		private const string BARCODENUMBER_PARM = "@BarcodeNumber";

		public FixedAssetBulkPurchase(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateFixedAssetPurchaseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_BulkPurchase", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VendorID", "@VendorID"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("AssetClassID", "@AssetClassID"), new FieldValue("Quantity", "@Quantity"), new FieldValue("ItemAmount", "@ItemAmount"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset_BulkPurchase", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFixedAssetPurchaseCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFixedAssetPurchaseText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFixedAssetPurchaseText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@AssetClassID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@Quantity", SqlDbType.Decimal);
			parameters.Add("@ItemAmount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@AssetClassID"].SourceColumn = "AssetClassID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@ItemAmount"].SourceColumn = "ItemAmount";
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

		private string GetInsertUpdateFixedAssetPurchaseDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_BulkPurchase_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("AssetID", "@AssetID"), new FieldValue("Amount", "@Amount"), new FieldValue("AssetName", "@AssetName"), new FieldValue("SerialNumber", "@SerialNumber"), new FieldValue("BarcodeNumber", "@BarcodeNumber"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFixedAssetPurchaseDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFixedAssetPurchaseDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFixedAssetPurchaseDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@AssetID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@AssetName", SqlDbType.NVarChar);
			parameters.Add("@SerialNumber", SqlDbType.NVarChar);
			parameters.Add("@BarcodeNumber", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@AssetID"].SourceColumn = "AssetID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@AssetName"].SourceColumn = "AssetName";
			parameters["@SerialNumber"].SourceColumn = "SerialNumber";
			parameters["@BarcodeNumber"].SourceColumn = "BarcodeNumber";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(FixedAssetBulkPurchaseData journalData)
		{
			return true;
		}

		public bool InsertUpdateFixedAssetPurchase(FixedAssetBulkPurchaseData fixedAssetPurchaseData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateFixedAssetPurchaseCommand = GetInsertUpdateFixedAssetPurchaseCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = fixedAssetPurchaseData.FixedAssetBulkPurchaseTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("FixedAsset_BulkPurchase", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text2 = dataRow["PayeeID"].ToString();
				string a = dataRow["PayeeType"].ToString();
				string a2 = "";
				if (a == "C")
				{
					a2 = new Customers(base.DBConfig).GetCustomerARAccountID(sysDocID, text2);
				}
				else if (a == "V")
				{
					a2 = new Vendors(base.DBConfig).GetVendorAPAccountID(sysDocID, text2);
				}
				else if (a == "E")
				{
					a2 = new Employees(base.DBConfig).GetEmployeeAccountID(sysDocID, text2);
				}
				else if (a == "A")
				{
					a2 = text2;
				}
				if (a2 == "")
				{
					throw new Exception("AccountID should be selected for the payee if a payee is in transaction.");
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != baseCurrencyID)
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal num = default(decimal);
					foreach (DataRow row in fixedAssetPurchaseData.FixedAssetBulkPurchaseDetailTable.Rows)
					{
						decimal result = default(decimal);
						decimal.TryParse(row["AmountFC"].ToString(), out result);
						string currencyRateType = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
						if (currencyRateType == "M")
						{
							row["Amount"] = Math.Round(result * d, currencyDecimalPoints);
						}
						else
						{
							row["Amount"] = Math.Round(result / d, currencyDecimalPoints);
						}
						if (currencyRateType == "M")
						{
							num += Math.Round(result * d, currencyDecimalPoints);
						}
						else
						{
							num += Math.Round(result / d, currencyDecimalPoints);
						}
					}
					dataRow["Amount"] = num;
				}
				insertUpdateFixedAssetPurchaseCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(fixedAssetPurchaseData, "FixedAsset_BulkPurchase", insertUpdateFixedAssetPurchaseCommand)) : (flag & Insert(fixedAssetPurchaseData, "FixedAsset_BulkPurchase", insertUpdateFixedAssetPurchaseCommand)));
				foreach (DataRow row2 in fixedAssetPurchaseData.FixedAssetBulkPurchaseDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdateFixedAssetPurchaseCommand = GetInsertUpdateFixedAssetPurchaseDetailsCommand(isUpdate: false);
				insertUpdateFixedAssetPurchaseCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteFixedAssetPurchaseDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (fixedAssetPurchaseData.Tables["FixedAsset_BulkPurchase_Detail"].Rows.Count > 0)
				{
					flag &= Insert(fixedAssetPurchaseData, "FixedAsset_BulkPurchase_Detail", insertUpdateFixedAssetPurchaseCommand);
				}
				foreach (DataRow row3 in fixedAssetPurchaseData.FixedAssetBulkPurchaseDetailTable.Rows)
				{
					string text3 = row3["AssetID"].ToString();
					decimal result2 = default(decimal);
					_ = DateTime.MinValue;
					if (dataRow["TransactionDate"] != DBNull.Value)
					{
						DateTime.Parse(dataRow["TransactionDate"].ToString());
					}
					decimal.TryParse(row3["Amount"].ToString(), out result2);
					string exp = "UPDATE FixedAsset SET AquesitionCost = ISNULL(AquesitionCost,0) + " + result2.ToString() + " WHERE AssetID = '" + text3 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				GLData journalData = CreatePurchaseGLData(fixedAssetPurchaseData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("FixedAsset_BulkPurchase", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string text4 = "Tranaction";
				text4 = "Fixed Asset Purchase";
				flag = (isUpdate ? (flag & AddActivityLog(text4, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(text4, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "FixedAsset_BulkPurchase", "VoucherID", sqlTransaction);
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

		private GLData CreatePurchaseGLData(FixedAssetBulkPurchaseData fixedAssetPurchaseData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = fixedAssetPurchaseData.FixedAssetBulkPurchaseTable.Rows[0];
			DataRow dataRow2 = fixedAssetPurchaseData.FixedAssetBulkPurchaseTable.Rows[0];
			string sysDocID = dataRow2["SysDocID"].ToString();
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow2["TransactionDate"];
			dataRow3["SysDocID"] = dataRow2["SysDocID"];
			dataRow3["VoucherID"] = dataRow2["VoucherID"];
			dataRow3["Reference"] = dataRow2["Reference"];
			dataRow3["CurrencyID"] = dataRow2["CurrencyID"];
			dataRow3["CurrencyRate"] = dataRow2["CurrencyRate"];
			dataRow3["Note"] = dataRow2["Note"];
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
			string text = dataRow["PayeeID"].ToString();
			string a = dataRow["PayeeType"].ToString();
			string text2 = "";
			if (a == "C")
			{
				text2 = new Customers(base.DBConfig).GetCustomerARAccountID(sysDocID, text);
			}
			else if (a == "V")
			{
				text2 = new Vendors(base.DBConfig).GetVendorAPAccountID(sysDocID, text);
			}
			else if (a == "E")
			{
				text2 = new Employees(base.DBConfig).GetEmployeeAccountID(sysDocID, text);
			}
			else if (a == "A")
			{
				text2 = text;
			}
			if (text2 == "")
			{
				throw new Exception("AccountID should be selected for the payee if a payee is in transaction.");
			}
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["PayeeType"] = dataRow2["PayeeType"];
			dataRow4["PayeeID"] = dataRow2["PayeeID"];
			dataRow4["AccountID"] = text2;
			if (dataRow2["PayeeType"] != DBNull.Value)
			{
				dataRow4["IsARAP"] = true;
			}
			dataRow4["Debit"] = DBNull.Value;
			dataRow4["DebitFC"] = DBNull.Value;
			dataRow4["Credit"] = dataRow2["Amount"];
			dataRow4["CreditFC"] = dataRow2["AmountFC"];
			dataRow4["Description"] = dataRow2["Note"];
			dataRow4["Reference"] = dataRow2["Reference"];
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			FixedAsset fixedAsset = new FixedAsset(base.DBConfig);
			foreach (DataRow row in fixedAssetPurchaseData.FixedAssetBulkPurchaseDetailTable.Rows)
			{
				string assetAccountID = fixedAsset.GetAssetAccountID(row["AssetID"].ToString(), "AssetAccountID", sqlTransaction);
				if (assetAccountID == "")
				{
					throw new CompanyException("Asset Account is not available for the selected asset: '" + row["AssetID"].ToString() + "'");
				}
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = assetAccountID;
				dataRow4["Debit"] = row["Amount"];
				dataRow4["DebitFC"] = row["AmountFC"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				dataRow4["Description"] = row["Description"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		public FixedAssetBulkPurchaseData GetFixedAssetPurchaseByID(string sysDocID, string voucherID)
		{
			try
			{
				FixedAssetBulkPurchaseData fixedAssetBulkPurchaseData = new FixedAssetBulkPurchaseData();
				string textCommand = "SELECT * FROM FixedAsset_BulkPurchase WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetBulkPurchaseData, "FixedAsset_BulkPurchase", textCommand);
				if (fixedAssetBulkPurchaseData == null || fixedAssetBulkPurchaseData.Tables.Count == 0 || fixedAssetBulkPurchaseData.Tables["FixedAsset_BulkPurchase"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT FPD.*,FA.AssetName \r\n                        FROM FixedAsset_BulkPurchase_Detail FPD INNER JOIN FixedAsset FA ON FPD.AssetID=FA.AssetID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetBulkPurchaseData, "FixedAsset_BulkPurchase_Detail", textCommand);
				return fixedAssetBulkPurchaseData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteFixedAssetPurchaseDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				flag &= ReduceAquesitionCost(sysDocID, voucherID, sqlTransaction);
				string commandText = "DELETE FROM FixedAsset_BulkPurchase_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool ReduceAquesitionCost(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			FixedAssetBulkPurchaseData fixedAssetBulkPurchaseData = new FixedAssetBulkPurchaseData();
			string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid FROM FixedAsset_BulkPurchase_Detail SOD INNER JOIN FixedAsset_BulkPurchase SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
			FillDataSet(fixedAssetBulkPurchaseData, "FixedAsset_BulkPurchase_Detail", textCommand, sqlTransaction);
			if (fixedAssetBulkPurchaseData.FixedAssetBulkPurchaseDetailTable.Rows.Count == 0)
			{
				return true;
			}
			bool result = false;
			bool.TryParse(fixedAssetBulkPurchaseData.FixedAssetBulkPurchaseDetailTable.Rows[0]["IsVoid"].ToString(), out result);
			if (!result)
			{
				foreach (DataRow row in fixedAssetBulkPurchaseData.FixedAssetBulkPurchaseDetailTable.Rows)
				{
					string text = row["AssetID"].ToString();
					decimal result2 = default(decimal);
					decimal.TryParse(row["Amount"].ToString(), out result2);
					string exp = "UPDATE FixedAsset SET AquesitionCost = ISNULL(AquesitionCost,0) - " + result2.ToString() + " WHERE AssetID = '" + text + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				return flag;
			}
			return flag;
		}

		public bool VoidFixedAssetPurchase(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidFixedAssetPurchase(sysDocID, voucherID, isVoid, null);
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

		private bool VoidFixedAssetPurchase(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				FixedAssetBulkPurchaseData fixedAssetBulkPurchaseData = new FixedAssetBulkPurchaseData();
				string textCommand = "SELECT PID.*,ISVOID FROM FixedAsset_BulkPurchase_Detail PID INNER JOIN FixedAsset_BulkPurchase PI ON PI.SysDocID=PID.SysDocID AND PI.VOucherID=PID.VoucherID\r\n                              WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
				FillDataSet(fixedAssetBulkPurchaseData, "FixedAsset_BulkPurchase_Detail", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM FixedAsset_BulkPurchase WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(fixedAssetBulkPurchaseData, "FixedAsset_BulkPurchase", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(fixedAssetBulkPurchaseData.FixedAssetBulkPurchaseDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (result)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				ReduceAquesitionCost(sysDocID, voucherID, sqlTransaction);
				textCommand = "UPDATE FixedAsset_BulkPurchase SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Fixed Asset Purchase", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteFixedAssetPurchase(string sysDocID, string voucherID, DataTable dsres)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("FixedAsset_BulkPurchase", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidFixedAssetPurchase(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				foreach (DataRow row in dsres.Rows)
				{
					string str = row["AssetID"].ToString();
					string commandText = "DELETE FROM FixedAsset WHERE AssetID = '" + str + "'";
					flag = Delete(commandText, sqlTransaction);
				}
				flag &= DeleteFixedAssetPurchaseDetailsRows(sysDocID, voucherID, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM FixedAsset_BulkPurchase WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Fixed Asset Purchase", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetFixedAssetPurchaseToPrint(string sysDocID, string voucherID)
		{
			return GetFixedAssetPurchaseToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetFixedAssetPurchaseToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.VendorID,VendorName,TransactionDate,\r\n                                IsCash,SI.BuyerID,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName,\r\n                                ISNULL(CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,Reference,ISNULL(DiscountFC,Discount) AS Discount,\r\n                                ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(TotalFC,Total) AS Total,PONumber,SI.Note,\r\n\t\t\t\t\t\t\t\tContainerNumber,Port,BOLNumber,Shipper,ClearingAgent\r\n                                FROM  FixedAsset_BulkPurchase SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "FixedAsset_BulkPurchase", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["FixedAsset_BulkPurchase"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,ProductID,Description,ISNULL(UnitQuantity,Quantity) AS Quantity,\r\n                        ISNULL(UnitPriceFC,UnitPrice) AS UnitPrice,\r\n                        ISNULL(UnitQuantity,Quantity)*ISNULL(UnitPriceFC,UnitPrice) AS Total,UnitID,LocationID\r\n                        FROM   FixedAsset_BulkPurchase_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "FixedAsset_BulkPurchase_Detail", cmdText);
				dataSet.Relations.Add("VendorInvoice", new DataColumn[2]
				{
					dataSet.Tables["FixedAsset_BulkPurchase"].Columns["SysDocID"],
					dataSet.Tables["FixedAsset_BulkPurchase"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["FixedAsset_BulkPurchase_Detail"].Columns["SysDocID"],
					dataSet.Tables["FixedAsset_BulkPurchase_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["FixedAsset_BulkPurchase"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["FixedAsset_BulkPurchase"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					row["TotalInWords"] = NumToWord.GetNumInWords(result);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "select ISNULL(IsVoid,'False') AS V,  SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name], INV.LocationID,INV.Amount,\r\n\t\t\t\t\t\t\tReference,TransactionDate [Invoice Date], INV.PayeeID \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t from FixedAsset_BulkPurchase INV\r\n\r\n\t\t\t\t\t\t\t LEFT JOIN Vendor V ON INV.VendorID=V.VendorID\r\n\t\t\t\t\t\t\t  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "FixedAsset_BulkPurchase", sqlCommand);
			return dataSet;
		}
	}
}
