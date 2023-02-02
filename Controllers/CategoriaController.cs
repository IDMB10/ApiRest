using APIRest.Models;
using APIRest.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase {

    private ICategoriaService _categoriaService;

    public CategoriaController(ICategoriaService categoriaService) {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(_categoriaService.Get());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Categoria categoria) {
        await _categoriaService.SaveAsync(categoria);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] Categoria categoria, [FromRoute] Guid id) {
        await _categoriaService.UpdateAsync(categoria, id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id) {
        await _categoriaService.DeleteAsync(id);
        return Ok();
    }
}