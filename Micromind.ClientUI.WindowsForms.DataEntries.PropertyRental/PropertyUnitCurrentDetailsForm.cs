using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using Microsoft.Office.Interop.Outlook;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental
{
	public class PropertyUnitCurrentDetailsForm : Form, IForm
	{
		private DataSet currentData;

		private DataSet historyData;

		private const string TABLENAME_CONST = "Property_Class";

		private const string IDFIELD_CONST = "PropertyClassID";

		private bool isNewRecord = true;

		private string propertyUnitID = "";

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl ultraTabPageControl2;

		private MMLabel mmLabelUnitName;

		private MMLabel mmLabel10;

		private MMLabel mmLabel8;

		private MMLabel mmLabel4;

		private DataGridList dataGridList;

		private MMTextBox textBoxAddress;

		private MMLabel mmLabel15;

		private MMLabel mmLabel16;

		private MMLabel mmLabel9;

		private MMLabel mmLabel12;

		private MMLabel mmLabel18;

		private GroupBox groupBox1;

		private GroupBox groupBox3;

		private GroupBox groupBox2;

		private DataGridList dataGridListChque;

		private MMLabel mmLabelTenantID;

		private UltraFormattedLinkLabel linkLabelTenant;

		private MMLabel mmLabel6;

		private MMLabel mmLabel3;

		private NumberTextBox mmLabelMobile;

		private NumberTextBox mmLabelEmail;

		private NumberTextBox mmLabelEndDate;

		private NumberTextBox mmLabel1StratDate;

		private NumberTextBox mmLabelTransactionDate;

		private NumberTextBox mmLabelVoucherID;

		private NumberTextBox mmLabelSysDocID;

		private NumberTextBox mmLabelRentAmount;

		private NumberTextBox mmLabelTotal;

		private NumberTextBox mmLabelDepositAmount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private NumberTextBox mmLabelTenantName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private NumberTextBox textBoxTaxAmount;

		private MMLabel mmLabel1;

		public ScreenAreas ScreenArea => ScreenAreas.PropertyRental;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		public string PropertyUnitID
		{
			get
			{
				return propertyUnitID;
			}
			set
			{
				propertyUnitID = value;
			}
		}

		public PropertyUnitCurrentDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += PropertyUnitCurrentDetailsForm_Load;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
		}

		public void LoadData(string id)
		{
			try
			{
				currentData = Factory.PropertyUnitSystem.GetPropertyUnitCurrentTenant(id);
				historyData = Factory.PropertyUnitSystem.GetPropertyUnitHistoryReport(id);
				FillData();
				formManager.ResetDirty();
			}
			catch (System.Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupGrid()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ChequeNo");
			dataTable.Columns.Add("ChequeDate", typeof(DateTime));
			dataTable.Columns.Add("Status");
			dataGridListChque.DataSource = dataTable;
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					mmLabelUnitName.Text = dataRow["PropertyUnitName"].ToString();
					mmLabelTenantID.Text = dataRow["CustomerID"].ToString();
					mmLabelTenantName.Text = dataRow["CustomerName"].ToString();
					mmLabelMobile.Text = dataRow["Mobile"].ToString();
					mmLabelEmail.Text = dataRow["Email2"].ToString();
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(dataRow["Address"].ToString());
					if (dataRow["Address2"].ToString() != "")
					{
						stringBuilder.AppendLine(", " + dataRow["Address2"].ToString());
					}
					if (dataRow["Address3"].ToString() != "")
					{
						stringBuilder.AppendLine(", " + dataRow["Address3"].ToString());
					}
					if (dataRow["City"].ToString() != "")
					{
						stringBuilder.AppendLine("City:" + dataRow["City"].ToString());
					}
					if (dataRow["Phone"].ToString() != "")
					{
						stringBuilder.AppendLine("Phone1:" + dataRow["Phone"].ToString());
					}
					if (dataRow["Phone2"].ToString() != "")
					{
						stringBuilder.AppendLine("Phone2:" + dataRow["Phone2"].ToString());
					}
					if (dataRow["Fax"].ToString() != "")
					{
						stringBuilder.AppendLine("Fax:" + dataRow["Fax"].ToString());
					}
					textBoxAddress.Text = stringBuilder.ToString();
					mmLabelSysDocID.Text = dataRow["SysDocID"].ToString();
					mmLabelVoucherID.Text = dataRow["VoucherID"].ToString();
					mmLabelTransactionDate.Text = dataRow["TransactionDate"].ToString();
					mmLabel1StratDate.Text = dataRow["ContractStartDate"].ToString();
					mmLabelEndDate.Text = dataRow["ContractEndDate"].ToString();
					decimal d = Convert.ToDecimal(dataRow["TotalRent"].ToString());
					mmLabelRentAmount.Text = Math.Round(d, Global.CurDecimalPoints).ToString();
					d = Convert.ToDecimal(dataRow["TotalDeposit"].ToString());
					mmLabelDepositAmount.Text = Math.Round(d, Global.CurDecimalPoints).ToString();
					d = Convert.ToDecimal(dataRow["Total"].ToString());
					mmLabelTotal.Text = Math.Round(d, Global.CurDecimalPoints).ToString();
					d = Convert.ToDecimal(dataRow["TaxAmount"].ToString());
					textBoxTaxAmount.Text = Math.Round(d, Global.CurDecimalPoints).ToString();
					new DataSet();
					DataSet propertypaymentdetails = Factory.PropertyCancelSystem.GetPropertypaymentdetails(mmLabelSysDocID.Text, mmLabelVoucherID.Text);
					DataTable dataTable = dataGridListChque.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in propertypaymentdetails.Tables[0].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Status"] = row["Status"];
						dataRow3["ChequeNo"] = row["ChequeNumber"];
						dataRow3["ChequeDate"] = row["ChequeDate"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					dataGridList.DataSource = historyData.Tables[0];
				}
			}
			catch (System.Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
		}

		private void PropertyClassGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void PropertyClassGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void PropertyUnitCurrentDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				dataGridList.ApplyUIDesign();
				dataGridListChque.ApplyUIDesign();
				SetupGrid();
				if (!base.IsDisposed)
				{
					LoadData(PropertyUnitID);
				}
			}
			catch (System.Exception e2)
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
		}

		private void mmLabel14_Click(object sender, EventArgs e)
		{
		}

		private void linkLabelTenant_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (mmLabelTenantID.Text != "")
			{
				new FormHelper().EditTenant(mmLabelTenantID.Text);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			FormHelper formHelper = new FormHelper();
			SysDocTypes sysDocTypes = SysDocTypes.PropertyRental;
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "SysDocType", "SysDocID", mmLabelSysDocID.Text);
			if (fieldValue != null)
			{
				sysDocTypes = (SysDocTypes)byte.Parse(fieldValue.ToString());
			}
			if (mmLabelVoucherID.Text != "")
			{
				switch (sysDocTypes)
				{
				case SysDocTypes.PropertyRental:
					formHelper.EditTransaction(TransactionListType.PropertyRental, mmLabelSysDocID.Text, mmLabelVoucherID.Text);
					break;
				case SysDocTypes.PropertyRenew:
					formHelper.EditTransaction(TransactionListType.PropertyRenew, mmLabelSysDocID.Text, mmLabelVoucherID.Text);
					break;
				}
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			checked
			{
				if (mmLabelEmail.Text != "")
				{
					Microsoft.Office.Interop.Outlook.Application application = (Microsoft.Office.Interop.Outlook.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("0006F03A-0000-0000-C000-000000000046")));
					_ = (AppointmentItem)(dynamic)application.CreateItem(OlItemType.olAppointmentItem);
					MailItem obj = (MailItem)(dynamic)application.CreateItem(OlItemType.olMailItem);
					obj.Subject = "";
					obj.To = mmLabelEmail.Text;
					obj.Body = "";
					obj.Importance = OlImportance.olImportanceLow;
					obj.Display(true);
				}
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
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.PropertyUnitCurrentDetailsForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			groupBox3 = new System.Windows.Forms.GroupBox();
			dataGridListChque = new Micromind.UISupport.DataGridList(components);
			groupBox2 = new System.Windows.Forms.GroupBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabelTenantName = new Micromind.UISupport.NumberTextBox();
			mmLabelEmail = new Micromind.UISupport.NumberTextBox();
			mmLabelMobile = new Micromind.UISupport.NumberTextBox();
			linkLabelTenant = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabelTenantID = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxAddress = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			groupBox1 = new System.Windows.Forms.GroupBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabelTotal = new Micromind.UISupport.NumberTextBox();
			mmLabelDepositAmount = new Micromind.UISupport.NumberTextBox();
			mmLabelRentAmount = new Micromind.UISupport.NumberTextBox();
			mmLabelEndDate = new Micromind.UISupport.NumberTextBox();
			mmLabel1StratDate = new Micromind.UISupport.NumberTextBox();
			mmLabelTransactionDate = new Micromind.UISupport.NumberTextBox();
			mmLabelVoucherID = new Micromind.UISupport.NumberTextBox();
			mmLabelSysDocID = new Micromind.UISupport.NumberTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			mmLabelUnitName = new Micromind.UISupport.MMLabel();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			formManager = new Micromind.DataControls.FormManager();
			textBoxTaxAmount = new Micromind.UISupport.NumberTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			tabPageGeneral.SuspendLayout();
			groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridListChque).BeginInit();
			groupBox2.SuspendLayout();
			groupBox1.SuspendLayout();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(groupBox3);
			tabPageGeneral.Controls.Add(groupBox2);
			tabPageGeneral.Controls.Add(groupBox1);
			tabPageGeneral.Controls.Add(mmLabelUnitName);
			tabPageGeneral.Controls.Add(mmLabel10);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(747, 426);
			groupBox3.Controls.Add(dataGridListChque);
			groupBox3.Location = new System.Drawing.Point(348, 264);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(389, 157);
			groupBox3.TabIndex = 40;
			groupBox3.TabStop = false;
			groupBox3.Text = "Cheque Details";
			dataGridListChque.AllowUnfittedView = false;
			dataGridListChque.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridListChque.DisplayLayout.Appearance = appearance;
			dataGridListChque.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridListChque.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListChque.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListChque.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridListChque.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListChque.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridListChque.DisplayLayout.MaxColScrollRegions = 1;
			dataGridListChque.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridListChque.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridListChque.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridListChque.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridListChque.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridListChque.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridListChque.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridListChque.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridListChque.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListChque.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridListChque.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridListChque.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridListChque.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridListChque.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridListChque.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridListChque.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridListChque.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridListChque.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridListChque.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridListChque.LoadLayoutFailed = false;
			dataGridListChque.Location = new System.Drawing.Point(6, 14);
			dataGridListChque.Name = "dataGridListChque";
			dataGridListChque.ShowDeleteMenu = false;
			dataGridListChque.ShowMinusInRed = true;
			dataGridListChque.ShowNewMenu = false;
			dataGridListChque.Size = new System.Drawing.Size(377, 137);
			dataGridListChque.TabIndex = 2;
			dataGridListChque.Text = "dataGridList1";
			groupBox2.Controls.Add(ultraFormattedLinkLabel2);
			groupBox2.Controls.Add(mmLabelTenantName);
			groupBox2.Controls.Add(mmLabelEmail);
			groupBox2.Controls.Add(mmLabelMobile);
			groupBox2.Controls.Add(linkLabelTenant);
			groupBox2.Controls.Add(mmLabelTenantID);
			groupBox2.Controls.Add(mmLabel8);
			groupBox2.Controls.Add(textBoxAddress);
			groupBox2.Controls.Add(mmLabel4);
			groupBox2.Location = new System.Drawing.Point(3, 36);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(339, 182);
			groupBox2.TabIndex = 39;
			groupBox2.TabStop = false;
			groupBox2.Text = "Tenant Details";
			ultraFormattedLinkLabel2.AutoSize = true;
			appearance13.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.LinkAppearance = appearance13;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(17, 147);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(81, 14);
			ultraFormattedLinkLabel2.TabIndex = 42;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Email Address : ";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			mmLabelTenantName.AllowDecimal = true;
			mmLabelTenantName.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelTenantName.CustomReportFieldName = "";
			mmLabelTenantName.CustomReportKey = "";
			mmLabelTenantName.CustomReportValueType = 1;
			mmLabelTenantName.Enabled = false;
			mmLabelTenantName.IsComboTextBox = false;
			mmLabelTenantName.IsModified = false;
			mmLabelTenantName.Location = new System.Drawing.Point(99, 22);
			mmLabelTenantName.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelTenantName.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelTenantName.Name = "mmLabelTenantName";
			mmLabelTenantName.NullText = "0";
			mmLabelTenantName.ReadOnly = true;
			mmLabelTenantName.Size = new System.Drawing.Size(231, 20);
			mmLabelTenantName.TabIndex = 43;
			mmLabelEmail.AllowDecimal = true;
			mmLabelEmail.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelEmail.CustomReportFieldName = "";
			mmLabelEmail.CustomReportKey = "";
			mmLabelEmail.CustomReportValueType = 1;
			mmLabelEmail.Enabled = false;
			mmLabelEmail.IsComboTextBox = false;
			mmLabelEmail.IsModified = false;
			mmLabelEmail.Location = new System.Drawing.Point(99, 143);
			mmLabelEmail.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelEmail.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelEmail.Name = "mmLabelEmail";
			mmLabelEmail.NullText = "0";
			mmLabelEmail.ReadOnly = true;
			mmLabelEmail.Size = new System.Drawing.Size(231, 20);
			mmLabelEmail.TabIndex = 42;
			mmLabelMobile.AllowDecimal = true;
			mmLabelMobile.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelMobile.CustomReportFieldName = "";
			mmLabelMobile.CustomReportKey = "";
			mmLabelMobile.CustomReportValueType = 1;
			mmLabelMobile.Enabled = false;
			mmLabelMobile.IsComboTextBox = false;
			mmLabelMobile.IsModified = false;
			mmLabelMobile.Location = new System.Drawing.Point(99, 120);
			mmLabelMobile.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelMobile.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelMobile.Name = "mmLabelMobile";
			mmLabelMobile.NullText = "0";
			mmLabelMobile.ReadOnly = true;
			mmLabelMobile.Size = new System.Drawing.Size(231, 20);
			mmLabelMobile.TabIndex = 41;
			linkLabelTenant.AutoSize = true;
			appearance15.ForeColor = System.Drawing.Color.Blue;
			linkLabelTenant.LinkAppearance = appearance15;
			linkLabelTenant.Location = new System.Drawing.Point(17, 26);
			linkLabelTenant.Name = "linkLabelTenant";
			linkLabelTenant.Size = new System.Drawing.Size(73, 14);
			linkLabelTenant.TabIndex = 27;
			linkLabelTenant.TabStop = true;
			linkLabelTenant.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelTenant.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelTenant.Value = "Tenant Name:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			linkLabelTenant.VisitedLinkAppearance = appearance16;
			linkLabelTenant.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelTenant_LinkClicked);
			mmLabelTenantID.AutoSize = true;
			mmLabelTenantID.BackColor = System.Drawing.Color.Transparent;
			mmLabelTenantID.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabelTenantID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabelTenantID.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabelTenantID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabelTenantID.IsFieldHeader = false;
			mmLabelTenantID.IsRequired = false;
			mmLabelTenantID.Location = new System.Drawing.Point(298, 22);
			mmLabelTenantID.Name = "mmLabelTenantID";
			mmLabelTenantID.PenWidth = 1f;
			mmLabelTenantID.ShowBorder = false;
			mmLabelTenantID.Size = new System.Drawing.Size(0, 13);
			mmLabelTenantID.TabIndex = 26;
			mmLabelTenantID.Visible = false;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(17, 124);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(41, 13);
			mmLabel8.TabIndex = 12;
			mmLabel8.Text = "Mobile:";
			textBoxAddress.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAddress.CustomReportFieldName = "";
			textBoxAddress.CustomReportKey = "";
			textBoxAddress.CustomReportValueType = 1;
			textBoxAddress.IsComboTextBox = false;
			textBoxAddress.IsModified = false;
			textBoxAddress.Location = new System.Drawing.Point(99, 45);
			textBoxAddress.MaxLength = 255;
			textBoxAddress.Multiline = true;
			textBoxAddress.Name = "textBoxAddress";
			textBoxAddress.ReadOnly = true;
			textBoxAddress.Size = new System.Drawing.Size(231, 72);
			textBoxAddress.TabIndex = 25;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(17, 48);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(50, 13);
			mmLabel4.TabIndex = 23;
			mmLabel4.Text = "Address:";
			groupBox1.Controls.Add(textBoxTaxAmount);
			groupBox1.Controls.Add(ultraFormattedLinkLabel1);
			groupBox1.Controls.Add(mmLabelTotal);
			groupBox1.Controls.Add(mmLabel1);
			groupBox1.Controls.Add(mmLabelDepositAmount);
			groupBox1.Controls.Add(mmLabelRentAmount);
			groupBox1.Controls.Add(mmLabelEndDate);
			groupBox1.Controls.Add(mmLabel1StratDate);
			groupBox1.Controls.Add(mmLabelTransactionDate);
			groupBox1.Controls.Add(mmLabelVoucherID);
			groupBox1.Controls.Add(mmLabelSysDocID);
			groupBox1.Controls.Add(mmLabel6);
			groupBox1.Controls.Add(mmLabel3);
			groupBox1.Controls.Add(mmLabel12);
			groupBox1.Controls.Add(mmLabel18);
			groupBox1.Controls.Add(mmLabel9);
			groupBox1.Controls.Add(mmLabel15);
			groupBox1.Controls.Add(mmLabel16);
			groupBox1.Location = new System.Drawing.Point(348, 37);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(389, 227);
			groupBox1.TabIndex = 38;
			groupBox1.TabStop = false;
			groupBox1.Text = "Register Details";
			ultraFormattedLinkLabel1.AutoSize = true;
			appearance17.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.LinkAppearance = appearance17;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(19, 48);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(82, 14);
			ultraFormattedLinkLabel1.TabIndex = 41;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Reg VoucherID:";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance18;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			mmLabelTotal.AllowDecimal = true;
			mmLabelTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelTotal.CustomReportFieldName = "";
			mmLabelTotal.CustomReportKey = "";
			mmLabelTotal.CustomReportValueType = 1;
			mmLabelTotal.Enabled = false;
			mmLabelTotal.IsComboTextBox = false;
			mmLabelTotal.IsModified = false;
			mmLabelTotal.Location = new System.Drawing.Point(145, 197);
			mmLabelTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelTotal.Name = "mmLabelTotal";
			mmLabelTotal.NullText = "0";
			mmLabelTotal.ReadOnly = true;
			mmLabelTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
			mmLabelTotal.Size = new System.Drawing.Size(230, 20);
			mmLabelTotal.TabIndex = 50;
			mmLabelTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabelDepositAmount.AllowDecimal = true;
			mmLabelDepositAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelDepositAmount.CustomReportFieldName = "";
			mmLabelDepositAmount.CustomReportKey = "";
			mmLabelDepositAmount.CustomReportValueType = 1;
			mmLabelDepositAmount.Enabled = false;
			mmLabelDepositAmount.IsComboTextBox = false;
			mmLabelDepositAmount.IsModified = false;
			mmLabelDepositAmount.Location = new System.Drawing.Point(145, 153);
			mmLabelDepositAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelDepositAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelDepositAmount.Name = "mmLabelDepositAmount";
			mmLabelDepositAmount.NullText = "0";
			mmLabelDepositAmount.ReadOnly = true;
			mmLabelDepositAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
			mmLabelDepositAmount.Size = new System.Drawing.Size(230, 20);
			mmLabelDepositAmount.TabIndex = 49;
			mmLabelDepositAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabelRentAmount.AllowDecimal = true;
			mmLabelRentAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelRentAmount.CustomReportFieldName = "";
			mmLabelRentAmount.CustomReportKey = "";
			mmLabelRentAmount.CustomReportValueType = 1;
			mmLabelRentAmount.Enabled = false;
			mmLabelRentAmount.IsComboTextBox = false;
			mmLabelRentAmount.IsModified = false;
			mmLabelRentAmount.Location = new System.Drawing.Point(145, 131);
			mmLabelRentAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelRentAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelRentAmount.Name = "mmLabelRentAmount";
			mmLabelRentAmount.NullText = "0";
			mmLabelRentAmount.ReadOnly = true;
			mmLabelRentAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
			mmLabelRentAmount.Size = new System.Drawing.Size(230, 20);
			mmLabelRentAmount.TabIndex = 48;
			mmLabelRentAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabelEndDate.AllowDecimal = true;
			mmLabelEndDate.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelEndDate.CustomReportFieldName = "";
			mmLabelEndDate.CustomReportKey = "";
			mmLabelEndDate.CustomReportValueType = 1;
			mmLabelEndDate.Enabled = false;
			mmLabelEndDate.IsComboTextBox = false;
			mmLabelEndDate.IsModified = false;
			mmLabelEndDate.Location = new System.Drawing.Point(145, 109);
			mmLabelEndDate.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelEndDate.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelEndDate.Name = "mmLabelEndDate";
			mmLabelEndDate.NullText = "0";
			mmLabelEndDate.ReadOnly = true;
			mmLabelEndDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			mmLabelEndDate.Size = new System.Drawing.Size(230, 20);
			mmLabelEndDate.TabIndex = 47;
			mmLabelEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel1StratDate.AllowDecimal = true;
			mmLabel1StratDate.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabel1StratDate.CustomReportFieldName = "";
			mmLabel1StratDate.CustomReportKey = "";
			mmLabel1StratDate.CustomReportValueType = 1;
			mmLabel1StratDate.Enabled = false;
			mmLabel1StratDate.IsComboTextBox = false;
			mmLabel1StratDate.IsModified = false;
			mmLabel1StratDate.Location = new System.Drawing.Point(145, 87);
			mmLabel1StratDate.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabel1StratDate.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabel1StratDate.Name = "mmLabel1StratDate";
			mmLabel1StratDate.NullText = "0";
			mmLabel1StratDate.ReadOnly = true;
			mmLabel1StratDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			mmLabel1StratDate.Size = new System.Drawing.Size(230, 20);
			mmLabel1StratDate.TabIndex = 46;
			mmLabel1StratDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabelTransactionDate.AllowDecimal = true;
			mmLabelTransactionDate.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelTransactionDate.CustomReportFieldName = "";
			mmLabelTransactionDate.CustomReportKey = "";
			mmLabelTransactionDate.CustomReportValueType = 1;
			mmLabelTransactionDate.Enabled = false;
			mmLabelTransactionDate.IsComboTextBox = false;
			mmLabelTransactionDate.IsModified = false;
			mmLabelTransactionDate.Location = new System.Drawing.Point(145, 65);
			mmLabelTransactionDate.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelTransactionDate.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelTransactionDate.Name = "mmLabelTransactionDate";
			mmLabelTransactionDate.NullText = "0";
			mmLabelTransactionDate.ReadOnly = true;
			mmLabelTransactionDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			mmLabelTransactionDate.Size = new System.Drawing.Size(230, 20);
			mmLabelTransactionDate.TabIndex = 45;
			mmLabelTransactionDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabelVoucherID.AllowDecimal = true;
			mmLabelVoucherID.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelVoucherID.CustomReportFieldName = "";
			mmLabelVoucherID.CustomReportKey = "";
			mmLabelVoucherID.CustomReportValueType = 1;
			mmLabelVoucherID.Enabled = false;
			mmLabelVoucherID.IsComboTextBox = false;
			mmLabelVoucherID.IsModified = false;
			mmLabelVoucherID.Location = new System.Drawing.Point(145, 43);
			mmLabelVoucherID.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelVoucherID.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelVoucherID.Name = "mmLabelVoucherID";
			mmLabelVoucherID.NullText = "0";
			mmLabelVoucherID.ReadOnly = true;
			mmLabelVoucherID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			mmLabelVoucherID.Size = new System.Drawing.Size(230, 20);
			mmLabelVoucherID.TabIndex = 44;
			mmLabelVoucherID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabelSysDocID.AllowDecimal = true;
			mmLabelSysDocID.BackColor = System.Drawing.Color.WhiteSmoke;
			mmLabelSysDocID.CustomReportFieldName = "";
			mmLabelSysDocID.CustomReportKey = "";
			mmLabelSysDocID.CustomReportValueType = 1;
			mmLabelSysDocID.Enabled = false;
			mmLabelSysDocID.IsComboTextBox = false;
			mmLabelSysDocID.IsModified = false;
			mmLabelSysDocID.Location = new System.Drawing.Point(145, 21);
			mmLabelSysDocID.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			mmLabelSysDocID.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			mmLabelSysDocID.Name = "mmLabelSysDocID";
			mmLabelSysDocID.NullText = "0";
			mmLabelSysDocID.ReadOnly = true;
			mmLabelSysDocID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			mmLabelSysDocID.Size = new System.Drawing.Size(230, 20);
			mmLabelSysDocID.TabIndex = 43;
			mmLabelSysDocID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(19, 200);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(75, 13);
			mmLabel6.TabIndex = 40;
			mmLabel6.Text = "Total Amount:";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(19, 156);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(87, 13);
			mmLabel3.TabIndex = 38;
			mmLabel3.Text = "Deposit Amount:";
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(19, 24);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(79, 13);
			mmLabel12.TabIndex = 26;
			mmLabel12.Text = "Reg SysDocID:";
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(19, 134);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(74, 13);
			mmLabel18.TabIndex = 36;
			mmLabel18.Text = "Rent Amount:";
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(19, 68);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(93, 13);
			mmLabel9.TabIndex = 28;
			mmLabel9.Text = "Transaction Date:";
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(19, 112);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(100, 13);
			mmLabel15.TabIndex = 33;
			mmLabel15.Text = "Contract End Date:";
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(19, 90);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(106, 13);
			mmLabel16.TabIndex = 32;
			mmLabel16.Text = "Contract Start Date:";
			mmLabelUnitName.AutoSize = true;
			mmLabelUnitName.BackColor = System.Drawing.Color.Transparent;
			mmLabelUnitName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabelUnitName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabelUnitName.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabelUnitName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabelUnitName.IsFieldHeader = false;
			mmLabelUnitName.IsRequired = false;
			mmLabelUnitName.Location = new System.Drawing.Point(118, 14);
			mmLabelUnitName.Name = "mmLabelUnitName";
			mmLabelUnitName.PenWidth = 1f;
			mmLabelUnitName.ShowBorder = false;
			mmLabelUnitName.Size = new System.Drawing.Size(17, 13);
			mmLabelUnitName.TabIndex = 22;
			mmLabelUnitName.Text = "--";
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(17, 14);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(86, 13);
			mmLabel10.TabIndex = 21;
			mmLabel10.Text = "Property Unit:";
			ultraTabPageControl2.Controls.Add(dataGridList);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(747, 413);
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance19;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance20.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance20.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance20.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance20;
			appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance21;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance22.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance22.BackColor2 = System.Drawing.SystemColors.Control;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance22;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance23;
			appearance24.BackColor = System.Drawing.SystemColors.Highlight;
			appearance24.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance24;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance25;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			appearance26.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance26;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance27.BackColor = System.Drawing.SystemColors.Control;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance27;
			appearance28.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance28;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance29;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance30;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(1, 0);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(745, 364);
			dataGridList.TabIndex = 1;
			dataGridList.Text = "dataGridList1";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(771, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.Visible = false;
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 449);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(771, 40);
			panelButtons.TabIndex = 11;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(771, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(661, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(20, 0);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(751, 449);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 17;
			appearance31.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance31;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&Current Details";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "History";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(747, 426);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxTaxAmount.AllowDecimal = true;
			textBoxTaxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaxAmount.CustomReportFieldName = "";
			textBoxTaxAmount.CustomReportKey = "";
			textBoxTaxAmount.CustomReportValueType = 1;
			textBoxTaxAmount.Enabled = false;
			textBoxTaxAmount.IsComboTextBox = false;
			textBoxTaxAmount.IsModified = false;
			textBoxTaxAmount.Location = new System.Drawing.Point(145, 175);
			textBoxTaxAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTaxAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTaxAmount.Name = "textBoxTaxAmount";
			textBoxTaxAmount.NullText = "0";
			textBoxTaxAmount.ReadOnly = true;
			textBoxTaxAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
			textBoxTaxAmount.Size = new System.Drawing.Size(230, 20);
			textBoxTaxAmount.TabIndex = 52;
			textBoxTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(19, 178);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(69, 13);
			mmLabel1.TabIndex = 51;
			mmLabel1.Text = "Tax Amount:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(771, 489);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "PropertyUnitCurrentDetailsForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Property Unit Current Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridListChque).EndInit();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ultraTabPageControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
