using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.UISupport;
using Micromind.UISupport.GUIControls.Others;
using NSOCRLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.OCR
{
	public class DocumentOCRForm : Form
	{
		private EntityDocData currentData;

		private DataSet sysDocList;

		private string docIDSeparator = "-";

		private string docNumberTitle = "Document Num:";

		private string currentFilePath = "";

		private string currentFileName = "";

		private TreeNode lastDocumentNode;

		private bool unknownToLastDoc;

		public NSOCRClass NsOCR;

		public int CfgObj;

		public int OcrObj;

		public int ImgObj;

		public int ScanObj;

		public int SvrObj;

		private bool Dwn;

		private Rectangle Frame;

		private Image bmp;

		public Graphics gr;

		private bool NoEvent;

		private bool Inited;

		private int pmBlockTag = -1;

		private OCRScanForm fmScan = new OCRScanForm();

		private OCRProgressForm fmWait = new OCRProgressForm();

		private OCROptionsForm fmOptions = new OCROptionsForm();

		private TreeNode mySelectedNode = new TreeNode();

		private IContainer components;

		private StatusStrip statusStrip1;

		private SplitContainer splitContainer1;

		private Label lbWait;

		private OpenFileDialog opImg;

		private CheckBox cbBin;

		private OpenFileDialog opBlocks;

		private SaveFileDialog svBlocks;

		private ToolStripStatusLabel lbSkew;

		private ToolStripStatusLabel lbSize;

		private ToolStripStatusLabel lbLines;

		private XPButton bkSave;

		private SaveFileDialog svFile;

		private CheckBox cbPixLines;

		private ToolStripStatusLabel lbInverted;

		private CheckBox cbCharRects;

		private TreeView treeView1;

		private FolderBrowserDialog folderBrowserDialog1;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonScan;

		private ToolStripButton toolStripButtonFile;

		private ToolStripButton toolStripButtonFolder;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButton1;

		private XPButton buttonProcess;

		private TextBox textBoxSelectedPath;

		private Label label1;

		private XPButton buttonClear;

		private ImageList imageList1;

		private MMSPictureBox PicBox;

		private ZoomTrackBarControl zoomTrackBarControl1;

		private System.Windows.Forms.ProgressBar progressBarLoad;

		private Label labelFCount;

		private Label labelFRead;

		private Label labelUnRead;

		private Label labelfc;

		private Label labelrc;

		private Label labelurc;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem toolStripMenuItemEdit;

		private ToolStripMenuItem toolStripMenuItemMain;

		private ToolStripMenuItem toolStripMenuItemChild;

		private ToolStripMenuItem toolStripMenuItemRemove;

		public DocumentOCRForm()
		{
			InitializeComponent();
			treeView1.LabelEdit = true;
			treeView1.ImageList = imageList1;
			treeView1.BeforeLabelEdit += TreeView1_BeforeLabelEdit;
			treeView1.DragDrop += DocumentOCRForm_DragDrop;
			treeView1.ItemDrag += TreeView1_ItemDrag;
			treeView1.DragEnter += TreeView1_DragEnter;
			treeView1.AfterSelect += TreeView1_AfterSelect;
			PicBox.ZoomPercentChanged += PicBox_ZoomPercentChanged;
			zoomTrackBarControl1.ValueChanged += ZoomTrackBarControl1_ValueChanged;
			treeView1.KeyDown += TreeView1_KeyDown;
			contextMenuStrip1.ItemClicked += menuStrip_ItemClicked;
			base.FormClosing += DocumentOCRForm_FormClosing;
			Init();
		}

		private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			string text = e.ClickedItem.Text;
			checked
			{
				switch (text)
				{
				default:
					if (text == "Remove")
					{
						treeView1.SelectedNode.Remove();
					}
					break;
				case "Rename":
					if (mySelectedNode != null)
					{
						treeView1.SelectedNode = mySelectedNode;
						treeView1.LabelEdit = true;
						if (!mySelectedNode.IsEditing)
						{
							mySelectedNode.BeginEdit();
						}
					}
					break;
				case "Add Child":
				{
					TreeNode node2 = new TreeNode("Node " + treeView1.SelectedNode.Nodes.Count);
					treeView1.SelectedNode.Nodes.Add(node2);
					break;
				}
				case "Add Main":
				{
					TreeNode parent = treeView1.SelectedNode.Parent;
					TreeNode node = new TreeNode("Main Node");
					if (parent != null)
					{
						parent.Nodes.Insert(parent.Nodes.IndexOf(treeView1.SelectedNode) + 1, node);
					}
					else
					{
						treeView1.Nodes.Insert(treeView1.Nodes.IndexOf(treeView1.SelectedNode) + 1, node);
					}
					break;
				}
				}
			}
		}

		private void DocumentOCRForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				UserPreferences.SaveCurrentUserSetting(base.Name + "Del", toolStripButton1.Checked);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void Init()
		{
			Global.GlobalSettings.LoadFormProperties(this);
			toolStripButton1.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "Del", defaultValue: false);
		}

		private void TreeView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && treeView1.SelectedNode != null && treeView1.SelectedNode.ImageIndex == 3 && ErrorHelper.QuestionMessageYesNo("Do you want to delete this item?") == DialogResult.Yes)
			{
				treeView1.SelectedNode.Remove();
			}
		}

		private void ZoomTrackBarControl1_ValueChanged(object sender, EventArgs e)
		{
			PicBox.Properties.ZoomPercent = zoomTrackBarControl1.Value;
		}

		private void PicBox_ZoomPercentChanged(object sender, EventArgs e)
		{
			zoomTrackBarControl1.Value = Convert.ToInt16(PicBox.Properties.ZoomPercent);
		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.ImageIndex == 3)
			{
				Bitmap image = new Bitmap(e.Node.Tag.ToString());
				PicBox.Image = image;
			}
			else
			{
				PicBox.Image = null;
			}
		}

		private void TreeView1_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
		{
			TreeNode treeNode = (TreeNode)e.Item;
			if (treeNode.ImageIndex != 0 && treeNode.ImageIndex != 1)
			{
				if (treeNode.ImageIndex == 2)
				{
					DoDragDrop(e.Item, DragDropEffects.None);
				}
				else
				{
					DoDragDrop(e.Item, DragDropEffects.Move);
				}
			}
		}

		private void DocumentOCRForm_DragDrop(object sender, DragEventArgs e)
		{
			Point pt = treeView1.PointToClient(new Point(e.X, e.Y));
			TreeNode nodeAt = treeView1.GetNodeAt(pt);
			TreeNode treeNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
			if (nodeAt.ImageIndex != 3 && nodeAt.ImageIndex != 1 && !treeNode.Equals(nodeAt) && nodeAt != null)
			{
				treeNode.Remove();
				nodeAt.Nodes.Add(treeNode);
				nodeAt.Expand();
			}
		}

		private void TreeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
		}

		public void ShowError(string api, int err)
		{
			MessageBox.Show(api + " Error #" + err.ToString("X"));
		}

		private SysDocTypes GetSysDocType(string sysDocID)
		{
			DataRow[] array = sysDocList.Tables[0].Select("SysDocID = '" + sysDocID + "'");
			if (array.Length != 0)
			{
				return (SysDocTypes)int.Parse(array[0]["SysDocType"].ToString());
			}
			return SysDocTypes.None;
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			try
			{
				sysDocList = Factory.SystemDocumentSystem.GetSystemDocumentList();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			try
			{
				NsOCR = (NSOCRClass)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("C428DD94-A519-4FDB-BD43-10E1B86CE59C")));
				LoadOCRSettings();
			}
			catch (Exception ex)
			{
				string str = "Cannot create NSOCR COM object instance. Possible reasons:\r\n - NSOCR.dll is missed.\r\n - NSOCR.dll was not registered with Regsvr32.\r\n - NSOCR.dll is x32 but this application is x64.\r\n";
				str = str + "\r\n Exception message:\r\n\r\n" + ex.Message;
				ErrorHelper.ErrorMessage(str);
				Close();
				return;
			}
			gr = PicBox.CreateGraphics();
			Inited = true;
			NsOCR.Engine_GetVersion(out string _);
			NsOCR.Engine_SetLicenseKey("");
			NsOCR.Engine_Initialize();
			NsOCR.Cfg_Create(out CfgObj);
			NsOCR.Cfg_LoadOptions(CfgObj, "Config.dat");
			NsOCR.Ocr_Create(CfgObj, out OcrObj);
			NsOCR.Img_Create(OcrObj, out ImgObj);
			NsOCR.Scan_Create(CfgObj, out ScanObj);
			buttonProcess.Enabled = false;
			NoEvent = true;
			NoEvent = false;
			bkSave.Enabled = false;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (Inited)
			{
				if (ImgObj != 0)
				{
					NsOCR.Img_Destroy(ImgObj);
				}
				if (OcrObj != 0)
				{
					NsOCR.Ocr_Destroy(OcrObj);
				}
				if (CfgObj != 0)
				{
					NsOCR.Cfg_Destroy(CfgObj);
				}
				if (ScanObj != 0)
				{
					NsOCR.Scan_Destroy(ScanObj);
				}
				NsOCR.Engine_Uninitialize();
			}
		}

		public bool IsImgLoaded()
		{
			if (!Inited)
			{
				return false;
			}
			if (NsOCR.Img_GetSize(ImgObj, out int Width, out int Height) > 1879048192)
			{
				return false;
			}
			if (Width > 0)
			{
				return Height > 0;
			}
			return false;
		}

		public float GetCurDocScale()
		{
			if (!IsImgLoaded())
			{
				return 1f;
			}
			Rectangle bounds = splitContainer1.Bounds;
			checked
			{
				int num = splitContainer1.SplitterDistance - 15;
				int num2 = bounds.Height - 45;
				NsOCR.Img_GetSize(ImgObj, out int Width, out int Height);
				float num3 = (float)num / (float)Width;
				float num4 = (float)num2 / (float)Height;
				if (num3 > num4)
				{
					return num4;
				}
				return num3;
			}
		}

		public void ShowImage()
		{
			if (!IsImgLoaded())
			{
				return;
			}
			float curDocScale = GetCurDocScale();
			Rectangle rectangle = default(Rectangle);
			int width = bmp.Width;
			int height = bmp.Height;
			Graphics graphics = Graphics.FromImage(new Bitmap(width, height, gr));
			graphics.DrawImage(bmp, 0, 0);
			if (cbPixLines.Checked)
			{
				DrawPixLines(graphics);
			}
			if (cbCharRects.Checked)
			{
				DrawCharRects(graphics);
			}
			Pen pen = new Pen(Color.Green);
			int num = NsOCR.Img_GetBlockCnt(ImgObj);
			checked
			{
				for (int i = 0; i < num; i++)
				{
					NsOCR.Img_GetBlock(ImgObj, i, out int BlkObj);
					NsOCR.Blk_GetRect(BlkObj, out int Xpos, out int Ypos, out int Width, out int Height);
					rectangle.X = (int)(curDocScale * (float)Xpos);
					rectangle.Y = (int)(curDocScale * (float)Ypos);
					rectangle.Width = (int)(curDocScale * (float)Width);
					rectangle.Height = (int)(curDocScale * (float)Height);
					Color color = Color.Red;
					switch (NsOCR.Blk_GetType(BlkObj))
					{
					case 1:
						color = Color.Green;
						break;
					case 6:
						color = Color.Lime;
						break;
					case 2:
						color = Color.Blue;
						break;
					case 7:
						color = Color.Navy;
						break;
					case 4:
						color = Color.Aqua;
						break;
					case 3:
						color = Color.Gray;
						break;
					case 5:
						color = Color.Black;
						break;
					case 8:
						color = Color.Olive;
						break;
					case 9:
						color = Color.Black;
						break;
					}
					pen.Width = 2f;
					pen.Color = color;
					new Font("Arial", 8f);
					new SolidBrush(Color.Lime);
					new PointF(rectangle.X, rectangle.Y);
					rectangle.Width = 12;
					rectangle.Height = 14;
					new SolidBrush(Color.Black);
				}
				if (Dwn)
				{
					rectangle.X = (int)(curDocScale * (float)Frame.Left);
					rectangle.Y = (int)(curDocScale * (float)Frame.Top);
					rectangle.Width = (int)(curDocScale * (float)Frame.Width);
					rectangle.Height = (int)(curDocScale * (float)Frame.Height);
					if (rectangle.Width >= width)
					{
						rectangle.Width = width - 1;
					}
					if (rectangle.Height >= height)
					{
						rectangle.Height = height - 1;
					}
					pen = new Pen(Color.Red);
				}
				GC.Collect();
			}
		}

		public void DrawPixLines(Graphics graphic)
		{
			float curDocScale = GetCurDocScale();
			int num = NsOCR.Img_GetPixLineCnt(ImgObj);
			checked
			{
				for (int i = 0; i < num; i++)
				{
					NsOCR.Img_GetPixLine(ImgObj, i, out int X1pos, out int Y1pos, out int X2pos, out int Y2pos, out int _);
					X1pos = (int)(curDocScale * (float)X1pos);
					Y1pos = (int)(curDocScale * (float)Y1pos);
					X2pos = (int)(curDocScale * (float)X2pos);
					Y2pos = (int)(curDocScale * (float)Y2pos);
					Point pt = new Point(X1pos, Y1pos);
					Point pt2 = new Point(X2pos, Y2pos);
					Pen pen = new Pen(Color.Red);
					pen.Width = 2f;
					graphic.DrawLine(pen, pt, pt2);
				}
			}
		}

		public void DrawCharRects(Graphics graphic)
		{
			float curDocScale = GetCurDocScale();
			int num = NsOCR.Img_GetBlockCnt(ImgObj);
			checked
			{
				for (int i = 0; i < num; i++)
				{
					NsOCR.Img_GetBlock(ImgObj, i, out int BlkObj);
					int num2 = NsOCR.Blk_GetLineCnt(BlkObj);
					for (int j = 0; j < num2; j++)
					{
						int num3 = NsOCR.Blk_GetWordCnt(BlkObj, j);
						for (int k = 0; k < num3; k++)
						{
							int num4 = NsOCR.Blk_GetCharCnt(BlkObj, j, k);
							for (int l = 0; l < num4; l++)
							{
								NsOCR.Blk_GetCharRect(BlkObj, j, k, l, out int Xpos, out int Ypos, out int Width, out int Height);
								Pen pen = new Pen(Color.Blue);
								graphic.DrawRectangle(pen, curDocScale * (float)Xpos, curDocScale * (float)Ypos, curDocScale * (float)Width, curDocScale * (float)Height);
							}
						}
					}
				}
			}
		}

		public void AdjustDocScale()
		{
			try
			{
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void OpenURL(string url)
		{
			Process.Start(url);
		}

		private void bkFile_Click(object sender, EventArgs e)
		{
			try
			{
				if (opImg.ShowDialog() == DialogResult.OK)
				{
					textBoxSelectedPath.Text = opImg.FileName;
					textBoxSelectedPath.Tag = "File";
					buttonProcess.Enabled = true;
					PicBox.Image = Image.FromFile(textBoxSelectedPath.Text);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadOCRSettings()
		{
			try
			{
				docIDSeparator = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_DocIDSeparator, "-");
				docNumberTitle = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_NumberTitle, "Document No:");
				unknownToLastDoc = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_AssignUnknown, defaultValue: false);
				string optionValue = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_FindBarcodes, defaultValue: false) ? "1" : "0";
				NsOCR.Cfg_SetOption(CfgObj, 0, "Zoning/FindBarcodes", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_ImageInversion, defaultValue: true) ? "2" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "ImgAlizer/Inversion", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_FixSkew, defaultValue: true) ? "360" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "ImgAlizer/SkewAngle", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_AutoRotate, defaultValue: true) ? "1" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "ImgAlizer/AutoRotate", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_AutoRotate, defaultValue: true) ? "1" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "ImgAlizer/NoiseFilter", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_RemoveLines, defaultValue: true) ? "1" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "PixLines/RemoveLines", optionValue);
				NsOCR.Cfg_SetOption(CfgObj, 0, "PixLines/FindHorLines", optionValue);
				NsOCR.Cfg_SetOption(CfgObj, 0, "PixLines/FindVerLines", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_GrayMode, defaultValue: false) ? "1" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "Main/GrayMode", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_FastMode, defaultValue: false) ? "1" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "Main/FastMode", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_BinTwice, defaultValue: false) ? "1" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "Binarizer/BinTwice", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_CorrectMixed, defaultValue: true) ? "1" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "WordAlizer/CorrectMixed", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_UseDictionary, defaultValue: false) ? "1" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "Dictionaries/UseDictionary", optionValue);
				optionValue = (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_CombineZonesHorz, defaultValue: false) ? "1" : "0");
				NsOCR.Cfg_SetOption(CfgObj, 0, "Zoning/OneColumn", optionValue);
				optionValue = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_BinaryThr, "255");
				NsOCR.Cfg_SetOption(CfgObj, 0, "Binarizer/SimpleThr", optionValue);
				optionValue = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_TextQuality, "-1");
				NsOCR.Cfg_SetOption(CfgObj, 0, "WordAlizer/TextQual", optionValue);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadImage(string imagePath)
		{
			try
			{
				currentFilePath = imagePath;
				currentFileName = Path.GetFileName(imagePath);
				NsOCR.Cfg_SetOption(CfgObj, 0, "ImgAlizer/AutoScale", "1");
				NsOCR.Cfg_SetOption(CfgObj, 0, "ImgAlizer/ScaleFactor", "1.0");
				int num2;
				switch (0)
				{
				case 0:
					num2 = NsOCR.Img_LoadFile(ImgObj, imagePath);
					break;
				case 1:
				{
					Array Bytes2 = File.ReadAllBytes(imagePath);
					num2 = NsOCR.Img_LoadFromMemory(ImgObj, ref Bytes2, Bytes2.Length);
					break;
				}
				default:
				{
					Bitmap bitmap = new Bitmap(imagePath);
					BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
					IntPtr scan = bitmapData.Scan0;
					int num = checked(Math.Abs(bitmapData.Stride) * bitmap.Height);
					byte[] array = new byte[num];
					Marshal.Copy(scan, array, 0, num);
					bitmap.UnlockBits(bitmapData);
					Array Bytes = array;
					num2 = NsOCR.Img_LoadBmpData(ImgObj, ref Bytes, bitmap.Width, bitmap.Height, 0, 0);
					break;
				}
				}
				if (num2 > 1879048192)
				{
					if (num2 == 1879048209)
					{
						if (MessageBox.Show("GhostSript library is needed to support PDF files. Just download and install it with default settings. Do you want to download GhostScript now?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							OpenURL("http://www.nicomsoft.com/files/ocr/ghostscript.htm");
						}
					}
					else
					{
						ShowError("Img_LoadFile", num2);
					}
				}
				else
				{
					DoImageLoaded();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void DoImageLoaded()
		{
			int num = NsOCR.Img_GetPageCount(ImgObj);
			if (num > 1879048192)
			{
				ShowError("Img_GetPageCount", num);
				return;
			}
			if (num > 1 && MessageBox.Show("Image contains multiple pages (" + num.ToString() + "). Do you want to process and save all pages automatically?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				ProcessEntireDocument();
				buttonProcess.Enabled = false;
				return;
			}
			num = NsOCR.Img_OCR(ImgObj, 0, 111, 0);
			if (num > 1879048192)
			{
				ShowError("Img_OCR", num);
				return;
			}
			ShowImageParams();
			num = NsOCR.Img_GetSkewAngle(ImgObj);
			if (num > 1879048192)
			{
				ShowError("Img_GetSkewAngle", num);
				lbSkew.Text = "";
			}
			else
			{
				lbSkew.Text = "Skew angle (degrees): " + (double)num / 1000.0;
			}
			num = NsOCR.Img_GetPixLineCnt(ImgObj);
			if (num > 1879048192)
			{
				ShowError("Img_GetPixLineCnt", num);
				return;
			}
			lbLines.Text = "Lines: " + num.ToString();
			num = NsOCR.Img_GetProperty(ImgObj, 6);
			lbInverted.Text = ((num == 1) ? "Inverted: Yes" : "Inverted: No");
			NsOCR.Img_GetSize(ImgObj, out int _, out int _);
			Frame.X = 0;
			Frame.Y = 0;
			Frame.Width = 0;
			Frame.Height = 0;
			AdjustDocScale();
			buttonProcess.Enabled = true;
			bkSave.Enabled = false;
		}

		public Image byteArrayToImage(byte[] byteArrayIn)
		{
			return Image.FromStream(new MemoryStream(byteArrayIn));
		}

		public static byte[] ConvertToBytes(Array myArray)
		{
			List<byte> list = new List<byte>();
			foreach (object item in myArray)
			{
				byte[] array = null;
				if (item is string)
				{
					char[] array2 = ((string)item).ToCharArray();
					array = new byte[array2.Length];
					int num = 0;
					for (num = 0; num < array2.Length; num = checked(num + 1))
					{
						array[num] = Convert.ToByte(array2[num]);
					}
				}
				else if (item is int)
				{
					array = BitConverter.GetBytes((int)item);
				}
				else if (item is double)
				{
					array = BitConverter.GetBytes((double)item);
				}
				else if (item is short)
				{
					array = BitConverter.GetBytes((short)item);
				}
				else if (item is int)
				{
					array = BitConverter.GetBytes((int)item);
				}
				else if (item is long)
				{
					array = BitConverter.GetBytes((long)item);
				}
				if (array != null)
				{
					list.AddRange(array);
				}
				array = null;
			}
			return list.ToArray();
		}

		public static byte[] ToByteArray(string value)
		{
			char[] array = value.ToCharArray();
			byte[] array2 = new byte[array.Length];
			for (int i = 0; i < array.Length; i = checked(i + 1))
			{
				byte b = array2[i] = Convert.ToByte(array[i]);
			}
			return array2;
		}

		public static Bitmap ByteToImage(byte[] blob)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(blob, 0, Convert.ToInt32(blob.Length));
			Bitmap result = new Bitmap(memoryStream, useIcm: false);
			memoryStream.Dispose();
			return result;
		}

		private static Bitmap ConvertToBitmap(byte[] imagesSource)
		{
			return new Bitmap((Image)new ImageConverter().ConvertFrom(imagesSource));
		}

		private void ShowImageParams()
		{
			int num = NsOCR.Img_GetProperty(ImgObj, 1);
			int num2 = NsOCR.Img_GetProperty(ImgObj, 3);
			int num3 = NsOCR.Img_GetProperty(ImgObj, 4);
			int num4 = NsOCR.Img_GetProperty(ImgObj, 5);
			NsOCR.Img_GetSize(ImgObj, out int Width, out int Height);
			lbSize.Text = "";
			if (num != 0)
			{
				lbSize.Text = "DPI: " + num.ToString() + ", ";
			}
			lbSize.Text = lbSize.Text + "BPP: " + num2.ToString() + ", ";
			lbSize.Text = lbSize.Text + "Size: " + num3.ToString() + "x" + num4.ToString() + " => " + Width.ToString() + "x" + Height.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			checked
			{
				if (IsImgLoaded())
				{
					int num = NsOCR.Img_GetPageCount(ImgObj);
					if (!int.TryParse("1", out int result))
					{
						result = 1;
					}
					result--;
					if (result < 0)
					{
						result = 0;
					}
					if (result >= num)
					{
						result = num - 1;
					}
					NsOCR.Img_SetPage(ImgObj, result);
					bkSave.Enabled = false;
					NsOCR.Img_GetSize(ImgObj, out int _, out int _);
					NsOCR.Img_OCR(ImgObj, 0, 111, 0);
					int Width2;
					int Height2;
					int num2 = NsOCR.Img_GetSize(ImgObj, out Width2, out Height2);
					if (num2 > 1879048192)
					{
						ShowError("Img_OCR", num2);
					}
					ShowImageParams();
					AdjustDocScale();
				}
			}
		}

		private void PicBoxOnMouseDown(object sender, MouseEventArgs e)
		{
			if (!IsImgLoaded())
			{
				return;
			}
			checked
			{
				if (e.Button == MouseButtons.Right)
				{
					Point point = new Point(e.X, e.Y);
					float curDocScale = GetCurDocScale();
					int num = NsOCR.Img_GetBlockCnt(ImgObj);
					int num2 = -1;
					int num3 = -1;
					Rectangle rectangle = default(Rectangle);
					for (int i = 0; i < num; i++)
					{
						NsOCR.Img_GetBlock(ImgObj, i, out int BlkObj);
						NsOCR.Blk_GetRect(BlkObj, out int Xpos, out int Ypos, out int Width, out int Height);
						rectangle.X = (int)(curDocScale * (float)Xpos);
						rectangle.Y = (int)(curDocScale * (float)Ypos);
						rectangle.Width = (int)(curDocScale * (float)Width);
						rectangle.Height = (int)(curDocScale * (float)Height);
						if (rectangle.Contains(point))
						{
							int num4 = (Width >= Height) ? Height : Width;
							if (num3 == -1 || num4 < num3)
							{
								num3 = num4;
								num2 = i;
							}
						}
					}
					if (num2 != -1)
					{
						pmBlockTag = num2;
						point = PicBox.PointToScreen(point);
					}
				}
				else
				{
					NsOCR.Img_GetSize(ImgObj, out int Width2, out int Height2);
					Dwn = true;
					float curDocScale2 = GetCurDocScale();
					Frame.X = (int)(1f / curDocScale2 * (float)e.X);
					if (Frame.X < 0)
					{
						Frame.X = 0;
					}
					if (Frame.X > Width2)
					{
						Frame.X = Width2;
					}
					Frame.Y = (int)(1f / curDocScale2 * (float)e.Y);
					if (Frame.Y < 0)
					{
						Frame.Y = 0;
					}
					if (Frame.Y > Height2)
					{
						Frame.Y = Height2;
					}
					Frame.Width = 0;
					Frame.Height = 0;
					ShowImage();
				}
			}
		}

		private void PicBoxOnMouseUp(object sender, MouseEventArgs e)
		{
			if (!Dwn)
			{
				return;
			}
			Dwn = false;
			if (!IsImgLoaded())
			{
				return;
			}
			NsOCR.Img_GetSize(ImgObj, out int Width, out int Height);
			checked
			{
				if (Frame.Right >= Width)
				{
					Frame.Width = Width - Frame.Left - 1;
				}
				if (Frame.Bottom >= Height)
				{
					Frame.Height = Height - Frame.Top - 1;
				}
				Width = Frame.Right - Frame.Left + 1;
				Height = Frame.Bottom - Frame.Top + 1;
				if (Width < 8 || Height < 8)
				{
					ShowImage();
					return;
				}
				int BlkObj;
				int num = NsOCR.Img_AddBlock(ImgObj, Frame.Left, Frame.Top, Width, Height, out BlkObj);
				if (num > 1879048192)
				{
					ShowError("Img_AddBlock", num);
					return;
				}
				NsOCR.Blk_Inversion(BlkObj, 256);
				NsOCR.Blk_Rotation(BlkObj, 256);
				ShowImage();
			}
		}

		private void PicBoxMouseMove(object sender, MouseEventArgs e)
		{
			GC.Collect();
			checked
			{
				if (Dwn && IsImgLoaded())
				{
					NsOCR.Img_GetSize(ImgObj, out int Width, out int Height);
					float curDocScale = GetCurDocScale();
					Frame.Width = (int)(1f / curDocScale * (float)e.X) - Frame.Left + 1;
					if (Frame.Width < 0)
					{
						Frame.Width = 0;
					}
					if (Frame.Width > Width)
					{
						Frame.Width = Width;
					}
					Frame.Height = (int)(1f / curDocScale * (float)e.Y) - Frame.Top + 1;
					if (Frame.Height < 0)
					{
						Frame.Height = 0;
					}
					if (Frame.Height > Height)
					{
						Frame.Height = Height;
					}
					ShowImage();
				}
			}
		}

		private void bkRecognize_Click(object sender, EventArgs e)
		{
			if (!IsImgLoaded())
			{
				MessageBox.Show("Image not loaded!");
			}
		}

		private void ProcessOCR()
		{
			try
			{
				buttonProcess.Enabled = false;
				bkSave.Enabled = false;
				lbWait.Visible = true;
				progressBarLoad.Visible = true;
				progressBarLoad.Style = ProgressBarStyle.Marquee;
				Refresh();
				int num;
				if (false)
				{
					num = NsOCR.Img_OCR(ImgObj, 112, 255, 0);
					goto IL_00c4;
				}
				num = NsOCR.Img_OCR(ImgObj, 112, 255, 1);
				if (num <= 1879048192)
				{
					fmWait.mode = 0;
					fmWait.fmMain = this;
					fmWait.ShowDialog();
					num = fmWait.res;
					goto IL_00c4;
				}
				ShowError("Ocr_OcrImg(1)", num);
				goto end_IL_0000;
				IL_00c4:
				lbWait.Visible = false;
				buttonProcess.Enabled = true;
				bkSave.Enabled = true;
				if (num <= 1879048192)
				{
					goto IL_0113;
				}
				if (num == 1879048220)
				{
					MessageBox.Show("Operation was cancelled.");
					goto IL_0113;
				}
				ShowError("Img_OCR", num);
				goto end_IL_0000;
				IL_0113:
				cbBin_CheckedChanged(this, null);
				ShowText();
				end_IL_0000:;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void ShowText()
		{
			checked
			{
				try
				{
					int flags = 1;
					NsOCR.Img_GetImgText(ImgObj, out string TextStr, flags);
					int num = TextStr.IndexOf(docNumberTitle);
					if (num < 0)
					{
						num = TextStr.IndexOf(docNumberTitle.ToUpper());
					}
					if (num >= 0)
					{
						int num2 = TextStr.Length - num;
						if (num2 > 30)
						{
							num2 = 30;
						}
						string text = TextStr.Substring(num, num2);
						text = text.Substring(docNumberTitle.Length, text.Length - docNumberTitle.Length).Trim();
						int num3 = text.IndexOf("\r");
						if (num3 > 0)
						{
							text = text.Substring(0, num3);
						}
						num3 = text.IndexOf(docIDSeparator);
						string text2 = "";
						if (num3 > 0)
						{
							text2 = text.Substring(0, num3).Trim();
							text = text.Substring(num3 + 1, text.Length - num3 - 1).Trim();
							num3 = text.IndexOf(' ');
							if (num3 > 0)
							{
								text = text.Substring(0, num3).Trim();
							}
							TreeNode treeNode = null;
							if (!treeView1.Nodes.ContainsKey(text2))
							{
								string text3 = "";
								int num4 = 0;
								DataRow[] array = sysDocList.Tables[0].Select("SysDocID = '" + text2 + "'");
								if (array.Length != 0)
								{
									text3 = array[0]["Name"].ToString();
									num4 = int.Parse(array[0]["SysDocType"].ToString());
								}
								else
								{
									AddUnknownDocument(currentFilePath, currentFileName);
								}
								treeNode = treeView1.Nodes.Add(text2, text3, 1, 1);
								treeNode.Tag = num4;
							}
							else
							{
								treeNode = treeView1.Nodes[text2];
							}
							TreeNode treeNode2 = null;
							if (treeNode.Nodes.ContainsKey(text))
							{
								treeNode2 = treeNode.Nodes[text];
							}
							else
							{
								treeNode2 = treeNode.Nodes.Add(text, text, 2, 2);
								treeNode2.Tag = currentFilePath;
							}
							lastDocumentNode = treeNode2;
							treeNode2 = treeNode2.Nodes.Add(currentFilePath, currentFileName, 3, 3);
							treeNode2.Tag = currentFilePath;
						}
						else
						{
							AddUnknownDocument(currentFilePath, currentFileName);
						}
					}
					else
					{
						AddUnknownDocument(currentFilePath, currentFileName);
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		private void AddUnknownDocument(string path, string fileName)
		{
			if (unknownToLastDoc && lastDocumentNode != null)
			{
				lastDocumentNode.Nodes.Add(path, fileName, 3, 3).Tag = path;
				return;
			}
			TreeNode treeNode = null;
			treeNode = (treeView1.Nodes.ContainsKey("None") ? treeView1.Nodes["None"] : treeView1.Nodes.Insert(0, "None", "None", 0));
			treeNode.Nodes.Add(path, fileName, 3, 3).Tag = path;
		}

		private void cbScale_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!NoEvent)
			{
				bkFile_Click(null, null);
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			ShowText();
		}

		private void nnTypeOCRText_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Blk_SetType(BlkObj, 1);
			ShowImage();
		}

		private void nnTypeICRDigit_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Blk_SetType(BlkObj, 2);
			ShowImage();
		}

		private void nnTypePicture_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Blk_SetType(BlkObj, 4);
			ShowImage();
		}

		private void nnTypeClear_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Blk_SetType(BlkObj, 3);
			ShowImage();
		}

		private void nnDeleteBlock_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Img_DeleteBlock(ImgObj, BlkObj);
			ShowImage();
		}

		private void OnPanelResize(object sender, EventArgs e)
		{
			AdjustDocScale();
		}

		private void cbBin_CheckedChanged(object sender, EventArgs e)
		{
			AdjustDocScale();
		}

		private void bkLoadBlocks_Click(object sender, EventArgs e)
		{
			if (opBlocks.ShowDialog() == DialogResult.OK)
			{
				NsOCR.Img_DeleteAllBlocks(ImgObj);
				bkSave.Enabled = false;
				int num = NsOCR.Img_LoadBlocks(ImgObj, opBlocks.FileName);
				if (num > 1879048192)
				{
					ShowError("Img_LoadBlocks", num);
				}
				else
				{
					ShowImage();
				}
			}
		}

		private void bkSaveBlocks_Click(object sender, EventArgs e)
		{
			if (svBlocks.ShowDialog() == DialogResult.OK)
			{
				int num = NsOCR.Img_SaveBlocks(ImgObj, svBlocks.FileName);
				if (num > 1879048192)
				{
					ShowError("Img_SaveBlocks", num);
				}
			}
		}

		private void bkClearBlocks_Click(object sender, EventArgs e)
		{
			NsOCR.Img_DeleteAllBlocks(ImgObj);
			bkSave.Enabled = false;
			ShowImage();
		}

		private void bkDetectBlocks_Click(object sender, EventArgs e)
		{
			NsOCR.Img_DeleteAllBlocks(ImgObj);
			NsOCR.Img_OCR(ImgObj, 112, 112, 0);
			ShowImage();
		}

		private string GetDocName(string str)
		{
			return Path.ChangeExtension(Path.GetFileName(str), ".pdf");
		}

		private void bkScan_Click(object sender, EventArgs e)
		{
			fmScan.fmMain = this;
			fmScan.ShowDialog();
		}

		private void nnTypeOCRDigit_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Blk_SetType(BlkObj, 6);
			ShowImage();
		}

		private void nnTypeZoning_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Blk_SetType(BlkObj, 5);
			ShowImage();
		}

		private void nnTypeBarCode_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Blk_SetType(BlkObj, 7);
			NsOCR.Cfg_SetOption(CfgObj, 7, "BarCode/SearchMode", "3");
			ShowImage();
		}

		private void nnTypeTable_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Blk_SetType(BlkObj, 8);
			ShowImage();
		}

		private void nnTypeMRZ_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			NsOCR.Blk_SetType(BlkObj, 9);
			ShowImage();
		}

		private void ProcessEntireDocument()
		{
			try
			{
				bkSave_Click(null, null);
				int num2;
				if (SvrObj != 0)
				{
					int ocrObjCnt = 0;
					int num = 1;
					num <<= 8;
					if (false)
					{
						num2 = NsOCR.Ocr_ProcessPages(ImgObj, SvrObj, 0, -1, ocrObjCnt, 0 | num);
						goto IL_00a7;
					}
					num2 = NsOCR.Ocr_ProcessPages(ImgObj, SvrObj, 0, -1, ocrObjCnt, 1 | num);
					if (num2 <= 1879048192)
					{
						fmWait.mode = 1;
						fmWait.fmMain = this;
						fmWait.ShowDialog();
						num2 = fmWait.res;
						goto IL_00a7;
					}
					ShowError("Ocr_ProcessPages(1)", num2);
				}
				goto end_IL_0000;
				IL_00a7:
				NsOCR.Cfg_SetOption(CfgObj, 0, "Main/NumKernels", "0");
				if (num2 > 1879048192)
				{
					if (num2 == 1879048220)
					{
						MessageBox.Show("Operation was cancelled.");
					}
					else
					{
						ShowError("Ocr_ProcessPages", num2);
					}
					NsOCR.Svr_Destroy(SvrObj);
				}
				else
				{
					num2 = NsOCR.Svr_SaveToFile(SvrObj, svFile.FileName);
					NsOCR.Svr_Destroy(SvrObj);
					if (num2 > 1879048192)
					{
						ShowError("Svr_SaveToFile", num2);
					}
					else
					{
						Process.Start(svFile.FileName);
					}
				}
				end_IL_0000:;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void bkSelectLanguages_Click(object sender, EventArgs e)
		{
		}

		private void cbPixLines_CheckedChanged(object sender, EventArgs e)
		{
			AdjustDocScale();
		}

		private void bkOptions_Click(object sender, EventArgs e)
		{
			fmOptions.fmMain = this;
			fmOptions.ShowDialog();
		}

		public void ShowHelp(string section)
		{
			string url = "file://" + AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Docs\\NSOCR.chm";
			Help.ShowHelp(this, url, section);
		}

		private void bkHelp_Click(object sender, EventArgs e)
		{
			ShowHelp("");
		}

		public static DialogResult InputBox(string title, string promptText, ref string value)
		{
			try
			{
				Form form = new Form();
				Label label = new Label();
				TextBox textBox = new TextBox();
				Button button = new Button();
				Button button2 = new Button();
				form.Text = title;
				label.Text = promptText;
				textBox.Text = value;
				button.Text = "OK";
				button2.Text = "Cancel";
				button.DialogResult = DialogResult.OK;
				button2.DialogResult = DialogResult.Cancel;
				label.SetBounds(9, 20, 372, 13);
				textBox.SetBounds(12, 36, 372, 20);
				button.SetBounds(228, 72, 75, 23);
				button2.SetBounds(309, 72, 75, 23);
				label.AutoSize = true;
				textBox.Anchor |= AnchorStyles.Right;
				button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				form.ClientSize = new Size(396, 107);
				form.Controls.AddRange(new Control[4]
				{
					label,
					textBox,
					button,
					button2
				});
				form.ClientSize = new Size(Math.Max(300, checked(label.Right + 10)), form.ClientSize.Height);
				form.FormBorderStyle = FormBorderStyle.FixedDialog;
				form.StartPosition = FormStartPosition.CenterScreen;
				form.MinimizeBox = false;
				form.MaximizeBox = false;
				form.AcceptButton = button;
				form.CancelButton = button2;
				DialogResult result = form.ShowDialog();
				value = textBox.Text;
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				throw;
			}
		}

		private void nnSetRegExp_Click(object sender, EventArgs e)
		{
			NsOCR.Img_GetBlock(ImgObj, pmBlockTag, out int BlkObj);
			string value = "";
			if (InputBox("Set regular expression (for entire zone)", "New regular expression:", ref value) == DialogResult.OK && NsOCR.Blk_SetWordRegEx(BlkObj, -1, -1, value, 0) > 0)
			{
				MessageBox.Show("Regular Expression syntax error!");
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				treeView1.Nodes.Clear();
				textBoxSelectedPath.Text = folderBrowserDialog1.SelectedPath;
				textBoxSelectedPath.Tag = "Folder";
				buttonProcess.Enabled = true;
			}
		}

		private void buttonProcess_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					if (textBoxSelectedPath.Text != "")
					{
						if (textBoxSelectedPath.Tag.ToString() == "File")
						{
							LoadImage(opImg.FileName);
							ProcessOCR();
						}
						else
						{
							treeView1.Nodes.Clear();
							string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
							int num = files.Length;
							int num2 = 0;
							int num3 = num;
							labelfc.Text = num.ToString();
							string[] array = files;
							foreach (string imagePath in array)
							{
								num2++;
								num3--;
								labelrc.Text = num2.ToString();
								labelurc.Text = num3.ToString();
								LoadImage(imagePath);
								ProcessOCR();
							}
						}
						progressBarLoad.Visible = false;
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to clear the form and remove all scanned images?") == DialogResult.Yes)
			{
				treeView1.Nodes.Clear();
				textBoxSelectedPath.Clear();
				currentFilePath = "";
				currentFileName = "";
				lastDocumentNode = null;
				buttonProcess.Enabled = false;
				PicBox.Image = null;
				labelfc.Text = "0";
				labelrc.Text = "0";
				labelurc.Text = "0";
			}
		}

		private void bkSave_Click(object sender, EventArgs e)
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

		private bool GetData()
		{
			try
			{
				currentData = new EntityDocData();
				foreach (TreeNode node in treeView1.Nodes)
				{
					if (node.ImageIndex != 0 && node.ImageIndex == 1)
					{
						foreach (TreeNode node2 in node.Nodes)
						{
							foreach (TreeNode node3 in node2.Nodes)
							{
								DataRow dataRow = currentData.EntityDocTable.NewRow();
								dataRow.BeginEdit();
								dataRow["EntityID"] = node2.Text;
								dataRow["EntityType"] = EntityTypesEnum.Transactions;
								dataRow["EntitySysDocID"] = node.Name;
								dataRow["EntityDocName"] = node3.Text;
								dataRow["EntityDocDesc"] = "";
								dataRow["EntityDocKeyword"] = "";
								byte[] array = (byte[])(dataRow["FileData"] = GetInputFile(node3.Tag.ToString()));
								dataRow.EndEdit();
								currentData.EntityDocTable.Rows.Add(dataRow);
							}
						}
					}
				}
				return true;
			}
			catch (FileNotFoundException ex)
			{
				ErrorHelper.WarningMessage("Could not find the file:" + ex.FileName);
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
			try
			{
				byte[] result = null;
				if (filePath == "")
				{
					throw new Exception("File not found.");
				}
				string extension = Path.GetExtension(Path.GetFileName(filePath));
				string a = string.Empty;
				switch (extension.ToLower())
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
					result = new BinaryReader(fileStream).ReadBytes(checked((int)fileStream.Length));
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		private bool ValidateData()
		{
			try
			{
				DataSet dataSet = new DataSet();
				DataTable dataTable = dataSet.Tables.Add("Docs");
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("VoucherID");
				dataTable.Columns.Add("SysDocType", typeof(int));
				foreach (TreeNode node in treeView1.Nodes)
				{
					if (node.ImageIndex != 0 && node.ImageIndex == 1)
					{
						foreach (TreeNode node2 in node.Nodes)
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow.BeginEdit();
							dataRow["SysDocID"] = node.Name;
							dataRow["VoucherID"] = node2.Text;
							dataRow["SysDocType"] = node.Tag.ToString();
							dataRow.EndEdit();
							dataTable.Rows.Add(dataRow);
						}
					}
				}
				DataSet notExistingDocs = Factory.EntityDocSystem.GetNotExistingDocs(dataSet);
				if (notExistingDocs.Tables[0].Rows.Count > 0)
				{
					ErrorHelper.WarningMessage("One or more documents not found.");
					foreach (DataRow row in notExistingDocs.Tables[0].Rows)
					{
						string b = row["SysDocID"].ToString();
						string b2 = row["VoucherID"].ToString();
						foreach (TreeNode node3 in treeView1.Nodes)
						{
							if (node3.Name == b)
							{
								node3.Expand();
								foreach (TreeNode node4 in node3.Nodes)
								{
									if (node4.Text == b2)
									{
										node4.ForeColor = Color.Red;
										break;
									}
								}
							}
						}
					}
					return false;
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
				if (currentData.EntityDocTable.Rows.Count == 0)
				{
					ErrorHelper.WarningMessage("There is no image found to save.");
					return false;
				}
				bool num = Factory.EntityDocSystem.SaveOCRDocs(currentData);
				if (num && toolStripButton1.Checked)
				{
					DeleteUploadedFiles();
				}
				return num;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void DeleteUploadedFiles()
		{
			try
			{
				foreach (TreeNode node in treeView1.Nodes)
				{
					if (node.ImageIndex == 1)
					{
						foreach (TreeNode node2 in node.Nodes)
						{
							foreach (TreeNode node3 in node2.Nodes)
							{
								File.Delete(node3.Tag.ToString());
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void treeView1_MouseDown(object sender, MouseEventArgs e)
		{
			mySelectedNode = treeView1.GetNodeAt(e.X, e.Y);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.OCR.DocumentOCRForm));
			statusStrip1 = new System.Windows.Forms.StatusStrip();
			lbSize = new System.Windows.Forms.ToolStripStatusLabel();
			lbSkew = new System.Windows.Forms.ToolStripStatusLabel();
			lbLines = new System.Windows.Forms.ToolStripStatusLabel();
			lbInverted = new System.Windows.Forms.ToolStripStatusLabel();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			treeView1 = new System.Windows.Forms.TreeView();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemMain = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemChild = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemRemove = new System.Windows.Forms.ToolStripMenuItem();
			PicBox = new Micromind.UISupport.GUIControls.Others.MMSPictureBox(components);
			cbCharRects = new System.Windows.Forms.CheckBox();
			cbPixLines = new System.Windows.Forms.CheckBox();
			cbBin = new System.Windows.Forms.CheckBox();
			lbWait = new System.Windows.Forms.Label();
			opImg = new System.Windows.Forms.OpenFileDialog();
			opBlocks = new System.Windows.Forms.OpenFileDialog();
			svBlocks = new System.Windows.Forms.SaveFileDialog();
			svFile = new System.Windows.Forms.SaveFileDialog();
			folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonScan = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonFile = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFolder = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			textBoxSelectedPath = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			imageList1 = new System.Windows.Forms.ImageList(components);
			zoomTrackBarControl1 = new DevExpress.XtraEditors.ZoomTrackBarControl();
			progressBarLoad = new System.Windows.Forms.ProgressBar();
			labelFCount = new System.Windows.Forms.Label();
			labelFRead = new System.Windows.Forms.Label();
			labelUnRead = new System.Windows.Forms.Label();
			labelfc = new System.Windows.Forms.Label();
			labelrc = new System.Windows.Forms.Label();
			labelurc = new System.Windows.Forms.Label();
			buttonClear = new Micromind.UISupport.XPButton();
			buttonProcess = new Micromind.UISupport.XPButton();
			bkSave = new Micromind.UISupport.XPButton();
			statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)PicBox.Properties).BeginInit();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)zoomTrackBarControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)zoomTrackBarControl1.Properties).BeginInit();
			SuspendLayout();
			statusStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
			statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				lbSize,
				lbSkew,
				lbLines,
				lbInverted
			});
			statusStrip1.Location = new System.Drawing.Point(0, 633);
			statusStrip1.Name = "statusStrip1";
			statusStrip1.Size = new System.Drawing.Size(1091, 22);
			statusStrip1.TabIndex = 1;
			statusStrip1.Text = "statusStrip1";
			lbSize.Name = "lbSize";
			lbSize.Size = new System.Drawing.Size(0, 17);
			lbSkew.Name = "lbSkew";
			lbSkew.Size = new System.Drawing.Size(0, 17);
			lbLines.Name = "lbLines";
			lbLines.Size = new System.Drawing.Size(0, 17);
			lbInverted.Name = "lbInverted";
			lbInverted.Size = new System.Drawing.Size(118, 17);
			lbInverted.Text = "toolStripStatusLabel1";
			lbInverted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lbInverted.Visible = false;
			splitContainer1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			splitContainer1.Location = new System.Drawing.Point(0, 88);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(treeView1);
			splitContainer1.Panel1.Resize += new System.EventHandler(OnPanelResize);
			splitContainer1.Panel2.Controls.Add(PicBox);
			splitContainer1.Size = new System.Drawing.Size(1091, 508);
			splitContainer1.SplitterDistance = 325;
			splitContainer1.TabIndex = 3;
			treeView1.AllowDrop = true;
			treeView1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			treeView1.ContextMenuStrip = contextMenuStrip1;
			treeView1.HideSelection = false;
			treeView1.Location = new System.Drawing.Point(10, 11);
			treeView1.Name = "treeView1";
			treeView1.Size = new System.Drawing.Size(300, 494);
			treeView1.TabIndex = 0;
			treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(treeView1_MouseDown);
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				toolStripMenuItemEdit,
				toolStripMenuItemMain,
				toolStripMenuItemChild,
				toolStripMenuItemRemove
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(128, 92);
			toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
			toolStripMenuItemEdit.Size = new System.Drawing.Size(127, 22);
			toolStripMenuItemEdit.Text = "Rename";
			toolStripMenuItemMain.Name = "toolStripMenuItemMain";
			toolStripMenuItemMain.Size = new System.Drawing.Size(127, 22);
			toolStripMenuItemMain.Text = "Add Main";
			toolStripMenuItemChild.Name = "toolStripMenuItemChild";
			toolStripMenuItemChild.Size = new System.Drawing.Size(127, 22);
			toolStripMenuItemChild.Text = "Add Child";
			toolStripMenuItemRemove.Name = "toolStripMenuItemRemove";
			toolStripMenuItemRemove.Size = new System.Drawing.Size(127, 22);
			toolStripMenuItemRemove.Text = "Remove";
			PicBox.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			PicBox.Cursor = System.Windows.Forms.Cursors.Default;
			PicBox.Location = new System.Drawing.Point(7, 12);
			PicBox.Name = "PicBox";
			PicBox.Properties.ShowMenu = false;
			PicBox.Properties.ShowScrollBars = true;
			PicBox.Properties.ZoomAccelerationFactor = 1.0;
			PicBox.Size = new System.Drawing.Size(747, 491);
			PicBox.TabIndex = 0;
			cbCharRects.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cbCharRects.AutoSize = true;
			cbCharRects.Location = new System.Drawing.Point(608, 599);
			cbCharRects.Name = "cbCharRects";
			cbCharRects.Size = new System.Drawing.Size(103, 17);
			cbCharRects.TabIndex = 8;
			cbCharRects.Text = "Show char rects";
			cbCharRects.UseVisualStyleBackColor = true;
			cbCharRects.Visible = false;
			cbCharRects.CheckedChanged += new System.EventHandler(cbPixLines_CheckedChanged);
			cbPixLines.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cbPixLines.AutoSize = true;
			cbPixLines.Location = new System.Drawing.Point(472, 599);
			cbPixLines.Name = "cbPixLines";
			cbPixLines.Size = new System.Drawing.Size(108, 17);
			cbPixLines.TabIndex = 7;
			cbPixLines.Text = "Show image lines";
			cbPixLines.UseVisualStyleBackColor = true;
			cbPixLines.Visible = false;
			cbPixLines.CheckedChanged += new System.EventHandler(cbPixLines_CheckedChanged);
			cbBin.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cbBin.AutoSize = true;
			cbBin.Location = new System.Drawing.Point(330, 599);
			cbBin.Name = "cbBin";
			cbBin.Size = new System.Drawing.Size(136, 17);
			cbBin.TabIndex = 6;
			cbBin.Text = "Display binarized image";
			cbBin.UseVisualStyleBackColor = true;
			cbBin.Visible = false;
			cbBin.CheckedChanged += new System.EventHandler(cbBin_CheckedChanged);
			lbWait.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			lbWait.AutoSize = true;
			lbWait.Location = new System.Drawing.Point(327, 619);
			lbWait.Name = "lbWait";
			lbWait.Size = new System.Drawing.Size(70, 13);
			lbWait.TabIndex = 11;
			lbWait.Text = "Please wait...";
			lbWait.Visible = false;
			opImg.Filter = "Image Files (bmp, jpg, tif, png, gif, pdf)|*.bmp;*.jpg;*.tif;*.png;*.gif;*.pdf|All Files|*";
			opImg.Title = "Open Image File";
			opBlocks.DefaultExt = "blk";
			opBlocks.Filter = "blk files|*.blk";
			svBlocks.DefaultExt = "blk";
			svBlocks.Filter = "blk files|*.blk";
			svFile.DefaultExt = "pdf";
			svFile.Filter = "PDF document(*.pdf)|*.pdf|RTF document (*.rtf)|*.rtf|ASCII Text document (*.txt)|*.txt|Unicode Text document (*.txt)|*.txt|XML document (*.xml)|*.xml|PDF/A document(*.pdf)|*.pdf";
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				toolStripButtonScan,
				toolStripSeparator1,
				toolStripButtonFile,
				toolStripButtonFolder,
				toolStripSeparator2,
				toolStripButton1
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(1091, 39);
			toolStrip1.TabIndex = 28;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonScan.Image = Micromind.ClientUI.Properties.Resources.scanner3;
			toolStripButtonScan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			toolStripButtonScan.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonScan.Name = "toolStripButtonScan";
			toolStripButtonScan.Size = new System.Drawing.Size(36, 36);
			toolStripButtonScan.Text = "toolStripButton1";
			toolStripButtonScan.ToolTipText = "Scan...";
			toolStripButtonScan.Click += new System.EventHandler(bkScan_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
			toolStripButtonFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFile.Image = Micromind.ClientUI.Properties.Resources.file;
			toolStripButtonFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			toolStripButtonFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFile.Name = "toolStripButtonFile";
			toolStripButtonFile.Size = new System.Drawing.Size(36, 36);
			toolStripButtonFile.Text = "Select a file";
			toolStripButtonFile.Click += new System.EventHandler(bkFile_Click);
			toolStripButtonFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFolder.Image = Micromind.ClientUI.Properties.Resources.folder;
			toolStripButtonFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			toolStripButtonFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFolder.Name = "toolStripButtonFolder";
			toolStripButtonFolder.Size = new System.Drawing.Size(36, 36);
			toolStripButtonFolder.Text = "Select a folder";
			toolStripButtonFolder.Click += new System.EventHandler(button1_Click_1);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
			toolStripButton1.CheckOnClick = true;
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.bin;
			toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(36, 36);
			toolStripButton1.Text = "Delete files after saving";
			textBoxSelectedPath.Location = new System.Drawing.Point(128, 55);
			textBoxSelectedPath.Name = "textBoxSelectedPath";
			textBoxSelectedPath.ReadOnly = true;
			textBoxSelectedPath.Size = new System.Drawing.Size(573, 20);
			textBoxSelectedPath.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(7, 57);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(115, 13);
			label1.TabIndex = 0;
			label1.Text = "Selected Document(s):";
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "unknown.png");
			imageList1.Images.SetKeyName(1, "book.png");
			imageList1.Images.SetKeyName(2, "file.png");
			imageList1.Images.SetKeyName(3, "image.png");
			imageList1.Images.SetKeyName(4, "check.png");
			zoomTrackBarControl1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			zoomTrackBarControl1.EditValue = 100;
			zoomTrackBarControl1.Location = new System.Drawing.Point(908, 602);
			zoomTrackBarControl1.Name = "zoomTrackBarControl1";
			zoomTrackBarControl1.Properties.LargeChange = 10;
			zoomTrackBarControl1.Properties.Maximum = 200;
			zoomTrackBarControl1.Properties.Middle = 100;
			zoomTrackBarControl1.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
			zoomTrackBarControl1.Properties.SmallChange = 5;
			zoomTrackBarControl1.Size = new System.Drawing.Size(175, 23);
			zoomTrackBarControl1.TabIndex = 29;
			zoomTrackBarControl1.Value = 100;
			progressBarLoad.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			progressBarLoad.Location = new System.Drawing.Point(401, 619);
			progressBarLoad.Name = "progressBarLoad";
			progressBarLoad.Size = new System.Drawing.Size(100, 13);
			progressBarLoad.TabIndex = 30;
			progressBarLoad.Visible = false;
			labelFCount.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelFCount.AutoSize = true;
			labelFCount.Location = new System.Drawing.Point(507, 618);
			labelFCount.Name = "labelFCount";
			labelFCount.Size = new System.Drawing.Size(60, 13);
			labelFCount.TabIndex = 31;
			labelFCount.Text = "File Count :";
			labelFRead.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelFRead.AutoSize = true;
			labelFRead.Location = new System.Drawing.Point(596, 618);
			labelFRead.Name = "labelFRead";
			labelFRead.Size = new System.Drawing.Size(36, 13);
			labelFRead.TabIndex = 32;
			labelFRead.Text = "Read:";
			labelUnRead.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelUnRead.AutoSize = true;
			labelUnRead.Location = new System.Drawing.Point(661, 618);
			labelUnRead.Name = "labelUnRead";
			labelUnRead.Size = new System.Drawing.Size(50, 13);
			labelUnRead.TabIndex = 33;
			labelUnRead.Text = "UnRead:";
			labelfc.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelfc.AutoSize = true;
			labelfc.Location = new System.Drawing.Point(566, 618);
			labelfc.Name = "labelfc";
			labelfc.Size = new System.Drawing.Size(13, 13);
			labelfc.TabIndex = 34;
			labelfc.Text = "0";
			labelrc.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelrc.AutoSize = true;
			labelrc.Location = new System.Drawing.Point(632, 618);
			labelrc.Name = "labelrc";
			labelrc.Size = new System.Drawing.Size(13, 13);
			labelrc.TabIndex = 35;
			labelrc.Text = "0";
			labelurc.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelurc.AutoSize = true;
			labelurc.Location = new System.Drawing.Point(712, 618);
			labelurc.Name = "labelurc";
			labelurc.Size = new System.Drawing.Size(13, 13);
			labelurc.TabIndex = 36;
			labelurc.Text = "0";
			buttonClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClear.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonClear.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClear.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClear.Location = new System.Drawing.Point(128, 602);
			buttonClear.Name = "buttonClear";
			buttonClear.Size = new System.Drawing.Size(110, 28);
			buttonClear.TabIndex = 5;
			buttonClear.Text = "&Clear";
			buttonClear.UseVisualStyleBackColor = true;
			buttonClear.Click += new System.EventHandler(buttonClear_Click);
			buttonProcess.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonProcess.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonProcess.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonProcess.Location = new System.Drawing.Point(711, 52);
			buttonProcess.Name = "buttonProcess";
			buttonProcess.Size = new System.Drawing.Size(110, 25);
			buttonProcess.TabIndex = 2;
			buttonProcess.Text = "Process";
			buttonProcess.UseVisualStyleBackColor = true;
			buttonProcess.Click += new System.EventHandler(buttonProcess_Click);
			bkSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			bkSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			bkSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			bkSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			bkSave.Location = new System.Drawing.Point(10, 602);
			bkSave.Name = "bkSave";
			bkSave.Size = new System.Drawing.Size(110, 28);
			bkSave.TabIndex = 4;
			bkSave.Text = "Save";
			bkSave.UseVisualStyleBackColor = true;
			bkSave.Click += new System.EventHandler(bkSave_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(1091, 655);
			base.Controls.Add(labelurc);
			base.Controls.Add(labelrc);
			base.Controls.Add(labelfc);
			base.Controls.Add(labelUnRead);
			base.Controls.Add(labelFRead);
			base.Controls.Add(labelFCount);
			base.Controls.Add(progressBarLoad);
			base.Controls.Add(zoomTrackBarControl1);
			base.Controls.Add(buttonClear);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxSelectedPath);
			base.Controls.Add(buttonProcess);
			base.Controls.Add(cbCharRects);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(cbPixLines);
			base.Controls.Add(bkSave);
			base.Controls.Add(cbBin);
			base.Controls.Add(lbWait);
			base.Controls.Add(splitContainer1);
			base.Controls.Add(statusStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "DocumentOCRForm";
			Text = "Automated Documents Attachment";
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(OnFormClosed);
			base.Load += new System.EventHandler(OnFormLoad);
			base.Resize += new System.EventHandler(OnPanelResize);
			statusStrip1.ResumeLayout(false);
			statusStrip1.PerformLayout();
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)PicBox.Properties).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)zoomTrackBarControl1.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)zoomTrackBarControl1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
