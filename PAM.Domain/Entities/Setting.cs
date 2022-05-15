using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class Setting : BaseEntity
    {
        public string VAT { get; set; }
        public string Lab_Director { get; set; }
    }
}
