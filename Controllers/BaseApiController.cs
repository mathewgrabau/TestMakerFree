using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMakerFreeWebApp.Data;

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        protected ApplicationDbContext Context;
        protected  JsonSerializerSettings JsonSettings { get; private set; }

        public BaseApiController(ApplicationDbContext context)
        {
            Context = context;

            JsonSettings = new JsonSerializerSettings {Formatting = Formatting.Indented};
        }
    }
}
