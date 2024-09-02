using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovate_Manage
{
    public class Teacher : Person
    {
        public string Subject { get; set; }
        public Teacher(uint id, string fullname, string gender, string subject)
        {
            this.Id = id;
            this.FullName = fullname;
            this.Gender = gender;
            this.Subject = subject;
        }
    }
}
