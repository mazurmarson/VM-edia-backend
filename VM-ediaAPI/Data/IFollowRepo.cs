using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public interface IFollowRepo : IGenRepo
    {
        Task<IEnumerable<Follow>> GetUserFollows(int userId);
        Task<Follow> GetFollow(int id);
        Task<Follow> AddFollow(Follow follow);

        Task<bool> UserIsExist(int id);
    }
}