namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class Pessoa : EntidadeBaseCoreSSO
    {
        public string Nome { get; set; }
        public string NomeAbreviado { get; set; }
        public Guid? IdNacionalidade { get; set; }
        public int Naturalizado { get; set; }
        public Guid IdNaturalidade { get; set; }
        public DateTime DataNascimento { get; set; }
        public int EstadoCivil { get; set; }
        public int RacaCor { get; set; }
        public int Sexo { get; set; }
        public Guid IdFiliacaoPai { get; set; }
        public Guid IdFiliacaoMae { get; set; }
        public int Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int Integridade { get; set; }
        public int IdFoto { get; set; }
        public string NomeSocial { get; set; }
    }
}
