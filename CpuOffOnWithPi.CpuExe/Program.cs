using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;

namespace CpuOffOnWithPi.CpuExe
{
    class Program
    {
        public static System.Threading.ManualResetEvent shutDown = new ManualResetEvent(false);

        static void Main(string[] args) {
            string baseUrl = @"http://*:" + ConfigurationManager.AppSettings["OwinHostPortNumber"];

            var startupOptions = new StartOptions(baseUrl) {
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };
            using (WebApp.Start<ExeOwinStartup>(startupOptions)) {
                //Console.WriteLine("Press Enter to quit.");
                //Console.ReadKey();
                shutDown.WaitOne(); // this strategy pulled from https://stackoverflow.com/a/17542760/7656
            }
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
