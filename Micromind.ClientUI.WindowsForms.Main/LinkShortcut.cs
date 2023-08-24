using Micromind.ClientLibraries;
using System;
using System.Collections;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class LinkShortcut
	{
		private static ArrayList shortcutList;

		private string name = "";

		private string key = "";

		private string category;

		private int categoryIndex = -1;

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

		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				key = value;
			}
		}

		public string Category
		{
			get
			{
				return category;
			}
			set
			{
				category = value;
			}
		}

		public int CategoryIndex
		{
			get
			{
				return categoryIndex;
			}
			set
			{
				if (value < 0)
				{
					throw new ApplicationException("Category index must be greater or equal than zero.");
				}
				categoryIndex = value;
			}
		}

		public static ArrayList ShortcutList => shortcutList;

		public LinkShortcut(string name, string key, int categoryIndex)
		{
			Name = name;
			Key = key;
			CategoryIndex = categoryIndex;
		}

		public LinkShortcut()
		{
		}

		public void Read()
		{
			RegistryHelper registryHelper = null;
			try
			{
				registryHelper = new RegistryHelper("Shortcuts\\" + category, writable: false);
				string stringValue = registryHelper.GetStringValue(Key);
				int num = stringValue.IndexOf(":");
				if (num > -1)
				{
					try
					{
						categoryIndex = int.Parse(stringValue.Substring(checked(num + 1)));
					}
					catch
					{
					}
					try
					{
						name = stringValue.Substring(0, num);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			finally
			{
				registryHelper?.Dispose();
			}
		}

		public void Save()
		{
			RegistryHelper registryHelper = null;
			try
			{
				registryHelper = new RegistryHelper("Shortcuts\\" + category, writable: true);
				registryHelper.SetValue(Key, name + ":" + categoryIndex.ToString());
			}
			catch
			{
			}
			finally
			{
				registryHelper?.Dispose();
			}
		}

		public static void ReadAll(string category)
		{
			RegistryHelper registryHelper = null;
			if (shortcutList == null)
			{
				shortcutList = new ArrayList();
			}
			try
			{
				registryHelper = new RegistryHelper("Shortcuts\\" + category, writable: false);
				string[] valueNames = registryHelper.GetValueNames();
				foreach (string text in valueNames)
				{
					LinkShortcut linkShortcut = new LinkShortcut();
					linkShortcut.key = text;
					linkShortcut.Category = category;
					linkShortcut.Read();
					shortcutList.Add(linkShortcut);
				}
			}
			catch
			{
			}
			finally
			{
				registryHelper?.Dispose();
			}
		}

		public static void SaveAll()
		{
			if (shortcutList != null)
			{
				try
				{
					foreach (LinkShortcut shortcut in shortcutList)
					{
						shortcut.Save();
					}
				}
				catch
				{
				}
			}
		}

		public override string ToString()
		{
			return Key;
		}
	}
}
