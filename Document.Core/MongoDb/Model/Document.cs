using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Alcheme.Data.Db
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstname")]
        public string FirstName { get; set; }

        [BsonElement("lastname")]
        public string LastName { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("startpage")]
        public int StartPage { get; set; }

        [BsonElement("endpage")]
        public int EndPage { get; set; }

        [BsonElement("filename")]
        public string Filename { get; set; }
    }
}
