using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

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
				.DefaultIndex(indexName);

			_client = new ElasticsearchClient(settings);
		}

		/// <summary>
		/// Indexes a document in Elasticsearch.
		/// </summary>
		/// <param name="document">The document to index.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the index response.</returns>
		public async Task<IndexResponse> IndexDocumentAsync(T document)
		{
			return await _client.IndexAsync(document);
		}

		/// <summary>
		/// Searches for documents based on a query.
		/// </summary>
		/// <param name="query">The query string for searching.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the search response.</returns>
		public async Task<SearchResponse<T>> SearchAsync(string query)
		{
			var searchResponse = await _client.SearchAsync<T>(s => s
				.Query(q => q
					.QueryString(d => d
						.Query(query)
					)
				)
			);
			return searchResponse;
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
