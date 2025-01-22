using _02_.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace _02_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibriController : ControllerBase
    {
        private readonly DaoLibri _daoLibri = DaoLibri.GetInstance();

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve all books", Description = "Gets a list of all books in the library.")]
        public IActionResult Get()
        {
            return Ok(_daoLibri.GetRecords());
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Retrieve a book by ID", Description = "Gets the details of a specific book by its ID.")]
        public IActionResult Get(int id)
        {
            var record = _daoLibri.FindRecord(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        [HttpPost("add")]
        [SwaggerOperation(Summary = "Add a new book", Description = "Adds a new book to the library.")]
        public IActionResult Post([FromQuery] LibroDto libroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var libro = new Libro
            {
                Id = libroDto.Id,
                Titolo = libroDto.Titolo,
                Autore = libroDto.Autore,
                Genere = libroDto.Genere,
                Prezzo = libroDto.Prezzo
            };
            _daoLibri.AddRecord(libro);
            return Ok();
        }

        [HttpPut("update")]
        [SwaggerOperation(Summary = "Update a book", Description = "Updates the details of an existing book.")]
        public IActionResult Put([FromQuery] LibroDto libroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var libro = new Libro
            {
                Id = libroDto.Id,
                Titolo = libroDto.Titolo,
                Autore = libroDto.Autore,
                Genere = libroDto.Genere,
                Prezzo = libroDto.Prezzo
            };
            _daoLibri.UpdateRecord(libro);
            return Ok();
        }

        [HttpDelete("delete/{id:int}")]
        [SwaggerOperation(Summary = "Delete a book", Description = "Deletes a book from the library.")]
        public IActionResult Delete(int id)
        {
            _daoLibri.DeleteRecord(id);
            return Ok();
        }
    }
}
