using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using System.Text;
using WebApplication1;
using WebApplication1.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddJwtDocumentation(builder);
builder.Services.AddSwaggerDocumentation(builder);



//builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServicesDependencies();
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("connectionName")));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.UseJwtAuthentication();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.MapHangfireDashboard("/Dashboard");



RecurringJob.AddOrUpdate<IUserService>(
    x => x.GetUsersFromExternalApi(),
    Cron.Minutely);

app.Run();
