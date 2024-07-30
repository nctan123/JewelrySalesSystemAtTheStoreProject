using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Service.IService;
using Microsoft.Extensions.DependencyInjection;

namespace JSSATSProject.Service.Service.Service
{
    public class ActiveJWTService : IActiveJWTService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ActiveJWTService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<ActiveJwt> SaveTokenAsync(string username, string token)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();

                var newActiveToken = new ActiveJwt()
                {
                    Username = username,
                    Token = token,
                    ExpiryDate = DateTime.Now
                };
                await unitOfWork.ActiveJWTRepository.InsertAsync(newActiveToken);
                await unitOfWork.SaveAsync();
                return newActiveToken;
            }
        }

        public async Task DeleteAsync(string username)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
                await unitOfWork.ActiveJWTRepository.DeleteAsync(username);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task<bool> IsValidTokenAsync(string token)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
                return await unitOfWork.ActiveJWTRepository.IsValidTokenAsync(token);
            }
        }

        public async Task<ActiveJwt?> GetByUsernameAsync(string username)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
                var result = await unitOfWork.ActiveJWTRepository.GetAsync(a => a.Username == username);
                return result.FirstOrDefault();
            }
        }
    }
}
