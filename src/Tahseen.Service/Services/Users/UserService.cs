﻿using AutoMapper;
using Tahseen.Service.Helpers;
using Tahseen.Domain.Entities;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Extensions;
using Tahseen.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.Library;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.DTOs.Users.UserCart;
using Tahseen.Service.DTOs.Users.UserSettings;
using Tahseen.Service.Interfaces.IUsersService;
using Tahseen.Service.DTOs.Users.ChangePassword;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Service.DTOs.Users.BorrowedBookCart;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IFineService _fineService;
        private readonly IUserCartService _userCartService;
        private readonly IRepository<User> _userRepository;
        private readonly IUserRatingService _userRatingService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IUserSettingService _userSettingService;
        private readonly IBorrowBookCartService _borrowBookCartService;
        private readonly IRepository<LibraryBranch> _libraryBranchRepository;
        private readonly IUserProgressTrackingService _userProgressTrackingService;

        public UserService(
            IMapper mapper,
            IFineService fineService,
            IUserCartService userCartService,
            IRepository<User> userRepository,
            IUserRatingService userRatingService,
            IFileUploadService fileUploadService,
            IUserSettingService userSettingService,
            IBorrowBookCartService borrowBookCartService,
            IRepository<LibraryBranch> libraryBranchRepository,
            IUserProgressTrackingService userProgressTrackingService)
        {
            this._mapper = mapper;
            this._fineService = fineService;
            this._userRepository = userRepository;
            this._userCartService = userCartService;
            this._fileUploadService = fileUploadService;
            this._userRatingService = userRatingService;
            this._userSettingService = userSettingService;
            this._borrowBookCartService = borrowBookCartService;
            this._libraryBranchRepository = libraryBranchRepository;
            this._userProgressTrackingService = userProgressTrackingService;
        }
        public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
        {
            var result = await _userRepository.SelectAll()
                .Where(e => e.FirstName == dto.FirstName 
                        && e.LastName == dto.LastName  
                        && e.LibraryBranchId == dto.LibraryBranchId 
                        && e.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            var LibraryChecking = await _libraryBranchRepository
                .SelectAll().Where(l => l.Id == dto.LibraryBranchId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if(LibraryChecking == null)
                throw new TahseenException(404, "This library is not found");
            
            if (result != null)
                throw new TahseenException(400, "User is exist");
            
            var data = this._mapper.Map<User>(dto);

            if(dto.UserImage != null)
            {
                var FileUloadForCreation = new FileUploadForCreationDto
                {
                    FolderPath = "UsersAssets",
                    FormFile = dto.UserImage
                };
                var FileResult = await this._fileUploadService.FileUploadAsync(FileUloadForCreation);
                data.UserImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
            }


            var HashedPassword = PasswordHelper.Hash(dto.Password);
            data.Password = HashedPassword.Hash;
            data.Salt = HashedPassword.Salt;
            data.Role = Tahseen.Domain.Enums.Roles.User;
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
            var data = await _userRepository
                .SelectAll()
                .Where(e => e.Id == Id && e.IsDeleted == false)
                .FirstOrDefaultAsync();
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
            var data = await _userRepository
                .SelectAll()
                .Where(e => e.Id == Id && e.IsDeleted == false)
                .FirstOrDefaultAsync();
            if (data is not null)
            {
                if (dto != null)
                {
                    if (dto.UserImage != null)
                    {
                        if (data.UserImage != null)
                        {
                            await this._fileUploadService.FileDeleteAsync(data.UserImage);
                        }

                        var FileUploadForCreation = new FileUploadForCreationDto
                        {
                            FolderPath = "UsersAssets",
                            FormFile = dto.UserImage
                        };
                        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

                        data.UserImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
                    }

                    // Update other properties only if dto's property is not null or empty
                    data.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : data.FirstName;
                    data.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : data.LastName;
                    data.Address = !string.IsNullOrEmpty(dto.Address) ? dto.Address : data.Address;
                    data.DateOfBirth = dto.DateOfBirth != null ? dto.DateOfBirth : data.DateOfBirth;

                    data.UpdatedAt = DateTime.UtcNow;
                    var result = await _userRepository.UpdateAsync(data);
                    return _mapper.Map<UserForResultDto>(result);
                }

                // If dto is null, you may want to handle this case (throw an exception or return a specific result).
                // For now, I'm returning the existing user data.
                return _mapper.Map<UserForResultDto>(data);
            }

            throw new TahseenException(404, "User is not found");
        }


        public async Task<bool> RemoveAsync(long Id)
        {
            var user = await this._userRepository.SelectAll()
                .Where(u => u.Id == Id && u.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
                throw new TahseenException(404, "User is not found");
            
            if(user.UserImage != null) 
            {
                await this._fileUploadService.FileDeleteAsync(user.UserImage);
            }
            return await _userRepository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params, long id)
        {
            var users = await _userRepository.SelectAll()
                .Where(t => t.IsDeleted == false && t.LibraryBranchId == id)
                .Include(b => b.BorrowedBooks.Where(n => n.IsDeleted == false))
                .Include(l => l.LibraryBranch) // Include related LibraryBranch
                .Where(l => l.LibraryBranch.IsDeleted == false)
                .ToPagedList(@params)
                .AsNoTracking()
                .ToListAsync();
                
            var result = _mapper.Map<IEnumerable<UserForResultDto>>(users);
 
            return result;
        }

        public async Task<UserForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await _userRepository.SelectAll()
                .Where(t => t.Id == Id && t.IsDeleted == false)
                .Include(b => b.BorrowedBooks.Where(n => n.IsDeleted == false))
                .Include(l => l.LibraryBranch) // Include related LibraryBranch
                //.Where(l => l.LibraryBranch.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (data != null && data.IsDeleted == false)
            {
                var result = this._mapper.Map<UserForResultDto>(data);
                return result;
            }
            throw new TahseenException(404, "User is not found");
        }
    }
}
