using System.Linq;
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
            CreateMap<Post, PostDTO>()
                .ForMember(dto => dto.Tags, e => e.MapFrom(z => z.PostTags.Select(pt => pt.Tag)));
            CreateMap<Tag, TagDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<Vote, VoteDTO>()
                .ForMember(dto => dto.Up, e => e.MapFrom(x => x.Up == true))
                .ForMember(dto => dto.Down, e => e.MapFrom(x => x.Up == false));
        }
    }
}
