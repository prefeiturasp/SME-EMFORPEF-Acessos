using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioUsuarioGrupo : RepositorioBaseCoreSSO<UsuarioGrupo>, IRepositorioUsuarioGrupo
    {
        public RepositorioUsuarioGrupo(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<bool> InserirUsuarioGrupoCustomizado(Guid usuarioId, Guid grupoId)
        {
            var insertUsuarioGrupo = $@"insert into [sys_usuariogrupo] ([usu_id],[gru_id],[usg_situacao]) values ('{usuarioId}','{grupoId}',1);";

            await conexao.Obter().ExecuteScalarAsync(insertUsuarioGrupo);
            return true;
        }

        public async Task<bool> DeletarUsuarioGrupoCustomizado(Guid usuarioId, Guid grupoId)
        {
            var deletarUsuarioGrupo = $@"delete from [sys_usuariogrupo] where [usu_id] = @usuarioId and [gru_id] = @grupoId";

            await conexao.Obter().ExecuteScalarAsync(deletarUsuarioGrupo, new { usuarioId, grupoId });
            return true;
        }
    }
}
