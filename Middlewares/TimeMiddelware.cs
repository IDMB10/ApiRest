public class TimeMiddelware {
    private readonly RequestDelegate _next;

    public TimeMiddelware(RequestDelegate nextRequest) {
        _next = nextRequest;
    }

    public async Task InvokeAsync(HttpContext context) {
        //si el request trae el parametro con nombre "time" no importa el endpoint retornará la fecha junto a la respuesta
        if (context.Request.Query.Any(p => p.Key == "time")) {
            await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
            return; //Salir para evitar ejectar el middleware 2 veces.
        }

        await _next(context); //Invoca al siguiente middelware.
    }
}

public static class TimeMiddelwareExtension {
    public static IApplicationBuilder UseTimeMiddelWare(this IApplicationBuilder builder) {
        return builder.UseMiddleware<TimeMiddelware>();
    }
}