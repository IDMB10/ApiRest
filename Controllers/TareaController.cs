using APIRest.Models;
using APIRest.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class TareaController : ControllerBase {
    private ITareaService _tareasService;
    public TareaController(ITareaService tareaService) {
        _tareasService = tareaService;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(_tareasService.Get());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Tarea tarea) {
        await _tareasService.SaveAsync(tarea);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] Tarea tarea, Guid id) {
        await _tareasService.UpdateAsync(tarea, id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) {
        await _tareasService.DeleteAsync(id);
        return Ok();
    }
}