using System;

namespace Micromind.ClientLibraries
{
	public class NewItemEventArg : EventArgs
	{
		public string ItemName;

		public NewItemEventArg()
		{
		}

		public NewItemEventArg(string itemName)
		{
			ItemName = itemName;
		}
	}
}
