using System;

namespace NineRays.Obfuscator
{
	public class SpecialNamespaceAttribute : Attribute
	{
		private string name;

		public string Name => name;

		public SpecialNamespaceAttribute(string Name)
		{
			name = Name;
		}
	}
}
