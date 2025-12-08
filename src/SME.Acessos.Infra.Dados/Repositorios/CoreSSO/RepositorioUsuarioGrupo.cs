using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioUsuarioGrupo(IConexaoCoreSSO conexao) : RepositorioBaseCoreSSO<UsuarioGrupo>(conexao), IRepositorioUsuarioGrupo
    {
        public async Task<bool> InserirUsuarioGrupoCustomizado(Guid usuarioId, Guid grupoId)
        {
            var insertUsuarioGrupo = $@"insert into [sys_usuariogrupo] ([usu_id],[gru_id],[usg_situacao]) values ('{usuarioId}','{grupoId}',1);";

            await conexao.Obter().ExecuteAsync(insertUsuarioGrupo);
            return true;
        }

        public async Task<bool> DeletarUsuarioGrupoCustomizado(Guid usuarioId, Guid grupoId)
        {
            var deletarUsuarioGrupo = $@"delete from [sys_usuariogrupo] where [usu_id] = @usuarioId and [gru_id] = @grupoId";

            await conexao.Obter().ExecuteAsync(deletarUsuarioGrupo, new { usuarioId, grupoId });
            return true;
        }

        public async Task<bool> AtivarVinculo(Guid usuarioId, Guid grupoId)
        {
            var situacaoAtivo = 1;
            var updateUsuarioGrupo = "update [sys_usuariogrupo] set [usg_situacao] = @situacaoAtivo where [usu_id] = @usuarioId and [gru_id] = @grupoId ";

            await conexao.Obter().ExecuteAsync(updateUsuarioGrupo, new { usuarioId, grupoId, situacaoAtivo });

            return true;
        }

        public Task<bool> PerfilJaVinculado(Guid usuarioId, Guid grupoId)
        {
            var query = $@"select top 1 count(1) from [sys_usuariogrupo] where [usu_id] = @usuarioId and [gru_id] = @grupoId";

            return conexao.Obter().ExecuteScalarAsync<bool>(query, new { usuarioId, grupoId });
        }
    }
}
