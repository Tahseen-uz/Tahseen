using AutoMapper;
using Tahseen.Service.Exceptions;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.DTOs.Books.Genre;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Service.Services.Books;

public class GenreService : IGenreService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Genre> _repository;

    public GenreService(IMapper mapper, IRepository<Genre> repository)
    {
        this._mapper = mapper;
        this._repository = repository;
    }

    public async Task<GenreForResultDto> AddAsync(GenreForCreationDto dto)
    {
        var Check = await this._repository.SelectAll()
            .Where(a => a.Name == dto.Name && a.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (Check != null)
            throw new TahseenException(409, "This Genre is already exist");
        
        var genre = this._mapper.Map<Genre>(dto);
        var result= await this._repository.CreateAsync(genre);
        return this._mapper.Map<GenreForResultDto>(result);
    }

    public async Task<GenreForResultDto> ModifyAsync(long id, GenreForUpdateDto dto)
    {
        var genre = await _repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (genre is not null)
        {
            var mappedGenre = this._mapper.Map(dto, genre);
            mappedGenre.UpdatedAt = DateTime.UtcNow;
            var result = await this._repository.UpdateAsync(mappedGenre);
            return this._mapper.Map<GenreForResultDto>(result);
        }
        throw new Exception("Genre not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        return await this._repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<GenreForResultDto>> RetrieveAllAsync()
    {
        var result = await this._repository.SelectAll()
            .Where(e => e.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync();

        return this._mapper.Map<IEnumerable<GenreForResultDto>>(result);
    }

    public async Task<GenreForResultDto> RetrieveByIdAsync(long id)
    {
        var genre = await this._repository.SelectAll()
            .Where(g => g.Id == id && g.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if(genre is null)
            throw new Exception("Genre  not found");

        return this._mapper.Map<GenreForResultDto>(genre);
    }
}