using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CpuOffOnWithPi.SelfUpdate
{
    class Program
    {
        static void Main(string[] args) {
            //foreach (var a in args) // Environment.GetCommandLineArgs()
            //    Console.WriteLine(a);
            //return;

            if (args == null || args.Length < 3) {
                Console.WriteLine("More parameters required!");
                return;
            }

            var i = 0;
            // THIS IS INCREDIBLY INSECURE! But it's only running on my personal machines, internally, so who cares.
            var sourceDirectory = args[i++];
            var targetDirectory = args[i++];
            var launchPathWhenDone = args[i++];

            Thread.Sleep(2 * 1000); // wait 2 seconds for the caller to completely close and locks to release.

            Copy(sourceDirectory, targetDirectory);
            Delete(sourceDirectory);

            
            Process p = new Process();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                p.StartInfo.FileName = $"{launchPathWhenDone}";
                p.StartInfo.Arguments = "";
                p.StartInfo.Verb = "runas";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                p.StartInfo.FileName = "mono";
                p.StartInfo.Arguments = $"{launchPathWhenDone}";
            }

            p.Start();
        }


        // Both copy methods from https://stackoverflow.com/a/690980/7656
        public static void Copy(string sourceDirectory, string targetDirectory) {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target) {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles()) {
                //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each sub-directory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories()) {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static void Delete(string sourceDirectory) {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);

            DeleteAll(diSource);
        }

        public static void DeleteAll(DirectoryInfo source) {
            // delete each file
            foreach (FileInfo fi in source.GetFiles()) {
                //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.Delete();
            }

            // Copy each sub-directory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories()) {
                DeleteAll(diSourceSubDir);
            }
        }
    }
}
