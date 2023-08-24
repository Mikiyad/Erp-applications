using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Micromind.Common.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CreditInsuranceStatusComboBox : UltraComboEditor
	{
		private ImageList imageList1;

		private IContainer components;

		public int SelectedID
		{
			get
			{
				if (base.SelectedIndex <= 0)
				{
					return 1;
				}
				return int.Parse(base.SelectedItem.DataValue.ToString());
			}
			set
			{
				if (Items.Count != 0)
				{
					if (value <= 0)
					{
						base.SelectedIndex = 0;
					}
					else
					{
						foreach (ValueListItem item in Items)
						{
							if (int.Parse(item.DataValue.ToString()) == value)
							{
								base.SelectedItem = item;
								break;
							}
						}
					}
				}
			}
		}

		public CRMRelatedTypes SelectedType
		{
			get
			{
				if (SelectedID < 0)
				{
					return CRMRelatedTypes.Lead;
				}
				return (CRMRelatedTypes)SelectedID;
			}
			set
			{
				SelectedID = (int)value;
			}
		}

		public CreditInsuranceStatusComboBox()
		{
			InitializeComponent();
			DropDownStyle = DropDownStyle.DropDownList;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.CRMRelatedTypesComboBox));
			imageList1 = new System.Windows.Forms.ImageList(components);
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "customer");
			imageList1.Images.SetKeyName(1, "lead");
			imageList1.Images.SetKeyName(2, "opporunity");
			base.Name = "ItemTypesComboBox";
			base.Size = new System.Drawing.Size(192, 21);
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}

		public void LoadRequestStatus()
		{
			Items.Clear();
		}

		public void LoadData()
		{
			Items.Clear();
			Items.Add(new ValueListItem(1, "Not Insured"));
			Items.Add(new ValueListItem(2, "Under Process"));
			Items.Add(new ValueListItem(3, "Insured"));
			Items.Add(new ValueListItem(4, "Insured - Sublimit of Parent"));
			Items.Add(new ValueListItem(5, "Rejected"));
			Items.Add(new ValueListItem(6, "OnHold"));
			Items.Add(new ValueListItem(7, "Cancelled"));
		}
	}
}
