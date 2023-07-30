using CleanArchitectureSample.ApplicationCore.Domain.Request;
using CleanArchitectureSample.ApplicationCore.Interfaces.Service;
using CleanArchitectureSample.Utilities.Constants;
using CleanArchitectureSample.WebApp.Models;
using CleanArchitectureSample.WebApp.Models.User;
using CleanArchitectureSample.WebApp.Utls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CleanArchitectureSample.WebApp.Controllers
{
    public class UserController : Controller
    {
        IUserService _UserService;
        Utility _Utility;
        public UserController(IUserService codeItemService, Utility utility)
        {
            _UserService = codeItemService;
            _Utility = utility;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var model = new UserListViewModel();
            if (!TempData.TryGetData<UserListViewModel.SearchInfo>("mUserSearch", out var mUserSearch))
            {
                // not from search
                model.Search.PageSize = 2;
                model.Search.PageIndex = pageIndex < 1 ? 1 : pageIndex;
                await InitDropdownListData(model);
                return View(model);
            }

            //search 
            var request = new UserSearchRequest();
            request.Content.PageSize = mUserSearch.PageSize;
            request.Content.PageIndex = mUserSearch.PageIndex;
            request.Content.Status = mUserSearch.Status;
            request.Content.UserName = mUserSearch.Username?.Trim();
            request.Content.Email = mUserSearch.Email?.Trim();
            var response = await _UserService.Search(request);
            if (response.IsError)
            {
                TempData.SaveData("ErrorMessage", response.SubMsg);
                await InitDropdownListData(model);
                return View(model);
            }

            model.TotalCount = response.Content.TotalCount;
            model.Users.Clear();
            foreach (var item in response.Content.Users)
            {
                var user = new UserListViewModel.User()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    NickName = item.NickName,
                    Email = item.Email,
                    Status = item.Status,
                };
                user.StatusDesc = await _Utility.GetCodeValue(CategoryNames.UserStatus, item.Status);
                model.Users.Add(user);
            }

            await InitDropdownListData(model);
            model.Search = mUserSearch;
            TempData.SaveData("mUserSearch", mUserSearch);
            return View(model);
        }
        [HttpPost]
        public IActionResult Search(UserListViewModel model)
        {
            model.Search.PageIndex = 1;
            TempData.SaveData("mUserSearch", model.Search);
            return RedirectToAction("Index");
        }
        public IActionResult PageIndex(int pageIndex = 1)
        {
            if (TempData.TryGetData<UserListViewModel.SearchInfo>("mUserSearch", out var mUserSearch))
            {
                mUserSearch.PageIndex = pageIndex < 1 ? 1 : pageIndex;
                TempData.SaveData("mUserSearch", mUserSearch);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", new { @pageIndex = pageIndex });
            }
        }

        private async Task InitDropdownListData(UserListViewModel model)
        {
            model.Statuses = await _Utility.GetSelectItems(CategoryNames.UserStatus, true);
        }
        public async Task<IActionResult> Create()
        {
            var model = new UserCreateViewModel();
            await InitCreateDropdownListData(model);
            return View(model);
        }
        private async Task InitCreateDropdownListData(UserCreateViewModel model)
        {
            model.Statuses = await _Utility.GetSelectItems(CategoryNames.UserStatus, true);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            var request = new UserCreateRequest();
            request.Content.UserName = model.UserName.Trim();
            request.Content.NickName = model.NickName.Trim();
            request.Content.Email = model.Email?.Trim();
            request.Content.Address = model.Address?.Trim();
            request.Content.Status = model.Status;
            var response = await _UserService.Create(request);
            if (response.IsError)
            {
                TempData.SaveData("ErrorMessage", response.SubMsg);
                await InitCreateDropdownListData(model);
                return View(model);
            }
            return RedirectToAction("Detail", new { id = response.Content.UserId });
        }
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var response = await _UserService.GetByUserId(id);
            if (response.IsError)
            {
                return RedirectToAction("Index", "Error", new { code = response.SubCode });
            }
            var model = new UserDetailViewModel();
            model.UserId = response.Content.UserId;
            model.UserName = response.Content.UserName;
            model.NickName = response.Content.NickName;
            model.Email = response.Content.Email;
            model.Address = response.Content.Address;
            model.Status = response.Content.Status;
            model.StatusDesc = await _Utility.GetCodeValue(CategoryNames.UserStatus, response.Content.Status);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _UserService.GetByUserId(id);
            if (response.IsError)
            {
                return RedirectToAction("Index", "Error", new { code = response.SubCode });
            }
            var model = new UserEditViewModel();
            model.UserId = response.Content.UserId;
            model.UserName = response.Content.UserName;
            model.NickName = response.Content.NickName;
            model.Email = response.Content.Email;
            model.Address = response.Content.Address;
            model.Status = response.Content.Status;

            await InitEditDropdownListData(model);
            return View(model);
        }
        private async Task InitEditDropdownListData(UserEditViewModel model)
        {
            model.Statuses = await _Utility.GetSelectItems(CategoryNames.UserStatus, true);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            var request = new UserUpdateRequest();
            request.Content.UserId = model.UserId;
            request.Content.NickName = model.NickName.Trim();
            request.Content.Email = model.Email?.Trim();
            request.Content.Address = model.Address?.Trim();
            request.Content.Status = model.Status;
            var response = await _UserService.Update(request);
            if (response.IsError)
            {
                TempData.SaveData("ErrorMessage", response.SubMsg);
                await InitEditDropdownListData(model);
                return View(model);
            }
            return RedirectToAction("Detail", new { id = model.UserId });
        }

    }
}