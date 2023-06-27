
using SME.Acessos.Aplicacao.Enumerados;

namespace SME.Acessos.Aplicacao.DTO
{
    public class RetornoAlteracaoSenhaDto
    {
        public RetornoAlteracaoSenhaDto(AlterarSenhaStatus status, string login = "")
        {
            Status = status;
            Login = login;
        }

        public string Login { get; set; }
        public AlterarSenhaStatus Status { get; set; }
    }
}
