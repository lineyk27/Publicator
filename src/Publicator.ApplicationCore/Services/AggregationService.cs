using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Services
{
    class AggregationService : IAggregationService
    {
        IMapper _mapper;
        public AggregationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<PostDTO> AggregateWithBookmarkVote(IEnumerable<Post> posts,User user)
        {
            if (user == null)
                return _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
            var result = new List<PostDTO>();
            PostDTO dto;
            foreach(var i in posts) 
            {
                dto = _mapper.Map<Post, PostDTO>(i);
                var vote = i.Votes.Where(x => x.UserId == user.Id).FirstOrDefault();
                var bookmark = i.Bookmarks.Where(x => x.UserId == user.Id).FirstOrDefault();
                if (vote != null)
                {
                    dto.CurrentVote = new VoteDTO() { Down = vote.Up == false, Up = vote.Up = true };
                }
                dto.CurrentBookmark = new BookmarkDTO() { Bookmarked = bookmark != null };
                result.Append(dto);
            };

            return result;
        }
    }
}
