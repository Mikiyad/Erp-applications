using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class GarmentRentalReturn : StoreObject
	{
		private const string GARMENTRENTALRETURN_TABLE = "GarmetRentalReturn";

		private const string GARMENTRENTALRETURNDETAIL_TABLE = "GarmetRental_Return_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string DISCOUNT_PARM = "@Discount";

		private const string CHARGES_PARM = "@Charges";

		private const string TOTAL_PARM = "@Total";

		private const string REGISTERID_PARM = "@RegisterID";

		public const string CASHAMOUNT_PARM = "@CashAmount";

		public const string CARDAMOUNT_PARM = "@CardAmount";

		public const string AMOUNTPAID_PARM = "@AmountPaid";

		public const string BALANCE_PARM = "@Balance";

		public const string CASHACCOUNTID_PARM = "@CashAccountID";

		public const string CARDACCOUNTID_PARM = "@CardAccountID";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string QUANTITYRETURNED_PARM = "@QuantityReturned";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string CONSIGNLOCATIONID_PARM = "@ConsignLocationID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public GarmentRentalReturn(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateGarmentRentalReturnText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Garment_Rental_Return", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("Total", "@Total"), new FieldValue("Reference", "@Reference"), new FieldValue("Discount", "@Discount"), new FieldValue("Charges", "@Charges"), new FieldValue("CashAmount", "@CashAmount"), new FieldValue("CardAmount", "@CardAmount"), new FieldValue("AmountPaid", "@AmountPaid"), new FieldValue("Balance", "@Balance"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("CashAccountID", "@CashAccountID"), new FieldValue("CardAccountID", "@CardAccountID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Garment_Rental_Return", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateGarmentRentalReturnCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateGarmentRentalReturnText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateGarmentRentalReturnText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@Charges", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@CashAmount", SqlDbType.Decimal);
			parameters.Add("@CardAmount", SqlDbType.Decimal);
			parameters.Add("@AmountPaid", SqlDbType.Decimal);
			parameters.Add("@Balance", SqlDbType.Decimal);
			parameters.Add("@CashAccountID", SqlDbType.NVarChar);
			parameters.Add("@CardAccountID", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Charges"].SourceColumn = "Charges";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@CashAmount"].SourceColumn = "CashAmount";
			parameters["@CardAmount"].SourceColumn = "CardAmount";
			parameters["@AmountPaid"].SourceColumn = "AmountPaid";
			parameters["@Balance"].SourceColumn = "Balance";
			parameters["@CashAccountID"].SourceColumn = "CashAccountID";
			parameters["@CardAccountID"].SourceColumn = "CardAccountID";
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

		private string GetInsertUpdateGarmentRentalReturnDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Garment_Rental_Return_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ConsignLocationID", "@ConsignLocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateGarmentRentalReturnDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateGarmentRentalReturnDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateGarmentRentalReturnDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@ConsignLocationID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@ConsignLocationID"].SourceColumn = "ConsignLocationID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(GarmentRentalReturnData journalData)
		{
			return true;
		}

		public bool InsertUpdateGarmentRentalReturn(GarmentRentalReturnData garmentRentalReturnData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateGarmentRentalReturnCommand = GetInsertUpdateGarmentRentalReturnCommand(isUpdate);
			try
			{
				DataRow dataRow = garmentRentalReturnData.GarmentRentalReturnTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string text3 = dataRow["SourceSysDocID"].ToString();
				string voucherID = dataRow["SourceVoucherID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Garment_Rental_Return", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text4 = new Databases(base.DBConfig).GetFieldValue("System_Document", "ConsignOutLocationID", "SysDocID", text3, sqlTransaction).ToString();
				if (text4 == "")
				{
					throw new CompanyException("There is no store assigned to this consignment out document.");
				}
				if (garmentRentalReturnData.GarmentRentalReturnTable.Rows.Count > 0)
				{
					DataRow dataRow2 = garmentRentalReturnData.GarmentRentalReturnTable.Rows[0];
					string registerID = dataRow2["RegisterID"].ToString();
					string text5 = "";
					string text6 = "";
					text5 = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CardReceivedAccountID");
					text6 = (string)(dataRow2["CashAccountID"] = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID"));
					dataRow2["CardAccountID"] = text5;
					dataRow2.EndEdit();
				}
				if (isUpdate)
				{
					flag &= DeleteGarmentRentalReturnDetailsRows(text2, text, isDeletingTransaction: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(113, text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				float num = 0f;
				foreach (DataRow row in garmentRentalReturnData.GarmentRentalReturnDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text7 = row["ProductID"].ToString();
					row["ConsignLocationID"] = text4;
					string idFieldValue = row["SourceSysDocID"].ToString();
					string checkFieldValue = row["SourceVoucherID"].ToString();
					int num2 = int.Parse(row["SourceRowIndex"].ToString());
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_Out_Detail", "QuantityReturned", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, "RowIndex", num2, sqlTransaction);
					float num3 = 0f;
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						num3 = float.Parse(fieldValue.ToString());
					}
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_Out_Detail", "QuantitySettled", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, "RowIndex", num2, sqlTransaction);
					float num4 = 0f;
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						num4 = float.Parse(fieldValue.ToString());
					}
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_Out_Detail", "Quantity", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, "RowIndex", num2, sqlTransaction);
					float num5 = 0f;
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						num5 = float.Parse(fieldValue.ToString());
					}
					if (num3 + num4 > num5)
					{
						throw new CompanyException("Returned quantity cannot be greater than balance quantity.", 1026);
					}
					num = float.Parse(row["Quantity"].ToString());
					num3 += num;
					UpdateRowReturnedQuantity(text3, voucherID, num2, num, sqlTransaction);
					string text8 = "";
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text7, sqlTransaction);
					if (fieldValue2 != null)
					{
						text8 = fieldValue2.ToString();
					}
					if (text8 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text8)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text7, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text7 + "\nUnit:" + row["UnitID"].ToString());
						float num6 = float.Parse(obj["Factor"].ToString());
						string text9 = obj["FactorType"].ToString();
						num = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num6;
						row["FactorType"] = text9;
						row["UnitQuantity"] = row["Quantity"];
						num = ((!(text9 == "M")) ? float.Parse(Math.Round(num * num6, 5).ToString()) : float.Parse(Math.Round(num / num6, 5).ToString()));
						row["Quantity"] = num;
					}
				}
				insertUpdateGarmentRentalReturnCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(garmentRentalReturnData, "Garment_Rental_Return", insertUpdateGarmentRentalReturnCommand)) : (flag & Insert(garmentRentalReturnData, "Garment_Rental_Return", insertUpdateGarmentRentalReturnCommand)));
				insertUpdateGarmentRentalReturnCommand = GetInsertUpdateGarmentRentalReturnDetailsCommand(isUpdate: false);
				insertUpdateGarmentRentalReturnCommand.Transaction = sqlTransaction;
				if (garmentRentalReturnData.Tables["Garment_Rental_Return_Detail"].Rows.Count > 0)
				{
					flag &= Insert(garmentRentalReturnData, "Garment_Rental_Return_Detail", insertUpdateGarmentRentalReturnCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in garmentRentalReturnData.GarmentRentalReturnDetailTable.Rows)
				{
					DataRow dataRow5 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["SysDocID"] = row2["SysDocID"];
					dataRow5["VoucherID"] = row2["VoucherID"];
					dataRow5["LocationID"] = text4;
					dataRow5["ProductID"] = row2["ProductID"];
					dataRow5["UnitID"] = row2["UnitID"];
					dataRow5["Quantity"] = -1f * float.Parse(row2["Quantity"].ToString());
					if (row2["UnitQuantity"] != DBNull.Value)
					{
						dataRow5["UnitQuantity"] = -1f * float.Parse(row2["UnitQuantity"].ToString());
					}
					else
					{
						dataRow5["UnitQuantity"] = DBNull.Value;
					}
					dataRow5["Factor"] = row2["UnitFactor"];
					dataRow5["FactorType"] = row2["FactorType"];
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["RowIndex"] = row2["RowIndex"];
					dataRow5["SysDocType"] = (byte)113;
					dataRow5["UnitPrice"] = 0;
					dataRow5["TransactionDate"] = dataRow["TransactionDate"];
					dataRow5["TransactionType"] = (byte)23;
					dataRow5.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
				}
				inventoryTransactionData.Merge(garmentRentalReturnData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(garmentRentalReturnData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in garmentRentalReturnData.GarmentRentalReturnDetailTable.Rows)
				{
					DataRow dataRow7 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["SysDocID"] = row3["SysDocID"];
					dataRow7["VoucherID"] = row3["VoucherID"];
					dataRow7["LocationID"] = row3["LocationID"];
					dataRow7["ProductID"] = row3["ProductID"];
					dataRow7["UnitID"] = row3["UnitID"];
					dataRow7["Quantity"] = row3["Quantity"];
					dataRow7["UnitQuantity"] = row3["UnitQuantity"];
					dataRow7["Factor"] = row3["UnitFactor"];
					dataRow7["FactorType"] = row3["FactorType"];
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["UnitPrice"] = 0;
					dataRow7["RowIndex"] = row3["RowIndex"];
					dataRow7["SysDocType"] = (byte)113;
					dataRow7["TransactionDate"] = dataRow["TransactionDate"];
					dataRow7["TransactionType"] = (byte)23;
					dataRow7.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow7);
				}
				string textCommand = "SELECT PLD.*,PL.ProductionDate,PL.ExpiryDate FROM Product_Lot_Issue_Detail PLD INNER JOIN Product_Lot PL ON PL.LotNumber = PLD.LotNumber WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product_Lot_Issue_Detail", textCommand);
				DataTable dataTable = garmentRentalReturnData.Tables["Product_Lot_Receiving_Detail"];
				dataTable.Rows.Clear();
				foreach (DataRow row4 in dataSet.Tables["Product_Lot_Issue_Detail"].Rows)
				{
					DataRow dataRow9 = dataTable.NewRow();
					dataRow9["ProductID"] = row4["ProductID"];
					dataRow9["LocationID"] = text4;
					dataRow9["LotNumber"] = row4["LotNumber"];
					dataRow9["ProductionDate"] = row4["ProductionDate"];
					dataRow9["ExpiryDate"] = row4["ExpiryDate"];
					dataRow9["LotQty"] = row4["SoldQty"];
					dataRow9["SysDocID"] = text2;
					dataRow9["VoucherID"] = text;
					dataRow9["RowIndex"] = row4["RowIndex"];
					dataTable.Rows.Add(dataRow9);
				}
				inventoryTransactionData.Merge(garmentRentalReturnData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(garmentRentalReturnData, isUpdate: false, sqlTransaction);
				flag &= new GarmentRental(base.DBConfig).CloseOpenGarmentment(text3, voucherID, sqlTransaction);
				GLData journalData = CreateGarmentRentalReturnGLData(garmentRentalReturnData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Garment_Rental_Return", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Garment Rental Return";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Garment_Rental_Return", "VoucherID", sqlTransaction);
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

		private GLData CreateGarmentRentalReturnGLData(GarmentRentalReturnData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.GarmentRentalReturnTable.Rows[0];
			string text = dataRow["CustomerID"].ToString();
			string idFieldValue = dataRow["SysDocID"].ToString();
			string idFieldValue2 = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", idFieldValue, sqlTransaction).ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
			string value = new Databases(base.DBConfig).GetFieldValue("Location", "DiscountGivenAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
			new Databases(base.DBConfig).GetFieldValue("Location", "SalesTaxAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
			string value2 = new Databases(base.DBConfig).GetFieldValue("Location", "SalesAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
			string text3 = new Databases(base.DBConfig).GetFieldValue("Location", "ARAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
			string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
			if (dataRow["CurrencyID"] != DBNull.Value)
			{
				_ = (baseCurrencyID != dataRow["CurrencyID"].ToString());
			}
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.GarmentRentalReturn;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["CurrencyID"] = dataRow["CurrencyID"];
			dataRow2["CurrencyRate"] = 0;
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Garment Rental Return - ";
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal num = default(decimal);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			foreach (DataRow row in transactionData.GarmentRentalReturnDetailTable.Rows)
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
			}
			decimal.TryParse(dataRow["AmountPaid"].ToString(), out result);
			decimal.TryParse(dataRow["Total"].ToString(), out result2);
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
			decimal result3 = default(decimal);
			decimal result4 = default(decimal);
			if (dataRow["Discount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["Discount"].ToString(), out result3);
			}
			dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["AccountID"] = value2;
			dataRow4["PayeeID"] = text;
			dataRow4["PayeeType"] = "A";
			dataRow4["IsARAP"] = false;
			dataRow4["Debit"] = DBNull.Value;
			dataRow4["Credit"] = result2;
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
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
			dataRow4["PayeeType"] = "C";
			dataRow4["IsARAP"] = true;
			dataRow4["Debit"] = result2 - result3;
			dataRow4["Credit"] = DBNull.Value;
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			if (dataRow["Charges"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["Charges"].ToString(), out result4);
			}
			if (result3 > 0m)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = value;
				dataRow4["PayeeID"] = text;
				dataRow4["PayeeType"] = "A";
				dataRow4["Debit"] = result3;
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			decimal result5 = default(decimal);
			decimal result6 = default(decimal);
			decimal.TryParse(dataRow["CashAmount"].ToString(), out result6);
			decimal.TryParse(dataRow["CardAmount"].ToString(), out result5);
			string text6 = "";
			if (result6 > 0m)
			{
				text6 = "Cash/";
			}
			else if (result5 > 0m)
			{
				text6 += "Card";
			}
			dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["AccountID"] = text5;
			dataRow4["PayeeID"] = text;
			dataRow4["PayeeType"] = "C";
			dataRow4["IsARAP"] = true;
			if (result > 0m)
			{
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = dataRow["AmountPaid"];
			}
			else
			{
				decimal num2 = Math.Abs(result);
				dataRow4["Debit"] = num2;
				dataRow4["Credit"] = DBNull.Value;
			}
			dataRow4["Reference"] = dataRow["Reference"];
			if (dataRow["Note"].ToString() != string.Empty)
			{
				dataRow4["Description"] = dataRow["Note"];
			}
			else
			{
				dataRow4["Description"] = "Payment Received" + text6;
			}
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			if (result6 != 0m)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["CashAccountID"].ToString();
				dataRow4["PayeeID"] = text;
				dataRow4["PayeeType"] = "A";
				dataRow4["IsARAP"] = false;
				if (result6 > 0m)
				{
					dataRow4["Debit"] = dataRow["CashAmount"];
					dataRow4["Credit"] = DBNull.Value;
				}
				else
				{
					decimal num3 = Math.Abs(result6);
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = num3;
				}
				if (dataRow["Note"].ToString() != string.Empty)
				{
					dataRow4["Description"] = dataRow["Note"];
				}
				else
				{
					dataRow4["Description"] = "Payment Received Cash ";
				}
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			if (result5 != 0m)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["CardAccountID"].ToString();
				dataRow4["PayeeID"] = text;
				dataRow4["PayeeType"] = "A";
				dataRow4["IsARAP"] = false;
				if (result5 > 0m)
				{
					dataRow4["Debit"] = dataRow["CardAmount"];
					dataRow4["Credit"] = DBNull.Value;
				}
				else
				{
					decimal num4 = Math.Abs(result5);
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = num4;
				}
				if (dataRow["Note"].ToString() != string.Empty)
				{
					dataRow4["Description"] = dataRow["Note"];
				}
				else
				{
					dataRow4["Description"] = "Payment Received Card ";
				}
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		public GarmentRentalReturnData GetGarmentRentalReturnByID(string sysDocID, string voucherID)
		{
			try
			{
				GarmentRentalReturnData garmentRentalReturnData = new GarmentRentalReturnData();
				string textCommand = "SELECT * FROM Garment_Rental_Return WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(garmentRentalReturnData, "Garment_Rental_Return", textCommand);
				if (garmentRentalReturnData == null || garmentRentalReturnData.Tables.Count == 0 || garmentRentalReturnData.Tables["Garment_Rental_Return"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT SD.*,P.Description,P.ItemType,P.IsTrackLot,P.IsTrackSerial,COD.Quantity AS DeliveredQty,COD.RowIndex AS SourceRowIndex,QuantitySettled,QuantityReturned, COD.Quantity - ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0)  AS BalanceQty\r\n                        ,CO.PackageID,CO.AmountPaid as BalanceAmount FROM Garment_Rental_Return_Detail SD INNER JOIN Product P ON SD.ProductID=P.ProductID\r\n                        INNER JOIN Garment_Rental_Detail COD ON COD.SysDocID=SD.SourceSysDocID\r\n                        AND COD.VoucherID=SD.SourceVoucherID AND COD.RowIndex=SD.SourceRowIndex\r\n                        INNER JOIN Garment_Rental CO ON CO.SysDocID=SD.SourceSysDocID  AND CO.VoucherID=SD.SourceVoucherID\r\n                        WHERE SD.VoucherID='" + voucherID + "' AND SD.SysDocID='" + sysDocID + "'";
				FillDataSet(garmentRentalReturnData, "Garment_Rental_Return_Detail", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (garmentRentalReturnData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					garmentRentalReturnData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				garmentRentalReturnData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return garmentRentalReturnData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteGarmentRentalReturnDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				GarmentRentalReturnData garmentRentalReturnData = new GarmentRentalReturnData();
				string textCommand = "SELECT SO.SourceSysDocID,SO.SourceVoucherID, SOD.*,ISNULL(ISVOID,'False') AS IsVoid\r\n                                    FROM Garment_Rental_Return_Detail SOD INNER JOIN Garment_Rental_Return SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                                    WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(garmentRentalReturnData, "Garment_Rental_Return_Detail", textCommand, sqlTransaction);
				if (garmentRentalReturnData.GarmentRentalReturnDetailTable.Rows.Count == 0)
				{
					return true;
				}
				bool result = false;
				bool.TryParse(garmentRentalReturnData.GarmentRentalReturnDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				string sysDocID2 = garmentRentalReturnData.GarmentRentalReturnDetailTable.Rows[0]["SourceSysDocID"].ToString();
				string voucherID2 = garmentRentalReturnData.GarmentRentalReturnDetailTable.Rows[0]["SourceVoucherID"].ToString();
				if (!result)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(113, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
					foreach (DataRow row in garmentRentalReturnData.GarmentRentalReturnDetailTable.Rows)
					{
						row["ProductID"].ToString();
						sysDocID2 = row["SourceSysDocID"].ToString();
						voucherID2 = row["SourceVoucherID"].ToString();
						string s = row["SourceRowIndex"].ToString();
						float num = float.Parse(row["Quantity"].ToString());
						flag &= UpdateRowReturnedQuantity(sysDocID2, voucherID2, int.Parse(s), -1f * num, sqlTransaction);
					}
				}
				flag &= new ConsignOut(base.DBConfig).CloseOpenConsignment(sysDocID2, voucherID2, sqlTransaction);
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Garment_Rental_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowReturnedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantitySettled,QuantityReturned FROM Garment_Rental_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Consign", textCommand);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					if (dataRow["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(dataRow["UnitQuantity"].ToString(), out result);
					}
					else
					{
						float.TryParse(dataRow["Quantity"].ToString(), out result);
					}
					float.TryParse(dataRow["QuantityReturned"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Garment_Rental_Detail SET QuantityReturned=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool VoidGarmentRentalReturn(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Garment_Rental_Return SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				GarmentRentalReturnData garmentRentalReturnData = new GarmentRentalReturnData();
				exp = "SELECT CO.SourceSysDocID,CO.SourceVoucherID, COD.*  FROM Garment_Rental_Return_Detail COD INNER JOIN Garment_Rental_Return CO ON COD.SysDocID=CO.SysDocID AND COD.VoucherID=CO.VoucherID\r\n                              WHERE COD.SysDocID = '" + sysDocID + "' AND COD.VoucherID = '" + voucherID + "'";
				FillDataSet(garmentRentalReturnData, "ConsignOut_Return_Detail", exp, sqlTransaction);
				string sysDocID2 = garmentRentalReturnData.Tables["Garment_Rental_Return_Detail"].Rows[0]["SourceSysDocID"].ToString();
				string voucherID2 = garmentRentalReturnData.Tables["Garment_Rental_Return_Detail"].Rows[0]["SourceVoucherID"].ToString();
				foreach (DataRow row in garmentRentalReturnData.GarmentRentalReturnDetailTable.Rows)
				{
					row["ProductID"].ToString();
					int result = 0;
					int.TryParse(row["ConsignRowIndex"].ToString(), out result);
					float result2 = 0f;
					if (row["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(row["UnitQuantity"].ToString(), out result2);
					}
					else
					{
						float.TryParse(row["Quantity"].ToString(), out result2);
					}
					flag &= UpdateRowReturnedQuantity(sysDocID2, voucherID2, result, -1f * result2, sqlTransaction);
				}
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(113, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new GarmentRental(base.DBConfig).CloseOpenGarmentment(sysDocID2, voucherID2, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Consign Out Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteGarmentRentalReturn(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteGarmentRentalReturnDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Garment_Rental_Return WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Garment Rental Return", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetUninvoicedConsignOuts(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer] FROM Consign_Out DN\r\n                             INNER JOIN Customer C ON DN.CustomerID=C.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False'";
				if (customerID != "")
				{
					text = text + " AND DN.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Garment_Rental_Return", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGarmentRentalReturnToPrint(string sysDocID, string voucherID)
		{
			return GetGarmentRentalReturnToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetGarmentRentalReturnToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SI.SysDocID,SI.VoucherID,SI.CustomerID,CustomerName,SI.CustomerAddress,SI.TransactionDate,\r\n                               CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                SI.Note,SI.Discount,SI.Charges,SI.Total,GR.PackageID,B.BOMName as PackageName,GR.ExpReturnDate as ExpReturnDate,SI.CashAmount,SI.CardAmount,SI.AmountPaid,SI.Balance\r\n                                FROM  Garment_Rental_Return SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                INNER JOIN Garment_Rental GR ON GR.SysDocID=SI.SourceSysDocID   AND GR.VoucherID=SI.SourceVoucherID \r\n\t\t\t\t\t\t\t\tINNER JOIN BOM B ON B.BOMID=GR.PackageID\r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Garment_Rental_Return", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Garment_Rental_Return"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT SD.*,P.Description,P.ItemType,P.IsTrackLot,P.IsTrackSerial,COD.Quantity AS DeliveredQty,COD.RowIndex AS ConsignRowIndex,QuantitySettled,QuantityReturned, COD.Quantity - ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0) + SD.Quantity AS BalanceQty\r\n                        FROM Garment_Rental_Return_Detail SD INNER JOIN Product P ON SD.ProductID=P.ProductID\r\n                        INNER JOIN Garment_Rental_Detail COD ON COD.SysDocID=SD.SourceSysDocID\r\n                        AND COD.VoucherID=SD.SourceVoucherID AND COD.RowIndex=SD.SourceRowIndex\r\n                        WHERE SD.SysDocID='" + sysDocID + "' AND SD.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Garment_Rental_Return_Detail", cmdText);
				dataSet.Relations.Add("GarmentRentalReturn", new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental_Return"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental_Return"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Garment_Rental_Return_Detail"].Columns["SysDocID"],
					dataSet.Tables["Garment_Rental_Return_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
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
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate ,\r\n                            INV.SalespersonID [Salesperson],SUM(DND.Quantity) AS Quantity\r\n                            FROM         Garment_Rental_Return INV\r\n                            INNER JOIN Garment_Rental_Return_Detail DND ON DND.SysDocID=INV.SysDocID AND DND.VoucherID=INV.VoucherID\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID ";
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
			FillDataSet(dataSet, "Garment_Rental_Return", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenConsignments(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT CO.SysDocID [Doc ID], CO.VoucherID [Number],CO.CustomerID [Customer Code], CUS.CustomerName [Customer Name],TransactionDate [Date]\r\n                            FROM Consign_Out CO INNER JOIN Consign_Out_Detail COD\r\n                            ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            INNER JOIN Customer CUS ON CO.CustomerID=CUS.CustomerID ";
				if (customerID != "")
				{
					str = str + " WHERE CO.CustomerID='" + customerID + "'";
				}
				str += " GROUP BY CO.SysDocID, CO.VoucherID,CO.CustomerID,TransactionDate, CUS.CustomerName\r\n                                HAVING\r\n                                SUM(ISNULL(COD.Quantity,0)-ISNULL(COD.QuantitySettled,0))>0 ";
				SqlCommand sqlCommand = new SqlCommand(str);
				FillDataSet(dataSet, "Garment_Rental_Return", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool ConsignmentHasSettlement(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Consign_Out_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantitySettled,0) + ISNULL(QuantityReturned,0)) >0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}
	}
}
