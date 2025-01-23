using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Domain;
using ContactManager.Data;
using Elastic.Clients.Elasticsearch;

namespace ContactManager.Service
{
	public class ContactService : IContactService
	{
		private readonly ElasticDataLayer<Contact> _elasticDataLayer;
		private readonly Uri elasticsearchUri;

		public ContactService()
		{
			elasticsearchUri = new Uri("http://localhost:9200");
			_elasticDataLayer = new ElasticDataLayer<Contact>(elasticsearchUri, "contacts");
		}

		/// <summary>
		/// Retrieves all contacts from the "contacts" index in Elasticsearch.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a list of contacts.</returns>
		public async Task<List<Contact>> GetAllContactsAsync()
		{
			return await _elasticDataLayer.GetAllContacts();
		}

		/// <summary>
		/// Retrieves a contact by its ID from Elasticsearch.
		/// </summary>
		/// <param name="id">The ID of the contact to retrieve.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the contact if found, or null if not found.</returns>
		public async Task<Contact?> GetContactByIdAsync(string id)
		{
			var response = await _elasticDataLayer.GetDocumentAsync(id);

			if (!response.IsValidResponse)
			{
				return null; // Contact not found or operation failed
			}

			return response.Source;
		}

		/// <summary>
		/// Creates a new contact in Elasticsearch.
		/// </summary>
		/// <param name="contact">The contact object to create.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the contact with its ID assigned by Elasticsearch.</returns>
		public async Task<Contact> CreateContactAsync(Contact contact)
		{
			var validator = new ContactValidator();
			var results = validator.Validate(contact);

			if (!results.IsValid) {
				throw new Exception("Contact failed validation.");
			}

			return await _elasticDataLayer.CreateContact(contact);
		}

		/// <summary>
		/// Updates an existing contact in Elasticsearch.
		/// </summary>
		/// <param name="contact">The contact object to update, including its ID.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the update was successful, false otherwise.</returns>
		public async Task<bool> UpdateContactAsync(Contact contact)
		{
			if (string.IsNullOrEmpty(contact.Id))
			{
				throw new ArgumentException("Contact ID must be provided for updates.");
			}

			var response = await _elasticDataLayer.UpdateDocumentAsync(contact.Id, contact);

			if (!response.IsValidResponse)
			{
				return false;
			}

			return true;
		}
	}
}
