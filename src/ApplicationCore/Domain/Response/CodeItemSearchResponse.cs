using Newtonsoft.Json;
using CleanArchitectureSample.Utilities;
using System.Collections.Generic;

namespace CleanArchitectureSample.ApplicationCore.Domain.Response
{
    public class CodeItemSearchResponse : BizResponse<CodeItemSearchResponseContent>
    {
    }
    public class CodeItemSearchResponseContent : SearchResult
    {
        public List<CodeItem> CodeItems { set; get; } = new List<CodeItem>();
        public class CodeItem
        {
            public string CodeId { get; set; }
            public string Category { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public int? SeqNo { get; set; }
            public string Remarks { get; set; }
        }
    }
}
