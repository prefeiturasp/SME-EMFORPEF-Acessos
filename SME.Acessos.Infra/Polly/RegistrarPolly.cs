using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;

namespace SME.Acessos.Infra.Polly
{
    public static class RegistrarPolly
    {
        public static void ConfigurarPolly(this IServiceCollection services)
        {
            IPolicyRegistry<string> registry = services.AddPolicyRegistry();

            var random = new Random();
            var politica = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                    + TimeSpan.FromMilliseconds(random.Next(0, 30)));

            registry.Add(PoliticaPolly.HTTP, politica);

            Random jitterer = new();
            var policyFila = Policy.Handle<Exception>()
              .WaitAndRetryAsync(3,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                      + TimeSpan.FromMilliseconds(jitterer.Next(0, 30)));

            registry.Add(PoliticaPolly.PublicaFila, policyFila);

        }
    }
}
