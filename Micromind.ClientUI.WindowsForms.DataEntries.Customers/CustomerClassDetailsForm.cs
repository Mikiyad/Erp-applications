using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerClassDetailsForm : Form, IForm
	{
		private CustomerClassData currentData;

		private const string TABLENAME_CONST = "Customer_Class";

		private const string IDFIELD_CONST = "ClassID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private MMLabel mmLabel4;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private UltraGroupBox ultraGroupBox2;

		private UltraFormattedLinkLabel linkLabelARAccount;

		private AllAccountsComboBox comboBoxARAccount;

		private CheckBox checkBoxIsInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private CheckBox checkBoxShowInPOS;

		private MMTextBox textBoxAccountName;

		private CheckBox checkBoxShowInLPO;

		private CheckBox checkBoxShowInProp;

		private ToolStripButton toolStripButtonInformation;

		private UltraGroupBox ultraGroupBox7;

		private TaxGroupComboBox comboBoxTaxGroup;

		private MMLabel mmLabel9;

		private ComboBox comboBoxTaxOption;

		private MMTextBox textBoxGroupName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2002;

		public ScreenTypes ScreenType => ScreenTypes.Card;

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
					buttonNew.Text = UIMessages.ClearButtonText;
					buttonDelete.Enabled = false;
					textBoxCode.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
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
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
			}
		}

		public CustomerClassDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += CustomerClassDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CustomerClassData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomerClassTable.Rows[0] : currentData.CustomerClassTable.NewRow();
				dataRow.BeginEdit();
				dataRow["ClassID"] = textBoxCode.Text.Trim();
				dataRow["ClassName"] = textBoxName.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["IsInactive"] = checkBoxIsInactive.Checked;
				dataRow["HasPOSAccess"] = !checkBoxShowInPOS.Checked;
				dataRow["IsLPO"] = checkBoxShowInLPO.Checked;
				dataRow["IsPRO"] = checkBoxShowInProp.Checked;
				if (comboBoxARAccount.SelectedID != "")
				{
					dataRow["ARAccountID"] = comboBoxARAccount.SelectedID;
				}
				else
				{
					dataRow["ARAccountID"] = DBNull.Value;
				}
				dataRow["TaxOption"] = checked(comboBoxTaxOption.SelectedIndex + 1);
				if (comboBoxTaxOption.SelectedIndex == 2)
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				else
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerClassTable.Rows.Add(dataRow);
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
			textBoxCode.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.CustomerClassSystem.GetCustomerClassByID(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
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
				ClearForm();
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["ClassID"].ToString();
					textBoxName.Text = dataRow["ClassName"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					if (dataRow["IsInactive"] != DBNull.Value)
					{
						checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					}
					else
					{
						checkBoxIsInactive.Checked = false;
					}
					if (dataRow["ARAccountID"] != DBNull.Value)
					{
						comboBoxARAccount.SelectedID = dataRow["ARAccountID"].ToString();
					}
					else
					{
						comboBoxARAccount.Clear();
					}
					if (dataRow["HasPOSAccess"] != DBNull.Value)
					{
						checkBoxShowInPOS.Checked = !bool.Parse(dataRow["HasPOSAccess"].ToString());
					}
					else
					{
						checkBoxShowInPOS.Checked = false;
					}
					if (dataRow["IsLPO"] != DBNull.Value)
					{
						checkBoxShowInLPO.Checked = bool.Parse(dataRow["IsLPO"].ToString());
					}
					else
					{
						checkBoxShowInLPO.Checked = false;
					}
					if (dataRow["IsPRO"] != DBNull.Value)
					{
						checkBoxShowInProp.Checked = bool.Parse(dataRow["IsPRO"].ToString());
					}
					else
					{
						checkBoxShowInProp.Checked = false;
					}
					if (!string.IsNullOrEmpty(dataRow["TaxOption"].ToString()))
					{
						comboBoxTaxOption.SelectedIndex = checked(int.Parse(dataRow["TaxOption"].ToString()) - 1);
					}
					else
					{
						comboBoxTaxOption.SelectedIndex = 1;
					}
					if (!string.IsNullOrEmpty(dataRow["TaxGroupID"].ToString()))
					{
						comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
					}
					else
					{
						comboBoxTaxGroup.Clear();
					}
				}
			}
			catch
			{
				throw;
			}
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.CustomerClassSystem.CreateCustomerClass(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.CustomerClass, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.CustomerClassSystem.UpdateCustomerClass(currentData);
				}
				if (flag)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Customer, needRefresh: true);
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					IsNewRecord = true;
					ClearForm();
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
			if (!screenRight.New && isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
				return false;
			}
			if (!screenRight.Edit && !isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Customer_Class", "ClassID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Customer_Class", "ClassID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Customer_Class", "ClassID");
			textBoxName.Clear();
			textBoxNote.Clear();
			checkBoxIsInactive.Checked = false;
			comboBoxARAccount.Clear();
			checkBoxShowInPOS.Checked = true;
			checkBoxShowInLPO.Checked = false;
			checkBoxShowInProp.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
			comboBoxTaxGroup.Clear();
			comboBoxTaxOption.SelectedIndex = -1;
		}

		private void CustomerClassGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void CustomerClassGroupDetailsForm_Validated(object sender, EventArgs e)
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
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.CustomerClassSystem.DeleteCustomerClass(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Customer_Class", "ClassID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Customer_Class", "ClassID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Customer_Class", "ClassID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Customer_Class", "ClassID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Customer_Class", "ClassID", toolStripTextBoxFind.Text.Trim()))
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

		private void CustomerClassDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void linkLabelARAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxARAccount.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.CustomerClass);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void comboBoxTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxOption.SelectedIndex == 0)
			{
				comboBoxTaxGroup.ReadOnly = false;
				return;
			}
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.Clear();
		}

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxTaxGroup.SelectedID);
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerClassDetailsForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.linePanelDown = new Micromind.UISupport.Line();
            this.buttonDelete = new Micromind.UISupport.XPButton();
            this.xpButton1 = new Micromind.UISupport.XPButton();
            this.buttonNew = new Micromind.UISupport.XPButton();
            this.buttonSave = new Micromind.UISupport.XPButton();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.textBoxAccountName = new Micromind.UISupport.MMTextBox();
            this.linkLabelARAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxARAccount = new Micromind.DataControls.AllAccountsComboBox();
            this.checkBoxShowInPOS = new System.Windows.Forms.CheckBox();
            this.checkBoxIsInactive = new System.Windows.Forms.CheckBox();
            this.checkBoxShowInLPO = new System.Windows.Forms.CheckBox();
            this.checkBoxShowInProp = new System.Windows.Forms.CheckBox();
            this.ultraGroupBox7 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxGroupName = new Micromind.UISupport.MMTextBox();
            this.mmLabel9 = new Micromind.UISupport.MMLabel();
            this.comboBoxTaxOption = new System.Windows.Forms.ComboBox();
            this.comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
            this.formManager = new Micromind.DataControls.FormManager();
            this.textBoxNote = new Micromind.UISupport.MMTextBox();
            this.textBoxName = new Micromind.UISupport.MMTextBox();
            this.textBoxCode = new Micromind.UISupport.MMTextBox();
            this.mmLabel4 = new Micromind.UISupport.MMLabel();
            this.mmLabel1 = new Micromind.UISupport.MMLabel();
            this.labelCode = new Micromind.UISupport.MMLabel();
            this.toolStrip1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxARAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox7)).BeginInit();
            this.ultraGroupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxTaxGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPrint,
            this.toolStripButtonFirst,
            this.toolStripButtonPrevious,
            this.toolStripButtonNext,
            this.toolStripButtonLast,
            this.toolStripSeparator1,
            this.toolStripButtonOpenList,
            this.toolStripSeparator3,
            this.toolStripTextBoxFind,
            this.toolStripButtonFind,
            this.toolStripSeparator2,
            this.toolStripButtonInformation});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(553, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonPrint.Image = global::Micromind.ClientUI.Properties.Resources.printer;
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
            this.toolStripButtonPrint.Text = "&Print";
            this.toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
            this.toolStripButtonPrint.Visible = false;
            // 
            // toolStripButtonFirst
            // 
            this.toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFirst.Image = global::Micromind.ClientUI.Properties.Resources.first;
            this.toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFirst.Name = "toolStripButtonFirst";
            this.toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonFirst.Text = "First";
            this.toolStripButtonFirst.Click += new System.EventHandler(this.toolStripButtonFirst_Click);
            // 
            // toolStripButtonPrevious
            // 
            this.toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrevious.Image = global::Micromind.ClientUI.Properties.Resources.prev;
            this.toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrevious.Name = "toolStripButtonPrevious";
            this.toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPrevious.Text = "Previous";
            this.toolStripButtonPrevious.Click += new System.EventHandler(this.toolStripButtonPrevious_Click);
            // 
            // toolStripButtonNext
            // 
            this.toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNext.Image = global::Micromind.ClientUI.Properties.Resources.next;
            this.toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNext.Name = "toolStripButtonNext";
            this.toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonNext.Text = "Next";
            this.toolStripButtonNext.Click += new System.EventHandler(this.toolStripButtonNext_Click);
            // 
            // toolStripButtonLast
            // 
            this.toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLast.Image = global::Micromind.ClientUI.Properties.Resources.last;
            this.toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLast.Name = "toolStripButtonLast";
            this.toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonLast.Text = "Last";
            this.toolStripButtonLast.Click += new System.EventHandler(this.toolStripButtonLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonOpenList
            // 
            this.toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenList.Image = global::Micromind.ClientUI.Properties.Resources.list;
            this.toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenList.Name = "toolStripButtonOpenList";
            this.toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonOpenList.Text = "Open List";
            this.toolStripButtonOpenList.Click += new System.EventHandler(this.toolStripButtonOpenList_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripTextBoxFind
            // 
            this.toolStripTextBoxFind.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxFind.Name = "toolStripTextBoxFind";
            this.toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
            // 
            // toolStripButtonFind
            // 
            this.toolStripButtonFind.Image = global::Micromind.ClientUI.Properties.Resources.find;
            this.toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFind.Name = "toolStripButtonFind";
            this.toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
            this.toolStripButtonFind.Text = "Find";
            this.toolStripButtonFind.Click += new System.EventHandler(this.toolStripButtonFind_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonInformation
            // 
            this.toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInformation.Image = global::Micromind.ClientUI.Properties.Resources.docinfo_24x24;
            this.toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInformation.Name = "toolStripButtonInformation";
            this.toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonInformation.Text = "Document Information";
            this.toolStripButtonInformation.Click += new System.EventHandler(this.toolStripButtonInformation_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.linePanelDown);
            this.panelButtons.Controls.Add(this.buttonDelete);
            this.panelButtons.Controls.Add(this.xpButton1);
            this.panelButtons.Controls.Add(this.buttonNew);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 316);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(553, 40);
            this.panelButtons.TabIndex = 9;
            // 
            // linePanelDown
            // 
            this.linePanelDown.BackColor = System.Drawing.Color.White;
            this.linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.linePanelDown.DrawWidth = 1;
            this.linePanelDown.IsVertical = false;
            this.linePanelDown.LineBackColor = System.Drawing.Color.Silver;
            this.linePanelDown.Location = new System.Drawing.Point(0, 0);
            this.linePanelDown.Name = "linePanelDown";
            this.linePanelDown.Size = new System.Drawing.Size(553, 1);
            this.linePanelDown.TabIndex = 14;
            this.linePanelDown.TabStop = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonDelete.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonDelete.Location = new System.Drawing.Point(216, 8);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(96, 24);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "De&lete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // xpButton1
            // 
            this.xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.xpButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xpButton1.BackColor = System.Drawing.Color.DarkGray;
            this.xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.xpButton1.Location = new System.Drawing.Point(443, 8);
            this.xpButton1.Name = "xpButton1";
            this.xpButton1.Size = new System.Drawing.Size(96, 24);
            this.xpButton1.TabIndex = 3;
            this.xpButton1.Text = "&Close";
            this.xpButton1.UseVisualStyleBackColor = false;
            this.xpButton1.Click += new System.EventHandler(this.xpButton1_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonNew.BackColor = System.Drawing.Color.DarkGray;
            this.buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonNew.Location = new System.Drawing.Point(114, 8);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(96, 24);
            this.buttonNew.TabIndex = 1;
            this.buttonNew.Text = "Ne&w...";
            this.buttonNew.UseVisualStyleBackColor = false;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonSave.BackColor = System.Drawing.Color.Silver;
            this.buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSave.Location = new System.Drawing.Point(12, 8);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(96, 24);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.ultraGroupBox2.Controls.Add(this.textBoxAccountName);
            this.ultraGroupBox2.Controls.Add(this.linkLabelARAccount);
            this.ultraGroupBox2.Controls.Add(this.comboBoxARAccount);
            this.ultraGroupBox2.Location = new System.Drawing.Point(7, 139);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(510, 66);
            this.ultraGroupBox2.TabIndex = 7;
            this.ultraGroupBox2.Text = "Accounts";
            // 
            // textBoxAccountName
            // 
            this.textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxAccountName.CustomReportFieldName = "";
            this.textBoxAccountName.CustomReportKey = "";
            this.textBoxAccountName.CustomReportValueType = ((byte)(1));
            this.textBoxAccountName.IsComboTextBox = false;
            this.textBoxAccountName.IsModified = false;
            this.textBoxAccountName.Location = new System.Drawing.Point(106, 43);
            this.textBoxAccountName.MaxLength = 255;
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.textBoxAccountName.ReadOnly = true;
            this.textBoxAccountName.Size = new System.Drawing.Size(398, 20);
            this.textBoxAccountName.TabIndex = 1;
            this.textBoxAccountName.TabStop = false;
            // 
            // linkLabelARAccount
            // 
            this.linkLabelARAccount.AutoSize = true;
            this.linkLabelARAccount.Location = new System.Drawing.Point(9, 24);
            this.linkLabelARAccount.Name = "linkLabelARAccount";
            this.linkLabelARAccount.Size = new System.Drawing.Size(91, 14);
            this.linkLabelARAccount.TabIndex = 0;
            this.linkLabelARAccount.TabStop = true;
            this.linkLabelARAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.linkLabelARAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.linkLabelARAccount.Value = "A/C Receiveable:";
            appearance1.ForeColor = System.Drawing.Color.Blue;
            this.linkLabelARAccount.VisitedLinkAppearance = appearance1;
            this.linkLabelARAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.linkLabelARAccount_LinkClicked);
            // 
            // comboBoxARAccount
            // 
            this.comboBoxARAccount.Assigned = false;
            this.comboBoxARAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxARAccount.CustomReportFieldName = "";
            this.comboBoxARAccount.CustomReportKey = "";
            this.comboBoxARAccount.CustomReportValueType = ((byte)(1));
            this.comboBoxARAccount.DescriptionTextBox = this.textBoxAccountName;
            appearance2.BackColor = System.Drawing.SystemColors.Window;
            appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxARAccount.DisplayLayout.Appearance = appearance2;
            this.comboBoxARAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxARAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance3.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxARAccount.DisplayLayout.GroupByBox.Appearance = appearance3;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxARAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
            this.comboBoxARAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance5.BackColor2 = System.Drawing.SystemColors.Control;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxARAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
            this.comboBoxARAccount.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxARAccount.DisplayLayout.MaxRowScrollRegions = 1;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxARAccount.DisplayLayout.Override.ActiveCellAppearance = appearance6;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxARAccount.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.comboBoxARAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxARAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance8.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxARAccount.DisplayLayout.Override.CardAreaAppearance = appearance8;
            appearance9.BorderColor = System.Drawing.Color.Silver;
            appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxARAccount.DisplayLayout.Override.CellAppearance = appearance9;
            this.comboBoxARAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxARAccount.DisplayLayout.Override.CellPadding = 0;
            appearance10.BackColor = System.Drawing.SystemColors.Control;
            appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance10.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxARAccount.DisplayLayout.Override.GroupByRowAppearance = appearance10;
            appearance11.TextHAlignAsString = "Left";
            this.comboBoxARAccount.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.comboBoxARAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxARAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxARAccount.DisplayLayout.Override.RowAppearance = appearance12;
            this.comboBoxARAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxARAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
            this.comboBoxARAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxARAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxARAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxARAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxARAccount.Editable = true;
            this.comboBoxARAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
            this.comboBoxARAccount.FilterString = "";
            this.comboBoxARAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
            this.comboBoxARAccount.FilterSysDocID = "";
            this.comboBoxARAccount.HasAllAccount = false;
            this.comboBoxARAccount.HasCustom = false;
            this.comboBoxARAccount.IsDataLoaded = false;
            this.comboBoxARAccount.Location = new System.Drawing.Point(106, 22);
            this.comboBoxARAccount.MaxDropDownItems = 12;
            this.comboBoxARAccount.MaxLength = 64;
            this.comboBoxARAccount.Name = "comboBoxARAccount";
            this.comboBoxARAccount.ShowInactiveItems = false;
            this.comboBoxARAccount.ShowQuickAdd = true;
            this.comboBoxARAccount.Size = new System.Drawing.Size(195, 20);
            this.comboBoxARAccount.TabIndex = 0;
            this.comboBoxARAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // checkBoxShowInPOS
            // 
            this.checkBoxShowInPOS.AutoSize = true;
            this.checkBoxShowInPOS.Checked = true;
            this.checkBoxShowInPOS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowInPOS.Location = new System.Drawing.Point(113, 106);
            this.checkBoxShowInPOS.Name = "checkBoxShowInPOS";
            this.checkBoxShowInPOS.Size = new System.Drawing.Size(141, 17);
            this.checkBoxShowInPOS.TabIndex = 4;
            this.checkBoxShowInPOS.Text = "Show in POS Customers";
            this.checkBoxShowInPOS.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsInactive
            // 
            this.checkBoxIsInactive.AutoSize = true;
            this.checkBoxIsInactive.Location = new System.Drawing.Point(419, 37);
            this.checkBoxIsInactive.Name = "checkBoxIsInactive";
            this.checkBoxIsInactive.Size = new System.Drawing.Size(64, 17);
            this.checkBoxIsInactive.TabIndex = 1;
            this.checkBoxIsInactive.Text = "Inactive";
            this.checkBoxIsInactive.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowInLPO
            // 
            this.checkBoxShowInLPO.AutoSize = true;
            this.checkBoxShowInLPO.Location = new System.Drawing.Point(254, 106);
            this.checkBoxShowInLPO.Name = "checkBoxShowInLPO";
            this.checkBoxShowInLPO.Size = new System.Drawing.Size(135, 17);
            this.checkBoxShowInLPO.TabIndex = 5;
            this.checkBoxShowInLPO.Text = "Show in LPO Customer";
            this.checkBoxShowInLPO.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowInProp
            // 
            this.checkBoxShowInProp.AutoSize = true;
            this.checkBoxShowInProp.Location = new System.Drawing.Point(388, 105);
            this.checkBoxShowInProp.Name = "checkBoxShowInProp";
            this.checkBoxShowInProp.Size = new System.Drawing.Size(153, 17);
            this.checkBoxShowInProp.TabIndex = 6;
            this.checkBoxShowInProp.Text = "Show in Property Customer";
            this.checkBoxShowInProp.UseVisualStyleBackColor = true;
            // 
            // ultraGroupBox7
            // 
            this.ultraGroupBox7.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.ultraGroupBox7.Controls.Add(this.ultraFormattedLinkLabel10);
            this.ultraGroupBox7.Controls.Add(this.textBoxGroupName);
            this.ultraGroupBox7.Controls.Add(this.mmLabel9);
            this.ultraGroupBox7.Controls.Add(this.comboBoxTaxOption);
            this.ultraGroupBox7.Location = new System.Drawing.Point(12, 217);
            this.ultraGroupBox7.Name = "ultraGroupBox7";
            this.ultraGroupBox7.Size = new System.Drawing.Size(512, 93);
            this.ultraGroupBox7.TabIndex = 8;
            this.ultraGroupBox7.Text = "Tax Details";
            // 
            // ultraFormattedLinkLabel10
            // 
            this.ultraFormattedLinkLabel10.AutoSize = true;
            appearance14.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel10.LinkAppearance = appearance14;
            this.ultraFormattedLinkLabel10.Location = new System.Drawing.Point(4, 51);
            this.ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
            this.ultraFormattedLinkLabel10.Size = new System.Drawing.Size(59, 14);
            this.ultraFormattedLinkLabel10.TabIndex = 73;
            this.ultraFormattedLinkLabel10.TabStop = true;
            this.ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel10.Value = "Tax Group:";
            appearance15.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance15;
            this.ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel10_LinkClicked);
            // 
            // textBoxGroupName
            // 
            this.textBoxGroupName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxGroupName.CustomReportFieldName = "";
            this.textBoxGroupName.CustomReportKey = "";
            this.textBoxGroupName.CustomReportValueType = ((byte)(1));
            this.textBoxGroupName.IsComboTextBox = false;
            this.textBoxGroupName.IsModified = false;
            this.textBoxGroupName.Location = new System.Drawing.Point(260, 46);
            this.textBoxGroupName.MaxLength = 30;
            this.textBoxGroupName.Name = "textBoxGroupName";
            this.textBoxGroupName.ReadOnly = true;
            this.textBoxGroupName.Size = new System.Drawing.Size(239, 20);
            this.textBoxGroupName.TabIndex = 72;
            this.textBoxGroupName.TabStop = false;
            // 
            // mmLabel9
            // 
            this.mmLabel9.AutoSize = true;
            this.mmLabel9.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel9.IsFieldHeader = false;
            this.mmLabel9.IsRequired = false;
            this.mmLabel9.Location = new System.Drawing.Point(1, 25);
            this.mmLabel9.Name = "mmLabel9";
            this.mmLabel9.PenWidth = 1F;
            this.mmLabel9.ShowBorder = false;
            this.mmLabel9.Size = new System.Drawing.Size(64, 13);
            this.mmLabel9.TabIndex = 70;
            this.mmLabel9.Text = "Tax Option:";
            // 
            // comboBoxTaxOption
            // 
            this.comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxTaxOption.FormattingEnabled = true;
            this.comboBoxTaxOption.Items.AddRange(new object[] {
            "Taxable",
            "Non Taxable"});
            this.comboBoxTaxOption.Location = new System.Drawing.Point(98, 22);
            this.comboBoxTaxOption.Name = "comboBoxTaxOption";
            this.comboBoxTaxOption.Size = new System.Drawing.Size(161, 21);
            this.comboBoxTaxOption.TabIndex = 0;
            this.comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(this.comboBoxTaxOption_SelectedIndexChanged);
            // 
            // comboBoxTaxGroup
            // 
            this.comboBoxTaxGroup.Assigned = false;
            this.comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxTaxGroup.CustomReportFieldName = "";
            this.comboBoxTaxGroup.CustomReportKey = "";
            this.comboBoxTaxGroup.CustomReportValueType = ((byte)(1));
            this.comboBoxTaxGroup.DescriptionTextBox = this.textBoxGroupName;
            appearance16.BackColor = System.Drawing.SystemColors.Window;
            appearance16.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxTaxGroup.DisplayLayout.Appearance = appearance16;
            this.comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance17.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance17.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance17.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance17;
            appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance18;
            this.comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance19.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance19.BackColor2 = System.Drawing.SystemColors.Control;
            appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance19;
            this.comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
            appearance20.BackColor = System.Drawing.SystemColors.Window;
            appearance20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance20;
            appearance21.BackColor = System.Drawing.SystemColors.Highlight;
            appearance21.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance21;
            this.comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance22.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance22;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            appearance23.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance23;
            this.comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
            appearance24.BackColor = System.Drawing.SystemColors.Control;
            appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance24.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance24.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance24;
            appearance25.TextHAlignAsString = "Left";
            this.comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance25;
            this.comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance26.BackColor = System.Drawing.SystemColors.Window;
            appearance26.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance26;
            this.comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance27.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance27;
            this.comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxTaxGroup.Editable = true;
            this.comboBoxTaxGroup.FilterString = "";
            this.comboBoxTaxGroup.HasAllAccount = false;
            this.comboBoxTaxGroup.HasCustom = false;
            this.comboBoxTaxGroup.IsDataLoaded = false;
            this.comboBoxTaxGroup.Location = new System.Drawing.Point(110, 263);
            this.comboBoxTaxGroup.MaxDropDownItems = 12;
            this.comboBoxTaxGroup.Name = "comboBoxTaxGroup";
            this.comboBoxTaxGroup.ReadOnly = true;
            this.comboBoxTaxGroup.ShowInactiveItems = false;
            this.comboBoxTaxGroup.ShowQuickAdd = true;
            this.comboBoxTaxGroup.Size = new System.Drawing.Size(161, 20);
            this.comboBoxTaxGroup.TabIndex = 1;
            this.comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // formManager
            // 
            this.formManager.BackColor = System.Drawing.Color.RosyBrown;
            this.formManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.formManager.IsForcedDirty = false;
            this.formManager.Location = new System.Drawing.Point(0, 31);
            this.formManager.MaximumSize = new System.Drawing.Size(20, 20);
            this.formManager.MinimumSize = new System.Drawing.Size(20, 20);
            this.formManager.Name = "formManager";
            this.formManager.Size = new System.Drawing.Size(20, 20);
            this.formManager.TabIndex = 16;
            this.formManager.Text = "formManager1";
            this.formManager.Visible = false;
            // 
            // textBoxNote
            // 
            this.textBoxNote.BackColor = System.Drawing.Color.White;
            this.textBoxNote.CustomReportFieldName = "";
            this.textBoxNote.CustomReportKey = "";
            this.textBoxNote.CustomReportValueType = ((byte)(1));
            this.textBoxNote.IsComboTextBox = false;
            this.textBoxNote.IsModified = false;
            this.textBoxNote.Location = new System.Drawing.Point(114, 80);
            this.textBoxNote.MaxLength = 255;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(397, 20);
            this.textBoxNote.TabIndex = 3;
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.Color.White;
            this.textBoxName.CustomReportFieldName = "";
            this.textBoxName.CustomReportKey = "";
            this.textBoxName.CustomReportValueType = ((byte)(1));
            this.textBoxName.IsComboTextBox = false;
            this.textBoxName.IsModified = false;
            this.textBoxName.Location = new System.Drawing.Point(114, 58);
            this.textBoxName.MaxLength = 64;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(397, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // textBoxCode
            // 
            this.textBoxCode.BackColor = System.Drawing.Color.White;
            this.textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxCode.CustomReportFieldName = "";
            this.textBoxCode.CustomReportKey = "";
            this.textBoxCode.CustomReportValueType = ((byte)(1));
            this.textBoxCode.IsComboTextBox = false;
            this.textBoxCode.IsModified = false;
            this.textBoxCode.Location = new System.Drawing.Point(114, 36);
            this.textBoxCode.MaxLength = 15;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(299, 20);
            this.textBoxCode.TabIndex = 0;
            // 
            // mmLabel4
            // 
            this.mmLabel4.AutoSize = true;
            this.mmLabel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel4.IsFieldHeader = false;
            this.mmLabel4.IsRequired = false;
            this.mmLabel4.Location = new System.Drawing.Point(14, 82);
            this.mmLabel4.Name = "mmLabel4";
            this.mmLabel4.PenWidth = 1F;
            this.mmLabel4.ShowBorder = false;
            this.mmLabel4.Size = new System.Drawing.Size(33, 13);
            this.mmLabel4.TabIndex = 5;
            this.mmLabel4.Text = "Note:";
            // 
            // mmLabel1
            // 
            this.mmLabel1.AutoSize = true;
            this.mmLabel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mmLabel1.IsFieldHeader = false;
            this.mmLabel1.IsRequired = true;
            this.mmLabel1.Location = new System.Drawing.Point(13, 60);
            this.mmLabel1.Name = "mmLabel1";
            this.mmLabel1.PenWidth = 1F;
            this.mmLabel1.ShowBorder = false;
            this.mmLabel1.Size = new System.Drawing.Size(77, 13);
            this.mmLabel1.TabIndex = 3;
            this.mmLabel1.Text = "Class Name:";
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelCode.IsFieldHeader = false;
            this.labelCode.IsRequired = true;
            this.labelCode.Location = new System.Drawing.Point(13, 38);
            this.labelCode.Name = "labelCode";
            this.labelCode.PenWidth = 1F;
            this.labelCode.ShowBorder = false;
            this.labelCode.Size = new System.Drawing.Size(74, 13);
            this.labelCode.TabIndex = 0;
            this.labelCode.Text = "Class Code:";
            // 
            // CustomerClassDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(553, 356);
            this.Controls.Add(this.comboBoxTaxGroup);
            this.Controls.Add(this.ultraGroupBox7);
            this.Controls.Add(this.checkBoxShowInProp);
            this.Controls.Add(this.checkBoxShowInLPO);
            this.Controls.Add(this.checkBoxShowInPOS);
            this.Controls.Add(this.checkBoxIsInactive);
            this.Controls.Add(this.ultraGroupBox2);
            this.Controls.Add(this.formManager);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.mmLabel4);
            this.Controls.Add(this.mmLabel1);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomerClassDetailsForm";
            this.Text = "Customer Class";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountGroupDetailsForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxARAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox7)).EndInit();
            this.ultraGroupBox7.ResumeLayout(false);
            this.ultraGroupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxTaxGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
