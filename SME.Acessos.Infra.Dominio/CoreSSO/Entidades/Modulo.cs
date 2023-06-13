
namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class Modulo : EntidadeBaseCoreSSO
    {
        public int SistemaId { get; set; }
        public int ModuloId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? IdPai { get; set; }
        public int Auditoria { get; set; }
        public int Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
