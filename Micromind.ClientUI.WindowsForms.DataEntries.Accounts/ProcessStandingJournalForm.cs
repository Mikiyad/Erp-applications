using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class ProcessStandingJournalForm : Form, IForm
	{
		private ChequebookData currentData;

		private const string TABLENAME_CONST = "Cheque_Register";

		private const string IDFIELD_CONST = "ChequeNumber";

		private bool isNewRecord = true;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private Label label1;

		private YearComboBox comboBoxStartYear;

		private MonthComboBox comboBoxStartMonth;

		private MMLabel mmLabel3;

		private DataEntryGrid dataGridItems;

		private ImageList imageList1;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPreview;

		private CheckBox standingJournalStatus;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1013;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		private bool IsDirty => false;

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public ProcessStandingJournalForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ProcessStandingJournalForm_Load;
			comboBoxStartYear.TextChanged += comboBoxStartYear_TextChanged;
			comboBoxStartMonth.TextChanged += comboBoxStartYear_TextChanged;
		}

		private void comboBoxStartYear_TextChanged(object sender, EventArgs e)
		{
			LoadData();
		}

		private bool GetData()
		{
			try
			{
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (!SaveData())
			{
				base.DialogResult = DialogResult.None;
			}
			else
			{
				base.DialogResult = DialogResult.None;
			}
		}

		public void LoadData()
		{
			try
			{
				DataSet standingJournalToProcess = Factory.StandingJournalSystem.GetStandingJournalToProcess(comboBoxStartYear.SelectedID, comboBoxStartMonth.SelectedID, standingJournalStatus.Checked);
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in standingJournalToProcess.Tables[0].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["P"] = row["P"];
					dataRow2["StandingJournalID"] = row["StandingJournalID"];
					dataRow2["StartMonth"] = row["StartMonth"];
					dataRow2["StartYear"] = row["StartYear"];
					dataRow2["EndMonth"] = row["EndMonth"];
					dataRow2["EndYear"] = row["EndYear"];
					dataRow2["Narration"] = row["Narration"];
					dataRow2["Amount"] = row["Amount"];
					dataTable.Rows.Add(dataRow2);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ClearForm();
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					_ = currentData.Tables[0].Rows[0];
				}
			}
			catch
			{
				throw;
			}
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
			try
			{
				bool flag = true;
				int num = 0;
				if (ErrorHelper.QuestionMessageYesNo("Do you want to process the standing journals? ") == DialogResult.No)
				{
					return false;
				}
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["P"].Value == null || !(row.Cells["P"].Value.ToString() != "") || Convert.ToBoolean(row.Cells["P"].Value.ToString()))
					{
						string standingJournalID = row.Cells["StandingJournalID"].Value.ToString();
						flag = Factory.StandingJournalSystem.ProcessStandingJournal(standingJournalID, comboBoxStartYear.SelectedID, comboBoxStartMonth.SelectedID);
						if (flag)
						{
							num = checked(num + 1);
							row.Cells["P"].Value = true;
						}
					}
				}
				ErrorHelper.InformationMessage(num + " transactions processed successfully.");
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void ClearForm()
		{
		}

		private void ChequebookGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ChequebookGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return false;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
			}
			return true;
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("P", typeof(bool)).DefaultValue = false;
				dataTable.Columns.Add("StandingJournalID");
				dataTable.Columns.Add("StartMonth", typeof(int));
				dataTable.Columns.Add("StartYear", typeof(int));
				dataTable.Columns.Add("EndMonth", typeof(int));
				dataTable.Columns.Add("EndYear", typeof(int));
				dataTable.Columns.Add("Narration");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.AllowAddNew = false;
				foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					if (!(column.Header.Caption == "Narration") && !(column.Header.Caption == "P"))
					{
						column.CellActivation = Activation.NoEdit;
					}
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["StandingJournalID"].Header.Caption = "Journal ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["StartMonth"].Header.Caption = "Start Month";
				dataGridItems.DisplayLayout.Bands[0].Columns["StartYear"].Header.Caption = "Start Year";
				dataGridItems.DisplayLayout.Bands[0].Columns["EndMonth"].Header.Caption = "End Month";
				dataGridItems.DisplayLayout.Bands[0].Columns["EndYear"].Header.Caption = "End Year";
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].MaxWidth = 18;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].MinWidth = 18;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void ProcessStandingJournalForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid();
				comboBoxStartMonth.LoadData();
				comboBoxStartYear.LoadData();
				comboBoxStartYear.SelectedID = DateTime.Today.Year;
				comboBoxStartMonth.SelectedID = DateTime.Today.Month;
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				DataSet journalVoucherToPrint = Factory.StandingJournalSystem.GetJournalVoucherToPrint(comboBoxStartYear.SelectedID, comboBoxStartMonth.SelectedID);
				if (journalVoucherToPrint == null || journalVoucherToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(journalVoucherToPrint, "Process Journal Voucher", SysDocTypes.GJournal);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void standingJournalStatus_CheckedChanged(object sender, EventArgs e)
		{
			LoadData();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.ProcessStandingJournalForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			imageList1 = new System.Windows.Forms.ImageList(components);
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			standingJournalStatus = new System.Windows.Forms.CheckBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxStartYear = new Micromind.DataControls.YearComboBox();
			comboBoxStartMonth = new Micromind.DataControls.MonthComboBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 337);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(598, 40);
			panelButtons.TabIndex = 6;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(15, 74);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(128, 13);
			label1.TabIndex = 20;
			label1.Text = "Journals to be processed:";
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "check.png");
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				toolStripButtonPreview
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(598, 31);
			toolStrip1.TabIndex = 27;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.print;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			standingJournalStatus.AutoSize = true;
			standingJournalStatus.Location = new System.Drawing.Point(181, 70);
			standingJournalStatus.Name = "standingJournalStatus";
			standingJournalStatus.Size = new System.Drawing.Size(56, 17);
			standingJournalStatus.TabIndex = 28;
			standingJournalStatus.Text = "Status";
			standingJournalStatus.UseVisualStyleBackColor = true;
			standingJournalStatus.Visible = false;
			standingJournalStatus.CheckedChanged += new System.EventHandler(standingJournalStatus_CheckedChanged);
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(18, 90);
			dataGridItems.MinimumSize = new System.Drawing.Size(422, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(567, 241);
			dataGridItems.TabIndex = 26;
			dataGridItems.Text = "dataEntryGrid1";
			comboBoxStartYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStartYear.FormattingEnabled = true;
			comboBoxStartYear.Location = new System.Drawing.Point(178, 37);
			comboBoxStartYear.Name = "comboBoxStartYear";
			comboBoxStartYear.Size = new System.Drawing.Size(80, 21);
			comboBoxStartYear.TabIndex = 25;
			comboBoxStartMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStartMonth.FormattingEnabled = true;
			comboBoxStartMonth.IsMonthNumbers = false;
			comboBoxStartMonth.Location = new System.Drawing.Point(86, 37);
			comboBoxStartMonth.Name = "comboBoxStartMonth";
			comboBoxStartMonth.Size = new System.Drawing.Size(86, 21);
			comboBoxStartMonth.TabIndex = 23;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(15, 40);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(47, 13);
			mmLabel3.TabIndex = 24;
			mmLabel3.Text = "Period:";
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(598, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(488, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(18, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Process";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(598, 377);
			base.Controls.Add(standingJournalStatus);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxStartYear);
			base.Controls.Add(comboBoxStartMonth);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(label1);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ProcessStandingJournalForm";
			Text = "Process Standing Journal";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
