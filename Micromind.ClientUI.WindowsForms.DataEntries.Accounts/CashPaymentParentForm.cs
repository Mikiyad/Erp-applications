using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class CashPaymentParentForm : Form, IForm, IWorkFlowForm
	{
		private IWorkFlowForm multiChequeForm;

		private IWorkFlowForm multiPayeeForm;

		private RegisterData currentData;

		private const string TABLENAME_CONST = "GL_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private Panel panel1;

		private RadioButton radioButtonMultiple;

		private RadioButton radioButtonSingle;

		private Panel panelMultiPayee;

		private Panel panelMultiCheque;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripSeparator toolStripSeparator5;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1027;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => false;

		private ITransactionForm SelectedForm
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					if (panelMultiCheque.Controls.Count == 0)
					{
						return null;
					}
					return panelMultiCheque.Controls[0] as ITransactionForm;
				}
				if (panelMultiPayee.Controls.Count == 0)
				{
					return null;
				}
				return panelMultiPayee.Controls[0] as ITransactionForm;
			}
		}

		public CashPaymentParentForm()
		{
			InitializeComponent();
			AddEvents();
			LoadForm();
			ShowSelectedForm();
		}

		private void AddEvents()
		{
			base.Load += CashPaymentParentForm_Load;
			EventHelper.FormCleared += EventHelper_FormCleared;
		}

		private void EventHelper_FormCleared(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			if (selectedForm != null)
			{
				if (selectedForm.IsNewRecord)
				{
					RadioButton radioButton = radioButtonMultiple;
					bool enabled = radioButtonSingle.Enabled = true;
					radioButton.Enabled = enabled;
				}
				else
				{
					RadioButton radioButton2 = radioButtonMultiple;
					bool enabled = radioButtonSingle.Enabled = false;
					radioButton2.Enabled = enabled;
				}
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed)
				{
					ITransactionForm selectedForm = SelectedForm;
					string systemDocID = selectedForm.SystemDocID;
					object fieldValue = Factory.DatabaseSystem.GetFieldValue("GL_Transaction", "IsSecondForm", "SysDocID", selectedForm.SystemDocID, "VoucherID", id);
					bool flag = false;
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						flag = Convert.ToBoolean(fieldValue.ToString());
					}
					if (!flag)
					{
						radioButtonMultiple.Checked = true;
					}
					else
					{
						radioButtonSingle.Checked = true;
					}
					selectedForm = SelectedForm;
					selectedForm.SystemDocID = systemDocID;
					selectedForm.LoadData(id);
					if (!selectedForm.IsNewRecord)
					{
						RadioButton radioButton = radioButtonSingle;
						bool enabled = radioButtonMultiple.Enabled = false;
						radioButton.Enabled = enabled;
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			try
			{
				_ = SelectedForm;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("GL_Transaction", "IsSecondForm", "SysDocID", sysDocID, "VoucherID", voucherID);
				bool flag = false;
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag = Convert.ToBoolean(fieldValue.ToString());
				}
				if (!flag)
				{
					radioButtonMultiple.Checked = true;
				}
				else
				{
					radioButtonSingle.Checked = true;
				}
				ITransactionForm selectedForm = SelectedForm;
				selectedForm.SystemDocID = sysDocID;
				selectedForm.EditDocument(sysDocID, voucherID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void RegisterGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void RegisterGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
		}

		public void SetApprovalStatus()
		{
			if (isNewRecord)
			{
				ToolStripButton toolStripButton = toolStripButtonApproval;
				ToolStripLabel toolStripLabel = toolStripLabelApproval;
				bool flag2 = toolStripSeparatorApproval.Visible = false;
				bool visible = toolStripLabel.Visible = flag2;
				toolStripButton.Visible = visible;
			}
			else
			{
				if (currentData == null || currentData.Tables[0].Rows.Count <= 0)
				{
					return;
				}
				DataRow dataRow = currentData.Tables[0].Rows[0];
				bool flag2;
				bool visible;
				if (!dataRow.Table.Columns.Contains("ApprovalStatus") || dataRow["ApprovalStatus"].IsDBNullOrEmpty())
				{
					ToolStripButton toolStripButton2 = toolStripButtonApproval;
					ToolStripLabel toolStripLabel2 = toolStripLabelApproval;
					flag2 = (toolStripSeparatorApproval.Visible = false);
					visible = (toolStripLabel2.Visible = flag2);
					toolStripButton2.Visible = visible;
					return;
				}
				switch (int.Parse(dataRow["ApprovalStatus"].ToString()))
				{
				case 3:
					toolStripButtonApproval.Text = "Rejected";
					toolStripButtonApproval.ForeColor = Color.Red;
					break;
				case 10:
					toolStripButtonApproval.Text = "Approved";
					toolStripButtonApproval.ForeColor = Color.ForestGreen;
					break;
				default:
					toolStripButtonApproval.Text = "Pending";
					toolStripButtonApproval.ForeColor = Color.Orange;
					break;
				}
				ToolStripButton toolStripButton3 = toolStripButtonApproval;
				ToolStripLabel toolStripLabel3 = toolStripLabelApproval;
				flag2 = (toolStripSeparatorApproval.Visible = true);
				visible = (toolStripLabel3.Visible = flag2);
				toolStripButton3.Visible = visible;
			}
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			toolStrip1.Enabled = false;
			if (radioButtonSingle.Checked)
			{
				multiChequeForm.ShowForApproval(sysDocID, voucherID, approvalTaskID);
			}
			else
			{
				multiPayeeForm.ShowForApproval(sysDocID, voucherID, approvalTaskID);
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			string nextID = DatabaseHelper.GetNextID("GL_Transaction", "VoucherID", selectedForm.VoucherID, "SysDocID", selectedForm.SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			string previousID = DatabaseHelper.GetPreviousID("GL_Transaction", "VoucherID", selectedForm.VoucherID, "SysDocID", selectedForm.SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			string lastID = DatabaseHelper.GetLastID("GL_Transaction", "VoucherID", "SysDocID", selectedForm.SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			string firstID = DatabaseHelper.GetFirstID("GL_Transaction", "VoucherID", "SysDocID", selectedForm.SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
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
				else
				{
					ITransactionForm selectedForm = SelectedForm;
					string text = Factory.DatabaseSystem.FindDocumentByNumber("GL_Transaction", "VoucherID", selectedForm.SystemDocID, toolStripTextBoxFind.Text);
					if (text != "")
					{
						LoadData(text);
					}
					else
					{
						ErrorHelper.InformationMessage(UIMessages.DocumentNotFound);
						toolStripTextBoxFind.SelectAll();
						toolStripTextBoxFind.Focus();
					}
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
			return SelectedForm?.CanClose() ?? true;
		}

		private void CashPaymentParentForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				_ = base.IsDisposed;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight("CashExpenseEntryForm");
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			if (radioButtonMultiple.Checked)
			{
				FormActivator.SelectedSysDocID = selectedForm.SystemDocID;
				FormActivator.BringFormToFront(FormActivator.ExpenseListFormObj);
			}
			else
			{
				FormActivator.SelectedSysDocID = selectedForm.SystemDocID;
				FormActivator.BringFormToFront(FormActivator.PaymentVoucherListObj);
			}
		}

		private void comboBoxCashAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxPDCReceived_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxPDCIssued_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxCardReceived_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonMultiCheque_CheckedChanged(object sender, EventArgs e)
		{
			ShowSelectedForm();
		}

		private void ShowSelectedForm()
		{
			if (radioButtonSingle.Checked)
			{
				panelMultiCheque.Visible = true;
				panelMultiPayee.Visible = false;
				base.Height = 500;
			}
			else
			{
				panelMultiCheque.Visible = false;
				panelMultiPayee.Visible = true;
				base.Height = 587;
			}
		}

		private void LoadForm()
		{
			CashPaymentForm cashPaymentForm = new CashPaymentForm();
			cashPaymentForm.FormBorderStyle = FormBorderStyle.None;
			cashPaymentForm.TopLevel = false;
			panelMultiCheque.Controls.Add(cashPaymentForm);
			cashPaymentForm.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			multiChequeForm = cashPaymentForm;
			cashPaymentForm.Show();
			cashPaymentForm.Size = panelMultiCheque.Size;
			CashExpenseEntryForm cashExpenseEntryForm = new CashExpenseEntryForm();
			cashExpenseEntryForm.FormBorderStyle = FormBorderStyle.None;
			cashExpenseEntryForm.TopLevel = false;
			panelMultiPayee.Controls.Add(cashExpenseEntryForm);
			cashExpenseEntryForm.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			cashExpenseEntryForm.Show();
			cashExpenseEntryForm.Size = panelMultiPayee.Size;
		}

		private void radioButtonMultiPayee_CheckedChanged(object sender, EventArgs e)
		{
			ShowSelectedForm();
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				ITransactionForm selectedForm = SelectedForm;
				if (!selectedForm.IsNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = selectedForm.VoucherID;
					docManagementForm.EntitySysDocID = selectedForm.SystemDocID;
					docManagementForm.EntityName = selectedForm.SystemDocID;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = selectedForm.VoucherID;
			journalDistibutionDialog.SysDocID = selectedForm.SystemDocID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			if (!selectedForm.IsNewRecord)
			{
				selectedForm.Preview();
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			if (!selectedForm.IsNewRecord)
			{
				selectedForm.Print();
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			ITransactionForm selectedForm = SelectedForm;
			FormHelper.ShowDocumentInfo(selectedForm.VoucherID, selectedForm.SystemDocID, this);
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to copy this document?") == DialogResult.Yes)
			{
				SelectedForm.DuplicateData();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.CashPaymentParentForm));
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
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
			toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
			toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			panel1 = new System.Windows.Forms.Panel();
			radioButtonMultiple = new System.Windows.Forms.RadioButton();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			panelMultiPayee = new System.Windows.Forms.Panel();
			panelMultiCheque = new System.Windows.Forms.Panel();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[21]
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
				toolStripButtonAttach,
				toolStripSeparator5,
				toolStripButton1,
				toolStripButtonPreview,
				toolStripButtonDistribution,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(862, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
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
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 28);
			toolStripButton1.Text = "&Print";
			toolStripButton1.ToolTipText = "Print (Ctrl+P)";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonApproval.AutoSize = false;
			toolStripButtonApproval.BackColor = System.Drawing.Color.Transparent;
			toolStripButtonApproval.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			toolStripButtonApproval.ForeColor = System.Drawing.Color.Green;
			toolStripButtonApproval.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			toolStripButtonApproval.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonApproval.Name = "toolStripButtonApproval";
			toolStripButtonApproval.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			toolStripButtonApproval.Size = new System.Drawing.Size(70, 22);
			toolStripButtonApproval.Text = "Pending";
			toolStripLabelApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripLabelApproval.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			toolStripLabelApproval.Name = "toolStripLabelApproval";
			toolStripLabelApproval.Size = new System.Drawing.Size(45, 28);
			toolStripLabelApproval.Text = "Status:";
			toolStripSeparatorApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripSeparatorApproval.Name = "toolStripSeparatorApproval";
			toolStripSeparatorApproval.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator4
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.Visible = false;
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			duplicateToolStripMenuItem.Text = "Copy";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(99, 6);
			panel1.Controls.Add(radioButtonMultiple);
			panel1.Controls.Add(radioButtonSingle);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(862, 26);
			panel1.TabIndex = 1;
			radioButtonMultiple.AutoSize = true;
			radioButtonMultiple.Checked = true;
			radioButtonMultiple.Location = new System.Drawing.Point(12, 4);
			radioButtonMultiple.Name = "radioButtonMultiple";
			radioButtonMultiple.Size = new System.Drawing.Size(61, 17);
			radioButtonMultiple.TabIndex = 0;
			radioButtonMultiple.TabStop = true;
			radioButtonMultiple.Text = "Multiple";
			radioButtonMultiple.UseVisualStyleBackColor = true;
			radioButtonMultiple.CheckedChanged += new System.EventHandler(radioButtonMultiPayee_CheckedChanged);
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(95, 4);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(54, 17);
			radioButtonSingle.TabIndex = 0;
			radioButtonSingle.Text = "Single";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtonMultiCheque_CheckedChanged);
			panelMultiPayee.Dock = System.Windows.Forms.DockStyle.Fill;
			panelMultiPayee.Location = new System.Drawing.Point(0, 57);
			panelMultiPayee.Name = "panelMultiPayee";
			panelMultiPayee.Size = new System.Drawing.Size(862, 491);
			panelMultiPayee.TabIndex = 2;
			panelMultiCheque.Dock = System.Windows.Forms.DockStyle.Fill;
			panelMultiCheque.Location = new System.Drawing.Point(0, 57);
			panelMultiCheque.Name = "panelMultiCheque";
			panelMultiCheque.Size = new System.Drawing.Size(862, 491);
			panelMultiCheque.TabIndex = 3;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(862, 548);
			base.Controls.Add(panelMultiCheque);
			base.Controls.Add(panelMultiPayee);
			base.Controls.Add(panel1);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CashPaymentParentForm";
			Text = "Cash Payment Voucher";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
