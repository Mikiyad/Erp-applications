using System;
using System.Collections;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	internal class NavigationStack : Stack
	{
		public ArrayList GetPageTitles(Form exceptionForm)
		{
			ArrayList arrayList = new ArrayList();
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Form form = (Form)enumerator.Current;
					if (form != exceptionForm && form != null && !form.IsDisposed)
					{
						arrayList.Add(form.Text);
					}
				}
				return arrayList;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		public Form GetFormByTitle(string title)
		{
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Form form = (Form)enumerator.Current;
					if (form != null && !form.IsDisposed && form.Text == title)
					{
						return form;
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return null;
		}

		public void Reset()
		{
			while (Count > 0)
			{
				Form form = Pop() as Form;
				if (form != null && !form.IsDisposed)
				{
					form.Close();
					form = null;
				}
			}
		}

		public Form GetNext(Form exceptionForm)
		{
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Form form = (Form)enumerator.Current;
					if (form != exceptionForm && form != null && !form.IsDisposed)
					{
						return form;
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return null;
		}

		public int GetFormIndex(Form form)
		{
			int num = 0;
			if (!Contains(form))
			{
				return num;
			}
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Form form2 = (Form)enumerator.Current;
					num = checked(num + 1);
					if (form2 == form && form2 != null && !form2.IsDisposed)
					{
						return num;
					}
				}
				return num;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		public int GetNextIndex(Form exceptionForm)
		{
			int num = 0;
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Form form = (Form)enumerator.Current;
					num = checked(num + 1);
					if (form != exceptionForm && form != null && !form.IsDisposed)
					{
						return num;
					}
				}
				return num;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}
}
