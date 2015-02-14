using System;

namespace MobileConfigUtility
{
    public enum Presence
    {
        Optional = 0,
        Required = 1,
        Conditional = 2,

    }
    public enum PlistType
    {
        Array = 0,
        Dict = 1,
        String = 2,
        Data = 3,
        Date = 4,
        Integer = 5,
        Real = 6,
        Boolean = 7,
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttributes : Attribute
    {
        public string KeyName { get; set; }
        public Presence Presence { get; set; }
        public PlistType PType { get; set; }
        public string DefaultsTo { get; set; }

        public KeyAttributes(string keyName, PlistType pType, Presence presence = Presence.Optional)
        {
            KeyName = keyName;
            Presence = presence;
            PType = pType;
            DefaultsTo = "default";
        }

        public KeyAttributes (string keyName, PlistType pType, string defaultsTo, Presence presence = Presence.Optional)
        {
            KeyName = keyName;
            Presence = presence;
            PType = pType;
            DefaultsTo = defaultsTo;
        }
    }
}

