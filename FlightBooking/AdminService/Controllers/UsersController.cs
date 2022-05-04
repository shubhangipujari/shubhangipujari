using AdminService.Models;
using AdminService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AdminService.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _userRepository;

        public UsersController(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult getUser()
        {
            var users = _userRepository.GetUsers();
            return new OkObjectResult(users);
        }
      
        [HttpGet("{id}", Name = "getUserById")]
        public IActionResult getUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            return new OkObjectResult(user);
        }

        [HttpPost]
        [Route("createUser")]


        public IActionResult CreateUser([FromBody] UserDetail user)
        {
            using (var scope = new TransactionScope())
            {
                _userRepository.CreateUser(user);
                scope.Complete();
                return CreatedAtAction(nameof(getUser), user);
            }
        }

        [HttpPut]
        [Route("updateUser")]
        public IActionResult UpdateUser([FromBody] UserDetail user)
        {
            if (user != null)
            {
                using (var scope = new TransactionScope())
                {
                    _userRepository.UpdateUser(user);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userRepository.DeleteUser(id);
            return new OkResult();
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public IActionResult login(string emailId, string password)
        {
            var tokan = _userRepository.Login(emailId, password);

            if (tokan == null)
            {
                return Unauthorized();
            }
            return new OkObjectResult(tokan);
        }

    }
}
