using PAMS.Domain.Entities.FieldScientistEntities;
using PAMS.DTOs.Request.FieldScientistRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAMS.DTOs.Response.FieldScientistResponse
{
    public class AllFieldScientistFMEnveTestsVM
    {
        public long Id { get; set; }
        public long SamplePointLocationId { get; set; }
        public string ClientName { get; set; }
        public string AnalystFullName { get; set; }
        public string SamplePointName { get; set; }
        public string Name { get; set; }
        public FieldLocationVM Location { get; set; }
        
        public DateTime Time { get; set; }
        public List<FMENVTemplateDto> FMENVSamples { get; set; }
        public ImageVM ImageDetails { get; set; }

        public static implicit operator AllFieldScientistFMEnveTestsVM(FMENVField model)
        {
            return model == null
                ? null
                : new AllFieldScientistFMEnveTestsVM
                {
                    Id = model.Id,
                    SamplePointLocationId = model.SamplePointLocationId,
                    AnalystFullName = (model.PamsUser is null) ? " " : $"{model.PamsUser.FirstName} {model.PamsUser.LastName}",
                    Name = model.Name,
                    ClientName = model.SamplePointLocation.Client.Name,
                    SamplePointName = model.SamplePointLocation.Name,
                    Location = model.FieldLocations,
                    Time = model.TimeModified,
                    FMENVSamples = model.FMENVSamples.Select(x => (FMENVTemplateDto)x).ToList()
                };
        }
    }

    public class AllFieldScientistDPRTestsVM
    {
        public long Id { get; set; }
        public long SamplePointLocationId { get; set; }
        public string AnalystFullName { get; set; }
        public string ClientName { get; set; }
        public string SamplePointName { get; set; }
        public string Name { get; set; }
        public FieldLocationVM Location { get; set; }
        public DateTime Time { get; set; }
        public List<DPRTemplateDto> DprSamples { get; set; }
        public ImageVM ImageDetails { get; set; }

        public static implicit operator AllFieldScientistDPRTestsVM(DPRField model)
        {
            return model == null
                ? null
                : new AllFieldScientistDPRTestsVM
                {
                    Id = model.Id,
                    SamplePointLocationId = model.SamplePointLocationId,
                    Name = model.Name,
                    AnalystFullName = (model.PamsUser is null) ? " " : $"{model.PamsUser.FirstName} {model.PamsUser.LastName}",
                    ClientName = model.SamplePointLocation.Client.Name,
                    SamplePointName = model.SamplePointLocation.Name,
                    Location = model.FieldLocations,
                    Time = model.TimeModified,
                    DprSamples = model.DPRSamples.Select(x => (DPRTemplateDto)x).ToList()
                };
        }
    }

    public class AllFieldScientistNesreaTestsVM
    {
        public long Id { get; set; }
        public long SamplePointLocationId { get; set; }
        public string AnalystFullName { get; set; }
        public string ClientName { get; set; }
        public string SamplePointName { get; set; }
        public string Name { get; set; }
        public FieldLocationVM Location { get; set; }
        public DateTime Time { get; set; }
        public List<NesreaTemplateDto> NesreaSamples { get; set; }
        public ImageVM ImageDetails { get; set; } = new ImageVM();

        public static implicit operator AllFieldScientistNesreaTestsVM(NESREAField model)
        {
            return model == null
                ? null
                : new AllFieldScientistNesreaTestsVM
                {
                    Id = model.Id,
                    SamplePointLocationId = model.SamplePointLocationId,
                    Name = model.Name,
                    AnalystFullName = (model.PamsUser is null) ? " " : $"{model.PamsUser.FirstName} {model.PamsUser.LastName}",
                    ClientName = model.SamplePointLocation.Client.Name,
                    SamplePointName = model.SamplePointLocation.Name,
                    Location = model.FieldLocations,
                    Time = model.TimeModified,
                    NesreaSamples = model.NESREASamples.Select(x => (NesreaTemplateDto)x).ToList()
                };
        }
    }

    public class FieldLocationVM
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public static implicit operator FieldLocationVM(FieldLocation model)
        {
            return model == null
                ? null
                : new FieldLocationVM
                {
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                };

        }
    }

}



