using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Data.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Micromind.Data
{
	public sealed class Products : StoreObject
	{
		public enum ProductAccounts
		{
			COGSAccount,
			AssetAccount,
			IncomeAccount
		}

		private const string PRODUCT_TABLE = "@Product";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string DESCRIPTION2_PARM = "@Description2";

		private const string DESCRIPTION3_PARM = "@Description3";

		private const string UPC_PARM = "@UPC1";

		private const string ISPRICEEMBEDDED_PARM = "@IsPriceEmbedded";

		private const string CLASSID_PARM = "@ClassID";

		private const string ITEMTYPE_PARM = "@ItemType";

		private const string VENDORREF_PARM = "@VendorRef";

		private const string MATRIXPARENTID_PARM = "@MatrixParentID";

		private const string ATTRIBUTE1_PARM = "@Attribute1";

		private const string ATTRIBUTE2_PARM = "@Attribute2";

		private const string ATTRIBUTE3_PARM = "@Attribute3";

		private const string EXCLUDEFROMCATALOGE_PARM = "@ExcludeFromCatalogue";

		private const string UNITPRICE1_PARM = "@UnitPrice1";

		private const string UNITPRICE2_PARM = "@UnitPrice2";

		private const string UNITPRICE3_PARM = "@UnitPrice3";

		private const string MINPRICE_PARM = "@MinPrice";

		private const string STANDARDCOST_PARM = "@StandardCost";

		private const string AVERAGECOST_PARM = "@AverageCost";

		private const string LASTCOST_PARM = "@LastCost";

		private const string COSTMETHOD_PARM = "@CostMethod";

		private const string QUANTITY_PARM = "@Quantity";

		private const string CATEGORYID_PARM = "@CategoryID";

		private const string QUANTITYPERUNIT_PARM = "@QuantityPerUnit";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string ISHOLDSALE_PARM = "@IsHoldSale";

		private const string WEIGHT_PARM = "@Weight";

		private const string PHOTO_PARM = "@Photo";

		private const string UNITID_PARM = "@UnitID";

		private const string REORDERLEVEL_PARM = "@ReorderLevel";

		private const string DEFAULTLOCATIONID_PARM = "@DefaultLocationID";

		private const string ISTRACKLOT_PARM = "@IsTrackLot";

		private const string ISTRACKSERIAL_PARM = "@IsTrackSerial";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string FACTOR_PARM = "@Factor";

		private const string ISMAINUNIT_PARM = "@IsMainUnit";

		private const string COGSACCOUNT_PARM = "@COGSAccount";

		private const string ASSETACCOUNT_PARM = "@AssetAccount";

		private const string INCOMEACCOUNT_PARM = "@IncomeAccount";

		private const string PREFERREDVENDOR_PARM = "@PreferredVendor";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string BOMID_PARM = "@BOMID";

		private const string EXPENSECODE_PARM = "@ExpenseCode";

		public const string TAXOPTION_PARM = "@TaxOption";

		public const string TAXGROUPID_PARM = "@TaxGroupID";

		public const string TAXIDNUMBER_PARM = "@TaxIDNumber";

		private const string STYLEID_PARM = "@StyleID";

		private const string ATTRIBUTE_PARM = "@Attribute";

		private const string SIZE_PARM = "@Size";

		private const string BRANDID_PARM = "@BrandID";

		private const string MANUFACTURERID_PARM = "@ManufacturerID";

		private const string ORIGIN_PARM = "@Origin";

		private const string WARRANTYPERIOD_PARM = "@WarrantyPeriod";

		private const string RACKBIN_PARM = "@RackBin";

		private const string NOTE_PARM = "@Note";

		private const string MATERIALID_PARM = "@MaterialID";

		private const string FINISHINGID_PARM = "@FinishingID";

		private const string COLORID_PARM = "@ColorID";

		private const string GRADEID_PARM = "@GradeID";

		private const string STANDARDID_PARM = "@StandardID";

		private const string PTYPE1_PARM = "@PType1";

		private const string PTYPE2_PARM = "@PType2";

		private const string PTYPE3_PARM = "@PType3";

		private const string PTYPE4_PARM = "@PType4";

		private const string PTYPE5_PARM = "@PType5";

		private const string PTYPE6_PARM = "@PType6";

		private const string PTYPE7_PARM = "@PType7";

		private const string PTYPE8_PARM = "@PType8";

		public const string BOMPRODUCTID_FIELD = "@BOMProductID";

		public const string ROWINDEX_FIELD = "@RowIndex";

		public const string COST_FIELD = "@Cost";

		private const string LOTNUMBER_PARM = "@LotNumber";

		private const string SOURCELOTNUMBER_PARM = "@SourceLotNumber";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string BINID_PARM = "@BinID";

		private const string RACKID_PARM = "@RackID";

		private const string LOTQTY_PARM = "@LotQty";

		private const string SOLDQTY_PARM = "@SoldQty";

		private const string PRODUCTIONDATE_PARM = "@ProductionDate";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string W3PLRENTPRICE_PARM = "@W3PLRentPrice";

		private const string USERDEFINED1_PARM = "@UserDefined1";

		private const string USERDEFINED2_PARM = "@UserDefined2";

		private const string USERDEFINED3_PARM = "@UserDefined3";

		private const string USERDEFINED4_PARM = "@UserDefined4";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTPARTSDETAIL_TABLE = "Product_Parts_Detail";

		private const string PRODUCTSUBSTITUTEDETAIL_TABLE = "Product_Substitute_Detail";

		private const string PRODUCTAPPLIEDMODELSDETAIL_TABLE = "Product_AppliedModels_Detail";

		private const string SPECIFICATION_PARM = "@Specification";

		private const string VEHICLEMAKEID_PARM = "@VehicleMakeID";

		private const string VEHICLETYPEID_PARM = "@VehicleTypeID";

		private const string VEHICLEMODELID_PARM = "@VehicleModelID";

		private const string PARTSMAKETYPEID_PARM = "@PartsMakeTypeID";

		private const string PARTSTYPEID_PARM = "@PartsTypeID";

		private const string PARTSFAMILYID_PARM = "@PartsFamilyID";

		private const string PARTSCHASISNO_PARM = "@PartsChasisNo";

		private const string PARTSMODEL_PARM = "@PartsModel";

		private const string PARTSENGINENO_PARM = "@PartsEngineNo";

		private const string OEMCODE_PARM = "@OEMCode";

		private const string PRODUCCTPRICELISTDETAIL_TABLE = "Product_PriceList_Detail";

		private const string UNITPRICE1GRID_PARM = "@UnitPrice1";

		private const string UNITPRICE2GRID_PARM = "@UnitPrice2";

		private const string UNITPRICE3GRID_PARM = "@UnitPrice3";

		private const string MINPRICEGRID_PARM = "@MinPrice";

		private const string LOCATIONIDGRID_PARM = "@LocationID";

		private const string SUBSTITUTEPRODUCTID_PARM = "@SubstituteProductID";

		private const string SUBPRODUCTDESCRIPTION_PARM = "@SubProductDescription";

		private const string REMARKS_PARM = "@Remarks";

		public Products(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product", new FieldValue("ProductID", "@ProductID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("Description2", "@Description2"), new FieldValue("Description3", "@Description3"), new FieldValue("UPC", "@UPC1"), new FieldValue("IsPriceEmbedded", "@IsPriceEmbedded"), new FieldValue("ClassID", "@ClassID"), new FieldValue("ItemType", "@ItemType"), new FieldValue("VendorRef", "@VendorRef"), new FieldValue("MatrixParentID", "@MatrixParentID"), new FieldValue("UnitPrice1", "@UnitPrice1"), new FieldValue("UnitPrice2", "@UnitPrice2"), new FieldValue("UnitPrice3", "@UnitPrice3"), new FieldValue("MinPrice", "@MinPrice"), new FieldValue("StandardCost", "@StandardCost"), new FieldValue("AverageCost", "@AverageCost"), new FieldValue("LastCost", "@LastCost"), new FieldValue("CostMethod", "@CostMethod"), new FieldValue("Quantity", "@Quantity"), new FieldValue("ExcludeFromCatalogue", "@ExcludeFromCatalogue"), new FieldValue("CategoryID", "@CategoryID"), new FieldValue("BOMID", "@BOMID"), new FieldValue("QuantityPerUnit", "@QuantityPerUnit"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("IsHoldSale", "@IsHoldSale"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("Weight", "@Weight"), new FieldValue("Photo", "@Photo"), new FieldValue("UnitID", "@UnitID"), new FieldValue("IsTrackLot", "@IsTrackLot"), new FieldValue("IsTrackSerial", "@IsTrackSerial"), new FieldValue("ReorderLevel", "@ReorderLevel"), new FieldValue("DefaultLocationID", "@DefaultLocationID"), new FieldValue("COGSAccount", "@COGSAccount"), new FieldValue("ExpenseCode", "@ExpenseCode"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxIDNumber", "@TaxIDNumber"), new FieldValue("AssetAccount", "@AssetAccount"), new FieldValue("IncomeAccount", "@IncomeAccount"), new FieldValue("PreferredVendor", "@PreferredVendor"), new FieldValue("StyleID", "@StyleID"), new FieldValue("Attribute1", "@Attribute1"), new FieldValue("Attribute2", "@Attribute2"), new FieldValue("Attribute3", "@Attribute3"), new FieldValue("Attribute", "@Attribute"), new FieldValue("Size", "@Size"), new FieldValue("BrandID", "@BrandID"), new FieldValue("ManufacturerID", "@ManufacturerID"), new FieldValue("MaterialID", "@MaterialID"), new FieldValue("FinishingID", "@FinishingID"), new FieldValue("ColorID", "@ColorID"), new FieldValue("GradeID", "@GradeID"), new FieldValue("StandardID", "@StandardID"), new FieldValue("PType1", "@PType1"), new FieldValue("PType2", "@PType2"), new FieldValue("PType3", "@PType3"), new FieldValue("PType4", "@PType4"), new FieldValue("PType5", "@PType5"), new FieldValue("PType6", "@PType6"), new FieldValue("PType7", "@PType7"), new FieldValue("PType8", "@PType8"), new FieldValue("W3PLRentPrice", "@W3PLRentPrice"), new FieldValue("Origin", "@Origin"), new FieldValue("WarrantyPeriod", "@WarrantyPeriod"), new FieldValue("RackBin", "@RackBin"), new FieldValue("UserDefined1", "@UserDefined1"), new FieldValue("UserDefined2", "@UserDefined2"), new FieldValue("UserDefined3", "@UserDefined3"), new FieldValue("UserDefined4", "@UserDefined4"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Description2", SqlDbType.NVarChar);
			parameters.Add("@Description3", SqlDbType.NVarChar);
			parameters.Add("@UPC1", SqlDbType.NVarChar);
			parameters.Add("@IsPriceEmbedded", SqlDbType.Bit);
			parameters.Add("@ClassID", SqlDbType.NVarChar);
			parameters.Add("@ItemType", SqlDbType.TinyInt);
			parameters.Add("@VendorRef", SqlDbType.NVarChar);
			parameters.Add("@MatrixParentID", SqlDbType.NVarChar);
			parameters.Add("@UnitPrice1", SqlDbType.Money);
			parameters.Add("@UnitPrice2", SqlDbType.Money);
			parameters.Add("@UnitPrice3", SqlDbType.Money);
			parameters.Add("@MinPrice", SqlDbType.Money);
			parameters.Add("@StandardCost", SqlDbType.Money);
			parameters.Add("@AverageCost", SqlDbType.Money);
			parameters.Add("@LastCost", SqlDbType.Money);
			parameters.Add("@CostMethod", SqlDbType.TinyInt);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@ExcludeFromCatalogue", SqlDbType.Bit);
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@IsTrackLot", SqlDbType.NVarChar);
			parameters.Add("@IsTrackSerial", SqlDbType.NVarChar);
			parameters.Add("@QuantityPerUnit", SqlDbType.Real);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@IsHoldSale", SqlDbType.Bit);
			parameters.Add("@Weight", SqlDbType.Decimal);
			parameters.Add("@Photo", SqlDbType.Image);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@BOMID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseCode", SqlDbType.NVarChar);
			parameters.Add("@ReorderLevel", SqlDbType.Real);
			parameters.Add("@DefaultLocationID", SqlDbType.NVarChar);
			parameters.Add("@COGSAccount", SqlDbType.NVarChar);
			parameters.Add("@AssetAccount", SqlDbType.NVarChar);
			parameters.Add("@IncomeAccount", SqlDbType.NVarChar);
			parameters.Add("@PreferredVendor", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@Attribute", SqlDbType.NVarChar);
			parameters.Add("@Attribute1", SqlDbType.NVarChar);
			parameters.Add("@Attribute2", SqlDbType.NVarChar);
			parameters.Add("@Attribute3", SqlDbType.NVarChar);
			parameters.Add("@Size", SqlDbType.NVarChar);
			parameters.Add("@BrandID", SqlDbType.NVarChar);
			parameters.Add("@ManufacturerID", SqlDbType.NVarChar);
			parameters.Add("@Origin", SqlDbType.NVarChar);
			parameters.Add("@WarrantyPeriod", SqlDbType.Int);
			parameters.Add("@RackBin", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@UserDefined1", SqlDbType.NVarChar);
			parameters.Add("@UserDefined2", SqlDbType.NVarChar);
			parameters.Add("@UserDefined3", SqlDbType.NVarChar);
			parameters.Add("@UserDefined4", SqlDbType.NVarChar);
			parameters.Add("@W3PLRentPrice", SqlDbType.Decimal);
			parameters.Add("@MaterialID", SqlDbType.NVarChar);
			parameters.Add("@FinishingID", SqlDbType.NVarChar);
			parameters.Add("@ColorID", SqlDbType.NVarChar);
			parameters.Add("@GradeID", SqlDbType.NVarChar);
			parameters.Add("@StandardID", SqlDbType.NVarChar);
			parameters.Add("@PType1", SqlDbType.NVarChar);
			parameters.Add("@PType2", SqlDbType.NVarChar);
			parameters.Add("@PType3", SqlDbType.NVarChar);
			parameters.Add("@PType4", SqlDbType.NVarChar);
			parameters.Add("@PType5", SqlDbType.NVarChar);
			parameters.Add("@PType6", SqlDbType.NVarChar);
			parameters.Add("@PType7", SqlDbType.NVarChar);
			parameters.Add("@PType8", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxIDNumber", SqlDbType.NVarChar);
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Description2"].SourceColumn = "Description2";
			parameters["@Description3"].SourceColumn = "Description3";
			parameters["@UPC1"].SourceColumn = "UPC";
			parameters["@IsPriceEmbedded"].SourceColumn = "IsPriceEmbedded";
			parameters["@ClassID"].SourceColumn = "ClassID";
			parameters["@ItemType"].SourceColumn = "ItemType";
			parameters["@VendorRef"].SourceColumn = "VendorRef";
			parameters["@MatrixParentID"].SourceColumn = "MatrixParentID";
			parameters["@UnitPrice1"].SourceColumn = "UnitPrice1";
			parameters["@UnitPrice2"].SourceColumn = "UnitPrice2";
			parameters["@UnitPrice3"].SourceColumn = "UnitPrice3";
			parameters["@MinPrice"].SourceColumn = "MinPrice";
			parameters["@ExcludeFromCatalogue"].SourceColumn = "ExcludeFromCatalogue";
			parameters["@StandardCost"].SourceColumn = "StandardCost";
			parameters["@AverageCost"].SourceColumn = "AverageCost";
			parameters["@LastCost"].SourceColumn = "LastCost";
			parameters["@CostMethod"].SourceColumn = "CostMethod";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@IsTrackLot"].SourceColumn = "IsTrackLot";
			parameters["@IsTrackSerial"].SourceColumn = "IsTrackSerial";
			parameters["@QuantityPerUnit"].SourceColumn = "QuantityPerUnit";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@IsHoldSale"].SourceColumn = "IsHoldSale";
			parameters["@Weight"].SourceColumn = "Weight";
			parameters["@Photo"].SourceColumn = "Photo";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@BOMID"].SourceColumn = "BOMID";
			parameters["@ExpenseCode"].SourceColumn = "ExpenseCode";
			parameters["@ReorderLevel"].SourceColumn = "ReorderLevel";
			parameters["@DefaultLocationID"].SourceColumn = "DefaultLocationID";
			parameters["@COGSAccount"].SourceColumn = "COGSAccount";
			parameters["@AssetAccount"].SourceColumn = "AssetAccount";
			parameters["@IncomeAccount"].SourceColumn = "IncomeAccount";
			parameters["@PreferredVendor"].SourceColumn = "PreferredVendor";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@Attribute1"].SourceColumn = "Attribute1";
			parameters["@Attribute2"].SourceColumn = "Attribute2";
			parameters["@Attribute3"].SourceColumn = "Attribute3";
			parameters["@Attribute"].SourceColumn = "Attribute";
			parameters["@Size"].SourceColumn = "Size";
			parameters["@BrandID"].SourceColumn = "BrandID";
			parameters["@W3PLRentPrice"].SourceColumn = "W3PLRentPrice";
			parameters["@ManufacturerID"].SourceColumn = "ManufacturerID";
			parameters["@Origin"].SourceColumn = "Origin";
			parameters["@WarrantyPeriod"].SourceColumn = "WarrantyPeriod";
			parameters["@RackBin"].SourceColumn = "RackBin";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@UserDefined1"].SourceColumn = "UserDefined1";
			parameters["@UserDefined2"].SourceColumn = "UserDefined2";
			parameters["@UserDefined3"].SourceColumn = "UserDefined3";
			parameters["@UserDefined4"].SourceColumn = "UserDefined4";
			parameters["@MaterialID"].SourceColumn = "MaterialID";
			parameters["@FinishingID"].SourceColumn = "FinishingID";
			parameters["@ColorID"].SourceColumn = "ColorID";
			parameters["@GradeID"].SourceColumn = "GradeID";
			parameters["@StandardID"].SourceColumn = "StandardID";
			parameters["@PType1"].SourceColumn = "PType1";
			parameters["@PType2"].SourceColumn = "PType2";
			parameters["@PType3"].SourceColumn = "PType3";
			parameters["@PType4"].SourceColumn = "PType4";
			parameters["@PType5"].SourceColumn = "PType5";
			parameters["@PType6"].SourceColumn = "PType6";
			parameters["@PType7"].SourceColumn = "PType7";
			parameters["@PType8"].SourceColumn = "PType8";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxIDNumber"].SourceColumn = "TaxIDNumber";
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

		private string GetInsertUpdateProductUnitText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Unit", new FieldValue("ProductID", "@ProductID"), new FieldValue("UnitID", "@UnitID"), new FieldValue("FactorType", "@FactorType"), new FieldValue("IsMainUnit", "@IsMainUnit"), new FieldValue("Factor", "@Factor"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductUnitCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateProductUnitText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateProductUnitText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@IsMainUnit", SqlDbType.Bit);
			parameters.Add("@Factor", SqlDbType.Real);
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@IsMainUnit"].SourceColumn = "IsMainUnit";
			parameters["@Factor"].SourceColumn = "Factor";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateProductSubstituteCommand(bool isUpdate)
		{
			insertCommand = null;
			updateCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateProductSubstituteText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateProductSubstituteText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@SubstituteProductID", SqlDbType.NVarChar);
			parameters.Add("@SubProductDescription", SqlDbType.NVarChar);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@SubstituteProductID"].SourceColumn = "SubstituteProductID";
			parameters["@SubProductDescription"].SourceColumn = "SubProductDescription";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateProductSubstituteText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Substitute_Detail", new FieldValue("ProductID", "@ProductID"), new FieldValue("SubstituteProductID", "@SubstituteProductID"), new FieldValue("SubProductDescription", "@SubProductDescription"), new FieldValue("UnitPrice", "@UnitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductAppliedCommand(bool isUpdate)
		{
			insertCommand = null;
			updateCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateProductAppliedText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateProductAppliedText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@VehicleMakeID", SqlDbType.NVarChar);
			parameters.Add("@VehicleTypeID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@VehicleMakeID"].SourceColumn = "VehicleMakeID";
			parameters["@VehicleTypeID"].SourceColumn = "VehicleTypeID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateProductAppliedText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_AppliedModels_Detail", new FieldValue("ProductID", "@ProductID"), new FieldValue("VehicleMakeID", "@VehicleMakeID"), new FieldValue("VehicleTypeID", "@VehicleTypeID"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductPriceListCommand(bool isUpdate)
		{
			insertCommand = null;
			updateCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateProductPriceListText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateProductPriceListText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@UnitPrice1", SqlDbType.Decimal);
			parameters.Add("@UnitPrice2", SqlDbType.Decimal);
			parameters.Add("@UnitPrice3", SqlDbType.Decimal);
			parameters.Add("@MinPrice", SqlDbType.Decimal);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@UnitPrice1"].SourceColumn = "UnitPrice1";
			parameters["@UnitPrice2"].SourceColumn = "UnitPrice2";
			parameters["@UnitPrice3"].SourceColumn = "UnitPrice3";
			parameters["@MinPrice"].SourceColumn = "MinPrice";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitID"].SourceColumn = "UnitID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateProductPriceListText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_PriceList_Detail", new FieldValue("ProductID", "@ProductID"), new FieldValue("UnitPrice1", "@UnitPrice1"), new FieldValue("UnitPrice2", "@UnitPrice2"), new FieldValue("UnitPrice3", "@UnitPrice3"), new FieldValue("MinPrice", "@MinPrice"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateProductLotReceivingDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Lot_Receiving_Detail", new FieldValue("SourceLotNumber", "@SourceLotNumber"), new FieldValue("LotNumber", "@LotNumber"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("BinID", "@BinID"), new FieldValue("RackID", "@RackID"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("LotQty", "@LotQty"), new FieldValue("ProductionDate", "@ProductionDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("Reference2", "@Reference2"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductLotReceivingDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateProductLotReceivingDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateProductLotReceivingDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SourceLotNumber", SqlDbType.NVarChar);
			parameters.Add("@LotNumber", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@BinID", SqlDbType.NVarChar);
			parameters.Add("@RackID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@LotQty", SqlDbType.Decimal);
			parameters.Add("@ProductionDate", SqlDbType.DateTime);
			parameters.Add("@ExpiryDate", SqlDbType.DateTime);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters["@SourceLotNumber"].SourceColumn = "SourceLotNumber";
			parameters["@LotNumber"].SourceColumn = "LotNumber";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@BinID"].SourceColumn = "BinID";
			parameters["@RackID"].SourceColumn = "RackID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@LotQty"].SourceColumn = "LotQty";
			parameters["@ProductionDate"].SourceColumn = "ProductionDate";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@Reference2"].SourceColumn = "Reference2";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateProductLotIssueDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Lot_Issue_Detail", new FieldValue("LotNumber", "@LotNumber"), new FieldValue("SourceLotNumber", "@SourceLotNumber"), new FieldValue("Reference", "@Reference"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("BinID", "@BinID"), new FieldValue("RackID", "@RackID"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("SoldQty", "@SoldQty"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Cost", "@Cost"), new FieldValue("Reference2", "@Reference2"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductLotIssueDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateProductLotIssueDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateProductLotIssueDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@LotNumber", SqlDbType.Int);
			parameters.Add("@SourceLotNumber", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@BinID", SqlDbType.NVarChar);
			parameters.Add("@RackID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@SoldQty", SqlDbType.Decimal);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Cost", SqlDbType.Decimal);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters["@LotNumber"].SourceColumn = "LotNumber";
			parameters["@SourceLotNumber"].SourceColumn = "SourceLotNumber";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@BinID"].SourceColumn = "BinID";
			parameters["@RackID"].SourceColumn = "RackID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@SoldQty"].SourceColumn = "SoldQty";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Reference2"].SourceColumn = "Reference2";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateProductPartDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Parts_Detail", new FieldValue("ProductID", "@ProductID", isUpdateConditionField: true), new FieldValue("Specification", "@Specification"), new FieldValue("VehicleMakeID", "@VehicleMakeID"), new FieldValue("VehicleTypeID", "@VehicleTypeID"), new FieldValue("VehicleModelID", "@VehicleModelID"), new FieldValue("PartsMakeTypeID", "@PartsMakeTypeID"), new FieldValue("PartsTypeID", "@PartsTypeID"), new FieldValue("PartsFamilyID", "@PartsFamilyID"), new FieldValue("PartsChasisNo", "@PartsChasisNo"), new FieldValue("PartsModel", "@PartsModel"), new FieldValue("OEMCode", "@OEMCode"), new FieldValue("PartsEngineNo", "@PartsEngineNo"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Parts_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductPartDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProductPartDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProductPartDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Specification", SqlDbType.NVarChar);
			parameters.Add("@VehicleMakeID", SqlDbType.NVarChar);
			parameters.Add("@VehicleTypeID", SqlDbType.NVarChar);
			parameters.Add("@VehicleModelID", SqlDbType.NVarChar);
			parameters.Add("@PartsMakeTypeID", SqlDbType.NVarChar);
			parameters.Add("@PartsTypeID", SqlDbType.NVarChar);
			parameters.Add("@PartsFamilyID", SqlDbType.NVarChar);
			parameters.Add("@PartsChasisNo", SqlDbType.NVarChar);
			parameters.Add("@PartsModel", SqlDbType.NVarChar);
			parameters.Add("@PartsEngineNo", SqlDbType.NVarChar);
			parameters.Add("@OEMCode", SqlDbType.NVarChar);
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Specification"].SourceColumn = "Specification";
			parameters["@VehicleMakeID"].SourceColumn = "VehicleMakeID";
			parameters["@VehicleTypeID"].SourceColumn = "VehicleTypeID";
			parameters["@VehicleModelID"].SourceColumn = "VehicleModelID";
			parameters["@PartsMakeTypeID"].SourceColumn = "PartsMakeTypeID";
			parameters["@PartsTypeID"].SourceColumn = "PartsTypeID";
			parameters["@PartsFamilyID"].SourceColumn = "PartsFamilyID";
			parameters["@PartsChasisNo"].SourceColumn = "PartsChasisNo";
			parameters["@PartsModel"].SourceColumn = "PartsModel";
			parameters["@PartsEngineNo"].SourceColumn = "PartsEngineNo";
			parameters["@OEMCode"].SourceColumn = "OEMCode";
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

		public bool InsertUpdateProduct(ProductData productData, bool isUpdate)
		{
			return InsertUpdateProduct(productData, isUpdate, null);
		}

		public bool InsertUpdateProduct(ProductData productData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			if (sqlTransaction == null)
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
			}
			insertUpdateCommand.Transaction = sqlTransaction;
			try
			{
				DataRow dataRow = productData.ProductTable.Rows[0];
				decimal result = default(decimal);
				decimal.TryParse(dataRow["StandardCost"].ToString(), out result);
				string text = dataRow["ProductID"].ToString();
				if (!isUpdate)
				{
					foreach (DataRow row in productData.ProductTable.Rows)
					{
						row["AverageCost"] = result;
						if (row["Weight"].ToString() == "" || row["Weight"] == DBNull.Value)
						{
							row["Weight"] = 0;
							row["WarrantyPeriod"] = 1;
						}
					}
				}
				flag = (isUpdate ? (flag & Update(productData, "Product", insertUpdateCommand)) : (flag & Insert(productData, "Product", insertUpdateCommand)));
				if (!isUpdate)
				{
					if (flag && productData.ProductPartsDetail.Rows.Count > 0)
					{
						insertUpdateCommand = GetInsertUpdateProductPartDetailsCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Insert(productData, "Product_Parts_Detail", insertUpdateCommand);
						insertUpdateCommand = GetInsertUpdateProductSubstituteCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						if (isUpdate)
						{
							string commandText = "DELETE FROM Product_Substitute_Detail WHERE ProductID = '" + text + "'";
							flag &= Delete(commandText, sqlTransaction);
						}
						if (productData.Tables.Contains("Product_Substitute_Detail") && productData.Tables["Product_Substitute_Detail"].Rows.Count > 0)
						{
							flag &= Insert(productData, "Product_Substitute_Detail", insertUpdateCommand);
						}
						insertUpdateCommand = GetInsertUpdateProductAppliedCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						if (isUpdate)
						{
							string commandText2 = "DELETE FROM Product_AppliedModels_Detail WHERE ProductID = '" + text + "'";
							flag &= Delete(commandText2, sqlTransaction);
						}
						if (productData.Tables.Contains("Product_AppliedModels_Detail") && productData.Tables["Product_AppliedModels_Detail"].Rows.Count > 0)
						{
							flag &= Insert(productData, "Product_AppliedModels_Detail", insertUpdateCommand);
						}
					}
				}
				else if (flag && productData.ProductPartsDetail.Rows.Count > 0)
				{
					string id = productData.ProductPartsDetail.Rows[0]["ProductID"].ToString();
					DataSet productByID = GetProductByID(id);
					if (productByID != null && productByID.Tables.Count > 0 && productByID.Tables["Product_Parts_Detail"].Rows.Count > 0)
					{
						insertUpdateCommand = GetInsertUpdateProductPartDetailsCommand(isUpdate: true);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Update(productData, "Product_Parts_Detail", insertUpdateCommand);
						insertUpdateCommand = GetInsertUpdateProductSubstituteCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						if (isUpdate)
						{
							string commandText3 = "DELETE FROM Product_Substitute_Detail WHERE ProductID = '" + text + "'";
							flag &= Delete(commandText3, sqlTransaction);
						}
						if (productData.Tables.Contains("Product_Substitute_Detail") && productData.Tables["Product_Substitute_Detail"].Rows.Count > 0)
						{
							flag &= Insert(productData, "Product_Substitute_Detail", insertUpdateCommand);
						}
						insertUpdateCommand = GetInsertUpdateProductAppliedCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						if (isUpdate)
						{
							string commandText4 = "DELETE FROM Product_AppliedModels_Detail WHERE ProductID = '" + text + "'";
							flag &= Delete(commandText4, sqlTransaction);
						}
						if (productData.Tables.Contains("Product_AppliedModels_Detail") && productData.Tables["Product_AppliedModels_Detail"].Rows.Count > 0)
						{
							flag &= Insert(productData, "Product_AppliedModels_Detail", insertUpdateCommand);
						}
					}
					else
					{
						insertUpdateCommand = GetInsertUpdateProductPartDetailsCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Insert(productData, "Product_Parts_Detail", insertUpdateCommand);
						insertUpdateCommand = GetInsertUpdateProductSubstituteCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						if (isUpdate)
						{
							string commandText5 = "DELETE FROM Product_Substitute_Detail WHERE ProductID = '" + text + "'";
							flag &= Delete(commandText5, sqlTransaction);
						}
						if (productData.Tables.Contains("Product_Substitute_Detail") && productData.Tables["Product_Substitute_Detail"].Rows.Count > 0)
						{
							flag &= Insert(productData, "Product_Substitute_Detail", insertUpdateCommand);
						}
						insertUpdateCommand = GetInsertUpdateProductAppliedCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						if (isUpdate)
						{
							string commandText6 = "DELETE FROM Product_AppliedModels_Detail WHERE ProductID = '" + text + "'";
							flag &= Delete(commandText6, sqlTransaction);
						}
						if (productData.Tables.Contains("Product_AppliedModels_Detail") && productData.Tables["Product_AppliedModels_Detail"].Rows.Count > 0)
						{
							flag &= Insert(productData, "Product_AppliedModels_Detail", insertUpdateCommand);
						}
					}
				}
				insertUpdateCommand = GetInsertUpdateProductUnitCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					string commandText7 = "DELETE FROM Product_Unit WHERE ProductID = '" + text + "'";
					flag &= Delete(commandText7, sqlTransaction);
				}
				if (productData.Tables.Contains("Product_Unit") && productData.Tables["Product_Unit"].Rows.Count > 0)
				{
					flag &= Insert(productData, "Product_Unit", insertUpdateCommand);
				}
				insertUpdateCommand = GetInsertUpdateProductPriceListCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					string commandText8 = "DELETE FROM Product_PriceList_Detail WHERE ProductID = '" + text + "'";
					flag &= Delete(commandText8, sqlTransaction);
				}
				if (productData.Tables.Contains("Product_PriceList_Detail") && productData.Tables["Product_PriceList_Detail"].Rows.Count > 0)
				{
					flag &= Insert(productData, "Product_PriceList_Detail", insertUpdateCommand);
				}
				if (flag)
				{
					if (isUpdate)
					{
						AddActivityLog("Item", text, ActivityTypes.Update, sqlTransaction);
					}
					else
					{
						AddActivityLog("Item", text, ActivityTypes.Add, sqlTransaction);
					}
				}
				if (flag)
				{
					UpdateTableRowInsertUpdateInfo("Product", "ProductID", text, sqlTransaction, !isUpdate);
				}
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Product, text, "Product", "ProductID", sqlTransaction);
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

		public bool InsertUpdateProductLotReceivingDetail(DataSet data, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				bool flag = true;
				SqlCommand insertUpdateProductLotReceivingDetailCommand = GetInsertUpdateProductLotReceivingDetailCommand(isUpdate: false);
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				insertUpdateProductLotReceivingDetailCommand.Transaction = sqlTransaction;
				if (data.Tables.Contains("Product_Lot_Receiving_Detail") && data.Tables["Product_Lot_Receiving_Detail"].Rows.Count > 0)
				{
					flag &= Insert(data, "Product_Lot_Receiving_Detail", insertUpdateProductLotReceivingDetailCommand);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdateProductLotIssueDetail(DataSet data, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				bool flag = true;
				SqlCommand insertUpdateProductLotIssueDetailCommand = GetInsertUpdateProductLotIssueDetailCommand(isUpdate: false);
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				insertUpdateProductLotIssueDetailCommand.Transaction = sqlTransaction;
				if (data.Tables.Contains("Product_Lot_Issue_Detail") && data.Tables["Product_Lot_Issue_Detail"].Rows.Count > 0)
				{
					flag &= Insert(data, "Product_Lot_Issue_Detail", insertUpdateProductLotIssueDetailCommand);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal decimal GetProductCurrentCost(string productID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT AverageCost \r\n\t\t\t\t\t\t   FROM Product  WHERE ProductID='" + productID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return decimal.Parse(obj.ToString());
			}
			return 0m;
		}

		public decimal GetProductLastCost(string productID, DateTime date)
		{
			string text = CommonLib.ToSqlDateTimeString(date);
			string exp = "SELECT  TOP 1 ISNULL(AverageCost, 0) AS Cost  FROM Inventory_Transactions \r\n                    where ProductID='" + productID + "' AND TransactionDate <='" + text + "'   ORDER BY TransactionDate  DESC";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return decimal.Parse(obj.ToString());
			}
			return 0m;
		}

		internal decimal GetProductLastSalesCOGS(string productID, string customerID, decimal quantity, SqlTransaction sqlTransaction)
		{
			if (quantity == 0m)
			{
				return 0m;
			}
			quantity = Math.Abs(quantity);
			string textCommand = "SELECT TOP 15 InvoiceNumber,SOldQty,Cost FROM Product_Lot_Sales p1\r\n\t\t\t\t\t\t\t\t\tWhere ItemCode='" + productID + "' AND InvoiceNumber IN\r\n\t\t\t\t\t\t\t\t\t(Select InvoiceNumber from Sales_Invoice WHERE CustomerID='" + customerID + "L-Y001') \r\n\t\t\t\t\t\t\t\t\tGroup by InvoiceNumber,SOldQty,DocID,Cost\r\n\t\t\t\t\t\t\t\t\tORDER BY InvoiceNumber DESC";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "product", textCommand);
			decimal result = default(decimal);
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				decimal num = decimal.Parse(row["SoldQty"].ToString());
				decimal d = decimal.Parse(row["Cost"].ToString());
				if (num > quantity)
				{
					result += quantity * d;
					quantity = default(decimal);
				}
				else
				{
					result += num * d;
					quantity -= num;
				}
				if (quantity == 0m)
				{
					break;
				}
			}
			if (quantity > 0m)
			{
				decimal productCurrentCost = GetProductCurrentCost(productID, sqlTransaction);
				result += quantity * productCurrentCost;
			}
			return result;
		}

		internal decimal GetAverageCost(string productID, SqlTransaction sqlTransaction)
		{
			decimal result = default(decimal);
			try
			{
				result = decimal.Parse(ExecuteSelectScalar("Product", "ProductID", productID, "AverageCost", sqlTransaction).ToString());
				return result;
			}
			catch
			{
				return result;
			}
		}

		internal float GetReservedQuantity(string productID, SqlTransaction sqlTransaction)
		{
			float result = 0f;
			try
			{
				result = float.Parse(ExecuteSelectScalar("Product", "ProductID", productID, "ReservedQuantity", sqlTransaction).ToString());
				return result;
			}
			catch
			{
				return result;
			}
		}

		internal float GetOrderedQuantity(string productID, SqlTransaction sqlTransaction)
		{
			float result = 0f;
			try
			{
				result = float.Parse(ExecuteSelectScalar("Product", "ProductID", productID, "OrderedQuantity", sqlTransaction).ToString());
				return result;
			}
			catch
			{
				return result;
			}
		}

		internal float GetTotalQuantity(string productID, SqlTransaction sqlTransaction)
		{
			float result = 0f;
			try
			{
				result = float.Parse(ExecuteSelectScalar("Product", "ProductID", productID, "Quantity", sqlTransaction).ToString());
				return result;
			}
			catch
			{
				return result;
			}
		}

		internal float GetLocationQuantity(string productID, string locationID, SqlTransaction sqlTransaction)
		{
			float result = 0f;
			try
			{
				string exp = "SELECT Quantity FROM Product_Location WHERE ProductID='" + productID + "' AND LocationID='" + locationID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return result;
				}
				if (!(obj.ToString() != ""))
				{
					return result;
				}
				result = float.Parse(obj.ToString());
				return result;
			}
			catch
			{
				return result;
			}
		}

		internal decimal GetUnitCost(int productID, SqlTransaction sqlTransaction)
		{
			decimal result = default(decimal);
			try
			{
				result = decimal.Parse(ExecuteSelectScalar("Product", "ProductID", productID, "StandardCost", sqlTransaction).ToString());
				return result;
			}
			catch
			{
				return result;
			}
		}

		internal CostMethods GetCostMethod(string productID, SqlTransaction sqlTransaction)
		{
			CostMethods result = CostMethods.Average;
			try
			{
				object obj = ExecuteSelectScalar("Product", "ProductID", productID, "CostMethod", sqlTransaction).ToString();
				if (obj == null)
				{
					return result;
				}
				result = (CostMethods)byte.Parse(obj.ToString());
				return result;
			}
			catch
			{
				return result;
			}
		}

		internal bool UpdateLocationQuantity(string productID, string locationID, float quantity, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(ProductID) FROM Product_Location WHERE ProductID='" + productID + "' AND LocationID='" + locationID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				exp = ((obj != null && !(obj.ToString() == "") && int.Parse(obj.ToString()) != 0) ? ("UPDATE Product_Location SET Quantity = " + quantity.ToString() + " WHERE ProductID='" + productID + "' AND LocationID='" + locationID + "'") : ("INSERT INTO Product_Location (ProductID,LocationID,Quantity) VALUES('" + productID + "','" + locationID + "'," + quantity.ToString() + ")"));
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateLocationQuantity(DataSet inventoryTransactionData, bool isReverse, SqlTransaction sqlTransaction)
		{
			try
			{
				bool flag = true;
				decimal d = 1m;
				if (isReverse)
				{
					d = -1m;
				}
				SqlCommand sqlCommand = new SqlCommand();
				DataTable dataTable = inventoryTransactionData.Tables["Inventory_Transactions"];
				string text = "";
				foreach (DataRow row in dataTable.Rows)
				{
					ItemTypes itemTypes = (ItemTypes)byte.Parse(row["ItemType"].ToString());
					if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly || itemTypes == ItemTypes.ConsignmentItem || itemTypes == ItemTypes.Inventory3PL)
					{
						string text2 = row["ProductID"].ToString();
						string text3 = row["LocationID"].ToString();
						decimal num = default(decimal);
						if (row["Quantity"] != null)
						{
							num = decimal.Parse(row["Quantity"].ToString());
						}
						text = "\n  IF Exists (SELECT * FROM Product_Location PL WHERE ProductID = '" + text2 + "' AND LocationID =  '" + text3 + "')\r\n\t\t\t\t\t\t\t\t\t\t UPDATE Product_Location SET Quantity = Quantity + " + d * num + " WHERE ProductID = '" + text2 + "' AND LocationID = '" + text3 + "'\r\n\t\t\t\t\t\t\t\t\t\t ELSE\r\n\t\t\t\t\t\t\t\t\t\t INSERT INTO Product_Location (ProductID,LocationID,Quantity) VALUES('" + text2 + "','" + text3 + "'," + num + ") ";
						if (text.Trim() != "")
						{
							sqlCommand = new SqlCommand(text, base.DBConfig.Connection);
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.Transaction = sqlTransaction;
							flag &= (sqlCommand.ExecuteNonQuery() > 0);
						}
					}
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateReservedQuantity(string productID, float quantity, SqlTransaction sqlTransaction)
		{
			string exp = "UPDATE Product SET ReservedQuantity=" + decimal.Parse(quantity.ToString()) + " WHERE ProductID='" + productID + "'";
			return ExecuteNonQuery(exp, sqlTransaction) > 0;
		}

		internal bool UpdateOrderedQuantity(string productID, float quantity, SqlTransaction sqlTransaction)
		{
			string exp = "UPDATE Product SET OrderedQuantity=" + quantity.ToString() + " WHERE ProductID='" + productID + "'";
			return ExecuteNonQuery(exp, sqlTransaction) > 0;
		}

		internal bool UpdateTotalQuantity(DataSet productData, SqlTransaction sqlTransaction)
		{
			try
			{
				bool flag = true;
				SqlCommand sqlCommand = new SqlCommand();
				DataTable dataTable = productData.Tables["Product"];
				string text = "";
				foreach (DataRow row in dataTable.Rows)
				{
					string str = row["ProductID"].ToString();
					string str2 = row["TotalQuantity"].ToString();
					string text2 = row["LastCost"].ToString();
					string text3 = row["AverageCost"].ToString();
					ItemTypes itemTypes = (ItemTypes)int.Parse(row["ItemType"].ToString());
					if (itemTypes == ItemTypes.Assembly || itemTypes == ItemTypes.ConsignmentItem || itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Inventory3PL)
					{
						text = text + "\n  UPDATE Product SET Quantity = " + str2;
						if (text2 != "")
						{
							text = text + ", LastCost = " + text2;
						}
						if (text3 != "")
						{
							text = text + ", AverageCost = " + text3;
						}
						text = text + " WHERE ProductID = '" + str + "' ";
					}
				}
				if (text != "")
				{
					sqlCommand = new SqlCommand(text, base.DBConfig.Connection);
					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.Transaction = sqlTransaction;
					flag &= (sqlCommand.ExecuteNonQuery() > 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteUnmatchingUnallocatedLotRows(string fromItemCode, string toItemCode)
		{
			bool result = true;
			try
			{
				string text = "DELETE  FROM  Unallocated_Lot_Items  WHERE ProductID+LocationID IN  (\r\n                            Select ItemCode+LocationID FROM (SELECT *, onhand-lotqty-unallocated as Diff from (\r\n                            SELECT ItemCode,LocationID, SUM(LotQty- (SoldQty+ISNULL(ReturnedQty,0))) AS LotQty , \r\n                            (SELECT SUM(Quantity) FROM Product_Location P WHERE P.ProductID = PL.ItemCode AND P.LocationID = PL.LocationID) \r\n                            AS Onhand,(SELECT ISNULL(SUM(Quantity),0) FROM Unallocated_Lot_Items UL WHERE UL.ProductID = PL.ItemCode AND UL.LocationID = PL.LocationID) AS Unallocated FROM Product_Lot PL group by itemcode,locationid\r\n                            ) x WHERE onhand-lotqty-unallocated <> 0) X2 WHERE Onhand = 0 AND Unallocated>0 AND LotQty=0   ";
				if (fromItemCode != "")
				{
					text = text + " AND ItemCode Between '" + fromItemCode + "' AND '" + toItemCode + "'";
				}
				text += " )";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				result = (ExecuteNonQuery(text, sqlTransaction) > 0);
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

		internal bool UpdateTotalQuantity(string productID, float quantity, SqlTransaction sqlTransaction)
		{
			string exp = "UPDATE Product SET Quantity=" + quantity.ToString() + " WHERE ProductID='" + productID + "'";
			return ExecuteNonQuery(exp, sqlTransaction) > 0;
		}

		internal bool UpdateAverageCost(string productID, decimal avgCost, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			avgCost = Math.Round(avgCost, currencyDecimalPoints);
			string exp = "UPDATE Product SET AverageCost=" + avgCost.ToString() + " WHERE ProductID='" + productID + "'";
			flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
			exp = "UPDATE Product_Lot SET AvgCost=" + avgCost.ToString() + " WHERE ItemCode='" + productID + "'\r\n\t\t\t\t\t\t  AND (LotQty- ISNULL(SoldQty,0) - ISNULL(ReturnedQty,0))>0";
			return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
		}

		internal ItemTypes GetItemType(string itemID, SqlTransaction sqlTransaction)
		{
			object obj = ExecuteSelectScalar("Product", "ProductID", itemID, "ItemType", sqlTransaction);
			ItemTypes result = ItemTypes.Inventory;
			if (obj != null)
			{
				try
				{
					result = (ItemTypes)byte.Parse(obj.ToString());
					return result;
				}
				catch
				{
					return result;
				}
			}
			return result;
		}

		internal ProductUnitTypes GetUnitType(int productID, int unitID, SqlTransaction sqlTransaction)
		{
			object obj = ExecuteSelectScalar("Product", "ProductID", "UnitID", productID, unitID, "ProductID", sqlTransaction);
			if (obj != DBNull.Value && obj != null)
			{
				return ProductUnitTypes.MainUnit;
			}
			return ProductUnitTypes.None;
		}

		internal float GetUnitFactor(int productID, SqlTransaction sqlTransaction)
		{
			object obj = ExecuteSelectScalar("Product", "ProductID", productID, "Factor", sqlTransaction);
			if (obj != DBNull.Value && obj != null)
			{
				return float.Parse(obj.ToString());
			}
			return 1f;
		}

		public ProductData GetProduct()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product");
			ProductData productData = new ProductData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productData, "Product", sqlBuilder);
			return productData;
		}

		public bool DeleteProduct(string productID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM UDF_Product  WHERE EntityID = '" + productID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Product_Unit  WHERE ProductID = '" + productID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Product WHERE ProductID = '" + productID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Product_Parts_Detail WHERE ProductID = '" + productID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Product_Substitute_Detail WHERE ProductID = '" + productID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Product_AppliedModels_Detail WHERE ProductID = '" + productID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Product_PriceList_Detail WHERE ProductID = '" + productID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Item", productID, ActivityTypes.Delete, null);
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

		public bool GetProductTransactionsExists(string productID)
		{
			try
			{
				base.DBConfig.StartNewTransaction();
				new SqlCommand();
				string exp = "SELECT COUNT(*) FROM (\r\n\r\n                             SELECT DISTINCT PRODUCTID FROM PURCHASE_ORDER_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PURCHASE_QUOTE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_ORDER_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_QUOTE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT BOMProductID  FROM ASSEMBLY_BUILD_DETAIL WHERE BOMPRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM W3PL_GRN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM W3PL_INVOICE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM W3PL_DELIVERY_DETAIL WHERE PRODUCTID = '" + productID + "' \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SERVICE_PARTSREPLACED_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALESPROFORMA_INVOICE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_RETURN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_RECEIPT_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_QUOTE_DETAIL_HISTORY WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_QUOTE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_POS_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_ORDER_PRODUCTLOT_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_ORDER_DETAIL WHERE PRODUCTID = '" + productID + "' \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_MAN_TARGET_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_INVOICE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_FORECASTING_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM SALES_ENQUIRY_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PURCHASE_RETURN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PURCHASE_RECEIPT_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PURCHASE_QUOTE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PURCHASE_ORDER_NONINV_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PURCHASE_ORDER_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PURCHASE_INVOICE_NONINV_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PURCHASE_INVOICE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PROJECT_SUBCONTRACT_PO_DETAIL WHERE PRODUCTID = '" + productID + "' \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PROJECT_SUBCONTRACT_PI_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PRODUCT_SUBSTITUTE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PRODUCT_PRICELIST_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PRODUCT_PRICE_BULK_UPDATE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PRODUCT_PARTS_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PRODUCT_LOT_RECEIVING_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PRODUCT_LOT_ISSUE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PRODUCT_CATEGORY_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PRODUCT_APPLIEDMODELS_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PRICE_LIST_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM POS_HOLD_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM PO_SHIPMENT_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT ITEMID\t  FROM PHYSICAL_STOCK_ENTRY_DETAIL WHERE ITEMID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM OPENING_BALANCE_BATCH_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM MATERIAL_RESERVATION_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM MFG_WORK_ORDER_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM JOB_MATERIAL_REQUISITION_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM JOB_MATERIAL_ESTIMATE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM JOB_INVENTORY_RETURN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM JOB_INVENTORY_ISSUE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM JOB_ESTIMATION_DETAIL_ITEM_HISTORY WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM JOB_ESTIMATION_DETAIL_ITEM WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM JOB_BOM_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM ITEM_TRANSACTION_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM INVENTORY_TRANSFER_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM INVENTORY_REPACKING_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM INVENTORY_DISMANTLE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM INVENTORY_DAMAGE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM INVENTORY_ADJUSTMENT_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM GRN_RETURN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM GARMENT_RENTAL_RETURN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM GARMENT_RENTAL_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM EXPORT_PICKLIST_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM EXPORT_PACKINGLIST_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM EA_WORKORDER_RESOURCES_DETAIL WHERE PRODUCTID = '" + productID + "' \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM EA_WORKORDER_INVENTORY_RETURN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM EA_WORKORDER_INVENTORY_ISSUE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM EA_MOBILIZATION_RESOURCES__DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM DELIVERY_RETURN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM DELIVERY_NOTE_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM CONSIGNOUT_SETTLEMENT_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM CONSIGNOUT_RETURN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM CONSIGNIN_SETTLEMENT_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM CONSIGNIN_RETURN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM CONSIGN_OUT_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM CONSIGN_IN_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                             UNION ALL  SELECT DISTINCT PRODUCTID FROM BOM_DETAIL WHERE PRODUCTID = '" + productID + "'  \r\n                            ) AS P";
				object obj = ExecuteScalar(exp);
				if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result: true);
			}
			return false;
		}

		public ProductData GetProductByID(string id)
		{
			ProductData productData = new ProductData();
			id = AddSingleQuote(id);
			string textCommand = "SELECT *,CAST((SELECT TOP 1 IT.AverageCost FROM Inventory_Transactions IT WHERE IT.Quantity > 0 AND IT.ProductId=P1.ProductID ORDER BY IT.TransactionDate Desc) AS DECIMAL(18,5))AS CurrentAvgCost,  \r\n                            ISNULL((select Top 1 CASE WHEN FactorType='D' THEN  PID.UnitPrice/PID.UnitFactor\r\n                            WHEN FactorType='M'  THEN  PID.UnitPrice*PID.UnitFactor\r\n                            ELSE PID.UnitPrice END AS UnitPrice from Purchase_Invoice_Detail PID \r\n                            INNER JOIN Purchase_Invoice PI ON PI.SysDocID=PID.SysDocID AND PI.VoucherID=PID.VoucherID INNER JOIN Product P11 ON PID.ProductID=P11.ProductID \r\n                             where p11.ProductID=P1.ProductID order by TransactionDate desc),0) AS LastPurchaseCost, CASE WHEN ISNULL((SELECT Count(P3.ProductID)\r\n\t\t\t\t\t\t\tFROM Product   P3 WHERE P3.Photo IS Not NULL AND P3.ProductID=P1.ProductID ),0) = 0 THEN \r\n\t\t\t\t\t\t\tISNULL((SELECT Count(P3.ProductParentID)\r\n\t\t\t\t\t\t\tFROM Product_Parent P3 WHERE P3.Photo IS Not NULL AND P3.ProductParentID=P1.MatrixParentID),0) ELSE \r\n\t\t\t\t\t\t\t(SELECT Count(P2.ProductID)\r\n\t\t\t\t\t\t\tFROM Product   P2 WHERE P2.Photo IS Not NULL AND P2.ProductID=P1.ProductID )  END AS HasPhoto FROM Product P1 WHERE ProductID = '" + id + "'";
			FillDataSet(productData, "Product", textCommand);
			textCommand = " SELECT * FROM Product_Unit WHERE ProductID = '" + id + "'";
			FillDataSet(productData, "Product_Unit", textCommand);
			textCommand = " SELECT * FROM Product_Parts_Detail WHERE ProductID = '" + id + "'";
			FillDataSet(productData, "Product_Parts_Detail", textCommand);
			textCommand = " SELECT * FROM Product_AppliedModels_Detail WHERE ProductID = '" + id + "'";
			FillDataSet(productData, "Product_AppliedModels_Detail", textCommand);
			textCommand = " SELECT * FROM Product_Substitute_Detail WHERE ProductID = '" + id + "'";
			FillDataSet(productData, "Product_Substitute_Detail", textCommand);
			textCommand = " SELECT * FROM Product_PriceList_Detail WHERE ProductID = '" + id + "'";
			FillDataSet(productData, "Product_PriceList_Detail", textCommand);
			textCommand = "SELECT * FROM UDF_Product WHERE EntityID = '" + id + "'";
			FillDataSet(productData, "UDF", textCommand);
			return productData;
		}

		public DataSet GetSaleProductByID(string id, string CustomerID)
		{
			DataSet dataSet = new DataSet();
			id = AddSingleQuote(id);
			string textCommand = "SELECT *,CAST((SELECT TOP 1 IT.AssetValue/IT.Quantity FROM Inventory_Transactions IT WHERE IT.Quantity > 0 AND IT.ProductId=P.ProductID ORDER BY IT.TransactionDate Desc) AS DECIMAL(10,4)) AS CurrentAvgCost, \r\n                            (SELECT TOP 1 IT1.UnitPrice FROM Inventory_Transactions IT1 WHERE IT1.ProductID = P1.ProductID AND IT1.PayeeID=IT.PayeeID AND TransactionType = 2  ORDER BY TransactionDate DESC,TransactionID DESC) AS LastSalePrice, CASE WHEN ISNULL((SELECT Count(P3.ProductID)\r\n\t\t\t\t\t\t\tFROM Product   P3 WHERE P3.Photo IS Not NULL AND P3.ProductID=P1.ProductID ),0) = 0 THEN \r\n\t\t\t\t\t\t\tISNULL((SELECT Count(P3.ProductParentID)\r\n\t\t\t\t\t\t\tFROM Product_Parent P3 WHERE P3.Photo IS Not NULL AND P3.ProductParentID=P1.MatrixParentID),0) ELSE \r\n\t\t\t\t\t\t\t(SELECT Count(P2.ProductID)\r\n\t\t\t\t\t\t\tFROM Product   P2 WHERE P2.Photo IS Not NULL AND P2.ProductID=P1.ProductID )  END AS HasPhoto FROM Product P1 LEFT JOIN Inventory_Transactions IT ON P1.ProductID=IT.ProductID WHERE P1.ProductID = '" + id + "' AND IT.PayeeID='" + CustomerID + "'";
			FillDataSet(dataSet, "Product", textCommand);
			return dataSet;
		}

		public DataSet GetProductByFields(params string[] columns)
		{
			return GetProductByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductByFields(string[] productID, params string[] columns)
		{
			return GetProductByFields(productID, isInactive: true, columns);
		}

		public DataSet GetProductByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product");
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
				commandHelper.FieldName = "ProductID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductList(bool inActive, bool showZeroBalance, string locationID)
		{
			string text = "";
			string exp = "SELECT ConditionalQuery FROM Card_Security WHERE UserID = '" + base.DBConfig.UserID + "' OR GroupID IN (SELECT GroupID FROM User_Group_Detail WHERE UserID = '" + base.DBConfig.UserID + "') Order by GroupID Desc";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				text = obj.ToString();
			}
			DataSet dataSet = new DataSet();
			string str = "\r\n                            \r\n                            SELECT * INTO #TEMP_MAIN  FROM (\r\n                            SELECT  IT.ProductID,IT.AverageCost,IT.TransactionDate,\r\n                            ROW_NUMBER() OVER (PARTITION BY IT.ProductID ORDER BY IT.TransactionDate DESC)  AS [R]\r\n                            FROM Inventory_Transactions IT WHERE IT.Quantity > 0 ) AXO_Product WHERE AXO_Product.R=1\r\n\r\n                            SELECT *  INTO #Axo_Product_Aging from Axo_Product_Aging\r\n\r\n                            Create Index #IND ON #Axo_Product_Aging (ProductID)             \r\n\r\n                            SELECT Product.IsInactive [I], CASE WHEN ISNULL((SELECT Count(P2.ProductID)\r\n\t\t\t\t\t\t\tFROM Product   P2 WHERE P2.Photo IS Not NULL AND P2.ProductID=Product.ProductID ),0) = 0 THEN \r\n\t\t\t\t\t\t\tISNULL((SELECT Count(P3.ProductParentID)\r\n\t\t\t\t\t\t\tFROM Product_Parent P3 WHERE P3.Photo IS Not NULL AND P3.ProductParentID=Product.MatrixParentID),0) ELSE \r\n\t\t\t\t\t\t\t(SELECT Count(P2.ProductID)\r\n\t\t\t\t\t\t\tFROM Product   P2 WHERE P2.Photo IS Not NULL AND P2.ProductID=Product.ProductID ) END AS P,\t'' AS F,\t\t\t\t\r\n\t\t\t\t\t\t\tProduct.ProductID [Item Code],MatrixParentID AS [Matrix Code],\r\n\t\t\t\t\t\t\tDescription,Product.Description2 AS ALIAS, Product.ItemType [Type],UnitPrice1,UnitPrice2,UnitPrice3,MinPrice,\r\n                           --CAST((SELECT TOP 1 IT.AverageCost  FROM Inventory_Transactions IT WHERE IT.Quantity > 0 AND IT.ProductId=Product.ProductID ORDER BY IT.TransactionDate Desc) AS DECIMAL(18,5)) AS [Avg Cost],\r\n                            CAST((SELECT AverageCost  FROM #TEMP_MAIN IT WHERE  IT.ProductId=Product.ProductID ) AS DECIMAL(18,5)) AS [Avg Cost],\r\n                            Product.UnitID AS Unit,\r\n                            CategoryName [Category],\r\n                            (SELECT DATEDIFF(Day,  Max(TransactionDate),GetDate()) FROM Inventory_Transactions IT WHERE IT.ProductID = Product.ProductID AND TransactionType = 2) AS LastSale,\r\n                            Attribute1,Attribute2,Attribute3,VendorRef, QuantityPerUnit,\r\n\t\t\t\t\t\t\tWeight,ReorderLevel,CountryName AS Origin,Size,Style.StyleName AS Style, PCL.ClassName AS Class,\r\n\t\t\t\t\t\t\tBrandName AS Brand,Age,ManufacturerID Manufacturer,UPC,ROUND(SUM(ISNULL(PL.Quantity,0)) ,2) AS  [Onhand],\r\n                           --ROUND( Product.AverageCost * (SUM(ISNULL(PL.Quantity,0))),2) AS Value ,\r\n                            CAST((SELECT AverageCost  FROM #TEMP_MAIN IT WHERE  IT.ProductId=Product.ProductID) AS DECIMAL(18,5))* (SUM(ISNULL(PL.Quantity,0))) AS Value,\r\n                            (SUM(ISNULL(PL.Quantity,0)))* Weight AS TotalWeight\r\n                            FROM Product LEFT OUTER JOIN Product_Location PL ON Product.ProductID=PL.ProductID \r\n                            LEFT OUTER JOIN Product_Category PC ON PC.CategoryID = Product.CategoryID\r\n                            LEFT OUTER JOIN Country CN ON CN.CountryID = Product.Origin\r\n                            LEFT OUTER JOIN Location Loc ON Loc.LocationID = PL.LocationID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Style Style ON Style.StyleID = Product.StyleID\r\n                            LEFT OUTER JOIN Product_Class PCL ON PCL.ClassID = Product.ClassID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Brand Brand ON Brand.BrandID = Product.BrandID\r\n                            LEFT OUTER JOIN #Axo_Product_Aging Aging ON Aging.ProductID = Product.ProductID\r\n                            \r\n\r\n\t\t\t\t\t\t\tWHERE 1=1 AND ISNULL(IsConsignOutLocation,'False')='False' ";
			if (!inActive)
			{
				str += " AND ISNULL(Product.IsInactive,'false') = 'false' ";
			}
			if (locationID != "")
			{
				str = str + " AND PL.LocationID='" + locationID + "' ";
			}
			if (text != "")
			{
				str = str + " AND Product." + text;
			}
			str += " GROUP BY Product.ProductID ,PL.ProductID,Weight,ReorderLevel,\r\n\t\t\t\t\t\t\tDescription,Product.Description2,Product.ItemType  ,Product.AverageCost  ,CategoryName  ,StyleName,BrandName  , Attribute1,Attribute2,Attribute3,UnitPrice1,UnitPrice2,UnitPrice3,MinPrice,MatrixParentID,VendorRef,QuantityPerUnit,\r\n\t\t\t\t\t\t\tAttribute,[Size],CountryName ,ManufacturerID  ,UPC,Product.IsInactive,Age,PCL.ClassName, Product.UnitID  ";
			if (!showZeroBalance)
			{
				str += "  HAVING ROUND(SUM(ISNULL(PL.Quantity,0)),2) <> 0 ";
			}
			str += " UNION\r\n\t\t\t\t\t\tSELECT PP.IsInactive [I], ISNULL((SELECT Count(P2.ProductParentID)\r\n\t\t\t\t\t\t\tFROM Product_Parent   P2 WHERE P2.Photo IS Not NULL AND P2.ProductParentID=PP.ProductParentID ),0) AS P,'' AS F,ProductParentID AS [Item Code],'' AS [Matrix Code], Description,'' AS ALIAS,\r\n                        PP.ItemType AS [Type],NULL AS UnitPrice1,NULL AS UnitPrice2,NULL AS UnitPrice3,NULL AS MinPrice, \r\n\t\t\t\t\t\tNULL AS [Avg Cost], NULL AS Unit, CategoryName [Category],NULL AS LastSale,'' AS Attribute1,'' AS Attribute2, '' AS Attribute3,'' AS VendorRef, NULL AS QuantityPerUnit,NULL AS Weight,NULL AS ReorderLevel,\r\n\t\t\t\t\t\tCountryName AS Origin,'' AS [Size],'' AS Style,PCL.ClassName AS Class, BrandID Brand,NULL AS Age,ManufacturerID Manufacturer,LookupCode UPC, NULL AS Onhand, NULL AS Value, NULL as  [Total Weight]\r\n\t\t\t\t\t\tFROM Product_Parent PP LEFT OUTER JOIN Product_Category PC ON PC.CategoryID = PP.CategoryID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Class PCL ON PCL.ClassID = PP.ClassID\r\n                         LEFT OUTER JOIN Country CN ON CN.CountryID = PP.Origin\r\n\t\t\t\t\t\t WHERE 1=1  ";
			if (!inActive)
			{
				str += " AND ISNULL(PP.IsInactive,'false') = 'false' ";
			}
			str += " ORDER BY [Item Code],Description ";
			str += " DROP TABLE  #Axo_Product_Aging   ";
			str += " DROP TABLE   #TEMP_MAIN   ";
			FillDataSet(dataSet, "Product", str);
			DataSet entityAssignedFlagsList = new EntityFlag(base.DBConfig).GetEntityAssignedFlagsList(EntityTypesEnum.Items);
			dataSet.Merge(entityAssignedFlagsList);
			return dataSet;
		}

		public DataSet GetPriceLevelComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ISNULL(ItemPrice1Name,'Unit Price 1') AS [0],ISNULL(ItemPrice2Name,'Unit Price 2') AS [1],ISNULL(ItemPrice3Name,'Unit Price 3') AS [2]\r\n\t\t\t\t\t\t   FROM Company WHERE CompanyID=1";
			FillDataSet(dataSet, "PriceLevel", textCommand);
			return dataSet;
		}

		public DataSet GetPriceListNames()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ItemPrice1Name,ItemPrice2Name,ItemPrice3Name\r\n\t\t\t\t\t\t   FROM Company WHERE CompanyID=1";
			FillDataSet(dataSet, "Product", textCommand);
			return dataSet;
		}

		public byte[] GetProductComboList()
		{
			DataSet dataSet = new DataSet();
			string exp = "SELECT DefaultInventoryLocationID FROM Users WHERE UserID = '" + base.DBConfig.UserID + "'";
			object obj = ExecuteScalar(exp);
			string text = "";
			string text2 = "";
			if (obj != null)
			{
				text = obj.ToString();
			}
			bool flag = false;
			string exp2 = "SELECT OptionValue FROM Company_Option WHERE OptionID = '" + 161 + "'";
			object obj2 = ExecuteScalar(exp2);
			if (obj2 != null)
			{
				flag = bool.Parse(obj2.ToString());
			}
			string str = "";
			string text3 = "";
			string str2 = "";
			if (flag)
			{
				text3 = "CAST((SELECT TOP 1 IT.AssetValue/IT.Quantity FROM Inventory_Transactions IT WHERE IT.Quantity > 0 AND IT.ProductId=P.ProductID ORDER BY IT.TransactionDate Desc) AS DECIMAL(10,4))AS AverageCost,";
			}
			else
			{
				text3 = " P.AverageCost,";
				str2 = " P.AverageCost,";
			}
			exp = "SELECT ConditionalQuery FROM Card_Security WHERE UserID = '" + base.DBConfig.UserID + "' OR GroupID IN (SELECT GroupID FROM User_Group_Detail WHERE UserID = '" + base.DBConfig.UserID + "') Order by GroupID Desc";
			obj = ExecuteScalar(exp);
			if (obj != null)
			{
				text2 = obj.ToString();
			}
			string str3 = ("SELECT P.ProductID [Code],P.Description [Name],UnitPrice1 Price,P.UnitID,P.ItemType,P.Attribute1,P.Attribute2,P.Attribute3,P.ClassID,P.StandardCost,\r\n                           P.MatrixParentID,P.BOMID,P.UPC AS UPC,REPLACE(P.UPC,'-','') AS SearchUPC,CONCAT(P.ProductID,'',P.UPC) AS SearchGridValue," + text3 + " CASE WHEN P.ItemType = 5 THEN 'True' ELSE ISNULL(P.IsTrackLot,'False') END AS IsTrackLot, \r\n                            ISNULL(P.IsTrackSerial,'False') AS IsTrackSerial, Weight,ISNULL(SUM(PL.Quantity),0) AS Quantity,P.TaxOption, P.TaxGroupID  as TaxGroupID,P.IsPriceEmbedded,LEN(Description) as NameLength,LEN(P.ProductID) as CodeLength \r\n\t\t\t\t\t\t   FROM Product P LEFT OUTER JOIN Product_Location PL ON P.ProductID = PL.ProductID   " + str) ?? "";
			if (text != "")
			{
				str3 = str3 + " AND PL.LocationID = '" + text + "' ";
			}
			str3 += " WHERE ISNULL(IsInactive,'False') = 'False'";
			if (text2 != "")
			{
				str3 = str3 + " AND P." + text2;
			}
			str3 = str3 + "   GROUP BY P.ProductID,P.Description  ,P.UnitID,P.ItemType,UnitPrice1,P.Attribute1,P.Attribute2,P.Attribute3,P.ClassID,P.StandardCost,P.MatrixParentID,P.BOMID,P.UPC," + str2 + "P.IsTrackLot, P.IsTrackSerial,P.Weight,P.TaxOption,P.TaxGroupID,P.IsPriceEmbedded\r\n\r\n\t\t\t\t\t\t   UNION\r\n\r\n\t\t\t\t\t\t   SELECT PP.ProductParentID AS [Code] ,Description [Name],0 AS Price, UnitID,  6 ItemType,'' AS Attribute1, '' AS Attribute2, '' AS Attribute3, ClassID, 0 AS StandardCost,\r\n                                '' AS MatrixParentID,NULL AS BOMID,'' AS UPC,'' AS SearchUPC,'' AS SearchGridValue, 0.0 AS AverageCost, 'False' AS IsTrackLot,'False' AS IsTrackSerial, 0 AS Weight,0 AS Quantity,0 AS TaxOption,''  as TaxGroupID,'False' AS IsPriceEmbedded,LEN(Description) as NameLength,LEN(PP.ProductParentID) as CodeLength \r\n\t\t\t\t\t\t   FROM Product_Parent PP WHERE ParentType = 1 AND ISNULL(IsInactive,'False') = 'False'\r\n\r\n\t\t\t\t\t\t   ORDER BY Code,P.Description";
			FillDataSet(dataSet, "Product", str3);
			str3 = "select PU.UnitID AS Code, U.UnitName AS Name,ProductID,FactorType,Factor,IsMainUnit--,( select 1/Factor FROM Product_Unit WHERE ProductID=Pu.ProductID ) AS Fraction\r\n                                FROM Product_Unit PU INNER JOIN Unit U ON PU.UnitID = U.UnitID\r\n\r\n                                UNION \r\n                                SELECT  P.UnitID AS Code,U.UnitName AS Name,ProductID,'M' AS FactorType,1 AS Factor, 'True' AS IsMainUnit--,1 AS Fraction\r\n                                FROM Product P  INNER JOIN Unit U ON P.Unitid = U.UnitID\r\n                                WHERE ProductID IN (SELECT DISTINCT ProductID FROM Product_Unit) \r\n\r\n                                ORDER BY ProductID,IsMainUnit desc,Code";
			FillDataSet(dataSet, "Product_Unit", str3);
			dataSet.RemotingFormat = SerializationFormat.Binary;
			return CommonLib.CompressDataSet(dataSet);
		}

		public DataSet GetProductPriceList(string productID, string customerID, string LocationID, string UnitID)
		{
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			DataSet dataSet3 = new DataSet();
			string str = "SELECT '' AS UnitID,ISNULL(PPD.UnitPrice1,P.UnitPrice1)AS UnitPrice1,ISNULL(PPD.UnitPrice2,P.UnitPrice2)AS UnitPrice2,ISNULL(PPD.UnitPrice3,P.UnitPrice3)AS \r\n                        UnitPrice3,ISNULL(PPD.MinPrice,P.MinPrice)AS MinPrice FROM Product_PriceList_Detail PPD  LEFT JOIN Product P ON \r\n                        PPD.ProductID=P.ProductID WHERE PPD.ProductID='" + productID + "'";
			str = str + " AND (PPD.LocationID = '" + LocationID + "' OR ISNULL(PPD.LocationID,'')='')";
			if (UnitID != "")
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_PriceList_Detail", "UnitID", "ProductID", productID, "UnitID", UnitID, null);
				UnitID = ((fieldValue == null) ? "" : fieldValue.ToString());
			}
			if (UnitID != "")
			{
				str = str + " AND PPD.UnitID = '" + UnitID + "'";
			}
			FillDataSet(dataSet2, "PriceList", str);
			if (dataSet2.Tables[0].Rows.Count > 0)
			{
				dataSet2.Tables[0].Rows[0][0] = UnitID;
			}
			if (dataSet2.Tables[0].Rows.Count == 0 || UnitID == "" || customerID == "")
			{
				dataSet2.Tables.Clear();
				str = "SELECT UnitID,UnitPrice1,UnitPrice2,UnitPrice3,MinPrice FROM Product WHERE ProductID='" + productID + "'";
				FillDataSet(dataSet2, "PriceList", str);
			}
			str = "SELECT ItemPrice1Name,ItemPrice2Name,ItemPrice3Name\r\n\t\t\t\t\t\t   FROM Company WHERE CompanyID=1";
			FillDataSet(dataSet3, "PriceName", str);
			dataSet.Tables.Add();
			dataSet.Tables[0].Columns.Add("PriceID");
			dataSet.Tables[0].Columns.Add("Price Name");
			dataSet.Tables[0].Columns.Add("Amount", typeof(decimal));
			dataSet.Tables[0].Columns.Add("UnitID");
			if (dataSet2.Tables[0].Rows[0]["UnitPrice1"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["UnitPrice1"].ToString() != "")
			{
				dataSet.Tables[0].Rows.Add("UnitPrice1", dataSet3.Tables[0].Rows[0]["ItemPrice1Name"].ToString(), dataSet2.Tables[0].Rows[0]["UnitPrice1"].ToString());
			}
			if (dataSet2.Tables[0].Rows[0]["UnitPrice2"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["UnitPrice2"].ToString() != "")
			{
				dataSet.Tables[0].Rows.Add("UnitPrice2", dataSet3.Tables[0].Rows[0]["ItemPrice2Name"].ToString(), dataSet2.Tables[0].Rows[0]["UnitPrice2"].ToString());
			}
			if (dataSet2.Tables[0].Rows[0]["UnitPrice3"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["UnitPrice3"].ToString() != "")
			{
				dataSet.Tables[0].Rows.Add("UnitPrice3", dataSet3.Tables[0].Rows[0]["ItemPrice3Name"].ToString(), dataSet2.Tables[0].Rows[0]["UnitPrice3"].ToString());
			}
			if (dataSet2.Tables[0].Rows[0]["MinPrice"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["MinPrice"].ToString() != "")
			{
				dataSet.Tables[0].Rows.Add("MinPrice", "Min Price", dataSet2.Tables[0].Rows[0]["MinPrice"].ToString());
			}
			if (dataSet2.Tables[0].Rows[0]["UnitID"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["UnitID"].ToString() != "")
			{
				dataSet.Tables[0].Rows[0]["UnitID"] = dataSet2.Tables[0].Rows[0]["UnitID"].ToString();
			}
			if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Price Name"].ToString() == "")
			{
				dataSet.Tables[0].Rows[0]["Price Name"] = "Price";
			}
			if (dataSet.Tables[0].Rows.Count > 1 && dataSet.Tables[0].Rows[1]["Price Name"].ToString() == "")
			{
				dataSet.Tables[0].Rows[1]["Price Name"] = "Price";
			}
			if (dataSet.Tables[0].Rows.Count > 2 && dataSet.Tables[0].Rows[2]["Price Name"].ToString() == "")
			{
				dataSet.Tables[0].Rows[2]["Price Name"] = "Price";
			}
			return dataSet;
		}

		public DataSet GetAllProductPriceList()
		{
			DataSet dataSet = new DataSet();
			new DataSet();
			new DataSet();
			string textCommand = " SELECT ProductID, Description, ISNULL(UnitPrice1, 0) AS UnitPrice, UnitID FROM Product";
			FillDataSet(dataSet, "Price_List_Detail", textCommand);
			return dataSet;
		}

		public decimal GetProductSalesPrice(string productID, string customerID, string locationID, string UnitID)
		{
			string str = "SELECT ISNULL(PPD.UnitPrice1,P.UnitPrice1)AS UnitPrice1,ISNULL(PPD.UnitPrice2,P.UnitPrice2)AS UnitPrice2,ISNULL(PPD.UnitPrice3,P.UnitPrice3)AS \r\n                        UnitPrice3,ISNULL(PPD.MinPrice,P.MinPrice)AS MinPrice FROM Product_PriceList_Detail PPD  LEFT JOIN Product P ON \r\n                        PPD.ProductID=P.ProductID WHERE PPD.ProductID='" + productID + "'";
			str = str + " AND (PPD.LocationID = '" + locationID + "' OR ISNULL(PPD.LocationID,'')='')";
			if (UnitID != "")
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_PriceList_Detail", "UnitID", "ProductID", productID, "UnitID", UnitID, null);
				UnitID = ((fieldValue == null) ? "" : fieldValue.ToString());
			}
			if (UnitID != "")
			{
				str = str + " AND PPD.UnitID = '" + UnitID + "'";
			}
			str += "ORDER BY PPD.LocationID DESC";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "PriceLevel", str);
			if (dataSet.Tables["PriceLevel"].Rows.Count == 0 || UnitID == "")
			{
				dataSet.Tables.Clear();
				str = "SELECT UnitPrice1,UnitPrice2,UnitPrice3,MinPrice FROM Product WHERE ProductID='" + productID + "'";
				FillDataSet(dataSet, "PriceLevel", str);
			}
			string a = "";
			if (customerID != "")
			{
				a = new Databases(base.DBConfig).GetFieldValue("Customer", "PriceLevelID", "CustomerID", customerID, null).ToString();
			}
			if ((a == "" || a == "0") && dataSet.Tables[0].Rows[0]["UnitPrice1"] != DBNull.Value)
			{
				return decimal.Parse(dataSet.Tables[0].Rows[0]["UnitPrice1"].ToString());
			}
			if (a == "1" && dataSet.Tables[0].Rows[0]["UnitPrice2"] != DBNull.Value)
			{
				return decimal.Parse(dataSet.Tables[0].Rows[0]["UnitPrice2"].ToString());
			}
			if (a == "2" && dataSet.Tables[0].Rows[0]["UnitPrice3"] != DBNull.Value)
			{
				return decimal.Parse(dataSet.Tables[0].Rows[0]["UnitPrice3"].ToString());
			}
			if (a == "3" && dataSet.Tables[0].Rows[0]["MinPrice"] != DBNull.Value)
			{
				return decimal.Parse(dataSet.Tables[0].Rows[0]["MinPrice"].ToString());
			}
			return 0m;
		}

		public DataSet GetProductSalesPriceDesc(string productID, string customerID, string unitID)
		{
			string text = CommonLib.ToSqlDateTimeString(DateTime.Now);
			string textCommand = "SELECT UnitPrice, PLT.Description, UnitPrice1,UnitPrice2,UnitPrice3,MinPrice \r\n                            FROM Product LEFT OUTER JOIN (\r\n\t\t\t\t\t                SELECT TOP 1 ProductID, Description, UnitPrice FROM Price_List_Detail PLD\r\n\t\t\t\t\t                INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID\r\n\t\t\t\t\t                WHERE PLD.ProductID = '" + productID + "' AND PL.CustomerID = '" + customerID + "' AND PLD.UnitID ='" + unitID + "' AND ISNULL(PL.Inactive, 1) <> 1 AND ValidDateTo >= '" + text + "' ORDER BY TransactionDate DESC ) AS PLT ON Product.ProductID = PLT.ProductID WHERE Product.ProductID = '" + productID + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "PriceLevel", textCommand);
			if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["UnitPrice"] != DBNull.Value)
			{
				return dataSet;
			}
			string a = "";
			if (customerID != "")
			{
				a = new Databases(base.DBConfig).GetFieldValue("Customer", "PriceLevelID", "CustomerID", customerID, null).ToString();
			}
			if (a == "" && dataSet.Tables[0].Rows[0]["UnitPrice1"] != DBNull.Value)
			{
				dataSet.Tables[0].Rows[0]["UnitPrice"] = dataSet.Tables[0].Rows[0]["UnitPrice1"];
			}
			else if (a == "1" && dataSet.Tables[0].Rows[0]["UnitPrice2"] != DBNull.Value)
			{
				dataSet.Tables[0].Rows[0]["UnitPrice"] = dataSet.Tables[0].Rows[0]["UnitPrice2"].ToString();
			}
			else if (a == "2" && dataSet.Tables[0].Rows[0]["UnitPrice3"] != DBNull.Value)
			{
				dataSet.Tables[0].Rows[0]["UnitPrice"] = dataSet.Tables[0].Rows[0]["UnitPrice3"].ToString();
			}
			else if (a == "3" && dataSet.Tables[0].Rows[0]["MinPrice"] != DBNull.Value)
			{
				dataSet.Tables[0].Rows[0]["UnitPrice"] = dataSet.Tables[0].Rows[0]["MinPrice"].ToString();
			}
			else
			{
				dataSet.Tables[0].Rows[0]["UnitPrice"] = 0;
			}
			return dataSet;
		}

		public decimal GetProductPurchasePrice(string productID)
		{
			string exp = "SELECT LastCost FROM Product WHERE ProductID='" + productID + "'";
			new DataSet();
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return decimal.Parse(obj.ToString());
			}
			return 0m;
		}

		public DataSet GetProductUnitComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT UnitID [Code],'' [Name],ProductID,FactorType,Factor\r\n\t\t\t\t\t\t   FROM Product_Unit ORDER BY ProductID";
			FillDataSet(dataSet, "Product_Unit", textCommand);
			return dataSet;
		}

		public float GetProductQuantity(string productID, string locationID)
		{
			string text = "SELECT SUM(ISNULL(Quantity,0)) AS Quantity\r\n\t\t\t\t\t\t   FROM Product_Location WHERE ProductID='" + productID + "' ";
			if (locationID != "")
			{
				text = text + " AND LocationID='" + locationID + "'";
			}
			object obj = ExecuteScalar(text);
			if (obj != null && obj.ToString() != "")
			{
				return float.Parse(obj.ToString());
			}
			return 0f;
		}

		public byte[] GetProductThumbnailImage(string productID)
		{
			string exp = "SELECT ISNULL(Photo, (SELECT Photo FROM Product_Parent PP WHERE ProductParentID = P.MatrixParentID)) AS Photo\r\n\t\t\t\t\t\t   FROM Product P WHERE ProductID='" + productID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return (byte[])obj;
			}
			return null;
		}

		private bool ThumbnailImageAbort()
		{
			return true;
		}

		public DataSet GetProductQuantityAndCost(string productID, string locationID)
		{
			string textCommand = "SELECT PL.Quantity , P.AverageCost\r\n\t\t\t\t\t\t   FROM Product_Location PL LEFT OUTER JOIN Product P ON P.ProductID=PL.ProductID WHERE PL.ProductID='" + productID + "' AND PL.LocationID='" + locationID + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Product", textCommand);
			return dataSet;
		}

		public decimal GetLastSaleTransationByCustomerID(string productID, string customerID)
		{
			SqlCommand sqlCommand = new SqlCommand();
			string text2 = sqlCommand.CommandText = "SELECT TOP 1 SID.UnitPrice AS UnitPrice  FROM Sales_Invoice_Detail SID  \r\n            INNER JOIN Sales_Invoice SI ON SI.SysDocID=SID.SysDocID AND SI.VoucherID=SID.VoucherID\r\n            WHERE SID.ProductID= @ProductID AND SI.CustomerID = @CustomerID ORDER BY SI.TransactionDate desc";
			sqlCommand.Parameters.AddWithValue("@CustomerID", customerID);
			sqlCommand.Parameters.AddWithValue("@ProductID", productID);
			sqlCommand.Connection = base.DBConfig.Connection;
			object obj = sqlCommand.ExecuteScalar();
			if (obj != null && obj.ToString() != "")
			{
				return decimal.Parse(obj.ToString());
			}
			return 0m;
		}

		public DataSet GetProductQuantityAndCostAsOfDate(string[] productIDs, string locationID, DateTime date)
		{
			try
			{
				if (productIDs.Length == 0 || string.IsNullOrWhiteSpace(locationID))
				{
					return null;
				}
				string text = CommonLib.ToSqlDateTimeString(date);
				string text2 = CommonLib.ToSqlArrayString(productIDs);
				string textCommand = " SELECT DISTINCT IT.ProductID, ISNULL((SELECT SUM(IT2.Quantity) \r\n\t\t\t\t\t\t\t\t FROM Inventory_Transactions IT2 WHERE IT.ProductID = IT2.ProductID \r\n\t\t\t\t\t\t\t\t\tAND IT2.LocationID = '" + locationID + "' AND TransactionDate <= '" + text + "'),0)  AS Quantity,  ISNULL( ITMax.AverageCost,0)  AS AverageCost\r\n\t\t\t\t\t\t\t\t FROM Inventory_Transactions IT\r\n\t\t\t\t\t\t\t\t INNER JOIN\r\n\t\t\t\t\t\t\t\t (\r\n                                    SELECT ProductID,AverageCost, Max(TransactionDate) TDate,Rank() Over (Partition by ProductID ORDER BY TransactiONDate DESC) AS RNK   FROM Inventory_Transactions  \r\n\t\t\t\t\t\t\t\t    WHERE TransactionDate <= '" + text + "'  \r\n\t\t\t\t\t\t\t\t    GROUP BY ProductID,AverageCost ,TransactionDate \r\n                                 ) ITMax ON IT.ProductID = ITMax.ProductID \r\n\t\t\t\t\t\t\t\t \r\n                                 WHERE RNK = 1 AND IT.ProductID IN (" + text2 + ") AND  IT.AverageCost IS NOT NULL\r\n\t\t\t\t\t\t\t\t ORDER BY ProductID ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductAvailability(string productID, string unitID)
		{
			string text = "SELECT PL.LocationID AS [Location Code],LocationName AS [Location Name],";
			text = ((!(unitID != "")) ? (text + "Quantity AS 'Onhand' ") : (text + " CASE (SELECT PU.Factor FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') WHEN 1 THEN Quantity \r\n\t\t\t\t\t\t\tELSE CASE (SELECT PU.FactorType FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') WHEN 'M'\r\n\t\t\t\t\t\t\tTHEN Quantity * (SELECT PU.Factor FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') ELSE\r\n\t\t\t\t\t\t\tQuantity / (SELECT PU.Factor FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') END  END AS 'Onhand' "));
			text = text + " FROM Product_Location PL INNER JOIN Location LOC ON LOC.LocationID = PL.LocationID WHERE PL.ProductID='" + productID + "' AND Quantity<>0";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Product", text);
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.MaterialReservationOnSo, false).ToString());
			if (flag)
			{
				text = "SELECT  SUM(RD.Quantity)AS TotalReserved, '" + flag.ToString() + "' AS isReserved FROM Reservation_Detail RD WHERE ProductID = '" + productID + "' AND Quantity<> 0";
				FillDataSet(dataSet, "ProductReserverd", text);
			}
			return dataSet;
		}

		internal DataRow GetProductUnitRow(string productID, string unitID)
		{
			string textCommand = "SELECT *\r\n\t\t\t\t\t\t   FROM Product_Unit PU WHERE PU.ProductID='" + productID + "' AND PU.UnitID='" + unitID + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Product_Unit", textCommand);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				return dataSet.Tables[0].Rows[0];
			}
			return null;
		}

		public bool AddProductPhoto(string productID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Product SET Photo=@Photo WHERE ProductID='" + productID + "'");
				sqlCommand.Parameters.AddWithValue("@Photo", image);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
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

		public bool RemoveProductPhoto(string productID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Product SET Photo= Null WHERE ProductID='" + productID + "'");
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
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

		public DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, bool showitemwithTansactions, bool showinactiveitems)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				if (fromWarehouse != "" || toWarehouse != "")
				{
					text3 = " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				string str = "SELECT DISTINCT IT.ProductID [Item Code] ,Product.Description AS [Item Description],\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID " + text3 + " AND IT2.TransactionDate<'" + text + "'),0) AS OpeningQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity*IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<'" + text + "'),0) AS OpeningValue,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<='" + text2 + "'),0) AS ClosingQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity * IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<='" + text2 + "'),0) AS ClosingValue                \r\n\t\t\t        FROM Inventory_Transactions IT INNER JOIN Product ON IT.ProductID=Product.ProductID             \r\n                                         WHERE ";
				str = str + " Product.ItemType NOT IN ('3') AND TransactionDate < '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND IT.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "') ";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += " GROUP BY IT.ProductID,Product.Description ORDER BY IT.ProductID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", str);
				DataSet dataSet2 = new DataSet();
				str = "SELECT 'OPENING STOCK ' SysDocID,'0000000' VoucherID,'0' SysDocType, '" + text + "' AS TransactionDate,IT.ProductID,Product.Description,'' AS LocationID, 0 AS UnitPrice, 0 AS AverageCost,'' AS Reference ,\r\n                                  CASE  WHEN  SUM(ISNULL(IT.Quantity, 0) )> 0 THEN SUM(ISNULL(IT.Quantity, 0) ) ELSE 0 END AS [Qty In],\r\n                                  CASE  WHEN SUM(ISNULL(IT.Quantity, 0) )< 0 THEN -1 * SUM(ISNULL(IT.Quantity, 0) ) ELSE 0 END AS [Qty Out], 0 AssetValue,\r\n                                  'OPENING STOCK'  AS PayeeName\r\n                                  From Inventory_Transactions IT INNER JOIN Product ON Product.ProductID=IT.ProductID \r\n                                  LEFt OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                  Vendor ON IT.PayeeID=Vendor.VendorID                                \r\n                                  WHERE  convert(date, transactionDate) < '" + text + "'";
				if (fromWarehouse != "")
				{
					str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "') ";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += "GROUP BY IT.ProductID,Product.Description  UNION ALL ";
				str = str + "SELECT SysDocID,VoucherID,SysDocType,CAST(TransactionDate AS DATE) as TransactionDate,IT.ProductID,Product.Description,LocationID,UnitPrice,IT.AverageCost,Reference ,\r\n\t\t\t\t\tCASE  WHEN IT.Quantity > 0 THEN ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty In],\r\n\t\t\t\t\tCASE  WHEN IT.Quantity < 0 THEN -1 * ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty Out],AssetValue,\r\n\t\t\t\t\tPayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName END) AS PayeeName\r\n\t\t\t\t\tFrom Inventory_Transactions IT INNER JOIN Product ON Product.ProductID=IT.ProductID \r\n\t\t\t\t\tLEFt OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\tVendor ON IT.PayeeID=Vendor.VendorID \r\n\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "') ";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += " ORDER BY TransactionDate, VoucherID";
				FillDataSet(dataSet2, "Inventory_Transactions", str);
				dataSet.Merge(dataSet2);
				dataSet.Tables["Inventory_Transactions"].Columns.Add("Balance");
				foreach (DataRow row in dataSet.Tables["Product"].Rows)
				{
					int num = Convert.ToInt32(decimal.Parse(row["OpeningQuantity"].ToString()));
					string str2 = row["Item Code"].ToString();
					string filterExpression = "ProductID='" + str2 + "' ";
					DataRow[] array = dataSet.Tables["Inventory_Transactions"].Select(filterExpression);
					foreach (DataRow obj2 in array)
					{
						int.TryParse(obj2["Qty Out"].ToString(), out int result);
						int.TryParse(obj2["Qty In"].ToString(), out int result2);
						obj2["Balance"] = (num = num + result2 - result);
					}
				}
				dataSet.Relations.Add("Balance Detail", dataSet.Tables["Product"].Columns["Item Code"], dataSet.Tables["Inventory_Transactions"].Columns["ProductID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromJob, string toJob, string fromCostCategory, string toCostCategory, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				if (fromWarehouse != "" || toWarehouse != "")
				{
					_ = " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				string text3 = "SELECT JIID.SysDocID, JIID.VoucherID, JIID.ProductID, JIID.JobID, JIID.LocationID, JIID.CostCategoryID, JIID.Description, JIID.RowIndex,JIID.Quantity, IT.AverageCost AS Cost, JIID.Quantity * IT.AverageCost AS Amount, \r\n                                Product.CategoryID, JobName, CostCategoryName, IT.TransactionDate, (SELECT DISTINCT SysDocType FROM Inventory_Transactions IT WHERE IT.SysDocID = JIID.SysDocID AND IT.VoucherID = JIID.VoucherID) AS SysDocType                              \r\n                                FROM Job_Inventory_Issue_Detail AS JIID \r\n                                INNER JOIN Inventory_Transactions IT ON JIID.SysDocID=IT.SysDocID AND JIID.VoucherID=IT.VoucherID AND JIID.ProductID=IT.ProductID\r\n                                INNER JOIN Job_Inventory_Issue JII ON JIID.SysDocID = JII.SysDocID AND JIID.VoucherID = JII.VoucherID \r\n                                INNER JOIN Product ON JIID.ProductID = Product.ProductID\r\n                                LEFT OUTER JOIN Job J ON JIID.JobID = J.JobID\r\n                                LEFT OUTER JOIN Job_Cost_Category JCC ON JIID.CostCategoryID = JCC.CostCategoryID                                 \r\n                                WHERE JIID.JobID <> NULL OR JIID.JobID <> '' ";
				text3 = text3 + " AND IT.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					text3 = text3 + " AND JIID.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND JIID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND JIID.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "'";
				}
				if (fromCostCategory != "")
				{
					text3 = text3 + " AND JIID.CostCategoryID BETWEEN '" + fromCostCategory + "' AND '" + toCostCategory + "'";
				}
				text3 += " UNION ";
				text3 += "SELECT  JIRD.SysDocID, JIRD.VoucherID, JIRD.ProductID, JIRD.JobID, JIRD.LocationID, JIRD.CostCategoryID, JIRD.Description,JIRD.RowIndex, (JIRD.Quantity ) * -1 , \r\n                        JIRD.Cost AS Cost, (JIRD.Quantity * JIRD.Cost )*-1 AS Amount\r\n                        , Product.CategoryID, JobName, CostCategoryName, TransactionDate, (SELECT DISTINCT SysDocType FROM Inventory_Transactions IT WHERE IT.SysDocID = JIRD.SysDocID AND IT.VoucherID = JIRD.VoucherID) AS SysDocType  \r\n                        FROM Job_Inventory_Return_Detail AS JIRD \r\n                        INNER JOIN Job_Inventory_Return JIR ON JIRD.SysDocID = JIR.SysDocID AND JIRD.VoucherID = JIR.VoucherID   \r\n                        INNER JOIN Product ON JIRD.ProductID = Product.ProductID\r\n                        LEFT OUTER JOIN Job J ON JIRD.JobID = J.JobID\r\n                        LEFT OUTER JOIN Job_Cost_Category JCC ON JIRD.CostCategoryID = JCC.CostCategoryID\r\n                        WHERE JIRD.JobID <> NULL OR JIRD.JobID <> '' ";
				text3 = text3 + " AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					text3 = text3 + " AND JIRD.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND JIRD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND JIRD.ProductID IN (SELECT DISTINCT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND JIRD.ProductID IN (SELECT DISTINCT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND JIRD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND JIRD.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "'";
				}
				if (fromCostCategory != "")
				{
					text3 = text3 + " AND JIRD.CostCategoryID BETWEEN '" + fromCostCategory + "' AND '" + toCostCategory + "'";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Inventory_Transactions", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromJob, string toJob, bool showitemwithTansactions, bool showinactiveitems, bool excludeInventoryTransfer, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				if (fromWarehouse != "" || toWarehouse != "")
				{
					text3 = " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				string str = "SELECT DISTINCT IT.ProductID [Item Code] ,Product.Description AS [Item Description],\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID " + text3 + " AND IT2.TransactionDate<'" + text + "'),0) AS OpeningQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity*IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<'" + text + "'),0) AS OpeningValue,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<='" + text2 + "'),0) AS ClosingQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity * IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<='" + text2 + "'),0) AS ClosingValue\r\n                   \r\n\t\t\t        FROM Inventory_Transactions IT INNER JOIN Product ON IT.ProductID=Product.ProductID \r\n                    \r\n                   WHERE ";
				str = str + " Product.ItemType NOT IN ('3') AND TransactionDate < '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND IT.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromJob != "")
				{
					str = str + " AND IT.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "')";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "'";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += " GROUP BY IT.ProductID,Product.Description ORDER BY IT.ProductID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", str);
				DataSet dataSet2 = new DataSet();
				str = "SELECT 'OPENING STOCK ' SysDocID,'0000000' VoucherID,'0' SysDocType, '" + text + "' AS TransactionDate,IT.ProductID,Product.Description,'' AS LocationID, 0 AS UnitPrice, 0 AS AverageCost,'' AS Reference ,\r\n                                '' AS JobID,'' AS JobName,'' AS CostCategoryName, '' AS CostCategoryID,''AS IssueRefrence,'' AS IssueDescription,\r\n                                  CASE  WHEN  SUM(ISNULL(IT.Quantity, 0) )> 0 THEN SUM(ISNULL(IT.Quantity, 0) ) ELSE 0 END AS [Qty In],\r\n                                  CASE  WHEN SUM(ISNULL(IT.Quantity, 0) )< 0 THEN -1 * SUM(ISNULL(IT.Quantity, 0) ) ELSE 0 END AS [Qty Out], 0 AssetValue,\r\n                                  'OPENING STOCK'  AS PayeeName\r\n                                  From Inventory_Transactions IT INNER JOIN Product ON Product.ProductID=IT.ProductID \r\n                                  LEFt OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                  Vendor ON IT.PayeeID=Vendor.VendorID \r\n                                 LEFT OUTER JOIN Job J ON IT.JobID = J.JobID\r\n                                    LEFT OUTER JOIN Job_Cost_Category JCC ON IT.CostCategoryID = JCC.CostCategoryID                                   \r\n                                  WHERE  convert(date, transactionDate) < '" + text + "'";
				if (fromWarehouse != "")
				{
					str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromJob != "")
				{
					str = str + " AND IT.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "')";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "'";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				if (excludeInventoryTransfer)
				{
					str += " AND IT.SysDocType NOT IN('19', '20', '21')";
				}
				str += "GROUP BY IT.ProductID,Product.Description  UNION ALL ";
				str = str + "SELECT SysDocID,VoucherID,SysDocType,CAST(TransactionDate AS DATE) as TransactionDate,IT.ProductID,Product.Description,LocationID,UnitPrice,IT.AverageCost,IT.Reference ,IT.JobID,JobName,JCC.CostCategoryName, IT.CostCategoryID,ISNULL((SELECT 'Issue No'+'-'+VoucherID FROM Inventory_Transfer WHERE AcceptSysDocID=IT.SysDocID AND  AcceptVoucherID=IT.VoucherID),\r\n            (SELECT 'Reject No'+'-'+VoucherID FROM Inventory_Transfer WHERE RejectAcceptSysDocID=IT.SysDocID AND  RejectAcceptVoucherID=IT.VoucherID)) AS IssueRefrence,(SELECT Inventory_Transfer.Description FROM Inventory_Transfer WHERE Inventory_Transfer.SysDocID=IT.SysDocID AND  Inventory_Transfer.VoucherID=IT.VoucherID) AS IssueDescription,\r\n\t\t\t\t\tCASE  WHEN IT.Quantity > 0 THEN ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty In],\r\n\t\t\t\t\tCASE  WHEN IT.Quantity < 0 THEN -1 * ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty Out],AssetValue,\r\n\t\t\t\t\tPayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName END) AS PayeeName\r\n\t\t\t\t\tFrom Inventory_Transactions IT INNER JOIN Product ON Product.ProductID=IT.ProductID \r\n\t\t\t\t\tLEFt OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\tVendor ON IT.PayeeID=Vendor.VendorID \r\n                    LEFT OUTER JOIN Job J ON IT.JobID = J.JobID\r\n                    LEFT OUTER JOIN Job_Cost_Category JCC ON IT.CostCategoryID = JCC.CostCategoryID\r\n\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromJob != "")
				{
					str = str + " AND IT.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "')";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "'";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				if (excludeInventoryTransfer)
				{
					str += " AND IT.SysDocType NOT IN('19', '20', '21')";
				}
				str += " ORDER BY TransactionDate, VoucherID";
				FillDataSet(dataSet2, "Inventory_Transactions", str);
				dataSet.Merge(dataSet2);
				dataSet.Tables["Inventory_Transactions"].Columns.Add("Balance");
				foreach (DataRow row in dataSet.Tables["Product"].Rows)
				{
					int num = Convert.ToInt32(decimal.Parse(row["OpeningQuantity"].ToString()));
					string str2 = row["Item Code"].ToString();
					string filterExpression = "ProductID='" + str2 + "' ";
					DataRow[] array = dataSet.Tables["Inventory_Transactions"].Select(filterExpression);
					foreach (DataRow obj2 in array)
					{
						int.TryParse(obj2["Qty Out"].ToString(), out int result);
						int.TryParse(obj2["Qty In"].ToString(), out int result2);
						obj2["Balance"] = (num = num + result2 - result);
					}
				}
				dataSet.Relations.Add("Balance Detail", dataSet.Tables["Product"].Columns["Item Code"], dataSet.Tables["Inventory_Transactions"].Columns["ProductID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryTransactionLotwiseReport(DateTime from, DateTime to, DateTime fromDate, DateTime toDate, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, bool? value, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = CommonLib.ToSqlDateTimeString(fromDate);
				string text4 = CommonLib.ToSqlDateTimeString(toDate);
				if (fromWarehouse != "" || toWarehouse != "")
				{
					_ = " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				string str = "SELECT DISTINCT IT.ProductID [Item Code] , P.Description AS [Item Description],\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  AND IT2.TransactionDate<'1-1-1990 0:0:0'),0) AS OpeningQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity*IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID   AND IT2.TransactionDate<'1-1-1990 0:0:0'),0) AS OpeningValue,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID   AND IT2.TransactionDate<='1-1-2990 23:59:59'),0) AS ClosingQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity * IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID   AND IT2.TransactionDate<='1-1-2990 23:59:59'),0) AS ClosingValue\r\n                      \r\n\t\t\t              FROM Inventory_Transactions IT INNER JOIN Product P ON IT.ProductID = P.ProductID \r\n\t\t\t\t\t      INNER JOIN Product_Lot PL ON P.ProductID = PL.ItemCode\r\n                             WHERE ";
				str = str + " P.ItemType NOT IN ('3') AND TransactionDate < '" + text2 + "' ";
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "" && fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (value.HasValue && value == true)
				{
					str = str + " AND ProductionDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
				}
				else if (value.HasValue && value == false)
				{
					str = str + " AND ExpiryDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
				}
				str += " GROUP BY IT.ProductID, P.Description ORDER BY IT.ProductID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", str);
				DataSet dataSet2 = new DataSet();
				str = "SELECT SysDocID,VoucherID,SysDocType,TransactionDate,IT.ProductID,P.Description,IT.LocationID,UnitPrice,\r\n                    IT.AverageCost, IT.Reference,\r\n                    CASE  WHEN ALT.LotQty  > 0 THEN ISNULL(ALT.LOTQty, 0) ELSE 0 END AS [Qty In],\r\n                    CASE  WHEN ALT.LotQty  < 0 THEN -1 * ISNULL(ALT.LotQty, 0) ELSE 0 END AS [Qty Out]\r\n                    ,AssetValue, PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName END) AS PayeeName\r\n                    , P.CategoryID, ALT.LotNumber , ALT.ProductionDate, ALT.ExpiryDate, ALT.Reference \r\n                    FROM                Inventory_Transactions IT \r\n                    INNER JOIN            Product P ON P.ProductID=IT.ProductID\r\n                    INNER JOIN            AXO_Lot_Lotledger ALT ON IT.SysDocID = ALT.DocID AND IT.VoucherID = ALT.ReceiptNumber\r\n                    AND IT.ProductID = ALT.ItemCode AND IT.RowIndex = ALT.RowIndex and IT.LocationID = ALT.LocationID \r\n                    LEFT OUTER JOIN        Customer ON IT.PayeeID=Customer.CustomerID \r\n                    LEFT OUTER JOIN        Vendor ON IT.PayeeID=Vendor.VendorID                                   \r\n\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND IT.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "" && fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (value.HasValue && value == true)
				{
					str = str + " AND ALT.ProductionDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
				}
				else if (value.HasValue && value == false)
				{
					str = str + " AND ALT.ExpiryDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
				}
				str += " ORDER BY TransactionDate";
				FillDataSet(dataSet2, "Inventory_Transactions", str);
				dataSet.Merge(dataSet2);
				dataSet.Tables["Inventory_Transactions"].Columns.Add("Balance");
				foreach (DataRow row in dataSet.Tables["Product"].Rows)
				{
					int num = Convert.ToInt32(decimal.Parse(row["OpeningQuantity"].ToString()));
					string str2 = row["Item Code"].ToString();
					string filterExpression = "ProductID='" + str2 + "' ";
					DataRow[] array = dataSet.Tables["Inventory_Transactions"].Select(filterExpression);
					foreach (DataRow obj2 in array)
					{
						int.TryParse(obj2["Qty Out"].ToString(), out int result);
						int.TryParse(obj2["Qty In"].ToString(), out int result2);
						obj2["Balance"] = (num = num + result2 - result);
					}
				}
				dataSet.Relations.Add("Balance Detail", dataSet.Tables["Product"].Columns["Item Code"], dataSet.Tables["Inventory_Transactions"].Columns["ProductID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryTransactionLotwiseReport(DateTime from, DateTime to, DateTime fromDate, DateTime toDate, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromJob, string toJob, bool? value)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = CommonLib.ToSqlDateTimeString(fromDate);
				string text4 = CommonLib.ToSqlDateTimeString(toDate);
				if (fromWarehouse != "" || toWarehouse != "")
				{
					_ = " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				string str = "SELECT DISTINCT IT.ProductID [Item Code] , P.Description AS [Item Description],IT.JobID, JobName,JCC.CostCategoryName, IT.CostCategoryID,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  AND IT2.TransactionDate<'1-1-1990 0:0:0'),0) AS OpeningQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity*IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID   AND IT2.TransactionDate<'1-1-1990 0:0:0'),0) AS OpeningValue,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID   AND IT2.TransactionDate<='1-1-2990 23:59:59'),0) AS ClosingQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity * IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID   AND IT2.TransactionDate<='1-1-2990 23:59:59'),0) AS ClosingValue\r\n\t\t\t        FROM Inventory_Transactions IT INNER JOIN Product P ON IT.ProductID = P.ProductID \r\n\t\t\t\t\t INNER JOIN Product_Lot PL ON P.ProductID = PL.ItemCode\r\n                     LEFT OUTER JOIN Job J ON IT.JobID = J.JobID\r\n                    LEFT OUTER JOIN Job_Cost_Category JCC ON IT.CostCategoryID = JCC.CostCategoryID\r\n                    WHERE ";
				str = str + " P.ItemType NOT IN ('3') AND TransactionDate < '" + text2 + "' ";
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromJob != "")
				{
					str = str + " AND IT.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				if (value.HasValue && value == true)
				{
					str = str + " AND ProductionDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
				}
				else if (value.HasValue && value == false)
				{
					str = str + " AND ExpiryDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
				}
				str += " GROUP BY IT.ProductID, P.Description,IT.JobID, JobName,JCC.CostCategoryName, IT.CostCategoryID ORDER BY IT.ProductID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", str);
				DataSet dataSet2 = new DataSet();
				str = "SELECT SysDocID,VoucherID,SysDocType,TransactionDate,IT.ProductID,P.Description,IT.LocationID,UnitPrice,IT.AverageCost, IT.Reference,IT.JobID, JobName,JCC.CostCategoryName, IT.CostCategoryID,\r\n\t\t\t\t\t    CASE  WHEN IT.Quantity > 0 THEN ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty In],\r\n\t\t\t\t\t    CASE  WHEN IT.Quantity < 0 THEN -1 * ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty Out],AssetValue,\r\n\t\t\t\t\t    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName END) AS PayeeName, P.CategoryID, PL.LotNumber, ProductionDate, ExpiryDate\r\n\t\t\t\t\t    FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID=IT.ProductID\r\n\t\t\t\t\t    INNER JOIN Product_Lot PL ON P.ProductID = PL.ItemCode  \r\n\t\t\t\t\t    LEFT OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t    Vendor ON IT.PayeeID=Vendor.VendorID  \r\n                        LEFT OUTER JOIN Job J ON IT.JobID = J.JobID\r\n                        LEFT OUTER JOIN Job_Cost_Category JCC ON IT.CostCategoryID = JCC.CostCategoryID\r\n\t\t\t\t\t    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND IT.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromJob != "")
				{
					str = str + " AND IT.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				if (value.HasValue && value == true)
				{
					str = str + " AND ProductionDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
				}
				else if (value.HasValue && value == false)
				{
					str = str + " AND ExpiryDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
				}
				str += " ORDER BY TransactionDate";
				FillDataSet(dataSet2, "Inventory_Transactions", str);
				dataSet.Merge(dataSet2);
				dataSet.Tables["Inventory_Transactions"].Columns.Add("Balance");
				foreach (DataRow row in dataSet.Tables["Product"].Rows)
				{
					int num = Convert.ToInt32(decimal.Parse(row["OpeningQuantity"].ToString()));
					string str2 = row["Item Code"].ToString();
					string filterExpression = "ProductID='" + str2 + "' ";
					DataRow[] array = dataSet.Tables["Inventory_Transactions"].Select(filterExpression);
					foreach (DataRow obj2 in array)
					{
						int.TryParse(obj2["Qty Out"].ToString(), out int result);
						int.TryParse(obj2["Qty In"].ToString(), out int result2);
						obj2["Balance"] = (num = num + result2 - result);
					}
				}
				dataSet.Relations.Add("Balance Detail", dataSet.Tables["Product"].Columns["Item Code"], dataSet.Tables["Inventory_Transactions"].Columns["ProductID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesByItemCategorySummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "Select ISNULL(PR.CategoryID,'No Category') AS CategoryID,Cat.CategoryName,\r\n\t\t\t\t\t\t\tMIN(UnitPrice) AS MinPrice,\r\n\t\t\t\t\t\t\tMAX(UnitPrice) AS MaxPrice,\r\n\t\t\t\t\t\t\tSUM(SID.Quantity) AS SalesQuantity,\r\n\t\t\t\t\t\t\tSUM(ISNULL(SID.UnitQuantity,SID.Quantity)*UnitPrice)  AS SalesAmount\r\n\t\t\t\t\t\t\tFROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON \r\n\t\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Category Cat ON Cat.CategoryID=PR.CategoryID  ";
				text3 = text3 + "WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				text3 += " GROUP BY PR.CategoryID , Cat.CategoryName";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["CategoryID"]
				};
				DataSet dataSet2 = new DataSet();
				text3 = "Select ISNULL(PR.CategoryID,'No Category') AS CategoryID,Cat.CategoryName,\r\n\t\t\t\t\t\t\t-1 * SUM(SID.Quantity) AS ReturnQuantity,\r\n\t\t\t\t\t\t\tSUM(ISNULL(SID.UnitQuantity,SID.Quantity)*UnitPrice)  AS ReturnAmount\r\n\t\t\t\t\t\t\tFROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON \r\n\t\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Category Cat ON Cat.CategoryID=PR.CategoryID\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				text3 += " GROUP BY PR.CategoryID , Cat.CategoryName";
				FillDataSet(dataSet2, "Product", text3);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["CategoryID"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesByItemSummaryReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "Select X.ProductID,X.Description,MIN(X.UnitPrice) AS MinPrice,MAX(X.UnitPrice) AS MaxPrice,\r\n                                MIN(X.UnitF) AS MinPriceF,MAX(X.UnitF) AS MaxPriceF,SUM(X.Quantity) AS SalesQuantity,\r\n                                SUM(X.Quantity*X.UnitPrice) AS SalesAmount,SUM(X.Quantity*X.UnitF) AS SalesAmountF FROM\r\n                                (Select SID.ProductID,TransactionDate,SID.VoucherID,SI.CustomerID + '-' + CustomerName AS 'Customer',PR.Description,\r\n                                Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n                                SID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                                (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                                (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF                    \r\n                                FROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON    \r\n                                SID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                                INNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n                                Customer ON Customer.CustomerID=SI.CustomerID \r\n                                    ";
				text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += ") X GROUP BY X.ProductID,X.Description ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["ProductID"]
				};
				text3 = "Select SID.ProductID,PR.Description,\r\n\t\t\t\t\t\t\tMIN(UnitPrice) AS MinPrice,\r\n\t\t\t\t\t\t\tMAX(UnitPrice) AS MaxPrice,\r\n\t\t\t\t\t\t\tSUM(SID.Quantity) AS SalesQuantity,\r\n\t\t\t\t\t\t\tSUM(SID.Quantity*UnitPrice) AS SalesAmount                        \r\n\t\t\t\t\t\t\tFROM ConsignOut_Settlement_Detail SID INNER JOIN ConsignOut_Settlement SI ON \r\n\t\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID INNER JOIN Product PR ON SID.ProductID=PR.ProductID             \r\n                                    ";
				text3 = text3 + "WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " GROUP BY SID.ProductID,PR.Description ";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Product", text3);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["ProductID"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				dataSet2 = new DataSet();
				text3 = "Select X.ProductID,X.Description,MIN(X.UnitPrice) AS MinPrice,MAX(X.UnitPrice) AS MaxPrice,\r\n                        MIN(X.UnitF) AS MinPriceF,MAX(X.UnitF) AS MaxPriceF,SUM(X.Quantity) AS ReturnQuantity,\r\n                        SUM(X.Quantity*X.UnitPrice) AS ReturnAmount,SUM(X.Quantity*X.UnitF) AS ReturnAmountF FROM\r\n                        (Select SID.ProductID,TransactionDate,SID.VoucherID,SI.CustomerID + '-' + CustomerName AS 'Customer',PR.Description,\r\n                        Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n                        SID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF                   \r\n                        FROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON    \r\n                        SID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                        INNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n                        Customer ON Customer.CustomerID=SI.CustomerID                              \r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += ")X GROUP BY X.ProductID,X.Description";
				FillDataSet(dataSet2, "Product", text3);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["ProductID"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesByItemDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "Select DISTINCT SID.ProductID,Product.Description\r\n\t\t\t\t\t\t\tFROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON SID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID\r\n\t\t\t\t\t\t\tINNER JOIN Product ON SID.ProductID=Product.ProductID  ";
				text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(SI.IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " GROUP BY SID.ProductID,Product.Description ";
				text3 += " UNION ";
				text3 += "Select DISTINCT SID.ProductID,Product.Description\r\n\t\t\t\t\t\t\tFROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON SID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID\r\n\t\t\t\t\t\t\tINNER JOIN Product ON SID.ProductID=Product.ProductID \r\n                                     ";
				text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(SI.IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " GROUP BY SID.ProductID,Product.Description ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				DataSet dataSet2 = new DataSet();
				text3 = "Select SID.ProductID,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.CustomerID + '-' + CustomerName AS 'Customer',SID.Description,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n\t\t\t\t\t\tSID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF        \r\n\t\t\t\t\t\tFROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tCustomer ON Customer.CustomerID=SI.CustomerID                               \r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " UNION ALL ";
				text3 = text3 + "Select SID.ProductID,\r\n\t\t\t\t\t\tCAST(TransactionDate AS DATE) AS TransactionDate,SID.VoucherID,SI.CustomerID + '-' + CustomerName AS 'Customer',SID.Description,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type],\r\n\t\t\t\t\t\t-1*SID.Quantity,UnitPrice, (-1*SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                        (CASE WHEN SID.FactorType='D' THEN (-1*SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (-1*SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF\r\n\t\t\t\t\t\tFROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tCustomer ON Customer.CustomerID=SI.CustomerID                                   \r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += "ORDER BY SID.VoucherID,TransactionDate";
				FillDataSet(dataSet2, "Sales", text3);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Sales Detail", dataSet.Tables["Product"].Columns["ProductID"], dataSet.Tables["Sales"].Columns["ProductID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesByItemCustomerSalespersonReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string str = "Select SID.ProductID,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.CustomerID + '-' + CustomerName AS 'Customer', SI.SalespersonID + '-' + FullName AS 'Salesperson', SID.Description, PR.UnitID,Salesperson.ReportTo,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n\t\t\t\t\t\tSID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF,(SELECT BrandName FROM Product_Brand PB WHERE PR.BrandID =PB.BrandID)[Brand Name]\r\n\t\t\t\t\t\tFROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                        INNER JOIN System_Document SD ON SD.SysDocID=SID.SysDocID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tCustomer ON Customer.CustomerID=SI.CustomerID\r\n                        LEFT JOIN Salesperson ON Salesperson.SalespersonID = SI.SalespersonID                                  \r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (customerIDs != "")
				{
					str = str + " AND SI.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND (SI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' OR Customer.ParentCustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ) ";
				}
				if (fromCustomerClass != "")
				{
					str = str + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					str = str + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromCustomerArea != "")
				{
					str = str + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
				}
				if (fromCustomerCountry != "")
				{
					str = str + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
				}
				if (fromSalesperson != "")
				{
					str = str + " AND ( SI.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ) ";
				}
				if (fromSalespersonGroup != "")
				{
					str = str + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					str = str + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					str = str + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					str = str + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				if (fromLocation != "")
				{
					str = str + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				str += " UNION ALL ";
				str = str + "Select SID.ProductID,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.CustomerID + '-' + CustomerName AS 'Customer', SI.SalespersonID + '-' + FullName AS 'Salesperson', SID.Description,PR.UnitID,Salesperson.ReportTo,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type],\r\n\t\t\t\t\t\t-1*SID.Quantity,UnitPrice, (-1*SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitP,\r\n                        (CASE WHEN SID.FactorType='D' THEN (-1*SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (-1*SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (-1*SID.Quantity*UnitPrice) END) AS AmountF,'' AS [Brand Name]\r\n\t\t\t\t\t\tFROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                        INNER JOIN System_Document SD ON SD.SysDocID=SID.SysDocID\r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tCustomer ON Customer.CustomerID=SI.CustomerID\r\n                        LEFT JOIN Salesperson ON Salesperson.SalespersonID = SI.SalespersonID\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (customerIDs != "")
				{
					str = str + " AND SI.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND (SI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' OR Customer.ParentCustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ) ";
				}
				if (fromCustomerClass != "")
				{
					str = str + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					str = str + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromCustomerArea != "")
				{
					str = str + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
				}
				if (fromCustomerCountry != "")
				{
					str = str + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
				}
				if (fromSalesperson != "")
				{
					str = str + " AND ( SI.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ) ";
				}
				if (fromSalespersonGroup != "")
				{
					str = str + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					str = str + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					str = str + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					str = str + " AND SI.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				if (fromLocation != "")
				{
					str = str + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Sales", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesByItemCustomerSalespersonReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs, string strGroupBy)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string str = "Select SID.ProductID,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.CustomerID + '-' + CustomerName AS 'Customer', SI.SalespersonID + '-' + FullName AS 'Salesperson', SID.Description,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n\t\t\t\t\t\tSID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF\r\n\t\t\t\t\t\tFROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                        INNER JOIN System_Document SD ON SD.SysDocID=SID.SysDocID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tCustomer ON Customer.CustomerID=SI.CustomerID\r\n                        LEFT JOIN Salesperson ON Salesperson.SalespersonID = SI.SalespersonID\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (customerIDs != "")
				{
					str = str + " AND SI.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND SI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromSalesperson != "")
				{
					str = str + " AND ( SI.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' )";
				}
				if (fromLocation != "")
				{
					str = str + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				str += " UNION ALL ";
				str = str + "Select SID.ProductID,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.CustomerID + '-' + CustomerName AS 'Customer', SI.SalespersonID + '-' + FullName AS 'Salesperson', SID.Description,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type],\r\n\t\t\t\t\t\t-1*SID.Quantity,UnitPrice, (-1*SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitP,\r\n                        (CASE WHEN SID.FactorType='D' THEN (-1*SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (-1*SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (-1*SID.Quantity*UnitPrice) END) AS AmountF\r\n\t\t\t\t\t\tFROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                        INNER JOIN System_Document SD ON SD.SysDocID=SID.SysDocID\r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tCustomer ON Customer.CustomerID=SI.CustomerID\r\n                        LEFT JOIN Salesperson ON Salesperson.SalespersonID = SI.SalespersonID\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (customerIDs != "")
				{
					str = str + " AND SI.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND SI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromSalesperson != "")
				{
					str = str + " AND ( SI.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR SI.ReportTo BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ) ";
				}
				if (fromLocation != "")
				{
					str = str + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Sales", str);
				str = "SELECT DISTINCT ";
				if (strGroupBy == "4")
				{
					str += "SD.LocationID AS [GROUP]";
				}
				if (strGroupBy == "3")
				{
					str += "SI.SalespersonID + '-' + FullName  AS [GROUP]";
				}
				else if (strGroupBy == "2")
				{
					str += "SI.CustomerID + '-' + CustomerName   AS [GROUP]";
				}
				else if (strGroupBy == "1")
				{
					str += "SID.ProductID AS [GROUP]";
				}
				str += "\tFROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                        INNER JOIN System_Document SD ON SD.SysDocID=SID.SysDocID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tCustomer ON Customer.CustomerID=SI.CustomerID\r\n                        LEFT JOIN Salesperson ON Salesperson.SalespersonID = SI.SalespersonID ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (customerIDs != "")
				{
					str = str + " AND SI.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND SI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromSalesperson != "")
				{
					str = str + " AND SI.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				str += " UNION ALL ";
				str = "SELECT DISTINCT ";
				if (strGroupBy == "4")
				{
					str += "SD.LocationID AS [GROUP]";
				}
				if (strGroupBy == "3")
				{
					str += "SI.SalespersonID + '-' + FullName  AS [GROUP]";
				}
				else if (strGroupBy == "2")
				{
					str += "SI.CustomerID + '-' + CustomerName   AS [GROUP]";
				}
				else if (strGroupBy == "1")
				{
					str += "SID.ProductID AS [GROUP]";
				}
				str += "\tFROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                        INNER JOIN System_Document SD ON SD.SysDocID=SID.SysDocID\r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tCustomer ON Customer.CustomerID=SI.CustomerID\r\n                        LEFT JOIN Salesperson ON Salesperson.SalespersonID = SI.SalespersonID ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromBrand != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (customerIDs != "")
				{
					str = str + " AND SI.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND SI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromSalesperson != "")
				{
					str = str + " AND SI.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				FillDataSet(dataSet, "Temp", str);
				if (dataSet.Tables["Sales"].Rows.Count > 0)
				{
					if (strGroupBy == "1")
					{
						dataSet.Relations.Add("Sales_Rel", new DataColumn[1]
						{
							dataSet.Tables["Temp"].Columns["GROUP"]
						}, new DataColumn[1]
						{
							dataSet.Tables["Sales"].Columns["ProductID"]
						}, createConstraints: false);
					}
					else if (strGroupBy == "2")
					{
						dataSet.Relations.Add("Sales_Rel", new DataColumn[1]
						{
							dataSet.Tables["Temp"].Columns["GROUP"]
						}, new DataColumn[1]
						{
							dataSet.Tables["Sales"].Columns["Customer"]
						}, createConstraints: false);
					}
					else if (strGroupBy == "3")
					{
						dataSet.Relations.Add("Sales_Rel", new DataColumn[1]
						{
							dataSet.Tables["Temp"].Columns["GROUP"]
						}, new DataColumn[1]
						{
							dataSet.Tables["Sales"].Columns["Salesperson"]
						}, createConstraints: false);
					}
					else if (strGroupBy == "4")
					{
						dataSet.Relations.Add("Sales_Rel", new DataColumn[1]
						{
							dataSet.Tables["Temp"].Columns["GROUP"]
						}, new DataColumn[1]
						{
							dataSet.Tables["Sales"].Columns["LocationID"]
						}, createConstraints: false);
					}
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesByItemCustomerSalespersonReport(string group1, string group2, string fields, string joinQuery, DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string str = "[Doc ID], [Doc Number],ASD.ProductID, ASD.Description,   ASD.AverageCost, ASD.CustomerID, Date, ASD.SalespersonID, ASD.ReportTo,  IsExport, Reference,ASD.CurrencyID, CurrencyRate,Type";
				DataSet dataSet = new DataSet();
				string str2 = "";
				string str3 = "";
				str2 = ((!(group1 == "")) ? (str2 + group1 + " ,") : "");
				str3 = ((!(group2 == "")) ? (str3 + group2 + " ,") : "");
				string text3 = "Select " + str2 + "  " + fields + " from Axo_Sales_Detail ASD LEFT OUTER JOIN Product P ON P.ProductID=ASD.ProductID  INNER JOIN System_Document SD ON SD.SysDocID=ASD.[Doc ID]  ";
				if (!string.IsNullOrEmpty(joinQuery))
				{
					text3 += joinQuery;
				}
				text3 = text3 + "  WHERE Date BETWEEN '" + text + "' AND '" + text2 + "'";
				if (fromItem != "")
				{
					text3 = text3 + " AND ASD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (customerIDs != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND ASD.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromCustomerArea != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
				}
				if (fromCustomerCountry != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
				}
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND ( ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "')";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				text3 = text3 + " Group By " + str3 + str;
				FillDataSet(dataSet, "Sales", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseByItemCategorySummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "Select ISNULL(PR.CategoryID,'No Category') AS CategoryID,Cat.CategoryName,\r\n\t\t\t\t\t\t\tMIN(UnitPrice) AS MinPrice,\r\n\t\t\t\t\t\t\tMAX(UnitPrice) AS MaxPrice,\r\n\t\t\t\t\t\t\tSUM(SID.Quantity) AS PurchaseQuantity,\r\n\t\t\t\t\t\t\tSUM(ISNULL(SID.UnitQuantity,SID.Quantity)*UnitPrice) AS PurchaseAmount\r\n\t\t\t\t\t\t\tFROM Purchase_Invoice_Detail SID INNER JOIN Purchase_Invoice SI ON \r\n\t\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Category Cat ON Cat.CategoryID=PR.CategoryID  ";
				text3 = text3 + "WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				text3 += " GROUP BY PR.CategoryID , Cat.CategoryName";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["CategoryID"]
				};
				DataSet dataSet2 = new DataSet();
				text3 = "Select ISNULL(PR.CategoryID,'No Category') AS CategoryID,Cat.CategoryName,\r\n\t\t\t\t\t\t\t-1 * SUM(SID.Quantity) AS ReturnQuantity,\r\n\t\t\t\t\t\t\tSUM(ISNULL(SID.UnitQuantity,SID.Quantity)*UnitPrice) AS ReturnAmount\r\n\t\t\t\t\t\t\tFROM Purchase_Return_Detail SID INNER JOIN Purchase_Return SI ON \r\n\t\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Category Cat ON Cat.CategoryID=PR.CategoryID\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				text3 += " GROUP BY PR.CategoryID , Cat.CategoryName";
				FillDataSet(dataSet2, "Product", text3);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["CategoryID"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseByItemSummaryReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "Select X.ProductID,X.Description,MIN(X.UnitPrice) AS MinPrice,MAX(X.UnitPrice) AS MaxPrice,\r\n                                MIN(X.UnitF) AS MinPriceF,MAX(X.UnitF) AS MaxPriceF,SUM(X.Quantity) AS PurchaseQuantity,\r\n                                SUM(X.Quantity*X.UnitPrice) AS PurchaseAmount,SUM(X.Quantity*X.UnitF) AS PurchaseAmountF FROM\r\n                                (Select SID.ProductID,TransactionDate,SID.VoucherID,SI.VendorID + '-' + VendorName AS 'Vendor',PR.Description,\r\n                                Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n                                SID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                                (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                                (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF                            \r\n                                FROM Purchase_Invoice_Detail SID INNER JOIN Purchase_Invoice SI ON    \r\n                                SID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                                INNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n                                Vendor ON Vendor.VendorID=SI.VendorID \r\n                                    ";
				text3 = text3 + "WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += ") X GROUP BY X.ProductID,X.Description";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["ProductID"]
				};
				DataSet dataSet2 = new DataSet();
				text3 = "Select X.ProductID,X.Description,MIN(X.UnitPrice) AS MinPrice,MAX(X.UnitPrice) AS MaxPrice,\r\n                                MIN(X.UnitF) AS MinPriceF,MAX(X.UnitF) AS MaxPriceF,SUM(X.Quantity) AS ReturnQuantity,\r\n                                SUM(X.Quantity*X.UnitPrice) AS ReturnAmount,SUM(X.Quantity*X.UnitF) AS ReturnAmountF FROM\r\n                                (Select SID.ProductID,TransactionDate,SID.VoucherID,SI.VendorID + '-' + VendorName AS 'Vendor',PR.Description,\r\n                                Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n                                SID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                                (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                                (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF                                \r\n                                FROM Purchase_Return_Detail SID INNER JOIN Purchase_Return SI ON    \r\n                                SID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n                                INNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n                                Vendor ON Vendor.VendorID=SI.VendorID                                 \r\n\t\t\t\t\t\t        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += ") X GROUP BY X.ProductID,X.Description";
				FillDataSet(dataSet2, "Product", text3);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["ProductID"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseByItemDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "Select DISTINCT SID.ProductID,Product.Description\r\n\t\t\t\t\t\t\tFROM Purchase_Invoice_Detail SID INNER JOIN Purchase_Invoice SI ON SID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID\r\n\t\t\t\t\t\t\tINNER JOIN Product ON SID.ProductID=Product.ProductID\r\n                                    ";
				text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(SI.IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " GROUP BY SID.ProductID,Product.Description ";
				text3 += " UNION ";
				text3 += "Select DISTINCT SID.ProductID,Product.Description\r\n\t\t\t\t\t\t\tFROM Purchase_Return_Detail SID INNER JOIN Purchase_Return SI ON SID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID\r\n\t\t\t\t\t\t\tINNER JOIN Product ON SID.ProductID=Product.ProductID\r\n                                    ";
				text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(SI.IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " GROUP BY SID.ProductID,Product.Description ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				DataSet dataSet2 = new DataSet();
				text3 = "Select SID.ProductID,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.VendorID + '-' + VendorName AS 'Vendor',SID.Description,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Purchase' ELSE 'Cash Purchase' END AS [Type],\r\n\t\t\t\t\t\tSID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF                       \r\n\t\t\t\t\t\tFROM Purchase_Invoice_Detail SID INNER JOIN Purchase_Invoice SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tVendor ON Vendor.VendorID=SI.VendorID                                \r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " UNION ALL  ";
				text3 = text3 + "Select SID.ProductID,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.VendorID + '-' + VendorName AS 'Vendor',SID.Description,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type],\r\n\t\t\t\t\t\t-1*SID.Quantity,UnitPrice, (-1*SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitP,\r\n                        (CASE WHEN SID.FactorType='D' THEN (-1*SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (-1*SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (-1*SID.Quantity*UnitPrice) END) AS AmountF                      \r\n\t\t\t\t\t\tFROM Purchase_Return_Detail SID INNER JOIN Purchase_Return SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tVendor ON Vendor.VendorID=SI.VendorID                                 \r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				FillDataSet(dataSet2, "Purchase", text3);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Purchase Detail", dataSet.Tables["Product"].Columns["ProductID"], dataSet.Tables["Purchase"].Columns["ProductID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool IsHoldSaleonProduct(string productID)
		{
			object obj = null;
			bool result = false;
			string exp = "SELECT IsHoldSale from Product WHERE ProductID='" + productID + "'";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				bool.TryParse(obj.ToString(), out result);
			}
			return result;
		}

		public bool IsSufficientQuantityOnhand(string productID, string unitID, string locationID, string detailsTableName, string sysDocID, string voucherID, decimal quantity)
		{
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			if (quantity == 0m)
			{
				return true;
			}
			ItemTypes itemType = GetItemType(productID, null);
			if (itemType != ItemTypes.Inventory && itemType != ItemTypes.ConsignmentItem && itemType != ItemTypes.Assembly && itemType != ItemTypes.Inventory3PL)
			{
				return true;
			}
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", productID, null);
			string b = "";
			if (fieldValue != null)
			{
				b = fieldValue.ToString();
			}
			string text = "SELECT ";
			text = ((!(unitID != b) || !(unitID != "")) ? (text + "Quantity AS 'Onhand' ") : (text + " CASE (SELECT PU.Factor FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') WHEN 1 THEN Quantity \r\n\t\t\t\t\t\t\tELSE CASE (SELECT PU.FactorType FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') WHEN 'M'\r\n\t\t\t\t\t\t\tTHEN Quantity * (SELECT PU.Factor FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') ELSE\r\n\t\t\t\t\t\t\tQuantity / (SELECT PU.Factor FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') END  END AS 'Onhand' "));
			text = text + " FROM Product_Location PL WHERE PL.ProductID='" + productID + "' AND LocationID = '" + locationID + "'";
			fieldValue = ExecuteScalar(text);
			if (fieldValue != null && fieldValue.ToString() != "")
			{
				decimal.TryParse(fieldValue.ToString(), out result);
			}
			if (sysDocID != "" && voucherID != "")
			{
				text = "SELECT ";
				text = ((!(unitID != b) || !(unitID != "")) ? (text + " SUM(ISNULL(Quantity,0)) AS 'Onhand' ") : (text + " CASE (SELECT PU.Factor FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID=PL.UnitID) WHEN 1 THEN SUM(PL.Quantity)\r\n\t\t\t\t\t\t\tELSE CASE (SELECT PU.FactorType FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') WHEN 'M'\r\n\t\t\t\t\t\t\tTHEN SUM(Quantity) * (SELECT PU.Factor FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') ELSE\r\n\t\t\t\t\t\t\tSUM(Quantity) / (SELECT PU.Factor FROM Product_Unit PU WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "') END  END AS 'Onhand' "));
				text = text + " FROM " + detailsTableName + " PL WHERE PL.ProductID='" + productID + "' AND PL.LocationID = '" + locationID + "' AND PL.SysDocID='" + sysDocID + "' AND PL.VoucherID='" + voucherID + "' GROUP BY UnitID";
				fieldValue = ExecuteScalar(text);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					decimal.TryParse(fieldValue.ToString(), out result2);
				}
				result += result2;
			}
			if (quantity <= result)
			{
				return true;
			}
			return false;
		}

		public DataSet GetProductListReport(string fromProduct, string toProduct, string fromClass, string toClass, string fromCategory, string toCategory, bool showZero, bool showInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT ProductID,Description,Description2,ClassName,UPC,Product.ItemType,Attribute1,Attribute2,Attribute3,\r\n\t\t\t\t\t\t\t\tCategoryName,BrandName,Product.[Size],Quantity\r\n\t\t\t\t\t\t\t\tFROM Product\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Class PC ON PC.ClassID=Product.ClassID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Category PCA ON PCA.CategoryID=Product.CategoryID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Brand PB ON PB.BrandID=Product.BrandID                               \r\n\t\t\t\t\t\t\t\tWHERE 1=1 AND Product.ItemType NOT IN ('3') ";
			if (!showZero)
			{
				text += " AND Quantity<>0 ";
			}
			if (fromProduct != "")
			{
				text = text + " AND ProductID>='" + fromProduct + "'";
			}
			if (toProduct != "")
			{
				text = text + " AND ProductID<='" + toProduct + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND Product.ClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND Product.ClassID<='" + toClass + "'";
			}
			if (fromCategory != "")
			{
				text = text + " AND Product.CategoryID>='" + fromCategory + "'";
			}
			if (toCategory != "")
			{
				text = text + " AND Product.CategoryID<='" + toCategory + "'";
			}
			if (fromManufacturer != "")
			{
				text = text + " AND Product.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text = text + " AND Product.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text = text + " AND Product.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Product.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Products", text);
			return dataSet;
		}

		public DataSet GetProductPriceListReport(string fromProduct, string toProduct, string fromClass, string toClass, string fromCategory, string toCategory, bool showInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT ProductID,Description,Description2,UnitPrice1,UnitPrice2,UnitPrice3,MinPrice\r\n\t\t\t\t\t\t\t\tFROM Product \r\n\t\t\t\t\t\t\t\tWHERE 1=1 AND ItemType NOT IN ('3')";
			if (fromProduct != "")
			{
				text = text + " AND ProductID>='" + fromProduct + "'";
			}
			if (toProduct != "")
			{
				text = text + " AND ProductID<='" + toProduct + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND Product.ClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND Product.ClassID<='" + toClass + "'";
			}
			if (fromCategory != "")
			{
				text = text + " AND Product.CategoryID>='" + fromCategory + "'";
			}
			if (toCategory != "")
			{
				text = text + " AND Product.CategoryID<='" + toCategory + "'";
			}
			if (fromManufacturer != "")
			{
				text = text + " AND Product.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text = text + " AND Product.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text = text + " AND Product.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Product.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Products", text);
			string textCommand = "SELECT ISNULL(ItemPrice1Name,'Price 1') AS ItemPrice1Name,ISNULL(ItemPrice2Name,'Price 2') AS ItemPrice2Name,ISNULL(ItemPrice3Name,'Price 3') AS ItemPrice3Name\r\n\t\t\t\t\t\t   FROM Company WHERE CompanyID=1";
			FillDataSet(dataSet, "PriceList", textCommand);
			return dataSet;
		}

		public DataSet GetProductSinglePriceListReport(string fromProduct, string toProduct, string fromClass, string toClass, string fromCategory, string toCategory, string unitPriceName, bool showInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = ((unitPriceName == "0") ? "UnitPrice1" : ((unitPriceName == "1") ? "UnitPrice2" : ((!(unitPriceName == "2")) ? "MinPrice" : "UnitPrice3")));
			string str = "SELECT ProductID,Description,Description2,Size,Attribute," + text + " AS UnitPrice  ";
			str += " FROM Product   \r\n\t\t\t\t\t\t\t\tWHERE 1=1 AND ItemType NOT IN ('3')";
			if (fromProduct != "")
			{
				str = str + " AND ProductID>='" + fromProduct + "'";
			}
			if (toProduct != "")
			{
				str = str + " AND ProductID<='" + toProduct + "'";
			}
			if (fromClass != "")
			{
				str = str + " AND Product.ClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				str = str + " AND Product.ClassID<='" + toClass + "'";
			}
			if (fromCategory != "")
			{
				str = str + " AND Product.CategoryID>='" + fromCategory + "'";
			}
			if (toCategory != "")
			{
				str = str + " AND Product.CategoryID<='" + toCategory + "'";
			}
			if (fromManufacturer != "")
			{
				str = str + " AND Product.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				str = str + " AND Product.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				str = str + " AND Product.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (!showInactive)
			{
				str += " AND ISNULL(Product.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Products", str);
			return dataSet;
		}

		public DataSet GetProductStockListItemWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				if (isAsOf)
				{
					asOfDate = DateTime.Today;
				}
				string text = "SELECT   DISTINCT PL.ProductID, Description,P.Size,Attribute\r\n\t\t\t\t\t\t\t\tFROM Product_Location PL INNER JOIN Product P\r\n\t\t\t\t\t\t\t\tON P.ProductID=PL.ProductID INNER JOIN Location LOC ON Loc.LocationID = PL.LocationID\r\n                                      \r\n\t\t\t\t\t\t\t\tWHERE ISNULL(IsConsignOutLocation,'False') = 'False' AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ItemType NOT IN ('3')) ";
				if (fromItem != "")
				{
					text = text + " AND PL.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND PL.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				if (!showZero)
				{
					text += " AND PL.Quantity<>0 ";
				}
				text += " ORDER BY PL.ProductID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Location", text);
				DataSet dataSet2 = new DataSet();
				string str = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				text = " SELECT DISTINCT IT.ProductID,P.Description,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID, SUM(IT.Quantity) AS Quantity,\r\n                            sum(AssetValue)  /  SUM(IT.Quantity) AS AverageCost ,\r\n                            sum(AssetValue)   AS Value,SUM(IT.AssetValue) AS [AssetValue],\r\n                            P.UPC,P.VendorRef AS VendorReference,P.Description2,P.BrandID,PB.BrandName,P.UnitID,PC.CategoryName as Category,PCS.ClassName as Class,P.StyleId,PS.StyleName\r\n                            FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n                            INNER JOIN Location Loc ON Loc.LocationID = IT.LocationID \r\n                            LEFT JOIN Product_Brand PB ON PB.BrandID=P.BrandID\r\n                            LEFT JOIN Product_Category PC ON PC.CategoryID=P.CategoryID  \r\n                            LEFT JOIN Product_Class PCS ON PCS.ClassID=P.ClassID \r\n                            LEFT JOIN Product_Style PS ON PS.StyleId=P.StyleId             \r\n                            WHERE  convert(date, TransactionDate) <= '" + str + "' AND P.ItemType IN(1, 7) AND ISNULL(Loc.IsConsignOutLocation,'False') = 'False'  \r\n                            and isnull (IsVoid,0) <> 1 AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ItemType NOT IN ('3'))";
				if (fromItem != "")
				{
					text = text + " AND IT.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND IT.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				text += "  GROUP BY IT.ProductID,P.Description,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,P.UPC,P.VendorRef,P.Description2,P.BrandID,PB.BrandName,P.UnitID,\r\n                    PC.CategoryName,PCS.ClassName,P.StyleId,PS.StyleName ";
				if (!showZero)
				{
					text += "  HAVING SUM(IT.Quantity)<>0 ";
				}
				text += " ORDER BY IT.ProductID";
				FillDataSet(dataSet2, "Quantity", text);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Product_Location", dataSet.Tables["Location"].Columns["ProductID"], dataSet.Tables["Quantity"].Columns["ProductID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryAgingSummaryReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromBrand, string toBrand, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
				_ = (asOfDate < DateTime.Today);
				if (asOfDate == DateTime.MaxValue)
				{
					CommonLib.ToSqlDateTimeString(DateTime.Parse("1-1-2099"));
				}
				else
				{
					CommonLib.ToSqlDateTimeString(asOfDate);
				}
				DataHelper dataHelper = new DataHelper(base.DBConfig);
				DataSet dataSet = new DataSet();
				dataSet = new CompanyOption(base.DBConfig).GetCompanyOptionList(143, 158);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowInvAging1, defaultValue: true);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowInvAging2, defaultValue: true);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowInvAging3, defaultValue: true);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowInvAging4, defaultValue: true);
				int companyOption = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvFrom1, 0);
				int companyOption2 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvFrom2, 31);
				int companyOption3 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvFrom3, 61);
				int companyOption4 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvFrom4, 91);
				int companyOption5 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvTo1, 30);
				int companyOption6 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvTo2, 60);
				int companyOption7 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvTo3, 90);
				int companyOption8 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvTo4, 120);
				companyOption = 0;
				companyOption2 = 1;
				companyOption3 = 2;
				companyOption4 = 3;
				companyOption5 = 1;
				companyOption6 = 2;
				companyOption7 = 3;
				companyOption8 = 4;
				string text = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				_ = "Age_" + companyOption + "to" + companyOption5;
				_ = "Age_" + companyOption2 + "to" + companyOption6;
				_ = "Age_" + companyOption3 + "to" + companyOption7;
				_ = "Age_" + companyOption4 + "to" + companyOption8;
				string text2 = "WITH CTE0 AS\u00a0\r\n                                ( SELECT ProductID , locationID , TransactionDate , sum ( Quantity ) AS Value\u00a0\r\n                                FROM Inventory_Transactions\u00a0\r\n                                WHERE Quantity > 0 AND Convert(date,TransactionDate,106)  <=  '" + text + "' \r\n                                GROUP BY ProductID , locationID , TransactionDate\u00a0\r\n                                )\u00a0\r\n\r\n                                , CTE AS\u00a0\r\n                                (\u00a0\r\n                                SELECT ProductID , locationID , TransactionDate , Value , sum ( Value ) OVER ( PARTITION BY ProductID , locationID ORDER BY TRANSACTIONDATE ) AS sum_qty\u00a0\r\n                                FROM CTE0\u00a0\r\n                                WHERE Value > 0\u00a0\r\n                                )\u00a0\r\n\r\n                                , CTE2 AS\u00a0\r\n                                (\u00a0\r\n                                SELECT ProductID , LocationID , - sum ( Quantity ) AS sum_qty\u00a0\r\n                                FROM Inventory_Transactions\u00a0\r\n                                WHERE Quantity < 0 AND Convert(date,TransactionDate,106)  <=  '" + text + "'\u00a0\r\n                                GROUP BY ProductID , LocationID\u00a0\r\n                                )\u00a0\r\n\r\n                                , CTE3 AS\u00a0\r\n                                (\u00a0\r\n                                SELECT a.ProductID , a.LocationID , a.TransactionDate , a.Value , a.sum_qty , isnull ( b.sum_qty , 0 ) AS sum_qty2 , ROW_NUMBER ( ) OVER ( PARTITION BY a.ProductID , a.LocationID ORDER BY a.TransactionDate ) AS row_num\u00a0\r\n                                FROM CTE a\u00a0\r\n                                LEFT JOIN CTE2 b ON a.ProductID = b.ProductID AND a.LocationID = b.LocationID\u00a0\r\n                                WHERE a.sum_qty > isnull ( b.sum_qty , 0 )\u00a0\r\n                                )\u00a0\r\n\r\n                                , CTEFinal AS\u00a0\r\n                                (\u00a0\r\n                                SELECT c.ProductID , p.Description , CategoryName , PB.BrandName , PCL.ClassName , c.LocationID , c.TransactionDate ,\u00a0\r\n                                ( SELECT Case when sum ( isnull(IT.Quantity,0) )>0 then sum (isnull(IT.AssetValue,0) ) /sum ( isnull(IT.Quantity,0) ) else 0 End FROM Inventory_Transactions IT WHERE IT.ProductID = C.ProductID AND Convert(date,TransactionDate,106)  <=  '" + text + "' ) AS [Rate] ,\u00a0\r\n                                CASE WHEN row_num = 1 THEN sum_qty - sum_qty2 ELSE c.Value END AS BalanceQty\u00a0\r\n                                FROM CTE3 c\u00a0\r\n                                INNER JOIN Product p ON p.ProductID = c.ProductID\u00a0\r\n                                LEFT JOIN Product_Category PC ON PC.CategoryID = P.CategoryID\u00a0\r\n                                LEFT JOIN Product_Class PCL ON PCL.ClassID = P.ClassID\u00a0\r\n                                LEFT JOIN Product_Brand PB ON PB.BrandID = P.BrandID\u00a0\r\n                                )\u00a0\r\n\r\n                                SELECT ProductID , Description , CategoryName , BrandName , ClassName , LocationID , cast ( round ( Rate , 2 ) AS numeric ( 36 , 3 ) ) AS Rate\u00a0\r\n                                , BalanceQty AS [Quantity] ,\u00a0\r\n                                CASE\u00a0\r\n                                WHEN datediff ( day , I.TransactionDate , '" + text + "' ) <= 0 THEN '1. Current'\u00a0\r\n                                WHEN ( datediff ( day , I.TransactionDate , '" + text + "' ) > 0 AND datediff ( day , I.TransactionDate , '" + text + "' ) <= 30 ) THEN '2. 0-30 Days' -- 1 month\r\n                                WHEN ( datediff ( day , I.TransactionDate , '" + text + "' ) > 30 AND datediff ( day , I.TransactionDate , '" + text + "' ) <= 60 ) THEN '3. 30-60 Days' -- 2 months\r\n                                WHEN ( datediff ( day , I.TransactionDate , '" + text + "' ) > 60 AND datediff ( day , I.TransactionDate , '" + text + "' ) <= 90 ) THEN '4. 60-90 Days' -- 3 months\r\n                                WHEN ( datediff ( day , I.TransactionDate , '" + text + "' ) > 90 AND datediff ( day , I.TransactionDate , '" + text + "' ) <= 120 ) THEN '5. 90-120 Days' -- 4 months\r\n                                WHEN ( datediff ( day , I.TransactionDate , '" + text + "' ) > 120 AND datediff ( day , I.TransactionDate , '" + text + "' ) <= 150 ) THEN '6. 120-150 Days' -- 5 months\r\n                                WHEN ( datediff ( day , I.TransactionDate , '" + text + "' ) > 150 AND datediff ( day , I.TransactionDate , '" + text + "' ) <= 180 ) THEN '7. 150-180 Days' -- 6 months\r\n                                WHEN ( datediff ( day , I.TransactionDate , '" + text + "' ) > 180 AND datediff ( day , I.TransactionDate , '" + text + "' ) <= 360 ) THEN '8. 180-360 Days' -- 6-12 Month\r\n                                WHEN ( datediff ( day , I.TransactionDate , '" + text + "' ) > 360 ) THEN '9. 360+ Days' -- > 1 Year Plus\r\n                                END AS Age\u00a0\r\n                                FROM CTEFinal I Where 1=1 ";
				if (!isInactive)
				{
					text2 += " AND I.ProductID IN (SELECT ProductID FROM Product WHERE ISNULL(IsInactive,'False')='False')";
				}
				if (fromItem != "")
				{
					text2 = text2 + " AND I.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text2 = text2 + " AND I.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text2 = text2 + " AND I.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text2 = text2 + " AND I.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text2 = text2 + " AND I.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text2 = text2 + " AND I.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text2 = text2 + " AND I.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text2 = text2 + " AND I.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text2 = text2 + " AND I.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text2 = text2 + " AND I.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text2 = text2 + " AND I.LocationID BETWEEN'" + fromLocation + "' AND '" + toLocation + "'";
				}
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Inventory_Aging", text2);
				return dataSet2;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryAgingSummaryReport_Last(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, DateTime asOfDate, bool showZero, bool isInactive)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				string str = "SELECT  IT.ProductID [Item Code],P.Description, P.Attribute1 AS Color, P.Attribute2 AS Size,P.MatrixParentID AS Article, SUM(IT.quantity) AS Qty, SUM(IT.AssetValue) AS Value,\r\n                                DateDiff(Day,(SELECT Min(TransactionDate) \r\n\t                            FROM Inventory_Transactions IT2 WHERE IT.ProductID = IT2.ProductID \r\n\t\t                        AND Quantity>0 AND transactiondate <= '" + text + "' HAVING Sum(quantity)>0 ), '" + text + "') AS Age FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID = IT.ProductID INNER JOIN Product_Location PL ON P.ProductID = PL.ProductID WHERE 1=1 AND P.ItemType = 1 ";
				str = str + " AND transactiondate <= '" + text + "' ";
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					str = str + " AND IT.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromLocation != "")
				{
					str = str + " AND PL.LocationID BETWEEN'" + fromLocation + "' AND '" + toLocation + "'";
				}
				if (!isInactive)
				{
					str += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				str += " GROUP BY IT.productid ,P.AverageCost,P.Description, P.Attribute1 , P.Attribute2 ,P.MatrixParentID ";
				if (!showZero)
				{
					str += " HAVING SUM(IT.Quantity) <> 0 ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Aging_Summary", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetMatrixProductStockListReport(string fromItem, string toItem, string fromCategory, string toCategory, bool showZero, bool showImage, bool isInactive)
		{
			try
			{
				string str = "SELECT DISTINCT PP.ProductParentID, PP.Description,SUM(PL.Quantity) AS Quantity, CASE SUM(PL.Quantity) WHEN 0 THEN 0 ELSE ROUND(SUM(PL.Quantity * P.AverageCost)/SUM(PL.Quantity),5) END AS AvgCost,\r\n\t\t\t\t\t\t\t\tMax(P.UnitPrice1) AS  UnitPrice1, Max(P.UnitPrice2) AS UnitPrice2, Max(P.UnitPrice3) AS UnitPrice3 \r\n\t\t\t\t\t\t\t\tFROM Product_Parent PP   \r\n\t\t\t\t\t\t\t\tINNER JOIN Product P ON P.MatrixParentID = PP.ProductParentID\r\n\t\t\t\t\t\t\t\tINNER JOIN Product_Location PL ON P.ProductID = PL.ProductID AND PL.Quantity <>0\r\n\t\t\t\t\t\t\t\tWHERE 1=1 AND P.ItemType NOT IN ('3')";
				if (fromItem != "")
				{
					str = str + " AND PP.ProductParentID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					str = str + " AND PP.ProductParentID <= '" + toItem + "' ";
				}
				if (fromCategory != "")
				{
					str = str + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					str = str + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (!isInactive)
				{
					str += " AND ISNULL(PP.IsInactive,'False')='False'";
				}
				str += " GROUP BY PP.ProductParentID, PP.Description ";
				if (!showZero)
				{
					str += " HAVING  SUM(PL.Quantity) <> 0 ";
				}
				str += " ORDER BY PP.ProductParentID,PP.Description ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product_Parent", str);
				DataSet dataSet2 = new DataSet();
				str = "SELECT DISTINCT MatrixParentID,P.ProductID,P.Description, P.Attribute1, P.Attribute2,P.Attribute3,  SUM(PL.Quantity) AS Quantity \r\n\t\t\t\t\t\tFROM Product P INNER JOIN Product_Location PL  ON PL.ProductID=P.ProductID\r\n\t\t\t\t\t\tWHERE 1=1 AND P.MatrixParentID IS NOT NULL AND P.ItemType NOT IN ('3')";
				if (fromItem != "")
				{
					str = str + " AND P.MatrixParentID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					str = str + " AND P.MatrixParentID <= '" + toItem + "' ";
				}
				if (fromCategory != "")
				{
					str = str + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					str = str + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (!isInactive)
				{
					str += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				str += " GROUP BY MatrixParentID,P.ProductID,P.Description, P.Attribute1, P.Attribute2,P.Attribute3  ";
				if (!showZero)
				{
					str += " HAVING SUM(PL.Quantity) <> 0 ";
				}
				str += " ORDER BY P.ProductID, P.Attribute1,P.Attribute2,P.Attribute3 ";
				FillDataSet(dataSet2, "Product", str);
				dataSet.Merge(dataSet2);
				if (showImage)
				{
					DataSet dataSet3 = new DataSet();
					str = "SELECT MatrixParentID, P.ProductID, P.Description, ISNULL(P.Photo, (SELECT Photo FROM Product_Parent PP WHERE ProductParentID = P.MatrixParentID)) AS Photo FROM Product P";
					FillDataSet(dataSet3, "Product_Photo", str);
					dataSet.Merge(dataSet3);
					dataSet.Relations.Add("Matrix_Product_Photo", dataSet.Tables["Product_Parent"].Columns["ProductParentID"], dataSet.Tables["Product_Photo"].Columns["MatrixParentID"], createConstraints: false);
				}
				dataSet.Relations.Add("Matrix_Product", dataSet.Tables["Product_Parent"].Columns["ProductParentID"], dataSet.Tables["Product"].Columns["MatrixParentID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductStockListLocationWiseReportOld(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				if (!isAsOf)
				{
					asOfDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
				}
				string text = "SELECT   DISTINCT PL.LocationID, LocationName\r\n\t\t\t\t\t\t\t\tFROM Product_Location PL INNER JOIN Location L\r\n\t\t\t\t\t\t\t\tON L.LocationID=PL.LocationID  \r\n                                    LEFT JOIN Product ON Product.ProductID=PL.ProductID \r\n\t\t\t\t\t\t\t\tWHERE 1=1 AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ItemType NOT IN ('3'))";
				if (fromItem != "")
				{
					text = text + " AND PL.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND PL.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND PL.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND PL.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(Product.IsInactive,'False')='False'";
				}
				text += " ORDER BY PL.LocationID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Location", text);
				string text2 = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				text = "  SELECT DISTINCT IT.ProductID,P.Description,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID, SUM(IT.Quantity) AS Quantity,IT.LocationID,\r\n                               AG.AverageCost AS AverageCost , AG.AverageCost * SUM(IT.Quantity)  AS Value,SUM(IT.AssetValue) AS [AssetValue],\r\n                                P.UPC,P.VendorRef AS VendorReference,P.Description2,P.BrandID,PB.BrandName,P.UnitID,PC.CategoryName as Category,PCS.ClassName as Class,P.StyleId,PS.StyleName\r\n                            FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n                            INNER JOIN Location Loc ON Loc.LocationID = IT.LocationID  LEFT OUTER JOIN   (SELECT T.ProductID,ISNULL(T.AverageCost,0) AS AverageCost FROM (\r\n\t\t\t\t\t\t SELECT   ProductID, AverageCost, Row_Number() OVER (Partition by ProductID ORDER BY TransactionDate DESC,TransactionID desc) AS RN FROM Inventory_Transactions WHERE TransactionDate <= '" + text2 + "' ) T WHERE RN =1\r\n\t\t\t\t\t\t\t ) AG ON AG.ProductID = IT.ProductID   LEFT JOIN Product_Brand PB ON PB.BrandID=P.BrandID \r\n                             LEFT JOIN Product_Category PC ON PC.CategoryID=P.CategoryID  \r\n                            LEFT JOIN Product_Class PCS ON PCS.ClassID=P.ClassID   \r\n                            LEFT JOIN Product_Style PS ON PS.StyleId=P.StyleId                                     \r\n                            WHERE  TransactionDate <= '" + text2 + "' AND P.ItemType IN(1, 7) AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ItemType NOT IN ('3')) ";
				if (fromItem != "")
				{
					text = text + " AND IT.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND IT.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND IT.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND IT.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				text += "  GROUP BY IT.ProductID,IT.LocationID,P.Description,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID ,AG.AverageCost,P.UPC,P.VendorRef,P.Description2,P.BrandID,PB.BrandName,P.UnitID,PC.CategoryName,PCS.ClassName,P.StyleId,PS.StyleName   ";
				if (!showZero)
				{
					text += "  HAVING SUM(IT.Quantity)<>0 ";
				}
				text += " ORDER BY IT.ProductID";
				FillDataSet(dataSet, "Quantity", text);
				dataSet.Relations.Add("Product_Location", dataSet.Tables["Location"].Columns["LocationID"], dataSet.Tables["Quantity"].Columns["LocationID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductStockListLocationWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				if (!isAsOf)
				{
					asOfDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
				}
				string text = "SELECT   DISTINCT PL.LocationID, LocationName\r\n\t\t\t\t\t\t\t\tFROM Product_Location PL INNER JOIN Location L\r\n\t\t\t\t\t\t\t\tON L.LocationID=PL.LocationID  \r\n                                    LEFT JOIN Product ON Product.ProductID=PL.ProductID \r\n\t\t\t\t\t\t\t\tWHERE 1=1 AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ItemType NOT IN ('3'))";
				if (fromItem != "")
				{
					text = text + " AND PL.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND PL.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND PL.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND PL.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(Product.IsInactive,'False')='False'";
				}
				text += " ORDER BY PL.LocationID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Location", text);
				string text2 = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				text = "With ProductLasDate as (\r\n                        SELECT T.ProductID,ISNULL(T.AverageCost,0) AS AverageCost   FROM (\r\n                        SELECT   ProductID, AverageCost, Row_Number() OVER (Partition by ProductID ORDER BY TransactionDate DESC,TransactionID desc) AS RN FROM Inventory_Transactions \r\n                        WHERE TransactionDate <= '" + text2 + "'\r\n                        ) T \r\n                        WHERE RN =1)\r\n                        select  \r\n                        DISTINCT IT.ProductID, SUM(IT.Quantity) AS Quantity,IT.LocationID,\r\n                        AG.AverageCost AS AverageCost , AG.AverageCost * SUM(IT.Quantity)  AS Value,\r\n                        SUM(IT.AssetValue) AS [AssetValue]\r\n                        into #tmp1\r\n                        from Inventory_Transactions IT\r\n                        INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n                        INNER JOIN Location Loc ON Loc.LocationID = IT.LocationID  \r\n                        left join ProductLasDate  AG ON AG.ProductID = IT.ProductID   \r\n                        where p.ItemType = 1  and TransactionDate <= '" + text2 + "' AND ( P.ItemType = 1 or  P.ItemType = 7 )\r\n                        and p.ItemType <> 3 \r\n                        AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ItemType NOT IN ('3')) \r\n                        AND ISNULL(P.IsInactive,'False')='False'  ";
				if (fromItem != "")
				{
					text = text + " AND IT.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND IT.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND IT.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND IT.LocationID <= '" + toLocation + "'";
				}
				text += "group by IT.ProductID,IT.LocationID , AG.AverageCost \r\n                        HAVING SUM(IT.Quantity)<>0  ";
				text += "select IT.ProductID,P.Description,P.Attribute1,P.Attribute2,P.Attribute3,it.AssetValue,it.AverageCost,it.LocationID,it.Quantity,it.Value ,\r\n                        P.UPC,P.VendorRef AS VendorReference,P.Description2,P.BrandID,PB.BrandName,P.UnitID,PC.CategoryName as Category,PCS.ClassName as Class,P.StyleId,PS.StyleName\r\n                        from #tmp1 IT \r\n                        INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n                        LEFT JOIN Product_Brand PB ON PB.BrandID = P.BrandID\r\n                        LEFT JOIN Product_Category PC ON PC.CategoryID = P.CategoryID\r\n                        LEFT JOIN Product_Class PCS ON PCS.ClassID = P.ClassID\r\n                        LEFT JOIN Product_Style PS ON PS.StyleId = P.StyleId  ";
				if (fromItem != "")
				{
					text = text + " AND IT.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND IT.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND IT.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND IT.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				text += "  GROUP BY IT.ProductID,IT.LocationID,P.Description,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID ,it.AverageCost,it.AssetValue,it.Quantity,it.Value,P.UPC,P.VendorRef,P.Description2,P.BrandID,PB.BrandName,P.UnitID,PC.CategoryName,PCS.ClassName,P.StyleId,PS.StyleName   ";
				if (!showZero)
				{
					text += "  HAVING SUM(IT.Quantity)<>0 ";
				}
				text += " ORDER BY IT.ProductID";
				text += " DROP Table #tmp1 ";
				FillDataSet(dataSet, "Quantity", text);
				dataSet.Relations.Add("Product_Location", dataSet.Tables["Location"].Columns["LocationID"], dataSet.Tables["Quantity"].Columns["LocationID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductCatalogReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool isInactive, bool zeroQuantity, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = "";
				DataSet dataSet = new DataSet();
				text = "SELECT   P.ProductID,Description,Description2,ClassName,P.ItemType,CategoryName,\r\n\t\t\t\t\t\tP.QuantityPerUnit,P.Weight,P.UnitID,P.StyleID,P.Attribute,UnitPrice1,UnitPrice2,UnitPrice3,MinPrice,\r\n\t\t\t\t\t\tISNULL(Photo, (SELECT Photo FROM Product_Parent PP WHERE ProductParentID = P.MatrixParentID)) AS Photo,\r\n\t\t\t\t\t\tP.[Size],BrandName,P.Origin, P.Note\r\n\t\t\t\t\t\tFROM Product P\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Class PCL ON PCL.ClassID=P.ClassID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Category PCA ON PCA.CategoryID=P.CategoryID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Brand PCB ON PCB.BrandID=P.BrandID                        \r\n\t\t\t\t\t\tWHERE 1=1 AND ISNULL(ExcludeFromCatalogue,'False') = 'False' AND P.ItemType NOT IN ('3') ";
				if (fromItem != "")
				{
					text = text + " AND P.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND P.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(P.IsInactive,'False')='False' ";
				}
				if (!zeroQuantity)
				{
					text += " AND Quantity > 0 ";
				}
				text += " ORDER BY P.ProductID";
				FillDataSet(dataSet, "Product", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal decimal GetProductCOGS(string productID, string locationID, string voucherNumber, string docID, int rowIndex, decimal quantity, SqlTransaction sqlTransaction)
		{
			object obj = null;
			int num = 1;
			string text = "";
			decimal d = default(decimal);
			string exp = "  SELECT ISNULL(CostMethod,1) FROM Product WHERE ProductID = '" + productID + "'";
			obj = ExecuteScalar(exp, sqlTransaction);
			switch (int.Parse(obj.ToString()))
			{
			case 2:
			{
				text = "SELECT SUM(SoldQty*Cost)\r\n\t\t\t\t\t\t   FROM Product_Lot_Sales  WHERE ItemCode='" + productID + "' AND InvoiceNumber='" + voucherNumber + "' AND DocID='" + docID + "' AND RowIndex = " + rowIndex;
				obj = ExecuteScalar(text, sqlTransaction);
				if (obj != null && obj.ToString() != "")
				{
					d = decimal.Parse(obj.ToString());
				}
				text = "SELECT SUM(ISNULL(Quantity,0))  FROM Unallocated_Lot_Items  WHERE ProductID='" + productID + "' AND VoucherID='" + voucherNumber + "' AND SysDocID='" + docID + "' AND RowIndex = 0";
				obj = ExecuteScalar(text, sqlTransaction);
				decimal num2 = default(decimal);
				if (obj != null && obj.ToString() != "")
				{
					num2 = decimal.Parse(obj.ToString());
				}
				if (num2 != 0m)
				{
					text = "SELECT (CASE CostMethod WHEN 1 THEN  ISNULL(AverageCost,0) ELSE ISNULL(LastCost,0) END) * " + num2 + " FROM Product\r\n\t\t\t\t\t\tWHERE ProductID='" + productID + "'";
					obj = ExecuteScalar(text, sqlTransaction);
					if (obj != null && obj.ToString() != "")
					{
						d += decimal.Parse(obj.ToString());
					}
				}
				break;
			}
			case 3:
				throw new CompanyException("LIFO costing method is not supported. Please user other costing methods.");
			default:
			{
				DateTime result = DateTime.Now;
				exp = "SELECT DISTINCT   TransactionDate FROM Inventory_Transactions IT WHERE IT.SysDocID = '" + docID + "' AND IT.VoucherID = '" + voucherNumber + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					DateTime.TryParse(obj.ToString(), out result);
				}
				d = new InventoryTransaction(base.DBConfig).GetProductAverageCostAsOfDate(productID, locationID, Math.Abs(quantity), result, sqlTransaction) * quantity;
				break;
			}
			}
			return Math.Abs(Math.Round(d, 5));
		}

		internal string GetProductAccountID(string productID, string sysDocID, ProductAccounts accountType, SqlTransaction sqlTransaction)
		{
			if (accountType == ProductAccounts.AssetAccount)
			{
				throw new Exception("ProductAssetAccount should not be retreived by SysDoc, use GetProductAccountIDByLocation() with warehouse location ID.");
			}
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", sysDocID, sqlTransaction);
			if (fieldValue == null)
			{
				throw new CompanyException("Location is not set for document:" + sysDocID);
			}
			string locationID = "";
			if (fieldValue != null)
			{
				locationID = fieldValue.ToString();
			}
			return GetProductAccountIDByLocation(productID, locationID, accountType, sqlTransaction);
		}

		internal DataSet GetProductTransactionAccounts(string productID, string docLocationID, string warehouseLocationID, string sysDocID, SqlTransaction sqlTransaction)
		{
			try
			{
				string textCommand = "SELECT CASE WHEN  P.COGSAccount IS NULL OR P.COGSAccount ='' THEN  \r\n                                    ISNULL((CASE WHEN  PC.COGSAccount IS NULL OR PC.COGSAccount <>'' THEN PC.COGSAccount ELSE NULL END),ISNULL(SD.COGSAccountID,Loc.COGSAccountID))\r\n                                    ELSE P.COGSAccount END AS COGSAccountID, \r\n                                    CASE WHEN  P.IncomeAccount IS NULL OR P.IncomeAccount ='' THEN  ISNULL((CASE WHEN  PC.IncomeAccount IS  NULL OR PC.IncomeAccount <>'' THEN PC.IncomeAccount ELSE NULL END),\r\n                                    ISNULL(SD.SalesAccountID,Loc.SalesAccountID)) ELSE P.IncomeAccount END AS IncomeAccountID, \r\n\t                             CASE WHEN  P.AssetAccount IS NULL OR P.AssetAccount ='' THEN ISNULL((CASE WHEN  PC.AssetAccount IS  NULL OR PC.AssetAccount <>'' THEN PC.AssetAccount ELSE NULL END),ISNULL(WHS.InventoryAccountID,Loc.InventoryAccountID)) ELSE P.AssetAccount END AS InventoryAssetAccountID,  \r\n\t                                P.AverageCost,P.ItemType,LOC.ConsignInAccountID,LOC.ConsignOutCOGSAccountID\r\n                                    FROM Product P\r\n                                    LEFT OUTER JOIN Product_Class PC ON P.ClassID=PC.ClassID \r\n                                    LEFT OUTER JOIN Location LOC ON LOC.LocationID = '" + docLocationID + "'\r\n                                    LEFT OUTER JOIN Location WHS ON WHS.LocationID = '" + warehouseLocationID + "'\r\n                                    LEFT OUTER JOIN System_Document SD ON SD.SysDocID = '" + sysDocID + "'\r\n                                    WHERE ProductID = '" + productID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", textCommand, sqlTransaction);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal string GetProductAccountIDByLocation(string productID, string locationID, ProductAccounts accountType, SqlTransaction sqlTransaction)
		{
			string text = "";
			text = "SELECT " + Enum.GetName(typeof(ProductAccounts), accountType) + " FROM Product  WHERE ProductID='" + productID + "'";
			object obj = ExecuteScalar(text);
			if (obj != null && obj.ToString() != "")
			{
				return obj.ToString();
			}
			if (locationID == "")
			{
				throw new CompanyException("Location is not set for document.");
			}
			string fieldName = "";
			switch (accountType)
			{
			case ProductAccounts.AssetAccount:
				fieldName = "InventoryAccountID";
				break;
			case ProductAccounts.COGSAccount:
				fieldName = "COGSAccountID";
				break;
			case ProductAccounts.IncomeAccount:
				fieldName = "SalesAccountID";
				break;
			}
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Location", fieldName, "LocationID", locationID, sqlTransaction);
			if (fieldValue != null)
			{
				return fieldValue.ToString();
			}
			throw new CompanyException("Account is not set for selected location:" + locationID);
		}

		public DataSet GetProductListPOS()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ProductID, CASE WHEN ISNULL(UPC,'') ='' THEN ProductID ELSE UPC END AS [Code], Description,UPC, Productid + ' ' + Description + ' ' + ISNULL(UPC,'') AS SearchColumn,UnitPrice1 AS Price,TaxGroupID,TaxOption \r\n\t\t\t\t\t\t\t\tFROM Product WHERE ISNULL(IsInactive,'False')='False' AND ISNULL(HideInPOS,'False')='False' ";
			FillDataSet(dataSet, "Product_Unit", textCommand);
			return dataSet;
		}

		public DataSet GetProductListPOS(string locationID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ProductID, CASE WHEN ISNULL(UPC,'') ='' THEN ProductID ELSE UPC END AS [Code], Description,UPC, Productid + ' ' + Description + ' ' + ISNULL(UPC,'') AS SearchColumn,\r\n                            (Select PL.Quantity from Product_Location PL where  PL.ProductID=Product.ProductID AND PL.LocationID='" + locationID + "' )AS OnHand,\r\n                            UnitPrice1 AS Price,TaxGroupID,TaxOption \r\n                            FROM Product WHERE ISNULL(IsInactive,'False')='False' AND ISNULL(HideInPOS,'False')='False'";
			FillDataSet(dataSet, "Product_Unit", textCommand);
			return dataSet;
		}

		public DataSet POSGetProductData(string code)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ProductID,UPC, ItemType, CASE WHEN ISNULL(UPC,'') ='' THEN ProductID ELSE UPC END AS [Code], Description,UnitPrice1 AS Price ,ISNULL((select TOP 1 IT.AverageCost from Inventory_Transactions IT INNER JOIN Product P2 ON P2.ProductID=IT.ProductID where p2.ProductID=Product.ProductID  order by TransactionDate desc),0 ) AS Cost,TaxGroupID,TaxOption\r\n\t\t\t\t\t\t\t\tFROM Product WHERE ISNULL(IsInactive,'False')='False' AND ISNULL(HideInPOS,'False')='False' \r\n\t\t\t\t\t\t\t\tAND UPC='" + code + "' OR ProductID='" + code + "'";
			FillDataSet(dataSet, "Product", textCommand);
			if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				return dataSet;
			}
			return null;
		}

		public DataSet GetTopProducts(DateTime from, DateTime to, int count)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string textCommand = "SELECT TOP " + count.ToString() + " ISNULL(CAt.CategoryName,'None') AS CategoryName ,SUM(Amount) Sales\r\n\t\t\t\t\t\t\t\t\t\tFROM Sales_Invoice_Detail SD\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN Sales_Invoice SI ON SD.SysDocID=SI.SysDocID AND SI.VoucherID=SD.VoucherID\r\n\t\t\t\t\t\t\t\t\t\tINNER JOIN Product P ON SD.ProductID=P.ProductID\r\n\t\t\t\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Category CAT ON Cat.CategoryID = P.CategoryID\r\n\t\t\t\t\t\t\t\t\t\tWHERE Amount > 0 AND TransactionDate Between '" + text + "' AND '" + text2 + "' \r\n\t\t\t\t\t\t\t\t\t\tGROUP BY  CategoryName\r\n\t\t\t\t\t\t\t\t\t\tORDER BY Sales DESC";
			FillDataSet(dataSet, "Product", textCommand);
			return dataSet;
		}

		public DataSet GetProductsToReorder()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ProductID [Item Code],Description,ReorderLevel Reorder,Quantity Onhand\r\n\t\t\t\t\t\t\tFROM Product\r\n\t\t\t\t\t\t\tWHERE Quantity<ReorderLevel\r\n\t\t\t\t\t\t\tAND ReorderLevel <> 0\r\n\t\t\t\t\t\t\tAND ISNULL(IsInactive,'False') = 'False'\r\n\t\t\t\t\t\t\t ";
			FillDataSet(dataSet, "Product", textCommand);
			return dataSet;
		}

		public DataSet GetProductQuantityByDate(bool showZeroBalance, string locationID, DateTime date)
		{
			DataSet dataSet = new DataSet();
			string str = CommonLib.ToSqlDateTimeString(date);
			string str2 = "SELECT IT.ProductID,P.Description,Attribute1 AS Color, Attribute2 AS Size, P.MatrixParentID,CategoryID, P.UnitPrice1,\r\n\t\t\t\t\t\t\t\t  SUM(IT.Quantity) AS Qty\r\n\t\t\t\t\t\t\t\tFROM Inventory_Transactions IT\r\n\t\t\t\t\t\t\t\tINNER JOIN Product P ON IT.ProductID = P.ProductID\r\n\t\t\t\t\t\t\t\tWHERE TransactionDate <= '" + str + "' ";
			if (locationID != "")
			{
				str2 = str2 + " AND PL.LocationID='" + locationID + "' ";
			}
			str2 += "   GROUP BY IT.ProductID,P.Description,P.MatrixParentID,CategoryID,P.UnitPrice1,Attribute1,Attribute2 ";
			if (!showZeroBalance)
			{
				str2 += "  HAVING SUM(ISNULL(IT.Quantity,0)) <> 0 ";
			}
			str2 += " ORDER BY MatrixParentID,ProductID ";
			FillDataSet(dataSet, "Product", str2);
			return dataSet;
		}

		public DataSet GetInventoryLedgerList(string productID, DateTime from, DateTime to, bool excludeInventoryTransfer)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "  = '" + productID + "'";
			if (productID.Contains("%") || productID.Contains("!"))
			{
				text3 = " LIKE '" + productID + "'";
			}
			string str = "SELECT 'OPENING STOCK ' [Doc ID],'0000000' [Doc Number],'0' [Type], '1-1-2019 0:0:0' AS  [Date],IT.ProductID,Product.Description,'' AS LocationID,\r\n                            'OPENING STOCK' AS Party,\r\n                            CASE  WHEN SUM(ISNULL(IT.Quantity, 0)) > 0 THEN SUM(ISNULL(IT.Quantity, 0)) ELSE 0 END AS[Qty In],\r\n                            CASE  WHEN SUM(ISNULL(IT.Quantity, 0)) < 0 THEN - 1 * SUM(ISNULL(IT.Quantity, 0)) ELSE 0 END AS[Qty Out], 0 AS Price, 0 AS[Avg Cost], 0[Asset Value]\r\n\r\n\r\n                            From Inventory_Transactions IT INNER JOIN Product ON Product.ProductID = IT.ProductID\r\n                            LEFt OUTER JOIN Customer ON IT.PayeeID = Customer.CustomerID LEFt OUTER JOIN\r\n                            Vendor ON IT.PayeeID = Vendor.VendorID\r\n                            LEFT OUTER JOIN Job J ON IT.JobID = J.JobID\r\n                            LEFT OUTER JOIN Job_Cost_Category JCC ON IT.CostCategoryID = JCC.CostCategoryID\r\n                            WHERE IT.ProductID " + text3 + " ";
			if (from != DateTime.MinValue)
			{
				str = str + "  AND convert(date, transactionDate) < '" + text + "'";
			}
			str += " GROUP BY IT.ProductID, Product.Description UNION ALL ";
			str = str + "SELECT SysDocID [Doc ID],VoucherID [Doc Number], SysdocType [Type], TransactionDate [Date], IT.ProductID,P.Description, IT.LocationID, \r\n\t\t\t\t\t\t\t\t (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS Party,\r\n\r\n\t\t\t\t\t\t\t\tCASE WHEN IT.Quantity >= 0 THEN IT.Quantity ELSE NULL END AS [Qty In],CASE WHEN IT.Quantity < 0 THEN -1 * IT.Quantity ELSE NULL END AS [Qty Out] ,[UnitPrice] Price,\r\n\t\t\t\t\t\t\t\tCASE IT.Quantity WHEN 0 THEN 0 ELSE IT.AverageCost END AS [Avg Cost],\r\n\t\t\t\t\t\t\t\tAssetValue [Asset Value]\r\n\t\t\t\t\t\t\t\tFROM Inventory_Transactions IT \r\n\t\t\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID = IT.ProductID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Account ON IT.PayeeID=Account.AccountID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN  Vendor ON IT.PayeeID=Vendor.VendorID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN  Employee ON IT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t\tWHERE IT.ProductID  " + text3 + " ";
			if (from != DateTime.MinValue)
			{
				str = str + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (excludeInventoryTransfer)
			{
				str += " AND IT.SysDocType NOT IN('19', '20', '21')";
			}
			FillDataSet(dataSet, "Product", str);
			dataSet.Tables["Product"].Columns.Add("Balance", typeof(decimal));
			dataSet.Tables["Product"].DefaultView.Sort = "Date Asc";
			DataTable table = dataSet.Tables["Product"].DefaultView.ToTable();
			dataSet = new DataSet();
			dataSet.Tables.Add(table);
			foreach (string item in (from s in dataSet.Tables["Product"].AsEnumerable()
				select s.Field<string>("ProductID")).Distinct().ToList())
			{
				decimal d = default(decimal);
				DataRow[] array = dataSet.Tables["Product"].Select($"ProductID ='{item}'");
				foreach (DataRow obj in array)
				{
					decimal.TryParse(obj["Qty Out"].ToString(), out decimal result);
					decimal.TryParse(obj["Qty In"].ToString(), out decimal result2);
					decimal num = d = d + result2 - result;
					obj["Balance"] = num;
				}
			}
			return dataSet;
		}

		public DataSet GetInventoryAgingList(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, DateTime reportDate, bool isInactive)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(reportDate);
			string text2 = "SELECT ItemCode AS ProductID,P.Description,P.Attribute1,P.Attribute2,p.Attribute3,LotNumber,ReceiptDate,MatrixParentID,CategoryID,DATEDIFF(Day,ReceiptDate, '" + text + "') AS Age,\r\n\t\t\t\t\t\t\t\tLotQty - ISNULL((SELECT SUM(SoldQty) FROM Product_Lot_Sales PLS INNER JOIN Inventory_Transactions IT \r\n\t\t\t\t\t\t\t\tON PLS.DocID = IT.SysDocID AND PLS.InvoiceNumber = IT.VoucherID AND PLS.RowIndex = IT.RowIndex AND PLS.ItemCode = IT.ProductID\r\n\t\t\t\t\t\t\t\t WHERE PL.LotNumber = PLS.LotNo AND IT.TransactionDate <= '" + text + "'),0) AS Balance  \r\n\t\t\t\t\t\t\t\tFROM Product_Lot PL  INNER JOIN Product P ON PL.ItemCode = P.ProductID WHERE ReceiptDate <= '" + text + "' ";
			if (fromItem != "")
			{
				text2 = text2 + " AND PL.ProductID >= '" + fromItem + "' ";
			}
			if (toItem != "")
			{
				text2 = text2 + " AND PL.ProductID <= '" + toItem + "' ";
			}
			if (fromClass != "")
			{
				text2 = text2 + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
			}
			if (toClass != "")
			{
				text2 = text2 + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
			}
			if (fromCategory != "")
			{
				text2 = text2 + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
			}
			if (toCategory != "")
			{
				text2 = text2 + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
			}
			if (fromLocation != "")
			{
				text2 = text2 + " AND PL.LocationID >= '" + fromLocation + "'";
			}
			if (toLocation != "")
			{
				text2 = text2 + " AND PL.LocationID <= '" + toLocation + "'";
			}
			if (!isInactive)
			{
				text2 += " AND ISNULL(Inactive,'False')='False'";
			}
			FillDataSet(dataSet, "Product", text2);
			return dataSet;
		}

		public DataSet GetBOMByProduct(string assemblyProductID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BOM.*,P.ItemType, P.Quantity FROM Product_BOM  BOM INNER JOIN Product P ON BOM.BOMProductID = P.ProductID WHERE BOM.ProductID = '" + assemblyProductID + "' ORDER BY RowIndex";
			FillDataSet(dataSet, "Product", textCommand);
			return dataSet;
		}

		public DataSet GetTransactionIssuesProductLots(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT PL.Reference AS LotReference,PLD.*,PL.ProductionDate,PL.ExpiryDate,PL.ReceiptDate,\r\n                        CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE (SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS Consign#\r\n                        FROM Product_Lot_Issue_Detail PLD INNER JOIN Product_Lot PL ON PL.LotNumber = PLD.LotNumber\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Product_Lot_Issue_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductAvailableLotsAndBins(string productID, string locationID, string sysDocID, string voucherID, string vendorID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT LotNumber,Reference,Reference2,P.ItemType,SourceLotNumber, ProductionDate,ExpiryDate,LotQty,Cost,BinID,RackID,\r\n                 CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE (SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS Consign# , LotQty-SoldQty-ISNULL(ReturnedQty,0) ";
			if (sysDocID != "" && voucherID != "")
			{
				text = text + " + ISNULL((SELECT SUM(SoldQty) FROM Product_Lot_Sales PLS WHERE DocID = '" + sysDocID + "' AND InvoiceNumber = '" + voucherID + "' AND LocationID = '" + locationID + "' AND PLS.LotNO = PL.LotNumber),0) ";
			}
			text = text + " AS AvailableQty FROM Product_Lot PL INNER JOIN Product P ON PL.ItemCode=P.ProductID\r\n                                WHERE ItemCode = '" + productID + "' AND LOTQty - ISNULL(ReturnedQty,0) - ISNULL(SoldQty,0) > 0 AND ISNULL(IsDeleted,'False') = 'False'";
			if (vendorID != "")
			{
				text = text + "  AND SupplierCode = '" + vendorID + "' ";
			}
			if (locationID != "")
			{
				text = text + "  AND LocationID = '" + locationID + "' ";
			}
			FillDataSet(dataSet, "Product_Lot", text);
			return dataSet;
		}

		public DataSet GetSOProductAvailableLotsAndBins(string productID, string locationID, string sysDocID, string voucherID, string vendorID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT LotNumber,Reference,Reference2,P.ItemType,SourceLotNumber, ProductionDate,ExpiryDate,LotQty,Cost,BinID,RackID,\r\n                 CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE (SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS Consign# , LotQty-SoldQty-ISNULL(ReturnedQty,0) ";
			if (sysDocID != "" && voucherID != "")
			{
				text = text + " + ISNULL((SELECT SUM(SoldQty) FROM Product_Lot_Sales PLS WHERE DocID = '" + sysDocID + "' AND InvoiceNumber = '" + voucherID + "' AND LocationID = '" + locationID + "' AND PLS.LotNO = PL.LotNumber),0) ";
			}
			text = text + " AS AvailableQty,(select Sum(Quantity) from Reservation_Detail RVD where RVD.LotNumber=PL.LotNumber AND RVD.ProductID=PL.ItemCode AND SysDocID <> '" + sysDocID + "' AND VoucherID <> '" + voucherID + "') AS QtyRs FROM Product_Lot PL INNER JOIN Product P ON PL.ItemCode=P.ProductID\r\n                                WHERE ItemCode = '" + productID + "' AND LOTQty - ISNULL(ReturnedQty,0) - ISNULL(SoldQty,0) > 0 AND ISNULL(IsDeleted,'False') = 'False'";
			if (vendorID != "")
			{
				text = text + "  AND SupplierCode = '" + vendorID + "' ";
			}
			if (locationID != "")
			{
				text = text + "  AND LocationID = '" + locationID + "' ";
			}
			FillDataSet(dataSet, "Product_Lot", text);
			return dataSet;
		}

		public DataSet GetProductAvailableLotsAndBins(string productID, string lotNumber)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT LotNumber,Reference,Reference2,P.ItemType,SourceLotNumber, ProductionDate,ExpiryDate,LotQty,Cost,BinID,\r\n                  LotQty-SoldQty-ISNULL(ReturnedQty,0) ";
			text = text + " AS AvailableQty FROM Product_Lot PL INNER JOIN Product P ON PL.ItemCode=P.ProductID\r\n                                WHERE ItemCode = '" + productID + "' AND  PL.Reference='" + lotNumber + "'  AND LOTQty - ISNULL(ReturnedQty,0) - ISNULL(SoldQty,0) > 0 AND ISNULL(IsDeleted,'False') = 'False'";
			FillDataSet(dataSet, "Product_Lot", text);
			return dataSet;
		}

		public DataSet GetProductReturnableLotsAndBins(string productID, string locationID, string sysDocID, string voucherID, string customerID, string returnSourceSysDocID, string returnSourceVoucherID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT DISTINCT PL.LotNumber , ISNULL(PL.Reference,PL.LotNumber) AS Reference,PL.Reference2,PL.BinID,PL.RackID,ReceiptNumber,PL.SourceLotNumber,PL.ReceiptDate,ProductionDate,ExpiryDate ,\r\n                                CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE (SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS SourceReceiptNumber\r\n                                ,isnull((SELECT PLS.SoldQty   FROM Product_Lot_Sales PLS WHERE PLS.LotNo=PL.LotNumber AND PLS.DocID = '" + returnSourceSysDocID + "'  AND PLS.InvoiceNumber = '" + returnSourceVoucherID + "'),0)as RowSoldQty\r\n                                FROM Product_Lot  PL\r\n                                LEFT OUTER JOIN Consign_In CON ON CON.SysDocID = PL.DocID AND CON.VoucherID = PL.ReceiptNumber\r\n                                WHERE PL.ItemCode = '" + productID + "' AND (PL.SourceLotNumber IS NULL OR PL.SourceLotNumber = 0) AND (Con.Status IS NULL OR Con.Status = 1  OR CON.Status =2) ";
			if (returnSourceSysDocID != "" && returnSourceVoucherID != "")
			{
				text = text + "AND PL.LotNumber IN (SELECT ISNULL(SourceLotNumber,LotNumber) AS LotNumber FROM Product_Lot_Issue_Detail PLS WHERE SysDocID = '" + returnSourceSysDocID + "'  AND VoucherID = '" + returnSourceVoucherID + "')";
				text = text + " UNION \r\n                            SELECT PL.LotNumber AS LotNumber,PL.Reference,PL.Reference2,PL.BinID,PL.RackID, PL.ReceiptNumber,PL.SourceLotNumber,PL.ReceiptDate, PL.ProductionDate,PL.ExpiryDate,\r\n                                     CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE (SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS SourceReceiptNumber\r\n                                    ,0 as RowSoldQty\r\n                                    FROM Product_Lot_Issue_Detail PLD INNER JOIN Product_Lot PL ON PL.LotNumber = PLD.LotNumber\r\n\t\t\t\t\t\t            INNER JOIN Delivery_Note DN ON DN.SysDocID = PLD.SysDocID AND DN.VoucherID = PLD.VoucherID\r\n\t\t\t\t\t\t            WHERE ItemCode = '" + productID + "' AND DN.InvoiceSysDocID = '" + returnSourceSysDocID + "' AND DN.InvoiceVoucherID = '" + returnSourceVoucherID + "'";
			}
			FillDataSet(dataSet, "Product_Lot", text);
			return dataSet;
		}

		public DataSet GetProductLotWiseAvailability(string productID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LotNumber,Reference [Lot Ref],Reference2,ReceiptDate [Rct.Date],V.vendorName AS Vendor, ProductionDate [Production], ExpiryDate AS Expiry,LocationName AS Location,PL.ReceivedQuantity AS [Received Qty],SUM(Quantity) AS [Quantity] FROM Axo_Product_Lot_Quantity PL\r\n                                LEFT OUTER JOIN Vendor V ON V.VendorID = PL.SupplierCode\r\n                                WHERE ProductID = '" + productID + "'   GROUP BY  LotNumber,Reference,ReceiptDate,V.vendorName,ProductionDate,ExpiryDate,LocationName,PL.ReceivedQuantity,Reference2";
			FillDataSet(dataSet, "Product_Lot", textCommand);
			return dataSet;
		}

		public DataSet GetProductLot(string lotID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PL.*, LotQty - ISNULL(SoldQty,0) AS BalanceQty,V.VendorName AS Vendor,\r\n                            (SELECT SUM(LotQty-ISNULL(SoldQty,0)) FROM Product_Lot WHERE ISNULL(SourceLotNumber,'') = " + lotID + ") AS ChildLotsBalance  FROM Product_Lot PL LEFT OUTER JOIN Vendor V \r\n                        ON V.VendorID = PL.SupplierCode WHERE LotNumber = " + lotID;
			FillDataSet(dataSet, "Product_Lot", textCommand);
			return dataSet;
		}

		public bool HasInUseLots(string sysDocID, string voucherID)
		{
			string exp = "SELECT COUNT(*) FROM Product_Lot PL INNER JOIN Product P ON P.ProductID = PL.ItemCode\r\n                            WHERE DocID = '" + sysDocID + "' AND ReceiptNumber = '" + voucherID + "'  AND (ISNULL(P.IsTrackLot,'False') = 'True' OR ItemType = 5) AND PL.LotNumber IN (SELECT LotNumber FROM Product_Lot_Issue_Detail PLD WHERE PLD.ProductID = PL.ItemCode)";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return int.Parse(obj.ToString()) > 0;
			}
			return false;
		}

		public DataSet GetPendingDNsReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string text3 = "SELECT DN.SysDocID [Doc ID], DN.VoucherID [Number], TransactionDate AS [Date], PONumber [PO Number], P.Description,QuantityReturned,\r\n                            DN.CustomerID + '-' + C.CustomerName AS [Customer], DND.ProductID, DND.UnitID, ISNULL(DND.UnitQuantity,DND.Quantity) AS Quantity,\r\n                            (SELECT DISTINCT ISNULL(SID.UnitQuantity,SID.Quantity) FROM Sales_Invoice_Detail SID WHERE SID.OrderSysDocID= DND.SysDocID AND SID.OrderVoucherID=DND.VoucherID\r\n                            AND SID.ProductID=DND.ProductID AND SID.OrderRowIndex=DND.RowIndex )AS QTY \r\n                            FROM Delivery_Note_Detail DND INNER JOIN Delivery_Note DN ON DND.SysDocID = DN.SysDocID AND DND.VoucherID = DN.VoucherID\r\n                            INNER JOIN Customer C ON DN.CustomerID=C.CustomerID \r\n                            LEFT OUTER JOIN Salesperson ON Salesperson.SalespersonID = DN.SalespersonID\r\n                            LEFT OUTER JOIN Product P ON P.ProductID=DND.ProductID                                   \r\n                               WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False' AND  DND.Quantity-ISNULL(QuantityReturned,0) <>0";
			if (fromDate != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (fromItem != "")
			{
				text3 = text3 + " AND DND.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromItemClass != "")
			{
				text3 = text3 + " AND DND.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
			}
			if (fromItemCategory != "")
			{
				text3 = text3 + " AND DND.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
			}
			if (fromManufacturer != "")
			{
				text3 = text3 + " AND DND.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text3 = text3 + " AND DND.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text3 = text3 + " AND DND.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (customerIDs != "")
			{
				text3 = text3 + " AND DN.CustomerID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND DN.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				text3 = text3 + " AND DN.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				text3 = text3 + " AND DN.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromCustomerArea != "")
			{
				text3 = text3 + " AND DN.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
			}
			if (fromCustomerCountry != "")
			{
				text3 = text3 + " AND DN.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
			}
			if (fromSalesperson != "")
			{
				text3 = text3 + " AND DN.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
			}
			if (fromSalespersonGroup != "")
			{
				text3 = text3 + " AND DN.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
			}
			if (fromSalespersonDivision != "")
			{
				text3 = text3 + " AND DN.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
			}
			if (fromSalespersonArea != "")
			{
				text3 = text3 + " AND DN.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
			}
			if (fromSalespersonCountry != "")
			{
				text3 = text3 + " AND DN.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
			}
			text3 += " ORDER BY CustomerName ";
			FillDataSet(dataSet, "Delivery_Note", text3);
			return dataSet;
		}

		public DataSet GetPickListReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string text3 = "SELECT DISTINCT SI.CUSTOMERID,SI.SysDocID,SI.VoucherID,SI.CustomerID,CS.CustomerName,CustomerAddress,CA.ContactName,SI.TransactionDate,SI.ContainerNumber,EPD.ProductID,\r\n                            SI.SalesPersonID,SI.RequiredDate,CA.AddressPrintFormat AS ShippingAddress,SM.ShippingMethodName,EPD.ProductID,\r\n                            ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                            SI.TermID,TermName,IsVoid,SI.Reference,Discount AS Discount,CA.Phone1,CA.Mobile,CA.Fax,CA.ContactName,\r\n                            Total AS Total,SI.PONumber,SI.Note, SI.InvoiceVoucherID, D.Drivername,D.Note AS [Driver No.],\r\n                            (SELECT TOP 1 RegistrationNumber FROM Vehicle WHERE VehicleID = SI.VehicleID) RegistrationNumber, SI.Reference2,\r\n                            SI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy,V.VehicleName,V.VehicleID,J.JobName,JC.CostCategoryName,Port.PortName                          \r\n                            FROM  Export_PickList SI LEFT JOIN Export_PickList_Detail EPD ON SI.SysDocID=EPD.SysDocID AND SI.VoucherID=EPD.VoucherID INNER JOIN Customer CS ON SI.CustomerID=CS.CustomerID INNER JOIN Product P ON EPD.ProductID = P.ProductID\r\n                            LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                            LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=SI.ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                            LEFT OUTER JOIN Driver D ON D.DriverID=SI.DriverID\r\n                            LEFT OUTER JOIN Vehicle V ON V.VehicleID=SI.VehicleID\r\n                            LEFT JOIN Job J ON J.JobID=SI.JobID\r\n                            LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SI.CostCategoryID\r\n                            LEFT JOIN Port  ON Port.PortID=SI.Port                                   \r\n                                                            where";
			if (fromDate != DateTime.MinValue)
			{
				text3 = text3 + "  SI.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (fromItem != "")
			{
				text3 = text3 + " AND EPD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromItemClass != "")
			{
				text3 = text3 + " AND EPD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
			}
			if (fromItemCategory != "")
			{
				text3 = text3 + " AND EPD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
			}
			if (fromManufacturer != "")
			{
				text3 = text3 + " AND EPD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text3 = text3 + " AND EPD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text3 = text3 + " AND EPD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND SI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				text3 = text3 + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				text3 = text3 + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromCustomerArea != "")
			{
				text3 = text3 + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
			}
			if (fromCustomerCountry != "")
			{
				text3 = text3 + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
			}
			text3 += " ORDER BY CS.CustomerName ";
			FillDataSet(dataSet, "Export_PickList", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "SELECT DISTINCT SI.CUSTOMERID,EPD.SysDocID, EPD.VoucherID, EPD.ProductID, EPD.Description, EPD.LocationID,EPD.RowIndex,PL.Reference ,SI.TransactionDate,\r\n                        ISNULL(EPD.UnitQuantity, EPD.Quantity) AS Quantity,PSP.* FROM Export_PickList_Detail EPD LEFT JOIN Export_PickList SI ON SI.SysDocID=EPD.SysDocID AND SI.VoucherID=EPD.VoucherID LEFT JOIN\r\n                        Product_Lot_Sales_PickList PSP ON EPD.SysDocID=PSP.DocID AND EPD.VoucherID=PSP.InvoiceNumber AND EPD.ProductID=PSP.ItemCode LEFT JOIN\r\n                         Product_Lot PL ON PL.LotNumber=PSP.LotNo WHERE";
			if (fromDate != DateTime.MinValue)
			{
				text3 = text3 + "  SI.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (fromItem != "")
			{
				text3 = text3 + " AND EPD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromItemClass != "")
			{
				text3 = text3 + " AND EPD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
			}
			if (fromItemCategory != "")
			{
				text3 = text3 + " AND EPD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND SI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				text3 = text3 + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				text3 = text3 + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromCustomerArea != "")
			{
				text3 = text3 + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
			}
			if (fromCustomerCountry != "")
			{
				text3 = text3 + " AND SI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
			}
			text3 += " ORDER BY SI.TransactionDate ";
			FillDataSet(dataSet2, "Export_PickList_Detail", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("PickList_REL", new DataColumn[2]
			{
				dataSet.Tables["Export_PickList"].Columns["SysDocID"],
				dataSet.Tables["Export_PickList"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Export_PickList_Detail"].Columns["SysDocID"],
				dataSet.Tables["Export_PickList_Detail"].Columns["VoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public bool UpdatePreviousTransactionsCOGS(DateTime fromDate, string fromItem, string endItem)
		{
			bool result = true;
			try
			{
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string str = StoreConfiguration.ToSqlDateTimeString(fromDate);
				string text = "select  * from  (\r\n                                    SELECT IT.*,  P.ItemType,\r\n                                    ROW_NUMBER() OVER( PARTITION BY IT.ProductID ORDER BY transactiondate ) AS Row\r\n                                    from Inventory_Transactions IT INNER JOIN Product P ON IT.ProductID = P.ProductID WHERE P.ItemType IN (1,7) AND  TransactionDate> '" + str + "' ";
				if (fromItem != "")
				{
					text = text + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + endItem + "' ";
				}
				text += ") s where row = 1   ORDER BY ProductID";
				FillDataSet(inventoryTransactionData, "Inventory_Transactions", text);
				if (inventoryTransactionData != null && inventoryTransactionData.Tables.Count != 0 && inventoryTransactionData.Tables[0].Rows.Count != 0)
				{
					result = new InventoryTransaction(base.DBConfig).UpdateFutureAndPastAffectedTransactionsCOGS(inventoryTransactionData, allocateLots: false, includeAllRecords: true, sqlTransaction);
				}
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

		public DataSet GetSalesByItemClassCategorySummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory, string fromClass, string toClass)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "Select ISNULL(PR.CategoryID,'No Category') AS CategoryID,Cat.CategoryName,ISNULL(PR.ClassID,'No Class') AS ClassID,Cal.ClassName,\r\n\t\t\t\t\t\t\tMIN(UnitPrice) AS MinPrice,\r\n\t\t\t\t\t\t\tMAX(UnitPrice) AS MaxPrice,\r\n\t\t\t\t\t\t\tSUM(SID.Quantity) AS SalesQuantity,\r\n\t\t\t\t\t\t\tSUM(SID.Quantity*UnitPrice) AS SalesAmount\r\n\t\t\t\t\t\t\tFROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON \r\n\t\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Category Cat ON Cat.CategoryID=PR.CategoryID \r\n                            LEFT OUTER JOIN Product_Class Cal ON Cal.ClassID=PR.ClassID ";
				text3 = text3 + "WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ClassID FROM Product WHERE CategoryID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				text3 += " GROUP BY PR.CategoryID,Cat.CategoryName,PR.ClassID,Cal.ClassName";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				DataSet dataSet2 = new DataSet();
				text3 = "Select ISNULL(PR.CategoryID,'No Category') AS CategoryID,Cat.CategoryName,ISNULL(PR.ClassID,'No Class') AS ClassID,Cal.ClassName,\r\n\t\t\t\t\t\t\t-1 * SUM(SID.Quantity) AS ReturnQuantity,\r\n\t\t\t\t\t\t\tSUM(SID.Quantity*UnitPrice) AS ReturnAmount\r\n\t\t\t\t\t\t\tFROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON \r\n\t\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Category Cat ON Cat.CategoryID=PR.CategoryID\r\n                            LEFT OUTER JOIN Product_Class Cal ON Cal.ClassID=PR.ClassID \r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromCategory != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ClassID FROM Product WHERE CategoryID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				text3 += " GROUP BY PR.CategoryID,Cat.CategoryName,PR.ClassID,Cal.ClassName";
				FillDataSet(dataSet2, "Product", text3);
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesByProductBrandSummaryReport(DateTime from, DateTime to, string fromBrand, string toBrand)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "Select ISNULL(PR.BrandID,'No Brand') AS BrandID, PCB.BrandName,\r\n\t\t\t\t\t\t\tMIN(UnitPrice) AS MinPrice,\r\n\t\t\t\t\t\t\tMAX(UnitPrice) AS MaxPrice,\r\n\t\t\t\t\t\t\tSUM(SID.Quantity) AS SalesQuantity,\r\n\t\t\t\t\t\t\tSUM(ISNULL(SID.UnitQuantity,SID.Quantity)*UnitPrice) AS SalesAmount\r\n\t\t\t\t\t\t\tFROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON \r\n\t\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Brand PCB ON PCB.BrandID=PR.BrandID ";
				text3 = text3 + "WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " GROUP BY PR.BrandID,PCB.BrandName";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["BrandID"]
				};
				DataSet dataSet2 = new DataSet();
				text3 = "Select ISNULL(PR.BrandID,'No Brand') AS BrandID, PCB.BrandName,\r\n\t\t\t\t\t\t\t-1 * SUM(SID.Quantity) AS ReturnQuantity,\r\n\t\t\t\t\t\t\tSUM(ISNULL(SID.UnitQuantity,SID.Quantity)*UnitPrice) AS ReturnAmount\r\n\t\t\t\t\t\t\tFROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON \r\n\t\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Product_Brand PCB ON PCB.BrandID=PR.BrandID \r\n\t\t\t\t\t\t    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromBrand != "")
				{
					text3 = text3 + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " GROUP BY PR.BrandID,PCB.BrandName";
				FillDataSet(dataSet2, "Product", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["BrandID"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductStockListCategoryWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive)
		{
			try
			{
				if (!isAsOf)
				{
					asOfDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
				}
				string text = "SELECT   DISTINCT PL.LocationID, LocationName\r\n        FROM Product_Location PL INNER JOIN Location L\r\n        ON L.LocationID=PL.LocationID\r\n        WHERE ISNULL(L.IsConsignOutLocation,'False') = 'False' AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ItemType NOT IN ('3'))";
				if (fromItem != "")
				{
					text = text + " AND PL.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND PL.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromBrand != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND PL.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND PL.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(Inactive,'False')='False'";
				}
				text += " ORDER BY PL.LocationID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Location", text);
				string text2 = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				text = " SELECT DISTINCT P.CategoryID,pb.CategoryName , SUM(IT.Quantity) AS Qty, IT.LocationID, AG.AverageCost* SUM(IT.Quantity)AS Value,\r\n                            SUM(IT.AssetValue) AS [AssetValue],P.StyleId,PS.StyleName\r\n            FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n            INNER JOIN Location Loc ON Loc.LocationID = IT.LocationID\r\n            LEFT OUTER JOIN\r\n            (SELECT T.ProductID, ISNULL(T.AverageCost, 0) AS AverageCost FROM(\r\n            SELECT   ProductID, AverageCost, Row_Number() OVER(Partition by ProductID ORDER BY TransactionDate DESC, TransactionID desc) AS RN\r\n            FROM Inventory_Transactions WHERE TransactionDate <= '" + text2 + "' ) T WHERE RN = 1\r\n            ) AG ON AG.ProductID = IT.ProductID\r\n            left join Product_Category pb on pb.CategoryID = p.CategoryID\r\n            LEFT JOIN Product_Style PS ON PS.StyleId=P.StyleId  \r\n\r\n            WHERE  TransactionDate <= '" + text2 + "'  AND ItemType IN(1, 7)\r\n            AND IT.ProductID IN(SELECT ProductID FROM Product WHERE ItemType NOT IN('3')) ";
				if (fromItem != "")
				{
					text = text + " AND IT.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND IT.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromBrand != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND IT.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND IT.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				text += " GROUP BY P.CategoryID, it.LocationID, PB.CategoryName, ag.AverageCost,p.StyleId,PS.StyleName HAVING SUM(IT.Quantity) <> 0  ORDER BY pb.CategoryName";
				FillDataSet(dataSet, "Category", text);
				dataSet.Relations.Add("Product_Category", dataSet.Tables["Location"].Columns["LocationID"], dataSet.Tables["Category"].Columns["LocationID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductStockListClassWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive)
		{
			try
			{
				if (!isAsOf)
				{
					asOfDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
				}
				string text = "SELECT   DISTINCT PL.LocationID, LocationName\r\n\t\t\t\t\t\t\t\tFROM Product_Location PL INNER JOIN Location L\r\n\t\t\t\t\t\t\t\tON L.LocationID=PL.LocationID\r\n\t\t\t\t\t\t\t\tWHERE ISNULL(L.IsConsignOutLocation,'False') = 'False' AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ItemType NOT IN ('3')) ";
				if (fromItem != "")
				{
					text = text + " AND PL.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND PL.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromBrand != "")
				{
					text = text + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND PL.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND PL.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(Inactive,'False')='False'";
				}
				text += " ORDER BY PL.LocationID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Location", text);
				string text2 = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				text = " SELECT DISTINCT P.ClassID,pc.ClassName , SUM(IT.Quantity) AS Qty, IT.LocationID, AG.AverageCost* SUM(IT.Quantity)AS Value,SUM(IT.AssetValue) AS [AssetValue],P.StyleId,PS.StyleName\r\n FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n INNER JOIN Location Loc ON Loc.LocationID = IT.LocationID\r\n LEFT OUTER JOIN\r\n (SELECT T.ProductID, ISNULL(T.AverageCost, 0) AS AverageCost FROM(\r\n SELECT   ProductID, AverageCost, Row_Number() OVER(Partition by ProductID ORDER BY TransactionDate DESC, TransactionID desc) AS RN\r\n FROM Inventory_Transactions WHERE TransactionDate  <= '" + text2 + "' ) T WHERE RN = 1\r\n ) AG ON AG.ProductID = IT.ProductID\r\n left join Product_Class pc on pc.ClassID = p.ClassID\r\nLEFT JOIN Product_Style PS ON PS.StyleId=P.StyleId\r\n WHERE  TransactionDate  <= '" + text2 + "'  AND P.ItemType IN(1, 7)\r\n AND IT.ProductID IN(SELECT ProductID FROM Product WHERE ItemType NOT IN('3'))";
				if (fromItem != "")
				{
					text = text + " AND IT.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND IT.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromBrand != "")
				{
					text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND IT.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND IT.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				text += " GROUP BY P.ClassID, IT.LocationID, pc.ClassName, ag.AverageCost ,P.StyleId,PS.StyleName HAVING SUM(IT.Quantity) <> 0  ORDER BY pc.ClassName";
				FillDataSet(dataSet, "Class", text);
				dataSet.Relations.Add("Product_Class", dataSet.Tables["Location"].Columns["LocationID"], dataSet.Tables["Class"].Columns["LocationID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool AllocateItemsToLot(string[] productIDs)
		{
			bool result = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				result = new InventoryTransaction(base.DBConfig).AllocateUnallocatedItemsToLot(productIDs, sqlTransaction);
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

		public decimal GetJobBOMProductPurchasePrice(string productID)
		{
			string exp = "SELECT ISNULL(PID.UnitPrice,jbd.Cost) as Cost FROM Product P\r\n\t\t\t\t\t\t\tLEFT JOIN Purchase_Invoice_Detail PID ON P.ProductID=PID.ProductID\r\n\t\t\t\t\t\t\tLEFT JOIN Job_BOM_Detail jbd ON P.ProductID=jbd.ProductID\r\n\t\t\t\t\t\t\tWHERE P.ProductID='" + productID + "'";
			new DataSet();
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return decimal.Parse(obj.ToString());
			}
			return 0m;
		}

		public decimal GetJobBOMLabourCost(string productID)
		{
			string exp = "SELECT TOP 1 ISNULL(jedi.UnitLabourCost,0) From Job_Estimation_Detail_Item jedi WHERE jedi.ProductID='" + productID + "' ORDER BY voucherid desc";
			new DataSet();
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return decimal.Parse(obj.ToString());
			}
			return 0m;
		}

		public DataSet GetSalesManDueReportnot(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, bool showZeroBalance, bool isFC)
		{
			try
			{
				CommonLib.ToSqlDateTimeString(fromDate);
				CommonLib.ToSqlDateTimeString(toDate);
				return new DataSet();
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesManDueReport(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, bool showZeroBalance, bool isFC, string customerIDs)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			string text = "";
			string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
			if (isFC)
			{
				text = "FC";
			}
			string text2 = "0";
			string text3 = "0";
			string text4 = "0";
			if (isFC)
			{
				text2 = "ISNULL(CONDebitFC,0)";
				text3 = "ISNULL(CONCreditFC,0)";
				text4 = "ISNULL(CONAmountFC,0)";
			}
			DataSet dataSet = new DataSet();
			string str = "SELECT * FROM (SELECT CUS.IsInactive I,Flag F,CASE WHEN ISNULL(IsHold,'False')='True' THEN 'Hold' ELSE 'Active' END AS Status, Cus.CustomerID ,CUS.SalesPersonID,\r\n                            CASE WHEN  ShortName IS NULL OR ShortName = '' THEN  CustomerName ELSE CustomerName + ' [' + ShortName + ']' END AS CustomerName,";
			str = ((!isFC) ? (str + "'" + baseCurrencyID + "' AS CUR ") : (str + "ISNULL(Cus.CurrencyID,'" + baseCurrencyID + "') AS CUR "));
			str = str + ", 0.00 AS CurrentBalance, 0.00 AS Month1, 0.00 AS Month2,0.00 AS Month3,0.00 AS Month4,0.00 AS Month5,0.00 AS Month6,0.00 AS [Over], \r\n\r\n                              CASE WHEN CUS.CurrencyID IS NULL OR CUS.CurrencyID = '" + baseCurrencyID + "' THEN\r\n\t\t\t\t\t\t\t  ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit ,0)) FROM ARJournal ARJ WHERE CUS.CustomerID=ARJ.CustomerID AND ISNULL(ISPDCRow,'False') = 'False' AND ISNULL(IsVoid,'False')='False'),0) ELSE\r\n\t\t\t\t\t\t\t   ISNULL((SELECT SUM(ISNULL(Debit" + text + " ,0)- ISNULL(Credit" + text + " ,0)) FROM ARJournal ARJ WHERE ";
			if (isFC)
			{
				str += " ARJ.CurrencyID = Cus.CurrencyID AND ";
			}
			str += "  CUS.CustomerID=ARJ.CustomerID AND  ISNULL(ISPDCRow,'False') = 'False' AND ISNULL(IsVoid,'False')='False'),0) ";
			if (isFC)
			{
				str = str + " + ISNULL((SELECT SUM(ISNULL(ConDebitFC ,0)- ISNULL(ConCreditFC ,0)) FROM ARJournal ARJ WHERE  ISNULL(ARJ.CurrencyID,'" + baseCurrencyID + "') <> Cus.CurrencyID AND  Cus.CustomerID=ARJ.CustomerID  AND ISNULL(ISPDCRow,'False') = 'False' AND ISNULL(IsVoid,'False')='False'),0) ";
			}
			str = str + " END  AS TotalBalance,\r\n\r\n                              0.0 AS Unallocated, 0.0 AS TotalDue,\r\n\t                          (Select CASE WHEN CUS.CurrencyID IS NULL OR CUS.CurrencyID = '" + baseCurrencyID + "' THEN SUM(Amount) ELSE SUM(ISNULL(AmountFC,0) + " + text4 + ") END FROM Cheque_Received CR WHERE Status IN (1,3,4,8) AND CR.PayeeType = 'C' AND CUS.CustomerID = CR.PayeeID  AND ISNULL(IsVoid,'False') = 'False') AS PDC , 0.00 AS NetOffPDC,\r\n\t                          CUS.PaymentTermID,CUS.CreditAmount,CUS.CreditReviewDate,CUS.CreditReviewBy,CUS.AcceptPDC,Rating,IsCustomerSince , CUS.SalesPersonID + '-'+ SP.FullName AS Salesperson, CON.CountryName Country,Area.AreaName Area,CLS.ClassName Class,\r\n\t                          CollectionUserID [Collection User],CA.ContactName,CA.ContactTitle,CA.Email,CA.Mobile,CA.Phone1,CA.Phone2,CA.Fax,CA.PostalCode,\r\n                                (SELECT (cast(CC.CategoryName as nvarchar)+', ') as [text()] FROM Entity_Category_Detail ccd INNER JOIN Entity_Category CC ON CC.CategoryID=CCD.CategoryID\r\n                                  WHERE ccd.EntityID = cus.CustomerID AND CCD.EntityType = 1 order by ccd.CategoryID for XML PATH('')) as Categories,CUS.InsStatus,Cus.InsApprovedAmount, InsRemarks,CollectionRemarks\r\n\r\n                              FROM Customer Cus\r\n                              LEFT OUTER JOIN Area ON Area.AreaID = CUS.AreaID\r\n                              LEFT OUTER JOIN Country CON ON CON.CountryID = CUS.CountryID\r\n                              LEFT OUTER JOIN Customer_Class CLS ON CLS.ClassID = CUS.CustomerClassID\r\n                              LEFT OUTER JOIN Customer_Address CA ON CA.CustomerID = CUS.CustomerID AND CA.AddressID = 'PRIMARY'\r\n                              LEFT OUTER JOIN Salesperson SP ON SP.SalespersonID = CUS.SalesPersonID ) AS Customer \r\n                              WHERE 1 = 1 ";
			if (!showZeroBalance)
			{
				str += " AND (ISNULL(PDC,0)<>0 OR ISNULL(TotalBalance,0) <> 0 )";
			}
			if (customerIDs != "")
			{
				str = str + " AND Customer.CustomerID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				str = str + " AND Customer.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				str = str + " AND Customer.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				str = str + " AND Customer.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromCustomerArea != "")
			{
				str = str + " AND Customer.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
			}
			if (fromCustomerCountry != "")
			{
				str = str + " AND Customer.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
			}
			if (fromSalesperson != "")
			{
				str = str + " AND Customer.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
			}
			if (fromSalespersonGroup != "")
			{
				str = str + " AND Customer.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
			}
			if (fromSalespersonDivision != "")
			{
				str = str + " AND Customer.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
			}
			if (fromSalespersonArea != "")
			{
				str = str + " AND Customer.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
			}
			if (fromSalespersonCountry != "")
			{
				str = str + " AND Customer.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
			}
			str += " ORDER BY CustomerID,CustomerName";
			FillDataSet(dataSet, "Customer", str);
			str = " SELECT ARID, ARJ.CustomerID,ARDate,ARDueDate,\r\n                                CASE WHEN Cus.CurrencyID IS NULL OR Cus.CurrencyID = '" + baseCurrencyID + "' THEN\r\n                                  ISNULL(Debit,0) -   \r\n                                (SELECT ISNULL(SUM(ISNULL(PaymentAmount,0)-ISNULL(RealizedGainLoss,0)),0) FROM AR_Payment_Allocation ARP WHERE ARJ.ARID = ARP.ARJournalID)  ELSE \r\n                                ISNULL(Debit" + text + ",0)  + " + text2 + " -  (SELECT ISNULL(SUM(ISNULL(PaymentAmount" + text + ",0)-ISNULL(RealizedGainLoss,0)),0) FROM AR_Payment_Allocation ARP WHERE ARJ.ARID = ARP.ARJournalID)\tEND\t AS AmountDue\r\n                                  FROM ARJournal ARJ   \r\n                                LEFT OUTER JOIN System_Document SD ON ARJ.SysDocID = SD.SysDocID\r\n                                INNER JOIN Customer Cus ON Cus.CustomerID = ARJ.CustomerID\r\n                                WHERE ISNULL(Debit,0)>0 AND ISNULL(IsVoid,'False')='False' AND  ISNULL(SD.SysDocType,1) NOT IN ( 7 ,12)\r\n                                AND (SELECT CASE WHEN Cus.CurrencyID IS NULL OR Cus.CurrencyID = '" + baseCurrencyID + "' THEN ISNULL(SUM(ISNULL(PaymentAmount,0)-ISNULL(RealizedGainLoss,0)),0) ELSE\r\n                                 ISNULL(SUM(ISNULL(PaymentAmount" + text + ",0)),0) END   FROM AR_Payment_Allocation PA\r\n\t                                WHERE ARJ.ARID = PA.ARJournalID)< (CASE WHEN Cus.CurrencyID IS NULL OR Cus.CurrencyID = '" + baseCurrencyID + "' THEN ISNULL(Debit,0) ELSE  ISNULL(Debit" + text + ",0) + " + text2 + " END) ";
			if (isFC)
			{
				str = str + " AND ISNULL(ARJ.CurrencyID,'" + baseCurrencyID + "') =  ISNULL(Cus.CurrencyID,'" + baseCurrencyID + "') ";
			}
			if (customerIDs != "")
			{
				str = str + " AND ARJ.CustomerID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				str = str + " AND ARJ.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				str = str + " AND ARJ.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				str = str + " AND ARJ.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromCustomerArea != "")
			{
				str = str + " AND ARJ.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
			}
			if (fromCustomerCountry != "")
			{
				str = str + " AND ARJ.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
			}
			str = str + "               UNION\r\n\t     \r\n                            SELECT ARID, ARJ.CustomerID,ARDate, ARDueDate, \r\n                                -1 *    CASE WHEN Cus.CurrencyID IS NULL OR Cus.CurrencyID = '" + baseCurrencyID + "' THEN ISNULL(Credit,0) ELSE ISNULL(Credit" + text + ",0) + " + text3 + " END    \r\n                                +\r\n\t\t\t\t\t\t\t\t(SELECT ISNULL(SUM(ISNULL(Amount,0)),0) FROM Cheque_Received Chq\r\n                                WHERE ARJ.SysDocID=Chq.SysDocID AND ARJ.VoucherID=Chq.VoucherID AND ARJ.CustomerID = Chq.PayeeID AND Status = 9)\r\n                                +\r\n                                (SELECT CASE WHEN Cus.CurrencyID IS NULL OR Cus.CurrencyID = '" + baseCurrencyID + "' THEN ISNULL(SUM(ISNULL(PaymentAmount,0)),0) ELSE ISNULL(SUM(ISNULL(PaymentAmount" + text + ",0)),0) END FROM AR_Payment_Allocation ARP\r\n                                WHERE ARJ.SysDocID=ARP.PaymentSysDocID AND ARJ.VoucherID=ARP.PaymentVoucherID  AND ARJ.ARID = ARP.PaymentARID AND ARJ.CustomerID = ARP.CustomerID)  AS Unallocated\r\n                                  FROM ARJournal ARJ   \r\n                                LEFT OUTER JOIN System_Document SD ON ARJ.SysDocID = SD.SysDocID\r\n                                INNER JOIN Customer CUS ON ARJ.CustomerID=Cus.CustomerID\r\n                                WHERE CASE WHEN Cus.CurrencyID IS NULL OR Cus.CurrencyID = '" + baseCurrencyID + "' THEN  ISNULL(Credit,0) ELSE ISNULL(Credit" + text + ",0) + " + text3 + " END > 0 AND ISNULL(IsVoid,'False')='False'  AND ISNULL(SD.SysDocType,1)  NOT IN ( 7 ,12) \r\n                                  AND (SELECT CASE WHEN Cus.CurrencyID IS NULL OR Cus.CurrencyID = '" + baseCurrencyID + "' THEN  ISNULL(SUM(ISNULL(PaymentAmount,0)),0) ELSE  ISNULL(SUM(ISNULL(PaymentAmount" + text + ",0)),0) END FROM AR_Payment_Allocation PA\r\n\t                                WHERE PA.PaymentSysDocID=ARJ.SysDocID AND PA.PaymentVoucherID=ARJ.VoucherID  AND PA.PaymentARID= ARJ.ARID AND PA.CustomerID = ARJ.CustomerID)< CASE WHEN Cus.CurrencyID IS NULL OR Cus.CurrencyID = '" + baseCurrencyID + "' THEN  ISNULL(ISNULL(Credit,0),0) ELSE  ISNULL(ISNULL(Credit" + text + ",0)+  " + text2 + ",0) END ";
			if (customerIDs != "")
			{
				str = str + " AND ARJ.CustomerID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				str = str + " AND ARJ.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				str = str + " AND ARJ.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				str = str + " AND ARJ.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromCustomerArea != "")
			{
				str = str + " AND ARJ.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
			}
			if (fromCustomerCountry != "")
			{
				str = str + " AND ARJ.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
			}
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Aging", str);
			DataHelper dataHelper = new DataHelper(base.DBConfig);
			DataSet dataSet3 = new DataSet();
			dataSet3 = new CompanyOption(base.DBConfig).GetCompanyOptionList(76, 107);
			bool companyOption = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingByDueDate, defaultValue: true);
			bool companyOption2 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging1, defaultValue: true);
			bool companyOption3 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging2, defaultValue: true);
			bool companyOption4 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging3, defaultValue: true);
			bool companyOption5 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging4, defaultValue: true);
			bool companyOption6 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging5, defaultValue: true);
			bool companyOption7 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.ShowAging6, defaultValue: true);
			dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom0, 0);
			int companyOption8 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom1, 1);
			int companyOption9 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom2, 31);
			int companyOption10 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom3, 61);
			int companyOption11 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom4, 91);
			int companyOption12 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom5, 121);
			int companyOption13 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom6, 151);
			dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingFrom7, 181);
			int companyOption14 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo0, 0);
			int companyOption15 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo1, 30);
			int companyOption16 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo2, 60);
			int companyOption17 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo3, 90);
			int companyOption18 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo4, 120);
			int companyOption19 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo5, 150);
			int companyOption20 = dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo6, 180);
			dataHelper.GetCompanyOption(dataSet3, CompanyOptionsEnum.AgingTo7, 999);
			int num = 1;
			int num2 = 0;
			if (companyOption7)
			{
				num = 6;
				num2 = companyOption20;
			}
			else if (companyOption6)
			{
				num = 5;
				num2 = companyOption19;
			}
			else if (companyOption5)
			{
				num = 4;
				num2 = companyOption18;
			}
			else if (companyOption4)
			{
				num = 3;
				num2 = companyOption17;
			}
			else if (companyOption3)
			{
				num = 2;
				num2 = companyOption16;
			}
			else if (companyOption2)
			{
				num = 1;
				num2 = companyOption15;
			}
			else
			{
				num = 0;
				num2 = companyOption14;
			}
			for (int i = 0; i < dataSet.Tables["Customer"].Rows.Count; i++)
			{
				DataRow dataRow = dataSet.Tables["Customer"].Rows[i];
				string str2 = dataRow["CustomerID"].ToString();
				_ = DateTime.Today;
				new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
				DataRow[] array = dataSet2.Tables["Aging"].Select("CustomerID = '" + str2 + "'");
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				decimal num5 = default(decimal);
				decimal num6 = default(decimal);
				decimal num7 = default(decimal);
				decimal num8 = default(decimal);
				decimal num9 = default(decimal);
				decimal num10 = default(decimal);
				decimal num11 = default(decimal);
				decimal d = default(decimal);
				for (int j = 0; j < array.Length; j++)
				{
					DateTime d2 = DateTime.Parse(array[j]["ARDate"].ToString());
					if (companyOption && array[j]["ARDueDate"] != DBNull.Value)
					{
						d2 = DateTime.Parse(array[j]["ARDueDate"].ToString());
					}
					if (!companyOption)
					{
						d2 = d2.AddDays(companyOption14);
					}
					if (array[j]["AmountDue"] != DBNull.Value && decimal.Parse(array[j]["AmountDue"].ToString()) > 0m)
					{
						decimal.Parse(array[j]["AmountDue"].ToString());
						int num12 = (DateTime.Today - d2).Days - companyOption14;
						if (num12 <= 0)
						{
							num3 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption8 && num12 <= companyOption15)
						{
							num5 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption9 && num12 <= companyOption16)
						{
							num6 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption10 && num12 <= companyOption17)
						{
							num7 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption11 && num12 <= companyOption18)
						{
							num8 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption12 && num12 <= companyOption19)
						{
							num9 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						else if (num12 >= companyOption13 && num12 <= companyOption20)
						{
							num10 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						if (num12 > num2)
						{
							num4 += decimal.Parse(array[j]["AmountDue"].ToString());
						}
						if (num12 >= companyOption8)
						{
							d += decimal.Parse(array[j]["AmountDue"].ToString());
						}
					}
					else if (array[j]["AmountDue"] != DBNull.Value)
					{
						num11 += Math.Abs(decimal.Parse(array[j]["AmountDue"].ToString()));
					}
				}
				decimal result = default(decimal);
				decimal.TryParse(dataRow["TotalBalance"].ToString(), out result);
				dataRow["CurrentBalance"] = num3;
				dataRow["Month1"] = num5;
				dataRow["Month2"] = num6;
				dataRow["Month3"] = num7;
				dataRow["Month4"] = num8;
				dataRow["Month5"] = num9;
				dataRow["Month6"] = num10;
				dataRow["Over"] = num4;
				dataRow["Unallocated"] = num11;
				if (d - num11 > 0m)
				{
					dataRow["TotalDue"] = d - num11;
				}
				else
				{
					dataRow["TotalDue"] = 0;
				}
				decimal result2 = default(decimal);
				decimal.TryParse(dataRow["PDC"].ToString(), out result2);
				if (flag)
				{
					dataRow["NetOffPDC"] = result - result2;
				}
				else
				{
					dataRow["NetOffPDC"] = result + result2;
				}
				if (!showZeroBalance && result == 0m && result2 == 0m)
				{
					dataSet.Tables["Customer"].Rows.RemoveAt(i);
					i--;
				}
			}
			for (int num13 = 6; num13 > num; num13--)
			{
				dataSet.Tables["Customer"].Columns.Remove("Month" + num13.ToString());
			}
			return dataSet;
		}

		public bool GetItemExistsinCategory(string productID, string customerID)
		{
			string exp = "SELECT bd.ProductID from BOM b INNER JOIN BOM_Detail bd ON b.BOMID=bd.BOMID INNER JOIN Product P ON P.CategoryID=bd.ProductID\r\n                            INNER JOIN Product_Category pc ON  bd.ProductID=pc.CategoryID\r\n                             WHERE P.ProductID='" + customerID + "' AND B.BOMID='" + productID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return true;
			}
			return false;
		}

		public DataSet GetsufficientQuantityforPackage(string productID, string customerID)
		{
			string textCommand = "SELECT bd.ProductID,pc.CategoryID,bd.Quantity from BOM b INNER JOIN BOM_Detail bd ON b.BOMID=bd.BOMID INNER JOIN Product P ON P.CategoryID=bd.ProductID\r\n                            INNER JOIN Product_Category pc ON  bd.ProductID=pc.CategoryID\r\n                             WHERE P.ProductID='" + customerID + "' AND B.BOMID='" + productID + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "PackageDtls", textCommand);
			return dataSet;
		}

		public byte[] GetItemFeatures(string productID)
		{
			string textCommand = "SELECT p.ProductID,p.Description,p.UnitID,u.UnitName,p.CategoryID,pc.CategoryName,p.BrandID,pb.BrandName,\r\n                            p.ManufacturerID,pm.ManufacturerName,p.Quantity,C.CountryName, P.Attribute,\r\n                            Case ItemType When 1 then 'Inventory' when 2 then 'Non-Inventory' when 3 then 'Service' when 4 then 'Discount' when 5 then 'Consignment Item' when 6 \r\n                            then 'Inventory 3PL' when 7 then 'Assembly' when 8 then 'Project Fee' END AS [Item  Type], ReorderLevel, \r\n                            MinPrice, UnitPrice1,UnitPrice2,UnitPrice3,StandardCost,LastCost, VendorRef,\r\n                            ISNULL((select Top 1 CASE WHEN FactorType='D' THEN  PID.UnitPrice/PID.UnitFactor\r\n                            WHEN FactorType='M'  THEN  PID.UnitPrice*PID.UnitFactor\r\n                            ELSE PID.UnitPrice END AS UnitPrice from Purchase_Invoice_Detail PID \r\n                            INNER JOIN Purchase_Invoice PI ON PI.SysDocID=PID.SysDocID AND PI.VoucherID=PID.VoucherID INNER JOIN Product P1 \r\n                            ON PID.ProductID=P1.ProductID  where p1.ProductID=P.ProductID order by TransactionDate desc),LastCost)AS [LAST COST],\r\n                            ISNULL(CAST((SELECT TOP 1 IT.AssetValue/IT.Quantity FROM Inventory_Transactions IT WHERE IT.Quantity > 0 AND IT.ProductId=P.ProductID ORDER BY IT.TransactionDate Desc) AS DECIMAL(10,4)),0 ) AS AverageCost\r\n\r\n                            from Product p\r\n                            LEFT JOIN Unit u ON u.UnitID = p.UnitID\r\n                            LEFT JOIN Product_Category pc ON pc.CategoryID = p.CategoryID\r\n                            LEFT JOIN Product_Brand pb ON pb.BrandID = p.BrandID \r\n                            LEFT JOIN Product_Manufacturer pm ON pm.ManufacturerID=p.ManufacturerID\r\n                            LEFT JOIN Country C ON C.CountryID=p.Origin\r\n                            WHERE P.ProductID='" + productID + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "ItemFeatures", textCommand);
			return CommonLib.CompressDataSet(dataSet);
		}

		public DataSet GetPackageID(string productID)
		{
			string textCommand = "SELECT TOP 1 B.BOMID,B.Amount,B.PricePercent,P.UnitPrice1,(P.UnitPrice1*B.PricePercent)/100 as PricetoAllocate FROM BOM B INNER JOIN BOM_Detail BD ON B.BOMID=BD.BOMID INNER JOIN Product p ON p.CategoryID=BD.ProductID\r\n                            WHERE P.ProductID='" + productID + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "packagedtls", textCommand);
			DataSet dataSet2 = new DataSet();
			textCommand = " SELECT UnitPrice2  as DefaultPricetoAllocate FROM Product where ProductID='" + productID + "'";
			FillDataSet(dataSet2, "defaultdtls", textCommand);
			dataSet.Merge(dataSet2);
			return dataSet;
		}

		public DataSet GetW3PLInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems, string fromCustomer, string toCustomer, string fromCusClass, string toCusClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				if (fromWarehouse != "" || toWarehouse != "")
				{
					text3 = " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				string str = "SELECT DISTINCT IT.ProductID [Item Code] ,Product.Description AS [Item Description],C.CustomerName,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID " + text3 + " AND IT2.TransactionDate<'" + text + "'),0) AS OpeningQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity*IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<'" + text + "'),0) AS OpeningValue,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<='" + text2 + "'),0) AS ClosingQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity * IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<='" + text2 + "'),0) AS ClosingValue\r\n\t\t\t        FROM Inventory_Transactions IT INNER JOIN Product ON IT.ProductID=Product.ProductID  LEFT JOIN Customer C ON C.CustomerID=IT.PayeeID WHERE ";
				str = str + " Product.ItemType  IN ('9') AND TransactionDate < '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND IT.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (customerIDs != "")
				{
					str = str + " AND IT.PayeeID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND IT.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCusClass != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCusClass + "' AND '" + toCusClass + "') ";
				}
				if (fromGroup != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromArea != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
				}
				if (fromCountry != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "') ";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += " GROUP BY IT.ProductID,Product.Description,C.CustomerName ORDER BY IT.ProductID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", str);
				DataSet dataSet2 = new DataSet();
				str = "SELECT 'OPENING STOCK ' SysDocID,'0000000' VoucherID,'0' SysDocType, '" + text + "' AS TransactionDate,IT.ProductID,Product.Description,'' AS LocationID, 0 AS UnitPrice, 0 AS AverageCost,'' AS Reference ,\r\n                                  CASE  WHEN  SUM(ISNULL(IT.Quantity, 0) )> 0 THEN SUM(ISNULL(IT.Quantity, 0) ) ELSE 0 END AS [Qty In],\r\n                                  CASE  WHEN SUM(ISNULL(IT.Quantity, 0) )< 0 THEN -1 * SUM(ISNULL(IT.Quantity, 0) ) ELSE 0 END AS [Qty Out], 0 AssetValue,\r\n                                  'OPENING STOCK'  AS PayeeName\r\n                                  From Inventory_Transactions IT INNER JOIN Product ON Product.ProductID=IT.ProductID \r\n                                  LEFt OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                  Vendor ON IT.PayeeID=Vendor.VendorID \r\n                                  WHERE  convert(date, transactionDate) < '" + text + "'";
				if (fromWarehouse != "")
				{
					str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (customerIDs != "")
				{
					str = str + " AND IT.PayeeID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND IT.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCusClass != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCusClass + "' AND '" + toCusClass + "') ";
				}
				if (fromGroup != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromArea != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
				}
				if (fromCountry != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "') ";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += "GROUP BY IT.ProductID,Product.Description  UNION ALL ";
				str = str + "SELECT SysDocID,VoucherID,SysDocType,CAST(TransactionDate AS DATE) as TransactionDate,IT.ProductID,Product.Description,LocationID,UnitPrice,IT.AverageCost,Reference ,\r\n\t\t\t\t\tCASE  WHEN IT.Quantity > 0 THEN ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty In],\r\n\t\t\t\t\tCASE  WHEN IT.Quantity < 0 THEN -1 * ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty Out],AssetValue,\r\n\t\t\t\t\tPayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName END) AS PayeeName\r\n\t\t\t\t\tFrom Inventory_Transactions IT INNER JOIN Product ON Product.ProductID=IT.ProductID \r\n\t\t\t\t\tLEFt OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\tVendor ON IT.PayeeID=Vendor.VendorID \r\n\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromCustomer != "")
				{
					str = str + " AND IT.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCusClass != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCusClass + "' AND '" + toCusClass + "') ";
				}
				if (fromGroup != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromArea != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
				}
				if (fromCountry != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "') ";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += " ORDER BY TransactionDate, VoucherID";
				FillDataSet(dataSet2, "Inventory_Transactions", str);
				dataSet.Merge(dataSet2);
				dataSet.Tables["Inventory_Transactions"].Columns.Add("Balance");
				foreach (DataRow row in dataSet.Tables["Product"].Rows)
				{
					int num = Convert.ToInt32(decimal.Parse(row["OpeningQuantity"].ToString()));
					string str2 = row["Item Code"].ToString();
					string filterExpression = "ProductID='" + str2 + "' ";
					DataRow[] array = dataSet.Tables["Inventory_Transactions"].Select(filterExpression);
					foreach (DataRow obj2 in array)
					{
						int.TryParse(obj2["Qty Out"].ToString(), out int result);
						int.TryParse(obj2["Qty In"].ToString(), out int result2);
						obj2["Balance"] = (num = num + result2 - result);
					}
				}
				dataSet.Relations.Add("Balance Detail", dataSet.Tables["Product"].Columns["Item Code"], dataSet.Tables["Inventory_Transactions"].Columns["ProductID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetW3PLProductStockListLocationWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromCustomer, string toCustomer, string fromCusClass, string toCusClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			try
			{
				if (!isAsOf)
				{
					asOfDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
				}
				string str = "SELECT   DISTINCT PL.LocationID, LocationName\r\n\t\t\t\t\t\t\t\tFROM Product_Location PL INNER JOIN Location L\r\n\t\t\t\t\t\t\t\tON L.LocationID=PL.LocationID\r\n\t\t\t\t\t\t\t\tWHERE 1=1 AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ItemType IN ('9'))";
				if (fromItem != "")
				{
					str = str + " AND PL.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					str = str + " AND PL.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					str = str + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					str = str + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					str = str + " AND PL.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromLocation != "")
				{
					str = str + " AND PL.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					str = str + " AND PL.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					str += " AND ISNULL(Inactive,'False')='False'";
				}
				str += " ORDER BY PL.LocationID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Location", str);
				string text = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				str = "  SELECT DISTINCT IT.ProductID,P.Description,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,C.CustomerName, SUM(IT.Quantity) AS Quantity,IT.LocationID,\r\n                               AG.AverageCost AS AverageCost , AG.AverageCost * SUM(IT.Quantity)  AS Value \r\n                            FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n                            INNER JOIN Location Loc ON Loc.LocationID = IT.LocationID  \r\n                            LEFT JOIN Customer C ON C.CustomerID=IT.PayeeID\r\n                            LEFT OUTER JOIN   (SELECT T.ProductID,ISNULL(T.AverageCost,0) AS AverageCost FROM (\r\n\t\t\t\t\t\t SELECT   ProductID, AverageCost, Row_Number() OVER (Partition by ProductID ORDER BY TransactionDate DESC,TransactionID desc) AS RN FROM Inventory_Transactions WHERE TransactionDate <= '" + text + "' ) T WHERE RN =1\r\n\t\t\t\t\t\t\t\t\t\t\t\t ) AG ON AG.ProductID = IT.ProductID\r\n                            WHERE  TransactionDate <= '" + text + "' AND ItemType IN(9)  ";
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					str = str + " AND IT.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromLocation != "")
				{
					str = str + " AND IT.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					str = str + " AND IT.LocationID <= '" + toLocation + "'";
				}
				if (customerIDs != "")
				{
					str = str + " AND IT.PayeeID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND IT.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCusClass != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCusClass + "' AND '" + toCusClass + "') ";
				}
				if (fromGroup != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromArea != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE AreaId BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
				}
				if (fromCountry != "")
				{
					str = str + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CountryId BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
				}
				if (!isInactive)
				{
					str += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				str += "  GROUP BY IT.ProductID,IT.LocationID,P.Description,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID ,AG.AverageCost,C.CustomerName ";
				if (!showZero)
				{
					str += "  HAVING SUM(IT.Quantity)<>0 ";
				}
				str += " ORDER BY IT.ProductID";
				FillDataSet(dataSet, "Quantity", str);
				dataSet.Relations.Add("Product_Location", dataSet.Tables["Location"].Columns["LocationID"], dataSet.Tables["Quantity"].Columns["LocationID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetW3PLInventoryAgingSummaryReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, DateTime asOfDate, bool showZero, bool isInactive, string fromCustomer, string toCustomer, string fromCusClass, string toCusClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			try
			{
				new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
				_ = (asOfDate < DateTime.Today);
				if (asOfDate == DateTime.MaxValue)
				{
					CommonLib.ToSqlDateTimeString(DateTime.Parse("1-1-2099"));
				}
				else
				{
					CommonLib.ToSqlDateTimeString(asOfDate);
				}
				DataHelper dataHelper = new DataHelper(base.DBConfig);
				DataSet dataSet = new DataSet();
				dataSet = new CompanyOption(base.DBConfig).GetCompanyOptionList(143, 158);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowInvAging1, defaultValue: true);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowInvAging2, defaultValue: true);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowInvAging3, defaultValue: true);
				dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.ShowInvAging4, defaultValue: true);
				int companyOption = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvFrom1, 0);
				int companyOption2 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvFrom2, 31);
				int companyOption3 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvFrom3, 61);
				int companyOption4 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvFrom4, 91);
				int companyOption5 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvTo1, 30);
				int companyOption6 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvTo2, 60);
				int companyOption7 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvTo3, 90);
				int companyOption8 = dataHelper.GetCompanyOption(dataSet, CompanyOptionsEnum.AgingInvTo4, 120);
				string text = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				string text2 = "Age_" + companyOption + "to" + companyOption5;
				string text3 = "Age_" + companyOption2 + "to" + companyOption6;
				string text4 = "Age_" + companyOption3 + "to" + companyOption7;
				string text5 = "Age_" + companyOption4 + "to" + companyOption8;
				string text6 = "WITH Age (ProductID,A,B,C,D) AS (\r\n                                SELECT ProductID,\r\n                                SUM(CASE WHEN DTAGE BETWEEN '" + companyOption + "' AND '" + companyOption5 + "' THEN Quantity ELSE 0 END) AS A,\r\n                                SUM(CASE WHEN DTAGE BETWEEN '" + companyOption2 + "' AND '" + companyOption6 + "' THEN Quantity ELSE 0 END) AS B,\r\n                                SUM(CASE WHEN DTAGE BETWEEN '" + companyOption3 + "' AND '" + companyOption7 + "' THEN Quantity ELSE 0 END) AS C,\r\n                                SUM(CASE WHEN DTAGE >= '" + companyOption4 + "' THEN Quantity ELSE 0 END) AS D\r\n                                FROM (\r\n                                select  it.ProductID , convert(date,it.TransactionDate,106) CDt , Quantity , DATEDIFF(d,TransactionDate, '" + text + "')  DTAGE,IT.PayeeID,C.CustomerName\r\n\r\n                                  from  Inventory_Transactions  IT \r\n                                    LEFT JOIN Customer C ON C.CustomerID=IT.PayeeID                                    \r\n                                    where IsVoid is null and  \r\n                                 -- ProductID  in (select Productid from   tmp_crstock ) and\r\n                                   Quantity > 0  \r\n\r\n                                )tmp_IT\r\n                                GROUP BY ProductID )\r\n                                SELECT a.ProductID, ClassID, CategoryID,PayeeID,CustomerName,Description,\r\n                                CASE WHEN A > OPSTK.StkQty THEN OPSTK.StkQty ELSE A END AS " + text2 + ",\r\n                                CASE WHEN (OPSTK.StkQty-A)>0 THEN CASE WHEN B>(OPSTK.StkQty-A) THEN (OPSTK.StkQty-A) ELSE B END ELSE 0 END AS  " + text3 + ",\r\n                                CASE WHEN (OPSTK.StkQty-(A+B))>0 THEN CASE WHEN C>(OPSTK.StkQty-(A+B)) THEN (OPSTK.StkQty-(A+B)) ELSE C END ELSE 0 END AS  " + text4 + ",\r\n                                CASE WHEN (OPSTK.StkQty-(A+B+C))>0 THEN CASE WHEN D>(OPSTK.StkQty-(A+B+C)) THEN (OPSTK.StkQty-(A+B+C)) ELSE D END ELSE 0 END AS  " + text5 + ",\r\n                                OPSTK.StkQty AS TotalStock\r\n                                FROM Age a\r\n                                INNER JOIN ( select   product.ProductID , product.CategoryID , product.ClassID, product.Description,  sum(it.quantity)  StkQty ,IT.PayeeID,C.CustomerName\r\n                                from \r\n                                Inventory_Transactions IT INNER JOIN\r\n                                Product ON it.ProductID = Product.ProductID\r\n                                LEFT JOIN Customer C ON C.CustomerID=IT.PayeeID\r\n                                where  isnull(IsVoid,'') =''   and convert(date,TransactionDate,106)  <=  '" + text + "'\r\n                                and  ItemType   IN(9)  ";
				if (!isInactive)
				{
					text6 += " AND ISNULL(Product.IsInactive,'False')='False'";
				}
				if (fromItem != "")
				{
					text6 = text6 + " AND IT.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text6 = text6 + " AND IT.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text6 = text6 + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text6 = text6 + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text6 = text6 + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text6 = text6 + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromLocation != "")
				{
					text6 = text6 + " AND PL.LocationID BETWEEN'" + fromLocation + "' AND '" + toLocation + "'";
				}
				if (customerIDs != "")
				{
					text6 = text6 + " AND IT.PayeeID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					text6 = text6 + " AND IT.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCusClass != "")
				{
					text6 = text6 + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCusClass + "' AND '" + toCusClass + "') ";
				}
				if (fromGroup != "")
				{
					text6 = text6 + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
				}
				if (fromArea != "")
				{
					text6 = text6 + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
				}
				if (fromCountry != "")
				{
					text6 = text6 + " AND IT.PayeeID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
				}
				text6 += "group by  product.ProductID , product.CategoryID , product.ClassID , product.Description,IT.PayeeID,C.CustomerName ";
				if (!showZero)
				{
					text6 += " having sum(it.quantity) <> 0";
				}
				text6 += " ) OPSTK ON a.ProductID = OPSTK.ProductID";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Inventory_Aging", text6);
				return dataSet2;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseByItemVendorBuyerReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string str = "Select SID.ProductID,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.VendorID + '-' + VendorName AS 'Vendor', SI.BuyerID + '-' + FullName AS 'Buyer', SID.Description,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n\t\t\t\t\t\tSID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF,SI.BOLNo\r\n\t\t\t\t\t\tFROM Purchase_Invoice_NonInv_Detail SID INNER JOIN Purchase_Invoice_NonInv SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tVendor ON Vendor.VendorID=SI.VendorID\r\n                        LEFT JOIN Buyer ON Buyer.BuyerID = SI.BuyerID\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (vendorIDs != "")
				{
					str = str + " AND SI.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					str = str + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromVendorClass != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromVendorClass + "' AND '" + toVendorClass + "') ";
				}
				if (fromVendorGroup != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromVendorGroup + "' AND '" + toVendorGroup + "') ";
				}
				if (fromBuyer != "")
				{
					str = str + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND SID.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Purchase", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseByInventoryItemVendorBuyerReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string str = "Select SID.ProductID,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.VendorID + '-' + VendorName AS 'Vendor', SI.BuyerID + '-' + FullName AS 'Buyer', SID.Description,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n\t\t\t\t\t\tSID.Quantity,UnitPrice, (SID.Quantity*UnitPrice) AS Amount,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.UnitPrice/SID.UnitFactor) WHEN SID.FactorType='M' THEN (SID.UnitPrice*SID.UnitFactor) ELSE SID.UnitPrice END) AS UnitF,\r\n                        (CASE WHEN SID.FactorType='D' THEN (SID.Quantity*(SID.UnitPrice/SID.UnitFactor)) WHEN SID.FactorType='M' THEN (SID.Quantity*(SID.UnitPrice*SID.UnitFactor)) ELSE (SID.Quantity*UnitPrice) END) AS AmountF\r\n\t\t\t\t\t\t,PC.CategoryName,PM.ManufacturerName,PS.StyleName,C.CountryName FROM Purchase_Invoice_Detail SID INNER JOIN Purchase_Invoice SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tVendor ON Vendor.VendorID=SI.VendorID\r\n                        LEFT JOIN Buyer ON Buyer.BuyerID = SI.BuyerID\r\n                        LEFT OUTER JOIN Product_Category PC On PC.CategoryId=PR.CategoryId\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Manufacturer PM On PM.ManufacturerId=PR.ManufacturerId\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Style PS On PS.StyleId=PR.StyleId\r\n\t\t\t\t\t\tLEFT OUTER JOIN Country C On C.CountryId=PR.Origin\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (vendorIDs != "")
				{
					str = str + " AND SI.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					str = str + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromVendorClass != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromVendorClass + "' AND '" + toVendorClass + "') ";
				}
				if (fromVendorGroup != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromVendorGroup + "' AND '" + toVendorGroup + "') ";
				}
				if (fromBuyer != "")
				{
					str = str + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND SID.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Purchase", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseSubContractByItemVendorBuyerReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string fromJob, string toJob)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string str = "Select SID.ProductID,SID.*,\r\n\t\t\t\t\t\tTransactionDate,SID.VoucherID,SI.VendorID + '-' + VendorName AS 'Vendor', SI.BuyerID + '-' + FullName AS 'Buyer', SID.Description,J.JobName,\r\n\t\t\t\t\t\tCase ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type],\r\n\t\t\t\t\t\tSID.Amount  AS Amount\r\n\t\t\t\t\t\tFROM Project_Subcontract_PI_Detail SID INNER JOIN Project_Subcontract_PI SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tVendor ON Vendor.VendorID=SI.VendorID\r\n                        LEFT JOIN Buyer ON Buyer.BuyerID = SI.BuyerID\r\n                        LEFT JOIN Job J ON J.JobID=SI.JobID\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromVendor != "")
				{
					str = str + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromVendorClass != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromVendorClass + "' AND '" + toVendorClass + "') ";
				}
				if (fromVendorGroup != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromVendorGroup + "' AND '" + toVendorGroup + "') ";
				}
				if (fromBuyer != "")
				{
					str = str + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND SID.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					str = str + " AND SI.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "PurchaseSubContract", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProjectSubContractOrderReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromJob, string toJob)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string str = "Select SID.ProductID,SID.*,\r\n\t\t\t\t\t\tSI.VendorID + '-' + VendorName AS 'Vendor', SI.BuyerID + '-' + FullName AS 'Buyer', SID.Description,SI.JobID,J.JobName \r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tFROM Project_Subcontract_PO_Detail SID INNER JOIN Project_Subcontract_PO SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Product PR ON SID.ProductID=PR.ProductID INNER JOIN \r\n\t\t\t\t\t\tVendor ON Vendor.VendorID=SI.VendorID\r\n                        LEFT JOIN Buyer ON Buyer.BuyerID = SI.BuyerID\r\n                        LEFT JOIN Job J ON J.JobID=SI.JobID\r\n\t\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					str = str + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					str = str + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromVendor != "")
				{
					str = str + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromVendorClass != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromVendorClass + "' AND '" + toVendorClass + "') ";
				}
				if (fromVendorGroup != "")
				{
					str = str + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromVendorGroup + "' AND '" + toVendorGroup + "') ";
				}
				if (fromBuyer != "")
				{
					str = str + " AND SI.BuyerID BETWEEN '" + fromBuyer + "' AND '" + toBuyer + "' ";
				}
				if (fromJob != "")
				{
					str = str + " AND SI.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "PurchaseSubContract", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool SetFlag(string productID, byte flagID)
		{
			try
			{
				string exp = (flagID <= 0) ? ("UPDATE Product SET Flag = NULL WHERE ProductID = '" + productID + "'") : ("UPDATE Product SET Flag = " + flagID + " WHERE ProductID = '" + productID + "'");
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductComboRowByID(string IDField)
		{
			string textCommand = "SELECT TOP 1 ProductID,Description,UPC FROM Product \r\n                            WHERE ProductID='" + IDField + "' OR UPC ='" + IDField + "' AND IsInactive=0";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "productCombo", textCommand);
			return dataSet;
		}

		public DataSet GetProducts()
		{
			string textCommand = "SELECT  ProductID[Code],Description[Name], ItemType FROM Product where\r\n                           IsInactive=0";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "products", textCommand);
			return dataSet;
		}

		public DataSet GetProductsForCombo()
		{
			string textCommand = "SELECT DISTINCT P.ProductID[Code],P.Description[Name], p.ItemType, Description2 AS ALIAS, PB.BrandName AS Brand, PC.ClassName AS Class, PCT.CategoryName AS Category ,PCT.CategoryID, \r\n                        (SELECT  SUM(ISNULL(POD.Quantity,0)-ISNULL(POD.QuantityReceived,0)) FROM  Purchase_Order_Detail POD where POD.ProductID=P.ProductID)AS [Total OrderedQty],\r\n                        (SELECT  TOP 1 PO.ETA  FROM  Purchase_Order_Detail POD  INNER JOIN Purchase_Order PO ON POD.SysDocID=PO.SysDocID AND POD.VoucherID=PO.VoucherID where POD.ProductID=P.ProductID ORDER BY ETA DESC)AS [Exp.DeliveryDate]\r\n                        FROM Product P \r\n                        LEFT JOIN Product_Brand PB ON P.BrandID=PB.BrandID\r\n                        LEFT JOIN Product_Class PC ON PC.ClassID =P.ClassID\r\n                        LEFT JOIN Product_Category PCT ON PCT.CategoryID=P.CategoryID\r\n                        where p.IsInactive=0 ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "products", textCommand);
			return dataSet;
		}

		public byte[] GetProductPartsList()
		{
			string textCommand = "select P.ProductID[Code],P.Description[Name], p.ItemType,P.*, (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.VehicleMakeID where PD1.VehicleMakeID=pd.VehicleMakeID AND GL.GenericListType=28) \r\n                            AS Make, (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.PartsMakeTypeID where PD1.PartsMakeTypeID=pd.PartsMakeTypeID AND GL.GenericListType=25) \r\n                            AS [Parts Make],(select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.PartsTypeID where PD1.PartsTypeID=pd.PartsTypeID AND GL.GenericListType=26) \r\n                            AS [Parts Type], (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.PartsFamilyID where PD1.PartsFamilyID=pd.PartsFamilyID AND GL.GenericListType=27) \r\n                            AS [Parts Family], (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.VehicleTypeID where PD1.VehicleTypeID=pd.VehicleTypeID AND GL.GenericListType=4) \r\n                            AS [Vehicle Type],  (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.VehicleModelID where PD1.VehicleModelID=pd.VehicleModelID AND GL.GenericListType=29)  AS [Vehicle Model], PD.OEMCode, PD.PartsModel\r\n                            from Product P LEFT JOIN Product_Parts_Detail PD ON P.ProductID=PD.ProductID ORDER BY P.ProductID ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "products", textCommand);
			dataSet.RemotingFormat = SerializationFormat.Binary;
			return CommonLib.CompressDataSet(dataSet);
		}

		public DataSet GetProductsForItemTransaction()
		{
			string textCommand = "SELECT  ProductID[Code],Description[Name],Case ItemType When 1 then 'Inventory' when 2 then 'Non-Inventory' when 3 then 'Service' when 4 then 'Discount' when 5 then 'Consignment Item' when 6 \r\n                            then 'Inventory 3PL' when 7 then 'Assembly' when 8 then 'Project Fee' END AS [Item  Type], UnitID  FROM Product  where IsInactive=0";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "products", textCommand);
			return dataSet;
		}

		public DataSet GetProductSalesPurchasePriceList(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT ProductID AS [Code], Product.CategoryID AS [Category],Description,StandardCost, UnitPrice1,UnitPrice2,UnitPrice3,MinPrice, LastCost, PC.StandardPricePercent, PC.WholesalePricePercent,PC.SpecialPricePercent,PC.MinimumPricePercent FROM Product LEFT OUTER JOIN Product_Category PC ON PC.CategoryID = Product.CategoryID  where  Product.IsInactive=0";
			if (fromItem != "")
			{
				text = text + " AND ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
			}
			if (fromClass != "")
			{
				text = text + " AND ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromCategory != "")
			{
				text = text + " AND ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
			}
			FillDataSet(dataSet, "products", text);
			return dataSet;
		}

		public DataSet GetProductUnitDetails(string productID, string unitID, string LocationID)
		{
			DataSet dataSet = new DataSet();
			string text = "select * from Product_Unit where 1=1";
			if (productID != "")
			{
				text = text + " AND ProductID = '" + productID + "' ";
			}
			if (unitID != "")
			{
				text = text + " AND UnitID = '" + unitID + "' ";
			}
			FillDataSet(dataSet, "ProductUnits", text);
			text = "select * from Product_PriceList_Detail where 1=1";
			if (productID != "")
			{
				text = text + " AND ProductID = '" + productID + "' ";
			}
			if (unitID != "")
			{
				text = text + " AND UnitID = '" + unitID + "' ";
			}
			if (LocationID != "")
			{
				text = text + " AND( LocationID = '" + LocationID + "' OR ISNULL(LocationID,'')='')";
			}
			FillDataSet(dataSet, "ProductPriceListDetail", text);
			return dataSet;
		}

		internal bool UpdateProductCost(DataSet productData, SqlTransaction sqlTransaction)
		{
			try
			{
				bool flag = true;
				SqlCommand sqlCommand = new SqlCommand();
				DataTable dataTable = productData.Tables["Product_Price_Bulk_Update_Detail"];
				string text = "";
				foreach (DataRow row in dataTable.Rows)
				{
					string str = row["ProductID"].ToString();
					string str2 = row["StandardPrice"].ToString();
					string text2 = row["WholesalePrice"].ToString();
					string text3 = row["SpecialPrice"].ToString();
					string text4 = row["MinimumPrice"].ToString();
					string text5 = row["StandardCost"].ToString();
					text = text + "\n  UPDATE Product SET UnitPrice1 = " + str2;
					if (text2 != "")
					{
						text = text + ", UnitPrice2 = " + text2;
					}
					if (text3 != "")
					{
						text = text + ", UnitPrice3 = " + text3;
					}
					if (text4 != "")
					{
						text = text + ", MinPrice = " + text4;
					}
					if (text5 != "")
					{
						text = text + ", StandardCost = " + text5;
					}
					text = text + " WHERE ProductID = '" + str + "' ";
				}
				if (text != "")
				{
					sqlCommand = new SqlCommand(text, base.DBConfig.Connection);
					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.Transaction = sqlTransaction;
					flag &= (sqlCommand.ExecuteNonQuery() > 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductPartsDetail(string productID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				if (productID != "")
				{
					string textCommand = "select PD.*, (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.VehicleMakeID where PD1.VehicleMakeID=pd.VehicleMakeID AND GL.GenericListType=28) \r\n                                    AS Make, (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.PartsMakeTypeID where PD1.PartsMakeTypeID=pd.PartsMakeTypeID AND GL.GenericListType=25) \r\n                                    AS [Parts Make],(select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.PartsTypeID where PD1.PartsTypeID=pd.PartsTypeID AND GL.GenericListType=26) \r\n                                    AS [Parts Type], (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.PartsFamilyID where PD1.PartsFamilyID=pd.PartsFamilyID AND GL.GenericListType=27) \r\n                                    AS [Parts Family], (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.VehicleTypeID where PD1.VehicleTypeID=pd.VehicleTypeID AND GL.GenericListType=4) \r\n                                    AS [Vehicle Type],  (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD1.VehicleModelID where PD1.VehicleModelID=pd.VehicleModelID AND GL.GenericListType=29)  AS [Vehicle Model]\r\n                                      from Product_Parts_Detail PD WHERE PD.ProductID LIKE '" + productID + "'";
					FillDataSet(dataSet, "Parts", textCommand);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Parts"].Rows.Count == 0)
					{
						return null;
					}
					textCommand = "select (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD.VehicleMakeID where PD1.VehicleMakeID=pd.VehicleMakeID AND GL.GenericListType=28) \r\n                            AS [Make_Applied],PAD.VehicleTypeID, (select DISTINCT GL.GenericListName from Generic_List GL LEFT JOIN Product_Parts_Detail PD1 ON  GL.GenericListID=PD.VehicleTypeID where PD1.VehicleTypeID=pd.VehicleTypeID AND GL.GenericListType=4) \r\n                            AS [Vehicle Type Applied], Remarks from Product_AppliedModels_Detail PAD\r\n                            LEFT JOIN Product_Parts_Detail PD ON PD.ProductID=PAD.ProductID\r\n                           \r\n\t\t\t\t\t        WHERE PD.ProductID LIKE '" + productID + "'";
					FillDataSet(dataSet, "ProductAppliedModelsDetails", textCommand);
					textCommand = "select PSD.SubProductDescription, PSD.SubstituteProductID, PSD.UnitPrice from Product_Substitute_Detail PSD\r\n                            LEFT JOIN Product_Parts_Detail PD ON PSD.ProductID=PD.ProductID\r\n\t\t\t\t\t        WHERE PD.ProductID LIKE '" + productID + "'";
					FillDataSet(dataSet, "ProductSubstituteDetail", textCommand);
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

		public DataSet GetSalesProfitabilityReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT ASD.*,SD.LocationID,C.CustomerName,S.FullName,P.UnitID FROM Axo_Sales_Detail ASD\r\n                            LEFT JOIN Customer C ON C.CustomerID=ASD.CustomerID\r\n                            LEFT JOIN Salesperson S ON S.SalespersonID=ASD.SalespersonID\r\n                           LEFT JOIN System_Document SD ON SD.SysDocID = ASD.[Doc ID]\r\n                                    INNER JOIN Product P ON ASD.ProductID = P.ProductID \r\n                                    \r\n\t\t\t\t\t\t    WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND ASD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (customerIDs != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND ASD.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromCustomerArea != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
				}
				if (fromCustomerCountry != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
				}
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND ( ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR S.ReportTo  BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "')";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				FillDataSet(dataSet, "Sales", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesProfitabilityReportSummary(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT ASD.ProductID,ASD.Description,SUM(ASD.Quantity) AS [Quantity],SUM(ASD.Amount) AS [Amount],\r\n                                SUM(ASD.COGS) AS [Cost],SUM(ASD.Profit) AS [Profit],SD.LocationID,S.FullName,CategoryName,ClassName,P.UnitID FROM Axo_Sales_Detail ASD\r\n                                LEFT JOIN Customer C ON C.CustomerID=ASD.CustomerID\r\n                                LEFT JOIN Salesperson S ON S.SalespersonID=ASD.SalespersonID\r\n                                LEFT JOIN System_Document SD ON SD.SysDocID = ASD.[Doc ID]\r\n                                    INNER JOIN Product P ON ASD.ProductID = P.ProductID          \r\n                                LEFT OUTER JOIN Product_Category PC on PC.CategoryID=P.CategoryID\r\n                                LEFT OUTER JOIN Product_Class CL on CL.ClassID=P.ClassID                    \r\n                                WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND ASD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (customerIDs != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND ASD.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromCustomerArea != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
				}
				if (fromCustomerCountry != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
				}
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND (ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR S.ReportTo  BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "')";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				text3 += " GROUP BY ASD.ProductID,ASD.Description,SD.LocationID,S.FullName,CategoryName,ClassName,P.UnitID ";
				FillDataSet(dataSet, "Sales", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesProfitabilityItemWiseReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT ASD.ProductID,P.Description,SUM(ASD.Quantity) AS [Quantity],SUM(ASD.Amount) AS [Amount],\r\n                                SUM(ASD.COGS) AS [Cost],SUM(ASD.Profit) AS [Profit]  ,CategoryName,ClassName ,P.UnitID                           \r\n                                FROM Axo_Sales_Detail ASD\r\n                                LEFT JOIN Customer C ON C.CustomerID=ASD.CustomerID\r\n                                LEFT JOIN Salesperson S ON S.SalespersonID=ASD.SalespersonID\r\n                                LEFT JOIN System_Document SD ON SD.SysDocID = ASD.[Doc ID]\r\n                                 INNER JOIN Product P ON ASD.ProductID = P.ProductID   \r\n                                 LEFT OUTER JOIN Product_Category PC on PC.CategoryID=P.CategoryID\r\n                                LEFT OUTER JOIN Product_Class CL on CL.ClassID=P.ClassID                                 \r\n                                WHERE ASD.Date BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND ASD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (customerIDs != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND ASD.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromCustomerArea != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaId BETWEEN '" + fromCustomerArea + "' AND '" + toCustomerArea + "') ";
				}
				if (fromCustomerCountry != "")
				{
					text3 = text3 + " AND ASD.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCustomerCountry + "' AND '" + toCustomerCountry + "') ";
				}
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND (ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' OR S.ReportTo  BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "')";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				text3 += " GROUP BY ASD.ProductID,P.Description,CategoryName,ClassName,P.UnitID ";
				FillDataSet(dataSet, "Sales", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaxDetailsReportOld(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromLocation, string toLocation, string customerIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string str = "SELECT ROW_NUMBER() Over ( order by SI.VoucherID) AS [SerialNo],\r\n                                CASE WHEN C.AreaID IS NOT NULL THEN A.AreaName  \r\n                                WHEN CA.State IS NOT NULL AND NULLIF(CA.State,'') IS NOT NULL  THEN CA.State \r\n                                WHEN L.LocationName  IS NOT NULL THEN L.LocationName  END AS [Emirates], \r\n                                SI.VoucherID AS [Invoice No], SI.TransactionDate AS [Invoice Date],C.CustomerName AS [Party Name],C.TaxIDNumber AS [TRN],SI.SysDocID,\r\n                                SI.Note AS[Description], SI.CurrencyID AS [Currency], Total AS [Gross Sales] , Discount  *-1 as Discount,\r\n                                TaxAmount AS [Vat Amount] ,ISNULL(RoundOff,0) AS RoundOff, (SI.Total-SI.Discount+SI.TaxAmount+ISNULL(SI.RoundOff,0)) AS [Total Amount]\r\n                                FROM Sales_Invoice SI \r\n                                LEFT JOIN Customer C ON SI.CustomerID=C.CustomerID\r\n                                LEFT JOIN Customer_Address CA ON SI.CustomerID =CA.CustomerID\r\n                                LEFT JOIN Area A ON C.AreaID =A.AreaID \r\n                                INNER JOIN System_Document SD ON SI.SysDocID =SD.SysDocID \r\n                                LEFT JOIN Location L ON SD.LocationID =L.LocationID \r\n\r\n\t\t\t\t\t\t        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				str += " AND ISNULL(IsVoid,'False')='False'  ";
				if (customerIDs != "")
				{
					str = str + " AND SI.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					str = str + " AND SI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					str = str + " AND CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromLocation != "")
				{
					str = str + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Sales", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		private static DataTable AddAutoIncrementColumn(DataTable dt)
		{
			int num = 1;
			foreach (DataRow row in dt.Rows)
			{
				row["RelationNo"] = num;
				num++;
			}
			return dt;
		}

		public DataSet GetTaxDetailsReport(DateTime fromDate, DateTime toDate)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				bool flag = false;
				flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.AllowJobCosting, false).ToString());
				string text3 = "";
				DataSet dataSet = new DataSet();
				new DataSet();
				text3 = "SELECT '1' as SlNo,ROW_NUMBER() OVER(ORDER BY (SELECT 1 as char)) AS ChildSlNo,T.Emirates,SUM(SalesAmount) AS SalesAmount,\r\n                   SUM(T.[Sales Tax Amount]) AS[SalesTaxAmount],\r\n                   SUM(T.[Sales Round Off]) AS[Sales Round Off],\r\n                   SUM(T.[Sales Expense]) AS[SalesExpense],\r\n                   SUM(T.[Sales Total]) AS[Sales Total]\r\n                    FROM(Select\r\n                ISNULL(CASE WHEN C.AreaID IS NOT NULL THEN A.AreaName\r\n                WHEN CA.State IS NOT NULL AND NULLIF(CA.State, '') IS NOT NULL  THEN CA.State\r\n                WHEN L.LocationName  IS NOT NULL THEN B.AreaName  END, 'GCC') AS[Emirates],\r\n                ISNULL(SUM(SI.Total), 0) - ISNULL(SUM(SI.Discount), 0)[SalesAmount],\r\n              ISNULL(SUM(SI.TaxAmount), 0)[Sales Tax Amount], ISNULL(SUM(RoundOff), 0)[Sales Round Off], ISNULL(SUM(SIE.Amount), 0)[Sales Expense],\r\n                (ISNULL(SUM(SI.Total), 0) - ISNULL(SUM(SI.Discount), 0) + ISNULL(SUM(SI.TaxAmount + RoundOff), 0) + ISNULL(SUM(SIE.Amount), 0)) AS[Sales Total]\r\n                FROM Sales_Invoice SI\r\n                LEFT JOIN Customer C ON SI.CustomerID = C.CustomerID\r\n                LEFT JOIN Customer_Address CA ON SI.CustomerID = CA.CustomerID\r\n                LEFT JOIN Area A ON C.AreaID = A.AreaID\r\n\r\n\r\n                INNER JOIN System_Document SD ON SI.SysDocID = SD.SysDocID\r\n                LEFT JOIN Location L ON SD.LocationID = L.LocationID\r\n\r\n                 LEFT JOIN Area B ON B.AreaID = L.AreaID\r\n                LEFT JOIN Sales_Invoice_Expense SIE ON SI.SysDocID = SIE.InvoiceSysDocID AND SIE.InvoiceVoucherID = SI.VoucherID\r\n                WHERE  ISNULL(IsVoid, 'False') = 'False' AND SI.TaxAmount <> 0  AND SI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  GROUP BY CASE WHEN C.AreaID IS NOT NULL THEN A.AreaName\r\n                WHEN CA.State IS NOT NULL AND NULLIF(CA.State, '') IS NOT NULL  THEN CA.State\r\n                WHEN L.LocationName  IS NOT NULL THEN B.AreaName  END";
				text3 += " UNION";
				text3 = text3 + " SELECT ISNULL(CASE WHEN C.AreaID IS NOT NULL THEN A.AreaName\r\n                WHEN CA.State IS NOT NULL AND NULLIF(CA.State, '') IS NOT NULL  THEN CA.State\r\n                WHEN L.LocationName  IS NOT NULL THEN B.AreaName  END, 'GCC') AS[Emirates],\r\n                -1 * SUM(ISNULL(SI.Total, 0)) - SUM(ISNULL(SI.Discount, 0))[SalesReturn Amount], -1 * SUM(ISNULL(SI.TaxAmount, 0))[SalesReturn Tax Amount], -1 * SUM(ISNULL(RoundOff, 0))[SalesReturn Round Off], ''[SalesReturn Expense],\r\n                -1 * (SUM(ISNULL(SI.Total, 0)) - SUM(ISNULL(SI.Discount, 0)) + SUM(ISNULL(SI.TaxAmount, 0) + ISNULL(RoundOff, 0))) AS[SalesReturn Total]\r\n                 FROM Sales_Return SI\r\n                 LEFT JOIN Customer C ON SI.CustomerID = C.CustomerID\r\n                 LEFT JOIN Customer_Address CA ON SI.CustomerID = CA.CustomerID\r\n                 LEFT JOIN Area A ON C.AreaID = A.AreaID\r\n\r\n\r\n                 INNER JOIN System_Document SD ON SI.SysDocID = SD.SysDocID\r\n                 LEFT JOIN Location L ON SD.LocationID = L.LocationID\r\n\r\n                 LEFT JOIN Area B ON B.AreaID = L.AreaID\r\n                 WHERE  ISNULL(IsVoid, 'False') = 'False' AND SI.TaxAmount <> 0   AND SI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  GROUP BY  CASE WHEN C.AreaID IS NOT NULL THEN A.AreaName\r\n                WHEN CA.State IS NOT NULL AND NULLIF(CA.State, '') IS NOT NULL  THEN CA.State\r\n                WHEN L.LocationName  IS NOT NULL THEN B.AreaName  END)T group BY T.Emirates";
				text3 += " UNION";
				text3 = text3 + " SELECT '2' as SlNo,'1' as ChildSlNo,'Purchase With Tax'[Emirates],\r\n                  -(SUM(ISNULL(PINV.Total, 0)) - SUM(ISNULL(PINV.Discount, 0)) + (-1 * SUM(ISNULL(PR.Total, 0)) - SUM(ISNULL(PR.Discount, 0)))) AS[Purchase Amount],\r\n                -(SUM(ISNULL(PINV.TaxAmount, 0)) + (-1 * (SUM(ISNULL(PR.TaxAmount, 0)))))[Purchase Tax],\r\n                ''[Purchase Roundoff],\r\n                ''[Purchase Expense], \r\n                -(SUM(ISNULL(PINV.Total, 0) - ISNULL(PINV.Discount, 0) + ISNULL(PINV.TaxAmount, 0)) + (-1 * (SUM(ISNULL(PR.Total, 0) - ISNULL(PR.Discount, 0) + ISNULL(PR.TaxAmount, 0)))))[Purchase Total]\r\n                FROM Purchase_Invoice PINV\r\n                LEFT JOIN Purchase_Return_Detail PRD ON PRD.SourceSysDocID = PINV.SysDocID AND PRD.SourceVoucherID = PINV.VoucherID\r\n                LEFT JOIN Purchase_Return PR ON PRD.SysDocID = PR.SysDocID AND PRD.VoucherID = PR.VoucherID\r\n                WHERE PINV.TaxOption = 1 AND ISNULL(PINV.IsVoid, 'False')= 'False'AND PINV.TransactionDate BETWEEN  '" + text + "' AND '" + text2 + "' ";
				text3 += " UNION";
				text3 = text3 + " SELECT '3' as SlNo,\r\n                '1' as ChildSlNo,\r\n                'Purchase With Reverse Charge Tax'[Emirates],\r\n                -(SUM(ISNULL(PINV.Total, 0)) - SUM(ISNULL(PINV.Discount, 0)) + (-1 * SUM(ISNULL(PR.Total, 0)) - SUM(ISNULL(PR.Discount, 0))))[PurchaseReverse Amount],\r\n                -(SUM(ISNULL(PINV.TaxAmount, 0)) + (-1 * (SUM(ISNULL(PR.TaxAmount, 0)))))[PurchaseReverse Tax], \r\n                ''[PurchaseReverse Roundoff],\r\n                ''[PurchaseReverse Expense],\r\n                -(SUM(ISNULL(PINV.Total, 0) - ISNULL(PINV.Discount, 0) + ISNULL(PINV.TaxAmount, 0)) + (-1 * (SUM(ISNULL(PR.Total, 0) - ISNULL(PR.Discount, 0) + ISNULL(PR.TaxAmount, 0)))))[PurchaseReverse Total]\r\n                FROM Purchase_Invoice PINV\r\n                LEFT JOIN Purchase_Return_Detail PRD ON PRD.SourceSysDocID = PINV.SysDocID AND PRD.SourceVoucherID = PINV.VoucherID\r\n                LEFT JOIN Purchase_Return PR ON PRD.SysDocID = PR.SysDocID AND PRD.VoucherID = PR.VoucherID\r\n                WHERE PINV.TaxOption = 3 AND ISNULL(PINV.IsVoid, 'False')= 'False'AND PINV.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 += " UNION";
				text3 = text3 + " SELECT '4' as SlNo,\r\n                '1' as ChildSlNo,\r\n                'Purchase NonInventory Tax'[Emirates],\r\n                -(SUM(ISNULL(PINIV.Total, 0)) - SUM(ISNULL(PINIV.Discount, 0)))[NonInventoryPurchase Amount],\r\n                -(SUM(ISNULL(PINIV.TaxAmount, 0)))[NonInventoryPurchase Tax], \r\n                ''[NonInventoryPurchase Roundoff],\r\n                ''[NonInventoryPurchase Expense],\r\n                -(SUM(ISNULL(PINIV.Total, 0) - ISNULL(PINIV.Discount, 0) + ISNULL(PINIV.TaxAmount, 0)))[NonInventoryPurchase Total]\r\n                FROM Purchase_Invoice_NonInv PINIV\r\n                   WHERE PINIV.TaxOption = 1 AND ISNULL(PINIV.IsVoid, 'False')= 'False'AND PINIV.TransactionDate BETWEEN  '" + text + "' AND '" + text2 + "' ";
				if (flag)
				{
					text3 += " UNION ";
					text3 = text3 + " SELECT 1 as SlNo, 2 as ChildSlNo,\r\n                             'Project Sub Contract Purchase' as Emirates ,\r\n                             isnull(sum(PI.Total), 0) SalesAmount ,\r\n                             isnull(sum(PI.TaxAmount), 0)[SalesTaxAmount],\r\n                             ''[Sales Round Off],\r\n                             ''[SalesExpense],\r\n                            (isnull(sum(PI.Total), 0)) - (isnull(sum(PI.Discount), 0)) + (isnull(sum(PI.TaxAmount), 0)) as [Sales Total]\r\n\r\n                            FROM Project_SubContract_PI PI LEFT OUTER JOIN\r\n                            Vendor ON PI.VendorID = Vendor.VendorID\r\n                            where TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' and\r\n                             ISNULL(IsVoid, 'False') = 'False'\r\n                            union all\r\n\r\n                            SELECT 1 as SlNo, 2 as ChildSlNo,\r\n                             'Project Billing Details' as Emirates,\r\n                              isnull(sum(SI.Total), 0) SalesAmount ,\r\n                             isnull(sum(SI.TaxAmount), 0)[SalesTaxAmount],\r\n                             ''[Sales Round Off],\r\n                             ''[SalesExpense],\r\n                            (isnull(sum(SI.Total), 0)) - (isnull(sum(SI.Discount), 0)) + (isnull(sum(SI.TaxAmount), 0)) as [Sales Total]\r\n                             FROM Job_Invoice SI\r\n                            LEFT JOIN Customer C ON SI.CustomerID = C.CustomerID\r\n                            WHERE 1 = 1--ISNULL(SI.IsCash, 'False') = 'True'\r\n                            AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                            AND\r\n                            ISNULL(IsVoid, 'False') = 'False'";
				}
				text3 += " UNION ALL";
				text3 = text3 + " SELECT DISTINCT 1 as SlNo, 2 as ChildSlNo, ACC.AccountName as Emirates ,\r\n               ''  SalesAmount,\r\n                (ISNULL(SUM(ISNULL(Debit, 0)) - SUM(ISNULL(Credit, 0)), 0)) as [SalesTaxAmount],\r\n              ''[Sales Round Off],\r\n              ''[SalesExpense],\r\n              ''[Sales Total]\r\n                FROM Journal_Details JD\r\n                  INNER JOIN Journal J ON J.JournalID = jD.JournalID\r\n                  INNER JOIN System_Document SD ON SD.SysDocID = JD.SysDocID\r\n                  INNER JOIN Account ACC ON ACC.AccountID = JD.AccountID\r\n                 WHERE JD.AccountID = (SELECT TOP 1 SalesTaxAccountID FROM TAX)  AND SD.SysDocType NOT IN('51', '39', '25', '34', '37', '35', '33', '26', '27', '28') AND\r\n            J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "' GROUP BY ACC.AccountName \r\n\r\n\r\n            UNION ALL\r\n\r\n\r\n            SELECT DISTINCT 1 as SlNo, 2 as ChildSlNo, ACC.AccountName as Emirates ,\r\n               ''  SalesAmount,\r\n                -(ISNULL(SUM(ISNULL(Debit, 0)) - SUM(ISNULL(Credit, 0)), 0)) as [SalesTaxAmount],\r\n              ''[Sales Round Off],\r\n              ''[SalesExpense],\r\n              ''[Sales Total] FROM Journal_Details JD\r\n                INNER JOIN Journal J ON J.JournalID = jD.JournalID\r\n                 INNER JOIN System_Document SD ON SD.SysDocID = JD.SysDocID\r\n                 INNER JOIN Account ACC ON ACC.AccountID = JD.AccountID\r\n                 WHERE JD.AccountID = (SELECT TOP 1 PurchaseTaxAccountID FROM TAX ) AND SD.SysDocType NOT IN('51', '39', '25', '34', '37', '35', '33', '26', '27', '28') AND\r\n             J.JournalDate BETWEEN  '" + text + "' AND '" + text2 + "'  GROUP BY ACC.AccountName";
				FillDataSet(dataSet, "TaxData", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetItemVendorList(string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromOrigin, string toOrigin, string fromStyle, string toStyle, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string vendorIDs)
		{
			string text = "";
			if (fromVendor != "" || fromVendorClass != "" || fromVendorGroup != "" || vendorIDs != "")
			{
				text = "Select DISTINCT SID.ProductID, SID.Description\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tFROM Purchase_Invoice_Detail SID INNER JOIN Purchase_Invoice SI ON    \r\n\t\t\t\t\t\tSID.SysDocID=SI.SysDocID AND SID.VoucherID=SI.VoucherID \r\n\t\t\t\t\t\t";
				text += " AND ISNULL(IsVoid,'False')='False'  ";
				if (fromItem != "")
				{
					text = text + " AND SID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text = text + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text = text + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND SID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (vendorIDs != "")
				{
					text = text + " AND SI.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					text = text + " AND SI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromVendorClass != "")
				{
					text = text + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromVendorClass + "' AND '" + toVendorClass + "') ";
				}
				if (fromVendorGroup != "")
				{
					text = text + " AND VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromVendorGroup + "' AND '" + toVendorGroup + "') ";
				}
			}
			else
			{
				text = "Select ProductID, Description FROM Product where 1=1";
				if (fromItem != "")
				{
					text = text + " AND ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text = text + " AND ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text = text + " AND ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Products", text);
			return dataSet;
		}

		public DataSet GetMonthlySalesPivotReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive)
		{
			try
			{
				if (!isAsOf)
				{
					asOfDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
				}
				string text = "";
				string str = StoreConfiguration.ToSqlDateTimeString(asOfDate);
				DataSet dataSet = new DataSet();
				text = "  SELECT Month(DATE) AS #,DATENAME(month, DATE) AS Month,P.ProductID,P.Description, SUM(ASD.Quantity) AS [QTY],SUM(Amount) AS [Amt]\r\n                            FROM Axo_Sales_Detail ASD\r\n                            INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                             LEFT JOIN System_Document SD ON SD.SysDocID = ASD.[Doc ID]                             \r\n                            WHERE  Date <= '" + str + "'";
				if (fromItem != "")
				{
					text = text + " AND ASD.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND ASD.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromLocation != "")
				{
					text = text + " AND SD.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND SD.LocationID <= '" + toLocation + "'";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				text += "  GROUP BY Month(DATE),DATENAME(month, DATE),P.ProductID,P.Description ";
				text += "  ORDER BY Month(Date)";
				FillDataSet(dataSet, "MonthData", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesPurchaseAnalysisPivotReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOf, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromBrand, string toBrand)
		{
			try
			{
				string text = "";
				string text2 = CommonLib.ToSqlDateTimeString(from);
				string text3 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				text = "SELECT Month(DATE) AS #,DATENAME(month, DATE) AS Month,P.ProductID,P.Description, SUM(ASD.Quantity) AS [SalesQty] ,0.00 AS [PurchaseQty],CategoryName , ClassName                        \r\n                            FROM Axo_Sales_Detail ASD\r\n                            INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                            LEFT JOIN System_Document SD ON SD.SysDocID = ASD.[Doc ID]   \r\n                            LEFT OUTER JOIN Product_Category PC ON PC. CategoryID=P.CategoryID  \r\n                            LEFT OUTER JOIN Product_Class cl ON cl.ClassID=P.ClassID  \r\n                            WHERE  Date BETWEEN '" + text2 + "' AND '" + text3 + "'";
				if (fromItem != "")
				{
					text = text + " AND ASD.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND ASD.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND SD.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND SD.LocationID <= '" + toLocation + "'";
				}
				text += " AND ISNULL(P.IsInactive,'False')='False'  ";
				text += " GROUP BY Month(DATE),DATENAME(month, DATE),P.ProductID,P.Description,CategoryName , ClassName    ORDER BY Month(Date)";
				FillDataSet(dataSet, "Sales", text);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["Date"]
				};
				text = "SELECT Month(DATE) AS #,DATENAME(month, DATE) AS Month,P.ProductID,P.Description,0.00 AS [SalesQtynew],SUM(ASD.Quantity) AS [PurchaseQtynew]   ,CategoryName , ClassName                                          \r\n                        FROM Axo_Purchase_Detail ASD\r\n                        INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                        LEFT OUTER JOIN Product_Category PC ON PC. CategoryID=P.CategoryID  \r\n                        LEFT OUTER JOIN Product_Class cl ON cl.ClassID=P.ClassID  \r\n                        LEFT JOIN System_Document SD ON SD.SysDocID = ASD.[Doc ID]                                  \r\n                        WHERE Date BETWEEN '" + text2 + "' AND '" + text3 + "'";
				if (fromItem != "")
				{
					text = text + " AND ASD.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND ASD.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND SD.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text = text + " AND SD.LocationID <= '" + toLocation + "'";
				}
				text += " AND ISNULL(P.IsInactive,'False')='False'  ";
				text += " GROUP BY Month(DATE),DATENAME(month, DATE),P.ProductID,P.Description ,CategoryName , ClassName   ORDER BY Month(Date)";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Sales", text);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["Date"]
				};
				dataSet.Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetMonthlySalesPivotReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				StoreConfiguration.ToSqlDateTimeString(asOfDate);
				DataSet dataSet = new DataSet();
				text3 = "  SELECT Month(DATE) AS #,DATENAME(month, DATE) AS Month,P.ProductID,P.Description, SUM(ASD.Quantity) AS [QTY],SUM(Amount) AS [Amt]\r\n                            \r\n                            FROM Axo_Sales_Detail ASD\r\n                            INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                             LEFT JOIN System_Document SD ON SD.SysDocID = ASD.[Doc ID]                           \r\n                            LEFT OUTER JOIN Salesperson ON ASD.SalespersonID=Salesperson.SalespersonID\r\n                            WHERE  Date BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND ASD.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text3 = text3 + " AND ASD.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND SD.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND SD.LocationID <= '" + toLocation + "'";
				}
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				if (!isInactive)
				{
					text3 += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				text3 += "  GROUP BY Month(DATE),DATENAME(month, DATE),P.ProductID,P.Description, FullName ";
				text3 += "  ORDER BY Month(Date)";
				FillDataSet(dataSet, "MonthData", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetMonthlySalesPivotReportByCatgory(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				StoreConfiguration.ToSqlDateTimeString(asOfDate);
				DataSet dataSet = new DataSet();
				text3 = "  SELECT Month(DATE) AS #,DATENAME(month, DATE) AS Month,PC.CategoryID,PC.CategoryName, SUM(ASD.Quantity) AS [QTY],SUM(Amount) AS [Amt]\r\n                        FROM Axo_Sales_Detail ASD\r\n                        INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                        LEFT JOIN Product_Category PC ON P.CategoryID=PC.CategoryID\r\n                        LEFT OUTER JOIN Salesperson ON ASD.SalespersonID=Salesperson.SalespersonID\r\n                        LEFT JOIN System_Document SD ON SD.SysDocID = ASD.[Doc ID]\r\n                        WHERE  Date BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND ASD.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text3 = text3 + " AND ASD.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND SD.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND SD.LocationID <= '" + toLocation + "'";
				}
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				if (!isInactive)
				{
					text3 += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				text3 += " GROUP BY Month(DATE),DATENAME(month, DATE),\r\n                        PC.CategoryID,PC.CategoryName,FullName   ";
				text3 += "  ORDER BY Month(Date)";
				FillDataSet(dataSet, "MonthData", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetMonthlySalesPivotReportMore(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				StoreConfiguration.ToSqlDateTimeString(asOfDate);
				DataSet dataSet = new DataSet();
				text3 = "  SELECT Month(DATE) AS #,DATENAME(month, DATE) AS Month,PC1.CategoryID,PC1.CategoryName, SUM(ASD.Quantity) AS [QTY],SUM(Amount) AS [Amt]\r\n                        FROM Axo_Sales_Detail ASD\r\n                        INNER JOIN Product P ON P.ProductID=ASD.ProductID\r\n                        LEFT JOIN Product_Category PC ON P.CategoryID=PC.CategoryID\r\n                        LEFT JOIN Product_Category PC1 ON PC1.CategoryID=PC.ParentCategoryID\r\n                        LEFT OUTER JOIN Salesperson ON ASD.SalespersonID=Salesperson.SalespersonID\r\n                        LEFT JOIN System_Document SD ON SD.SysDocID = ASD.[Doc ID]\r\n                        WHERE  Date BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND ASD.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text3 = text3 + " AND ASD.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND ASD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND SD.LocationID >= '" + fromLocation + "'";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND SD.LocationID <= '" + toLocation + "'";
				}
				if (fromSalesperson != "")
				{
					text3 = text3 + " AND ASD.SalespersonID BETWEEN '" + fromSalesperson + "' AND '" + toSalesperson + "' ";
				}
				if (fromSalespersonGroup != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE GroupID BETWEEN '" + fromSalespersonGroup + "' AND '" + toSalespersonGroup + "') ";
				}
				if (fromSalespersonDivision != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE DivisionID BETWEEN '" + fromSalespersonDivision + "' AND '" + toSalespersonDivision + "') ";
				}
				if (fromSalespersonArea != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE AreaID BETWEEN '" + fromSalespersonArea + "' AND '" + toSalespersonArea + "') ";
				}
				if (fromSalespersonCountry != "")
				{
					text3 = text3 + " AND ASD.SalesPersonID IN (SELECT SalesPersonID FROM Salesperson WHERE CountryID BETWEEN '" + fromSalespersonCountry + "' AND '" + toSalespersonCountry + "') ";
				}
				if (!isInactive)
				{
					text3 += " AND ISNULL(P.IsInactive,'False')='False'";
				}
				text3 += " GROUP BY Month(DATE),DATENAME(month, DATE),\r\n                            PC1.CategoryID,PC1.CategoryName,FullName   ";
				text3 += "  ORDER BY Month(Date)";
				FillDataSet(dataSet, "MonthData", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public string GenerateProductID(ProductData pdata)
		{
			string result = "";
			string text = "";
			DataSet dataSet = new DataSet();
			text = "select ItemQuery from Card_Setting where ID=1";
			FillDataSet(dataSet, "test", text);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				string text2 = "";
				string text3 = dataSet.Tables[0].Rows[0]["ItemQuery"].ToString();
				string[] array = (from DataColumn x in pdata.Tables[0].Columns
					select x.ColumnName).ToArray();
				foreach (string text4 in array)
				{
					if (text2 != "")
					{
						text3 = text2;
					}
					if (text3.Contains(text4))
					{
						string str = pdata.Tables[0].Rows[0][text4].ToString();
						text2 = text3.Replace("'" + text4 + "'", "''" + str + "''");
					}
				}
				string exp = "declare @out_var varchar(max);\r\n                                    execute sp_executesql \r\n                                        N'select @out_var =" + text2 + "' , \r\n                                            N'@out_var varchar(max) OUTPUT', \r\n                                            @out_var = @out_var output\r\n                                                     select @out_var";
				object obj = ExecuteScalar(exp);
				if (obj != null)
				{
					result = obj.ToString();
				}
			}
			return result;
		}

		public DataSet GetAvailbleProductBin(bool isBinOnly, string binID)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT  TOP 1000         PL.ItemCode AS ProductID, PL.LocationID, PL.AvgCost, PL.Cost, SUM(PL2.LotQty) AS ReceivedQuantity, SUM(PL.LotQty) \r\n                        AS LotQty, SUM(PL.SoldQty)AS SoldQty, SUM(PL.LotQty - PL.SoldQty) AS Quantity,   PL2.BinID, BN.BinName,P.TaxOption, P.TaxGroupID  as TaxGroupID, P.Description, P.UnitID, P.IsTrackLot\r\n                        FROM(SELECT        ISNULL(SourceLotNumber, LotNumber) AS LotNumber, ItemCode, Cost, AvgCost, LotQty - ISNULL(ReturnedQty, 0) AS LotQty, SoldQty, LocationID\r\n                        FROM  dbo.Product_Lot AS Lot\r\n                        WHERE(LotQty - ISNULL(SoldQty, 0) - ISNULL(ReturnedQty, 0) <> 0)) AS PL INNER JOIN\r\n                        dbo.Product P ON PL.ItemCode=P.ProductID INNER JOIN\r\n                        dbo.Product_Lot AS PL2 ON PL.LotNumber = PL2.LotNumber INNER JOIN\r\n                        dbo.Location AS LOC ON LOC.LocationID = PL.LocationID INNER JOIN\r\n                        Bin as BN ON BN.BinID = PL2.BinID ";
			if (binID != "")
			{
				str = str + "WHERE BN.BinID='" + binID + "' ";
			}
			str += "GROUP BY  PL.ItemCode, PL.LocationID, PL.AvgCost, PL.Cost, LOC.LocationName, PL2.BinID,  BN.BinName,P.TaxOption, P.TaxGroupID, P.Description, P.UnitID, P.IsTrackLot ORDER BY ProductID";
			if (isBinOnly)
			{
				str = "SELECT DISTINCT  B.BinID,B.BinName FROM  Product_Lot PL \r\n                        INNER JOIN Bin B ON B.BinID=PL.BinID\r\n                        WHERE (LotQty - ISNULL(SoldQty, 0) - ISNULL(ReturnedQty, 0) <> 0)";
			}
			FillDataSet(dataSet, "AvailProductBin", str);
			str = "\tSELECT LotNumber,Reference,ProductID,BinID,RackID,SoldQty,UnitPrice,Cost,Reference2 FROM(SELECT  TOP 1000  PL.LotNumber, PL.ItemCode AS ProductID, PL.LocationID, PL.AvgCost, PL.Cost, SUM(PL2.LotQty) AS ReceivedQuantity, SUM(PL.LotQty) \r\n                         AS LotQty, SUM(PL.SoldQty)AS SoldQty, SUM(PL.LotQty - PL.SoldQty) AS Quantity, PL2.DocID, PL2.ReceiptNumber, PL2.ReceiptDate, PL2.SupplierCode,\r\n                        PL2.Reference, PL2.ProductionDate, PL2.ExpiryDate, LOC.LocationName, PL2.Reference2, PL2.BinID, PL2.RackID, BN.BinName,'0' as UnitPrice\r\n                        FROM(SELECT        ISNULL(SourceLotNumber, LotNumber) AS LotNumber, ItemCode, Cost, AvgCost, LotQty - ISNULL(ReturnedQty, 0) AS LotQty, SoldQty, LocationID\r\n                        FROM  dbo.Product_Lot AS Lot\r\n                        WHERE(LotQty - ISNULL(SoldQty, 0) - ISNULL(ReturnedQty, 0) <> 0)) AS PL INNER JOIN\r\n                        dbo.Product_Lot AS PL2 ON PL.LotNumber = PL2.LotNumber INNER JOIN\r\n                        dbo.Location AS LOC ON LOC.LocationID = PL.LocationID LEFT JOIN\r\n                        Bin as BN ON BN.BinID = PL2.BinID WHERE BN.BinID='" + binID + "' GROUP BY PL.LotNumber, PL.ItemCode, PL.LocationID, PL.AvgCost, PL.Cost, PL2.DocID, PL2.ReceiptNumber, PL2.ReceiptDate, PL2.SupplierCode, PL2.Reference, PL2.Reference2,\r\n                        PL2.ProductionDate, PL2.ExpiryDate, LOC.LocationName, PL2.BinID, PL2.RackID, BN.BinName\r\n                        ORDER BY ProductID, PL.LotNumber) AS LOTS";
			FillDataSet(dataSet, "LotTag", str);
			return dataSet;
		}

		public DataSet GetProductsByCatgeory(string categoryID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "select ProductID from Product where CategoryID ='" + categoryID + "'";
			FillDataSet(dataSet, "Products", textCommand);
			return dataSet;
		}

		public bool UpdateLotDetails(DataSet currentData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand sqlCommand = new SqlCommand();
			foreach (DataRow row in currentData.Tables[0].Rows)
			{
				sqlCommand = new SqlCommand("Update Product_Lot Set Reference=@Reference,Reference2=@Reference2,BinID=@Bin,RackID=@Rack,ProductionDate=@ProductionDate,ExpiryDate=@ExpiryDate Where LotNumber=@LotID;\r\n                                ", base.DBConfig.Connection);
				sqlCommand.Parameters.Add("@LotID", SqlDbType.VarChar).Value = row["LotNumber"];
				sqlCommand.Parameters.Add("@Reference", SqlDbType.VarChar).Value = row["Reference"];
				sqlCommand.Parameters.Add("@Reference2", SqlDbType.VarChar).Value = row["Reference2"];
				if (row["BinID"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@Bin", SqlDbType.VarChar).Value = row["BinID"];
				}
				else
				{
					sqlCommand.Parameters.Add("@Bin", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["RackID"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@Rack", SqlDbType.VarChar).Value = row["RackID"];
				}
				else
				{
					sqlCommand.Parameters.Add("@Rack", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["ExpiryDate"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = row["ExpiryDate"];
				}
				else
				{
					sqlCommand.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["ProductionDate"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@ProductionDate", SqlDbType.VarChar).Value = row["ProductionDate"];
				}
				else
				{
					sqlCommand.Parameters.Add("@ProductionDate", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["SysDocID"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@SysDocID", SqlDbType.VarChar).Value = row["SysDocID"];
				}
				else
				{
					sqlCommand.Parameters.Add("@SysDocID", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["VoucherID"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@VoucherID", SqlDbType.VarChar).Value = row["VoucherID"];
				}
				else
				{
					sqlCommand.Parameters.Add("@VoucherID", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["RowIndex"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@RowIndex", SqlDbType.Int).Value = row["RowIndex"];
				}
				else
				{
					sqlCommand.Parameters.Add("@RowIndex", SqlDbType.Int).Value = DBNull.Value;
				}
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.Transaction = sqlTransaction;
				flag &= (sqlCommand.ExecuteNonQuery() > 0);
			}
			return flag;
		}

		public bool UpdateLotReceivingDetails(DataSet currentData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand sqlCommand = new SqlCommand();
			foreach (DataRow row in currentData.Tables[0].Rows)
			{
				sqlCommand = new SqlCommand("Update  Product_Lot_Receiving_Detail  Set LotNumber=@Reference, ProductionDate=@ProductionDate,ExpiryDate=@ExpiryDate, Reference2=@Reference2,BinID=@Bin,\r\n                            RackID=@Rack Where SysDocID=@SysDocID AND VoucherID=@VoucherID \r\n                            AND LotNumber=(select Reference from Product_Lot where LotNumber=@LotID) ", base.DBConfig.Connection);
				sqlCommand.Parameters.Add("@LotID", SqlDbType.VarChar).Value = row["LotNumber"];
				sqlCommand.Parameters.Add("@Reference", SqlDbType.VarChar).Value = row["Reference"];
				sqlCommand.Parameters.Add("@Reference2", SqlDbType.VarChar).Value = row["Reference2"];
				if (row["BinID"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@Bin", SqlDbType.VarChar).Value = row["BinID"];
				}
				else
				{
					sqlCommand.Parameters.Add("@Bin", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["RackID"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@Rack", SqlDbType.VarChar).Value = row["RackID"];
				}
				else
				{
					sqlCommand.Parameters.Add("@Rack", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["ExpiryDate"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = row["ExpiryDate"];
				}
				else
				{
					sqlCommand.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["ProductionDate"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@ProductionDate", SqlDbType.VarChar).Value = row["ProductionDate"];
				}
				else
				{
					sqlCommand.Parameters.Add("@ProductionDate", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["SysDocID"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@SysDocID", SqlDbType.VarChar).Value = row["SysDocID"];
				}
				else
				{
					sqlCommand.Parameters.Add("@SysDocID", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["VoucherID"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@VoucherID", SqlDbType.VarChar).Value = row["VoucherID"];
				}
				else
				{
					sqlCommand.Parameters.Add("@VoucherID", SqlDbType.VarChar).Value = DBNull.Value;
				}
				if (row["RowIndex"].ToString() != "")
				{
					sqlCommand.Parameters.Add("@RowIndex", SqlDbType.Int).Value = row["RowIndex"];
				}
				else
				{
					sqlCommand.Parameters.Add("@RowIndex", SqlDbType.Int).Value = DBNull.Value;
				}
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.Transaction = sqlTransaction;
				flag &= (sqlCommand.ExecuteNonQuery() > 0);
			}
			return flag;
		}

		public DataSet GetProductLotDetails(string product, string location, string lotIDFrom, string lotIDTo)
		{
			DataSet dataSet = new DataSet();
			string text = "select LotNumber,Reference,Reference2,SourceLotNumber,ProductionDate,ExpiryDate,LotQty,Cost,BinID,RackID,DocID,ReceiptNumber,RowIndex,\r\n                 CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE (SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS Consign# , LotQty-SoldQty-ISNULL(ReturnedQty,0) as AvailableQty  from Product_Lot PL where  LotQty-SoldQty-ISNULL(ReturnedQty,0)>0 ";
			if (product != "")
			{
				text = text + " AND ItemCode = '" + product + "'";
			}
			if (lotIDFrom != "" && lotIDTo != "")
			{
				text = text + " AND LotNumber Between '" + lotIDFrom + "' AND '" + lotIDTo + "'";
			}
			if (location != "")
			{
				text = text + " AND LocationID='" + location + "'";
			}
			FillDataSet(dataSet, "Product_Lot", text);
			return dataSet;
		}

		public DataSet GetProductLotDetails(string product)
		{
			DataSet dataSet = new DataSet();
			string text = "select LotNumber,Reference,Reference2,SourceLotNumber,ProductionDate,ExpiryDate,LotQty,Cost,BinID,RackID,DocID,ReceiptNumber,RowIndex,\r\n                 CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE (SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS Consign# , LotQty-SoldQty-ISNULL(ReturnedQty,0) as AvailableQty  from Product_Lot PL where  LotQty-SoldQty-ISNULL(ReturnedQty,0)>0 ";
			if (product != "")
			{
				text = text + " AND ItemCode = '" + product + "'";
			}
			FillDataSet(dataSet, "Product_Lot", text);
			return dataSet;
		}

		public decimal GetProductCostwithMultiUnit(string productID, string unitID, string locationID)
		{
			DataSet productUnitDetails = GetProductUnitDetails(productID, unitID, locationID);
			DataSet dataSet = new DataSet();
			string textCommand = "select StandardCost,AverageCost from Product where ProductID='" + productID + "'";
			FillDataSet(dataSet, "ProductCost", textCommand);
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			decimal result = default(decimal);
			if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
			{
				DataRow dataRow2 = productUnitDetails.Tables[0].Rows[0];
				string a = dataRow2["FactorType"].ToString();
				decimal num = decimal.Parse(dataRow2["Factor"].ToString());
				if (!string.IsNullOrEmpty(dataRow["StandardCost"].ToString()))
				{
					decimal.TryParse(((object)decimal.Parse(dataRow["StandardCost"].ToString())).ToString(), out result);
				}
				if (result == 0m)
				{
					result = decimal.Parse(dataRow["AverageCost"].ToString());
				}
				if (a == "M")
				{
					result /= num;
				}
				else
				{
					result *= num;
				}
				return result;
			}
			if (!string.IsNullOrEmpty(dataRow["StandardCost"].ToString()))
			{
				decimal.TryParse(((object)decimal.Parse(dataRow["StandardCost"].ToString())).ToString(), out result);
			}
			if (result == 0m)
			{
				result = decimal.Parse(dataRow["AverageCost"].ToString());
			}
			return result;
		}

		public bool IsProductExist(string productID)
		{
			string cardSecurityQuery = cardSecurityQuery = new Security(base.DBConfig).GetCardSecurityQuery();
			string text = "SELECT Count(*) \r\n\t\t\t\t\t\t   FROM Product  WHERE ProductID='" + productID + "' ";
			if (cardSecurityQuery != "")
			{
				text = text + " AND " + cardSecurityQuery;
			}
			object obj = ExecuteScalar(text);
			if (obj != null)
			{
				if (decimal.Parse(obj.ToString()) > 0m)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public bool IsExistProductTransaction(string productID)
		{
			string exp = "Select Count(*) from Inventory_Transactions WHERE ProductID='" + productID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				if (decimal.Parse(obj.ToString()) > 0m)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public DataSet GetProductUnits(string productID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select * from Product_Unit WHERE ProductID='" + productID + "'";
			FillDataSet(dataSet, "ProductUnit", textCommand);
			return dataSet;
		}
	}
}
