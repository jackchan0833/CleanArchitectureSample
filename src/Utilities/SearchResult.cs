using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureSample.Utilities
{
    public class SearchResult
    {
        /// <summary>
        /// Start from 1.
        /// </summary>
        [JsonProperty("page_index")]
        public int PageIndex { get; set; }
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}
