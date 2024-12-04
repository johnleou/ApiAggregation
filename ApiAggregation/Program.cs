using ApiAggregationProject.Api.Clients;
using ApiAggregationProject.Api.Middleware;
using ApiAggregationProject.Api.Models;
using ApiAggregationProject.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure API endpoints
var userBaseUrl = builder.Configuration["UserApi:BaseUrl"];
var commentBaseUrl = builder.Configuration["CommentApi:BaseUrl"];
var postBaseUrl = builder.Configuration["PostApi:BaseUrl"];

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddMemoryCache();

//Register API clients
builder.Services.AddHttpClient<IDataService<User>, UserClient>();
builder.Services.AddHttpClient<IDataService<Comment>, CommentClient>();
builder.Services.AddHttpClient<IDataService<Post>, PostClient>();

//Register extra services
builder.Services.AddScoped<IAggregationService, AggregationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHelper>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();