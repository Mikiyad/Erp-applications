using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Infragistics.Win;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class EmailConfigForm : Form, IForm
	{
		private CompanyInformationData currentData;

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private FormManager formManager;

		private NumberTextBox FromtextBoxToMonth4;

		private MMTextBox textBoxE1Name;

		private MMLabel mmLabel29;

		private MMLabel mmLabel30;

		private MMLabel mmLabel36;

		private MMTextBox textBoxE1Address;

		private MMLabel mmLabel1;

		private MMLabel mmLabel3;

		private MMTextBox textBoxE1OutServer;

		private MMTextBox textBoxE1Password;

		private MMLabel mmLabel4;

		private MMTextBox textBoxE1UserName;

		private MMLabel mmLabel5;

		private Button buttonCopyE4;

		private MMTextBox textBoxE3Password;

		private MMLabel mmLabel10;

		private MMTextBox textBoxE3UserName;

		private MMLabel mmLabel11;

		private MMTextBox textBoxE3OutServer;

		private MMLabel mmLabel12;

		private MMTextBox textBoxE3Address;

		private MMLabel mmLabel13;

		private MMTextBox textBoxE3Name;

		private MMLabel mmLabel14;

		private MMLabel mmLabel15;

		private MMLabel mmLabel16;

		private MMLabel mmLabel17;

		private MMTextBox textBoxE1InServer;

		private MMTextBox textBoxE3InServer;

		private MMLabel mmLabel18;

		private MMTextBox textBoxE4InServer;

		private MMLabel mmLabel20;

		private Button buttonCopyE3;

		private MMTextBox textBoxE4Password;

		private MMLabel mmLabel21;

		private MMTextBox textBoxE4UserName;

		private MMLabel mmLabel22;

		private MMTextBox textBoxE4OutServer;

		private MMLabel mmLabel23;

		private MMTextBox textBoxE4Address;

		private MMLabel mmLabel24;

		private MMTextBox textBoxE4Name;

		private MMLabel mmLabel25;

		private MMLabel mmLabel26;

		private MMLabel mmLabel27;

		private MMLabel mmLabel38;

		private MMLabel mmLabel37;

		private MMTextBox textBoxE2InServer;

		private MMLabel mmLabel19;

		private Button buttonCopyE2;

		private MMTextBox textBoxE2Password;

		private MMLabel mmLabel2;

		private MMTextBox textBoxE2UserName;

		private MMLabel mmLabel6;

		private MMTextBox textBoxE2OutServer;

		private MMLabel mmLabel7;

		private MMTextBox textBoxE2Address;

		private MMLabel mmLabel8;

		private MMTextBox textBoxE2Name;

		private MMLabel mmLabel9;

		private CheckBox checkBoxE1SSL;

		private CheckBox checkBoxE2SSL;

		private CheckBox checkBoxE3SSL;

		private CheckBox checkBoxE4SSL;

		private MMTextBox textBoxE1Port;

		private MMLabel mmLabel28;

		private MMTextBox textBoxE2Port;

		private MMLabel mmLabel31;

		private MMTextBox textBoxE3Port;

		private MMLabel mmLabel32;

		private MMTextBox textBoxE4Port;

		private MMLabel mmLabel33;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private GroupBox groupBox3;

		private MMLabel mmLabel39;

		private RibbonControl ribbonControl1;

		private GroupBox groupBox2;

		private MMLabel mmLabel35;

		private GroupBox groupBox1;

		private CheckBox checkBoxCCSalesperson;

		private MMLabel mmLabel34;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraTabPageControl ultraTabPageControl2;

		private MMLabel mmLabel41;

		private MMTextBox textBoxCCAccGeneral;

		private MMLabel mmLabel42;

		private MMTextBox textBoxCCPayslip;

		private MMLabel mmLabel40;

		private MMTextBox textBoxCCStatement;

		private MMLabel mmLabel43;

		private MMTextBox textBoxCCPurchase;

		private MMLabel mmLabel44;

		private MMTextBox textBoxCCSales;

		private MMLabel mmLabel45;

		private MMTextBox textBoxCCNotification;

		private MMTextBox textBoxAccountBody;

		private MMTextBox textBoxPayslipBody;

		private MMTextBox textBoxStatementBody;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6002;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return false;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public EmailConfigForm()
		{
			InitializeComponent();
			Translator.Translators.Translate(this);
		}

		private bool GetData()
		{
			try
			{
				currentData = new CompanyInformationData();
				DataRow dataRow = currentData.EmailConfigTable.NewRow();
				dataRow["EmailID"] = 1;
				dataRow["CompanyID"] = 1;
				dataRow["EmailAddress"] = textBoxE1Address.Text;
				dataRow["EmailPass"] = textBoxE1Password.Text;
				dataRow["UserName"] = textBoxE1UserName.Text;
				dataRow["SenderName"] = textBoxE1Name.Text;
				dataRow["OutgoingServer"] = textBoxE1OutServer.Text;
				dataRow["IncommingServer"] = textBoxE1InServer.Text;
				dataRow["EmailUseSSL"] = checkBoxE1SSL.Checked;
				dataRow["EmailSMTPPort"] = textBoxE1Port.Text;
				dataRow["CCSalesperson"] = checkBoxCCSalesperson.Checked;
				dataRow["CC1"] = textBoxCCAccGeneral.Text;
				dataRow["CC2"] = textBoxCCStatement.Text;
				dataRow["CC3"] = textBoxCCPayslip.Text;
				dataRow["Body1"] = textBoxAccountBody.Text;
				dataRow["Body2"] = textBoxStatementBody.Text;
				dataRow["Body3"] = textBoxPayslipBody.Text;
				currentData.EmailConfigTable.Rows.Add(dataRow);
				dataRow = currentData.EmailConfigTable.NewRow();
				dataRow["EmailID"] = 2;
				dataRow["CompanyID"] = 1;
				dataRow["EmailAddress"] = textBoxE2Address.Text;
				dataRow["EmailPass"] = textBoxE2Password.Text;
				dataRow["UserName"] = textBoxE2UserName.Text;
				dataRow["SenderName"] = textBoxE2Name.Text;
				dataRow["OutgoingServer"] = textBoxE2OutServer.Text;
				dataRow["IncommingServer"] = textBoxE2InServer.Text;
				dataRow["EmailUseSSL"] = checkBoxE2SSL.Checked;
				dataRow["EmailSMTPPort"] = textBoxE2Port.Text;
				dataRow["CC1"] = textBoxCCPurchase.Text;
				dataRow.EndEdit();
				currentData.EmailConfigTable.Rows.Add(dataRow);
				dataRow = currentData.EmailConfigTable.NewRow();
				dataRow["EmailID"] = 3;
				dataRow["CompanyID"] = 1;
				dataRow["EmailAddress"] = textBoxE3Address.Text;
				dataRow["EmailPass"] = textBoxE3Password.Text;
				dataRow["UserName"] = textBoxE3UserName.Text;
				dataRow["SenderName"] = textBoxE3Name.Text;
				dataRow["OutgoingServer"] = textBoxE3OutServer.Text;
				dataRow["IncommingServer"] = textBoxE3InServer.Text;
				dataRow["EmailUseSSL"] = checkBoxE3SSL.Checked;
				dataRow["EmailSMTPPort"] = textBoxE3Port.Text;
				dataRow["CC1"] = textBoxCCSales.Text;
				dataRow.EndEdit();
				currentData.EmailConfigTable.Rows.Add(dataRow);
				dataRow = currentData.EmailConfigTable.NewRow();
				dataRow["EmailID"] = 4;
				dataRow["CompanyID"] = 1;
				dataRow["EmailAddress"] = textBoxE4Address.Text;
				dataRow["EmailPass"] = textBoxE4Password.Text;
				dataRow["UserName"] = textBoxE4UserName.Text;
				dataRow["SenderName"] = textBoxE4Name.Text;
				dataRow["OutgoingServer"] = textBoxE4OutServer.Text;
				dataRow["IncommingServer"] = textBoxE4InServer.Text;
				dataRow["EmailUseSSL"] = checkBoxE4SSL.Checked;
				dataRow["EmailSMTPPort"] = textBoxE4Port.Text;
				dataRow["CC1"] = textBoxCCNotification.Text;
				dataRow.EndEdit();
				currentData.EmailConfigTable.Rows.Add(dataRow);
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
					currentData = Factory.CompanyInformationSystem.GetEmailConfig(CompanyEmailConfigTypes.None);
					if (currentData != null)
					{
						FillData();
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables["Email_Config"].Rows.Count == 0)
			{
				return;
			}
			DataRow[] array = currentData.Tables["Email_Config"].Select("EmailID = 1");
			if (array.Length == 0)
			{
				return;
			}
			DataRow dataRow = array[0];
			textBoxE1Address.Text = dataRow["EmailAddress"].ToString();
			textBoxE1Name.Text = dataRow["SenderName"].ToString();
			textBoxE1Password.Text = dataRow["EmailPass"].ToString();
			textBoxE1OutServer.Text = dataRow["OutgoingServer"].ToString();
			textBoxE1InServer.Text = dataRow["IncommingServer"].ToString();
			textBoxE1UserName.Text = dataRow["UserName"].ToString();
			textBoxE1Port.Text = dataRow["EmailSMTPPort"].ToString();
			if (dataRow["EmailUseSSL"] != DBNull.Value)
			{
				checkBoxE1SSL.Checked = bool.Parse(dataRow["EmailUseSSL"].ToString());
			}
			if (dataRow["CCSalesperson"] != DBNull.Value)
			{
				checkBoxCCSalesperson.Checked = bool.Parse(dataRow["CCSalesperson"].ToString());
			}
			textBoxCCAccGeneral.Text = dataRow["CC1"].ToString();
			textBoxCCStatement.Text = dataRow["CC2"].ToString();
			textBoxCCPayslip.Text = dataRow["CC3"].ToString();
			textBoxAccountBody.Text = dataRow["Body1"].ToString();
			textBoxStatementBody.Text = dataRow["Body2"].ToString();
			textBoxPayslipBody.Text = dataRow["Body3"].ToString();
			array = currentData.Tables["Email_Config"].Select("EmailID = 2");
			if (array.Length == 0)
			{
				return;
			}
			dataRow = array[0];
			textBoxE2Address.Text = dataRow["EmailAddress"].ToString();
			textBoxE2Name.Text = dataRow["SenderName"].ToString();
			textBoxE2Password.Text = dataRow["EmailPass"].ToString();
			textBoxE2OutServer.Text = dataRow["OutgoingServer"].ToString();
			textBoxE2InServer.Text = dataRow["IncommingServer"].ToString();
			textBoxE2UserName.Text = dataRow["UserName"].ToString();
			textBoxE2Port.Text = dataRow["EmailSMTPPort"].ToString();
			if (dataRow["EmailUseSSL"] != DBNull.Value)
			{
				checkBoxE2SSL.Checked = bool.Parse(dataRow["EmailUseSSL"].ToString());
			}
			textBoxCCPurchase.Text = dataRow["CC1"].ToString();
			array = currentData.Tables["Email_Config"].Select("EmailID = 3");
			if (array.Length == 0)
			{
				return;
			}
			dataRow = array[0];
			textBoxE3Address.Text = dataRow["EmailAddress"].ToString();
			textBoxE3Name.Text = dataRow["SenderName"].ToString();
			textBoxE3Password.Text = dataRow["EmailPass"].ToString();
			textBoxE3OutServer.Text = dataRow["OutgoingServer"].ToString();
			textBoxE3InServer.Text = dataRow["IncommingServer"].ToString();
			textBoxE3UserName.Text = dataRow["UserName"].ToString();
			textBoxE3Port.Text = dataRow["EmailSMTPPort"].ToString();
			if (dataRow["EmailUseSSL"] != DBNull.Value)
			{
				checkBoxE3SSL.Checked = bool.Parse(dataRow["EmailUseSSL"].ToString());
			}
			textBoxCCSales.Text = dataRow["CC1"].ToString();
			array = currentData.Tables["Email_Config"].Select("EmailID = 4");
			if (array.Length != 0)
			{
				dataRow = array[0];
				textBoxE4Address.Text = dataRow["EmailAddress"].ToString();
				textBoxE4Name.Text = dataRow["SenderName"].ToString();
				textBoxE4Password.Text = dataRow["EmailPass"].ToString();
				textBoxE4OutServer.Text = dataRow["OutgoingServer"].ToString();
				textBoxE4InServer.Text = dataRow["IncommingServer"].ToString();
				textBoxE4UserName.Text = dataRow["UserName"].ToString();
				textBoxE4Port.Text = dataRow["EmailSMTPPort"].ToString();
				if (dataRow["EmailUseSSL"] != DBNull.Value)
				{
					checkBoxE4SSL.Checked = bool.Parse(dataRow["EmailUseSSL"].ToString());
				}
				textBoxCCNotification.Text = dataRow["CC1"].ToString();
			}
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				Close();
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
				bool flag = Factory.CompanyInformationSystem.UpdateEmailConfig(currentData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
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
			if (!screenRight.Edit)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
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
			formManager.ResetDirty();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					LoadData("1");
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

		private void buttonCopy_Click(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			textBoxE4Address.Text = textBoxE1Address.Text;
			textBoxE4Name.Text = textBoxE1Name.Text;
			textBoxE4Password.Text = textBoxE1Password.Text;
			textBoxE4OutServer.Text = textBoxE1OutServer.Text;
			textBoxE4UserName.Text = textBoxE1UserName.Text;
			textBoxE4InServer.Text = textBoxE1InServer.Text;
		}

		private void buttonCopyE2_Click(object sender, EventArgs e)
		{
			textBoxE2Address.Text = textBoxE1Address.Text;
			textBoxE2Name.Text = textBoxE1Name.Text;
			textBoxE2Password.Text = textBoxE1Password.Text;
			textBoxE2OutServer.Text = textBoxE1OutServer.Text;
			textBoxE2InServer.Text = textBoxE1InServer.Text;
			textBoxE2UserName.Text = textBoxE1UserName.Text;
		}

		private void buttonCopyE3_Click(object sender, EventArgs e)
		{
			textBoxE3Address.Text = textBoxE1Address.Text;
			textBoxE3Name.Text = textBoxE1Name.Text;
			textBoxE3Password.Text = textBoxE1Password.Text;
			textBoxE3OutServer.Text = textBoxE1OutServer.Text;
			textBoxE3InServer.Text = textBoxE1InServer.Text;
			textBoxE3UserName.Text = textBoxE1UserName.Text;
		}

		private void mmLabel35_Click(object sender, EventArgs e)
		{
		}

		private void textBoxAccountBody_TextChanged(object sender, EventArgs e)
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.EmailConfigForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			groupBox3 = new System.Windows.Forms.GroupBox();
			textBoxAccountBody = new Micromind.UISupport.MMTextBox();
			mmLabel41 = new Micromind.UISupport.MMLabel();
			textBoxCCAccGeneral = new Micromind.UISupport.MMTextBox();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			groupBox2 = new System.Windows.Forms.GroupBox();
			textBoxPayslipBody = new Micromind.UISupport.MMTextBox();
			mmLabel42 = new Micromind.UISupport.MMLabel();
			textBoxCCPayslip = new Micromind.UISupport.MMTextBox();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			groupBox1 = new System.Windows.Forms.GroupBox();
			textBoxStatementBody = new Micromind.UISupport.MMTextBox();
			mmLabel40 = new Micromind.UISupport.MMLabel();
			textBoxCCStatement = new Micromind.UISupport.MMTextBox();
			checkBoxCCSalesperson = new System.Windows.Forms.CheckBox();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			mmLabel30 = new Micromind.UISupport.MMLabel();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			textBoxE1Name = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxE1Address = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxE1Port = new Micromind.UISupport.MMTextBox();
			textBoxE1OutServer = new Micromind.UISupport.MMTextBox();
			mmLabel28 = new Micromind.UISupport.MMLabel();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxE1InServer = new Micromind.UISupport.MMTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxE1UserName = new Micromind.UISupport.MMTextBox();
			checkBoxE1SSL = new System.Windows.Forms.CheckBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxE1Password = new Micromind.UISupport.MMTextBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel43 = new Micromind.UISupport.MMLabel();
			textBoxCCPurchase = new Micromind.UISupport.MMTextBox();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxE2Name = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxE2Port = new Micromind.UISupport.MMTextBox();
			textBoxE2Address = new Micromind.UISupport.MMTextBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxE2OutServer = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			checkBoxE2SSL = new System.Windows.Forms.CheckBox();
			textBoxE2UserName = new Micromind.UISupport.MMTextBox();
			textBoxE2InServer = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			textBoxE2Password = new Micromind.UISupport.MMTextBox();
			buttonCopyE2 = new System.Windows.Forms.Button();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel44 = new Micromind.UISupport.MMLabel();
			textBoxCCSales = new Micromind.UISupport.MMTextBox();
			mmLabel27 = new Micromind.UISupport.MMLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxE3Name = new Micromind.UISupport.MMTextBox();
			textBoxE3Port = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			textBoxE3Address = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			checkBoxE3SSL = new System.Windows.Forms.CheckBox();
			textBoxE3OutServer = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxE3UserName = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxE3Password = new Micromind.UISupport.MMTextBox();
			buttonCopyE3 = new System.Windows.Forms.Button();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			textBoxE3InServer = new Micromind.UISupport.MMTextBox();
			mmLabel26 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel45 = new Micromind.UISupport.MMLabel();
			textBoxCCNotification = new Micromind.UISupport.MMTextBox();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			textBoxE4Port = new Micromind.UISupport.MMTextBox();
			buttonCopyE4 = new System.Windows.Forms.Button();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			checkBoxE4SSL = new System.Windows.Forms.CheckBox();
			textBoxE4Name = new Micromind.UISupport.MMTextBox();
			mmLabel37 = new Micromind.UISupport.MMLabel();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			textBoxE4Address = new Micromind.UISupport.MMTextBox();
			textBoxE4InServer = new Micromind.UISupport.MMTextBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxE4OutServer = new Micromind.UISupport.MMTextBox();
			textBoxE4Password = new Micromind.UISupport.MMTextBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			textBoxE4UserName = new Micromind.UISupport.MMTextBox();
			ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			tabPageGeneral.SuspendLayout();
			groupBox3.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox1.SuspendLayout();
			tabPageDetails.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(groupBox3);
			tabPageGeneral.Controls.Add(groupBox2);
			tabPageGeneral.Controls.Add(groupBox1);
			tabPageGeneral.Controls.Add(mmLabel30);
			tabPageGeneral.Controls.Add(mmLabel36);
			tabPageGeneral.Controls.Add(mmLabel29);
			tabPageGeneral.Controls.Add(textBoxE1Name);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(textBoxE1Address);
			tabPageGeneral.Controls.Add(mmLabel3);
			tabPageGeneral.Controls.Add(textBoxE1Port);
			tabPageGeneral.Controls.Add(textBoxE1OutServer);
			tabPageGeneral.Controls.Add(mmLabel28);
			tabPageGeneral.Controls.Add(mmLabel17);
			tabPageGeneral.Controls.Add(textBoxE1InServer);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(textBoxE1UserName);
			tabPageGeneral.Controls.Add(checkBoxE1SSL);
			tabPageGeneral.Controls.Add(mmLabel4);
			tabPageGeneral.Controls.Add(textBoxE1Password);
			tabPageGeneral.Location = new System.Drawing.Point(-10000, -10000);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(974, 592);
			groupBox3.Controls.Add(textBoxAccountBody);
			groupBox3.Controls.Add(mmLabel41);
			groupBox3.Controls.Add(textBoxCCAccGeneral);
			groupBox3.Controls.Add(mmLabel39);
			groupBox3.Location = new System.Drawing.Point(493, 156);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(468, 204);
			groupBox3.TabIndex = 9;
			groupBox3.TabStop = false;
			groupBox3.Text = "General";
			textBoxAccountBody.AcceptsReturn = true;
			textBoxAccountBody.BackColor = System.Drawing.Color.White;
			textBoxAccountBody.CustomReportFieldName = "";
			textBoxAccountBody.CustomReportKey = "";
			textBoxAccountBody.CustomReportValueType = 1;
			textBoxAccountBody.IsComboTextBox = false;
			textBoxAccountBody.IsRequired = true;
			textBoxAccountBody.Location = new System.Drawing.Point(20, 63);
			textBoxAccountBody.MaxLength = 5000;
			textBoxAccountBody.Multiline = true;
			textBoxAccountBody.Name = "textBoxAccountBody";
			textBoxAccountBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxAccountBody.Size = new System.Drawing.Size(437, 124);
			textBoxAccountBody.TabIndex = 1;
			textBoxAccountBody.TextChanged += new System.EventHandler(textBoxAccountBody_TextChanged);
			mmLabel41.AutoSize = true;
			mmLabel41.BackColor = System.Drawing.Color.Transparent;
			mmLabel41.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel41.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel41.IsFieldHeader = false;
			mmLabel41.IsRequired = false;
			mmLabel41.Location = new System.Drawing.Point(23, 22);
			mmLabel41.Name = "mmLabel41";
			mmLabel41.PenWidth = 1f;
			mmLabel41.ShowBorder = false;
			mmLabel41.Size = new System.Drawing.Size(25, 13);
			mmLabel41.TabIndex = 113;
			mmLabel41.Text = "CC:";
			mmLabel41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxCCAccGeneral.BackColor = System.Drawing.Color.White;
			textBoxCCAccGeneral.CustomReportFieldName = "";
			textBoxCCAccGeneral.CustomReportKey = "";
			textBoxCCAccGeneral.CustomReportValueType = 1;
			textBoxCCAccGeneral.IsComboTextBox = false;
			textBoxCCAccGeneral.IsRequired = true;
			textBoxCCAccGeneral.Location = new System.Drawing.Point(51, 19);
			textBoxCCAccGeneral.MaxLength = 255;
			textBoxCCAccGeneral.Name = "textBoxCCAccGeneral";
			textBoxCCAccGeneral.Size = new System.Drawing.Size(159, 20);
			textBoxCCAccGeneral.TabIndex = 0;
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(17, 47);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(62, 13);
			mmLabel39.TabIndex = 108;
			mmLabel39.Text = "Email Body:";
			mmLabel39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox2.Controls.Add(textBoxPayslipBody);
			groupBox2.Controls.Add(mmLabel42);
			groupBox2.Controls.Add(textBoxCCPayslip);
			groupBox2.Controls.Add(mmLabel35);
			groupBox2.Location = new System.Drawing.Point(19, 371);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(464, 202);
			groupBox2.TabIndex = 10;
			groupBox2.TabStop = false;
			groupBox2.Text = "Payslip Email Settings";
			textBoxPayslipBody.AcceptsReturn = true;
			textBoxPayslipBody.BackColor = System.Drawing.Color.White;
			textBoxPayslipBody.CustomReportFieldName = "";
			textBoxPayslipBody.CustomReportKey = "";
			textBoxPayslipBody.CustomReportValueType = 1;
			textBoxPayslipBody.IsComboTextBox = false;
			textBoxPayslipBody.IsRequired = true;
			textBoxPayslipBody.Location = new System.Drawing.Point(14, 63);
			textBoxPayslipBody.MaxLength = 5000;
			textBoxPayslipBody.Multiline = true;
			textBoxPayslipBody.Name = "textBoxPayslipBody";
			textBoxPayslipBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxPayslipBody.Size = new System.Drawing.Size(437, 124);
			textBoxPayslipBody.TabIndex = 1;
			mmLabel42.AutoSize = true;
			mmLabel42.BackColor = System.Drawing.Color.Transparent;
			mmLabel42.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel42.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel42.IsFieldHeader = false;
			mmLabel42.IsRequired = false;
			mmLabel42.Location = new System.Drawing.Point(16, 22);
			mmLabel42.Name = "mmLabel42";
			mmLabel42.PenWidth = 1f;
			mmLabel42.ShowBorder = false;
			mmLabel42.Size = new System.Drawing.Size(25, 13);
			mmLabel42.TabIndex = 115;
			mmLabel42.Text = "CC:";
			mmLabel42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxCCPayslip.BackColor = System.Drawing.Color.White;
			textBoxCCPayslip.CustomReportFieldName = "";
			textBoxCCPayslip.CustomReportKey = "";
			textBoxCCPayslip.CustomReportValueType = 1;
			textBoxCCPayslip.IsComboTextBox = false;
			textBoxCCPayslip.IsRequired = true;
			textBoxCCPayslip.Location = new System.Drawing.Point(44, 19);
			textBoxCCPayslip.MaxLength = 255;
			textBoxCCPayslip.Name = "textBoxCCPayslip";
			textBoxCCPayslip.Size = new System.Drawing.Size(159, 20);
			textBoxCCPayslip.TabIndex = 0;
			mmLabel35.AutoSize = true;
			mmLabel35.BackColor = System.Drawing.Color.Transparent;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(16, 47);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(62, 13);
			mmLabel35.TabIndex = 108;
			mmLabel35.Text = "Email Body:";
			mmLabel35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel35.Click += new System.EventHandler(mmLabel35_Click);
			groupBox1.Controls.Add(textBoxStatementBody);
			groupBox1.Controls.Add(mmLabel40);
			groupBox1.Controls.Add(textBoxCCStatement);
			groupBox1.Controls.Add(checkBoxCCSalesperson);
			groupBox1.Controls.Add(mmLabel34);
			groupBox1.Location = new System.Drawing.Point(19, 156);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(464, 204);
			groupBox1.TabIndex = 8;
			groupBox1.TabStop = false;
			groupBox1.Text = "Statement Email Settings";
			textBoxStatementBody.AcceptsReturn = true;
			textBoxStatementBody.BackColor = System.Drawing.Color.White;
			textBoxStatementBody.CustomReportFieldName = "";
			textBoxStatementBody.CustomReportKey = "";
			textBoxStatementBody.CustomReportValueType = 1;
			textBoxStatementBody.IsComboTextBox = false;
			textBoxStatementBody.IsRequired = true;
			textBoxStatementBody.Location = new System.Drawing.Point(14, 64);
			textBoxStatementBody.MaxLength = 5000;
			textBoxStatementBody.Multiline = true;
			textBoxStatementBody.Name = "textBoxStatementBody";
			textBoxStatementBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxStatementBody.Size = new System.Drawing.Size(437, 124);
			textBoxStatementBody.TabIndex = 2;
			mmLabel40.AutoSize = true;
			mmLabel40.BackColor = System.Drawing.Color.Transparent;
			mmLabel40.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel40.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel40.IsFieldHeader = false;
			mmLabel40.IsRequired = false;
			mmLabel40.Location = new System.Drawing.Point(16, 21);
			mmLabel40.Name = "mmLabel40";
			mmLabel40.PenWidth = 1f;
			mmLabel40.ShowBorder = false;
			mmLabel40.Size = new System.Drawing.Size(25, 13);
			mmLabel40.TabIndex = 111;
			mmLabel40.Text = "CC:";
			mmLabel40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxCCStatement.BackColor = System.Drawing.Color.White;
			textBoxCCStatement.CustomReportFieldName = "";
			textBoxCCStatement.CustomReportKey = "";
			textBoxCCStatement.CustomReportValueType = 1;
			textBoxCCStatement.IsComboTextBox = false;
			textBoxCCStatement.IsRequired = true;
			textBoxCCStatement.Location = new System.Drawing.Point(44, 18);
			textBoxCCStatement.MaxLength = 255;
			textBoxCCStatement.Name = "textBoxCCStatement";
			textBoxCCStatement.Size = new System.Drawing.Size(159, 20);
			textBoxCCStatement.TabIndex = 0;
			checkBoxCCSalesperson.AutoSize = true;
			checkBoxCCSalesperson.Location = new System.Drawing.Point(228, 20);
			checkBoxCCSalesperson.Name = "checkBoxCCSalesperson";
			checkBoxCCSalesperson.Size = new System.Drawing.Size(144, 17);
			checkBoxCCSalesperson.TabIndex = 1;
			checkBoxCCSalesperson.Text = "CC assigned salesperson";
			checkBoxCCSalesperson.UseVisualStyleBackColor = true;
			mmLabel34.AutoSize = true;
			mmLabel34.BackColor = System.Drawing.Color.Transparent;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(17, 48);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(62, 13);
			mmLabel34.TabIndex = 108;
			mmLabel34.Text = "Email Body:";
			mmLabel34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel30.AutoSize = true;
			mmLabel30.BackColor = System.Drawing.Color.Transparent;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(16, 14);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(128, 13);
			mmLabel30.TabIndex = 17;
			mmLabel30.Text = "Accounting && Finance";
			mmLabel30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel36.AutoSize = true;
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(16, 33);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(316, 13);
			mmLabel36.TabIndex = 19;
			mmLabel36.Text = "Accounts related emails, like statement of accounts and reports.";
			mmLabel36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(30, 52);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(75, 13);
			mmLabel29.TabIndex = 0;
			mmLabel29.Text = "Sender Name:";
			mmLabel29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE1Name.BackColor = System.Drawing.Color.White;
			textBoxE1Name.CustomReportFieldName = "";
			textBoxE1Name.CustomReportKey = "";
			textBoxE1Name.CustomReportValueType = 1;
			textBoxE1Name.IsComboTextBox = false;
			textBoxE1Name.IsRequired = true;
			textBoxE1Name.Location = new System.Drawing.Point(128, 50);
			textBoxE1Name.MaxLength = 64;
			textBoxE1Name.Name = "textBoxE1Name";
			textBoxE1Name.Size = new System.Drawing.Size(178, 20);
			textBoxE1Name.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(30, 74);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(77, 13);
			mmLabel1.TabIndex = 23;
			mmLabel1.Text = "Email Address:";
			mmLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE1Address.BackColor = System.Drawing.Color.White;
			textBoxE1Address.CustomReportFieldName = "";
			textBoxE1Address.CustomReportKey = "";
			textBoxE1Address.CustomReportValueType = 1;
			textBoxE1Address.IsComboTextBox = false;
			textBoxE1Address.IsRequired = true;
			textBoxE1Address.Location = new System.Drawing.Point(128, 72);
			textBoxE1Address.MaxLength = 64;
			textBoxE1Address.Name = "textBoxE1Address";
			textBoxE1Address.Size = new System.Drawing.Size(178, 20);
			textBoxE1Address.TabIndex = 1;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(619, 72);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(110, 13);
			mmLabel3.TabIndex = 25;
			mmLabel3.Text = "Outgoing mail server:";
			mmLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE1Port.BackColor = System.Drawing.Color.White;
			textBoxE1Port.CustomReportFieldName = "";
			textBoxE1Port.CustomReportKey = "";
			textBoxE1Port.CustomReportValueType = 1;
			textBoxE1Port.IsComboTextBox = false;
			textBoxE1Port.IsRequired = true;
			textBoxE1Port.Location = new System.Drawing.Point(655, 101);
			textBoxE1Port.MaxLength = 64;
			textBoxE1Port.Name = "textBoxE1Port";
			textBoxE1Port.Size = new System.Drawing.Size(65, 20);
			textBoxE1Port.TabIndex = 6;
			textBoxE1Port.Text = "25";
			textBoxE1OutServer.BackColor = System.Drawing.Color.White;
			textBoxE1OutServer.CustomReportFieldName = "";
			textBoxE1OutServer.CustomReportKey = "";
			textBoxE1OutServer.CustomReportValueType = 1;
			textBoxE1OutServer.IsComboTextBox = false;
			textBoxE1OutServer.IsRequired = true;
			textBoxE1OutServer.Location = new System.Drawing.Point(763, 71);
			textBoxE1OutServer.MaxLength = 64;
			textBoxE1OutServer.Name = "textBoxE1OutServer";
			textBoxE1OutServer.Size = new System.Drawing.Size(178, 20);
			textBoxE1OutServer.TabIndex = 5;
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(618, 104);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(31, 13);
			mmLabel28.TabIndex = 106;
			mmLabel28.Text = "Port:";
			mmLabel28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(619, 50);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(117, 13);
			mmLabel17.TabIndex = 25;
			mmLabel17.Text = "Incomming mail server:";
			mmLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE1InServer.BackColor = System.Drawing.Color.White;
			textBoxE1InServer.CustomReportFieldName = "";
			textBoxE1InServer.CustomReportKey = "";
			textBoxE1InServer.CustomReportValueType = 1;
			textBoxE1InServer.IsComboTextBox = false;
			textBoxE1InServer.IsRequired = true;
			textBoxE1InServer.Location = new System.Drawing.Point(763, 48);
			textBoxE1InServer.MaxLength = 64;
			textBoxE1InServer.Name = "textBoxE1InServer";
			textBoxE1InServer.Size = new System.Drawing.Size(178, 20);
			textBoxE1InServer.TabIndex = 4;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(315, 51);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(63, 13);
			mmLabel5.TabIndex = 27;
			mmLabel5.Text = "User Name:";
			mmLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE1UserName.BackColor = System.Drawing.Color.White;
			textBoxE1UserName.CustomReportFieldName = "";
			textBoxE1UserName.CustomReportKey = "";
			textBoxE1UserName.CustomReportValueType = 1;
			textBoxE1UserName.IsComboTextBox = false;
			textBoxE1UserName.IsRequired = true;
			textBoxE1UserName.Location = new System.Drawing.Point(416, 50);
			textBoxE1UserName.MaxLength = 64;
			textBoxE1UserName.Name = "textBoxE1UserName";
			textBoxE1UserName.Size = new System.Drawing.Size(178, 20);
			textBoxE1UserName.TabIndex = 2;
			checkBoxE1SSL.AutoSize = true;
			checkBoxE1SSL.Location = new System.Drawing.Point(744, 103);
			checkBoxE1SSL.Name = "checkBoxE1SSL";
			checkBoxE1SSL.Size = new System.Drawing.Size(82, 17);
			checkBoxE1SSL.TabIndex = 7;
			checkBoxE1SSL.Text = "Enable SSL";
			checkBoxE1SSL.UseVisualStyleBackColor = true;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(315, 73);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(57, 13);
			mmLabel4.TabIndex = 29;
			mmLabel4.Text = "Password:";
			mmLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE1Password.BackColor = System.Drawing.Color.White;
			textBoxE1Password.CustomReportFieldName = "";
			textBoxE1Password.CustomReportKey = "";
			textBoxE1Password.CustomReportValueType = 1;
			textBoxE1Password.IsComboTextBox = false;
			textBoxE1Password.IsRequired = true;
			textBoxE1Password.Location = new System.Drawing.Point(416, 72);
			textBoxE1Password.MaxLength = 32;
			textBoxE1Password.Name = "textBoxE1Password";
			textBoxE1Password.Size = new System.Drawing.Size(178, 20);
			textBoxE1Password.TabIndex = 3;
			textBoxE1Password.UseSystemPasswordChar = true;
			tabPageDetails.Controls.Add(mmLabel43);
			tabPageDetails.Controls.Add(textBoxCCPurchase);
			tabPageDetails.Controls.Add(mmLabel16);
			tabPageDetails.Controls.Add(mmLabel15);
			tabPageDetails.Controls.Add(mmLabel9);
			tabPageDetails.Controls.Add(textBoxE2Name);
			tabPageDetails.Controls.Add(mmLabel8);
			tabPageDetails.Controls.Add(textBoxE2Port);
			tabPageDetails.Controls.Add(textBoxE2Address);
			tabPageDetails.Controls.Add(mmLabel31);
			tabPageDetails.Controls.Add(mmLabel7);
			tabPageDetails.Controls.Add(textBoxE2OutServer);
			tabPageDetails.Controls.Add(mmLabel6);
			tabPageDetails.Controls.Add(checkBoxE2SSL);
			tabPageDetails.Controls.Add(textBoxE2UserName);
			tabPageDetails.Controls.Add(textBoxE2InServer);
			tabPageDetails.Controls.Add(mmLabel2);
			tabPageDetails.Controls.Add(mmLabel19);
			tabPageDetails.Controls.Add(textBoxE2Password);
			tabPageDetails.Controls.Add(buttonCopyE2);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(974, 592);
			mmLabel43.AutoSize = true;
			mmLabel43.BackColor = System.Drawing.Color.Transparent;
			mmLabel43.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel43.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel43.IsFieldHeader = false;
			mmLabel43.IsRequired = false;
			mmLabel43.Location = new System.Drawing.Point(29, 117);
			mmLabel43.Name = "mmLabel43";
			mmLabel43.PenWidth = 1f;
			mmLabel43.ShowBorder = false;
			mmLabel43.Size = new System.Drawing.Size(25, 13);
			mmLabel43.TabIndex = 113;
			mmLabel43.Text = "CC:";
			mmLabel43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxCCPurchase.BackColor = System.Drawing.Color.White;
			textBoxCCPurchase.CustomReportFieldName = "";
			textBoxCCPurchase.CustomReportKey = "";
			textBoxCCPurchase.CustomReportValueType = 1;
			textBoxCCPurchase.IsComboTextBox = false;
			textBoxCCPurchase.IsRequired = true;
			textBoxCCPurchase.Location = new System.Drawing.Point(127, 114);
			textBoxCCPurchase.MaxLength = 64;
			textBoxCCPurchase.Name = "textBoxCCPurchase";
			textBoxCCPurchase.Size = new System.Drawing.Size(178, 20);
			textBoxCCPurchase.TabIndex = 112;
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(14, 15);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(130, 13);
			mmLabel16.TabIndex = 47;
			mmLabel16.Text = "Vendors && Purchasing";
			mmLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(14, 32);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(243, 13);
			mmLabel15.TabIndex = 48;
			mmLabel15.Text = "Purchase orders and emails related to purchasing";
			mmLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(29, 54);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(75, 13);
			mmLabel9.TabIndex = 10;
			mmLabel9.Text = "Sender Name:";
			mmLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE2Name.BackColor = System.Drawing.Color.White;
			textBoxE2Name.CustomReportFieldName = "";
			textBoxE2Name.CustomReportKey = "";
			textBoxE2Name.CustomReportValueType = 1;
			textBoxE2Name.IsComboTextBox = false;
			textBoxE2Name.IsRequired = true;
			textBoxE2Name.Location = new System.Drawing.Point(127, 51);
			textBoxE2Name.MaxLength = 64;
			textBoxE2Name.Name = "textBoxE2Name";
			textBoxE2Name.Size = new System.Drawing.Size(178, 20);
			textBoxE2Name.TabIndex = 11;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(29, 76);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(77, 13);
			mmLabel8.TabIndex = 95;
			mmLabel8.Text = "Email Address:";
			mmLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE2Port.BackColor = System.Drawing.Color.White;
			textBoxE2Port.CustomReportFieldName = "";
			textBoxE2Port.CustomReportKey = "";
			textBoxE2Port.CustomReportValueType = 1;
			textBoxE2Port.IsComboTextBox = false;
			textBoxE2Port.IsRequired = true;
			textBoxE2Port.Location = new System.Drawing.Point(674, 96);
			textBoxE2Port.MaxLength = 64;
			textBoxE2Port.Name = "textBoxE2Port";
			textBoxE2Port.Size = new System.Drawing.Size(65, 20);
			textBoxE2Port.TabIndex = 17;
			textBoxE2Port.Text = "25";
			textBoxE2Address.BackColor = System.Drawing.Color.White;
			textBoxE2Address.CustomReportFieldName = "";
			textBoxE2Address.CustomReportKey = "";
			textBoxE2Address.CustomReportValueType = 1;
			textBoxE2Address.IsComboTextBox = false;
			textBoxE2Address.IsRequired = true;
			textBoxE2Address.Location = new System.Drawing.Point(127, 74);
			textBoxE2Address.MaxLength = 64;
			textBoxE2Address.Name = "textBoxE2Address";
			textBoxE2Address.Size = new System.Drawing.Size(178, 20);
			textBoxE2Address.TabIndex = 12;
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(637, 100);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(31, 13);
			mmLabel31.TabIndex = 108;
			mmLabel31.Text = "Port:";
			mmLabel31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(637, 72);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(110, 13);
			mmLabel7.TabIndex = 96;
			mmLabel7.Text = "Outgoing mail server:";
			mmLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE2OutServer.BackColor = System.Drawing.Color.White;
			textBoxE2OutServer.CustomReportFieldName = "";
			textBoxE2OutServer.CustomReportKey = "";
			textBoxE2OutServer.CustomReportValueType = 1;
			textBoxE2OutServer.IsComboTextBox = false;
			textBoxE2OutServer.IsRequired = true;
			textBoxE2OutServer.Location = new System.Drawing.Point(763, 71);
			textBoxE2OutServer.MaxLength = 64;
			textBoxE2OutServer.Name = "textBoxE2OutServer";
			textBoxE2OutServer.Size = new System.Drawing.Size(178, 20);
			textBoxE2OutServer.TabIndex = 16;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(321, 51);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(63, 13);
			mmLabel6.TabIndex = 97;
			mmLabel6.Text = "User Name:";
			mmLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxE2SSL.AutoSize = true;
			checkBoxE2SSL.Location = new System.Drawing.Point(763, 97);
			checkBoxE2SSL.Name = "checkBoxE2SSL";
			checkBoxE2SSL.Size = new System.Drawing.Size(82, 17);
			checkBoxE2SSL.TabIndex = 18;
			checkBoxE2SSL.Text = "Enable SSL";
			checkBoxE2SSL.UseVisualStyleBackColor = true;
			textBoxE2UserName.BackColor = System.Drawing.Color.White;
			textBoxE2UserName.CustomReportFieldName = "";
			textBoxE2UserName.CustomReportKey = "";
			textBoxE2UserName.CustomReportValueType = 1;
			textBoxE2UserName.IsComboTextBox = false;
			textBoxE2UserName.IsRequired = true;
			textBoxE2UserName.Location = new System.Drawing.Point(419, 49);
			textBoxE2UserName.MaxLength = 64;
			textBoxE2UserName.Name = "textBoxE2UserName";
			textBoxE2UserName.Size = new System.Drawing.Size(178, 20);
			textBoxE2UserName.TabIndex = 13;
			textBoxE2InServer.BackColor = System.Drawing.Color.White;
			textBoxE2InServer.CustomReportFieldName = "";
			textBoxE2InServer.CustomReportKey = "";
			textBoxE2InServer.CustomReportValueType = 1;
			textBoxE2InServer.IsComboTextBox = false;
			textBoxE2InServer.IsRequired = true;
			textBoxE2InServer.Location = new System.Drawing.Point(763, 48);
			textBoxE2InServer.MaxLength = 64;
			textBoxE2InServer.Name = "textBoxE2InServer";
			textBoxE2InServer.Size = new System.Drawing.Size(178, 20);
			textBoxE2InServer.TabIndex = 15;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(321, 74);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(57, 13);
			mmLabel2.TabIndex = 98;
			mmLabel2.Text = "Password:";
			mmLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(637, 51);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(117, 13);
			mmLabel19.TabIndex = 100;
			mmLabel19.Text = "Incomming mail server:";
			mmLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE2Password.BackColor = System.Drawing.Color.White;
			textBoxE2Password.CustomReportFieldName = "";
			textBoxE2Password.CustomReportKey = "";
			textBoxE2Password.CustomReportValueType = 1;
			textBoxE2Password.IsComboTextBox = false;
			textBoxE2Password.IsRequired = true;
			textBoxE2Password.Location = new System.Drawing.Point(419, 72);
			textBoxE2Password.MaxLength = 32;
			textBoxE2Password.Name = "textBoxE2Password";
			textBoxE2Password.Size = new System.Drawing.Size(178, 20);
			textBoxE2Password.TabIndex = 14;
			textBoxE2Password.UseSystemPasswordChar = true;
			buttonCopyE2.Location = new System.Drawing.Point(866, 21);
			buttonCopyE2.Name = "buttonCopyE2";
			buttonCopyE2.Size = new System.Drawing.Size(75, 23);
			buttonCopyE2.TabIndex = 9;
			buttonCopyE2.Text = "Copy ";
			buttonCopyE2.UseVisualStyleBackColor = true;
			buttonCopyE2.Click += new System.EventHandler(buttonCopyE2_Click);
			ultraTabPageControl1.Controls.Add(mmLabel44);
			ultraTabPageControl1.Controls.Add(textBoxCCSales);
			ultraTabPageControl1.Controls.Add(mmLabel27);
			ultraTabPageControl1.Controls.Add(mmLabel14);
			ultraTabPageControl1.Controls.Add(textBoxE3Name);
			ultraTabPageControl1.Controls.Add(textBoxE3Port);
			ultraTabPageControl1.Controls.Add(mmLabel13);
			ultraTabPageControl1.Controls.Add(mmLabel32);
			ultraTabPageControl1.Controls.Add(textBoxE3Address);
			ultraTabPageControl1.Controls.Add(mmLabel12);
			ultraTabPageControl1.Controls.Add(checkBoxE3SSL);
			ultraTabPageControl1.Controls.Add(textBoxE3OutServer);
			ultraTabPageControl1.Controls.Add(mmLabel11);
			ultraTabPageControl1.Controls.Add(textBoxE3UserName);
			ultraTabPageControl1.Controls.Add(mmLabel10);
			ultraTabPageControl1.Controls.Add(textBoxE3Password);
			ultraTabPageControl1.Controls.Add(buttonCopyE3);
			ultraTabPageControl1.Controls.Add(mmLabel18);
			ultraTabPageControl1.Controls.Add(textBoxE3InServer);
			ultraTabPageControl1.Controls.Add(mmLabel26);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(974, 592);
			mmLabel44.AutoSize = true;
			mmLabel44.BackColor = System.Drawing.Color.Transparent;
			mmLabel44.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel44.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel44.IsFieldHeader = false;
			mmLabel44.IsRequired = false;
			mmLabel44.Location = new System.Drawing.Point(29, 117);
			mmLabel44.Name = "mmLabel44";
			mmLabel44.PenWidth = 1f;
			mmLabel44.ShowBorder = false;
			mmLabel44.Size = new System.Drawing.Size(25, 13);
			mmLabel44.TabIndex = 113;
			mmLabel44.Text = "CC:";
			mmLabel44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxCCSales.BackColor = System.Drawing.Color.White;
			textBoxCCSales.CustomReportFieldName = "";
			textBoxCCSales.CustomReportKey = "";
			textBoxCCSales.CustomReportValueType = 1;
			textBoxCCSales.IsComboTextBox = false;
			textBoxCCSales.IsRequired = true;
			textBoxCCSales.Location = new System.Drawing.Point(127, 114);
			textBoxCCSales.MaxLength = 64;
			textBoxCCSales.Name = "textBoxCCSales";
			textBoxCCSales.Size = new System.Drawing.Size(178, 20);
			textBoxCCSales.TabIndex = 112;
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(15, 16);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(77, 13);
			mmLabel27.TabIndex = 64;
			mmLabel27.Text = "Sales && CRM";
			mmLabel27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(29, 54);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(75, 13);
			mmLabel14.TabIndex = 20;
			mmLabel14.Text = "Sender Name:";
			mmLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE3Name.BackColor = System.Drawing.Color.White;
			textBoxE3Name.CustomReportFieldName = "";
			textBoxE3Name.CustomReportKey = "";
			textBoxE3Name.CustomReportValueType = 1;
			textBoxE3Name.IsComboTextBox = false;
			textBoxE3Name.IsRequired = true;
			textBoxE3Name.Location = new System.Drawing.Point(127, 51);
			textBoxE3Name.MaxLength = 64;
			textBoxE3Name.Name = "textBoxE3Name";
			textBoxE3Name.Size = new System.Drawing.Size(178, 20);
			textBoxE3Name.TabIndex = 21;
			textBoxE3Port.BackColor = System.Drawing.Color.White;
			textBoxE3Port.CustomReportFieldName = "";
			textBoxE3Port.CustomReportKey = "";
			textBoxE3Port.CustomReportValueType = 1;
			textBoxE3Port.IsComboTextBox = false;
			textBoxE3Port.IsRequired = true;
			textBoxE3Port.Location = new System.Drawing.Point(674, 96);
			textBoxE3Port.MaxLength = 64;
			textBoxE3Port.Name = "textBoxE3Port";
			textBoxE3Port.Size = new System.Drawing.Size(65, 20);
			textBoxE3Port.TabIndex = 27;
			textBoxE3Port.Text = "25";
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(29, 76);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(77, 13);
			mmLabel13.TabIndex = 50;
			mmLabel13.Text = "Email Address:";
			mmLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(637, 100);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(31, 13);
			mmLabel32.TabIndex = 110;
			mmLabel32.Text = "Port:";
			mmLabel32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE3Address.BackColor = System.Drawing.Color.White;
			textBoxE3Address.CustomReportFieldName = "";
			textBoxE3Address.CustomReportKey = "";
			textBoxE3Address.CustomReportValueType = 1;
			textBoxE3Address.IsComboTextBox = false;
			textBoxE3Address.IsRequired = true;
			textBoxE3Address.Location = new System.Drawing.Point(127, 74);
			textBoxE3Address.MaxLength = 64;
			textBoxE3Address.Name = "textBoxE3Address";
			textBoxE3Address.Size = new System.Drawing.Size(178, 20);
			textBoxE3Address.TabIndex = 22;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(637, 72);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(110, 13);
			mmLabel12.TabIndex = 51;
			mmLabel12.Text = "Outgoing mail server:";
			mmLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxE3SSL.AutoSize = true;
			checkBoxE3SSL.Location = new System.Drawing.Point(763, 97);
			checkBoxE3SSL.Name = "checkBoxE3SSL";
			checkBoxE3SSL.Size = new System.Drawing.Size(82, 17);
			checkBoxE3SSL.TabIndex = 28;
			checkBoxE3SSL.Text = "Enable SSL";
			checkBoxE3SSL.UseVisualStyleBackColor = true;
			textBoxE3OutServer.BackColor = System.Drawing.Color.White;
			textBoxE3OutServer.CustomReportFieldName = "";
			textBoxE3OutServer.CustomReportKey = "";
			textBoxE3OutServer.CustomReportValueType = 1;
			textBoxE3OutServer.IsComboTextBox = false;
			textBoxE3OutServer.IsRequired = true;
			textBoxE3OutServer.Location = new System.Drawing.Point(763, 71);
			textBoxE3OutServer.MaxLength = 64;
			textBoxE3OutServer.Name = "textBoxE3OutServer";
			textBoxE3OutServer.Size = new System.Drawing.Size(177, 20);
			textBoxE3OutServer.TabIndex = 26;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(321, 51);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(63, 13);
			mmLabel11.TabIndex = 52;
			mmLabel11.Text = "User Name:";
			mmLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE3UserName.BackColor = System.Drawing.Color.White;
			textBoxE3UserName.CustomReportFieldName = "";
			textBoxE3UserName.CustomReportKey = "";
			textBoxE3UserName.CustomReportValueType = 1;
			textBoxE3UserName.IsComboTextBox = false;
			textBoxE3UserName.IsRequired = true;
			textBoxE3UserName.Location = new System.Drawing.Point(419, 49);
			textBoxE3UserName.MaxLength = 64;
			textBoxE3UserName.Name = "textBoxE3UserName";
			textBoxE3UserName.Size = new System.Drawing.Size(178, 20);
			textBoxE3UserName.TabIndex = 23;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(321, 74);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(57, 13);
			mmLabel10.TabIndex = 53;
			mmLabel10.Text = "Password:";
			mmLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE3Password.BackColor = System.Drawing.Color.White;
			textBoxE3Password.CustomReportFieldName = "";
			textBoxE3Password.CustomReportKey = "";
			textBoxE3Password.CustomReportValueType = 1;
			textBoxE3Password.IsComboTextBox = false;
			textBoxE3Password.IsRequired = true;
			textBoxE3Password.Location = new System.Drawing.Point(419, 72);
			textBoxE3Password.MaxLength = 32;
			textBoxE3Password.Name = "textBoxE3Password";
			textBoxE3Password.Size = new System.Drawing.Size(178, 20);
			textBoxE3Password.TabIndex = 24;
			textBoxE3Password.UseSystemPasswordChar = true;
			buttonCopyE3.Location = new System.Drawing.Point(866, 21);
			buttonCopyE3.Name = "buttonCopyE3";
			buttonCopyE3.Size = new System.Drawing.Size(75, 23);
			buttonCopyE3.TabIndex = 19;
			buttonCopyE3.Text = "Copy ";
			buttonCopyE3.UseVisualStyleBackColor = true;
			buttonCopyE3.Click += new System.EventHandler(buttonCopyE3_Click);
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(637, 51);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(117, 13);
			mmLabel18.TabIndex = 55;
			mmLabel18.Text = "Incomming mail server:";
			mmLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE3InServer.BackColor = System.Drawing.Color.White;
			textBoxE3InServer.CustomReportFieldName = "";
			textBoxE3InServer.CustomReportKey = "";
			textBoxE3InServer.CustomReportValueType = 1;
			textBoxE3InServer.IsComboTextBox = false;
			textBoxE3InServer.IsRequired = true;
			textBoxE3InServer.Location = new System.Drawing.Point(763, 48);
			textBoxE3InServer.MaxLength = 64;
			textBoxE3InServer.Name = "textBoxE3InServer";
			textBoxE3InServer.Size = new System.Drawing.Size(177, 20);
			textBoxE3InServer.TabIndex = 25;
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(15, 32);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(244, 13);
			mmLabel26.TabIndex = 65;
			mmLabel26.Text = "Emails related to sales and customer relationships";
			mmLabel26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ultraTabPageControl2.Controls.Add(mmLabel45);
			ultraTabPageControl2.Controls.Add(textBoxCCNotification);
			ultraTabPageControl2.Controls.Add(mmLabel38);
			ultraTabPageControl2.Controls.Add(textBoxE4Port);
			ultraTabPageControl2.Controls.Add(buttonCopyE4);
			ultraTabPageControl2.Controls.Add(mmLabel33);
			ultraTabPageControl2.Controls.Add(mmLabel25);
			ultraTabPageControl2.Controls.Add(checkBoxE4SSL);
			ultraTabPageControl2.Controls.Add(textBoxE4Name);
			ultraTabPageControl2.Controls.Add(mmLabel37);
			ultraTabPageControl2.Controls.Add(mmLabel24);
			ultraTabPageControl2.Controls.Add(textBoxE4Address);
			ultraTabPageControl2.Controls.Add(textBoxE4InServer);
			ultraTabPageControl2.Controls.Add(mmLabel23);
			ultraTabPageControl2.Controls.Add(mmLabel20);
			ultraTabPageControl2.Controls.Add(textBoxE4OutServer);
			ultraTabPageControl2.Controls.Add(textBoxE4Password);
			ultraTabPageControl2.Controls.Add(mmLabel22);
			ultraTabPageControl2.Controls.Add(mmLabel21);
			ultraTabPageControl2.Controls.Add(textBoxE4UserName);
			ultraTabPageControl2.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(974, 592);
			mmLabel45.AutoSize = true;
			mmLabel45.BackColor = System.Drawing.Color.Transparent;
			mmLabel45.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel45.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel45.IsFieldHeader = false;
			mmLabel45.IsRequired = false;
			mmLabel45.Location = new System.Drawing.Point(29, 117);
			mmLabel45.Name = "mmLabel45";
			mmLabel45.PenWidth = 1f;
			mmLabel45.ShowBorder = false;
			mmLabel45.Size = new System.Drawing.Size(25, 13);
			mmLabel45.TabIndex = 114;
			mmLabel45.Text = "CC:";
			mmLabel45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxCCNotification.BackColor = System.Drawing.Color.White;
			textBoxCCNotification.CustomReportFieldName = "";
			textBoxCCNotification.CustomReportKey = "";
			textBoxCCNotification.CustomReportValueType = 1;
			textBoxCCNotification.IsComboTextBox = false;
			textBoxCCNotification.IsRequired = true;
			textBoxCCNotification.Location = new System.Drawing.Point(127, 114);
			textBoxCCNotification.MaxLength = 64;
			textBoxCCNotification.Name = "textBoxCCNotification";
			textBoxCCNotification.Size = new System.Drawing.Size(178, 20);
			textBoxCCNotification.TabIndex = 113;
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(15, 16);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(153, 13);
			mmLabel38.TabIndex = 79;
			mmLabel38.Text = "Notifications && Reminders";
			mmLabel38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE4Port.BackColor = System.Drawing.Color.White;
			textBoxE4Port.CustomReportFieldName = "";
			textBoxE4Port.CustomReportKey = "";
			textBoxE4Port.CustomReportValueType = 1;
			textBoxE4Port.IsComboTextBox = false;
			textBoxE4Port.IsRequired = true;
			textBoxE4Port.Location = new System.Drawing.Point(674, 96);
			textBoxE4Port.MaxLength = 64;
			textBoxE4Port.Name = "textBoxE4Port";
			textBoxE4Port.Size = new System.Drawing.Size(65, 20);
			textBoxE4Port.TabIndex = 37;
			textBoxE4Port.Text = "25";
			buttonCopyE4.Location = new System.Drawing.Point(865, 21);
			buttonCopyE4.Name = "buttonCopyE4";
			buttonCopyE4.Size = new System.Drawing.Size(75, 23);
			buttonCopyE4.TabIndex = 29;
			buttonCopyE4.Text = "Copy ";
			buttonCopyE4.UseVisualStyleBackColor = true;
			buttonCopyE4.Click += new System.EventHandler(button1_Click);
			mmLabel33.AutoSize = true;
			mmLabel33.BackColor = System.Drawing.Color.Transparent;
			mmLabel33.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel33.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel33.IsFieldHeader = false;
			mmLabel33.IsRequired = false;
			mmLabel33.Location = new System.Drawing.Point(637, 100);
			mmLabel33.Name = "mmLabel33";
			mmLabel33.PenWidth = 1f;
			mmLabel33.ShowBorder = false;
			mmLabel33.Size = new System.Drawing.Size(31, 13);
			mmLabel33.TabIndex = 112;
			mmLabel33.Text = "Port:";
			mmLabel33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(29, 54);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(75, 13);
			mmLabel25.TabIndex = 30;
			mmLabel25.Text = "Sender Name:";
			mmLabel25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxE4SSL.AutoSize = true;
			checkBoxE4SSL.Location = new System.Drawing.Point(763, 97);
			checkBoxE4SSL.Name = "checkBoxE4SSL";
			checkBoxE4SSL.Size = new System.Drawing.Size(82, 17);
			checkBoxE4SSL.TabIndex = 38;
			checkBoxE4SSL.Text = "Enable SSL";
			checkBoxE4SSL.UseVisualStyleBackColor = true;
			textBoxE4Name.BackColor = System.Drawing.Color.White;
			textBoxE4Name.CustomReportFieldName = "";
			textBoxE4Name.CustomReportKey = "";
			textBoxE4Name.CustomReportValueType = 1;
			textBoxE4Name.IsComboTextBox = false;
			textBoxE4Name.IsRequired = true;
			textBoxE4Name.Location = new System.Drawing.Point(127, 51);
			textBoxE4Name.MaxLength = 64;
			textBoxE4Name.Name = "textBoxE4Name";
			textBoxE4Name.Size = new System.Drawing.Size(178, 20);
			textBoxE4Name.TabIndex = 31;
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(15, 31);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(174, 13);
			mmLabel37.TabIndex = 80;
			mmLabel37.Text = "System notifications and reminders";
			mmLabel37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(29, 77);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(77, 13);
			mmLabel24.TabIndex = 67;
			mmLabel24.Text = "Email Address:";
			mmLabel24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE4Address.BackColor = System.Drawing.Color.White;
			textBoxE4Address.CustomReportFieldName = "";
			textBoxE4Address.CustomReportKey = "";
			textBoxE4Address.CustomReportValueType = 1;
			textBoxE4Address.IsComboTextBox = false;
			textBoxE4Address.IsRequired = true;
			textBoxE4Address.Location = new System.Drawing.Point(127, 74);
			textBoxE4Address.MaxLength = 64;
			textBoxE4Address.Name = "textBoxE4Address";
			textBoxE4Address.Size = new System.Drawing.Size(178, 20);
			textBoxE4Address.TabIndex = 32;
			textBoxE4InServer.BackColor = System.Drawing.Color.White;
			textBoxE4InServer.CustomReportFieldName = "";
			textBoxE4InServer.CustomReportKey = "";
			textBoxE4InServer.CustomReportValueType = 1;
			textBoxE4InServer.IsComboTextBox = false;
			textBoxE4InServer.IsRequired = true;
			textBoxE4InServer.Location = new System.Drawing.Point(762, 48);
			textBoxE4InServer.MaxLength = 64;
			textBoxE4InServer.Name = "textBoxE4InServer";
			textBoxE4InServer.Size = new System.Drawing.Size(178, 20);
			textBoxE4InServer.TabIndex = 35;
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(637, 72);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(110, 13);
			mmLabel23.TabIndex = 68;
			mmLabel23.Text = "Outgoing mail server:";
			mmLabel23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(637, 51);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(117, 13);
			mmLabel20.TabIndex = 72;
			mmLabel20.Text = "Incomming mail server:";
			mmLabel20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE4OutServer.BackColor = System.Drawing.Color.White;
			textBoxE4OutServer.CustomReportFieldName = "";
			textBoxE4OutServer.CustomReportKey = "";
			textBoxE4OutServer.CustomReportValueType = 1;
			textBoxE4OutServer.IsComboTextBox = false;
			textBoxE4OutServer.IsRequired = true;
			textBoxE4OutServer.Location = new System.Drawing.Point(762, 71);
			textBoxE4OutServer.MaxLength = 64;
			textBoxE4OutServer.Name = "textBoxE4OutServer";
			textBoxE4OutServer.Size = new System.Drawing.Size(178, 20);
			textBoxE4OutServer.TabIndex = 36;
			textBoxE4Password.BackColor = System.Drawing.Color.White;
			textBoxE4Password.CustomReportFieldName = "";
			textBoxE4Password.CustomReportKey = "";
			textBoxE4Password.CustomReportValueType = 1;
			textBoxE4Password.IsComboTextBox = false;
			textBoxE4Password.IsRequired = true;
			textBoxE4Password.Location = new System.Drawing.Point(419, 72);
			textBoxE4Password.MaxLength = 32;
			textBoxE4Password.Name = "textBoxE4Password";
			textBoxE4Password.Size = new System.Drawing.Size(178, 20);
			textBoxE4Password.TabIndex = 34;
			textBoxE4Password.UseSystemPasswordChar = true;
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(321, 51);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(63, 13);
			mmLabel22.TabIndex = 69;
			mmLabel22.Text = "User Name:";
			mmLabel22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(321, 74);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(57, 13);
			mmLabel21.TabIndex = 70;
			mmLabel21.Text = "Password:";
			mmLabel21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxE4UserName.BackColor = System.Drawing.Color.White;
			textBoxE4UserName.CustomReportFieldName = "";
			textBoxE4UserName.CustomReportKey = "";
			textBoxE4UserName.CustomReportValueType = 1;
			textBoxE4UserName.IsComboTextBox = false;
			textBoxE4UserName.IsRequired = true;
			textBoxE4UserName.Location = new System.Drawing.Point(419, 49);
			textBoxE4UserName.MaxLength = 64;
			textBoxE4UserName.Name = "textBoxE4UserName";
			textBoxE4UserName.Size = new System.Drawing.Size(178, 20);
			textBoxE4UserName.TabIndex = 33;
			ribbonControl1.ExpandCollapseItem.Id = 0;
			ribbonControl1.ExpandCollapseItem.Name = "";
			ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[1]
			{
				ribbonControl1.ExpandCollapseItem
			});
			ribbonControl1.Location = new System.Drawing.Point(0, 0);
			ribbonControl1.MaxItemId = 1;
			ribbonControl1.Name = "ribbonControl1";
			ribbonControl1.Size = new System.Drawing.Size(998, 47);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 631);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(998, 40);
			panelButtons.TabIndex = 39;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(998, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(888, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Cancel";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(buttonClose_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(788, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 15;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Location = new System.Drawing.Point(8, 10);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(978, 615);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 299;
			appearance.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance;
			ultraTab.Key = "Email";
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&Accounting";
			ultraTab2.Key = "Log";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Purchasing";
			ultraTab3.TabPage = ultraTabPageControl1;
			ultraTab3.Text = "&Sales";
			ultraTab4.TabPage = ultraTabPageControl2;
			ultraTab4.Text = "&Notifications";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(974, 592);
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(998, 671);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(formManager);
			base.Controls.Add(ribbonControl1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(400, 215);
			base.Name = "EmailConfigForm";
			Text = "Email Configuration";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(AccountGroupDetailsForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
