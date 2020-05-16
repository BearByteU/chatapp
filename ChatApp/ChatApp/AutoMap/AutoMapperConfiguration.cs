using AutoMapper;
using ChatApp.Models;
using ChatApp.ViewModels;

namespace ChatApp.AutoMap
{

    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {            
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();
        }
    }

}