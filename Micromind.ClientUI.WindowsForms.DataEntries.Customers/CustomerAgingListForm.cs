using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerAgingListForm : Form, IDataForm, IDataList, IExternalReport
	{
		private DataSet flagData;

		private bool isFirstTimeActivating = true;

		private Hashtable listViewKeys = new Hashtable();

		private bool isReadOnly;

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet customerData;

		private bool showInactiveItems;

		private XPButton buttonDone;

		private XPButton buttonNew;

		private XPButton buttonDelete;

		private Panel panelButtons;

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

		private ToolStripButton toolStripButtonAutoAllocate;

		private CheckBox checkBoxShowZero;

		private ContextMenuStrip contextMenuStripList;

		private ToolStripMenuItem flagsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem toolStripItemClearFlag;

		private ImageList imageList1;

		private ToolStripButton toolStripButtonMerge;

		private ToolStripButton toolStripButtonFitText;

		private ToolStripButton toolStripButtonClearFilter;

		private CheckBox checkBoxFC;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem ledgerToolStripMenuItem;

		private ToolStripMenuItem addActivityToolStripMenuItem;

		private ToolStripMenuItem addFollowUpToolStripMenuItem;

		private ToolStripButton toolStripButtonShowInactive;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem commentsToolStripMenuItem;

		private ToolStrip toolStrip1;

		private ScreenAccessRight screenRight;

		private bool allowflagcustomers;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 1016;

		public ScreenTypes ScreenType => ScreenTypes.List;

		private bool ShowInactiveItems
		{
			get
			{
				return showInactiveItems;
			}
			set
			{
				showInactiveItems = value;
			}
		}

		public CustomerAgingListForm()
		{
			InitializeComponent();
			base.Activated += CustomerBalanceListForm_Activated;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.NewMenuClicked += dataGridList_NewMenuClicked;
			dataGridList.OpenMenuClicked += dataGridList_OpenMenuClicked;
			dataGridList.DeleteMenuClicked += dataGridList_DeleteMenuClicked;
			base.FormClosing += CustomerBalanceListForm_FormClosing;
			dataGridList.DropDownMenu.Opening += contextMenuStripList_Opening;
			AddMenuItems();
			dataGridList.InitializeLayout += dataGridList_InitializeLayout;
			dataGridList.InitializeRow += dataGridList_InitializeRow;
			dataGridList.AfterCellUpdate += dataGridList_AfterCellUpdate;
			dataGridList.ClickCellButton += dataGridList_ClickCellButton;
			dataGridList.BeforeRowFilterDropDownPopulate += DataGridList_BeforeRowFilterDropDownPopulate;
			dataGridList.InitializeRow += DataGridList_InitializeRow;
		}

		private void DataGridList_BeforeRowFilterDropDownPopulate(object sender, BeforeRowFilterDropDownPopulateEventArgs e)
		{
			try
			{
				if (e.Column.Key == "F")
				{
					e.Handled = true;
					foreach (DataRow row in flagData.Tables[0].Rows)
					{
						int argb = int.Parse(row["Color"].ToString());
						ValueListItem valueListItem = new ValueListItem(row["Name"].ToString());
						valueListItem.Appearance.Image = ImageHelper.CreateImage(Color.FromArgb(argb), 12, 16);
						e.ValueList.ValueListItems.Add(valueListItem);
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FlagItem_Click(object sender, EventArgs e)
		{
			try
			{
				ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
				int num = int.Parse(((NameValue)toolStripMenuItem.Tag).Name.ToString());
				if (Factory.EntityFlagSystem.SetFlag(1, GetSelectedID(), num, !toolStripMenuItem.Checked))
				{
					if (toolStripMenuItem.Checked)
					{
						customerData.Tables["Entity_Flag_Detail"].Rows.Add(GetSelectedID(), num, 1, toolStripMenuItem.Text, int.Parse(((NameValue)toolStripMenuItem.Tag).ID.ToString()));
					}
					else
					{
						DataRow[] array = customerData.Tables["Entity_Flag_Detail"].Select("EntityID = '" + GetSelectedID() + "' AND FlagID = " + num);
						customerData.Tables["Entity_Flag_Detail"].Rows.Remove(array[0]);
					}
					SetRowFlags(dataGridList.ActiveRow);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void DataGridList_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			try
			{
				SetRowFlags(e.Row);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetRowFlags(UltraGridRow row)
		{
			string str = row.Cells["CustomerID"].Value.ToString();
			DataRow[] array = customerData.Tables["Entity_Flag_Detail"].Select("EntityID = '" + str + "'");
			if (array.Length != 0)
			{
				string text = "";
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < array.Length; i = checked(i + 1))
				{
					int argb = int.Parse(array[i]["Color"].ToString());
					if (text != "")
					{
						text += " ";
					}
					text += array[i]["FlagName"].ToString();
					Bitmap value = ImageHelper.CreateImage(Color.FromArgb(argb), 12, 16);
					arrayList.Add(value);
				}
				Bitmap imageBackground = ImageHelper.CombineBitmaps((Bitmap[])arrayList.ToArray(typeof(Bitmap)));
				row.Cells["F"].Appearance.ImageBackground = imageBackground;
				row.Cells["F"].Value = text;
				row.Cells["F"].Appearance.ForeColor = Color.Transparent;
				row.Cells["F"].ActiveAppearance.ForeColor = Color.Transparent;
				row.Cells["F"].SelectedAppearance.ForeColor = Color.Transparent;
			}
			else
			{
				row.Cells["F"].Appearance.ImageBackground = null;
				row.Cells["F"].Value = "";
			}
		}

		private void AddMenuItems()
		{
			try
			{
				dataGridList.DropDownMenu.Items.Insert(1, contextMenuStripList.Items["ledgerToolStripMenuItem"]);
				dataGridList.DropDownMenu.Items.Insert(2, contextMenuStripList.Items["salesStatisticsToolStripMenuItem"]);
				dataGridList.DropDownMenu.Items.Insert(3, new ToolStripSeparator());
				dataGridList.DropDownMenu.Items.Insert(4, contextMenuStripList.Items["flagsToolStripMenuItem"]);
				dataGridList.DropDownMenu.Items.Insert(5, contextMenuStripList.Items["commentsToolStripMenuItem"]);
				if (GlobalRules.IsModuleAvailable(AxolonModules.CRM))
				{
					dataGridList.DropDownMenu.Items.Insert(6, new ToolStripSeparator());
					dataGridList.DropDownMenu.Items.Insert(7, contextMenuStripList.Items["addActivityToolStripMenuItem"]);
					dataGridList.DropDownMenu.Items.Insert(8, contextMenuStripList.Items["addFollowUpToolStripMenuItem"]);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void dataGridList_ClickCellButton(object sender, CellEventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
			FormActivator.FollowupDetailsFormObj.SourceSysDocID = "";
			FormActivator.FollowupDetailsFormObj.SourceVoucherID = dataGridList.ActiveRow.Cells["CustomerID"].Value.ToString();
			FormActivator.FollowupDetailsFormObj.CRMType = CRMRelatedTypes.Customer;
			FormActivator.FollowupDetailsFormObj.ThisfollowupBy = "";
			FormActivator.FollowupDetailsFormObj.ThisfollowupDate = DateTime.Now;
			FormActivator.FollowupDetailsFormObj.ThisfollowupTime = DateTime.Now;
			FormActivator.FollowupDetailsFormObj.Status = 1;
			FormActivator.FollowupDetailsFormObj.ActiveNewRecord();
		}

		private void dataGridList_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "CollectionRemarks")
			{
				Factory.DatabaseSystem.UpdateFieldValue("Customer", "CollectionRemarks", Global.CurrentUser + ":" + e.Cell.Value, typeof(string), "CustomerID", e.Cell.Row.Cells["CustomerID"].Value.ToString());
			}
		}

		private void dataGridList_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void dataGridList_InitializeRow(object sender, InitializeRowEventArgs e)
		{
		}

		private void CustomerBalanceListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				UserPreferences.SaveCurrentUserSetting(base.Name + "MergeCell", toolStripButtonMerge.Checked);
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
			ShowLedger();
		}

		private void ShowLedger()
		{
			if (dataGridList.ActiveRow.IsDataRow)
			{
				string selectedID = GetSelectedID();
				CustomerLedgerForm customerLedgerForm = new CustomerLedgerForm();
				customerLedgerForm.SelectedID = selectedID;
				customerLedgerForm.Show();
			}
		}

		private void CustomerBalanceListForm_Activated(object sender, EventArgs e)
		{
			if (isFirstTimeActivating)
			{
				isFirstTimeActivating = false;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && customerData != null)
			{
				customerData.Dispose();
				customerData = null;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CustomerAgingListForm));
			panelButtons = new System.Windows.Forms.Panel();
			checkBoxFC = new System.Windows.Forms.CheckBox();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonDone = new Micromind.UISupport.XPButton();
			buttonOpen = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			contextMenuStripList = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			imageList1 = new System.Windows.Forms.ImageList(components);
			toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			microsoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonShowInactive = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAllowGrouping = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFitText = new System.Windows.Forms.ToolStripButton();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMerge = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAutoAllocate = new System.Windows.Forms.ToolStripButton();
			toolStripButtonClearFilter = new System.Windows.Forms.ToolStripButton();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ledgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			addActivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			addFollowUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			commentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			flagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripItemClearFlag = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			contextMenuStripList.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(checkBoxFC);
			panelButtons.Controls.Add(checkBoxShowZero);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Controls.Add(buttonOpen);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 559);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(987, 40);
			panelButtons.TabIndex = 1;
			checkBoxFC.AutoSize = true;
			checkBoxFC.Location = new System.Drawing.Point(539, 11);
			checkBoxFC.Name = "checkBoxFC";
			checkBoxFC.Size = new System.Drawing.Size(106, 17);
			checkBoxFC.TabIndex = 4;
			checkBoxFC.Text = "Foreign Currency";
			checkBoxFC.UseVisualStyleBackColor = true;
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(335, 11);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(198, 17);
			checkBoxShowZero.TabIndex = 3;
			checkBoxShowZero.Text = "Include customers with zero balance";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(987, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 22);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(879, 8);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 22);
			buttonDone.TabIndex = 5;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			buttonOpen.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpen.BackColor = System.Drawing.Color.DarkGray;
			buttonOpen.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpen.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpen.Location = new System.Drawing.Point(12, 8);
			buttonOpen.Name = "buttonOpen";
			buttonOpen.Size = new System.Drawing.Size(96, 22);
			buttonOpen.TabIndex = 0;
			buttonOpen.Text = "&Open";
			buttonOpen.UseVisualStyleBackColor = false;
			buttonOpen.Click += new System.EventHandler(buttonGotoItem_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 22);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonShowInactive,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButtonFitText,
				toolStripSeparator3,
				toolStripButtonColumnChooser,
				toolStripButtonMerge,
				toolStripButtonAutoAllocate,
				toolStripButtonClearFilter
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(987, 31);
			toolStrip1.TabIndex = 289;
			toolStrip1.Text = "toolStrip1";
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
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
			dataGridList.Location = new System.Drawing.Point(12, 37);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(963, 511);
			dataGridList.TabIndex = 290;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			contextMenuStripList.Items.AddRange(new System.Windows.Forms.ToolStripItem[7]
			{
				salesStatisticsToolStripMenuItem,
				ledgerToolStripMenuItem,
				addActivityToolStripMenuItem,
				addFollowUpToolStripMenuItem,
				commentsToolStripMenuItem,
				toolStripSeparator6,
				flagsToolStripMenuItem
			});
			contextMenuStripList.Name = "contextMenuStripList";
			contextMenuStripList.Size = new System.Drawing.Size(158, 142);
			contextMenuStripList.Opening += new System.ComponentModel.CancelEventHandler(contextMenuStripList_Opening);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(154, 6);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "colorcategory.png");
			imageList1.Images.SetKeyName(1, "followup.gif");
			toolStripButtonRefresh.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			toolStripButtonRefresh.Size = new System.Drawing.Size(73, 28);
			toolStripButtonRefresh.Text = "Refresh";
			toolStripButtonRefresh.Click += new System.EventHandler(toolStripButtonRefresh_Click);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				microsoftExcelToolStripMenuItem
			});
			toolStripDropDownButton1.Image = Micromind.ClientUI.Properties.Resources.Export;
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(76, 28);
			toolStripDropDownButton1.Text = "Export";
			microsoftExcelToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Excel;
			microsoftExcelToolStripMenuItem.Name = "microsoftExcelToolStripMenuItem";
			microsoftExcelToolStripMenuItem.Size = new System.Drawing.Size(160, 30);
			microsoftExcelToolStripMenuItem.Text = "Microsoft Excel";
			microsoftExcelToolStripMenuItem.Click += new System.EventHandler(microsoftExcelToolStripMenuItem_Click);
			toolStripButtonShowInactive.CheckOnClick = true;
			toolStripButtonShowInactive.Image = Micromind.ClientUI.Properties.Resources.ShowInactive;
			toolStripButtonShowInactive.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowInactive.Name = "toolStripButtonShowInactive";
			toolStripButtonShowInactive.Size = new System.Drawing.Size(103, 28);
			toolStripButtonShowInactive.Text = "Show Inactive";
			toolStripButtonShowInactive.Click += new System.EventHandler(toolStripButtonShowInactive_Click);
			toolStripButtonAllowGrouping.CheckOnClick = true;
			toolStripButtonAllowGrouping.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonAllowGrouping.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAllowGrouping.Name = "toolStripButtonAllowGrouping";
			toolStripButtonAllowGrouping.Size = new System.Drawing.Size(106, 28);
			toolStripButtonAllowGrouping.Text = "Allow Grouping";
			toolStripButtonAllowGrouping.Click += new System.EventHandler(toolStripButtonAllowGrouping_Click);
			toolStripButtonAutoFit.Image = Micromind.ClientUI.Properties.Resources.autofit;
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(96, 28);
			toolStripButtonAutoFit.Text = "Fit to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButtonFitText.Image = Micromind.ClientUI.Properties.Resources.colbestsize;
			toolStripButtonFitText.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFitText.Name = "toolStripButtonFitText";
			toolStripButtonFitText.Size = new System.Drawing.Size(85, 28);
			toolStripButtonFitText.Text = "Fit to Text";
			toolStripButtonFitText.Click += new System.EventHandler(toolStripButtonFitText_Click);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(75, 28);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
			toolStripButtonMerge.CheckOnClick = true;
			toolStripButtonMerge.Image = Micromind.ClientUI.Properties.Resources.merge;
			toolStripButtonMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMerge.Name = "toolStripButtonMerge";
			toolStripButtonMerge.Size = new System.Drawing.Size(90, 28);
			toolStripButtonMerge.Text = "Merge Cells";
			toolStripButtonMerge.Click += new System.EventHandler(toolStripButtonMerge_Click);
			toolStripButtonAutoAllocate.Image = Micromind.ClientUI.Properties.Resources.allocate;
			toolStripButtonAutoAllocate.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoAllocate.Name = "toolStripButtonAutoAllocate";
			toolStripButtonAutoAllocate.Size = new System.Drawing.Size(99, 28);
			toolStripButtonAutoAllocate.Text = "Auto Allocate";
			toolStripButtonAutoAllocate.ToolTipText = "Auto allocate unallocated amounts";
			toolStripButtonAutoAllocate.Click += new System.EventHandler(toolStripButtonAutoAllocate_Click);
			toolStripButtonClearFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonClearFilter.Image = Micromind.ClientUI.Properties.Resources.clearfilter;
			toolStripButtonClearFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonClearFilter.Name = "toolStripButtonClearFilter";
			toolStripButtonClearFilter.Size = new System.Drawing.Size(28, 28);
			toolStripButtonClearFilter.Text = "Clear All Filters";
			toolStripButtonClearFilter.Click += new System.EventHandler(toolStripButtonClearFilter_Click);
			salesStatisticsToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Chart_48;
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			salesStatisticsToolStripMenuItem.Click += new System.EventHandler(salesStatisticsToolStripMenuItem_Click);
			ledgerToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.ledger;
			ledgerToolStripMenuItem.Name = "ledgerToolStripMenuItem";
			ledgerToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			ledgerToolStripMenuItem.Text = "Journal Details...";
			ledgerToolStripMenuItem.Click += new System.EventHandler(ledgerToolStripMenuItem_Click);
			addActivityToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.activity;
			addActivityToolStripMenuItem.Name = "addActivityToolStripMenuItem";
			addActivityToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			addActivityToolStripMenuItem.Text = "Add Activity...";
			addActivityToolStripMenuItem.Click += new System.EventHandler(addActivityToolStripMenuItem_Click);
			addFollowUpToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.followup;
			addFollowUpToolStripMenuItem.Name = "addFollowUpToolStripMenuItem";
			addFollowUpToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			addFollowUpToolStripMenuItem.Text = "Add FollowUp...";
			addFollowUpToolStripMenuItem.Click += new System.EventHandler(addFollowUpToolStripMenuItem_Click);
			commentsToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.comment;
			commentsToolStripMenuItem.Name = "commentsToolStripMenuItem";
			commentsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			commentsToolStripMenuItem.Text = "Comments...";
			commentsToolStripMenuItem.Click += new System.EventHandler(commentsToolStripMenuItem1_Click);
			flagsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				toolStripSeparator4,
				toolStripItemClearFlag
			});
			flagsToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.colorcategory;
			flagsToolStripMenuItem.Name = "flagsToolStripMenuItem";
			flagsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			flagsToolStripMenuItem.Text = "Flags";
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(138, 6);
			toolStripItemClearFlag.Name = "toolStripItemClearFlag";
			toolStripItemClearFlag.Size = new System.Drawing.Size(141, 22);
			toolStripItemClearFlag.Tag = "0";
			toolStripItemClearFlag.Text = "Clear All Flags";
			toolStripItemClearFlag.Click += new System.EventHandler(toolStripItemFlag_Click);
			base.AcceptButton = buttonOpen;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(987, 599);
			base.Controls.Add(dataGridList);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "CustomerAgingListForm";
			Text = "Customer Aging List";
			base.Load += new System.EventHandler(CustomerBalanceListForm_Load);
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			contextMenuStripList.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			Global.GlobalSettings.LoadFormProperties(this);
			toolStripButtonMerge.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "MergeCell", defaultValue: false);
		}

		private void LoadData()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					PublicFunctions.StartWaiting(this);
					customerData = Factory.CustomerSystem.GetCustomerAgingBalanceList(checkBoxShowZero.Checked, checkBoxFC.Checked, toolStripButtonShowInactive.Checked);
					DataSet dataSet = new DataSet();
					if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.ShowCollectionCustomersOnly) && !Global.isUserAdmin)
					{
						DataRow[] rows = customerData.Tables[0].Select("[Collection User] ='" + Global.CurrentUser + "'");
						dataSet.Merge(rows);
					}
					else
					{
						DataRow[] rows2 = customerData.Tables[0].Select("CustomerID IS  NOT NULL");
						dataSet.Merge(rows2);
					}
					if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables.Contains("Customer"))
					{
						customerData.Tables.Remove("Customer");
					}
					customerData.Merge(dataSet);
					dataGridList.DataSource = customerData.Tables["Customer"];
					dataGridList.ApplyFormat();
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

		private void HighlightOverdueRows()
		{
		}

		private void HideShowColumns()
		{
			if (dataGridList.DisplayLayout.Bands.Count != 0)
			{
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["InsRemarks"];
				UltraGridColumn ultraGridColumn2 = dataGridList.DisplayLayout.Bands[0].Columns["InsStatus"];
				UltraGridColumn ultraGridColumn3 = dataGridList.DisplayLayout.Bands[0].Columns["InsApprovedAmount"];
				UltraGridColumn ultraGridColumn4 = dataGridList.DisplayLayout.Bands[0].Columns["PaymentTermID"];
				UltraGridColumn ultraGridColumn5 = dataGridList.DisplayLayout.Bands[0].Columns["CreditAmount"];
				UltraGridColumn ultraGridColumn6 = dataGridList.DisplayLayout.Bands[0].Columns["CreditReviewDate"];
				UltraGridColumn ultraGridColumn7 = dataGridList.DisplayLayout.Bands[0].Columns["CreditReviewBy"];
				UltraGridColumn ultraGridColumn8 = dataGridList.DisplayLayout.Bands[0].Columns["AcceptPDC"];
				UltraGridColumn ultraGridColumn9 = dataGridList.DisplayLayout.Bands[0].Columns["IsCustomerSince"];
				UltraGridColumn ultraGridColumn10 = dataGridList.DisplayLayout.Bands[0].Columns["ContactName"];
				UltraGridColumn ultraGridColumn11 = dataGridList.DisplayLayout.Bands[0].Columns["ContactTitle"];
				bool flag2 = dataGridList.DisplayLayout.Bands[0].Columns["PostalCode"].Hidden = true;
				bool flag4 = ultraGridColumn11.Hidden = flag2;
				bool flag6 = ultraGridColumn10.Hidden = flag4;
				bool flag8 = ultraGridColumn9.Hidden = flag6;
				bool flag10 = ultraGridColumn8.Hidden = flag8;
				bool flag12 = ultraGridColumn7.Hidden = flag10;
				bool flag14 = ultraGridColumn6.Hidden = flag12;
				bool flag16 = ultraGridColumn5.Hidden = flag14;
				bool flag18 = ultraGridColumn4.Hidden = flag16;
				bool flag20 = ultraGridColumn3.Hidden = flag18;
				bool hidden = ultraGridColumn2.Hidden = flag20;
				ultraGridColumn.Hidden = hidden;
				dataGridList.DisplayLayout.Bands[0].Columns["Email"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["LastSale"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["LastPayment"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Mobile"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Phone1"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Phone2"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Fax"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Followup"].Hidden = true;
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
				FormActivator.CustomerDetailsFormObj.LoadData(selectedID);
				FormActivator.BringFormToFront(FormActivator.CustomerDetailsFormObj);
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
			FormActivator.BringFormToFront(FormActivator.CustomerDetailsFormObj);
		}

		private void buttonGotoItem_Click(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void CustomerBalanceListForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				LoadFlags();
				Init();
				LoadData();
				SetupGrid();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadFlags()
		{
			try
			{
				flagData = Factory.EntityFlagSystem.GetEntityFlagList(EntityTypesEnum.Customers);
				int num = 0;
				foreach (DataRow row in flagData.Tables[0].Rows)
				{
					Bitmap image = new Bitmap(12, 16);
					using (Graphics graphics = Graphics.FromImage(image))
					{
						graphics.Clear(Color.FromArgb(int.Parse(row["Color"].ToString())));
					}
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(row["Name"].ToString(), image);
					toolStripMenuItem.CheckOnClick = true;
					toolStripMenuItem.Click += FlagItem_Click;
					flagsToolStripMenuItem.DropDownItems.Insert(num, toolStripMenuItem);
					num = checked(num + 1);
					toolStripMenuItem.Tag = new NameValue(row["Code"].ToString(), row["Color"].ToString());
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupGrid()
		{
			try
			{
				HideShowColumns();
				dataGridList.LoadLayout();
				dataGridList.ApplyUIDesign();
				new DataHelper();
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingByDueDate, defaultValue: true);
				bool companyOption = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging1, defaultValue: true);
				bool companyOption2 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging2, defaultValue: true);
				bool companyOption3 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging3, defaultValue: true);
				bool companyOption4 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging4, defaultValue: true);
				bool companyOption5 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging5, defaultValue: true);
				bool companyOption6 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging6, defaultValue: true);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom0, 0);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom1, 1);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom2, 31);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom3, 61);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom4, 91);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom5, 121);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom6, 151);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom7, 181);
				int companyOption7 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo0, 0);
				int companyOption8 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo1, 30);
				int companyOption9 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo2, 60);
				int companyOption10 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo3, 90);
				int companyOption11 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo4, 120);
				int companyOption12 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo5, 150);
				int companyOption13 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo6, 180);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo7, 999);
				string companyOption14 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName0, "Current");
				string companyOption15 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName1, "1 - 30 Days");
				string companyOption16 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName2, "31 - 60 Days");
				string companyOption17 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName3, "61 - 90 Days");
				string companyOption18 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName4, "91 - 120 Days");
				string companyOption19 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName5, "121 - 150 Days");
				string companyOption20 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName6, "151 - 180 Days");
				string companyOption21 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName7, "Over 180 Days");
				int num = 0;
				num = (companyOption6 ? companyOption13 : (companyOption5 ? companyOption12 : (companyOption4 ? companyOption11 : (companyOption3 ? companyOption10 : (companyOption2 ? companyOption9 : ((!companyOption) ? companyOption7 : companyOption8))))));
				companyOption21 = "Over " + num + " Days";
				dataGridList.DisplayLayout.Bands[0].Columns["FollowUp"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridList.DisplayLayout.Bands[0].Columns["FollowUp"].CellButtonAppearance.Image = imageList1.Images[1];
				dataGridList.DisplayLayout.Bands[0].Columns["FollowUp"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridList.DisplayLayout.Bands[0].Columns["FollowUp"].Width = 20;
				dataGridList.DisplayLayout.Bands[0].Columns["FollowUp"].LockedWidth = true;
				dataGridList.DisplayLayout.Bands[0].Columns["FollowUp"].MaxLength = 20;
				dataGridList.DisplayLayout.Bands[0].Columns["FollowUp"].MinLength = 20;
				dataGridList.DisplayLayout.Bands[0].Columns["FollowUp"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["CustomerID"].Header.Caption = "Customer Code";
				dataGridList.DisplayLayout.Bands[0].Columns["CustomerName"].Header.Caption = "Customer Name";
				dataGridList.DisplayLayout.Bands[0].Columns["LastSale"].Header.Caption = "Last Sale";
				dataGridList.DisplayLayout.Bands[0].Columns["LastPayment"].Header.Caption = "Last Pay";
				dataGridList.DisplayLayout.Bands[0].Columns["InsStatus"].Header.Caption = "Ins Status";
				dataGridList.DisplayLayout.Bands[0].Columns["InsapprovedAmount"].Header.Caption = "Insured Amount";
				dataGridList.DisplayLayout.Bands[0].Columns["TotalDue"].Header.Caption = "Total Due";
				dataGridList.DisplayLayout.Bands[0].Columns["LastSale"].Format = "#,##0 days";
				dataGridList.DisplayLayout.Bands[0].Columns["LastPayment"].Format = "#,##0 days";
				dataGridList.DisplayLayout.Bands[0].Columns["LastSale"].NullText = "None";
				dataGridList.DisplayLayout.Bands[0].Columns["LastPayment"].NullText = "None";
				dataGridList.DisplayLayout.Bands[0].Columns["CurrentBalance"].Header.Caption = companyOption14;
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month1"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["Month1"].Header.Caption = companyOption15;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month2"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["Month2"].Header.Caption = companyOption16;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month3"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["Month3"].Header.Caption = companyOption17;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month4"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["Month4"].Header.Caption = companyOption18;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month5"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["Month5"].Header.Caption = companyOption19;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month6"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["Month6"].Header.Caption = companyOption20;
				}
				dataGridList.DisplayLayout.Bands[0].Columns["Over"].Header.Caption = companyOption21;
				dataGridList.DisplayLayout.Bands[0].Columns["PaymentTermID"].Header.Caption = "Payment Term";
				dataGridList.DisplayLayout.Bands[0].Columns["CreditAmount"].Header.Caption = "Credit Limit";
				dataGridList.DisplayLayout.Bands[0].Columns["CreditReviewDate"].Header.Caption = "Review Date";
				dataGridList.DisplayLayout.Bands[0].Columns["CreditReviewBy"].Header.Caption = "Review By";
				dataGridList.DisplayLayout.Bands[0].Columns["AcceptPDC"].Header.Caption = "Accept PDC";
				dataGridList.DisplayLayout.Bands[0].Columns["IsCustomerSince"].Header.Caption = "Customer Since";
				dataGridList.DisplayLayout.Bands[0].Columns["NetOffPDC"].Header.Caption = "Net Off PDC";
				dataGridList.DisplayLayout.Bands[0].Columns["ContactName"].Header.Caption = "Contact Name";
				dataGridList.DisplayLayout.Bands[0].Columns["ContactTitle"].Header.Caption = "Contact Title";
				dataGridList.DisplayLayout.Bands[0].Columns["PostalCode"].Header.Caption = "Postal Code";
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["F"];
				ultraGridColumn.Header.Caption = "";
				ultraGridColumn.Header.Appearance.Image = imageList1.Images[0];
				ultraGridColumn.Header.Appearance.ImageVAlign = VAlign.Middle;
				ultraGridColumn.Hidden = false;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].Width = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].MaxWidth = 80;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].MinWidth = 80;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].LockedWidth = false;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].AllowRowFiltering = DefaultableBoolean.True;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(0, "None");
				valueList.ValueListItems.Add(1, "1");
				valueList.ValueListItems.Add(2, "2");
				valueList.ValueListItems.Add(3, "3");
				valueList.ValueListItems.Add(4, "4");
				valueList.ValueListItems.Add(5, "5");
				valueList.ValueListItems.Add(6, "6");
				valueList.ValueListItems.Add(7, "7");
				valueList.ValueListItems.Add(8, "8");
				valueList.ValueListItems.Add(9, "9");
				valueList.ValueListItems.Add(10, "10");
				dataGridList.DisplayLayout.Bands[0].Columns["Rating"].ValueList = valueList;
				dataGridList.DisplayLayout.Bands[0].Columns["Rating"].CellActivation = Activation.ActivateOnly;
				dataGridList.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridList.DisplayLayout.Bands[0].Override.FixedHeaderIndicator = FixedHeaderIndicator.Button;
				dataGridList.DisplayLayout.UseFixedHeaders = true;
				dataGridList.FormatAllNumericColumns(new string[2]
				{
					"LastPayment",
					"LastSale"
				});
				dataGridList.AddNumericColumnsSummary(null, SummaryType.Sum);
				dataGridList.DisplayLayout.Bands[0].Columns["F"].AllowRowSummaries = AllowRowSummaries.False;
				dataGridList.DisplayLayout.Bands[0].Columns["CollectionRemarks"].CellClickAction = CellClickAction.EditAndSelectText;
				dataGridList.DisplayLayout.Bands[0].Columns["CollectionRemarks"].CellActivation = Activation.AllowEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["CollectionRemarks"].CellAppearance.BackColor = Color.LightYellow;
				dataGridList.DisplayLayout.Bands[0].Columns["CollectionRemarks"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				dataGridList.DisplayLayout.Bands[0].Columns["CollectionRemarks"].Header.Caption = "Collection Remarks";
				dataGridList.DisplayLayout.Bands[0].Columns["CollectionRemarks"].MaxLength = 255;
				dataGridList.DisplayLayout.Bands[0].Columns["CollectionRemarks"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
				dataGridList.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].CellDisplayStyle = CellDisplayStyle.FormattedText;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].Width = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].MaxWidth = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].MinWidth = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].LockedWidth = true;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].AllowRowFiltering = DefaultableBoolean.False;
				dataGridList.SetInactiveColumn("I");
				dataGridList.AddColumnRowCount(dataGridList.DisplayLayout.Bands[0].Columns["CustomerID"]);
				ValueList valueList2 = new ValueList();
				valueList2.ValueListItems.Add(1, "Not-Insured");
				valueList2.ValueListItems.Add(2, "Processing");
				valueList2.ValueListItems.Add(3, "Insured");
				valueList2.ValueListItems.Add(4, "Sublimit");
				valueList2.ValueListItems.Add(5, "Rejected");
				valueList2.ValueListItems.Add(6, "Hold");
				valueList2.ValueListItems.Add(7, "Cancelled");
				dataGridList.DisplayLayout.Bands[0].Columns["InsStatus"].ValueList = valueList2;
				dataGridList.DisplayLayout.Bands[0].Columns["InsStatus"].CellActivation = Activation.NoEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["InsStatus"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;
				if (!allowflagcustomers)
				{
					flagsToolStripMenuItem.Visible = false;
				}
				else
				{
					flagsToolStripMenuItem.Visible = true;
				}
			}
			catch (Exception e)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		public void OnActivated()
		{
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.Allowflagcustomers))
			{
				allowflagcustomers = false;
			}
			else
			{
				allowflagcustomers = true;
			}
		}

		private string GetSelectedID()
		{
			if (dataGridList.ActiveRow == null)
			{
				return "";
			}
			if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
			{
				return "";
			}
			return dataGridList.ActiveRow.Cells["CustomerID"].Text.ToString();
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
			if (!isReadOnly)
			{
				string selectedID = GetSelectedID();
				if (selectedID != "")
				{
					try
					{
						if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?") == DialogResult.Yes)
						{
							PublicFunctions.StartWaiting(this);
							if (Factory.CustomerSystem.DeleteCustomer(selectedID))
							{
								try
								{
									GetSelectedItem().Delete(displayPrompt: false);
								}
								catch
								{
								}
							}
						}
					}
					catch (SqlException ex)
					{
						if (ex.Number == 547)
						{
							ErrorHelper.ErrorMessage("Cannot delete this item because it is in use or referenced by other records.");
						}
						else
						{
							ErrorHelper.ProcessError(ex);
						}
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
		}

		public void RefreshData()
		{
		}

		private string GetDocumentTitle()
		{
			return "Customer List";
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

		private void toolStripButtonAutoAllocate_Click(object sender, EventArgs e)
		{
			AutoAllocate();
		}

		private void AutoAllocate()
		{
			foreach (UltraGridRow row in dataGridList.Rows)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal result4 = default(decimal);
				decimal result5 = default(decimal);
				decimal result6 = default(decimal);
				decimal result7 = default(decimal);
				decimal result8 = default(decimal);
				decimal result9 = default(decimal);
				decimal.TryParse(row.Cells["Unallocated"].Value.ToString(), out result);
				if (!(result == 0m))
				{
					decimal.TryParse(row.Cells["Over"].Value.ToString(), out result2);
					if (result > 0m && result2 > 0m)
					{
						if (result2 > result)
						{
							result2 -= result;
							result = default(decimal);
						}
						else
						{
							result -= result2;
							result2 = default(decimal);
						}
						row.Cells["Over"].Value = result2;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month6"))
					{
						decimal.TryParse(row.Cells["Month6"].Value.ToString(), out result3);
						if (result > 0m && result3 > 0m)
						{
							if (result3 > result)
							{
								result3 -= result;
								result = default(decimal);
							}
							else
							{
								result -= result3;
								result3 = default(decimal);
							}
						}
						row.Cells["Month6"].Value = result3;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month5"))
					{
						decimal.TryParse(row.Cells["Month5"].Value.ToString(), out result4);
						if (result > 0m && result4 > 0m)
						{
							if (result4 > result)
							{
								result4 -= result;
								result = default(decimal);
							}
							else
							{
								result -= result4;
								result4 = default(decimal);
							}
						}
						row.Cells["Month5"].Value = result4;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month4"))
					{
						decimal.TryParse(row.Cells["Month4"].Value.ToString(), out result5);
						if (result > 0m && result5 > 0m)
						{
							if (result5 > result)
							{
								result5 -= result;
								result = default(decimal);
							}
							else
							{
								result -= result5;
								result5 = default(decimal);
							}
						}
						row.Cells["Month4"].Value = result5;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month3"))
					{
						decimal.TryParse(row.Cells["Month3"].Value.ToString(), out result6);
						if (result > 0m && result6 > 0m)
						{
							if (result6 > result)
							{
								result6 -= result;
								result = default(decimal);
							}
							else
							{
								result -= result6;
								result6 = default(decimal);
							}
						}
						row.Cells["Month3"].Value = result6;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month2"))
					{
						decimal.TryParse(row.Cells["Month2"].Value.ToString(), out result7);
						if (result > 0m && result7 > 0m)
						{
							if (result7 > result)
							{
								result7 -= result;
								result = default(decimal);
							}
							else
							{
								result -= result7;
								result7 = default(decimal);
							}
						}
						row.Cells["Month2"].Value = result7;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Month1"))
					{
						decimal.TryParse(row.Cells["Month1"].Value.ToString(), out result8);
						if (result > 0m && result8 > 0m)
						{
							if (result8 > result)
							{
								result8 -= result;
								result = default(decimal);
							}
							else
							{
								result -= result8;
								result8 = default(decimal);
							}
						}
						row.Cells["Month1"].Value = result8;
					}
					decimal.TryParse(row.Cells["CurrentBalance"].Value.ToString(), out result9);
					if (result > 0m && result9 > 0m)
					{
						if (result9 > result)
						{
							result9 -= result;
							result = default(decimal);
						}
						else
						{
							result -= result9;
							result9 = default(decimal);
						}
						row.Cells["CurrentBalance"].Value = result9;
					}
					row.Cells["Unallocated"].Value = result;
				}
			}
		}

		private void toolStripItemFlag_Click(object sender, EventArgs e)
		{
			try
			{
				if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Remove all flags?") == DialogResult.Yes && Factory.EntityFlagSystem.SetFlag(1, GetSelectedID(), -1, removeFlag: true))
				{
					DataRow[] array = customerData.Tables["Entity_Flag_Detail"].Select("EntityID = '" + GetSelectedID() + "' AND EntityType= 1");
					for (int i = 0; i < array.Length; i = checked(i + 1))
					{
						customerData.Tables["Entity_Flag_Detail"].Rows.Remove(array[i]);
					}
					SetRowFlags(dataGridList.ActiveRow);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void contextMenuStripList_Opening(object sender, CancelEventArgs e)
		{
			if (GetSelectedItem() != null)
			{
				foreach (object dropDownItem in flagsToolStripMenuItem.DropDownItems)
				{
					if (!(dropDownItem.GetType() == typeof(ToolStripSeparator)))
					{
						ToolStripMenuItem toolStripMenuItem = dropDownItem as ToolStripMenuItem;
						int num = int.Parse(toolStripMenuItem.Tag.ToString());
						if (customerData.Tables["Entity_Flag_Detail"].Select("EntityID = '" + GetSelectedID() + "' AND FlagID = " + num).Length == 0)
						{
							toolStripMenuItem.Checked = false;
						}
						else
						{
							toolStripMenuItem.Checked = true;
						}
					}
				}
			}
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

		private void toolStripButtonFitText_Click(object sender, EventArgs e)
		{
			dataGridList.AutoSizeColumns();
		}

		private void toolStripButtonClearFilter_Click(object sender, EventArgs e)
		{
			if (dataGridList.DisplayLayout != null && dataGridList.DisplayLayout.Bands.Count > 0)
			{
				dataGridList.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
			}
		}

		private void salesStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridList.ActiveRow != null && dataGridList.ActiveRow.IsDataRow)
			{
				string selectedID = dataGridList.ActiveRow.Cells["CustomerID"].Value.ToString();
				CustomerSalesStatisticForm customerSalesStatisticForm = new CustomerSalesStatisticForm();
				customerSalesStatisticForm.SelectedID = selectedID;
				customerSalesStatisticForm.Show();
			}
		}

		private void ledgerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowLedger();
		}

		private void addActivityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				FormActivator.BringFormToFront(FormActivator.ActivityDetailsFormObj);
				FormActivator.ActivityDetailsFormObj.AddNewActivity(CRMRelatedTypes.Customer, selectedID);
			}
		}

		private void addFollowUpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
			FormActivator.FollowupDetailsFormObj.SourceSysDocID = "";
			FormActivator.FollowupDetailsFormObj.SourceVoucherID = dataGridList.ActiveRow.Cells["CustomerID"].Value.ToString();
			FormActivator.FollowupDetailsFormObj.CRMType = CRMRelatedTypes.CustomerCollection;
			FormActivator.FollowupDetailsFormObj.ThisfollowupBy = "";
			FormActivator.FollowupDetailsFormObj.ThisfollowupDate = DateTime.Now;
			FormActivator.FollowupDetailsFormObj.ThisfollowupTime = DateTime.Now;
			FormActivator.FollowupDetailsFormObj.Status = 1;
			FormActivator.FollowupDetailsFormObj.ActiveNewRecord();
		}

		private void commentsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				string selectedID = GetSelectedID();
				if (!(selectedID == ""))
				{
					EntityCommentsForm entityCommentsForm = new EntityCommentsForm();
					entityCommentsForm.EntityID = selectedID;
					entityCommentsForm.EntityName = GetSelectedItem().Cells["CustomerName"].ToString();
					entityCommentsForm.EntityType = EntityTypesEnum.Customers;
					entityCommentsForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}
	}
}
