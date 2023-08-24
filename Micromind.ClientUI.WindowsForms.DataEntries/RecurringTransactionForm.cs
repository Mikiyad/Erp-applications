using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler.UI;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries
{
	public class RecurringTransactionForm : Form
	{
		private RecurringInvoiceData currentData;

		private IContainer components;

		private Line linePanelDown;

		private TextEdit textBoxVoucher;

		private LabelControl labelControl1;

		private LabelControl labelControl2;

		private LabelControl labelControl3;

		private LabelControl labelControl4;

		private TextEdit textBoxTransactionID;

		private LabelControl labelControl5;

		private LabelControl labelControl6;

		private DateEdit dateEditStart;

		private DateEdit dateEditEnd;

		private TextEdit textBoxDocType;

		private TextEdit textBoxSysDoc;

		private BarButtonItem barButtonItemRefresh;

		private RibbonControl ribbon;

		private BarButtonItem barButtonItemNewGroup;

		private BarButtonItem barButtonItem1;

		private BarButtonGroup barButtonGroup1;

		private BarButtonGroup barButtonGroup2;

		private BarButtonItem barButtonItem3;

		private BarButtonItem barButtonItem4;

		private BarButtonItem barButtonItem5;

		private BarButtonItem barButtonItem6;

		private BarButtonItem barButtonItem2;

		private BarButtonItem barButtonLoadSync;

		private BarButtonGroup barButtonGroup3;

		private BarCheckItem barItemSelectedRowsOnly;

		private BarButtonItem barButtonPrintReport;

		private BarButtonItem barButtonPrintChart;

		private BarCheckItem barItemHideFilterHeader;

		private BarSubItem barItemChartColor;

		private BarEditItem barItemTitle;

		private BarSubItem barItemChartTypes;

		private BarButtonItem barButtonItemFilter;

		private BarButtonGroup barButtonGroup5;

		private BarButtonItem barButtonItem8;

		private BarButtonItem barButtonItem9;

		private BarButtonItem barButtonItem10;

		private BarButtonGroup barButtonGroup4;

		private BarButtonItem barButtonItem7;

		private BarButtonGroup barButtonGroup6;

		private BarButtonItem barButtonItem11;

		private BarButtonItem barButtonItemExport;

		private BarButtonItem barButtonItemImport;

		private BarButtonItem barButtonNewSync;

		private BarButtonItem barButtonItemDelete;

		private BarButtonItem barButtonSyncData;

		private BarButtonItem barButtonItem13;

		private BarSubItem barSubItemRecurrence;

		private BarSubItem barSubItem1;

		private BarSubItem barSubItem2;

		private RibbonPage ribbonPagePivotReport;

		private RibbonPageGroup ribbonPageGroupEdit;

		private RibbonPageGroup ribbonPageGroup1;

		private BarButtonItem barButtonItem12;

		private BarSubItem barSubItem3;

		private BarButtonItem barButtonItemSave;

		private BarButtonItem barButtonItemDeleteItem;

		private BarButtonItem barButtonItemSchedule;

		private CheckBox checkBoxIsActive;

		private CheckBox checkBoxIsHold;

		public string SysDocID
		{
			set
			{
				textBoxSysDoc.Text = value;
			}
		}

		public string VoucherID
		{
			set
			{
				textBoxVoucher.Text = value;
			}
		}

		public SysDocTypes DocType
		{
			set
			{
				textBoxDocType.Text = value.ToString();
			}
		}

		public RecurringTransactionForm()
		{
			InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void EnterNameDialog_Activated(object sender, EventArgs e)
		{
		}

		private void linkLabelRecurrence_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void barButtonItemSchedule_ItemClick(object sender, ItemClickEventArgs e)
		{
			AppointmentRecurrenceForm appointmentRecurrenceForm = new AppointmentRecurrenceForm();
			appointmentRecurrenceForm.Text = "Schedule";
			appointmentRecurrenceForm.Controls[3].Visible = false;
			appointmentRecurrenceForm.Controls[5].Visible = false;
			appointmentRecurrenceForm.Height = 235;
			appointmentRecurrenceForm.Controls[4].Location = new Point(10, 10);
			appointmentRecurrenceForm.Controls[1].Location = new Point(90, 170);
			appointmentRecurrenceForm.Controls[2].Location = new Point(10, 170);
			appointmentRecurrenceForm.Show();
		}

		private bool GetData()
		{
			try
			{
				currentData = new RecurringInvoiceData();
				DataRow dataRow = currentData.RecurringInvoiceTransactionTable.NewRow();
				dataRow.BeginEdit();
				dataRow["TransactionID"] = textBoxTransactionID.Text;
				dataRow["SysDocID"] = textBoxSysDoc.Text;
				dataRow["VoucherID"] = textBoxVoucher.Text;
				dataRow["SysDocType"] = (SysDocTypes)Enum.Parse(typeof(SysDocTypes), textBoxDocType.Text, ignoreCase: true);
				dataRow["StartDate"] = dateEditStart.EditValue;
				dataRow["EndDate"] = dateEditEnd.EditValue;
				dataRow["RepeateEvery"] = DBNull.Value;
				dataRow["Interval"] = DBNull.Value;
				dataRow["LastRunDate"] = DBNull.Value;
				dataRow["LastSysDocID"] = DBNull.Value;
				dataRow["SourceSysDocID"] = textBoxSysDoc.Text;
				dataRow["SourceVoucherID"] = textBoxVoucher.Text;
				dataRow.EndEdit();
				currentData.RecurringInvoiceTransactionTable.Rows.Add(dataRow);
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			SaveData();
		}

		private bool SaveData()
		{
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag = Factory.RecurringInvoiceSystem.CreateRecurringInvoice(currentData, isUpdate: false, isPosting: false);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void RecurringTransactionForm_Load(object sender, EventArgs e)
		{
			ClearForm();
		}

		private void ClearForm()
		{
			textBoxTransactionID.Text = Factory.SystemDocumentSystem.GetNextNumber("Recurring_Transaction", "TransactionID");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.RecurringTransactionForm));
			linePanelDown = new Micromind.UISupport.Line();
			textBoxVoucher = new DevExpress.XtraEditors.TextEdit();
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			labelControl2 = new DevExpress.XtraEditors.LabelControl();
			labelControl3 = new DevExpress.XtraEditors.LabelControl();
			labelControl4 = new DevExpress.XtraEditors.LabelControl();
			textBoxTransactionID = new DevExpress.XtraEditors.TextEdit();
			labelControl5 = new DevExpress.XtraEditors.LabelControl();
			labelControl6 = new DevExpress.XtraEditors.LabelControl();
			dateEditStart = new DevExpress.XtraEditors.DateEdit();
			dateEditEnd = new DevExpress.XtraEditors.DateEdit();
			textBoxDocType = new DevExpress.XtraEditors.TextEdit();
			textBoxSysDoc = new DevExpress.XtraEditors.TextEdit();
			barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
			ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
			barButtonItemNewGroup = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonGroup2 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			barButtonLoadSync = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup3 = new DevExpress.XtraBars.BarButtonGroup();
			barItemSelectedRowsOnly = new DevExpress.XtraBars.BarCheckItem();
			barButtonPrintReport = new DevExpress.XtraBars.BarButtonItem();
			barButtonPrintChart = new DevExpress.XtraBars.BarButtonItem();
			barItemHideFilterHeader = new DevExpress.XtraBars.BarCheckItem();
			barItemChartColor = new DevExpress.XtraBars.BarSubItem();
			barItemTitle = new DevExpress.XtraBars.BarEditItem();
			barItemChartTypes = new DevExpress.XtraBars.BarSubItem();
			barButtonItemFilter = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup5 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup4 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup6 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemExport = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemImport = new DevExpress.XtraBars.BarButtonItem();
			barButtonNewSync = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
			barButtonSyncData = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem13 = new DevExpress.XtraBars.BarButtonItem();
			barSubItemRecurrence = new DevExpress.XtraBars.BarSubItem();
			barSubItem1 = new DevExpress.XtraBars.BarSubItem();
			barSubItem2 = new DevExpress.XtraBars.BarSubItem();
			barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemDeleteItem = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemSchedule = new DevExpress.XtraBars.BarButtonItem();
			ribbonPagePivotReport = new DevExpress.XtraBars.Ribbon.RibbonPage();
			ribbonPageGroupEdit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			barButtonItem12 = new DevExpress.XtraBars.BarButtonItem();
			barSubItem3 = new DevExpress.XtraBars.BarSubItem();
			checkBoxIsActive = new System.Windows.Forms.CheckBox();
			checkBoxIsHold = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)textBoxVoucher.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxTransactionID.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dateEditStart.Properties.CalendarTimeProperties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dateEditStart.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dateEditEnd.Properties.CalendarTimeProperties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dateEditEnd.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxDocType.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxSysDoc.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)ribbon).BeginInit();
			SuspendLayout();
			linePanelDown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-41, 295);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(693, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			textBoxVoucher.Enabled = false;
			textBoxVoucher.Location = new System.Drawing.Point(355, 112);
			textBoxVoucher.Name = "textBoxVoucher";
			textBoxVoucher.Size = new System.Drawing.Size(192, 20);
			textBoxVoucher.TabIndex = 1;
			labelControl1.Location = new System.Drawing.Point(16, 118);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(39, 13);
			labelControl1.TabIndex = 2;
			labelControl1.Text = "Doc ID :";
			labelControl2.Location = new System.Drawing.Point(257, 115);
			labelControl2.Name = "labelControl2";
			labelControl2.Size = new System.Drawing.Size(83, 13);
			labelControl2.TabIndex = 3;
			labelControl2.Text = "Voucher Number:";
			labelControl3.Location = new System.Drawing.Point(16, 140);
			labelControl3.Name = "labelControl3";
			labelControl3.Size = new System.Drawing.Size(52, 13);
			labelControl3.TabIndex = 5;
			labelControl3.Text = "Doc Type :";
			labelControl4.Location = new System.Drawing.Point(257, 139);
			labelControl4.Name = "labelControl4";
			labelControl4.Size = new System.Drawing.Size(74, 13);
			labelControl4.TabIndex = 8;
			labelControl4.Text = "Transaction ID:";
			textBoxTransactionID.Enabled = false;
			textBoxTransactionID.Location = new System.Drawing.Point(355, 136);
			textBoxTransactionID.Name = "textBoxTransactionID";
			textBoxTransactionID.Size = new System.Drawing.Size(192, 20);
			textBoxTransactionID.TabIndex = 7;
			labelControl5.Location = new System.Drawing.Point(16, 163);
			labelControl5.Name = "labelControl5";
			labelControl5.Size = new System.Drawing.Size(57, 13);
			labelControl5.TabIndex = 9;
			labelControl5.Text = "Start Date :";
			labelControl6.Location = new System.Drawing.Point(16, 187);
			labelControl6.Name = "labelControl6";
			labelControl6.Size = new System.Drawing.Size(51, 13);
			labelControl6.TabIndex = 10;
			labelControl6.Text = "End Date :";
			dateEditStart.EditValue = null;
			dateEditStart.Location = new System.Drawing.Point(80, 159);
			dateEditStart.Name = "dateEditStart";
			dateEditStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			dateEditStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			dateEditStart.Size = new System.Drawing.Size(152, 20);
			dateEditStart.TabIndex = 11;
			dateEditEnd.EditValue = null;
			dateEditEnd.Location = new System.Drawing.Point(80, 182);
			dateEditEnd.Name = "dateEditEnd";
			dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			dateEditEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			dateEditEnd.Size = new System.Drawing.Size(152, 20);
			dateEditEnd.TabIndex = 12;
			textBoxDocType.Enabled = false;
			textBoxDocType.Location = new System.Drawing.Point(80, 136);
			textBoxDocType.Name = "textBoxDocType";
			textBoxDocType.Size = new System.Drawing.Size(152, 20);
			textBoxDocType.TabIndex = 19;
			textBoxSysDoc.Enabled = false;
			textBoxSysDoc.Location = new System.Drawing.Point(80, 113);
			textBoxSysDoc.Name = "textBoxSysDoc";
			textBoxSysDoc.Size = new System.Drawing.Size(152, 20);
			textBoxSysDoc.TabIndex = 18;
			barButtonItemRefresh.Caption = "Refresh";
			barButtonItemRefresh.Id = 42;
			barButtonItemRefresh.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Circulation;
			barButtonItemRefresh.Name = "barButtonItemRefresh";
			ribbon.ApplicationButtonImageOptions.Image = Micromind.ClientUI.Properties.Resources.axolon_48;
			ribbon.ExpandCollapseItem.Id = 0;
			ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[40]
			{
				ribbon.ExpandCollapseItem,
				barButtonItemNewGroup,
				barButtonItem1,
				barButtonGroup1,
				barButtonGroup2,
				barButtonItem3,
				barButtonItem4,
				barButtonItem5,
				barButtonItem6,
				barButtonItem2,
				barButtonLoadSync,
				barButtonGroup3,
				barItemSelectedRowsOnly,
				barButtonPrintReport,
				barButtonPrintChart,
				barItemHideFilterHeader,
				barItemChartColor,
				barItemTitle,
				barItemChartTypes,
				barButtonItemFilter,
				barButtonGroup5,
				barButtonItem8,
				barButtonItem9,
				barButtonItem10,
				barButtonGroup4,
				barButtonItem7,
				barButtonGroup6,
				barButtonItem11,
				barButtonItemExport,
				barButtonItemImport,
				barButtonNewSync,
				barButtonItemDelete,
				barButtonSyncData,
				barButtonItem13,
				barSubItemRecurrence,
				barSubItem1,
				barSubItem2,
				barButtonItemSave,
				barButtonItemDeleteItem,
				barButtonItemSchedule
			});
			ribbon.Location = new System.Drawing.Point(0, 0);
			ribbon.MaxItemId = 51;
			ribbon.Name = "ribbon";
			ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[1]
			{
				ribbonPagePivotReport
			});
			ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
			ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
			ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages;
			ribbon.Size = new System.Drawing.Size(563, 95);
			ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
			ribbon.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.True;
			barButtonItemNewGroup.Caption = "New Group";
			barButtonItemNewGroup.Id = 1;
			barButtonItemNewGroup.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.newfolder;
			barButtonItemNewGroup.Name = "barButtonItemNewGroup";
			barButtonItem1.Caption = "barButtonItem1";
			barButtonItem1.Id = 3;
			barButtonItem1.Name = "barButtonItem1";
			barButtonGroup1.Caption = "barButtonGroup1";
			barButtonGroup1.Id = 4;
			barButtonGroup1.Name = "barButtonGroup1";
			barButtonGroup2.Caption = "barButtonGroup2";
			barButtonGroup2.Id = 5;
			barButtonGroup2.Name = "barButtonGroup2";
			barButtonItem3.Caption = "barButtonItem3";
			barButtonItem3.Id = 6;
			barButtonItem3.Name = "barButtonItem3";
			barButtonItem4.Caption = "barButtonItem4";
			barButtonItem4.Id = 7;
			barButtonItem4.Name = "barButtonItem4";
			barButtonItem5.Caption = "barButtonItem5";
			barButtonItem5.Id = 8;
			barButtonItem5.Name = "barButtonItem5";
			barButtonItem6.Caption = "Save As...";
			barButtonItem6.Id = 9;
			barButtonItem6.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Save_as;
			barButtonItem6.Name = "barButtonItem6";
			barButtonItem2.Caption = "Save";
			barButtonItem2.Id = 10;
			barButtonItem2.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.saveicon;
			barButtonItem2.Name = "barButtonItem2";
			barButtonLoadSync.Caption = "Select Sync";
			barButtonLoadSync.Id = 11;
			barButtonLoadSync.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.editdoc;
			barButtonLoadSync.Name = "barButtonLoadSync";
			barButtonGroup3.Caption = "barButtonGroup3";
			barButtonGroup3.Id = 12;
			barButtonGroup3.Name = "barButtonGroup3";
			barItemSelectedRowsOnly.BindableChecked = true;
			barItemSelectedRowsOnly.Caption = "Selection Only";
			barItemSelectedRowsOnly.Checked = true;
			barItemSelectedRowsOnly.Id = 15;
			barItemSelectedRowsOnly.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.selectedrows;
			barItemSelectedRowsOnly.Name = "barItemSelectedRowsOnly";
			barButtonPrintReport.Caption = "Print Report";
			barButtonPrintReport.Id = 17;
			barButtonPrintReport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.print;
			barButtonPrintReport.Name = "barButtonPrintReport";
			barButtonPrintChart.Caption = "Print Chart";
			barButtonPrintChart.Id = 18;
			barButtonPrintChart.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.chart_1;
			barButtonPrintChart.Name = "barButtonPrintChart";
			barItemHideFilterHeader.Caption = "Hide Fields Panel";
			barItemHideFilterHeader.Id = 21;
			barItemHideFilterHeader.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.columns;
			barItemHideFilterHeader.Name = "barItemHideFilterHeader";
			barItemChartColor.Caption = "Colors";
			barItemChartColor.Id = 22;
			barItemChartColor.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.colorpalet;
			barItemChartColor.Name = "barItemChartColor";
			barItemTitle.Caption = "Chart Title:";
			barItemTitle.Edit = null;
			barItemTitle.EditWidth = 150;
			barItemTitle.Id = 25;
			barItemTitle.Name = "barItemTitle";
			barItemChartTypes.Caption = "Chart Type";
			barItemChartTypes.Id = 26;
			barItemChartTypes.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Chart_48;
			barItemChartTypes.Name = "barItemChartTypes";
			barButtonItemFilter.Caption = "Filter";
			barButtonItemFilter.Id = 27;
			barButtonItemFilter.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.filter;
			barButtonItemFilter.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.filter;
			barButtonItemFilter.Name = "barButtonItemFilter";
			barButtonGroup5.Caption = "barButtonGroup5";
			barButtonGroup5.Id = 30;
			barButtonGroup5.Name = "barButtonGroup5";
			barButtonItem8.Caption = "barButtonItem8";
			barButtonItem8.Id = 31;
			barButtonItem8.Name = "barButtonItem8";
			barButtonItem9.Caption = "barButtonItem9";
			barButtonItem9.Id = 32;
			barButtonItem9.Name = "barButtonItem9";
			barButtonItem10.Caption = "barButtonItem10";
			barButtonItem10.Id = 33;
			barButtonItem10.Name = "barButtonItem10";
			barButtonGroup4.Caption = "barButtonGroup4";
			barButtonGroup4.Id = 34;
			barButtonGroup4.Name = "barButtonGroup4";
			barButtonItem7.Caption = "barButtonItem7";
			barButtonItem7.Id = 35;
			barButtonItem7.Name = "barButtonItem7";
			barButtonGroup6.Caption = "barButtonGroup6";
			barButtonGroup6.Id = 36;
			barButtonGroup6.Name = "barButtonGroup6";
			barButtonItem11.Caption = "barButtonItem11";
			barButtonItem11.Id = 37;
			barButtonItem11.Name = "barButtonItem11";
			barButtonItemExport.Caption = "Export...";
			barButtonItemExport.Id = 38;
			barButtonItemExport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.ExportToXMLFile48;
			barButtonItemExport.Name = "barButtonItemExport";
			barButtonItemImport.Caption = "Import...";
			barButtonItemImport.Id = 39;
			barButtonItemImport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.download_icon;
			barButtonItemImport.Name = "barButtonItemImport";
			barButtonNewSync.Caption = "Setup Sync";
			barButtonNewSync.Id = 40;
			barButtonNewSync.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.newfile;
			barButtonNewSync.Name = "barButtonNewSync";
			barButtonItemDelete.Caption = "Delete";
			barButtonItemDelete.Id = 41;
			barButtonItemDelete.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.DeleteRed;
			barButtonItemDelete.Name = "barButtonItemDelete";
			barButtonSyncData.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			barButtonSyncData.Caption = "Sync Data";
			barButtonSyncData.Id = 43;
			barButtonSyncData.Name = "barButtonSyncData";
			barButtonItem13.Caption = "barButtonItem13";
			barButtonItem13.Id = 44;
			barButtonItem13.Name = "barButtonItem13";
			barSubItemRecurrence.Caption = "Recurrence";
			barSubItemRecurrence.Id = 45;
			barSubItemRecurrence.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barSubItemRecurrence.ImageOptions.Image");
			barSubItemRecurrence.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barSubItemRecurrence.ImageOptions.LargeImage");
			barSubItemRecurrence.Name = "barSubItemRecurrence";
			barSubItem1.Caption = "Save";
			barSubItem1.Id = 46;
			barSubItem1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barSubItem1.ImageOptions.Image");
			barSubItem1.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barSubItem1.ImageOptions.LargeImage");
			barSubItem1.Name = "barSubItem1";
			barSubItem2.Caption = "Delete";
			barSubItem2.Id = 47;
			barSubItem2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barSubItem2.ImageOptions.Image");
			barSubItem2.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barSubItem2.ImageOptions.LargeImage");
			barSubItem2.Name = "barSubItem2";
			barButtonItemSave.Caption = "Save";
			barButtonItemSave.Id = 48;
			barButtonItemSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItemSave.ImageOptions.Image");
			barButtonItemSave.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItemSave.ImageOptions.LargeImage");
			barButtonItemSave.Name = "barButtonItemSave";
			barButtonItemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemSave_ItemClick);
			barButtonItemDeleteItem.Caption = "Delete";
			barButtonItemDeleteItem.Id = 49;
			barButtonItemDeleteItem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItemDeleteItem.ImageOptions.Image");
			barButtonItemDeleteItem.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItemDeleteItem.ImageOptions.LargeImage");
			barButtonItemDeleteItem.Name = "barButtonItemDeleteItem";
			barButtonItemSchedule.Caption = "Schedule";
			barButtonItemSchedule.Id = 50;
			barButtonItemSchedule.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItemSchedule.ImageOptions.Image");
			barButtonItemSchedule.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItemSchedule.ImageOptions.LargeImage");
			barButtonItemSchedule.Name = "barButtonItemSchedule";
			barButtonItemSchedule.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemSchedule_ItemClick);
			ribbonPagePivotReport.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2]
			{
				ribbonPageGroupEdit,
				ribbonPageGroup1
			});
			ribbonPagePivotReport.Name = "ribbonPagePivotReport";
			ribbonPagePivotReport.Text = "Sync";
			ribbonPageGroupEdit.ItemLinks.Add(barButtonItemSave);
			ribbonPageGroupEdit.ItemLinks.Add(barButtonItemDeleteItem);
			ribbonPageGroupEdit.Name = "ribbonPageGroupEdit";
			ribbonPageGroupEdit.Text = "Actions";
			ribbonPageGroup1.ItemLinks.Add(barButtonItemSchedule);
			ribbonPageGroup1.Name = "ribbonPageGroup1";
			ribbonPageGroup1.Text = "Options";
			barButtonItem12.Caption = "Refresh";
			barButtonItem12.Id = 42;
			barButtonItem12.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Circulation;
			barButtonItem12.Name = "barButtonItem12";
			barSubItem3.Caption = "Recurrence";
			barSubItem3.Id = 45;
			barSubItem3.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barSubItem3.ImageOptions.Image");
			barSubItem3.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barSubItem3.ImageOptions.LargeImage");
			barSubItem3.Name = "barSubItem3";
			checkBoxIsActive.AutoSize = true;
			checkBoxIsActive.Location = new System.Drawing.Point(80, 211);
			checkBoxIsActive.Name = "checkBoxIsActive";
			checkBoxIsActive.Size = new System.Drawing.Size(67, 17);
			checkBoxIsActive.TabIndex = 20;
			checkBoxIsActive.Text = "Is Active";
			checkBoxIsActive.UseVisualStyleBackColor = true;
			checkBoxIsHold.AutoSize = true;
			checkBoxIsHold.Location = new System.Drawing.Point(80, 234);
			checkBoxIsHold.Name = "checkBoxIsHold";
			checkBoxIsHold.Size = new System.Drawing.Size(59, 17);
			checkBoxIsHold.TabIndex = 21;
			checkBoxIsHold.Text = "Is Hold";
			checkBoxIsHold.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(563, 287);
			base.Controls.Add(checkBoxIsHold);
			base.Controls.Add(checkBoxIsActive);
			base.Controls.Add(ribbon);
			base.Controls.Add(textBoxDocType);
			base.Controls.Add(textBoxSysDoc);
			base.Controls.Add(dateEditEnd);
			base.Controls.Add(dateEditStart);
			base.Controls.Add(labelControl6);
			base.Controls.Add(labelControl5);
			base.Controls.Add(labelControl4);
			base.Controls.Add(textBoxTransactionID);
			base.Controls.Add(labelControl3);
			base.Controls.Add(labelControl2);
			base.Controls.Add(labelControl1);
			base.Controls.Add(textBoxVoucher);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RecurringTransactionForm";
			Text = "Recurrence Transactions Settings";
			base.Activated += new System.EventHandler(EnterNameDialog_Activated);
			base.Load += new System.EventHandler(RecurringTransactionForm_Load);
			((System.ComponentModel.ISupportInitialize)textBoxVoucher.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxTransactionID.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)dateEditStart.Properties.CalendarTimeProperties).EndInit();
			((System.ComponentModel.ISupportInitialize)dateEditStart.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)dateEditEnd.Properties.CalendarTimeProperties).EndInit();
			((System.ComponentModel.ISupportInitialize)dateEditEnd.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxDocType.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxSysDoc.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)ribbon).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
