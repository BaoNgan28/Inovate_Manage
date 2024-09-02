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
        public Handle() {
        }
        public static void LoaderSpinner( int row, int col)
        {
            string[] spinner = { "|", "/", "-", "\\" };
            for (int i = 1; i <= 10; i++)
            {
                int percent = i * 10;
                string symbol = spinner[i % spinner.Length];
                Terminal.Print(symbol + " " + percent + "%", col, row, ConsoleColor.Green); // In ra với phần trăm
                Thread.Sleep(120); // Giả lập công việc với sleep
            }
        }
        //di chuyển lên xuống
        //di chuyển trái phải
        //main loop

    }
}
