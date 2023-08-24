using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Micromind.Facade
{
	public sealed class ContactSystem : MarshalByRefObject, IContactSystem, IDisposable
	{
		private Config config;

		public ContactSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateContact(ContactData data)
		{
			return new Contacts(config).InsertUpdateContact(data, isUpdate: false);
		}

		public bool UpdateContact(ContactData data)
		{
			return UpdateContact(data, checkConcurrency: false);
		}

		public bool UpdateContact(ContactData data, bool checkConcurrency)
		{
			return new Contacts(config).InsertUpdateContact(data, isUpdate: true);
		}

		public ContactData GetContact()
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.GetContact();
			}
		}

		public bool DeleteContact(string groupID)
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.DeleteContact(groupID);
			}
		}

		public ContactData GetContactByID(string id)
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.GetContactByID(id);
			}
		}

		public DataSet GetContactByFields(params string[] columns)
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.GetContactByFields(columns);
			}
		}

		public DataSet GetContactByFields(string[] ids, params string[] columns)
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.GetContactByFields(ids, columns);
			}
		}

		public DataSet GetContactByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.GetContactByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetContactList()
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.GetContactList();
			}
		}

		public DataSet GetContactComboList()
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.GetContactComboList();
			}
		}

		public bool AddContactPhoto(string employeeID, byte[] pictureByte)
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.AddContactPhoto(employeeID, pictureByte);
			}
		}

		public bool RemoveContactPhoto(string contactID)
		{
			using (Contacts contacts = new Contacts(config))
			{
				return contacts.RemoveContactPhoto(contactID);
			}
		}

		public byte[] GetContactThumbnailImage(string contactID)
		{
			using (Contacts contacts = new Contacts(config))
			{
				byte[] contactThumbnailImage = contacts.GetContactThumbnailImage(contactID);
				if (contactThumbnailImage == null)
				{
					return null;
				}
				return CreateThumbnail(contactThumbnailImage, 128, 128);
			}
		}

		private ImageCodecInfo GetEncoder(ImageFormat format)
		{
			ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
			foreach (ImageCodecInfo imageCodecInfo in imageDecoders)
			{
				if (imageCodecInfo.FormatID == format.Guid)
				{
					return imageCodecInfo;
				}
			}
			return null;
		}

		private byte[] CreateThumbnail(byte[] image, int width, int height)
		{
			if (image == null)
			{
				return null;
			}
			ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(image, 0, image.Length);
			Bitmap bitmap = new Bitmap(memoryStream);
			int num = width;
			int num2 = height;
			int width2 = bitmap.Width;
			int height2 = bitmap.Height;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			num4 = (float)num / (float)width2;
			num5 = (float)num2 / (float)height2;
			num3 = ((!(num5 < num4)) ? num4 : num5);
			checked
			{
				num = (int)((float)width2 * num3);
				num2 = (int)((float)height2 * num3);
				Image thumbnailImage = bitmap.GetThumbnailImage(num, num2, ThumbnailImageAbort, IntPtr.Zero);
				Encoder quality = Encoder.Quality;
				EncoderParameters encoderParameters = new EncoderParameters(1);
				EncoderParameter encoderParameter = new EncoderParameter(quality, 50L);
				encoderParameters.Param[0] = encoderParameter;
				memoryStream = new MemoryStream();
				thumbnailImage.Save(memoryStream, encoder, encoderParameters);
				return memoryStream.ToArray();
			}
		}

		private bool ThumbnailImageAbort()
		{
			return true;
		}
	}
}
