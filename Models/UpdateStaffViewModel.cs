namespace StaffRegistration.Models
{
    public class UpdateStaffViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ContactNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
