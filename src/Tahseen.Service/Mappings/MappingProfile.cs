using AutoMapper;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Events;
using Tahseen.Domain.Entities.Feedback;
using Tahseen.Domain.Entities.SchoolAndEducations;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Books.BookReviews;
using Tahseen.Service.DTOs.Books.CompletedBooks;
using Tahseen.Service.DTOs.Books.Genre;
using Tahseen.Service.DTOs.Books.Publishers;
using Tahseen.Service.DTOs.Events.Event;
using Tahseen.Service.DTOs.Events.EventRegistration;
using Tahseen.Service.DTOs.Feedbacks.Feedback;
using Tahseen.Service.DTOs.Feedbacks.News;
using Tahseen.Service.DTOs.Feedbacks.SurveyResponses;
using Tahseen.Service.DTOs.Feedbacks.Surveys;
using Tahseen.Service.DTOs.SchoolAndEducations;
using Tahseen.Service.DTOs.Users.BorrowedBook;
using Tahseen.Service.DTOs.Users.Fine;
using Tahseen.Service.DTOs.Users.Registration;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.DTOs.Users.UserProgressTracking;
using Tahseen.Service.DTOs.Users.UserSettings;

namespace Tahseen.Service.Mappings;

public class MappingProfile:Profile
{
    public MappingProfile()
    {

        //Folder Name: Books
        CreateMap<BookReviews, BookReviewForCreationDto>().ReverseMap();
        CreateMap<BookReviews,BookReviewForResultDto>().ReverseMap();
        CreateMap<BookReviews, BookReviewForUpdateDto>().ReverseMap();

        CreateMap<CompletedBooks, CompletedBookForCreationDto>().ReverseMap();
        CreateMap<CompletedBooks,CompletedBookForResultDto>().ReverseMap();
        CreateMap<CompletedBooks,CompletedBookForUpdateDto>().ReverseMap();

        CreateMap<Publisher,PublisherForCreationDto>().ReverseMap();
        CreateMap<Publisher,PublisherForResultDto>().ReverseMap();
        CreateMap<Publisher,PublisherForUpdateDto>().ReverseMap();
        
        CreateMap<Author, AuthorForCreationDto>().ReverseMap();
        CreateMap<Author, AuthorForUpdateDto>().ReverseMap();
        CreateMap<Author, AuthorForResultDto>().ReverseMap();

        CreateMap<Book, BookForCreationDto>().ReverseMap();
        CreateMap<Book, BookForUpdateDto>().ReverseMap();
        CreateMap<Book, BookForResultDto>().ReverseMap();

        CreateMap<Genre, GenreForCreationDto>().ReverseMap();
        CreateMap<Genre, GenreForUpdateDto>().ReverseMap();
        CreateMap<Genre, GenreForResultDto>().ReverseMap();


        //Folder Name: Feedbacks
        CreateMap<Feedback,FeedbackForCreationDto>().ReverseMap();
        CreateMap<Feedback,FeedbackForResultDto>().ReverseMap();
        CreateMap<Feedback,FeedbackForUpdateDto>().ReverseMap();
        
        CreateMap<News,NewsForCreationDto>().ReverseMap();
        CreateMap<News,NewsForResultDto>().ReverseMap();
        CreateMap<News, NewsForUpdateDto>().ReverseMap();
        
        CreateMap<Surveys,SurveyForCreationDto>().ReverseMap();
        CreateMap<Surveys,SurveyForResultDto>().ReverseMap();
        CreateMap<Surveys,SurveyForUpdateDto>().ReverseMap();
        
        CreateMap<SurveyResponses,SurveyResponseForCreationDto>().ReverseMap();
        CreateMap<SurveyResponses,SurveyResponseForResultDto>().ReverseMap();
        CreateMap<SurveyResponses,SurveyResponseForUpdateDto>().ReverseMap();
        


        //Folder Name: SchoolAndEducations
        CreateMap<PupilBookConnection,PupilBookConnectionForCreationDto>().ReverseMap();
        CreateMap<PupilBookConnection,PupilBookConnectionForResultDto>().ReverseMap();
        CreateMap<PupilBookConnection,PupilBookConnectionForUpdateDto>().ReverseMap();
        
        CreateMap<Pupil,PupilForCreationDto>().ReverseMap();
        CreateMap<Pupil,PupilForResultDto>().ReverseMap();
        CreateMap<Pupil,PupilForUpdateDto>().ReverseMap();
        
        CreateMap<SchoolBook,SchoolBookForCreationDto>().ReverseMap();
        CreateMap<SchoolBook,SchoolBookForResultDto>().ReverseMap();
        CreateMap<SchoolBook,SchoolBookForUpdateDto>().ReverseMap();



        //Folder Name: Users
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();

        CreateMap<BorrowedBook, BorrowedBookForCreationDto>().ReverseMap();
        CreateMap<BorrowedBook, BorrowedBookForUpdateDto>().ReverseMap();
        CreateMap<BorrowedBook, BorrowedBookForResultDto>().ReverseMap();

        CreateMap<Fine, FineServiceForCreationDto>().ReverseMap();
        CreateMap<Fine, FineServiceForUpdateDto>().ReverseMap();
        CreateMap<Fine, FineServiceForResultDto>().ReverseMap();

        CreateMap<Registration, RegistrationForCreationDto>().ReverseMap();
        CreateMap<Registration, RegistrationForUpdateDto>().ReverseMap();
        CreateMap<Registration, RegistrationForResultDto>().ReverseMap();

        CreateMap<UserProgressTracking, UserProgressTrackingForCreationDto>().ReverseMap();
        CreateMap<UserProgressTracking, UserProgressTrackingForUpdateDto>().ReverseMap();
        CreateMap<UserProgressTracking, UserProgressTrackingForResultDto>().ReverseMap();

        CreateMap<UserSettings, UserSettingsForCreationDto>().ReverseMap();
        CreateMap<UserSettings, UserSettingsForUpdateDto>().ReverseMap();
        CreateMap<UserSettings, UserSettingsForResultDto>().ReverseMap();

        //Folder Name: Events

        CreateMap<Event, EventForCreationDto>().ReverseMap();
        CreateMap<Event, EventForResultDto>().ReverseMap();
        CreateMap<Event, EventForUpdateDto>().ReverseMap();

        CreateMap<EventRegistration, EventRegistrationForCreationDto>().ReverseMap();
        CreateMap<EventRegistration, EventRegistrationForResultDto>().ReverseMap();
        CreateMap<EventRegistration, EventRegistrationForUpdateDto>().ReverseMap();




    }
}
