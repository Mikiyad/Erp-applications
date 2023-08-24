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
	public sealed class PropertySystem : MarshalByRefObject, IPropertySystem, IDisposable
	{
		private Config config;

		public PropertySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProperty(PropertyData data)
		{
			return new Property(config).InsertUpdateProperty(data, isUpdate: false);
		}

		public bool UpdateProperty(PropertyData data)
		{
			return UpdateProperty(data, checkConcurrency: false);
		}

		public bool UpdateProperty(PropertyData data, bool checkConcurrency)
		{
			return new Property(config).InsertUpdateProperty(data, isUpdate: true);
		}

		public PropertyData GetProperty()
		{
			using (Property property = new Property(config))
			{
				return property.GetProperty();
			}
		}

		public bool DeleteProperty(string groupID)
		{
			using (Property property = new Property(config))
			{
				return property.DeleteProperty(groupID);
			}
		}

		public PropertyData GetPropertyByID(string id)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyByID(id);
			}
		}

		public DataSet GetPropertyByFields(params string[] columns)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyByFields(columns);
			}
		}

		public DataSet GetPropertyByFields(string[] ids, params string[] columns)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyByFields(ids, columns);
			}
		}

		public DataSet GetPropertyByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyList(bool showInactive)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyList(showInactive);
			}
		}

		public DataSet GetPropertyComboList()
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyComboList();
			}
		}

		public DataSet GetPropertyRentDetailsReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromUnit, string toUnit, string fromProperty, string toProperty, string Type, string customerIDs, string fromAgent, string toAgent)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyRentDetailsReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromUnit, toUnit, fromProperty, toProperty, Type, customerIDs, fromAgent, toAgent);
			}
		}

		public DataSet GetPropertyAvailabilityReport(DateTime date, string fromUnit, string toUnit, string fromProperty, string toProperty, string fromPropertyClass, string toPropertyClass)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyAvailabilityReport(date, fromUnit, toUnit, fromProperty, toProperty, fromPropertyClass, toPropertyClass);
			}
		}

		public DataSet GetPropertyUnitAvailabilityReport(DateTime from, DateTime to, string fromUnit, string toUnit, string fromProperty, string toProperty, string basedOn)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyUnitAvailabilityReport(from, to, fromUnit, toUnit, fromProperty, toProperty, basedOn);
			}
		}

		public DataSet GetPropertyUnitHistoryReport(DateTime from, DateTime to, string fromUnit, string toUnit, string fromProperty, string toProperty)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyUnitHistoryReport(from, to, fromUnit, toUnit, fromProperty, toProperty);
			}
		}

		public DataSet GetPropertyReport(DateTime date, string fromProperty, string toProperty, string fromUnit, string toUnit)
		{
			using (Property property = new Property(config))
			{
				return property.GetPropertyReport(date, fromProperty, toProperty, fromUnit, toUnit);
			}
		}

		public byte[] GetPropertyThumbnailImage(string contactID)
		{
			using (Property property = new Property(config))
			{
				byte[] propertyThumbnailImage = property.GetPropertyThumbnailImage(contactID);
				if (propertyThumbnailImage == null)
				{
					return null;
				}
				return CreateThumbnail(propertyThumbnailImage, 128, 128);
			}
		}

		public bool AddPropertyPhoto(string propertyID, byte[] pictureByte)
		{
			using (Property property = new Property(config))
			{
				return property.AddPropertyPhoto(propertyID, pictureByte);
			}
		}

		public bool RemovePropertyPhoto(string propertyID)
		{
			using (Property property = new Property(config))
			{
				return property.RemovePropertyPhoto(propertyID);
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
	}
}
