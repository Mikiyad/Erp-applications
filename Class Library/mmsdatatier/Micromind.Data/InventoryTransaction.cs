using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace Micromind.Data
{
	public sealed class InventoryTransaction : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string EQWORKORDERID_PARM = "@EqWorkOrderID";

		private const string UNITID_PARM = "@UnitID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string QUANTITY_PARM = "@Quantity";

		private const string FOCQUANTITY_PARM = "@FOCQuantity";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string BINID_PARM = "@BinID";

		private const string RACKID_PARM = "@RackID";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string COST_PARM = "@Cost";

		private const string SYSDOCTYPE_PARM = "@SysDocType";

		private const string LOTNUMBER_PARM = "@LotNumber";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DISCOUNT_PARM = "@Discount";

		private const string AVERAGECOST_PARM = "@AverageCost";

		private const string ASSETVALUE_PARM = "@AssetValue";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string TRANSACTIONTYPE_PARM = "@TransactionType";

		private const string ISNONCOSTEDGRN_PARM = "@IsNonCostedGRN";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string ISRECOST_PARM = "@IsRecost";

		private const string REFSYSDOCID_PARM = "@RefSysDocID";

		private const string REFVOUCHERID_PARM = "@RefVoucherID";

		private const string REFROWINDEX_PARM = "@RefRowIndex";

		private const string REFTRANSACTIONID_PARM = "@RefTransactionID";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string INVENTORYTRANSACTIONS_TABLE = "Inventory_Transactions";

		public InventoryTransaction(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateInventoryTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Transactions", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("LocationID", "@LocationID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("EqWorkOrderID", "@EqWorkOrderID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("UnitID", "@UnitID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("Quantity", "@Quantity"), new FieldValue("FOCQuantity", "@FOCQuantity"), new FieldValue("Factor", "@Factor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("Cost", "@Cost"), new FieldValue("TransactionType", "@TransactionType"), new FieldValue("SysDocType", "@SysDocType"), new FieldValue("LotNumber", "@LotNumber"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Discount", "@Discount"), new FieldValue("IsNonCostedGRN", "@IsNonCostedGRN"), new FieldValue("AverageCost", "@AverageCost"), new FieldValue("AssetValue", "@AssetValue"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("IsRecost", "@IsRecost"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("RefSysDocID", "@RefSysDocID"), new FieldValue("RefVoucherID", "@RefVoucherID"), new FieldValue("RefRowIndex", "@RefRowIndex"), new FieldValue("RefTransactionID", "@RefTransactionID"), new FieldValue("PayeeID", "@PayeeID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Inventory_Transactions", new FieldValue("CreatedBy", "@CreatedBy"), new FieldValue("DateCreated", "@DateCreated"));
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryTransactionCommand(bool isUpdate)
		{
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetInsertUpdateInventoryTransactionText(isUpdate), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@EqWorkOrderID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@FOCQuantity", SqlDbType.Real);
			parameters.Add("@Factor", SqlDbType.Real);
			parameters.Add("@FactorType", SqlDbType.Char);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@SysDocType", SqlDbType.NVarChar);
			parameters.Add("@LotNumber", SqlDbType.NVarChar);
			parameters.Add("@TransactionType", SqlDbType.TinyInt);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Money);
			parameters.Add("@Discount", SqlDbType.Money);
			parameters.Add("@IsNonCostedGRN", SqlDbType.Bit);
			parameters.Add("@AverageCost", SqlDbType.Money);
			parameters.Add("@AssetValue", SqlDbType.Money);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@RefSysDocID", SqlDbType.NVarChar);
			parameters.Add("@RefVoucherID", SqlDbType.NVarChar);
			parameters.Add("@RefRowIndex", SqlDbType.Int);
			parameters.Add("@RefTransactionID", SqlDbType.Int);
			parameters.Add("@IsRecost", SqlDbType.Bit);
			if (isUpdate)
			{
				parameters.Add("@CreatedBy", SqlDbType.NVarChar);
				parameters.Add("@DateCreated", SqlDbType.DateTime);
			}
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@EqWorkOrderID"].SourceColumn = "EqWorkOrderID";
			parameters["@TransactionType"].SourceColumn = "TransactionType";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@FOCQuantity"].SourceColumn = "FOCQuantity";
			parameters["@Factor"].SourceColumn = "Factor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@SysDocType"].SourceColumn = "SysDocType";
			parameters["@LotNumber"].SourceColumn = "LotNumber";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@IsNonCostedGRN"].SourceColumn = "IsNonCostedGRN";
			parameters["@AverageCost"].SourceColumn = "AverageCost";
			parameters["@AssetValue"].SourceColumn = "AssetValue";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@IsRecost"].SourceColumn = "IsRecost";
			parameters["@RefSysDocID"].SourceColumn = "RefSysDocID";
			parameters["@RefVoucherID"].SourceColumn = "RefVoucherID";
			parameters["@RefRowIndex"].SourceColumn = "RefRowIndex";
			parameters["@RefTransactionID"].SourceColumn = "RefTransactionID";
			if (isUpdate)
			{
				parameters["@CreatedBy"].SourceColumn = "CreatedBy";
				parameters["@DateCreated"].SourceColumn = "DateCreated";
			}
			return insertCommand;
		}

		private bool ValidateData(InventoryTransactionData journalData)
		{
			return true;
		}

		public bool InsertUpdateInventoryTransaction(InventoryTransactionData inventoryTransactionData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				bool flag = true;
				DataRow dataRow = inventoryTransactionData.InventoryTransactionTable.Rows[0];
				if (sqlTransaction == null)
				{
					throw new Exception("SQLTransaction cannot be null");
				}
				InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)byte.Parse(dataRow["TransactionType"].ToString());
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				int num = int.Parse(dataRow["SysDocType"].ToString());
				if (isUpdate && (inventoryTransactionTypes == InventoryTransactionTypes.Purchase || inventoryTransactionTypes == InventoryTransactionTypes.Adjustment || inventoryTransactionTypes == InventoryTransactionTypes.Assembly || inventoryTransactionTypes == InventoryTransactionTypes.GarmentRental || inventoryTransactionTypes == InventoryTransactionTypes.ConsignOut || inventoryTransactionTypes == InventoryTransactionTypes.ConsignIn || inventoryTransactionTypes == InventoryTransactionTypes.W3PLGRN || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryReturn || inventoryTransactionTypes == InventoryTransactionTypes.CustomerCreditMemo || inventoryTransactionTypes == InventoryTransactionTypes.Transfer || inventoryTransactionTypes == InventoryTransactionTypes.Sale || inventoryTransactionTypes == InventoryTransactionTypes.InventoryDismantle))
				{
					string text3 = "";
					text3 = "UPDATE Product_Lot SET IsDeleted='True' WHERE ReceiptNumber='" + dataRow["VoucherID"].ToString() + "' AND\r\n\t\t\t\t\t\t\t DocID = '" + dataRow["SysDocID"].ToString() + "'";
					flag &= Update(text3, sqlTransaction);
				}
				if (inventoryTransactionTypes == InventoryTransactionTypes.Purchase || inventoryTransactionTypes == InventoryTransactionTypes.Adjustment || inventoryTransactionTypes == InventoryTransactionTypes.Assembly || inventoryTransactionTypes == InventoryTransactionTypes.Sale || inventoryTransactionTypes == InventoryTransactionTypes.VendorCreditMemo || inventoryTransactionTypes == InventoryTransactionTypes.CustomerCreditMemo || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryReturn || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryIssue || inventoryTransactionTypes == InventoryTransactionTypes.ConsignOutSettlement || inventoryTransactionTypes == InventoryTransactionTypes.ConsignOut || inventoryTransactionTypes == InventoryTransactionTypes.ConsignOutReturn || inventoryTransactionTypes == InventoryTransactionTypes.ConsignIn || inventoryTransactionTypes == InventoryTransactionTypes.W3PLDelivery || inventoryTransactionTypes == InventoryTransactionTypes.W3PLGRN || inventoryTransactionTypes == InventoryTransactionTypes.ConsignInReturn || inventoryTransactionTypes == InventoryTransactionTypes.OpeningInventory || inventoryTransactionTypes == InventoryTransactionTypes.Transfer || inventoryTransactionTypes == InventoryTransactionTypes.GarmentRental || inventoryTransactionTypes == InventoryTransactionTypes.GarmentRentalReturn || inventoryTransactionTypes == InventoryTransactionTypes.InventoryDismantle)
				{
					AllocateInvoiceToLot(inventoryTransactionData, sqlTransaction);
				}
				if (isUpdate || inventoryTransactionTypes == InventoryTransactionTypes.VendorCreditMemo)
				{
					flag &= new InventoryTransaction(base.DBConfig).RemoveOldLotAllocations(text2, text, sqlTransaction);
				}
				flag &= InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, isDirect: false, sqlTransaction);
				SysDocTypes sysDocTypes = (SysDocTypes)num;
				if (sysDocTypes == SysDocTypes.AssemblyBuild || sysDocTypes == SysDocTypes.InventoryRepacking || sysDocTypes == SysDocTypes.DeliveryReturn || sysDocTypes == SysDocTypes.CashSalesReturn || sysDocTypes == SysDocTypes.CreditSalesReturn)
				{
					string exp = "   UPDATE PL SET PL.Cost = IT.UnitPrice,PL.AvgCost = IT.AverageCost\r\n                                    FROM Product_Lot PL INNER JOIN Inventory_Transactions IT ON IT.SysDocID = PL.DocID AND IT.VoucherID = PL.ReceiptNumber AND IT.RowIndex = PL.RowIndex\r\n                                    INNER JOIN Product P ON P.ProductID = PL.ItemCode\r\n                                    WHERE P.CostMethod = 1 AND PL.DocID  = '" + text2 + "' AND PL.ReceiptNumber = '" + text + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		private bool CreateNewLots(InventoryTransactionData transactionData, DataRow transactionRow, DataRow productRow, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			string text = productRow["ProductID"].ToString();
			CostMethods costMethod = new Products(base.DBConfig).GetCostMethod(text, sqlTransaction);
			string text2 = transactionRow["SysDocID"].ToString();
			string text3 = transactionRow["VoucherID"].ToString();
			int num = int.Parse(productRow["RowIndex"].ToString());
			ItemTypes itemType = new Products(base.DBConfig).GetItemType(text, sqlTransaction);
			if (itemType != ItemTypes.Inventory && itemType != ItemTypes.Assembly && itemType != ItemTypes.ConsignmentItem && itemType != ItemTypes.Inventory3PL)
			{
				return true;
			}
			bool flag2 = false;
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "IsTrackLot", "ProductID", text, sqlTransaction);
			if (fieldValue != null && fieldValue.ToString() != "")
			{
				flag2 = bool.Parse(fieldValue.ToString());
			}
			SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(transactionRow["SysDocType"].ToString());
			decimal avgCost = new Products(base.DBConfig).GetAverageCost(productRow["ProductID"].ToString(), sqlTransaction);
			if (productRow["UnitPrice"] == DBNull.Value)
			{
				productRow["UnitPrice"] = 0;
			}
			decimal d = decimal.Parse(productRow["Quantity"].ToString());
			if (flag2 || itemType == ItemTypes.ConsignmentItem || itemType == ItemTypes.Inventory3PL)
			{
				DataRow[] array = transactionData.ProductLotReceivingDetail.Select("ProductID = '" + text + "' AND RowIndex = " + num);
				string text4 = "";
				fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
				if (fieldValue != null)
				{
					text4 = fieldValue.ToString();
				}
				if (text4 != "" && productRow["UnitID"] != DBNull.Value && productRow["UnitID"].ToString() != text4)
				{
					throw new CompanyException("Unit of measurements for the product '" + text + "' does not match");
				}
				decimal d2 = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					d2 += decimal.Parse(array[i]["LotQty"].ToString());
				}
				if (d2 != d)
				{
					throw new CompanyException("Lot allocations are not equal to total quantity.");
				}
				for (int j = 0; j < array.Length; j++)
				{
					decimal.Parse(array[j]["LotQty"].ToString());
					decimal.Parse(productRow["UnitPrice"].ToString());
					_ = DateTime.MinValue;
					_ = DateTime.MinValue;
					array[j]["LocationID"].ToString();
					if (array[j]["ProductionDate"] != DBNull.Value)
					{
						DateTime.Parse(array[j]["ProductionDate"].ToString());
					}
					if (array[j]["ExpiryDate"] != DBNull.Value)
					{
						DateTime.Parse(array[j]["ExpiryDate"].ToString());
					}
					int num2 = -1;
					if (array[j]["SourceLotNumber"] != DBNull.Value && array[j]["SourceLotNumber"].ToString() != "" && int.Parse(array[j]["SourceLotNumber"].ToString()) > 0)
					{
						num2 = int.Parse(array[j]["SourceLotNumber"].ToString());
						num2 = GetBaseSourceLotNumber(num2, sqlTransaction);
						array[j]["SourceLotNumber"] = num2;
					}
					else
					{
						array[j]["SourceLotNumber"] = DBNull.Value;
					}
					string text5 = PrepareLotCreationCommandText(transactionRow, productRow, array[j], avgCost, num2, sqlTransaction);
					new SqlCommand(text5, base.DBConfig.Connection, sqlTransaction);
					int num3 = new Databases(base.DBConfig).ExecuteNonQuery(text5, sqlTransaction);
					flag = (flag && num3 > 0);
				}
			}
			else
			{
				DataSet dataSet = new DataSet();
				int num4 = -1;
				switch (sysDocTypes)
				{
				case SysDocTypes.DeliveryReturn:
				{
					string text5 = "SELECT RecordID,LotNo,SoldQty,PLS.Cost FROM Product_Lot_Sales PLS  \r\n                                INNER JOIN Delivery_Note_Detail DND ON DND.SysDocID = PLS.DocID AND DND.VoucherID = PLS.InvoiceNumber  AND DND.RowIndex = PLS.RowIndex\r\n                                INNER JOIN Delivery_Note DN ON DN.SysDocID = DND.SysDocID AND DN.VoucherID = DND.VoucherID \r\n                                INNER JOIN Delivery_Return DR ON DR.DNoteSysDocID = DN.SysDocID AND DR.DNoteVoucherID = DN.VoucherID\r\n                                WHERE DR.SysDocID = '" + text2 + "' AND DR.VoucherID = '" + text3 + "' AND PLS.ItemCode = '" + text + "' AND PLS.RowIndex = " + num;
					FillDataSet(dataSet, "SourceLot", text5, sqlTransaction);
					break;
				}
				case SysDocTypes.ConsignOut:
				{
					string text5 = "SELECT RecordID,LotNo,SoldQty,PLS.Cost FROM Product_Lot_Sales PLS  \r\n                                INNER JOIN Delivery_Note_Detail DND ON DND.SysDocID = PLS.DocID AND DND.VoucherID = PLS.InvoiceNumber  AND DND.RowIndex = PLS.RowIndex\r\n                                INNER JOIN Consign_Out_Detail CO ON CO.SourceSysDocID = DND.SysDocID AND CO.SourceVoucherID = DND.VoucherID AND CO.SourceRowIndex = DND.RowIndex\r\n                                WHERE CO.SysDocID = '" + text2 + "' AND CO.VoucherID = '" + text3 + "' AND PLS.ItemCode = '" + text + "' AND PLS.RowIndex = " + num;
					FillDataSet(dataSet, "SourceLot", text5, sqlTransaction);
					break;
				}
				case SysDocTypes.ConsignOutReturn:
				{
					string text5 = " SELECT RecordID,LotNo,SoldQty,PLS.Cost FROM Product_Lot_Sales PLS  \r\n                                INNER JOIN Delivery_Note_Detail DND ON DND.SysDocID = PLS.DocID AND DND.VoucherID = PLS.InvoiceNumber  AND DND.RowIndex = PLS.RowIndex\r\n                                INNER JOIN Consign_Out_Detail CO ON CO.SourceSysDocID = DND.SysDocID AND CO.SourceVoucherID = DND.VoucherID AND CO.SourceRowIndex = DND.RowIndex\r\n\t\t\t\t\t\t\t\tINNER JOIN ConsignOut_Return_Detail COR ON COR.ConsignSysDocID = CO.SysDocID AND COR.ConsignVoucherID = CO.VoucherID AND COR.ConsignRowIndex = CO.RowIndex\r\n                                WHERE COR.SysDocID = '" + text2 + "' AND COR.VoucherID = '" + text3 + "' AND PLS.ItemCode = '" + text + "' AND PLS.RowIndex = " + num;
					FillDataSet(dataSet, "SourceLot", text5, sqlTransaction);
					break;
				}
				case SysDocTypes.CreditSalesReturn:
				case SysDocTypes.CashSalesReturn:
				{
					string text5 = "SELECT RecordID,LotNo,SoldQty,PLS.Cost FROM Product_Lot_Sales PLS  \r\n                                INNER JOIN Delivery_Note_Detail DND ON DND.SysDocID = PLS.DocID AND DND.VoucherID = PLS.InvoiceNumber  AND DND.RowIndex = PLS.RowIndex\r\n                                INNER JOIN Delivery_Note DN ON DN.SysDocID = DND.SysDocID AND DN.VoucherID = DND.VoucherID \r\n                                INNER JOIN Sales_Invoice_Detail SID ON SID.OrderSysDocID = DN.SysDocID AND SID.OrderVoucherID = DN.VoucherID \r\n                                INNER JOIN Sales_Return_Detail DR ON DR.SourceSysDocID = SID.SysDocID AND DR.SourceVoucherID = SID.VoucherID\r\n\r\n                                WHERE DR.SysDocID = '" + text2 + "' AND DR.VoucherID = '" + text3 + "' AND PLS.ItemCode = '" + text + "' AND PLS.RowIndex = " + num;
					FillDataSet(dataSet, "SourceLot", text5, sqlTransaction);
					if ((dataSet == null || dataSet.Tables[0].Rows.Count == 0) && costMethod == CostMethods.FIFO)
					{
						dataSet = new DataSet();
						text5 = "SELECT  RecordID,TransactionDate,LotNo,SoldQty- ISNULL(ReturnQty,0) AS SoldQty,Cost\r\n                                    FROM Product_Lot_Sales WHERE ItemCode = '" + text + "'  AND SoldQty-ISNULL(ReturnQty, 0) > 0\r\n                                    ORDER BY TransactionDate DESC";
						FillDataSet(dataSet, "SourceLot", text5, sqlTransaction);
					}
					break;
				}
				case SysDocTypes.TransitTransferIn:
				case SysDocTypes.DirectInventoryTransfer:
				{
					string text5 = "SELECT RecordID,LotNo,SoldQty - ISNULL(ReturnQty,0) AS SoldQty,PLS.Cost FROM Product_Lot_Sales PLS  \r\n                            INNER JOIN Inventory_Transfer_Detail TRD ON TRD.SysDocID = PLS.DocID AND TRD.VoucherID = PLS.InvoiceNumber  AND TRD.RowIndex = PLS.RowIndex\r\n                            INNER JOIN Inventory_Transfer TR ON TR.SysDocID = TRD.SysDocID AND TR.VoucherID = TRD.VoucherID\r\n                            WHERE TR.AcceptSysDocID = '" + text2 + "' AND TR.AcceptVoucherID = '" + text3 + "' AND PLS.ItemCode = '" + text + "' AND PLS.RowIndex = " + num;
					FillDataSet(dataSet, "SourceLot", text5, sqlTransaction);
					break;
				}
				case SysDocTypes.ReturnTransitTransfer:
				{
					string text5 = "SELECT RecordID,LotNo,SoldQty,PLS.Cost FROM Product_Lot_Sales  PLS  with (nolock)\r\n                            INNER JOIN Inventory_Transfer TR  with (nolock) ON TR.SysDocID = PLS.DocID AND TR.VoucherID = PLS.InvoiceNumber\r\n                            INNER JOIN Inventory_Transfer_Detail TRD with (nolock) ON TRD.SysDocID = TR.SysDocID AND TRD.VoucherID = TR.VoucherID  AND TRD.RowIndex = PLS.RowIndex\r\n                            WHERE TR.RejectAcceptSysDocID = '" + text2 + "' AND TR.RejectAcceptVoucherID = '" + text3 + "' AND PLS.ItemCode = '" + text + "' AND PLS.RowIndex = " + num;
					FillDataSet(dataSet, "SourceLot", text5, sqlTransaction);
					break;
				}
				case SysDocTypes.TransitTransferOut:
				{
					string text5 = "SELECT RecordID,LotNo,SoldQty,PLS.Cost FROM Product_Lot_Sales PLS  \r\n                            WHERE PLS.DocID = '" + text2 + "' AND PLS.InvoiceNumber = '" + text3 + "' AND PLS.ItemCode = '" + text + "' AND PLS.RowIndex =" + num;
					FillDataSet(dataSet, "SourceLot", text5, sqlTransaction);
					break;
				}
				}
				decimal num5 = decimal.Parse(productRow["Quantity"].ToString());
				if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables["SourceLot"].Rows.Count > 0)
				{
					foreach (DataRow row in dataSet.Tables["SourceLot"].Rows)
					{
						if (num5 == 0m)
						{
							break;
						}
						decimal num6 = decimal.Parse(row["SoldQty"].ToString());
						decimal num7 = decimal.Parse(row["Cost"].ToString());
						if (row["LotNo"] != DBNull.Value && row["LotNo"].ToString() != "")
						{
							num4 = int.Parse(row["LotNo"].ToString());
						}
						if (costMethod == CostMethods.FIFO)
						{
							avgCost = num7;
						}
						if (sysDocTypes == SysDocTypes.CashSalesReturn || sysDocTypes == SysDocTypes.CreditSalesReturn || sysDocTypes == SysDocTypes.DeliveryReturn || sysDocTypes == SysDocTypes.TransitTransferIn || sysDocTypes == SysDocTypes.TransitTransferOut || sysDocTypes == SysDocTypes.ReturnTransitTransfer || sysDocTypes == SysDocTypes.DirectInventoryTransfer)
						{
							productRow["UnitPrice"] = num7;
						}
						num4 = GetBaseSourceLotNumber(num4, sqlTransaction);
						int sourceRecordID = int.Parse(row["RecordID"].ToString());
						string text5;
						if (num6 >= num5)
						{
							text5 = ((costMethod != CostMethods.FIFO) ? PrepareNonTrackingLotCreationCommandText(transactionRow, productRow, sysDocTypes, num5, avgCost, num4, sourceRecordID, sqlTransaction) : PrepareNonTrackingLotCreationCommandText(transactionRow, productRow, sysDocTypes, num5, num7, num4, sourceRecordID, sqlTransaction));
							num5 = default(decimal);
						}
						else
						{
							text5 = ((costMethod != CostMethods.FIFO) ? PrepareNonTrackingLotCreationCommandText(transactionRow, productRow, sysDocTypes, num6, avgCost, num4, sourceRecordID, sqlTransaction) : PrepareNonTrackingLotCreationCommandText(transactionRow, productRow, sysDocTypes, num6, num7, num4, sourceRecordID, sqlTransaction));
							num5 -= num6;
						}
						new SqlCommand(text5, base.DBConfig.Connection, sqlTransaction);
						int num8 = ExecuteNonQuery(text5, sqlTransaction);
						flag = (flag && num8 > 0);
					}
					if (num5 > 0m)
					{
						string text5 = PrepareNonTrackingLotCreationCommandText(transactionRow, productRow, sysDocTypes, num5, avgCost, -1, -1, sqlTransaction);
						new SqlCommand(text5, base.DBConfig.Connection, sqlTransaction);
						int num9 = ExecuteNonQuery(text5, sqlTransaction);
						flag = (flag && num9 > 0);
					}
				}
				else
				{
					string text5 = PrepareNonTrackingLotCreationCommandText(transactionRow, productRow, sysDocTypes, num5, avgCost, num4, -1, sqlTransaction);
					new SqlCommand(text5, base.DBConfig.Connection, sqlTransaction);
					int num10 = ExecuteNonQuery(text5, sqlTransaction);
					flag = (flag && num10 > 0);
				}
			}
			if (costMethod == CostMethods.Average)
			{
				string text5 = "UPDATE Product_Lot SET AvgCost=" + avgCost.ToString() + " WHERE ItemCode='" + productRow["ProductID"].ToString() + "'\r\n\t\t\t\t\t\t  AND (LotQty- ISNULL(SoldQty,0) - ISNULL(ReturnedQty,0))>0";
				flag &= (ExecuteNonQuery(text5, sqlTransaction) >= 0);
			}
			return flag;
		}

		private int GetBaseSourceLotNumber(int lotNumber, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT BaseLotNumber = \r\n\t\t\t\t\t\t    CASE WHEN PL.SourceLotNumber IS NULL THEN PL.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL2.SourceLotNumber IS NULL THEN PL2.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL3.SourceLotNumber IS NULL THEN PL3.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL4.SourceLotNumber IS NULL THEN PL4.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL5.SourceLotNumber IS NULL THEN PL5.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL6.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL7.SourceLotNumber IS NULL THEN PL7.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL8.SourceLotNumber IS NULL THEN PL8.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL9.LotNumber ELSE \r\n                            PL9.SourceLotNumber END \r\n                            FROM Product_LOT PL9 WHERE PL9.LotNumber = PL8.SourceLotNumber) END \r\n                            FROM Product_LOT PL8 WHERE PL8.LotNumber = PL7.SourceLotNumber) END \r\n                            FROM Product_LOT PL7 WHERE PL7.LotNumber = PL6.SourceLotNumber) END \r\n                            FROM Product_LOT PL6 WHERE PL6.LotNumber = PL5.SourceLotNumber) END \r\n                            FROM Product_LOT PL5 WHERE PL5.LotNumber = PL4.SourceLotNumber) END \r\n                            FROM Product_LOT PL4 WHERE PL4.LotNumber = PL3.SourceLotNumber) END \r\n                            FROM Product_LOT PL3 WHERE PL3.LotNumber = PL2.SourceLotNumber) END \r\n                            FROM Product_LOT PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END\r\n                            FROM Product_Lot PL  WHERE PL.LotNumber = " + lotNumber.ToString();
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return int.Parse(obj.ToString());
				}
				return lotNumber;
			}
			catch
			{
				throw;
			}
		}

		private string PrepareNonTrackingLotCreationCommandText(DataRow transactionRow, DataRow productRow, SysDocTypes sysDocType, decimal quantity, decimal avgCost, int sourceLotNumber, int sourceRecordID, SqlTransaction sqlTransaction)
		{
			int num = new InventoryTransaction(base.DBConfig).CreateLotNumber(sqlTransaction);
			int num2 = int.Parse(productRow["RowIndex"].ToString());
			string str = "INSERT INTO [Product_Lot] (ItemCode,LotNumber,Cost,AvgCost,LotQty,SoldQty,DocID,ReceiptNumber,RowIndex,LocationID,ReceiptDate,SupplierCode,SourceLotNumber,SourceRecordID) VALUES('" + productRow["ProductID"].ToString() + "'," + num + "," + productRow["UnitPrice"].ToString() + "," + avgCost.ToString() + "," + quantity.ToString() + "," + 0 + ",'" + productRow["SysDocID"].ToString() + "','" + productRow["VoucherID"].ToString() + "'," + num2 + ",'" + productRow["LocationID"].ToString() + "','";
			str = ((sysDocType != SysDocTypes.OpeningInventory) ? (str + StoreConfiguration.ToSqlDateTimeString(DateTime.Parse(transactionRow["TransactionDate"].ToString()))) : (str + StoreConfiguration.ToSqlDateTimeString(DateTime.Parse(productRow["TransactionDate"].ToString()))));
			str = str + "','" + productRow["PayeeID"].ToString() + "' ";
			str = ((sourceLotNumber != -1) ? (str + ", " + sourceLotNumber) : (str + ", NULL "));
			if (sourceRecordID == -1)
			{
				return str + ", NULL )";
			}
			return str + ", " + sourceRecordID + ")";
		}

		private string PrepareLotCreationCommandText(DataRow transactionRow, DataRow productRow, DataRow lotRow, decimal avgCost, int sourceLotNumber, SqlTransaction sqlTransaction)
		{
			string text = "";
			DateTime dateTime = DateTime.MinValue;
			DateTime dateTime2 = DateTime.MinValue;
			int num = int.Parse(productRow["RowIndex"].ToString());
			int num2 = new InventoryTransaction(base.DBConfig).CreateLotNumber(sqlTransaction);
			decimal num3 = decimal.Parse(productRow["UnitPrice"].ToString());
			DateTime date = DateTime.Parse(transactionRow["TransactionDate"].ToString());
			if (sourceLotNumber > 0)
			{
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product_Lot", "SELECT Cost,ReceiptDate,ProductionDate,ExpiryDate,BinID,Reference2 FROM Product_Lot WHERE LotNumber = " + sourceLotNumber, sqlTransaction);
				if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					num3 = decimal.Parse(dataSet.Tables[0].Rows[0]["Cost"].ToString());
					date = DateTime.Parse(dataSet.Tables[0].Rows[0]["ReceiptDate"].ToString());
					if (dataSet.Tables[0].Rows[0]["ExpiryDate"] != DBNull.Value)
					{
						dateTime2 = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["ExpiryDate"].ToString());
					}
					if (dataSet.Tables[0].Rows[0]["ProductionDate"] != DBNull.Value)
					{
						dateTime = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["ProductionDate"].ToString());
					}
					dataSet.Tables[0].Rows[0]["BinID"].ToString();
					dataSet.Tables[0].Rows[0]["Reference2"].ToString();
				}
				else if (lotRow["ReceiptDate"] != DBNull.Value)
				{
					date = DateTime.Parse(lotRow["ReceiptDate"].ToString());
				}
			}
			else
			{
				if (lotRow["ProductionDate"] != DBNull.Value)
				{
					dateTime = DateTime.Parse(lotRow["ProductionDate"].ToString());
				}
				if (lotRow["ExpiryDate"] != DBNull.Value)
				{
					dateTime2 = DateTime.Parse(lotRow["ExpiryDate"].ToString());
				}
				lotRow["BinID"].ToString();
				lotRow["Reference2"].ToString();
				lotRow["RackID"].ToString();
			}
			text = "INSERT INTO [Product_Lot] (ItemCode,LotNumber,Reference,SourceLotNumber,Cost,AvgCost,LotQty,SoldQty,ProductionDate,ExpiryDate,DocID,ReceiptNumber,RowIndex,LocationID,ReceiptDate,BinID,Reference2,SupplierCode,RackID) VALUES('" + productRow["ProductID"].ToString() + "'," + num2 + ",'" + lotRow["LotNumber"].ToString() + "', ";
			text = ((lotRow["SourceLotNumber"] == DBNull.Value) ? (text + " NULL ") : (text + lotRow["SourceLotNumber"]));
			text = text + "," + num3 + "," + avgCost.ToString() + "," + lotRow["LotQty"].ToString() + "," + 0 + ",";
			text = ((!(dateTime == DateTime.MinValue)) ? (text + "'" + StoreConfiguration.ToSqlDateTimeString(dateTime) + "',") : (text + "NULL,"));
			text = ((!(dateTime2 == DateTime.MinValue)) ? (text + "'" + StoreConfiguration.ToSqlDateTimeString(dateTime2) + "'") : (text + "NULL"));
			return text + ",'" + productRow["SysDocID"].ToString() + "','" + productRow["VoucherID"].ToString() + "'," + num + ",'" + productRow["LocationID"].ToString() + "','" + StoreConfiguration.ToSqlDateTimeString(date) + "','" + lotRow["BinID"].ToString() + "','" + lotRow["Reference2"].ToString() + "','" + productRow["PayeeID"].ToString() + "','" + lotRow["RackID"].ToString() + "')";
		}

		public bool AllocateInvoiceToLot(InventoryTransactionData inventoryTransactionData, SqlTransaction sqlTransaction)
		{
			string text = "";
			bool flag = true;
			DataRow dataRow = inventoryTransactionData.InventoryTransactionTable.Rows[0];
			InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)byte.Parse(dataRow["TransactionType"].ToString());
			DataSet lotData = new DataSet();
			DateTime dateTime = DateTime.Parse(dataRow["TransactionDate"].ToString());
			StoreConfiguration.ToSqlDateTimeString(dateTime);
			string commaSeperatedIDs = GetCommaSeperatedIDs(inventoryTransactionData, "Inventory_Transactions", "ProductID");
			text = " SELECT DISTINCT ProductID,ItemTYpe,IsTrackLot,IsTrackSerial,CostMethod FROM Product WHERE ProductID IN (" + commaSeperatedIDs + ") ";
			FillDataSet(lotData, "Product", text, sqlTransaction);
			ArrayList arrayList = new ArrayList();
			foreach (DataRow row in lotData.Tables[0].Rows)
			{
				arrayList.Add(row["ProductID"].ToString());
			}
			new Databases(base.DBConfig).BulkCopy(arrayList, "#TMP_Item", "ProductID", sqlTransaction);
			if (inventoryTransactionTypes == InventoryTransactionTypes.CustomerCreditMemo)
			{
				text = "Select DISTINCT LotNo AS LotNumber,SUM(PLS.SoldQty) AS SoldQty,PLS.ItemCode,ReceiptDate,ReceiptNumber,PLS.LocationID,PL.Cost,AvgCost, \r\n                            SUM(PLS.SoldQty) AS LotQty,PL.BinID,PL.Reference2 FROM Product_Lot_Sales PLS \r\n                            INNER JOIN Product_Lot PL ON PL.LotNumber = PLS.LotNo\r\n                            WHERE PLS.ItemCode IN (SELECT ProductID FROM #TMP_Item) and \r\n                             ISNULL(IsDeleted,'False') = 'False'\r\n                             GROUP BY LotNo,PLS.ItemCode,ReceiptDate,ReceiptNumber,PLS.LocationID,PL.Cost,AvgCost,PL.BinID,PL.Reference2\r\n                            Order BY ReceiptDate,ReceiptNumber DESC  \r\n                            DROP TABLE #TMP_Item";
				FillDataSet(lotData, "Product_Lot", text, sqlTransaction);
			}
			else
			{
				text = "Select LotNumber,SoldQty,ItemCode,ReceiptDate,ReceiptNumber,LocationID,Cost,AvgCost, (LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0) AS LotQty,BinID,Reference2 FROM product_lot\r\n\t\t\t\t\t\t\t\tWHERE ItemCode IN (SELECT ProductID FROM #TMP_Item) and  \r\n\t\t\t\t\t\t\t\t(LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0)>0 and ISNULL(IsDeleted,'False') = 'False'\r\n\t\t\t\t\t\t\t\tOrder BY ReceiptDate,ReceiptNumber DESC \r\n                                DROP TABLE #TMP_Item ";
				FillDataSet(lotData, "Product_Lot", text, sqlTransaction);
			}
			string supplierCode = dataRow["PayeeID"].ToString();
			foreach (DataRow row2 in inventoryTransactionData.InventoryTransactionTable.Rows)
			{
				decimal d = decimal.Parse(row2["Quantity"].ToString());
				int costMethod = 1;
				int itemType = 1;
				bool result = false;
				bool result2 = false;
				string text2 = row2["ProductID"].ToString();
				string locationID = row2["LocationID"].ToString();
				DataRow[] array = lotData.Tables["Product"].Select("ProductID = '" + text2 + "' ");
				if (array != null && array.Length != 0)
				{
					costMethod = int.Parse(array[0]["CostMethod"].ToString());
					itemType = int.Parse(array[0]["ItemType"].ToString());
					bool.TryParse(array[0]["IsTrackLot"].ToString(), out result);
					bool.TryParse(array[0]["IsTrackSerial"].ToString(), out result2);
				}
				if (d > 0m)
				{
					flag &= CreateNewLots(inventoryTransactionData, dataRow, row2, sqlTransaction);
				}
				else
				{
					string sysDocID = row2["SysDocID"].ToString();
					string voucherID = row2["VoucherID"].ToString();
					int rowIndex = int.Parse(row2["RowIndex"].ToString());
					decimal price = default(decimal);
					if (row2["UnitPrice"] != DBNull.Value)
					{
						price = decimal.Parse(row2["UnitPrice"].ToString());
					}
					decimal quantity = default(decimal);
					if (row2["Quantity"] != DBNull.Value)
					{
						quantity = decimal.Parse(row2["Quantity"].ToString());
					}
					flag &= AllocateProductLineToLot(inventoryTransactionData, ref lotData, dateTime, sysDocID, voucherID, text2, locationID, rowIndex, quantity, price, itemType, costMethod, supplierCode, inventoryTransactionTypes, sqlTransaction);
				}
			}
			return flag;
		}

		private bool AllocateProductLineToLot(InventoryTransactionData transactionData, ref DataSet lotData, DateTime transactionDate, string sysDocID, string voucherID, string productID, string locationID, int rowIndex, decimal quantity, decimal price, int itemType, int costMethod, string supplierCode, InventoryTransactionTypes transactionType, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			ItemTypes itemTypes = (ItemTypes)itemType;
			if (itemTypes != ItemTypes.Inventory && itemTypes != ItemTypes.ConsignmentItem && itemTypes != ItemTypes.Inventory3PL && itemTypes != ItemTypes.Assembly)
			{
				return true;
			}
			bool flag2 = false;
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "IsTrackLot", "ProductID", productID, sqlTransaction);
			if (fieldValue != null && fieldValue.ToString() != "")
			{
				flag2 = bool.Parse(fieldValue.ToString());
			}
			if (transactionType == InventoryTransactionTypes.CustomerCreditMemo || transactionType == InventoryTransactionTypes.JobInventoryReturn)
			{
				quantity = -1m * quantity;
			}
			decimal num = quantity;
			string text = "";
			if (quantity != 0m && (flag2 || itemTypes == ItemTypes.ConsignmentItem || itemTypes == ItemTypes.Inventory3PL))
			{
				DataRow[] array = transactionData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + productID + "' AND RowIndex = " + rowIndex);
				if (array.Length == 0)
				{
					throw new CompanyException("Lot allocation not found item: '" + productID + "'");
				}
				DataRow[] array2 = array;
				foreach (DataRow dataRow in array2)
				{
					decimal num2 = decimal.Parse(dataRow["SoldQty"].ToString());
					int num3 = int.Parse(dataRow["LotNumber"].ToString());
					string b = dataRow["LocationID"].ToString();
					if (locationID != b)
					{
						throw new CompanyException("One of the selected lots is in a different location.\n Product Code:" + productID + "\nLot Number:" + dataRow["Reference"].ToString());
					}
					DataRow[] array3 = lotData.Tables["Product_Lot"].Select("LotNumber = " + num3);
					if (array3.Length == 0)
					{
						throw new CompanyException("Error in lot allocation. Lot number: " + num3 + " not found.\n Product Code:" + productID);
					}
					DataRow dataRow2 = array3[0];
					decimal d = decimal.Parse(dataRow2["LotQty"].ToString());
					if (d < num2)
					{
						throw new CompanyException("Available quantity in lot number: " + num3 + " is less than issued quantity.Product Code:" + productID);
					}
					dataRow2["LotQty"] = d - Math.Abs(num2);
					switch (transactionType)
					{
					case InventoryTransactionTypes.VendorCreditMemo:
					case InventoryTransactionTypes.ConsignInReturn:
						text = text + "\n  UPDATE Product_Lot SET ReturnedQty = ISNULL(ReturnedQty,0) + " + num2 + " WHERE LotNumber='" + dataRow["LotNumber"].ToString() + "' AND ItemCode='" + dataRow["ProductID"].ToString() + "' ";
						break;
					case InventoryTransactionTypes.CustomerCreditMemo:
					case InventoryTransactionTypes.JobInventoryReturn:
						text = ((!(num2 < 0m)) ? (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) - (") : (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) + ("));
						text = text + num2 + ") WHERE LotNumber='" + dataRow["LotNumber"].ToString() + "' AND ItemCode='" + dataRow["ProductID"].ToString() + "' ";
						break;
					default:
						text = ((!(num2 < 0m)) ? (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) + (") : (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) - ("));
						text = text + num2 + ") WHERE LotNumber='" + dataRow["LotNumber"].ToString() + "' AND ItemCode='" + dataRow["ProductID"].ToString() + "' ";
						break;
					}
					decimal num4 = default(decimal);
					switch (costMethod)
					{
					case 1:
						num4 = GetProductAverageCostAsOfDate(productID, locationID, Math.Abs(num2), transactionDate, sqlTransaction);
						break;
					case 2:
						num4 = ((dataRow["Cost"] != null && dataRow["Cost"].ToString() != string.Empty) ? decimal.Parse(dataRow["Cost"].ToString()) : 0m);
						break;
					}
					if (transactionType == InventoryTransactionTypes.CustomerCreditMemo || transactionType == InventoryTransactionTypes.JobInventoryReturn)
					{
						num2 = -1m * num2;
					}
					if (num2 != 0m)
					{
						text = text + "\n  INSERT INTO [Product_Lot_Sales] (DocID,InvoiceNumber,TransactionDate,RowIndex,ItemCode,LocationID,LotNo,SoldQty,UnitPrice,Cost,BinID,Reference2) VALUES('" + sysDocID + "','" + voucherID + "','" + StoreConfiguration.ToSqlDateTimeString(transactionDate) + "'," + rowIndex + ",'" + productID + "','" + locationID + "'," + dataRow["LotNumber"].ToString() + "," + num2.ToString() + "," + price + "," + num4.ToString() + ",'" + dataRow["BinID"].ToString() + "','" + dataRow["Reference2"].ToString() + "') ";
						num += Math.Abs(num2);
					}
				}
			}
			else
			{
				DataRow[] array4 = lotData.Tables["Product_Lot"].Select("ItemCode = '" + productID + "' AND LocationID = '" + locationID + "'", "ReceiptDate");
				if (num > 0m)
				{
					throw new CompanyException("Cannot allocate negative quantity. Transaction No:" + voucherID + " Item:" + productID);
				}
				DataRow[] array2 = array4;
				foreach (DataRow dataRow3 in array2)
				{
					if (num == 0m)
					{
						break;
					}
					decimal num5 = decimal.Parse(dataRow3["LotQty"].ToString());
					decimal num6 = default(decimal);
					if (num5 >= Math.Abs(num))
					{
						num5 += num;
						num6 = Math.Abs(num);
						num = default(decimal);
						dataRow3["LotQty"] = num5;
					}
					else
					{
						num += num5;
						num6 = num5;
						dataRow3["LotQty"] = default(decimal);
					}
					switch (transactionType)
					{
					case InventoryTransactionTypes.VendorCreditMemo:
						text = text + "\n  UPDATE Product_Lot SET ReturnedQty = ISNULL(ReturnedQty,0) + " + num6 + " WHERE LotNumber='" + dataRow3["LotNumber"].ToString() + "' AND ItemCode='" + dataRow3["ItemCode"].ToString() + "' ";
						break;
					case InventoryTransactionTypes.CustomerCreditMemo:
					case InventoryTransactionTypes.JobInventoryReturn:
						text = ((!(num6 < 0m)) ? (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) - (") : (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) + ("));
						text = text + num6 + ") WHERE LotNumber='" + dataRow3["LotNumber"].ToString() + "' AND ItemCode='" + dataRow3["ItemCode"].ToString() + "' ";
						break;
					default:
						text = ((!(num6 < 0m)) ? (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) + (") : (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) - ("));
						text = text + num6 + ") WHERE LotNumber='" + dataRow3["LotNumber"].ToString() + "' AND ItemCode='" + dataRow3["ItemCode"].ToString() + "' ";
						break;
					}
					decimal num7 = default(decimal);
					num7 = decimal.Parse(dataRow3["Cost"].ToString());
					if (transactionType == InventoryTransactionTypes.CustomerCreditMemo || transactionType == InventoryTransactionTypes.JobInventoryReturn)
					{
						num6 = -1m * num6;
					}
					if (num6 != 0m)
					{
						text = text + "\n  INSERT INTO [Product_Lot_Sales] (DocID,InvoiceNumber,TransactionDate,RowIndex,ItemCode,LocationID,LotNo,SoldQty,UnitPrice,Cost,BinID,Reference2) VALUES('" + sysDocID + "','" + voucherID + "','" + StoreConfiguration.ToSqlDateTimeString(transactionDate) + "'," + rowIndex + ",'" + productID + "','" + locationID + "'," + dataRow3["LotNumber"].ToString() + "," + num6.ToString() + "," + price + "," + num7.ToString() + ",'" + dataRow3["BinID"].ToString() + "','" + dataRow3["Reference2"].ToString() + "') ";
					}
				}
			}
			if (text != "")
			{
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
			}
			if (Math.Abs(num) > 0m)
			{
				if (flag2)
				{
					throw new CompanyException("You cannot issue more than available quantity for an item which you are tracking the lot.");
				}
				text = " INSERT INTO [Unallocated_Lot_Items] (SysDocID,VoucherID,TransactionDate,ProductID,RowIndex,LocationID,Quantity,Price) VALUES ('" + sysDocID + "','" + voucherID + "','" + StoreConfiguration.ToSqlDateTimeString(transactionDate) + "','" + productID + "'," + rowIndex + ",'" + locationID + "'," + Math.Abs(num) + "," + price + ")";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				{
					foreach (DataRow row in transactionData.InventoryTransactionTable.Rows)
					{
						row["IsRecost"] = true;
					}
					return flag;
				}
			}
			return flag;
		}

		internal bool AllocateUnallocatedItemsToLot(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string text = "SELECT UL.ProductID\r\n\t\t\t\t\t\t\t\tFROM Unallocated_Lot_Items UL INNER JOIN Product P ON P.ProductID =UL.ProductID \r\n\t\t\t\t\t\t\t\tINNER JOIN Inventory_Transactions IT ON IT.SysDocID = UL.SysDocID AND IT.VoucherID =UL.VoucherID AND IT.ProductID = UL.ProductID AND IT.RowIndex = UL.RowIndex ";
			if (sysDocID != "")
			{
				text = text + " WHERE UL.ProductID IN (SELECT ProductID FROM Inventory_Transactions WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "')";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Products", text, sqlTransaction);
			List<string> list = new List<string>();
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				string item = row["ProductID"].ToString();
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			string[] productIDs = list.ToArray();
			return AllocateUnallocatedItemsToLot(productIDs, sqlTransaction);
		}

		internal bool AllocateUnallocatedItemsToLot(string[] productIDs, SqlTransaction sqlTransaction)
		{
			if (productIDs.Length == 0)
			{
				return true;
			}
			bool flag = true;
			string text = "";
			for (int i = 0; i < productIDs.Length; i++)
			{
				text = "'" + productIDs[i] + "'";
				if (i < productIDs.Length - 1)
				{
					text += ",";
				}
			}
			string str = "SELECT DISTINCT UL.ID,UL.SysDocID,UL.VoucherID,UL.ProductID,UL.RowIndex,UL.LocationID,UL.Quantity,UL.Price ,P.CostMethod,P.ItemType,IT.TransactionDate,0 AS AllocatedQty \r\n\t\t\t\t\t\t\t\tFROM Unallocated_Lot_Items UL INNER JOIN Product P ON P.ProductID =UL.ProductID \r\n\t\t\t\t\t\t\t\tINNER JOIN Inventory_Transactions IT ON IT.SysDocID = UL.SysDocID AND IT.VoucherID =UL.VoucherID AND IT.ProductID = UL.ProductID AND IT.RowIndex = UL.RowIndex ";
			str = str + " WHERE UL.ProductID IN (" + text + ")";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Unallocated_Lot_Items", str, sqlTransaction);
			str = "Select DISTINCT LotNumber,SoldQty,ReturnedQty,ItemCode,DocID,ReceiptDate,ReceiptNumber,LocationID,Cost,AvgCost, (LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0) AS LotQty FROM product_lot\r\n\t\t\t\t\t\t\t\tWHERE ItemCode IN (" + text + ") and  \r\n\t\t\t\t\t\t\t\t(LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0)>0 and ISNULL(IsDeleted,'False') = 'False'\r\n\t\t\t\t\t\t\t\tOrder BY ReceiptDate,ReceiptNumber DESC ";
			FillDataSet(dataSet, "Product_Lot", str, sqlTransaction);
			DataTable dataTable = dataSet.Tables["Product_Lot"];
			InventoryTransactionTypes inventoryTransactionTypes = InventoryTransactionTypes.Sale;
			Dictionary<string, KeyValuePair<string, string>> dictionary = new Dictionary<string, KeyValuePair<string, string>>();
			str = "";
			DataSet dataSet2 = new DataSet();
			DataTable dataTable2 = dataSet2.Tables.Add("Product_Lot_Sales");
			dataTable2.Columns.Add("DocID", typeof(string));
			dataTable2.Columns.Add("InvoiceNumber", typeof(string));
			dataTable2.Columns.Add("TransactionDate", typeof(DateTime));
			dataTable2.Columns.Add("RowIndex", typeof(int));
			dataTable2.Columns.Add("ItemCode", typeof(string));
			dataTable2.Columns.Add("LocationID", typeof(string));
			dataTable2.Columns.Add("LotNo", typeof(int));
			dataTable2.Columns.Add("SoldQty", typeof(decimal));
			dataTable2.Columns.Add("UnitPrice", typeof(decimal));
			dataTable2.Columns.Add("Cost", typeof(decimal));
			Dictionary<string, KeyValuePair<string, string>> dictionary2 = new Dictionary<string, KeyValuePair<string, string>>();
			foreach (DataRow row in dataSet.Tables["Unallocated_Lot_Items"].Rows)
			{
				string text2 = row["SysDocID"].ToString();
				string text3 = row["VoucherID"].ToString();
				string text4 = row["ProductID"].ToString();
				string text5 = row["LocationID"].ToString();
				decimal num = Math.Abs(decimal.Parse(row["Quantity"].ToString()));
				decimal num2 = decimal.Parse(row["Price"].ToString());
				int num3 = int.Parse(row["RowIndex"].ToString());
				int.Parse(row["ItemType"].ToString());
				CostMethods costMethods = CostMethods.Average;
				costMethods = CostMethods.Average;
				DateTime dateTime = DateTime.Parse(row["TransactionDate"].ToString());
				int.Parse(row["ID"].ToString());
				DataRow[] array = dataTable.Select("ItemCode = '" + text4 + "'  AND LocationID = '" + text5 + "'", "ReceiptDate,LotNumber");
				decimal num4 = num;
				DataRow[] array2 = array;
				foreach (DataRow dataRow2 in array2)
				{
					if (num4 == 0m)
					{
						break;
					}
					string text6 = dataRow2["DocID"].ToString();
					string text7 = dataRow2["ReceiptNumber"].ToString();
					if (costMethods == CostMethods.FIFO && !dictionary2.ContainsKey(text6 + text7))
					{
						dictionary2.Add(text6 + text7, new KeyValuePair<string, string>(text6, text7));
					}
					decimal num5 = decimal.Parse(dataRow2["LotQty"].ToString());
					decimal num6 = default(decimal);
					if (num5 >= Math.Abs(num4))
					{
						num5 -= num4;
						num6 = Math.Abs(num4);
						num4 = default(decimal);
						dataRow2["LotQty"] = num5;
					}
					else
					{
						num4 -= num5;
						num6 = num5;
						dataRow2["LotQty"] = default(decimal);
					}
					DataRow[] array3 = dataTable.Select("ItemCode='" + dataRow2["ItemCode"].ToString() + "' AND LotNumber='" + dataRow2["LotNumber"].ToString() + "' ");
					if (inventoryTransactionTypes == InventoryTransactionTypes.VendorCreditMemo)
					{
						decimal num7 = default(decimal);
						if (array3.Length != 0 && !array3[0]["ReturnedQty"].IsDBNullOrEmpty())
						{
							num7 = decimal.Parse(array3[0]["ReturnedQty"].ToString());
						}
						num7 += num6;
						array3[0]["ReturnedQty"] = num7;
					}
					else
					{
						decimal num8 = default(decimal);
						if (array3.Length != 0 && !array3[0]["SoldQty"].IsDBNullOrEmpty())
						{
							num8 = decimal.Parse(array3[0]["SoldQty"].ToString());
						}
						if (num6 < 0m)
						{
							num8 -= num6;
						}
						else
						{
							num8 += num6;
						}
						array3[0]["SoldQty"] = num8;
					}
					decimal num9 = default(decimal);
					num9 = decimal.Parse(dataRow2["Cost"].ToString());
					if (num6 != 0m)
					{
						DataRow dataRow3 = dataTable2.NewRow();
						dataRow3["DocID"] = text2;
						dataRow3["InvoiceNumber"] = text3;
						dataRow3["TransactionDate"] = dateTime;
						dataRow3["RowIndex"] = num3;
						dataRow3["ItemCode"] = text4;
						dataRow3["LocationID"] = text5;
						dataRow3["LotNo"] = dataRow2["LotNumber"].ToString();
						dataRow3["SoldQty"] = num6.ToString();
						dataRow3["UnitPrice"] = num2;
						dataRow3["Cost"] = num9.ToString();
						dataRow3.EndEdit();
						dataTable2.Rows.Add(dataRow3);
					}
				}
				row["Quantity"] = num4;
				if (!dictionary.ContainsKey(text2 + text3))
				{
					dictionary.Add(text2 + text3, new KeyValuePair<string, string>(text2, text3));
				}
			}
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			insertCommand = new SqlCommand(" INSERT INTO  Product_Lot_Sales  (DocID,InvoiceNumber,TransactionDate,RowIndex,ItemCode,LocationID,LotNo,SoldQty,UnitPrice,Cost) \r\n\t\t\t\t\t\t\t\t\t\tValues(@DocID,@InvoiceNumber,@TransactionDate,@RowIndex,@ItemCode,@LocationID,@LotNo,@SoldQty,@UnitPrice,@Cost)");
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@DocID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceNumber", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ItemCode", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@LotNo", SqlDbType.Int);
			parameters.Add("@SoldQty", SqlDbType.Decimal);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Cost", SqlDbType.Decimal);
			parameters["@DocID"].SourceColumn = "DocID";
			parameters["@InvoiceNumber"].SourceColumn = "InvoiceNumber";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@ItemCode"].SourceColumn = "ItemCode";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@LotNo"].SourceColumn = "LotNo";
			parameters["@SoldQty"].SourceColumn = "SoldQty";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Cost"].SourceColumn = "Cost";
			insertCommand.Connection = base.DBConfig.Connection;
			if (dataSet2.Tables["Product_Lot_Sales"].Rows.Count > 0)
			{
				flag &= Insert(dataSet2, "Product_Lot_Sales", insertCommand, sqlTransaction);
			}
			flag &= UpdateUnallocatedLotsQuantity(dataSet.GetChanges(), sqlTransaction);
			flag &= UpdateProductLotQuantity(dataSet.GetChanges(), sqlTransaction);
			str = "DELETE FROM Unallocated_Lot_Items WHERE Quantity = 0";
			flag &= (ExecuteNonQuery(str, sqlTransaction) >= 0);
			str = "  UPDATE  PLD \r\n                              SET LotNumber= PLS.LotNo\r\n                              FROM Product_Lot_Issue_Detail PLD INNER JOIN Product_Lot_Sales PLS\r\n                              ON PLD.SysDocID = PLS.DocID AND PLD.VoucherID = PLS.InvoiceNumber AND PLD.ProductID = PLS.ItemCode AND PLD.RowIndex = PLS.RowIndex\r\n                              WHERE ProductID IN (" + text + ")";
			return flag & (ExecuteNonQuery(str, sqlTransaction) >= 0);
		}

		private bool UpdateUnallocatedLotsQuantity(DataSet data, SqlTransaction sqlTransaction)
		{
			try
			{
				if (data == null || !data.Tables.Contains("Unallocated_Lot_Items") || data.Tables["Unallocated_Lot_Items"].Rows.Count == 0)
				{
					return true;
				}
				updateCommand = new SqlCommand("", base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				updateCommand.Transaction = sqlTransaction;
				updateCommand.CommandText = "UPDATE Unallocated_Lot_Items SET Quantity = @Quantity WHERE ID = @ID ";
				updateCommand.Parameters.Add("@Quantity", SqlDbType.Decimal, 18, "Quantity");
				updateCommand.Parameters.Add("@ID", SqlDbType.Int, 18, "ID");
				dsCommand.UpdateBatchSize = 1000;
				updateCommand.UpdatedRowSource = UpdateRowSource.None;
				bool result = Update(data, "Unallocated_Lot_Items", updateCommand);
				updateCommand.UpdatedRowSource = UpdateRowSource.Both;
				dsCommand.UpdateBatchSize = 1;
				return result;
			}
			catch
			{
				throw;
			}
		}

		private bool UpdateProductLotQuantity(DataSet data, SqlTransaction sqlTransaction)
		{
			try
			{
				if (data == null || !data.Tables.Contains("Product_Lot") || data.Tables["Product_Lot"].Rows.Count == 0)
				{
					return true;
				}
				updateCommand = new SqlCommand("", base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				updateCommand.Transaction = sqlTransaction;
				updateCommand.CommandText = "UPDATE Product_Lot SET SoldQty = @SoldQty, ReturnedQty = @ReturnedQty WHERE LotNumber = @LotNumber ";
				updateCommand.Parameters.Add("@SoldQty", SqlDbType.Decimal, 18, "SoldQty");
				updateCommand.Parameters.Add("@ReturnedQty", SqlDbType.Decimal, 18, "ReturnedQty");
				updateCommand.Parameters.Add("@LotNumber", SqlDbType.Int, 18, "LotNumber");
				dsCommand.UpdateBatchSize = 1000;
				updateCommand.UpdatedRowSource = UpdateRowSource.None;
				bool result = Update(data, "Product_Lot", updateCommand);
				updateCommand.UpdatedRowSource = UpdateRowSource.Both;
				dsCommand.UpdateBatchSize = 1;
				return result;
			}
			catch
			{
				throw;
			}
		}

		public InventoryTransactionData GetInventoryTransaction(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			try
			{
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				string textCommand = "SELECT IT.*,P.ItemType FROM Inventory_Transactions IT INNER JOIN Product P  ON P.ProductID = IT.ProductID\r\n\t\t\t\t\t\t\t\tWHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherNumber + "'";
				FillDataSet(inventoryTransactionData, "Inventory_Transactions", textCommand, sqlTransaction);
				return inventoryTransactionData;
			}
			catch
			{
				throw;
			}
		}

		public bool RemoveInvoiceLotAllocation(InventoryTransactionData inventoryTransactionData, SqlTransaction sqlTransaction)
		{
			string text = "";
			bool flag = true;
			DataSet dataSet = new DataSet();
			string text2 = inventoryTransactionData.InventoryTransactionTable.Rows[0]["VoucherID"].ToString();
			string text3 = inventoryTransactionData.InventoryTransactionTable.Rows[0]["SysDocID"].ToString();
			new DataSet();
			InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)byte.Parse(inventoryTransactionData.InventoryTransactionTable.Rows[0]["TransactionType"].ToString());
			if (inventoryTransactionTypes == InventoryTransactionTypes.Purchase)
			{
				return true;
			}
			text = "Select * FROM Product_Lot_Sales WHERE DocID='" + text3 + "' AND InvoiceNumber='" + text2 + "'";
			FillDataSet(dataSet, "Product_Lot_Sales", text, sqlTransaction);
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				decimal num = decimal.Parse(row["SoldQty"].ToString());
				int num2 = int.Parse(row["LotNo"].ToString());
				text = ((inventoryTransactionTypes != InventoryTransactionTypes.VendorCreditMemo) ? ("Update Product_Lot SET SoldQty=SoldQty - " + num + " WHERE LotNumber='" + num2.ToString() + "' AND ItemCode='" + row["ItemCode"].ToString() + "'") : ("Update Product_Lot SET ReturnedQty = ISNULL(ReturnedQty,0) - " + num + " WHERE LotNumber='" + num2.ToString() + "' AND ItemCode='" + row["ItemCode"].ToString() + "'"));
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
			}
			text = "DELETE FROM Product_Lot_Sales WHERE DocID='" + text3 + "' AND InvoiceNumber='" + text2 + "'";
			return flag & (ExecuteNonQuery(text, sqlTransaction) >= 0);
		}

		private bool InsertUpdateInventoryTransaction(InventoryTransactionData inventoryTransactionData, bool isUpdate, bool isDirect, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				DataRow dataRow = inventoryTransactionData.InventoryTransactionTable.Rows[0];
				if (sqlTransaction == null)
				{
					throw new Exception("SQLTransaction cannot be null");
				}
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string a = dataRow["LocationID"].ToString();
				SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(dataRow["SysDocType"].ToString());
				DateTime dateTime = DateTime.Parse(dataRow["TransactionDate"].ToString());
				StoreConfiguration.ToSqlDateTimeString(dateTime);
				ValidateInventoryLockDate(inventoryTransactionData, sqlTransaction);
				if (!isUpdate && sysDocTypes != SysDocTypes.ConsignOut && sysDocTypes != SysDocTypes.ConsignOutReturn && a != "CON_OUT" && sysDocTypes != SysDocTypes.GarmentRental && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Inventory_Transactions", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1048);
				}
				if (isUpdate && sysDocTypes != SysDocTypes.ConsignOut && sysDocTypes != SysDocTypes.GarmentRental)
				{
					flag &= DeleteInventoryTransaction((int)sysDocTypes, text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)byte.Parse(dataRow["TransactionType"].ToString());
				bool flag2 = true;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Company", "NegativeQuantityAction", "CompanyID", "1", sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = (int.Parse(fieldValue.ToString()) != 3);
				}
				bool result = false;
				object obj = null;
				obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.EnableCostRunning, null);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result);
				}
				bool result2 = false;
				object obj2 = null;
				obj2 = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.FutureCosting, null);
				if (obj2 != null)
				{
					bool.TryParse(obj2.ToString(), out result2);
				}
				if (result)
				{
					result2 = true;
				}
				string commaSeperatedIDs = GetCommaSeperatedIDs(inventoryTransactionData, "Inventory_Transactions", "ProductID");
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT P.ProductID,P.ItemType,P.LastCost,CostMethod, P.Quantity AS TotalQuantity,AverageCost FROM Product P  \r\n\t\t\t\t\t\t\tWHERE P.ProductID IN  (" + commaSeperatedIDs + ")";
				FillDataSet(dataSet, "Product", textCommand, sqlTransaction);
				textCommand = "SELECT PL.ProductID, PL.LocationID, PL.Quantity AS LocationQuantity FROM Product_Location  PL  \r\n\t\t\t\t\t\t\tWHERE PL.ProductID IN  (" + commaSeperatedIDs + ")";
				FillDataSet(dataSet, "Product_Location", textCommand, sqlTransaction);
				string text3 = "";
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				DataSet dataSet2 = new DataSet();
				DataTable dataTable = dataSet2.Tables.Add("TR");
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("VoucherID");
				foreach (DataRow row in inventoryTransactionData.InventoryTransactionTable.Rows)
				{
					string text4 = row["SysDocID"].ToString() + row["VoucherID"].ToString();
					if (!arrayList.Contains(text4))
					{
						arrayList.Add(text4);
						dataTable.Rows.Add(row["SysDocID"].ToString(), row["VoucherID"].ToString());
					}
					if (!arrayList2.Contains(row["ProductID"].ToString()))
					{
						arrayList2.Add(row["ProductID"].ToString());
					}
				}
				DataTable dataTable2 = dataSet2.Tables.Add("Item");
				dataTable2.Columns.Add("ProductID");
				foreach (object item in arrayList2)
				{
					dataTable2.Rows.Add(item.ToString());
					text3 = ((!(text3 == string.Empty)) ? (text3 + ",'" + item.ToString() + "'") : ("'" + item.ToString() + "'"));
				}
				DataSet lotData = null;
				DataSet itData = null;
				if (!result)
				{
					lotData = GetCostingLotData(dataSet2, sqlTransaction);
					itData = GetCostingInventoryData(arrayList2, dateTime, includePreviousRowsTable: true, text2, text, sqlTransaction);
				}
				for (int i = 0; i < inventoryTransactionData.InventoryTransactionTable.Rows.Count; i++)
				{
					DataRow itRow = inventoryTransactionData.InventoryTransactionTable.Rows[i];
					itRow["SysDocID"] = dataRow["SysDocID"];
					itRow["VoucherID"] = dataRow["VoucherID"];
					string text5 = itRow["ProductID"].ToString();
					int.Parse(itRow["RowIndex"].ToString());
					a = itRow["LocationID"].ToString();
					decimal num = default(decimal);
					DataRow[] array = dataSet.Tables["Product"].Select("ProductID = '" + text5 + "'");
					if (array.Length == 0)
					{
						throw new CompanyException("Item code not found in items table.\nThe missing item code is:'" + text5 + "'", 1044);
					}
					DataRow dataRow3 = array[0];
					ItemTypes itemTypes = (ItemTypes)byte.Parse(dataRow3["ItemType"].ToString());
					CostMethods costMethods = (CostMethods)byte.Parse(dataRow3["CostMethod"].ToString());
					itRow["ItemType"] = (byte)itemTypes;
					if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.ConsignmentItem || itemTypes == ItemTypes.Inventory3PL || itemTypes == ItemTypes.Assembly)
					{
						float num2 = float.Parse(itRow["Quantity"].ToString());
						float result3 = 0f;
						if ((inventoryTransactionTypes == InventoryTransactionTypes.Assembly || inventoryTransactionTypes == InventoryTransactionTypes.Production) && num2 > 0f)
						{
							decimal num3 = default(decimal);
							foreach (DataRow row2 in inventoryTransactionData.InventoryTransactionTable.Rows)
							{
								decimal result4 = default(decimal);
								decimal.TryParse(row2["Quantity"].ToString(), out result4);
								if (!(result4 >= 0m) && row2["AssetValue"] != DBNull.Value)
								{
									num3 += Math.Abs(decimal.Parse(row2["AssetValue"].ToString()));
								}
							}
							decimal d = default(decimal);
							decimal d2 = default(decimal);
							switch (sysDocTypes)
							{
							case SysDocTypes.InventoryRepacking:
							{
								decimal d4 = default(decimal);
								if (itRow["Cost"] != DBNull.Value)
								{
									d4 = decimal.Parse(itRow["Cost"].ToString());
								}
								d = (decimal)num2 * d4;
								break;
							}
							case SysDocTypes.Production:
							{
								decimal d3 = default(decimal);
								num3 = default(decimal);
								if (itRow["Cost"] != DBNull.Value)
								{
									d3 = decimal.Parse(itRow["Cost"].ToString());
								}
								d = (decimal)num2 * d3;
								break;
							}
							default:
								textCommand = "SELECT DISTINCT\r\n\t\t\t\t\t\t\t\tISNULL((SELECT ABS(SUM(ISNULL(Amount,0))) FROM Assembly_Build_Expense WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'),0) AS TotalCost\r\n\t\t\t\t\t\t\t\tFROM Assembly_Build_Expense";
								fieldValue = ExecuteScalar(textCommand, sqlTransaction);
								if (fieldValue != null && fieldValue.ToString() != "")
								{
									d = decimal.Parse(fieldValue.ToString());
								}
								break;
							}
							d += num3;
							if (num2 != 0f)
							{
								d2 = Math.Round(d / (decimal)num2, 5);
							}
							itRow["UnitPrice"] = Math.Round(d2, 5);
						}
						DataRow[] array2 = dataSet.Tables["Product_Location"].Select("ProductID = '" + text5 + "' AND LocationID IS NOT NULL AND LocationID = '" + a + "'");
						DataRow dataRow5 = null;
						if (array2.Length != 0)
						{
							float.TryParse(array2[0]["LocationQuantity"].ToString(), out result3);
							dataRow5 = array2[0];
						}
						float result5 = 0f;
						float.TryParse(dataRow3["TotalQuantity"].ToString(), out result5);
						decimal num4 = default(decimal);
						if (!result)
						{
							if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
							{
								num4 = GetProductAverageCostAsOfDate(text5, a, (decimal)(-1f * num2), dateTime, sqlTransaction);
								if (num4 == 0m && num2 > 0f)
								{
									num4 = ((itRow["UnitPrice"] != DBNull.Value) ? decimal.Parse(itRow["UnitPrice"].ToString()) : 0m);
								}
							}
							else
							{
								num4 = default(decimal);
							}
						}
						if ((inventoryTransactionTypes == InventoryTransactionTypes.Adjustment && num2 > 0f) || (inventoryTransactionTypes == InventoryTransactionTypes.Assembly && num2 > 0f) || (inventoryTransactionTypes == InventoryTransactionTypes.OpeningInventory && num2 > 0f) || inventoryTransactionTypes == InventoryTransactionTypes.Purchase)
						{
							num = default(decimal);
							decimal.TryParse(itRow["UnitPrice"].ToString(), out num);
							if (inventoryTransactionTypes == InventoryTransactionTypes.Purchase && !isUpdate)
							{
								decimal result6 = default(decimal);
								decimal.TryParse(itRow["Cost"].ToString(), out result6);
								dataRow3["LastCost"] = result6;
							}
						}
						else
						{
							num = num4;
						}
						if (inventoryTransactionTypes != InventoryTransactionTypes.Sale)
						{
							_ = 1;
						}
						if ((inventoryTransactionTypes == InventoryTransactionTypes.Adjustment && num2 < 0f) || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryIssue || (inventoryTransactionTypes == InventoryTransactionTypes.Assembly && num2 < 0f))
						{
							itRow["UnitPrice"] = num;
						}
						float num5 = result3 + num2;
						bool flag3 = num2 < 0f;
						if ((inventoryTransactionTypes == InventoryTransactionTypes.Sale || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryIssue || inventoryTransactionTypes == InventoryTransactionTypes.Transfer || (inventoryTransactionTypes == InventoryTransactionTypes.Adjustment && sysDocTypes != SysDocTypes.SalesPOS)) && sysDocTypes != SysDocTypes.TransitTransferIn && flag3 && !flag2 && num5 < 0f)
						{
							throw new CompanyException("You do not have sufficient quantity onhand for one or more items.\nItem:" + text5);
						}
						float num6 = result5 + num2;
						if (dataRow3 != null)
						{
							dataRow3["TotalQuantity"] = num6;
						}
						if (dataRow5 != null)
						{
							dataRow5["LocationQuantity"] = num5;
						}
						bool isLocationCosting = new CompanyInformations(base.DBConfig).GetIsLocationCosting(sqlTransaction);
						if (!result)
						{
							DataRow prevRow = null;
							string text6 = "";
							if (isLocationCosting)
							{
								text6 = " AND LocationID = '" + a + "' ";
							}
							if (itData != null)
							{
								DataRow[] array3 = itData.Tables["First_Rows"].Select("ProductID = '" + text5 + "' " + text6, "TransactionDate DESC,TransactionID DESC");
								if (array3.Length != 0)
								{
									prevRow = array3[0];
								}
								array3 = itData.Tables["Inventory_Transactions"].Select("ProductID = '" + text5 + "' " + text6 + " AND TransactionDate < '" + dateTime + "'");
								int itRowIndex = array3.Length;
								if (costMethods != CostMethods.Average)
								{
									throw new CompanyException("FIFO is not supported yet. Please use average cost method.");
								}
								SetAvgCostIndividualITRowCosting(text5, ref itRow, prevRow, ref itData, inventoryTransactionData, ref array3, ref lotData, itRowIndex, isNewRow: true, sqlTransaction);
							}
						}
						dataRow3["AverageCost"] = itRow["AverageCost"];
					}
				}
				flag &= new Products(base.DBConfig).UpdateTotalQuantity(dataSet, sqlTransaction);
				flag &= new Products(base.DBConfig).UpdateLocationQuantity(inventoryTransactionData, isReverse: false, sqlTransaction);
				SqlCommand insertUpdateInventoryTransactionCommand = GetInsertUpdateInventoryTransactionCommand(isUpdate);
				insertUpdateInventoryTransactionCommand.Transaction = sqlTransaction;
				if (inventoryTransactionData.Tables["Inventory_Transactions"].Rows.Count > 0)
				{
					flag &= Insert(inventoryTransactionData, "Inventory_Transactions", insertUpdateInventoryTransactionCommand);
				}
				switch (sysDocTypes)
				{
				default:
					if (!result2)
					{
						return flag & UpdateFutureAndPastAffectedTransactionsCOGS(inventoryTransactionData, sqlTransaction);
					}
					return flag;
				case SysDocTypes.GoodsReceivedNote:
					return flag;
				case SysDocTypes.ImportGoodsReceivedNote:
					return flag;
				}
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		internal bool UpdateFutureAndPastAffectedTransactionsCOGS(InventoryTransactionData inventoryTransactionData, SqlTransaction sqlTransaction)
		{
			return UpdateFutureAndPastAffectedTransactionsCOGS(inventoryTransactionData, allocateLots: true, includeAllRecords: false, sqlTransaction);
		}

		internal bool UpdateFutureAndPastAffectedTransactionsCOGS(InventoryTransactionData inventoryTransactionData, bool allocateLots, bool includeAllRecords, SqlTransaction sqlTransaction)
		{
			try
			{
				if (inventoryTransactionData.InventoryTransactionTable.Rows.Count > 0)
				{
					inventoryTransactionData.InventoryTransactionTable.Rows[0]["SysDocID"].ToString();
					inventoryTransactionData.InventoryTransactionTable.Rows[0]["VoucherID"].ToString();
				}
				return (byte)(1 & (UpdateFutureTransactionsCost(inventoryTransactionData, includeAllRecords, sqlTransaction) ? 1 : 0)) != 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool DELETED_SetRowCostAndValue(string productID, CostMethods costMethod, ref DataRow itRow, int transactionID, SqlTransaction sqlTransaction)
		{
			_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			bool flag = true;
			string text = "";
			decimal value = default(decimal);
			SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(itRow["SysDocType"].ToString());
			string text2 = itRow["VoucherID"].ToString();
			string text3 = itRow["SysDocID"].ToString();
			string text4 = itRow["SysDocID"].ToString();
			int num = int.Parse(itRow["RowIndex"].ToString());
			DateTime dateTime = DateTime.Parse(itRow["TransactionDate"].ToString());
			decimal num2 = decimal.Parse(itRow["Quantity"].ToString());
			decimal num3 = default(decimal);
			bool flag2 = false;
			if (itRow["IsNonCostedGRN"] != DBNull.Value)
			{
				flag2 = bool.Parse(itRow["IsNonCostedGRN"].ToString());
			}
			bool flag3 = false;
			if (itRow["UnitPrice"] != DBNull.Value)
			{
				num3 = decimal.Parse(itRow["UnitPrice"].ToString());
			}
			string text5 = StoreConfiguration.ToSqlDateTimeString(dateTime);
			InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)int.Parse(itRow["TransactionType"].ToString());
			if ((inventoryTransactionTypes == InventoryTransactionTypes.Adjustment && num2 > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.Assembly && num2 > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.OpeningInventory && num2 > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.Purchase && num2 > 0m))
			{
				decimal num4 = Math.Round(num2 * num3, 5);
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				text = " SELECT SUM(Quantity-ISNULL(ReturnedQuantity,0)) AS TotalQuantity, SUM(AssetValue) AS TotalValue FROM Inventory_Transactions WHERE    ProductID = '" + productID + "' AND TransactionDate <= '" + text5 + "' ";
				text = ((!flag3) ? (text + " AND TransactionType <> 5 ") : (text + " AND LocationID = '" + text4 + "'"));
				if (transactionID > 0)
				{
					text = text + " AND TransactionID <> " + transactionID;
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "IT", text, sqlTransaction);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					decimal.TryParse(dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString(), out result);
					decimal.TryParse(dataSet.Tables[0].Rows[0]["TotalValue"].ToString(), out result2);
				}
				if (flag2)
				{
					text = ((costMethod != CostMethods.FIFO) ? (" SELECT TOP 1 ISNULL(UnitPrice,0) AS Cost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate < '" + text5 + "' AND TransactionType IN(1,6,8,18) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate DESC,TransactionID DESC ") : (" SELECT TOP 1 ISNULL(UnitPrice,0) AS Cost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate < '" + text5 + "' AND TransactionType IN(1,6,8,18) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate DESC,TransactionID DESC "));
					object obj = ExecuteScalar(text, sqlTransaction);
					if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
					{
						num4 = (value = decimal.Parse(obj.ToString())) * num2;
						itRow["UnitPrice"] = Math.Abs(value);
						text = "UPDATE Inventory_Transactions SET UnitPrice = " + Math.Abs(value) + " WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text2 + "' AND RowIndex = " + num;
						flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
						text = "UPDATE PL SET Cost = IT.UnitPrice FROM Product_Lot PL INNER JOIN Inventory_Transactions IT \r\n                                ON PL.DocID = IT.SysDocID AND PL.ReceiptNumber = IT.VoucherID AND PL.RowIndex = IT.RowIndex\r\n                                WHERE PL.DocID = '" + text3 + "' AND PL.ReceiptNumber = '" + text2 + "'";
						flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					}
				}
				else if (result > 0m && result2 >= 0m && !flag2)
				{
					value = ((!(result + num2 != 0m)) ? num3 : Math.Round((result2 + num4) / (result + num2), 5));
				}
				else if (result < 0m)
				{
					text = ((!flag3) ? (" SELECT DISTINCT TOP 1 IT2.TotalQuantity,IT2.TotalValue,IT.AverageCost ,IT.TransactionDate, IT.TransactionID\r\n                                        FROM Inventory_Transactions IT LEFT OUTER JOIN \r\n                                        (SELECT TransactionID, SUM(Quantity-ISNULL(ReturnedQuantity,0)) OVER (Order by TransactionDate,TransactionID) AS TotalQuantity, \r\n\t                                    SUM(ISNULL(AssetValue,0)) OVER (Order by TransactionDate,TransactionID) AS TotalValue\r\n\t                                    FROM Inventory_Transactions WHERE ProductID = '" + productID + "'  AND TransactionType <> 5) AS IT2 ON IT.TransactionID = IT2.TransactionID\r\n\t                                    WHERE  IT.ProductID = '" + productID + "' AND IT.TransactionType <> 5 AND TotalQuantity >= 0 AND TransactionDate < '" + text5 + "' \t\r\n\t                                    ORDER BY IT.TransactionDate DESC, IT.TransactionID DESC") : (" SELECT DISTINCT TOP 1 IT2.TotalQuantity,IT2.TotalValue,IT.AverageCost ,IT.TransactionDate, IT.TransactionID\r\n                                        FROM Inventory_Transactions IT LEFT OUTER JOIN \r\n                                        (SELECT TransactionID, SUM(Quantity- ISNULL(ReturnedQuantity,0)) OVER (Order by TransactionDate,TransactionID) AS TotalQuantity, \r\n\t                                    SUM(ISNULL(AssetValue,0)) OVER (Order by TransactionDate,TransactionID) AS TotalValue\r\n\t                                    FROM Inventory_Transactions WHERE ProductID = '" + productID + "'  AND LocationID = '" + text4 + "') AS IT2 ON IT.TransactionID = IT2.TransactionID\r\n\t                                    WHERE  IT.ProductID = '" + productID + "' AND IT.LocationID = '" + text4 + "' AND TotalQuantity >= 0 AND TransactionDate < '" + text5 + "' \t\r\n\t                                    ORDER BY IT.TransactionDate DESC, IT.TransactionID DESC"));
					dataSet = new DataSet();
					FillDataSet(dataSet, "IT", text, sqlTransaction);
					DateTime date = new DateTime(1800, 1, 1);
					if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
					{
						decimal.TryParse(dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString(), out result);
						decimal.TryParse(dataSet.Tables[0].Rows[0]["TotalValue"].ToString(), out result2);
						if (result == 0m)
						{
							result2 = default(decimal);
						}
						date = DateTime.Parse(dataSet.Tables[0].Rows[0]["TransactionDate"].ToString());
					}
					text = ((!flag3) ? (" SELECT DISTINCT TOP 1 IT2.TotalQuantity,IT2.TotalValue,IT.AverageCost ,IT.TransactionDate, IT.TransactionID\r\n                                        FROM Inventory_Transactions IT LEFT OUTER JOIN \r\n                                        (SELECT TransactionID, SUM(Quantity - ISNULL(ReturnedQuantity,0)) OVER (Order by TransactionDate,TransactionID) AS TotalQuantity, \r\n\t                                    SUM(ISNULL(AssetValue,0)) OVER (Order by TransactionDate,TransactionID) AS TotalValue\r\n\t                                    FROM Inventory_Transactions WHERE  ProductID = '" + productID + "'  AND TransactionType <> 5) AS IT2 ON IT.TransactionID = IT2.TransactionID\r\n\t                                    WHERE IT.ProductID = '" + productID + "' AND IT.TransactionType <> 5 AND TotalQuantity >= 0 AND TransactionDate >= '" + StoreConfiguration.ToSqlDateTimeString(date) + "' \t\r\n\t                                    ORDER BY IT.TransactionDate DESC, IT.TransactionID DESC") : (" SELECT DISTINCT TOP 1 IT2.TotalQuantity,IT2.TotalValue,IT.AverageCost ,IT.TransactionDate, IT.TransactionID\r\n                                        FROM Inventory_Transactions IT LEFT OUTER JOIN \r\n                                        (SELECT TransactionID, SUM(Quantity - ISNULL(ReturnedQuantity,0)) OVER (Order by TransactionDate,TransactionID) AS TotalQuantity, \r\n\t                                    SUM(ISNULL(AssetValue,0)) OVER (Order by TransactionDate,TransactionID) AS TotalValue\r\n\t                                    FROM Inventory_Transactions WHERE ProductID = '" + productID + "'  AND LocationID = '" + text4 + "') AS IT2 ON IT.TransactionID = IT2.TransactionID\r\n\t                                    WHERE IT.ProductID = '" + productID + "' AND IT.LocationID = '" + text4 + "' AND TotalQuantity >= 0 AND TransactionDate >= '" + StoreConfiguration.ToSqlDateTimeString(date) + "' \t\r\n\t                                    ORDER BY IT.TransactionDate DESC, IT.TransactionID DESC"));
					dataSet = new DataSet();
					FillDataSet(dataSet, "IT", text, sqlTransaction);
					decimal num5 = default(decimal);
					decimal num6 = default(decimal);
					decimal num7 = default(decimal);
					decimal num8 = default(decimal);
					num7 = 0m;
					num8 = 0m;
					for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
					{
						if (!dataSet.Tables[0].Rows[i]["Quantity"].IsDBNullOrEmpty())
						{
							num5 = decimal.Parse(dataSet.Tables[0].Rows[i]["Quantity"].ToString());
						}
						if (!dataSet.Tables[0].Rows[i]["AssetValue"].IsDBNullOrEmpty())
						{
							num6 = decimal.Parse(dataSet.Tables[0].Rows[i]["AssetValue"].ToString());
						}
						num8 += num5;
						num7 += num6;
					}
					if (result == 0m)
					{
						result2 = default(decimal);
					}
					value = Math.Abs(Math.Round((num7 + num4) / (num8 + num2), 5));
					value = ((!(result + num2 > 0m)) ? num3 : Math.Round((result2 + num4) / (result + num2), 5));
				}
				else
				{
					value = num3;
				}
				itRow["AverageCost"] = Math.Abs(value);
				itRow["AssetValue"] = num4;
			}
			else if ((inventoryTransactionTypes == InventoryTransactionTypes.Transfer && num2 > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.ConsignOut && num2 > 0m) || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryReturn || inventoryTransactionTypes == InventoryTransactionTypes.CustomerCreditMemo || inventoryTransactionTypes == InventoryTransactionTypes.ConsignOutReturn)
			{
				if (sysDocTypes == SysDocTypes.TransitTransferIn || sysDocTypes == SysDocTypes.ReturnTransitTransfer)
				{
					text = "SELECT SysDocID,VoucherID FROM Inventory_Transfer WHERE \r\n                            ( AcceptSysDocID = '" + text3 + "' AND AcceptVoucherID = '" + text2 + "') OR (RejectAcceptSysDocID = '" + text3 + "' AND RejectAcceptVoucherID = '" + text2 + "')";
					DataSet dataSet2 = new DataSet();
					FillDataSet(dataSet2, "Transfer", text, sqlTransaction);
					if (dataSet2 != null && dataSet2.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = dataSet2.Tables[0].Rows[0];
						string text6 = dataRow["SysDocID"].ToString();
						string text7 = dataRow["VoucherID"].ToString();
						text = " SELECT CASE WHEN Quantity = 0 THEN 0 ELSE AssetValue/Quantity END FROM Inventory_Transactions WHERE SysDocID = '" + text6 + "' AND VoucherID = '" + text7 + "' AND RowIndex = " + num;
						object obj = ExecuteScalar(text, sqlTransaction);
						if (obj != null && obj.ToString() != "")
						{
							itRow["AssetValue"] = Math.Round(decimal.Parse(obj.ToString()) * num2, 5);
							itRow["AverageCost"] = Math.Round(decimal.Parse(obj.ToString()), 5);
						}
						if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
						{
							decimal num9 = decimal.Parse(obj.ToString());
							value = num9;
							_ = num9 * num2;
							itRow["UnitPrice"] = Math.Abs(value);
							text = "UPDATE Inventory_Transactions SET UnitPrice = " + Math.Abs(value) + " WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text2 + "' AND RowIndex = " + num;
							flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
							text = "UPDATE PL SET Cost = " + num9 + " FROM Product_Lot PL\r\n                                WHERE PL.DocID = '" + text3 + "' AND PL.ReceiptNumber = '" + text2 + "'";
							flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
						}
					}
				}
				else if (sysDocTypes == SysDocTypes.TransitTransferOut && num2 > 0m)
				{
					switch (costMethod)
					{
					case CostMethods.FIFO:
					{
						decimal num11 = CalculateFifoCOGS(productID, -1m * Convert.ToDecimal(num2), sysDocTypes, text3, text2, num, dateTime, sqlTransaction);
						itRow["AssetValue"] = Math.Abs(num11);
						num3 = Math.Abs(Math.Round(num11 / Convert.ToDecimal(num2), 5));
						if (num2 != 0m)
						{
							itRow["AverageCost"] = num3;
							itRow["UnitPrice"] = num3;
						}
						else
						{
							num3 = default(decimal);
							itRow["AverageCost"] = 0;
						}
						break;
					}
					case CostMethods.LIFO:
						throw new CompanyException("LIFO is not supported in the current version.");
					default:
					{
						decimal num10 = CalculateAverageCostCOGS(productID, -1m * Convert.ToDecimal(num2), transactionID, dateTime, sqlTransaction);
						itRow["AssetValue"] = Math.Abs(num10);
						num3 = Math.Abs(Math.Round(num10 / Convert.ToDecimal(num2), 5));
						if (num2 != 0m)
						{
							itRow["AverageCost"] = num3;
							itRow["UnitPrice"] = num3;
						}
						else
						{
							num3 = default(decimal);
							itRow["AverageCost"] = 0;
						}
						break;
					}
					}
					text = "UPDATE PL SET Cost = " + num3 + " FROM Product_Lot PL \r\n                                WHERE PL.DocID = '" + text3 + "' AND PL.ReceiptNumber = '" + text2 + "'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				}
				else
				{
					switch (sysDocTypes)
					{
					case SysDocTypes.ConsignOut:
					{
						text = "SELECT CASE WHEN IT.Quantity = 0 THEN 0 ELSE AssetValue/IT.Quantity END FROM Inventory_Transactions IT ,\r\n                                 (SELECT * FROM Consign_Out_Detail WHERE SysDocID ='" + text3 + "' aND VoucherID = '" + text2 + "' AND RowIndex = " + num + ") AS COD\r\n                                  WHERE IT.SysDocID = COD.SourceSysDocID AND IT.VoucherID = COD.SourceVoucherID AND IT.RowIndex = COD.SourceRowIndex ";
						object obj = ExecuteScalar(text, sqlTransaction);
						if (obj != null && obj.ToString() != "")
						{
							num3 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), 5));
							if (obj != null && obj.ToString() != "")
							{
								itRow["AssetValue"] = Math.Round(decimal.Parse(obj.ToString()) * num2, 5);
								itRow["AverageCost"] = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), 5));
								itRow["UnitPrice"] = num3;
							}
							else
							{
								num3 = default(decimal);
								itRow["AverageCost"] = 0;
								itRow["UnitPrice"] = 0;
							}
							text = "UPDATE PL SET Cost = " + num3 + " FROM Product_Lot PL\r\n                                WHERE PL.DocID = '" + text3 + "' AND PL.ReceiptNumber = '" + text2 + "'";
							flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
						}
						break;
					}
					case SysDocTypes.DeliveryReturn:
					{
						text = "SELECT DNoteSysDocID,DNoteVoucherID FROM Delivery_Return WHERE \r\n                             SysDocID = '" + text3 + "' AND VoucherID = '" + text2 + "'";
						DataSet dataSet3 = new DataSet();
						FillDataSet(dataSet3, "Delivery_Return", text, sqlTransaction);
						if (dataSet3 != null && dataSet3.Tables[0].Rows.Count > 0)
						{
							DataRow dataRow2 = dataSet3.Tables[0].Rows[0];
							string text8 = dataRow2["DNoteSysDocID"].ToString();
							string text9 = dataRow2["DNoteVoucherID"].ToString();
							text = ((costMethod != CostMethods.FIFO) ? (" SELECT CASE WHEN Quantity = 0 THEN 0 ELSE AssetValue/Quantity END FROM Inventory_Transactions WHERE SysDocID = '" + text8 + "' AND VoucherID = '" + text9 + "' AND RowIndex = " + num) : (" SELECT TOP 1 ISNULL(UnitPrice,0) AS Cost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate < '" + text5 + "' AND TransactionType IN(1,6,8,18) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate DESC,TransactionID DESC "));
							object obj = ExecuteScalar(text, sqlTransaction);
							if (obj != null && obj.ToString() != "")
							{
								itRow["AssetValue"] = Math.Round(decimal.Parse(obj.ToString()) * num2, 5);
								decimal num12 = Math.Round(decimal.Parse(obj.ToString()), 5);
								itRow["AverageCost"] = num12;
								itRow["UnitPrice"] = num12;
								text = "UPDATE Inventory_Transactions SET UnitPrice = " + Math.Abs(num12) + " WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text2 + "' AND RowIndex = " + num;
								int num13 = ExecuteNonQuery(text, sqlTransaction);
								flag = (flag && num13 >= 0);
								text = "UPDATE PL SET Cost = IT.UnitPrice FROM Product_Lot PL INNER JOIN Inventory_Transactions IT \r\n                                ON PL.DocID = IT.SysDocID AND PL.ReceiptNumber = IT.VoucherID AND PL.RowIndex = IT.RowIndex\r\n                                WHERE PL.DocID = '" + text3 + "' AND PL.ReceiptNumber = '" + text2 + "'";
								num13 = ExecuteNonQuery(text, sqlTransaction);
								flag = (flag && num13 >= 0);
							}
						}
						break;
					}
					default:
						switch (costMethod)
						{
						case CostMethods.FIFO:
						{
							decimal value3 = CalculateFifoCOGS(productID, Convert.ToDecimal(num2), sysDocTypes, text3, text2, num, dateTime, sqlTransaction);
							itRow["AssetValue"] = Math.Abs(Math.Round(Math.Abs(value3), 5));
							break;
						}
						case CostMethods.LIFO:
							throw new CompanyException("LIFO is not supported in the current version.");
						default:
						{
							decimal value2 = CalculateAverageCostCOGS(productID, Convert.ToDecimal(num2), transactionID, dateTime, sqlTransaction);
							itRow["AssetValue"] = Math.Round(Math.Abs(value2), 5);
							break;
						}
						}
						break;
					}
				}
			}
			else if ((sysDocTypes == SysDocTypes.TransitTransferIn || sysDocTypes == SysDocTypes.ReturnTransitTransfer) && num2 < 0m)
			{
				text = "SELECT SysDocID,VoucherID FROM Inventory_Transfer WHERE \r\n                            ( AcceptSysDocID = '" + text3 + "' AND AcceptVoucherID = '" + text2 + "') OR (RejectAcceptSysDocID = '" + text3 + "' AND RejectAcceptVoucherID = '" + text2 + "')";
				DataSet dataSet4 = new DataSet();
				FillDataSet(dataSet4, "Transfer", text, sqlTransaction);
				if (dataSet4 != null && dataSet4.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow3 = dataSet4.Tables[0].Rows[0];
					string text10 = dataRow3["SysDocID"].ToString();
					string text11 = dataRow3["VoucherID"].ToString();
					text = " SELECT CASE WHEN Quantity = 0 THEN 0 ELSE AssetValue/Quantity END FROM Inventory_Transactions WHERE SysDocID = '" + text10 + "' AND VoucherID = '" + text11 + "' AND RowIndex = " + num;
					object obj = ExecuteScalar(text, sqlTransaction);
					if (obj != null && obj.ToString() != "")
					{
						itRow["AssetValue"] = -1m * Math.Abs(Math.Round(decimal.Parse(obj.ToString()) * num2, 5));
						itRow["AverageCost"] = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), 5));
						itRow["UnitPrice"] = 0;
					}
				}
			}
			else
			{
				switch (costMethod)
				{
				case CostMethods.FIFO:
				{
					decimal num15 = CalculateFifoCOGS(productID, Convert.ToDecimal(num2), sysDocTypes, text3, text2, num, dateTime, sqlTransaction);
					itRow["AssetValue"] = -1m * Math.Abs(num15);
					if (num2 != 0m)
					{
						itRow["AverageCost"] = Math.Abs(Math.Round(num15 / Convert.ToDecimal(num2), 5));
					}
					else
					{
						itRow["AverageCost"] = 0;
					}
					break;
				}
				case CostMethods.LIFO:
					throw new CompanyException("LIFO is not supported in the current version.");
				default:
				{
					decimal num14 = CalculateAverageCostCOGS(productID, Convert.ToDecimal(num2), transactionID, dateTime, sqlTransaction);
					itRow["AssetValue"] = -1m * Math.Abs(Math.Round(num14, 5));
					if (num2 != 0m)
					{
						itRow["AverageCost"] = Math.Abs(Math.Round(num14 / Convert.ToDecimal(num2), 5));
					}
					else
					{
						itRow["AverageCost"] = 0;
					}
					break;
				}
				}
			}
			return true;
		}

		internal decimal CalculateAverageCostCOGS(string productID, decimal quantity, int transactionID, DateTime transactionDate, SqlTransaction sqlTransaction)
		{
			_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			decimal d = default(decimal);
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			string text = "";
			string text2 = StoreConfiguration.ToSqlDateTimeString(transactionDate);
			DataSet runningTotalsAsOfDate = GetRunningTotalsAsOfDate(productID, transactionDate, transactionID, excludeTransfers: true, sqlTransaction);
			bool flag = true;
			if (runningTotalsAsOfDate == null || runningTotalsAsOfDate.Tables.Count == 0 || runningTotalsAsOfDate.Tables[0].Rows.Count == 0)
			{
				flag = false;
			}
			else
			{
				DataRow dataRow = runningTotalsAsOfDate.Tables[0].Rows[0];
				decimal.TryParse(dataRow["TotalQuantity"].ToString(), out result);
				decimal.TryParse(dataRow["TotalValue"].ToString(), out result2);
				if (result < Math.Abs(quantity))
				{
					flag = false;
				}
			}
			if (flag && result2 >= 0m)
			{
				if (result2 >= 0m)
				{
					d = ((!(result != 0m)) ? result2 : (result2 * (quantity / result)));
				}
			}
			else
			{
				text = " SELECT TOP 1 AverageCost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate >= '" + text2 + "' AND TransactionType IN(1,6,8,18) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate ASC,TransactionID ASC ";
				object obj = ExecuteScalar(text, sqlTransaction);
				if (obj != null && obj.ToString() != "")
				{
					return decimal.Parse(obj.ToString()) * quantity;
				}
				if (result == 0m)
				{
					return 0m;
				}
				if (result > 0m && result2 > 0m)
				{
					d = result2 / result * quantity;
				}
				else
				{
					text = " SELECT TOP 1 AverageCost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate < '" + text2 + "' AND TransactionType IN(1,6,8,18) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate DESC,TransactionID DESC ";
					obj = ExecuteScalar(text, sqlTransaction);
					if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
					{
						decimal d2 = decimal.Parse(obj.ToString());
						d = quantity * d2;
					}
				}
			}
			return Math.Round(d, 5);
		}

		internal decimal CalculateFifoCOGS(DataSet lotData, string productID, decimal quantity, SysDocTypes sysDocType, string docID, string voucherNumber, int rowIndex, DateTime transactionDate, SqlTransaction sqlTransaction)
		{
			decimal num = default(decimal);
			CommonLib.ToSqlDateTimeString(transactionDate);
			_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			DataTable dataTable = lotData.Tables["Product_Lot_Sales"];
			if (quantity < 0m)
			{
				DataRow[] array = dataTable.Select(" ItemCode='" + productID + "' AND InvoiceNumber='" + voucherNumber + "' AND DocID='" + docID + "' AND RowIndex = " + rowIndex);
				num = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num += decimal.Parse(array[i]["SoldQty"].ToString()) * decimal.Parse(array[i]["Cost"].ToString());
				}
				return Math.Abs(Math.Round(num, 5));
			}
			if (sysDocType == SysDocTypes.InventoryRepacking || (sysDocType == SysDocTypes.AssemblyBuild && quantity > 0m))
			{
				return CalculateFifoCOGS(productID, quantity, sysDocType, docID, voucherNumber, rowIndex, transactionDate, sqlTransaction);
			}
			if (quantity > 0m)
			{
				return CalculateFifoCOGS(productID, quantity, sysDocType, docID, voucherNumber, rowIndex, transactionDate, sqlTransaction);
			}
			return 0m;
		}

		internal decimal CalculateFifoCOGS(string productID, decimal quantity, SysDocTypes sysDocType, string docID, string voucherNumber, int rowIndex, DateTime transactionDate, SqlTransaction sqlTransaction)
		{
			decimal d = default(decimal);
			_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string text = "";
			string text2 = CommonLib.ToSqlDateTimeString(transactionDate);
			if (quantity < 0m)
			{
				text = "SELECT SUM(SoldQty*Cost)\r\n\t\t\t\t\t\t   FROM Product_Lot_Sales  WHERE ItemCode='" + productID + "' AND InvoiceNumber='" + voucherNumber + "' AND DocID='" + docID + "' AND RowIndex = " + rowIndex;
				object obj = ExecuteScalar(text, sqlTransaction);
				if (obj != null && obj.ToString() != "")
				{
					d = decimal.Parse(obj.ToString());
				}
				text = "SELECT SUM(ISNULL(Quantity,0))  FROM Unallocated_Lot_Items  WHERE ProductID='" + productID + "' AND VoucherID='" + voucherNumber + "' AND SysDocID='" + docID + "' AND RowIndex = " + rowIndex;
				obj = ExecuteScalar(text, sqlTransaction);
				decimal d2 = default(decimal);
				if (obj != null && obj.ToString() != "")
				{
					d2 = decimal.Parse(obj.ToString());
				}
				if (d2 != 0m)
				{
					text = " SELECT TOP 1 ISNULL(UnitPrice,0) AS Cost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate < '" + text2 + "' AND TransactionType IN(1,6,8,18) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate DESC,TransactionID DESC ";
					obj = ExecuteScalar(text, sqlTransaction);
					if (obj != null && obj.ToString() != "")
					{
						decimal d3 = decimal.Parse(obj.ToString());
						d += d2 * d3;
					}
				}
				return Math.Abs(Math.Round(d, 5));
			}
			if (sysDocType == SysDocTypes.InventoryRepacking || (sysDocType == SysDocTypes.AssemblyBuild && quantity > 0m))
			{
				decimal d4 = default(decimal);
				foreach (DataRow row in new InventoryTransaction(base.DBConfig).GetInventoryTransaction(docID, voucherNumber, sqlTransaction).InventoryTransactionTable.Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result);
					if (!(result >= 0m) && row["AssetValue"] != DBNull.Value)
					{
						d4 += Math.Abs(decimal.Parse(row["AssetValue"].ToString()));
					}
				}
				decimal d5 = default(decimal);
				string text3 = "";
				text3 = ((sysDocType != SysDocTypes.InventoryRepacking) ? ("SELECT DISTINCT\r\n\t\t\t\t\t\t\t\tISNULL((SELECT ABS(SUM(ISNULL(Amount,0))) FROM Assembly_Build_Expense WHERE SysDocID = '" + docID + "' AND VoucherID = '" + voucherNumber + "'),0) AS TotalCost\r\n\t\t\t\t\t\t\t\tFROM Assembly_Build_Expense") : ("SELECT DISTINCT\r\n\t\t\t\t\t\t\t\tISNULL((SELECT ABS(SUM(ISNULL(Amount,0))) FROM Inventory_Repacking_Expense WHERE SysDocID = '" + docID + "' AND VoucherID = '" + voucherNumber + "'),0) AS TotalCost\r\n\t\t\t\t\t\t\t\tFROM Inventory_Repacking_Expense"));
				object obj2 = ExecuteScalar(text3, sqlTransaction);
				if (obj2 != null && obj2.ToString() != "")
				{
					d5 = decimal.Parse(obj2.ToString());
				}
				return d5 + d4;
			}
			if (quantity > 0m)
			{
				string textCommand = "SELECT DocID,InvoiceNumber,SoldQty,PLS.Cost,TransactionDate FROM Product_Lot_Sales PLS INNER JOIN Inventory_Transactions IT ON  IT.SysDocID = PLS.DocID AND IT.VoucherID = PLS.InvoiceNumber AND IT.RowIndex = PLS.RowIndex\r\n\t\t\t\t\t\t\t\t\t\t\t WHERE ItemCode = '" + productID + "' AND TransactionType = 2 AND IT.TransactionDate < '" + text2 + "' \r\n\t\t\t\t\t\t\t\t\t\t\t GROUP BY DocID,InvoiceNumber,SoldQty,PLS.Cost ,TransactionDate\r\n\t\t\t\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\t\t\t\t\t ORDER BY TransactionDate DESC";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Lot", textCommand, sqlTransaction);
				decimal result2 = default(decimal);
				decimal num = quantity;
				{
					foreach (DataRow row2 in dataSet.Tables["Lot"].Rows)
					{
						decimal num2 = decimal.Parse(row2["SoldQty"].ToString());
						d = decimal.Parse(row2["Cost"].ToString());
						if (num2 > num)
						{
							result2 += num * d;
							num = default(decimal);
						}
						else
						{
							result2 += num2 * d;
							num -= num2;
						}
						if (num == 0m)
						{
							return result2;
						}
					}
					return result2;
				}
			}
			return 0m;
		}

		internal decimal CalculateAssemblyCost(string productID, decimal quantity, SysDocTypes sysDocType, string docID, string voucherNumber, int rowIndex, DateTime transactionDate, SqlTransaction sqlTransaction)
		{
			CommonLib.ToSqlDateTimeString(transactionDate);
			if (sysDocType == SysDocTypes.InventoryRepacking || (sysDocType == SysDocTypes.AssemblyBuild && quantity > 0m))
			{
				decimal d = default(decimal);
				foreach (DataRow row in new InventoryTransaction(base.DBConfig).GetInventoryTransaction(docID, voucherNumber, sqlTransaction).InventoryTransactionTable.Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result);
					if (!(result >= 0m) && row["AssetValue"] != DBNull.Value)
					{
						d += Math.Abs(decimal.Parse(row["AssetValue"].ToString()));
					}
				}
				decimal d2 = default(decimal);
				string text = "";
				text = ((sysDocType != SysDocTypes.InventoryRepacking) ? ("SELECT DISTINCT\r\n\t\t\t\t\t\t\t\tISNULL((SELECT ABS(SUM(ISNULL(Amount,0))) FROM Assembly_Build_Expense WHERE SysDocID = '" + docID + "' AND VoucherID = '" + voucherNumber + "'),0) AS TotalCost\r\n\t\t\t\t\t\t\t\tFROM Assembly_Build_Expense") : ("SELECT DISTINCT\r\n\t\t\t\t\t\t\t\tISNULL((SELECT ABS(SUM(ISNULL(Amount,0))) FROM Inventory_Repacking_Expense WHERE SysDocID = '" + docID + "' AND VoucherID = '" + voucherNumber + "'),0) AS TotalCost\r\n\t\t\t\t\t\t\t\tFROM Inventory_Repacking_Expense"));
				object obj = ExecuteScalar(text, sqlTransaction);
				if (obj != null && obj.ToString() != "")
				{
					d2 = decimal.Parse(obj.ToString());
				}
				return d2 + d;
			}
			return 0m;
		}

		internal bool SetRowCosting(ref DataSet itData, ref DataSet lotData, ArrayList productIDList, string effectingSysDocID, string effectingVoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				bool isLocationCosting = new CompanyInformations(base.DBConfig).GetIsLocationCosting(sqlTransaction);
				for (int i = 0; i < productIDList.Count; i++)
				{
					string text = productIDList[i].ToString();
					if (text == "FPO-VAR67-XAG025")
					{
						text = text;
					}
					if (itData == null || itData.Tables.Count == 0)
					{
						break;
					}
					DataRow[] itRows = itData.Tables[0].Select("ProductID = '" + text + "'", "TransactionDate ASC,TransactionID ASC");
					if (itRows.Length != 0)
					{
						new ServerTestTimer();
						for (int j = 0; j < itRows.Length; j++)
						{
							DataRow itRow = itRows[j];
							DataRow prevRow = null;
							string b = itRows[j]["LocationID"].ToString();
							if (!isLocationCosting)
							{
								int num = j;
								while (num > 0)
								{
									num--;
									if (int.Parse(itRows[num]["SysDocType"].ToString()) != 95)
									{
										prevRow = itRows[num];
										break;
									}
								}
							}
							else
							{
								int num2 = j;
								while (num2 > 0)
								{
									num2--;
									if (itRows[num2]["LocationID"].ToString() == b && int.Parse(itRows[num2]["SysDocType"].ToString()) != 95)
									{
										prevRow = itRows[num2];
										break;
									}
								}
							}
							decimal result = default(decimal);
							decimal.TryParse(itRow["Quantity"].ToString(), out result);
							if (!itRow["RowNumber"].IsDBNullOrEmpty())
							{
								int.Parse(itRow["RowNumber"].ToString());
							}
							string text2 = itRow["VoucherID"].ToString();
							string text3 = itRow["SysDocID"].ToString();
							int num3 = int.Parse(itRow["RowIndex"].ToString());
							if (new Products(base.DBConfig).GetCostMethod(text, sqlTransaction) != CostMethods.Average)
							{
								throw new CompanyException("Fifo costing is not implemented. Please use Average Cost method.");
							}
							SetAvgCostIndividualITRowCosting(text, ref itRow, prevRow, ref itData, null, ref itRows, ref lotData, j, isNewRow: false, sqlTransaction);
							if (result > 0m)
							{
								DataRow[] array = lotData.Tables["Product_Lot"].Select("DocID = '" + text3 + "' AND ReceiptNumber = '" + text2 + "' AND RowIndex = " + num3);
								if (array.Length != 0)
								{
									decimal num4 = default(decimal);
									if (!itRow["AssetValue"].IsDBNullOrEmpty())
									{
										decimal.Parse(itRow["AssetValue"].ToString());
									}
									num4 = Math.Abs(Math.Round(0m / result, 5));
									for (int k = 0; k < array.Length; k++)
									{
										decimal d = default(decimal);
										if (!array[k]["Cost"].IsDBNullOrEmpty())
										{
											d = decimal.Parse(array[k]["Cost"].ToString());
										}
										if (num4 != d)
										{
											array[k]["Cost"] = num4;
											string str = array[k]["LotNumber"].ToString();
											array = lotData.Tables["Product_Lot_Sales"].Select("LotNo = " + str);
											for (int l = 0; l < array.Length; l++)
											{
												array[l]["Cost"] = num4;
											}
										}
									}
								}
							}
						}
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		internal bool SetAvgCostIndividualITRowCosting(string productID, ref DataRow itRow, DataRow prevRow, ref DataSet itData, DataSet newItData, ref DataRow[] itRows, ref DataSet lotData, int itRowIndex, bool isNewRow, SqlTransaction sqlTransaction)
		{
			try
			{
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				bool isLocationCosting = new CompanyInformations(base.DBConfig).GetIsLocationCosting(sqlTransaction);
				object obj = null;
				string text = "";
				int transactionID = -1;
				decimal d = default(decimal);
				decimal num = default(decimal);
				CostMethods costMethods = CostMethods.Average;
				int currentRowNumber = 0;
				string locationID = itRow["LocationID"].ToString();
				new ServerTestTimer().Start();
				DataRow[] array = itRows;
				SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(itRow["SysDocType"].ToString());
				string voucherNumber = itRow["VoucherID"].ToString();
				string sysDocID = itRow["SysDocID"].ToString();
				int num2 = int.Parse(itRow["RowIndex"].ToString());
				int num3 = 0;
				if (!itRow["RefTransactionID"].IsDBNullOrEmpty())
				{
					num3 = int.Parse(itRow["RefTransactionID"].ToString());
				}
				DateTime transactionDate = DateTime.Parse(itRow["TransactionDate"].ToString());
				decimal quantity = default(decimal);
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				if (!itRow["ReturnedQuantity"].IsDBNullOrEmpty())
				{
					decimal.TryParse(itRow["ReturnedQuantity"].ToString(), out result2);
				}
				decimal.TryParse(itRow["Quantity"].ToString(), out quantity);
				decimal.TryParse(itRow["AssetValue"].ToString(), out result);
				if (itRow.Table.Columns.Contains("RowNumber"))
				{
					currentRowNumber = int.Parse(itRow["RowNumber"].ToString());
				}
				decimal num4 = default(decimal);
				bool flag = false;
				if (itRow["IsNonCostedGRN"] != DBNull.Value)
				{
					flag = bool.Parse(itRow["IsNonCostedGRN"].ToString());
				}
				decimal result3 = default(decimal);
				decimal result4 = default(decimal);
				decimal result5 = default(decimal);
				int result6 = 0;
				if (prevRow != null)
				{
					decimal.TryParse(prevRow["TotalQuantity"].ToString(), out result3);
					decimal.TryParse(prevRow["TotalValue"].ToString(), out result4);
					decimal.TryParse(prevRow["AverageCost"].ToString(), out result5);
					int.TryParse(prevRow["SysDocType"].ToString(), out result6);
					if (result3 == 0m)
					{
						result4 = default(decimal);
					}
					if (isNewRow)
					{
						if (isLocationCosting)
						{
							//from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							//	where v.Field<string>("ProductID") == productID && v.Field<int>("SysDocType") != 95 && v.Field<string>("LocationID") == locationID && v.Field<DateTime>("TransactionDate") < transactionDate
							//	orderby v.Field<long>("RowNumber") descending
							//	select v;
						}
						else
						{
							//from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							//	where v.Field<string>("ProductID") == productID && v.Field<int>("SysDocType") != 95 && v.Field<DateTime>("TransactionDate") < transactionDate
							//	orderby v.Field<long>("RowNumber") descending
							//	select v;
						}
					}
					else if (isLocationCosting)
					{
						//from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
						//	where v.Field<string>("ProductID") == productID && v.Field<int>("SysDocType") != 95 && v.Field<string>("LocationID") == locationID && v.Field<long>("RowNumber") < currentRowNumber
						//	orderby v.Field<long>("RowNumber") descending
						//	select v;
					}
					else
					{ 
						//from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
						//	where v.Field<string>("ProductID") == productID && v.Field<int>("SysDocType") != 95 && v.Field<long>("RowNumber") < currentRowNumber
						//	orderby v.Field<long>("RowNumber") descending
						//	select v;
					}
				}
				else
				{
					if (isNewRow)
					{
						result3 = quantity - result2;
						result4 = quantity * num4;
						result5 = num4;
					}
					else
					{
						decimal.TryParse(itRow["TotalQuantity"].ToString(), out result3);
						decimal.TryParse(itRow["TotalValue"].ToString(), out result4);
						decimal.TryParse(itRow["AverageCost"].ToString(), out result5);
						if (result3 == 0m)
						{
							result4 = default(decimal);
						}
						if (sysDocTypes == SysDocTypes.ConsignOut || sysDocTypes == SysDocTypes.CashSalesReturn || sysDocTypes == SysDocTypes.CreditSalesReturn)
						{
							OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection = null;
							orderedEnumerableRowCollection = ((!isLocationCosting) ? (from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && v.Field<int>("SysDocType") != 95 && (v.Field<byte>("TransactionType") == 1 || v.Field<byte>("TransactionType") == 18 || v.Field<byte>("TransactionType") == 18 || v.Field<byte>("TransactionType") == 8) && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
								orderby v.Field<long>("RowNumber")
								select v) : (from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && v.Field<string>("LocationID") == locationID && v.Field<int>("SysDocType") != 95 && (v.Field<byte>("TransactionType") == 1 || v.Field<byte>("TransactionType") == 18 || v.Field<byte>("TransactionType") == 18 || v.Field<byte>("TransactionType") == 8) && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
								orderby v.Field<long>("RowNumber")
								select v));
							if (orderedEnumerableRowCollection != null && orderedEnumerableRowCollection.Count() > 0)
							{
								object obj2 = orderedEnumerableRowCollection.First().Field<object>("AverageCost");
								if (!obj2.IsNullOrEmpty())
								{
									result5 = decimal.Parse(obj2.ToString());
								}
							}
						}
						result3 -= quantity - result2;
						result4 -= result;
					}
					int.TryParse(itRow["SysDocType"].ToString(), out result6);
				}
				if (!isNewRow)
				{
					transactionID = int.Parse(itRow["TransactionID"].ToString());
				}
				if (voucherNumber == "DL1285")
				{
					voucherNumber = voucherNumber;
				}
				if (voucherNumber == "FC158813")
				{
					voucherNumber = voucherNumber;
				}
				if (transactionID == 3930)
				{
					transactionID = transactionID;
				}
				if (transactionID == 1842733)
				{
					transactionID = transactionID;
				}
				if (num4 > num4)
				{
					throw new Exception();
				}
				if (itRow["UnitPrice"] != DBNull.Value)
				{
					num4 = Math.Abs(decimal.Parse(itRow["UnitPrice"].ToString()));
				}
				string text2 = StoreConfiguration.ToSqlDateTimeString(transactionDate);
				InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)int.Parse(itRow["TransactionType"].ToString());
				if ((inventoryTransactionTypes == InventoryTransactionTypes.Adjustment && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.Assembly && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.OpeningInventory && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.CustomerCreditMemo && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.Purchase && quantity > 0m) || ((inventoryTransactionTypes == InventoryTransactionTypes.Transfer && sysDocTypes != SysDocTypes.TransitTransferOut && quantity > 0m) & isLocationCosting) || (inventoryTransactionTypes == InventoryTransactionTypes.ConsignOut && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.Production && quantity > 0m))
				{
					decimal num5 = Math.Round((quantity - result2) * num4, 5);
					result = num5;
					switch (inventoryTransactionTypes)
					{
					case InventoryTransactionTypes.Assembly:
						if (isNewRow)
						{
							EnumerableRowCollection<DataRow> enumerableRowCollection = from v in newItData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<decimal>("Quantity") < 0m
								select v;
							num5 = default(decimal);
							foreach (DataRow item in enumerableRowCollection)
							{
								num5 += decimal.Parse(item["AssetValue"].ToString());
							}
							decimal num6 = default(decimal);
							if (itRow["Cost"] != DBNull.Value)
							{
								num6 = decimal.Parse(itRow["Cost"].ToString());
							}
							num5 += num6;
							num4 = Math.Round(num5 / quantity, 5, MidpointRounding.AwayFromZero);
							itRow["UnitPrice"] = num4;
							itRow["AssetValue"] = num5;
						}
						else
						{
							num5 = CalculateAssemblyCost(productID, quantity, sysDocTypes, sysDocID, voucherNumber, num2, transactionDate, sqlTransaction);
							num4 = Math.Round(num5 / quantity, 5, MidpointRounding.AwayFromZero);
							itRow["UnitPrice"] = num4;
						}
						break;
					case InventoryTransactionTypes.Transfer:
						if (isLocationCosting)
						{
							OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection3 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<int>("TransactionID") == transactionID - 1
								orderby v.Field<long>("RowNumber")
								select v;
							if (orderedEnumerableRowCollection3 != null && orderedEnumerableRowCollection3.Count() > 0)
							{
								object obj5 = orderedEnumerableRowCollection3.First().Field<object>("AverageCost");
								if (!obj5.IsNullOrEmpty())
								{
									obj = decimal.Parse(obj5.ToString());
								}
							}
							else
							{
								orderedEnumerableRowCollection3 = from v in newItData.Tables["Inventory_Transactions"].AsEnumerable()
									where v.Field<string>("ProductID") == productID && v.Field<decimal>("Quantity") == -1m * quantity && v.Field<string>("VoucherID") == voucherNumber && v.Field<string>("SysDocID") == sysDocID
									orderby v.Field<string>("VoucherID")
									select v;
								if (orderedEnumerableRowCollection3 != null && orderedEnumerableRowCollection3.Count() > 0)
								{
									object obj6 = orderedEnumerableRowCollection3.First().Field<object>("AverageCost");
									if (!obj6.IsNullOrEmpty())
									{
										obj = decimal.Parse(obj6.ToString());
									}
								}
							}
							if (obj != null)
							{
								num4 = decimal.Parse(obj.ToString());
							}
							num5 = decimal.Parse(obj.ToString()) * quantity;
							itRow["AssetValue"] = num5;
							itRow["UnitPrice"] = num4;
						}
						break;
					case InventoryTransactionTypes.ConsignOut:
					{
						text = "SELECT LocationID,SourceSysDocID,SourceVoucherID,SourceRowIndex FROM Consign_Out_Detail WHERE ProductID = '" + productID + "' AND  SysDocID = '" + sysDocID + "' AND  VoucherID = '" + voucherNumber + "'";
						DataSet dataSet = new DataSet();
						FillDataSet(dataSet, "Consign", text, sqlTransaction).ToString();
						string sourceLocID = "";
						string sourceSysDocID = "";
						string sourceVoucherID = "";
						int sourceRowIndex = -1;
						if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
						{
							sourceLocID = dataSet.Tables[0].Rows[0]["LocationID"].ToString();
							sourceSysDocID = dataSet.Tables[0].Rows[0]["SourceSysDocID"].ToString();
							sourceVoucherID = dataSet.Tables[0].Rows[0]["SourceVoucherID"].ToString();
							sourceRowIndex = int.Parse(dataSet.Tables[0].Rows[0]["SourceRowIndex"].ToString());
						}
						OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection4 = null;
						orderedEnumerableRowCollection4 = ((!isLocationCosting) ? (from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							where v.Field<string>("ProductID") == productID && v.Field<string>("SysDocID") == sourceSysDocID && v.Field<string>("VoucherID") == sourceVoucherID && v.Field<int>("RowIndex") == sourceRowIndex
							orderby v.Field<long>("RowNumber") descending
							select v) : (from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							where v.Field<string>("ProductID") == productID && v.Field<string>("LocationID") == sourceLocID && v.Field<int>("TransactionID") != transactionID && v.Field<DateTime>("TransactionDate") <= transactionDate && v.Field<string>("SysDocID") == sourceSysDocID && v.Field<string>("VoucherID") == sourceVoucherID
							orderby v.Field<long>("RowNumber") descending
							select v));
						if (orderedEnumerableRowCollection4.Count() > 0)
						{
							num4 = orderedEnumerableRowCollection4.First().Field<decimal>("AverageCost");
							obj = num4;
							num5 = decimal.Parse(obj.ToString()) * quantity;
							itRow["AssetValue"] = num5;
							itRow["UnitPrice"] = num4;
						}
						else
						{
							text = "SELECT TOP 1 AverageCost FROM Inventory_Transactions IT WHERE ProductID = '" + productID + "' AND SysDocID = '" + sourceSysDocID + "' AND VoucherID = '" + sourceVoucherID + "' AND RowIndex = " + sourceRowIndex + " ORDER BY TransactionDate DESC";
							obj = ExecuteScalar(text, sqlTransaction);
							if (obj.IsNullOrEmpty())
							{
								obj = 0;
							}
							num4 = decimal.Parse(obj.ToString());
							num5 = num4 * quantity;
							itRow["AssetValue"] = num5;
							itRow["UnitPrice"] = num4;
						}
						break;
					}
					case InventoryTransactionTypes.CustomerCreditMemo:
					{
						decimal d2 = result5;
						if (num3 == 0)
						{
							OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection2 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") < currentRowNumber && v.Field<byte>("TransactionType") == 2
								orderby v.Field<long>("RowNumber") descending
								select v;
							if (orderedEnumerableRowCollection2 != null && orderedEnumerableRowCollection2.Count() > 0)
							{
								object obj3 = orderedEnumerableRowCollection2.First().Field<object>("AverageCost");
								if (!obj3.IsDBNullOrEmpty())
								{
									d2 = decimal.Parse(obj3.ToString());
								}
							}
							else
							{
								orderedEnumerableRowCollection2 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
									where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
									orderby v.Field<long>("RowNumber")
									select v;
								if (orderedEnumerableRowCollection2 != null && orderedEnumerableRowCollection2.Count() > 0)
								{
									object obj4 = orderedEnumerableRowCollection2.First().Field<object>("AverageCost");
									if (!obj4.IsDBNullOrEmpty())
									{
										d2 = decimal.Parse(obj4.ToString());
									}
								}
							}
							num5 = Math.Round(Math.Abs(quantity * d2), 5, MidpointRounding.AwayFromZero);
							num4 = Math.Round(num5 / quantity, 5, MidpointRounding.AwayFromZero);
							itRow["AssetValue"] = num5;
						}
						else
						{
							text = "SELECT TOP 1 AverageCost FROM Inventory_Transactions IT WHERE TransactionID = " + num3;
							obj = ExecuteScalar(text, sqlTransaction);
							if (obj.IsNullOrEmpty())
							{
								obj = result5;
							}
							num4 = decimal.Parse(obj.ToString());
							num5 = num4 * quantity;
							itRow["AssetValue"] = num5;
						}
						break;
					}
					}
					result = num5;
					if (flag)
					{
						decimal num7 = default(decimal);
						if (costMethods == CostMethods.FIFO)
						{
							text = " SELECT TOP 1 ISNULL(UnitPrice,0) AS Cost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate < '" + text2 + "' AND TransactionType IN(1,6,8,18) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate DESC,TransactionID DESC ";
						}
						else if (itRowIndex != 0)
						{
							DataRow[] array2 = itData.Tables["Inventory_Transactions"].Select("ProductID = '" + productID + "' AND  TransactionType IN (1) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 AND RowNumber < " + currentRowNumber, "RowNumber DESC");
							num7 = ((array2.Length == 0 || !(quantity - result2 > 0m)) ? result5 : decimal.Parse(array2[0]["UnitPrice"].ToString()));
						}
						else
						{
							text = " SELECT TOP 1 ISNULL(UnitPrice,0) AS Cost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate < '" + text2 + "' AND TransactionType IN(1) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate DESC,TransactionID DESC ";
							obj = ExecuteScalar(text, sqlTransaction);
							if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
							{
								num7 = decimal.Parse(obj.ToString());
							}
						}
						num5 = Math.Round(num7 * (quantity - result2), 5);
						result = num5;
						decimal num8 = Math.Abs(num7);
						itRow["AverageCost"] = num7;
						if (itRow["UnitPrice"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["UnitPrice"].ToString());
						}
						if (d != num8)
						{
							itRow["UnitPrice"] = num8;
						}
						itRow["AssetValue"] = num5;
					}
					if (result3 > 0m && result4 >= 0m)
					{
						num = ((!(result3 + quantity != 0m)) ? num4 : Math.Round((result4 + num5) / (result3 + (quantity - result2)), 5));
					}
					else if (result3 < 0m)
					{
						if (costMethods != CostMethods.FIFO)
						{
							OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection5 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") < currentRowNumber
								orderby v.Field<long>("RowNumber") descending
								select v;
							int num9 = -1;
							if (orderedEnumerableRowCollection5 != null && orderedEnumerableRowCollection5.Count() > 0)
							{
								num9 = (int)orderedEnumerableRowCollection5.First().Field<long>("RowNumber");
							}
							DataRow[] array3 = itData.Tables["Inventory_Transactions"].Select("ProductID = '" + productID + "' AND  TransactionType IN (1,6,8,18) AND Quantity-ISNULL(ReturnedQuantity,0)>0 AND RowNumber> " + num9 + " AND  RowNumber < " + currentRowNumber, "RowNumber ASC");
							if (array3.Length != 0 || orderedEnumerableRowCollection5.Count() > 0)
							{
								decimal result7 = default(decimal);
								decimal result8 = default(decimal);
								if (result7 == 0m)
								{
									result8 = default(decimal);
								}
								if (orderedEnumerableRowCollection5.Count() > 0)
								{
									DataRow dataRow = orderedEnumerableRowCollection5.First();
									decimal.TryParse(dataRow["TotalQuantity"].ToString(), out result7);
									decimal.TryParse(dataRow["TotalValue"].ToString(), out result8);
								}
								decimal num10 = default(decimal);
								decimal num11 = default(decimal);
								decimal num12 = default(decimal);
								decimal num13 = default(decimal);
								decimal num14 = default(decimal);
								num13 = result8;
								num14 = result7;
								for (int i = 0; i < array3.Length; i++)
								{
									num10 = default(decimal);
									num11 = default(decimal);
									num12 = default(decimal);
									if (!array3[i]["Quantity"].IsDBNullOrEmpty())
									{
										num10 = decimal.Parse(array3[i]["Quantity"].ToString());
									}
									if (!array3[i]["AssetValue"].IsDBNullOrEmpty())
									{
										num12 = decimal.Parse(array3[i]["AssetValue"].ToString());
									}
									if (!array3[i]["ReturnedQuantity"].IsDBNullOrEmpty())
									{
										num11 = decimal.Parse(array3[i]["ReturnedQuantity"].ToString());
									}
									num14 += num10 - num11;
									num13 += num12;
								}
								if (result3 == 0m)
								{
									result4 = default(decimal);
								}
								if (num14 + (quantity - result2) != 0m)
								{
									num = Math.Abs(Math.Round((num13 + num5) / (num14 + (quantity - result2)), 5));
								}
							}
							else
							{
								num = num4;
							}
							int num15 = 0;
							for (int num16 = itRowIndex - 1; num16 >= 0; num16--)
							{
								decimal result9 = default(decimal);
								decimal.TryParse(array[num16]["TotalQuantity"].ToString(), out result9);
								if (!(result9 < 0m))
								{
									break;
								}
								num15 = num16;
							}
							decimal num17 = default(decimal);
							for (int j = num15; j < itRowIndex; j++)
							{
								decimal result10 = default(decimal);
								decimal result11 = default(decimal);
								decimal result12 = default(decimal);
								decimal result13 = default(decimal);
								int.Parse(array[j]["TransactionID"].ToString());
								decimal.TryParse(array[j]["Quantity"].ToString(), out result10);
								decimal.TryParse(array[j]["AssetValue"].ToString(), out result11);
								decimal.TryParse(array[j]["AverageCost"].ToString(), out result12);
								decimal.TryParse(array[j]["TotalValue"].ToString(), out result13);
								int num18 = int.Parse(array[j]["TransactionType"].ToString());
								int num19 = int.Parse(array[j]["SysDocType"].ToString());
								if (!array[j]["IsNonCostedGRN"].IsDBNullOrEmpty())
								{
									bool.Parse(array[j]["IsNonCostedGRN"].ToString());
								}
								if (num19 == 95)
								{
									array[j]["AverageCost"] = result5;
								}
								else
								{
									array[j]["AverageCost"] = num;
								}
								if (num19 != 95 && num19 != 29 && num19 != 32 && num19 != 34 && num19 != 29 && (result10 < 0m || (result10 > 0m && num18 != 1 && num18 != 6 && num18 != 8 && num18 != 18 && num18 != 3)))
								{
									array[j]["AssetValue"] = Math.Round(result10 * num, 5, MidpointRounding.AwayFromZero);
									num17 += Math.Round(result10 * num, 5) - result11;
								}
								array[j]["TotalValue"] = Math.Round(result13 + num17, 5);
							}
							result4 += Math.Round(num17, 5);
						}
					}
					else
					{
						num = num4;
					}
					if (itRow["AverageCost"] != DBNull.Value)
					{
						d = decimal.Parse(itRow["AverageCost"].ToString());
					}
					if (d != Math.Abs(num))
					{
						itRow["AverageCost"] = Math.Abs(num);
					}
					if (itRow["AssetValue"] != DBNull.Value)
					{
						d = decimal.Parse(itRow["AssetValue"].ToString());
					}
					if (d != num5)
					{
						itRow["AssetValue"] = Math.Round(num5, 5, MidpointRounding.AwayFromZero);
					}
				}
				else if ((inventoryTransactionTypes == InventoryTransactionTypes.Transfer && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.ConsignOut && quantity > 0m) || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryReturn || inventoryTransactionTypes == InventoryTransactionTypes.ConsignOutReturn)
				{
					if ((sysDocTypes == SysDocTypes.TransitTransferIn && !isLocationCosting) || sysDocTypes == SysDocTypes.ReturnTransitTransfer)
					{
						obj = 0;
						num = result5;
						obj = result5;
						if (obj != null && obj.ToString() != "")
						{
							decimal num20 = Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero);
							result = num20;
							if (itRow["AssetValue"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["AssetValue"].ToString());
							}
							if (d != num20)
							{
								itRow["AssetValue"] = num20;
							}
							num20 = Math.Round(decimal.Parse(obj.ToString()), 5);
							if (itRow["AverageCost"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["AverageCost"].ToString());
							}
							if (d != num20)
							{
								itRow["AverageCost"] = num20;
							}
						}
						if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
						{
							result = (num = decimal.Parse(obj.ToString())) * quantity;
							if (itRow["UnitPrice"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["UnitPrice"].ToString());
							}
							if (d != num)
							{
								itRow["UnitPrice"] = num;
							}
						}
					}
					else if (sysDocTypes == SysDocTypes.TransitTransferOut && quantity > 0m)
					{
						obj = result5;
						if (isLocationCosting)
						{
							OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection6 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<int>("TransactionID") == transactionID - 1
								orderby v.Field<long>("RowNumber")
								select v;
							if (orderedEnumerableRowCollection6 != null && orderedEnumerableRowCollection6.Count() > 0)
							{
								object obj7 = orderedEnumerableRowCollection6.First().Field<object>("AverageCost");
								if (!obj7.IsNullOrEmpty())
								{
									obj = decimal.Parse(obj7.ToString());
								}
							}
						}
						decimal num21 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero));
						if (itRow["AssetValue"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AssetValue"].ToString());
						}
						if (d != num21)
						{
							itRow["AssetValue"] = num21;
						}
						result = num21;
						num21 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), 5));
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != num21)
						{
							itRow["AverageCost"] = num21;
						}
						itRow["UnitPrice"] = num21;
					}
					else if (sysDocTypes == SysDocTypes.DirectInventoryTransfer && quantity > 0m)
					{
						obj = result5;
						decimal num22 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero));
						if (itRow["AssetValue"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AssetValue"].ToString());
						}
						if (d != num22)
						{
							itRow["AssetValue"] = num22;
						}
						result = num22;
						num22 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), 5));
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != num22)
						{
							itRow["AverageCost"] = num22;
						}
						itRow["UnitPrice"] = num22;
					}
					else if (sysDocTypes == SysDocTypes.DeliveryReturn)
					{
						obj = result5;
						if (result3 < 0m)
						{
							OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection7 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") < currentRowNumber && v.Field<byte>("TransactionType") == 2
								orderby v.Field<long>("RowNumber") descending
								select v;
							if (orderedEnumerableRowCollection7 != null && orderedEnumerableRowCollection7.Count() > 0)
							{
								object obj8 = orderedEnumerableRowCollection7.First().Field<object>("AverageCost");
								if (!obj8.IsNullOrEmpty())
								{
									obj = decimal.Parse(obj8.ToString());
								}
							}
							else
							{
								orderedEnumerableRowCollection7 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
									where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
									orderby v.Field<long>("RowNumber")
									select v;
								if (orderedEnumerableRowCollection7 != null && orderedEnumerableRowCollection7.Count() > 0)
								{
									object obj9 = orderedEnumerableRowCollection7.First().Field<object>("AverageCost");
									if (!obj9.IsNullOrEmpty())
									{
										obj = decimal.Parse(obj9.ToString());
									}
								}
							}
						}
						decimal num23 = Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero);
						if (itRow["AssetValue"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AssetValue"].ToString());
						}
						if (d != num23)
						{
							itRow["AssetValue"] = num23;
						}
						result = num23;
						num23 = Math.Round(decimal.Parse(obj.ToString()), 5);
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != num23)
						{
							itRow["AverageCost"] = num23;
						}
						if (itRow["UnitPrice"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["UnitPrice"].ToString());
						}
						if (d != num23)
						{
							itRow["UnitPrice"] = num23;
						}
					}
					else
					{
						decimal d3 = result5;
						OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection8 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") < currentRowNumber && v.Field<byte>("TransactionType") == 2
							orderby v.Field<long>("RowNumber") descending
							select v;
						if (orderedEnumerableRowCollection8 != null && orderedEnumerableRowCollection8.Count() > 0)
						{
							object obj10 = orderedEnumerableRowCollection8.First().Field<object>("AverageCost");
							if (!obj10.IsNullOrEmpty())
							{
								d3 = decimal.Parse(obj10.ToString());
							}
						}
						else
						{
							orderedEnumerableRowCollection8 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
								orderby v.Field<long>("RowNumber")
								select v;
							if (orderedEnumerableRowCollection8 != null && orderedEnumerableRowCollection8.Count() > 0)
							{
								object obj11 = orderedEnumerableRowCollection8.First().Field<object>("AverageCost");
								if (!obj11.IsNullOrEmpty())
								{
									d3 = decimal.Parse(obj11.ToString());
								}
							}
						}
						decimal num24 = Math.Round(Math.Abs(quantity * d3), 5, MidpointRounding.AwayFromZero);
						if (quantity < 0m)
						{
							num24 = -1m * num24;
						}
						result = num24;
						if (itRow["AssetValue"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AssetValue"].ToString());
						}
						if (d != num24)
						{
							itRow["AssetValue"] = num24;
						}
						if (quantity != 0m)
						{
							if (itRow["AverageCost"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["AverageCost"].ToString());
							}
							if (d != Math.Abs(result5))
							{
								itRow["AverageCost"] = result5;
							}
						}
						else
						{
							if (itRow["AverageCost"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["AverageCost"].ToString());
							}
							if (d != 0m)
							{
								itRow["AverageCost"] = 0;
							}
						}
					}
				}
				else if ((sysDocTypes == SysDocTypes.TransitTransferOut || sysDocTypes == SysDocTypes.ReturnTransitTransfer || sysDocTypes == SysDocTypes.TransitTransferIn || sysDocTypes == SysDocTypes.DirectInventoryTransfer) && quantity < 0m)
				{
					obj = result5;
					if (prevRow == null)
					{
						OrderedEnumerableRowCollection<DataRow> source = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							where v.Field<string>("ProductID") == productID && v.Field<DateTime>("TransactionDate") > transactionDate && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber + 1 && (v.Field<byte>("TransactionType") == 1 || v.Field<byte>("TransactionType") == 6 || v.Field<byte>("TransactionType") == 8 || v.Field<byte>("TransactionType") == 18)
							orderby v.Field<DateTime>("TransactionDate") descending
							select v;
						if (source.Count() > 0)
						{
							DataRow dataRow2 = source.Last();
							if (dataRow2 != null && !dataRow2["AverageCost"].IsDBNullOrEmpty())
							{
								obj = decimal.Parse(dataRow2["AverageCost"].ToString());
							}
						}
						else
						{
							source = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && v.Field<DateTime>("TransactionDate") > transactionDate && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber + 1
								orderby v.Field<DateTime>("TransactionDate") descending
								select v;
							if (source.Count() > 0)
							{
								DataRow dataRow3 = source.Last();
								if (dataRow3 != null && !dataRow3["AverageCost"].IsDBNullOrEmpty())
								{
									obj = decimal.Parse(dataRow3["AverageCost"].ToString());
								}
							}
						}
					}
					if (obj != null && obj.ToString() != "")
					{
						decimal num25 = -1m * Math.Abs(Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero));
						result = num25;
						if (itRow["AssetValue"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AssetValue"].ToString());
						}
						if (d != num25)
						{
							itRow["AssetValue"] = num25;
						}
						decimal num26 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), 5));
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != num26)
						{
							itRow["AverageCost"] = num26;
						}
					}
				}
				else if (sysDocTypes == SysDocTypes.GRNReturn)
				{
					itRow["AssetValue"] = 0;
					itRow["AverageCost"] = result5;
					result = default(decimal);
				}
				else if (costMethods == CostMethods.FIFO)
				{
					decimal num27 = default(decimal);
					DataRow[] array4 = lotData.Tables["Product_Lot_Sales"].Select("DocNumber = '" + sysDocID + voucherNumber + "' AND RowIndex = " + num2);
					if (array4.Length != 0)
					{
						for (int k = 0; k < array4.Length; k++)
						{
							num27 += -1m * Math.Abs(decimal.Parse(array4[k]["COGS"].ToString()));
						}
					}
					result = num27;
					if (itRow["AssetValue"] != DBNull.Value)
					{
						d = decimal.Parse(itRow["AssetValue"].ToString());
					}
					if (d != num27)
					{
						itRow["AssetValue"] = Math.Round(num27, 5, MidpointRounding.AwayFromZero);
					}
					if (quantity != 0m)
					{
						decimal num28 = Math.Abs(Math.Round(num27 / Convert.ToDecimal(quantity), 5));
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != num28)
						{
							itRow["AverageCost"] = num28;
						}
					}
					else
					{
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != 0m)
						{
							itRow["AverageCost"] = 0;
						}
					}
				}
				else
				{
					bool flag2 = true;
					if (sysDocTypes != SysDocTypes.TransitTransferOut)
					{
						if (result3 + quantity < 0m)
						{
							flag2 = false;
						}
					}
					else if (result3 < 0m)
					{
						flag2 = false;
					}
					decimal num29 = result5;
					if (!flag2)
					{
						OrderedEnumerableRowCollection<DataRow> source2 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							where v.Field<string>("ProductID") == productID && v.Field<DateTime>("TransactionDate") > transactionDate && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<int>("SysDocType") != 47
							orderby v.Field<DateTime>("TransactionDate") descending
							select v;
						if (source2.Count() > 0)
						{
							DataRow dataRow4 = source2.Last();
							if (dataRow4 != null && !dataRow4["AverageCost"].IsDBNullOrEmpty())
							{
								num29 = decimal.Parse(dataRow4["AverageCost"].ToString());
							}
						}
					}
					decimal num30 = default(decimal);
					num30 = ((!(quantity < 0m)) ? Math.Round(Math.Abs(num29 * quantity), 5, MidpointRounding.AwayFromZero) : Math.Round(-1m * Math.Abs(num29 * quantity), 5, MidpointRounding.AwayFromZero));
					result = num30;
					if (itRow["AssetValue"] != DBNull.Value)
					{
						d = decimal.Parse(itRow["AssetValue"].ToString());
					}
					if (d == 0m || d != num30)
					{
						itRow["AssetValue"] = num30;
					}
					if (quantity != 0m)
					{
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d == 0m || d != num29)
						{
							itRow["AverageCost"] = Math.Abs(num29);
						}
					}
					else
					{
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						itRow["AverageCost"] = Math.Abs(num29);
					}
				}
				if (!isNewRow)
				{
					decimal num31 = Math.Round(result4, 5, MidpointRounding.AwayFromZero) + Math.Round(result, 5);
					if (itRow["TotalValue"] != DBNull.Value)
					{
						d = decimal.Parse(itRow["TotalValue"].ToString());
					}
					if (d != num31)
					{
						itRow["TotalValue"] = num31;
					}
					num31 = result3 + (quantity - result2);
					if (itRow["TotalQuantity"] != DBNull.Value)
					{
						d = decimal.Parse(itRow["TotalQuantity"].ToString());
					}
					if (d != num31)
					{
						itRow["TotalQuantity"] = num31;
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		internal bool SetFIFOIndividualITRowCosting(string productID, ref DataRow itRow, DataRow prevRow, ref DataSet itData, DataSet newItData, ref DataRow[] itRows, ref DataSet lotData, int itRowIndex, bool isNewRow, SqlTransaction sqlTransaction)
		{
			try
			{
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				bool isLocationCosting = new CompanyInformations(base.DBConfig).GetIsLocationCosting(sqlTransaction);
				object obj = null;
				string text = "";
				int transactionID = -1;
				decimal d = default(decimal);
				decimal num = default(decimal);
				int currentRowNumber = 0;
				string locationID = itRow["LocationID"].ToString();
				new ServerTestTimer().Start();
				SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(itRow["SysDocType"].ToString());
				string voucherNumber = itRow["VoucherID"].ToString();
				string sysDocID = itRow["SysDocID"].ToString();
				int num2 = int.Parse(itRow["RowIndex"].ToString());
				DateTime transactionDate = DateTime.Parse(itRow["TransactionDate"].ToString());
				decimal quantity = default(decimal);
				decimal result = default(decimal);
				decimal.TryParse(itRow["Quantity"].ToString(), out quantity);
				decimal.TryParse(itRow["AssetValue"].ToString(), out result);
				if (itRow.Table.Columns.Contains("RowNumber"))
				{
					currentRowNumber = int.Parse(itRow["RowNumber"].ToString());
				}
				decimal num3 = default(decimal);
				bool flag = false;
				if (itRow["IsNonCostedGRN"] != DBNull.Value)
				{
					flag = bool.Parse(itRow["IsNonCostedGRN"].ToString());
				}
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal result4 = default(decimal);
				int result5 = 0;
				if (prevRow != null)
				{
					decimal.TryParse(prevRow["TotalQuantity"].ToString(), out result2);
					decimal.TryParse(prevRow["TotalValue"].ToString(), out result3);
					decimal.TryParse(prevRow["AverageCost"].ToString(), out result4);
					int.TryParse(prevRow["SysDocType"].ToString(), out result5);
					if (result2 == 0m)
					{
						result3 = default(decimal);
					}
					if (isNewRow)
					{
						if (isLocationCosting)
						{
							//from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							//	where v.Field<string>("ProductID") == productID && v.Field<string>("LocationID") == locationID && v.Field<DateTime>("TransactionDate") < transactionDate
							//	orderby v.Field<long>("RowNumber") descending
							//	select v;
						}
						else
						{
							//from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							//	where v.Field<string>("ProductID") == productID && v.Field<DateTime>("TransactionDate") < transactionDate
							//	orderby v.Field<long>("RowNumber") descending
							//	select v;
						}
					}
					else if (isLocationCosting)
					{
					//	from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
					//		where v.Field<string>("ProductID") == productID && v.Field<string>("LocationID") == locationID && v.Field<long>("RowNumber") < currentRowNumber
					//		orderby v.Field<long>("RowNumber") descending
					//		select v;
					}
					else
					{
						//from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
						//	where v.Field<string>("ProductID") == productID && v.Field<long>("RowNumber") < currentRowNumber
						//	orderby v.Field<long>("RowNumber") descending
						//	select v;
					}
				}
				else
				{
					if (isNewRow)
					{
						result2 = quantity;
						result3 = quantity * num3;
						result4 = num3;
					}
					else
					{
						decimal.TryParse(itRow["TotalQuantity"].ToString(), out result2);
						decimal.TryParse(itRow["TotalValue"].ToString(), out result3);
						decimal.TryParse(itRow["AverageCost"].ToString(), out result4);
						if (result2 == 0m)
						{
							result3 = default(decimal);
						}
						if (sysDocTypes == SysDocTypes.ConsignOut || sysDocTypes == SysDocTypes.CashSalesReturn || sysDocTypes == SysDocTypes.CreditSalesReturn)
						{
							OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection = null;
							orderedEnumerableRowCollection = ((!isLocationCosting) ? (from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && (v.Field<byte>("TransactionType") == 1 || v.Field<byte>("TransactionType") == 18 || v.Field<byte>("TransactionType") == 18 || v.Field<byte>("TransactionType") == 8) && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
								orderby v.Field<long>("RowNumber")
								select v) : (from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && v.Field<string>("LocationID") == locationID && (v.Field<byte>("TransactionType") == 1 || v.Field<byte>("TransactionType") == 18 || v.Field<byte>("TransactionType") == 18 || v.Field<byte>("TransactionType") == 8) && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
								orderby v.Field<long>("RowNumber")
								select v));
							if (orderedEnumerableRowCollection != null && orderedEnumerableRowCollection.Count() > 0)
							{
								object obj2 = orderedEnumerableRowCollection.First().Field<object>("AverageCost");
								if (!obj2.IsNullOrEmpty())
								{
									result4 = decimal.Parse(obj2.ToString());
								}
							}
						}
						result2 -= quantity;
						result3 -= result;
					}
					int.TryParse(itRow["SysDocType"].ToString(), out result5);
				}
				if (!isNewRow)
				{
					transactionID = int.Parse(itRow["TransactionID"].ToString());
				}
				if (voucherNumber == "000742")
				{
					voucherNumber = voucherNumber;
				}
				if (transactionID == 140956)
				{
					transactionID = transactionID;
				}
				if (itRow["UnitPrice"] != DBNull.Value)
				{
					num3 = Math.Abs(decimal.Parse(itRow["UnitPrice"].ToString()));
				}
				string text2 = StoreConfiguration.ToSqlDateTimeString(transactionDate);
				InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)int.Parse(itRow["TransactionType"].ToString());
				if ((inventoryTransactionTypes == InventoryTransactionTypes.Adjustment && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.Assembly && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.OpeningInventory && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.CustomerCreditMemo && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.Purchase && quantity > 0m) || ((inventoryTransactionTypes == InventoryTransactionTypes.Transfer && sysDocTypes != SysDocTypes.TransitTransferOut && quantity > 0m) & isLocationCosting) || ((inventoryTransactionTypes == InventoryTransactionTypes.ConsignOut && quantity > 0m) & isLocationCosting))
				{
					decimal num4 = Math.Round(quantity * num3, 5);
					result = num4;
					num = num3;
					switch (inventoryTransactionTypes)
					{
					case InventoryTransactionTypes.Assembly:
						if (isNewRow)
						{
							EnumerableRowCollection<DataRow> enumerableRowCollection = from v in newItData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<decimal>("Quantity") < 0m
								select v;
							num4 = default(decimal);
							foreach (DataRow item in enumerableRowCollection)
							{
								num4 += decimal.Parse(item["AssetValue"].ToString());
							}
							decimal num5 = default(decimal);
							if (itRow["Cost"] != DBNull.Value)
							{
								num5 = decimal.Parse(itRow["Cost"].ToString());
							}
							num4 += num5;
							num3 = Math.Round(num4 / quantity, 5, MidpointRounding.AwayFromZero);
							itRow["UnitPrice"] = num3;
							itRow["AssetValue"] = num4;
						}
						else
						{
							num4 = CalculateAssemblyCost(productID, quantity, sysDocTypes, sysDocID, voucherNumber, num2, transactionDate, sqlTransaction);
							num3 = Math.Round(num4 / quantity, 5, MidpointRounding.AwayFromZero);
							itRow["UnitPrice"] = num3;
							itRow["AssetValue"] = num4;
						}
						break;
					case InventoryTransactionTypes.Transfer:
						if (isLocationCosting)
						{
							OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection2 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<int>("TransactionID") == transactionID - 1
								orderby v.Field<long>("RowNumber")
								select v;
							if (orderedEnumerableRowCollection2 != null && orderedEnumerableRowCollection2.Count() > 0)
							{
								object obj3 = orderedEnumerableRowCollection2.First().Field<object>("AverageCost");
								if (!obj3.IsNullOrEmpty())
								{
									obj = decimal.Parse(obj3.ToString());
								}
							}
							else
							{
								orderedEnumerableRowCollection2 = from v in newItData.Tables["Inventory_Transactions"].AsEnumerable()
									where v.Field<string>("ProductID") == productID && v.Field<decimal>("Quantity") == -1m * quantity && v.Field<string>("VoucherID") == voucherNumber && v.Field<string>("SysDocID") == sysDocID
									orderby v.Field<string>("VoucherID")
									select v;
								if (orderedEnumerableRowCollection2 != null && orderedEnumerableRowCollection2.Count() > 0)
								{
									object obj4 = orderedEnumerableRowCollection2.First().Field<object>("AverageCost");
									if (!obj4.IsNullOrEmpty())
									{
										obj = decimal.Parse(obj4.ToString());
									}
								}
							}
							if (obj != null)
							{
								num3 = decimal.Parse(obj.ToString());
							}
							num4 = decimal.Parse(obj.ToString()) * quantity;
							itRow["AssetValue"] = num4;
							itRow["UnitPrice"] = num3;
						}
						break;
					case InventoryTransactionTypes.ConsignOut:
					{
						text = "SELECT LocationID,SourceSysDocID,SourceVoucherID FROM Consign_Out_Detail WHERE ProductID = '" + productID + "' AND  SysDocID = '" + sysDocID + "' AND  VoucherID = '" + voucherNumber + "'";
						DataSet dataSet = new DataSet();
						FillDataSet(dataSet, "Consign", text, sqlTransaction).ToString();
						string sourceLocID = "";
						string sourceSysDocID = "";
						string sourceVoucherID = "";
						if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
						{
							sourceLocID = dataSet.Tables[0].Rows[0]["LocationID"].ToString();
							sourceSysDocID = dataSet.Tables[0].Rows[0]["SourceSysDocID"].ToString();
							sourceVoucherID = dataSet.Tables[0].Rows[0]["SourceVoucherID"].ToString();
						}
						OrderedEnumerableRowCollection<DataRow> source = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
							where v.Field<string>("ProductID") == productID && v.Field<string>("LocationID") == sourceLocID && v.Field<DateTime>("TransactionDate") <= transactionDate && v.Field<string>("SysDocID") == sourceSysDocID && v.Field<string>("VoucherID") == sourceVoucherID
							orderby v.Field<long>("RowNumber") descending
							select v;
						if (source.Count() > 0)
						{
							num3 = source.First().Field<decimal>("AverageCost");
							obj = num3;
							num4 = decimal.Parse(obj.ToString()) * quantity;
							itRow["AssetValue"] = num4;
							itRow["UnitPrice"] = num3;
						}
						break;
					}
					case InventoryTransactionTypes.CustomerCreditMemo:
						text = "SELECT SUM(LotQty*Cost) FROM Product_Lot WHERE DocID = '" + sysDocID + "' AND ReceiptNumber = '" + voucherNumber + "' AND ItemCode = '" + productID + "' AND RowIndex= " + num2;
						obj = ExecuteScalar(text, sqlTransaction);
						if (!obj.IsDBNullOrEmpty())
						{
							num4 = decimal.Parse(obj.ToString());
						}
						num3 = ((!(quantity != 0m)) ? default(decimal) : Math.Round(num4 / quantity, 5, MidpointRounding.AwayFromZero));
						itRow["AssetValue"] = num4;
						num = num3;
						break;
					}
					result = num4;
					if (flag)
					{
						decimal num6 = default(decimal);
						text = " SELECT TOP 1 ISNULL(UnitPrice,0) AS Cost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate < '" + text2 + "' AND TransactionType IN(1,6,8,18) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate DESC,TransactionID DESC ";
						obj = ExecuteScalar(text, sqlTransaction);
						if (!obj.IsDBNullOrEmpty())
						{
							num6 = decimal.Parse(obj.ToString());
						}
						num4 = Math.Round(num6 * quantity, 5);
						result = num4;
						itRow["AverageCost"] = num6;
						if (itRow["UnitPrice"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["UnitPrice"].ToString());
						}
						itRow["AssetValue"] = num4;
					}
					if (itRow["AverageCost"] != DBNull.Value)
					{
						d = decimal.Parse(itRow["AverageCost"].ToString());
					}
					if (d != Math.Abs(num))
					{
						itRow["AverageCost"] = Math.Abs(num);
					}
					if (itRow["AssetValue"] != DBNull.Value)
					{
						d = decimal.Parse(itRow["AssetValue"].ToString());
					}
					if (d != num4)
					{
						itRow["AssetValue"] = Math.Round(num4, 5, MidpointRounding.AwayFromZero);
					}
				}
				else if ((inventoryTransactionTypes == InventoryTransactionTypes.Transfer && quantity > 0m) || (inventoryTransactionTypes == InventoryTransactionTypes.ConsignOut && quantity > 0m) || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryReturn || inventoryTransactionTypes == InventoryTransactionTypes.ConsignOutReturn)
				{
					if ((sysDocTypes == SysDocTypes.TransitTransferIn && !isLocationCosting) || sysDocTypes == SysDocTypes.ReturnTransitTransfer)
					{
						obj = 0;
						num = result4;
						obj = result4;
						if (obj != null && obj.ToString() != "")
						{
							decimal num7 = Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero);
							result = num7;
							if (itRow["AssetValue"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["AssetValue"].ToString());
							}
							if (d != num7)
							{
								itRow["AssetValue"] = num7;
							}
							num7 = Math.Round(decimal.Parse(obj.ToString()), 5);
							if (itRow["AverageCost"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["AverageCost"].ToString());
							}
							if (d != num7)
							{
								itRow["AverageCost"] = num7;
							}
						}
						if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
						{
							result = (num = decimal.Parse(obj.ToString())) * quantity;
							if (itRow["UnitPrice"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["UnitPrice"].ToString());
							}
							if (d != num)
							{
								itRow["UnitPrice"] = num;
							}
						}
					}
					else if (sysDocTypes == SysDocTypes.TransitTransferOut && quantity > 0m)
					{
						decimal num8 = default(decimal);
						decimal d2 = default(decimal);
						decimal num9 = default(decimal);
						DataRow[] array = lotData.Tables["Product_Lot_Sales"].Select("DocNumber = '" + sysDocID + voucherNumber + "' AND RecordID <> -1 AND RowIndex = " + num2);
						if (array.Length != 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								num8 += -1m * Math.Abs(decimal.Parse(array[i]["COGS"].ToString()));
								d2 = decimal.Parse(array[i]["Cost"].ToString());
								num9 += decimal.Parse(array[i]["SoldQty"].ToString());
							}
						}
						if (num9 < Math.Abs(quantity))
						{
							num8 += -1m * (Math.Abs(quantity) - num9) * d2;
						}
						result = num8;
						decimal num10 = num8;
						if (itRow["AssetValue"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AssetValue"].ToString());
						}
						if (d != num10)
						{
							itRow["AssetValue"] = num10;
						}
						result = num10;
						num10 = ((!(num9 != 0m)) ? default(decimal) : Math.Abs(Math.Round(num10 / num9, 5)));
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != num10)
						{
							itRow["AverageCost"] = num10;
						}
						itRow["UnitPrice"] = num10;
					}
					else if (sysDocTypes == SysDocTypes.DirectInventoryTransfer && quantity > 0m)
					{
						obj = result4;
						decimal num11 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero));
						if (itRow["AssetValue"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AssetValue"].ToString());
						}
						if (d != num11)
						{
							itRow["AssetValue"] = num11;
						}
						result = num11;
						num11 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), 5));
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != num11)
						{
							itRow["AverageCost"] = num11;
						}
						itRow["UnitPrice"] = num11;
					}
					else
					{
						switch (sysDocTypes)
						{
						case SysDocTypes.ConsignOut:
							if (!isLocationCosting)
							{
								obj = result4;
								if (result2 < 0m)
								{
									OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection5 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
										where v.Field<string>("ProductID") == productID && v.Field<long>("RowNumber") < currentRowNumber && ((v.Field<decimal>("TotalQuantity") >= 0m && (v.Field<byte>("TransactionType") == 2 || v.Field<byte>("TransactionType") == 1 || v.Field<byte>("TransactionType") == 6)) || v.Field<byte>("TransactionType") == 1)
										orderby v.Field<long>("RowNumber") descending
										select v;
									if (orderedEnumerableRowCollection5 != null && orderedEnumerableRowCollection5.Count() > 0)
									{
										object obj9 = orderedEnumerableRowCollection5.First().Field<object>("AverageCost");
										if (!obj9.IsNullOrEmpty())
										{
											obj = decimal.Parse(obj9.ToString());
										}
									}
									else
									{
										orderedEnumerableRowCollection5 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
											where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
											orderby v.Field<long>("RowNumber")
											select v;
										if (orderedEnumerableRowCollection5 != null && orderedEnumerableRowCollection5.Count() > 0)
										{
											object obj10 = orderedEnumerableRowCollection5.First().Field<object>("AverageCost");
											if (!obj10.IsNullOrEmpty())
											{
												obj = decimal.Parse(obj10.ToString());
											}
										}
									}
								}
							}
							if (obj != null && obj.ToString() != "" && obj != null && obj.ToString() != "")
							{
								decimal num14 = Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero);
								if (itRow["AssetValue"] != DBNull.Value)
								{
									d = decimal.Parse(itRow["AssetValue"].ToString());
								}
								if (d != num14)
								{
									itRow["AssetValue"] = num14;
								}
								result = num14;
								num14 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), 5));
								if (itRow["AverageCost"] != DBNull.Value)
								{
									d = decimal.Parse(itRow["AverageCost"].ToString());
								}
								if (d != num14)
								{
									itRow["AverageCost"] = num14;
								}
							}
							break;
						case SysDocTypes.DeliveryReturn:
						{
							obj = result4;
							if (result2 < 0m)
							{
								OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection4 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
									where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") < currentRowNumber && v.Field<byte>("TransactionType") == 2
									orderby v.Field<long>("RowNumber") descending
									select v;
								if (orderedEnumerableRowCollection4 != null && orderedEnumerableRowCollection4.Count() > 0)
								{
									object obj7 = orderedEnumerableRowCollection4.First().Field<object>("AverageCost");
									if (!obj7.IsNullOrEmpty())
									{
										obj = decimal.Parse(obj7.ToString());
									}
								}
								else
								{
									orderedEnumerableRowCollection4 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
										where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
										orderby v.Field<long>("RowNumber")
										select v;
									if (orderedEnumerableRowCollection4 != null && orderedEnumerableRowCollection4.Count() > 0)
									{
										object obj8 = orderedEnumerableRowCollection4.First().Field<object>("AverageCost");
										if (!obj8.IsNullOrEmpty())
										{
											obj = decimal.Parse(obj8.ToString());
										}
									}
								}
							}
							decimal num13 = Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero);
							if (itRow["AssetValue"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["AssetValue"].ToString());
							}
							if (d != num13)
							{
								itRow["AssetValue"] = num13;
							}
							result = num13;
							num13 = Math.Round(decimal.Parse(obj.ToString()), 5);
							if (itRow["AverageCost"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["AverageCost"].ToString());
							}
							if (d != num13)
							{
								itRow["AverageCost"] = num13;
							}
							if (itRow["UnitPrice"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["UnitPrice"].ToString());
							}
							if (d != num13)
							{
								itRow["UnitPrice"] = num13;
							}
							break;
						}
						default:
						{
							decimal d3 = result4;
							OrderedEnumerableRowCollection<DataRow> orderedEnumerableRowCollection3 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
								where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") < currentRowNumber && v.Field<byte>("TransactionType") == 2
								orderby v.Field<long>("RowNumber") descending
								select v;
							if (orderedEnumerableRowCollection3 != null && orderedEnumerableRowCollection3.Count() > 0)
							{
								object obj5 = orderedEnumerableRowCollection3.First().Field<object>("AverageCost");
								if (!obj5.IsNullOrEmpty())
								{
									d3 = decimal.Parse(obj5.ToString());
								}
							}
							else
							{
								orderedEnumerableRowCollection3 = from v in itData.Tables["Inventory_Transactions"].AsEnumerable()
									where v.Field<string>("ProductID") == productID && v.Field<decimal>("TotalQuantity") >= 0m && v.Field<long>("RowNumber") > currentRowNumber
									orderby v.Field<long>("RowNumber")
									select v;
								if (orderedEnumerableRowCollection3 != null && orderedEnumerableRowCollection3.Count() > 0)
								{
									object obj6 = orderedEnumerableRowCollection3.First().Field<object>("AverageCost");
									if (!obj6.IsNullOrEmpty())
									{
										d3 = decimal.Parse(obj6.ToString());
									}
								}
							}
							decimal num12 = Math.Round(Math.Abs(quantity * d3), 5, MidpointRounding.AwayFromZero);
							if (quantity < 0m)
							{
								num12 = -1m * num12;
							}
							result = num12;
							if (itRow["AssetValue"] != DBNull.Value)
							{
								d = decimal.Parse(itRow["AssetValue"].ToString());
							}
							if (d != num12)
							{
								itRow["AssetValue"] = num12;
							}
							if (quantity != 0m)
							{
								if (itRow["AverageCost"] != DBNull.Value)
								{
									d = decimal.Parse(itRow["AverageCost"].ToString());
								}
								if (d != Math.Abs(result4))
								{
									itRow["AverageCost"] = result4;
								}
							}
							else
							{
								if (itRow["AverageCost"] != DBNull.Value)
								{
									d = decimal.Parse(itRow["AverageCost"].ToString());
								}
								if (d != 0m)
								{
									itRow["AverageCost"] = 0;
								}
							}
							break;
						}
						}
					}
				}
				else if ((sysDocTypes == SysDocTypes.ReturnTransitTransfer || sysDocTypes == SysDocTypes.TransitTransferIn || sysDocTypes == SysDocTypes.DirectInventoryTransfer) && quantity < 0m)
				{
					obj = result4;
					if (obj != null && obj.ToString() != "")
					{
						decimal num15 = -1m * Math.Abs(Math.Round(decimal.Parse(obj.ToString()) * quantity, 5, MidpointRounding.AwayFromZero));
						result = num15;
						if (itRow["AssetValue"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AssetValue"].ToString());
						}
						if (d != num15)
						{
							itRow["AssetValue"] = num15;
						}
						decimal num16 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), 5));
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != num16)
						{
							itRow["AverageCost"] = num16;
						}
					}
				}
				else
				{
					decimal num17 = default(decimal);
					decimal d4 = default(decimal);
					decimal num18 = default(decimal);
					DataRow[] array2 = lotData.Tables["Product_Lot_Sales"].Select("DocNumber = '" + sysDocID + voucherNumber + "' AND RecordID <> -1 AND RowIndex = " + num2);
					if (array2.Length != 0)
					{
						for (int j = 0; j < array2.Length; j++)
						{
							num17 += -1m * Math.Abs(decimal.Parse(array2[j]["COGS"].ToString()));
							d4 = decimal.Parse(array2[j]["Cost"].ToString());
							num18 += decimal.Parse(array2[j]["SoldQty"].ToString());
						}
					}
					if (num18 < Math.Abs(quantity))
					{
						num17 += -1m * (Math.Abs(quantity) - num18) * d4;
					}
					result = num17;
					if (itRow["AssetValue"] != DBNull.Value)
					{
						d = decimal.Parse(itRow["AssetValue"].ToString());
					}
					if (d != num17)
					{
						itRow["AssetValue"] = Math.Round(num17, 5, MidpointRounding.AwayFromZero);
					}
					if (quantity != 0m)
					{
						decimal num19 = Math.Abs(Math.Round(num17 / Convert.ToDecimal(quantity), 5));
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != num19)
						{
							itRow["AverageCost"] = num19;
						}
					}
					else
					{
						if (itRow["AverageCost"] != DBNull.Value)
						{
							d = decimal.Parse(itRow["AverageCost"].ToString());
						}
						if (d != 0m)
						{
							itRow["AverageCost"] = 0;
						}
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		private void ReCalculateRunningTotals(ref DataRow[] rows)
		{
			try
			{
				if (rows.Length >= 2)
				{
					for (int i = 1; i < rows.Length; i++)
					{
						DataRow obj = rows[i - 1];
						DataRow dataRow = rows[i];
						decimal d = decimal.Parse(obj["RunningValue"].ToString());
						decimal d2 = decimal.Parse(obj["RunningQuantity"].ToString());
						decimal d3 = decimal.Parse(dataRow["AssetValue"].ToString());
						decimal d4 = decimal.Parse(dataRow["Quantity"].ToString());
						dataRow["RunningValue"] = d + d3;
						dataRow["RunningQuantity"] = d2 + d4;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private string GetUpdateChildLotsCostQuery(ArrayList updateLotsArray, SqlTransaction sqlTransaction)
		{
			try
			{
				if (updateLotsArray.Count == 0)
				{
					return "";
				}
				string text = "";
				for (int i = 0; i < updateLotsArray.Count; i++)
				{
					if (text != "")
					{
						text += ",";
					}
					text = text + "'" + updateLotsArray[i].ToString() + "'";
				}
				string text2 = "  UPDATE PL  SET Cost = CASE WHEN IT.UnitPrice <> 0 THEN UnitPrice ELSE AverageCost END ,\r\n                            AvgCost = AverageCost FROM Product_Lot PL \r\n                            INNER JOIN Inventory_Transactions IT ON PL.DocID = IT.SysDocID AND PL.ReceiptNumber = IT.VoucherID AND PL.RowIndex = IT.RowIndex\r\n                             WHERE PL.SourceLotNumber IS NOT NULL AND PL.SourceLotNumber IN\r\n                            (SELECT LotNumber FROM Product_Lot PL2 WHERE PL2.DocID + PL2.ReceiptNumber IN (" + text + "))";
				return text2 + " UPDATE PLS  SET PLS.Cost = PL.Cost FROM Product_Lot_Sales PLS \r\n                        INNER JOIN Product_Lot PL ON PL.LotNumber = PLS.LotNo \r\n                        WHERE PLS.LotNo IN\r\n                        (SELECT LotNumber FROM Product_Lot PL2 WHERE PL2.DocID + PL2.ReceiptNumber IN (" + text + ") \r\n\r\n\t                        UNION \r\n                         SELECT LotNumber FROM Product_Lot PL2 WHERE PL2.SourceLotNumber IN (SELECT LotNumber FROM Product_Lot PL2 WHERE PL2.DocID + PL2.ReceiptNumber IN (" + text + ")))";
			}
			catch
			{
				throw;
			}
		}

		private void AddCOGSRowForUpdate(ref DataSet updateData, int transactionID, string productID, decimal unitPrice, decimal avgCost, decimal assetValue)
		{
		}

		internal bool UpdateFutureTransactionsCost(InventoryTransactionData inventoryTransactionData, SqlTransaction sqlTransaction)
		{
			return UpdateFutureTransactionsCost(inventoryTransactionData, includeAllRecords: false, sqlTransaction);
		}

		private DataSet GetCostingInventoryData(ArrayList productIDs, DateTime transactionDate, bool includePreviousRowsTable, string excludingSysDocID, string excludingVoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(transactionDate);
				bool isLocationCosting = new CompanyInformations(base.DBConfig).GetIsLocationCosting(sqlTransaction);
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text2 = "";
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ProductID", typeof(string));
				foreach (string productID in productIDs)
				{
					dataTable.Rows.Add(productID);
				}
				DataSet dataSet = new DataSet();
				SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(base.DBConfig.Connection, SqlBulkCopyOptions.Default, sqlTransaction);
				text2 = "  CREATE TABLE TEMP_ITEM55 (ProductID nvarchar(64)) ";
				ExecuteNonQuery(text2, sqlTransaction);
				sqlBulkCopy.DestinationTableName = "TEMP_ITEM55";
				sqlBulkCopy.WriteToServer(dataTable);
				string text4 = ",IT.LocationID";
				string text5 = "LocationID,";
				if (!isLocationCosting)
				{
					text4 = "";
					text5 = "";
				}
				text2 = "  --Create Temp Table for previous transactions\r\n                            SELECT  DISTINCT IT.TransactionID,IT.SysDocID,IT.VoucherID,IT.TransactionType,IT.ProductID,IT.SysDocType, IT.RowIndex,\r\n                                    IT.TransactionDate,IT.ReturnedQuantity,IT.RefTransactionID,IT.Quantity,IT.UnitPrice,IT.AverageCost,ROUND(AssetValue,4) AS AssetValue,UnitPrice AS OldUnitPrice,IT.AverageCost AS OldAverageCost,\r\n                                    ROUND(IT.AssetValue,4) AS OldAssetValue,IsNonCostedGRN,IT.LocationID,\r\n                                    ROW_NUMBER()  OVER ( Partition BY IT.ProductID " + text4 + " Order BY " + text5 + " TransactionDate, IT.TransactionID ) RowNumber,\r\n                                       SUM(IT.Quantity - ISNULL(ReturnedQuantity,0))  OVER ( Partition BY IT.ProductID " + text4 + " Order by " + text5 + " TransactionDate, IT.TransactionID ROWS between unbounded preceding and current ROW) AS TotalQuantity,\r\n                                     SUM(ROUND(AssetValue,4))  OVER ( Partition BY IT.ProductID " + text4 + " Order by " + text5 + " TransactionDate, IT.TransactionID ROWS between unbounded preceding and current ROW ) AS TotalValue,P.CostMethod\r\n                                            INTO #TMP_PREV    FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID = IT.ProductID WHERE IT.ProductID IN (SELECT ProductID FROM TEMP_ITEM55 ) \r\n                        CREATE Index IND ON #TMP_PREV (TransactionDate DESC,TransactionID DESC)\r\n\r\n                            SELECT DISTINCT PRV.*, 1 AS CostMethod FROM #TMP_PREV PRV\r\n                                 WHERE PRV.Quantity <> 0  AND PRV.Transactiondate >= '" + text + "' ";
				text2 = text2 + " UNION\r\n                           SELECT PRV.*,1 AS CostMethod FROM #TMP_PREV PRV WHERE  TransactionDate >=\r\n\t\t\t\t\t\t\t\t\t\t(   \r\n\t\t\t\t\t\t\t\t\t\t\tISNULL((SELECT TOP 1 TransactionDate FROM #TMP_PREV  WHERE ProductID = PRV.ProductID \r\n\t\t\t\t\t\t\t\t\t\t\tGROUP by " + text5 + " TransactionDate,ProductID,TransactionID,TotalQuantity\r\n\t\t\t\t\t\t\t\t\t\t\tHAVING TotalQuantity>=0\r\n\t\t\t\t\t\t\t\t\t\t\tORDER BY " + text5 + " TransactionDate DESC,TransactionID DESC),'1-1-1900') ) ORDER BY " + text5 + " ProductID,TransactionDate,TransactionID ";
				text2 += "DROP TABLE #TMP_PREV";
				FillDataSet(dataSet, "Inventory_Transactions", text2, sqlTransaction);
				if (includePreviousRowsTable)
				{
					text2 = " WITH summary AS (\r\n                                     SELECT   DISTINCT IT.TransactionID,IT.SysDocID,IT.VoucherID,IT.TransactionType,IT.ProductID,IT.SysDocType, IT.RowIndex,\r\n                                    IT.TransactionDate,IT.ReturnedQuantity,IT.RefTransactionID,IT.Quantity,IT.UnitPrice,IT.AverageCost,ROUND(AssetValue," + 5 + ") AS AssetValue,UnitPrice AS OldUnitPrice,IT.AverageCost AS OldAverageCost,\r\n                                    ROUND(IT.AssetValue," + 5 + ") AS OldAssetValue,IsNonCostedGRN,IT.LocationID,\r\n                                    ROW_NUMBER()  OVER ( Partition BY IT.ProductID " + text4 + " Order by " + text5 + " TransactionDate DESC, IT.TransactionID DESC) RowNumber,\r\n                                       SUM(IT.Quantity - ISNULL(ReturnedQuantity,0))  OVER ( Partition BY IT.ProductID " + text4 + " Order by " + text5 + " TransactionDate, IT.TransactionID ROWS between unbounded preceding and current ROW) AS TotalQuantity,\r\n                                     SUM(ROUND(AssetValue," + 5 + "))  OVER ( Partition BY IT.ProductID " + text4 + " Order by " + text5 + " TransactionDate, IT.TransactionID ROWS between unbounded preceding and current ROW ) AS TotalValue,P.CostMethod\r\n                                     FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID = IT.ProductID WHERE  IT.ProductID IN ( SELECT ProductID FROM TEMP_ITEM55) ";
					text2 = ((!(excludingSysDocID == "")) ? (text2 + " AND IT.TransactionDate <= '" + text + "' AND TransactionID NOT IN (SELECT TransactionID FROM Inventory_Transactions WHERE SysDocID = '" + excludingSysDocID + "' AND VoucherID = '" + excludingVoucherID + "' ))  ") : (text2 + " AND IT.TransactionDate < '" + text + "' )  "));
					text2 += " SELECT S.* FROM summary S WHERE RowNumber = 1 ";
					FillDataSet(dataSet, "First_Rows", text2, sqlTransaction);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				try
				{
					string exp = "DROP TABLE Temp_Item55";
					ExecuteNonQuery(exp, sqlTransaction);
				}
				catch
				{
				}
			}
		}

		private DataSet GetCostingLotData(DataSet trData, SqlTransaction sqlTransaction)
		{
			try
			{
				SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(base.DBConfig.Connection, SqlBulkCopyOptions.Default, sqlTransaction);
				string exp = "Create Table TEMP_COST_TR (SysDocID nvarchar(7),VoucherID nvarchar(15))\r\n                                CREATE TABLE TEMP_COST_ITEM (ProductID nvarchar(64)) ";
				ExecuteNonQuery(exp, sqlTransaction);
				sqlBulkCopy.DestinationTableName = "TEMP_COST_TR";
				sqlBulkCopy.WriteToServer(trData.Tables["TR"]);
				sqlBulkCopy.DestinationTableName = "TEMP_COST_ITEM";
				sqlBulkCopy.WriteToServer(trData.Tables["Item"]);
				DataSet dataSet = new DataSet();
				exp = "SELECT RecordID,LotNo,DocID,InvoiceNumber, DocID + InvoiceNumber AS DocNumber, ItemCode,RowIndex, SoldQty,Cost, SUM(SoldQty * ISNULL(Cost,0)) AS COGS\r\n\t\t\t\t\t\t   FROM Product_Lot_Sales   WHERE ItemCode IN (SELECT ProductID FROM Temp_Cost_ITEM) ";
				if (trData.Tables["TR"].Rows.Count > 0)
				{
					exp += " AND EXISTS (SELECT * FROM TEMP_COST_TR TMP WHERE Product_Lot_Sales.docid = TMP.SysDocID and Product_Lot_Sales.InvoiceNumber = TMP.VoucherID) ";
				}
				exp += "  GROUP BY ItemCode,RowIndex,DocID, InvoiceNumber,SoldQty,Cost,LotNo,RecordID\r\n\r\n\t\t\t\t\t\t   UNION\r\n\r\n\t\t\t\t\t\t   SELECT  -1 AS RecordID,-1 AS LotNo,SysDocID AS DocID,VoucherID AS InvoiceNumber,SysDocID + VoucherID AS DocNumber,ProductID AS ItemCode,RowIndex,Quantity AS SoldQty,0 AS Cost,  \r\n                            SUM(Quantity) * ISNULL((SELECT TOP 1 Cost FROM Product_Lot PL \r\n\t\t\t\t\t\t   WHERE PL.ItemCode = UL.ProductID AND Cost > 0),0) AS COGS \r\n\t\t\t\t\t\t   FROM Unallocated_Lot_Items UL   WHERE ProductID IN (SELECT ProductID FROM Temp_Cost_ITEM)  ";
				if (trData.Tables["TR"].Rows.Count > 0)
				{
					exp += " AND    EXISTS (SELECT * FROM TEMP_COST_TR TMP WHERE UL.SysDocID = TMP.SysDocID and UL.VoucherID = TMP.VoucherID) ";
				}
				exp += " GROUP BY ProductID,RowIndex,SysDocID,Quantity, VoucherID";
				FillDataSet(dataSet, "Product_Lot_Sales", exp, sqlTransaction);
				exp = "SELECt LotNumber,ItemCode,Cost,AvgCost,LotQty,SoldQty,DocID,ReceiptNumber,RowIndex FROM Product_Lot\r\n                         WHERE LotNumber IN (SELECT LotNo FROM Product_Lot_Sales WHERE ItemCode IN  (SELECT ProductID FROM Temp_Cost_ITEM) ";
				if (trData.Tables["TR"].Rows.Count > 0)
				{
					exp += " AND  EXISTS (SELECT * FROM TEMP_COST_TR TMP WHERE Product_Lot.docid = TMP.SysDocID and Product_Lot_Sales.InvoiceNumber = TMP.VoucherID) ";
				}
				exp += ")";
				FillDataSet(dataSet, "Product_Lot", exp, sqlTransaction);
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				try
				{
					string exp2 = "DROP  Table TEMP_COST_TR \r\n                        DROP TABLE TEMP_COST_ITEM  ";
					ExecuteNonQuery(exp2, sqlTransaction);
				}
				catch
				{
				}
			}
		}

		internal bool UpdateFutureTransactionsCost(InventoryTransactionData inventoryTransactionData, bool includeAllRecords, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				ServerTestTimer serverTestTimer = new ServerTestTimer();
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DateTime dateTime = SqlDateTime.MinValue.Value;
				string text = "";
				string text2 = "";
				string a = "";
				string text3 = "";
				GLData glData = new GLData();
				DataSet dataSet = new DataSet();
				ArrayList arrayList = new ArrayList();
				if (inventoryTransactionData.InventoryTransactionTable.Rows.Count > 0)
				{
					text = inventoryTransactionData.InventoryTransactionTable.Rows[0]["SysDocID"].ToString();
					text2 = inventoryTransactionData.InventoryTransactionTable.Rows[0]["VoucherID"].ToString();
					inventoryTransactionData.InventoryTransactionTable.Rows[0]["LocationID"].ToString();
					DataRow dataRow = glData.JournalTable.NewRow();
					dataRow["JournalID"] = 0;
					dataRow["JournalDate"] = DateTime.Today;
					dataRow["SysDocID"] = text;
					dataRow["VoucherID"] = text2;
					dataRow.EndEdit();
					glData.JournalTable.Rows.Add(dataRow);
					dateTime = DateTime.Parse(inventoryTransactionData.InventoryTransactionTable.Rows[0]["TransactionDate"].ToString());
					a = CommonLib.ToSqlDateTimeString(dateTime);
					text3 = GetCommaSeperatedIDs(inventoryTransactionData, "Inventory_Transactions", "ProductID");
					foreach (DataRow row in inventoryTransactionData.InventoryTransactionTable.Rows)
					{
						int result = 1;
						if (row.Table.Columns.Contains("ItemType"))
						{
							result = 1;
							int.TryParse(row["ItemType"].ToString(), out result);
						}
						if ((result == 1 || result == 7) && !arrayList.Contains(row["ProductID"].ToString()))
						{
							arrayList.Add(row["ProductID"].ToString());
						}
					}
				}
				if (arrayList.Count == 0 || text3 == "" || a == "")
				{
					return true;
				}
				dataSet = GetCostingInventoryData(arrayList, dateTime, includePreviousRowsTable: false, "", "", sqlTransaction);
				if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					return true;
				}
				string text4 = " ";
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal num3 = default(decimal);
				decimal result4 = default(decimal);
				DataSet dataSet2 = new DataSet();
				ArrayList arrayList2 = new ArrayList();
				DataSet dataSet3 = new DataSet();
				DataTable dataTable = dataSet3.Tables.Add("TR");
				dataTable.Columns.Add("SysDocID", typeof(string));
				dataTable.Columns.Add("VoucherID", typeof(string));
				foreach (DataRow row2 in dataSet.Tables[0].Rows)
				{
					string text5 = row2["SysDocID"].ToString() + row2["VoucherID"].ToString();
					if (!arrayList2.Contains(text5))
					{
						arrayList2.Add(text5);
						dataTable.Rows.Add(row2["SysDocID"].ToString(), row2["VoucherID"].ToString());
					}
				}
				DataTable dataTable2 = dataSet3.Tables.Add("Item");
				dataTable2.Columns.Add("ProductID");
				foreach (object item in arrayList)
				{
					dataTable2.Rows.Add(item.ToString());
				}
				dataSet2 = GetCostingLotData(dataSet3, sqlTransaction);
				serverTestTimer.Start();
				SetRowCosting(ref dataSet, ref dataSet2, arrayList, text, text2, sqlTransaction);
				DataSet dataSet4 = new DataSet();
				string textCommand = "SELECT ISNULL(P.AssetAccount,PC.AssetAccount) AS InventoryAccountID,ISNULL(P.COGSAccount,PC.COGSAccount) AS COGSAccountID,ISNULL(P.IncomeAccount ,PC.IncomeAccount)AS SalesAccountID FROM \r\n                        Product P LEFT JOIN Product_Class PC ON P.ClassID = PC.ClassID WHERE P.ProductID IN (" + text3 + ")";
				FillDataSet(dataSet4, "Product", textCommand, sqlTransaction);
				string commaSeperatedIDs = GetCommaSeperatedIDs(dataSet, "Inventory_Transactions", "LocationID");
				string commaSeperatedIDs2 = GetCommaSeperatedIDs(dataSet, "Inventory_Transactions", "SysDocID");
				textCommand = " SELECT LocationID,SalesAccountID,COGSAccountID,InventoryAccountID,ProjectWIPAccountID,\r\n                        ProjectIncomeAccountID,ProjectCostAccountID,ConsignOutSalesAccountID,ConsignOutCOGSAccountID,UnInvoicedInventoryAccountID FROM Location\r\n                        WHERE LocationID IN (" + commaSeperatedIDs + ") OR LocationID IN (SELECT LocationID FROM System_Document SD WHERE SD.SysDocID IN (" + commaSeperatedIDs2 + "))";
				FillDataSet(dataSet4, "Location", textCommand, sqlTransaction);
				textCommand = "SELECT SysDocID, LocationID,DivisionID,SalesAccountID,COGSAccountID FROM System_Document WHERE SysDocID IN (" + commaSeperatedIDs2 + ")";
				FillDataSet(dataSet4, "System_Document", textCommand, sqlTransaction);
				_ = new InventoryTransactionData().Tables[0];
				ArrayList arrayList3 = new ArrayList();
				for (int i = 0; i < arrayList.Count; i++)
				{
					string text6 = arrayList[i].ToString();
					dataSet = dataSet.GetChanges();
					if (dataSet == null || dataSet.Tables.Count == 0)
					{
						return true;
					}
					DataRow[] array = dataSet.Tables[0].Select("ProductID = '" + text6 + "'", "TransactionDate,TransactionID");
					if (array.Length != 0)
					{
						foreach (DataRow dataRow4 in array)
						{
							int.Parse(dataRow4["TransactionID"].ToString());
							num = default(decimal);
							num2 = default(decimal);
							decimal result5 = default(decimal);
							num3 = default(decimal);
							StoreConfiguration.ToSqlDateTimeString(DateTime.Parse(dataRow4["TransactionDate"].ToString()));
							string text7 = dataRow4["SysDocID"].ToString();
							string text8 = dataRow4["VoucherID"].ToString();
							if (includeAllRecords || !(text7 == text) || !(text8 == text2))
							{
								Convert.ToInt32(dataRow4["RowIndex"].ToString());
								string warehouseLocationID = dataRow4["LocationID"].ToString();
								decimal.TryParse(dataRow4["Quantity"].ToString(), out result5);
								decimal.TryParse(dataRow4["UnitPrice"].ToString(), out result4);
								decimal.TryParse(dataRow4["AverageCost"].ToString(), out num);
								decimal.TryParse(dataRow4["AssetValue"].ToString(), out result2);
								decimal.TryParse(dataRow4["OldAverageCost"].ToString(), out num2);
								decimal.TryParse(dataRow4["OldAssetValue"].ToString(), out result3);
								decimal.TryParse(dataRow4["OldUnitPrice"].ToString(), out num3);
								InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)int.Parse(dataRow4["TransactionType"].ToString());
								SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(dataRow4["SysDocType"].ToString());
								if (result2 != result3 || num != num2 || num3 != result4 || sysDocTypes == SysDocTypes.GoodsReceivedNote || sysDocTypes == SysDocTypes.ImportGoodsReceivedNote)
								{
									decimal d = result3 - result2;
									if ((inventoryTransactionTypes != InventoryTransactionTypes.Purchase || !(result5 > 0m)) && (inventoryTransactionTypes != InventoryTransactionTypes.OpeningInventory || !(result5 > 0m)) && (sysDocTypes != SysDocTypes.TransitTransferOut || !(result5 > 0m)) && (sysDocTypes != SysDocTypes.TransitTransferIn || !(result5 < 0m)) && (sysDocTypes != SysDocTypes.ReturnTransitTransfer || !(result5 < 0m)) && (sysDocTypes != SysDocTypes.ConsignOut || !(result5 < 0m)) && sysDocTypes != SysDocTypes.GRNReturn && (sysDocTypes != SysDocTypes.ConsignOutReturn || !(result5 < 0m)) && sysDocTypes != SysDocTypes.DirectInventoryTransfer)
									{
										d = Math.Round(d, currencyDecimalPoints);
										if (d != 0m)
										{
											flag &= CreateCOGSDiffJournalDetail(ref glData, dataSet4, sysDocTypes, text6, text7, text8, warehouseLocationID, d, sqlTransaction);
										}
									}
								}
								if (result5 > 0m && !arrayList3.Contains(text7 + text8))
								{
									arrayList3.Add(text7 + text8);
								}
							}
						}
					}
				}
				if (text4.Trim() != "")
				{
					flag &= (ExecuteNonQuery(text4, updateInBatch: true, sqlTransaction) >= 0);
				}
				flag &= UpdateInventoryTransactionTableCOGS(dataSet.GetChanges(), sqlTransaction);
				flag &= UpdateProductLotTableCost(dataSet2, sqlTransaction);
				flag &= UpdateProductLotSaleTableCost(dataSet2.GetChanges(), sqlTransaction);
				if (glData.JournalDetailsTable.Rows.Count > 0)
				{
					flag &= new Journal(base.DBConfig).InsertCOGSDiffJournalDetails(glData, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		private bool UpdateProductLotTableCost(DataSet data, SqlTransaction sqlTransaction)
		{
			try
			{
				if (data == null)
				{
					return true;
				}
				updateCommand = new SqlCommand("", base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				updateCommand.Transaction = sqlTransaction;
				updateCommand.CommandText = "UPDATE Product_Lot SET Cost = @Cost, AvgCost = @AverageCost WHERE LotNumber = @LotNumber ";
				updateCommand.Parameters.Add("@Cost", SqlDbType.Money, 18, "Cost");
				updateCommand.Parameters.Add("@AverageCost", SqlDbType.Decimal, 18, "AvgCost");
				updateCommand.Parameters.Add("@LotNumber", SqlDbType.Int, 18, "LotNumber");
				dsCommand.UpdateBatchSize = 1000;
				updateCommand.UpdatedRowSource = UpdateRowSource.None;
				bool result = Update(data, "Product_Lot", updateCommand);
				updateCommand.UpdatedRowSource = UpdateRowSource.Both;
				dsCommand.UpdateBatchSize = 1;
				return result;
			}
			catch
			{
				throw;
			}
		}

		private bool UpdateProductLotSaleTableCost(DataSet data, SqlTransaction sqlTransaction)
		{
			try
			{
				if (data == null)
				{
					return true;
				}
				updateCommand = new SqlCommand("", base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				updateCommand.Transaction = sqlTransaction;
				updateCommand.CommandText = "UPDATE Product_Lot_Sales SET Cost = @Cost WHERE RecordID = @RecordID";
				updateCommand.Parameters.Add("@Cost", SqlDbType.Money, 18, "Cost");
				updateCommand.Parameters.Add("@RecordID", SqlDbType.Int, 18, "RecordID");
				dsCommand.UpdateBatchSize = 1000;
				updateCommand.UpdatedRowSource = UpdateRowSource.None;
				bool result = Update(data, "Product_Lot_Sales", updateCommand);
				updateCommand.UpdatedRowSource = UpdateRowSource.Both;
				dsCommand.UpdateBatchSize = 1;
				return result;
			}
			catch
			{
				throw;
			}
		}

		private bool UpdateInventoryTransactionTableCOGS(DataSet data, SqlTransaction sqlTransaction)
		{
			try
			{
				if (data == null)
				{
					return true;
				}
				data.Tables[0].Select("VoucherID = 'AW088293'");
				updateCommand = new SqlCommand("", base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				updateCommand.Transaction = sqlTransaction;
				updateCommand.CommandText = "UPDATE Inventory_Transactions SET AssetValue = @AssetValue, UnitPrice = @UnitPrice, AverageCost = @AverageCost WHERE TransactionID = @TransactionID ";
				updateCommand.Parameters.Add("@AssetValue", SqlDbType.Money, 18, "AssetValue");
				updateCommand.Parameters.Add("@UnitPrice", SqlDbType.Decimal, 18, "UnitPrice");
				updateCommand.Parameters.Add("@AverageCost", SqlDbType.Decimal, 18, "AverageCost");
				updateCommand.Parameters.Add("@TransactionID", SqlDbType.Decimal, 18, "TransactionID");
				dsCommand.UpdateBatchSize = 1000;
				updateCommand.UpdatedRowSource = UpdateRowSource.None;
				bool result = Update(data, "Inventory_Transactions", updateCommand);
				updateCommand.UpdatedRowSource = UpdateRowSource.Both;
				dsCommand.UpdateBatchSize = 1;
				return result;
			}
			catch
			{
				throw;
			}
		}

		private string GetQueryForUpdateLotSalesCOGS(string productID, string sysDocID, string voucherID, int rowIndex, CostMethods costMethod, decimal quantity, decimal newAvgCost, decimal newCOGS, SqlTransaction sqlTransaction)
		{
			try
			{
				string result = "";
				if (quantity != 0m)
				{
					Math.Round(Math.Abs(newCOGS / quantity), 5);
				}
				if (costMethod == CostMethods.Average || costMethod == CostMethods.None)
				{
					result = "UPDATE Product_Lot_Sales SET Cost = " + newAvgCost + " WHERE DocID = '" + sysDocID + "' AND  InvoiceNumber = '" + voucherID + "'AND ItemCode = '" + productID + "' AND RowIndex = " + rowIndex;
				}
				else
				{
					_ = 2;
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		private string GetQueryForUpdateAssemblyCost(string productID, string sysDocID, string voucherID, int rowIndex, CostMethods costMethod, decimal diffAmount, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				switch (costMethod)
				{
				case CostMethods.None:
				case CostMethods.Average:
					text = " UPDATE  Inventory_Transactions SET UnitPrice = CASE WHEN Quantity = 0 THEN 0 ELSE UnitPrice + " + diffAmount + "/Quantity END ,\r\n                                 AssetValue = AssetValue + " + diffAmount + "  WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND RowIndex = -1 ";
					break;
				case CostMethods.FIFO:
					text = " UPDATE  Inventory_Transactions SET UnitPrice = CASE WHEN Quantity = 0 THEN 0 ELSE UnitPrice + " + diffAmount + "/Quantity END ,\r\n                                 AssetValue = AssetValue + " + diffAmount + "  WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND RowIndex = -1 ";
					break;
				}
				return text + " UPDATE  PL SET PL.Cost = IT.UnitPrice FROM Product_Lot PL INNER JOIN Inventory_Transactions IT ON PL.DocID = IT.SysDocID\r\n                            AND PL.ReceiptNumber = IT.VoucherID AND PL.RowIndex = -1\r\n                            WHERE PL.DocID = '" + sysDocID + "' AND PL.ReceiptNumber = '" + voucherID + "'";
			}
			catch
			{
				throw;
			}
		}

		private string GetQueryForUpdateTransferCost(string productID, string sysDocID, string voucherID, int rowIndex, CostMethods costMethod, decimal diffAmount, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				string text2 = "";
				string text3 = "";
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Inventory_Transfer", "AcceptVoucherID", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					text2 = fieldValue.ToString();
				}
				if (text2 == "")
				{
					return "";
				}
				fieldValue = new Databases(base.DBConfig).GetFieldValue("Inventory_Transfer", "AcceptSysDocID", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					text3 = fieldValue.ToString();
				}
				text = " UPDATE  Inventory_Transactions SET UnitPrice = CASE WHEN Quantity = 0 THEN 0 ELSE UnitPrice + " + diffAmount + "/Quantity END ,\r\n                                 AssetValue = AssetValue + " + diffAmount + "  WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text2 + "' AND RowIndex = " + rowIndex;
				return text + " UPDATE  PL SET PL.Cost = IT.UnitPrice FROM Product_Lot PL INNER JOIN Inventory_Transactions IT ON PL.DocID = IT.SysDocID\r\n                            AND PL.ReceiptNumber = IT.VoucherID AND PL.RowIndex = " + rowIndex + "\r\n                            WHERE PL.DocID = '" + text3 + "' AND PL.ReceiptNumber = '" + text2 + "'";
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateGRNCosting(string purchaseInvoiceSysDocID, string purchaseInvoiceVoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				bool flag = true;
				bool result = false;
				object obj = null;
				obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.EnableCostRunning, null);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result);
				}
				text = " UPDATE IT SET UnitPrice = CASE WHEN PID.UnitQuantity IS NOT NULL AND PID.Quantity<>0 THEN (PID.UnitQuantity * (PID.UnitPrice+ ISNULL(LCost,0)))/PID.Quantity \r\n\t                            ELSE PID.Unitprice + ISNULL(LCost,0) END, Cost =  PID.LCost, \r\n                                AssetValue = (IT.Quantity - ISNULL(ReturnedQuantity,0)) * (CASE WHEN PID.UnitQuantity IS NOT NULL AND PID.Quantity<>0 THEN (PID.UnitQuantity * (PID.UnitPrice+ ISNULL(LCost,0)))/PID.Quantity \r\n\t                            ELSE PID.Unitprice + ISNULL(LCost,0) END) ,IsNonCostedGRN = 'False' FROM Inventory_Transactions IT \r\n                                INNER JOIN Purchase_Invoice_Detail PID ON IT.SysdocID = PID.OrderSysDocID AND IT.VoucherID = PID.OrderVoucherID AND IT.ProductID = PID.ProductID AND IT.RowIndex= PID.OrderRowIndex\r\n                                WHERE PID.SysDocID = '" + purchaseInvoiceSysDocID + "' AND PID.VoucherID = '" + purchaseInvoiceVoucherID + "'";
				text = text + " UPDATE PL SET PL.Cost = (CASE WHEN PID.UnitQuantity IS NOT NULL AND PID.Quantity<>0 THEN (PID.UnitQuantity * (PID.UnitPrice+ ISNULL(LCost,0)))/PID.Quantity \r\n\t                            ELSE PID.Unitprice + ISNULL(LCost,0) END) FROM Product_Lot PL \r\n                                INNER JOIN Purchase_Invoice_Detail PID ON PL.DocID = PID.OrderSysDocID AND PL.ReceiptNumber = PID.OrderVoucherID AND PL.RowIndex = PID.OrderRowIndex\r\n                                WHERE PID.SysDocID = '" + purchaseInvoiceSysDocID + "' AND PID.VoucherID = '" + purchaseInvoiceVoucherID + "' ";
				text = text + " UPDATE PLS SET Cost = PL.Cost FROM Product_Lot_Sales PLs \r\n\t\t\t\t\t\t\t\tINNER JOIN Product_Lot PL ON PL.LotNumber = PLS.LotNo\r\n                                INNER JOIN Purchase_Invoice_Detail PID ON PL.DocID = PID.OrderSysDocID AND PL.ReceiptNumber = PID.OrderVoucherID AND PL.RowIndex = PID.OrderRowIndex\r\n                                    WHERE PID.SysDocID = '" + purchaseInvoiceSysDocID + "' AND PID.VoucherID = '" + purchaseInvoiceVoucherID + "' ";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				text = "SELECT DISTINCT OrderSysDocID, OrderVoucherID FROM Purchase_Invoice_Detail PID\r\n\t\t\t\t\t        WHERE SysDocID = '" + purchaseInvoiceSysDocID + "' AND VoucherID = '" + purchaseInvoiceVoucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "GRN", text);
				string text2 = "";
				string text3 = "";
				foreach (DataRow row in dataSet.Tables["GRN"].Rows)
				{
					text2 = row["OrderSysDocID"].ToString();
					text3 = row["OrderVoucherID"].ToString();
					text = "UPDATE IT  SET UnitPrice = GRN_IT.UnitPrice, Cost = GRN_IT.Cost,IT.AverageCost = GRN_IT.AverageCost, IsNonCostedGRN = 'False', AssetValue = (IT.Quantity - ISNULL(IT.ReturnedQuantity,0)) * GRN_IT.UnitPrice\r\n                                        FROM Inventory_Transactions IT\r\n                                        INNER JOIN GRN_Return_Detail GRRD ON IT.SysDocID = GRRD.SysDocID AND IT.VoucherID = GRRD.VoucherID  AND IT.RowIndex = GRRD.RowIndex\r\n                                        INNER JOIN  Inventory_Transactions GRN_IT ON GRN_IT.SysDocID = GRRD.SourceSysDocID AND GRN_IT.VoucherID = GRRD.SourceVoucherID \r\n                                        AND GRN_IT.RowIndex = GRRD.SourceRowIndex\r\n                                        WHERE GRRD.SourceSysDocID = '" + text2 + "' AND GRRD.SourceVoucherID = '" + text3 + "' ";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					if (!result)
					{
						InventoryTransactionData inventoryTransaction = GetInventoryTransaction(text2, text3, sqlTransaction);
						flag &= UpdateFutureAndPastAffectedTransactionsCOGS(inventoryTransaction, sqlTransaction);
					}
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		private bool CreateCOGSDiffJournalDetail(ref GLData glData, DataSet accountData, SysDocTypes sysDocType, string productID, string sysDocID, string voucherID, string warehouseLocationID, decimal amount, SqlTransaction sqlTransaction)
		{
			try
			{
				if (amount == 0m)
				{
					return true;
				}
				string text = "";
				string text2 = "";
				bool result = true;
				string text3 = "";
				if (voucherID == "000742")
				{
					voucherID = voucherID;
				}
				DataRow dataRow = glData.JournalTable.Rows[0];
				string text4 = accountData.Tables["System_Document"].Select("SysDocID = '" + sysDocID + "'")[0]["LocationID"].ToString();
				string value = accountData.Tables["System_Document"].Select("SysDocID = '" + sysDocID + "'")[0]["DivisionID"].ToString();
				DataRow dataRow2 = accountData.Tables["Product"].Rows[0];
				string text5 = dataRow2["COGSAccountID"].ToString();
				string text6 = dataRow2["InventoryAccountID"].ToString();
				string text7 = "";
				string text8 = "";
				string text9 = "";
				DataRow[] array = accountData.Tables["System_Document"].Select("SysDocID = '" + sysDocID + "'");
				if (array.Length != 0 && text5 == "")
				{
					text5 = array[0]["COGSAccountID"].ToString();
				}
				array = accountData.Tables["Location"].Select("LocationID = '" + warehouseLocationID + "'");
				if (array.Length != 0 && text6 == "")
				{
					text6 = array[0]["InventoryAccountID"].ToString();
				}
				array = accountData.Tables["Location"].Select("LocationID = '" + text4 + "'");
				if (array.Length != 0)
				{
					if (text5 == "")
					{
						text5 = array[0]["COGSAccountID"].ToString();
					}
					if (text6 == "")
					{
						text6 = array[0]["InventoryAccountID"].ToString();
					}
					if (text9 == "")
					{
						text9 = array[0]["ProjectWIPAccountID"].ToString();
					}
					text8 = array[0]["ConsignOutCOGSAccountID"].ToString();
					text7 = array[0]["UnInvoicedInventoryAccountID"].ToString();
				}
				text2 = text6;
				text = text7;
				switch (sysDocType)
				{
				case SysDocTypes.ConsignOutSettlement:
					text = text8;
					break;
				case SysDocTypes.SalesReceipt:
					text = text5;
					break;
				case SysDocTypes.JobInventoryIssue:
				{
					text3 = "SELECT J.WIPAccountID FROM Job_Inventory_Issue_Detail JI INNER JOIN Job J ON JI.JobID = J.JobID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND JI.ProductID = '" + productID + "'";
					object obj = ExecuteScalar(text3, sqlTransaction);
					if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
					{
						text9 = obj.ToString();
					}
					text = text9;
					break;
				}
				case SysDocTypes.JobInventoryReturn:
				{
					text3 = "SELECT J.WIPAccountID FROM Job_Inventory_Issue_Detail JI INNER JOIN Job J ON JI.JobID = J.JobID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND JI.ProductID = '" + productID + "'";
					object obj = ExecuteScalar(text3, sqlTransaction);
					if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
					{
						text9 = obj.ToString();
					}
					text2 = text9;
					text = text6;
					break;
				}
				case SysDocTypes.SalesInvoice:
				case SysDocTypes.ExportSalesInvoice:
					text = text5;
					break;
				case SysDocTypes.ConsignOut:
				{
					text3 = "SELECT LOC.InventoryAccountID  FROM System_Document SD\r\n                                 INNER JOIN Location LOC ON Loc.LocationID = SD.ConsignOutLocationID WHERE SD.SysDocID = '" + sysDocID + "' ";
					object obj = ExecuteScalar(text3, sqlTransaction);
					if (obj == null || string.IsNullOrEmpty(obj.ToString()))
					{
						throw new CompanyException("There is no account assigned to consignment location for document: " + sysDocID);
					}
					text2 = obj.ToString();
					text = text7;
					break;
				}
				case SysDocTypes.ConsignOutReturn:
				{
					text3 = "SELECT LOC.InventoryAccountID  FROM System_Document SD\r\n                                 INNER JOIN Location LOC ON Loc.LocationID = SD.ConsignOutLocationID WHERE SD.SysDocID = '" + sysDocID + "' ";
					object obj = ExecuteScalar(text3, sqlTransaction);
					text2 = ((obj == null || string.IsNullOrEmpty(obj.ToString())) ? text6 : obj.ToString());
					text = text6;
					break;
				}
				case SysDocTypes.AssemblyBuild:
				case SysDocTypes.InventoryRepacking:
					text = text6;
					break;
				case SysDocTypes.TransitTransferOut:
				{
					text3 = "SELECT LOC.InventoryAccountID  FROM Inventory_Transfer IT INNER JOIN Inventory_Transfer_Type ITT ON IT.TransferTypeID = ITT.TypeID\r\n                                 INNER JOIN Location LOC ON Loc.LocationID = ITT.LocationID WHERE IT.SysDocID = '" + sysDocID + "' AND IT.VoucherID = '" + voucherID + "'";
					object obj = ExecuteScalar(text3, sqlTransaction);
					text = ((obj == null || string.IsNullOrEmpty(obj.ToString())) ? text6 : obj.ToString());
					break;
				}
				case SysDocTypes.InventoryAdjustment:
				{
					text3 = "SELECT AT.AccountID  FROM Inventory_Adjustment IA    INNER JOIN Adjustment_Type AT ON IA.AdjustmentTypeID = AT.TypeID\r\n                                 WHERE IA.SysDocID =  '" + sysDocID + "' AND IA.VoucherID = '" + voucherID + "'";
					object obj = ExecuteScalar(text3, sqlTransaction);
					string text11 = "";
					if (obj == null || string.IsNullOrEmpty(obj.ToString()))
					{
						throw new Exception("Adjustment account not found for transactoin " + voucherID);
					}
					text11 = obj.ToString();
					text2 = text6;
					text = text11;
					break;
				}
				case SysDocTypes.InventoryNoneSale:
				{
					text3 = "SELECT AT.AccountID  FROM Inventory_Damage IA    INNER JOIN Adjustment_Type AT ON IA.AdjustmentTypeID = AT.TypeID AND ISNULL(AT.IsNonSale,0) = 1\r\n                                 WHERE IA.SysDocID =  '" + sysDocID + "' AND IA.VoucherID = '" + voucherID + "'";
					object obj = ExecuteScalar(text3, sqlTransaction);
					string text10 = "";
					if (obj == null || string.IsNullOrEmpty(obj.ToString()))
					{
						throw new Exception("Adjustment account not found for transactoin " + voucherID);
					}
					text10 = obj.ToString();
					text2 = text6;
					text = text10;
					break;
				}
				case SysDocTypes.TransitTransferIn:
				case SysDocTypes.ReturnTransitTransfer:
				{
					text3 = "SELECT LOC.InventoryAccountID  FROM Inventory_Transfer IT INNER JOIN Inventory_Transfer_Type ITT ON IT.TransferTypeID = ITT.TypeID\r\n                                 INNER JOIN Location LOC ON Loc.LocationID = ITT.LocationID WHERE (IT.AcceptSysDocID = '" + sysDocID + "' AND IT.AcceptVoucherID = '" + voucherID + "')\r\n\t\t\t\t\t\t\t\t OR  (IT.RejectAcceptSysDocID = '" + sysDocID + "' AND IT.RejectAcceptVoucherID = '" + voucherID + "')";
					object obj = ExecuteScalar(text3, sqlTransaction);
					if (obj == null || string.IsNullOrEmpty(obj.ToString()))
					{
						throw new Exception("Transfer location account not found for transaction : " + voucherID);
					}
					text = obj.ToString();
					text2 = text6;
					break;
				}
				case SysDocTypes.DeliveryNote:
				case SysDocTypes.ExportDeliveryNote:
					text2 = text6;
					text = text7;
					break;
				case SysDocTypes.DeliveryReturn:
					text = text7;
					text2 = text6;
					break;
				case SysDocTypes.CreditSalesReturn:
				case SysDocTypes.CashSalesReturn:
					text = text5;
					text2 = text6;
					break;
				case SysDocTypes.CashPurchaseReturn:
				case SysDocTypes.CreditPurchaseReturn:
					text = text5;
					text2 = text6;
					break;
				}
				if (string.IsNullOrEmpty(text2))
				{
					throw new CompanyException("Inventory Costing cannot be done because credit account not found for item:" + productID + " Transaction Voucher No:" + voucherID + ", Transaction Type:" + sysDocType.ToString());
				}
				if (string.IsNullOrEmpty(text))
				{
					throw new CompanyException("Inventory Costing cannot be done because debit account not found for item:" + productID + " Transaction Voucher No:" + voucherID + ", Transaction Type:" + sysDocType.ToString());
				}
				if (text2 == "")
				{
					throw new CompanyException("Inventory asset account or Credit Account not set for location: " + text4 + " or item: " + productID + " Transaction Voucher No:" + voucherID);
				}
				if (text == "")
				{
					throw new CompanyException("Debit acount not set for location: " + text4 + " or item: " + productID + " Transaction Voucher No:" + voucherID);
				}
				DataRow dataRow3 = glData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text2;
				if (amount > 0m)
				{
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = amount;
				}
				else
				{
					dataRow3["Debit"] = Math.Abs(amount);
					dataRow3["Credit"] = DBNull.Value;
				}
				dataRow3["SysDocID"] = sysDocID;
				dataRow3["VoucherID"] = voucherID;
				dataRow3["IsBaseOnly"] = true;
				dataRow3["Reference"] = "SYS_COGS";
				dataRow3["JDDate"] = dataRow["JournalDate"];
				dataRow3["DocType"] = dataRow["SysDocType"];
				dataRow3["CompanyID"] = "1";
				dataRow3["DivisionID"] = value;
				dataRow3.EndEdit();
				glData.JournalDetailsTable.Rows.Add(dataRow3);
				dataRow3 = glData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text;
				if (amount > 0m)
				{
					dataRow3["Debit"] = amount;
					dataRow3["Credit"] = DBNull.Value;
				}
				else
				{
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = Math.Abs(amount);
				}
				dataRow3["SysDocID"] = sysDocID;
				dataRow3["VoucherID"] = voucherID;
				dataRow3["IsBaseOnly"] = true;
				dataRow3["Reference"] = "SYS_COGS";
				dataRow3["JDDate"] = dataRow["JournalDate"];
				dataRow3["DocType"] = dataRow["SysDocType"];
				dataRow3["CompanyID"] = "1";
				dataRow3["DivisionID"] = value;
				dataRow3.EndEdit();
				glData.JournalDetailsTable.Rows.Add(dataRow3);
				if (sysDocType == SysDocTypes.DeliveryNote || sysDocType == SysDocTypes.ExportDeliveryNote)
				{
					text3 = "SELECT InvoiceSysDocID,InvoiceVoucherID,SD.SysDocType,ISNULL((SELECT ISNULL(P.COGSAccount,CASE WHEN  PC.COGSAccount IS NULL OR PC.COGSAccount <>'' THEN PC.COGSAccount ELSE NULL END)  FROM Product P LEFT OUTER JOIN Product_Class PC ON P.ClassID = PC.ClassID WHERE ProductID = '" + productID + "'), ISNULL(SD.COGSAccountID,LOC.COGSAccountID)) AS COGSAccountID\r\n                                      FROM Delivery_Note DN INNER JOIN System_Document SD ON SD.SysDocID = InvoiceSysDocID \r\n                                    INNER JOIN Location LOC ON Loc.locationID = SD.LocationID\r\n                                    WHERE DN.SysDocID = '" + sysDocID + "' AND VOucherID = '" + voucherID + "'";
					SysDocTypes sysDocTypes = SysDocTypes.None;
					DataSet dataSet = new DataSet();
					FillDataSet(dataSet, "DNote", text3, sqlTransaction);
					if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
					{
						string text12 = dataSet.Tables[0].Rows[0]["InvoiceSysDocID"].ToString();
						string text13 = dataSet.Tables[0].Rows[0]["InvoiceVoucherID"].ToString();
						string text14 = dataSet.Tables[0].Rows[0]["COGSAccountID"].ToString();
						sysDocTypes = (SysDocTypes)int.Parse(dataSet.Tables[0].Rows[0]["SysDocType"].ToString());
						if ((sysDocTypes == SysDocTypes.SalesInvoice || sysDocTypes == SysDocTypes.ExportSalesInvoice) && text12 != "" && text13 != "")
						{
							dataRow3 = glData.JournalDetailsTable.NewRow();
							dataRow3.BeginEdit();
							text2 = text7;
							dataRow3["JournalID"] = 0;
							dataRow3["AccountID"] = text2;
							if (amount > 0m)
							{
								dataRow3["Debit"] = DBNull.Value;
								dataRow3["Credit"] = amount;
							}
							else
							{
								dataRow3["Debit"] = Math.Abs(amount);
								dataRow3["Credit"] = DBNull.Value;
							}
							dataRow3["SysDocID"] = text12;
							dataRow3["VoucherID"] = text13;
							dataRow3["IsBaseOnly"] = true;
							dataRow3["Reference"] = "SYS_COGS";
							dataRow3["CompanyID"] = "1";
							dataRow3["DivisionID"] = value;
							dataRow3.EndEdit();
							glData.JournalDetailsTable.Rows.Add(dataRow3);
							dataRow3 = glData.JournalDetailsTable.NewRow();
							dataRow3.BeginEdit();
							dataRow3["JournalID"] = 0;
							accountData.Tables["System_Document"].Select("SysDocID = '" + sysDocID + "'")[0]["LocationID"].ToString();
							text = (string)(dataRow3["AccountID"] = text14);
							if (amount > 0m)
							{
								dataRow3["Debit"] = amount;
								dataRow3["Credit"] = DBNull.Value;
							}
							else
							{
								dataRow3["Debit"] = DBNull.Value;
								dataRow3["Credit"] = Math.Abs(amount);
							}
							dataRow3["SysDocID"] = text12;
							dataRow3["VoucherID"] = text13;
							dataRow3["IsBaseOnly"] = true;
							dataRow3["Reference"] = "SYS_COGS";
							dataRow3["CompanyID"] = "1";
							dataRow3["DivisionID"] = value;
							dataRow3.EndEdit();
							glData.JournalDetailsTable.Rows.Add(dataRow3);
						}
					}
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		internal DataSet GetRunningTotalsAsOfDate(string productID, DateTime asOfDate, int excludingTransactionID, bool excludeTransfers, SqlTransaction sqlTransaction)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				string text2 = "   SELECT ISNULL(SUM(Quantity - ISNULL(ReturnedQuantity,0)),0) AS TotalQuantity,ISNULL(SUM(AssetValue),0) AS TotalValue FROM Inventory_Transactions IT WHERE   ProductID = '" + productID + "' AND TransactionDate<= '" + text + "' ";
				if (excludingTransactionID > 0)
				{
					text2 = text2 + " AND TransactionID <> " + excludingTransactionID;
				}
				if (excludeTransfers)
				{
					text2 += " AND TransactionType <> 5";
				}
				FillDataSet(dataSet, "IT", text2, sqlTransaction);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal decimal GetProductAverageCostAsOfDate(string productID, string locationID, decimal soldQuantity, DateTime asOfDate, SqlTransaction sqlTransaction)
		{
			try
			{
				bool isLocationCosting = new CompanyInformations(base.DBConfig).GetIsLocationCosting(sqlTransaction);
				string text = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				decimal result = default(decimal);
				string text2 = " SELECT CONVERT(decimal,  SUM(Quantity-ISNULL(ReturnedQuantity,0))) FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate < '" + text + "'";
				if (isLocationCosting)
				{
					text2 = text2 + " AND LocationID = '" + locationID + "'";
				}
				object obj = ExecuteScalar(text2, sqlTransaction);
				if (string.IsNullOrEmpty(obj.ToString()) || decimal.Parse(obj.ToString()) <= soldQuantity)
				{
					text2 = " SELECT TOP 1 AverageCost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' AND TransactionDate >= '" + text + "' ";
					if (isLocationCosting)
					{
						text2 = text2 + " AND LocationID = '" + locationID + "' ";
					}
					text2 += " AND TransactionType IN(1,6,8,18) AND ISNULL(IsNonCostedGRN,'False') = 'False' AND Quantity>0 ORDER BY TransactionDate ASC ";
				}
				else
				{
					text2 = " SELECT TOP 1 AverageCost FROM Inventory_Transactions WHERE ProductID = '" + productID + "' ";
					if (isLocationCosting)
					{
						text2 = text2 + " AND LocationID = '" + locationID + "' ";
					}
					text2 = text2 + " AND TransactionDate <= '" + text + "'  AND TransactionType IN(1,6,8,18)  AND ISNULL(IsNonCostedGRN,'False') = 'False' ORDER BY TransactionDate DESC";
				}
				obj = ExecuteScalar(text2, sqlTransaction);
				if (obj == null || !(obj.ToString() != ""))
				{
					return 0m;
				}
				decimal.TryParse(obj.ToString(), out result);
				return result;
			}
			catch
			{
				throw;
			}
		}

		internal int CreateLotNumber(SqlTransaction sqlTransaction)
		{
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Company", "LastLotNumber", "CompanyID", 1, sqlTransaction);
			int num = 0;
			if (fieldValue != null && fieldValue.ToString() != "")
			{
				num = int.Parse(fieldValue.ToString());
			}
			num++;
			new Databases(base.DBConfig).UpdateFieldValue("Company", "LastLotNumber", num, "CompanyID", 1, sqlTransaction);
			return num;
		}

		private GLData CreateInventoryTransactionGLData(InventoryTransactionData transactionData)
		{
			return new GLData();
		}

		public bool VoidInventoryTransaction(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				string text = "";
				string exp = "SELECT CustomerID FROM InventoryTransaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (text != "")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "Balance", "CustomerID", text, sqlTransaction).ToString(), out result);
					exp = "SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM InventoryTransaction WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					decimal.TryParse(ExecuteScalar(exp, sqlTransaction).ToString(), out result2);
					if (result2 != 0m)
					{
						if (isVoid)
						{
							result -= result2;
						}
						else
						{
							result += result2;
						}
						exp = "UPDATE Customer SET Balance=" + result.ToString() + " WHERE CustomerID='" + text + "'";
						flag &= Update(exp, sqlTransaction);
					}
					exp = "UPDATE ARJOURNAL SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInventoryTransaction(int sysDocType, string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (!AllowDeleteInventoryTransaction(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("This transaction cannot be modified because it is in use or referred by other transactions.");
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				string textCommand = "SELECT * FROM Inventory_Transactions WHERE  SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND SysDocType = " + sysDocType;
				FillDataSet(inventoryTransactionData, "Inventory_Transactions", textCommand);
				if (inventoryTransactionData == null)
				{
					return false;
				}
				if (inventoryTransactionData.Tables[0].Rows.Count == 0)
				{
					return true;
				}
				InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)byte.Parse(inventoryTransactionData.Tables[0].Rows[0]["TransactionType"].ToString());
				StringBuilder stringBuilder = new StringBuilder();
				List<string> list = new List<string>();
				for (int i = 0; i < inventoryTransactionData.Tables[0].Rows.Count; i++)
				{
					string item = inventoryTransactionData.Tables[0].Rows[i]["ProductID"].ToString();
					if (!list.Contains(item))
					{
						list.Add(item);
					}
					string str = "";
					if (i > 0)
					{
						str = ",";
					}
					stringBuilder.AppendLine(str + "'" + AddSingleQuote(inventoryTransactionData.Tables[0].Rows[i]["ProductID"].ToString()) + "' ");
				}
				textCommand = " SELECT ProductID,ItemType, Quantity AS TotalQuantity,AverageCost,LastCost FROM Product WHERE ProductID IN (" + stringBuilder.ToString() + ")";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", textCommand, sqlTransaction);
				textCommand = "SELECT ProductID,LocationID, Quantity AS LocationQuantity FROM Product_Location WHERE ProductID IN (" + stringBuilder.ToString() + ")";
				FillDataSet(dataSet, "Product_Location", textCommand, sqlTransaction);
				new InventoryTransactionData();
				foreach (DataRow row in inventoryTransactionData.Tables[0].Rows)
				{
					string text = row["ProductID"].ToString();
					string text2 = row["LocationID"].ToString();
					DataRow dataRow2 = dataSet.Tables["Product"].Select("ProductID = '" + text + "'")[0];
					ItemTypes itemTypes = (ItemTypes)byte.Parse(dataRow2["ItemType"].ToString());
					row["ItemType"] = (byte)itemTypes;
					if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.ConsignmentItem || itemTypes == ItemTypes.Inventory3PL || itemTypes == ItemTypes.Assembly)
					{
						float num = float.Parse(row["Quantity"].ToString());
						DataRow[] array = dataSet.Tables["Product_Location"].Select("ProductID = '" + text + "' AND LocationID = '" + text2 + "'");
						float result = 0f;
						DataRow dataRow3 = null;
						if (array.Length != 0)
						{
							float.TryParse(array[0]["LocationQuantity"].ToString(), out result);
							dataRow3 = array[0];
						}
						float result2 = 0f;
						float.TryParse(dataRow2["TotalQuantity"].ToString(), out result2);
						decimal result3 = default(decimal);
						decimal.TryParse(dataRow2["AverageCost"].ToString(), out result3);
						if ((inventoryTransactionTypes == InventoryTransactionTypes.Adjustment && num > 0f) || inventoryTransactionTypes == InventoryTransactionTypes.Purchase)
						{
							decimal.Parse(row["UnitPrice"].ToString());
						}
						float num2 = result - num;
						float num3 = result2 - num;
						if (dataRow2 != null)
						{
							dataRow2["TotalQuantity"] = num3;
						}
						if (dataRow3 != null)
						{
							dataRow3["LocationQuantity"] = num2;
						}
					}
				}
				bool result4 = false;
				object obj = null;
				obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.EnableCostRunning, null);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result4);
				}
				flag &= new Products(base.DBConfig).UpdateTotalQuantity(dataSet, sqlTransaction);
				flag &= new Products(base.DBConfig).UpdateLocationQuantity(inventoryTransactionData, isReverse: true, sqlTransaction);
				if (inventoryTransactionTypes == InventoryTransactionTypes.Purchase || inventoryTransactionTypes == InventoryTransactionTypes.Adjustment || inventoryTransactionTypes == InventoryTransactionTypes.ConsignOut || inventoryTransactionTypes == InventoryTransactionTypes.ConsignIn || inventoryTransactionTypes == InventoryTransactionTypes.W3PLGRN || inventoryTransactionTypes == InventoryTransactionTypes.Transfer || inventoryTransactionTypes == InventoryTransactionTypes.CustomerCreditMemo || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryReturn || inventoryTransactionTypes == InventoryTransactionTypes.OpeningInventory || inventoryTransactionTypes == InventoryTransactionTypes.Sale)
				{
					textCommand = "UPDATE Product_Lot SET IsDeleted='True' WHERE ReceiptNumber='" + voucherID + "' AND\r\n\t\t\t\t\t\t\t DocID = '" + sysDocID + "'";
					Update(textCommand, sqlTransaction);
					flag &= RemoveOldLotAllocations(sysDocID, voucherID, sqlTransaction);
				}
				InventoryTransactionData inventoryTransaction = GetInventoryTransaction(sysDocID, voucherID, sqlTransaction);
				flag &= RemoveInvoiceLotAllocation(inventoryTransactionData, sqlTransaction);
				string commandText = "DELETE FROM Inventory_Transactions WHERE SysDocType = " + sysDocType + " AND SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				textCommand = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				if (!isDeletingTransaction)
				{
					return flag;
				}
				AllocateUnallocatedItemsToLot(list.ToArray(), sqlTransaction);
				if (result4)
				{
					return flag;
				}
				UpdateFutureAndPastAffectedTransactionsCOGS(inventoryTransaction, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool AllowDeleteInventoryTransaction(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT COUNT(*) FROM Product_Lot PL \r\n                            INNER JOIN Product P ON PL.ItemCode = P.ProductID\r\n                            WHERE DocID = '" + sysDocID + "' AND ReceiptNumber = '" + voucherID + "' AND (ISNULL(P.IsTrackLot,'False') = 'True' OR ItemType = 5) AND (ISNULL(SoldQty,0) + ISNULL(ReturnedQty,0)>0)";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		internal bool RemoveOldLotAllocations(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				bool flag = true;
				text = "SELECT COUNT(LotNumber) from Product_Lot WHERE ReceiptNumber='" + voucherID + "' AND\r\n\t\t\t\t\t\t\t DocID = '" + sysDocID + "' AND ISNULL(IsDeleted,'False')='True'";
				int num = 0;
				object obj = ExecuteScalar(text, sqlTransaction);
				if (obj != null)
				{
					num = int.Parse(obj.ToString());
				}
				if (num > 0)
				{
					text = " INSERT INTO  Unallocated_Lot_Items\r\n\t\t\t\t\t\t\t (SysDocID,VoucherID,productid,rowindex,locationid,quantity,price)  \r\n\t\t\t\t\t\t\t\t(SELECT docid,InvoiceNumber,itemcode,rowindex,locationid,soldqty,UnitPrice FROM Product_Lot_Sales WHERE LOTNO IN \r\n\t\t\t\t\t\t\t\t(SELECT LotNumber from Product_Lot WHERE DocID = '" + sysDocID + "' AND ReceiptNumber='" + voucherID + "' )) ";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					text = "DELETE FROM Product_Lot_Sales WHERE LOTNO IN (SELECT LotNumber from Product_Lot WHERE DocID = '" + sysDocID + "' AND ReceiptNumber='" + voucherID + "' ) ";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					text = "DELETE FROM Product_Lot WHERE ReceiptNumber='" + voucherID + "' AND\r\n\t\t\t\t\t\t\t DocID = '" + sysDocID + "' AND ISNULL(IsDeleted,'False')='True'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					text = "UPDATE  IT SET IsRecost = 'True' FROM Inventory_Transactions IT INNER JOIN Unallocated_Lot_Items UL\r\n                            ON IT.SysDocID = UL.SysDocID AND IT.VoucherID = UL.VoucherID";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal decimal GetRowAssetValue(string productID, string sysDocID, string voucherID, int rowIndex, SqlTransaction sqlTransaction)
		{
			return GetRowAssetValue(productID, sysDocID, voucherID, rowIndex, mergeWithRefRows: false, sqlTransaction);
		}

		internal decimal GetRowAssetValue(string productID, string sysDocID, string voucherID, int rowIndex, bool mergeWithRefRows, SqlTransaction sqlTransaction)
		{
			return GetRowAssetValue(productID, sysDocID, voucherID, rowIndex, "", mergeWithRefRows, sqlTransaction);
		}

		internal decimal GetRowAssetValue(string productID, string sysDocID, string voucherID, int rowIndex, string transitLocationID, bool mergeWithRefRows, SqlTransaction sqlTransaction)
		{
			string text = "";
			text = "SELECT SUM(ISNULL(AssetValue,0)) AS AssetValue FROM Inventory_Transactions IT   WHERE ";
			if (!transitLocationID.IsNullOrEmpty())
			{
				text = text + " LocationID <> '" + transitLocationID + "' AND ";
			}
			text += " (ProductID = @ProductID AND SysDocID = @SysDocID AND VoucherID = @VoucherID AND RowIndex = @RowIndex) ";
			if (mergeWithRefRows)
			{
				text += " OR (ProductID = @ProductID AND RefSysDocID = @SysDocID AND RefVoucherID = @VoucherID AND RefRowIndex = @RowIndex)";
			}
			SqlCommand sqlCommand = new SqlCommand(text, base.DBConfig.Connection, sqlTransaction);
			sqlCommand.Parameters.AddWithValue("@ProductID", productID);
			sqlCommand.Parameters.AddWithValue("@SysDocID", sysDocID);
			sqlCommand.Parameters.AddWithValue("@VoucherID", voucherID);
			sqlCommand.Parameters.AddWithValue("@RowIndex", rowIndex);
			object obj = sqlCommand.ExecuteScalar();
			if (obj != null && obj.ToString() != "")
			{
				return Convert.ToDecimal(obj.ToString());
			}
			return 0m;
		}

		internal decimal GetRowAssetValue(string productID, string sysDocID, string voucherID, int rowIndex, decimal quantity, SqlTransaction sqlTransaction)
		{
			string text = "";
			text = "SELECT AssetValue FROM Inventory_Transactions IT   WHERE ProductID = '" + productID + "' AND SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND RowIndex = " + rowIndex + " AND Quantity = " + quantity;
			object obj = ExecuteScalar(text);
			if (obj != null && obj.ToString() != "")
			{
				return Convert.ToDecimal(obj.ToString());
			}
			return 0m;
		}

		public bool ValidateInventoryLockDate(InventoryTransactionData inventoryTransactionData, SqlTransaction sqlTransaction)
		{
			DataRow dataRow = inventoryTransactionData.InventoryTransactionTable.Rows[0];
			string idFieldValue = dataRow["SysDocID"].ToString();
			string checkFieldValue = dataRow["VoucherID"].ToString();
			DateTime dateTime = DateTime.Parse(dataRow["TransactionDate"].ToString());
			DateTime t = dateTime;
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Inventory_Transactions", "TransactionDate", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, sqlTransaction);
			if (!fieldValue.IsNullOrEmpty())
			{
				t = DateTime.Parse(fieldValue.ToString());
			}
			DataSet lastClosingPeriod = new CompanyInformations(base.DBConfig).GetLastClosingPeriod();
			if (lastClosingPeriod.Tables.Count > 0 && lastClosingPeriod.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow2 = lastClosingPeriod.Tables[0].Rows[0];
				DateTime t2 = default(DateTime);
				if (!string.IsNullOrEmpty(dataRow2["InventoryCloseDate"].ToString()))
				{
					t2 = DateTime.Parse(dataRow2["InventoryCloseDate"].ToString());
					t2 = new DateTime(t2.Year, t2.Month, t2.Day, 23, 59, 59);
				}
				if (dateTime <= t2 || t <= t2)
				{
					throw new CompanyException("Cannot record a transaction in a closed period.", 1038);
				}
			}
			return true;
		}
	}
}
