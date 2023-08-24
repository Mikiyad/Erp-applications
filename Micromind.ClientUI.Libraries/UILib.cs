using Infragistics.Win;
using Micromind.ClientLibraries;
using Micromind.DataCaches.Libraries;
using Micromind.UISupport;
using System;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	public class UILib
	{
		public static bool isLoadingSummaryDetails;

		public const byte SubItemIndent = 5;

		public static int NumberOfUse
		{
			get
			{
				try
				{
					object value = Application.UserAppDataRegistry.GetValue("NumberOfUse");
					if (value.IsNullOrEmpty())
					{
						return 0;
					}
					return int.Parse(value.ToString());
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				Application.UserAppDataRegistry.SetValue("NumberOfUse", value);
			}
		}

		public static bool IsMatch(string source, string pattern)
		{
			return new Regex(pattern).IsMatch(source);
		}

		public static string GetIDNumber()
		{
			string text = "";
			string text2 = "";
			Random random = new Random((int)(~DateTime.Now.Ticks));
			char[] array = Micromind.ClientLibraries.Global.CompanyName.GetHashCode().ToString().ToCharArray();
			checked
			{
				for (int num = array.Length - 1; num >= 0; num--)
				{
					if (array[num] != '0')
					{
						text += array[num].ToString();
					}
					else if (num != array.Length - 1)
					{
						text += array[num].ToString();
					}
					if (text.Length == 3)
					{
						break;
					}
				}
				if (text.Length < 2)
				{
					text = random.Next(100, 999).ToString();
				}
				else if (text.Length > 3)
				{
					text = text.Substring(0, 3);
				}
			}
			random = new Random((int)(~DateTime.Now.Ticks));
			text2 = random.Next(100, 999).ToString();
			return text + "-" + text2;
		}

		public static void AddPanelLine(Panel panel)
		{
			Line line = new Line();
			line.Name = "rightLine";
			line.IsVertical = true;
			line.LineBackColor = Color.FromArgb(188, 218, 247);
			line.DrawWidth = 1;
			line.Height = panel.Height;
			panel.Controls.Add(line);
			line.Left = checked(panel.Width - 1);
			line.Top = 0;
			line.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			line.Visible = true;
		}

		public static void LogoutCurrentCompany()
		{
			if (Micromind.ClientLibraries.Global.ConStatus != ConnectionStatus.DisConnected)
			{
				Factory.DatabaseSystem.ChangeDatabaseName("master");
				Micromind.ClientLibraries.Global.CurrentDatabaseName = "master";
				Micromind.DataCaches.Libraries.Global.ResetAllCaches();
				FormActivator.ResetAllForms();
				Micromind.ClientLibraries.Global.IsUserAdmin = false;
			}
		}

		public static string GetComputerName()
		{
			try
			{
				return Environment.MachineName;
			}
			catch
			{
				return "";
			}
		}

		public static string GetIP()
		{
			IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
			return addressList[checked(addressList.Length - 1)].ToString();
		}

		public static bool IsTrialRegistered()
		{
			try
			{
				return bool.Parse(Application.UserAppDataRegistry.GetValue("IsTrialRegistered").ToString());
			}
			catch
			{
				return false;
			}
		}

		public static bool IsConnectedToInternet()
		{
			try
			{
				Dns.GetHostEntry("www.microsoft.com");
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static ValueList GetSysDocTypesValueList()
		{
			return new ValueList
			{
				ValueListItems = 
				{
					{
						(object)1,
						PublicFunctions.GetSysDocTypeString(1)
					},
					{
						(object)2,
						PublicFunctions.GetSysDocTypeString(2)
					},
					{
						(object)3,
						PublicFunctions.GetSysDocTypeString(3)
					},
					{
						(object)4,
						PublicFunctions.GetSysDocTypeString(4)
					},
					{
						(object)5,
						PublicFunctions.GetSysDocTypeString(5)
					},
					{
						(object)6,
						PublicFunctions.GetSysDocTypeString(6)
					},
					{
						(object)7,
						PublicFunctions.GetSysDocTypeString(7)
					},
					{
						(object)8,
						PublicFunctions.GetSysDocTypeString(8)
					},
					{
						(object)9,
						PublicFunctions.GetSysDocTypeString(9)
					},
					{
						(object)10,
						PublicFunctions.GetSysDocTypeString(10)
					},
					{
						(object)11,
						PublicFunctions.GetSysDocTypeString(11)
					},
					{
						(object)12,
						PublicFunctions.GetSysDocTypeString(12)
					},
					{
						(object)13,
						PublicFunctions.GetSysDocTypeString(13)
					},
					{
						(object)14,
						PublicFunctions.GetSysDocTypeString(14)
					},
					{
						(object)15,
						PublicFunctions.GetSysDocTypeString(15)
					},
					{
						(object)16,
						PublicFunctions.GetSysDocTypeString(16)
					},
					{
						(object)17,
						PublicFunctions.GetSysDocTypeString(17)
					},
					{
						(object)18,
						PublicFunctions.GetSysDocTypeString(18)
					},
					{
						(object)19,
						PublicFunctions.GetSysDocTypeString(19)
					},
					{
						(object)20,
						PublicFunctions.GetSysDocTypeString(20)
					},
					{
						(object)21,
						PublicFunctions.GetSysDocTypeString(21)
					},
					{
						(object)24,
						PublicFunctions.GetSysDocTypeString(24)
					},
					{
						(object)25,
						PublicFunctions.GetSysDocTypeString(25)
					},
					{
						(object)26,
						PublicFunctions.GetSysDocTypeString(26)
					},
					{
						(object)27,
						PublicFunctions.GetSysDocTypeString(27)
					},
					{
						(object)28,
						PublicFunctions.GetSysDocTypeString(28)
					},
					{
						(object)29,
						PublicFunctions.GetSysDocTypeString(29)
					},
					{
						(object)32,
						PublicFunctions.GetSysDocTypeString(32)
					},
					{
						(object)33,
						PublicFunctions.GetSysDocTypeString(33)
					},
					{
						(object)34,
						PublicFunctions.GetSysDocTypeString(34)
					},
					{
						(object)35,
						PublicFunctions.GetSysDocTypeString(35)
					},
					{
						(object)37,
						PublicFunctions.GetSysDocTypeString(37)
					},
					{
						(object)39,
						PublicFunctions.GetSysDocTypeString(39)
					},
					{
						(object)40,
						PublicFunctions.GetSysDocTypeString(40)
					},
					{
						(object)43,
						PublicFunctions.GetSysDocTypeString(43)
					},
					{
						(object)44,
						PublicFunctions.GetSysDocTypeString(44)
					},
					{
						(object)45,
						PublicFunctions.GetSysDocTypeString(45)
					},
					{
						(object)46,
						PublicFunctions.GetSysDocTypeString(46)
					},
					{
						(object)47,
						PublicFunctions.GetSysDocTypeString(47)
					},
					{
						(object)48,
						PublicFunctions.GetSysDocTypeString(48)
					},
					{
						(object)50,
						PublicFunctions.GetSysDocTypeString(50)
					},
					{
						(object)51,
						PublicFunctions.GetSysDocTypeString(51)
					},
					{
						(object)53,
						PublicFunctions.GetSysDocTypeString(53)
					},
					{
						(object)54,
						PublicFunctions.GetSysDocTypeString(54)
					},
					{
						(object)55,
						PublicFunctions.GetSysDocTypeString(55)
					},
					{
						(object)56,
						PublicFunctions.GetSysDocTypeString(56)
					},
					{
						(object)57,
						PublicFunctions.GetSysDocTypeString(57)
					},
					{
						(object)62,
						PublicFunctions.GetSysDocTypeString(62)
					},
					{
						(object)64,
						PublicFunctions.GetSysDocTypeString(64)
					},
					{
						(object)65,
						PublicFunctions.GetSysDocTypeString(65)
					},
					{
						(object)66,
						PublicFunctions.GetSysDocTypeString(66)
					},
					{
						(object)67,
						PublicFunctions.GetSysDocTypeString(67)
					},
					{
						(object)68,
						PublicFunctions.GetSysDocTypeString(68)
					},
					{
						(object)71,
						PublicFunctions.GetSysDocTypeString(71)
					},
					{
						(object)72,
						PublicFunctions.GetSysDocTypeString(72)
					},
					{
						(object)73,
						PublicFunctions.GetSysDocTypeString(73)
					},
					{
						(object)74,
						PublicFunctions.GetSysDocTypeString(74)
					},
					{
						(object)76,
						PublicFunctions.GetSysDocTypeString(76)
					},
					{
						(object)79,
						PublicFunctions.GetSysDocTypeString(79)
					},
					{
						(object)80,
						PublicFunctions.GetSysDocTypeString(80)
					},
					{
						(object)81,
						PublicFunctions.GetSysDocTypeString(81)
					},
					{
						(object)84,
						PublicFunctions.GetSysDocTypeString(84)
					},
					{
						(object)87,
						PublicFunctions.GetSysDocTypeString(87)
					},
					{
						(object)88,
						PublicFunctions.GetSysDocTypeString(88)
					},
					{
						(object)89,
						PublicFunctions.GetSysDocTypeString(89)
					},
					{
						(object)90,
						PublicFunctions.GetSysDocTypeString(90)
					},
					{
						(object)92,
						PublicFunctions.GetSysDocTypeString(92)
					},
					{
						(object)93,
						PublicFunctions.GetSysDocTypeString(93)
					},
					{
						(object)94,
						PublicFunctions.GetSysDocTypeString(94)
					},
					{
						(object)95,
						PublicFunctions.GetSysDocTypeString(95)
					},
					{
						(object)267,
						PublicFunctions.GetSysDocTypeString(267)
					},
					{
						(object)1000,
						PublicFunctions.GetSysDocTypeString(1000)
					},
					{
						(object)1001,
						PublicFunctions.GetSysDocTypeString(1001)
					},
					{
						(object)1002,
						PublicFunctions.GetSysDocTypeString(1002)
					},
					{
						(object)1003,
						PublicFunctions.GetSysDocTypeString(1003)
					},
					{
						(object)1004,
						PublicFunctions.GetSysDocTypeString(1004)
					}
				}
			};
		}
	}
}
