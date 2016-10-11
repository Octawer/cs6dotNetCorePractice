using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using static System.Console;

namespace Ch_10_Ex_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlFilePath = @"C:\Code\Ch_10\Ch_10_Ex_2\xmlShapes.xml";

            var shapeList = new List<Shape>
            {
                new Circle { Color = Color.AliceBlue.Name , Radius = 2.50 },
                new Rectangle { Color = Color.Azure.Name, Height = 12.30, Width = 5.50 },
                new Circle { Color = Color.Black.Name, Radius = 12.10 },
                new Rectangle { Color = Color.DarkGreen.Name, Height = 30.12, Width = 9.70 },
                new Rectangle { Color = Color.Gold.Name, Height = 99.12, Width = 40.70 }
            };



            //SerializePeople();

            SerializeShapes(shapeList, xmlFilePath);

            WriteDeserializedShapes(xmlFilePath);
        }

        private static void SerializePeople()
        {
            // create an object graph
            var people = new List<Person> {
                    new Person(30000M) { FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(1974, 3, 14) },
                    new Person(40000M) { FirstName = "Bob", LastName = "Jones",DateOfBirth = new DateTime(1969, 11, 23) },
                    new Person(20000M) { FirstName = "Charlie", LastName = "Rose",DateOfBirth = new DateTime(1964, 5, 4), Children = new HashSet<Person>
                    {
                        new Person(0M) { FirstName = "Sally", LastName = "Rose", DateOfBirth = new DateTime(1990, 7, 12) } }
                    }
            };
            // create a file to write to
            string xmlFilepath = @"C:\Code\Ch_10\Ch_10_Ex_2\xmlPeople.xml";
            FileStream xmlStream = File.Create(xmlFilepath);
            // create an object that will format a List of Persons as XML
            var xs = new XmlSerializer(typeof(List<Person>));
            // serialize the object graph to the stream
            xs.Serialize(xmlStream, people);
            // you must dispose the stream to release the file lock
            xmlStream.Dispose();
            WriteLine($"Written {new FileInfo(xmlFilepath).Length} bytes of XML to {xmlFilepath} ");
            WriteLine();
            // Display the serialized object graph
            WriteLine(File.ReadAllText(xmlFilepath));
        }

        private static void SerializeShapes(List<Shape> shapeList, string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream xmlStream = File.Create(filePath))
                {
                    var xs = new XmlSerializer(typeof(List<Shape>));
                    xs.Serialize(xmlStream, shapeList);
                    
                }

                WriteLine($"Written {new FileInfo(filePath).Length} bytes of XML to {filePath}");
                WriteLine();
                // Display the serialized object graph
                WriteLine(File.ReadAllText(filePath));
            }
        }

        private static void WriteDeserializedShapes(string xmlFilePath)
        {
            if (File.Exists(xmlFilePath))
            {
                using (FileStream xmlStream = File.Open(xmlFilePath, FileMode.Open))
                {
                    var serializerXml = new XmlSerializer(typeof(List<Shape>));

                    List<Shape> loadedShapesXml = serializerXml.Deserialize(xmlStream) as List<Shape>;
                    foreach (Shape item in loadedShapesXml)
                    {
                        WriteLine($" {item.GetType().Name} is {item.Color} and has an area of { item.Area } ");
                    }
                }

                //File.Delete(xmlFilePath);
            }
            else
            {
                WriteLine($"File {xmlFilePath} does not exist !!");
            }
        }
    }
}
