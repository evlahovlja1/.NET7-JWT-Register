namespace WebApi.Helpers;

using System.Net.Mail;
public class EmailSender
    {
        public async Task<bool> SendEmailAsync(string userEmail, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("lifeplannerdemo@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            
            // client.Credentials = new System.Net.NetworkCredential("lifeplannerdemo@gmail.com", "AcZ29Th5vgAytEh");
            client.Credentials = new System.Net.NetworkCredential("lifeplannerdemo@gmail.com", "ktiwhjwhzmhppnuw");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch
            {
                // log exception
            }
            return false;
        }
    }