using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SIO.Domain.Commands;
using SIO.Domain.Queries;
using SIO.Infrastructure;
using SIO.Infrastructure.Commands;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.Extensions
{
    public static class SIOInfrastructureBuilderExtensions
    {
        public static ISIOInfrastructureBuilder DisableCommandLogging(this ISIOInfrastructureBuilder builder)
        {
            builder.Services.AddSingleton<ICommandStore, NoOpCommandStore>();
            return builder;
        }

        public static ISIOInfrastructureBuilder DisableQueryLogging(this ISIOInfrastructureBuilder builder)
        {
            builder.Services.AddSingleton<IQueryStore, NoOpQueryStore>();
            return builder;
        }
    }
}
