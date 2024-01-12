//using MassTransit;
//using Microsoft.Extensions.Options;
//using Notify.Events;
//using Shared.Class;
//using Shared.EventBus;

using MassTransit;
using MassTransit.Caching;
using Microsoft.Extensions.Options;
using Notify.Services;
using Shared.Class;
using Shared.StateMachine;
//using Weather.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection("MessageBroker"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddSagaStateMachine<NotifyStateMachine, NotifyState>()
       .InMemoryRepository();
    //busConfigurator.AddConsumers(typeof(Program).Assembly);
    //busConfigurator.AddConsumer<NotifyTriggerConsumer>();

    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });
        configurator.ReceiveEndpoint(ec =>
        {
            ec.ConfigureSagas(context);

        });

    });
});



builder.Services.AddTransient<INotify, Notify.Services.Notify>();
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
