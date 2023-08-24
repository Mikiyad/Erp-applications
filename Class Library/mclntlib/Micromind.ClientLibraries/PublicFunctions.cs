using Micromind.Common.Data;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	public sealed class PublicFunctions
	{
		private static OpenFileDialog openFileDialog = new OpenFileDialog();

		private static Cursor prevCursor = null;

		public static void FormatNumber(TextBox textBox)
		{
			try
			{
				textBox.Text = decimal.Parse(textBox.Text).ToString(Format.TextBoxMoney);
			}
			catch
			{
			}
		}

		public static void DeFormatNumber(TextBox textBox)
		{
			try
			{
				textBox.Text = Global.RemoveChar(textBox.Text, ',');
			}
			catch
			{
			}
		}

		public static string GetSysDocTypeString(int sysDocType)
		{
			if (sysDocType >= 1000)
			{
				switch (sysDocType)
				{
				case 1000:
					return "Exch. Gain/Loss";
				case 1001:
					return "Exch. Gain/Loss";
				case 1002:
				case 1003:
				case 1004:
					return "Opening Balance";
				default:
					return "";
				}
			}
			switch (sysDocType)
			{
			case 8:
				return "Cash Expense";
			case 4:
				return "Cash Payment";
			case 34:
				return "Cash Purchase";
			case 35:
				return "Cash Pur Return";
			case 3:
				return "Cash Receipt";
			case 28:
				return "Cash Sale Return";
			case 7:
				return "Cheque Maturity";
			case 14:
				return "Issued Cheque Cleared";
			case 9:
				return "Cheque Expense";
			case 5:
				return "Cheque Payment";
			case 2:
				return "Cheque Receipt";
			case 11:
				return "Credit Note";
			case 37:
				return "Purchase Return";
			case 27:
				return "Sales Return";
			case 90:
				return "ConsignIn Closing";
			case 205:
				return "Opening Inventory";
			case 202:
				return "Opening Balance";
			case 203:
				return "Opening Balance";
			case 204:
				return "Opening Balance";
			case 10:
				return "Debit Note";
			case 24:
				return "Delivery Note";
			case 53:
				return "Export Delivery Note";
			case 29:
				return "Delivery Ret.";
			case 41:
				return "Deposit";
			case 42:
				return "Expense";
			case 6:
				return "Transfer";
			case 1:
				return "Journal Entry";
			case 39:
				return "Imp.Purchase Invoice";
			case 38:
				return "Imp.Purchase Order";
			case 18:
				return "Inventory Adj";
			case 15:
				return "Issued Chq Cancellation";
			case 16:
				return "Issued Chq Ret";
			case 17:
				return "Issued Security Chq";
			case 36:
				return "Packing List";
			case 33:
				return "Purchase Invoice";
			case 51:
				return "Sales Invoice";
			case 31:
				return "Purchase Order";
			case 30:
				return "Purchase Quote";
			case 32:
				return "GRN";
			case 50:
				return "Import GRN";
			case 13:
				return "Rec.Chq Cancellation";
			case 12:
				return "Returned Cheque";
			case 21:
				return "Stock Transfer Ret";
			case 25:
				return "Sales Invoice";
			case 23:
				return "Sales Order";
			case 22:
				return "Sales Quote";
			case 26:
				return "Cash Sale";
			case 20:
				return "Stock Transfer In";
			case 19:
				return "Stock Transfer Out";
			case 43:
				return "Salary Payable";
			case 47:
				return "Consign Out";
			case 44:
				return "Salary Payment";
			case 45:
				return "Loan/Advance";
			case 48:
				return "Con.Settlement";
			case 49:
				return "Exchange Gain/Loss";
			case 54:
				return "Con.Out Return";
			case 55:
				return "Consign In";
			case 56:
				return "Con.In Settlement";
			case 57:
				return "Con.In Return";
			case 46:
				return "POS Receipt";
			case 62:
				return "POS Return";
			case 64:
				return "TT Payment";
			case 65:
				return "TT Receipt";
			case 79:
				return "TR";
			case 80:
				return "TR Payment";
			case 66:
				return "Cash Receipt";
			case 67:
				return "Assembly Build";
			case 68:
				return "Emp Loan Payment";
			case 69:
				return "Salary Payment Cash";
			case 70:
				return "Salary Payment Chq";
			case 71:
				return "PRJ Product Issue";
			case 72:
				return "PRJ Product Issue";
			case 73:
				return "PRJ Expense Issue";
			case 74:
				return "Project Invoice";
			case 76:
				return "PRJ Timesheet";
			case 40:
				return "Direct Inv. Transfer";
			case 87:
				return "Inv. None Sale";
			case 89:
				return "Inventory Repacking";
			case 94:
				return "LPO Receipt";
			case 112:
				return "Garment Rental";
			case 113:
				return "Garment Rental Return";
			case 95:
				return "GRN Return";
			case 88:
				return "Opening Inventory";
			case 84:
				return "ChequeDiscount";
			case 116:
				return "Purchase Non-Inventory";
			case 248:
				return "TR Application";
			case 238:
				return "Discount Allocation";
			case 241:
				return "Write Off";
			case 101:
				return "Property Rental";
			case 102:
				return "Property Renewal";
			case 103:
				return "Property Cancel";
			case 104:
				return "Property Rent Post";
			case 260:
				return "Property Service Invoice";
			case 255:
				return "Property Service Request";
			case 258:
				return "Property Service Assign";
			case 215:
				return "Subcontract Order";
			case 218:
				return "Subcontract Invoice";
			case 259:
				return "Sales InvoiceNI";
			case 93:
				return "Employee Leave Payment";
			case 92:
				return "Employee Leave Encashment";
			case 267:
				return "Bill Discount";
			default:
				return sysDocType.ToString();
			}
		}

		public static bool IsListViewScrollVisible(ListView listView)
		{
			if (listView.Items.Count == 0)
			{
				return false;
			}
			if (listView.Width < checked(listView.ClientRectangle.Width + 10))
			{
				return false;
			}
			return true;
		}

		public static void GoHelp(Control control, string keyWord)
		{
			HelpProvider helpProvider = new HelpProvider();
			string text = Application.StartupPath + "\\Axolon Help.chm";
			if (!File.Exists(text))
			{
				ErrorHelper.WarningMessage("The help file cannot be found. ");
			}
			helpProvider.HelpNamespace = text;
			helpProvider.SetHelpKeyword(control, keyWord);
			helpProvider.SetHelpNavigator(control, HelpNavigator.KeywordIndex);
		}

		public static void GoHelp(Control control, string keyWord, HelpNavigator helpNavigator)
		{
			HelpProvider helpProvider = new HelpProvider();
			string text = Application.StartupPath + "\\Axolon Help.chm";
			if (!File.Exists(text))
			{
				ErrorHelper.WarningMessage("The help file cannot be found.");
			}
			helpProvider.HelpNamespace = text;
			helpProvider.SetHelpKeyword(control, keyWord);
			helpProvider.SetHelpNavigator(control, helpNavigator);
		}

		public static void OnHelpButtonClick(object o, HelpEventArgs e)
		{
			try
			{
				Form form = (Form)o;
				GoHelp(form, form.Text.Trim());
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public static int GetNumberOfDecimals(string number)
		{
			try
			{
				int num = number.IndexOf(".");
				if (num < 0)
				{
					return 0;
				}
				return checked(number.Length - num - 1);
			}
			catch
			{
				return 0;
			}
		}

		public static int GetNumberOfNumbers(string number)
		{
			if (number.Length == 0)
			{
				return 0;
			}
			number = Global.RemoveChar(number, ',');
			try
			{
				if (GetNumberOfDecimals(number) == 0)
				{
					return number.Length;
				}
				return checked(number.Length - 1);
			}
			catch
			{
				return 0;
			}
		}

		public static string GetFixedText(string text)
		{
			checked
			{
				int num;
				for (num = text.IndexOf("&", 0); num >= 0; num = text.IndexOf("&", num))
				{
					if (text.IndexOf("&", num + 1, 1) >= 0)
					{
						text = text.Remove(num + 1, 1);
					}
					else
					{
						num++;
					}
					num++;
				}
				return text;
			}
		}

		public static string GetChangedText(string text)
		{
			checked
			{
				int num;
				for (num = text.IndexOf("&", 0); num >= 0; num = text.IndexOf("&", num))
				{
					if (text.IndexOf("&", num + 1, 1) < 0)
					{
						text = text.Substring(0, num) + "&&" + text.Substring(num + 1);
						num++;
					}
					else
					{
						num++;
					}
					num++;
				}
				return text;
			}
		}

		public static void StartWaiting(Form form)
		{
			if (form != null)
			{
				prevCursor = null;
				form.Cursor = Cursors.WaitCursor;
				if (form.Parent != null)
				{
					form.Parent.Cursor = Cursors.WaitCursor;
				}
				Cursor.Current = Cursors.WaitCursor;
			}
		}

		public static void EndWaiting(Form form)
		{
			if (form != null)
			{
				if (prevCursor == null)
				{
					prevCursor = Cursors.Arrow;
				}
				form.Cursor = prevCursor;
				if (form.Parent != null)
				{
					form.Parent.Cursor = prevCursor;
				}
			}
		}

		public static void SaveListViewHeadersPosition(ListView list)
		{
			try
			{
				string name = list.FindForm().Name;
				foreach (ColumnHeader column in list.Columns)
				{
					Global.CompanySettings.SaveSetting(name + "Header" + column.Index, column.Width);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public static void LoadListViewHeadersPosition(ListView list)
		{
			string name = list.FindForm().Name;
			foreach (ColumnHeader column in list.Columns)
			{
				object setting = Global.CompanySettings.GetSetting(name + "Header" + column.Index);
				if (setting == null)
				{
					throw new ApplicationException("No column found for this list view.");
				}
				try
				{
					column.Width = int.Parse(setting.ToString());
				}
				catch
				{
					throw;
				}
			}
		}

		public static Color GetColorFromRGB(string strColor, Color defaultColor)
		{
			if (strColor == null || strColor.Length == 0)
			{
				return defaultColor;
			}
			try
			{
				string[] array = strColor.Split(',');
				if (array.Length == 3)
				{
					int red = int.Parse(array[0]);
					int green = int.Parse(array[1]);
					int blue = int.Parse(array[2]);
					return Color.FromArgb(red, green, blue);
				}
				return defaultColor;
			}
			catch
			{
				return defaultColor;
			}
		}

		public static bool AddImage()
		{
			string text = "";
			openFileDialog.CheckPathExists = true;
			openFileDialog.ValidateNames = true;
			openFileDialog.CheckFileExists = true;
			openFileDialog.Filter = "Pictures (*.BMP;*.JPG)|*.BMP;*.JPG";
			openFileDialog.Title = "Add Logo";
			DialogResult dialogResult = openFileDialog.ShowDialog();
			if (dialogResult == DialogResult.Cancel)
			{
				return false;
			}
			MemoryStream memoryStream = null;
			if (dialogResult == DialogResult.OK)
			{
				text = openFileDialog.FileName;
				try
				{
					if (text.Length == 0)
					{
						return false;
					}
					Image image = Image.FromFile(text);
					memoryStream = new MemoryStream();
					image.Save(memoryStream, ImageFormat.Bmp);
					text = text.Substring(checked(text.LastIndexOf("\\") + 1));
					int num = -1;
					try
					{
						num = int.Parse(Factory.CompanyInformationSystem.GetCompanyInformation().CompanyInformationTable.Rows[0]["SetupID"].ToString());
					}
					catch (Exception e)
					{
						ErrorHelper.ProcessError(e);
						return false;
					}
					Factory.CompanyInformationSystem.SaveLogo(num, text, memoryStream);
					Global.CompanyLogo = image;
					memoryStream.Close();
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
					return false;
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Close();
						memoryStream = null;
					}
				}
				return true;
			}
			return false;
		}

		public static bool ClearLogo()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete the logo?") != DialogResult.Yes)
				{
					return false;
				}
				int num = -1;
				try
				{
					num = int.Parse(Factory.CompanyInformationSystem.GetCompanyInformation().CompanyInformationTable.Rows[0]["SetupID"].ToString());
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
				Factory.CompanyInformationSystem.SaveLogo(num, "", null);
				Global.CompanyLogo = null;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
				return false;
			}
			return true;
		}

		public static Image ConvertImageToGrayscale(Image image)
		{
			Bitmap bitmap = new Bitmap(image);
			Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height);
			checked
			{
				for (int i = 0; i < bitmap2.Height; i++)
				{
					for (int j = 0; j < bitmap2.Width; j++)
					{
						Color pixel = bitmap.GetPixel(j, i);
						int num = (int)unchecked((double)(int)pixel.R * 0.3 + (double)(int)pixel.G * 0.59 + (double)(int)pixel.B * 0.11);
						bitmap2.SetPixel(j, i, Color.FromArgb(num, num, num));
					}
				}
				Stream stream = new MemoryStream();
				bitmap2.Save(stream, ImageFormat.Bmp);
				return Image.FromStream(stream);
			}
		}

		public static void StartProcess(string exePath, params string[] arguments)
		{
			string text = "";
			if (arguments.Length != 0)
			{
				foreach (string str in arguments)
				{
					text = text + str + " ";
				}
			}
			try
			{
				Process.Start(new ProcessStartInfo(exePath)
				{
					Arguments = text,
					WorkingDirectory = Path.GetDirectoryName(exePath)
				});
			}
			catch
			{
			}
		}

		public static bool IsCorrectCreditCardNumber(string cardNumber)
		{
			int[] array = new int[16];
			int[] array2 = new int[16];
			int[] array3 = new int[16];
			int[] array4 = new int[16];
			int num = 0;
			checked
			{
				for (int i = 0; i <= 15; i++)
				{
					array[i] = -1;
				}
				char[] array5 = cardNumber.ToCharArray();
				for (int j = 0; j <= array5.Length - 1; j++)
				{
					array[j] = Convert.ToInt32(array5[j]) - 48;
				}
				if (array[15] != -1)
				{
					for (int k = 1; k <= 15; k += 2)
					{
						array2[k] = 1;
					}
					for (int l = 0; l <= 15; l += 2)
					{
						array2[l] = 2;
					}
					array4[0] = 0;
					for (num = 0; num <= 15; num++)
					{
						array3[num] = array[num] * array2[num];
						if (array3[num] > 9)
						{
							array3[num] -= 9;
						}
						else
						{
							array3[num] = array3[num];
						}
						if (num == 0)
						{
							array4[num] = array3[num];
						}
						if (num != 0)
						{
							array4[num] = array4[num - 1] + array3[num];
						}
						if (unchecked(array4[num] % 10) == 0)
						{
							return true;
						}
					}
				}
				if (array[14] != -1 && array[15] == -1)
				{
					for (int m = 1; m <= 14; m += 2)
					{
						array2[m] = 2;
					}
					for (int n = 0; n <= 14; n += 2)
					{
						array2[n] = 1;
					}
					for (int num2 = 0; num2 <= 14; num2++)
					{
						array4[0] = 0;
					}
					for (num = 0; num <= 14; num++)
					{
						array3[num] = array[num] * array2[num];
						if (array3[num] > 9)
						{
							array3[num] -= 9;
						}
						else
						{
							array3[num] = array3[num];
						}
						if (num == 0)
						{
							array4[num] = array3[num];
						}
						if (num != 0)
						{
							array4[num] = array4[num - 1] + array3[num];
						}
						if (unchecked(array4[num] % 10) == 0)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public static string ReplaceAny(string txtData, int startIndex, int length, char ch)
		{
			if (length <= 0)
			{
				return txtData;
			}
			string text = "";
			checked
			{
				for (int i = startIndex; i < length && i < txtData.Length; i++)
				{
					text += ch.ToString();
				}
				for (int j = length; j < txtData.Length && j < txtData.Length; j++)
				{
					text += txtData[j].ToString();
				}
				return text;
			}
		}

		public static void PlayAlarmSound()
		{
			try
			{
				if (bool.Parse(Global.GlobalSettings.GetSetting("PlaySoundOnAlarm", "true").ToString()))
				{
					Win32API.PlaySound(Global.GlobalSettings.GetSetting("AlarmSoundFile", Application.StartupPath + Path.DirectorySeparatorChar.ToString() + "reminder.wav").ToString(), 0, 1);
				}
			}
			catch
			{
				throw;
			}
		}

		public static Image GetEmployeeThumbnailImage(string employeeID)
		{
			try
			{
				byte[] employeeThumbnailImage = Factory.EmployeeSystem.GetEmployeeThumbnailImage(employeeID);
				if (employeeThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(employeeThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static Image GetPatientThumbnailImage(string patientID)
		{
			try
			{
				byte[] patientThumbnailImage = Factory.PatientSystem.GetPatientThumbnailImage(patientID);
				if (patientThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(patientThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static Image GetHorseThumbnailImage(string employeeID)
		{
			try
			{
				byte[] horseThumbnailImage = Factory.HorseSummarySystem.GetHorseThumbnailImage(employeeID);
				if (horseThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(horseThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static Image GetProductThumbnailImage(string productID, bool isProductParentID)
		{
			try
			{
				byte[] array = (!isProductParentID) ? Factory.ProductSystem.GetProductThumbnailImage(productID) : Factory.ProductParentSystem.GetProductThumbnailImage(productID);
				if (array == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(array), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static Image GetContactThumbnailImage(string contactID)
		{
			try
			{
				byte[] contactThumbnailImage = Factory.ContactSystem.GetContactThumbnailImage(contactID);
				if (contactThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(contactThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static Image GetCandidateThumbnailImage(string candidateID)
		{
			try
			{
				byte[] candidateThumbnailImage = Factory.CandidateSystem.GetCandidateThumbnailImage(candidateID);
				if (candidateThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(candidateThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static Image GetVehicleThumbnailImage(string vehicleID)
		{
			try
			{
				byte[] vehicleThumbnailImage = Factory.VehicleSystem.GetVehicleThumbnailImage(vehicleID);
				if (vehicleThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(vehicleThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static Image GetGadgetThumbnailImage(string gadgetID)
		{
			try
			{
				byte[] gadgetThumbnailImage = Factory.CustomGadgetSystem.GetGadgetThumbnailImage(gadgetID);
				if (gadgetThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(gadgetThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static Image GetPropertyThumbnailImage(string propertyID)
		{
			try
			{
				byte[] propertyThumbnailImage = Factory.PropertySystem.GetPropertyThumbnailImage(propertyID);
				if (propertyThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(propertyThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static Image GetPropertyUnitThumbnailImage(string unitID)
		{
			try
			{
				byte[] propertyUnitThumbnailImage = Factory.PropertyUnitSystem.GetPropertyUnitThumbnailImage(unitID);
				if (propertyUnitThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(propertyUnitThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static bool ThumbnailImageAbort()
		{
			return true;
		}

		public static bool AddProductPhoto(string productID, Image image, bool isMatrixParent)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (array.Length / 1024 > 200)
				{
					throw new Exception("Image size is larger than allowed. Image size should be maximum 200 KB.");
				}
				if (!isMatrixParent)
				{
					return Factory.ProductSystem.AddProductPhoto(productID, array);
				}
				return Factory.ProductParentSystem.AddProductPhoto(productID, array);
			}
			return false;
		}

		public static bool AddEmployeePhoto(string employeeID, Image image)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (array.Length / 1024 > 200)
				{
					throw new Exception("Image size is larger than allowed. Image size should be maximum 200 KB.");
				}
				return Factory.EmployeeSystem.AddEmployeePhoto(employeeID, array);
			}
			return false;
		}

		public static bool AddPatientPhoto(string patientID, Image image)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (array.Length / 1024 > 200)
				{
					throw new Exception("Image size is larger than allowed. Image size should be maximum 200 KB.");
				}
				return Factory.PatientSystem.AddPatientPhoto(patientID, array);
			}
			return false;
		}

		public static bool AddHorsePhoto(string HorseID, Image image)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (array.Length / 1024 > 200)
				{
					throw new Exception("Image size is larger than allowed. Image size should be maximum 200 KB.");
				}
				return Factory.HorseSummarySystem.AddHorsePhoto(HorseID, array);
			}
			return false;
		}

		public static bool AddContactPhoto(string employeeID, Image image)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (array.Length / 1024 > 200)
				{
					throw new Exception("Image size is larger than allowed. Image size should be maximum 200 KB.");
				}
				return Factory.ContactSystem.AddContactPhoto(employeeID, array);
			}
			return false;
		}

		public static bool AddVehiclePhoto(string vehicleID, Image image)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (array.Length / 1024 > 200)
				{
					throw new Exception("Image size is larger than allowed. Image size should be maximum 200 KB.");
				}
				return Factory.VehicleSystem.AddVehiclePhoto(vehicleID, array);
			}
			return false;
		}

		public static bool AddGadgetPhoto(string productID, Image image, bool isMatrixParent)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (image.Height > 256 || image.Width > 256)
				{
					throw new Exception("Image size not be exceed than 256x256 px");
				}
				return Factory.CustomGadgetSystem.AddGadgetPhoto(productID, array);
			}
			return false;
		}

		public static bool AddPropertyPhoto(string propertyID, Image image, bool isMatrixParent)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (image.Height > 256 || image.Width > 256)
				{
					throw new Exception("Image size not be exceed than 256x256 px");
				}
				return Factory.PropertySystem.AddPropertyPhoto(propertyID, array);
			}
			return false;
		}

		public static bool AddPropertyUnitPhoto(string unitID, Image image, bool isMatrixParent)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (image.Height > 256 || image.Width > 256)
				{
					throw new Exception("Image size not be exceed than 256x256 px");
				}
				return Factory.PropertyUnitSystem.AddPropertyUnitPhoto(unitID, array);
			}
			return false;
		}

		public static string GetNextCardNumber(string tableName, string idFieldName, string filterColumnName, string filterValue)
		{
			try
			{
				if (!CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AutoCardCode, defaultValue: false))
				{
					return "";
				}
				return Factory.SystemDocumentSystem.GetNextCardNumber(tableName, idFieldName, filterColumnName, filterValue);
			}
			catch
			{
				throw;
			}
		}

		public static string GetNextCardNumber(string tableName, string idFieldName)
		{
			try
			{
				return GetNextCardNumber(tableName, idFieldName, "", "");
			}
			catch
			{
				throw;
			}
		}

		public static bool AddCandidatePhoto(string candidateID, Image image)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (array.Length / 1024 > 200)
				{
					throw new Exception("Image size is larger than allowed. Image size should be maximum 200 KB.");
				}
				return Factory.CandidateSystem.AddCandidatePhoto(candidateID, array);
			}
			return false;
		}

		public static bool AddLogo(Image image)
		{
			if (image != null)
			{
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				array = memoryStream.ToArray();
				if (array.Length / 1024 > 200)
				{
					throw new Exception("Image size is larger than allowed. Image size should be maximum 100 KB.");
				}
				return Factory.CompanyInformationSystem.AddLogo(array);
			}
			return false;
		}

		public static Image GetLogoThumbnailImage()
		{
			checked
			{
				try
				{
					byte[] logoThumbnailImage = Factory.CompanyInformationSystem.GetLogoThumbnailImage();
					if (logoThumbnailImage == null)
					{
						return null;
					}
					Image image = Image.FromStream(new MemoryStream(logoThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
					if (image == null)
					{
						return null;
					}
					int num = 186;
					int num2 = 144;
					int width = image.Width;
					int height = image.Height;
					float num3 = 0f;
					float num4 = 0f;
					float num5 = 0f;
					num4 = (float)num / (float)width;
					num5 = (float)num2 / (float)height;
					num3 = ((!(num5 < num4)) ? num4 : num5);
					num = (int)((float)width * num3);
					num2 = (int)((float)height * num3);
					return image.GetThumbnailImage(num, num2, ThumbnailImageAbort, IntPtr.Zero);
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

		public static Image GetLogo()
		{
			checked
			{
				try
				{
					byte[] logoThumbnailImage = Factory.CompanyInformationSystem.GetLogoThumbnailImage();
					if (logoThumbnailImage == null)
					{
						return null;
					}
					Image image = Image.FromStream(new MemoryStream(logoThumbnailImage), useEmbeddedColorManagement: true, validateImageData: true);
					if (image == null)
					{
						return null;
					}
					int num = 186;
					int num2 = 144;
					int width = image.Width;
					int height = image.Height;
					float num3 = 0f;
					float num4 = 0f;
					float num5 = 0f;
					num4 = (float)num / (float)width;
					num5 = (float)num2 / (float)height;
					num3 = ((!(num5 < num4)) ? num4 : num5);
					num = (int)((float)width * num3);
					num2 = (int)((float)height * num3);
					return image.GetThumbnailImage(num, num2, ThumbnailImageAbort, IntPtr.Zero);
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

		public bool ByteArrayToFile(string fileName, byte[] byteArray)
		{
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				fileStream.Write(byteArray, 0, byteArray.Length);
				fileStream.Close();
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			return false;
		}

		public byte[] ReadFileToByteArray(string fileName)
		{
			try
			{
				return File.ReadAllBytes(fileName);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		public static bool AddCustomerSignature(string customerID, Image image)
		{
			if (image != null)
			{
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Position = 0L;
				memoryStream.Read(array, 0, array.Length);
				if (array.Length / 1024 > 200)
				{
					throw new Exception("Image size is larger than allowed. Image size should be maximum 200 KB.");
				}
				return Factory.CustomerSystem.AddCustomerSignature(customerID, array);
			}
			return false;
		}

		public static Image GetCustomerSignatureThumbnailImage(string customerID)
		{
			try
			{
				byte[] customerSignatureThumbnailImage = Factory.CustomerSystem.GetCustomerSignatureThumbnailImage(customerID);
				if (customerSignatureThumbnailImage == null)
				{
					return null;
				}
				return Image.FromStream(new MemoryStream(customerSignatureThumbnailImage));
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
