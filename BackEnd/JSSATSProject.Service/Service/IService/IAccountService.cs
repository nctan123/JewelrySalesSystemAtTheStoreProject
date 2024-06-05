using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.AccountModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IAccountService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> SignUpAsync(RequestSignUp signUpModel);
        public Task<ResponseModel> SignInAsync(RequestSignIn signInModel);
        //Task<ResponseModel> VerifyEmailAsync(string username, string verifyKey);
        //Task<ResponseModel> ForgetPasswordAsync(SignInModel signInModel);
    }
}