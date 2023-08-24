using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class VehicleDocuments : StoreObject
	{
		private const string VEHICLEDOCUMENT_TABLE = "Vehicle_Document";

		private const string VEHICLEID_PARM = "@VehicleID";

		private const string DOCUMENTNUMBER_PARM = "@DocumentNumber";

		private const string DOCUMENTTYPEID_PARM = "@DocumentTypeID";

		private const string ISSUEPLACE_PARM = "@IssuePlace";

		private const string ISSUEDATE_PARM = "@IssueDate";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string REMARKS_PARM = "@Remarks";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public VehicleDocuments(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vehicle_Document", new FieldValue("VehicleID", "@VehicleID", isUpdateConditionField: true), new FieldValue("DocumentNumber", "@DocumentNumber", isUpdateConditionField: true), new FieldValue("DocumentTypeID", "@DocumentTypeID"), new FieldValue("IssuePlace", "@IssuePlace"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Vehicle_Document", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters.Add("@DocumentNumber", SqlDbType.NVarChar);
			parameters.Add("@DocumentTypeID", SqlDbType.NVarChar);
			parameters.Add("@IssuePlace", SqlDbType.NVarChar);
			parameters.Add("@IssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@ExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@DocumentNumber"].SourceColumn = "DocumentNumber";
			parameters["@DocumentTypeID"].SourceColumn = "DocumentTypeID";
			parameters["@IssuePlace"].SourceColumn = "IssuePlace";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@Remarks"].SourceColumn = "Remarks";
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

		public bool InsertVehicleDocument(VehicleDocumentData accountVehicleDocumentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountVehicleDocumentData.VehicleDocumentTable.Rows[0]["VehicleID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteVehicleDocuments(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountVehicleDocumentData, "Vehicle_Document", insertUpdateCommand);
				string text = accountVehicleDocumentData.VehicleDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Vehicle Document", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Vehicle_Document", "DocumentNumber", text, sqlTransaction, isInsert: true);
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

		public bool UpdateVehicleDocument(VehicleDocumentData accountVehicleDocumentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountVehicleDocumentData.VehicleDocumentTable.Rows[0]["VehicleID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteVehicleDocuments(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(accountVehicleDocumentData, "Vehicle_Document", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountVehicleDocumentData.VehicleDocumentTable.Rows[0]["DocumentNumber"];
				UpdateTableRowByID("Vehicle_Document", "DocumentNumber", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountVehicleDocumentData.VehicleDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Vehicle Document", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Vehicle_Document", "DocumentNumber", obj, sqlTransaction, isInsert: false);
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

		private bool DeleteVehicleDocuments(SqlTransaction sqlTransaction, string employeeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Vehicle_Document WHERE VehicleID = '" + employeeID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Vehicle Document", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public VehicleDocumentData GetVehicleDocument()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vehicle_Document");
			VehicleDocumentData vehicleDocumentData = new VehicleDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vehicleDocumentData, "Vehicle_Document", sqlBuilder);
			return vehicleDocumentData;
		}

		public VehicleDocumentData GetVehicleDocumentsByVehicleID(string employeeID)
		{
			VehicleDocumentData vehicleDocumentData = new VehicleDocumentData();
			string textCommand = "Select VehicleID,DocumentNumber,DocumentTypeID,IssuePlace,IssueDate,ExpiryDate,Remarks \r\n                            FROM Vehicle_Document\r\n                            WHERE VehicleID='" + employeeID + "' ORDER BY RowIndex";
			FillDataSet(vehicleDocumentData, "Vehicle_Document", textCommand);
			return vehicleDocumentData;
		}

		public bool DeleteVehicleDocument(string employeeDocumentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Vehicle_Document WHERE DocumentNumber = '" + employeeDocumentID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Vehicle Document", employeeDocumentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public VehicleDocumentData GetVehicleDocumentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DocumentNumber";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Vehicle_Document";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			VehicleDocumentData vehicleDocumentData = new VehicleDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vehicleDocumentData, "Vehicle_Document", sqlBuilder);
			return vehicleDocumentData;
		}

		public DataSet GetVehicleDocumentByFields(params string[] columns)
		{
			return GetVehicleDocumentByFields(null, isInactive: true, columns);
		}

		public DataSet GetVehicleDocumentByFields(string[] employeeDocumentID, params string[] columns)
		{
			return GetVehicleDocumentByFields(employeeDocumentID, isInactive: true, columns);
		}

		public DataSet GetVehicleDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vehicle_Document");
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
				commandHelper.FieldName = "DocumentNumber";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Vehicle_Document";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Vehicle_Document", sqlBuilder);
			return dataSet;
		}

		public DataSet GetVehicleDocumentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ED.VehicleID,E.FirstName + ' ' + E.LastName AS [Vehicle Name],DT.TypeName AS [Type], DocumentNumber, IssuePlace,IssueDate [Issue Date],ExpiryDate [Expiry Date]   FROM Vehicle_Document ED INNER JOIN Vehicle E ON ED.VehicleID = E.VehicleID\r\n                                INNER JOIN Vehicle_Doc_Type DT ON DT.TypeID = ED.DocumentTypeID ";
			FillDataSet(dataSet, "Vehicle_Document", textCommand);
			return dataSet;
		}

		public DataSet GetVehicleDocumentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VehicleDocumentID [Code],VehicleDocumentName [Name]\r\n                           FROM VehicleDocument ORDER BY VehicleDocumentID,VehicleDocumentName";
			FillDataSet(dataSet, "Vehicle_Document", textCommand);
			return dataSet;
		}
	}
}
