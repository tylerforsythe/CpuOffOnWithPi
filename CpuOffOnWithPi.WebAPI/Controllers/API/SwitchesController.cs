using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CpuOffOnWithPi.WebAPI;

namespace CpuOffOnWithPi.WebAPI.Controllers.API
{
    public class SwitchesController : ApiController
    {
        // POST API/Switches/SetSwitch
        [HttpPost]
        public string SetSwitch(SwitchParameters parameters) {
            var values = new Dictionary<string, string>
            {
                { "outletId", parameters.SwitchNumber.ToString() },
                { "outletStatus", parameters.Status }
            };

            var content = new FormUrlEncodedContent(values);
            var response = CpuOffOnWithPi.WebAPI.Program.client.PostAsync("http://rpi2.local/lightswitch.php", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            return "SUCCESS " + responseString;
        }

        public class SwitchParameters
        {
            public int SwitchNumber { get; set; }
            public string Status { get; set; }
        }
        

        // other ideas: CPU usage, memory usage, disk usage, top 10 program usage by CPU or memory, etc
    } 
}
