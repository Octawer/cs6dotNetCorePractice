using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

using static System.Console;

namespace Ex_10_3
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
        [FileName("dataContractJsonOutput.json")]
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

    public interface ICategorySerializer
    {
        void Serialize(string filePath, List<Category> categories);
    }

    public class XmlCategorySerializer : ICategorySerializer
    {
        public void Serialize(string filePath, List<Category> categories)
        {
            using (FileStream xmlStream = File.Create(filePath))
            {
                // create an object that will format the data
                var xs = new XmlSerializer(typeof(List<Category>));
                // serialize the object graph to the stream
                xs.Serialize(xmlStream, categories);
                WriteLine($"Written {new FileInfo(filePath).Length} bytes of XML to {filePath}");
                WriteLine();
            }
        }
    }

    public class JsonCategorySerializer : ICategorySerializer
    {
        public void Serialize(string filePath, List<Category> categories)
        {
            using (FileStream jsonStream = File.Create(filePath))
            using (StreamWriter jsonWriter = new StreamWriter(jsonStream))
            {
                // create an object that will format a the data
                var jsonSerializer = new JavaScriptSerializer();
                // serialize the object graph to the stream
                var jsonProducts = jsonSerializer.Serialize(categories);
                jsonWriter.Write(jsonProducts);
                WriteLine($"Written {new FileInfo(filePath).Length} bytes of JSON to {filePath}");
                WriteLine();
            }
        }
    }

    public class BinaryCategorySerializer : ICategorySerializer
    {
        public void Serialize(string filePath, List<Category> categories)
        {
            using (FileStream binaryStream = File.Create(filePath))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(binaryStream, categories);
                WriteLine($"Written {new FileInfo(filePath).Length} bytes of proprietary binary to {filePath}");
                WriteLine();
            }
        }
    }

    public class DataContractJsonCategorySerializer : ICategorySerializer
    {
        public void Serialize(string filePath, List<Category> categories)
        {
            using (FileStream dataContractStream = File.Create(filePath))
            {
                var dataContractJsonSerializer = new DataContractJsonSerializer(typeof(List<Category>));
                dataContractJsonSerializer.WriteObject(dataContractStream, categories);
                WriteLine($"Written {new FileInfo(filePath).Length} bytes of data contract json to {filePath}");
                WriteLine();
            }
        }
    }
}
