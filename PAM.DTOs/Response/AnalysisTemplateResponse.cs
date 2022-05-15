using PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class AnalysisTemplateResponse<T> 
    {
        public T Analysis { get; set; }
    }
}
