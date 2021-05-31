using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;

namespace SysTeMate.BMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ApiController
    {

        public ApplicationUserController()
        {

        }

        [HttpPost]
        public async Task<ActionResult<ApplicationUserVm>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
