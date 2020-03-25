using System;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private PublicatorDbContext _context;
        public UnitOfWork(PublicatorDbContext context)
        {
            _context = context;
        }
        private Repository<User> userRepository;
        private Repository<Bookmark> bookmarkRepository;
        private Repository<Comment> commentRepository;
        private Repository<Post> postRepository;
        private Repository<PostTag> postTagRepository;
        private Repository<UserTag> userTagRepository;
        private Repository<SubscriptionNewPost> subscriptionNewPostRepository;
        private Repository<Tag> tagRepository;
        private Repository<State> stateRepository;
        private Repository<UserSubscription> userSubscriptionRepository;
        private Repository<Vote> voteRepository;
        private Repository<Role> roleRepository;
        private Repository<Community> communityRepository;
        private Repository<UserCommunity> userCommunityRepository;
        public Repository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new Repository<User>(_context);
                }
                return userRepository;
            }
        }
        public Repository<Bookmark> BookmarkRepository
        {
            get
            {
                if (bookmarkRepository == null)
                {
                    bookmarkRepository = new Repository<Bookmark>(_context);
                }
                return bookmarkRepository;
            }
        }
        public Repository<Comment> CommentRepository
        {
            get
            {
                if (commentRepository == null)
                {
                    commentRepository = new Repository<Comment>(_context);
                }
                return commentRepository;
            }
        }
        public Repository<Post> PostRepository
        {
            get
            {
                if (postRepository == null)
                {
                    postRepository = new Repository<Post>(_context);
                }
                return postRepository;
            }
        }
        public Repository<PostTag> PostTagRepository
        {
            get
            {
                if (postTagRepository == null)
                {
                    postTagRepository = new Repository<PostTag>(_context);
                }
                return postTagRepository;
            }
        }
        public Repository<UserTag> UserTagRepository
        {
            get
            {
                if (userTagRepository == null)
                {
                    userTagRepository = new Repository<UserTag>(_context);
                }
                return userTagRepository;
            }
        }
        public Repository<SubscriptionNewPost> SubscriptionNewPostRepository
        {
            get
            {
                if (subscriptionNewPostRepository == null)
                {
                    subscriptionNewPostRepository = new Repository<SubscriptionNewPost>(_context);
                }
                return subscriptionNewPostRepository;
            }
        }
        public Repository<Tag> TagRepository
        {
            get
            {
                if (tagRepository == null)
                {
                    tagRepository = new Repository<Tag>(_context);
                }
                return tagRepository;
            }
        }
        public Repository<State> StateRepository
        {
            get
            {
                if (stateRepository == null)
                {
                    stateRepository = new Repository<State>(_context);
                }
                return stateRepository;
            }
        }
        public Repository<UserSubscription> UserSubscriptionRepository
        {
            get
            {
                if (userSubscriptionRepository == null)
                {
                    userSubscriptionRepository = new Repository<UserSubscription>(_context);
                }
                return userSubscriptionRepository;
            }
        }
        public Repository<Vote> VoteRepository
        {
            get
            {
                if (voteRepository == null)
                {
                    voteRepository = new Repository<Vote>(_context);
                }
                return voteRepository;
            }
        }
        public Repository<UserCommunity> UserCommunityRepository
        {
            get
            {
                if (userCommunityRepository == null)
                {
                    userCommunityRepository = new Repository<UserCommunity>(_context);
                }
                return userCommunityRepository;
            }
        }
        public Repository<Community> CommunityRepository
        {
            get
            {
                if (communityRepository == null)
                {
                    communityRepository = new Repository<Community>(_context);
                }
                return communityRepository;
            }
        }
        public Repository<Role> RoleRepository
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new Repository<Role>(_context);
                }
                return roleRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
