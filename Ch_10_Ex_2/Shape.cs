using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace Ch_10_Ex_2
{
    [Serializable]
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    public class Shape
    {
        public string Color { get; set; }
        public virtual double Area { get; }
    }

    [XmlRoot("Circle")]
    public class Circle : Shape
    {
        [XmlElement("rad")]
        public double Radius { get; set; }

        [XmlElement("arrrr")]
        public override double Area
        {
            get
            {
                return 2 * Math.PI * Radius;
            }
        }
    }

    public class Rectangle : Shape
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public override double Area
        {
            get
            {
                return Height * Width;
            }
        }
    }

    public class Person
    {
        public Person()
        {

        }

        public Person(decimal initialSalary)
        {
            Salary = initialSalary;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public HashSet<Person> Children { get; set; }
        protected decimal Salary { get; set; }
    }
}
