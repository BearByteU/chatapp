using AutoMapper;
using ChatApp.Constants;
using ChatApp.Interface;
using ChatApp.Models;
using ChatApp.SRMDbContexts.IRepository;
using ChatApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Service
{
    public class MessageService : IMessageService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<Message>> RecieveMessage(MessageDto request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                var reciverUser = _unitOfWork.UserRepository.Find().Where(x => x.Id == request.UserFrom).FirstOrDefault();
                if (reciverUser == null)
                {
                    return null;
                }
                var getMessage = await _unitOfWork.MessageRepository.Find().Where(x => x.UserTo == request.UserTo).ToListAsync();
                if (getMessage != null)
                {
                    
                    return getMessage;
                }
                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EntityResponseModel> SendMessage(MessageDto request)
        {
            try
            {
                if (request == null)
                {
                    return new EntityResponseModel
                    {
                        Status = false,
                        Message = ConstantString.ValidationError,
                    };
                }
                var reciverUser = _unitOfWork.UserRepository.Find().Where(x => x.Id == request.UserFrom).FirstOrDefault();
                if (reciverUser == null)
                {
                    return new EntityResponseModel
                    {
                        Status = false,
                        Message = ConstantString.ValidationError,
                    };
                }
                var sendmsg = _mapper.Map<Message>(request);
                await _unitOfWork.MessageRepository.Insert(sendmsg);
                await _unitOfWork.Save();
                ChatHub ch = new ChatHub();
                await ch.SendMessage(request.UserTo.ToString(), request.MessageDescription);
                return new EntityResponseModel
                {
                    Message = ConstantString.Success,
                    Status = true
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
