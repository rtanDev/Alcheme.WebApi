using Alcheme.Data.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcheme.Data.Common.Interfaces
{
    public interface IDocumentServices
    {
        List<Document> GetDocuments();
        Document GetDocument(string id);
        Document AddDocument(Document document);
        void DeleteDocument(string id);

        Document UpdateDocument(Document document);
    }
}
