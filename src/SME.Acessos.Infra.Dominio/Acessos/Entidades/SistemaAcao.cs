using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dominio.Acessos.Entidades
{
    public class SistemaAcao : EntidadeBaseAcessos
    {
        public string NomeSistema { get; set; }
        public long CodigoSistema { get; set; }
        public string Endereco { get; set; }
        public TipoAcao TipoAcao { get; set; }
    }
}
