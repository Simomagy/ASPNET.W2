namespace _02_.Models;

public class Libro
{
    public int Id { get; set; }
    public string Titolo { get; set; } = string.Empty;
    public string Autore { get; set; } = string.Empty;
    public string Genere { get; set; } = string.Empty;
    public double Prezzo { get; set; }
}
