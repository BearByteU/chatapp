using ChatApp.ViewModels;
using System.Threading.Tasks;

namespace TaskQ.BAL.Interface
{
    public interface IUserService
    {
        Task<EntityResponseModel> CreateUser(UserDto request);
        Task<EntityResponseModel> UpdateUserDetail(UserDto request);
        Task<EntityResponseModel> DeleteUser(long id);
        
    }
}
