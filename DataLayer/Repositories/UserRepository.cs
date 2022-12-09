using System.Formats.Asn1;
using DataLayer.DBContext;
using Domain.Interfaces;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories;

public class UserRepository : IUserRepository
{

    private readonly StormTestContext _context;

    public UserRepository(StormTestContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<int> AddUser(User user)
    {
        var addUser = await _context.Users.AddAsync(user);
        Save();
        return addUser.Entity.Id;
    }

    public async Task<int> UpdateUser(User user)
    {

        var updateUser = _context.Users.Update(user);
        Save();
        return updateUser.Entity.Id;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await GetById(id);
        if (user == null)
        {
            return false;
        }

        user.IsDelete = true;
        var Id = await UpdateUser(user);
        if (Id == null)
        {
            return false;
        }
        return true;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}