using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ContainerStstusComboBox : ComboBox
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
				if (value <= 0)
				{
					SelectedIndex = -1;
				}
				else
				{
					SelectedIndex = value - 1;
				}
			}
		}

		public ContainerStstusComboBox()
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
			base.Name = "ContainerStatusComboBox";
			base.Size = new System.Drawing.Size(192, 20);
		}

		public void LoadData()
		{
			base.Items.Clear();
			base.Items.Add("Shipped");
			base.Items.Add("Arrived");
			base.Items.Add("DO Release");
			base.Items.Add("Customes Checking");
			base.Items.Add("Shipment Removal");
			base.Items.Add("Shipment Delivered");
			base.Items.Add("Off Loaded");
			base.Items.Add("Returning");
			base.Items.Add("Returned");
			base.Items.Add("Cancelled");
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
