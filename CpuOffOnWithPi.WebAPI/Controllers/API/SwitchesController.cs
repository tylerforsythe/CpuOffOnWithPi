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
        public string SetSwitch(int switchNumber, string status) {
            var values = new Dictionary<string, string>
            {
                { "outletId", switchNumber.ToString() },
                { "outletStatus", status }
            };

            var content = new FormUrlEncodedContent(values);
            var response = CpuOffOnWithPi.WebAPI.Program.client.PostAsync("http://rpi2.local/lightswitch.php", content).Result;
            var responseString = response.Content.ReadAsStringAsync();

            return "SUCCESS " + responseString;
        }

        
        public string SetSwitchGet(int switchNumber, string status) {
            var values = new Dictionary<string, string>
            {
                { "outletId", switchNumber.ToString() },
                { "outletStatus", status }
            };

            var content = new FormUrlEncodedContent(values);
            var response = CpuOffOnWithPi.WebAPI.Program.client.PostAsync("http://rpi2.local/lightswitch.php", content).Result;
            var responseString = response.Content.ReadAsStringAsync();

            return "SUCCESS " + responseString;
        }

        
        [HttpPost]
        public string SetSwitchGetNoParam() {
            var values = new Dictionary<string, string>
            {
                { "outletId", "1" },
                { "outletStatus", "off" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = CpuOffOnWithPi.WebAPI.Program.client.PostAsync("http://rpi2.local/lightswitch.php", content).Result;
            var responseString = response.Content.ReadAsStringAsync();

            return "SUCCESS " + responseString;
        }
        

        // GET api/values/5 
        public string Get(int id) {
            return "Switches value " + id;
        }

        // other ideas: CPU usage, memory usage, disk usage, top 10 program usage by CPU or memory, etc
    } 
}
