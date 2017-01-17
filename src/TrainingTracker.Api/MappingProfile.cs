using AutoMapper;
using TrainingTracker.Api.Models;

namespace TrainingTracker.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Log, Service.Models.Log>();
            CreateMap<Service.Models.Log, Log>();
            CreateMap<Set, Service.Models.Set>();
            CreateMap<Service.Models.Set, Set>();
        }
    }
}
