using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LPC.Domain.Database;
using MediatR;
using LPC.Domain.Queries;
using LPC.Domain.Mapper;
using LPC.Domain.Helpers.Interfaces;
using LPC.Domain.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<LpcDbContext>((options) => _ = options.UseNpgsql("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=long_play_collection;"));
builder.Services.AddMediatR(typeof(GetAllRecordsQueryHandler));
builder.Services.AddAutoMapper(typeof(LPCProfile));
builder.Services.AddTransient(typeof(ILPCollectionService), typeof(LPCollectionService));

var app = builder.Build();

app.UseFileServer();

app.MapControllers();

app.Run();
