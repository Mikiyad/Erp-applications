using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class DepMethodComboBox : ComboBox
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

		public DepMethodComboBox()
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
			base.Items.Add("Manual");
			base.Items.Add("Straight Line");
			base.Items.Add("Reducing Balance");
		}

		public void Clear()
		{
			SelectedIndex = -1;
		}
	}
}
