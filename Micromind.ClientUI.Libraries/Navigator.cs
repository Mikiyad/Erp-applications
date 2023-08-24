using Micromind.ClientUI.WindowsForms;
using System;
using System.Collections;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	public class Navigator
	{
		public Form HomeForm;

		private NavigationStack forwardStack;

		private NavigationStack backStack;

		private Form currentObject;

		private ArrayList popedFormList = new ArrayList();

		private static formMain main;

		internal static formMain MainForm => main;

		internal Form Current => currentObject;

		public bool HasHomeFormOnly
		{
			get
			{
				if (HomeForm == null)
				{
					return false;
				}
				if (checked(backStack.Count + forwardStack.Count) > 1)
				{
					return false;
				}
				if (forwardStack.Count == 1 && forwardStack.Peek().GetType() == HomeForm.GetType())
				{
					return true;
				}
				if (backStack.Count == 1 && backStack.Peek().GetType() == HomeForm.GetType())
				{
					return true;
				}
				return false;
			}
		}

		public int BackCount => backStack.Count;

		public int ForwardCount => forwardStack.Count;

		public event EventHandler OnEndBack;

		public event EventHandler OnEndForward;

		public event EventHandler OnPrevious;

		public Navigator(formMain mainForm)
		{
			if (mainForm == null)
			{
				throw new NullReferenceException("Main form cannot be null.");
			}
			forwardStack = new NavigationStack();
			backStack = new NavigationStack();
			main = mainForm;
		}

		internal static bool IsActiveChild(Form form)
		{
			return MainForm.IsActiveChild(form);
		}

		internal static void ApplyChildsPreferences()
		{
			MainForm.ApplyChildsPreferences();
		}

		internal static void GoHome()
		{
			MainForm.GoHome();
		}

		internal static Form GetActiveForm()
		{
			return MainForm.GetActiveChild();
		}

		internal static void GoHelp()
		{
			MainForm.GoHelp();
		}

		public ArrayList GetUnusedFormList()
		{
			return popedFormList;
		}

		public void Add(Form item)
		{
			Add(item, raiseEvents: true);
		}

		public void Add(Form item, bool raiseEvents)
		{
			if (currentObject != null && item != currentObject && !backStack.Contains(currentObject))
			{
				backStack.Push(currentObject);
				if (popedFormList.Contains(currentObject))
				{
					popedFormList.Remove(currentObject);
				}
			}
			if (item != null && !backStack.Contains(item))
			{
				backStack.Push(item);
				if (popedFormList.Contains(item))
				{
					popedFormList.Remove(item);
				}
			}
			while (forwardStack.Count > 0)
			{
				Form form = forwardStack.Pop() as Form;
				if (form != null && !popedFormList.Contains(form) && !forwardStack.Contains(form) && !backStack.Contains(form))
				{
					popedFormList.Add(form);
				}
			}
			currentObject = item;
			EndBackForward();
		}

		public void AddUnique(Form item)
		{
			Add(item, raiseEvents: true);
		}

		public void AddUnique(Form item, bool raiseEvents)
		{
			Add(item, raiseEvents);
		}

		public void GetHomeForm(out Form form)
		{
			form = null;
			if (HomeForm == null)
			{
				return;
			}
			int formIndex = backStack.GetFormIndex(HomeForm);
			if (formIndex > 0)
			{
				Back(out form, formIndex);
				return;
			}
			formIndex = forwardStack.GetFormIndex(HomeForm);
			if (formIndex > 0)
			{
				Forward(out form, formIndex);
			}
		}

		private void RemoveAll(bool raiseEvents)
		{
			Reset(raiseEvents);
		}

		private void RemoveAll()
		{
			Reset();
		}

		public void Reset()
		{
			Reset(raiseEvents: true);
		}

		public void Reset(bool raiseEvents)
		{
			forwardStack.Reset();
			backStack.Reset();
			int index = 0;
			while (popedFormList.Count > 0)
			{
				Form form = popedFormList[index] as Form;
				if (form != null)
				{
					form.Close();
					form = null;
				}
				popedFormList.RemoveAt(index);
			}
			forwardStack.Clear();
			backStack.Clear();
			currentObject = null;
			if (raiseEvents)
			{
				EndBackForward();
			}
		}

		public void ResetForwardPointer()
		{
			forwardStack.Clear();
			EndBackForward();
		}

		public void RemoveCurrent(Form form)
		{
			if (form == currentObject)
			{
				if (backStack.Count > 0 && backStack.Peek() == currentObject)
				{
					backStack.Pop();
				}
				else if (forwardStack.Count > 0 && forwardStack.Peek() == currentObject)
				{
					forwardStack.Pop();
				}
			}
		}

		public Form GetBack()
		{
			return backStack.GetNext(currentObject);
		}

		public int Back(out Form item)
		{
			return BackwardIt(out item, backStack.GetNextIndex(currentObject));
		}

		public int Back(out Form item, int n)
		{
			if (backStack.Contains(currentObject))
			{
				n = checked(n + 1);
			}
			return BackwardIt(out item, n);
		}

		private int BackwardIt(out Form item, int n)
		{
			item = MoveThroughHistory(checked(-n));
			EndBackForward();
			return backStack.Count;
		}

		public Form GetForward()
		{
			return forwardStack.GetNext(currentObject);
		}

		public int Forward(out Form item)
		{
			return ForwardIt(out item, forwardStack.GetNextIndex(currentObject));
		}

		public int Forward(out Form item, int n)
		{
			if (forwardStack.Contains(currentObject))
			{
				n = checked(n + 1);
			}
			return ForwardIt(out item, n);
		}

		private int ForwardIt(out Form item, int n)
		{
			item = MoveThroughHistory(n);
			EndBackForward();
			return forwardStack.Count;
		}

		private void EndBackForward()
		{
			EndBack();
			EndForward();
		}

		public void Previous()
		{
			if (this.OnPrevious != null)
			{
				this.OnPrevious(this, null);
			}
		}

		private void EndBack()
		{
			if (backStack.Count == 0)
			{
				if (this.OnEndBack != null)
				{
					this.OnEndBack(true, null);
				}
			}
			else if (backStack.Count == 1)
			{
				if (backStack.Peek() as Form == currentObject)
				{
					if (this.OnEndBack != null)
					{
						this.OnEndBack(true, null);
					}
				}
				else if (this.OnEndBack != null)
				{
					this.OnEndBack(false, null);
				}
			}
			else if (this.OnEndBack != null)
			{
				this.OnEndBack(false, null);
			}
		}

		private void EndForward()
		{
			if (forwardStack.Count == 0)
			{
				if (this.OnEndForward != null)
				{
					this.OnEndForward(true, null);
				}
			}
			else if (forwardStack.Count == 1)
			{
				if (forwardStack.Peek() as Form == currentObject)
				{
					if (this.OnEndForward != null)
					{
						this.OnEndForward(false, null);
					}
				}
				else if (this.OnEndForward != null)
				{
					this.OnEndForward(true, null);
				}
			}
			else if (this.OnEndForward != null)
			{
				this.OnEndForward(false, null);
			}
		}

		public ArrayList GetBackList()
		{
			return backStack.GetPageTitles(currentObject);
		}

		public ArrayList GetForwardList()
		{
			return forwardStack.GetPageTitles(currentObject);
		}

		public Form GetObject(string title)
		{
			Form form = null;
			form = GetForwardObject(title);
			if (form == null)
			{
				form = GetBackObject(title);
			}
			return form;
		}

		public Form GetBackObject(string title)
		{
			return backStack.GetFormByTitle(title);
		}

		public Form GetForwardObject(string title)
		{
			return forwardStack.GetFormByTitle(title);
		}

		private Form MoveThroughHistory(int n)
		{
			Form form = null;
			checked
			{
				NavigationStack navigationStack;
				NavigationStack navigationStack2;
				int num;
				if (n < 0)
				{
					navigationStack = backStack;
					navigationStack2 = forwardStack;
					num = -n;
				}
				else
				{
					if (n <= 0)
					{
						return form;
					}
					navigationStack = forwardStack;
					navigationStack2 = backStack;
					num = n;
				}
				_ = currentObject;
				bool flag = false;
				int num2 = 0;
				while (!flag && navigationStack.Count > 0)
				{
					form = (navigationStack.Pop() as Form);
					num2++;
					if (form != null)
					{
						navigationStack2.Push(form);
						if (num2 >= num)
						{
							flag = true;
						}
					}
				}
				if (form != null)
				{
					currentObject = form;
				}
				return form;
			}
		}
	}
}
