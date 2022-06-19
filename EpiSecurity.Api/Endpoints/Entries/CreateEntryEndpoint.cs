using EpiSecurity.Api.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EpiSecurity.Shared;

namespace EpiSecurity.Api.Endpoints.Entries
{
    //CreateEntryEndpoint class will extend into EndpointBaseAsync with request to Entry and will give no result
    public class CreateEntryEndpoint : EndpointBaseAsync
        .WithRequest<EntryResponseDTO>
        .WithoutResult
    {
        //Have a readonly variable from EntryDBContext
        private readonly EntryDBContext _context;

        //Get the context from EntryDBContext
        public CreateEntryEndpoint(EntryDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpPost("/entries")]
        [SwaggerOperation(Summary = "Create a new entry",
            Description = "Create a new entry",
            OperationId = "Entry.Create",
            Tags = new[] { "Endpoints for EpiSecurity API" })]
        public override async Task<ActionResult> HandleAsync(EntryResponseDTO entryRequest, CancellationToken cancellationToken = default)
        {
            if (_context.Entries == null)
            {
                return Problem("Entity set 'EntryDBContext.Patients'  is null.");
            }

            var entry = new Entry()
            {
                EntryId = entryRequest.EntryId,
                Email = entryRequest.Email,
                FirstName = entryRequest.FirstName,
                LastName = entryRequest.LastName,
                Gender = entryRequest.Gender,
                IsBlackListed = entryRequest.IsBlackListed,
                IsContractOrEmployee = entryRequest.IsContractorOrEmployee,
                IsCustomer = entryRequest.IsCustomer,
                PhoneNumber = entryRequest.PhoneNumber
            };

            try{
                _context.Entries.Add(entry);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }

            return Ok();
        }
    }
}
