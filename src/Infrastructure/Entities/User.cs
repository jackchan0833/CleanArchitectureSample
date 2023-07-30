using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.Infrastructure.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
