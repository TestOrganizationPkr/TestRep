using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Repository<TObject> : IRepository<TObject>
       where TObject : class
    {
        private static readonly string DatabaseName = ConfigurationManager.AppSettings["database"];
        private static readonly string EndPoint = ConfigurationManager.AppSettings["endpoint"];
        private static readonly string AuthKey = ConfigurationManager.AppSettings["authKey"];

        protected string CollectionName = string.Empty;
        private static DocumentClient client;

        /// <summary>
        /// Method to initialize Document DB Connection
        /// </summary>
        public static void Initialize()
        {
            client = new DocumentClient(new Uri(EndPoint), AuthKey);
        }

        /// <summary>
        /// Method to fetch all the records from the DB Collection
        /// </summary>
        /// <param name="options"></param>
        /// <returns>List of Documents from specific collection</returns>
        public virtual async Task<ICollection<TObject>> All()
        {
            Initialize();
            IDocumentQuery<TObject> query = client.CreateDocumentQuery<TObject>(
                UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName))
                .AsDocumentQuery();
            List<TObject> results = new List<TObject>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<TObject>());

            }
            return results;
        }

        /// <summary>
        /// This is to get document by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Document specific to the 'ID'</returns>
        public virtual async Task<TObject> GetById(string id)
        {
            Initialize();
            Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, CollectionName, id));
            TObject results = null;
            if (null != document)
            {
                results = (TObject)(dynamic)document;
            }
            return results;
        }

        /// <summary>
        /// This is to create a new document
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Document which is created</returns>
        public virtual async Task<TObject> Create(TObject item)
        {
            Initialize();
            Document document = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), item);
            var results = (TObject)(dynamic)document;
            return results;
        }

        /// <summary>
        /// Method to delete the document from the collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if the document is deleted successfully</returns>
        public virtual async Task<bool> Delete(string id)
        {
            Initialize();
            Document document = await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, CollectionName, id));
            return document == null;
        }

        /// <summary>
        /// Method to Replace/Update the document in DocumentDB collection
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TObject"></param>
        /// <returns>Returns Updated document</returns>
        public virtual async Task<TObject> Update(string id, TObject item)
        {
            Initialize();
            Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, CollectionName, id));
            Document updatedDoc = await client.ReplaceDocumentAsync(document.SelfLink, item, null);
            return (TObject)(dynamic)updatedDoc;
        }

        /// <summary>
        /// Method to dispose unused objects
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Method to dispose client object
        /// </summary>
        /// <param name="check"></param>
        protected virtual void Dispose(bool check)
        {
            if (client != null)
                client = null;
        }
    }
}
