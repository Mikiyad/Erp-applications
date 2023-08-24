using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms.Others.HelpSupports;
using System.Windows.Forms;

namespace Micromind.ClientUI
{
	public sealed class UIMessages
	{
		public static string EnterRequiredFields => GetMessage(0);

		public static string EnterChequeNumber => GetMessage(1);

		public static string DocumentNumberInUse => GetMessage(2);

		public static string TransactionWithZeroAmount => GetMessage(3);

		public static string DeleteRecord => GetMessage(4);

		public static string DocumentNotFound => GetMessage(5);

		public static string DoYouWantToSave => GetMessage(6);

		public static string Void => GetMessage(7);

		public static string Unvoid => GetMessage(8);

		public static string WantToVoid => GetMessage(9);

		public static string WantToUnvoid => GetMessage(10);

		public static string UnableToSave => GetMessage(11);

		public static string ClearButtonText => GetMessage(12);

		public static string NewButtonText => GetMessage(13);

		public static string SelectAccount => GetMessage(14);

		public static string AnalysisNotAdded => GetMessage(15);

		public static string InvalidAmount => GetMessage(16);

		public static string TransactionNotBalance => GetMessage(17);

		public static string ChequeNotRegistered => GetMessage(18);

		public static string ChequeInUseNotBlank => GetMessage(19);

		public static string InvalidChequeNumber => GetMessage(20);

		public static string EnterAtLeastOneRowOfPayment => GetMessage(21);

		public static string EnterChequeAmount => GetMessage(22);

		public static string EnterChequeDate => GetMessage(23);

		public static string EnterChequeBank => GetMessage(24);

		public static string SelectAChequebook => GetMessage(25);

		public static string ChequeNumberExist => GetMessage(26);

		public static string DepositChequeWithDifferentCurrency => GetMessage(27);

		public static string SelectAtLeastOneChequeToDeposit => GetMessage(28);

		public static string ReturnedChequeError => GetMessage(29);

		public static string CancelChequeError => GetMessage(30);

		public static string ChequeAlreadyCancelled => GetMessage(31);

		public static string SelectAnItemFirst => GetMessage(32);

		public static string NoPermissionNew => GetMessage(33);

		public static string NoPermissionEdit => GetMessage(34);

		public static string NoPermissionView => GetMessage(35);

		public static string NoPermissionDelete => GetMessage(36);

		public static string RecordDeleteInUse => GetMessage(37);

		public static string PayeeMustHaveAccount => GetMessage(38);

		public static string SelectAtLeastOneChequeToClear => GetMessage(39);

		public static string DocumentNotApproved => GetMessage(41);

		private UIMessages()
		{
		}

		public static ErrorHelperDialogResult WarningMessage(Form owner, string message, MessageBoxButtons buttons)
		{
			using (WarningMessageDialog warningMessageDialog = new WarningMessageDialog())
			{
				return warningMessageDialog.ShowDialog(owner, message, buttons);
			}
		}

		public static string GetMessage(int messageIndex)
		{
			string message = Messages.GetMessage(messageIndex);
			string text = Translator.Translators.GetText(message);
			if (text == string.Empty || text == null)
			{
				return message;
			}
			return text;
		}
	}
}
