using Document.Core;
using Microsoft.AspNetCore.Mvc;

namespace Alcheme.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentServices _documentServices;
        public DocumentController(IDocumentServices documentServices)
        {
            _documentServices = documentServices;
        }

        [HttpGet]
        public IActionResult GetDocuments()
        {
            return Ok(_documentServices.GetDocuments());
        }

        [HttpGet("{id}", Name ="GetDocument")]
        public IActionResult GetDocument(string id)
        {
            return Ok(_documentServices.GetDocument(id));
        }

        [HttpPost]
        public IActionResult AddDocument(Document.Core.Document document)
        {
            _documentServices.AddDocument(document);
            return CreatedAtRoute("GetDocument", new { id = document.Id }, document);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteDocument(string id)
        {
            _documentServices.DeleteDocument(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateDocument(Document.Core.Document document)
        {
            return Ok(_documentServices.UpdateDocument(document));
        }
    }
}
