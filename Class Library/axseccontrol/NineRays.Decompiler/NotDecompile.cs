using System;

namespace NineRays.Decompiler
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
	public class NotDecompile : Attribute
	{
	}
}
