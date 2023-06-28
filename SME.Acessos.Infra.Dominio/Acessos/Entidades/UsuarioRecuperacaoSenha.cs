using System.Text.RegularExpressions;
using SME.Acessos.Infra.Dominio.Constantes;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Infra.Dominio.Acessos.Entidades
{
    public class UsuarioRecuperacaoSenha : EntidadeBaseAcessos
    {
        public string Login { get; set; }
        public DateTime? Expiracao { get; set; }
        public Guid? Token { get; set; }
        public long CodigoSistema { get; set; }
        
        public bool TokenRecuperacaoSenhaValido()
            => Expiracao > DateTime.Now;
        
        public void FinalizarRecuperacaoSenha()
        {
            Token = null;
            Expiracao = null;
        }

        public void ValidarSenha(string novaSenha)
        {
            if (novaSenha.Length < 8)
                throw new NegocioException(MensagemNegocio.A_SENHA_DEVE_TER_NO_MÍNIMO_8_CARACTERES);

            if (novaSenha.Length > 12)
                throw new NegocioException(MensagemNegocio.A_SENHA_DEVE_TER_NO_MÁXIMO_12_CARACTERES);

            if (novaSenha.Contains(" "))
                throw new NegocioException(MensagemNegocio.A_SENHA_NAO_PODE_CONTER_ESPACOS_EM_BRANCO);

            var regexSenha = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d|\W)[^áàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ]{8,12}$");

            if (!regexSenha.IsMatch(novaSenha))
                throw new NegocioException(MensagemNegocio.A_SENHA_DEVE_CONTER_SOMENTE);
        }

        public void IniciarRecuperacaoDeSenha(string usuarioCoreEmail)
        {
            if (string.IsNullOrWhiteSpace(usuarioCoreEmail))
            {
                throw new NegocioException("Você não tem um e-mail cadastrado para recuperar sua senha. Para restabelecer o seu acesso, procure o Diretor da sua UE ou Administrador do SGP da sua unidade.");
            }

            Token = Guid.NewGuid();
            Expiracao = DateTime.Now.AddHours(6);
        }
    }
}
