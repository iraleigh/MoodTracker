using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database
builder.Services.AddDbContext<MoodDb>(opts =>
{
    var connectionString = Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CONNECTIONSTRING");
    opts.UseNpgsql(connectionString ?? builder.Configuration["Moods:PostgresConnection"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var moods = app.MapGroup("/moods");

app.MapGet("/", Moods.GetMoods).WithName("GetMoods").WithOpenApi();
app.MapGet("/{id}", Moods.GetMood).WithName("GetMood").WithOpenApi();
app.MapPost("/", Moods.CreateMood).WithName("CreateMood").WithOpenApi();
app.MapPut("/{id}", Moods.UpdateMood).WithName("UpdateMood").WithOpenApi();
app.MapDelete("/{id}", Moods.DeleteMood).WithName("DeleteMood").WithOpenApi();


app.Run();