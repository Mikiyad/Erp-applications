using System.Data;

namespace Micromind.DataControls.Libraries
{
	public class ComboData
	{
		private string name;

		private string id;

		private object tag;

		private DbType type;

		public string Name
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

		public string ID
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

		public object Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		public DbType FieldType
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		public ComboData()
		{
		}

		public ComboData(string name, string id)
		{
			this.name = name;
			this.id = id;
		}

		public ComboData(string name, string id, object tag)
		{
			this.name = name;
			this.id = id;
			this.tag = tag;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
