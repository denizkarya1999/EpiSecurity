using EpiSecurity.Api.Models;
using EpiSecurity.Shared;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RepoDb;
using Swashbuckle.AspNetCore.Annotations;

namespace EpiSecurity.Api.Endpoints.Entries
{
    //GetEntryByIDRepoDBEndpoint class will extend into EndpointBaseAsync with an ID request and will get the entry based on that ID
    public class GetEntryByIdRepoDBEndpoint : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult<EntryResponseDTO>
    {
        //Create a connectionString to be used
        private readonly string connectionString = @"Data Source=EPITEC-27\SQLEXPRESS04;Initial Catalog=EntriesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Do the same action as it was available in controller
        [HttpGet("/entries/repodb/{id}")]
        [SwaggerOperation(Summary = "Get entry by Id",
            Description = "Get entry by Id",
            OperationId = "Entry.GetById.RepoDB",
            Tags = new[] { "Endpoints for EpiSecurity API using RepoDB" })]
        public override async Task<ActionResult<EntryResponseDTO>> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //Open an Sql connection
            using (var connection = new SqlConnection(connectionString))
            {
                //Create a parameter to make Id usable for SQL command
                var parameter = new { EntryId = id };
                //Execute SQL command to get the user by Id
                var entry = (await connection.ExecuteQueryAsync<EntryResponseDTO>("SELECT * FROM [dbo].[Entry] WHERE EntryId = @EntryId;", parameter)).FirstOrDefault();
                //Return patient
                return entry;
            }
        }
    }
}
