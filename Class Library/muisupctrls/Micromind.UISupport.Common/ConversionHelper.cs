using System;
using System.Drawing;

namespace Micromind.UISupport.Common
{
	public class ConversionHelper
	{
		protected static SizeConverter _sc = new SizeConverter();

		protected static PointConverter _pc = new PointConverter();

		protected static Type _stringType = Type.GetType("System.String");

		public static string SizeToString(Size size)
		{
			return (string)_sc.ConvertTo(size, _stringType);
		}

		public static Size StringToSize(string str)
		{
			return (Size)_sc.ConvertFrom(str);
		}

		public static string PointToString(Point point)
		{
			return (string)_pc.ConvertTo(point, _stringType);
		}

		public static Point StringToPoint(string str)
		{
			return (Point)_pc.ConvertFrom(str);
		}
	}
}
