using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inovate_Manage
{
    public class Terminal
    {
        private Terminal() { }
        private static Terminal gI;
        private static readonly object gILock = new object();
        private static readonly object _lock = new object();
        public static Terminal gi()
        {
            lock (gILock)
            {
                if (gI == null)
                {
                    gI = new Terminal();
                }
            }
            return gI;
        }
        public static int sizeX { get; set; } = 100;
        public static int sizeY { get; set; } = 20;

        // In tiêu đề console
        public void SetTitle(string title, int time = 75)
        {
            string currentTitle = "";
            foreach (char c in title)
            {
                currentTitle += c;
                Console.Title = currentTitle;
                Thread.Sleep(time);
            }
        }

        // In theo vị trí
        public static void Print(string s, int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            lock (_lock)
            {
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = color;
                Console.WriteLine(s);
                Console.ResetColor();
                Console.SetCursorPosition(0, sizeY);
            }
        }
        public static void Print_Random(string s, int x, int y, int minX = 0, int maxX = 0, int time = 200)
        {
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.Green,
                              ConsoleColor.DarkBlue, ConsoleColor.DarkYellow, ConsoleColor.Cyan, ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.DarkMagenta };
            Random random = new Random();
            int direction = 1;

            if (maxX == 0) maxX = Console.WindowWidth;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    break;
                }
                Console.SetCursorPosition(x, y);
                Console.Write(new string(' ', s.Length));

                Console.ForegroundColor = colors[random.Next(colors.Length)];

                // Cập nhật vị trí x
                x += direction;

                if (x <= minX || x >= maxX - s.Length)
                {
                    direction *= -1;
                }

                // In dòng chữ tại vị trí mới
                Console.SetCursorPosition(x, y);
                Console.Write(s);

                Thread.Sleep(time);
                Console.ResetColor();
            }
        }


        // Hiệu ứng đánh chữ
        public static void EffectPrint(string s, int x, int y, ConsoleColor color = ConsoleColor.White, int time = 100)
        {
            string current = "";
            foreach (char c in s)
            {
                current += c;
                Print(current, x, y, color);
                Thread.Sleep(time);
            }
        }

        // Hiệu ứng đánh chữ random màu
        public static void EffectRandome(string s, int x, int y, int time = 100)
        {
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.Green,
                                      ConsoleColor.DarkBlue, ConsoleColor.DarkYellow };
            Random random = new Random();
            for (int i = 0; i < s.Length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                int index = random.Next(colors.Length);
                Console.ForegroundColor = colors[index];
                Console.Write(s[i]);
                Thread.Sleep(time);
            }
        }
        public void Delete_Key(string item, int x, int y)
        {
            Print(new string(' ', item.Length + 10), x, y); 
        }
        // Hiện menu lựa chọn
        public void ShowMenuSolution()
        {
            var data = Data.Gi(); 
            for (int i = 0; i < data.Solution.Length; i++)
            {
                if (i == data.pSolution)
                {
                    Print($"<>{data.Solution[i]}<>", 2, i + 2, ConsoleColor.Red);
                }
                else
                {
                    Delete_Key(data.Solution[i], 2, i + 2); ///xoá in đè
                    Print(data.Solution[i], 2, i + 2, ConsoleColor.Green);
                }
            }
        }

        public void ShowTitleStudent()
        {
            var data = Data.Gi();
            for (int i = 0; i < data.Title_Student.Length; i++)
            {
                Print(data.Title_Student[i], 29 + (i * 15), 12, ConsoleColor.Cyan);
            }
        }

        public void ShowTitleTeacher()
        {
            var data = Data.Gi();
            for (int i = 0; i < data.Title_Teacher.Length; i++)
            {
                Print(data.Title_Teacher[i], 35 + (i * 15), 12, ConsoleColor.Cyan);
            }
        }

        // Hiện list giảng viên
        public void ShowMenuTeacher(List<Teacher> teachers)
        {
            var data = Data.Gi();
            ShowTitleTeacher();
            Terminal.Print("DANH SÁCH GIẢNG VIÊN", 20, 1, ConsoleColor.Cyan);
            for (int i = 0; i < teachers.Count; i++)
            {
                Delete_Key(teachers[i].FullName,sizeX / 2 - 30, i + 2);
                Print(teachers[i].FullName, sizeX / 2 - 30, i + 2, ConsoleColor.White);
                if (i == data.pTeacher)
                {
                    Print($"<> {teachers[data.pTeacher].FullName} <>", sizeX / 2 - 30, data.pTeacher + 2, ConsoleColor.Red);
                    Delete_Key(teachers[data.pTeacher].FullName, 50, 10);
                    Print(teachers[data.pTeacher].FullName, 50, 10, ConsoleColor.DarkRed);
                    Print(teachers[data.pTeacher].Id.ToString(), 35, 13, ConsoleColor.White);
                    Delete_Key(teachers[data.pTeacher].Gender.ToString(), 50, 13);
                    Print(teachers[data.pTeacher].Gender.ToString(), 50, 13, ConsoleColor.White);
                    Delete_Key(teachers[data.pTeacher].Subject, 65, 13);
                    Print(teachers[data.pTeacher].Subject, 65, 13, ConsoleColor.White);
                }
            }
        }

        // Hiện list sinh viên
        public void ShowMenuStudent(List<Student> students)
        {

            var data = Data.Gi();
            Terminal.Print("DANH SÁCH SINH VIÊN", 20, 1, ConsoleColor.Cyan);
            ShowTitleStudent();
            for (int i = 0; i < students.Count; i++)
            {
                Delete_Key(students[i].FullName, sizeX / 2 - 30, i + 2);
                Print(students[i].FullName, sizeX / 2 - 30, i + 2, ConsoleColor.White);
                if (i == data.pStudent)
                {
                    Print($"<> {students[data.pStudent].FullName} <>", sizeX / 2 - 30, data.pStudent + 2, ConsoleColor.Red);
                    Delete_Key(students[data.pStudent].FullName, 50, 10);
                    Print(students[data.pStudent].FullName, 50, 10, ConsoleColor.DarkRed);
                    Print(students[data.pStudent].Id.ToString(), 29, 13, ConsoleColor.White);
                    Delete_Key(students[data.pStudent].Gender.ToString(), 47, 13);
                    Print(students[data.pStudent].Gender.ToString(), 47, 13, ConsoleColor.White);
                    Print(students[data.pStudent].Age.ToString(), 60, 13, ConsoleColor.White);
                    Print(students[data.pStudent].Class, 73, 13, ConsoleColor.White);
                    Print(students[data.pStudent].GPA.ToString(), 89, 13, students[data.pStudent].Passing() ? ConsoleColor.Green : ConsoleColor.Red);
                }
            }
            
        }

        // Hiện các chức năng
        public void ShowMenuFunction()
        {
            var data = Data.Gi();
            for (int i = 0; i < data.Function.Length; i++)
            {
                Delete_Key(data.Function[i], sizeX / 2 - 30, i + 2);
                Print(data.Function[i], sizeX / 2 - 30, i + 2, ConsoleColor.White); // Sửa lại vị trí
                if (i == data.pFunction)
                {
                    Print($"<> {data.Function[i]} <>", sizeX / 2 - 30, i + 2, ConsoleColor.Red);
                }
            }
        }
    }
}
