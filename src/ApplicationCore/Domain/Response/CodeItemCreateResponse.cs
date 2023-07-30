using Newtonsoft.Json;
using CleanArchitectureSample.Utilities;
using System.Collections.Generic;

namespace CleanArchitectureSample.ApplicationCore.Domain.Response
{
    public class CodeItemCreateResponse : BizResponse<CodeItemCreateResponseContent>
    {
    }
    public class CodeItemCreateResponseContent
    {
        public string CodeId { get; set; }
    }
}
