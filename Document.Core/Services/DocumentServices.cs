using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Alcheme.Data.Common
{
    public class DocumentServices : IDocumentServices
    {
        private readonly IMongoCollection<Document> _document;
        public DocumentServices(IDbClient dbClient)
        {
            _document = dbClient.GetDocumentCollection();
        }

        public Document AddDocument(Document document)
        {
            _document.InsertOne(document);
            return document;
        }

        public void DeleteDocument(string id) => _document.DeleteOne(document => document.Id == id);

        public List<Document> GetDocuments() => _document.Find(document => true).ToList();

        public Document GetDocument(string id) => _document.Find(document => document.Id == id).FirstOrDefault();

        public Document UpdateDocument(Document document)
        {
            GetDocument(document.Id);
            _document.ReplaceOne(doc => doc.Id == document.Id, document);
            return document;
        }
    }
}
