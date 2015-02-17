using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MobileConfigUtility
{
    [XmlRoot("dict")]
    public class Profile : Payload, IXmlSerializable
    {
        // C# member mappings <key>									    // Plist Types <value>

        [KeyAttributes("PayloadContent",PlistType.Array)]
        public List<Payload> PayloadContent { get; set; }				// Array

        [KeyAttributes("PayloadExpirationDate",PlistType.Date)]
        public DateTime PayloadExpirationDate { get; set; }			    // Date
        
        [KeyAttributes("PayloadRemovalDisallowed",PlistType.Boolean,"false")]
        public bool PayloadRemovalDisallowed { get; set; }				// Boolean
        
        [KeyAttributes("PayloadScope",PlistType.String)]
        public string PayloadScope { get; set; }						// String
        
        [KeyAttributes("RemovalDate",PlistType.Date)]
        public DateTime RemovalDate { get; set; }						// Date
        
        [KeyAttributes("DurationUntilRemoval",PlistType.Real)]
        public Double DurationUntilRemoval { get; set; }				// Float

        [KeyAttributes("ConsentText",PlistType.Dict)]
        public Dictionary<string, object> ConsentText { get; set; }		// Dictionary

        //public bool Encrypted??

        public Profile()
        {
            PayloadContent = new List<Payload>();
            // This is a must from Apple.
            PayloadType = "Configuration";
        }
    }
}
