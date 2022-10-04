using System;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;


using LPC.Web.Configuration;
using LPC.Domain.Database;
using MediatR;
using LPC.Domain.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<LpcDbContext>((options) => _ = options.UseNpgsql("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=long_play_collection;"));
builder.Services.AddMediatR(typeof(GetAllRecordsQueryHandler));
var app = builder.Build();

app.UseFileServer();

app.MapControllers();

app.Run();
