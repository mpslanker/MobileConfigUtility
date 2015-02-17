using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Reflection;

namespace MobileConfigUtility
{
    [XmlRoot("dict")]
    public class Payload : IPayload, IXmlSerializable
    {
        [KeyAttributes("PayloadType", PlistType.String, Presence.Required)]
        public string PayloadType { get; set; }
        
        [KeyAttributes("PayloadVersion", PlistType.Integer, "1", Presence.Required)]
        public int PayloadVersion { get; set; }
        
        [KeyAttributes("PayloadIdentifier", PlistType.String, Presence.Required)]
        public string PayloadIdentifier { get; set; }
        
        [KeyAttributes("PayloadUUID", PlistType.String, Presence.Required)]
        public string PayloadUUID { get; set; }
        
        [KeyAttributes("PayloadDisplayName", PlistType.String, Presence.Required)]
        public string PayloadDisplayName { get; set; }
        
        [KeyAttributes("PayloadDescription", PlistType.String)]
        public string PayloadDescription { get; set; }
        
        [KeyAttributes("PayloadOrganization", PlistType.String)]
        public string PayloadOrganization { get; set; }

        public Payload()
        {
			PropertyInfo[] properties = this.GetType().GetProperties();
			Array.Sort(properties, delegate(PropertyInfo pi1, PropertyInfo pi2) { return pi1.Name.CompareTo(pi2.Name); });
			foreach (PropertyInfo pi in properties) {
			  foreach (object attr in pi.GetCustomAttributes(true)) {
			      if (attr is KeyAttributes) {
			          KeyAttributes ka = attr as KeyAttributes;
						if (ka.DefaultsTo != "default") {
							pi.SetValue(this, Convert.ChangeType(ka.DefaultsTo, pi.PropertyType));
			          }
			      }
			  }
			}		
        }

        public virtual void ReadXml(XmlReader reader)
        {
            // Do Stuff Here.
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            //writer.WriteStartElement("dict");
            // This XmlSerializer is needed for complex members.
            XmlSerializer serial = new XmlSerializer(typeof(Payload));

			// Set PayloadUUID, PayloadIdentifier
			PayloadUUID = Guid.NewGuid ().ToString ();
			PayloadIdentifier = System.Environment.MachineName + "." + Guid.NewGuid ().ToString();
			if (PayloadType != null && !PayloadType.Equals("Configuration"))
			{
				PayloadIdentifier = PayloadIdentifier + "." + PayloadType + "." + PayloadUUID;
			}

            PropertyInfo[] properties = this.GetType().GetProperties();
            Array.Sort(properties, delegate(PropertyInfo pi1, PropertyInfo pi2) { return pi1.Name.CompareTo(pi2.Name); });
            foreach (PropertyInfo pi in properties)
            {
                // Start with the assumption that each key is optional.
                bool writeValueFlag = false;
                // We now have an ordered array of properties.
                // Loop over get attributes and write XML as necessary.
                foreach (object attr in pi.GetCustomAttributes(true))
                {
                    if (attr is KeyAttributes)
                    {
                        KeyAttributes ka = attr as KeyAttributes;

                        switch(ka.Presence.GetHashCode())
                        {
                        case(0):		// Optional
                            // Test for non-default data, write key if exists
                                if (CheckIfPropertyHasData(pi, ka))
                                {
                                    writer.WriteElementString("key", ka.KeyName);
                                    // Set writeValueFlag
                                    writeValueFlag = true;
                                }
                            
                            break;
                        case(1):		// Required
                            if (CheckIfPropertyHasData(pi, ka))
                            {
                                writer.WriteElementString("key", ka.KeyName);
                                writeValueFlag = true;
                            }
                            else
                            {
                                // If you have not provided a required field we blow the fuck up.
                                Exception ex = new Exception(this.GetType().ToString() + ":" + pi.Name + " is a required. Please provide a value.");
                                throw ex;
                            }
                            break;
                        case(2):		// Conditional
                            if (CheckIfPropertyHasData(pi, ka))
                            {
                                writer.WriteElementString("key", ka.KeyName);
                                // Set writeValueFlag
                                writeValueFlag = true;
                            }
                            break;
                        }

                        if (writeValueFlag)
                        {
                            switch(ka.PType.GetHashCode())
                            {
                            case(0):		// Array
                                writer.WriteStartElement ("array");
                                var arr = pi.GetValue(this);
                                if (arr is List<Payload>)
                                {
                                    var mylist = arr as List<Payload>;
                                    foreach (Payload p in mylist)
                                    {
                                        serial.Serialize(writer, p);
                                    }
                                }
                                writer.WriteEndElement ();
                                break;

                            case(1):		// Dict
                                break;

                            case(2):		// String
                                writer.WriteElementString ("string", pi.GetValue (this).ToString ());
                                break;

                            case(3):		// Data
                                break;

                            case(4):		// Date
                                writer.WriteElementString ("date", pi.GetValue (this).ToString ());
                                break;

                            case(5):		// Integer
                                writer.WriteElementString ("integer", pi.GetValue (this).ToString ());
                                break;

                            case(6):		// Real
                                writer.WriteElementString ("real", pi.GetValue (this).ToString ());
                                break;

                            case(7):		// Boolean
                                writer.WriteStartElement (pi.GetValue (this).ToString ().ToLower ());
                                writer.WriteEndElement ();
                                break;
                            }
                        }
                    }
                }
            }
            //writer.WriteEndElement();
        }

