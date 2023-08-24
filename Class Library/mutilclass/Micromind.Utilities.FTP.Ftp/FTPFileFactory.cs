using System;

namespace Micromind.Utilities.FTP.Ftp
{
	public class FTPFileFactory
	{
		internal const string WINDOWS_STR = "WINDOWS";

		internal const string UNIX_STR = "UNIX";

		private string system;

		private FTPFileParser windows = new WindowsFileParser();

		private FTPFileParser unix = new UnixFileParser();

		private FTPFileParser parser;

		private bool rotateParsersOnFail = true;

		private void InitBlock()
		{
		}

		internal FTPFileFactory(string system)
		{
			InitBlock();
			SetParser(system);
		}

		public FTPFileFactory(FTPFileParser parser)
		{
			InitBlock();
			this.parser = parser;
			rotateParsersOnFail = false;
		}

		private void SetParser(string system)
		{
			this.system = system;
			if (system.ToUpper().StartsWith("WINDOWS"))
			{
				parser = windows;
				return;
			}
			if (system.ToUpper().StartsWith("UNIX"))
			{
				parser = unix;
				return;
			}
			throw new FTPException("Unknown SYST: " + system);
		}

		internal virtual FTPFile[] Parse(string[] files)
		{
			FTPFile[] array = new FTPFile[files.Length];
			if (files.Length == 0)
			{
				return array;
			}
			int num = 0;
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					FTPFile fTPFile = parser.Parse(files[i]);
					if (fTPFile != null)
					{
						array[num++] = fTPFile;
					}
				}
				catch (FormatException ex)
				{
					if (!rotateParsersOnFail || num != 0)
					{
						throw ex;
					}
					RotateParsers();
					FTPFile fTPFile2 = parser.Parse(files[i]);
					if (fTPFile2 != null)
					{
						array[num++] = fTPFile2;
					}
				}
			}
			FTPFile[] array2 = new FTPFile[num];
			Array.Copy(array, 0, array2, 0, num);
			return array2;
		}

		private void RotateParsers()
		{
			if (parser == unix)
			{
				parser = windows;
			}
			else if (parser == windows)
			{
				parser = unix;
			}
		}

		public virtual string GetSystem()
		{
			return system;
		}
	}
}
