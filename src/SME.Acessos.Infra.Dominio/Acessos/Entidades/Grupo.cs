namespace SME.Acessos.Infra.Dominio.Acessos.Entidades
{
    public class Grupo : EntidadeBaseAcessos
    {
        public Guid Perfil { get; set; }
        public string Nome { get; set; }
        public long IdAbrangencia { get; set; }
        public bool EhPerfilManual { get; set; }
    }
}
