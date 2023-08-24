using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class DeliveryReturn : StoreObject
	{
		private const string DELIVERYRETURN_TABLE = "Delivery_Return";

		private const string DELIVERYRETURNDETAIL_TABLE = "Delivery_Return_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string DNOTESYSDOCID_PARM = "@DNoteSysDocID";

		private const string DNOTEVOUCHERID_PARM = "@DNoteVoucherID";

		private const string ISVOID_PARM = "@IsVoid";

		private const string ISEXPORT_PARM = "@IsExport";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TOTAL_PARM = "@Total";

		private const string DRIVERID_PARM = "@DriverID";

		private const string VEHICLEID_PARM = "@VehicleID";

		private const string ISCOMPLETERETURN_PARM = "@IsCompleteReturn";

		private const string REASONID_PARM = "@ReasonID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REMARKS_PARM = "@Remarks";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string DNROWINDEX_PARM = "@DNRowIndex";

		public DeliveryReturn(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateDeliveryReturnText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Delivery_Return", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("ReasonID", "@ReasonID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("TermID", "@TermID"), new FieldValue("IsExport", "@IsExport"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("IsCompleteReturn", "@IsCompleteReturn"), new FieldValue("Note", "@Note"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Delivery_Return", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDeliveryReturnCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDeliveryReturnText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDeliveryReturnText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@ReasonID", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters.Add("@IsCompleteReturn", SqlDbType.Bit);
			parameters.Add("@IsExport", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@ReasonID"].SourceColumn = "ReasonID";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@IsCompleteReturn"].SourceColumn = "IsCompleteReturn";
			parameters["@IsExport"].SourceColumn = "IsExport";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
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

		private string GetInsertUpdateDeliveryReturnDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Delivery_Return_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("DNRowIndex", "@DNRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDeliveryReturnDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDeliveryReturnDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDeliveryReturnDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@DNRowIndex", SqlDbType.Int);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@DNRowIndex"].SourceColumn = "DNRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(DeliveryReturnData journalData)
		{
			return true;
		}

		public bool InsertUpdateDeliveryReturn(DeliveryReturnData deliveryReturnData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateDeliveryReturnCommand = GetInsertUpdateDeliveryReturnCommand(isUpdate);
			try
			{
				DataRow dataRow = deliveryReturnData.DeliveryReturnTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string text3 = dataRow["DNoteSysDocID"].ToString();
				string text4 = dataRow["DNoteVoucherID"].ToString();
				bool result = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Delivery_Note", "IsInvoiced", "SysDocID", text3, "VoucherID", text4, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					bool.TryParse(fieldValue.ToString(), out result);
				}
				if (result && base.DBConfig.UserID.ToLower() != "axolonfixer")
				{
					flag = false;
					throw new CompanyException("This delivery note is already invoiced and cannot be returned or modifed. Use the sales return instead.", 1011);
				}
				bool flag2 = false;
				bool num = bool.Parse(dataRow["IsExport"].ToString());
				string text5 = "";
				text5 = ((!num) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text5 != "")
				{
					salesFlows = (SalesFlows)int.Parse(text5.ToString());
				}
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					flag2 = true;
				}
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					flag &= ValidateDate(deliveryReturnData, isUpdate, sqlTransaction);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Delivery_Return", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in deliveryReturnData.DeliveryReturnDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text6 = row["ProductID"].ToString();
					string text7 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text6, sqlTransaction);
					if (fieldValue != null)
					{
						text7 = fieldValue.ToString();
					}
					if (text7 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text7)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text6, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text6 + "\nUnit:" + row["UnitID"].ToString());
						float num2 = float.Parse(obj["Factor"].ToString());
						string text8 = obj["FactorType"].ToString();
						float num3 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num2;
						row["FactorType"] = text8;
						row["UnitQuantity"] = row["Quantity"];
						num3 = ((!(text8 == "M")) ? float.Parse(Math.Round(num3 * num2, 5).ToString()) : float.Parse(Math.Round(num3 / num2, 5).ToString()));
						row["Quantity"] = num3;
					}
				}
				insertUpdateDeliveryReturnCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(deliveryReturnData, "Delivery_Return", insertUpdateDeliveryReturnCommand)) : (flag & Insert(deliveryReturnData, "Delivery_Return", insertUpdateDeliveryReturnCommand)));
				insertUpdateDeliveryReturnCommand = GetInsertUpdateDeliveryReturnDetailsCommand(isUpdate: false);
				insertUpdateDeliveryReturnCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteDeliveryReturnDetailsRows(text2, text, isDeletingTransaction: false, sqlTransaction);
					if (flag2)
					{
						flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(29, text2, text, isDeletingTransaction: false, sqlTransaction);
					}
				}
				foreach (DataRow row2 in deliveryReturnData.DeliveryReturnDetailTable.Rows)
				{
					string exp = "SELECT Count(VoucherID) FROM Delivery_Note_Detail WHERE (ISNULL(UnitQuantity,Quantity)- QuantityReturned)<0 AND \r\n                                   SysDocID='" + text3 + "' AND VoucherID='" + text4 + "' AND RowIndex=" + row2["DNRowIndex"].ToString();
					fieldValue = ExecuteScalar(exp, sqlTransaction);
					if (fieldValue != null && fieldValue.ToString() != "" && int.Parse(fieldValue.ToString()) > 0)
					{
						flag = false;
						throw new CompanyException("Returned quantity cannot be greater than delivered quantity.", 1012);
					}
				}
				if (deliveryReturnData.Tables["Delivery_Return_Detail"].Rows.Count > 0)
				{
					flag &= Insert(deliveryReturnData, "Delivery_Return_Detail", insertUpdateDeliveryReturnCommand);
				}
				if (flag2)
				{
					InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row3 in deliveryReturnData.DeliveryReturnDetailTable.Rows)
					{
						DataRow dataRow5 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["SysDocID"] = row3["SysDocID"];
						dataRow5["VoucherID"] = row3["VoucherID"];
						dataRow5["LocationID"] = row3["LocationID"];
						dataRow5["UnitID"] = row3["UnitID"];
						dataRow5["ProductID"] = row3["ProductID"];
						dataRow5["Quantity"] = decimal.Parse(row3["Quantity"].ToString());
						dataRow5["Reference"] = dataRow["Reference"];
						dataRow5["SysDocType"] = (byte)29;
						dataRow5["TransactionDate"] = dataRow["TransactionDate"];
						dataRow5["TransactionType"] = (byte)3;
						dataRow5["UnitPrice"] = 0;
						dataRow5["PayeeType"] = "C";
						dataRow5["PayeeID"] = dataRow["CustomerID"];
						dataRow5["RefSysDocID"] = dataRow["DNoteSysDocID"];
						dataRow5["RefVoucherID"] = dataRow["DNoteVoucherID"];
						dataRow5["RefRowIndex"] = row3["DNRowIndex"];
						dataRow5["RowIndex"] = row3["RowIndex"];
						dataRow5["DivisionID"] = dataRow["DivisionID"];
						dataRow5["CompanyID"] = dataRow["CompanyID"];
						if (row3["UnitQuantity"] != DBNull.Value && row3["UnitFactor"] != DBNull.Value)
						{
							dataRow5["UnitQuantity"] = row3["UnitQuantity"];
							dataRow5["Factor"] = row3["UnitFactor"];
							dataRow5["FactorType"] = row3["FactorType"];
						}
						dataRow5.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
					}
					inventoryTransactionData.Merge(deliveryReturnData.Tables["Product_Lot_Receiving_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(deliveryReturnData, isUpdate: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
					string exp2 = " UPDATE Inventory_Transactions SET  \r\n                                RefTransactionID = (SELECT TransactionID FROM Inventory_Transactions IT2 WHERE IT2.SysDocID = DR.DNoteSysDocID AND IT2.VoucherID = DR.DNoteVoucherID AND IT2.RowIndex = DRD.DNRowIndex)\r\n                                FROM Inventory_Transactions IT\r\n                                INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = IT.SysDocID  AND DRD.VoucherID = IT.VoucherID AND DRD.RowIndex = IT.RowIndex\r\n                                INNER JOIN Delivery_Return DR ON DR.SysDocID = DRD.SysDocID AND DR.VoucherID = DRD.VoucherID\r\n                                 WHERE IT.SysDocID = '" + text2 + "' AND IT.VoucherID = '" + text + "'";
					flag &= (new InventoryTransaction(base.DBConfig).ExecuteNonQuery(exp2, sqlTransaction) >= 0);
					GLData journalData = CreateReturnGLData(deliveryReturnData, sqlTransaction);
					flag = ((!(base.DBConfig.UserID.ToLower() == "axolonfixer")) ? (flag & new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction)) : ((new Journal(base.DBConfig).GetJournalID(text2, text, sqlTransaction) >= 0) ? (flag & new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction)) : (flag & new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate: false, sqlTransaction))));
					flag &= UpdateInventoryTransactionRowID(text2, text, sqlTransaction);
				}
				foreach (DataRow row4 in deliveryReturnData.DeliveryReturnDetailTable.Rows)
				{
					string exp3 = "UPDATE Delivery_Note_Detail SET QuantityReturned = ISNULL(QuantityReturned,0) +" + row4["Quantity"].ToString() + " WHERE \r\n                                   SysDocID='" + text3 + "' AND VoucherID='" + text4 + "' AND RowIndex=" + row4["DNRowIndex"].ToString();
					flag &= (ExecuteNonQuery(exp3, sqlTransaction) > 0);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Delivery_Return", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Delivery Return";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Delivery_Return", "VoucherID", sqlTransaction);
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

		private bool ValidateDate(DeliveryReturnData deliveryReturnData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(DateTime.Parse(DateTime.Parse(deliveryReturnData.DeliveryReturnTable.Rows[0]["TransactionDate"].ToString()).ToShortDateString()));
				DataRow dataRow = deliveryReturnData.DeliveryReturnTable.Rows[0];
				string text2 = dataRow["DNoteSysDocID"].ToString();
				string text3 = dataRow["DNoteVoucherID"].ToString();
				string exp = "Select Case When CONVERT(date,TransactionDate) > CONVERT(date,'" + text + "') Then 'True' Else 'False' End From Delivery_Note Where SysDocID='" + text2 + "' AND VoucherID='" + text3 + "'";
				if (bool.Parse(ExecuteScalar(exp, sqlTransaction).ToString()))
				{
					throw new CompanyException("Selected DN date is greater than return date.");
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		private bool UpdateInventoryTransactionRowID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Delivery_Return_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateReturnGLData(DeliveryReturnData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.DeliveryReturnTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string value = dataRow["CustomerID"].ToString();
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				string value2 = dataRow["CompanyID"].ToString();
				string value3 = dataRow["DivisionID"].ToString();
				string textCommand = "SELECT SD.LocationID,  LOC.UnInvoicedInventoryAccountID, LOC.InventoryAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                WHERE SysDocID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string text3 = dataRow2["UnInvoicedInventoryAccountID"].ToString();
				bool result = false;
				bool.TryParse(dataRow["IsExport"].ToString(), out result);
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.DeliveryReturn;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Delivery Return - " + text2;
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num = default(decimal);
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				decimal d = default(decimal);
				foreach (DataRow row in transactionData.DeliveryReturnDetailTable.Rows)
				{
					string text4 = row["ProductID"].ToString();
					string warehouseLocationID = row["LocationID"].ToString();
					int rowIndex = int.Parse(row["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text4, docLocationID, warehouseLocationID, text, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					dataRow2 = dataSet.Tables[0].Rows[0];
					string text5 = dataRow2["InventoryAssetAccountID"].ToString();
					if (!(text5 == text3))
					{
						ItemTypes itemTypes = ItemTypes.Inventory;
						object obj2 = dataRow2["ItemType"].ToString();
						if (obj2 == null || !(obj2.ToString() != ""))
						{
							throw new CompanyException("Item type is not selected for the product:" + text4);
						}
						itemTypes = (ItemTypes)byte.Parse(obj2.ToString());
						decimal num2 = default(decimal);
						num2 = ((itemTypes == ItemTypes.ConsignmentItem) ? default(decimal) : Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text4, text, text2, rowIndex, sqlTransaction)));
						if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
						{
							string text6 = text5;
							if (hashtable.ContainsKey(text6))
							{
								num = decimal.Parse(hashtable[text6].ToString());
								num += Math.Round(num2, currencyDecimalPoints);
								hashtable[text6] = num;
							}
							else
							{
								hashtable.Add(text6, Math.Round(num2, currencyDecimalPoints));
								arrayList.Add(text6);
							}
							d += Math.Round(num2, currencyDecimalPoints);
						}
					}
				}
				if (d != 0m)
				{
					for (int i = 0; i < hashtable.Count; i++)
					{
						DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						string text6 = arrayList[i].ToString();
						num = decimal.Parse(hashtable[text6].ToString());
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = text6;
						dataRow4["PayeeID"] = value;
						dataRow4["Debit"] = num;
						dataRow4["Credit"] = DBNull.Value;
						dataRow4["IsBaseOnly"] = true;
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4["JVEntryType"] = (byte)1;
						dataRow4["CompanyID"] = value2;
						dataRow4["DivisionID"] = value3;
						dataRow4.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow4);
					}
				}
				if (d != 0m)
				{
					if (text3 == "")
					{
						throw new CompanyException("Inventory on delivery account is not set.");
					}
					DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text3;
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = num;
					dataRow4["IsBaseOnly"] = true;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["JVEntryType"] = (byte)1;
					dataRow4["CompanyID"] = value2;
					dataRow4["DivisionID"] = value3;
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

		public DeliveryReturnData GetDeliveryReturnByID(string sysDocID, string voucherID)
		{
			try
			{
				DeliveryReturnData deliveryReturnData = new DeliveryReturnData();
				string textCommand = "SELECT * FROM Delivery_Return WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(deliveryReturnData, "Delivery_Return", textCommand);
				if (deliveryReturnData == null || deliveryReturnData.Tables.Count == 0 || deliveryReturnData.Tables["Delivery_Return"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,\r\n                        CASE WHEN ItemType = 5 THEN 'True' ELSE IsTrackLot END  AS IsTrackLot,IsTrackSerial,ItemType\r\n                        FROM Delivery_Return_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(deliveryReturnData, "Delivery_Return_Detail", textCommand);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(deliveryReturnData, "Product_Lot_Receiving_Detail", textCommand);
				return deliveryReturnData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteDeliveryReturnDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				DeliveryReturnData deliveryReturnData = new DeliveryReturnData();
				string textCommand = "SELECT SOD.*,dNoteSysDocID,dNoteVoucherID,ISVOID,IsExport FROM Delivery_Return_Detail SOD INNER JOIN Delivery_Return SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(deliveryReturnData, "Delivery_Return_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(deliveryReturnData.DeliveryReturnDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					string text = deliveryReturnData.DeliveryReturnDetailTable.Rows[0]["DNoteSysDocID"].ToString();
					string text2 = deliveryReturnData.DeliveryReturnDetailTable.Rows[0]["DNoteVoucherID"].ToString();
					bool result2 = false;
					bool.TryParse(deliveryReturnData.DeliveryReturnDetailTable.Rows[0]["IsExport"].ToString(), out result2);
					string text3 = "";
					text3 = ((!result2) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
					SalesFlows salesFlows = SalesFlows.DirectInvoice;
					if (text3 != "")
					{
						salesFlows = (SalesFlows)int.Parse(text3.ToString());
					}
					if (salesFlows == SalesFlows.SOThenDNThenInvoice)
					{
						flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(29, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
					}
					foreach (DataRow row in deliveryReturnData.DeliveryReturnDetailTable.Rows)
					{
						string exp = "UPDATE Delivery_Note_Detail SET QuantityReturned = ISNULL(QuantityReturned,2) - " + row["Quantity"].ToString() + " WHERE \r\n                                   SysDocID='" + text + "' AND VoucherID='" + text2 + "' AND RowIndex=" + row["DNRowIndex"].ToString();
						flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
					}
				}
				textCommand = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Delivery_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidDeliveryReturn(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidDeliveryReturn(sysDocID, voucherID, isVoid, null);
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

		public bool CanEdit(string sysDocID, string voucherID)
		{
			return CanEdit(sysDocID, voucherID, null);
		}

		public bool CanEdit(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			if (!new InventoryTransaction(base.DBConfig).AllowDeleteInventoryTransaction(sysDocID, voucherID, sqlTransaction))
			{
				return false;
			}
			return true;
		}

		public bool VoidDeliveryReturn(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				if (!CanEdit(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("This transaction cannot be modifed because some of items are refered by other transactions.");
				}
				bool flag2 = false;
				string text = "SELECT ISNULL(SalesFlow,1)  FROM Delivery_Return\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Delivery_Return", "SalesFlow", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					salesFlows = (SalesFlows)int.Parse(fieldValue.ToString());
				}
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					flag2 = true;
				}
				text = "UPDATE Delivery_Return SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				DeliveryReturnData deliveryReturnData = new DeliveryReturnData();
				text = "SELECT DRD.*,dNoteSysDocID,dNoteVoucherID FROM Delivery_Return_Detail DRD INNER JOIN Delivery_Return DR ON DRD.SysDocID=DR.SysDocID AND DRD.VoucherID=DR.VoucherID\r\n                              WHERE DRD.SysDocID = '" + sysDocID + "' AND DRD.VoucherID = '" + voucherID + "'";
				FillDataSet(deliveryReturnData, "Delivery_Return_Detail", text, sqlTransaction);
				string text2 = deliveryReturnData.DeliveryReturnDetailTable.Rows[0]["DNoteSysDocID"].ToString();
				string text3 = deliveryReturnData.DeliveryReturnDetailTable.Rows[0]["DNoteVoucherID"].ToString();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Delivery_Note", "IsInvoiced", "SysDocID", text2, "VoucherID", text3, sqlTransaction).ToString(), out result);
				if (result)
				{
					flag = false;
					throw new CompanyException("This delivery note is already invoiced and cannot be returned. Use the sales return instead.", 1011);
				}
				if (flag2)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(29, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
					foreach (DataRow row in deliveryReturnData.DeliveryReturnDetailTable.Rows)
					{
						text = "UPDATE Delivery_Note_Detail SET QuantityReturned = ISNULL(QuantityReturned,2) - " + row["Quantity"].ToString() + " WHERE \r\n                                   SysDocID='" + text2 + "' AND VoucherID='" + text3 + "' AND RowIndex=" + row["DNRowIndex"].ToString();
						flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
					}
				}
				text = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(text, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Delivery Return", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteDeliveryReturn(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Delivery_Return", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidDeliveryReturn(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteDeliveryReturnDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Delivery_Return WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Delivery Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetUninvoicedDeliveryReturns(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer] FROM Delivery_Return DN\r\n                             INNER JOIN Customer C ON DN.CustomerID=C.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False'";
				if (customerID != "")
				{
					text = text + " AND DN.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Delivery_Return", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDeliveryReturnToPrint(string sysDocID, string voucherID)
		{
			return GetDeliveryReturnToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetDeliveryReturnToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SI.SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,CA.Email,CA.ContactName,TransactionDate,SD.LocationID SYSLOCID, LocationName AS SYSLocationName,\r\n                                SI.SalesPersonID,SP.FullName,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,SI.Reference,Discount AS Discount,CA.Phone1,CA.Mobile,CA.Fax,CA.ContactName,\r\n                                Total AS Total,SI.PONumber, SI.Note, D.Drivername,D.Note AS [Driver No.],\r\n                                (SELECT TOP 1 RegistrationNumber FROM Vehicle WHERE VehicleID = SI.VehicleID) RegistrationNumber, SI.Reference2,\r\n\r\n                                (SELECT  DISTINCT TOP 1 SO.TransactionDate FROM Sales_Order_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON DND.SourceSysDocID=SND.SysDocID AND DND.SourceVoucherID=SND.VoucherID\r\n                                LEFT OUTER JOIN  Sales_Order SO ON DND.SourceSysDocID=SO.SysDocID AND DND.SourceVoucherID=SO.VoucherID\r\n                                WHERE DND.SysDocID=SI.DNoteSysDocID AND DND.VoucherID=SI.DNoteVoucherID) AS [SalesOrderDate],\r\n\t\t\t\t\t\t\t    (select DN.PONumber from Delivery_Note DN where SysDocID=SI.DNoteSysDocID AND VoucherID=SI.DNoteVoucherID) AS PONumber,\r\n                                (select DN.PODate from Delivery_Note DN where SysDocID=SI.DNoteSysDocID AND VoucherID=SI.DNoteVoucherID) AS PODate,\r\n\t\t\t\t\t\t\t\t(select DN.VehicleID from Delivery_Note DN where SysDocID=SI.DNoteSysDocID AND VoucherID=SI.DNoteVoucherID) AS Vehicle,\r\n                                SI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy,V.VehicleName,V.VehicleID,J.JobName,JC.CostCategoryName,SI.VehicleID AS OutVehicle,Customer.TaxIDNumber\r\n                                FROM  Delivery_Return SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT OUTER JOIN Driver D ON D.DriverID=SI.DriverID\r\n                                LEFT OUTER JOIN Vehicle V ON V.VehicleID=SI.VehicleID\r\n                                LEFT JOIN Job J ON J.JobID=SI.JobID\r\n                                LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SI.CostCategoryID\r\n                            \r\n                                LEFT JOIN Salesperson SP ON SI.SalespersonID=SP.SalespersonID\r\n                                LEFT JOIN System_Document SD ON SD.SysDocID=SI.SysDocID\r\n                                LEFT JOIN Location ON SD.LocationID=Location.LocationID\r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Delivery_Return", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Delivery_Return"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,DND.ProductID,DND.Description,LocationID,ISNULL(UnitQuantity,DND.Quantity) AS Quantity, Product.BrandID,Product.Size,\r\n                        UnitPrice AS UnitPrice,\r\n                        ISNULL(UnitQuantity,DND.Quantity)*UnitPrice AS Total,DND.UnitID,C.CountryName,\r\n                        PC.CategoryName,Product.Description2,PB.BrandName,DND.Remarks\r\n                        FROM   Delivery_Return_Detail DND\r\n\r\n                        INNER JOIN Product ON DND.ProductID = Product.ProductID\r\n                        LEFT JOIN Product_Brand PB ON Product.BrandID=PB.BrandID\r\n                        LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=Product.CategoryID\r\n                        LEFT OUTER JOIN Country C ON Product.Origin=C.CountryID \r\n\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Delivery_Return_Detail", cmdText);
				dataSet.Relations.Add("CustomerDeliveryReturn", new DataColumn[2]
				{
					dataSet.Tables["Delivery_Return"].Columns["SysDocID"],
					dataSet.Tables["Delivery_Return"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Delivery_Return_Detail"].Columns["SysDocID"],
					dataSet.Tables["Delivery_Return_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Delivery_Return"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Delivery_Return"].Rows)
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
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS V,  SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Voucher Date],\r\n                            INV.SalespersonID [Salesperson],Total [Amount],J.JobID,J.JobName, INV.Reference, INV.Reference2\r\n                            FROM   Delivery_Return INV\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Delivery_Return", sqlCommand);
			return dataSet;
		}
	}
}
