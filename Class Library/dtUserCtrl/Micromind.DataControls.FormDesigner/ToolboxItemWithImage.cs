using System.Drawing.Design;

namespace Micromind.DataControls.FormDesigner
{
	internal class ToolboxItemWithImage
	{
		public System.Drawing.Design.ToolboxItem item;

		public int image;

		public ToolboxItemWithImage(System.Drawing.Design.ToolboxItem item, int image)
		{
			this.item = item;
			this.image = image;
		}
	}
}
