using Publicator.Infrastructure.Entities;

namespace Publicator.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public Repository<User> UserRepository { get; }
        public Repository<Bookmark> BookmarkRepository { get; }
        public Repository<Comment> CommentRepository { get; }
        public Repository<Post> PostRepository { get; }
        public Repository<PostTag> PostTagRepository { get; }
        public Repository<UserTag> UserTagRepository { get; }
        public Repository<SubscriptionNewPost> SubscriptionNewPostRepository { get; }
        public Repository<Tag> TagRepository { get; }
        public Repository<State> StateRepository { get; }
        public Repository<UserSubscription> UserSubscriptionRepository { get; }
        public Repository<Vote> VoteRepository { get; }
        public Repository<Community> CommunityRepository { get; }
        public Repository<UserCommunity> UserCommunityRepository { get; }
        public Repository<Role> RoleRepository{ get; }
        public void Save();
    }
}
