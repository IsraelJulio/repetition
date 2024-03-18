using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repetition.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuizRepository, QuizRepository>();
        }
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuizRepository, QuizRepository>();


        }
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins(configuration.GetSection("Cors:AllowedOrigin").Get<string[]>())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            return services;
        }
    }
}
