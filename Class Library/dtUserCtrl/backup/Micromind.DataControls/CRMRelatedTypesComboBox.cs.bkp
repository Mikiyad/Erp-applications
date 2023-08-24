using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Micromind.Common.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CRMRelatedTypesComboBox : UltraComboEditor
	{
		private ImageList imageList1;

		private IContainer components;

		private int isFlag;

		public int IsFlag
		{
			get
			{
				return isFlag;
			}
			set
			{
				isFlag = value;
			}
		}

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

		public CRMRelatedTypesComboBox()
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
			ValueListItem valueListItem = new ValueListItem(1, "Lead");
			valueListItem.Appearance.Image = imageList1.Images["lead"];
			Items.Add(valueListItem);
			valueListItem = new ValueListItem(2, "Customer");
			valueListItem.Appearance.Image = imageList1.Images["customer"];
			Items.Add(valueListItem);
			valueListItem = new ValueListItem(3, "Opporunity");
			valueListItem.Appearance.Image = imageList1.Images["opporunity"];
			Items.Add(valueListItem);
		}
	}
}
