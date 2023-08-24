using Infragistics.Win;
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
	public class ProductCategoryDetailsForm : Form, IForm
	{
		private ProductCategoryData currentData;

		private const string TABLENAME_CONST = "Product_Category";

		private const string IDFIELD_CONST = "CategoryID";

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

		private ProductCategoryComboBox comboBoxParentCategory;

		private MMLabel mmLabel2;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private NumberTextBox textBoxAge1;

		private MMLabel mmLabel3;

		private MMLabel mmLabel5;

		private NumberTextBox textBoxAge2;

		private MMLabel mmLabel6;

		private NumberTextBox textBoxAge3;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel7;

		private NumberTextBox textBoxSpecialPricePercent;

		private MMLabel mmLabel8;

		private NumberTextBox textBoxWholeSalePricePercent;

		private MMLabel mmLabel9;

		private NumberTextBox textBoxStdPricePercent;

		private MMLabel mmLabel10;

		private NumberTextBox textBoxMinPricePercent;

		private Label label3;

		private PercentTextBox textBoxCommissionPercent;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4008;

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

		public ProductCategoryDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ProductCategoryDetailsForm_Load;
			comboBoxParentCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ProductCategoryData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ProductCategoryTable.Rows[0] : currentData.ProductCategoryTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CategoryID"] = textBoxCode.Text.Trim();
				dataRow["CategoryName"] = textBoxName.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["IsInactive"] = checkBoxInactive.Checked;
				dataRow["ParentCategoryID"] = comboBoxParentCategory.SelectedID;
				dataRow["AgeGroup1"] = textBoxAge1.Text;
				dataRow["AgeGroup2"] = textBoxAge2.Text;
				dataRow["AgeGroup3"] = textBoxAge3.Text;
				if (!string.IsNullOrEmpty(textBoxStdPricePercent.Text))
				{
					dataRow["StandardPricePercent"] = textBoxStdPricePercent.Text;
				}
				else
				{
					dataRow["StandardPricePercent"] = 0;
				}
				if (!string.IsNullOrEmpty(textBoxWholeSalePricePercent.Text))
				{
					dataRow["WholesalePricePercent"] = textBoxWholeSalePricePercent.Text;
				}
				else
				{
					dataRow["WholesalePricePercent"] = 0;
				}
				if (!string.IsNullOrEmpty(textBoxSpecialPricePercent.Text))
				{
					dataRow["SpecialPricePercent"] = textBoxSpecialPricePercent.Text;
				}
				else
				{
					dataRow["SpecialPricePercent"] = 0;
				}
				if (!string.IsNullOrEmpty(textBoxMinPricePercent.Text))
				{
					dataRow["MinimumPricePercent"] = textBoxMinPricePercent.Text;
				}
				else
				{
					dataRow["MinimumPricePercent"] = 0;
				}
				if (!string.IsNullOrEmpty(textBoxMinPricePercent.Text))
				{
					dataRow["CommissionPercent"] = textBoxCommissionPercent.Text;
				}
				else
				{
					dataRow["CommissionPercent"] = 0;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.ProductCategoryTable.Rows.Add(dataRow);
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
					currentData = Factory.ProductCategorySystem.GetProductCategoryByID(id.Trim());
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
					textBoxCode.Text = dataRow["CategoryID"].ToString();
					textBoxName.Text = dataRow["CategoryName"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					if (dataRow["AgeGroup1"] != DBNull.Value)
					{
						textBoxAge1.Text = dataRow["AgeGroup1"].ToString();
					}
					else
					{
						textBoxAge1.Text = "0";
					}
					if (dataRow["AgeGroup2"] != DBNull.Value)
					{
						textBoxAge2.Text = dataRow["AgeGroup2"].ToString();
					}
					else
					{
						textBoxAge2.Text = "0";
					}
					if (dataRow["AgeGroup3"] != DBNull.Value)
					{
						textBoxAge3.Text = dataRow["AgeGroup3"].ToString();
					}
					else
					{
						textBoxAge3.Text = "0";
					}
					comboBoxParentCategory.SelectedID = dataRow["ParentCategoryID"].ToString();
					if (dataRow["IsInactive"] != DBNull.Value)
					{
						checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					}
					else
					{
						checkBoxInactive.Checked = false;
					}
					if (dataRow["StandardPricePercent"] != DBNull.Value)
					{
						textBoxStdPricePercent.Text = dataRow["StandardPricePercent"].ToString();
					}
					else
					{
						textBoxStdPricePercent.Text = "0".ToString();
					}
					if (dataRow["WholesalePricePercent"] != DBNull.Value)
					{
						textBoxWholeSalePricePercent.Text = dataRow["WholesalePricePercent"].ToString();
					}
					else
					{
						textBoxWholeSalePricePercent.Text = "0".ToString();
					}
					if (dataRow["SpecialPricePercent"] != DBNull.Value)
					{
						textBoxSpecialPricePercent.Text = dataRow["SpecialPricePercent"].ToString();
					}
					else
					{
						textBoxSpecialPricePercent.Text = "0".ToString();
					}
					if (dataRow["MinimumPricePercent"] != DBNull.Value)
					{
						textBoxMinPricePercent.Text = dataRow["MinimumPricePercent"].ToString();
					}
					else
					{
						textBoxMinPricePercent.Text = "0".ToString();
					}
					if (dataRow["CommissionPercent"] != DBNull.Value)
					{
						textBoxCommissionPercent.Text = dataRow["CommissionPercent"].ToString();
					}
					else
					{
						textBoxCommissionPercent.Text = "0".ToString();
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
					flag = Factory.ProductCategorySystem.CreateProductCategory(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.ProductCategory, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.ProductCategorySystem.UpdateProductCategory(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Product_Category", "CategoryID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Product_Category", "CategoryID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Product_Category", "CategoryID");
			comboBoxParentCategory.Clear();
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxAge1.Text = "0";
			textBoxAge2.Text = "0";
			textBoxAge3.Text = "0";
			textBoxMinPricePercent.Clear();
			textBoxStdPricePercent.Clear();
			textBoxWholeSalePricePercent.Clear();
			textBoxSpecialPricePercent.Clear();
			checkBoxInactive.Checked = false;
			textBoxCommissionPercent.Text = 0.0.ToString();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void ProductCategoryGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ProductCategoryGroupDetailsForm_Validated(object sender, EventArgs e)
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
				bool num = Factory.ProductCategorySystem.DeleteProductCategory(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.ProductCategory, needRefresh: true);
				}
				return num;
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
			LoadData(DatabaseHelper.GetNextID("Product_Category", "CategoryID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Product_Category", "CategoryID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Product_Category", "CategoryID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Product_Category", "CategoryID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Product_Category", "CategoryID", toolStripTextBoxFind.Text.Trim()))
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

		private void ProductCategoryDetailsForm_Load(object sender, EventArgs e)
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ProductCategory);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (CompanyPreferences.ItemCodeCreationBasedOn != 1)
			{
				return;
			}
			ProductCategoryData productCategoryByID = Factory.ProductCategorySystem.GetProductCategoryByID(textBoxCode.Text);
			if (productCategoryByID == null || productCategoryByID.Tables.Count == 0 || productCategoryByID.Tables[0].Rows.Count == 0)
			{
				if (!string.IsNullOrEmpty(comboBoxParentCategory.SelectedID) && isNewRecord)
				{
					textBoxCode.Text = Factory.SystemDocumentSystem.GetNextCatgryWiseDocNumber("Product_Category", "CategoryID", "ParentCategoryID", comboBoxParentCategory.SelectedID);
				}
				if (string.IsNullOrEmpty(textBoxCode.Text))
				{
					textBoxCode.Text = PublicFunctions.GetNextCardNumber("Product_Category", "CategoryID");
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductCategoryDetailsForm));
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
            this.labelCode = new Micromind.UISupport.MMLabel();
            this.textBoxCode = new Micromind.UISupport.MMTextBox();
            this.mmLabel1 = new Micromind.UISupport.MMLabel();
            this.textBoxName = new Micromind.UISupport.MMTextBox();
            this.mmLabel4 = new Micromind.UISupport.MMLabel();
            this.textBoxNote = new Micromind.UISupport.MMTextBox();
            this.formManager = new Micromind.DataControls.FormManager();
            this.comboBoxParentCategory = new Micromind.DataControls.ProductCategoryComboBox();
            this.mmLabel2 = new Micromind.UISupport.MMLabel();
            this.checkBoxInactive = new System.Windows.Forms.CheckBox();
            this.textBoxAge1 = new Micromind.UISupport.NumberTextBox();
            this.mmLabel3 = new Micromind.UISupport.MMLabel();
            this.mmLabel5 = new Micromind.UISupport.MMLabel();
            this.textBoxAge2 = new Micromind.UISupport.NumberTextBox();
            this.mmLabel6 = new Micromind.UISupport.MMLabel();
            this.textBoxAge3 = new Micromind.UISupport.NumberTextBox();
            this.mmLabel7 = new Micromind.UISupport.MMLabel();
            this.textBoxSpecialPricePercent = new Micromind.UISupport.NumberTextBox();
            this.mmLabel8 = new Micromind.UISupport.MMLabel();
            this.textBoxWholeSalePricePercent = new Micromind.UISupport.NumberTextBox();
            this.mmLabel9 = new Micromind.UISupport.MMLabel();
            this.textBoxStdPricePercent = new Micromind.UISupport.NumberTextBox();
            this.mmLabel10 = new Micromind.UISupport.MMLabel();
            this.textBoxMinPricePercent = new Micromind.UISupport.NumberTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCommissionPercent = new Micromind.UISupport.PercentTextBox();
            this.toolStrip1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxParentCategory)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(547, 31);
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
            this.panelButtons.Location = new System.Drawing.Point(0, 196);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(547, 40);
            this.panelButtons.TabIndex = 13;
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
            this.linePanelDown.Size = new System.Drawing.Size(547, 1);
            this.linePanelDown.TabIndex = 0;
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
            this.xpButton1.Location = new System.Drawing.Point(437, 8);
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
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelCode.IsFieldHeader = false;
            this.labelCode.IsRequired = true;
            this.labelCode.Location = new System.Drawing.Point(8, 38);
            this.labelCode.Name = "labelCode";
            this.labelCode.PenWidth = 1F;
            this.labelCode.ShowBorder = false;
            this.labelCode.Size = new System.Drawing.Size(94, 13);
            this.labelCode.TabIndex = 0;
            this.labelCode.Text = "Category Code:";
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
            this.textBoxCode.Size = new System.Drawing.Size(173, 20);
            this.textBoxCode.TabIndex = 0;
            // 
            // mmLabel1
            // 
            this.mmLabel1.AutoSize = true;
            this.mmLabel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mmLabel1.IsFieldHeader = false;
            this.mmLabel1.IsRequired = true;
            this.mmLabel1.Location = new System.Drawing.Point(8, 58);
            this.mmLabel1.Name = "mmLabel1";
            this.mmLabel1.PenWidth = 1F;
            this.mmLabel1.ShowBorder = false;
            this.mmLabel1.Size = new System.Drawing.Size(97, 13);
            this.mmLabel1.TabIndex = 2;
            this.mmLabel1.Text = "Category Name:";
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
            this.textBoxName.MaxLength = 125;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(379, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // mmLabel4
            // 
            this.mmLabel4.AutoSize = true;
            this.mmLabel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel4.IsFieldHeader = false;
            this.mmLabel4.IsRequired = false;
            this.mmLabel4.Location = new System.Drawing.Point(8, 164);
            this.mmLabel4.Name = "mmLabel4";
            this.mmLabel4.PenWidth = 1F;
            this.mmLabel4.ShowBorder = false;
            this.mmLabel4.Size = new System.Drawing.Size(33, 13);
            this.mmLabel4.TabIndex = 9;
            this.mmLabel4.Text = "Note:";
            // 
            // textBoxNote
            // 
            this.textBoxNote.BackColor = System.Drawing.Color.White;
            this.textBoxNote.CustomReportFieldName = "";
            this.textBoxNote.CustomReportKey = "";
            this.textBoxNote.CustomReportValueType = ((byte)(1));
            this.textBoxNote.IsComboTextBox = false;
            this.textBoxNote.IsModified = false;
            this.textBoxNote.Location = new System.Drawing.Point(114, 163);
            this.textBoxNote.MaxLength = 255;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(379, 20);
            this.textBoxNote.TabIndex = 12;
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
            // comboBoxParentCategory
            // 
            this.comboBoxParentCategory.Assigned = false;
            this.comboBoxParentCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxParentCategory.CustomReportFieldName = "";
            this.comboBoxParentCategory.CustomReportKey = "";
            this.comboBoxParentCategory.CustomReportValueType = ((byte)(1));
            this.comboBoxParentCategory.DescriptionTextBox = null;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxParentCategory.DisplayLayout.Appearance = appearance1;
            this.comboBoxParentCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxParentCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxParentCategory.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxParentCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.comboBoxParentCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxParentCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.comboBoxParentCategory.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxParentCategory.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxParentCategory.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxParentCategory.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.comboBoxParentCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxParentCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxParentCategory.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxParentCategory.DisplayLayout.Override.CellAppearance = appearance8;
            this.comboBoxParentCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxParentCategory.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxParentCategory.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.comboBoxParentCategory.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.comboBoxParentCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxParentCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxParentCategory.DisplayLayout.Override.RowAppearance = appearance11;
            this.comboBoxParentCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxParentCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.comboBoxParentCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxParentCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxParentCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxParentCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxParentCategory.Editable = true;
            this.comboBoxParentCategory.FilterString = "";
            this.comboBoxParentCategory.HasAllAccount = false;
            this.comboBoxParentCategory.HasCustom = false;
            this.comboBoxParentCategory.IsDataLoaded = false;
            this.comboBoxParentCategory.Location = new System.Drawing.Point(114, 80);
            this.comboBoxParentCategory.MaxDropDownItems = 12;
            this.comboBoxParentCategory.Name = "comboBoxParentCategory";
            this.comboBoxParentCategory.ShowInactiveItems = false;
            this.comboBoxParentCategory.Size = new System.Drawing.Size(173, 20);
            this.comboBoxParentCategory.TabIndex = 3;
            this.comboBoxParentCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel2
            // 
            this.mmLabel2.AutoSize = true;
            this.mmLabel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel2.IsFieldHeader = false;
            this.mmLabel2.IsRequired = false;
            this.mmLabel2.Location = new System.Drawing.Point(8, 83);
            this.mmLabel2.Name = "mmLabel2";
            this.mmLabel2.PenWidth = 1F;
            this.mmLabel2.ShowBorder = false;
            this.mmLabel2.Size = new System.Drawing.Size(86, 13);
            this.mmLabel2.TabIndex = 18;
            this.mmLabel2.Text = "Parent Category:";
            // 
            // checkBoxInactive
            // 
            this.checkBoxInactive.AutoSize = true;
            this.checkBoxInactive.Location = new System.Drawing.Point(293, 37);
            this.checkBoxInactive.Name = "checkBoxInactive";
            this.checkBoxInactive.Size = new System.Drawing.Size(64, 17);
            this.checkBoxInactive.TabIndex = 1;
            this.checkBoxInactive.Text = "Inactvie";
            this.checkBoxInactive.UseVisualStyleBackColor = true;
            // 
            // textBoxAge1
            // 
            this.textBoxAge1.AllowDecimal = false;
            this.textBoxAge1.CustomReportFieldName = "";
            this.textBoxAge1.CustomReportKey = "";
            this.textBoxAge1.CustomReportValueType = ((byte)(1));
            this.textBoxAge1.IsComboTextBox = false;
            this.textBoxAge1.IsModified = false;
            this.textBoxAge1.Location = new System.Drawing.Point(114, 111);
            this.textBoxAge1.MaxLength = 7;
            this.textBoxAge1.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxAge1.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxAge1.Name = "textBoxAge1";
            this.textBoxAge1.NullText = "0";
            this.textBoxAge1.Size = new System.Drawing.Size(78, 20);
            this.textBoxAge1.TabIndex = 5;
            this.textBoxAge1.Text = "0";
            this.textBoxAge1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mmLabel3
            // 
            this.mmLabel3.AutoSize = true;
            this.mmLabel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel3.IsFieldHeader = false;
            this.mmLabel3.IsRequired = false;
            this.mmLabel3.Location = new System.Drawing.Point(8, 114);
            this.mmLabel3.Name = "mmLabel3";
            this.mmLabel3.PenWidth = 1F;
            this.mmLabel3.ShowBorder = false;
            this.mmLabel3.Size = new System.Drawing.Size(65, 13);
            this.mmLabel3.TabIndex = 20;
            this.mmLabel3.Text = "Age Days 1:";
            // 
            // mmLabel5
            // 
            this.mmLabel5.AutoSize = true;
            this.mmLabel5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel5.IsFieldHeader = false;
            this.mmLabel5.IsRequired = false;
            this.mmLabel5.Location = new System.Drawing.Point(198, 114);
            this.mmLabel5.Name = "mmLabel5";
            this.mmLabel5.PenWidth = 1F;
            this.mmLabel5.ShowBorder = false;
            this.mmLabel5.Size = new System.Drawing.Size(65, 13);
            this.mmLabel5.TabIndex = 22;
            this.mmLabel5.Text = "Age Days 2:";
            // 
            // textBoxAge2
            // 
            this.textBoxAge2.AllowDecimal = false;
            this.textBoxAge2.CustomReportFieldName = "";
            this.textBoxAge2.CustomReportKey = "";
            this.textBoxAge2.CustomReportValueType = ((byte)(1));
            this.textBoxAge2.IsComboTextBox = false;
            this.textBoxAge2.IsModified = false;
            this.textBoxAge2.Location = new System.Drawing.Point(265, 111);
            this.textBoxAge2.MaxLength = 7;
            this.textBoxAge2.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxAge2.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxAge2.Name = "textBoxAge2";
            this.textBoxAge2.NullText = "0";
            this.textBoxAge2.Size = new System.Drawing.Size(78, 20);
            this.textBoxAge2.TabIndex = 6;
            this.textBoxAge2.Text = "0";
            this.textBoxAge2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mmLabel6
            // 
            this.mmLabel6.AutoSize = true;
            this.mmLabel6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel6.IsFieldHeader = false;
            this.mmLabel6.IsRequired = false;
            this.mmLabel6.Location = new System.Drawing.Point(348, 114);
            this.mmLabel6.Name = "mmLabel6";
            this.mmLabel6.PenWidth = 1F;
            this.mmLabel6.ShowBorder = false;
            this.mmLabel6.Size = new System.Drawing.Size(65, 13);
            this.mmLabel6.TabIndex = 24;
            this.mmLabel6.Text = "Age Days 3:";
            // 
            // textBoxAge3
            // 
            this.textBoxAge3.AllowDecimal = false;
            this.textBoxAge3.CustomReportFieldName = "";
            this.textBoxAge3.CustomReportKey = "";
            this.textBoxAge3.CustomReportValueType = ((byte)(1));
            this.textBoxAge3.IsComboTextBox = false;
            this.textBoxAge3.IsModified = false;
            this.textBoxAge3.Location = new System.Drawing.Point(415, 111);
            this.textBoxAge3.MaxLength = 7;
            this.textBoxAge3.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxAge3.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxAge3.Name = "textBoxAge3";
            this.textBoxAge3.NullText = "0";
            this.textBoxAge3.Size = new System.Drawing.Size(78, 20);
            this.textBoxAge3.TabIndex = 7;
            this.textBoxAge3.Text = "0";
            this.textBoxAge3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mmLabel7
            // 
            this.mmLabel7.AutoSize = true;
            this.mmLabel7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel7.IsFieldHeader = false;
            this.mmLabel7.IsRequired = false;
            this.mmLabel7.Location = new System.Drawing.Point(310, 139);
            this.mmLabel7.Name = "mmLabel7";
            this.mmLabel7.PenWidth = 1F;
            this.mmLabel7.ShowBorder = false;
            this.mmLabel7.Size = new System.Drawing.Size(69, 13);
            this.mmLabel7.TabIndex = 30;
            this.mmLabel7.Text = "Spcl.Price %:";
            // 
            // textBoxSpecialPricePercent
            // 
            this.textBoxSpecialPricePercent.AllowDecimal = false;
            this.textBoxSpecialPricePercent.CustomReportFieldName = "";
            this.textBoxSpecialPricePercent.CustomReportKey = "";
            this.textBoxSpecialPricePercent.CustomReportValueType = ((byte)(1));
            this.textBoxSpecialPricePercent.IsComboTextBox = false;
            this.textBoxSpecialPricePercent.IsModified = false;
            this.textBoxSpecialPricePercent.Location = new System.Drawing.Point(385, 136);
            this.textBoxSpecialPricePercent.MaxLength = 7;
            this.textBoxSpecialPricePercent.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxSpecialPricePercent.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxSpecialPricePercent.Name = "textBoxSpecialPricePercent";
            this.textBoxSpecialPricePercent.NullText = "0";
            this.textBoxSpecialPricePercent.Size = new System.Drawing.Size(43, 20);
            this.textBoxSpecialPricePercent.TabIndex = 10;
            this.textBoxSpecialPricePercent.Text = "0";
            this.textBoxSpecialPricePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mmLabel8
            // 
            this.mmLabel8.AutoSize = true;
            this.mmLabel8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel8.IsFieldHeader = false;
            this.mmLabel8.IsRequired = false;
            this.mmLabel8.Location = new System.Drawing.Point(163, 139);
            this.mmLabel8.Name = "mmLabel8";
            this.mmLabel8.PenWidth = 1F;
            this.mmLabel8.ShowBorder = false;
            this.mmLabel8.Size = new System.Drawing.Size(91, 13);
            this.mmLabel8.TabIndex = 29;
            this.mmLabel8.Text = "WhleSalePrice %:";
            // 
            // textBoxWholeSalePricePercent
            // 
            this.textBoxWholeSalePricePercent.AllowDecimal = false;
            this.textBoxWholeSalePricePercent.CustomReportFieldName = "";
            this.textBoxWholeSalePricePercent.CustomReportKey = "";
            this.textBoxWholeSalePricePercent.CustomReportValueType = ((byte)(1));
            this.textBoxWholeSalePricePercent.IsComboTextBox = false;
            this.textBoxWholeSalePricePercent.IsModified = false;
            this.textBoxWholeSalePricePercent.Location = new System.Drawing.Point(257, 136);
            this.textBoxWholeSalePricePercent.MaxLength = 7;
            this.textBoxWholeSalePricePercent.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxWholeSalePricePercent.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxWholeSalePricePercent.Name = "textBoxWholeSalePricePercent";
            this.textBoxWholeSalePricePercent.NullText = "0";
            this.textBoxWholeSalePricePercent.Size = new System.Drawing.Size(50, 20);
            this.textBoxWholeSalePricePercent.TabIndex = 9;
            this.textBoxWholeSalePricePercent.Text = "0";
            this.textBoxWholeSalePricePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mmLabel9
            // 
            this.mmLabel9.AutoSize = true;
            this.mmLabel9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel9.IsFieldHeader = false;
            this.mmLabel9.IsRequired = false;
            this.mmLabel9.Location = new System.Drawing.Point(8, 139);
            this.mmLabel9.Name = "mmLabel9";
            this.mmLabel9.PenWidth = 1F;
            this.mmLabel9.ShowBorder = false;
            this.mmLabel9.Size = new System.Drawing.Size(64, 13);
            this.mmLabel9.TabIndex = 28;
            this.mmLabel9.Text = "Std.Price %:";
            // 
            // textBoxStdPricePercent
            // 
            this.textBoxStdPricePercent.AllowDecimal = false;
            this.textBoxStdPricePercent.CustomReportFieldName = "";
            this.textBoxStdPricePercent.CustomReportKey = "";
            this.textBoxStdPricePercent.CustomReportValueType = ((byte)(1));
            this.textBoxStdPricePercent.IsComboTextBox = false;
            this.textBoxStdPricePercent.IsModified = false;
            this.textBoxStdPricePercent.Location = new System.Drawing.Point(114, 136);
            this.textBoxStdPricePercent.MaxLength = 7;
            this.textBoxStdPricePercent.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxStdPricePercent.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxStdPricePercent.Name = "textBoxStdPricePercent";
            this.textBoxStdPricePercent.NullText = "0";
            this.textBoxStdPricePercent.Size = new System.Drawing.Size(43, 20);
            this.textBoxStdPricePercent.TabIndex = 8;
            this.textBoxStdPricePercent.Text = "0";
            this.textBoxStdPricePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mmLabel10
            // 
            this.mmLabel10.AutoSize = true;
            this.mmLabel10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel10.IsFieldHeader = false;
            this.mmLabel10.IsRequired = false;
            this.mmLabel10.Location = new System.Drawing.Point(430, 139);
            this.mmLabel10.Name = "mmLabel10";
            this.mmLabel10.PenWidth = 1F;
            this.mmLabel10.ShowBorder = false;
            this.mmLabel10.Size = new System.Drawing.Size(65, 13);
            this.mmLabel10.TabIndex = 32;
            this.mmLabel10.Text = "Min.Price %:";
            // 
            // textBoxMinPricePercent
            // 
            this.textBoxMinPricePercent.AllowDecimal = false;
            this.textBoxMinPricePercent.CustomReportFieldName = "";
            this.textBoxMinPricePercent.CustomReportKey = "";
            this.textBoxMinPricePercent.CustomReportValueType = ((byte)(1));
            this.textBoxMinPricePercent.IsComboTextBox = false;
            this.textBoxMinPricePercent.IsModified = false;
            this.textBoxMinPricePercent.Location = new System.Drawing.Point(498, 136);
            this.textBoxMinPricePercent.MaxLength = 7;
            this.textBoxMinPricePercent.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxMinPricePercent.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxMinPricePercent.Name = "textBoxMinPricePercent";
            this.textBoxMinPricePercent.NullText = "0";
            this.textBoxMinPricePercent.Size = new System.Drawing.Size(43, 20);
            this.textBoxMinPricePercent.TabIndex = 11;
            this.textBoxMinPricePercent.Text = "0";
            this.textBoxMinPricePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(300, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 177;
            this.label3.Text = "Commission %: ";
            // 
            // textBoxCommissionPercent
            // 
            this.textBoxCommissionPercent.CustomReportFieldName = "";
            this.textBoxCommissionPercent.CustomReportKey = "";
            this.textBoxCommissionPercent.CustomReportValueType = ((byte)(1));
            this.textBoxCommissionPercent.IsComboTextBox = false;
            this.textBoxCommissionPercent.IsModified = false;
            this.textBoxCommissionPercent.Location = new System.Drawing.Point(385, 82);
            this.textBoxCommissionPercent.Name = "textBoxCommissionPercent";
            this.textBoxCommissionPercent.Size = new System.Drawing.Size(60, 20);
            this.textBoxCommissionPercent.TabIndex = 4;
            this.textBoxCommissionPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ProductCategoryDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(547, 236);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxCommissionPercent);
            this.Controls.Add(this.mmLabel10);
            this.Controls.Add(this.textBoxMinPricePercent);
            this.Controls.Add(this.mmLabel7);
            this.Controls.Add(this.textBoxSpecialPricePercent);
            this.Controls.Add(this.mmLabel8);
            this.Controls.Add(this.textBoxWholeSalePricePercent);
            this.Controls.Add(this.mmLabel9);
            this.Controls.Add(this.textBoxStdPricePercent);
            this.Controls.Add(this.mmLabel6);
            this.Controls.Add(this.textBoxAge3);
            this.Controls.Add(this.mmLabel5);
            this.Controls.Add(this.textBoxAge2);
            this.Controls.Add(this.mmLabel3);
            this.Controls.Add(this.textBoxAge1);
            this.Controls.Add(this.checkBoxInactive);
            this.Controls.Add(this.mmLabel2);
            this.Controls.Add(this.comboBoxParentCategory);
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
            this.Name = "ProductCategoryDetailsForm";
            this.Text = "Product Category Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountGroupDetailsForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxParentCategory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
