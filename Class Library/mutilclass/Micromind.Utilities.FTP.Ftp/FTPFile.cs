using System;
using System.Globalization;
using System.Text;

namespace Micromind.Utilities.FTP.Ftp
{
	public class FTPFile
	{
		public const int UNKNOWN = -1;

		public const int WINDOWS = 0;

		public const int UNIX = 1;

		private static readonly string format = "dd-MM-yyyy HH:mm";

		private int type;

		protected internal bool isLink;

		protected internal int linkCount = 1;

		protected internal string permissions;

		protected internal bool isDir;

		protected internal long size;

		protected internal string name;

		protected internal string linkedname;

		protected internal string owner;

		protected internal string group;

		protected internal DateTime lastModified;

		protected internal string raw;

		public virtual int Type => type;

		public virtual string Name => name;

		public virtual string Raw => raw;

		public virtual int LinkCount
		{
			get
			{
				return linkCount;
			}
			set
			{
				linkCount = value;
			}
		}

		public virtual bool Link
		{
			get
			{
				return isLink;
			}
			set
			{
				isLink = value;
			}
		}

		public virtual string LinkedName
		{
			get
			{
				return linkedname;
			}
			set
			{
				linkedname = value;
			}
		}

		public virtual string Group
		{
			get
			{
				return group;
			}
			set
			{
				group = value;
			}
		}

		public virtual string Owner
		{
			get
			{
				return owner;
			}
			set
			{
				owner = value;
			}
		}

		public virtual bool Dir
		{
			get
			{
				return isDir;
			}
			set
			{
				isDir = value;
			}
		}

		public virtual string Permissions
		{
			get
			{
				return permissions;
			}
			set
			{
				permissions = value;
			}
		}

		public virtual DateTime LastModified => lastModified;

		public virtual long Size => size;

		internal FTPFile(int type, string raw, string name, long size, bool isDir, ref DateTime lastModified)
		{
			this.type = type;
			this.raw = raw;
			this.name = name;
			this.size = size;
			this.isDir = isDir;
			this.lastModified = lastModified;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(raw);
			stringBuilder.Append("[").Append("Name=").Append(name)
				.Append(",")
				.Append("Size=")
				.Append(size)
				.Append(",")
				.Append("Permissions=")
				.Append(permissions)
				.Append(",")
				.Append("Owner=")
				.Append(owner)
				.Append(",")
				.Append("Group=")
				.Append(group)
				.Append(",")
				.Append("Is link=")
				.Append(isLink)
				.Append(",")
				.Append("Link count=")
				.Append(linkCount)
				.Append(",")
				.Append("Is dir=")
				.Append(isDir)
				.Append(",")
				.Append("Linked name=")
				.Append(linkedname)
				.Append(",")
				.Append("Permissions=")
				.Append(permissions)
				.Append(",")
				.Append("Last modified=")
				.Append(lastModified.ToString(format, CultureInfo.CurrentCulture.DateTimeFormat))
				.Append("]");
			return stringBuilder.ToString();
		}
	}
}
