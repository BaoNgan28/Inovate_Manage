using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Inovate_Manage
{
    public class Data
    {
        //constructor
        private Data() { }
        private static Data gI;
        private readonly static object gILock = new object();
        public static Data Gi()
        {
            lock (gILock)
            {
                if (gI == null)
                {
                    gI = new Data();
                }
            }
            return gI;
        }
        //Tiêu đề
        public string[] Solution = { "Giảng Viên", "Học Sinh", "Tuỳ Chỉnh", "Thoát" };
        public string[] Title_Student = { "ID", "Giới Tính", "Tuổi", "Lớp", "GPA" };
        public string[] Title_Teacher = { "ID", "Giới Tính", "Môn Dạy" };
        public string[] supChucnang = { "DANH SÁCH GIẢNG VIÊN", "DANH SÁCH HỌC SINH" };
        public string[] Function = { "THÊM", "SỬA", "XOÁ" };
        public int pTeacher = 0;
        public int pStudent = 0;
        public int pSolution = 0;
        public int pFunction = 0;
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
        //tạo data mẫu
        public void Add_Data()
        {
            //data giảng viên
            Teachers = new List<Teacher>()
            {
                new Teacher (1, "VÕ TẤN DŨNG", "NAM", "CSDL & GIẢI THUẬT"),
                new Teacher(2, "NGUYỄN ĐẮC DZỰ TRÌNH", "NAM", "KĨ THUẬT LẬP TRÌNH"),
                new Teacher(3, "HÀN MINH CHÂU", "NAM", "NHẬP MÔN HỆ ĐIỀU HÀNH")
            };
            //data sinh vien
            Students = new List<Student>
            {
                new Student(2380601424, "NGUYỄN THANH BẢO NGÂN", "NỮ", 19, "23DTHA5", 3.9),
                new Student(2380601806, "NGUYỄN CÔNG QUANG", "NAM", 21, "23DTHA6", 1.9),
                new Student(2380601640, "NGUYỄN TRƯỜNG PHÁT", "NAM", 19, "23DTHA7", 3.7),
                new Student(2380601465, "LÊ HUỲNH NGỌC", "NAM", 19, "23DTHA5", 2.6),
                new Student(2380602440, "HUỲNH NGỌC ANH TUẤN", "NAM", 19, "23DTHA6", 3.6)
            };
        }
        public Data(List<Student> students, List<Teacher> teachers)
        {
            this.Students = students;
            this.Teachers = teachers;
        }

        //Lấy thông tin sinh viên
        public uint[] GetStudentIds()
        {
            return Students.Select(s => s.Id).ToArray();
        }
        public string[] GetStudentNames()
        {
            return Students.Select(s => s.FullName).ToArray();
        }
        public int[] GetStudentAges()
        {
            return Students.Select(s => s.Age).ToArray();
        }
        public string[] GetStudentClasses()
        {
            return Students.Select(s => s.Class).ToArray();
        }
        public double[] GetStudentGPAs()
        {
            return Students.Select(s => s.GPA).ToArray();
        }
        //Lấy thông tin Giảng viên
        public uint[] GetTeacherIds()
        {
            return Teachers.Select(t => t.Id).ToArray();
        }
        public string[] GetTeachersNames()
        {
            return Teachers.Select(t => t.FullName).ToArray();
        }
        public string[] GetTeachersGender()
        {
            return Teachers.Select(t => t.Gender).ToArray();
        }
        public string[] GetTeacherSubject()
        {
            return Teachers.Select(t => t.Subject).ToArray();
        }
        //thêm thông tin cho sinh vien
        public bool AddStudent(uint id, string fullname, string gender, int age, string cLass, double GPA)
        {
            try
            {
                Students.Add(new Student(id, fullname, gender, age, cLass, GPA));
                return true;
            }
            catch (Exception ex)
            {
                Terminal.EffectPrint("Lỗi tại AddStudent \n" + ex.ToString(), 0, Terminal.sizeY);
                return false;
            }
        }
    }
}

