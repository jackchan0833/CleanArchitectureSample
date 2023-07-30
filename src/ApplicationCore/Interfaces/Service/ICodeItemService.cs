using CleanArchitectureSample.ApplicationCore.Domain.Request;
using CleanArchitectureSample.ApplicationCore.Domain.Response;
using CleanArchitectureSample.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.ApplicationCore.Interfaces.Service
{
    public interface ICodeItemService
    {
        Task<CodeItemCreateResponse> Create(CodeItemCreateRequest request);
        Task<BizResponse> Update(CodeItemUpdateRequest request);
        Task<CodeItemDetailResponse> Get(string category, string name);
        Task<CodeItemDetailResponse> Get(string codeId);
        Task<CodeItemSearchResponse> Search(CodeItemSearchRequest request);
        Task<List<string>> GetAllCategories();
    }
}
