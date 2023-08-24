using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Micromind.ClientLibraries
{
	public sealed class Win32API
	{
		public struct RECT
		{
			public int Left;

			public int Right;

			public int Bottom;

			public int Top;
		}

		public struct PRINTER_DEFAULTS
		{
			public string pDatatype;

			public long pDevMode;

			public long pDesiredAccess;
		}

		public const int WM_ACTIVATE = 6;

		public const int WA_CLICKACTIVE = 2;

		public const int SW_SHOWNORMAL = 1;

		public const int SW_SHOWMINIMIZED = 2;

		public const int SW_SHOWMAXIMIZED = 3;

		public const int WPF_RESTORETOMAXIMIZED = 2;

		public const uint SM_CYHSCROLL = 3u;

		public const uint SM_CXVSCROLL = 2u;

		[DllImport("kernel32.dll")]
		private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, uint VolumeNameSize, ref uint VolumeSerialNumber, ref uint MaximumComponentLength, ref uint FileSystemFlags, StringBuilder FileSystemNameBuffer, uint FileSystemNameSize);

		[DllImport("USER32.DLL", SetLastError = true)]
		public static extern uint ShowWindow(uint hwnd, int showCommand);

		[DllImport("USER32.DLL", SetLastError = true)]
		public static extern uint SetForegroundWindow(uint hwnd);

		[DllImport("USER32.DLL")]
		public static extern int GetWindowRect(IntPtr hwnd, ref RECT rect);

		[DllImport("USER32.DLL")]
		public static extern long GetSystemMetrics(uint nIndex);

		[DllImport("winspool.drv")]
		public static extern int PrinterProperties(IntPtr hwnd, int hPrinter);

		[DllImport("winspool.drv")]
		public static extern long OpenPrinter(string pPrinterName, long phPrinter, PRINTER_DEFAULTS pDefault);

		[DllImport("winspool.drv")]
		public static extern long ClosePrinter(long hPrinter);

		[DllImport("winmm.dll")]
		public static extern int PlaySound(string lpszName, int hModule, int dwFlags);

		public static string GetVolumeSerial(string strDriveLetter)
		{
			uint VolumeSerialNumber = 0u;
			uint MaximumComponentLength = 0u;
			StringBuilder stringBuilder = new StringBuilder(256);
			uint FileSystemFlags = 0u;
			StringBuilder stringBuilder2 = new StringBuilder(256);
			checked
			{
				GetVolumeInformation(strDriveLetter, stringBuilder, (uint)stringBuilder.Capacity, ref VolumeSerialNumber, ref MaximumComponentLength, ref FileSystemFlags, stringBuilder2, (uint)stringBuilder2.Capacity);
				return Convert.ToString(VolumeSerialNumber);
			}
		}

		public static Point GetWindowLocation(IntPtr handle)
		{
			RECT rect = default(RECT);
			GetWindowRect(handle, ref rect);
			Point result = default(Point);
			result.X = rect.Left;
			result.Y = rect.Top;
			return result;
		}

		public static uint GetHorizontalScrollBarHeight()
		{
			return checked((uint)GetSystemMetrics(3u));
		}

		public static uint GetVerticalScrollBarWidth()
		{
			return checked((uint)GetSystemMetrics(2u));
		}
	}
}
