using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls;
using Micromind.UISupport;
using Microsoft.Office.Interop.Excel;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Global = Micromind.ClientLibraries.Global;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class EmployeeSIFDialogForm : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private DataSet data;

		private string _period;

		private string _sysdocid = string.Empty;

		private string _voucherID = string.Empty;

		private string _molID = string.Empty;

		private string _routecode = string.Empty;

		private IContainer components;

		private Panel panelButtons;

		private UISupport.Line linePanelDown;

		private XPButton buttonCreate;

		private MMTextBox textBoxFileName;

		private XPButton buttonSelectTemplatePath;

		private MMLabel mmLabel7;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private MMLabel mmSponser;

		private SponsorComboBox comboBoxSponsor;

		private BankComboBox comboBoxBank;

		private System.Windows.Forms.TextBox textBoxVoucherNumber;

		private System.Windows.Forms.TextBox textBoxDoc;

		private RadioButton radioButtonTab;

		private RadioButton radioButtonComma;

		private MMLabel mmLabel3;

		private XPButton xpButton1;

		private MMLabel mmLabel4;

		private BackgroundWorker backgroundWorker1;

		private Panel panel1;

		private RadioButton radXl;

		private RadioButton radSif;

		public string Period
		{
			get
			{
				return _period;
			}
			set
			{
				string[] array = value.Split('-');
				int day = DateTime.DaysInMonth(int.Parse(array[1].ToString()), int.Parse(array[0].ToString()));
				StartDate = new DateTime(int.Parse(array[1].ToString()), int.Parse(array[0].ToString()), 1);
				EndDate = new DateTime(int.Parse(array[1].ToString()), int.Parse(array[0].ToString()), day);
				_period = value;
			}
		}

		private DateTime StartDate
		{
			get;
			set;
		}

		private DateTime EndDate
		{
			get;
			set;
		}

		public string SysDocID
		{
			private get
			{
				return _sysdocid;
			}
			set
			{
				_sysdocid = value;
			}
		}

		public string VoucherID
		{
			private get
			{
				return _voucherID;
			}
			set
			{
				_voucherID = value;
			}
		}

		public string MOLID
		{
			private get
			{
				return _molID;
			}
			set
			{
				_molID = value;
			}
		}

		public string RouteCode
		{
			private get
			{
				return _routecode;
			}
			set
			{
				_routecode = value;
			}
		}

		public event EventHandler ValidateSelection;

		public EmployeeSIFDialogForm()
		{
			InitializeComponent();
			base.Activated += SelectShortcutFormDialog_Activated;
		}

		private void dataGridItems_DoubleClick(object sender, EventArgs e)
		{
		}

		private void SelectShortcutFormDialog_Activated(object sender, EventArgs e)
		{
		}

		private void SelectItem()
		{
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
		}

		private void panelButtons_Paint(object sender, PaintEventArgs e)
		{
		}

		private void EmployeeSIFDialogForm_Load(object sender, EventArgs e)
		{
			try
			{
				textBoxDoc.Text = SysDocID;
				textBoxVoucherNumber.Text = VoucherID;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void SetData(DataSet _currentData)
		{
			data = _currentData;
		}

		public void LoadData()
		{
		}

		private void FillData()
		{
		}

		private void buttonSelectTemplatePath_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(comboBoxSponsor.SelectedID))
			{
				ErrorHelper.InformationMessage("Please select a Sponser.");
				return;
			}
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			string text = folderBrowserDialog.SelectedPath = Factory.DatabaseSystem.GetServerPath();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				string selectedPath = folderBrowserDialog.SelectedPath;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Sponsor", "MOLId", "SponsorID", comboBoxSponsor.SelectedID);
				if (fieldValue == null)
				{
					ErrorHelper.InformationMessage("Please select a valid MOLID.");
					return;
				}
				MOLID = fieldValue.ToString();
				textBoxFileName.Text = selectedPath + "\\" + fieldValue + DateTime.Now.ToString("yyMMddHHmmss") + ".SIF";
			}
		}

		private void buttonCreate_Click(object sender, EventArgs e)
		{
			if (radXl.Checked)
			{
				xpButton1_Click(sender, e);
			}
			else if (radSif.Checked)
			{
				try
				{
					if (string.IsNullOrWhiteSpace(textBoxFileName.Text))
					{
						ErrorHelper.InformationMessage("Please select a file path.");
					}
					else if (string.IsNullOrWhiteSpace(comboBoxBank.SelectedID))
					{
						ErrorHelper.InformationMessage("Please select a bank.");
					}
					else if (string.IsNullOrWhiteSpace(comboBoxSponsor.SelectedID))
					{
						ErrorHelper.InformationMessage("Please select a Sponser.");
					}
					else
					{
						object fieldValue = Factory.DatabaseSystem.GetFieldValue("Bank", "RoutingCode", "BankID", comboBoxBank.SelectedID);
						if (fieldValue == null || fieldValue.ToString() == string.Empty)
						{
							ErrorHelper.InformationMessage("Please specify BankRouteCode.");
						}
						else
						{
							DataView defaultView = data.Tables[1].DefaultView;
							defaultView.RowFilter = "SponsorID = '" + comboBoxSponsor.SelectedID + "'";
							DataSet dataSet = new DataSet();
                            System.Data.DataTable table = defaultView.ToTable();
							dataSet.Tables.Add(table);
							if (dataSet.Tables.Contains("SalarySheet_Detail") && dataSet.Tables["SalarySheet_Detail"].Rows.Count != 0)
							{
								string text = textBoxFileName.Text;
								if (!File.Exists(text))
								{
									StringBuilder stringBuilder = new StringBuilder();
									using (StreamWriter streamWriter = File.CreateText(text))
									{
										decimal num = default(decimal);
										int num2 = 0;
										string value = ",";
										if (radioButtonComma.Checked)
										{
											value = ",";
										}
										else if (radioButtonTab.Checked)
										{
											value = "\t";
										}
										foreach (DataRow row in dataSet.Tables["SalarySheet_Detail"].Rows)
										{
											stringBuilder.Append("EDR");
											stringBuilder.Append(value);
											stringBuilder.Append(row["LabourID"].ToString());
											stringBuilder.Append(value);
											stringBuilder.Append(row["BankRouteCode"].ToString());
											stringBuilder.Append(value);
											stringBuilder.Append(row["IBAN"].ToString());
											stringBuilder.Append(value);
											stringBuilder.Append(StartDate.ToString("yyyy-MM-dd"));
											stringBuilder.Append(",");
											stringBuilder.Append(EndDate.ToString("yyyy-MM-dd"));
											stringBuilder.Append(value);
											string text2 = row["WorkDays"].ToString();
											text2 = text2.Substring(0, text2.IndexOf('.', 0));
											stringBuilder.Append(text2);
											stringBuilder.Append(value);
											text2 = row["NetSalary"].ToString();
											text2 = text2.Substring(0, text2.IndexOf('.', 0));
											stringBuilder.Append(text2);
											stringBuilder.Append(value);
											stringBuilder.Append("0");
											stringBuilder.Append(value);
											text2 = row["Absent"].ToString();
											text2 = text2.Substring(0, text2.IndexOf('.', 0));
											stringBuilder.Append(text2);
											stringBuilder.Append(Environment.NewLine);
											num2 = checked(num2 + 1);
											num += decimal.Parse(row["NetSalary"].ToString());
										}
										stringBuilder.Append("SCR");
										stringBuilder.Append(value);
										stringBuilder.Append(MOLID);
										stringBuilder.Append(value);
										stringBuilder.Append(fieldValue.ToString());
										stringBuilder.Append(value);
										stringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd"));
										stringBuilder.Append(value);
										stringBuilder.Append(DateTime.Now.ToString("HHmm"));
										stringBuilder.Append(value);
										stringBuilder.Append(Period);
										stringBuilder.Append(value);
										stringBuilder.Append(num2.ToString());
										stringBuilder.Append(value);
										stringBuilder.Append($"{num:n}".Replace(",", ""));
										stringBuilder.Append(value);
										stringBuilder.Append(Global.BaseCurrencyID);
										stringBuilder.Append(value);
										stringBuilder.Append("PAC");
										stringBuilder.Append(value);
										stringBuilder.Append(EndDate.ToString("MMM", CultureInfo.InvariantCulture).ToUpper());
										streamWriter.Write(stringBuilder.ToString());
										ErrorHelper.InformationMessage("File created successfully!");
									}
								}
							}
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void CreateExcel()
		{
			Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
			if (application == null)
			{
				MessageBox.Show("Excel is not properly installed!!");
				return;
			}
			object value = Missing.Value;
			Workbook workbook = application.Workbooks.Add(value);
			checked
			{
				Worksheet worksheet = (Worksheet)(dynamic)workbook.Worksheets.get_Item((object)1);
				for (int i = 1; i < 20; i++)
				{
					((dynamic)worksheet.Columns[i, Type.Missing]).NumberFormat = "@";
					((dynamic)worksheet.Columns[i, Type.Missing]).ColumnWidth = 16;
				}
				((dynamic)worksheet.Columns[4, Type.Missing]).ColumnWidth = 30;
				try
				{
					if (string.IsNullOrWhiteSpace(textBoxFileName.Text))
					{
						ErrorHelper.InformationMessage("Please select a file path.");
					}
					else if (string.IsNullOrWhiteSpace(comboBoxBank.SelectedID))
					{
						ErrorHelper.InformationMessage("Please select a bank.");
					}
					else if (string.IsNullOrWhiteSpace(comboBoxSponsor.SelectedID))
					{
						ErrorHelper.InformationMessage("Please select a Sponser.");
					}
					else
					{
						object fieldValue = Factory.DatabaseSystem.GetFieldValue("Bank", "RoutingCode", "BankID", comboBoxBank.SelectedID);
						if (fieldValue == null || fieldValue.ToString() == string.Empty)
						{
							ErrorHelper.InformationMessage("Please specify BankRouteCode.");
						}
						else
						{
							DataView defaultView = data.Tables[1].DefaultView;
							defaultView.RowFilter = "SponsorID = '" + comboBoxSponsor.SelectedID + "'";
							DataSet dataSet = new DataSet();
                            System.Data.DataTable table = defaultView.ToTable();
							dataSet.Tables.Add(table);
							if (dataSet.Tables.Contains("SalarySheet_Detail") && dataSet.Tables["SalarySheet_Detail"].Rows.Count != 0)
							{
								string text = textBoxFileName.Text;
								if (!File.Exists(text))
								{
									new StringBuilder();
									using (File.CreateText(text))
									{
										decimal num = default(decimal);
										int num2 = 0;
										if (!radioButtonComma.Checked)
										{
											_ = radioButtonTab.Checked;
										}
										int num3 = 0;
										int count = dataSet.Tables["SalarySheet_Detail"].Rows.Count;
										for (int j = 1; j <= count; j++)
										{
											DataRow dataRow = dataSet.Tables["SalarySheet_Detail"].Rows[j - 1];
											worksheet.Cells[j, 1] = "EDR";
											worksheet.Cells[j, 2] = dataRow["LabourID"].ToString();
											worksheet.Cells[j, 3] = dataRow["BankRouteCode"].ToString();
											worksheet.Cells[j, 4] = dataRow["IBAN"].ToString();
											worksheet.Cells[j, 5] = StartDate.ToString("yyyy-MM-dd");
											worksheet.Cells[j, 6] = EndDate.ToString("yyyy-MM-dd");
											string text2 = dataRow["WorkDays"].ToString();
											text2 = text2.Substring(0, text2.IndexOf('.', 0));
											worksheet.Cells[j, 7] = text2;
											text2 = dataRow["NetSalary"].ToString();
											text2 = text2.Substring(0, text2.IndexOf('.', 0));
											worksheet.Cells[j, 8] = text2 + ".00";
											worksheet.Cells[j, 9] = "0.00";
											text2 = dataRow["Absent"].ToString();
											text2 = text2.Substring(0, text2.IndexOf('.', 0));
											worksheet.Cells[j, 10] = text2;
											((dynamic)worksheet.Cells.Style).WrapText = true;
											num2++;
											num += decimal.Parse(dataRow["NetSalary"].ToString());
											num3 = j;
										}
										worksheet.Cells[num3 + 1, 1] = "SCR";
										worksheet.Cells[num3 + 1, 2] = MOLID;
										worksheet.Cells[num3 + 1, 3] = fieldValue.ToString();
										worksheet.Cells[num3 + 1, 4] = DateTime.Now.ToString("yyyy-MM-dd");
										worksheet.Cells[num3 + 1, 5] = DateTime.Now.ToString("HHmm");
										worksheet.Cells[num3 + 1, 6] = Period;
										worksheet.Cells[num3 + 1, 7] = num2.ToString();
										worksheet.Cells[num3 + 1, 8] = $"{num:n}".Replace(",", "");
										worksheet.Cells[num3 + 1, 9] = Global.BaseCurrencyID;
										worksheet.Cells[num3 + 1, 10] = "AP";
										object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("Users", "EmployeeID", "UserID", Global.CurrentUser);
										object fieldValue3 = Factory.DatabaseSystem.GetFieldValue("Employee_Address", "Email", "EmployeeID", fieldValue2);
										if (fieldValue3 != null)
										{
											worksheet.Cells[num3 + 1, 11] = fieldValue3.ToString();
										}
										for (int k = 1; k < 20; k++)
										{
											((dynamic)worksheet.Columns[k, Type.Missing]).NumberFormat = "@";
											((dynamic)worksheet.Columns[k, Type.Missing]).ColumnWidth = 16;
										}
										((dynamic)worksheet.Columns[4, Type.Missing]).ColumnWidth = 30;
										worksheet.Name = MOLID + DateTime.Now.ToString("yyMMddHHmmss");
										workbook.SaveAs(text + ".xls", XlFileFormat.xlWorkbookNormal, value, value, value, value, XlSaveAsAccessMode.xlExclusive, value, value, value, value, value);
										workbook.Close(true, value, value);
										application.Quit();
										Marshal.ReleaseComObject(worksheet);
										Marshal.ReleaseComObject(workbook);
										Marshal.ReleaseComObject(application);
										MessageBox.Show("Excel file created , you can find the file " + text);
									}
								}
							}
						}
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			CreateExcel();
		}

		private void radXl_CheckedChanged(object sender, EventArgs e)
		{
			if (radXl.Checked)
			{
				RadioButton radioButton = radioButtonTab;
				bool enabled = radioButtonComma.Enabled = false;
				radioButton.Enabled = enabled;
			}
			else
			{
				RadioButton radioButton2 = radioButtonTab;
				bool enabled = radioButtonComma.Enabled = true;
				radioButton2.Enabled = enabled;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeSIFDialogForm));
			panelButtons = new System.Windows.Forms.Panel();
			xpButton1 = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCreate = new Micromind.UISupport.XPButton();
			textBoxFileName = new Micromind.UISupport.MMTextBox();
			buttonSelectTemplatePath = new Micromind.UISupport.XPButton();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmSponser = new Micromind.UISupport.MMLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxDoc = new System.Windows.Forms.TextBox();
			radioButtonTab = new System.Windows.Forms.RadioButton();
			radioButtonComma = new System.Windows.Forms.RadioButton();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			comboBoxBank = new Micromind.DataControls.BankComboBox();
			comboBoxSponsor = new Micromind.DataControls.SponsorComboBox();
			panel1 = new System.Windows.Forms.Panel();
			radXl = new System.Windows.Forms.RadioButton();
			radSif = new System.Windows.Forms.RadioButton();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCreate);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 153);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(487, 40);
			panelButtons.TabIndex = 3;
			panelButtons.Paint += new System.Windows.Forms.PaintEventHandler(panelButtons_Paint);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(195, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 15;
			xpButton1.Text = "&Create";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Visible = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(487, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCreate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCreate.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCreate.BackColor = System.Drawing.Color.DarkGray;
			buttonCreate.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCreate.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
			buttonCreate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCreate.Location = new System.Drawing.Point(377, 8);
			buttonCreate.Name = "buttonCreate";
			buttonCreate.Size = new System.Drawing.Size(96, 24);
			buttonCreate.TabIndex = 4;
			buttonCreate.Text = "&Create";
			buttonCreate.UseVisualStyleBackColor = false;
			buttonCreate.Click += new System.EventHandler(buttonCreate_Click);
			textBoxFileName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFileName.CustomReportFieldName = "";
			textBoxFileName.CustomReportKey = "";
			textBoxFileName.CustomReportValueType = 1;
			textBoxFileName.IsComboTextBox = false;
			textBoxFileName.Location = new System.Drawing.Point(72, 90);
			textBoxFileName.MaxLength = 1000;
			textBoxFileName.Name = "textBoxFileName";
			textBoxFileName.ReadOnly = true;
			textBoxFileName.Size = new System.Drawing.Size(344, 20);
			textBoxFileName.TabIndex = 15;
			textBoxFileName.TabStop = false;
			buttonSelectTemplatePath.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectTemplatePath.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectTemplatePath.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectTemplatePath.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectTemplatePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectTemplatePath.Location = new System.Drawing.Point(419, 89);
			buttonSelectTemplatePath.Name = "buttonSelectTemplatePath";
			buttonSelectTemplatePath.Size = new System.Drawing.Size(25, 22);
			buttonSelectTemplatePath.TabIndex = 2;
			buttonSelectTemplatePath.Text = "...";
			buttonSelectTemplatePath.UseVisualStyleBackColor = false;
			buttonSelectTemplatePath.Click += new System.EventHandler(buttonSelectTemplatePath_Click);
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(6, 95);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(26, 13);
			mmLabel7.TabIndex = 16;
			mmLabel7.Text = "File:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(3, 23);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(67, 13);
			mmLabel1.TabIndex = 17;
			mmLabel1.Text = "Salary Sheet";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(6, 71);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(38, 13);
			mmLabel2.TabIndex = 18;
			mmLabel2.Text = "Bank :";
			mmSponser.AutoSize = true;
			mmSponser.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmSponser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmSponser.IsFieldHeader = false;
			mmSponser.IsRequired = false;
			mmSponser.Location = new System.Drawing.Point(5, 47);
			mmSponser.Name = "mmSponser";
			mmSponser.PenWidth = 1f;
			mmSponser.ShowBorder = false;
			mmSponser.Size = new System.Drawing.Size(52, 13);
			mmSponser.TabIndex = 19;
			mmSponser.Text = "Sponser :";
			textBoxVoucherNumber.Location = new System.Drawing.Point(155, 20);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.ReadOnly = true;
			textBoxVoucherNumber.Size = new System.Drawing.Size(119, 20);
			textBoxVoucherNumber.TabIndex = 25;
			textBoxDoc.Location = new System.Drawing.Point(73, 20);
			textBoxDoc.MaxLength = 15;
			textBoxDoc.Name = "textBoxDoc";
			textBoxDoc.ReadOnly = true;
			textBoxDoc.Size = new System.Drawing.Size(77, 20);
			textBoxDoc.TabIndex = 26;
			radioButtonTab.AutoSize = true;
			radioButtonTab.Location = new System.Drawing.Point(143, 116);
			radioButtonTab.Name = "radioButtonTab";
			radioButtonTab.Size = new System.Drawing.Size(44, 17);
			radioButtonTab.TabIndex = 4;
			radioButtonTab.Text = "Tab";
			radioButtonTab.UseVisualStyleBackColor = true;
			radioButtonComma.AutoSize = true;
			radioButtonComma.Checked = true;
			radioButtonComma.Location = new System.Drawing.Point(73, 116);
			radioButtonComma.Name = "radioButtonComma";
			radioButtonComma.Size = new System.Drawing.Size(60, 17);
			radioButtonComma.TabIndex = 3;
			radioButtonComma.TabStop = true;
			radioButtonComma.Text = "Comma";
			radioButtonComma.UseVisualStyleBackColor = true;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(6, 117);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(56, 13);
			mmLabel3.TabIndex = 29;
			mmLabel3.Text = "Seperator:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(3, 6);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(50, 13);
			mmLabel4.TabIndex = 32;
			mmLabel4.Text = "Save As:";
			comboBoxBank.Assigned = false;
			comboBoxBank.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBank.CustomReportFieldName = "";
			comboBoxBank.CustomReportKey = "";
			comboBoxBank.CustomReportValueType = 1;
			comboBoxBank.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBank.DisplayLayout.Appearance = appearance;
			comboBoxBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBank.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBank.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBank.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBank.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxBank.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxBank.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxBank.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBank.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBank.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBank.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBank.Editable = true;
			comboBoxBank.FilterString = "";
			comboBoxBank.HasAllAccount = false;
			comboBoxBank.HasCustom = false;
			comboBoxBank.IsDataLoaded = false;
			comboBoxBank.Location = new System.Drawing.Point(72, 67);
			comboBoxBank.MaxDropDownItems = 12;
			comboBoxBank.Name = "comboBoxBank";
			comboBoxBank.ShowInactiveItems = false;
			comboBoxBank.ShowQuickAdd = true;
			comboBoxBank.Size = new System.Drawing.Size(156, 20);
			comboBoxBank.TabIndex = 1;
			comboBoxBank.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSponsor.Assigned = false;
			comboBoxSponsor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSponsor.CustomReportFieldName = "";
			comboBoxSponsor.CustomReportKey = "";
			comboBoxSponsor.CustomReportValueType = 1;
			comboBoxSponsor.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSponsor.DisplayLayout.Appearance = appearance13;
			comboBoxSponsor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSponsor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxSponsor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxSponsor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSponsor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSponsor.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSponsor.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSponsor.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxSponsor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSponsor.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxSponsor.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxSponsor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSponsor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxSponsor.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxSponsor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSponsor.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxSponsor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSponsor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSponsor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSponsor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSponsor.Editable = true;
			comboBoxSponsor.FilterString = "";
			comboBoxSponsor.HasAllAccount = false;
			comboBoxSponsor.HasCustom = false;
			comboBoxSponsor.IsDataLoaded = false;
			comboBoxSponsor.Location = new System.Drawing.Point(73, 44);
			comboBoxSponsor.MaxDropDownItems = 12;
			comboBoxSponsor.Name = "comboBoxSponsor";
			comboBoxSponsor.ShowInactiveItems = false;
			comboBoxSponsor.ShowQuickAdd = true;
			comboBoxSponsor.Size = new System.Drawing.Size(155, 20);
			comboBoxSponsor.TabIndex = 0;
			comboBoxSponsor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panel1.Controls.Add(radXl);
			panel1.Controls.Add(mmLabel4);
			panel1.Controls.Add(radSif);
			panel1.Location = new System.Drawing.Point(316, 119);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(159, 27);
			panel1.TabIndex = 135;
			radXl.AutoSize = true;
			radXl.Location = new System.Drawing.Point(113, 4);
			radXl.Name = "radXl";
			radXl.Size = new System.Drawing.Size(37, 17);
			radXl.TabIndex = 1;
			radXl.Text = "xls";
			radXl.UseVisualStyleBackColor = true;
			radSif.AutoSize = true;
			radSif.Checked = true;
			radSif.Location = new System.Drawing.Point(64, 4);
			radSif.Name = "radSif";
			radSif.Size = new System.Drawing.Size(41, 17);
			radSif.TabIndex = 0;
			radSif.TabStop = true;
			radSif.Text = "SIF";
			radSif.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCreate;
			base.ClientSize = new System.Drawing.Size(487, 193);
			base.Controls.Add(panel1);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(radioButtonTab);
			base.Controls.Add(radioButtonComma);
			base.Controls.Add(textBoxDoc);
			base.Controls.Add(textBoxVoucherNumber);
			base.Controls.Add(comboBoxBank);
			base.Controls.Add(comboBoxSponsor);
			base.Controls.Add(mmSponser);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(textBoxFileName);
			base.Controls.Add(buttonSelectTemplatePath);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "EmployeeSIFDialogForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Create Employee SIF";
			base.Load += new System.EventHandler(EmployeeSIFDialogForm_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxBank).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
