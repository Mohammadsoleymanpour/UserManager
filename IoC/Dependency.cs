using Application.Interface;
using Application.Services;
using DataLayer.Repositories;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public class Dependency
{
    public static void ManageDependencyInjection(IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserServices, UserService>();
    }
}