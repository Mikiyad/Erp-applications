using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class GarmentRental : StoreObject
	{
		private const string GARMENTRENTAL_TABLE = "Garment_Rental";

		private const string GARMENTRENTALDETAIL_TABLE = "Garment_Rental_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PACKAGEID_PARM = "@PackageID";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string DISCOUNT_PARM = "@Discount";

		private const string CHARGES_PARM = "@Charges";

		private const string TOTAL_PARM = "@Total";

		private const string EXPRETURNDATE_PARM = "@ExpReturnDate";

		private const string OUTDATE_PARM = "@OutDate";

		private const string CASHAMOUNT_PARM = "@CashAmount";

		private const string CARDAMOUNT_PARM = "@CardAmount";

		private const string AMOUNTPAID_PARM = "@AmountPaid";

		private const string BALANCE_PARM = "@Balance";

		private const string CASHACCOUNTID_PARM = "@CashAccountID";

		private const string CARDACCOUNTID_PARM = "@CardAccountID";

		private const string RECEIPTVOUCHERID_PARM = "@ReceiptVoucherID";

		private const string RECEIPTVOUCHERAMOUNT_PARM = "@ReceiptVoucherAmount";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string CONSIGNLOCATIONID_PARM = "@GarmentLocationID";

		private const string AMOUNT_PARM = "@Amount";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public GarmentRental(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateGarmentRentalText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Garment_Rental", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("Charges", "@Charges"), new FieldValue("CashAmount", "@CashAmount"), new FieldValue("CardAmount", "@CardAmount"), new FieldValue("AmountPaid", "@AmountPaid"), new FieldValue("Balance", "@Balance"), new FieldValue("CashAccountID", "@CashAccountID"), new FieldValue("CardAccountID", "@CardAccountID"), new FieldValue("ReceiptVoucherID", "@ReceiptVoucherID"), new FieldValue("ReceiptVoucherAmount", "@ReceiptVoucherAmount"), new FieldValue("ExpReturnDate", "@ExpReturnDate"), new FieldValue("OutDate", "@OutDate"), new FieldValue("Total", "@Total"), new FieldValue("PackageID", "@PackageID"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("TaxOption", "@TaxOption"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Garment_Rental", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateGarmentRentalCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateGarmentRentalText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateGarmentRentalText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PackageID", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@Charges", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@ExpReturnDate", SqlDbType.DateTime);
			parameters.Add("@OutDate", SqlDbType.DateTime);
			parameters.Add("@CashAmount", SqlDbType.Decimal);
			parameters.Add("@CardAmount", SqlDbType.Decimal);
			parameters.Add("@AmountPaid", SqlDbType.Decimal);
			parameters.Add("@Balance", SqlDbType.Decimal);
			parameters.Add("@CashAccountID", SqlDbType.NVarChar);
			parameters.Add("@CardAccountID", SqlDbType.NVarChar);
			parameters.Add("@ReceiptVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ReceiptVoucherAmount", SqlDbType.Decimal);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PackageID"].SourceColumn = "PackageID";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Charges"].SourceColumn = "Charges";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@CashAmount"].SourceColumn = "CashAmount";
			parameters["@CardAmount"].SourceColumn = "CardAmount";
			parameters["@AmountPaid"].SourceColumn = "AmountPaid";
			parameters["@Balance"].SourceColumn = "Balance";
			parameters["@CashAccountID"].SourceColumn = "CashAccountID";
			parameters["@CardAccountID"].SourceColumn = "CardAccountID";
			parameters["@ReceiptVoucherID"].SourceColumn = "ReceiptVoucherID";
			parameters["@ReceiptVoucherAmount"].SourceColumn = "ReceiptVoucherAmount";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@ExpReturnDate"].SourceColumn = "ExpReturnDate";
			parameters["@OutDate"].SourceColumn = "OutDate";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
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

		private string GetInsertUpdateGarmentRentalDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Garment_Rental_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ConsignLocationID", "@GarmentLocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("Discount", "@Discount"), new FieldValue("Amount", "@Amount"), new FieldValue("PackageID", "@PackageID"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateGarmentRentalDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateGarmentRentalDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateGarmentRentalDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@GarmentLocationID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@PackageID", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@GarmentLocationID"].SourceColumn = "ConsignLocationID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PackageID"].SourceColumn = "PackageID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(GarmentRentalData journalData)
		{
			return true;
		}

		public bool InsertUpdateGarmentRental(GarmentRentalData garmentRentalData, bool isUpdate)
		{
			object obj = null;
			bool flag = true;
			SqlCommand insertUpdateGarmentRentalCommand = GetInsertUpdateGarmentRentalCommand(isUpdate);
			try
			{
				DataRow dataRow = garmentRentalData.GarmentRentalTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string value = "";
				obj = new Databases(base.DBConfig).GetFieldValue("System_Document", "ConsignOutLocationID", "SysDocID", text2, sqlTransaction);
				if (obj != null && obj.ToString() != "")
				{
					value = obj.ToString();
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new CompanyException("Garmentment out location is not assigned to the document.");
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Garment_Rental", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text3 = "";
				if (false)
				{
					obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString());
					if (obj != null)
					{
						text3 = obj.ToString();
					}
				}
				else
				{
					obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString());
					if (obj != null)
					{
						text3 = obj.ToString();
					}
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text3 != "")
				{
					salesFlows = (SalesFlows)int.Parse(text3.ToString());
				}
				bool flag2 = false;
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					flag2 = true;
				}
				foreach (DataRow row in garmentRentalData.GarmentRentalDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string idFieldValue = row["ProductID"].ToString();
					row["ConsignLocationID"] = value;
					new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", idFieldValue, sqlTransaction)?.ToString();
				}
				insertUpdateGarmentRentalCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(garmentRentalData, "Garment_Rental", insertUpdateGarmentRentalCommand)) : (flag & Insert(garmentRentalData, "Garment_Rental", insertUpdateGarmentRentalCommand)));
				insertUpdateGarmentRentalCommand = GetInsertUpdateGarmentRentalDetailsCommand(isUpdate: false);
				insertUpdateGarmentRentalCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteGarmentRentalDetailsRows(text2, text, isDeletingTransaction: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(112, text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				if (garmentRentalData.Tables["Garment_Rental_Detail"].Rows.Count > 0)
				{
					flag &= Insert(garmentRentalData, "Garment_Rental_Detail", insertUpdateGarmentRentalCommand);
				}
				InventoryTransactionData inventoryTransactionData = null;
				if (!flag2)
				{
					inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row2 in garmentRentalData.GarmentRentalDetailTable.Rows)
					{
						DataRow dataRow3 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow3.BeginEdit();
						dataRow3["SysDocID"] = row2["SysDocID"];
						dataRow3["VoucherID"] = row2["VoucherID"];
						dataRow3["LocationID"] = value;
						dataRow3["ProductID"] = row2["ProductID"];
						dataRow3["UnitID"] = row2["UnitID"];
						dataRow3["Quantity"] = -1f * float.Parse(row2["Quantity"].ToString());
						if (row2["UnitQuantity"] != DBNull.Value)
						{
							dataRow3["UnitQuantity"] = -1f * float.Parse(row2["UnitQuantity"].ToString());
						}
						else
						{
							dataRow3["UnitQuantity"] = DBNull.Value;
						}
						dataRow3["UnitPrice"] = 0;
						dataRow3["Factor"] = row2["UnitFactor"];
						dataRow3["FactorType"] = row2["FactorType"];
						dataRow3["Reference"] = dataRow["Reference"];
						dataRow3["RowIndex"] = row2["RowIndex"];
						dataRow3["SysDocType"] = (byte)112;
						dataRow3["TransactionDate"] = dataRow["TransactionDate"];
						dataRow3["TransactionType"] = (byte)22;
						dataRow3.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow3);
						dataRow3 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow3.BeginEdit();
						dataRow3["SysDocID"] = row2["SysDocID"];
						dataRow3["VoucherID"] = row2["VoucherID"];
						dataRow3["LocationID"] = value;
						dataRow3["ProductID"] = row2["ProductID"];
						dataRow3["UnitID"] = row2["UnitID"];
						dataRow3["Quantity"] = float.Parse(row2["Quantity"].ToString());
						if (row2["UnitQuantity"] != DBNull.Value)
						{
							dataRow3["UnitQuantity"] = float.Parse(row2["UnitQuantity"].ToString());
						}
						else
						{
							dataRow3["UnitQuantity"] = DBNull.Value;
						}
						dataRow3["UnitPrice"] = 0;
						dataRow3["Factor"] = row2["UnitFactor"];
						dataRow3["FactorType"] = row2["FactorType"];
						dataRow3["Reference"] = dataRow["Reference"];
						dataRow3["RowIndex"] = row2["RowIndex"];
						dataRow3["SysDocType"] = (byte)112;
						dataRow3["TransactionDate"] = dataRow["TransactionDate"];
						dataRow3["TransactionType"] = (byte)22;
						dataRow3.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow3);
					}
					inventoryTransactionData.Merge(garmentRentalData.Tables["Product_Lot_Issue_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(garmentRentalData, isUpdate: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				}
				else
				{
					inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row3 in garmentRentalData.GarmentRentalDetailTable.Rows)
					{
						DataRow dataRow5 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["SysDocID"] = row3["SysDocID"];
						dataRow5["VoucherID"] = row3["VoucherID"];
						dataRow5["Description"] = row3["Description"];
						dataRow5["LocationID"] = value;
						dataRow5["ProductID"] = row3["ProductID"];
						dataRow5["UnitID"] = row3["UnitID"];
						dataRow5["Quantity"] = row3["Quantity"];
						dataRow5["UnitQuantity"] = row3["UnitQuantity"];
						dataRow5["Factor"] = row3["UnitFactor"];
						dataRow5["FactorType"] = row3["FactorType"];
						dataRow5["Reference"] = dataRow["Reference"];
						dataRow5["RowIndex"] = row3["RowIndex"];
						dataRow5["Discount"] = row3["Discount"];
						dataRow5["SysDocType"] = (byte)112;
						dataRow5["TransactionDate"] = dataRow["TransactionDate"];
						dataRow5["TransactionType"] = (byte)22;
						dataRow5.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
					}
				}
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				if (garmentRentalData.GarmentRentalTable.Rows.Count > 0)
				{
					DataRow dataRow6 = garmentRentalData.GarmentRentalTable.Rows[0];
					string registerID = dataRow6["RegisterID"].ToString();
					string text4 = "";
					string text5 = "";
					text4 = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CardReceivedAccountID");
					text5 = (string)(dataRow6["CashAccountID"] = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID"));
					dataRow6["CardAccountID"] = text4;
					dataRow6.EndEdit();
				}
				if (garmentRentalData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(garmentRentalData, text2, text, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateGarmentRentalGLData(garmentRentalData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Garment_Rental", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Garment Rental";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Garment_Rental", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.GarmentRental, text2, text, "Garment_Rental", sqlTransaction);
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

		private GLData CreateGarmentRentalGLData(GarmentRentalData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.GarmentRentalTable.Rows[0];
			string text = dataRow["CustomerID"].ToString();
			string idFieldValue = dataRow["SysDocID"].ToString();
			string idFieldValue2 = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", idFieldValue, sqlTransaction).ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
			new Databases(base.DBConfig).GetFieldValue("Location", "DiscountGivenAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
			new Databases(base.DBConfig).GetFieldValue("Location", "SalesTaxAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
			string text3 = new Databases(base.DBConfig).GetFieldValue("Location", "ARAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
			string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
			if (dataRow["CurrencyID"] != DBNull.Value)
			{
				_ = (baseCurrencyID != dataRow["CurrencyID"].ToString());
			}
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.GarmentRental;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["CurrencyID"] = dataRow["CurrencyID"];
			dataRow2["CurrencyRate"] = 0;
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Garment Rental - ";
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal num = default(decimal);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal num2 = default(decimal);
			foreach (DataRow row in transactionData.GarmentRentalDetailTable.Rows)
			{
				idFieldValue2 = row["LocationID"].ToString();
				string text4 = row["ProductID"].ToString();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "ItemType", "ProductID", text4, sqlTransaction);
				if (fieldValue == null || !(fieldValue.ToString() != ""))
				{
					throw new CompanyException("Item type is not selected for the product:" + text4);
				}
				byte.Parse(fieldValue.ToString());
				if (row["UnitQuantity"] != DBNull.Value)
				{
					decimal.Parse(row["UnitQuantity"].ToString());
				}
				else
				{
					decimal.Parse(row["Quantity"].ToString());
				}
				new Products(base.DBConfig).GetProductCurrentCost(text4, sqlTransaction);
				decimal result3 = default(decimal);
				decimal.TryParse(row["UnitPrice"].ToString(), out result3);
			}
			decimal.TryParse(dataRow["AmountPaid"].ToString(), out result);
			decimal.TryParse(dataRow["ReceiptVoucherAmount"].ToString(), out result2);
			num2 = result - result2;
			DataRow dataRow4;
			if (result != 0m)
			{
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					idFieldValue2 = arrayList[i].ToString();
					num = decimal.Parse(hashtable[idFieldValue2].ToString());
					text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text2;
					dataRow4["PayeeID"] = text;
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = num;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			idFieldValue2 = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", idFieldValue, sqlTransaction).ToString();
			dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			string text5 = new Databases(base.DBConfig).GetFieldValue("Customer", "ARAccountID", "CustomerID", text, sqlTransaction).ToString();
			if (text5 == "" || text5 == string.Empty || text5 == null)
			{
				text5 = text3;
			}
			dataRow4["JournalID"] = 0;
			dataRow4["AccountID"] = text5;
			dataRow4["PayeeID"] = text;
			dataRow4["Debit"] = DBNull.Value;
			dataRow4["Credit"] = num2;
			dataRow4["PayeeType"] = "C";
			dataRow4["IsARAP"] = true;
			dataRow4["Reference"] = dataRow["Reference"];
			if (dataRow["Note"].ToString() != string.Empty)
			{
				dataRow4["Description"] = dataRow["Note"];
			}
			else
			{
				dataRow4["Description"] = "Advance Payment ";
			}
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			decimal result4 = default(decimal);
			decimal result5 = default(decimal);
			decimal result6 = default(decimal);
			if (dataRow["Discount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["Discount"].ToString(), out result4);
			}
			if (dataRow["Charges"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["Charges"].ToString(), out result6);
			}
			if (dataRow["TaxAmount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out result5);
			}
			decimal result7 = default(decimal);
			decimal result8 = default(decimal);
			decimal.TryParse(dataRow["CashAmount"].ToString(), out result8);
			decimal.TryParse(dataRow["CardAmount"].ToString(), out result7);
			if (result8 > 0m)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["CashAccountID"].ToString();
				dataRow4["PayeeID"] = text;
				dataRow4["PayeeType"] = "A";
				dataRow4["IsARAP"] = false;
				dataRow4["Debit"] = dataRow["CashAmount"];
				dataRow4["Credit"] = DBNull.Value;
				if (dataRow["Note"].ToString() != string.Empty)
				{
					dataRow4["Description"] = dataRow["Note"];
				}
				else
				{
					dataRow4["Description"] = "Advance Cash Payment ";
				}
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			if (result7 > 0m)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["CardAccountID"].ToString();
				dataRow4["PayeeID"] = text;
				dataRow4["PayeeType"] = "A";
				dataRow4["IsARAP"] = false;
				dataRow4["Debit"] = dataRow["CardAmount"];
				dataRow4["Credit"] = DBNull.Value;
				if (dataRow["Note"].ToString() != string.Empty)
				{
					dataRow4["Description"] = dataRow["Note"];
				}
				else
				{
					dataRow4["Description"] = "Advance Card Payment ";
				}
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		public GarmentRentalData GetGarmentRentalByID(string sysDocID, string voucherID)
		{
			try
			{
				GarmentRentalData garmentRentalData = new GarmentRentalData();
				string textCommand = "SELECT * FROM Garment_Rental WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(garmentRentalData, "Garment_Rental", textCommand);
				if (garmentRentalData == null || garmentRentalData.Tables.Count == 0 || garmentRentalData.Tables["Garment_Rental"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,TD.Quantity-ISNULL(QuantitySettled,0) - ISNULL(QuantityReturned,0) AS QuantityBalance,Product.Description,Product.ItemType,BD.ProductID AS CataegoryCode,BD.Quantity as PackgQty,\r\n                        CASE WHEN ItemType = 5 THEN 'True' ELSE IsTrackLot END  AS IsTrackLot,IsTrackSerial\r\n                        FROM Garment_Rental_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        INNER JOIN Garment_Rental T ON TD.VoucherID=T.VoucherID AND  TD.SysDocID=T.SysDocID\r\n                        LEFT JOIN BOM B ON B.BOMID=T.PackageID\r\n                        LEFT JOIN BOM_Detail BD ON B.BOMID=BD.BOMID AND BD.ProductID=Product.CategoryID\r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(garmentRentalData, "Garment_Rental_Detail", textCommand);
				textCommand = "SELECT * from General_Payment_Detail where SourceVoucherID='" + voucherID + "' AND SourceSysDocID='" + sysDocID + "'";
				FillDataSet(garmentRentalData, "General_Payment_Detail", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (garmentRentalData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					garmentRentalData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				garmentRentalData.Merge(transactionIssuesProductLots, preserveChanges: false);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(garmentRentalData, "Tax_Detail", textCommand);
				return garmentRentalData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGarmentRentalList(string sysDocID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT Coo.SysDocID [Doc ID], Coo.VoucherID [Number],Cos.VoucherID [SettleNo], Coo.TransactionDate AS [Date],Cus.CustomerName AS [Vendor]\r\n                             FROM Garment_Rental Coo INNER JOIN Customer CUS ON Coo.CustomerID = Cus.CustomerID\r\n\t\t\t\t\t\t\t INNER JOIN GarmentRental_Settlement Cos ON Coo.VoucherID=Cos.GarmentVoucherID\r\n                             WHERE ISNULL(Coo.IsVoid,'False')='False'  AND Coo.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND Coo.SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY Coo.TransactionDate, Coo.VoucherID ";
			FillDataSet(dataSet, "Garment_Rental", str);
			return dataSet;
		}

		internal bool DeleteGarmentRentalDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				GarmentRentalData garmentRentalData = new GarmentRentalData();
				string textCommand = "SELECT SOD.*,ISVOID FROM Garment_Rental_Detail SOD INNER JOIN Garment_Rental SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(garmentRentalData, "Garment_Rental_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(garmentRentalData.GarmentRentalDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(112, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				}
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Garment_Rental_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidGarmentRental(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidGarmentRental(sysDocID, voucherID, isVoid, null);
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

		public bool VoidGarmentRental(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				bool flag2 = true;
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				else
				{
					flag2 = false;
				}
				string exp = "UPDATE Garment_Rental SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				GarmentRentalData dataSet = new GarmentRentalData();
				exp = "SELECT * FROM Garment_Rental_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Garment_Rental_Detail", exp, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(112, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				if (!flag || !flag2)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Garment Rental", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteGarmentRental(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Garment_Rental", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidGarmentRental(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteGarmentRentalDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Garment_Rental WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Garment Rental", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetUninvoicedGarmentRentals(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer] FROM Garment_Rental DN\r\n                             INNER JOIN Customer C ON DN.CustomerID=C.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False'";
				if (customerID != "")
				{
					text = text + " AND DN.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Garment_Rental", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGarmentRentalToPrint(string sysDocID, string voucherID)
		{
			return GetGarmentRentalToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetGarmentRentalToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,TransactionDate,ExpReturnDate,Customer.TaxIDNumber as CTaxIDNo, ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,\r\n                                SI.SalesPersonID,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,Reference,Discount AS Discount,SI.PackageID,B.BOMName as PackageName,\r\n                                Total AS Total,SI.Note,SI.Discount,SI.Charges,SI.CashAmount,SI.CardAmount,SI.AmountPaid,SI.Balance,SI.ReceiptVoucherID,SI.ReceiptVoucherAmount,\r\n                                (Total-SI.Charges)+SI.Discount AS SubTotal\r\n                                FROM  Garment_Rental SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT JOIN BOM B ON B.BOMID=SI.PackageID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Garment_Rental", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Garment_Rental"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,ProductID,Description,LocationID,ISNULL(UnitQuantity,Quantity) AS Quantity,\r\n                        UnitPrice AS UnitPrice,\r\n                        ISNULL(UnitQuantity,Quantity)*UnitPrice AS Total,UnitID,Discount AS LineDiscount,Amount, GD.TaxAmount, GD.TaxGroupID,TG.TaxGroupName\r\n                        FROM   Garment_Rental_Detail GD\r\n                        LEFT JOIN Tax_Group TG ON GD.TaxGroupID=TG.TaxGroupID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Garment_Rental_Detail", cmdText);
				dataSet.Relations.Add("CustomerGarmentRental", new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental_Detail"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Garment_Rental"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Garment_Rental"].Rows)
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
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Rental Date],\r\n                            INV.SalespersonID [Salesperson],SUM(DND.Quantity) AS Quantity\r\n                            FROM         Garment_Rental INV\r\n                            INNER JOIN Garment_Rental_Detail DND ON DND.SysDocID=INV.SysDocID AND DND.VoucherID=INV.VoucherID\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " GROUP BY IsVoid,INV.SysDocID,INV.VoucherID,INV.CustomerID ,CustomerName,TransactionDate,INV.SalespersonID";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Garment_Rental", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenGarmentRentals(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT CO.SysDocID [Doc ID], CO.VoucherID [Number],CO.CustomerID [Customer Code], CUS.CustomerName [Customer Name],TransactionDate [Date],CO.PackageID [PackageID],CO.RegisterID [RegisterID]\r\n                            FROM Garment_Rental CO INNER JOIN Garment_Rental_Detail COD\r\n                            ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            INNER JOIN Customer CUS ON CO.CustomerID=CUS.CustomerID  WHERE ISNULL(IsClosed,'False')='False' ";
				if (customerID != "")
				{
					str = str + " AND CO.CustomerID='" + customerID + "'";
				}
				str += " GROUP BY CO.SysDocID, CO.VoucherID,CO.CustomerID,TransactionDate,CO.PackageID,CO.RegisterID, CUS.CustomerName\r\n                                HAVING\r\n                                SUM(ISNULL(COD.Quantity,0)-ISNULL(COD.QuantitySettled,0))>0 ";
				SqlCommand sqlCommand = new SqlCommand(str);
				FillDataSet(dataSet, "Garment_Rental", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool CloseOpenGarmentment(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END))- SUM(ISNULL(QuantitySettled,0) ) - SUM(ISNULL(QuantityReturned,0) )\r\n\t                             FROM Garment_Rental_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || obj.ToString() == "")
				{
					return true;
				}
				exp = ((!(decimal.Parse(obj.ToString()) <= 0m)) ? ("UPDATE Garment_Rental SET IsClosed = 'False' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'") : ("UPDATE Garment_Rental SET IsClosed = 'True' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'"));
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateGarmentmentStatus(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "Update CO SET Status = CASE  WHEN BalanceQty <= 0 THEN  2 WHEN BalanceQty>0 THEN  1 END\r\n                                FROM Garment_Rental CO INNER JOIN\r\n                                (select SysDocID,VoucherID, SUM(ISNULL(Quantity,0)-ISNULL(QuantityReturned,0)- ISNULL(QuantitySettled,0)) AS BalanceQty\r\n                                FROM Garment_Rental_Detail COD WHERE COD.SysDocID = '" + sysDocID + "' AND COD.VoucherID = '" + voucherID + "'\r\n                                GROUP BY SysDocID,VOucherID) AS COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                                WHERE CO.SysDocID = '" + sysDocID + "' AND CO.VoucherID = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool AllowModify(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Garment_Rental_Return POD\r\n                                WHERE SourceSysDocID='" + sysDocID + "' AND SourceVoucherID='" + voucherNumber + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public DataSet GetGarmentmentRentalIssuedReport(DateTime settleto, DateTime from, DateTime to, string fromCustomer, string toCustomer, int intstatus)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT T.SysDocID,T.VoucherID,T.[Set ID],T.[Set No.],T.CustomerID,T.CustomerName,T.[Set Date],T.[Con Date],CASE WHEN T.Status=2 THEN 'Settled' ELSE 'UnSettlted' END AS Status,\r\n                                SUM(COD.Quantity) AS [Con Qty],ISNULL(T.[Set Qty],0)AS [Set Qty],ISNULL(T.Amount,0) AS Amount,ISNULL(T.Cost,0) AS Cost,\r\n                                (ISNULL(T.Amount,0)-ISNULL(T.Cost,0)) AS GrossProfit,\r\n                                ISNULL(T.[Total Expense],0) AS Expense,\r\n                                (ISNULL(T.Amount,0)-ISNULL(T.Cost,0)-ISNULL(T.[Total Expense],0)) AS NetProfit,T.Category\r\n                                FROM Garment_Rental_Detail COD INNER JOIN \r\n                                (SELECT DISTINCT COD.SysDocID,COD.VoucherID,CO.TransactionDate [Con Date],C.CustomerID,C.CustomerName,CO.Status,\r\n                                CSD.SysDocID AS [Set ID],CSD.VoucherID AS [Set No.],CS.TransactionDate AS [Set Date],SUM(CSD.Quantity) AS [Set Qty],\r\n                                (SELECT SUM(CE.Amount) FROM GarmentRental_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID) AS [Total Expense],\r\n                                (SELECT -1*SUM(IT.Assetvalue) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Cost],\r\n                                (SELECT -1*SUM(IT.UnitPrice*IT.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Amount],\r\n                                (SELECT SUBSTRING(\r\n                                (SELECT  ',' + CategoryName FROM  Product_Category WHERE CategoryID IN (SELECT DISTINCT CategoryID FROM Product P LEFT JOIN GarmentRental_Settlement_Detail COS1 ON P.ProductID=COS1.ProductID WHERE COS1.SysDocID=CSD.SysDocID AND COS1.VoucherID=CSD.VoucherID )   FOR XML PATH('')),2,20000)) AS  Category\r\n                                FROM  Garment_Rental CO LEFT OUTER JOIN  Garment_Rental_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                                LEFT OUTER JOIN GarmentRental_Settlement_Detail CSD ON CSD.GarmentSysDocID = COD.SysDocID AND CSD.GarmentVoucherID = COD.VoucherID AND CSD.GarmentRowIndex = COD.RowIndex\r\n                                LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n                                LEFT OUTER JOIN GarmentRental_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                                LEFT OUTER JOIN Customer C ON C.CustomerID=CO.CustomerID ";
				text3 = text3 + " WHERE CS.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND CS.IsVoid IS NULL ";
				if (fromCustomer != "" || toCustomer != "")
				{
					text3 = text3 + " AND CO.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text3 += "GROUP BY COD.SysDocID,COD.VoucherID,CO.TransactionDate,C.CustomerID,C.CustomerName,CO.Status,CSD.SysDocID,CSD.VoucherID,CS.TransactionDate,CS.SysDocID,CS.VoucherID) T ON COD.SysDocID=T.SysDocID AND COD.VoucherID=T.VoucherID   WHERE T.CustomerID IS NOT NULL AND T.Status  IN (1,2)  ";
				text3 += " GROUP BY T.SysDocID,T.VoucherID,T.CustomerID,T.CustomerName,T.Category,T.Status,T.[Set Qty],T.Amount,T.Cost,T.[Total Expense],T.[Set ID],T.[Set No.],T.[Set Date],T.[Con Date] ORDER BY T.VoucherID,T.[Set No.]";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Garment_Rental", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGarmentmentRentalSettlementReport(DateTime to, string fromCustomer, string toCustomer)
		{
			try
			{
				string text = "SELECT CO.SysDocID [Doc ID], CO.VoucherID [Number],CO.CustomerID [Customer Code], CUS.CustomerName [Customer Name],CO.TransactionDate [Date],DATEDIFF(DAY,CO.TransactionDate,GETDATE()) AGE,\r\n                            (SELECT SUBSTRING(\r\n                             (SELECT  ',' + CategoryName FROM  Product_Category WHERE CategoryID IN (SELECT DISTINCT CategoryID FROM Product P LEFT JOIN Inventory_Transactions IT1 ON P.ProductID=IT1.ProductID WHERE IT1.SysDocID=CO.SysDocID AND IT1.VoucherID=CO.VoucherID )   FOR XML PATH('')),2,20000)) AS  Category,\r\n                            (SELECT V.VehicleName FROM Delivery_Note DN LEFT JOIN Vehicle V ON DN.VehicleID=V.VehicleID WHERE DN.SysDocID=CO.SourceSysDocID AND DN.VoucherID=CO.SourceVoucherID) AS Truck,\r\n                            COD.ProductID,COD.Description,COD.UnitID,COD.Quantity,COD.UnitPrice,(IT.AssetValue/IT.Quantity) AS COST\r\n                            FROM Garment_Rental CO INNER JOIN Garment_Rental_Detail COD\r\n                            ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            LEFT JOIN Inventory_Transactions IT ON COD.SourceSysDocID=IT.SysDocID AND COD.SourceVoucherID=It.VoucherID AND COD.SourceRowIndex=IT.RowIndex\r\n                            INNER JOIN Customer CUS ON CO.CustomerID=CUS.CustomerID  WHERE ISNULL(IsClosed,'False')='False'  \r\n                            GROUP BY CO.SysDocID, CO.VoucherID,CO.CustomerID,CO.TransactionDate, IT.AssetValue,IT.Quantity,CUS.CustomerName,COD.ProductID,COD.Description,COD.UnitID,COD.Quantity,COD.UnitPrice,CO.SourceSysDocID,CO.SourceVoucherID\r\n                            HAVING\r\n                            SUM(ISNULL(COD.Quantity,0)-ISNULL(COD.QuantitySettled,0))>0 ";
				if (fromCustomer != "" || toCustomer != "")
				{
					text = text + " AND CO.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text = text + "AND CO.TransactionDate <= '" + to + "'";
				text += " ORDER BY CO.TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Garment_Rental", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPendingGarmentmentsReport(string fromCustomer, string toCustomer)
		{
			try
			{
				if (fromCustomer != "" || toCustomer != "")
				{
					_ = " AND CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				string text = "SELECT DISTINCT CI.SysDocID,CI.VoucherID,CI.CustomerID,CustomerName,TransactionDate\r\n                                    FROM Garment_Rental CI INNER JOIN Garment_Rental_Detail CID\r\n                                    ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                                    INNER JOIN Customer V ON V.CustomerID = CI.CustomerID\r\n                                    WHERE ISNULL(IsVoid,'False') = 'False' \r\n\t\t\t\t\t\t\t\t\tGROUP BY CI.SysDocID,CI.VoucherID,CI.CustomerID,CustomerName,TransactionDate\r\n\t\t\t\t\t\t\t\t\tHAVING SUM(Quantity - ISNULL(QuantityReturned,0) - ISNULL(CID.QuantitySettled,0)) > 0 ";
				if (fromCustomer != "")
				{
					text = text + " AND CI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Garment_Rental", text);
				DataSet dataSet2 = new DataSet();
				text = "SELECT CiD.SysDocID,CID.VoucherID,CID.Description,ProductID,ISNULL(Quantity,0) AS Quantity,ISNULL(QuantitySettled,0) AS QuantitySettled,ISNULL(QuantityReturned,0) AS QuantityReturned,\r\n                            Quantity-ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0) AS BalanceQty\r\n                            FROM Garment_Rental CI INNER JOIN Garment_Rental_Detail CID\r\n                            ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                            WHERE ISNULL(IsClosed,'False') = 'False' ";
				if (fromCustomer != "")
				{
					text = text + " AND CI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text += " ORDER BY TransactionDate";
				FillDataSet(dataSet2, "Garment_Rental_Detail", text);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Garment_REL", new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental_Detail"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGarmentRentalSummaryReport_Old(string sysDocID, string voucherID, DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromGroup, string toGroup)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = " SELECT DISTINCT COD.SysDocID+'-'+COD.VoucherID AS [Con No],C.CustomerID+'-'+C.CustomerName AS [Customer],\r\n                                DATEdIFF(day,CO.TransactionDate,CS.TransactionDate) AS Days,\r\n                                CSD.SysDocID+'-'+CSD.VoucherID AS [Set Id],CO.TransactionDate [Con Date],CS.TransactionDate AS [Set Date],\r\n                                (SELECT SUM(CE.Amount) FROM GarmentRental_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID) AS [Total Expense],\r\n                                (SELECT -1*SUM(IT.Assetvalue) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Cost],\r\n                                (SELECT -1*SUM(IT.UnitPrice*IT.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Amount]\r\n                                FROM  Garment_Rental CO LEFT OUTER JOIN  Garment_Rental_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                                LEFT OUTER JOIN GarmentRental_Settlement_Detail CSD ON CSD.GarmentSysDocID = COD.SysDocID AND CSD.GarmentVoucherID = COD.VoucherID AND CSD.GarmentRowIndex = COD.RowIndex\r\n                                LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n                                LEFT JOIN GarmentRental_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                                LEFT JOIN Customer C ON C.CustomerID=CO.CustomerID\r\n                                WHERE COD.SysDocID= '" + sysDocID + "' AND COD.VoucherID= '" + voucherID + "'";
				if (fromCustomer != "")
				{
					text3 = text3 + " AND C.CustomerID >= '" + fromCustomer + "' ";
				}
				if (toCustomer != "")
				{
					text3 = text3 + " AND C.CustomerID <= '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					text3 = text3 + " AND C.CustomerClassID IN (SELECT ClassID FROM Customer_Class WHERE ClassID >= '" + fromCustomerClass + "')";
				}
				if (toCustomerClass != "")
				{
					text3 = text3 + " AND C.CustomerClassID IN (SELECT ClassID FROM Customer_Class WHERE ClassID <= '" + toCustomerClass + "')";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND C.CustomerGroupID IN (SELECT GroupID FROM Customer_Group WHERE GroupID >= '" + fromGroup + "')";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND C.CustomerGroupID IN (SELECT GroupID FROM Customer_Group WHERE GroupID <= '" + toGroup + "') ";
				}
				text3 = text3 + "AND CO. TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				FillDataSet(dataSet, "Garment_Rental", text3);
				text3 = "SELECT '" + sysDocID + "' AS DocID, '" + voucherID + " AS ReceiptNumber' , TEMP.ProductID,TEMP.Description, TEMP.UnitID,\r\n                         TEMP.Quantity,TEMP.[QTY Settled],TEMP.[QTY Returned],Temp.Cost,TEMP.[Total Cost],TEMP.Price,Temp.Amount,\r\n                        SUM(Temp.Expense) AS [Item Expense]\r\n                        FROM \r\n                        (SELECT DISTINCT COD.ProductID,COD.Description, COD.UnitID,\r\n                        ISNULL(COD.UnitQuantity,COD.Quantity) AS Quantity, (COD.QuantitySettled) [QTY Settled],(COD.QuantityReturned) [QTY Returned],\r\n                        (SELECT (SUM(IT.Assetvalue)/SUM(IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=COD.ProductID) AS [Cost],\r\n                        (SELECT -1*SUM(IT.Assetvalue) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.RowIndex=CSD.RowIndex AND ABS(IT.Quantity)=CSD.Quantity) AS [Total Cost],\r\n                        (SELECT (SUM(IT.UnitPrice*IT.Quantity)/SUM(IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=COD.ProductID) AS [Price],\r\n                        (SELECT -1*(SUM(IT.UnitPrice*IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=COD.ProductID AND IT.RowIndex = CSD.RowIndex) AS [Amount],\r\n                        (SELECT (CSD.Amount*(SELECT SUM(CE.Amount) FROM GarmentRental_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID)/(SELECT -1*(SUM(IT.UnitPrice*IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID))) AS Expense\r\n                        FROM  Garment_Rental CO LEFT OUTER JOIN  Garment_Rental_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                        LEFT OUTER JOIN GarmentRental_Settlement_Detail CSD ON CSD.GarmentSysDocID = COD.SysDocID AND CSD.GarmentVoucherID = COD.VoucherID AND CSD.GarmentRowIndex = COD.RowIndex\r\n                        LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n                        LEFT JOIN GarmentRental_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                        LEFT JOIN Customer C ON C.CustomerID=CO.CustomerID\r\n                        WHERE COD.SysDocID= '" + sysDocID + "' AND COD.VoucherID= '" + voucherID + "' ";
				text3 = text3 + "AND CO. TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ) AS TEMP WHERE 1=1";
				text3 += " GROUP BY TEMP.ProductID,TEMP.Description, TEMP.UnitID,TEMP.Quantity,TEMP.[QTY Settled],TEMP.[QTY Returned],Temp.Cost,TEMP.[Total Cost],TEMP.Price,Temp.Amount";
				FillDataSet(dataSet, "Garment_Rental_Detail", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGarmentRentalSummaryReport(string sysDocID, string voucherID, DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromGroup, string toGroup)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = " SELECT DISTINCT COD.SysDocID+'-'+COD.VoucherID AS [Con No],C.CustomerID+'-'+C.CustomerName AS [Customer],\r\n                                DATEDIFF(day,CO.TransactionDate,CS.TransactionDate) AS Days,\r\n                                CSD.SysDocID+'-'+CSD.VoucherID AS [Set Id],CSD.SysDocID,CSD.VoucherID,CO.TransactionDate [Con Date],CS.TransactionDate AS [Set Date],\r\n                                (SELECT SUM(CE.Amount) FROM GarmentRental_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID) AS [Total Expense],\r\n                                (SELECT -1*SUM(IT.Assetvalue) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Cost],\r\n                                (SELECT -1*SUM(IT.UnitPrice*IT.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Amount],\r\n((SELECT -1*SUM(IT.UnitPrice*IT.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID)-(SELECT ISNULL(SUM(CE.Amount),0) FROM GarmentRental_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID)) AS Receivable\r\n                                FROM  Garment_Rental CO LEFT OUTER JOIN  Garment_Rental_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                                LEFT OUTER JOIN GarmentRental_Settlement_Detail CSD ON CSD.GarmentSysDocID = COD.SysDocID AND CSD.GarmentVoucherID = COD.VoucherID AND CSD.GarmentRowIndex = COD.RowIndex\r\n                                LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n                                LEFT JOIN GarmentRental_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                                LEFT JOIN Customer C ON C.CustomerID=CO.CustomerID\r\n                                WHERE COD.SysDocID= '" + sysDocID + "' AND COD.VoucherID= '" + voucherID + "'";
				if (fromCustomer != "")
				{
					text3 = text3 + " AND C.CustomerID >= '" + fromCustomer + "' ";
				}
				if (toCustomer != "")
				{
					text3 = text3 + " AND C.CustomerID <= '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					text3 = text3 + " AND C.CustomerClassID IN (SELECT ClassID FROM Customer_Class WHERE ClassID >= '" + fromCustomerClass + "')";
				}
				if (toCustomerClass != "")
				{
					text3 = text3 + " AND C.CustomerClassID IN (SELECT ClassID FROM Customer_Class WHERE ClassID <= '" + toCustomerClass + "')";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND C.CustomerGroupID IN (SELECT GroupID FROM Customer_Group WHERE GroupID >= '" + fromGroup + "')";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND C.CustomerGroupID IN (SELECT GroupID FROM Customer_Group WHERE GroupID <= '" + toGroup + "') ";
				}
				text3 = text3 + "AND CO. TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				FillDataSet(dataSet, "Garment_Rental", text3);
				new DataTable();
				foreach (DataRow row in dataSet.Tables["Garment_Rental"].Rows)
				{
					sysDocID = row["sysDocID"].ToString();
					voucherID = row["voucherID"].ToString();
					text3 = "SELECT '" + sysDocID + "' AS DocID, '" + voucherID + "' AS ReceiptNumber , TEMP.ProductID,TEMP.Description, TEMP.UnitID,TEMP.SysDocID,TEMP.VoucherID,\r\n                        TEMP.Quantity,TEMP.[QTY Settled],TEMP.[QTY Returned],Temp.Cost,TEMP.[Total Cost],TEMP.Price,Temp.Amount,\r\n                        SUM(Temp.Expense) AS [Item Expense]\r\n                        FROM \r\n                        (SELECT  COD.ProductID,COD.Description, COD.UnitID,CSD.SysDocID,CSD.VoucherID,\r\n                        ISNULL(COD.UnitQuantity,COD.Quantity) AS Quantity, (CSD.Quantity) [QTY Settled],(COD.QuantityReturned) [QTY Returned],\r\n                        (SELECT (SUM(IT.Assetvalue)/SUM(IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=CSD.ProductID AND IT.RowIndex = CSD.RowIndex) AS [Cost],\r\n                        (SELECT (SUM(IT.Assetvalue)/SUM(IT.Quantity))*(CSD.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.RowIndex=CSD.RowIndex  AND ABS(IT.Quantity)=CSD.Quantity AND IT.ProductID=CSD.ProductID ) AS [Total Cost],\r\n                        CSD.UnitPrice AS [Price],\r\n                        (SELECT (SUM(IT.UnitPrice*IT.Quantity))/SUM(IT.Quantity)*(CSD.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=CSD.ProductID  AND IT.RowIndex = CSD.RowIndex AND ABS(IT.Quantity) = CSD.Quantity) AS [Amount],\r\n                        (SELECT (CSD.Amount*(SELECT SUM(CE.Amount) FROM GarmentRental_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID)/(SELECT -1*(SUM(IT.UnitPrice*IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID))) AS Expense\r\n                        FROM  Garment_Rental CO LEFT OUTER JOIN  Garment_Rental_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                        LEFT OUTER JOIN GarmentRental_Settlement_Detail CSD ON CSD.GarmentSysDocID = COD.SysDocID AND CSD.GarmentVoucherID = COD.VoucherID AND CSD.GarmentRowIndex = COD.RowIndex\r\n                        LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n                        LEFT JOIN GarmentRental_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                        LEFT JOIN Customer C ON C.CustomerID=CO.CustomerID\r\n                        WHERE CSD.SysDocID='" + sysDocID + "' AND CSD.VoucherID= '" + voucherID + "' ";
					text3 = text3 + "AND CO. TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ) AS TEMP WHERE 1=1";
					text3 += "GROUP BY TEMP.ProductID,TEMP.Description,TEMP.SysDocID,TEMP.VoucherID, TEMP.UnitID,TEMP.Quantity,TEMP.[QTY Settled],TEMP.[QTY Returned],Temp.Cost,TEMP.[Total Cost],TEMP.Price,Temp.Amount";
					FillDataSet(dataSet, "Garment_Rental_Detail", text3);
				}
				dataSet.Relations.Add("GarmentRental_Rel", new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental_Detail"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool AllowDelete(string sysDocID, string voucherNumber)
		{
			string exp = "SELECT COUNT(*) FROM Garment_Rental_Return PEA WHERE  SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public DataSet GetGarmentRentalAgreement(string sysDocID, string voucherID, string CustomerID)
		{
			return GetGarmentRentalAgreement(sysDocID, new string[1]
			{
				voucherID
			}, CustomerID);
		}

		public DataSet GetGarmentRentalAgreement(string sysDocID, string[] voucherID, string CustomerID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,TransactionDate,ExpReturnDate,\r\n                                SI.SalesPersonID,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,Reference,Discount AS Discount,SI.PackageID,B.BOMName as PackageName,\r\n                                Total AS Total,SI.Note,SI.Discount,SI.Charges,SI.CashAmount,SI.CardAmount,SI.AmountPaid,SI.Balance\r\n                                FROM  Garment_Rental SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                INNER JOIN BOM B ON B.BOMID=SI.PackageID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Garment_Rental", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Garment_Rental"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,ProductID,Description,LocationID,ISNULL(UnitQuantity,Quantity) AS Quantity,\r\n                        UnitPrice AS UnitPrice,\r\n                        ISNULL(UnitQuantity,Quantity)*UnitPrice AS Total,UnitID\r\n                        FROM   Garment_Rental_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Garment_Rental_Detail", cmdText);
				cmdText = "SELECT  Customer.CustomerID, CustomerName,ForeignName, LegalName,ClassName,Customer.ContactName,Customer.TermID,\r\n                            AreaName,Customer.AcceptCheckPayment,CA.Phone1,CA.Mobile,CA.Fax,CA.Email,CA.Website,\r\n                            Customer.AcceptPDC,Customer.CreditLimitType,Customer.CreditAmount,GroupName,\r\n                            Customer.CountryID,Customer.ShippingMethodID,Customer.IsInactive,IsHold,Customer.BankName,Customer.BankBranch,Customer.BankAccountNumber,\r\n                            Customer.StatementSendingMethod,Customer.PaymentTermID,IsCustomerSince,\r\n                            Customer.Note,Customer.PaymentMethodID,Customer.SalesPersonID + '-' + Salesperson.FullName AS Salesperson\r\n                            FROM Customer LEFT OUTER JOIN Customer_Class CusClass ON Customer.CustomerClassID=CusClass.ClassID\r\n                            LEFT OUTER JOIN Customer_Group Cus_Group ON Customer.CustomerGroupID=Cus_Group.GroupID\r\n                            LEFT OUTER JOIN Area Area ON Customer.AreaID=Area.AreaID\r\n                            LEFT OUTER JOIN Salesperson Salesperson ON Customer.SalespersonID=Salesperson.SalespersonID\r\n                            LEFT OUTER JOIN Customer_Address CA ON Customer.CustomerID=CA.CustomerID AND Customer.PrimaryAddressID=CA.AddressID \r\n                            WHERE 1=1 ";
				if (CustomerID != "")
				{
					cmdText = cmdText + " AND Customer.CustomerID='" + CustomerID + "'";
				}
				FillDataSet(dataSet, "CustomerDetails", cmdText);
				dataSet.Relations.Add("CustomerGarmentRental", new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental_Detail"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Garment_Rental"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Garment_Rental"].Rows)
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
	}
}
