
using Microsoft.EntityFrameworkCore;
using TodoListBackend.Contexts;
using TodoListBackend.Database;
using TodoListBackend.Services;

namespace TodoListBackend;

public class Program
{
    const string todoConnectionStringConfigName = "TodoList";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Database configuration
        var configuration = builder.Configuration;

        if(configuration == null)
        {
            throw new NullReferenceException(nameof(configuration));
        }


        string? todoConnectionString = configuration.GetConnectionString(todoConnectionStringConfigName);

        if(todoConnectionString == null)
        {
            throw new ArgumentNullException(nameof(todoConnectionString));
        }

        builder.Services.AddDbContext<TodoDbContext>(options =>
        {
            options.UseSqlServer(todoConnectionString);
        });

        builder.Services.AddTransient<IDBContext, TodoDbContext>();
        builder.Services.AddTransient<ITimeService, TimeService>();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        string[] allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? throw new ApplicationException("AllowedOrigins is not set");

        const string corsPolicyName = "_allowSpecificOrigins";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: corsPolicyName, policy =>
            {
                policy.WithOrigins(allowedOrigins).WithMethods("GET","POST","DELETE", "OPTIONS").WithHeaders("content-type");
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors(corsPolicyName);


        app.MapControllers();

        app.Run();
    }
}