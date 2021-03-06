﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Vote.Commands
{
    class VoteForPostHandler : IRequestHandler<VoteForPost, VoteDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public VoteForPostHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<VoteDTO> Handle(
            VoteForPost request, 
            CancellationToken cancellationToken
            )
        {
            var currentVote = (from v in _context.Votes
                               where v.PostId == request.PostId
                               select v)
                               .FirstOrDefault();


            var post = _context.Posts.FirstOrDefault(x => x.Id == request.PostId);
            if (currentVote == null)
            {
                var newVote = new Infrastructure.Models.Vote()
                {
                    PostId = request.PostId,
                    UserId = (Guid)request.UserId,
                    Up = request.Up,
                    CreationDate = DateTime.Now
                };
                _context.Votes.Add(newVote);

                post.CurrentRating += request.Up ? 1 : -1;
            }
            else
            {
                if(currentVote.Up == request.Up)
                {
                    _context.Votes.Remove(currentVote);
                    post.CurrentRating += currentVote.Up ? -1 : 1;
                }
                else
                {
                    currentVote.Up = request.Up;
                    _context.Votes.Update(currentVote);
                    post.CurrentRating += currentVote.Up ? 2 : -2;
                }
            }

            _context.Posts.Update(post);
            
            await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<Infrastructure.Models.Vote, VoteDTO>(currentVote);

            return dto;
        }
    }
}
