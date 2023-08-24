using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.DataCaches.Others;
using Micromind.UISupport;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class PrintTemplateReaderDialog : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private byte[] stream;

		private XPButton buttonOK;

		private XPButton buttonCancel;

		private Line line1;

		private MMLabel labelContact;

		private XPButton buttonBrowse;

		private OpenFileDialog openFileDialog;

		private MMLabel baLabel1;

		private MMTextBox textBoxFileName;

		private MMTextBox textBoxTemplateName;

		private RadioButton radioButtonSingleTemplate;

		private RadioButton radioButtonMultipleTemplates;

		private RadioButton radioButtonDirectory;

		private Label label1;

		private FolderBrowserDialog folderBrowserDialog;

		private CheckBox checkBoxOverwrite;

		private Container components;

		private string[] fileNames = new string[0];

		public PrintTemplateReaderDialog()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
				if (stream != null)
				{
					stream = null;
				}
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			buttonOK = new Micromind.UISupport.XPButton();
			buttonCancel = new Micromind.UISupport.XPButton();
			line1 = new Micromind.UISupport.Line();
			labelContact = new Micromind.UISupport.MMLabel();
			textBoxFileName = new Micromind.UISupport.MMTextBox();
			buttonBrowse = new Micromind.UISupport.XPButton();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			textBoxTemplateName = new Micromind.UISupport.MMTextBox();
			baLabel1 = new Micromind.UISupport.MMLabel();
			radioButtonSingleTemplate = new System.Windows.Forms.RadioButton();
			radioButtonMultipleTemplates = new System.Windows.Forms.RadioButton();
			radioButtonDirectory = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			checkBoxOverwrite = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(344, 168);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(80, 24);
			buttonOK.TabIndex = 8;
			buttonOK.Text = "&Load";
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(432, 168);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(80, 24);
			buttonCancel.TabIndex = 9;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(5, 160);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(507, 1);
			line1.TabIndex = 6;
			line1.TabStop = false;
			labelContact.AutoSize = true;
			labelContact.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelContact.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelContact.IsFieldHeader = false;
			labelContact.Location = new System.Drawing.Point(13, 96);
			labelContact.Name = "labelContact";
			labelContact.PenWidth = 1f;
			labelContact.ShowBorder = false;
			labelContact.Size = new System.Drawing.Size(26, 16);
			labelContact.TabIndex = 3;
			labelContact.Text = "File:";
			textBoxFileName.AcceptsReturn = false;
			textBoxFileName.AcceptsTab = false;
			textBoxFileName.AutoSize = true;
			textBoxFileName.BackColor = System.Drawing.Color.White;
			textBoxFileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxFileName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxFileName.HideSelection = true;
			textBoxFileName.IsComboTextBox = false;
			textBoxFileName.Lines = new string[0];
			textBoxFileName.Location = new System.Drawing.Point(109, 96);
			textBoxFileName.MaxLength = 64;
			textBoxFileName.Multiline = false;
			textBoxFileName.Name = "textBoxFileName";
			textBoxFileName.PasswordChar = '\0';
			textBoxFileName.ReadOnly = true;
			textBoxFileName.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxFileName.Size = new System.Drawing.Size(339, 20);
			textBoxFileName.TabIndex = 4;
			textBoxFileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			textBoxFileName.WordWrap = true;
			buttonBrowse.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonBrowse.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonBrowse.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonBrowse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonBrowse.Location = new System.Drawing.Point(455, 92);
			buttonBrowse.Name = "buttonBrowse";
			buttonBrowse.Size = new System.Drawing.Size(57, 28);
			buttonBrowse.TabIndex = 5;
			buttonBrowse.Text = "&Browse...";
			buttonBrowse.Click += new System.EventHandler(buttonBrowse_Click);
			textBoxTemplateName.AcceptsReturn = false;
			textBoxTemplateName.AcceptsTab = false;
			textBoxTemplateName.AutoSize = true;
			textBoxTemplateName.BackColor = System.Drawing.Color.White;
			textBoxTemplateName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxTemplateName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxTemplateName.HideSelection = true;
			textBoxTemplateName.IsComboTextBox = false;
			textBoxTemplateName.Lines = new string[0];
			textBoxTemplateName.Location = new System.Drawing.Point(109, 120);
			textBoxTemplateName.MaxLength = 64;
			textBoxTemplateName.Multiline = false;
			textBoxTemplateName.Name = "textBoxTemplateName";
			textBoxTemplateName.PasswordChar = '\0';
			textBoxTemplateName.ReadOnly = false;
			textBoxTemplateName.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxTemplateName.Size = new System.Drawing.Size(339, 20);
			textBoxTemplateName.TabIndex = 7;
			textBoxTemplateName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			textBoxTemplateName.WordWrap = true;
			baLabel1.AutoSize = true;
			baLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			baLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			baLabel1.IsFieldHeader = false;
			baLabel1.Location = new System.Drawing.Point(13, 120);
			baLabel1.Name = "baLabel1";
			baLabel1.PenWidth = 1f;
			baLabel1.ShowBorder = false;
			baLabel1.Size = new System.Drawing.Size(88, 16);
			baLabel1.TabIndex = 6;
			baLabel1.Text = "&Template Name:";
			radioButtonSingleTemplate.Checked = true;
			radioButtonSingleTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonSingleTemplate.Location = new System.Drawing.Point(13, 40);
			radioButtonSingleTemplate.Name = "radioButtonSingleTemplate";
			radioButtonSingleTemplate.Size = new System.Drawing.Size(136, 16);
			radioButtonSingleTemplate.TabIndex = 0;
			radioButtonSingleTemplate.TabStop = true;
			radioButtonSingleTemplate.Text = "Single Template";
			radioButtonSingleTemplate.CheckedChanged += new System.EventHandler(radioButtonSingleTemplate_CheckedChanged);
			radioButtonMultipleTemplates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonMultipleTemplates.Location = new System.Drawing.Point(157, 40);
			radioButtonMultipleTemplates.Name = "radioButtonMultipleTemplates";
			radioButtonMultipleTemplates.Size = new System.Drawing.Size(176, 16);
			radioButtonMultipleTemplates.TabIndex = 1;
			radioButtonMultipleTemplates.Text = "Multiple Templates";
			radioButtonDirectory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonDirectory.Location = new System.Drawing.Point(341, 40);
			radioButtonDirectory.Name = "radioButtonDirectory";
			radioButtonDirectory.Size = new System.Drawing.Size(176, 16);
			radioButtonDirectory.TabIndex = 2;
			radioButtonDirectory.Text = "Templates Directory";
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.Location = new System.Drawing.Point(11, 8);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(368, 16);
			label1.TabIndex = 10;
			label1.Text = "Select how you would like to load print templates.";
			folderBrowserDialog.ShowNewFolderButton = false;
			checkBoxOverwrite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxOverwrite.Location = new System.Drawing.Point(341, 69);
			checkBoxOverwrite.Name = "checkBoxOverwrite";
			checkBoxOverwrite.Size = new System.Drawing.Size(176, 16);
			checkBoxOverwrite.TabIndex = 11;
			checkBoxOverwrite.Text = "Overwrite existing templates";
			base.AcceptButton = buttonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(522, 199);
			base.Controls.Add(checkBoxOverwrite);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonDirectory);
			base.Controls.Add(radioButtonMultipleTemplates);
			base.Controls.Add(radioButtonSingleTemplate);
			base.Controls.Add(textBoxTemplateName);
			base.Controls.Add(baLabel1);
			base.Controls.Add(labelContact);
			base.Controls.Add(buttonBrowse);
			base.Controls.Add(textBoxFileName);
			base.Controls.Add(line1);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonOK);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PrintTemplateReaderDialog";
			Text = "Load Print Templates";
			base.Load += new System.EventHandler(ApplicationActivatorForm_Load);
			ResumeLayout(false);
		}

		private bool IsValid()
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				if (radioButtonSingleTemplate.Checked && !checkBoxOverwrite.Checked && textBoxTemplateName.Text.Trim() != string.Empty && Factory.PrintTemplateSystem.ExistPrintTemplate(textBoxTemplateName.Text))
				{
					ErrorHelper.WarningMessage("This template name already exists.", "Please type a different template name.");
					textBoxTemplateName.Focus();
					textBoxTemplateName.SelectAll();
					return false;
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
			return true;
		}

		private bool LoadTemplates()
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				if (radioButtonSingleTemplate.Checked && stream != null)
				{
					if (!Factory.PrintTemplateSystem.LoadPrintTemplateStream(stream, textBoxTemplateName.Text, checkBoxOverwrite.Checked))
					{
						ErrorHelper.WarningMessage("Unable to load the template.", "Please make sure template file has correct format.");
						return false;
					}
				}
				else if (radioButtonMultipleTemplates.Checked || radioButtonDirectory.Checked)
				{
					if (radioButtonDirectory.Checked)
					{
						ReadFiles();
					}
					string[] array = fileNames;
					foreach (string fileName in array)
					{
						if (GetName(fileName))
						{
							Factory.PrintTemplateSystem.LoadPrintTemplateStream(stream, "", checkBoxOverwrite.Checked);
						}
					}
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
			return true;
		}

		private void GetSubdirFiles(string dir, string searchPattern, StringCollection strCol)
		{
			string[] directories = Directory.GetDirectories(dir);
			if (directories.Length != 0)
			{
				string[] array = directories;
				foreach (string text in array)
				{
					string[] files = Directory.GetFiles(text, searchPattern);
					strCol.AddRange(files);
					GetSubdirFiles(text, searchPattern, strCol);
				}
			}
		}

		private void ReadFiles()
		{
			string text = textBoxFileName.Text;
			new ArrayList();
			StringCollection stringCollection = new StringCollection();
			string[] files = Directory.GetFiles(text, "*.dat");
			stringCollection.AddRange(files);
			GetSubdirFiles(text, "*.dat", stringCollection);
			fileNames = new string[stringCollection.Count];
			stringCollection.CopyTo(fileNames, 0);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.None;
			if (IsValid() && LoadTemplates())
			{
				if (radioButtonSingleTemplate.Checked)
				{
					ErrorHelper.InformationMessage("Successfuly loaded the template " + textBoxTemplateName.Text);
				}
				else
				{
					ErrorHelper.InformationMessage("Successfuly loaded the selected templates ");
				}
				PrintTemplates.ForceReferesh();
				Close();
				base.DialogResult = DialogResult.OK;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void ApplicationActivatorForm_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SelectFiles()
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = true;
			openFileDialog.InitialDirectory = Application.StartupPath;
			openFileDialog.FileName = "";
			openFileDialog.Filter = "Template Files (*.dat)|*.dat";
			if (radioButtonMultipleTemplates.Checked)
			{
				openFileDialog.Multiselect = true;
			}
			else
			{
				openFileDialog.Multiselect = false;
			}
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			if (radioButtonSingleTemplate.Checked)
			{
				if (GetName(openFileDialog.FileName))
				{
					textBoxFileName.Text = openFileDialog.FileName;
					textBoxTemplateName.Enabled = true;
					textBoxTemplateName.SelectAll();
				}
			}
			else
			{
				fileNames = openFileDialog.FileNames;
				textBoxTemplateName.Enabled = false;
				textBoxFileName.Text = "Multiple Templates";
			}
		}

		private void SelectDirectories()
		{
			folderBrowserDialog.SelectedPath = Application.StartupPath;
			folderBrowserDialog.ShowNewFolderButton = false;
			folderBrowserDialog.Description = "Select a directory that contains templates you would like to load.";
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxFileName.Text = folderBrowserDialog.SelectedPath;
				textBoxTemplateName.Enabled = false;
			}
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			if (!radioButtonDirectory.Checked)
			{
				SelectFiles();
			}
			else
			{
				SelectDirectories();
			}
			base.DialogResult = DialogResult.None;
		}

		private bool GetName(string fileName)
		{
			bool result = true;
			if (stream != null)
			{
				stream = null;
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamReader streamReader = null;
			try
			{
				streamReader = new StreamReader(fileName, Encoding.ASCII);
				new StringBuilder();
				string text = streamReader.ReadLine();
				bool flag = false;
				while (text != null)
				{
					memoryStream.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
					memoryStream.Write(Encoding.ASCII.GetBytes("\n\r"), 0, 2);
					if (text.Trim() != string.Empty && text.Trim().Substring(0, 4).Equals("!@#$"))
					{
						flag = true;
						memoryStream.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
						memoryStream.Write(Encoding.ASCII.GetBytes("\n\r"), 0, 2);
						break;
					}
					text = streamReader.ReadLine();
				}
				if (!flag)
				{
					return false;
				}
				string text2 = streamReader.ReadLine();
				bool flag2 = false;
				while (text2 != null)
				{
					memoryStream.Write(Encoding.ASCII.GetBytes(text2), 0, text2.Length);
					memoryStream.Write(Encoding.ASCII.GetBytes("\n\r"), 0, 2);
					string[] array = text2.Split('=');
					if (array.Length == 2 && !flag2 && array[0].Trim().ToLower() == "name")
					{
						textBoxTemplateName.Text = array[1].Trim();
						flag2 = true;
					}
					text2 = streamReader.ReadLine();
				}
				return result;
			}
			catch (FileNotFoundException)
			{
				ErrorHelper.WarningMessage("Unable to find the file specified.");
				return false;
			}
			catch (IOException e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			catch
			{
				return false;
			}
		}

		private void radioButtonSingleTemplate_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonSingleTemplate.Checked)
			{
				textBoxTemplateName.Enabled = true;
			}
			else
			{
				textBoxTemplateName.Enabled = false;
			}
		}
	}
}
