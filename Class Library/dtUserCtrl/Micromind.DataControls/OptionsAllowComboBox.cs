using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class OptionsAllowComboBox : SingleColumnComboBox
	{
		private bool hasPasswordItem = true;

		private Container components;

		public bool HasPasswordItem
		{
			get
			{
				return hasPasswordItem;
			}
			set
			{
				hasPasswordItem = value;
			}
		}

		public OptionsAllowComboBox()
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
			base.Items.Add("Allow");
			base.Items.Add("Allow With Warning");
			base.Items.Add("Don't Allow");
			if (hasPasswordItem)
			{
				base.Items.Add("Password");
			}
			SelectedIndex = 1;
		}
	}
}
