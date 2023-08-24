using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Micromind.Data
{
	public sealed class EntityDoc : StoreObject
	{
		public const string ENTITYDOC_TABLE = "EntityDocs";

		public const string ENTITYID_PARM = "@EntityID";

		public const string ENTITYTYPE_PARM = "@EntityType";

		public const string ENTITYDOCPATH_PARM = "@EntityDocPath";

		private const string ENTITYDOCNAME_PARM = "@EntityDocName";

		private const string ENTITYDOCDESC_PARM = "@EntityDocDesc";

		private const string ENTITYDOCKEYWORD_PARM = "@EntityDocKeyword";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ENTITYSYSDOCID_PARM = "@EntitySysDocID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const byte COMPANY_ID = 1;

		public EntityDoc(Config config)
			: base(config)
		{
		}

		public EntityDocData GetEntityDocByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EntityID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "EntityDocs";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EntityDocData entityDocData = new EntityDocData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(entityDocData, "EntityDocs", sqlBuilder);
			return entityDocData;
		}

		public EntityDocData GetEntityDocByID(EntityTypesEnum entityType, string entityID, string fileName)
		{
			EntityDocData entityDocData = new EntityDocData();
			try
			{
				string text = "SELECT *\r\n                                  FROM EntityDocs WHERE EntityID='" + entityID + "' AND EntityType= " + (int)entityType + " AND EntityDocName=N'" + fileName + "'";
				FillDataSet(entityDocData, "EntityDocs", text.Trim());
				return entityDocData;
			}
			catch
			{
				return entityDocData;
			}
		}

		public DataSet GetEntityDocList(EntityTypesEnum entityType, string entityID, int entityRowIndex)
		{
			DataSet dataSet = new DataSet();
			string text = ((byte)entityType).ToString();
			try
			{
				string text2 = "SELECT EntityID [Entity ID], EntityDocName [File Name], EntityDocDesc [Description], EntityDocKeyword [Keywords] \r\n                                  FROM EntityDocs WHERE EntityID='" + entityID + "' AND EntityType='" + text + "' ";
				if (entityRowIndex >= 0)
				{
					text2 = text2 + " AND RowIndex = " + entityRowIndex;
				}
				FillDataSet(dataSet, "EntityDocs", text2);
				return dataSet;
			}
			catch
			{
				return dataSet;
			}
		}

		public DataSet GetEntityDocList(EntityTypesEnum entityType, string entitySysDocID, string entityID, int entityRowIndex)
		{
			DataSet dataSet = new DataSet();
			string text = ((byte)entityType).ToString();
			try
			{
				string text2 = "SELECT EntityID [Entity ID], EntityDocName [File Name], EntityDocDesc [Description], EntityDocKeyword [Keywords] \r\n                                  FROM EntityDocs WHERE EntityID='" + entityID + "' AND EntityType='" + text + "' ";
				if (entityRowIndex >= 0)
				{
					text2 = text2 + " AND RowIndex = " + entityRowIndex;
				}
				if (!string.IsNullOrEmpty(entitySysDocID))
				{
					text2 = text2 + " AND EntitySysDocID = '" + entitySysDocID + "'";
				}
				FillDataSet(dataSet, "EntityDocs", text2);
				return dataSet;
			}
			catch
			{
				return dataSet;
			}
		}

		public byte[] GetEntityDocByPath(string path)
		{
			byte[] result = null;
			if (File.Exists(path))
			{
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				result = new BinaryReader(fileStream).ReadBytes((int)fileStream.Length);
			}
			return result;
		}

		public byte[] GetEntityFile(EntityTypesEnum entityType, string entityID, string entitySysDocID, string entityDocName)
		{
			string text = "";
			string text2 = "";
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text2 = new Databases(base.DBConfig).GetFieldValue("Company", "FileSavingPath", "CompanyID", (byte)1, sqlTransaction).ToString();
				if (text2 == "" || text2 == null)
				{
					string executablePath = Application.ExecutablePath;
					text2 = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
				}
				text = ((entityType != EntityTypesEnum.Transactions) ? (text2 + "\\" + entityType + "\\" + entityID + "\\" + entityDocName) : (text2 + "\\" + entityType + "\\" + entitySysDocID + "\\" + entityID + "\\" + entityDocName));
				if (File.Exists(text))
				{
					FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read);
					MemoryStream memoryStream = new MemoryStream();
					fileStream.CopyTo(memoryStream);
					fileStream.Close();
					return memoryStream.ToArray();
				}
				return null;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteEntityDoc(EntityDocData entityDocData)
		{
			string text = "";
			try
			{
				string text2 = entityDocData.EntityDocTable.Rows[0]["EntityID"].ToString();
				EntityTypesEnum entityTypesEnum = (EntityTypesEnum)int.Parse(entityDocData.EntityDocTable.Rows[0]["EntityType"].ToString());
				byte entityType = byte.Parse(entityDocData.EntityDocTable.Rows[0]["EntityType"].ToString());
				string text3 = entityDocData.EntityDocTable.Rows[0]["EntityDocName"].ToString();
				string text4 = entityDocData.EntityDocTable.Rows[0]["EntitySysDocID"].ToString();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = new Databases(base.DBConfig).GetFieldValue("Company", "FileSavingPath", "CompanyID", (byte)1, sqlTransaction).ToString();
				if (text == "" || text == null)
				{
					string executablePath = Application.ExecutablePath;
					text = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
				}
				string path = text + "\\" + entityTypesEnum + "\\" + text2 + "\\" + text3;
				if (entityTypesEnum == EntityTypesEnum.Transactions)
				{
					path = text + "\\" + entityTypesEnum + "\\" + text4 + "\\" + text2 + "\\" + text3;
				}
				if (File.Exists(path))
				{
					File.Delete(path);
					return DeleteEntityDoc(entityType, text2, text3, sqlTransaction);
				}
				if (entityTypesEnum == EntityTypesEnum.ExternalReports)
				{
					return DeleteEntityDoc(entityType, text2, text3, sqlTransaction);
				}
			}
			catch
			{
				throw;
			}
			return false;
		}

		private bool DeleteEntityDoc(byte entityType, string entityID, string entityDocName, SqlTransaction sqlTransaction)
		{
			bool flag = false;
			try
			{
				string commandText = (entityType == 23) ? ("DELETE FROM EntityDocs WHERE EntityType = " + entityType + " AND EntityID = '" + entityID + "'") : ("DELETE FROM EntityDocs WHERE EntityType = " + entityType + " AND EntityID = '" + entityID + "' AND EntityDocName = '" + entityDocName + "'");
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("EntityDoc", entityID, ActivityTypes.Delete, null);
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

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EntityDocs", new FieldValue("EntityID", "@EntityID", isUpdateConditionField: true), new FieldValue("EntityType", "@EntityType", isUpdateConditionField: true), new FieldValue("EntityDocName", "@EntityDocName", isUpdateConditionField: true), new FieldValue("EntitySysDocID", "@EntitySysDocID"), new FieldValue("EntityDocPath", "@EntityDocPath"), new FieldValue("EntityDocDesc", "@EntityDocDesc"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("EntityDocKeyword", "@EntityDocKeyword"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EntityDocs", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@EntityID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters.Add("@EntityDocName", SqlDbType.NVarChar);
			parameters.Add("@EntitySysDocID", SqlDbType.NVarChar);
			parameters.Add("@EntityDocPath", SqlDbType.NVarChar);
			parameters.Add("@EntityDocDesc", SqlDbType.NVarChar);
			parameters.Add("@EntityDocKeyword", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@EntityID"].SourceColumn = "EntityID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			parameters["@EntityDocName"].SourceColumn = "EntityDocName";
			parameters["@EntitySysDocID"].SourceColumn = "EntitySysDocID";
			parameters["@EntityDocPath"].SourceColumn = "EntityDocPath";
			parameters["@EntityDocDesc"].SourceColumn = "EntityDocDesc";
			parameters["@EntityDocKeyword"].SourceColumn = "EntityDocKeyword";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		private string GetEditUpdateText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE EntityDocs");
			stringBuilder.Append(" SET EntityDocDesc = @EntityDocDesc, ");
			stringBuilder.Append("EntityDocKeyword = @EntityDocKeyword, ");
			stringBuilder.Append("DateUpdated = @DateUpdated");
			stringBuilder.Append(" WHERE EntityType = @EntityType");
			stringBuilder.Append(" AND EntityID = @EntityID");
			stringBuilder.Append(" AND EntityDocName = @EntityDocName");
			return stringBuilder.ToString();
		}

		public bool SaveOCRDocs(EntityDocData entityDocData)
		{
			bool flag = false;
			string text = "";
			try
			{
				text = new Databases(base.DBConfig).GetFieldValue("Company", "FileSavingPath", "CompanyID", (byte)1, null).ToString();
				if (text == "" || text == null)
				{
					string executablePath = Application.ExecutablePath;
					text = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
				}
				foreach (DataRow row in entityDocData.EntityDocTable.Rows)
				{
					string text2 = row["EntityID"].ToString();
					string text3 = row["EntitySysDocID"].ToString();
					EntityTypesEnum entityTypesEnum = (EntityTypesEnum)int.Parse(row["EntityType"].ToString());
					string text4 = text + "\\" + entityTypesEnum.ToString() + "\\" + text2;
					if (entityTypesEnum == EntityTypesEnum.Transactions)
					{
						text4 = text + "\\" + entityTypesEnum.ToString() + "\\" + text3 + "\\" + text2;
					}
					string text5 = row["EntityDocName"].ToString();
					row["EntityDocDesc"].ToString();
					row["EntityDocKeyword"].ToString();
					string text6 = text5;
					string text7 = text4 + "\\" + text6;
					int num = 1;
					while (File.Exists(text7))
					{
						text6 = Path.GetFileNameWithoutExtension(text5) + "_" + num + Path.GetExtension(text5);
						text7 = text4 + "\\" + text6;
						row["EntityDocName"] = text6;
						num++;
					}
					byte[] bytes = (byte[])row["FileData"];
					if (IsFileSavedToDisk(text4, text6, bytes))
					{
						row["EntityDocPath"] = text7;
					}
				}
				return InsertEntityDoc(entityDocData);
			}
			catch
			{
				throw;
			}
		}

		public bool SaveEntityDoc(EntityDocData entityDocData)
		{
			bool flag = false;
			string text = "";
			try
			{
				string text2 = entityDocData.EntityDocTable.Rows[0]["EntityID"].ToString();
				string text3 = entityDocData.EntityDocTable.Rows[0]["EntitySysDocID"].ToString();
				EntityTypesEnum entityTypesEnum = (EntityTypesEnum)int.Parse(entityDocData.EntityDocTable.Rows[0]["EntityType"].ToString());
				if (entityTypesEnum == EntityTypesEnum.ExternalReports)
				{
					DeleteEntityDoc(entityDocData);
				}
				text = new Databases(base.DBConfig).GetFieldValue("Company", "FileSavingPath", "CompanyID", (byte)1, null).ToString();
				if (text == "" || text == null)
				{
					string executablePath = Application.ExecutablePath;
					text = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
				}
				string text4 = text + "\\" + entityTypesEnum.ToString() + "\\" + text2;
				if (entityTypesEnum == EntityTypesEnum.Transactions)
				{
					text4 = text + "\\" + entityTypesEnum.ToString() + "\\" + text3 + "\\" + text2;
				}
				foreach (DataRow row in entityDocData.EntityDocTable.Rows)
				{
					string text5 = row["EntityDocName"].ToString();
					row["EntityDocDesc"].ToString();
					row["EntityDocKeyword"].ToString();
					string text6 = text5;
					string text7 = text4 + "\\" + text6;
					int num = 1;
					while (File.Exists(text7))
					{
						text6 = Path.GetFileNameWithoutExtension(text5) + "_" + num + Path.GetExtension(text5);
						text7 = text4 + "\\" + text6;
						row["EntityDocName"] = text6;
						num++;
					}
					byte[] bytes = (byte[])row["FileData"];
					if (IsFileSavedToDisk(text4, text6, bytes))
					{
						row["EntityDocPath"] = text7;
					}
				}
				return InsertEntityDoc(entityDocData);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertEntityDoc(EntityDocData entityDocData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(entityDocData, "EntityDocs", insertUpdateCommand);
				string text = entityDocData.EntityDocTable.Rows[0]["EntityID"].ToString();
				AddActivityLog("EntityDoc", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EntityDocs", "EntityID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEntityDoc(EntityDocData entityDocData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(entityDocData, "EntityDocs", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = entityDocData.EntityDocTable.Rows[0]["EntityID"];
				UpdateTableRowByID("EntityDocs", "EntityID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = entityDocData.EntityDocTable.Rows[0]["EntityDocName"].ToString();
				AddActivityLog("EntityDoc", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EntityDocs", "EntityID", obj, sqlTransaction, isInsert: false);
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

		private bool IsFileSavedToDisk(string docPath, string docName, byte[] bytes)
		{
			try
			{
				if (!Directory.Exists(docPath))
				{
					Directory.CreateDirectory(docPath);
				}
				File.WriteAllBytes(docPath + "\\" + docName, bytes);
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool IsFileExist(EntityTypesEnum entityType, string entityID, string fileName)
		{
			bool result = true;
			string text = "";
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				text = new Databases(base.DBConfig).GetFieldValue("Company", "FileSavingPath", "CompanyID", (byte)1, sqlTransaction).ToString();
				if (text == "" || text == null)
				{
					string executablePath = Application.ExecutablePath;
					text = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
				}
				if (File.Exists(text + "\\" + entityType.ToString() + "\\" + entityID + "\\" + fileName))
				{
					return result;
				}
				result = false;
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public EntityDocData GetEntityDoc()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EntityDocs");
			EntityDocData entityDocData = new EntityDocData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(entityDocData, "EntityDocs", sqlBuilder);
			return entityDocData;
		}

		public DataSet GetEntityDocByFields(params string[] columns)
		{
			return GetEntityDocByFields(null, isInactive: true, columns);
		}

		public DataSet GetEntityDocByFields(string[] entityDocID, params string[] columns)
		{
			return GetEntityDocByFields(entityDocID, isInactive: true, columns);
		}

		public DataSet GetEntityDocByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EntityDocs");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "EntityID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "EntityDocs";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "EntityDocs", sqlBuilder);
			return dataSet;
		}

		public DataSet GetNotExistingDocs(DataSet dsTransactions)
		{
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				new SqlCommand().Transaction = sqlTransaction;
				DataSet dataSet = new DataSet();
				string text = "";
				DataTable dataTable = dsTransactions.Tables[0].DefaultView.ToTable(true, "SysDocType");
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					DataRow dataRow = dataTable.Rows[i];
					_ = (SysDocTypes)Enum.ToObject(typeof(SysDocTypes), int.Parse(dataRow["SysDocType"].ToString()));
					DoubleString tableName = GetTableName(1, int.Parse(dataRow["SysDocType"].ToString()));
					text += " SELECT SysDocID, VoucherID  FROM " + tableName.FirstString;
					if (i < dataTable.Rows.Count - 1)
					{
						text += " UNION ";
					}
				}
				SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(base.DBConfig.Connection, SqlBulkCopyOptions.Default, sqlTransaction);
				string exp = "Create Table TEMP_VALID_SYS (SysDocID nvarchar(7),VoucherID nvarchar(15),SysDocType tinyint) ";
				ExecuteNonQuery(exp, sqlTransaction);
				sqlBulkCopy.DestinationTableName = "TEMP_VALID_SYS";
				sqlBulkCopy.WriteToServer(dsTransactions.Tables[0]);
				string text2 = "select * from  TEMP_VALID_SYS  where  not  exists  ( select sysdocid, voucherid from (" + text + ") s where TEMP_VALID_SYS.SysDocID = s.SysDocID and TEMP_VALID_SYS.VoucherID = s.VoucherID  )";
				new SqlCommand(text2);
				FillDataSet(dataSet, "TransactionsNotExists", text2);
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (dsTransactions.Tables[0].Rows.Count > 0)
				{
					string exp2 = "DROP  Table TEMP_VALID_SYS";
					ExecuteNonQuery(exp2, sqlTransaction);
				}
			}
		}

		public DataSet TempDataset()
		{
			string text = "";
			DataSet dataSet = new DataSet();
			text = "select * from tmp_DATAtable";
			FillDataSet(dataSet, "Transactions", text);
			return dataSet;
		}

		public DataSet GetEntityApprovalStatus(DataSet dsTransanction, SysDocTypes docType, string UserID, bool includeApproveUser)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			DataSet dataSet = new DataSet();
			DataTable dataTable = dsTransanction.Tables[0];
			if (dataTable.Rows.Count > 0 && docType != SysDocTypes.None)
			{
				if (dataTable.Columns.Contains("SysDocID"))
				{
					DataRow dataRow = dataTable.Rows[0];
					text2 = dataRow["SysDocID"].ToString();
					text3 = dataRow["VoucherID"].ToString();
					int num = (int)Enum.Parse(typeof(SysDocTypes), docType.ToString());
					if (num != -1)
					{
						DoubleString tableName = GetTableName(1, num);
						DataSet dataSet2 = new DataSet();
						if (tableName.FirstString != "")
						{
							text = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS  WHERE  TABLE_NAME = '" + tableName.FirstString + "' AND COLUMN_NAME = 'ApprovalStatus'";
							FillDataSet(dataSet2, "columnexists", text);
						}
						if (dataSet2.Tables.Count > 0 && dataSet2.Tables[0].Rows.Count > 0)
						{
							text = " SELECT DISTINCT AL.ApproverID,TR.SysDocID, TR.VoucherID,ISNULL(TR.ApprovalStatus,0) as ApprovalStatus,A.NotifyonPrint,A.AllownextTransaction,A.AllowtoEdit,\r\n                            (SELECT TOP 1 ApproverID FROM Approval_Task AT WHERE AT.DocumentSysDocID=TR.SysDocID AND AT.DocumentCode=TR.VoucherID ORDER BY AT.DateApproved DESC) AS [ApprovedBy],\r\n                             CASE WHEN TR.ApprovalStatus=10 AND A.AllowtoEdit=1 AND(SELECT TOP 1 ApproverID FROM Approval_Task AT WHERE AT.DocumentSysDocID=TR.SysDocID AND AT.DocumentCode=TR.VoucherID ORDER BY AT.DateApproved DESC)<>'" + UserID + "' THEN 'false'\r\n                             ELSE 'true' END AS ModifyTransaction,CASE  WHEN TR.ApprovalStatus = 1 AND A.NotifyonPrint = 1 THEN 'PENDING' WHEN TR.ApprovalStatus = 2 AND  A.NotifyonPrint = 1 THEN 'WAITING' \r\n\t\t\t            \t WHEN TR.ApprovalStatus =  3  AND A.NotifyonPrint = 1 THEN 'REJECTED' WHEN TR.ApprovalStatus =10 AND A.NotifyonPrint = 1 THEN 'APPROVED' ELSE 'NA' END AS [APPROVAL_STATUS]   FROM " + tableName.FirstString + " TR INNER JOIN System_Document SD ON SD.SysDocID=TR.SysDocID \r\n                              LEFT JOIN Approval A ON SD.SysDocType=A.ObjectID LEFT JOIN Approval_Level AL ON A.ApprovalID=AL.ApprovalID  WHERE TR.SysDocID= '" + text2 + "'AND TR.VoucherID='" + text3 + "'";
							FillDataSet(dataSet, "Transactions", text);
						}
					}
					return dataSet;
				}
				return dataSet;
			}
			return dataSet;
		}

		public DoubleString GetTableName(int objectType, int objectID)
		{
			switch (objectType)
			{
			case 1:
				switch (objectID)
				{
				case 31:
				case 38:
					return new DoubleString("Purchase_Order", "VoucherID");
				case 213:
					return new DoubleString("Purchase_Cost_Entry", "VoucherID");
				case 115:
					return new DoubleString("Purchase_Order_NonInv", "VoucherID");
				case 116:
					return new DoubleString("Purchase_Invoice_NonInv", "VoucherID");
				case 30:
				case 63:
					return new DoubleString("Purchase_Quote", "VoucherID");
				case 33:
				case 34:
				case 39:
					return new DoubleString("Purchase_Invoice", "VoucherID");
				case 36:
					return new DoubleString("PO_Shipment", "VoucherID");
				case 25:
				case 26:
				case 51:
					return new DoubleString("Sales_Invoice", "VoucherID");
				case 259:
				case 260:
					return new DoubleString("Sales_Invoice_NonInv", "VoucherID");
				case 55:
					return new DoubleString("Consign_In", "VoucherID");
				case 57:
					return new DoubleString("ConsignIn_Return", "VoucherID");
				case 56:
					return new DoubleString("ConsignIn_Settlement", "VoucherID");
				case 47:
					return new DoubleString("Consign_Out", "VoucherID");
				case 54:
					return new DoubleString("ConsignOut_Return", "VoucherID");
				case 48:
					return new DoubleString("ConsignOut_Settlement", "VoucherID");
				case 24:
				case 53:
					return new DoubleString("Delivery_Note", "VoucherID");
				case 2:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 64:
				case 65:
				case 66:
					return new DoubleString("GL_Transaction", "VoucherID");
				case 3:
					return new DoubleString("GL_Transaction", "VoucherID");
				case 118:
					return new DoubleString("Sales_Enquiry", "VoucherID");
				case 23:
				case 52:
					return new DoubleString("Sales_Order", "VoucherID");
				case 22:
					return new DoubleString("Sales_Quote", "VoucherID");
				case 43:
					return new DoubleString("SalarySheet", "VoucherID");
				case 85:
					return new DoubleString("OverTimeEntry", "VoucherID");
				case 1:
					return new DoubleString("Journal", "VoucherID");
				case 27:
					return new DoubleString("Sales_Return", "VoucherID");
				case 59:
					return new DoubleString("FixedAsset_Transfer", "VoucherID");
				case 77:
					return new DoubleString("Job_Material_Requisition", "VoucherID");
				case 221:
					return new DoubleString("Employee_Appraisal", "VoucherID");
				case 220:
					return new DoubleString("Employee_GeneralActivity", "VoucherID");
				default:
					switch (objectID)
					{
					case 33:
					case 34:
					case 39:
						return new DoubleString("Purchase_Invoice", "VoucherID");
					case 32:
					case 50:
						return new DoubleString("Purchase_Receipt", "VoucherID");
					case 87:
						return new DoubleString("Inventory_Damage", "VoucherID");
					case 18:
						return new DoubleString("Inventory_Adjustment", "VoucherID");
					case 89:
						return new DoubleString("Inventory_Repacking", "VoucherID");
					case 208:
						return new DoubleString("Export_PickList", "VoucherID");
					case 217:
						return new DoubleString("CL_Voucher", "VoucherID");
					case 219:
						return new DoubleString("Credit_Limit_Review", "VoucherID");
					case 244:
						return new DoubleString("Purchase_PrePayment_Invoice", "VoucherID");
					case 215:
						return new DoubleString("Project_Subcontract_PO", "VoucherID");
					case 218:
						return new DoubleString("Project_SubContract_PI", "VoucherID");
					case 120:
						return new DoubleString("Service_CallTrack", "VoucherID");
					case 110:
						return new DoubleString("Job_Estimation", "VoucherID");
					case 76:
						return new DoubleString("Job_Timesheet", "VoucherID");
					case 73:
						return new DoubleString("Job_Expense_Issue", "VoucherID");
					case 71:
						return new DoubleString("Job_Inventory_Issue", "VoucherID");
					case 72:
						return new DoubleString("Job_Inventory_Return", "VoucherID");
					case 74:
						return new DoubleString("Job_Invoice", "VoucherID");
					case 261:
						return new DoubleString("FixedAsset_Purchase_Order", "VoucherID");
					case 81:
						return new DoubleString("Job", "JobID");
					case 211:
						return new DoubleString("Job_Maintenance_Service", "VoucherID");
					case 69:
					case 70:
					case 99:
						return new DoubleString("Payroll_Transaction", "VoucherID");
					case 108:
						return new DoubleString("Project_Expense_Allocation", "VoucherID");
					case 94:
						return new DoubleString("LPO_Receipt", "VoucherID");
					case 79:
						return new DoubleString("Bank_Facility_Transaction", "VoucherID");
					case 80:
						return new DoubleString("Bank_Facility_Payment", "VoucherID");
					case 248:
						return new DoubleString("TR_Application", "VoucherID");
					case 84:
						return new DoubleString("Cheque_Discount", "VoucherID");
					case 83:
						return new DoubleString("Cheque_Send", "VoucherID");
					case 17:
						return new DoubleString("Security_Cheque", "VoucherID");
					default:
						return new DoubleString("", "");
					}
				}
			case 2:
			{
				DataComboType dataComboType = (DataComboType)objectID;
				switch (dataComboType)
				{
				case DataComboType.Customer:
					return new DoubleString("Customer", "CustomerID");
				case DataComboType.Vendor:
					return new DoubleString("Vendor", "VendorID");
				case DataComboType.Employee:
					return new DoubleString("Employee", "EmployeeID");
				case DataComboType.Job:
					return new DoubleString("Job", "JobID");
				case DataComboType.Accounts:
					return new DoubleString("Account", "AccountID");
				case DataComboType.Product:
					return new DoubleString("Product", "ProductID");
				case DataComboType.JobBOM:
					return new DoubleString("Job_BOM", "JobBOMID");
				default:
					throw new Exception("Card type is not implemented in approval function 'GetTableName'. Type:" + dataComboType.ToString());
				}
			}
			default:
				throw new Exception("Object type is not implemented in approval function 'GetTableName'.");
			}
		}
	}
}
