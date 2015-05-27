using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Log;

namespace YueRen.Common.Util
{
    /// <summary>
    ///  关于本类的说明
    /// </summary>
    public class Mail
    {
        public Mail() { }

        public static bool Send(string body, string title, string singleEmailAddr)
        {
            string[] mailList = new string[] { singleEmailAddr };
            bool sendOk = false;
            int count = 0;
            while (!sendOk)
            {

                sendOk = SendMailList(body, title, mailList);

                if (count >= 10)
                    break;

                count++;
            }
            return sendOk;
        }

        public static bool SendMailList(string body, string title, string[] emailAddr)
        {

            //if (Configuration.AdminEmail == null ||
            //    Configuration.AdminEmail.Trim().Length < 1)
            //    return;
            //if (Configuration.SmtpServer == null ||
            //    Configuration.SmtpServer.Trim().Length == 0)
            //    return;

            SmtpMail smtp = new SmtpMail();
            smtp.MailDomain = "61.129.44.251";    //218.30.78.29这台smtp 要下架
            smtp.MailServerUserName = "";
            smtp.MailServerPassWord = "";
            smtp.From = "favbook@cmfu.com";

            //smtp.MailDomain = "mail.shanda.com.cn";
            //smtp.MailServerUserName = "esales";
            //smtp.MailServerPassWord = "1472583690";
            // smtp.From = "esales@shanda.com.cn";
            smtp.Html = true;


            smtp.FromName = "起点中文网";
            smtp.AddRecipient(emailAddr);
            smtp.Body = body;

            smtp.Html = true;

            smtp.Subject = title;
            try
            {

                return smtp.Send();
            }
            catch (Exception e)
            {
                LogHelper.Error(e.Message);
                // ExceptionManager.Publish( e );
            }

            return false;
        }

    }
}


