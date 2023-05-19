using FluentValidation;
using FluentValidation.AspNetCore;
using SimApi.Data.Domain;
using SimApi.Service.RestExtension;
using SimApi.Service.Validators;
using System;
using System.Reflection;

namespace SimApi.Service;

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
        services.AddValidatorsFromAssemblyContaining<StaffValidator>();
        services.AddCustomSwaggerExtension();
        services.AddDbContextExtension(Configuration);
        services.AddMapperExtension();
        services.AddRepositoryExtension();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

        }
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DefaultModelsExpandDepth(-1);
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sim Company");
            c.DocumentTitle = "SimApi Company";
        });

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
