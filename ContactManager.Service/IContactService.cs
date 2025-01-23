using ContactManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Service
{
	public interface IContactService
	{
		/// <summary>
		/// Retrieves all contacts from the "contacts" index in Elasticsearch.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a list of contacts.</returns>
		Task<List<Contact>> GetAllContactsAsync();

		/// <summary>
		/// Retrieves a contact by its ID from Elasticsearch.
		/// </summary>
		/// <param name="id">The ID of the contact to retrieve.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the contact if found, or null if not found.</returns>
		Task<Contact?> GetContactByIdAsync(string id);

		/// <summary>
		/// Creates a new contact in Elasticsearch.
		/// </summary>
		/// <param name="contact">The contact object to create.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the contact with its ID assigned by Elasticsearch.</returns>
		Task<Contact> CreateContactAsync(Contact contact);

		/// <summary>
		/// Updates an existing contact in Elasticsearch.
		/// </summary>
		/// <param name="contact">The contact object to update, including its ID.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the update was successful, false otherwise.</returns>
		Task<bool> UpdateContactAsync(Contact contact);
	}
}
