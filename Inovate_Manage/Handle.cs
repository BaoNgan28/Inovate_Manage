using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inovate_Manage
{
    public class Handle
    {
        private static readonly Lazy<Handle> instance = new Lazy<Handle>(() => new Handle());
        public static Handle gI() => instance.Value;
        private bool IsRunning = true;

        public Handle()
        {
            var data = Data.Gi();
            data.Add_Data();
            MainLoop();
        }

        public void start()
        {
            Terminal.gi().SetTitle("Xin chào");
            Terminal.EffectRandome("Ấn Phím bất kỳ để bắt đầu", Terminal.sizeX / 2 - 3, Terminal.sizeY / 2 + 3, 55);
            Console.ReadKey();
        }

        public void stop()
        {
            Console.ReadKey();
            Console.Clear();
            Terminal.EffectRandome("Tạm Biệt", Terminal.sizeX / 2 + 5, Terminal.sizeY / 2, 30);
            Terminal.gi().SetTitle("Then Kiu bây bề đã chạy thử", 35);
            Console.SetCursorPosition(0, 0);
        }

        public static void LoaderSpinner(int row, int col)
        {
            string[] spinner = { "|", "/", "-", "\\" };
            for (int i = 1; i <= 10; i++)
            {
                int percent = i * 10;
                string symbol = spinner[i % spinner.Length];
                Terminal.Print(symbol + " " + percent + "%", col, row, ConsoleColor.Green);
                Thread.Sleep(120);
            }
        }

        // di chuyển lên xuống
        public static int HandleKey<T>(IEnumerable<T> array, int position, ConsoleKeyInfo key)
        {
            var list = array.ToList();
            if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
            {
                if (position == 0)
                {
                    position = list.Count;
                }
                position--;
            }
            if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
            {
                position++;
                if (position == list.Count)
                    position = 0;
            }
            return position;
        }

        // di chuyển trái phải
        private void ReadKey()
        {
            var data = Data.Gi();
            bool isRunRL = true;

            while (isRunRL)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (data.pSolution == 0)
                        {
                            data.pTeacher = HandleKey(data.Teachers, data.pTeacher, key);
                            Console.Clear();
                            Terminal.gi().ShowMenuSolution();
                            Terminal.gi().ShowMenuTeacher(data.Teachers);
                        }
                        else if (data.pSolution == 1)
                        {
                            data.pStudent = HandleKey(data.Students, data.pStudent, key);
                            Console.Clear();
                            Terminal.gi().ShowMenuSolution();
                            Terminal.gi().ShowMenuStudent(data.Students);
                        }
                        else if (data.pSolution == 2)
                        {
                            data.pFunction = HandleKey(data.Function, data.pFunction, key);
                            Console.Clear();
                            Terminal.gi().ShowMenuSolution();
                            Terminal.gi().ShowMenuFunction();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        isRunRL = false;
                        Console.Clear();
                        Terminal.gi().ShowMenuSolution();
                        break;

                    case ConsoleKey.UpArrow:

                    case ConsoleKey.DownArrow:
                        if (data.pSolution == 0)
                        {
                            data.pTeacher = HandleKey(data.Teachers, data.pTeacher, key);
                            Console.Clear();
                            Terminal.gi().ShowMenuSolution();
                            Terminal.gi().ShowMenuTeacher(data.Teachers);
                        }
                        else if (data.pSolution == 1)
                        {
                            data.pStudent = HandleKey(data.Students, data.pStudent, key);
                            Console.Clear();
                            Terminal.gi().ShowMenuSolution();
                            Terminal.gi().ShowMenuStudent(data.Students);
                        }
                        else if (data.pSolution == 2)
                        {
                            data.pFunction = HandleKey(data.Function, data.pFunction, key);
                            Console.Clear();
                            Terminal.gi().ShowMenuSolution();
                            Terminal.gi().ShowMenuFunction();
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (data.pSolution == 1)
                        {
                            //sửa thông tin khi enter vào tên trong ds sv
                            if(data.pFunction == 1)
                            { Function.gi().ChangeStudent(); }
                            else if(data.pFunction == 2)
                            {
                                Function.gi().RemoveStudent();
                            }
                        }
                        else if (data.pSolution == 2)
                        {
                            if (data.pFunction == 0)
                            {
                                Function.gi().Add_StudentToList();
                            }
                            else if (data.pFunction == 1)
                            {
                                Function.gi().ChooseId(key);
                            }
                            else
                            {
                                Function.gi().ChooseId(key);
                            }
                        }
                        break;
                    default:
                        isRunRL = false;
                        break;
                }
            }
        }


        public void MainLoop()
        {
            while (true)
            {
                Console.Clear();
                var data = Data.Gi();

                Terminal.gi().ShowMenuSolution();
                if (data.pSolution == 0)
                {
                    Terminal.gi().ShowMenuTeacher(data.Teachers);
                }
                else if (data.pSolution == 1)
                {
                    Terminal.gi().ShowMenuStudent(data.Students);
                }
                else if (data.pSolution == 2)
                {
                    Terminal.gi().ShowMenuFunction();
                }
                ConsoleKeyInfo key1 = Console.ReadKey();

                if (key1.Key == ConsoleKey.RightArrow || key1.Key == ConsoleKey.LeftArrow)
                {
                    ReadKey();
                }
                else if (key1.Key == ConsoleKey.Enter)
                {
                    ReadKey();
                }
                else
                {
                    data.pSolution = HandleKey(data.Solution, data.pSolution, key1);
                }
            }
        }
    }
}
