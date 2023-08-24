using Micromind.ClientLibraries;
using Micromind.ClientUI.SoftReg;
using Micromind.Common.Data;
using System;
using System.Data;
using System.Text;

namespace Micromind.ClientUI.Libraries
{
	public class WebHelper
	{
		public string CreateCLToken(string systemKey, string userID, string pass)
		{
			checked
			{
				try
				{
					string s = systemKey + userID + pass;
					byte[] bytes = Encoding.ASCII.GetBytes(s);
					s = "";
					for (int i = 0; i < bytes.Length; i += 3)
					{
						s += bytes[i].ToString();
					}
					return s.Substring(0, 6) + s.Substring(s.Length - 2, 1);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return "";
				}
			}
		}

		public bool RequestCreditLimit(string systemKey, string customerID, string customerName, decimal amount, decimal currentLimit, decimal balance)
		{
			try
			{
				bool flag = true;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Company", "CompanyUID", "CompanyID", 1);
				int num = 1;
				if (!fieldValue.IsDBNullOrEmpty())
				{
					num = int.Parse(fieldValue.ToString());
				}
				CLTokenData cLTokenData = new CLTokenData();
				DataRow dataRow = cLTokenData.CLTokenTable.NewRow();
				dataRow["SystemKey"] = systemKey;
				dataRow["amount"] = amount;
				dataRow["CustomerID"] = customerID;
				cLTokenData.CLTokenTable.Rows.Add(dataRow);
				flag = Factory.CLTokenSystem.RequestCLToken(cLTokenData);
				if (flag)
				{
					string text = "";
					DataSet cLUserList = Factory.UserSystem.GetCLUserList();
					string text2 = "";
					foreach (DataRow row in cLUserList.Tables[0].Rows)
					{
						if (text2 != "")
						{
							text2 += ",";
						}
						text2 += row["Email"].ToString();
						text = text + row["UserID"].ToString() + Factory.Decrypt(row["CLUserPass"].ToString());
					}
					Micromind.ClientUI.SoftReg.SoftReg softReg = new Micromind.ClientUI.SoftReg.SoftReg();
					flag &= softReg.RequestCL(1, systemKey, customerID, customerName, amount, Global.CurrentUser, currentLimit, balance, text);
					if (!flag)
					{
						return false;
					}
					if (text2 != "")
					{
						MailHelper mailHelper = new MailHelper(CompanyEmailConfigTypes.Accounting);
						string body = "<html><p>\r\n                        You have received a credit limit approval \r\n                        request.<o:p></o:p></span></p>\r\n                        <p >\r\n                        Customer: " + customerID + " | " + customerName + "<o:p></o:p></span></p>\r\n                        <p>\r\n                        Invoice Amount: " + amount.ToString(Format.TotalAmountFormat) + "<o:p></o:p></span></p>\r\n                        <p>\r\n                        Current Limit: " + currentLimit.ToString(Format.TotalAmountFormat) + "\r\n                        </span></p>\r\n                        <p>\r\n                        Current Balance: " + balance.ToString(Format.TotalAmountFormat) + "<o:p></o:p></span></p>\r\n                        <p>\r\n                        <a href=\"http://www.starasoft.com/softregws/CLApprovalForm.aspx?CID=" + num + "&SID=" + systemKey + "\">\r\n                        Please click here to approve or reject the request</a><o:p></o:p></span></p></html>";
						flag &= mailHelper.SendMail(text2, "Credit Limit Approval Request", body, isBodyHtml: true);
					}
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}
	}
}
