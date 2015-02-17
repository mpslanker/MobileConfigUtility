using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace MobileConfigUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneratePlist();
            //Console.ReadLine();
        }


        static void PrintProperties(Type myType)
        {
            PropertyInfo[] properties = myType.GetProperties();
            Array.Sort(properties, delegate(PropertyInfo pi1, PropertyInfo pi2) { return pi1.Name.CompareTo(pi2.Name); });
            foreach (PropertyInfo pi in properties)
            {
                Console.Write (pi.Name.ToString() + " - ");
                try
                {
                    //Console.Write(pi.GetValue (prof).ToString ());
                }
                catch(Exception ex)
                {
                    Console.Write (ex.Message);
                }
                Console.WriteLine("");
            }
        }

        static void WriteSimpleXml()
        {
            // We will try to write simple XML using the Plist object.
            Plist plist = new Plist();
            plist.CreateDummy();
        }

        static void GeneratePlist()
        {
            Plist plist = new Plist();
            Profile profile = new Profile();
            
            profile.PayloadDisplayName = "Demo Display Name";
            profile.PayloadIdentifier = "Demo-Identifier";
            profile.PayloadUUID = "00000000-0000-0000-0000-000000000000";

            EmailPayload emailPayload = new EmailPayload ();

            emailPayload.PayloadDisplayName = "NON-EXCHANGE_MAIL";
            emailPayload.PayloadIdentifier = "Demo-Identifier.Email";
            emailPayload.PayloadUUID = "00000000-0000-0000-0000-000000000000";
            emailPayload.PayloadVersion = 1;
            emailPayload.EmailAccountDescription = "POP3 Email Account.";
            emailPayload.EmailAccountName = "Your Display Name";
            emailPayload.EmailAccountType = EmailAccountTypes.EmailTypeIMAP;
            emailPayload.EmailAddress = "email@example.com";
            emailPayload.IncomingMailServerAuthentication = ServerAuthMethods.EmailAuthPassword;
            emailPayload.IncomingMailServerHostName = "email.example.com";
            emailPayload.IncomingMailServerPortNumber = 110;
            emailPayload.IncomingMailServerUseSSL = false;
            emailPayload.IncomingMailServerUsername = "email@example.com";
            emailPayload.IncomingPassword = "your_password";
            emailPayload.OutgoingPasswordSameAsIncomingPassword = true;
            emailPayload.OutgoingMailServerAuthentication = ServerAuthMethods.EmailAuthPassword;
            emailPayload.OutgoingMailServerHostName = "email.example.com";
            emailPayload.OutgoingMailServerPortNumber = 25;
            emailPayload.OutgoingMailServerUseSSL = false;
            emailPayload.OutgoingMailServerUsername = "email@example.com";
            emailPayload.PreventMove = false;
            emailPayload.PreventAppSheet = false;
            emailPayload.SMIMEEnable = false;
            emailPayload.SMIMESigningCertificateUUID = "";
            emailPayload.SMIMEEncryptionCertificateUUID = "";
            emailPayload.SMIMEEnablePerMessageSwitch = false;
            emailPayload.disableMailRecentsSyncing = false;
            
			ExchangePayload exchPayload = new ExchangePayload ();

			exchPayload.PayloadDisplayName = "EXCHANGE_MAIL";
			exchPayload.PayloadIdentifier = "Demo-Identifier.Exchange";
			exchPayload.PayloadUUID = "00000000-0000-0000-0000-000000000000";
			exchPayload.PayloadVersion = 1;
			exchPayload.EmailAddress = "exchange@example.com";
			exchPayload.UserName = "username";
			exchPayload.Password = "your_password";
			exchPayload.Host = "owa.example.com";

            //profile.PayloadContent.Add (emailPayload);
			profile.PayloadContent.Add (exchPayload);

            // Write to file
            plist.Create(profile);
            // Write to console - By default on your desktop.
            //plist.Create(Console.OpenStandardOutput(),profile);
        }
    }
}
