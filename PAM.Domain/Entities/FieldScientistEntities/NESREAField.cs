using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities.FieldScientistEntities
{
    public class NESREAField : FieldBaseEntity
    {
        public string Name => "NESREA";
        public long? ImageModelId { get; set; }
        public ImageModel ImageModel { get; set; }
        public string PamsUserId { get; set; }
        public PamsUser PamsUser { get; set; }
        public bool Submitted { get; set; }
        public long SamplePointLocationId { get; set; }
        public SamplePointLocation SamplePointLocation { get; set; }
        public FieldLocation FieldLocations { get; set; }
        public List<NESREAFieldResult> NESREASamples { get; set; }
    }
}
