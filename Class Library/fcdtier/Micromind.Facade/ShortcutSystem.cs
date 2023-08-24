using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;

namespace Micromind.Facade
{
	public sealed class ShortcutSystem : MarshalByRefObject, IShortcutSystem, IDisposable
	{
		private Config config;

		public ShortcutSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateShortcut(ShortcutData data)
		{
			return new Shortcut(config).InsertShortcut(data);
		}

		public ShortcutData GetShortcuts(byte type, string userID)
		{
			using (Shortcut shortcut = new Shortcut(config))
			{
				return shortcut.GetShortcuts(type, userID);
			}
		}

		public bool DeleteShortcut(byte type, string userID, string key)
		{
			using (Shortcut shortcut = new Shortcut(config))
			{
				return shortcut.DeleteShortcut(type, userID, key);
			}
		}
	}
}
