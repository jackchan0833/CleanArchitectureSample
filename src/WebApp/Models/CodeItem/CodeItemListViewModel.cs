using CleanArchitectureSample.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CleanArchitectureSample.WebApp.Models.CodeItem
{
    public class CodeItemListViewModel
    {
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public SearchInfo Search { set; get; } = new SearchInfo();
        public int TotalCount { set; get; }
        public List<CodeItem> CodeItems { get; set; } = new List<CodeItem>();
        public class CodeItem
        {
            public string CodeId { get; set; }
            public string Category { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public int? SeqNo { get; set; }
            public string Remarks { get; set; }
        }
        public class SearchInfo : SearchCriteria
        {
            public string Category { set; get; }
            public string Name { set; get; }
        }
    }
}
