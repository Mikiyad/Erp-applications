using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Data.WS;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Micromind.Data
{
	public sealed class Licenses : StoreObject
	{
		private static string LICENSEID_PARM = "@LicenseID";

		private static string LICENSEKEY_PARM = "@LicenseKey";

		private static string LICENSECODE_PARM = "@LicenseCode";

		private static string NUMACTIVATED_PARM = "@NumActivated";

		private static string FIRSTDATEACTIVATED_PARM = "@FirstDateActivated";

		private static string LASTDATEACTIVATED_PARM = "@LastDateActivated";

		private static string COMPANY_PARM = "@Company";

		private static string EMAIL_PARM = "@Email";

		private static string PHONE_PARM = "@Phone";

		private static string CONTACT_PARM = "@Contact";

		private static string MACHINEID_PARM = "@MachineID";

		public bool CheckConlicense = true;

		public Licenses(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters(LicenseData.LICENSE_TABLE, new FieldValue(LicenseData.LICENSEKEY_FIELD, LICENSEKEY_PARM), new FieldValue(LicenseData.LICENSECODE_FIELD, LICENSECODE_PARM), new FieldValue(LicenseData.NUMACTIVATED_FIELD, NUMACTIVATED_PARM), new FieldValue(LicenseData.FIRSTDATEACTIVATED_FIELD, FIRSTDATEACTIVATED_PARM), new FieldValue(LicenseData.LASTDATEACTIVATED_FIELD, LASTDATEACTIVATED_PARM), new FieldValue(LicenseData.COMPANY_FIELD, COMPANY_PARM), new FieldValue(LicenseData.EMAIL_FIELD, EMAIL_PARM), new FieldValue(LicenseData.PHONE_FIELD, PHONE_PARM), new FieldValue(LicenseData.CONTACT_FIELD, CONTACT_PARM), new FieldValue(LicenseData.MACHINEID_FIELD, MACHINEID_PARM));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters(LicenseData.LICENSE_TABLE, new FieldValue(LicenseData.LICENSEID_FIELD, LICENSEID_PARM, isUpdateConditionField: true), new FieldValue(LicenseData.LICENSEKEY_FIELD, LICENSEKEY_PARM), new FieldValue(LicenseData.LICENSECODE_FIELD, LICENSECODE_PARM), new FieldValue(LicenseData.NUMACTIVATED_FIELD, NUMACTIVATED_PARM), new FieldValue(LicenseData.FIRSTDATEACTIVATED_FIELD, FIRSTDATEACTIVATED_PARM), new FieldValue(LicenseData.LASTDATEACTIVATED_FIELD, LASTDATEACTIVATED_PARM), new FieldValue(LicenseData.COMPANY_FIELD, COMPANY_PARM), new FieldValue(LicenseData.EMAIL_FIELD, EMAIL_PARM), new FieldValue(LicenseData.PHONE_FIELD, PHONE_PARM), new FieldValue(LicenseData.CONTACT_FIELD, CONTACT_PARM), new FieldValue(LicenseData.MACHINEID_FIELD, MACHINEID_PARM));
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add(LICENSEKEY_PARM, SqlDbType.NVarChar);
				parameters.Add(LICENSECODE_PARM, SqlDbType.NVarChar);
				parameters.Add(NUMACTIVATED_PARM, SqlDbType.Int);
				parameters.Add(FIRSTDATEACTIVATED_PARM, SqlDbType.DateTime);
				parameters.Add(LASTDATEACTIVATED_PARM, SqlDbType.DateTime);
				parameters.Add(COMPANY_PARM, SqlDbType.NVarChar);
				parameters.Add(EMAIL_PARM, SqlDbType.NVarChar);
				parameters.Add(PHONE_PARM, SqlDbType.NVarChar);
				parameters.Add(CONTACT_PARM, SqlDbType.NVarChar);
				parameters.Add(MACHINEID_PARM, SqlDbType.NVarChar);
				parameters[LICENSEKEY_PARM].SourceColumn = LicenseData.LICENSEKEY_FIELD;
				parameters[LICENSECODE_PARM].SourceColumn = LicenseData.LICENSECODE_FIELD;
				parameters[NUMACTIVATED_PARM].SourceColumn = LicenseData.NUMACTIVATED_FIELD;
				parameters[FIRSTDATEACTIVATED_PARM].SourceColumn = LicenseData.FIRSTDATEACTIVATED_FIELD;
				parameters[LASTDATEACTIVATED_PARM].SourceColumn = LicenseData.LASTDATEACTIVATED_FIELD;
				parameters[COMPANY_PARM].SourceColumn = LicenseData.COMPANY_FIELD;
				parameters[EMAIL_PARM].SourceColumn = LicenseData.EMAIL_FIELD;
				parameters[PHONE_PARM].SourceColumn = LicenseData.PHONE_FIELD;
				parameters[CONTACT_PARM].SourceColumn = LicenseData.CONTACT_FIELD;
				parameters[MACHINEID_PARM].SourceColumn = LicenseData.MACHINEID_FIELD;
			}
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand(GetUpdateText(), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add(LICENSEID_PARM, SqlDbType.Int);
				parameters.Add(LICENSEKEY_PARM, SqlDbType.NVarChar);
				parameters.Add(LICENSECODE_PARM, SqlDbType.NVarChar);
				parameters.Add(NUMACTIVATED_PARM, SqlDbType.Int);
				parameters.Add(FIRSTDATEACTIVATED_PARM, SqlDbType.DateTime);
				parameters.Add(LASTDATEACTIVATED_PARM, SqlDbType.DateTime);
				parameters.Add(COMPANY_PARM, SqlDbType.NVarChar);
				parameters.Add(EMAIL_PARM, SqlDbType.NVarChar);
				parameters.Add(PHONE_PARM, SqlDbType.NVarChar);
				parameters.Add(CONTACT_PARM, SqlDbType.NVarChar);
				parameters.Add(MACHINEID_PARM, SqlDbType.NVarChar);
				parameters[LICENSEID_PARM].SourceColumn = LicenseData.LICENSEID_FIELD;
				parameters[LICENSEKEY_PARM].SourceColumn = LicenseData.LICENSEKEY_FIELD;
				parameters[LICENSECODE_PARM].SourceColumn = LicenseData.LICENSECODE_FIELD;
				parameters[NUMACTIVATED_PARM].SourceColumn = LicenseData.NUMACTIVATED_FIELD;
				parameters[FIRSTDATEACTIVATED_PARM].SourceColumn = LicenseData.FIRSTDATEACTIVATED_FIELD;
				parameters[LASTDATEACTIVATED_PARM].SourceColumn = LicenseData.LASTDATEACTIVATED_FIELD;
				parameters[COMPANY_PARM].SourceColumn = LicenseData.COMPANY_FIELD;
				parameters[EMAIL_PARM].SourceColumn = LicenseData.EMAIL_FIELD;
				parameters[PHONE_PARM].SourceColumn = LicenseData.PHONE_FIELD;
				parameters[CONTACT_PARM].SourceColumn = LicenseData.CONTACT_FIELD;
				parameters[MACHINEID_PARM].SourceColumn = LicenseData.MACHINEID_FIELD;
			}
			return updateCommand;
		}

		public bool InsertLicense(LicenseData licenseData)
		{
			bool flag = true;
			if (licenseData == null)
			{
				throw new NullReferenceException("LicenseData is null.");
			}
			if (licenseData.LicenseTable.Rows.Count == 0)
			{
				return flag;
			}
			string text = licenseData.LicenseTable.Rows[0][LicenseData.MACHINEID_FIELD].ToString();
			if (text.Trim() == string.Empty)
			{
				throw new ApplicationException("Machine id is empty.");
			}
			DataRow dataRow = licenseData.LicenseTable.Rows[0];
			dataRow[LicenseData.NUMACTIVATED_FIELD] = 1;
			dataRow[LicenseData.FIRSTDATEACTIVATED_FIELD] = DateTime.Now;
			dataRow[LicenseData.LASTDATEACTIVATED_FIELD] = DateTime.Now;
			if (ExistLicenseByMachineID(text))
			{
				return UpdateLicense(licenseData);
			}
			SqlCommand insertCommand = GetInsertCommand();
			try
			{
				flag &= Insert(licenseData, LicenseData.LICENSE_TABLE, insertCommand);
				string entiyID = dataRow[LicenseData.LICENSEKEY_FIELD].ToString();
				AddActivityLog("License Key", entiyID, ActivityTypes.Add, null);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		private bool UpdateLicense(LicenseData licenseData)
		{
			bool flag = true;
			DataRow dataRow = licenseData.LicenseTable.Rows[0];
			LicenseData licenseByMachineID = GetLicenseByMachineID(dataRow[LicenseData.MACHINEID_FIELD].ToString());
			if (licenseByMachineID.LicenseTable.Rows.Count == 0)
			{
				throw new ApplicationException("No license data found.");
			}
			DataRow dataRow2 = licenseByMachineID.LicenseTable.Rows[0];
			int num = int.Parse(dataRow2[LicenseData.NUMACTIVATED_FIELD].ToString());
			dataRow2[LicenseData.NUMACTIVATED_FIELD] = num + 1;
			dataRow2[LicenseData.LASTDATEACTIVATED_FIELD] = DateTime.Now;
			licenseByMachineID.AcceptChanges();
			dataRow2[LicenseData.LICENSEID_FIELD] = dataRow2[LicenseData.LICENSEID_FIELD];
			SqlCommand updateCommand = GetUpdateCommand();
			try
			{
				flag &= Update(licenseByMachineID, LicenseData.LICENSE_TABLE, updateCommand);
				string entiyID = dataRow[LicenseData.LICENSEKEY_FIELD].ToString();
				AddActivityLog("License Key", entiyID, ActivityTypes.Update, null);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				if (licenseByMachineID != null)
				{
					licenseByMachineID.Dispose();
					licenseByMachineID = null;
				}
			}
		}

		public LicenseData GetLicenses()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = LicenseData.LICENSE_TABLE;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddOrderByColumn(LicenseData.LICENSE_TABLE, LicenseData.COMPANY_FIELD);
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = false;
			LicenseData licenseData = new LicenseData();
			FillDataSet(licenseData, LicenseData.LICENSE_TABLE, sqlBuilder);
			return licenseData;
		}

		public LicenseData GetLicenseByID(int licenseID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = LicenseData.LICENSEID_FIELD;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = licenseID;
			commandHelper.TableName = LicenseData.LICENSE_TABLE;
			sqlBuilder.AddCommandHelper(commandHelper);
			LicenseData licenseData = new LicenseData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(licenseData, LicenseData.LICENSE_TABLE, sqlBuilder);
				return licenseData;
			}
			catch
			{
				throw;
			}
		}

		public LicenseData GetLicenseByMachineID(string machineID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = LicenseData.MACHINEID_FIELD;
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = machineID;
			commandHelper.TableName = LicenseData.LICENSE_TABLE;
			sqlBuilder.AddCommandHelper(commandHelper);
			LicenseData licenseData = new LicenseData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(licenseData, LicenseData.LICENSE_TABLE, sqlBuilder);
				return licenseData;
			}
			catch
			{
				throw;
			}
		}

		public bool ExistLicenseByMachineID(string machineID)
		{
			try
			{
				return IsTableFieldValueExist(LicenseData.LICENSE_TABLE, LicenseData.MACHINEID_FIELD, machineID);
			}
			catch
			{
				throw;
			}
		}

		public string GetLicenseIDByMachineID(string machineID)
		{
			try
			{
				object obj = ExecuteSelectScalar(LicenseData.LICENSE_TABLE, LicenseData.MACHINEID_FIELD, machineID, LicenseData.LICENSEID_FIELD);
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "-1";
		}

		public string GetLicenseMachineIDByID(int id)
		{
			object obj = ExecuteSelectScalar(LicenseData.LICENSE_TABLE, LicenseData.LICENSEID_FIELD, id, LicenseData.MACHINEID_FIELD);
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return "";
		}

		public int GetNumberOfUsesByKey(string key)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT COUNT(").Append(LicenseData.LICENSEKEY_FIELD).Append(") ");
			stringBuilder.Append("FROM ").Append(LicenseData.LICENSE_TABLE).Append(" ");
			stringBuilder.Append("WHERE ").Append(LicenseData.LICENSEKEY_FIELD).Append("='");
			stringBuilder.Append(key).Append("'");
			object obj = ExecuteScalar(stringBuilder.ToString());
			if (obj != null && obj != DBNull.Value)
			{
				return int.Parse(obj.ToString());
			}
			return 0;
		}
	}
}
