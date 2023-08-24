using Micromind.Common.Data;
using Micromind.DataCaches;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace Micromind.DataControls
{
	public class paymentMethodsComboBox : MultiColumnComboBox
	{
		private bool showLCMethods;

		private Container components;

		public PaymentMethodTypes SelectedPaymentType
		{
			get
			{
				if (base.SelectedRow != null)
				{
					int result = -1;
					int.TryParse(base.SelectedRow.Cells["MethodType"].Value.ToString(), out result);
					if (result > 0)
					{
						return (PaymentMethodTypes)result;
					}
					return PaymentMethodTypes.Other;
				}
				return PaymentMethodTypes.Other;
			}
		}

		public paymentMethodsComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.PaymentMethod;
			TABLENAME_FIELD = "Payment_Method";
			ID_FIELD = "PaymentMethodID";
			NAME_FIELD = "MethodName";
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			base.Editable = false;
			base.Name = "paymentMethodsComboBox";
			base.Size = new System.Drawing.Size(216, 20);
			ResumeLayout(false);
		}

		protected DataSet GetData(bool isReferesh)
		{
			return CombosData.GetPaymentMethodList(isReferesh);
		}

		public override void LoadData()
		{
			LoadData(isReferesh: false);
		}

		public void LoadData(bool isReferesh)
		{
			FillData(GetData(isReferesh));
			base.IsDataLoaded = true;
		}
	}
}
