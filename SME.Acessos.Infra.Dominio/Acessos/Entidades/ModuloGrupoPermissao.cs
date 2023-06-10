namespace SME.Acessos.Infra.Dominio.Acessos
{
    public class ModuloGrupoPermissao : EntidadeBaseAcessos
    {
        public string Descricao { get; set; }
        public bool EhConsulta { get; set; }
        public bool EhInsercao { get; set; }
        public bool EhAlteracao { get; set; }
        public bool EhExclusao { get; set; }
        public int IdModCoreSSO { get; set; }
    }
}
