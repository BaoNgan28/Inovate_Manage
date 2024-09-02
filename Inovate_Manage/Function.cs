using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Add_StudentToList();
        }
        public void Add_StudentToList()
        {
            var data = Data.Gi();
            Console.Clear();
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            uint ID;
            while (true)
            {
                Terminal.Print("NHẬP ID SINH VIÊN (10 chữ số): ", 45, 2);
                Console.SetCursorPosition(45, 3);

                string idInput = Console.ReadLine();
                if (uint.TryParse(idInput, out ID) && idInput.Length == 10)
                {
                    Console.SetCursorPosition(0, 4);
                    Console.Write(new string(' ', Console.WindowWidth));
                    break;
                }
                else
                {
                    // ID không hợp lệ
                    Terminal.Print("Nhập chưa đúng. Xoá và nhập lại!", 0, 4);
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


            Terminal.Print("GPA :", 45, 12);
            Console.SetCursorPosition(45, 13);
            double Gpa = Convert.ToDouble(Console.ReadLine());
            Student sv = new Student(ID, Name, Gt, Age, cLASS, Gpa);
            Data.Gi().Students.Add(sv);
        }
    }
}
