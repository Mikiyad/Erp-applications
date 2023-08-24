using System;

namespace Micromind.Common.Libraries
{
	[Serializable]
	public class ApplicationUpdateConfig
	{
		public string ProductName;

		public string productVersion = "3.0.0.0";

		public int MajorVersion = 3;

		public int MinorVersion;

		public int BuildVersion;

		public int RevisionVersion;

		public string ClientUpdatePath = "";

		public string ProductVersion
		{
			get
			{
				return GetVersionString();
			}
			set
			{
				productVersion = value;
				string[] array = value.Split('.');
				if (array.Length != 0)
				{
					MajorVersion = int.Parse(array[0]);
					MinorVersion = int.Parse(array[1]);
					BuildVersion = int.Parse(array[2]);
					RevisionVersion = int.Parse(array[3]);
				}
			}
		}

		public int GetVersionInteger()
		{
			return checked(MajorVersion * 100000000 + MinorVersion * 1000000 + BuildVersion * 10000 + RevisionVersion);
		}

		public string GetVersionString()
		{
			return MajorVersion.ToString() + "." + MinorVersion.ToString() + "." + BuildVersion + "." + RevisionVersion;
		}
	}
}
