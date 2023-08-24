using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Micromind.Facade
{
	public sealed class ProductSystem : MarshalByRefObject, IProductSystem, IDisposable
	{
		private Config config;

		public string ItemPicDirectory
		{
			get
			{
				string result = "Item Images";
				using (Settings settings = new Settings(config))
				{
					try
					{
						object data = settings.GetData("E4C9AB0B1E954fe8A4839E8540290204I", "E4C9AB0B1E954fe8A4839E8540290205N", "E4C9AB0B1E954fe8A4839E8540290203K", "ItemPicDirectory");
						if (data == null)
						{
							return result;
						}
						if (data.ToString().Length <= 0)
						{
							return result;
						}
						result = data.ToString();
						return result;
					}
					catch
					{
						return result;
					}
				}
			}
			set
			{
				using (Settings settings = new Settings(config))
				{
					try
					{
						settings.SaveSetting("E4C9AB0B1E954fe8A4839E8540290204I", "E4C9AB0B1E954fe8A4839E8540290205N", "E4C9AB0B1E954fe8A4839E8540290203K", "ItemPicDirectory", value);
					}
					catch
					{
						throw;
					}
				}
			}
		}

		private string ItemImageDirectory
		{
			get
			{
				string result = "Item Images";
				using (Settings settings = new Settings(config))
				{
					try
					{
						object data = settings.GetData("E4C9AB0B1E954fe8A4839E8540290204I", "E4C9AB0B1E954fe8A4839E8540290205N", "E4C9AB0B1E954fe8A4839E8540290203K", "ItemPicDirectory");
						if (data == null)
						{
							return result;
						}
						if (data.ToString().Length <= 0)
						{
							return result;
						}
						result = data.ToString();
						return result;
					}
					catch
					{
						return result;
					}
				}
			}
		}

		public ProductSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool UpdateProduct(ProductData productData)
		{
			using (Products products = new Products(config))
			{
				return products.InsertUpdateProduct(productData, isUpdate: true);
			}
		}

		public bool CreateProduct(ProductData productData)
		{
			using (Products products = new Products(config))
			{
				return products.InsertUpdateProduct(productData, isUpdate: false);
			}
		}

		public ProductData GetProductByID(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductByID(productID);
			}
		}

		public bool DeleteProduct(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.DeleteProduct(productID);
			}
		}

		public byte[] GetItemThumbnailImage(int id, int width, int height)
		{
			MemoryStream memoryStream = null;
			try
			{
				string text = "";
				if (text.Length > 0)
				{
					try
					{
						Image thumbnailImage = Image.FromFile(text).GetThumbnailImage(width, height, null, IntPtr.Zero);
						memoryStream = new MemoryStream();
						thumbnailImage.Save(memoryStream, ImageFormat.Bmp);
					}
					catch
					{
						return null;
					}
				}
			}
			catch
			{
				return null;
			}
			return memoryStream.ToArray();
		}

		private bool IsImageFileExist(string imageName)
		{
			string text = "";
			try
			{
				text = Process.GetCurrentProcess().MainModule.FileName;
				int length = text.LastIndexOf("\\");
				text = text.Substring(0, length) + "\\" + ItemImageDirectory;
				text = text + "\\" + imageName;
			}
			catch
			{
				throw;
			}
			return File.Exists(text);
		}

		private string GetUniqueImageName(string imageName)
		{
			int num = 1;
			string text = imageName;
			char newChar = ' ';
			char[] invalidPathChars = Path.GetInvalidPathChars();
			foreach (char oldChar in invalidPathChars)
			{
				text = text.Replace(oldChar, newChar);
			}
			checked
			{
				while (IsImageFileExist(text))
				{
					num++;
					text = ((imageName.IndexOf(".") < 0) ? (imageName + num.ToString()) : (imageName.Substring(0, imageName.IndexOf(".")) + num.ToString() + "." + imageName.Substring(imageName.IndexOf(".") + 1, imageName.Length - imageName.IndexOf(".") - 1)));
				}
				text = Path.GetFileNameWithoutExtension(text);
				return text + ".jpg";
			}
		}

		public byte[] GetProductComboList()
		{
			using (Products products = new Products(config))
			{
				return products.GetProductComboList();
			}
		}

		public DataSet GetProductUnitComboList()
		{
			using (Products products = new Products(config))
			{
				return products.GetProductUnitComboList();
			}
		}

		public float GetProductQuantity(string productID, string locationID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductQuantity(productID, locationID);
			}
		}

		public DataSet GetProductQuantityAndCost(string productID, string locationID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductQuantityAndCost(productID, locationID);
			}
		}

		public DataSet GetProductAvailability(string productID, string unitID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductAvailability(productID, unitID);
			}
		}

		public DataSet GetProductList(bool inActive, bool showZeroBalance, string locationID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductList(inActive, showZeroBalance, locationID);
			}
		}

		public bool AddProductPhoto(string productID, byte[] pictureByte)
		{
			using (Products products = new Products(config))
			{
				return products.AddProductPhoto(productID, pictureByte);
			}
		}

		public bool RemoveProductPhoto(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.RemoveProductPhoto(productID);
			}
		}

		public bool GetProductTransactionsExists(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductTransactionsExists(productID);
			}
		}

		public byte[] GetProductThumbnailImage(string productID)
		{
			return GetProductThumbnailImage(productID, 128, 128);
		}

		public byte[] GetProductThumbnailImage(string productID, int width, int height)
		{
			using (Products products = new Products(config))
			{
				byte[] productThumbnailImage = products.GetProductThumbnailImage(productID);
				if (productThumbnailImage == null)
				{
					return null;
				}
				return CreateThumbnail(productThumbnailImage, width, height);
			}
		}

		private byte[] CreateThumbnail(byte[] image, int width, int height)
		{
			if (image == null)
			{
				return null;
			}
			ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(image, 0, image.Length);
			Bitmap bitmap = new Bitmap(memoryStream);
			int num = width;
			int num2 = height;
			int width2 = bitmap.Width;
			int height2 = bitmap.Height;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			num4 = (float)num / (float)width2;
			num5 = (float)num2 / (float)height2;
			num3 = ((!(num5 < num4)) ? num4 : num5);
			checked
			{
				num = (int)((float)width2 * num3);
				num2 = (int)((float)height2 * num3);
				Image thumbnailImage = bitmap.GetThumbnailImage(num, num2, ThumbnailImageAbort, IntPtr.Zero);
				Encoder quality = Encoder.Quality;
				EncoderParameters encoderParameters = new EncoderParameters(1);
				EncoderParameter encoderParameter = new EncoderParameter(quality, 50L);
				encoderParameters.Param[0] = encoderParameter;
				memoryStream = new MemoryStream();
				thumbnailImage.Save(memoryStream, encoder, encoderParameters);
				return memoryStream.ToArray();
			}
		}

		private static bool ThumbnailImageAbort()
		{
			return true;
		}

		private ImageCodecInfo GetEncoder(ImageFormat format)
		{
			ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
			foreach (ImageCodecInfo imageCodecInfo in imageDecoders)
			{
				if (imageCodecInfo.FormatID == format.Guid)
				{
					return imageCodecInfo;
				}
			}
			return null;
		}

		public DataSet GetPriceLevelComboList()
		{
			using (Products products = new Products(config))
			{
				return products.GetPriceLevelComboList();
			}
		}

		public DataSet GetSalesByItemSummaryReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesByItemSummaryReport(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetSalesByItemDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesByItemDetailReport(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPriceListNames()
		{
			using (Products products = new Products(config))
			{
				return products.GetPriceListNames();
			}
		}

		public DataSet GetProductPriceList(string productID, string customerID, string LocationID, string UnitID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductPriceList(productID, customerID, LocationID, UnitID);
			}
		}

		public DataSet GetAllProductPriceList()
		{
			using (Products products = new Products(config))
			{
				return products.GetAllProductPriceList();
			}
		}

		public decimal GetProductSalesPrice(string productID, string customerID, string locationID, string UnitID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductSalesPrice(productID, customerID, locationID, UnitID);
			}
		}

		public DataSet GetProductSalesPriceDesc(string productID, string customerID, string unitID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductSalesPriceDesc(productID, customerID, unitID);
			}
		}

		public decimal GetProductPurchasePrice(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductPurchasePrice(productID);
			}
		}

		public DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, bool showitemwithTansactions, bool showinactiveitems)
		{
			using (Products products = new Products(config))
			{
				return products.GetInventoryTransactionReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, showitemwithTansactions, showinactiveitems);
			}
		}

		public DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromJob, string toJob, string fromCostCategory, string toCostCategory, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetInventoryTransactionReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromJob, toJob, fromCostCategory, toCostCategory, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromJob, string toJob, bool showitemwithTansactions, bool showinactiveitems, bool excludeInventoryTransfer, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetInventoryTransactionReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromJob, toJob, showitemwithTansactions, showinactiveitems, excludeInventoryTransfer, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetInventoryTransactionLotwiseReport(DateTime from, DateTime to, DateTime fromDate, DateTime toDate, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, bool? production, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetInventoryTransactionLotwiseReport(from, to, fromDate, toDate, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, production, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetInventoryTransactionLotwiseReport(DateTime from, DateTime to, DateTime fromDate, DateTime toDate, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromJob, string toJob, bool? production)
		{
			using (Products products = new Products(config))
			{
				return products.GetInventoryTransactionLotwiseReport(from, to, fromDate, toDate, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromJob, toJob, production);
			}
		}

		public bool IsHoldSaleonProduct(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.IsHoldSaleonProduct(productID);
			}
		}

		public bool IsSufficientQuantityOnhand(string productID, string unitID, string locationID, string detailsTableName, string sysDocID, string voucherID, decimal quantity)
		{
			using (Products products = new Products(config))
			{
				return products.IsSufficientQuantityOnhand(productID, unitID, locationID, detailsTableName, sysDocID, voucherID, quantity);
			}
		}

		public DataSet GetSalesByItemCategorySummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesByItemCategorySummaryReport(from, to, fromCategory, toCategory);
			}
		}

		public DataSet GetPurchaseByItemCategorySummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory)
		{
			using (Products products = new Products(config))
			{
				return products.GetPurchaseByItemCategorySummaryReport(from, to, fromCategory, toCategory);
			}
		}

		public DataSet GetProductListReport(string fromProduct, string toProduct, string fromClass, string toClass, string fromCategory, string toCategory, bool showZero, bool showInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductListReport(fromProduct, toProduct, fromClass, toClass, fromCategory, toCategory, showZero, showInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPurchaseByItemSummaryReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetPurchaseByItemSummaryReport(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPurchaseByItemDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetPurchaseByItemDetailReport(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetProductPriceListReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductPriceListReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, showInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetProductSinglePriceListReport(string fromProduct, string toProduct, string fromClass, string toClass, string fromCategory, string toCategory, string unitPriceName, bool showInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductSinglePriceListReport(fromProduct, toProduct, fromClass, toClass, fromCategory, toCategory, unitPriceName, showInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetProductStockListItemWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductStockListItemWiseReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, isAsOfDate, asOfDate, showZero, isInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetInventoryAgingSummaryReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromBrand, string toBrand, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetInventoryAgingSummaryReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromLocation, toLocation, fromBrand, toBrand, asOfDate, showZero, isInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetMatrixProductStockListReport(string fromItem, string toItem, string fromCategory, string toCategory, bool showZero, bool showImage, bool isInactive)
		{
			using (Products products = new Products(config))
			{
				return products.GetMatrixProductStockListReport(fromItem, toItem, fromCategory, toCategory, showZero, showImage, isInactive);
			}
		}

		public DataSet GetProductStockListLocationWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductStockListLocationWiseReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromLocation, toLocation, isAsOfDate, asOfDate, showZero, isInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetProductCatalogReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool isInactive, bool showZeroQuantity, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				DataSet productCatalogReport = products.GetProductCatalogReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, isInactive, showZeroQuantity, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
				foreach (DataRow row in productCatalogReport.Tables["Product"].Rows)
				{
					if (row["Photo"] != DBNull.Value)
					{
						byte[] image = (byte[])row["Photo"];
						image = (byte[])(row["Photo"] = CreateThumbnail(image, 128, 128));
					}
				}
				return productCatalogReport;
			}
		}

		public DataSet GetProductListPOS()
		{
			using (Products products = new Products(config))
			{
				return products.GetProductListPOS();
			}
		}

		public DataSet GetProductListPOS(string locationID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductListPOS(locationID);
			}
		}

		public DataSet GetTopProducts(DateTime from, DateTime to, int count)
		{
			using (Products products = new Products(config))
			{
				return products.GetTopProducts(from, to, count);
			}
		}

		public DataSet POSGetProductData(string code)
		{
			using (Products products = new Products(config))
			{
				return products.POSGetProductData(code);
			}
		}

		public DataSet GetProductsToReorder()
		{
			using (Products products = new Products(config))
			{
				return products.GetProductsToReorder();
			}
		}

		public DataSet GetInventoryLedgerList(string productID, DateTime from, DateTime to, bool excludeInventoryTransfer)
		{
			using (Products products = new Products(config))
			{
				return products.GetInventoryLedgerList(productID, from, to, excludeInventoryTransfer);
			}
		}

		public DataSet GetProductQuantityAndCostAsOfDate(string productID, string locationID, DateTime date)
		{
			using (Products products = new Products(config))
			{
				string[] productIDs = new string[1]
				{
					productID
				};
				return products.GetProductQuantityAndCostAsOfDate(productIDs, locationID, date);
			}
		}

		public DataSet GetProductQuantityAndCostAsOfDate(string[] productIDs, string locationID, DateTime date)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductQuantityAndCostAsOfDate(productIDs, locationID, date);
			}
		}

		public decimal GetProductLastCost(string productID, DateTime date)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductLastCost(productID, date);
			}
		}

		public bool UpdateInvoicesWhichRequireCOGSUpdate(string batchRef, DateTime from, DateTime to)
		{
			using (new InventoryTransaction(config))
			{
				return false;
			}
		}

		public DataSet GetProductAvailableLotsAndBins(string productID, string locationID, string sysDocID, string voucherID, string vendorID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductAvailableLotsAndBins(productID, locationID, sysDocID, voucherID, vendorID);
			}
		}

		public DataSet GetSOProductAvailableLotsAndBins(string productID, string locationID, string sysDocID, string voucherID, string vendorID)
		{
			using (Products products = new Products(config))
			{
				return products.GetSOProductAvailableLotsAndBins(productID, locationID, sysDocID, voucherID, vendorID);
			}
		}

		public bool UpdatePreviousTransactionsCOGS(DateTime fromDate, string fromItem, string endItem)
		{
			using (Products products = new Products(config))
			{
				return products.UpdatePreviousTransactionsCOGS(fromDate, fromItem, endItem);
			}
		}

		public DataSet GetProductLot(string lotID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductLot(lotID);
			}
		}

		public DataSet GetProductReturnableLotsAndBins(string productID, string locationID, string sysDocID, string voucherID, string customerID, string returnSourceSysDocID, string returnSourceVoucherID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductReturnableLotsAndBins(productID, locationID, sysDocID, voucherID, customerID, returnSourceSysDocID, returnSourceVoucherID);
			}
		}

		public DataSet GetProductLotWiseAvailability(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductLotWiseAvailability(productID);
			}
		}

		public bool DocumentHasUsedLots(string sysDocID, string voucherID)
		{
			using (Products products = new Products(config))
			{
				return products.HasInUseLots(sysDocID, voucherID);
			}
		}

		public DataSet GetSalesByItemCustomerSalespersonReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesByItemCustomerSalespersonReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetSalesByItemCustomerSalespersonReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs, string strGroupBy)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesByItemCustomerSalespersonReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromSalesperson, toSalesperson, fromLocation, toLocation, customerIDs, strGroupBy);
			}
		}

		public DataSet GetSalesByItemCustomerSalespersonReport(string group1, string group2, string fields, string joinQuery, DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesByItemCustomerSalespersonReport(group1, group2, fields, joinQuery, fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs);
			}
		}

		public DataSet GetSalesProfitabilityReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesProfitabilityReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetSalesProfitabilityReportSummary(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesProfitabilityReportSummary(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetSalesProfitabilityItemWiseReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesProfitabilityItemWiseReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPendingDNsReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetPendingDNsReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, customerIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPickListReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetPickListReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetSalesByItemClassCategorySummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory, string fromClass, string toClass)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesByItemClassCategorySummaryReport(from, to, fromCategory, toCategory, fromClass, toClass);
			}
		}

		public DataSet GetSalesByProductBrandSummaryReport(DateTime from, DateTime to, string fromBrand, string toBrand)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesByProductBrandSummaryReport(from, to, fromBrand, toBrand);
			}
		}

		public DataSet GetProductStockListCategoryWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductStockListCategoryWiseReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromLocation, toLocation, isAsOfDate, asOfDate, showZero, isInactive);
			}
		}

		public DataSet GetProductStockListClassWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductStockListClassWiseReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromLocation, toLocation, isAsOfDate, asOfDate, showZero, isInactive);
			}
		}

		public bool AllocateItemsToLot(string[] productIDs)
		{
			using (Products products = new Products(config))
			{
				return products.AllocateItemsToLot(productIDs);
			}
		}

		public decimal GetJobBOMProductPurchasePrice(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.GetJobBOMProductPurchasePrice(productID);
			}
		}

		public decimal GetJobBOMLabourCost(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.GetJobBOMLabourCost(productID);
			}
		}

		public DataSet GetSalesManDueReport(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesManDueReport(fromDate, toDate, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, showZeroBalance: false, isFC: false, customerIDs);
			}
		}

		public decimal GetLastSaleTransationByCustomerID(string productID, string customerID)
		{
			using (Products products = new Products(config))
			{
				return products.GetLastSaleTransationByCustomerID(productID, customerID);
			}
		}

		public DataSet GetSaleProductByID(string productID, string customerID)
		{
			using (Products products = new Products(config))
			{
				return products.GetSaleProductByID(productID, customerID);
			}
		}

		public bool GetItemExistsinCategory(string productID, string customerID)
		{
			using (Products products = new Products(config))
			{
				return products.GetItemExistsinCategory(productID, customerID);
			}
		}

		public DataSet GetsufficientQuantityforPackage(string productID, string customerID)
		{
			using (Products products = new Products(config))
			{
				return products.GetsufficientQuantityforPackage(productID, customerID);
			}
		}

		public byte[] GetItemFeatures(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.GetItemFeatures(productID);
			}
		}

		public DataSet GetPackageID(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.GetPackageID(productID);
			}
		}

		public DataSet GetW3PLInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems, string fromCustomer, string toCustomer, string fromCusClass, string toCusClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			using (Products products = new Products(config))
			{
				return products.GetW3PLInventoryTransactionReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, showitemwithTansactions, showinactiveitems, fromCustomer, toCustomer, fromCusClass, toCusClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs);
			}
		}

		public DataSet GetW3PLProductStockListLocationWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive, string fromCustomer, string toCustomer, string fromCusClass, string toCusClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			using (Products products = new Products(config))
			{
				return products.GetW3PLProductStockListLocationWiseReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromLocation, toLocation, isAsOfDate, asOfDate, showZero, isInactive, fromCustomer, toCustomer, fromCusClass, toCusClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs);
			}
		}

		public DataSet GetW3PLInventoryAgingSummaryReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, DateTime asOfDate, bool showZero, bool isInactive, string fromCustomer, string toCustomer, string fromCusClass, string toCusClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			using (Products products = new Products(config))
			{
				return products.GetW3PLInventoryAgingSummaryReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromLocation, toLocation, asOfDate, showZero, isInactive, fromCustomer, toCustomer, fromCusClass, toCusClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs);
			}
		}

		public DataSet GetPurchaseByItemVendorBuyerReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs)
		{
			using (Products products = new Products(config))
			{
				return products.GetPurchaseByItemVendorBuyerReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromVendor, toVendor, fromVendorClass, toVendorClass, fromVendorGroup, toVendorGroup, fromBuyer, toBuyer, fromLocation, toLocation, vendorIDs);
			}
		}

		public DataSet GetPurchaseByInventoryItemVendorBuyerReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Products products = new Products(config))
			{
				return products.GetPurchaseByInventoryItemVendorBuyerReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromVendor, toVendor, fromVendorClass, toVendorClass, fromVendorGroup, toVendorGroup, fromBuyer, toBuyer, fromLocation, toLocation, vendorIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPurchaseSubContractByItemVendorBuyerReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string toJob, string fromJob)
		{
			using (Products products = new Products(config))
			{
				return products.GetPurchaseSubContractByItemVendorBuyerReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromVendor, toVendor, fromVendorClass, toVendorClass, fromVendorGroup, toVendorGroup, fromBuyer, toBuyer, fromLocation, toLocation, fromJob, toJob);
			}
		}

		public DataSet GetProjectSubContractOrderReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string toJob, string fromJob)
		{
			using (Products products = new Products(config))
			{
				return products.GetProjectSubContractOrderReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromVendor, toVendor, fromVendorClass, toVendorClass, fromVendorGroup, toVendorGroup, fromBuyer, toBuyer, fromJob, toJob);
			}
		}

		public bool SetFlag(string productID, byte flagID)
		{
			using (Products products = new Products(config))
			{
				return products.SetFlag(productID, flagID);
			}
		}

		public DataSet GetProductComboRowByID(string IDField)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductComboRowByID(IDField);
			}
		}

		public DataSet GetProducts()
		{
			using (Products products = new Products(config))
			{
				return products.GetProducts();
			}
		}

		public DataSet GetProductsForCombo()
		{
			using (Products products = new Products(config))
			{
				return products.GetProductsForCombo();
			}
		}

		public DataSet GetProductsForItemTransaction()
		{
			using (Products products = new Products(config))
			{
				return products.GetProductsForItemTransaction();
			}
		}

		public DataSet GetProductSalesPurchasePriceList(string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductSalesPurchasePriceList(fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory);
			}
		}

		public DataSet GetProductPartsDetail(string ProductID)
		{
			return new Products(config).GetProductPartsDetail(ProductID);
		}

		public byte[] GetProductPartsList()
		{
			using (Products products = new Products(config))
			{
				return products.GetProductPartsList();
			}
		}

		public DataSet GetProductUnitDetails(string productID, string unitID, string LocationID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductUnitDetails(productID, unitID, LocationID);
			}
		}

		public DataSet GetTaxDetailsReport(DateTime fromDate, DateTime toDate)
		{
			using (Products products = new Products(config))
			{
				return products.GetTaxDetailsReport(fromDate, toDate);
			}
		}

		public DataSet GetItemVendorList(string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromItemBrand, string toItemBrand, string fromManufacturer, string toManufacturer, string fromOrigin, string toOrigin, string fromStyle, string toStyle, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string vendorIDs)
		{
			using (Products products = new Products(config))
			{
				return products.GetItemVendorList(fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromItemBrand, toItemBrand, fromManufacturer, toManufacturer, fromOrigin, toOrigin, fromStyle, toStyle, fromVendor, toVendor, fromVendorClass, toVendorClass, fromVendorGroup, toVendorGroup, vendorIDs);
			}
		}

		public DataSet GetMonthlySalesPivotReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive)
		{
			using (Products products = new Products(config))
			{
				return products.GetMonthlySalesPivotReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromLocation, toLocation, isAsOfDate, asOfDate, showZero, isInactive);
			}
		}

		public DataSet GetMonthlySalesPivotReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Products products = new Products(config))
			{
				return products.GetMonthlySalesPivotReport(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromLocation, toLocation, isAsOfDate, asOfDate, showZero, isInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetMonthlySalesPivotReportMore(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Products products = new Products(config))
			{
				return products.GetMonthlySalesPivotReportMore(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromLocation, toLocation, isAsOfDate, asOfDate, showZero, isInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetMonthlySalesPivotReportByCatgory(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Products products = new Products(config))
			{
				return products.GetMonthlySalesPivotReportByCatgory(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromLocation, toLocation, isAsOfDate, asOfDate, showZero, isInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetSalesPurchaseAnalysisPivotReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromBrand, string toBrand)
		{
			using (Products products = new Products(config))
			{
				return products.GetSalesPurchaseAnalysisPivotReport(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromLocation, toLocation, isAsOfDate, showZero, isInactive, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin, fromBrand, toBrand);
			}
		}

		public string GenerateProductID(ProductData pdata)
		{
			using (Products products = new Products(config))
			{
				return products.GenerateProductID(pdata);
			}
		}

		public DataSet GetAvailbleProductBin(bool IsBinOnly, string binID)
		{
			using (Products products = new Products(config))
			{
				return products.GetAvailbleProductBin(IsBinOnly, binID);
			}
		}

		public DataSet GetProductsByCatgeory(string categoryID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductsByCatgeory(categoryID);
			}
		}

		public DataSet GetProductLotDetails(string product, string location, string lotIDFrom, string lotIDTo)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductLotDetails(product, location, lotIDFrom, lotIDTo);
			}
		}

		public bool UpdateLotDetails(DataSet currentData, SqlTransaction sqlTransaction)
		{
			using (Products products = new Products(config))
			{
				return products.UpdateLotDetails(currentData, sqlTransaction);
			}
		}

		public bool UpdateLotReceivingDetails(DataSet currentData, SqlTransaction sqlTransaction)
		{
			using (Products products = new Products(config))
			{
				return products.UpdateLotReceivingDetails(currentData, sqlTransaction);
			}
		}

		public decimal GetProductCostwithMultiUnit(string productID, string unitID, string locationID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductCostwithMultiUnit(productID, unitID, locationID);
			}
		}

		public bool IsProductExist(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.IsProductExist(productID);
			}
		}

		public bool IsExistProductTransaction(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.IsExistProductTransaction(productID);
			}
		}

		public DataSet GetProductUnits(string productID)
		{
			using (Products products = new Products(config))
			{
				return products.GetProductUnits(productID);
			}
		}
	}
}
