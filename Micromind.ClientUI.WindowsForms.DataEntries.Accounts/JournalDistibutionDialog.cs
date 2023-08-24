using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class JournalDistibutionDialog : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private GLData currentData;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private ScreenAccessRight screenRight;

		private string voucherID;

		private string sysDocID;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private DataGridList dataGridList;

		private Panel panelDetails;

		private Label label6;

		private Label label5;

		private Label label4;

		private Label label2;

		private TextBox textBoxSysDocID;

		private CurrencySelector currencySelector;

		private Label label3;

		private TextBox textBoxNote;

		private Label label1;

		private TextBox textBoxRef1;

		private TextBox textBoxVoucherNumber;

		private DateTimePicker dateTimePickerJournalDate;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPreview;

		private ContextMenuStrip contextMenuStripAccountDetails;

		private ToolStripMenuItem accountMasterToolStripMenuItem;

		private ToolStripMenuItem accountLedgerToolStripMenuItem;

		public bool IsMultiSelect
		{
			get
			{
				return isMultiSelect;
			}
			set
			{
				isMultiSelect = value;
			}
		}

		public string VoucherID
		{
			get
			{
				return voucherID;
			}
			set
			{
				string text2 = voucherID = (textBoxVoucherNumber.Text = value);
			}
		}

		public string SysDocID
		{
			get
			{
				return sysDocID;
			}
			set
			{
				string text2 = sysDocID = (textBoxSysDocID.Text = value);
			}
		}

		public event EventHandler ValidateSelection;

		public JournalDistibutionDialog()
		{
			InitializeComponent();
			base.Activated += SelectShortcutFormDialog_Activated;
			dataGridList.DoubleClick += dataGridItems_DoubleClick;
		}

		private void dataGridItems_DoubleClick(object sender, EventArgs e)
		{
		}

		private void SelectShortcutFormDialog_Activated(object sender, EventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			CanClose = true;
			if (this.ValidateSelection != null)
			{
				this.ValidateSelection(this, null);
			}
			if (CanClose)
			{
				Close();
			}
		}

		private void SelectItem()
		{
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (dataGridList.ActiveRow != null && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "This users's connection with the server will be lost. Are you sure to remove this user?") == DialogResult.Yes)
			{
				try
				{
					Factory.RemoveUser(dataGridList.ActiveRow.Cells["UserID"].Value.ToString(), dataGridList.ActiveRow.Cells["MachineID"].Value.ToString(), dataGridList.ActiveRow.Cells["DBName"].Value.ToString());
					LoadData();
				}
				catch (Exception ex)
				{
					ErrorHelper.ErrorMessage(ex.Message);
				}
			}
		}

		private void panelButtons_Paint(object sender, PaintEventArgs e)
		{
		}

		private void JournalDistibutionDialog_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				SetupGrid();
				LoadData();
				dataGridList.ApplyUIDesign();
				dataGridList.ContextMenuStrip = contextMenuStripAccountDetails;
			}
			catch (Exception e2)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		public void LoadData()
		{
			try
			{
				currentData = Factory.JournalSystem.GetJournalDistributionSummary(sysDocID, voucherID);
				if (currentData != null)
				{
					FillData();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Journal"].Rows[0];
					dateTimePickerJournalDate.Value = DateTime.Parse(dataRow["JournalDate"].ToString());
					textBoxSysDocID.Text = dataRow["SysDocID"].ToString();
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					if (dataRow["CurrencyID"] != DBNull.Value)
					{
						currencySelector.SelectedID = dataRow["CurrencyID"].ToString();
						if (dataRow["CurrencyRate"] != DBNull.Value)
						{
							currencySelector.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
						}
						else
						{
							currencySelector.Rate = 1m;
						}
					}
					else
					{
						currencySelector.SelectedID = "";
						currencySelector.Rate = 1m;
					}
					textBoxNote.Text = dataRow["Note"].ToString();
					if (currentData.Tables.Contains("Journal_Details") && currentData.JournalDetailsTable.Rows.Count != 0)
					{
						DataTable dataTable = dataGridList.DataSource as DataTable;
						dataTable.Rows.Clear();
						if (currentData.Tables.Contains("Journal_Details") && currentData.JournalDetailsTable.Rows.Count != 0)
						{
							foreach (DataRow row in currentData.Tables["Journal_Details"].Rows)
							{
								DataRow dataRow3 = dataTable.NewRow();
								dataRow3["Account Code"] = row["Account Code"];
								dataRow3["Account Name"] = row["Account Name"];
								dataRow3["Cost Center"] = row["Cost Center"];
								if (row["Project Code"] != DBNull.Value)
								{
									dataRow3["Project Code"] = row["Project Code"];
								}
								if (row["Cost Category"] != DBNull.Value)
								{
									dataRow3["Cost Category"] = row["Cost Category"];
								}
								if (row["Debit"] != DBNull.Value)
								{
									dataRow3["Debit"] = Math.Round(decimal.Parse(row["Debit"].ToString()), Global.CurDecimalPoints);
								}
								else
								{
									dataRow3["Debit"] = DBNull.Value;
								}
								dataRow3["Payee ID"] = row["Payee ID"];
								dataRow3["Payee Name"] = row["Payee Name"];
								if (row["Credit"] != DBNull.Value)
								{
									dataRow3["Credit"] = Math.Round(decimal.Parse(row["Credit"].ToString()), Global.CurDecimalPoints);
								}
								else
								{
									dataRow3["Credit"] = DBNull.Value;
								}
								bool flag = false;
								if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != Global.BaseCurrencyID)
								{
									flag = true;
								}
								if (flag)
								{
									if (row["CreditFC"] != DBNull.Value)
									{
										dataRow3["CreditFC"] = Math.Round(decimal.Parse(row["CreditFC"].ToString()), Global.CurDecimalPoints);
										dataGridList.DisplayLayout.Bands[0].Columns["CreditFC"].Hidden = false;
									}
									else
									{
										dataRow3["CreditFC"] = DBNull.Value;
										dataGridList.DisplayLayout.Bands[0].Columns["CreditFC"].Hidden = true;
									}
									if (row["DebitFC"] != DBNull.Value)
									{
										dataRow3["DebitFC"] = Math.Round(decimal.Parse(row["DebitFC"].ToString()), Global.CurDecimalPoints);
										dataGridList.DisplayLayout.Bands[0].Columns["DebitFC"].Hidden = false;
									}
									else
									{
										dataRow3["DebitFC"] = DBNull.Value;
										dataGridList.DisplayLayout.Bands[0].Columns["DebitFC"].Hidden = true;
									}
								}
								else
								{
									dataGridList.DisplayLayout.Bands[0].Columns["CreditFC"].Hidden = true;
									dataGridList.DisplayLayout.Bands[0].Columns["DebitFC"].Hidden = true;
								}
								dataRow3.EndEdit();
								dataTable.Rows.Add(dataRow3);
							}
							dataTable.AcceptChanges();
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridList.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Account Code");
				dataTable.Columns.Add("Account Name");
				dataTable.Columns.Add("Payee ID");
				dataTable.Columns.Add("Payee Name");
				dataTable.Columns.Add("Cost Center");
				dataTable.Columns.Add("Project Code");
				dataTable.Columns.Add("Cost Category");
				dataTable.Columns.Add("Debit", typeof(decimal));
				dataTable.Columns.Add("Credit", typeof(decimal));
				dataTable.Columns.Add("DebitFC", typeof(decimal));
				dataTable.Columns.Add("CreditFC", typeof(decimal));
				dataGridList.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["Project Code"];
				bool hidden = dataGridList.DisplayLayout.Bands[0].Columns["Cost Category"].Hidden = !useJobCosting;
				ultraGridColumn.Hidden = hidden;
				dataGridList.DisplayLayout.Bands[0].Columns["Debit"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridList.DisplayLayout.Bands[0].Columns["Debit"].Format = Format.GridAmountFormat;
				dataGridList.DisplayLayout.Bands[0].Columns["Credit"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridList.DisplayLayout.Bands[0].Columns["Credit"].Format = Format.GridAmountFormat;
				dataGridList.DisplayLayout.Bands[0].Columns["DebitFC"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridList.DisplayLayout.Bands[0].Columns["DebitFC"].Format = Format.GridAmountFormat;
				dataGridList.DisplayLayout.Bands[0].Columns["CreditFC"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridList.DisplayLayout.Bands[0].Columns["CreditFC"].Format = Format.GridAmountFormat;
				dataGridList.DisplayLayout.Bands[0].Columns["Account Name"].Width = checked(30 * dataGridList.Width) / 100;
				dataGridList.DisplayLayout.Bands[0].Summaries.Add("TotalDebit", SummaryType.Sum, dataGridList.DisplayLayout.Bands[0].Columns["Debit"], SummaryPosition.UseSummaryPositionColumn);
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalDebit"].Appearance.BackColor = Color.White;
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalDebit"].Appearance.TextHAlign = HAlign.Right;
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalDebit"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalDebit"].DisplayFormat = "{0:n}";
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalDebit"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridList.DisplayLayout.Bands[0].Summaries.Add("TotalCredit", SummaryType.Sum, dataGridList.DisplayLayout.Bands[0].Columns["Credit"], SummaryPosition.UseSummaryPositionColumn);
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalCredit"].Appearance.BackColor = Color.White;
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalCredit"].Appearance.TextHAlign = HAlign.Right;
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalCredit"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalCredit"].DisplayFormat = "{0:n}";
				dataGridList.DisplayLayout.Bands[0].Summaries["TotalCredit"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
			}
			catch (Exception e)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				string text = textBoxSysDocID.Text;
				string text2 = textBoxVoucherNumber.Text;
				DataSet journalToPrint = Factory.JournalSystem.GetJournalToPrint(text, text2);
				if (journalToPrint == null || journalToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(journalToPrint, text, "Journal Voucher", SysDocTypes.GJournal, isPrint, showPrintDialog);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void accountMasterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string id = dataGridList.ActiveRow.Cells["Account Code"].Value.ToString();
			new FormHelper().EditAccount(id);
		}

		private void accountLedgerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TransactionListForm transactionListForm = new TransactionListForm();
			string parmVal = "AccountID";
			string text = dataGridList.ActiveRow.Cells["Account Code"].Value.ToString();
			text = dataGridList.ActiveRow.Cells[0].Value.ToString();
			transactionListForm.ListType = TransactionListType.TrialBalance;
			int selectedPeriod = 10;
			transactionListForm.Show();
			transactionListForm.dateControl1.SelectedPeriod = (DatePeriods)selectedPeriod;
			transactionListForm.dateControl1.FromDate = dateTimePickerJournalDate.Value;
			transactionListForm.dateControl1.ToDate = dateTimePickerJournalDate.Value;
			transactionListForm.dateControl1.Enabled = false;
			transactionListForm.Text = "Account Ledger";
			transactionListForm.ShowTrialBalance(parmVal, text, dateTimePickerJournalDate.Value, dateTimePickerJournalDate.Value);
		}

		private void HideShowColumns()
		{
			if (dataGridList.DisplayLayout.Bands.Count != 0)
			{
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["JournalID"];
				UltraGridColumn ultraGridColumn2 = dataGridList.DisplayLayout.Bands[0].Columns["JournalDetailID"];
				UltraGridColumn ultraGridColumn3 = dataGridList.DisplayLayout.Bands[0].Columns["CreditReviewDate"];
				UltraGridColumn ultraGridColumn4 = dataGridList.DisplayLayout.Bands[0].Columns["CreditReviewBy"];
				UltraGridColumn ultraGridColumn5 = dataGridList.DisplayLayout.Bands[0].Columns["AcceptPDC"];
				UltraGridColumn ultraGridColumn6 = dataGridList.DisplayLayout.Bands[0].Columns["IsCustomerSince"];
				UltraGridColumn ultraGridColumn7 = dataGridList.DisplayLayout.Bands[0].Columns["ContactName"];
				UltraGridColumn ultraGridColumn8 = dataGridList.DisplayLayout.Bands[0].Columns["ContactTitle"];
				bool flag2 = dataGridList.DisplayLayout.Bands[0].Columns["PostalCode"].Hidden = true;
				bool flag4 = ultraGridColumn8.Hidden = flag2;
				bool flag6 = ultraGridColumn7.Hidden = flag4;
				bool flag8 = ultraGridColumn6.Hidden = flag6;
				bool flag10 = ultraGridColumn5.Hidden = flag8;
				bool flag12 = ultraGridColumn4.Hidden = flag10;
				bool flag14 = ultraGridColumn3.Hidden = flag12;
				bool hidden = ultraGridColumn2.Hidden = flag14;
				ultraGridColumn.Hidden = hidden;
				dataGridList.DisplayLayout.Bands[0].Columns["Email"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Mobile"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Phone1"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Phone2"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Fax"].Hidden = true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.JournalDistibutionDialog));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			dataGridList = new Micromind.UISupport.DataGridList();
			panelDetails = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBoxSysDocID = new System.Windows.Forms.TextBox();
			currencySelector = new Micromind.DataControls.CurrencySelector();
			label3 = new System.Windows.Forms.Label();
			textBoxNote = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			dateTimePickerJournalDate = new System.Windows.Forms.DateTimePicker();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			contextMenuStripAccountDetails = new System.Windows.Forms.ContextMenuStrip();
			accountMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			accountLedgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			panelDetails.SuspendLayout();
			toolStrip1.SuspendLayout();
			contextMenuStripAccountDetails.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 414);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(645, 40);
			panelButtons.TabIndex = 3;
			panelButtons.Paint += new System.Windows.Forms.PaintEventHandler(panelButtons_Paint);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(645, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(535, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Close";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridList.ContextMenuStrip = contextMenuStripAccountDetails;
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
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(12, 147);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(621, 261);
			dataGridList.TabIndex = 4;
			dataGridList.Text = "dataGridList1";
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxSysDocID);
			panelDetails.Controls.Add(currencySelector);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerJournalDate);
			panelDetails.Location = new System.Drawing.Point(12, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(619, 112);
			panelDetails.TabIndex = 5;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(268, 18);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(90, 13);
			label6.TabIndex = 123;
			label6.Text = "Voucher Number:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(5, 18);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(44, 13);
			label5.TabIndex = 122;
			label5.Text = "Doc ID:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(5, 41);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(70, 13);
			label4.TabIndex = 121;
			label4.Text = "Journal Date:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(5, 62);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(52, 13);
			label2.TabIndex = 120;
			label2.Text = "Currency:";
			textBoxSysDocID.Location = new System.Drawing.Point(94, 13);
			textBoxSysDocID.MaxLength = 15;
			textBoxSysDocID.Name = "textBoxSysDocID";
			textBoxSysDocID.ReadOnly = true;
			textBoxSysDocID.Size = new System.Drawing.Size(139, 20);
			textBoxSysDocID.TabIndex = 119;
			currencySelector.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector.Enabled = false;
			currencySelector.Location = new System.Drawing.Point(94, 58);
			currencySelector.MaximumSize = new System.Drawing.Size(99999, 20);
			currencySelector.MinimumSize = new System.Drawing.Size(5, 20);
			currencySelector.Name = "currencySelector";
			currencySelector.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			currencySelector.SelectedID = "";
			currencySelector.Size = new System.Drawing.Size(139, 20);
			currencySelector.TabIndex = 3;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(5, 83);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			textBoxNote.Location = new System.Drawing.Point(94, 80);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ReadOnly = true;
			textBoxNote.Size = new System.Drawing.Size(423, 20);
			textBoxNote.TabIndex = 6;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(268, 41);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(360, 36);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.ReadOnly = true;
			textBoxRef1.Size = new System.Drawing.Size(139, 20);
			textBoxRef1.TabIndex = 4;
			textBoxVoucherNumber.Location = new System.Drawing.Point(360, 14);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.ReadOnly = true;
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 1;
			dateTimePickerJournalDate.Enabled = false;
			dateTimePickerJournalDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerJournalDate.Location = new System.Drawing.Point(94, 36);
			dateTimePickerJournalDate.Name = "dateTimePickerJournalDate";
			dateTimePickerJournalDate.Size = new System.Drawing.Size(139, 20);
			dateTimePickerJournalDate.TabIndex = 2;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				toolStripButtonPreview
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(645, 31);
			toolStrip1.TabIndex = 6;
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
			contextMenuStripAccountDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				accountMasterToolStripMenuItem,
				accountLedgerToolStripMenuItem
			});
			contextMenuStripAccountDetails.Name = "contextMenuStripAccountDetails";
			contextMenuStripAccountDetails.Size = new System.Drawing.Size(159, 70);
			accountMasterToolStripMenuItem.Name = "accountMasterToolStripMenuItem";
			accountMasterToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			accountMasterToolStripMenuItem.Text = "Account Master";
			accountMasterToolStripMenuItem.Click += new System.EventHandler(accountMasterToolStripMenuItem_Click);
			accountLedgerToolStripMenuItem.Name = "accountLedgerToolStripMenuItem";
			accountLedgerToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			accountLedgerToolStripMenuItem.Text = "Account Ledger";
			accountLedgerToolStripMenuItem.Click += new System.EventHandler(accountLedgerToolStripMenuItem_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(645, 454);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(dataGridList);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "JournalDistibutionDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Journal Distribution Summary";
			base.Load += new System.EventHandler(JournalDistibutionDialog_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			contextMenuStripAccountDetails.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
