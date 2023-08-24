using System;
using System.IO;

namespace Micromind.Utilities
{
	public class FileUtility
	{
		private const string DELIMITER_BACKSLASH = "\\";

		private const string DELIMITER_FORWARDSLASH = "/";

		public static bool IsUNCPath(string path)
		{
			if (new Uri(path).IsUnc)
			{
				return true;
			}
			return false;
		}

		public static string AppendSlashURLorUNC(string path)
		{
			if (IsUNCPath(path))
			{
				return AppendTerminalBackSlash(path);
			}
			return AppendTerminalForwardSlash(path);
		}

		public static string ConvertToUNCPath(string path)
		{
			return path.Replace("/", "\\");
		}

		public static string AppendTerminalBackSlash(string path)
		{
			if (path.IndexOf("\\", path.Length - 1) == -1)
			{
				return path + "\\";
			}
			return path;
		}

		public static string AppendTerminalForwardSlash(string path)
		{
			if (path.IndexOf("/", path.Length - 1) == -1)
			{
				return path + "/";
			}
			return path;
		}

		public static string GetRootDirectoryFromFilePath(string path)
		{
			return AppendTerminalBackSlash(Path.GetDirectoryName(path));
		}

		public static void DeleteDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				try
				{
					Directory.Delete(path, recursive: true);
				}
				catch (UnauthorizedAccessException ex)
				{
					throw ex;
				}
			}
		}

		public static void DeleteFile(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					File.Delete(path);
				}
				catch (UnauthorizedAccessException ex)
				{
					throw ex;
				}
			}
		}

		public static void CopyDirectory(string sourcePath, string destPath, bool overWrite)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);
			CopyDirRecurse(destinationPath: new DirectoryInfo(destPath).FullName, sourcePath: directoryInfo.FullName, overWrite: overWrite);
		}

		private static void CopyDirRecurse(string sourcePath, string destinationPath, bool overWrite)
		{
			sourcePath = AppendTerminalBackSlash(sourcePath);
			destinationPath = AppendTerminalBackSlash(destinationPath);
			FileSystemInfo[] fileSystemInfos = new DirectoryInfo(sourcePath).GetFileSystemInfos();
			foreach (FileSystemInfo fileSystemInfo in fileSystemInfos)
			{
				try
				{
					if (fileSystemInfo is FileInfo)
					{
						File.Copy(fileSystemInfo.FullName, destinationPath + Path.DirectorySeparatorChar.ToString() + fileSystemInfo.Name, overWrite);
					}
					else
					{
						Directory.CreateDirectory(destinationPath + Path.DirectorySeparatorChar.ToString() + fileSystemInfo.Name);
						CopyDirRecurse(fileSystemInfo.FullName, destinationPath + Path.DirectorySeparatorChar.ToString() + fileSystemInfo.Name, overWrite);
					}
				}
				catch
				{
				}
			}
		}
	}
}
