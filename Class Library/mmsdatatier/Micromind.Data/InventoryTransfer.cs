using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class InventoryTransfer : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ACCEPTDATE_PARM = "@AcceptDate";

		private const string REJECTDATE_PARM = "@RejectDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string ACCEPTREFERENCE_PARM = "@AcceptReference";

		private const string LOCATIONFROMID_PARM = "@LocationFromID";

		private const string LOCATIONTOID_PARM = "@LocationToID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string DRIVERID_PARM = "@DriverID";

		private const string VEHICLENUMBER_PARM = "@VehicleNumber";

		private const string REJECTREFERENCE_PARM = "@RejectReference";

		private const string REJECTNOTE_PARM = "@RejectNote";

		private const string TRANSFERTYPEID_PARM = "@TransferTypeID";

		private const string ACCEPTSYSDOCID_PARM = "@AcceptSysDocID";

		private const string REJECTSYSDOCID_PARM = "@RejectSysDocID";

		private const string ACCEPTVOUCHERID_PARM = "@AcceptVoucherID";

		private const string REJECTACCEPTSYSDOCID_PARM = "@RejectAcceptSysDocID";

		private const string REJECTACCEPTVOUCHERID_PARM = "@RejectAcceptVoucherID";

		private const string REJECTACCEPTNOTE_PARM = "@RejectAcceptNote";

		private const string REJECTACCEPTREFERENCE_PARM = "@RejectAcceptReference";

		private const string REJECTACCEPTDATE_PARM = "@RejectAcceptDate";

		private const string ISVOID_PARM = "@IsVoid";

		private const string ISACCEPTED_PARM = "@IsAccepted";

		private const string ISREJECTED_PARM = "@IsRejected";

		private const string ACCEPTEDBY_PARM = "@AcceptedBy";

		private const string REJECTEDBY_PARM = "@RejectedBy";

		private const string REJECTACCEPTEDBY_PARM = "@RejectAcceptedBy";

		private const string INVENTORYTRANSFER_TABLE = "Inventory_Transfer";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string UNITID_PARM = "@UnitID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string REMARKS_PARM = "@Remarks";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string ACCEPTEDQUANTITY_PARM = "@AcceptedQuantity";

		private const string ACCEPTEDUNITQUANTITY_PARM = "@AcceptedUnitQuantity";

		private const string REJECTEDQUANTITY_PARM = "@RejectedQuantity";

		private const string REJECTEDUNITQUANTITY_PARM = "@RejectedUnitQuantity";

		private const string LISTVOUCHERID_PARM = "@ListVoucherID";

		private const string LISTSYSDOCID_PARM = "@ListSysDocID";

		private const string LISTROWINDEX_PARM = "@ListRowIndex";

		private const string INVENTORYTRANSFERDETAIL_TABLE = "Inventory_Transfer_Detail";

		public InventoryTransfer(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateInventoryTransferText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Transfer", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("Reference", "@Reference"), new FieldValue("AcceptReference", "@AcceptReference"), new FieldValue("LocationFromID", "@LocationFromID"), new FieldValue("LocationToID", "@LocationToID"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleNumber", "@VehicleNumber"), new FieldValue("TransferTypeID", "@TransferTypeID"), new FieldValue("AcceptSysDocID", "@AcceptSysDocID"), new FieldValue("AcceptVoucherID", "@AcceptVoucherID"), new FieldValue("RejectSysDocID", "@RejectSysDocID"), new FieldValue("RejectReference", "@RejectReference"), new FieldValue("RejectNote", "@RejectNote"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("AcceptDate", "@AcceptDate"), new FieldValue("RejectAcceptSysDocID", "@RejectAcceptSysDocID"), new FieldValue("RejectAcceptVoucherID", "@RejectAcceptVoucherID"), new FieldValue("RejectAcceptDate", "@RejectAcceptDate"), new FieldValue("RejectAcceptNote", "@RejectAcceptNote"), new FieldValue("RejectAcceptReference", "@RejectAcceptReference"), new FieldValue("IsAccepted", "@IsAccepted"), new FieldValue("IsRejected", "@IsRejected"), new FieldValue("AcceptedBy", "@AcceptedBy"), new FieldValue("RejectAcceptedBy", "@RejectAcceptedBy"), new FieldValue("RejectDate", "@RejectDate"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Inventory_Transfer", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryTransferCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryTransferText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryTransferText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@AcceptReference", SqlDbType.NVarChar);
			parameters.Add("@IsAccepted", SqlDbType.Bit);
			parameters.Add("@IsRejected", SqlDbType.Bit);
			parameters.Add("@LocationFromID", SqlDbType.NVarChar);
			parameters.Add("@LocationToID", SqlDbType.NVarChar);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@VehicleNumber", SqlDbType.NVarChar);
			parameters.Add("@AcceptSysDocID", SqlDbType.NVarChar);
			parameters.Add("@AcceptVoucherID", SqlDbType.NVarChar);
			parameters.Add("@AcceptedBy", SqlDbType.NVarChar);
			parameters.Add("@RejectSysDocID", SqlDbType.NVarChar);
			parameters.Add("@RejectReference", SqlDbType.NVarChar);
			parameters.Add("@RejectNote", SqlDbType.NVarChar);
			parameters.Add("@RejectAcceptSysDocID", SqlDbType.NVarChar);
			parameters.Add("@RejectAcceptVoucherID", SqlDbType.NVarChar);
			parameters.Add("@RejectAcceptNote", SqlDbType.NVarChar);
			parameters.Add("@RejectAcceptReference", SqlDbType.NVarChar);
			parameters.Add("@RejectAcceptDate", SqlDbType.DateTime);
			parameters.Add("@RejectAcceptedBy", SqlDbType.NVarChar);
			parameters.Add("@TransferTypeID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@AcceptDate", SqlDbType.DateTime);
			parameters.Add("@RejectDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@IsAccepted"].SourceColumn = "IsAccepted";
			parameters["@IsRejected"].SourceColumn = "IsRejected";
			parameters["@AcceptReference"].SourceColumn = "AcceptReference";
			parameters["@LocationFromID"].SourceColumn = "LocationFromID";
			parameters["@LocationToID"].SourceColumn = "LocationToID";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@AcceptSysDocID"].SourceColumn = "AcceptSysDocID";
			parameters["@AcceptVoucherID"].SourceColumn = "AcceptVoucherID";
			parameters["@AcceptedBy"].SourceColumn = "AcceptedBy";
			parameters["@RejectSysDocID"].SourceColumn = "RejectSysDocID";
			parameters["@RejectReference"].SourceColumn = "RejectReference";
			parameters["@RejectNote"].SourceColumn = "RejectNote";
			parameters["@TransferTypeID"].SourceColumn = "TransferTypeID";
			parameters["@RejectAcceptSysDocID"].SourceColumn = "RejectAcceptSysDocID";
			parameters["@RejectAcceptReference"].SourceColumn = "RejectAcceptReference";
			parameters["@RejectAcceptVoucherID"].SourceColumn = "RejectAcceptVoucherID";
			parameters["@RejectAcceptNote"].SourceColumn = "RejectAcceptNote";
			parameters["@RejectAcceptedBy"].SourceColumn = "RejectAcceptedBy";
			parameters["@RejectAcceptDate"].SourceColumn = "RejectAcceptDate";
			parameters["@VehicleNumber"].SourceColumn = "VehicleNumber";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@AcceptDate"].SourceColumn = "AcceptDate";
			parameters["@RejectDate"].SourceColumn = "RejectDate";
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

		private string GetInsertUpdateInventoryTransferDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Transfer_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("UnitID", "@UnitID"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("Factor", "@Factor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("RejectedQuantity", "@RejectedQuantity"), new FieldValue("RejectedUnitQuantity", "@RejectedUnitQuantity"), new FieldValue("AcceptedQuantity", "@AcceptedQuantity"), new FieldValue("ListVoucherID", "@ListVoucherID"), new FieldValue("ListSysDocID", "@ListSysDocID"), new FieldValue("ListRowIndex", "@ListRowIndex"), new FieldValue("AcceptedUnitQuantity", "@AcceptedUnitQuantity"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryTransferDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryTransferDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryTransferDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Decimal);
			parameters.Add("@UnitQuantity", SqlDbType.Decimal);
			parameters.Add("@Factor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.Char);
			parameters.Add("@AcceptedQuantity", SqlDbType.Decimal);
			parameters.Add("@AcceptedUnitQuantity", SqlDbType.Decimal);
			parameters.Add("@RejectedQuantity", SqlDbType.Decimal);
			parameters.Add("@RejectedUnitQuantity", SqlDbType.Decimal);
			parameters.Add("@ListSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ListVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ListRowIndex", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@Factor"].SourceColumn = "Factor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@AcceptedQuantity"].SourceColumn = "AcceptedQuantity";
			parameters["@AcceptedUnitQuantity"].SourceColumn = "AcceptedUnitQuantity";
			parameters["@RejectedQuantity"].SourceColumn = "RejectedQuantity";
			parameters["@RejectedUnitQuantity"].SourceColumn = "RejectedUnitQuantity";
			parameters["@ListSysDocID"].SourceColumn = "ListSysDocID";
			parameters["@ListVoucherID"].SourceColumn = "ListVoucherID";
			parameters["@ListRowIndex"].SourceColumn = "ListRowIndex";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(InventoryTransferData journalData)
		{
			return true;
		}

		public bool RejectInventoryTransfer(InventoryTransferData inventoryTransferData)
		{
			bool flag = true;
			SqlCommand sqlCommand = new SqlCommand();
			try
			{
				DataRow dataRow = inventoryTransferData.InventoryTransferTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string text3 = dataRow["RejectSysDocID"].ToString();
				string exp = "SELECT COUNT(*) FROM Inventory_Transfer WHERE RejectSysDocID = '" + text3 + "' AND VoucherID = '" + text + "' AND ISNULL(IsRejectAccepted,'False') = 'True'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && obj.ToString() != "" && Convert.ToInt32(obj) > 0)
				{
					throw new CompanyException("This transaction cannot be modified because it has already been accepted.");
				}
				exp = "UPDATE Inventory_Transfer SET RejectSysDocID = @RejectSysDocID, RejectReference = @RejectReference, RejectDate = @RejectDate, RejectNote = @RejectNote, IsRejected= @IsRejected, RejectedBy = @RejectedBy \r\n                                    WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'";
				sqlCommand.Transaction = sqlTransaction;
				sqlCommand.CommandText = exp;
				sqlCommand.Connection = base.DBConfig.Connection;
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.Parameters.AddWithValue("@RejectSysDocID", dataRow["RejectSysDocID"]);
				sqlCommand.Parameters.AddWithValue("@RejectReference", dataRow["RejectReference"]);
				sqlCommand.Parameters.AddWithValue("@RejectDate", dataRow["RejectDate"]);
				sqlCommand.Parameters.AddWithValue("@RejectNote", dataRow["RejectNote"]);
				sqlCommand.Parameters.AddWithValue("@IsRejected", 1);
				sqlCommand.Parameters.AddWithValue("@RejectedBy", base.DBConfig.UserID);
				flag &= (sqlCommand.ExecuteNonQuery() > 0);
				if (flag)
				{
					string entityName = "Inventory Transfer Rejection";
					flag &= AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ReturnTransitTransfer, text2, text, "Inventory_Transfer", sqlTransaction);
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

		public bool AcceptRejectedInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateInventoryTransferCommand = GetInsertUpdateInventoryTransferCommand(isUpdate: true);
			try
			{
				DataRow dataRow = inventoryTransferData.InventoryTransferTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string idFieldValue = dataRow["LocationFromID"].ToString();
				string idFieldValue2 = dataRow["LocationToID"].ToString();
				string a = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
				string text3 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue, sqlTransaction).ToString();
				string text4 = dataRow["TransferTypeID"].ToString();
				string text5 = "";
				text5 = new Databases(base.DBConfig).GetFieldValue("Inventory_Transfer_Type", "LocationID", "TypeID", text4, sqlTransaction).ToString();
				if (text5 == "")
				{
					throw new CompanyException("Transfer location is not selected for the transfer type.\nType:" + text4);
				}
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				foreach (DataRow row in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text6 = row["ProductID"].ToString();
					string text7 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text6, sqlTransaction);
					if (fieldValue != null)
					{
						text7 = fieldValue.ToString();
					}
					if (text7 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text7)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text6, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text6 + "\nUnit:" + row["UnitID"].ToString());
						float num4 = float.Parse(obj["Factor"].ToString());
						string a2 = obj["FactorType"].ToString();
						float num5 = float.Parse(row["RejectedQuantity"].ToString());
						row["RejectedQuantity"] = row["RejectedQuantity"];
						num5 = ((!(a2 == "M")) ? float.Parse(Math.Round(num5 * num4, 5).ToString()) : float.Parse(Math.Round(num5 / num4, 5).ToString()));
						row["RejectedUnitQuantity"] = num5;
					}
					if (row["Quantity"] != DBNull.Value)
					{
						num += float.Parse(row["Quantity"].ToString());
					}
					if (row["AcceptedQuantity"] != DBNull.Value)
					{
						num2 += float.Parse(row["AcceptedQuantity"].ToString());
					}
					if (row["RejectedQuantity"] != DBNull.Value)
					{
						num3 += float.Parse(row["RejectedQuantity"].ToString());
					}
				}
				insertUpdateInventoryTransferCommand.Transaction = sqlTransaction;
				flag &= Update(inventoryTransferData, "Inventory_Transfer", insertUpdateInventoryTransferCommand);
				flag &= DeleteInventoryTransferDetailsRows(text2, text, sqlTransaction);
				insertUpdateInventoryTransferCommand = GetInsertUpdateInventoryTransferDetailsCommand(isUpdate: false);
				insertUpdateInventoryTransferCommand.Transaction = sqlTransaction;
				inventoryTransferData.InventoryTransferDetailsTable.AcceptChanges();
				foreach (DataRow row2 in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					row2.SetAdded();
				}
				if (inventoryTransferData.Tables["Inventory_Transfer_Detail"].Rows.Count > 0)
				{
					flag &= Insert(inventoryTransferData, "Inventory_Transfer_Detail", insertUpdateInventoryTransferCommand);
				}
				string commandText = "UPDATE Inventory_Transfer SET  IsRejectAccepted= 'True', RejectAcceptedBy = '" + base.DBConfig.UserID + "'\r\n                                    WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'";
				insertUpdateInventoryTransferCommand.Transaction = sqlTransaction;
				insertUpdateInventoryTransferCommand.CommandText = commandText;
				insertUpdateInventoryTransferCommand.Connection = base.DBConfig.Connection;
				insertUpdateInventoryTransferCommand.CommandType = CommandType.Text;
				insertUpdateInventoryTransferCommand.Parameters.AddWithValue("@RejectReference", dataRow["RejectReference"]);
				insertUpdateInventoryTransferCommand.Parameters.AddWithValue("@RejectDate", dataRow["RejectDate"]);
				insertUpdateInventoryTransferCommand.Parameters.AddWithValue("@RejectNote", dataRow["RejectNote"]);
				flag &= (insertUpdateInventoryTransferCommand.ExecuteNonQuery() > 0);
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = dataRow["RejectAcceptSysDocID"];
					dataRow4["VoucherID"] = dataRow["RejectAcceptVoucherID"];
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["LocationID"] = text5;
					dataRow4["ProductID"] = row3["ProductID"];
					dataRow4["UnitID"] = row3["UnitID"];
					dataRow4["UnitPrice"] = 0;
					dataRow4["Quantity"] = -1f * float.Parse(row3["RejectedQuantity"].ToString());
					if (row3["RejectedUnitQuantity"] != DBNull.Value)
					{
						dataRow4["UnitQuantity"] = -1f * float.Parse(row3["RejectedUnitQuantity"].ToString());
					}
					else
					{
						dataRow4["UnitQuantity"] = DBNull.Value;
					}
					dataRow4["Factor"] = row3["Factor"];
					dataRow4["FactorType"] = row3["FactorType"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row3["RowIndex"];
					dataRow4["SysDocType"] = (byte)21;
					dataRow4["TransactionDate"] = dataRow["RejectAcceptDate"];
					dataRow4["TransactionType"] = (byte)5;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
					dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = dataRow["RejectAcceptSysDocID"];
					dataRow4["VoucherID"] = dataRow["RejectAcceptVoucherID"];
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["LocationID"] = dataRow["LocationFromID"];
					dataRow4["ProductID"] = row3["ProductID"];
					dataRow4["UnitID"] = row3["UnitID"];
					dataRow4["Quantity"] = row3["RejectedQuantity"];
					dataRow4["UnitQuantity"] = row3["RejectedUnitQuantity"];
					dataRow4["Factor"] = row3["Factor"];
					dataRow4["FactorType"] = row3["FactorType"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row3["RowIndex"];
					dataRow4["SysDocType"] = (byte)21;
					dataRow4["TransactionDate"] = dataRow["RejectAcceptDate"];
					dataRow4["TransactionType"] = (byte)5;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(inventoryTransferData, isUpdate: false, sqlTransaction);
				inventoryTransactionData.Merge(inventoryTransferData.Tables["Product_Lot_Issue_Detail"]);
				inventoryTransactionData.Merge(inventoryTransferData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(inventoryTransferData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				if (a != text3)
				{
					if (a == "" || text3 == "")
					{
						throw new CompanyException("Asset account is not set for source or destination location.");
					}
					GLData journalData = CreateInventoryTransferRejectGLData(inventoryTransferData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				}
				if (flag)
				{
					string entityName = "Rejected Transfer Acceptance";
					flag = ((!isUpdate) ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)));
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["RejectAcceptSysDocID"].ToString(), dataRow["RejectAcceptVoucherID"].ToString(), "Inventory_Transfer", "RejectAcceptVoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.TransitTransferIn, text2, text, "Inventory_Transfer", sqlTransaction);
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

		public bool AcceptInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate)
		{
			return AcceptInventoryTransfer(inventoryTransferData, isUpdate, null);
		}

		public bool AcceptInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateInventoryTransferCommand = GetInsertUpdateInventoryTransferCommand(isUpdate: true);
			bool flag2 = false;
			try
			{
				DataRow dataRow = inventoryTransferData.InventoryTransferTable.Rows[0];
				string voucherID = dataRow["VoucherID"].ToString();
				string text = dataRow["AcceptSysDocID"].ToString();
				string text2 = dataRow["AcceptVoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				string text3 = dataRow["TransferTypeID"].ToString();
				string text4 = "";
				text4 = new Databases(base.DBConfig).GetFieldValue("Inventory_Transfer_Type", "LocationID", "TypeID", text3, sqlTransaction).ToString();
				if (text4 == "")
				{
					throw new CompanyException("Transfer location is not selected for the transfer type.\nType:" + text3);
				}
				dataRow["AcceptedBy"] = base.DBConfig.UserID;
				bool result = false;
				if (dataRow["IsRejected"] != DBNull.Value)
				{
					bool.TryParse(dataRow["IsRejected"].ToString(), out result);
				}
				if (result)
				{
					throw new CompanyException("This inventory transfer transaction cannot be changed because it has some rejected quantities.", 1043);
				}
				dataRow["LocationFromID"].ToString();
				string idFieldValue = dataRow["LocationToID"].ToString();
				string a = new Databases(base.DBConfig).GetFieldValue("Inventory_Transfer_Type", "AccountID", "TypeID", text3, sqlTransaction).ToString();
				string b = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue, sqlTransaction).ToString();
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
					flag2 = true;
				}
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				foreach (DataRow row in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text5 = row["ProductID"].ToString();
					string text6 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text5, sqlTransaction);
					if (fieldValue != null)
					{
						text6 = fieldValue.ToString();
					}
					if (text6 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text6)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text5, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text5 + "\nUnit:" + row["UnitID"].ToString());
						float num5 = float.Parse(obj["Factor"].ToString());
						string a2 = obj["FactorType"].ToString();
						float num6 = float.Parse(row["AcceptedQuantity"].ToString());
						row["AcceptedUnitQuantity"] = row["AcceptedQuantity"];
						num6 = ((!(a2 == "M")) ? float.Parse(Math.Round(num6 * num5, 5).ToString()) : float.Parse(Math.Round(num6 / num5, 5).ToString()));
						row["AcceptedQuantity"] = num6;
						num4 += float.Parse(row["AcceptedUnitQuantity"].ToString());
					}
					if (row["Quantity"] != DBNull.Value)
					{
						num += float.Parse(row["Quantity"].ToString());
					}
					if (row["AcceptedQuantity"] != DBNull.Value && row["AcceptedUnitQuantity"] == DBNull.Value)
					{
						num2 += float.Parse(row["AcceptedQuantity"].ToString());
					}
					if (row["RejectedQuantity"] != DBNull.Value)
					{
						num3 += float.Parse(row["RejectedQuantity"].ToString());
					}
				}
				if (num <= num2 + num3)
				{
					dataRow["IsAccepted"] = true;
				}
				else if (num <= num2 + num4 + num3)
				{
					dataRow["IsAccepted"] = true;
				}
				else
				{
					dataRow["IsAccepted"] = false;
				}
				insertUpdateInventoryTransferCommand.Transaction = sqlTransaction;
				flag &= Update(inventoryTransferData, "Inventory_Transfer", insertUpdateInventoryTransferCommand);
				flag &= DeleteInventoryTransferAcceptanceDetailsRows(sysDocID, voucherID, text, text2, sqlTransaction);
				insertUpdateInventoryTransferCommand = GetInsertUpdateInventoryTransferDetailsCommand(isUpdate: false);
				insertUpdateInventoryTransferCommand.Transaction = sqlTransaction;
				inventoryTransferData.InventoryTransferDetailsTable.AcceptChanges();
				foreach (DataRow row2 in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					row2.SetAdded();
				}
				if (inventoryTransferData.Tables["Inventory_Transfer_Detail"].Rows.Count > 0)
				{
					flag &= Insert(inventoryTransferData, "Inventory_Transfer_Detail", insertUpdateInventoryTransferCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = dataRow["AcceptSysDocID"];
					dataRow4["VoucherID"] = dataRow["AcceptVoucherID"];
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["LocationID"] = text4;
					dataRow4["ProductID"] = row3["ProductID"];
					dataRow4["UnitID"] = row3["UnitID"];
					dataRow4["UnitPrice"] = 0;
					dataRow4["Quantity"] = -1f * float.Parse(row3["AcceptedQuantity"].ToString());
					if (row3["AcceptedUnitQuantity"] != DBNull.Value)
					{
						dataRow4["UnitQuantity"] = -1f * float.Parse(row3["AcceptedUnitQuantity"].ToString());
					}
					else
					{
						dataRow4["UnitQuantity"] = DBNull.Value;
					}
					dataRow4["Factor"] = row3["Factor"];
					dataRow4["FactorType"] = row3["FactorType"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row3["RowIndex"];
					dataRow4["SysDocType"] = (byte)20;
					dataRow4["TransactionDate"] = dataRow["AcceptDate"];
					dataRow4["TransactionType"] = (byte)5;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
					dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = dataRow["AcceptSysDocID"];
					dataRow4["VoucherID"] = dataRow["AcceptVoucherID"];
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["LocationID"] = dataRow["LocationToID"];
					dataRow4["ProductID"] = row3["ProductID"];
					dataRow4["UnitID"] = row3["UnitID"];
					dataRow4["Quantity"] = row3["AcceptedQuantity"];
					dataRow4["UnitQuantity"] = row3["AcceptedUnitQuantity"];
					dataRow4["Factor"] = row3["Factor"];
					dataRow4["FactorType"] = row3["FactorType"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row3["RowIndex"];
					dataRow4["SysDocType"] = (byte)20;
					dataRow4["TransactionDate"] = dataRow["AcceptDate"];
					dataRow4["TransactionType"] = (byte)5;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(inventoryTransferData, isUpdate: false, sqlTransaction);
				inventoryTransactionData.Merge(inventoryTransferData.Tables["Product_Lot_Issue_Detail"]);
				inventoryTransactionData.Merge(inventoryTransferData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(inventoryTransferData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				if (a != b)
				{
					GLData journalData = CreateInventoryTransferAcceptanceGLData(inventoryTransferData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Inventory_Transfer", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Inventory Transfer In";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, text, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, text, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["AcceptSysDocID"].ToString(), dataRow["AcceptVoucherID"].ToString(), "Inventory_Transfer", "AcceptVoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.TransitTransferIn, text, voucherID, "Inventory_Transfer", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				if (flag2)
				{
					base.DBConfig.EndTransaction(flag);
				}
			}
		}

		public bool InsertUpdateInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate)
		{
			return InsertUpdateInventoryTransfer(inventoryTransferData, isUpdate, null);
		}

		public bool InsertUpdateInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateInventoryTransferCommand = GetInsertUpdateInventoryTransferCommand(isUpdate);
			bool flag2 = false;
			try
			{
				DataRow dataRow = inventoryTransferData.InventoryTransferTable.Rows[0];
				if (sqlTransaction == null)
				{
					flag2 = true;
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate)
				{
					if (new SystemDocuments(base.DBConfig).ExistDocumentNumber("Inventory_Transfer", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
					{
						throw new CompanyException("Document number already exist.", 1046);
					}
				}
				else
				{
					string exp = "SELECT SUM(ISNULL(AcceptedQuantity,0) + ISNULL(RejectedQuantity,0)) FROM Inventory_Transfer_Detail WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'";
					object obj = ExecuteScalar(exp, sqlTransaction);
					if (obj != null && obj.ToString() != "" && decimal.Parse(obj.ToString()) > 0m)
					{
						throw new CompanyException("Cannot edit because the transfer is already received.");
					}
				}
				string idFieldValue = dataRow["LocationFromID"].ToString();
				string idFieldValue2 = dataRow["LocationToID"].ToString();
				string text3 = dataRow["TransferTypeID"].ToString();
				string a = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue, sqlTransaction).ToString();
				string text4 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
				string text5 = "";
				text5 = new Databases(base.DBConfig).GetFieldValue("Inventory_Transfer_Type", "LocationID", "TypeID", text3, sqlTransaction).ToString();
				if (text5 == "")
				{
					throw new CompanyException("Transfer location is not selected for the transfer type.\nType:" + text3);
				}
				foreach (DataRow row in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text6 = row["ProductID"].ToString();
					string text7 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text6, sqlTransaction);
					if (fieldValue != null)
					{
						text7 = fieldValue.ToString();
					}
					if (text7 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text7)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text6, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text6 + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj2["Factor"].ToString());
						string text8 = obj2["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["Factor"] = num;
						row["FactorType"] = text8;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text8 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdateInventoryTransferCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(inventoryTransferData, "Inventory_Transfer", insertUpdateInventoryTransferCommand)) : (flag & Insert(inventoryTransferData, "Inventory_Transfer", insertUpdateInventoryTransferCommand)));
				insertUpdateInventoryTransferCommand = GetInsertUpdateInventoryTransferDetailsCommand(isUpdate: false);
				insertUpdateInventoryTransferCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteInventoryTransferDetailsRows(text2, text, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(19, text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				if (inventoryTransferData.Tables["Inventory_Transfer_Detail"].Rows.Count > 0)
				{
					flag &= Insert(inventoryTransferData, "Inventory_Transfer_Detail", insertUpdateInventoryTransferCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row2["SysDocID"];
					dataRow4["VoucherID"] = row2["VoucherID"];
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["LocationID"] = dataRow["LocationFromID"];
					dataRow4["ProductID"] = row2["ProductID"];
					dataRow4["UnitID"] = row2["UnitID"];
					dataRow4["UnitPrice"] = 0;
					dataRow4["Quantity"] = -1f * float.Parse(row2["Quantity"].ToString());
					if (row2["UnitQuantity"] != DBNull.Value)
					{
						dataRow4["UnitQuantity"] = -1f * float.Parse(row2["UnitQuantity"].ToString());
					}
					else
					{
						dataRow4["UnitQuantity"] = DBNull.Value;
					}
					dataRow4["Factor"] = row2["Factor"];
					dataRow4["FactorType"] = row2["FactorType"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row2["RowIndex"];
					dataRow4["SysDocType"] = (byte)19;
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)5;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
					dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row2["SysDocID"];
					dataRow4["VoucherID"] = row2["VoucherID"];
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["LocationID"] = text5;
					dataRow4["ProductID"] = row2["ProductID"];
					dataRow4["UnitID"] = row2["UnitID"];
					dataRow4["Quantity"] = float.Parse(row2["Quantity"].ToString());
					if (row2["UnitQuantity"] != DBNull.Value)
					{
						dataRow4["UnitQuantity"] = float.Parse(row2["UnitQuantity"].ToString());
					}
					else
					{
						dataRow4["UnitQuantity"] = DBNull.Value;
					}
					dataRow4["Factor"] = row2["Factor"];
					dataRow4["FactorType"] = row2["FactorType"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row2["RowIndex"];
					dataRow4["SysDocType"] = (byte)19;
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)5;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(inventoryTransferData, isUpdate: false, sqlTransaction);
				inventoryTransactionData.Merge(inventoryTransferData.Tables["Product_Lot_Issue_Detail"]);
				inventoryTransactionData.Merge(inventoryTransferData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(inventoryTransferData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				if (a != text4)
				{
					if (a == "" || text4 == "")
					{
						throw new CompanyException("Inventory Asset account is not set for source or destination location.", 2007);
					}
					GLData journalData = CreateInventoryTransferGLData(inventoryTransferData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Inventory_Transfer", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Inventory Transfer";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Inventory_Transfer", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.TransitTransferOut, text2, text, "Inventory_Transfer", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				if (flag2)
				{
					base.DBConfig.EndTransaction(flag);
				}
			}
		}

		public bool InsertUpdateDirectInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateInventoryTransferCommand = GetInsertUpdateInventoryTransferCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = inventoryTransferData.InventoryTransferTable.Rows[0];
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Inventory_Transfer", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string idFieldValue = dataRow["LocationFromID"].ToString();
				string idFieldValue2 = dataRow["LocationToID"].ToString();
				string a = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue, sqlTransaction).ToString();
				string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", idFieldValue2, sqlTransaction).ToString();
				dataRow["IsAccepted"] = true;
				foreach (DataRow row in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					row["AcceptedQuantity"] = row["Quantity"];
					string text3 = row["ProductID"].ToString();
					string text4 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text3, sqlTransaction);
					if (fieldValue != null)
					{
						text4 = fieldValue.ToString();
					}
					if (text4 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text4)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text3, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text3 + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj["Factor"].ToString());
						string text5 = obj["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["Factor"] = num;
						row["FactorType"] = text5;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text5 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdateInventoryTransferCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(inventoryTransferData, "Inventory_Transfer", insertUpdateInventoryTransferCommand)) : (flag & Insert(inventoryTransferData, "Inventory_Transfer", insertUpdateInventoryTransferCommand)));
				insertUpdateInventoryTransferCommand = GetInsertUpdateInventoryTransferDetailsCommand(isUpdate: false);
				insertUpdateInventoryTransferCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteInventoryTransferDetailsRows(sysDocID, text, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(40, sysDocID, text, isDeletingTransaction: false, sqlTransaction);
				}
				if (inventoryTransferData.Tables["Inventory_Transfer_Detail"].Rows.Count > 0)
				{
					flag &= Insert(inventoryTransferData, "Inventory_Transfer_Detail", insertUpdateInventoryTransferCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in inventoryTransferData.InventoryTransferDetailsTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row2["SysDocID"];
					dataRow4["VoucherID"] = row2["VoucherID"];
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["LocationID"] = dataRow["LocationFromID"];
					dataRow4["ProductID"] = row2["ProductID"];
					dataRow4["UnitID"] = row2["UnitID"];
					dataRow4["Quantity"] = -1f * float.Parse(row2["Quantity"].ToString());
					if (row2["UnitQuantity"] != DBNull.Value)
					{
						dataRow4["UnitQuantity"] = -1f * float.Parse(row2["UnitQuantity"].ToString());
					}
					else
					{
						dataRow4["UnitQuantity"] = DBNull.Value;
					}
					dataRow4["Factor"] = row2["Factor"];
					dataRow4["FactorType"] = row2["FactorType"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row2["RowIndex"];
					dataRow4["SysDocType"] = (byte)40;
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)5;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
					dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row2["SysDocID"];
					dataRow4["VoucherID"] = row2["VoucherID"];
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["LocationID"] = dataRow["LocationToID"];
					dataRow4["ProductID"] = row2["ProductID"];
					dataRow4["UnitID"] = row2["UnitID"];
					dataRow4["Quantity"] = float.Parse(row2["Quantity"].ToString());
					if (row2["UnitQuantity"] != DBNull.Value)
					{
						dataRow4["UnitQuantity"] = float.Parse(row2["UnitQuantity"].ToString());
					}
					else
					{
						dataRow4["UnitQuantity"] = DBNull.Value;
					}
					dataRow4["Factor"] = row2["Factor"];
					dataRow4["FactorType"] = row2["FactorType"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row2["RowIndex"];
					dataRow4["SysDocType"] = (byte)40;
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)5;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(inventoryTransferData, isUpdate: false, sqlTransaction);
				inventoryTransactionData.Merge(inventoryTransferData.Tables["Product_Lot_Issue_Detail"]);
				inventoryTransactionData.Merge(inventoryTransferData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(inventoryTransferData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				if (a != text2)
				{
					if (a == "" || text2 == "")
					{
						throw new CompanyException("Inventory Asset account is not set for source or destination location.", 2007);
					}
					GLData journalData = CreateDirectInventoryTransferGLData(inventoryTransferData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Inventory_Transfer", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Inventory Transfer";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Inventory_Transfer", "VoucherID", sqlTransaction);
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

		private GLData CreateInventoryTransferAcceptanceGLData(InventoryTransferData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.InventoryTransferTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string locationID = dataRow["LocationFromID"].ToString();
			string locationID2 = dataRow["LocationToID"].ToString();
			dataRow["AcceptSysDocID"].ToString();
			string idFieldValue = dataRow["TransferTypeID"].ToString();
			string sysDocID = dataRow["AcceptSysDocID"].ToString();
			string voucherID = dataRow["AcceptVoucherID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			string text = new Databases(base.DBConfig).GetFieldValue("Inventory_Transfer_Type", "LocationID", "TypeID", idFieldValue, sqlTransaction).ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			if (text2 == "")
			{
				throw new CompanyException("Inventory transfer account is not set for selected transfer location.");
			}
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.TransitTransferIn;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["AcceptDate"];
			dataRow2["SysDocID"] = dataRow["AcceptSysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["AcceptVoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Inventory Transfer In";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Products products = new Products(base.DBConfig);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			string text3 = "";
			foreach (DataRow row in transactionData.InventoryTransferDetailsTable.Rows)
			{
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				string productID = row["ProductID"].ToString();
				decimal d = Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, sysDocID, voucherID, rowIndex, text, mergeWithRefRows: false, sqlTransaction));
				products.GetProductAccountIDByLocation(productID, locationID, Products.ProductAccounts.AssetAccount, sqlTransaction);
				text3 = products.GetProductAccountIDByLocation(productID, locationID2, Products.ProductAccounts.AssetAccount, sqlTransaction);
				if (hashtable.ContainsKey(text3))
				{
					num2 = decimal.Parse(hashtable[text3].ToString());
					num2 += Math.Round(d, currencyDecimalPoints);
					hashtable[text3] = num2;
				}
				else
				{
					hashtable.Add(text3, Math.Round(d, currencyDecimalPoints));
					arrayList.Add(text3);
				}
				num += Math.Round(d, currencyDecimalPoints);
			}
			DataRow dataRow3 = null;
			if (num != 0m)
			{
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text2;
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = num;
				dataRow3["Description"] = dataRow["Description"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["JVEntryType"] = (byte)1;
				dataRow3["CompanyID"] = value;
				dataRow3["DivisionID"] = value2;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					text3 = (string)(dataRow3["AccountID"] = arrayList[i].ToString());
					num2 = decimal.Parse(hashtable[text3].ToString());
					dataRow3["Debit"] = num2;
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["JVEntryType"] = (byte)1;
					dataRow3["CompanyID"] = value;
					dataRow3["DivisionID"] = value2;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			return gLData;
		}

		private GLData CreateInventoryTransferRejectGLData(InventoryTransferData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.InventoryTransferTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string locationID = dataRow["LocationFromID"].ToString();
			dataRow["LocationToID"].ToString();
			dataRow["RejectAcceptVoucherID"].ToString();
			string idFieldValue = dataRow["TransferTypeID"].ToString();
			string text = new Databases(base.DBConfig).GetFieldValue("Inventory_Transfer_Type", "LocationID", "TypeID", idFieldValue, sqlTransaction).ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			string sysDocID = dataRow["RejectAcceptSysDocID"].ToString();
			string voucherID = dataRow["RejectAcceptVoucherID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			if (text2 == "")
			{
				throw new CompanyException("Inventory transfer account is not set for selected transfer type.");
			}
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.ReturnTransitTransfer;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["RejectAcceptDate"];
			dataRow2["SysDocID"] = dataRow["RejectAcceptSysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["RejectAcceptVoucherID"];
			dataRow2["Reference"] = dataRow["RejectAcceptReference"];
			dataRow2["Narration"] = "Inventory Transfer Rejection";
			dataRow2["Note"] = dataRow["RejectAcceptNote"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Products products = new Products(base.DBConfig);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			string text3 = "";
			foreach (DataRow row in transactionData.InventoryTransferDetailsTable.Rows)
			{
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				string productID = row["ProductID"].ToString();
				decimal d = Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, sysDocID, voucherID, rowIndex, text, mergeWithRefRows: false, sqlTransaction));
				text3 = products.GetProductAccountIDByLocation(productID, locationID, Products.ProductAccounts.AssetAccount, sqlTransaction);
				if (hashtable.ContainsKey(text3))
				{
					num2 = decimal.Parse(hashtable[text3].ToString());
					num2 += Math.Round(d, currencyDecimalPoints);
					hashtable[text3] = num2;
				}
				else
				{
					hashtable.Add(text3, Math.Round(d, currencyDecimalPoints));
					arrayList.Add(text3);
				}
				num += Math.Round(d, currencyDecimalPoints);
			}
			DataRow dataRow3 = null;
			if (num != 0m)
			{
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text2;
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = num;
				dataRow3["Description"] = dataRow["Description"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["CompanyID"] = value;
				dataRow3["DivisionID"] = value2;
				dataRow3["JVEntryType"] = (byte)1;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					text3 = (string)(dataRow3["AccountID"] = arrayList[i].ToString());
					num2 = decimal.Parse(hashtable[text3].ToString());
					dataRow3["Debit"] = num2;
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["CompanyID"] = value;
					dataRow3["DivisionID"] = value2;
					dataRow3["JVEntryType"] = (byte)1;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			return gLData;
		}

		private GLData CreateDirectInventoryTransferGLData(InventoryTransferData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.InventoryTransferTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string locationID = dataRow["LocationFromID"].ToString();
			string text = dataRow["LocationToID"].ToString();
			string text2 = "";
			dataRow["TransferTypeID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.DirectInventoryTransfer;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Inventory Transfer";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			string sysDocID = dataRow["SysDocID"].ToString();
			string voucherID = dataRow["VoucherID"].ToString();
			Products products = new Products(base.DBConfig);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable2 = new Hashtable();
			ArrayList arrayList2 = new ArrayList();
			decimal d = default(decimal);
			decimal num = default(decimal);
			string text3 = "";
			foreach (DataRow row in transactionData.InventoryTransferDetailsTable.Rows)
			{
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				string productID = row["ProductID"].ToString();
				decimal.Parse(row["Quantity"].ToString());
				decimal d2 = Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, sysDocID, voucherID, rowIndex, text, mergeWithRefRows: false, sqlTransaction));
				text2 = products.GetProductAccountIDByLocation(productID, locationID, Products.ProductAccounts.AssetAccount, sqlTransaction);
				string productAccountIDByLocation = products.GetProductAccountIDByLocation(productID, text, Products.ProductAccounts.AssetAccount, sqlTransaction);
				text3 = text2;
				if (hashtable.ContainsKey(text3))
				{
					num = decimal.Parse(hashtable[text3].ToString());
					num += Math.Round(d2, currencyDecimalPoints);
					hashtable[text3] = num;
				}
				else
				{
					hashtable.Add(text3, Math.Round(d2, currencyDecimalPoints));
					arrayList.Add(text3);
				}
				text3 = productAccountIDByLocation;
				if (hashtable2.ContainsKey(text3))
				{
					num = decimal.Parse(hashtable2[text3].ToString());
					num += Math.Round(d2, currencyDecimalPoints);
					hashtable2[text3] = num;
				}
				else
				{
					hashtable2.Add(text3, Math.Round(d2, currencyDecimalPoints));
					arrayList2.Add(text3);
				}
				d += Math.Round(d2, currencyDecimalPoints);
			}
			DataRow dataRow3 = null;
			if (d != 0m)
			{
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					text3 = (string)(dataRow3["AccountID"] = arrayList[i].ToString());
					num = decimal.Parse(hashtable[text3].ToString());
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = num;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["CompanyID"] = value;
					dataRow3["DivisionID"] = value2;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				for (int j = 0; j < hashtable2.Count; j++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					text3 = arrayList2[j].ToString();
					num = decimal.Parse(hashtable2[text3].ToString());
					dataRow3["AccountID"] = text3;
					dataRow3["Debit"] = num;
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["CompanyID"] = value;
					dataRow3["DivisionID"] = value2;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			return gLData;
		}

		private GLData CreateInventoryTransferGLData(InventoryTransferData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.InventoryTransferTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string locationID = dataRow["LocationFromID"].ToString();
			dataRow["LocationToID"].ToString();
			string idFieldValue = dataRow["TransferTypeID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			string text = new Databases(base.DBConfig).GetFieldValue("Inventory_Transfer_Type", "LocationID", "TypeID", idFieldValue, sqlTransaction).ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			if (text2 == "")
			{
				throw new CompanyException("Inventory asset account is not set for selected transfer location.\nLocation:" + text);
			}
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.TransitTransferOut;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Inventory Transfer Out";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			string sysDocID = dataRow["SysDocID"].ToString();
			string voucherID = dataRow["VoucherID"].ToString();
			Products products = new Products(base.DBConfig);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			string text3 = "";
			foreach (DataRow row in transactionData.InventoryTransferDetailsTable.Rows)
			{
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				string productID = row["ProductID"].ToString();
				decimal d = Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, sysDocID, voucherID, rowIndex, text, mergeWithRefRows: false, sqlTransaction));
				text3 = products.GetProductAccountIDByLocation(productID, locationID, Products.ProductAccounts.AssetAccount, sqlTransaction);
				if (hashtable.ContainsKey(text3))
				{
					num2 = decimal.Parse(hashtable[text3].ToString());
					num2 += Math.Round(d, currencyDecimalPoints);
					hashtable[text3] = num2;
				}
				else
				{
					hashtable.Add(text3, Math.Round(d, currencyDecimalPoints));
					arrayList.Add(text3);
				}
				num += Math.Round(d, currencyDecimalPoints);
			}
			DataRow dataRow3 = null;
			if (num != 0m)
			{
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					text3 = (string)(dataRow3["AccountID"] = arrayList[i].ToString());
					num2 = decimal.Parse(hashtable[text3].ToString());
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = Math.Round(num2, currencyDecimalPoints);
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["CompanyID"] = value;
					dataRow3["DivisionID"] = value2;
					dataRow3["JVEntryType"] = (byte)1;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text2;
				dataRow3["Debit"] = Math.Round(num, currencyDecimalPoints);
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["Description"] = dataRow["Description"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["CompanyID"] = value;
				dataRow3["DivisionID"] = value2;
				dataRow3["JVEntryType"] = (byte)1;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
			}
			return gLData;
		}

		public DataSet GetInventoryTransfersToAccept(string locationID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT SysDocID,VoucherID AS Number,TransferTypeID [Type], TransactionDate [Date],LocationFromID [From],LocationToID [To],1 AS Reason,Description\r\n                               FROM         Inventory_Transfer\r\n                                WHERE ISNULL(IsAccepted,'False')='False' AND  ISNULL(IsRejected,'False') = 'False' AND ISNULL(IsVoid,'False')='False'\r\n                                AND LocationToID='" + locationID + "' ";
				str = str + " UNION \r\n                                SELECT SysDocID,VoucherID AS Number,TransferTypeID [Type], TransactionDate [Date],LocationToID [From],LocationFromID [To],2 AS Reason,Description\r\n                                FROM         Inventory_Transfer\r\n                                WHERE ISNULL(IsAccepted,'False')='False' AND ISNULL(IsVoid,'False')='False' AND ISNULL(IsRejected,'False') = 'True' AND ISNULL(IsRejectAccepted,'False')='False'\r\n                                AND LocationFromID = '" + locationID + "' ORDER BY Date ASC";
				FillDataSet(dataSet, "Inventory_Transfer", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryTransfersToReturn(string locationID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID,VoucherID AS Number, TransactionDate [Date],LocationFromID [From],LocationToID [To],Description\r\n                               FROM         Inventory_Transfer\r\n                                WHERE ISNULL(IsAccepted,'False')='False'  AND ISNULL(IsRejected,'False') = 'False' AND ISNULL(IsVoid,'False')='False'\r\n                                AND LocationToID='" + locationID + "'";
				FillDataSet(dataSet, "Inventory_Transfer", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public InventoryTransferData GetInventoryTransferByID(string sysDocID, string voucherID)
		{
			try
			{
				InventoryTransferData inventoryTransferData = new InventoryTransferData();
				string textCommand = "SELECT IT.*, ITT.LocationID AS TransitLocationID FROM Inventory_Transfer IT\r\n                                LEFT OUTER JOIN Inventory_Transfer_Type ITT ON IT.TransferTypeID = ITT.TypeID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryTransferData, "Inventory_Transfer", textCommand);
				if (inventoryTransferData == null || inventoryTransferData.Tables.Count == 0 || inventoryTransferData.Tables["Inventory_Transfer"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.SysDocID,TD.VoucherID,TD.ProductID,TD.UnitID, ISNULL(TD.UnitQuantity,TD.Quantity) as Quantity,TD.Factor,TD.FactorType,ISNULL(TD.AcceptedUnitQuantity,TD.AcceptedQuantity) as AcceptedQuantity,TD.AcceptedFactor,TD.AcceptedFactorType,ISNULL(TD.RejectedUnitQuantity,TD.RejectedQuantity),\r\n                        TD.RejectedFactor,TD.RejectedFactorType,TD.RowIndex,TD.Remarks,Product.Description,CASE WHEN ItemType = 5 THEN 'True' ELSE IsTrackLot END  AS IsTrackLot,IsTrackSerial\r\n                        FROM Inventory_Transfer_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex";
				FillDataSet(inventoryTransferData, "Inventory_Transfer_Detail", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (inventoryTransferData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					inventoryTransferData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				inventoryTransferData.Merge(transactionIssuesProductLots, preserveChanges: false);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryTransferData, "Product_Lot_Receiving_Detail", textCommand);
				textCommand = "SELECT PL.LotNumber,PL.Reference,PL.SourceLotNumber,PL.LocationID,PL.ItemCode AS ProductID,PL.RowIndex,ISNULL(PL2.ProductionDate,PL.ProductionDate) AS ProductionDate,ISNULL(PL2.ExpiryDate,PL.ExpiryDate) AS ExpiryDate,\r\n                            ISNULL(PL2.ReceiptDate,PL.ReceiptDate) AS ReceiptDate, ISNULL(PL2.ReceiptNumber,PL.ReceiptNumber) AS VoucherID,PL.DocID AS SysDocID,PL.LotQty, PL.SoldQty   FROM Product_Lot PL LEFT OUTER JOIN \r\n\t\t\t\t\t\t\tProduct_Lot PL2 ON PL2.LotNumber = PL.SourceLotNumber\r\n                             WHERE PL.DocID = '" + sysDocID + "' AND PL.ReceiptNumber='" + voucherID + "'";
				FillDataSet(inventoryTransferData, "Transit_Lots", textCommand);
				return inventoryTransferData;
			}
			catch
			{
				throw;
			}
		}

		public InventoryTransferData GetInventoryTransferAcceptanceByID(string acceptanceSysDocID, string voucherID)
		{
			try
			{
				InventoryTransferData inventoryTransferData = new InventoryTransferData();
				string text = "SELECT IT.*, 1 AS Reason, ITT.LocationID AS TransitLocationID FROM Inventory_Transfer IT\r\n                                INNER JOIN Inventory_Transfer_Type ITT ON IT.TransferTypeID = ITT.TypeID WHERE AcceptVoucherID='" + voucherID + "' AND AcceptSysDocID='" + acceptanceSysDocID + "'";
				text = text + " UNION \r\n                       SELECT IT.*, 2 AS Reason, ITT.LocationID AS TransitLocationID FROM Inventory_Transfer IT\r\n                                INNER JOIN Inventory_Transfer_Type ITT ON IT.TransferTypeID = ITT.TypeID WHERE RejectAcceptVoucherID='" + voucherID + "' AND RejectAcceptSysDocID='" + acceptanceSysDocID + "'";
				FillDataSet(inventoryTransferData, "Inventory_Transfer", text);
				if (inventoryTransferData == null || inventoryTransferData.Tables.Count == 0 || inventoryTransferData.Tables["Inventory_Transfer"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT TD.SysDocID,TD.VoucherID,TD.ProductID,TD.UnitID, ISNULL(TD.UnitQuantity,TD.Quantity) as Quantity,TD.Factor,TD.FactorType,ISNULL(TD.AcceptedUnitQuantity,TD.AcceptedQuantity) as AcceptedQuantity,TD.AcceptedFactor,TD.AcceptedFactorType,ISNULL(TD.RejectedUnitQuantity,TD.RejectedQuantity) AS RejectedQuantity,\r\n                        TD.RejectedFactor,TD.RejectedFactorType,TD.RowIndex,TD.Remarks,Product.Description,IsTrackLot,IsTrackSerial\r\n                        FROM Inventory_Transfer_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        INNER JOIN Inventory_Transfer IT ON IT.SysDocID = TD.SysDocID AND IT.VoucherID = TD.VoucherID\r\n                        WHERE AcceptVoucherID='" + voucherID + "' AND AcceptSysDocID='" + acceptanceSysDocID + "' ";
				text = text + " UNION \r\n                        SELECT TD.SysDocID,TD.VoucherID,TD.ProductID,TD.UnitID, ISNULL(TD.UnitQuantity,TD.Quantity) as Quantity,TD.Factor,TD.FactorType,ISNULL(TD.AcceptedUnitQuantity,TD.AcceptedQuantity) as AcceptedQuantity,TD.AcceptedFactor,TD.AcceptedFactorType,ISNULL(TD.RejectedUnitQuantity,TD.RejectedQuantity)  AS RejectedQuantity,\r\n                        TD.RejectedFactor,TD.RejectedFactorType,TD.RowIndex,TD.Remarks,Product.Description,IsTrackLot,IsTrackSerial\r\n                        FROM Inventory_Transfer_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        INNER JOIN Inventory_Transfer IT ON IT.SysDocID = TD.SysDocID AND IT.VoucherID = TD.VoucherID\r\n                        WHERE RejectAcceptVoucherID='" + voucherID + "' AND RejectAcceptSysDocID='" + acceptanceSysDocID + "'  ORDER BY TD.RowIndex";
				FillDataSet(inventoryTransferData, "Inventory_Transfer_Detail", text);
				text = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + acceptanceSysDocID + "'";
				FillDataSet(inventoryTransferData, "Product_Lot_Receiving_Detail", text);
				return inventoryTransferData;
			}
			catch
			{
				throw;
			}
		}

		public InventoryTransferData GetInventoryTransferRejectionByID(string rejectSysDocID, string voucherID)
		{
			try
			{
				InventoryTransferData inventoryTransferData = new InventoryTransferData();
				string textCommand = "SELECT * FROM Inventory_Transfer WHERE VoucherID='" + voucherID + "' AND RejectSysDocID='" + rejectSysDocID + "'";
				FillDataSet(inventoryTransferData, "Inventory_Transfer", textCommand);
				if (inventoryTransferData == null || inventoryTransferData.Tables.Count == 0 || inventoryTransferData.Tables["Inventory_Transfer"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.SysDocID,TD.VoucherID,TD.ProductID,TD.UnitID, ISNULL(TD.UnitQuantity,TD.Quantity) as Quantity,TD.Factor,TD.FactorType,ISNULL(TD.AcceptedUnitQuantity,TD.AcceptedQuantity) as AcceptedQuantity,TD.AcceptedFactor,TD.AcceptedFactorType,ISNULL(TD.RejectedUnitQuantity,TD.RejectedQuantity),\r\n                        TD.RejectedFactor,TD.RejectedFactorType,TD.RowIndex,TD.Remarks, Product.Description,ISNULL(IsTrackLot,'False') AS IsTrackLot, ISNULL(IsTrackSerial,'False') AS IsTrackSerial\r\n                        FROM Inventory_Transfer_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        INNER JOIN Inventory_Transfer IT ON IT.SysDocID = TD.SysDocID AND IT.VoucherID = TD.VoucherID\r\n                        WHERE IT.VoucherID='" + voucherID + "' AND RejectSysDocID='" + rejectSysDocID + "' ORDER BY TD.RowIndex";
				FillDataSet(inventoryTransferData, "Inventory_Transfer_Detail", textCommand);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + rejectSysDocID + "'";
				FillDataSet(inventoryTransferData, "Product_Lot_Receiving_Detail", textCommand);
				return inventoryTransferData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInventoryTransferDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Inventory_Transfer_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInventoryTransferAcceptanceDetailsRows(string sysDocID, string voucherID, string acceptSysDocID, string acceptVoucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (new Products(base.DBConfig).HasInUseLots(acceptSysDocID, voucherID))
				{
					throw new CompanyException("This transaction cannot be modifed because some of items are refered by other transactions.");
				}
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(20, acceptSysDocID, acceptVoucherID, isDeletingTransaction: false, sqlTransaction);
				string commandText = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + acceptSysDocID + "' AND VoucherID = '" + acceptVoucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Inventory_Transfer_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidInventoryTransfer(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Inventory_Transfer SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(19, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(20, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(40, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Inventory Transfer", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteInventoryTransfer(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteInventoryTransferDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Inventory_Transfer WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(19, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(20, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(40, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Inventory Transfer", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetInventoryTransferReport(DateTime from, DateTime to, string warehouseCode, bool isTransferOut)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT IT.* FROM Inventory_Transfer IT \r\n                                WHERE ";
				text3 = text3 + "  TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (warehouseCode != "")
				{
					text3 = ((!isTransferOut) ? (text3 + " AND LocationToID = '" + warehouseCode + "' ") : (text3 + " AND LocationFromID = '" + warehouseCode + "' "));
				}
				text3 += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Transfer", text3);
				DataSet dataSet2 = new DataSet();
				text3 = "SELECT ITD.*,p.Description FROM Inventory_Transfer_Detail ITD \r\n                            INNER JOIN Product P ON P.ProductID=ITD.ProductID\r\n                            INNER JOIN Inventory_Transfer IT ON IT.SysDocID=ITD.SysDocID AND IT.VoucherID=ITD.VoucherID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND P.ItemType NOT IN ('3') ";
				FillDataSet(dataSet2, "TransferDetail", text3);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("TransferRel", new DataColumn[2]
				{
					dataSet.Tables["Transfer"].Columns["SysDocID"],
					dataSet.Tables["Transfer"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["TransferDetail"].Columns["SysDocID"],
					dataSet.Tables["TransferDetail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryTransferToPrint(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT IT.*,V.RegistrationNumber,D.DriverName,(Select LocationName From Location where LocationID= IT.LocationFromID) AS [From Location], \r\n                                    (SELECT LocationName From Location where LocationID= IT.LocationToID) AS [To Location] \r\n                               FROM Inventory_Transfer IT LEFT JOIN Vehicle V ON V.VehicleID=IT.VehicleNumber LEFT JOIN Driver D ON D.DriverID=IT.DriverID \r\n                               WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Inventory_Transfer", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Inventory_Transfer"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT TD.*,Product.Description,IsTrackLot,IsTrackSerial\r\n                        FROM Inventory_Transfer_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(dataSet, "Inventory_Transfer_Detail", cmdText);
				dataSet.Relations.Add("InventoryTransfer", new DataColumn[2]
				{
					dataSet.Tables["Inventory_Transfer"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Transfer"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Inventory_Transfer_Detail"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Transfer_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetRejectedInventoryTransferToPrint(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT IT.*,(Select LocationName From Location where LocationID= IT.LocationFromID) AS [From Location],\r\n                                (SELECT LocationName From Location where LocationID= IT.LocationToID) AS [To Location]\r\n                            FROM Inventory_Transfer IT WHERE VoucherID='" + voucherID + "' AND RejectSysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Inventory_Transfer", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Inventory_Transfer"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT TD.*,Product.Description,ISNULL(IsTrackLot,'False') AS IsTrackLot, ISNULL(IsTrackSerial,'False') AS IsTrackSerial\r\n                        FROM Inventory_Transfer_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        INNER JOIN Inventory_Transfer IT ON IT.SysDocID = TD.SysDocID AND IT.VoucherID = TD.VoucherID\r\n                        WHERE TD.VoucherID='" + voucherID + "' AND RejectSysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(dataSet, "Inventory_Transfer_Detail", cmdText);
				dataSet.Relations.Add("InventoryTransfer", new DataColumn[2]
				{
					dataSet.Tables["Inventory_Transfer"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Transfer"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Inventory_Transfer_Detail"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Transfer_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetReceivedInventoryTransferToPrint(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT *, (SELECT LocationName FROM Location WHERE LocationID=I.LocationFromID) AS FromLocation,(SELECT LocationName FROM Location WHERE LocationID=I.LocationToID) AS ToLocation, 1 AS Reason FROM Inventory_Transfer I  WHERE AcceptVoucherID='" + voucherID + "' AND AcceptSysDocID='" + sysDocID + "'";
				text = text + " UNION\r\n \r\n                       SELECT *, (SELECT LocationName FROM Location WHERE LocationID=I.LocationFromID) AS FromLocation,(SELECT LocationName FROM Location WHERE LocationID=I.LocationToID) AS ToLocation,2 AS Reason FROM Inventory_Transfer I WHERE RejectAcceptVoucherID='" + voucherID + "' AND RejectAcceptSysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(text);
				FillDataSet(dataSet, "Inventory_Transfer", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Inventory_Transfer"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT TD.*,Product.Description,IsTrackLot,IsTrackSerial\r\n                        FROM Inventory_Transfer_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        INNER JOIN Inventory_Transfer IT ON IT.SysDocID = TD.SysDocID AND IT.VoucherID = TD.VoucherID\r\n                        WHERE AcceptVoucherID='" + voucherID + "' AND AcceptSysDocID='" + sysDocID + "' ";
				text = text + " UNION\r\n \r\n                        SELECT TD.*,Product.Description,IsTrackLot,IsTrackSerial\r\n                        FROM Inventory_Transfer_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        INNER JOIN Inventory_Transfer IT ON IT.SysDocID = TD.SysDocID AND IT.VoucherID = TD.VoucherID\r\n                        WHERE RejectAcceptVoucherID='" + voucherID + "' AND RejectAcceptSysDocID='" + sysDocID + "'  ORDER BY RowIndex";
				FillDataSet(dataSet, "Inventory_Transfer_Detail", text);
				dataSet.Relations.Add("InventoryTransfer", new DataColumn[2]
				{
					dataSet.Tables["Inventory_Transfer"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Transfer"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Inventory_Transfer_Detail"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Transfer_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public string GetNextAcceptVoucherID(string acceptSysDocID, string acceptVoucherID, int type)
		{
			string exp = "";
			switch (type)
			{
			case 1:
				exp = "SELECT TOP 1 VoucherID FROM\r\n                                    (Select AcceptVoucherID AS VoucherID FROM Inventory_Transfer WHERE AcceptSysDocID = '" + acceptSysDocID + "'\r\n                                    UNION \r\n                                    SELECT RejectAcceptVoucherID AS VoucherID FROM Inventory_Transfer WHERE RejectAcceptSysdocID = '" + acceptSysDocID + "') AS TRF\r\n\r\n                                    WHERE TRF.VoucherID > '" + acceptVoucherID + "' ORDER BY TRF.VoucherID";
				break;
			case 2:
				exp = "SELECT TOP 1 VoucherID FROM\r\n                                    (Select AcceptVoucherID AS VoucherID FROM Inventory_Transfer WHERE AcceptSysDocID = '" + acceptSysDocID + "'\r\n                                    UNION \r\n                                    SELECT RejectAcceptVoucherID AS VoucherID FROM Inventory_Transfer WHERE RejectAcceptSysdocID = '" + acceptSysDocID + "') AS TRF\r\n                                        WHERE TRF.VoucherID < '" + acceptVoucherID + "' ORDER BY TRF.VoucherID DESC";
				break;
			case 3:
				exp = "SELECT MAX (VoucherID) FROM\r\n                                    (Select AcceptVoucherID AS VoucherID FROM Inventory_Transfer WHERE AcceptSysDocID = '" + acceptSysDocID + "'\r\n                                    UNION \r\n                                    SELECT RejectAcceptVoucherID AS VoucherID FROM Inventory_Transfer WHERE RejectAcceptSysdocID = '" + acceptSysDocID + "') AS TRF";
				break;
			case 4:
				exp = "SELECT MIN (VoucherID) FROM\r\n                                    (Select AcceptVoucherID AS VoucherID FROM Inventory_Transfer WHERE AcceptSysDocID = '" + acceptSysDocID + "'\r\n                                    UNION \r\n                                    SELECT RejectAcceptVoucherID AS VoucherID FROM Inventory_Transfer WHERE RejectAcceptSysdocID = '" + acceptSysDocID + "') AS TRF";
				break;
			}
			return Convert.ToString(ExecuteScalar(exp));
		}

		public DataSet GetListInventoryTransfer(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT  IT.SysDocID, IT.VoucherID, IT.TransactionDate, IT.LocationFromID, IT.LocationToID from Inventory_Transfer IT INNER JOIN System_Document SD  on IT.SysDocID=SD.SysDocID   WHERE SD.SysDocType='19' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND IT.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND IT.SysDocID = '" + sysDocID + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Inventory_Transfer", sqlCommand);
			return dataSet;
		}

		public DataSet GetListDirectInventoryTransfer(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   IT.SysDocID, IT.VoucherID, IT.TransactionDate, IT.LocationFromID, IT.LocationToID from Inventory_Transfer IT INNER JOIN System_Document SD  on IT.SysDocID=SD.SysDocID   WHERE SD.SysDocType='40' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND IT.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Inventory_Transfer", sqlCommand);
			return dataSet;
		}

		public DataSet GetListInventoryTransfersToAccept(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT  AcceptSysDocID as SysDocID ,AcceptVoucherID as VoucherID ,AcceptDate FROM Inventory_Transfer ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE AcceptDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Inventory_Transfer", sqlCommand);
			return dataSet;
		}

		public DataSet GetListInventoryTransferReturn(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT  RejectSysDocID as SysDocID,VoucherID,RejectDate FROM Inventory_Transfer ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE RejectDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Inventory_Transfer", sqlCommand);
			return dataSet;
		}

		public string FindDocumentByNumber(string tableName, string fieldName, string sysDocID, string filterFieldName, object filterFieldValue, string numberQuery)
		{
			numberQuery = AddSingleQuote(numberQuery.ToString());
			string text = "";
			int result = 0;
			text = ((!int.TryParse(numberQuery, out result)) ? ("SELECT TOP 1 " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " = '" + numberQuery + "' ") : ("SELECT TOP 1 " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " LIKE '%" + numberQuery + "' "));
			if (sysDocID != "")
			{
				text = text + " AND AcceptSysDocID = '" + sysDocID + "'";
			}
			if (filterFieldName != "" && filterFieldValue != null)
			{
				text = text + " AND " + filterFieldName + " = '" + filterFieldValue.ToString() + "' ";
			}
			text = text + " ORDER BY " + fieldName + " DESC ";
			object obj = ExecuteScalar(text);
			if (obj == null)
			{
				return "";
			}
			if (int.TryParse(numberQuery, out result))
			{
				int num = 0;
				for (int i = 0; i < obj.ToString().Length; i++)
				{
					if (int.TryParse(obj.ToString().Substring(i, 1), out result))
					{
						num = i;
						break;
					}
				}
				if (int.Parse(obj.ToString().Substring(num, obj.ToString().Length - num)) != int.Parse(numberQuery))
				{
					return "";
				}
			}
			if (obj != null)
			{
				return obj.ToString();
			}
			return "";
		}

		public bool ReCostTransferTransaction(string sysDocID, string voucherID, SysDocTypes docType)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				InventoryTransferData inventoryTransferData = null;
				switch (docType)
				{
				case SysDocTypes.TransitTransferOut:
					inventoryTransferData = GetInventoryTransferByID(sysDocID, voucherID);
					break;
				case SysDocTypes.TransitTransferIn:
					inventoryTransferData = GetInventoryTransferAcceptanceByID(sysDocID, voucherID);
					break;
				case SysDocTypes.ReturnTransitTransfer:
					inventoryTransferData = GetInventoryTransferRejectionByID(sysDocID, voucherID);
					break;
				case SysDocTypes.DirectInventoryTransfer:
					inventoryTransferData = GetInventoryTransferByID(sysDocID, voucherID);
					break;
				default:
					throw new Exception("SysDoc is not supported.");
				}
				if (inventoryTransferData == null || inventoryTransferData.Tables.Count == 0 || inventoryTransferData.InventoryTransferTable.Rows.Count == 0)
				{
					throw new Exception("Transaction not found.\nVoucherID:" + voucherID);
				}
				GLData journalData = null;
				switch (docType)
				{
				case SysDocTypes.TransitTransferOut:
					journalData = CreateInventoryTransferGLData(inventoryTransferData, sqlTransaction);
					break;
				case SysDocTypes.TransitTransferIn:
					journalData = CreateInventoryTransferAcceptanceGLData(inventoryTransferData, sqlTransaction);
					break;
				case SysDocTypes.ReturnTransitTransfer:
					journalData = CreateInventoryTransferRejectGLData(inventoryTransferData, sqlTransaction);
					break;
				case SysDocTypes.DirectInventoryTransfer:
					journalData = CreateDirectInventoryTransferGLData(inventoryTransferData, sqlTransaction);
					break;
				}
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate: false, sqlTransaction);
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
	}
}
