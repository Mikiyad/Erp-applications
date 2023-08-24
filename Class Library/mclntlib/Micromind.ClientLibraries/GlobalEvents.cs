using Micromind.Common.Data;
using System;

namespace Micromind.ClientLibraries
{
	public sealed class GlobalEvents
	{
		public delegate void ApprovalHandler(ApprovalStatus newStatus, int taskID);

		public static event EventHandler ShortcutLinkClicked;

		public static event ApprovalHandler ApprovalStatusChanged;

		public static event EventHandler UserFavoriteChanged;

		public static void OnUserFavoriteChanged(object sender, EventArgs args)
		{
			if (GlobalEvents.UserFavoriteChanged != null)
			{
				GlobalEvents.UserFavoriteChanged(sender, args);
			}
		}

		public static void OnApprovalStatusChanged(ApprovalStatus status, int taskID)
		{
			if (GlobalEvents.ApprovalStatusChanged != null)
			{
				GlobalEvents.ApprovalStatusChanged(status, taskID);
			}
		}

		public static void OnShortcutLinkClicked(object sender, EventArgs args)
		{
			if (GlobalEvents.ShortcutLinkClicked != null)
			{
				GlobalEvents.ShortcutLinkClicked(sender, args);
			}
		}
	}
}
