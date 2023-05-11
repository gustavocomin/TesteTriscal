using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data;
using SistemaCompra.Infra.Data.Produto;
using SistemaCompra.Infra.Data.SolicitacaoCompra;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.IO;
using System.Reflection;

namespace SistemaCompra.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var assembly = AppDomain.CurrentDomain.Load("SistemaCompra.Application");
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);
            services.AddSignalR();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ISolicitacaoCompraRepository, SolicitacaoCompraRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<SistemaCompraContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly("SistemaCompra.API"))
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prova Sisprev", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            VerificarConexaoBancoDados(logger);

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prova Sisprev V1");
                c.RoutePrefix = string.Empty;
            });
        }

        private void VerificarConexaoBancoDados(ILogger<Startup> logger)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

                var optionsBuilder = new DbContextOptionsBuilder<SistemaCompraContext>();
                using var dbContext = new SistemaCompraContext();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

                using var db = new SistemaCompraContext(optionsBuilder.Options);
                if (!db.TestarConexao())
                    throw new Exception("Não foi possível conectar ao banco de dados.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao conectar ao banco de dados: {ex.Message}");
                throw; // Ou lide com o erro de outra maneira, de acordo com suas necessidades.
            }
        }
    }
}
