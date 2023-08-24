using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class TransactionListForm : Form, IDataForm, IDataList, IExternalReport
	{
		private bool isFirstTimeActivating = true;

		private TransactionListType listType;

		private TransactionPaymentType paymentType = TransactionPaymentType.Cash;

		public string parameter1 = "";

		private Hashtable listViewKeys = new Hashtable();

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet listData;

		private DataSet smartlistdata;

		private DataSet reportData;

		private XPButton buttonDone;

		private XPButton buttonNew;

		private Panel panelButtons;

		private ContextMenu contextMenuPopup;

		private MenuItem menuItemGotoAccount;

		private MenuItem menuItem2;

		private MenuItem menuItemNew;

		private MenuItem menuItemDelete;

		private MenuItem menuItem1;

		private MenuItem menuItemMarkAs;

		private MenuItem menuItemInactive;

		private ContextMenu contextMenu2;

		private MenuItem menuItemFlat;

		private MenuItem menuItemHierarchical;

		private MenuItem menuItem3;

		private MenuItem menuItemShowInactiveItems;

		private Line linePanelDown;

		private IContainer components;

		private XPButton buttonOpen;

		private DataGridList dataGridList;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonRefresh;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem microsoftExcelToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonAllowGrouping;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonColumnChooser;

		private ToolStripButton toolStripButtonAutoFit;

		private XPButton buttonDelete;

		public DateControl dateControl1;

		private XPButton xpButton1;

		private CheckBox checkBoxVoid;

		private PictureBox pictureBoxVoid;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonMerge;

		private CheckBox checkBoxClosed;

		private ToolStrip toolStrip1;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool useProperty = CompanyPreferences.UseProperty;

		private string attributeID1Title = CompanyPreferences.AttributeID1Title;

		private string attributeID2Title = CompanyPreferences.AttributeID2Title;

		private MMLabel mmLabelStatus;

		private GenericListComboBox comboBoxStatus;

		private string selectedDocID = "";

		private bool isFirstTB;

		private ScreenAccessRight screenRight;

		public TransactionListType ListType
		{
			get
			{
				return listType;
			}
			set
			{
				listType = value;
			}
		}

		public TransactionPaymentType PaymentType
		{
			get
			{
				return paymentType;
			}
			set
			{
				paymentType = value;
			}
		}

		public string SelectedDocID
		{
			get
			{
				return selectedDocID;
			}
			set
			{
				selectedDocID = value;
			}
		}

		private bool ShowInactiveItems
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		public TransactionListForm()
		{
			InitializeComponent();
			base.Activated += CompanyAccountsListForm_Activated;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.NewMenuClicked += dataGridList_NewMenuClicked;
			dataGridList.OpenMenuClicked += dataGridList_OpenMenuClicked;
			dataGridList.DeleteMenuClicked += dataGridList_DeleteMenuClicked;
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowNewMenu = false;
			base.FormClosing += TransactionListForm_FormClosing;
			dataGridList.AllowUnfittedView = true;
			comboBoxStatus.SelectedIndexChanged += comboBoxInsuranceClaimStatus_SelectedIndexChanged;
		}

		private void TransactionListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				UserPreferences.SaveCurrentUserSetting(base.Name + "MergeCell", toolStripButtonMerge.Checked);
				Global.GlobalSettings.SaveFormProperties(this);
				dataGridList.SaveLayout();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridList_DeleteMenuClicked(object sender, EventArgs e)
		{
		}

		private void dataGridList_OpenMenuClicked(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void dataGridList_NewMenuClicked(object sender, EventArgs e)
		{
			New();
		}

		private void dataGridList_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			if (dataGridList.ActiveRow != null)
			{
				if (ListType == TransactionListType.TrialBalance)
				{
					isFirstTB = true;
					TransactionListForm transactionListForm = new TransactionListForm();
					string text = null;
					string text2 = null;
					bool flag = false;
					foreach (UltraGridBand band in dataGridList.DisplayLayout.Bands)
					{
						if (band.Columns.Exists("GroupID"))
						{
							text = "GroupID";
						}
						if (band.Columns.Exists("AccountID"))
						{
							text = "AccountID";
						}
						string selectedSysDocID = GetSelectedSysDocID();
						string selectedID = GetSelectedID();
						if (!string.IsNullOrEmpty(selectedSysDocID) && !string.IsNullOrEmpty(selectedID))
						{
							text2 = selectedSysDocID;
							text = selectedID;
							new FormHelper().EditTransaction(text2, text);
							flag = true;
						}
						if (!flag)
						{
							text2 = dataGridList.ActiveRow.Cells[0].Value.ToString();
							transactionListForm.ListType = TransactionListType.TrialBalance;
							transactionListForm.dateControl1.LoadData();
							int selectedPeriod = (int)dateControl1.SelectedPeriod;
							transactionListForm.Show();
							transactionListForm.dateControl1.SelectedPeriod = (DatePeriods)selectedPeriod;
							transactionListForm.dateControl1.Enabled = false;
							transactionListForm.Text = "Trial Balance List";
							transactionListForm.ShowTrialBalance(text, text2, dateControl1.FromDate, dateControl1.ToDate);
						}
					}
				}
				else
				{
					OpenForEdit();
				}
			}
		}

		public void ShowTrialBalance(string parmVal1, string paramVal2, DateTime fromDate, DateTime toDate)
		{
			try
			{
				dateControl1.FromDate = fromDate;
				dateControl1.ToDate = toDate;
				LoadReportData(parmVal1.ToString(), paramVal2.ToString());
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadReportData(string ParamValue1, string ParamValue2)
		{
			try
			{
				Refresh();
				dataGridList.DataSource = Factory.JournalSystem.GetTrialBalanceAccountList(ParamValue1, ParamValue2, dateControl1.FromDate, dateControl1.ToDate);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CompanyAccountsListForm_Activated(object sender, EventArgs e)
		{
			if (isFirstTimeActivating)
			{
				isFirstTimeActivating = false;
				dataGridList.AutoFitColumns();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && listData != null)
			{
				listData.Dispose();
				listData = null;
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.TransactionListForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			buttonDelete = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDone = new Micromind.UISupport.XPButton();
			buttonOpen = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			contextMenuPopup = new System.Windows.Forms.ContextMenu();
			menuItemGotoAccount = new System.Windows.Forms.MenuItem();
			menuItem2 = new System.Windows.Forms.MenuItem();
			menuItemNew = new System.Windows.Forms.MenuItem();
			menuItemDelete = new System.Windows.Forms.MenuItem();
			menuItem1 = new System.Windows.Forms.MenuItem();
			menuItemMarkAs = new System.Windows.Forms.MenuItem();
			menuItemInactive = new System.Windows.Forms.MenuItem();
			contextMenu2 = new System.Windows.Forms.ContextMenu();
			menuItemFlat = new System.Windows.Forms.MenuItem();
			menuItemHierarchical = new System.Windows.Forms.MenuItem();
			menuItem3 = new System.Windows.Forms.MenuItem();
			menuItemShowInactiveItems = new System.Windows.Forms.MenuItem();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			microsoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAllowGrouping = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMerge = new System.Windows.Forms.ToolStripButton();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			dateControl1 = new Micromind.DataControls.DateControl();
			xpButton1 = new Micromind.UISupport.XPButton();
			checkBoxVoid = new System.Windows.Forms.CheckBox();
			pictureBoxVoid = new System.Windows.Forms.PictureBox();
			checkBoxClosed = new System.Windows.Forms.CheckBox();
			mmLabelStatus = new Micromind.UISupport.MMLabel();
			comboBoxStatus = new Micromind.DataControls.GenericListComboBox();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStatus).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Controls.Add(buttonOpen);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 459);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(828, 48);
			panelButtons.TabIndex = 4;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 15);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "Delete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(828, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(720, 16);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 3;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			buttonOpen.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpen.BackColor = System.Drawing.Color.DarkGray;
			buttonOpen.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpen.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpen.Location = new System.Drawing.Point(12, 15);
			buttonOpen.Name = "buttonOpen";
			buttonOpen.Size = new System.Drawing.Size(96, 24);
			buttonOpen.TabIndex = 0;
			buttonOpen.Text = "&Open";
			buttonOpen.UseVisualStyleBackColor = false;
			buttonOpen.Click += new System.EventHandler(buttonGotoItem_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 15);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Visible = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			contextMenuPopup.MenuItems.AddRange(new System.Windows.Forms.MenuItem[6]
			{
				menuItemGotoAccount,
				menuItem2,
				menuItemNew,
				menuItemDelete,
				menuItem1,
				menuItemMarkAs
			});
			menuItemGotoAccount.Index = 0;
			menuItemGotoAccount.Text = "&Goto Account";
			menuItem2.Index = 1;
			menuItem2.Text = "-";
			menuItemNew.Index = 2;
			menuItemNew.Text = "&New Account";
			menuItemDelete.Index = 3;
			menuItemDelete.Text = "&Delete Account";
			menuItem1.Index = 4;
			menuItem1.Text = "-";
			menuItemMarkAs.Index = 5;
			menuItemMarkAs.MenuItems.AddRange(new System.Windows.Forms.MenuItem[1]
			{
				menuItemInactive
			});
			menuItemMarkAs.Text = "Mark As";
			menuItemInactive.Index = 0;
			menuItemInactive.Text = "Inactive";
			contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[4]
			{
				menuItemFlat,
				menuItemHierarchical,
				menuItem3,
				menuItemShowInactiveItems
			});
			menuItemFlat.Index = 0;
			menuItemFlat.Text = "Flat View";
			menuItemHierarchical.Index = 1;
			menuItemHierarchical.Text = "Hierarchical View";
			menuItem3.Index = 2;
			menuItem3.Text = "-";
			menuItemShowInactiveItems.Index = 3;
			menuItemShowInactiveItems.Text = "Show Inactive Items";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButton1,
				toolStripSeparator3,
				toolStripButtonColumnChooser,
				toolStripButtonMerge
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(828, 31);
			toolStrip1.TabIndex = 289;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonRefresh.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			toolStripButtonRefresh.Size = new System.Drawing.Size(74, 28);
			toolStripButtonRefresh.Text = "Refresh";
			toolStripButtonRefresh.Click += new System.EventHandler(toolStripButtonRefresh_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				microsoftExcelToolStripMenuItem
			});
			toolStripDropDownButton1.Image = Micromind.ClientUI.Properties.Resources.Export;
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(77, 28);
			toolStripDropDownButton1.Text = "Export";
			microsoftExcelToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Excel;
			microsoftExcelToolStripMenuItem.Name = "microsoftExcelToolStripMenuItem";
			microsoftExcelToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			microsoftExcelToolStripMenuItem.Text = "Microsoft Excel";
			microsoftExcelToolStripMenuItem.Click += new System.EventHandler(microsoftExcelToolStripMenuItem_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAllowGrouping.CheckOnClick = true;
			toolStripButtonAllowGrouping.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonAllowGrouping.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAllowGrouping.Name = "toolStripButtonAllowGrouping";
			toolStripButtonAllowGrouping.Size = new System.Drawing.Size(118, 28);
			toolStripButtonAllowGrouping.Text = "Allow Grouping";
			toolStripButtonAllowGrouping.Click += new System.EventHandler(toolStripButtonAllowGrouping_Click);
			toolStripButtonAutoFit.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonAutoFit.Image");
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(99, 28);
			toolStripButtonAutoFit.Text = "Fit Columns";
			toolStripButtonAutoFit.ToolTipText = "Fit Columns to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(118, 28);
			toolStripButton1.Text = "Resize Columns";
			toolStripButton1.ToolTipText = "Resize columns to length of text";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(83, 28);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
			toolStripButtonMerge.CheckOnClick = true;
			toolStripButtonMerge.Image = Micromind.ClientUI.Properties.Resources.merge;
			toolStripButtonMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMerge.Name = "toolStripButtonMerge";
			toolStripButtonMerge.Size = new System.Drawing.Size(97, 28);
			toolStripButtonMerge.Text = "Merge Cells";
			toolStripButtonMerge.Click += new System.EventHandler(toolStripButtonMerge_Click);
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
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(12, 122);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(804, 331);
			dataGridList.TabIndex = 3;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 31);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(422, 88);
			dateControl1.TabIndex = 0;
			dateControl1.ToDate = new System.DateTime(2017, 12, 26, 23, 59, 59, 59);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(328, 49);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 2;
			xpButton1.Text = "Refresh";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			checkBoxVoid.AutoSize = true;
			checkBoxVoid.Location = new System.Drawing.Point(51, 100);
			checkBoxVoid.Name = "checkBoxVoid";
			checkBoxVoid.Size = new System.Drawing.Size(115, 17);
			checkBoxVoid.TabIndex = 1;
			checkBoxVoid.Text = "Show voided items";
			checkBoxVoid.UseVisualStyleBackColor = true;
			pictureBoxVoid.Image = Micromind.ClientUI.Properties.Resources.voidicon;
			pictureBoxVoid.Location = new System.Drawing.Point(494, 52);
			pictureBoxVoid.Name = "pictureBoxVoid";
			pictureBoxVoid.Size = new System.Drawing.Size(26, 20);
			pictureBoxVoid.TabIndex = 291;
			pictureBoxVoid.TabStop = false;
			pictureBoxVoid.Visible = false;
			checkBoxClosed.AutoSize = true;
			checkBoxClosed.Location = new System.Drawing.Point(186, 100);
			checkBoxClosed.Name = "checkBoxClosed";
			checkBoxClosed.Size = new System.Drawing.Size(114, 17);
			checkBoxClosed.TabIndex = 292;
			checkBoxClosed.Text = "Show closed items";
			checkBoxClosed.UseVisualStyleBackColor = true;
			checkBoxClosed.Visible = false;
			mmLabelStatus.AutoSize = true;
			mmLabelStatus.BackColor = System.Drawing.Color.Transparent;
			mmLabelStatus.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabelStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabelStatus.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabelStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabelStatus.IsFieldHeader = false;
			mmLabelStatus.IsRequired = false;
			mmLabelStatus.Location = new System.Drawing.Point(38, 101);
			mmLabelStatus.Name = "mmLabelStatus";
			mmLabelStatus.PenWidth = 1f;
			mmLabelStatus.ShowBorder = false;
			mmLabelStatus.Size = new System.Drawing.Size(70, 13);
			mmLabelStatus.TabIndex = 294;
			mmLabelStatus.Text = "Claim Status:";
			mmLabelStatus.Visible = false;
			comboBoxStatus.Assigned = false;
			comboBoxStatus.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStatus.CustomReportFieldName = "";
			comboBoxStatus.CustomReportKey = "";
			comboBoxStatus.CustomReportValueType = 1;
			comboBoxStatus.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStatus.DisplayLayout.Appearance = appearance13;
			comboBoxStatus.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStatus.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStatus.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStatus.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxStatus.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStatus.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxStatus.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStatus.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStatus.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStatus.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxStatus.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStatus.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStatus.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStatus.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxStatus.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStatus.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStatus.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxStatus.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxStatus.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStatus.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxStatus.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxStatus.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStatus.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxStatus.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStatus.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStatus.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStatus.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStatus.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxStatus.Editable = true;
			comboBoxStatus.FilterString = "";
			comboBoxStatus.GenericListType = Micromind.Common.Data.GenericListTypes.ActionStatus;
			comboBoxStatus.HasAllAccount = false;
			comboBoxStatus.HasCustom = false;
			comboBoxStatus.IsDataLoaded = false;
			comboBoxStatus.IsSingleColumn = false;
			comboBoxStatus.Location = new System.Drawing.Point(126, 97);
			comboBoxStatus.MaxDropDownItems = 12;
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.ShowInactiveItems = false;
			comboBoxStatus.ShowQuickAdd = true;
			comboBoxStatus.Size = new System.Drawing.Size(109, 20);
			comboBoxStatus.TabIndex = 293;
			comboBoxStatus.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxStatus.Visible = false;
			base.AcceptButton = buttonOpen;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(828, 507);
			base.Controls.Add(checkBoxClosed);
			base.Controls.Add(pictureBoxVoid);
			base.Controls.Add(checkBoxVoid);
			base.Controls.Add(xpButton1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(dataGridList);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(mmLabelStatus);
			base.Controls.Add(comboBoxStatus);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "TransactionListForm";
			Text = "List";
			base.Load += new System.EventHandler(CompanyAccountsListForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStatus).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			toolStripButtonMerge.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "MergeCell", defaultValue: false);
			Global.GlobalSettings.LoadFormProperties(this);
		}

		private void SetupList()
		{
			try
			{
				if (dataGridList.DisplayLayout != null && dataGridList.DisplayLayout.Bands.Count > 0)
				{
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("V"))
					{
						ValueList valueList = new ValueList();
						ValueListItem valueListItem = new ValueListItem();
						valueListItem.DataValue = true;
						valueListItem.DisplayText = " ";
						valueListItem.Appearance.Image = pictureBoxVoid.Image;
						valueList.ValueListItems.Add(valueListItem);
						valueList.ValueListItems.Add(false, " ");
						dataGridList.DisplayLayout.Bands[0].Columns["V"].ValueList = valueList;
						dataGridList.DisplayLayout.Bands[0].Columns["V"].Header.Appearance.Image = pictureBoxVoid.Image;
						dataGridList.DisplayLayout.Bands[0].Columns["V"].Header.Caption = "";
						dataGridList.DisplayLayout.Bands[0].Columns["V"].Width = 18;
						dataGridList.DisplayLayout.Bands[0].Columns["V"].MinWidth = 18;
						dataGridList.DisplayLayout.Bands[0].Columns["V"].MaxWidth = 18;
						dataGridList.DisplayLayout.Bands[0].Columns["V"].LockedWidth = true;
						dataGridList.DisplayLayout.Bands[0].Columns["V"].Header.Appearance.ImageHAlign = HAlign.Center;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("JobID"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["JobID"].Hidden = !useJobCosting;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("JobName"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["JobName"].Hidden = !useJobCosting;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID1ID"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1ID"].Hidden = !useProperty;
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1ID"].Header.Caption = "Property";
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID1Name"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1Name"].Hidden = !useProperty;
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1Name"].Header.Caption = attributeID1Title + " Name";
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID2ID"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2ID"].Hidden = !useProperty;
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2ID"].Header.Caption = attributeID2Title + " ID";
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID2Name"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2Name"].Hidden = !useProperty;
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2Name"].Header.Caption = attributeID2Title + " Name";
					}
					if (listType == TransactionListType.Journal)
					{
						ValueList valueList2 = new ValueList();
						valueList2.ValueListItems.Add(1, "JV");
						valueList2.ValueListItems.Add(2, "Chq Receipt");
						valueList2.ValueListItems.Add(3, "Cash Receipt");
						valueList2.ValueListItems.Add(4, "Cash Payment");
						valueList2.ValueListItems.Add(5, "Chq Payment");
						valueList2.ValueListItems.Add(6, "Fund Transfer");
						valueList2.ValueListItems.Add(7, "Cheque Deposit");
						valueList2.ValueListItems.Add(8, "Cash Expense");
						valueList2.ValueListItems.Add(9, "Chq Expense");
						valueList2.ValueListItems.Add(10, "Debit Note");
						valueList2.ValueListItems.Add(11, "Credit Note");
						valueList2.ValueListItems.Add(12, "Returned Cheque");
						valueList2.ValueListItems.Add(13, "R.Chq Cancellation");
						valueList2.ValueListItems.Add(14, "I.Chq Clearance");
						valueList2.ValueListItems.Add(15, "I.Chq Cancellation");
						valueList2.ValueListItems.Add(16, "I.Chq Return");
						valueList2.ValueListItems.Add(17, "Security Cheque");
						valueList2.ValueListItems.Add(18, "Inventory Adjustment");
						valueList2.ValueListItems.Add(19, "Inventory Transfer");
						valueList2.ValueListItems.Add(20, "Inventory Transfer");
						valueList2.ValueListItems.Add(21, "Inventory Transfer");
						valueList2.ValueListItems.Add(25, "Sales Invoice");
						valueList2.ValueListItems.Add(26, "Sales Receipt");
						valueList2.ValueListItems.Add(27, "Sales Return");
						valueList2.ValueListItems.Add(28, "Sales Return");
						valueList2.ValueListItems.Add(32, "Purchase Receipt");
						valueList2.ValueListItems.Add(33, "Purchase Invoice");
						valueList2.ValueListItems.Add(34, "Cash Purchase");
						valueList2.ValueListItems.Add(35, "Purchase Return");
						valueList2.ValueListItems.Add(37, "Purchase Return");
						valueList2.ValueListItems.Add(39, "Import Purchase Invoice");
						valueList2.ValueListItems.Add(40, "Transfer");
						valueList2.ValueListItems.Add(41, "Deposit");
						valueList2.ValueListItems.Add(42, "Expense");
						valueList2.ValueListItems.Add(44, "Payroll");
						valueList2.ValueListItems.Add(45, "Employee Loan");
						valueList2.ValueListItems.Add(43, "Salary Sheet");
						valueList2.ValueListItems.Add(36, "PO Shipment");
						valueList2.ValueListItems.Add(25, "Job Invoice");
						dataGridList.DisplayLayout.Bands[0].Columns["Type"].ValueList = valueList2;
						UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[1].Columns["JournalID"];
						UltraGridColumn ultraGridColumn2 = dataGridList.DisplayLayout.Bands[1].Columns["SysDocID"];
						bool flag2 = dataGridList.DisplayLayout.Bands[1].Columns["VoucherID"].Hidden = true;
						bool hidden = ultraGridColumn2.Hidden = flag2;
						ultraGridColumn.Hidden = hidden;
						UltraGridColumn ultraGridColumn3 = dataGridList.DisplayLayout.Bands[1].Columns["JournalID"];
						UltraGridColumn ultraGridColumn4 = dataGridList.DisplayLayout.Bands[1].Columns["SysDocID"];
						ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridList.DisplayLayout.Bands[1].Columns["VoucherID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
						ExcludeFromColumnChooser excludeFromColumnChooser5 = ultraGridColumn3.ExcludeFromColumnChooser = (ultraGridColumn4.ExcludeFromColumnChooser = excludeFromColumnChooser2);
					}
					if (listType == TransactionListType.InventoryLedger)
					{
						ValueList sysDocTypesValueList = UILib.GetSysDocTypesValueList();
						dataGridList.DisplayLayout.Bands[0].Columns["Type"].ValueList = sysDocTypesValueList;
						if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
						{
							dataGridList.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
							dataGridList.DisplayLayout.Bands[0].Columns["Price"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
						}
					}
					else if (listType == TransactionListType.IssuedCheque)
					{
						ValueList valueList3 = new ValueList();
						valueList3.ValueListItems.Add(1, "Blank");
						valueList3.ValueListItems.Add(2, "Issued");
						valueList3.ValueListItems.Add(4, "Cleared");
						valueList3.ValueListItems.Add(5, "Bounced");
						valueList3.ValueListItems.Add(8, "Void");
						valueList3.ValueListItems.Add(9, "Cancelled");
						dataGridList.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList3;
						dataGridList.DisplayLayout.Bands[0].Columns["Payee Name"].Width = 200;
						int num3 = dataGridList.DisplayLayout.Bands[0].Columns["P"].MaxWidth = (dataGridList.DisplayLayout.Bands[0].Columns["Status"].MinWidth = 20);
					}
					else if (listType == TransactionListType.ReceivedCheque)
					{
						ValueList valueList4 = new ValueList();
						valueList4.ValueListItems.Add(1, "Undeposited");
						valueList4.ValueListItems.Add(2, "Matured");
						valueList4.ValueListItems.Add(3, "Discounted");
						valueList4.ValueListItems.Add(7, "Cleared");
						valueList4.ValueListItems.Add(8, "Bounced");
						valueList4.ValueListItems.Add(9, "Cancelled");
						dataGridList.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList4;
					}
					else if (listType == TransactionListType.PurchaseOrder || listType == TransactionListType.ImportPurchaseOrder)
					{
						ValueList valueList5 = new ValueList();
						valueList5.ValueListItems.Add(1, "Open");
						valueList5.ValueListItems.Add(2, "Shipped");
						valueList5.ValueListItems.Add(3, "Received");
						valueList5.ValueListItems.Add(4, "Closed");
						valueList5.ValueListItems.Add(5, "Cancelled");
						dataGridList.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList5;
					}
					else if (listType == TransactionListType.ReceivedCheque)
					{
						ValueList valueList6 = new ValueList();
						valueList6.ValueListItems.Add(1, "Undeposited");
						valueList6.ValueListItems.Add(2, "Matured");
						valueList6.ValueListItems.Add(3, "Discounted");
						valueList6.ValueListItems.Add(4, "Sent to Bank");
						valueList6.ValueListItems.Add(7, "Cleared");
						valueList6.ValueListItems.Add(8, "Bounced");
						valueList6.ValueListItems.Add(9, "Cancelled");
						dataGridList.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList6;
					}
					else if (listType == TransactionListType.ActivityLog)
					{
						ValueList valueList7 = new ValueList();
						valueList7.ValueListItems.Add(1, "Add");
						valueList7.ValueListItems.Add(2, "Update");
						valueList7.ValueListItems.Add(3, "Delete");
						valueList7.ValueListItems.Add(4, "Activate");
						valueList7.ValueListItems.Add(5, "Inactivate");
						valueList7.ValueListItems.Add(6, "Void");
						valueList7.ValueListItems.Add(7, "Unvoid");
						valueList7.ValueListItems.Add(8, "Close");
						valueList7.ValueListItems.Add(9, "TempConvert");
						valueList7.ValueListItems.Add(100, "Login");
						valueList7.ValueListItems.Add(101, "Logout");
						valueList7.ValueListItems.Add(200, "Other");
						dataGridList.DisplayLayout.Bands[0].Columns["Type"].ValueList = valueList7;
						dataGridList.DisplayLayout.Bands[0].Columns["Date"].Format = "G";
					}
					else if (listType == TransactionListType.SalesHistory)
					{
						ValueList valueList8 = new ValueList();
						valueList8.ValueListItems.Add(25, "Sales");
						valueList8.ValueListItems.Add(46, "Sales");
						valueList8.ValueListItems.Add(27, "Return");
						dataGridList.DisplayLayout.Bands[0].Columns["Type"].ValueList = valueList8;
						dataGridList.DisplayLayout.Bands[0].Columns["CustomerID"].Header.Caption = "Customer Code";
						dataGridList.DisplayLayout.Bands[0].Columns["CustomerName"].Header.Caption = "Customer Code";
						dataGridList.DisplayLayout.Bands[0].Columns["SalespersonID"].Header.Caption = "Salesperson Code";
						dataGridList.DisplayLayout.Bands[0].Columns["Doc ID"].Header.Caption = "Doc ID";
						dataGridList.DisplayLayout.Bands[0].Columns["Doc Number"].Header.Caption = "Number";
						dataGridList.DisplayLayout.Bands[1].Columns["SysDocID"].Hidden = true;
						dataGridList.DisplayLayout.Bands[1].Columns["VoucherID"].Hidden = true;
					}
					else if (listType == TransactionListType.ARPaymentAllocation)
					{
						checkBoxVoid.Visible = false;
						buttonOpen.Enabled = false;
						buttonDelete.Visible = true;
						buttonNew.Visible = true;
						buttonNew.Enabled = false;
						dataGridList.DisplayLayout.Bands[0].Columns["Invoice Date"].Hidden = true;
						dataGridList.DisplayLayout.Bands[0].Columns["Due Date"].Hidden = true;
						dataGridList.DisplayLayout.Bands[0].Columns["Payment Date"].Hidden = true;
					}
					else if (listType == TransactionListType.APPaymentAllocation)
					{
						checkBoxVoid.Visible = false;
						buttonOpen.Enabled = false;
						buttonDelete.Visible = true;
						buttonNew.Visible = true;
						buttonNew.Enabled = false;
					}
					if (listType == TransactionListType.FreightCharges)
					{
						dataGridList.DisplayLayout.Bands[0].Columns["Doc ID"].Hidden = true;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID1ID"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1ID"].Header.Caption = "Property";
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1ID"].Hidden = !useProperty;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID1Name"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1Name"].Header.Caption = attributeID1Title + " Name";
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1Name"].Hidden = !useProperty;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID2ID"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2ID"].Header.Caption = attributeID2Title + " ID";
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2ID"].Hidden = !useProperty;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID2Name"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2Name"].Header.Caption = attributeID2Title + " Name";
						dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2Name"].Hidden = !useProperty;
					}
					dataGridList.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadData()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					PublicFunctions.StartWaiting(this);
					if (listType == TransactionListType.SalesInvoice)
					{
						listData = Factory.SalesInvoiceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isExport: false, checkBoxVoid.Checked, SelectedDocID, IsCash: false);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesInvoiceNI)
					{
						listData = Factory.SalesInvoiceNISystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isExport: false, checkBoxVoid.Checked, SelectedDocID, IsCash: false, NonInventoryInvoiceType.SalesInvoice);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesReceipt)
					{
						listData = Factory.SalesInvoiceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isExport: false, checkBoxVoid.Checked, SelectedDocID, IsCash: true);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesEnquiry)
					{
						listData = Factory.SalesEnquirySystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesOrder)
					{
						listData = Factory.SalesOrderSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isExport: false, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesQuote)
					{
						listData = Factory.SalesQuoteSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesReturn)
					{
						listData = Factory.SalesReturnSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.DeliveryNote)
					{
						listData = Factory.DeliveryNoteSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.CRMActivity)
					{
						listData = Factory.ActivitySystem.GetActivityList(dateControl1.FromDate, dateControl1.ToDate);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.CRMCustomerActivity)
					{
						listData = Factory.ActivitySystem.GetCustomerActivityList(dateControl1.FromDate, dateControl1.ToDate);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.LegalActivity)
					{
						listData = Factory.LegalActivitySystem.GetLegalActivityList(dateControl1.FromDate, dateControl1.ToDate);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.DeliveryReturn)
					{
						listData = Factory.DeliveryReturnSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SendChequeToBank)
					{
						listData = Factory.SendChequeSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ARPaymentAllocation)
					{
						buttonDelete.Size = new Size(115, 24);
						buttonDelete.Text = "Remove Allocation";
						listData = Factory.ARJournalSystem.GetARAllocationList(dateControl1.FromDate, dateControl1.ToDate);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesHistory)
					{
						listData = Factory.CustomerSystem.GetSalesHistory(dateControl1.FromDate, dateControl1.ToDate);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ActivityLog)
					{
						listData = Factory.ActivityLogSystem.GetActivityLogList(dateControl1.FromDate, dateControl1.ToDate);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.InventoryLedger)
					{
						listData = Factory.ProductSystem.GetInventoryLedgerList(parameter1, dateControl1.FromDate, dateControl1.ToDate, excludeInventoryTransfer: false);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchaseInvoice)
					{
						listData = Factory.PurchaseInvoiceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchaseInvoiceNI)
					{
						listData = Factory.PurchaseInvoiceNISystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchaseOrder)
					{
						listData = Factory.PurchaseOrderSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isImport: false, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchaseOrderNI)
					{
						listData = Factory.PurchaseOrderNISystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isImport: false, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ImportPurchaseOrder)
					{
						listData = Factory.PurchaseOrderSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isImport: true, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchaseQuote)
					{
						listData = Factory.PurchaseQuoteSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isImport: false, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchaseReturn)
					{
						listData = Factory.PurchaseReturnSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.CashPurchase)
					{
						listData = Factory.PurchaseInvoiceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.GRNReturn)
					{
						listData = Factory.GRNReturnSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.APPaymentAllocation)
					{
						buttonDelete.Size = new Size(115, 24);
						buttonDelete.Text = "Remove Allocation";
						listData = Factory.APJournalSystem.GetAPAllocationList(dateControl1.FromDate, dateControl1.ToDate);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchaseGRN)
					{
						listData = Factory.PurchaseReceiptSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, includeLocal: true, includeImport: false, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ImportGRN)
					{
						listData = Factory.PurchaseReceiptSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, includeLocal: false, includeImport: true, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ProformaInvoice)
					{
						listData = Factory.PurchaseQuoteSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isImport: true, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ExportSalesInvoice)
					{
						listData = Factory.SalesInvoiceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isExport: true, checkBoxVoid.Checked, selectedDocID, IsCash: false);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ExportSalesOrder)
					{
						listData = Factory.SalesOrderSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isExport: true, checkBoxVoid.Checked, selectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ExportSalesProforma)
					{
						listData = Factory.SalesProformaSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, isExport: true);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ConsignIn)
					{
						listData = Factory.ConsignInSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ConsignInSettlement)
					{
						listData = Factory.ConsignInSettlementSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ConsignOut)
					{
						listData = Factory.ConsignOutSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ConsignOutReturn)
					{
						listData = Factory.ConsignOutReturnSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ExportDeliveryNote)
					{
						listData = Factory.DeliveryNoteSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.POShipment)
					{
						listData = Factory.POShipmentSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JournalEntry)
					{
						listData = Factory.JournalSystem.GetJVList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PaymentRequest)
					{
						listData = Factory.PaymentRequestSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.Journal)
					{
						listData = Factory.JournalSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.TrialBalance)
					{
						listData = Factory.JournalSystem.GetTrialBalanceList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.Expense)
					{
						listData = Factory.TransactionSystem.GetExpenseList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.DebitNote)
					{
						listData = Factory.TransactionSystem.GetDebitNoteList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.CreditNote)
					{
						listData = Factory.TransactionSystem.GetCreditNoteList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ReceiptVoucher)
					{
						listData = Factory.TransactionSystem.GetReceiptVoucherList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ReceiptVoucherMultiple)
					{
						listData = Factory.TransactionSystem.GetReceiptVoucherMultipleList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ReturnVoucher)
					{
						listData = Factory.TransactionSystem.GetReturnVoucherList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.IssuedChequeReturn)
					{
						listData = Factory.IssuedChequeSystem.GetIssuedChequeReturnList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PaymentVoucher)
					{
						listData = Factory.TransactionSystem.GetPaymentVoucherList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, PaymentType);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ChequeReceipt)
					{
						listData = Factory.TransactionSystem.GetChequeReceiptList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ChequePayment)
					{
						listData = Factory.TransactionSystem.GetChequePaymentList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.FundTransfer)
					{
						listData = Factory.TransactionSystem.GetTransferList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.IssuedCheque)
					{
						listData = Factory.IssuedChequeSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ReceivedCheque)
					{
						listData = Factory.ReceivedChequeSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ReceivedChequeCancel)
					{
						listData = Factory.TransactionSystem.GetReceivedChequeReturnList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SecurityCheque)
					{
						listData = Factory.IssuedChequeSystem.GetSecurityChequeList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.FixedAssetPurchase)
					{
						listData = Factory.FixedAssetPurchaseSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.FixedAssetPurchaseOrder)
					{
						listData = Factory.FixedAssetPurchaseOrderSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.FixedAssetSale)
					{
						listData = Factory.FixedAssetSaleSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.FixedAssetTransfer)
					{
						listData = Factory.FixedAssetTransferSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobInvoice)
					{
						listData = Factory.JobInvoiceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobInventoryIssue)
					{
						listData = Factory.JobInventoryIssueSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.TR)
					{
						listData = Factory.BankFacilityTransactionSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, BankFacilityTypes.TR, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.TRPayment)
					{
						listData = Factory.BankFacilityPaymentSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, BankFacilityTypes.TR, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.InventoryRepacking)
					{
						listData = Factory.InventoryRepackingSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.InventoryTransfer)
					{
						listData = Factory.InventoryTransferSystem.GetListInventoryTransfer(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.DirectInventoryTransfer)
					{
						listData = Factory.InventoryTransferSystem.GetListDirectInventoryTransfer(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.InventoryTransferAcceptance)
					{
						listData = Factory.InventoryTransferSystem.GetListInventoryTransfersToAccept(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.InventoryTransferReturn)
					{
						listData = Factory.InventoryTransferSystem.GetListInventoryTransferReturn(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.CLVoucher)
					{
						listData = Factory.CLVoucherSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ArrivalReport)
					{
						listData = Factory.ArrivalReportSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isImport: true, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.QualityClaim)
					{
						checkBoxClosed.Visible = true;
						listData = Factory.QualityClaimSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isImport: true, checkBoxVoid.Checked, checkBoxClosed.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PropertyServiceInvoice)
					{
						listData = Factory.SalesInvoiceNISystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isExport: false, checkBoxVoid.Checked, SelectedDocID, IsCash: false, NonInventoryInvoiceType.PropertySalesInvoice);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PropertyIncomePosting)
					{
						listData = Factory.RentalPostingSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PropertyRental)
					{
						listData = Factory.PropertyRentSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, val: true, checkBoxVoid.Checked, 1);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PropertyCancel)
					{
						listData = Factory.PropertyCancelSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, val: true, checkBoxVoid.Checked, 2);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PropertyRenew)
					{
						listData = Factory.PropertyRentSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, val: true, checkBoxVoid.Checked, 2);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ContainerTracking)
					{
						listData = Factory.ContainerStrackingSystem.GetContainerTrackingList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.CashSalaryPayment)
					{
						listData = Factory.PayrollTransactionSystem.GetSalaryCashList(dateControl1.FromDate, dateControl1.ToDate, isImport: true, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ChequeSalaryPayment)
					{
						listData = Factory.PayrollTransactionSystem.GetSalaryChequeList(dateControl1.FromDate, dateControl1.ToDate, isImport: true, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.TransferSalaryPayment)
					{
						listData = Factory.PayrollTransactionSystem.GetSalaryBankList(dateControl1.FromDate, dateControl1.ToDate, isImport: true, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalarySheet)
					{
						listData = Factory.SalarySheetSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeLoan)
					{
						listData = Factory.EmployeeLoanSystem.GetListEmployeeLoan(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeProvision)
					{
						listData = Factory.EmployeeProvisionSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeLoanSettlement)
					{
						listData = Factory.EmployeeLoanSystem.GetListEmployeeLoanSettlement(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeEOS)
					{
						listData = Factory.EmployeeEOSSettlementSystem.GetEmployeeEOSList(dateControl1.FromDate, dateControl1.ToDate);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeLoanPayment)
					{
						listData = Factory.EmployeeLoanSystem.GetListEmployeeLoanPayment(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchaseClaim)
					{
						listData = Factory.PurchaseClaimSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobEstimation)
					{
						listData = Factory.JobEstimationSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobTimesheet)
					{
						listData = Factory.JobTimesheetSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobInventoryReturn)
					{
						listData = Factory.JobInventoryReturnSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobClosing)
					{
						listData = Factory.JobSystem.GetClosedJobList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobExpenseIssue)
					{
						listData = Factory.JobExpenseIssueSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobMaterialEstimate)
					{
						listData = Factory.JobMaterialEstimateSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobMaterialRequisition)
					{
						listData = Factory.JobMaterialRequisitionSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.AssemblyBuild)
					{
						listData = Factory.AssemblyBuildSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.Production)
					{
						listData = Factory.ProductionSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.WorkOrder)
					{
						listData = Factory.WorkOrderSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.OverTimeEntry)
					{
						listData = Factory.OverTimeEntrySystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.GarmentRental)
					{
						listData = Factory.GarmentRentalSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.GarmentRentalReturn)
					{
						listData = Factory.GarmentRentalReturnSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ServiceCallTrack)
					{
						listData = Factory.ServiceCallTrackSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ExportPickList)
					{
						listData = Factory.ExportPickListSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesProforma)
					{
						listData = Factory.SalesProformaSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, isExport: false);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ExportPackingList)
					{
						listData = Factory.ExportPackingListSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, isShipment: false);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.Shipment)
					{
						listData = Factory.ExportPackingListSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, isShipment: true);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.MaintenanceScheduler)
					{
						listData = Factory.MaintenanceSchedulerSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobMaintenanceSchedule)
					{
						listData = Factory.JobMaintenanceScheduleSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ChequeDeposit)
					{
						listData = Factory.TransactionSystem.GetChequeDepositList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.MaintenanceEntry)
					{
						listData = Factory.MaintenanceEntrySystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.MaintenanceScheduler)
					{
						listData = Factory.MaintenanceSchedulerSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobMaintenanceEntry)
					{
						listData = Factory.JobMaintenanceServiceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeAppraisal)
					{
						listData = Factory.EmployeeAppraisalSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchaseCostEntry)
					{
						listData = Factory.PurchaseCostEntrySystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.CustomerInsurance)
					{
						listData = Factory.CustomerInsuranceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.InventoryDamage)
					{
						listData = Factory.InventoryDamageSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ConsignOutSettlement)
					{
						listData = Factory.ConsignOutSettlementSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeSalary)
					{
						listData = Factory.EmployeeSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ProjectSubContractPO)
					{
						listData = Factory.ProjectSubContractSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, isImport: true, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ProjectSubContractPI)
					{
						listData = Factory.ProjectSubContractPISystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeOpeningBalanceLeave)
					{
						listData = Factory.OpeningBalanceLeaveSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeGeneralActivity)
					{
						listData = Factory.EmployeeActivitySystem.GetGeneralActivityList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeePerformance)
					{
						listData = Factory.EmployeePerformanceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.BillOfLading)
					{
						listData = Factory.BillOfLadingSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PriceList)
					{
						listData = Factory.PriceListSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.Requisition)
					{
						listData = Factory.RequisitionSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.Mobilization)
					{
						listData = Factory.MobilizationSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.FixedAssetBulkPurchase)
					{
						listData = Factory.FixedAssetBulkPurchaseSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.FixedAssetDep)
					{
						listData = Factory.FixedAssetDepSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ItemTransaction)
					{
						listData = Factory.ItemTransactionSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.IssuedChequeClearance)
					{
						listData = Factory.IssuedChequeSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EquipmentTransfer)
					{
						listData = Factory.EquipmentTransferSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EquipmentWorkOrder)
					{
						listData = Factory.EquipmentWorkOrderSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.InventoryAdjustment)
					{
						listData = Factory.InventoryAdjustmentSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.CustomerOpeningBalance)
					{
						listData = Factory.OpeningBalanceBatchSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, "SYS_010");
						goto IL_2cbc;
					}
					if (listType == TransactionListType.VendorOpeningBalance)
					{
						listData = Factory.OpeningBalanceBatchSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, "SYS_011");
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeOpeningBalance)
					{
						listData = Factory.OpeningBalanceBatchSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, "SYS_012");
						goto IL_2cbc;
					}
					if (listType == TransactionListType.InventoryOpeningBalance)
					{
						listData = Factory.OpeningBalanceBatchSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, "SYS_013");
						goto IL_2cbc;
					}
					if (listType == TransactionListType.WorkOrderInventoryReturn)
					{
						listData = Factory.WorkOrderInventoryReturnSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.FreightCharges)
					{
						listData = Factory.FreightChargeSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.LPOReceipt)
					{
						listData = Factory.LPOReceiptSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.WorkOrderInventoryIssue)
					{
						listData = Factory.WorkOrderInventoryIssueSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalaryAddition)
					{
						listData = Factory.SalarySystem.GetAdditionList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.InventoryDismantle)
					{
						listData = Factory.InventoryDismantleSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalaryDeduction)
					{
						listData = Factory.SalarySystem.GetDeductionList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ProductPriceBulkupdate)
					{
						listData = Factory.ProductPriceBulkpdateSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ChequeReceiptOpening)
					{
						listData = Factory.OpeningEntryTransactionSystem.GetReceiptVoucherList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ChequePaymentOpening)
					{
						listData = Factory.OpeningEntryTransactionSystem.GetPaymentVoucherList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, PaymentType);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.MaterialReservation)
					{
						listData = Factory.MaterialReservationSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.LegalAction)
					{
						listData = Factory.LegalActionSystem.GetLegalActionList(dateControl1.FromDate, dateControl1.ToDate);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesForecasting)
					{
						listData = Factory.SalesForecastingSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PurchasePrepaymentInvoice)
					{
						listData = Factory.PurchasePrepaymentInvoiceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.TaskTransaction)
					{
						listData = Factory.TaskTransactionSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.TRApplication)
					{
						listData = Factory.TRApplicationSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, BankFacilityTypes.TR, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.Budgeting)
					{
						listData = Factory.BudgetingSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.VehicleMileageTrack)
					{
						listData = Factory.VehicleMileageTrackSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.SalesManTarget)
					{
						listData = Factory.SalesManTargetSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.CustomerInsuranceClaim)
					{
						checkBoxVoid.Visible = false;
						mmLabelStatus.Visible = true;
						comboBoxStatus.Visible = true;
						mmLabelStatus.BringToFront();
						comboBoxStatus.BringToFront();
						listData = Factory.CustomerInsuranceClaimSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, comboBoxStatus.SelectedID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ChequeDiscount)
					{
						listData = Factory.DiscountChequeSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.ChequeReceiptMultiple)
					{
						listData = Factory.TransactionSystem.GetChequeReceiptMultipleList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.StandingJournal)
					{
						listData = Factory.StandingJournalSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.LeaveRequest)
					{
						listData = Factory.EmployeeActivitySystem.GetEmployeeLeaveList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.LeaveSalaryPayemnt)
					{
						listData = Factory.EmployeeActivitySystem.GetEmployeeLeavePaymentList(dateControl1.FromDate, dateControl1.ToDate, showVoid: true);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.IssuedChequeCancellation)
					{
						listData = Factory.IssuedChequeSystem.GetChequeCancelledList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.LoanEntry)
					{
						listData = Factory.LoanEntrySystem.GetListLoanEntry(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.JobManHrsBudgeting)
					{
						listData = Factory.JobManHrsBudgetingSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.EmployeeLeave)
					{
						listData = Factory.EmployeeLeaveProcessSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PropertyServiceRequest)
					{
						listData = Factory.PropertyServiceSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.PropertyServiceAssign)
					{
						listData = Factory.PropertyServiceSystem.GetPropertyServiceAssignList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked, SelectedDocID);
						goto IL_2cbc;
					}
					if (listType == TransactionListType.BillDiscount)
					{
						listData = Factory.DiscountBillSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, checkBoxVoid.Checked);
						goto IL_2cbc;
					}
					ErrorHelper.ErrorMessage("Transaction List is not implimented for type: " + listType.ToString());
					goto end_IL_0009;
					IL_2cbc:
					if (SelectedDocID != "" && listType != TransactionListType.ARPaymentAllocation && listType != TransactionListType.APPaymentAllocation && listType != TransactionListType.ActivityLog && listType != TransactionListType.TrialBalance)
					{
						DataView defaultView = listData.Tables[0].DefaultView;
						DataSet dataSet = new DataSet();
						DataTable dataTable = defaultView.ToTable();
						if (dataTable.Columns.Contains("Doc ID"))
						{
							dataTable = defaultView.ToTable(true, "Doc ID");
						}
						else if (dataTable.Columns.Contains("SysDocID"))
						{
							dataTable = defaultView.ToTable(true, "SysDocID");
						}
						dataSet.Tables.Add(dataTable);
						string text = "";
						string text2 = "";
						text = SelectedDocID;
						text2 = Factory.DatabaseSystem.GetFieldValue("System_Document", "LocationID", "SysDocID", selectedDocID).ToString();
						if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
						{
							DataSet openQueryList = Factory.SystemDocumentSystem.GetOpenQueryList(text);
							if (openQueryList != null && openQueryList.Tables.Count != 0 && openQueryList.Tables[0].Rows.Count != 0)
							{
								string text3 = openQueryList.Tables["System_Document"].Rows[0]["OpenListQuery"].ToString();
								if (!string.IsNullOrEmpty(text3))
								{
									string text4 = "";
									text4 = Factory.SystemDocumentSystem.ReplaceSystemParametersOpenList(text3, dateControl1.FromDate, dateControl1.ToDate, text, SelectedDocID);
									if (!string.IsNullOrEmpty(text4))
									{
										listData = Factory.SystemDocumentSystem.ExecuteOpenListQuery(text4, checkBoxVoid.Checked, isBuiltin: true);
									}
									else
									{
										listData = Factory.SystemDocumentSystem.ExecuteOpenListQuery(text3, checkBoxVoid.Checked, isBuiltin: true);
									}
								}
							}
						}
					}
					dataGridList.DataSource = listData;
					dataGridList.ApplyFormat();
					SetupList();
					end_IL_0009:;
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
				finally
				{
					PublicFunctions.EndWaiting(this);
				}
			}
		}

		private void HideShowColumns()
		{
			if (dataGridList.DataSource != null)
			{
				_ = dataGridList.DisplayLayout.Bands.Count;
			}
		}

		private void buttonDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OpenForEdit()
		{
			if (listType == TransactionListType.ARPaymentAllocation || listType == TransactionListType.APPaymentAllocation)
			{
				return;
			}
			_ = listType;
			_ = 156;
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				string selectedSysDocID = GetSelectedSysDocID();
				FormHelper formHelper = new FormHelper();
				string subType = "";
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Type"))
				{
					subType = dataGridList.ActiveRow.Cells["Type"].Value.ToString();
				}
				if (ListType == TransactionListType.LeaveRequest)
				{
					int selectedActivityID = GetSelectedActivityID();
					formHelper.EditTransaction(listType, selectedID, selectedActivityID);
				}
				else
				{
					formHelper.EditTransaction(listType, selectedSysDocID, selectedID, subType);
				}
			}
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			New();
		}

		private void New()
		{
			new FormHelper();
			if (listType == TransactionListType.SalesInvoice)
			{
				FormActivator.BringFormToFront(FormActivator.SalesInvoiceFormObj);
			}
			else
			{
				ErrorHelper.ErrorMessage("General List is not implimented for type: " + listType.ToString());
			}
		}

		private void buttonGotoItem_Click(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void CompanyAccountsListForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					Init();
					dateControl1.Enabled = true;
					LoadData();
					dataGridList.LoadLayout();
					dataGridList.ApplyUIDesign();
					dataGridList.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
					dataGridList.ApplyFormat();
					foreach (UltraGridBand band in dataGridList.DisplayLayout.Bands)
					{
						if (band.Columns.Exists("Amount"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Amount"], addSummary: true);
						}
						if (band.Columns.Exists("AmountFC"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["AmountFC"], addSummary: true);
						}
						if (band.Columns.Exists("Total"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Total"], addSummary: true);
						}
						if (band.Columns.Exists("Debit"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Debit"], addSummary: true);
						}
						if (band.Columns.Exists("Credit"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Credit"], addSummary: true);
						}
						if (band.Columns.Exists("DebitFC"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["DebitFC"], addSummary: true);
						}
						if (band.Columns.Exists("CreditFC"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["CreditFC"], addSummary: true);
						}
						if (band.Columns.Exists("Amount FC"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Amount FC"], addSummary: true);
						}
						if (band.Columns.Exists("Discount FC"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Discount FC"], addSummary: true);
						}
						if (band.Columns.Exists("Discount"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Discount"], addSummary: true);
						}
						if (band.Columns.Exists("Gain/Loss"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Gain/Loss"], addSummary: true);
						}
						if (band.Columns.Exists("Cur Rate"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Cur Rate"], addSummary: false);
						}
						if (band.Columns.Exists("Quantity"))
						{
							dataGridList.ApplyQuantityColumnFormat(band.Columns["Quantity"], addSummary: true);
						}
						if (band.Columns.Exists("Qty In"))
						{
							dataGridList.ApplyQuantityColumnFormat(band.Columns["Qty In"], addSummary: true);
						}
						if (band.Columns.Exists("Qty Out"))
						{
							dataGridList.ApplyQuantityColumnFormat(band.Columns["Qty Out"], addSummary: true);
						}
						if (band.Columns.Exists("Price"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Price"], addSummary: false);
						}
						if (band.Columns.Exists("Asset Value"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Asset Value"], addSummary: false);
						}
						if (band.Columns.Exists("Avg Cost"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Avg Cost"], addSummary: false);
						}
						if (band.Columns.Exists("Quantity"))
						{
							dataGridList.ApplyQuantityColumnFormat(band.Columns["Quantity"], addSummary: true);
						}
						if (band.Columns.Exists("UnitPrice"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["UnitPrice"], addSummary: false);
						}
						if (band.Columns.Exists("Amount"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Amount"], addSummary: true);
						}
						if (band.Columns.Exists("Claim Amount"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Claim Amount"], addSummary: true);
						}
						if (band.Columns.Exists("Received Amount"))
						{
							dataGridList.ApplyNumericColumnFormat(band.Columns["Received Amount"], addSummary: true);
						}
					}
					if (dataGridList.DisplayLayout.Bands.Count > 0 && dataGridList.DisplayLayout.Bands[0].Summaries.Count > 0)
					{
						if (dataGridList.DisplayLayout.Bands[0].Summaries.Exists("count"))
						{
							dataGridList.DisplayLayout.Bands[0].Summaries["count"].SummaryPosition = SummaryPosition.UseSummaryPositionColumn;
							dataGridList.DisplayLayout.Bands[0].Summaries["count"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
							dataGridList.DisplayLayout.Bands[0].Summaries["count"].SummaryPositionColumn = dataGridList.DisplayLayout.Bands[0].Columns[1];
						}
						if (dataGridList.DisplayLayout.Bands[0].Columns.Contains("Cur Rate"))
						{
							dataGridList.DisplayLayout.Bands[0].Columns["Cur Rate"].AllowRowSummaries = AllowRowSummaries.False;
						}
						if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID1ID"))
						{
							dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1ID"].Header.Caption = attributeID1Title + " ID";
							dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1ID"].Hidden = !useProperty;
						}
						if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID1Name"))
						{
							dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1Name"].Header.Caption = attributeID1Title + " Name";
							dataGridList.DisplayLayout.Bands[0].Columns["AttributeID1Name"].Hidden = !useProperty;
						}
						if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID2ID"))
						{
							dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2ID"].Header.Caption = attributeID2Title + " ID";
							dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2ID"].Hidden = !useProperty;
						}
						if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("AttributeID2Name"))
						{
							dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2Name"].Header.Caption = attributeID2Title + " Name";
							dataGridList.DisplayLayout.Bands[0].Columns["AttributeID2Name"].Hidden = !useProperty;
						}
					}
					dataGridList.DisplayLayout.Override.AllowColSizing = AllowColSizing.Free;
					HideShowColumns();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
		}

		public void OnActivated()
		{
		}

		private void CloseForm()
		{
			Close();
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Accounts;
		}

		public static int GetScreenID()
		{
			return 7004;
		}

		private string GetSelectedID()
		{
			try
			{
				string result = "";
				if (dataGridList.ActiveRow != null && dataGridList.ActiveRow.Band.Index == 0)
				{
					if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return "";
					}
					if (dataGridList.ActiveRow.Cells.Exists("Doc Number"))
					{
						result = dataGridList.ActiveRow.Cells["Doc Number"].Text.ToString();
					}
					else if (dataGridList.ActiveRow.Cells.Exists("VoucherID"))
					{
						result = dataGridList.ActiveRow.Cells["VoucherID"].Text.ToString();
					}
					else if (dataGridList.ActiveRow.Cells.Exists("Number"))
					{
						result = dataGridList.ActiveRow.Cells["Number"].Text.ToString();
					}
					else if (dataGridList.ActiveRow.Cells.Exists("Batch Number"))
					{
						result = dataGridList.ActiveRow.Cells["Batch Number"].Text.ToString();
					}
					else if (dataGridList.ActiveRow.Cells.Exists("Journal ID"))
					{
						result = dataGridList.ActiveRow.Cells["Journal ID"].Text.ToString();
					}
					else if (dataGridList.ActiveRow.Cells.Exists("EmployeeID"))
					{
						result = dataGridList.ActiveRow.Cells["EmployeeID"].Text.ToString();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private string GetSelectedSysDocID()
		{
			try
			{
				string result = "";
				if (dataGridList.ActiveRow.Band.Index == 1)
				{
					return "";
				}
				if (dataGridList.ActiveRow != null)
				{
					if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return "";
					}
					if (dataGridList.ActiveRow.Cells.Exists("Doc ID"))
					{
						result = dataGridList.ActiveRow.Cells["Doc ID"].Text.ToString();
					}
					else if (dataGridList.ActiveRow.Cells.Exists("SysDocID"))
					{
						result = dataGridList.ActiveRow.Cells["SysDocID"].Text.ToString();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private int GetSelectedActivityID()
		{
			try
			{
				int result = 0;
				if (dataGridList.ActiveRow != null && dataGridList.ActiveRow.Band.Index == 0)
				{
					if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return 0;
					}
					if (dataGridList.ActiveRow.Cells.Exists("TransactionID"))
					{
						result = int.Parse(dataGridList.ActiveRow.Cells["TransactionID"].Text.ToString());
					}
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return 0;
			}
		}

		private UltraGridRow GetSelectedItem()
		{
			if (dataGridList.ActiveRow != null)
			{
				return dataGridList.ActiveRow;
			}
			return null;
		}

		public void RefreshData()
		{
		}

		private string GetDocumentTitle()
		{
			return "Account List";
		}

		private void Print()
		{
			try
			{
				PrintHelper.PreviewDocument(ultraGridPrintDocument1, Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public bool SaveReport(ExternalReportTypes reportType)
		{
			return true;
		}

		public void ClearView()
		{
		}

		private void toolStripButtonRefresh_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print();
		}

		private void microsoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new DataExportHelper().ExportToExcel(dataGridList);
		}

		private void toolStripButtonAllowGrouping_Click(object sender, EventArgs e)
		{
			dataGridList.DisplayLayout.GroupByBox.Hidden = !toolStripButtonAllowGrouping.Checked;
		}

		private void toolStripButtonColumnChooser_Click(object sender, EventArgs e)
		{
			dataGridList.ShowColumnChooser();
		}

		private void toolStripButtonAutoFit_Click(object sender, EventArgs e)
		{
			dataGridList.AutoFitColumns();
		}

		private void toolStripButtonShowInactive_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (dataGridList.ActiveRow != null)
			{
				try
				{
					if (listType == TransactionListType.ARPaymentAllocation)
					{
						if (ErrorHelper.QuestionMessageYesNo("Deleting a payment allocation will delete all the allocations in that batch.\nAre you sure you want to delete the selected payment allocation and all other allocations in the same batch?") != DialogResult.No)
						{
							Factory.ARJournalSystem.DeleteARAllocation(int.Parse(dataGridList.ActiveRow.Cells["ID"].Value.ToString()));
							LoadData();
						}
					}
					else if (listType == TransactionListType.APPaymentAllocation && ErrorHelper.QuestionMessageYesNo("Deleting a payment allocation will delete all the allocations in that batch.\nAre you sure you want to delete the selected payment allocation and all other allocations in the same batch?") != DialogResult.No)
					{
						Factory.APJournalSystem.DeleteAPAllocation(int.Parse(dataGridList.ActiveRow.Cells["ID"].Value.ToString()));
						LoadData();
					}
				}
				catch (CompanyException e2)
				{
					ErrorHelper.ProcessError(e2);
				}
				catch (Exception e3)
				{
					ErrorHelper.ProcessError(e3);
				}
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			dataGridList.AutoSizeColumns();
		}

		private void toolStripButtonMerge_Click(object sender, EventArgs e)
		{
			if (toolStripButtonMerge.Checked)
			{
				dataGridList.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.OnlyWhenSorted;
			}
			else
			{
				dataGridList.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.Never;
			}
		}

		private void comboBoxInsuranceClaimStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listType == TransactionListType.CustomerInsuranceClaim)
			{
				listData = Factory.CustomerInsuranceClaimSystem.GetList(dateControl1.FromDate, dateControl1.ToDate, comboBoxStatus.SelectedID);
			}
		}
	}
}
