using System.Text;
using System.Text.Json;
using _04_LIbreria.Models;

namespace _04_LIbreria.Services;

// Sono classi che offrono dei Servizi a chiunque ne abbia bisogno
// Solitamente si pongono a meta' strada tra le classi Dao e i controllers
// Non e' raro vedere un Servizio che svolga anche la funzione delle classi Dao
public class LibriService : ILibriService
{
    // HttpClient e' una classe che permette digestire le chiamate API e le risposte dal server (Service codes)
    private readonly HttpClient _client;

    public LibriService(HttpClient client)
    {
        _client = client;
    }

    public List<Libro> GetRecords()
    {
        // Step 1: Chiedo i dati all'API tramite la chiamata corretta
        var response = _client.GetAsync("http://localhost:5121/api/Libri/GetAll").Result;
        // Step 2: Verifico che la chiamata sia andata a buon fine code 200
        response.EnsureSuccessStatusCode();
        // Step 3: Trasformo il JSON in una stringa
        var jsonResponse = response.Content.ReadAsStringAsync().Result;
        // Step 4: Trasformo la stringa in una lista di libri
        // Possibile inconveniente: Le chiavi del JSON devono corrispondere ai nomi delle proprieta' della classe Libro
        // La soluzione e' andare nella classe modello e aggiungere gli attributi [JsonPropertyName("nomeChiaveJSON")]
        var libri = JsonSerializer.Deserialize<List<Libro>>(jsonResponse);

        // Step 5: Ritorno la lista di libri o una lista vuota in caso di errore
        return libri ?? [];
    }

    public Libro? FindRecord(int id)
    {
        var response = _client.GetAsync($"http://localhost:5121/api/Libri/{id}").Result;
        if (!response.IsSuccessStatusCode)
            return null;
        var jsonResponse = response.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<Libro>(jsonResponse);
    }

    public bool DeleteRecord(int id)
    {
        var response = _client.DeleteAsync($"http://localhost:5121/api/Libri/delete/{id}").Result;
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public bool UpdateRecord(int id, Libro libro)
    {
        var json = JsonSerializer.Serialize(libro);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = _client.PutAsync($"http://localhost:5121/api/Libri/update/{id}", content).Result;
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public bool AddRecord(Libro libro)
    {
        var json = JsonSerializer.Serialize(libro);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = _client.PostAsync("http://localhost:5121/api/Libri/add", content).Result;
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }
}
