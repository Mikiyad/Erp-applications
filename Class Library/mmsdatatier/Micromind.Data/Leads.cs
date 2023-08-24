using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Data.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Leads : StoreObject
	{
		private const string LEADID_PARM = "@LeadID";

		private const string LEADNAME_PARM = "@LeadName";

		private const string FOREIGNNAME_PARM = "@ForeignName";

		private const string COMPANYNAME_PARM = "@CompanyName";

		private const string FIRSTNAME_PARM = "@FirstName";

		private const string LASTNAME_PARM = "@LastName";

		private const string MIDDLENAME_PARM = "@MiddleName";

		private const string NOTE_PARM = "@Note";

		private const string PARENTLEADID_PARM = "@ParentLeadID";

		private const string AREAID_PARM = "@AreaID";

		private const string COUNTRYID_PARM = "@CountryID";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string STAGEID_PARM = "@StageID";

		private const string PRIMARYADDRESSID_PARM = "@PrimaryAddressID";

		private const string SHORTNAME_PARM = "@ShortName";

		private const string SUBAREAID_PARM = "@SubAreaID";

		private const string RATING_PARM = "@Rating";

		private const string DATEESTABLISHED_PARM = "@DateEstablished";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string ISLEADSINCE_PARM = "@IsLeadSince";

		private const string LEADSOURCEID_PARM = "@LeadSourceID";

		private const string LEADOWNERID_PARM = "@LeadOwnerID";

		private const string INDUSTRYID_PARM = "@IndustryID";

		private const string LEADSTATUS_PARM = "@LeadStatus";

		private const string COMPANYSIZE_PARM = "@CompanySize";

		private const string EMAILOPTOUT_PARM = "@EmailOptOut";

		private const string ANNUALTURNOVER_PARM = "@AnnualTurnOver";

		private const string EMPLOYEECOUNT_PARM = "@EmployeeCount";

		private const string REFERREDBY_PARM = "@ReferredBy";

		private const string PROFILEDETAILS_PARM = "@ProfileDetails";

		private const string EXPECTVALUE_PARM = "@ExpectValue";

		public const string REASONID_PARM = "@ReasonID";

		public const string REMARKS_PARM = "@Remarks";

		private const string USERDEFINED1_PARM = "@UserDefined1";

		private const string USERDEFINED2_PARM = "@UserDefined2";

		private const string USERDEFINED3_PARM = "@UserDefined3";

		private const string USERDEFINED4_PARM = "@UserDefined4";

		private const string SALESPERSONID_PARM = "@SalesPersonID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string CONTACTID_PARM = "@ContactID";

		private const string JOBTITLE_PARM = "@JobTitle";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ISINACTIVE_PARM = "@IsInactive";

		public Leads(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Lead", new FieldValue("LeadID", "@LeadID", isUpdateConditionField: true), new FieldValue("LeadName", "@LeadName"), new FieldValue("ForeignName", "@ForeignName"), new FieldValue("CompanyName", "@CompanyName"), new FieldValue("AreaID", "@AreaID"), new FieldValue("StageID", "@StageID"), new FieldValue("CountryID", "@CountryID"), new FieldValue("LeadSourceID", "@LeadSourceID"), new FieldValue("LeadOwnerID", "@LeadOwnerID"), new FieldValue("IndustryID", "@IndustryID"), new FieldValue("LeadStatus", "@LeadStatus"), new FieldValue("CompanySize", "@CompanySize"), new FieldValue("EmailOptOut", "@EmailOptOut"), new FieldValue("AnnualTurnOver", "@AnnualTurnOver"), new FieldValue("EmployeeCount", "@EmployeeCount"), new FieldValue("ReferredBy", "@ReferredBy"), new FieldValue("ShortName", "@ShortName"), new FieldValue("SubAreaID", "@SubAreaID"), new FieldValue("Rating", "@Rating"), new FieldValue("DateEstablished", "@DateEstablished"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("IsLeadSince", "@IsLeadSince"), new FieldValue("PrimaryAddressID", "@PrimaryAddressID"), new FieldValue("SalesPersonID", "@SalesPersonID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("ExpectValue", "@ExpectValue"), new FieldValue("ReasonID", "@ReasonID"), new FieldValue("Remarks", "@Remarks"), new FieldValue("ProfileDetails", "@ProfileDetails"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Lead", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@LeadID", SqlDbType.NVarChar);
			parameters.Add("@LeadName", SqlDbType.NVarChar);
			parameters.Add("@ForeignName", SqlDbType.NVarChar);
			parameters.Add("@CompanyName", SqlDbType.NVarChar);
			parameters.Add("@ShortName", SqlDbType.NVarChar);
			parameters.Add("@SubAreaID", SqlDbType.NVarChar);
			parameters.Add("@Rating", SqlDbType.TinyInt);
			parameters.Add("@DateEstablished", SqlDbType.DateTime);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@IsLeadSince", SqlDbType.DateTime);
			parameters.Add("@LeadSourceID", SqlDbType.NVarChar);
			parameters.Add("@LeadOwnerID", SqlDbType.NVarChar);
			parameters.Add("@IndustryID", SqlDbType.NVarChar);
			parameters.Add("@LeadStatus", SqlDbType.NVarChar);
			parameters.Add("@CompanySize", SqlDbType.TinyInt);
			parameters.Add("@EmailOptOut", SqlDbType.Bit);
			parameters.Add("@AnnualTurnOver", SqlDbType.Money);
			parameters.Add("@EmployeeCount", SqlDbType.Int);
			parameters.Add("@ReferredBy", SqlDbType.NVarChar);
			parameters.Add("@AreaID", SqlDbType.NVarChar);
			parameters.Add("@StageID", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@PrimaryAddressID", SqlDbType.NVarChar);
			parameters.Add("@SalesPersonID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@ProfileDetails", SqlDbType.NVarChar);
			parameters.Add("@ExpectValue", SqlDbType.Money);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@ReasonID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@LeadID"].SourceColumn = "LeadID";
			parameters["@LeadName"].SourceColumn = "LeadName";
			parameters["@ForeignName"].SourceColumn = "ForeignName";
			parameters["@CompanyName"].SourceColumn = "CompanyName";
			parameters["@AreaID"].SourceColumn = "AreaID";
			parameters["@StageID"].SourceColumn = "StageID";
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@ShortName"].SourceColumn = "ShortName";
			parameters["@SubAreaID"].SourceColumn = "SubAreaID";
			parameters["@Rating"].SourceColumn = "Rating";
			parameters["@DateEstablished"].SourceColumn = "DateEstablished";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@IsLeadSince"].SourceColumn = "IsLeadSince";
			parameters["@LeadSourceID"].SourceColumn = "LeadSourceID";
			parameters["@LeadOwnerID"].SourceColumn = "LeadOwnerID";
			parameters["@IndustryID"].SourceColumn = "IndustryID";
			parameters["@LeadStatus"].SourceColumn = "LeadStatus";
			parameters["@CompanySize"].SourceColumn = "CompanySize";
			parameters["@EmailOptOut"].SourceColumn = "EmailOptOut";
			parameters["@AnnualTurnOver"].SourceColumn = "AnnualTurnOver";
			parameters["@EmployeeCount"].SourceColumn = "EmployeeCount";
			parameters["@ReferredBy"].SourceColumn = "ReferredBy";
			parameters["@ProfileDetails"].SourceColumn = "ProfileDetails";
			parameters["@ExpectValue"].SourceColumn = "ExpectValue";
			parameters["@ReasonID"].SourceColumn = "ReasonID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@PrimaryAddressID"].SourceColumn = "PrimaryAddressID";
			parameters["@SalesPersonID"].SourceColumn = "SalesPersonID";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		private string GetInsertUpdateLeadContactText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Lead_Contact_Detail", new FieldValue("LeadID", "@LeadID", isUpdateConditionField: true), new FieldValue("ContactID", "@ContactID", isUpdateConditionField: true), new FieldValue("JobTitle", "@JobTitle"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateLeadContactCommand(bool isUpdate)
		{
			SqlCommand sqlCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				sqlCommand = new SqlCommand(GetInsertUpdateLeadContactText(isUpdate: true), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			else
			{
				sqlCommand = new SqlCommand(GetInsertUpdateLeadContactText(isUpdate: false), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			parameters.Add("@LeadID", SqlDbType.NVarChar);
			parameters.Add("@ContactID", SqlDbType.NVarChar);
			parameters.Add("@JobTitle", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@LeadID"].SourceColumn = "LeadID";
			parameters["@ContactID"].SourceColumn = "ContactID";
			parameters["@JobTitle"].SourceColumn = "JobTitle";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Note"].SourceColumn = "Note";
			return sqlCommand;
		}

		public bool InsertUpdateLead(LeadData accountLeadData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(accountLeadData, "Lead", insertUpdateCommand) : Update(accountLeadData, "Lead", insertUpdateCommand));
				string text = accountLeadData.LeadTable.Rows[0]["LeadID"].ToString();
				if (flag)
				{
					insertUpdateCommand = new LeadAddresses(base.DBConfig).GetInsertUpdateCommand(isUpdate);
					insertUpdateCommand.Transaction = sqlTransaction;
					if (isUpdate)
					{
						flag &= Update(accountLeadData, "Lead_Address", insertUpdateCommand);
					}
					else if (accountLeadData.Tables["Lead_Address"].Rows.Count > 0)
					{
						flag &= Insert(accountLeadData, "Lead_Address", insertUpdateCommand);
					}
				}
				if (flag)
				{
					insertUpdateCommand = GetInsertUpdateLeadContactCommand(isUpdate: false);
					if (isUpdate)
					{
						DeleteLeadContacts(sqlTransaction, text.ToString());
					}
					insertUpdateCommand.Transaction = sqlTransaction;
					if (accountLeadData.Tables["Lead_Contact_Detail"].Rows.Count > 0)
					{
						flag &= Insert(accountLeadData, "Lead_Contact_Detail", insertUpdateCommand);
					}
				}
				if (isUpdate)
				{
					AddActivityLog("Lead", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("Lead", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Lead", "LeadID", text, sqlTransaction, !isUpdate);
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Lead, text.ToString(), "Lead", "LeadID", sqlTransaction);
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

		private bool DeleteLeadContacts(SqlTransaction sqlTransaction, string leadID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Lead_Contact_Detail WHERE LeadID = '" + leadID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public LeadData GetLead()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Lead");
			LeadData leadData = new LeadData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(leadData, "Lead", sqlBuilder);
			return leadData;
		}

		public bool DeleteLead(string leadID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM UDF_Lead  WHERE EntityID = '" + leadID + "'";
				flag = Delete(commandText, trans);
				commandText = "DELETE FROM Lead_Address  WHERE LeadID = '" + leadID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Lead_Contact_Detail  WHERE LeadID = '" + leadID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Entity_Category_Detail  WHERE EntityID = '" + leadID + "' AND EntityType = " + 5;
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Lead WHERE LeadID = '" + leadID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Lead", leadID, ActivityTypes.Delete, null);
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

		public LeadData GetLeadByID(string id)
		{
			try
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				CommandHelper commandHelper = null;
				commandHelper = new CommandHelper();
				commandHelper.FieldName = "LeadID";
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.FieldValue = id;
				commandHelper.TableName = "Lead";
				sqlBuilder.AddCommandHelper(commandHelper);
				sqlBuilder.IsComparing = true;
				LeadData leadData = new LeadData();
				sqlBuilder.UseDistinct = false;
				FillDataSet(leadData, "Lead", sqlBuilder);
				if (leadData == null || leadData.Tables.Count == 0 || leadData.Tables[0].Rows.Count == 0)
				{
					return leadData;
				}
				string a = "";
				if (a == "")
				{
					a = "PRIMARY";
				}
				a = leadData.Tables["Lead"].Rows[0]["PrimaryAddressID"].ToString();
				string textCommand = "SELECT * FROM Lead_Address\r\n                            WHERE LeadID='" + id + "' AND AddressID='" + a + "'";
				FillDataSet(leadData, "Lead_Address", textCommand);
				textCommand = "SELECT     CD.LeadID, CD.ContactID, CD.JobTitle,CD.Note, C.FirstName,C.LastName,C.Country,C.City,C.Phone1,C.Phone2,C.Email1\r\n                        FROM Lead_Contact_Detail AS CD INNER JOIN Contact C ON C.ContactID = CD.ContactID \r\n                            WHERE LeadID='" + id + "'  ORDER BY RowIndex";
				FillDataSet(leadData, "Lead_Contact_Detail", textCommand);
				textCommand = "SELECT * FROM UDF_Lead WHERE EntityID = '" + id + "'";
				FillDataSet(leadData, "UDF", textCommand);
				return leadData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetLeadByFields(params string[] columns)
		{
			return GetLeadByFields(null, isInactive: true, columns);
		}

		public DataSet GetLeadByFields(string[] leadID, params string[] columns)
		{
			return GetLeadByFields(leadID, isInactive: true, columns);
		}

		public DataSet GetLeadByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Lead");
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
				commandHelper.FieldName = "LeadID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Lead";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Lead";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Lead", sqlBuilder);
			return dataSet;
		}

		public DataSet GetLeadList(bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT L.IsInactive AS [I],L.LeadID AS [Lead Code], L.LeadName AS Name, L.CompanyName AS Company, CA.City, CA.Phone1 AS Phone, CA.Mobile,L.LeadSourceID AS [LeadSource],L.LeadOwnerID AS [LeadOwner],L.IndustryID AS [Industry],L.SalesPersonID AS [SalesPerson],\r\n\t\t\t\t    \tLS.NAME AS [Status],L.CountryID AS [Country],L.AreaID AS [Area],GL.GenericListName [Stage],L.Rating AS [Rating],L.IsLeadSince AS [LeadSince],\r\n                        CA.Fax, CA.Email,(SELECT Top 1 CategoryID FROM Entity_Category_Detail WHERE Entity_Category_Detail.EntityID=L.LeadID) AS Category\r\n                        FROM  Lead L INNER JOIN Lead_Address AS CA ON L.LeadID = CA.LeadID \r\n\t\t\t\t\t  LEFT JOIN Lead_Status LS ON L.LeadStatus=LS.LeadStatusID AND CA.AddressID = 'PRIMARY' \r\n                      LEFT JOIN Generic_List GL ON GL.GenericListID=L.StageID";
			if (!showInactive)
			{
				text += " WHERE ISNULL(L.IsInactive,'False')='False'";
			}
			FillDataSet(dataSet, "Lead", text);
			return dataSet;
		}

		public DataSet GetLeadDocumentAddress(string leadID, string addressField)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT " + addressField + ",AddressPrintFormat\r\n                           FROM Lead INNER JOIN Lead_Address CA ON Lead.LeadID=CA.LeadID AND Lead." + addressField + "=CA.AddressID\r\n                           WHERE CA.LeadID='" + leadID + "'";
			FillDataSet(dataSet, "Lead", textCommand);
			return dataSet;
		}

		public DataSet GetLeadComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LeadID [Code],LeadName [Name],SalesPersonID\r\n                            FROM Lead\r\n                            WHERE ISINACTIVE<>1 ORDER BY LeadID,LeadName";
			FillDataSet(dataSet, "Lead", textCommand);
			return dataSet;
		}

		public DataSet GetLeadSourceComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LeadSourceID [Code],LeadSourceName [Name]\r\n                            FROM Lead_Source\r\n                            WHERE INACTIVE<>1 ORDER BY LeadSourceID,LeadSourceName";
			FillDataSet(dataSet, "Lead_Source", textCommand);
			return dataSet;
		}

		public DataSet GetLeadBalanceSummary(string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, bool isFC)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			if (isFC)
			{
				text = "FC";
			}
			string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
			baseCurrencyID = ((!isFC) ? ("'" + baseCurrencyID + "' AS CurrencyID,") : " Lead.CurrencyID,");
			string str = "SELECT DISTINCT ARJ.LeadID [Lead Code] ,LeadName AS [Lead Name] ,";
			str += baseCurrencyID;
			str = str + " ISNULL((SELECT SUM(ISNULL(Debit" + text + ",0)- ISNULL(Credit" + text + ",0)) ";
			if (!isFC)
			{
				str += " + ISNULL((SELECT SUM(ISNULL(RealizedGainLoss,0)) FROM AR_Payment_Allocation ARP WHERE ARP.LeadID=ARJ.LeadID),0) ";
			}
			str += " FROM ARJournal ARJ2 \r\n                             WHERE ARJ.LeadID=ARJ2.LeadID \r\n                             AND ISNULL(IsVoid,'False')='False'),0)\r\n                             AS [Net Balance]\r\n                             FROM ARJournal ARJ INNER JOIN Lead ON ARJ.LeadID=Lead.LeadID\r\n                              WHERE  ISNULL(IsVoid,'False')='False'  ";
			if (fromLead != "")
			{
				str = str + " AND ARJ.LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND ARJ.LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND ARJ.LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (!showZeroBalance)
			{
				str = str + "           and\r\n                             ISNULL((SELECT SUM(ISNULL(Debit" + text + ",0)- ISNULL(Credit" + text + ",0)) ";
			}
			if (!isFC)
			{
				str += " + ISNULL((SELECT SUM(ISNULL(RealizedGainLoss,0)) FROM AR_Payment_Allocation ARP WHERE ARP.LeadID=ARJ.LeadID),0) ";
			}
			str += " FROM ARJournal ARJ2 \r\n                             WHERE ARJ.LeadID=ARJ2.LeadID \r\n                             AND ISNULL(IsVoid,'False')='False'),0) <> 0 ";
			str += "ORDER BY ARJ.LeadID ";
			FillDataSet(dataSet, "Lead", str);
			return dataSet;
		}

		public DataSet GetLeadBalanceAmount(string leadCode)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LeadID [Lead Code],LeadName [Lead Name],ISNULL(Balance,0) + ISNULL(PDCAmount,0) \r\n                                + ISNULL((SELECT SUM(ISNULL(RealizedGainLoss,0)) FROM AR_Payment_Allocation ARP WHERE ARP.LeadID=Lead.LeadID),0)\r\n                                 AS [Balance Due],\r\n                                ISNULL(PDCAmount,0) AS [PDC Amount],ISNULL(Balance,0)+ ISNULL((SELECT SUM(ISNULL(RealizedGainLoss,0)) \r\n                                FROM AR_Payment_Allocation ARP WHERE ARP.LeadID=Lead.LeadID),0) AS [Net Balance] FROM Lead \r\n                                WHERE LeadID='" + leadCode + "'";
			FillDataSet(dataSet, "Lead", textCommand);
			return dataSet;
		}

		public DataSet GetLeadBalanceDetailFCReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, string currencyID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT DISTINCT ARJ.LeadID [Lead Code] ,LeadName AS [Lead Name] ,\r\n                        ISNULL((SELECT SUM(ISNULL(DebitFC,0)- ISNULL(CreditFC,0))\r\n                            FROM ARJournal ARJ2 \r\n                         WHERE ARJ.LeadID=ARJ2.LeadID AND ARJ2.ARDate<'" + text + "'  AND CurrencyID = '" + currencyID + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Opening Balance],\r\n                        ISNULL((SELECT SUM(ISNULL(DebitFC,0)- ISNULL(CreditFC,0))\r\n                        FROM ARJournal ARJ2 \r\n                         WHERE ARJ.LeadID=ARJ2.LeadID AND ARJ2.ARDate<='" + text2 + "'  AND CurrencyID = '" + currencyID + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Ending Balance]\r\n                        FROM ARJournal ARJ INNER JOIN Lead ON ARJ.LeadID=Lead.LeadID WHERE ";
			str = str + " ARDate < '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			str = str + " AND ARJ.CurrencyID='" + currencyID + "' ";
			if (fromLead != "")
			{
				str = str + " AND ARJ.LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " ORDER BY ARJ.LeadID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Lead", str);
			DataSet dataSet2 = new DataSet();
			str = "SELECT LeadID [Lead Code],SysDocType,'' AS [Doc Type], ARJ.SysDocID AS [Doc ID],VoucherID [Doc No],ARDate AS [Date],\r\n                            ChequeNumber AS [Chq#],ChequeDate AS [Chq Date],Description,Reference,CurrencyID,CurrencyRate,DebitFC AS Debit,CreditFC AS Credit\r\n                            FROM ARJournal ARJ LEFT OUTER JOIN System_Document SD ON ARJ.SysDocID=SD.SysDocID WHERE \r\n                            ARDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str = str + "  AND CurrencyID = '" + currencyID + "'  AND ISNULL(IsVoid,'False')='False' ";
			if (fromLead != "")
			{
				str = str + " AND LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " ORDER BY Date";
			FillDataSet(dataSet2, "ARJournal", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Balance Detail", dataSet.Tables["Lead"].Columns["Lead Code"], dataSet.Tables["ARJournal"].Columns["Lead Code"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetLeadBalanceDetailReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, string currencyID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "";
			if (currencyID != new Currencies(base.DBConfig).GetBaseCurrencyID())
			{
				text3 = "FC";
			}
			string str = "SELECT DISTINCT ARJ.LeadID [Lead Code] ,LeadName AS [Lead Name] ,\r\n                        ISNULL((SELECT SUM(ISNULL(Debit" + text3 + ",0)- ISNULL(Credit" + text3 + ",0))\r\n                         FROM ARJournal ARJ2 \r\n                         WHERE ARJ.LeadID=ARJ2.LeadID AND ARJ2.ARDate<'" + text + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Opening Balance],\r\n                        ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0))\r\n\r\n                        FROM ARJournal ARJ2 \r\n                         WHERE ARJ.LeadID=ARJ2.LeadID AND ARJ2.ARDate<='" + text2 + "' AND ISNULL(IsVoid,'False')='False'),0)\r\n                        AS [Ending Balance]\r\n                        FROM ARJournal ARJ INNER JOIN Lead ON ARJ.LeadID=Lead.LeadID WHERE ";
			str = str + " ARDate < '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLead != "")
			{
				str = str + " AND ARJ.LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " ORDER BY ARJ.LeadID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Lead", str);
			DataSet dataSet2 = new DataSet();
			str = "SELECT LeadID [Lead Code],SysDocType,'' AS [Doc Type], ARJ.SysDocID AS [Doc ID],VoucherID [Doc No],ARDate AS [Date],\r\n                            ChequeNumber AS [Chq#],ChequeDate AS [Chq Date],Description,Reference,CurrencyID,CurrencyRate,DebitFC,CreditFC, \r\n                            Debit, Credit \r\n                            FROM ARJournal ARJ LEFT OUTER JOIN System_Document SD ON ARJ.SysDocID=SD.SysDocID WHERE \r\n                            ARDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLead != "")
			{
				str = str + " AND LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " ORDER BY Date";
			FillDataSet(dataSet2, "ARJournal", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Balance Detail", dataSet.Tables["Lead"].Columns["Lead Code"], dataSet.Tables["ARJournal"].Columns["Lead Code"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetSalesByLeadDetailReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "Select DISTINCT SI.LeadID,LeadName\r\n                            FROM Sales_Invoice SI INNER JOIN Lead ON SI.LeadID=Lead.LeadID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLead != "")
			{
				text3 = text3 + " AND SI.LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " UNION ";
			text3 += "Select DISTINCT SI.LeadID,LeadName\r\n                            FROM Sales_Return SI INNER JOIN Lead ON SI.LeadID=Lead.LeadID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLead != "")
			{
				text3 = text3 + " AND SI.LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " ORDER BY LeadID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Lead", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select SysDocID,VoucherID,LeadID,TransactionDate,Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n                    SalespersonID,CurrencyID,CurrencyRate,Discount,DiscountFC,\r\n                    Total,TotalFC \r\n\r\n                    FROM Sales_Invoice SI WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLead != "")
			{
				text3 = text3 + " AND LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " UNION ";
			text3 = text3 + "Select SysDocID,VoucherID,LeadID,TransactionDate,Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type],\r\n                    SalespersonID,CurrencyID,CurrencyRate,-1*Discount,-1*DiscountFC,\r\n                    -1*Total,-1*TotalFC \r\n\r\n                    FROM Sales_Return SI WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLead != "")
			{
				text3 = text3 + " AND LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " ORDER BY TransactionDate";
			FillDataSet(dataSet2, "Sales", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Sales Detail", dataSet.Tables["Lead"].Columns["LeadID"], dataSet.Tables["Sales"].Columns["LeadID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetSalesByLeadSummaryReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "Select SI.LeadID, CU.LeadName,\r\n                            SUM(Discount) AS Discount,\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN Total ELSE 0 END) AS [CashSale],\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN Total ELSE 0 END) AS [CreditSale] \r\n                            FROM Sales_Invoice SI INNER JOIN Lead CU ON SI.LeadID=CU.LeadID\r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLead != "")
			{
				str = str + " AND SI.LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND SI.LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND SI.LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY SI.LeadID,CU.LeadName";
			FillDataSet(dataSet, "Sales", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["LeadID"]
			};
			str = "Select SI.LeadID, CU.LeadName,\r\n                    -1*SUM(Discount) AS DiscountReturn,\r\n                    SUM(Total) AS [SalesReturn]\r\n                    FROM Sales_Return SI INNER JOIN Lead CU ON SI.LeadID=CU.LeadID \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLead != "")
			{
				str = str + " AND SI.LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND SI.LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND SI.LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY SI.LeadID,CU.LeadName";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Sales", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["LeadID"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetSalesByLeadGroupDetailReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "Select DISTINCT ISNULL(CG.GroupID,'No Group') AS GroupID,CG.GroupName\r\n                            FROM Sales_Invoice SI INNER JOIN Lead ON SI.LeadID=Lead.LeadID\r\n                            LEFT OUTER JOIN Lead_Group CG ON CG.GroupID=Lead.LeadGroupID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " UNION ";
			text3 += "Select DISTINCT ISNULL(CG.GroupID,'No Group') AS GroupID,CG.GroupName\r\n                            FROM Sales_Return SI INNER JOIN Lead ON SI.LeadID=Lead.LeadID\r\n                            LEFT OUTER JOIN Lead_Group CG ON CG.GroupID=Lead.LeadGroupID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				text3 = text3 + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " ORDER BY GroupID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Lead", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select ISNULL(CG.GroupID,'No Group') AS GroupID,SysDocID,VoucherID,SI.LeadID + '-' + Lead.LeadName AS Lead,TransactionDate,SI.Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n                    SI.SalespersonID,CurrencyID,CurrencyRate,Discount,DiscountFC,\r\n                    Total,TotalFC \r\n\r\n                    FROM Sales_Invoice SI INNER JOIN Lead ON Lead.LeadID=SI.LeadID\r\n                    LEFT OUTER JOIN Lead_Group CG ON CG.GroupID=Lead.LeadGroupID \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				text3 = text3 + " AND SI.LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " UNION ";
			text3 = text3 + "Select ISNULL(CG.GroupID,'No Group') AS GroupID,SysDocID,VoucherID,SI.LeadID + '-' + Lead.LeadName AS Lead,TransactionDate,SI.Note,\r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type],\r\n                    SI.SalespersonID,CurrencyID,CurrencyRate,-1*Discount,-1*DiscountFC,\r\n                    -1*Total,-1*TotalFC \r\n\r\n                    FROM Sales_Return SI INNER JOIN Lead ON Lead.LeadID=SI.LeadID\r\n                    LEFT OUTER JOIN Lead_Group CG ON CG.GroupID=Lead.LeadGroupID  \r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				text3 = text3 + " AND SI.LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			text3 += " ORDER BY TransactionDate";
			FillDataSet(dataSet2, "Sales", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Sales Detail", dataSet.Tables["Lead"].Columns["GroupID"], dataSet.Tables["Sales"].Columns["GroupID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetSalesByLeadGroupSummaryReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "Select ISNULL(CG.GroupID,'No Group') AS GroupID, CG.GroupName,\r\n                            SUM(Discount) AS Discount,\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN Total ELSE 0 END) AS [CashSale],\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN Total ELSE 0 END) AS [CreditSale] \r\n                            FROM Sales_Invoice SI INNER JOIN Lead CU ON SI.LeadID=CU.LeadID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Lead_Group CG ON CU.LeadGroupID=CG.GroupID\r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY CG.GroupID, CG.GroupName";
			FillDataSet(dataSet, "Sales", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["LeadID"]
			};
			str = "Select ISNULL(CG.GroupID,'No Group') AS GroupID, CG.GroupName,\r\n                            -1*SUM(Discount) AS DiscountReturn,\r\n                            SUM(Total) AS [SalesReturn]\r\n                            FROM Sales_Return SI INNER JOIN Lead CU ON SI.LeadID=CU.LeadID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Lead_Group CG ON CU.LeadGroupID=CG.GroupID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromGroup != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			str += " GROUP BY CG.GroupID, CG.GroupName";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Sales", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["GroupID"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetLeadListReport(string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  LeadID,LeadName,CompanyName,LeadClassID, AreaID,\r\n                                ContactName,CountryID,LeadGroupID \r\n                                FROM Lead \r\n                                WHERE 1=1 ";
			if (fromLead != "")
			{
				text = text + " AND LeadID>='" + fromLead + "'";
			}
			if (toLead != "")
			{
				text = text + " AND LeadID<='" + toLead + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND LeadClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND LeadClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text = text + " AND LeadGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text = text + " AND LeadGroupID<='" + toGroup + "'";
			}
			if (fromArea != "")
			{
				text = text + " AND AreaID>='" + fromArea + "'";
			}
			if (toArea != "")
			{
				text = text + " AND AreaID<='" + toArea + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Leads", text);
			return dataSet;
		}

		public DataSet GetLeadPrimaryContactListReport(string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  Lead.LeadID,LeadName,CompanyName,\r\n                            CA.ContactName,AddressPrintFormat,City,Country,Phone1,Mobile,Fax,Email\r\n                            FROM Lead LEFT OUTER JOIN Lead_Address CA\r\n                            ON Lead.LeadID=CA.LeadID\r\n                                WHERE CA.AddressID=Lead.PrimaryAddressID ";
			if (fromLead != "")
			{
				text = text + " AND Lead.LeadID>='" + fromLead + "'";
			}
			if (toLead != "")
			{
				text = text + " AND Lead.LeadID<='" + toLead + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND LeadClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND LeadClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text = text + " AND LeadGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text = text + " AND LeadGroupID<='" + toGroup + "'";
			}
			if (fromArea != "")
			{
				text = text + " AND AreaID>='" + fromArea + "'";
			}
			if (toArea != "")
			{
				text = text + " AND AreaID<='" + toArea + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Leads", text);
			return dataSet;
		}

		public DataSet GetLeadProfileReport(string fromLead, string toLead, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  Lead.LeadID, LeadName,ForeignName,CompanyName,Lead.ContactName,\r\n                            AreaName,CA.Phone1,CA.Mobile,CA.Fax,CA.Email,CA.Website, Lead.CountryID,Lead.IsInactive,\r\n                            IsLeadSince, Lead.Note,Lead.SalesPersonID + '-' + Salesperson.FullName AS Salesperson\r\n                            FROM Lead\r\n                            LEFT OUTER JOIN Area Area ON Lead.AreaID=Area.AreaID\r\n                            LEFT OUTER JOIN Salesperson Salesperson ON Lead.SalespersonID=Salesperson.SalespersonID\r\n                            LEFT OUTER JOIN Lead_Address CA ON Lead.LeadID=CA.LeadID AND Lead.PrimaryAddressID=CA.AddressID \r\n                            WHERE 1=1 ";
			if (fromLead != "")
			{
				text = text + " AND Lead.LeadID>='" + fromLead + "'";
			}
			if (toLead != "")
			{
				text = text + " AND Lead.LeadID <='" + toLead + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Lead.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Leads", text);
			return dataSet;
		}

		public DataSet GetLeadBySourceReport(string fromSource, string toSource, string fromArea, string toArea, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  Lead.LeadID, LeadName,ForeignName,CompanyName,Lead.ContactName,\r\n                            AreaName,CA.Phone1,CA.Mobile,CA.Fax,CA.Email,CA.Website, Lead.CountryID,Lead.IsInactive,\r\n                            IsLeadSince, Lead.Note,Lead.SalesPersonID + '-' + Salesperson.FullName AS Salesperson\r\n                            FROM Lead\r\n                            LEFT OUTER JOIN Area Area ON Lead.AreaID=Area.AreaID\r\n                            LEFT OUTER JOIN Salesperson Salesperson ON Lead.SalespersonID=Salesperson.SalespersonID\r\n                            LEFT OUTER JOIN Lead_Address CA ON Lead.LeadID=CA.LeadID AND Lead.PrimaryAddressID=CA.AddressID \r\n                            WHERE 1=1 ";
			if (fromSource != "")
			{
				text = text + " AND Lead.LeadSourceID>='" + fromSource + "'";
			}
			if (toSource != "")
			{
				text = text + " AND Lead.LeadSourceID<='" + toSource + "'";
			}
			if (fromArea != "")
			{
				text = text + " AND Lead.AreaID>='" + fromArea + "'";
			}
			if (toArea != "")
			{
				text = text + " AND Lead.AreaID<='" + toArea + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Lead.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Leads", text);
			return dataSet;
		}

		public DataSet GetLeadActivityReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT LeadID,LeadName FROM Lead WHERE ISNULL(IsInactive,'False')='False' ";
			if (fromLead != "")
			{
				str = str + " AND LeadID >= '" + fromLead + "'";
			}
			if (toLead != "")
			{
				str = str + " AND LeadID <= '" + toLead + "'";
			}
			if (fromClass != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID <= '" + toGroup + "') ";
			}
			str += " ORDER BY LeadID ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Lead", str);
			DataSet dataSet2 = new DataSet();
			string text3 = "";
			if (fromLead != "")
			{
				text3 = " AND LeadID BETWEEN '" + fromLead + "' AND '" + toLead + "' ";
			}
			if (fromClass != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				str = str + " AND LeadID IN (SELECT LeadID FROM Lead WHERE LeadGroupID <= '" + toGroup + "') ";
			}
			str = "SELECT Sales_Invoice.SysDocID,VoucherID,LeadID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Sales_Invoice INNER JOIN System_Document SD ON Sales_Invoice.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' " + text3 + "\r\n\r\n                        UNION \r\n\r\n                        SELECT Sales_Return.SysDocID,VoucherID,LeadID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Sales_Return INNER JOIN System_Document SD ON Sales_Return.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n                        UNION \r\n\r\n                        SELECT Sales_Quote.SysDocID,VoucherID,LeadID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Sales_Quote INNER JOIN System_Document SD ON Sales_Quote.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n                        UNION \r\n\r\n                        SELECT Sales_Order.SysDocID,VoucherID,LeadID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Sales_Order INNER JOIN System_Document SD ON Sales_Order.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n                        UNION \r\n\r\n                        SELECT Sales_Order.SysDocID,VoucherID,LeadID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        Total AS Amount \r\n                        FROM Sales_Order INNER JOIN System_Document SD ON Sales_Order.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n\r\n                        UNION \r\n\r\n                        SELECT Delivery_Note.SysDocID,VoucherID,LeadID,TransactionDate,\r\n                        SysDocType,Note,\r\n                        NULL AS Amount \r\n                        FROM Delivery_Note INNER JOIN System_Document SD ON Delivery_Note.SysDocID=SD.SysDocID\r\n                        WHERE  ISNULL(IsVoid,'False')='False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  " + text3 + "\r\n\r\n                        UNION \r\n\r\n\r\n                        SELECT GL_Transaction.SysDocID,VoucherID,PayeeID AS LeadID,TransactionDate,\r\n                        SysDocType,Description AS [Note],\r\n                        NULL AS Amount \r\n                        FROM GL_Transaction INNER JOIN System_Document SD ON GL_Transaction.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid,'False')='False' AND PayeeType='C' AND\r\n                        TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n\r\n                        ORDER BY TransactionDate";
			FillDataSet(dataSet2, "LeadActivity", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Lead_Activity", dataSet.Tables["Lead"].Columns["LeadID"], dataSet.Tables["LeadActivity"].Columns["LeadID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetTopLeads(DateTime from, DateTime to, int count)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string textCommand = "SELECT TOP " + count.ToString() + " LeadName Name,SUM(Total-Discount) AS Sales \r\n                                    FROM Sales_Invoice SI\r\n                                    INNER JOIN Lead CUS ON CUS.LeadID=SI.LeadID\r\n                                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                                    GROUP BY LeadName \r\n                                    HAVING SUM(Total-Discount)>0\r\n                                    ORDER BY Sales DESC";
			FillDataSet(dataSet, "Lead", textCommand);
			return dataSet;
		}

		public DataSet GetTopSalesperson(DateTime from, DateTime to, int count)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string textCommand = "SELECT TOP " + count.ToString() + " ISNULL(SI.SalespersonID,'None') AS Salesperson,SUM(Total-Discount) AS Sales \r\n                                    FROM Sales_Invoice SI \r\n                                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                                    GROUP BY SI.SalespersonID\r\n                                    HAVING SUM(Total-Discount)>0\r\n                                    ORDER BY Sales DESC";
			FillDataSet(dataSet, "Lead", textCommand);
			return dataSet;
		}

		public DataSet GetMonthlySalesReport(DateTime from, DateTime to)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				base.DBConfig.StartNewTransaction();
				string textCommand = " SELECT * INTO Temp_Sale FROM\r\n                            (\r\n                            SELECT DatePart(Month,TransactionDate) AS M,SUM(Total) Total \r\n                            FROM Sales_Invoice\r\n                            WHERE DATEPART(Year,TransactionDate) = DATEPART(Year,GetDate())  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(IsVoid,'False')='False'\r\n                            Group BY DatePart(Month,TransactionDate)\r\n\r\n\r\n                            UNION \r\n\r\n                            SELECT   DatePart(Month,TransactionDate) AS M,SUM(Total) Total\r\n\r\n                            FROM Sales_POS\r\n                            WHERE DATEPART(Year,TransactionDate) = DATEPART(Year,GetDate())  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(IsVoid,'False')='False'\r\n                            Group BY DatePart(Month,TransactionDate)\r\n \r\n                             UNION \r\n\r\n                             SELECT   DatePart(Month,TransactionDate) AS M, SUM(-1 * Total) Total\r\n\r\n                            FROM Sales_Return\r\n                            WHERE DATEPART(Year,TransactionDate) = DATEPART(Year,GetDate())  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(IsVoid,'False')='False'\r\n                            Group BY DatePart(Month,TransactionDate)\r\n \r\n                             UNION \r\n\r\n                            SELECT DatePart(Month,TransactionDate) AS M,SUM(Total) Total\r\n                            FROM Sales_Receipt\r\n                            WHERE DATEPART(Year,TransactionDate) = DATEPART(Year,GetDate())  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(IsVoid,'False')='False'\r\n                            Group BY DatePart(Month,TransactionDate)\r\n                            ) as TMP\r\n\r\n\r\n\r\n                            SELECT M,'Name' AS [Month], SUM(Total) AS Sale FROM Temp_Sale\r\n                            GROUP BY M\r\n                            ORDER BY M\r\n\r\n                            DROP Table Temp_Sale";
				FillDataSet(dataSet, "Sales", textCommand);
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					DataHelper dataHelper = new DataHelper(base.DBConfig);
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						row["Month"] = dataHelper.GetMonthNameAbr(int.Parse(row["M"].ToString()));
					}
					dataSet.Tables[0].Columns.Remove("M");
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result: false);
			}
		}

		public bool SetFlag(string leadID, byte flagID)
		{
			try
			{
				string exp = (flagID <= 0) ? ("UPDATE Lead SET Flag = NULL WHERE LeadID = '" + leadID + "'") : ("UPDATE Lead SET Flag = " + flagID + " WHERE LeadID = '" + leadID + "'");
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}
	}
}
