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
                Terminal.Print(symbol + " " + percent + "%", col, row, ConsoleColor.Green); // In ra với phần trăm
                Thread.Sleep(120);
            }
        }
        //di chuyển lên xuống
        public static int HandleKey<T>(IEnumerable<T> array, int position)
        {
            var list = array.ToList();
            ConsoleKeyInfo key = Console.ReadKey(true);
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
            if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Backspace)
            {
                return -1;
            }
            return position;
        }

        //di chuyển (trái phải viết trong hàm luôn)
        public static void Move(string[] solution, string[] function, List<Student> student, List<Teacher> teacher)
        {
            var data = Data.Gi();
            data.Add_Data();
            while (true)
            {
                Console.Clear();
                Terminal.gi().ShowMenuSolution();
                data.pSolution = HandleKey(data.Solution, data.pSolution);

                switch (data.pSolution)
                {
                    case 0: //nếu pSolution = 0 hiển thị ds giảng viên

                        break;

                    case 1: //nếu pSolution = 1 hiển thị ds sinh viên
                        break;

                    case 2: //nếu pSolution = 2 hiển thị ds tuỳ chỉnh
                        break;

                    case 3: //pSolution = 3 thì hiển thị Stop
                        break;
                }
            }
        }
        public void MainLoop()
        {
            while (IsRunning)
            {
                Console.Clear();
                var data = Data.Gi();
                data.Add_Data();
                Terminal.gi().ShowMenuSolution();
                data.pSolution = HandleKey(data.Solution, data.pSolution);
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
                else
                {
                    stop();
                }
            }
        }
    }
}
