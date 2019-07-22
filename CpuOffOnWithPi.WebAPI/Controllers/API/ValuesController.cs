﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Terminate() {
            Program.shutDown.Set(); // from https://stackoverflow.com/a/17542760/7656
            return "TERMINATE CMD";
        }
    } 
}
