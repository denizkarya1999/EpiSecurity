using EpiSecurity.Api.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EpiSecurity.Api.Endpoints.Entries
{
    //GetEntryByIdEndpoint class will extend into EndpointBaseAsync with an ID request and will get a Entry based on that ID
    public class GetEntryByIdEndpoint : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult<Entry>
    {
        //Have a readonly variable from EntryDBContext
        private readonly EntryDBContext _context;

        //Get the context from EntryDBContext
        public GetEntryByIdEndpoint(EntryDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpGet("entries/{id}")]
        [SwaggerOperation(Summary = "Get the particular entry",
            Description = "Get the particular entry",
            OperationId = "Entry.GetID",
            Tags = new[] { "Endpoints for EpiSecurity API" })]
        public override async Task<ActionResult<Entry>> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (_context.Entries == null)
            {
                return NotFound();
            }
            var entry = await _context.Entries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            return entry;
        }
    }
}
