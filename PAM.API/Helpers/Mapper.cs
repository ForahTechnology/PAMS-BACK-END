using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using PAMS.Domain.Enums;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.API.Helpers
{
    public class PhysicoChemicalMap : ClassMap<ParameterUploadResponse>
    {
        public PhysicoChemicalMap()
        {
            Map(x => x.Parameter).Name("Parameter");
            Map(x => x.Unit).Name("Unit");
            Map(x => x.Limit).Name("Limit");
        }
    }

}