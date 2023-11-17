using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Language;
using Microsoft.EntityFrameworkCore;
using Reservations.API;
using Reservations.API.GraphQL;
using Reservations.API.InputTypes;
using Reservations.API.Services;
using Reservations.API.Types;
using ProviderType = Reservations.API.GraphQL.Types.ObjectTypes.ProviderType;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPooledDbContextFactory<ReservationDbContext>(options => options.UseInMemoryDatabase("Reservations"));

builder.Services
    .AddSingleton<UserRepository>();

builder.Services.AddTransient<IProviderService, ProviderService>();
builder.Services.AddScoped<Query>();

builder.Services
    .AddGraphQLServer()
    .AddApolloFederation()
    .RegisterDbContext<ReservationDbContext>(DbContextKind.Pooled)
    .RegisterService<UserRepository>()
    .RegisterService<ProviderService>()
    .AddQueryType<QueryType>()
    .AddMutationType<MutationType>()
    .AddType<ProviderType>()
    .AddType<CreateAppointmentSlotsInputType>()
    .AddType<AppointmentStatusType>()
    .AddDiagnosticEventListener<Log>();

var app = builder.Build();
using (var scope = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IServiceScopeFactory>()
       .CreateScope())
{
    var services = scope.ServiceProvider;

    DataSeeder.Initialize(services);
}

app.MapGraphQL();
app.Run();

public class Log : ExecutionDiagnosticEventListener
{
    public override IDisposable ExecuteRequest(IRequestContext context)
    {
        Console.WriteLine(context.Request.Query?.ToString());

        if (context.Request.VariableValues is not null)
            foreach (var variable in context.Request.VariableValues)
                Console.WriteLine($"{variable.Key}: {((IValueNode)variable.Value!).ToString()}");

        return base.ExecuteRequest(context);
    }
}