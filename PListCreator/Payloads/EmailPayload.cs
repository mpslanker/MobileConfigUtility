using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MobileConfigUtility
{
    public enum EmailAccountTypes
    {
        EmailTypePOP = 0,
        EmailTypeIMAP = 1,
    }

	public enum ServerAuthMethods
	{
		EmailAuthPassword = 0,
		EmailAuthNone = 1,
	}

    public class EmailPayload : Payload, IXmlSerializable
    {
        [KeyAttributes("EmailAccountDescription",PlistType.String)]
        public string EmailAccountDescription { get; set; }

        [KeyAttributes("EmailAccountName",PlistType.String)]
        public string EmailAccountName { get; set; }
        
        [KeyAttributes("EmailAccountType", PlistType.String, Presence.Required)]
        public EmailAccountTypes EmailAccountType { get; set; }

        [KeyAttributes("EmailAddress",PlistType.String,Presence.Required)]
        public string EmailAddress { get; set; }

        [KeyAttributes("IncomingMailServerAuthentication",PlistType.String,Presence.Required)]
        public ServerAuthMethods IncomingMailServerAuthentication { get; set; }

        [KeyAttributes("IncomingMailServerHostName",PlistType.String,Presence.Required)]
        public string IncomingMailServerHostName { get; set; }

        [KeyAttributes("IncomingMailServerPortNumber",PlistType.Integer, "0")]
        public int IncomingMailServerPortNumber { get; set; }

        [KeyAttributes("IncomingMailServerUseSSL",PlistType.Boolean, "true")]
        public bool IncomingMailServerUseSSL { get; set; }

        [KeyAttributes("IncomingMailServerUsername",PlistType.String,Presence.Required)]
        public string IncomingMailServerUsername { get; set; }

        [KeyAttributes("IncomingPassword",PlistType.String, Presence.Required)]
        public string IncomingPassword { get; set; }

        [KeyAttributes("IncomingPassword",PlistType.String,Presence.Conditional)]
        public string OutgoingPassword { get; set; }

        [KeyAttributes("OutgoingPasswordSameAsIncomingPassword",PlistType.Boolean, "false")]
        public bool OutgoingPasswordSameAsIncomingPassword { get; set; }

        [KeyAttributes("OutgoingMailServerAuthentication",PlistType.String,Presence.Required)]
        public ServerAuthMethods OutgoingMailServerAuthentication { get; set; }

        [KeyAttributes("OutgoingMailServerHostName",PlistType.String,Presence.Required)]
        public string OutgoingMailServerHostName { get; set; }

        [KeyAttributes("OutgoingMailServerPortNumber",PlistType.Integer, "25")]
        public int OutgoingMailServerPortNumber { get; set; }

        [KeyAttributes("OutgoingMailServerUseSSL",PlistType.Boolean, "true")]
        public bool OutgoingMailServerUseSSL { get; set; }

        [KeyAttributes("OutgoingMailServerUsername",PlistType.String,Presence.Required)]
        public string OutgoingMailServerUsername { get; set; }

        [KeyAttributes("PreventMove",PlistType.Boolean, "false")]
        public bool PreventMove { get; set; }						// Default: false

        [KeyAttributes("PreventAppSheet",PlistType.Boolean, "false")]
        public bool PreventAppSheet { get; set; }					// Default: false

        [KeyAttributes("SMIMEEnable",PlistType.Boolean, "false")]
        public bool SMIMEEnable { get; set; }						// Default: false

        [KeyAttributes("SMIMESigningCertificateUUID",PlistType.String, "")]
        public string SMIMESigningCertificateUUID { get; set; }

        [KeyAttributes("SMIMEEncryptionCertificateUUID",PlistType.String, "")]
        public string SMIMEEncryptionCertificateUUID { get; set; }

        [KeyAttributes("SMIMEEnablePerMessageSwitch",PlistType.Boolean, "false")]
        public bool SMIMEEnablePerMessageSwitch { get; set; }		// Default: false

        // WhyTF did Apple not capitalize this one???
        [KeyAttributes("disableMailRecentsSyncing", PlistType.Boolean, "false")]
        public bool disableMailRecentsSyncing { get; set; }		// Default: false

        public EmailPayload ()
        {
            PayloadType = "com.apple.mail.managed";
        }
    }
}

