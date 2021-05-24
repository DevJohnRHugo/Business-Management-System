using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysTeMate.BMS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {

        }


        public IActionResult GetAccounts()
        {
            return Ok();
        }


        public IActionResult GetAccount(int id) 
        {
            return Ok();
        }


        public IActionResult SignUp()
        {
            return Ok();
        }


        public IActionResult SignIn()
        {
            return Ok();
        }


        public IActionResult Create()
        {
            return Ok();
        }


        public IActionResult Update()
        {
            return Ok();
        }


        public IActionResult Remove()
        {
            return Ok();
        }
    }
}
