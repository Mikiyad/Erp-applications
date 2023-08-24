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
	public sealed class HorseSummarySystem : MarshalByRefObject, IHorseSummarySystem, IDisposable
	{
		private Config config;

		public HorseSummarySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateHorseSummary(HorseSummaryData data)
		{
			return new HorseSummary(config).InsertHorseSummary(data);
		}

		public bool UpdateHorseSummary(HorseSummaryData data)
		{
			return UpdateHorseSummary(data, checkConcurrency: false);
		}

		public bool UpdateHorseSummary(HorseSummaryData data, bool checkConcurrency)
		{
			return new HorseSummary(config).UpdateHorseSummary(data);
		}

		public HorseSummaryData GetHorseSummary()
		{
			using (HorseSummary horseSummary = new HorseSummary(config))
			{
				return horseSummary.GetHorseSummary();
			}
		}

		public bool DeleteHorseSummary(string groupID)
		{
			using (HorseSummary horseSummary = new HorseSummary(config))
			{
				return horseSummary.DeleteHorseSummary(groupID);
			}
		}

		public HorseSummaryData GetHorseSummaryByID(string id)
		{
			using (HorseSummary horseSummary = new HorseSummary(config))
			{
				return horseSummary.GetHorseSummaryByID(id);
			}
		}

		public byte[] GetHorseThumbnailImage(string horseID)
		{
			return GetHorseThumbnailImage(horseID, 128, 128);
		}

		public byte[] GetHorseThumbnailImage(string horseID, int width, int height)
		{
			using (HorseSummary horseSummary = new HorseSummary(config))
			{
				byte[] horseThumbnailImage = horseSummary.GetHorseThumbnailImage(horseID);
				if (horseThumbnailImage == null)
				{
					return null;
				}
				return CreateThumbnail(horseThumbnailImage, width, height);
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

		public bool AddHorsePhoto(string horseID, byte[] pictureByte)
		{
			using (HorseSummary horseSummary = new HorseSummary(config))
			{
				return horseSummary.AddHorsePhoto(horseID, pictureByte);
			}
		}

		public bool RemoveHorsePhoto(string horseID)
		{
			using (HorseSummary horseSummary = new HorseSummary(config))
			{
				return horseSummary.RemoveHorsePhoto(horseID);
			}
		}

		public DataSet GetHorseSummaryList(bool isInactive)
		{
			using (HorseSummary horseSummary = new HorseSummary(config))
			{
				return horseSummary.GetHorseSummaryList(isInactive);
			}
		}

		public DataSet GetHorseSummaryComboList()
		{
			using (HorseSummary horseSummary = new HorseSummary(config))
			{
				return horseSummary.GetHorseSummaryComboList();
			}
		}

		public DataSet GetHorseSummaryReport(string fromHorse, string toHorse, string fromTrainer, string toTrainer, string fromLocation, string toLocation, bool showInactive)
		{
			using (HorseSummary horseSummary = new HorseSummary(config))
			{
				return horseSummary.GetHorseSummaryReport(fromHorse, toHorse, fromTrainer, toTrainer, fromLocation, toLocation, showInactive);
			}
		}
	}
}
