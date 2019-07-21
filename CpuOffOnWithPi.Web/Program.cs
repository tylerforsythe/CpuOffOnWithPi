using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace CpuOffOnWithPi.Web
{
    class Program
    {
        public static System.Threading.ManualResetEvent shutDown = new ManualResetEvent(false);

        static void Main(string[] args) {
            string baseUrl = @"http://*:" + ConfigurationManager.AppSettings["OwinHostPortNumber"];

            var startupOptions = new StartOptions(baseUrl) {
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };
            using (WebApp.Start<WebOwinStartup>(startupOptions)) {
                // if we're on my personal dev machine, launch a web-browser for quick testing.
                //if (Environment.MachineName.ToLower() == "gtx")
                //    LaunchWebBrowserTest();
                //Console.WriteLine("Press Enter to quit.");
                //Console.ReadKey();
                shutDown.WaitOne(); // this strategy pulled from https://stackoverflow.com/a/17542760/7656
            }
        }

        static void LaunchWebBrowserTest() {
            var url = "http://localhost:" + ConfigurationManager.AppSettings["OwinHostPortNumber"] + "/";
            Process.Start("chrome.exe", url);
        }
    }
}
