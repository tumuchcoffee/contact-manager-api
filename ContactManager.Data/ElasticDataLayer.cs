using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ContactManager.Domain;
using Elastic.Transport;

namespace ContactManager.Data
{
	public class ElasticDataLayer<T> where T : class
	{
		public readonly ElasticsearchClient _client;

		/// <summary>
		/// Initializes a new instance of the ElasticsearchDataLayer class.
		/// </summary>
		/// <param name="uri">The URI of the Elasticsearch server.</param>
		public ElasticDataLayer(Uri uri, string indexName)
		{
			var settings = new ElasticsearchClientSettings(uri)
				.DefaultIndex(indexName)
				.Authentication(new BasicAuthentication("elastic", "changeme"));

			_client = new ElasticsearchClient(settings);
		}

		/// <summary>
		/// Indexes a document in Elasticsearch.
		/// </summary>
		/// <param name="document">The document to index.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the index response.</returns>
		public async Task<Contact> CreateContact(Contact document)
		{
			var response = await _client.IndexAsync<Contact>(document);

			if (response.IsValidResponse)
			{
				return new Contact()
				{
					Address1 = document.Address1,
					Address2 = document.Address2,
					City = document.City,
					State = document.State,
					ZipCode = document.ZipCode,
					Email = document.Email,
					Phone = document.Phone,
					FirstName = document.FirstName,
					LastName = document.LastName,
					Id = response.Id
				};
			}
			else
			{
				throw new Exception(response.DebugInformation.ToString());
			}
		}

		public async Task<List<Contact>> GetAllContacts()
		{
			var response = await _client.SearchAsync<Contact>(s => s
				.Index("contacts")
				.Query(q => q
					.MatchAll(m => new MatchAllQueryDescriptor())
				)
			);

			if (response.IsValidResponse)
			{
				// Convert hits to a list of Contact objects
				return response.Hits.Select(h => h.Source).ToList();
			}

			return new List<Contact>();
		}

		/// <summary>
		/// Deletes a document by its ID.
		/// </summary>
		/// <param name="id">The ID of the document to delete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the delete response.</returns>
		public async Task<DeleteResponse> DeleteDocumentAsync(string id)
		{
			return await _client.DeleteAsync<T>(id);
		}

		/// <summary>
		/// Retrieves a document by its ID.
		/// </summary>
		/// <param name="id">The ID of the document to retrieve.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the get document response.</returns>
		public async Task<GetResponse<T>> GetDocumentAsync(string id)
		{
			return await _client.GetAsync<T>(id);
		}

		/// <summary>
		/// Updates a document in Elasticsearch.
		/// </summary>
		/// <param name="id">The ID of the document to update.</param>
		/// <param name="document">The updated document.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the update response.</returns>
		public async Task<UpdateResponse<T>> UpdateDocumentAsync(string id, T document)
		{
			return await _client.UpdateAsync<T, T>(id, u => u
				.Doc(document)
			);
		}
	}
}
