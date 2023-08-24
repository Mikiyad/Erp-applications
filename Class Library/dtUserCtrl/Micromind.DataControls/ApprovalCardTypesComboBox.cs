using Micromind.Common.Data;
using Micromind.DataControls.Libraries;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ApprovalCardTypesComboBox : ComboBox
	{
		private Container components;

		public DataComboType SelectedID
		{
			get
			{
				ComboData comboData = base.SelectedItem as ComboData;
				if (comboData != null)
				{
					return (DataComboType)int.Parse(comboData.ID);
				}
				return DataComboType.None;
			}
			set
			{
				foreach (object item in base.Items)
				{
					ComboData comboData = item as ComboData;
					if (comboData != null)
					{
						string iD = comboData.ID;
						int num = (int)value;
						if (iD == num.ToString())
						{
							base.SelectedItem = item;
							break;
						}
					}
				}
			}
		}

		public ApprovalCardTypesComboBox()
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

		public void LoadData()
		{
			base.Items.Clear();
			base.Items.Add(new ComboData("Customer", 1.ToString()));
			base.Items.Add(new ComboData("Vendor", 2.ToString()));
			base.Items.Add(new ComboData("Product", 23.ToString()));
			base.Items.Add(new ComboData("Accounts", 4.ToString()));
			base.Items.Add(new ComboData("Employee", 18.ToString()));
			base.Items.Add(new ComboData("Project", 95.ToString()));
			base.Items.Add(new ComboData("BOM", 156.ToString()));
		}

		public void Clear()
		{
			if (base.Items.Count > 0)
			{
				SelectedIndex = 0;
			}
			else
			{
				SelectedIndex = -1;
			}
		}
	}
}
