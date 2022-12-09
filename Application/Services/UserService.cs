using Application.Common;
using Application.Interface;
using Application.Security;
using Application.ViewModel.User;
using Domain.Interfaces;
using Domain.Models.User;

namespace Application.Services;

public class UserService : IUserServices
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<OperationResult<IEnumerable<UserViewModel>>> GetUsers()
    {
        var Users = await _userRepository.GetUsers();
        if (Users == null)
        {
            return OperationResult<IEnumerable<UserViewModel>>.NotFound();
        }
        List<UserViewModel> userViewModels = new List<UserViewModel>();

        foreach (var user in Users)
        {
            userViewModels.Add(new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                UserName = user.UserName,
            });
        }

        return OperationResult<IEnumerable<UserViewModel>>.Success(userViewModels);
    }

    public async Task<OperationResult<int>> AddUser(AddUserViewModel user)
    {
        var CrUser = new User()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Password = PasswordHelper.EncodePasswordMd5(user.Password)
        };
        int Id = await _userRepository.AddUser(CrUser);
        if (Id == null)
        {
            return OperationResult<int>.Error();
        }
        return OperationResult<int>.Success(Id);
    }

    public async Task<OperationResult<int>> UpdateUser(UpdateUserViewModel user)
    {
        var getUser = await _userRepository.GetById(user.Id);
        if (getUser == null)
        {
            return OperationResult<int>.NotFound();
        }

        getUser.FirstName = user.FirstName;
        getUser.LastName = user.LastName;
        getUser.UserName = user.UserName;
        getUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
        int Id = await _userRepository.UpdateUser(getUser);
        if (Id == null)
        {
            return OperationResult<int>.Error();
        }
        return OperationResult<int>.Success(Id);
    }

    public async Task<OperationResult<UserViewModel>> GetUserById(int userId)
    {
        var user = await _userRepository.GetById(userId);
        if (user==null)
        {
            return OperationResult<UserViewModel>.Error("کاربری پیدا نشد");
        }
        var userVM = new UserViewModel()
        {
            Id = user.Id,
            LastName = user.LastName,
            FirstName = user.FirstName,
            Password = user.Password,
            UserName = user.UserName
        };
        return OperationResult<UserViewModel>.Success(userVM);
    }


    public async Task<OperationResult<bool>> DeleteUser(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
        {
            return OperationResult<bool>.NotFound();
        }

        var res = await _userRepository.DeleteUser(user.Id);
        if (!res)
        {
            return OperationResult<bool>.Error();
        }
        return OperationResult<bool>.Success(true);
    }

    public async Task<OperationResult<UserViewModel?>> LoginUser(LoginViewModel login)
    {
        string hashpassword = PasswordHelper.EncodePasswordMd5(login.Password);
        var res = await _userRepository.LoginUser(login.UserName, hashpassword);
        var user = await _userRepository.GetUserByUserName(login.UserName);
        var userViewModel = new UserViewModel()
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
        };
        return OperationResult<UserViewModel?>.Success(userViewModel);
    }

    public async Task<OperationResult<int?>> AddToken(AddUserTokenViewModel tokenViewModel)
    {
        var token = new UserToken()
        {
            HashJwtToken = tokenViewModel.HashToken,

            UserId = tokenViewModel.UserId,
            TokenExpireDate = tokenViewModel.HashTokenExTime,

        };

        var Id = await _userRepository.AddToken(token);

        return OperationResult<int?>.Success(Id);
    }
}