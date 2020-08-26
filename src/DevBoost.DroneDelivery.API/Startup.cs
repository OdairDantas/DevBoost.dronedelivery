using DevBoost.DroneDelivery.Application.Bus;
using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Application.Handles;
using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
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
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers();
            services.AddMediatR(typeof(Startup));
            var assembly = AppDomain.CurrentDomain.Load("DevBoost.DroneDelivery.Application");
            services.AddMediatR(assembly);
            services.AddTransient<IMediatrHandler, MediatrHandler>();

            services.AddScoped<IRequestHandler<CarregarBareriaDroneCommand, bool>, DroneCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarDroneCommand, bool>, DroneCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<EntregarPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<INotificationHandler<SolicitarEntregaEvent>, EntregaHandler>();
            services.AddScoped<INotificationHandler<DroneAutonomiaBaixaEvent>, DroneHandler>();

            services.AddScoped<IDroneQueries, DroneQueries>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();





            services.AddSwaggerGen(c => c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "Drone Delivery", Version = "v1" }));
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IDroneRepository, DroneRepository>();
            services.AddTransient<IDroneItinerarioRepository, DroneItinerarioRepository>();
            services.AddTransient<IEntregaRepository, EntregaRepository>();
            services.AddDbContext<DroneDeliveryContext>();



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
