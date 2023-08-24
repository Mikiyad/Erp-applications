using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Property : StoreObject
	{
		private const string PROPERTYID_PARM = "@PropertyID";

		private const string PROPERTYNAME_PARM = "@PropertyName";

		private const string SHORTNAME_PARM = "@ShortName";

		private const string OFFERTYPEID_PARM = "@OfferTypeID";

		private const string FIXEDASSETID_PARM = "@FixedAssetID";

		private const string PROPERTYCLASSID_PARM = "@PropertyClassID";

		private const string COUNTRYID_PARM = "@CountryID";

		private const string CITYID_PARM = "@CityID";

		private const string AREAID_PARM = "@AreaID";

		private const string ADDRESS1_PARM = "@Address1";

		private const string ADDRESS2_PARM = "@Address2";

		private const string YEARBUILT_PARM = "@YearBuilt";

		private const string BUILTBY_PARM = "@Builtby";

		private const string LANDAREA_PARM = "@LandArea";

		private const string BUILDAREA_PARM = "@BuildArea";

		private const string OWNERNAME_PARM = "@OwnerName";

		private const string REGISTERNUMBER_PARM = "@RegisterNumber";

		private const string RENTINCOMEACCOUNTID_PARM = "@RentIncomeAccountID";

		private const string PREPAIDRENTINCOMEACCOUNTID_PARM = "@PrepaidRentIncomeAccountID";

		private const string NOTE_PARM = "@Note";

		private const string STATUS_PARM = "@Status";

		private const string BASEDONPERIODICINVOICE_PARM = "@BasedOnPeriodicInvoice";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string ELECTRICITYREGNNUMBER_PARM = "@ElectricityRegnNumber";

		private const string TELECOMREGNNUMBER_PARM = "@TelecomRegnNumber";

		private const string MUNICIPALITYREGNNUMBER_PARM = "@MunicipalityRegnNumber";

		private const string ELECTRICITYPREMISENUMBER_PARM = "@ElectricityPremiseNumber";

		private const string ELECTRICITYCONTRACTNUMBER_PARM = "@ElectricityContractNumber";

		public const string TAXOPTION_PARM = "@TaxOption";

		public const string TAXGROUPID_PARM = "@TaxGroupID";

		public const string TAXIDNUMBER_PARM = "@TaxIDNumber";

		private const string FACILITYID_PARM = "@FacilityID";

		private const string TYPE_PARM = "@Type";

		private const string PROPERTYOWNERID_PARM = "@PropertyOwnerID";

		private const string OWNERSHIPPERCENT_PARM = "@OwnerShipPercent";

		private const string DESCRIPTION_PARM = "@Description";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Property(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property", new FieldValue("PropertyID", "@PropertyID", isUpdateConditionField: true), new FieldValue("PropertyName", "@PropertyName"), new FieldValue("ShortName", "@ShortName"), new FieldValue("OfferTypeID", "@OfferTypeID"), new FieldValue("FixedAssetID", "@FixedAssetID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxIDNumber", "@TaxIDNumber"), new FieldValue("ElectricityRegnNumber", "@ElectricityRegnNumber"), new FieldValue("TelecomRegnNumber", "@TelecomRegnNumber"), new FieldValue("MunicipalityRegnNumber", "@MunicipalityRegnNumber"), new FieldValue("ElectricityContractNumber", "@ElectricityContractNumber"), new FieldValue("ElectricityPremiseNumber", "@ElectricityPremiseNumber"), new FieldValue("PropertyClassID", "@PropertyClassID"), new FieldValue("CountryID", "@CountryID"), new FieldValue("CityID", "@CityID"), new FieldValue("AreaID", "@AreaID"), new FieldValue("Address1", "@Address1"), new FieldValue("Address2", "@Address2"), new FieldValue("YearBuilt", "@YearBuilt"), new FieldValue("Builtby", "@Builtby"), new FieldValue("LandArea", "@LandArea"), new FieldValue("BuildArea", "@BuildArea"), new FieldValue("OwnerName", "@OwnerName"), new FieldValue("RegisterNumber", "@RegisterNumber"), new FieldValue("RentIncomeAccountID", "@RentIncomeAccountID"), new FieldValue("PrepaidRentIncomeAccountID", "@PrepaidRentIncomeAccountID"), new FieldValue("Note", "@Note"), new FieldValue("LocationID", "@LocationID"), new FieldValue("IsPeriodicInvoice", "@BasedOnPeriodicInvoice"), new FieldValue("Status", "@Status"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PropertyID", SqlDbType.NVarChar);
			parameters.Add("@PropertyName", SqlDbType.NVarChar);
			parameters.Add("@ShortName", SqlDbType.NVarChar);
			parameters.Add("@OfferTypeID", SqlDbType.NVarChar);
			parameters.Add("@FixedAssetID", SqlDbType.NVarChar);
			parameters.Add("@PropertyClassID", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@CityID", SqlDbType.NVarChar);
			parameters.Add("@AreaID", SqlDbType.NVarChar);
			parameters.Add("@Address1", SqlDbType.NVarChar);
			parameters.Add("@Address2", SqlDbType.NVarChar);
			parameters.Add("@YearBuilt", SqlDbType.NVarChar);
			parameters.Add("@Builtby", SqlDbType.NVarChar);
			parameters.Add("@LandArea", SqlDbType.Decimal);
			parameters.Add("@BuildArea", SqlDbType.Decimal);
			parameters.Add("@OwnerName", SqlDbType.NVarChar);
			parameters.Add("@RegisterNumber", SqlDbType.NVarChar);
			parameters.Add("@RentIncomeAccountID", SqlDbType.NVarChar);
			parameters.Add("@PrepaidRentIncomeAccountID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.Bit);
			parameters.Add("@ElectricityRegnNumber", SqlDbType.NVarChar);
			parameters.Add("@TelecomRegnNumber", SqlDbType.NVarChar);
			parameters.Add("@MunicipalityRegnNumber", SqlDbType.NVarChar);
			parameters.Add("@ElectricityPremiseNumber", SqlDbType.NVarChar);
			parameters.Add("@ElectricityContractNumber", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxIDNumber", SqlDbType.NVarChar);
			parameters.Add("@BasedOnPeriodicInvoice", SqlDbType.Bit);
			parameters["@PropertyID"].SourceColumn = "PropertyID";
			parameters["@PropertyName"].SourceColumn = "PropertyName";
			parameters["@ShortName"].SourceColumn = "ShortName";
			parameters["@OfferTypeID"].SourceColumn = "OfferTypeID";
			parameters["@FixedAssetID"].SourceColumn = "FixedAssetID";
			parameters["@PropertyClassID"].SourceColumn = "PropertyClassID";
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@CityID"].SourceColumn = "CityID";
			parameters["@AreaID"].SourceColumn = "AreaID";
			parameters["@Address1"].SourceColumn = "Address1";
			parameters["@Address2"].SourceColumn = "Address2";
			parameters["@YearBuilt"].SourceColumn = "YearBuilt";
			parameters["@Builtby"].SourceColumn = "Builtby";
			parameters["@LandArea"].SourceColumn = "LandArea";
			parameters["@BuildArea"].SourceColumn = "BuildArea";
			parameters["@OwnerName"].SourceColumn = "OwnerName";
			parameters["@ElectricityRegnNumber"].SourceColumn = "ElectricityRegnNumber";
			parameters["@TelecomRegnNumber"].SourceColumn = "TelecomRegnNumber";
			parameters["@MunicipalityRegnNumber"].SourceColumn = "MunicipalityRegnNumber";
			parameters["@ElectricityContractNumber"].SourceColumn = "ElectricityContractNumber";
			parameters["@ElectricityPremiseNumber"].SourceColumn = "ElectricityPremiseNumber";
			parameters["@RegisterNumber"].SourceColumn = "RegisterNumber";
			parameters["@RentIncomeAccountID"].SourceColumn = "RentIncomeAccountID";
			parameters["@PrepaidRentIncomeAccountID"].SourceColumn = "PrepaidRentIncomeAccountID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxIDNumber"].SourceColumn = "TaxIDNumber";
			parameters["@BasedOnPeriodicInvoice"].SourceColumn = "IsPeriodicInvoice";
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

		private string GetInsertUpdateFacilityText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Facility", new FieldValue("FacilityID", "@FacilityID"), new FieldValue("PropertyID", "@PropertyID"), new FieldValue("Type", "@Type"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFacilityCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFacilityText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFacilityText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@FacilityID", SqlDbType.NVarChar);
			parameters.Add("@PropertyID", SqlDbType.NVarChar);
			parameters.Add("@Type", SqlDbType.Int);
			parameters["@FacilityID"].SourceColumn = "FacilityID";
			parameters["@PropertyID"].SourceColumn = "PropertyID";
			parameters["@Type"].SourceColumn = "Type";
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

		private string GetInsertUpdateOwnerDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Owner_Detail", new FieldValue("PropertyID", "@PropertyID"), new FieldValue("PropertyOwnerID", "@PropertyOwnerID"), new FieldValue("Description", "@Description"), new FieldValue("OwnerShipPercent", "@OwnerShipPercent"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Tax_Group_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateOwnerCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateOwnerDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateOwnerDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@PropertyID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@PropertyOwnerID", SqlDbType.NVarChar);
			parameters.Add("@OwnerShipPercent", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@PropertyID"].SourceColumn = "PropertyID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@PropertyOwnerID"].SourceColumn = "PropertyOwnerID";
			parameters["@OwnerShipPercent"].SourceColumn = "OwnerShipPercent";
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

		public bool InsertUpdateProperty(PropertyData accountPropertyData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(accountPropertyData, "Property", insertUpdateCommand) : Update(accountPropertyData, "Property", insertUpdateCommand));
				string text = accountPropertyData.PropertyTable.Rows[0]["PropertyID"].ToString();
				if (accountPropertyData.Tables.Contains("Property_Facility") && accountPropertyData.Tables["Property_Facility"].Rows.Count > 0)
				{
					if (isUpdate)
					{
						DeletePopertFacilityDetailsRows(text, sqlTransaction);
					}
					if (accountPropertyData.Tables["Property_Facility"].Rows.Count > 0)
					{
						insertUpdateCommand = GetInsertUpdateFacilityCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Insert(accountPropertyData, "Property_Facility", insertUpdateCommand);
					}
				}
				if (isUpdate)
				{
					DeletePopertOwnerDetailsRows(text, sqlTransaction);
				}
				if (accountPropertyData.Tables["Property_Owner_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateOwnerCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPropertyData, "Property_Owner_Detail", insertUpdateCommand);
				}
				if (isUpdate)
				{
					AddActivityLog("Property", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("Property", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Property", "PropertyID", text, sqlTransaction, !isUpdate);
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

		internal bool DeletePopertFacilityDetailsRows(string PropertyId, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Property_Facility  WHERE PropertyID='" + PropertyId + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePopertOwnerDetailsRows(string PropertyId, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Property_Owner_Detail  WHERE PropertyID='" + PropertyId + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public PropertyData GetProperty()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property");
			PropertyData propertyData = new PropertyData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyData, "Property", sqlBuilder);
			return propertyData;
		}

		public bool DeleteProperty(string ID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Property WHERE PropertyID = '" + ID + "'";
				flag &= Delete(commandText, sqlTransaction);
				if (flag)
				{
					DeletePopertFacilityDetailsRows(ID, sqlTransaction);
				}
				if (flag)
				{
					DeletePopertOwnerDetailsRows(ID, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Property", ID, ActivityTypes.Delete, null);
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

		public PropertyData GetPropertyByID(string id)
		{
			new SqlBuilder();
			PropertyData propertyData = new PropertyData();
			string textCommand = "SELECT *, \r\n\t\t\t\t\t\t\t(SELECT Count(P2.PropertyID)\r\n\t\t\t\t\t\t\tFROM Property   P2 WHERE P2.Photo IS Not NULL AND P2.PropertyID=P1.PropertyID)  AS HasPhoto  from Property P1 where PropertyID='" + id + "'";
			FillDataSet(propertyData, "Property", textCommand);
			textCommand = " SELECT *, P.PropertyName FROM Property_Unit PU INNER JOIN Property P ON PU.PropertyID=P.PropertyID  WHERE P.PropertyID = '" + id + "'";
			FillDataSet(propertyData, "Property_Unit", textCommand);
			textCommand = "SELECT * FROM UDF_Property WHERE EntityID = '" + id + "'";
			FillDataSet(propertyData, "UDF", textCommand);
			textCommand = " SELECT T.FacilityID,T.FacilityName,T.Type FROM (SELECT P.FacilityID AS 'FacilityID',G.GenericListName as 'FacilityName',Type FROM Property_Facility P inner join Generic_List G ON P.FacilityID=G.GenericListID WHERE PropertyID ='" + id + "'\r\n                    union all select GenericListID AS 'FacilityID',GenericListName as 'FacilityName','0' as Type from Generic_List WHERE GenericListType ='12' \r\n                    AND GenericListID NOT IN (SELECT DISTINCT  FacilityID FROM Property_Facility WHERE PropertyID='" + id + "')) AS T ORDER BY T.FacilityID";
			FillDataSet(propertyData, "Property_Facility", textCommand);
			textCommand = " SELECT PU.*, GL.GenericListName as OwnerName FROM Property_Owner_Detail PU INNER JOIN Property P ON PU.PropertyID=P.PropertyID LEFT JOIN Generic_List GL ON GL.GenericListID=PU.PropertyOwnerID AND  GL.GenericListType=38 WHERE P.PropertyID = '" + id + "'";
			FillDataSet(propertyData, "Property_Owner_Detail", textCommand);
			return propertyData;
		}

		public DataSet GetPropertyByFields(params string[] columns)
		{
			return GetPropertyByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyByFields(string[] PropertyID, params string[] columns)
		{
			return GetPropertyByFields(PropertyID, isInactive: true, columns);
		}

		public DataSet GetPropertyByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property");
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
				commandHelper.FieldName = "PropertyID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Property";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "Status";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Property";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Property", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPropertyList(bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "select P.PropertyID as [Property Code],P.PropertyName AS Name  from  Property P\r\n                              LEFT OUTER JOIN Country C ON C.CountryID = P.CountryID\r\n                        LEFT OUTER JOIN Area ON Area.AreaID = P.AreaID                        \r\n                        ";
			if (!showInactive)
			{
				text += " WHERE ISNULL(P.Status,'False')='False'";
			}
			FillDataSet(dataSet, "Property", text);
			return dataSet;
		}

		public DataSet GetPropertyComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyID [Code],PropertyName [Name], IsPeriodicInvoice  FROM Property\r\n                            WHERE STATUS<>1 ORDER BY PropertyID,PropertyName";
			FillDataSet(dataSet, "Property", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyRentDetailsReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromUnit, string toUnit, string fromProperty, string toProperty, string Type, string customerIDs, string fromAgent, string toAgent)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT DISTINCT PR.CustomerID,Cus.CustomerName FROM Property_Rent PR\r\n                                INNER JOIN Customer CUS ON Cus.CustomerID = PR.CustomerID ";
				text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (Type != "")
				{
					text3 = text3 + " AND PR.AgreementStatus IN ('" + Type + "')";
				}
				if (fromProperty != "")
				{
					text3 = text3 + " AND PR.PropertyID BETWEEN '" + fromProperty + "' AND '" + toProperty + "' ";
				}
				if (customerIDs != "")
				{
					text3 = text3 + " AND PR.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND PR.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "') ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND CustomerClassID BETWEEN '" + fromClass + "' AND '" + toClass + "' ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "' ";
				}
				if (fromUnit != "")
				{
					text3 = text3 + " AND PR.UnitID IN (SELECT UnitID FROM Property_Unit WHERE UnitID BETWEEN '" + fromUnit + "' AND '" + toUnit + "') ";
				}
				if (fromAgent != "")
				{
					text3 = text3 + " AND PR.PropertyAgentID IN (SELECT PropertyAgentID FROM Property_Agent WHERE PropertyAgentID BETWEEN '" + fromAgent + "' AND '" + toAgent + "') ";
				}
				text3 += " GROUP BY PR.CustomerID,Cus.CustomerName ORDER BY CustomerID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Customer", text3);
				DataSet dataSet2 = new DataSet();
				if (Type == "3")
				{
					text3 = "select * from Property_cancel PC\r\n                        LEFT JOIN Property P ON PC.PropertyID = P.PropertyID\r\n                        LEFT JOIN Property_Unit PU ON PC.UnitID = PU.PropertyUnitID\r\n                        LEFT JOIN Customer C ON PC.CustomerID = C.CustomerID WHERE PC.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
					if (fromProperty != "")
					{
						text3 = text3 + " AND PC.PropertyID BETWEEN '" + fromProperty + "' AND '" + toProperty + "' ";
					}
					if (customerIDs != "")
					{
						text3 = text3 + " AND PC.CustomerID IN(" + customerIDs + ")";
					}
					if (fromCustomer != "")
					{
						text3 = text3 + " AND PC.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "') ";
					}
					if (fromClass != "")
					{
						text3 = text3 + " AND CustomerClassID BETWEEN '" + fromClass + "' AND '" + toClass + "' ";
					}
					if (fromGroup != "")
					{
						text3 = text3 + " AND CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "' ";
					}
					if (fromUnit != "")
					{
						text3 = text3 + " AND PC.UnitID IN (SELECT UnitID FROM Property_Unit WHERE UnitID BETWEEN '" + fromUnit + "' AND '" + toUnit + "') ";
					}
					if (fromAgent != "")
					{
						text3 = text3 + " AND PC.PropertyAgentID IN (SELECT PropertyAgentID FROM Property_Agent WHERE PropertyAgentID BETWEEN '" + fromAgent + "' AND '" + toAgent + "') ";
					}
					FillDataSet(dataSet2, "PropertyDetails", text3);
				}
				else
				{
					text3 = "SELECT PR.SysDocID,PR.VoucherID,PR.TransactionDate,PR.PropertyID,P.PropertyName,PR.UnitID,PU.PropertyUnitName,PR.Total,PRD.Reference, PR.NoofInstallments, PR.Note, PRD.Amount, \r\n                                PRD.Description, PRD.IncomeID, PI.AccountID, PI.Description AS [Income Description], PI.IncomeName, PI.IncomeRate, PI.IncomeType,\r\n                                PR.ContractStartDate,PR.ContractEndDate,PR.CustomerID,C.CustomerName\r\n                                FROM Property_Rent PR\r\n                                LEFT OUTER JOIN Property_Rent_Detail PRD ON PR.SysDocID=PRD.SysDocID AND PR.VoucherID=PRD.VoucherID\r\n                                LEFT OUTER JOIN PropertyIncome_Code PI ON PI.IncomeID=PRD.IncomeID\r\n                                 LEFT JOIN Property P ON PR.PropertyID=P.PropertyID\r\n                                LEFT JOIN Property_Unit PU ON PR.UnitID=PU.PropertyUnitID \r\n                                LEFT JOIN Customer C ON PR.CustomerID=C.CustomerID                                                                 \r\n                                WHERE PR.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
					if (Type != "")
					{
						text3 = text3 + " AND PR.AgreementStatus IN ('" + Type + "')";
					}
					if (fromProperty != "")
					{
						text3 = text3 + " AND PR.PropertyID BETWEEN '" + fromProperty + "' AND '" + toProperty + "' ";
					}
					if (customerIDs != "")
					{
						text3 = text3 + " AND PR.CustomerID IN(" + customerIDs + ")";
					}
					if (fromCustomer != "")
					{
						text3 = text3 + " AND PR.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "') ";
					}
					if (fromClass != "")
					{
						text3 = text3 + " AND CustomerClassID BETWEEN '" + fromClass + "' AND '" + toClass + "' ";
					}
					if (fromGroup != "")
					{
						text3 = text3 + " AND CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "' ";
					}
					if (fromUnit != "")
					{
						text3 = text3 + " AND PR.UnitID IN (SELECT UnitID FROM Property_Unit WHERE UnitID BETWEEN '" + fromUnit + "' AND '" + toUnit + "') ";
					}
					if (fromAgent != "")
					{
						text3 = text3 + " AND PR.PropertyAgentID IN (SELECT PropertyAgentID FROM Property_Agent WHERE PropertyAgentID BETWEEN '" + fromAgent + "' AND '" + toAgent + "') ";
					}
					FillDataSet(dataSet2, "PropertyDetails", text3);
				}
				dataSet.Merge(dataSet2);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPropertyUnitAvailabilityReport(DateTime from, DateTime to, string fromUnit, string toUnit, string fromProperty, string toProperty, string basedOn)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT P.PropertyID,P.PropertyName,PU.PropertyUnitID,PU.PropertyUnitName  \r\n                            FROM Property_Unit PU INNER JOIN Property P ON P.PropertyID =PU.PropertyID \r\n                            LEFT JOIN Property_Rent PR ON PR.PropertyID=P.PropertyID AND PR.UnitID=PU.PropertyUnitID\r\n                            WHERE ('" + from + "'  NOT BETWEEN  ContractStartDate AND ContractEndDate  \r\n                            OR PU.PropertyUnitID NOT IN( SELECT UnitID FROM Property_Rent Property_Rent ))";
				if (fromProperty != "")
				{
					text = text + " AND PR.PropertyID BETWEEN '" + fromProperty + "' AND '" + toProperty + "' ";
				}
				if (fromUnit != "")
				{
					text = text + " AND PU.PropertyUnitID IN (SELECT PropertyUnitID FROM Property_Unit WHERE PropertyUnitID BETWEEN '" + fromUnit + "' AND '" + toUnit + "') ";
				}
				FillDataSet(dataSet, "PropertyDetails", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPropertyUnitHistoryReport(DateTime from, DateTime to, string fromUnit, string toUnit, string fromProperty, string toProperty)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = " SELECT PU.*\r\n                                        FROM Property_Unit PU INNER JOIN Property P ON P.PropertyID =PU.PropertyID \r\n                                        LEFT JOIN Property_Rent PR ON PR.PropertyID=P.PropertyID AND PR.UnitID=PU.PropertyUnitID\r\n\t\t\t\t\t\t\t            LEFT JOIN Property_Cancel PC ON PC.PropertyID = PR.PropertyID WHERE 1=1 ";
				if (fromProperty != "")
				{
					text = text + " AND PR.PropertyID BETWEEN '" + fromProperty + "' AND '" + toProperty + "' ";
				}
				if (fromUnit != "")
				{
					text = text + " AND PU.PropertyUnitID IN (SELECT PropertyUnitID FROM Property_Unit WHERE PropertyUnitID BETWEEN '" + fromUnit + "' AND '" + toUnit + "') ";
				}
				FillDataSet(dataSet, "PropertyDetails", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPropertyAvailabilityReport(DateTime date, string fromUnit, string toUnit, string fromProperty, string toProperty, string fromPropertyClass, string toPropertyClass)
		{
			try
			{
				CommonLib.ToSqlDateTimeString(date);
				DataSet dataSet = new DataSet();
				string text = "Select PU.*, P.PropertyName from Property_Unit PU \r\n                                INNER JOIN Property  P ON PU.PropertyID=P.PropertyID\r\n                                where PU.UnitStatus=1 ";
				if (fromProperty != "")
				{
					text = text + " AND PU.PropertyID BETWEEN '" + fromProperty + "' AND '" + toProperty + "' ";
				}
				if (fromUnit != "")
				{
					text = text + " AND PU.PropertyUnitID IN (SELECT PropertyUnitID FROM Property_Unit WHERE PropertyUnitID BETWEEN '" + fromUnit + "' AND '" + toUnit + "') ";
				}
				if (fromPropertyClass != "")
				{
					text = text + " AND P.PropertyClassID >='" + fromPropertyClass + "'";
				}
				if (toPropertyClass != "")
				{
					text = text + " AND P.PropertyClassID <='" + toPropertyClass + "'";
				}
				FillDataSet(dataSet, "PropertyDetails", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public byte[] GetPropertyThumbnailImage(string propertyID)
		{
			string exp = "SELECT Photo \r\n                           FROM PRoperty WHERE PropertyID='" + propertyID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return (byte[])obj;
			}
			return null;
		}

		public bool AddPropertyPhoto(string propertyID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Property SET Photo=@Photo WHERE PropertyID='" + propertyID + "'");
				sqlCommand.Parameters.AddWithValue("@Photo", image);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool RemovePropertyPhoto(string propertyID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Property SET Photo= Null WHERE propertyID='" + propertyID + "'");
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public DataSet GetPropertyUnitNames()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ItemPrice1Name,ItemPrice2Name,ItemPrice3Name\r\n\t\t\t\t\t\t   FROM Company WHERE CompanyID=1";
			FillDataSet(dataSet, "Product", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyReport(DateTime date, string fromProperty, string toProperty, string fromUnit, string toUnit)
		{
			try
			{
				CommonLib.ToSqlDateTimeString(date);
				DataSet dataSet = new DataSet();
				string text = "select P.*, PC.PropertyClassName, C.CountryName, CT.CityName, AssetName from Property P LEFT OUTER JOIN Property_Class PC ON P.PropertyClassID=PC.PropertyClassID \r\n                                LEFT OUTER JOIN Country C ON P.CountryID=C.CountryID \r\n                                LEFT OUTER JOIN City CT ON P.CityID=CT.CityID\r\n                                LEFT OUTER JOIN FixedAsset F ON P.FixedAssetID=F.AssetID where 1=1 ";
				if (fromProperty != "")
				{
					text = text + " AND P.PropertyID BETWEEN '" + fromProperty + "' AND '" + toProperty + "' ";
				}
				FillDataSet(dataSet, "PropertyDetails", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
