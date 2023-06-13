namespace SME.Acessos.Infra.Dominio
{
    public abstract class EntidadeBase<TChave>
        where TChave : struct
    {
        public TChave Id {  get; set; }
    }
}
