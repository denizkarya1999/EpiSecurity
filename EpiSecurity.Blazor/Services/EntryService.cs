//Implement the needed libraries
using EpiSecurity.Shared;
using System.Text;
using System.Text.Json;

namespace EpiSecurity.Blazor.Services
{
    public class EntryService : IEntryService
    {
        //An HttpClient which will be injected through constructor injection
        private readonly HttpClient _httpClient;

        //Create a constructor to use HttpClient
        public EntryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //This method will add new entrants using EpiSecurity.API
        public async Task<EntryResponseDTO> CreateEntry(EntryResponseDTO entry)
        {
            //Seriliaze the entry
            var entrantJson =
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(entry), Encoding.UTF8, "application/json");

            //Post the patient to route of CreateEntryEndpoint
            var response = await _httpClient.PostAsync($"/entries", entrantJson);

            var responseContent = await response.Content.ReadAsStringAsync();

            //Otherwise return as null
            return null;
        }

        //This method will get all of the entrants from EpiSecurity.API
        public async Task<IEnumerable<EntryResponseDTO>> DisplayAllEntries()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<EntryResponseDTO>>
                (await _httpClient.GetStreamAsync($"/entries/repodb"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        //This method will get the entrant by id from EpiSecurity.API
        public async Task<EntryResponseDTO> DisplayEntryDetails(Guid entryId)
        {
            return await JsonSerializer.DeserializeAsync<EntryResponseDTO>
                (await _httpClient.GetStreamAsync($"/entries/repodb/{entryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        //This method will delete the entrant from database using EpiSecurity.API
        public async Task RemoveEntry(Guid entryId)
        {
            await _httpClient.DeleteAsync($"/entries/{entryId}");
        }

        //This method will edit the entrant using EpiSecurity.API
        public async Task UpdateEntry(EntryResponseDTO entry)
        {
            //Find the entry
            var patientJson =
                new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");

            //Put entry into system
            await _httpClient.PutAsync($"/entries/edit", patientJson);
        }
    }
}
