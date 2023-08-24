using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PaymentMethodSystem : MarshalByRefObject, IPaymentMethodSystem
	{
		private Config config;

		public PaymentMethodSystem(Config config)
		{
			this.config = config;
		}

		public bool CreatePaymentMethod(PaymentMethodData paymentMethodData)
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.InsertPaymentMethod(paymentMethodData);
			}
		}

		public bool UpdatePaymentMethod(PaymentMethodData paymentMethodData)
		{
			return new PaymentMethods(config).UpdatePaymentMethod(paymentMethodData);
		}

		public PaymentMethodData GetPaymentMethods()
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.GetPaymentMethods();
			}
		}

		public bool DeletePaymentMethod(string paymentMethodID)
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.DeletePaymentMethod(paymentMethodID);
			}
		}

		public DataSet GetPaymentMethodsByFields(params string[] columns)
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.GetPaymentMethodsByFields(columns);
			}
		}

		public DataSet GetPaymentMethodsByFields(int[] paymentMethodID, params string[] columns)
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.GetPaymentMethodsByFields(paymentMethodID, columns);
			}
		}

		public DataSet GetPaymentMethodsByFields(int[] paymentMethodID, bool isInactive, params string[] columns)
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.GetPaymentMethodsByFields(paymentMethodID, isInactive, columns);
			}
		}

		public PaymentMethodData GetPaymentMethodByID(string paymentMethodID)
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.GetPaymentMethodByID(paymentMethodID);
			}
		}

		public bool ExistPaymentMethod(string shortName)
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.ExistPaymentMethod(shortName);
			}
		}

		public DataSet GetPaymentMethodsComboList()
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.GetPaymentMethodsComboList();
			}
		}

		public DataSet GetPaymentMethodsList()
		{
			using (PaymentMethods paymentMethods = new PaymentMethods(config))
			{
				return paymentMethods.GetPaymentMethodsList();
			}
		}
	}
}
