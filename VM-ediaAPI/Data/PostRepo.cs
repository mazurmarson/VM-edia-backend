using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class PostRepo : GenRepo, IPostRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PostRepo(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDetailsDto> GetPostDetailsDto(int postId, int userId)
        {
           // bool userReaction;
            var photos = await _context.Photos.Where(x => x.Id == postId).ToListAsync();
            List<PostPhotoDto> postPhotoDto = _mapper.Map<List<Photo>, List<PostPhotoDto>>(photos);

            var positiveReactions = await _context.Reactions.Where(x => x.PostId == postId && x.IsPositive == true).CountAsync();
            var negativeReactions = await _context.Reactions.Where(x => x.PostId == postId && x.IsPositive == false).CountAsync();

            PostReactionDto postReactionDto = new PostReactionDto
            {
                PositveReactions = positiveReactions,
                NegativeReactions = negativeReactions
            };

            var comments = await _context.Comments.Where(x => x.PostId == postId).Include(x => x.User).Select(x => new PostCommentDto
            {
                CommentId = x.Id,
                UserId = x.UserId,
                UserLogin = x.User.Login,
                Content = x.Content,
                CreatedAt = x.CreatedAt
            }).ToListAsync();



           
            
                var reaction = await _context.Reactions.Where(x => x.PostId == postId && x.UserId == userId).FirstOrDefaultAsync();
                // var reaction =  await _context.Posts.Where(x => x.Id == postId && x.UserId == userId).FirstOrDefaultAsync();
                if (reaction != null)
                {
                    bool userReaction = reaction.IsPositive;
                    BoolDto boolDto = new BoolDto(userReaction);

                    

                    var postDetailsDto = await _context.Posts.Where(x => x.Id == postId).Select(x => new PostDetailsDto
                    {
                        PostId = postId,
                        UserId = x.UserId,
                        AuthorLogin = x.User.Login,
                        Description = x.Description,
                        CreateAt = x.CreateAt,
                        UserReaction = boolDto,
                        Photos = postPhotoDto,
                        Reactions = postReactionDto,
                        Comments = comments

                    }).FirstOrDefaultAsync();

                    return postDetailsDto;
                }
                else
                {
                    var postDetailsDto = await _context.Posts.Where(x => x.Id == postId).Select(x => new PostDetailsDto
                    {
                        PostId = postId,
                        UserId = x.UserId,
                        AuthorLogin = x.User.Login,
                        Description = x.Description,
                        CreateAt = x.CreateAt,
                        Photos = postPhotoDto,
                        Reactions = postReactionDto,
                        Comments = comments

                    }).FirstOrDefaultAsync();

                    return postDetailsDto;
                }

                
            



        }
    }
}