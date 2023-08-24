using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class SingleColumnComboBox : ComboBox
	{
		private IContainer components;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual int SelectedID
		{
			get
			{
				if (SelectedIndex == -1)
				{
					return -1;
				}
				return SelectedIndex + 1;
			}
			set
			{
				if (base.Items.Count == 0)
				{
					LoadData();
				}
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

		public SingleColumnComboBox()
		{
			InitializeComponent();
		}

		public SingleColumnComboBox(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public virtual void LoadData()
		{
			MessageBox.Show("Load Data called from base");
		}

		public virtual void Clear()
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
		}
	}
}
