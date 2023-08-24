using Micromind.Common.Data;

namespace Micromind.Common.Interfaces
{
	public interface IShortcutSystem
	{
		bool CreateShortcut(ShortcutData shortcutData);

		ShortcutData GetShortcuts(byte type, string userID);

		bool DeleteShortcut(byte type, string userID, string key);
	}
}
