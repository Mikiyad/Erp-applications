namespace Micromind.ClientLibraries
{
	public class Entity
	{
		private object name;

		private object id;

		public object Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public object Value
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}
	}
}
