using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class VoidBlankChequeForm : Form, IForm
	{
		private DataRow currentChequeRow;

		private TransactionData currentData;

		private const string TABLENAME_CONST = "GL_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string selectedChequeID = "";

		private ScreenAccessRight screenRight;

		private bool isVoid;

		private string chequebookID = "";

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label label4;

		private ReturnedChequeReasonComboBox comboBoxReason;

		private TextBox textBoxNote;

		private Label label3;

		private Label labelVoided;

		private Label label6;

		private TextBox textBoxReasonName;

		private TextBox textBoxStatus;

		private Label label9;

		private MMLabel mmLabel2;

		private TextBox textBoxChequeNumber;

		private TextBox textBoxBankName;

		private UltraGroupBox ultraGroupBox1;

		private XPButton buttonSelectCheque;

		private ComboBox comboBoxAction;

		private Label label1;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1031;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => "ICR01";

		private bool IsDirty
		{
			get
			{
				if (IsVoid)
				{
					return false;
				}
				return formManager.GetDirtyStatus();
			}
		}

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
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
					xPButton.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
				}
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
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

		private bool IsVoid
		{
			get
			{
				return isVoid;
			}
			set
			{
				if (isVoid != value)
				{
					isVoid = value;
					buttonSave.Enabled = !value;
					panelDetails.Enabled = !value;
					labelVoided.Visible = value;
					if (value)
					{
						buttonVoid.Text = UIMessages.Unvoid;
					}
					else
					{
						buttonVoid.Text = UIMessages.Void;
					}
				}
			}
		}

		public VoidBlankChequeForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += TransactionLeavesForm_Load;
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

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		public void LoadData(string journalID)
		{
			try
			{
				if (!(journalID.Trim() == "") && CanClose())
				{
					currentData = Factory.TransactionSystem.GetTransactionByID(SystemDocID, journalID);
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
				ClearForm();
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["GL_Transaction"].Rows[0];
					textBoxNote.Text = dataRow["Description"].ToString();
					if (dataRow["IsVoid"] != DBNull.Value)
					{
						IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
					}
					else
					{
						IsVoid = false;
					}
					if (currentData.Tables.Contains("Transaction_Details"))
					{
						_ = currentData.TransactionDetailsTable.Rows.Count;
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
			if (ErrorHelper.QuestionMessageYesNo("You are going to void a blank cheque.", "Are you sure you want to continue?") == DialogResult.No)
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag = Factory.IssuedChequeSystem.VoidBlankCheque(chequebookID, textBoxChequeNumber.Text, comboBoxReason.SelectedID, textBoxNote.Text, comboBoxAction.SelectedIndex == 0);
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
			catch (CompanyException ex)
			{
				if (ex.Number == 1003)
				{
					ErrorHelper.ErrorMessage(UIMessages.ReturnedChequeError);
				}
				return false;
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
			if (textBoxChequeNumber.Text == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			IssuedChequeStatus issuedChequeStatus = (IssuedChequeStatus)byte.Parse(currentChequeRow["Status"].ToString());
			if (issuedChequeStatus == IssuedChequeStatus.Voided && comboBoxAction.SelectedIndex == 0)
			{
				ErrorHelper.WarningMessage("This cheque is already voided.");
				textBoxChequeNumber.Focus();
				return false;
			}
			if (issuedChequeStatus == IssuedChequeStatus.Blank && comboBoxAction.SelectedIndex == 1)
			{
				ErrorHelper.WarningMessage("This cheque is not voided.");
				textBoxChequeNumber.Focus();
				return false;
			}
			if (currentChequeRow["ChequeID"] != DBNull.Value)
			{
				ErrorHelper.WarningMessage("This cheque is already in use.");
				textBoxChequeNumber.Focus();
				return false;
			}
			return true;
		}

		private decimal GetTransactionBalance()
		{
			return 0m;
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
			try
			{
				textBoxNote.Clear();
				comboBoxReason.Clear();
				textBoxBankName.Clear();
				textBoxStatus.Clear();
				textBoxReasonName.Clear();
				textBoxChequeNumber.Clear();
				IsVoid = false;
				formManager.ResetDirty();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void TransactionLeaveGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void TransactionLeaveGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
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

		private void TransactionLeavesForm_Load(object sender, EventArgs e)
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
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(SystemDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (Void(!IsVoid))
			{
				IsVoid = !IsVoid;
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				DialogResult dialogResult = (!isVoid) ? ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid) : ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void comboBoxBank_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxReason_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxReasonName.Text = comboBoxReason.SelectedName;
		}

		private void buttonSelectCheque_Click(object sender, EventArgs e)
		{
			try
			{
				SelectChequeDialog selectChequeDialog = new SelectChequeDialog();
				selectChequeDialog.DataSource = Factory.IssuedChequeSystem.GetRegisteredBlankChequeList();
				selectChequeDialog.Grid.DisplayLayout.Bands[0].Columns["ChequeID"].Hidden = true;
				selectChequeDialog.Grid.DisplayLayout.Bands[0].Columns["ChequebookID"].Hidden = true;
				selectChequeDialog.Grid.DisplayLayout.Bands[0].Columns["Cheque #"].Width = 75;
				selectChequeDialog.Grid.DisplayLayout.Bands[0].Columns["Chequebook"].Width = 121;
				if (selectChequeDialog.ShowDialog() == DialogResult.OK)
				{
					string text = chequebookID = selectChequeDialog.SelectedRow.Cells["ChequebookID"].Value.ToString();
					string chequeNumber = selectChequeDialog.SelectedRow.Cells["Cheque #"].Value.ToString();
					DataSet blankCheque = Factory.IssuedChequeSystem.GetBlankCheque(text, chequeNumber);
					DataRow dataRow = currentChequeRow = blankCheque.Tables[0].Rows[0];
					textBoxChequeNumber.Text = dataRow["ChequeNumber"].ToString();
					textBoxBankName.Text = dataRow["ChequebookName"].ToString();
					IssuedChequeStatus issuedChequeStatus = (IssuedChequeStatus)byte.Parse(dataRow["Status"].ToString());
					textBoxStatus.Text = issuedChequeStatus.ToString();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void VoidBlankChequeForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxAction.Items.Clear();
				comboBoxAction.Items.Add("Void");
				comboBoxAction.Items.Add("Unvoid");
				comboBoxAction.SelectedIndex = 0;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.VoidBlankChequeForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			panelDetails = new System.Windows.Forms.Panel();
			comboBoxAction = new System.Windows.Forms.ComboBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			buttonSelectCheque = new Micromind.UISupport.XPButton();
			textBoxChequeNumber = new System.Windows.Forms.TextBox();
			textBoxBankName = new System.Windows.Forms.TextBox();
			textBoxStatus = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			comboBoxReason = new Micromind.DataControls.ReturnedChequeReasonComboBox();
			textBoxReasonName = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			labelVoided = new System.Windows.Forms.Label();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxReason).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[9]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(592, 25);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.Visible = false;
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 22);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 22);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 22);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 22);
			toolStripButtonLast.Text = "Last Record";
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(55, 22);
			toolStripButtonFind.Text = "Find";
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 189);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(580, 40);
			panelButtons.TabIndex = 1;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(213, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 2;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Visible = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(315, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(111, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(580, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(470, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(9, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
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
			panelDetails.Controls.Add(comboBoxAction);
			panelDetails.Controls.Add(ultraGroupBox1);
			panelDetails.Controls.Add(comboBoxReason);
			panelDetails.Controls.Add(textBoxReasonName);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(labelVoided);
			panelDetails.Location = new System.Drawing.Point(5, 5);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(563, 178);
			panelDetails.TabIndex = 0;
			comboBoxAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxAction.FormattingEnabled = true;
			comboBoxAction.Location = new System.Drawing.Point(87, 3);
			comboBoxAction.Name = "comboBoxAction";
			comboBoxAction.Size = new System.Drawing.Size(90, 21);
			comboBoxAction.TabIndex = 0;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel2);
			ultraGroupBox1.Controls.Add(buttonSelectCheque);
			ultraGroupBox1.Controls.Add(textBoxChequeNumber);
			ultraGroupBox1.Controls.Add(textBoxBankName);
			ultraGroupBox1.Controls.Add(textBoxStatus);
			ultraGroupBox1.Controls.Add(label9);
			ultraGroupBox1.Controls.Add(label4);
			ultraGroupBox1.Location = new System.Drawing.Point(3, 83);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(557, 71);
			ultraGroupBox1.TabIndex = 3;
			ultraGroupBox1.Text = "Select the cheque to void";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(3, 28);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(80, 13);
			mmLabel2.TabIndex = 2;
			mmLabel2.Text = "Chq Number:";
			buttonSelectCheque.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectCheque.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectCheque.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectCheque.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectCheque.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSelectCheque.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectCheque.Location = new System.Drawing.Point(175, 25);
			buttonSelectCheque.Name = "buttonSelectCheque";
			buttonSelectCheque.Size = new System.Drawing.Size(25, 20);
			buttonSelectCheque.TabIndex = 0;
			buttonSelectCheque.Text = "...";
			buttonSelectCheque.UseVisualStyleBackColor = false;
			buttonSelectCheque.Click += new System.EventHandler(buttonSelectCheque_Click);
			textBoxChequeNumber.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxChequeNumber.Location = new System.Drawing.Point(84, 25);
			textBoxChequeNumber.Name = "textBoxChequeNumber";
			textBoxChequeNumber.ReadOnly = true;
			textBoxChequeNumber.Size = new System.Drawing.Size(90, 20);
			textBoxChequeNumber.TabIndex = 140;
			textBoxChequeNumber.TabStop = false;
			textBoxBankName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankName.Location = new System.Drawing.Point(279, 25);
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.ReadOnly = true;
			textBoxBankName.Size = new System.Drawing.Size(278, 20);
			textBoxBankName.TabIndex = 139;
			textBoxBankName.TabStop = false;
			textBoxStatus.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxStatus.Location = new System.Drawing.Point(84, 47);
			textBoxStatus.Name = "textBoxStatus";
			textBoxStatus.ReadOnly = true;
			textBoxStatus.Size = new System.Drawing.Size(116, 20);
			textBoxStatus.TabIndex = 138;
			textBoxStatus.TabStop = false;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(3, 50);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(40, 13);
			label9.TabIndex = 137;
			label9.Text = "Status:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(206, 29);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(71, 13);
			label4.TabIndex = 131;
			label4.Text = "Chequebook:";
			comboBoxReason.Assigned = false;
			comboBoxReason.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxReason.CustomReportFieldName = "";
			comboBoxReason.CustomReportKey = "";
			comboBoxReason.CustomReportValueType = 1;
			comboBoxReason.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxReason.DisplayLayout.Appearance = appearance;
			comboBoxReason.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxReason.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReason.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxReason.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReason.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxReason.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxReason.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxReason.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxReason.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxReason.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxReason.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxReason.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxReason.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxReason.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxReason.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxReason.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxReason.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxReason.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxReason.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxReason.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxReason.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxReason.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxReason.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxReason.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxReason.Editable = true;
			comboBoxReason.FilterString = "";
			comboBoxReason.HasAllAccount = false;
			comboBoxReason.HasCustom = false;
			comboBoxReason.IsDataLoaded = false;
			comboBoxReason.Location = new System.Drawing.Point(87, 26);
			comboBoxReason.MaxDropDownItems = 12;
			comboBoxReason.Name = "comboBoxReason";
			comboBoxReason.ShowInactiveItems = false;
			comboBoxReason.ShowQuickAdd = true;
			comboBoxReason.Size = new System.Drawing.Size(90, 20);
			comboBoxReason.TabIndex = 1;
			comboBoxReason.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxReason.SelectedIndexChanged += new System.EventHandler(comboBoxReason_SelectedIndexChanged);
			textBoxReasonName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxReasonName.Location = new System.Drawing.Point(180, 26);
			textBoxReasonName.Name = "textBoxReasonName";
			textBoxReasonName.ReadOnly = true;
			textBoxReasonName.Size = new System.Drawing.Size(380, 20);
			textBoxReasonName.TabIndex = 118;
			textBoxReasonName.TabStop = false;
			textBoxNote.Location = new System.Drawing.Point(87, 48);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(473, 20);
			textBoxNote.TabIndex = 2;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 6);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(40, 13);
			label1.TabIndex = 20;
			label1.Text = "Action:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(3, 30);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(47, 13);
			label6.TabIndex = 20;
			label6.Text = "Reason:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 51);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			labelVoided.BackColor = System.Drawing.Color.Transparent;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(1003, 91);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(91, 27);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(580, 229);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(588, 238);
			base.Name = "VoidBlankChequeForm";
			Text = "Void Blank Cheque";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(VoidBlankChequeForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxReason).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
