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
using EasyWakeOnLan;
using Microsoft.Owin.BuilderProperties;
using Newtonsoft.Json;

namespace CpuOffOnWithPi.WebAPI.Controllers.API
{
    public class CpuController : ApiController
    {
        // POST API/Cpu/TurnOn
        [HttpPost]
        public string TurnOn(CpuOnParameters parameters) {
            string macAddress = parameters.MacAddress;
            //Instance the class
            EasyWakeOnLanClient wakeOnLanClient = new EasyWakeOnLanClient();
            //Wake the remote PC
            wakeOnLanClient.Wake(macAddress);

            return "SUCCESS " + parameters.IpAddress;
        }

        public class CpuOnParameters
        {
            public string IpAddress { get; set; }
            public string MacAddress { get; set; }
        }
        
        // POST API/Cpu/TurnOff
        [HttpPost]
        public string TurnOff(CpuOffParameters parameters) {
            var values = new Dictionary<string, string> {
                { "noname", "nothing" }
            };

            var content = new FormUrlEncodedContent(values);
            var cpuUrl = $"http://{parameters.IpAddress}/API/Values/Shutdown";
            var response = CpuOffOnWithPi.WebAPI.Program.SingleWebClient.GetAsync(cpuUrl).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            return "SUCCESS " + responseString;
        }

        public class CpuOffParameters
        {
            public string IpAddress { get; set; }
            public string MacAddress { get; set; }
        }


        [HttpPost]
        public HttpResponseMessage GetCpus() {
            var cpus = ReadCpuConfig();
            var s = JsonConvert.SerializeObject(cpus);
            var r = new HttpResponseMessage();
            r.Content = new StringContent(s, Encoding.UTF8, "application/json");
            return r;
        }

        private List<CpuConfig> ReadCpuConfig() {
            var cpus = new List<CpuConfig>();
            var rawCpuCount = ConfigurationManager.AppSettings["CpuConfigCount"];
            var cpuCountNbr = 0;
            if (string.IsNullOrEmpty(rawCpuCount) || !int.TryParse(rawCpuCount, out cpuCountNbr))
                return cpus;

            for (var i = 1; i <= cpuCountNbr; ++i) {
                var rawInfo = ConfigurationManager.AppSettings["CpuConfig" + i.ToString()];
                if (string.IsNullOrEmpty(rawInfo))
                    continue;
                var rawSplit = rawInfo.Split(new string[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
                var cpu = new CpuConfig();
                cpu.FriendlyName = rawSplit[0];
                cpu.MachineName = rawSplit[1];
                cpu.IpAddress = rawSplit[2];
                cpu.MacAddress = rawSplit[3];
                cpus.Add(cpu);
            }

            return cpus;
        }

        public class CpuConfig
        {
            public string FriendlyName { get; set; }
            public string MachineName { get; set; }
            public string IpAddress { get; set; }
            public string MacAddress { get; set; }
        }
        

        // other ideas: CPU usage, memory usage, disk usage, top 10 program usage by CPU or memory, etc
    } 
}
