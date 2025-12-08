
namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class GrupoPermissao : EntidadeBaseCoreSSO
    {
        public int SistemaId { get; set; }
        public int ModuloId { get; set; }
        public Modulo Modulo { get; set; }
        public bool EhConsulta { get; set; }
        public bool EhInsercao { get; set; }
        public bool EhAlteracao { get; set; }
        public bool EhExclusao { get; set; }
        
        public void AdicionarModulo(Modulo modulo)
        {
            if (modulo != null)
            {
                Modulo = modulo;
                ModuloId = modulo.ModuloId;
            }
        }
    }
}
