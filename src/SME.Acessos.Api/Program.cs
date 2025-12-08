using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.SqlClient;
using SME.Acessos.Api.Middlewares;
using SME.Acessos.Api.SwaggerOperationFilters;
using SME.Acessos.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
    c.OperationFilter<AdicionaCabecalhoHttp>());

var registradorDeDependencia = new RegistradorDeDependencias(builder.Services, builder.Configuration);
registradorDeDependencia.Registrar();

builder.Services.AddSingleton(registradorDeDependencia);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

app.UseElasticApm(builder.Configuration,
    new SqlClientDiagnosticSubscriber(),
    new HttpDiagnosticsSubscriber());

app.UseTratamentoExcecoesGlobalMiddleware();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); 

app.Run();
