using Microsoft.AspNetCore.Mvc;

namespace MyWeb.Modules.Hello;

public class HelloService
{
    public string SayHello(string name)
    {
        return $"Hello {name}";
    }

    public string GetContent(string content)
    {
        return $"Content-Type: {content}";
    }
}

public class HelloModule : IModule
{
    string SayHello(string name, [FromServices] HelloService service)
    {
        return service.SayHello(name);
    }

    string GetContent([FromHeader(Name = "content-type")] string contentType, HelloService service)
    {
        return service.GetContent(contentType);
    }

    public WebApplication MapEndpoints(WebApplication endpoints)
    {
        endpoints.MapGet("/hello/say-hello", SayHello)
            .WithTags("hello-module");
        endpoints.MapGet("/hello/get-content-type", GetContent)
            .WithTags("hello-module");

        return endpoints;
    }

    public IServiceCollection RegisterModules(IServiceCollection builder)
    {
        builder.AddScoped<HelloService>();
        return builder;
    }
}