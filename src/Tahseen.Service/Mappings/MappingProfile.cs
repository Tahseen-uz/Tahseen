using AutoMapper;
using Tahseen.Domain.Entities.Events;
using Tahseen.Service.DTOs.Events.Event;
using Tahseen.Service.DTOs.Events.EventRegistration;

namespace Tahseen.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Event,EventForCreationDto>().ReverseMap();
            CreateMap<Event,EventForResultDto>().ReverseMap();
            CreateMap<Event,EventForUpdateDto>().ReverseMap();
            CreateMap<EventRegistration,EventRegistrationForCreationDto>().ReverseMap();
            CreateMap<EventRegistration,EventRegistrationForResultDto>().ReverseMap();
            CreateMap<EventRegistration,EventRegistrationForUpdateDto>().ReverseMap();
        }

    }
}
