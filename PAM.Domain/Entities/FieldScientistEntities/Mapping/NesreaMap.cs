using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities.FieldScientistEntities.Mapping
{
    public class NesreaMap : IEntityTypeConfiguration<NESREAField>
    {
        public void Configure(EntityTypeBuilder<NESREAField> builder)
        {
            builder.ToTable(nameof(NESREAField));


            builder.HasOne(x => x.FieldLocations).WithOne(x => x.NESREAField)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class FieldLocationMap : IEntityTypeConfiguration<FieldLocation>
    {
        public void Configure(EntityTypeBuilder<FieldLocation> builder)
        {
            builder.ToTable(nameof(FieldLocation));    
            
            builder.HasOne(x => x.NESREAField).WithOne(x => x.FieldLocations)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
