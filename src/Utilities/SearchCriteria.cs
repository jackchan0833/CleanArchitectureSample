using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureSample.Utilities
{
    public class SearchCriteria
    {
        [JsonProperty("page_index")]
        public int PageIndex { get; set; }
        [JsonProperty("page_size")]
        public int PageSize { get; set; } = 10;
    }
}
