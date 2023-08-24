using DevExpress.XtraWizard;
using Infragistics.Documents.Excel;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Utilities
{
	public class ImportFromExcelForm : Form
	{
		private DataSet importData = new DataSet();

		private Workbook workBook;

		private IContainer components;

		private Button button1;

		private WizardControl wizardControl1;

		private WelcomeWizardPage welcomeWizardPage1;

		private WizardPage wizardPageEntityType;

		private RadioButton radioButtonEmployee;

		private RadioButton radioButtonVendor;

		private RadioButton radioButtonProduct;

		private RadioButton radioButtonCustomer;

		private CompletionWizardPage completionWizardPage1;

		private WizardPage wizardPageBrowse;

		private Label label1;

		private TextBox textBoxFileName;

		private Button buttonBrowseFile;

		private OpenFileDialog openFileDialog1;

		private WizardPage wizardPageMapping;

		private DataEntryGrid dataGridMapping;

		private WizardPage wizardPageViewData;

		private DataGridList dataGridDataView;

		private RadioButton radioButtonAccount;

		public ImportFromExcelForm()
		{
			InitializeComponent();
		}

		private void ImportFromExcelForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridDataView.ApplyUIDesign();
				dataGridMapping.SetupUI();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void buttonBrowseFile_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "Microsoft Excel Files|*.xls;*.xlsx";
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				textBoxFileName.Text = openFileDialog1.FileName;
			}
		}

		private bool ValidateMapping()
		{
			string text = "";
			text = GetPrimaryKey();
			bool flag = false;
			foreach (UltraGridRow row in dataGridMapping.Rows)
			{
				if (row.Cells[1].Value.ToString() == text)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				ErrorHelper.ErrorMessage("One of the columns should be a primary key. Assign one of the columns to '" + text + "' data column.");
				return false;
			}
			bool flag2 = false;
			string nameFieldKey = GetNameFieldKey();
			if (nameFieldKey != "")
			{
				foreach (UltraGridRow row2 in dataGridMapping.Rows)
				{
					if (row2.Cells[1].Value.ToString() == nameFieldKey)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					ErrorHelper.ErrorMessage("One of the columns should be a name column. Assign one of the columns to '" + nameFieldKey + "' data column.");
					return false;
				}
			}
			List<string> list = new List<string>();
			foreach (UltraGridRow row3 in dataGridMapping.Rows)
			{
				if (row3.Cells[1].Value.ToString() != "")
				{
					if (list.Contains(row3.Cells[1].Value.ToString()))
					{
						ErrorHelper.ErrorMessage("One or more fields are mapped to same data field. Each data field should be mapped to one column only.\nColumn Name:" + row3.Cells[0].Value.ToString());
						return false;
					}
					list.Add(row3.Cells[1].Value.ToString());
				}
			}
			return true;
		}

		private string GetPrimaryKey()
		{
			string result = "";
			if (radioButtonCustomer.Checked)
			{
				result = "CustomerID";
			}
			else if (radioButtonEmployee.Checked)
			{
				result = "EmployeeID";
			}
			else if (radioButtonProduct.Checked)
			{
				result = "ProductID";
			}
			else if (radioButtonVendor.Checked)
			{
				result = "VendorID";
			}
			return result;
		}

		private string GetNameFieldKey()
		{
			string result = "";
			if (radioButtonCustomer.Checked)
			{
				result = "CustomerName";
			}
			else if (radioButtonEmployee.Checked)
			{
				result = "";
			}
			else if (radioButtonProduct.Checked)
			{
				result = "Description";
			}
			else if (radioButtonVendor.Checked)
			{
				result = "VendorName";
			}
			return result;
		}

		private bool ValidateData()
		{
			string primaryKey = GetPrimaryKey();
			List<string> list = new List<string>();
			foreach (UltraGridRow row in dataGridDataView.Rows)
			{
				if (list.Contains(row.Cells[primaryKey].Value.ToString()))
				{
					row.Cells[primaryKey].Appearance.BackColor = Color.Red;
					ErrorHelper.ErrorMessage("One or more rows have duplicated primary key. Each row must have a unique key.");
					return false;
				}
				list.Add(row.Cells[primaryKey].Value.ToString());
			}
			return true;
		}

		private void wizardControl1_NextClick(object sender, WizardCommandButtonClickEventArgs e)
		{
			checked
			{
				try
				{
					if (e.Page.Name == "wizardPageBrowse")
					{
						if (textBoxFileName.Text.Trim() == string.Empty)
						{
							ErrorHelper.InformationMessage("Please select an Excel file!");
							e.Handled = true;
						}
						else
						{
							workBook = Workbook.Load(textBoxFileName.Text, verifyExcel2007Xml: true);
							Worksheet worksheet = workBook.Worksheets[0];
							DataSet dataSet = new DataSet();
							dataSet.Tables.Add("Table");
							dataSet.Tables[0].Columns.Add("DBColumn");
							dataSet.Tables[0].Columns.Add("ExcelColumn");
							ValueList valueList = new ValueList();
							foreach (WorksheetCell item in (IEnumerable<WorksheetCell>)worksheet.Rows[0].Cells)
							{
								if (item.Value != null)
								{
									valueList.ValueListItems.Add(item.ColumnIndex, item.Value.ToString());
								}
							}
							dataGridMapping.DataSource = dataSet;
							if (radioButtonCustomer.Checked)
							{
								importData = DataImportHelper.CustomerImportFields;
								foreach (DataColumn column in importData.Tables[0].Columns)
								{
									dataSet.Tables[0].Rows.Add(column.ColumnName.ToString(), "");
								}
							}
							else if (radioButtonProduct.Checked)
							{
								importData = DataImportHelper.ProductImportFields;
								foreach (DataColumn column2 in importData.Tables[0].Columns)
								{
									dataSet.Tables[0].Rows.Add(column2.ColumnName.ToString(), "");
								}
							}
							else if (radioButtonVendor.Checked)
							{
								importData = DataImportHelper.VendorImportFields;
								foreach (DataColumn column3 in importData.Tables[0].Columns)
								{
									dataSet.Tables[0].Rows.Add(column3.ColumnName.ToString(), "");
								}
							}
							else if (radioButtonEmployee.Checked)
							{
								importData = DataImportHelper.EmployeeImportFields;
								foreach (DataColumn column4 in importData.Tables[0].Columns)
								{
									dataSet.Tables[0].Rows.Add(column4.ColumnName.ToString(), "");
								}
							}
							else if (radioButtonAccount.Checked)
							{
								importData = DataImportHelper.AccountImportFields;
								foreach (DataColumn column5 in importData.Tables[0].Columns)
								{
									dataSet.Tables[0].Rows.Add(column5.ColumnName.ToString(), "");
								}
							}
							dataGridMapping.DisplayLayout.Bands[0].Columns[1].ValueList = valueList;
							dataGridMapping.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
							dataGridMapping.DisplayLayout.Bands[0].Columns[1].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
							dataGridMapping.AllowAddNew = false;
							dataGridMapping.DisplayLayout.Bands[0].Columns[1].Header.Caption = "Excel Column";
							dataGridMapping.DisplayLayout.Bands[0].Columns[0].Header.Caption = "Database Column";
							foreach (UltraGridRow row in dataGridMapping.Rows)
							{
								string text = row.Cells[0].Value.ToString();
								foreach (ValueListItem valueListItem in valueList.ValueListItems)
								{
									if (valueListItem.DisplayText.ToLower() == text.ToLower())
									{
										row.Cells[1].Value = valueListItem.DataValue;
										break;
									}
								}
							}
						}
					}
					else if (e.Page.Name == "wizardPageMapping")
					{
						if (!ValidateMapping())
						{
							e.Handled = true;
						}
						else
						{
							foreach (WorksheetRow item2 in (IEnumerable<WorksheetRow>)workBook.Worksheets[0].Rows)
							{
								if (item2.Index != 0)
								{
									DataRow dataRow = importData.Tables[0].NewRow();
									foreach (UltraGridRow row2 in dataGridMapping.Rows)
									{
										if (row2.Cells[1].Value != null && !string.IsNullOrWhiteSpace(row2.Cells[1].Value.ToString()) && item2.Cells[row2.Index].GetText() != null)
										{
											if (importData.Tables[0].Columns[row2.Cells[0].Value.ToString()].DataType == typeof(DateTime) && !DateTime.TryParse(item2.Cells[row2.Index].GetText(), out DateTime _))
											{
												ErrorHelper.WarningMessage("Invalid date value : " + item2.Cells[row2.Index].GetText());
											}
											int index = int.Parse(row2.Cells[1].Value.ToString());
											dataRow[row2.Cells[0].Value.ToString()] = item2.Cells[index].GetText();
										}
									}
									importData.Tables[0].Rows.Add(dataRow);
								}
							}
							List<string> list = new List<string>();
							foreach (UltraGridRow row3 in dataGridMapping.Rows)
							{
								list.Add(row3.Cells[1].Value.ToString());
							}
							for (int i = 0; i < importData.Tables[0].Columns.Count; i++)
							{
								if (!list.Contains(importData.Tables[0].Columns[i].ColumnName))
								{
									importData.Tables[0].Columns.RemoveAt(i);
									i--;
								}
							}
							dataGridDataView.DataSource = importData;
						}
					}
					else if (e.Page.Name == "wizardPageViewData")
					{
						if (!ValidateData())
						{
							e.Handled = true;
						}
						else if (InsertRowsInDatabase())
						{
							ErrorHelper.InformationMessage("You have successfully imported " + dataGridDataView.Rows.Count.ToString() + " rows.");
						}
						else
						{
							e.Handled = true;
							ErrorHelper.ErrorMessage("Failed to insert selected rows. Please check your data.");
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private bool InsertRowsInDatabase()
		{
			try
			{
				bool flag = true;
				if (radioButtonCustomer.Checked)
				{
					CustomerData customerData = new CustomerData();
					DataTable customerTable = customerData.CustomerTable;
					foreach (UltraGridRow row in dataGridDataView.Rows)
					{
						DataRow dataRow = customerTable.NewRow();
						foreach (UltraGridColumn column in dataGridDataView.DisplayLayout.Bands[0].Columns)
						{
							dataRow[column.Key] = row.Cells[column.Key].Value.ToString();
						}
						customerTable.Rows.Add(dataRow);
					}
					flag &= Factory.CustomerSystem.CreateCustomer(customerData);
				}
				else if (radioButtonProduct.Checked)
				{
					ProductData productData = new ProductData();
					DataTable productTable = productData.ProductTable;
					foreach (UltraGridRow row2 in dataGridDataView.Rows)
					{
						DataRow dataRow2 = productTable.NewRow();
						foreach (UltraGridColumn column2 in dataGridDataView.DisplayLayout.Bands[0].Columns)
						{
							dataRow2[column2.Key] = row2.Cells[column2.Key].Value.ToString();
						}
						productTable.Rows.Add(dataRow2);
					}
					flag &= Factory.ProductSystem.CreateProduct(productData);
				}
				else if (radioButtonEmployee.Checked)
				{
					EmployeeData employeeData = new EmployeeData();
					DataTable employeeTable = employeeData.EmployeeTable;
					foreach (UltraGridRow row3 in dataGridDataView.Rows)
					{
						DataRow dataRow3 = employeeTable.NewRow();
						foreach (UltraGridColumn column3 in dataGridDataView.DisplayLayout.Bands[0].Columns)
						{
							dataRow3[column3.Key] = row3.Cells[column3.Key].Value.ToString();
						}
						employeeTable.Rows.Add(dataRow3);
					}
					flag &= Factory.EmployeeSystem.CreateEmployee(employeeData);
				}
				else if (radioButtonVendor.Checked)
				{
					VendorData vendorData = new VendorData();
					DataTable vendorTable = vendorData.VendorTable;
					foreach (UltraGridRow row4 in dataGridDataView.Rows)
					{
						DataRow dataRow4 = vendorTable.NewRow();
						foreach (UltraGridColumn column4 in dataGridDataView.DisplayLayout.Bands[0].Columns)
						{
							dataRow4[column4.Key] = row4.Cells[column4.Key].Value.ToString();
						}
						vendorTable.Rows.Add(dataRow4);
					}
					flag &= Factory.VendorSystem.CreateVendor(vendorData);
				}
				else if (radioButtonEmployee.Checked)
				{
					EmployeeData employeeData2 = new EmployeeData();
					DataTable employeeTable2 = employeeData2.EmployeeTable;
					foreach (UltraGridRow row5 in dataGridDataView.Rows)
					{
						DataRow dataRow5 = employeeTable2.NewRow();
						foreach (UltraGridColumn column5 in dataGridDataView.DisplayLayout.Bands[0].Columns)
						{
							dataRow5[column5.Key] = row5.Cells[column5.Key].Value.ToString();
						}
						employeeTable2.Rows.Add(dataRow5);
					}
					flag &= Factory.EmployeeSystem.CreateEmployee(employeeData2);
				}
				else if (radioButtonAccount.Checked)
				{
					CompanyAccountData companyAccountData = new CompanyAccountData();
					DataTable companyAccountTable = companyAccountData.CompanyAccountTable;
					foreach (UltraGridRow row6 in dataGridDataView.Rows)
					{
						DataRow dataRow6 = companyAccountTable.NewRow();
						foreach (UltraGridColumn column6 in dataGridDataView.DisplayLayout.Bands[0].Columns)
						{
							dataRow6[column6.Key] = row6.Cells[column6.Key].Value.ToString();
						}
						companyAccountTable.Rows.Add(dataRow6);
					}
					flag &= Factory.CompanyAccountSystem.CreateCompanyAccount(companyAccountData);
				}
				return flag;
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2627)
				{
					ErrorHelper.ErrorMessage("One or more of the rows have duplicated ID which already exist.\nYou cannot create a row with a duplicate ID. Please check your data and make sure that all the IDs are unique.");
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
		{
			Close();
		}

		private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to cancel the wizard?") == DialogResult.Yes)
			{
				Close();
			}
		}

		private void welcomeWizardPage1_PageValidating(object sender, WizardPageValidatingEventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Utilities.ImportFromExcelForm));
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
			button1 = new System.Windows.Forms.Button();
			wizardControl1 = new DevExpress.XtraWizard.WizardControl();
			welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
			wizardPageEntityType = new DevExpress.XtraWizard.WizardPage();
			radioButtonEmployee = new System.Windows.Forms.RadioButton();
			radioButtonVendor = new System.Windows.Forms.RadioButton();
			radioButtonProduct = new System.Windows.Forms.RadioButton();
			radioButtonCustomer = new System.Windows.Forms.RadioButton();
			completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
			wizardPageBrowse = new DevExpress.XtraWizard.WizardPage();
			label1 = new System.Windows.Forms.Label();
			textBoxFileName = new System.Windows.Forms.TextBox();
			buttonBrowseFile = new System.Windows.Forms.Button();
			wizardPageMapping = new DevExpress.XtraWizard.WizardPage();
			dataGridMapping = new Micromind.DataControls.DataEntryGrid();
			wizardPageViewData = new DevExpress.XtraWizard.WizardPage();
			dataGridDataView = new Micromind.UISupport.DataGridList(components);
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			radioButtonAccount = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)wizardControl1).BeginInit();
			wizardControl1.SuspendLayout();
			wizardPageEntityType.SuspendLayout();
			wizardPageBrowse.SuspendLayout();
			wizardPageMapping.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridMapping).BeginInit();
			wizardPageViewData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDataView).BeginInit();
			SuspendLayout();
			button1.Location = new System.Drawing.Point(547, 420);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(88, 26);
			button1.TabIndex = 1;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			wizardControl1.Controls.Add(welcomeWizardPage1);
			wizardControl1.Controls.Add(wizardPageEntityType);
			wizardControl1.Controls.Add(completionWizardPage1);
			wizardControl1.Controls.Add(wizardPageBrowse);
			wizardControl1.Controls.Add(wizardPageMapping);
			wizardControl1.Controls.Add(wizardPageViewData);
			wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			wizardControl1.Image = (System.Drawing.Image)resources.GetObject("wizardControl1.Image");
			wizardControl1.Location = new System.Drawing.Point(0, 0);
			wizardControl1.Name = "wizardControl1";
			wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[6]
			{
				welcomeWizardPage1,
				wizardPageEntityType,
				wizardPageBrowse,
				wizardPageMapping,
				wizardPageViewData,
				completionWizardPage1
			});
			wizardControl1.Size = new System.Drawing.Size(653, 455);
			wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(wizardControl1_CancelClick);
			wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(wizardControl1_FinishClick);
			wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(wizardControl1_NextClick);
			welcomeWizardPage1.IntroductionText = "This wizard helps you to import data from Microsoft Excel files.\r\n\r\nThe first row in y our excel files should be column names and other rows should be data rows.";
			welcomeWizardPage1.Name = "welcomeWizardPage1";
			welcomeWizardPage1.Size = new System.Drawing.Size(436, 322);
			welcomeWizardPage1.PageValidating += new DevExpress.XtraWizard.WizardPageValidatingEventHandler(welcomeWizardPage1_PageValidating);
			wizardPageEntityType.Controls.Add(radioButtonAccount);
			wizardPageEntityType.Controls.Add(radioButtonEmployee);
			wizardPageEntityType.Controls.Add(radioButtonVendor);
			wizardPageEntityType.Controls.Add(radioButtonProduct);
			wizardPageEntityType.Controls.Add(radioButtonCustomer);
			wizardPageEntityType.DescriptionText = "Select the entity you want to import from the list";
			wizardPageEntityType.Name = "wizardPageEntityType";
			wizardPageEntityType.Size = new System.Drawing.Size(621, 310);
			wizardPageEntityType.Text = "What do You Want to Import";
			radioButtonEmployee.AutoSize = true;
			radioButtonEmployee.Location = new System.Drawing.Point(35, 94);
			radioButtonEmployee.Name = "radioButtonEmployee";
			radioButtonEmployee.Size = new System.Drawing.Size(108, 17);
			radioButtonEmployee.TabIndex = 0;
			radioButtonEmployee.Text = "Import Employees";
			radioButtonEmployee.UseVisualStyleBackColor = true;
			radioButtonVendor.AutoSize = true;
			radioButtonVendor.Location = new System.Drawing.Point(35, 71);
			radioButtonVendor.Name = "radioButtonVendor";
			radioButtonVendor.Size = new System.Drawing.Size(96, 17);
			radioButtonVendor.TabIndex = 0;
			radioButtonVendor.Text = "Import Vendors";
			radioButtonVendor.UseVisualStyleBackColor = true;
			radioButtonProduct.AutoSize = true;
			radioButtonProduct.Location = new System.Drawing.Point(35, 48);
			radioButtonProduct.Name = "radioButtonProduct";
			radioButtonProduct.Size = new System.Drawing.Size(99, 17);
			radioButtonProduct.TabIndex = 0;
			radioButtonProduct.Text = "Import Products";
			radioButtonProduct.UseVisualStyleBackColor = true;
			radioButtonCustomer.AutoSize = true;
			radioButtonCustomer.Checked = true;
			radioButtonCustomer.Location = new System.Drawing.Point(35, 25);
			radioButtonCustomer.Name = "radioButtonCustomer";
			radioButtonCustomer.Size = new System.Drawing.Size(106, 17);
			radioButtonCustomer.TabIndex = 0;
			radioButtonCustomer.TabStop = true;
			radioButtonCustomer.Text = "Import Customers";
			radioButtonCustomer.UseVisualStyleBackColor = true;
			completionWizardPage1.Name = "completionWizardPage1";
			completionWizardPage1.Size = new System.Drawing.Size(436, 322);
			wizardPageBrowse.Controls.Add(label1);
			wizardPageBrowse.Controls.Add(textBoxFileName);
			wizardPageBrowse.Controls.Add(buttonBrowseFile);
			wizardPageBrowse.DescriptionText = "Select the excel file you want to import from";
			wizardPageBrowse.Name = "wizardPageBrowse";
			wizardPageBrowse.Size = new System.Drawing.Size(621, 310);
			wizardPageBrowse.Text = "Select Excel File";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(19, 62);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(93, 13);
			label1.TabIndex = 2;
			label1.Text = "Browse Excel File:";
			textBoxFileName.Location = new System.Drawing.Point(22, 78);
			textBoxFileName.Name = "textBoxFileName";
			textBoxFileName.ReadOnly = true;
			textBoxFileName.Size = new System.Drawing.Size(474, 20);
			textBoxFileName.TabIndex = 1;
			buttonBrowseFile.Location = new System.Drawing.Point(502, 77);
			buttonBrowseFile.Name = "buttonBrowseFile";
			buttonBrowseFile.Size = new System.Drawing.Size(24, 20);
			buttonBrowseFile.TabIndex = 0;
			buttonBrowseFile.Text = "...";
			buttonBrowseFile.UseVisualStyleBackColor = true;
			buttonBrowseFile.Click += new System.EventHandler(buttonBrowseFile_Click);
			wizardPageMapping.Controls.Add(dataGridMapping);
			wizardPageMapping.DescriptionText = "Map the columns from excel with database columns";
			wizardPageMapping.Name = "wizardPageMapping";
			wizardPageMapping.Size = new System.Drawing.Size(621, 310);
			wizardPageMapping.Text = "Data Column Mapping";
			dataGridMapping.AllowAddNew = false;
			dataGridMapping.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridMapping.DisplayLayout.Appearance = appearance;
			dataGridMapping.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridMapping.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMapping.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMapping.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridMapping.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMapping.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridMapping.DisplayLayout.MaxColScrollRegions = 1;
			dataGridMapping.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridMapping.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridMapping.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridMapping.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridMapping.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridMapping.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridMapping.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridMapping.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridMapping.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridMapping.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMapping.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridMapping.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridMapping.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridMapping.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridMapping.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridMapping.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridMapping.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridMapping.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridMapping.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridMapping.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridMapping.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridMapping.ExitEditModeOnLeave = false;
			dataGridMapping.Location = new System.Drawing.Point(3, 14);
			dataGridMapping.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridMapping.Name = "dataGridMapping";
			dataGridMapping.ShowDeleteMenu = true;
			dataGridMapping.ShowInsertMenu = true;
			dataGridMapping.ShowMoveRowsMenu = true;
			dataGridMapping.Size = new System.Drawing.Size(615, 296);
			dataGridMapping.TabIndex = 2;
			dataGridMapping.Text = "dataEntryGrid1";
			wizardPageViewData.Controls.Add(dataGridDataView);
			wizardPageViewData.DescriptionText = "Review your data before processing";
			wizardPageViewData.Name = "wizardPageViewData";
			wizardPageViewData.Size = new System.Drawing.Size(621, 310);
			wizardPageViewData.Text = "Review Your Data";
			dataGridDataView.AllowUnfittedView = false;
			dataGridDataView.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDataView.DisplayLayout.Appearance = appearance13;
			dataGridDataView.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDataView.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDataView.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDataView.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridDataView.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDataView.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridDataView.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDataView.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDataView.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDataView.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridDataView.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDataView.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridDataView.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDataView.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridDataView.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDataView.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDataView.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridDataView.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridDataView.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDataView.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridDataView.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridDataView.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDataView.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridDataView.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDataView.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDataView.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDataView.Location = new System.Drawing.Point(3, 3);
			dataGridDataView.Name = "dataGridDataView";
			dataGridDataView.ShowDeleteMenu = false;
			dataGridDataView.ShowMinusInRed = true;
			dataGridDataView.ShowNewMenu = false;
			dataGridDataView.Size = new System.Drawing.Size(622, 307);
			dataGridDataView.TabIndex = 0;
			dataGridDataView.Text = "dataGridList2";
			openFileDialog1.FileName = "openFileDialog1";
			radioButtonAccount.AutoSize = true;
			radioButtonAccount.Location = new System.Drawing.Point(35, 117);
			radioButtonAccount.Name = "radioButtonAccount";
			radioButtonAccount.Size = new System.Drawing.Size(142, 17);
			radioButtonAccount.TabIndex = 1;
			radioButtonAccount.Text = "Import Chart of Accounts";
			radioButtonAccount.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(653, 455);
			base.Controls.Add(wizardControl1);
			base.Controls.Add(button1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ImportFromExcelForm";
			Text = "Import Data";
			base.Load += new System.EventHandler(ImportFromExcelForm_Load);
			((System.ComponentModel.ISupportInitialize)wizardControl1).EndInit();
			wizardControl1.ResumeLayout(false);
			wizardPageEntityType.ResumeLayout(false);
			wizardPageEntityType.PerformLayout();
			wizardPageBrowse.ResumeLayout(false);
			wizardPageBrowse.PerformLayout();
			wizardPageMapping.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridMapping).EndInit();
			wizardPageViewData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridDataView).EndInit();
			ResumeLayout(false);
		}
	}
}
