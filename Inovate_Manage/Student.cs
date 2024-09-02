using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovate_Manage
{
    public class Student : Person
    {
        public double GPA { get; set; }
        public string Class {  get; set; }
        public Student(uint id, string fullname, string gender, int age, string cLass, double gpa) 
        {  
            this.Id = id;
            this.FullName = fullname;
            this.Gender = gender;
            this.Age = age;
            this.Class = cLass;
            this.GPA = gpa;
        }
        public bool Passing()
        {
            return GPA >= 2.0;
        }
    }
}
