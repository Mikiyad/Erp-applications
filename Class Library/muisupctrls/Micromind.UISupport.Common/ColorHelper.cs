using System.Drawing;

namespace Micromind.UISupport.Common
{
	public class ColorHelper
	{
		public static Color TabBackgroundFromBaseColor(Color backColor)
		{
			if (backColor.R == 212 && backColor.G == 208 && backColor.B == 200)
			{
				return Color.FromArgb(247, 243, 233);
			}
			if (backColor.R == 236 && backColor.G == 233 && backColor.B == 216)
			{
				return Color.FromArgb(255, 251, 233);
			}
			int red = 255 - (255 - backColor.R) / 2;
			int green = 255 - (255 - backColor.G) / 2;
			int blue = 255 - (255 - backColor.B) / 2;
			return Color.FromArgb(red, green, blue);
		}
	}
}
