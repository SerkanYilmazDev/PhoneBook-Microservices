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
        private readonly ReportDbContext _dbContext;

        public ReportsController(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("personId/{id}")]
        public async Task<ActionResult> GetReportsByPersonId(Guid id)
        {
            return Ok(await _dbContext.Reports.Where(s => s.PersonId == id).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _dbContext.Reports.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetReport(Guid id)
        {
            if (id == default)
                return BadRequest();

            var response = await _dbContext.Reports.FirstOrDefaultAsync(s => s.Id == id);

            return Ok(response);
        }
    }
}
