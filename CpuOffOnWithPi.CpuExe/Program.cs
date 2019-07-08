using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpuOffOnWithPi.CpuExe
{
    class Program
    {
        static void Main(string[] args) {
        }

        /// <summary>
        /// On Windows, this will require the user to be an admin!
        /// </summary>
        public static void ExecuteShutdown() {
            Process.Start(new ProcessStartInfo("shutdown", "/s /t 0") {
                CreateNoWindow = true, UseShellExecute = false
            });
        }
    }
}
