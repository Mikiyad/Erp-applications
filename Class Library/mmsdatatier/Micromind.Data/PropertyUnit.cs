using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyUnit : StoreObject
	{
		private const string PROPERTYUNITID_PARM = "@PropertyUnitID";

		private const string PROPERTYUNITNAME_PARM = "@PropertyUnitName";

		private const string SHORTNAME_PARM = "@ShortName";

		private const string UNITSTATUS_PARM = "@UnitStatus";

		private const string AVAILABLEFROM_PARM = "@AvailableFrom";

		private const string AVAILABLETO_PARM = "@AvailableTo";

		private const string PROPERTYID_PARM = "@PropertyID";

		private const string PARENTUNITID_PARM = "@ParentUnitID";

		private const string UNITTYPEID_PARM = "@UnitTypeID";

		private const string NOBEDROOMS_PARM = "@NoBedRooms";

		private const string NOBATHROOMS_PARM = "@NoBathRooms";

		private const string TOTALROOMS_PARM = "@TotalRooms";

		private const string AREA_PARM = "@Area";

		private const string NOOFPARKING_PARM = "@NoofParking";

		private const string VIEWTYPEID_PARM = "@ViewTypeID";

		private const string KITHENTYPEID_PARM = "@KitchenTypeID";

		private const string NOTE_PARM = "@Note";

		private const string STATUS_PARM = "@Status";

		private const string PROPERTYTYPE_PARM = "@PropertyType";

		private const string ELECTRICITYPREMISENUMBER_PARM = "@ElectricityPremiseNumber";

		private const string MUNICIPALITYFILENUMBER_PARM = "@MunicipalityFileNumber";

		private const string ELECTRICITYFILENUMBER_PARM = "@ElectricityFileNumber";

		private const string MUNICIPALITYPERMITNUMBER_PARM = "@MunicipalityPermitNumber";

		private const string ELECTRICITYPERMITNUMBER_PARM = "@ElectricityPermitNumber";

		private const string RENTALINCOME_PARM = "@RentalIncome";

		public const string TAXOPTION_PARM = "@TaxOption";

		public const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PropertyUnit(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Unit", new FieldValue("PropertyUnitID", "@PropertyUnitID", isUpdateConditionField: true), new FieldValue("PropertyUnitName", "@PropertyUnitName"), new FieldValue("ShortName", "@ShortName"), new FieldValue("UnitStatus", "@UnitStatus"), new FieldValue("AvailableFrom", "@AvailableFrom"), new FieldValue("PropertyType", "@PropertyType"), new FieldValue("ParentUnitID", "@ParentUnitID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("ElectricityFileNumber", "@ElectricityFileNumber"), new FieldValue("MunicipalityFileNumber", "@MunicipalityFileNumber"), new FieldValue("ElectricityPermitNumber", "@ElectricityPermitNumber"), new FieldValue("MunicipalityPermitNumber", "@MunicipalityPermitNumber"), new FieldValue("ElectricityPremiseNumber", "@ElectricityPremiseNumber"), new FieldValue("AvailableTo", "@AvailableTo"), new FieldValue("PropertyID", "@PropertyID"), new FieldValue("UnitTypeID", "@UnitTypeID"), new FieldValue("NoBedRooms", "@NoBedRooms"), new FieldValue("NoBathRooms", "@NoBathRooms"), new FieldValue("TotalRooms", "@TotalRooms"), new FieldValue("Area", "@Area"), new FieldValue("NoofParking", "@NoofParking"), new FieldValue("ViewTypeID", "@ViewTypeID"), new FieldValue("KitchenTypeID", "@KitchenTypeID"), new FieldValue("Note", "@Note"), new FieldValue("RentalIncome", "@RentalIncome"), new FieldValue("Status", "@Status"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Unit", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PropertyUnitID", SqlDbType.NVarChar);
			parameters.Add("@PropertyUnitName", SqlDbType.NVarChar);
			parameters.Add("@ShortName", SqlDbType.NVarChar);
			parameters.Add("@UnitStatus", SqlDbType.NVarChar);
			parameters.Add("@AvailableFrom", SqlDbType.DateTime);
			parameters.Add("@AvailableTo", SqlDbType.DateTime);
			parameters.Add("@PropertyID", SqlDbType.NVarChar);
			parameters.Add("@ParentUnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitTypeID", SqlDbType.NVarChar);
			parameters.Add("@NoBedRooms", SqlDbType.NVarChar);
			parameters.Add("@NoBathRooms", SqlDbType.Int);
			parameters.Add("@TotalRooms", SqlDbType.Int);
			parameters.Add("@Area", SqlDbType.Decimal);
			parameters.Add("@NoofParking", SqlDbType.Int);
			parameters.Add("@ViewTypeID", SqlDbType.NVarChar);
			parameters.Add("@KitchenTypeID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.Bit);
			parameters.Add("@PropertyType", SqlDbType.TinyInt);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@ElectricityFileNumber", SqlDbType.NVarChar);
			parameters.Add("@ElectricityPermitNumber", SqlDbType.NVarChar);
			parameters.Add("@ElectricityPremiseNumber", SqlDbType.NVarChar);
			parameters.Add("@MunicipalityFileNumber", SqlDbType.NVarChar);
			parameters.Add("@MunicipalityPermitNumber", SqlDbType.NVarChar);
			parameters.Add("@RentalIncome", SqlDbType.Decimal);
			parameters["@PropertyUnitID"].SourceColumn = "PropertyUnitID";
			parameters["@PropertyUnitName"].SourceColumn = "PropertyUnitName";
			parameters["@ShortName"].SourceColumn = "ShortName";
			parameters["@UnitStatus"].SourceColumn = "UnitStatus";
			parameters["@AvailableFrom"].SourceColumn = "AvailableFrom";
			parameters["@AvailableTo"].SourceColumn = "AvailableTo";
			parameters["@PropertyID"].SourceColumn = "PropertyID";
			parameters["@ParentUnitID"].SourceColumn = "ParentUnitID";
			parameters["@UnitTypeID"].SourceColumn = "UnitTypeID";
			parameters["@NoBedRooms"].SourceColumn = "NoBedRooms";
			parameters["@NoBathRooms"].SourceColumn = "NoBathRooms";
			parameters["@TotalRooms"].SourceColumn = "TotalRooms";
			parameters["@Area"].SourceColumn = "Area";
			parameters["@NoofParking"].SourceColumn = "NoofParking";
			parameters["@ViewTypeID"].SourceColumn = "ViewTypeID";
			parameters["@KitchenTypeID"].SourceColumn = "KitchenTypeID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@PropertyType"].SourceColumn = "PropertyType";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@ElectricityFileNumber"].SourceColumn = "ElectricityFileNumber";
			parameters["@ElectricityPermitNumber"].SourceColumn = "ElectricityPermitNumber";
			parameters["@ElectricityPremiseNumber"].SourceColumn = "ElectricityPremiseNumber";
			parameters["@MunicipalityFileNumber"].SourceColumn = "MunicipalityFileNumber";
			parameters["@MunicipalityPermitNumber"].SourceColumn = "MunicipalityPermitNumber";
			parameters["@RentalIncome"].SourceColumn = "RentalIncome";
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

		public bool InsertUpdatePropertyUnit(PropertyUnitData accountPropertyUnitData, bool isUpdate)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = ((!isUpdate) ? Insert(accountPropertyUnitData, "Property_Unit", insertUpdateCommand) : Update(accountPropertyUnitData, "Property_Unit", insertUpdateCommand));
				string text = accountPropertyUnitData.PropertyUnitTable.Rows[0]["PropertyUnitID"].ToString();
				if (isUpdate)
				{
					AddActivityLog("PropertyUnit", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("PropertyUnit", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Property_Unit", "PropertyUnitID", text, sqlTransaction, !isUpdate);
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

		public PropertyUnitData GetPropertyUnit()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Unit");
			PropertyUnitData propertyUnitData = new PropertyUnitData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyUnitData, "Property_Unit", sqlBuilder);
			return propertyUnitData;
		}

		public bool DeletePropertyUnit(string vendorID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Property_Unit WHERE PropertyUnitID = '" + vendorID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("PropertyUnit", vendorID, ActivityTypes.Delete, null);
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

		public PropertyUnitData GetPropertyUnitByID(string id)
		{
			PropertyUnitData propertyUnitData = new PropertyUnitData();
			string textCommand = "SELECT *,(SELECT Count(P2.PropertyUnitID) FROM Property_Unit   P2 WHERE P2.Photo IS Not NULL AND P2.PropertyUnitID = P1.PropertyUnitID)  AS HasPhoto FROM Property_Unit P1 WHERE PropertyUnitID = '" + id + "'";
			FillDataSet(propertyUnitData, "Property_Unit", textCommand);
			textCommand = "SELECT * FROM UDF_PropertyUnit WHERE EntityID = '" + id + "'";
			FillDataSet(propertyUnitData, "UDF", textCommand);
			return propertyUnitData;
		}

		public DataSet GetPropertyUnitByFields(params string[] columns)
		{
			return GetPropertyUnitByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyUnitByFields(string[] PropertyUnitID, params string[] columns)
		{
			return GetPropertyUnitByFields(PropertyUnitID, isInactive: true, columns);
		}

		public DataSet GetPropertyUnitByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Unit");
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
				commandHelper.FieldName = "PropertyUnitID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Property_Unit";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "Status";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Property_Unit";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Property_Unit", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPropertyUnitList(bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "select PU.PropertyUnitID as [PropertyUnit Code],PU.PropertyUnitName AS Name ,P.PropertyName,PU.AvailableFrom,PU.AvailableTo,[NoBedRooms],[NoBathRooms],[TotalRooms],[Area],[NoofParking]\r\n                             ,RentalIncome from Property_Unit PU INNER JOIN Property P ON P.PropertyID=PU.PropertyID ORDER BY PropertyUnitID,PropertyUnitName ";
			if (!showInactive)
			{
				text += " WHERE ISNULL(P.Status,'False')='False'";
			}
			FillDataSet(dataSet, "Property_Unit", text);
			return dataSet;
		}

		public DataSet GetPropertyUnitComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyUnitID [Code],PropertyUnitName [Name],ISNULL(IsVirtual,'false')as IsVirtual, PropertyID, ISNULL(UnitStatus, 'false') as UnitStatus   FROM Property_Unit \r\n                            WHERE STATUS<>1 ORDER BY PropertyUnitID,PropertyUnitName";
			FillDataSet(dataSet, "Property_Unit", textCommand);
			return dataSet;
		}

		public byte[] GetPropertyUnitThumbnailImage(string propertyUnitID)
		{
			string exp = "SELECT Photo \r\n                           FROM Property_Unit WHERE PropertyUnitID='" + propertyUnitID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return (byte[])obj;
			}
			return null;
		}

		public bool AddPropertyUnitPhoto(string propertyUnitID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Property_Unit SET Photo=@Photo WHERE PropertyUnitID='" + propertyUnitID + "'");
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

		public bool RemovePropertyUnitPhoto(string propertyUnitID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Property_Unit SET Photo= Null WHERE PropertyUnitID='" + propertyUnitID + "'");
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

		public bool UpdatePropertyUnitStatus(string propertyUnitID, int status, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "Select Count(*) FROM Property_Unit sqd\r\n                                WHERE PropertyUnitID='" + propertyUnitID + "' ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Property_Unit SET UnitStatus='" + status + "' WHERE PropertyUnitID='" + propertyUnitID + "' ";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetPropertyUnitCurrentTenant(string propertyUnitID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select top 1 PR.SysDocID,PR.VoucherID,CONVERT(VARCHAR(10),PR.TransactionDate,3)as TransactionDate,CONVERT(VARCHAR(10),PR.ContractStartDate,3)as ContractStartDate,\r\n                              CONVERT(VARCHAR(10),PR.ContractEndDate,3)as ContractEndDate,PR.Total+isnull(taxamount,0) as Total,Isnull(PR.TaxAmount,0) as TaxAmount,(Select ISnull(Sum(Amount),0) from Property_Rent_Detail PRD Left join PropertyIncome_Code PIC ON PRD.IncomeID=PIC.IncomeID Where PRD.SysDocID=PR.SysDocID AND PRD.VoucherID=PR.VoucherID AND PIC.IncomeType=1) AS TotalRent,\r\n                              (Select ISnull(Sum(Amount),0) from Property_Rent_Detail PRD Left join PropertyIncome_Code PIC ON PRD.IncomeID=PIC.IncomeID Where PRD.SysDocID=PR.SysDocID AND PRD.VoucherID=PR.VoucherID AND PIC.IncomeType=3) AS TotalDeposit,\r\n                              PU.PropertyUnitName,C.CustomerID,C.CustomerName,CA.Address1 as [Address],CA.address2,CA.address3,\r\n                              CA.city,CA.Phone1 as [Phone],CA.Phone2,fax,CA.Mobile,CA.Email [Email2],CA.Country [CountryID] from Property_Rent PR \r\n                              LEFT JOIN Customer C ON C.CustomerID=PR.CustomerID \r\n                              LEFT JOIN Customer_Address CA ON C.CustomerID=CA.CustomerID \r\n                              LEFT JOIN Property_Unit PU ON PU.PropertyUnitID=PR.UnitID  \r\n                               where AgreementType!=3 and UNITID='" + propertyUnitID + "' Order by PR.TransactionDate DESC";
			FillDataSet(dataSet, "Property_Unit", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyUnitHistoryReport(string propertyUnitID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT PR.SysDocID [Reg SysDocID],PR.VoucherID [Reg VoucherID],PR.TransactionDate [Reg TransactionDate],PR.ContractStartDate,PR.ContractEndDate,PR.TotalDays,PR.Total,PR.AgreementStatus,\r\n                                                                PC.SysDocID [Can SysDocID],PC.VoucherID [Can VoucherID],PC.TransactionDate [Can TransactionDate]\r\n                                        FROM Property_Unit PU INNER JOIN Property P ON P.PropertyID =PU.PropertyID \r\n                                        LEFT JOIN Property_Rent PR ON PR.PropertyID=P.PropertyID AND PR.UnitID=PU.PropertyUnitID\r\n\t\t\t\t\t\t\t            LEFT JOIN Property_Cancel PC ON PC.PropertyID = PR.PropertyID \r\n                                        Where PR.AgreementType=3 AND PU.PropertyUnitID IN (SELECT PropertyUnitID FROM Property_Unit WHERE PropertyUnitID='" + propertyUnitID + "')  Order by PR.TransactionDate DESC";
				FillDataSet(dataSet, "PropertyDetails", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
