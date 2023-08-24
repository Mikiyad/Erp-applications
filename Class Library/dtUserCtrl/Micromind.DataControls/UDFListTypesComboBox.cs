using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls.Libraries;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class UDFListTypesComboBox : SingleColumnComboBox
	{
		private Container components;

		public string SelectedCustomListCode
		{
			get
			{
				if (SelectedIndex < 0)
				{
					return "";
				}
				ComboData comboData = base.SelectedItem as ComboData;
				if (comboData == null || comboData.Tag == null)
				{
					return "";
				}
				return comboData.Tag.ToString();
			}
		}

		public override int SelectedID
		{
			get
			{
				if (SelectedIndex < 0)
				{
					return -1;
				}
				ComboData comboData = base.SelectedItem as ComboData;
				if (comboData == null)
				{
					return -1;
				}
				return int.Parse(comboData.ID);
			}
			set
			{
				if (value < 0)
				{
					SelectedIndex = -1;
				}
				else
				{
					foreach (ComboData item in base.Items)
					{
						if (int.Parse(item.ID.ToString()) == value)
						{
							base.SelectedItem = item;
						}
					}
				}
			}
		}

		public UDFListTypesComboBox()
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
			base.Items.Add(new ComboData(DataComboType.AccountGroup.ToString(), 17.ToString()));
			base.Items.Add(new ComboData(DataComboType.Accounts.ToString(), 4.ToString()));
			base.Items.Add(new ComboData(DataComboType.Activity.ToString(), 99.ToString()));
			base.Items.Add(new ComboData(DataComboType.Agent.ToString(), 117.ToString()));
			base.Items.Add(new ComboData(DataComboType.Area.ToString(), 9.ToString()));
			base.Items.Add(new ComboData(DataComboType.Bank.ToString(), 45.ToString()));
			base.Items.Add(new ComboData(DataComboType.Buyer.ToString(), 22.ToString()));
			base.Items.Add(new ComboData(DataComboType.City.ToString(), 106.ToString()));
			base.Items.Add(new ComboData(DataComboType.Color.ToString(), 212.ToString()));
			base.Items.Add(new ComboData(DataComboType.CompanyDivision.ToString(), 230.ToString()));
			base.Items.Add(new ComboData(DataComboType.Competitor.ToString(), 98.ToString()));
			base.Items.Add(new ComboData(DataComboType.Contact.ToString(), 19.ToString()));
			base.Items.Add(new ComboData(DataComboType.ContainerSize.ToString(), 124.ToString()));
			base.Items.Add(new ComboData(DataComboType.ContainerType.ToString(), 202.ToString()));
			base.Items.Add(new ComboData(DataComboType.CostCenter.ToString(), 52.ToString()));
			base.Items.Add(new ComboData(DataComboType.Country.ToString(), 8.ToString()));
			base.Items.Add(new ComboData(DataComboType.Currency.ToString(), 20.ToString()));
			base.Items.Add(new ComboData(DataComboType.Customer.ToString(), 1.ToString()));
			base.Items.Add(new ComboData(DataComboType.CustomerClass.ToString(), 5.ToString()));
			base.Items.Add(new ComboData(DataComboType.CustomerGroup.ToString(), 6.ToString()));
			base.Items.Add(new ComboData(DataComboType.Department.ToString(), 30.ToString()));
			base.Items.Add(new ComboData(DataComboType.Destination.ToString(), 46.ToString()));
			base.Items.Add(new ComboData(DataComboType.Division.ToString(), 35.ToString()));
			base.Items.Add(new ComboData(DataComboType.Driver.ToString(), 59.ToString()));
			base.Items.Add(new ComboData(DataComboType.Employee.ToString(), 18.ToString()));
			base.Items.Add(new ComboData(DataComboType.Grade.ToString(), 31.ToString()));
			base.Items.Add(new ComboData(DataComboType.Item.ToString(), 3.ToString()));
			base.Items.Add(new ComboData(DataComboType.Job.ToString(), 95.ToString()));
			base.Items.Add(new ComboData(DataComboType.Lead.ToString(), 88.ToString()));
			base.Items.Add(new ComboData(DataComboType.Location.ToString(), 29.ToString()));
			base.Items.Add(new ComboData(DataComboType.Nationality.ToString(), 33.ToString()));
			base.Items.Add(new ComboData(DataComboType.Opportunity.ToString(), 97.ToString()));
			base.Items.Add(new ComboData(DataComboType.PaymentTerm.ToString(), 12.ToString()));
			base.Items.Add(new ComboData(DataComboType.Port.ToString(), 62.ToString()));
			base.Items.Add(new ComboData(DataComboType.Position.ToString(), 36.ToString()));
			base.Items.Add(new ComboData(DataComboType.ProductBrand.ToString(), 25.ToString()));
			base.Items.Add(new ComboData(DataComboType.ProductCategory.ToString(), 24.ToString()));
			base.Items.Add(new ComboData(DataComboType.ProductClass.ToString(), 28.ToString()));
			base.Items.Add(new ComboData(DataComboType.Property.ToString(), 142.ToString()));
			base.Items.Add(new ComboData(DataComboType.Salesperson.ToString(), 11.ToString()));
			base.Items.Add(new ComboData(DataComboType.User.ToString(), 68.ToString()));
			base.Items.Add(new ComboData(DataComboType.UserGroup.ToString(), 69.ToString()));
			base.Items.Add(new ComboData(DataComboType.Vehicle.ToString(), 107.ToString()));
			base.Items.Add(new ComboData(DataComboType.Vendor.ToString(), 2.ToString()));
			DataSet customListCodes = Factory.CustomListSystem.GetCustomListCodes();
			if (customListCodes != null)
			{
				foreach (DataRow row in customListCodes.Tables["Custom_List"].Rows)
				{
					string tag = row["ListCode"].ToString();
					string name = row["ListName"].ToString();
					base.Items.Add(new ComboData(name, 1000.ToString(), tag));
				}
			}
			base.Sorted = true;
		}
	}
}
