using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.ClientUI.WindowsForms.DataEntries.Utilities;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ProductDetailsForm : Form, IForm
	{
		private string incomeAccountID = "";

		private string assetAccountID = "";

		private string cogsAccountID = "";

		private string ExpenseCode = "";

		private bool canAccessCost = true;

		private ProductData currentData;

		private const string TABLENAME_CONST = "Product";

		private const string IDFIELD_CONST = "ProductID";

		private bool isNewRecord = true;

		private string filterString = "";

		private int substringQty;

		private string mainUnit = "";

		private bool isLoading;

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private MMTextBox textBoxUPC;

		private MMLabel mmLabel3;

		private MMTextBox textBoxVendorRef;

		private MMLabel mmLabel6;

		private ItemTypesComboBox comboBoxItemType;

		private MMLabel mmLabel7;

		private ItemCostingComboBox comboBoxCostMethod;

		private MMLabel mmLabel8;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageUserDefined;

		private UnitPriceTextBox textBoxStandardPrice;

		private ItemClassComboBox comboBoxClass;

		private MMLabel labelStandardPrice;

		private MMLabel mmLabel11;

		private UnitPriceTextBox textBoxAvgCost;

		private MMLabel mmLabel12;

		private UnitPriceTextBox textBoxStandardCost;

		private ProductCategoryComboBox comboBoxCategory;

		private QuantityTextBox textBoxReorderLevel;

		private MMLabel mmLabel15;

		private QuantityTextBox textBoxQuantityPerUnit;

		private UnitComboBox comboBoxUOM;

		private MMLabel mmLabel21;

		private NumberTextBox textBoxWarranty;

		private QuantityTextBox textBoxQuantityOnhand;

		private MMLabel mmLabel22;

		private vendorsFlatComboBox comboBoxPrefVendor;

		private Panel panel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private UltraTabPageControl ultraTabPageControl1;

		private DataEntryGrid dataGridUnits;

		private UnitComboBox comboBoxGridUnit;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel11;

		private UnitComboBox comboBoxMainUnitTab;

		private MMLabel mmLabel5;

		private OpenFileDialog openFileDialog1;

		private MMLabel labelMinPrice;

		private MMLabel labelSpecialPrice;

		private MMLabel labelWholesalePrice;

		private UnitPriceTextBox textBoxMinimumPrice;

		private UnitPriceTextBox textBoxSpecialPrice;

		private UnitPriceTextBox textBoxWholesalePrice;

		private MMLabel mmLabel10;

		private UnitPriceTextBox textBoxLastCost;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxPhoto;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private PictureBox pictureBoxNoImage;

		private ToolStripButton toolStripButtonShowPicture;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel12;

		private MMLabel labelCustomerNameHeader;

		private UDFEntryControl udfEntryGrid;

		private UltraFormattedLinkLabel linkLabelBOM;

		private BOMComboBox comboBoxBOM;

		private CheckBox checkBoxExcludeFromCatalogue;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private CheckBox checkBoxTrackSerial;

		private CheckBox checkBoxTrackLot;

		private MMLabel mmLabel14;

		private NumberTextBox textBoxWeight;

		private CountryComboBox comboBoxOrigin;

		private MMLabel mmLabel20;

		private MMTextBox textBoxAttribute;

		private MMLabel mmLabel19;

		private MMTextBox textBoxSize;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private ProductStyleComboBox comboBoxStyle;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private ProductBrandComboBox comboBoxBrand;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ProductManufacturerComboBox comboBoxManufacturer;

		private ToolStripButton toolStripButtonDuplicate;

		private MMTextBox textBoxRackBin;

		private MMLabel mmLabel2;

		private ToolStripButton toolStripButtonInformation;

		private UnitPriceTextBox textBox3PLRent;

		private MMLabel mmLabel9;

		private XPButton buttonAccounts;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem importToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private MMTextBox textBoxExpenseCode;

		private MMLabel labelExpenseCode;

		private ExpenseCodeComboBox comboBoxExpenseCode;

		private CheckBox checkBoxHoldSale;

		private MMLabel mmLabel13;

		private MMTextBox textBoxAlias;

		private MMLabel labelDescription3;

		private MMTextBox textDescription3;

		private UltraTabPageControl ultraTabPageControl2;

		private GroupBox groupBox2;

		private MMTextBox textBoxModelNo;

		private MMTextBox textBoxChasisNo;

		private MMLabel mmLabel31;

		private MMLabel mmLabel32;

		private GenericListComboBox comboBoxPartsFamily;

		private GenericListComboBox comboboxPartsMakeType;

		private MMLabel mmLabel24;

		private GenericListComboBox comboBoxPartsType;

		private MMLabel mmLabel16;

		private MMTextBox textBoxSpecification;

		private GroupBox groupBox1;

		private GenericListComboBox comboBoxVehicleMake;

		private GenericListComboBox comboBoxVehicleModel;

		private GenericListComboBox ComboBoxVehicleType;

		private MMTextBox textBoxEngineNo;

		private GroupBox groupBox4;

		private GroupBox groupBox3;

		private ProductComboBox comboBoxSubstiProduct;

		private DataEntryGrid datagridAppliedModels;

		private DataEntryGrid dataGridSubstituteItems;

		private GenericListComboBox comboBoxgridVehicleType;

		private GenericListComboBox comboBoxGridVehicleMake;

		private MMLabel mmLabel26;

		private MMTextBox textBoxOEMCode;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel16;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel15;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel14;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel13;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraTabPageControl ultraTabPageControl3;

		private LocationComboBox comboBoxGridLocation;

		private DataEntryGrid dataEntryGridPriceList;

		private XPButton buttonTranslate;

		private UltraTabPageControl ultraTabPageControl4;

		private MMTextBox textBoxNote;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel labelAttribute3;

		private MMTextBox textBoxAttribute3;

		private MMLabel labelAttribute2;

		private MMTextBox textBoxAttribute2;

		private MMLabel labelAttribute1;

		private MMTextBox textBoxAttribute1;

		private GenericListComboBox comboBoxColor;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel21;

		private GenericListComboBox comboBoxMaterial;

		private GenericListComboBox comboBoxStandard;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel17;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel20;

		private GenericListComboBox comboBoxFinish;

		private GenericListComboBox comboBoxGrade;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel18;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel19;

		private UltraGroupBox ultraGroupBox7;

		private TaxGroupComboBox comboBoxTaxGroup;

		private MMLabel mmLabel4;

		private ComboBox comboBoxTaxOption;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel22;

		private UnitComboBox unitComboBox1;

		private UltraTabPageControl ultraTabPageControl5;

		private UltraGroupBox ultraGroupBox2;

		private GroupBox groupBox5;

		private GroupBox groupBox6;

		private ListBox listBoxSelectedFields;

		private CheckedListBox checkedListBoxFields;

		private MMLabel mmLabel17;

		private NumberTextBox textBoxSubstring;

		private XPButton buttonGenerate;

		private CheckBox checkBoxUPCPriceEmbedded;

		private UltraExpandableGroupBox ultraExpandableGroupBoxType;

		private UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel1;

		private GenericListComboBox genericListComboBoxType1;

		private GenericListComboBox genericListComboBoxType3;

		private UltraFormattedLinkLabel linkLabelType3;

		private GenericListComboBox genericListComboBoxType2;

		private UltraFormattedLinkLabel linkLabelType2;

		private UltraFormattedLinkLabel linkLabelType1;

		private GenericListComboBox genericListComboBoxType8;

		private UltraFormattedLinkLabel linkLabelType8;

		private GenericListComboBox genericListComboBoxType7;

		private UltraFormattedLinkLabel linkLabelType7;

		private GenericListComboBox genericListComboBoxType6;

		private UltraFormattedLinkLabel linkLabelType6;

		private GenericListComboBox genericListComboBoxType5;

		private UltraFormattedLinkLabel linkLabelType5;

		private UltraFormattedLinkLabel linkLabelType4;

		private GenericListComboBox genericListComboBoxType4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelCompanyDivision;

		private CompanyDivisionComboBox comboBoxCompanyDivision;

		private System.Windows.Forms.ToolTip toolTip1;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4010;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public TimeSpan TranslationTime
		{
			get;
			private set;
		}

		public string TranslationSpeechUrl
		{
			get;
			private set;
		}

		public Exception Error
		{
			get;
			private set;
		}

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
					UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLoadImage;
					UltraFormattedLinkLabel ultraFormattedLinkLabel2 = linkRemovePicture;
					bool flag2 = linkAddPicture.Enabled = false;
					bool visible = ultraFormattedLinkLabel2.Enabled = flag2;
					ultraFormattedLinkLabel.Visible = visible;
					buttonGenerate.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					linkAddPicture.Enabled = true;
					buttonGenerate.Enabled = false;
				}
				toolStripButtonDuplicate.Enabled = !value;
				comboBoxItemType.Enabled = value;
				comboBoxCostMethod.Enabled = (value & (comboBoxItemType.SelectedIndex == 0));
				toolStripButtonAttach.Enabled = !value;
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

		public ProductDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			CreateContextMenu();
			labelStandardPrice.Text = CompanyPreferences.UnitPrice1Title + ":";
			labelWholesalePrice.Text = CompanyPreferences.UnitPrice2Title + ":";
			labelSpecialPrice.Text = CompanyPreferences.UnitPrice3Title + ":";
			labelDescription3.Text = CompanyPreferences.Description3 + ":";
			labelAttribute1.Text = CompanyPreferences.Attribute1Name + ":";
			labelAttribute2.Text = CompanyPreferences.Attribute2Name + ":";
			labelAttribute3.Text = CompanyPreferences.Attribute3Name + ":";
			EnableProductTypeCombo();
			if (CompanyPreferences.ActivatePartsDetails)
			{
				ultraTabControl1.Tabs[1].Visible = true;
				ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[0];
			}
			try
			{
				comboBoxCostMethod.LoadData();
				comboBoxItemType.LoadData();
				comboBoxItemType.SelectedIndex = 0;
				comboBoxCostMethod.SelectedIndex = 0;
				dataGridUnits.SetupUI();
				dataEntryGridPriceList.SetupUI();
				SetupUnitGrid();
				SetupPriceListGrid();
				SetupSubstituteItemsGrid();
				SetupAppliedModelsGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					checkBoxTrackSerial.Enabled = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void EnableProductTypeCombo()
		{
			linkLabelType1.Value = CompanyPreferences.ProductType1Name + ":";
			linkLabelType2.Value = CompanyPreferences.ProductType2Name + ":";
			linkLabelType3.Value = CompanyPreferences.ProductType3Name + ":";
			linkLabelType4.Value = CompanyPreferences.ProductType4Name + ":";
			linkLabelType5.Value = CompanyPreferences.ProductType5Name + ":";
			linkLabelType6.Value = CompanyPreferences.ProductType6Name + ":";
			linkLabelType7.Value = CompanyPreferences.ProductType7Name + ":";
			linkLabelType8.Value = CompanyPreferences.ProductType8Name + ":";
			if (linkLabelType1.Value.ToString() != "P.Type 1:" || linkLabelType2.Value.ToString() != "P.Type 2:" || linkLabelType3.Value.ToString() != "P.Type 3:" || linkLabelType4.Value.ToString() != "P.Type 4:" || linkLabelType5.Value.ToString() != "P.Type 5:" || linkLabelType6.Value.ToString() != "P.Type 6:" || linkLabelType7.Value.ToString() != "P.Type 7:" || linkLabelType8.Value.ToString() != "P.Type 8:")
			{
				ultraExpandableGroupBoxType.Expanded = true;
			}
			else
			{
				ultraExpandableGroupBoxType.Expanded = false;
			}
		}

		private void AddEvents()
		{
			base.Load += ProductDetailsForm_Load;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			dataGridUnits.AfterCellUpdate += dataGridUnits_AfterCellUpdate;
			dataGridUnits.BeforeRowDeactivate += dataGridUnits_BeforeRowDeactivate;
			dataGridUnits.CellDataError += dataGridUnits_CellDataError;
			comboBoxItemType.SelectedIndexChanged += comboBoxItemType_SelectedIndexChanged;
			comboBoxCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
			textBoxName.TextChanged += textBoxName_TextChanged;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
			dataGridSubstituteItems.AfterCellUpdate += dataGridSubstituteItems_AfterCellUpdate;
		}

		private void dataGridSubstituteItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridSubstituteItems.ActiveRow != null && e.Cell.Column.Key == "Item Code")
				{
					dataGridSubstituteItems.ActiveRow.Cells["Description"].Value = comboBoxSubstiProduct.SelectedName;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void textBoxCode_Validating(object sender, CancelEventArgs e)
		{
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		public bool WebRequestTest()
		{
			string requestUriString = "http://www.google.com";
			try
			{
				WebRequest.Create(requestUriString).GetResponse();
			}
			catch (WebException)
			{
				return false;
			}
			return true;
		}

		public string Translate(string sourceText, string sourceLanguage, string targetLanguage)
		{
			Error = null;
			TranslationSpeechUrl = null;
			TranslationTime = TimeSpan.Zero;
			DateTime now = DateTime.Now;
			string text = string.Empty;
			checked
			{
				try
				{
					string address = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}", "en", "ar", HttpUtility.UrlEncode(sourceText));
					string tempFileName = Path.GetTempFileName();
					using (WebClient webClient = new WebClient())
					{
						webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
						webClient.DownloadFile(address, tempFileName);
					}
					if (File.Exists(tempFileName))
					{
						string text2 = File.ReadAllText(tempFileName);
						int num = text2.IndexOf(string.Format(",,\"{0}\"", "en"));
						if (num == -1)
						{
							int num2 = text2.IndexOf('"');
							if (num2 != -1)
							{
								int num3 = text2.IndexOf('"', num2 + 1);
								if (num3 != -1)
								{
									text = text2.Substring(num2 + 1, num3 - num2 - 1);
								}
							}
						}
						else
						{
							text2 = text2.Substring(0, num);
							text2 = text2.Replace("],[", ",");
							text2 = text2.Replace("]", string.Empty);
							text2 = text2.Replace("[", string.Empty);
							text2 = text2.Replace("\",\"", "\"");
							string[] array = text2.Split(new char[1]
							{
								'"'
							}, StringSplitOptions.RemoveEmptyEntries);
							for (int i = 0; i < array.Count(); i += 2)
							{
								string text3 = array[i];
								if (text3.StartsWith(",,"))
								{
									i--;
								}
								else
								{
									text = text + text3 + "  ";
								}
							}
						}
						text = text.Trim();
						text = text.Replace(" ?", "?");
						text = text.Replace(" !", "!");
						text = text.Replace(" ,", ",");
						text = text.Replace(" .", ".");
						text = text.Replace(" ;", ";");
						TranslationSpeechUrl = string.Format("https://translate.googleapis.com/translate_tts?ie=UTF-8&q={0}&tl={1}&total=1&idx=0&textlen={2}&client=gtx", HttpUtility.UrlEncode(text), "ar", text.Length);
					}
				}
				catch (Exception ex)
				{
					Exception ex3 = Error = ex;
				}
				TranslationTime = DateTime.Now - now;
				return text;
			}
		}

		private void comboBoxItemType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxItemType.SelectedType == ItemTypes.Inventory)
			{
				buttonAccounts.Enabled = true;
				comboBoxCostMethod.Enabled = true;
				UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLabelBOM;
				bool enabled = comboBoxBOM.Enabled = false;
				ultraFormattedLinkLabel.Enabled = enabled;
				QuantityTextBox quantityTextBox = textBoxReorderLevel;
				enabled = (textBoxQuantityPerUnit.Enabled = true);
				quantityTextBox.Enabled = enabled;
				checkBoxTrackLot.Enabled = true;
				textBox3PLRent.Enabled = false;
			}
			else if (comboBoxItemType.SelectedType == ItemTypes.ConsignmentItem)
			{
				buttonAccounts.Enabled = false;
				comboBoxCostMethod.Enabled = false;
				UltraFormattedLinkLabel ultraFormattedLinkLabel2 = linkLabelBOM;
				bool enabled = comboBoxBOM.Enabled = false;
				ultraFormattedLinkLabel2.Enabled = enabled;
				checkBoxTrackLot.Enabled = false;
				checkBoxTrackLot.Checked = true;
				textBox3PLRent.Enabled = false;
			}
			else if (comboBoxItemType.SelectedType == ItemTypes.Inventory3PL)
			{
				buttonAccounts.Enabled = false;
				comboBoxCostMethod.Enabled = false;
				UltraFormattedLinkLabel ultraFormattedLinkLabel3 = linkLabelBOM;
				bool enabled = comboBoxBOM.Enabled = false;
				ultraFormattedLinkLabel3.Enabled = enabled;
				checkBoxTrackLot.Enabled = false;
				checkBoxTrackLot.Checked = true;
				textBox3PLRent.Enabled = true;
			}
			else if (comboBoxItemType.SelectedType == ItemTypes.Service || comboBoxItemType.SelectedType == ItemTypes.NonInventory || comboBoxItemType.SelectedType == ItemTypes.Discount)
			{
				buttonAccounts.Enabled = true;
				comboBoxCostMethod.Enabled = false;
				textBox3PLRent.Enabled = false;
				QuantityTextBox quantityTextBox2 = textBoxReorderLevel;
				bool enabled = textBoxQuantityPerUnit.Enabled = false;
				quantityTextBox2.Enabled = enabled;
				UltraFormattedLinkLabel ultraFormattedLinkLabel4 = linkLabelBOM;
				enabled = (comboBoxBOM.Enabled = false);
				ultraFormattedLinkLabel4.Enabled = enabled;
				CheckBox checkBox = checkBoxTrackLot;
				enabled = (checkBoxTrackLot.Checked = false);
				checkBox.Enabled = enabled;
			}
			else if (comboBoxItemType.SelectedType == ItemTypes.Assembly)
			{
				UltraFormattedLinkLabel ultraFormattedLinkLabel5 = linkLabelBOM;
				bool enabled = comboBoxBOM.Enabled = true;
				ultraFormattedLinkLabel5.Enabled = enabled;
				checkBoxTrackLot.Enabled = true;
				textBox3PLRent.Enabled = false;
			}
			else if (comboBoxItemType.SelectedType == ItemTypes.ProjectFee)
			{
				buttonAccounts.Enabled = true;
				comboBoxCostMethod.Enabled = false;
				QuantityTextBox quantityTextBox3 = textBoxReorderLevel;
				bool enabled = textBoxQuantityPerUnit.Enabled = false;
				quantityTextBox3.Enabled = enabled;
				UltraFormattedLinkLabel ultraFormattedLinkLabel6 = linkLabelBOM;
				enabled = (comboBoxBOM.Enabled = false);
				ultraFormattedLinkLabel6.Enabled = enabled;
				CheckBox checkBox2 = checkBoxTrackLot;
				enabled = (checkBoxTrackLot.Checked = false);
				checkBox2.Enabled = enabled;
				textBox3PLRent.Enabled = false;
			}
			if (comboBoxItemType.SelectedType == ItemTypes.Service)
			{
				comboBoxExpenseCode.Visible = true;
				textBoxExpenseCode.Visible = true;
				labelExpenseCode.Visible = true;
			}
			else
			{
				comboBoxExpenseCode.Visible = false;
				textBoxExpenseCode.Visible = false;
				labelExpenseCode.Visible = false;
			}
		}

		private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (CompanyPreferences.ItemCodeCreationBasedOn != 1)
			{
				return;
			}
			ProductData productByID = Factory.ProductSystem.GetProductByID(textBoxCode.Text);
			if (productByID == null || productByID.Tables.Count == 0 || productByID.Tables[0].Rows.Count == 0)
			{
				if (!string.IsNullOrEmpty(comboBoxCategory.SelectedID))
				{
					textBoxCode.Text = Factory.SystemDocumentSystem.GetNextCatgryWiseDocNumber("Product", "ProductID", "CategoryID", comboBoxCategory.SelectedID);
				}
				if (string.IsNullOrEmpty(textBoxCode.Text))
				{
					textBoxCode.Text = PublicFunctions.GetNextCardNumber("Product", "ProductID");
				}
			}
		}

		private void dataGridUnits_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridUnits.ActiveCell.Column.Key == "Unit")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridUnit.Text = dataGridUnits.ActiveCell.Text;
				comboBoxGridUnit.QuickAddItem();
			}
			else if (dataGridUnits.ActiveCell.Column.Key == "Factor")
			{
				ErrorHelper.InformationMessage("Please enter a numeric value greater than zero.");
			}
		}

		private void dataGridUnits_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridUnits.ActiveRow != null)
			{
				foreach (UltraGridRow row in dataGridUnits.Rows)
				{
					if (!row.IsActiveRow && row.Cells["Unit"].Value == dataGridUnits.ActiveRow.Cells["Unit"].Value)
					{
						ErrorHelper.InformationMessage("This unit is already added for this item.");
						e.Cancel = true;
						return;
					}
				}
				if (comboBoxMainUnitTab.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Please select the main UOM first.");
					e.Cancel = true;
				}
			}
		}

		private void textBoxCode_TextChanged(object sender, EventArgs e)
		{
			if (textBoxCode.Text.Trim() != "")
			{
				Text = "Item Details - " + textBoxCode.Text.Trim();
			}
			else
			{
				Text = "Item Details";
			}
			SetHeaderName();
		}

		private void SetHeaderName()
		{
			labelCustomerNameHeader.Text = textBoxCode.Text + " - " + textBoxName.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxName.Text.Trim() == "")
			{
				labelCustomerNameHeader.Text = "";
			}
		}

		private void dataGridUnits_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (!(e.Cell.Column.Key == "Unit") && !(e.Cell.Column.Key == "Factor") && !(e.Cell.Column.Key == "Factor Type"))
			{
				return;
			}
			if (e.Cell.Row.Cells["Factor Type"].Value.ToString() == "" || e.Cell.Row.Cells["Factor"].Value.ToString() == "" || e.Cell.Row.Cells["Unit"].Value.ToString() == "")
			{
				e.Cell.Row.Cells["Description"].Value = "";
				return;
			}
			string a = e.Cell.Row.Cells["Factor Type"].Value.ToString();
			decimal result = 1m;
			decimal.TryParse(e.Cell.Row.Cells["Factor"].Value.ToString(), out result);
			if (a == "M")
			{
				e.Cell.Row.Cells["Description"].Value = "1 " + comboBoxUOM.Text + " = " + result.ToString() + " " + e.Cell.Row.Cells["Unit"].Value.ToString();
			}
			else
			{
				e.Cell.Row.Cells["Description"].Value = "1 " + e.Cell.Row.Cells["Unit"].Value.ToString() + " = " + result.ToString() + " " + comboBoxUOM.Text;
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ProductData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ProductTable.Rows[0] : currentData.ProductTable.NewRow();
				dataRow.BeginEdit();
				if (isNewRecord)
				{
					dataRow["ProductID"] = textBoxCode.Text.Trim();
				}
				else
				{
					dataRow["ProductID"] = textBoxCode.Text;
				}
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Description2"] = textBoxAlias.Text;
				dataRow["Description3"] = textDescription3.Text;
				dataRow["IsInactive"] = checkBoxInactive.Checked;
				dataRow["IsHoldSale"] = checkBoxHoldSale.Checked;
				dataRow["Description"] = textBoxName.Text;
				if (comboBoxClass.SelectedID != "")
				{
					dataRow["ClassID"] = comboBoxClass.SelectedID;
				}
				else
				{
					dataRow["ClassID"] = DBNull.Value;
				}
				dataRow["ItemType"] = comboBoxItemType.SelectedID;
				dataRow["CostMethod"] = 1;
				dataRow["IsTrackLot"] = checkBoxTrackLot.Checked;
				dataRow["IsTrackSerial"] = checkBoxTrackSerial.Checked;
				if (comboBoxItemType.SelectedType == ItemTypes.ConsignmentItem)
				{
					dataRow["IsTrackLot"] = true;
				}
				dataRow["ExcludeFromCatalogue"] = checkBoxExcludeFromCatalogue.Checked;
				if (comboBoxUOM.SelectedID != "")
				{
					dataRow["UnitID"] = comboBoxUOM.SelectedID;
				}
				else
				{
					dataRow["UnitID"] = DBNull.Value;
				}
				if (comboBoxCategory.SelectedID != "")
				{
					dataRow["CategoryID"] = comboBoxCategory.SelectedID;
				}
				else
				{
					dataRow["CategoryID"] = DBNull.Value;
				}
				dataRow["UPC"] = textBoxUPC.Text;
				dataRow["IsPriceEmbedded"] = checkBoxUPCPriceEmbedded.Checked;
				dataRow["VendorRef"] = textBoxVendorRef.Text;
				dataRow["UnitPrice1"] = textBoxStandardPrice.Text;
				dataRow["UnitPrice2"] = textBoxWholesalePrice.Text;
				dataRow["UnitPrice3"] = textBoxSpecialPrice.Text;
				dataRow["MinPrice"] = textBoxMinimumPrice.Text;
				dataRow["LastCost"] = textBoxLastCost.Text;
				dataRow["StandardCost"] = textBoxStandardCost.Text;
				dataRow["QuantityPerUnit"] = textBoxQuantityPerUnit.Text;
				dataRow["RackBin"] = textBoxRackBin.Text;
				if (textBox3PLRent.Text != "")
				{
					dataRow["W3PLRentPrice"] = textBox3PLRent.Text;
				}
				else
				{
					dataRow["W3PLRentPrice"] = 0;
				}
				if (textBoxReorderLevel.Text != "")
				{
					dataRow["ReorderLevel"] = textBoxReorderLevel.Text;
				}
				else
				{
					dataRow["ReorderLevel"] = DBNull.Value;
				}
				dataRow["Note"] = textBoxNote.Text;
				if (comboBoxManufacturer.SelectedID != "")
				{
					dataRow["ManufacturerID"] = comboBoxManufacturer.SelectedID;
				}
				else
				{
					dataRow["ManufacturerID"] = DBNull.Value;
				}
				if (comboBoxBrand.SelectedID != "")
				{
					dataRow["BrandID"] = comboBoxBrand.SelectedID;
				}
				else
				{
					dataRow["BrandID"] = DBNull.Value;
				}
				if (comboBoxOrigin.SelectedID != "")
				{
					dataRow["Origin"] = comboBoxOrigin.SelectedID;
				}
				else
				{
					dataRow["Origin"] = DBNull.Value;
				}
				if (comboBoxStyle.SelectedID != "")
				{
					dataRow["StyleID"] = comboBoxStyle.SelectedID;
				}
				else
				{
					dataRow["StyleID"] = DBNull.Value;
				}
				dataRow["Size"] = textBoxSize.Text;
				dataRow["Attribute"] = textBoxAttribute.Text;
				if (textBoxWeight.Text != "")
				{
					dataRow["Weight"] = textBoxWeight.Text;
				}
				else
				{
					dataRow["Weight"] = DBNull.Value;
				}
				if (textBoxWarranty.Text != "")
				{
					dataRow["WarrantyPeriod"] = textBoxWarranty.Text;
				}
				else
				{
					dataRow["WarrantyPeriod"] = DBNull.Value;
				}
				if (comboBoxPrefVendor.SelectedID != "")
				{
					dataRow["PreferredVendor"] = comboBoxPrefVendor.SelectedID;
				}
				else
				{
					dataRow["PreferredVendor"] = DBNull.Value;
				}
				if (incomeAccountID != "")
				{
					dataRow["IncomeAccount"] = incomeAccountID;
				}
				else
				{
					dataRow["IncomeAccount"] = DBNull.Value;
				}
				if (assetAccountID != "")
				{
					dataRow["AssetAccount"] = assetAccountID;
				}
				else
				{
					dataRow["AssetAccount"] = DBNull.Value;
				}
				if (cogsAccountID != "")
				{
					dataRow["COGSAccount"] = cogsAccountID;
				}
				else
				{
					dataRow["COGSAccount"] = DBNull.Value;
				}
				if (comboBoxItemType.SelectedType == ItemTypes.Assembly)
				{
					dataRow["BOMID"] = comboBoxBOM.SelectedID;
				}
				else
				{
					dataRow["BOMID"] = DBNull.Value;
				}
				if (comboBoxItemType.SelectedType == ItemTypes.Service)
				{
					if (comboBoxExpenseCode.SelectedID != "")
					{
						dataRow["ExpenseCode"] = comboBoxExpenseCode.SelectedID;
						ExpenseCode = comboBoxExpenseCode.SelectedID;
					}
					else
					{
						dataRow["ExpenseCode"] = DBNull.Value;
					}
				}
				else
				{
					dataRow["ExpenseCode"] = DBNull.Value;
				}
				dataRow["TaxOption"] = comboBoxTaxOption.SelectedIndex;
				if (comboBoxTaxOption.SelectedIndex == 1)
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(comboBoxMaterial.SelectedID))
				{
					dataRow["MaterialID"] = comboBoxMaterial.SelectedID;
				}
				else
				{
					dataRow["MaterialID"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(comboBoxFinish.SelectedID))
				{
					dataRow["FinishingID"] = comboBoxFinish.SelectedID;
				}
				else
				{
					dataRow["FinishingID"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(comboBoxColor.SelectedID))
				{
					dataRow["ColorID"] = comboBoxColor.SelectedID;
				}
				else
				{
					dataRow["ColorID"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(comboBoxGrade.SelectedID))
				{
					dataRow["GradeID"] = comboBoxGrade.SelectedID;
				}
				else
				{
					dataRow["GradeID"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(comboBoxStandard.SelectedID))
				{
					dataRow["StandardID"] = comboBoxStandard.SelectedID;
				}
				else
				{
					dataRow["StandardID"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(genericListComboBoxType1.SelectedID))
				{
					dataRow["PType1"] = genericListComboBoxType1.SelectedID;
				}
				else
				{
					dataRow["PType1"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(genericListComboBoxType2.SelectedID))
				{
					dataRow["PType2"] = genericListComboBoxType2.SelectedID;
				}
				else
				{
					dataRow["PType2"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(genericListComboBoxType3.SelectedID))
				{
					dataRow["PType3"] = genericListComboBoxType3.SelectedID;
				}
				else
				{
					dataRow["PType3"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(genericListComboBoxType4.SelectedID))
				{
					dataRow["PType4"] = genericListComboBoxType4.SelectedID;
				}
				else
				{
					dataRow["PType4"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(genericListComboBoxType5.SelectedID))
				{
					dataRow["PType5"] = genericListComboBoxType5.SelectedID;
				}
				else
				{
					dataRow["PType5"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(genericListComboBoxType6.SelectedID))
				{
					dataRow["PType6"] = genericListComboBoxType6.SelectedID;
				}
				else
				{
					dataRow["PType6"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(genericListComboBoxType7.SelectedID))
				{
					dataRow["PType7"] = genericListComboBoxType7.SelectedID;
				}
				else
				{
					dataRow["PType7"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(genericListComboBoxType8.SelectedID))
				{
					dataRow["PType8"] = genericListComboBoxType8.SelectedID;
				}
				else
				{
					dataRow["PType8"] = DBNull.Value;
				}
				if (comboBoxCompanyDivision.SelectedID != "")
				{
					dataRow["DivisionID"] = comboBoxCompanyDivision.SelectedID;
				}
				else
				{
					dataRow["DivisionID"] = DBNull.Value;
				}
				dataRow["Attribute1"] = textBoxAttribute1.Text;
				dataRow["Attribute2"] = textBoxAttribute2.Text;
				dataRow["Attribute3"] = textBoxAttribute3.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.ProductTable.Rows.Add(dataRow);
				}
				currentData.UnitTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridUnits.Rows)
				{
					DataRow dataRow2 = currentData.UnitTable.NewRow();
					dataRow2["ProductID"] = textBoxCode.Text;
					dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
					dataRow2["FactorType"] = row.Cells["Factor Type"].Value.ToString();
					dataRow2["Factor"] = row.Cells["Factor"].Value.ToString();
					currentData.UnitTable.Rows.Add(dataRow2);
				}
				if (CompanyPreferences.ActivatePartsDetails)
				{
					DataRow dataRow3 = (!isNewRecord) ? ((currentData.ProductPartsDetail.Rows.Count > 0) ? currentData.ProductPartsDetail.Rows[0] : currentData.ProductPartsDetail.NewRow()) : currentData.ProductPartsDetail.NewRow();
					dataRow.BeginEdit();
					dataRow3["ProductID"] = textBoxCode.Text;
					dataRow3["Specification"] = textBoxSpecification.Text;
					dataRow3["VehicleMakeID"] = comboBoxVehicleMake.SelectedID;
					dataRow3["VehicleTypeID"] = ComboBoxVehicleType.SelectedID;
					dataRow3["VehicleModelID"] = comboBoxVehicleModel.SelectedID;
					dataRow3["PartsMakeTypeID"] = comboboxPartsMakeType.SelectedID;
					dataRow3["PartsTypeID"] = comboBoxPartsType.SelectedID;
					dataRow3["PartsFamilyID"] = comboBoxPartsFamily.SelectedID;
					dataRow3["PartsChasisNo"] = textBoxChasisNo.Text;
					dataRow3["PartsModel"] = textBoxModelNo.Text;
					dataRow3["PartsEngineNo"] = textBoxEngineNo.Text;
					dataRow3["OEMCode"] = textBoxOEMCode.Text;
					dataRow3.EndEdit();
					if (isNewRecord || dataRow3.RowState == DataRowState.Detached)
					{
						currentData.ProductPartsDetail.Rows.Add(dataRow3);
					}
					foreach (UltraGridRow row2 in dataGridSubstituteItems.Rows)
					{
						DataRow dataRow4 = currentData.ProductSubstituteDetail.NewRow();
						dataRow4["ProductID"] = textBoxCode.Text;
						dataRow4["SubstituteProductID"] = row2.Cells["Item Code"].Value.ToString();
						dataRow4["SubProductDescription"] = row2.Cells["Description"].Value.ToString();
						if (!string.IsNullOrEmpty(row2.Cells["UnitPrice"].Value.ToString()))
						{
							dataRow4["UnitPrice"] = row2.Cells["UnitPrice"].Value.ToString();
						}
						else
						{
							dataRow4["UnitPrice"] = 0;
						}
						currentData.ProductSubstituteDetail.Rows.Add(dataRow4);
					}
					foreach (UltraGridRow row3 in datagridAppliedModels.Rows)
					{
						DataRow dataRow5 = currentData.ProductAppliedModelsDetail.NewRow();
						dataRow5["ProductID"] = textBoxCode.Text;
						dataRow5["VehicleMakeID"] = row3.Cells["Vehicle Make"].Value.ToString();
						dataRow5["VehicleTypeID"] = row3.Cells["Vehicle Type"].Value.ToString();
						dataRow5["Remarks"] = row3.Cells["Remarks"].Value.ToString();
						currentData.ProductAppliedModelsDetail.Rows.Add(dataRow5);
					}
				}
				currentData.ProductPriceListDetail.Rows.Clear();
				foreach (UltraGridRow row4 in dataEntryGridPriceList.Rows)
				{
					DataRow dataRow6 = currentData.ProductPriceListDetail.NewRow();
					dataRow6["ProductID"] = textBoxCode.Text;
					if (!string.IsNullOrEmpty(row4.Cells["UnitPrice1"].Value.ToString()))
					{
						dataRow6["UnitPrice1"] = row4.Cells["UnitPrice1"].Value.ToString();
					}
					else
					{
						dataRow6["UnitPrice1"] = 0;
					}
					if (!string.IsNullOrEmpty(row4.Cells["UnitPrice2"].Value.ToString()))
					{
						dataRow6["UnitPrice2"] = row4.Cells["UnitPrice2"].Value.ToString();
					}
					else
					{
						dataRow6["UnitPrice2"] = 0;
					}
					if (!string.IsNullOrEmpty(row4.Cells["UnitPrice3"].Value.ToString()))
					{
						dataRow6["UnitPrice3"] = row4.Cells["UnitPrice3"].Value.ToString();
					}
					else
					{
						dataRow6["UnitPrice3"] = 0;
					}
					if (!string.IsNullOrEmpty(row4.Cells["MinPrice"].Value.ToString()))
					{
						dataRow6["MinPrice"] = row4.Cells["MinPrice"].Value.ToString();
					}
					else
					{
						dataRow6["MinPrice"] = 0;
					}
					dataRow6["LocationID"] = row4.Cells["Location"].Value.ToString();
					dataRow6["UnitID"] = row4.Cells["Unit"].Value.ToString();
					currentData.ProductPriceListDetail.Rows.Add(dataRow6);
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
					currentData = Factory.ProductSystem.GetProductByID(id);
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
						if (toolStripButtonShowPicture.Checked && linkLoadImage.Visible)
						{
							LoadPhoto();
						}
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
				isLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["ProductID"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxAlias.Text = dataRow["Description2"].ToString();
					textDescription3.Text = dataRow["Description3"].ToString();
					if (dataRow["IsInactive"] != DBNull.Value)
					{
						checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					}
					else
					{
						checkBoxInactive.Checked = false;
					}
					if (dataRow["IsHoldSale"] != DBNull.Value)
					{
						checkBoxHoldSale.Checked = bool.Parse(dataRow["IsHoldSale"].ToString());
					}
					else
					{
						checkBoxHoldSale.Checked = false;
					}
					textBoxName.Text = dataRow["Description"].ToString();
					comboBoxClass.SelectedID = dataRow["ClassID"].ToString();
					comboBoxItemType.SelectedID = int.Parse(dataRow["ItemType"].ToString());
					comboBoxCostMethod.SelectedID = int.Parse(dataRow["CostMethod"].ToString());
					string text3 = comboBoxUOM.SelectedID = (comboBoxMainUnitTab.SelectedID = dataRow["UnitID"].ToString());
					comboBoxCompanyDivision.SelectedID = dataRow["DivisionID"].ToString();
					comboBoxCategory.SelectedID = dataRow["CategoryID"].ToString();
					textBoxUPC.Text = dataRow["UPC"].ToString();
					textBoxVendorRef.Text = dataRow["VendorRef"].ToString();
					textBoxRackBin.Text = dataRow["RackBin"].ToString();
					textBox3PLRent.Text = dataRow["W3PLRentPrice"].ToString();
					if (dataRow["IsPriceEmbedded"] != DBNull.Value)
					{
						checkBoxUPCPriceEmbedded.Checked = bool.Parse(dataRow["IsPriceEmbedded"].ToString());
					}
					else
					{
						checkBoxUPCPriceEmbedded.Checked = false;
					}
					if (dataRow["ExcludeFromCatalogue"] != DBNull.Value)
					{
						checkBoxExcludeFromCatalogue.Checked = bool.Parse(dataRow["ExcludeFromCatalogue"].ToString());
					}
					else
					{
						checkBoxExcludeFromCatalogue.Checked = false;
					}
					if (dataRow["IsTrackLot"] != DBNull.Value)
					{
						checkBoxTrackLot.Checked = bool.Parse(dataRow["IsTrackLot"].ToString());
					}
					else
					{
						checkBoxTrackLot.Checked = false;
					}
					if (dataRow["IsTrackSerial"] != DBNull.Value)
					{
						checkBoxTrackSerial.Checked = bool.Parse(dataRow["IsTrackSerial"].ToString());
					}
					else
					{
						checkBoxTrackSerial.Checked = false;
					}
					if (dataRow["UnitPrice1"] != DBNull.Value)
					{
						textBoxStandardPrice.Text = decimal.Parse(dataRow["UnitPrice1"].ToString()).ToString(Format.UnitPriceFormat);
					}
					else
					{
						textBoxStandardPrice.Text = 0.ToString(Format.UnitPriceFormat);
					}
					if (dataRow["UnitPrice2"] != DBNull.Value)
					{
						textBoxWholesalePrice.Text = decimal.Parse(dataRow["UnitPrice2"].ToString()).ToString(Format.UnitPriceFormat);
					}
					if (dataRow["UnitPrice3"] != DBNull.Value)
					{
						textBoxSpecialPrice.Text = decimal.Parse(dataRow["UnitPrice3"].ToString()).ToString(Format.UnitPriceFormat);
					}
					if (dataRow["MinPrice"] != DBNull.Value)
					{
						textBoxMinimumPrice.Text = decimal.Parse(dataRow["MinPrice"].ToString()).ToString(Format.UnitPriceFormat);
					}
					if (canAccessCost)
					{
						if (dataRow["CurrentAvgCost"] != DBNull.Value)
						{
							textBoxAvgCost.Text = decimal.Parse(dataRow["CurrentAvgCost"].ToString()).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxAvgCost.Text = 0.ToString(Format.TotalAmountFormat);
						}
						if (dataRow["LastPurchaseCost"] != DBNull.Value)
						{
							textBoxLastCost.Text = decimal.Parse(dataRow["LastPurchaseCost"].ToString()).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxLastCost.Text = 0.ToString(Format.TotalAmountFormat);
						}
						if (dataRow["StandardCost"] != DBNull.Value)
						{
							textBoxStandardCost.Text = decimal.Parse(dataRow["StandardCost"].ToString()).ToString(Format.UnitPriceFormat);
						}
						else
						{
							textBoxStandardCost.Text = 0.ToString(Format.TotalAmountFormat);
						}
					}
					else
					{
						textBoxAvgCost.Visible = false;
						textBoxLastCost.Visible = false;
						textBoxStandardCost.Visible = false;
					}
					textBoxQuantityPerUnit.Text = dataRow["QuantityPerUnit"].ToString();
					textBoxReorderLevel.Text = dataRow["ReorderLevel"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxQuantityOnhand.Text = dataRow["Quantity"].ToString();
					comboBoxManufacturer.SelectedID = dataRow["ManufacturerID"].ToString();
					comboBoxBrand.SelectedID = dataRow["BrandID"].ToString();
					comboBoxOrigin.SelectedID = dataRow["Origin"].ToString();
					comboBoxStyle.SelectedID = dataRow["StyleID"].ToString();
					textBoxSize.Text = dataRow["Size"].ToString();
					textBoxAttribute.Text = dataRow["Attribute"].ToString();
					textBoxWeight.Text = dataRow["Weight"].ToString();
					textBoxWarranty.Text = dataRow["WarrantyPeriod"].ToString();
					comboBoxPrefVendor.SelectedID = dataRow["PreferredVendor"].ToString();
					incomeAccountID = dataRow["IncomeAccount"].ToString();
					assetAccountID = dataRow["AssetAccount"].ToString();
					cogsAccountID = dataRow["COGSAccount"].ToString();
					comboBoxExpenseCode.SelectedID = dataRow["ExpenseCode"].ToString();
					ExpenseCode = dataRow["ExpenseCode"].ToString();
					if (comboBoxItemType.SelectedType == ItemTypes.Assembly)
					{
						comboBoxBOM.SelectedID = dataRow["BOMID"].ToString();
					}
					else
					{
						comboBoxBOM.Clear();
					}
					if (!string.IsNullOrEmpty(dataRow["MaterialID"].ToString()))
					{
						comboBoxMaterial.SelectedID = dataRow["MaterialID"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["FinishingID"].ToString()))
					{
						comboBoxFinish.SelectedID = dataRow["FinishingID"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["ColorID"].ToString()))
					{
						comboBoxColor.SelectedID = dataRow["ColorID"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["GradeID"].ToString()))
					{
						comboBoxGrade.SelectedID = dataRow["GradeID"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["StandardID"].ToString()))
					{
						comboBoxStandard.SelectedID = dataRow["StandardID"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["PType1"].ToString()))
					{
						genericListComboBoxType1.SelectedID = dataRow["PType1"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["PType2"].ToString()))
					{
						genericListComboBoxType2.SelectedID = dataRow["PType2"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["PType3"].ToString()))
					{
						genericListComboBoxType3.SelectedID = dataRow["PType3"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["PType4"].ToString()))
					{
						genericListComboBoxType4.SelectedID = dataRow["PType4"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["PType5"].ToString()))
					{
						genericListComboBoxType5.SelectedID = dataRow["PType5"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["PType6"].ToString()))
					{
						genericListComboBoxType6.SelectedID = dataRow["PType6"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["PType7"].ToString()))
					{
						genericListComboBoxType7.SelectedID = dataRow["PType7"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["PType8"].ToString()))
					{
						genericListComboBoxType8.SelectedID = dataRow["PType8"].ToString();
					}
					textBoxAttribute1.Text = dataRow["Attribute1"].ToString();
					textBoxAttribute2.Text = dataRow["Attribute2"].ToString();
					textBoxAttribute3.Text = dataRow["Attribute3"].ToString();
					if (!string.IsNullOrEmpty(dataRow["TaxOption"].ToString()))
					{
						comboBoxTaxOption.SelectedIndex = int.Parse(dataRow["TaxOption"].ToString());
					}
					else
					{
						comboBoxTaxOption.SelectedIndex = 0;
					}
					if (!string.IsNullOrEmpty(dataRow["TaxGroupID"].ToString()))
					{
						comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
					}
					else
					{
						comboBoxTaxGroup.Clear();
					}
					if (dataRow["HasPhoto"] != DBNull.Value)
					{
						bool flag = Convert.ToBoolean(byte.Parse(dataRow["HasPhoto"].ToString()));
						linkLoadImage.Visible = flag;
						linkRemovePicture.Enabled = flag;
						if (flag)
						{
							pictureBoxPhoto.Image = null;
						}
						else
						{
							pictureBoxPhoto.Image = pictureBoxNoImage.Image;
						}
					}
					else
					{
						linkLoadImage.Visible = false;
						pictureBoxPhoto.Image = pictureBoxNoImage.Image;
						linkRemovePicture.Enabled = false;
					}
					SetHeaderName();
					DataTable dataTable = dataGridUnits.DataSource as DataTable;
					dataTable?.Rows.Clear();
					dataTable.BeginLoadData();
					foreach (DataRow row in currentData.UnitTable.Rows)
					{
						if (!(row["UnitID"].ToString() == dataRow["UnitID"].ToString()))
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Unit"] = row["UnitID"];
							dataRow3["Factor Type"] = row["FactorType"];
							dataRow3["Factor"] = row["Factor"];
							string a = row["FactorType"].ToString();
							decimal result = 1m;
							decimal.TryParse(row["Factor"].ToString(), out result);
							if (a == "M")
							{
								dataRow3["Description"] = "1 " + comboBoxUOM.Text + " = " + result.ToString() + " " + row["UnitID"].ToString();
							}
							else
							{
								dataRow3["Description"] = "1 " + row["UnitID"].ToString() + " = " + result.ToString() + " " + comboBoxUOM.Text;
							}
							dataTable.Rows.Add(dataRow3);
						}
					}
					dataTable.EndLoadData();
					dataTable.AcceptChanges();
					if (currentData.Tables.Contains("Product_Parts_Detail") && currentData.ProductPartsDetail.Rows.Count > 0)
					{
						DataRow dataRow4 = currentData.Tables["Product_Parts_Detail"].Rows[0];
						if (dataRow4["VehicleMakeID"] != DBNull.Value && dataRow4["VehicleMakeID"].ToString() != "")
						{
							comboBoxVehicleMake.SelectedID = dataRow4["VehicleMakeID"].ToString();
						}
						else
						{
							comboBoxVehicleMake.Clear();
						}
						if (dataRow4["VehicleTypeID"] != DBNull.Value && dataRow4["VehicleTypeID"].ToString() != "")
						{
							ComboBoxVehicleType.SelectedID = dataRow4["VehicleTypeID"].ToString();
						}
						else
						{
							ComboBoxVehicleType.Clear();
						}
						if (dataRow4["VehicleModelID"] != DBNull.Value && dataRow4["VehicleModelID"].ToString() != "")
						{
							comboBoxVehicleModel.SelectedID = dataRow4["VehicleModelID"].ToString();
						}
						else
						{
							comboBoxVehicleModel.Clear();
						}
						if (dataRow4["PartsMakeTypeID"] != DBNull.Value && dataRow4["PartsMakeTypeID"].ToString() != "")
						{
							comboboxPartsMakeType.SelectedID = dataRow4["PartsMakeTypeID"].ToString();
						}
						else
						{
							comboboxPartsMakeType.Clear();
						}
						if (dataRow4["PartsTypeID"] != DBNull.Value && dataRow4["PartsTypeID"].ToString() != "")
						{
							comboBoxPartsType.SelectedID = dataRow4["PartsTypeID"].ToString();
						}
						else
						{
							comboBoxPartsType.Clear();
						}
						if (dataRow4["PartsFamilyID"] != DBNull.Value && dataRow4["PartsFamilyID"].ToString() != "")
						{
							comboBoxPartsFamily.SelectedID = dataRow4["PartsFamilyID"].ToString();
						}
						else
						{
							comboBoxPartsFamily.Clear();
						}
						textBoxSpecification.Text = dataRow4["Specification"].ToString();
						textBoxChasisNo.Text = dataRow4["PartsChasisNo"].ToString();
						textBoxModelNo.Text = dataRow4["PartsModel"].ToString();
						textBoxEngineNo.Text = dataRow4["PartsEngineNo"].ToString();
						textBoxOEMCode.Text = dataRow4["OEMCode"].ToString();
					}
					else
					{
						textBoxSpecification.Clear();
						textBoxChasisNo.Clear();
						textBoxModelNo.Clear();
						textBoxEngineNo.Clear();
						textBoxOEMCode.Clear();
						comboBoxVehicleMake.Clear();
						ComboBoxVehicleType.Clear();
						comboBoxVehicleModel.Clear();
						comboboxPartsMakeType.Clear();
						comboBoxPartsType.Clear();
						comboBoxPartsFamily.Clear();
					}
					DataTable dataTable2 = dataGridSubstituteItems.DataSource as DataTable;
					dataTable2?.Rows.Clear();
					dataTable2.BeginLoadData();
					foreach (DataRow row2 in currentData.ProductSubstituteDetail.Rows)
					{
						DataRow dataRow6 = dataTable2.NewRow();
						dataRow6["Item Code"] = row2["SubstituteProductID"];
						dataRow6["Description"] = row2["SubProductDescription"];
						dataRow6["UnitPrice"] = row2["UnitPrice"];
						dataTable2.Rows.Add(dataRow6);
					}
					dataTable2.EndLoadData();
					dataTable2.AcceptChanges();
					DataTable dataTable3 = datagridAppliedModels.DataSource as DataTable;
					dataTable3?.Rows.Clear();
					dataTable3.BeginLoadData();
					foreach (DataRow row3 in currentData.ProductAppliedModelsDetail.Rows)
					{
						DataRow dataRow8 = dataTable3.NewRow();
						dataRow8["Vehicle Make"] = row3["VehicleMakeID"];
						dataRow8["Vehicle Type"] = row3["VehicleTypeID"];
						dataRow8["Remarks"] = row3["Remarks"];
						dataTable3.Rows.Add(dataRow8);
					}
					dataTable3.EndLoadData();
					dataTable3.AcceptChanges();
					DataTable dataTable4 = dataEntryGridPriceList.DataSource as DataTable;
					dataTable4.Rows.Clear();
					if (currentData.Tables.Contains("Product_PriceList_Detail") && currentData.ProductPriceListDetail.Rows.Count != 0)
					{
						foreach (DataRow row4 in currentData.Tables["Product_PriceList_Detail"].Rows)
						{
							DataRow dataRow10 = dataTable4.NewRow();
							dataRow10["UnitPrice1"] = row4["UnitPrice1"];
							dataRow10["UnitPrice2"] = row4["UnitPrice2"];
							dataRow10["UnitPrice3"] = row4["UnitPrice3"];
							dataRow10["MinPrice"] = row4["MinPrice"];
							dataRow10["Location"] = row4["LocationID"];
							dataRow10["Unit"] = row4["UnitID"];
							dataTable4.Rows.Add(dataRow10);
						}
						dataTable4.AcceptChanges();
						if (currentData.Tables["UDF"].Rows.Count > 0)
						{
							_ = currentData.Tables["UDF"].Rows[0];
							foreach (DataColumn column in currentData.Tables["UDF"].Columns)
							{
								_ = (column.ColumnName == "EntityID");
							}
						}
						else
						{
							udfEntryGrid.ClearData();
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				isLoading = false;
			}
		}

		private void DisableUnitsCombo(string productID)
		{
			if (Factory.ProductSystem.IsExistProductTransaction(productID))
			{
				UnitComboBox unitComboBox = comboBoxUOM;
				bool enabled = comboBoxMainUnitTab.Enabled = false;
				unitComboBox.Enabled = enabled;
			}
			else
			{
				UnitComboBox unitComboBox2 = comboBoxUOM;
				bool enabled = comboBoxMainUnitTab.Enabled = true;
				unitComboBox2.Enabled = enabled;
			}
		}

		private bool DisableGridUnits(string productID)
		{
			return Factory.ProductSystem.IsExistProductTransaction(productID);
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
				bool flag = (!isNewRecord) ? Factory.ProductSystem.UpdateProduct(currentData) : Factory.ProductSystem.CreateProduct(currentData);
				if (flag)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Product, needRefresh: true);
					ComboDataHelper.SetRefreshStatus(DataComboType.ProductUnit, needRefresh: true);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Product", "ProductID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (comboBoxItemType.SelectedIndex == -1)
			{
				ErrorHelper.InformationMessage("Please select the item type.");
				comboBoxItemType.Focus();
				return false;
			}
			if (comboBoxCostMethod.Enabled && comboBoxCostMethod.SelectedIndex == -1)
			{
				ErrorHelper.InformationMessage("Please select the costing method.");
				comboBoxItemType.Focus();
				return false;
			}
			if (dataGridUnits.Rows.Count > 0 && comboBoxUOM.SelectedID == "")
			{
				ErrorHelper.InformationMessage("You must select the main UOM when other units are added.");
				return false;
			}
			if (dataGridUnits.Rows.Any((UltraGridRow row) => string.IsNullOrEmpty(row.GetCellValue("Factor Type").ToString()) || string.IsNullOrEmpty(row.GetCellValue("Factor").ToString())))
			{
				ErrorHelper.InformationMessage("Please select the Factor or Factor Type for the rows!");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
				return false;
			}
			if (!isNewRecord && checkBoxInactive.Checked && Factory.ProductSystem.GetProductQuantity(textBoxCode.Text, "") != 0f)
			{
				ErrorHelper.WarningMessage("An item that has balance cannot be inactive.");
				return false;
			}
			if (comboBoxItemType.SelectedID == 2 || comboBoxItemType.SelectedID == 3)
			{
				if (incomeAccountID == "")
				{
					ErrorHelper.InformationMessage("Please select a sales account for this item.");
					return false;
				}
				if (cogsAccountID == "")
				{
					ErrorHelper.InformationMessage("Please select a purchase account for this item.");
					return false;
				}
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Product", "ProductID");
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxAlias.Clear();
			textDescription3.Clear();
			comboBoxClass.Clear();
			comboBoxUOM.Clear();
			comboBoxCategory.Clear();
			textBoxUPC.Clear();
			textBoxVendorRef.Clear();
			textBoxStandardPrice.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxWholesalePrice.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxSpecialPrice.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxMinimumPrice.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxLastCost.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxAvgCost.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxStandardCost.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxQuantityPerUnit.Text = "1";
			textBoxReorderLevel.Text = "0";
			textBoxNote.Clear();
			textBoxQuantityOnhand.Text = "0";
			comboBoxManufacturer.Clear();
			comboBoxBrand.Clear();
			comboBoxOrigin.Clear();
			comboBoxStyle.Clear();
			textBoxSize.Clear();
			textBoxAttribute.Clear();
			textBoxWeight.Text = "0";
			textBoxWarranty.Text = "0";
			comboBoxPrefVendor.Clear();
			incomeAccountID = "";
			textBoxRackBin.Clear();
			assetAccountID = "";
			cogsAccountID = "";
			comboBoxBOM.Clear();
			comboBoxExpenseCode.Clear();
			textBox3PLRent.Text = 0.ToString(Format.TotalAmountFormat);
			if (comboBoxItemType.SelectedType == ItemTypes.ConsignmentItem)
			{
				checkBoxTrackLot.Checked = true;
			}
			else
			{
				checkBoxTrackLot.Checked = false;
			}
			checkBoxTrackSerial.Checked = false;
			comboBoxCompanyDivision.Clear();
			udfEntryGrid.ClearData();
			checkBoxUPCPriceEmbedded.Checked = false;
			checkBoxInactive.Checked = false;
			checkBoxHoldSale.Checked = false;
			comboBoxMainUnitTab.Clear();
			dataGridUnits.Clear();
			datagridAppliedModels.Clear();
			dataGridSubstituteItems.Clear();
			dataEntryGridPriceList.Clear();
			textBoxSpecification.Clear();
			textBoxChasisNo.Clear();
			textBoxModelNo.Clear();
			textBoxEngineNo.Clear();
			textBoxOEMCode.Clear();
			comboBoxVehicleMake.Clear();
			ComboBoxVehicleType.Clear();
			comboBoxVehicleModel.Clear();
			comboboxPartsMakeType.Clear();
			comboBoxPartsType.Clear();
			comboBoxPartsFamily.Clear();
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			comboBoxMaterial.Clear();
			comboBoxFinish.Clear();
			comboBoxGrade.Clear();
			comboBoxStandard.Clear();
			comboBoxColor.Clear();
			textBoxAttribute1.Clear();
			textBoxAttribute2.Clear();
			textBoxAttribute3.Clear();
			genericListComboBoxType1.Clear();
			genericListComboBoxType2.Clear();
			genericListComboBoxType3.Clear();
			genericListComboBoxType4.Clear();
			genericListComboBoxType5.Clear();
			genericListComboBoxType6.Clear();
			genericListComboBoxType7.Clear();
			genericListComboBoxType8.Clear();
			comboBoxTaxGroup.Clear();
			comboBoxTaxOption.SelectedIndex = 0;
			if (CompanyPreferences.TaxEntityTypes.Contains("P"))
			{
				comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
				comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
			}
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void ProductGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ProductGroupDetailsForm_Validated(object sender, EventArgs e)
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
				if (Factory.ProductSystem.GetProductTransactionsExists(textBoxCode.Text))
				{
					ErrorHelper.ErrorMessage("Some transactions already done with this item. You can't delete " + textBoxCode.Text);
					return false;
				}
				bool num = Factory.ProductSystem.DeleteProduct(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Product, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextIDByCardSecurity("Product", "ProductID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousIDByCardSecurity("Product", "ProductID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastIDByCardSecurity("Product", "ProductID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstIDByCardSecurity("Product", "ProductID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.ProductSystem.IsProductExist(toolStripTextBoxFind.Text))
				{
					LoadData(toolStripTextBoxFind.Text);
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

		private void ProductDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				if (CompanyPreferences.TaxEntityTypes.Contains("P"))
				{
					comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
					comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
			if (!GlobalRules.IsFeatureAllowed(EditionFeatures.LotTracking))
			{
				CheckBox checkBox = checkBoxTrackLot;
				bool visible = checkBoxTrackSerial.Visible = false;
				checkBox.Visible = visible;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice1))
			{
				textBoxStandardPrice.Visible = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice2))
			{
				textBoxWholesalePrice.Visible = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice3))
			{
				textBoxSpecialPrice.Visible = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
			{
				textBoxMinimumPrice.Visible = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void mmLabel14_Click(object sender, EventArgs e)
		{
		}

		private void comboBoxClass_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProductClass(comboBoxClass.SelectedID);
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProductCategory(comboBoxCategory.SelectedID);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditUOM(comboBoxUOM.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProductManufacturer(comboBoxManufacturer.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProductBrand(comboBoxBrand.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxOrigin.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProductStyle(comboBoxStyle.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxPrefVendor.SelectedID);
		}

		private void SetupUnitGrid()
		{
			dataGridUnits.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Unit");
			dataTable.Columns.Add("Factor Type", typeof(char));
			dataTable.Columns.Add("Factor", typeof(decimal));
			dataTable.Columns.Add("Description");
			dataGridUnits.DataSource = dataTable;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 64;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Unit"].ValueList = comboBoxGridUnit;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Factor"].MinValue = 0;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Factor"].MaxValue = 100000;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Factor"].MaxLength = 20;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Factor"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Factor"].Format = "n";
			dataGridUnits.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add("M", "Multiply");
			valueList.ValueListItems.Add("D", "Divide");
			dataGridUnits.DisplayLayout.Bands[0].Columns["Factor Type"].ValueList = valueList;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Factor Type"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridUnits.Width) / 100;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Unit"].Width = checked(10 * dataGridUnits.Width) / 100;
			dataGridUnits.DisplayLayout.Bands[0].Columns["Factor"].Width = checked(10 * dataGridUnits.Width) / 100;
		}

		private void SetupPriceListGrid()
		{
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Location");
			dataTable.Columns.Add("Unit");
			dataTable.Columns.Add("UnitPrice1", typeof(decimal));
			dataTable.Columns.Add("UnitPrice2", typeof(decimal));
			dataTable.Columns.Add("UnitPrice3", typeof(decimal));
			dataTable.Columns.Add("MinPrice", typeof(decimal));
			dataEntryGridPriceList.DataSource = dataTable;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 64;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Location"].ValueList = comboBoxGridLocation;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 64;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["Unit"].ValueList = comboBoxGridUnit;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice1"].MinValue = 0;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice1"].MaxValue = 100000;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice1"].MaxLength = 20;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellAppearance.TextHAlign = HAlign.Right;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice1"].Format = "n";
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice1"].Header.Caption = CompanyPreferences.UnitPrice1Title;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice2"].MinValue = 0;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice2"].MaxValue = 100000;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice2"].MaxLength = 20;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice2"].CellAppearance.TextHAlign = HAlign.Right;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice2"].Format = "n";
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice2"].Header.Caption = CompanyPreferences.UnitPrice2Title;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice3"].MinValue = 0;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice3"].MaxValue = 100000;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice3"].MaxLength = 20;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice3"].CellAppearance.TextHAlign = HAlign.Right;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice3"].Format = "n";
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["UnitPrice3"].Header.Caption = CompanyPreferences.UnitPrice3Title;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["MinPrice"].MinValue = 0;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["MinPrice"].MaxValue = 100000;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["MinPrice"].MaxLength = 20;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["MinPrice"].CellAppearance.TextHAlign = HAlign.Right;
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["MinPrice"].Format = "n";
			dataEntryGridPriceList.DisplayLayout.Bands[0].Columns["MinPrice"].Header.Caption = "Minimum Price";
		}

		private void comboBoxMainUnitTab_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!comboBoxUOM.Focused)
			{
				comboBoxUOM.SelectedID = comboBoxMainUnitTab.SelectedID;
			}
		}

		private void comboBoxUOM_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!comboBoxMainUnitTab.Focused)
			{
				comboBoxMainUnitTab.SelectedID = comboBoxUOM.SelectedID;
			}
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
		}

		private void buttonRemoveImage_Click(object sender, EventArgs e)
		{
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddProductPhoto(textBoxCode.Text, image, isMatrixParent: false))
					{
						pictureBoxPhoto.Image = image;
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
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.ProductSystem.RemoveProductPhoto(textBoxCode.Text))
					{
						pictureBoxPhoto.Image = pictureBoxNoImage.Image;
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot remove the image.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot remove image.");
			}
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetProductThumbnailImage(textBoxCode.Text, isProductParentID: false);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ProductListFormObj);
		}

		private void ultraFormattedLinkLabel12_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (textBoxCode.Text != "" && !isNewRecord)
			{
				string text = textBoxCode.Text;
				FormActivator.ProductQuantityFormObj.LoadData(text);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void toolStripButtonShowPicture_Click(object sender, EventArgs e)
		{
		}

		private void linkLabelBOM_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBOM(comboBoxBOM.SelectedID);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Vendors;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void checkBoxTrackLot_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxTrackLot.Checked)
			{
				checkBoxTrackSerial.Checked = false;
			}
		}

		private void checkBoxTrackSerial_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxTrackSerial.Checked)
			{
				checkBoxTrackLot.Checked = false;
			}
		}

		private void groupBoxAccounts_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonDuplicate_Click(object sender, EventArgs e)
		{
			if (CanClose() && !(textBoxCode.Text == "") && ErrorHelper.QuestionMessageYesNo("Do you want to duplicate this item?") == DialogResult.Yes)
			{
				IsNewRecord = true;
				formManager.IsForcedDirty = true;
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void buttonAccounts_Click(object sender, EventArgs e)
		{
			InventoryAccountsForm inventoryAccountsForm = new InventoryAccountsForm();
			inventoryAccountsForm.AssetAccount = assetAccountID;
			inventoryAccountsForm.COGSAccount = cogsAccountID;
			inventoryAccountsForm.IncomeAccount = incomeAccountID;
			inventoryAccountsForm.ItemType = comboBoxItemType.SelectedType;
			if (inventoryAccountsForm.ShowDialog() == DialogResult.OK)
			{
				assetAccountID = inventoryAccountsForm.AssetAccount;
				cogsAccountID = inventoryAccountsForm.COGSAccount;
				incomeAccountID = inventoryAccountsForm.IncomeAccount;
				if (!formManager.IsForcedDirty)
				{
					formManager.IsForcedDirty = inventoryAccountsForm.IsDirty;
				}
			}
		}

		private void comboBoxManufacturer_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void importToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new ImportProductFromExcelForm().Show();
		}

		private void SetupSubstituteItemsGrid()
		{
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Item Code");
			dataTable.Columns.Add("Description");
			dataTable.Columns.Add("UnitPrice");
			dataGridSubstituteItems.DataSource = dataTable;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxSubstiProduct;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns[0].Width = checked(30 * dataGridSubstituteItems.Width) / 100;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns[1].Width = checked(50 * dataGridSubstituteItems.Width) / 100;
			dataGridSubstituteItems.DisplayLayout.Bands[0].Columns[2].Width = checked(10 * dataGridSubstituteItems.Width) / 100;
			dataGridSubstituteItems.SetupUI();
		}

		private void SetupAppliedModelsGrid()
		{
			datagridAppliedModels.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Vehicle Make");
			dataTable.Columns.Add("Vehicle Type");
			dataTable.Columns.Add("Remarks");
			datagridAppliedModels.DataSource = dataTable;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Make"].CharacterCasing = CharacterCasing.Upper;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Make"].MaxLength = 64;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Make"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Make"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Make"].ValueList = comboBoxGridVehicleMake;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Type"].CharacterCasing = CharacterCasing.Upper;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Type"].MaxLength = 64;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Type"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Type"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns["Vehicle Type"].ValueList = comboBoxgridVehicleType;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns[0].Width = checked(40 * datagridAppliedModels.Width) / 100;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns[1].Width = checked(40 * datagridAppliedModels.Width) / 100;
			datagridAppliedModels.DisplayLayout.Bands[0].Columns[2].Width = checked(50 * datagridAppliedModels.Width) / 100;
			datagridAppliedModels.SetupUI();
		}

		private void dataGridSubstituteItems_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			try
			{
				if (dataGridSubstituteItems.ActiveRow != null)
				{
					string id = dataGridSubstituteItems.ActiveRow.Cells["Item Code"].Value.ToString();
					ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[0];
					new FormHelper().EditProduct(id);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.VehicleMake, comboBoxVehicleMake.SelectedID);
		}

		private void ultraFormattedLinkLabel15_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.VehicleType, ComboBoxVehicleType.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.VehicleModel, comboBoxVehicleModel.SelectedID);
		}

		private void ultraFormattedLinkLabel13_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.PartsMakeType, comboboxPartsMakeType.SelectedID);
		}

		private void ultraFormattedLinkLabel16_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.PartsType, comboBoxPartsType.SelectedID);
		}

		private void ultraFormattedLinkLabel14_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.PartsFamily, comboBoxPartsFamily.SelectedID);
		}

		private void buttonTranslate_Click(object sender, EventArgs e)
		{
			if (WebRequestTest())
			{
				textDescription3.Text = Translate(textBoxName.Text, "en", "ar");
			}
			else
			{
				ErrorHelper.WarningMessage("Couldn't connect with service.");
			}
		}

		private void ultraFormattedLinkLabel17_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.Material, comboBoxMaterial.SelectedID);
		}

		private void ultraFormattedLinkLabel19_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.Color, comboBoxColor.SelectedID);
		}

		private void ultraFormattedLinkLabel18_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.Finish, comboBoxFinish.SelectedID);
		}

		private void ultraFormattedLinkLabel20_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.ProductGrade, comboBoxGrade.SelectedID);
		}

		private void ultraFormattedLinkLabel21_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.Standard, comboBoxStandard.SelectedID);
		}

		private void comboBoxTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxOption.SelectedIndex == 1)
			{
				comboBoxTaxGroup.ReadOnly = false;
				return;
			}
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.Clear();
		}

		private void ultraFormattedLinkLabel22_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxTaxGroup.SelectedID);
		}

		private void CreateContextMenu()
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Inventory Ledger");
			toolStripMenuItem.Click += menuItem_Click;
			toolStripMenuItem.Name = "Inventory Ledger";
			contextMenuStrip.Items.Add(toolStripMenuItem);
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Location Wise on Hand");
			toolStripMenuItem2.Click += menuItem_Click;
			toolStripMenuItem2.Name = "Location Wise on Hand";
			contextMenuStrip.Items.Add(toolStripMenuItem2);
			ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Sale Statistics");
			toolStripMenuItem3.Click += menuItem_Click;
			toolStripMenuItem3.Name = "Sale Statistics";
			contextMenuStrip.Items.Add(toolStripMenuItem3);
			ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem("Available Lots");
			toolStripMenuItem4.Click += menuItem_Click;
			toolStripMenuItem4.Name = "Available Lots";
			contextMenuStrip.Items.Add(toolStripMenuItem4);
			ContextMenuStrip = contextMenuStrip;
		}

		private void menuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ToolStripItem toolStripItem = (ToolStripItem)sender;
				if (toolStripItem.Name == "Inventory Ledger")
				{
					InventoryLedgerForm inventoryLedgerForm = new InventoryLedgerForm();
					inventoryLedgerForm.SelectedID = textBoxCode.Text;
					inventoryLedgerForm.Show();
					inventoryLedgerForm.BringToFront();
				}
				else if (toolStripItem.Name == "Sale Statistics")
				{
					InventorySalesStatisticForm inventorySalesStatisticForm = new InventorySalesStatisticForm();
					inventorySalesStatisticForm.SelectedID = textBoxCode.Text;
					inventorySalesStatisticForm.CategoryID = comboBoxCategory.SelectedID;
					inventorySalesStatisticForm.Show();
					inventorySalesStatisticForm.BringToFront();
				}
				else if (toolStripItem.Name == "Location Wise on Hand")
				{
					FormActivator.ProductQuantityFormObj.LoadData(textBoxCode.Text);
					FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
				}
				else if (toolStripItem.Name == "Available Lots")
				{
					ProductLotDetailsDialog productLotDetailsDialog = new ProductLotDetailsDialog();
					productLotDetailsDialog.ProductID = textBoxCode.Text;
					productLotDetailsDialog.Description = textBoxName.Text;
					productLotDetailsDialog.LoadData(textBoxCode.Text);
					productLotDetailsDialog.ShowDialog(this);
				}
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void comboBoxManufacturer_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxOrigin_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxBrand_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void checkedListBoxFields_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void checkedListBoxFields_ItemCheck(object sender, ItemCheckEventArgs e)
		{
		}

		private void buttonGenerate_Click(object sender, EventArgs e)
		{
			ProductData productData = new ProductData();
			if (GetData())
			{
				productData = currentData;
				string text = Factory.ProductSystem.GenerateProductID(productData);
				textBoxCode.Text = text;
			}
		}

		private void ultraFormattedLinkLabelType1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericProductTypeList(GenericListTypes.ProductType1, genericListComboBoxType1.SelectedID);
		}

		private void ultraFormattedLinkLabelType2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericProductTypeList(GenericListTypes.ProductType2, genericListComboBoxType2.SelectedID);
		}

		private void ultraFormattedLinkLabelType3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericProductTypeList(GenericListTypes.ProductType3, genericListComboBoxType3.SelectedID);
		}

		private void ultraFormattedLinkLabelType4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericProductTypeList(GenericListTypes.ProductType4, genericListComboBoxType4.SelectedID);
		}

		private void ultraFormattedLinkLabelType5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericProductTypeList(GenericListTypes.ProductType5, genericListComboBoxType5.SelectedID);
		}

		private void ultraFormattedLinkLabelType6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericProductTypeList(GenericListTypes.ProductType6, genericListComboBoxType6.SelectedID);
		}

		private void ultraFormattedLinkLabelType7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericProductTypeList(GenericListTypes.ProductType7, genericListComboBoxType7.SelectedID);
		}

		private void ultraFormattedLinkLabelType8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericProductTypeList(GenericListTypes.ProductType8, genericListComboBoxType8.SelectedID);
		}

		private void ultraFormattedLinkLabelCompanyDivision_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCompanyDivision(comboBoxCompanyDivision.SelectedID);
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
			Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance179 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance183 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance184 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance185 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance186 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance187 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance188 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance189 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance190 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance191 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance192 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance193 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance194 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance195 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance196 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance197 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance198 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance199 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance200 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance201 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance202 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance203 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance204 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance205 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance206 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance207 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance208 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance209 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance210 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance211 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance212 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance213 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance214 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance215 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance216 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance217 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance218 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance219 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance220 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance221 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance222 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance223 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance224 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance225 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance226 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance227 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance228 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance229 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance230 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance231 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance232 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance233 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance234 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance235 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance236 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance237 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance238 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance239 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance240 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance241 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance242 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance243 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance244 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance245 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance246 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance247 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance248 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance249 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance250 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance251 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance252 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance253 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance254 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance255 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance256 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance257 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance258 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance259 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance260 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance261 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance262 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance263 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance264 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance265 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance266 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance267 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance268 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance269 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance270 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance271 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance272 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance273 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance274 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance275 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance276 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance277 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance278 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance279 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance280 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance281 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance282 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance283 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance284 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance285 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance286 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance287 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance288 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance289 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance290 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance291 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance292 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance293 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance294 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance295 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance296 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance297 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance298 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance299 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance300 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance301 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance302 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance303 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance304 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance305 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance306 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance307 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance308 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance309 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance310 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance311 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance312 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance313 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance314 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance315 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance316 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance317 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance318 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance319 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance320 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance321 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance322 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance323 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance324 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance325 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance326 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance327 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance328 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance329 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance330 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance331 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance332 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance333 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance334 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance335 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance336 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance337 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance338 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance339 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance340 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance341 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance342 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance343 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance344 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance345 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance346 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance347 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance348 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance349 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance350 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance351 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance352 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance353 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance354 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance355 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance356 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance357 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance358 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance359 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance360 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance361 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance362 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance363 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance364 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance365 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance366 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance367 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance368 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance369 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance370 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance371 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance372 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance373 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance374 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance375 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance376 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance377 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance378 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance379 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance380 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance381 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance382 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance383 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance384 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance385 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance386 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance387 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance388 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance389 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance390 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance391 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance392 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance393 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance394 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance395 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance396 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance397 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance398 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance399 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance400 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance401 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance402 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance403 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance404 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance405 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance406 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance407 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance408 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance409 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance410 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance411 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance412 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance413 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance414 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance415 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance416 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance417 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance418 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance419 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance420 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance421 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance422 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance423 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance424 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance425 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance426 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance427 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance428 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance429 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance430 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance431 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance432 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance433 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance434 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance435 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance436 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance437 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance438 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance439 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance440 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance441 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance442 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance443 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance444 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance445 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance446 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance447 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance448 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance449 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance450 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance451 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance452 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance453 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance454 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance455 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance456 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance457 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance458 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance459 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance460 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance461 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance462 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance463 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance464 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance465 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance466 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance467 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance468 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance469 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance470 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance471 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance472 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance473 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance474 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance475 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance476 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance477 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance478 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance479 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance480 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance481 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance482 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance483 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance484 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance485 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance486 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance487 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance488 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance489 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance490 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance491 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance492 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance493 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance494 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance495 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance496 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance497 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance498 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance499 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance500 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance501 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance502 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance503 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance504 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance505 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance506 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance507 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance508 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance509 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance510 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance511 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance512 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance513 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance514 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance515 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance516 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance517 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance518 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance519 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance520 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance521 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance522 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance523 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance524 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance525 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance526 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance527 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance528 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance529 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance530 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance531 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance532 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance533 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance534 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance535 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance536 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance537 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance538 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance539 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance540 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ProductDetailsForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance541 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabelCompanyDivision = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCompanyDivision = new Micromind.DataControls.CompanyDivisionComboBox();
			checkBoxUPCPriceEmbedded = new System.Windows.Forms.CheckBox();
			buttonGenerate = new Micromind.UISupport.XPButton();
			buttonTranslate = new Micromind.UISupport.XPButton();
			labelDescription3 = new Micromind.UISupport.MMLabel();
			textDescription3 = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxAlias = new Micromind.UISupport.MMTextBox();
			checkBoxHoldSale = new System.Windows.Forms.CheckBox();
			textBox3PLRent = new Micromind.UISupport.UnitPriceTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxRackBin = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxWeight = new Micromind.UISupport.NumberTextBox();
			comboBoxOrigin = new Micromind.DataControls.CountryComboBox();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxAttribute = new Micromind.UISupport.MMTextBox();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			textBoxSize = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxStyle = new Micromind.DataControls.ProductStyleComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxBrand = new Micromind.DataControls.ProductBrandComboBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxManufacturer = new Micromind.DataControls.ProductManufacturerComboBox();
			checkBoxTrackSerial = new System.Windows.Forms.CheckBox();
			checkBoxTrackLot = new System.Windows.Forms.CheckBox();
			comboBoxBOM = new Micromind.DataControls.BOMComboBox();
			linkLabelBOM = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel12 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxQuantityOnhand = new Micromind.UISupport.QuantityTextBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			comboBoxUOM = new Micromind.DataControls.UnitComboBox();
			textBoxReorderLevel = new Micromind.UISupport.QuantityTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			textBoxQuantityPerUnit = new Micromind.UISupport.QuantityTextBox();
			comboBoxCategory = new Micromind.DataControls.ProductCategoryComboBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxStandardCost = new Micromind.UISupport.UnitPriceTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxLastCost = new Micromind.UISupport.UnitPriceTextBox();
			textBoxAvgCost = new Micromind.UISupport.UnitPriceTextBox();
			labelMinPrice = new Micromind.UISupport.MMLabel();
			labelSpecialPrice = new Micromind.UISupport.MMLabel();
			labelWholesalePrice = new Micromind.UISupport.MMLabel();
			labelStandardPrice = new Micromind.UISupport.MMLabel();
			textBoxMinimumPrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxSpecialPrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxWholesalePrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxStandardPrice = new Micromind.UISupport.UnitPriceTextBox();
			comboBoxClass = new Micromind.DataControls.ItemClassComboBox();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxCostMethod = new Micromind.DataControls.ItemCostingComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			comboBoxItemType = new Micromind.DataControls.ItemTypesComboBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxVendorRef = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxUPC = new Micromind.UISupport.MMTextBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			groupBox4 = new System.Windows.Forms.GroupBox();
			comboBoxgridVehicleType = new Micromind.DataControls.GenericListComboBox();
			comboBoxGridVehicleMake = new Micromind.DataControls.GenericListComboBox();
			datagridAppliedModels = new Micromind.DataControls.DataEntryGrid();
			groupBox3 = new System.Windows.Forms.GroupBox();
			comboBoxSubstiProduct = new Micromind.DataControls.ProductComboBox();
			dataGridSubstituteItems = new Micromind.DataControls.DataEntryGrid();
			groupBox2 = new System.Windows.Forms.GroupBox();
			ultraFormattedLinkLabel13 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel16 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel26 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel14 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxOEMCode = new Micromind.UISupport.MMTextBox();
			textBoxEngineNo = new Micromind.UISupport.MMTextBox();
			textBoxModelNo = new Micromind.UISupport.MMTextBox();
			textBoxChasisNo = new Micromind.UISupport.MMTextBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			comboBoxPartsFamily = new Micromind.DataControls.GenericListComboBox();
			comboboxPartsMakeType = new Micromind.DataControls.GenericListComboBox();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			comboBoxPartsType = new Micromind.DataControls.GenericListComboBox();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			textBoxSpecification = new Micromind.UISupport.MMTextBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			comboBoxVehicleMake = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel15 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxVehicleModel = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ComboBoxVehicleType = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox7 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel22 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			comboBoxTaxOption = new System.Windows.Forms.ComboBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraExpandableGroupBoxType = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			ultraExpandableGroupBoxPanel1 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			genericListComboBoxType8 = new Micromind.DataControls.GenericListComboBox();
			linkLabelType8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			genericListComboBoxType7 = new Micromind.DataControls.GenericListComboBox();
			linkLabelType7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			genericListComboBoxType6 = new Micromind.DataControls.GenericListComboBox();
			linkLabelType6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			genericListComboBoxType5 = new Micromind.DataControls.GenericListComboBox();
			linkLabelType5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelType4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			genericListComboBoxType4 = new Micromind.DataControls.GenericListComboBox();
			genericListComboBoxType3 = new Micromind.DataControls.GenericListComboBox();
			linkLabelType3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			genericListComboBoxType2 = new Micromind.DataControls.GenericListComboBox();
			linkLabelType2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelType1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			genericListComboBoxType1 = new Micromind.DataControls.GenericListComboBox();
			labelAttribute3 = new Micromind.UISupport.MMLabel();
			textBoxAttribute3 = new Micromind.UISupport.MMTextBox();
			labelAttribute2 = new Micromind.UISupport.MMLabel();
			textBoxAttribute2 = new Micromind.UISupport.MMTextBox();
			buttonAccounts = new Micromind.UISupport.XPButton();
			labelAttribute1 = new Micromind.UISupport.MMLabel();
			textBoxAttribute1 = new Micromind.UISupport.MMTextBox();
			comboBoxColor = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel21 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxMaterial = new Micromind.DataControls.GenericListComboBox();
			comboBoxStandard = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel17 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel20 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxFinish = new Micromind.DataControls.GenericListComboBox();
			comboBoxGrade = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel18 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel19 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxExpenseCode = new Micromind.UISupport.MMTextBox();
			labelExpenseCode = new Micromind.UISupport.MMLabel();
			comboBoxExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
			checkBoxExcludeFromCatalogue = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPrefVendor = new Micromind.DataControls.vendorsFlatComboBox();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			textBoxWarranty = new Micromind.UISupport.NumberTextBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel11 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxMainUnitTab = new Micromind.DataControls.UnitComboBox();
			comboBoxGridUnit = new Micromind.DataControls.UnitComboBox();
			dataGridUnits = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			unitComboBox1 = new Micromind.DataControls.UnitComboBox();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			dataEntryGridPriceList = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxSubstring = new Micromind.UISupport.NumberTextBox();
			groupBox6 = new System.Windows.Forms.GroupBox();
			listBoxSelectedFields = new System.Windows.Forms.ListBox();
			groupBox5 = new System.Windows.Forms.GroupBox();
			checkedListBoxFields = new System.Windows.Forms.CheckedListBox();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDuplicate = new System.Windows.Forms.ToolStripButton();
			toolStripButtonShowPicture = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panel1 = new System.Windows.Forms.Panel();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			formManager = new Micromind.DataControls.FormManager();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOrigin).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBrand).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManufacturer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBOM).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUOM).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxgridVehicleType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVehicleMake).BeginInit();
			((System.ComponentModel.ISupportInitialize)datagridAppliedModels).BeginInit();
			groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSubstiProduct).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridSubstituteItems).BeginInit();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPartsFamily).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboboxPartsMakeType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPartsType).BeginInit();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicleMake).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicleModel).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxVehicleType).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).BeginInit();
			ultraGroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraExpandableGroupBoxType).BeginInit();
			ultraExpandableGroupBoxType.SuspendLayout();
			ultraExpandableGroupBoxPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType8).BeginInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType7).BeginInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType6).BeginInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType5).BeginInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType4).BeginInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType3).BeginInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType2).BeginInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxColor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxMaterial).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStandard).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFinish).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGrade).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxExpenseCode).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPrefVendor).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxMainUnitTab).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridUnits).BeginInit();
			ultraTabPageControl4.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)unitComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridPriceList).BeginInit();
			ultraTabPageControl5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			groupBox6.SuspendLayout();
			groupBox5.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabelCompanyDivision);
			tabPageGeneral.Controls.Add(comboBoxCompanyDivision);
			tabPageGeneral.Controls.Add(checkBoxUPCPriceEmbedded);
			tabPageGeneral.Controls.Add(buttonGenerate);
			tabPageGeneral.Controls.Add(buttonTranslate);
			tabPageGeneral.Controls.Add(labelDescription3);
			tabPageGeneral.Controls.Add(textDescription3);
			tabPageGeneral.Controls.Add(mmLabel13);
			tabPageGeneral.Controls.Add(textBoxAlias);
			tabPageGeneral.Controls.Add(checkBoxHoldSale);
			tabPageGeneral.Controls.Add(textBox3PLRent);
			tabPageGeneral.Controls.Add(mmLabel9);
			tabPageGeneral.Controls.Add(textBoxRackBin);
			tabPageGeneral.Controls.Add(mmLabel2);
			tabPageGeneral.Controls.Add(mmLabel14);
			tabPageGeneral.Controls.Add(textBoxWeight);
			tabPageGeneral.Controls.Add(comboBoxOrigin);
			tabPageGeneral.Controls.Add(mmLabel20);
			tabPageGeneral.Controls.Add(textBoxAttribute);
			tabPageGeneral.Controls.Add(mmLabel19);
			tabPageGeneral.Controls.Add(textBoxSize);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel5);
			tabPageGeneral.Controls.Add(comboBoxStyle);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel6);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel4);
			tabPageGeneral.Controls.Add(comboBoxBrand);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel3);
			tabPageGeneral.Controls.Add(comboBoxManufacturer);
			tabPageGeneral.Controls.Add(checkBoxTrackSerial);
			tabPageGeneral.Controls.Add(checkBoxTrackLot);
			tabPageGeneral.Controls.Add(comboBoxBOM);
			tabPageGeneral.Controls.Add(linkLabelBOM);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel12);
			tabPageGeneral.Controls.Add(linkRemovePicture);
			tabPageGeneral.Controls.Add(linkAddPicture);
			tabPageGeneral.Controls.Add(linkLoadImage);
			tabPageGeneral.Controls.Add(pictureBoxPhoto);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel10);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel9);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel8);
			tabPageGeneral.Controls.Add(textBoxQuantityOnhand);
			tabPageGeneral.Controls.Add(mmLabel22);
			tabPageGeneral.Controls.Add(comboBoxUOM);
			tabPageGeneral.Controls.Add(textBoxReorderLevel);
			tabPageGeneral.Controls.Add(mmLabel15);
			tabPageGeneral.Controls.Add(textBoxQuantityPerUnit);
			tabPageGeneral.Controls.Add(comboBoxCategory);
			tabPageGeneral.Controls.Add(mmLabel12);
			tabPageGeneral.Controls.Add(textBoxStandardCost);
			tabPageGeneral.Controls.Add(mmLabel10);
			tabPageGeneral.Controls.Add(mmLabel11);
			tabPageGeneral.Controls.Add(textBoxLastCost);
			tabPageGeneral.Controls.Add(textBoxAvgCost);
			tabPageGeneral.Controls.Add(labelMinPrice);
			tabPageGeneral.Controls.Add(labelSpecialPrice);
			tabPageGeneral.Controls.Add(labelWholesalePrice);
			tabPageGeneral.Controls.Add(labelStandardPrice);
			tabPageGeneral.Controls.Add(textBoxMinimumPrice);
			tabPageGeneral.Controls.Add(textBoxSpecialPrice);
			tabPageGeneral.Controls.Add(textBoxWholesalePrice);
			tabPageGeneral.Controls.Add(textBoxStandardPrice);
			tabPageGeneral.Controls.Add(comboBoxClass);
			tabPageGeneral.Controls.Add(checkBoxInactive);
			tabPageGeneral.Controls.Add(mmLabel8);
			tabPageGeneral.Controls.Add(labelCode);
			tabPageGeneral.Controls.Add(comboBoxCostMethod);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(mmLabel7);
			tabPageGeneral.Controls.Add(comboBoxItemType);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(textBoxVendorRef);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(mmLabel3);
			tabPageGeneral.Controls.Add(textBoxUPC);
			tabPageGeneral.Controls.Add(pictureBoxNoImage);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(665, 474);
			ultraFormattedLinkLabelCompanyDivision.AutoSize = true;
			ultraFormattedLinkLabelCompanyDivision.Location = new System.Drawing.Point(10, 289);
			ultraFormattedLinkLabelCompanyDivision.Name = "ultraFormattedLinkLabelCompanyDivision";
			ultraFormattedLinkLabelCompanyDivision.Size = new System.Drawing.Size(79, 14);
			ultraFormattedLinkLabelCompanyDivision.TabIndex = 131;
			ultraFormattedLinkLabelCompanyDivision.TabStop = true;
			toolTip1.SetToolTip(ultraFormattedLinkLabelCompanyDivision, "Company Division");
			ultraFormattedLinkLabelCompanyDivision.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelCompanyDivision.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelCompanyDivision.Value = "Comp.Division:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelCompanyDivision.VisitedLinkAppearance = appearance;
			ultraFormattedLinkLabelCompanyDivision.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelCompanyDivision_LinkClicked);
			comboBoxCompanyDivision.Assigned = false;
			comboBoxCompanyDivision.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCompanyDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCompanyDivision.CustomReportFieldName = "";
			comboBoxCompanyDivision.CustomReportKey = "";
			comboBoxCompanyDivision.CustomReportValueType = 1;
			comboBoxCompanyDivision.DescriptionTextBox = null;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCompanyDivision.DisplayLayout.Appearance = appearance2;
			comboBoxCompanyDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCompanyDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			comboBoxCompanyDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCompanyDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCompanyDivision.DisplayLayout.Override.CellAppearance = appearance9;
			comboBoxCompanyDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCompanyDivision.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderAppearance = appearance11;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			comboBoxCompanyDivision.DisplayLayout.Override.RowAppearance = appearance12;
			comboBoxCompanyDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCompanyDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			comboBoxCompanyDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCompanyDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCompanyDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCompanyDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCompanyDivision.Editable = true;
			comboBoxCompanyDivision.FilterString = "";
			comboBoxCompanyDivision.HasAllAccount = false;
			comboBoxCompanyDivision.HasCustom = false;
			comboBoxCompanyDivision.IsDataLoaded = false;
			comboBoxCompanyDivision.Location = new System.Drawing.Point(110, 284);
			comboBoxCompanyDivision.MaxDropDownItems = 12;
			comboBoxCompanyDivision.Name = "comboBoxCompanyDivision";
			comboBoxCompanyDivision.ShowInactiveItems = false;
			comboBoxCompanyDivision.ShowQuickAdd = true;
			comboBoxCompanyDivision.Size = new System.Drawing.Size(151, 20);
			comboBoxCompanyDivision.TabIndex = 19;
			comboBoxCompanyDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			checkBoxUPCPriceEmbedded.AutoSize = true;
			checkBoxUPCPriceEmbedded.Location = new System.Drawing.Point(496, 321);
			checkBoxUPCPriceEmbedded.Name = "checkBoxUPCPriceEmbedded";
			checkBoxUPCPriceEmbedded.Size = new System.Drawing.Size(104, 17);
			checkBoxUPCPriceEmbedded.TabIndex = 90;
			checkBoxUPCPriceEmbedded.Text = "Price Embedded";
			checkBoxUPCPriceEmbedded.UseVisualStyleBackColor = true;
			buttonGenerate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonGenerate.BackColor = System.Drawing.Color.DarkGray;
			buttonGenerate.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonGenerate.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonGenerate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonGenerate.Enabled = false;
			buttonGenerate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonGenerate.Location = new System.Drawing.Point(298, 9);
			buttonGenerate.Name = "buttonGenerate";
			buttonGenerate.Size = new System.Drawing.Size(20, 20);
			buttonGenerate.TabIndex = 89;
			buttonGenerate.Text = "G";
			buttonGenerate.UseVisualStyleBackColor = false;
			buttonGenerate.Click += new System.EventHandler(buttonGenerate_Click);
			buttonTranslate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonTranslate.BackColor = System.Drawing.Color.DarkGray;
			buttonTranslate.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonTranslate.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonTranslate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonTranslate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonTranslate.Location = new System.Drawing.Point(443, 103);
			buttonTranslate.Name = "buttonTranslate";
			buttonTranslate.Size = new System.Drawing.Size(40, 23);
			buttonTranslate.TabIndex = 5;
			buttonTranslate.Text = "Arabic";
			buttonTranslate.UseVisualStyleBackColor = false;
			buttonTranslate.Click += new System.EventHandler(buttonTranslate_Click);
			labelDescription3.AutoSize = true;
			labelDescription3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelDescription3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelDescription3.IsFieldHeader = false;
			labelDescription3.IsRequired = false;
			labelDescription3.Location = new System.Drawing.Point(10, 108);
			labelDescription3.Name = "labelDescription3";
			labelDescription3.PenWidth = 1f;
			labelDescription3.ShowBorder = false;
			labelDescription3.Size = new System.Drawing.Size(69, 13);
			labelDescription3.TabIndex = 88;
			labelDescription3.Text = "Description 3";
			textDescription3.BackColor = System.Drawing.Color.White;
			textDescription3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textDescription3.CustomReportFieldName = "";
			textDescription3.CustomReportKey = "";
			textDescription3.CustomReportValueType = 1;
			textDescription3.IsComboTextBox = false;
			textDescription3.IsModified = false;
			textDescription3.Location = new System.Drawing.Point(110, 104);
			textDescription3.MaxLength = 255;
			textDescription3.Name = "textDescription3";
			textDescription3.Size = new System.Drawing.Size(330, 20);
			textDescription3.TabIndex = 5;
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(10, 85);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(32, 13);
			mmLabel13.TabIndex = 86;
			mmLabel13.Text = "Alias:";
			textBoxAlias.BackColor = System.Drawing.Color.White;
			textBoxAlias.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAlias.CustomReportFieldName = "";
			textBoxAlias.CustomReportKey = "";
			textBoxAlias.CustomReportValueType = 1;
			textBoxAlias.IsComboTextBox = false;
			textBoxAlias.IsModified = false;
			textBoxAlias.Location = new System.Drawing.Point(110, 82);
			textBoxAlias.MaxLength = 64;
			textBoxAlias.Name = "textBoxAlias";
			textBoxAlias.Size = new System.Drawing.Size(374, 20);
			textBoxAlias.TabIndex = 4;
			checkBoxHoldSale.AutoSize = true;
			checkBoxHoldSale.Location = new System.Drawing.Point(410, 11);
			checkBoxHoldSale.Name = "checkBoxHoldSale";
			checkBoxHoldSale.Size = new System.Drawing.Size(72, 17);
			checkBoxHoldSale.TabIndex = 2;
			checkBoxHoldSale.Text = "Hold Sale";
			checkBoxHoldSale.UseVisualStyleBackColor = true;
			textBox3PLRent.CustomReportFieldName = "";
			textBox3PLRent.CustomReportKey = "";
			textBox3PLRent.CustomReportValueType = 1;
			textBox3PLRent.IsComboTextBox = false;
			textBox3PLRent.IsModified = false;
			textBox3PLRent.Location = new System.Drawing.Point(501, 453);
			textBox3PLRent.MaxLength = 10;
			textBox3PLRent.Name = "textBox3PLRent";
			textBox3PLRent.Size = new System.Drawing.Size(120, 20);
			textBox3PLRent.TabIndex = 30;
			textBox3PLRent.Text = "0.00";
			textBox3PLRent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(412, 457);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(55, 13);
			mmLabel9.TabIndex = 84;
			mmLabel9.Text = "3PL Rent:";
			textBoxRackBin.BackColor = System.Drawing.Color.White;
			textBoxRackBin.CustomReportFieldName = "";
			textBoxRackBin.CustomReportKey = "";
			textBoxRackBin.CustomReportValueType = 1;
			textBoxRackBin.IsComboTextBox = false;
			textBoxRackBin.IsModified = false;
			textBoxRackBin.Location = new System.Drawing.Point(110, 434);
			textBoxRackBin.MaxLength = 30;
			textBoxRackBin.Name = "textBoxRackBin";
			textBoxRackBin.Size = new System.Drawing.Size(106, 20);
			textBoxRackBin.TabIndex = 34;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(10, 437);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(63, 13);
			mmLabel2.TabIndex = 82;
			mmLabel2.Text = "Rack - Bin :";
			mmLabel14.AutoSize = true;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(490, 265);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(44, 13);
			mmLabel14.TabIndex = 81;
			mmLabel14.Text = "Weight:";
			textBoxWeight.AllowDecimal = true;
			textBoxWeight.CustomReportFieldName = "";
			textBoxWeight.CustomReportKey = "";
			textBoxWeight.CustomReportValueType = 1;
			textBoxWeight.IsComboTextBox = false;
			textBoxWeight.IsModified = false;
			textBoxWeight.Location = new System.Drawing.Point(540, 261);
			textBoxWeight.MaxLength = 7;
			textBoxWeight.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxWeight.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxWeight.Name = "textBoxWeight";
			textBoxWeight.NullText = "0";
			textBoxWeight.Size = new System.Drawing.Size(65, 20);
			textBoxWeight.TabIndex = 18;
			textBoxWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxOrigin.Assigned = false;
			comboBoxOrigin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxOrigin.CustomReportFieldName = "";
			comboBoxOrigin.CustomReportKey = "";
			comboBoxOrigin.CustomReportValueType = 1;
			comboBoxOrigin.DescriptionTextBox = null;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOrigin.DisplayLayout.Appearance = appearance14;
			comboBoxOrigin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOrigin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance15.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance15.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance15.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOrigin.DisplayLayout.GroupByBox.Appearance = appearance15;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOrigin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance16;
			comboBoxOrigin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance17.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance17.BackColor2 = System.Drawing.SystemColors.Control;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOrigin.DisplayLayout.GroupByBox.PromptAppearance = appearance17;
			comboBoxOrigin.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOrigin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOrigin.DisplayLayout.Override.ActiveCellAppearance = appearance18;
			appearance19.BackColor = System.Drawing.SystemColors.Highlight;
			appearance19.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOrigin.DisplayLayout.Override.ActiveRowAppearance = appearance19;
			comboBoxOrigin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOrigin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOrigin.DisplayLayout.Override.CardAreaAppearance = appearance20;
			appearance21.BorderColor = System.Drawing.Color.Silver;
			appearance21.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOrigin.DisplayLayout.Override.CellAppearance = appearance21;
			comboBoxOrigin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOrigin.DisplayLayout.Override.CellPadding = 0;
			appearance22.BackColor = System.Drawing.SystemColors.Control;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOrigin.DisplayLayout.Override.GroupByRowAppearance = appearance22;
			appearance23.TextHAlignAsString = "Left";
			comboBoxOrigin.DisplayLayout.Override.HeaderAppearance = appearance23;
			comboBoxOrigin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOrigin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			comboBoxOrigin.DisplayLayout.Override.RowAppearance = appearance24;
			comboBoxOrigin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance25.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOrigin.DisplayLayout.Override.TemplateAddRowAppearance = appearance25;
			comboBoxOrigin.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxOrigin.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxOrigin.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxOrigin.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxOrigin.Editable = true;
			comboBoxOrigin.FilterString = "";
			comboBoxOrigin.HasAllAccount = false;
			comboBoxOrigin.HasCustom = false;
			comboBoxOrigin.IsDataLoaded = false;
			comboBoxOrigin.Location = new System.Drawing.Point(330, 215);
			comboBoxOrigin.MaxDropDownItems = 12;
			comboBoxOrigin.Name = "comboBoxOrigin";
			comboBoxOrigin.ShowInactiveItems = false;
			comboBoxOrigin.ShowQuickAdd = true;
			comboBoxOrigin.Size = new System.Drawing.Size(154, 20);
			comboBoxOrigin.TabIndex = 13;
			comboBoxOrigin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxOrigin.SelectedIndexChanged += new System.EventHandler(comboBoxOrigin_SelectedIndexChanged);
			mmLabel20.AutoSize = true;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(268, 263);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(49, 13);
			mmLabel20.TabIndex = 79;
			mmLabel20.Text = "Attribute:";
			textBoxAttribute.BackColor = System.Drawing.Color.White;
			textBoxAttribute.CustomReportFieldName = "";
			textBoxAttribute.CustomReportKey = "";
			textBoxAttribute.CustomReportValueType = 1;
			textBoxAttribute.IsComboTextBox = false;
			textBoxAttribute.IsModified = false;
			textBoxAttribute.Location = new System.Drawing.Point(330, 261);
			textBoxAttribute.MaxLength = 30;
			textBoxAttribute.Name = "textBoxAttribute";
			textBoxAttribute.Size = new System.Drawing.Size(154, 20);
			textBoxAttribute.TabIndex = 17;
			mmLabel19.AutoSize = true;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(10, 265);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(30, 13);
			mmLabel19.TabIndex = 78;
			mmLabel19.Text = "Size:";
			textBoxSize.BackColor = System.Drawing.Color.White;
			textBoxSize.CustomReportFieldName = "";
			textBoxSize.CustomReportKey = "";
			textBoxSize.CustomReportValueType = 1;
			textBoxSize.IsComboTextBox = false;
			textBoxSize.IsModified = false;
			textBoxSize.Location = new System.Drawing.Point(110, 261);
			textBoxSize.MaxLength = 30;
			textBoxSize.Name = "textBoxSize";
			textBoxSize.Size = new System.Drawing.Size(149, 20);
			textBoxSize.TabIndex = 16;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(268, 240);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(32, 14);
			ultraFormattedLinkLabel5.TabIndex = 77;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Style:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxStyle.Assigned = false;
			comboBoxStyle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStyle.CustomReportFieldName = "";
			comboBoxStyle.CustomReportKey = "";
			comboBoxStyle.CustomReportValueType = 1;
			comboBoxStyle.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStyle.DisplayLayout.Appearance = appearance27;
			comboBoxStyle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStyle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxStyle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxStyle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStyle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStyle.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStyle.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxStyle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStyle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStyle.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxStyle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStyle.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxStyle.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxStyle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStyle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxStyle.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxStyle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStyle.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxStyle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStyle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStyle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStyle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStyle.Editable = true;
			comboBoxStyle.FilterString = "";
			comboBoxStyle.HasAllAccount = false;
			comboBoxStyle.HasCustom = false;
			comboBoxStyle.IsDataLoaded = false;
			comboBoxStyle.Location = new System.Drawing.Point(330, 238);
			comboBoxStyle.MaxDropDownItems = 12;
			comboBoxStyle.Name = "comboBoxStyle";
			comboBoxStyle.ShowInactiveItems = false;
			comboBoxStyle.Size = new System.Drawing.Size(154, 20);
			comboBoxStyle.TabIndex = 15;
			comboBoxStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxStyle.SelectedIndexChanged += new System.EventHandler(comboBoxStyle_SelectedIndexChanged);
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(268, 218);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(37, 14);
			ultraFormattedLinkLabel6.TabIndex = 75;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Origin:";
			appearance39.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance39;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(10, 241);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(36, 14);
			ultraFormattedLinkLabel4.TabIndex = 76;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Brand:";
			appearance40.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance40;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxBrand.Assigned = false;
			comboBoxBrand.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBrand.CustomReportFieldName = "";
			comboBoxBrand.CustomReportKey = "";
			comboBoxBrand.CustomReportValueType = 1;
			comboBoxBrand.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBrand.DisplayLayout.Appearance = appearance41;
			comboBoxBrand.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBrand.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBrand.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxBrand.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBrand.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxBrand.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBrand.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBrand.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBrand.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxBrand.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBrand.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBrand.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxBrand.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBrand.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxBrand.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxBrand.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBrand.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxBrand.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxBrand.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBrand.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxBrand.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBrand.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBrand.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBrand.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBrand.Editable = true;
			comboBoxBrand.FilterString = "";
			comboBoxBrand.HasAllAccount = false;
			comboBoxBrand.HasCustom = false;
			comboBoxBrand.IsDataLoaded = false;
			comboBoxBrand.Location = new System.Drawing.Point(110, 238);
			comboBoxBrand.MaxDropDownItems = 12;
			comboBoxBrand.Name = "comboBoxBrand";
			comboBoxBrand.ShowInactiveItems = false;
			comboBoxBrand.Size = new System.Drawing.Size(149, 20);
			comboBoxBrand.TabIndex = 14;
			comboBoxBrand.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBrand.SelectedIndexChanged += new System.EventHandler(comboBoxBrand_SelectedIndexChanged);
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(10, 218);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(72, 14);
			ultraFormattedLinkLabel3.TabIndex = 68;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Manufacturer:";
			appearance53.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance53;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxManufacturer.Assigned = false;
			comboBoxManufacturer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxManufacturer.CustomReportFieldName = "";
			comboBoxManufacturer.CustomReportKey = "";
			comboBoxManufacturer.CustomReportValueType = 1;
			comboBoxManufacturer.DescriptionTextBox = null;
			appearance54.BackColor = System.Drawing.SystemColors.Window;
			appearance54.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxManufacturer.DisplayLayout.Appearance = appearance54;
			comboBoxManufacturer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxManufacturer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance55.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance55.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance55.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManufacturer.DisplayLayout.GroupByBox.Appearance = appearance55;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManufacturer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance56;
			comboBoxManufacturer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance57.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance57.BackColor2 = System.Drawing.SystemColors.Control;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManufacturer.DisplayLayout.GroupByBox.PromptAppearance = appearance57;
			comboBoxManufacturer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxManufacturer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxManufacturer.DisplayLayout.Override.ActiveCellAppearance = appearance58;
			appearance59.BackColor = System.Drawing.SystemColors.Highlight;
			appearance59.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxManufacturer.DisplayLayout.Override.ActiveRowAppearance = appearance59;
			comboBoxManufacturer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxManufacturer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance60.BackColor = System.Drawing.SystemColors.Window;
			comboBoxManufacturer.DisplayLayout.Override.CardAreaAppearance = appearance60;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			appearance61.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxManufacturer.DisplayLayout.Override.CellAppearance = appearance61;
			comboBoxManufacturer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxManufacturer.DisplayLayout.Override.CellPadding = 0;
			appearance62.BackColor = System.Drawing.SystemColors.Control;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManufacturer.DisplayLayout.Override.GroupByRowAppearance = appearance62;
			appearance63.TextHAlignAsString = "Left";
			comboBoxManufacturer.DisplayLayout.Override.HeaderAppearance = appearance63;
			comboBoxManufacturer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxManufacturer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			comboBoxManufacturer.DisplayLayout.Override.RowAppearance = appearance64;
			comboBoxManufacturer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance65.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxManufacturer.DisplayLayout.Override.TemplateAddRowAppearance = appearance65;
			comboBoxManufacturer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxManufacturer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxManufacturer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxManufacturer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxManufacturer.Editable = true;
			comboBoxManufacturer.FilterString = "";
			comboBoxManufacturer.HasAllAccount = false;
			comboBoxManufacturer.HasCustom = false;
			comboBoxManufacturer.IsDataLoaded = false;
			comboBoxManufacturer.Location = new System.Drawing.Point(110, 215);
			comboBoxManufacturer.MaxDropDownItems = 12;
			comboBoxManufacturer.Name = "comboBoxManufacturer";
			comboBoxManufacturer.ShowInactiveItems = false;
			comboBoxManufacturer.ShowQuickAdd = true;
			comboBoxManufacturer.Size = new System.Drawing.Size(149, 20);
			comboBoxManufacturer.TabIndex = 12;
			comboBoxManufacturer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxManufacturer.SelectedIndexChanged += new System.EventHandler(comboBoxManufacturer_SelectedIndexChanged);
			comboBoxManufacturer.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(comboBoxManufacturer_InitializeLayout);
			checkBoxTrackSerial.AutoSize = true;
			checkBoxTrackSerial.Location = new System.Drawing.Point(218, 158);
			checkBoxTrackSerial.Name = "checkBoxTrackSerial";
			checkBoxTrackSerial.Size = new System.Drawing.Size(128, 17);
			checkBoxTrackSerial.TabIndex = 9;
			checkBoxTrackSerial.Text = "Track Serial Numbers";
			checkBoxTrackSerial.UseVisualStyleBackColor = true;
			checkBoxTrackSerial.CheckedChanged += new System.EventHandler(checkBoxTrackSerial_CheckedChanged);
			checkBoxTrackLot.AutoSize = true;
			checkBoxTrackLot.Location = new System.Drawing.Point(110, 158);
			checkBoxTrackLot.Name = "checkBoxTrackLot";
			checkBoxTrackLot.Size = new System.Drawing.Size(72, 17);
			checkBoxTrackLot.TabIndex = 8;
			checkBoxTrackLot.Text = "Track Lot";
			checkBoxTrackLot.UseVisualStyleBackColor = true;
			checkBoxTrackLot.CheckedChanged += new System.EventHandler(checkBoxTrackLot_CheckedChanged);
			comboBoxBOM.Assigned = false;
			comboBoxBOM.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBOM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBOM.CustomReportFieldName = "";
			comboBoxBOM.CustomReportKey = "";
			comboBoxBOM.CustomReportValueType = 1;
			comboBoxBOM.DescriptionTextBox = null;
			appearance66.BackColor = System.Drawing.SystemColors.Window;
			appearance66.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBOM.DisplayLayout.Appearance = appearance66;
			comboBoxBOM.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBOM.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance67.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBOM.DisplayLayout.GroupByBox.Appearance = appearance67;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBOM.DisplayLayout.GroupByBox.BandLabelAppearance = appearance68;
			comboBoxBOM.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance69.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance69.BackColor2 = System.Drawing.SystemColors.Control;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBOM.DisplayLayout.GroupByBox.PromptAppearance = appearance69;
			comboBoxBOM.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBOM.DisplayLayout.MaxRowScrollRegions = 1;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			appearance70.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBOM.DisplayLayout.Override.ActiveCellAppearance = appearance70;
			appearance71.BackColor = System.Drawing.SystemColors.Highlight;
			appearance71.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBOM.DisplayLayout.Override.ActiveRowAppearance = appearance71;
			comboBoxBOM.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBOM.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance72.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBOM.DisplayLayout.Override.CardAreaAppearance = appearance72;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			appearance73.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBOM.DisplayLayout.Override.CellAppearance = appearance73;
			comboBoxBOM.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBOM.DisplayLayout.Override.CellPadding = 0;
			appearance74.BackColor = System.Drawing.SystemColors.Control;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBOM.DisplayLayout.Override.GroupByRowAppearance = appearance74;
			appearance75.TextHAlignAsString = "Left";
			comboBoxBOM.DisplayLayout.Override.HeaderAppearance = appearance75;
			comboBoxBOM.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBOM.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			comboBoxBOM.DisplayLayout.Override.RowAppearance = appearance76;
			comboBoxBOM.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance77.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBOM.DisplayLayout.Override.TemplateAddRowAppearance = appearance77;
			comboBoxBOM.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBOM.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBOM.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBOM.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBOM.Editable = true;
			comboBoxBOM.Enabled = false;
			comboBoxBOM.FilterString = "";
			comboBoxBOM.HasAllAccount = false;
			comboBoxBOM.HasCustom = false;
			comboBoxBOM.IsDataLoaded = false;
			comboBoxBOM.Location = new System.Drawing.Point(330, 340);
			comboBoxBOM.MaxDropDownItems = 12;
			comboBoxBOM.Name = "comboBoxBOM";
			comboBoxBOM.ShowInactiveItems = false;
			comboBoxBOM.ShowQuickAdd = true;
			comboBoxBOM.Size = new System.Drawing.Size(154, 20);
			comboBoxBOM.TabIndex = 23;
			comboBoxBOM.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelBOM.AutoSize = true;
			linkLabelBOM.Enabled = false;
			linkLabelBOM.Location = new System.Drawing.Point(269, 343);
			linkLabelBOM.Name = "linkLabelBOM";
			linkLabelBOM.Size = new System.Drawing.Size(32, 14);
			linkLabelBOM.TabIndex = 66;
			linkLabelBOM.TabStop = true;
			linkLabelBOM.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelBOM.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelBOM.Value = "BOM:";
			appearance78.ForeColor = System.Drawing.Color.Blue;
			linkLabelBOM.VisitedLinkAppearance = appearance78;
			linkLabelBOM.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelBOM_LinkClicked);
			ultraFormattedLinkLabel12.AutoSize = true;
			ultraFormattedLinkLabel12.Location = new System.Drawing.Point(10, 412);
			ultraFormattedLinkLabel12.Name = "ultraFormattedLinkLabel12";
			ultraFormattedLinkLabel12.Size = new System.Drawing.Size(91, 14);
			ultraFormattedLinkLabel12.TabIndex = 65;
			ultraFormattedLinkLabel12.TabStop = true;
			ultraFormattedLinkLabel12.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel12.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel12.Value = "Quantity Onhand:";
			appearance79.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel12.VisitedLinkAppearance = appearance79;
			ultraFormattedLinkLabel12.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel12_LinkClicked);
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(535, 141);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 63;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance80.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance80;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(496, 141);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 62;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance81.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance81;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(525, 57);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 61;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance82.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance82;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(496, 9);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(128, 128);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 60;
			pictureBoxPhoto.TabStop = false;
			ultraFormattedLinkLabel10.AutoSize = true;
			ultraFormattedLinkLabel10.Location = new System.Drawing.Point(10, 195);
			ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
			ultraFormattedLinkLabel10.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel10.TabIndex = 55;
			ultraFormattedLinkLabel10.TabStop = true;
			ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel10.Value = "Item Class:";
			appearance83.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance83;
			ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel10_LinkClicked);
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(268, 195);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(52, 14);
			ultraFormattedLinkLabel9.TabIndex = 54;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "Category:";
			appearance84.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance84;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(10, 342);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(60, 14);
			ultraFormattedLinkLabel8.TabIndex = 53;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Main UOM:";
			appearance85.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance85;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			textBoxQuantityOnhand.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxQuantityOnhand.CustomReportFieldName = "";
			textBoxQuantityOnhand.CustomReportKey = "";
			textBoxQuantityOnhand.CustomReportValueType = 1;
			textBoxQuantityOnhand.ForeColor = System.Drawing.SystemColors.WindowText;
			textBoxQuantityOnhand.IsComboTextBox = false;
			textBoxQuantityOnhand.IsModified = false;
			textBoxQuantityOnhand.Location = new System.Drawing.Point(110, 409);
			textBoxQuantityOnhand.Name = "textBoxQuantityOnhand";
			textBoxQuantityOnhand.ReadOnly = true;
			textBoxQuantityOnhand.Size = new System.Drawing.Size(106, 20);
			textBoxQuantityOnhand.TabIndex = 32;
			textBoxQuantityOnhand.TabStop = false;
			textBoxQuantityOnhand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel22.AutoSize = true;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(10, 320);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(64, 13);
			mmLabel22.TabIndex = 48;
			mmLabel22.Text = "Vendor Ref:";
			comboBoxUOM.Assigned = false;
			comboBoxUOM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUOM.CustomReportFieldName = "";
			comboBoxUOM.CustomReportKey = "";
			comboBoxUOM.CustomReportValueType = 1;
			comboBoxUOM.DescriptionTextBox = null;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUOM.DisplayLayout.Appearance = appearance86;
			comboBoxUOM.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUOM.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance87.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUOM.DisplayLayout.GroupByBox.Appearance = appearance87;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUOM.DisplayLayout.GroupByBox.BandLabelAppearance = appearance88;
			comboBoxUOM.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance89.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance89.BackColor2 = System.Drawing.SystemColors.Control;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUOM.DisplayLayout.GroupByBox.PromptAppearance = appearance89;
			comboBoxUOM.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUOM.DisplayLayout.MaxRowScrollRegions = 1;
			appearance90.BackColor = System.Drawing.SystemColors.Window;
			appearance90.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUOM.DisplayLayout.Override.ActiveCellAppearance = appearance90;
			appearance91.BackColor = System.Drawing.SystemColors.Highlight;
			appearance91.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUOM.DisplayLayout.Override.ActiveRowAppearance = appearance91;
			comboBoxUOM.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUOM.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUOM.DisplayLayout.Override.CardAreaAppearance = appearance92;
			appearance93.BorderColor = System.Drawing.Color.Silver;
			appearance93.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUOM.DisplayLayout.Override.CellAppearance = appearance93;
			comboBoxUOM.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUOM.DisplayLayout.Override.CellPadding = 0;
			appearance94.BackColor = System.Drawing.SystemColors.Control;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUOM.DisplayLayout.Override.GroupByRowAppearance = appearance94;
			appearance95.TextHAlignAsString = "Left";
			comboBoxUOM.DisplayLayout.Override.HeaderAppearance = appearance95;
			comboBoxUOM.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUOM.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance96.BackColor = System.Drawing.SystemColors.Window;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			comboBoxUOM.DisplayLayout.Override.RowAppearance = appearance96;
			comboBoxUOM.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance97.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUOM.DisplayLayout.Override.TemplateAddRowAppearance = appearance97;
			comboBoxUOM.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUOM.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUOM.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUOM.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUOM.Editable = true;
			comboBoxUOM.FilterString = "";
			comboBoxUOM.HasAllAccount = false;
			comboBoxUOM.HasCustom = false;
			comboBoxUOM.IsDataLoaded = false;
			comboBoxUOM.Location = new System.Drawing.Point(110, 336);
			comboBoxUOM.MaxDropDownItems = 12;
			comboBoxUOM.Name = "comboBoxUOM";
			comboBoxUOM.ShowInactiveItems = false;
			comboBoxUOM.ShowQuickAdd = true;
			comboBoxUOM.Size = new System.Drawing.Size(149, 20);
			comboBoxUOM.TabIndex = 22;
			comboBoxUOM.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxUOM.SelectedIndexChanged += new System.EventHandler(comboBoxUOM_SelectedIndexChanged);
			textBoxReorderLevel.CustomReportFieldName = "";
			textBoxReorderLevel.CustomReportKey = "";
			textBoxReorderLevel.CustomReportValueType = 1;
			textBoxReorderLevel.IsComboTextBox = false;
			textBoxReorderLevel.IsModified = false;
			textBoxReorderLevel.Location = new System.Drawing.Point(110, 387);
			textBoxReorderLevel.MaxLength = 10;
			textBoxReorderLevel.Name = "textBoxReorderLevel";
			textBoxReorderLevel.Size = new System.Drawing.Size(106, 20);
			textBoxReorderLevel.TabIndex = 31;
			textBoxReorderLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel15.AutoSize = true;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(10, 390);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(77, 13);
			mmLabel15.TabIndex = 41;
			mmLabel15.Text = "Reorder Level:";
			textBoxQuantityPerUnit.CustomReportFieldName = "";
			textBoxQuantityPerUnit.CustomReportKey = "";
			textBoxQuantityPerUnit.CustomReportValueType = 1;
			textBoxQuantityPerUnit.IsComboTextBox = false;
			textBoxQuantityPerUnit.IsModified = false;
			textBoxQuantityPerUnit.Location = new System.Drawing.Point(110, 365);
			textBoxQuantityPerUnit.MaxLength = 10;
			textBoxQuantityPerUnit.Name = "textBoxQuantityPerUnit";
			textBoxQuantityPerUnit.Size = new System.Drawing.Size(106, 20);
			textBoxQuantityPerUnit.TabIndex = 24;
			textBoxQuantityPerUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxCategory.Assigned = false;
			comboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCategory.CustomReportFieldName = "";
			comboBoxCategory.CustomReportKey = "";
			comboBoxCategory.CustomReportValueType = 1;
			comboBoxCategory.DescriptionTextBox = null;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCategory.DisplayLayout.Appearance = appearance98;
			comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance99.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance99;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance100;
			comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance101.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance101.BackColor2 = System.Drawing.SystemColors.Control;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance101;
			comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance102.BackColor = System.Drawing.SystemColors.Window;
			appearance102.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance102;
			appearance103.BackColor = System.Drawing.SystemColors.Highlight;
			appearance103.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance103;
			comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance104;
			appearance105.BorderColor = System.Drawing.Color.Silver;
			appearance105.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance105;
			comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
			appearance106.BackColor = System.Drawing.SystemColors.Control;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance106;
			appearance107.TextHAlignAsString = "Left";
			comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance107;
			comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance108.BackColor = System.Drawing.SystemColors.Window;
			appearance108.BorderColor = System.Drawing.Color.Silver;
			comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance108;
			comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance109.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance109;
			comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCategory.Editable = true;
			comboBoxCategory.FilterString = "";
			comboBoxCategory.HasAllAccount = false;
			comboBoxCategory.HasCustom = false;
			comboBoxCategory.IsDataLoaded = false;
			comboBoxCategory.Location = new System.Drawing.Point(330, 193);
			comboBoxCategory.MaxDropDownItems = 12;
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.ShowInactiveItems = false;
			comboBoxCategory.Size = new System.Drawing.Size(154, 20);
			comboBoxCategory.TabIndex = 11;
			comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCategory.SelectedIndexChanged += new System.EventHandler(comboBoxCategory_SelectedIndexChanged);
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(225, 412);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(77, 13);
			mmLabel12.TabIndex = 35;
			mmLabel12.Text = "Standard Cost:";
			textBoxStandardCost.CustomReportFieldName = "";
			textBoxStandardCost.CustomReportKey = "";
			textBoxStandardCost.CustomReportValueType = 1;
			textBoxStandardCost.IsComboTextBox = false;
			textBoxStandardCost.IsModified = false;
			textBoxStandardCost.Location = new System.Drawing.Point(308, 409);
			textBoxStandardCost.MaxLength = 10;
			textBoxStandardCost.Name = "textBoxStandardCost";
			textBoxStandardCost.Size = new System.Drawing.Size(97, 20);
			textBoxStandardCost.TabIndex = 33;
			textBoxStandardCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(225, 390);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(54, 13);
			mmLabel10.TabIndex = 32;
			mmLabel10.Text = "Last Cost:";
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(225, 368);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(53, 13);
			mmLabel11.TabIndex = 32;
			mmLabel11.Text = "Avg Cost:";
			textBoxLastCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLastCost.CustomReportFieldName = "";
			textBoxLastCost.CustomReportKey = "";
			textBoxLastCost.CustomReportValueType = 1;
			textBoxLastCost.ForeColor = System.Drawing.SystemColors.WindowText;
			textBoxLastCost.IsComboTextBox = false;
			textBoxLastCost.IsModified = false;
			textBoxLastCost.Location = new System.Drawing.Point(308, 387);
			textBoxLastCost.Name = "textBoxLastCost";
			textBoxLastCost.ReadOnly = true;
			textBoxLastCost.Size = new System.Drawing.Size(97, 20);
			textBoxLastCost.TabIndex = 28;
			textBoxLastCost.TabStop = false;
			textBoxLastCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAvgCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAvgCost.CustomReportFieldName = "";
			textBoxAvgCost.CustomReportKey = "";
			textBoxAvgCost.CustomReportValueType = 1;
			textBoxAvgCost.ForeColor = System.Drawing.SystemColors.WindowText;
			textBoxAvgCost.IsComboTextBox = false;
			textBoxAvgCost.IsModified = false;
			textBoxAvgCost.Location = new System.Drawing.Point(308, 365);
			textBoxAvgCost.Name = "textBoxAvgCost";
			textBoxAvgCost.ReadOnly = true;
			textBoxAvgCost.Size = new System.Drawing.Size(97, 20);
			textBoxAvgCost.TabIndex = 25;
			textBoxAvgCost.TabStop = false;
			textBoxAvgCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelMinPrice.AutoSize = true;
			labelMinPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelMinPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelMinPrice.IsFieldHeader = false;
			labelMinPrice.IsRequired = false;
			labelMinPrice.Location = new System.Drawing.Point(409, 434);
			labelMinPrice.Name = "labelMinPrice";
			labelMinPrice.PenWidth = 1f;
			labelMinPrice.ShowBorder = false;
			labelMinPrice.Size = new System.Drawing.Size(78, 13);
			labelMinPrice.TabIndex = 15;
			labelMinPrice.Text = "Minimum Price:";
			labelSpecialPrice.AutoSize = true;
			labelSpecialPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelSpecialPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelSpecialPrice.IsFieldHeader = false;
			labelSpecialPrice.IsRequired = false;
			labelSpecialPrice.Location = new System.Drawing.Point(409, 412);
			labelSpecialPrice.Name = "labelSpecialPrice";
			labelSpecialPrice.PenWidth = 1f;
			labelSpecialPrice.ShowBorder = false;
			labelSpecialPrice.Size = new System.Drawing.Size(65, 13);
			labelSpecialPrice.TabIndex = 15;
			labelSpecialPrice.Text = "Unit Price 3:";
			labelWholesalePrice.AutoSize = true;
			labelWholesalePrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelWholesalePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelWholesalePrice.IsFieldHeader = false;
			labelWholesalePrice.IsRequired = false;
			labelWholesalePrice.Location = new System.Drawing.Point(409, 390);
			labelWholesalePrice.Name = "labelWholesalePrice";
			labelWholesalePrice.PenWidth = 1f;
			labelWholesalePrice.ShowBorder = false;
			labelWholesalePrice.Size = new System.Drawing.Size(65, 13);
			labelWholesalePrice.TabIndex = 15;
			labelWholesalePrice.Text = "Unit Price 2:";
			labelStandardPrice.AutoSize = true;
			labelStandardPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelStandardPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelStandardPrice.IsFieldHeader = false;
			labelStandardPrice.IsRequired = false;
			labelStandardPrice.Location = new System.Drawing.Point(409, 368);
			labelStandardPrice.Name = "labelStandardPrice";
			labelStandardPrice.PenWidth = 1f;
			labelStandardPrice.ShowBorder = false;
			labelStandardPrice.Size = new System.Drawing.Size(65, 13);
			labelStandardPrice.TabIndex = 15;
			labelStandardPrice.Text = "Unit Price 1:";
			textBoxMinimumPrice.CustomReportFieldName = "";
			textBoxMinimumPrice.CustomReportKey = "";
			textBoxMinimumPrice.CustomReportValueType = 1;
			textBoxMinimumPrice.IsComboTextBox = false;
			textBoxMinimumPrice.IsModified = false;
			textBoxMinimumPrice.Location = new System.Drawing.Point(501, 431);
			textBoxMinimumPrice.MaxLength = 10;
			textBoxMinimumPrice.Name = "textBoxMinimumPrice";
			textBoxMinimumPrice.Size = new System.Drawing.Size(120, 20);
			textBoxMinimumPrice.TabIndex = 29;
			textBoxMinimumPrice.Text = "0.00";
			textBoxMinimumPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSpecialPrice.CustomReportFieldName = "";
			textBoxSpecialPrice.CustomReportKey = "";
			textBoxSpecialPrice.CustomReportValueType = 1;
			textBoxSpecialPrice.IsComboTextBox = false;
			textBoxSpecialPrice.IsModified = false;
			textBoxSpecialPrice.Location = new System.Drawing.Point(501, 409);
			textBoxSpecialPrice.MaxLength = 10;
			textBoxSpecialPrice.Name = "textBoxSpecialPrice";
			textBoxSpecialPrice.Size = new System.Drawing.Size(120, 20);
			textBoxSpecialPrice.TabIndex = 28;
			textBoxSpecialPrice.Text = "0.00";
			textBoxSpecialPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxWholesalePrice.CustomReportFieldName = "";
			textBoxWholesalePrice.CustomReportKey = "";
			textBoxWholesalePrice.CustomReportValueType = 1;
			textBoxWholesalePrice.IsComboTextBox = false;
			textBoxWholesalePrice.IsModified = false;
			textBoxWholesalePrice.Location = new System.Drawing.Point(501, 387);
			textBoxWholesalePrice.MaxLength = 10;
			textBoxWholesalePrice.Name = "textBoxWholesalePrice";
			textBoxWholesalePrice.Size = new System.Drawing.Size(120, 20);
			textBoxWholesalePrice.TabIndex = 27;
			textBoxWholesalePrice.Text = "0.00";
			textBoxWholesalePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxStandardPrice.CustomReportFieldName = "";
			textBoxStandardPrice.CustomReportKey = "";
			textBoxStandardPrice.CustomReportValueType = 1;
			textBoxStandardPrice.IsComboTextBox = false;
			textBoxStandardPrice.IsModified = false;
			textBoxStandardPrice.Location = new System.Drawing.Point(501, 365);
			textBoxStandardPrice.MaxLength = 10;
			textBoxStandardPrice.Name = "textBoxStandardPrice";
			textBoxStandardPrice.Size = new System.Drawing.Size(120, 20);
			textBoxStandardPrice.TabIndex = 26;
			textBoxStandardPrice.Text = "0.00";
			textBoxStandardPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxClass.Assigned = false;
			comboBoxClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxClass.CustomReportFieldName = "";
			comboBoxClass.CustomReportKey = "";
			comboBoxClass.CustomReportValueType = 1;
			comboBoxClass.DescriptionTextBox = null;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			appearance110.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxClass.DisplayLayout.Appearance = appearance110;
			comboBoxClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance111.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance111.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance111.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance111.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxClass.DisplayLayout.GroupByBox.Appearance = appearance111;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance112;
			comboBoxClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance113.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance113.BackColor2 = System.Drawing.SystemColors.Control;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxClass.DisplayLayout.GroupByBox.PromptAppearance = appearance113;
			comboBoxClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance114.BackColor = System.Drawing.SystemColors.Window;
			appearance114.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxClass.DisplayLayout.Override.ActiveCellAppearance = appearance114;
			appearance115.BackColor = System.Drawing.SystemColors.Highlight;
			appearance115.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxClass.DisplayLayout.Override.ActiveRowAppearance = appearance115;
			comboBoxClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			comboBoxClass.DisplayLayout.Override.CardAreaAppearance = appearance116;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			appearance117.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxClass.DisplayLayout.Override.CellAppearance = appearance117;
			comboBoxClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxClass.DisplayLayout.Override.CellPadding = 0;
			appearance118.BackColor = System.Drawing.SystemColors.Control;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxClass.DisplayLayout.Override.GroupByRowAppearance = appearance118;
			appearance119.TextHAlignAsString = "Left";
			comboBoxClass.DisplayLayout.Override.HeaderAppearance = appearance119;
			comboBoxClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance120.BackColor = System.Drawing.SystemColors.Window;
			appearance120.BorderColor = System.Drawing.Color.Silver;
			comboBoxClass.DisplayLayout.Override.RowAppearance = appearance120;
			comboBoxClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance121.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance121;
			comboBoxClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxClass.Editable = true;
			comboBoxClass.FilterString = "";
			comboBoxClass.HasAllAccount = false;
			comboBoxClass.HasCustom = false;
			comboBoxClass.IsDataLoaded = false;
			comboBoxClass.Location = new System.Drawing.Point(110, 192);
			comboBoxClass.MaxDropDownItems = 12;
			comboBoxClass.Name = "comboBoxClass";
			comboBoxClass.ShowInactiveItems = false;
			comboBoxClass.ShowQuickAdd = true;
			comboBoxClass.Size = new System.Drawing.Size(149, 20);
			comboBoxClass.TabIndex = 10;
			comboBoxClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxClass.SelectedIndexChanged += new System.EventHandler(comboBoxClass_SelectedIndexChanged);
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(341, 11);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(262, 136);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(84, 13);
			mmLabel8.TabIndex = 26;
			mmLabel8.Text = "Costing Method:";
			mmLabel8.Visible = false;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(10, 12);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(68, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Item Code:";
			comboBoxCostMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCostMethod.FormattingEnabled = true;
			comboBoxCostMethod.Items.AddRange(new object[2]
			{
				"Average",
				"FIFO"
			});
			comboBoxCostMethod.Location = new System.Drawing.Point(354, 133);
			comboBoxCostMethod.Name = "comboBoxCostMethod";
			comboBoxCostMethod.SelectedID = 0;
			comboBoxCostMethod.Size = new System.Drawing.Size(130, 21);
			comboBoxCostMethod.TabIndex = 7;
			comboBoxCostMethod.Visible = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(10, 32);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(75, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Description:";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(10, 136);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(57, 13);
			mmLabel7.TabIndex = 7;
			mmLabel7.Text = "Item Type:";
			comboBoxItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxItemType.FormattingEnabled = true;
			comboBoxItemType.Location = new System.Drawing.Point(110, 133);
			comboBoxItemType.Name = "comboBoxItemType";
			comboBoxItemType.SelectedID = 0;
			comboBoxItemType.SelectedType = Micromind.Common.Data.ItemTypes.Inventory;
			comboBoxItemType.Size = new System.Drawing.Size(149, 21);
			comboBoxItemType.TabIndex = 6;
			comboBoxItemType.SelectedIndexChanged += new System.EventHandler(comboBoxItemType_SelectedIndexChanged);
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(110, 9);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(187, 20);
			textBoxCode.TabIndex = 0;
			textBoxVendorRef.BackColor = System.Drawing.Color.White;
			textBoxVendorRef.CustomReportFieldName = "";
			textBoxVendorRef.CustomReportKey = "";
			textBoxVendorRef.CustomReportValueType = 1;
			textBoxVendorRef.IsComboTextBox = false;
			textBoxVendorRef.IsModified = false;
			textBoxVendorRef.Location = new System.Drawing.Point(110, 313);
			textBoxVendorRef.MaxLength = 30;
			textBoxVendorRef.Name = "textBoxVendorRef";
			textBoxVendorRef.Size = new System.Drawing.Size(149, 20);
			textBoxVendorRef.TabIndex = 20;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(110, 31);
			textBoxName.MaxLength = 255;
			textBoxName.Multiline = true;
			textBoxName.Name = "textBoxName";
			textBoxName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxName.Size = new System.Drawing.Size(374, 48);
			textBoxName.TabIndex = 3;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(10, 368);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(90, 13);
			mmLabel6.TabIndex = 19;
			mmLabel6.Text = "Quantity Per Unit:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(268, 322);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(32, 13);
			mmLabel3.TabIndex = 18;
			mmLabel3.Text = "UPC:";
			textBoxUPC.BackColor = System.Drawing.Color.White;
			textBoxUPC.CustomReportFieldName = "";
			textBoxUPC.CustomReportKey = "";
			textBoxUPC.CustomReportValueType = 1;
			textBoxUPC.IsComboTextBox = false;
			textBoxUPC.IsModified = false;
			textBoxUPC.Location = new System.Drawing.Point(330, 318);
			textBoxUPC.MaxLength = 60;
			textBoxUPC.Name = "textBoxUPC";
			textBoxUPC.Size = new System.Drawing.Size(154, 20);
			textBoxUPC.TabIndex = 21;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(575, 84);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 64;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			ultraTabPageControl2.Controls.Add(groupBox4);
			ultraTabPageControl2.Controls.Add(groupBox3);
			ultraTabPageControl2.Controls.Add(groupBox2);
			ultraTabPageControl2.Controls.Add(mmLabel16);
			ultraTabPageControl2.Controls.Add(textBoxSpecification);
			ultraTabPageControl2.Controls.Add(groupBox1);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(665, 474);
			groupBox4.Controls.Add(comboBoxgridVehicleType);
			groupBox4.Controls.Add(comboBoxGridVehicleMake);
			groupBox4.Controls.Add(datagridAppliedModels);
			groupBox4.Location = new System.Drawing.Point(16, 324);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new System.Drawing.Size(646, 150);
			groupBox4.TabIndex = 4;
			groupBox4.TabStop = false;
			groupBox4.Text = "Applied Models";
			comboBoxgridVehicleType.Assigned = false;
			comboBoxgridVehicleType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxgridVehicleType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxgridVehicleType.CustomReportFieldName = "";
			comboBoxgridVehicleType.CustomReportKey = "";
			comboBoxgridVehicleType.CustomReportValueType = 1;
			comboBoxgridVehicleType.DescriptionTextBox = null;
			appearance122.BackColor = System.Drawing.SystemColors.Window;
			appearance122.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxgridVehicleType.DisplayLayout.Appearance = appearance122;
			comboBoxgridVehicleType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxgridVehicleType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance123.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance123.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance123.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance123.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxgridVehicleType.DisplayLayout.GroupByBox.Appearance = appearance123;
			appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxgridVehicleType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance124;
			comboBoxgridVehicleType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance125.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance125.BackColor2 = System.Drawing.SystemColors.Control;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance125.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxgridVehicleType.DisplayLayout.GroupByBox.PromptAppearance = appearance125;
			comboBoxgridVehicleType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxgridVehicleType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance126.BackColor = System.Drawing.SystemColors.Window;
			appearance126.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxgridVehicleType.DisplayLayout.Override.ActiveCellAppearance = appearance126;
			appearance127.BackColor = System.Drawing.SystemColors.Highlight;
			appearance127.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxgridVehicleType.DisplayLayout.Override.ActiveRowAppearance = appearance127;
			comboBoxgridVehicleType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxgridVehicleType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance128.BackColor = System.Drawing.SystemColors.Window;
			comboBoxgridVehicleType.DisplayLayout.Override.CardAreaAppearance = appearance128;
			appearance129.BorderColor = System.Drawing.Color.Silver;
			appearance129.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxgridVehicleType.DisplayLayout.Override.CellAppearance = appearance129;
			comboBoxgridVehicleType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxgridVehicleType.DisplayLayout.Override.CellPadding = 0;
			appearance130.BackColor = System.Drawing.SystemColors.Control;
			appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance130.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance130.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxgridVehicleType.DisplayLayout.Override.GroupByRowAppearance = appearance130;
			appearance131.TextHAlignAsString = "Left";
			comboBoxgridVehicleType.DisplayLayout.Override.HeaderAppearance = appearance131;
			comboBoxgridVehicleType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxgridVehicleType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance132.BackColor = System.Drawing.SystemColors.Window;
			appearance132.BorderColor = System.Drawing.Color.Silver;
			comboBoxgridVehicleType.DisplayLayout.Override.RowAppearance = appearance132;
			comboBoxgridVehicleType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance133.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxgridVehicleType.DisplayLayout.Override.TemplateAddRowAppearance = appearance133;
			comboBoxgridVehicleType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxgridVehicleType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxgridVehicleType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxgridVehicleType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxgridVehicleType.Editable = true;
			comboBoxgridVehicleType.FilterString = "";
			comboBoxgridVehicleType.GenericListType = Micromind.Common.Data.GenericListTypes.VehicleType;
			comboBoxgridVehicleType.HasAllAccount = false;
			comboBoxgridVehicleType.HasCustom = false;
			comboBoxgridVehicleType.IsDataLoaded = false;
			comboBoxgridVehicleType.IsSingleColumn = false;
			comboBoxgridVehicleType.Location = new System.Drawing.Point(435, 65);
			comboBoxgridVehicleType.MaxDropDownItems = 12;
			comboBoxgridVehicleType.Name = "comboBoxgridVehicleType";
			comboBoxgridVehicleType.ShowInactiveItems = false;
			comboBoxgridVehicleType.ShowQuickAdd = true;
			comboBoxgridVehicleType.Size = new System.Drawing.Size(142, 20);
			comboBoxgridVehicleType.TabIndex = 88;
			comboBoxgridVehicleType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxgridVehicleType.Visible = false;
			comboBoxGridVehicleMake.Assigned = false;
			comboBoxGridVehicleMake.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridVehicleMake.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridVehicleMake.CustomReportFieldName = "";
			comboBoxGridVehicleMake.CustomReportKey = "";
			comboBoxGridVehicleMake.CustomReportValueType = 1;
			comboBoxGridVehicleMake.DescriptionTextBox = null;
			appearance134.BackColor = System.Drawing.SystemColors.Window;
			appearance134.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridVehicleMake.DisplayLayout.Appearance = appearance134;
			comboBoxGridVehicleMake.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridVehicleMake.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance135.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance135.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance135.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance135.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVehicleMake.DisplayLayout.GroupByBox.Appearance = appearance135;
			appearance136.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVehicleMake.DisplayLayout.GroupByBox.BandLabelAppearance = appearance136;
			comboBoxGridVehicleMake.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance137.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance137.BackColor2 = System.Drawing.SystemColors.Control;
			appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance137.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVehicleMake.DisplayLayout.GroupByBox.PromptAppearance = appearance137;
			comboBoxGridVehicleMake.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridVehicleMake.DisplayLayout.MaxRowScrollRegions = 1;
			appearance138.BackColor = System.Drawing.SystemColors.Window;
			appearance138.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridVehicleMake.DisplayLayout.Override.ActiveCellAppearance = appearance138;
			appearance139.BackColor = System.Drawing.SystemColors.Highlight;
			appearance139.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridVehicleMake.DisplayLayout.Override.ActiveRowAppearance = appearance139;
			comboBoxGridVehicleMake.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridVehicleMake.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance140.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridVehicleMake.DisplayLayout.Override.CardAreaAppearance = appearance140;
			appearance141.BorderColor = System.Drawing.Color.Silver;
			appearance141.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridVehicleMake.DisplayLayout.Override.CellAppearance = appearance141;
			comboBoxGridVehicleMake.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridVehicleMake.DisplayLayout.Override.CellPadding = 0;
			appearance142.BackColor = System.Drawing.SystemColors.Control;
			appearance142.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance142.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance142.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVehicleMake.DisplayLayout.Override.GroupByRowAppearance = appearance142;
			appearance143.TextHAlignAsString = "Left";
			comboBoxGridVehicleMake.DisplayLayout.Override.HeaderAppearance = appearance143;
			comboBoxGridVehicleMake.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridVehicleMake.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance144.BackColor = System.Drawing.SystemColors.Window;
			appearance144.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridVehicleMake.DisplayLayout.Override.RowAppearance = appearance144;
			comboBoxGridVehicleMake.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance145.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridVehicleMake.DisplayLayout.Override.TemplateAddRowAppearance = appearance145;
			comboBoxGridVehicleMake.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridVehicleMake.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridVehicleMake.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridVehicleMake.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridVehicleMake.Editable = true;
			comboBoxGridVehicleMake.FilterString = "";
			comboBoxGridVehicleMake.GenericListType = Micromind.Common.Data.GenericListTypes.VehicleMake;
			comboBoxGridVehicleMake.HasAllAccount = false;
			comboBoxGridVehicleMake.HasCustom = false;
			comboBoxGridVehicleMake.IsDataLoaded = false;
			comboBoxGridVehicleMake.IsSingleColumn = false;
			comboBoxGridVehicleMake.Location = new System.Drawing.Point(252, 65);
			comboBoxGridVehicleMake.MaxDropDownItems = 12;
			comboBoxGridVehicleMake.Name = "comboBoxGridVehicleMake";
			comboBoxGridVehicleMake.ShowInactiveItems = false;
			comboBoxGridVehicleMake.ShowQuickAdd = true;
			comboBoxGridVehicleMake.Size = new System.Drawing.Size(142, 20);
			comboBoxGridVehicleMake.TabIndex = 98;
			comboBoxGridVehicleMake.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridVehicleMake.Visible = false;
			datagridAppliedModels.AllowAddNew = false;
			appearance146.BackColor = System.Drawing.SystemColors.Window;
			appearance146.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			datagridAppliedModels.DisplayLayout.Appearance = appearance146;
			datagridAppliedModels.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			datagridAppliedModels.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance147.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance147.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance147.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance147.BorderColor = System.Drawing.SystemColors.Window;
			datagridAppliedModels.DisplayLayout.GroupByBox.Appearance = appearance147;
			appearance148.ForeColor = System.Drawing.SystemColors.GrayText;
			datagridAppliedModels.DisplayLayout.GroupByBox.BandLabelAppearance = appearance148;
			datagridAppliedModels.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance149.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance149.BackColor2 = System.Drawing.SystemColors.Control;
			appearance149.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance149.ForeColor = System.Drawing.SystemColors.GrayText;
			datagridAppliedModels.DisplayLayout.GroupByBox.PromptAppearance = appearance149;
			datagridAppliedModels.DisplayLayout.MaxColScrollRegions = 1;
			datagridAppliedModels.DisplayLayout.MaxRowScrollRegions = 1;
			appearance150.BackColor = System.Drawing.SystemColors.Window;
			appearance150.ForeColor = System.Drawing.SystemColors.ControlText;
			datagridAppliedModels.DisplayLayout.Override.ActiveCellAppearance = appearance150;
			appearance151.BackColor = System.Drawing.SystemColors.Highlight;
			appearance151.ForeColor = System.Drawing.SystemColors.HighlightText;
			datagridAppliedModels.DisplayLayout.Override.ActiveRowAppearance = appearance151;
			datagridAppliedModels.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			datagridAppliedModels.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			datagridAppliedModels.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance152.BackColor = System.Drawing.SystemColors.Window;
			datagridAppliedModels.DisplayLayout.Override.CardAreaAppearance = appearance152;
			appearance153.BorderColor = System.Drawing.Color.Silver;
			appearance153.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			datagridAppliedModels.DisplayLayout.Override.CellAppearance = appearance153;
			datagridAppliedModels.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			datagridAppliedModels.DisplayLayout.Override.CellPadding = 0;
			appearance154.BackColor = System.Drawing.SystemColors.Control;
			appearance154.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance154.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance154.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance154.BorderColor = System.Drawing.SystemColors.Window;
			datagridAppliedModels.DisplayLayout.Override.GroupByRowAppearance = appearance154;
			appearance155.TextHAlignAsString = "Left";
			datagridAppliedModels.DisplayLayout.Override.HeaderAppearance = appearance155;
			datagridAppliedModels.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			datagridAppliedModels.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance156.BackColor = System.Drawing.SystemColors.Window;
			appearance156.BorderColor = System.Drawing.Color.Silver;
			datagridAppliedModels.DisplayLayout.Override.RowAppearance = appearance156;
			datagridAppliedModels.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance157.BackColor = System.Drawing.SystemColors.ControlLight;
			datagridAppliedModels.DisplayLayout.Override.TemplateAddRowAppearance = appearance157;
			datagridAppliedModels.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			datagridAppliedModels.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			datagridAppliedModels.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			datagridAppliedModels.IncludeLotItems = false;
			datagridAppliedModels.LoadLayoutFailed = false;
			datagridAppliedModels.Location = new System.Drawing.Point(4, 15);
			datagridAppliedModels.Name = "datagridAppliedModels";
			datagridAppliedModels.ShowClearMenu = true;
			datagridAppliedModels.ShowDeleteMenu = true;
			datagridAppliedModels.ShowInsertMenu = true;
			datagridAppliedModels.ShowMoveRowsMenu = true;
			datagridAppliedModels.Size = new System.Drawing.Size(635, 129);
			datagridAppliedModels.TabIndex = 0;
			datagridAppliedModels.Text = "dataEntryGrid1";
			groupBox3.Controls.Add(comboBoxSubstiProduct);
			groupBox3.Controls.Add(dataGridSubstituteItems);
			groupBox3.Location = new System.Drawing.Point(17, 174);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(646, 150);
			groupBox3.TabIndex = 3;
			groupBox3.TabStop = false;
			groupBox3.Text = "Substitute Product Entry";
			comboBoxSubstiProduct.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxSubstiProduct.Assigned = false;
			comboBoxSubstiProduct.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSubstiProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSubstiProduct.CustomReportFieldName = "";
			comboBoxSubstiProduct.CustomReportKey = "";
			comboBoxSubstiProduct.CustomReportValueType = 1;
			comboBoxSubstiProduct.DescriptionTextBox = null;
			appearance158.BackColor = System.Drawing.SystemColors.Window;
			appearance158.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSubstiProduct.DisplayLayout.Appearance = appearance158;
			comboBoxSubstiProduct.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSubstiProduct.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance159.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance159.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance159.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance159.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSubstiProduct.DisplayLayout.GroupByBox.Appearance = appearance159;
			appearance160.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSubstiProduct.DisplayLayout.GroupByBox.BandLabelAppearance = appearance160;
			comboBoxSubstiProduct.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance161.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance161.BackColor2 = System.Drawing.SystemColors.Control;
			appearance161.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance161.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSubstiProduct.DisplayLayout.GroupByBox.PromptAppearance = appearance161;
			comboBoxSubstiProduct.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSubstiProduct.DisplayLayout.MaxRowScrollRegions = 1;
			appearance162.BackColor = System.Drawing.SystemColors.Window;
			appearance162.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSubstiProduct.DisplayLayout.Override.ActiveCellAppearance = appearance162;
			appearance163.BackColor = System.Drawing.SystemColors.Highlight;
			appearance163.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSubstiProduct.DisplayLayout.Override.ActiveRowAppearance = appearance163;
			comboBoxSubstiProduct.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSubstiProduct.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance164.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSubstiProduct.DisplayLayout.Override.CardAreaAppearance = appearance164;
			appearance165.BorderColor = System.Drawing.Color.Silver;
			appearance165.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSubstiProduct.DisplayLayout.Override.CellAppearance = appearance165;
			comboBoxSubstiProduct.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSubstiProduct.DisplayLayout.Override.CellPadding = 0;
			appearance166.BackColor = System.Drawing.SystemColors.Control;
			appearance166.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance166.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance166.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance166.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSubstiProduct.DisplayLayout.Override.GroupByRowAppearance = appearance166;
			appearance167.TextHAlignAsString = "Left";
			comboBoxSubstiProduct.DisplayLayout.Override.HeaderAppearance = appearance167;
			comboBoxSubstiProduct.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSubstiProduct.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance168.BackColor = System.Drawing.SystemColors.Window;
			appearance168.BorderColor = System.Drawing.Color.Silver;
			comboBoxSubstiProduct.DisplayLayout.Override.RowAppearance = appearance168;
			comboBoxSubstiProduct.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance169.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSubstiProduct.DisplayLayout.Override.TemplateAddRowAppearance = appearance169;
			comboBoxSubstiProduct.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSubstiProduct.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSubstiProduct.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSubstiProduct.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSubstiProduct.Editable = true;
			comboBoxSubstiProduct.FilterCustomerID = "";
			comboBoxSubstiProduct.FilterString = "";
			comboBoxSubstiProduct.FilterSysDocID = "";
			comboBoxSubstiProduct.HasAllAccount = false;
			comboBoxSubstiProduct.HasCustom = false;
			comboBoxSubstiProduct.IsDataLoaded = false;
			comboBoxSubstiProduct.Location = new System.Drawing.Point(253, 80);
			comboBoxSubstiProduct.MaxDropDownItems = 12;
			comboBoxSubstiProduct.Name = "comboBoxSubstiProduct";
			comboBoxSubstiProduct.Show3PLItems = true;
			comboBoxSubstiProduct.ShowInactiveItems = false;
			comboBoxSubstiProduct.ShowOnlyLotItems = false;
			comboBoxSubstiProduct.ShowQuickAdd = true;
			comboBoxSubstiProduct.Size = new System.Drawing.Size(100, 20);
			comboBoxSubstiProduct.TabIndex = 95;
			comboBoxSubstiProduct.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSubstiProduct.Visible = false;
			dataGridSubstituteItems.AllowAddNew = false;
			appearance170.BackColor = System.Drawing.SystemColors.Window;
			appearance170.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridSubstituteItems.DisplayLayout.Appearance = appearance170;
			dataGridSubstituteItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridSubstituteItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance171.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance171.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance171.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance171.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSubstituteItems.DisplayLayout.GroupByBox.Appearance = appearance171;
			appearance172.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSubstituteItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance172;
			dataGridSubstituteItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance173.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance173.BackColor2 = System.Drawing.SystemColors.Control;
			appearance173.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance173.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSubstituteItems.DisplayLayout.GroupByBox.PromptAppearance = appearance173;
			dataGridSubstituteItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridSubstituteItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance174.BackColor = System.Drawing.SystemColors.Window;
			appearance174.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridSubstituteItems.DisplayLayout.Override.ActiveCellAppearance = appearance174;
			appearance175.BackColor = System.Drawing.SystemColors.Highlight;
			appearance175.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridSubstituteItems.DisplayLayout.Override.ActiveRowAppearance = appearance175;
			dataGridSubstituteItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridSubstituteItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridSubstituteItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance176.BackColor = System.Drawing.SystemColors.Window;
			dataGridSubstituteItems.DisplayLayout.Override.CardAreaAppearance = appearance176;
			appearance177.BorderColor = System.Drawing.Color.Silver;
			appearance177.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridSubstituteItems.DisplayLayout.Override.CellAppearance = appearance177;
			dataGridSubstituteItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridSubstituteItems.DisplayLayout.Override.CellPadding = 0;
			appearance178.BackColor = System.Drawing.SystemColors.Control;
			appearance178.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance178.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance178.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance178.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSubstituteItems.DisplayLayout.Override.GroupByRowAppearance = appearance178;
			appearance179.TextHAlignAsString = "Left";
			dataGridSubstituteItems.DisplayLayout.Override.HeaderAppearance = appearance179;
			dataGridSubstituteItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridSubstituteItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance180.BackColor = System.Drawing.SystemColors.Window;
			appearance180.BorderColor = System.Drawing.Color.Silver;
			dataGridSubstituteItems.DisplayLayout.Override.RowAppearance = appearance180;
			dataGridSubstituteItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance181.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridSubstituteItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance181;
			dataGridSubstituteItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridSubstituteItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridSubstituteItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridSubstituteItems.IncludeLotItems = false;
			dataGridSubstituteItems.LoadLayoutFailed = false;
			dataGridSubstituteItems.Location = new System.Drawing.Point(3, 19);
			dataGridSubstituteItems.Name = "dataGridSubstituteItems";
			dataGridSubstituteItems.ShowClearMenu = true;
			dataGridSubstituteItems.ShowDeleteMenu = true;
			dataGridSubstituteItems.ShowInsertMenu = true;
			dataGridSubstituteItems.ShowMoveRowsMenu = true;
			dataGridSubstituteItems.Size = new System.Drawing.Size(635, 123);
			dataGridSubstituteItems.TabIndex = 0;
			dataGridSubstituteItems.Text = "dataEntryGrid1";
			dataGridSubstituteItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridSubstituteItems_DoubleClickRow);
			groupBox2.Controls.Add(ultraFormattedLinkLabel13);
			groupBox2.Controls.Add(ultraFormattedLinkLabel16);
			groupBox2.Controls.Add(mmLabel26);
			groupBox2.Controls.Add(ultraFormattedLinkLabel14);
			groupBox2.Controls.Add(textBoxOEMCode);
			groupBox2.Controls.Add(textBoxEngineNo);
			groupBox2.Controls.Add(textBoxModelNo);
			groupBox2.Controls.Add(textBoxChasisNo);
			groupBox2.Controls.Add(mmLabel31);
			groupBox2.Controls.Add(mmLabel32);
			groupBox2.Controls.Add(comboBoxPartsFamily);
			groupBox2.Controls.Add(comboboxPartsMakeType);
			groupBox2.Controls.Add(mmLabel24);
			groupBox2.Controls.Add(comboBoxPartsType);
			groupBox2.Location = new System.Drawing.Point(260, 59);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(402, 109);
			groupBox2.TabIndex = 2;
			groupBox2.TabStop = false;
			groupBox2.Text = "Parts";
			ultraFormattedLinkLabel13.AutoSize = true;
			ultraFormattedLinkLabel13.Location = new System.Drawing.Point(8, 17);
			ultraFormattedLinkLabel13.Name = "ultraFormattedLinkLabel13";
			ultraFormattedLinkLabel13.Size = new System.Drawing.Size(62, 14);
			ultraFormattedLinkLabel13.TabIndex = 98;
			ultraFormattedLinkLabel13.TabStop = true;
			ultraFormattedLinkLabel13.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel13.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel13.Value = "Make Type:";
			appearance182.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel13.VisitedLinkAppearance = appearance182;
			ultraFormattedLinkLabel13.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel13_LinkClicked);
			ultraFormattedLinkLabel16.AutoSize = true;
			ultraFormattedLinkLabel16.Location = new System.Drawing.Point(7, 38);
			ultraFormattedLinkLabel16.Name = "ultraFormattedLinkLabel16";
			ultraFormattedLinkLabel16.Size = new System.Drawing.Size(29, 14);
			ultraFormattedLinkLabel16.TabIndex = 101;
			ultraFormattedLinkLabel16.TabStop = true;
			ultraFormattedLinkLabel16.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel16.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel16.Value = "Type";
			appearance183.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel16.VisitedLinkAppearance = appearance183;
			ultraFormattedLinkLabel16.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel16_LinkClicked);
			mmLabel26.AutoSize = true;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(5, 88);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(62, 13);
			mmLabel26.TabIndex = 99;
			mmLabel26.Text = "OEM Code:";
			ultraFormattedLinkLabel14.AutoSize = true;
			ultraFormattedLinkLabel14.Location = new System.Drawing.Point(8, 62);
			ultraFormattedLinkLabel14.Name = "ultraFormattedLinkLabel14";
			ultraFormattedLinkLabel14.Size = new System.Drawing.Size(40, 14);
			ultraFormattedLinkLabel14.TabIndex = 99;
			ultraFormattedLinkLabel14.TabStop = true;
			ultraFormattedLinkLabel14.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel14.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel14.Value = "Family:";
			appearance184.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel14.VisitedLinkAppearance = appearance184;
			ultraFormattedLinkLabel14.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel14_LinkClicked);
			textBoxOEMCode.BackColor = System.Drawing.Color.White;
			textBoxOEMCode.CustomReportFieldName = "";
			textBoxOEMCode.CustomReportKey = "";
			textBoxOEMCode.CustomReportValueType = 1;
			textBoxOEMCode.IsComboTextBox = false;
			textBoxOEMCode.IsModified = false;
			textBoxOEMCode.Location = new System.Drawing.Point(76, 84);
			textBoxOEMCode.MaxLength = 30;
			textBoxOEMCode.Name = "textBoxOEMCode";
			textBoxOEMCode.Size = new System.Drawing.Size(142, 20);
			textBoxOEMCode.TabIndex = 3;
			textBoxEngineNo.BackColor = System.Drawing.Color.White;
			textBoxEngineNo.CustomReportFieldName = "";
			textBoxEngineNo.CustomReportKey = "";
			textBoxEngineNo.CustomReportValueType = 1;
			textBoxEngineNo.IsComboTextBox = false;
			textBoxEngineNo.IsModified = false;
			textBoxEngineNo.Location = new System.Drawing.Point(288, 61);
			textBoxEngineNo.MaxLength = 30;
			textBoxEngineNo.Name = "textBoxEngineNo";
			textBoxEngineNo.Size = new System.Drawing.Size(105, 20);
			textBoxEngineNo.TabIndex = 6;
			textBoxModelNo.BackColor = System.Drawing.Color.White;
			textBoxModelNo.CustomReportFieldName = "";
			textBoxModelNo.CustomReportKey = "";
			textBoxModelNo.CustomReportValueType = 1;
			textBoxModelNo.IsComboTextBox = false;
			textBoxModelNo.IsModified = false;
			textBoxModelNo.Location = new System.Drawing.Point(288, 37);
			textBoxModelNo.MaxLength = 30;
			textBoxModelNo.Name = "textBoxModelNo";
			textBoxModelNo.Size = new System.Drawing.Size(105, 20);
			textBoxModelNo.TabIndex = 5;
			textBoxChasisNo.BackColor = System.Drawing.Color.White;
			textBoxChasisNo.CustomReportFieldName = "";
			textBoxChasisNo.CustomReportKey = "";
			textBoxChasisNo.CustomReportValueType = 1;
			textBoxChasisNo.IsComboTextBox = false;
			textBoxChasisNo.IsModified = false;
			textBoxChasisNo.Location = new System.Drawing.Point(288, 13);
			textBoxChasisNo.MaxLength = 30;
			textBoxChasisNo.Name = "textBoxChasisNo";
			textBoxChasisNo.Size = new System.Drawing.Size(105, 20);
			textBoxChasisNo.TabIndex = 4;
			mmLabel31.AutoSize = true;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(224, 68);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(60, 13);
			mmLabel31.TabIndex = 96;
			mmLabel31.Text = "Engine No:";
			mmLabel32.AutoSize = true;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(226, 17);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(58, 13);
			mmLabel32.TabIndex = 97;
			mmLabel32.Text = "Chasis No:";
			comboBoxPartsFamily.Assigned = false;
			comboBoxPartsFamily.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPartsFamily.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPartsFamily.CustomReportFieldName = "";
			comboBoxPartsFamily.CustomReportKey = "";
			comboBoxPartsFamily.CustomReportValueType = 1;
			comboBoxPartsFamily.DescriptionTextBox = null;
			appearance185.BackColor = System.Drawing.SystemColors.Window;
			appearance185.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPartsFamily.DisplayLayout.Appearance = appearance185;
			comboBoxPartsFamily.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPartsFamily.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance186.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance186.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance186.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance186.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPartsFamily.DisplayLayout.GroupByBox.Appearance = appearance186;
			appearance187.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPartsFamily.DisplayLayout.GroupByBox.BandLabelAppearance = appearance187;
			comboBoxPartsFamily.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance188.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance188.BackColor2 = System.Drawing.SystemColors.Control;
			appearance188.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance188.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPartsFamily.DisplayLayout.GroupByBox.PromptAppearance = appearance188;
			comboBoxPartsFamily.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPartsFamily.DisplayLayout.MaxRowScrollRegions = 1;
			appearance189.BackColor = System.Drawing.SystemColors.Window;
			appearance189.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPartsFamily.DisplayLayout.Override.ActiveCellAppearance = appearance189;
			appearance190.BackColor = System.Drawing.SystemColors.Highlight;
			appearance190.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPartsFamily.DisplayLayout.Override.ActiveRowAppearance = appearance190;
			comboBoxPartsFamily.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPartsFamily.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance191.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPartsFamily.DisplayLayout.Override.CardAreaAppearance = appearance191;
			appearance192.BorderColor = System.Drawing.Color.Silver;
			appearance192.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPartsFamily.DisplayLayout.Override.CellAppearance = appearance192;
			comboBoxPartsFamily.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPartsFamily.DisplayLayout.Override.CellPadding = 0;
			appearance193.BackColor = System.Drawing.SystemColors.Control;
			appearance193.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance193.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance193.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance193.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPartsFamily.DisplayLayout.Override.GroupByRowAppearance = appearance193;
			appearance194.TextHAlignAsString = "Left";
			comboBoxPartsFamily.DisplayLayout.Override.HeaderAppearance = appearance194;
			comboBoxPartsFamily.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPartsFamily.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance195.BackColor = System.Drawing.SystemColors.Window;
			appearance195.BorderColor = System.Drawing.Color.Silver;
			comboBoxPartsFamily.DisplayLayout.Override.RowAppearance = appearance195;
			comboBoxPartsFamily.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance196.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPartsFamily.DisplayLayout.Override.TemplateAddRowAppearance = appearance196;
			comboBoxPartsFamily.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPartsFamily.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPartsFamily.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPartsFamily.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPartsFamily.Editable = true;
			comboBoxPartsFamily.FilterString = "";
			comboBoxPartsFamily.GenericListType = Micromind.Common.Data.GenericListTypes.PartsFamily;
			comboBoxPartsFamily.HasAllAccount = false;
			comboBoxPartsFamily.HasCustom = false;
			comboBoxPartsFamily.IsDataLoaded = false;
			comboBoxPartsFamily.IsSingleColumn = false;
			comboBoxPartsFamily.Location = new System.Drawing.Point(76, 60);
			comboBoxPartsFamily.MaxDropDownItems = 12;
			comboBoxPartsFamily.Name = "comboBoxPartsFamily";
			comboBoxPartsFamily.ShowInactiveItems = false;
			comboBoxPartsFamily.ShowQuickAdd = true;
			comboBoxPartsFamily.Size = new System.Drawing.Size(142, 20);
			comboBoxPartsFamily.TabIndex = 2;
			comboBoxPartsFamily.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboboxPartsMakeType.Assigned = false;
			comboboxPartsMakeType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboboxPartsMakeType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboboxPartsMakeType.CustomReportFieldName = "";
			comboboxPartsMakeType.CustomReportKey = "";
			comboboxPartsMakeType.CustomReportValueType = 1;
			comboboxPartsMakeType.DescriptionTextBox = null;
			appearance197.BackColor = System.Drawing.SystemColors.Window;
			appearance197.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboboxPartsMakeType.DisplayLayout.Appearance = appearance197;
			comboboxPartsMakeType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboboxPartsMakeType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance198.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance198.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance198.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance198.BorderColor = System.Drawing.SystemColors.Window;
			comboboxPartsMakeType.DisplayLayout.GroupByBox.Appearance = appearance198;
			appearance199.ForeColor = System.Drawing.SystemColors.GrayText;
			comboboxPartsMakeType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance199;
			comboboxPartsMakeType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance200.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance200.BackColor2 = System.Drawing.SystemColors.Control;
			appearance200.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance200.ForeColor = System.Drawing.SystemColors.GrayText;
			comboboxPartsMakeType.DisplayLayout.GroupByBox.PromptAppearance = appearance200;
			comboboxPartsMakeType.DisplayLayout.MaxColScrollRegions = 1;
			comboboxPartsMakeType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance201.BackColor = System.Drawing.SystemColors.Window;
			appearance201.ForeColor = System.Drawing.SystemColors.ControlText;
			comboboxPartsMakeType.DisplayLayout.Override.ActiveCellAppearance = appearance201;
			appearance202.BackColor = System.Drawing.SystemColors.Highlight;
			appearance202.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboboxPartsMakeType.DisplayLayout.Override.ActiveRowAppearance = appearance202;
			comboboxPartsMakeType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboboxPartsMakeType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance203.BackColor = System.Drawing.SystemColors.Window;
			comboboxPartsMakeType.DisplayLayout.Override.CardAreaAppearance = appearance203;
			appearance204.BorderColor = System.Drawing.Color.Silver;
			appearance204.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboboxPartsMakeType.DisplayLayout.Override.CellAppearance = appearance204;
			comboboxPartsMakeType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboboxPartsMakeType.DisplayLayout.Override.CellPadding = 0;
			appearance205.BackColor = System.Drawing.SystemColors.Control;
			appearance205.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance205.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance205.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance205.BorderColor = System.Drawing.SystemColors.Window;
			comboboxPartsMakeType.DisplayLayout.Override.GroupByRowAppearance = appearance205;
			appearance206.TextHAlignAsString = "Left";
			comboboxPartsMakeType.DisplayLayout.Override.HeaderAppearance = appearance206;
			comboboxPartsMakeType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboboxPartsMakeType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance207.BackColor = System.Drawing.SystemColors.Window;
			appearance207.BorderColor = System.Drawing.Color.Silver;
			comboboxPartsMakeType.DisplayLayout.Override.RowAppearance = appearance207;
			comboboxPartsMakeType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance208.BackColor = System.Drawing.SystemColors.ControlLight;
			comboboxPartsMakeType.DisplayLayout.Override.TemplateAddRowAppearance = appearance208;
			comboboxPartsMakeType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboboxPartsMakeType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboboxPartsMakeType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboboxPartsMakeType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboboxPartsMakeType.Editable = true;
			comboboxPartsMakeType.FilterString = "";
			comboboxPartsMakeType.GenericListType = Micromind.Common.Data.GenericListTypes.PartsMakeType;
			comboboxPartsMakeType.HasAllAccount = false;
			comboboxPartsMakeType.HasCustom = false;
			comboboxPartsMakeType.IsDataLoaded = false;
			comboboxPartsMakeType.IsSingleColumn = false;
			comboboxPartsMakeType.Location = new System.Drawing.Point(76, 12);
			comboboxPartsMakeType.MaxDropDownItems = 12;
			comboboxPartsMakeType.Name = "comboboxPartsMakeType";
			comboboxPartsMakeType.ShowInactiveItems = false;
			comboboxPartsMakeType.ShowQuickAdd = true;
			comboboxPartsMakeType.Size = new System.Drawing.Size(142, 20);
			comboboxPartsMakeType.TabIndex = 0;
			comboboxPartsMakeType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel24.AutoSize = true;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(226, 42);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(39, 13);
			mmLabel24.TabIndex = 84;
			mmLabel24.Text = "Model:";
			comboBoxPartsType.Assigned = false;
			comboBoxPartsType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPartsType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPartsType.CustomReportFieldName = "";
			comboBoxPartsType.CustomReportKey = "";
			comboBoxPartsType.CustomReportValueType = 1;
			comboBoxPartsType.DescriptionTextBox = null;
			appearance209.BackColor = System.Drawing.SystemColors.Window;
			appearance209.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPartsType.DisplayLayout.Appearance = appearance209;
			comboBoxPartsType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPartsType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance210.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance210.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance210.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance210.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPartsType.DisplayLayout.GroupByBox.Appearance = appearance210;
			appearance211.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPartsType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance211;
			comboBoxPartsType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance212.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance212.BackColor2 = System.Drawing.SystemColors.Control;
			appearance212.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance212.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPartsType.DisplayLayout.GroupByBox.PromptAppearance = appearance212;
			comboBoxPartsType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPartsType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance213.BackColor = System.Drawing.SystemColors.Window;
			appearance213.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPartsType.DisplayLayout.Override.ActiveCellAppearance = appearance213;
			appearance214.BackColor = System.Drawing.SystemColors.Highlight;
			appearance214.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPartsType.DisplayLayout.Override.ActiveRowAppearance = appearance214;
			comboBoxPartsType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPartsType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance215.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPartsType.DisplayLayout.Override.CardAreaAppearance = appearance215;
			appearance216.BorderColor = System.Drawing.Color.Silver;
			appearance216.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPartsType.DisplayLayout.Override.CellAppearance = appearance216;
			comboBoxPartsType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPartsType.DisplayLayout.Override.CellPadding = 0;
			appearance217.BackColor = System.Drawing.SystemColors.Control;
			appearance217.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance217.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance217.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance217.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPartsType.DisplayLayout.Override.GroupByRowAppearance = appearance217;
			appearance218.TextHAlignAsString = "Left";
			comboBoxPartsType.DisplayLayout.Override.HeaderAppearance = appearance218;
			comboBoxPartsType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPartsType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance219.BackColor = System.Drawing.SystemColors.Window;
			appearance219.BorderColor = System.Drawing.Color.Silver;
			comboBoxPartsType.DisplayLayout.Override.RowAppearance = appearance219;
			comboBoxPartsType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance220.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPartsType.DisplayLayout.Override.TemplateAddRowAppearance = appearance220;
			comboBoxPartsType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPartsType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPartsType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPartsType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPartsType.Editable = true;
			comboBoxPartsType.FilterString = "";
			comboBoxPartsType.GenericListType = Micromind.Common.Data.GenericListTypes.PartsType;
			comboBoxPartsType.HasAllAccount = false;
			comboBoxPartsType.HasCustom = false;
			comboBoxPartsType.IsDataLoaded = false;
			comboBoxPartsType.IsSingleColumn = false;
			comboBoxPartsType.Location = new System.Drawing.Point(76, 35);
			comboBoxPartsType.MaxDropDownItems = 12;
			comboBoxPartsType.Name = "comboBoxPartsType";
			comboBoxPartsType.ShowInactiveItems = false;
			comboBoxPartsType.ShowQuickAdd = true;
			comboBoxPartsType.Size = new System.Drawing.Size(142, 20);
			comboBoxPartsType.TabIndex = 1;
			comboBoxPartsType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel16.AutoSize = true;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(17, 14);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(71, 13);
			mmLabel16.TabIndex = 79;
			mmLabel16.Text = "Specification:";
			textBoxSpecification.BackColor = System.Drawing.Color.White;
			textBoxSpecification.CustomReportFieldName = "";
			textBoxSpecification.CustomReportKey = "";
			textBoxSpecification.CustomReportValueType = 1;
			textBoxSpecification.IsComboTextBox = false;
			textBoxSpecification.IsModified = false;
			textBoxSpecification.Location = new System.Drawing.Point(97, 10);
			textBoxSpecification.MaxLength = 255;
			textBoxSpecification.Multiline = true;
			textBoxSpecification.Name = "textBoxSpecification";
			textBoxSpecification.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxSpecification.Size = new System.Drawing.Size(381, 48);
			textBoxSpecification.TabIndex = 0;
			groupBox1.Controls.Add(comboBoxVehicleMake);
			groupBox1.Controls.Add(ultraFormattedLinkLabel15);
			groupBox1.Controls.Add(comboBoxVehicleModel);
			groupBox1.Controls.Add(ultraFormattedLinkLabel1);
			groupBox1.Controls.Add(ComboBoxVehicleType);
			groupBox1.Controls.Add(ultraFormattedLinkLabel2);
			groupBox1.Location = new System.Drawing.Point(16, 60);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(238, 108);
			groupBox1.TabIndex = 1;
			groupBox1.TabStop = false;
			groupBox1.Text = "Vehicle";
			comboBoxVehicleMake.Assigned = false;
			comboBoxVehicleMake.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVehicleMake.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVehicleMake.CustomReportFieldName = "";
			comboBoxVehicleMake.CustomReportKey = "";
			comboBoxVehicleMake.CustomReportValueType = 1;
			comboBoxVehicleMake.DescriptionTextBox = null;
			appearance221.BackColor = System.Drawing.SystemColors.Window;
			appearance221.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVehicleMake.DisplayLayout.Appearance = appearance221;
			comboBoxVehicleMake.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVehicleMake.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance222.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance222.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance222.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance222.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleMake.DisplayLayout.GroupByBox.Appearance = appearance222;
			appearance223.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicleMake.DisplayLayout.GroupByBox.BandLabelAppearance = appearance223;
			comboBoxVehicleMake.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance224.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance224.BackColor2 = System.Drawing.SystemColors.Control;
			appearance224.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance224.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicleMake.DisplayLayout.GroupByBox.PromptAppearance = appearance224;
			comboBoxVehicleMake.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVehicleMake.DisplayLayout.MaxRowScrollRegions = 1;
			appearance225.BackColor = System.Drawing.SystemColors.Window;
			appearance225.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVehicleMake.DisplayLayout.Override.ActiveCellAppearance = appearance225;
			appearance226.BackColor = System.Drawing.SystemColors.Highlight;
			appearance226.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVehicleMake.DisplayLayout.Override.ActiveRowAppearance = appearance226;
			comboBoxVehicleMake.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVehicleMake.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance227.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleMake.DisplayLayout.Override.CardAreaAppearance = appearance227;
			appearance228.BorderColor = System.Drawing.Color.Silver;
			appearance228.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVehicleMake.DisplayLayout.Override.CellAppearance = appearance228;
			comboBoxVehicleMake.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVehicleMake.DisplayLayout.Override.CellPadding = 0;
			appearance229.BackColor = System.Drawing.SystemColors.Control;
			appearance229.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance229.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance229.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance229.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleMake.DisplayLayout.Override.GroupByRowAppearance = appearance229;
			appearance230.TextHAlignAsString = "Left";
			comboBoxVehicleMake.DisplayLayout.Override.HeaderAppearance = appearance230;
			comboBoxVehicleMake.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVehicleMake.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance231.BackColor = System.Drawing.SystemColors.Window;
			appearance231.BorderColor = System.Drawing.Color.Silver;
			comboBoxVehicleMake.DisplayLayout.Override.RowAppearance = appearance231;
			comboBoxVehicleMake.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance232.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVehicleMake.DisplayLayout.Override.TemplateAddRowAppearance = appearance232;
			comboBoxVehicleMake.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVehicleMake.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVehicleMake.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVehicleMake.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVehicleMake.Editable = true;
			comboBoxVehicleMake.FilterString = "";
			comboBoxVehicleMake.GenericListType = Micromind.Common.Data.GenericListTypes.VehicleMake;
			comboBoxVehicleMake.HasAllAccount = false;
			comboBoxVehicleMake.HasCustom = false;
			comboBoxVehicleMake.IsDataLoaded = false;
			comboBoxVehicleMake.IsSingleColumn = false;
			comboBoxVehicleMake.Location = new System.Drawing.Point(81, 13);
			comboBoxVehicleMake.MaxDropDownItems = 12;
			comboBoxVehicleMake.Name = "comboBoxVehicleMake";
			comboBoxVehicleMake.ShowInactiveItems = false;
			comboBoxVehicleMake.ShowQuickAdd = true;
			comboBoxVehicleMake.Size = new System.Drawing.Size(142, 20);
			comboBoxVehicleMake.TabIndex = 0;
			comboBoxVehicleMake.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel15.AutoSize = true;
			ultraFormattedLinkLabel15.Location = new System.Drawing.Point(14, 41);
			ultraFormattedLinkLabel15.Name = "ultraFormattedLinkLabel15";
			ultraFormattedLinkLabel15.Size = new System.Drawing.Size(32, 14);
			ultraFormattedLinkLabel15.TabIndex = 100;
			ultraFormattedLinkLabel15.TabStop = true;
			ultraFormattedLinkLabel15.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel15.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel15.Value = "Type:";
			appearance233.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel15.VisitedLinkAppearance = appearance233;
			ultraFormattedLinkLabel15.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel15_LinkClicked);
			comboBoxVehicleModel.Assigned = false;
			comboBoxVehicleModel.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVehicleModel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVehicleModel.CustomReportFieldName = "";
			comboBoxVehicleModel.CustomReportKey = "";
			comboBoxVehicleModel.CustomReportValueType = 1;
			comboBoxVehicleModel.DescriptionTextBox = null;
			appearance234.BackColor = System.Drawing.SystemColors.Window;
			appearance234.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVehicleModel.DisplayLayout.Appearance = appearance234;
			comboBoxVehicleModel.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVehicleModel.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance235.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance235.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance235.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance235.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleModel.DisplayLayout.GroupByBox.Appearance = appearance235;
			appearance236.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicleModel.DisplayLayout.GroupByBox.BandLabelAppearance = appearance236;
			comboBoxVehicleModel.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance237.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance237.BackColor2 = System.Drawing.SystemColors.Control;
			appearance237.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance237.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicleModel.DisplayLayout.GroupByBox.PromptAppearance = appearance237;
			comboBoxVehicleModel.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVehicleModel.DisplayLayout.MaxRowScrollRegions = 1;
			appearance238.BackColor = System.Drawing.SystemColors.Window;
			appearance238.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVehicleModel.DisplayLayout.Override.ActiveCellAppearance = appearance238;
			appearance239.BackColor = System.Drawing.SystemColors.Highlight;
			appearance239.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVehicleModel.DisplayLayout.Override.ActiveRowAppearance = appearance239;
			comboBoxVehicleModel.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVehicleModel.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance240.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleModel.DisplayLayout.Override.CardAreaAppearance = appearance240;
			appearance241.BorderColor = System.Drawing.Color.Silver;
			appearance241.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVehicleModel.DisplayLayout.Override.CellAppearance = appearance241;
			comboBoxVehicleModel.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVehicleModel.DisplayLayout.Override.CellPadding = 0;
			appearance242.BackColor = System.Drawing.SystemColors.Control;
			appearance242.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance242.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance242.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance242.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleModel.DisplayLayout.Override.GroupByRowAppearance = appearance242;
			appearance243.TextHAlignAsString = "Left";
			comboBoxVehicleModel.DisplayLayout.Override.HeaderAppearance = appearance243;
			comboBoxVehicleModel.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVehicleModel.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance244.BackColor = System.Drawing.SystemColors.Window;
			appearance244.BorderColor = System.Drawing.Color.Silver;
			comboBoxVehicleModel.DisplayLayout.Override.RowAppearance = appearance244;
			comboBoxVehicleModel.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance245.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVehicleModel.DisplayLayout.Override.TemplateAddRowAppearance = appearance245;
			comboBoxVehicleModel.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVehicleModel.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVehicleModel.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVehicleModel.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVehicleModel.Editable = true;
			comboBoxVehicleModel.FilterString = "";
			comboBoxVehicleModel.GenericListType = Micromind.Common.Data.GenericListTypes.VehicleModel;
			comboBoxVehicleModel.HasAllAccount = false;
			comboBoxVehicleModel.HasCustom = false;
			comboBoxVehicleModel.IsDataLoaded = false;
			comboBoxVehicleModel.IsSingleColumn = false;
			comboBoxVehicleModel.Location = new System.Drawing.Point(81, 61);
			comboBoxVehicleModel.MaxDropDownItems = 12;
			comboBoxVehicleModel.Name = "comboBoxVehicleModel";
			comboBoxVehicleModel.ShowInactiveItems = false;
			comboBoxVehicleModel.ShowQuickAdd = true;
			comboBoxVehicleModel.Size = new System.Drawing.Size(142, 20);
			comboBoxVehicleModel.TabIndex = 2;
			comboBoxVehicleModel.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(17, 19);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(34, 14);
			ultraFormattedLinkLabel1.TabIndex = 86;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Make:";
			appearance246.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance246;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ComboBoxVehicleType.Assigned = false;
			ComboBoxVehicleType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxVehicleType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxVehicleType.CustomReportFieldName = "";
			ComboBoxVehicleType.CustomReportKey = "";
			ComboBoxVehicleType.CustomReportValueType = 1;
			ComboBoxVehicleType.DescriptionTextBox = null;
			appearance247.BackColor = System.Drawing.SystemColors.Window;
			appearance247.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxVehicleType.DisplayLayout.Appearance = appearance247;
			ComboBoxVehicleType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxVehicleType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance248.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance248.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance248.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance248.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxVehicleType.DisplayLayout.GroupByBox.Appearance = appearance248;
			appearance249.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxVehicleType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance249;
			ComboBoxVehicleType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance250.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance250.BackColor2 = System.Drawing.SystemColors.Control;
			appearance250.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance250.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxVehicleType.DisplayLayout.GroupByBox.PromptAppearance = appearance250;
			ComboBoxVehicleType.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxVehicleType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance251.BackColor = System.Drawing.SystemColors.Window;
			appearance251.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxVehicleType.DisplayLayout.Override.ActiveCellAppearance = appearance251;
			appearance252.BackColor = System.Drawing.SystemColors.Highlight;
			appearance252.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxVehicleType.DisplayLayout.Override.ActiveRowAppearance = appearance252;
			ComboBoxVehicleType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxVehicleType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance253.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxVehicleType.DisplayLayout.Override.CardAreaAppearance = appearance253;
			appearance254.BorderColor = System.Drawing.Color.Silver;
			appearance254.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxVehicleType.DisplayLayout.Override.CellAppearance = appearance254;
			ComboBoxVehicleType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxVehicleType.DisplayLayout.Override.CellPadding = 0;
			appearance255.BackColor = System.Drawing.SystemColors.Control;
			appearance255.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance255.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance255.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance255.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxVehicleType.DisplayLayout.Override.GroupByRowAppearance = appearance255;
			appearance256.TextHAlignAsString = "Left";
			ComboBoxVehicleType.DisplayLayout.Override.HeaderAppearance = appearance256;
			ComboBoxVehicleType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxVehicleType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance257.BackColor = System.Drawing.SystemColors.Window;
			appearance257.BorderColor = System.Drawing.Color.Silver;
			ComboBoxVehicleType.DisplayLayout.Override.RowAppearance = appearance257;
			ComboBoxVehicleType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance258.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxVehicleType.DisplayLayout.Override.TemplateAddRowAppearance = appearance258;
			ComboBoxVehicleType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxVehicleType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxVehicleType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxVehicleType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxVehicleType.Editable = true;
			ComboBoxVehicleType.FilterString = "";
			ComboBoxVehicleType.GenericListType = Micromind.Common.Data.GenericListTypes.VehicleType;
			ComboBoxVehicleType.HasAllAccount = false;
			ComboBoxVehicleType.HasCustom = false;
			ComboBoxVehicleType.IsDataLoaded = false;
			ComboBoxVehicleType.IsSingleColumn = false;
			ComboBoxVehicleType.Location = new System.Drawing.Point(81, 37);
			ComboBoxVehicleType.MaxDropDownItems = 12;
			ComboBoxVehicleType.Name = "ComboBoxVehicleType";
			ComboBoxVehicleType.ShowInactiveItems = false;
			ComboBoxVehicleType.ShowQuickAdd = true;
			ComboBoxVehicleType.Size = new System.Drawing.Size(142, 20);
			ComboBoxVehicleType.TabIndex = 1;
			ComboBoxVehicleType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(14, 66);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(37, 14);
			ultraFormattedLinkLabel2.TabIndex = 97;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Model:";
			appearance259.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance259;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			tabPageDetails.Controls.Add(ultraGroupBox7);
			tabPageDetails.Controls.Add(ultraGroupBox1);
			tabPageDetails.Controls.Add(textBoxExpenseCode);
			tabPageDetails.Controls.Add(labelExpenseCode);
			tabPageDetails.Controls.Add(comboBoxExpenseCode);
			tabPageDetails.Controls.Add(checkBoxExcludeFromCatalogue);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel7);
			tabPageDetails.Controls.Add(comboBoxPrefVendor);
			tabPageDetails.Controls.Add(mmLabel21);
			tabPageDetails.Controls.Add(textBoxWarranty);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(665, 474);
			ultraGroupBox7.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox7.Controls.Add(ultraFormattedLinkLabel22);
			ultraGroupBox7.Controls.Add(comboBoxTaxGroup);
			ultraGroupBox7.Controls.Add(mmLabel4);
			ultraGroupBox7.Controls.Add(comboBoxTaxOption);
			ultraGroupBox7.Location = new System.Drawing.Point(12, 103);
			ultraGroupBox7.Name = "ultraGroupBox7";
			ultraGroupBox7.Size = new System.Drawing.Size(650, 93);
			ultraGroupBox7.TabIndex = 8;
			ultraGroupBox7.Text = "Tax Details";
			ultraFormattedLinkLabel22.AutoSize = true;
			appearance260.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel22.LinkAppearance = appearance260;
			ultraFormattedLinkLabel22.Location = new System.Drawing.Point(1, 48);
			ultraFormattedLinkLabel22.Name = "ultraFormattedLinkLabel22";
			ultraFormattedLinkLabel22.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel22.TabIndex = 73;
			ultraFormattedLinkLabel22.TabStop = true;
			ultraFormattedLinkLabel22.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel22.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel22.Value = "Tax Group:";
			appearance261.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel22.VisitedLinkAppearance = appearance261;
			ultraFormattedLinkLabel22.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel22_LinkClicked);
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance262.BackColor = System.Drawing.SystemColors.Window;
			appearance262.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance262;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance263.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance263.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance263.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance263.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance263;
			appearance264.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance264;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance265.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance265.BackColor2 = System.Drawing.SystemColors.Control;
			appearance265.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance265.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance265;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance266.BackColor = System.Drawing.SystemColors.Window;
			appearance266.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance266;
			appearance267.BackColor = System.Drawing.SystemColors.Highlight;
			appearance267.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance267;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance268.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance268;
			appearance269.BorderColor = System.Drawing.Color.Silver;
			appearance269.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance269;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance270.BackColor = System.Drawing.SystemColors.Control;
			appearance270.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance270.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance270.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance270.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance270;
			appearance271.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance271;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance272.BackColor = System.Drawing.SystemColors.Window;
			appearance272.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance272;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance273.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance273;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(91, 46);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(156, 20);
			comboBoxTaxGroup.TabIndex = 1;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(1, 26);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(64, 13);
			mmLabel4.TabIndex = 70;
			mmLabel4.Text = "Tax Option:";
			comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
			comboBoxTaxOption.FormattingEnabled = true;
			comboBoxTaxOption.Items.AddRange(new object[3]
			{
				"Based on Party",
				"Taxable",
				"Non Taxable"
			});
			comboBoxTaxOption.Location = new System.Drawing.Point(91, 22);
			comboBoxTaxOption.Name = "comboBoxTaxOption";
			comboBoxTaxOption.Size = new System.Drawing.Size(156, 21);
			comboBoxTaxOption.TabIndex = 0;
			comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(comboBoxTaxOption_SelectedIndexChanged);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(ultraExpandableGroupBoxType);
			ultraGroupBox1.Controls.Add(labelAttribute3);
			ultraGroupBox1.Controls.Add(textBoxAttribute3);
			ultraGroupBox1.Controls.Add(labelAttribute2);
			ultraGroupBox1.Controls.Add(textBoxAttribute2);
			ultraGroupBox1.Controls.Add(buttonAccounts);
			ultraGroupBox1.Controls.Add(labelAttribute1);
			ultraGroupBox1.Controls.Add(textBoxAttribute1);
			ultraGroupBox1.Controls.Add(comboBoxColor);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel21);
			ultraGroupBox1.Controls.Add(comboBoxMaterial);
			ultraGroupBox1.Controls.Add(comboBoxStandard);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel17);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel20);
			ultraGroupBox1.Controls.Add(comboBoxFinish);
			ultraGroupBox1.Controls.Add(comboBoxGrade);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel18);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel19);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 202);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(652, 259);
			ultraGroupBox1.TabIndex = 9;
			ultraGroupBox1.Text = "Additional Details";
			ultraExpandableGroupBoxType.Controls.Add(ultraExpandableGroupBoxPanel1);
			ultraExpandableGroupBoxType.ExpandedSize = new System.Drawing.Size(376, 193);
			ultraExpandableGroupBoxType.Location = new System.Drawing.Point(268, 66);
			ultraExpandableGroupBoxType.Name = "ultraExpandableGroupBoxType";
			ultraExpandableGroupBoxType.Size = new System.Drawing.Size(376, 193);
			ultraExpandableGroupBoxType.TabIndex = 108;
			ultraExpandableGroupBoxType.Text = "Product Types";
			ultraExpandableGroupBoxPanel1.Controls.Add(genericListComboBoxType8);
			ultraExpandableGroupBoxPanel1.Controls.Add(linkLabelType8);
			ultraExpandableGroupBoxPanel1.Controls.Add(genericListComboBoxType7);
			ultraExpandableGroupBoxPanel1.Controls.Add(linkLabelType7);
			ultraExpandableGroupBoxPanel1.Controls.Add(genericListComboBoxType6);
			ultraExpandableGroupBoxPanel1.Controls.Add(linkLabelType6);
			ultraExpandableGroupBoxPanel1.Controls.Add(genericListComboBoxType5);
			ultraExpandableGroupBoxPanel1.Controls.Add(linkLabelType5);
			ultraExpandableGroupBoxPanel1.Controls.Add(linkLabelType4);
			ultraExpandableGroupBoxPanel1.Controls.Add(genericListComboBoxType4);
			ultraExpandableGroupBoxPanel1.Controls.Add(genericListComboBoxType3);
			ultraExpandableGroupBoxPanel1.Controls.Add(linkLabelType3);
			ultraExpandableGroupBoxPanel1.Controls.Add(genericListComboBoxType2);
			ultraExpandableGroupBoxPanel1.Controls.Add(linkLabelType2);
			ultraExpandableGroupBoxPanel1.Controls.Add(linkLabelType1);
			ultraExpandableGroupBoxPanel1.Controls.Add(genericListComboBoxType1);
			ultraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraExpandableGroupBoxPanel1.Location = new System.Drawing.Point(3, 19);
			ultraExpandableGroupBoxPanel1.Name = "ultraExpandableGroupBoxPanel1";
			ultraExpandableGroupBoxPanel1.Size = new System.Drawing.Size(370, 171);
			ultraExpandableGroupBoxPanel1.TabIndex = 0;
			genericListComboBoxType8.Assigned = false;
			genericListComboBoxType8.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBoxType8.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBoxType8.CustomReportFieldName = "";
			genericListComboBoxType8.CustomReportKey = "";
			genericListComboBoxType8.CustomReportValueType = 1;
			genericListComboBoxType8.DescriptionTextBox = null;
			appearance274.BackColor = System.Drawing.SystemColors.Window;
			appearance274.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBoxType8.DisplayLayout.Appearance = appearance274;
			genericListComboBoxType8.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBoxType8.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance275.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance275.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance275.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance275.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType8.DisplayLayout.GroupByBox.Appearance = appearance275;
			appearance276.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType8.DisplayLayout.GroupByBox.BandLabelAppearance = appearance276;
			genericListComboBoxType8.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance277.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance277.BackColor2 = System.Drawing.SystemColors.Control;
			appearance277.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance277.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType8.DisplayLayout.GroupByBox.PromptAppearance = appearance277;
			genericListComboBoxType8.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBoxType8.DisplayLayout.MaxRowScrollRegions = 1;
			appearance278.BackColor = System.Drawing.SystemColors.Window;
			appearance278.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBoxType8.DisplayLayout.Override.ActiveCellAppearance = appearance278;
			appearance279.BackColor = System.Drawing.SystemColors.Highlight;
			appearance279.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBoxType8.DisplayLayout.Override.ActiveRowAppearance = appearance279;
			genericListComboBoxType8.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBoxType8.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance280.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType8.DisplayLayout.Override.CardAreaAppearance = appearance280;
			appearance281.BorderColor = System.Drawing.Color.Silver;
			appearance281.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBoxType8.DisplayLayout.Override.CellAppearance = appearance281;
			genericListComboBoxType8.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBoxType8.DisplayLayout.Override.CellPadding = 0;
			appearance282.BackColor = System.Drawing.SystemColors.Control;
			appearance282.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance282.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance282.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance282.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType8.DisplayLayout.Override.GroupByRowAppearance = appearance282;
			appearance283.TextHAlignAsString = "Left";
			genericListComboBoxType8.DisplayLayout.Override.HeaderAppearance = appearance283;
			genericListComboBoxType8.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBoxType8.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance284.BackColor = System.Drawing.SystemColors.Window;
			appearance284.BorderColor = System.Drawing.Color.Silver;
			genericListComboBoxType8.DisplayLayout.Override.RowAppearance = appearance284;
			genericListComboBoxType8.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance285.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBoxType8.DisplayLayout.Override.TemplateAddRowAppearance = appearance285;
			genericListComboBoxType8.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBoxType8.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBoxType8.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBoxType8.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBoxType8.Editable = true;
			genericListComboBoxType8.FilterString = "";
			genericListComboBoxType8.GenericListType = Micromind.Common.Data.GenericListTypes.ProductType8;
			genericListComboBoxType8.HasAllAccount = false;
			genericListComboBoxType8.HasCustom = false;
			genericListComboBoxType8.IsDataLoaded = false;
			genericListComboBoxType8.IsSingleColumn = false;
			genericListComboBoxType8.Location = new System.Drawing.Point(105, 149);
			genericListComboBoxType8.MaxDropDownItems = 12;
			genericListComboBoxType8.Name = "genericListComboBoxType8";
			genericListComboBoxType8.ShowInactiveItems = false;
			genericListComboBoxType8.ShowQuickAdd = true;
			genericListComboBoxType8.Size = new System.Drawing.Size(154, 20);
			genericListComboBoxType8.TabIndex = 122;
			genericListComboBoxType8.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelType8.AutoSize = true;
			linkLabelType8.Location = new System.Drawing.Point(10, 155);
			linkLabelType8.Name = "linkLabelType8";
			linkLabelType8.Size = new System.Drawing.Size(51, 14);
			linkLabelType8.TabIndex = 121;
			linkLabelType8.TabStop = true;
			linkLabelType8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelType8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelType8.Value = "P.Type 8:";
			appearance286.ForeColor = System.Drawing.Color.Blue;
			linkLabelType8.VisitedLinkAppearance = appearance286;
			linkLabelType8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelType8_LinkClicked);
			genericListComboBoxType7.Assigned = false;
			genericListComboBoxType7.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBoxType7.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBoxType7.CustomReportFieldName = "";
			genericListComboBoxType7.CustomReportKey = "";
			genericListComboBoxType7.CustomReportValueType = 1;
			genericListComboBoxType7.DescriptionTextBox = null;
			appearance287.BackColor = System.Drawing.SystemColors.Window;
			appearance287.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBoxType7.DisplayLayout.Appearance = appearance287;
			genericListComboBoxType7.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBoxType7.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance288.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance288.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance288.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance288.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType7.DisplayLayout.GroupByBox.Appearance = appearance288;
			appearance289.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType7.DisplayLayout.GroupByBox.BandLabelAppearance = appearance289;
			genericListComboBoxType7.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance290.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance290.BackColor2 = System.Drawing.SystemColors.Control;
			appearance290.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance290.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType7.DisplayLayout.GroupByBox.PromptAppearance = appearance290;
			genericListComboBoxType7.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBoxType7.DisplayLayout.MaxRowScrollRegions = 1;
			appearance291.BackColor = System.Drawing.SystemColors.Window;
			appearance291.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBoxType7.DisplayLayout.Override.ActiveCellAppearance = appearance291;
			appearance292.BackColor = System.Drawing.SystemColors.Highlight;
			appearance292.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBoxType7.DisplayLayout.Override.ActiveRowAppearance = appearance292;
			genericListComboBoxType7.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBoxType7.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance293.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType7.DisplayLayout.Override.CardAreaAppearance = appearance293;
			appearance294.BorderColor = System.Drawing.Color.Silver;
			appearance294.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBoxType7.DisplayLayout.Override.CellAppearance = appearance294;
			genericListComboBoxType7.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBoxType7.DisplayLayout.Override.CellPadding = 0;
			appearance295.BackColor = System.Drawing.SystemColors.Control;
			appearance295.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance295.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance295.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance295.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType7.DisplayLayout.Override.GroupByRowAppearance = appearance295;
			appearance296.TextHAlignAsString = "Left";
			genericListComboBoxType7.DisplayLayout.Override.HeaderAppearance = appearance296;
			genericListComboBoxType7.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBoxType7.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance297.BackColor = System.Drawing.SystemColors.Window;
			appearance297.BorderColor = System.Drawing.Color.Silver;
			genericListComboBoxType7.DisplayLayout.Override.RowAppearance = appearance297;
			genericListComboBoxType7.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance298.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBoxType7.DisplayLayout.Override.TemplateAddRowAppearance = appearance298;
			genericListComboBoxType7.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBoxType7.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBoxType7.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBoxType7.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBoxType7.Editable = true;
			genericListComboBoxType7.FilterString = "";
			genericListComboBoxType7.GenericListType = Micromind.Common.Data.GenericListTypes.ProductType7;
			genericListComboBoxType7.HasAllAccount = false;
			genericListComboBoxType7.HasCustom = false;
			genericListComboBoxType7.IsDataLoaded = false;
			genericListComboBoxType7.IsSingleColumn = false;
			genericListComboBoxType7.Location = new System.Drawing.Point(105, 128);
			genericListComboBoxType7.MaxDropDownItems = 12;
			genericListComboBoxType7.Name = "genericListComboBoxType7";
			genericListComboBoxType7.ShowInactiveItems = false;
			genericListComboBoxType7.ShowQuickAdd = true;
			genericListComboBoxType7.Size = new System.Drawing.Size(154, 20);
			genericListComboBoxType7.TabIndex = 120;
			genericListComboBoxType7.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelType7.AutoSize = true;
			linkLabelType7.Location = new System.Drawing.Point(10, 134);
			linkLabelType7.Name = "linkLabelType7";
			linkLabelType7.Size = new System.Drawing.Size(51, 14);
			linkLabelType7.TabIndex = 119;
			linkLabelType7.TabStop = true;
			linkLabelType7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelType7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelType7.Value = "P.Type 7:";
			appearance299.ForeColor = System.Drawing.Color.Blue;
			linkLabelType7.VisitedLinkAppearance = appearance299;
			linkLabelType7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelType7_LinkClicked);
			genericListComboBoxType6.Assigned = false;
			genericListComboBoxType6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBoxType6.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBoxType6.CustomReportFieldName = "";
			genericListComboBoxType6.CustomReportKey = "";
			genericListComboBoxType6.CustomReportValueType = 1;
			genericListComboBoxType6.DescriptionTextBox = null;
			appearance300.BackColor = System.Drawing.SystemColors.Window;
			appearance300.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBoxType6.DisplayLayout.Appearance = appearance300;
			genericListComboBoxType6.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBoxType6.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance301.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance301.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance301.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance301.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType6.DisplayLayout.GroupByBox.Appearance = appearance301;
			appearance302.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType6.DisplayLayout.GroupByBox.BandLabelAppearance = appearance302;
			genericListComboBoxType6.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance303.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance303.BackColor2 = System.Drawing.SystemColors.Control;
			appearance303.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance303.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType6.DisplayLayout.GroupByBox.PromptAppearance = appearance303;
			genericListComboBoxType6.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBoxType6.DisplayLayout.MaxRowScrollRegions = 1;
			appearance304.BackColor = System.Drawing.SystemColors.Window;
			appearance304.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBoxType6.DisplayLayout.Override.ActiveCellAppearance = appearance304;
			appearance305.BackColor = System.Drawing.SystemColors.Highlight;
			appearance305.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBoxType6.DisplayLayout.Override.ActiveRowAppearance = appearance305;
			genericListComboBoxType6.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBoxType6.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance306.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType6.DisplayLayout.Override.CardAreaAppearance = appearance306;
			appearance307.BorderColor = System.Drawing.Color.Silver;
			appearance307.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBoxType6.DisplayLayout.Override.CellAppearance = appearance307;
			genericListComboBoxType6.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBoxType6.DisplayLayout.Override.CellPadding = 0;
			appearance308.BackColor = System.Drawing.SystemColors.Control;
			appearance308.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance308.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance308.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance308.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType6.DisplayLayout.Override.GroupByRowAppearance = appearance308;
			appearance309.TextHAlignAsString = "Left";
			genericListComboBoxType6.DisplayLayout.Override.HeaderAppearance = appearance309;
			genericListComboBoxType6.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBoxType6.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance310.BackColor = System.Drawing.SystemColors.Window;
			appearance310.BorderColor = System.Drawing.Color.Silver;
			genericListComboBoxType6.DisplayLayout.Override.RowAppearance = appearance310;
			genericListComboBoxType6.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance311.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBoxType6.DisplayLayout.Override.TemplateAddRowAppearance = appearance311;
			genericListComboBoxType6.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBoxType6.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBoxType6.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBoxType6.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBoxType6.Editable = true;
			genericListComboBoxType6.FilterString = "";
			genericListComboBoxType6.GenericListType = Micromind.Common.Data.GenericListTypes.ProductType6;
			genericListComboBoxType6.HasAllAccount = false;
			genericListComboBoxType6.HasCustom = false;
			genericListComboBoxType6.IsDataLoaded = false;
			genericListComboBoxType6.IsSingleColumn = false;
			genericListComboBoxType6.Location = new System.Drawing.Point(105, 107);
			genericListComboBoxType6.MaxDropDownItems = 12;
			genericListComboBoxType6.Name = "genericListComboBoxType6";
			genericListComboBoxType6.ShowInactiveItems = false;
			genericListComboBoxType6.ShowQuickAdd = true;
			genericListComboBoxType6.Size = new System.Drawing.Size(154, 20);
			genericListComboBoxType6.TabIndex = 118;
			genericListComboBoxType6.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelType6.AutoSize = true;
			linkLabelType6.Location = new System.Drawing.Point(10, 113);
			linkLabelType6.Name = "linkLabelType6";
			linkLabelType6.Size = new System.Drawing.Size(51, 14);
			linkLabelType6.TabIndex = 117;
			linkLabelType6.TabStop = true;
			linkLabelType6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelType6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelType6.Value = "P.Type 6:";
			appearance312.ForeColor = System.Drawing.Color.Blue;
			linkLabelType6.VisitedLinkAppearance = appearance312;
			linkLabelType6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelType6_LinkClicked);
			genericListComboBoxType5.Assigned = false;
			genericListComboBoxType5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBoxType5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBoxType5.CustomReportFieldName = "";
			genericListComboBoxType5.CustomReportKey = "";
			genericListComboBoxType5.CustomReportValueType = 1;
			genericListComboBoxType5.DescriptionTextBox = null;
			appearance313.BackColor = System.Drawing.SystemColors.Window;
			appearance313.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBoxType5.DisplayLayout.Appearance = appearance313;
			genericListComboBoxType5.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBoxType5.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance314.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance314.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance314.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance314.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType5.DisplayLayout.GroupByBox.Appearance = appearance314;
			appearance315.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType5.DisplayLayout.GroupByBox.BandLabelAppearance = appearance315;
			genericListComboBoxType5.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance316.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance316.BackColor2 = System.Drawing.SystemColors.Control;
			appearance316.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance316.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType5.DisplayLayout.GroupByBox.PromptAppearance = appearance316;
			genericListComboBoxType5.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBoxType5.DisplayLayout.MaxRowScrollRegions = 1;
			appearance317.BackColor = System.Drawing.SystemColors.Window;
			appearance317.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBoxType5.DisplayLayout.Override.ActiveCellAppearance = appearance317;
			appearance318.BackColor = System.Drawing.SystemColors.Highlight;
			appearance318.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBoxType5.DisplayLayout.Override.ActiveRowAppearance = appearance318;
			genericListComboBoxType5.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBoxType5.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance319.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType5.DisplayLayout.Override.CardAreaAppearance = appearance319;
			appearance320.BorderColor = System.Drawing.Color.Silver;
			appearance320.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBoxType5.DisplayLayout.Override.CellAppearance = appearance320;
			genericListComboBoxType5.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBoxType5.DisplayLayout.Override.CellPadding = 0;
			appearance321.BackColor = System.Drawing.SystemColors.Control;
			appearance321.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance321.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance321.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance321.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType5.DisplayLayout.Override.GroupByRowAppearance = appearance321;
			appearance322.TextHAlignAsString = "Left";
			genericListComboBoxType5.DisplayLayout.Override.HeaderAppearance = appearance322;
			genericListComboBoxType5.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBoxType5.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance323.BackColor = System.Drawing.SystemColors.Window;
			appearance323.BorderColor = System.Drawing.Color.Silver;
			genericListComboBoxType5.DisplayLayout.Override.RowAppearance = appearance323;
			genericListComboBoxType5.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance324.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBoxType5.DisplayLayout.Override.TemplateAddRowAppearance = appearance324;
			genericListComboBoxType5.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBoxType5.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBoxType5.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBoxType5.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBoxType5.Editable = true;
			genericListComboBoxType5.FilterString = "";
			genericListComboBoxType5.GenericListType = Micromind.Common.Data.GenericListTypes.ProductType5;
			genericListComboBoxType5.HasAllAccount = false;
			genericListComboBoxType5.HasCustom = false;
			genericListComboBoxType5.IsDataLoaded = false;
			genericListComboBoxType5.IsSingleColumn = false;
			genericListComboBoxType5.Location = new System.Drawing.Point(105, 86);
			genericListComboBoxType5.MaxDropDownItems = 12;
			genericListComboBoxType5.Name = "genericListComboBoxType5";
			genericListComboBoxType5.ShowInactiveItems = false;
			genericListComboBoxType5.ShowQuickAdd = true;
			genericListComboBoxType5.Size = new System.Drawing.Size(154, 20);
			genericListComboBoxType5.TabIndex = 116;
			genericListComboBoxType5.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelType5.AutoSize = true;
			linkLabelType5.Location = new System.Drawing.Point(10, 92);
			linkLabelType5.Name = "linkLabelType5";
			linkLabelType5.Size = new System.Drawing.Size(51, 14);
			linkLabelType5.TabIndex = 115;
			linkLabelType5.TabStop = true;
			linkLabelType5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelType5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelType5.Value = "P.Type 5:";
			appearance325.ForeColor = System.Drawing.Color.Blue;
			linkLabelType5.VisitedLinkAppearance = appearance325;
			linkLabelType5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelType5_LinkClicked);
			linkLabelType4.AutoSize = true;
			linkLabelType4.Location = new System.Drawing.Point(10, 70);
			linkLabelType4.Name = "linkLabelType4";
			linkLabelType4.Size = new System.Drawing.Size(51, 14);
			linkLabelType4.TabIndex = 114;
			linkLabelType4.TabStop = true;
			linkLabelType4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelType4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelType4.Value = "P.Type 4:";
			appearance326.ForeColor = System.Drawing.Color.Blue;
			linkLabelType4.VisitedLinkAppearance = appearance326;
			linkLabelType4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelType4_LinkClicked);
			genericListComboBoxType4.Assigned = false;
			genericListComboBoxType4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBoxType4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBoxType4.CustomReportFieldName = "";
			genericListComboBoxType4.CustomReportKey = "";
			genericListComboBoxType4.CustomReportValueType = 1;
			genericListComboBoxType4.DescriptionTextBox = null;
			appearance327.BackColor = System.Drawing.SystemColors.Window;
			appearance327.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBoxType4.DisplayLayout.Appearance = appearance327;
			genericListComboBoxType4.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBoxType4.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance328.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance328.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance328.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance328.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType4.DisplayLayout.GroupByBox.Appearance = appearance328;
			appearance329.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType4.DisplayLayout.GroupByBox.BandLabelAppearance = appearance329;
			genericListComboBoxType4.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance330.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance330.BackColor2 = System.Drawing.SystemColors.Control;
			appearance330.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance330.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType4.DisplayLayout.GroupByBox.PromptAppearance = appearance330;
			genericListComboBoxType4.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBoxType4.DisplayLayout.MaxRowScrollRegions = 1;
			appearance331.BackColor = System.Drawing.SystemColors.Window;
			appearance331.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBoxType4.DisplayLayout.Override.ActiveCellAppearance = appearance331;
			appearance332.BackColor = System.Drawing.SystemColors.Highlight;
			appearance332.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBoxType4.DisplayLayout.Override.ActiveRowAppearance = appearance332;
			genericListComboBoxType4.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBoxType4.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance333.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType4.DisplayLayout.Override.CardAreaAppearance = appearance333;
			appearance334.BorderColor = System.Drawing.Color.Silver;
			appearance334.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBoxType4.DisplayLayout.Override.CellAppearance = appearance334;
			genericListComboBoxType4.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBoxType4.DisplayLayout.Override.CellPadding = 0;
			appearance335.BackColor = System.Drawing.SystemColors.Control;
			appearance335.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance335.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance335.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance335.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType4.DisplayLayout.Override.GroupByRowAppearance = appearance335;
			appearance336.TextHAlignAsString = "Left";
			genericListComboBoxType4.DisplayLayout.Override.HeaderAppearance = appearance336;
			genericListComboBoxType4.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBoxType4.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance337.BackColor = System.Drawing.SystemColors.Window;
			appearance337.BorderColor = System.Drawing.Color.Silver;
			genericListComboBoxType4.DisplayLayout.Override.RowAppearance = appearance337;
			genericListComboBoxType4.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance338.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBoxType4.DisplayLayout.Override.TemplateAddRowAppearance = appearance338;
			genericListComboBoxType4.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBoxType4.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBoxType4.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBoxType4.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBoxType4.Editable = true;
			genericListComboBoxType4.FilterString = "";
			genericListComboBoxType4.GenericListType = Micromind.Common.Data.GenericListTypes.ProductType4;
			genericListComboBoxType4.HasAllAccount = false;
			genericListComboBoxType4.HasCustom = false;
			genericListComboBoxType4.IsDataLoaded = false;
			genericListComboBoxType4.IsSingleColumn = false;
			genericListComboBoxType4.Location = new System.Drawing.Point(105, 65);
			genericListComboBoxType4.MaxDropDownItems = 12;
			genericListComboBoxType4.Name = "genericListComboBoxType4";
			genericListComboBoxType4.ShowInactiveItems = false;
			genericListComboBoxType4.ShowQuickAdd = true;
			genericListComboBoxType4.Size = new System.Drawing.Size(154, 20);
			genericListComboBoxType4.TabIndex = 113;
			genericListComboBoxType4.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			genericListComboBoxType3.Assigned = false;
			genericListComboBoxType3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBoxType3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBoxType3.CustomReportFieldName = "";
			genericListComboBoxType3.CustomReportKey = "";
			genericListComboBoxType3.CustomReportValueType = 1;
			genericListComboBoxType3.DescriptionTextBox = null;
			appearance339.BackColor = System.Drawing.SystemColors.Window;
			appearance339.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBoxType3.DisplayLayout.Appearance = appearance339;
			genericListComboBoxType3.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBoxType3.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance340.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance340.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance340.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance340.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType3.DisplayLayout.GroupByBox.Appearance = appearance340;
			appearance341.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType3.DisplayLayout.GroupByBox.BandLabelAppearance = appearance341;
			genericListComboBoxType3.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance342.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance342.BackColor2 = System.Drawing.SystemColors.Control;
			appearance342.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance342.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType3.DisplayLayout.GroupByBox.PromptAppearance = appearance342;
			genericListComboBoxType3.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBoxType3.DisplayLayout.MaxRowScrollRegions = 1;
			appearance343.BackColor = System.Drawing.SystemColors.Window;
			appearance343.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBoxType3.DisplayLayout.Override.ActiveCellAppearance = appearance343;
			appearance344.BackColor = System.Drawing.SystemColors.Highlight;
			appearance344.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBoxType3.DisplayLayout.Override.ActiveRowAppearance = appearance344;
			genericListComboBoxType3.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBoxType3.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance345.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType3.DisplayLayout.Override.CardAreaAppearance = appearance345;
			appearance346.BorderColor = System.Drawing.Color.Silver;
			appearance346.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBoxType3.DisplayLayout.Override.CellAppearance = appearance346;
			genericListComboBoxType3.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBoxType3.DisplayLayout.Override.CellPadding = 0;
			appearance347.BackColor = System.Drawing.SystemColors.Control;
			appearance347.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance347.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance347.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance347.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType3.DisplayLayout.Override.GroupByRowAppearance = appearance347;
			appearance348.TextHAlignAsString = "Left";
			genericListComboBoxType3.DisplayLayout.Override.HeaderAppearance = appearance348;
			genericListComboBoxType3.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBoxType3.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance349.BackColor = System.Drawing.SystemColors.Window;
			appearance349.BorderColor = System.Drawing.Color.Silver;
			genericListComboBoxType3.DisplayLayout.Override.RowAppearance = appearance349;
			genericListComboBoxType3.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance350.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBoxType3.DisplayLayout.Override.TemplateAddRowAppearance = appearance350;
			genericListComboBoxType3.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBoxType3.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBoxType3.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBoxType3.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBoxType3.Editable = true;
			genericListComboBoxType3.FilterString = "";
			genericListComboBoxType3.GenericListType = Micromind.Common.Data.GenericListTypes.ProductType3;
			genericListComboBoxType3.HasAllAccount = false;
			genericListComboBoxType3.HasCustom = false;
			genericListComboBoxType3.IsDataLoaded = false;
			genericListComboBoxType3.IsSingleColumn = false;
			genericListComboBoxType3.Location = new System.Drawing.Point(105, 44);
			genericListComboBoxType3.MaxDropDownItems = 12;
			genericListComboBoxType3.Name = "genericListComboBoxType3";
			genericListComboBoxType3.ShowInactiveItems = false;
			genericListComboBoxType3.ShowQuickAdd = true;
			genericListComboBoxType3.Size = new System.Drawing.Size(154, 20);
			genericListComboBoxType3.TabIndex = 112;
			genericListComboBoxType3.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelType3.AutoSize = true;
			linkLabelType3.Location = new System.Drawing.Point(10, 50);
			linkLabelType3.Name = "linkLabelType3";
			linkLabelType3.Size = new System.Drawing.Size(51, 14);
			linkLabelType3.TabIndex = 111;
			linkLabelType3.TabStop = true;
			linkLabelType3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelType3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelType3.Value = "P.Type 3:";
			appearance351.ForeColor = System.Drawing.Color.Blue;
			linkLabelType3.VisitedLinkAppearance = appearance351;
			linkLabelType3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelType3_LinkClicked);
			genericListComboBoxType2.Assigned = false;
			genericListComboBoxType2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBoxType2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBoxType2.CustomReportFieldName = "";
			genericListComboBoxType2.CustomReportKey = "";
			genericListComboBoxType2.CustomReportValueType = 1;
			genericListComboBoxType2.DescriptionTextBox = null;
			appearance352.BackColor = System.Drawing.SystemColors.Window;
			appearance352.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBoxType2.DisplayLayout.Appearance = appearance352;
			genericListComboBoxType2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBoxType2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance353.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance353.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance353.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance353.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType2.DisplayLayout.GroupByBox.Appearance = appearance353;
			appearance354.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance354;
			genericListComboBoxType2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance355.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance355.BackColor2 = System.Drawing.SystemColors.Control;
			appearance355.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance355.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType2.DisplayLayout.GroupByBox.PromptAppearance = appearance355;
			genericListComboBoxType2.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBoxType2.DisplayLayout.MaxRowScrollRegions = 1;
			appearance356.BackColor = System.Drawing.SystemColors.Window;
			appearance356.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBoxType2.DisplayLayout.Override.ActiveCellAppearance = appearance356;
			appearance357.BackColor = System.Drawing.SystemColors.Highlight;
			appearance357.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBoxType2.DisplayLayout.Override.ActiveRowAppearance = appearance357;
			genericListComboBoxType2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBoxType2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance358.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType2.DisplayLayout.Override.CardAreaAppearance = appearance358;
			appearance359.BorderColor = System.Drawing.Color.Silver;
			appearance359.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBoxType2.DisplayLayout.Override.CellAppearance = appearance359;
			genericListComboBoxType2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBoxType2.DisplayLayout.Override.CellPadding = 0;
			appearance360.BackColor = System.Drawing.SystemColors.Control;
			appearance360.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance360.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance360.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance360.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType2.DisplayLayout.Override.GroupByRowAppearance = appearance360;
			appearance361.TextHAlignAsString = "Left";
			genericListComboBoxType2.DisplayLayout.Override.HeaderAppearance = appearance361;
			genericListComboBoxType2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBoxType2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance362.BackColor = System.Drawing.SystemColors.Window;
			appearance362.BorderColor = System.Drawing.Color.Silver;
			genericListComboBoxType2.DisplayLayout.Override.RowAppearance = appearance362;
			genericListComboBoxType2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance363.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBoxType2.DisplayLayout.Override.TemplateAddRowAppearance = appearance363;
			genericListComboBoxType2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBoxType2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBoxType2.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBoxType2.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBoxType2.Editable = true;
			genericListComboBoxType2.FilterString = "";
			genericListComboBoxType2.GenericListType = Micromind.Common.Data.GenericListTypes.ProductType2;
			genericListComboBoxType2.HasAllAccount = false;
			genericListComboBoxType2.HasCustom = false;
			genericListComboBoxType2.IsDataLoaded = false;
			genericListComboBoxType2.IsSingleColumn = false;
			genericListComboBoxType2.Location = new System.Drawing.Point(105, 23);
			genericListComboBoxType2.MaxDropDownItems = 12;
			genericListComboBoxType2.Name = "genericListComboBoxType2";
			genericListComboBoxType2.ShowInactiveItems = false;
			genericListComboBoxType2.ShowQuickAdd = true;
			genericListComboBoxType2.Size = new System.Drawing.Size(154, 20);
			genericListComboBoxType2.TabIndex = 110;
			genericListComboBoxType2.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelType2.AutoSize = true;
			linkLabelType2.Location = new System.Drawing.Point(10, 29);
			linkLabelType2.Name = "linkLabelType2";
			linkLabelType2.Size = new System.Drawing.Size(51, 14);
			linkLabelType2.TabIndex = 109;
			linkLabelType2.TabStop = true;
			linkLabelType2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelType2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelType2.Value = "P.Type 2:";
			appearance364.ForeColor = System.Drawing.Color.Blue;
			linkLabelType2.VisitedLinkAppearance = appearance364;
			linkLabelType2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelType2_LinkClicked);
			linkLabelType1.AutoSize = true;
			linkLabelType1.Location = new System.Drawing.Point(10, 6);
			linkLabelType1.Name = "linkLabelType1";
			linkLabelType1.Size = new System.Drawing.Size(51, 14);
			linkLabelType1.TabIndex = 108;
			linkLabelType1.TabStop = true;
			linkLabelType1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelType1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelType1.Value = "P.Type 1:";
			appearance365.ForeColor = System.Drawing.Color.Blue;
			linkLabelType1.VisitedLinkAppearance = appearance365;
			linkLabelType1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelType1_LinkClicked);
			genericListComboBoxType1.Assigned = false;
			genericListComboBoxType1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBoxType1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBoxType1.CustomReportFieldName = "";
			genericListComboBoxType1.CustomReportKey = "";
			genericListComboBoxType1.CustomReportValueType = 1;
			genericListComboBoxType1.DescriptionTextBox = null;
			appearance366.BackColor = System.Drawing.SystemColors.Window;
			appearance366.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBoxType1.DisplayLayout.Appearance = appearance366;
			genericListComboBoxType1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBoxType1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance367.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance367.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance367.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance367.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType1.DisplayLayout.GroupByBox.Appearance = appearance367;
			appearance368.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance368;
			genericListComboBoxType1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance369.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance369.BackColor2 = System.Drawing.SystemColors.Control;
			appearance369.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance369.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBoxType1.DisplayLayout.GroupByBox.PromptAppearance = appearance369;
			genericListComboBoxType1.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBoxType1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance370.BackColor = System.Drawing.SystemColors.Window;
			appearance370.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBoxType1.DisplayLayout.Override.ActiveCellAppearance = appearance370;
			appearance371.BackColor = System.Drawing.SystemColors.Highlight;
			appearance371.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBoxType1.DisplayLayout.Override.ActiveRowAppearance = appearance371;
			genericListComboBoxType1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBoxType1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance372.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType1.DisplayLayout.Override.CardAreaAppearance = appearance372;
			appearance373.BorderColor = System.Drawing.Color.Silver;
			appearance373.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBoxType1.DisplayLayout.Override.CellAppearance = appearance373;
			genericListComboBoxType1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBoxType1.DisplayLayout.Override.CellPadding = 0;
			appearance374.BackColor = System.Drawing.SystemColors.Control;
			appearance374.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance374.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance374.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance374.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBoxType1.DisplayLayout.Override.GroupByRowAppearance = appearance374;
			appearance375.TextHAlignAsString = "Left";
			genericListComboBoxType1.DisplayLayout.Override.HeaderAppearance = appearance375;
			genericListComboBoxType1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBoxType1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance376.BackColor = System.Drawing.SystemColors.Window;
			appearance376.BorderColor = System.Drawing.Color.Silver;
			genericListComboBoxType1.DisplayLayout.Override.RowAppearance = appearance376;
			genericListComboBoxType1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance377.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBoxType1.DisplayLayout.Override.TemplateAddRowAppearance = appearance377;
			genericListComboBoxType1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBoxType1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBoxType1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBoxType1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBoxType1.Editable = true;
			genericListComboBoxType1.FilterString = "";
			genericListComboBoxType1.GenericListType = Micromind.Common.Data.GenericListTypes.ProductType1;
			genericListComboBoxType1.HasAllAccount = false;
			genericListComboBoxType1.HasCustom = false;
			genericListComboBoxType1.IsDataLoaded = false;
			genericListComboBoxType1.IsSingleColumn = false;
			genericListComboBoxType1.Location = new System.Drawing.Point(105, 2);
			genericListComboBoxType1.MaxDropDownItems = 12;
			genericListComboBoxType1.Name = "genericListComboBoxType1";
			genericListComboBoxType1.ShowInactiveItems = false;
			genericListComboBoxType1.ShowQuickAdd = true;
			genericListComboBoxType1.Size = new System.Drawing.Size(154, 20);
			genericListComboBoxType1.TabIndex = 107;
			genericListComboBoxType1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			labelAttribute3.AutoSize = true;
			labelAttribute3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelAttribute3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelAttribute3.IsFieldHeader = false;
			labelAttribute3.IsRequired = false;
			labelAttribute3.Location = new System.Drawing.Point(4, 141);
			labelAttribute3.Name = "labelAttribute3";
			labelAttribute3.PenWidth = 1f;
			labelAttribute3.ShowBorder = false;
			labelAttribute3.Size = new System.Drawing.Size(58, 13);
			labelAttribute3.TabIndex = 99;
			labelAttribute3.Text = "Attribute 3:";
			textBoxAttribute3.BackColor = System.Drawing.Color.White;
			textBoxAttribute3.CustomReportFieldName = "";
			textBoxAttribute3.CustomReportKey = "";
			textBoxAttribute3.CustomReportValueType = 1;
			textBoxAttribute3.IsComboTextBox = false;
			textBoxAttribute3.IsModified = false;
			textBoxAttribute3.Location = new System.Drawing.Point(93, 137);
			textBoxAttribute3.MaxLength = 30;
			textBoxAttribute3.Name = "textBoxAttribute3";
			textBoxAttribute3.Size = new System.Drawing.Size(154, 20);
			textBoxAttribute3.TabIndex = 5;
			labelAttribute2.AutoSize = true;
			labelAttribute2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelAttribute2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelAttribute2.IsFieldHeader = false;
			labelAttribute2.IsRequired = false;
			labelAttribute2.Location = new System.Drawing.Point(4, 118);
			labelAttribute2.Name = "labelAttribute2";
			labelAttribute2.PenWidth = 1f;
			labelAttribute2.ShowBorder = false;
			labelAttribute2.Size = new System.Drawing.Size(58, 13);
			labelAttribute2.TabIndex = 97;
			labelAttribute2.Text = "Attribute 2:";
			textBoxAttribute2.BackColor = System.Drawing.Color.White;
			textBoxAttribute2.CustomReportFieldName = "";
			textBoxAttribute2.CustomReportKey = "";
			textBoxAttribute2.CustomReportValueType = 1;
			textBoxAttribute2.IsComboTextBox = false;
			textBoxAttribute2.IsModified = false;
			textBoxAttribute2.Location = new System.Drawing.Point(93, 114);
			textBoxAttribute2.MaxLength = 30;
			textBoxAttribute2.Name = "textBoxAttribute2";
			textBoxAttribute2.Size = new System.Drawing.Size(154, 20);
			textBoxAttribute2.TabIndex = 4;
			buttonAccounts.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAccounts.BackColor = System.Drawing.Color.DarkGray;
			buttonAccounts.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAccounts.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAccounts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonAccounts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAccounts.Location = new System.Drawing.Point(4, 220);
			buttonAccounts.Name = "buttonAccounts";
			buttonAccounts.Size = new System.Drawing.Size(96, 24);
			buttonAccounts.TabIndex = 10;
			buttonAccounts.Text = "&Accounts...";
			buttonAccounts.UseVisualStyleBackColor = false;
			buttonAccounts.Click += new System.EventHandler(buttonAccounts_Click);
			labelAttribute1.AutoSize = true;
			labelAttribute1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelAttribute1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelAttribute1.IsFieldHeader = false;
			labelAttribute1.IsRequired = false;
			labelAttribute1.Location = new System.Drawing.Point(4, 96);
			labelAttribute1.Name = "labelAttribute1";
			labelAttribute1.PenWidth = 1f;
			labelAttribute1.ShowBorder = false;
			labelAttribute1.Size = new System.Drawing.Size(58, 13);
			labelAttribute1.TabIndex = 95;
			labelAttribute1.Text = "Attribute 1:";
			textBoxAttribute1.BackColor = System.Drawing.Color.White;
			textBoxAttribute1.CustomReportFieldName = "";
			textBoxAttribute1.CustomReportKey = "";
			textBoxAttribute1.CustomReportValueType = 1;
			textBoxAttribute1.IsComboTextBox = false;
			textBoxAttribute1.IsModified = false;
			textBoxAttribute1.Location = new System.Drawing.Point(93, 92);
			textBoxAttribute1.MaxLength = 30;
			textBoxAttribute1.Name = "textBoxAttribute1";
			textBoxAttribute1.Size = new System.Drawing.Size(154, 20);
			textBoxAttribute1.TabIndex = 3;
			comboBoxColor.Assigned = false;
			comboBoxColor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxColor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxColor.CustomReportFieldName = "";
			comboBoxColor.CustomReportKey = "";
			comboBoxColor.CustomReportValueType = 1;
			comboBoxColor.DescriptionTextBox = null;
			appearance378.BackColor = System.Drawing.SystemColors.Window;
			appearance378.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxColor.DisplayLayout.Appearance = appearance378;
			comboBoxColor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxColor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance379.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance379.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance379.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance379.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxColor.DisplayLayout.GroupByBox.Appearance = appearance379;
			appearance380.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxColor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance380;
			comboBoxColor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance381.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance381.BackColor2 = System.Drawing.SystemColors.Control;
			appearance381.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance381.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxColor.DisplayLayout.GroupByBox.PromptAppearance = appearance381;
			comboBoxColor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxColor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance382.BackColor = System.Drawing.SystemColors.Window;
			appearance382.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxColor.DisplayLayout.Override.ActiveCellAppearance = appearance382;
			appearance383.BackColor = System.Drawing.SystemColors.Highlight;
			appearance383.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxColor.DisplayLayout.Override.ActiveRowAppearance = appearance383;
			comboBoxColor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxColor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance384.BackColor = System.Drawing.SystemColors.Window;
			comboBoxColor.DisplayLayout.Override.CardAreaAppearance = appearance384;
			appearance385.BorderColor = System.Drawing.Color.Silver;
			appearance385.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxColor.DisplayLayout.Override.CellAppearance = appearance385;
			comboBoxColor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxColor.DisplayLayout.Override.CellPadding = 0;
			appearance386.BackColor = System.Drawing.SystemColors.Control;
			appearance386.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance386.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance386.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance386.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxColor.DisplayLayout.Override.GroupByRowAppearance = appearance386;
			appearance387.TextHAlignAsString = "Left";
			comboBoxColor.DisplayLayout.Override.HeaderAppearance = appearance387;
			comboBoxColor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxColor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance388.BackColor = System.Drawing.SystemColors.Window;
			appearance388.BorderColor = System.Drawing.Color.Silver;
			comboBoxColor.DisplayLayout.Override.RowAppearance = appearance388;
			comboBoxColor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance389.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxColor.DisplayLayout.Override.TemplateAddRowAppearance = appearance389;
			comboBoxColor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxColor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxColor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxColor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxColor.Editable = true;
			comboBoxColor.FilterString = "";
			comboBoxColor.GenericListType = Micromind.Common.Data.GenericListTypes.Color;
			comboBoxColor.HasAllAccount = false;
			comboBoxColor.HasCustom = false;
			comboBoxColor.IsDataLoaded = false;
			comboBoxColor.IsSingleColumn = false;
			comboBoxColor.Location = new System.Drawing.Point(375, 23);
			comboBoxColor.MaxDropDownItems = 12;
			comboBoxColor.Name = "comboBoxColor";
			comboBoxColor.ShowInactiveItems = false;
			comboBoxColor.ShowQuickAdd = true;
			comboBoxColor.Size = new System.Drawing.Size(155, 20);
			comboBoxColor.TabIndex = 6;
			comboBoxColor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel21.AutoSize = true;
			ultraFormattedLinkLabel21.Location = new System.Drawing.Point(4, 72);
			ultraFormattedLinkLabel21.Name = "ultraFormattedLinkLabel21";
			ultraFormattedLinkLabel21.Size = new System.Drawing.Size(51, 14);
			ultraFormattedLinkLabel21.TabIndex = 93;
			ultraFormattedLinkLabel21.TabStop = true;
			ultraFormattedLinkLabel21.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel21.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel21.Value = "Standard:";
			appearance390.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel21.VisitedLinkAppearance = appearance390;
			ultraFormattedLinkLabel21.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel21_LinkClicked);
			comboBoxMaterial.Assigned = false;
			comboBoxMaterial.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxMaterial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxMaterial.CustomReportFieldName = "";
			comboBoxMaterial.CustomReportKey = "";
			comboBoxMaterial.CustomReportValueType = 1;
			comboBoxMaterial.DescriptionTextBox = null;
			appearance391.BackColor = System.Drawing.SystemColors.Window;
			appearance391.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxMaterial.DisplayLayout.Appearance = appearance391;
			comboBoxMaterial.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxMaterial.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance392.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance392.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance392.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance392.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxMaterial.DisplayLayout.GroupByBox.Appearance = appearance392;
			appearance393.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxMaterial.DisplayLayout.GroupByBox.BandLabelAppearance = appearance393;
			comboBoxMaterial.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance394.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance394.BackColor2 = System.Drawing.SystemColors.Control;
			appearance394.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance394.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxMaterial.DisplayLayout.GroupByBox.PromptAppearance = appearance394;
			comboBoxMaterial.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxMaterial.DisplayLayout.MaxRowScrollRegions = 1;
			appearance395.BackColor = System.Drawing.SystemColors.Window;
			appearance395.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxMaterial.DisplayLayout.Override.ActiveCellAppearance = appearance395;
			appearance396.BackColor = System.Drawing.SystemColors.Highlight;
			appearance396.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxMaterial.DisplayLayout.Override.ActiveRowAppearance = appearance396;
			comboBoxMaterial.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxMaterial.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance397.BackColor = System.Drawing.SystemColors.Window;
			comboBoxMaterial.DisplayLayout.Override.CardAreaAppearance = appearance397;
			appearance398.BorderColor = System.Drawing.Color.Silver;
			appearance398.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxMaterial.DisplayLayout.Override.CellAppearance = appearance398;
			comboBoxMaterial.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxMaterial.DisplayLayout.Override.CellPadding = 0;
			appearance399.BackColor = System.Drawing.SystemColors.Control;
			appearance399.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance399.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance399.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance399.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxMaterial.DisplayLayout.Override.GroupByRowAppearance = appearance399;
			appearance400.TextHAlignAsString = "Left";
			comboBoxMaterial.DisplayLayout.Override.HeaderAppearance = appearance400;
			comboBoxMaterial.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxMaterial.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance401.BackColor = System.Drawing.SystemColors.Window;
			appearance401.BorderColor = System.Drawing.Color.Silver;
			comboBoxMaterial.DisplayLayout.Override.RowAppearance = appearance401;
			comboBoxMaterial.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance402.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxMaterial.DisplayLayout.Override.TemplateAddRowAppearance = appearance402;
			comboBoxMaterial.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxMaterial.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxMaterial.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxMaterial.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxMaterial.Editable = true;
			comboBoxMaterial.FilterString = "";
			comboBoxMaterial.GenericListType = Micromind.Common.Data.GenericListTypes.Material;
			comboBoxMaterial.HasAllAccount = false;
			comboBoxMaterial.HasCustom = false;
			comboBoxMaterial.IsDataLoaded = false;
			comboBoxMaterial.IsSingleColumn = false;
			comboBoxMaterial.Location = new System.Drawing.Point(93, 25);
			comboBoxMaterial.MaxDropDownItems = 12;
			comboBoxMaterial.Name = "comboBoxMaterial";
			comboBoxMaterial.ShowInactiveItems = false;
			comboBoxMaterial.ShowQuickAdd = true;
			comboBoxMaterial.Size = new System.Drawing.Size(154, 20);
			comboBoxMaterial.TabIndex = 0;
			comboBoxMaterial.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxStandard.Assigned = false;
			comboBoxStandard.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxStandard.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStandard.CustomReportFieldName = "";
			comboBoxStandard.CustomReportKey = "";
			comboBoxStandard.CustomReportValueType = 1;
			comboBoxStandard.DescriptionTextBox = null;
			appearance403.BackColor = System.Drawing.SystemColors.Window;
			appearance403.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStandard.DisplayLayout.Appearance = appearance403;
			comboBoxStandard.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStandard.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance404.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance404.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance404.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance404.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStandard.DisplayLayout.GroupByBox.Appearance = appearance404;
			appearance405.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStandard.DisplayLayout.GroupByBox.BandLabelAppearance = appearance405;
			comboBoxStandard.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance406.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance406.BackColor2 = System.Drawing.SystemColors.Control;
			appearance406.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance406.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStandard.DisplayLayout.GroupByBox.PromptAppearance = appearance406;
			comboBoxStandard.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStandard.DisplayLayout.MaxRowScrollRegions = 1;
			appearance407.BackColor = System.Drawing.SystemColors.Window;
			appearance407.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStandard.DisplayLayout.Override.ActiveCellAppearance = appearance407;
			appearance408.BackColor = System.Drawing.SystemColors.Highlight;
			appearance408.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStandard.DisplayLayout.Override.ActiveRowAppearance = appearance408;
			comboBoxStandard.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStandard.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance409.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStandard.DisplayLayout.Override.CardAreaAppearance = appearance409;
			appearance410.BorderColor = System.Drawing.Color.Silver;
			appearance410.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStandard.DisplayLayout.Override.CellAppearance = appearance410;
			comboBoxStandard.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStandard.DisplayLayout.Override.CellPadding = 0;
			appearance411.BackColor = System.Drawing.SystemColors.Control;
			appearance411.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance411.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance411.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance411.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStandard.DisplayLayout.Override.GroupByRowAppearance = appearance411;
			appearance412.TextHAlignAsString = "Left";
			comboBoxStandard.DisplayLayout.Override.HeaderAppearance = appearance412;
			comboBoxStandard.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStandard.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance413.BackColor = System.Drawing.SystemColors.Window;
			appearance413.BorderColor = System.Drawing.Color.Silver;
			comboBoxStandard.DisplayLayout.Override.RowAppearance = appearance413;
			comboBoxStandard.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance414.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStandard.DisplayLayout.Override.TemplateAddRowAppearance = appearance414;
			comboBoxStandard.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStandard.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStandard.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStandard.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStandard.Editable = true;
			comboBoxStandard.FilterString = "";
			comboBoxStandard.GenericListType = Micromind.Common.Data.GenericListTypes.Standard;
			comboBoxStandard.HasAllAccount = false;
			comboBoxStandard.HasCustom = false;
			comboBoxStandard.IsDataLoaded = false;
			comboBoxStandard.IsSingleColumn = false;
			comboBoxStandard.Location = new System.Drawing.Point(93, 69);
			comboBoxStandard.MaxDropDownItems = 12;
			comboBoxStandard.Name = "comboBoxStandard";
			comboBoxStandard.ShowInactiveItems = false;
			comboBoxStandard.ShowQuickAdd = true;
			comboBoxStandard.Size = new System.Drawing.Size(154, 20);
			comboBoxStandard.TabIndex = 2;
			comboBoxStandard.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel17.AutoSize = true;
			ultraFormattedLinkLabel17.Location = new System.Drawing.Point(4, 28);
			ultraFormattedLinkLabel17.Name = "ultraFormattedLinkLabel17";
			ultraFormattedLinkLabel17.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel17.TabIndex = 85;
			ultraFormattedLinkLabel17.TabStop = true;
			ultraFormattedLinkLabel17.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel17.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel17.Value = "Material:";
			appearance415.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel17.VisitedLinkAppearance = appearance415;
			ultraFormattedLinkLabel17.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel17_LinkClicked);
			ultraFormattedLinkLabel20.AutoSize = true;
			ultraFormattedLinkLabel20.Location = new System.Drawing.Point(279, 47);
			ultraFormattedLinkLabel20.Name = "ultraFormattedLinkLabel20";
			ultraFormattedLinkLabel20.Size = new System.Drawing.Size(38, 14);
			ultraFormattedLinkLabel20.TabIndex = 91;
			ultraFormattedLinkLabel20.TabStop = true;
			ultraFormattedLinkLabel20.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel20.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel20.Value = "Grade:";
			appearance416.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel20.VisitedLinkAppearance = appearance416;
			ultraFormattedLinkLabel20.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel20_LinkClicked);
			comboBoxFinish.Assigned = false;
			comboBoxFinish.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFinish.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFinish.CustomReportFieldName = "";
			comboBoxFinish.CustomReportKey = "";
			comboBoxFinish.CustomReportValueType = 1;
			comboBoxFinish.DescriptionTextBox = null;
			appearance417.BackColor = System.Drawing.SystemColors.Window;
			appearance417.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFinish.DisplayLayout.Appearance = appearance417;
			comboBoxFinish.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFinish.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance418.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance418.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance418.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance418.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFinish.DisplayLayout.GroupByBox.Appearance = appearance418;
			appearance419.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFinish.DisplayLayout.GroupByBox.BandLabelAppearance = appearance419;
			comboBoxFinish.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance420.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance420.BackColor2 = System.Drawing.SystemColors.Control;
			appearance420.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance420.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFinish.DisplayLayout.GroupByBox.PromptAppearance = appearance420;
			comboBoxFinish.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFinish.DisplayLayout.MaxRowScrollRegions = 1;
			appearance421.BackColor = System.Drawing.SystemColors.Window;
			appearance421.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFinish.DisplayLayout.Override.ActiveCellAppearance = appearance421;
			appearance422.BackColor = System.Drawing.SystemColors.Highlight;
			appearance422.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFinish.DisplayLayout.Override.ActiveRowAppearance = appearance422;
			comboBoxFinish.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFinish.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance423.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFinish.DisplayLayout.Override.CardAreaAppearance = appearance423;
			appearance424.BorderColor = System.Drawing.Color.Silver;
			appearance424.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFinish.DisplayLayout.Override.CellAppearance = appearance424;
			comboBoxFinish.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFinish.DisplayLayout.Override.CellPadding = 0;
			appearance425.BackColor = System.Drawing.SystemColors.Control;
			appearance425.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance425.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance425.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance425.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFinish.DisplayLayout.Override.GroupByRowAppearance = appearance425;
			appearance426.TextHAlignAsString = "Left";
			comboBoxFinish.DisplayLayout.Override.HeaderAppearance = appearance426;
			comboBoxFinish.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFinish.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance427.BackColor = System.Drawing.SystemColors.Window;
			appearance427.BorderColor = System.Drawing.Color.Silver;
			comboBoxFinish.DisplayLayout.Override.RowAppearance = appearance427;
			comboBoxFinish.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance428.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFinish.DisplayLayout.Override.TemplateAddRowAppearance = appearance428;
			comboBoxFinish.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFinish.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFinish.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFinish.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFinish.Editable = true;
			comboBoxFinish.FilterString = "";
			comboBoxFinish.GenericListType = Micromind.Common.Data.GenericListTypes.Finish;
			comboBoxFinish.HasAllAccount = false;
			comboBoxFinish.HasCustom = false;
			comboBoxFinish.IsDataLoaded = false;
			comboBoxFinish.IsSingleColumn = false;
			comboBoxFinish.Location = new System.Drawing.Point(93, 47);
			comboBoxFinish.MaxDropDownItems = 12;
			comboBoxFinish.Name = "comboBoxFinish";
			comboBoxFinish.ShowInactiveItems = false;
			comboBoxFinish.ShowQuickAdd = true;
			comboBoxFinish.Size = new System.Drawing.Size(154, 20);
			comboBoxFinish.TabIndex = 1;
			comboBoxFinish.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGrade.Assigned = false;
			comboBoxGrade.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGrade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGrade.CustomReportFieldName = "";
			comboBoxGrade.CustomReportKey = "";
			comboBoxGrade.CustomReportValueType = 1;
			comboBoxGrade.DescriptionTextBox = null;
			appearance429.BackColor = System.Drawing.SystemColors.Window;
			appearance429.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGrade.DisplayLayout.Appearance = appearance429;
			comboBoxGrade.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGrade.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance430.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance430.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance430.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance430.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGrade.DisplayLayout.GroupByBox.Appearance = appearance430;
			appearance431.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGrade.DisplayLayout.GroupByBox.BandLabelAppearance = appearance431;
			comboBoxGrade.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance432.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance432.BackColor2 = System.Drawing.SystemColors.Control;
			appearance432.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance432.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGrade.DisplayLayout.GroupByBox.PromptAppearance = appearance432;
			comboBoxGrade.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGrade.DisplayLayout.MaxRowScrollRegions = 1;
			appearance433.BackColor = System.Drawing.SystemColors.Window;
			appearance433.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGrade.DisplayLayout.Override.ActiveCellAppearance = appearance433;
			appearance434.BackColor = System.Drawing.SystemColors.Highlight;
			appearance434.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGrade.DisplayLayout.Override.ActiveRowAppearance = appearance434;
			comboBoxGrade.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGrade.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance435.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGrade.DisplayLayout.Override.CardAreaAppearance = appearance435;
			appearance436.BorderColor = System.Drawing.Color.Silver;
			appearance436.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGrade.DisplayLayout.Override.CellAppearance = appearance436;
			comboBoxGrade.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGrade.DisplayLayout.Override.CellPadding = 0;
			appearance437.BackColor = System.Drawing.SystemColors.Control;
			appearance437.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance437.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance437.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance437.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGrade.DisplayLayout.Override.GroupByRowAppearance = appearance437;
			appearance438.TextHAlignAsString = "Left";
			comboBoxGrade.DisplayLayout.Override.HeaderAppearance = appearance438;
			comboBoxGrade.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGrade.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance439.BackColor = System.Drawing.SystemColors.Window;
			appearance439.BorderColor = System.Drawing.Color.Silver;
			comboBoxGrade.DisplayLayout.Override.RowAppearance = appearance439;
			comboBoxGrade.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance440.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGrade.DisplayLayout.Override.TemplateAddRowAppearance = appearance440;
			comboBoxGrade.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGrade.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGrade.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGrade.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGrade.Editable = true;
			comboBoxGrade.FilterString = "";
			comboBoxGrade.GenericListType = Micromind.Common.Data.GenericListTypes.ProductGrade;
			comboBoxGrade.HasAllAccount = false;
			comboBoxGrade.HasCustom = false;
			comboBoxGrade.IsDataLoaded = false;
			comboBoxGrade.IsSingleColumn = false;
			comboBoxGrade.Location = new System.Drawing.Point(375, 45);
			comboBoxGrade.MaxDropDownItems = 12;
			comboBoxGrade.Name = "comboBoxGrade";
			comboBoxGrade.ShowInactiveItems = false;
			comboBoxGrade.ShowQuickAdd = true;
			comboBoxGrade.Size = new System.Drawing.Size(154, 20);
			comboBoxGrade.TabIndex = 7;
			comboBoxGrade.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel18.AutoSize = true;
			ultraFormattedLinkLabel18.Location = new System.Drawing.Point(4, 50);
			ultraFormattedLinkLabel18.Name = "ultraFormattedLinkLabel18";
			ultraFormattedLinkLabel18.Size = new System.Drawing.Size(37, 14);
			ultraFormattedLinkLabel18.TabIndex = 87;
			ultraFormattedLinkLabel18.TabStop = true;
			ultraFormattedLinkLabel18.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel18.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel18.Value = "Finish:";
			appearance441.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel18.VisitedLinkAppearance = appearance441;
			ultraFormattedLinkLabel18.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel18_LinkClicked);
			ultraFormattedLinkLabel19.AutoSize = true;
			ultraFormattedLinkLabel19.Location = new System.Drawing.Point(279, 25);
			ultraFormattedLinkLabel19.Name = "ultraFormattedLinkLabel19";
			ultraFormattedLinkLabel19.Size = new System.Drawing.Size(34, 14);
			ultraFormattedLinkLabel19.TabIndex = 89;
			ultraFormattedLinkLabel19.TabStop = true;
			ultraFormattedLinkLabel19.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel19.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel19.Value = "Color:";
			appearance442.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel19.VisitedLinkAppearance = appearance442;
			ultraFormattedLinkLabel19.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel19_LinkClicked);
			textBoxExpenseCode.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxExpenseCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxExpenseCode.CustomReportFieldName = "";
			textBoxExpenseCode.CustomReportKey = "";
			textBoxExpenseCode.CustomReportValueType = 1;
			textBoxExpenseCode.IsComboTextBox = false;
			textBoxExpenseCode.IsModified = false;
			textBoxExpenseCode.Location = new System.Drawing.Point(206, 62);
			textBoxExpenseCode.MaxLength = 64;
			textBoxExpenseCode.Name = "textBoxExpenseCode";
			textBoxExpenseCode.ReadOnly = true;
			textBoxExpenseCode.Size = new System.Drawing.Size(213, 20);
			textBoxExpenseCode.TabIndex = 7;
			textBoxExpenseCode.Visible = false;
			labelExpenseCode.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelExpenseCode.AutoSize = true;
			labelExpenseCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelExpenseCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelExpenseCode.IsFieldHeader = false;
			labelExpenseCode.IsRequired = false;
			labelExpenseCode.Location = new System.Drawing.Point(9, 65);
			labelExpenseCode.Name = "labelExpenseCode";
			labelExpenseCode.PenWidth = 1f;
			labelExpenseCode.ShowBorder = false;
			labelExpenseCode.Size = new System.Drawing.Size(79, 13);
			labelExpenseCode.TabIndex = 5;
			labelExpenseCode.Text = "Expense Code:";
			labelExpenseCode.Visible = false;
			comboBoxExpenseCode.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			comboBoxExpenseCode.Assigned = false;
			comboBoxExpenseCode.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxExpenseCode.CustomReportFieldName = "";
			comboBoxExpenseCode.CustomReportKey = "";
			comboBoxExpenseCode.CustomReportValueType = 1;
			comboBoxExpenseCode.DescriptionTextBox = textBoxExpenseCode;
			appearance443.BackColor = System.Drawing.SystemColors.Window;
			appearance443.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxExpenseCode.DisplayLayout.Appearance = appearance443;
			comboBoxExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance444.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance444.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance444.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance444.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance444;
			appearance445.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance445;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance446.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance446.BackColor2 = System.Drawing.SystemColors.Control;
			appearance446.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance446.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance446;
			comboBoxExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance447.BackColor = System.Drawing.SystemColors.Window;
			appearance447.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance447;
			appearance448.BackColor = System.Drawing.SystemColors.Highlight;
			appearance448.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance448;
			comboBoxExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance449.BackColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance449;
			appearance450.BorderColor = System.Drawing.Color.Silver;
			appearance450.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxExpenseCode.DisplayLayout.Override.CellAppearance = appearance450;
			comboBoxExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxExpenseCode.DisplayLayout.Override.CellPadding = 0;
			appearance451.BackColor = System.Drawing.SystemColors.Control;
			appearance451.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance451.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance451.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance451.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance451;
			appearance452.TextHAlignAsString = "Left";
			comboBoxExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance452;
			comboBoxExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance453.BackColor = System.Drawing.SystemColors.Window;
			appearance453.BorderColor = System.Drawing.Color.Silver;
			comboBoxExpenseCode.DisplayLayout.Override.RowAppearance = appearance453;
			comboBoxExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance454.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance454;
			comboBoxExpenseCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxExpenseCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxExpenseCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxExpenseCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxExpenseCode.Editable = true;
			comboBoxExpenseCode.FilterString = "";
			comboBoxExpenseCode.HasAllAccount = false;
			comboBoxExpenseCode.HasCustom = false;
			comboBoxExpenseCode.IsDataLoaded = false;
			comboBoxExpenseCode.Location = new System.Drawing.Point(103, 62);
			comboBoxExpenseCode.MaxDropDownItems = 12;
			comboBoxExpenseCode.Name = "comboBoxExpenseCode";
			comboBoxExpenseCode.ShowInactiveItems = false;
			comboBoxExpenseCode.ShowQuickAdd = true;
			comboBoxExpenseCode.Size = new System.Drawing.Size(100, 20);
			comboBoxExpenseCode.TabIndex = 6;
			comboBoxExpenseCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxExpenseCode.Visible = false;
			checkBoxExcludeFromCatalogue.AutoSize = true;
			checkBoxExcludeFromCatalogue.Location = new System.Drawing.Point(103, 43);
			checkBoxExcludeFromCatalogue.Name = "checkBoxExcludeFromCatalogue";
			checkBoxExcludeFromCatalogue.Size = new System.Drawing.Size(178, 17);
			checkBoxExcludeFromCatalogue.TabIndex = 4;
			checkBoxExcludeFromCatalogue.Text = "Exclude from Product Catalogue";
			checkBoxExcludeFromCatalogue.UseVisualStyleBackColor = true;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(251, 22);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(91, 14);
			ultraFormattedLinkLabel7.TabIndex = 2;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Preferred Vendor:";
			appearance455.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance455;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			comboBoxPrefVendor.Assigned = false;
			comboBoxPrefVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPrefVendor.CustomReportFieldName = "";
			comboBoxPrefVendor.CustomReportKey = "";
			comboBoxPrefVendor.CustomReportValueType = 1;
			comboBoxPrefVendor.DescriptionTextBox = null;
			appearance456.BackColor = System.Drawing.SystemColors.Window;
			appearance456.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPrefVendor.DisplayLayout.Appearance = appearance456;
			comboBoxPrefVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPrefVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance457.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance457.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance457.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance457.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPrefVendor.DisplayLayout.GroupByBox.Appearance = appearance457;
			appearance458.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPrefVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance458;
			comboBoxPrefVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance459.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance459.BackColor2 = System.Drawing.SystemColors.Control;
			appearance459.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance459.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPrefVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance459;
			comboBoxPrefVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPrefVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance460.BackColor = System.Drawing.SystemColors.Window;
			appearance460.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPrefVendor.DisplayLayout.Override.ActiveCellAppearance = appearance460;
			appearance461.BackColor = System.Drawing.SystemColors.Highlight;
			appearance461.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPrefVendor.DisplayLayout.Override.ActiveRowAppearance = appearance461;
			comboBoxPrefVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPrefVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance462.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPrefVendor.DisplayLayout.Override.CardAreaAppearance = appearance462;
			appearance463.BorderColor = System.Drawing.Color.Silver;
			appearance463.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPrefVendor.DisplayLayout.Override.CellAppearance = appearance463;
			comboBoxPrefVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPrefVendor.DisplayLayout.Override.CellPadding = 0;
			appearance464.BackColor = System.Drawing.SystemColors.Control;
			appearance464.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance464.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance464.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance464.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPrefVendor.DisplayLayout.Override.GroupByRowAppearance = appearance464;
			appearance465.TextHAlignAsString = "Left";
			comboBoxPrefVendor.DisplayLayout.Override.HeaderAppearance = appearance465;
			comboBoxPrefVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPrefVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance466.BackColor = System.Drawing.SystemColors.Window;
			appearance466.BorderColor = System.Drawing.Color.Silver;
			comboBoxPrefVendor.DisplayLayout.Override.RowAppearance = appearance466;
			comboBoxPrefVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance467.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPrefVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance467;
			comboBoxPrefVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPrefVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPrefVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPrefVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPrefVendor.Editable = true;
			comboBoxPrefVendor.FilterString = "";
			comboBoxPrefVendor.FilterSysDocID = "";
			comboBoxPrefVendor.HasAll = false;
			comboBoxPrefVendor.HasCustom = false;
			comboBoxPrefVendor.IsDataLoaded = false;
			comboBoxPrefVendor.Location = new System.Drawing.Point(345, 19);
			comboBoxPrefVendor.MaxDropDownItems = 12;
			comboBoxPrefVendor.Name = "comboBoxPrefVendor";
			comboBoxPrefVendor.ShowConsignmentOnly = false;
			comboBoxPrefVendor.ShowQuickAdd = true;
			comboBoxPrefVendor.Size = new System.Drawing.Size(122, 20);
			comboBoxPrefVendor.TabIndex = 3;
			comboBoxPrefVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel21.AutoSize = true;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(9, 22);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(80, 13);
			mmLabel21.TabIndex = 0;
			mmLabel21.Text = "Warranty Days:";
			textBoxWarranty.AllowDecimal = false;
			textBoxWarranty.CustomReportFieldName = "";
			textBoxWarranty.CustomReportKey = "";
			textBoxWarranty.CustomReportValueType = 1;
			textBoxWarranty.IsComboTextBox = false;
			textBoxWarranty.IsModified = false;
			textBoxWarranty.Location = new System.Drawing.Point(103, 19);
			textBoxWarranty.MaxLength = 5;
			textBoxWarranty.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxWarranty.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxWarranty.Name = "textBoxWarranty";
			textBoxWarranty.NullText = "0";
			textBoxWarranty.Size = new System.Drawing.Size(122, 20);
			textBoxWarranty.TabIndex = 1;
			textBoxWarranty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			ultraTabPageControl1.Controls.Add(mmLabel5);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel11);
			ultraTabPageControl1.Controls.Add(comboBoxMainUnitTab);
			ultraTabPageControl1.Controls.Add(comboBoxGridUnit);
			ultraTabPageControl1.Controls.Add(dataGridUnits);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(665, 474);
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(11, 42);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(138, 13);
			mmLabel5.TabIndex = 56;
			mmLabel5.Text = "Unit of measurements table:";
			ultraFormattedLinkLabel11.AutoSize = true;
			ultraFormattedLinkLabel11.Location = new System.Drawing.Point(11, 13);
			ultraFormattedLinkLabel11.Name = "ultraFormattedLinkLabel11";
			ultraFormattedLinkLabel11.Size = new System.Drawing.Size(60, 14);
			ultraFormattedLinkLabel11.TabIndex = 55;
			ultraFormattedLinkLabel11.TabStop = true;
			ultraFormattedLinkLabel11.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel11.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel11.Value = "Main UOM:";
			appearance468.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel11.VisitedLinkAppearance = appearance468;
			comboBoxMainUnitTab.Assigned = false;
			comboBoxMainUnitTab.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxMainUnitTab.CustomReportFieldName = "";
			comboBoxMainUnitTab.CustomReportKey = "";
			comboBoxMainUnitTab.CustomReportValueType = 1;
			comboBoxMainUnitTab.DescriptionTextBox = null;
			appearance469.BackColor = System.Drawing.SystemColors.Window;
			appearance469.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxMainUnitTab.DisplayLayout.Appearance = appearance469;
			comboBoxMainUnitTab.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxMainUnitTab.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance470.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance470.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance470.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance470.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxMainUnitTab.DisplayLayout.GroupByBox.Appearance = appearance470;
			appearance471.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxMainUnitTab.DisplayLayout.GroupByBox.BandLabelAppearance = appearance471;
			comboBoxMainUnitTab.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance472.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance472.BackColor2 = System.Drawing.SystemColors.Control;
			appearance472.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance472.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxMainUnitTab.DisplayLayout.GroupByBox.PromptAppearance = appearance472;
			comboBoxMainUnitTab.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxMainUnitTab.DisplayLayout.MaxRowScrollRegions = 1;
			appearance473.BackColor = System.Drawing.SystemColors.Window;
			appearance473.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxMainUnitTab.DisplayLayout.Override.ActiveCellAppearance = appearance473;
			appearance474.BackColor = System.Drawing.SystemColors.Highlight;
			appearance474.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxMainUnitTab.DisplayLayout.Override.ActiveRowAppearance = appearance474;
			comboBoxMainUnitTab.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxMainUnitTab.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance475.BackColor = System.Drawing.SystemColors.Window;
			comboBoxMainUnitTab.DisplayLayout.Override.CardAreaAppearance = appearance475;
			appearance476.BorderColor = System.Drawing.Color.Silver;
			appearance476.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxMainUnitTab.DisplayLayout.Override.CellAppearance = appearance476;
			comboBoxMainUnitTab.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxMainUnitTab.DisplayLayout.Override.CellPadding = 0;
			appearance477.BackColor = System.Drawing.SystemColors.Control;
			appearance477.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance477.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance477.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance477.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxMainUnitTab.DisplayLayout.Override.GroupByRowAppearance = appearance477;
			appearance478.TextHAlignAsString = "Left";
			comboBoxMainUnitTab.DisplayLayout.Override.HeaderAppearance = appearance478;
			comboBoxMainUnitTab.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxMainUnitTab.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance479.BackColor = System.Drawing.SystemColors.Window;
			appearance479.BorderColor = System.Drawing.Color.Silver;
			comboBoxMainUnitTab.DisplayLayout.Override.RowAppearance = appearance479;
			comboBoxMainUnitTab.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance480.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxMainUnitTab.DisplayLayout.Override.TemplateAddRowAppearance = appearance480;
			comboBoxMainUnitTab.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxMainUnitTab.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxMainUnitTab.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxMainUnitTab.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxMainUnitTab.Editable = true;
			comboBoxMainUnitTab.FilterString = "";
			comboBoxMainUnitTab.HasAllAccount = false;
			comboBoxMainUnitTab.HasCustom = false;
			comboBoxMainUnitTab.IsDataLoaded = false;
			comboBoxMainUnitTab.Location = new System.Drawing.Point(75, 11);
			comboBoxMainUnitTab.MaxDropDownItems = 12;
			comboBoxMainUnitTab.Name = "comboBoxMainUnitTab";
			comboBoxMainUnitTab.ShowInactiveItems = false;
			comboBoxMainUnitTab.ShowQuickAdd = true;
			comboBoxMainUnitTab.Size = new System.Drawing.Size(162, 20);
			comboBoxMainUnitTab.TabIndex = 54;
			comboBoxMainUnitTab.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxMainUnitTab.SelectedIndexChanged += new System.EventHandler(comboBoxMainUnitTab_SelectedIndexChanged);
			comboBoxGridUnit.Assigned = false;
			comboBoxGridUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridUnit.CustomReportFieldName = "";
			comboBoxGridUnit.CustomReportKey = "";
			comboBoxGridUnit.CustomReportValueType = 1;
			comboBoxGridUnit.DescriptionTextBox = null;
			appearance481.BackColor = System.Drawing.SystemColors.Window;
			appearance481.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridUnit.DisplayLayout.Appearance = appearance481;
			comboBoxGridUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance482.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance482.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance482.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance482.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridUnit.DisplayLayout.GroupByBox.Appearance = appearance482;
			appearance483.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance483;
			comboBoxGridUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance484.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance484.BackColor2 = System.Drawing.SystemColors.Control;
			appearance484.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance484.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance484;
			comboBoxGridUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance485.BackColor = System.Drawing.SystemColors.Window;
			appearance485.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridUnit.DisplayLayout.Override.ActiveCellAppearance = appearance485;
			appearance486.BackColor = System.Drawing.SystemColors.Highlight;
			appearance486.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridUnit.DisplayLayout.Override.ActiveRowAppearance = appearance486;
			comboBoxGridUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance487.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridUnit.DisplayLayout.Override.CardAreaAppearance = appearance487;
			appearance488.BorderColor = System.Drawing.Color.Silver;
			appearance488.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridUnit.DisplayLayout.Override.CellAppearance = appearance488;
			comboBoxGridUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridUnit.DisplayLayout.Override.CellPadding = 0;
			appearance489.BackColor = System.Drawing.SystemColors.Control;
			appearance489.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance489.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance489.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance489.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridUnit.DisplayLayout.Override.GroupByRowAppearance = appearance489;
			appearance490.TextHAlignAsString = "Left";
			comboBoxGridUnit.DisplayLayout.Override.HeaderAppearance = appearance490;
			comboBoxGridUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance491.BackColor = System.Drawing.SystemColors.Window;
			appearance491.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridUnit.DisplayLayout.Override.RowAppearance = appearance491;
			comboBoxGridUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance492.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance492;
			comboBoxGridUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridUnit.Editable = true;
			comboBoxGridUnit.FilterString = "";
			comboBoxGridUnit.HasAllAccount = false;
			comboBoxGridUnit.HasCustom = false;
			comboBoxGridUnit.IsDataLoaded = false;
			comboBoxGridUnit.Location = new System.Drawing.Point(510, 11);
			comboBoxGridUnit.MaxDropDownItems = 12;
			comboBoxGridUnit.Name = "comboBoxGridUnit";
			comboBoxGridUnit.ShowInactiveItems = false;
			comboBoxGridUnit.ShowQuickAdd = true;
			comboBoxGridUnit.Size = new System.Drawing.Size(108, 20);
			comboBoxGridUnit.TabIndex = 1;
			comboBoxGridUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridUnit.Visible = false;
			dataGridUnits.AllowAddNew = false;
			dataGridUnits.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance493.BackColor = System.Drawing.SystemColors.Window;
			appearance493.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridUnits.DisplayLayout.Appearance = appearance493;
			dataGridUnits.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridUnits.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance494.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance494.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance494.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance494.BorderColor = System.Drawing.SystemColors.Window;
			dataGridUnits.DisplayLayout.GroupByBox.Appearance = appearance494;
			appearance495.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridUnits.DisplayLayout.GroupByBox.BandLabelAppearance = appearance495;
			dataGridUnits.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance496.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance496.BackColor2 = System.Drawing.SystemColors.Control;
			appearance496.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance496.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridUnits.DisplayLayout.GroupByBox.PromptAppearance = appearance496;
			dataGridUnits.DisplayLayout.MaxColScrollRegions = 1;
			dataGridUnits.DisplayLayout.MaxRowScrollRegions = 1;
			appearance497.BackColor = System.Drawing.SystemColors.Window;
			appearance497.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridUnits.DisplayLayout.Override.ActiveCellAppearance = appearance497;
			appearance498.BackColor = System.Drawing.SystemColors.Highlight;
			appearance498.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridUnits.DisplayLayout.Override.ActiveRowAppearance = appearance498;
			dataGridUnits.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridUnits.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridUnits.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance499.BackColor = System.Drawing.SystemColors.Window;
			dataGridUnits.DisplayLayout.Override.CardAreaAppearance = appearance499;
			appearance500.BorderColor = System.Drawing.Color.Silver;
			appearance500.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridUnits.DisplayLayout.Override.CellAppearance = appearance500;
			dataGridUnits.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridUnits.DisplayLayout.Override.CellPadding = 0;
			appearance501.BackColor = System.Drawing.SystemColors.Control;
			appearance501.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance501.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance501.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance501.BorderColor = System.Drawing.SystemColors.Window;
			dataGridUnits.DisplayLayout.Override.GroupByRowAppearance = appearance501;
			appearance502.TextHAlignAsString = "Left";
			dataGridUnits.DisplayLayout.Override.HeaderAppearance = appearance502;
			dataGridUnits.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridUnits.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance503.BackColor = System.Drawing.SystemColors.Window;
			appearance503.BorderColor = System.Drawing.Color.Silver;
			dataGridUnits.DisplayLayout.Override.RowAppearance = appearance503;
			dataGridUnits.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance504.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridUnits.DisplayLayout.Override.TemplateAddRowAppearance = appearance504;
			dataGridUnits.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridUnits.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridUnits.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridUnits.IncludeLotItems = false;
			dataGridUnits.LoadLayoutFailed = false;
			dataGridUnits.Location = new System.Drawing.Point(11, 58);
			dataGridUnits.Name = "dataGridUnits";
			dataGridUnits.ShowClearMenu = true;
			dataGridUnits.ShowDeleteMenu = true;
			dataGridUnits.ShowInsertMenu = true;
			dataGridUnits.ShowMoveRowsMenu = true;
			dataGridUnits.Size = new System.Drawing.Size(641, 355);
			dataGridUnits.TabIndex = 0;
			ultraTabPageControl4.Controls.Add(textBoxNote);
			ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(665, 474);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(0, 18);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(662, 452);
			textBoxNote.TabIndex = 54;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(665, 474);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(8, 7);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(660, 406);
			udfEntryGrid.TabIndex = 1;
			ultraTabPageControl3.Controls.Add(unitComboBox1);
			ultraTabPageControl3.Controls.Add(comboBoxGridLocation);
			ultraTabPageControl3.Controls.Add(dataEntryGridPriceList);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(665, 474);
			unitComboBox1.Assigned = false;
			unitComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			unitComboBox1.CustomReportFieldName = "";
			unitComboBox1.CustomReportKey = "";
			unitComboBox1.CustomReportValueType = 1;
			unitComboBox1.DescriptionTextBox = null;
			appearance505.BackColor = System.Drawing.SystemColors.Window;
			appearance505.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			unitComboBox1.DisplayLayout.Appearance = appearance505;
			unitComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			unitComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance506.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance506.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance506.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance506.BorderColor = System.Drawing.SystemColors.Window;
			unitComboBox1.DisplayLayout.GroupByBox.Appearance = appearance506;
			appearance507.ForeColor = System.Drawing.SystemColors.GrayText;
			unitComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance507;
			unitComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance508.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance508.BackColor2 = System.Drawing.SystemColors.Control;
			appearance508.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance508.ForeColor = System.Drawing.SystemColors.GrayText;
			unitComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance508;
			unitComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			unitComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance509.BackColor = System.Drawing.SystemColors.Window;
			appearance509.ForeColor = System.Drawing.SystemColors.ControlText;
			unitComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance509;
			appearance510.BackColor = System.Drawing.SystemColors.Highlight;
			appearance510.ForeColor = System.Drawing.SystemColors.HighlightText;
			unitComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance510;
			unitComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			unitComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance511.BackColor = System.Drawing.SystemColors.Window;
			unitComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance511;
			appearance512.BorderColor = System.Drawing.Color.Silver;
			appearance512.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			unitComboBox1.DisplayLayout.Override.CellAppearance = appearance512;
			unitComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			unitComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance513.BackColor = System.Drawing.SystemColors.Control;
			appearance513.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance513.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance513.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance513.BorderColor = System.Drawing.SystemColors.Window;
			unitComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance513;
			appearance514.TextHAlignAsString = "Left";
			unitComboBox1.DisplayLayout.Override.HeaderAppearance = appearance514;
			unitComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			unitComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance515.BackColor = System.Drawing.SystemColors.Window;
			appearance515.BorderColor = System.Drawing.Color.Silver;
			unitComboBox1.DisplayLayout.Override.RowAppearance = appearance515;
			unitComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance516.BackColor = System.Drawing.SystemColors.ControlLight;
			unitComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance516;
			unitComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			unitComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			unitComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			unitComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			unitComboBox1.Editable = true;
			unitComboBox1.FilterString = "";
			unitComboBox1.HasAllAccount = false;
			unitComboBox1.HasCustom = false;
			unitComboBox1.IsDataLoaded = false;
			unitComboBox1.Location = new System.Drawing.Point(278, 227);
			unitComboBox1.MaxDropDownItems = 12;
			unitComboBox1.Name = "unitComboBox1";
			unitComboBox1.ShowInactiveItems = false;
			unitComboBox1.ShowQuickAdd = true;
			unitComboBox1.Size = new System.Drawing.Size(108, 20);
			unitComboBox1.TabIndex = 3;
			unitComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			unitComboBox1.Visible = false;
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance517.BackColor = System.Drawing.SystemColors.Window;
			appearance517.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance517;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance518.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance518.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance518.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance518.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance518;
			appearance519.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance519;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance520.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance520.BackColor2 = System.Drawing.SystemColors.Control;
			appearance520.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance520.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance520;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance521.BackColor = System.Drawing.SystemColors.Window;
			appearance521.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance521;
			appearance522.BackColor = System.Drawing.SystemColors.Highlight;
			appearance522.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance522;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance523.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance523;
			appearance524.BorderColor = System.Drawing.Color.Silver;
			appearance524.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance524;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance525.BackColor = System.Drawing.SystemColors.Control;
			appearance525.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance525.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance525.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance525.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance525;
			appearance526.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance526;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance527.BackColor = System.Drawing.SystemColors.Window;
			appearance527.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance527;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance528.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance528;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(6, 44);
			comboBoxGridLocation.MaxDropDownItems = 12;
			comboBoxGridLocation.Name = "comboBoxGridLocation";
			comboBoxGridLocation.ShowAll = false;
			comboBoxGridLocation.ShowConsignIn = false;
			comboBoxGridLocation.ShowConsignOut = false;
			comboBoxGridLocation.ShowDefaultLocationOnly = false;
			comboBoxGridLocation.ShowInactiveItems = false;
			comboBoxGridLocation.ShowNormalLocations = true;
			comboBoxGridLocation.ShowPOSOnly = false;
			comboBoxGridLocation.ShowQuickAdd = true;
			comboBoxGridLocation.ShowWarehouseOnly = false;
			comboBoxGridLocation.Size = new System.Drawing.Size(100, 20);
			comboBoxGridLocation.TabIndex = 2;
			comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridLocation.Visible = false;
			dataEntryGridPriceList.AllowAddNew = false;
			dataEntryGridPriceList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance529.BackColor = System.Drawing.SystemColors.Window;
			appearance529.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGridPriceList.DisplayLayout.Appearance = appearance529;
			dataEntryGridPriceList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGridPriceList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance530.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance530.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance530.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance530.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridPriceList.DisplayLayout.GroupByBox.Appearance = appearance530;
			appearance531.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridPriceList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance531;
			dataEntryGridPriceList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance532.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance532.BackColor2 = System.Drawing.SystemColors.Control;
			appearance532.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance532.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridPriceList.DisplayLayout.GroupByBox.PromptAppearance = appearance532;
			dataEntryGridPriceList.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGridPriceList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance533.BackColor = System.Drawing.SystemColors.Window;
			appearance533.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGridPriceList.DisplayLayout.Override.ActiveCellAppearance = appearance533;
			appearance534.BackColor = System.Drawing.SystemColors.Highlight;
			appearance534.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGridPriceList.DisplayLayout.Override.ActiveRowAppearance = appearance534;
			dataEntryGridPriceList.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGridPriceList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGridPriceList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance535.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGridPriceList.DisplayLayout.Override.CardAreaAppearance = appearance535;
			appearance536.BorderColor = System.Drawing.Color.Silver;
			appearance536.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGridPriceList.DisplayLayout.Override.CellAppearance = appearance536;
			dataEntryGridPriceList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGridPriceList.DisplayLayout.Override.CellPadding = 0;
			appearance537.BackColor = System.Drawing.SystemColors.Control;
			appearance537.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance537.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance537.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance537.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridPriceList.DisplayLayout.Override.GroupByRowAppearance = appearance537;
			appearance538.TextHAlignAsString = "Left";
			dataEntryGridPriceList.DisplayLayout.Override.HeaderAppearance = appearance538;
			dataEntryGridPriceList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGridPriceList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance539.BackColor = System.Drawing.SystemColors.Window;
			appearance539.BorderColor = System.Drawing.Color.Silver;
			dataEntryGridPriceList.DisplayLayout.Override.RowAppearance = appearance539;
			dataEntryGridPriceList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance540.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGridPriceList.DisplayLayout.Override.TemplateAddRowAppearance = appearance540;
			dataEntryGridPriceList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGridPriceList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGridPriceList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGridPriceList.IncludeLotItems = false;
			dataEntryGridPriceList.LoadLayoutFailed = false;
			dataEntryGridPriceList.Location = new System.Drawing.Point(1, 3);
			dataEntryGridPriceList.Name = "dataEntryGridPriceList";
			dataEntryGridPriceList.ShowClearMenu = true;
			dataEntryGridPriceList.ShowDeleteMenu = true;
			dataEntryGridPriceList.ShowInsertMenu = true;
			dataEntryGridPriceList.ShowMoveRowsMenu = true;
			dataEntryGridPriceList.Size = new System.Drawing.Size(661, 467);
			dataEntryGridPriceList.TabIndex = 1;
			ultraTabPageControl5.Controls.Add(ultraGroupBox2);
			ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(665, 474);
			ultraGroupBox2.Controls.Add(mmLabel17);
			ultraGroupBox2.Controls.Add(textBoxSubstring);
			ultraGroupBox2.Controls.Add(groupBox6);
			ultraGroupBox2.Controls.Add(groupBox5);
			ultraGroupBox2.Location = new System.Drawing.Point(33, 26);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(590, 242);
			ultraGroupBox2.TabIndex = 0;
			ultraGroupBox2.Text = "Settings";
			mmLabel17.AutoSize = true;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(16, 204);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(56, 13);
			mmLabel17.TabIndex = 134;
			mmLabel17.Text = "SubString:";
			textBoxSubstring.AllowDecimal = true;
			textBoxSubstring.CustomReportFieldName = "";
			textBoxSubstring.CustomReportKey = "";
			textBoxSubstring.CustomReportValueType = 1;
			textBoxSubstring.IsComboTextBox = false;
			textBoxSubstring.IsModified = false;
			textBoxSubstring.Location = new System.Drawing.Point(77, 203);
			textBoxSubstring.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSubstring.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSubstring.Name = "textBoxSubstring";
			textBoxSubstring.NullText = "0";
			textBoxSubstring.Size = new System.Drawing.Size(66, 20);
			textBoxSubstring.TabIndex = 133;
			groupBox6.Controls.Add(listBoxSelectedFields);
			groupBox6.Location = new System.Drawing.Point(304, 19);
			groupBox6.Name = "groupBox6";
			groupBox6.Size = new System.Drawing.Size(266, 165);
			groupBox6.TabIndex = 132;
			groupBox6.TabStop = false;
			groupBox6.Text = "Selected Fields";
			listBoxSelectedFields.FormattingEnabled = true;
			listBoxSelectedFields.Location = new System.Drawing.Point(4, 20);
			listBoxSelectedFields.Name = "listBoxSelectedFields";
			listBoxSelectedFields.Size = new System.Drawing.Size(256, 134);
			listBoxSelectedFields.TabIndex = 129;
			groupBox5.Controls.Add(checkedListBoxFields);
			groupBox5.Location = new System.Drawing.Point(6, 19);
			groupBox5.Name = "groupBox5";
			groupBox5.Size = new System.Drawing.Size(271, 165);
			groupBox5.TabIndex = 130;
			groupBox5.TabStop = false;
			groupBox5.Text = "Value Fields";
			checkedListBoxFields.FormattingEnabled = true;
			checkedListBoxFields.Location = new System.Drawing.Point(2, 19);
			checkedListBoxFields.Name = "checkedListBoxFields";
			checkedListBoxFields.Size = new System.Drawing.Size(250, 139);
			checkedListBoxFields.TabIndex = 146;
			checkedListBoxFields.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(checkedListBoxFields_ItemCheck);
			checkedListBoxFields.SelectedIndexChanged += new System.EventHandler(checkedListBoxFields_SelectedIndexChanged);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
			{
				toolStripSeparator6,
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButtonOpenList,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonDuplicate,
				toolStripButtonShowPicture,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(669, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDuplicate.Image = Micromind.ClientUI.Properties.Resources.duplicate1;
			toolStripButtonDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDuplicate.Name = "toolStripButtonDuplicate";
			toolStripButtonDuplicate.Size = new System.Drawing.Size(85, 28);
			toolStripButtonDuplicate.Text = "Duplicate";
			toolStripButtonDuplicate.Click += new System.EventHandler(toolStripButtonDuplicate_Click);
			toolStripButtonShowPicture.CheckOnClick = true;
			toolStripButtonShowPicture.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonShowPicture.Image");
			toolStripButtonShowPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowPicture.Name = "toolStripButtonShowPicture";
			toolStripButtonShowPicture.Size = new System.Drawing.Size(104, 28);
			toolStripButtonShowPicture.Text = "Show Picture";
			toolStripButtonShowPicture.ToolTipText = "Auto load pictures";
			toolStripButtonShowPicture.Click += new System.EventHandler(toolStripButtonShowPicture_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				importToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 19);
			toolStripDropDownButton1.Text = "Actions";
			importToolStripMenuItem.Name = "importToolStripMenuItem";
			importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			importToolStripMenuItem.Text = "Import";
			importToolStripMenuItem.Click += new System.EventHandler(importToolStripMenuItem_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 557);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(669, 40);
			panelButtons.TabIndex = 0;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(669, 1);
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
			xpButton1.Location = new System.Drawing.Point(559, 8);
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
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Controls.Add(ultraTabPageControl4);
			ultraTabControl1.Controls.Add(ultraTabPageControl5);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 60);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(669, 497);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance541.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance541;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "Parts";
			ultraTab2.Visible = false;
			ultraTab3.TabPage = tabPageDetails;
			ultraTab3.Text = "&Details";
			ultraTab4.TabPage = ultraTabPageControl1;
			ultraTab4.Text = "Units of Measure";
			ultraTab5.TabPage = ultraTabPageControl4;
			ultraTab5.Text = "Note";
			ultraTab6.TabPage = tabPageUserDefined;
			ultraTab6.Text = "&User Defined";
			ultraTab7.TabPage = ultraTabPageControl3;
			ultraTab7.Text = "Price List";
			ultraTab8.TabPage = ultraTabPageControl5;
			ultraTab8.Text = "Settings";
			ultraTab8.Visible = false;
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[8]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7,
				ultraTab8
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(665, 474);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MaximumSize = new System.Drawing.Size(0, 29);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(669, 29);
			panel1.TabIndex = 315;
			labelCustomerNameHeader.AutoSize = true;
			labelCustomerNameHeader.BackColor = System.Drawing.Color.Transparent;
			labelCustomerNameHeader.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCustomerNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCustomerNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCustomerNameHeader.IsFieldHeader = false;
			labelCustomerNameHeader.IsRequired = true;
			labelCustomerNameHeader.Location = new System.Drawing.Point(32, 8);
			labelCustomerNameHeader.Name = "labelCustomerNameHeader";
			labelCustomerNameHeader.PenWidth = 1f;
			labelCustomerNameHeader.ShowBorder = false;
			labelCustomerNameHeader.Size = new System.Drawing.Size(0, 13);
			labelCustomerNameHeader.TabIndex = 3;
			labelCustomerNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 25);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(669, 597);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(panel1);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ProductDetailsForm";
			Text = "Item Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOrigin).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBrand).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManufacturer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBOM).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUOM).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxClass).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			groupBox4.ResumeLayout(false);
			groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxgridVehicleType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVehicleMake).EndInit();
			((System.ComponentModel.ISupportInitialize)datagridAppliedModels).EndInit();
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSubstiProduct).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridSubstituteItems).EndInit();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPartsFamily).EndInit();
			((System.ComponentModel.ISupportInitialize)comboboxPartsMakeType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPartsType).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicleMake).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicleModel).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxVehicleType).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).EndInit();
			ultraGroupBox7.ResumeLayout(false);
			ultraGroupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraExpandableGroupBoxType).EndInit();
			ultraExpandableGroupBoxType.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType8).EndInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType7).EndInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType6).EndInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType5).EndInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType4).EndInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType3).EndInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType2).EndInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBoxType1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxColor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxMaterial).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStandard).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFinish).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGrade).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxExpenseCode).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPrefVendor).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxMainUnitTab).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridUnits).EndInit();
			ultraTabPageControl4.ResumeLayout(false);
			ultraTabPageControl4.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)unitComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridPriceList).EndInit();
			ultraTabPageControl5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			groupBox6.ResumeLayout(false);
			groupBox5.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
