using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Micromind.Utilities.MAPI
{
	public class Mapi
	{
		private const int MapiLogonUI = 1;

		private const int MapiPasswordUI = 131072;

		private const int MapiNewSession = 2;

		private const int MapiForceDownload = 4096;

		private const int MapiExtendedUI = 32;

		private IntPtr session = IntPtr.Zero;

		private IntPtr winhandle = IntPtr.Zero;

		private const int MapiORIG = 0;

		private const int MapiTO = 1;

		private const int MapiCC = 2;

		private const int MapiBCC = 3;

		private MapiRecipDesc origin = new MapiRecipDesc();

		private ArrayList recpts = new ArrayList();

		private ArrayList attachs = new ArrayList();

		private const int MapiUnreadOnly = 32;

		private const int MapiGuaranteeFiFo = 256;

		private const int MapiLongMsgID = 16384;

		private StringBuilder lastMsgID = new StringBuilder(600);

		private string findseed;

		private const int MapiPeek = 128;

		private const int MapiSuprAttach = 2048;

		private const int MapiEnvOnly = 64;

		private const int MapiBodyAsFile = 512;

		private const int MapiUnread = 1;

		private const int MapiReceiptReq = 2;

		private const int MapiSent = 4;

		private MapiMessage lastMsg;

		private int error;

		private readonly string[] errors = new string[27]
		{
			"OK [0]",
			"User abort [1]",
			"General MAPI failure [2]",
			"MAPI login failure [3]",
			"Disk full [4]",
			"Insufficient memory [5]",
			"Access denied [6]",
			"-unknown- [7]",
			"Too many sessions [8]",
			"Too many files were specified [9]",
			"Too many recipients were specified [10]",
			"A specified attachment was not found [11]",
			"Attachment open failure [12]",
			"Attachment write failure [13]",
			"Unknown recipient [14]",
			"Bad recipient type [15]",
			"No messages [16]",
			"Invalid message [17]",
			"Text too large [18]",
			"Invalid session [19]",
			"Type not supported [20]",
			"A recipient was specified ambiguously [21]",
			"Message in use [22]",
			"Network failure [23]",
			"Invalid edit fields [24]",
			"Invalid recipients [25]",
			"Not supported [26]"
		};

		public bool Logon(IntPtr hwnd)
		{
			winhandle = hwnd;
			error = MAPILogon(hwnd, null, null, 0, 0, ref session);
			if (error != 0)
			{
				error = MAPILogon(hwnd, null, null, 1, 0, ref session);
			}
			return error == 0;
		}

		public void Reset()
		{
			findseed = null;
			origin = new MapiRecipDesc();
			recpts.Clear();
			attachs.Clear();
			lastMsg = null;
		}

		public void Logoff()
		{
			if (session != IntPtr.Zero)
			{
				error = MAPILogoff(session, winhandle, 0, 0);
				session = IntPtr.Zero;
			}
		}

		[DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
		private static extern int MAPILogon(IntPtr hwnd, string prf, string pw, int flg, int rsv, ref IntPtr sess);

		[DllImport("MAPI32.DLL")]
		private static extern int MAPILogoff(IntPtr sess, IntPtr hwnd, int flg, int rsv);

		public bool Send(string sub, string txt)
		{
			lastMsg = new MapiMessage();
			lastMsg.subject = sub;
			lastMsg.noteText = txt;
			lastMsg.originator = AllocOrigin();
			lastMsg.recips = AllocRecips(out lastMsg.recipCount);
			lastMsg.files = AllocAttachs(out lastMsg.fileCount);
			error = MAPISendMail(session, winhandle, lastMsg, 0, 0);
			Dealloc();
			Reset();
			return error == 0;
		}

		public void AddRecip(string name, string addr, bool cc)
		{
			MapiRecipDesc mapiRecipDesc = new MapiRecipDesc();
			if (cc)
			{
				mapiRecipDesc.recipClass = 2;
			}
			else
			{
				mapiRecipDesc.recipClass = 1;
			}
			mapiRecipDesc.name = name;
			mapiRecipDesc.address = addr;
			recpts.Add(mapiRecipDesc);
		}

		public void SetSender(string sname, string saddr)
		{
			origin.name = sname;
			origin.address = saddr;
		}

		public void Attach(string filepath)
		{
			attachs.Add(filepath);
		}

		private IntPtr AllocOrigin()
		{
			origin.recipClass = 0;
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MapiRecipDesc)));
			Marshal.StructureToPtr(origin, intPtr, fDeleteOld: false);
			return intPtr;
		}

		private IntPtr AllocRecips(out int recipCount)
		{
			recipCount = 0;
			if (recpts.Count == 0)
			{
				return IntPtr.Zero;
			}
			int num = Marshal.SizeOf(typeof(MapiRecipDesc));
			IntPtr intPtr = Marshal.AllocHGlobal(recpts.Count * num);
			int num2 = (int)intPtr;
			for (int i = 0; i < recpts.Count; i++)
			{
				Marshal.StructureToPtr(recpts[i] as MapiRecipDesc, (IntPtr)num2, fDeleteOld: false);
				num2 += num;
			}
			recipCount = recpts.Count;
			return intPtr;
		}

		private IntPtr AllocAttachs(out int fileCount)
		{
			fileCount = 0;
			if (attachs == null)
			{
				return IntPtr.Zero;
			}
			if (attachs.Count <= 0 || attachs.Count > 100)
			{
				return IntPtr.Zero;
			}
			int num = Marshal.SizeOf(typeof(MapiFileDesc));
			IntPtr intPtr = Marshal.AllocHGlobal(attachs.Count * num);
			MapiFileDesc mapiFileDesc = new MapiFileDesc();
			mapiFileDesc.position = -1;
			int num2 = (int)intPtr;
			for (int i = 0; i < attachs.Count; i++)
			{
				string path = attachs[i] as string;
				mapiFileDesc.name = Path.GetFileName(path);
				mapiFileDesc.path = path;
				Marshal.StructureToPtr(mapiFileDesc, (IntPtr)num2, fDeleteOld: false);
				num2 += num;
			}
			fileCount = attachs.Count;
			return intPtr;
		}

		private void Dealloc()
		{
			Type typeFromHandle = typeof(MapiRecipDesc);
			int num = Marshal.SizeOf(typeFromHandle);
			if (lastMsg.originator != IntPtr.Zero)
			{
				Marshal.DestroyStructure(lastMsg.originator, typeFromHandle);
				Marshal.FreeHGlobal(lastMsg.originator);
			}
			if (lastMsg.recips != IntPtr.Zero)
			{
				int num2 = (int)lastMsg.recips;
				for (int i = 0; i < lastMsg.recipCount; i++)
				{
					Marshal.DestroyStructure((IntPtr)num2, typeFromHandle);
					num2 += num;
				}
				Marshal.FreeHGlobal(lastMsg.recips);
			}
			if (lastMsg.files != IntPtr.Zero)
			{
				Type typeFromHandle2 = typeof(MapiFileDesc);
				int num3 = Marshal.SizeOf(typeFromHandle2);
				int num4 = (int)lastMsg.files;
				for (int j = 0; j < lastMsg.fileCount; j++)
				{
					Marshal.DestroyStructure((IntPtr)num4, typeFromHandle2);
					num4 += num3;
				}
				Marshal.FreeHGlobal(lastMsg.files);
			}
		}

		[DllImport("MAPI32.DLL")]
		private static extern int MAPISendMail(IntPtr sess, IntPtr hwnd, MapiMessage message, int flg, int rsv);

		public bool Next(ref MailEnvelop env)
		{
			error = MAPIFindNext(session, winhandle, null, findseed, 16384, 0, lastMsgID);
			if (error != 0)
			{
				return false;
			}
			findseed = lastMsgID.ToString();
			IntPtr ptrmsg = IntPtr.Zero;
			error = MAPIReadMail(session, winhandle, findseed, 2240, 0, ref ptrmsg);
			if (error != 0 || ptrmsg == IntPtr.Zero)
			{
				return false;
			}
			lastMsg = new MapiMessage();
			Marshal.PtrToStructure(ptrmsg, lastMsg);
			MapiRecipDesc mapiRecipDesc = new MapiRecipDesc();
			if (lastMsg.originator != IntPtr.Zero)
			{
				Marshal.PtrToStructure(lastMsg.originator, mapiRecipDesc);
			}
			env.id = findseed;
			env.date = DateTime.ParseExact(lastMsg.dateReceived, "yyyy/MM/dd HH:mm", DateTimeFormatInfo.InvariantInfo);
			env.subject = lastMsg.subject;
			env.from = mapiRecipDesc.name;
			env.unread = ((lastMsg.flags & 1) != 0);
			env.atts = lastMsg.fileCount;
			error = MAPIFreeBuffer(ptrmsg);
			return error == 0;
		}

		[DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
		private static extern int MAPIFindNext(IntPtr sess, IntPtr hwnd, string typ, string seed, int flg, int rsv, StringBuilder id);

		public string Read(string id, out MailAttach[] aat)
		{
			aat = null;
			IntPtr ptrmsg = IntPtr.Zero;
			error = MAPIReadMail(session, winhandle, id, 2176, 0, ref ptrmsg);
			if (error != 0 || ptrmsg == IntPtr.Zero)
			{
				return null;
			}
			lastMsg = new MapiMessage();
			Marshal.PtrToStructure(ptrmsg, lastMsg);
			if (lastMsg.fileCount > 0 && lastMsg.fileCount < 100 && lastMsg.files != IntPtr.Zero)
			{
				GetAttachNames(out aat);
			}
			MAPIFreeBuffer(ptrmsg);
			return lastMsg.noteText;
		}

		public bool Delete(string id)
		{
			error = MAPIDeleteMail(session, winhandle, id, 0, 0);
			return error == 0;
		}

		public bool SaveAttachm(string id, string name, string savepath)
		{
			IntPtr ptrmsg = IntPtr.Zero;
			error = MAPIReadMail(session, winhandle, id, 128, 0, ref ptrmsg);
			if (error != 0 || ptrmsg == IntPtr.Zero)
			{
				return false;
			}
			lastMsg = new MapiMessage();
			Marshal.PtrToStructure(ptrmsg, lastMsg);
			bool result = false;
			if (lastMsg.fileCount > 0 && lastMsg.fileCount < 100 && lastMsg.files != IntPtr.Zero)
			{
				result = SaveAttachByName(name, savepath);
			}
			MAPIFreeBuffer(ptrmsg);
			return result;
		}

		private void GetAttachNames(out MailAttach[] aat)
		{
			aat = new MailAttach[lastMsg.fileCount];
			int num = Marshal.SizeOf(typeof(MapiFileDesc));
			MapiFileDesc mapiFileDesc = new MapiFileDesc();
			int num2 = (int)lastMsg.files;
			for (int i = 0; i < lastMsg.fileCount; i++)
			{
				Marshal.PtrToStructure((IntPtr)num2, mapiFileDesc);
				num2 += num;
				aat[i] = new MailAttach();
				if (mapiFileDesc.flags == 0)
				{
					aat[i].position = mapiFileDesc.position;
					aat[i].name = mapiFileDesc.name;
					aat[i].path = mapiFileDesc.path;
				}
			}
		}

		private bool SaveAttachByName(string name, string savepath)
		{
			bool result = true;
			int num = Marshal.SizeOf(typeof(MapiFileDesc));
			MapiFileDesc mapiFileDesc = new MapiFileDesc();
			int num2 = (int)lastMsg.files;
			for (int i = 0; i < lastMsg.fileCount; i++)
			{
				Marshal.PtrToStructure((IntPtr)num2, mapiFileDesc);
				num2 += num;
				if (mapiFileDesc.flags == 0 && mapiFileDesc.name != null)
				{
					try
					{
						if (name == mapiFileDesc.name)
						{
							if (File.Exists(savepath))
							{
								File.Delete(savepath);
							}
							File.Move(mapiFileDesc.path, savepath);
						}
					}
					catch (Exception)
					{
						result = false;
						error = 13;
					}
					try
					{
						File.Delete(mapiFileDesc.path);
					}
					catch (Exception)
					{
					}
				}
			}
			return result;
		}

		[DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
		private static extern int MAPIReadMail(IntPtr sess, IntPtr hwnd, string id, int flg, int rsv, ref IntPtr ptrmsg);

		[DllImport("MAPI32.DLL")]
		private static extern int MAPIFreeBuffer(IntPtr ptr);

		[DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
		private static extern int MAPIDeleteMail(IntPtr sess, IntPtr hwnd, string id, int flg, int rsv);

		public bool SingleAddress(string label, out string name, out string addr)
		{
			name = null;
			addr = null;
			int newrec = 0;
			IntPtr ptrnew = IntPtr.Zero;
			error = MAPIAddress(session, winhandle, null, 1, label, 0, IntPtr.Zero, 0, 0, ref newrec, ref ptrnew);
			if (error != 0 || newrec < 1 || ptrnew == IntPtr.Zero)
			{
				return false;
			}
			MapiRecipDesc mapiRecipDesc = new MapiRecipDesc();
			Marshal.PtrToStructure(ptrnew, mapiRecipDesc);
			name = mapiRecipDesc.name;
			addr = mapiRecipDesc.address;
			MAPIFreeBuffer(ptrnew);
			return true;
		}

		[DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
		private static extern int MAPIAddress(IntPtr sess, IntPtr hwnd, string caption, int editfld, string labels, int recipcount, IntPtr ptrrecips, int flg, int rsv, ref int newrec, ref IntPtr ptrnew);

		public string Error()
		{
			if (error <= 26)
			{
				return errors[error];
			}
			return "?unknown? [" + error.ToString() + "]";
		}
	}
}
