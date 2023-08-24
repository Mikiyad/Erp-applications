using Micromind.ClientLibraries;
using Micromind.DataControls.Libraries;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class SubSmartListComboBox : SingleColumnComboBox
	{
		private Container components;

		public override int SelectedID
		{
			get
			{
				if (SelectedIndex < 0)
				{
					return -1;
				}
				ComboData comboData = base.SelectedItem as ComboData;
				if (comboData == null)
				{
					return -1;
				}
				return int.Parse(comboData.ID);
			}
			set
			{
				if (value < 0)
				{
					SelectedIndex = -1;
				}
				else
				{
					foreach (ComboData item in base.Items)
					{
						if (int.Parse(item.ID.ToString()) == value)
						{
							base.SelectedItem = item;
						}
					}
				}
			}
		}

		public SubSmartListComboBox()
		{
			InitializeComponent();
			base.DropDownStyle = ComboBoxStyle.DropDownList;
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
			foreach (DataRow row in Factory.SmartListSystem.GetSubSmartListComboList().Tables[0].Rows)
			{
				ComboData item = new ComboData(row["Name"].ToString(), row["Code"].ToString());
				base.Items.Add(item);
			}
		}
	}
}
