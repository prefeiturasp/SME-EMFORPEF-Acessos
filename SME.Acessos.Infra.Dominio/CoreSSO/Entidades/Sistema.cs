namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

public class Sistema : EntidadeBaseCoreSSO
{
    public int SistemaId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Caminho { get; set; }
    public string UrlImagem { get; set; }
    public string UrlLogoCabecalho { get; set; }
    public int TipoAutenticacao { get; set; }
    public string UrlIntegracao { get; set; }
    public int Situacao { get; set; }
    public string CaminhoLogout { get; set; }
    public int OcultaLogo { get; set; }
}