using AutoMapper;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Events;
using Tahseen.Domain.Entities.Feedbacks;
using Tahseen.Domain.Entities.Librarians;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities.Notifications;
using Tahseen.Domain.Entities.Reservations;
using Tahseen.Domain.Entities.Rewards;
using Tahseen.Domain.Entities.SchoolAndEducations;
using Tahseen.Domain.Entities.Users;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Books.BookReviews;
using Tahseen.Service.DTOs.Books.CompletedBooks;
using Tahseen.Service.DTOs.Books.Genre;
using Tahseen.Service.DTOs.Books.Publishers;
using Tahseen.Service.DTOs.Events.EventRegistration;
using Tahseen.Service.DTOs.Events.Events;
using Tahseen.Service.DTOs.Feedbacks.Feedback;
using Tahseen.Service.DTOs.Feedbacks.News;
using Tahseen.Service.DTOs.Feedbacks.SurveyResponses;
using Tahseen.Service.DTOs.Feedbacks.Surveys;
using Tahseen.Service.DTOs.Feedbacks.UserMessages;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Service.DTOs.Librarians;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;
using Tahseen.Service.DTOs.Notifications;
using Tahseen.Service.DTOs.Reservations;
using Tahseen.Service.DTOs.Rewards.Badge;
using Tahseen.Service.DTOs.Rewards.UserBadges;
using Tahseen.Service.DTOs.SchoolAndEducations;
using Tahseen.Service.DTOs.Users.BorrowedBook;
using Tahseen.Service.DTOs.Users.BorrowedBookCart;
using Tahseen.Service.DTOs.Users.Fine;
using Tahseen.Service.DTOs.Users.Registration;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.DTOs.Users.UserCart;
using Tahseen.Service.DTOs.Users.UserProgressTracking;
using Tahseen.Service.DTOs.Users.UserSettings;
using Tahseen.Service.DTOs.Users.Wishlists;

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

        //Folder Name: Events
        CreateMap<Event, EventForCreationDto>().ReverseMap();
        CreateMap<Event, EventForUpdateDto>().ReverseMap();
        CreateMap<Event, EventForResultDto>().ReverseMap();

        CreateMap<EventRegistration, EventRegistrationForCreationDto>().ReverseMap();
        CreateMap<EventRegistration, EventRegistrationForUpdateDto>().ReverseMap();
        CreateMap<EventRegistration, EventRegistrationForResultDto>().ReverseMap();

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
        
        CreateMap<UserMessage, UserMessageForCreationDto>().ReverseMap();
        CreateMap<UserMessage, UserMessageForResultDto>().ReverseMap();
        CreateMap<UserMessage, UserMessageForUpdateDto>().ReverseMap();

        CreateMap<UserRatings, UserRatingForCreationDto>().ReverseMap();
        CreateMap<UserRatings, UserRatingForResultDto>().ReverseMap();
        CreateMap<UserRatings, UserRatingForUpdateDto>().ReverseMap();

        //Folder Name: Librarians and Library Branches
        CreateMap<Librarian, LibrarianForCreationDto>().ReverseMap();
        CreateMap<Librarian, LibrarianForUpdateDto>().ReverseMap();
        CreateMap<Librarian, LibrarianForResultDto>().ReverseMap();

        CreateMap<LibraryBranch, LibraryBranchForCreationDto>().ReverseMap();
        CreateMap<LibraryBranch, LibraryBranchForResultDto>().ReverseMap();
        CreateMap<LibraryBranch, LibraryBranchForUpdateDto>().ReverseMap();

        //Folder Name: Notification

        CreateMap<Notification, NotificationForCreationDto>().ReverseMap();
        CreateMap<Notification, NotificationForResultDto>().ReverseMap();
        CreateMap<Notification, NotificationForUpdateDto>().ReverseMap();

        //Folder Name: Reservations

        CreateMap<Reservation, ReservationForCreationDto>().ReverseMap();
        CreateMap<Reservation, ReservationForUpdateDto>().ReverseMap();
        CreateMap<Reservation, ReservationForResultDto>().ReverseMap();


        //Folder Name: Rewards
        CreateMap<Badge, BadgeForCreationDto>().ReverseMap();
        CreateMap<Badge, BadgeForUpdateDto>().ReverseMap();
        CreateMap<Badge, BadgeForResultDto>().ReverseMap();

        CreateMap<UserBadges, UserBadgesForCreationDto>().ReverseMap();
        CreateMap<UserBadges, UserBadgesForUpdateDto>().ReverseMap();
        CreateMap<UserBadges, UserBadgesForResultDto>().ReverseMap();

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

        CreateMap<UserCart, UserCartForCreationDto>().ReverseMap();
        CreateMap<UserCart, UserCartForResultDto>().ReverseMap();
        CreateMap<UserCart, UserCartForUpdateDto>().ReverseMap();

        CreateMap<BorrowedBook, BorrowedBookForCreationDto>().ReverseMap();
        CreateMap<BorrowedBook, BorrowedBookForUpdateDto>().ReverseMap();
        CreateMap<BorrowedBook, BorrowedBookForResultDto>().ReverseMap();

        CreateMap<Fine, FineForCreationDto>().ReverseMap();
        CreateMap<Fine, FineForUpdateDto>().ReverseMap();
        CreateMap<Fine, FineForResultDto>().ReverseMap();

        CreateMap<Registration, RegistrationForCreationDto>().ReverseMap();
        CreateMap<Registration, RegistrationForResultDto>().ReverseMap();

        CreateMap<UserProgressTracking, UserProgressTrackingForCreationDto>().ReverseMap();
        CreateMap<UserProgressTracking, UserProgressTrackingForUpdateDto>().ReverseMap();
        CreateMap<UserProgressTracking, UserProgressTrackingForResultDto>().ReverseMap();

        CreateMap<UserSettings, UserSettingsForCreationDto>().ReverseMap();
        CreateMap<UserSettings, UserSettingsForUpdateDto>().ReverseMap();
        CreateMap<UserSettings, UserSettingsForResultDto>().ReverseMap();

        CreateMap<BorrowedBookCart, BorrowedBookCartForCreationDto>().ReverseMap();
        CreateMap<BorrowedBookCart, BorrowedBookCartForResultDto>().ReverseMap();

        CreateMap<WishList, WishlistForCreationDto>().ReverseMap();
        CreateMap<WishList, WishlistForResultDto>().ReverseMap();
        CreateMap<WishList, WishlistForUpdateDto>().ReverseMap();



    }
}
