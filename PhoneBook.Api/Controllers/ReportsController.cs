using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;
using Shared.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("/report-api")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly IMediator _mediator;
        protected readonly IBusPublisher BusPublisher;


        public ReportsController(PhoneBookDbContext dbContext,
            IMediator mediator,
            IBusPublisher busPublisher)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            BusPublisher = busPublisher;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePersonReportsByLocation(CreatePersonReportsByLocationCommand command)
        {
            var context = CorrelationContext.Create(Guid.NewGuid(), command.PersonId);

            await BusPublisher.SendAsync(command, context);

            return Accepted(context);
        }
    }
}
