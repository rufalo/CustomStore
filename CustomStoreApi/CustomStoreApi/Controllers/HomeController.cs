using CustomStoreApi.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.Controllers
{
    public class HomeController : ControllerBase
    {
        ApplicationContext _Context;
        public HomeController(ApplicationContext context)
        {
            _Context = context;
        }
        public IActionResult Index(int id = 5)
        {
            return Ok(id);
        }

        public int Test()
        {
            return 20;
        }
    }
}
