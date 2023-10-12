using AutoMapper;
<<<<<<< HEAD
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

=======
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Books.BookReviews;
using Tahseen.Service.DTOs.Books.CompletedBooks;
using Tahseen.Service.DTOs.Books.Publishers;

namespace Tahseen.Service.Mappings;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<BookReviews, BookReviewForCreationDto>().ReverseMap();
        CreateMap<BookReviews,BookReviewForResultDto>().ReverseMap();
        CreateMap<BookReviewForUpdateDto,BookReviews>().ReverseMap();

        CreateMap<CompletedBooks, CompletedBookForCreationDto>().ReverseMap();
        CreateMap<CompletedBooks,CompletedBookForResultDto>().ReverseMap();
        CreateMap<CompletedBooks,CompletedBookForUpdateDto>().ReverseMap();

        CreateMap<Publisher,PublisherForCreationDto>().ReverseMap();
        CreateMap<Publisher,PublisherForResultDto>().ReverseMap();
        CreateMap<Publisher,PublisherForUpdateDto>().ReverseMap();
>>>>>>> edceee8cb89e40d19bf87790c7d9124a3ddc9298
    }
}
