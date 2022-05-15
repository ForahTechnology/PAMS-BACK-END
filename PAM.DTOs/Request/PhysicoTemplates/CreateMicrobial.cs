using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request.PhysicoTemplates
{
    public class CreateMicrobial
    {
        public Guid PhysicoId { get; set; }
        public ICollection<ParameterUploadResponse> Parameters { get; set; }
    }
}
