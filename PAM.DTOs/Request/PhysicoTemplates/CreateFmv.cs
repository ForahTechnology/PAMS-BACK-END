using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request.PhysicoTemplates
{
    public class CreateFmv
    {
        public string SectorName { get; set; }
        public ICollection<ParameterUploadResponse> Parameters { get; set; }
    }
}
