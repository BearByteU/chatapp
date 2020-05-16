using ChatApp.Models;
using ChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Interface
{
    public interface IMessageService
    {
        Task<EntityResponseModel> SendMessage(MessageDto request);
        Task<List<Message>> RecieveMessage(MessageDto request);
    }
}
