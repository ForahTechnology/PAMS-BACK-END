using Microsoft.AspNetCore.Identity;
using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class PamsUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime PasswordResetDate { get; set; }
        public string OTP { get; set; }
        public bool Active { get; set; }
        public long? ImageModelId { get; set; }
        public ImageModel ImageModel { get; set; }

        //public int ClassRoleId { get; set; }
        //public ClassRole ClassRole { get; set; }

    }
}
