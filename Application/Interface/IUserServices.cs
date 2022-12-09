using Application.Common;
using Application.ViewModel.User;
using Domain.Models.User;

namespace Application.Interface;

public interface IUserServices
{
    Task<OperationResult<IEnumerable<UserViewModel>>> GetUsers();
    Task<OperationResult<int>> AddUser(AddUserViewModel user);
    Task<OperationResult<int>> UpdateUser(UpdateUserViewModel user);
    Task<OperationResult<UserViewModel>> GetUserById(int userId);
    Task<OperationResult<bool>> UserIsExist(string userName);
    Task<OperationResult<bool>> DeleteUser(int id);
    Task<OperationResult<UserViewModel?>> LoginUser(LoginViewModel login);
    Task<OperationResult<int?>> AddToken(AddUserTokenViewModel tokenViewModel);
}