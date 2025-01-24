using _04_LIbreria.Models;
using _04_LIbreria.Services;
using Microsoft.AspNetCore.Mvc;

namespace _04_LIbreria.Controllers;

public class MvcLibriController : Controller
{
    // Campo statico che mi permette di sfruttare il Service
    private readonly ILibriService _libriService;

    public MvcLibriController(ILibriService libriService)
    {
        _libriService = libriService;
    }
    // GET
    public IActionResult Index()
    {
        // Compiti del Service:
        //1 Chiamare il metodo GET del controller API
        //2 Prendere il risultato del metodo GET (che e' JSON)
        //3 Trasformare il JSON in una lista di libri [List<Libro>]
        // Questi passaggi vengono delocalizzati e fatti svolgere ai Service

        // Compiti dell'MVC Cnontroller:
        //1 Passare la lista in arrivo dal Service alla View
        var libri = _libriService.GetRecords();

        // L'MVC Controller prendera' i dati in arrivo dal Service e passarli alla View (I dati saranno gia' pronti)
        return View(libri);
    }

    public IActionResult Details(int id)
    {
        var libro = _libriService.FindRecord(id);
        if (libro == null) return NotFound();
        return View(libro);
    }

    public IActionResult Delete(int id)
    {
        if (_libriService.DeleteRecord(id))
            return RedirectToAction(nameof(Index));
        return NotFound();
    }

    public IActionResult EditForm(int id)
    {
        var libro = _libriService.FindRecord(id);
        if (libro == null) return NotFound();
        return View(libro);
    }

    public IActionResult Edit(Dictionary<string, string> parameters)
    {
        var libro = new Libro
        {
            Id = int.Parse(parameters["id"]),
            Titolo = parameters["titolo"],
            Autore = parameters["autore"],
            Genere = parameters["genere"],
            Prezzo = double.Parse(parameters["prezzo"])
        };
        _libriService.UpdateRecord(int.Parse(parameters["id"]), libro);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult AddForm()
    {
        return View();
    }

    public IActionResult Add(Dictionary<string, string> parameters)
    {
        var libro = new Libro
        {
            Titolo = parameters["titolo"],
            Autore = parameters["autore"],
            Genere = parameters["genere"],
            Prezzo = double.Parse(parameters["prezzo"])
        };
        _libriService.AddRecord(libro);
        return RedirectToAction(nameof(Index));
    }
}
