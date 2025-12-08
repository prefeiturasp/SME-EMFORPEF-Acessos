namespace SME.Acessos.Infra.Servicos.Eol
{
    public interface IServicoEol
    {
        Task<bool> VerificarFuncionarioAtivo(string registroFuncional);
    }
}
