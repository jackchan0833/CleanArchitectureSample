using CleanArchitectureSample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.ApplicationCore.Domain.Request
{
    public class UserCreateRequest : BizRequest<UserCreateRequestContent>
    {

    }
    public class UserCreateRequestContent
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
