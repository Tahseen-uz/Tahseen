using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;
using Tahseen.Service.DTOs.Users.Registration;
using Tahseen.Service.Helpers;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Service.DTOs.Users.BorrowedBookCart;
using Tahseen.Service.DTOs.Users.UserCart;
using Tahseen.Service.DTOs.Users.UserSettings;

namespace Tahseen.Service.Services.Users
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IFineService _fineService;
        private readonly IUserProgressTrackingService _userProgressTrackingService;
        private readonly IUserSettingService _userSettingService;
        private readonly IUserRatingService _userRatingService;
        private readonly IUserCartService _userCartService;
        private readonly IBorrowBookCartService _borrowBookCartService;
        private readonly IFileUploadService _fileUploadService;

        public RegistrationService(IRepository<User> userRepository,
            IMapper mapper,
            IFineService fineService,
            IUserProgressTrackingService userProgressTrackingService,
            IUserSettingService userSettingService,
            IUserRatingService userRatingService,
            IUserCartService userCartService,
            IBorrowBookCartService borrowBookCartService,
            IFileUploadService fileUploadService)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._fineService = fineService;
            this._userProgressTrackingService = userProgressTrackingService;
            this._userSettingService = userSettingService;
            this._userRatingService = userRatingService;
            this._userCartService = userCartService;
            this._borrowBookCartService = borrowBookCartService;
            this._fileUploadService = fileUploadService;
        }
        public async Task<RegistrationForResultDto> AddAsync(RegistrationForCreationDto dto)
        {
            var result = await this._userRepository.SelectAll().Where(e => e.PhoneNumber == dto.PhoneNumber && e.IsDeleted == false).FirstOrDefaultAsync();
            if(result == null)
            {
                var HashedPassword = PasswordHelper.Hash(dto.Password);
                var data = new User()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    EmailAddress = dto.EmailAddress,
                    Password = HashedPassword.Hash,
                    Salt = HashedPassword.Salt,
                    PhoneNumber = dto.PhoneNumber,
                    Address = null,
                    Role = Domain.Enums.Roles.User,
                    FineAmount = null,
                    UserImage = null,
                    LibraryBranchId = null,
                };

                var CreatedData = await this._userRepository.CreateAsync(data);


                var UserRatingForCreation = new UserRatingForCreationDto()
                {
                    BooksCompleted = 0,
                    Rating = 0,
                    UserId = CreatedData.Id,
                };

                await _userRatingService.AddAsync(UserRatingForCreation);

                var UserCartCreation = new UserCartForCreationDto()
                {
                    UserId = CreatedData.Id,


                };
                await this._userCartService.AddAsync(UserCartCreation);

                var borrowBookCartCreationDto = new BorrowedBookCartForCreationDto
                {
                    UserId = CreatedData.Id
                };


                await _borrowBookCartService.AddAsync(borrowBookCartCreationDto);

                var UserSettingCreation = new UserSettingsForCreationDto()
                {
                    UserId = CreatedData.Id,
                    LanguagePreference = Domain.Enums.LanguagePreference.Uzbek,
                    NotificationPreference = Domain.Enums.NotificationStatus.Read,
                    ThemePreference = Domain.Enums.ThemePreference.Light,
                };

                await this._userSettingService.AddAsync(UserSettingCreation);

                return _mapper.Map<RegistrationForResultDto>(CreatedData);
            }
            throw new TahseenException(400, "This user is exist");
        }

      
    }
}
