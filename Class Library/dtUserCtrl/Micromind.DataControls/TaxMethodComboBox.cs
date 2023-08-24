using Micromind.Common.Data;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class TaxMethodComboBox : ComboBox
	{
		private Container components;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		public TaxCalculationMethods SelectedMethod
		{
			get
			{
				return (TaxCalculationMethods)SelectedID;
			}
			set
			{
				SelectedID = (int)value;
			}
		}

		public TaxMethodComboBox()
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
			base.Items.Add("Percentage of Sale");
			base.Items.Add("Percentage of Cost");
			base.Items.Add("Percentage of Profit");
		}

		public void Clear()
		{
			SelectedIndex = 0;
		}
	}
}
