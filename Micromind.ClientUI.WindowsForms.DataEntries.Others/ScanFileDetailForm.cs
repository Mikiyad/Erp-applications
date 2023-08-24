using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WIA;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ScanFileDetailForm : Form
	{
		private EntityDocData currentData;

		private string entityID = "";

		private int entityRowIndex = -1;

		private EntityTypesEnum entityType = EntityTypesEnum.Customers;

		private int newDoc;

		private int cmbCMIndex;

		private Stopwatch sw = new Stopwatch();

		private decimal nudWidthNew;

		private string filedesc = "";

		private string filekeyword = "";

		private string tempFileName = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private Label label7;

		private ComboBox cmbColorMode;

		private CheckBox cbxCustomPixel;

		private Label label6;

		private Label label5;

		private NumericUpDown nudHeightInch;

		private NumericUpDown nudWidthInch;

		private Label label2;

		private NumericUpDown nudRes;

		private Label label4;

		private Label label3;

		private NumericUpDown nudHeight;

		private NumericUpDown nudWidth;

		private PictureBox picScan;

		private TextBox txtWT;

		private Label label8;

		private NumericUpDown nudTime;

		private ProgressBar pbWaiting;

		private Button btnStopScan;

		private Button btnStartScan;

		private MMLabel mmLabel3;

		private MMTextBox textFilePath;

		private MMTextBox textFileKeyword;

		private MMTextBox textFileDesc;

		private MMLabel mmLabel4;

		private MMLabel mmLabel1;

		private GroupBox groupBox1;

		private MMLabel mmLabel2;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private TextBox txtPath;

		private Button btnSelectPath;

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

		public decimal NudWidth
		{
			get
			{
				return nudWidthNew;
			}
			set
			{
				nudWidth.Value = (nudWidthNew = value);
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

		public string FilePath
		{
			get
			{
				return txtPath.Text;
			}
			set
			{
				txtPath.Text = value;
			}
		}

		public string FileDesc
		{
			get
			{
				return filedesc;
			}
			set
			{
				filedesc = value;
			}
		}

		public string FileKeyword
		{
			get
			{
				return filekeyword;
			}
			set
			{
				filekeyword = value;
			}
		}

		public string FileName
		{
			get
			{
				return tempFileName;
			}
			set
			{
				tempFileName = value;
			}
		}

		public ScanFileDetailForm()
		{
			InitializeComponent();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SaveData();
			Close();
		}

		private void btnStartScan_Click(object sender, EventArgs e)
		{
			if (textFilePath.Text != "")
			{
				ScanDoc();
				btnStartScan.Enabled = false;
				btnStopScan.Enabled = true;
				cmbColorMode.Enabled = false;
				nudWidthInch.Enabled = false;
				nudHeightInch.Enabled = false;
				nudRes.Enabled = false;
				cbxCustomPixel.Enabled = false;
				nudHeight.Enabled = false;
				nudWidth.Enabled = false;
				cmbCMIndex = cmbColorMode.SelectedIndex;
			}
			else
			{
				ErrorHelper.InformationMessage("Assign a File Name");
			}
		}

		private void btnStopScan_Click(object sender, EventArgs e)
		{
			btnStartScan.Enabled = true;
			btnStopScan.Enabled = false;
			cmbColorMode.Enabled = true;
			btnStartScan.Enabled = true;
			btnStopScan.Enabled = false;
			cmbColorMode.Enabled = true;
			nudWidthInch.Enabled = true;
			nudHeightInch.Enabled = true;
			nudRes.Enabled = true;
			cbxCustomPixel.Enabled = true;
			if (cbxCustomPixel.Checked)
			{
				nudHeight.Enabled = true;
				nudWidth.Enabled = true;
			}
			sw.Reset();
			sw.Stop();
		}

		private void btnSelectPath_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowNewFolderButton = true;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				txtPath.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void ScanFileForm_Load(object sender, EventArgs e)
		{
			txtPath.Text = Path.GetTempPath();
			nudHeightInch.Value = 11m;
			nudWidthInch.Value = 8m;
			cmbColorMode.SelectedIndex = 1;
			nudRes.Value = 200m;
			textFilePath.Text = DateTime.Today.ToLongDateString();
		}

		private static void AdjustScannerSettings(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel, int scanStartTopPixel, int scanWidthPixels, int scanHeightPixels, int brightnessPercents, int contrastPercents, int colorMode)
		{
			SetWIAProperty(scannnerItem.Properties, "6147", scanResolutionDPI);
			SetWIAProperty(scannnerItem.Properties, "6148", scanResolutionDPI);
			SetWIAProperty(scannnerItem.Properties, "6149", scanStartLeftPixel);
			SetWIAProperty(scannnerItem.Properties, "6150", scanStartTopPixel);
			SetWIAProperty(scannnerItem.Properties, "6151", scanWidthPixels);
			SetWIAProperty(scannnerItem.Properties, "6152", scanHeightPixels);
			SetWIAProperty(scannnerItem.Properties, "6154", brightnessPercents);
			SetWIAProperty(scannnerItem.Properties, "6155", contrastPercents);
			SetWIAProperty(scannnerItem.Properties, "6146", colorMode);
		}

		private static void SetWIAProperty(IProperties properties, object propName, object propValue)
		{
			
		}

		private static void SaveImageToJPEG(ImageFile image, string fileName)
		{
			ImageProcess imageProcess = new ImageProcessClass();
			object Index = "Convert";
			string filterID = imageProcess.FilterInfos[ref Index].FilterID;
			imageProcess.Filters.Add(filterID);
			SetWIAProperty(imageProcess.Filters[imageProcess.Filters.Count].Properties, "FormatID", "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}");
			image = imageProcess.Apply(image);
			image.SaveFile(fileName);
		}

		private static void SaveImageToPdf(ImageFile image, string fileName)
		{
			ImageProcessClass imageProcessClass = new ImageProcessClass();
			object Index = "Convert";
			string filterID = ((IImageProcess)imageProcessClass).FilterInfos[ref Index].FilterID;
			((IImageProcess)imageProcessClass).Filters.Add(filterID);
			image = ((IImageProcess)imageProcessClass).Apply(image);
			image.SaveFile(fileName);
		}

		private void ScanDoc()
		{
			try
			{
				DateTime now = DateTime.Now;
				textFilePath.Text = now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString() + now.Millisecond.ToString();
				CommonDialogClass commonDialogClass = new CommonDialogClass();
				Device device = commonDialogClass.ShowSelectDevice(WiaDeviceType.ScannerDeviceType);
				if (device != null)
				{
					Item item = device.Items[1];
					AdjustScannerSettings(item, (int)nudRes.Value, 0, 0, (int)nudWidth.Value, (int)nudHeight.Value, 0, 0, cmbCMIndex);
					object obj = commonDialogClass.ShowTransfer(item, "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}");
					if (obj != null)
					{
						ImageFile image = (ImageFile)obj;
						string text = "";
						Directory.GetFiles(txtPath.Text, "*.JPEG,*.pdf");
						string text2 = "";
						text2 = ((!radioButton1.Checked) ? ".pdf" : ".JPEG");
						try
						{
							text = txtPath.Text + "\\" + textFilePath.Text.Trim() + text2;
							FileName = textFilePath.Text.Trim() + text2;
						}
						catch (Exception)
						{
							text = txtPath.Text + "\\" + textFilePath.Text.Trim() + text2;
							FileName = textFilePath.Text.Trim() + text2;
						}
						if (radioButton1.Checked)
						{
							SaveImageToJPEG(image, text);
						}
						else
						{
							SaveImageToPdf(image, text);
						}
						picScan.ImageLocation = text;
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Check the Device Connection \n or \n Change the Scanner Device", "Devic Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private bool SaveData()
		{
			if (txtPath.Text == "")
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				return Factory.EntityDocSystem.SaveEntityDoc(currentData);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetData()
		{
			try
			{
				currentData = new EntityDocData();
				DataRow dataRow = currentData.EntityDocTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EntityID"] = EntityID;
				dataRow["RowIndex"] = EntityRowIndex;
				dataRow["EntityType"] = EntityType;
				if (EntitySysDocID != "")
				{
					dataRow["EntitySysDocID"] = EntitySysDocID;
				}
				dataRow["EntityDocName"] = FileName;
				dataRow["EntityDocDesc"] = textFileDesc.Text;
				dataRow["EntityDocKeyword"] = textFileKeyword.Text;
				byte[] array = (byte[])(dataRow["FileData"] = GetInputFile(txtPath.Text + "\\" + FileName));
				dataRow.EndEdit();
				currentData.EntityDocTable.Rows.Add(dataRow);
				return true;
			}
			catch (FileNotFoundException ex)
			{
				ErrorHelper.WarningMessage("Could not find the file: " + ex.FileName);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private byte[] GetInputFile(string filePath)
		{
			byte[] result = null;
			if (filePath == "")
			{
				filePath = FilePath;
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

		private string GetFileName(string filePath)
		{
			if (filePath != string.Empty)
			{
				return filePath.Substring(checked(FilePath.LastIndexOf("\\") + 1));
			}
			return string.Empty;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.ScanFileDetailForm));
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			label7 = new System.Windows.Forms.Label();
			cmbColorMode = new System.Windows.Forms.ComboBox();
			cbxCustomPixel = new System.Windows.Forms.CheckBox();
			label6 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			nudHeightInch = new System.Windows.Forms.NumericUpDown();
			nudWidthInch = new System.Windows.Forms.NumericUpDown();
			label2 = new System.Windows.Forms.Label();
			nudRes = new System.Windows.Forms.NumericUpDown();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			nudHeight = new System.Windows.Forms.NumericUpDown();
			nudWidth = new System.Windows.Forms.NumericUpDown();
			picScan = new System.Windows.Forms.PictureBox();
			txtWT = new System.Windows.Forms.TextBox();
			label8 = new System.Windows.Forms.Label();
			nudTime = new System.Windows.Forms.NumericUpDown();
			pbWaiting = new System.Windows.Forms.ProgressBar();
			btnStopScan = new System.Windows.Forms.Button();
			btnStartScan = new System.Windows.Forms.Button();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textFilePath = new Micromind.UISupport.MMTextBox();
			textFileKeyword = new Micromind.UISupport.MMTextBox();
			textFileDesc = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			groupBox1 = new System.Windows.Forms.GroupBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			radioButton2 = new System.Windows.Forms.RadioButton();
			radioButton1 = new System.Windows.Forms.RadioButton();
			txtPath = new System.Windows.Forms.TextBox();
			btnSelectPath = new System.Windows.Forms.Button();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudHeightInch).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudWidthInch).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudRes).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudHeight).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudWidth).BeginInit();
			((System.ComponentModel.ISupportInitialize)picScan).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudTime).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 587);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(791, 40);
			panelButtons.TabIndex = 2;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(579, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(791, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(681, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(82, 158);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(61, 13);
			label7.TabIndex = 62;
			label7.Text = "Color Mode";
			cmbColorMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cmbColorMode.FormattingEnabled = true;
			cmbColorMode.Items.AddRange(new object[3]
			{
				"Black and White",
				"Color",
				"Gray Scale"
			});
			cmbColorMode.Location = new System.Drawing.Point(147, 150);
			cmbColorMode.Name = "cmbColorMode";
			cmbColorMode.Size = new System.Drawing.Size(133, 21);
			cmbColorMode.TabIndex = 61;
			cbxCustomPixel.AutoSize = true;
			cbxCustomPixel.Location = new System.Drawing.Point(48, 259);
			cbxCustomPixel.Name = "cbxCustomPixel";
			cbxCustomPixel.Size = new System.Drawing.Size(113, 17);
			cbxCustomPixel.TabIndex = 60;
			cbxCustomPixel.Text = "Custom Pixel Units";
			cbxCustomPixel.UseVisualStyleBackColor = true;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(245, 285);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(63, 13);
			label6.TabIndex = 59;
			label6.Text = "Pixel Height";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(174, 285);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(60, 13);
			label5.TabIndex = 58;
			label5.Text = "Pixel Width";
			nudHeightInch.Location = new System.Drawing.Point(125, 185);
			nudHeightInch.Maximum = new decimal(new int[4]
			{
				45,
				0,
				0,
				0
			});
			nudHeightInch.Name = "nudHeightInch";
			nudHeightInch.Size = new System.Drawing.Size(65, 20);
			nudHeightInch.TabIndex = 57;
			nudHeightInch.Value = new decimal(new int[4]
			{
				11,
				0,
				0,
				0
			});
			nudWidthInch.Location = new System.Drawing.Point(48, 185);
			nudWidthInch.Maximum = new decimal(new int[4]
			{
				40,
				0,
				0,
				0
			});
			nudWidthInch.Name = "nudWidthInch";
			nudWidthInch.Size = new System.Drawing.Size(65, 20);
			nudWidthInch.TabIndex = 56;
			nudWidthInch.Value = new decimal(new int[4]
			{
				8,
				0,
				0,
				0
			});
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(204, 209);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(57, 13);
			label2.TabIndex = 55;
			label2.Text = "Resolution";
			nudRes.Increment = new decimal(new int[4]
			{
				100,
				0,
				0,
				0
			});
			nudRes.Location = new System.Drawing.Point(200, 185);
			nudRes.Maximum = new decimal(new int[4]
			{
				2400,
				0,
				0,
				0
			});
			nudRes.Minimum = new decimal(new int[4]
			{
				75,
				0,
				0,
				0
			});
			nudRes.Name = "nudRes";
			nudRes.Size = new System.Drawing.Size(65, 20);
			nudRes.TabIndex = 54;
			nudRes.Value = new decimal(new int[4]
			{
				200,
				0,
				0,
				0
			});
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(133, 209);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(38, 13);
			label4.TabIndex = 53;
			label4.Text = "Height";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(56, 209);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(35, 13);
			label3.TabIndex = 52;
			label3.Text = "Width";
			nudHeight.Location = new System.Drawing.Point(244, 256);
			nudHeight.Maximum = new decimal(new int[4]
			{
				99999999,
				0,
				0,
				0
			});
			nudHeight.Name = "nudHeight";
			nudHeight.ReadOnly = true;
			nudHeight.Size = new System.Drawing.Size(65, 20);
			nudHeight.TabIndex = 51;
			nudHeight.Value = new decimal(new int[4]
			{
				825,
				0,
				0,
				0
			});
			nudWidth.Location = new System.Drawing.Point(173, 256);
			nudWidth.Maximum = new decimal(new int[4]
			{
				99999999,
				0,
				0,
				0
			});
			nudWidth.Name = "nudWidth";
			nudWidth.ReadOnly = true;
			nudWidth.Size = new System.Drawing.Size(65, 20);
			nudWidth.TabIndex = 50;
			nudWidth.Value = new decimal(new int[4]
			{
				630,
				0,
				0,
				0
			});
			picScan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			picScan.Location = new System.Drawing.Point(332, 53);
			picScan.Name = "picScan";
			picScan.Size = new System.Drawing.Size(450, 530);
			picScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			picScan.TabIndex = 45;
			picScan.TabStop = false;
			txtWT.BackColor = System.Drawing.Color.Black;
			txtWT.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtWT.Font = new System.Drawing.Font("Consolas", 18f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			txtWT.ForeColor = System.Drawing.Color.Chartreuse;
			txtWT.Location = new System.Drawing.Point(413, 317);
			txtWT.Name = "txtWT";
			txtWT.Size = new System.Drawing.Size(260, 29);
			txtWT.TabIndex = 49;
			txtWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtWT.Visible = false;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(676, 252);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(68, 13);
			label8.TabIndex = 48;
			label8.Text = "Waiting Sec.";
			label8.Visible = false;
			nudTime.Location = new System.Drawing.Point(684, 281);
			nudTime.Minimum = new decimal(new int[4]
			{
				2,
				0,
				0,
				0
			});
			nudTime.Name = "nudTime";
			nudTime.Size = new System.Drawing.Size(49, 20);
			nudTime.TabIndex = 47;
			nudTime.Value = new decimal(new int[4]
			{
				10,
				0,
				0,
				0
			});
			nudTime.Visible = false;
			pbWaiting.Location = new System.Drawing.Point(411, 279);
			pbWaiting.Name = "pbWaiting";
			pbWaiting.Size = new System.Drawing.Size(262, 25);
			pbWaiting.TabIndex = 46;
			pbWaiting.Visible = false;
			btnStopScan.Enabled = false;
			btnStopScan.Location = new System.Drawing.Point(235, 308);
			btnStopScan.Name = "btnStopScan";
			btnStopScan.Size = new System.Drawing.Size(75, 23);
			btnStopScan.TabIndex = 74;
			btnStopScan.Text = "Stop";
			btnStopScan.UseVisualStyleBackColor = true;
			btnStopScan.Click += new System.EventHandler(btnStopScan_Click);
			btnStartScan.Location = new System.Drawing.Point(147, 308);
			btnStartScan.Name = "btnStartScan";
			btnStartScan.Size = new System.Drawing.Size(75, 23);
			btnStartScan.TabIndex = 73;
			btnStartScan.Text = "Start";
			btnStartScan.UseVisualStyleBackColor = true;
			btnStartScan.Click += new System.EventHandler(btnStartScan_Click);
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(19, 81);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(63, 13);
			mmLabel3.TabIndex = 69;
			mmLabel3.Text = "Description:";
			textFilePath.BackColor = System.Drawing.Color.White;
			textFilePath.CustomReportFieldName = "";
			textFilePath.CustomReportKey = "";
			textFilePath.CustomReportValueType = 1;
			textFilePath.IsComboTextBox = false;
			textFilePath.Location = new System.Drawing.Point(118, 56);
			textFilePath.MaxLength = 1000;
			textFilePath.Name = "textFilePath";
			textFilePath.Size = new System.Drawing.Size(199, 20);
			textFilePath.TabIndex = 66;
			textFilePath.TabStop = false;
			textFilePath.Visible = false;
			textFileKeyword.BackColor = System.Drawing.Color.White;
			textFileKeyword.CustomReportFieldName = "";
			textFileKeyword.CustomReportKey = "";
			textFileKeyword.CustomReportValueType = 1;
			textFileKeyword.IsComboTextBox = false;
			textFileKeyword.Location = new System.Drawing.Point(118, 105);
			textFileKeyword.MaxLength = 255;
			textFileKeyword.Name = "textFileKeyword";
			textFileKeyword.Size = new System.Drawing.Size(199, 20);
			textFileKeyword.TabIndex = 65;
			textFileDesc.BackColor = System.Drawing.Color.White;
			textFileDesc.CustomReportFieldName = "";
			textFileDesc.CustomReportKey = "";
			textFileDesc.CustomReportValueType = 1;
			textFileDesc.IsComboTextBox = false;
			textFileDesc.Location = new System.Drawing.Point(118, 81);
			textFileDesc.MaxLength = 255;
			textFileDesc.Name = "textFileDesc";
			textFileDesc.Size = new System.Drawing.Size(199, 20);
			textFileDesc.TabIndex = 64;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(19, 108);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(93, 13);
			mmLabel4.TabIndex = 68;
			mmLabel4.Text = "Search Keywords:";
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(23, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(59, 13);
			mmLabel1.TabIndex = 67;
			mmLabel1.Text = "Select File:";
			mmLabel1.Visible = false;
			groupBox1.Location = new System.Drawing.Point(15, 128);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(311, 218);
			groupBox1.TabIndex = 75;
			groupBox1.TabStop = false;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(64, 402);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(49, 13);
			mmLabel2.TabIndex = 80;
			mmLabel2.Text = "Save as:";
			mmLabel2.Visible = false;
			radioButton2.AutoSize = true;
			radioButton2.Location = new System.Drawing.Point(176, 400);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new System.Drawing.Size(41, 17);
			radioButton2.TabIndex = 79;
			radioButton2.Text = "Pdf";
			radioButton2.UseVisualStyleBackColor = true;
			radioButton2.Visible = false;
			radioButton1.AutoSize = true;
			radioButton1.Checked = true;
			radioButton1.Location = new System.Drawing.Point(119, 399);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new System.Drawing.Size(48, 17);
			radioButton1.TabIndex = 78;
			radioButton1.TabStop = true;
			radioButton1.Text = "Jpeg";
			radioButton1.UseVisualStyleBackColor = true;
			radioButton1.Visible = false;
			txtPath.Location = new System.Drawing.Point(54, 432);
			txtPath.Name = "txtPath";
			txtPath.Size = new System.Drawing.Size(174, 20);
			txtPath.TabIndex = 77;
			txtPath.Visible = false;
			btnSelectPath.Location = new System.Drawing.Point(234, 425);
			btnSelectPath.Name = "btnSelectPath";
			btnSelectPath.Size = new System.Drawing.Size(72, 29);
			btnSelectPath.TabIndex = 76;
			btnSelectPath.Text = "Select Path";
			btnSelectPath.UseVisualStyleBackColor = true;
			btnSelectPath.Visible = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(791, 627);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(radioButton2);
			base.Controls.Add(radioButton1);
			base.Controls.Add(txtPath);
			base.Controls.Add(btnSelectPath);
			base.Controls.Add(btnStopScan);
			base.Controls.Add(btnStartScan);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(textFileDesc);
			base.Controls.Add(textFileKeyword);
			base.Controls.Add(textFilePath);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(label7);
			base.Controls.Add(cmbColorMode);
			base.Controls.Add(cbxCustomPixel);
			base.Controls.Add(label6);
			base.Controls.Add(label5);
			base.Controls.Add(nudHeightInch);
			base.Controls.Add(nudWidthInch);
			base.Controls.Add(label2);
			base.Controls.Add(nudRes);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(nudHeight);
			base.Controls.Add(nudWidth);
			base.Controls.Add(picScan);
			base.Controls.Add(txtWT);
			base.Controls.Add(label8);
			base.Controls.Add(nudTime);
			base.Controls.Add(pbWaiting);
			base.Controls.Add(panelButtons);
			base.Controls.Add(groupBox1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(450, 215);
			base.Name = "ScanFileDetailForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Scan File";
			base.Load += new System.EventHandler(ScanFileForm_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)nudHeightInch).EndInit();
			((System.ComponentModel.ISupportInitialize)nudWidthInch).EndInit();
			((System.ComponentModel.ISupportInitialize)nudRes).EndInit();
			((System.ComponentModel.ISupportInitialize)nudHeight).EndInit();
			((System.ComponentModel.ISupportInitialize)nudWidth).EndInit();
			((System.ComponentModel.ISupportInitialize)picScan).EndInit();
			((System.ComponentModel.ISupportInitialize)nudTime).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
