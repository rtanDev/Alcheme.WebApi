using Alcheme.Data.Common.Interfaces;
using Alcheme.Data.Common.Model;
using Alcheme.Data.Common.MongoDb;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Alcheme.Data.Common.Services
{
    public class DocumentServices : IDocumentServices
    {
        private readonly IMongoCollection<Document> _document;
        private readonly ILogger<DocumentServices> _logger;

        public DocumentServices(
            IDbClient dbClient,
            ILogger<DocumentServices> logger
            )
        {
            _document = dbClient.GetDocumentCollection();
            _logger = logger;
        }

        public Document AddDocument(Document document)
        {
            _document.InsertOne(document);
            return document;
        }

        public void DeleteDocument(string id) => _document.DeleteOne(document => document.Id == id);

        public List<Document> GetDocuments() => _document.Find(document => true).ToList();

        public Document GetDocument(string id)
        {
            var result = _document.Find(document => document.Id == id).FirstOrDefault();
            _logger.LogInformation($"{result}");

            return result;
        }

        public Document UpdateDocument(Document document)
        {
            GetDocument(document.Id);
            _document.ReplaceOne(doc => doc.Id == document.Id, document);
            return document;
        }
    }
}
