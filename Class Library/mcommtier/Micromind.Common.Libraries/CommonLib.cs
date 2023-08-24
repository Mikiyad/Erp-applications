using Micromind.Securities;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;

namespace Micromind.Common.Libraries
{
	public sealed class CommonLib
	{
		private static bool invalid;

		public static string GetNumberRange(string[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			checked
			{
				string[] array2;
				for (int i = 0; i < data.Length; i++)
				{
					string[] array = data[i].Split('-');
					if (array.Length == 2)
					{
						try
						{
							int num = int.Parse(array[0]);
							int num2 = int.Parse(array[1]);
							if (num < num2)
							{
								for (int j = num; j <= num2; j++)
								{
									stringBuilder.Append(j).Append(",");
								}
								stringBuilder.Remove(stringBuilder.Length - 1, 1);
							}
							else
							{
								stringBuilder.Append(num).Append(",").Append(num2);
							}
						}
						catch
						{
							stringBuilder.Append(AddRange(array[0], array[1]));
						}
						data[i] = stringBuilder.ToString();
						stringBuilder.Remove(0, stringBuilder.Length);
						continue;
					}
					if (array.Length > 2)
					{
						array2 = array;
						foreach (string value in array2)
						{
							stringBuilder.Append(value).Append(",");
						}
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
					}
					else if (array.Length == 1)
					{
						stringBuilder.Append(array[0]);
					}
					data[i] = stringBuilder.ToString();
					stringBuilder.Remove(0, stringBuilder.Length);
				}
				string text = "";
				array2 = data;
				foreach (string str in array2)
				{
					text = text + str + ",";
				}
				if (text.Length > 0)
				{
					text = text.Remove(text.Length - 1, 1);
				}
				return text;
			}
		}

		public static decimal ConvertToFC(decimal baseAmount, string currencyID, string rateType, decimal currencyRate)
		{
			if (currencyRate == 0m)
			{
				return 0m;
			}
			decimal num = default(decimal);
			if (rateType == "M")
			{
				return Math.Round(baseAmount / currencyRate, 4);
			}
			return Math.Round(baseAmount * currencyRate, 4);
		}

		public static decimal ConvertToBaseCurrency(decimal fcAmount, string currencyID, string rateType, decimal currencyRate)
		{
			if (currencyRate == 0m)
			{
				return 0m;
			}
			decimal num = default(decimal);
			if (rateType == "M")
			{
				return Math.Round(fcAmount * currencyRate, 4);
			}
			return Math.Round(fcAmount / currencyRate, 4);
		}

		public static string AddRange(string number1, string number2)
		{
			char[] array = number1.ToCharArray();
			char[] array2 = number2.ToCharArray();
			string text = "";
			string str = "";
			int num = 0;
			int num2 = 0;
			checked
			{
				if (array.Length != 0 && number1 != "0" && (array[0] == '0' || char.IsLetter(array[0])))
				{
					do
					{
						text += array[num++].ToString();
					}
					while (num < array.Length && (array[num] == '0' || char.IsLetter(array[num])));
				}
				if (array2.Length != 0 && number2 != "0" && (array2[0] == '0' || char.IsLetter(array2[0])))
				{
					do
					{
						str += array2[num2++].ToString();
					}
					while (num2 < array2.Length && (array2[num2] == '0' || char.IsLetter(array2[num2])));
				}
				string text2 = "";
				int num3 = 0;
				string text3 = "";
				int num4 = 0;
				for (num3 = num; num3 < array.Length && char.IsDigit(array[num3]); num3++)
				{
					text2 += array[num3].ToString();
				}
				for (num4 = num2; num4 < array2.Length && char.IsDigit(array2[num4]); num4++)
				{
					text3 += array2[num4].ToString();
				}
				int num5;
				try
				{
					num5 = int.Parse(text2);
				}
				catch
				{
					num5 = 0;
				}
				int num6;
				try
				{
					num6 = int.Parse(text3);
				}
				catch
				{
					num6 = 0;
				}
				text2 = "";
				text3 = "";
				for (int i = num3; i < array.Length; i++)
				{
					text2 += array[i].ToString();
				}
				for (int j = num4; j < array2.Length; j++)
				{
					text3 += array2[j].ToString();
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (num5 < num6)
				{
					if (text2 == text3)
					{
						for (int k = num5; k <= num6; k++)
						{
							string text4 = k.ToString() + text2;
							if (text.Length > 0)
							{
								text4 = text + text4;
							}
							stringBuilder.Append(text4).Append(",");
						}
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
					}
					else
					{
						for (int l = num5; l <= num6; l++)
						{
							string text5 = l.ToString() + text2;
							if (text.Length > 0)
							{
								text5 = text + text5;
							}
							stringBuilder.Append(text5).Append(",");
						}
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
					}
				}
				else
				{
					stringBuilder.Append(text2 + num5.ToString()).Append(",").Append(text3 + num6.ToString());
				}
				return stringBuilder.ToString();
			}
		}

		public static string ToSqlDateTimeString(DateTime date)
		{
			if (date < SqlDateTime.MinValue.Value)
			{
				date = SqlDateTime.MinValue.Value;
			}
			else if (date > SqlDateTime.MaxValue.Value)
			{
				date = SqlDateTime.MaxValue.Value;
			}
			return date.Month + "-" + date.Day + "-" + date.Year + " " + date.Hour + ":" + date.Minute + ":" + date.Second;
		}

		public static string ToSqlArrayString(string[] items)
		{
			string text = "";
			checked
			{
				for (int i = 0; i < items.Length; i++)
				{
					text = text + "'" + items[i] + "'";
					if (i < items.Length - 1)
					{
						text += ",";
					}
				}
				return text;
			}
		}

		public string AddSingleQuote(string str)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			int num;
			for (num = 0; num != str.Length; num = checked(num + 2))
			{
				num = str.IndexOf('\'', num);
				if (num < 0)
				{
					break;
				}
				str = str.Insert(num, "'");
			}
			return str;
		}

		public static object DeserializeFromStream(byte[] streamBytes)
		{
			try
			{
				if (streamBytes.Length == 0)
				{
					return null;
				}
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(streamBytes, 0, streamBytes.Length);
				return DeserializeFromStream(memoryStream);
			}
			catch
			{
				throw;
			}
		}

		public static object DeserializeFromStream(MemoryStream stream)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			stream.Seek(0L, SeekOrigin.Begin);
			return ((IFormatter)binaryFormatter).Deserialize((Stream)stream);
		}

