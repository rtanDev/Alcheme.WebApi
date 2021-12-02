using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcheme.Data.Common.Model
{
    public class BSON 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        protected string id { get; set; }
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

    }
}
