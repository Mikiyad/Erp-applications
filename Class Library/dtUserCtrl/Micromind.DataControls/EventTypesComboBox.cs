using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class EventTypesComboBox : SingleColumnComboBox
	{
		private Container components;

		public new int SelectedID
		{
			get
			{
				if (SelectedIndex < 0)
				{
					return -1;
				}
				return SelectedIndex;
			}
			set
			{
				if (value < 0)
				{
					SelectedIndex = -1;
				}
				else
				{
					SelectedIndex = value;
				}
			}
		}

		public EventTypesComboBox()
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

		public override void LoadData()
		{
			base.Items.Clear();
			base.Items.Add("Email");
			base.Items.Add("Meeting");
			base.Items.Add("Call");
			base.Items.Add("Other");
		}
	}
}
