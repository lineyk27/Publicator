using System;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.Infrastructure
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private PublicatorDbContext context;
        private bool disposed;
        public UnitOfWork(string connectionstring)
        {
            context = PublicatorDbContextFactory.Create(connectionstring);
            disposed = false;
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
                    userRepository = new Repository<User>(context);
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
                    bookmarkRepository = new Repository<Bookmark>(context);
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
                    commentRepository = new Repository<Comment>(context);
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
                    postRepository = new Repository<Post>(context);
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
                    postTagRepository = new Repository<PostTag>(context);
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
                    userTagRepository = new Repository<UserTag>(context);
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
                    subscriptionNewPostRepository = new Repository<SubscriptionNewPost>(context);
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
                    tagRepository = new Repository<Tag>(context);
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
                    stateRepository = new Repository<State>(context);
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
                    userSubscriptionRepository = new Repository<UserSubscription>(context);
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
                    voteRepository = new Repository<Vote>(context);
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
                    userCommunityRepository = new Repository<UserCommunity>(context);
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
                    communityRepository = new Repository<Community>(context);
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
                    roleRepository = new Repository<Role>(context);
                }
                return roleRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
