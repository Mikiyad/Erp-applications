using System;

namespace Micromind.Utilities
{
	public interface IStatusForm
	{
		event EventHandler CanceledByUser;
	}
}
