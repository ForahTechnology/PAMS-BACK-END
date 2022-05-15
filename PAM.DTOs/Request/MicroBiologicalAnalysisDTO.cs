using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class MicroBiologicalAnalysisDTO
    {
        public string Microbial_Group { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
        public string Test_Method { get; set; }
    }
}
