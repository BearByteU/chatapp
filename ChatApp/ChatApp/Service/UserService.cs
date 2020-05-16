using AutoMapper;
using ChatApp.Constants;
using ChatApp.Models;
using ChatApp.SRMDbContexts.IRepository;
using ChatApp.ViewModels;
using System;
using System.Threading.Tasks;
using TaskQ.BAL.Interface;

namespace ChatApp.Service
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EntityResponseModel> CreateUser(UserDto request)
        {
            try
            {
                if (request == null)
                {
                    return new EntityResponseModel
                    {
                        Message = ConstantString.ValidationError,
                        Status = false
                    };
                }
                var newUser = _mapper.Map<User>(request);
                await _unitOfWork.UserRepository.Insert(newUser);
                await _unitOfWork.Save();
                return new EntityResponseModel
                {
                    Message = ConstantString.Success,
                    Status = true                    
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Task<EntityResponseModel> DeleteUser(long id)
        {
            try
            {

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<EntityResponseModel> UpdateUserDetail(UserDto request)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}