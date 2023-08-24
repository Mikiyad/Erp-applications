using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class OpportunityStageComboBox : SingleColumnComboBox
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

		public OpportunityStageComboBox()
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
			base.Items.Add("None");
			base.Items.Add("Prospecting");
			base.Items.Add("Qualification");
			base.Items.Add("Needs Analysis");
			base.Items.Add("Value Proposition");
			base.Items.Add("Id. Decision Makers");
			base.Items.Add("Perception Analysis");
			base.Items.Add("Proposal/Price Quote");
			base.Items.Add("Negotiation/Review");
			base.Items.Add("Closed Won");
			base.Items.Add("Closed Lost");
		}
	}
}
