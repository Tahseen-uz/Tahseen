using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Reservations;

namespace Tahseen.Service.Interfaces.IReservationsServices
{
    public interface IReservationsService
    {
        public Task<ReservationForResultDto> AddAsync(ReservationForCreationDto dto);
        public Task<ReservationForResultDto> ModifyAsync(long Id, ReservationForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<ReservationForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<ReservationForResultDto>> RetrieveAllAsync(PaginationParams @params);
    }
}
