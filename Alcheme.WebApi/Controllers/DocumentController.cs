using Alcheme.Data.Common;
using Alcheme.Data.Common.Interfaces;
using Alcheme.Data.Common.Model;
using Alcheme.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Alcheme.WebApi.Controllers
{

    [ApiController]
    [ApiVersion("2021.3")]

    public class DocumentController : ControllerBase
    {
        private readonly IDocumentServices _documentServices;
        public DocumentController(IDocumentServices documentServices)
        {
            _documentServices = documentServices;
        }

        [HttpGet(ApiRoutes.Documents.GetDocuments)]
        public IActionResult GetDocuments()
        {
            return Ok(_documentServices.GetDocuments());
        }

        [HttpGet(ApiRoutes.Documents.GetDocument, Name="GetDocument")]
        public IActionResult GetDocument(string id)
        {
            return Ok(_documentServices.GetDocument(id));
        }

        [HttpPost(ApiRoutes.Documents.AddDocument)]
        public IActionResult AddDocument(Document document)
        {
            _documentServices.AddDocument(document);
            return CreatedAtRoute("GetDocument", new { id = document.Id }, document);
        }


        [HttpDelete(ApiRoutes.Documents.DeleteDocument)]
        public IActionResult DeleteDocument(string id)
        {
            _documentServices.DeleteDocument(id);
            return NoContent();
        }

        [HttpPut(ApiRoutes.Documents.UpdateDocument)]
        public IActionResult UpdateDocument(Document document)
        {
            return Ok(_documentServices.UpdateDocument(document));
        }
    }
}
