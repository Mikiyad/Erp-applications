using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class MMComboBox : UltraCombo
	{
		private IContainer components;

		public MMComboBox()
		{
			InitializeComponent();
			base.KeyDown += MMComboBox_KeyDown;
		}

		private void MMComboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape && !base.IsDroppedDown)
			{
				PerformAction(UltraComboAction.Dropdown);
			}
		}

		public void SetupControl()
		{
			try
			{
				if (base.DisplayLayout.Bands.Count != 0)
				{
					if (base.DisplayLayout.Bands[0].Columns.Count > 0)
					{
						base.ValueMember = base.DisplayLayout.Bands[0].Columns[0].Key;
						base.DisplayLayout.Bands[0].Columns[0].Hidden = true;
					}
					base.DisplayLayout.Bands[0].ColHeadersVisible = false;
					base.DisplayLayout.Bands[0].Override.BorderStyleCell = UIElementBorderStyle.None;
					base.DisplayLayout.Bands[0].Override.BorderStyleRow = UIElementBorderStyle.None;
					base.DisplayLayout.Bands[0].Override.DefaultColWidth = 200;
					base.DisplayLayout.Bands[0].Override.DefaultRowHeight = 16;
					base.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
					base.DisplayLayout.Appearance.BorderColor = SystemColors.InactiveCaption;
					base.DisplayLayout.Bands[0].Override.HotTrackRowAppearance.BackColor = Color.LightSteelBlue;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
	}
}
