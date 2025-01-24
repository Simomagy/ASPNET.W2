using _04_LIbreria.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// Controller per gestire i libri.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LibriController : ControllerBase
{
    private readonly DaoLibri _daoLibri = DaoLibri.GetInstance();

    /// <summary>
    /// Ottiene tutti i libri.
    /// </summary>
    /// <returns>I libri.</returns>
    [HttpGet("GetAll")]
    public IActionResult Get()
    {
        return Ok(_daoLibri.GetRecords());
    }

    /// <summary>
    /// Ottiene un libro per ID.
    /// </summary>
    /// <param name="id">L'ID del libro.</param>
    /// <returns>Il libro con l'ID specificato.</returns>
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var record = _daoLibri.FindRecord(id);
        if (record == null) return NotFound();
        return Ok(record);
    }

    /// <summary>
    /// Aggiunge un nuovo libro.
    /// </summary>
    /// <param name="libroDto">Il DTO del libro da aggiungere.</param>
    /// <returns>Il risultato dell'operazione.</returns>
    [HttpPost("add")]
    public IActionResult Post([FromBody] LibroDto libroDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var libro = CreateLibroFromDto(libroDto);
        _daoLibri.AddRecord(libro);
        return CreatedAtAction(nameof(Get), new { id = libro.Id }, libro);
    }

    /// <summary>
    /// Aggiorna un libro.
    /// </summary>
    /// <param name="id">L'ID del libro da aggiornare.</param>
    /// <param name="libroDto">Il DTO del libro con le informazioni aggiornate.</param>
    /// <returns>Il risultato dell'operazione.</returns>
    [HttpPut("update/{id:int}")]
    public IActionResult Put(int id, [FromBody] LibroDto libroDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var libro = CreateLibroFromDto(libroDto);
        libro.Id = id;
        _daoLibri.UpdateRecord(libro);
        return NoContent();
    }

    /// <summary>
    /// Elimina un libro.
    /// </summary>
    /// <param name="id">L'ID del libro da eliminare.</param>
    /// <returns>Il risultato dell'operazione.</returns>
    [HttpDelete("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        _daoLibri.DeleteRecord(id);
        return NoContent();
    }

    private Libro CreateLibroFromDto(LibroDto libroDto)
    {
        return new Libro
        {
            Titolo = libroDto.Titolo,
            Autore = libroDto.Autore,
            Genere = libroDto.Genere,
            Prezzo = libroDto.Prezzo
        };
    }
}
