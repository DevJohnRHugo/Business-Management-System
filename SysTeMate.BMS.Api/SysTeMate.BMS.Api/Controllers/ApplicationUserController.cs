using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Domain.Constants;

namespace SysTeMate.BMS.Api.Controllers
{
    [Authorize(Roles = Role.CanManageUserAccounts)]
    [Route("api/account")]
    [ApiController]
    public class ApplicationUserController : ApiController
    {

        public ApplicationUserController()
        {

        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUserVm>> Register(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult<ApplicationUserVm>> SignIn(SignInUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("sign-out")]
        public async Task<ActionResult<ApplicationUserVm>> SignOut([FromBody] string userName)
        {
            return await Mediator.Send(new SignOutUserCommand { UserName = userName });
        }

        [HttpPut]
        public async Task<ActionResult<ApplicationUserVm>> Update(UpdateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult<ApplicationUserVm>> Delete(DeleteUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
