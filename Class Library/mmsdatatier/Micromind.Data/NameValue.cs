namespace Micromind.Data
{
	public class NameValue
	{
		public string Name = "";

		public string ID = "";

		public string Type = "";

		public NameValue(string name, string id)
		{
			Name = name;
			ID = id;
		}

		public NameValue(string name, int id, string Type)
		{
			Name = name;
			ID = id.ToString();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
