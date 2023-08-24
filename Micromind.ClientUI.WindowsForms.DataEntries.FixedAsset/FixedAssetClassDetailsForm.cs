using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.FixedAsset
{
	public class FixedAssetClassDetailsForm : Form, IForm
	{
		private FixedAssetClassData currentData;

		private const string TABLENAME_CONST = "FixedAsset_Class";

		private const string IDFIELD_CONST = "AssetClassID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

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

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private AllAccountsComboBox comboBoxPLAccount;

		private AllAccountsComboBox comboBoxDepAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private AllAccountsComboBox comboBoxAssetAccount;

		private MMTextBox textBoxPLAccountName;

		private MMTextBox textBoxDepAccountName;

		private MMTextBox textBoxAssetAccountName;

		private DepMethodComboBox comboBoxDepMethod;

		private MMLabel mmLabel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private AllAccountsComboBox comboBoxAccumulatedAccount;

		private MMTextBox textBoxAccumulatedName;

		private ToolStripButton toolStripButtonInformation;

		public ScreenAreas ScreenArea => ScreenAreas.FixedAsset;

		public int ScreenID => 6011;

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

		public FixedAssetClassDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += FixedAssetClassDetailsForm_Load;
			comboBoxAssetAccount.SelectedIndexChanged += comboBoxAssetAccount_SelectedIndexChanged;
			comboBoxDepAccount.SelectedIndexChanged += comboBoxDepAccount_SelectedIndexChanged;
			comboBoxPLAccount.SelectedIndexChanged += comboBoxPLAccount_SelectedIndexChanged;
		}

		private void comboBoxPLAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxPLAccountName.Text = comboBoxPLAccount.SelectedName;
		}

		private void comboBoxDepAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxDepAccountName.Text = comboBoxDepAccount.SelectedName;
		}

		private void comboBoxAssetAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxAssetAccountName.Text = comboBoxAssetAccount.SelectedName;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new FixedAssetClassData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.AssetClassTable.Rows[0] : currentData.AssetClassTable.NewRow();
				dataRow.BeginEdit();
				dataRow["AssetClassID"] = textBoxCode.Text.Trim();
				dataRow["AssetClassName"] = textBoxName.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["AssetAccountID"] = ((comboBoxAssetAccount.SelectedID == "") ? ((IConvertible)DBNull.Value) : ((IConvertible)comboBoxAssetAccount.SelectedID));
				dataRow["DepAccountID"] = ((comboBoxDepAccount.SelectedID == "") ? ((IConvertible)DBNull.Value) : ((IConvertible)comboBoxDepAccount.SelectedID));
				dataRow["AccumDepAccountID"] = ((comboBoxAccumulatedAccount.SelectedID == "") ? ((IConvertible)DBNull.Value) : ((IConvertible)comboBoxAccumulatedAccount.SelectedID));
				dataRow["ProfitLossAccountID"] = ((comboBoxPLAccount.SelectedID == "") ? ((IConvertible)DBNull.Value) : ((IConvertible)comboBoxPLAccount.SelectedID));
				dataRow["DepMethod"] = comboBoxDepMethod.SelectedID;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.AssetClassTable.Rows.Add(dataRow);
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
					currentData = Factory.FixedAssetClassSystem.GetAssetClassByID(id.Trim());
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
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textBoxCode.Text = dataRow["AssetClassID"].ToString();
				textBoxName.Text = dataRow["AssetClassName"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				comboBoxAssetAccount.SelectedID = dataRow["AssetAccountID"].ToString();
				comboBoxAccumulatedAccount.SelectedID = dataRow["AccumDepAccountID"].ToString();
				comboBoxDepAccount.SelectedID = dataRow["DepAccountID"].ToString();
				comboBoxPLAccount.SelectedID = dataRow["ProfitLossAccountID"].ToString();
				if (dataRow["DepMethod"] != DBNull.Value)
				{
					comboBoxDepMethod.SelectedID = int.Parse(dataRow["DepMethod"].ToString());
				}
				checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
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
					flag = Factory.FixedAssetClassSystem.CreateAssetClass(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.FixedAssetClass, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.FixedAssetClassSystem.UpdateAssetClass(currentData);
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
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0 || comboBoxDepMethod.SelectedID == -1)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("FixedAsset_Class", "AssetClassID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("FixedAsset_Class", "AssetClassID");
			textBoxName.Clear();
			textBoxNote.Clear();
			comboBoxAssetAccount.Clear();
			comboBoxDepAccount.Clear();
			comboBoxPLAccount.Clear();
			textBoxAssetAccountName.Clear();
			textBoxDepAccountName.Clear();
			textBoxPLAccountName.Clear();
			comboBoxAccumulatedAccount.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void FixedAssetClassClassDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void FixedAssetClassClassDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.FixedAssetClassSystem.DeleteAssetClass(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("FixedAsset_Class", "AssetClassID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("FixedAsset_Class", "AssetClassID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("FixedAsset_Class", "AssetClassID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("FixedAsset_Class", "AssetClassID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("FixedAsset_Class", "AssetClassID", toolStripTextBoxFind.Text.Trim()))
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

		private void AccountClassDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void FixedAssetClassDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					comboBoxDepMethod.LoadData();
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
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.FixedAssetClass);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
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
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.FixedAsset.FixedAssetClassDetailsForm));
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
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPLAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxDepAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxAssetAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxPLAccountName = new Micromind.UISupport.MMTextBox();
			textBoxDepAccountName = new Micromind.UISupport.MMTextBox();
			textBoxAssetAccountName = new Micromind.UISupport.MMTextBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxDepMethod = new Micromind.DataControls.DepMethodComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxAccumulatedAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxAccumulatedName = new Micromind.UISupport.MMTextBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPLAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAssetAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccumulatedAccount).BeginInit();
			SuspendLayout();
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
			toolStrip1.Size = new System.Drawing.Size(578, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
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
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
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
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 288);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(578, 40);
			panelButtons.TabIndex = 14;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(578, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(468, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(12, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(318, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			appearance.FontData.BoldAsString = "False";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(11, 232);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(82, 15);
			ultraFormattedLinkLabel3.TabIndex = 135;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "P/L on Sale A/C:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance2;
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance3;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(11, 188);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(89, 15);
			ultraFormattedLinkLabel2.TabIndex = 136;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Depreciation A/C:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance4;
			comboBoxPLAccount.Assigned = false;
			comboBoxPLAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPLAccount.CustomReportFieldName = "";
			comboBoxPLAccount.CustomReportKey = "";
			comboBoxPLAccount.CustomReportValueType = 1;
			comboBoxPLAccount.DescriptionTextBox = null;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPLAccount.DisplayLayout.Appearance = appearance5;
			comboBoxPLAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPLAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPLAccount.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPLAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxPLAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPLAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxPLAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPLAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPLAccount.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPLAccount.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxPLAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPLAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPLAccount.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPLAccount.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxPLAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPLAccount.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPLAccount.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxPLAccount.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxPLAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPLAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxPLAccount.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxPLAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPLAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxPLAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPLAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPLAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPLAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPLAccount.Editable = true;
			comboBoxPLAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxPLAccount.FilterString = "";
			comboBoxPLAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxPLAccount.FilterSysDocID = "";
			comboBoxPLAccount.HasAllAccount = false;
			comboBoxPLAccount.HasCustom = false;
			comboBoxPLAccount.IsDataLoaded = false;
			comboBoxPLAccount.Location = new System.Drawing.Point(133, 231);
			comboBoxPLAccount.MaxDropDownItems = 12;
			comboBoxPLAccount.Name = "comboBoxPLAccount";
			comboBoxPLAccount.ShowInactiveItems = false;
			comboBoxPLAccount.ShowQuickAdd = true;
			comboBoxPLAccount.Size = new System.Drawing.Size(151, 20);
			comboBoxPLAccount.TabIndex = 12;
			comboBoxPLAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDepAccount.Assigned = false;
			comboBoxDepAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDepAccount.CustomReportFieldName = "";
			comboBoxDepAccount.CustomReportKey = "";
			comboBoxDepAccount.CustomReportValueType = 1;
			comboBoxDepAccount.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDepAccount.DisplayLayout.Appearance = appearance17;
			comboBoxDepAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDepAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepAccount.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxDepAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxDepAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDepAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDepAccount.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDepAccount.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxDepAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDepAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDepAccount.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDepAccount.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxDepAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDepAccount.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepAccount.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxDepAccount.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxDepAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDepAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxDepAccount.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxDepAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDepAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxDepAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDepAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDepAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDepAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDepAccount.Editable = true;
			comboBoxDepAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxDepAccount.FilterString = "";
			comboBoxDepAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxDepAccount.FilterSysDocID = "";
			comboBoxDepAccount.HasAllAccount = false;
			comboBoxDepAccount.HasCustom = false;
			comboBoxDepAccount.IsDataLoaded = false;
			comboBoxDepAccount.Location = new System.Drawing.Point(133, 187);
			comboBoxDepAccount.MaxDropDownItems = 12;
			comboBoxDepAccount.Name = "comboBoxDepAccount";
			comboBoxDepAccount.ShowInactiveItems = false;
			comboBoxDepAccount.ShowQuickAdd = true;
			comboBoxDepAccount.Size = new System.Drawing.Size(151, 20);
			comboBoxDepAccount.TabIndex = 8;
			comboBoxDepAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance29.FontData.BoldAsString = "False";
			appearance29.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance29;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(11, 166);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabel1.TabIndex = 137;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Asset A/C:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance30;
			comboBoxAssetAccount.Assigned = false;
			comboBoxAssetAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAssetAccount.CustomReportFieldName = "";
			comboBoxAssetAccount.CustomReportKey = "";
			comboBoxAssetAccount.CustomReportValueType = 1;
			comboBoxAssetAccount.DescriptionTextBox = null;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAssetAccount.DisplayLayout.Appearance = appearance31;
			comboBoxAssetAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAssetAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxAssetAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAssetAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAssetAccount.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAssetAccount.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxAssetAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAssetAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAssetAccount.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAssetAccount.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxAssetAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAssetAccount.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAssetAccount.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxAssetAccount.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxAssetAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAssetAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxAssetAccount.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxAssetAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAssetAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxAssetAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAssetAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAssetAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAssetAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAssetAccount.Editable = true;
			comboBoxAssetAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAssetAccount.FilterString = "";
			comboBoxAssetAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAssetAccount.FilterSysDocID = "";
			comboBoxAssetAccount.HasAllAccount = false;
			comboBoxAssetAccount.HasCustom = false;
			comboBoxAssetAccount.IsDataLoaded = false;
			comboBoxAssetAccount.Location = new System.Drawing.Point(133, 165);
			comboBoxAssetAccount.MaxDropDownItems = 12;
			comboBoxAssetAccount.Name = "comboBoxAssetAccount";
			comboBoxAssetAccount.ShowInactiveItems = false;
			comboBoxAssetAccount.ShowQuickAdd = true;
			comboBoxAssetAccount.Size = new System.Drawing.Size(151, 20);
			comboBoxAssetAccount.TabIndex = 6;
			comboBoxAssetAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxPLAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPLAccountName.CustomReportFieldName = "";
			textBoxPLAccountName.CustomReportKey = "";
			textBoxPLAccountName.CustomReportValueType = 1;
			textBoxPLAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxPLAccountName.IsComboTextBox = false;
			textBoxPLAccountName.Location = new System.Drawing.Point(290, 231);
			textBoxPLAccountName.MaxLength = 255;
			textBoxPLAccountName.Name = "textBoxPLAccountName";
			textBoxPLAccountName.ReadOnly = true;
			textBoxPLAccountName.Size = new System.Drawing.Size(265, 20);
			textBoxPLAccountName.TabIndex = 13;
			textBoxPLAccountName.TabStop = false;
			textBoxDepAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDepAccountName.CustomReportFieldName = "";
			textBoxDepAccountName.CustomReportKey = "";
			textBoxDepAccountName.CustomReportValueType = 1;
			textBoxDepAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxDepAccountName.IsComboTextBox = false;
			textBoxDepAccountName.Location = new System.Drawing.Point(290, 187);
			textBoxDepAccountName.MaxLength = 255;
			textBoxDepAccountName.Name = "textBoxDepAccountName";
			textBoxDepAccountName.ReadOnly = true;
			textBoxDepAccountName.Size = new System.Drawing.Size(265, 20);
			textBoxDepAccountName.TabIndex = 9;
			textBoxDepAccountName.TabStop = false;
			textBoxAssetAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAssetAccountName.CustomReportFieldName = "";
			textBoxAssetAccountName.CustomReportKey = "";
			textBoxAssetAccountName.CustomReportValueType = 1;
			textBoxAssetAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxAssetAccountName.IsComboTextBox = false;
			textBoxAssetAccountName.Location = new System.Drawing.Point(290, 165);
			textBoxAssetAccountName.MaxLength = 255;
			textBoxAssetAccountName.Name = "textBoxAssetAccountName";
			textBoxAssetAccountName.ReadOnly = true;
			textBoxAssetAccountName.Size = new System.Drawing.Size(265, 20);
			textBoxAssetAccountName.TabIndex = 7;
			textBoxAssetAccountName.TabStop = false;
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(133, 80);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(422, 20);
			textBoxNote.TabIndex = 3;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(133, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(422, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(133, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(179, 20);
			textBoxCode.TabIndex = 0;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 81);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Note:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 58);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(77, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Class Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(74, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Class Code:";
			comboBoxDepMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDepMethod.FormattingEnabled = true;
			comboBoxDepMethod.Location = new System.Drawing.Point(133, 106);
			comboBoxDepMethod.Name = "comboBoxDepMethod";
			comboBoxDepMethod.Size = new System.Drawing.Size(151, 21);
			comboBoxDepMethod.TabIndex = 4;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(8, 109);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(109, 13);
			mmLabel2.TabIndex = 140;
			mmLabel2.Text = "Depreciation Method:";
			appearance43.FontData.BoldAsString = "False";
			appearance43.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance43;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(11, 210);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(112, 15);
			ultraFormattedLinkLabel4.TabIndex = 144;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Accumulated Dep A/C:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance44;
			comboBoxAccumulatedAccount.Assigned = false;
			comboBoxAccumulatedAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccumulatedAccount.CustomReportFieldName = "";
			comboBoxAccumulatedAccount.CustomReportKey = "";
			comboBoxAccumulatedAccount.CustomReportValueType = 1;
			comboBoxAccumulatedAccount.DescriptionTextBox = textBoxAccumulatedName;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccumulatedAccount.DisplayLayout.Appearance = appearance45;
			comboBoxAccumulatedAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccumulatedAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccumulatedAccount.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccumulatedAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxAccumulatedAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccumulatedAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxAccumulatedAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccumulatedAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccumulatedAccount.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccumulatedAccount.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxAccumulatedAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccumulatedAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccumulatedAccount.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccumulatedAccount.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxAccumulatedAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccumulatedAccount.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccumulatedAccount.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxAccumulatedAccount.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxAccumulatedAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccumulatedAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccumulatedAccount.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxAccumulatedAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccumulatedAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
			comboBoxAccumulatedAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccumulatedAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccumulatedAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccumulatedAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccumulatedAccount.Editable = true;
			comboBoxAccumulatedAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAccumulatedAccount.FilterString = "";
			comboBoxAccumulatedAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAccumulatedAccount.FilterSysDocID = "";
			comboBoxAccumulatedAccount.HasAllAccount = false;
			comboBoxAccumulatedAccount.HasCustom = false;
			comboBoxAccumulatedAccount.IsDataLoaded = false;
			comboBoxAccumulatedAccount.Location = new System.Drawing.Point(133, 209);
			comboBoxAccumulatedAccount.MaxDropDownItems = 12;
			comboBoxAccumulatedAccount.Name = "comboBoxAccumulatedAccount";
			comboBoxAccumulatedAccount.ShowInactiveItems = false;
			comboBoxAccumulatedAccount.ShowQuickAdd = true;
			comboBoxAccumulatedAccount.Size = new System.Drawing.Size(151, 20);
			comboBoxAccumulatedAccount.TabIndex = 10;
			comboBoxAccumulatedAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAccumulatedName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccumulatedName.CustomReportFieldName = "";
			textBoxAccumulatedName.CustomReportKey = "";
			textBoxAccumulatedName.CustomReportValueType = 1;
			textBoxAccumulatedName.ForeColor = System.Drawing.Color.Black;
			textBoxAccumulatedName.IsComboTextBox = false;
			textBoxAccumulatedName.Location = new System.Drawing.Point(290, 209);
			textBoxAccumulatedName.MaxLength = 255;
			textBoxAccumulatedName.Name = "textBoxAccumulatedName";
			textBoxAccumulatedName.ReadOnly = true;
			textBoxAccumulatedName.Size = new System.Drawing.Size(265, 20);
			textBoxAccumulatedName.TabIndex = 11;
			textBoxAccumulatedName.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(578, 328);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(comboBoxAccumulatedAccount);
			base.Controls.Add(textBoxAccumulatedName);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(comboBoxDepMethod);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(comboBoxPLAccount);
			base.Controls.Add(comboBoxDepAccount);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(comboBoxAssetAccount);
			base.Controls.Add(textBoxPLAccountName);
			base.Controls.Add(textBoxDepAccountName);
			base.Controls.Add(textBoxAssetAccountName);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "FixedAssetClassDetailsForm";
			Text = "Fixed Asset Class";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountClassDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxPLAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAssetAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccumulatedAccount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
