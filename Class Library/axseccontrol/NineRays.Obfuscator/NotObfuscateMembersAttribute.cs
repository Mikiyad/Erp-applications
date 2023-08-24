using System;

namespace NineRays.Obfuscator
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class NotObfuscateMembersAttribute : Attribute
	{
		private string pattern = string.Empty;

		public string Pattern => pattern;

		public NotObfuscateMembersAttribute(string pattern)
		{
			this.pattern = pattern;
		}
	}
}
