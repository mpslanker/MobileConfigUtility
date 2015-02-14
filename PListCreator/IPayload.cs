using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MobileConfigUtility
{
    interface IPayload
    {
        string PayloadType { get; set; }
        int PayloadVersion { get; set; }
        string PayloadIdentifier { get; set; }
        string PayloadUUID { get; set; }
        string PayloadDisplayName { get; set; }
        string PayloadDescription { get; set; }
        string PayloadOrganization { get; set; }
    }
}
