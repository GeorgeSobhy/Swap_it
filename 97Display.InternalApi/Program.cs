using SwapIt.BusinessLogic;
using SwapIt.BusinessLogic.Authentication;
using SwapIt.Data;
using SwapIt.Api;
using SwapIt.Api.HealthCheck;
using SwapIt.Mapper;
using SwapIt.Mapper.HostedServices;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Pattern.EF;
using RquestContext;
using RquestContext.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.Internal().MethodMappingEnabled = false;
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(
       options => options.UseSqlServer("name=ConnectionStrings:97DisplayCRMConnection")); //, ServiceLifetime.Transient
builder.Services.AddDbContext<ApplicationContext>(
       options => options.UseSqlServer("name=ConnectionStrings:97DisplayCRMConnection")); //, ServiceLifetime.Transient
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddHttpClient<InternalAPIHttpClient>();
//builder.Services.AddScoped<IInternalAPIProvider, InternalAPIProvider>();
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("x-api-version"));
});



builder.Services.AddHealthChecks()
    .AddCheck("ICMP Health Check",
        new ICMPHealthCheck(200, new HttpContextAccessor()));

builder.Services.AddControllers();

// Configure services
//builder.Services.AddSingleton<IConfigManager, ConfigManager>();

builder.Services.AddConfig(builder.Configuration);



builder.Services.AddMemoryCache();
builder.Services.AddServices();
builder.Services.AddProviders();
builder.Services.AddScoped<IRequestContext, RequestContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SwapIt Api",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

var types = from asm in AppDomain.CurrentDomain.GetAssemblies()
            from type in asm.GetTypes()
            where type.FullName == "SwapIt.Api.Controllers.AquaController"
            orderby type.Name
            select type;
foreach (var type in types)
{
    Console.WriteLine("{0} ({1})", type.Name, type.Assembly.FullName);
    var methods = type.GetMethods().Where(x => x.DeclaringType != null && x.DeclaringType.FullName == "SwapIt.Api.Controllers.AquaController").ToList();

    foreach (var method in methods)
    {
 

        Console.WriteLine("\t{0} {1} {2}", method.ReturnType == null ? "void" : method.ReturnType.FullName.Replace("System.Threading.Tasks.Task`1[[System.Collections.Generic.List`1[[SwapIt.Mapper.Models.", "List[").Replace(", System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]", "]").Replace("System.Threading.Tasks.Task`1[[SwapIt.Mapper.Models.", "").Replace(", 97Display.Mapper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", ""), method.Name, string.Join(" ", method.GetParameters().Select(x => x.Name).ToArray()));
    }
}

builder.Services.AddHostedService<QueuedHostedService>();
builder.Services.AddSingleton<IBackgroundTaskQueue>(ctx =>
{
    return new BackgroundTaskQueue(10000);
});

builder.Services.AddMvc(config =>
{
    config.Filters.Add<LogDetailsResultFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

// set global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<JwtMiddleware>();

//app.UseHealthChecks(new PathString("/api/health"),
//    new CustomHealthCheckOptions());


app.MapControllers();
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwapIt Api");
    //Remove if not want Swagger at root
    c.RoutePrefix = string.Empty;

});
app.UseSession();




app.Run();

public partial class Program { }