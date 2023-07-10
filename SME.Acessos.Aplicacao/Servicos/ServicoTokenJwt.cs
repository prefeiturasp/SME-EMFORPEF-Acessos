using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Aplicacao.Settings;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoTokenJwt : IServicoTokenJwt
    {
        private readonly JwtTokenSettings jwtTokenSettings;
        private string tokenGerado;

        public ServicoTokenJwt(IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            this.jwtTokenSettings = jwtTokenSettings?.Value;
        }

        public string GerarToken(string usuarioLogin, string usuarioNome, Guid? guidPerfil, IEnumerable<long> permissionamentos)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, usuarioLogin));
            claims.Add(new Claim("login", usuarioLogin));
            claims.Add(new Claim("nome", usuarioNome));
            claims.Add(new Claim("perfil", guidPerfil.HasValue ? guidPerfil.ToString() : string.Empty));

            foreach (var permissao in permissionamentos)
                claims.Add(new Claim("roles", permissao.ToString()));

            var now = DateTimeExtensions.HorarioBrasilia();
            var token = new JwtSecurityToken(
                issuer: jwtTokenSettings.Issuer,
                audience: jwtTokenSettings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(jwtTokenSettings.ExpiresInMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(jwtTokenSettings.IssuerSigningKey)),
                        SecurityAlgorithms.HmacSha256)
                );

            tokenGerado = new JwtSecurityTokenHandler()
                      .WriteToken(token);

            return tokenGerado;
        }

        public DateTime ObterDataHoraCriacao()
            => ObterDataHoraCriacao(ObterTokenAtual());

        public DateTime ObterDataHoraExpiracao()
        {
            var tokenStr = ObterTokenAtual();
            if (!string.IsNullOrEmpty(tokenStr))
            {
                var token = (new JwtSecurityTokenHandler()).ReadToken(tokenStr) as JwtSecurityToken;
                return token.ValidTo;
            }
            return DateTime.MinValue;
        }

        private string ObterTokenAtual()
        {
            if (!string.IsNullOrEmpty(tokenGerado))
                return tokenGerado;

            return "";
        }

        private DateTime ObterDataHoraCriacao(string tokenStr)
        {
            if (!string.IsNullOrEmpty(tokenStr))
            {
                var token = (new JwtSecurityTokenHandler()).ReadToken(tokenStr) as JwtSecurityToken;
                return token.ValidFrom;
            }

            return DateTime.MinValue;
        }

    }
}
