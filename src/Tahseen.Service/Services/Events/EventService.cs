using System;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tahseen.Data.IRepositories;
using System.Collections.Generic;
using Tahseen.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.Events;
using Tahseen.Service.DTOs.Events.Event;
using Tahseen.Service.Interfaces.IEventService;
using Tahseen.Service.DTOs.Events.EventRegistration;

namespace Tahseen.Service.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> eventService;
        private readonly IMapper mapper;

        public async Task<bool> RemoveAsync(long Id)
        {
          return await eventService.DeleteAsync(Id);  
        }

        public IQueryable<EventForResultDto> RetrieveAll()
        {
            var dataItem = eventService.SelectAll();
            return this.mapper.Map<IQueryable<EventForResultDto>>(dataItem);
        }

        public async Task<EventForResultDto> RetrieveByIdAsync(long Id)
        {
            var dataItem = await eventService.SelectByIdAsync(Id);
            return this.mapper.Map<EventForResultDto>(dataItem);
        }

        public EventService(IRepository<Event> eventService, IMapper mapper)
        {
            this.eventService = eventService;
            this.mapper = mapper;
        }

        public async Task<EventForResultDto> AddAsync(EventForCreationDto dto)
        {
          var dataItem = this.mapper.Map<Event>(dto);
          var res = await this.eventService.CreateAsync(dataItem);
          return this.mapper.Map<EventForResultDto>(res);
        }

        public async Task<EventForResultDto> ModifyAsync(long Id, EventForUpdateDto dto)
        {
            var updateData = await this.eventService.SelectAll().FirstOrDefaultAsync(u => u.Id == Id);
            if (updateData is null || updateData.IsDeleted)
            {
                throw new TahseenException(404, " Event is not found");
            }
            var resData = this.mapper.Map(dto, updateData);
            var result = await this.eventService.UpdateAsync(resData);
            return this.mapper.Map<EventForResultDto>(result);
        }
    }
}
