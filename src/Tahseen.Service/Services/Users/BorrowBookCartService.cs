using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Users;
using Tahseen.Service.DTOs.Users.BorrowedBookCart;
using Tahseen.Service.DTOs.Users.UserCart;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class BorrowBookCartService : IBorrowBookCartService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BorrowedBookCart> _repository;

        public BorrowBookCartService(IMapper mapper, IRepository<BorrowedBookCart> repository)
        {
            _repository = repository;
            this._mapper = mapper;
        }

        

        public async Task<BorrowedBookCartForResultDto> AddAsync(BorrowedBookCartForCreationDto dto)
        {
            var result = await _repository.SelectAll().Where(e => e.UserId == dto.UserId && e.IsDeleted == false).FirstOrDefaultAsync();
            if (result != null)
            {
                throw new TahseenException(400, "BorrowedBookCart is exist");
            }
            var data = this._mapper.Map<BorrowedBookCart>(dto);
            var result2 = await this._repository.CreateAsync(data);
            return this._mapper.Map<BorrowedBookCartForResultDto>(result2);
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            return await this._repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<BorrowedBookCartForResultDto>> RetrieveAllAsync()
        {
            var AllData = this._repository.SelectAll().Where(t => t.IsDeleted == false); ;
            return this._mapper.Map<IEnumerable<BorrowedBookCartForResultDto>>(AllData);
        }

        public async Task<BorrowedBookCartForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await _repository.SelectByIdAsync(Id);
            if (data != null && data.IsDeleted == false)
            {
                return this._mapper.Map<BorrowedBookCartForResultDto>(data);
            }
            throw new TahseenException(404, "Not Found");
        }
    }
}
