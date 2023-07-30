using CleanArchitectureSample.ApplicationCore.Domain.Request;
using CleanArchitectureSample.ApplicationCore.Domain.Entities;
using CleanArchitectureSample.ApplicationCore.Interfaces.Repository;
using CleanArchitectureSample.ApplicationCore.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureSample.ApplicationCore.Domain.Response;
using CleanArchitectureSample.Utilities.Constants;
using CleanArchitectureSample.Utilities;

namespace CleanArchitectureSample.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task<UserCreateResponse> Create(UserCreateRequest request)
        {
            var response = new UserCreateResponse();
            var requestContent = request.Content;

            var existUser = await _UserRepository.GetByUserName(requestContent.UserName);
            if (existUser != null)
            {
                response.Fail(Errors.UserNameExistAlready);
                return response;
            }
            var user = new User();
            user.UserName = requestContent.UserName;
            user.NickName = requestContent.NickName;
            user.Email = requestContent.Email;
            user.Address = requestContent.Address;
            user.Status = requestContent.Status;
            string userId = await _UserRepository.Create(user);
            response.Content.UserId = userId;
            response.Success();
            return response;
        }

        public async Task<BizResponse> DeleteByUserName(string userName)
        {
            var response = new BizResponse();
            await _UserRepository.DeleteByUserName(userName);

            response.Success();
            return response;
        }

        public async Task<UserDetailResponse> GetByUserId(string userId)
        {
            var response = new UserDetailResponse();
            var user = await _UserRepository.GetByUserId(userId);
            if (user == null)
            {
                response.Fail(Errors.RecordNotFound);
                return response;
            }
            response.Content.UserId = user.UserId;
            response.Content.UserName = user.UserName;
            response.Content.NickName = user.NickName;
            response.Content.Email = user.Email;
            response.Content.Address = user.Address;
            response.Content.Status = user.Status;

            response.Success();
            return response;
        }

        public async Task<UserDetailResponse> GetByUserName(string userName)
        {
            var response = new UserDetailResponse();
            var user = await _UserRepository.GetByUserName(userName);
            if (user == null)
            {
                response.Fail(Errors.RecordNotFound);
                return response;
            }
            response.Content.UserId = user.UserId;
            response.Content.UserName = user.UserName;
            response.Content.NickName = user.NickName;
            response.Content.Email = user.Email;
            response.Content.Address = user.Address;
            response.Content.Status = user.Status;

            response.Success();
            return response;
        }

        public async Task<UserSearchResponse> Search(UserSearchRequest request)
        {
            var response = new UserSearchResponse();
            var searchResult = await _UserRepository.Search(request.Content.PageIndex, request.Content.PageSize, request.Content.UserName, request.Content.Email, request.Content.Status);
            foreach (var entity in searchResult.Records)
            {
                response.Content.Users.Add(new UserSearchResponseContent.UserInfo()
                {
                    UserId = entity.UserId,
                    UserName = entity.UserName,
                    NickName = entity.NickName,
                    Email = entity.Email,
                    Address = entity.Address,
                    Status = entity.Status,
                });
            }

            response.Content.PageSize = searchResult.PageSize;
            response.Content.PageIndex = searchResult.PageIndex;
            response.Content.TotalCount = searchResult.TotalCount;
            response.Success();
            return response;
        }

        public async Task<BizResponse> Update(UserUpdateRequest request)
        {
            var response = new BizResponse();

            var user = await _UserRepository.GetByUserId(request.Content.UserId);
            if (user == null)
            {
                response.Fail(Errors.RecordNotFound);
                return response;
            }
            user.NickName = request.Content.NickName;
            user.Email = request.Content.Email;
            user.Address = request.Content.Address;
            user.Status = request.Content.Status;
            await _UserRepository.Update(user);

            response.Success();
            return response;
        }
    }
}
