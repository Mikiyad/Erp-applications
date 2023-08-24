using Micromind.Common.Data;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class POStatusComboBox : SingleColumnComboBox
	{
		private Container components;

		public override int SelectedID
		{
			get
			{
				if (SelectedIndex <= 0)
				{
					return -1;
				}
				return SelectedIndex + 1;
			}
			set
			{
				if (base.Items.Count != 0)
				{
					if (value <= 0)
					{
						SelectedIndex = 0;
					}
					else
					{
						SelectedIndex = value - 1;
					}
				}
			}
		}

		public PurchaseOrderStatus SelectedStatus
		{
			get
			{
				if (SelectedID < 0)
				{
					return PurchaseOrderStatus.Open;
				}
				return (PurchaseOrderStatus)SelectedID;
			}
			set
			{
				SelectedID = (int)value;
			}
		}

		public POStatusComboBox()
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

		public void LoadRequestStatus()
		{
			base.Items.Clear();
			base.Items.Add("Open");
			base.Items.Add("Closed");
		}

		public override void LoadData()
		{
			base.Items.Clear();
			base.Items.Add("Open");
			base.Items.Add("Shipped");
			base.Items.Add("Received");
			base.Items.Add("Closed");
			base.Items.Add("Cancelled");
		}
	}
}
