using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alcheme.WebApi.Contracts
{
    public class ApiRoutes
    {
        private const string Base = "api";

        public class Documents
        {
            public const string GetDocuments = Base + "/documents";
            public const string GetDocument = GetDocuments + "/{id}";
            public const string DeleteDocument = GetDocuments + "/{id}";

            public const string AddDocument = GetDocuments;
            public const string UpdateDocument = GetDocuments;
        }
    }
}
