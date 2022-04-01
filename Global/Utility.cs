using System;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Net.Mail;

namespace Global
{
    public static class Utility
    {
        public class Captcha
        {
            private static readonly Random random = new Random();

            public static MemoryStream GenerateCaptcha(out string verificationCode)
            {
                // 创建一个指定大小的位图
                Bitmap bitmap = new Bitmap(50, 30);
                // 在位图上创建一个绘图图面，并设置底色
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(GenerateRandomColor());
                // 创建一支笔，根据坐标绘图
                Pen pen = new Pen(Color.Transparent);
                // 绘制随机干扰线条
                for (int i = 0; i < random.Next(10, 20); i++)
                {
                    pen.Color = GenerateRandomColor();
                    graphics.DrawLine(pen, GenerateRandomPoint(), GenerateRandomPoint());
                    bitmap.SetPixel(random.Next(50), random.Next(30), GenerateRandomColor());
                }

                // 绘制随机字符串
                char[] randomChar = "qwertyuipasdfghjkzxcvbnm78945623MNBVCXZLKJHGFDSAPIUYTREWQ".ToCharArray();
                char[] code = new char[4];
                PointF pointF = new PointF(-2, 4.2F);

                for (int i = 0; i < 4; i++)
                {
                    code[i] = randomChar[random.Next(randomChar.Length)];
                    // 防止字体过于靠右
                    pointF.X = (i == 0) ? pointF.X : pointF.X + 11;
                    graphics.DrawString(code[i].ToString(), new Font("幼圆", 18, FontStyle.Regular),
                                        new SolidBrush(Color.Black), pointF);
                }

                verificationCode = new string(code);
                // 将图片以内存流的方式返回
                MemoryStream memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return memoryStream;
            }

            private static Point GenerateRandomPoint()
            {
                Point point = new Point
                {
                    X = random.Next(50),
                    Y = random.Next(30)
                };
                return point;
            }

            private static Color GenerateRandomColor()
            {
                return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }
        }

        public static string MD5Encrypt(string encryptedStr)
        {
            byte[] bytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(encryptedStr));
            StringBuilder sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 生成指定位数随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomString(int length)
        {
            Random random = new Random();
            char[] creatChar = new char[length];
            string str = "qwertyuiopasdfghjklzxcvbnm789456123MNBVCXZLKJHGFDSAPOIUYTREWQ";
            char[] randomChar = str.ToCharArray();

            for (int i = 0; i < length; i++)
            {
                creatChar[i] = randomChar[random.Next(randomChar.Length)];
            }

            return new string(creatChar);
        }

        public static void SendEmail(string mailTo, string mailSubject, string mailContent)
        {
            // 设置发件邮箱信息
            string smtpServer = "smtp.qq.com";
            string mailForm = "ylhework@foxmail.com";
            string authorizationCode = "bbdltwdwbsanbdcf";

            // 设置发送邮件
            MailMessage mailMessage = new MailMessage(mailForm, mailTo)// 发件人和收件人
            {
                Subject = mailSubject,
                Body = mailContent,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Priority = MailPriority.Normal,
            };

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient(smtpServer)// 指定smtp服务器
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(mailForm, authorizationCode)
            };

            try
            {
                smtpClient.Send(mailMessage);
            }
            finally
            {
                smtpClient.Dispose();
                mailMessage.Dispose();
            }
        }
    }
}
