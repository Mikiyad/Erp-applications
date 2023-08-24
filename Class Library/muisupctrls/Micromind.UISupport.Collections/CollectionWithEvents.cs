using System.Collections;

namespace Micromind.UISupport.Collections
{
	public class CollectionWithEvents : CollectionBase
	{
		public event CollectionClear Clearing;

		public event CollectionClear Cleared;

		public event CollectionChange Inserting;

		public event CollectionChange Inserted;

		public event CollectionChange Removing;

		public event CollectionChange Removed;

		protected override void OnClear()
		{
			if (this.Clearing != null)
			{
				this.Clearing();
			}
		}

		protected override void OnClearComplete()
		{
			if (this.Cleared != null)
			{
				this.Cleared();
			}
		}

		protected override void OnInsert(int index, object value)
		{
			if (this.Inserting != null)
			{
				this.Inserting(index, value);
			}
		}

		protected override void OnInsertComplete(int index, object value)
		{
			if (this.Inserted != null)
			{
				this.Inserted(index, value);
			}
		}

		protected override void OnRemove(int index, object value)
		{
			if (this.Removing != null)
			{
				this.Removing(index, value);
			}
		}

		protected override void OnRemoveComplete(int index, object value)
		{
			if (this.Removed != null)
			{
				this.Removed(index, value);
			}
		}

		protected int IndexOf(object value)
		{
			return base.List.IndexOf(value);
		}
	}
}
