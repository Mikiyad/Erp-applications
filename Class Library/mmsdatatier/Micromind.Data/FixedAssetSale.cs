using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FixedAssetSale : StoreObject
	{
		private const string FIXEDASSETSALE_TABLE = "FixedAsset_Sale";

		private const string FIXEDASSETSALEDETAIL_TABLE = "FixedAsset_Sale_Detail";

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

		public FixedAssetSale(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateFixedAssetSaleText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Sale", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Reference", "@Reference"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset_Sale", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFixedAssetSaleCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFixedAssetSaleText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFixedAssetSaleText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
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
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
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

		private string GetInsertUpdateFixedAssetSaleDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Sale_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("AssetID", "@AssetID"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFixedAssetSaleDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFixedAssetSaleDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFixedAssetSaleDetailsText(isUpdate: false), base.DBConfig.Connection);
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

		private bool ValidateData(FixedAssetSaleData journalData)
		{
			return true;
		}

		public bool InsertUpdateFixedAssetSale(FixedAssetSaleData fixedAssetSaleData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateFixedAssetSaleCommand = GetInsertUpdateFixedAssetSaleCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = fixedAssetSaleData.FixedAssetSaleTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("FixedAsset_Sale", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
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
					foreach (DataRow row in fixedAssetSaleData.FixedAssetSaleDetailTable.Rows)
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
				insertUpdateFixedAssetSaleCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(fixedAssetSaleData, "FixedAsset_Sale", insertUpdateFixedAssetSaleCommand)) : (flag & Insert(fixedAssetSaleData, "FixedAsset_Sale", insertUpdateFixedAssetSaleCommand)));
				foreach (DataRow row2 in fixedAssetSaleData.FixedAssetSaleDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdateFixedAssetSaleCommand = GetInsertUpdateFixedAssetSaleDetailsCommand(isUpdate: false);
				insertUpdateFixedAssetSaleCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteFixedAssetSaleDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (fixedAssetSaleData.Tables["FixedAsset_Sale_Detail"].Rows.Count > 0)
				{
					flag &= Insert(fixedAssetSaleData, "FixedAsset_Sale_Detail", insertUpdateFixedAssetSaleCommand);
				}
				foreach (DataRow row3 in fixedAssetSaleData.FixedAssetSaleDetailTable.Rows)
				{
					row3["AssetID"].ToString();
					decimal result2 = default(decimal);
					_ = DateTime.MinValue;
					if (dataRow["TransactionDate"] != DBNull.Value)
					{
						DateTime.Parse(dataRow["TransactionDate"].ToString());
					}
					decimal.TryParse(row3["Amount"].ToString(), out result2);
				}
				if (fixedAssetSaleData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(fixedAssetSaleData, sysDocID, text, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateSaleGLData(fixedAssetSaleData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("FixedAsset_Sale", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string text3 = "Tranaction";
				text3 = "Fixed Asset Sale";
				flag = (isUpdate ? (flag & AddActivityLog(text3, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(text3, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "FixedAsset_Sale", "VoucherID", sqlTransaction);
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

		private GLData CreateSaleGLData(FixedAssetSaleData fixedAssetSaleData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = fixedAssetSaleData.FixedAssetSaleTable.Rows[0];
			DataRow dataRow2 = fixedAssetSaleData.FixedAssetSaleTable.Rows[0];
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
			dataRow4["Debit"] = dataRow2["Amount"];
			dataRow4["DebitFC"] = dataRow2["AmountFC"];
			dataRow4["Credit"] = DBNull.Value;
			dataRow4["CreditFC"] = DBNull.Value;
			dataRow4["Description"] = dataRow2["Note"];
			dataRow4["Reference"] = dataRow2["Reference"];
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			FixedAsset fixedAsset = new FixedAsset(base.DBConfig);
			foreach (DataRow row in fixedAssetSaleData.FixedAssetSaleDetailTable.Rows)
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
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["Credit"] = row["Amount"];
				dataRow4["CreditFC"] = row["AmountFC"];
				dataRow4["Description"] = row["Description"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		public FixedAssetSaleData GetFixedAssetSaleByID(string sysDocID, string voucherID)
		{
			try
			{
				FixedAssetSaleData fixedAssetSaleData = new FixedAssetSaleData();
				string textCommand = "SELECT * FROM FixedAsset_Sale WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetSaleData, "FixedAsset_Sale", textCommand);
				if (fixedAssetSaleData == null || fixedAssetSaleData.Tables.Count == 0 || fixedAssetSaleData.Tables["FixedAsset_Sale"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT FPD.*,FA.AssetName \r\n                        FROM FixedAsset_Sale_Detail FPD INNER JOIN FixedAsset FA ON FPD.AssetID=FA.AssetID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetSaleData, "FixedAsset_Sale_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetSaleData, "Tax_Detail", textCommand);
				return fixedAssetSaleData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteFixedAssetSaleDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				flag &= ReduceAquesitionCost(sysDocID, voucherID, sqlTransaction);
				string commandText = "DELETE FROM FixedAsset_Sale_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
			FixedAssetSaleData fixedAssetSaleData = new FixedAssetSaleData();
			string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid FROM FixedAsset_Sale_Detail SOD INNER JOIN FixedAsset_Sale SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
			FillDataSet(fixedAssetSaleData, "FixedAsset_Sale_Detail", textCommand, sqlTransaction);
			if (fixedAssetSaleData.FixedAssetSaleDetailTable.Rows.Count == 0)
			{
				return true;
			}
			bool result = false;
			bool.TryParse(fixedAssetSaleData.FixedAssetSaleDetailTable.Rows[0]["IsVoid"].ToString(), out result);
			if (!result)
			{
				foreach (DataRow row in fixedAssetSaleData.FixedAssetSaleDetailTable.Rows)
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

		public bool VoidFixedAssetSale(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidFixedAssetSale(sysDocID, voucherID, isVoid, null);
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

		private bool VoidFixedAssetSale(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				FixedAssetSaleData fixedAssetSaleData = new FixedAssetSaleData();
				string textCommand = "SELECT PID.*,ISVOID FROM FixedAsset_Sale_Detail PID INNER JOIN FixedAsset_Sale PI ON PI.SysDocID=PID.SysDocID AND PI.VOucherID=PID.VoucherID\r\n                              WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
				FillDataSet(fixedAssetSaleData, "FixedAsset_Sale_Detail", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM FixedAsset_Sale WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(fixedAssetSaleData, "FixedAsset_Sale", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(fixedAssetSaleData.FixedAssetSaleDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (result)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				ReduceAquesitionCost(sysDocID, voucherID, sqlTransaction);
				textCommand = "UPDATE FixedAsset_Sale SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Fixed Asset Sale", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteFixedAssetSale(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("FixedAsset_Sale", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidFixedAssetSale(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteFixedAssetSaleDetailsRows(sysDocID, voucherID, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM FixedAsset_Sale WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Fixed Asset Sale", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetFixedAssetSaleToPrint(string sysDocID, string voucherID)
		{
			return GetFixedAssetSaleToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetFixedAssetSaleToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT   ISNULL(IsVoid,'False') AS V,  SysDocID [Doc ID],VoucherID [Doc Number],TD.PayeeID [Vendor Code],\r\n                                 (CASE PayeeType\r\n                                    WHEN 'C' THEN Customer.CustomerName\r\n                                    WHEN 'V' THEN Vendor.VendorName\r\n                                    WHEN 'E' THEN Employee.FirstName\r\n                                    ELSE Account.AccountName END) AS [Vendor Name],TransactionDate [Invoice Date]\r\n                            FROM FixedAsset_Sale TD\r\n                            LEFt OUTER JOIN \r\n                        Account ON TD.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON TD.PayeeID=Employee.AccountID\r\n                         WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "FixedAsset_Sale", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["FixedAsset_Sale"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT  FSD.SysDocID,FSD.VoucherID,FSD.AssetID,FSD.Description,FA.AssetName,FSD.Amount,FSD.Amount FROM  \r\n                        FixedAsset_Sale_Detail FSD LEFT OUTER JOIN FixedAsset FA ON FSD.AssetID=FA.AssetID \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "FixedAsset_Sale_Detail", cmdText);
				dataSet.Relations.Add("FixedAssetSalesDetail", dataSet.Tables["FixedAsset_Sale_Detail"].Columns["AssetID"], dataSet.Tables["FixedAsset"].Columns["AssetID"], createConstraints: false);
				dataSet.Tables["FixedAsset_Sale"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["FixedAsset_Sale"].Rows)
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
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS V,  SysDocID [Doc ID],VoucherID [Doc Number],INV.PayeeID [Vendor Code],\r\n                                 TransactionDate [Invoice Date]\r\n                            FROM FixedAsset_Sale INV \r\n                             LEFt OUTER JOIN\r\n                        Customer ON INV.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON INV.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON INV.PayeeID=Employee.EmployeeID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "FixedAsset_Sale", sqlCommand);
			return dataSet;
		}
	}
}
