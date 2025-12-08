using System.ComponentModel.DataAnnotations;

namespace SME.Acessos.Aplicacao.DTO
{
    public class AlterarNomeUsuarioDTO
    {
        [Required(ErrorMessage = "É necessário informar o nome")]
        public string Nome { get; set; }
    }
}
