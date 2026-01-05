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
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoTokenJwt(IOptions<JwtTokenSettings> jwtTokenSettings) : IServicoTokenJwt
    {
        private readonly JwtTokenSettings jwtTokenSettings = jwtTokenSettings?.Value ?? throw new ArgumentNullException(nameof(jwtTokenSettings));
        private string? tokenGerado;

        public string GerarToken(ClaimsTokenDto claimsTokenDto)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, claimsTokenDto.UsuarioLogin),
                new Claim("login", claimsTokenDto.UsuarioLogin),
                new Claim("nome", claimsTokenDto.UsuarioNome),
                new Claim("sistema", claimsTokenDto.SistemaId.ToString()),
                new Claim("perfil", claimsTokenDto.GuidPerfil.HasValue ? claimsTokenDto.GuidPerfil.Value.ToString() : string.Empty),
            ];

            PreencherClaimsPerfis(claimsTokenDto.PerfisUsuario, claims);

            PreencherClaimsDres(claimsTokenDto.Dres, claims);

            PreencherClaimsRoles(claimsTokenDto.Permissionamentos, claims);

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

        private static void PreencherClaimsRoles(IEnumerable<long> permissionamentos, List<Claim> claims)
        {
            if (permissionamentos.EhNulo())
                return;
            
            foreach (var permissao in permissionamentos)
                claims.Add(new Claim("roles", permissao.ToString()));
            
        }

        private static void PreencherClaimsDres(IEnumerable<string> dres, List<Claim> claims)
        {
            if (dres.EhNulo())
                return;
            
            foreach (var dre in dres)
                claims.Add(new Claim("dres", dre));
            
        }

        private static void PreencherClaimsPerfis(IEnumerable<UsuarioGrupoPessoa> perfisUsuario, List<Claim> claims)
        {
            if (perfisUsuario.EhNulo())
                return;
            
            foreach (var perfil in perfisUsuario)
                claims.Add(new Claim("perfis", perfil.GrupoId.ToString()));
        }

        public DateTime ObterDataHoraCriacao()
            => ObterDataHoraCriacao(ObterTokenAtual());

        public DateTime ObterDataHoraExpiracao()
        {
            var tokenStr = ObterTokenAtual();
            if (!string.IsNullOrEmpty(tokenStr))
            {
                if (new JwtSecurityTokenHandler().ReadToken(tokenStr) is JwtSecurityToken token)
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
                if (new JwtSecurityTokenHandler().ReadToken(tokenStr) is JwtSecurityToken token)
                    return token.ValidFrom;
            }

            return DateTime.MinValue;
        }

        private static T? ObterValorToken<T>(ClaimsPrincipal principal, string tipo)
        {
            var claim = principal.Claims.FirstOrDefault(c => c.Type == tipo);
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                return (T?)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claim.Value);
            }

            return default;
        }
    }
}
