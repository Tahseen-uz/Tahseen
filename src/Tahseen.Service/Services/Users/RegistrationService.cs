using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;
using Tahseen.Service.DTOs.Users.Registration;


namespace Tahseen.Service.Services.Users
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<Registration> _regisRepo;
        private readonly IMapper _mapper;
        public RegistrationService(IRepository<Registration> RegisRepo, IMapper Mapper)
        {
            this._mapper = Mapper;
            this._regisRepo = RegisRepo;
        }
        public async Task<RegistrationForResultDto> AddAsync(RegistrationForCreationDto dto)
        {
            var result = await this._regisRepo.SelectAll().Where(e => e.PhoneNumber == dto.PhoneNumber && e.Password == dto.Password && e.IsDeleted == false).FirstOrDefaultAsync();
            if(result == null)
            {
                var Data = this._mapper.Map<Registration>(dto);
                var CreatedData = await _regisRepo.CreateAsync(Data);
                return this._mapper.Map<RegistrationForResultDto>(CreatedData);
            }
            throw new TahseenException(400, "This user is exist");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            return await this._regisRepo.DeleteAsync(Id);
        }

        public async Task<IEnumerable<RegistrationForResultDto>> RetrieveAllAsync()
        {
            var AllData = this._regisRepo.SelectAll();
            return this._mapper.Map<IEnumerable<RegistrationForResultDto>>(AllData);
        }

        public async Task<RegistrationForResultDto> RetrieveByIdAsync(long Id)
        {
            var Data = await this._regisRepo.SelectByIdAsync(Id);
            return this._mapper.Map<RegistrationForResultDto>(Data);
        }
    }
}
