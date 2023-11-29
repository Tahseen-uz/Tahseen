using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Library;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Users.BorrowedBookCart;
using Tahseen.Service.DTOs.Users.ChangePassword;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.DTOs.Users.UserCart;
using Tahseen.Service.DTOs.Users.UserSettings;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Tahseen.Service.Helpers;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<LibraryBranch> _libraryBranchRepository;
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
            IFileUploadService fileUploadService,
            IRepository<LibraryBranch> libraryBranchRepository)
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
            _libraryBranchRepository = libraryBranchRepository;
        }
        public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
        {
            var result = await _userRepository.SelectAll().Where(e => e.FirstName == dto.FirstName && e.LastName == dto.LastName && e.LibraryBranchId == dto.LibraryBranchId && e.IsDeleted == false).FirstOrDefaultAsync();
            var LibraryChecking = await _libraryBranchRepository.SelectAll().Where(l => l.Id == dto.LibraryBranchId).FirstOrDefaultAsync();
            if(LibraryChecking == null)
            {
                throw new TahseenException(404, "This library is not found");
            }
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
            var HashedPassword = PasswordHelper.Hash(dto.Password);
            data.Password = HashedPassword.Hash;
            data.Salt = HashedPassword.Salt;
            data.FineAmount = 0;
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

        public async Task<bool> ChangePasswordAsync(long Id, UserForChangePasswordDto dto)
        {
            var data = await _userRepository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if(data == null || PasswordHelper.Verify(dto.OldPassword, data.Salt, data.Password) == false){
                throw new TahseenException(400, "User or Password is Incorrect");
            }
            else if(dto.NewPassword != dto.ConfirmPassword)
            {
                throw new TahseenException(400, "New Password and Confirm Password does not Match");
            }
            var HashedPassword = PasswordHelper.Hash(dto.ConfirmPassword);
            data.Salt = HashedPassword.Salt;
            data.Password = HashedPassword.Hash;
            await _userRepository.UpdateAsync(data);
            return true;
        }

        public async Task<UserForResultDto> ModifyAsync(long Id, UserForUpdateDto dto)
        {
            var data = await _userRepository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if (data is not null)
            {
                if (dto != null && dto.UserImage != null)
                {
                    if(data.UserImage != null)
                    {
                        await this._fileUploadService.FileDeleteAsync(data.UserImage);
                    };
                    var FileUploadForCreation = new FileUploadForCreationDto
                    {
                        FolderPath = "UsersAssets",
                        FormFile = dto.UserImage
                    };
                    var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);
                
               

                    data.UserImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
                    data.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : data.FirstName;
                    data.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : data.LastName;
                    data.Address = !string.IsNullOrEmpty(dto.Address) ? dto.Address : data.Address;
                    data.DateOfBirth = dto.DateOfBirth != null ? dto.DateOfBirth : data.DateOfBirth;
                }
                if(dto != null && dto.UserImage == null)
                {
                    data.UserImage = data.UserImage;
                    data.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : data.FirstName;
                    data.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : data.LastName;
                    data.Address = !string.IsNullOrEmpty(dto.Address) ? dto.Address : data.Address;
                    data.DateOfBirth = dto.DateOfBirth != null ? dto.DateOfBirth : data.DateOfBirth;
                }

                data.UpdatedAt = DateTime.UtcNow;
                var result = await _userRepository.UpdateAsync(data);
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

        public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            var users = await _userRepository.SelectAll()
                .Where(t => t.IsDeleted == false)
                .Include(b => b.BorrowedBooks)
                .ToPagedList(@params)
                .AsNoTracking()
                .ToListAsync();
                
            foreach (var user in users)
            {
            };
            return _mapper.Map<IEnumerable<UserForResultDto>>(users);
        }

        public async Task<UserForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await _userRepository.SelectAll()
                .Where(t => t.Id == Id && t.IsDeleted == false)
                .Include(b => b.BorrowedBooks)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (data != null && data.IsDeleted == false)
            {
                return this._mapper.Map<UserForResultDto>(data);
            }
            throw new TahseenException(404, "User is not found");
        }
    }
}
