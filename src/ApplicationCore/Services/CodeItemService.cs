using CleanArchitectureSample.ApplicationCore.Domain.Entities;
using CleanArchitectureSample.ApplicationCore.Domain.Request;
using CleanArchitectureSample.ApplicationCore.Domain.Response;
using CleanArchitectureSample.ApplicationCore.Interfaces.Repository;
using CleanArchitectureSample.ApplicationCore.Interfaces.Service;
using CleanArchitectureSample.Utilities;
using CleanArchitectureSample.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArchitectureSample.ApplicationCore.Services
{
    public class CodeItemService : ICodeItemService
    {
        private readonly ICodeItemRepository _CodeItemRepository;
        public CodeItemService(ICodeItemRepository codeItemRepository)
        {
            _CodeItemRepository = codeItemRepository;
        }

        public async Task<CodeItemCreateResponse> Create(CodeItemCreateRequest request)
        {
            var response = new CodeItemCreateResponse();
            var requestContent = request.Content;
            var codeItem = await _CodeItemRepository.Get(requestContent.Category, requestContent.Name);
            if (codeItem != null)
            {
                response.Fail(Errors.RecordExistsAlready);
                return response;
            }
            codeItem = new CodeItem();
            codeItem.Category = requestContent.Category;
            codeItem.CodeName = requestContent.Name;
            codeItem.CodeValue = requestContent.Value;
            codeItem.Remarks = requestContent.Remarks;
            codeItem.SeqNo = requestContent.SeqNo;
            string codeId = await _CodeItemRepository.Create(codeItem);
            response.Content.CodeId = codeId;

            response.Success();
            return response;
        }

        public async Task<CodeItemDetailResponse> Get(string category, string name)
        {
            var response = new CodeItemDetailResponse();

            var entity = await _CodeItemRepository.Get(category, name);
            if (entity == null)
            {
                response.Fail(Errors.RecordNotFound);
                return response;
            }
            response.Content.CodeId = entity.CodeId;
            response.Content.Category = entity.Category;
            response.Content.Name = entity.CodeName;
            response.Content.Value = entity.CodeValue;
            response.Content.Remarks = entity.Remarks;
            response.Content.SeqNo = entity.SeqNo;
            response.Success();
            return response;
        }

        public async Task<CodeItemDetailResponse> Get(string codeId)
        {
            var response = new CodeItemDetailResponse();

            var entity = await _CodeItemRepository.Get(codeId);
            if (entity == null)
            {
                response.Fail(Errors.RecordNotFound);
                return response;
            }
            response.Content.CodeId = entity.CodeId;
            response.Content.Category = entity.Category;
            response.Content.Name = entity.CodeName;
            response.Content.Value = entity.CodeValue;
            response.Content.Remarks = entity.Remarks;
            response.Content.SeqNo = entity.SeqNo;
            response.Success();
            return response;
        }

        public async Task<List<string>> GetAllCategories()
        {
            return await _CodeItemRepository.GetAllCategories();
        }

        public async Task<CodeItemSearchResponse> Search(CodeItemSearchRequest request)
        {
            var response = new CodeItemSearchResponse();
            var searchResult = await _CodeItemRepository.Search(request.Content.PageIndex, request.Content.PageSize, request.Content.Categories?.ToArray(), request.Content.CodeName);
            foreach (var entity in searchResult.Records)
            {
                response.Content.CodeItems.Add(new CodeItemSearchResponseContent.CodeItem()
                {
                    CodeId = entity.CodeId,
                    Category = entity.Category,
                    Name = entity.CodeName,
                    Value = entity.CodeValue,
                    SeqNo = entity.SeqNo,
                    Remarks = entity.Remarks,
                });
            }

            response.Content.PageSize = searchResult.PageSize;
            response.Content.PageIndex = searchResult.PageIndex;
            response.Content.TotalCount = searchResult.TotalCount;
            response.Success();
            return response;
        }

        public async Task<BizResponse> Update(CodeItemUpdateRequest request)
        {
            var response = new BizResponse();
            var requestContent = request.Content;
            var codeItem = await _CodeItemRepository.Get(requestContent.CodeId);
            if (codeItem == null)
            {
                response.Fail(Errors.RecordNotFound);
                return response;
            }
            codeItem.Category = requestContent.Category;
            codeItem.CodeName = requestContent.Name;
            codeItem.CodeValue = requestContent.Value;
            codeItem.Remarks = requestContent.Remarks;
            codeItem.SeqNo = requestContent.SeqNo;
            await _CodeItemRepository.Update(codeItem);

            response.Success();
            return response;
        }
    }
}
