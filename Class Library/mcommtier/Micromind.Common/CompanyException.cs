using System;
using System.Runtime.Serialization;

namespace Micromind.Common
{
	[Serializable]
	public class CompanyException : Exception
	{
		public int exceptionNumber = -1;

		public int Number
		{
			get
			{
				return exceptionNumber;
			}
			set
			{
				exceptionNumber = value;
			}
		}

		public CompanyException()
		{
		}

		public CompanyException(string message, int number)
			: base(message)
		{
			Number = number;
		}

		public CompanyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			exceptionNumber = info.GetInt32("Number");
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Number", exceptionNumber, typeof(int));
			base.GetObjectData(info, context);
		}

		public CompanyException(string message)
			: base(message)
		{
		}
	}
}
