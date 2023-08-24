using System.ComponentModel;
using System.Drawing;
using System.Resources;

namespace Micromind.DataControls
{
	public class DaysComboBox : SingleColumnComboBox
	{
		private Container components;

		public DaysComboBox()
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

		public override void LoadData()
		{
			base.Items.Clear();
			base.Items.Add("N/A");
			base.Items.Add("Saturday");
			base.Items.Add("Sunday");
			base.Items.Add("Monday");
			base.Items.Add("Tuesday");
			base.Items.Add("Wednesday");
			base.Items.Add("Thursday");
			base.Items.Add("Friday");
		}
	}
}
