namespace Micromind.DataControls.FormDesigner
{
	internal class ToolboxItem
	{
		public string text;

		public int image;

		public bool groupControl;

		public object tag;

		internal ToolboxItem(string text, int image, bool groupControl)
		{
			this.text = text;
			this.image = image;
			this.groupControl = groupControl;
			tag = null;
		}
	}
}
