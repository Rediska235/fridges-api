using Fridges.API.Middlewares;
using Fridges.Application;
using Fridges.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplicaton();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();
