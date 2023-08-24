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
	public sealed class PropertyUnitSystem : MarshalByRefObject, IPropertyUnitSystem, IDisposable
	{
		private Config config;

		public PropertyUnitSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyUnit(PropertyUnitData data)
		{
			return new PropertyUnit(config).InsertUpdatePropertyUnit(data, isUpdate: false);
		}

		public bool UpdatePropertyUnit(PropertyUnitData data)
		{
			return UpdatePropertyUnit(data, checkConcurrency: false);
		}

		public bool UpdatePropertyUnit(PropertyUnitData data, bool checkConcurrency)
		{
			return new PropertyUnit(config).InsertUpdatePropertyUnit(data, isUpdate: true);
		}

		public PropertyUnitData GetPropertyUnit()
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.GetPropertyUnit();
			}
		}

		public bool DeletePropertyUnit(string groupID)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.DeletePropertyUnit(groupID);
			}
		}

		public PropertyUnitData GetPropertyUnitByID(string id)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.GetPropertyUnitByID(id);
			}
		}

		public DataSet GetPropertyUnitByFields(params string[] columns)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.GetPropertyUnitByFields(columns);
			}
		}

		public DataSet GetPropertyUnitByFields(string[] ids, params string[] columns)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.GetPropertyUnitByFields(ids, columns);
			}
		}

		public DataSet GetPropertyUnitByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.GetPropertyUnitByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyUnitList(bool showInactive)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.GetPropertyUnitList(showInactive);
			}
		}

		public DataSet GetPropertyUnitComboList()
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.GetPropertyUnitComboList();
			}
		}

		public byte[] GetPropertyUnitThumbnailImage(string unitID)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				byte[] propertyUnitThumbnailImage = propertyUnit.GetPropertyUnitThumbnailImage(unitID);
				if (propertyUnitThumbnailImage == null)
				{
					return null;
				}
				return CreateThumbnail(propertyUnitThumbnailImage, 128, 128);
			}
		}

		public bool AddPropertyUnitPhoto(string unitID, byte[] pictureByte)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.AddPropertyUnitPhoto(unitID, pictureByte);
			}
		}

		public bool RemovePropertyUnitPhoto(string unitID)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.RemovePropertyUnitPhoto(unitID);
			}
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

		private bool ThumbnailImageAbort()
		{
			return true;
		}

		public DataSet GetPropertyUnitCurrentTenant(string propertyUnitID)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.GetPropertyUnitCurrentTenant(propertyUnitID);
			}
		}

		public DataSet GetPropertyUnitHistoryReport(string propertyUnitID)
		{
			using (PropertyUnit propertyUnit = new PropertyUnit(config))
			{
				return propertyUnit.GetPropertyUnitHistoryReport(propertyUnitID);
			}
		}
	}
}
