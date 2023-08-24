using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FixedAssetTransfer : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ACCEPTDATE_PARM = "@AcceptDate";

		private const string RETURNDATE_PARM = "@ReturnDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string LOCATIONFROMID_PARM = "@LocationFromID";

		private const string LOCATIONTOID_PARM = "@LocationToID";

		private const string DIVISIONFROMID_PARM = "@DivisionFromID";

		private const string DIVISIONTOID_PARM = "@DivisionToID";

		private const string DEPARTMENTFROMOID_PARM = "@DepartmentFromID";

		private const string DEPARTMENTTOID_PARM = "@DepartmentToID";

		private const string EMPLOYEEFROMID_PARM = "@EmployeeFromID";

		private const string EMPLOYEETOID_PARM = "@EmployeeToID";

		private const string NOTE_PARM = "@Note";

		private const string DRIVERID_PARM = "@DriverID";

		private const string VEHICLENUMBER_PARM = "@VehicleNumber";

		private const string TRANSFERTYPE_PARM = "@TransferType";

		private const string ISVOID_PARM = "@IsVoid";

		private const string FIXEDASSETTRANSFER_TABLE = "FixedAsset_Transfer";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ASSETID_PARM = "@AssetID";

		private const string FIXEDASSETTRANSFERDETAIL_TABLE = "FixedAsset_Transfer_Detail";

		public FixedAssetTransfer(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateFixedAssetTransferText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Transfer", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("AssetID", "@AssetID"), new FieldValue("Reference", "@Reference"), new FieldValue("LocationFromID", "@LocationFromID"), new FieldValue("LocationToID", "@LocationToID"), new FieldValue("DivisionFromID", "@DivisionFromID"), new FieldValue("DivisionToID", "@DivisionToID"), new FieldValue("DepartmentFromID", "@DepartmentFromID"), new FieldValue("DepartmentToID", "@DepartmentToID"), new FieldValue("EmployeeFromID", "@EmployeeFromID"), new FieldValue("EmployeeToID", "@EmployeeToID"), new FieldValue("TransferType", "@TransferType"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset_Transfer", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFixedAssetTransferCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFixedAssetTransferText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFixedAssetTransferText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@AssetID", SqlDbType.NVarChar);
			parameters.Add("@LocationFromID", SqlDbType.NVarChar);
			parameters.Add("@LocationToID", SqlDbType.NVarChar);
			parameters.Add("@DivisionFromID", SqlDbType.NVarChar);
			parameters.Add("@DivisionToID", SqlDbType.NVarChar);
			parameters.Add("@DepartmentFromID", SqlDbType.NVarChar);
			parameters.Add("@DepartmentToID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeFromID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeToID", SqlDbType.NVarChar);
			parameters.Add("@TransferType", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@AssetID"].SourceColumn = "AssetID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@LocationFromID"].SourceColumn = "LocationFromID";
			parameters["@LocationToID"].SourceColumn = "LocationToID";
			parameters["@DivisionFromID"].SourceColumn = "DivisionFromID";
			parameters["@DivisionToID"].SourceColumn = "DivisionToID";
			parameters["@DepartmentFromID"].SourceColumn = "DepartmentFromID";
			parameters["@DepartmentToID"].SourceColumn = "DepartmentToID";
			parameters["@EmployeeFromID"].SourceColumn = "EmployeeFromID";
			parameters["@EmployeeToID"].SourceColumn = "EmployeeToID";
			parameters["@TransferType"].SourceColumn = "TransferType";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Note"].SourceColumn = "Note";
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

		private bool ValidateData(FixedAssetTransferData journalData)
		{
			return true;
		}

		public bool InsertUpdateFixedAssetTransfer(FixedAssetTransferData fixedAssetTransferData, bool isUpdate)
		{
			return InsertUpdateFixedAssetTransfer(fixedAssetTransferData, isUpdate, null);
		}

		public bool InsertUpdateFixedAssetTransfer(FixedAssetTransferData fixedAssetTransferData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateFixedAssetTransferCommand = GetInsertUpdateFixedAssetTransferCommand(isUpdate);
			bool flag2 = false;
			try
			{
				DataRow dataRow = fixedAssetTransferData.FixedAssetTransferTable.Rows[0];
				if (sqlTransaction == null)
				{
					flag2 = true;
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				string text2 = dataRow["AssetID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("FixedAsset_Transfer", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text3 = dataRow["LocationToID"].ToString();
				string text4 = dataRow["DivisionToID"].ToString();
				string text5 = dataRow["DepartmentToID"].ToString();
				string text6 = dataRow["EmployeeToID"].ToString();
				insertUpdateFixedAssetTransferCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(fixedAssetTransferData, "FixedAsset_Transfer", insertUpdateFixedAssetTransferCommand)) : (flag & Insert(fixedAssetTransferData, "FixedAsset_Transfer", insertUpdateFixedAssetTransferCommand)));
				string exp = " UPDATE FixedAsset SET AssetLocationID= '" + text3 + "', DivisionID = '" + text4 + "' , DepartmentID = '" + text5 + "', EmployeeID = '" + text6 + "'\r\n                                WHERE AssetID= '" + text2 + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("FixedAsset_Transfer", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Fixed Asset Transfer";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "FixedAsset_Transfer", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.FixedAssetTransfer, sysDocID, text, "FixedAsset_Transfer", sqlTransaction);
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

		private GLData CreateFixedAssetTransferGLData(FixedAssetTransferData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.FixedAssetTransferTable.Rows[0];
			string value = "";
			string value2 = "";
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.FixedAssetTransfer;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Fixed Asset Transfer";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = value2;
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = value;
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		public FixedAssetTransferData GetFixedAssetTransferByID(string sysDocID, string voucherID)
		{
			try
			{
				FixedAssetTransferData fixedAssetTransferData = new FixedAssetTransferData();
				string textCommand = "SELECT * FROM FixedAsset_Transfer WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(fixedAssetTransferData, "FixedAsset_Transfer", textCommand);
				if (fixedAssetTransferData == null || fixedAssetTransferData.Tables.Count == 0 || fixedAssetTransferData.Tables["FixedAsset_Transfer"].Rows.Count == 0)
				{
					return null;
				}
				return fixedAssetTransferData;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidFixedAssetTransfer(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE FixedAsset_Transfer SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
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
				AddActivityLog("Fixed Asset Transfer", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteFixedAssetTransfer(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM FixedAsset_Transfer WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Fixed Asset Transfer", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetFixedAssetTransferReport(DateTime from, DateTime to, string warehouseCode, bool isTransferOut)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT IT.* FROM FixedAsset_Transfer IT \r\n                                WHERE ";
				text3 = text3 + "  TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (warehouseCode != "")
				{
					text3 = ((!isTransferOut) ? (text3 + " AND LocationToID = '" + warehouseCode + "' ") : (text3 + " AND LocationFromID = '" + warehouseCode + "' "));
				}
				text3 += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Transfer", text3);
				DataSet dataSet2 = new DataSet();
				text3 = "SELECT ITD.*,p.Description FROM FixedAsset_Transfer_Detail ITD \r\n                            INNER JOIN Product P ON P.ProductID=ITD.ProductID\r\n                            INNER JOIN FixedAsset_Transfer IT ON IT.SysDocID=ITD.SysDocID AND IT.VoucherID=ITD.VoucherID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
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

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT 'False' AS V, SysDocID,VoucherID,FT.AssetID [Asset Code],FA.AssetName AS [Asset Name], TransactionDate [Date], LocationfromID [From Location],[DivisionFromID] AS [From Division], DepartmentFromID AS [From Department],\r\n                                LocationToID [To Location], DivisionToID [To Division], DepartmentToID [To Department], Reference\r\n                                FROM fixedasset_transfer FT\r\n                                INNER JOIN FixedAsset FA ON FT.AssetID = FA.AssetID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "FixedAsset_Transfer", sqlCommand);
			return dataSet;
		}
	}
}
