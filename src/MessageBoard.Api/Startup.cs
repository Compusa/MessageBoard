using MediatR;
using MessageBoard.Application;
using MessageBoard.Domain;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Infrastructure;
using MessageBoard.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace MessageBoard.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<MessageBoardContext>(options =>
                {
                    options.UseInMemoryDatabase("MessageBoard");
                });

            services
                .AddScoped<IReadOnlyMessageBoardContext, ReadOnlyMessageBoardContext>()
                .AddScoped<IMessageRepository, MessageRepository>();

            services.AddControllers();

            services
                //.AddProblemDetails()
                .AddMediatR(typeof(MessageDto).Assembly)
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Message Board API", Version = "v1" });

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);

                    options.CustomSchemaIds((type) => type.Name.Replace("Dto", string.Empty).Replace("Command", string.Empty));
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseProblemDetails();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Message Board API V1");
                });
        }
    }
}
