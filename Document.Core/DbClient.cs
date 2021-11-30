
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Document.Core
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Document> _document;
        public DbClient(IOptions<AlchemeDbConfig> alchemeDbConfig)
        {
            var client = new MongoClient(alchemeDbConfig.Value.Connection_String);
            var database = client.GetDatabase(alchemeDbConfig.Value.Database_Name);
            _document = database.GetCollection<Document>(alchemeDbConfig.Value.Documents_Collection_Name);
        }
        public IMongoCollection<Document> GetDocumentCollection() => _document;
    }
}
