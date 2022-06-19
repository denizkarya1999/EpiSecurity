using EpiSecurity.Api.Models;
using EpiSecurity.Shared;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RepoDb;
using Swashbuckle.AspNetCore.Annotations;

namespace EpiSecurity.Api.Endpoints.Entries
{
    //GetAllEntriesRepoDBEndpoint will extend into EndPointBaseAsync without a request and it will get a list of entries
    public class GetAllEntriesRepoDBEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<EntryResponseDTO>>
    {
        //Create a connectionString to be used
        private readonly string connectionString = @"Data Source=EPITEC-27\SQLEXPRESS04;Initial Catalog=EntriesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Do the same action as it was available in controller
        [HttpGet("/entries/repodb")]
        [SwaggerOperation(Summary = "Get all entries",
            Description = "Get all entries",
            OperationId = "Entry.GetAll.RepoDB",
            Tags = new[] { "Endpoints for EpiSecurity API using RepoDB" })]
        public override async Task<ActionResult<List<EntryResponseDTO>>> HandleAsync
        (CancellationToken cancellationToken = default)
        {
            //Open an Sql connection
            using (var connection = new SqlConnection(connectionString))
            {
                /* SQL Command:
                 * SELECT * FROM [dbo].[Entry];
                 */
                var entry = (await connection.ExecuteQueryAsync<EntryResponseDTO>("SELECT * FROM [dbo].[Entry]")).ToList();

                //Return it as list
                return entry;
            }
        }
     }
}
