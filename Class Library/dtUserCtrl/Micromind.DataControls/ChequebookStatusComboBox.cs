using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ChequebookStatusComboBox : ComboBox
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
				if (base.Items.Count != 0)
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
		}

		public ChequebookStatusComboBox()
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
			base.Items.Add("Active");
			base.Items.Add("Hold");
			base.Items.Add("Closed");
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
