using System;
using System.Collections;
using System.Drawing;

namespace Micromind.DataControls.QueryBuilder
{
	public class Defaults
	{
		public const string Version = "5.4";

		public static Type DebugType = null;

		public const bool TrustedConnection = false;

		public const string Server = "Sql Server";

		public const string User = "sa";

		public const string Password = "sa";

		public const string DbaseList = "GPPROD";

		public const int Timeout = 120;

		public const string TimeFormat = "dd.MM.yyyy HH:mm";

		public const string DateFormat = "dd.MM.yyyy";

		public const string NullText = "(NULL)";

		public const string IdentText = "(IDENT)";

		public const string XmlNullText = "@Null@";

		public const int HtmlHeaderRepeat = 25;

		public const string HtmlHeaderColor = "#E0D0C0";

		public const string CreateFunc = "CREATE FUNCTION {0} (@Param as int)\r\nRETURNS int\r\nAS\r\nBEGIN\r\nRETURN 0\r\nEND\r\n";

		public const string CreateProc = "CREATE PROCEDURE {0} AS\r\nSELECT * from Table\r\n";

		public const string CreateView = "CREATE VIEW {0}\r\nAS\r\nSELECT * from Table\r\n";

		public const string CreateTrig = "CREATE TRIGGER {0} ON Table\r\nFOR UPDATE,INSERT\r\nAS\r\n";

		public const string CreateTabl = "IF object_id('{0}') IS NULL\r\nCREATE TABLE {0}\r\n(\r\nID int IDENTITY(1,1) NOT NULL,\r\nName varchar(30) PRIMARY KEY NOT NULL\r\n)\r\nGO\r\n";

		public const string DelSysObj = "DeletedSysObjects\\";

		public const string ScriptDir = "Scripts\\";

		public const string BackupDir = "DatabaseBackup\\";

		public const string XmlSettings = "SqlBuilder.xml";

		public const string CompoundScript = "CompoundScript.sql";

		public const string XmlBackup = "Backup.xml";

		public const string UpdateUrl = "http://netcult.ch/elmue/UpdateSqlBuilder.txt";

		public const string DownloadUrl = "http://electronix.ch/ptbsync/SqlBuilder.zip";

		public const int UpdateInterval = 1;

		public const string EncryptionKey = "1Gsdu7s$B6(d3#H@j7kSqPlke6&?0!:M§A.-U*'Y=Fua";

		public static readonly Color BkgColControl = Color.White;

		public const string UserDataTypes = " nBinario nBoolean nDate nDecimal nFloat nGUID nLong nPrecio nStringBig nStringLong nStringMax nStringMid nStringShort nTexto ";

		public const int IndentPlain = 4;

		public const int IndentHtml = 20;

		public const int IndentRtf = 300;

		public static Color GridBackColor(bool b_Primary, bool b_Unique, bool b_Disabled)
		{
			if (b_Disabled)
			{
				return Color.FromArgb(240, 240, 240);
			}
			if (b_Primary && b_Unique)
			{
				return Color.FromArgb(255, 234, 255);
			}
			if (b_Primary)
			{
				return Color.FromArgb(255, 234, 234);
			}
			if (b_Unique)
			{
				return Color.FromArgb(255, 255, 229);
			}
			return BkgColControl;
		}

		public static Color GridSelectedBackColor(bool b_Disabled)
		{
			if (b_Disabled)
			{
				return Color.FromArgb(221, 221, 221);
			}
			return Color.FromArgb(187, 187, 255);
		}

		public static Color GridForeColor(Type t_DataType)
		{
			if (t_DataType == typeof(DBNull))
			{
				return Color.FromArgb(170, 170, 170);
			}
			if (t_DataType == typeof(byte) || t_DataType == typeof(short) || t_DataType == typeof(int) || t_DataType == typeof(long) || t_DataType == typeof(double) || t_DataType == typeof(decimal))
			{
				return Color.DarkGreen;
			}
			if (t_DataType == typeof(DateTime))
			{
				return Color.DarkBlue;
			}
			if (t_DataType == typeof(string))
			{
				return Color.DarkRed;
			}
			if (t_DataType == typeof(Exception))
			{
				return Color.Red;
			}
			return Color.Black;
		}

		public static Color ListViewColor(string s_FileExt)
		{
			if (!(s_FileExt == ".sql"))
			{
				if (!(s_FileExt == ".tabl"))
				{
					if (!(s_FileExt == ".proc"))
					{
						if (!(s_FileExt == ".func"))
						{
							if (!(s_FileExt == ".view"))
							{
								if (s_FileExt == ".trig")
								{
									return Color.FromArgb(0, 96, 0);
								}
								return Color.Black;
							}
							return Color.FromArgb(128, 128, 0);
						}
						return Color.FromArgb(64, 112, 96);
					}
					return Color.FromArgb(0, 160, 0);
				}
				return Color.FromArgb(96, 48, 160);
			}
			return Color.FromArgb(0, 0, 204);
		}

		public static Hashtable DefineParserColors()
		{
			return new Hashtable
			{
				{
					Parser.eType.Comand,
					Color.Blue
				},
				{
					Parser.eType.Keyword,
					Color.BlueViolet
				},
				{
					Parser.eType.Function,
					Color.Red
				},
				{
					Parser.eType.Operator,
					Color.Red
				},
				{
					Parser.eType.DataType,
					Color.DodgerBlue
				},
				{
					Parser.eType.String,
					Color.DarkRed
				},
				{
					Parser.eType.CommentL,
					Color.DarkCyan
				},
				{
					Parser.eType.CommentP,
					Color.DarkCyan
				},
				{
					Parser.eType.Number,
					Color.DarkGreen
				}
			};
		}

		public static bool CheckLineComment(string s_Text)
		{
			string text = s_Text.Substring(2).ToUpper().Trim();
			while (text.StartsWith("*") || text.StartsWith("-"))
			{
				text = text.Substring(1);
			}
			text = text.Trim();
			if (text.StartsWith("BY "))
			{
				text = text.Substring(3).Trim();
			}
			string[] array = text.Split(' ');
			if ((array.Length == 2 || (array.Length == 3 && array[2].Length < 5)) && array[0].Length < 13 && (array[1].Length == 8 || array[1].Length == 10) && ((array[1][2] == '_' && array[1][5] == '_') || (array[1][2] == '/' && array[1][5] == '/') || (array[1][2] == '.' && array[1][5] == '.') || (array[1][2] == '-' && array[1][5] == '-')))
			{
				return false;
			}
			if (text.StartsWith("INICIO ") || text.StartsWith("FIN ") || text.StartsWith("END ") || text.EndsWith(" (COMENTADO)") || text.EndsWith(" INICIO") || text.EndsWith(" FIN"))
			{
				return false;
			}
			return true;
		}
	}
}
