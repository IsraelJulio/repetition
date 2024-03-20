using Api.Extensions.OData;
using Infra.Context;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing.Conventions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using repetition.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddControllers()
                .AddOData(opt =>
                {
                    opt.Conventions.Remove(opt.Conventions.OfType<MetadataRoutingConvention>().First());
                    opt.AddRouteComponents("odata"
                       , EdmModelConfiguration.GetEdmModel()
                       , services => SearchBindingsConfiguration.GetSearchBindings(services)
                    );
                    opt.Select().Expand().Filter().OrderBy().SetMaxTop(1000).Count();
                }
                )
                .AddNewtonsoftJson(
                    options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    }
                  );
builder.Services.AddControllers();
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsImplementation", builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()) ;
    //options.AddDefaultPolicy(builder =>
    //{
    //    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    //});
});

builder.Services.AddDbContext<RepetitionDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    opt.UseNpgsql(connectionString);
}, ServiceLifetime.Transient);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();


app.UseCors("MyCorsImplementation");

app.UseEndpoints(endpoints =>endpoints.MapControllers());

app.Run();
