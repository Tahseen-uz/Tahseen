using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Service.DTOs.Message;
using Tahseen.Service.DTOs.Registrations;
using Tahseen.Service.DTOs.Users.BorrowedBookCart;
using Tahseen.Service.DTOs.Users.Registration;
using Tahseen.Service.DTOs.Users.UserCart;
using Tahseen.Service.DTOs.Users.UserSettings;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Helpers;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Interfaces.IMessageServices;
using Tahseen.Service.Interfaces.IUsersService;

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
        private readonly IMessageSevice _messageService;
        private readonly IMemoryCache _cache;
        public RegistrationService(IRepository<User> userRepository,
            IMapper mapper,
            IFineService fineService,
            IUserProgressTrackingService userProgressTrackingService,
            IUserSettingService userSettingService,
            IUserRatingService userRatingService,
            IUserCartService userCartService,
            IBorrowBookCartService borrowBookCartService,
            IFileUploadService fileUploadService,
            IMessageSevice messageService,
            IMemoryCache cache)
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
            _messageService = messageService;
            _cache = cache;
        }
        public async Task<RegistrationForResultDto> AddAsync(RegistrationForCreationDto dto)
        {
            var result = await this._userRepository.SelectAll().Where(e => e.EmailAddress == dto.EmailAddress && e.IsDeleted == false).FirstOrDefaultAsync();
            if(dto.VerificationCode == null)
            {
                throw new TahseenException(400, "Invalid Verification code");
            }
            if (result == null)
            {
                var EmailAndVerificationCode = new VerifyCodeDto()
                {
                    Email = dto.EmailAddress,
                    VerificationCode = dto.VerificationCode,
                };
                var isVerified = await VerifyCodeAsync(EmailAndVerificationCode);
                    
                if (isVerified == true)
                {

                    _cache.Remove($"{dto.EmailAddress}_VerificationCode");
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
                else
                {
                    throw new TahseenException(400, "Verification code is not verified");
                }

               
            }
            throw new TahseenException(400, "This user is exist");
        }

        public async Task<string> SendVerificationCodeAsync(SendVerificationCodeDto dto)
        {
            var existingUser = await this._userRepository
                .SelectAll()
                .Where(e => e.EmailAddress == dto.Email && e.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (existingUser == null)
            {
                var verificationCode = GenerateCodeForEmailVerificationAsync();
                var cacheKey = $"{dto.Email}_VerificationCode";
                var cacheExpiration = TimeSpan.FromSeconds(60); // Set expiration time to 2 minutes

                try
                {
                    // Store the verification code in the cache with a 2-minute expiration
                    _cache.Set(cacheKey, verificationCode, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = cacheExpiration
                    });

                    // Send the verification code to the user via email
                    var message = new MessageForCreationDto()
                    {
                        Subject = "Tahseen.uz tasdiqlash kod",
                        To = dto.Email,
                        Body = $"Tahseen.uz tasdiqlash kodingiz: {verificationCode}",
                    };

                    await _messageService.SendEmail(message);

                    return verificationCode;
                }
                catch (Exception ex)
                {
                    // Handle exceptions related to cache or email sending
                    // Log the exception or provide a more meaningful error message
                    throw new TahseenException(500, "Error occurred while sending verification code.");
                }
            }
            else
            {
                throw new TahseenException(400, "This user already exists.");
            }
        }


        public async Task<bool> VerifyCodeAsync(VerifyCodeDto dto)
        {
            var storedCode = _cache.Get<string>($"{dto.Email}_VerificationCode");

            if (storedCode != null && storedCode == dto.VerificationCode)
            {
                return true;
            }

            return false;
        }

        private string GenerateCodeForEmailVerificationAsync()
        {
            string code = Guid.NewGuid().ToString("N").Substring(0, 6);
            return code;
        }


    }
}