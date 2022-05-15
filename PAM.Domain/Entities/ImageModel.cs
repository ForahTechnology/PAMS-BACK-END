using PAMS.Domain.Entities.FieldScientistEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class ImageModel : FieldBaseEntity
    {
        public string Name { get; set; }

        public string FileType { get; set; }

        public string Extension { get; set; }

        public byte[] LogoBase64 { get; set; }
    }
}
