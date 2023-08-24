using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class BouncedChecks : StoreObject
	{
		public const string BOUNCEDCHECKID_PARM = "@BouncedCheckID";

		public const string ORIGINALCHECKID_PARM = "@OriginalCheckID";

		public const string BOUNCEDDATE_PARM = "@BouncedDate";

		public const string ISREASONBANKCREDIT_PARM = "@IsReasonBankCredit";

		public const string DESCRIPTION_PARM = "@Description";

		public BouncedChecks(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Bounced Checks]", new FieldValue("OriginalCheckID", "@OriginalCheckID"), new FieldValue("BouncedDate", "@BouncedDate"), new FieldValue("IsReasonBankCredit", "@IsReasonBankCredit"), new FieldValue("Description", "@Description"));
			return sqlBuilder.GetInsertExpression();
		}

		public SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@OriginalCheckID", SqlDbType.Int);
				parameters.Add("@BouncedDate", SqlDbType.DateTime);
				parameters.Add("@IsReasonBankCredit", SqlDbType.Bit);
				parameters.Add("@Description", SqlDbType.NVarChar);
				parameters["@OriginalCheckID"].SourceColumn = "OriginalCheckID";
				parameters["@BouncedDate"].SourceColumn = "BouncedDate";
				parameters["@IsReasonBankCredit"].SourceColumn = "IsReasonBankCredit";
				parameters["@Description"].SourceColumn = "Description";
			}
			return insertCommand;
		}

		public SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand("InsertBouncedCheck", base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.StoredProcedure;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add("@BouncedCheckID", SqlDbType.Int);
				parameters.Add("@OriginalCheckID", SqlDbType.Int);
				parameters.Add("@BouncedDate", SqlDbType.DateTime);
				parameters.Add("@IsReasonBankCredit", SqlDbType.Bit);
				parameters.Add("@Description", SqlDbType.NVarChar);
				parameters["@BouncedCheckID"].SourceColumn = "BouncedCheckID";
				parameters["@OriginalCheckID"].SourceColumn = "OriginalCheckID";
				parameters["@BouncedDate"].SourceColumn = "BouncedDate";
				parameters["@IsReasonBankCredit"].SourceColumn = "IsReasonBankCredit";
				parameters["@Description"].SourceColumn = "Description";
			}
			return updateCommand;
		}

		public bool InsertBouncedCheck(string origCheckID, DateTime date, bool isReasonBankCredit, string description, SqlTransaction sqlTransaction)
		{
			BouncedCheckData bouncedCheckData = new BouncedCheckData();
			DataRow dataRow = bouncedCheckData.BouncedCheckTable.NewRow();
			dataRow["OriginalCheckID"] = origCheckID;
			dataRow["BouncedDate"] = date;
			dataRow["IsReasonBankCredit"] = isReasonBankCredit;
			dataRow["Description"] = description;
			bouncedCheckData.BouncedCheckTable.Rows.Add(dataRow);
			return InsertBouncedCheck(bouncedCheckData, sqlTransaction);
		}

		public bool InsertBouncedCheck(BouncedCheckData bouncedCheckData, SqlTransaction sqlTransaction)
		{
			if (bouncedCheckData.Tables["[Bounced Checks]"].Rows.Count > 0)
			{
				return Insert(bouncedCheckData, "[Bounced Checks]", GetInsertCommand(), sqlTransaction);
			}
			return true;
		}

		public bool UpdateBouncedCheck(BouncedCheckData bouncedCheckData)
		{
			return Update(bouncedCheckData, "[Bounced Checks]", GetUpdateCommand());
		}

		public DataSet GetBouncedChecks()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddColumn("GL_Transaction", "TransactionID");
			sqlBuilder.AddColumn("GL_Transaction", "Amount");
			sqlBuilder.AddColumn("[Transaction Checks]", "CheckDate");
			sqlBuilder.AddColumn("[Transaction Checks]", "CheckNumber");
			sqlBuilder.AddColumn("[Bounced Checks]", "BouncedDate");
			sqlBuilder.AddColumn("[Bounced Checks]", "Description");
			sqlBuilder.AddColumn("Partners", "Name");
			sqlBuilder.AddJointer("GL_Transaction", "TransactionID", "[Transaction Checks]", "TransactionID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("GL_Transaction", "TransactionID", "[Partner Transactions]", "TransactionID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("[Partner Transactions]", "PartnerID", "Partners", "PartnerID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("[Transaction Checks]", "CheckID", "[Bounced Checks]", "OriginalCheckID", JoinType.RightOuterJoin);
			sqlBuilder.JointerSourceTable = "GL_Transaction";
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			try
			{
				FillDataSet(dataSet, "[Bounced Checks]", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public BouncedCheckData GetBouncedCheck(string checkID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.TableName = "[Bounced Checks]";
			commandHelper.FieldName = "OriginalCheckID";
			commandHelper.FieldValue = checkID;
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			BouncedCheckData bouncedCheckData = new BouncedCheckData();
			try
			{
				FillDataSet(bouncedCheckData, "[Bounced Checks]", sqlBuilder);
				return bouncedCheckData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBouncedChecks(int partnerID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddColumn("GL_Transaction", "TransactionID");
			sqlBuilder.AddColumn("GL_Transaction", "Amount");
			sqlBuilder.AddColumn("[Transaction Checks]", "CheckDate");
			sqlBuilder.AddColumn("[Transaction Checks]", "CheckNumber");
			sqlBuilder.AddColumn("[Bounced Checks]", "BouncedDate");
			sqlBuilder.AddColumn("[Bounced Checks]", "Description");
			sqlBuilder.AddColumn("Partners", "Name");
			sqlBuilder.AddJointer("GL_Transaction", "TransactionID", "[Transaction Checks]", "TransactionID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("GL_Transaction", "TransactionID", "[Partner Transactions]", "TransactionID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("[Partner Transactions]", "PartnerID", "Partners", "PartnerID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("[Transaction Checks]", "CheckID", "[Bounced Checks]", "OriginalCheckID", JoinType.RightOuterJoin);
			sqlBuilder.JointerSourceTable = "GL_Transaction";
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Partner Transactions]";
			commandHelper.FieldName = "PartnerID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = partnerID;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			try
			{
				FillDataSet(dataSet, "[Bounced Checks]", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBouncedChecks(DateTime from, DateTime to)
		{
			return null;
		}

		public DataSet GetBouncedChecks(int partnerID, DateTime from, DateTime to)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddColumn("GL_Transaction", "TransactionID");
			sqlBuilder.AddColumn("GL_Transaction", "Amount");
			sqlBuilder.AddColumn("[Transaction Checks]", "CheckDate");
			sqlBuilder.AddColumn("[Transaction Checks]", "CheckNumber");
			sqlBuilder.AddColumn("[Bounced Checks]", "BouncedDate");
			sqlBuilder.AddColumn("[Bounced Checks]", "Description");
			sqlBuilder.AddColumn("Partners", "Name");
			sqlBuilder.AddJointer("GL_Transaction", "TransactionID", "[Transaction Checks]", "TransactionID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("GL_Transaction", "TransactionID", "[Partner Transactions]", "TransactionID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("[Partner Transactions]", "PartnerID", "Partners", "PartnerID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("[Transaction Checks]", "CheckID", "[Bounced Checks]", "OriginalCheckID", JoinType.RightOuterJoin);
			sqlBuilder.JointerSourceTable = "GL_Transaction";
			commandHelper = new CommandHelper();
			commandHelper.TableName = "GL_Transaction";
			commandHelper.FieldName = "GLType";
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "GL_Transaction";
			commandHelper.FieldName = "PaymentMethodID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = (byte)2;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Partner Transactions]";
			commandHelper.FieldName = "PartnerID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = partnerID;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "BouncedDate";
			commandHelper.SqlFieldType = SqlDbType.DateTime;
			commandHelper.FieldValue = from.ToString(StoreConfiguration.CurrentCulture);
			commandHelper.FieldValue2 = to.ToString(StoreConfiguration.CurrentCulture);
			commandHelper.TableName = "[Bounced Checks]";
			commandHelper.SqlOp.LogicalValueOp = LogicalValueOperator.BETWEEN;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			try
			{
				FillDataSet(dataSet, "[Bounced Checks]", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBouncedChecks(int[] partnersID, DateTime from, DateTime to)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddColumn("GL_Transaction", "TransactionID");
			sqlBuilder.AddColumn("GL_Transaction", "Amount");
			sqlBuilder.AddColumn("[Transaction Checks]", "CheckDate");
			sqlBuilder.AddColumn("[Transaction Checks]", "CheckNumber");
			sqlBuilder.AddColumn("[Bounced Checks]", "BouncedDate");
			sqlBuilder.AddColumn("[Bounced Checks]", "Description");
			sqlBuilder.AddColumn("Partners", "Name");
			sqlBuilder.AddJointer("GL_Transaction", "TransactionID", "[Transaction Checks]", "TransactionID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("GL_Transaction", "TransactionID", "[Partner Transactions]", "TransactionID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("[Partner Transactions]", "PartnerID", "Partners", "PartnerID", JoinType.RightOuterJoin);
			sqlBuilder.AddJointer("[Transaction Checks]", "CheckID", "[Bounced Checks]", "OriginalCheckID", JoinType.RightOuterJoin);
			sqlBuilder.JointerSourceTable = "GL_Transaction";
			commandHelper = new CommandHelper();
			commandHelper.TableName = "GL_Transaction";
			commandHelper.FieldName = "GLType";
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "GL_Transaction";
			commandHelper.FieldName = "PaymentMethodID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = (byte)2;
			sqlBuilder.AddCommandHelper(commandHelper);
			if (partnersID.Length != 0)
			{
				commandHelper = new CommandHelper();
				commandHelper.TableName = "[Partner Transactions]";
				commandHelper.FieldName = "PartnerID";
				commandHelper.SqlFieldType = SqlDbType.Int;
				commandHelper.FieldValue = partnersID;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (from.Year != DateTime.MinValue.Year)
			{
				commandHelper = new CommandHelper();
				commandHelper.FieldName = "BouncedDate";
				commandHelper.SqlFieldType = SqlDbType.DateTime;
				commandHelper.FieldValue = from.ToString(StoreConfiguration.CurrentCulture);
				commandHelper.FieldValue2 = to.ToString(StoreConfiguration.CurrentCulture);
				commandHelper.TableName = "[Bounced Checks]";
				commandHelper.SqlOp.LogicalValueOp = LogicalValueOperator.BETWEEN;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			try
			{
				FillDataSet(dataSet, "[Bounced Checks]", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
