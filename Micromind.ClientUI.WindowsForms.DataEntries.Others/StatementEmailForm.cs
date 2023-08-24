using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class StatementEmailForm : Form
	{
		private DataSet currentData;

		private DateTime periodFrom = DateTime.Now;

		private DateTime periodTo = DateTime.Now;

		private EmailMessageData currentMessageData;

		private EmailFormDesigns formDesign = EmailFormDesigns.General;

		private IContainer components;

		private TextBox textBox1;

		private Label label1;

		private Button buttonEmail;

		private Button buttonStop;

		private Panel panel1;

		private Line line1;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem microsoftExcelToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonAutoFit;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator3;

		private ImageList imageList1;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private Label labelTitle;

		private DataGridList dataGridList;

		private UltraTabPageControl tabPageDetails;

		private Label label3;

		private DataGridList dataGridLog;

		private CheckBox checkBoxShowSent;

		public EmailFormDesigns Design
		{
			get
			{
				return formDesign;
			}
			set
			{
				formDesign = value;
			}
		}

		public StatementEmailForm()
		{
			InitializeComponent();
			base.Load += StatementEmailForm_Load;
			dataGridList.ClickCellButton += dataGridList_ClickCellButton;
			dataGridList.CellChange += dataGridList_AfterCellUpdate;
			dataGridLog.ApplyUIDesign();
			ultraTabControl1.SelectedTabChanged += ultraTabControl1_SelectedTabChanged;
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
			if (ultraTabControl1.SelectedTab.Key == "Log")
			{
				LoadLog();
			}
		}

		private void dataGridList_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Include")
			{
				CalculateCount();
			}
		}

		private void CalculateCount()
		{
			int num = 0;
			foreach (UltraGridRow row in dataGridList.Rows)
			{
				bool flag = false;
				if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(row.Cells["Include"].Value))
				{
					flag = bool.Parse(row.Cells["Include"].Value.ToString());
				}
				if (flag)
				{
					num = checked(num + 1);
				}
			}
			textBox1.Text = num.ToString();
		}

		private void StatementEmailForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupGrid();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridList_ClickCellButton(object sender, CellEventArgs e)
		{
			try
			{
				UltraGridRow row = e.Cell.Row;
				if (row != null && row.IsDataRow)
				{
					string str = dataGridList.ActiveRow.Cells["CustomerID"].Value.ToString();
					DataSet dataSet = currentData.Copy();
					dataSet.Tables["Customer"].Rows.Clear();
					dataSet.Tables["Customer"].Rows.Add(currentData.Tables[0].Select("[Customer Code] = '" + str + "'")[0].ItemArray);
					XtraReport printDocument = PrintHelper.GetPrintDocument(dataSet, "Customer Statement", "", SysDocTypes.None);
					DocumentViewForm documentViewForm = new DocumentViewForm();
					if (printDocument != null)
					{
						documentViewForm.Document = printDocument;
						documentViewForm.ShowDialog();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridList.ApplyUIDesign();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("CustomerID");
				dataTable.Columns.Add("CustomerName");
				dataTable.Columns.Add("PeriodFrom");
				dataTable.Columns.Add("PeriodTo");
				dataTable.Columns.Add("Balance", typeof(decimal));
				dataTable.Columns.Add("EmailAddress");
				dataTable.Columns.Add("PrefMethod", typeof(byte));
				dataTable.Columns.Add("Preview");
				dataTable.Columns.Add("Include", typeof(bool)).DefaultValue = true;
				dataTable.Columns.Add("Status", typeof(byte));
				dataGridList.DataSource = dataTable;
				dataGridList.DisplayLayout.Bands[0].Columns["Preview"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridList.DisplayLayout.Bands[0].Columns["Preview"].CellButtonAppearance.TextHAlign = HAlign.Center;
				dataGridList.DisplayLayout.Bands[0].Columns["Preview"].AllowGroupBy = DefaultableBoolean.False;
				dataGridList.DisplayLayout.Bands[0].Columns["Preview"].AllowRowFiltering = DefaultableBoolean.False;
				dataGridList.DisplayLayout.Bands[0].Columns["Preview"].CellButtonAppearance.ImageHAlign = HAlign.Center;
				dataGridList.DisplayLayout.Bands[0].Columns["Preview"].CellButtonAppearance.Image = imageList1.Images[0];
				dataGridList.DisplayLayout.Bands[0].Columns["Preview"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridList.DisplayLayout.Bands[0].Columns["EmailAddress"].Header.Caption = "Email";
				dataGridList.DisplayLayout.Bands[0].Columns["PrefMethod"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["status"].Hidden = true;
				dataGridList.ApplyNumericColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Balance"], addSummary: false);
				dataGridList.DisplayLayout.Bands[0].Columns["Include"].CellActivation = Activation.AllowEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["Include"].CellClickAction = CellClickAction.Edit;
				dataGridList.DisplayLayout.Bands[0].Columns["Include"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
				dataGridList.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
			}
			catch (Exception e)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		public void LoadData(DataSet data, DateTime from, DateTime to)
		{
			currentData = data;
			data.Tables["Customer"].Copy();
			DataTable dataTable = dataGridList.DataSource as DataTable;
			dataTable.Rows.Clear();
			foreach (DataRow row in data.Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["CustomerID"] = row["Customer Code"];
				dataRow2["CustomerName"] = row["Customer Name"];
				dataRow2["CustomerName"] = row["Customer Name"];
				dataRow2["EmailAddress"] = row["StatementEmail"];
				dataRow2["PrefMethod"] = row["StatementSendingMethod"];
				dataRow2["PeriodFrom"] = from.ToShortDateString();
				dataRow2["PeriodTo"] = to.ToShortDateString();
				dataRow2["Balance"] = row["Ending Balance"];
				dataTable.Rows.Add(dataRow2);
			}
			textBox1.Text = dataGridList.Rows.Count.ToString();
		}

		private bool GetData()
		{
			try
			{
				if (currentMessageData == null)
				{
					currentMessageData = new EmailMessageData();
				}
				foreach (UltraGridRow row in dataGridList.Rows)
				{
					bool flag = false;
					if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(row.Cells["Include"].Value))
					{
						flag = bool.Parse(row.Cells["Include"].Value.ToString());
					}
					if (flag)
					{
						string text = row.Cells["EmailAddress"].Value.ToString();
						if (!(text == "") && CommonLib.IsValidEmail(text))
						{
							DataRow dataRow = currentMessageData.EmailMessageTable.NewRow();
							dataRow["Subject"] = "Statement of Account for " + row.Cells["CustomerName"].Value.ToString();
							dataRow["PeriodFrom"] = periodFrom;
							dataRow["PeriodTo"] = periodTo;
							dataRow["MessageType"] = 2;
							dataRow["UserID"] = Global.CurrentUser;
							dataRow["EmailDate"] = DateTime.Now;
							dataRow["Amount"] = row.Cells["Balance"].Value.ToString();
							dataRow["ConfigType"] = CompanyEmailConfigTypes.Accounting;
							dataRow["RecipientAddress"] = row.Cells["EmailAddress"].Value.ToString();
							string str = dataGridList.ActiveRow.Cells["CustomerID"].Value.ToString();
							DataSet dataSet = currentData.Copy();
							dataSet.Tables["Customer"].Rows.Clear();
							dataSet.Tables["Customer"].Rows.Add(currentData.Tables[0].Select("[Customer Code] = '" + str + "'")[0].ItemArray);
							MemoryStream transactionPreviewPDF = PrintHelper.GetTransactionPreviewPDF(PrintHelper.GetPrintDocument(dataSet, "Customer Statement", "", SysDocTypes.None));
							dataRow["Attachment"] = transactionPreviewPDF.ToArray();
							dataRow["AttachmentName"] = "Statement.pdf";
							dataRow["PartyType"] = "C";
							dataRow["PartyID"] = row.Cells["CustomerID"].Value.ToString();
							row.Cells["status"].Value = 1;
							dataRow.EndEdit();
							currentMessageData.EmailMessageTable.Rows.Add(dataRow);
						}
					}
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonEmail_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					if (SaveData())
					{
						for (int i = 0; i < dataGridList.Rows.Count; i++)
						{
							byte b = 0;
							UltraGridRow ultraGridRow = dataGridList.Rows[i];
							if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(ultraGridRow.Cells["status"].Value))
							{
								b = byte.Parse(ultraGridRow.Cells["status"].Value.ToString());
							}
							if (b == 1)
							{
								ultraGridRow.Delete(displayPrompt: false);
								i--;
							}
						}
						CalculateCount();
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private bool ValidateData()
		{
			return true;
		}

		private bool SaveData()
		{
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			if (currentMessageData.EmailMessageTable.Rows.Count == 0)
			{
				ErrorHelper.WarningMessage("There is no item selected to email.");
				return false;
			}
			try
			{
				bool flag = Factory.EmailMessageSystem.CreateEmailMessage(currentMessageData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				return flag;
			}
			catch (CompanyException e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
				return false;
			}
		}

		private void LoadLog()
		{
			try
			{
				DataSet emailMessageList = Factory.EmailMessageSystem.GetEmailMessageList(EmailMessageTypes.Statement, checkBoxShowSent.Checked);
				dataGridLog.DataSource = emailMessageList.Tables[0];
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(1, "Pending");
				valueList.ValueListItems.Add(2, "Retry");
				valueList.ValueListItems.Add(3, "Failed");
				valueList.ValueListItems.Add(10, "Sent");
				dataGridLog.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList;
				dataGridLog.DisplayLayout.Bands[0].Columns["MessageID"].Hidden = true;
				dataGridLog.DisplayLayout.Bands[0].Columns["PartyType"].Hidden = true;
				dataGridLog.DisplayLayout.Bands[0].Columns["PartyID"].Header.Caption = "Customer Code";
				dataGridLog.DisplayLayout.Bands[0].Columns["PartyName"].Header.Caption = "Customer Name";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void tabPageDetails_Paint(object sender, PaintEventArgs e)
		{
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			Close();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.StatementEmailForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			labelTitle = new System.Windows.Forms.Label();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxShowSent = new System.Windows.Forms.CheckBox();
			label3 = new System.Windows.Forms.Label();
			dataGridLog = new Micromind.UISupport.DataGridList(components);
			textBox1 = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			buttonEmail = new System.Windows.Forms.Button();
			buttonStop = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			line1 = new Micromind.UISupport.Line();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			microsoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			imageList1 = new System.Windows.Forms.ImageList(components);
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridLog).BeginInit();
			panel1.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(labelTitle);
			tabPageGeneral.Controls.Add(dataGridList);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(756, 382);
			labelTitle.AutoSize = true;
			labelTitle.Location = new System.Drawing.Point(3, 8);
			labelTitle.Name = "labelTitle";
			labelTitle.Size = new System.Drawing.Size(370, 13);
			labelTitle.TabIndex = 294;
			labelTitle.Text = "Select the documents that you want to email and click Email Now to process:";
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.Location = new System.Drawing.Point(3, 29);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(750, 346);
			dataGridList.TabIndex = 293;
			dataGridList.Text = "dataGridList1";
			tabPageDetails.Controls.Add(checkBoxShowSent);
			tabPageDetails.Controls.Add(label3);
			tabPageDetails.Controls.Add(dataGridLog);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(756, 382);
			tabPageDetails.Paint += new System.Windows.Forms.PaintEventHandler(tabPageDetails_Paint);
			checkBoxShowSent.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			checkBoxShowSent.AutoSize = true;
			checkBoxShowSent.Location = new System.Drawing.Point(526, 10);
			checkBoxShowSent.Name = "checkBoxShowSent";
			checkBoxShowSent.Size = new System.Drawing.Size(103, 17);
			checkBoxShowSent.TabIndex = 295;
			checkBoxShowSent.Text = "Show sent items";
			checkBoxShowSent.UseVisualStyleBackColor = true;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 8);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(104, 13);
			label3.TabIndex = 294;
			label3.Text = "Statement Email Log";
			dataGridLog.AllowUnfittedView = false;
			dataGridLog.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridLog.DisplayLayout.Appearance = appearance13;
			dataGridLog.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridLog.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridLog.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridLog.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridLog.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridLog.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridLog.DisplayLayout.MaxColScrollRegions = 1;
			dataGridLog.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridLog.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridLog.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridLog.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridLog.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridLog.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridLog.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridLog.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridLog.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridLog.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridLog.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridLog.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridLog.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridLog.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridLog.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridLog.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridLog.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridLog.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridLog.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridLog.Location = new System.Drawing.Point(3, 29);
			dataGridLog.Name = "dataGridLog";
			dataGridLog.ShowDeleteMenu = false;
			dataGridLog.ShowMinusInRed = true;
			dataGridLog.ShowNewMenu = false;
			dataGridLog.Size = new System.Drawing.Size(746, 342);
			dataGridLog.TabIndex = 293;
			dataGridLog.Text = "dataGridList1";
			textBox1.Location = new System.Drawing.Point(96, 19);
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new System.Drawing.Size(100, 20);
			textBox1.TabIndex = 293;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(14, 22);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(79, 13);
			label1.TabIndex = 294;
			label1.Text = "Total Selected:";
			buttonEmail.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonEmail.Location = new System.Drawing.Point(674, 17);
			buttonEmail.Name = "buttonEmail";
			buttonEmail.Size = new System.Drawing.Size(93, 24);
			buttonEmail.TabIndex = 295;
			buttonEmail.Text = "Send Email";
			buttonEmail.UseVisualStyleBackColor = true;
			buttonEmail.Click += new System.EventHandler(buttonEmail_Click);
			buttonStop.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonStop.Location = new System.Drawing.Point(575, 17);
			buttonStop.Name = "buttonStop";
			buttonStop.Size = new System.Drawing.Size(93, 24);
			buttonStop.TabIndex = 295;
			buttonStop.Text = "Cancel";
			buttonStop.UseVisualStyleBackColor = true;
			buttonStop.Click += new System.EventHandler(buttonStop_Click);
			panel1.Controls.Add(line1);
			panel1.Controls.Add(buttonStop);
			panel1.Controls.Add(buttonEmail);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(textBox1);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 439);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(780, 52);
			panel1.TabIndex = 296;
			line1.BackColor = System.Drawing.Color.White;
			line1.Dock = System.Windows.Forms.DockStyle.Top;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(0, 0);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(780, 1);
			line1.TabIndex = 296;
			line1.TabStop = false;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonAutoFit,
				toolStripButton1,
				toolStripSeparator3
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(780, 25);
			toolStrip1.TabIndex = 297;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(52, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				microsoftExcelToolStripMenuItem
			});
			toolStripDropDownButton1.Image = Micromind.ClientUI.Properties.Resources.Export;
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(69, 22);
			toolStripDropDownButton1.Text = "Export";
			microsoftExcelToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Excel;
			microsoftExcelToolStripMenuItem.Name = "microsoftExcelToolStripMenuItem";
			microsoftExcelToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			microsoftExcelToolStripMenuItem.Text = "Microsoft Excel";
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonAutoFit.Image = Micromind.ClientUI.Properties.Resources.column;
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(91, 22);
			toolStripButtonAutoFit.Text = "Fit Columns";
			toolStripButtonAutoFit.ToolTipText = "Fit columns to fit in screen";
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.resize;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(105, 22);
			toolStripButton1.Text = "Resize Column";
			toolStripButton1.ToolTipText = "Resize column to show contents";
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "icon-view.png");
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Location = new System.Drawing.Point(12, 28);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(760, 405);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 298;
			appearance25.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance25;
			ultraTab.Key = "Email";
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&Email";
			ultraTab2.Key = "Log";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Log";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(756, 382);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(780, 491);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panel1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "StatementEmailForm";
			Text = "Email Documents";
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridLog).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
