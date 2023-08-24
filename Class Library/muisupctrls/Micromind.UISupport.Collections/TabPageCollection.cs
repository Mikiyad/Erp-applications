using Micromind.UISupport.Controls;

namespace Micromind.UISupport.Collections
{
	public class TabPageCollection : CollectionWithEvents
	{
		public TabPage this[int index] => base.List[index] as TabPage;

		public TabPage this[string title]
		{
			get
			{
				foreach (TabPage item in base.List)
				{
					if (item.Title == title)
					{
						return item;
					}
				}
				return null;
			}
		}

		public TabPage Add(TabPage value)
		{
			base.List.Add(value);
			return value;
		}

		public void AddRange(TabPage[] values)
		{
			foreach (TabPage value in values)
			{
				Add(value);
			}
		}

		public void Remove(TabPage value)
		{
			base.List.Remove(value);
		}

		public void Insert(int index, TabPage value)
		{
			base.List.Insert(index, value);
		}

		public bool Contains(TabPage value)
		{
			return base.List.Contains(value);
		}

		public int IndexOf(TabPage value)
		{
			return base.List.IndexOf(value);
		}
	}
}
