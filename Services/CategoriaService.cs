using APIRest.Models;

namespace APIRest.Services;

public class CategoriaService : ICategoriaService {

    TareasContext _context;

    public CategoriaService(TareasContext context) {
        _context = context;
    }
    public IEnumerable<Categoria> Get() {

        return _context.Categorias ?? Enumerable.Empty<Categoria>();
    }

    public async Task SaveAsync(Categoria categoria) {

        if (_context.Categorias == null) {
            return;
        }

        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Categoria categoria, Guid id) {

        if (_context.Categorias == null) {
            return;
        }

        var categoriaActual = await _context.Categorias.FindAsync(id);

        if (categoriaActual != null) {
            categoriaActual.Nombre = categoria.Nombre;
            categoriaActual.Descripcion = categoria.Descripcion;
            categoriaActual.Peso = categoria.Peso;
            categoriaActual.Tareas = categoria.Tareas;
            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteAsync(Guid id) {

        if (_context.Categorias == null) {
            return;
        }

        var categoriaActual = await _context.Categorias.FindAsync(id);

        if (categoriaActual != null) {
            _context.Categorias.Remove(categoriaActual);
            await _context.SaveChangesAsync();
        }
    }
}

public interface ICategoriaService {
    IEnumerable<Categoria> Get();
    Task SaveAsync(Categoria categoria);
    Task UpdateAsync(Categoria categoria, Guid id);
    Task DeleteAsync(Guid id);
}