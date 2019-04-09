using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Utils.Politicas
{
    public class AdministradorOuProprioFuncionarioHandler : AuthorizationHandler<AdministradorOuProprioFuncionarioRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdministradorOuProprioFuncionarioRequirement requirement)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (requirement == null)
                throw new ArgumentNullException(nameof(requirement));

            HttpContext httpContext = ((AuthorizationFilterContext)context.Resource).HttpContext;

            if (!context.User.Identity.IsAuthenticated)
                return Task.CompletedTask;

            if (httpContext.User.Identities.First().Claims.Where(c => (c.Type == ClaimTypes.Role) && (c.Value == "Administrador")).Count() == 1)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            int idFuncionarioLogado = int.Parse(httpContext.User.Identities.First().Claims.Where(c => c.Type == "IdFuncionario").First().Value);
            int idFuncionario = int.Parse(httpContext.Request.Path.Value.Split("/").Last());

            if (idFuncionarioLogado == idFuncionario)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
