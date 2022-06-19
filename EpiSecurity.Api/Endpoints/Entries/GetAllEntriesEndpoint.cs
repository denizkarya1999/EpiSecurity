using EpiSecurity.Api.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace EpiSecurity.Api.Endpoints.Entries
{
    //GetAllEntriesEndpoint will extend into EndPointBaseAsync without a request and it will get a list of entries
    public class GetAllEntriesEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<Entry>>
    {
        //Have a readonly variable from EntryDBContext
        private readonly EntryDBContext _context;

        //Get the context from EntryDBContext
        public GetAllEntriesEndpoint(EntryDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpGet("/entries")]
        [SwaggerOperation(Summary = "Get all entries",
            Description = "Get all entries",
            OperationId = "Entry.GetAll",
            Tags = new[] { "Endpoints for EpiSecurity API" })]
        public override async Task<ActionResult<List<Entry>>> HandleAsync
        (CancellationToken cancellationToken = default)
        {
            if (_context.Entries == null)
            {
                return NotFound();
            }

            return await _context.Entries.ToListAsync();
        }
    }
}