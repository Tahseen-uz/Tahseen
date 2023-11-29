using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.DTOs.Message;
using Tahseen.Service.DTOs.Users.Registration;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Helpers;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Interfaces.IMessageServices;
using Tahseen.Service.Interfaces.IUsersService;

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

    public RegistrationService(
        IRepository<User> userRepository,
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
        _userRepository = userRepository;
        _mapper = mapper;
        _fineService = fineService;
        _userProgressTrackingService = userProgressTrackingService;
        _userSettingService = userSettingService;
        _userRatingService = userRatingService;
        _userCartService = userCartService;
        _borrowBookCartService = borrowBookCartService;
        _fileUploadService = fileUploadService;
        _messageService = messageService;
        _cache = cache;
    }

    public async Task<RegistrationForResultDto> AddAsync(RegistrationForCreationDto dto, string verificationCode)
    {
        var result = await _userRepository.SelectAll().Where(e => e.PhoneNumber == dto.PhoneNumber && e.IsDeleted == false).FirstOrDefaultAsync();
        if (result == null)
        {
            // Retrieve the verification code from the cache
            var storedCode = _cache.Get<string>($"{dto.EmailAddress}_VerificationCode");

            // Check if the entered code matches the stored code
            if (storedCode != null && storedCode == verificationCode)
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
                    Role = Tahseen.Domain.Enums.Roles.User,
                    FineAmount = null,
                    UserImage = null,
                    LibraryBranchId = null,
                };

                // Save the user to the database
                var createdData = await _userRepository.CreateAsync(data);

                // Remove the user's info from the cache after registration
                _cache.Remove($"{dto.EmailAddress}_VerificationCode");

                return _mapper.Map<RegistrationForResultDto>(createdData);
            }
            else
            {
                // Incorrect or expired verification code
                throw new TahseenException(400, "Incorrect or expired verification code");
            }
        }

        throw new TahseenException(400, "This user already exists");
    }

    public async Task<string> SendVerificationCodeAsync(string email)
    {
        // Generate and save a new verification code to the cache
        var code = GenerateCodeForEmailVerificationAsync();
        _cache.Set($"{email}_VerificationCode", code, TimeSpan.FromMinutes(15)); // Adjust expiration time as needed

        // Send the verification code to the user's email
        var message = new MessageForCreationDto()
        {
            Subject = "Tahseen.uz tasdiqlash kod",
            To = email,
            Body = code,
        };
        await _messageService.SendEmail(message);

        return code;
    }

    private string GenerateCodeForEmailVerificationAsync()
    {
        string code = Guid.NewGuid().ToString("N").Substring(0, 6);
        return code;
    }
}