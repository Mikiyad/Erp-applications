using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class Customer1Selector : UserControl
	{
		private string fromCustomer = "";

		private string toCustomer = "";

		private string fromClass = "";

		private string toClass = "";

		private string fromGroup = "";

		private string toGroup = "";

		private string fromArea = "";

		private string toArea = "";

		private string fromCountry = "";

		private string toCountry = "";

		private DataComboType selectedCombo;

		private List<string> customerList = new List<string>();

		private List<string> classList = new List<string>();

		private List<string> groupList = new List<string>();

		private List<string> areaList = new List<string>();

		private List<string> countryList = new List<string>();

		private IContainer components;

		private Button buttonClass;

		private TextBox textBoxCustomerClass;

		private Button buttonGroup;

		private TextBox textBoxCustomerGroup;

		private Button buttonArea;

		private TextBox textBoxArea;

		private Button buttonCustomer;

		private TextBox textBoxCustomer;

		private Label label1;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label7;

		private Button buttonCountry;

		private TextBox textBoxCountry;

		public string FromRange
		{
			get
			{
				string result = "";
				switch (selectedCombo)
				{
				case DataComboType.Customer:
					result = fromCustomer;
					break;
				case DataComboType.CustomerClass:
					result = fromClass;
					break;
				case DataComboType.CustomerGroup:
					result = fromGroup;
					break;
				case DataComboType.Area:
					result = fromArea;
					break;
				case DataComboType.Country:
					result = fromCountry;
					break;
				}
				return result;
			}
			set
			{
				switch (selectedCombo)
				{
				case DataComboType.Vendor:
				case DataComboType.Item:
				case DataComboType.Accounts:
				case DataComboType.VendorGroup:
					break;
				case DataComboType.Customer:
					fromCustomer = value;
					break;
				case DataComboType.CustomerClass:
					fromClass = value;
					break;
				case DataComboType.CustomerGroup:
					fromGroup = value;
					break;
				case DataComboType.Area:
					fromArea = value;
					break;
				case DataComboType.Country:
					fromCountry = value;
					break;
				}
			}
		}

		public string ToRange
		{
			get
			{
				string result = "";
				switch (selectedCombo)
				{
				case DataComboType.Customer:
					result = toCustomer;
					break;
				case DataComboType.CustomerClass:
					result = toClass;
					break;
				case DataComboType.CustomerGroup:
					result = toGroup;
					break;
				case DataComboType.Area:
					result = toArea;
					break;
				case DataComboType.Country:
					result = toCountry;
					break;
				}
				return result;
			}
			set
			{
				switch (selectedCombo)
				{
				case DataComboType.Vendor:
				case DataComboType.Item:
				case DataComboType.Accounts:
				case DataComboType.VendorGroup:
					break;
				case DataComboType.Customer:
					toCustomer = value;
					break;
				case DataComboType.CustomerClass:
					toClass = value;
					break;
				case DataComboType.CustomerGroup:
					toGroup = value;
					break;
				case DataComboType.Area:
					toArea = value;
					break;
				case DataComboType.Country:
					toCountry = value;
					break;
				}
			}
		}

		public string FromCustomer
		{
			get
			{
				return fromCustomer;
			}
			set
			{
				fromCustomer = value;
			}
		}

		public string ToCustomer => toCustomer;

		public string FromClass => fromClass;

		public string ToClass => toClass;

		public string FromGroup => fromGroup;

		public string ToGroup => toGroup;

		public string FromCountry => fromCountry;

		public string ToCountry => toCountry;

		public string FromArea => fromArea;

		public string ToArea => toArea;

		public string Customers
		{
			get
			{
				if (customerList.Count > 0)
				{
					return "'" + string.Join("','", customerList) + "'";
				}
				return "";
			}
		}

		public string CustomerClass
		{
			get
			{
				if (classList.Count > 0)
				{
					return "'" + string.Join("','", classList) + "'";
				}
				return "";
			}
		}

		public string CustomerGroup
		{
			get
			{
				if (groupList.Count > 0)
				{
					return "'" + string.Join("','", groupList) + "'";
				}
				return "";
			}
		}

		public string Area
		{
			get
			{
				if (areaList.Count > 0)
				{
					return "'" + string.Join("','", areaList) + "'";
				}
				return "";
			}
		}

		public string Country
		{
			get
			{
				if (countryList.Count > 0)
				{
					return "'" + string.Join("','", countryList) + "'";
				}
				return "";
			}
		}

		public Customer1Selector()
		{
			InitializeComponent();
		}

		private void CustomerSelector_Load(object sender, EventArgs e)
		{
		}

		private string GetSelectedItems(DataComboType selectedComboType, DataSet data, List<string> selectedDocs)
		{
			string text = "";
			SelectDocumentDialogByRange selectDocumentDialogByRange = new SelectDocumentDialogByRange();
			selectDocumentDialogByRange.DataSource = data;
			selectDocumentDialogByRange.IsMultiSelect = true;
			selectDocumentDialogByRange.SelectedDocuments = selectedDocs;
			selectDocumentDialogByRange.SelectedCombo = selectedComboType;
			selectedCombo = selectedComboType;
			if (FromRange != "" || ToRange != "")
			{
				selectDocumentDialogByRange.RangeFrom = FromRange;
				selectDocumentDialogByRange.RangeTo = ToRange;
			}
			selectDocumentDialogByRange.Text = "Select " + Enum.GetName(typeof(DataComboType), selectedComboType);
			if (selectDocumentDialogByRange.ShowDialog(this) != DialogResult.OK)
			{
				text = ((!(selectDocumentDialogByRange.RangeFrom != "") && !(selectDocumentDialogByRange.RangeTo != "")) ? string.Join(",", selectedDocs) : (selectDocumentDialogByRange.RangeFrom + "...." + selectDocumentDialogByRange.RangeTo));
			}
			else if (selectDocumentDialogByRange.RangeFrom != "")
			{
				selectedCombo = selectedComboType;
				FromRange = selectDocumentDialogByRange.RangeFrom;
				ToRange = selectDocumentDialogByRange.RangeTo;
				text = selectDocumentDialogByRange.RangeFrom + "...." + selectDocumentDialogByRange.RangeTo;
				selectedDocs.Clear();
			}
			else
			{
				selectedDocs.Clear();
				foreach (UltraGridRow selectedRow in selectDocumentDialogByRange.SelectedRows)
				{
					string item = selectedRow.Cells["Code"].Value.ToString();
					selectedRow.Cells["Name"].Value.ToString();
					selectedDocs.Add(item);
				}
				text = string.Join(",", selectedDocs);
				FromRange = "";
				ToRange = "";
			}
			return text;
		}

		private void buttonCustomer_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.CustomerSystem.GetCustomerList();
			textBoxCustomer.Text = GetSelectedItems(DataComboType.Customer, dataSet, customerList);
		}

		private void buttonClass_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.CustomerClassSystem.GetCustomerClassComboList();
			textBoxCustomerClass.Text = GetSelectedItems(DataComboType.CustomerClass, dataSet, classList);
		}

		private void buttonGroup_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.CustomerGroupSystem.GetCustomerGroupComboList();
			textBoxCustomerGroup.Text = GetSelectedItems(DataComboType.CustomerGroup, dataSet, groupList);
		}

		private void buttonArea_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.AreaSystem.GetAreaComboList();
			textBoxArea.Text = GetSelectedItems(DataComboType.Area, dataSet, areaList);
		}

		private void buttonCountry_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.CountrySystem.GetCountryComboList();
			textBoxCountry.Text = GetSelectedItems(DataComboType.Country, dataSet, countryList);
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
			buttonClass = new System.Windows.Forms.Button();
			textBoxCustomerClass = new System.Windows.Forms.TextBox();
			buttonGroup = new System.Windows.Forms.Button();
			textBoxCustomerGroup = new System.Windows.Forms.TextBox();
			buttonArea = new System.Windows.Forms.Button();
			textBoxArea = new System.Windows.Forms.TextBox();
			buttonCustomer = new System.Windows.Forms.Button();
			textBoxCustomer = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			buttonCountry = new System.Windows.Forms.Button();
			textBoxCountry = new System.Windows.Forms.TextBox();
			SuspendLayout();
			buttonClass.Location = new System.Drawing.Point(319, 28);
			buttonClass.Name = "buttonClass";
			buttonClass.Size = new System.Drawing.Size(26, 21);
			buttonClass.TabIndex = 1;
			buttonClass.Text = "...";
			buttonClass.UseVisualStyleBackColor = true;
			buttonClass.Click += new System.EventHandler(buttonClass_Click);
			textBoxCustomerClass.Location = new System.Drawing.Point(103, 29);
			textBoxCustomerClass.Name = "textBoxCustomerClass";
			textBoxCustomerClass.ReadOnly = true;
			textBoxCustomerClass.Size = new System.Drawing.Size(214, 20);
			textBoxCustomerClass.TabIndex = 70;
			buttonGroup.Location = new System.Drawing.Point(319, 50);
			buttonGroup.Name = "buttonGroup";
			buttonGroup.Size = new System.Drawing.Size(26, 21);
			buttonGroup.TabIndex = 2;
			buttonGroup.Text = "...";
			buttonGroup.UseVisualStyleBackColor = true;
			buttonGroup.Click += new System.EventHandler(buttonGroup_Click);
			textBoxCustomerGroup.Location = new System.Drawing.Point(103, 51);
			textBoxCustomerGroup.Name = "textBoxCustomerGroup";
			textBoxCustomerGroup.ReadOnly = true;
			textBoxCustomerGroup.Size = new System.Drawing.Size(214, 20);
			textBoxCustomerGroup.TabIndex = 74;
			buttonArea.Location = new System.Drawing.Point(319, 72);
			buttonArea.Name = "buttonArea";
			buttonArea.Size = new System.Drawing.Size(26, 21);
			buttonArea.TabIndex = 3;
			buttonArea.Text = "...";
			buttonArea.UseVisualStyleBackColor = true;
			buttonArea.Click += new System.EventHandler(buttonArea_Click);
			textBoxArea.Location = new System.Drawing.Point(103, 73);
			textBoxArea.Name = "textBoxArea";
			textBoxArea.ReadOnly = true;
			textBoxArea.Size = new System.Drawing.Size(214, 20);
			textBoxArea.TabIndex = 76;
			buttonCustomer.Location = new System.Drawing.Point(319, 6);
			buttonCustomer.Name = "buttonCustomer";
			buttonCustomer.Size = new System.Drawing.Size(26, 21);
			buttonCustomer.TabIndex = 0;
			buttonCustomer.Text = "...";
			buttonCustomer.UseVisualStyleBackColor = true;
			buttonCustomer.Click += new System.EventHandler(buttonCustomer_Click);
			textBoxCustomer.Location = new System.Drawing.Point(103, 7);
			textBoxCustomer.Name = "textBoxCustomer";
			textBoxCustomer.ReadOnly = true;
			textBoxCustomer.Size = new System.Drawing.Size(214, 20);
			textBoxCustomer.TabIndex = 78;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(8, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(62, 13);
			label1.TabIndex = 80;
			label1.Text = "Customers :";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(8, 31);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(38, 13);
			label3.TabIndex = 83;
			label3.Text = "Class :";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(8, 76);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(35, 13);
			label4.TabIndex = 86;
			label4.Text = "Area :";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(8, 54);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(42, 13);
			label5.TabIndex = 85;
			label5.Text = "Group :";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(8, 98);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(49, 13);
			label7.TabIndex = 87;
			label7.Text = "Country :";
			buttonCountry.Location = new System.Drawing.Point(319, 94);
			buttonCountry.Name = "buttonCountry";
			buttonCountry.Size = new System.Drawing.Size(26, 21);
			buttonCountry.TabIndex = 4;
			buttonCountry.Text = "...";
			buttonCountry.UseVisualStyleBackColor = true;
			buttonCountry.Click += new System.EventHandler(buttonCountry_Click);
			textBoxCountry.Location = new System.Drawing.Point(103, 95);
			textBoxCountry.Name = "textBoxCountry";
			textBoxCountry.ReadOnly = true;
			textBoxCountry.Size = new System.Drawing.Size(214, 20);
			textBoxCountry.TabIndex = 88;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(buttonCountry);
			base.Controls.Add(textBoxCountry);
			base.Controls.Add(label7);
			base.Controls.Add(label4);
			base.Controls.Add(label5);
			base.Controls.Add(label3);
			base.Controls.Add(label1);
			base.Controls.Add(buttonCustomer);
			base.Controls.Add(textBoxCustomer);
			base.Controls.Add(buttonArea);
			base.Controls.Add(textBoxArea);
			base.Controls.Add(buttonGroup);
			base.Controls.Add(textBoxCustomerGroup);
			base.Controls.Add(buttonClass);
			base.Controls.Add(textBoxCustomerClass);
			base.Name = "Customer1Selector";
			base.Size = new System.Drawing.Size(354, 128);
			base.Load += new System.EventHandler(CustomerSelector_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
