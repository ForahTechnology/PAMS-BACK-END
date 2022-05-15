using AutoMapper;
using PAMS.Domain.Entities;
using PAMS.DTOs.Response;

namespace PAMS.Persistence.Profiles
{
    /// <summary>
    /// This class inherits from AutoMapper which enable us to create a mapping profile to automatically map objects.
    /// Ensure you register your objects to enable mapping.
    /// </summary>
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //Map to and from any object here
            CreateMap<Client, ClientMobile>();
        }
    }
}
