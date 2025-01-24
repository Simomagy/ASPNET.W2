using System.Text.Json.Serialization;

namespace _04_LIbreria.Models;

public class Libro
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("titolo")]
    public string Titolo { get; set; } = string.Empty;
    [JsonPropertyName("autore")]
    public string Autore { get; set; } = string.Empty;
    [JsonPropertyName("genere")]
    public string Genere { get; set; } = string.Empty;
    [JsonPropertyName("prezzo")]
    public double Prezzo { get; set; }
}
