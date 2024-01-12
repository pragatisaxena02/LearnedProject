using MassTransit;
using Microsoft.Extensions.Options;
using Shared.Class;
using Weather.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection("MessageBroker"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

//builder.Services.AddMassTransit(busConfigurator =>
//{
//    busConfigurator.SetKebabCaseEndpointNameFormatter();
//    busConfigurator.AddConsumer<NotifyTriggerConsumer>();
//    busConfigurator.UsingRabbitMq((context, configurator) =>
//    {
//        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
//        configurator.Host(new Uri(settings.Host), h =>
//        {
//            h.Username(settings.Username);
//            h.Password(settings.Password);
//        });
//    });
//});

builder.Services.AddMassTransit(x =>
{
   // x.AddConsumer<NotifyTriggerConsumer>();   

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});
//builder.Services.AddMassTransitHostedService();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
