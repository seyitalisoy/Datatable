using Castle.DynamicProxy;
using Core.Utilities.Exceptions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Security
{
    public class ClaimsControlAspect : MethodInterception
    {

        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public ClaimsControlAspect(string roles)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _roles = roles.Split(',');
        }

        protected override void OnBefore(IInvocation invocation)
        {

            var roleClaims = _httpContextAccessor.HttpContext.User
                .Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new AuthorizationException("Yetkiniz yok");
        }
    }
}
