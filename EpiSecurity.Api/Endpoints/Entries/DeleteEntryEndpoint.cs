using EpiSecurity.Api.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EpiSecurity.Api.Endpoints.Entries
{
    //DeleteEntryEndpoint class will extend into EndpointBaseAsync with an ID request and will get a Entry based on that ID
    public class DeleteEntryEndpoint : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult
    {
        //Have a readonly variable from EntryDBContext
        private readonly EntryDBContext _context;

        //Get the context from EntryDBContext
        public DeleteEntryEndpoint(EntryDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpDelete("entries/{id}")]
        [SwaggerOperation(Summary = "Delete entry",
            Description = "Delete entry",
            OperationId = "Entry.Delete",
            Tags = new[] { "Endpoints for EpiSecurity API" })]
        public override async Task<ActionResult> HandleAsync(Guid id, CancellationToken cancellationToken = default)
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

            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
