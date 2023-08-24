using System;
using System.Collections;
using System.IO;

namespace Micromind.DataControls.QueryBuilder
{
	public class ArrayListEx : ArrayList
	{
		protected class CompareFiledate : IComparer
		{
			public int Compare(object o_File1, object o_File2)
			{
				DateTime lastWriteTime = File.GetLastWriteTime(o_File1.ToString());
				DateTime lastWriteTime2 = File.GetLastWriteTime(o_File2.ToString());
				return lastWriteTime.CompareTo(lastWriteTime2);
			}
		}

		public override int Add(object value)
		{
			if (!(value is string))
			{
				throw new Exception("ArrayListNoCase supports only strings!");
			}
			return base.Add(value);
		}

		public int AddOnce(object value)
		{
			int num = IndexOf(value);
			if (num >= 0)
			{
				return num;
			}
			return Add(value);
		}

		public override void AddRange(ICollection i_Coll)
		{
			foreach (object item in i_Coll)
			{
				Add(item);
			}
		}

		public void AddRangeOnce(ICollection i_Coll)
		{
			foreach (object item in i_Coll)
			{
				AddOnce(item);
			}
		}

		public override void Insert(int index, object value)
		{
			if (!(value is string))
			{
				throw new Exception("ArrayListNoCase supports only strings!");
			}
			base.Insert(index, value);
		}

		public override void InsertRange(int index, ICollection c)
		{
			throw new Exception("Function not supported");
		}

		public override bool Contains(object o_Item)
		{
			return IndexOf(o_Item) >= 0;
		}

		public override int IndexOf(object value)
		{
			for (int i = 0; i < Count; i++)
			{
				if (string.Compare(this[i].ToString(), value.ToString(), ignoreCase: true) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		public override int IndexOf(object value, int startIndex)
		{
			throw new Exception("Function not supported");
		}

		public override int IndexOf(object value, int startIndex, int count)
		{
			throw new Exception("Function not supported");
		}

		public override int LastIndexOf(object value)
		{
			for (int num = Count - 1; num >= 0; num--)
			{
				if (string.Compare(this[num].ToString(), value.ToString(), ignoreCase: true) == 0)
				{
					return num;
				}
			}
			return -1;
		}

		public override int LastIndexOf(object value, int startIndex)
		{
			throw new Exception("Function not supported");
		}

		public override int LastIndexOf(object value, int startIndex, int count)
		{
			throw new Exception("Function not supported");
		}

		public string ToList(string s_Separator)
		{
			string text = "";
			for (int i = 0; i < Count; i++)
			{
				if (i > 0)
				{
					text += s_Separator;
				}
				text += (string)this[i];
			}
			return text;
		}

		public void FromList(string s_Values, string s_Separator)
		{
			Clear();
			string[] array = Functions.SplitEx(s_Values, s_Separator);
			foreach (string value in array)
			{
				AddOnce(value);
			}
		}

		public void SortByFileDate()
		{
			Sort(new CompareFiledate());
		}
	}
}
