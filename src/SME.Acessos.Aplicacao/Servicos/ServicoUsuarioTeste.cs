using Bogus;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Extensoes;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoUsuarioTeste(
        IServicoUsuarios servicoUsuarios,
        IRepositorioUsuario repositorioUsuario) : IServicoUsuarioTeste
    {
        public async Task<IEnumerable<DadosPessoaUsuarioDto>> CadastrarUsuariosEmMassaAsync(int quantidade, Guid? perfilId)
        {
            perfilId ??= new Guid("7EDA4540-A16C-4FE5-8322-9F75B3414E27");

            var cpfsUnicos = await GerarCpfsValidosEIneditosAsync(quantidade);
            var cpfsQueue = new Queue<string>(cpfsUnicos);

            var fakerUsuario = new Faker<UsuarioDTO>("pt_BR")
                .RuleFor(u => u.Nome, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Senha, f => f.Internet.Password())
                .RuleFor(u => u.Login, _ => cpfsQueue.Dequeue());

            var novosUsuarios = fakerUsuario.Generate(quantidade);

            var usuariosBulk = novosUsuarios.Select(u => new UsuarioBulkInsertDto
            {
                PessoaId = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid(),
                Nome = u.Nome,
                Login = u.Login,
                Email = u.Email,
                SenhaCriptografada = CriptografiaExtensions.CriptografarSenhaTripleDES(u.Senha)
            }).ToList();

            await repositorioUsuario.InserirUsuariosEmMassaAsync(usuariosBulk, perfilId.Value);

            return usuariosBulk.Select(u => new DadosPessoaUsuarioDto
            {
                Login = u.Login,
                Nome = u.Nome,
                Email = u.Email,
                Senha = novosUsuarios.First(n => n.Login == u.Login).Senha,
                PerfilId = perfilId.Value
            });
        }

        public async Task ExcluirUsuariosEmMassaAsync(IEnumerable<string> logins)
        {
            await repositorioUsuario.ExcluirUsuariosEmMassaAsync(logins);
        }

        private async Task<HashSet<string>> GerarCpfsValidosEIneditosAsync(int quantidadeDesejada)
        {
            var cpfsAprovados = new HashSet<string>(quantidadeDesejada);

            while (cpfsAprovados.Count < quantidadeDesejada)
            {
                int quantidadeFaltante = quantidadeDesejada - cpfsAprovados.Count;
                var loteTemporario = new HashSet<string>(quantidadeFaltante);

                while (loteTemporario.Count < quantidadeFaltante)
                {
                    loteTemporario.Add(GerarCpfMatematico());
                }

                var cpfsQueJaExistemNoBanco = await servicoUsuarios.ObterLoginsExistentesAsync(loteTemporario);

                loteTemporario.ExceptWith(cpfsQueJaExistemNoBanco);
                cpfsAprovados.UnionWith(loteTemporario);
            }

            return cpfsAprovados;
        }

        private static string GerarCpfMatematico()
        {
            Span<char> cpfSpan = stackalloc char[11];
            int soma1 = 0, soma2 = 0;

            for (int i = 0; i < 9; i++)
            {
                int digito = Random.Shared.Next(0, 10);
                cpfSpan[i] = (char)(digito + '0');
                soma1 += digito * (10 - i);
                soma2 += digito * (11 - i);
            }

            int d1 = soma1 % 11 < 2 ? 0 : 11 - (soma1 % 11);
            cpfSpan[9] = (char)(d1 + '0');
            soma2 += d1 * 2;

            int d2 = soma2 % 11 < 2 ? 0 : 11 - (soma2 % 11);
            cpfSpan[10] = (char)(d2 + '0');

            return new string(cpfSpan);
        }
    }
}