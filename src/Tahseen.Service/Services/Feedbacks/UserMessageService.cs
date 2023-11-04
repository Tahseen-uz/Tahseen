using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Feedbacks;
using Tahseen.Service.DTOs.Feedbacks.UserMessages;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IFeedbackService;

namespace Tahseen.Service.Services.Feedbacks;

public class UserMessageService : IUserMessageService
{
    private readonly IMapper mapper;
    private readonly IRepository<UserMessage> repository;

    public UserMessageService(IMapper mapper, IRepository<UserMessage> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<UserMessageForResultDto> AddAsync(UserMessageForCreationDto dto)
    {
        var mapped = mapper.Map<UserMessage>(dto);
        var result = await repository.CreateAsync(mapped);
        return mapper.Map<UserMessageForResultDto>(result);
    }

    public async Task<UserMessageForResultDto> ModifyAsync(long id,UserMessageForUpdateDto dto)
    {
        var getUserMessage = await repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (getUserMessage == null)
            throw new TahseenException(404, "UserMessage doesn't found");

        var mappedUserMessage = mapper.Map(dto, getUserMessage);
        var result = await repository.UpdateAsync(mappedUserMessage);

        return mapper.Map<UserMessageForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id) => await repository.DeleteAsync(id);

    public async Task<IEnumerable<UserMessageForResultDto>> RetrieveAllAsync()
    {
        var results = repository.SelectAll().Where(t => !t.IsDeleted);
        return mapper.Map<IEnumerable<UserMessageForResultDto>>(results);
    }

    public async Task<UserMessageForResultDto> RetrieveByIdAsync(long id)
    {
        var result = await repository.SelectByIdAsync(id);
        if (result is null && result.IsDeleted)
            throw new TahseenException(404, "UserMessage doesn't found");

        return mapper.Map<UserMessageForResultDto>(result);
    }
}
