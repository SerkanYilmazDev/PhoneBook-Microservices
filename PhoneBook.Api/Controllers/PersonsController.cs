using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly IMediator _mediator;

        public PersonsController(PhoneBookDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default)
                return BadRequest();

            var response = await _dbContext.Persons.Include(p => p.Informations)
                                                   .Include(p => p.Locations)
                                                   .Include(p => p.Phones)
                                                   .Include(p => p.Emails).FirstOrDefaultAsync(s => s.Id == id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetList()
        {
            return Ok(await _dbContext.Persons.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> CreatePerson(CreatePersonCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var command = new DeletePersonCommand
            {
                PersonId = id
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("{id}/createPersonEmail")]
        public async Task<ActionResult> CreatePersonEmail(Guid id, CreatePersonEmailCommand command)
        {
            command.PersonId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("{id}/createPersonPhoneNumber")]
        public async Task<ActionResult> CreatePersonPhoneNumber(Guid id, CreatePersonPhoneNumberCommand command)
        {
            command.PersonId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("{id}/createPersonLocation")]
        public async Task<ActionResult> CreatePersonLocation(Guid id, CreatePersonLocationCommand command)
        {
            command.PersonId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("{id}/createPersonInformation")]
        public async Task<ActionResult> CreatePersonInformation(Guid id, CreatePersonInformationCommand command)
        {
            command.PersonId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{emailId}/deletePersonEmail")]
        public async Task<IActionResult> DeletePersonEmail(Guid emailId)
        {
            var command = new DeletePersonEmailCommand
            {
                EmailId = emailId
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{phoneNumberId}/deletePersonPhoneNumber")]
        public async Task<IActionResult> DeletePersonPhoneNumber(Guid phoneNumberId)
        {
            var command = new DeletePersonPhoneNumberCommand
            {
                PhoneNumberId = phoneNumberId
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{locationId}/deletePersonLocation")]
        public async Task<IActionResult> DeletePersonLocation(Guid locationId)
        {
            var command = new DeletePersonLocationCommand
            {
                LocationId = locationId
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{informationId}/deletePersonInformation")]
        public async Task<IActionResult> DeletePersonInformation(Guid informationId)
        {
            var command = new DeletePersonInformationCommand
            {
                InformationId = informationId
            };
            return Ok(await _mediator.Send(command));
        }
    }
}
