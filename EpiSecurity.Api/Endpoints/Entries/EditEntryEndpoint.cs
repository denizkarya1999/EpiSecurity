using EpiSecurity.Api.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EpiSecurity.Shared;

namespace EpiSecurity.Api.Endpoints.Entries
{
    //EditEntryEndpoint class will extend into EndPointBaseAsync with request to Entry without showing a result
    public class EditEntryEndpoint : EndpointBaseAsync
        .WithRequest<EntryResponseDTO>
        .WithoutResult
    {
        //Have a readonly variable from EntryDBContext
        private readonly EntryDBContext _context;

        //Get the context from EntryDBContext
        public EditEntryEndpoint(EntryDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpPut("/entries/edit")]
        [SwaggerOperation(Summary = "Edit entry",
            Description = "Edit entry",
            OperationId = "Entry.Edit",
            Tags = new[] { "Endpoints for EpiSecurity API" })]
        public override async Task<ActionResult> HandleAsync(EntryResponseDTO EntryRequest, CancellationToken cancellationToken = default)
        {
            var entry = await _context.Entries.FindAsync(EntryRequest.EntryId);

            if (entry is not null)
            {
                entry.Email = EntryRequest.Email;
                entry.FirstName = EntryRequest.FirstName;
                entry.Gender = EntryRequest.Gender;
                entry.IsBlackListed = EntryRequest.IsBlackListed;
                entry.IsCustomer = EntryRequest.IsCustomer;
                entry.LastName = EntryRequest.LastName;
                entry.PhoneNumber = EntryRequest.PhoneNumber;

                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
