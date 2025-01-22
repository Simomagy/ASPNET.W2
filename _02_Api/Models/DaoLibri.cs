namespace _02_.Models;

public class DaoLibri
{
    private DaoLibri() { }
    private static DaoLibri? _instance;

    public static DaoLibri GetInstance()
    {
        return _instance ??= new DaoLibri();
    }

    private List<Libro?> _list =
    [
        new Libro()
        {
            Id = 1, Titolo = "Il signore degli anelli", Autore = "J.R.R. Tolkien", Genere = "Fantasy", Prezzo = 20.50
        },

        new Libro()
        {
            Id = 2, Titolo = "Il vecchio e il mare", Autore = "Ernest Hemingway", Genere = "Romanzo", Prezzo = 15.30
        },

        new Libro()
        {
            Id = 3, Titolo = "Il nome della rosa", Autore = "Umberto Eco", Genere = "Giallo", Prezzo = 18.20
        },

        new Libro()
        {
            Id = 4, Titolo = "Il ritratto di Dorian Gray", Autore = "Oscar Wilde", Genere = "Romanzo", Prezzo = 12.40
        },

        new Libro()
        {
            Id = 5, Titolo = "Il barone rampante", Autore = "Italo Calvino", Genere = "Romanzo", Prezzo = 14.90
        }
    ];

    public List<Libro?> GetRecords()
    {
        return _list;
    }

    public Libro? FindRecord(int id)
    {
        return _list.FirstOrDefault(x => x != null && x.Id == id);
    }

    public void AddRecord(Libro? libro)
    {
        if (libro == null) return;
        _list.Add(libro);
    }

    public void UpdateRecord(Libro libro)
    {
        var record = _list.FirstOrDefault(x => x != null && x.Id == libro.Id);
        if (record == null) return;
        record.Titolo = libro.Titolo;
        record.Autore = libro.Autore;
        record.Genere = libro.Genere;
        record.Prezzo = libro.Prezzo;
    }

    public void DeleteRecord(int id)
    {
        var record = _list.FirstOrDefault(x => x != null && x.Id == id);
        if (record == null) return;
        _list.Remove(record);
    }
}
