using Micromind.Common.Data;
using System;
using System.Collections;

namespace Micromind.DataCaches
{
	public static class ComboDataHelper
	{
		public static Hashtable comboRefreshRegister;

		static ComboDataHelper()
		{
			comboRefreshRegister = new Hashtable();
			string[] names = Enum.GetNames(typeof(DataComboType));
			for (int i = 0; i < names.Length; i++)
			{
				comboRefreshRegister.Add(names[i], false);
			}
		}

		public static bool NeedRefresh(DataComboType type)
		{
			return bool.Parse(comboRefreshRegister[type.ToString()].ToString());
		}

		public static void SetRefreshStatus(DataComboType type, bool needRefresh)
		{
			comboRefreshRegister[type.ToString()] = needRefresh;
		}

		public static void ResetToRefreshAll()
		{
			string[] names = Enum.GetNames(typeof(DataComboType));
			for (int i = 0; i < names.Length; i++)
			{
				comboRefreshRegister[names[i]] = true;
			}
		}
	}
}
