//Implement the needed library
using EpiSecurity.Shared;

namespace EpiSecurity.Blazor.Services
{
    //This is the interface of the EntryService
    interface IEntryService
    {
        //Methods for executing CRUD operations (CreateEntry, DisplayAllEntries, DisplayEntryDetails, RemoveEntry and UpdateEntry)
        Task<EntryResponseDTO> CreateEntry(EntryResponseDTO patient);
        Task<IEnumerable<EntryResponseDTO>> DisplayAllEntries();
        Task<EntryResponseDTO> DisplayEntryDetails(Guid entryId);
        Task RemoveEntry(Guid entryId);
        Task UpdateEntry(EntryResponseDTO patient);
    }
}
