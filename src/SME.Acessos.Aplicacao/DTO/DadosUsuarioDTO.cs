namespace SME.Acessos.Aplicacao.DTO
{
    public record DadosUsuarioDto(
        string Nome, 
        string Cpf, 
        string Login, 
        string Email, 
        string Telefone, 
        string Endereco, 
        string Numero, 
        string Complemento, 
        string Bairro, 
        string Cep, 
        string Cidade, 
        string Estado, 
        string? NomeSocial);
}
