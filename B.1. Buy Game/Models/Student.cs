using System;
using System.Collections.Generic;

namespace BuyGame.Models
{
    public class Student
    {
        public string Name { get; internal set; }
        public double AvailableMoney { get; set; }
        public List<Student> Friends { get; set; }

        public Student(string name, double money)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AvailableMoney = money;
            Friends = new List<Student>();
        }
    }
}
