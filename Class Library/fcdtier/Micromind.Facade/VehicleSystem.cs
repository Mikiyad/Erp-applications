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
	public sealed class VehicleSystem : MarshalByRefObject, IVehicleSystem, IDisposable
	{
		private Config config;

		public VehicleSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateVehicle(VehicleData data)
		{
			return new Vehicle(config).InsertVehicle(data);
		}

		public bool UpdateVehicle(VehicleData data)
		{
			return UpdateVehicle(data, checkConcurrency: false);
		}

		public bool UpdateVehicle(VehicleData data, bool checkConcurrency)
		{
			return new Vehicle(config).UpdateVehicle(data);
		}

		public VehicleData GetVehicle()
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.GetVehicle();
			}
		}

		public bool DeleteVehicle(string groupID)
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.DeleteVehicle(groupID);
			}
		}

		public VehicleData GetVehicleByID(string id)
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.GetVehicleByID(id);
			}
		}

		public DataSet GetVehicleByFields(params string[] columns)
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.GetVehicleByFields(columns);
			}
		}

		public DataSet GetVehicleByFields(string[] ids, params string[] columns)
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.GetVehicleByFields(ids, columns);
			}
		}

		public DataSet GetVehicleByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.GetVehicleByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetVehicleList()
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.GetVehicleList();
			}
		}

		public DataSet GetVehicleComboList()
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.GetVehicleComboList();
			}
		}

		public DataSet GetVehicleList(string fromVehicle, string toVehicle)
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.GetVehicleList(fromVehicle, toVehicle);
			}
		}

		public bool AddVehiclePhoto(string vehicleID, byte[] pictureByte)
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.AddVehiclePhoto(vehicleID, pictureByte);
			}
		}

		public byte[] GetVehicleThumbnailImage(string vehicleID)
		{
			return GetVehicleThumbnailImage(vehicleID, 128, 128);
		}

		public byte[] GetVehicleThumbnailImage(string vehicleID, int width, int height)
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				MemoryStream memoryStream = vehicle.GetvehicleThumbnailImage(vehicleID);
				if (memoryStream == null)
				{
					return null;
				}
				return CreateThumbnail(memoryStream, width, height);
			}
		}

		private byte[] CreateThumbnail(MemoryStream stream, int width, int height)
		{
			if (stream == null)
			{
				return null;
			}
			ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
			Bitmap bitmap = new Bitmap(stream);
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
				stream = new MemoryStream();
				thumbnailImage.Save(stream, encoder, encoderParameters);
				return stream.ToArray();
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

		public bool RemoveVehiclePhoto(string vehicleID)
		{
			using (Vehicle vehicle = new Vehicle(config))
			{
				return vehicle.RemoveVehiclePhoto(vehicleID);
			}
		}
	}
}
