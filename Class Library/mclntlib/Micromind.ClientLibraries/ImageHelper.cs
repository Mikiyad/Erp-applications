using System.Drawing;

namespace Micromind.ClientLibraries
{
	public class ImageHelper
	{
		public static Bitmap CombineBitmaps(Bitmap[] bitmaps)
		{
			if (bitmaps.Length == 0)
			{
				return new Bitmap(1, 1);
			}
			int num = 0;
			int num2 = 0;
			Bitmap[] array = bitmaps;
			checked
			{
				foreach (Bitmap bitmap in array)
				{
					num += bitmap.Width;
					if (num2 < bitmap.Height)
					{
						num2 = bitmap.Height;
					}
				}
				num += bitmaps.Length - 1;
				Bitmap bitmap2 = new Bitmap(num, num2);
				using (Graphics graphics = Graphics.FromImage(bitmap2))
				{
					int num3 = 0;
					int num4 = 0;
					array = bitmaps;
					foreach (Bitmap bitmap3 in array)
					{
						num4++;
						graphics.DrawImage(bitmap3, num3, 0, bitmap3.Width, num2);
						num3 += bitmap3.Width;
						num3++;
					}
					return bitmap2;
				}
			}
		}

		public static Bitmap CreateImage(Color color, int width, int height)
		{
			Bitmap bitmap = new Bitmap(12, 18);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				using (SolidBrush brush = new SolidBrush(color))
				{
					graphics.FillRectangle(brush, 0, 0, width, height);
					return bitmap;
				}
			}
		}
	}
}
