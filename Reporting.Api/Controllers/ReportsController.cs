using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reporting.Api.Data;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Reporting.Api.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ReportDbContext _dbContext;

        public ReportsController(ReportDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        [HttpGet("personId/{id}")]
        public async Task<ActionResult> GetReportsByPersonId(Guid id)
        {
            return Ok(await _dbContext.Reports.Where(s => s.PersonId == id).ToListAsync());
        }
    }
}
