using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls.Libraries;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ItemTypesComboBox : ComboBox
	{
		private Container components;

		public int SelectedID
		{
			get
			{
				if (base.SelectedItem == null)
				{
					return 0;
				}
				return int.Parse(((ComboData)base.SelectedItem).ID);
			}
			set
			{
				if (base.Items.Count == 0)
				{
					SelectedIndex = -1;
				}
				else
				{
					foreach (object item in base.Items)
					{
						if ((item as ComboData).ID == value.ToString())
						{
							base.SelectedItem = item;
						}
					}
				}
			}
		}

		public ItemTypes SelectedType
		{
			get
			{
				if (SelectedID == 0)
				{
					return ItemTypes.Inventory;
				}
				return (ItemTypes)SelectedID;
			}
			set
			{
				SelectedID = (int)value;
			}
		}

		public ItemTypesComboBox()
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
			base.Items.Add(new ComboData("Inventory", 1.ToString()));
			base.Items.Add(new ComboData("Non-Inventory", 2.ToString()));
			base.Items.Add(new ComboData("Service", 3.ToString()));
			base.Items.Add(new ComboData("Discount", 4.ToString()));
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.Consignment))
			{
				base.Items.Add(new ComboData("Consignment Item", 5.ToString()));
			}
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.Warehouse3PL))
			{
				base.Items.Add(new ComboData("Inventory 3PL", 9.ToString()));
			}
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.BasicManufacturing))
			{
				base.Items.Add(new ComboData("Assembly", 7.ToString()));
			}
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.JobCosting))
			{
				base.Items.Add(new ComboData("Project Fee", 8.ToString()));
			}
		}
	}
}
