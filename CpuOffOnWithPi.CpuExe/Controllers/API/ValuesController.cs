using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CpuOffOnWithPi.CpuExe.Controllers.API
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get() {
            return new[] { "value1", "value2" };
        }

        // GET api/values/5 
        public string Get(int id) {
            return "value";
        }

        // GET api/values/shutdown
        [HttpGet]
        public string Shutdown() {
            Program.ExecuteShutdown();
            return "SHUTDOWN CMD";
        }

        // GET api/values/terminate
        [HttpGet]
        public string Terminate() {
            Program.shutDown.Set(); // from https://stackoverflow.com/a/17542760/7656
            return "TERMINATE CMD";
        }

        // other ideas: CPU usage, memory usage, disk usage, top 10 program usage by CPU or memory, etc
    } 
}
