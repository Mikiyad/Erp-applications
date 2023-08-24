using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Micromind.Facade
{
	public sealed class ProductParentSystem : MarshalByRefObject, IProductParentSystem, IDisposable
	{
		private Config config;

		public ProductParentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductParent(ProductParentData data)
		{
			return new ProductParent(config).InsertUpdateProductParent(data, isUpdate: false);
		}

		public bool UpdateProductParent(ProductParentData data)
		{
			return new ProductParent(config).InsertUpdateProductParent(data, isUpdate: true);
		}

		public ProductParentData GetProductParent()
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.GetProductParent();
			}
		}

		public bool DeleteProductParent(string productParentID, bool deleteComponents)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.DeleteProductParent(productParentID, deleteComponents);
			}
		}

		public bool DeleteComponents(string[] productIDs)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.DeleteComponents(productIDs);
			}
		}

		public ProductParentData GetProductParentByID(string id)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.GetProductParentByID(id);
			}
		}

		public DataSet GetProductParentByFields(params string[] columns)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.GetProductParentByFields(columns);
			}
		}

		public DataSet GetProductParentByFields(string[] ids, params string[] columns)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.GetProductParentByFields(ids, columns);
			}
		}

		public DataSet GetProductParentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.GetProductParentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductParentList()
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.GetProductParentList();
			}
		}

		public DataSet GetProductParentComboList()
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.GetProductParentComboList();
			}
		}

		public DataSet GetMatrixTable(string productParentID, bool showAllComponents)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.GetMatrixTable(productParentID, showAllComponents);
			}
		}

		public DataSet GetMatrixQuantityTable(string productParentID, bool showAllComponents)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.GetMatrixQuantityTable(productParentID, showAllComponents);
			}
		}

		public bool AddProductPhoto(string productParentID, byte[] image)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.AddProductPhoto(productParentID, image);
			}
		}

		public bool RemoveProductPhoto(string productParentID)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.RemoveProductPhoto(productParentID);
			}
		}

		public bool AddProductParentPhoto(string productParentID, byte[] image)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				return productParent.AddProductPhoto(productParentID, image);
			}
		}

		public byte[] GetProductThumbnailImage(string productParentID)
		{
			return GetProductThumbnailImage(productParentID, 128, 128);
		}

		public bool AddProductToMatrix(string matrixID, string productID, string attribute1, string attribute2, string attribute3)
		{
			return new ProductParent(config).AddProductToMatrix(matrixID, productID, attribute1, attribute2, attribute3);
		}

		public bool RemoveProductFromMatrix(string matrixID, string productID)
		{
			return new ProductParent(config).RemoveProductFromMatrix(matrixID, productID);
		}

		public byte[] GetProductThumbnailImage(string productParentID, int width, int height)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				byte[] productThumbnailImage = productParent.GetProductThumbnailImage(productParentID);
				return CreateThumbnail(productThumbnailImage, width, height);
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

		public DataSet GetProductParentCatalogReport(string fromItem, string toItem, string fromCategory, string toCategory, bool isInactive, bool showZeroQuantity, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (ProductParent productParent = new ProductParent(config))
			{
				DataSet productParentCatalogReport = productParent.GetProductParentCatalogReport(fromItem, toItem, fromCategory, toCategory, isInactive, showZeroQuantity, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
				foreach (DataRow row in productParentCatalogReport.Tables["Product"].Rows)
				{
					if (row["Photo"] != DBNull.Value)
					{
						byte[] image = (byte[])row["Photo"];
						image = (byte[])(row["Photo"] = CreateThumbnail(image, 128, 128));
					}
				}
				return productParentCatalogReport;
			}
		}

		private byte[] CreateThumbnail(byte[] image, int width, int height)
		{
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
	}
}
