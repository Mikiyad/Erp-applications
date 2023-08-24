using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
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
	public class ReminderListForm : Form, IDataForm, IDataList, IExternalReport
	{
		private bool isFirstTimeActivating = true;

		private DataComboType listType = DataComboType.AccountGroup;

		private Hashtable listViewKeys = new Hashtable();

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet listData;

		private XPButton buttonDone;

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

		private DataGridList dataGridList;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonRefresh;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem microsoftExcelToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonColumnChooser;

		private ToolStripButton toolStripButtonAutoFit;

		private ToolStripButton toolStripButtonFitText;

		private ToolStrip toolStrip1;

		private ScreenAccessRight screenRight;

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

		public ReminderListForm()
		{
			InitializeComponent();
			base.Activated += CompanyAccountsListForm_Activated;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.NewMenuClicked += dataGridList_NewMenuClicked;
			dataGridList.OpenMenuClicked += dataGridList_OpenMenuClicked;
			dataGridList.DeleteMenuClicked += dataGridList_DeleteMenuClicked;
			base.FormClosing += ReminderListForm_FormClosing;
		}

		private void ReminderListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				dataGridList.SaveLayout();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridList_DeleteMenuClicked(object sender, EventArgs e)
		{
			Delete();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.ReminderListForm));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDone = new Micromind.UISupport.XPButton();
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
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			dataGridList = new Micromind.UISupport.DataGridList();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument();
			toolStripButtonFitText = new System.Windows.Forms.ToolStripButton();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 467);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(666, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(666, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(558, 8);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 5;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[9]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonAutoFit,
				toolStripButtonFitText,
				toolStripSeparator3,
				toolStripButtonColumnChooser
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(666, 25);
			toolStrip1.TabIndex = 289;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonRefresh.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			toolStripButtonRefresh.Size = new System.Drawing.Size(66, 22);
			toolStripButtonRefresh.Text = "Refresh";
			toolStripButtonRefresh.Click += new System.EventHandler(toolStripButtonRefresh_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(52, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
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
			microsoftExcelToolStripMenuItem.Click += new System.EventHandler(microsoftExcelToolStripMenuItem_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonAutoFit.Image = Micromind.ClientUI.Properties.Resources.AutoFitColumns;
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(92, 22);
			toolStripButtonAutoFit.Text = "Fit to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(75, 22);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
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
			dataGridList.Location = new System.Drawing.Point(12, 42);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(642, 407);
			dataGridList.TabIndex = 290;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			toolStripButtonFitText.Image = Micromind.ClientUI.Properties.Resources.colbestsize;
			toolStripButtonFitText.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFitText.Name = "toolStripButtonFitText";
			toolStripButtonFitText.Size = new System.Drawing.Size(79, 22);
			toolStripButtonFitText.Text = "Fit to Text";
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(666, 507);
			base.Controls.Add(dataGridList);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "ReminderListForm";
			Text = "Reminders";
			base.Load += new System.EventHandler(CompanyAccountsListForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
		}

		private void SetupList()
		{
			try
			{
				if (dataGridList.DisplayLayout != null && dataGridList.DisplayLayout.Bands.Count > 0)
				{
					dataGridList.DisplayLayout.Bands[0].SortedColumns.Add("Reminder", descending: false, groupBy: true);
					dataGridList.DisplayLayout.Bands[0].Columns["SysDocID"].Hidden = true;
					dataGridList.DisplayLayout.Bands[0].Columns["VoucherID"].Header.Caption = "Number";
					dataGridList.DisplayLayout.Bands[0].Columns["PayeeID"].Header.Caption = "Party Code";
					dataGridList.DisplayLayout.Bands[0].Columns["PayeeName"].Header.Caption = "Party Name";
					dataGridList.DisplayLayout.Bands[0].Columns["ARDate"].Header.Caption = "Date";
					dataGridList.DisplayLayout.Bands[0].Columns["ARDueDate"].Header.Caption = "Due Date";
					dataGridList.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
					dataGridList.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
					dataGridList.DisplayLayout.Bands[0].Columns["ARDate"].CellAppearance.TextHAlign = HAlign.Right;
					dataGridList.DisplayLayout.Bands[0].Columns["ARDueDate"].CellAppearance.TextHAlign = HAlign.Right;
					dataGridList.DisplayLayout.Bands[0].Summaries.Add(SummaryType.Sum, dataGridList.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn).Appearance.TextHAlign = HAlign.Right;
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
					PublicFunctions.StartWaiting(this);
					listData = Factory.ReminderSystem.GetReminders(Global.CurrentUser);
					dataGridList.DataSource = listData.Tables[0];
					dataGridList.ApplyFormat();
					SetupList();
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
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				FormHelper formHelper = new FormHelper();
				if (listType == DataComboType.AccountGroup)
				{
					formHelper.EditAccountGroup(selectedID);
				}
				else if (listType == DataComboType.PayrollItem)
				{
					formHelper.EditPayrollItem(selectedID);
				}
				else if (listType == DataComboType.Analysis)
				{
					formHelper.EditAnalysis(selectedID);
				}
				else if (listType == DataComboType.AnalysisGroup)
				{
					formHelper.EditAnalysisGroup(selectedID);
				}
				else if (listType == DataComboType.Area)
				{
					formHelper.EditArea(selectedID);
				}
				else if (listType == DataComboType.Bank)
				{
					formHelper.EditBank(selectedID);
				}
				else if (listType == DataComboType.Benefit)
				{
					formHelper.EditBenefit(selectedID);
				}
				else if (listType == DataComboType.Buyer)
				{
					formHelper.EditBuyer(selectedID);
				}
				else if (listType == DataComboType.CompanyDocType)
				{
					formHelper.EditCompanyDocType(selectedID);
				}
				else if (listType == DataComboType.CompanyDocument)
				{
					formHelper.EditCompanyDocument(selectedID);
				}
				else if (listType == DataComboType.Contact)
				{
					formHelper.EditContact(selectedID);
				}
				else if (listType == DataComboType.Country)
				{
					formHelper.EditCountry(selectedID);
				}
				else if (listType == DataComboType.Currency)
				{
					formHelper.EditCurrency(selectedID);
				}
				else if (listType == DataComboType.CustomerClass)
				{
					formHelper.EditCustomerClass(selectedID);
				}
				else if (listType == DataComboType.CustomerGroup)
				{
					formHelper.EditCustomerGroup(selectedID);
				}
				else if (listType == DataComboType.Deduction)
				{
					formHelper.EditDeduction(selectedID);
				}
				else if (listType == DataComboType.Degree)
				{
					formHelper.EditDegree(selectedID);
				}
				else if (listType == DataComboType.Department)
				{
					formHelper.EditDepartment(selectedID);
				}
				else if (listType == DataComboType.Destination)
				{
					formHelper.EditDestination(selectedID);
				}
				else if (listType == DataComboType.Division)
				{
					formHelper.EditDivision(selectedID);
				}
				else if (listType == DataComboType.EmployeeDocType)
				{
					formHelper.EditEmployeeDocType(selectedID);
				}
				else if (listType == DataComboType.EmployeeGroup)
				{
					formHelper.EditEmployeeGroup(selectedID);
				}
				else if (listType == DataComboType.Grade)
				{
					formHelper.EditGrade(selectedID);
				}
				else if (listType == DataComboType.LeaveType)
				{
					formHelper.EditLeaveType(selectedID);
				}
				else if (listType == DataComboType.Location)
				{
					formHelper.EditLocation(selectedID);
				}
				else if (listType == DataComboType.Nationality)
				{
					formHelper.EditNationality(selectedID);
				}
				else if (listType == DataComboType.PaymentMethod)
				{
					formHelper.EditPaymentMethod(selectedID);
				}
				else if (listType == DataComboType.PaymentTerm)
				{
					formHelper.EditPaymentTerm(selectedID);
				}
				else if (listType == DataComboType.Position)
				{
					formHelper.EditPosition(selectedID);
				}
				else if (listType == DataComboType.PriceLevel)
				{
					formHelper.EditPriceLevel(selectedID);
				}
				else if (listType == DataComboType.ProductBrand)
				{
					formHelper.EditProductBrand(selectedID);
				}
				else if (listType == DataComboType.ProductCategory)
				{
					formHelper.EditProductCategory(selectedID);
				}
				else if (listType == DataComboType.ProductClass)
				{
					formHelper.EditProductClass(selectedID);
				}
				else if (listType == DataComboType.ProductManufacturer)
				{
					formHelper.EditProductManufacturer(selectedID);
				}
				else if (listType == DataComboType.ProductStyle)
				{
					formHelper.EditProductStyle(selectedID);
				}
				else if (listType == DataComboType.Religion)
				{
					formHelper.EditReligion(selectedID);
				}
				else if (listType == DataComboType.Salesperson)
				{
					formHelper.EditSalesperson(selectedID);
				}
				else if (listType == DataComboType.ShippingMethod)
				{
					formHelper.EditShippingMethod(selectedID);
				}
				else if (listType == DataComboType.Skill)
				{
					formHelper.EditSkill(selectedID);
				}
				else if (listType == DataComboType.Sponsor)
				{
					formHelper.EditSponsor(selectedID);
				}
				else if (listType == DataComboType.TenancyContract)
				{
					formHelper.EditTenancyContract(selectedID);
				}
				else if (listType == DataComboType.TradeLicense)
				{
					formHelper.EditTradeLicense(selectedID);
				}
				else if (listType == DataComboType.Unit)
				{
					formHelper.EditUOM(selectedID);
				}
				else if (listType == DataComboType.VendorClass)
				{
					formHelper.EditVendorClass(selectedID);
				}
				else if (listType == DataComboType.VendorGroup)
				{
					formHelper.EditVendorGroup(selectedID);
				}
				else if (listType == DataComboType.Visa)
				{
					formHelper.EditVisa(selectedID);
				}
				else
				{
					ErrorHelper.ErrorMessage("General List Open is not implimented for type: " + listType.ToString());
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
			FormHelper formHelper = new FormHelper();
			string text = "";
			if (listType == DataComboType.AccountGroup)
			{
				formHelper.EditAccountGroup(text);
			}
			else if (listType == DataComboType.PayrollItem)
			{
				formHelper.EditPayrollItem(text);
			}
			else if (listType == DataComboType.Analysis)
			{
				formHelper.EditAnalysis(text);
			}
			else if (listType == DataComboType.AnalysisGroup)
			{
				formHelper.EditAnalysisGroup(text);
			}
			else if (listType == DataComboType.Area)
			{
				formHelper.EditArea(text);
			}
			else if (listType == DataComboType.Bank)
			{
				formHelper.EditBank(text);
			}
			else if (listType == DataComboType.Benefit)
			{
				formHelper.EditBenefit(text);
			}
			else if (listType == DataComboType.Buyer)
			{
				formHelper.EditBuyer(text);
			}
			else if (listType == DataComboType.CompanyDocType)
			{
				formHelper.EditCompanyDocType(text);
			}
			else if (listType == DataComboType.CompanyDocument)
			{
				formHelper.EditCompanyDocument(text);
			}
			else if (listType == DataComboType.Contact)
			{
				formHelper.EditContact(text);
			}
			else if (listType == DataComboType.Country)
			{
				formHelper.EditCountry(text);
			}
			else if (listType == DataComboType.Currency)
			{
				formHelper.EditCurrency(text);
			}
			else if (listType == DataComboType.CustomerClass)
			{
				formHelper.EditCustomerClass(text);
			}
			else if (listType == DataComboType.CustomerGroup)
			{
				formHelper.EditCustomerGroup(text);
			}
			else if (listType == DataComboType.Deduction)
			{
				formHelper.EditDeduction(text);
			}
			else if (listType == DataComboType.Degree)
			{
				formHelper.EditDegree(text);
			}
			else if (listType == DataComboType.Department)
			{
				formHelper.EditDepartment(text);
			}
			else if (listType == DataComboType.Destination)
			{
				formHelper.EditDestination(text);
			}
			else if (listType == DataComboType.Division)
			{
				formHelper.EditDivision(text);
			}
			else if (listType == DataComboType.EmployeeDocType)
			{
				formHelper.EditEmployeeDocType(text);
			}
			else if (listType == DataComboType.EmployeeGroup)
			{
				formHelper.EditEmployeeGroup(text);
			}
			else if (listType == DataComboType.Grade)
			{
				formHelper.EditGrade(text);
			}
			else if (listType == DataComboType.LeaveType)
			{
				formHelper.EditLeaveType(text);
			}
			else if (listType == DataComboType.Location)
			{
				formHelper.EditLocation(text);
			}
			else if (listType == DataComboType.Nationality)
			{
				formHelper.EditNationality(text);
			}
			else if (listType == DataComboType.PaymentMethod)
			{
				formHelper.EditPaymentMethod(text);
			}
			else if (listType == DataComboType.PaymentTerm)
			{
				formHelper.EditPaymentTerm(text);
			}
			else if (listType == DataComboType.Position)
			{
				formHelper.EditPosition(text);
			}
			else if (listType == DataComboType.PriceLevel)
			{
				formHelper.EditPriceLevel(text);
			}
			else if (listType == DataComboType.ProductBrand)
			{
				formHelper.EditProductBrand(text);
			}
			else if (listType == DataComboType.ProductCategory)
			{
				formHelper.EditProductCategory(text);
			}
			else if (listType == DataComboType.ProductClass)
			{
				formHelper.EditProductClass(text);
			}
			else if (listType == DataComboType.ProductManufacturer)
			{
				formHelper.EditProductManufacturer(text);
			}
			else if (listType == DataComboType.ProductStyle)
			{
				formHelper.EditProductStyle(text);
			}
			else if (listType == DataComboType.Religion)
			{
				formHelper.EditReligion(text);
			}
			else if (listType == DataComboType.Salesperson)
			{
				formHelper.EditSalesperson(text);
			}
			else if (listType == DataComboType.ShippingMethod)
			{
				formHelper.EditShippingMethod(text);
			}
			else if (listType == DataComboType.Skill)
			{
				formHelper.EditSkill(text);
			}
			else if (listType == DataComboType.Sponsor)
			{
				formHelper.EditSponsor(text);
			}
			else if (listType == DataComboType.TenancyContract)
			{
				formHelper.EditTenancyContract(text);
			}
			else if (listType == DataComboType.TradeLicense)
			{
				formHelper.EditTradeLicense(text);
			}
			else if (listType == DataComboType.Unit)
			{
				formHelper.EditUOM(text);
			}
			else if (listType == DataComboType.VendorClass)
			{
				formHelper.EditVendorClass(text);
			}
			else if (listType == DataComboType.VendorGroup)
			{
				formHelper.EditVendorGroup(text);
			}
			else if (listType == DataComboType.Visa)
			{
				formHelper.EditVisa(text);
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
					LoadData();
					dataGridList.LoadLayout();
					dataGridList.ApplyUIDesign();
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
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		public void OnActivated()
		{
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
				if (dataGridList.ActiveRow != null)
				{
					if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return "";
					}
					result = dataGridList.ActiveRow.Cells[0].Text.ToString();
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
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

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			Delete();
		}

		private void Delete()
		{
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
	}
}
