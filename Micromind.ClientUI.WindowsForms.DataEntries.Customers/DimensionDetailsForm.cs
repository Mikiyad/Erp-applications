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
	public class DimensionDetailsForm : Form, IForm
	{
		private DimensionData currentData;

		private const string TABLENAME_CONST = "Dimension";

		private const string IDFIELD_CONST = "DimensionID";

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private DataEntryGrid dataGridAttributes;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem insertRowToolStripMenuItem;

		private ToolStripMenuItem moveUpToolStripMenuItem;

		private ToolStripMenuItem moveDownToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ToolStripButton toolStripButtonInformation;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4015;

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

		public DimensionDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += DimensionDetailsForm_Load;
			dataGridAttributes.BeforeCellUpdate += dataGridAttributes_BeforeCellUpdate;
			dataGridAttributes.AfterRowUpdate += dataGridAttributes_AfterRowUpdate;
		}

		private void dataGridAttributes_AfterRowUpdate(object sender, RowEventArgs e)
		{
			if (e.Row != null && e.Row.Cells[0].Value != null)
			{
				e.Row.Cells[0].Value = e.Row.Cells[0].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[1].Value != null)
			{
				e.Row.Cells[1].Value = e.Row.Cells[1].Value.ToString().Trim();
			}
		}

		private void dataGridAttributes_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "AttributeID")
			{
				string b = e.NewValue.ToString();
				foreach (UltraGridRow row in dataGridAttributes.Rows)
				{
					if (row.Index != e.Cell.Row.Index && row.Cells["AttributeID"].Value.ToString() == b)
					{
						ErrorHelper.WarningMessage("Another item with same code is already exist. Please enter another code.");
						e.Cancel = true;
						break;
					}
				}
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new DimensionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.DimensionTable.Rows[0] : currentData.DimensionTable.NewRow();
				dataRow.BeginEdit();
				dataRow["DimensionID"] = textBoxCode.Text.Trim();
				dataRow["DimensionName"] = textBoxName.Text.Trim();
				dataRow["IsInactive "] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.DimensionTable.Rows.Add(dataRow);
				}
				currentData.DimensionAttributeTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridAttributes.Rows)
				{
					dataRow = currentData.DimensionAttributeTable.NewRow();
					dataRow["AttributeID"] = row.Cells["AttributeID"].Value.ToString();
					dataRow["AttributeName"] = row.Cells["AttributeName"].Value.ToString();
					dataRow["DimensionID"] = textBoxCode.Text;
					dataRow["RowIndex"] = row.Index;
					currentData.DimensionAttributeTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridAttributes.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("AttributeID");
				dataTable.Columns.Add("AttributeName");
				dataGridAttributes.DataSource = dataTable;
				dataGridAttributes.DisplayLayout.Bands[0].Columns["AttributeID"].Header.Caption = "Attribute Code";
				dataGridAttributes.DisplayLayout.Bands[0].Columns["AttributeName"].Header.Caption = "Attribute Name";
				dataGridAttributes.DisplayLayout.Bands[0].Columns["AttributeID"].CharacterCasing = CharacterCasing.Upper;
				dataGridAttributes.DisplayLayout.Bands[0].Columns["AttributeID"].MaxLength = 15;
				dataGridAttributes.DisplayLayout.Bands[0].Columns["AttributeName"].MaxLength = 15;
			}
			catch (Exception e)
			{
				dataGridAttributes.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
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
					currentData = Factory.DimensionSystem.GetDimensionByID(id.Trim());
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
					textBoxCode.Text = dataRow["DimensionID"].ToString();
					textBoxName.Text = dataRow["DimensionName"].ToString();
					if (dataRow["IsInactive "] != DBNull.Value)
					{
						checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive "].ToString());
					}
					else
					{
						checkBoxInactive.Checked = false;
					}
					DataTable dataTable = dataGridAttributes.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in currentData.DimensionAttributeTable.Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["AttributeID"] = row["AttributeID"];
						dataRow3["AttributeName"] = row["AttributeName"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.AcceptChanges();
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
					flag = Factory.DimensionSystem.CreateDimension(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Dimension, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.DimensionSystem.UpdateDimension(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Dimension", "DimensionID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Dimension", "DimensionID", textBoxCode.Text.Trim()))
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
			textBoxCode.Clear();
			textBoxName.Clear();
			(dataGridAttributes.DataSource as DataTable).Rows.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void DimensionGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void DimensionGroupDetailsForm_Validated(object sender, EventArgs e)
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
				bool num = Factory.DimensionSystem.DeleteDimension(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Dimension, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Dimension", "DimensionID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Dimension", "DimensionID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Dimension", "DimensionID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Dimension", "DimensionID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Dimension", "DimensionID", toolStripTextBoxFind.Text.Trim()))
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

		private void DimensionDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridAttributes.SetupUI();
				SetupGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridAttributes.LoadLayoutFailed = true;
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
			new FormHelper().ShowList(DataComboType.Dimension);
		}

		private void insertRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int index = dataGridAttributes.ActiveRow.Index;
			UltraGridRow row = dataGridAttributes.DisplayLayout.Bands[0].AddNew();
			dataGridAttributes.Rows.Move(row, index);
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dataGridAttributes.ActiveRow.Delete(displayPrompt: false);
		}

		private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridAttributes.ActiveRow != null && dataGridAttributes.ActiveRow.Index != 0)
			{
				dataGridAttributes.Rows.Move(dataGridAttributes.ActiveRow, checked(dataGridAttributes.ActiveRow.Index - 1));
			}
		}

		private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridAttributes.ActiveRow != null && dataGridAttributes.ActiveRow.Index != dataGridAttributes.Rows.Count - 1)
				{
					dataGridAttributes.Rows.Move(dataGridAttributes.ActiveRow, dataGridAttributes.ActiveRow.Index + 1);
				}
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.DimensionDetailsForm));
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
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			insertRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			dataGridAttributes = new Micromind.DataControls.DataEntryGrid();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridAttributes).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
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
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(479, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
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
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 300);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(479, 40);
			panelButtons.TabIndex = 6;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(479, 1);
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
			xpButton1.Location = new System.Drawing.Point(369, 8);
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
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(293, 37);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 2;
			checkBoxInactive.Text = "Inactvie";
			checkBoxInactive.UseVisualStyleBackColor = true;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5]
			{
				moveUpToolStripMenuItem,
				moveDownToolStripMenuItem,
				toolStripSeparator4,
				insertRowToolStripMenuItem,
				removeRowToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(138, 98);
			moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
			moveUpToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			moveUpToolStripMenuItem.Text = "Move Up";
			moveUpToolStripMenuItem.Click += new System.EventHandler(moveUpToolStripMenuItem_Click);
			moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
			moveDownToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			moveDownToolStripMenuItem.Text = "Move Down";
			moveDownToolStripMenuItem.Click += new System.EventHandler(moveDownToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(134, 6);
			insertRowToolStripMenuItem.Name = "insertRowToolStripMenuItem";
			insertRowToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			insertRowToolStripMenuItem.Text = "Insert Row";
			insertRowToolStripMenuItem.Click += new System.EventHandler(insertRowToolStripMenuItem_Click);
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			removeRowToolStripMenuItem.Click += new System.EventHandler(removeRowToolStripMenuItem_Click);
			dataGridAttributes.AllowAddNew = false;
			dataGridAttributes.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridAttributes.ContextMenuStrip = contextMenuStrip1;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridAttributes.DisplayLayout.Appearance = appearance;
			dataGridAttributes.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridAttributes.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridAttributes.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridAttributes.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridAttributes.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridAttributes.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridAttributes.DisplayLayout.MaxColScrollRegions = 1;
			dataGridAttributes.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridAttributes.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridAttributes.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridAttributes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridAttributes.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridAttributes.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridAttributes.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridAttributes.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridAttributes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridAttributes.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridAttributes.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridAttributes.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridAttributes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridAttributes.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridAttributes.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridAttributes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridAttributes.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridAttributes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridAttributes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridAttributes.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridAttributes.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridAttributes.ExitEditModeOnLeave = false;
			dataGridAttributes.Location = new System.Drawing.Point(11, 87);
			dataGridAttributes.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridAttributes.Name = "dataGridAttributes";
			dataGridAttributes.ShowClearMenu = true;
			dataGridAttributes.ShowDeleteMenu = true;
			dataGridAttributes.ShowInsertMenu = true;
			dataGridAttributes.ShowMoveRowsMenu = true;
			dataGridAttributes.Size = new System.Drawing.Size(454, 207);
			dataGridAttributes.TabIndex = 17;
			dataGridAttributes.Text = "dataEntryGrid1";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(114, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(351, 20);
			textBoxName.TabIndex = 3;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(114, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(173, 20);
			textBoxCode.TabIndex = 1;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 60);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(105, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Dimension Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 38);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(102, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Dimension Code:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(479, 340);
			base.Controls.Add(dataGridAttributes);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "DimensionDetailsForm";
			Text = "Dimension Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridAttributes).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
