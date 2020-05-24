using System;
using System.Collections.Generic;

namespace Publicator.ApplicationCore.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public UserDTO CreatorUser { get; set; }
        public DateTime CreationDate{ get;set; }
        public int CurrentRating{ get; set; }
        public CommunityDTO Community { get; set; }
        public VoteDTO CurrentVote { get; set; }
        public BookmarkDTO CurrentBookmark { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }
    }
}
