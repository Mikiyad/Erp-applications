using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries
{
	public class ImportSmartListForm : Form
	{
		private const string FILTER_CONST = "Image Files (*.bmp, *.jpg, *.jpeg, *.png, *.tif, *.tiff) | *.bmp; *.jpg; *.jpeg; *.png; *.tif; *.tiff |MS Office Files (*.doc, *.docx, *.xls, *.xlsx) | *.doc; *.docx; *.xls; *.xlsx |Text Files (*.txt, *.csv) |*.axs, *.txt; *.csv |All files (*.*) | *.*";

		private const string FILTER_CONST1 = "Files|*.axs; |All files (*.*) | *.*";

		private SmartListData currentData;

		private const string TABLENAME_CONST = "EntityDocs";

		private const string IDFIELD_CONST = "EntityID";

		private bool isNewRecord = true;

		private string reportType = "";

		private bool isFileUploaded;

		private string entityID = "";

		private int entityRowIndex = -1;

		private EntityTypesEnum entityType = EntityTypesEnum.Customers;

		private IContainer components;

		private Button buttonCancel;

		private Line linePanelDown;

		private Button buttonUpload;

		private OpenFileDialog openFileDialog;

		private Label label1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private Button buttonSelectMultiple;

		private DataEntryGrid ultraGridDocs;

		private SmartListCategoryComboBox ComboBoxCategory;

		private PivotGroupComboBox comboBoxPivotGroup;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string FileName
		{
			get;
			set;
		}

		public string EntitySysDocID
		{
			get;
			set;
		}

		public string EntityID
		{
			get
			{
				return entityID;
			}
			set
			{
				entityID = value;
			}
		}

		public int EntityRowIndex
		{
			get
			{
				return entityRowIndex;
			}
			set
			{
				entityRowIndex = value;
			}
		}

		public EntityTypesEnum EntityType
		{
			get
			{
				return entityType;
			}
			set
			{
				entityType = value;
			}
		}

		public bool IsFileUploaded
		{
			get
			{
				return isFileUploaded;
			}
			set
			{
				isFileUploaded = value;
			}
		}

		public string ReportType
		{
			get
			{
				return reportType;
			}
			set
			{
				reportType = value;
				if (reportType == "PivotReport")
				{
					Text = "Import Pivot Report";
				}
				else if (reportType == "CustomGadget")
				{
					Text = "Import Custom Gadget";
				}
			}
		}

		public ImportSmartListForm()
		{
			InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void UploadFileDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void ultraGridDocs_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.All;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void ultraGridDocs_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
			DataTable dataTable = ultraGridDocs.DataSource as DataTable;
			checked
			{
				for (int i = 0; i < array.Length; i++)
				{
					DataRow dataRow = dataTable.NewRow();
					string path = (string)(dataRow["Path"] = array[i]);
					dataRow["Name"] = Path.GetFileName(path);
					int num = 1;
					while (dataTable.Select("Name = '" + dataRow["Name"].ToString() + "'").Length != 0)
					{
						dataRow["Name"] = Path.GetFileNameWithoutExtension(path) + "_" + num + Path.GetExtension(path);
						num++;
					}
					dataTable.Rows.Add(dataRow);
				}
				if (ultraGridDocs.Rows.Count > 0)
				{
					buttonUpload.Enabled = true;
				}
				else
				{
					buttonUpload.Enabled = false;
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private bool ValidateData()
		{
			return true;
		}

		private void UploadFileDialog_Activated(object sender, EventArgs e)
		{
		}

		private void buttonOpenFileDialog_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png, *.tif, *.tiff) | *.bmp; *.jpg; *.jpeg; *.png; *.tif; *.tiff |MS Office Files (*.doc, *.docx, *.xls, *.xlsx) | *.doc; *.docx; *.xls; *.xlsx |Text Files (*.txt, *.csv) |*.axs, *.txt; *.csv |All files (*.*) | *.*";
			openFileDialog.DefaultExt = "txt";
			openFileDialog.FileName = "";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (!new FileInfo(openFileDialog.FileName).Exists)
				{
					ErrorHelper.WarningMessage("Invalid file. Please try again later!");
					buttonUpload.Enabled = false;
				}
				else
				{
					buttonUpload.Enabled = true;
				}
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonUpload_Click(object sender, EventArgs e)
		{
			if (SaveData())
			{
				SaveDatas();
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				base.DialogResult = DialogResult.None;
			}
		}

		private bool SaveData()
		{
			try
			{
				foreach (UltraGridRow row in ultraGridDocs.Rows)
				{
					if (ReportType == "SmartList")
					{
						if (string.IsNullOrWhiteSpace(row.Cells["CategoryName"].Value.ToString()))
						{
							ErrorHelper.InformationMessage("Pleas EnterValid CategoryName");
							return false;
						}
						if (Factory.SmartListSystem.GetCategoryListByID(int.Parse(row.Cells["Category"].Value.ToString())).Tables["Smartlist_Category"].Rows.Count <= 0)
						{
							int num = NewCategory(row.Cells["CategoryName"].Value.ToString());
							ComboBoxCategory.LoadData(isReferesh: true);
							setValue(row.Cells["Category"].Value.ToString(), num.ToString());
						}
					}
					else if (ReportType == "PivotReport")
					{
						if (string.IsNullOrWhiteSpace(row.Cells["GroupName"].Value.ToString()))
						{
							ErrorHelper.InformationMessage("Pleas EnterValid GroupName");
							return false;
						}
						if (Factory.PivotGroupSystem.GetPivotGroupByID(row.Cells["Group"].Value.ToString()).Tables["Pivot_Group"].Rows.Count <= 0)
						{
							int num2 = NewGroup(row.Cells["GroupName"].Value.ToString());
							comboBoxPivotGroup.LoadData(isReferesh: true);
							setValue(row.Cells["Group"].Value.ToString(), num2.ToString());
						}
					}
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void setValue(string oldvalue, string newValue)
		{
			if (ReportType == "SmartList")
			{
				foreach (UltraGridRow item in ultraGridDocs.Rows.Where((UltraGridRow x) => x.Cells["Category"].Value.ToString() == oldvalue))
				{
					item.Cells["Category"].Value = newValue;
				}
			}
			else if (ReportType == "PivotReport")
			{
				foreach (UltraGridRow item2 in ultraGridDocs.Rows.Where((UltraGridRow x) => x.Cells["Group"].Value.ToString() == oldvalue))
				{
					item2.Cells["Group"].Value = newValue;
				}
			}
			else if (ReportType == "CustomGadget")
			{
				foreach (UltraGridRow item3 in ultraGridDocs.Rows.Where((UltraGridRow x) => x.Cells["Category"].Value.ToString() == oldvalue))
				{
					item3.Cells["Category"].Value = newValue;
				}
			}
		}

		private void SaveDatas()
		{
			try
			{
				foreach (UltraGridRow row in ultraGridDocs.Rows)
				{
					if (ReportType == "SmartList")
					{
						currentData = new SmartListData();
						currentData.ReadXml(row.Cells["Path"].Value.ToString(), XmlReadMode.ReadSchema);
						currentData.Tables["Smartlist"].Rows[0]["CategoryId"] = row.Cells["Category"].Value.ToString();
						if (currentData != null)
						{
							Factory.SmartListSystem.CreateSmartList(currentData);
						}
					}
					else if (ReportType == "PivotReport")
					{
						PivotData pivotData = new PivotData();
						pivotData.ReadXml(row.Cells["Path"].Value.ToString(), XmlReadMode.ReadSchema);
						pivotData.Tables["Custom_Gadget"].Rows[0]["CategoryId"] = PublicFunctions.GetNextCardNumber("Custom_Gadget", "CustomGadgetID");
						pivotData.Tables["Pivot_Report"].Rows[0]["GroupId"] = row.Cells["Group"].Value.ToString();
						if (pivotData != null)
						{
							Factory.PivotSystem.InsertUpdatePivot(pivotData, isUpdate: false);
						}
					}
					else if (ReportType == "CustomGadget")
					{
						CustomGadgetData customGadgetData = new CustomGadgetData();
						customGadgetData.ReadXml(row.Cells["Path"].Value.ToString(), XmlReadMode.ReadSchema);
						customGadgetData.Tables["Custom_Gadget"].Rows[0]["CategoryId"] = row.Cells["Category"].Value.ToString();
						if (customGadgetData != null)
						{
							Factory.CustomGadgetSystem.CreateCustomGadget(customGadgetData);
						}
					}
				}
				ErrorHelper.InformationMessage("Exported Successfully");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private int NewCategory(string name)
		{
			return Factory.SmartListSystem.CreateCategory(name, "-1");
		}

		private int NewGroup(string name)
		{
			return Factory.PivotGroupSystem.CreateGroup(name);
		}

		private byte[] GetInputFile(string filePath)
		{
			byte[] result = null;
			if (filePath == "")
			{
				filePath = "";
			}
			string extension = Path.GetExtension(Path.GetFileName(filePath));
			string a = string.Empty;
			extension = extension.ToLower();
			try
			{
				switch (extension)
				{
				case ".doc":
					a = "application/vnd.ms-word";
					break;
				case ".docx":
					a = "application/vnd.ms-word";
					break;
				case ".xls":
					a = "application/vnd.ms-excel";
					break;
				case ".xlsx":
					a = "application/vnd.ms-excel";
					break;
				case ".jpg":
				case ".jpeg":
					a = "image/jpg";
					break;
				case ".png":
					a = "image/png";
					break;
				case ".gif":
					a = "image/gif";
					break;
				case ".pdf":
					a = "application/pdf";
					break;
				case ".tif":
					a = "image/tif";
					break;
				case ".bmp":
					a = "image/bmp";
					break;
				case ".dwg":
					a = "application/autocad_dwg";
					break;
				}
				if (a != string.Empty)
				{
					FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
					return new BinaryReader(fileStream).ReadBytes(checked((int)fileStream.Length));
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void UploadFileDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		private void AddEvents()
		{
			ultraGridDocs.CellChange += dataGridItems_CellChange;
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Header.Caption.ToString() == "CategoryName")
			{
				string categoryId = ultraGridDocs.ActiveRow.Cells["Category"].Value.ToString();
				if (!string.IsNullOrWhiteSpace(categoryId))
				{
					foreach (UltraGridRow item in ultraGridDocs.Rows.Where((UltraGridRow x) => x.Cells["Category"].Value.ToString() == categoryId))
					{
						item.Cells["CategoryName"].Value = e.Cell.Text;
					}
				}
			}
			else if (e.Cell.Column.Header.Caption.ToString() == "Category" && ultraGridDocs.ActiveRow != null && !string.IsNullOrWhiteSpace(ComboBoxCategory.SelectedName))
			{
				ultraGridDocs.ActiveRow.Cells["CategoryName"].Value = ComboBoxCategory.SelectedName;
			}
		}

		private void SetupGrid()
		{
			try
			{
				DataTable dataTable = new DataSet().Tables.Add("Files");
				dataTable.Columns.Add("ID");
				dataTable.Columns.Add("Path");
				if (ReportType == "SmartList")
				{
					dataTable.Columns.Add("Smart List");
					dataTable.Columns.Add("Category");
					dataTable.Columns.Add("CategoryName");
					ultraGridDocs.DataSource = dataTable;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Smart List"].CellActivation = Activation.NoEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Category"].MaxLength = 64;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Category"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Category"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Category"].ValueList = ComboBoxCategory;
				}
				else if (ReportType == "PivotReport")
				{
					dataTable.Columns.Add("PivotReport");
					dataTable.Columns.Add("Group");
					dataTable.Columns.Add("GroupName");
					ultraGridDocs.DataSource = dataTable;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["PivotReport"].CellActivation = Activation.NoEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Group"].MaxLength = 64;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Group"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Group"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Group"].ValueList = comboBoxPivotGroup;
					comboBoxPivotGroup.LoadData();
				}
				else if (ReportType == "CustomGadget")
				{
					dataTable.Columns.Add("CustomGadget");
					dataTable.Columns.Add("Category");
					dataTable.Columns.Add("CategoryName");
					ultraGridDocs.DataSource = dataTable;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["CustomGadget"].CellActivation = Activation.NoEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Category"].MaxLength = 64;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Category"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Category"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
					ValueList valueList = new ValueList();
					valueList.ValueListItems.Add("General");
					valueList.ValueListItems.Add("Accounts");
					valueList.ValueListItems.Add("Sales & Customers");
					valueList.ValueListItems.Add("Purchase & Vendors");
					valueList.ValueListItems.Add("Inventory");
					valueList.ValueListItems.Add("HR & Admin");
					valueList.ValueListItems.Add("Miscellaneous");
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Category"].ValueList = valueList;
				}
				ultraGridDocs.DisplayLayout.Bands[0].Columns["ID"].Hidden = true;
				ultraGridDocs.DisplayLayout.Bands[0].Columns["Path"].Hidden = true;
				ultraGridDocs.ContextMenuStrip = contextMenuStrip1;
				ultraGridDocs.SetupUI();
			}
			catch (Exception e)
			{
				ultraGridDocs.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			openFileDialog.Filter = "Files|*.axs; |All files (*.*) | *.*";
			openFileDialog.Multiselect = true;
			openFileDialog.DefaultExt = "axs";
			openFileDialog.FileName = "";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				List<string> fileNames = openFileDialog.FileNames.ToList();
				FillData(fileNames);
				buttonUpload.Enabled = true;
			}
		}

		private void FillData(List<string> fileNames)
		{
			DataTable dataTable = ultraGridDocs.DataSource as DataTable;
			foreach (string fileName in fileNames)
			{
				try
				{
					FileInfo fileInfo = new FileInfo(fileName);
					currentData = new SmartListData();
					currentData.ReadXml(fileInfo.FullName, XmlReadMode.ReadSchema);
					DataRow dataRow = dataTable.NewRow();
					if (ReportType == "SmartList")
					{
						dataRow["ID"] = currentData.Tables["SmartList"].Rows[0]["SmartListId"];
						dataRow["Path"] = fileInfo.FullName;
						dataRow["Smart List"] = currentData.Tables["SmartList"].Rows[0]["SmartListName"];
						dataRow["Category"] = currentData.Tables["SmartList"].Rows[0]["CategoryId"].ToString();
						ComboBoxCategory.SelectedID = currentData.Tables["SmartList"].Rows[0]["CategoryId"].ToString();
						dataRow["CategoryName"] = ComboBoxCategory.SelectedName;
					}
					else if (ReportType == "PivotReport")
					{
						dataRow["ID"] = currentData.Tables["Pivot_Report"].Rows[0]["PivotId"];
						dataRow["Path"] = fileInfo.FullName;
						dataRow["PivotReport"] = currentData.Tables["Pivot_Report"].Rows[0]["PivotName"].ToString();
						dataRow["Group"] = currentData.Tables["Pivot_Report"].Rows[0]["GroupID"].ToString();
						comboBoxPivotGroup.SelectedID = currentData.Tables["Pivot_Report"].Rows[0]["GroupID"].ToString();
						dataRow["GroupName"] = comboBoxPivotGroup.SelectedName;
					}
					else if (ReportType == "CustomGadget")
					{
						dataRow["ID"] = currentData.Tables["Custom_Gadget"].Rows[0]["CustomGadgetID"];
						dataRow["Path"] = fileInfo.FullName;
						dataRow["CustomGadget"] = currentData.Tables["Custom_Gadget"].Rows[0]["CustomGadgetName"].ToString();
						dataRow["Category"] = currentData.Tables["Custom_Gadget"].Rows[0]["CategoryID"].ToString();
						dataRow["CategoryName"] = ((GadgetCategories)int.Parse(currentData.Tables["Custom_Gadget"].Rows[0]["CategoryID"].ToString())).ToString();
					}
					dataRow.EndEdit();
					dataTable.Rows.Add(dataRow);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
			dataTable.AcceptChanges();
		}

		private void UploadFileDialog_Load(object sender, EventArgs e)
		{
			ComboBoxCategory.LoadData(isReferesh: true);
			SetupGrid();
			AddEvents();
			ultraGridDocs.ShowDeleteMenu = true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.ImportSmartListForm));
			label1 = new System.Windows.Forms.Label();
			buttonCancel = new System.Windows.Forms.Button();
			buttonUpload = new System.Windows.Forms.Button();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			buttonSelectMultiple = new System.Windows.Forms.Button();
			comboBoxPivotGroup = new Micromind.DataControls.PivotGroupComboBox();
			ComboBoxCategory = new Micromind.DataControls.SmartListCategoryComboBox();
			ultraGridDocs = new Micromind.DataControls.DataEntryGrid();
			linePanelDown = new Micromind.UISupport.Line();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPivotGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGridDocs).BeginInit();
			SuspendLayout();
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Location = new System.Drawing.Point(56, 39);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(61, 13);
			label1.TabIndex = 6;
			label1.Text = "Select files ";
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(686, 396);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonUpload.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonUpload.Enabled = false;
			buttonUpload.Location = new System.Drawing.Point(583, 395);
			buttonUpload.Name = "buttonUpload";
			buttonUpload.Size = new System.Drawing.Size(97, 25);
			buttonUpload.TabIndex = 3;
			buttonUpload.Text = "Upload";
			buttonUpload.UseVisualStyleBackColor = true;
			buttonUpload.Click += new System.EventHandler(buttonUpload_Click);
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				deleteToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(117, 26);
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			deleteToolStripMenuItem.Text = "Delete...";
			buttonSelectMultiple.Location = new System.Drawing.Point(12, 33);
			buttonSelectMultiple.Name = "buttonSelectMultiple";
			buttonSelectMultiple.Size = new System.Drawing.Size(38, 22);
			buttonSelectMultiple.TabIndex = 27;
			buttonSelectMultiple.Text = "...";
			buttonSelectMultiple.UseVisualStyleBackColor = true;
			buttonSelectMultiple.Click += new System.EventHandler(button1_Click);
			comboBoxPivotGroup.Assigned = false;
			comboBoxPivotGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPivotGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPivotGroup.CustomReportFieldName = "";
			comboBoxPivotGroup.CustomReportKey = "";
			comboBoxPivotGroup.CustomReportValueType = 1;
			comboBoxPivotGroup.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPivotGroup.DisplayLayout.Appearance = appearance;
			comboBoxPivotGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPivotGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxPivotGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPivotGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPivotGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPivotGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxPivotGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPivotGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPivotGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPivotGroup.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxPivotGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPivotGroup.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPivotGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxPivotGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxPivotGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPivotGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxPivotGroup.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxPivotGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPivotGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxPivotGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPivotGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPivotGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPivotGroup.DisplayMember = "Name";
			comboBoxPivotGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPivotGroup.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxPivotGroup.Editable = true;
			comboBoxPivotGroup.FilterString = "";
			comboBoxPivotGroup.HasAllAccount = false;
			comboBoxPivotGroup.HasCustom = false;
			comboBoxPivotGroup.IsDataLoaded = false;
			comboBoxPivotGroup.Location = new System.Drawing.Point(520, 112);
			comboBoxPivotGroup.MaxDropDownItems = 12;
			comboBoxPivotGroup.Name = "comboBoxPivotGroup";
			comboBoxPivotGroup.ShowInactiveItems = false;
			comboBoxPivotGroup.ShowQuickAdd = true;
			comboBoxPivotGroup.Size = new System.Drawing.Size(134, 20);
			comboBoxPivotGroup.TabIndex = 162;
			comboBoxPivotGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPivotGroup.Visible = false;
			ComboBoxCategory.Assigned = false;
			ComboBoxCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxCategory.CustomReportFieldName = "";
			ComboBoxCategory.CustomReportKey = "";
			ComboBoxCategory.CustomReportValueType = 1;
			ComboBoxCategory.DescriptionTextBox = null;
			ComboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxCategory.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			ComboBoxCategory.Editable = true;
			ComboBoxCategory.FilterString = "";
			ComboBoxCategory.HasAllAccount = false;
			ComboBoxCategory.HasCustom = false;
			ComboBoxCategory.IsDataLoaded = false;
			ComboBoxCategory.Location = new System.Drawing.Point(394, 112);
			ComboBoxCategory.MaxDropDownItems = 12;
			ComboBoxCategory.Name = "ComboBoxCategory";
			ComboBoxCategory.ShowInactiveItems = false;
			ComboBoxCategory.ShowQuickAdd = true;
			ComboBoxCategory.Size = new System.Drawing.Size(120, 20);
			ComboBoxCategory.TabIndex = 161;
			ComboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxCategory.Visible = false;
			ultraGridDocs.AllowAddNew = false;
			ultraGridDocs.AllowDrop = true;
			ultraGridDocs.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ultraGridDocs.DisplayLayout.Appearance = appearance13;
			ultraGridDocs.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ultraGridDocs.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			ultraGridDocs.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			ultraGridDocs.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			ultraGridDocs.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			ultraGridDocs.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			ultraGridDocs.DisplayLayout.MaxColScrollRegions = 1;
			ultraGridDocs.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridDocs.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			ultraGridDocs.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			ultraGridDocs.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			ultraGridDocs.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ultraGridDocs.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			ultraGridDocs.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ultraGridDocs.DisplayLayout.Override.CellAppearance = appearance20;
			ultraGridDocs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ultraGridDocs.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			ultraGridDocs.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			ultraGridDocs.DisplayLayout.Override.HeaderAppearance = appearance22;
			ultraGridDocs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ultraGridDocs.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			ultraGridDocs.DisplayLayout.Override.RowAppearance = appearance23;
			ultraGridDocs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			ultraGridDocs.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			ultraGridDocs.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ultraGridDocs.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ultraGridDocs.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ultraGridDocs.IncludeLotItems = false;
			ultraGridDocs.LoadLayoutFailed = false;
			ultraGridDocs.Location = new System.Drawing.Point(12, 59);
			ultraGridDocs.Name = "ultraGridDocs";
			ultraGridDocs.ShowClearMenu = true;
			ultraGridDocs.ShowDeleteMenu = false;
			ultraGridDocs.ShowInsertMenu = true;
			ultraGridDocs.ShowMoveRowsMenu = true;
			ultraGridDocs.Size = new System.Drawing.Size(753, 306);
			ultraGridDocs.TabIndex = 26;
			ultraGridDocs.Text = "dataGrid";
			linePanelDown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 383);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(787, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			base.AcceptButton = buttonUpload;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(781, 427);
			base.Controls.Add(comboBoxPivotGroup);
			base.Controls.Add(ComboBoxCategory);
			base.Controls.Add(ultraGridDocs);
			base.Controls.Add(buttonSelectMultiple);
			base.Controls.Add(label1);
			base.Controls.Add(buttonUpload);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(buttonCancel);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ImportSmartListForm";
			Text = "Import Smart List";
			base.Activated += new System.EventHandler(UploadFileDialog_Activated);
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(UploadFileDialog_FormClosed);
			base.Load += new System.EventHandler(UploadFileDialog_Load);
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxPivotGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGridDocs).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
