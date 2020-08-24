using DevBoost.DroneDelivery.Application.Bus;
using DevBoost.DroneDelivery.Application.Services;
using DevBoost.DroneDelivery.Domain.Interfaces.Contexts;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using DevBoost.DroneDelivery.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace DevBoost.DroneDelivery.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMediatR(typeof(Startup));
            var assembly = AppDomain.CurrentDomain.Load("DevBoost.DroneDelivery.Application");
            services.AddMediatR(assembly);
            services.AddTransient<IMediatrHandler, MediatrHandler>();
            services.AddSwaggerGen(c => c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "Drone Delivery", Version = "v1" }));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IDroneRepository,DroneRepository>();
            services.AddTransient<IDroneItinerarioRepository, DroneItinerarioRepository>();
            services.AddTransient<IEntregaRepository, EntregaRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<IDbContext, DroneDeliveryContext>();


            services.AddTransient<IPedidoService,PedidoService>();
            services.AddTransient<IDroneService,DroneService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Drone Delivery"); });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
