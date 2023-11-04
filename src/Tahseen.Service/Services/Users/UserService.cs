using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Data.Repositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Users.BorrowedBookCart;
using Tahseen.Service.DTOs.Users.Fine;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.DTOs.Users.UserCart;
using Tahseen.Service.DTOs.Users.UserProgressTracking;
using Tahseen.Service.DTOs.Users.UserSettings;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class UserService : IUserService
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

        public UserService(IRepository<User> userRepository,
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
        public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
        {
            var result = await _userRepository.SelectAll().Where(e => e.EmailAddress == dto.EmailAddress && e.Password == e.Password && e.IsDeleted == false).FirstOrDefaultAsync();
            if (result != null)
            {
                throw new TahseenException(400, "User is exist");
            }

            var FileUloadForCreation = new FileUploadForCreationDto
            {
                FolderPath = "UsersAssets",
                FormFile = dto.UserImage
            };
            var FileResult = await this._fileUploadService.FileUploadAsync(FileUloadForCreation);

            var data = this._mapper.Map<User>(dto);
            data.UserImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
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

            return _mapper.Map<UserForResultDto>(CreatedData);
        }

        public async Task<UserForResultDto> ModifyAsync(long Id, UserForUpdateDto dto)
        {
            var data = await _userRepository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if (data is not null)
            {

                // deleting image
                await this._fileUploadService.FileDeleteAsync(data.UserImage);

                // uploading image
                var FileUloadForCreation = new FileUploadForCreationDto
                {
                    FolderPath = "UsersAssets",
                    FormFile = dto.UserImage
                };
                var FileResult = await this._fileUploadService.FileUploadAsync(FileUloadForCreation);

                var MappedData = this._mapper.Map(dto, data);
                MappedData.UserImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
                MappedData.UpdatedAt = DateTime.UtcNow;
                var result = await _userRepository.UpdateAsync(MappedData);
                return _mapper.Map<UserForResultDto>(result);
            }
            throw new TahseenException(404, "User is not found");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            var user = await this._userRepository.SelectAll()
                .Where(u => u.Id == Id && u.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (user is null)
                throw new TahseenException(404, "User is not found");

            await this._fileUploadService.FileDeleteAsync(user.UserImage);
            return await _userRepository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync()
        {
            var users =  _userRepository.SelectAll().Where(t => t.IsDeleted == false);
            foreach (var user in users)
            {
                user.UserImage = $"https://localhost:7020/{user.UserImage.Replace('\\', '/').TrimStart('/')}";
            }
            return _mapper.Map<IEnumerable<UserForResultDto>>(users);
        }

        public async Task<UserForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await _userRepository.SelectByIdAsync(Id);
            if(data != null && data.IsDeleted == false)
            {
                data.UserImage = $"https://localhost:7020/{data.UserImage.Replace('\\', '/').TrimStart('/')}";
                return this._mapper.Map<UserForResultDto>(data);
            }
            throw new TahseenException(404, "User is not found");
        }
    }
}
