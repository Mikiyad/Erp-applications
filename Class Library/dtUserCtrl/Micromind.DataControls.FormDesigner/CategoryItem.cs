namespace Micromind.DataControls.FormDesigner
{
	internal class CategoryItem : ToolboxItem
	{
		public CategoryItem(string text, bool expanded)
			: base(text, expanded ? 1 : 0, groupControl: true)
		{
		}
	}
}
