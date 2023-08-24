using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class TextControl : RichTextBox
	{
		private StringReader myReader;

		private FontDialog fontDialog;

		private SaveFileDialog saveFileDialog;

		private ColorDialog colorDialog;

		private PrintDialog printDialog;

		private PrintDocument printDocument;

		private OpenFileDialog openFileDialog;

		private ContextMenu contextMenu;

		private MenuItem menuItemUndo;

		private MenuItem menuItem2;

		private MenuItem menuItemCut;

		private MenuItem menuItemCopy;

		private MenuItem menuItemPaste;

		private MenuItem menuItemSelectAll;

		private MenuItem menuItem1;

		private MenuItem menuItemParagraph;

		private MenuItem menuItemLeft;

		private MenuItem menuItemCenter;

		private MenuItem menuItemRight;

		private MenuItem menuItem8;

		private MenuItem menuItemBullets;

		private MenuItem menuItemBackcolor;

		private MenuItem menuItemBlue;

		private MenuItem menuItemGreen;

		private MenuItem menuItemPink;

		private MenuItem menuItemYellow;

		private MenuItem menuItemWhite;

		private MenuItem menuItemInsertDateTime;

		private MenuItem menuItemSep3;

		private MenuItem menuItemFont;

		private MenuItem menuItemColor;

		private MenuItem menuItemSep4;

		private MenuItem menuItemOpen;

		private MenuItem menuItemSave;

		private MenuItem menuItemPrint;

		private Container components;

		public TextControl()
		{
			InitializeComponent();
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
			fontDialog = new System.Windows.Forms.FontDialog();
			saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			colorDialog = new System.Windows.Forms.ColorDialog();
			printDialog = new System.Windows.Forms.PrintDialog();
			printDocument = new System.Drawing.Printing.PrintDocument();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			contextMenu = new System.Windows.Forms.ContextMenu();
			menuItemUndo = new System.Windows.Forms.MenuItem();
			menuItem2 = new System.Windows.Forms.MenuItem();
			menuItemCut = new System.Windows.Forms.MenuItem();
			menuItemCopy = new System.Windows.Forms.MenuItem();
			menuItemPaste = new System.Windows.Forms.MenuItem();
			menuItemSelectAll = new System.Windows.Forms.MenuItem();
			menuItem1 = new System.Windows.Forms.MenuItem();
			menuItemParagraph = new System.Windows.Forms.MenuItem();
			menuItemLeft = new System.Windows.Forms.MenuItem();
			menuItemCenter = new System.Windows.Forms.MenuItem();
			menuItemRight = new System.Windows.Forms.MenuItem();
			menuItem8 = new System.Windows.Forms.MenuItem();
			menuItemBullets = new System.Windows.Forms.MenuItem();
			menuItemBackcolor = new System.Windows.Forms.MenuItem();
			menuItemBlue = new System.Windows.Forms.MenuItem();
			menuItemGreen = new System.Windows.Forms.MenuItem();
			menuItemPink = new System.Windows.Forms.MenuItem();
			menuItemYellow = new System.Windows.Forms.MenuItem();
			menuItemWhite = new System.Windows.Forms.MenuItem();
			menuItemInsertDateTime = new System.Windows.Forms.MenuItem();
			menuItemSep3 = new System.Windows.Forms.MenuItem();
			menuItemFont = new System.Windows.Forms.MenuItem();
			menuItemColor = new System.Windows.Forms.MenuItem();
			menuItemSep4 = new System.Windows.Forms.MenuItem();
			menuItemOpen = new System.Windows.Forms.MenuItem();
			menuItemSave = new System.Windows.Forms.MenuItem();
			menuItemPrint = new System.Windows.Forms.MenuItem();
			printDialog.Document = printDocument;
			printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);
			contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[17]
			{
				menuItemUndo,
				menuItem2,
				menuItemCut,
				menuItemCopy,
				menuItemPaste,
				menuItemSelectAll,
				menuItem1,
				menuItemParagraph,
				menuItemBackcolor,
				menuItemInsertDateTime,
				menuItemSep3,
				menuItemFont,
				menuItemColor,
				menuItemSep4,
				menuItemOpen,
				menuItemSave,
				menuItemPrint
			});
			contextMenu.Popup += new System.EventHandler(contextMenu_Popup);
			menuItemUndo.Index = 0;
			menuItemUndo.Text = "Undo";
			menuItemUndo.Click += new System.EventHandler(menuItemUndo_Click);
			menuItem2.Index = 1;
			menuItem2.Text = "-";
			menuItemCut.Index = 2;
			menuItemCut.Text = "Cut";
			menuItemCut.Click += new System.EventHandler(menuItemCut_Click);
			menuItemCopy.Index = 3;
			menuItemCopy.Text = "Copy";
			menuItemCopy.Click += new System.EventHandler(menuItemCopy_Click);
			menuItemPaste.Index = 4;
			menuItemPaste.Text = "Paste";
			menuItemPaste.Click += new System.EventHandler(menuItemPaste_Click);
			menuItemSelectAll.Index = 5;
			menuItemSelectAll.Text = "Select All";
			menuItemSelectAll.Click += new System.EventHandler(menuItemSelectAll_Click);
			menuItem1.Index = 6;
			menuItem1.Text = "-";
			menuItemParagraph.Index = 7;
			menuItemParagraph.MenuItems.AddRange(new System.Windows.Forms.MenuItem[5]
			{
				menuItemLeft,
				menuItemCenter,
				menuItemRight,
				menuItem8,
				menuItemBullets
			});
			menuItemParagraph.Text = "Paragraph";
			menuItemLeft.Index = 0;
			menuItemLeft.Text = "Left";
			menuItemLeft.Click += new System.EventHandler(menuItemLeft_Click);
			menuItemCenter.Index = 1;
			menuItemCenter.Text = "Center";
			menuItemCenter.Click += new System.EventHandler(menuItemCenter_Click);
			menuItemRight.Index = 2;
			menuItemRight.Text = "Right";
			menuItemRight.Click += new System.EventHandler(menuItemRight_Click);
			menuItem8.Index = 3;
			menuItem8.Text = "-";
			menuItemBullets.Index = 4;
			menuItemBullets.Text = "Bullets";
			menuItemBullets.Click += new System.EventHandler(menuItemBullets_Click);
			menuItemBackcolor.Index = 8;
			menuItemBackcolor.MenuItems.AddRange(new System.Windows.Forms.MenuItem[5]
			{
				menuItemBlue,
				menuItemGreen,
				menuItemPink,
				menuItemYellow,
				menuItemWhite
			});
			menuItemBackcolor.Text = "Color";
			menuItemBlue.Index = 0;
			menuItemBlue.Text = "&Blue";
			menuItemBlue.Click += new System.EventHandler(menuItemBlue_Click);
			menuItemGreen.Index = 1;
			menuItemGreen.Text = "&Green";
			menuItemGreen.Click += new System.EventHandler(menuItemGreen_Click);
			menuItemPink.Index = 2;
			menuItemPink.Text = "&Pink";
			menuItemPink.Click += new System.EventHandler(menuItemPink_Click);
			menuItemYellow.Index = 3;
			menuItemYellow.Text = "&Yellow";
			menuItemYellow.Click += new System.EventHandler(menuItemYellow_Click);
			menuItemWhite.Index = 4;
			menuItemWhite.Text = "&White";
			menuItemWhite.Click += new System.EventHandler(menuItemWhite_Click);
			menuItemInsertDateTime.Index = 9;
			menuItemInsertDateTime.Text = "Date Time";
			menuItemInsertDateTime.Click += new System.EventHandler(menuItemInsertDateTime_Click);
			menuItemSep3.Index = 10;
			menuItemSep3.Text = "-";
			menuItemFont.Index = 11;
			menuItemFont.Text = "Text Font...";
			menuItemFont.Click += new System.EventHandler(menuItemFont_Click);
			menuItemColor.Index = 12;
			menuItemColor.Text = "Text Color...";
			menuItemColor.Click += new System.EventHandler(menuItemColor_Click);
			menuItemSep4.Index = 13;
			menuItemSep4.Text = "-";
			menuItemOpen.Index = 14;
			menuItemOpen.Text = "Open...";
			menuItemOpen.Click += new System.EventHandler(menuItemOpen_Click);
			menuItemSave.Index = 15;
			menuItemSave.Text = "Save As...";
			menuItemSave.Click += new System.EventHandler(menuItemSave_Click);
			menuItemPrint.Index = 16;
			menuItemPrint.Text = "Print...";
			menuItemPrint.Click += new System.EventHandler(menuItemPrint_Click);
			ContextMenu = contextMenu;
			Font = new System.Drawing.Font("Tahoma", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Size = new System.Drawing.Size(472, 148);
			base.Resize += new System.EventHandler(TextControl_Resize);
			base.BackColorChanged += new System.EventHandler(TextControl_BackColorChanged);
		}

		private void TextControl_Resize(object sender, EventArgs e)
		{
		}

		private void TextControl_Load(object sender, EventArgs e)
		{
			base.DetectUrls = true;
		}

		private void menuItemCut_Click(object sender, EventArgs e)
		{
			Cut();
		}

		private void menuItemCopy_Click(object sender, EventArgs e)
		{
			if (SelectedText.Length == 0)
			{
				SelectAll();
				Copy();
				SelectionLength = 0;
			}
			else
			{
				Copy();
			}
		}

		private void menuItemPaste_Click(object sender, EventArgs e)
		{
			Paste();
		}

		private void contextMenu_Popup(object sender, EventArgs e)
		{
			if (!base.CanUndo)
			{
				menuItemUndo.Enabled = false;
			}
			else
			{
				menuItemUndo.Enabled = true;
			}
			if (base.SelectionBullet)
			{
				menuItemBullets.Checked = true;
			}
			else
			{
				menuItemBullets.Checked = false;
			}
			if (base.SelectionAlignment == HorizontalAlignment.Center)
			{
				menuItemCenter.Checked = true;
			}
			else
			{
				menuItemCenter.Checked = false;
			}
			if (base.SelectionAlignment == HorizontalAlignment.Left)
			{
				menuItemLeft.Checked = true;
			}
			else
			{
				menuItemLeft.Checked = false;
			}
			if (base.SelectionAlignment == HorizontalAlignment.Right)
			{
				menuItemRight.Checked = true;
			}
			else
			{
				menuItemRight.Checked = false;
			}
			bool flag6;
			bool flag8;
			bool flag10;
			bool visible;
			if (base.ReadOnly)
			{
				menuItemPaste.Enabled = false;
				menuItemCut.Enabled = false;
				menuItemUndo.Enabled = false;
				MenuItem menuItem = menuItemColor;
				MenuItem menuItem2 = menuItemFont;
				MenuItem menuItem3 = menuItemParagraph;
				MenuItem menuItem4 = menuItemSep3;
				MenuItem menuItem5 = menuItemInsertDateTime;
				MenuItem menuItem6 = menuItemSep4;
				bool flag2 = menuItemBackcolor.Visible = false;
				bool flag4 = menuItem6.Visible = flag2;
				flag6 = (menuItem5.Visible = flag4);
				flag8 = (menuItem4.Visible = flag6);
				flag10 = (menuItem3.Visible = flag8);
				visible = (menuItem2.Visible = flag10);
				menuItem.Visible = visible;
			}
			MenuItem menuItem7 = menuItemBlue;
			MenuItem menuItem8 = menuItemPink;
			MenuItem menuItem9 = menuItemWhite;
			MenuItem menuItem10 = menuItemYellow;
			flag6 = (menuItemGreen.Checked = false);
			flag8 = (menuItem10.Checked = flag6);
			flag10 = (menuItem9.Checked = flag8);
			visible = (menuItem8.Checked = flag10);
			menuItem7.Checked = visible;
		}

		private void menuItemUndo_Click(object sender, EventArgs e)
		{
			Undo();
		}

		private void menuItemSelectAll_Click(object sender, EventArgs e)
		{
			SelectAll();
		}

		private void menuItemRightToLeft_Click(object sender, EventArgs e)
		{
		}

		private void richTextBox_RightToLeftChanged(object sender, EventArgs e)
		{
		}

		private void menuItemFont_Click(object sender, EventArgs e)
		{
			fontDialog.Font = base.SelectionFont;
			if (fontDialog.ShowDialog() == DialogResult.OK)
			{
				base.SelectionFont = fontDialog.Font;
			}
		}

		private void menuItemSave_Click(object sender, EventArgs e)
		{
			saveFileDialog.Filter = "Plain Text (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
			StreamWriter streamWriter = null;
			if (!base.ReadOnly)
			{
				saveFileDialog.FilterIndex = 2;
			}
			else
			{
				saveFileDialog.FilterIndex = 1;
			}
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if (saveFileDialog.FilterIndex == 1)
					{
						streamWriter = new StreamWriter(saveFileDialog.FileName, append: false);
						string[] lines = base.Lines;
						foreach (string value in lines)
						{
							streamWriter.WriteLine(value);
						}
					}
					else
					{
						SaveFile(saveFileDialog.FileName);
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
				finally
				{
					if (streamWriter != null)
					{
						streamWriter.Close();
						streamWriter = null;
					}
				}
			}
		}

		public string GetRTFFormatText()
		{
			string result = "";
			MemoryStream memoryStream = null;
			try
			{
				memoryStream = new MemoryStream();
				SaveFile(memoryStream, RichTextBoxStreamType.RichText);
				result = new ASCIIEncoding().GetString(memoryStream.GetBuffer());
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return result;
			}
			finally
			{
				memoryStream?.Close();
			}
		}

		public void LoadRTFFormatText(string text)
		{
			MemoryStream memoryStream = null;
			try
			{
				ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
				memoryStream = new MemoryStream();
				byte[] bytes = aSCIIEncoding.GetBytes(text);
				memoryStream.Write(bytes, 0, bytes.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				LoadFile(memoryStream, RichTextBoxStreamType.RichText);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				memoryStream?.Close();
			}
		}

		private void menuItemColor_Click(object sender, EventArgs e)
		{
			colorDialog.Color = base.SelectionColor;
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				base.SelectionColor = colorDialog.Color;
			}
		}

		private void menuItemBullets_Click(object sender, EventArgs e)
		{
			base.SelectionBullet = !menuItemBullets.Checked;
			menuItemBullets.Checked = !menuItemBullets.Checked;
		}

		private void menuItemLeft_Click(object sender, EventArgs e)
		{
			base.SelectionAlignment = HorizontalAlignment.Left;
		}

		private void menuItemCenter_Click(object sender, EventArgs e)
		{
			base.SelectionAlignment = HorizontalAlignment.Center;
		}

		private void menuItemRight_Click(object sender, EventArgs e)
		{
			base.SelectionAlignment = HorizontalAlignment.Right;
		}

		public void LoadBackcolor(string color)
		{
			try
			{
				if (color == null || color.Trim().Length == 0)
				{
					color = Global.CompanySettings.GetSetting(base.Name + "tcbc").ToString();
				}
				string[] array = color.Split(',');
				if (array.Length == 3)
				{
					int red = int.Parse(array[0]);
					int green = int.Parse(array[1]);
					int blue = int.Parse(array[2]);
					BackColor = Color.FromArgb(red, green, blue);
				}
			}
			catch
			{
			}
		}

		public string GetBackColor()
		{
			return BackColor.R + "," + BackColor.G + "," + BackColor.B;
		}

		private void menuItemBackcolor_Click(object sender, EventArgs e)
		{
			colorDialog.Color = BackColor;
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				BackColor = colorDialog.Color;
				try
				{
					string val = BackColor.R + "," + BackColor.G + "," + BackColor.B;
					Global.CompanySettings.SaveSetting(base.Name + "tcbc", val);
				}
				catch
				{
				}
			}
		}

		public void HideBackcolorMenuItem()
		{
			menuItemBackcolor.Visible = false;
		}

		private void menuItemDateTime_Click(object sender, EventArgs e)
		{
		}

		private void menuItemInsertDateTime_Click(object sender, EventArgs e)
		{
			SelectedText = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
		}

		private void menuItemPrint_Click(object sender, EventArgs e)
		{
			printDocument.PrinterSettings = Global.printDocument.PrinterSettings;
			printDialog.Document = printDocument;
			string text = Text;
			myReader = new StringReader(text);
			try
			{
				if (printDialog.ShowDialog() == DialogResult.OK)
				{
					printDocument.Print();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			float num = 0f;
			float num2 = 0f;
			int i = 0;
			float x = e.MarginBounds.Left;
			float num3 = e.MarginBounds.Top;
			string text = null;
			Font font = Font;
			SolidBrush solidBrush = new SolidBrush(Color.Black);
			for (num = (float)e.MarginBounds.Height / font.GetHeight(e.Graphics); (float)i < num; i++)
			{
				if ((text = myReader.ReadLine()) == null)
				{
					break;
				}
				num2 = num3 + (float)i * font.GetHeight(e.Graphics);
				e.Graphics.DrawString(text, font, solidBrush, x, num2, new StringFormat());
			}
			if (text != null)
			{
				e.HasMorePages = true;
			}
			else
			{
				e.HasMorePages = false;
			}
			solidBrush.Dispose();
		}

		private void menuItemOpen_Click(object sender, EventArgs e)
		{
			try
			{
				openFileDialog.Filter = "Plain Text (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					StreamReader streamReader = new StreamReader(new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read));
					streamReader.BaseStream.Seek(0L, SeekOrigin.Begin);
					string text = streamReader.ReadLine();
					string text2 = "";
					while (text != null)
					{
						text2 = text2 + text + "\n";
						text = streamReader.ReadLine();
					}
					if (openFileDialog.FilterIndex == 1)
					{
						Text = text2;
					}
					else
					{
						LoadRTFFormatText(text2);
					}
					streamReader.Close();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void menuItemBlue_Click(object sender, EventArgs e)
		{
		}

		private void menuItemPink_Click(object sender, EventArgs e)
		{
		}

		private void menuItemGreen_Click(object sender, EventArgs e)
		{
		}

		private void menuItemYellow_Click(object sender, EventArgs e)
		{
		}

		private void menuItemWhite_Click(object sender, EventArgs e)
		{
		}

		private void TextControl_BackColorChanged(object sender, EventArgs e)
		{
		}
	}
}
