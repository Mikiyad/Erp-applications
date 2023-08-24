using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class GenderComboBox : ComboBox
	{
		private Container components;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public char SelectedGender
		{
			get
			{
				if (SelectedIndex == 1)
				{
					return 'F';
				}
				return 'M';
			}
			set
			{
				if (base.Items.Count != 0)
				{
					switch (value)
					{
					case 'M':
						SelectedIndex = 0;
						break;
					case 'F':
						SelectedIndex = 1;
						break;
					}
				}
			}
		}

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

		public GenderComboBox()
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
			base.Items.Add("Male");
			base.Items.Add("Female");
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
