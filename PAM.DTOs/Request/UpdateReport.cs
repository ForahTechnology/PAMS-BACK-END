using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class UpdateReport
    {
        public Guid ID { get; set; }
        public Guid SamplingID { get; set; }
        public string Lab_Sample_Ref_Number { get; set; }
        public string Certificate_Number { get; set; }
        public string Sample_Label { get; set; }
        public Laboratory_Environment_Conditions Lab_Env_Con { get; set; }
        public DateTime Date_Recieved_In_Lab { get; set; }
        public DateTime Date_Analysed_In_Lab { get; set; }
        public string Sample_Type { get; set; }
        public string Batch_Number { get; set; }
        public string Comment { get; set; }
        public string Lab_Analyst { get; set; }
    }
}
