using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MobileConfigUtility
{
    public class Plist
    {
        public string DocType { get; set; }
        public string PubId { get; set; }
        public string SysId { get; set; }
        public string Subset { get; set; }
        public string Version { get; set; }
        public string OutputFile { get; set; }
        public Profile Profile { get; set; }
        private XmlWriterSettings settings;
        private XmlSerializer serializer;
        private XmlWriter writer;

        public Plist()
        {
            // The XmlWriter does most of the work here and we provide some default settings.
            settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";

            // Grab the defaults that we need.
            SetDefaultValues();
        }

        public Plist(XmlWriterSettings customSettings)
        {
            settings = customSettings;
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            // Default to dropping output on the desktop as settings.mobileconfig.
            OutputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "settings.mobileconfig");

            // Default plist values from Apple.
            DocType = "plist";
            PubId = "-//Apple//DTD PLIST 1.0//EN";
            SysId = "http://www.apple.com/DTDs/PropertyList-1.0.dtd";
            Subset = null;			// This doesn't seemed to be used, so I just set it to null.
            Version = "1.0";

            // Also we will need a serializer to serialize the profile.
            serializer = new XmlSerializer(typeof(Profile));

        }

        public void Create(Profile profile)
        {
            // We will need that XmlWriter.
            writer = XmlWriter.Create(OutputFile, settings);

            writer.WriteStartDocument();
            writer.WriteDocType(DocType,PubId,SysId,Subset);
            writer.WriteStartElement(DocType);
            writer.WriteAttributeString("version",Version);
            serializer.Serialize(writer,profile);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            
            writer.Close();
        }

        public void Create(Stream stream,Profile profile)
        {
            // We will need that XmlWriter.
            writer = XmlWriter.Create(stream, settings);

            writer.WriteStartDocument();
            writer.WriteDocType(DocType,PubId,SysId,Subset);
            writer.WriteStartElement(DocType);
            writer.WriteAttributeString("version", Version);
            serializer.Serialize(writer,profile);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            
            writer.Close();
        }

        public void CreateDummy()
        {
            writer = XmlWriter.Create(OutputFile,settings);

            writer.WriteStartDocument();
            writer.WriteDocType(DocType, PubId, SysId, Subset);
            writer.WriteStartElement(DocType);
            writer.WriteAttributeString("version", Version);
            writer.WriteElementString("key", "TestKey");
            writer.WriteElementString("value", "TestValue");
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Close();
        }
    }
}
