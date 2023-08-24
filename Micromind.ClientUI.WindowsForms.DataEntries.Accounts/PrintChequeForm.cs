using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class PrintChequeForm : Form, IForm
	{
		private DataRow currentChequeRow;

		private DataSet currentData;

		private int currentChequeID = -1;

		private const string TABLENAME_CONST = "GL_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string selectedChequeID = "";

		private bool allowChangePayeeName = CompanyPreferences.AllowChangePayeeNameInChequePrinting;

		private ScreenAccessRight screenRight;

		private string chequebookID = "";

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonPrint;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private XPButton buttonPreview;

		private Label label4;

		private TextBox textBoxStatus;

		private Label label9;

		private MMLabel mmLabel2;

		private TextBox textBoxChequeNumber;

		private TextBox textBoxBankName;

		private UltraGroupBox ultraGroupBox1;

		private XPButton buttonSelectCheque;

		private TextBox textBoxPayee;

		private Label label1;

		private Label label2;

		private TextBox textBoxAmount;

		private TextBox textBoxDate;

		private Label label3;

		private Label label5;

		private TextBox textBoxTemplate;

		private XPButton xpButton2;

		private Label label6;

		private TextBox textBoxPrintName;

		private TextBox textBoxStamp1;

		private Label label7;

		private CheckBox checkBoxStamp1;

		private TextBox textBoxStamp2;

		private CheckBox checkBoxStamp2;

		private OpenFileDialog openFileDialog;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1031;

		public ScreenTypes ScreenType => ScreenTypes.Other;

		private string SystemDocID => "ICR01";

		public PrintChequeForm()
		{
			InitializeComponent();
			AddEvents();
			textBoxPrintName.ReadOnly = !allowChangePayeeName;
		}

		private void AddEvents()
		{
			base.Load += TransactionLeavesForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null)
				{
					return false;
				}
				if (!currentData.Tables["Cheque_Issued"].Columns.Contains("AmountInWords"))
				{
					DataColumn dataColumn = new DataColumn("AmountInWords", typeof(string), "");
					dataColumn.MaxLength = 500;
					currentData.Tables["Cheque_Issued"].Columns.Add(dataColumn);
				}
				DataRow dataRow = currentData.Tables["Cheque_Issued"].Rows[0];
				if (checkBoxStamp1.Checked)
				{
					dataRow["Stamp1"] = textBoxStamp1.Text;
				}
				else
				{
					dataRow["Stamp1"] = "";
				}
				if (checkBoxStamp2.Checked)
				{
					dataRow["Stamp2"] = textBoxStamp2.Text;
				}
				else
				{
					dataRow["Stamp2"] = "";
				}
				if (textBoxPrintName.Text.Trim() != "")
				{
					dataRow["NameOnCheck"] = textBoxPrintName.Text;
				}
				else
				{
					dataRow["NameOnCheck"] = textBoxPayee.Text;
				}
				int curDecimalPoints = Global.CurDecimalPoints;
				dataRow["AmountInWords"] = NumToWord.GetNumInWords(decimal.Parse(textBoxAmount.Text), curDecimalPoints);
				dataRow.EndEdit();
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			try
			{
				textBoxBankName.Clear();
				textBoxStatus.Clear();
				textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDate.Clear();
				textBoxPayee.Clear();
				textBoxTemplate.Clear();
				textBoxChequeNumber.Clear();
				textBoxPrintName.Clear();
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
			return true;
		}

		private void TransactionLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
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

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void comboBoxBank_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxReason_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void buttonSelectCheque_Click(object sender, EventArgs e)
		{
			try
			{
				SelectChequeToPrintDialog selectChequeToPrintDialog = new SelectChequeToPrintDialog();
				if (selectChequeToPrintDialog.ShowDialog() == DialogResult.OK)
				{
					string text = selectChequeToPrintDialog.SelectedRow.Cells["ChequeID"].Value.ToString();
					bool isSecurityChq = bool.Parse(selectChequeToPrintDialog.SelectedRow.Cells["IsSecurityChq"].Value.ToString());
					currentChequeID = int.Parse(text);
					DataRow dataRow = currentChequeRow = (currentData = Factory.IssuedChequeSystem.GetChequeIssuedandSecirutyChqByID(text, isSecurityChq)).Tables["Cheque_Issued"].Rows[0];
					textBoxChequeNumber.Text = dataRow["ChequeNumber"].ToString();
					textBoxBankName.Text = dataRow["ChequebookName"].ToString();
					textBoxPayee.Text = dataRow["PayeeName"].ToString();
					if (!string.IsNullOrWhiteSpace(dataRow["PrintName"].ToString()))
					{
						textBoxPrintName.Text = dataRow["PrintName"].ToString();
					}
					else
					{
						textBoxPrintName.Text = dataRow["NameOnCheck"].ToString();
					}
					bool result = false;
					bool.TryParse(dataRow["IsPrinted"].ToString(), out result);
					if (result)
					{
						textBoxPrintName.Enabled = false;
					}
					else
					{
						textBoxPrintName.Enabled = true;
					}
					textBoxTemplate.Text = dataRow["TemplateName"].ToString();
					if (!string.IsNullOrEmpty(dataRow["ChequeDate"].ToString()))
					{
						textBoxDate.Text = DateTime.Parse(dataRow["ChequeDate"].ToString()).ToShortDateString();
					}
					textBoxAmount.Text = decimal.Parse(dataRow["Amount"].ToString()).ToString(Format.TotalAmountFormat);
					if (!string.IsNullOrEmpty(dataRow["Status"].ToString()))
					{
						IssuedChequeStatus issuedChequeStatus = (IssuedChequeStatus)byte.Parse(dataRow["Status"].ToString());
						textBoxStatus.Text = issuedChequeStatus.ToString();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void PrintChequeForm_Load(object sender, EventArgs e)
		{
		}

		private void checkBoxStamp1_CheckedChanged(object sender, EventArgs e)
		{
			textBoxStamp1.Enabled = checkBoxStamp1.Checked;
		}

		private void checkBoxStamp2_CheckedChanged(object sender, EventArgs e)
		{
			textBoxStamp2.Enabled = checkBoxStamp2.Checked;
		}

		private bool ValidateData()
		{
			if (textBoxChequeNumber.Text == "")
			{
				ErrorHelper.InformationMessage("Please select a cheque to print.");
				return false;
			}
			return true;
		}

		private void buttonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true);
		}

		private void Print(bool isPrint, bool showPrintDialog)
		{
			try
			{
				if (ValidateData() && GetData())
				{
					if (currentData == null || currentData.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Please select a cheque to print.");
					}
					else
					{
						string templateName = textBoxTemplate.Text.Substring(checked(textBoxTemplate.Text.LastIndexOf("\\") + 1));
						PrintHelper.PrintCheque(currentData, templateName, isPrint, showPrintDialog);
						if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Is your cheque printed successfully and mark it as printed?") == DialogResult.Yes && Factory.IssuedChequeSystem.MarkAsPrinted(currentChequeID) && Factory.DatabaseSystem.UpdateFieldValue("Cheque_Issued", "PrintName", textBoxPrintName.Text, typeof(string), "ChequeID", currentChequeID) > 0)
						{
							ClearForm();
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: true);
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			openFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\Print Templates\\Cheques";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxTemplate.Text = openFileDialog.FileName.Substring(checked(Path.GetDirectoryName(Application.ExecutablePath).Length + 1));
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.PrintChequeForm));
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
			buttonPreview = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonPrint = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			buttonSelectCheque = new Micromind.UISupport.XPButton();
			textBoxChequeNumber = new System.Windows.Forms.TextBox();
			textBoxPayee = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxBankName = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBoxStatus = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBoxAmount = new System.Windows.Forms.TextBox();
			textBoxDate = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxTemplate = new System.Windows.Forms.TextBox();
			xpButton2 = new Micromind.UISupport.XPButton();
			label6 = new System.Windows.Forms.Label();
			textBoxPrintName = new System.Windows.Forms.TextBox();
			textBoxStamp1 = new System.Windows.Forms.TextBox();
			label7 = new System.Windows.Forms.Label();
			checkBoxStamp1 = new System.Windows.Forms.CheckBox();
			textBoxStamp2 = new System.Windows.Forms.TextBox();
			checkBoxStamp2 = new System.Windows.Forms.CheckBox();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
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
			toolStripButtonPrint.Size = new System.Drawing.Size(52, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(23, 22);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonPrevious.Image");
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(23, 22);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(23, 22);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(23, 22);
			toolStripButtonLast.Text = "Last Record";
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(50, 22);
			toolStripButtonFind.Text = "Find";
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			panelButtons.Controls.Add(buttonPreview);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonPrint);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 189);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(580, 40);
			panelButtons.TabIndex = 9;
			buttonPreview.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPreview.BackColor = System.Drawing.Color.DarkGray;
			buttonPreview.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPreview.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPreview.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPreview.Location = new System.Drawing.Point(111, 8);
			buttonPreview.Name = "buttonPreview";
			buttonPreview.Size = new System.Drawing.Size(96, 24);
			buttonPreview.TabIndex = 1;
			buttonPreview.Text = "Preview";
			buttonPreview.UseVisualStyleBackColor = false;
			buttonPreview.Click += new System.EventHandler(buttonPreview_Click);
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
			xpButton1.TabIndex = 2;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPrint.BackColor = System.Drawing.Color.Silver;
			buttonPrint.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPrint.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPrint.Location = new System.Drawing.Point(9, 8);
			buttonPrint.Name = "buttonPrint";
			buttonPrint.Size = new System.Drawing.Size(96, 24);
			buttonPrint.TabIndex = 0;
			buttonPrint.Text = "&Print";
			buttonPrint.UseVisualStyleBackColor = false;
			buttonPrint.Click += new System.EventHandler(buttonPrint_Click);
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
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel2);
			ultraGroupBox1.Controls.Add(buttonSelectCheque);
			ultraGroupBox1.Controls.Add(textBoxChequeNumber);
			ultraGroupBox1.Controls.Add(textBoxPayee);
			ultraGroupBox1.Controls.Add(label1);
			ultraGroupBox1.Controls.Add(textBoxBankName);
			ultraGroupBox1.Controls.Add(label4);
			ultraGroupBox1.Location = new System.Drawing.Point(9, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(571, 71);
			ultraGroupBox1.TabIndex = 3;
			ultraGroupBox1.Text = "Select the cheque to print";
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
			buttonSelectCheque.TabIndex = 1;
			buttonSelectCheque.Text = "...";
			buttonSelectCheque.UseVisualStyleBackColor = false;
			buttonSelectCheque.Click += new System.EventHandler(buttonSelectCheque_Click);
			textBoxChequeNumber.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxChequeNumber.Location = new System.Drawing.Point(84, 25);
			textBoxChequeNumber.Name = "textBoxChequeNumber";
			textBoxChequeNumber.ReadOnly = true;
			textBoxChequeNumber.Size = new System.Drawing.Size(90, 20);
			textBoxChequeNumber.TabIndex = 0;
			textBoxChequeNumber.TabStop = false;
			textBoxPayee.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayee.Location = new System.Drawing.Point(84, 48);
			textBoxPayee.Name = "textBoxPayee";
			textBoxPayee.ReadOnly = true;
			textBoxPayee.Size = new System.Drawing.Size(473, 20);
			textBoxPayee.TabIndex = 3;
			textBoxPayee.TabStop = false;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 51);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(71, 13);
			label1.TabIndex = 131;
			label1.Text = "Payee Name:";
			textBoxBankName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankName.Location = new System.Drawing.Point(279, 25);
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.ReadOnly = true;
			textBoxBankName.Size = new System.Drawing.Size(278, 20);
			textBoxBankName.TabIndex = 2;
			textBoxBankName.TabStop = false;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(206, 29);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(71, 13);
			label4.TabIndex = 131;
			label4.Text = "Chequebook:";
			textBoxStatus.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxStatus.Location = new System.Drawing.Point(450, 104);
			textBoxStatus.Name = "textBoxStatus";
			textBoxStatus.ReadOnly = true;
			textBoxStatus.Size = new System.Drawing.Size(116, 20);
			textBoxStatus.TabIndex = 3;
			textBoxStatus.TabStop = false;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(410, 108);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(40, 13);
			label9.TabIndex = 137;
			label9.Text = "Status:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(215, 107);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(46, 13);
			label2.TabIndex = 131;
			label2.Text = "Amount:";
			textBoxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmount.Location = new System.Drawing.Point(288, 104);
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.ReadOnly = true;
			textBoxAmount.Size = new System.Drawing.Size(116, 20);
			textBoxAmount.TabIndex = 2;
			textBoxAmount.TabStop = false;
			textBoxDate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDate.Location = new System.Drawing.Point(93, 104);
			textBoxDate.Name = "textBoxDate";
			textBoxDate.ReadOnly = true;
			textBoxDate.Size = new System.Drawing.Size(116, 20);
			textBoxDate.TabIndex = 1;
			textBoxDate.TabStop = false;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 107);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 140;
			label3.Text = "Date:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(12, 130);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(54, 13);
			label5.TabIndex = 131;
			label5.Text = "Template:";
			textBoxTemplate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTemplate.Location = new System.Drawing.Point(93, 127);
			textBoxTemplate.Name = "textBoxTemplate";
			textBoxTemplate.ReadOnly = true;
			textBoxTemplate.Size = new System.Drawing.Size(311, 20);
			textBoxTemplate.TabIndex = 4;
			textBoxTemplate.TabStop = false;
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(410, 127);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(25, 20);
			xpButton2.TabIndex = 142;
			xpButton2.Text = "...";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Click += new System.EventHandler(xpButton2_Click);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(12, 85);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(62, 13);
			label6.TabIndex = 131;
			label6.Text = "Print Name:";
			textBoxPrintName.Location = new System.Drawing.Point(93, 82);
			textBoxPrintName.Name = "textBoxPrintName";
			textBoxPrintName.ReadOnly = true;
			textBoxPrintName.Size = new System.Drawing.Size(473, 20);
			textBoxPrintName.TabIndex = 0;
			textBoxPrintName.TabStop = false;
			textBoxStamp1.BackColor = System.Drawing.Color.White;
			textBoxStamp1.Enabled = false;
			textBoxStamp1.Location = new System.Drawing.Point(111, 149);
			textBoxStamp1.Name = "textBoxStamp1";
			textBoxStamp1.Size = new System.Drawing.Size(141, 20);
			textBoxStamp1.TabIndex = 6;
			textBoxStamp1.Text = "A/C PAYEE ONLY";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(12, 152);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(45, 13);
			label7.TabIndex = 131;
			label7.Text = "Stamps:";
			checkBoxStamp1.AutoSize = true;
			checkBoxStamp1.Location = new System.Drawing.Point(93, 152);
			checkBoxStamp1.Name = "checkBoxStamp1";
			checkBoxStamp1.Size = new System.Drawing.Size(15, 14);
			checkBoxStamp1.TabIndex = 5;
			checkBoxStamp1.UseVisualStyleBackColor = true;
			checkBoxStamp1.CheckedChanged += new System.EventHandler(checkBoxStamp1_CheckedChanged);
			textBoxStamp2.BackColor = System.Drawing.Color.White;
			textBoxStamp2.Enabled = false;
			textBoxStamp2.Location = new System.Drawing.Point(292, 149);
			textBoxStamp2.Name = "textBoxStamp2";
			textBoxStamp2.Size = new System.Drawing.Size(141, 20);
			textBoxStamp2.TabIndex = 8;
			textBoxStamp2.Text = "NON NEGOTIABLE";
			checkBoxStamp2.AutoSize = true;
			checkBoxStamp2.Location = new System.Drawing.Point(274, 152);
			checkBoxStamp2.Name = "checkBoxStamp2";
			checkBoxStamp2.Size = new System.Drawing.Size(15, 14);
			checkBoxStamp2.TabIndex = 7;
			checkBoxStamp2.UseVisualStyleBackColor = true;
			checkBoxStamp2.CheckedChanged += new System.EventHandler(checkBoxStamp2_CheckedChanged);
			openFileDialog.AddExtension = false;
			openFileDialog.DefaultExt = "repx";
			openFileDialog.Filter = "Print Templates|*.repx";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(580, 229);
			base.Controls.Add(checkBoxStamp2);
			base.Controls.Add(checkBoxStamp1);
			base.Controls.Add(xpButton2);
			base.Controls.Add(textBoxStamp2);
			base.Controls.Add(textBoxStamp1);
			base.Controls.Add(textBoxDate);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxPrintName);
			base.Controls.Add(label6);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(textBoxTemplate);
			base.Controls.Add(label7);
			base.Controls.Add(label5);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(textBoxAmount);
			base.Controls.Add(label2);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(textBoxStatus);
			base.Controls.Add(label9);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(588, 238);
			base.Name = "PrintChequeForm";
			Text = "Print Cheque";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(PrintChequeForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
