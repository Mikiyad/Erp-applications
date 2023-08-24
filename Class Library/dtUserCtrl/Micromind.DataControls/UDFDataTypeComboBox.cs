using Micromind.DataControls.Libraries;
using System.ComponentModel;
using System.Drawing;
using System.Resources;

namespace Micromind.DataControls
{
	public class UDFDataTypeComboBox : SingleColumnComboBox
	{
		private Container components;

		public override int SelectedID
		{
			get
			{
				if (SelectedIndex == -1)
				{
					return -1;
				}
				return int.Parse(((ComboData)base.SelectedItem).ID);
			}
			set
			{
				foreach (ComboData item in base.Items)
				{
					if (item.ID == value.ToString())
					{
						base.SelectedItem = item;
						break;
					}
				}
			}
		}

		public UDFDataTypeComboBox()
		{
			InitializeComponent();
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
			base.Name = "ItemTypesComboBox";
			base.Size = new System.Drawing.Size(192, 20);
		}

		public override void LoadData()
		{
			base.Items.Clear();
			base.Items.Add(new ComboData("Text", 1.ToString()));
			base.Items.Add(new ComboData("Date", 4.ToString()));
			base.Items.Add(new ComboData("Time", 5.ToString()));
			base.Items.Add(new ComboData("Number", 6.ToString()));
			base.Items.Add(new ComboData("CheckBox", 7.ToString()));
			base.Items.Add(new ComboData("Note", 3.ToString()));
			base.Items.Add(new ComboData("List", 2.ToString()));
		}
	}
}
