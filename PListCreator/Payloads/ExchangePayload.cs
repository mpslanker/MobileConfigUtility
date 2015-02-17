using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MobileConfigUtility
{
	public class ExchangePayload : Payload, IXmlSerializable
	{
		[KeyAttributes("EmailAddress", PlistType.String, Presence.Required)]
		public string EmailAddress { get; set; }

		[KeyAttributes("Host", PlistType.String, Presence.Required)]
		public string Host { get; set; }

		[KeyAttributes("SSL", PlistType.Boolean, "true", Presence.Optional)]
		public bool SSL { get; set; }

		[KeyAttributes("UserName", PlistType.String, Presence.Required)]
		public string UserName { get; set; }

		[KeyAttributes("Password", PlistType.String, Presence.Optional)]
		public string Password { get; set; }

		[KeyAttributes("Certificate", PlistType.String, Presence.Optional)]
		public byte[] Certificate { get; set; }

		[KeyAttributes("CertificateName", PlistType.String, Presence.Optional)]
		public string CertificateName { get; set; }

		[KeyAttributes("CertificatePassword", PlistType.String, Presence.Optional)]
		public byte[] CertificatePassword { get; set; }

		[KeyAttributes("PreventMove", PlistType.Boolean, "false", Presence.Optional)]
		public bool PreventMove { get; set; }

		[KeyAttributes("PreventAppSheet", PlistType.Boolean, "false", Presence.Optional)]
		public bool PreventAppSheet { get; set; }

		[KeyAttributes("PayloadCertificateUUID", PlistType.String)]
		public string PayloadCertificateUUID { get; set; }

		[KeyAttributes("SMIMEEnabled", PlistType.Boolean, "false", Presence.Optional)]
		public bool SMIMEEnabled { get; set; }

		[KeyAttributes("SMIMESigningCertificateUUID", PlistType.String, Presence.Optional)]
		public string SMIMESigningCertificateUUID { get; set; }

		[KeyAttributes("SMIMEEncryptionCertificateUUID", PlistType.String, Presence.Optional)]
		public string SMIMEEncryptionCertificateUUID { get; set; }

		[KeyAttributes("SMIMEEnablePerMessageSwitch", PlistType.Boolean, Presence.Optional)]
		public bool SMIMEEnablePerMessageSwitch { get; set; }

		[KeyAttributes("disableMailRecentsSyncing", PlistType.Boolean, "false")]
		public bool disableMailRecentsSyncing { get; set; }

		[KeyAttributes("MailNumberOfPastDaysToSync", PlistType.Integer)]
		public int MailNumberOfPastDaysToSync { get; set; }

		[KeyAttributes("HeaderMagic", PlistType.String, Presence.Optional)]
		public string HeaderMagic { get; set; }

		[KeyAttributes("Path", PlistType.String, Presence.Optional)]
		public string Path { get; set; }

		[KeyAttributes("Port", PlistType.Integer, Presence.Optional)]
		public int Port { get; set; }

		[KeyAttributes("ExternalHost", PlistType.String, Presence.Optional)]
		public string ExternalHost { get; set; }

		[KeyAttributes("ExternalSSL", PlistType.Boolean, "true", Presence.Optional)]
		public bool ExternalSSL { get; set; }

		[KeyAttributes("ExternalPath", PlistType.String, Presence.Optional)]
		public string ExternalPath { get; set; }

		[KeyAttributes("ExternalPort", PlistType.Integer, Presence.Optional)]
		public int ExternalPort { get; set; }

		public ExchangePayload ()
		{
			PayloadType = "com.apple.eas.account";		// iOS PayloadType
			// PayloadType = "com.apple.ews.account";	// OS X PayloadType
		}
	}
}

