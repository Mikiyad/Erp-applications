using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CustomerSelector : UserControl, ICustomReportControl
	{
		private bool showGroupAndClass = true;

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private List<string> CustomerList = new List<string>();

		private IContainer components;

		private customersFlatComboBox comboBoxFromCustomer;

		private customersFlatComboBox comboBoxToCustomer;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private RadioButton radioButtonClass;

		private RadioButton radioButtonGroup;

		private CustomerClassComboBox comboBoxFromClass;

		private CustomerClassComboBox comboBoxToClass;

		private Label label1;

		private CustomerGroupComboBox comboBoxFromGroup;

		private CustomerGroupComboBox comboBoxToGroup;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private RadioButton radioButtonSingle;

		private customersFlatComboBox comboBoxSingleCustomer;

		private Button buttonMultiple;

		private RadioButton radioButtonMultipleCustomer;

		private TextBox textBoxMultipleCustomers;

		private Label label6;

		private Label label7;

		private RadioButton radioButtonArea;

		private Label label8;

		private Label label9;

		private RadioButton radioButtonCountry;

		private CountryComboBox comboBoxToCountry;

		private CountryComboBox comboBoxFromCountry;

		private AreaComboBox comboBoxToArea;

		private AreaComboBox comboBoxFromArea;

		public string CustomReportFieldName
		{
			get
			{
				return crFieldName;
			}
			set
			{
				crFieldName = value;
			}
		}

		public string CustomReportKey
		{
			get
			{
				return crKey;
			}
			set
			{
				crKey = value;
			}
		}

		public byte CustomReportValueType
		{
			get
			{
				return crValueType;
			}
			set
			{
				crValueType = value;
			}
		}

		[DefaultValue(true)]
		public bool ShowGroupAndClass
		{
			get
			{
				return showGroupAndClass;
			}
			set
			{
				showGroupAndClass = value;
				if (value)
				{
					RadioButton radioButton = radioButtonClass;
					RadioButton radioButton2 = radioButtonGroup;
					CustomerClassComboBox customerClassComboBox = comboBoxFromClass;
					CustomerClassComboBox customerClassComboBox2 = comboBoxToClass;
					CustomerGroupComboBox customerGroupComboBox = comboBoxFromGroup;
					bool flag2 = comboBoxToGroup.Visible = true;
					bool flag4 = customerGroupComboBox.Visible = flag2;
					bool flag6 = customerClassComboBox2.Visible = flag4;
					bool flag8 = customerClassComboBox.Visible = flag6;
					bool visible = radioButton2.Visible = flag8;
					radioButton.Visible = visible;
					base.Height = 99;
				}
				else
				{
					RadioButton radioButton3 = radioButtonClass;
					RadioButton radioButton4 = radioButtonGroup;
					CustomerClassComboBox customerClassComboBox3 = comboBoxFromClass;
					CustomerClassComboBox customerClassComboBox4 = comboBoxToClass;
					CustomerGroupComboBox customerGroupComboBox2 = comboBoxFromGroup;
					bool flag2 = comboBoxToGroup.Visible = false;
					bool flag4 = customerGroupComboBox2.Visible = flag2;
					bool flag6 = customerClassComboBox4.Visible = flag4;
					bool flag8 = customerClassComboBox3.Visible = flag6;
					bool visible = radioButton4.Visible = flag8;
					radioButton3.Visible = visible;
					base.Height = 48;
				}
			}
		}

		public bool IsSingleCustomer => radioButtonSingle.Checked;

		public string FromCustomer
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleCustomer.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromCustomer.SelectedID;
				}
				return "";
			}
		}

		public string FromCustomerName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleCustomer.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromCustomer.SelectedName;
				}
				return "";
			}
		}

		public string ToCustomer
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleCustomer.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToCustomer.SelectedID;
				}
				return "";
			}
		}

		public string ToCustomerName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleCustomer.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToCustomer.SelectedName;
				}
				return "";
			}
		}

		public string FromClass
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxFromClass.SelectedID;
				}
				return "";
			}
		}

		public string FromClassName
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxFromClass.SelectedName;
				}
				return "";
			}
		}

		public string ToClass
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxToClass.SelectedID;
				}
				return "";
			}
		}

		public string ToClassName
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxToClass.SelectedName;
				}
				return "";
			}
		}

		public string FromGroup
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxFromGroup.SelectedID;
				}
				return "";
			}
		}

		public string FromGroupName
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxFromGroup.SelectedName;
				}
				return "";
			}
		}

		public string ToGroup
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxToGroup.SelectedID;
				}
				return "";
			}
		}

		public string ToGroupName
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxToGroup.SelectedName;
				}
				return "";
			}
		}

		public string FromArea
		{
			get
			{
				if (radioButtonArea.Checked)
				{
					return comboBoxFromArea.SelectedID;
				}
				return "";
			}
		}

		public string FromAreaName
		{
			get
			{
				if (radioButtonArea.Checked)
				{
					return comboBoxFromArea.SelectedName;
				}
				return "";
			}
		}

		public string ToArea
		{
			get
			{
				if (radioButtonArea.Checked)
				{
					return comboBoxToArea.SelectedID;
				}
				return "";
			}
		}

		public string ToAreaName
		{
			get
			{
				if (radioButtonArea.Checked)
				{
					return comboBoxToArea.SelectedName;
				}
				return "";
			}
		}

		public string FromCountry
		{
			get
			{
				if (radioButtonCountry.Checked)
				{
					return comboBoxFromCountry.SelectedID;
				}
				return "";
			}
		}

		public string FromCountryName
		{
			get
			{
				if (radioButtonCountry.Checked)
				{
					return comboBoxFromCountry.SelectedName;
				}
				return "";
			}
		}

		public string ToCountry
		{
			get
			{
				if (radioButtonCountry.Checked)
				{
					return comboBoxToCountry.SelectedID;
				}
				return "";
			}
		}

		public string ToCountryName
		{
			get
			{
				if (radioButtonCountry.Checked)
				{
					return comboBoxToCountry.SelectedName;
				}
				return "";
			}
		}

		public string MultipleCustomers
		{
			get
			{
				if (radioButtonMultipleCustomer.Checked)
				{
					return "'" + string.Join("','", CustomerList) + "'";
				}
				return "";
			}
		}

		public CustomerSelector()
		{
			InitializeComponent();
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT CustomerID FROM Customer)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleCustomer.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT CUSTOMERID FROM Customer WHERE CustomerID Between '" + comboBoxFromCustomer.SelectedID + "' AND '" + comboBoxToCustomer.SelectedID + "')";
				}
				if (radioButtonClass.Checked)
				{
					return "ANY(SELECT CUSTOMERID FROM Customer WHERE CustomerClassID Between '" + comboBoxFromClass.SelectedID + "' AND '" + comboBoxToClass.SelectedID + "')";
				}
				if (radioButtonGroup.Checked)
				{
					return "ANY(SELECT CUSTOMERID FROM Customer WHERE CustomerGroupID Between '" + comboBoxFromGroup.SelectedID + "' AND '" + comboBoxToGroup.SelectedID + "')";
				}
				if (radioButtonArea.Checked)
				{
					return " ANY(SELECT CUSTOMERID FROM Customer WHERE AreaID Between '" + comboBoxFromArea.SelectedID + "' AND '" + comboBoxToArea.SelectedID + "')";
				}
				if (radioButtonCountry.Checked)
				{
					return " ANY(SELECT CUSTOMERID FROM Customer WHERE CountryID Between '" + comboBoxFromCountry.SelectedID + "' AND '" + comboBoxToCountry.SelectedID + "')";
				}
				return "1=1";
			}
			if (crFieldName == "")
			{
				return "''=''";
			}
			if (radioButtonAll.Checked)
			{
				return "''=''";
			}
			if (radioButtonSingle.Checked)
			{
				return crFieldName + " = '" + comboBoxSingleCustomer.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromCustomer.SelectedID + "' AND '" + comboBoxToCustomer.SelectedID + "')";
			}
			if (radioButtonClass.Checked)
			{
				return crFieldName + " = ANY(SELECT CUSTOMERID FROM Customer WHERE CustomerClassID Between '" + comboBoxFromClass.SelectedID + "' AND '" + comboBoxToClass.SelectedID + "')";
			}
			if (radioButtonGroup.Checked)
			{
				return crFieldName + " = ANY(SELECT CUSTOMERID FROM Customer WHERE CustomerGroupID Between '" + comboBoxFromGroup.SelectedID + "' AND '" + comboBoxToGroup.SelectedID + "')";
			}
			if (radioButtonArea.Checked)
			{
				return crFieldName + " = ANY(SELECT CUSTOMERID FROM Customer WHERE AreaID Between '" + comboBoxFromArea.SelectedID + "' AND '" + comboBoxToArea.SelectedID + "')";
			}
			if (radioButtonCountry.Checked)
			{
				return crFieldName + " = ANY(SELECT CUSTOMERID FROM Customer WHERE CountryID Between '" + comboBoxFromCountry.SelectedID + "' AND '" + comboBoxToCountry.SelectedID + "')";
			}
			return "''=''";
		}

		private void CustomerSelector_Load(object sender, EventArgs e)
		{
			buttonMultiple.Enabled = false;
		}

		private void EnableDisableControls()
		{
			comboBoxSingleCustomer.Enabled = radioButtonSingle.Checked;
			customersFlatComboBox customersFlatComboBox = comboBoxFromCustomer;
			bool enabled = comboBoxToCustomer.Enabled = radioButtonRange.Checked;
			customersFlatComboBox.Enabled = enabled;
			CustomerGroupComboBox customerGroupComboBox = comboBoxFromGroup;
			enabled = (comboBoxToGroup.Enabled = radioButtonGroup.Checked);
			customerGroupComboBox.Enabled = enabled;
			CustomerClassComboBox customerClassComboBox = comboBoxFromClass;
			enabled = (comboBoxToClass.Enabled = radioButtonClass.Checked);
			customerClassComboBox.Enabled = enabled;
			AreaComboBox areaComboBox = comboBoxFromArea;
			enabled = (comboBoxToArea.Enabled = radioButtonArea.Checked);
			areaComboBox.Enabled = enabled;
			CountryComboBox countryComboBox = comboBoxFromCountry;
			enabled = (comboBoxToCountry.Enabled = radioButtonCountry.Checked);
			countryComboBox.Enabled = enabled;
			buttonMultiple.Enabled = radioButtonMultipleCustomer.Checked;
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
		}

		private void buttonMultiple_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> selectedDocuments = new List<string>();
			dataSet = Factory.CustomerSystem.GetCustomerList();
			SelectTransactionDialog selectTransactionDialog = new SelectTransactionDialog();
			selectTransactionDialog.DataSource = dataSet;
			selectTransactionDialog.IsMultiSelect = true;
			selectTransactionDialog.SelectedDocuments = selectedDocuments;
			selectTransactionDialog.Text = "Select Customers";
			if (selectTransactionDialog.ShowDialog(this) == DialogResult.OK)
			{
				selectedDocuments = selectTransactionDialog.SelectedDocuments;
				foreach (UltraGridRow selectedRow in selectTransactionDialog.SelectedRows)
				{
					string item = selectedRow.Cells["Code"].Value.ToString();
					selectedRow.Cells["Name"].Value.ToString();
					CustomerList.Add(item);
				}
				textBoxMultipleCustomers.Text = string.Join(",", CustomerList);
			}
		}

		private void radioButtonMultipleCustomer_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonMultipleCustomer.Checked)
			{
				buttonMultiple.Enabled = true;
				textBoxMultipleCustomers.Clear();
				CustomerList.Clear();
			}
			else
			{
				buttonMultiple.Enabled = false;
				textBoxMultipleCustomers.Clear();
				CustomerList.Clear();
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
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			radioButtonClass = new System.Windows.Forms.RadioButton();
			radioButtonGroup = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			buttonMultiple = new System.Windows.Forms.Button();
			radioButtonMultipleCustomer = new System.Windows.Forms.RadioButton();
			textBoxMultipleCustomers = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			radioButtonArea = new System.Windows.Forms.RadioButton();
			label8 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			radioButtonCountry = new System.Windows.Forms.RadioButton();
			comboBoxToArea = new Micromind.DataControls.AreaComboBox();
			comboBoxFromArea = new Micromind.DataControls.AreaComboBox();
			comboBoxToCountry = new Micromind.DataControls.CountryComboBox();
			comboBoxFromCountry = new Micromind.DataControls.CountryComboBox();
			comboBoxToGroup = new Micromind.DataControls.CustomerGroupComboBox();
			comboBoxFromGroup = new Micromind.DataControls.CustomerGroupComboBox();
			comboBoxToClass = new Micromind.DataControls.CustomerClassComboBox();
			comboBoxFromClass = new Micromind.DataControls.CustomerClassComboBox();
			comboBoxToCustomer = new Micromind.DataControls.customersFlatComboBox();
			comboBoxSingleCustomer = new Micromind.DataControls.customersFlatComboBox();
			comboBoxFromCustomer = new Micromind.DataControls.customersFlatComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxToArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCustomer).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(295, 51);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(88, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.Text = "All Customers";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 49);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonClass.AutoSize = true;
			radioButtonClass.Location = new System.Drawing.Point(6, 71);
			radioButtonClass.Name = "radioButtonClass";
			radioButtonClass.Size = new System.Drawing.Size(100, 17);
			radioButtonClass.TabIndex = 6;
			radioButtonClass.Text = "Customer Class:";
			radioButtonClass.UseVisualStyleBackColor = true;
			radioButtonClass.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonGroup.AutoSize = true;
			radioButtonGroup.Location = new System.Drawing.Point(6, 93);
			radioButtonGroup.Name = "radioButtonGroup";
			radioButtonGroup.Size = new System.Drawing.Size(104, 17);
			radioButtonGroup.TabIndex = 9;
			radioButtonGroup.Text = "Customer Group:";
			radioButtonGroup.UseVisualStyleBackColor = true;
			radioButtonGroup.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(295, 97);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(23, 13);
			label1.TabIndex = 11;
			label1.Text = "To:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(295, 74);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 6;
			label2.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(139, 51);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 6;
			label3.Text = "From:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(139, 74);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 6;
			label4.Text = "From:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(139, 97);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 13);
			label5.TabIndex = 11;
			label5.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Checked = true;
			radioButtonSingle.Location = new System.Drawing.Point(123, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.TabStop = true;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			buttonMultiple.Location = new System.Drawing.Point(398, 23);
			buttonMultiple.Name = "buttonMultiple";
			buttonMultiple.Size = new System.Drawing.Size(26, 23);
			buttonMultiple.TabIndex = 61;
			buttonMultiple.Text = "...";
			buttonMultiple.UseVisualStyleBackColor = true;
			buttonMultiple.Click += new System.EventHandler(buttonMultiple_Click);
			radioButtonMultipleCustomer.AutoSize = true;
			radioButtonMultipleCustomer.Location = new System.Drawing.Point(6, 27);
			radioButtonMultipleCustomer.Name = "radioButtonMultipleCustomer";
			radioButtonMultipleCustomer.Size = new System.Drawing.Size(116, 17);
			radioButtonMultipleCustomer.TabIndex = 60;
			radioButtonMultipleCustomer.Text = "Multiple Customers:";
			radioButtonMultipleCustomer.UseVisualStyleBackColor = true;
			radioButtonMultipleCustomer.CheckedChanged += new System.EventHandler(radioButtonMultipleCustomer_CheckedChanged);
			textBoxMultipleCustomers.Location = new System.Drawing.Point(182, 24);
			textBoxMultipleCustomers.Name = "textBoxMultipleCustomers";
			textBoxMultipleCustomers.ReadOnly = true;
			textBoxMultipleCustomers.Size = new System.Drawing.Size(214, 20);
			textBoxMultipleCustomers.TabIndex = 59;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(139, 119);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(33, 13);
			label6.TabIndex = 65;
			label6.Text = "From:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(295, 119);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(23, 13);
			label7.TabIndex = 66;
			label7.Text = "To:";
			radioButtonArea.AutoSize = true;
			radioButtonArea.Location = new System.Drawing.Point(6, 116);
			radioButtonArea.Name = "radioButtonArea";
			radioButtonArea.Size = new System.Drawing.Size(50, 17);
			radioButtonArea.TabIndex = 62;
			radioButtonArea.Text = "Area:";
			radioButtonArea.UseVisualStyleBackColor = true;
			radioButtonArea.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(139, 142);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(33, 13);
			label8.TabIndex = 70;
			label8.Text = "From:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(295, 142);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(23, 13);
			label9.TabIndex = 71;
			label9.Text = "To:";
			radioButtonCountry.AutoSize = true;
			radioButtonCountry.Location = new System.Drawing.Point(6, 139);
			radioButtonCountry.Name = "radioButtonCountry";
			radioButtonCountry.Size = new System.Drawing.Size(64, 17);
			radioButtonCountry.TabIndex = 67;
			radioButtonCountry.Text = "Country:";
			radioButtonCountry.UseVisualStyleBackColor = true;
			radioButtonCountry.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxToArea.Assigned = false;
			comboBoxToArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToArea.CustomReportFieldName = "";
			comboBoxToArea.CustomReportKey = "";
			comboBoxToArea.CustomReportValueType = 1;
			comboBoxToArea.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToArea.DisplayLayout.Appearance = appearance;
			comboBoxToArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToArea.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToArea.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToArea.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToArea.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToArea.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToArea.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToArea.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToArea.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToArea.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToArea.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToArea.Editable = true;
			comboBoxToArea.Enabled = false;
			comboBoxToArea.FilterString = "";
			comboBoxToArea.HasAllAccount = false;
			comboBoxToArea.HasCustom = false;
			comboBoxToArea.IsDataLoaded = false;
			comboBoxToArea.Location = new System.Drawing.Point(321, 112);
			comboBoxToArea.MaxDropDownItems = 12;
			comboBoxToArea.Name = "comboBoxToArea";
			comboBoxToArea.ShowInactiveItems = false;
			comboBoxToArea.ShowQuickAdd = true;
			comboBoxToArea.Size = new System.Drawing.Size(103, 20);
			comboBoxToArea.TabIndex = 73;
			comboBoxToArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromArea.Assigned = false;
			comboBoxFromArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromArea.CustomReportFieldName = "";
			comboBoxFromArea.CustomReportKey = "";
			comboBoxFromArea.CustomReportValueType = 1;
			comboBoxFromArea.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromArea.DisplayLayout.Appearance = appearance13;
			comboBoxFromArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromArea.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromArea.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromArea.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromArea.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromArea.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromArea.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromArea.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromArea.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromArea.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromArea.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromArea.Editable = true;
			comboBoxFromArea.Enabled = false;
			comboBoxFromArea.FilterString = "";
			comboBoxFromArea.HasAllAccount = false;
			comboBoxFromArea.HasCustom = false;
			comboBoxFromArea.IsDataLoaded = false;
			comboBoxFromArea.Location = new System.Drawing.Point(183, 112);
			comboBoxFromArea.MaxDropDownItems = 12;
			comboBoxFromArea.Name = "comboBoxFromArea";
			comboBoxFromArea.ShowInactiveItems = false;
			comboBoxFromArea.ShowQuickAdd = true;
			comboBoxFromArea.Size = new System.Drawing.Size(112, 20);
			comboBoxFromArea.TabIndex = 72;
			comboBoxFromArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToCountry.Assigned = false;
			comboBoxToCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToCountry.CustomReportFieldName = "";
			comboBoxToCountry.CustomReportKey = "";
			comboBoxToCountry.CustomReportValueType = 1;
			comboBoxToCountry.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToCountry.DisplayLayout.Appearance = appearance25;
			comboBoxToCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCountry.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToCountry.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToCountry.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToCountry.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToCountry.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToCountry.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCountry.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToCountry.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToCountry.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToCountry.Editable = true;
			comboBoxToCountry.Enabled = false;
			comboBoxToCountry.FilterString = "";
			comboBoxToCountry.HasAllAccount = false;
			comboBoxToCountry.HasCustom = false;
			comboBoxToCountry.IsDataLoaded = false;
			comboBoxToCountry.Location = new System.Drawing.Point(321, 134);
			comboBoxToCountry.MaxDropDownItems = 12;
			comboBoxToCountry.Name = "comboBoxToCountry";
			comboBoxToCountry.ShowInactiveItems = false;
			comboBoxToCountry.ShowQuickAdd = true;
			comboBoxToCountry.Size = new System.Drawing.Size(103, 20);
			comboBoxToCountry.TabIndex = 69;
			comboBoxToCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromCountry.Assigned = false;
			comboBoxFromCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromCountry.CustomReportFieldName = "";
			comboBoxFromCountry.CustomReportKey = "";
			comboBoxFromCountry.CustomReportValueType = 1;
			comboBoxFromCountry.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromCountry.DisplayLayout.Appearance = appearance37;
			comboBoxFromCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCountry.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxFromCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxFromCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromCountry.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromCountry.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxFromCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromCountry.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromCountry.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxFromCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromCountry.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCountry.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxFromCountry.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxFromCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromCountry.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxFromCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxFromCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromCountry.Editable = true;
			comboBoxFromCountry.Enabled = false;
			comboBoxFromCountry.FilterString = "";
			comboBoxFromCountry.HasAllAccount = false;
			comboBoxFromCountry.HasCustom = false;
			comboBoxFromCountry.IsDataLoaded = false;
			comboBoxFromCountry.Location = new System.Drawing.Point(183, 134);
			comboBoxFromCountry.MaxDropDownItems = 12;
			comboBoxFromCountry.Name = "comboBoxFromCountry";
			comboBoxFromCountry.ShowInactiveItems = false;
			comboBoxFromCountry.ShowQuickAdd = true;
			comboBoxFromCountry.Size = new System.Drawing.Size(112, 20);
			comboBoxFromCountry.TabIndex = 68;
			comboBoxFromCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToGroup.Assigned = false;
			comboBoxToGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToGroup.CustomReportFieldName = "";
			comboBoxToGroup.CustomReportKey = "";
			comboBoxToGroup.CustomReportValueType = 1;
			comboBoxToGroup.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToGroup.DisplayLayout.Appearance = appearance49;
			comboBoxToGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxToGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxToGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToGroup.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToGroup.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToGroup.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxToGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToGroup.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxToGroup.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxToGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxToGroup.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxToGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxToGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToGroup.Editable = true;
			comboBoxToGroup.Enabled = false;
			comboBoxToGroup.FilterString = "";
			comboBoxToGroup.HasAllAccount = false;
			comboBoxToGroup.HasCustom = false;
			comboBoxToGroup.IsDataLoaded = false;
			comboBoxToGroup.Location = new System.Drawing.Point(321, 90);
			comboBoxToGroup.MaxDropDownItems = 12;
			comboBoxToGroup.Name = "comboBoxToGroup";
			comboBoxToGroup.ShowInactiveItems = false;
			comboBoxToGroup.ShowQuickAdd = true;
			comboBoxToGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxToGroup.TabIndex = 11;
			comboBoxToGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromGroup.Assigned = false;
			comboBoxFromGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromGroup.CustomReportFieldName = "";
			comboBoxFromGroup.CustomReportKey = "";
			comboBoxFromGroup.CustomReportValueType = 1;
			comboBoxFromGroup.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromGroup.DisplayLayout.Appearance = appearance61;
			comboBoxFromGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxFromGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromGroup.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxFromGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromGroup.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxFromGroup.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxFromGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromGroup.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxFromGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxFromGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromGroup.Editable = true;
			comboBoxFromGroup.Enabled = false;
			comboBoxFromGroup.FilterString = "";
			comboBoxFromGroup.HasAllAccount = false;
			comboBoxFromGroup.HasCustom = false;
			comboBoxFromGroup.IsDataLoaded = false;
			comboBoxFromGroup.Location = new System.Drawing.Point(183, 90);
			comboBoxFromGroup.MaxDropDownItems = 12;
			comboBoxFromGroup.Name = "comboBoxFromGroup";
			comboBoxFromGroup.ShowInactiveItems = false;
			comboBoxFromGroup.ShowQuickAdd = true;
			comboBoxFromGroup.Size = new System.Drawing.Size(112, 20);
			comboBoxFromGroup.TabIndex = 10;
			comboBoxFromGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToClass.Assigned = false;
			comboBoxToClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToClass.CustomReportFieldName = "";
			comboBoxToClass.CustomReportKey = "";
			comboBoxToClass.CustomReportValueType = 1;
			comboBoxToClass.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToClass.DisplayLayout.Appearance = appearance73;
			comboBoxToClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxToClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxToClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToClass.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToClass.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxToClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToClass.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxToClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToClass.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxToClass.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxToClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxToClass.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxToClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxToClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToClass.Editable = true;
			comboBoxToClass.Enabled = false;
			comboBoxToClass.FilterString = "";
			comboBoxToClass.HasAllAccount = false;
			comboBoxToClass.HasCustom = false;
			comboBoxToClass.IsDataLoaded = false;
			comboBoxToClass.Location = new System.Drawing.Point(321, 68);
			comboBoxToClass.MaxDropDownItems = 12;
			comboBoxToClass.Name = "comboBoxToClass";
			comboBoxToClass.ShowInactiveItems = false;
			comboBoxToClass.ShowQuickAdd = true;
			comboBoxToClass.Size = new System.Drawing.Size(103, 20);
			comboBoxToClass.TabIndex = 8;
			comboBoxToClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromClass.Assigned = false;
			comboBoxFromClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromClass.CustomReportFieldName = "";
			comboBoxFromClass.CustomReportKey = "";
			comboBoxFromClass.CustomReportValueType = 1;
			comboBoxFromClass.DescriptionTextBox = null;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromClass.DisplayLayout.Appearance = appearance85;
			comboBoxFromClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.GroupByBox.Appearance = appearance86;
			appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
			comboBoxFromClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance88.BackColor2 = System.Drawing.SystemColors.Control;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
			comboBoxFromClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromClass.DisplayLayout.Override.ActiveCellAppearance = appearance89;
			appearance90.BackColor = System.Drawing.SystemColors.Highlight;
			appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromClass.DisplayLayout.Override.ActiveRowAppearance = appearance90;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.CardAreaAppearance = appearance91;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromClass.DisplayLayout.Override.CellAppearance = appearance92;
			comboBoxFromClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromClass.DisplayLayout.Override.CellPadding = 0;
			appearance93.BackColor = System.Drawing.SystemColors.Control;
			appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance93.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.GroupByRowAppearance = appearance93;
			appearance94.TextHAlignAsString = "Left";
			comboBoxFromClass.DisplayLayout.Override.HeaderAppearance = appearance94;
			comboBoxFromClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromClass.DisplayLayout.Override.RowAppearance = appearance95;
			comboBoxFromClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
			comboBoxFromClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromClass.Editable = true;
			comboBoxFromClass.Enabled = false;
			comboBoxFromClass.FilterString = "";
			comboBoxFromClass.HasAllAccount = false;
			comboBoxFromClass.HasCustom = false;
			comboBoxFromClass.IsDataLoaded = false;
			comboBoxFromClass.Location = new System.Drawing.Point(183, 68);
			comboBoxFromClass.MaxDropDownItems = 12;
			comboBoxFromClass.Name = "comboBoxFromClass";
			comboBoxFromClass.ShowInactiveItems = false;
			comboBoxFromClass.ShowQuickAdd = true;
			comboBoxFromClass.Size = new System.Drawing.Size(112, 20);
			comboBoxFromClass.TabIndex = 7;
			comboBoxFromClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToCustomer.Assigned = false;
			comboBoxToCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToCustomer.CustomReportFieldName = "";
			comboBoxToCustomer.CustomReportKey = "";
			comboBoxToCustomer.CustomReportValueType = 1;
			comboBoxToCustomer.DescriptionTextBox = null;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToCustomer.DisplayLayout.Appearance = appearance97;
			comboBoxToCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance98.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance98.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCustomer.DisplayLayout.GroupByBox.Appearance = appearance98;
			appearance99.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance99;
			comboBoxToCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance100.BackColor2 = System.Drawing.SystemColors.Control;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance100;
			comboBoxToCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance101;
			appearance102.BackColor = System.Drawing.SystemColors.Highlight;
			appearance102.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance102;
			comboBoxToCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToCustomer.DisplayLayout.Override.CardAreaAppearance = appearance103;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			appearance104.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToCustomer.DisplayLayout.Override.CellAppearance = appearance104;
			comboBoxToCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance105.BackColor = System.Drawing.SystemColors.Control;
			appearance105.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance105.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance105.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance105;
			appearance106.TextHAlignAsString = "Left";
			comboBoxToCustomer.DisplayLayout.Override.HeaderAppearance = appearance106;
			comboBoxToCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.Color.Silver;
			comboBoxToCustomer.DisplayLayout.Override.RowAppearance = appearance107;
			comboBoxToCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance108;
			comboBoxToCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToCustomer.Editable = true;
			comboBoxToCustomer.Enabled = false;
			comboBoxToCustomer.FilterString = "";
			comboBoxToCustomer.FilterSysDocID = "";
			comboBoxToCustomer.HasAll = false;
			comboBoxToCustomer.HasCustom = false;
			comboBoxToCustomer.IsDataLoaded = false;
			comboBoxToCustomer.Location = new System.Drawing.Point(321, 46);
			comboBoxToCustomer.MaxDropDownItems = 12;
			comboBoxToCustomer.Name = "comboBoxToCustomer";
			comboBoxToCustomer.ShowConsignmentOnly = false;
			comboBoxToCustomer.ShowInactive = false;
			comboBoxToCustomer.ShowLPOCustomersOnly = false;
			comboBoxToCustomer.ShowPROCustomersOnly = false;
			comboBoxToCustomer.ShowQuickAdd = true;
			comboBoxToCustomer.Size = new System.Drawing.Size(103, 20);
			comboBoxToCustomer.TabIndex = 5;
			comboBoxToCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleCustomer.Assigned = false;
			comboBoxSingleCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleCustomer.CustomReportFieldName = "";
			comboBoxSingleCustomer.CustomReportKey = "";
			comboBoxSingleCustomer.CustomReportValueType = 1;
			comboBoxSingleCustomer.DescriptionTextBox = null;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleCustomer.DisplayLayout.Appearance = appearance109;
			comboBoxSingleCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance110.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance110.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleCustomer.DisplayLayout.GroupByBox.Appearance = appearance110;
			appearance111.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance111;
			comboBoxSingleCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance112.BackColor2 = System.Drawing.SystemColors.Control;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance112;
			comboBoxSingleCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance113;
			appearance114.BackColor = System.Drawing.SystemColors.Highlight;
			appearance114.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance114;
			comboBoxSingleCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleCustomer.DisplayLayout.Override.CardAreaAppearance = appearance115;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			appearance116.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleCustomer.DisplayLayout.Override.CellAppearance = appearance116;
			comboBoxSingleCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance117.BackColor = System.Drawing.SystemColors.Control;
			appearance117.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance117.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance117.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance117.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance117;
			appearance118.TextHAlignAsString = "Left";
			comboBoxSingleCustomer.DisplayLayout.Override.HeaderAppearance = appearance118;
			comboBoxSingleCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleCustomer.DisplayLayout.Override.RowAppearance = appearance119;
			comboBoxSingleCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance120;
			comboBoxSingleCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleCustomer.Editable = true;
			comboBoxSingleCustomer.FilterString = "";
			comboBoxSingleCustomer.FilterSysDocID = "";
			comboBoxSingleCustomer.HasAll = false;
			comboBoxSingleCustomer.HasCustom = false;
			comboBoxSingleCustomer.IsDataLoaded = false;
			comboBoxSingleCustomer.Location = new System.Drawing.Point(182, 2);
			comboBoxSingleCustomer.MaxDropDownItems = 12;
			comboBoxSingleCustomer.Name = "comboBoxSingleCustomer";
			comboBoxSingleCustomer.ShowConsignmentOnly = false;
			comboBoxSingleCustomer.ShowInactive = false;
			comboBoxSingleCustomer.ShowLPOCustomersOnly = false;
			comboBoxSingleCustomer.ShowPROCustomersOnly = false;
			comboBoxSingleCustomer.ShowQuickAdd = true;
			comboBoxSingleCustomer.Size = new System.Drawing.Size(241, 20);
			comboBoxSingleCustomer.TabIndex = 2;
			comboBoxSingleCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromCustomer.Assigned = false;
			comboBoxFromCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromCustomer.CustomReportFieldName = "";
			comboBoxFromCustomer.CustomReportKey = "";
			comboBoxFromCustomer.CustomReportValueType = 1;
			comboBoxFromCustomer.DescriptionTextBox = null;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromCustomer.DisplayLayout.Appearance = appearance121;
			comboBoxFromCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance122.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance122.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCustomer.DisplayLayout.GroupByBox.Appearance = appearance122;
			appearance123.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance123;
			comboBoxFromCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance124.BackColor2 = System.Drawing.SystemColors.Control;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance124;
			comboBoxFromCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance125;
			appearance126.BackColor = System.Drawing.SystemColors.Highlight;
			appearance126.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance126;
			comboBoxFromCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromCustomer.DisplayLayout.Override.CardAreaAppearance = appearance127;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			appearance128.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromCustomer.DisplayLayout.Override.CellAppearance = appearance128;
			comboBoxFromCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance129.BackColor = System.Drawing.SystemColors.Control;
			appearance129.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance129.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance129.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance129;
			appearance130.TextHAlignAsString = "Left";
			comboBoxFromCustomer.DisplayLayout.Override.HeaderAppearance = appearance130;
			comboBoxFromCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromCustomer.DisplayLayout.Override.RowAppearance = appearance131;
			comboBoxFromCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance132;
			comboBoxFromCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromCustomer.Editable = true;
			comboBoxFromCustomer.Enabled = false;
			comboBoxFromCustomer.FilterString = "";
			comboBoxFromCustomer.FilterSysDocID = "";
			comboBoxFromCustomer.HasAll = false;
			comboBoxFromCustomer.HasCustom = false;
			comboBoxFromCustomer.IsDataLoaded = false;
			comboBoxFromCustomer.Location = new System.Drawing.Point(183, 46);
			comboBoxFromCustomer.MaxDropDownItems = 12;
			comboBoxFromCustomer.Name = "comboBoxFromCustomer";
			comboBoxFromCustomer.ShowConsignmentOnly = false;
			comboBoxFromCustomer.ShowInactive = false;
			comboBoxFromCustomer.ShowLPOCustomersOnly = false;
			comboBoxFromCustomer.ShowPROCustomersOnly = false;
			comboBoxFromCustomer.ShowQuickAdd = true;
			comboBoxFromCustomer.Size = new System.Drawing.Size(112, 20);
			comboBoxFromCustomer.TabIndex = 4;
			comboBoxFromCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToArea);
			base.Controls.Add(comboBoxFromArea);
			base.Controls.Add(comboBoxToCountry);
			base.Controls.Add(comboBoxFromCountry);
			base.Controls.Add(label8);
			base.Controls.Add(label9);
			base.Controls.Add(radioButtonCountry);
			base.Controls.Add(label6);
			base.Controls.Add(label7);
			base.Controls.Add(radioButtonArea);
			base.Controls.Add(buttonMultiple);
			base.Controls.Add(radioButtonMultipleCustomer);
			base.Controls.Add(textBoxMultipleCustomers);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(comboBoxToGroup);
			base.Controls.Add(comboBoxFromGroup);
			base.Controls.Add(label5);
			base.Controls.Add(label1);
			base.Controls.Add(comboBoxToClass);
			base.Controls.Add(comboBoxFromClass);
			base.Controls.Add(radioButtonGroup);
			base.Controls.Add(radioButtonClass);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(label4);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(labelTo);
			base.Controls.Add(comboBoxToCustomer);
			base.Controls.Add(comboBoxSingleCustomer);
			base.Controls.Add(comboBoxFromCustomer);
			base.Name = "CustomerSelector";
			base.Size = new System.Drawing.Size(439, 170);
			base.Load += new System.EventHandler(CustomerSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCustomer).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
