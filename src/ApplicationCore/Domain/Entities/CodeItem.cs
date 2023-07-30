using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureSample.ApplicationCore.Domain.Entities
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
