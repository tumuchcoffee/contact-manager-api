using ContactManager.Domain;

namespace ContactManagerAPI.Extensions
{
	public class GetAllResponse
	{
		public List<Contact>? Result { get; set; }
		public string? Exception { get; set; }
		public bool HasError { get; set; }
	}

	public class GetByIdResponse
	{
		public Contact? Result { get; set; }
		public Exception? Exception { get; set; }
		public bool HasError { get; set; }
	}

	public class CreateResponse
	{
		public Contact? Result { get; set; }
		public Exception? Exception { get; set; }
		public bool HasError { get; set; }
	}

	public class UpdateResponse
	{
		public bool? Result { get; set; }
		public Exception? Exception { get; set; }
		public bool HasError { get; set; }
	}
}
