namespace EpiSecurity.Shared
{
    public class EntryResponseDTO
    {
        /*
         * Constructors that will check these details;
         * Entry ID
         * First Name
         * Last Name
         * Birth Date
         * Gender
         * Phone Number
         * E-Mail
         * Whether they are a Contractor or Employee
         * Whether they are customer or not
         * Whether they are blacklisted by the company
         */
        public Guid EntryId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public bool IsBlackListed { get; set; }
        public bool IsContractorOrEmployee { get; set; }
        public bool IsCustomer { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}