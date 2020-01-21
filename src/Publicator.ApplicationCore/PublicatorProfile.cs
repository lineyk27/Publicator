using AutoMapper;
using Publicator.Infrastructure.Entities;
using Publicator.ApplicationCore.DTO;

namespace Publicator.ApplicationCore
{
    class PublicatorProfile : Profile
    {
        public PublicatorProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Post, PostDTO>();
            CreateMap<Tag, TagDTO>();
            CreateMap<Comment, CommentDTO>();
        }
    }
}
