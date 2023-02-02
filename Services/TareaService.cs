using APIRest.Models;

namespace APIRest.Services;

public class TareaService : ITareaService {
    private TareasContext _context;

    public TareaService(TareasContext context) {
        _context = context;
    }

    public IEnumerable<Tarea> Get() {
        return _context.Tareas ?? Enumerable.Empty<Tarea>();
    }

    public async Task SaveAsync(Tarea tarea) {
        if (_context.Tareas == null) {
            return;
        }
        await _context.Tareas.AddAsync(tarea);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tarea tareaNueva, Guid id) {

        if (_context.Tareas == null) {
            return;
        }
        Tarea? tareaActual;
        tareaActual = await _context.Tareas.FindAsync(id);

        if (tareaActual != null) {
            tareaActual.Categoria = tareaNueva.Categoria;
            tareaActual.Descripcion = tareaNueva.Descripcion;
            tareaActual.FechaCreacion = tareaNueva.FechaCreacion;
            tareaActual.PriodidadTarea = tareaNueva.PriodidadTarea;
            tareaActual.Titulo = tareaNueva.Titulo;
            tareaActual.Resumen = tareaNueva.Resumen;

            await _context.SaveChangesAsync();
        }
    }
    public async Task DeleteAsync(Guid id) {
        Tarea? tareaEliminar;

        if (_context.Tareas == null) {
            return;
        }
        tareaEliminar = await _context.Tareas.FindAsync(id);
        if (tareaEliminar == null) {
            return;
        }
        _context.Tareas.Remove(tareaEliminar);
        await _context.SaveChangesAsync();
    }
}

public interface ITareaService {

    IEnumerable<Tarea> Get();
    Task SaveAsync(Tarea tarea);
    Task UpdateAsync(Tarea tareaNueva, Guid id);
    Task DeleteAsync(Guid id);
}