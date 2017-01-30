using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Options;

namespace DataAccessLayer.Contract
{
    public class Repository<TObject> : IRepository<TObject> where TObject : class
    {
        private static string DatabaseName = string.Empty;
        private static string EndPoint = string.Empty;
        private static string AuthKey = string.Empty;
        protected string CollectionName = string.Empty;

        private static DocumentClient client = null;

        private static bool isDBExists = false;
        private static bool isCollectionExists = false;

        public Repository(IOptions<ConfigurationSettings> settings)
        {
            DatabaseName = settings.Value.DatabaseName;
            EndPoint = settings.Value.EndPoint;
            AuthKey = settings.Value.AuthKey;
            CollectionName = settings.Value.CollectionName;

            client = new DocumentClient(new Uri(EndPoint), AuthKey);
            if (!isDBExists)
               CreateDBIfNotExist();
            if (!isCollectionExists)
                CreateCollectionIfNotExist();
        }


        public async Task<TObject> Create(TObject item)
        {
            Document document = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), item);
            return (TObject)(dynamic)document;
        }

        public virtual async Task<bool> Delete(string id)
        {
            try
            {
                Document document = await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, CollectionName, id));
                return document == null;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (client != null)
                client = null;
        }

        public async Task<List<TObject>> GetAll()
        {
            var results = new List<TObject>();

            var query = client.CreateDocumentQuery<TObject>(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName)).AsDocumentQuery();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<TObject>());
            }
            return results;
        }

        public virtual async Task<TObject> Update(TObject item)
        {
            Document updatedDocument = await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), item);
            Document result = await client.ReadDocumentAsync(updatedDocument.SelfLink, null);
            return (TObject)(dynamic)updatedDocument;
        }

        private async Task CreateDBIfNotExist()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseName));
                isDBExists = true;
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseName });
                else
                    throw ex;
            }
        }

        private async Task CreateCollectionIfNotExist()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName));
                isCollectionExists = true;
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    await client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(DatabaseName), new DocumentCollection { Id = CollectionName });
                else
                    throw ex;
            }
        }

        public async Task<TObject> Find(string id)
        {
            TObject results = null;
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, CollectionName, id));
                if (null != document)
                {
                    results = (TObject)(dynamic)document;
                }
                return results;
            }
            catch
            {
                return null;
            }
        }
    }
}