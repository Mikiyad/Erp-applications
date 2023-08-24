using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class CardSettingsForm : Form, IForm
	{
		private string sysDocTypeID = "";

		private string sysDocID = "";

		private const string TABLENAME_CONST = "System_Document";

		private const string IDFIELD_CONST = "SysDocID";

		private string docName = "";

		public int LocationY = 20;

		public int LocationX = 70;

		private bool isNewRecord = true;

		private SysDocEntityTypes entityType = SysDocEntityTypes.CustomerClass;

		private SysDocTypes doctype = SysDocTypes.None;

		public bool isEnabled = true;

		private CompanyInformationData currentData;

		private CompanyOptionData companyOptionData;

		private DataSet data;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator8;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageUserDefined;

		private UltraTabPageControl ultraTabPageControl3;

		private UltraTabPageControl ultraTabPageControl5;

		private UltraTabPageControl ultraTabPageControl6;

		private UltraTabPageControl ultraTabPageControl7;

		private MMLabel mmLabel58;

		private OptionsAllowComboBox comboBoxoptionsAllocation;

		private MMLabel mmLabel59;

		private UltraTabPageControl tabPagePOS;

		private UltraTabPageControl ultraTabPageProject;

		private UltraTabPageControl ultraTabPageControl8;

		private UltraTabPageControl ultraTabPageControl9;

		private UltraTabPageControl ultraTabPageControl10;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraTabPageControl tabPageControlProperty;

		private UDFTypeComboBox comboBoxUDFType;

		private MMLabel labelCode;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6014;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string SysDocTypeID
		{
			set
			{
				sysDocTypeID = value;
				formManager.ResetDirty();
			}
		}

		public string SysDocID
		{
			set
			{
				sysDocID = value;
				formManager.ResetDirty();
			}
		}

		public bool IsEnabled
		{
			set
			{
				isEnabled = value;
			}
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (value)
				{
					docName = "";
				}
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
			}
		}

		public CardSettingsForm()
		{
			InitializeComponent();
			AddEvents();
			base.AcceptButton = buttonSave;
		}

		private void AddEvents()
		{
			base.Load += SysDocDetailsForm_Load;
		}

		private void checkBoxPrintAfterSave_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void sysDocComboBoxRecurringInvoice_SelectedIndexChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
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

		private bool GetDataSales()
		{
			try
			{
				currentData = Factory.CompanyInformationSystem.GetCompanyInformation();
				data = Factory.CompanyOptionSystem.GetSysDocCompanyOptionList();
				DataTable table = data.Tables["Company_Option"].Clone();
				if (this.companyOptionData == null)
				{
					CompanyOptionData companyOptionData = new CompanyOptionData();
					companyOptionData.Tables.Remove("Company_Option");
					companyOptionData.Tables.Add(table);
					this.companyOptionData = companyOptionData;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetDataAccounts()
		{
			try
			{
				currentData = Factory.CompanyInformationSystem.GetCompanyInformation();
				data = Factory.CompanyOptionSystem.GetSysDocCompanyOptionList();
				DataTable table = data.Tables["Company_Option"].Clone();
				if (this.companyOptionData == null)
				{
					CompanyOptionData companyOptionData = new CompanyOptionData();
					companyOptionData.Tables.Remove("Company_Option");
					companyOptionData.Tables.Add(table);
					this.companyOptionData = companyOptionData;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetDataProperty()
		{
			try
			{
				currentData = Factory.CompanyInformationSystem.GetCompanyInformation();
				data = Factory.CompanyOptionSystem.GetSysDocCompanyOptionList();
				DataTable table = data.Tables["Company_Option"].Clone();
				if (this.companyOptionData == null)
				{
					CompanyOptionData companyOptionData = new CompanyOptionData();
					companyOptionData.Tables.Remove("Company_Option");
					companyOptionData.Tables.Add(table);
					this.companyOptionData = companyOptionData;
				}
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
			SaveData();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					IsNewRecord = false;
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add("Types");
			dataSet.Tables[0].Columns.Add("Code");
			dataSet.Tables[0].Columns.Add("Name");
			foreach (object value in Enum.GetValues(typeof(DataComboType)))
			{
				int num = (int)(DataComboType)Enum.Parse(typeof(DataComboType), value.ToString());
				if (!(value.ToString().ToLower() == "none"))
				{
					dataSet.Tables[0].Rows.Add(num, value.ToString());
				}
			}
			dataSet.Tables[0].DefaultView.Sort = "Name ASC";
			DataSet dataSet2 = new DataSet();
			dataSet2.Tables.Add(dataSet.Tables[0].DefaultView.ToTable());
			comboBoxUDFType.DataSource = dataSet2;
			LoadControls();
		}

		private void LoadControls()
		{
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				if (!IsNewRecord)
				{
					IsNewRecord = true;
					ClearForm();
				}
				return true;
			}
			if (!IsNewRecord)
			{
				switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
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
				bool flag = Factory.CompanyInformationSystem.UpdateCompanyOptions(currentData);
				flag &= Factory.CompanyOptionSystem.CreateSysDocCompanyOption(companyOptionData, 1);
				if (flag)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.SysDoc, needRefresh: true);
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					CompanyPreferences.LoadCompanyPreferences();
					CompanyOptions.LoadCompanyOptions();
					CompanyOptions.LoadSysDocCompanyOptions();
					ErrorHelper.InformationMessage("Some changes may need to restart the application in order to take effect.");
					ClearForm();
					Close();
				}
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
				Close();
			}
		}

		private void ClearForm()
		{
			formManager.ResetDirty();
		}

		private void SponsorGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void SponsorGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private bool Delete()
		{
			return true;
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
			int num = 0;
			if (!(num.ToString() == "0"))
			{
				LoadData(DatabaseHelper.GetLastID("System_Document", "SysDocID", "SysDocType", num.ToString()));
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (!(num.ToString() == "0"))
			{
				LoadData(DatabaseHelper.GetFirstID("System_Document", "SysDocID", "SysDocType", num.ToString()));
			}
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("System_Document", "SysDocID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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

		private void SysDocDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					LoadData("1");
					LoadControls();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void GetControls()
		{
			new DataSet();
			DataSet controls = Factory.SystemDocumentSystem.GetControls();
			LocationY = 20;
			foreach (DataRow row in controls.Tables[0].Rows)
			{
				SysDocControlTypes controlType = (SysDocControlTypes)int.Parse(row["ControlTypeId"].ToString());
				CreateControl(controlType, row["ControlValue"].ToString());
				checked
				{
					LocationY += 30;
				}
			}
		}

		private void LoadTabs(SysDocTypes docType)
		{
		}

		private void CreateControl(SysDocControlTypes ControlType, string value)
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
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.SysDoc);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void buttonSelectTemplatePath_Click(object sender, EventArgs e)
		{
		}

		private void buttonEntitySelection_Click(object sender, EventArgs e)
		{
		}

		private void buttonItemClasses_Click(object sender, EventArgs e)
		{
		}

		private void buttonAccounts_Click(object sender, EventArgs e)
		{
		}

		private void buttonAccountClasses_Click(object sender, EventArgs e)
		{
		}

		private void buttonUsers_Click(object sender, EventArgs e)
		{
		}

		private void checkBoxAllowmultiprint_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void buttonAttachements_Click(object sender, EventArgs e)
		{
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.FreightChargesFormObj);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.SysDoc);
		}

		private void checkBoxPrintAfterSave_CheckedChanged_1(object sender, EventArgs e)
		{
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void ultraTabControl1_SelectedTabChanged_1(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void ultraTabControl1_SelectedTabChanged_2(object sender, SelectedTabChangedEventArgs e)
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab12 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab13 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.CardSettingsForm));
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl7 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			comboBoxoptionsAllocation = new Micromind.DataControls.OptionsAllowComboBox();
			mmLabel59 = new Micromind.UISupport.MMLabel();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			tabPagePOS = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageProject = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl8 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl9 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl10 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			tabPageControlProperty = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			formManager = new Micromind.DataControls.FormManager();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			comboBoxUDFType = new Micromind.DataControls.UDFTypeComboBox();
			labelCode = new Micromind.UISupport.MMLabel();
			ultraTabPageControl7.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageDetails.AutoScroll = true;
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl7.Controls.Add(mmLabel58);
			ultraTabPageControl7.Controls.Add(comboBoxoptionsAllocation);
			ultraTabPageControl7.Controls.Add(mmLabel59);
			ultraTabPageControl7.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl7.Name = "ultraTabPageControl7";
			ultraTabPageControl7.Size = new System.Drawing.Size(690, 406);
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(24, 376);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(41, 13);
			mmLabel58.TabIndex = 23;
			mmLabel58.Text = "Action:";
			mmLabel58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			comboBoxoptionsAllocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxoptionsAllocation.FormattingEnabled = true;
			comboBoxoptionsAllocation.HasPasswordItem = false;
			comboBoxoptionsAllocation.Items.AddRange(new object[2]
			{
				"Allow",
				"Don't Allow "
			});
			comboBoxoptionsAllocation.Location = new System.Drawing.Point(70, 373);
			comboBoxoptionsAllocation.Name = "comboBoxoptionsAllocation";
			comboBoxoptionsAllocation.Size = new System.Drawing.Size(119, 21);
			comboBoxoptionsAllocation.TabIndex = 22;
			mmLabel59.AutoSize = true;
			mmLabel59.BackColor = System.Drawing.Color.Transparent;
			mmLabel59.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel59.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel59.IsFieldHeader = false;
			mmLabel59.IsRequired = false;
			mmLabel59.Location = new System.Drawing.Point(14, 357);
			mmLabel59.Name = "mmLabel59";
			mmLabel59.PenWidth = 1f;
			mmLabel59.ShowBorder = false;
			mmLabel59.Size = new System.Drawing.Size(114, 13);
			mmLabel59.TabIndex = 24;
			mmLabel59.Text = "To Remove Allocation:";
			mmLabel59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			tabPageUserDefined.AutoScroll = true;
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl3.AutoScroll = true;
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl6.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl6.Name = "ultraTabPageControl6";
			ultraTabPageControl6.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(690, 406);
			tabPagePOS.Location = new System.Drawing.Point(-10000, -10000);
			tabPagePOS.Name = "tabPagePOS";
			tabPagePOS.Size = new System.Drawing.Size(690, 406);
			ultraTabPageProject.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageProject.Name = "ultraTabPageProject";
			ultraTabPageProject.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl8.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl8.Name = "ultraTabPageControl8";
			ultraTabPageControl8.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl9.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl9.Name = "ultraTabPageControl9";
			ultraTabPageControl9.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl10.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl10.Name = "ultraTabPageControl10";
			ultraTabPageControl10.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(690, 406);
			tabPageControlProperty.Location = new System.Drawing.Point(1, 20);
			tabPageControlProperty.Name = "tabPageControlProperty";
			tabPageControlProperty.Size = new System.Drawing.Size(690, 406);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButton1,
				toolStripSeparator8,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(700, 31);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 28);
			toolStripButton1.Text = "Open List";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
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
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 527);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(700, 40);
			panelButtons.TabIndex = 22;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(596, 9);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 16;
			xpButton1.Text = "&Cancel";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(496, 9);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 15;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(700, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(14, 36);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(95, 15);
			ultraFormattedLinkLabel2.TabIndex = 126;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Document Type:";
			ultraFormattedLinkLabel2.Visible = false;
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
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
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage2);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Controls.Add(ultraTabPageControl5);
			ultraTabControl1.Controls.Add(ultraTabPageControl6);
			ultraTabControl1.Controls.Add(ultraTabPageControl7);
			ultraTabControl1.Controls.Add(tabPagePOS);
			ultraTabControl1.Controls.Add(ultraTabPageProject);
			ultraTabControl1.Controls.Add(ultraTabPageControl8);
			ultraTabControl1.Controls.Add(ultraTabPageControl9);
			ultraTabControl1.Controls.Add(ultraTabPageControl10);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(tabPageControlProperty);
			ultraTabControl1.Location = new System.Drawing.Point(4, 94);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage2;
			ultraTabControl1.Size = new System.Drawing.Size(692, 427);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 123;
			ultraTab.TabPage = tabPageDetails;
			ultraTab.Text = "C&ustomers && Sales";
			ultraTab.Visible = false;
			ultraTab2.TabPage = ultraTabPageControl7;
			ultraTab2.Text = "Receivables && &Collection";
			ultraTab2.Visible = false;
			ultraTab3.TabPage = tabPageUserDefined;
			ultraTab3.Text = "&Vendors && Purchasing";
			ultraTab3.Visible = false;
			ultraTab4.TabPage = ultraTabPageControl3;
			ultraTab4.Text = "&Inventory";
			ultraTab4.Visible = false;
			ultraTab5.TabPage = ultraTabPageControl6;
			ultraTab5.Text = "&HR && Admin";
			ultraTab5.Visible = false;
			ultraTab6.TabPage = ultraTabPageControl5;
			ultraTab6.Text = "&Reports";
			ultraTab6.Visible = false;
			ultraTab7.TabPage = tabPagePOS;
			ultraTab7.Text = "PO&S";
			ultraTab7.Visible = false;
			ultraTab8.TabPage = ultraTabPageProject;
			ultraTab8.Text = "&Project";
			ultraTab8.Visible = false;
			ultraTab9.TabPage = ultraTabPageControl8;
			ultraTab9.Text = "CR&M";
			ultraTab9.Visible = false;
			ultraTab10.TabPage = ultraTabPageControl9;
			ultraTab10.Text = "General";
			ultraTab10.Visible = false;
			ultraTab11.TabPage = ultraTabPageControl10;
			ultraTab11.Text = "Legal";
			ultraTab11.Visible = false;
			ultraTab12.TabPage = ultraTabPageControl1;
			ultraTab12.Text = "Accounts";
			ultraTab12.Visible = false;
			ultraTab13.Key = "tabPageControlProperty";
			ultraTab13.TabPage = tabPageControlProperty;
			ultraTab13.Text = "Property";
			ultraTab13.Visible = false;
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[13]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7,
				ultraTab8,
				ultraTab9,
				ultraTab10,
				ultraTab11,
				ultraTab12,
				ultraTab13
			});
			ultraTabControl1.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(ultraTabControl1_SelectedTabChanged_2);
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(690, 406);
			comboBoxUDFType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxUDFType.FormattingEnabled = true;
			comboBoxUDFType.Location = new System.Drawing.Point(76, 31);
			comboBoxUDFType.Name = "comboBoxUDFType";
			comboBoxUDFType.Size = new System.Drawing.Size(230, 21);
			comboBoxUDFType.TabIndex = 124;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = false;
			labelCode.Location = new System.Drawing.Point(13, 34);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(36, 13);
			labelCode.TabIndex = 125;
			labelCode.Text = "Entity:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(700, 567);
			base.Controls.Add(comboBoxUDFType);
			base.Controls.Add(labelCode);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CardSettingsForm";
			Text = "Card Settings";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(SysDocDetailsForm_Load);
			ultraTabPageControl7.ResumeLayout(false);
			ultraTabPageControl7.PerformLayout();
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
