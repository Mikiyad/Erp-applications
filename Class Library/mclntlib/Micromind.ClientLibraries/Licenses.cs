using System;
using System.IO;


namespace Micromind.ClientLibraries
{
	
	
	public class Licenses
	{
		private string uid = "765EE8C0D4C94440B2C5B52CF75CC9D0";

		private string AFile1 => "mlpcir1.dlg";

		private string AFile2 => "mlpcir2.dlg";

		private string EFile1 => "mlpcirr1.dlo";

		private string EFile2 => "mlpcirr2.dlo";

		private string BFile1 => "bnsns1.spn";

		private string BFile2 => "bnsns2.spn";

		private bool IsExpired()
		{
			return false;
		}

		private bool IsExpired(string path)
		{
			if (File.Exists(path))
			{
				FileStream fileStream = null;
				BinaryReader binaryReader = null;
				try
				{
					fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
					binaryReader = new BinaryReader(fileStream);
					string text = binaryReader.ReadString();
					if (text != null)
					{
						text = text.Trim();
						if (text.Equals(uid))
						{
							return binaryReader.ReadBoolean();
						}
					}
				}
				catch
				{
					return false;
				}
				finally
				{
					fileStream?.Close();
					binaryReader?.Close();
				}
			}
			return false;
		}

		private string GetFile1()
		{
			if (GlobalRules.IsBeta)
			{
				return BFile1;
			}
			if (GlobalRules.IsTrial)
			{
				return EFile1;
			}
			return AFile1;
		}

		private string GetFile2()
		{
			if (GlobalRules.IsBeta)
			{
				return BFile2;
			}
			if (GlobalRules.IsTrial)
			{
				return EFile2;
			}
			return AFile2;
		}

		private void Expire(DateTime expireDate)
		{
			string text = "";
			text = Environment.SystemDirectory + Path.DirectorySeparatorChar.ToString() + GetFile2();
			Expire(expireDate, text);
		}

		private void Expire(DateTime expireDate, string path)
		{
			FileStream fileStream = null;
			BinaryWriter binaryWriter = null;
			try
			{
				fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
				binaryWriter = new BinaryWriter(fileStream);
				binaryWriter.Write(uid);
				binaryWriter.Write(value: true);
				binaryWriter.Write(expireDate.Year);
				binaryWriter.Write(expireDate.Month);
				binaryWriter.Write(expireDate.Day);
			}
			catch
			{
			}
			finally
			{
				fileStream?.Close();
				binaryWriter?.Close();
				try
				{
					File.SetAttributes(path, FileAttributes.ReadOnly | FileAttributes.Hidden);
				}
				catch
				{
				}
			}
		}

		internal int GetRunNumbers()
		{
			int result = 0;
			string str = Environment.SystemDirectory + Path.DirectorySeparatorChar.ToString();
			str += GetFile1();
			if (File.Exists(str))
			{
				FileStream fileStream = null;
				BinaryReader binaryReader = null;
				try
				{
					fileStream = new FileStream(str, FileMode.Open, FileAccess.Read);
					binaryReader = new BinaryReader(fileStream);
					string text = binaryReader.ReadString();
					if (text == null)
					{
						return result;
					}
					text = text.Trim();
					if (!text.Equals(uid))
					{
						return result;
					}
					result = binaryReader.ReadInt32();
					return result;
				}
				catch
				{
					CreateExpiryFile(str);
					return result;
				}
				finally
				{
					fileStream?.Close();
					binaryReader?.Close();
				}
			}
			CreateExpiryFile(str);
			return result;
		}

		private void CreateExpiryFile(string path)
		{
			FileStream fileStream = null;
			BinaryWriter binaryWriter = null;
			int value = 1;
			try
			{
				if (File.Exists(path))
				{
					fileStream = new FileStream(path, FileMode.Truncate, FileAccess.Write);
					value = 10;
				}
				else
				{
					fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
				}
				binaryWriter = new BinaryWriter(fileStream);
				binaryWriter.Write(uid);
				binaryWriter.Write(value);
			}
			catch
			{
			}
			finally
			{
				fileStream?.Close();
				binaryWriter?.Close();
				File.SetAttributes(path, FileAttributes.Hidden);
			}
		}

		public void IncrementDay()
		{
			string str = Environment.SystemDirectory + Path.DirectorySeparatorChar.ToString();
			str += GetFile1();
			FileStream fileStream = null;
			BinaryWriter binaryWriter = null;
			BinaryReader binaryReader = null;
			int num = 0;
			try
			{
				fileStream = new FileStream(str, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				binaryReader = new BinaryReader(fileStream);
				string text = binaryReader.ReadString();
				if (text != null)
				{
					text = text.Trim();
					if (text.Equals(uid))
					{
						num = binaryReader.ReadInt32();
						try
						{
							num = checked(num + 1);
						}
						catch
						{
						}
					}
				}
				binaryWriter = new BinaryWriter(fileStream);
				fileStream.Seek(0L, SeekOrigin.Begin);
				binaryWriter.Write(uid);
				binaryWriter.Write(num);
			}
			catch
			{
			}
			finally
			{
				fileStream?.Close();
				binaryWriter?.Close();
				try
				{
					File.SetAttributes(str, FileAttributes.Hidden);
				}
				catch
				{
				}
			}
		}
	}
}
