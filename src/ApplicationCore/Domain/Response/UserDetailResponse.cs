using Newtonsoft.Json;
using CleanArchitectureSample.Utilities;
using System.Collections.Generic;

namespace CleanArchitectureSample.ApplicationCore.Domain.Response
{
    public class UserDetailResponse : BizResponse<UserDetailResponseContent>
    {
    }
    public class UserDetailResponseContent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
