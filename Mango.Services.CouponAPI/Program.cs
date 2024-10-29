using Mango.Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options=> 
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
app.Run();

void ApplyChanges()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbcontext =  scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (dbcontext?.Database.GetPendingMigrations().Count() > 0)
            dbcontext.Database.Migrate();
    }

}
