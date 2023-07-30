using Newtonsoft.Json;
using CleanArchitectureSample.Utilities;
using System.Collections.Generic;

namespace CleanArchitectureSample.ApplicationCore.Domain.Response
{
    public class UserCreateResponse : BizResponse<UserCreateResponseContent>
    {
    }
    public class UserCreateResponseContent
    {
        public string UserId { get; set; }
    }
}
