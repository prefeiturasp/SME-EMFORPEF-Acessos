namespace SME.Acessos.Infra.Polly;

public abstract class PoliticaPolly
{
    public const string HTTP = "RetentativaHttp";
    public const string PublicaFila = "RetentativaRabbit";
}
