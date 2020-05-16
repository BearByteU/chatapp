using ChatApp.Models;
using System;
using System.Threading.Tasks;

namespace ChatApp.SRMDbContexts.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<User, int> UserRepository { get; }
        IGenericRepository<Message, int> MessageRepository { get; }
        Task Save();
    }
}
