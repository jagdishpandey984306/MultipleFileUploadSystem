using FileUpload.Model;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace FileUpload
{
    public static class EmailHelper
    {
        public static string Sending(AttachmentsFile sender)
        {
            try
            {
                var from = new MailAddress(sender.EmailId, sender.EmailId);
                var to = new MailAddress(sender.To, sender.To);
                string subject = "File";
                string body = "Please find your attached file as below:";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from.Address, sender.Password),
                    Timeout = 20000
                };

                using (var message = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    for (int i = 0; i < sender.Attachments.Length; i++)
                    {
                        switch (sender.Extension[i])
                        {
                            case ".jpeg":
                            case ".jpg":
                            case ".png":
                                Attachment attachment = new Attachment(new MemoryStream(sender.Attachments[i]), sender.FileName[i]);
                                message.Attachments.Add(attachment);
                                break;
                            case ".pdf":
                                attachment = new Attachment(new MemoryStream(sender.Attachments[i]), sender.FileName[i]);
                                message.Attachments.Add(attachment);
                                break;
                            default:
                                break;
                        }
                    }
                    smtp.Send(message);
                    return "Success";
                }
            }
            catch (Exception)
            {
                return "Error";
            }
        }
    }
}

