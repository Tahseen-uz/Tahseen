using Tahseen.Service.Mappings;
using Tahseen.Data.Repositories;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Services.Books;
using Tahseen.Service.Services.Users;
using Tahseen.Service.Services.Events;
using Tahseen.Service.Services.Rewards;
using Tahseen.Service.Services.Feedbacks;
using Tahseen.Service.Services.Libraries;
using Tahseen.Service.Services.Reservations;
using Tahseen.Service.Services.Notifications;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.Interfaces.IUsersService;
using Tahseen.Service.Interfaces.IEventsServices;
using Tahseen.Service.Interfaces.IRewardsService;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Service.Services.SchoolAndEducations;
using Tahseen.Service.Interfaces.ILibrariesService;
using Tahseen.Service.Interfaces.ILibrariansService;
using Tahseen.Service.Interfaces.ISchoolAndEducation;
using Tahseen.Service.Interfaces.INotificationServices;
using Tahseen.Service.Interfaces.IReservationsServices;
using Tahseen.Service.Interfaces.IAuthService;
using Tahseen.Service.Services.AuthService;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Services.FileUploadService;

namespace Tahseen.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddAutoMapper(typeof(MappingProfile));

        //Folder Name: IUSerService
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFineService, FineService>();
        services.AddScoped<IUserCartService, UserCartService>();
        services.AddScoped<IWishlistService, WishlistService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IUserSettingService, UserSettingsService>();
        services.AddScoped<IBorrowedBookService, BorrowedBookService>();
        services.AddScoped<IBorrowBookCartService, BorrowBookCartService>();
        services.AddScoped<IUserProgressTrackingService, UserProgressTrackingService>();


        //Folder Name: IBookService
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IBookReviewService, BookReviewService>();
        services.AddScoped<ICompletedBookService, CompletedBookService>();

        //Folder Name:IEventService
        services.AddScoped<IEventsService, EventService>();
        services.AddScoped<IEventRegistrationService, EventRegistrationService>();

        //Folder Name:IFeedBackService
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<ISurveyService, SurveyService>();
        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<IUserRatingService, UserRatingService>();
        services.AddScoped<IUserMessageService, UserMessageService>();
        services.AddScoped<ISurveyResponseService, SurveyResponseService>();

        //Folder Name:ILibrariansService
        services.AddScoped<ILibrarianService, LibrarianService>();

        //Folder Name: ILibrariesService
        services.AddScoped<ILibraryBranchService, LibraryBranchService>();

        //Folder Name:INotificationService
        services.AddScoped<INotificationService, NotificationService>();

        //Folder Name: IReservationService
        services.AddScoped<IReservationsService, ReservationService>();

        //Folder Name: IRewardsService
        services.AddScoped<IBadgeService, BadgeService>();
        services.AddScoped<IUserBadgesService, UserBadgesService>();

        //Folder Name: ISchoolAndEducationService
        services.AddScoped<IPupilService, PupilService>();
        services.AddScoped<ISchoolBookService, SchoolBookService>();
        services.AddScoped<IPupilBookConnectionService, PupilBookConnectionService>();

        //Folder Name: Authentication
        services.AddScoped<IAuthService, AuthService>();

        //Folder Name: FileUploadService
        services.AddScoped<IFileUploadService, FileUploadService>();
    }


    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shamsheer.Api", Version = "v1" });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{ }
            }
        });
        });
    }
};
