using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using static System.Console;

namespace Ch_10_Ex_3
{
    enum SerializationType
    {
        [FileName("jsonOutput.json")]
        [Description("JsonFormat")]
        Json,
        [FileName("xmlOutput.xml")]
        [Description("XmlFormat")]
        Xml,
        [FileName("binaryOutput.bin")]
        [Description("BinaryFormat")]
        Binary,
        [FileName("soapOutput.txt")]
        [Description("SoapFormat")]
        Soap,
        [FileName("dataContractOutput.txt")]
        [Description("DataContractFormat")]
        DataContract,
        [FileName("dataContractJsonOutput.txt")]
        [Description("DataContractJsonFormat")]
        DataContractJson
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class FileNameAttribute : Attribute
    {
        public string Name { get; set; }

        public FileNameAttribute(string name)
        {
            Name = name;
        }
    }

    public interface IProductSerializer
    {
        void Serialize(string filePath, List<Product> product);
    }

    public class XmlProductSerializer : IProductSerializer
    {
        public void Serialize(string filePath, List<Product> products)
        {
            using (FileStream xmlStream = File.Create(filePath))
            {
                // create an object that will format a List of Persons as XML
                var xs = new XmlSerializer(products.GetType());
                // serialize the object graph to the stream
                xs.Serialize(xmlStream, products);
                WriteLine($"Written {new FileInfo(filePath).Length} bytes of XML to {filePath}");
                WriteLine();
            }
        }
    }

    public class JsonProductSerializer : IProductSerializer
    {
        public void Serialize(string filePath, List<Product> products)
        {
            using (FileStream jsonStream = File.Create(filePath))
            using (StreamWriter jsonWriter = new StreamWriter(jsonStream))
            {
                // create an object that will format a List of Persons as XML
                var jsonSerializer = new JavaScriptSerializer();
                // serialize the object graph to the stream
                var jsonProducts = jsonSerializer.Serialize(products);
                jsonWriter.Write(jsonProducts);
                WriteLine($"Written {new FileInfo(filePath).Length} bytes of JSON to {filePath}");
                WriteLine();
            }
        }
    }
}