		public static string GetNextDocumentNumber(string lastNumber)
		{
			string text = "";
			int num = 1;
			checked
			{
				for (int i = 0; i < lastNumber.Length && !char.IsNumber(lastNumber[i]); i++)
				{
					text += lastNumber[i].ToString();
				}
				num = int.Parse(lastNumber.Substring(text.Length)) + 1;
				if (lastNumber != "")
				{
					int num2 = lastNumber.Length - text.Length;
					string text2 = "";
					for (int j = 0; j < num2; j++)
					{
						text2 += "0";
					}
					return text + num.ToString(text2);
				}
				return text + num.ToString("000000");
			}
		}

		public static string GetNumberPrefix(string voucherID)
		{
			int num = voucherID.Length;
			checked
			{
				int num2 = voucherID.Length - 1;
				while (num2 >= 0)
				{
					if (char.IsDigit(voucherID[num2]))
					{
						num--;
						num2--;
						continue;
					}
					return voucherID.Substring(0, num);
				}
				return voucherID.Substring(0, num);
			}
		}

		public static bool IsValidEmail(string emailID)
		{
			string[] array = emailID.Split(new string[1]
			{
				","
			}, StringSplitOptions.None);
			for (int i = 0; i < array.Length; i = checked(i + 1))
			{
				if (!IsValidEmailAddress(array[i].Trim()))
				{
					return false;
				}
			}
			return true;
		}

		private static bool IsValidEmailAddress(string strIn)
		{
			invalid = false;
			if (string.IsNullOrEmpty(strIn))
			{
				return false;
			}
			try
			{
				strIn = Regex.Replace(strIn, "(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200.0));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
			if (invalid)
			{
				return false;
			}
			try
			{
				return Regex.IsMatch(strIn, "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250.0));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}

		private static string DomainMapper(Match match)
		{
			IdnMapping idnMapping = new IdnMapping();
			string text = match.Groups[2].Value;
			try
			{
				text = idnMapping.GetAscii(text);
			}
			catch (ArgumentException)
			{
				invalid = true;
			}
			return match.Groups[1].Value + text;
		}

		public static string Encrypt(string cryptorID, string str)
		{
			return new ConfigHelper(cryptorID).Cryptor.Encrypt(str);
		}

		public static string Decrypt(string cryptorID, string str)
		{
			return new ConfigHelper(cryptorID).Cryptor.Decrypt(str);
		}

		public static string GetAxolonPassword(string cryptorID, string password, bool isEncrypted)
		{
			try
			{
				ConfigHelper configHelper = new ConfigHelper(cryptorID);
				if (!isEncrypted)
				{
					password = configHelper.Cryptor.Encrypt(password);
				}
				password = configHelper.Cryptor.Encrypt(password);
				password = password.Substring(3, 10);
				return password;
			}
			catch
			{
				throw;
			}
		}

		public static byte[] CompressData(byte[] inbyt)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				new MemoryStream();
				DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress);
				deflateStream.Write(inbyt, 0, inbyt.Length);
				deflateStream.Flush();
				deflateStream.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return memoryStream.ToArray();
		}

		public static byte[] CompressDataSet(DataSet ds)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				ds.RemotingFormat = SerializationFormat.Binary;
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				MemoryStream memoryStream2 = new MemoryStream();
				binaryFormatter.Serialize(memoryStream2, ds);
				byte[] array = memoryStream2.ToArray();
				DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress);
				deflateStream.Write(array, 0, array.Length);
				deflateStream.Flush();
				deflateStream.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return memoryStream.ToArray();
		}

		public static DataSet DecompressDataSet(byte[] bytDs)
		{
			try
			{
				DataSet dataSet = new DataSet();
				MemoryStream memoryStream = new MemoryStream(bytDs);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, leaveOpen: true);
				new StreamReader(deflateStream);
				MemoryStream memoryStream2 = new MemoryStream();
				deflateStream.CopyTo(memoryStream2);
				memoryStream2.Seek(0L, SeekOrigin.Begin);
				dataSet.RemotingFormat = SerializationFormat.Binary;
				return (DataSet)new BinaryFormatter().Deserialize(memoryStream2, null);
			}
			catch
			{
				return null;
			}
		}

		public static byte[] DecompressData(byte[] bytDs)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream(bytDs);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, leaveOpen: true);
				new StreamReader(deflateStream);
				MemoryStream memoryStream2 = new MemoryStream();
				deflateStream.CopyTo(memoryStream2);
				memoryStream2.Seek(0L, SeekOrigin.Begin);
				return memoryStream2.ToArray();
			}
			catch
			{
				return null;
			}
		}

		public static int GetObjectSize(object TestObject)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, TestObject);
			return memoryStream.ToArray().Length;
		}
	}
}
