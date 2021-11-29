using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Exceptions;
using VM_ediaAPI.Helpers;
using VM_ediaAPI.Models;


namespace VM_ediaAPI.Data
{
    public class UserRepo : GenRepo, IUserRepo
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;
        public UserRepo(DataContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IMapper mapper):base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
        }

        public async Task<RegisterUserDto> Register(RegisterUserDto registerUserDto)
        {
            var user = new User() 
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Login = registerUserDto.Login,
                Mail = registerUserDto.Mail,
                DateOfBirth = registerUserDto.DateOfBirth,
                Description = registerUserDto.Description,
                MainPhotoUrl = registerUserDto.MainPhotoUrl
                

            };
            var hashedPassword = _passwordHasher.HashPassword(user, registerUserDto.Password);
            user.PasswordHash = hashedPassword;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return registerUserDto;
        }

        


        public async Task<string> GenerateJwt(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Mail == dto.Mail);

            if(user is null)
            {
                throw new BadRequestException("Inavalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Inavalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                //Tu bedzie można dodać role
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials:cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> EditUser(User user, string newPassword)
        {
            
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<DetailsUserDto> GetUserDetails(int id, int userId, PageParameters pageParameters)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id ==id);
            if(user == null)
            {
                return null;
            }
            int followers = await _context.Follows.CountAsync(x => x.FollowedUserId == id);
            int following = await _context.Follows.CountAsync(x => x.FollowerId == id);
            int followingId = 0;

            var posts = await _context.Posts.Where(x => x.UserId == id).OrderByDescending(x => x.CreateAt).Select(x => new UserDetailsPostDto{
                PostId = x.Id,
                PhotoId = x.Photos.OrderBy(x => x.Id).Select(x => x.Id).FirstOrDefault(),
                UrlAdress = x.Photos.OrderBy(x => x.Id).Select(x => x.UrlAdress).FirstOrDefault(),
                MoreThanOne = !x.Photos.Count().Equals(1)
            }).ToListAsync();
      //     var post = await _context.Posts.Where(x => x.UserId == id).OrderBy(x => x.CreateAt).ToListAsync();
       //     var postsDto = _mapper.Map<List<Post>, List<UserDetailsPhotoDto>>(post);
            // var photos = await _context.Photos.Where(x => x.UserId == id).Select(x => new PhotoUserDto()
            // {
            //     Id = x.Id,
            //     UserId = x.UserId,
         
            //     UrlAdress = x.UrlAdress,
     

            // }).ToListAsync();

            if(userId != 0)
            {
                var isFollowed = await _context.Follows.AnyAsync(x => x.FollowedUser.Id == id && x.FollowerId == userId);
                if(isFollowed == true)
                {
                    followingId = await _context.Follows.Where(x => x.FollowedUser.Id == id && x.FollowerId == userId).Select(x => x.Id).FirstOrDefaultAsync();
                }
                    
            }
            

            DetailsUserDto detailsUserDto = new DetailsUserDto
            {
                Id = user.Id,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                MainPhotoUrl = user.MainPhotoUrl,
                AmountFollowers = followers,
                AmoutnFollowing = following,
                FollowingId = followingId,
                Posts = PagedList<UserDetailsPostDto>.ToPagedList(posts, pageParameters.PageNumber, pageParameters.PageSize)
               // Photos = photos
            };

            return detailsUserDto;
        }

        public async Task<DetailsUserLoggedDto> GetUserDetailsLogged(int id, int loggedUserId)
        {
           
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id ==id);
            int followers = await _context.Follows.CountAsync(x => x.FollowedUserId == id);
            int following = await _context.Follows.CountAsync(x => x.FollowerId == id);
            var isFollowed = await _context.Follows.AnyAsync(x => x.FollowedUser.Id == id && x.FollowerId == loggedUserId);
            // var photos = await _context.Photos.Where(x => x.UserId == id).Select(x => new PhotoUserDto()
            // {
            //     Id = x.Id,
            //     UserId = x.UserId,
         
            //     UrlAdress = x.UrlAdress,
     

            // }).ToListAsync();

            DetailsUserLoggedDto detailsUserLoggedDto = new DetailsUserLoggedDto
            {
                Id = user.Id,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                MainPhotoUrl = user.MainPhotoUrl,
                AmountFollowers = followers,
                AmoutnFollowing = following,
                isFollowed = isFollowed
               // Photos = photos
            };

            return detailsUserLoggedDto;
        }

        public async Task<IEnumerable<UserFollowersDto>> GetUserFollowers(int id)
        {
            var users = await _context.Follows.Where(x => x.FollowedUserId == id).Include(x => x.Follower).Select(x => new UserFollowersDto()
            {
                Id = x.Id,
                FollowerId = x.FollowerId,
                Login = x.Follower.Login,
                MainPhotoUrl = x.Follower.MainPhotoUrl
            }).ToListAsync();
            return users;
        }

        public async Task<IEnumerable<UserFollowingDto>> GetUserFollowing(int id)
        {
            var users = await _context.Follows.Where(x => x.FollowerId == id).Include(x => x.FollowedUser).Select(x => new UserFollowingDto()
            {
                Id = x.Id,
                FollowingId = x.FollowedUserId,
                Login = x.FollowedUser.Login,
                MainPhotoUrl = x.FollowedUser.MainPhotoUrl
            }).ToListAsync();

            return users;
        }

        public Task<bool> IsFollowed(int userId, int detailUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<UsersDisplayDto>> GetSearchedAndSortedUsers(PageParameters pageParameters, string searchString)
        {
            searchString = searchString.ToLower();
            var users = await _context.Users.Where(x => x.Login.ToLower().Contains(searchString) || x.FirstName.ToLower().Contains(searchString) || x.LastName.ToLower().Contains(searchString)).ToListAsync();
            List<UsersDisplayDto> usersToReturn = _mapper.Map<List<UsersDisplayDto>>(users);

            return PagedList<UsersDisplayDto>.ToPagedList(usersToReturn, pageParameters.PageNumber, pageParameters.PageSize);
        }

        public async Task<IEnumerable<WallDto>> GetWall(int id)
        {
             var follows = await _context.Follows.Where(x => x.FollowerId == id).Select(x => x.FollowedUserId).ToListAsync();
             List<WallDto> wallDto = new List<WallDto>();
             foreach(var follow in follows)
             {
                    var post = await  _context.Posts.Include(x => x.Photos).Include(x => x.Reactions).Include(x => x.User).Include(x => x.Comments).Where(x => x.UserId == follow).Select(x => new WallDto() {
                     UserId = x.UserId,
                     Login = x.User.Login,
                     MainPhotoUrl = x.User.MainPhotoUrl,
                     PostId = x.Id,
                     CreateAt = x.CreateAt,
                     Description = x.Description,
                     PositiveReactions = x.Reactions.Where(x => x.IsPositive == true).Count(),
                     NegativeReactions = x.Reactions.Where(x => x.IsPositive == false).Count(),
                      Photos = _mapper.Map<List<PostPhotoDto>>( x.Photos.Where(z => z.PostId == x.Id).ToList()),
                      Comments = x.Comments.Where(z => z.PostId == x.Id).Select( u => new PostCommentDto() {
                          CommentId = u.Id,
                        UserId = u.UserId,
                        UserLogin = u.User.Login,
                        Content = u.Content,
                        CreatedAt = u.CreatedAt
                      }).ToList()
                 }).OrderByDescending(x => x.CreateAt).AsSingleQuery().FirstOrDefaultAsync();
                 if(post != null)
                 {
                     wallDto.Add(post);
                 }
             }
            // var post = await _context.Posts.ForEachAsync(follows, x => follows)
            return wallDto;
            
        }





        //Obsluga przykłądowego bledu NotFound w metodzie GET
        //If(smomething is null)
        //throw new NotFoundException("Something not found")
    }
}