using System.Linq;
using AutoMapper;
using Publicator.Infrastructure.Models;
using Publicator.ApplicationCore.DTO;

namespace Publicator.ApplicationCore
{
    class PublicatorProfile : Profile
    {
        public PublicatorProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.ImageUrl, e => e.MapFrom(x => x.PictureName));
            CreateMap<Post, PostDTO>()
                .ForMember(dto => dto.Tags, e => e.MapFrom(z => z.PostTags.Select(pt => pt.Tag)));
            CreateMap<Tag, TagDTO>();
            CreateMap<Comment, CommentDTO>()
                .ForMember(dto => dto.CreatorUser, e => e.MapFrom(z => z.User))
                .ForMember(dto => dto.Replies, e => e.MapFrom(z => z.RepliesComments));
            CreateMap<Community, CommunityDTO>()
                .ForMember(dto => dto.ImageUrl, e => e.MapFrom(x => x.PictureName));
            CreateMap<Role, RoleDTO>();
            CreateMap<Vote, VoteDTO>()
                .ForMember(dto => dto.Up, e => e.MapFrom(x => x.Up == true))
                .ForMember(dto => dto.Down, e => e.MapFrom(x => x.Up == false));
            CreateMap<Bookmark, BookmarkDTO>()
                .ForMember(dto => dto.Bookmarked, e => e.NullSubstitute(false));
        }
    }
}
