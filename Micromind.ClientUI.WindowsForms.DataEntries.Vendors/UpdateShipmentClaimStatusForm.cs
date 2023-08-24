using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class UpdateShipmentClaimStatusForm : Form
	{
		private const string TABLENAME_CONST = "Purchase_Receipt";

		private PurchaseReceiptData currentData;

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private IContainer components;

		private XPButton buttonFill;

		private LocationComboBox locationComboBox2;

		private ComboBox comboBoxStatus;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxVoucherNumber;

		private TextBox textBoxRef1;

		private TextBox textBoxRef2;

		private Label label4;

		private Label label1;

		private TextBox textBoxGroupName;

		private TextBox textBoxClaimAmount;

		private Label label2;

		private Label label3;

		private Label label5;

		private TextBox textBoxNote;

		private Label label6;

		private GroupBox groupBox1;

		private DataGridList dataGridLot;

		private XPButton buttonSalesStatistics;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonComments;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonInformation;

		private Label label8;

		private Label label7;

		private Label labelAmountLC;

		private AmountTextBox textBoxAmountLC;

		private CurrencySelector comboBoxCurrency;

		private Label labelCurrency;

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public string SystemDocID
		{
			get
			{
				return comboBoxSysDoc.SelectedID;
			}
			set
			{
				comboBoxSysDoc.SelectedID = value;
			}
		}

		public string VoucherID
		{
			get
			{
				return textBoxVoucherNumber.Text;
			}
			set
			{
				textBoxVoucherNumber.Text = value;
			}
		}

		public UpdateShipmentClaimStatusForm()
		{
			InitializeComponent();
			base.Activated += IssueLotSelectionForm_Activated;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += UpdateLotDetailsForm_Load;
			base.FormClosing += UpdateLotDetailsForm_FormClosing;
			textBoxClaimAmount.TextChanged += textBoxClaimAmount_TextChanged;
			comboBoxCurrency.SelectedIndexChanged += comboBoxCurrency_SelectedIndexChanged;
		}

		private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalculateBaseAmount();
		}

		private void textBoxClaimAmount_TextChanged(object sender, EventArgs e)
		{
			CalculateBaseAmount();
		}

		private void UpdateLotDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void UpdateLotDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupListGrid();
				Global.GlobalSettings.LoadFormProperties(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupListGrid()
		{
			dataGridLot.ApplyUIDesign();
			dataGridLot.ApplyFormat();
			if (!dataGridLot.DisplayLayout.Bands[0].Summaries.Exists("TotalQty"))
			{
				dataGridLot.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridLot.DisplayLayout.Bands[0].Columns["Qty Received"], SummaryPosition.UseSummaryPositionColumn);
				dataGridLot.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridLot.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridLot.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
			}
			if (!dataGridLot.DisplayLayout.Bands[0].Summaries.Exists("SoldQty"))
			{
				dataGridLot.DisplayLayout.Bands[0].Summaries.Add("SoldQty", SummaryType.Sum, dataGridLot.DisplayLayout.Bands[0].Columns["Sold"], SummaryPosition.UseSummaryPositionColumn);
				dataGridLot.DisplayLayout.Bands[0].Summaries["SoldQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridLot.DisplayLayout.Bands[0].Summaries["SoldQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridLot.DisplayLayout.Bands[0].Summaries["SoldQty"].DisplayFormat = "{0:n}";
			}
			if (!dataGridLot.DisplayLayout.Bands[0].Summaries.Exists("ReturnedQty"))
			{
				dataGridLot.DisplayLayout.Bands[0].Summaries.Add("ReturnedQty", SummaryType.Sum, dataGridLot.DisplayLayout.Bands[0].Columns["Returned"], SummaryPosition.UseSummaryPositionColumn);
				dataGridLot.DisplayLayout.Bands[0].Summaries["ReturnedQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridLot.DisplayLayout.Bands[0].Summaries["ReturnedQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridLot.DisplayLayout.Bands[0].Summaries["ReturnedQty"].DisplayFormat = "{0:n}";
			}
			if (!dataGridLot.DisplayLayout.Bands[0].Summaries.Exists("BalanceQty"))
			{
				dataGridLot.DisplayLayout.Bands[0].Summaries.Add("BalanceQty", SummaryType.Sum, dataGridLot.DisplayLayout.Bands[0].Columns["Balance"], SummaryPosition.UseSummaryPositionColumn);
				dataGridLot.DisplayLayout.Bands[0].Summaries["BalanceQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridLot.DisplayLayout.Bands[0].Summaries["BalanceQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridLot.DisplayLayout.Bands[0].Summaries["BalanceQty"].DisplayFormat = "{0:n}";
			}
		}

		private void IssueLotSelectionForm_Activated(object sender, EventArgs e)
		{
		}

		private void buttonFill_Click(object sender, EventArgs e)
		{
			try
			{
				if (GetData())
				{
					if (!Factory.PurchaseReceiptSystem.UpdateContainerDetails(currentData))
					{
						ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
					}
					else
					{
						ClearForm();
						Close();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void LoadData()
		{
			try
			{
				FillLotDetails();
				currentData = Factory.PurchaseReceiptSystem.GetPurchaseReceiptByID(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
				if (currentData != null)
				{
					FillData();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Purchase_Receipt"].Rows[0];
					if (dataRow["ClaimStatus"] != DBNull.Value)
					{
						comboBoxStatus.SelectedIndex = int.Parse(dataRow["ClaimStatus"].ToString());
					}
					else
					{
						comboBoxStatus.SelectedIndex = 0;
					}
					textBoxGroupName.Text = dataRow["GroupName"].ToString();
					if (dataRow["ClaimCurrencyID"] != DBNull.Value)
					{
						comboBoxCurrency.SelectedID = dataRow["ClaimCurrencyID"].ToString();
					}
					else
					{
						comboBoxCurrency.SelectedID = Global.BaseCurrencyID;
					}
					if (dataRow["ClaimAmount"] != DBNull.Value)
					{
						if (dataRow["ClaimAmountFC"] != DBNull.Value)
						{
							textBoxClaimAmount.Text = decimal.Parse(dataRow["ClaimAmountFC"].ToString()).ToString(Format.TotalAmountFormat);
							textBoxAmountLC.Text = decimal.Parse(dataRow["ClaimAmount"].ToString()).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxClaimAmount.Text = decimal.Parse(dataRow["ClaimAmount"].ToString()).ToString(Format.TotalAmountFormat);
							textBoxAmountLC.Text = decimal.Parse(dataRow["ClaimAmount"].ToString()).ToString(Format.TotalAmountFormat);
						}
					}
					else
					{
						textBoxClaimAmount.Text = default(decimal).ToString(Format.TotalAmountFormat);
					}
					textBoxRef1.Text = dataRow["ClaimRef1"].ToString();
					textBoxRef2.Text = dataRow["ClaimRef2"].ToString();
					textBoxNote.Text = dataRow["ClaimRemarks"].ToString();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillLotDetails()
		{
			DataSet containerLotData = Factory.PurchaseReceiptSystem.GetContainerLotData(SystemDocID, VoucherID);
			dataGridLot.DataSource = containerLotData;
			SetupListGrid();
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				AllowEditTransDiffLocation = false;
			}
			else
			{
				AllowEditTransDiffLocation = true;
			}
		}

		private void xpButtonNew_Click(object sender, EventArgs e)
		{
			ClearForm();
		}

		private void ClearForm()
		{
			try
			{
				textBoxGroupName.Clear();
				comboBoxStatus.SelectedIndex = -1;
				textBoxRef1.Clear();
				textBoxRef2.Clear();
				textBoxNote.Clear();
				textBoxClaimAmount.Clear();
				comboBoxSysDoc.Clear();
				textBoxVoucherNumber.Clear();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PurchaseReceiptData();
				}
				DataRow dataRow = currentData.PurchaseReceiptTable.NewRow();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["GroupName"] = textBoxGroupName.Text;
				dataRow["ClaimRef1"] = textBoxRef1.Text;
				dataRow["ClaimRef2"] = textBoxRef2.Text;
				dataRow["ClaimRemarks"] = textBoxNote.Text;
				if (comboBoxStatus.SelectedIndex > -1)
				{
					dataRow["ClaimStatus"] = comboBoxStatus.SelectedIndex;
				}
				else
				{
					dataRow["ClaimStatus"] = DBNull.Value;
				}
				if (comboBoxCurrency.SelectedID != "")
				{
					dataRow["ClaimCurrencyID"] = comboBoxCurrency.SelectedID;
					dataRow["ClaimCurrencyRate"] = comboBoxCurrency.Rate;
					if (comboBoxCurrency.SelectedID != Global.BaseCurrencyID)
					{
						dataRow["ClaimAmountFC"] = textBoxClaimAmount.Text;
					}
					if (comboBoxCurrency.SelectedID != Global.BaseCurrencyID)
					{
						dataRow["ClaimAmountFC"] = textBoxClaimAmount.Text;
						dataRow["ClaimAmount"] = textBoxAmountLC.Text;
					}
					else
					{
						dataRow["ClaimAmount"] = textBoxClaimAmount.Text;
						dataRow["ClaimAmountFC"] = DBNull.Value;
					}
				}
				else
				{
					dataRow["ClaimCurrencyID"] = DBNull.Value;
					dataRow["ClaimCurrencyRate"] = DBNull.Value;
				}
				dataRow.EndEdit();
				currentData.PurchaseReceiptTable.Rows.Add(dataRow);
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				DocManagementForm docManagementForm = new DocManagementForm();
				docManagementForm.EntityID = comboBoxSysDoc.SelectedID + "-" + textBoxVoucherNumber.Text.Trim();
				docManagementForm.EntitySysDocID = "US_CS";
				docManagementForm.EntityName = "US_CS";
				docManagementForm.EntityType = EntityTypesEnum.Transactions;
				docManagementForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonComments_Click(object sender, EventArgs e)
		{
			try
			{
				EntityCommentsForm entityCommentsForm = new EntityCommentsForm();
				entityCommentsForm.EntityID = comboBoxSysDoc.SelectedID + "-" + textBoxVoucherNumber.Text.Trim();
				entityCommentsForm.EntityName = "US_CS";
				entityCommentsForm.EntityType = EntityTypesEnum.Transactions;
				entityCommentsForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			FormHelper.ShowDocumentInfo(comboBoxSysDoc.SelectedID + "-" + textBoxVoucherNumber.Text, "US_CS", this);
		}

		private void buttonSalesStatistics_Click(object sender, EventArgs e)
		{
			ShipmentsSalesStatisticsForm shipmentsSalesStatisticsForm = new ShipmentsSalesStatisticsForm();
			shipmentsSalesStatisticsForm.SystemDocID = comboBoxSysDoc.SelectedID;
			shipmentsSalesStatisticsForm.VoucherID = textBoxVoucherNumber.Text;
			shipmentsSalesStatisticsForm.ShowDialog();
		}

		private void CalculateBaseAmount()
		{
			decimal result = default(decimal);
			decimal.TryParse(textBoxClaimAmount.Text, out result);
			if (comboBoxCurrency.IsBaseCurrency || comboBoxCurrency.SelectedID == "")
			{
				textBoxAmountLC.Text = result.ToString(Format.TotalAmountFormat);
			}
			else
			{
				textBoxAmountLC.Text = comboBoxCurrency.GetBaseEquivalant(result).ToString(Format.TotalAmountFormat);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.UpdateShipmentClaimStatusForm));
			buttonFill = new Micromind.UISupport.XPButton();
			comboBoxStatus = new System.Windows.Forms.ComboBox();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			textBoxGroupName = new System.Windows.Forms.TextBox();
			textBoxClaimAmount = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxNote = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			groupBox1 = new System.Windows.Forms.GroupBox();
			label8 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			buttonSalesStatistics = new Micromind.UISupport.XPButton();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			dataGridLot = new Micromind.UISupport.DataGridList(components);
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonComments = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			labelAmountLC = new System.Windows.Forms.Label();
			textBoxAmountLC = new Micromind.UISupport.AmountTextBox();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			labelCurrency = new System.Windows.Forms.Label();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridLot).BeginInit();
			toolStrip1.SuspendLayout();
			SuspendLayout();
			buttonFill.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonFill.BackColor = System.Drawing.Color.DarkGray;
			buttonFill.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonFill.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonFill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonFill.Location = new System.Drawing.Point(95, 219);
			buttonFill.Name = "buttonFill";
			buttonFill.Size = new System.Drawing.Size(96, 24);
			buttonFill.TabIndex = 8;
			buttonFill.Text = "Save";
			buttonFill.UseVisualStyleBackColor = false;
			buttonFill.Click += new System.EventHandler(buttonFill_Click);
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Items.AddRange(new object[3]
			{
				"Open",
				"Pending",
				"Closed"
			});
			comboBoxStatus.Location = new System.Drawing.Point(97, 44);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(147, 21);
			comboBoxStatus.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(338, 22);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.ReadOnly = true;
			textBoxVoucherNumber.Size = new System.Drawing.Size(144, 20);
			textBoxVoucherNumber.TabIndex = 150;
			textBoxRef1.Location = new System.Drawing.Point(97, 67);
			textBoxRef1.MaxLength = 30;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(147, 20);
			textBoxRef1.TabIndex = 5;
			textBoxRef2.Location = new System.Drawing.Point(338, 66);
			textBoxRef2.MaxLength = 30;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.Size = new System.Drawing.Size(144, 20);
			textBoxRef2.TabIndex = 6;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(251, 69);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(69, 13);
			label4.TabIndex = 174;
			label4.Text = "Reference 2:";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(11, 71);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(69, 13);
			label1.TabIndex = 173;
			label1.Text = "Reference 1:";
			textBoxGroupName.Location = new System.Drawing.Point(338, 44);
			textBoxGroupName.MaxLength = 64;
			textBoxGroupName.Name = "textBoxGroupName";
			textBoxGroupName.Size = new System.Drawing.Size(144, 20);
			textBoxGroupName.TabIndex = 3;
			textBoxClaimAmount.Location = new System.Drawing.Point(97, 89);
			textBoxClaimAmount.MaxLength = 30;
			textBoxClaimAmount.Name = "textBoxClaimAmount";
			textBoxClaimAmount.Size = new System.Drawing.Size(147, 20);
			textBoxClaimAmount.TabIndex = 4;
			textBoxClaimAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(11, 92);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(71, 13);
			label2.TabIndex = 178;
			label2.Text = "Claim Amount";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(251, 47);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(73, 13);
			label3.TabIndex = 177;
			label3.Text = "Group Name :";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(11, 47);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(43, 13);
			label5.TabIndex = 179;
			label5.Text = "Status :";
			textBoxNote.Location = new System.Drawing.Point(97, 133);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(385, 79);
			textBoxNote.TabIndex = 7;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(11, 136);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(55, 13);
			label6.TabIndex = 181;
			label6.Text = "Remarks :";
			groupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			groupBox1.Controls.Add(labelAmountLC);
			groupBox1.Controls.Add(textBoxAmountLC);
			groupBox1.Controls.Add(comboBoxCurrency);
			groupBox1.Controls.Add(labelCurrency);
			groupBox1.Controls.Add(label8);
			groupBox1.Controls.Add(label7);
			groupBox1.Controls.Add(buttonSalesStatistics);
			groupBox1.Controls.Add(comboBoxStatus);
			groupBox1.Controls.Add(textBoxNote);
			groupBox1.Controls.Add(buttonFill);
			groupBox1.Controls.Add(label6);
			groupBox1.Controls.Add(textBoxVoucherNumber);
			groupBox1.Controls.Add(label5);
			groupBox1.Controls.Add(textBoxGroupName);
			groupBox1.Controls.Add(comboBoxSysDoc);
			groupBox1.Controls.Add(textBoxClaimAmount);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(label4);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(textBoxRef2);
			groupBox1.Controls.Add(textBoxRef1);
			groupBox1.Location = new System.Drawing.Point(12, 38);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(639, 254);
			groupBox1.TabIndex = 183;
			groupBox1.TabStop = false;
			groupBox1.Text = "Shipment Claim Status";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(251, 25);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(52, 13);
			label8.TabIndex = 184;
			label8.Text = "Doc NO :";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(11, 25);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(47, 13);
			label7.TabIndex = 183;
			label7.Text = "Doc ID :";
			buttonSalesStatistics.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSalesStatistics.BackColor = System.Drawing.Color.DarkGray;
			buttonSalesStatistics.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSalesStatistics.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSalesStatistics.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSalesStatistics.Location = new System.Drawing.Point(197, 219);
			buttonSalesStatistics.Name = "buttonSalesStatistics";
			buttonSalesStatistics.Size = new System.Drawing.Size(96, 24);
			buttonSalesStatistics.TabIndex = 182;
			buttonSalesStatistics.Text = "Sales Statistics";
			buttonSalesStatistics.UseVisualStyleBackColor = false;
			buttonSalesStatistics.Click += new System.EventHandler(buttonSalesStatistics_Click);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.Enabled = false;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(97, 22);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(147, 20);
			comboBoxSysDoc.TabIndex = 148;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dataGridLot.AllowUnfittedView = false;
			dataGridLot.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridLot.DisplayLayout.Appearance = appearance13;
			dataGridLot.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridLot.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridLot.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridLot.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridLot.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridLot.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridLot.DisplayLayout.MaxColScrollRegions = 1;
			dataGridLot.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridLot.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridLot.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridLot.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridLot.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridLot.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridLot.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridLot.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridLot.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridLot.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridLot.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridLot.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridLot.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridLot.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridLot.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridLot.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridLot.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridLot.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridLot.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridLot.LoadLayoutFailed = false;
			dataGridLot.Location = new System.Drawing.Point(12, 298);
			dataGridLot.Name = "dataGridLot";
			dataGridLot.ShowDeleteMenu = false;
			dataGridLot.ShowMinusInRed = true;
			dataGridLot.ShowNewMenu = false;
			dataGridLot.Size = new System.Drawing.Size(639, 219);
			dataGridLot.TabIndex = 184;
			dataGridLot.Text = "dataGridList1";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				toolStripButtonAttach,
				toolStripButtonComments,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(660, 31);
			toolStrip1.TabIndex = 185;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButtonComments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonComments.Image = Micromind.ClientUI.Properties.Resources.comment;
			toolStripButtonComments.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonComments.Name = "toolStripButtonComments";
			toolStripButtonComments.Size = new System.Drawing.Size(28, 28);
			toolStripButtonComments.Text = "Comments...";
			toolStripButtonComments.Click += new System.EventHandler(toolStripButtonComments_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			labelAmountLC.AutoSize = true;
			labelAmountLC.Location = new System.Drawing.Point(11, 116);
			labelAmountLC.Name = "labelAmountLC";
			labelAmountLC.Size = new System.Drawing.Size(62, 13);
			labelAmountLC.TabIndex = 188;
			labelAmountLC.Text = "Amount LC:";
			textBoxAmountLC.AllowDecimal = true;
			textBoxAmountLC.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmountLC.CustomReportFieldName = "";
			textBoxAmountLC.CustomReportKey = "";
			textBoxAmountLC.CustomReportValueType = 1;
			textBoxAmountLC.IsComboTextBox = false;
			textBoxAmountLC.IsModified = false;
			textBoxAmountLC.Location = new System.Drawing.Point(97, 111);
			textBoxAmountLC.MaxLength = 15;
			textBoxAmountLC.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAmountLC.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAmountLC.Name = "textBoxAmountLC";
			textBoxAmountLC.NullText = "0";
			textBoxAmountLC.ReadOnly = true;
			textBoxAmountLC.Size = new System.Drawing.Size(147, 20);
			textBoxAmountLC.TabIndex = 186;
			textBoxAmountLC.Text = "0.00";
			textBoxAmountLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmountLC.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(338, 88);
			comboBoxCurrency.MaximumSize = new System.Drawing.Size(99999, 20);
			comboBoxCurrency.MinimumSize = new System.Drawing.Size(5, 20);
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			comboBoxCurrency.SelectedID = "";
			comboBoxCurrency.Size = new System.Drawing.Size(144, 20);
			comboBoxCurrency.TabIndex = 185;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(251, 91);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 13);
			labelCurrency.TabIndex = 187;
			labelCurrency.Text = "Currency:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(660, 529);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(dataGridLot);
			base.Controls.Add(groupBox1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UpdateShipmentClaimStatusForm";
			Text = "Update Shipment Claim Status";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridLot).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
