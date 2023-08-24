using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class EditLotDetailsForm : Form
	{
		private DataTable productLotTable;

		private DataSet currentData;

		private string vendorID = "";

		private bool activatebin = CompanyPreferences.ActivateBin;

		private bool isDefaultLoad;

		private bool materialReservationOnSO = CompanyPreferences.MaterialReservationONSO;

		private bool isNewRecord = true;

		private bool allowEdit = true;

		private string sysDocID = "";

		private string VoucherID = "";

		private int RowIndex;

		private DataSet companyInfo;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonSave;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private DataEntryGrid dataGridItems;

		private TextBox textBoxProductName;

		private XPButton buttonFill;

		private BinComboBox comboBoxGridBin;

		private RackComboBox comboBoxGridRack;

		private TextBox textBoxLotFrom;

		private Label label2;

		private LocationComboBox locationComboBox2;

		private TextBox textBoxLotTo;

		private Label label4;

		private ProductComboBox productComboBox;

		private LocationComboBox locationComboBox;

		private XPButton xpButtonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

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

		public EditLotDetailsForm()
		{
			InitializeComponent();
			base.Activated += IssueLotSelectionForm_Activated;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += UpdateLotDetailsForm_Load;
			base.FormClosing += UpdateLotDetailsForm_FormClosing;
		}

		private void UpdateLotDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				dataGridItems.SaveLayout();
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
				base.ActiveControl = productComboBox;
				dataGridItems.SetupUI();
				Global.GlobalSettings.LoadFormProperties(this);
				SetupGrid();
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FillData()
		{
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			DataRow dataRow = null;
			if ((currentData != null) & (currentData.Tables.Count > 0))
			{
				DataTable dataTable2 = currentData.Tables["Product_Lot"];
				for (int i = 0; i < dataTable2.Rows.Count; i = checked(i + 1))
				{
					dataRow = dataTable2.Rows[i];
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["LotID"] = dataRow["LotNumber"];
					dataRow2["Reference"] = dataRow["Reference"];
					dataRow2["Reference2"] = dataRow["Reference2"];
					dataRow2["Consign#"] = dataRow["Consign#"];
					dataRow2["ExpiryDate"] = dataRow["ExpiryDate"];
					dataRow2["Productiondate"] = dataRow["ProductionDate"];
					dataRow2["LotQty"] = dataRow["AvailableQty"];
					dataRow2["BinID"] = dataRow["BinID"];
					dataRow2["RackID"] = dataRow["RackID"];
					dataRow2["DocID"] = dataRow["DocID"];
					dataRow2["ReceiptNumber"] = dataRow["ReceiptNumber"];
					dataRow2["RowIndex"] = dataRow["RowIndex"];
					sysDocID = dataRow["DocID"].ToString();
					VoucherID = dataRow["ReceiptNumber"].ToString();
					RowIndex = int.Parse(dataRow["RowIndex"].ToString());
					dataTable.Rows.Add(dataRow2);
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.SetupUI();
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("LotID", typeof(int));
				dataTable.Columns.Add("Reference");
				dataTable.Columns.Add("Reference2");
				dataTable.Columns.Add("Consign#");
				dataTable.Columns.Add("BinID");
				dataTable.Columns.Add("RackID");
				dataTable.Columns.Add("ProductionDate", typeof(DateTime));
				dataTable.Columns.Add("ExpiryDate", typeof(DateTime));
				dataTable.Columns.Add("LotQty", typeof(float));
				dataTable.Columns.Add("DocID");
				dataTable.Columns.Add("RowIndex");
				dataTable.Columns.Add("ReceiptNumber");
				dataGridItems.DataSource = dataTable;
				companyInfo = Factory.CompanyInformationSystem.GetCompanyInformation();
				string caption = companyInfo.Tables[0].Rows[0]["LotNoIdentity"].ToString();
				string caption2 = companyInfo.Tables[0].Rows[0]["Reference2"].ToString();
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["LotID"].Header.Caption = "Lot Number";
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].Header.Caption = caption;
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"].Header.Caption = caption2;
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Header.Caption = "Bin";
				dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].Header.Caption = "Rack";
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].Header.Caption = "Prod.Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].Header.Caption = "Exp.Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"].Header.Caption = "Lot Qty";
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].ValueList = comboBoxGridBin;
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].ValueList = comboBoxGridRack;
				dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].Header.Appearance.Cursor = Cursors.Hand;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["LotID"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["Consign#"].CellActivation = Activation.NoEdit;
				Activation activation5 = ultraGridColumn.CellActivation = (ultraGridColumn2.CellActivation = activation2);
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["LotID"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Consign#"].CellAppearance.BackColor = Color.WhiteSmoke;
				Color color4 = cellAppearance.BackColor = (cellAppearance2.BackColor = color);
				dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"].Format = "n";
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["DocID"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptNumber"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["RowIndex"].Hidden = true;
				bool hidden = ultraGridColumn4.Hidden = flag2;
				ultraGridColumn3.Hidden = hidden;
				dataGridItems.AllowAddNew = false;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void IssueLotSelectionForm_Activated(object sender, EventArgs e)
		{
			productComboBox.Focus();
		}

		private void Grid_AfterCellActivate(object sender, EventArgs e)
		{
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				SaveData();
			}
			catch (Exception e2)
			{
				base.DialogResult = DialogResult.None;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void labelCustomize_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void checkBoxUnitPriceOverWrite_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxShowAvailableLots_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void LoadData()
		{
			try
			{
				currentData = Factory.ProductSystem.GetProductLotDetails(productComboBox.SelectedID, locationComboBox.SelectedID, textBoxLotFrom.Text, textBoxLotTo.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonFill_Click(object sender, EventArgs e)
		{
			if (productComboBox.SelectedID != "")
			{
				LoadData();
				FillData();
			}
			else
			{
				ErrorHelper.InformationMessage("Please select one product");
			}
		}

		private void CreateData()
		{
			currentData.Tables.Remove("Product_Lot");
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Product_Lot";
			dataTable.Columns.Add("LotNumber", typeof(int));
			dataTable.Columns.Add("Reference");
			dataTable.Columns.Add("Reference2");
			dataTable.Columns.Add("Consign#");
			dataTable.Columns.Add("BinID");
			dataTable.Columns.Add("RackID");
			dataTable.Columns.Add("ProductionDate", typeof(DateTime));
			dataTable.Columns.Add("ExpiryDate", typeof(DateTime));
			dataTable.Columns.Add("LotQty", typeof(float));
			dataTable.Columns.Add("SysDocID");
			dataTable.Columns.Add("VoucherID");
			dataTable.Columns.Add("RowIndex", typeof(int));
			currentData.Tables.Add(dataTable);
		}

		private bool GetData()
		{
			try
			{
				CreateData();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow = currentData.Tables[0].NewRow();
					dataRow.BeginEdit();
					dataRow["LotNumber"] = row.Cells["LotID"].Value.ToString();
					dataRow["Reference"] = row.Cells["Reference"].Value.ToString();
					dataRow["Reference2"] = row.Cells["Reference2"].Value.ToString();
					if (row.Cells["BinID"].Value != null && row.Cells["BinID"].Value.ToString() != "")
					{
						dataRow["BinID"] = row.Cells["BinID"].Value.ToString();
					}
					else
					{
						dataRow["BinID"] = DBNull.Value;
					}
					if (row.Cells["RackID"].Value != null && row.Cells["RackID"].Value.ToString() != "")
					{
						dataRow["RackID"] = row.Cells["RackID"].Value.ToString();
					}
					else
					{
						dataRow["RackID"] = DBNull.Value;
					}
					if (row.Cells["ProductionDate"].Value != null && row.Cells["ProductionDate"].Value.ToString() != "")
					{
						dataRow["ProductionDate"] = DateTime.Parse(row.Cells["ProductionDate"].Value.ToString());
					}
					else
					{
						dataRow["ProductionDate"] = DBNull.Value;
					}
					if (row.Cells["ExpiryDate"].Value != null && row.Cells["ExpiryDate"].Value.ToString() != "")
					{
						dataRow["ExpiryDate"] = DateTime.Parse(row.Cells["ExpiryDate"].Value.ToString());
					}
					else
					{
						dataRow["ExpiryDate"] = DBNull.Value;
					}
					dataRow["SysDocID"] = row.Cells["DocID"].Value.ToString();
					dataRow["VoucherID"] = row.Cells["ReceiptNumber"].Value.ToString();
					dataRow["RowIndex"] = row.Cells["RowIndex"].Value.ToString();
					dataRow.EndEdit();
					currentData.Tables[0].Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool SaveData()
		{
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
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
		{
			if (!allowEdit)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "You cannot edit this transfer transaction because it is already accepted or rejected.", "Document is in use.");
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			bool flag = false;
			try
			{
				bool flag2 = Factory.ProductSystem.UpdateLotReceivingDetails(currentData, null);
				if (flag2)
				{
					flag2 &= Factory.ProductSystem.UpdateLotDetails(currentData, null);
				}
				if (flag2)
				{
					flag = true;
				}
				if (!flag2)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				return flag2;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			finally
			{
				if (flag && clearAfter)
				{
					ClearForm();
					IsNewRecord = true;
				}
			}
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

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditItem(productComboBox.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(locationComboBox.SelectedID);
		}

		private void ClearForm()
		{
			try
			{
				productComboBox.Clear();
				locationComboBox.Clear();
				textBoxLotFrom.Clear();
				textBoxLotTo.Clear();
				textBoxProductName.Clear();
				(dataGridItems.DataSource as DataTable).Rows.Clear();
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
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.EditLotDetailsForm));
			panelButtons = new System.Windows.Forms.Panel();
			xpButtonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			textBoxProductName = new System.Windows.Forms.TextBox();
			buttonFill = new Micromind.UISupport.XPButton();
			textBoxLotFrom = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxLotTo = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			locationComboBox = new Micromind.DataControls.LocationComboBox();
			productComboBox = new Micromind.DataControls.ProductComboBox();
			comboBoxGridRack = new Micromind.DataControls.RackComboBox();
			comboBoxGridBin = new Micromind.DataControls.BinComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)locationComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)productComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(xpButtonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 421);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(801, 40);
			panelButtons.TabIndex = 6;
			xpButtonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButtonNew.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButtonNew.BackColor = System.Drawing.Color.DarkGray;
			xpButtonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButtonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButtonNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButtonNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			xpButtonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButtonNew.Location = new System.Drawing.Point(587, 8);
			xpButtonNew.Name = "xpButtonNew";
			xpButtonNew.Size = new System.Drawing.Size(96, 24);
			xpButtonNew.TabIndex = 1;
			xpButtonNew.Text = "&New";
			xpButtonNew.UseVisualStyleBackColor = false;
			xpButtonNew.Click += new System.EventHandler(xpButtonNew_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.DarkGray;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(482, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(801, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(691, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			textBoxProductName.Location = new System.Drawing.Point(223, 10);
			textBoxProductName.Name = "textBoxProductName";
			textBoxProductName.ReadOnly = true;
			textBoxProductName.Size = new System.Drawing.Size(356, 20);
			textBoxProductName.TabIndex = 15;
			buttonFill.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonFill.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonFill.BackColor = System.Drawing.Color.DarkGray;
			buttonFill.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonFill.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonFill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonFill.Location = new System.Drawing.Point(693, 34);
			buttonFill.Name = "buttonFill";
			buttonFill.Size = new System.Drawing.Size(96, 24);
			buttonFill.TabIndex = 4;
			buttonFill.Text = "Fill Data";
			buttonFill.UseVisualStyleBackColor = false;
			buttonFill.Click += new System.EventHandler(buttonFill_Click);
			textBoxLotFrom.Location = new System.Drawing.Point(282, 32);
			textBoxLotFrom.Name = "textBoxLotFrom";
			textBoxLotFrom.Size = new System.Drawing.Size(80, 20);
			textBoxLotFrom.TabIndex = 2;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(220, 37);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(60, 13);
			label2.TabIndex = 21;
			label2.Text = "Lot From:";
			textBoxLotTo.Location = new System.Drawing.Point(422, 32);
			textBoxLotTo.Name = "textBoxLotTo";
			textBoxLotTo.Size = new System.Drawing.Size(80, 20);
			textBoxLotTo.TabIndex = 3;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(371, 37);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(45, 13);
			label4.TabIndex = 24;
			label4.Text = "Lot To:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(15, 13);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(35, 15);
			ultraFormattedLinkLabel4.TabIndex = 25;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Item:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance3;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(14, 35);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(56, 15);
			ultraFormattedLinkLabel1.TabIndex = 26;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Location:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			locationComboBox.Assigned = false;
			locationComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			locationComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			locationComboBox.CustomReportFieldName = "";
			locationComboBox.CustomReportKey = "";
			locationComboBox.CustomReportValueType = 1;
			locationComboBox.DescriptionTextBox = null;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			locationComboBox.DisplayLayout.Appearance = appearance5;
			locationComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			locationComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			locationComboBox.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			locationComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			locationComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			locationComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			locationComboBox.DisplayLayout.MaxColScrollRegions = 1;
			locationComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			locationComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			locationComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			locationComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			locationComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			locationComboBox.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			locationComboBox.DisplayLayout.Override.CellAppearance = appearance12;
			locationComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			locationComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			locationComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			locationComboBox.DisplayLayout.Override.HeaderAppearance = appearance14;
			locationComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			locationComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			locationComboBox.DisplayLayout.Override.RowAppearance = appearance15;
			locationComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			locationComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			locationComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			locationComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			locationComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			locationComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			locationComboBox.Editable = true;
			locationComboBox.FilterString = "";
			locationComboBox.HasAllAccount = false;
			locationComboBox.HasCustom = false;
			locationComboBox.IsDataLoaded = false;
			locationComboBox.Location = new System.Drawing.Point(76, 32);
			locationComboBox.MaxDropDownItems = 12;
			locationComboBox.Name = "locationComboBox";
			locationComboBox.ShowAll = false;
			locationComboBox.ShowConsignIn = false;
			locationComboBox.ShowConsignOut = false;
			locationComboBox.ShowDefaultLocationOnly = false;
			locationComboBox.ShowInactiveItems = false;
			locationComboBox.ShowNormalLocations = true;
			locationComboBox.ShowPOSOnly = false;
			locationComboBox.ShowQuickAdd = true;
			locationComboBox.ShowWarehouseOnly = false;
			locationComboBox.Size = new System.Drawing.Size(138, 20);
			locationComboBox.TabIndex = 1;
			locationComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			productComboBox.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			productComboBox.Assigned = false;
			productComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			productComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			productComboBox.CustomReportFieldName = "";
			productComboBox.CustomReportKey = "";
			productComboBox.CustomReportValueType = 1;
			productComboBox.DescriptionTextBox = textBoxProductName;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			productComboBox.DisplayLayout.Appearance = appearance17;
			productComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			productComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			productComboBox.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			productComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			productComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			productComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			productComboBox.DisplayLayout.MaxColScrollRegions = 1;
			productComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			productComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			productComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			productComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			productComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			productComboBox.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			productComboBox.DisplayLayout.Override.CellAppearance = appearance24;
			productComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			productComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			productComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			productComboBox.DisplayLayout.Override.HeaderAppearance = appearance26;
			productComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			productComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			productComboBox.DisplayLayout.Override.RowAppearance = appearance27;
			productComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			productComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			productComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			productComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			productComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			productComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			productComboBox.Editable = true;
			productComboBox.FilterCustomerID = "";
			productComboBox.FilterString = "";
			productComboBox.FilterSysDocID = "";
			productComboBox.HasAllAccount = false;
			productComboBox.HasCustom = false;
			productComboBox.IsDataLoaded = false;
			productComboBox.Location = new System.Drawing.Point(76, 10);
			productComboBox.MaxDropDownItems = 12;
			productComboBox.Name = "productComboBox";
			productComboBox.Show3PLItems = true;
			productComboBox.ShowInactiveItems = false;
			productComboBox.ShowOnlyLotItems = true;
			productComboBox.ShowQuickAdd = true;
			productComboBox.Size = new System.Drawing.Size(138, 20);
			productComboBox.TabIndex = 0;
			productComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridRack.Assigned = false;
			comboBoxGridRack.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridRack.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridRack.CustomReportFieldName = "";
			comboBoxGridRack.CustomReportKey = "";
			comboBoxGridRack.CustomReportValueType = 1;
			comboBoxGridRack.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridRack.DisplayLayout.Appearance = appearance29;
			comboBoxGridRack.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridRack.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxGridRack.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxGridRack.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridRack.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridRack.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridRack.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridRack.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxGridRack.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridRack.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxGridRack.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxGridRack.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridRack.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridRack.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxGridRack.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridRack.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxGridRack.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridRack.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridRack.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridRack.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridRack.Editable = true;
			comboBoxGridRack.FilterBinID = "";
			comboBoxGridRack.FilterString = "";
			comboBoxGridRack.HasAllAccount = false;
			comboBoxGridRack.HasCustom = false;
			comboBoxGridRack.IsDataLoaded = false;
			comboBoxGridRack.Location = new System.Drawing.Point(312, 216);
			comboBoxGridRack.MaxDropDownItems = 12;
			comboBoxGridRack.Name = "comboBoxGridRack";
			comboBoxGridRack.ShowInactiveItems = false;
			comboBoxGridRack.ShowQuickAdd = true;
			comboBoxGridRack.Size = new System.Drawing.Size(100, 20);
			comboBoxGridRack.TabIndex = 19;
			comboBoxGridRack.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridRack.Visible = false;
			comboBoxGridBin.Assigned = false;
			comboBoxGridBin.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridBin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridBin.CustomReportFieldName = "";
			comboBoxGridBin.CustomReportKey = "";
			comboBoxGridBin.CustomReportValueType = 1;
			comboBoxGridBin.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridBin.DisplayLayout.Appearance = appearance41;
			comboBoxGridBin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridBin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxGridBin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBin.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxGridBin.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridBin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridBin.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridBin.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxGridBin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridBin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridBin.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxGridBin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridBin.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxGridBin.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxGridBin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridBin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridBin.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxGridBin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridBin.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxGridBin.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridBin.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridBin.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridBin.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridBin.Editable = true;
			comboBoxGridBin.FilterString = "";
			comboBoxGridBin.HasAllAccount = false;
			comboBoxGridBin.HasCustom = false;
			comboBoxGridBin.IsDataLoaded = false;
			comboBoxGridBin.Location = new System.Drawing.Point(254, 175);
			comboBoxGridBin.MaxDropDownItems = 12;
			comboBoxGridBin.Name = "comboBoxGridBin";
			comboBoxGridBin.ShowInactiveItems = false;
			comboBoxGridBin.ShowQuickAdd = true;
			comboBoxGridBin.Size = new System.Drawing.Size(100, 20);
			comboBoxGridBin.TabIndex = 18;
			comboBoxGridBin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridBin.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.AllowCustomizeHeaders = true;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance53;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance60;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance62;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance63;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(10, 59);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(778, 346);
			dataGridItems.TabIndex = 5;
			dataGridItems.Text = "dataEntryGrid1";
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(801, 461);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(locationComboBox);
			base.Controls.Add(productComboBox);
			base.Controls.Add(textBoxLotTo);
			base.Controls.Add(label4);
			base.Controls.Add(textBoxLotFrom);
			base.Controls.Add(label2);
			base.Controls.Add(comboBoxGridRack);
			base.Controls.Add(comboBoxGridBin);
			base.Controls.Add(buttonFill);
			base.Controls.Add(textBoxProductName);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EditLotDetailsForm";
			Text = "Update Lot Details";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)locationComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)productComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
