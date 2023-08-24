using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;

namespace Micromind.DataControls
{
	public sealed class ComboEvents
	{
		public static event EventHandler QuickAddRequested;

		public static event EventHandler CreateNewAccount;

		public static event EventHandler CreateNewItem;

		private ComboEvents()
		{
		}

		internal static void OnCreateNewAccount(object sender, string itemName, CompanyAccountTypes accountType)
		{
			if (ComboEvents.CreateNewAccount != null)
			{
				ComboEvents.CreateNewAccount(sender, new NewAccountEventArg(itemName, accountType));
			}
		}

		internal static void OnCreateNewItem(object sender, string itemName)
		{
			if (ComboEvents.CreateNewItem != null)
			{
				ComboEvents.CreateNewItem(sender, new NewItemEventArg(itemName));
			}
		}

		internal static void OnQuickAddRequested(object sender, string id, DataComboType comboType)
		{
			if (ComboEvents.QuickAddRequested != null)
			{
				ComboQuickAddData comboQuickAddData = new ComboQuickAddData();
				comboQuickAddData.ID = id;
				comboQuickAddData.Sender = sender;
				comboQuickAddData.ComboType = comboType;
				ComboEvents.QuickAddRequested(comboQuickAddData, null);
			}
		}
	}
}
