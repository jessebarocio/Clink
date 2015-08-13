using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clink.Reporters
{
    class SmtpReporter : IReporter
    {
        public string SmtpServer
        { get; set; }
        public int? SmtpPort
        { get; set; }
        public string UserName
        { get; set; }
        public string Password
        { get; set; }
        public bool UseSsl
        { get; set; }
        public string FromAddress
        { get; set; }
        public string ToAddress
        { get; set; }

        public void Report( Endpoint endpoint, EndpointStatus status )
        {
            var message = BuildMessage( endpoint, status );
            using ( var client = BuildSmtpClient() )
            {
                client.Send( message );
            }
        }

        private MailMessage BuildMessage( Endpoint endpoint, EndpointStatus status )
        {
            var message = new MailMessage();
            if ( !String.IsNullOrEmpty( FromAddress ) )
            {
                message.From = new MailAddress( FromAddress );
            }
            message.To.Add( ToAddress );
            message.Subject = String.Format( "Endpoint is {0}: {1}", status.Status, endpoint.Description );

            var template = GetTemplate();
            template = template.Replace( "##Description##", endpoint.Description );
            template = template.Replace( "##Url##", endpoint.Url );
            template = template.Replace( "##Status##", status.Status.ToString() );
            template = template.Replace( "##Timestamp##", status.Timestamp.ToString() );
            message.Body = template;

            return message;
        }

        private SmtpClient BuildSmtpClient()
        {
            SmtpClient client;
            // If SmtpServer and SmtpPort are set, we have enough to build an SmtpClient with their settings.
            if ( !String.IsNullOrEmpty( SmtpServer ) && SmtpPort.HasValue )
            {
                client = new SmtpClient( SmtpServer, SmtpPort.Value );
                if ( !String.IsNullOrEmpty( UserName ) && !String.IsNullOrEmpty( Password ) )
                {
                    client.Credentials = new NetworkCredential( UserName, Password );
                }
                client.EnableSsl = UseSsl;
            }
            else
            {
                client = new SmtpClient();
            }
            return client;
        }


        private static string GetTemplate()
        {
            using ( var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream( "Clink.Reporters.EmailTemplate.txt" ) )
            using ( var streamReader = new StreamReader( resource ) )
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
