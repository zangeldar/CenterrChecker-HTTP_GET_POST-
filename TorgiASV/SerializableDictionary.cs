using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TorgiASV
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable, ISerializable
    {
        public SerializableDictionary() { }

        public SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /*
        public void GetObjectData (SerializationInfo info, StreamingContext context){}
        */

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
    }
}
    /*
    public static string Serialize(SerializableDictionary<string, object> data)
    {
        using (StringWriter sw = new StringWriter())
        {
            XmlSerializer ser = new XmlSerializer(typeof(SerializableDictionary<string, object>));
            ser.Serialize(sw, data);
            return sw.ToString();
        }
    }
    */
    /*
    Serialize(new SerializableDictionary<string, object>() {
    { "Node 1", "Node 1 value" },
    { "Node 2", true },
    { "Node 3", 5}    
    });
    */

    //<?xml version="1.0" encoding="utf-16"?>
    //<dictionary>
    //  <item>
    //    <key>
    //      <string>Node 1</string>
    //    </key>
    //    <value>
    //      <anyType xmlns:q1="http://www.w3.org/2001/XMLSchema" d4p1:type="q1:string" xmlns:d4p1="http://www.w3.org/2001/XMLSchema-instance">Node 1 value</anyType>
    //    </value>
    //  </item>
    //  <item>
    //    <key>
    //      <string>Node 2</string>
    //    </key>
    //    <value>
    //      <anyType xmlns:q1="http://www.w3.org/2001/XMLSchema" d4p1:type="q1:boolean" xmlns:d4p1="http://www.w3.org/2001/XMLSchema-instance">true</anyType>
    //    </value>
    //  </item>
    //  <item>
    //    <key>
    //      <string>Node 3</string>
    //    </key>
    //    <value>
    //      <anyType xmlns:q1="http://www.w3.org/2001/XMLSchema" d4p1:type="q1:int" xmlns:d4p1="http://www.w3.org/2001/XMLSchema-instance">5</anyType>
    //    </value>
    //  </item>
    //</dictionary>

    /*
    Serialize(new SerializableDictionary<string, object>() {
    { "Node 1", "Node 1 value" },
    { "Node 2", true },
    { "Node 3", new int[]{1,2,3}}
    });
    */

// System.InvalidOperationException: The type System.Int32[] may not be used in this context.
// System.InvalidOperationException: There was an error generating the XML document.
//                                   method WriteXml valueSerializer.Serialize(writer, value);