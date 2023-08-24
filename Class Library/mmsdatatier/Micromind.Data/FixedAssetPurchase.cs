using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Micromind.Data
{
	public sealed class FixedAssetPurchase : StoreObject
	{
		private const string FIXEDASSETPURCHASE_TABLE = "FixedAsset_Purchase";

		private const string FIXEDASSETPURCHASEDETAIL_TABLE = "FixedAsset_Purchase_Detail";

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

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ASSETID_PARM = "@AssetID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TAXOPTION_PARM = "@TaxOption";

		public FixedAssetPurchase(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateFixedAssetPurchaseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Purchase", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VendorID", "@VendorID"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Reference", "@Reference"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset_Purchase", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
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
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Purchase_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("AssetID", "@AssetID"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Description", "@Description"));
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
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@AssetID"].SourceColumn = "AssetID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(FixedAssetPurchaseData journalData)
		{
			return true;
		}

		public bool InsertUpdateFixedAssetPurchase(FixedAssetPurchaseData fixedAssetPurchaseData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateFixedAssetPurchaseCommand = GetInsertUpdateFixedAssetPurchaseCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = fixedAssetPurchaseData.FixedAssetPurchaseTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("FixedAsset_Purchase", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
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
					foreach (DataRow row in fixedAssetPurchaseData.FixedAssetPurchaseDetailTable.Rows)
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
				flag = (isUpdate ? (flag & Update(fixedAssetPurchaseData, "FixedAsset_Purchase", insertUpdateFixedAssetPurchaseCommand)) : (flag & Insert(fixedAssetPurchaseData, "FixedAsset_Purchase", insertUpdateFixedAssetPurchaseCommand)));
				foreach (DataRow row2 in fixedAssetPurchaseData.FixedAssetPurchaseDetailTable.Rows)
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
				if (fixedAssetPurchaseData.Tables["FixedAsset_Purchase_Detail"].Rows.Count > 0)
				{
					flag &= Insert(fixedAssetPurchaseData, "FixedAsset_Purchase_Detail", insertUpdateFixedAssetPurchaseCommand);
				}
				foreach (DataRow row3 in fixedAssetPurchaseData.FixedAssetPurchaseDetailTable.Rows)
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
				if (fixedAssetPurchaseData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(fixedAssetPurchaseData, sysDocID, text, isUpdate, sqlTransaction);
				}
				GLData journalData = CreatePurchaseGLData(fixedAssetPurchaseData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("FixedAsset_Purchase", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string text4 = "Tranaction";
				text4 = "Fixed Asset Purchase";
				flag = (isUpdate ? (flag & AddActivityLog(text4, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(text4, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "FixedAsset_Purchase", "VoucherID", sqlTransaction);
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

		private GLData CreatePurchaseGLData(FixedAssetPurchaseData fixedAssetPurchaseData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = fixedAssetPurchaseData.FixedAssetPurchaseTable.Rows[0];
			DataRow dataRow2 = fixedAssetPurchaseData.FixedAssetPurchaseTable.Rows[0];
			string sysDocID = dataRow2["SysDocID"].ToString();
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
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
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("AccountID");
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataTable.Columns["AccountID"]
			};
			dataTable.Columns.Add("Amount");
			FixedAsset fixedAsset = new FixedAsset(base.DBConfig);
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			string assetAccountID = "";
			foreach (DataRow row in fixedAssetPurchaseData.FixedAssetPurchaseDetailTable.Rows)
			{
				assetAccountID = fixedAsset.GetAssetAccountID(row["AssetID"].ToString(), "AssetAccountID", sqlTransaction);
				if (assetAccountID == "")
				{
					throw new CompanyException("Asset Account is not available for the selected asset: '" + row["AssetID"].ToString() + "'");
				}
				if (dataTable.Rows.Contains(assetAccountID))
				{
					DataRow dataRow5 = dataTable.AsEnumerable().FirstOrDefault((DataRow tt) => tt.Field<string>("AccountID") == assetAccountID);
					if (dataRow5 != null)
					{
						decimal.TryParse(dataRow5["Amount"].ToString(), out result2);
					}
					decimal.TryParse(row["Amount"].ToString(), out result);
					result2 += Math.Round(result, currencyDecimalPoints);
					dataRow5["Amount"] = result2.ToString();
				}
				else
				{
					dataTable.Rows.Add(assetAccountID, row["Amount"].ToString());
				}
			}
			DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
			dataRow6.BeginEdit();
			foreach (DataRow row2 in dataTable.Rows)
			{
				dataRow6 = gLData.JournalDetailsTable.NewRow();
				dataRow6.BeginEdit();
				dataRow6["JournalID"] = 0;
				dataRow6["AccountID"] = row2["AccountID"];
				dataRow6["Debit"] = row2["Amount"];
				dataRow6["DebitFC"] = DBNull.Value;
				dataRow6["Credit"] = DBNull.Value;
				dataRow6["CreditFC"] = DBNull.Value;
				dataRow6["Description"] = dataRow2["Note"];
				dataRow6.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow6);
			}
			decimal result3 = default(decimal);
			decimal.TryParse(dataRow2["TaxAmount"].ToString(), out result3);
			if (dataRow2["TaxOption"] != DBNull.Value)
			{
				byte.Parse(dataRow2["TaxOption"].ToString());
			}
			if (result3 > 0m && result3 > 0m)
			{
				if (fixedAssetPurchaseData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = fixedAssetPurchaseData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num = default(decimal);
					DataRow obj = array[i];
					dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					string text3 = "";
					text3 = obj["TaxItemID"].ToString();
					string text4 = "";
					string exp = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text3.Trim() + "'";
					object obj2 = ExecuteScalar(exp);
					if (obj2 != null)
					{
						text4 = obj2.ToString();
					}
					if (text4 == "")
					{
						throw new CompanyException("Purchase tax account is not set for tax item: " + text3 + ".");
					}
					decimal.TryParse(obj["TaxAmount"].ToString(), out num);
					dataRow6["AccountID"] = text4;
					dataRow6["PayeeID"] = dataRow2["VendorID"];
					dataRow6["PayeeType"] = "A";
					dataRow6["Debit"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow6["Credit"] = DBNull.Value;
					dataRow6["Description"] = dataRow2["Note"];
					dataRow6["Reference"] = dataRow2["Reference"];
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
			}
			dataRow6 = gLData.JournalDetailsTable.NewRow();
			dataRow6.BeginEdit();
			dataRow6["JournalID"] = 0;
			dataRow6["PayeeType"] = dataRow2["PayeeType"];
			dataRow6["PayeeID"] = dataRow2["PayeeID"];
			dataRow6["AccountID"] = text2;
			if (dataRow2["PayeeType"] != DBNull.Value)
			{
				dataRow6["IsARAP"] = true;
			}
			decimal result4 = default(decimal);
			decimal result5 = default(decimal);
			decimal.TryParse(dataRow2["TaxAmount"].ToString(), out result4);
			decimal.TryParse(dataRow2["Amount"].ToString(), out result5);
			dataRow6["Debit"] = DBNull.Value;
			dataRow6["DebitFC"] = DBNull.Value;
			dataRow6["Credit"] = dataRow2["Amount"];
			dataRow6["Credit"] = Math.Round(result5 + result4, currencyDecimalPoints);
			dataRow6["CreditFC"] = dataRow2["AmountFC"];
			dataRow6["Description"] = dataRow2["Note"];
			dataRow6["Reference"] = dataRow2["Reference"];
			dataRow6.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow6);
			return gLData;
		}

		public FixedAssetPurchaseData GetFixedAssetPurchaseByID(string sysDocID, string voucherID)
		{
			try
			{
				FixedAssetPurchaseData fixedAssetPurchaseData = new FixedAssetPurchaseData();
				string textCommand = "SELECT * FROM FixedAsset_Purchase WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetPurchaseData, "FixedAsset_Purchase", textCommand);
				if (fixedAssetPurchaseData == null || fixedAssetPurchaseData.Tables.Count == 0 || fixedAssetPurchaseData.Tables["FixedAsset_Purchase"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT FPD.*,FA.AssetName \r\n                        FROM FixedAsset_Purchase_Detail FPD INNER JOIN FixedAsset FA ON FPD.AssetID=FA.AssetID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetPurchaseData, "FixedAsset_Purchase_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetPurchaseData, "Tax_Detail", textCommand);
				return fixedAssetPurchaseData;
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
				string commandText = "DELETE FROM FixedAsset_Purchase_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
			FixedAssetPurchaseData fixedAssetPurchaseData = new FixedAssetPurchaseData();
			string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid FROM FixedAsset_Purchase_Detail SOD INNER JOIN FixedAsset_Purchase SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
			FillDataSet(fixedAssetPurchaseData, "FixedAsset_Purchase_Detail", textCommand, sqlTransaction);
			if (fixedAssetPurchaseData.FixedAssetPurchaseDetailTable.Rows.Count == 0)
			{
				return true;
			}
			bool result = false;
			bool.TryParse(fixedAssetPurchaseData.FixedAssetPurchaseDetailTable.Rows[0]["IsVoid"].ToString(), out result);
			if (!result)
			{
				foreach (DataRow row in fixedAssetPurchaseData.FixedAssetPurchaseDetailTable.Rows)
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
				FixedAssetPurchaseData fixedAssetPurchaseData = new FixedAssetPurchaseData();
				string textCommand = "SELECT PID.*,ISVOID FROM FixedAsset_Purchase_Detail PID INNER JOIN FixedAsset_Purchase PI ON PI.SysDocID=PID.SysDocID AND PI.VOucherID=PID.VoucherID\r\n                              WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
				FillDataSet(fixedAssetPurchaseData, "FixedAsset_Purchase_Detail", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM FixedAsset_Purchase WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(fixedAssetPurchaseData, "FixedAsset_Purchase", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(fixedAssetPurchaseData.FixedAssetPurchaseDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (result)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				ReduceAquesitionCost(sysDocID, voucherID, sqlTransaction);
				textCommand = "UPDATE FixedAsset_Purchase SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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

		public bool DeleteFixedAssetPurchase(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("FixedAsset_Purchase", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidFixedAssetPurchase(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteFixedAssetPurchaseDetailsRows(sysDocID, voucherID, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM FixedAsset_Purchase WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				string cmdText = "SELECT FP.*,(CASE PayeeType\r\n\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\tWHEN 'E' THEN Emp2.FirstName + ' ' + Emp2.LastName\r\n\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS PayeeName FROM FixedAsset_Purchase FP\r\n                                LEFT OUTER JOIN Account ON FP.PayeeID=Account.AccountID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Customer ON FP.PayeeID=Customer.CustomerID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Vendor ON FP.PayeeID=Vendor.VendorID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Employee Emp2 ON FP.PayeeID=Emp2.EmployeeID\r\n                                 WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "FixedAsset_Purchase", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["FixedAsset_Purchase"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT FPD.*,FA.AssetName\r\n                      FROM   FixedAsset_Purchase_Detail FPD INNER JOIN FixedAsset FA ON FPD.AssetID = FA.AssetID\r\n                        WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(dataSet, "FixedAsset_Purchase_Detail", cmdText);
				dataSet.Relations.Add("FixedAssetPurchase", new DataColumn[2]
				{
					dataSet.Tables["FixedAsset_Purchase"].Columns["SysDocID"],
					dataSet.Tables["FixedAsset_Purchase"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["FixedAsset_Purchase_Detail"].Columns["SysDocID"],
					dataSet.Tables["FixedAsset_Purchase_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["FixedAsset_Purchase_Detail"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["FixedAsset_Purchase_Detail"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["TotalInWords"].ToString(), out result);
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
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS V,  SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],\r\n\t\t\t\t\t\t\tReference,TransactionDate [Invoice Date], \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tINV.BuyerID [Purchaseperson],INV.CurrencyID AS Currency, INV.CurrencyRate AS [Cur Rate]\r\n\t\t\t\t\t\t\tFROM         FixedAsset_Purchase INV LEFT JOIN Vendor \r\n\t\t\t\t\t\t\t\tON VENDOR.VendorID=INV.VendorID where 1=1\r\n                            ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "FixedAsset_Purchase", sqlCommand);
			return dataSet;
		}
	}
}
