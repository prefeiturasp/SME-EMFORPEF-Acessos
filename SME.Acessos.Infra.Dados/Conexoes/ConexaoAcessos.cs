using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.Acessos.Infra.Dados
{
    public class ConexaoAcessos : ConexaoBase, IConexaoAcessos
    {
        public ConexaoAcessos(string stringConexao)
        {
            conexao = new NpgsqlConnection(stringConexao);
            Abrir();
        }

    }
}
