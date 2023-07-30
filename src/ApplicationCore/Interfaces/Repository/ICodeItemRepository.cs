using CleanArchitectureSample.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.ApplicationCore.Interfaces.Repository
{
    public interface ICodeItemRepository
    {
        Task<string> Create(CodeItem entity);
        Task<bool> Update(CodeItem entity);
        Task<List<CodeItem>> GetByCategory(string categoryName);
        Task<List<CodeItem>> GetByCategories(params string[] categoryName);
        Task<CodeItem> Get(string category, string name);
        Task<SearchResult<CodeItem>> Search(int pageIndex, int pageSize, string[] categoryName, string codeName);
        Task<CodeItem> Get(string codeId);
        Task<List<string>> GetAllCategories();
    }
}
