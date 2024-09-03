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
            start();
            stop();
        }
        public void start()
        {
            Terminal.gi().SetTitle("Xin chào");
            Terminal.EffectRandome("Ấn Phím bất kỳ để bắt đầu", Terminal.sizeX / 2, Terminal.sizeY / 2, 55);
            Console.ReadKey();
        }
        public void stop()
        {
            Console.Clear();
            Terminal.EffectRandome("Tạm Biệt", Terminal.sizeX / 2 + 5, Terminal.sizeY / 2, 30);
            Terminal.gi().SetTitle("Then Kiu bây bề đã chạy thử", 35);
            Console.SetCursorPosition(0, 0);
        }
        public static void LoaderSpinner( int row, int col)
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

        //di chuyển trái phải
        //main loop

    }
}
