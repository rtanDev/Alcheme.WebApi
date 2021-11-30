using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Core
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
