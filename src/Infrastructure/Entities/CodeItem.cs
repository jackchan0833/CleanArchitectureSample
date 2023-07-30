using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.Infrastructure.Entities
{
    public class CodeItem
    {
        public string CodeId { get; set; }
        public string Category { get; set; }
        public string CodeName { get; set; }
        public string CodeValue { get; set; }
        public int? SeqNo { get; set; }
        public string Remarks { get; set; }
    }
}
