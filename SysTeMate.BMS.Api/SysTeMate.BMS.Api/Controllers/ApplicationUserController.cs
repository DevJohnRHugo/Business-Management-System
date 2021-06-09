using Microsoft.AspNetCore.Authorization;
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
    [Route("api/account")]
    [ApiController]
    public class ApplicationUserController : ApiController
    {

        public ApplicationUserController()
        {

        }

        [HttpPost("create")]
        public async Task<ActionResult<ApplicationUserVm>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("update")]
        public async Task<ActionResult<ApplicationUserVm>> Update(UpdateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ApplicationUserVm>> Delete(DeleteUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
