using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities.FieldScientistEntities
{
    public class DPRFieldResult : FieldBaseEntity
    {
        public long DPRFieldId { get; set; }
        public DPRField DPRField { get; set; }
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }
    }

    public class DPRTemplate : BaseEntity
    {
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
    }
}
