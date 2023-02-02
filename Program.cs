using APIRest;
using APIRest.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregando servicio. Inyectando la dependencia 
//Con builder.Services.addScope se genera nueva instancia de la dependencia a nivel de controlador.
//Con builder.Services.AddSingleton se crea una unica instancia a nivel de toda la WebApi. No recondable
//builder.Services.AddScoped<IHelloWorldService, HelloWorldService>(); //Cada vez que se inyecte la interfaz IHelloWorldService creara un nuevo objeto de HelloWorldService internamente.
//builder.Services.AddScoped(p => new HelloWorldService()); //Inyección la dependencia usando la clase directamente (Aunque se tiene que castear porque el constructor del controlador tiene como parametro el IHelloWorldService)

builder.Services.AddScoped<IHelloWorldService>(p => new HelloWorldService()); //Inyección la dependencia usando la clase directamente pero usando el constructor que tiene como parametro la interfaz. Se pueden pasar parametros a la clase si se desea.

//Agregando el servicio de base de datos en memoria.
//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB")); 

//Agregando el servicio para conectarse a la db en SQLServer.
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareasWA"));

//Inyectando los servicios.
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITareaService, TareaService>();


var app = builder.Build();

//Todas las sentencias que tienen app.use... son middelwares
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseWelcomePage(); 

app.UseTimeMiddelWare();  //Llamando el middleware personalizado
//Para probarlo se usa http://localhost:5184?time, retorna la hora

app.MapControllers();

app.Run();
