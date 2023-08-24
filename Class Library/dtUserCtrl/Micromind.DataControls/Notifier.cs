using System;
using System.Drawing;

namespace Micromind.DataControls
{
	public class Notifier
	{
		public object Tag;

		private ReminderNotifierForm notifierForm = new ReminderNotifierForm();

		public event EventHandler CloseClicked;

		public event EventHandler TitleClicked;

		public event EventHandler ContentClicked;

		public Notifier()
		{
			notifierForm.TitleRectangle = new Rectangle(40, 9, 70, 25);
			notifierForm.ContentRectangle = new Rectangle(8, 41, 133, 68);
			notifierForm.TitleClick += TitleClick;
			notifierForm.ContentClick += ContentClick;
			notifierForm.CloseClick += CloseClick;
			notifierForm.CloseClickable = true;
			notifierForm.TitleClickable = false;
			notifierForm.ContentClickable = true;
			notifierForm.EnableSelectionRectangle = false;
			notifierForm.KeepVisibleOnMousOver = true;
			notifierForm.ReShowOnMouseOver = true;
		}

		public void ShowReminder(string title, string content)
		{
			notifierForm.Show(title, content, 300, 60000, 300);
		}

		private void CloseClick(object obj, EventArgs ea)
		{
			if (this.CloseClicked != null)
			{
				this.CloseClicked(this, null);
			}
		}

		private void TitleClick(object obj, EventArgs ea)
		{
			if (this.TitleClicked != null)
			{
				this.TitleClicked(this, null);
			}
		}

		private void ContentClick(object obj, EventArgs ea)
		{
			if (this.ContentClicked != null)
			{
				this.ContentClicked(this, null);
			}
		}

		public void Hide()
		{
			notifierForm.Hide();
			notifierForm.Close();
		}
	}
}
