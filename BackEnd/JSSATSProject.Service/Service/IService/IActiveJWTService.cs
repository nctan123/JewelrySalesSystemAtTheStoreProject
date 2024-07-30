using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Service.IService;

public interface IActiveJWTService
{
    Task<ActiveJwt> SaveTokenAsync(string username, string token);
    Task DeleteAsync(string username);

    Task<bool> IsValidTokenAsync(string token);
    Task<ActiveJwt?> GetByUsernameAsync(string username);
}