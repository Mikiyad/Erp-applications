using System;
using System.Collections;
using System.Reflection;

namespace Micromind.DataControls.QueryBuilder
{
	public class StackEx : ArrayList
	{
		private int ms32_Rotate = -1;

		public object Rotate
		{
			get
			{
				if (ms32_Rotate < 0)
				{
					ms32_Rotate = Count - 1;
				}
				if (ms32_Rotate < 0)
				{
					return null;
				}
				return this[ms32_Rotate--];
			}
		}

		public void PushUnique(object o_Value)
		{
			Remove(o_Value);
			Push(o_Value);
		}

		public void Push(object o_Value)
		{
			Add(o_Value);
			ms32_Rotate = Count - 1;
		}

		public object Pop()
		{
			if (Count == 0)
			{
				return null;
			}
			object result = this[Count - 1];
			RemoveAt(Count - 1);
			return result;
		}

		public object Peek()
		{
			if (Count == 0)
			{
				return null;
			}
			return this[Count - 1];
		}

		public string Serialize(char c_Delimiter, int MaxCount)
		{
			string text = "";
			int num = Count - 1;
			int num2 = Math.Max(0, num - MaxCount);
			for (int i = num2; i <= num; i++)
			{
				Serializable serializable = (Serializable)this[i];
				if (i > num2)
				{
					text += c_Delimiter.ToString();
				}
				text += serializable.Serialize();
			}
			return text;
		}

		public void DeSerialize(string s_Serial, Type t_ValueType, char c_Delimiter)
		{
			Clear();
			if (s_Serial.Length != 0)
			{
				Type[] types = new Type[0];
				ConstructorInfo constructor = t_ValueType.GetConstructor(types);
				string[] array = s_Serial.Split(c_Delimiter);
				foreach (string s_Serial2 in array)
				{
					Serializable serializable = (Serializable)constructor.Invoke(null);
					serializable.Deserialize(s_Serial2);
					Push(serializable);
				}
			}
		}
	}
}
