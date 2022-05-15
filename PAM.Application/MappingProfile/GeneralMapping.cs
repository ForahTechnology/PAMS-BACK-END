using AutoMapper;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request.PhysicoTemplates;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Application.MappingProfile
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<FMEnv, CreateFmv>().ReverseMap();
            CreateMap<MicroBiologicalAnalysisResponse, MicroBiologicalAnalysis>().ReverseMap();
            CreateMap<List<MicroBiologicalAnalysisResponse>, List<MicroBiologicalAnalysis>>().ReverseMap();
            CreateMap<PhysicoChemicalAnalysisResponse, PhysicoChemicalAnalysis>().ReverseMap();
            CreateMap<List<PhysicoChemicalAnalysisResponse>, List<PhysicoChemicalAnalysis>>().ReverseMap();
        }
    }
}
