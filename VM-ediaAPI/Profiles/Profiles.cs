using AutoMapper;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<User, UpdateUserDto>();
            CreateMap<UpdateUserDto, User>();
        }
        
    }
}