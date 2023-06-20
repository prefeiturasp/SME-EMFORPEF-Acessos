using System.Text;
using Dapper;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.Enumeradores;
using Modulo = SME.Acessos.Infra.Dominio.Acessos.Entidades.Modulo;

namespace SME.Acessos.Infra.Dados.Acessos;

public class RepositorioPermissao : RepositorioBaseAcessos<Modulo>, IRepositorioPermissao
{
    public RepositorioPermissao(IConexaoAcessos conexao) : base(conexao)
    {
    }

    public async Task<IList<Modulo>> ObterPermissoesPorModulos(IList<GrupoPermissao> modulosGrupoPermissao)
    {
        var query = new StringBuilder();
        
        query.AppendLine(@"select id, descricao, idmodcoresso,idacao from modulos where 1=1 ");
        
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
                query.AppendLine($" or (idmodcoresso = {acesso.ModuloId} and idacao in ({String.Join(",", acoes)}))");

            acoes.Clear();
        }
        
        int indexOfOr = query.ToString().IndexOf(" or ");
        query.Replace(" or ", "and (", indexOfOr, " or ".Length).Append(")");

        var ret = query.ToString();
        return (await conexao.Obter().QueryAsync<Modulo>(query.ToString())).ToList();
    }
}