using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Helpers;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public interface IUserRepo : IGenRepo
    {
         Task<RegisterUserDto> Register(RegisterUserDto registerUserDto);
         Task<string> GenerateJwt(LoginDto dto);
         Task<IEnumerable<User>> GetUsers();
         Task<User> EditUser(User user, string newPassword);
         Task<User> GetUserById(int id);
          Task<DetailsUserDto> GetUserDetails(int id, int userId, PageParameters pageParameters);

          Task<DetailsUserLoggedDto> GetUserDetailsLogged(int id, int loggedUserId);

          Task<IEnumerable<UserFollowersDto>> GetUserFollowers(int id);
          Task<IEnumerable<UserFollowingDto>> GetUserFollowing(int id);
          
         //   Task<bool> IsFollowed(int userId, int detailUserId);
             Task<PagedList<UsersDisplayDto>> GetSearchedAndSortedUsers(PageParameters pageParameters, string searchString);

             Task<IEnumerable<WallDto>> GetWall(int id);
         
    }
}