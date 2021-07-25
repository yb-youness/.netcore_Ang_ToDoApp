using System.Linq;
using API.Data;
using API.Errors;
using API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

    

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //! This to configure the connection string 
            services.AddDbContext<Context>(
                conf => conf.UseSqlite(_Configuration.GetConnectionString("DefaultConectionString")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            //! To Configure The Validation Error Msg The Order Is Imprtante
            services.Configure<ApiBehaviorOptions>(opt => {
               opt.InvalidModelStateResponseFactory = actionContext =>{
                      var errors = actionContext.ModelState  // This To Crab On the Contetxt
                        .Where(errors=> errors.Value.Errors.Count >0) // This is To Check If he Model Contains A Error
                        .SelectMany(x=> x.Value.Errors) // This To Extarct The Array Of The Errors
                        .Select(x => x.ErrorMessage).ToArray(); // This To Format The Error Msg 
                  var errorResp = new ApiValidationErrorResponce{
                       Errors = errors
                  };
                    return new BadRequestObjectResult(errorResp);     
               };
            }) ;
            
             //! Cors   + Add To The Midelware
             services.AddCors( op =>{
                op.AddPolicy("CorsPolicy",policy =>{
                  policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:4200");
                });
             });
           


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
               //! Middleware
               app.UseMiddleware<ExceptionMiddleware>();
            //! Change This To Coustme Error Midelware Handler To Get The Coustme Errors 
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseSwagger();
            //     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            // }



            //! Coustme Error Handler For Not Found
            //! If A Error Habend Redirect To The Coustme Controller
             app.UseStatusCodePagesWithReExecute("/errors/{0}");
             
             //? This Causes A Cors Error
            //app.UseHttpsRedirection();

            app.UseRouting();
           
            //! Cors Midelware
             app.UseCors("CorsPolicy");
            
           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
