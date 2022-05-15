using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities.FieldScientistEntities.Mapping
{
    public class SamplePointLocationMap : IEntityTypeConfiguration<SamplePointLocation>
    {
        public void Configure(EntityTypeBuilder<SamplePointLocation> builder)
        {
            builder.ToTable(nameof(SamplePointLocation));

            builder.HasMany(x => x.NESREAFields).WithOne(x => x.SamplePointLocation).HasForeignKey(x => x.SamplePointLocationId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.DPRFields).WithOne(x => x.SamplePointLocation).HasForeignKey(x => x.SamplePointLocationId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.FMENVFields).WithOne(x => x.SamplePointLocation).HasForeignKey(x => x.SamplePointLocationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
