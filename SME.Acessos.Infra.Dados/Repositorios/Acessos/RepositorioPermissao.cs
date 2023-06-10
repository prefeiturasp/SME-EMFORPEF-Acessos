using System.Text;
using Dapper;
using SME.Acessos.Infra.Dominio;
using SME.Acessos.Infra.Dominio.Acessos;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dados.Acessos;

public class RepositorioPermissao : RepositorioQuery, IRepositorioPermissao
{
    public RepositorioPermissao(IConexaoAcessos conexao) : base(conexao)
    {
    }

    public async Task<IEnumerable<int>> ObterPermissoesPorModulos(IList<ModuloGrupoPermissao> modulosGrupoPermissao)
    {
        var query = new StringBuilder();
        
        query.AppendLine(@"select id as Permissao from modulos where 1=1 ");
        
        var acoes = new List<int>();

        foreach (var acesso in modulosGrupoPermissao)
        {
            if (acesso.EhAlteracao)
                acoes.Add((int)TipoPermissao.Alteracao);

            if (acesso.EhConsulta)
                acoes.Add((int)TipoPermissao.Consulta);

            if (acesso.EhExclusao)
                acoes.Add((int)TipoPermissao.Exclusao);

            if (acesso.EhInsercao)
                acoes.Add((int)TipoPermissao.Inclusao);

            if (acoes.Count > 0)
                query.AppendLine($" or (idmodcoresso = {acesso.IdModCoreSSO} and idacao in ({String.Join(",", acoes)}))");

            acoes.Clear();
        }
        
        int indexOfOr = query.ToString().IndexOf("or");
        query.Replace("or", "and (", indexOfOr, "or".Length).Append(")");

        return await conexao.Obter().QueryAsync<int>(query.ToString());
    }
}