using Microsoft.EntityFrameworkCore;
using PersonColor.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var useEntityFramework = builder.Configuration.GetValue<bool>("UseEntityFramework");
if (useEntityFramework)
{
    builder.Services.AddDbContext<PersonDbContext>(options =>
        options.UseSqlite("Data Source=persons.db"));
    builder.Services.AddScoped<IPersonRepository, EfPersonRepository>();
}
else
{
    var csvPath = Path.Combine(AppContext.BaseDirectory, "sample-input.csv");

    if (!File.Exists(csvPath))
    {
        return;
    }

    builder.Services.AddSingleton<IPersonRepository>(sp => new CsvPersonRepository(csvPath));
}

var app = builder.Build();

if (useEntityFramework)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PersonDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
