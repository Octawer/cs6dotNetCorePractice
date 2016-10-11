using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Console;

namespace Ch_10_Ex_3
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductSerializer serializer;

            string outputFileDir = @"C:\Code\Ch_10\Ch_10_Ex_3\output";
            string outputFile;
            
            var db = new Northwind();
            var products = db.Products.OrderBy(product => product.ProductName);
            var categories = db.Categories.OrderBy(category => category.CategoryName);

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
                    break;
                case SerializationType.DataContract:
                    break;
                case SerializationType.DataContractJson:
                    break;
                case SerializationType.Json:
                    outputFile = Path.Combine(outputFileDir, SerializationType.Json.GetAttributeOfType<FileNameAttribute>()?.Name);
                    serializer = new JsonProductSerializer();
                    serializer.Serialize(outputFile, products.ToList());
                    break;
                case SerializationType.Soap:
                    break;
                case SerializationType.Xml:
                    outputFile = Path.Combine(outputFileDir, SerializationType.Xml.GetAttributeOfType<FileNameAttribute>()?.Name);
                    serializer = new XmlProductSerializer();
                    serializer.Serialize(outputFile, products.ToList());
                    break;
                default:
                    break;
            }

           
        }
    }
}
