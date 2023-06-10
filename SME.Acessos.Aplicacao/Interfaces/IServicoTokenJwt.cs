using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoTokenJwt
    {
        string GerarToken(string usuarioLogin, string usuarioNome, Guid guidPerfil, IEnumerable<int> permissionamentos);

        DateTime ObterDataHoraCriacao();

        DateTime ObterDataHoraExpiracao();
    }
}