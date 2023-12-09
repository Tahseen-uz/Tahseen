using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Reservations;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Reservations;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Tahseen.Service.Interfaces.IReservationsServices;

namespace Tahseen.Service.Services.Reservations;

public class ReservationService : IReservationsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Reservation> _repository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Book> _bookRepository;
    public ReservationService(
        IMapper mapper, 
        IRepository<Reservation> repository,
        IRepository<Book> bookRepository,
        IRepository<User> userRepository)
    {
        this._mapper = mapper;
        this._repository = repository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<ReservationForResultDto> AddAsync(ReservationForCreationDto dto)
    {
        var Check = this._repository.SelectAll()
            .Where(r => r.BookId == dto.BookId && r.UserId == r.UserId && r.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This reservation is exist");
        }

        var user = await _userRepository.SelectAll().
           Where(u => u.Id == dto.UserId && u.IsDeleted == false).
           AsNoTracking().
           FirstOrDefaultAsync();

        if (user == null)
            throw new TahseenException(404, "User is not found");

        var book = await _bookRepository.SelectAll().
            Where(e => e.Id == dto.BookId && e.IsDeleted == false).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (book == null)
            throw new TahseenException(404, "Book is not found");

        var reservation = _mapper.Map<Reservation>(dto);
        var result= await _repository.CreateAsync(reservation);
        return _mapper.Map<ReservationForResultDto>(result);
    }

    public async Task<ReservationForResultDto> ModifyAsync(long id, ReservationForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll().
           Where(u => u.Id == dto.UserId && u.IsDeleted == false).
           AsNoTracking().
           FirstOrDefaultAsync();

        if (user == null)
            throw new TahseenException(404, "User is not found");

        var book = await _bookRepository.SelectAll().
            Where(e => e.Id == dto.BookId && e.IsDeleted == false).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (book == null)
            throw new TahseenException(404, "Book is not found");

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

    public async Task<IEnumerable<ReservationForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var AllData = await this._repository.SelectAll()
            .Where(t => t.IsDeleted == false)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        var result = this._mapper.Map<IEnumerable<ReservationForResultDto>>(AllData);
        foreach(var item in result)
        {
            item.ReservationStatus = item.ReservationStatus.ToString();
        }
        return result;
    }

    public async Task<ReservationForResultDto> RetrieveByIdAsync(long id)
    {
        var reservation = await _repository.SelectByIdAsync(id);
        if (reservation is not null && !reservation.IsDeleted)
        {
            var result = this._mapper.Map<ReservationForResultDto>(reservation);
            result.ReservationStatus = result.ReservationStatus.ToString();
            return result;
        }

        throw new Exception("Reservation  not found");
    }

 
}