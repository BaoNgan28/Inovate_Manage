using System;
using System.Collections.Generic;
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
        //in tiêu đề console
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
        //in theo vị trí
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
        //Hiệu ứng đánh chữ
        public static void EffectPrint(string s, int x, int y, ConsoleColor color = ConsoleColor.White, int time = 100)
        {
            string current = "";
            foreach (char c in current)
            {
                current += c;
                Print(s, x, y, color);
                Thread.Sleep(time);
            }
        }
        //hiệu ứng đánh chữ random màu
        public static void EffectRandome(string s, int x, int y, int time = 100)
        {

            ConsoleColor[] color = { ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.Green,
                                     ConsoleColor.DarkBlue, ConsoleColor.DarkYellow};
            Random random = new Random();
            for (int i = 0; i < s.Length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                int index = random.Next(color.Length);
                Console.ForegroundColor = color[index];
                Console.Write(s[i]);
                Thread.Sleep(time);
            }
        }
        //hiện menu lựa chọn
        public void ShowMenuSolution()
        {
            for (int i = 0; i < Data.Gi().Solution.Length; i++)
            {
                if (i == Data.Gi().pSolution)
                {
                    Print($"<>{Data.Gi().Solution[i]}<>", 2, i + 2, ConsoleColor.Red);
                }
                else
                    Print(Data.Gi().Solution[i], 2, i + 2, ConsoleColor.Green);
            }
        }
        public void ShowTitleStudent()
        {
            for (int i = 0; i < Data.Gi().Title_Student.Length; i++)
            {
                Print(Data.Gi().Title_Student[i], 29 + (i * 15), 12, ConsoleColor.Cyan);
            }
        }
        public void ShowTitleTeacher()
        {
            for (int i = 0; i < Data.Gi().Title_Teacher.Length; i++)
            {
                Print(Data.Gi().Title_Teacher[i], 40 + (i * 15), 12, ConsoleColor.Cyan);
            }
        }
        //hiện list giảng viên
        public void ShowMenuTeacher(List<Teacher> teachers)
        {
            ShowTitleTeacher();
            Terminal.Print("DANH SÁCH GIẢNG VIÊN", 20, 1, ConsoleColor.Cyan);
            for (int i = 0; i < teachers.Count; i++)
            {
                Print(teachers[i].FullName, sizeX / 2 - 30, i + 2, ConsoleColor.White);
                if (i == Data.Gi().pTeacher)
                {
                    Print($"<> {teachers[i].FullName} <>", sizeX / 2 - 30, i + 2, ConsoleColor.Red);
                    Print(teachers[i].FullName, 55, 10, ConsoleColor.DarkRed);
                    Print(teachers[i].Id.ToString(), 40 , 13, ConsoleColor.White);
                    Print(teachers[i].Gender.ToString(), 55 , 13, ConsoleColor.White);
                    Print(teachers[i].Subject, 70 , 13, ConsoleColor.White);
                }
            }
        }
        //hiện list sinh viên
        public void ShowMenuStudent(List<Student> students)
        {
            ShowTitleStudent();
            for (int i = 0; i < students.Count; i++)
            {
                Print(students[i].FullName, sizeX / 2 - 30, i + 2, ConsoleColor.White);
                if (Data.Gi().pStudent == i)
                {
                    Print($"<> {students[i].FullName} <>", sizeX / 2 - 30, i + 2, ConsoleColor.Red);
                    Print(students[i].FullName, 50,10, ConsoleColor.DarkRed);
                    Print(students[i].Id.ToString(), 29 , 13, ConsoleColor.White);
                    Print(students[i].Gender.ToString(), 47, 13, ConsoleColor.White);
                    Print(students[i].Age.ToString(), 60, 13, ConsoleColor.White);
                    Print(students[i].Class, 73, 13, ConsoleColor.White);
                    Print(students[i].GPA.ToString(), 89, 13, students[i].Passing() ? ConsoleColor.Green : ConsoleColor.Red);
                }
            }
        }
        //hiện các chức năng
        public void ShowMenuFunction()
        {
            for (int i = 0; i < Data.Gi().Function.Length; i++)
            {
                Print(Data.Gi().Function[i], sizeX / 2 - 30, i + 2, ConsoleColor.White); //sửa lại vị trí
                if(i == Data.Gi().pFunction)
                {
                    Print($"<> {Data.Gi().Function[i]} <>", sizeX / 2 - 30, i + 2, ConsoleColor.Red);
                }
            }
        }

    }
}
