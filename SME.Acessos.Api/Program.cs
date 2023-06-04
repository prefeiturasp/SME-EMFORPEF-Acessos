using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.SqlClient;
using SME.Acessos.Api;
using SME.Acessos.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var registradorDeDependencia = new RegistradorDeDependencias(builder.Services, builder.Configuration);
registradorDeDependencia.Registrar();

builder.Services.AddSingleton(registradorDeDependencia);

var app = builder.Build();

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
