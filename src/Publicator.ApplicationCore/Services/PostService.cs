using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.ApplicationCore.Services
{
    public class PostService : IPostService
    {
        private IUnitOfWork _unitOfWork;
        private IUserService _userService;
        private ITagService _tagService;
        private ICommunityService _communityService;
        private int _page;
        private int _pageSize;
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                if (value > 0)
                    _page = value;
            }
        }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > 0)
                    _pageSize = value;
            }
        }
        public HotPeriod Period{ get; set; }
        public PostService(
            IUnitOfWork unitOfWork, 
            IUserService userService, 
            ITagService tagService,
            ICommunityService communityService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _tagService = tagService;
            _communityService = communityService;
            PageSize = 10;
            Page = 1;
            Period = HotPeriod.Month;
        }

        public async void AddSubscriptionNewPostAsync(Post post)
        {
            var currentuser = await _userService.GetCurrentUserAsync();
            //var tags = await _tagService.GetByPostAsync(post);
            var creator = await _userService.GetByPostAsync(post);
            var community = await _communityService.GetByPostAsync(post);
            var usersubscribers = (await _unitOfWork
                .UserSubscriptionRepository
                .GetAsync(x => x.SubscriptionUserId == currentuser.Id,includeProperties:"SubscriberUser"))
                .Select(x => x.SubscriberUser);
            var communitysubscribers = (await _unitOfWork
                .UserCommunityRepository
                .GetAsync(x => x.CommunityId == community.Id,includeProperties:"User"))
                .Select(x => x.User);
            foreach (var i in usersubscribers)
            {
                await AddSingleSubscriptionNewPostAsync(post, i, creator, null);
            }
            foreach (var i in communitysubscribers)
            {
                await AddSingleSubscriptionNewPostAsync(post, i, null, community);
            }
            // TODO add tags subscribe logic(or no)
        }

        public async Task AddSingleSubscriptionNewPostAsync(Post post,
            User subscriberuser,
            User subscriptionuser,
            Community subscriptioncommunity)
        {
            var currentsubscription = (await _unitOfWork
                .SubscriptionNewPostRepository
                .GetAsync(x => x.UserId == subscriberuser.Id && x.PostId == post.Id))
                .FirstOrDefault();
            if(currentsubscription != null)
            {
                currentsubscription.SubscriptionCommunityId = (Guid)subscriptioncommunity?.Id;
                currentsubscription.SubscriptionUserId = (Guid)subscriptionuser?.Id;
                _unitOfWork
                    .SubscriptionNewPostRepository
                    .Update(currentsubscription);                
            }
            else
            {
                var newpost = new SubscriptionNewPost()
                {
                    PostId = post.Id,
                    UserId = subscriberuser.Id
                };
                newpost.SubscriptionCommunityId = (Guid)subscriptioncommunity?.Id;
                newpost.SubscriptionUserId = (Guid)subscriptionuser?.Id;
                _unitOfWork
                    .SubscriptionNewPostRepository
                    .Insert(newpost);
            }
            _unitOfWork.Save();
        }

        public async Task<bool> AddToBookmarkAsync(Post post)
        {
            var user = await _userService.GetCurrentUserAsync();
            var currentbookmark = (await _unitOfWork
                .BookmarkRepository
                .GetAsync(x => x.UserId == user.Id && x.PostId == post.Id))
                .FirstOrDefault();
            if(currentbookmark == null)
            {
                var newbookmark= new Bookmark()
                {
                    UserId = user.Id,
                    PostId = post.Id,
                    CreationDate = DateTime.Now
                };
                _unitOfWork.BookmarkRepository.Insert(newbookmark);
                _unitOfWork.Save();
                return true;
            }
            else
            {
                _unitOfWork.BookmarkRepository.Delete(currentbookmark);
                _unitOfWork.Save();
                return false;
            }
        }
        public async Task<bool> RemoveFromBookmarkAsync(Post post)
        {
            var user = await _userService.GetCurrentUserAsync();
            var currentbookmark = (await _unitOfWork
                .BookmarkRepository
                .GetAsync(x => x.UserId == user.Id && x.PostId == post.Id))
                .FirstOrDefault();
            if (currentbookmark != null)
            {
                _unitOfWork.BookmarkRepository.Delete(currentbookmark);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> HasBookmarkAsync(Post post)
        {
            var user = await _userService.GetCurrentUserAsync();
            return (await _unitOfWork
                .BookmarkRepository
                .GetAsync(x => x.PostId == post.Id && x.UserId == user.Id))
                .FirstOrDefault() != null;
        }

        public async Task<Post> CreateAsync(string name, string content, Community community, IEnumerable<Tag> tags)
        {
            var user = await _userService.GetCurrentUserAsync();
            var post = new Post()
            {
                Name = name,
                Content = content,
                CommunityId = community.Id,
                CreationDate = DateTime.Now,
                CreatorUserId = user.Id
            };
            _unitOfWork.PostRepository.Insert(post);
            _unitOfWork.Save();
            AddTagsToPostAsync(post, tags);
            return post;
        }

        public async void AddTagsToPostAsync(Post post, IEnumerable<Tag> tags)
        {
            foreach(var i in tags)
            {
                var tag = await _tagService.CreateAsync(i.Name);
                _unitOfWork.PostTagRepository.Insert(new PostTag()
                {
                    PostId = post.Id,
                    TagId = i.Id,                    
                });
                _unitOfWork.Save();
            }
        }

        public async Task<IEnumerable<Post>> GetNewAsync()
        {
            return (await _unitOfWork
                .PostRepository
                .GetAsync(includeProperties: "PostTags.Tag,CreatorUser,Votes,Bookmarks"))
                .OrderByDescending(x => x.CreationDate)
                .Skip(GetStartPage())
                .Take(PageSize);
        }

        public int GetStartPage()
        {
            return (Page - 1) * PageSize;
        }

        public async Task<IEnumerable<Post>> GetBookmarks()
        {
            var user = await _userService.GetCurrentUserAsync();
            return (await _unitOfWork
                .BookmarkRepository
                .GetAsync(x => x.UserId == user.Id,includeProperties: "Post.PostTags.Tag, Post.CreatorUser, Post.Votes, Post.Bookmarks"))
                .Select(x => x.Post)
                .OrderByDescending(x => x.CreationDate)
                .Skip(GetStartPage())
                .Take(PageSize);
        }

        public async Task<IEnumerable<Post>> GetHotAsync()
        {
            DateTime startDate;
            var endDate = DateTime.Now;
            switch (Period)
            {
                case HotPeriod.Day:
                    startDate = endDate.AddDays(-1);
                    break;
                case HotPeriod.Week:
                    startDate = endDate.AddDays(-7);
                    break;
                case HotPeriod.Year:
                    startDate = endDate.AddYears(-1);
                    break;
                default:
                    startDate = endDate.AddMonths(-1);
                    break;
            }
            var periodDate = (endDate - startDate).TotalHours;

            var startPage = (Page - 1) * PageSize;
            var posts = (await _unitOfWork
                .PostRepository
                .GetAsync(includeProperties: "PostTags.Tag,CreatorUser,Votes,Bookmarks"))
                .Where(x => x.CreationDate <= endDate && x.CreationDate >= startDate)
                .OrderByDescending(x => (x.CurrentRating / periodDate))
                .Skip(startPage)
                .Take(PageSize);

            return posts;
        }

        public async Task<IEnumerable<Post>> GetByCommunity(Community community)
        {
            return (await _unitOfWork
                .CommunityRepository
                .GetAsync(x => x.Id == community.Id, includeProperties: "Posts.PostTags.Tag,Posts.CreatorUser,Votes,Bookmarks"))
                .FirstOrDefault()
                .Posts
                .OrderByDescending(x => x.CreationDate)
                .Skip(GetStartPage())
                .Take(PageSize);
        }

        public async Task<IEnumerable<Post>> GetByCreatorAsync(User creatoruser)
        {
            return (await _unitOfWork
                .PostRepository
                .GetAsync(x => x.CreatorUserId == creatoruser.Id,includeProperties: "PostTags.Tag,CreatorUser,Votes,Bookmarks"))
                .OrderByDescending(x => x.CreationDate)
                .Skip(GetStartPage())
                .Take(PageSize);
        }

        public async Task<Post> GetByIdAsync(Guid postid)
        {
            var post = (await _unitOfWork
                .PostRepository
                .GetAsync(x => x.Id == postid,includeProperties: "PostTags.Tag,CreatorUser,Votes,Bookmarks"))
                .FirstOrDefault();
            if (post == null)
                throw new ResourceNotFoundException("Post not found");
            return post;
        }

        public async Task<IEnumerable<Post>> GetBySubscriptionAsync(User user)
        {
            return (await _unitOfWork
                .SubscriptionNewPostRepository
                .GetAsync(x => x.SubscriptionUserId == user.Id, includeProperties: "Post.PostTags.Tag,Post.CreatorUser,Votes,Bookmarks"))
                .Select(x => x.Post)
                .OrderByDescending(x => x.CreationDate)
                .Skip(GetStartPage())
                .Take(PageSize);
        }

        public async Task<IEnumerable<Post>> GetByTagAsync(Tag tag)
        {
            return (await _unitOfWork
                .PostTagRepository
                .GetAsync(x => x.TagId == tag.Id,includeProperties: "Post.PostTags.Tag,Post.CreatorUser,Post.Votes,Post.Bookmarks"))
                .Select(x => x.Post)
                .OrderByDescending(x => x.CreationDate)
                .Skip(GetStartPage())
                .Take(PageSize);
        }

        public async Task<int> CalcCurrentRatingAsync(Post post)
        {
            var currentrating = (await _unitOfWork
                .VoteRepository
                .GetAsync(x => x.PostId == post.Id))
                .Select(x => x.Up ? 1 : -1)
                .Sum();
            post.CurrentRating = currentrating;
            _unitOfWork
                .PostRepository
                .Update(post);
            _unitOfWork.Save();
            return currentrating;
        }

        public async Task<IEnumerable<Post>> GetVotedByCreatorAsync(User creatorvoteuser)
        {
            return (await _unitOfWork
                .PostRepository
                .GetAsync(x => x.Votes.Any(t => t.UserId == creatorvoteuser.Id), includeProperties: "Votes, Bookmarks,PostTags.Tag,CreatorUser"))
                .OrderByDescending(x => x.CreationDate)
                .Skip(GetStartPage())
                .Take(PageSize);
        }

        public async Task<Vote> VoteAsync(Post post, bool up)
        {
            var user = await _userService.GetCurrentUserAsync();
            Vote currentvote = (await _unitOfWork
                .VoteRepository
                .GetAsync(x => x.PostId == post.Id && x.UserId == user.Id))
                .FirstOrDefault();
            if(currentvote == null)
            {
                currentvote = new Vote()
                {
                    Id = Guid.NewGuid(),
                    PostId = post.Id,
                    UserId = user.Id,
                    CreationDate = DateTime.Now,
                    Up = up
                };
                _unitOfWork.VoteRepository.Insert(currentvote);
                _unitOfWork.Save();
            }
            else
            {
                if(currentvote.Up == up)
                {
                    _unitOfWork.VoteRepository.Delete(currentvote);
                    _unitOfWork.Save();
                    currentvote = null;
                }
                else
                {
                    currentvote.Up = up;
                    currentvote.CreationDate = DateTime.Now;
                    _unitOfWork.VoteRepository.Update(currentvote);
                    _unitOfWork.Save();
                }
            }
            await CalcCurrentRatingAsync(post);
            return currentvote;    
        }

        public async Task<Vote> CurrentVoteAsync(Post post)
        {
            var user = await _userService.GetCurrentUserAsync();
            return (await _unitOfWork
                .VoteRepository
                .GetAsync(x => x.UserId == user.Id && x.PostId == post.Id))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Post>> GetBySearchAsync(
            string query, 
            DateTime? startDate, 
            DateTime? endDate,
            int? minRating,
            Community community,
            User creatorUser)
        {
            var result = await _unitOfWork
                .PostRepository
                .GetAsync(x => x.Name.ToLower().Contains(query) ||
                x.Content.ToLower().Contains(query),includeProperties: "Community,PostTags.Tag,CreatorUser,Votes,Bookmarks");

            if(community != null)
            {
                result = result.Where(x => x.CommunityId == community.Id);
            }
            if(startDate != null)
            {
                result = result.Where(x => startDate <= x.CreationDate);
            }
            if (endDate != null)
            {
                result = result.Where(x => x.CreationDate <= endDate);
            }
            if (minRating != null)
            {
                result = result.Where(x => x.CurrentRating >= minRating);
            }
            if(creatorUser != null)
            {
                result = result.Where(x => x.CreatorUserId == creatorUser.Id);
            }
            return result.Skip(GetStartPage()).Take(PageSize).OrderByDescending(x => x.CreationDate);

        }
    }
}
