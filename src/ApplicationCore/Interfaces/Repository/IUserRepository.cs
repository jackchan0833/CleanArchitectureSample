using CleanArchitectureSample.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.ApplicationCore.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<string> Create(User entity);
        Task<User> GetByUserName(string userName);
        Task<User> GetByUserId(string userId);
        Task<bool> Update(User entity);
        Task<bool> DeleteByUserName(string userName);
        Task<bool> DeleteByUserId(string userId);
        Task<SearchResult<User>> Search(int pageIndex, int pageSize, string userName, string email, string status);
    }
}
