using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HelloWorldController : ControllerBase {

    private ILogger<HelloWorldController> _logger;

    private IHelloWorldService _helloWorldService;
    //HelloWorldService _helloWorldClass;

    //ctor para cuando se inyecta AddScoped<IHelloWorldService, HelloWorldService>() o AddScoped<IHelloWorldService>(p => new HelloWorldService())
    public HelloWorldController(IHelloWorldService helloWorldService, ILogger<HelloWorldController> logger) {
        _helloWorldService = helloWorldService;
        _logger = logger;
    }

    // //Ctor para cuando se inyecta la clase directamente sin castear con addScope(p => new HelloWorldService())
    // public HelloWorldController(HelloWorldService helloWorldClass)
    // {
    //     _helloWorldClass = helloWorldClass;
    // }

    [HttpGet]
    public IActionResult Get() {
        _logger.LogInformation("Retornando mensaje de saludo");
        return Ok(_helloWorldService.GetHelloWorld());
    }
}