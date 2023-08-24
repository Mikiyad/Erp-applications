using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class AccountTypesComboBox : ComboBox
	{
		private Container components;

		public int SelectedID
		{
			get
			{
				return SelectedIndex + 1;
			}
			set
			{
				SelectedIndex = value - 1;
			}
		}

		public AccountTypesComboBox()
		{
			InitializeComponent();
			base.ParentChanged += AccountTypesComboBox_ParentChanged;
		}

		private void AccountTypesComboBox_ParentChanged(object sender, EventArgs e)
		{
			LoadData();
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
			new System.Resources.ResourceManager(typeof(Micromind.DataControls.AccountTypesComboBox));
			SuspendLayout();
			base.Name = "AccountTypesComboBox";
			base.Size = new System.Drawing.Size(192, 20);
		}

		public void LoadData()
		{
			base.Items.Clear();
			base.Items.Add("Asset");
			base.Items.Add("Liability");
			base.Items.Add("Income");
			base.Items.Add("Expense");
			base.Items.Add("Capital");
		}
	}
}
