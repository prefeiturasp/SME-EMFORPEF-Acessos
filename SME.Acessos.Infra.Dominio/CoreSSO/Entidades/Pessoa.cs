namespace SME.Acessos.Infra.Dominio.CoreSSO.Entidades
{
    public class Pessoa : EntidadeBaseCoreSSO
    {
        public string Nome { get; set; }
        public string NomeAbreviado { get; set; }
        public string IdNacionalidade { get; set; }
        public int Naturalizado { get; set; }
        public string IdNaturalidade { get; set; }
        public DateTime DataNascimento { get; set; }
        public int EstadoCivil { get; set; }
        public int RacaCor { get; set; }
        public int Sexo { get; set; }
        public int IdFiliacaoPai { get; set; }
        public int IdFiliacaoMae { get; set; }
        public int Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int Integridade { get; set; }
        public int IdFoto { get; set; }
        public string NomeSocial { get; set; }
    }
}
