
using Alcheme.Data.Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Alcheme.Data.Db
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Document> _document;
        public DbClient(IOptions<AlchemeDbConfig> alchemeDbConfig)
        {
            var client = new MongoClient(alchemeDbConfig.Value.ConnectionString);
            var database = client.GetDatabase(alchemeDbConfig.Value.DatabaseName);
            _document = database.GetCollection<Document>(alchemeDbConfig.Value.DocumentsCollectionName);
        }
        public IMongoCollection<Document> GetDocumentCollection() => _document;
    }
}
