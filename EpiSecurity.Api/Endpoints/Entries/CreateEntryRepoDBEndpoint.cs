using EpiSecurity.Api.Models;
using EpiSecurity.Shared;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.Annotations;

namespace EpiSecurity.Api.Endpoints.Entries
{
    //CreateEntryEndpoint class will extend into EndpointBaseAsync with request to EntryResponseDto and will give no result
    public class CreateEntryRepoDBEndpoint : EndpointBaseAsync
        .WithRequest<EntryResponseDTO>
        .WithoutResult
    {
        //Create a connectionString to be used
        private readonly string connectionString = @"Data Source=EPITEC-27\SQLEXPRESS04;Initial Catalog=EntriesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Do the same action as it was available in controller
        [HttpPost("entries/repodb")]
        [SwaggerOperation(Summary = "Create a new entry",
            Description = "Create a new entry",
            OperationId = "Entry.Create.RepoDB",
            Tags = new[] { "Endpoints for EpiSecurity API using RepoDB" })]
        public override async Task<ActionResult<Entry>> HandleAsync(EntryResponseDTO entry, CancellationToken cancellationToken = default)
        {
            //Open an Sql connection
            using (var connection = new SqlConnection(connectionString))
            {
                //Execute SQL Comment to add the patient
                /*
                 * INSERT INTO [dbo].[Entry] (EntryId, Email, FirstName, Gender, IsBlackListed, IsContractOrEmployee, IsCustomer, LastName, PhoneNumber)
                 * VALUES ('@EntryId', '@Email', '@FirstName', '@Gender', '@IsBlackListed', '@IsContractOrEmployee', '@IsCustomer', '@LastName', '@PhoneNumber');
                 */
                connection.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO [dbo].[Entry] (EntryId, Email, FirstName, Gender, IsBlackListed, IsContractOrEmployee, IsCustomer, LastName, PhoneNumber)" +
                    " VALUES (@EntryId, @Email, @FirstName, @Gender, @IsBlackListed, @IsContractOrEmployee, @IsCustomer, @LastName, @PhoneNumber);",
                    connection);
                cmd.Parameters.AddWithValue("@EntryId", entry.EntryId);
                cmd.Parameters.AddWithValue("@Email", entry.Email);
                cmd.Parameters.AddWithValue("@FirstName", entry.FirstName);
                cmd.Parameters.AddWithValue("@Gender", entry.Gender);
                cmd.Parameters.AddWithValue("@IsBlackListed", entry.IsBlackListed);
                cmd.Parameters.AddWithValue("@IsContractOrEmployee", entry.IsContractorOrEmployee);
                cmd.Parameters.AddWithValue("@IsCustomer", entry.IsCustomer);
                cmd.Parameters.AddWithValue("@LastName", entry.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", entry.PhoneNumber);
                cmd.ExecuteNonQuery();
                connection.Close();

                //Write a parameter to add 
                return Ok();
            }
        }
    }
}
