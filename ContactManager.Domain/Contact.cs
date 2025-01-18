namespace ContactManager.Domain
{
	public class Contact
	{
		public string? Id { get; set; }
		public string FirstName { get; set; }
		public string? LastName { get; set; }
		public long? Phone { get; set; } // Using long for phone number to handle international numbers
		public string? Address1 { get; set; }
		public string? Address2 { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public string? ZipCode { get; set; }
		public string? Email { get; set; }
	}
}
