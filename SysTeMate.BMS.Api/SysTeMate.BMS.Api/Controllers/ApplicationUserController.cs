using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.Queries;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Domain.Constants;
using SysTeMate.BMS.Infrastructure.Models;

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
        public async Task<ActionResult<ApplicationUserVm>> SignOut()
        {
            await Mediator.Send(new SignOutUserCommand());
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ApplicationUserListVm> GetUser(Guid id)
        {
            return await Mediator.Send(new GetApplicationUserQuery { Id = id });
        }

        [HttpGet("all")]
        public async Task<ApplicationUserListVm> GetUsers()
        {
            return await Mediator.Send(new GetApplicationUserQuery { Id = null });
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
