using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inovate_Manage
{

    public class Function
    {
        //thêm sinh viên
        private static Function gI;
        public static Function gi()
        {
            if (gI == null)
            {
                gI = new Function();
            }
            return gI;
        }
        public Function()
        {

        }
        public void Add_StudentToList()
        {
            var data = Data.Gi();
            data.Add_Data();
            Console.Clear();
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            uint ID;
            while (true)
            {
                Terminal.Print("NHẬP ID SINH VIÊN (10 chữ số): ", 45, 2);
                Console.SetCursorPosition(45, 3);

                string idInput = Console.ReadLine();
                if (uint.TryParse(idInput, out ID) && idInput.Length == 10 && FindStudentById(ID) == null)  //Tryparse: chuyển chuỗi vừa nhập thành kiểu dữ liệu của ID
                {
                    Console.SetCursorPosition(0, 4);
                    Console.Write(new string(' ', Console.WindowWidth));
                    break;
                }
                else
                {
                    //if (FindStudentById(ID) != null)
                    //{
                    //    Terminal.Print("TRÙNG ID. Xoá và nhập lại!", 0, 4);
                    //}
                    Terminal.Print("ID TRÙNG hoặc NHẬP SAI. Xoá và nhập lại!", 0, 4);
                    Console.SetCursorPosition(45, 4);
                }
            }

            string Name;
            bool check;
            int flag = 0;
            do
            {
                Terminal.Print("NHẬP HỌ TÊN: ", 45, 4);
                Console.SetCursorPosition(45, 5);
                Name = Console.ReadLine();
                string[] words = Name.Split(' ');
                check = true;
                foreach (var word in words)
                {
                    if (!char.IsUpper(word[0]))
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    if (flag == 1)
                    {
                        Console.SetCursorPosition(0, 6);
                        Console.Write(new string(' ', Console.WindowWidth));
                    }

                    break;
                }
                else
                {
                    flag = 1;
                    Terminal.Print("Các từ bắt đầu bằng chữ in hoa. Hãy xoá và nhập lại", 0, 6);
                    Console.SetCursorPosition(45, 5);
                }
            } while (check == false);

            Terminal.Print("GIỚI TÍNH: ", 45, 6);
            Console.SetCursorPosition(45, 7);
            string Gt = Console.ReadLine();

            Terminal.Print("TUỔI: ", 45, 8);
            Console.SetCursorPosition(45, 9);
            int Age = Convert.ToInt32(Console.ReadLine());

            Terminal.Print("LỚP HỌC: (23DTHA + LỚP BẠN HỌC): ", 45, 10);
            Console.SetCursorPosition(45, 11);
            string cLASS = Console.ReadLine();

            double Gpa;
            do
            {
                Terminal.Print("GPA :", 45, 12);
                Console.SetCursorPosition(45, 13);
                Gpa = Convert.ToDouble(Console.ReadLine());
                if (Gpa >= 0 && Gpa <= 4.0)
                {
                    Console.SetCursorPosition(0, 13);
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                    break;
                }
                else
                {
                    Terminal.Print("Nhập sai. Hãy nhập lại!", 0, 13);
                    Console.SetCursorPosition(45, 13);
                }
            } while (true);
            bool isAdded = data.AddStudent(ID, Name, Gt, Age, cLASS, Gpa);
            Console.Clear();
            Handle.LoaderSpinner(13, 55);
            Thread.Sleep(100);
            Console.Clear();
            Terminal.Print("HOÀN THÀNH! ẤN PHÍM BẤT KÌ ĐỂ XEM DANH SÁCH", 35, 15, ConsoleColor.Red);
            ConsoleKeyInfo key = Console.ReadKey();
            Console.Clear();
            data.pSolution = 1;
            data.pStudent = Handle.HandleKey(data.Students, data.pStudent, key);
            Terminal.gi().ShowMenuSolution();
            Terminal.gi().ShowMenuStudent(data.Students);
        }
        //Sửa thông tin sinh viên
        public Student FindStudentById(uint id)
        {
            var data = Data.Gi();
            return data.Students.FirstOrDefault(s => s.Id == id);
        }
        //sửa lại nếu enter thì sửa id theo position
        public void ChooseId(ConsoleKeyInfo key)
        {
            var data = Data.Gi();
            data.Add_Data();
            Console.Clear();
            data.pSolution = 1;
            data.pStudent = Handle.HandleKey(data.Students, data.pStudent, key);
            Terminal.gi().ShowMenuStudent(data.Students);
            if (data.pFunction == 1)
            {
                Terminal.Print_Random("DI CHUYỂN LÊN XUỐNG ĐỂ CHỌN SINH VIÊN CẦN SỬA", 3, data.Students.Count + 11);
            }
            else if (data.pFunction == 2)
            {
                Terminal.Print_Random("DI CHUYỂN LÊN XUỐNG ĐỂ CHỌN SINH VIÊN CẦN XOÁ", 3, data.Students.Count + 11);
            }

        }
        public void ChangeStudent()
        {
            var data = Data.Gi();
            var svSelected = data.Students[data.pStudent];
            Terminal.Print("NHẬP THÔNG TIN CẦN SỬA", 3, data.Students.Count + 12);
            Terminal.Print("NHẬP HỌ TÊN SINH VIÊN: ", 3, data.Students.Count + 13);
            Console.SetCursorPosition(26, data.Students.Count + 13);
            string name = Console.ReadLine();
            if (name != null)
            {
                svSelected.FullName = name;
            }

            Terminal.Print("NHẬP GIỚI TÍNH: ", 3, data.Students.Count + 14);
            Console.SetCursorPosition(19, data.Students.Count + 14);
            string gt = Console.ReadLine();
            if (gt != null)
            {
                svSelected.Gender = gt;
            }

            Terminal.Print("NHẬP TUỔI: ", 3, data.Students.Count + 15);
            Console.SetCursorPosition(14, data.Students.Count + 15);
            int age = Convert.ToInt32(Console.ReadLine());
            if (age != 0)
            {
                svSelected.Age = age;
            }

            Terminal.Print("NHẬP LỚP: ", 3, data.Students.Count + 16);
            Console.SetCursorPosition(14, data.Students.Count + 16);
            string Class = Console.ReadLine();
            if (Class != null)
            {
                svSelected.Class = Class;
            }

            Terminal.Print("NHẬP GPA: ", 3, data.Students.Count + 17);
            Console.SetCursorPosition(13, data.Students.Count + 17);
            double gpa = Convert.ToDouble(Console.ReadLine());
            do
            {
                if (gpa >= 0 && gpa <= 4.0)
                {
                    svSelected.GPA = gpa;
                    Console.SetCursorPosition(3, data.Students.Count + 18);
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                    break;
                }
                else
                {
                    Terminal.Print("Nhập sai. Hãy nhập lại!", 3, data.Students.Count + 18);
                    Console.SetCursorPosition(12, data.Students.Count + 17);
                }
            } while (true);
            Console.Clear();
            Handle.LoaderSpinner(13, 55);
            Console.Clear();
            Thread.Sleep(100);
            Console.Clear();
            Terminal.gi().ShowMenuStudent(data.Students);
        }
        //hàm xoá
        public void RemoveStudent()
        {
            var data = Data.Gi();
            var sv = data.Students[data.pStudent];
            if (sv != null)
            {
                data.Students.Remove(sv);
                Console.Clear();
                Handle.LoaderSpinner(13, 55);
                Thread.Sleep(100);
                Console.Clear();
                Terminal.gi().ShowMenuStudent(data.Students);
            }
            else
            {
                Console.Clear();
                Terminal.Print("ID không hợp lệ", 3, data.Students.Count + 15);
            }
        }
    }
}
