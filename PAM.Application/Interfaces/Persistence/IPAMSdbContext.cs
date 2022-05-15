using Microsoft.EntityFrameworkCore;
using PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Persistence
{
    public interface IPAMSdbContext
    {
        DbSet<PamsUser>  PamsUsers { get; set; }
        DbSet<UserActivation> UserActivations { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<ClientSample> ClientSamples { get; set; }
        Task<int> SaveChangesAsync();
    }
}
