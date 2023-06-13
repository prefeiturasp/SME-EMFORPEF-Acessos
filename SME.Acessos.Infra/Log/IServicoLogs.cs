using SME.Acessos.Infra.Dominio;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Servicos
{
    public interface IServicoLogs
    {
        Task Enviar(string mensagem, LogContexto contexto = LogContexto.Geral, LogNivel nivel = LogNivel.Critico, string observacao = "", string rastreamento = "");
    }
}
