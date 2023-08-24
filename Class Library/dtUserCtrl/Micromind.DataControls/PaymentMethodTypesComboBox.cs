using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Micromind.Common.Data;
using System.ComponentModel;
using System.Drawing;

namespace Micromind.DataControls
{
	public class PaymentMethodTypesComboBox : UltraComboEditor
	{
		private bool hideCheque;

		private bool hideARAccount;

		private Container components;

		public bool HideCheque
		{
			get
			{
				return hideCheque;
			}
			set
			{
				hideCheque = value;
			}
		}

		public bool HideARAccount
		{
			get
			{
				return hideARAccount;
			}
			set
			{
				hideARAccount = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public PaymentMethodTypes SelectedType
		{
			get
			{
				if (base.SelectedItem.ToString() == "Cheque")
				{
					return PaymentMethodTypes.Check;
				}
				if (base.SelectedItem.ToString() == "Credit Card")
				{
					return PaymentMethodTypes.CreditCard;
				}
				if (base.SelectedItem.ToString() == "Transfer")
				{
					return PaymentMethodTypes.Transfer;
				}
				if (base.SelectedItem.ToString() == "Accounts Receivable")
				{
					return PaymentMethodTypes.AccountReceivable;
				}
				return PaymentMethodTypes.Cash;
			}
			set
			{
				switch (value)
				{
				case PaymentMethodTypes.Check:
					Value = "Cheque";
					break;
				case PaymentMethodTypes.CreditCard:
					Value = "Credit Card";
					break;
				case PaymentMethodTypes.Transfer:
					Value = "Transfer";
					break;
				case PaymentMethodTypes.AccountReceivable:
					Value = "Accounts Receivable";
					break;
				default:
					Value = "Cash";
					break;
				}
			}
		}

		public PaymentMethodTypesComboBox()
		{
			InitializeComponent();
			DropDownStyle = DropDownStyle.DropDownList;
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
			SuspendLayout();
			base.Name = "PaymentMethodTypesComboBox";
			base.Size = new System.Drawing.Size(192, 20);
			ResumeLayout(false);
		}

		public void LoadData()
		{
			Items.Clear();
			Items.Add("Cash");
			if (!hideCheque)
			{
				Items.Add("Cheque");
			}
			Items.Add("Credit Card");
			Items.Add("Transfer");
			if (!hideARAccount)
			{
				Items.Add("Accounts Receivable");
			}
		}
	}
}
