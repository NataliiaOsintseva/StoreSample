using Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Store.Domain.Concrete
{
    public class OrderProcessor : IOrder
    {
        private EmailSettings emailSettings;

        public OrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, Shipment shipment)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder emailBody = new StringBuilder()
                    .AppendLine("You've got new order!\n")
                    .AppendLine("Items:");

                foreach(var item in cart.Lists)
                {
                    var subtotal = item.Product.Price * item.Quantity;
                    emailBody.AppendFormat("{0} {1} | Subtotal: {2:c}",item.Quantity, item.Product.Name, subtotal);
                }

                emailBody.AppendFormat("Total order value: {0:c}", cart.CalculateTotalValue())
                .AppendLine("\nShipt To:")
                .AppendLine(shipment.Name)
                .AppendLine(shipment.HomeAddress)
                .AppendLine(shipment.City)
                .AppendLine(shipment.Country)
                .AppendFormat(shipment.Discount ? "Customer is a discount-holder" : null);

                MailMessage message = new MailMessage(

                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "You have a new order!",
                    emailBody.ToString());

                if (emailSettings.WriteAsFile)
                {
                    message.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(message);
            }
        }
    }

    public class EmailSettings
    {
        public string MailToAddress = "admin_orders@example.com";
        public string MailFromAddress = "customer_store@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\store_emails";
    }

}