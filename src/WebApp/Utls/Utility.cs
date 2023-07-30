using CleanArchitectureSample.ApplicationCore.Domain.Request;
using CleanArchitectureSample.ApplicationCore.Interfaces.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitectureSample.WebApp.Utls
{
    public class Utility
    {
        ICodeItemService _CodeItemService;
        public Utility(ICodeItemService codeItemService)
        {
            _CodeItemService = codeItemService;
        }
        public async Task<string> GetCodeValue(string category, string name)
        {
            var codeItemResponse = await _CodeItemService.Get(category, name);
            if (codeItemResponse.IsSuccess)
            {
                return codeItemResponse.Content.Value;
            }
            return null;
        }
        public async Task<List<SelectListItem>> GetAllCategories(bool hasEmptySelectItem  = false, string defaultEmptyText = "--Select--")
        {
            var result = new List<SelectListItem>();
            var categories = await _CodeItemService.GetAllCategories();
            foreach (var category in categories)
            {
                result.Add(new SelectListItem()
                {
                    Text = category,
                    Value = category
                });
            }
            if (hasEmptySelectItem)
            {
                result.Insert(0, new SelectListItem()
                {
                    Value = string.Empty,
                    Text = defaultEmptyText
                });
            }
            return result;
        }
        public async Task<List<SelectListItem>> GetSelectItems(string category, bool hasEmptySelectItem = false, string defaultEmptyText = "--Select--")
        {
            var result = new List<SelectListItem>();
            if (!string.IsNullOrWhiteSpace(category))
            {
                var request = new CodeItemSearchRequest();
                request.Content.PageIndex = 1;
                request.Content.PageSize = 99999;
                request.Content.Categories.Add(category);
                var searchResponse = await _CodeItemService.Search(request);
                if (searchResponse.IsSuccess)
                {
                    foreach (var codeItem in searchResponse.Content.CodeItems)
                    {
                        result.Add(new SelectListItem()
                        {
                            Text = codeItem.Value,
                            Value = codeItem.Name
                        });
                    }
                }
            }
            if (hasEmptySelectItem)
            {
                result.Insert(0, new SelectListItem()
                {
                    Value = string.Empty,
                    Text = defaultEmptyText
                });
            }
            return result;
        }
    }
}
