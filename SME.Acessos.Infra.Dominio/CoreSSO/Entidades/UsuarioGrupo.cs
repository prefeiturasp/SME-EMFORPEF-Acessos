
namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class UsuarioGrupo : EntidadeBaseCoreSSO
    {
        public Usuario Usuario { get; set; }
        public Guid GrupoId { get; set; }
        public Grupo Grupo { get; set; }
        
        public void AdicionarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                Usuario = usuario;
                Id = usuario.Id;
            }
        }
        public void AdicionarGrupo(Grupo grupo)
        {
            if (grupo != null)
            {
                Grupo = grupo;
                GrupoId = grupo.Id;
            }
        }
    }
}
