using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Micromind.UISupport.Common
{
	public class ResourceHelper
	{
		public static Cursor LoadCursor(Type assemblyType, string cursorName)
		{
			return new Cursor(Assembly.GetAssembly(assemblyType).GetManifestResourceStream(cursorName));
		}

		public static Icon LoadIcon(Type assemblyType, string iconName)
		{
			return new Icon(Assembly.GetAssembly(assemblyType).GetManifestResourceStream(iconName));
		}

		public static Icon LoadIcon(Type assemblyType, string iconName, Size iconSize)
		{
			return new Icon(LoadIcon(assemblyType, iconName), iconSize);
		}

		public static Bitmap LoadBitmap(Type assemblyType, string imageName)
		{
			return LoadBitmap(assemblyType, imageName, makeTransparent: false, new Point(0, 0));
		}

		public static Bitmap LoadBitmap(Type assemblyType, string imageName, Point transparentPixel)
		{
			return LoadBitmap(assemblyType, imageName, makeTransparent: true, transparentPixel);
		}

		public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize)
		{
			return LoadBitmapStrip(assemblyType, imageName, imageSize, makeTransparent: false, new Point(0, 0));
		}

		public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize, Point transparentPixel)
		{
			return LoadBitmapStrip(assemblyType, imageName, imageSize, makeTransparent: true, transparentPixel);
		}

		protected static Bitmap LoadBitmap(Type assemblyType, string imageName, bool makeTransparent, Point transparentPixel)
		{
			Bitmap bitmap = new Bitmap(Assembly.GetAssembly(assemblyType).GetManifestResourceStream(imageName));
			if (makeTransparent)
			{
				Color pixel = bitmap.GetPixel(transparentPixel.X, transparentPixel.Y);
				bitmap.MakeTransparent(pixel);
			}
			return bitmap;
		}

		protected static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize, bool makeTransparent, Point transparentPixel)
		{
			ImageList imageList = new ImageList();
			imageList.ImageSize = imageSize;
			Stream manifestResourceStream = Assembly.GetAssembly(assemblyType).GetManifestResourceStream(imageName);
			if (manifestResourceStream != null)
			{
				Bitmap bitmap = new Bitmap(manifestResourceStream);
				if (makeTransparent)
				{
					Color pixel = bitmap.GetPixel(transparentPixel.X, transparentPixel.Y);
					bitmap.MakeTransparent(pixel);
				}
				imageList.Images.AddStrip(bitmap);
			}
			return imageList;
		}
	}
}
