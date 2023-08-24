using System;

namespace NineRays.Obfuscator
{
	public class SpecialNameAttribute : Attribute
	{
		private string name;

		public string Name => name;

		public SpecialNameAttribute(string Name)
		{
			name = Name;
		}
	}
}
