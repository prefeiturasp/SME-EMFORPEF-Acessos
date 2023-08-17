using SME.Acessos.Infra.Dominio.Constantes;
using SME.Acessos.Infra.Dominio.Extensions;
using System.Text.RegularExpressions;

namespace SME.Acessos.Infra.Dominio.Acessos.Entidades
{
    public class UsuarioRecuperacaoSenha : EntidadeBaseAcessos
    {
        public string Login { get; set; }
        public DateTime? Expiracao { get; set; }
        public Guid? Token { get; set; }
        public long CodigoSistema { get; set; }
        
        public bool TokenRecuperacaoSenhaValido()
            => Expiracao > DateTimeExtensions.HorarioBrasilia();
        
        public void FinalizarRecuperacaoSenha()
        {
            Token = null;
            Expiracao = null;
        }

        public void ValidarSenha(string novaSenha)
        {
            if (novaSenha.Length < 8)
                throw new NegocioException(MensagemNegocio.A_SENHA_DEVE_TER_NO_MINIMO_8_CARACTERES);

            if (novaSenha.Length > 12)
                throw new NegocioException(MensagemNegocio.A_SENHA_DEVE_TER_NO_MAXIMO_12_CARACTERES);

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
                throw CodigoSistema switch
                {
                    Constantes.Constantes.CODIGO_SISTEMA_CONECTA_FORMACAO => new NegocioException(MensagemNegocio.VOCE_NAO_TEM_EMAIL_CADASTRADO_PARA_RECUPERAR_SENHA_CONECTA, System.Net.HttpStatusCode.BadRequest),
                    _ => new NegocioException(MensagemNegocio.VOCE_NAO_TEM_EMAIL_CADASTRADO_PARA_RECUPERAR_SENHA),
                };
            }

            Token = Guid.NewGuid();
            Expiracao = DateTimeExtensions.HorarioBrasilia().AddHours(6);
        }
    }
}
