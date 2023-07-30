﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CleanArchitectureSample.WebApp.Models.User
{
    public class UserEditViewModel
    {
        public List<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
