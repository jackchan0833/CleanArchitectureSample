using CleanArchitectureSample.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CleanArchitectureSample.WebApp.Models.User
{
    public class UserListViewModel
    {
        public List<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
        public SearchInfo Search { set; get; } = new SearchInfo();
        public int TotalCount { set; get; }
        public class SearchInfo : SearchCriteria
        {
            public string Username { set; get; }
            public string Nickname { set; get; }
            public string Email { set; get; }
            public string Status { set; get; }
        }
        public List<User> Users { get; set; } = new List<User>();
        public class User
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string NickName { get; set; }
            public string Email { get; set; }
            public string Status { set; get; }
            public string StatusDesc { get; set; }
        }
    }
}
