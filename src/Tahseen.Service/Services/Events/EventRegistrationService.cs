using System;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tahseen.Domain.Entities;
using System.Collections.Generic;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.Events;
using Tahseen.Service.DTOs.Users.UserSettings;
using Tahseen.Service.Interfaces.IEventService;
using Tahseen.Service.DTOs.Events.EventRegistration;

namespace Tahseen.Service.Services.Events
{
    public class EventRegistrationService : IEventRegistrationService
    {
        private readonly IRepository<EventRegistration> eventRegistrationService;
        private readonly IMapper mapper;


        public async Task<bool> RemoveAsync(long Id)
        {
            return await eventRegistrationService.DeleteAsync(Id);
        }

        public IQueryable<EventRegistrationForResultDto> RetrieveAll()
        {
            var getData = eventRegistrationService.SelectAll();
            return this.mapper.Map<IQueryable<EventRegistrationForResultDto>>(getData);

        }


        public async Task<EventRegistrationForResultDto> RetrieveByIdAsync(long Id)
        {
            var getById = await eventRegistrationService.SelectByIdAsync(Id);
            return this.mapper.Map<EventRegistrationForResultDto>(getById);
        }


        public async Task<EventRegistrationForResultDto> AddAsync(EventRegistrationForCreationDto dto)
        {
            var dataRegistration = this.mapper.Map<EventRegistration>(dto);
            var dataResult = await this.eventRegistrationService.CreateAsync(dataRegistration);
            return this.mapper.Map<EventRegistrationForResultDto>(dataResult);
        }


        public async Task<EventRegistrationForResultDto> ModifyAsync(long Id, EventRegistrationForCreationDto dto)
        {
            var updateData = await this.eventRegistrationService.SelectAll().FirstOrDefaultAsync(u => u.Id == Id);
            if (updateData is null || updateData.IsDeleted)
            {
                throw new TahseenException(404, " Event registration is not found");
            }
            var resData = this.mapper.Map(dto, updateData);
            var result = await this.eventRegistrationService.UpdateAsync(resData);
            return this.mapper.Map<EventRegistrationForResultDto>(result);
        }
       
    }
}
