using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CpuOffOnWithPi.WebAPI;

namespace CpuOffOnWithPi.WebAPI.Controllers.API
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get() {
            return new[] { "value1", "value2" };
        }

        // GET api/values/5 
        public string Get(int id) {
            return "value" + id;
        }

        // GET api/values/terminate
        [HttpGet]
        public string Log() {
            var lines = File.ReadAllLines(Program.LogPath);
            for (var i = 0; i < lines.Length; ++i) {
                var l = lines[i];
                l = l.Replace(Environment.NewLine, "<br/>");
                lines[i] = l;
            }

            return string.Join("", lines);
        }
        

        // GET api/values/terminate
        [HttpGet]
        public string Terminate() {
            Program.shutDown.Set(); // from https://stackoverflow.com/a/17542760/7656
            return "TERMINATE CMD";
        }

        
        // POST API/values/Update
        [HttpGet]
        public string Update() {
            var thisAppPath = System.AppContext.BaseDirectory;
            //if (!thisAppPath.EndsWith(@"\") && !thisAppPath.EndsWith(@"/"))
            //    thisAppPath = thisAppPath;
            var pathToCheckForUpdate = ConfigurationManager.AppSettings["PathToCheckForUpdate"];
            if (!Directory.Exists(pathToCheckForUpdate))
                return "DIR NOT FOUND";

            var fileToCheck = Path.Combine(pathToCheckForUpdate, "UPDATE.txt");
            if (!File.Exists(fileToCheck))
                return "UPDATE FILE NOT FOUND";

            File.Delete(fileToCheck);

            var pathToCopyTool = ConfigurationManager.AppSettings["PathToUpdateCopyTool"];

            Process p = new Process();
            p.StartInfo.FileName = pathToCopyTool;
            p.StartInfo.Arguments = $"{pathToCheckForUpdate} {thisAppPath} {thisAppPath}CpuOffOnWithPi.WebAPI.exe";
            p.StartInfo.Verb = "runas";
            p.Start();
            Program.shutDown.Set();
            
            return "SUCCESS ";
        }
    } 
}
