namespace Micromind.DataControls.FormDesigner
{
	internal struct ItemDragEventArgs
	{
		public ToolboxItem item;

		public ItemDragEventArgs(ToolboxItem item)
		{
			this.item = item;
		}
	}
}
