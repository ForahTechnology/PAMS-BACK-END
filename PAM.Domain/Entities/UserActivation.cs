using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class UserActivation : BaseEntity
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
