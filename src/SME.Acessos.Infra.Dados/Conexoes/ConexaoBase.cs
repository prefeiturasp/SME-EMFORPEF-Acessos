using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.Acessos.Infra.Dados
{
    public abstract class ConexaoBase : IConexaoBase
    {
        protected IDbConnection conexao;

        public void Dispose()
        {
            if (conexao.State == ConnectionState.Open)
                conexao.Close();

            GC.SuppressFinalize(this);
        }

        public void Abrir()
        {
            if (conexao.State != ConnectionState.Open)
                conexao.Open();
        }

        public void Fechar()
        {
            if (conexao.State != ConnectionState.Closed)
            {
                conexao.Close();
            }
        }

        public IDbConnection Obter()
            => conexao;
    }
}
