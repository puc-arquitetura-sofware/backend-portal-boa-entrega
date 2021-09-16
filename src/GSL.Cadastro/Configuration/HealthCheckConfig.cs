using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GSL.Cadastro.Api.Configuration
{
    public static class HealthCheckConfig
    {
        public static void AddHealthCheckConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
                            //.AddSqlServer(configuration.GetConnectionString("NOME-DA-VARIVEL-DE-CONEXAO"), name: "BoaEntregaCadastroDB");
            services.AddHealthChecksUI()
                .AddInMemoryStorage();

        }

        public static void UseHealthCheckConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthChecks("/status",
              new HealthCheckOptions()
              {
                  ResponseWriter = async (context, report) =>
                  {
                      var result = JsonConvert.SerializeObject(
                          new
                          {
                              statusApplication = report.Status.ToString(),
                              healthChecks = report.Entries.Select(e => new
                              {
                                  check = e.Key,
                                  ErrorMessage = e.Value.Exception?.Message,
                                  status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                              })
                          });
                      context.Response.ContentType = MediaTypeNames.Application.Json;
                      await context.Response.WriteAsync(result);
                  }
              });

            // Gera o endpoint que retornará os dados utilizados no dashboard
            app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI();
        }
    }
}
