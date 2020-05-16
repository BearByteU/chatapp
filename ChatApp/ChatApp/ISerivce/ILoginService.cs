using ChatApp.ViewModels;
using System.Threading.Tasks;

namespace ChatApp.ISerivce
{
    public interface ILoginService
    {
        Task<EntityResponseModel> FetchLoginCredential(LoginDto login);
    }
}
