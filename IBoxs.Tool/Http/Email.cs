using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Tool.Http
{
    class Email
    {
        /// <summary>
        /// 数据组合为邮箱配置信息
        /// </summary>
        /// <param name="fromEmail"></param>
        /// <param name="pwd"></param>
        /// <param name="smtp"></param>
        /// <param name="port"></param>
        /// <param name="ssl"></param>
        /// <param name="deplayName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetMailConfig(string fromEmail, string pwd, string smtp, int port, bool ssl,string deplayName=null)
        {
            GC.Collect();
            Dictionary<string, string> MailConfig = new Dictionary<string, string>();
            MailConfig.Add("from", fromEmail);
            MailConfig.Add("pwd", pwd);
            MailConfig.Add("smtp", smtp);
            MailConfig.Add("port", port.ToString());
            if (ssl)
                MailConfig.Add("ssl", "1");
            else
                MailConfig.Add("ssl", "0");
            if (deplayName == null)
            {
                MailConfig.Add("deplayName",fromEmail.Split('@')[0]);
            }
            else
                MailConfig.Add("deplayName", deplayName);
            return MailConfig;
        }

        /// <summary>
        /// 系统发送邮件
        /// </summary>
        /// <param name="MailConfig">邮件配置</param>
        /// <param name="subject">主题</param>
        /// <param name="text">内容</param>
        /// <param name="email">收件人</param>
        /// <param name="beac">错误原因</param>
        /// <returns></returns>
        public static bool SendMailRun(Dictionary<string, string> MailConfig, string subject, string text, string email, out string beac)
        {
            beac = "";
            if (!HttpTool.IsConnectInternet())
            {
                beac = "网络不通";
                return false;
            }
            string from = MailConfig["from"];
            string pwd = MailConfig["pwd"];
            string smtpServer = MailConfig["smtp"];
            string port = MailConfig["port"];
            bool ssl = false;
            if (Convert.ToInt16(MailConfig["ssl"]) > 0)
                ssl = true;

            if (smtpServer.Contains("qq.com"))
            {
                bool p = qqMail(MailConfig, subject, text, email, out beac);
                return p;
            }
            //邮件发送者
            MailAddress f = new MailAddress(from);
            //邮件接收者
            MailAddress to = new MailAddress(email);
            MailMessage mailobj = new MailMessage(f, to);
            // 添加发送和抄送
            // mailobj.To.Add("");
            // mailobj.CC.Add("");

            //邮件标题
            mailobj.Subject = subject;
            //邮件内容
            mailobj.Body = text;

            //邮件不是html格式
            mailobj.IsBodyHtml = true;
            //邮件编码格式
            mailobj.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //邮件优先级
            mailobj.Priority = MailPriority.High;

            //Initializes a new instance of the System.Net.Mail.SmtpClient class 
            //that sends e-mail by using the specified SMTP server.
            SmtpClient smtp = new SmtpClient(smtpServer);
            //或者用：
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = mailServer;
            smtp.EnableSsl = ssl;
            //不使用默认凭据访问服务器
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(from.Split('@')[0], pwd);
            //使用network发送到smtp服务器
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                //开始发送邮件
                smtp.Send(mailobj);
                return true;
            }
            catch (Exception e)
            {
                beac = e.Message;
                return false;
            }
        }

        private static bool qqMail(Dictionary<string, string> MailConfig, string subject, string text, string email, out string beac)
        {
            beac = "";
            if (!HttpTool.IsConnectInternet())
            {
                beac = "网络不通";
                return false;
            }
            string from = MailConfig["from"];
            string pwd = MailConfig["pwd"];
            string smtpServer = MailConfig["smtp"];
            string port = MailConfig["port"];
            bool ssl = false;
            if (Convert.ToInt16(MailConfig["ssl"]) > 0)
                ssl = true;

            beac = "";
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mail.From = new MailAddress(from, "", Encoding.UTF8);
            mail.To.Add(new MailAddress(email));
            mail.IsBodyHtml = true;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Subject = subject;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.Normal;
            mail.Body = text;
            mail.Headers.Add("Disposition-Notification-To", "通知信息");
            smtp.Host = smtpServer;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Timeout = 1000000;
            smtp.EnableSsl = ssl;
            smtp.Port = Convert.ToInt32(port);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(from, pwd);
            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                beac = ex.Message;
                return false;
            }
        }
    }
}
