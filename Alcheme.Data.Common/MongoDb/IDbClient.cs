using Alcheme.Data.Common;
using Alcheme.Data.Common.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcheme.Data.Common.MongoDb
{
    public interface IDbClient
    {
        IMongoCollection<Document> GetDocumentCollection();
    }
}
