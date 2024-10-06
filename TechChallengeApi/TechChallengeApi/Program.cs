using Common.Constants;
using TechChallengeApi.BuilderExtensions;

var builder = WebApplication.CreateBuilder(args);

var Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(GenericSettings.AppSettings + GenericSettings.JsonExtension, false, true)
            .AddJsonFile(GenericSettings.AppSettings + Environment.GetEnvironmentVariable(GenericSettings.AspnetCoreEnvironment) + GenericSettings.JsonExtension, true, true)
            .AddEnvironmentVariables()
            .Build();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDependencyInjection(Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
