using Application.Common;
using Application.Interface;
using Application.ViewModel.User;
using Microsoft.AspNetCore.Mvc;

namespace StormTest.Controllers
{
    [ApiController]
    [Route("api/Users/")]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        [Route("GetUsers")]
        public async Task<OperationResult<IEnumerable<UserViewModel>>> Index()
        {
            return await _userServices.GetUsers();
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<OperationResult<UserViewModel>> GetUserById(int id)
        {

            return await _userServices.GetUserById(id);
        }


        [HttpPost]
        [Route("AddUser")]
        public async Task<OperationResult<int>> AddUser(AddUserViewModel userViewModel)
        {
            return await _userServices.AddUser(userViewModel);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<OperationResult<int>> UpdateUser([FromForm] UpdateUserViewModel userViewModel)
        {

            return await _userServices.UpdateUser(userViewModel);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<OperationResult<bool>> DeleteUser(int id)
        {
            return await _userServices.DeleteUser(id);
        }


    }
}
