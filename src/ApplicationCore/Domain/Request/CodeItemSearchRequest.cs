using Newtonsoft.Json;
using CleanArchitectureSample.Utilities;
using System.Collections.Generic;

namespace CleanArchitectureSample.ApplicationCore.Domain.Request
{
    public class CodeItemSearchRequest : BizRequest<CodeItemSearchRequestContent>
    {
    }
    public class CodeItemSearchRequestContent : SearchCriteria
    {
        public List<string> Categories { get; set; } = new List<string>();
        public string CodeName { get; set; }
    }
}
