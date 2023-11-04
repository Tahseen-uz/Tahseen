using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.DTOs.Users.Fine;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class FineService : IFineService
    {
        private readonly IRepository<Fine> _fineRepository;
        private readonly IMapper _mapper;

        public FineService(IRepository<Fine> fineRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._fineRepository = fineRepository;
        }
        public async Task<FineForResultDto> AddAsync(FineForCreationDto dto)
        {
            var MappedData = this._mapper.Map<Fine>(dto);
            var result = await this._fineRepository.CreateAsync(MappedData);
            return this._mapper.Map<FineForResultDto>(result);
        }

        public async Task<FineForResultDto> ModifyAsync(long Id, FineForUpdateDto dto)
        {
            var data = await this._fineRepository.SelectAll().FirstOrDefaultAsync(e => e.Id == Id);
            if (data != null && data.IsDeleted == false)
            {
                var MappedData = this._mapper.Map(dto, data);
                MappedData.UpdatedAt = DateTime.UtcNow;
                var result = await this._fineRepository.UpdateAsync(MappedData);
                return this._mapper.Map<FineForResultDto>(result);
            }
            throw new TahseenException(404, "Fine is not found");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            return await this._fineRepository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<FineForResultDto>> RetrieveAllAsync()
        {
            var AllData = this._fineRepository.SelectAll();
            return this._mapper.Map<IEnumerable<FineForResultDto>>(AllData);
        }

        public async Task<FineForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await this._fineRepository.SelectByIdAsync(Id);
            return this._mapper.Map<FineForResultDto>(data);
        }
    }
}
