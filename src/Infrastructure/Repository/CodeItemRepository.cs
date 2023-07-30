using CleanArchitectureSample.ApplicationCore.Domain.Entities;
using CleanArchitectureSample.ApplicationCore.Interfaces.Repository;
using CleanArchitectureSample.Infrastructure.Mapping;
using CleanArchitectureSample.Utilities;
using CleanArchitectureSample.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArchitectureSample.Infrastructure.Repository
{
    public class CodeItemRepository : BaseRepository, ICodeItemRepository
    {
        private readonly SampleDbContext _DbContext;
        public CodeItemRepository(SampleDbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<string> Create(CodeItem entity)
        {
            var dataEntity = new Entities.CodeItem();
            dataEntity = entity.ToDataEntity(dataEntity);
            dataEntity.CodeId = System.Guid.NewGuid().ToString();
            this._DbContext.CodeItems.Add(dataEntity);
            await this._DbContext.SaveChangesAsync();
            return dataEntity.CodeId;
        }

        public async Task<CodeItem> Get(string category, string name)
        {
            CodeItem entity = null;
            var dataEntity = this._DbContext.CodeItems.FirstOrDefault(th => th.Category == category && th.CodeName == name);
            if (dataEntity == null)
            {
                return entity;
            }

            entity = new CodeItem();
            entity = dataEntity.ToDomainEntity(entity);

            await Task.CompletedTask;
            return entity;
        }

        public async Task<List<CodeItem>> GetByCategories(params string[] categoryNames)
        {
            List<CodeItem> result = new List<CodeItem>();
            foreach(var categoryName in categoryNames)
            {
                var items = await GetByCategory(categoryName);
                result.AddRange(items);
            }
            return result;
        }

        public async Task<List<CodeItem>> GetByCategory(string categoryName)
        {
            List<CodeItem> result = new List<CodeItem>();
            var listDataEntities = this._DbContext.CodeItems.Where(th => th.Category == categoryName).ToList();
            foreach (var dataEntity in listDataEntities)
            {
                var item = new CodeItem();
                item = dataEntity.ToDomainEntity(item);
                result.Add(item);
            }

            await Task.CompletedTask;
            return result;
        }

        public async Task<bool> Update(CodeItem entity)
        {
            var dataEntity = this._DbContext.CodeItems.FirstOrDefault(th => th.CodeId == entity.CodeId);
            if (dataEntity == null)
            {
                return false;
            }

            dataEntity = entity.ToDataEntity(dataEntity);
            await this._DbContext.SaveChangesAsync();

            return true;
        }

        public Task<CodeItem> Get(string codeId)
        {
            CodeItem entity = null;
            var dataEntity = this._DbContext.CodeItems.FirstOrDefault(th => th.CodeId == codeId);
            if (dataEntity == null)
            {
                return Task.FromResult(entity);
            }

            entity = new CodeItem();
            entity = dataEntity.ToDomainEntity(entity);

            return Task.FromResult(entity);
        }

        public async Task<List<string>> GetAllCategories()
        {
            await Task.CompletedTask;
            return this._DbContext.CodeItems.Select(th => th.Category).Distinct().ToList();
        }

        public async Task<SearchResult<CodeItem>> Search(int pageIndex, int pageSize, string[] categoryNames, string codeName)
        {
            var result = new SearchResult<CodeItem>();
            var query = this._DbContext.CodeItems.AsQueryable();
            if (categoryNames != null && categoryNames.Any())
            {
                query = query.Where(th => categoryNames.Contains(th.Category));
            }
            if (!string.IsNullOrWhiteSpace(codeName))
            {
                query = query.Where(th => th.CodeName.Contains(codeName));
            }
            result.PageSize = pageSize;
            result.PageIndex = pageIndex;
            result.TotalCount = query.Count();
            var listDataEntities = query.TakePagination(pageIndex, pageSize).ToList();
            foreach (var item in listDataEntities)
            {
                var entity = new CodeItem();
                entity = item.ToDomainEntity(entity);
                result.Records.Add(entity);
            }

            await Task.CompletedTask;
            return result;
        }
    }
}
