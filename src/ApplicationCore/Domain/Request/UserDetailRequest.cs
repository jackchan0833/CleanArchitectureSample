using CleanArchitectureSample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.ApplicationCore.Domain.Request
{
    public class UserDetailRequest : BizRequest<UserDetailRequestContent>
    {

    }
    public class UserDetailRequestContent
    {
        public string UserId { get; set; } //user id or username to query detail
        public string UserName { get; set; }
    }
}
