using ChatbotAI.Db;
using ChatbotAI.Dtos;
using ChatbotAI.Middlewares;
using ChatbotAI.Repositories;
using ChatbotAI.Services.Implementations;
using ChatbotAI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddSingleton<IGenerationService, GenerationService>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();

var orgin = configuration["Origin"] ?? throw new NullReferenceException("Empty orgin");

builder.Services.AddCors(options =>
    options.AddPolicy("Origin",
        builder =>
        {
            builder.WithOrigins(orgin)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
        }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Origin");

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapPost("/api/chat/send", async (CreateMessageRequest req, IChatService service) =>
{
    var result = await service.SendMessageAsync(req.UserMessage);
    return Results.Ok(result);
});

app.MapGet("/api/chat/history", async (IChatService service) =>
{
    var messages = await service.GetChatHistoryAsync();
    return Results.Ok(messages);
});

app.MapPost("/api/chat/{id}/rate", async (int id, RateResponseRequest req, IChatService service) =>
{
    await service.RateResponseAsync(id, req.IsPositive);
    return Results.Ok();
});

app.Run();