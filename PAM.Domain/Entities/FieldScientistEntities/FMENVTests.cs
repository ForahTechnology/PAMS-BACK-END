using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities.FieldScientistEntities
{
    public class FMENVFieldResult : FieldBaseEntity
    {
        public long FMENVFieldId { get; set; }
        public FMENVField FMENVField { get; set; }
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }
    }
    public class FMENVTemplate : BaseEntity
    {
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
    }
   

}
