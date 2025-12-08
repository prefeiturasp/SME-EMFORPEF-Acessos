namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

public class Grupo : EntidadeBaseCoreSSO
{
    public string Nome { get; set; }
    public int Situacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
    public int VisaoId { get; set; }
    public Visao Visao { get; set; }
    public int SistemaId { get; set; }
    public Sistema Sistema { get; set; }
    public int Integridade { get; set; }
    
    public void AdicionarVisao(Visao visao)
    {
        if (visao != null)
        {
            Visao = visao;
            VisaoId = visao.VisaoId;
        }
    }
    
    public void AdicionarSistema(Sistema sistema)
    {
        if (sistema != null)
        {
            Sistema = sistema;
            SistemaId = sistema.SistemaId;
        }
    }
}