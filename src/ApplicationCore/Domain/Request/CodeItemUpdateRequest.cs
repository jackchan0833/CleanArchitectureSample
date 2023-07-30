﻿using CleanArchitectureSample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.ApplicationCore.Domain.Request
{
    public class CodeItemUpdateRequest : BizRequest<CodeItemUpdateRequestContent>
    {

    }
    public class CodeItemUpdateRequestContent
    {
        public string CodeId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? SeqNo { get; set; }
        public string Remarks { get; set; }
    }
}
