using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Micromind.DataControls.QueryBuilder
{
	public class Functions
	{
		public enum eReg
		{
			Main,
			WorkDir,
			WorkState
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
		internal struct WIN32_FIND_DATAW
		{
			public const int MAX_PATH = 260;

			public uint dwFileAttributes;

			public FILETIME ftCreationTime;

			public FILETIME ftLastAccessTime;

			public FILETIME ftLastWriteTime;

			public uint nFileSizeHigh;

			public uint nFileSizeLow;

			public uint dwReserved0;

			public uint dwReserved1;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string cFileName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string cAlternateFileName;
		}

		[ComImport]
		[Guid("00021401-0000-0000-C000-000000000046")]
		[ClassInterface(ClassInterfaceType.None)]
		internal class ShellLinkObject
		{
			
		}

		[ComImport]
		[Guid("000214F9-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IShellLinkW
		{
			void GetPath([Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cch, [MarshalAs(UnmanagedType.Struct)] ref WIN32_FIND_DATAW pfd, uint fFlags);

			void GetIDList(out IntPtr ppidl);

			void SetIDList(IntPtr pidl);

			void GetDescription([Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cch);

			void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

			void GetWorkingDirectory([Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cch);

			void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

			void GetArguments([Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cch);

			void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

			void GetHotkey(out ushort pwHotkey);

			void SetHotkey(ushort wHotkey);

			void GetShowCmd(out int piShowCmd);

			void SetShowCmd(int iShowCmd);

			void GetIconLocation([Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cch, out int piIcon);

			void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

			void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, uint dwReserved);

			void Resolve(IntPtr hwnd, uint fFlags);

			void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
		}

		public enum eShowMode
		{
			Normal = 1,
			Maximized = 3,
			Minimized = 7
		}

		private const string ms_RegistryRoot = "Software\\ElmueSoft\\SqlBuilder";

		private static string ms_NullColor;

		public static string ExePath;

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		private static extern void OutputDebugStringA(string s_Text);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		private static extern int FormatMessageA(int Flags, int Unused1, int Error, int Unused2, StringBuilder s_Text, int BufLen, int Unused3);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern int FormatMessageW(int Flags, int Unused1, int Error, int Unused2, StringBuilder s_Text, int BufLen, int Unused3);

		[DllImport("kernel32.dll")]
		private static extern int GetCurrentThreadId();

		[DllImport("user32.dll")]
		private static extern int GetWindowThreadProcessId(IntPtr h_Wnd, out int ProcID);

		[DllImport("user32.dll")]
		public static extern int MapVirtualKey(int Code, int Translation);

		static Functions()
		{
			ms_NullColor = GetHtmlColor(Defaults.GridForeColor(typeof(DBNull)));
			ExePath = Terminate(Path.GetDirectoryName(Application.ExecutablePath));
		}

		public static void PrintDebug(string s_Text)
		{
			OutputDebugStringA(s_Text);
		}

		public static void PrintDebug(string s_Text, Type t_Origin)
		{
			if (t_Origin == Defaults.DebugType)
			{
				OutputDebugStringA(s_Text);
			}
		}

		public static void PrintDebug(string s_Format, object o_Para1, Type t_Origin)
		{
			if (t_Origin == Defaults.DebugType)
			{
				OutputDebugStringA(string.Format(s_Format, o_Para1));
			}
		}

		public static void PrintDebug(string s_Format, object o_Para1, object o_Para2, Type t_Origin)
		{
			if (t_Origin == Defaults.DebugType)
			{
				OutputDebugStringA(string.Format(s_Format, o_Para1, o_Para2));
			}
		}

		public static void PrintDebug(string s_Format, object o_Para1, object o_Para2, object o_Para3, Type t_Origin)
		{
			if (t_Origin == Defaults.DebugType)
			{
				OutputDebugStringA(string.Format(s_Format, o_Para1, o_Para2, o_Para3));
			}
		}

		public static void PrintDebug(string s_Format, object o_Para1, object o_Para2, object o_Para3, object o_Para4, Type t_Origin)
		{
			if (t_Origin == Defaults.DebugType)
			{
				OutputDebugStringA(string.Format(s_Format, o_Para1, o_Para2, o_Para3, o_Para4));
			}
		}

		public static string[] GetWorkDirectories()
		{
			string[] subKeyNames = Registry.CurrentUser.CreateSubKey("Software\\ElmueSoft\\SqlBuilder\\WorkDirs").GetSubKeyNames();
			for (int i = 0; i < subKeyNames.Length; i++)
			{
				subKeyNames[i] = subKeyNames[i].Replace("/", "\\");
			}
			return subKeyNames;
		}

		public static void AddRemoveWorkDir(bool b_Add, string s_Path)
		{
			s_Path = "Software\\ElmueSoft\\SqlBuilder\\WorkDirs\\" + s_Path.Replace("\\", "/");
			if (b_Add)
			{
				Registry.CurrentUser.CreateSubKey(s_Path);
			}
			else
			{
				Registry.CurrentUser.DeleteSubKeyTree(s_Path);
			}
		}

		public static bool RegisterFileExtension(string s_Ext, string s_Menu, string s_App, string s_CmdLine, string s_NewKey)
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\." + s_Ext);
			if (registryKey == null)
			{
				return false;
			}
			string text = ToStr(registryKey.GetValue(""));
			if (text.Length == 0)
			{
				text = s_NewKey;
				registryKey.SetValue("", text);
				registryKey.CreateSubKey("DefaultIcon").SetValue("", s_App + ",0");
			}
			if (string.Compare(text, s_NewKey, ignoreCase: true) == 0)
			{
				s_Menu = "open";
			}
			RegistryKey registryKey2 = Registry.CurrentUser.CreateSubKey("Software\\Classes\\" + text);
			if (registryKey2 == null)
			{
				return false;
			}
			RegistryKey registryKey3 = registryKey2.CreateSubKey("shell").CreateSubKey(s_Menu).CreateSubKey("command");
			if (registryKey3 == null)
			{
				return false;
			}
			registryKey3.SetValue("", $"\"{s_App}\" {s_CmdLine} \"%1\"");
			return true;
		}

		public static void EnumFiles(ArrayList i_FileList, string s_Path, string s_Filter, int s32_Level)
		{
			if (Directory.Exists(s_Path))
			{
				string[] array = s_Filter.Split('|');
				foreach (string text in array)
				{
					RecursiveEnumFiles(i_FileList, s_Path, text.Trim(), s32_Level);
				}
			}
		}

		public static byte[] ReadBinaryResource(string s_ResourceName)
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(s_ResourceName);
			byte[] array = new byte[manifestResourceStream.Length];
			manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
			return array;
		}

		public static void SaveBinaryToFile(string s_Path, byte[] u8_Data)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = File.OpenWrite(s_Path);
				fileStream.Write(u8_Data, 0, u8_Data.Length);
			}
			finally
			{
				fileStream?.Close();
			}
		}

		public static void Execute(string s_File, ProcessWindowStyle e_Wnd)
		{
			Process process = new Process();
			process.StartInfo.FileName = s_File;
			process.StartInfo.WindowStyle = e_Wnd;
			process.Start();
		}

		private static void RecursiveEnumFiles(ArrayList i_FileList, string s_Path, string s_Filter, int s32_Level)
		{
			if (s_Path == null || s_Path == "")
			{
				return;
			}
			Application.DoEvents();
			s_Path = Terminate(s_Path);
			if (s_Path.EndsWith(".svn\\") || s_Path.EndsWith("_svn\\") || s_Path.EndsWith("DeletedSysObjects\\"))
			{
				return;
			}
			string[] files = Directory.GetFiles(s_Path, s_Filter);
			foreach (string value in files)
			{
				i_FileList.Add(value);
			}
			if (s32_Level > 0)
			{
				files = Directory.GetDirectories(s_Path);
				foreach (string s_Path2 in files)
				{
					RecursiveEnumFiles(i_FileList, s_Path2, s_Filter, s32_Level - 1);
				}
			}
		}

		public static string GetFileName(string s_Path)
		{
			if (s_Path == null)
			{
				return "";
			}
			int num = s_Path.LastIndexOf("\\");
			if (num <= 0)
			{
				return s_Path;
			}
			return s_Path.Substring(num + 1);
		}

		public static string GetPath(string s_Path)
		{
			if (s_Path == null)
			{
				return "";
			}
			int num = s_Path.LastIndexOf("\\");
			if (num <= 0)
			{
				return "";
			}
			return s_Path.Substring(0, num + 1);
		}

		public static string GetFileExtension(string s_Path)
		{
			s_Path = GetFileName(s_Path);
			int num = s_Path.LastIndexOf('.');
			if (num <= 0)
			{
				return "";
			}
			return s_Path.Substring(num).ToLower();
		}

		public static string Terminate(string s_Path)
		{
			if (s_Path == null || s_Path.Length == 0)
			{
				return "";
			}
			if (!s_Path.EndsWith("\\"))
			{
				s_Path += "\\";
			}
			return s_Path;
		}

		public static int ToInt(object o_Value)
		{
			if (o_Value == null || o_Value is DBNull)
			{
				return 0;
			}
			return Convert.ToInt32(o_Value);
		}

		public static string ToStr(object Obj)
		{
			if (Obj == null || Obj is DBNull)
			{
				return "";
			}
			string text = Obj.ToString();
			if (Obj is float || Obj is double)
			{
				return text.Replace(',', '.');
			}
			return text;
		}

		public static string DbValueForDisplay(object o_Value, bool b_ForHtml)
		{
			if (o_Value == null)
			{
				return "(NO QUERY RESULT)";
			}
			if (o_Value is DBNull)
			{
				if (b_ForHtml)
				{
					return string.Format("<font color={0}>{1}</font>", ms_NullColor, "(NULL)");
				}
				return "(NULL)";
			}
			if (o_Value is byte[])
			{
				string text = "";
				byte[] array = (byte[])o_Value;
				for (int i = 0; i < array.Length; i++)
				{
					if (i >= 20)
					{
						text += $"..... (Length {FormatSize(array.Length)})";
						break;
					}
					if (text.Length > 0)
					{
						text += ",";
					}
					text += array[i].ToString("X2");
				}
				return text;
			}
			if (o_Value is DateTime)
			{
				return ((DateTime)o_Value).ToString("dd.MM.yyyy HH:mm");
			}
			if (b_ForHtml)
			{
				return ReplaceHtml(o_Value.ToString());
			}
			return o_Value.ToString().Replace("\r", "\\r").Replace("\n", "\\n");
		}

		public static string DbValueForSql(object o_Value)
		{
			if (o_Value is DBNull)
			{
				return "NULL";
			}
			if (o_Value is short || o_Value is int || o_Value is long)
			{
				return o_Value.ToString();
			}
			if (o_Value is bool)
			{
				if (!(bool)o_Value)
				{
					return "0";
				}
				return "1";
			}
			if (o_Value is double || o_Value is decimal)
			{
				return o_Value.ToString().Replace(",", ".");
			}
			if (o_Value is DateTime)
			{
				return "'" + ((DateTime)o_Value).ToString("yyyyMMdd HH:mm") + "'";
			}
			if (o_Value is byte[])
			{
				return "Byte[]";
			}
			string str = o_Value.ToString().TrimEnd().Replace("'", "''");
			return "'" + str + "'";
		}

		public static string DbValueSerialize(object o_Value)
		{
			if (o_Value is DBNull)
			{
				return "@Null@";
			}
			if (o_Value is DateTime)
			{
				return ((DateTime)o_Value).ToString("yyyy.MM.dd HH:mm:ss");
			}
			if (o_Value is byte[])
			{
				StringBuilder stringBuilder = new StringBuilder(50000);
				byte[] array = (byte[])o_Value;
				foreach (byte b in array)
				{
					stringBuilder.Append(b.ToString("X2"));
					if (stringBuilder.Length % 100 == 0)
					{
						stringBuilder.Append("\r\n");
					}
				}
				return stringBuilder.ToString();
			}
			return o_Value.ToString().TrimEnd();
		}

		public static object DbValueDeserialize(string s_Value, Type t_Type)
		{
			try
			{
				if (s_Value == "@Null@")
				{
					return DBNull.Value;
				}
				if (t_Type == typeof(string))
				{
					return s_Value;
				}
				if (t_Type == typeof(int))
				{
					return int.Parse(s_Value);
				}
				if (t_Type == typeof(bool))
				{
					return bool.Parse(s_Value);
				}
				if (t_Type == typeof(long))
				{
					return long.Parse(s_Value);
				}
				if (t_Type == typeof(double))
				{
					return StrToDouble(s_Value);
				}
				if (t_Type == typeof(decimal))
				{
					return decimal.Parse(s_Value);
				}
				if (t_Type == typeof(DateTime))
				{
					if (s_Value.Length != 19 || s_Value[4] != '.' || s_Value[7] != '.' || s_Value[10] != ' ' || s_Value[13] != ':' || s_Value[16] != ':')
					{
						return null;
					}
					int year = int.Parse(s_Value.Substring(0, 4));
					int month = int.Parse(s_Value.Substring(5, 2));
					int day = int.Parse(s_Value.Substring(8, 2));
					int hour = int.Parse(s_Value.Substring(11, 2));
					int minute = int.Parse(s_Value.Substring(14, 2));
					int second = int.Parse(s_Value.Substring(17, 2));
					return new DateTime(year, month, day, hour, minute, second);
				}
				if (t_Type == typeof(byte[]))
				{
					s_Value = s_Value.Replace("\r\n", "");
					if (s_Value.Length % 2 != 0)
					{
						return null;
					}
					byte[] array = new byte[s_Value.Length / 2];
					int num = 0;
					for (int i = 0; i < array.Length; i++)
					{
						int num2 = 0;
						for (int j = 0; j < 2; j++)
						{
							num2 *= 16;
							switch (s_Value[num++])
							{
							case '0':
								num2 = num2;
								break;
							case '1':
								num2++;
								break;
							case '2':
								num2 += 2;
								break;
							case '3':
								num2 += 3;
								break;
							case '4':
								num2 += 4;
								break;
							case '5':
								num2 += 5;
								break;
							case '6':
								num2 += 6;
								break;
							case '7':
								num2 += 7;
								break;
							case '8':
								num2 += 8;
								break;
							case '9':
								num2 += 9;
								break;
							case 'A':
								num2 += 10;
								break;
							case 'B':
								num2 += 11;
								break;
							case 'C':
								num2 += 12;
								break;
							case 'D':
								num2 += 13;
								break;
							case 'E':
								num2 += 14;
								break;
							case 'F':
								num2 += 15;
								break;
							default:
								return null;
							}
						}
						array[i] = (byte)num2;
					}
					return array;
				}
			}
			catch
			{
			}
			return null;
		}

		public static double StrToDouble(string s_In)
		{
			double num = 0.0;
			double num2 = 1.0;
			bool flag = false;
			foreach (char c in s_In)
			{
				switch (c)
				{
				case ',':
				case '.':
					flag = true;
					continue;
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					num = 10.0 * num + (double)(c - 48);
					if (flag)
					{
						num2 *= 10.0;
					}
					continue;
				}
				break;
			}
			return num / num2;
		}

		public static void RemoveWriteProtection(string s_File)
		{
			try
			{
				File.SetAttributes(s_File, FileAttributes.Archive);
			}
			catch
			{
			}
		}

		public static string CutBeginAt(string s_In, string s_Delimiter)
		{
			if (s_In == null)
			{
				return "";
			}
			int num = IndexOf(s_In, s_Delimiter, 0);
			if (num >= 0)
			{
				return s_In.Substring(num + s_Delimiter.Length);
			}
			return s_In;
		}

		public static string CutEndReverseAt(string s_In, string s_Delimiter)
		{
			if (s_In == null)
			{
				return "";
			}
			int num = LastIndexOf(s_In, s_Delimiter, s_In.Length - 1);
			if (num >= 0)
			{
				return s_In.Substring(0, num);
			}
			return s_In;
		}

		public static int LastIndexOf(string s_Source, string s_Value, int s32_StartIndex)
		{
			if (s_Source == null || s_Value == null || s_Source.Length == 0 || s_Value.Length == 0)
			{
				return -1;
			}
			if (s32_StartIndex < 0 || s32_StartIndex >= s_Source.Length)
			{
				return -1;
			}
			return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(s_Source, s_Value, s32_StartIndex, CompareOptions.IgnoreCase);
		}

		public static int IndexOf(string s_Source, string s_Value, int s32_StartIndex)
		{
			if (s_Source == null || s_Value == null || s_Source.Length == 0 || s_Value.Length == 0)
			{
				return -1;
			}
			if (s32_StartIndex < 0 || s32_StartIndex >= s_Source.Length)
			{
				return -1;
			}
			return CultureInfo.InvariantCulture.CompareInfo.IndexOf(s_Source, s_Value, s32_StartIndex, CompareOptions.IgnoreCase);
		}

		public static int Find(string s_Source, string s_Value, int s32_StartIndex, bool b_Reverse, bool b_WholeWord)
		{
			int num;
			char c;
			char c2;
			do
			{
				num = ((!b_Reverse) ? IndexOf(s_Source, s_Value, s32_StartIndex) : LastIndexOf(s_Source, s_Value, s32_StartIndex));
				if (!b_WholeWord || num < 0)
				{
					break;
				}
				int num2 = num + s_Value.Length;
				int num3 = num - 1;
				s32_StartIndex = ((!b_Reverse) ? num2 : num3);
				c = '\0';
				c2 = '\0';
				if (num3 >= 0)
				{
					c = s_Source[num3];
				}
				if (num2 < s_Source.Length)
				{
					c2 = s_Source[num2];
				}
			}
			while (char.IsLetterOrDigit(c) || c == '_' || char.IsLetterOrDigit(c2) || c2 == '_');
			return num;
		}

		public static string Replace(string s_String, int s32_Start, int s32_Length, string s_NewValue)
		{
			if (s_String == null)
			{
				return "";
			}
			s_String = s_String.Remove(s32_Start, s32_Length);
			s_String = s_String.Insert(s32_Start, s_NewValue);
			return s_String;
		}

		public static string[] SplitEx(string s_In, string s_Delim)
		{
			if (s_In == null || s_In.Trim() == "")
			{
				return new string[0];
			}
			ArrayList arrayList = new ArrayList();
			int s32_StartIndex = 0;
			while (true)
			{
				s32_StartIndex = IndexOf(s_In, s_Delim, s32_StartIndex);
				if (s32_StartIndex < 0)
				{
					break;
				}
				arrayList.Add(s32_StartIndex);
				s32_StartIndex += s_Delim.Length;
			}
			arrayList.Add(s_In.Length);
			string[] array = new string[arrayList.Count];
			int num = 0;
			for (int i = 0; i < arrayList.Count; i++)
			{
				int num2 = (int)arrayList[i];
				array[i] = s_In.Substring(num, num2 - num);
				num = num2 + s_Delim.Length;
			}
			return array;
		}

		public static string[] SplitEx(string s_In, char u16_Delim)
		{
			if (s_In == null || s_In.Trim() == "")
			{
				return new string[0];
			}
			return s_In.Split(u16_Delim);
		}

		public static string Right(string s_In, int s32_Count)
		{
			if (s_In == null)
			{
				return "";
			}
			if (s32_Count <= 0)
			{
				return "";
			}
			if (s32_Count >= s_In.Length)
			{
				return s_In;
			}
			return s_In.Substring(s_In.Length - s32_Count);
		}

		public static string Left(string s_In, int s32_Count)
		{
			if (s_In == null)
			{
				return "";
			}
			if (s32_Count <= 0)
			{
				return "";
			}
			if (s32_Count >= s_In.Length)
			{
				return s_In;
			}
			return s_In.Substring(0, s32_Count);
		}

		public static string ShortenText(string s_Text, int s32_MaxLen)
		{
			if (s_Text == null)
			{
				return "";
			}
			if (s32_MaxLen == 0 || s_Text.Length <= s32_MaxLen)
			{
				return s_Text;
			}
			return s_Text.Substring(0, s32_MaxLen) + "...";
		}

		public static string ReplaceCRLF(string s_In)
		{
			return s_In.Replace("\r", "").Replace("\n", "\r\n");
		}

		public static string ReplaceHtml(string s_Text)
		{
			if (s_Text == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder(s_Text.Length * 5);
			foreach (char c in s_Text)
			{
				switch (c)
				{
				case '&':
					stringBuilder.Append("&amp;");
					continue;
				case '<':
					stringBuilder.Append("&lt;");
					continue;
				case '>':
					stringBuilder.Append("&gt;");
					continue;
				case '"':
					stringBuilder.Append("&quot;");
					continue;
				case '\n':
					stringBuilder.Append("<br>\r\n");
					continue;
				case ',':
					stringBuilder.Append(",<wbr>");
					continue;
				case '\r':
					continue;
				}
				if (c < '\u0080')
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.AppendFormat("&#{0};", (uint)c);
				}
			}
			return stringBuilder.Replace("  ", " &nbsp;").ToString();
		}

		public static string ReplaceRtf(string s_Text)
		{
			if (s_Text == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder(s_Text.Length * 8);
			foreach (char c in s_Text)
			{
				switch (c)
				{
				case '\\':
					stringBuilder.Append("\\\\");
					continue;
				case '{':
					stringBuilder.Append("\\{");
					continue;
				case '}':
					stringBuilder.Append("\\}");
					continue;
				case '\r':
					continue;
				}
				if (c < '\u0080')
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.AppendFormat("\\u{0}?", (uint)c);
				}
			}
			return stringBuilder.ToString();
		}

		public static string GetHtmlColor(Color c_Col)
		{
			return "#" + (c_Col.ToArgb() & 0xFFFFFF).ToString("X6");
		}

		public static string FirstToUpper(string s_In, bool b_IsFile)
		{
			int num = b_IsFile ? s_In.LastIndexOf('.') : (-1);
			if (s_In == null)
			{
				return "";
			}
			string text = s_In.ToLower();
			string text2 = s_In.ToUpper();
			string text3 = "";
			bool flag = true;
			for (int i = 0; i < s_In.Length; i++)
			{
				char c = text[i];
				char c2 = text2[i];
				if (c == c2)
				{
					flag = (i != num);
					text3 += c.ToString();
				}
				else
				{
					text3 = ((!flag) ? (text3 + c.ToString()) : (text3 + c2.ToString()));
					flag = false;
				}
			}
			return text3;
		}

		public static void LimitOnScreen(ref int Left, ref int Top, ref int Width, ref int Height)
		{
			Rectangle workingArea = Screen.FromRectangle(new Rectangle(Left, Top, Width, Height)).WorkingArea;
			Width = Math.Max(0, Math.Min(workingArea.Width, Width));
			Height = Math.Max(0, Math.Min(workingArea.Height, Height));
			Left = Math.Max(workingArea.X, Math.Min(workingArea.X + workingArea.Width - Width, Left));
			Top = Math.Max(workingArea.Y, Math.Min(workingArea.Y + workingArea.Height - Height, Top));
		}

		public static Point CenterToRectangle(Rectangle k_Bounds, Size k_Size)
		{
			int x = k_Bounds.Left + (k_Bounds.Width - k_Size.Width) / 2;
			int y = k_Bounds.Top + (k_Bounds.Height - k_Size.Height) / 2;
			return new Point(x, y);
		}

		public static void CenterWindow(Form frm)
		{
			Rectangle k_Bounds = (frm.Owner == null) ? Screen.FromControl(frm).WorkingArea : new Rectangle(frm.Owner.Location, frm.Owner.Size);
			frm.Location = CenterToRectangle(k_Bounds, frm.Size);
		}

		public static string FormatSize(long s64_Size)
		{
			if (s64_Size < 1024)
			{
				return s64_Size.ToString() + " Byte";
			}
			s64_Size *= 10;
			s64_Size /= 1024;
			if (s64_Size < 10240)
			{
				return $"{s64_Size / 10}.{s64_Size % 10} KB";
			}
			s64_Size /= 1024;
			if (s64_Size < 10240)
			{
				return $"{s64_Size / 10}.{s64_Size % 10} MB";
			}
			s64_Size /= 1024;
			return $"{s64_Size / 10}.{s64_Size % 10} GB";
		}

		public static string ExplainApiError(int s32_Error)
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				FormatMessageW(4096, 0, s32_Error, 0, stringBuilder, stringBuilder.Capacity, 0);
			}
			else
			{
				FormatMessageA(4096, 0, s32_Error, 0, stringBuilder, stringBuilder.Capacity, 0);
			}
			if (stringBuilder.Length == 0)
			{
				stringBuilder.Append("Windows has no explanation for this error code.");
			}
			return $"Error {s32_Error}: {stringBuilder}";
		}

		public static string CalcMD5(string s_Text)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.Unicode.GetBytes(s_Text);
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("X2");
			}
			return text;
		}

		public static string Crypt(string s_Data, string s_Password, bool b_Encrypt)
		{
			byte[] rgbSalt = new byte[13]
			{
				38,
				25,
				129,
				78,
				160,
				109,
				149,
				52,
				38,
				117,
				100,
				5,
				246
			};
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(s_Password, rgbSalt);
			Rijndael rijndael = Rijndael.Create();
			rijndael.Key = passwordDeriveBytes.GetBytes(32);
			rijndael.IV = passwordDeriveBytes.GetBytes(16);
			ICryptoTransform transform = b_Encrypt ? rijndael.CreateEncryptor() : rijndael.CreateDecryptor();
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			try
			{
				byte[] array = (!b_Encrypt) ? Convert.FromBase64String(s_Data) : Encoding.Unicode.GetBytes(s_Data);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.Close();
				if (b_Encrypt)
				{
					return Convert.ToBase64String(memoryStream.ToArray());
				}
				return Encoding.Unicode.GetString(memoryStream.ToArray());
			}
			catch
			{
				return "";
			}
		}

		public static Icon ReadEmbeddedIconResource(string s_IconName)
		{
			return new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("SqlBuilder.Resources." + s_IconName));
		}

		public static string ReadStringResource(string s_ResourceName)
		{
			return new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("SqlBuilder.Resources." + s_ResourceName)).ReadToEnd();
		}

		public static int GetCurrentThread()
		{
			return GetCurrentThreadId();
		}

		public static int GetWindowThread(IntPtr h_Wnd)
		{
			int ProcID;
			return GetWindowThreadProcessId(h_Wnd, out ProcID);
		}

		public static string GetRandomString(int s32_Len, int s32_Seed, string s_Chars)
		{
			Random random = new Random(s32_Seed);
			StringBuilder stringBuilder = new StringBuilder(s32_Len);
			for (int i = 0; i < s32_Len; i++)
			{
				int index = random.Next(s_Chars.Length);
				stringBuilder.Append(s_Chars[index]);
			}
			return stringBuilder.ToString();
		}

		public static int GetStringChecksum(string s_String)
		{
			int num = 0;
			foreach (char c in s_String)
			{
				num += c;
			}
			return num;
		}

		public static Color ChangeBrightness(Color c_Color, int s32_Percent)
		{
			if (s32_Percent < -100 || s32_Percent > 100)
			{
				throw new Exception("Invalid parameter");
			}
			double num = (double)(100 - Math.Abs(s32_Percent)) / 100.0;
			if (s32_Percent > 0)
			{
				int num2 = (int)(num * (double)(255 - c_Color.R));
				int num3 = (int)(num * (double)(255 - c_Color.G));
				int num4 = (int)(num * (double)(255 - c_Color.B));
				return Color.FromArgb(255 - num2, 255 - num3, 255 - num4);
			}
			int red = (int)(num * (double)(int)c_Color.R);
			int green = (int)(num * (double)(int)c_Color.G);
			int blue = (int)(num * (double)(int)c_Color.B);
			return Color.FromArgb(red, green, blue);
		}

		public static bool IsStringAscii(string s_Text)
		{
			for (int i = 0; i < s_Text.Length; i++)
			{
				if (s_Text[i] > '\u007f')
				{
					return false;
				}
			}
			return true;
		}
	}
}
