using CleanArchitectureSample.ApplicationCore.Domain.Request;
using CleanArchitectureSample.ApplicationCore.Domain.Response;
using CleanArchitectureSample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.ApplicationCore.Interfaces.Service
{
    public interface IUserService
    {
        Task<UserCreateResponse> Create(UserCreateRequest request);
        Task<UserDetailResponse> GetByUserId(string userId);
        Task<UserDetailResponse> GetByUserName(string userName);
        Task<BizResponse> Update(UserUpdateRequest request);
        Task<BizResponse> DeleteByUserName(string userName);
        Task<UserSearchResponse> Search(UserSearchRequest request);
    }
}
