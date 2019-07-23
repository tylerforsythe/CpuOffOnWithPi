using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using CpuOffOnWithPi.WebAPI;
using Newtonsoft.Json;

namespace CpuOffOnWithPi.WebAPI.Controllers.API
{
    public class CpuController : ApiController
    {
        // POST API/Cpu/TurnOn
        [HttpPost]
        public string TurnOn(CpuOnParameters parameters) {
            // send wake-on-lan

            return "SUCCESS " + parameters.MachineName;
        }

        public class CpuOnParameters
        {
            public string MachineName { get; set; }
        }
        
        // POST API/Cpu/TurnOff
        [HttpPost]
        public string TurnOff(CpuOffParameters parameters) {
            var values = new Dictionary<string, string>
            {
                { "outletId", parameters.MachineName }
            };

            var content = new FormUrlEncodedContent(values);
            var response = CpuOffOnWithPi.WebAPI.Program.client.PostAsync("http://rpi2.local/lightswitch.php", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            return "SUCCESS " + responseString;
        }

        public class CpuOffParameters
        {
            public string MachineName { get; set; }
        }


        [HttpPost]
        public HttpResponseMessage GetCpus() {
            var cpus = ReadCpuConfig();
            var s = JsonConvert.SerializeObject(cpus);
            var r = new HttpResponseMessage();
            r.Content = new StringContent(s, Encoding.UTF8, "application/json");
            return r;
        }

        private List<CpuInfo> ReadCpuConfig() {
            var cpus = new List<CpuInfo>();
            var rawCpuCount = ConfigurationManager.AppSettings["CpuInfoCount"];
            var cpuCountNbr = 0;
            if (string.IsNullOrEmpty(rawCpuCount) || !int.TryParse(rawCpuCount, out cpuCountNbr))
                return cpus;

            for (var i = 1; i <= cpuCountNbr; ++i) {
                var rawInfo = ConfigurationManager.AppSettings["CpuInfo" + i.ToString()];
                if (string.IsNullOrEmpty(rawInfo))
                    continue;
                var rawSplit = rawInfo.Split(new string[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
                var cpu = new CpuInfo();
                cpu.FriendlyName = rawSplit[0];
                cpu.MachineName = rawSplit[1];
                cpu.IpAddress = rawSplit[2];
                cpu.MacAddress = rawSplit[3];
                cpus.Add(cpu);
            }

            return cpus;
        }

        public class CpuInfo
        {
            public string FriendlyName { get; set; }
            public string MachineName { get; set; }
            public string IpAddress { get; set; }
            public string MacAddress { get; set; }
        }
        

        // other ideas: CPU usage, memory usage, disk usage, top 10 program usage by CPU or memory, etc
    } 
}
