using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ProductSelector : UserControl, ICustomReportControl
	{
		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private bool onlyAssemblyItem;

		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private RadioButton radioButtonClass;

		private RadioButton radioButtonCategory;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private RadioButton radioButtonSingle;

		private ProductComboBox comboBoxSingleItem;

		private ProductComboBox comboBoxFromItem;

		private ProductComboBox comboBoxToItem;

		private ItemClassComboBox comboBoxFromClass;

		private ItemClassComboBox comboBoxToClass;

		private ProductCategoryComboBox comboBoxFromCategory;

		private ProductCategoryComboBox comboBoxToCategory;

		private Label label6;

		private Label label7;

		private RadioButton radioButtonBrand;

		private ProductBrandComboBox comboBoxFromBrand;

		private ProductBrandComboBox comboBoxToBrand;

		private ProductManufacturerComboBox comboBoxToManufacturer;

		private ProductManufacturerComboBox comboBoxFromManufacturer;

		private Label label8;

		private Label label9;

		private RadioButton radioButtonManufacturer;

		private CountryComboBox comboBoxToOrigin;

		private CountryComboBox comboBoxFromOrigin;

		private Label label10;

		private Label label11;

		private RadioButton radioButtonOrigin;

		private ProductStyleComboBox comboBoxToStyle;

		private ProductStyleComboBox comboBoxFromStyle;

		private Label label12;

		private Label label13;

		private RadioButton radioButtonStyle;

		public string CustomReportFieldName
		{
			get
			{
				return crFieldName;
			}
			set
			{
				crFieldName = value;
			}
		}

		public string CustomReportKey
		{
			get
			{
				return crKey;
			}
			set
			{
				crKey = value;
			}
		}

		public byte CustomReportValueType
		{
			get
			{
				return crValueType;
			}
			set
			{
				crValueType = value;
			}
		}

		public bool ShowOnlyAssemlbyItems
		{
			get
			{
				return onlyAssemblyItem;
			}
			set
			{
				onlyAssemblyItem = value;
				comboBoxSingleItem.ShowOnlyAssemblyItems = value;
			}
		}

		public string FromItem
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleItem.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromItem.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonSingle.Checked)
				{
					comboBoxSingleItem.SelectedID = value;
				}
				else if (radioButtonRange.Checked)
				{
					comboBoxFromItem.SelectedID = value;
				}
			}
		}

		public string FromItemName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleItem.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromItem.SelectedName;
				}
				return "";
			}
		}

		public string ToItem
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleItem.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToItem.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonSingle.Checked)
				{
					comboBoxSingleItem.SelectedID = value;
				}
				else if (radioButtonRange.Checked)
				{
					comboBoxToItem.SelectedID = value;
				}
			}
		}

		public string ToItemName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleItem.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToItem.SelectedName;
				}
				return "";
			}
		}

		public string FromClass
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxFromClass.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonClass.Checked)
				{
					comboBoxFromClass.SelectedID = value;
				}
			}
		}

		public string FromClassName
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxFromClass.SelectedName;
				}
				return "";
			}
		}

		public string ToClass
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxToClass.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonClass.Checked)
				{
					comboBoxToClass.SelectedID = value;
				}
			}
		}

		public string ToClassName
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxToClass.SelectedName;
				}
				return "";
			}
		}

		public string FromCategory
		{
			get
			{
				if (radioButtonCategory.Checked)
				{
					return comboBoxFromCategory.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonCategory.Checked)
				{
					comboBoxFromCategory.SelectedID = value;
				}
			}
		}

		public string FromCategoryName
		{
			get
			{
				if (radioButtonCategory.Checked)
				{
					return comboBoxFromCategory.SelectedName;
				}
				return "";
			}
		}

		public string ToCategory
		{
			get
			{
				if (radioButtonCategory.Checked)
				{
					return comboBoxToCategory.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonCategory.Checked)
				{
					comboBoxToCategory.SelectedID = value;
				}
			}
		}

		public string ToCategoryName
		{
			get
			{
				if (radioButtonCategory.Checked)
				{
					return comboBoxToCategory.SelectedName;
				}
				return "";
			}
		}

		public string FromBrand
		{
			get
			{
				if (radioButtonBrand.Checked)
				{
					return comboBoxFromBrand.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonBrand.Checked)
				{
					comboBoxFromBrand.SelectedID = value;
				}
			}
		}

		public string FromBrandName
		{
			get
			{
				if (radioButtonBrand.Checked)
				{
					return comboBoxFromBrand.SelectedName;
				}
				return "";
			}
		}

		public string ToBrand
		{
			get
			{
				if (radioButtonBrand.Checked)
				{
					return comboBoxToBrand.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonBrand.Checked)
				{
					comboBoxToBrand.SelectedID = value;
				}
			}
		}

		public string ToBrandName
		{
			get
			{
				if (radioButtonBrand.Checked)
				{
					return comboBoxToBrand.SelectedName;
				}
				return "";
			}
		}

		public string FromManufacturer
		{
			get
			{
				if (radioButtonManufacturer.Checked)
				{
					return comboBoxFromManufacturer.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonManufacturer.Checked)
				{
					comboBoxFromManufacturer.SelectedID = value;
				}
			}
		}

		public string FromManufacturerName
		{
			get
			{
				if (radioButtonManufacturer.Checked)
				{
					return comboBoxFromManufacturer.SelectedName;
				}
				return "";
			}
		}

		public string ToManufacturer
		{
			get
			{
				if (radioButtonManufacturer.Checked)
				{
					return comboBoxToManufacturer.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonManufacturer.Checked)
				{
					comboBoxToManufacturer.SelectedID = value;
				}
			}
		}

		public string ToManufacturerName
		{
			get
			{
				if (radioButtonManufacturer.Checked)
				{
					return comboBoxToManufacturer.SelectedName;
				}
				return "";
			}
		}

		public string FromStyle
		{
			get
			{
				if (radioButtonStyle.Checked)
				{
					return comboBoxFromStyle.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonStyle.Checked)
				{
					comboBoxFromStyle.SelectedID = value;
				}
			}
		}

		public string FromStyleName
		{
			get
			{
				if (radioButtonStyle.Checked)
				{
					return comboBoxFromStyle.SelectedName;
				}
				return "";
			}
		}

		public string ToStyle
		{
			get
			{
				if (radioButtonStyle.Checked)
				{
					return comboBoxToStyle.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonStyle.Checked)
				{
					comboBoxToStyle.SelectedID = value;
				}
			}
		}

		public string ToStyleName
		{
			get
			{
				if (radioButtonStyle.Checked)
				{
					return comboBoxToStyle.SelectedName;
				}
				return "";
			}
		}

		public string FromOrigin
		{
			get
			{
				if (radioButtonOrigin.Checked)
				{
					return comboBoxFromOrigin.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonOrigin.Checked)
				{
					comboBoxFromOrigin.SelectedID = value;
				}
			}
		}

		public string FromOriginName
		{
			get
			{
				if (radioButtonOrigin.Checked)
				{
					return comboBoxFromOrigin.SelectedName;
				}
				return "";
			}
		}

		public string ToOrigin
		{
			get
			{
				if (radioButtonOrigin.Checked)
				{
					return comboBoxToOrigin.SelectedID;
				}
				return "";
			}
			set
			{
				if (radioButtonOrigin.Checked)
				{
					comboBoxToOrigin.SelectedID = value;
				}
			}
		}

		public string ToOriginName
		{
			get
			{
				if (radioButtonOrigin.Checked)
				{
					return comboBoxToOrigin.SelectedName;
				}
				return "";
			}
		}

		public string SetRadioButton
		{
			set
			{
				switch (value)
				{
				case "radioButtonSingle":
					radioButtonSingle.Checked = true;
					break;
				case "radioButtonRange":
					radioButtonRange.Checked = true;
					break;
				case "radioButtonClass":
					radioButtonClass.Checked = true;
					break;
				case "radioButtonCategory":
					radioButtonCategory.Checked = true;
					break;
				case "radioButtonManufacturer":
					radioButtonManufacturer.Checked = true;
					break;
				case "radioButtonOrigin":
					radioButtonOrigin.Checked = true;
					break;
				case "radioButtonStyle":
					radioButtonStyle.Checked = true;
					break;
				}
			}
		}

		public ProductSelector()
		{
			InitializeComponent();
			base.Load += ProductSelector_Load;
		}

		private void ProductSelector_Load(object sender, EventArgs e)
		{
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT ProductID FROM Product)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleItem.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT ProductID FROM Product WHERE ProductID Between '" + comboBoxFromItem.SelectedID + "' AND '" + comboBoxToItem.SelectedID + "')";
				}
				if (radioButtonClass.Checked)
				{
					return "ANY(SELECT ProductID FROM Product WHERE ClassID Between '" + comboBoxFromClass.SelectedID + "' AND '" + comboBoxToClass.SelectedID + "')";
				}
				if (radioButtonCategory.Checked)
				{
					return "ANY(SELECT ProductID FROM Product WHERE CategoryID Between '" + comboBoxFromCategory.SelectedID + "' AND '" + comboBoxToCategory.SelectedID + "')";
				}
				if (radioButtonBrand.Checked)
				{
					return "ANY(SELECT ProductID FROM Product WHERE BrandID Between '" + comboBoxFromBrand.SelectedID + "' AND '" + comboBoxToBrand.SelectedID + "')";
				}
				if (radioButtonManufacturer.Checked)
				{
					return "ANY(SELECT ProductID FROM Product WHERE ManufacturerID Between '" + comboBoxFromManufacturer.SelectedID + "' AND '" + comboBoxToManufacturer.SelectedID + "')";
				}
				if (radioButtonOrigin.Checked)
				{
					return "ANY(SELECT ProductID FROM Product WHERE Origin Between '" + comboBoxFromOrigin.SelectedID + "' AND '" + comboBoxToOrigin.SelectedID + "')";
				}
				if (radioButtonStyle.Checked)
				{
					return "ANY(SELECT ProductID FROM Product WHERE StyleID Between '" + comboBoxFromStyle.SelectedID + "' AND '" + comboBoxToStyle.SelectedID + "')";
				}
				return "1=1";
			}
			if (crFieldName == "")
			{
				return "''=''";
			}
			if (radioButtonAll.Checked)
			{
				return "''=''";
			}
			if (radioButtonSingle.Checked)
			{
				return crFieldName + " = '" + comboBoxSingleItem.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromItem.SelectedID + "' AND '" + comboBoxToItem.SelectedID + "')";
			}
			if (radioButtonClass.Checked)
			{
				return crFieldName + " = ANY(SELECT ProductID FROM Product WHERE ClassID Between '" + comboBoxFromClass.SelectedID + "' AND '" + comboBoxToClass.SelectedID + "')";
			}
			if (radioButtonCategory.Checked)
			{
				return crFieldName + " = ANY(SELECT ProductID FROM Product WHERE CategoryID Between '" + comboBoxFromCategory.SelectedID + "' AND '" + comboBoxToCategory.SelectedID + "')";
			}
			if (radioButtonBrand.Checked)
			{
				return crFieldName + " = ANY(SELECT ProductID FROM Product WHERE BrandID Between '" + comboBoxFromBrand.SelectedID + "' AND '" + comboBoxToBrand.SelectedID + "')";
			}
			if (radioButtonManufacturer.Checked)
			{
				return crFieldName + " = ANY(SELECT ProductID FROM Product WHERE ManufacturerID Between '" + comboBoxFromManufacturer.SelectedID + "' AND '" + comboBoxToManufacturer.SelectedID + "')";
			}
			if (radioButtonOrigin.Checked)
			{
				return crFieldName + " = ANY(SELECT ProductID FROM Product WHERE Origin Between '" + comboBoxFromOrigin.SelectedID + "' AND '" + comboBoxToOrigin.SelectedID + "')";
			}
			if (radioButtonStyle.Checked)
			{
				return crFieldName + " = ANY(SELECT ProductID FROM Product WHERE StyleID Between '" + comboBoxFromStyle.SelectedID + "' AND '" + comboBoxToStyle.SelectedID + "')";
			}
			return "''=''";
		}

		private void EnableDisableControls()
		{
			comboBoxSingleItem.Enabled = radioButtonSingle.Checked;
			ProductComboBox productComboBox = comboBoxFromItem;
			bool enabled = comboBoxToItem.Enabled = radioButtonRange.Checked;
			productComboBox.Enabled = enabled;
			ProductCategoryComboBox productCategoryComboBox = comboBoxFromCategory;
			enabled = (comboBoxToCategory.Enabled = radioButtonCategory.Checked);
			productCategoryComboBox.Enabled = enabled;
			ItemClassComboBox itemClassComboBox = comboBoxFromClass;
			enabled = (comboBoxToClass.Enabled = radioButtonClass.Checked);
			itemClassComboBox.Enabled = enabled;
			ProductBrandComboBox productBrandComboBox = comboBoxFromBrand;
			enabled = (comboBoxToBrand.Enabled = radioButtonBrand.Checked);
			productBrandComboBox.Enabled = enabled;
			ProductManufacturerComboBox productManufacturerComboBox = comboBoxFromManufacturer;
			enabled = (comboBoxToManufacturer.Enabled = radioButtonManufacturer.Checked);
			productManufacturerComboBox.Enabled = enabled;
			ProductStyleComboBox productStyleComboBox = comboBoxFromStyle;
			enabled = (comboBoxToStyle.Enabled = radioButtonStyle.Checked);
			productStyleComboBox.Enabled = enabled;
			CountryComboBox countryComboBox = comboBoxFromOrigin;
			enabled = (comboBoxToOrigin.Enabled = radioButtonOrigin.Checked);
			countryComboBox.Enabled = enabled;
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
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
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			radioButtonClass = new System.Windows.Forms.RadioButton();
			radioButtonCategory = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			radioButtonBrand = new System.Windows.Forms.RadioButton();
			label8 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			radioButtonManufacturer = new System.Windows.Forms.RadioButton();
			label10 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			radioButtonOrigin = new System.Windows.Forms.RadioButton();
			label12 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			radioButtonStyle = new System.Windows.Forms.RadioButton();
			comboBoxToStyle = new Micromind.DataControls.ProductStyleComboBox();
			comboBoxFromStyle = new Micromind.DataControls.ProductStyleComboBox();
			comboBoxToOrigin = new Micromind.DataControls.CountryComboBox();
			comboBoxFromOrigin = new Micromind.DataControls.CountryComboBox();
			comboBoxToManufacturer = new Micromind.DataControls.ProductManufacturerComboBox();
			comboBoxFromManufacturer = new Micromind.DataControls.ProductManufacturerComboBox();
			comboBoxToBrand = new Micromind.DataControls.ProductBrandComboBox();
			comboBoxFromBrand = new Micromind.DataControls.ProductBrandComboBox();
			comboBoxToCategory = new Micromind.DataControls.ProductCategoryComboBox();
			comboBoxFromCategory = new Micromind.DataControls.ProductCategoryComboBox();
			comboBoxToClass = new Micromind.DataControls.ItemClassComboBox();
			comboBoxFromClass = new Micromind.DataControls.ItemClassComboBox();
			comboBoxToItem = new Micromind.DataControls.ProductComboBox();
			comboBoxFromItem = new Micromind.DataControls.ProductComboBox();
			comboBoxSingleItem = new Micromind.DataControls.ProductComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxToStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToOrigin).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromOrigin).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToManufacturer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromManufacturer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToBrand).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromBrand).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleItem).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(265, 29);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(64, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.Text = "All Items";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 26);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonClass.AutoSize = true;
			radioButtonClass.Location = new System.Drawing.Point(6, 50);
			radioButtonClass.Name = "radioButtonClass";
			radioButtonClass.Size = new System.Drawing.Size(76, 17);
			radioButtonClass.TabIndex = 6;
			radioButtonClass.Text = "Item Class:";
			radioButtonClass.UseVisualStyleBackColor = true;
			radioButtonClass.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonCategory.AutoSize = true;
			radioButtonCategory.Location = new System.Drawing.Point(6, 72);
			radioButtonCategory.Name = "radioButtonCategory";
			radioButtonCategory.Size = new System.Drawing.Size(93, 17);
			radioButtonCategory.TabIndex = 9;
			radioButtonCategory.Text = "Item Category:";
			radioButtonCategory.UseVisualStyleBackColor = true;
			radioButtonCategory.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(265, 75);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(23, 13);
			label1.TabIndex = 11;
			label1.Text = "To:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(265, 52);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 6;
			label2.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(116, 29);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 6;
			label3.Text = "From:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(116, 53);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 6;
			label4.Text = "From:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(116, 75);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 13);
			label5.TabIndex = 11;
			label5.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Checked = true;
			radioButtonSingle.Location = new System.Drawing.Point(100, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.TabStop = true;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(116, 97);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(33, 13);
			label6.TabIndex = 18;
			label6.Text = "From:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(265, 97);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(23, 13);
			label7.TabIndex = 19;
			label7.Text = "To:";
			radioButtonBrand.AutoSize = true;
			radioButtonBrand.Location = new System.Drawing.Point(6, 94);
			radioButtonBrand.Name = "radioButtonBrand";
			radioButtonBrand.Size = new System.Drawing.Size(79, 17);
			radioButtonBrand.TabIndex = 17;
			radioButtonBrand.Text = "Item Brand:";
			radioButtonBrand.UseVisualStyleBackColor = true;
			radioButtonBrand.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(116, 119);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(33, 13);
			label8.TabIndex = 23;
			label8.Text = "From:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(265, 119);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(23, 13);
			label9.TabIndex = 24;
			label9.Text = "To:";
			radioButtonManufacturer.AutoSize = true;
			radioButtonManufacturer.Location = new System.Drawing.Point(6, 116);
			radioButtonManufacturer.Name = "radioButtonManufacturer";
			radioButtonManufacturer.Size = new System.Drawing.Size(91, 17);
			radioButtonManufacturer.TabIndex = 22;
			radioButtonManufacturer.Text = "Manufacturer:";
			radioButtonManufacturer.UseVisualStyleBackColor = true;
			radioButtonManufacturer.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(116, 141);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(33, 13);
			label10.TabIndex = 28;
			label10.Text = "From:";
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(265, 141);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(23, 13);
			label11.TabIndex = 29;
			label11.Text = "To:";
			radioButtonOrigin.AutoSize = true;
			radioButtonOrigin.Location = new System.Drawing.Point(6, 138);
			radioButtonOrigin.Name = "radioButtonOrigin";
			radioButtonOrigin.Size = new System.Drawing.Size(55, 17);
			radioButtonOrigin.TabIndex = 27;
			radioButtonOrigin.Text = "Origin:";
			radioButtonOrigin.UseVisualStyleBackColor = true;
			radioButtonOrigin.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(116, 163);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(33, 13);
			label12.TabIndex = 33;
			label12.Text = "From:";
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(265, 163);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(23, 13);
			label13.TabIndex = 34;
			label13.Text = "To:";
			radioButtonStyle.AutoSize = true;
			radioButtonStyle.Location = new System.Drawing.Point(6, 160);
			radioButtonStyle.Name = "radioButtonStyle";
			radioButtonStyle.Size = new System.Drawing.Size(51, 17);
			radioButtonStyle.TabIndex = 32;
			radioButtonStyle.Text = "Style:";
			radioButtonStyle.UseVisualStyleBackColor = true;
			radioButtonStyle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxToStyle.Assigned = false;
			comboBoxToStyle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToStyle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToStyle.CustomReportFieldName = "";
			comboBoxToStyle.CustomReportKey = "";
			comboBoxToStyle.CustomReportValueType = 1;
			comboBoxToStyle.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToStyle.DisplayLayout.Appearance = appearance;
			comboBoxToStyle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToStyle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToStyle.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToStyle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToStyle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToStyle.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToStyle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToStyle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToStyle.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToStyle.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToStyle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToStyle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToStyle.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToStyle.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToStyle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToStyle.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToStyle.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToStyle.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToStyle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToStyle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToStyle.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToStyle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToStyle.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToStyle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToStyle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToStyle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToStyle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToStyle.Editable = true;
			comboBoxToStyle.Enabled = false;
			comboBoxToStyle.FilterString = "";
			comboBoxToStyle.HasAllAccount = false;
			comboBoxToStyle.HasCustom = false;
			comboBoxToStyle.IsDataLoaded = false;
			comboBoxToStyle.Location = new System.Drawing.Point(294, 159);
			comboBoxToStyle.MaxDropDownItems = 12;
			comboBoxToStyle.Name = "comboBoxToStyle";
			comboBoxToStyle.ShowInactiveItems = false;
			comboBoxToStyle.Size = new System.Drawing.Size(103, 20);
			comboBoxToStyle.TabIndex = 36;
			comboBoxToStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromStyle.Assigned = false;
			comboBoxFromStyle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromStyle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromStyle.CustomReportFieldName = "";
			comboBoxFromStyle.CustomReportKey = "";
			comboBoxFromStyle.CustomReportValueType = 1;
			comboBoxFromStyle.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromStyle.DisplayLayout.Appearance = appearance13;
			comboBoxFromStyle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromStyle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromStyle.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromStyle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromStyle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromStyle.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromStyle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromStyle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromStyle.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromStyle.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromStyle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromStyle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromStyle.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromStyle.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromStyle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromStyle.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromStyle.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromStyle.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromStyle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromStyle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromStyle.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromStyle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromStyle.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromStyle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromStyle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromStyle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromStyle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromStyle.Editable = true;
			comboBoxFromStyle.Enabled = false;
			comboBoxFromStyle.FilterString = "";
			comboBoxFromStyle.HasAllAccount = false;
			comboBoxFromStyle.HasCustom = false;
			comboBoxFromStyle.IsDataLoaded = false;
			comboBoxFromStyle.Location = new System.Drawing.Point(161, 159);
			comboBoxFromStyle.MaxDropDownItems = 12;
			comboBoxFromStyle.Name = "comboBoxFromStyle";
			comboBoxFromStyle.ShowInactiveItems = false;
			comboBoxFromStyle.Size = new System.Drawing.Size(103, 20);
			comboBoxFromStyle.TabIndex = 35;
			comboBoxFromStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToOrigin.Assigned = false;
			comboBoxToOrigin.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToOrigin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToOrigin.CustomReportFieldName = "";
			comboBoxToOrigin.CustomReportKey = "";
			comboBoxToOrigin.CustomReportValueType = 1;
			comboBoxToOrigin.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToOrigin.DisplayLayout.Appearance = appearance25;
			comboBoxToOrigin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToOrigin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToOrigin.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToOrigin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToOrigin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToOrigin.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToOrigin.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToOrigin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToOrigin.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToOrigin.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToOrigin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToOrigin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToOrigin.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToOrigin.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToOrigin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToOrigin.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToOrigin.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToOrigin.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToOrigin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToOrigin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToOrigin.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToOrigin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToOrigin.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToOrigin.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToOrigin.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToOrigin.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToOrigin.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToOrigin.Editable = true;
			comboBoxToOrigin.Enabled = false;
			comboBoxToOrigin.FilterString = "";
			comboBoxToOrigin.HasAllAccount = false;
			comboBoxToOrigin.HasCustom = false;
			comboBoxToOrigin.IsDataLoaded = false;
			comboBoxToOrigin.Location = new System.Drawing.Point(294, 137);
			comboBoxToOrigin.MaxDropDownItems = 12;
			comboBoxToOrigin.Name = "comboBoxToOrigin";
			comboBoxToOrigin.ShowInactiveItems = false;
			comboBoxToOrigin.Size = new System.Drawing.Size(103, 20);
			comboBoxToOrigin.TabIndex = 31;
			comboBoxToOrigin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromOrigin.Assigned = false;
			comboBoxFromOrigin.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromOrigin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromOrigin.CustomReportFieldName = "";
			comboBoxFromOrigin.CustomReportKey = "";
			comboBoxFromOrigin.CustomReportValueType = 1;
			comboBoxFromOrigin.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromOrigin.DisplayLayout.Appearance = appearance37;
			comboBoxFromOrigin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromOrigin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromOrigin.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromOrigin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxFromOrigin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromOrigin.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxFromOrigin.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromOrigin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromOrigin.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromOrigin.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxFromOrigin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromOrigin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromOrigin.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromOrigin.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxFromOrigin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromOrigin.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromOrigin.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxFromOrigin.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxFromOrigin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromOrigin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromOrigin.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxFromOrigin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromOrigin.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxFromOrigin.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromOrigin.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromOrigin.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromOrigin.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromOrigin.Editable = true;
			comboBoxFromOrigin.Enabled = false;
			comboBoxFromOrigin.FilterString = "";
			comboBoxFromOrigin.HasAllAccount = false;
			comboBoxFromOrigin.HasCustom = false;
			comboBoxFromOrigin.IsDataLoaded = false;
			comboBoxFromOrigin.Location = new System.Drawing.Point(161, 137);
			comboBoxFromOrigin.MaxDropDownItems = 12;
			comboBoxFromOrigin.Name = "comboBoxFromOrigin";
			comboBoxFromOrigin.ShowInactiveItems = false;
			comboBoxFromOrigin.Size = new System.Drawing.Size(103, 20);
			comboBoxFromOrigin.TabIndex = 30;
			comboBoxFromOrigin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToManufacturer.Assigned = false;
			comboBoxToManufacturer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToManufacturer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToManufacturer.CustomReportFieldName = "";
			comboBoxToManufacturer.CustomReportKey = "";
			comboBoxToManufacturer.CustomReportValueType = 1;
			comboBoxToManufacturer.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToManufacturer.DisplayLayout.Appearance = appearance49;
			comboBoxToManufacturer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToManufacturer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToManufacturer.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToManufacturer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxToManufacturer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToManufacturer.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxToManufacturer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToManufacturer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToManufacturer.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToManufacturer.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxToManufacturer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToManufacturer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToManufacturer.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToManufacturer.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxToManufacturer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToManufacturer.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToManufacturer.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxToManufacturer.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxToManufacturer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToManufacturer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxToManufacturer.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxToManufacturer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToManufacturer.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxToManufacturer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToManufacturer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToManufacturer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToManufacturer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToManufacturer.Editable = true;
			comboBoxToManufacturer.Enabled = false;
			comboBoxToManufacturer.FilterString = "";
			comboBoxToManufacturer.HasAllAccount = false;
			comboBoxToManufacturer.HasCustom = false;
			comboBoxToManufacturer.IsDataLoaded = false;
			comboBoxToManufacturer.Location = new System.Drawing.Point(294, 115);
			comboBoxToManufacturer.MaxDropDownItems = 12;
			comboBoxToManufacturer.Name = "comboBoxToManufacturer";
			comboBoxToManufacturer.ShowInactiveItems = false;
			comboBoxToManufacturer.Size = new System.Drawing.Size(103, 20);
			comboBoxToManufacturer.TabIndex = 26;
			comboBoxToManufacturer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromManufacturer.Assigned = false;
			comboBoxFromManufacturer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromManufacturer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromManufacturer.CustomReportFieldName = "";
			comboBoxFromManufacturer.CustomReportKey = "";
			comboBoxFromManufacturer.CustomReportValueType = 1;
			comboBoxFromManufacturer.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromManufacturer.DisplayLayout.Appearance = appearance61;
			comboBoxFromManufacturer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromManufacturer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromManufacturer.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromManufacturer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxFromManufacturer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromManufacturer.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxFromManufacturer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromManufacturer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromManufacturer.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromManufacturer.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxFromManufacturer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromManufacturer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromManufacturer.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromManufacturer.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxFromManufacturer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromManufacturer.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromManufacturer.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxFromManufacturer.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxFromManufacturer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromManufacturer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromManufacturer.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxFromManufacturer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromManufacturer.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxFromManufacturer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromManufacturer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromManufacturer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromManufacturer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromManufacturer.Editable = true;
			comboBoxFromManufacturer.Enabled = false;
			comboBoxFromManufacturer.FilterString = "";
			comboBoxFromManufacturer.HasAllAccount = false;
			comboBoxFromManufacturer.HasCustom = false;
			comboBoxFromManufacturer.IsDataLoaded = false;
			comboBoxFromManufacturer.Location = new System.Drawing.Point(161, 115);
			comboBoxFromManufacturer.MaxDropDownItems = 12;
			comboBoxFromManufacturer.Name = "comboBoxFromManufacturer";
			comboBoxFromManufacturer.ShowInactiveItems = false;
			comboBoxFromManufacturer.Size = new System.Drawing.Size(103, 20);
			comboBoxFromManufacturer.TabIndex = 25;
			comboBoxFromManufacturer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToBrand.Assigned = false;
			comboBoxToBrand.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToBrand.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToBrand.CustomReportFieldName = "";
			comboBoxToBrand.CustomReportKey = "";
			comboBoxToBrand.CustomReportValueType = 1;
			comboBoxToBrand.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToBrand.DisplayLayout.Appearance = appearance73;
			comboBoxToBrand.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToBrand.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToBrand.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToBrand.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxToBrand.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToBrand.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxToBrand.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToBrand.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToBrand.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToBrand.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxToBrand.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToBrand.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToBrand.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToBrand.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxToBrand.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToBrand.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToBrand.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxToBrand.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxToBrand.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToBrand.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxToBrand.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxToBrand.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToBrand.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxToBrand.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToBrand.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToBrand.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToBrand.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToBrand.Editable = true;
			comboBoxToBrand.Enabled = false;
			comboBoxToBrand.FilterString = "";
			comboBoxToBrand.HasAllAccount = false;
			comboBoxToBrand.HasCustom = false;
			comboBoxToBrand.IsDataLoaded = false;
			comboBoxToBrand.Location = new System.Drawing.Point(294, 93);
			comboBoxToBrand.MaxDropDownItems = 12;
			comboBoxToBrand.Name = "comboBoxToBrand";
			comboBoxToBrand.ShowInactiveItems = false;
			comboBoxToBrand.Size = new System.Drawing.Size(103, 20);
			comboBoxToBrand.TabIndex = 21;
			comboBoxToBrand.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromBrand.Assigned = false;
			comboBoxFromBrand.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromBrand.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromBrand.CustomReportFieldName = "";
			comboBoxFromBrand.CustomReportKey = "";
			comboBoxFromBrand.CustomReportValueType = 1;
			comboBoxFromBrand.DescriptionTextBox = null;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromBrand.DisplayLayout.Appearance = appearance85;
			comboBoxFromBrand.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromBrand.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromBrand.DisplayLayout.GroupByBox.Appearance = appearance86;
			appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromBrand.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
			comboBoxFromBrand.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance88.BackColor2 = System.Drawing.SystemColors.Control;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromBrand.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
			comboBoxFromBrand.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromBrand.DisplayLayout.MaxRowScrollRegions = 1;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromBrand.DisplayLayout.Override.ActiveCellAppearance = appearance89;
			appearance90.BackColor = System.Drawing.SystemColors.Highlight;
			appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromBrand.DisplayLayout.Override.ActiveRowAppearance = appearance90;
			comboBoxFromBrand.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromBrand.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromBrand.DisplayLayout.Override.CardAreaAppearance = appearance91;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromBrand.DisplayLayout.Override.CellAppearance = appearance92;
			comboBoxFromBrand.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromBrand.DisplayLayout.Override.CellPadding = 0;
			appearance93.BackColor = System.Drawing.SystemColors.Control;
			appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance93.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromBrand.DisplayLayout.Override.GroupByRowAppearance = appearance93;
			appearance94.TextHAlignAsString = "Left";
			comboBoxFromBrand.DisplayLayout.Override.HeaderAppearance = appearance94;
			comboBoxFromBrand.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromBrand.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromBrand.DisplayLayout.Override.RowAppearance = appearance95;
			comboBoxFromBrand.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromBrand.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
			comboBoxFromBrand.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromBrand.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromBrand.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromBrand.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromBrand.Editable = true;
			comboBoxFromBrand.Enabled = false;
			comboBoxFromBrand.FilterString = "";
			comboBoxFromBrand.HasAllAccount = false;
			comboBoxFromBrand.HasCustom = false;
			comboBoxFromBrand.IsDataLoaded = false;
			comboBoxFromBrand.Location = new System.Drawing.Point(161, 93);
			comboBoxFromBrand.MaxDropDownItems = 12;
			comboBoxFromBrand.Name = "comboBoxFromBrand";
			comboBoxFromBrand.ShowInactiveItems = false;
			comboBoxFromBrand.Size = new System.Drawing.Size(103, 20);
			comboBoxFromBrand.TabIndex = 20;
			comboBoxFromBrand.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToCategory.Assigned = false;
			comboBoxToCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToCategory.CustomReportFieldName = "";
			comboBoxToCategory.CustomReportKey = "";
			comboBoxToCategory.CustomReportValueType = 1;
			comboBoxToCategory.DescriptionTextBox = null;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToCategory.DisplayLayout.Appearance = appearance97;
			comboBoxToCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance98.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance98.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCategory.DisplayLayout.GroupByBox.Appearance = appearance98;
			appearance99.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance99;
			comboBoxToCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance100.BackColor2 = System.Drawing.SystemColors.Control;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance100;
			comboBoxToCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToCategory.DisplayLayout.Override.ActiveCellAppearance = appearance101;
			appearance102.BackColor = System.Drawing.SystemColors.Highlight;
			appearance102.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToCategory.DisplayLayout.Override.ActiveRowAppearance = appearance102;
			comboBoxToCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToCategory.DisplayLayout.Override.CardAreaAppearance = appearance103;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			appearance104.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToCategory.DisplayLayout.Override.CellAppearance = appearance104;
			comboBoxToCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToCategory.DisplayLayout.Override.CellPadding = 0;
			appearance105.BackColor = System.Drawing.SystemColors.Control;
			appearance105.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance105.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance105.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCategory.DisplayLayout.Override.GroupByRowAppearance = appearance105;
			appearance106.TextHAlignAsString = "Left";
			comboBoxToCategory.DisplayLayout.Override.HeaderAppearance = appearance106;
			comboBoxToCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.Color.Silver;
			comboBoxToCategory.DisplayLayout.Override.RowAppearance = appearance107;
			comboBoxToCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance108;
			comboBoxToCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToCategory.Editable = true;
			comboBoxToCategory.Enabled = false;
			comboBoxToCategory.FilterString = "";
			comboBoxToCategory.HasAllAccount = false;
			comboBoxToCategory.HasCustom = false;
			comboBoxToCategory.IsDataLoaded = false;
			comboBoxToCategory.Location = new System.Drawing.Point(294, 71);
			comboBoxToCategory.MaxDropDownItems = 12;
			comboBoxToCategory.Name = "comboBoxToCategory";
			comboBoxToCategory.ShowInactiveItems = false;
			comboBoxToCategory.Size = new System.Drawing.Size(103, 20);
			comboBoxToCategory.TabIndex = 16;
			comboBoxToCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromCategory.Assigned = false;
			comboBoxFromCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromCategory.CustomReportFieldName = "";
			comboBoxFromCategory.CustomReportKey = "";
			comboBoxFromCategory.CustomReportValueType = 1;
			comboBoxFromCategory.DescriptionTextBox = null;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromCategory.DisplayLayout.Appearance = appearance109;
			comboBoxFromCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance110.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance110.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCategory.DisplayLayout.GroupByBox.Appearance = appearance110;
			appearance111.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance111;
			comboBoxFromCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance112.BackColor2 = System.Drawing.SystemColors.Control;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance112;
			comboBoxFromCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromCategory.DisplayLayout.Override.ActiveCellAppearance = appearance113;
			appearance114.BackColor = System.Drawing.SystemColors.Highlight;
			appearance114.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromCategory.DisplayLayout.Override.ActiveRowAppearance = appearance114;
			comboBoxFromCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromCategory.DisplayLayout.Override.CardAreaAppearance = appearance115;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			appearance116.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromCategory.DisplayLayout.Override.CellAppearance = appearance116;
			comboBoxFromCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromCategory.DisplayLayout.Override.CellPadding = 0;
			appearance117.BackColor = System.Drawing.SystemColors.Control;
			appearance117.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance117.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance117.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance117.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCategory.DisplayLayout.Override.GroupByRowAppearance = appearance117;
			appearance118.TextHAlignAsString = "Left";
			comboBoxFromCategory.DisplayLayout.Override.HeaderAppearance = appearance118;
			comboBoxFromCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromCategory.DisplayLayout.Override.RowAppearance = appearance119;
			comboBoxFromCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance120;
			comboBoxFromCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromCategory.Editable = true;
			comboBoxFromCategory.Enabled = false;
			comboBoxFromCategory.FilterString = "";
			comboBoxFromCategory.HasAllAccount = false;
			comboBoxFromCategory.HasCustom = false;
			comboBoxFromCategory.IsDataLoaded = false;
			comboBoxFromCategory.Location = new System.Drawing.Point(161, 71);
			comboBoxFromCategory.MaxDropDownItems = 12;
			comboBoxFromCategory.Name = "comboBoxFromCategory";
			comboBoxFromCategory.ShowInactiveItems = false;
			comboBoxFromCategory.Size = new System.Drawing.Size(103, 20);
			comboBoxFromCategory.TabIndex = 16;
			comboBoxFromCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToClass.Assigned = false;
			comboBoxToClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToClass.CustomReportFieldName = "";
			comboBoxToClass.CustomReportKey = "";
			comboBoxToClass.CustomReportValueType = 1;
			comboBoxToClass.DescriptionTextBox = null;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToClass.DisplayLayout.Appearance = appearance121;
			comboBoxToClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance122.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance122.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.GroupByBox.Appearance = appearance122;
			appearance123.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance123;
			comboBoxToClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance124.BackColor2 = System.Drawing.SystemColors.Control;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.PromptAppearance = appearance124;
			comboBoxToClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToClass.DisplayLayout.Override.ActiveCellAppearance = appearance125;
			appearance126.BackColor = System.Drawing.SystemColors.Highlight;
			appearance126.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToClass.DisplayLayout.Override.ActiveRowAppearance = appearance126;
			comboBoxToClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.CardAreaAppearance = appearance127;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			appearance128.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToClass.DisplayLayout.Override.CellAppearance = appearance128;
			comboBoxToClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToClass.DisplayLayout.Override.CellPadding = 0;
			appearance129.BackColor = System.Drawing.SystemColors.Control;
			appearance129.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance129.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance129.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.GroupByRowAppearance = appearance129;
			appearance130.TextHAlignAsString = "Left";
			comboBoxToClass.DisplayLayout.Override.HeaderAppearance = appearance130;
			comboBoxToClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.Color.Silver;
			comboBoxToClass.DisplayLayout.Override.RowAppearance = appearance131;
			comboBoxToClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance132;
			comboBoxToClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToClass.Editable = true;
			comboBoxToClass.Enabled = false;
			comboBoxToClass.FilterString = "";
			comboBoxToClass.HasAllAccount = false;
			comboBoxToClass.HasCustom = false;
			comboBoxToClass.IsDataLoaded = false;
			comboBoxToClass.Location = new System.Drawing.Point(294, 49);
			comboBoxToClass.MaxDropDownItems = 12;
			comboBoxToClass.Name = "comboBoxToClass";
			comboBoxToClass.ShowInactiveItems = false;
			comboBoxToClass.ShowQuickAdd = true;
			comboBoxToClass.Size = new System.Drawing.Size(103, 20);
			comboBoxToClass.TabIndex = 15;
			comboBoxToClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromClass.Assigned = false;
			comboBoxFromClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromClass.CustomReportFieldName = "";
			comboBoxFromClass.CustomReportKey = "";
			comboBoxFromClass.CustomReportValueType = 1;
			comboBoxFromClass.DescriptionTextBox = null;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromClass.DisplayLayout.Appearance = appearance133;
			comboBoxFromClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance134.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance134.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.GroupByBox.Appearance = appearance134;
			appearance135.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance135;
			comboBoxFromClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance136.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance136.BackColor2 = System.Drawing.SystemColors.Control;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance136.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.PromptAppearance = appearance136;
			comboBoxFromClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			appearance137.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromClass.DisplayLayout.Override.ActiveCellAppearance = appearance137;
			appearance138.BackColor = System.Drawing.SystemColors.Highlight;
			appearance138.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromClass.DisplayLayout.Override.ActiveRowAppearance = appearance138;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.CardAreaAppearance = appearance139;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			appearance140.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromClass.DisplayLayout.Override.CellAppearance = appearance140;
			comboBoxFromClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromClass.DisplayLayout.Override.CellPadding = 0;
			appearance141.BackColor = System.Drawing.SystemColors.Control;
			appearance141.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance141.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance141.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.GroupByRowAppearance = appearance141;
			appearance142.TextHAlignAsString = "Left";
			comboBoxFromClass.DisplayLayout.Override.HeaderAppearance = appearance142;
			comboBoxFromClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromClass.DisplayLayout.Override.RowAppearance = appearance143;
			comboBoxFromClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance144;
			comboBoxFromClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromClass.Editable = true;
			comboBoxFromClass.Enabled = false;
			comboBoxFromClass.FilterString = "";
			comboBoxFromClass.HasAllAccount = false;
			comboBoxFromClass.HasCustom = false;
			comboBoxFromClass.IsDataLoaded = false;
			comboBoxFromClass.Location = new System.Drawing.Point(161, 49);
			comboBoxFromClass.MaxDropDownItems = 12;
			comboBoxFromClass.Name = "comboBoxFromClass";
			comboBoxFromClass.ShowInactiveItems = false;
			comboBoxFromClass.ShowQuickAdd = true;
			comboBoxFromClass.Size = new System.Drawing.Size(103, 20);
			comboBoxFromClass.TabIndex = 15;
			comboBoxFromClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxToItem.Assigned = false;
			comboBoxToItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToItem.CustomReportFieldName = "";
			comboBoxToItem.CustomReportKey = "";
			comboBoxToItem.CustomReportValueType = 1;
			comboBoxToItem.DescriptionTextBox = null;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToItem.DisplayLayout.Appearance = appearance145;
			comboBoxToItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance146.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance146.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance146.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToItem.DisplayLayout.GroupByBox.Appearance = appearance146;
			appearance147.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance147;
			comboBoxToItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance148.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance148.BackColor2 = System.Drawing.SystemColors.Control;
			appearance148.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance148.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToItem.DisplayLayout.GroupByBox.PromptAppearance = appearance148;
			comboBoxToItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToItem.DisplayLayout.Override.ActiveCellAppearance = appearance149;
			appearance150.BackColor = System.Drawing.SystemColors.Highlight;
			appearance150.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToItem.DisplayLayout.Override.ActiveRowAppearance = appearance150;
			comboBoxToItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToItem.DisplayLayout.Override.CardAreaAppearance = appearance151;
			appearance152.BorderColor = System.Drawing.Color.Silver;
			appearance152.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToItem.DisplayLayout.Override.CellAppearance = appearance152;
			comboBoxToItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToItem.DisplayLayout.Override.CellPadding = 0;
			appearance153.BackColor = System.Drawing.SystemColors.Control;
			appearance153.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance153.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance153.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance153.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToItem.DisplayLayout.Override.GroupByRowAppearance = appearance153;
			appearance154.TextHAlignAsString = "Left";
			comboBoxToItem.DisplayLayout.Override.HeaderAppearance = appearance154;
			comboBoxToItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			appearance155.BorderColor = System.Drawing.Color.Silver;
			comboBoxToItem.DisplayLayout.Override.RowAppearance = appearance155;
			comboBoxToItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance156;
			comboBoxToItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToItem.Editable = true;
			comboBoxToItem.Enabled = false;
			comboBoxToItem.FilterCustomerID = "";
			comboBoxToItem.FilterString = "";
			comboBoxToItem.FilterSysDocID = "";
			comboBoxToItem.HasAllAccount = false;
			comboBoxToItem.HasCustom = false;
			comboBoxToItem.IsDataLoaded = false;
			comboBoxToItem.Location = new System.Drawing.Point(294, 27);
			comboBoxToItem.MaxDropDownItems = 12;
			comboBoxToItem.Name = "comboBoxToItem";
			comboBoxToItem.Show3PLItems = true;
			comboBoxToItem.ShowInactiveItems = false;
			comboBoxToItem.ShowQuickAdd = true;
			comboBoxToItem.Size = new System.Drawing.Size(103, 20);
			comboBoxToItem.TabIndex = 14;
			comboBoxToItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxFromItem.Assigned = false;
			comboBoxFromItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromItem.CustomReportFieldName = "";
			comboBoxFromItem.CustomReportKey = "";
			comboBoxFromItem.CustomReportValueType = 1;
			comboBoxFromItem.DescriptionTextBox = null;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromItem.DisplayLayout.Appearance = appearance157;
			comboBoxFromItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance158.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromItem.DisplayLayout.GroupByBox.Appearance = appearance158;
			appearance159.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance159;
			comboBoxFromItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance160.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance160.BackColor2 = System.Drawing.SystemColors.Control;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance160.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromItem.DisplayLayout.GroupByBox.PromptAppearance = appearance160;
			comboBoxFromItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromItem.DisplayLayout.Override.ActiveCellAppearance = appearance161;
			appearance162.BackColor = System.Drawing.SystemColors.Highlight;
			appearance162.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromItem.DisplayLayout.Override.ActiveRowAppearance = appearance162;
			comboBoxFromItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromItem.DisplayLayout.Override.CardAreaAppearance = appearance163;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			appearance164.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromItem.DisplayLayout.Override.CellAppearance = appearance164;
			comboBoxFromItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromItem.DisplayLayout.Override.CellPadding = 0;
			appearance165.BackColor = System.Drawing.SystemColors.Control;
			appearance165.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance165.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromItem.DisplayLayout.Override.GroupByRowAppearance = appearance165;
			appearance166.TextHAlignAsString = "Left";
			comboBoxFromItem.DisplayLayout.Override.HeaderAppearance = appearance166;
			comboBoxFromItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromItem.DisplayLayout.Override.RowAppearance = appearance167;
			comboBoxFromItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance168;
			comboBoxFromItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromItem.Editable = true;
			comboBoxFromItem.Enabled = false;
			comboBoxFromItem.FilterCustomerID = "";
			comboBoxFromItem.FilterString = "";
			comboBoxFromItem.FilterSysDocID = "";
			comboBoxFromItem.HasAllAccount = false;
			comboBoxFromItem.HasCustom = false;
			comboBoxFromItem.IsDataLoaded = false;
			comboBoxFromItem.Location = new System.Drawing.Point(161, 27);
			comboBoxFromItem.MaxDropDownItems = 12;
			comboBoxFromItem.Name = "comboBoxFromItem";
			comboBoxFromItem.Show3PLItems = true;
			comboBoxFromItem.ShowInactiveItems = false;
			comboBoxFromItem.ShowQuickAdd = true;
			comboBoxFromItem.Size = new System.Drawing.Size(103, 20);
			comboBoxFromItem.TabIndex = 13;
			comboBoxFromItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxSingleItem.Assigned = false;
			comboBoxSingleItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleItem.CustomReportFieldName = "";
			comboBoxSingleItem.CustomReportKey = "";
			comboBoxSingleItem.CustomReportValueType = 1;
			comboBoxSingleItem.DescriptionTextBox = null;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			appearance169.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleItem.DisplayLayout.Appearance = appearance169;
			comboBoxSingleItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance170.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance170.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance170.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance170.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleItem.DisplayLayout.GroupByBox.Appearance = appearance170;
			appearance171.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance171;
			comboBoxSingleItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance172.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance172.BackColor2 = System.Drawing.SystemColors.Control;
			appearance172.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance172.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleItem.DisplayLayout.GroupByBox.PromptAppearance = appearance172;
			comboBoxSingleItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance173.BackColor = System.Drawing.SystemColors.Window;
			appearance173.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleItem.DisplayLayout.Override.ActiveCellAppearance = appearance173;
			appearance174.BackColor = System.Drawing.SystemColors.Highlight;
			appearance174.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleItem.DisplayLayout.Override.ActiveRowAppearance = appearance174;
			comboBoxSingleItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleItem.DisplayLayout.Override.CardAreaAppearance = appearance175;
			appearance176.BorderColor = System.Drawing.Color.Silver;
			appearance176.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleItem.DisplayLayout.Override.CellAppearance = appearance176;
			comboBoxSingleItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleItem.DisplayLayout.Override.CellPadding = 0;
			appearance177.BackColor = System.Drawing.SystemColors.Control;
			appearance177.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance177.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance177.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance177.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleItem.DisplayLayout.Override.GroupByRowAppearance = appearance177;
			appearance178.TextHAlignAsString = "Left";
			comboBoxSingleItem.DisplayLayout.Override.HeaderAppearance = appearance178;
			comboBoxSingleItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance179.BackColor = System.Drawing.SystemColors.Window;
			appearance179.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleItem.DisplayLayout.Override.RowAppearance = appearance179;
			comboBoxSingleItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance180.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance180;
			comboBoxSingleItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleItem.Editable = true;
			comboBoxSingleItem.FilterCustomerID = "";
			comboBoxSingleItem.FilterString = "";
			comboBoxSingleItem.FilterSysDocID = "";
			comboBoxSingleItem.HasAllAccount = false;
			comboBoxSingleItem.HasCustom = false;
			comboBoxSingleItem.IsDataLoaded = false;
			comboBoxSingleItem.Location = new System.Drawing.Point(161, 5);
			comboBoxSingleItem.MaxDropDownItems = 12;
			comboBoxSingleItem.Name = "comboBoxSingleItem";
			comboBoxSingleItem.Show3PLItems = true;
			comboBoxSingleItem.ShowInactiveItems = false;
			comboBoxSingleItem.ShowQuickAdd = true;
			comboBoxSingleItem.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleItem.TabIndex = 12;
			comboBoxSingleItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToStyle);
			base.Controls.Add(comboBoxFromStyle);
			base.Controls.Add(label12);
			base.Controls.Add(label13);
			base.Controls.Add(radioButtonStyle);
			base.Controls.Add(comboBoxToOrigin);
			base.Controls.Add(comboBoxFromOrigin);
			base.Controls.Add(label10);
			base.Controls.Add(label11);
			base.Controls.Add(radioButtonOrigin);
			base.Controls.Add(comboBoxToManufacturer);
			base.Controls.Add(comboBoxFromManufacturer);
			base.Controls.Add(label8);
			base.Controls.Add(label9);
			base.Controls.Add(radioButtonManufacturer);
			base.Controls.Add(comboBoxToBrand);
			base.Controls.Add(comboBoxFromBrand);
			base.Controls.Add(label6);
			base.Controls.Add(label7);
			base.Controls.Add(radioButtonBrand);
			base.Controls.Add(comboBoxToCategory);
			base.Controls.Add(comboBoxFromCategory);
			base.Controls.Add(comboBoxToClass);
			base.Controls.Add(comboBoxFromClass);
			base.Controls.Add(comboBoxToItem);
			base.Controls.Add(comboBoxFromItem);
			base.Controls.Add(comboBoxSingleItem);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(label5);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonCategory);
			base.Controls.Add(radioButtonClass);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(label4);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(labelTo);
			base.Name = "ProductSelector";
			base.Size = new System.Drawing.Size(414, 185);
			base.Load += new System.EventHandler(ProductSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToStyle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromStyle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToOrigin).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromOrigin).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToManufacturer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromManufacturer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToBrand).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromBrand).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleItem).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
