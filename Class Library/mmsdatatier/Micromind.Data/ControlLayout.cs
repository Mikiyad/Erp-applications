using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ControlLayout : StoreObject
	{
		private const string CONTROLTYPE_PARM = "@ControlType";

		private const string LAYOUTNAME_PARM = "@LayoutName";

		private const string CONTROLNAME_PARM = "@ControlName";

		public const string LAYOUTDATA_PARM = "@LayoutData";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ControlLayout(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Control_Layout", new FieldValue("ControlType", "@ControlType", isUpdateConditionField: true), new FieldValue("LayoutName", "@LayoutName", isUpdateConditionField: true), new FieldValue("ControlName", "@ControlName", isUpdateConditionField: true), new FieldValue("LayoutData", "@LayoutData"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Control_Layout", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ControlType", SqlDbType.TinyInt);
			parameters.Add("@LayoutName", SqlDbType.NVarChar);
			parameters.Add("@ControlName", SqlDbType.NVarChar);
			parameters.Add("@LayoutData", SqlDbType.Image);
			parameters["@ControlType"].SourceColumn = "ControlType";
			parameters["@LayoutName"].SourceColumn = "LayoutName";
			parameters["@ControlName"].SourceColumn = "ControlName";
			parameters["@LayoutData"].SourceColumn = "LayoutData";
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

		public bool InsertControlLayout(ControlLayoutData controlLayoutData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				string text = controlLayoutData.ControlLayoutTable.Rows[0]["ControlName"].ToString();
				string text2 = controlLayoutData.ControlLayoutTable.Rows[0]["LayoutName"].ToString();
				string exp = "DELETE FROM Control_Layout WHERE ControlName = '" + text + "' AND LayoutName = '" + text2 + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag = Insert(controlLayoutData, "Control_Layout", insertUpdateCommand);
				string text3 = controlLayoutData.ControlLayoutTable.Rows[0]["ControlName"].ToString();
				if (byte.Parse(controlLayoutData.ControlLayoutTable.Rows[0]["ControlType"].ToString()) == 1)
				{
					AddActivityLog("Form Layout", text3, ActivityTypes.Add, sqlTransaction);
				}
				else
				{
					AddActivityLog("Layout", text3, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Control_Layout", "ControlName", text3, sqlTransaction, isInsert: true);
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

		public ControlLayoutData GetControlLayout()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Control_Layout");
			ControlLayoutData controlLayoutData = new ControlLayoutData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(controlLayoutData, "Control_Layout", sqlBuilder);
			return controlLayoutData;
		}

		public bool DeleteControlLayout(ControlLayoutTypes type, string controlName, string layoutName)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Control_Layout WHERE ControlType = " + (byte)type + " AND ControlName = '" + controlName + "' AND LayoutName = '" + layoutName + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Layout", controlName + "-" + layoutName, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public byte[] GetControlLayoutByID(ControlLayoutTypes type, string controlName, string layoutName)
		{
			try
			{
				if (layoutName == "")
				{
					layoutName = "Default";
				}
				string exp = "SELECT LayoutData FROM Control_Layout  CL WHERE ControlType = " + (byte)type + " AND ControlName = '" + controlName + "' AND LayoutName = '" + layoutName + "'";
				byte[] array = (byte[])ExecuteScalar(exp);
				if (array != null)
				{
					array = CommonLib.CompressData(array);
				}
				return array;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetControlLayoutComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ControlType,ControlName [Code],LayoutName [Name]  FROM Control_Layout ";
			FillDataSet(dataSet, "Control_Layout", textCommand);
			return dataSet;
		}
	}
}
