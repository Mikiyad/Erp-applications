using DevExpress.XtraWizard;
using Infragistics.Documents.Excel;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class GridImportFromExcelForm : Form
	{
		private DataSet importData = new DataSet();

		private Workbook workBook;

		private IContainer components;

		private Button button1;

		private WizardControl wizardControl1;

		private WelcomeWizardPage welcomeWizardPage1;

		private CompletionWizardPage completionWizardPage1;

		private WizardPage wizardPageBrowse;

		private Label label1;

		private TextBox textBoxFileName;

		private Button buttonBrowseFile;

		private OpenFileDialog openFileDialog1;

		private WizardPage wizardPageMapping;

		private DataEntryGrid dataGridMapping;

		private CheckBox checkBoxLotItems;

		public DataSet ImportData => importData;

		public UltraGrid Grid
		{
			get;
			set;
		}

		public GridImportFromExcelForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.FormClosing += Form_FormClosing;
		}

		private void GridImportFromExcelForm_Load(object sender, EventArgs e)
		{
			try
			{
				importData = new DataSet();
				dataGridMapping.SetupUI();
				wizardControl1.SelectedPage = wizardPageBrowse;
				checkBoxLotItems.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "LotBased", defaultValue: false);
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

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			UserPreferences.SaveCurrentUserSetting(base.Name + "LotBased", checkBoxLotItems.Checked);
		}

		private bool ValidateMapping()
		{
			List<string> list = new List<string>();
			foreach (UltraGridRow row in dataGridMapping.Rows)
			{
				if (row.Cells[1].Value.ToString() != "")
				{
					if (list.Contains(row.Cells[1].Value.ToString()))
					{
						ErrorHelper.ErrorMessage("One or more fields are mapped to same data field. Each data field should be mapped to one column only.", "Column Name:", row.Cells[0].Value.ToString());
						return false;
					}
					list.Add(row.Cells[1].Value.ToString());
				}
			}
			return true;
		}

		private bool ValidateData()
		{
			return true;
		}

		private void wizardControl1_NextClick(object sender, WizardCommandButtonClickEventArgs e)
		{
			if (!(e.Page.Name == "wizardPageBrowse"))
			{
				return;
			}
			if (textBoxFileName.Text.Trim() == string.Empty)
			{
				ErrorHelper.InformationMessage("Please select an Excel file.");
				e.Handled = true;
				return;
			}
			workBook = Workbook.Load(textBoxFileName.Text, verifyExcel2007Xml: true);
			Worksheet worksheet = workBook.Worksheets[0];
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add("Table");
			dataSet.Tables[0].Columns.Add("DBColumn");
			dataSet.Tables[0].Columns.Add("ExcelColumn");
			DataTable dataTable = Grid.DataSource as DataTable;
			importData.Merge(dataTable);
			foreach (DataColumn column in importData.Tables[0].Columns)
			{
				if (!Grid.DisplayLayout.Bands[0].Columns[column.ColumnName].Hidden && Grid.DisplayLayout.Bands[0].Columns[column.ColumnName].CellActivation == Activation.AllowEdit)
				{
					dataSet.Tables[0].Rows.Add(column.ColumnName.ToString(), "");
				}
			}
			if (dataTable.Columns.Contains("LotNumber") && checkBoxLotItems.Checked)
			{
				dataTable.Columns.Remove("LotNumber");
				dataTable.Columns.Remove("BinID");
				dataTable.Columns.Remove("RackID");
				dataTable.Columns.Remove("Reference");
				dataTable.Columns.Remove("Reference2");
				dataTable.Columns.Remove("Location");
				dataTable.Columns.Remove("SourceLotNumber");
				dataTable.Columns.Remove("SoldQty");
				dataTable.Columns.Remove("Cost");
				dataTable.Columns.Remove("ProductionDate");
				dataTable.Columns.Remove("ExpiryDate");
			}
			if (!dataTable.Columns.Contains("LotNumber") && checkBoxLotItems.Checked)
			{
				dataSet.Tables[0].Rows.Add("LotNumber", "");
				dataSet.Tables[0].Rows.Add("BinID", "");
				dataSet.Tables[0].Rows.Add("RackID", "");
				dataSet.Tables[0].Rows.Add("Reference", "");
				dataSet.Tables[0].Rows.Add("Reference2", "");
				dataSet.Tables[0].Rows.Add("Location", "");
				dataSet.Tables[0].Rows.Add("SourceLotNumber", "");
				dataSet.Tables[0].Rows.Add("SoldQty", "");
				dataSet.Tables[0].Rows.Add("Cost", "");
				dataSet.Tables[0].Rows.Add("ProductionDate", "");
				dataSet.Tables[0].Rows.Add("ExpiryDate", "");
			}
			dataGridMapping.DataSource = dataSet;
			ValueList valueList = new ValueList();
			foreach (WorksheetCell item in (IEnumerable<WorksheetCell>)worksheet.Rows[0].Cells)
			{
				if (item.Value != null)
				{
					valueList.ValueListItems.Add(item.ColumnIndex, item.Value.ToString());
				}
			}
			dataGridMapping.DisplayLayout.Bands[0].Columns[1].ValueList = valueList;
			dataGridMapping.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
			dataGridMapping.DisplayLayout.Bands[0].Columns[1].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			dataGridMapping.AllowAddNew = false;
			dataGridMapping.DisplayLayout.Bands[0].Columns[0].Header.Caption = "Database Column";
			dataGridMapping.DisplayLayout.Bands[0].Columns[1].Header.Caption = "Excel Column";
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

		private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
		{
			string userMessage = "";
			if (!ValidateMapping())
			{
				base.DialogResult = DialogResult.None;
				e.Cancel = true;
			}
			else
			{
				try
				{
					Worksheet worksheet = workBook.Worksheets[0];
					importData.Tables[0].Rows.Clear();
					if (!ImportData.Tables[0].Columns.Contains("LotNumber") && checkBoxLotItems.Checked)
					{
						importData.Tables[0].Columns.Add("LotNumber");
						importData.Tables[0].Columns.Add("BinID");
						importData.Tables[0].Columns.Add("RackID");
						importData.Tables[0].Columns.Add("Reference");
						importData.Tables[0].Columns.Add("Reference2");
						importData.Tables[0].Columns.Add("SourceLotNumber");
						importData.Tables[0].Columns.Add("SoldQty");
						if (!importData.Tables[0].Columns.Contains("Location"))
						{
							importData.Tables[0].Columns.Add("Location");
						}
						importData.Tables[0].Columns.Add("ProductionDate");
						importData.Tables[0].Columns.Add("ExpiryDate");
						if (!importData.Tables[0].Columns.Contains("Cost"))
						{
							importData.Tables[0].Columns.Add("Cost");
						}
					}
					foreach (WorksheetRow item in (IEnumerable<WorksheetRow>)worksheet.Rows)
					{
						if (item.Index != 0)
						{
							DataRow dataRow = importData.Tables[0].NewRow();
							foreach (UltraGridRow row in dataGridMapping.Rows)
							{
								if (row.Cells[1].Value != null && !string.IsNullOrWhiteSpace(row.Cells[1].Value.ToString()))
								{
									item.Cells[row.Index].GetText();
									if (item.Cells[row.Index].GetText() != null)
									{
										int index = int.Parse(row.Cells[1].Value.ToString());
										item.Cells[index].GetText();
										if (importData.Tables[0].Columns[row.Cells[0].Value.ToString()].DataType == typeof(DateTime) && !string.IsNullOrEmpty(item.Cells[index].GetText()) && !DateTime.TryParse(item.Cells[index].GetText(), out DateTime _))
										{
											ErrorHelper.WarningMessage("Invalid date value:", item.Cells[row.Index].GetText());
										}
										userMessage = row.Cells[0].Value.ToString();
										if (importData.Tables[0].Columns[row.Cells[0].Value.ToString()].DataType == typeof(decimal))
										{
											if (!string.IsNullOrEmpty(item.Cells[index].GetText()))
											{
												dataRow[row.Cells[0].Value.ToString()] = decimal.Parse(item.Cells[index].Value.ToString());
											}
										}
										else if (!string.IsNullOrEmpty(item.Cells[index].GetText()))
										{
											dataRow[row.Cells[0].Value.ToString()] = item.Cells[index].GetText();
										}
									}
								}
							}
							if (!AreAllCellsEmpty(dataRow))
							{
								importData.Tables[0].Rows.Add(dataRow);
							}
						}
					}
					List<string> list = new List<string>();
					foreach (UltraGridRow row2 in dataGridMapping.Rows)
					{
						if (row2.Cells[1].Value != null && row2.Cells[1].Value.ToString() != "")
						{
							list.Add(row2.Cells[0].Value.ToString());
						}
					}
					for (int i = 0; i < importData.Tables[0].Columns.Count; i++)
					{
						if (!list.Contains(importData.Tables[0].Columns[i].ColumnName))
						{
							importData.Tables[0].Columns.RemoveAt(i);
							i--;
						}
					}
					if (ValidateData())
					{
						base.DialogResult = DialogResult.OK;
						Close();
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2, userMessage);
					e.Cancel = true;
					base.DialogResult = DialogResult.None;
				}
			}
		}

		public bool AreAllCellsEmpty(DataRow row)
		{
			return row.ItemArray?.All((object x) => string.IsNullOrWhiteSpace(x.ToString())) ?? true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.GridImportFromExcelForm));
			button1 = new System.Windows.Forms.Button();
			wizardControl1 = new DevExpress.XtraWizard.WizardControl();
			welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
			completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
			wizardPageBrowse = new DevExpress.XtraWizard.WizardPage();
			label1 = new System.Windows.Forms.Label();
			textBoxFileName = new System.Windows.Forms.TextBox();
			buttonBrowseFile = new System.Windows.Forms.Button();
			wizardPageMapping = new DevExpress.XtraWizard.WizardPage();
			dataGridMapping = new Micromind.DataControls.DataEntryGrid();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			checkBoxLotItems = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)wizardControl1).BeginInit();
			wizardControl1.SuspendLayout();
			wizardPageBrowse.SuspendLayout();
			wizardPageMapping.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridMapping).BeginInit();
			SuspendLayout();
			button1.Location = new System.Drawing.Point(547, 420);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(88, 26);
			button1.TabIndex = 1;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			wizardControl1.Controls.Add(welcomeWizardPage1);
			wizardControl1.Controls.Add(completionWizardPage1);
			wizardControl1.Controls.Add(wizardPageBrowse);
			wizardControl1.Controls.Add(wizardPageMapping);
			wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			wizardControl1.Location = new System.Drawing.Point(0, 0);
			wizardControl1.Name = "wizardControl1";
			wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[4]
			{
				welcomeWizardPage1,
				wizardPageBrowse,
				wizardPageMapping,
				completionWizardPage1
			});
			wizardControl1.Size = new System.Drawing.Size(653, 455);
			wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(wizardControl1_CancelClick);
			wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(wizardControl1_FinishClick);
			wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(wizardControl1_NextClick);
			welcomeWizardPage1.IntroductionText = "This wizard helps you to import data from Microsoft Excel files.\r\n\r\nThe first row in y our excel files should be column names and other rows should be data rows.";
			welcomeWizardPage1.Name = "welcomeWizardPage1";
			welcomeWizardPage1.Size = new System.Drawing.Size(436, 322);
			welcomeWizardPage1.Visible = false;
			welcomeWizardPage1.PageValidating += new DevExpress.XtraWizard.WizardPageValidatingEventHandler(welcomeWizardPage1_PageValidating);
			completionWizardPage1.Name = "completionWizardPage1";
			completionWizardPage1.Size = new System.Drawing.Size(436, 322);
			completionWizardPage1.Visible = false;
			wizardPageBrowse.AllowBack = false;
			wizardPageBrowse.Controls.Add(checkBoxLotItems);
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
			dataGridMapping.LoadLayoutFailed = false;
			dataGridMapping.Location = new System.Drawing.Point(3, 14);
			dataGridMapping.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridMapping.Name = "dataGridMapping";
			dataGridMapping.ShowClearMenu = true;
			dataGridMapping.ShowDeleteMenu = true;
			dataGridMapping.ShowInsertMenu = true;
			dataGridMapping.ShowMoveRowsMenu = true;
			dataGridMapping.Size = new System.Drawing.Size(615, 296);
			dataGridMapping.TabIndex = 2;
			dataGridMapping.Text = "dataEntryGrid1";
			openFileDialog1.FileName = "openFileDialog1";
			checkBoxLotItems.AutoSize = true;
			checkBoxLotItems.Location = new System.Drawing.Point(22, 122);
			checkBoxLotItems.Name = "checkBoxLotItems";
			checkBoxLotItems.Size = new System.Drawing.Size(69, 17);
			checkBoxLotItems.TabIndex = 3;
			checkBoxLotItems.Text = "Lot Items";
			checkBoxLotItems.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(653, 455);
			base.Controls.Add(wizardControl1);
			base.Controls.Add(button1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "GridImportFromExcelForm";
			Text = "Import Data";
			base.Load += new System.EventHandler(GridImportFromExcelForm_Load);
			((System.ComponentModel.ISupportInitialize)wizardControl1).EndInit();
			wizardControl1.ResumeLayout(false);
			wizardPageBrowse.ResumeLayout(false);
			wizardPageBrowse.PerformLayout();
			wizardPageMapping.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridMapping).EndInit();
			ResumeLayout(false);
		}
	}
}
