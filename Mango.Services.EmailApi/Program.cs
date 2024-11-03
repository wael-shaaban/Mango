using Microsoft.EntityFrameworkCore;
using Mango.Services.EmailApi.Data;
using Mango.Services.EmailApi.Extensions;
using Mango.Services.EmailApi.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
dbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(new EmailService(dbContextOptions.Options));
builder.Services.AddRabbitMQConsumerService(builder.Configuration);
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

app.UseAuthorization();

app.MapControllers();
ApplyChanges();
app.UseRabbitMqServiceConsumer();
app.Run();
//for apply pending migrations
void ApplyChanges()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (dbcontext?.Database.GetPendingMigrations().Any() == true)
            dbcontext.Database.Migrate();
    }
}