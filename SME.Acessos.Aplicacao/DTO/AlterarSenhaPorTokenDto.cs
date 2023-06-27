using System.ComponentModel.DataAnnotations;

namespace SME.Acesos.Aplicacao.DTO
{
    public class AlterarSenhaPorTokenDto
    {
        [Required(ErrorMessage = "É necessario informar o token de recuperação de senha")]
        public string Token { get; set; }

        [Required(ErrorMessage = "É necessario informar a nova senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "É necessario informar a nova senha")]
        public int Sistema { get; set; }
    }
}
