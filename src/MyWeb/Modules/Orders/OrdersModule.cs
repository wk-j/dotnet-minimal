namespace MyWeb.Modules.Orders;

public class OrderConfig
{
    public string ConnectionString { set; get; } = "db";
}

public class OrdersModule : IModule
{

    string Order()
    {
        return "Hello, world";
    }

    public WebApplication MapEndpoints(WebApplication endpoints)
    {
        endpoints.MapGet("/order/orders", Order)
        .WithTags("order-module");

        return endpoints;
    }

    public IServiceCollection RegisterModules(IServiceCollection builder)
    {
        builder.AddSingleton(new OrderConfig());
        return builder;
    }
}