using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Identity;
using SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Memento;
using SysTeMate.BMS.Application.ApplicationUsers.Queries;
using SysTeMate.BMS.Application.Common.Interfaces;
using SysTeMate.BMS.Infrastructure.ApplicationUsers.Memento;
using SysTeMate.BMS.Infrastructure.Identity;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Application.UnitTests
{
    [TestFixture]
    public class IdentityServiceTests
    {
        private IIdentityService _identityService;
        private IUserCredentialHistory<UserCredentialState> _userCredentialHistory;
        private IUserCredential<UserCredentialState> _userCredential;
        private Mock<IUserManager<ApplicationUser, IdentityResult>> _userManager;
        private Mock<ISignInManager<SignInResult>> _signInManager;
        private CreateUserCommand _createUserCommand;
        private SignInUserCommand _signInUserCommand;
        private SignOutUserCommand _signOutUserCommand;
        private UpdateUserCommand _updateUserCommand;
        private DeleteUserCommand _deleteUserCommand;
        private ApplicationUser _applicationUsers;

        [SetUp]
        public void Setup()
        {
            _userCredential = new UserCredential();
            _userCredentialHistory = new UserCredentialHistory(_userCredential);
            _userManager = new Mock<IUserManager<ApplicationUser, IdentityResult>>();
            _signInManager = new Mock<ISignInManager<SignInResult>>();
            _identityService = new IdentityService(_userManager.Object, _userCredentialHistory, _signInManager.Object);
            _applicationUsers = new ApplicationUser
            {
                Id = new Guid().ToString(),
                UserName = "test",
                EmployeeId = 2
            };

            _createUserCommand = new CreateUserCommand
            {
                UserName = "John2",
                Password = "Engineering_1105",
                EmployeeId = 2,
                Roles = new List<string> { "CanProcessOrders" }
            };

            _signInUserCommand = new SignInUserCommand { UserName = "Test", Password = "Test" };
            _signOutUserCommand = new SignOutUserCommand { UserName = "Test" };

            _updateUserCommand = new UpdateUserCommand
            {
                UserName = "John2",
                Password = "Engineering_1105",
                EmployeeId = 2,
                Roles = new List<string> { "CanProcessOrders" }
            };

            _deleteUserCommand = new DeleteUserCommand { UserName = "test", EmployeeId = 1, Id = new Guid() };
        }

        [Test]
        public async Task CreateUser_CreateUserSuccessful_ReturnsTrue()
        {
            _userManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(um => um.AddToRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<string>>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _identityService.CreateUser(_createUserCommand);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CreateUser_CreateUserFailed_ReturnsFalse()
        {
            _userManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            _userManager.Setup(um => um.AddToRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<string>>()))
                .ReturnsAsync(IdentityResult.Failed());

            _userManager.Setup(um => um.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser
                {
                    Id = new Guid().ToString()
                });

            var result = await _identityService.CreateUser(_createUserCommand);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task SignIn_Successful_ReturnsTrue()
        {
            _signInManager.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(SignInResult.Success);

            var reuslt = await _identityService.SignIn(_signInUserCommand);
        }

        [Test]
        public async Task SignIn_Failed_ReturnsFalse()
        {
            _signInManager.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(SignInResult.Failed);           

            var result = await _identityService.SignIn(_signInUserCommand);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetApplicationUsers_RetrieveAll_ReturnsAllUsers()
        {
            _userManager.Setup(um => um.Users()).Returns(new List<ApplicationUser>
            {
               _applicationUsers,
               _applicationUsers
            }.AsQueryable());            

            var result = await _identityService.GetApplicationUsers( new GetApplicationUserQuery());

            Assert.NotZero(result.ApplicationUsers.Count);
        }

        [Test]
        public async Task GetApplicationUsers_UserExist_ReturnsUser()
        {
            _userManager.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_applicationUsers);

            var result = await _identityService.GetApplicationUsers(new GetApplicationUserQuery { Id = new Guid() });

            Assert.IsNotNull(result.ApplicationUsers);
        }

        [Test]
        public async Task GetApplicationUsers_UserNotExist_ReturnsNull()
        {
            _userManager.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var result = await _identityService.GetApplicationUsers(new GetApplicationUserQuery { Id = new Guid() });

            Assert.IsNull(result.ApplicationUsers);
        }

        [Test]
        public async Task UpdateUser_UpdateUserSuccess_ReturnsTrue()
        {
            _userManager.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_applicationUsers);
            _userManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string> { "CanManagerUserAccounts" });
            _userManager.Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>()))
               .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(um => um.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(um => um.RemoveFromRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<string>>()))
              .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
              .ReturnsAsync(IdentityResult.Success);

            var result = await _identityService.UpdateUser(_updateUserCommand);

            Assert.IsTrue(result);
        }
    }
}
