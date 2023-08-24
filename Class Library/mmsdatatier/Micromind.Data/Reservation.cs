using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Reservation : StoreObject
	{
		public const string RESERVATIONDETAIL_TABLE = "Reservation_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string RESERVEID_PARM = "@ReserveID";

		private const string SYSDOCTYPE_PARM = "@SysDocType";

		private const string LOTNUMBER_PARM = "@LotNumber";

		private const string JOBID_PARM = "@JobID";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string VALIDDATEUPTO_PARM = "@ValidDateUpTo";

		private const string SOURCERESERVEID_PARM = "@SourceReserveID";

		private const string QUANTITY_PARM = "@Quantity";

		public Reservation(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateReservationText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Reservation_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("SysDocType", "@SysDocType"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LotNumber", "@LotNumber"), new FieldValue("JobID", "@JobID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("ValidDateUpTo", "@ValidDateUpTo"), new FieldValue("SourceReserveID", "@SourceReserveID"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateReservationCommand(bool isUpdate)
		{
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetInsertUpdateReservationText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SysDocType", SqlDbType.Int);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@LotNumber", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Decimal);
			parameters.Add("@ValidDateUpTo", SqlDbType.DateTime);
			parameters.Add("@SourceReserveID", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SysDocType"].SourceColumn = "SysDocType";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@LotNumber"].SourceColumn = "LotNumber";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@ValidDateUpTo"].SourceColumn = "ValidDateUpTo";
			parameters["@SourceReserveID"].SourceColumn = "SourceReserveID";
			return insertCommand;
		}

		private string GetInsertUpdateReservationDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateReservationDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateReservationDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateReservationDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ReservationData journalData)
		{
			return true;
		}

		public object GetSourceReserveID(string tableName, string fieldName, string idFieldName, object idFieldValue, string idFieldName2, object idFieldValue2, string idFieldName3, object idFieldValue3, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + idFieldName + " = '" + idFieldValue + "' AND " + idFieldName2 + " = '" + idFieldValue2 + "' AND " + idFieldName3 + " = '" + idFieldValue3 + "'";
				return ExecuteScalar(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdateReservation(DataSet transactionData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateReservationCommand = GetInsertUpdateReservationCommand(isUpdate);
			try
			{
				DataRow dataRow = transactionData.Tables[0].Rows[0];
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Reservation_Detail", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", text2, sqlTransaction).ToString());
				DataTable dataTable = new DataTable();
				DataTable dataTable2 = new DataTable();
				DataTable dataTable3 = new DataTable();
				int value = 1;
				string text3 = "";
				string text4 = "";
				string text5 = "";
				string text6 = "";
				decimal num = default(decimal);
				switch (sysDocTypes)
				{
				case SysDocTypes.SalesOrder:
				case SysDocTypes.ExportSalesOrder:
					dataTable.TableName = "Sales_Order".ToString();
					dataTable2.TableName = "Sales_Order_Detail".ToString();
					dataTable3.TableName = "Sales_Order_ProductLot_Detail".ToString();
					value = 1;
					break;
				case SysDocTypes.DeliveryNote:
				case SysDocTypes.ExportDeliveryNote:
					dataTable.TableName = "Delivery_Note".ToString();
					dataTable2.TableName = "Delivery_Note_Detail".ToString();
					dataTable3.TableName = "Product_Lot_Issue_Detail".ToString();
					value = -1;
					break;
				case SysDocTypes.GoodsReceivedNote:
					dataTable.TableName = "Purchase_Receipt".ToString();
					dataTable2.TableName = "Purchase_Receipt_Detail".ToString();
					dataTable3.TableName = "Product_Lot_Receiving_Detail".ToString();
					value = 1;
					break;
				}
				ReservationData reservationData = new ReservationData();
				foreach (DataRow row in transactionData.Tables[dataTable2.TableName].Rows)
				{
					DataRow dataRow3 = null;
					string text7 = "";
					string text8 = "";
					int num2 = 0;
					int num3 = 0;
					text7 = row["ProductID"].ToString();
					num2 = int.Parse(row["RowIndex"].ToString());
					if (sysDocTypes == SysDocTypes.DeliveryNote || sysDocTypes == SysDocTypes.ExportDeliveryNote)
					{
						text3 = row["SourceSysDocID"].ToString();
						text4 = row["SourceVoucherID"].ToString();
					}
					if (sysDocTypes == SysDocTypes.GoodsReceivedNote)
					{
						text3 = new Databases(base.DBConfig).GetFieldValue("Purchase_Order_Detail", "SourceSysDocID", "ProductID", text7, sqlTransaction).ToString();
						text4 = new Databases(base.DBConfig).GetFieldValue("Purchase_Order_Detail", "SourceVoucherID", "ProductID", text7, sqlTransaction).ToString();
						text6 = new Databases(base.DBConfig).GetFieldValue("Purchase_Order_Detail", "SourceRowIndex", "ProductID", text7, sqlTransaction).ToString();
					}
					string filterExpression = "ProductID = '" + text7 + "'";
					string sort = "ProductID DESC";
					if (sysDocTypes != SysDocTypes.GoodsReceivedNote && !string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
					{
						text5 = GetSourceReserveID("Reservation_Detail", "ReserveID", "SysDocID", text3, "VoucherID", text4, "ProductID", text7, sqlTransaction).ToString();
					}
					DataTable dataTable4 = new DataTable();
					if (transactionData.Tables[dataTable3.TableName].Select(filterExpression, sort).Length != 0)
					{
						dataTable4 = transactionData.Tables[dataTable3.TableName].Select(filterExpression, sort).CopyToDataTable();
					}
					if (dataTable4.Rows.Count > 0)
					{
						foreach (DataRow row2 in transactionData.Tables[dataTable3.TableName].Rows)
						{
							text7 = row2["ProductID"].ToString();
							text8 = row2["LotNumber"].ToString();
							if (sysDocTypes != SysDocTypes.GoodsReceivedNote && !string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
							{
								text5 = GetSourceReserveID("Reservation_Detail", "ReserveID", "SysDocID", text3, "VoucherID", text4, "ProductID", text7, sqlTransaction).ToString();
							}
							string filterExpression2 = "ProductID = '" + text7 + "'";
							string sort2 = "ProductID DESC";
							transactionData.Tables[dataTable3.TableName].Select(filterExpression2, sort2).CopyToDataTable();
							string filterExpression3 = "ProductID = '" + text7 + "' AND LotNumber='" + text8 + "'";
							DataRow[] array = transactionData.Tables[dataTable3.TableName].Select(filterExpression3);
							DataRow[] array2 = reservationData.Tables["Reservation_Detail"].Select(filterExpression3);
							DataRow[] array3 = array;
							for (int i = 0; i < array3.Length; i++)
							{
								int.TryParse(array3[i]["RowIndex"].ToString(), out num2);
							}
							if (array.Length != 0)
							{
								for (int j = 0; j < array.Length; j++)
								{
									string filterExpression4 = "ProductID = '" + text7 + "'";
									string sort3 = "ProductID DESC";
									DataTable dataTable5 = transactionData.Tables[dataTable3.TableName].Select(filterExpression4, sort3).CopyToDataTable();
									num = GetSOReservewithGRNQuantity(text3, text4, text6, text7, sqlTransaction);
									string textCommand = "SELECT PL.* FROM Product_Lot PL  WHERE DocID = '" + text2 + "' AND ReceiptNumber = '" + text + "'AND ItemCode = '" + text7 + "'";
									DataSet dataSet = new DataSet();
									FillDataSet(dataSet, "Product_Lot_Receiving_Detail", textCommand);
									if (dataSet.Tables[0].Rows.Count > 0)
									{
										num3 = int.Parse(dataSet.Tables[0].Rows[0]["LotNumber"].ToString());
									}
									foreach (DataRow row3 in dataTable5.Rows)
									{
										if (row3["ProductID"].ToString().Trim() == text7.Trim())
										{
											dataRow3 = reservationData.Tables["Reservation_Detail"].NewRow();
											if (sysDocTypes != SysDocTypes.GoodsReceivedNote)
											{
												dataRow3["SysDocID"] = row["SysDocID"];
												dataRow3["VoucherID"] = row["VoucherID"];
												dataRow3["OrderRowIndex"] = num2;
												dataRow3["SysDocType"] = sysDocTypes;
												dataRow3["LotNumber"] = row3["LotNumber"];
											}
											else
											{
												dataRow3["SysDocID"] = text3;
												dataRow3["VoucherID"] = text4;
												dataRow3["OrderRowIndex"] = text6;
												dataRow3["SysDocType"] = SysDocTypes.SalesOrder;
												dataRow3["LotNumber"] = num3;
											}
											dataRow3["ProductID"] = row3["ProductID"];
											dataRow3["JobID"] = row["JobID"];
											if (sysDocTypes != SysDocTypes.GoodsReceivedNote)
											{
												dataRow3["CustomerID"] = transactionData.Tables[dataTable.TableName].Rows[0]["CustomerID"];
											}
											if (sysDocTypes != SysDocTypes.GoodsReceivedNote)
											{
												dataRow3["Quantity"] = (decimal)value * decimal.Parse(row3["SoldQty"].ToString());
											}
											else
											{
												dataRow3["Quantity"] = (decimal)value * num;
											}
											dataRow3["ValidDateUpTo"] = DateTime.Now;
											if (string.IsNullOrEmpty(text5))
											{
												text5 = "0";
											}
											dataRow3["SourceReserveID"] = int.Parse(text5);
											dataRow3.EndEdit();
											if (array2.Length == 0)
											{
												reservationData.Tables["Reservation_Detail"].Rows.Add(dataRow3);
											}
										}
									}
								}
							}
							else
							{
								dataRow3 = reservationData.Tables["Reservation_Detail"].NewRow();
								dataRow3["SysDocID"] = row["SysDocID"];
								dataRow3["VoucherID"] = row["VoucherID"];
								dataRow3["SysDocType"] = sysDocTypes;
								dataRow3["ProductID"] = row["ProductID"];
								dataRow3["LotNumber"] = 0;
								dataRow3["JobID"] = row["JobID"];
								dataRow3["CustomerID"] = transactionData.Tables[dataTable.TableName].Rows[0]["CustomerID"];
								dataRow3["OrderRowIndex"] = row["RowIndex"];
								dataRow3["Quantity"] = (decimal)value * decimal.Parse(row["Quantity"].ToString());
								dataRow3["ValidDateUpTo"] = DateTime.Now;
								if (string.IsNullOrEmpty(text5))
								{
									text5 = "0";
								}
								dataRow3["SourceReserveID"] = int.Parse(text5);
								dataRow3.EndEdit();
								reservationData.Tables["Reservation_Detail"].Rows.Add(dataRow3);
							}
						}
					}
					else if (dataTable4.Rows.Count == 0)
					{
						dataRow3 = reservationData.Tables["Reservation_Detail"].NewRow();
						dataRow3["SysDocID"] = row["SysDocID"];
						dataRow3["VoucherID"] = row["VoucherID"];
						dataRow3["SysDocType"] = sysDocTypes;
						dataRow3["ProductID"] = row["ProductID"];
						dataRow3["LotNumber"] = 0;
						dataRow3["JobID"] = row["JobID"];
						dataRow3["CustomerID"] = transactionData.Tables[dataTable.TableName].Rows[0]["CustomerID"];
						dataRow3["OrderRowIndex"] = row["RowIndex"];
						dataRow3["Quantity"] = 0;
						dataRow3["ValidDateUpTo"] = DateTime.Now;
						if (string.IsNullOrEmpty(text5))
						{
							text5 = "0";
						}
						dataRow3["SourceReserveID"] = int.Parse(text5);
						dataRow3.EndEdit();
						reservationData.Tables["Reservation_Detail"].Rows.Add(dataRow3);
					}
				}
				if (isUpdate)
				{
					flag &= DeleteReservationDetailsRows(text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				insertUpdateReservationCommand.Transaction = sqlTransaction;
				if (reservationData.ReservationDetailsTable.Rows.Count > 0)
				{
					flag &= Insert(reservationData, "Reservation_Detail", insertUpdateReservationCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				string entityName = "Reservation-" + text2 + "-" + text;
				if (!isUpdate)
				{
					return flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction);
				}
				return flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public ReservationData GetReservationByID(string sysDocID, string voucherID)
		{
			try
			{
				ReservationData reservationData = new ReservationData();
				string textCommand = "SELECT * FROM _Reservation WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(reservationData, "Reservation_Detail", textCommand);
				if (reservationData == null || reservationData.Tables.Count == 0 || reservationData.Tables["Reservation_Detail"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*, CASE WHEN ItemType = 5 THEN 'True' ELSE ISNULL(IsTrackLot,'False') END AS IsTrackLot,ItemType, ISNULL(IsTrackSerial,'False') AS IsTrackSerial\r\n                        FROM _Reservation_Detail TD INNER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex";
				FillDataSet(reservationData, "Reservation_Detail", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (reservationData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					reservationData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				reservationData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return reservationData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteReservationDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText;
				if (int.Parse(new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", sysDocID, sqlTransaction).ToString()) == 32)
				{
					commandText = "DELETE RD FROM Reservation_Detail RD INNER JOIN Product_Lot PL ON RD.LotNumber=PL.LotNumber WHERE PL.DocID = '" + sysDocID + "' AND PL.ReceiptNumber = '" + voucherID + "'";
					flag &= Delete(commandText, sqlTransaction);
				}
				commandText = "DELETE FROM Reservation_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidReservation(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteReservation(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteReservationDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Reservation", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetReservationToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT MR.* FROM _Reservation MR\r\n                               \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Reservation_Detail", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Reservation_Detail"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT *, SUM(Quantity * COST) AS Amount, SUM(ISNULL(UnitQuantity,0)* Cost) AS UnitAmount FROM _Reservation_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        GROUP BY SysDocID,VoucherID,ProductID,Description,UnitID,Quantity,Cost,Factor,UnitQuantity,FactorType,RowIndex\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Reservation_Detail", cmdText);
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
			string text3 = "SELECT SysDocID [Doc ID], VoucherID [Doc Number],TransactionDate, Description from _Reservation ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Reservation_Detail", sqlCommand);
			return dataSet;
		}

		public decimal GetSOReservewithGRNQuantity(string sysDocID, string voucherNumber, string rowIndex, string productID, SqlTransaction sqlTransaction)
		{
			string exp = "Select Quantity FROM Sales_Order_Detail SOD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' AND RowIndex='" + rowIndex + "' AND ProductID='" + productID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "")
			{
				return decimal.Parse(obj.ToString());
			}
			return 0m;
		}
	}
}
