using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Console;

namespace Ex_10_3
{
    class Program
    {
        static void Main(string[] args)
        {
            ICategorySerializer serializer;

            string outputFileDir = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "output");  // not the executing one, but the static one... (not bin/debug ...)
            string outputFile = string.Empty;

            var db = new Northwind();
            db.Configuration.ProxyCreationEnabled = false;
            //var products = db.Products.OrderBy(product => product.ProductName).ToList();
            var categories = db.Categories.Include("Products").OrderBy(category => category.CategoryName).ToList();

            var serializationTypes = new List<SerializationType>();

            foreach (var serializationType in Enum.GetValues(typeof(SerializationType)).Cast<SerializationType>())
            {
                serializationTypes.Add(serializationType);
            }

            WriteLine($"Enter a serialization type from the following: {string.Join(",", serializationTypes.ToArray())}");
            var selectedType = (SerializationType)Enum.Parse(typeof(SerializationType), ReadLine());

            switch (selectedType)
            {
                case SerializationType.Binary:
                    outputFile = Path.Combine(outputFileDir, SerializationType.Binary.GetAttributeOfType<FileNameAttribute>()?.Name);
                    serializer = new BinaryCategorySerializer();
                    serializer.Serialize(outputFile, categories);
                    break;
                case SerializationType.DataContract:
                    break;
                case SerializationType.DataContractJson:
                    outputFile = Path.Combine(outputFileDir, SerializationType.DataContractJson.GetAttributeOfType<FileNameAttribute>()?.Name);
                    serializer = new DataContractJsonCategorySerializer();
                    serializer.Serialize(outputFile, categories);
                    break;
                case SerializationType.Json:
                    outputFile = Path.Combine(outputFileDir, SerializationType.Json.GetAttributeOfType<FileNameAttribute>()?.Name);
                    serializer = new JsonCategorySerializer();
                    serializer.Serialize(outputFile, categories);
                    break;
                case SerializationType.Soap:
                    break;
                case SerializationType.Xml:
                    outputFile = Path.Combine(outputFileDir, SerializationType.Xml.GetAttributeOfType<FileNameAttribute>()?.Name);
                    serializer = new XmlCategorySerializer();
                    serializer.Serialize(outputFile, categories);
                    break;
                default:
                    break;
            }
            
        }
    }
}
