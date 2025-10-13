using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            // servisler gelecek
            services.AddSingleton<Stopwatch>();
        }
    }
}
