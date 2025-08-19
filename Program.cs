using Microsoft.EntityFrameworkCore;
using DDDSample.Application.Services;
using DDDSample.Domain.Repositories;
using DDDSample.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// ���U MyDbContext�A�бN "YourConnectionString" ��������ڳs�u�r��
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderService>();

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
