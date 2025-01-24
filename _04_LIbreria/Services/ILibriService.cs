using _04_LIbreria.Models;

namespace _04_LIbreria.Services;

// Il compito di questa interfaccia e' solo quello di proteggere il Service collegato
// Quindi in questa interfaccia troveremo solo firme di metodi
public interface ILibriService
{
    List<Libro> GetRecords();
    Libro? FindRecord(int id);
    bool DeleteRecord(int id);
    bool UpdateRecord(int id, Libro libro);
    bool AddRecord(Libro libro);
}
