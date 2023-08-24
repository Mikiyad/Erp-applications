using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CreditlimitReviewForm : Form, IForm
	{
		private CreditLimitReviewData currentData;

		private const string TABLENAME_CONST = "Credit_Limit_Review";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool restrictTransaction;

		private ScreenAccessRight screenRight;

		private string customerID;

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private customersFlatComboBox comboBoxCustomer;

		private TextBox textBoxCustomerName;

		private MMLabel mmLabel1;

		private TextBox textBoxVoucherNumber;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerDate;

		private UserComboBox comboBoxRatingBy;

		private MMLabel mmLabel44;

		private MMLabel mmLabel4;

		private UltraComboEditor comboBoxRating;

		private TextBox textBoxRatingBy;

		private UltraGroupBox ultraGroupBox4;

		private TextBox textBoxPrevRatingDate;

		private TextBox textBoxPrevRating;

		private MMLabel mmLabel5;

		private MMLabel mmLabel6;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel9;

		private DateTimePicker dateTimePickerRateDate;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMLabel labelDays;

		private MMLabel LabelGraceDays;

		private NumberTextBox textBoxGraceDaysPrev;

		private MMLabel mmLabel12;

		private AmountTextBox textBoxUnsecuredLimitPrev;

		private CheckBox checkBoxUnsecuredLimitPrev;

		private MMLabel mmLabel13;

		private MMTextBox textBoxTempLimitPrev;

		private CheckBox checkBoxAcceptChequePrev;

		private RadioButton radioButtonSublimitPrev;

		private CheckBox checkBoxAcceptPDCPrev;

		private RadioButton radioButtonCreditLimitAmountPrev;

		private MMLabel mmLabel67;

		private MMLabel mmLabel68;

		private NumberTextBox textBoxGraceDays;

		private MMSDateTimePicker dateTimePickerCLValidity;

		private MMLabel mmLabel59;

		private AmountTextBox textBoxUnsecuredLimit;

		private CheckBox checkBoxUnsecuredLimit;

		private MMLabel mmLabel49;

		private MMTextBox textBoxTempLimit;

		private CheckBox checkBoxAcceptCheque;

		private RadioButton radioButtonSublimit;

		private CheckBox checkBoxAcceptPDC;

		private AmountTextBox textBoxCreditLimitAmount;

		private RadioButton radioButtonCreditLimitNoCredit;

		private RadioButton radioButtonCreditLimitUnlimited;

		private RadioButton radioButtonCreditLimitAmount;

		private MMTextBox textBoxRatingRemarks;

		private MMLabel mmLabel3;

		private MMLabel mmLabel20;

		private MMLabel mmLabel19;

		private TextBox dateTimePickerCLValidityPrev;

		private MMLabel mmLabel18;

		private MMLabel mmLabel15;

		private MMLabel mmLabel14;

		private TextBox textBoxRatingByPrev;

		private UserComboBox comboBoxRatingByPrev;

		private MMLabel mmLabel7;

		private MMLabel mmLabel8;

		private TextBox textboxcreditAmount;

		private MMLabel labelSignMessage;

		private UltraFormattedLinkLabel linkLoadImage;

		private MMLabel mmLabel70;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private PictureBox pictureBoxPhoto;

		private PictureBox pictureBoxNoImage;

		private OpenFileDialog openFileDialog1;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 4012;

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
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					textBoxVoucherNumber.ReadOnly = true;
				}
				comboBoxCustomer.Enabled = value;
				comboBoxSysDoc.Enabled = value;
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
				SetApprovalStatus();
			}
		}

		private bool RestrictTransaction
		{
			get
			{
				return restrictTransaction;
			}
			set
			{
				restrictTransaction = value;
			}
		}

		public string CustomerID
		{
			get
			{
				return customerID;
			}
			set
			{
				customerID = value;
			}
		}

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		public CreditlimitReviewForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += LeadStatusForm_Load;
			comboBoxCustomer.SelectedIndexChanged += ComboBoxCustomer_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private void ComboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			string selectedID = comboBoxCustomer.SelectedID;
			if (selectedID == "")
			{
				ClearForm();
			}
			else
			{
				LoadCustomerData(selectedID);
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CreditLimitReviewData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CreditLimitReviewTable.Rows[0] : currentData.CreditLimitReviewTable.NewRow();
				dataRow.BeginEdit();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["RatingRemarks"] = textBoxRatingRemarks.Text;
				if (comboBoxRating.SelectedIndex != -1)
				{
					dataRow["Rating"] = comboBoxRating.SelectedIndex;
				}
				else
				{
					dataRow["Rating"] = DBNull.Value;
				}
				if (comboBoxRatingBy.SelectedIndex != -1)
				{
					dataRow["RatingBy"] = comboBoxRatingBy.SelectedID;
				}
				else
				{
					dataRow["RatingBy"] = DBNull.Value;
				}
				if (comboBoxRatingBy.SelectedID == "")
				{
					dataRow["RatingBy"] = DBNull.Value;
				}
				else
				{
					dataRow["RatingBy"] = comboBoxRatingBy.SelectedID;
				}
				if (textBoxCreditLimitAmount.Text != "")
				{
					dataRow["CreditAmount"] = textBoxCreditLimitAmount.Text;
				}
				else
				{
					dataRow["CreditAmount"] = 0;
				}
				_ = dateTimePickerDate.Value;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				_ = dateTimePickerRateDate.Value;
				dataRow["RatingDate"] = dateTimePickerRateDate.Value;
				if (radioButtonCreditLimitAmount.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.CreditAmount;
				}
				else if (radioButtonCreditLimitNoCredit.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.NoCredit;
				}
				else if (radioButtonSublimit.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.ParentSublimit;
				}
				else
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.Unlimited;
				}
				dataRow["AcceptCheckPayment"] = checkBoxAcceptCheque.Checked;
				dataRow["AcceptPDC"] = checkBoxAcceptPDC.Checked;
				if (radioButtonCreditLimitAmount.Checked)
				{
					if (dateTimePickerCLValidity.Checked)
					{
						dataRow["CLValidity"] = dateTimePickerCLValidity.Value;
					}
					else
					{
						dataRow["CLValidity"] = DBNull.Value;
					}
				}
				else
				{
					dataRow["CLValidity"] = DBNull.Value;
				}
				dataRow["LimitPDCUnsecured"] = checkBoxUnsecuredLimit.Checked;
				if (checkBoxUnsecuredLimit.Checked)
				{
					dataRow["PDCUnsecuredLimitAmount"] = textBoxUnsecuredLimit.Text;
				}
				else
				{
					dataRow["PDCUnsecuredLimitAmount"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(textBoxGraceDays.Text))
				{
					dataRow["GraceDays"] = textBoxGraceDays.Text;
				}
				else
				{
					dataRow["GraceDays"] = 0;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CreditLimitReviewTable.Rows.Add(dataRow);
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
			textBoxVoucherNumber.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.CreditLimitReviewSystem.GetCreditLimitReviewByID(comboBoxSysDoc.SelectedID, id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxVoucherNumber.Text = id;
						textBoxVoucherNumber.Focus();
					}
					else
					{
						FillData();
						IsNewRecord = false;
						buttonSave.Enabled = false;
						textBoxVoucherNumber.ReadOnly = true;
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
					if (dataRow["VoucherID"] != DBNull.Value)
					{
						textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					}
					if (dataRow["CustomerID"] != DBNull.Value)
					{
						comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					}
					if (dataRow["Rating"] != DBNull.Value)
					{
						comboBoxRating.SelectedIndex = int.Parse(dataRow["Rating"].ToString());
					}
					else
					{
						comboBoxRating.SelectedIndex = 0;
					}
					if (dataRow["RatingDate"] != DBNull.Value)
					{
						textBoxPrevRatingDate.Text = DateTime.Parse(dataRow["RatingDate"].ToString()).ToShortDateString();
					}
					else
					{
						textBoxPrevRatingDate.Clear();
					}
					if (dataRow["RatingBy"] != DBNull.Value)
					{
						comboBoxRatingBy.SelectedID = dataRow["RatingBy"].ToString();
					}
					else
					{
						comboBoxRatingBy.Clear();
					}
					textBoxRatingRemarks.Text = dataRow["RatingRemarks"].ToString();
					if (dataRow["TransactionDate"] != DBNull.Value)
					{
						dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					}
					if (dataRow["RatingDate"] != DBNull.Value)
					{
						dateTimePickerRateDate.Value = DateTime.Parse(dataRow["RatingDate"].ToString());
					}
					if (dataRow["CreditAmount"] != DBNull.Value)
					{
						textBoxCreditLimitAmount.Text = decimal.Parse(dataRow["CreditAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxCreditLimitAmount.Text = 0.ToString(Format.TotalAmountFormat);
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
				return true;
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
				bool flag = (!isNewRecord) ? Factory.CreditLimitReviewSystem.CreateCreditLimitReview(currentData, isUpdate: true) : Factory.CreditLimitReviewSystem.CreateCreditLimitReview(currentData, isUpdate: false);
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

		public void SetApprovalStatus()
		{
			if (IsNewRecord)
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
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Credit_Limit_Review", "VoucherID");
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
			if (RestrictTransaction)
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				return false;
			}
			if (!IsNewRecord)
			{
				DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(currentData, SysDocTypes.CreditLimitReview, Global.CurrentUser, includeApproveUser: false);
				if (entityApprovalStatus.Tables[0].Rows.Count > 0 && !bool.Parse(entityApprovalStatus.Tables[0].Rows[0]["ModifyTransaction"].ToString()))
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
					return false;
				}
			}
			return true;
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
			comboBoxCustomer.FilterSysDocID = comboBoxSysDoc.SelectedID;
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
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
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			checkBoxUnsecuredLimitPrev.Visible = false;
			textBoxUnsecuredLimitPrev.Visible = false;
			mmLabel14.Visible = false;
			comboBoxCustomer.Enabled = true;
			comboBoxSysDoc.Enabled = true;
			comboBoxCustomer.Clear();
			textBoxPrevRating.Clear();
			comboBoxRating.Clear();
			comboBoxRatingBy.Clear();
			textBoxPrevRatingDate.Clear();
			textBoxRatingRemarks.Clear();
			textBoxUnsecuredLimit.Clear();
			checkBoxUnsecuredLimit.Checked = false;
			checkBoxAcceptCheque.Checked = true;
			checkBoxAcceptPDC.Checked = true;
			radioButtonCreditLimitNoCredit.Checked = true;
			dateTimePickerCLValidity.Clear();
			dateTimePickerCLValidity.Checked = false;
			textBoxCreditLimitAmount.Clear();
			textBoxGraceDays.Text = "0";
			textBoxUnsecuredLimitPrev.Clear();
			checkBoxUnsecuredLimitPrev.Checked = false;
			checkBoxAcceptChequePrev.Checked = true;
			checkBoxAcceptPDCPrev.Checked = true;
			comboBoxRatingByPrev.Clear();
			dateTimePickerCLValidityPrev.Clear();
			textBoxGraceDaysPrev.Text = "0";
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			linkAddPicture.Enabled = false;
			linkRemovePicture.Enabled = false;
			comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
			formManager.ResetDirty();
		}

		private void ProductManufacturerGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ProductManufacturerGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.CreditLimitReviewSystem.DeleteCreditLimitReview(textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Credit_Limit_Review", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Credit_Limit_Review", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Credit_Limit_Review", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Credit_Limit_Review", "VoucherID", "SysDocID", SystemDocID);
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
				else if (Factory.DatabaseSystem.ExistFieldValue("Credit_Limit_Review", "VoucherID", toolStripTextBoxFind.Text.Trim()))
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

		private void RelaeseTypeForm_Load(object sender, EventArgs e)
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			CreditLimitPreviewListForm creditLimitPreviewListForm = new CreditLimitPreviewListForm();
			creditLimitPreviewListForm.CustomerID = comboBoxCustomer.SelectedID;
			creditLimitPreviewListForm.ShowDialog();
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		public void LoadCustomerData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == ""))
				{
					CustomerData customerByID = Factory.CustomerSystem.GetCustomerByID(id);
					if (customerByID != null || customerByID.Tables.Count != 0 || customerByID.Tables[0].Rows.Count != 0)
					{
						DataRow dataRow = customerByID.Tables[0].Rows[0];
						if (dataRow["CustomerID"] != DBNull.Value)
						{
							comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
						}
						if (dataRow["CreditAmount"] == DBNull.Value)
						{
							string text3 = textBoxCreditLimitAmount.Text = (textboxcreditAmount.Text = 0.ToString(Format.TotalAmountFormat));
						}
						else
						{
							string text3 = textBoxCreditLimitAmount.Text = (textboxcreditAmount.Text = decimal.Parse(dataRow["CreditAmount"].ToString()).ToString(Format.TotalAmountFormat));
						}
						if (dataRow["Rating"] != DBNull.Value)
						{
							textBoxPrevRating.Text = int.Parse(dataRow["Rating"].ToString()).ToString();
							if (textBoxPrevRating.Text == "0")
							{
								textBoxPrevRating.Text = "N/A";
							}
							comboBoxRating.SelectedIndex = int.Parse(dataRow["Rating"].ToString());
						}
						else
						{
							textBoxPrevRating.Text = "";
							comboBoxRating.SelectedIndex = 0;
						}
						if (dataRow["RatingDate"] != DBNull.Value)
						{
							textBoxPrevRatingDate.Text = DateTime.Parse(dataRow["RatingDate"].ToString()).ToShortDateString();
						}
						else
						{
							textBoxPrevRatingDate.Clear();
						}
						if (dataRow["RatingBy"] == DBNull.Value)
						{
							string text3 = comboBoxRatingBy.SelectedID = (comboBoxRatingByPrev.SelectedID = Global.CurrentUser);
						}
						else
						{
							string text3 = comboBoxRatingBy.SelectedID = (comboBoxRatingByPrev.SelectedID = dataRow["RatingBy"].ToString());
						}
						textBoxRatingRemarks.Text = dataRow["RatingRemarks"].ToString();
						CreditLimitTypes creditLimitTypes = CreditLimitTypes.Unlimited;
						if (dataRow["CreditLimitType"] != DBNull.Value)
						{
							creditLimitTypes = (CreditLimitTypes)byte.Parse(dataRow["CreditLimitType"].ToString());
						}
						switch (creditLimitTypes)
						{
						case CreditLimitTypes.CreditAmount:
							radioButtonCreditLimitAmount.Checked = true;
							break;
						case CreditLimitTypes.NoCredit:
							radioButtonCreditLimitNoCredit.Checked = true;
							textboxcreditAmount.Text = "No Credit";
							break;
						case CreditLimitTypes.ParentSublimit:
						{
							RadioButton radioButton = radioButtonSublimit;
							bool @checked = radioButtonSublimitPrev.Checked = true;
							radioButton.Checked = @checked;
							textboxcreditAmount.Text = "Sublimit of Parent";
							break;
						}
						default:
							radioButtonCreditLimitUnlimited.Checked = true;
							textboxcreditAmount.Text = "Unlimted";
							break;
						}
						decimal.Parse(dataRow["CreditAmount"].ToString()).ToString(Format.TotalAmountFormat);
						textBoxRatingRemarks.Text = dataRow["RatingRemarks"].ToString();
						if (dataRow["CLValidity"] != DBNull.Value)
						{
							dateTimePickerCLValidity.Checked = true;
							dateTimePickerCLValidity.Value = DateTime.Parse(dataRow["CLValidity"].ToString());
							dateTimePickerCLValidityPrev.Text = DateTime.Parse(dataRow["CLValidity"].ToString()).ToShortDateString();
						}
						else
						{
							dateTimePickerCLValidity.Clear();
							dateTimePickerCLValidity.Checked = false;
							dateTimePickerCLValidityPrev.Text = "";
						}
						if (dataRow["AcceptCheckPayment"] != DBNull.Value)
						{
							CheckBox checkBox = checkBoxAcceptCheque;
							bool @checked = checkBoxAcceptChequePrev.Checked = bool.Parse(dataRow["AcceptCheckPayment"].ToString());
							checkBox.Checked = @checked;
						}
						else
						{
							CheckBox checkBox2 = checkBoxAcceptCheque;
							bool @checked = checkBoxAcceptChequePrev.Checked = true;
							checkBox2.Checked = @checked;
						}
						if (dataRow["AcceptPDC"] != DBNull.Value)
						{
							CheckBox checkBox3 = checkBoxAcceptPDC;
							bool @checked = checkBoxAcceptPDCPrev.Checked = bool.Parse(dataRow["AcceptPDC"].ToString());
							checkBox3.Checked = @checked;
						}
						else
						{
							CheckBox checkBox4 = checkBoxAcceptPDC;
							bool @checked = checkBoxAcceptPDCPrev.Checked = true;
							checkBox4.Checked = @checked;
						}
						if (dataRow["HasPhoto"] != DBNull.Value)
						{
							bool flag6 = Convert.ToBoolean(byte.Parse(dataRow["HasPhoto"].ToString()));
							linkLoadImage.Visible = flag6;
							if (flag6)
							{
								labelSignMessage.Visible = false;
								linkAddPicture.Enabled = false;
								linkRemovePicture.Enabled = true;
							}
							else
							{
								pictureBoxPhoto.Image = null;
								labelSignMessage.Visible = true;
								linkAddPicture.Enabled = true;
								linkRemovePicture.Enabled = false;
							}
						}
						else
						{
							linkLoadImage.Visible = false;
							labelSignMessage.Visible = true;
							linkRemovePicture.Enabled = false;
						}
						if (dataRow["LimitPDCUnsecured"] != DBNull.Value)
						{
							CheckBox checkBox5 = checkBoxUnsecuredLimitPrev;
							MMLabel mMLabel = mmLabel14;
							bool flag8 = textBoxUnsecuredLimitPrev.Visible = bool.Parse(dataRow["LimitPDCUnsecured"].ToString());
							bool @checked = mMLabel.Visible = flag8;
							checkBox5.Visible = @checked;
							CheckBox checkBox6 = checkBoxUnsecuredLimit;
							@checked = (checkBoxUnsecuredLimitPrev.Checked = bool.Parse(dataRow["LimitPDCUnsecured"].ToString()));
							checkBox6.Checked = @checked;
							if (dataRow["PDCUnsecuredLimitAmount"] == DBNull.Value)
							{
								string text3 = textBoxUnsecuredLimit.Text = (textBoxUnsecuredLimitPrev.Text = 0.ToString(Format.TotalAmountFormat));
							}
							else
							{
								string text3 = textBoxUnsecuredLimit.Text = (textBoxUnsecuredLimitPrev.Text = decimal.Parse(dataRow["PDCUnsecuredLimitAmount"].ToString()).ToString(Format.TotalAmountFormat));
							}
						}
						else
						{
							checkBoxUnsecuredLimit.Checked = false;
							string text3 = textBoxUnsecuredLimit.Text = (textBoxUnsecuredLimitPrev.Text = 0.ToString(Format.TotalAmountFormat));
						}
						if (dataRow["GraceDays"] == DBNull.Value)
						{
							string text3 = textBoxGraceDays.Text = (textBoxGraceDaysPrev.Text = "0");
						}
						else
						{
							string text3 = textBoxGraceDays.Text = (textBoxGraceDaysPrev.Text = dataRow["GraceDays"].ToString());
						}
						if (textBoxGraceDays.Text.ToString().Equals("0"))
						{
							textBoxGraceDaysPrev.Visible = false;
							LabelGraceDays.Visible = false;
							labelDays.Visible = false;
						}
						else
						{
							textBoxGraceDaysPrev.Visible = true;
							LabelGraceDays.Visible = true;
							labelDays.Visible = true;
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ClearForm();
			}
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.CreditLimitReviewSystem.GetNextDocumentNumber(SystemDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void LeadStatusForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
					if (CustomerID != null && CustomerID != "")
					{
						LoadCustomerData(CustomerID);
					}
					else
					{
						IsNewRecord = true;
						comboBoxCustomer.ReadOnly = false;
						ClearForm();
					}
					comboBoxSysDoc.FilterByType(SysDocTypes.CreditLimitReview);
					formManager.ResetDirty();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityName = comboBoxSysDoc.SelectedName;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
		}

		private void comboBoxRating_ValueChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
		}

		private void radioButtonCreditLimitAmount_CheckedChanged(object sender, EventArgs e)
		{
			AmountTextBox amountTextBox = textBoxCreditLimitAmount;
			MMSDateTimePicker mMSDateTimePicker = dateTimePickerCLValidity;
			bool flag = checkBoxUnsecuredLimit.Enabled = radioButtonCreditLimitAmount.Checked;
			bool enabled = mMSDateTimePicker.Enabled = flag;
			amountTextBox.Enabled = enabled;
		}

		private void checkBoxUnsecuredLimit_CheckedChanged(object sender, EventArgs e)
		{
			textBoxUnsecuredLimit.Enabled = checkBoxUnsecuredLimit.Checked;
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.CreditLimitReview);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(comboBoxCustomer.SelectedID == "") && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddCustomerSignature(comboBoxCustomer.SelectedID, image))
					{
						pictureBoxPhoto.Image = image;
						labelSignMessage.Visible = false;
						linkRemovePicture.Enabled = true;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add picture.");
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(comboBoxCustomer.SelectedID == "") && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.CustomerSystem.RemoveCustomerSignature(comboBoxCustomer.SelectedID))
					{
						pictureBoxPhoto.Image = null;
						labelSignMessage.Visible = true;
						linkAddPicture.Enabled = true;
						linkRemovePicture.Enabled = false;
					}
					else
					{
						labelSignMessage.Visible = false;
						ErrorHelper.ErrorMessage("Cannot remove the image.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot remove image.");
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(comboBoxCustomer.SelectedID == ""))
				{
					pictureBoxPhoto.Image = PublicFunctions.GetCustomerSignatureThumbnailImage(comboBoxCustomer.SelectedID);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
			Infragistics.Win.ValueListItem valueListItem = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CreditlimitReviewForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
			toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxRatingBy = new System.Windows.Forms.TextBox();
			comboBoxRating = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			textboxcreditAmount = new System.Windows.Forms.TextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxRatingByPrev = new System.Windows.Forms.TextBox();
			comboBoxRatingByPrev = new Micromind.DataControls.UserComboBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			dateTimePickerCLValidityPrev = new System.Windows.Forms.TextBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			labelDays = new Micromind.UISupport.MMLabel();
			LabelGraceDays = new Micromind.UISupport.MMLabel();
			textBoxGraceDaysPrev = new Micromind.UISupport.NumberTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxUnsecuredLimitPrev = new Micromind.UISupport.AmountTextBox();
			checkBoxUnsecuredLimitPrev = new System.Windows.Forms.CheckBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxTempLimitPrev = new Micromind.UISupport.MMTextBox();
			checkBoxAcceptChequePrev = new System.Windows.Forms.CheckBox();
			checkBoxAcceptPDCPrev = new System.Windows.Forms.CheckBox();
			radioButtonCreditLimitAmountPrev = new System.Windows.Forms.RadioButton();
			textBoxPrevRatingDate = new System.Windows.Forms.TextBox();
			radioButtonSublimitPrev = new System.Windows.Forms.RadioButton();
			textBoxPrevRating = new System.Windows.Forms.TextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel67 = new Micromind.UISupport.MMLabel();
			mmLabel68 = new Micromind.UISupport.MMLabel();
			textBoxGraceDays = new Micromind.UISupport.NumberTextBox();
			dateTimePickerCLValidity = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel59 = new Micromind.UISupport.MMLabel();
			textBoxUnsecuredLimit = new Micromind.UISupport.AmountTextBox();
			checkBoxUnsecuredLimit = new System.Windows.Forms.CheckBox();
			mmLabel49 = new Micromind.UISupport.MMLabel();
			textBoxTempLimit = new Micromind.UISupport.MMTextBox();
			checkBoxAcceptCheque = new System.Windows.Forms.CheckBox();
			radioButtonSublimit = new System.Windows.Forms.RadioButton();
			checkBoxAcceptPDC = new System.Windows.Forms.CheckBox();
			textBoxCreditLimitAmount = new Micromind.UISupport.AmountTextBox();
			radioButtonCreditLimitNoCredit = new System.Windows.Forms.RadioButton();
			radioButtonCreditLimitUnlimited = new System.Windows.Forms.RadioButton();
			radioButtonCreditLimitAmount = new System.Windows.Forms.RadioButton();
			textBoxRatingRemarks = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			dateTimePickerRateDate = new System.Windows.Forms.DateTimePicker();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			comboBoxRatingBy = new Micromind.DataControls.UserComboBox();
			mmLabel44 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			labelSignMessage = new Micromind.UISupport.MMLabel();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel70 = new Micromind.UISupport.MMLabel();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRating).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRatingByPrev).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRatingBy).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
			{
				toolStripButtonPrint,
				toolStripButtonApproval,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonInformation,
				toolStripLabelApproval,
				toolStripSeparatorApproval
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(706, 31);
			toolStrip1.TabIndex = 11;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator2.Visible = false;
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripLabelApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripLabelApproval.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			toolStripLabelApproval.Name = "toolStripLabelApproval";
			toolStripLabelApproval.Size = new System.Drawing.Size(45, 28);
			toolStripLabelApproval.Text = "Status:";
			toolStripSeparatorApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripSeparatorApproval.Name = "toolStripSeparatorApproval";
			toolStripSeparatorApproval.Size = new System.Drawing.Size(6, 31);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 529);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(706, 40);
			panelButtons.TabIndex = 7;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(706, 1);
			linePanelDown.TabIndex = 0;
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
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(584, 8);
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
			buttonNew.TabIndex = 2;
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
			buttonSave.TabIndex = 1;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(221, 61);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(321, 20);
			textBoxCustomerName.TabIndex = 3;
			textBoxCustomerName.TabStop = false;
			textBoxVoucherNumber.Location = new System.Drawing.Point(329, 36);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(111, 20);
			textBoxVoucherNumber.TabIndex = 1;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(488, 35);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(126, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxRatingBy.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRatingBy.Location = new System.Drawing.Point(223, 44);
			textBoxRatingBy.MaxLength = 64;
			textBoxRatingBy.Name = "textBoxRatingBy";
			textBoxRatingBy.ReadOnly = true;
			textBoxRatingBy.Size = new System.Drawing.Size(235, 20);
			textBoxRatingBy.TabIndex = 3;
			textBoxRatingBy.TabStop = false;
			comboBoxRating.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			valueListItem.DataValue = (byte)0;
			valueListItem.DisplayText = "N/A";
			valueListItem2.DataValue = (byte)1;
			valueListItem2.DisplayText = "1";
			valueListItem3.DataValue = "2";
			valueListItem3.DisplayText = "2";
			valueListItem4.DataValue = (byte)3;
			valueListItem4.DisplayText = "3";
			valueListItem5.DataValue = "4";
			valueListItem5.DisplayText = "4";
			valueListItem6.DataValue = (byte)5;
			valueListItem6.DisplayText = "5";
			valueListItem7.DataValue = (byte)6;
			valueListItem7.DisplayText = "6";
			valueListItem8.DataValue = "7";
			valueListItem8.DisplayText = "7";
			valueListItem9.DataValue = "8";
			valueListItem9.DisplayText = "8";
			valueListItem10.DataValue = "9";
			valueListItem10.DisplayText = "9";
			valueListItem11.DataValue = "10";
			valueListItem11.DisplayText = "10";
			comboBoxRating.Items.AddRange(new Infragistics.Win.ValueListItem[11]
			{
				valueListItem,
				valueListItem2,
				valueListItem3,
				valueListItem4,
				valueListItem5,
				valueListItem6,
				valueListItem7,
				valueListItem8,
				valueListItem9,
				valueListItem10,
				valueListItem11
			});
			comboBoxRating.Location = new System.Drawing.Point(100, 20);
			comboBoxRating.Name = "comboBoxRating";
			comboBoxRating.Size = new System.Drawing.Size(119, 21);
			comboBoxRating.TabIndex = 0;
			comboBoxRating.ValueChanged += new System.EventHandler(comboBoxRating_ValueChanged);
			ultraGroupBox4.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox4.Controls.Add(textboxcreditAmount);
			ultraGroupBox4.Controls.Add(mmLabel8);
			ultraGroupBox4.Controls.Add(textBoxRatingByPrev);
			ultraGroupBox4.Controls.Add(comboBoxRatingByPrev);
			ultraGroupBox4.Controls.Add(mmLabel7);
			ultraGroupBox4.Controls.Add(mmLabel20);
			ultraGroupBox4.Controls.Add(mmLabel19);
			ultraGroupBox4.Controls.Add(dateTimePickerCLValidityPrev);
			ultraGroupBox4.Controls.Add(mmLabel18);
			ultraGroupBox4.Controls.Add(mmLabel15);
			ultraGroupBox4.Controls.Add(mmLabel14);
			ultraGroupBox4.Controls.Add(labelDays);
			ultraGroupBox4.Controls.Add(LabelGraceDays);
			ultraGroupBox4.Controls.Add(textBoxGraceDaysPrev);
			ultraGroupBox4.Controls.Add(mmLabel12);
			ultraGroupBox4.Controls.Add(textBoxUnsecuredLimitPrev);
			ultraGroupBox4.Controls.Add(checkBoxUnsecuredLimitPrev);
			ultraGroupBox4.Controls.Add(mmLabel13);
			ultraGroupBox4.Controls.Add(textBoxTempLimitPrev);
			ultraGroupBox4.Controls.Add(checkBoxAcceptChequePrev);
			ultraGroupBox4.Controls.Add(checkBoxAcceptPDCPrev);
			ultraGroupBox4.Controls.Add(radioButtonCreditLimitAmountPrev);
			ultraGroupBox4.Controls.Add(textBoxPrevRatingDate);
			ultraGroupBox4.Controls.Add(radioButtonSublimitPrev);
			ultraGroupBox4.Controls.Add(textBoxPrevRating);
			ultraGroupBox4.Controls.Add(mmLabel5);
			ultraGroupBox4.Controls.Add(mmLabel6);
			ultraGroupBox4.Location = new System.Drawing.Point(4, 95);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(679, 175);
			ultraGroupBox4.TabIndex = 4;
			ultraGroupBox4.Text = "Previous Limit";
			textboxcreditAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textboxcreditAmount.Location = new System.Drawing.Point(98, 69);
			textboxcreditAmount.MaxLength = 64;
			textboxcreditAmount.Name = "textboxcreditAmount";
			textboxcreditAmount.ReadOnly = true;
			textboxcreditAmount.Size = new System.Drawing.Size(116, 20);
			textboxcreditAmount.TabIndex = 162;
			textboxcreditAmount.TabStop = false;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(18, 73);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(76, 13);
			mmLabel8.TabIndex = 161;
			mmLabel8.Text = "Credit Amount";
			textBoxRatingByPrev.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRatingByPrev.Location = new System.Drawing.Point(219, 45);
			textBoxRatingByPrev.MaxLength = 64;
			textBoxRatingByPrev.Name = "textBoxRatingByPrev";
			textBoxRatingByPrev.ReadOnly = true;
			textBoxRatingByPrev.Size = new System.Drawing.Size(235, 20);
			textBoxRatingByPrev.TabIndex = 157;
			textBoxRatingByPrev.TabStop = false;
			comboBoxRatingByPrev.Assigned = false;
			comboBoxRatingByPrev.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxRatingByPrev.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRatingByPrev.CustomReportFieldName = "";
			comboBoxRatingByPrev.CustomReportKey = "";
			comboBoxRatingByPrev.CustomReportValueType = 1;
			comboBoxRatingByPrev.DescriptionTextBox = textBoxRatingByPrev;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRatingByPrev.DisplayLayout.Appearance = appearance;
			comboBoxRatingByPrev.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRatingByPrev.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRatingByPrev.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRatingByPrev.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxRatingByPrev.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRatingByPrev.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxRatingByPrev.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRatingByPrev.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRatingByPrev.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRatingByPrev.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxRatingByPrev.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRatingByPrev.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRatingByPrev.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRatingByPrev.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxRatingByPrev.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRatingByPrev.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRatingByPrev.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxRatingByPrev.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxRatingByPrev.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRatingByPrev.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxRatingByPrev.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxRatingByPrev.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRatingByPrev.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxRatingByPrev.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRatingByPrev.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRatingByPrev.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRatingByPrev.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Never;
			comboBoxRatingByPrev.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRatingByPrev.Editable = true;
			comboBoxRatingByPrev.FilterString = "";
			comboBoxRatingByPrev.HasAllAccount = false;
			comboBoxRatingByPrev.HasCustom = false;
			comboBoxRatingByPrev.IsDataLoaded = false;
			comboBoxRatingByPrev.Location = new System.Drawing.Point(98, 44);
			comboBoxRatingByPrev.MaxDropDownItems = 12;
			comboBoxRatingByPrev.Name = "comboBoxRatingByPrev";
			comboBoxRatingByPrev.ReadOnly = true;
			comboBoxRatingByPrev.ShowInactiveItems = false;
			comboBoxRatingByPrev.ShowQuickAdd = true;
			comboBoxRatingByPrev.Size = new System.Drawing.Size(116, 20);
			comboBoxRatingByPrev.TabIndex = 156;
			comboBoxRatingByPrev.TabStop = false;
			comboBoxRatingByPrev.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(18, 47);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(57, 13);
			mmLabel7.TabIndex = 158;
			mmLabel7.Text = "Rating By:";
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(579, 135);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(179, 13);
			mmLabel20.TabIndex = 155;
			mmLabel20.Text = "Accept post-dated cheque payment";
			mmLabel20.Visible = false;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(579, 114);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(123, 13);
			mmLabel19.TabIndex = 154;
			mmLabel19.Text = "Accept cheque payment";
			mmLabel19.Visible = false;
			dateTimePickerCLValidityPrev.BackColor = System.Drawing.Color.WhiteSmoke;
			dateTimePickerCLValidityPrev.Location = new System.Drawing.Point(98, 94);
			dateTimePickerCLValidityPrev.MaxLength = 64;
			dateTimePickerCLValidityPrev.Name = "dateTimePickerCLValidityPrev";
			dateTimePickerCLValidityPrev.ReadOnly = true;
			dateTimePickerCLValidityPrev.Size = new System.Drawing.Size(116, 20);
			dateTimePickerCLValidityPrev.TabIndex = 153;
			dateTimePickerCLValidityPrev.TabStop = false;
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(592, 73);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(91, 13);
			mmLabel18.TabIndex = 152;
			mmLabel18.Text = "Sublimit of Parent";
			mmLabel18.Visible = false;
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(592, 86);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(61, 13);
			mmLabel15.TabIndex = 149;
			mmLabel15.Text = "Amount of:";
			mmLabel15.Visible = false;
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(234, 98);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(122, 13);
			mmLabel14.TabIndex = 148;
			mmLabel14.Text = "Limit PDC Unsecured to:";
			mmLabel14.Visible = false;
			labelDays.AutoSize = true;
			labelDays.BackColor = System.Drawing.Color.Transparent;
			labelDays.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelDays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelDays.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelDays.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelDays.IsFieldHeader = false;
			labelDays.IsRequired = false;
			labelDays.Location = new System.Drawing.Point(460, 73);
			labelDays.Name = "labelDays";
			labelDays.PenWidth = 1f;
			labelDays.ShowBorder = false;
			labelDays.Size = new System.Drawing.Size(31, 13);
			labelDays.TabIndex = 147;
			labelDays.Text = "Days";
			labelDays.Visible = false;
			LabelGraceDays.AutoSize = true;
			LabelGraceDays.BackColor = System.Drawing.Color.Transparent;
			LabelGraceDays.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			LabelGraceDays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			LabelGraceDays.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			LabelGraceDays.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			LabelGraceDays.IsFieldHeader = false;
			LabelGraceDays.IsRequired = false;
			LabelGraceDays.Location = new System.Drawing.Point(221, 73);
			LabelGraceDays.Name = "LabelGraceDays";
			LabelGraceDays.PenWidth = 1f;
			LabelGraceDays.ShowBorder = false;
			LabelGraceDays.Size = new System.Drawing.Size(178, 13);
			LabelGraceDays.TabIndex = 146;
			LabelGraceDays.Text = "Grace Days on Over Due Payment :";
			LabelGraceDays.Visible = false;
			textBoxGraceDaysPrev.AllowDecimal = false;
			textBoxGraceDaysPrev.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGraceDaysPrev.CustomReportFieldName = "";
			textBoxGraceDaysPrev.CustomReportKey = "";
			textBoxGraceDaysPrev.CustomReportValueType = 1;
			textBoxGraceDaysPrev.IsComboTextBox = false;
			textBoxGraceDaysPrev.IsModified = false;
			textBoxGraceDaysPrev.Location = new System.Drawing.Point(401, 69);
			textBoxGraceDaysPrev.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxGraceDaysPrev.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxGraceDaysPrev.Name = "textBoxGraceDaysPrev";
			textBoxGraceDaysPrev.NullText = "0";
			textBoxGraceDaysPrev.ReadOnly = true;
			textBoxGraceDaysPrev.Size = new System.Drawing.Size(53, 20);
			textBoxGraceDaysPrev.TabIndex = 145;
			textBoxGraceDaysPrev.TabStop = false;
			textBoxGraceDaysPrev.Text = "0";
			textBoxGraceDaysPrev.Visible = false;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(18, 98);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(46, 13);
			mmLabel12.TabIndex = 140;
			mmLabel12.Text = "Valid till:";
			textBoxUnsecuredLimitPrev.AllowDecimal = true;
			textBoxUnsecuredLimitPrev.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnsecuredLimitPrev.CustomReportFieldName = "";
			textBoxUnsecuredLimitPrev.CustomReportKey = "";
			textBoxUnsecuredLimitPrev.CustomReportValueType = 1;
			textBoxUnsecuredLimitPrev.IsComboTextBox = false;
			textBoxUnsecuredLimitPrev.IsModified = false;
			textBoxUnsecuredLimitPrev.Location = new System.Drawing.Point(356, 94);
			textBoxUnsecuredLimitPrev.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxUnsecuredLimitPrev.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxUnsecuredLimitPrev.Name = "textBoxUnsecuredLimitPrev";
			textBoxUnsecuredLimitPrev.NullText = "0";
			textBoxUnsecuredLimitPrev.ReadOnly = true;
			textBoxUnsecuredLimitPrev.Size = new System.Drawing.Size(98, 20);
			textBoxUnsecuredLimitPrev.TabIndex = 141;
			textBoxUnsecuredLimitPrev.TabStop = false;
			textBoxUnsecuredLimitPrev.Text = "0.00";
			textBoxUnsecuredLimitPrev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxUnsecuredLimitPrev.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxUnsecuredLimitPrev.Visible = false;
			checkBoxUnsecuredLimitPrev.AutoSize = true;
			checkBoxUnsecuredLimitPrev.Enabled = false;
			checkBoxUnsecuredLimitPrev.Location = new System.Drawing.Point(219, 97);
			checkBoxUnsecuredLimitPrev.Name = "checkBoxUnsecuredLimitPrev";
			checkBoxUnsecuredLimitPrev.Size = new System.Drawing.Size(15, 14);
			checkBoxUnsecuredLimitPrev.TabIndex = 136;
			checkBoxUnsecuredLimitPrev.UseVisualStyleBackColor = true;
			checkBoxUnsecuredLimitPrev.Visible = false;
			checkBoxUnsecuredLimitPrev.Click += new System.EventHandler(checkBoxUnsecuredLimit_CheckedChanged);
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(18, 123);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(61, 13);
			mmLabel13.TabIndex = 137;
			mmLabel13.Text = "Temp Limit:";
			textBoxTempLimitPrev.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTempLimitPrev.CustomReportFieldName = "";
			textBoxTempLimitPrev.CustomReportKey = "";
			textBoxTempLimitPrev.CustomReportValueType = 1;
			textBoxTempLimitPrev.IsComboTextBox = false;
			textBoxTempLimitPrev.IsModified = false;
			textBoxTempLimitPrev.Location = new System.Drawing.Point(98, 119);
			textBoxTempLimitPrev.MaxLength = 30;
			textBoxTempLimitPrev.Name = "textBoxTempLimitPrev";
			textBoxTempLimitPrev.ReadOnly = true;
			textBoxTempLimitPrev.Size = new System.Drawing.Size(116, 20);
			textBoxTempLimitPrev.TabIndex = 143;
			textBoxTempLimitPrev.TabStop = false;
			textBoxTempLimitPrev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			checkBoxAcceptChequePrev.AutoSize = true;
			checkBoxAcceptChequePrev.Checked = true;
			checkBoxAcceptChequePrev.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptChequePrev.Enabled = false;
			checkBoxAcceptChequePrev.Location = new System.Drawing.Point(562, 113);
			checkBoxAcceptChequePrev.Name = "checkBoxAcceptChequePrev";
			checkBoxAcceptChequePrev.Size = new System.Drawing.Size(15, 14);
			checkBoxAcceptChequePrev.TabIndex = 142;
			checkBoxAcceptChequePrev.UseVisualStyleBackColor = true;
			checkBoxAcceptChequePrev.Visible = false;
			checkBoxAcceptPDCPrev.AutoSize = true;
			checkBoxAcceptPDCPrev.Checked = true;
			checkBoxAcceptPDCPrev.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptPDCPrev.Enabled = false;
			checkBoxAcceptPDCPrev.Location = new System.Drawing.Point(562, 135);
			checkBoxAcceptPDCPrev.Name = "checkBoxAcceptPDCPrev";
			checkBoxAcceptPDCPrev.Size = new System.Drawing.Size(15, 14);
			checkBoxAcceptPDCPrev.TabIndex = 144;
			checkBoxAcceptPDCPrev.UseVisualStyleBackColor = true;
			checkBoxAcceptPDCPrev.Visible = false;
			radioButtonCreditLimitAmountPrev.AutoSize = true;
			radioButtonCreditLimitAmountPrev.Enabled = false;
			radioButtonCreditLimitAmountPrev.Location = new System.Drawing.Point(592, 84);
			radioButtonCreditLimitAmountPrev.Name = "radioButtonCreditLimitAmountPrev";
			radioButtonCreditLimitAmountPrev.Size = new System.Drawing.Size(14, 13);
			radioButtonCreditLimitAmountPrev.TabIndex = 134;
			radioButtonCreditLimitAmountPrev.UseVisualStyleBackColor = true;
			radioButtonCreditLimitAmountPrev.Visible = false;
			textBoxPrevRatingDate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPrevRatingDate.Location = new System.Drawing.Point(295, 18);
			textBoxPrevRatingDate.MaxLength = 64;
			textBoxPrevRatingDate.Name = "textBoxPrevRatingDate";
			textBoxPrevRatingDate.ReadOnly = true;
			textBoxPrevRatingDate.Size = new System.Drawing.Size(159, 20);
			textBoxPrevRatingDate.TabIndex = 1;
			textBoxPrevRatingDate.TabStop = false;
			radioButtonSublimitPrev.AutoSize = true;
			radioButtonSublimitPrev.Enabled = false;
			radioButtonSublimitPrev.Location = new System.Drawing.Point(575, 73);
			radioButtonSublimitPrev.Name = "radioButtonSublimitPrev";
			radioButtonSublimitPrev.Size = new System.Drawing.Size(14, 13);
			radioButtonSublimitPrev.TabIndex = 138;
			radioButtonSublimitPrev.UseVisualStyleBackColor = true;
			radioButtonSublimitPrev.Visible = false;
			textBoxPrevRating.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPrevRating.Location = new System.Drawing.Point(98, 19);
			textBoxPrevRating.MaxLength = 64;
			textBoxPrevRating.Name = "textBoxPrevRating";
			textBoxPrevRating.ReadOnly = true;
			textBoxPrevRating.Size = new System.Drawing.Size(116, 20);
			textBoxPrevRating.TabIndex = 0;
			textBoxPrevRating.TabStop = false;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(18, 21);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(74, 13);
			mmLabel5.TabIndex = 90;
			mmLabel5.Text = "Credit Rating:";
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(221, 22);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(68, 13);
			mmLabel6.TabIndex = 91;
			mmLabel6.Text = "Rating Date:";
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(labelSignMessage);
			ultraGroupBox1.Controls.Add(linkLoadImage);
			ultraGroupBox1.Controls.Add(mmLabel70);
			ultraGroupBox1.Controls.Add(linkRemovePicture);
			ultraGroupBox1.Controls.Add(linkAddPicture);
			ultraGroupBox1.Controls.Add(pictureBoxPhoto);
			ultraGroupBox1.Controls.Add(pictureBoxNoImage);
			ultraGroupBox1.Controls.Add(mmLabel67);
			ultraGroupBox1.Controls.Add(mmLabel68);
			ultraGroupBox1.Controls.Add(textBoxGraceDays);
			ultraGroupBox1.Controls.Add(dateTimePickerCLValidity);
			ultraGroupBox1.Controls.Add(mmLabel59);
			ultraGroupBox1.Controls.Add(textBoxUnsecuredLimit);
			ultraGroupBox1.Controls.Add(checkBoxUnsecuredLimit);
			ultraGroupBox1.Controls.Add(mmLabel49);
			ultraGroupBox1.Controls.Add(textBoxTempLimit);
			ultraGroupBox1.Controls.Add(checkBoxAcceptCheque);
			ultraGroupBox1.Controls.Add(radioButtonSublimit);
			ultraGroupBox1.Controls.Add(checkBoxAcceptPDC);
			ultraGroupBox1.Controls.Add(textBoxCreditLimitAmount);
			ultraGroupBox1.Controls.Add(radioButtonCreditLimitNoCredit);
			ultraGroupBox1.Controls.Add(radioButtonCreditLimitUnlimited);
			ultraGroupBox1.Controls.Add(radioButtonCreditLimitAmount);
			ultraGroupBox1.Controls.Add(textBoxRatingRemarks);
			ultraGroupBox1.Controls.Add(mmLabel3);
			ultraGroupBox1.Controls.Add(mmLabel9);
			ultraGroupBox1.Controls.Add(dateTimePickerRateDate);
			ultraGroupBox1.Controls.Add(comboBoxRating);
			ultraGroupBox1.Controls.Add(textBoxRatingBy);
			ultraGroupBox1.Controls.Add(mmLabel4);
			ultraGroupBox1.Controls.Add(comboBoxRatingBy);
			ultraGroupBox1.Controls.Add(mmLabel44);
			ultraGroupBox1.Location = new System.Drawing.Point(0, 244);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(683, 257);
			ultraGroupBox1.TabIndex = 5;
			ultraGroupBox1.Text = "New Review";
			mmLabel67.AutoSize = true;
			mmLabel67.BackColor = System.Drawing.Color.Transparent;
			mmLabel67.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel67.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel67.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel67.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel67.IsFieldHeader = false;
			mmLabel67.IsRequired = false;
			mmLabel67.Location = new System.Drawing.Point(591, 149);
			mmLabel67.Name = "mmLabel67";
			mmLabel67.PenWidth = 1f;
			mmLabel67.ShowBorder = false;
			mmLabel67.Size = new System.Drawing.Size(31, 13);
			mmLabel67.TabIndex = 131;
			mmLabel67.Text = "Days";
			mmLabel68.AutoSize = true;
			mmLabel68.BackColor = System.Drawing.Color.Transparent;
			mmLabel68.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel68.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel68.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel68.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel68.IsFieldHeader = false;
			mmLabel68.IsRequired = false;
			mmLabel68.Location = new System.Drawing.Point(327, 150);
			mmLabel68.Name = "mmLabel68";
			mmLabel68.PenWidth = 1f;
			mmLabel68.ShowBorder = false;
			mmLabel68.Size = new System.Drawing.Size(178, 13);
			mmLabel68.TabIndex = 130;
			mmLabel68.Text = "Grace Days on Over Due Payment :";
			textBoxGraceDays.AllowDecimal = false;
			textBoxGraceDays.CustomReportFieldName = "";
			textBoxGraceDays.CustomReportKey = "";
			textBoxGraceDays.CustomReportValueType = 1;
			textBoxGraceDays.IsComboTextBox = false;
			textBoxGraceDays.IsModified = false;
			textBoxGraceDays.Location = new System.Drawing.Point(522, 146);
			textBoxGraceDays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxGraceDays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxGraceDays.Name = "textBoxGraceDays";
			textBoxGraceDays.NullText = "0";
			textBoxGraceDays.Size = new System.Drawing.Size(59, 20);
			textBoxGraceDays.TabIndex = 14;
			textBoxGraceDays.Text = "0";
			dateTimePickerCLValidity.Checked = false;
			dateTimePickerCLValidity.CustomFormat = " ";
			dateTimePickerCLValidity.Enabled = false;
			dateTimePickerCLValidity.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerCLValidity.Location = new System.Drawing.Point(394, 100);
			dateTimePickerCLValidity.Name = "dateTimePickerCLValidity";
			dateTimePickerCLValidity.ShowCheckBox = true;
			dateTimePickerCLValidity.Size = new System.Drawing.Size(114, 20);
			dateTimePickerCLValidity.TabIndex = 8;
			dateTimePickerCLValidity.Value = new System.DateTime(0L);
			mmLabel59.AutoSize = true;
			mmLabel59.BackColor = System.Drawing.Color.Transparent;
			mmLabel59.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel59.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel59.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel59.IsFieldHeader = false;
			mmLabel59.IsRequired = false;
			mmLabel59.Location = new System.Drawing.Point(327, 104);
			mmLabel59.Name = "mmLabel59";
			mmLabel59.PenWidth = 1f;
			mmLabel59.ShowBorder = false;
			mmLabel59.Size = new System.Drawing.Size(46, 13);
			mmLabel59.TabIndex = 124;
			mmLabel59.Text = "Valid till:";
			textBoxUnsecuredLimit.AllowDecimal = true;
			textBoxUnsecuredLimit.BackColor = System.Drawing.Color.White;
			textBoxUnsecuredLimit.CustomReportFieldName = "";
			textBoxUnsecuredLimit.CustomReportKey = "";
			textBoxUnsecuredLimit.CustomReportValueType = 1;
			textBoxUnsecuredLimit.Enabled = false;
			textBoxUnsecuredLimit.IsComboTextBox = false;
			textBoxUnsecuredLimit.IsModified = false;
			textBoxUnsecuredLimit.Location = new System.Drawing.Point(521, 100);
			textBoxUnsecuredLimit.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxUnsecuredLimit.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxUnsecuredLimit.Name = "textBoxUnsecuredLimit";
			textBoxUnsecuredLimit.NullText = "0";
			textBoxUnsecuredLimit.Size = new System.Drawing.Size(137, 20);
			textBoxUnsecuredLimit.TabIndex = 10;
			textBoxUnsecuredLimit.Text = "0.00";
			textBoxUnsecuredLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxUnsecuredLimit.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			checkBoxUnsecuredLimit.AutoSize = true;
			checkBoxUnsecuredLimit.Enabled = false;
			checkBoxUnsecuredLimit.Location = new System.Drawing.Point(521, 82);
			checkBoxUnsecuredLimit.Name = "checkBoxUnsecuredLimit";
			checkBoxUnsecuredLimit.Size = new System.Drawing.Size(142, 17);
			checkBoxUnsecuredLimit.TabIndex = 9;
			checkBoxUnsecuredLimit.Text = "Limit PDC Unsecured to:";
			checkBoxUnsecuredLimit.UseVisualStyleBackColor = true;
			checkBoxUnsecuredLimit.CheckedChanged += new System.EventHandler(checkBoxUnsecuredLimit_CheckedChanged);
			mmLabel49.AutoSize = true;
			mmLabel49.BackColor = System.Drawing.Color.Transparent;
			mmLabel49.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel49.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel49.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel49.IsFieldHeader = false;
			mmLabel49.IsRequired = false;
			mmLabel49.Location = new System.Drawing.Point(327, 125);
			mmLabel49.Name = "mmLabel49";
			mmLabel49.PenWidth = 1f;
			mmLabel49.ShowBorder = false;
			mmLabel49.Size = new System.Drawing.Size(61, 13);
			mmLabel49.TabIndex = 121;
			mmLabel49.Text = "Temp Limit:";
			textBoxTempLimit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTempLimit.CustomReportFieldName = "";
			textBoxTempLimit.CustomReportKey = "";
			textBoxTempLimit.CustomReportValueType = 1;
			textBoxTempLimit.IsComboTextBox = false;
			textBoxTempLimit.IsModified = false;
			textBoxTempLimit.Location = new System.Drawing.Point(394, 121);
			textBoxTempLimit.MaxLength = 30;
			textBoxTempLimit.Name = "textBoxTempLimit";
			textBoxTempLimit.ReadOnly = true;
			textBoxTempLimit.Size = new System.Drawing.Size(114, 20);
			textBoxTempLimit.TabIndex = 12;
			textBoxTempLimit.TabStop = false;
			textBoxTempLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			checkBoxAcceptCheque.AutoSize = true;
			checkBoxAcceptCheque.Checked = true;
			checkBoxAcceptCheque.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptCheque.Location = new System.Drawing.Point(17, 130);
			checkBoxAcceptCheque.Name = "checkBoxAcceptCheque";
			checkBoxAcceptCheque.Size = new System.Drawing.Size(142, 17);
			checkBoxAcceptCheque.TabIndex = 11;
			checkBoxAcceptCheque.Text = "Accept cheque payment";
			checkBoxAcceptCheque.UseVisualStyleBackColor = true;
			radioButtonSublimit.AutoSize = true;
			radioButtonSublimit.Location = new System.Drawing.Point(17, 104);
			radioButtonSublimit.Name = "radioButtonSublimit";
			radioButtonSublimit.Size = new System.Drawing.Size(107, 17);
			radioButtonSublimit.TabIndex = 7;
			radioButtonSublimit.Text = "Sublimit of Parent";
			radioButtonSublimit.UseVisualStyleBackColor = true;
			checkBoxAcceptPDC.AutoSize = true;
			checkBoxAcceptPDC.Checked = true;
			checkBoxAcceptPDC.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptPDC.Location = new System.Drawing.Point(17, 149);
			checkBoxAcceptPDC.Name = "checkBoxAcceptPDC";
			checkBoxAcceptPDC.Size = new System.Drawing.Size(195, 17);
			checkBoxAcceptPDC.TabIndex = 13;
			checkBoxAcceptPDC.Text = "Accept post-dated cheque payment";
			checkBoxAcceptPDC.UseVisualStyleBackColor = true;
			textBoxCreditLimitAmount.AllowDecimal = true;
			textBoxCreditLimitAmount.BackColor = System.Drawing.Color.White;
			textBoxCreditLimitAmount.CustomReportFieldName = "";
			textBoxCreditLimitAmount.CustomReportKey = "";
			textBoxCreditLimitAmount.CustomReportValueType = 1;
			textBoxCreditLimitAmount.IsComboTextBox = false;
			textBoxCreditLimitAmount.IsModified = false;
			textBoxCreditLimitAmount.Location = new System.Drawing.Point(394, 79);
			textBoxCreditLimitAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxCreditLimitAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxCreditLimitAmount.Name = "textBoxCreditLimitAmount";
			textBoxCreditLimitAmount.NullText = "0";
			textBoxCreditLimitAmount.Size = new System.Drawing.Size(114, 20);
			textBoxCreditLimitAmount.TabIndex = 6;
			textBoxCreditLimitAmount.Text = "0.00";
			textBoxCreditLimitAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxCreditLimitAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			radioButtonCreditLimitNoCredit.AutoSize = true;
			radioButtonCreditLimitNoCredit.Checked = true;
			radioButtonCreditLimitNoCredit.Location = new System.Drawing.Point(108, 80);
			radioButtonCreditLimitNoCredit.Name = "radioButtonCreditLimitNoCredit";
			radioButtonCreditLimitNoCredit.Size = new System.Drawing.Size(69, 17);
			radioButtonCreditLimitNoCredit.TabIndex = 4;
			radioButtonCreditLimitNoCredit.TabStop = true;
			radioButtonCreditLimitNoCredit.Text = "No Credit";
			radioButtonCreditLimitNoCredit.UseVisualStyleBackColor = true;
			radioButtonCreditLimitUnlimited.AutoSize = true;
			radioButtonCreditLimitUnlimited.Location = new System.Drawing.Point(17, 80);
			radioButtonCreditLimitUnlimited.Name = "radioButtonCreditLimitUnlimited";
			radioButtonCreditLimitUnlimited.Size = new System.Drawing.Size(68, 17);
			radioButtonCreditLimitUnlimited.TabIndex = 4;
			radioButtonCreditLimitUnlimited.Text = "Unlimited";
			radioButtonCreditLimitUnlimited.UseVisualStyleBackColor = true;
			radioButtonCreditLimitAmount.AutoSize = true;
			radioButtonCreditLimitAmount.Location = new System.Drawing.Point(311, 80);
			radioButtonCreditLimitAmount.Name = "radioButtonCreditLimitAmount";
			radioButtonCreditLimitAmount.Size = new System.Drawing.Size(76, 17);
			radioButtonCreditLimitAmount.TabIndex = 5;
			radioButtonCreditLimitAmount.Text = "Amount of:";
			radioButtonCreditLimitAmount.UseVisualStyleBackColor = true;
			radioButtonCreditLimitAmount.CheckedChanged += new System.EventHandler(radioButtonCreditLimitAmount_CheckedChanged);
			textBoxRatingRemarks.BackColor = System.Drawing.Color.White;
			textBoxRatingRemarks.CustomReportFieldName = "";
			textBoxRatingRemarks.CustomReportKey = "";
			textBoxRatingRemarks.CustomReportValueType = 1;
			textBoxRatingRemarks.IsComboTextBox = false;
			textBoxRatingRemarks.IsModified = false;
			textBoxRatingRemarks.Location = new System.Drawing.Point(75, 183);
			textBoxRatingRemarks.MaxLength = 255;
			textBoxRatingRemarks.Multiline = true;
			textBoxRatingRemarks.Name = "textBoxRatingRemarks";
			textBoxRatingRemarks.Size = new System.Drawing.Size(484, 60);
			textBoxRatingRemarks.TabIndex = 114;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(17, 183);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(52, 13);
			mmLabel3.TabIndex = 115;
			mmLabel3.Text = "Remarks:";
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(229, 24);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(68, 13);
			mmLabel9.TabIndex = 94;
			mmLabel9.Text = "Rating Date:";
			dateTimePickerRateDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerRateDate.Location = new System.Drawing.Point(332, 21);
			dateTimePickerRateDate.Name = "dateTimePickerRateDate";
			dateTimePickerRateDate.Size = new System.Drawing.Size(126, 20);
			dateTimePickerRateDate.TabIndex = 1;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(17, 24);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(74, 13);
			mmLabel4.TabIndex = 90;
			mmLabel4.Text = "Credit Rating:";
			comboBoxRatingBy.Assigned = false;
			comboBoxRatingBy.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxRatingBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRatingBy.CustomReportFieldName = "";
			comboBoxRatingBy.CustomReportKey = "";
			comboBoxRatingBy.CustomReportValueType = 1;
			comboBoxRatingBy.DescriptionTextBox = textBoxRatingBy;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRatingBy.DisplayLayout.Appearance = appearance13;
			comboBoxRatingBy.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRatingBy.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRatingBy.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRatingBy.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxRatingBy.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRatingBy.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxRatingBy.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRatingBy.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRatingBy.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRatingBy.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxRatingBy.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRatingBy.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRatingBy.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRatingBy.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxRatingBy.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRatingBy.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRatingBy.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxRatingBy.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxRatingBy.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRatingBy.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxRatingBy.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxRatingBy.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRatingBy.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxRatingBy.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRatingBy.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRatingBy.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRatingBy.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRatingBy.Editable = true;
			comboBoxRatingBy.FilterString = "";
			comboBoxRatingBy.HasAllAccount = false;
			comboBoxRatingBy.HasCustom = false;
			comboBoxRatingBy.IsDataLoaded = false;
			comboBoxRatingBy.Location = new System.Drawing.Point(100, 44);
			comboBoxRatingBy.MaxDropDownItems = 12;
			comboBoxRatingBy.Name = "comboBoxRatingBy";
			comboBoxRatingBy.ShowInactiveItems = false;
			comboBoxRatingBy.ShowQuickAdd = true;
			comboBoxRatingBy.Size = new System.Drawing.Size(119, 20);
			comboBoxRatingBy.TabIndex = 2;
			comboBoxRatingBy.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel44.AutoSize = true;
			mmLabel44.BackColor = System.Drawing.Color.Transparent;
			mmLabel44.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel44.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel44.IsFieldHeader = false;
			mmLabel44.IsRequired = false;
			mmLabel44.Location = new System.Drawing.Point(18, 46);
			mmLabel44.Name = "mmLabel44";
			mmLabel44.PenWidth = 1f;
			mmLabel44.ShowBorder = false;
			mmLabel44.Size = new System.Drawing.Size(57, 13);
			mmLabel44.TabIndex = 92;
			mmLabel44.Text = "Rating By:";
			appearance25.FontData.BoldAsString = "True";
			appearance25.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance25;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(24, 39);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 95;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			appearance27.FontData.BoldAsString = "True";
			appearance27.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance27;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(221, 39);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 97;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance28;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance29;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(101, 36);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(114, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(444, 39);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(38, 13);
			mmLabel2.TabIndex = 85;
			mmLabel2.Text = "Date:";
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = textBoxCustomerName;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance41;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomer.Editable = true;
			comboBoxCustomer.FilterString = "";
			comboBoxCustomer.FilterSysDocID = "";
			comboBoxCustomer.HasAll = false;
			comboBoxCustomer.HasCustom = false;
			comboBoxCustomer.IsDataLoaded = false;
			comboBoxCustomer.Location = new System.Drawing.Point(101, 61);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(114, 20);
			comboBoxCustomer.TabIndex = 3;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(21, 65);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(57, 13);
			mmLabel1.TabIndex = 82;
			mmLabel1.Text = "Customer:";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 0;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			labelSignMessage.AutoSize = true;
			labelSignMessage.BackColor = System.Drawing.Color.Transparent;
			labelSignMessage.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelSignMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelSignMessage.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelSignMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelSignMessage.IsFieldHeader = false;
			labelSignMessage.IsRequired = false;
			labelSignMessage.Location = new System.Drawing.Point(238, 119);
			labelSignMessage.Name = "labelSignMessage";
			labelSignMessage.PenWidth = 1f;
			labelSignMessage.ShowBorder = false;
			labelSignMessage.Size = new System.Drawing.Size(43, 13);
			labelSignMessage.TabIndex = 138;
			labelSignMessage.Text = "No Sign";
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(232, 119);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(53, 14);
			linkLoadImage.TabIndex = 137;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Sign";
			appearance53.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance53;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			mmLabel70.AutoSize = true;
			mmLabel70.BackColor = System.Drawing.Color.Transparent;
			mmLabel70.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel70.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel70.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel70.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel70.IsFieldHeader = false;
			mmLabel70.IsRequired = false;
			mmLabel70.Location = new System.Drawing.Point(217, 86);
			mmLabel70.Name = "mmLabel70";
			mmLabel70.PenWidth = 1f;
			mmLabel70.ShowBorder = false;
			mmLabel70.Size = new System.Drawing.Size(57, 13);
			mmLabel70.TabIndex = 136;
			mmLabel70.Text = "Signature:";
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(255, 150);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 134;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance54.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance54;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(219, 150);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 133;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance55.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance55;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(220, 101);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(80, 48);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 132;
			pictureBoxPhoto.TabStop = false;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(238, 111);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 33);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 135;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(706, 569);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(comboBoxSysDoc);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(ultraGroupBox4);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(textBoxVoucherNumber);
			base.Controls.Add(comboBoxCustomer);
			base.Controls.Add(textBoxCustomerName);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CreditlimitReviewForm";
			Text = "Credit Limit Review";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(LeadStatusForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxRating).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ultraGroupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRatingByPrev).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRatingBy).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
