using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class HorseSummary : StoreObject
	{
		private const string HORSEID_PARM = "@HorseCode";

		private const string NAME_PARM = "@HorseName";

		private const string TYPE_PARM = "@HorseType";

		private const string REGISTERNUMBER_PARM = "@RegisterNumber";

		private const string MICROCHIPNUMBER_PARM = "@MicroChipNumber";

		public const string ISINACTIVE_PARM = "@IsInactive";

		private const string BRAND_PARM = "@Brand";

		private const string BREED_PARM = "@Breed";

		private const string BIRTHDATE_PARM = "@DateOfBirth";

		private const string COLOR_PARM = "@Colour";

		private const string SEX_PARM = "@Sex";

		private const string SIRE_PARM = "@Sire";

		private const string OWNERSHIPTYPEID_PARM = "@OwnershipTypeID";

		private const string CATEGORYID_PARM = "@CategoryID";

		private const string DAM_PARM = "@Dam";

		private const string SIREOFDAM_PARM = "@SireOfDam";

		private const string COUNTRYOFORIGIN_PARM = "@CountryOfOrigin";

		private const string CURRENTOWNERSHIP_PARM = "@CurrentOwnerShip";

		private const string PREVIOUSOWNERSHIP_PARM = "@PreviousOwnership";

		private const string OWNERSHIPCHANGEDDATE_PARM = "@OwnerShipChangedDate";

		private const string BREEDER_PARM = "@Breeder";

		private const string LOCATION_PARM = "@LocationID";

		private const string TRAINER_PARM = "@RiderID";

		private const string CARETAKER_PARM = "@CareTaker";

		private const string PASSPORTISSUEDATE_PARM = "@PassportIssueDate";

		private const string PASSPORTEXPIRYDATE_PARM = "@PassportExpiryDate";

		private const string REVALIDATED_PARM = "@RevalidationDate";

		private const string IMPORTEDFROM_PARM = "@ImportedFrom";

		private const string IMPORTEDDATE_PARM = "@ImportedDate";

		private const string PASTPERFORMANCE_PARM = "@PastPerformance";

		private const string EXPORTEDTO_PARM = "@ExportedTo";

		private const string RECEIVEDFROM_PARM = "@ReceivedFrom";

		private const string RECEIVEDDATE_PARM = "@ReceivedDate";

		private const string TRANSFERREDTO_PARM = "@TransferredTo";

		private const string TRANSFERREDDATE_PARM = "@TransferredDate";

		private const string SOLDAT_PARM = "@SoldAt";

		private const string SOLDDATE_PARM = "@SoldDate";

		private const string USERDEFINED1_PARM = "@UserDefined1";

		private const string USERDEFINED2_PARM = "@UserDefined2";

		private const string USERDEFINED3_PARM = "@UserDefined3";

		private const string USERDEFINED4_PARM = "@UserDefined4";

		private const string SEXCHANGEDFROM_PARM = "@SexChangedFrom";

		private const string SEXCHANGEDDATE_PARM = "@SexChangedDate";

		private const string TRANSFEROFOWNERSHIPGIVENON_PARM = "@OwnerShipTransferDate";

		private const string DEADON_PARM = "@DeadDate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public bool CheckConcurrency = true;

		public HorseSummary(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Horse_Summary", new FieldValue("HorseCode", "@HorseCode", isUpdateConditionField: true), new FieldValue("HorseName", "@HorseName"), new FieldValue("HorseType", "@HorseType"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("RegisterNumber", "@RegisterNumber"), new FieldValue("MicroChipNumber", "@MicroChipNumber"), new FieldValue("Brand", "@Brand"), new FieldValue("Breed", "@Breed"), new FieldValue("DateOfBirth", "@DateOfBirth"), new FieldValue("Colour", "@Colour"), new FieldValue("Sex", "@Sex"), new FieldValue("OwnershipTypeID", "@OwnershipTypeID"), new FieldValue("CategoryID", "@CategoryID"), new FieldValue("Sire", "@Sire"), new FieldValue("Dam", "@Dam"), new FieldValue("SireOfDam", "@SireOfDam"), new FieldValue("CountryOfOrigin", "@CountryOfOrigin"), new FieldValue("CurrentOwnerShip", "@CurrentOwnerShip"), new FieldValue("PreviousOwnership", "@PreviousOwnership"), new FieldValue("OwnerShipChangedDate", "@OwnerShipChangedDate"), new FieldValue("Breeder", "@Breeder"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RiderID", "@RiderID"), new FieldValue("CareTaker", "@CareTaker"), new FieldValue("PassportIssueDate", "@PassportIssueDate"), new FieldValue("PassportExpiryDate", "@PassportExpiryDate"), new FieldValue("RevalidationDate", "@RevalidationDate"), new FieldValue("ImportedFrom", "@ImportedFrom"), new FieldValue("ImportedDate", "@ImportedDate"), new FieldValue("PastPerformance", "@PastPerformance"), new FieldValue("ExportedTo", "@ExportedTo"), new FieldValue("ReceivedFrom", "@ReceivedFrom"), new FieldValue("ReceivedDate", "@ReceivedDate"), new FieldValue("TransferredTo", "@TransferredTo"), new FieldValue("TransferredDate", "@TransferredDate"), new FieldValue("SoldAt", "@SoldAt"), new FieldValue("SoldDate", "@SoldDate"), new FieldValue("SexChangedFrom", "@SexChangedFrom"), new FieldValue("SexChangedDate", "@SexChangedDate"), new FieldValue("OwnerShipTransferDate", "@OwnerShipTransferDate"), new FieldValue("DeadDate", "@DeadDate"), new FieldValue("UserDefined1", "@UserDefined1"), new FieldValue("UserDefined2", "@UserDefined2"), new FieldValue("UserDefined3", "@UserDefined3"), new FieldValue("UserDefined4", "@UserDefined4"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Horse_Summary", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@HorseCode", SqlDbType.NVarChar);
			parameters.Add("@HorseName", SqlDbType.NVarChar);
			parameters.Add("@HorseType", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@RegisterNumber", SqlDbType.NVarChar);
			parameters.Add("@MicroChipNumber", SqlDbType.NVarChar);
			parameters.Add("@Brand", SqlDbType.NVarChar);
			parameters.Add("@Breed", SqlDbType.NVarChar);
			parameters.Add("@DateOfBirth", SqlDbType.DateTime);
			parameters.Add("@Colour", SqlDbType.NVarChar);
			parameters.Add("@Sex", SqlDbType.NVarChar);
			parameters.Add("@OwnershipTypeID", SqlDbType.NVarChar);
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@Sire", SqlDbType.NVarChar);
			parameters.Add("@Dam", SqlDbType.NVarChar);
			parameters.Add("@SireOfDam", SqlDbType.NVarChar);
			parameters.Add("@CountryOfOrigin", SqlDbType.NVarChar);
			parameters.Add("@CurrentOwnerShip", SqlDbType.NVarChar);
			parameters.Add("@PreviousOwnership", SqlDbType.NVarChar);
			parameters.Add("@OwnerShipChangedDate", SqlDbType.DateTime);
			parameters.Add("@Breeder", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@RiderID", SqlDbType.NVarChar);
			parameters.Add("@CareTaker", SqlDbType.NVarChar);
			parameters.Add("@PassportIssueDate", SqlDbType.DateTime);
			parameters.Add("@PassportExpiryDate", SqlDbType.DateTime);
			parameters.Add("@RevalidationDate", SqlDbType.DateTime);
			parameters.Add("@ImportedFrom", SqlDbType.NVarChar);
			parameters.Add("@ImportedDate", SqlDbType.DateTime);
			parameters.Add("@PastPerformance", SqlDbType.NVarChar);
			parameters.Add("@ExportedTo", SqlDbType.NVarChar);
			parameters.Add("@ReceivedFrom", SqlDbType.NVarChar);
			parameters.Add("@ReceivedDate", SqlDbType.DateTime);
			parameters.Add("@TransferredTo", SqlDbType.NVarChar);
			parameters.Add("@TransferredDate", SqlDbType.DateTime);
			parameters.Add("@SoldAt", SqlDbType.NVarChar);
			parameters.Add("@SoldDate", SqlDbType.DateTime);
			parameters.Add("@SexChangedFrom", SqlDbType.NVarChar);
			parameters.Add("@SexChangedDate", SqlDbType.DateTime);
			parameters.Add("@OwnerShipTransferDate", SqlDbType.DateTime);
			parameters.Add("@DeadDate", SqlDbType.DateTime);
			parameters.Add("@UserDefined1", SqlDbType.NVarChar);
			parameters.Add("@UserDefined2", SqlDbType.NVarChar);
			parameters.Add("@UserDefined3", SqlDbType.NVarChar);
			parameters.Add("@UserDefined4", SqlDbType.NVarChar);
			parameters["@HorseCode"].SourceColumn = "HorseCode";
			parameters["@HorseName"].SourceColumn = "HorseName";
			parameters["@HorseType"].SourceColumn = "HorseType";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@RegisterNumber"].SourceColumn = "RegisterNumber";
			parameters["@MicroChipNumber"].SourceColumn = "MicroChipNumber";
			parameters["@Brand"].SourceColumn = "Brand";
			parameters["@Breed"].SourceColumn = "Breed";
			parameters["@DateOfBirth"].SourceColumn = "DateOfBirth";
			parameters["@Colour"].SourceColumn = "Colour";
			parameters["@Sex"].SourceColumn = "Sex";
			parameters["@OwnershipTypeID"].SourceColumn = "OwnershipTypeID";
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@Sire"].SourceColumn = "Sire";
			parameters["@Dam"].SourceColumn = "Dam";
			parameters["@SireOfDam"].SourceColumn = "SireOfDam";
			parameters["@CountryOfOrigin"].SourceColumn = "CountryOfOrigin";
			parameters["@CurrentOwnerShip"].SourceColumn = "CurrentOwnerShip";
			parameters["@PreviousOwnership"].SourceColumn = "PreviousOwnership";
			parameters["@OwnerShipChangedDate"].SourceColumn = "OwnerShipChangedDate";
			parameters["@Breeder"].SourceColumn = "Breeder";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@RiderID"].SourceColumn = "RiderID";
			parameters["@CareTaker"].SourceColumn = "CareTaker";
			parameters["@PassportIssueDate"].SourceColumn = "PassportIssueDate";
			parameters["@PassportExpiryDate"].SourceColumn = "PassportExpiryDate";
			parameters["@RevalidationDate"].SourceColumn = "RevalidationDate";
			parameters["@ImportedFrom"].SourceColumn = "ImportedFrom";
			parameters["@ImportedDate"].SourceColumn = "ImportedDate";
			parameters["@PastPerformance"].SourceColumn = "PastPerformance";
			parameters["@ExportedTo"].SourceColumn = "ExportedTo";
			parameters["@ReceivedFrom"].SourceColumn = "ReceivedFrom";
			parameters["@ReceivedDate"].SourceColumn = "ReceivedDate";
			parameters["@TransferredTo"].SourceColumn = "TransferredTo";
			parameters["@TransferredDate"].SourceColumn = "TransferredDate";
			parameters["@SoldAt"].SourceColumn = "SoldAt";
			parameters["@SoldDate"].SourceColumn = "SoldDate";
			parameters["@SexChangedFrom"].SourceColumn = "SexChangedFrom";
			parameters["@SexChangedDate"].SourceColumn = "SexChangedDate";
			parameters["@OwnerShipTransferDate"].SourceColumn = "OwnerShipTransferDate";
			parameters["@DeadDate"].SourceColumn = "DeadDate";
			parameters["@UserDefined1"].SourceColumn = "UserDefined1";
			parameters["@UserDefined2"].SourceColumn = "UserDefined2";
			parameters["@UserDefined3"].SourceColumn = "UserDefined3";
			parameters["@UserDefined4"].SourceColumn = "UserDefined4";
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

		public bool InsertHorseSummary(HorseSummaryData accountJobTaskData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountJobTaskData, "Horse_Summary", insertUpdateCommand);
				string text = accountJobTaskData.HorseSummaryTable.Rows[0]["HorseCode"].ToString();
				AddActivityLog("Horse Summary", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Horse_Summary", "HorseCode", text, sqlTransaction, isInsert: true);
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

		public bool UpdateHorseSummary(HorseSummaryData accountJobTaskData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountJobTaskData, "Horse_Summary", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountJobTaskData.HorseSummaryTable.Rows[0]["HorseCode"];
				UpdateTableRowByID("Horse_Summary", "HorseCode", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountJobTaskData.HorseSummaryTable.Rows[0]["HorseCode"].ToString();
				AddActivityLog("Horse Summary", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Horse_Summary", "HorseCode", obj, sqlTransaction, isInsert: false);
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

		public HorseSummaryData GetHorseSummary()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Horse_Summary");
			HorseSummaryData horseSummaryData = new HorseSummaryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(horseSummaryData, "Horse_Summary", sqlBuilder);
			return horseSummaryData;
		}

		public bool DeleteHorseSummary(string HorseID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Horse_Summary WHERE HorseCode = '" + HorseID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Horse Details", HorseID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public HorseSummaryData GetHorseSummaryByID(string id)
		{
			HorseSummaryData horseSummaryData = new HorseSummaryData();
			string textCommand = "SELECT HS.*,CASE WHEN Photo IS NULL THEN 'False' ELSE 'True' END AS HasPhoto  FROM Horse_Summary HS\r\n                            WHERE HorseCode='" + id + "'";
			FillDataSet(horseSummaryData, "Horse_Summary", textCommand);
			textCommand = "SELECT * FROM UDF_Horse WHERE EntityID = '" + id + "'";
			FillDataSet(horseSummaryData, "UDF", textCommand);
			return horseSummaryData;
		}

		public bool AddHorsePhoto(string horseID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Horse_Summary SET Photo=@Photo WHERE HorseCode='" + horseID + "'");
				sqlCommand.Parameters.AddWithValue("@Photo", image);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return result;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool RemoveHorsePhoto(string horseID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Horse_Summary SET Photo= Null WHERE HorseCode='" + horseID + "'");
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

		public byte[] GetHorseThumbnailImage(string horseID)
		{
			string exp = "SELECT Photo\r\n\t\t\t\t\t\t   FROM Horse_Summary P WHERE  HorseCode='" + horseID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return (byte[])obj;
			}
			return null;
		}

		public DataSet GetHorseSummaryList(bool isInactive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select  HorseCode [Code],HorseName [Name], MicroChipNumber, Brand,Breed,DateOfBirth,\r\n                                Dam,SireOfDam,Breeder,LocationName AS [Location], ImportedDate,PastPerformance,ReceivedDate FROM Horse_Summary H\r\n                                LEFT JOIN Location L ON H.LocationID=L.LocationID";
			FillDataSet(dataSet, "Horse_Summary", textCommand);
			return dataSet;
		}

		public DataSet GetHorseSummaryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT HorseCode [Code],HorseName [Name]\r\n                           FROM Horse_Summary ORDER BY HorseCode ,HorseName";
			FillDataSet(dataSet, "Horse_Summary", textCommand);
			return dataSet;
		}

		public DataSet GetHorseSummaryReport(string from, string to, string fromTrainer, string toTrainer, string fromLocation, string toLocation, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "select HS.Brand,HS.Brand, HS.Breeder, HS.CareTaker,Case HS.Colour WHEN 1 THEN 'OVERO' WHEN 2 THEN 'BROWN & WHITE' WHEN 3 THEN 'CREAM' \r\n                            WHEN 4 THEN 'NEGRO' WHEN 5 THEN 'UNKNOWN' WHEN 6 THEN 'GREY' WHEN 7 THEN 'CHESTNUT' WHEN 8 THEN 'GREY(TORDO)' WHEN 9 THEN 'BROWN' WHEN 10 THEN 'ROAN'\r\n                            WHEN 11 THEN 'DB/BR' WHEN 12 THEN 'BAY' WHEN 13 THEN 'PALOMINO' WHEN 14 THEN 'SORREL' WHEN 15 THEN 'BLACK & WHITE' WHEN 16 THEN 'TRI COL' WHEN 17 THEN 'BLACK'\r\n                            WHEN 18 THEN 'PIEBALD' END AS COLOR, \r\n                            (Select L1.CountryName from Horse_Summary HS1 LEFT JOIN Country L1 ON HS1.CountryOfOrigin=L1.CountryID Where HS1.HorseCode=Hs.HorseCode) AS [Origin],\r\n                             HS.HorseCode, HS.HorseName, HS.HorseType,(Select L1.CountryName from Horse_Summary HS1 LEFT JOIN Country L1 ON HS1.ImportedFrom=L1.CountryID Where HS1.HorseCode=Hs.HorseCode) AS [Imported FROM], HS.CurrentOwnerShip, HS.Dam,L.LocationName,HX.HorseSexName, \r\n                            HS.DateOfBirth,HT.HorseTypeName, HS.DeadDate, (Select L1.CountryName from Horse_Summary HS1 LEFT JOIN Country L1 ON HS1.ExportedTo=L1.CountryID Where HS1.HorseCode=Hs.HorseCode) AS [Exported TO], HS.HorseCode, HS.HorseName, HS.HorseType,(Select L1.CountryName from Horse_Summary HS1 LEFT JOIN Country L1 ON HS1.ImportedFrom=L1.CountryID Where HS1.HorseCode=Hs.HorseCode) AS [Imported FROM],\r\n                             HS.IsInactive, HS.LocationID, HS.MicroChipNumber, HS.PastPerformance, HS.PassportExpiryDate, HS.PassportIssueDate, HS.Photo, HS.PreviousOwnership,(Select L1.CountryName from Horse_Summary HS1 LEFT JOIN Country L1 ON HS1.ReceivedFrom=L1.CountryID Where HS1.HorseCode=Hs.HorseCode) AS [Received FROM],(Select L1.CountryName from Horse_Summary HS1 LEFT JOIN Country L1 ON HS1.TransferredTo=L1.CountryID Where HS1.HorseCode=Hs.HorseCode) AS [Transferred TO],(Select L1.LocationName from Horse_Summary HS1 LEFT JOIN Location L1 ON HS1.SoldAt=L1.LocationID Where HS1.HorseCode=Hs.HorseCode) AS [Sold AT],\r\n                            RS.RiderName,HS.ReceivedDate, HS.RegisterNumber, HS.RevalidationDate, HS.RiderID from Horse_Summary HS Left Join Rider_Summary RS On  HS.RiderID=Rs.RiderID\r\n                            LEFT JOIN Horse_Type HT ON HS.HorseType=HT.HorseTypeID\r\n                            LEFT JOIN Location L ON HS.LocationID=L.LocationID\r\n                            LEFT JOIN Horse_Sex HX ON HS.Sex=HX.HorseSexID\r\n                            WHERE 1=1 ";
			if (from != "")
			{
				text = text + " AND HS.HorseCode>='" + from + "'";
			}
			if (to != "")
			{
				text = text + " AND HS.HorseCode<='" + to + "'";
			}
			if (fromTrainer != "")
			{
				text = text + " AND HS.RiderID>='" + fromTrainer + "'";
			}
			if (toTrainer != "")
			{
				text = text + " AND HS.RiderID<='" + toTrainer + "'";
			}
			if (fromLocation != "")
			{
				text = text + " AND HS.LocationID>='" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				text = text + " AND HS.LocationID<='" + toLocation + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(HS.IsInactive,'False')='False' ";
			}
			FillDataSet(dataSet, "HorseSummary", text);
			return dataSet;
		}
	}
}
