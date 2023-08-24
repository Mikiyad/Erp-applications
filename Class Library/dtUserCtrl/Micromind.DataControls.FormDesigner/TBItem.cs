using System.Drawing.Design;

namespace Micromind.DataControls.FormDesigner
{
	internal class TBItem : ToolboxItem
	{
		public TBItem(string text, int image, System.Drawing.Design.ToolboxItem item)
			: base(text, image, groupControl: false)
		{
			tag = item;
		}
	}
}
