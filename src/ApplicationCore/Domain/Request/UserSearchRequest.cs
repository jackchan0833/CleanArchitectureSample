using CleanArchitectureSample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.ApplicationCore.Domain.Request
{
    public class UserSearchRequest : BizRequest<UserSearchRequestContent>
    {

    }
    public class UserSearchRequestContent : SearchCriteria
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
