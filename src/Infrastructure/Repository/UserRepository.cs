using CleanArchitectureSample.ApplicationCore.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureSample.ApplicationCore.Domain.Entities;
using Newtonsoft.Json;
using CleanArchitectureSample.Utilities;
using CleanArchitectureSample.Utilities.Constants;
using CleanArchitectureSample.Infrastructure.Mapping;
using CleanArchitectureSample.ApplicationCore.Domain.Request;

namespace CleanArchitectureSample.Infrastructure.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly SampleDbContext _DbContext;
        public UserRepository(SampleDbContext dbContext): base(dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<string> Create(User entity)
        {
            var dataEntity = new Entities.User();
            dataEntity = entity.ToDataEntity(dataEntity);
            dataEntity.UserId = System.Guid.NewGuid().ToString();
            this._DbContext.Users.Add(dataEntity);
            await this._DbContext.SaveChangesAsync();
            return dataEntity.UserId;
        }

        public async Task<bool> DeleteByUserId(string userId)
        {
            var dataEntity = this._DbContext.Users.FirstOrDefault(th => th.UserId == userId);
            if (dataEntity == null)
            {
                return false;
            }
            this._DbContext.Users.Remove(dataEntity);
            await this._DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByUserName(string userName)
        {
            var dataEntity = this._DbContext.Users.FirstOrDefault(th => th.UserName == userName);
            if (dataEntity == null)
            {
                return false;
            }
            this._DbContext.Users.Remove(dataEntity);
            await this._DbContext.SaveChangesAsync();
            return true;
        }


        public async Task<User> GetByUserId(string userId)
        {
            var dataEntity = this._DbContext.Users.FirstOrDefault(th => th.UserId == userId);
            if (dataEntity == null)
            {
                return null;
            }
            var entity = new User();
            entity = dataEntity.ToDomainEntity(entity);
            await Task.CompletedTask;
            return entity;
        }

        public async Task<User> GetByUserName(string userName)
        {
            var dataEntity = this._DbContext.Users.FirstOrDefault(th => th.UserName == userName);
            if (dataEntity == null)
            {
                return null;
            }
            var entity = new User();
            entity = dataEntity.ToDomainEntity(entity);

            await Task.CompletedTask;
            return entity;
        }

        public async Task<SearchResult<User>> Search(int pageIndex, int pageSize, string userName, string email, string status)
        {
            var result = new SearchResult<User>();
            var query = this._DbContext.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                query = query.Where(th => th.UserName.Contains(userName));
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(th => th.Email.Contains(email));
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(th => th.Status == status);
            }

            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var listDataEntities = query.TakePagination(pageIndex, pageSize).ToList();
            foreach(var dataEntity  in listDataEntities)
            {
                var entity = new User();
                entity = dataEntity.ToDomainEntity(entity);
                result.Records.Add(entity);
            }

            await Task.CompletedTask;
            return result;
        }

        public async Task<bool> Update(User entity)
        {
            var dataEntity = this._DbContext.Users.FirstOrDefault(th => th.UserId == entity.UserId);
            if (dataEntity == null)
            {
                return false;
            }
            dataEntity = entity.ToDataEntity(dataEntity);
            await this._DbContext.SaveChangesAsync();
            return true;
        }
    }
}
