using System.Collections.Generic;
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
            CreateMap<Photo, PostPhotoDto>();
            CreateMap<PostPhotoDto, Photo>();
            CreateMap<UpdatePostDto, Post>();
            CreateMap<Post, UpdatePostDto>();
            CreateMap<UpdateCommentDto, Comment>();
            CreateMap<Comment,UpdateCommentDto>();
         //   CreateMap<List<Photo>, List<postPhotoDto>>(photos);
            // CreateMap<List<Photo>, <List<PostPhotoDto>>();
        }
        
    }
}