using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace CpuOffOnWithPi.WebAPI
{
    class Program
    {
        public static System.Threading.ManualResetEvent shutDown = new ManualResetEvent(false);
        public static readonly HttpClient SingleWebClient = new HttpClient();
        public static string LogPath = @"log.txt";

        static void Main(string[] args) {
            string baseUrl = @"http://*:" + ConfigurationManager.AppSettings["OwinHostPortNumber"];

            var startupOptions = new StartOptions(baseUrl) {
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };
            using (WebApp.Start<WebOwinStartup>(startupOptions)) {
                //Console.WriteLine("Press Enter to quit.");
                //Console.ReadKey();
                //Log($"Startup!");
                shutDown.WaitOne(); // this strategy pulled from https://stackoverflow.com/a/17542760/7656
                Thread.Sleep(300);
            }
        }

        public static void Log(string message) {
            var l = new List<string>();
            l.Add($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}  {message}");
            l.Add(Environment.NewLine);
            File.AppendAllLines(LogPath, l);
        }
    }
}
