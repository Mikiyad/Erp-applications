using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Micromind.UISupport.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					resourceMan = new ResourceManager("Micromind.UISupport.Properties.Resources", typeof(Resources).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static Bitmap _001_03 => (Bitmap)ResourceManager.GetObject("001_03", resourceCulture);

		internal static Bitmap _001_04 => (Bitmap)ResourceManager.GetObject("001_04", resourceCulture);

		internal static Bitmap autofit => (Bitmap)ResourceManager.GetObject("autofit", resourceCulture);

		internal static Bitmap colbestsize => (Bitmap)ResourceManager.GetObject("colbestsize", resourceCulture);

		internal static Bitmap column => (Bitmap)ResourceManager.GetObject("column", resourceCulture);

		internal static Bitmap Copy => (Bitmap)ResourceManager.GetObject("Copy", resourceCulture);

		internal static Bitmap delete_2__1_ => (Bitmap)ResourceManager.GetObject("delete-2 (1)", resourceCulture);

		internal static Bitmap fullscreen => (Bitmap)ResourceManager.GetObject("fullscreen", resourceCulture);

		internal static Bitmap fullscreen2 => (Bitmap)ResourceManager.GetObject("fullscreen2", resourceCulture);

		internal Resources()
		{
		}
	}
}