        private bool CheckIfPropertyHasData(PropertyInfo pi, KeyAttributes ka)
        {
            bool flag = false;
            switch(ka.PType.GetHashCode())
            {
                case(0):        // Array
                    var arr = pi.GetValue(this);
                    if (arr is List<Payload>)
                    {
                        var mylist = arr as List<Payload>;
                        if (mylist.Any()) { flag = true; }
                    }
                    break;

                case(1):        // Dict
                    
                    break;

                case(2):        // String
                    if (pi.GetValue(this) != null)
                    {
                        if (!pi.GetValue(this).Equals(String.Empty)) { flag = true; }
                    }
                    break;
                case (3):       // Data
                    break;
                case (4):       // Date
                    if (DateTime.Compare(DateTime.Parse(pi.GetValue(this).ToString()), DateTime.MinValue) != 0)
                    {
                        flag = true;
                    }
                    break;
                case (5):       // Integer
                    if (int.Parse(pi.GetValue(this).ToString()) > 0)
                    {
                        flag = true;
                    }
                    break;
                case (6):       // Real
                    if (float.Parse(pi.GetValue(this).ToString()) > 0.0)
                    {
                        flag = true;
                    }
                    break;
                case (7):       // Boolean
                    string val1 = ka.DefaultsTo.ToString();
                    string val2 = pi.GetValue(this).ToString();
                    if (!val1.Equals(val2,StringComparison.CurrentCultureIgnoreCase))
                    {
                        flag = true;
                    }
                    break;
            }
            return flag;
        }
        
        public virtual XmlSchema GetSchema()
        {
            return (null);
        }

        public void PrintPropertiesToConsole()
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            Array.Sort(properties, delegate(PropertyInfo pi1, PropertyInfo pi2) { return pi1.Name.CompareTo(pi2.Name); });
            foreach (PropertyInfo pi in properties)
            {
                foreach (object attr in pi.GetCustomAttributes(true))
                {
                    if (attr is KeyAttributes)
                    {
                        KeyAttributes ka = attr as KeyAttributes;
                        Console.WriteLine ("[" + ka.KeyName +" "+ ka.PType +" "+ ka.Presence +"]");
                    }
                }
                Console.Write (pi.Name.ToString() + " - ");
                try
                {
                    Console.Write(pi.GetValue (this).ToString ());
                }
                catch(Exception ex)
                {
                    Console.Write (ex.Message);
                }
                Console.WriteLine("");
            }
        }
    }
}
