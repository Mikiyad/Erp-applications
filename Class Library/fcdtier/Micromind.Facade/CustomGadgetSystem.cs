using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.Data;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Micromind.Facade
{
	public sealed class CustomGadgetSystem : MarshalByRefObject, ICustomGadgetSystem, IDisposable
	{
		private Config config;

		public CustomGadgetSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomGadget(CustomGadgetData customGadgetData)
		{
			if (new CustomGadgets(config).InsertUpdateCustomGadget(customGadgetData, isUpdate: false))
			{
				return true;
			}
			return false;
		}

		public bool UpdateCustomGadget(CustomGadgetData customGadgetData)
		{
			return UpdateCustomGadget(customGadgetData, checkConcurrency: true);
		}

		public bool UpdateCustomGadget(CustomGadgetData customGadgetData, bool checkConcurrency)
		{
			return new CustomGadgets(config).InsertUpdateCustomGadget(customGadgetData, isUpdate: true);
		}

		public CustomGadgetData GetCustomGadgets()
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.GetCustomGadgets();
			}
		}

		public bool DeleteCustomGadget(string customGadgetID)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.DeleteCustomGadget(customGadgetID);
			}
		}

		public DataSet GetCustomGadgetsByFields(params string[] columns)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.GetCustomGadgetsByFields(columns);
			}
		}

		public DataSet GetCustomGadgetsByFields(string[] customGadgetsID, params string[] columns)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.GetCustomGadgetsByFields(customGadgetsID, columns);
			}
		}

		public DataSet GetCustomGadgetsByFields(string[] customGadgetsID, bool isInactive, params string[] columns)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.GetCustomGadgetsByFields(customGadgetsID, isInactive, columns);
			}
		}

		public CustomGadgetData GetCustomGadgetByID(string customGadgetID)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.GetCustomGadgetByID(customGadgetID);
			}
		}

		public bool ExistCustomGadget(string customGadgetName)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.ExistCustomGadget(customGadgetName);
			}
		}

		public bool CreateUpdateCustomGadgetBatch(DataSet listData, bool checkConcurrency)
		{
			ApplicationAssert.CheckCondition(listData != null, "listData Parameter cannot be null.", 0);
			ApplicationAssert.CheckCondition(listData.Tables.Contains("Custom_Gadget"), "CustomGadget Data must exist.", 0);
			CustomGadgets customGadgets = new CustomGadgets(config);
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				foreach (DataRow row in listData.Tables["Custom_Gadget"].Rows)
				{
					bool flag = false;
					string text = "-1";
					string text2 = string.Empty;
					if (listData.Tables["Custom_Gadget"].Columns.Contains("CustomGadgetName"))
					{
						text2 = row["CustomGadgetName"].ToString();
					}
					if (!(text2 == string.Empty))
					{
						CustomGadgetData customGadgetData = new CustomGadgetData();
						DataRow dataRow2 = customGadgetData.CustomGadgetTable.NewRow();
						bool flag2 = true;
						bool flag3 = true;
						foreach (DataColumn column in listData.Tables["Custom_Gadget"].Columns)
						{
							try
							{
								if (customGadgetData.CustomGadgetTable.Columns.Contains(column.ColumnName))
								{
									dataRow2[column.ColumnName] = row[column.ColumnName];
								}
								else if (column.ColumnName.ToLower() == "OverwriteExistingRecord".ToLower())
								{
									flag2 = bool.Parse(row[column.ColumnName].ToString());
								}
								else if (column.ColumnName.ToLower() == "CreateNewRecord".ToLower())
								{
									flag3 = bool.Parse(row[column.ColumnName].ToString());
								}
							}
							catch
							{
							}
						}
						customGadgetData.CustomGadgetTable.Rows.Add(dataRow2);
						if (customGadgets.ExistCustomGadget(text2))
						{
							flag = true;
							text = customGadgets.GetCustomGadgetIDByName(text2);
							customGadgetData.CustomGadgetTable.AcceptChanges();
							dataRow2["CustomGadgetID"] = text;
						}
						try
						{
							if (!flag)
							{
								if (flag3)
								{
									CreateCustomGadget(customGadgetData);
								}
							}
							else if (flag2)
							{
								UpdateCustomGadget(customGadgetData, checkConcurrency);
							}
						}
						catch (Exception ex)
						{
							stringBuilder.Append(ex.Message).Append("\n");
						}
						customGadgetData.Dispose();
						customGadgetData = null;
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				customGadgets.Dispose();
				customGadgets = null;
				if (stringBuilder.Length > 0)
				{
					ApplicationLog.WriteImportLogError(stringBuilder);
				}
				stringBuilder = null;
			}
			return true;
		}

		public DataSet GetCustomGadgetComboList()
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.GetCustomGadgetComboList();
			}
		}

		public DataSet GetCustomGadgetList()
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.GetCustomGadgetList();
			}
		}

		public DataSet GetTableSchema(string query)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.GetTableSchema(query);
			}
		}

		public DataSet GetCustomGadgetData(string reportID, string[] parArray, string[] valArray)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.GetCustomGadgetData(reportID, parArray, valArray);
			}
		}

		public bool SaveLayout(string reportID, byte[] layout, int formWidth, int formHeight)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.SaveLayout(reportID, layout, formWidth, formHeight);
			}
		}

		public bool AddGadgetPhoto(string productID, byte[] pictureByte)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				return customGadgets.AddGadgetPhoto(productID, pictureByte);
			}
		}

		public byte[] GetGadgetThumbnailImage(string vehicleID)
		{
			return GetGadgetThumbnailImage(vehicleID, 128, 128);
		}

		public byte[] GetGadgetThumbnailImage(string vehicleID, int width, int height)
		{
			using (CustomGadgets customGadgets = new CustomGadgets(config))
			{
				MemoryStream memoryStream = customGadgets.GetgadgetThumbnailImage(vehicleID);
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
				System.Drawing.Imaging.Encoder quality = System.Drawing.Imaging.Encoder.Quality;
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
	}
}
