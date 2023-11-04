using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Books.Genre;
using Tahseen.Service.Exceptions;
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
        var Check = await this._repository.SelectAll().Where(a => a.Name == dto.Name && a.IsDeleted == false).FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This Genre is exist");
        }
        var genre = _mapper.Map<Genre>(dto);
        var result= await _repository.CreateAsync(genre);
        return _mapper.Map<GenreForResultDto>(result);
    }

    public async Task<GenreForResultDto> ModifyAsync(long id, GenreForUpdateDto dto)
    {
        var genre = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (genre is not null)
        {
            var mappedGenre = _mapper.Map(dto, genre);
            mappedGenre.UpdatedAt = DateTime.UtcNow;
            var result = await _repository.UpdateAsync(mappedGenre);
            return _mapper.Map<GenreForResultDto>(result);
        }
        throw new Exception("Genre not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<GenreForResultDto>> RetrieveAllAsync()
    {
        var result = _repository.SelectAll().Where(e => e.IsDeleted != true);
        return _mapper.Map<IEnumerable<GenreForResultDto>>(result);
    }

    public async Task<GenreForResultDto> RetrieveByIdAsync(long id)
    {
        var genre = await _repository.SelectByIdAsync(id);
        if (genre is not null && !genre.IsDeleted)
            return _mapper.Map<GenreForResultDto>(genre);

        throw new Exception("Genre  not found");
    }
}