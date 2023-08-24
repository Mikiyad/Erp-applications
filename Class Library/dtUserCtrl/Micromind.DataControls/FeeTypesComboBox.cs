using Micromind.Common.Data;
using Micromind.DataControls.Libraries;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class FeeTypesComboBox : ComboBox
	{
		private Container components;

		public int SelectedID
		{
			get
			{
				if (base.SelectedItem == null)
				{
					return 0;
				}
				return int.Parse(((ComboData)base.SelectedItem).ID);
			}
			set
			{
				if (base.Items.Count == 0)
				{
					SelectedIndex = -1;
				}
				else
				{
					foreach (object item in base.Items)
					{
						if ((item as ComboData).ID == value.ToString())
						{
							base.SelectedItem = item;
						}
					}
				}
			}
		}

		public JobFeeTypes SelectedType
		{
			get
			{
				if (SelectedID == 0)
				{
					return JobFeeTypes.Fee;
				}
				return (JobFeeTypes)SelectedID;
			}
			set
			{
				SelectedID = (int)value;
			}
		}

		public FeeTypesComboBox()
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
			base.Items.Add(new ComboData("Project Fee", 1.ToString()));
			base.Items.Add(new ComboData("Service", 2.ToString()));
			base.Items.Add(new ComboData("Advance", 3.ToString()));
			base.Items.Add(new ComboData("Retention", 4.ToString()));
		}
	}
}
