using CleanArchitectureSample.ApplicationCore.Domain.Request;
using CleanArchitectureSample.ApplicationCore.Interfaces.Service;
using CleanArchitectureSample.WebApp.Models;
using CleanArchitectureSample.WebApp.Models.CodeItem;
using CleanArchitectureSample.WebApp.Utls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CleanArchitectureSample.WebApp.Controllers
{
    public class CodeItemController : Controller
    {
        ICodeItemService _CodeItemService;
        Utility _Utility;
        public CodeItemController(ICodeItemService codeItemService, Utility utility)
        {
            _CodeItemService = codeItemService;
            _Utility = utility;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var model = new CodeItemListViewModel();
            if (!TempData.TryGetData<CodeItemListViewModel.SearchInfo>("mCodeItemSearch", out var mCodeItemSearch))
            {
                // not from search
                model.Search.PageSize = 2;
                model.Search.PageIndex = pageIndex < 1 ? 1 : pageIndex;
                await InitDropdownListData(model);
                return View(model);
            }

            //search 
            var request = new CodeItemSearchRequest();
            request.Content.PageSize = mCodeItemSearch.PageSize;
            request.Content.PageIndex = mCodeItemSearch.PageIndex;
            if (!string.IsNullOrWhiteSpace(mCodeItemSearch.Category))
            {
                request.Content.Categories.Add(mCodeItemSearch.Category);
            }
            request.Content.CodeName = mCodeItemSearch.Name?.Trim();
            var response = await _CodeItemService.Search(request);
            if (response.IsError)
            {
                TempData.SaveData("ErrorMessage", response.SubMsg);
                await InitDropdownListData(model);
                return View(model);
            }

            model.TotalCount = response.Content.TotalCount;
            model.CodeItems.Clear();
            foreach (var item in response.Content.CodeItems)
            {
                model.CodeItems.Add(new CodeItemListViewModel.CodeItem()
                {
                    CodeId = item.CodeId,
                    Category = item.Category,
                    Name = item.Name,
                    Value = item.Value,
                    Remarks = item.Remarks
                });
            }

            await InitDropdownListData(model);
            model.Search = mCodeItemSearch;
            TempData.SaveData("mCodeItemSearch", mCodeItemSearch);
            return View(model);
        }
        [HttpPost]
        public IActionResult Search(CodeItemListViewModel model)
        {
            model.Search.PageIndex = 1;
            TempData.SaveData("mCodeItemSearch", model.Search);
            return RedirectToAction("Index");
        }
        public IActionResult PageIndex(int pageIndex = 1)
        {
            if (TempData.TryGetData<CodeItemListViewModel.SearchInfo>("mCodeItemSearch", out var mCodeItemSearch))
            {
                mCodeItemSearch.PageIndex = pageIndex < 1 ? 1 : pageIndex;
                TempData.SaveData("mCodeItemSearch", mCodeItemSearch);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", new { @pageIndex = pageIndex });
            }
        }

        private async Task InitDropdownListData(CodeItemListViewModel model)
        {
            model.Categories = await _Utility.GetAllCategories(true);
        }
        public IActionResult Create(string category)
        {
            var model = new CodeItemCreateViewModel();
            if (!string.IsNullOrWhiteSpace(category))
            {
                model.Category = category.Trim();
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CodeItemCreateViewModel model)
        {
            var request = new CodeItemCreateRequest();
            request.Content.Category = model.Category.Trim();
            request.Content.Name = model.Name.Trim();
            request.Content.Value = model.Value?.Trim();
            request.Content.SeqNo = string.IsNullOrWhiteSpace(model.SeqNo) ? null : Convert.ToInt32(model.SeqNo.Trim());
            request.Content.Remarks = model.Remarks?.Trim();
            var response = await _CodeItemService.Create(request);
            if (response.IsError)
            {
                TempData.SaveData("ErrorMessage", response.SubMsg);
                return View(model);
            }
            return RedirectToAction("Detail", new { id = response.Content.CodeId });
        }
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var response = await _CodeItemService.Get(id);
            if (response.IsError)
            {
                return RedirectToAction("Index", "Error", new { code = response.SubCode });
            }
            var model = new CodeItemDetailViewModel();
            model.CodeId = response.Content.CodeId;
            model.Category = response.Content.Category;
            model.Name = response.Content.Name;
            model.Value = response.Content.Value;
            model.SeqNo = response.Content.SeqNo == null ? string.Empty : response.Content.SeqNo.Value.ToString();
            model.Remarks = response.Content.Remarks;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _CodeItemService.Get(id);
            if (response.IsError)
            {
                return RedirectToAction("Index", "Error", new { code = response.SubCode });
            }
            var model = new CodeItemEditViewModel();
            model.CodeId = response.Content.CodeId;
            model.Category = response.Content.Category;
            model.Name = response.Content.Name;
            model.Value = response.Content.Value;
            model.SeqNo = response.Content.SeqNo == null ? string.Empty : response.Content.SeqNo.Value.ToString();
            model.Remarks = response.Content.Remarks;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CodeItemEditViewModel model)
        {
            var request = new CodeItemUpdateRequest();
            request.Content.CodeId = model.CodeId;
            request.Content.Category = model.Category;
            request.Content.Name = model.Name?.Trim();
            request.Content.Value = model.Value?.Trim();
            request.Content.SeqNo = string.IsNullOrWhiteSpace(model.SeqNo) ? null : Convert.ToInt32(model.SeqNo);
            request.Content.Remarks = model.Remarks;
            var response = await _CodeItemService.Update(request);
            if (response.IsError)
            {
                TempData.SaveData("ErrorMessage", response.SubMsg);
                return View(model);
            }
            return RedirectToAction("Detail", new { id = model.CodeId });
        }

    }
}