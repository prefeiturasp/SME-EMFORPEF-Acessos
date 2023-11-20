using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Aplicacao.Settings;
using SME.Acessos.Infra.Dominio.Extensions;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoTokenJwt : IServicoTokenJwt
    {
        private readonly JwtTokenSettings jwtTokenSettings;
        private string tokenGerado;

        public ServicoTokenJwt(IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            this.jwtTokenSettings = jwtTokenSettings?.Value ?? throw new ArgumentNullException(nameof(jwtTokenSettings));
        }

        public string GerarToken(string usuarioLogin, string usuarioNome, int sistemaId, Guid? guidPerfil, IEnumerable<long> permissionamentos)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, usuarioLogin),
                new Claim("login", usuarioLogin),
                new Claim("nome", usuarioNome),
                new Claim("sistema", sistemaId.ToString()),
                new Claim("perfil", guidPerfil.HasValue ? guidPerfil.Value.ToString() : string.Empty),
            };

            if (permissionamentos != null && permissionamentos.Any())
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
                        Encoding.UTF8.GetBytes(jwtTokenSettings.IssuerSigningKey)),
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

        public DadosUsuarioTokenDTO ObterDadosToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSettings.IssuerSigningKey));
            var validator = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = key,
                ValidIssuer = jwtTokenSettings.Issuer,
                ValidAudience = jwtTokenSettings.Audience,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = false
            };

            try
            {
                if (validator.CanReadToken(token))
                {
                    ClaimsPrincipal principal;
                    principal = validator.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                    if (
                        principal.HasClaim(c => c.Type == "login") &&
                        principal.HasClaim(c => c.Type == "nome") &&
                        principal.HasClaim(c => c.Type == "sistema"))
                    {
                        return new DadosUsuarioTokenDTO
                        {
                            Login = ObterValorToken<string>(principal, "login"),
                            Nome = ObterValorToken<string>(principal, "nome"),
                            Sistema = ObterValorToken<int>(principal, "sistema"),
                            Perfil = ObterValorToken<Guid>(principal, "perfil")
                        };
                    }
                }

                throw new NegocioException("Token inválido", System.Net.HttpStatusCode.Unauthorized);
            }
            catch (SecurityTokenException)
            {
                throw new NegocioException("Token inválido", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        private string ObterTokenAtual()
        {
            if (!string.IsNullOrEmpty(tokenGerado))
                return tokenGerado;

            return "";
        }

        private static DateTime ObterDataHoraCriacao(string tokenStr)
        {
            if (!string.IsNullOrEmpty(tokenStr))
            {
                var token = (new JwtSecurityTokenHandler()).ReadToken(tokenStr) as JwtSecurityToken;
                return token.ValidFrom;
            }

            return DateTime.MinValue;
        }

        private static T? ObterValorToken<T>(ClaimsPrincipal principal, string tipo)
        {
            var claim = principal.Claims.FirstOrDefault(c => c.Type == tipo);
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claim.Value);
            }

            return default;
        }
    }
}
