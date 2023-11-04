using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Reservations;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Reservations;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IReservationsServices;

namespace Tahseen.Service.Services.Reservations;

public class ReservationService : IReservationsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Reservation> _repository;

    public ReservationService(IMapper mapper, IRepository<Reservation> repository)
    {
        this._mapper = mapper;
        this._repository = repository;
    }

    public async Task<ReservationForResultDto> AddAsync(ReservationForCreationDto dto)
    {
        var Check = this._repository.SelectAll().Where(r => r.BookId == dto.BookId && r.UserId == r.UserId && r.IsDeleted == false).FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This reservation is exist");
        }
        var reservation = _mapper.Map<Reservation>(dto);
        var result= await _repository.CreateAsync(reservation);
        return _mapper.Map<ReservationForResultDto>(result);
    }

    public async Task<ReservationForResultDto> ModifyAsync(long id, ReservationForUpdateDto dto)
    {
        var reservation = await _repository.SelectAll().Where(r => r.Id == id && r.IsDeleted == false).FirstOrDefaultAsync();
        if (reservation is not null)
        {
            var mappedReservation = _mapper.Map(dto, reservation);
            mappedReservation.UpdatedAt = DateTime.UtcNow;
            var result = await _repository.UpdateAsync(mappedReservation);
            return _mapper.Map<ReservationForResultDto>(result);
        }
        throw new Exception("Reservation not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ReservationForResultDto>> RetrieveAllAsync()
    {
        var AllData = this._repository.SelectAll().Where(t => t.IsDeleted == false);
        return _mapper.Map<IEnumerable<ReservationForResultDto>>(AllData);
    }

    public async Task<ReservationForResultDto> RetrieveByIdAsync(long id)
    {
        var reservation = await _repository.SelectByIdAsync(id);
        if (reservation is not null && !reservation.IsDeleted)
            return _mapper.Map<ReservationForResultDto>(reservation);
        
        throw new Exception("Reservation  not found");
    }

 
}