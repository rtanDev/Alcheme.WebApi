using Alcheme.Data.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcheme.Data.Db
{
    public interface IDbClient
    {
        IMongoCollection<Document> GetDocumentCollection();
    }
}
