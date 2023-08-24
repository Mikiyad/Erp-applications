using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries
{
	public class EditFileDialog : Form, IForm
	{
		private EntityDocData currentData;

		private const string TABLENAME_CONST = "EntityDocs";

		private const string IDFIELD_CONST = "EntityID";

		private bool isNewRecord = true;

		private string entityID = "";

		private EntityTypesEnum entityType = EntityTypesEnum.Customers;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonCancel;

		private Line linePanelDown;

		private MMTextBox textFileName;

		private MMLabel mmLabel1;

		private Button buttonSave;

		private MMTextBox textFileKeyword;

		private MMLabel mmLabel4;

		private MMLabel mmLabel3;

		private MMTextBox textFileDesc;

		private OpenFileDialog openFileDialog;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string FilePath
		{
			get
			{
				return textFileName.Text;
			}
			set
			{
				textFileName.Text = value;
			}
		}

		public string FileDesc
		{
			get
			{
				return textFileDesc.Text;
			}
			set
			{
				textFileDesc.Text = value;
			}
		}

		public string FileKeyword
		{
			get
			{
				return textFileKeyword.Text;
			}
			set
			{
				textFileKeyword.Text = value;
			}
		}

		public string FileName
		{
			get
			{
				return textFileName.Text;
			}
			set
			{
				textFileName.Text = value;
			}
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

		public string EntitySysDocID
		{
			get;
			set;
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

		public EditFileDialog()
		{
			InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
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

		private void button2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private bool ValidateData()
		{
			if (textFileName.Text.Length == 0)
			{
				return false;
			}
			if (textFileDesc.Text.Trim().Length == 0 && textFileKeyword.Text.Trim().Length == 0)
			{
				DialogResult dialogResult = ErrorHelper.QuestionMessageYesNo("Description or Keyword field not provided. Are you sure you want to continue");
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
				_ = 6;
				return true;
			}
			return true;
		}

		private void UploadFileDialog_Activated(object sender, EventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (SaveData())
			{
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
				return Factory.EntityDocSystem.UpdateEntityDoc(currentData);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void LoadData(EntityTypesEnum entityType, string entityID, string sysDocID, string fileName)
		{
			try
			{
				if (!(entityID == "") || entityType != 0 || !(fileName.Trim() == ""))
				{
					currentData = Factory.EntityDocSystem.GetEntityDocByID(entityType, entityID, fileName);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						isNewRecord = true;
					}
					else
					{
						EntityID = entityID;
						EntityType = entityType;
						FileName = fileName;
						FillData();
						isNewRecord = false;
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textFileName.Text = FileName;
				string text3 = textFileDesc.Text = (FileDesc = dataRow["EntityDocDesc"].ToString());
				text3 = (textFileKeyword.Text = (FileKeyword = dataRow["EntityDocKeyword"].ToString()));
				textFileDesc.Focus();
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null)
				{
					currentData = new EntityDocData();
				}
				DataRow dataRow = currentData.EntityDocTable.Rows[0];
				dataRow.BeginEdit();
				dataRow["EntityID"] = EntityID;
				dataRow["EntityType"] = (int)EntityType;
				dataRow["EntityDocName"] = FileName;
				string text2 = (string)(dataRow["EntityDocDesc"] = (FileDesc = textFileDesc.Text.Trim()));
				text2 = (string)(dataRow["EntityDocKeyword"] = (FileKeyword = textFileKeyword.Text.Trim()));
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
			textFileName.Clear();
			textFileDesc.Clear();
			textFileKeyword.Clear();
		}

		private string GetFileName(string filePath)
		{
			if (filePath != string.Empty)
			{
				return filePath.Substring(checked(FilePath.LastIndexOf("\\") + 1));
			}
			return string.Empty;
		}

		private void EditFileDialog_Load(object sender, EventArgs e)
		{
			try
			{
				LoadData(entityType, entityID, EntitySysDocID, FileName);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.EditFileDialog));
			buttonCancel = new System.Windows.Forms.Button();
			buttonSave = new System.Windows.Forms.Button();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			textFileDesc = new Micromind.UISupport.MMTextBox();
			textFileName = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textFileKeyword = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			linePanelDown = new Micromind.UISupport.Line();
			SuspendLayout();
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(387, 222);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.Location = new System.Drawing.Point(284, 221);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(97, 25);
			buttonSave.TabIndex = 2;
			buttonSave.Text = "Save";
			buttonSave.UseVisualStyleBackColor = true;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			textFileDesc.BackColor = System.Drawing.Color.White;
			textFileDesc.IsComboTextBox = false;
			textFileDesc.Location = new System.Drawing.Point(108, 55);
			textFileDesc.MaxLength = 255;
			textFileDesc.Name = "textFileDesc";
			textFileDesc.Size = new System.Drawing.Size(336, 20);
			textFileDesc.TabIndex = 0;
			textFileName.BackColor = System.Drawing.Color.WhiteSmoke;
			textFileName.IsComboTextBox = false;
			textFileName.Location = new System.Drawing.Point(108, 29);
			textFileName.MaxLength = 1000;
			textFileName.Name = "textFileName";
			textFileName.ReadOnly = true;
			textFileName.Size = new System.Drawing.Size(336, 20);
			textFileName.TabIndex = 4;
			textFileName.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(10, 58);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(63, 13);
			mmLabel3.TabIndex = 24;
			mmLabel3.Text = "Description:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(10, 32);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(57, 13);
			mmLabel1.TabIndex = 5;
			mmLabel1.Text = "File Name:";
			textFileKeyword.BackColor = System.Drawing.Color.White;
			textFileKeyword.IsComboTextBox = false;
			textFileKeyword.Location = new System.Drawing.Point(108, 78);
			textFileKeyword.MaxLength = 255;
			textFileKeyword.Name = "textFileKeyword";
			textFileKeyword.Size = new System.Drawing.Size(336, 20);
			textFileKeyword.TabIndex = 1;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(10, 81);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(93, 13);
			mmLabel4.TabIndex = 24;
			mmLabel4.Text = "Search Keywords:";
			linePanelDown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 209);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(488, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(482, 253);
			base.Controls.Add(textFileDesc);
			base.Controls.Add(textFileName);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(textFileKeyword);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(buttonSave);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EditFileDialog";
			Text = "Edit File";
			base.Activated += new System.EventHandler(UploadFileDialog_Activated);
			base.Load += new System.EventHandler(EditFileDialog_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
